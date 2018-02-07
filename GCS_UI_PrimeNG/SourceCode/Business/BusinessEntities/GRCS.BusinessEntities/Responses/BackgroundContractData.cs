using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    [DataContract]
    public class BackgroundContractData
    {
        [DataMember]
        public long TotalCount { get; set; }
        [DataMember]
        public long ContractId { get; set; }
        [DataMember]
        public string ItemType { get; set; }
        [DataMember]
        public long IntCount { get; set; }
        [DataMember]
        public long Repertoire { get; set; }
        [DataMember]
        public long Processed { get; set; }
        [DataMember]
        public long Processing { get; set; }
        [DataMember]
        public long UnProcessed { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string modifieddatetime { get; set; }

        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int NoOfRetry { get; set; }
        [DataMember]
        public string Trait { get; set; }
        [DataMember]
        public string ProjectIdUPCISRC { get; set; } 
        [DataMember]
        public string VersionTitle { get; set; }
        [DataMember]
        public string ARTIST { get; set; }

    }
}
