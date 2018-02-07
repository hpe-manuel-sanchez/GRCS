using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2RepertoireResult
    {
        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string ErrorReason { get; set; }
    }
}
