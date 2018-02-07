using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class UserPreferenceInfo
    {
        [DataMember]
        public List<Preferences> PreferenceList { get; set; }
        [DataMember]
        public List<UserPreference> UserPreferenceList { get; set; }
    }
}
