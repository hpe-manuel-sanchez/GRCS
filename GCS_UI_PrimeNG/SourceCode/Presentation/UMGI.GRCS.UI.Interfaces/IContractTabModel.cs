/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractTabModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
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

using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IContractTabModel
    {
        ContractDetails Contract { get; set; }
        ContractInfo Contracts { get; set; }
        NoticeCompany PcCompany { get; set; }
        ArtistDetail Artist { get; set; }
        ContractSearch ContractsSearch { get; set; }
        GlobalAddressSearch SearchGlobalAddressList { get; set; }
        IEnumerable<SelectListItem> Country { get; set; }
        IEnumerable<SelectListItem> SearchCategory { get; set; }
        IEnumerable<SelectListItem> WorkFlowStatus { get; set; }
        IEnumerable<SelectListItem> ContractStatus { get; set; }
        IEnumerable<SelectListItem> ContractingParty { get; set; }
        IEnumerable<SelectListItem> ClearanceAdminCompany { get; set; }
        IEnumerable<SelectListItem> RightsType { get; set; }

        IEnumerable<SelectListItem> UmgSigningCompList { get; set; }
        IEnumerable<SelectListItem> PcCompanyCountryList { get; set; }
        IEnumerable<SelectListItem> ContractDescriptionList { get; set; }
        IEnumerable<SelectListItem> ContractStatusList { get; set; }
        IEnumerable<SelectListItem> OnActiveRosterList { get; set; }
        IEnumerable<SelectListItem> RightsPeriodList { get; set; }
        IEnumerable<SelectListItem> LostRightsIndicatorList { get; set; }
        IEnumerable<SelectListItem> LostRightsReasonList { get; set; }
        IEnumerable<SelectListItem> LegalReviewRequiredList { get; set; }
        IEnumerable<SelectListItem> ActiveForMarketingList { get; set; }
        IEnumerable<SelectListItem> SensitiveArtistList { get; set; }
        IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        ContractInfo AddParentContract { get; set; }
        List<ContractInfo> SplitContract { get; set; }
        IEnumerable<SelectListItem> SelectTemplate { get; set; }
        string UserRoleName { get; set; }
    }
}
