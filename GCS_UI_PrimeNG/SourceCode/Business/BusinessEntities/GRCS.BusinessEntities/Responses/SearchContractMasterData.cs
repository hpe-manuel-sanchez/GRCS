using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    [DataContract]
    public class SearchContractMasterData
    {
        /// <summary>
        /// Gets or sets the contract status.
        /// </summary>
        /// <value>The contract status.</value>
        [DataMember]
        public List<StringIdentifier> ContractStatus { get; set; }

        /// <summary>
        /// Gets or sets the workflow status.
        /// </summary>
        /// <value>The workflow status.</value>
        [DataMember]
        public List<StringIdentifier> WorkflowStatus { get; set; }

        /// <summary>
        /// Gets or sets the right types.
        /// </summary>
        /// <value>The right types.</value>
        [DataMember]
        public List<StringIdentifier> RightTypes { get; set; }
    }
}
