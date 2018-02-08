/* ***************************************************************************
 * Copyrights ® 2012 Universal Musical Group
 * ***************************************************************************
 * File Name      : UnityDependencyResolver.cs
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
using System.Collections.Generic;
using System.Web.Mvc;
using Unity;

namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Dependency relover for Unity Framework
    /// Logger Service will not be available at this time for logging
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>The requested service or object.</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>The requested services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}
