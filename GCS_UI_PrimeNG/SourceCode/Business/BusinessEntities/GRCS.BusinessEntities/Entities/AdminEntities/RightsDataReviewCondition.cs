/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsDataReviewCondition.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vaishnavi Lakshmanan
 * Created on   : 05-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for RightsDataReviewCondition
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    /// <summary>
    /// RightsDataReviewCondition
    /// </summary>
    [DataContract]
    public class RightsDataReviewCondition : EntityInformation
    {

        /// <summary>
        /// Gets or sets the rights review id.
        /// </summary>
        /// <value>The rights review id.</value>
        [DataMember]
        public long? RightsReviewId { get; set; }

        /// <summary>
        /// Gets or sets the rights review id.
        /// </summary>
        /// <value>The rights review id.</value>
        [DataMember]
        public byte? RightsReviewTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the review rule.
        /// </summary>
        /// <value>The type of the review rule.</value>
        [DataMember]
        public string ReviewRuleTypeDesc { get; set; }

        /// <summary>
        /// Gets or sets the is review.
        /// </summary>
        /// <value>The is review.</value>
        [DataMember]
        public bool? IsReview { get; set; }

        /// <summary>
        /// Gets or sets the review option id.
        /// Used especially for model binding in view.
        /// </summary>
        /// <value>The review option id.</value>
        [DataMember]
        public string ReviewOptionId { get; set; }

        [DataMember]
        public RightsCountry RightCountry { get; set; }

        /// <summary>
        /// For UI Purpose. Denotes checked for the respective rights review
        /// </summary>
        public bool IsRightsReviewChecked { get; set; }
        /// <summary>
        /// Last Modified Time will be helpful while updating a record to check concurrency issue
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the last modified date. 
        /// This property is used to hold the serialized LastModifiedTime for Update scenario. This one need not be datemember property as it's not defined to travel across WCF services.
        /// </summary>
        /// <value>The last modified date.</value>
        public string LastModifiedDate { get; set; }
    }
}
