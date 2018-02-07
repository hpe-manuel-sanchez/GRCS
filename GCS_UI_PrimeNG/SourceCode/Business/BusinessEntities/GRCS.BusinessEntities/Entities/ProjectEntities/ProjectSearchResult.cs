/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ProjectSearchResult.cs
 * Project Code :   
 * Author       : Ajitha R
 * Created on   : 25/09/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for ProjectSearchResult.
                  
******************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    [DataContract]
   public  class ProjectSearchResult : EntityInformation 
    {
        /// <summary>
        /// Gets or sets the r2 rows retrieved.
        /// </summary>
        /// <value>The r2 rows retrieved.</value>
        [DataMember]
        public long R2RowsRetrieved { get; set; }

        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>The row count.</value>
        [DataMember]
        public int RowCount { get; set; }


        /// <summary>
        /// Gets or sets the index of the row Interface Log table.
        /// </summary>
        /// <value>The index of the row.</value>
        [DataMember]
        public long RowIndex { get; set; }
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
         [DataMember]
        public List<ProjectInfo> Values { get; set; }

         /// <summary>
         /// Gets or sets the column identifier.
         /// </summary>
         /// <value>The column identifier.</value>
         [DataMember]
         public long ColumnIdentifier { get; set; }
    }
}
