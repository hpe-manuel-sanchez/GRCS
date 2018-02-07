/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractDetails.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 17-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Contract Details
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// ContractDetails which is inherited from ContractInfo Class
    /// </summary>
    [DataContract]
    [Serializable]
    public partial class ContractDetails : ContractInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractDetails"/> class.
        /// </summary>
        public ContractDetails()
        {
            ArtistInfo = new ArtistInfo();
            //this.ContractTerritoryInfo = new TerritoryInfo();
            ClearanceCompanyCountryInfo = new CompanyInfo();
            ContractExploitationRestrictionsList = new List<ContractExploitationRestrictions>();
            RightsAndRestrictions = new RightsAndRestrictions();
            SecondaryExploitationTemplate = new SecondaryExploitationTemplate();
            ContractTerritoryList = new List<TerritorialDisplay>();
            WorkFlowIdentifier = Guid.NewGuid();
            SplitDealContracts = new List<ContractInfo>();
        }

        

        /// <summary>
        /// Default value is null.
        /// </summary>
        /// <value>The is contract in active roster.</value>
        [DataMember]
        public bool? IsContractInActiveRoster { get; set; }

        /// <summary>
        /// Gets or sets the rights expiry date.
        /// </summary>
        /// <value>The rights expiry date.</value>
        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? RightsExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loss rights indicator.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loss rights indicator; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public Boolean IsLossRightsIndicator { get; set; }

        /// <summary>
        /// Gets/ Sets the look up value, LostRightsReasonId
        /// </summary>
        /// <value>The lost rights reason id.</value>
        [DataMember]
        public byte? LostRightsReasonId { get; set; }

        /// <summary>
        /// Gets or sets the rights expiry rule.
        /// </summary>
        /// <value>The rights expiry rule.</value>
        [DataMember]
        public string RightsExpiryRule { get; set; }

        /// <summary>
        /// Gets or sets the rights exceptions.
        /// </summary>
        /// <value>The rights exceptions.</value>
        [DataMember]
        public string RightsExceptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rights exception applied.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is rights exception applied; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public Boolean IsRightsExceptionApplied { get; set; }

        /// <summary>
        /// Gets or sets the rights reversion.
        /// </summary>
        /// <value>The rights reversion.</value>
        [DataMember]
        public string RightsReversion { get; set; }

        /// <summary>
        /// Gets or sets the rights exception notes.
        /// </summary>
        /// <value>The rights exception notes.</value>
        [DataMember]
        public string RightsExceptionNotes { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is clearance routing contract.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is clearance routing contract; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsClearanceRoutingContract { get; set; }
        

        /// <summary>
        /// Gets or sets the pc notice country company.
        /// </summary>
        /// <value>The pc notice country company.</value>
        [DataMember]
        public string PcNoticeCountryCompany { get; set; }

        /// <summary>
        /// Gets or sets the pc notice company extension.
        /// </summary>
        /// <value>The pc notice company extension.</value>
        [DataMember]
        public string PcNoticeCompanyExtension { get; set; }

        

        //Clearance Control

        /// <summary>
        /// Gets or sets the is legal rights review required. If not given, default is null.
        /// </summary>
        /// <value>The is legal rights review required.</value>
        [DataMember]
        public bool? IsLegalRightsReviewRequired { get; set; }

        /// <summary>
        /// Default value is null. Gets or Sets the value whether Contract is active for marketing or not
        /// </summary>
        /// <value>The is active for marketing.</value>
        [DataMember]
        public bool? IsActiveForMarketing { get; set; }

        [DataMember]
        public bool IsActiveForMarketingChanged { get; set; }

        /// <summary>
        /// Gets or sets the is rights type changed.
        /// </summary>
        /// <value>The is rights type changed.</value>
        [DataMember]
        public long IsRightsTypeChanged { get; set; }


        /// <summary>
        /// Gets or sets the is sensitive artist. Null is default
        /// </summary>
        /// <value>The is sensitive artist.</value>
        [DataMember]
        public bool? IsSensitiveArtist { get; set; }

        /// <summary>
        /// Gets or sets the clearing notes.
        /// </summary>
        /// <value>The clearing notes.</value>
        [DataMember]
        public string ClearingNotes { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [DataMember]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the email receipients.
        /// </summary>
        /// <value>The email receipients.</value>
        [DataMember]
        public string EmailReceipients { get; set; }

        /// <summary>
        /// Gets or sets the email receipient ids.
        /// </summary>
        /// <value>The email receipient ids.</value>
        [DataMember]
        public string EmailReceipientIds { get; set; }

        /// <summary>
        /// Gets or sets the artist info.
        /// </summary>
        /// <value>The artist info.</value>
        [DataMember]
        public ArtistInfo ArtistInfo { get; set; }

        
        /// <summary>
        /// Gets or sets the pc notice country company id.
        /// </summary>
        /// <value>The pc notice country company id.</value>
        [DataMember]
        public long PcNoticeCountryCompanyId { get; set; }

        
       
        /// <summary>
        /// Gets or sets the contract territory list.
        /// </summary>
        /// <value>The contract territory list.</value>
        [DataMember]
        public List<TerritorialDisplay> ContractTerritoryList { get; set; }

        /// <summary>
        /// Gets or sets the rights and restrictions.
        /// </summary>
        /// <value>The rights and restrictions.</value>
        [DataMember]
        public RightsAndRestrictions RightsAndRestrictions { get; set; }

        /// <summary>
        /// Gets or sets the secondary exploitation template.
        /// </summary>
        /// <value>The secondary exploitation template.</value>
        [DataMember]
        public SecondaryExploitationTemplate SecondaryExploitationTemplate { get; set; }

        

        /// <summary>
        /// Gets or sets the contract exploitation restrictions list.
        /// </summary>
        /// <value>The contract exploitation restrictions list.</value>
        [DataMember]
        public List<ContractExploitationRestrictions> ContractExploitationRestrictionsList { get; set; }

        /// <summary>
        /// Gets or sets the work flow identifier.
        /// </summary>
        /// <value>The work flow identifier.</value>
        [DataMember(IsRequired = true)]
        public Guid WorkFlowIdentifier { get; set; }


        /// <summary>
        /// Gets or sets the split deal contracts.
        /// </summary>
        /// <value>The split deal contracts.</value>
        [DataMember]
        public List<ContractInfo> SplitDealContracts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is notification required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is notification required; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsNotificationRequired { get; set; }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>The notification.</value>
        [DataMember]
        public string Notification { get; set; }


        /// <summary>
        /// Gets or sets the pc notice company id.
        /// </summary>
        /// <value>The pc notice company id.</value>
        [DataMember]
        private long? PcNoticeCompanyId { get; set; }




        /// <summary>
        /// Gets or sets the lost rights reason.
        /// </summary>
        /// <value>The lost rights reason.</value>
        [DataMember]
        private string LostRightsReason { get; set; }

        /// <summary>
        /// Gets or sets the clearance company country info.
        /// </summary>
        /// <value>The clearance company country info.</value>
        [DataMember]
        private CompanyInfo ClearanceCompanyCountryInfo { get; set; }

        /// <summary>
        /// Gets or sets the Contract Description Id .
        /// </summary>
        /// <value>The Contract Description Id.</value>
        // Contract Description
        [DataMember]
        private long ContractDescriptionId { get; set; }

        // Repertoire Details
        /// <summary>
        /// Gets or sets the pc notice country id.
        /// </summary>
        /// <value>The pc notice country id.</value>
        [DataMember]
        private long? PcNoticeCountryId { get; set; }


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

    public partial class ContractDetails
    {

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>IsDigitalUnbundle.</value>
        [DataMember]
        public bool? IsDigitalUnbundle { get; set; }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>ResourceId.</value>
        [DataMember]
        public long ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>ResourceIdsArray.</value>
        [DataMember]
        public string ResourceIdsArray { get; set; }

        /// <summary>
        /// Gets or sets the Contract PreClearanceInformation List
        /// </summary>
        /// <value>The Contract PreClearanceInformation List.</value>
        [DataMember]
        public List<ContractPreClearanceInformation> ContractPreClearanceInformation { get; set; }

        /// <summary>
        /// Gets or sets the Contract Digital Restrictions List
        /// </summary>
        /// <value>The Contract Digital Restrictions List.</value>
        [DataMember]
        public List<ContractDigitalRestrictions> ContractDigitalRestrictions { get; set; }


        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>IsPhysicalRights.</value>
        [DataMember]
        public bool? IsPhysicalRights { get; set; }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>IsDigitalRights.</value>
        [DataMember]
        public bool? IsDigitalRights { get; set; }


    }
}