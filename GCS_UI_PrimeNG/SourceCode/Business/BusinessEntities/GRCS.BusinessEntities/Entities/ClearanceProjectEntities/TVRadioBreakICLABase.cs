/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TVRadioBreakICLABase.cs 
 * Project Code : UMG-GRCS 
 * Author       : sarika tyagi
 * Created on   : 10-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class TVRadioBreakICLABase
    {
        /// <summary>
        /// Budget
        /// </summary>
        [DataMember]
        public decimal? Budget { get; set; }

        /// <summary>
        /// BudgetInUSD
        /// </summary>
        [DataMember]
        public decimal? BudgetInUSD { get; set; }

        /// <summary>
        /// ProductionCostOfCommercial
        /// </summary>
        [DataMember]
        public decimal? ProductionCostOfCommercial { get; set; }
    }
}
