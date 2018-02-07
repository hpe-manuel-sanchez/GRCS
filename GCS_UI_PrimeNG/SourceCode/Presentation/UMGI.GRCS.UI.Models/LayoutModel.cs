/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : LayoutModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 28-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */
using UMGI.GRCS.UI.Interfaces;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Model for Layout Page
    /// </summary>
    public class LayoutModel:ILayoutModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutModel"/> class.
        /// </summary>
        public LayoutModel()
        {
            SessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            UserName = SessionWrapper.CurrentUserInfo.UserLoginName;

            var configFactory = DependencyResolver.Current.GetService<IConfigFactory>();
            AppVersion = configFactory.AppVersion;
            AppEnvironment = configFactory.AppEnvironment;
            AppBuildDate = configFactory.AppBuildDate;
        }

        ISessionWrapper SessionWrapper { get; set; }

        public string AppBuildDate { get; set; }
        public string AppEnvironment { get; set; }
        public string AppVersion { get; set; }
        public string UserName { get; set; }
    }
}
