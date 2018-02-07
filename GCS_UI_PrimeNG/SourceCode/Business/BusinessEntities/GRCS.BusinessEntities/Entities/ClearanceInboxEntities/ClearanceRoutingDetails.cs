using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceRoutingDetails
    {
        [DataMember]
        public string ResourceTitle { get; set; }

        [DataMember]
        public string Workgroup { get; set; }

        [DataMember]
        public string WorkgroupRole { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public DateTime? ReviewDate { get; set; }

        [DataMember]
        public string Comments { get; set; }
    }
}
