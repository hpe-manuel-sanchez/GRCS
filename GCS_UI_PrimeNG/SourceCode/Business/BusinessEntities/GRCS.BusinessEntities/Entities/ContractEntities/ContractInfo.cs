/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
Pavan Kumar          18/047/2012    To abstract high level contract information
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for ContractInfo
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    [DataContract]
    [Serializable]
    [KnownType(typeof (ContractDetails))]
    public class ContractInfo : BaseSearch
    {
        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the parent contract id.
        /// </summary>
        /// <value>The parent contract id.</value>
        [DataMember]
        public long? ParentContractId { get; set; }

        /// <summary>
        /// Gets or sets the deal flag.
        /// </summary>
        /// <value>The deal flag. "D" for Duplicate; "S" for Split Deal.</value>
        [DataMember]
        public string DealFlag { get; set; }

        /// <summary>
        /// StatusType
        /// </summary>
        [DataMember]
        // [Display(Name = "StatusType", ResourceType = typeof(Entity))]
        public int StatusType { get; set; }

        /// <summary>
        /// Artist Related
        /// </summary>
        /// <value>The artist id.</value>
        [DataMember]
        [ReadOnly(true)]
        public long? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
        [DataMember]
        public string ArtistName { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public long? TalentId { get; set; }
        ///// <summary>
        ///// Gets or sets the contracting party id.
        ///// </summary>
        ///// <value>The contracting party id.</value>
        //[DataMember]
        //public int ContractingPartyId { get; set; }

        /// <summary>
        /// Contracting Parties Related
        /// </summary>
        /// <value>The contracting party.</value>
        [DataMember]
        public string ContractingParty { get; set; }

        /// <summary>
        /// Default value is null
        /// </summary>
        /// <value>The clearance company country id.</value>
        [DataMember]
        public long? ClearanceCompanyCountryId { get; set; }

        /// <summary>
        /// Gets or sets the clearance company id.
        /// </summary>
        /// <value>The clearance company id.</value>
        [DataMember]
        public long? ClearanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the clearance country id.
        /// </summary>
        /// <value>The clearance country id.</value>
        [DataMember]
        public long? ClearanceCountryId { get; set; }

        /// <summary>
        /// Gets or sets the umg signing company id.
        /// </summary>
        /// <value>The umg signing company id.</value>
        [DataMember]
        public long? UmgSigningCompanyId { get; set; }


        /// <summary>
        /// Gets or sets the clearance company country.
        /// </summary>
        /// <value>The clearance company country.</value>
        [DataMember]
        public string ClearanceCompanyCountry { get; set; }

        /// <summary>
        /// Default value of Contract Status id is null
        /// </summary>
        /// <value>The contract status id.</value>
        [DataMember]
        public byte? ContractStatusId { get; set; }

        /// <summary>
        /// Default value of the WorkflowId should be null (Draft).
        /// </summary>
        /// <value>The workflow status id.</value>
        [DataMember]
        public byte? WorkflowStatusId { get; set; }

        /// <summary>
        /// Workflow status of Contract, From Business
        /// </summary>
        /// <value>The workflow status.</value>
        [DataMember]
        public string WorkflowStatus { get; set; }

        /// <summary>
        /// Gets or sets the contract status.
        /// </summary>
        /// <value>The contract status.</value>
        [DataMember]
        public string ContractStatus { get; set; }

        /// <summary>
        /// Gets or sets the contract description.
        /// </summary>
        /// <value>The contract description.</value>
        [DataMember]
        public string ContractDescription { get; set; }

        // Contract Term
        /// <summary>
        /// Gets or sets the contract commencement date.
        /// </summary>
        /// <value>The contract commencement date.</value>
        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? ContractCommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the contract end date.
        /// </summary>
        /// <value>The contract end date.</value>
        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// Gets or sets the contract last change date.
        /// </summary>
        /// <value>The contract last change date.</value>
        [DataMember]
        public DateTime? ContractLastChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the contract creation date.
        /// </summary>
        /// <value>The contract creation date.</value>
        [DataMember]
        public DateTime? ContractCreationDate { get; set; }


        /// <summary>
        /// Gets or sets the local contract ref number.
        /// </summary>
        /// <value>The local contract ref number.</value>
        [DataMember]
        public string LocalContractRefNumber { get; set; }

        /// <summary>
        /// Gets or sets the umg signing company.
        /// </summary>
        /// <value>The umg signing company.</value>
        [DataMember]
        public string UmgSigningCompany { get; set; }

        /// <summary>
        /// Gets or sets the rights type id.
        /// </summary>
        /// <value>The rights type id.</value>
        [DataMember]
        public byte? RightsTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the rights type.
        /// </summary>
        /// <value>The name of the rights type.</value>
        [DataMember]
        public string RightsTypeName { get; set; }
        /// <summary>
        /// AccountId
        /// </summary>
        [DataMember]
        public long? AccountId { get; set; }
        /// <summary>
        /// Gets or sets the artist name in local characters.
        /// </summary>
        /// <value>The artist name in local characters.</value>
        [DataMember]
        public string ArtistNameInLocalCharacters { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        /// <value>The created user.</value>
        [DataMember]
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the modified user.
        /// </summary>
        /// <value>The modified user.</value>
        [DataMember]
        public string ModifiedUser { get; set; }

        /// <summary>
        /// Gets or sets the other rights.
        /// </summary>
        /// <value>The other rights.</value>
        [DataMember]
        public string OtherRights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has parent contract.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has parent contract; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasParentContract { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has child contract.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has child contract; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasChildContract { get; set; }

        /// <summary>
        /// To identify whether the contract has Split deal contracts already available or not.
        /// If true, then only SplitDealContracts will be used for processing.
        /// </summary>
        [DataMember]
        public bool HasSplitDealContracts { get; set; }

        /// <summary>
        /// To identify whether the split deals related to contract got modified or not.
        /// If true, then only split deal contracts will be used for Processing.
        /// </summary>
        [DataMember]
        public bool HasSplitDealsContractsModified { get; set; }

        /// <summary>
        /// The dummy parent id of the contract. Associated as split deal id
        /// </summary>
        /// <value>The dummy parent id. Null if no dummy parent</value>
        [DataMember]
        public long? DummyParentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is work queue.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is work queue; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsWorkQueue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is parent contract search.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is parent contract search; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsParentContractSearch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can edit.
        /// </summary>
        /// <value><c>true</c> if this instance can edit; otherwise, <c>false</c>.</value>
        public bool CanEdit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can delete.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can delete; otherwise, <c>false</c>.
        /// </value>
        public bool CanDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can view.
        /// </summary>
        /// <value><c>true</c> if this instance can view; otherwise, <c>false</c>.</value>
        public bool CanView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can copy.
        /// </summary>
        /// <value><c>true</c> if this instance can copy; otherwise, <c>false</c>.</value>
        public bool CanCopy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can link.
        /// </summary>
        /// <value><c>true</c> if this instance can link; otherwise, <c>false</c>.</value>
        public bool CanLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can link approved.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can link approved; otherwise, <c>false</c>.
        /// </value>
        public bool CanLinkApproved { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance can unlink.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can unlink; otherwise, <c>false</c>.
        /// </value>
        public bool CanUnlink { get; set; }

        /// <summary>
        /// Gets or sets the territorial rights definition.
        /// </summary>
        /// <value>The territorial rights definition.</value>
        [DataMember]
        public string TerritorialRightsDefinition { get; set; }

        [DataMember]
        public bool HasRepertoire { get; set; }

        /// <summary>
        /// Boolean variable to determine the artist search to be performed in a "exact" or "contains" fashion.
        /// True for Exact search; False for otherwise
        /// </summary>
        /// <value>Is Exact Artist Search</value>
        [DataMember]
        public bool IsExactArtistSearch { get; set; }

        /// <summary>
        /// True if Having Artist Info object details else false
        /// </summary>
        [DataMember]
        public bool HasArtistInfo { get; set; }


        public long RightSetId { get; set; }

        /// <summary>
        /// Gets or sets the rights period id.
        /// </summary>
        /// <value>The rights period id.</value>
        [DataMember]
        public byte RightsPeriodId { get; set; }

        [DataMember]
        public List<long> DigitalRestrictionIds { get; set; }

        [DataMember]
        public string WorkFlowInitialStatus { get; set; }

        [DataMember]
        public long? ArtistIntialId { get; set; }

        [DataMember]
        public DateTime? WorkFlwChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the contracting party id.
        /// </summary>
        /// <value>The contracting party id.</value>
        [DataMember]
        public int ContractingPartyId { get; set; }

    }
}
