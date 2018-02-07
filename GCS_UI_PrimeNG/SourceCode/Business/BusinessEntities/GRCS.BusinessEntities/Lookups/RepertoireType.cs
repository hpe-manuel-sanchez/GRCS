
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Lookups
{
    [DataContract]
    public enum RepertoireType
    {
        [EnumMember]
        Select = 0,
        [EnumMember]
        Contract = 1,
        [EnumMember]
        Release = 2,
        [EnumMember]
        Resource = 3,
        [EnumMember]
        Track = 4,
        [EnumMember]
        Project = 5
    }
}
