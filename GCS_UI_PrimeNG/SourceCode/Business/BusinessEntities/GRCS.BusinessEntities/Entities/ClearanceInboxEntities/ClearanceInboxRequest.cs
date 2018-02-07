using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceInboxRequest
    {
        [DataMember]
        public long ResourceId { get; set; }

        [DataMember]
        public long ActionId { get; set; }

        [DataMember]
        public long ClearanceProjectId { get; set; }

        [DataMember]
        public long? ReleaseId { get; set; }

        [DataMember]
        public long? PackageId { get; set; }

        [DataMember]
        public long RequestTypeId { get; set; }

        [DataMember]
        public long RoutedItemId { get; set; }

        [DataMember]
        public long RequestId { get; set; }

        [DataMember]
        public Dictionary<string, string> KeyRoutedItemRequest { get; set; }

        [DataMember]
        public long? ResourceTypeId { get; set; }

        [DataMember]
        public string ResourceType { get; set; }

        [DataMember]
        public string ResourceTitle { get; set; }

        [DataMember]
        public long ArtistId { get; set; }

        [DataMember]
        public string PrimaryArtistName { get; set; }

        [DataMember]
        public string VersionTitle { get; set; }

        [DataMember]
        public string Isrc { get; set; }

        [DataMember]
        public string Upc { get; set; }

        [DataMember]
        public long ClearanceProjectResourceId { get; set; }

        [DataMember]
        public string RequestType { get; set; }

        [DataMember]
        public long? RequestStatusId { get; set; }

        [DataMember]
        public string RequestStatus { get; set; }

        [DataMember]
        public long? ApprovalStatusId { get; set; }

        [DataMember]
        public string ApprovalStatus { get; set; }

        [DataMember]
        public long? ConfigurationId { get; set; }

        [DataMember]
        public string Configuration { get; set; }

        [DataMember]
        public DateTime AvailableDate { get; set; }

        [DataMember]
        public string LastRoutingComment { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public long CommentCount { get; set; }

        [DataMember]
        public bool IsOpen { get; set; }

        [DataMember]
        public bool IsReinstated { get; set; }

        [DataMember]
        public bool IsDisabled { get; set; }

        [DataMember]
        public bool IsNonUMGAndNonExclusive { get; set; }

        [DataMember]
        public bool IsExistingReleaseRequest { get; set; }

        [DataMember]
        public bool UndoStatus { get; set; }

        [DataMember]
        public string ArchiveFlag { get; set; }

        [DataMember]
        public long? AssignedToUserId { get; set; }

        [DataMember]
        public string AssignedToUser { get; set; }

        [DataMember]
        public long? WorkgroupId { get; set; }

        [DataMember]
        public string WorkgroupName { get; set; }

        [DataMember]
        public long? RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public long? AdminCompanyId { get; set; }

        [DataMember]
        public string AdminCompany { get; set; }

        [DataMember]
        public long RightId { get; set; }

        [DataMember]
        public string RightsType_Desc { get; set; }

        [DataMember]
        public long SalesChannelId { get; set; }

        [DataMember]
        public DateTime? ReviewDate { get; set; }

        [DataMember]
        public long RejectReasonId { get; set; }

        [DataMember]
        public long? SequenceNo { get; set; }

        [DataMember]
        public string CreatedByUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public long? ContractId { get; set; }

        [DataMember]
        public string IsoCode { get; set; }

        [DataMember]
        public List<string> ContractSummary { get; set; }

        [DataMember]
        public string ContractArtistName { get; set; }

        [DataMember]
        public List<long> ContractIds { get; set; }

        [DataMember]
        public List<ListItem> RequestTypes { get; set; }

        [DataMember]
        public bool IsRoutingTriggered { get; set; }

        [DataMember]
        public bool IsRoutingStop { get; set; }

        [DataMember]
        public Byte RoutingCalculationStatus { get; set; }

        [DataMember]
        public long TotalRecordCount { get; set; }

        [DataMember]
        public DateTime ModifiedDateRequest { get; set; }

        [DataMember]
        public string ModifiedDateRequestString { get; set; }
        [DataMember]
        public string ModifiedDateRoutedString { get; set; }

        [DataMember]
        public string AssignedToUserName { get; set; }
    }
}
