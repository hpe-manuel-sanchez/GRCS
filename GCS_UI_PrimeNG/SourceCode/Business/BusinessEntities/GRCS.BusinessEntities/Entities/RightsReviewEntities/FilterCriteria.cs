/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : FilterCriteria.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 12-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Filter Criteria                   
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Filter Criteria 
    /// </summary>
    [DataContract]
    public class FilterCriteria
    {
        /// <summary>
        /// Gets or sets the start index.
        /// </summary>
        /// <value>The start index.</value>
        [DataMember]
        public int StartIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        [DataMember]
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sort field.
        /// </summary>
        /// <value>The sort field.</value>
        [DataMember]
        public string SortField
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the filter id.
        /// </summary>
        /// <value>The filter id.</value>
        [DataMember]
        public string FilterId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating 
        /// whether this instance is ascending order.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance 
        /// 	is ascending order; 
        /// 	otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsAscendingOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total rows.
        /// </summary>
        /// <value>The total rows.</value>
        [DataMember]
        public long TotalRows
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column identifier.
        /// </summary>
        /// <value>The column identifier.</value>
        [DataMember]
        public long ColumnIdentifier
        {
            get;
            set;
        }
    }
}
