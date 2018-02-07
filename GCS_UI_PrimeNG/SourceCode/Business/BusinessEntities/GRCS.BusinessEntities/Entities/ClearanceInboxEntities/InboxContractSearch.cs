using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
     [DataContract]
    public class InboxContractSearch
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractSearchResult"/> class.
        /// </summary>
        public InboxContractSearch()
        {
            ContractSearchInfo = new List<ContractDetails>();
        }

        /// <summary>
        /// ContractSearchInfo
        /// </summary>
        [DataMember]
        public List<ContractDetails> ContractSearchInfo { get; set; }

        /// <summary>
        /// ContractsCount
        /// </summary>
        [DataMember]
        public long ContractsCount { get; set; }

        public long ResourceId { get; set; }

        public string ResourceIds { get; set; }   

        public string contractId { get; set; }

        public string RoutedItemId { get; set; }

        public string RoutedItemIds { get; set; }

    }
}
