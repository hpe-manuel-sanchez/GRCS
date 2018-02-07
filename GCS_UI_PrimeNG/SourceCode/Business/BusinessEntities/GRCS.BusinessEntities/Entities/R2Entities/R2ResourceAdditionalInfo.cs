using System.Runtime.Serialization;
namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2ResourceAdditionalInfo : R2ServiceAdditionalInfo
    {

        [DataMember]
        public bool IsMediaPortal;
        [DataMember]
        public string MusicTime;
        [DataMember]
        public bool HasSideArtist;
    }


}
