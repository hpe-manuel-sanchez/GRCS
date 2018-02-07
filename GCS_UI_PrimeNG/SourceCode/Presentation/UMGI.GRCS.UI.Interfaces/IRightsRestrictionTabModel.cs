/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsRestrictionModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Parvatharaj C
 * Created on     : 10-07-2012 
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

using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

using System.Web.Mvc;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IRightsRestrictionTabModel
    {        
        List<ContractDigitalRestrictions> DigitalRestriction { get; set; }        
        List<ContractRightsAcquired> RightsAcquired { get; set; }
        IEnumerable<SelectListItem> ContentTypeList { get; set; }
        IEnumerable<SelectListItem> UseTypeList { get; set; }
        IEnumerable<SelectListItem> CommercialModelsList { get; set; }
        IEnumerable<SelectListItem> RestrictionList { get; set; }
        IEnumerable<SelectListItem> ConsentPeriodList { get; set; }
        IEnumerable<SelectListItem> IsAcquiredList { get; set; }
        IEnumerable<SelectListItem> DealRestrictOption { get; set; }
        IEnumerable<SelectListItem> DigitalTemplate { get; set; }
        bool  Result { get; set; }
        long TemplateId { get; set; }
        string TemplateName { get; set; }
        string ClearanceAdmin { get; set; }
        bool IsNewTemplate { get; set; }
        string LastModifiedDate { get; set; }
         DateTime LastModifiedTime { get; set; }
    }
}
