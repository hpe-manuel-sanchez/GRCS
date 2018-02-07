using Moq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Utilities;

namespace UMGI.GRCS.UnitTestUIProject.Utilities
{
    public static class Initialize
    {
        public static ISessionWrapper GetSessionWrapper(IUserRepository userRepository)
        {
            var sessionWrapper = new Mock<ISessionWrapper>();

            UserInfo userInfo = GetUserInfo();

            sessionWrapper.Setup(
                s => s.GcsCurrentPermissions)
                    .Returns(
                        userRepository.GetGcsAuthorizationByLoginNameAndApplication(userInfo));

            userInfo.UserName = sessionWrapper.Object.GcsCurrentPermissions.UserName;
            userInfo.UserId = sessionWrapper.Object.GcsCurrentPermissions.UserId;
            userInfo.EmailId = sessionWrapper.Object.GcsCurrentPermissions.EmailId;

            sessionWrapper.Setup(s => s.CurrentUserInfo).Returns(userInfo);

            return sessionWrapper.Object;
        }

        static public ILogFactory GetLogFactory()
        {
            var logFactory = new Mock<ILogFactory>();
            logFactory.Setup(s => s.LogWriter).Returns(LogUtil<LogFactory>.CreateInstance());
            return logFactory.Object;
        }

        static public HttpContextBase GenerateContext()
        {
            var server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            var session = new MockHttpSession();

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            context.SetupGet(x => x.Server).Returns(server.Object);
            context.SetupGet(x => x.Session).Returns(session);

            HttpContext.Current = MockContext.FakeHttpContext();

            return context.Object;
        }

        private static UserInfo GetUserInfo()
        {
            UserInfo CurrentUserInfo = new UserInfo
            {
                UserLoginName = ConfigurationManager.AppSettings["UserName"],
                UserApplicationName = AnaTargetApplication.Gcs
            };

            return CurrentUserInfo;
        }

        private class MockHttpSession : HttpSessionStateBase
        {
            Dictionary<string, object> m_SessionStorage = new Dictionary<string, object>();

            public override object this[string name]
            {
                get { return m_SessionStorage[name]; }
                set { m_SessionStorage[name] = value; }
            }
        }

        private class MockContext
        {
            public static HttpContext FakeHttpContext()
            {
                var httpRequest = new HttpRequest("", "http://localhost/", "");
                var stringWriter = new StringWriter();
                var httpResponse = new HttpResponse(stringWriter);
                var httpContext = new HttpContext(httpRequest, httpResponse);

                var sessionContainer = new HttpSessionStateContainer(
                    "id",
                    new SessionStateItemCollection(),
                    new HttpStaticObjectsCollection(),
                    10,
                    true,
                    HttpCookieMode.AutoDetect,
                    SessionStateMode.InProc,
                    false);

                SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

                return httpContext;
            }
        }
    }

    public static class Objects
    {
        public static object GetReflectedProperty(this object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            return property.GetValue(obj, null);
        }
    }
}
