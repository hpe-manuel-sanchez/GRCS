using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public class RoutingVariationSaveInfo
    {
         [DataMember]
        public List<BaseTerritory>  Territories { get; set; }

        [DataMember]
         public List<BaseCompany> Companies { get; set; }

        [DataMember]
        public List<ArtistInfo> ArtistList { get; set; }

        [DataMember]
        public RoutingVariations RoutingVariation { get; set; }
    }
}
