using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using ProjectSearchResult = UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities.ProjectSearchResult;
using RoleType = UMGI.GRCS.BusinessEntities.Lookups.RoleType;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceProjectRepository
    {
        //IClearanceProjectModel IClearanceProjectModel { get; set; }
        ProjectSearchResult SearchProject(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);
        IClearanceProjectModel FillProjectSearchDropDown(LeanUserInfo userInfo = null);

        MasterProject SaveProjectDetails(MasterProject masterProjectDetails, LeanUserInfo userInfo);

        MasterProject GetMasterProjectDetails(int clearanceProjectId, LeanUserInfo userInfo);

        //List<RequestTypeRegular> GetRequestedPriceLevel(LeanUserInfo userInfo);

        //List<RequestTypeRegular> GetCurrentPricelevel(LeanUserInfo userInfo);

        bool SaveRequestType(ClearanceRegularProject requestTypeRegular, LeanUserInfo userInfo);

        IClearanceProjectModel GetMasterData(List<string> inputMasterDataType, LeanUserInfo userInfo);
        // IClearanceProjectModel getCompanyListUserWise(string loginName);
        IClearanceProjectModel GetClearanceProjectDropDownByUserList(string loginName);

        List<CompanyInfo> CompanySearch(string name, string isacCode, string country, int jtStartIndex, int jtPageSize, bool isPaging, LeanUserInfo userInfo);
        List<CompanyInfo> GetCompanies(string companyIds, LeanUserInfo userInfo);
        ClearanceRegularProject SaveRegularProjectDetails(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);

        ProjectSearchResult InquiryClearanceProjectSearch(ClearanceProjectInquirySearchCriteria SearchCriteria, string loginDetails = "");
        IClearanceProjectModel GetInquiryClearanceProjectSearchDropDown(string loginName, LeanUserInfo userInfo);
        List<ClearanceMasterData> GetClearanceProjectRequestTypeDropDownByProjectType(int RequestTypeId, LeanUserInfo userInfo);      
            
        IClearanceProjectModel GetRegularReleases(ClearanceReleaseSearch searchcriteria);
        List<ClearanceResource> GetRegularResources(int upcNumber);

        bool TransferUser(string[] UserTransferList, int NewOwnerId, LeanUserInfo userInfo);
        IClearenceUserTransferModel FillWorkGroupUserDropDown(LeanUserInfo userInfo, int WorkgroupID);
        ProjectSearchResult SearchProjectForUserTransfer(ClearanceProjectSearchCriteria projectSearchCriteria, int[] _arrstatuslist, LeanUserInfo userInfo);
        //  IClearanceProjectModel GetWorkGroupDropdown(UserInfo userInfo);

        bool RemoveFile(int documentId, LeanUserInfo userInfo);
        UploadDocument ReadFile(int documentId, LeanUserInfo userInfo);

        //List<DropDeviatedICLALevel> FillDeviatedICLALevel(LeanUserInfo userInfo);
        List<ResourceDetail> SearchR2MockDataResource(ResourceSearch resourceSearch, LeanUserInfo userInfo);
        //List<DropPriceLevel> FillPriceLevel(LeanUserInfo userInfo);
        IClearanceResourceModel GetMasterDataResource(List<string> inputMasterDataType, LeanUserInfo userInfo);
        //List<DropPromotionalLevel> FillPromotionalPriceLevel(LeanUserInfo userInfo);
        //List<DropClubLevel> FillClubPriceLevel(LeanUserInfo userInfo);
        BusinessEntities.Entities.ProjectEntities.ProjectSearchResult ProjectSearch(ProjectSearchCriteria criteria);
        Dictionary<long, string> CreateProject(R2Project project, long clrProjectId, LeanUserInfo userInfo);
        bool StatusProjectUpdate(long projectId, int statustype, ClearanceRoutedProject clearanceRoutedProjectData, LeanUserInfo user,DateTime ModifiedDate);
        ClearanceRegularProject GetRegularProjectDetails(int clearanceProjectId, LeanUserInfo userInfo);        
        int getMusicClassTypeIdForExistingRelease(string musicType, LeanUserInfo userInfo);
        string getLabelNmForExistingRelease(int LabelId, LeanUserInfo userInfo);
        bool RemoveReleaseProject(ClearanceRelease clearanceRelease, LeanUserInfo userInfo);        
        List<CompanyInfo> GetCompaniesPushToR2(List<KeyValuePair<long, string>> Workgroups, LeanUserInfo userInfo);
        IClearanceProjectModel GetDivisionPushToR2(string companyIds, LeanUserInfo userInfo);
        IClearanceProjectModel GetLabelPushToR2(string labelIds, LeanUserInfo userInfo);
        bool SaveR2ProjectIdLinking(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);
        List<string> AutoCompleteSearch(SearchCriteria autoSearch, string userLogin);
        bool SaveReleaseResourceToPushToR2(List<ClearancePushR2Item> clrPushR2Item, LeanUserInfo userInfo);
        List<ClearanceInboxRequest> GetRequestSummaryRequests(string clearanceProjectId, short GridType, int pageSize, int pageNo, string jtSorting, LeanUserInfo userInfo, out byte routingStatus);
        bool ClearanceRoutingAction(ClearanceRoutedProject clrRoutedProject, LeanUserInfo userInfo);
        CompanyInfo GetRequestorCompany(long company_Id, LeanUserInfo userInfo);
        List<ClearanceRoutingDetails> GetRoutingDetails(long routedItemID);      
        ClearanceRegularProject GetRegularProjectInfo_RequestType(int clearanceProjectId, LeanUserInfo userInfo);
        ClearanceRegularProject GetRegularProjectReleases(int clearanceProjectId, LeanUserInfo userInfo);
        ClearanceRegularProject GetRegularProjectResources(int clearanceProjectId, LeanUserInfo userInfo);
        ProjectSearchResult SearchProjectToAllocateUPC(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);
        ProjectSearchResult SearchClearanceUnlockedProjects(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);
        bool UnlockProject(long clrProjectId, LeanUserInfo userInfo);
        string IsProjectUnlocked(int clrProjectId, LeanUserInfo userInfo);
        bool IsProjectUnlockedByAdmin(long clrProjectId, LeanUserInfo userInfo);
        bool GetAuthorizationsForR2(string windowsUserName, string taskName,
                                                      AnaTargetApplication targetApplication,
                                                      string hashCode);
        List<CompanyInfo> GetDivisions(string divisionIds, string taskType, LeanUserInfo userInfo);
        List<CompanyInfo> GetLabels(long companyId, long divisionId, string taskType, LeanUserInfo userInfo);
        List<HoldbackPeriod> GetRequestType(int ProjectTypeId);
        List<ListItem> GetRCCHandlerList(RoleType roleType, LeanUserInfo userInfo);
        List<string> SearchProjectForEmail(string projectReference, LeanUserInfo userInfo);
        bool HasSpecialPermission(long userId, long workgroupId, bool isR2AuthorizedPermissionCheck);
        List<ClearanceAdminCompany> AutoCompleteCreateSearchProject(SearchCriteria autoSearch, long userId);
        long GetTerritoryIdRequestorCompany(long RequestorCompanyId);
        List<ClearanceMasterData> AutoCompleteSearchLabels(SearchCriteria autoSearch, long userId);

    }
}
