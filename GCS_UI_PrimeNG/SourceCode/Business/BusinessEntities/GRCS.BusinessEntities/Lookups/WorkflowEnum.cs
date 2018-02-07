/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : WorkFlowEnum.cs
 * Project Code :   
 * Author       : Pavan Kumar K
 * Created on   : 16 July 2012 
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
 * Description     Declare Workflow status Enum

****************************************************************************/

using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Lookups
{

    #region Workflow status enumerator

    /// <summary>
    /// Workflow Status
    /// </summary>
    public enum WorkflowStatus
    {
        /// <summary>
        /// Initial state of the Contract
        /// </summary>
        [Description("Draft")] Draft = 0,

        /// <summary>
        /// 
        /// </summary>
        [Description("Data Entry")] DataEntry = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Pending Approval")] PendingApproval = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("Approved")] Approved = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("Under Amendment")] UnderAmendment = 4
    }

    #endregion Workflow status enumerator
}