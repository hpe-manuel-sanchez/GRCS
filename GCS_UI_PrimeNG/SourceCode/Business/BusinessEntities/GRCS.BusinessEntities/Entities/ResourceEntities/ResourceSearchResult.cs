/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceSearchResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 03/10/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
         
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for ResourceSearchResult Details
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    [DataContract]
    public class ResourceSearchResult : EntityInformation
    {
        /// <summary>
        /// Gets or sets the r2 rows retrieved.
        /// </summary>
        /// <value>The r2 rows retrieved.</value>
        [DataMember]
        public int R2RowsRetrieved { get; set; }

        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>The row count.</value>
        [DataMember]
        public int RowCount { get; set; }

        /// <summary>
        /// Gets or sets the index of the row.
        /// </summary>
        /// <value>The index of the row.</value>
        [DataMember]
        public long RowIndex { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [DataMember]
        public List<ResourceInfo> Values { get; set; }

        /// <summary>
        /// Gets or sets the column identifier.
        /// </summary>
        /// <value>The column identifier.</value>
        [DataMember]
        public long ColumnIdentifier { get; set; }

        
    }
}
