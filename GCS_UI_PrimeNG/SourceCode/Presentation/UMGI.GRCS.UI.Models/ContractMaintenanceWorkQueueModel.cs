/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractMaintenanceWorkQueueModel.cs
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
using System.Globalization;
using System.Linq;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.UI.Interfaces;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// </summary>    
    public class ContractMaintenanceWorkQueueModel : IContractMaintenanceWorkQueueModel
    {
        /// <summary>
        /// Gets or sets the contract maintenance work queue.
        /// </summary>
        /// /// <value>The contract maintenance work queue.</value>

        public ContractMaintenanceWorkQueueModel()
        {
            ContractMaintenanceWorkQueue = new List<ContractInfo>();            
        }

        public IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }
        public List<ContractInfo> ContractMaintenanceWorkQueue { get; set; }
    }
}

