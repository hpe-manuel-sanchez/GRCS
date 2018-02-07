/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : SearchRightsCriteria.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : Rikhu Prasad
 * Created on   : 17/12/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for SearchRightsCriteria
                  
****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// Rights Search
    /// </summary>
    [DataContract]
    [Serializable]
    public class SearchRightsCriteria : EntityInformation
    {
        /// <summary>
        /// Gets or sets a value indicating whether [active for marketing].
        /// </summary>
        /// <value><c>true</c> if [active for marketing]; otherwise, <c>false</c>.</value>
        [DataMember]
        public byte? IsActiveForMarketing { get; set; }
        
        /// <summary>
        /// Gets or sets the digital exploitation rights.
        /// </summary>
        /// <value>The digital exploitation rights.</value>
        [DataMember]
        public byte? IsDigitalExploitationRights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is digital unbundling allowed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is digital unbundling allowed; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public byte? IsDigitalUnbundlingAllowed { get; set; }

        /// <summary>
        /// Gets or sets the mobile exploitation rights.
        /// </summary>
        /// <value>The mobile exploitation rights.</value>
        [DataMember]
        public byte? IsMobileExploitationRights { get; set; }

        /// <summary>
        /// Gets or sets the physical exploitation rights.
        /// </summary>
        /// <value>The physical exploitation rights.</value>
        [DataMember]
        public byte? IsPhysicalExploitationRights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is PPB revenue chain.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is PPB revenue chain; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public byte? IsPpbRevenueChain { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether [lost rights indicator].
        /// </summary>
        /// <value><c>true</c> if [lost rights indicator]; otherwise, <c>false</c>.</value>
        [DataMember]
        public byte? LostRightsIndicator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [lost rights reason].
        /// </summary>
        /// <value><c>true</c> if [lost rights reason]; otherwise, <c>false</c>.</value>
        [DataMember]
        public int? LostRightsReasonId { get; set; }

        /// <summary>
        /// Gets or sets the lost rights from date.
        /// </summary>
        /// <value>The lost rights from date.</value>
        [DataMember]
        public string LostRightsFromDate { get; set; }

        /// <summary>
        /// Gets or sets the lost rights to date.
        /// </summary>
        /// <value>The lost rights to date.</value>
        [DataMember]
        public string LostRightsToDate { get; set; }
        
        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        [DataMember]
        public byte? RightsPeriodId { get; set; }

        /// <summary>
        /// Gets or sets the rights review status.
        /// </summary>
        /// <value>The rights review status.</value>
        [DataMember]
        public byte? RightsReviewStatusId { get; set; }

        /// <summary>
        /// Gets or sets the territorial rights.
        /// </summary>
        /// <value>The territorial rights.</value>
        [DataMember]
        public string TerritorialRights { get; set; }

        #region "For UI Purpose"
        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? FromDt { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? ToDt { get; set; }

        [DataMember]
        public string TerritorialRightsId { get; set; }

        [DataMember]
        public string AdminCompaniesList { get; set; }
        #endregion 

    }
 }
