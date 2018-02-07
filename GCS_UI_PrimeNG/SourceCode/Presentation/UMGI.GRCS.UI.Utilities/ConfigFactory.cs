using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.Core.Utilities.Helper;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Class used for accessing Config - AppSetting Value
    /// </summary>
    public class ConfigFactory : IConfigFactory
    {
        private ILogFactory _logFactory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFactory"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        public ConfigFactory(ILogFactory loggerFactory)
        {
            try
            {
                _logFactory = loggerFactory;
                Binding = ConfigUtil.GetAppSettingsValue(Constants.Binding);
                IsSecurityEnabled = bool.Parse(ConfigUtil.GetAppSettingsValue(Constants.IsSecured));
                IsBindingInConfig = bool.Parse(ConfigUtil.GetAppSettingsValue(Constants.IsBindingInConfig));
                TimeOut = int.Parse(ConfigUtil.GetAppSettingsValue(Constants.ServiceTimeout));
                ContractService = ConfigUtil.GetAppSettingsValue(Constants.ContractService);
                ArtistService = ConfigUtil.GetAppSettingsValue(Constants.ArtistService);
                ProjectService = ConfigUtil.GetAppSettingsValue(Constants.ProjectService);
                ReleaseService = ConfigUtil.GetAppSettingsValue(Constants.ReleaseService);
                ResourceService = ConfigUtil.GetAppSettingsValue(Constants.ResourceService);
                RightsService = ConfigUtil.GetAppSettingsValue(Constants.RightsService);
                UserService = ConfigUtil.GetAppSettingsValue(Constants.UserService);
                GrcsUtilityService = ConfigUtil.GetAppSettingsValue(Constants.GrcsUtilityService);
                WorkQueueService = ConfigUtil.GetAppSettingsValue(Constants.WorkQueueService);
                GlobalService = ConfigUtil.GetAppSettingsValue(Constants.GlobalService);
                RepertoireService = ConfigUtil.GetAppSettingsValue(Constants.RepertoireService);
                PCompanyService = ConfigUtil.GetAppSettingsValue(Constants.PCompanyService);
                WorkgroupService = ConfigUtil.GetAppSettingsValue(Constants.WorkgroupService);
                ClearanceInboxService = ConfigUtil.GetAppSettingsValue(Constants.ClearanceInboxService);
                ClearanceProjectService = ConfigUtil.GetAppSettingsValue(Constants.ClearanceProjectService);
                ClearanceResourceService = ConfigUtil.GetAppSettingsValue(Constants.ClearanceResourceService);
                ClearanceReleaseService = ConfigUtil.GetAppSettingsValue(Constants.ClearanceReleaseService);
                RoutingService = ConfigUtil.GetAppSettingsValue(Constants.RoutingService);
                PageSize = ConfigUtil.GetNumericValue(_logFactory.LogWriter, Constants.PageSize, 25);

                PageSizeValues = new Dictionary<int, string>();
                string[] pageSizeArray = ConfigUtil.GetAppSettingsValue(Constants.PageSizeValues).Split(',');
                if (pageSizeArray != null)
                {
                    pageSizeArray.All(item =>
                    {
                        PageSizeValues.Add(GetValue(item), item);
                        return true;
                    });
                }

                AppVersion = ConfigUtil.GetAppSettingsValue(Constants.AppVersion);
                AppEnvironment = ConfigUtil.GetAppSettingsValue(Constants.AppEnvironment);
                AppBuildDate = ConfigUtil.GetAppSettingsValue(Constants.AppBuildDate);
                IsContractAdminModuleOnly = bool.Parse(ConfigUtil.GetAppSettingsValue(Constants.IsContractAdminModuleOnly));

                IsLocalDateTimeEnabled = bool.Parse(ConfigUtil.GetAppSettingsValue(Constants.IsLocalDateTimeEnabled));

                ReportService = ConfigUtil.GetAppSettingsValue(Constants.ReportService);
                ReportServerUrl = ConfigUtil.GetAppSettingsValue(Constants.ReportServerUrl);
                ReportUserDomain = ConfigUtil.GetAppSettingsValue(Constants.ReportUserDomain);
                ReportUserName = ConfigUtil.GetAppSettingsValue(Constants.ReportUserName);
                ReportUserPassword = ConfigUtil.GetAppSettingsValue(Constants.ReportUserPassword);
                ReportPath = ConfigUtil.GetAppSettingsValue(Constants.ReportPath);
            }
            catch (System.Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "ConfigFactory"), ex);
                throw;
            }
        }

        private int GetValue(string item)
        {
            int pageSize;
            int.TryParse(item, out pageSize);
            return pageSize;
        }

        public string Binding { get; set; }

        public bool IsSecurityEnabled { get; set; }

        public bool IsBindingInConfig { get; set; }

        public int TimeOut { get; set; }

        public string ContractService { get; set; }

        public string ArtistService { get; set; }

        public string ProjectService { get; set; }

        public string ReleaseService { get; set; }

        public string ResourceService { get; set; }

        public string RightsService { get; set; }

        public string UserService { get; set; }

        public string GrcsUtilityService { get; set; }

        public string WorkQueueService { get; set; }

        public string GlobalService { get; set; }

        public string RepertoireService { get; set; }

        public string PCompanyService { get; set; }

        public int PageSize { get; set; }

        public Dictionary<int, string> PageSizeValues { get; set; }

        public string AppVersion { get; set; }

        public string AppEnvironment { get; set; }

        public string AppBuildDate { get; set; }

        public bool IsContractAdminModuleOnly { get; set; }

        public bool IsLocalDateTimeEnabled { get; set; }

        public string ReportService { get; set; }

        public string ReportServerUrl { get; set; }

        public string ReportUserDomain { get; set; }

        public string ReportUserName { get; set; }

        public string ReportUserPassword { get; set; }

        public string ReportPath { get; set; }

        //GCS
        public string WorkgroupService { get; set; }

        public string ClearanceInboxService { get; set; }

        public string ClearanceProjectService { get; set; }

        public string ClearanceResourceService { get; set; }

        public string ClearanceReleaseService { get; set; }

        public string RoutingService { get; set; }

        /// <summary>
        /// Gets the page size list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPageSizeList()
        {
            return PageSizeValues.Select(item => new SelectListItem
            {
                Text = item.Value,
                Value = item.Key.ToString(CultureInfo.InvariantCulture)
            });
        }
    }
}