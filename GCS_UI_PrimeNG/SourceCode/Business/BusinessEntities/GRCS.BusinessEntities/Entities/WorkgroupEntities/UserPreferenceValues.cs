using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class UserPreferenceValues
    {
        [DataMember]
        public long UserPreferenceValuesID { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
