using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    [DataContract]
    [Serializable]
    public class ArtistContractSearchResult
    {
        /// <summary>
        /// Artist Contracts
        /// </summary>
        [DataMember]
        public List<ArtistContract> ArtistContracts { get; set; }

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
