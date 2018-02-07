using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public class RoutingRuleVariation:RoutingRule
    {
        [DataMember]
        public List<RoutingVariations> RoutingVariation { get; set; }


    }
}
