using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    [DataContract]
    public class WorkQueueResult : EntityInformation
    {
        [DataMember]
        public List<WorkQueue> WorkQueueItems { get; set; }


        [DataMember]
        public int TotalRows { get; set; }
    }
}
