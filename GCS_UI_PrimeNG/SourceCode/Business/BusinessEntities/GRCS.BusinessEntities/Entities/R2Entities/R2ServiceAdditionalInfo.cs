using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2ServiceAdditionalInfo : EntityInformation
    {
        [DataMember]
        public List<ArtistInfo> Artists;
    }


}
