using System;
using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using ProjectSearchResult = UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities.ProjectSearchResult;
using RoleType = UMGI.GRCS.BusinessEntities.Lookups.RoleType;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IClearanceProject
    {
        [OperationContract(Name = "ClearanceProjectSearch")]
        ProjectSearchResult SearchProject(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);


        [OperationContract(Name = "FillProjectSearchDropDown")]
        List<ClearanceMasterData> FillProjectSearchDropDown(LeanUserInfo userInfo);

        [OperationContract(Name = "GetMasterData")]
        List<ClearanceMasterData> GetMasterData(List<string> inputMasterDataType, LeanUserInfo userInfo);
        [OperationContract(Name = "SearchR2Resource")]
        List<ResourceDetail> SearchR2Resource(ResourceSearch resourceSearch, LeanUserInfo userInfo);

        [OperationContract(Name = "SaveProjectDetails")]
        MasterProject SaveProjectDetails(MasterProject masterProjectDetails, LeanUserInfo userInfo);

        [OperationContract(Name = "SaveRegularProjectDetails")]
        ClearanceRegularProject SaveRegularProjectDetails(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);

        [OperationContract(Name = "GetMasterProjectDetails")]
        MasterProject GetMasterProjectDetails(int clearanceProjectId, LeanUserInfo userInfo);

        [OperationContract(Name = "SearchCompany")]
        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, bool isPaging, LeanUserInfo userInfo);

        [OperationContract(Name = "GetCompanies")]
        List<CompanyInfo> GetCompanies(string companyId, LeanUserInfo userInfos);

        [OperationContract(Name = "GetClearanceProjectDropDownByUserList")]
        List<ClearanceMasterData> GetClearanceProjectDropDownByUserList(string loginName);

        [OperationContract(Name = "ClearanceProjectInquirySearch")]
        ProjectSearchResult InquiryClearanceProjectSearch(ClearanceProjectInquirySearchCriteria projectSearchCriteria, string loginName);

        [OperationContract(Name = "GetClearanceProjectRequestTypeDropDownByProjectType")]
        List<ClearanceMasterData> GetClearanceProjectRequestTypeDropDownByProjectType(int ProjectTypeId, LeanUserInfo userInfo);

        [OperationContract(Name = "SaveRequestType")]
        bool SaveRequestType(ClearanceRegularProject requestTypeRegular, LeanUserInfo userInfo);

        [OperationContract(Name = "TransferUser")]
        bool TransferUser(string[] _addUserTransferList, int NewOwnerId, LeanUserInfo userInfo);

        [OperationContract(Name = "FillWorkGroupUserDropDown")]
        List<UserTransfer> FillWorkGroupUserDropDown(LeanUserInfo userInfo, int WorkgroupID);

        [OperationContract(Name = "SearchProjectForUserTransfer")]
        ProjectSearchResult SearchProjectForUserTransfer(ClearanceProjectSearchCriteria projectSearchCriteria, int[] _arrstatuslist, LeanUserInfo userInfo);

        //add by vikas to fetch workgroup
        [OperationContract(Name = "GetWorkGroupDropdown")]
        List<ClearanceMasterData> GetWorkGroupDropdown(string userLoginName);

        [OperationContract(Name = "RemoveFile")]
        bool RemoveFile(int documentId, LeanUserInfo userInfo);

        [OperationContract(Name = "ReadFile")]
        UploadDocument ReadFile(int documentId, LeanUserInfo userInfo);

        [OperationContract(Name = "ReopenProject")]
        bool ReopenProject(long projectId, int _ReopenedStatusId, LeanUserInfo userInfo);

        [OperationContract(Name = "GetRegularProjectDetails")]
        ClearanceRegularProject GetRegularProjectDetails(int projectid, LeanUserInfo userInfo);


        [OperationContract(Name = "StatusProjectUpdate")]
        bool StatusProjectUpdate(long projectId, int statustype, ClearanceRoutedProject clearanceRoutedProjectData, LeanUserInfo user, DateTime ModifiedDate);

        [OperationContract]
        string getLabelNmForExistingRelease(int LabelId, LeanUserInfo userInfo);

        [OperationContract]
        int getMusicClassTypeIdForExistingRelease(string musicType, LeanUserInfo userInfo);

        [OperationContract]
        bool RemoveReleaseProject(ClearanceRelease clearanceRelease, LeanUserInfo userInfo);

        [OperationContract]
        LeanUserInfo GetEmailId(String LoginName, LeanUserInfo userInfo);

        [OperationContract]
        List<CompanyInfo> GetCompaniesPushToR2(List<KeyValuePair<long, string>> Workgroups, LeanUserInfo userInfo);

        [OperationContract]
        List<ClearanceRegularProject> GetDivisionPushToR2(string companyIds, LeanUserInfo userInfo);

        [OperationContract]
        List<ClearanceRegularProject> GetLabelPushToR2(string labelIds, LeanUserInfo userInfo);

        [OperationContract]
        bool SaveR2ProjectIdLinking(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);

        [OperationContract]
        List<string> AutoCompleteSearch(SearchCriteria autoSearch, string userLogin);

        [OperationContract]
        bool SaveReleaseResourceToPushToR2(List<ClearancePushR2Item> clrPushR2Item, LeanUserInfo userInfo);

        [OperationContract(Name = "ClearanceRoutingAction")]
        bool ClearanceRoutingAction(ClearanceRoutedProject clrRoutedProject, LeanUserInfo userInfo);

        [OperationContract(Name = "GetSafeCountries")]
        List<long> GetSafeCountries(List<long> countryList, LeanUserInfo userInfo);

        [OperationContract(Name = "GetRequestorCompany")]
        CompanyInfo GetRequestorCompany(long company_Id, LeanUserInfo userInfo);

        [OperationContract(Name = "GetRequestSummaryRequests")]
        List<ClearanceInboxRequest> GetRequestSummaryRequests(string clearanceProjectId, short GridType, int pageSize, int pageNo, string jtSorting, LeanUserInfo userInfo, out byte routingStatus);

        [OperationContract(Name = "GetRoutingDetails")]
        List<ClearanceRoutingDetails> GetRoutingDetails(long routedItemID);

        [OperationContract(Name = "Search")]
        Entities.ProjectEntities.ProjectSearchResult Search(ProjectSearchCriteria searchCriteria);

        [OperationContract(Name = "CreateProject")]
        Dictionary<long, string> CreateProject(R2Project project, long clrProjectId, UserInfo userInfo);

        [OperationContract(Name = "GetRegularProjectInfo_RequestType")]
        ClearanceRegularProject GetRegularProjectInfo_RequestType(int projectid, LeanUserInfo userInfo);

        [OperationContract(Name = "GetRegularProjectReleases")]
        ClearanceRegularProject GetRegularProjectReleases(int projectid, LeanUserInfo userInfo);

        [OperationContract(Name = "GetRegularProjectResources")]
        ClearanceRegularProject GetRegularProjectResources(int projectid, LeanUserInfo userInfo);
        [OperationContract]
        ProjectSearchResult SearchProjectToAllocateUPC(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);
        [OperationContract]
        ProjectSearchResult SearchClearanceUnlockedProjects(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);
        [OperationContract]
        bool UpdateProjectLockingStatus(long clrProjectId, LeanUserInfo userInfo);
        [OperationContract]
        string IsProjectUnlocked(int clrProjectId, LeanUserInfo userInfo);
        [OperationContract]
        bool IsProjectUnlockedByAdmin(long clrProjectId, LeanUserInfo userInfo);

        [OperationContract]
        bool SendEmail(string subject, string body, string toAddress, LeanUserInfo userInfo);

        [OperationContract]
        bool GetAuthorizationsForR2(string windowsUserName, string taskName,
                                                       AnaTargetApplication targetApplication,
                                                       string hashCode);
        [OperationContract]
        List<CompanyInfo> GetDivisions(string divisionIds, string taskType, LeanUserInfo userInfo);

        [OperationContract]
        List<CompanyInfo> GetLabels(long companyId, long divisionId, string taskType, LeanUserInfo userInfo);

        [OperationContract]
        List<HoldbackPeriod> GetRequestType(int ProjectTypeId);

        [OperationContract]
        List<ListItem> GetRCCHandlerList(RoleType roleType, LeanUserInfo userInfo);

        [OperationContract]
        bool QueueEmailFailedReleases(List<ClearancePushR2Item> clrPushR2Item);

        [OperationContract]
        bool QueueEmailFailedResources(List<ClearancePushR2Item> clrPushR2Item);

        [OperationContract]
        bool CompleteRegularProjectNewReleases();

        [OperationContract]
        List<string> SearchProjectForEmail(string projectReference, LeanUserInfo userInfo);
        [OperationContract]
        bool HasSpecialPermission(long userId, long workgroupId, bool isR2AuthorizedPermissionCheck);

        [OperationContract]
        List<ClearanceAdminCompany> GetCompanyListFromANA(long userId, string taskType, string companyName);

        [OperationContract]
        List<ClearanceAdminCompany> AutoCompleteCreateSearchProject(SearchCriteria autoSearch, long userId);

        [OperationContract]
        long GetTerritoryIdRequestorCompany(long RequestorCompanyId);

        [OperationContract]
        List<ClearanceMasterData> AutoCompleteSearchLabels(SearchCriteria autoSearch, long userId);
    }
}
