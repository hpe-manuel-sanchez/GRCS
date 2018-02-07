/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : WorkFlowEnum.cs
 * Project Code :   
 * Author       : Pavan Kumar K
 * Created on   : 10 Sep 2012 
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Declare Contract Status Enum

****************************************************************************/

using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Lookups
{

    #region "ContractStatus"

    /// <summary>
    /// Declare All Enum for Contract Status
    /// </summary>
    public enum ContractStatus
    {
        // Table values Look up type Id = 6, CTRCT
        // 1	Signed Contract
        // 2	Deal Memo/Draft Contract
        // 3	Clearance Routing Contract

        #region "Contract Status Enum"

        /// <summary>
        /// Declare Enum for Signed Contract
        /// </summary>
        [Description("Signed Contract")] SignedContract = 1,

        /// <summary>
        /// Declare Enum for Deal Memo / Draft contract
        /// </summary>
        [Description("Deal Memo/Draft Contract")] DealMemoDraftContract = 2,

        /// <summary>
        /// Declare Enum for Clearance Routing Contract
        /// </summary>
        [Description("Clearance Routing Contract")] ClearanceRoutingContract = 3

        #endregion "Contract Status Enum"
    }

    #endregion "ContractStatus"
}