using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    public class ProjectSearchResult
    {
        [DataMember]
        public int TotalRows { get; set; }

        [DataMember]
        public List<ClearanceProject> Values { get; set; }
    }
}
