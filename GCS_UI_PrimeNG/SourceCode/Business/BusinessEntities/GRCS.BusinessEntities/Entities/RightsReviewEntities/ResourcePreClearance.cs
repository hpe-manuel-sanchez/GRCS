/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourcePreClearance.cs 
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
* Description :  Defines the entities for Resource PreClearance Details
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource PreClearance Details
    /// </summary>
    [DataContract]
    public class ResourcePreClearance
    {
        /// <summary>
        /// Gets or sets the pre clearance info.
        /// </summary>
        /// <value>The pre clearance info.</value>
        [DataMember]
        public PreClearanceInfo PreClearanceInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        [DataMember]
        public ReviewStatus ReviewStatus
        {
            get;
            set;
        }
    }
}
