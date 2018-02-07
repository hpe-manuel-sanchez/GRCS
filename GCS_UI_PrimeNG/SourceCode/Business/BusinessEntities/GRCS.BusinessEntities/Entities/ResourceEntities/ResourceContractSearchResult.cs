using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    [DataContract]
    [Serializable]
    public class ResourceContractSearchResult
    {
        /// <summary>
        /// Resource Contracts
        /// </summary>
        [DataMember]
        public List<ResourceContract> ResourceContracts { get; set; }

        /// <summary>
        /// TotalRows
        /// </summary>
        [DataMember]
        public int TotalRows { get; set; }

        /// <summary>
        /// RowIndex
        /// </summary>
        [DataMember]
        public long RowIndex { get; set; }
    }
}
