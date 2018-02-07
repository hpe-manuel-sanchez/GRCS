using System.Runtime.Serialization;
using System;
namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    [DataContract]
    public class DeviationArtistContract : ArtistInfo
    {
        [DataMember]
        public long ContractId { get; set; }

        [DataMember]
        public DateTime ModifiedDateTime { get; set; }
    }
}
