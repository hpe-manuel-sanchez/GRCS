using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxAuthorization
    {
        [DataMember]
        public long RoleId { get; set; }

        [DataMember]
        public Dictionary<string, string> RequestDetailColumnsVisibility { get; set; }

        [DataMember]
        public bool ShowManageContract { get; set; }
    }
}