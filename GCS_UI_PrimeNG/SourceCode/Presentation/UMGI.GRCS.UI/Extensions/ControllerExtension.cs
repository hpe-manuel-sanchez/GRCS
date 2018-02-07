
/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : CloneExtension.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 15-02-2013 
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
using System.IO;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Extensions
{
  public static class ControllerExtensionsHelper
        {
            public static string PartialViewToString(this Controller controller)
            {
                return controller.PartialViewToString(null, null);
            }

            public static string RenderPartialViewToString(this Controller controller, string viewName)
            {
                return controller.PartialViewToString(viewName, null);
            }

            public static string RenderPartialViewToString(this Controller controller, object model)
            {
                return controller.PartialViewToString(null, model);
            }

            public static string PartialViewToString(this Controller controller, string viewName, object model)
            {
                if (string.IsNullOrEmpty(viewName))
                {
                    viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
                }

                controller.ViewData.Model = model;

                using (var stringWriter = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
                    viewResult.View.Render(viewContext, stringWriter);
                    return stringWriter.GetStringBuilder().ToString();
                }
            }
        }
}