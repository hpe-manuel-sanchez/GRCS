using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2Projects
    {
        [DataMember]
        public WSProjectSearchServiceProxy.WSProject[] Projects { get; set; }
    }
}