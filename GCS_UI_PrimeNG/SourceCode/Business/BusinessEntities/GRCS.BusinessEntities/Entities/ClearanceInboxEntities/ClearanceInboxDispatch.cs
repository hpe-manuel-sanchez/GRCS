using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxDispatch
    {
        [DataMember]
        public long WorkgroupId { get; set; }

        [DataMember]
        public string WorkgroupName { get; set; }

        [DataMember]
        public IEnumerable<string> WorkgroupPrimaryUser { get; set; }
    }
}