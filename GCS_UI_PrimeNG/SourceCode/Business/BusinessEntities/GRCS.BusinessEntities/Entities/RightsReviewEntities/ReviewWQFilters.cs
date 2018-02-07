/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReviewWQFilters.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Review WQ Filters details
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Review WQ Filters Details
    /// </summary>
    [DataContract]
    public class ReviewWQFilters : FilterCriteria
    {
        public ReviewWQFilters()
        {
            Status = ReviewStatusType.NewForReview | ReviewStatusType.FinalForReview ;
            Filters = new List<Filter>();      
        }
        /// <summary>
        /// Gets or sets the type of the repertoire.
        /// </summary>
        /// <value>The type of the repertoire.</value>
        [DataMember]
        public RepertoireType RepertoireType
        {
            get;
            set;
        }
               
        /// <summary>
        /// Gets or sets the type of the Resources.
        /// </summary>
        /// <value>The type of the Resources.</value>
        [DataMember]
        public ReviewStatusType Status
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>The filters.</value>
        [DataMember]
        public List<Filter> Filters
        {
            get;
            set;
        }

        [DataMember]
        public bool? IsMultiArtist { get; set; }

        
        [DataMember]
        public bool? IsOst { get; set; }

        [DataMember]
        public int ReviewReason { get; set; }



        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>The filters.</value>
        [DataMember]
        public ColumnSetting Columns
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the is exact search.
        /// </summary>
        /// <value>The is exact search.</value>
        [DataMember]
        public bool IsExactSearch
        {
            get;
            set;
        }
    }
}
