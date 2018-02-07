/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IWorkgroupModel.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IWorkgroupModel
    {
        List<CompanyInfo> CompanySearch { get; set; }
        IEnumerable<SelectListItem> RolesList { get; set; }
        List<CompanyInfo> GetCompanies { get; set; }
        WorkgroupSearchResult Workgroup { get; set; }
        bool RequestReassignToWorkgroup { get; set; }
        List<WorkgroupSearchResult> GetWorkgroupByChild { get; set; }
        List<WorkGroupUser> GetWorkgroupUserList { get; set; }
        List<CountryInfo> CountrySearch { get; set; }
        List<Workgroup> GetRoles { get; set; }
        List<CountryInfo> GetCountries { get; set; }
        List<CountryInfo> CountriesForWorkGroup { get; set; }
        bool DeleteWorkgroup { get; set; }
        string DefaultUserName { get; set; }
        List<CompanyInfo> GetCompaniesOfWorkgroup { get; set; }
        List<WorkgroupSearchResult> GetWorkgroups { get; set; }
        List<long> LinkArtistContractToWorkgroup { get; set; }
        List<long> LinkResourceContractToWorkgroup { get; set; }
        List<LeanContractInfo> ContractsList { get; set; }
        LeanContractInfo LeanContract { get; set; }
        IEnumerable<SelectListItem> RequestTypeList { get; set; }
        List<Workgroup> GetRequestType { get; set; }
        ChildWorkgroup GetChildWorkgroup { get; set; }
        IEnumerable<SelectListItem> ResourceTypeList { get; set; }
        List<string> SuggestiveSearchForWorkgroup { get; set; }
        List<WorkgroupSearchResult> SearchWorkgroupForRemoveUsers { get; set; }
        List<WorkgroupSearchResult> SearchWorkgroupToAddUsers { get; set; }
        List<string> RemoveUserFromMultipleWorkgroup { get; set; }
        bool AddUserInMultipleWorkgroup { get; set; }
        IClearanceProjectModel GetClrPACompanyAndCurrencyUserList { get; set; }
    }
}
