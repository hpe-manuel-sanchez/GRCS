using System;
using System.Web.Mvc;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Factory class for logging in UI layer
    /// </summary>
    public class LogFactory : ILogFactory
    {
        private const string LOGGING_CONFIG_APP_SETTING_KEY = "LOGGING_CONFIG";

        public LogFactory()
        {
            LogInitializer.Initialize(LOGGING_CONFIG_APP_SETTING_KEY);
            LogWriter = LogUtil<LogFactory>.CreateInstance();
            MeasureLogWriter = LogUtil<LogFactory>.CreateMeasureInstance();
            SetUserRelatedInfo();
        }

        public ILog LogWriter { get; set; }
        public IMeasureLog MeasureLogWriter { get; set; }

        private void SetUserRelatedInfo()
        {
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            LogWriter.SetAttributes(sessionWrapper.CurrentUserInfo.UserLoginName, sessionWrapper.CurrentUserInfo.ClientIpAddress);
            MeasureLogWriter.SetAttributes(sessionWrapper.CurrentUserInfo.UserLoginName, sessionWrapper.CurrentUserInfo.ClientIpAddress);
        }
    }
}