using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [Serializable]
    public class RoutingVariationOutput
    {

        #region "Routing variation output"

        public long RoutingRuleID { get; set; }

        public string RuleName { get; set; }

        public long RoutingVariationID { get; set; }

        public bool CanSkipNational { get; set; }

        public bool CanSkipInternational { get; set; }

        public bool CanSkipLocallabel { get; set; }

        public long SkippedNationalMarketingWorkgroupId { get; set; }

        public long SkippedInterNationalMarketingWorkgroupId { get; set; }

        public long SkippedLocalLabelWorkgroupId { get; set; }

        public bool CanSendEmail { get; set; }

        #endregion
    }
}
