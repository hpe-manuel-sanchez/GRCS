/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : SearchPreClearedCriteria.cs
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
 * Description : Defines the entities for SearchPreClearedCriteria
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// PreCleared Search
    /// </summary>
    [DataContract]
    public class SearchPreClearedCriteria : EntityInformation
    {
        /// <summary>
        /// Gets or sets the budget compilation.
        /// </summary>
        /// <value>The budget compilation.</value>
        [DataMember]
        public string BudgetCompilation{ get; set; }

        /// <summary>
        /// Gets or sets the direct marketing.
        /// </summary>
        /// <value>The direct marketing.</value>
        [DataMember]
        public string DirectMarketing{ get; set; }

        /// <summary>
        /// Gets or sets the master sync use.
        /// </summary>
        /// <value>The master sync use.</value>
        [DataMember]
        public string MasterSyncUse{ get; set; }

        /// <summary>
        /// Gets or sets the mid price compilation.
        /// </summary>
        /// <value>The mid price compilation.</value>
        [DataMember]
        public string MidPriceCompilation{ get; set; }

        /// <summary>
        /// Gets or sets the premiums.
        /// </summary>
        /// <value>The premiums.</value>
        [DataMember]
        public string Premiums{ get; set; }

        /// <summary>
        /// Gets or sets the top price compilation.
        /// </summary>
        /// <value>The top price compilation.</value>
        [DataMember]
        public string TopPriceCompilation { get; set; }

    }
}
