/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IPriorityWorkQueueModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Mohammad
 * Created on     : 09-05-2012 
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
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IPriorityWorkQueueModel
    {
        
        WorkQueue WorkQueues { get; set; }
        IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }
        IEnumerable<SelectListItem> WorkQueueFilterItems { get; set; }
        IEnumerable<SelectListItem> ReviewReasonItems { get; set; }
        List<SelectListItem> ClearanceAdminList { get; set; }
        string ClearanceAdminIds { get; set; }
        string ClearanceAdminNames { get; set; }
    }
}
