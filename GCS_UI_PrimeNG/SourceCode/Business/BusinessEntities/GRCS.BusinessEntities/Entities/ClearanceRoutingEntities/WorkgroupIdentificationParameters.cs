using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public class WorkgroupIdentificationParameters
    {
        [DataMember]
        public RoleType RoleTypeId { get; set; }
        [DataMember]
        public long ClearanceAdminCompanyId { get; set; }
        [DataMember]
        public long CountryId { get; set; }
        [DataMember]
        public long ArtistContractId { get; set; }
        [DataMember]
        public long ResourceContractId { get; set; }
        [DataMember]
        public List<long> ProjectRequestTypeId { get; set; }
        [DataMember]
        public short ResourceRequestTypeId { get; set; }
        [DataMember]
        public long ResourceId { get; set; } 

      
    }
}
