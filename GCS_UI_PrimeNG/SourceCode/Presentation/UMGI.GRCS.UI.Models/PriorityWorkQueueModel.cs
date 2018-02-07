/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : PriorityWorkQueueModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Ramesh Johnson
 * Created on     : 16-08-2012 
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
// In progress item do not review this code...

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Utilities;

namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PriorityWorkQueueModel : IPriorityWorkQueueModel
    {
        public PriorityWorkQueueModel()
        {
            WorkQueues = new WorkQueue();
            ClearanceAdminList = new List<SelectListItem>();
        }

        public WorkQueue WorkQueues { get; set; }
        public IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }
        public IEnumerable<SelectListItem> WorkQueueFilterItems { get; set; }
        public IEnumerable<SelectListItem> ReviewReasonItems { get; set; }
        public List<SelectListItem> ClearanceAdminList { get; set; }
        public string ClearanceAdminIds { get; set; }
        public string ClearanceAdminNames { get; set; }
    }
}
