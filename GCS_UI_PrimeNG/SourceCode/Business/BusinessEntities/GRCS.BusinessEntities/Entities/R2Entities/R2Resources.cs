using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2Resources
    {
        [DataMember]
        public WSResourceSearchServiceProxy.WSResource[] Resources { get; set; }
    }
}