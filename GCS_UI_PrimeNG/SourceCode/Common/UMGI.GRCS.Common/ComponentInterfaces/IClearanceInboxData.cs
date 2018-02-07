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
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceInboxData
    {
        void SaveInboxFilters(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria);

        string GetInboxFilters(LeanUserInfo userInfo, RoleGroup roleGroup);

        List<ListItem> GetRccHandlers(LeanUserInfo userInfo);

        List<ListItem> GetRequestors(LeanUserInfo userInfo);

        Dictionary<long, Dictionary<long, string>> GetUserWorkgroups(LeanUserInfo userInfo);

        ClearanceInboxSearchResult GetInboxSearchResult(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria);

        void ManageInboxFolders(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFolder clearanceInboxFolder, FolderAction folderAction);

        void ManageInboxProjects(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxProject clearanceInboxProject);

        void UpdateProjectReadStatus(LeanUserInfo userInfo, ClearanceInboxProject clearanceInboxProject);

        void SaveInboxFolder(LeanUserInfo userInfo, ClearanceInboxFolder clearanceInboxFolder);

        void DeleteUnsubmittedProjects(LeanUserInfo userInfo, ClearanceInboxFolder clearanceInboxFolder);

        long GetUserIdFromUserName(string userName);

        List<ClearanceInboxFolder> GetFolders();

        ClearanceInboxFolder GetFolderById(long folderId);

        List<ClearanceInboxDispatch> GetDispatchWorkgroups(long workGroupId);

        ClearanceInboxProjectDetail GetInboxProjectDetail(RoleGroup roleGroup, LeanUserInfo userInfo, long folderId, long clearanceProjectId, long workgroupId);

        List<ClearanceInboxRequest> GetInboxProjectRequests(RoleGroup roleGroup, LeanUserInfo userInfo, long clearanceProjectId, long workgroupId, long folderId, int pageSize, int pageNo, String sortField, byte gridType);

        void UpdateRequestAssignedToUser(LeanUserInfo userInfo, ClearanceInboxRequest clearanceInboxRequest);

        void UpdateRequestReviewComment(LeanUserInfo userInfo, ClearanceInboxRequest clearanceInboxRequest);

        void UpdateRequestAssignedTo_ReviewComment(LeanUserInfo userInfo, ClearanceInboxRequestAction clearanceInboxRequestAction);

        Dictionary<long, string> GetResourceArtistDetail(long resourceId);

        ClearanceInboxResourceHistory GetResourceHistory(long resourceId, int startIndex, int pagesize, string sortingField);

        List<ClearanceRejectReason> GetRejectReasons();

        void AddRejectReason(ClearanceRejectReason clearanceRejectData, string userLoginName);

        void DeleteRejectReason(long reasonId, string userLoginName);

        void SetNotificationStatus(RoleGroup roleGroupId, long projectId, RoutingNotificationEnum notificatonType, LeanUserInfo userInfo);

        void SaveRCCHandler(long ProjectId, long UserId, LeanUserInfo userInfo);
        List<long> GetResourceContractDetail(List<long> routedItemId);

        ClearanceRoutedProject RCCAdminRouteAction(LeanUserInfo userInfo, ClearanceInboxRequestAction clearanceInboxRequestAction, ClearanceRoutedProject clearanceRoutedProject);
        ClearanceRoutedProject RoutedItemDetails(ClearanceRoutedProject clearanceRoutedProject);
        List<long> EmailContentMasterProject(LeanUserInfo userInfo, long clearanceprojectId);

    }
}
