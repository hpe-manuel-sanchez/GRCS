using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : LogActionFilter.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 26-04-2013 
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

namespace UMGI.GRCS.UI.Helper
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log(GlobalConstants.Start, filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log(GlobalConstants.End, filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log(GlobalConstants.ResultStart, filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log(GlobalConstants.ResultEnd, filterContext.RouteData);
        }


        private void Log(string messageString, RouteData routeData)
        {
            try
            {
                var controllerName = routeData.Values[GlobalConstants.Controller];
                var actionName = routeData.Values[GlobalConstants.Action];
                var message = String.Format(" controller:{0} action:{1} {2}:{3}", controllerName, actionName, messageString, DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss.ffffff"));
                //Debug.WriteLine(message, "Message Timing");
                Trace.WriteLine(message, GlobalConstants.ControllerMethodTiming);
                Trace.Flush();
            }
            catch (Exception)
            {
                //No throw
            }
        }

    } 
}