/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IWorkgroupRepository.cs 
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
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using System;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IWorkgroupRepository
    {
        IWorkgroupModel WorkgroupModel { get; set; }
        List<WorkgroupSearchResult> SearchWorkgroup(WorkgroupSearchCriteria workGroupSearchCriteria);
        List<Request> DeactivateWorkGroup(RequestSearch requestSearch);
        List<CompanyInfo> GetCompanies(string companyIds, string userLoginName);
        List<WorkGroupUser> GetUsers(WorkGroupUserSearchCriteria userSearchCriteria, AnaTargetApplication targetApplication, string userLoginName);
        Workgroup Workgroup(long workGroupId, string userInfo);
        ChildWorkgroup GetChildWorkgroup(long workGroupId, string userInfo);
        List<WorkGroupUser> GetWorkgroupUserList(string userIds, string userName);
        bool RequestReassignToWorkgroup(long expectWorkgroupId, long assignedgtWorkgroupId, string requestIds, string userName);
        List<WorkgroupSearchResult> GetWorkgroupByChild(long parentId, long workgroupId, WorkgroupSearchCriteria workGroupSearchCriteria);
        string AddWorkgroup(Workgroup workgroupDetails, string userLoginName);
        List<TerritorialDisplay> GetTerritories();
        List<Workgroup> GetRoles(string userLoginName);
        bool DeleteWorkgroup(long workgroupId, string userInfo, DateTime modifiedDate);
        bool UpdateWorkGroup(Workgroup workgroupDetails, string userLoginName);
        List<TerritorialDisplay> GetTerritoriesForWorkGroup(long workGroupId, string userLoginName);
        string AddChildWorkgroup(ChildWorkgroup childWorkgroupDetails);
        List<CompanyInfo> GetCompaniesOfWorkgroup(CompanySearchCriteria companySearchCriteria);
        List<WorkgroupSearchResult> GetWorkgroups(WorkgroupSearchCriteria workGroupSearchCriteria, string contractIds);
        List<long> LinkArtistContractToWorkgroup(string contractIds, string workgroupIds, string userInfo);
        List<long> LinkResourceContractToWorkgroup(List<DeviationResourceContract> deviationResourceContract, string workgroupIds, string userInfo);
        List<LeanContractInfo> GetContractsByArtist(long artistId, string userLoginName);
        List<LeanContractInfo> GetContractsByResource(long resourceId, string userLoginName);
        void GetLeanContract(long contractId, string userLoginName);
        List<Workgroup> GetRequestType(string userLoginName);
        bool UpdateChildWorkGroup(ChildWorkgroup childWorkgroupDetails);
        List<string> SuggestiveSearchForWorkgroup(string suggestiveInput, string workgroupElement, string pageName, string workgroupId, string userLoginName);
        List<WorkgroupSearchResult> SearchWorkgroupForRemoveUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string loginName);
        List<WorkgroupSearchResult> SearchWorkgroupToAddUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string userLoginName, string searchLoginId);
        List<string> RemoveUserFromMultipleWorkgroup(string userToRemove, string workgroupIdList, string userLoginName);
        bool AddUserInMultipleWorkgroup(List<Workgroup> workgroupDetails, WorkGroupUser userData, string userLoginName);
        List<Preferences> GetEmailPreference(string userInfo);
        List<UserPreference> GetUserPreferences(long userId);
        bool InsertUserPrefernces(List<UserPreference> UserPreferenceList, long userId, string loginUserName);
        List<WorkgroupBase> GetWorkgroups(long userId);
        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string jtSorting, string userLoginName);
        IClearanceProjectModel GetClrPACompanyAndCurrencyUserList(string userLoginName);
    }
}
