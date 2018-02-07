using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities
{
    [DataContract]
    public class ClearanceResourceSearchResult : EntityInformation
    {
        /// <summary>
        /// Clearance Resource Search Result
        /// </summary>
        public ClearanceResourceSearchResult()
        {
            lstClearanceResource = new List<ClearanceResource>();
        }
        /// <summary>
        /// Resource Info List
        /// </summary>
        [DataMember]
        public List<ClearanceResource> lstClearanceResource { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        [DataMember]
        public FilterFields Criteria { get; set; }

        /// <summary>
        /// Gets or sets the rows retreived.
        /// </summary>
        /// <value>The rows retreived.</value>
        [DataMember]
        public long RowsRetreived { get; set; }

        /// <summary>
        /// Primary key of the interface log table
        /// </summary>
        [DataMember]
        public long RowIndex { get; set; }
        /// <summary>
        /// Gets or sets the r2 rows retrieved.
        /// </summary>
        /// <value>The r2 rows retrieved.</value>
        [DataMember]
        public int R2RowsRetrieved { get; set; }
    }
}
