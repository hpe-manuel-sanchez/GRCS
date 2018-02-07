using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public class RoutingRule
    {
        [DataMember]
        public Int64 RuleNumber { get; set; }

        [DataMember]
        public string RuleName { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

		[DataMember]
		public DateTime ModifiedDateTime { get; set; }

        [DataMember]
        public string LoginName { get; set; }
    }
}
