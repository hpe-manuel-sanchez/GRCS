/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : CustomControllerActivator.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 24-07-2012 
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
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Class for Controller Injection 
    /// </summary>
    public class CustomControllerActivator : IControllerActivator
    {
        /// <summary>
        /// When implemented in a class, creates a controller.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerType">The controller type.</param>
        /// <returns>The created controller.</returns>
        IController IControllerActivator.Create(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }

}