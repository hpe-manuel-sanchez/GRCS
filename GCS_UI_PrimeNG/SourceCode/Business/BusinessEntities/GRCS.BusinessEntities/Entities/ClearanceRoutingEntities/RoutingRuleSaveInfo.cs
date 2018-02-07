using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public class RoutingRuleSaveInfo
    {
        [DataMember]
        public RoutingRule RoutingRule { get; set; }

        [DataMember]
        public List<RoutingVariationSaveInfo> RoutingVariationSaveInfo { get; set; }

        [DataMember]
        public bool IsSaveAsFlag { get; set; }


    }
}
