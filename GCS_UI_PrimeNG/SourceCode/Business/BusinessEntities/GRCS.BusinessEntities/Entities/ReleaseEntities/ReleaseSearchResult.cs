/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ReleaseSearchResult.cs
 * Project Code :   
 *Author       : vijayakumar R
 * Created on   : 03/10/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for ReleaseSearchResult.
                  
******************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
    [DataContract]
    public class ReleaseSearchResult : EntityInformation
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
        public List<ReleaseInfo> Values { get; set; }

        /// <summary>
        /// Gets or sets the column identifier.
        /// </summary>
        /// <value>The column identifier.</value>
        [DataMember]
        public long ColumnIdentifier { get; set; }


        /// <summary>
        /// List of all Label corresponding to the Search Result. 
        /// </summary>
        [DataMember]
        public List<ClearanceMasterData> Labels { get; set; }

        /// <summary>
        /// List of all MusicClassType corresponding to the Search Result.
        /// </summary>
        [DataMember]
        public List<ClearanceMasterData> MusicClassTypes { get; set; }   

    }
}
