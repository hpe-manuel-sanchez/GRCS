using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxRequestAction
    {
        [DataMember]
        public long WorkgroupId { get; set; }

        [DataMember]
        public long FolderId { get; set; }

        [DataMember]
        public long ProjectId { get; set; }

        [DataMember]
        public byte ProjectType { get; set; }

        [DataMember]
        public List<ClearanceInboxRequest> Requests { get; set; }

        [DataMember]
        public long ActionId { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public long ToWorkgroupId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public DateTime ProjectModifiedDate { get; set; }

        [DataMember]
        public string ProjectModifiedDateString { get; set; }
    }
}