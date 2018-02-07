using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class Preferences
    {
        [DataMember]
        public long PreferenceID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? SequenceNumber { get; set; }

        [DataMember]
        public byte? PreferenceType { get; set; }
    }
}
