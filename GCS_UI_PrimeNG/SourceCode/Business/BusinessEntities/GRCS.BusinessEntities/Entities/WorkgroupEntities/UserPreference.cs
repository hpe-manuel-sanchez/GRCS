using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class UserPreference
    {
        [DataMember]
        public long UserPrefernceID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public long? PreferenceID { get; set; }
        [DataMember]
        public List<UserPreferenceValues> UserPreferenceValuesList { get; set; }
        [DataMember]
        public string WorkGroupValues { get; set; }
    }
}
