/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IResourcePreClearanceReviewModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Karthik P
 * Created on     : 12-02-2013 
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
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;

namespace UMGI.GRCS.UI.Interfaces.RightsReviewWQ
{
    public interface IResourcePreClearanceReviewModel
    {

        ResourcePreClearanceResult PreClearanceResult { get; set; }
        PreClearanceMasterData PreClearanceMasterData { get; set; }
        List<SelectListItem> ReviewStatusDropDown { get; set; }
    }
}
