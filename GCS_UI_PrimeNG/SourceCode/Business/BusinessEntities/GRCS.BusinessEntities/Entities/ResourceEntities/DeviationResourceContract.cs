using System.Runtime.Serialization;
using System;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    [DataContract]
    public class DeviationResourceContract : ArtistInfo
    {
        [DataMember]
        public long ContractId { get; set; }

        [DataMember]
        public string ResourceTitle { get; set; }

        [DataMember]
        public DateTime  ModifiedDateTime { get; set; }

        [DataMember]
        public long WorkgroupId { get; set; }

        [DataMember]
        public long? ResourceId { get; set; }
    }
}
