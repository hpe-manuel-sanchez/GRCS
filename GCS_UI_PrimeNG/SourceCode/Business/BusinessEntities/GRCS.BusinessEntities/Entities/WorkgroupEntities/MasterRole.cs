using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract(Name = "MasterRole")]
	[Flags]
    public enum MasterRole
    {
        [EnumMember]
        Requestor = 01,
        [EnumMember]
        Reviewer = 02        
    }
}
