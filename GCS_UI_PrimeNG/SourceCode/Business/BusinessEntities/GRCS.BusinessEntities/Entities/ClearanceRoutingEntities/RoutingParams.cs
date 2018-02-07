using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    public class RoutingParams
    {
        public List<long> ThridPartyCompanyIds { get; set; }
        public List<long> ExcludedCountryIds { get; set; }
        public List<long> ExcludedComapnyIds { get; set; }
        
        
    }
}
