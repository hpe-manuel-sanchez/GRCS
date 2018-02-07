/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : RepertoireRightsSearchResult.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : Ramesh J
 * Created on   : 18/02/2013 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for RepertoireRightsSearchResult
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;


namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// Repertoire Rights Search Result
    /// </summary>
    [DataContract]
    public class ReportoireDigitalRestriction : EntityInformation
    {
        /// <summary>
        /// Gets or sets the right set ID.
        /// </summary>
        /// <value>The right set ID.</value>
        [DataMember]
        public int RightsSetId { get; set; }

        /// <summary>
        /// Gets or sets the type of the use.
        /// </summary>
        /// <value>The type of the use.</value>
        [DataMember]
        public string UseType { get; set; }

        /// <summary>
        /// Gets or sets the commercial model.
        /// </summary>
        /// <value>The commercial model.</value>
        [DataMember]
        public string CommercialModel { get; set; }

        /// <summary>
        /// Gets or sets the restrictions.
        /// </summary>
        /// <value>The restrictions.</value>
        [DataMember]
        public string Restrictions { get; set; }

        /// <summary>
        /// Gets or sets the consent period.
        /// </summary>
        /// <value>The consent period.</value>
        [DataMember]
        public string ConsentPeriod { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [DataMember]
        public string Notes { get; set; }

        public int MergeCount { get; set; }
       
    }
}
