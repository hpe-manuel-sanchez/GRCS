using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    public class SearchCriteria
    {
        [DataMember]
        public string term { get; set; }
        [DataMember]
        public string searchBy { get; set; }

    }
}
