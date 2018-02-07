/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : IProjectData.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : GCS Team
 * Created on   : 08-12-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks

***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/

using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceInboxManager
    {
        ClearanceInboxSearchResult SaveInboxFilters(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria);

        string GetInboxFilters(LeanUserInfo userInfo, RoleGroup roleGroup);

        List<ListItem> GetRccHandlers(LeanUserInfo userInfo);

        List<ListItem> GetRequestors(LeanUserInfo userInfo);

        Dictionary<long, Dictionary<long, string>> GetUserWorkgroups(LeanUserInfo userInfo);

        ClearanceInboxData GetInboxData(LeanUserInfo userInfo, RoleGroup roleGroup);

        ClearanceInboxSearchResult GetInboxSearchResult(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria);

        ClearanceInboxSearchResult ManageInboxFolders(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxFolder clearanceInboxFolder, FolderAction folderAction);

        ClearanceInboxSearchResult ManageInboxProjects(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxProject clearanceInboxProject);

        void UpdateProjectReadStatus(LeanUserInfo userInfo, ClearanceInboxProject clearanceInboxProject);

        ClearanceInboxSearchResult SaveInboxFolder(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxFolder clearanceInboxFolder);

        ClearanceInboxSearchResult DeleteUnsubmittedProjects(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxFolder clearanceInboxFolder);

        long GetUserIdFromUserName(string userName);

        List<ClearanceInboxFolder> GetFolders();

        ClearanceInboxFolder GetFolderById(long folderId);

        List<ClearanceInboxDispatch> GetDispatchWorkgroups(long workGroupId);

        ClearanceInboxProjectDetail GetInboxProjectDetail(RoleGroup roleGroup, LeanUserInfo userInfo, long folderId, long clearanceProjectId, long workgroupId);

        List<ClearanceInboxRequest> GetInboxProjectRequests(RoleGroup roleGroup, LeanUserInfo userInfo, long clearanceProjectId, long workgroupId, long folderId, int pageSize, int pageNo, String sortField, byte gridType);

        void UpdateRequestAssignedToUser(LeanUserInfo userInfo, ClearanceInboxRequest clearanceInboxRequest);

        void UpdateRequestReviewComment(LeanUserInfo userInfo, ClearanceInboxRequest clearanceInboxRequest);

        void UpdateRequestAssignedTo_ReviewComment(LeanUserInfo userInfo, ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool selectAllAcrossPages, byte gridType, long userId, string userName, bool isAssignedToEnabled, bool isCommentMultipleEnabled, string commentMultiple);

        ClearanceInboxSearchResult PerformRequestAction(LeanUserInfo userInfo, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool selectAllAcrossPages, byte gridType);

        List<ContractDetails> ClearanceContractSearch(ContractDetails contractSearch, LeanUserInfo userInformation);

        List<ContractDetails> GetResourceArtistDetail(long resourceId,List<long> routedItemId, LeanUserInfo userInformation);

        ClearanceInboxResourceHistory GetResourceHistory(LeanUserInfo userInfo, long ResourceId, int startIndex, int pagesize, string sortingField);

        List<ClearanceRejectReason> GetRejectReasons();

        void AddRejectReason(ClearanceRejectReason clearanceRejectData, string userLoginName);

        void DeleteRejectReason(long reasonId, string userLoginName);

        void SetNotificationStatus(RoleGroup roleGroupId, long projectId, RoutingNotificationEnum notificatonType, LeanUserInfo userInfo);

        ContractInfo CreateClearanceContract(ContractDetails contractDetails, LeanUserInfo userInfor);

        void SaveRCCHandler(long ProjectId, long UserId, LeanUserInfo userInfo);

        bool SaveManageContract(List<KeyValuePair<long, bool>> contractIdList, long resourceId, long routedItemId, string userName);

        string GenerateEmail(long clrProjectId, string emailType, LeanUserInfo userInfo, List<long> resourcesId, bool selectAllAcrossPages, byte gridType, RoleGroup roleGroup, ClearanceInboxRequestAction clearanceInboxRequestaction);
    }
}
