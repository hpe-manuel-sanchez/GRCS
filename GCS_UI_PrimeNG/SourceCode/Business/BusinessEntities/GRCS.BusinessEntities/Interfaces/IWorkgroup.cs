/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : IWorkgroup.cs
 * Project Code :   
 * Author       : Pavan Kumar K
 * Created on   : 13 July 2012 
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;


namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IWorkgroup
    {
        [OperationContract(Name = "GetCompaniesOfWorkgroup")]
        List<CompanyInfo> GetCompaniesOfWorkgroup(CompanySearchCriteria companySearchCriteria);


        [OperationContract(Name = "SearchWorgroup")]
        List<WorkgroupSearchResult> GetWorkgroups(WorkgroupSearchCriteria workgroupSearchCriteria);

        [OperationContract(Name = "DeactivateWorkGroup")]
        List<Request> DeactivateWorkGroup(RequestSearch requestSearch);

        [OperationContract(Name = "GetCompanies")]
        List<CompanyInfo> GetCompanies(string companyIds, string userLoginName);


        [OperationContract(Name = "AddWorkGroup")]
        string AddWorkGroup(Workgroup workgroupDetails, string userLoginName);

        [OperationContract(Name = "GetRoles")]
        List<Workgroup> GetRoles(string userName);

        [OperationContract(Name = "RequestReassignToWorkgroup")]
        bool RequestReassignToWorkgroup(long expectWorkgroupId, long assignedWorkgroupId, string requestIds, string userName);

        [OperationContract(Name = "GetWorkgroupByChild")]
        List<WorkgroupSearchResult> GetWorkgroupByChild(long parentId, long workgroupId, WorkgroupSearchCriteria workgroupSearchCriteria);

        [OperationContract(Name = "GetUsersList")]
        List<WorkGroupUser> GetUsersList(string userIds, string userName);

        [OperationContract(Name = "SearchSingleWorgroup")]
        Workgroup GetWorkgroup(long workGroupId, string userName);

        [OperationContract(Name = "SearchSingleChildWorgroup")]
        ChildWorkgroup GetChildWorkgroup(long workGroupId, string userName);

        [OperationContract(Name = "GetTerritories")]
        List<TerritorialDisplay> GetTerritoriesForWorkgroup(long workGroupId, string userName);

        [OperationContract(Name = "DeleteWorkgroup")]
        bool DeleteWorkgroup(long workgroupId, string userName, DateTime modifiedDate);

        [OperationContract(Name = "UpdateWorkGroup")]
        bool UpdateWorkGroup(Workgroup workgroupDetails, string userLoginName);

        [OperationContract(Name = "SearchWorgroupForContract")]
        List<WorkgroupSearchResult> GetWorkgroups(WorkgroupSearchCriteria workgroupSearchCriteria, string contractIds);

        [OperationContract(Name = "LinkArtistContractToWorkgroup")]
        List<long> LinkArtistContractToWorkgroup(string contractIds, string workgroupIds, string userLoginName);

        [OperationContract(Name = "LinkResourceContractToWorkgroup")]
        List<long> LinkResourceContractToWorkgroup(List<DeviationResourceContract> contractAndResourceIds, string workgroupIds, string userLoginName);

        [OperationContract(Name = "AddChildWorkGroup")]
        string AddChildWorkGroup(ChildWorkgroup childWorkgroupDetails);

        [OperationContract(Name = "GetRequestType")]
        List<Workgroup> GetRequestType(string userName);

        [OperationContract(Name = "GetResourceType")]
        List<KeyValuePair<int, string>> GetResourceType(string userName);

        [OperationContract(Name = "UpdateChildWorkGroup")]
        bool UpdateChildWorkGroup(ChildWorkgroup childWorkgroupDetails);

        [OperationContract(Name = "GetWorkgroupNamesForAutoComplete")]
        List<string> GetWorkgroupNamesForAutoComplete(string workGroupName, string userName);

        [OperationContract(Name = "GetUsersForAutoComplete")]
        List<string> GetUsersForAutoComplete(string user, string userName);

        [OperationContract(Name = "SearchWorkgroupForRemoveUsers")]
        List<WorkgroupSearchResult> SearchWorkgroupForRemoveUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string loginName);

        [OperationContract(Name = "SearchWorkgroupToAddUsers")]
        List<WorkgroupSearchResult> SearchWorkgroupToAddUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string userLoginName, string searchLoginId);

        [OperationContract(Name = "RemoveUserFromMultipleWorkgroup")]
        List<string> RemoveUserFromMultipleWorkgroup(string userToRemove, string workgroupIdList, string userLoginName);

        [OperationContract(Name = "AddUserInMultipleWorkgroup")]
        bool AddUserInMultipleWorkgroup(List<Workgroup> workgroupDetails, WorkGroupUser userData, string userLoginName);

        [OperationContract(Name = "GetEmailPreferences")]
        List<Preferences> GetEmailPreference(string userLoginName);

        [OperationContract(Name = "GetUserPreferences")]
        List<UserPreference> GetUserPreferences(long userId);

        [OperationContract(Name = "InsertUserPreferences")]
        bool InsertUserPrefernces(List<UserPreference> userPreferenceList, long userId, string loginUserName);

        [OperationContract(Name = "GetWorkgroups")]
        List<WorkgroupBase> GetWorkgroups(long userId);
    }
}
