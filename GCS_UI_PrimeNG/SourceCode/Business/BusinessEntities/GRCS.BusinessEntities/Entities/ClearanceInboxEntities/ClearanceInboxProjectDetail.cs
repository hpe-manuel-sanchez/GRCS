using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxProjectDetail
    {
      
        [DataMember]
        public string ProjectReferenceNumber { get; set; }

        [DataMember]
        public List<string> Upc { get; set; }

        [DataMember]
        public List<ListItem> RccHandler { get; set; }

        [DataMember]
        public string StatusDesc { get; set; }

        [DataMember]
        public List<ListItem> AvailableActions { get; set; }

        [DataMember]
        public List<ClearanceInboxRequest> Requests { get; set; }

        [DataMember]
        public string ProjectDetail { get; set; }

        [DataMember]
        public int ProjectType { get; set; }

        [DataMember]
        public int ProjectStatus { get; set; }

        [DataMember]
        public long clrProjectId { get; set; }

        [DataMember]
        public long ReminderCount { get; set; }

        [DataMember]
        public long NotificationCount { get; set; }

        [DataMember]
        public byte RoutingStatus { get; set; }

        [DataMember]
        public DateTime ProjectModifiedDate { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public long FolderId { get; set; }
    }
}