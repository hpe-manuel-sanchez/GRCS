/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : SecondaryRightsResult.cs 
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
* Description :  Defines the entities for Secondary Rights Result Details
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Secondary Rights Result Details
    /// </summary>
    [DataContract]
    public class SecondaryRightsResult
    {
        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>The rights.</value>
        [DataMember]
        public ResourceSecondaryRights Rights
        {
            get;
            set;
        }
    }
}
