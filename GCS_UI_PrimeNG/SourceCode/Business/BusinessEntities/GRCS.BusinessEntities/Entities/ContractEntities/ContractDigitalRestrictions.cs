using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractDigitalRestrictions.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Contract Digital Restrictions
                  
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// ContractDigitalRestrictions which is inherited from ClassInfo class
    /// </summary>
    [DataContract]
    [Serializable]
    public class ContractDigitalRestrictions : EntityInformation
    {
        /// <summary>
        /// Property holding the NotificationRecipient "DigitalRestrictionId"
        /// </summary>
        [DataMember]
        public long DigitalRestrictionId { get; set; }

        /// <summary>
        /// Check Digital Restriction
        /// </summary>
        [DataMember]
        public bool DigitalRestrictionIdChecked { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "ContentTypeId"
        /// </summary>
        [DataMember]
        public byte? ContentTypeId { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "UseTypeId"
        /// </summary>
        [DataMember]
        public byte UseTypeId { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "CommercialModelId"
        /// </summary>
        [DataMember]
        public byte CommercialModelId { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "RestrictionId"
        /// </summary>
        [DataMember]
        public byte RestrictionId { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "ConsentPeriodId"
        /// </summary>
        [DataMember]
        public byte? ConsentPeriodId { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "Notes"
        /// </summary>
        /// <value>The notes.</value>
        [DataMember]
        public string Notes { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is inserted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is inserted; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsInserted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsDeleted { get; set; }

        #region UC016

        /// <summary>
        /// Gets or sets a value indicating whether [restriction exist].
        /// </summary>
        /// <value><c>true</c> if [restriction exist]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool RestrictionExist { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [restriction does not exist].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [restriction does not exist]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool RestrictionDoesNotExist { get; set; }

        /// <summary>
        /// Gets or sets the is restriction.
        /// </summary>
        /// <value>The is restriction.</value>
        [DataMember]
        public bool? IsRestriction { get; set; }
        #endregion 

        #region UC20

        /// <summary>
        /// Gets or sets the right set id.
        /// </summary>
        /// <value>The right set id.</value>
        [DataMember]
        public long RightSetId { get; set; }

        /// <summary>
        /// Gets or sets the restriction reason id.
        /// </summary>
        /// <value>The restriction reason id.</value>
        [DataMember]
        public byte? RestrictionReasonId { get; set; }

        /// <summary>
        /// Gets or sets the restriction reason id.
        /// </summary>
        /// <value>The restriction reason id.</value>
        [DataMember]
        public string RestrictionOtherReasonInfo { get; set; }

        [DataMember]
        public string LostRights { get; set; }

        #endregion

        /// <summary>
        /// Property holding the ContentTypeDesc
        /// </summary>
        [DataMember]
        public string ContentTypeDesc { get; set; }

        /// <summary>
        /// Property holding the UseTypeDesc
        /// </summary>
        [DataMember]
        public string UseTypeDesc { get; set; }

        /// <summary>
        /// Property holding the CommercialModelDec
        /// </summary>
        [DataMember]
        public string CommercialModelDec { get; set; }
        /// <summary>
        /// Property holding the RestrictionDesc
        /// </summary>
        [DataMember]
        public string RestrictionDesc { get; set; }

        /// <summary>
        /// Property holding the ConsentPeriodDesc
        /// </summary>
        [DataMember]
        public string ConsentPeriodDesc { get; set; }

        /// <summary>
        /// Property holding the RestrictionTypeId
        /// </summary>
        [DataMember]
        public byte RestrictionTypeId { get; set; }


    }
}