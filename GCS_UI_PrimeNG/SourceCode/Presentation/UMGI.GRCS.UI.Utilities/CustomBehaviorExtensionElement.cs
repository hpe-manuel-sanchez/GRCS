/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : CustomBehaviorExtensionElement.cs
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

using System;
using System.ServiceModel.Configuration;

namespace UMGI.GRCS.UI.Utilities
{
    public class CustomBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new WcfEndpointBehavior();
        }

        public override Type BehaviorType
        {
            get { return typeof(WcfEndpointBehavior); }
        }
    }
}
