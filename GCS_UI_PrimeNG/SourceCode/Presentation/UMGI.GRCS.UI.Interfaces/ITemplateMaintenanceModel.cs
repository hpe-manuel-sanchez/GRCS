/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ITemplateMaintenanceModel.cs
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
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface ITemplateMaintenanceModel
    {
        IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        List<ContractTemplateInfo> ContractTemplateInfos { get; set; }
        List<TemplateDetails> DigitalRestrictionTemplates { get; set; }
    }
}
