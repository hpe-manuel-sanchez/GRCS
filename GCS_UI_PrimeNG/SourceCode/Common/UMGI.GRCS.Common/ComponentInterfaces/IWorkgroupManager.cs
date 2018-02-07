/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IWorkgroupManager.cs 
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
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using System;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IWorkgroupManager
    {
        List<WorkgroupSearchResult> GetWorkgroups(WorkgroupSearchCriteria workgroupSearchCriteria);
        List<Request> DeactivateWorkGroup(RequestSearch requestSearch);
        List<CompanyInfo> GetCompanies(string companyIds);
        string AddWorkGroup(Workgroup workgroupDetails, string userLoginName);
        List<Workgroup> GetRoles();
        List<WorkGroupUser> GetUsersList(string userIds);
        Workgroup GetWorkgroup(long workGroupId);
        byte? GetRoleWorkgroup(long workGroupId);
        ChildWorkgroup GetChildWorkgroup(long workGroupId);
        List<TerritorialDisplay> GetTerritoriesForWorkgroup(long workGroupId);
        bool RequestReassignToWorkgroup(long expectWorkgroupId, long assignedgtWorkgroupId, string requestIds, string userName);
        List<WorkgroupSearchResult> GetWorkgroupByChild(long parentId, long workgroupId, WorkgroupSearchCriteria workgroupSearchCriteria);
        bool DeleteWorkgroup(long workgroupId, string userName, DateTime modifiedDate);
        bool UpdateWorkGroup(Workgroup workgroupDetails, string userLoginName);
        List<CompanyInfo> GetCompaniesOfWorkgroup(CompanySearchCriteria companySearchCriteria);
        string AddChildWorkGroup(ChildWorkgroup childWorkgroupDetails);
        List<WorkgroupSearchResult> GetWorkgroups(WorkgroupSearchCriteria workgroupSearchCriteria, string contractIds);
        List<long> LinkArtistContractToWorkgroup(string contractIds, string workgroupIds, string userLoginName);
        List<long> LinkResourceContractToWorkgroup(List<DeviationResourceContract> contractAndResourceIds, string workgroupIds, string userLoginName);
        GcsAuthentication GetUserAuthorizationData(string userName);
        List<Workgroup> GetRequestType();
        List<KeyValuePair<int, string>> GetResourceType();
        bool UpdateChildWorkGroup(ChildWorkgroup childWorkgroupDetails);
        List<string> GetWorkgroupNamesForAutoComplete(string workgroupName);
        List<string> GetUsersForAutoComplete(string user);
        long IdentifyWorkgroup(WorkgroupIdentificationParameters workgroupIdentificationParameters);
        List<WorkgroupSearchResult> SearchWorkgroupForRemoveUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string loginName);
        List<WorkgroupSearchResult> SearchWorkgroupToAddUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string userLoginName, string searchLoginId);
        List<string> RemoveUserFromMultipleWorkgroup(string userToRemove, string workgroupIdList, string userLoginName);
        bool AddUserInMultipleWorkgroup(List<Workgroup> workgroupDetails, WorkGroupUser userData, string userLoginName);
        List<Preferences> GetEmailPreference();
        List<UserPreference> GetUserPreferences(long userId);
        bool InsertUserPrefernces(List<UserPreference> userPreferenceList, long userId, string loginUserName);
        List<WorkgroupBase> GetWorkgroups(long userId);
        bool HasSpecialPermission(long userId, long workgroupId, bool isR2AuthorizedPermissionCheck);

        List<LeanUserInfo> GetUserDetailsForWorkgroup(long workgroupId);
    }
}
