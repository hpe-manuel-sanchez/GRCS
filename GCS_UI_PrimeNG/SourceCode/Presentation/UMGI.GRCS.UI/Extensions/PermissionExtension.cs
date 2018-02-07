/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : PermissionExtension.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 02-08-2012 
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.UI.Helper;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Extensions
{
    /// <summary>
    /// Permission related extension class
    /// </summary>
    public static class PermissionExtension
    {
        /// <summary>
        /// Applies the permission.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="selectedClearanceAdminComp">The selected clearance admin comp.</param>
        /// <param name="selectedClearanceAdminCountry">The selected clearance admin country.</param>
        /// <returns></returns>
        public static MvcHtmlString ApplyPermission(this HtmlHelper helper, string elementName, string elementTag, long selectedClearanceAdminComp = 0, long selectedClearanceAdminCountry = 0)
        {
            var controlPermissions = ControlPermissions.GetControlPermissions();
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.CurrentPermissions != null)
            {
                var tasks = new List<GrsTasks>();

                if (controlPermissions.Any(permission => permission.Control == elementName && permission.Visibility == GlobalConstants.ControlVisible))
                {
                    tasks = controlPermissions.Where(permission => permission.Control == elementName && permission.Visibility == GlobalConstants.ControlVisible).Select(task => (GrsTasks)Enum.Parse(typeof(GrsTasks),task.TaskId.ToString(CultureInfo.InvariantCulture))).ToList();
                }

                return MvcHtmlString.Create(tasks.Any(task => sessionWrapper.CurrentPermissions.Permissions.Any(permission => permission.Tasks.HasFlag(task))) ? elementTag : string.Empty);
            }
            return MvcHtmlString.Create(elementTag);
        }

        /// <summary>
        /// Creates ActionLink with permission
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <returns></returns>
        public static MvcHtmlString ActionLinkWithPermission(this HtmlHelper helper, string linkText, string actionName, string controllerName,string controlName)
        {
            var controlPermissions = ControlPermissions.GetControlPermissions();
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.CurrentPermissions != null)
            {
                var tasks = new List<GrsTasks>();

                if (controlPermissions.Any(permission => permission.Control == controlName && permission.Visibility == GlobalConstants.ControlVisible))
                {
                    tasks = controlPermissions.Where(permission => permission.Control == controlName && permission.Visibility == GlobalConstants.ControlVisible).Select(task => (GrsTasks)Enum.Parse(typeof(GrsTasks), task.TaskId.ToString(CultureInfo.InvariantCulture))).ToList();
                }

                if (tasks.Any(task => sessionWrapper.CurrentPermissions.Permissions.Any(permission => permission.Tasks.HasFlag(task))))
                {
                    return helper.ActionLink(linkText, actionName, controllerName);
                }

                return MvcHtmlString.Create(string.Empty);
            }

            return helper.ActionLink(linkText, actionName, controllerName);            
        }

        /// <summary>
        /// Determines whether the specified permission is available for the user.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// 	<c>true</c> if the specified task has permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPermission(GrsTasks task)
        {
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.CurrentPermissions != null && sessionWrapper.CurrentPermissions.Permissions != null)
            {
                return sessionWrapper.CurrentPermissions.Permissions.Any(permission => permission.Tasks.HasFlag(task));
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified permission is available for the user.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// 	<c>true</c> if the specified task has permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPermission(GcsTasks task)
        {
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.GcsCurrentPermissions != null && sessionWrapper.GcsCurrentPermissions.Permissions != null)
            {
                return sessionWrapper.GcsCurrentPermissions.Permissions.Any(permission => permission.Tasks.Contains(task));
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified permission is available for the role.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// 	<c>true</c> if the specified task has permission; otherwise, <c>false</c>.
        /// </returns>
        public static Dictionary<GcsTasks, bool> HasPermissionBasedOnRole(GcsTasks[] tasks, string RoleName)
        {
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            Dictionary<GcsTasks, bool> taskList = new Dictionary<GcsTasks, bool>();
            if (sessionWrapper.GcsCurrentPermissions != null && sessionWrapper.GcsCurrentPermissions.Permissions != null)
            {

                foreach (var permission in sessionWrapper.GcsCurrentPermissions.Permissions)
                {
                    if (permission.Role.ToUpper() == RoleName.ToUpper())
                    {
                        foreach (var task in tasks)
                        {
							if (permission.Tasks.Contains(task))
                            {
                                // var value= sessionWrapper.GcsCurrentPermissions.Permissions.Select(permission =>permission.Tasks  permission.Role.ToUpper() == RoleName && permission.Tasks.HasFlag(task));
                                taskList.Add(task, true);
                            }
                            else
                            {
                                taskList.Add(task, false);
                            }
                        }
                    }
                }
                
            }
            return taskList;
            
        }

        /// <summary>
        /// Determines whether one of the permissions are available for the user.
        /// </summary>
        /// <param name="tasks">The tasks to be checked.</param>
        /// <returns>
        /// 	<c>true</c> if the specified task has permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAnyPermission(GrsTasks[] tasks)
        {
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.CurrentPermissions != null && sessionWrapper.CurrentPermissions.Permissions != null)
            {
                return tasks.Select(task => sessionWrapper.CurrentPermissions.Permissions.Any(permission => permission.Tasks.HasFlag(task))).Any(returnValue => returnValue);
            }
            return false;
        }

        /// <summary>
        /// Determines whether one of the permissions are available for the user.
        /// </summary>
        /// <param name="tasks">The tasks to be checked.</param>
        /// <returns>
        /// 	<c>true</c> if the specified task has permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAnyPermission(GcsTasks[] tasks)
        {
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.GcsCurrentPermissions != null && sessionWrapper.GcsCurrentPermissions.Permissions != null)
            {
				return tasks.Select(task => sessionWrapper.GcsCurrentPermissions.Permissions.Any(permission => permission.Tasks.Contains(task))).Any(returnValue => returnValue);
            }
            return false;
        }


        /// <summary>
        /// Filters the permissions.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public static GrsPermission[] FilterPermissions(GrsTasks task)
        {            
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.CurrentPermissions != null && sessionWrapper.CurrentPermissions.Permissions != null)
            {
                var filteredPermissions = new List<GrsPermission>();
                var allPermissions = (sessionWrapper.CurrentPermissions.CloneDataContract()).Permissions;
                allPermissions.All(permission =>
                {

                    if (permission.Tasks.HasFlag(task))
                    {
                        permission.Tasks = task;
                        filteredPermissions.Add(permission);
                    }
                    return true;
                });
                return filteredPermissions.ToArray();
            }
            return new GrsPermission[0];
        }
      
      

        /// <summary>
        /// Filters the permissions based on tasks.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns></returns>
        public static GrsPermission[] FilterPermissions(GrsTasks[] tasks)
        {
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            if (sessionWrapper.CurrentPermissions != null && sessionWrapper.CurrentPermissions.Permissions != null)
            {
                var filteredPermissions = new List<GrsPermission>();
                var allPermissions = (sessionWrapper.CurrentPermissions.CloneDataContract()).Permissions;
                allPermissions.All(permission =>
                {
                    foreach (var task in tasks)
                    {
                        if (permission.Tasks.HasFlag(task))
                        {
                            permission.Tasks = task;
                            filteredPermissions.Add(permission);
                        }
                    }
                    return true;
                });
                return filteredPermissions.ToArray();
            }
            return new GrsPermission[0];
        }

    }
}
