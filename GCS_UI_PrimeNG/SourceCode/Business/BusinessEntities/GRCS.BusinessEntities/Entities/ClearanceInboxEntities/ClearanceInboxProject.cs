using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxProject
    {
        [DataMember]
        public long CurrentFolderId { get; set; }

        [DataMember]
        public long OriginalSystemFolderId { get; set; }
        
        [DataMember]
        public long? ToFolderId { get; set; }

        [DataMember]
        public long ClearanceProjectId { get; set; }

        [DataMember]
        public string ProjectTitle { get; set; }

        [DataMember]
        public string ProjectType { get; set; }

        [DataMember]
        public long ProjectTypeId { get; set; }

        [DataMember]
        public string ProjectDetail { get; set; }

        [DataMember]
        public string ProjectReferenceNumber { get; set; }

        [DataMember]
        public long ProjectStatusId { get; set; }

        [DataMember]
        public long? EstimatedSalesUnit { get; set; }
                
        [DataMember]
        public DateTime? ReleaseDate { get; set; }

        [DataMember]
        public DateTime ProjectSubmissionDate { get; set; }

        [DataMember]
        public DateTime? NotificationRecieved { get; set; }

        [DataMember]
        public long? RccHandlerId { get; set; }

        [DataMember]
        public string RccHandlerName { get; set; }

        [DataMember]
        public string RequestorName { get; set; }

        [DataMember]
        public string RequestingCompanyName { get; set; }

        [DataMember]
        public string RequestingCompanyIsoName { get; set; }

        [DataMember]
        public string ThirdPartyCompanyName { get; set; }

        [DataMember]
        public string ThirdPartyCompanyIsoName { get; set; }

        [DataMember]
        public bool IsUnread { get; set; }

        [DataMember]
        public bool IsThirdParty { get; set; }

        [DataMember]
        public bool IsAllRequestReviewed { get; set; }

        [DataMember]
        public long? AssignedToUserId { get; set; }

        [DataMember]
        public string AssignedToUser { get; set; }

        [DataMember]
        public long RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public long WorkgroupId { get; set; }

        [DataMember]
        public string WorkGroupName { get; set; }

        [DataMember]
        public long TotalRecordCount { get; set; }

        [DataMember]
        public string CreatedByUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public string ModifiedUserAssignedTo { get; set; }

        [DataMember]
        public DateTime? ModifiedDateAssignedTo { get; set; }
    }
}