/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : HomeController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 10-07-2012 
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
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.UI.Helper;

namespace UMGI.GRCS.UI.Controllers
{
    /// <summary>
    /// Default controller for the application
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Default page for the rights application
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = string.Empty;
            if (System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] == AnaTargetApplication.Gcs.ToString())
				return RedirectToAction("Index", "ClearanceInbox");
            else
                return View();
        }

        /// <summary>
        /// Refreshes the session.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult RefreshSession()
        {
            return PartialView(GlobalConstants.Index);
        }

        /// <summary>
        /// Returns the menu
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 3600, VaryByParam = "None", VaryByCustom = "UserName")]
        public ActionResult ClearanceMenu()
        {
            return PartialView();
        }
        
        /// <summary>
        /// About Us Page.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult About()
        {
            return PartialView();
        }
    }
}
