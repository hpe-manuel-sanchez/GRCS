/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : SimpleBehaviorExtensionElement.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 21-12-2012 
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
using System.ServiceModel.Configuration;

namespace UMGI.GRCS.UI.Utilities
{
    public class SimpleBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(WcfEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            // Create the  endpoint behavior that will insert the message
            // inspector into the client runtime
            return new WcfEndpointBehavior();
        }
    }
}


