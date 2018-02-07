/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : IProjectData.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : Dhruv Arora
 * Created on   : 14-10-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks

***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/

using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Constants;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceProjectData
    {
        ClearanceRegularProject SaveRegularProjectDetails(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);

        MasterProject SaveProjectDetails(MasterProject masterProjectDetails, LeanUserInfo userInfo);

        MasterProject GetMasterProjectDetails(int clearanceProjectId);

        ProjectSearchResult SearchProject(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo);

        List<ClearanceMasterData> FillProjectSearchDropDown();

        List<ClearanceMasterData> GetMasterData(List<string> _inputMasterDataType);

        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, bool isPaging);

        List<CompanyInfo> GetCompanies(string companyIds);

        List<ClearanceMasterData> GetClearanceProjectDropDownByUserList(string loginName, bool IsinCache);

        bool SaveRequestType(ClearanceRegularProject requestTypeRegular, LeanUserInfo userInfo);

        MasterProject SaveClearanceResource(MasterProject masterProjectDetails, LeanUserInfo userinfo);

        ClearanceRegularProject SaveClearanceResourceRegular(ClearanceRegularProject regularProjectDetails, LeanUserInfo userinfo);

        /// <summary>
        /// Save list of Resource
        /// </summary>
        /// <param name="resourceDetailList"></param>
        /// <param name="projecttype"></param>
        /// <param name="statustype"></param>
        /// <param name="clrProjectId"></param>
        /// <param name="isChangedfromSave"></param>
        /// <param name="newResourceIds"></param>
        /// <returns></returns>
        List<ResourceInfo> TestSaveResource(List<ResourceInfo> resourceDetailList, Constants.ProjectType projecttype, Constants.StatusTypePA statustype, long clrProjectId, bool isChangedfromSave, out List<long> newResourceIds);

        ProjectSearchResult InquiryClearanceProjectSearch(ClearanceProjectInquirySearchCriteria projectSearchCriteria, string loginName);

        List<ClearanceMasterData> GetClearanceProjectRequestTypeDropDownByProjectType(int projectTypeId);

        bool TransferUser(string[] addUserTransferList, int NewOwnerId, LeanUserInfo userInfo);

        List<UserTransfer> FillWorkGroupUserDropDown(LeanUserInfo userInfo, int WorkgroupID);

        ProjectSearchResult SearchProjectForUserTransfer(ClearanceProjectSearchCriteria projectSearchCriteria, int[] _arrstatuslist, LeanUserInfo userInfo);

        List<ClearanceMasterData> GetWorkGroupDropdown(string userLoginName);

        bool RemoveFile(int documentId);

        UploadDocument ReadFile(int documentId);

        bool ReopenProject(long projectId, LeanUserInfo userinfo, int _ReopenedStatusId);

        ClearanceRegularProject GetRegularProjectDetails(int clearanceProjectId);

        ClearanceRegularProject SaveExistingReleases(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);

        ClearanceRegularProject SaveNewReleases(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo);

        ClearanceRegularProject GetReleasesNewExisting(ClearanceRegularProject regularProject);

        bool StatusProjectUpdate(long projectId, int statustype, LeanUserInfo user, DateTime ModifiedDate);

        string getLabelNmForExistingRelease(int labelId);

        int getMusicClassTypeIdForExistingRelease(string musicType);

        List<ClearanceMasterData> GetLabelNameForExistingRelease(List<long> labelIds);

        bool RemoveReleaseProject(ClearanceRelease clearanceRelease);

        ClearanceRelease GetReleasesForR2(long releaseId);

        ClearanceResource GetResourceForR2(long clrProjectResourceId);

        byte UpdatePushR2ItemStatus(List<ClearancePushR2Item> clrPushR2Item);

        List<ClearancePushR2Item> GetR2ItemsPending(int numberOfItems, byte retriesMaxValue);

        ClearancePushR2Item GetR2ItembyID(long repertoireId, int item_type);

        LeanUserInfo GetEmailId(String LoginName);

        string GetCompanyIdPushToR2(List<KeyValuePair<long, string>> Workgroups);

        List<ClearanceRegularProject> GetDivisionPushToR2(string companyIds);

        List<ClearanceRegularProject> GetLabelPushToR2(string divisionIds);

        bool SaveR2ProjectIdLinking(ClearanceRegularProject regularProjectDetails);

        List<string> AutoCompleteSearch(SearchCriteria autoSearch, string userLogin);

        bool SaveReleaseResourceToPushToR2(List<ClearancePushR2Item> clrPushR2Item);

        CompanyInfo GetRequestorCompany(long company_Id);

        List<ClearanceInboxRequest> GetRequestSummaryRequests(string clearanceProjectId, short GridType, int pageSize, int pageNo, string jtSorting, out byte _routingStatus);

        List<ClearanceRoutingDetails> GetRoutingDetails(long routedItemID);

        ClearanceRegularProject GetRegularProjectInfo_RequestType(int projectid);

        ClearanceRegularProject GetRegularProjectReleases(int projectid);

        ClearanceRegularProject GetRegularProjectResources(int projectid);

        #region Downstream Notification

        ClearanceProjectDetails GetClearanceProjectDetails(int clrProjectId);

        string GetXmlfromInterfaceLog(long itemId, byte itemType);

        long InsertIntoInterfaceLog(string xmlString, long itemId, byte itemType);

        long GetClrProjectIdFromProjectCode(string projectCode);

        List<long> GetReleaseFromClrProject(long clrProjectId);

        List<long> GetResourceFromClrProject(long clrProjectId);

        #endregion Downstream Notification

        ProjectSearchResult SearchProjectToAllocateUPC(ClearanceProjectSearchCriteria projectSearchCriteria);

        ProjectSearchResult SearchClearanceUnlockedProjects(ClearanceProjectSearchCriteria projectSearchCriteria);

        bool UpdateProjectLockingStatus(long clrProjectId, long? userId);

        bool UpdateProjectLockingStatusOnClose(long clrProjectId, long? userId);

        string IsProjectUnlocked(long clrProjectId, long currenctUserId);

        bool IsProjectUnlockedByAdmin(long clrProjectId, long currenctUserId);

        List<CompanyInfo> GetDivisions(string divisionIds, long userId, string taskType);

        List<CompanyInfo> GetLabels(long companyId, long divisionId, long userId, string taskType);

        List<HoldbackPeriod> GetRequestType(int ProjectTypeId);

        List<ListItem> GetRCCHandlerList(RoleType roleType);

        void QueueEmailPushToR2(EmailType type, List<ClearancePushR2Item> clrPushR2Item);

        void QueueEmailPushToR2Repertoire(EmailType type, List<ClearancePushR2Item> clrPushR2Item);

        Dictionary<long, long> GetListOfProjectsToComplete();

        List<string> SearchProjectForEmail(string projectReference, LeanUserInfo userInfo);

        string GetHashCodeForUser(string UserLoginName, string AnaTaskType);

        bool SaveUserAnaPermission(R2Authentication lstR2Authentication, string UserLoginName);

        List<ClearanceAdminCompany> GetCompanyListFromANA(long userId, string taskType, string companyName);

        List<ClearanceAdminCompany> AutoCompleteCreateSearchProject(SearchCriteria autoSearch, long userId);

        string IsProjectLocked(long clrProjectId, long currenctUserId);

        long GetTerritoryIdRequestorCompany(long RequestorCompanyId);

        List<ClearanceMasterData> AutoCompleteSearchLabels(SearchCriteria autoSearch, long userId);

        ClearanceRelease GetPackageDetailForRelease(ClearanceRelease clrRelease);

        bool UpdateRemovedResourceArtist(long resourceId, List<ArtistInfo> artistList, LeanUserInfo userInfo);

        bool UpdateRemovedReleaseArtist(long releaseId, List<ArtistInfo> artistList, LeanUserInfo userInfo);

        bool? GetProjectReleaseType(long clrProjectId);
    }
}