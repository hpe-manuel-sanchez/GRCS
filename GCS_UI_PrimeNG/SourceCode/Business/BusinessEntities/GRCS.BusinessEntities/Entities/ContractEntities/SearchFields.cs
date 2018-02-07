using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// This class is to keep the search Parameters in session.
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchFields
    {
        /// <summary>
        /// Gets or sets the search criteria.
        /// </summary>
        /// <value>The search criteria.</value>
        [DataMember]
        public ContractDetails SearchCriteria { get; set; }

        /// <summary>
        /// Gets or sets the filter criteria.
        /// </summary>
        /// <value>The filter criteria.</value>
        [DataMember]
        public FilterFields FilterCriteria { get; set; }
    }
}
