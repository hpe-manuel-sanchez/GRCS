/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IContractMaintenanceWorkQueueModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : 
 * Created on     : 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IContractMaintenanceWorkQueueModel
    {
        /// <summary>
        /// Gets or sets the contract maintenance work queue.
        /// </summary>
        /// <value>The contract maintenance work queue.</value>
        List<ContractInfo> ContractMaintenanceWorkQueue { get; set; }
        IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }
    }

  
}
