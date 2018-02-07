using System.Text;
using System;
using System.Web.Mvc;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Controllers
{
    /// <summary>
    /// Default controller for the application
    /// </summary>
    public class ErrorController : BaseController
    {
        /// <summary>
        /// About Us Page.
        /// </summary>
        /// <returns></returns>
        public ErrorController(ILogFactory logFactory)
        {
            LoggerFactory = logFactory;
        }

        /// <summary>
        /// About Us Page.
        /// </summary>
        /// <returns></returns>
        public JsonResult LogError(string status, string exception, string postData)
        {
            try
            {
                string message = string.Join(";", status, exception, postData);
                LoggerFactory.LogWriter.Error(Category.UI, message);
                return Json(new { Error = false });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true });
            }
        }
    }
}
