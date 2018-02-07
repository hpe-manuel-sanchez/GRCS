/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : SearchCriteria.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : Rikhu Prasad
 * Created on   : 17/12/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for SearchCriteria
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// Repertoire Search
    /// </summary>
    [DataContract]
    [Serializable]
    public class SearchRepertoireCriteria : EntityInformation
    {
        public SearchRepertoireCriteria()
        {
            SearchResourceReleaseCriteria = new SearchResourceReleaseCriteria();
            SearchRightsCriteria = new SearchRightsCriteria();
            FilterCriteria = new FilterFields();
        }
        /// <summary>
        /// Gets or sets the search resource release criteria.
        /// </summary>
        /// <value>The search resource release criteria.</value>
        [DataMember]
        public SearchResourceReleaseCriteria SearchResourceReleaseCriteria { get; set; }

        /// <summary>
        /// Gets or sets the search rights criteria.
        /// </summary>
        /// <value>The search rights criteria.</value>
        [DataMember]
        public SearchRightsCriteria SearchRightsCriteria { get; set; }

        /// <summary>
        /// Gets or sets the search pre cleared criteria.
        /// </summary>
        /// <value>The search pre cleared criteria.</value>
        [DataMember]
        public List<PreClearance> PreClearanceList { get; set; }

        /// <summary>
        /// Gets or sets the search exploitation criteria.
        /// </summary>
        /// <value>The search exploitation criteria.</value>
        [DataMember]
        public SearchExploitationCriteria SearchExploitationCriteria { get; set; }

        /// <summary>
        /// Gets or sets the filter criteria.
        /// </summary>
        /// <value>The filter criteria.</value>
        [DataMember]
        public FilterFields FilterCriteria { get; set; }

        /// <summary>
        /// ExploitationsList
        /// </summary>
        [DataMember]
        public List<Exploitations> ExploitationsList { get; set; }

        /// <summary>
        /// Gets or sets the digital restriction list.
        /// </summary>
        /// <value>The digital restriction list.</value>
        [DataMember]
        public List<ContractDigitalRestrictions> DigitalRestrictionList { get; set; }

        /// <summary>
        /// Gets or sets the Admin Companies Ids List for sensitive user
        /// </summary>
        [DataMember]
        public string AdminCompaniesList { get; set; }
    }
}
