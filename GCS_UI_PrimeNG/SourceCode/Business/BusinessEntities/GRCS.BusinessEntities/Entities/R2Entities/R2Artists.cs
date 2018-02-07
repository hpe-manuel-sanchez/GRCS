using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2Artists
    {
        [DataMember]
        public WSPeopleSearchServiceProxy.WSPeople[] Artists { get; set; }
    }
}