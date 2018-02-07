using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    public class FilterCriteria 
    {
        [DataMember]
        public int jtStartIndex { get; set; }

        [DataMember]
        public int jtPageSize { get; set; }

        [DataMember]
        public string SortField { get; set; }

        [DataMember]
        public string FieldId { get; set; }


        [DataMember]
        public bool IsAscendingOrder { get; set; }
    }
}
