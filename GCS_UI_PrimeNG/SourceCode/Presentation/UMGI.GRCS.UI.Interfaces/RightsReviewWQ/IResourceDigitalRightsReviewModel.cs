/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IResourceDigitalRightsReviewModel.cs
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

using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using System.Collections.Generic;
namespace UMGI.GRCS.UI.Interfaces.RightsReviewWQ
{
    public interface IResourceDigitalRightsReviewModel
    {
        ResourceDigitalRightsResult DigitalRightsResult { get; set; }
        DigitalRightsMasterData DigitalRightsMasterData { get; set; }
        string[] UseTypeDropDown { get; set; }
        string[] CommercialModelReasonDropDown { get; set; }
        string[] ConsentPeriodDropDown { get; set; }
        string[] RestrictionDropDown { get; set; }
        string[] RestrictionReasonDropDown { get; set; }
        List<SelectListItem> ReviewStatusDropDown { get; set; }
    }
}
