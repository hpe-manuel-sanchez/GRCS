using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.UI.Helper;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.Utilities;

namespace UMGI.GRCS.UI
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Ignore png, js, gif, css.
            routes.IgnoreRoute("{file}.png");
            routes.IgnoreRoute("{file}.js");
            routes.IgnoreRoute("{file}.gif");
            routes.IgnoreRoute("{file}.css");

            routes.MapRoute(
              "Default", // Route name
              "{controller}/{action}/{id}", // URL with parameters
              new { controller = "workgroup", action = "Index", id = UrlParameter.Optional } // Parameter defaults
          );
        }

        /// <summary>
        /// Application Start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new UnityDependencyResolver(GetUnityContainer()));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                if (Context.Handler is IRequiresSessionState)
                {
                    HttpContext context = HttpContext.Current;
                    var userName = string.Empty;
                    //Set CurrentUserInfo
                    var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
                    if (sessionWrapper.CurrentUserInfo == null || !sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                    {
                        userName = ConfigurationManager.AppSettings["UserName"];
                        if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["UserName"]))
                            userName = context.User.Identity.Name.Substring(context.User.Identity.Name.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                    }

                    if (sessionWrapper.GcsCurrentPermissions == null || !sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                        sessionWrapper.CurrentUserInfo = new UserInfo
                        {
                            UserLoginName = userName,
                            UserApplicationName = AnaTargetApplication.Gcs
                        };

                    DependencyResolver.Current.GetService<ILogFactory>().LogWriter.SetAttributes(userName, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

                    if (sessionWrapper.GcsCurrentPermissions == null)
                    {
                        if (sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName != null)
                            sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName = null;
                        sessionWrapper.GcsCurrentPermissions = DependencyResolver.Current
                            .GetService<IUserRepository>()
                            .GetGcsAuthorizationByLoginNameAndApplication(sessionWrapper.CurrentUserInfo);
                    }

                    if (sessionWrapper.GcsCurrentPermissions == null)
                    {
                        Response.StatusCode = 401;
                        Response.StatusDescription = GlobalConstants.UnAuthorized;
                        Response.Redirect(GlobalConstants.RedirectUnAuthorized);
                        Response.End();
                    }
                    else
                    {
                        sessionWrapper.CurrentUserInfo.UserName = sessionWrapper.GcsCurrentPermissions.UserName;
                        sessionWrapper.CurrentUserInfo.UserId = sessionWrapper.GcsCurrentPermissions.UserId;
                        sessionWrapper.CurrentUserInfo.EmailId = sessionWrapper.GcsCurrentPermissions.EmailId;

                        if (sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                            sessionWrapper.CurrentUserInfo.IsMimicUser = true;
                    }
                }
            }
            catch (FaultException Ex)
            {
                if (Ex.Code.Name == "30101")
                    Response.Redirect("~/UnAuthorized.htm");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unauthorized user")
                    Response.Redirect("~/UnAuthorized.htm");
                else
                    throw ex;
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            return (custom == GlobalConstants.UserName)
                ? DependencyResolver.Current.GetService<ISessionWrapper>().CurrentUserInfo.UserLoginName
                : base.GetVaryByCustomString(context, custom);
        }

        /// <summary>
        /// Gets the unity container.
        /// </summary>
        /// <returns></returns>
        private IUnityContainer GetUnityContainer()
        {
            //Create UnityContainer
            IUnityContainer container = new UnityContainer()
                //.RegisterType<IControllerActivator, CustomControllerActivator>()
                .RegisterType<ISessionWrapper, HttpContextSessionWrapper>()
                .RegisterType<IConfigFactory, ConfigFactory>()
                .RegisterType<IServiceFactory, ServiceFactory>()
                .RegisterType<ILogFactory, LogFactory>()
                .RegisterType<IContractRepository, ContractRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                //.RegisterType<IAdminRepository, AdminRepository>()
                //.RegisterType<IWorkQueueRepository, WorkQueueRepository>()
                .RegisterType<IWorkgroupRepository, WorkgroupRepository>()
                .RegisterType<IClearanceReleaseRepository, ClearanceReleaseRepository>()
                .RegisterType<IClearanceResourceRepository, ClearanceResourceRepository>()
                .RegisterType<IArtistRepository, ArtistRepository>()
                .RegisterType<IClearanceInboxRepository, ClearanceInboxRepository>()
                .RegisterType<IGlobalRepository, GlobalRepository>()
                .RegisterType<IClearanceProjectRepository, ClearanceProjectRepository>()
                .RegisterType<IRoutingRepository, RoutingRepository>();
            return container;
        }
    }
}