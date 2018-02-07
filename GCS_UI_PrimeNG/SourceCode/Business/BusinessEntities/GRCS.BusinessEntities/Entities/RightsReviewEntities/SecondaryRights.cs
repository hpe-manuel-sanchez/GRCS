/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : SecondaryRights.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 11-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Secondary Rights Details
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Secondary Rights Details
    /// </summary>
    [DataContract]
    public class SecondaryRights : RightSetInfo
    {
        public SecondaryRights()
        {
            SecondaryExploitation = 
                new List<ContractExploitationRestrictions>();
            ReviewStatus = new ReviewStatus();
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

        /// <summary>
        /// Gets or sets the secondary exploitation.
        /// </summary>
        /// <value>The secondary exploitation.</value>
        [DataMember]
        public List<ContractExploitationRestrictions> SecondaryExploitation
        {
            get;
            set;
        }
    }
}
