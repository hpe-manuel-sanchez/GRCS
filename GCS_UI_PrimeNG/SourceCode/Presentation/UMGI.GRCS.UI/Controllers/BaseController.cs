/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : BaseController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.UI.Helper;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Controllers
{
    /// <summary>
    /// Base class for Controllers in the project
    /// </summary>
    [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public abstract class BaseController : Controller
    {
        public ISessionWrapper SessionWrapper { get; set; }

        public ILogFactory LoggerFactory { get; set; }
                       
        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {

            if (LoggerFactory != null && LoggerFactory.LogWriter != null)
            {
                LoggerFactory.LogWriter.Debug("Error:", filterContext.Exception);
            }

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };
            filterContext.HttpContext.Response.ClearContent();
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }

        /// <summary>
        /// Redirects to un authorized page.
        /// </summary>
        protected void RedirectToUnAuthorizedPage()
        {
            Response.StatusCode = 401;
            Response.StatusDescription = GlobalConstants.UnAuthorized;
            Response.Redirect(GlobalConstants.RedirectUnAuthorized);
            Response.End();
        }


        /// <summary>
        /// Permissions check and redirect.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        protected void PermissionCheckNdRedirect(GrsTasks[] tasks)
        {
            if (!PermissionExtension.HasAnyPermission(tasks))
            {
                RedirectToUnAuthorizedPage();
            }
        }

        /// <summary>
        /// Permissions check and redirect.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        protected void PermissionCheckNdRedirect(GcsTasks[] tasks)
        {
            if (!PermissionExtension.HasAnyPermission(tasks))
            {
                RedirectToUnAuthorizedPage();
            }
        }

        /// <summary>
        /// Permissions check and redirect.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        protected void PermissionCheckNdRedirect(GcsTasks tasks)
        {
            if (!PermissionExtension.HasPermission(tasks))
            {
                RedirectToUnAuthorizedPage();
            }
        }
       
        /// <summary>
        /// Set the user task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        protected GrsTasks SetUserTask(GrsTasks task)
        {
            var taskDetails = GrsTasks.None;

            if (PermissionExtension.HasPermission(task))
            {
                taskDetails = task;
            }
            return taskDetails;
        }

        /// <summary>
        /// Task updation based on permission.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <param name="clearanceAdminCompanies">The clearance admin companies.</param>
        /// <param name="contractInfo">The contract info.</param>
        /// <param name="tasksPermission">The tasks permission.</param>
        /// <returns></returns>
        protected GrsTasks TaskUpdationBasedOnPermission(GrsTasks tasks, List<ClearanceAdminCompany> clearanceAdminCompanies, ContractInfo contractInfo, GrsTasks tasksPermission)
        {
            if (PermissionExtension.HasPermission(tasksPermission))
            {
                var workflowTask = clearanceAdminCompanies.SingleOrDefault(work => work.Id == Convert.ToInt64(contractInfo.ClearanceCompanyCountryId));
                if (workflowTask != null)
                {
                    tasks = tasksPermission;
                }
            }
            return tasks;
        }

        /// <summary>
        /// Check Task permission based on Role.
        /// </summary>        
        /// <param name="GcsTasks">GcsTasks</param>
        /// <param name="RoleName">RoleName.</param>        
        /// <returns></returns>
        protected Dictionary<GcsTasks, bool> PermissionCheckBasedOnRole(GcsTasks[] tasks, String RoleName)
        {
            return PermissionExtension.HasPermissionBasedOnRole(tasks, RoleName);
            //if (PermissionExtension.HasPermissionBasedOnRole(tasks,RoleName))
            //{
            //    return true;
            //}

            //return false;
        }
    }
}