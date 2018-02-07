/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ArtistSearchResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for Artist Search Results.                                      
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    [DataContract]
    public class ArtistSearchResult : EntityInformation
    {
        /// <summary>
        /// Artist Search Result
        /// </summary>
        public ArtistSearchResult()
        {
            ArtistDetails = new List<ArtistDetail>();
        }

        /// <summary>
        /// Artist Details
        /// </summary>
        [DataMember]
        public ObservableCollection<ArtistDetail> Details
        {
            get
            {
                var collection = new ObservableCollection<ArtistDetail>();
                foreach (var artistDetail in ArtistDetails)
                {
                    collection.Add(artistDetail);
                }
                return collection;
            }
        }

        /// <summary>
        /// Artist Details
        /// </summary>
        [DataMember]
        public List<ArtistDetail> ArtistDetails { get; set; }

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