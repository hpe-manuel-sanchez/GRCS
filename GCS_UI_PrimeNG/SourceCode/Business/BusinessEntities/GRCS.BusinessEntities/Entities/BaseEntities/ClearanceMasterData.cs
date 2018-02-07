using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    public class ClearanceMasterData
    {

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long Value { get; set; }

        [DataMember]
        public string Type { get; set; }
    }
}
