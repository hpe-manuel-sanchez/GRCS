/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : IProjectManager.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : Dhruv Arora
 * Created on   : 10-14-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks

***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/

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
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using ProjectSearchResult = UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities.ProjectSearchResult;
using RoleType = UMGI.GRCS.BusinessEntities.Lookups.RoleType;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceProjectManager
    {
        ClearanceRegularProject SaveRegularProjectDetails(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);
        MasterProject SaveProjectDetails(MasterProject masterProjectDetails, LeanUserInfo userInfo);
        MasterProject GetMasterProjectDetails(int clearanceProjectId);
        ProjectSearchResult SearchProject(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);
        List<ClearanceMasterData> FillProjectSearchDropDown();
        List<ClearanceMasterData> GetMasterData(List<string> inputMasterDataType, bool selectedMasterDataTypesOnly = false);
        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, bool isPaging);
        List<CompanyInfo> GetCompanies(string companyIds);

        List<ClearanceMasterData> GetClearanceProjectDropDownByUserList(string loginName);
        bool SaveRequestType(ClearanceRegularProject requestTypeRegular, LeanUserInfo userInfo);
        ProjectSearchResult InquiryClearanceProjectSearch(ClearanceProjectInquirySearchCriteria projectSearchCriteria, string loginName);
        List<ClearanceMasterData> GetClearanceProjectRequestTypeDropDownByProjectType(int projectTypeId);
        bool TransferUser(string[] addUserTransferList, int NewOwnerId, LeanUserInfo userInfo);
        List<UserTransfer> FillWorkGroupUserDropDown(LeanUserInfo userInfo, int WorkgroupID);//modify by vikas , fetch User based on WorkgroupID
        ProjectSearchResult SearchProjectForUserTransfer(ClearanceProjectSearchCriteria projectSearchCriteria, int[] _arrstatuslist, LeanUserInfo userInfo);
        //add by vikas to fetch workgroup
        List<ClearanceMasterData> GetWorkGroupDropdown(string userLoginName);
        bool RemoveFile(int documentId);
        UploadDocument ReadFile(int documentId);
        bool ReopenProject(long projectId, LeanUserInfo userinfo, int _ReopenedStatusId);
        ClearanceRegularProject GetRegularProjectDetails(int clearanceProjectId);
        BusinessEntities.Entities.ProjectEntities.ProjectSearchResult Search(ProjectSearchCriteria searchCriteria);
        Dictionary<long, string> CreateProject(R2Project project, long clrProjectId, UserInfo userInfo);
        bool StatusProjectUpdate(long projectId, int statustype, ClearanceRoutedProject clearanceRoutedProjectData, LeanUserInfo user, DateTime ModifiedDate);
        string getLabelNmForExistingRelease(int LabelId);
        int getMusicClassTypeIdForExistingRelease(string musicType);
        List<ClearanceMasterData> GetLabelNameForExistingRelease(List<long> labelIds);
        bool RemoveReleaseProject(ClearanceRelease clearanceRelease);
        ClearanceRelease GetReleasesForR2(long releaseId);
        ClearanceResource GetResourceForR2(long clrProjectResourceId);
        byte UpdatePushR2ItemStatus(List<BusinessEntities.Entities.ClearanceProjectEntities.ClearancePushR2Item> clrPushR2Item);
        LeanUserInfo GetEmailId(String LoginName);
        string GetCompaniesPushToR2(List<KeyValuePair<long, string>> Workgroups);
        List<ClearanceRegularProject> GetDivisionPushToR2(string companyIds);
        List<ClearanceRegularProject> GetLabelPushToR2(string divisionIds);
        bool SaveR2ProjectIdLinking(ClearanceRegularProject regularProjectDetails);
        List<string> AutoCompleteSearch(SearchCriteria autoSearch, string userLogin);
        bool SaveReleaseResourceToPushToR2(List<BusinessEntities.Entities.ClearanceProjectEntities.ClearancePushR2Item> clrPushR2Item);
        List<ClearanceInboxRequest> GetRequestSummaryRequests(string clearanceProjectId, short GridType, int pageSize, int pageNo, string jtSorting, out byte _routingStatus);
        bool ClearanceRoutingAction(ClearanceRoutedProject clrRoutedProject);
        List<long> GetSafeCountries(List<long> countryList);
        CompanyInfo GetRequestorCompany(long company_Id);
        bool SendEmail(string subject, string body, string toAddress);
        List<ClearanceRoutingDetails> GetRoutingDetails(long routedItemID);
        ClearanceRegularProject GetRegularProjectInfo_RequestType(int projectid);
        ClearanceRegularProject GetRegularProjectReleases(int projectid);
        ClearanceRegularProject GetRegularProjectResources(int projectid);
        ProjectSearchResult SearchProjectToAllocateUPC(ClearanceProjectSearchCriteria projectSearchCriteria);
        ProjectSearchResult SearchClearanceUnlockedProjects(ClearanceProjectSearchCriteria projectSearchCriteria);
        bool UpdateProjectLockingStatus(long clrProjectId, long? userId);
        string IsProjectUnlocked(int clrProjectId, long userId);
        bool IsProjectUnlockedByAdmin(long clrProjectId, long currenctUserId);


        #region Downstream Notification
        ClrProject BuildClearanceProject(long clrProjectId);
        T BuildClearanceProjectXml<T>(long clrProjectId, bool notify, NotificationType clrType);
        long GetClrProjectIdFromProjectCode(string projectCode);
        List<ClrRelease> GetReleaseFromClrProject(long releaseId);
        List<ClrResource> GetResourceFromClrProject(long resourceId);

        #endregion

        bool GetAuthorizationsForR2(string windowsUserName, string taskName,
                                                      AnaTargetApplication targetApplication,
                                                      string hashCode);


        List<CompanyInfo> GetDivisions(string divisionIds, long userId, string taskType);
        List<CompanyInfo> GetLabels(long companyId, long divisionId, long userId, string taskType);
        List<HoldbackPeriod> GetRequestType(int ProjectTypeId);

        List<ListItem> GetRCCHandlerList(RoleType roleType);

        bool QueueEmailFailedReleases(List<BusinessEntities.Entities.ClearanceProjectEntities.ClearancePushR2Item> clrPushR2Item);

        bool QueueEmailFailedResources(List<BusinessEntities.Entities.ClearanceProjectEntities.ClearancePushR2Item> clrPushR2Item);

        bool CompleteRegularProjectNewReleases();

        List<string> SearchProjectForEmail(string projectReference, LeanUserInfo userInfo);

        List<ClearanceAdminCompany> GetCompanyListFromANA(long userId, string taskType, string companyName);
        List<ClearanceAdminCompany> AutoCompleteCreateSearchProject(SearchCriteria autoSearch, long userId);
        string IsProjectLocked(int Project_Id, long user_id);
        long GetTerritoryIdRequestorCompany(long RequestorCompanyId);
        BusinessEntities.Entities.ClearanceProjectEntities.ClearancePushR2Item GetR2ItembyID(long repertoireId, int item_type);
        List<BusinessEntities.Entities.ClearanceProjectEntities.ClearancePushR2Item> GetR2ItemsPending(int numberOfItems, byte retriesMaxValue);
        void QueueEmailPushToR2Repertoire(UMGI.GRCS.BusinessEntities.Lookups.EmailType type, List<BusinessEntities.Entities.ClearanceProjectEntities.ClearancePushR2Item> clrPushR2Item);
        List<ClearanceMasterData> AutoCompleteSearchLabels(SearchCriteria autoSearch, long userId);
        bool InsertErrorInfoIntoBackgroundTable(long projectId, RoutingAction routingActionType, LeanUserInfo user, Exception exception);

    }
}
