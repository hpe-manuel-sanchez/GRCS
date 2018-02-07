using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    public class UserTransfer : ClassInfo
    {
        [DataMember]
        public long touserId { get; set; }

        [DataMember]
        public string To { get; set; }

    }
}
