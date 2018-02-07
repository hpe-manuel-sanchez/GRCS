/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DigitalRightsResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Digital Rights Result details
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Digital Rights 
    /// Result Details
    /// </summary>
    [DataContract]
    public class DigitalRightsResult :Resource
    {
        /// <summary>
        /// Gets or sets the resource digital rights.
        /// </summary>
        /// <value>The resource digital rights.</value>
        [DataMember]
        public ResourceDigitalRestrictions ResourceDigitalRights
        {
            get;
            set;
        }
    }
}
