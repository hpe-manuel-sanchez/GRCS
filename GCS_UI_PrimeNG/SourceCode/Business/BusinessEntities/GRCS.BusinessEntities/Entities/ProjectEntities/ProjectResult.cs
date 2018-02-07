/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ProjectSearchResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Project Search Results
                  
****************************************************************************/

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    /// <summary>
    /// ProjectSearchResult which is inherited from BaseClass ClassInfo 
    /// </summary>
    public class ProjectResult : EntityInformation
    {
        /// <summary>
        /// Release Search Result
        /// </summary>
        public ProjectResult()
        {
            Details = new ObservableCollection<ProjectDetail>();
        }

        /// <summary>
        /// Release Details
        /// </summary>
        [DataMember]
        public ObservableCollection<ProjectDetail> Details { get; set; }

        /// <summary>
        /// Gridview Display - Current Page Number
        /// </summary>
        [DataMember]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gridview Display - Current Page Size 
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// Page count 
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}