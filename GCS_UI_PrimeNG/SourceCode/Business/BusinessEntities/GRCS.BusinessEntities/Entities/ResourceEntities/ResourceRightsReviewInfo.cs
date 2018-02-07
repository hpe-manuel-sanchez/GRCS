/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rengaraj 
 * Created on   : 13-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
 * Rengaraj          21/12/2012      Modified for code review
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for ResourceInfo Details
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.Resx.Resource.EntityResource;
using System.ComponentModel.DataAnnotations;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{

    [DataContract]
    public class ResourceRightsReviewInfo 
    {
        public ResourceRightsReviewInfo()
        {
            RightsReviewCondition = new List<RightsDataReviewCondition>();
        }

        /// <summary>
        /// rightsReviewCondition
        /// </summary>
        [DataMember]
        public List<RightsDataReviewCondition> RightsReviewCondition { get; set; }

        /// <summary>
        /// Gets or sets the Legal Review flag.
        /// </summary>
        /// <value>The isLegalReviewRequired.</value>
        [DataMember]
        public bool IsLegalReviewRequired { get; set; }

        /// <summary>
        /// Gets or sets the Review Status Type
        /// </summary>
        /// <value>The reviewStatusType.</value>
        [DataMember]
        [Display(Name = "StatusType", ResourceType = typeof(Entity))] 
        public byte? ReviewStatusType { get; set; }

        /// <summary>
        /// Gets or sets the Review Status Type
        /// </summary>
        /// <value>The reviewStatusType.</value>
        [DataMember]
        public string RightsReviewReason { get; set; }


    }
}
