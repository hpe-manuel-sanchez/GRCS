using System.Runtime.Serialization;
using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Lookups
{
    public enum RightsAcquiredType
    {
        [EnumMember]
        [Description("Physical Exploitation Rights")] Physical = 1,
        [EnumMember]
        [Description("Digital Exploitation Rights")] Digital = 2,
        [EnumMember]
        [Description("Mobile Exploitation Rights")] Mobile = 3,
        [EnumMember]
        [Description(" Claim Public Performance & Broadcasting Revenue")] Claim = 4,     

    }
}
