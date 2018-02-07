/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Identifier.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vijay Venkatesh Prasad . N
 * Created on   : 31-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      Defines the Search Criteria for Get Companies

****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Search Criteria for GetCompanies
    /// </summary>
    [DataContract]
    public class CompanySearch : EntityInformation
    {
        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        /// <value>The search term.</value>
        [DataMember]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance need to be filtered by User based companies.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filter required; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsFilterRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is country required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is country required; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsCountryRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has page details.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has page details; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasPageDetails { get; set; }

        /// <summary>
        /// Gets or sets the page details.
        /// </summary>
        /// <value>The page details.</value>
        [DataMember]
        public Page PageDetails { get; set; }

        [DataMember]
        public GrsTasks Tasks { get; set; }
    }
}
