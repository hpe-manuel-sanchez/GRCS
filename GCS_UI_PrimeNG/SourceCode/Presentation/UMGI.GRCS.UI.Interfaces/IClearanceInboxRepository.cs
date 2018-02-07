using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceInboxRepository
    {
        List<KeyValuePair<byte, string>> RoleGroups { get; }

        KeyValuePair<byte, string> PreferredRoleGroup { get; }

        bool DisplayArtistConsentFolder { get; }

        long GetFolderToExpandByDefault(RoleGroup roleGroup, List<ClearanceInboxFolder> folders);

        ClearanceInboxSearchResult SaveInboxFilters(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel);

        IClearanceInboxModel GetInboxData(RoleGroup roleGroup);
        
        List<ListItem> GetRccHandlers();
        
        List<ListItem> GetRequestors();

        Dictionary<long, Dictionary<long, string>> GetUserWorkgroups();

        ClearanceInboxSearchResult GetInboxSearchResult(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel);

        ClearanceInboxSearchResult ManageInboxFolders(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder, FolderAction folderAction);

        ClearanceInboxSearchResult ManageInboxProjects(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxProject clearanceInboxProject);
        
        void UpdateProjectReadStatus(ClearanceInboxProject clearanceInboxProject);

        ClearanceInboxSearchResult SaveInboxFolder(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder);

        ClearanceInboxSearchResult DeleteUnsubmittedProjects(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder);

        List<ClearanceInboxFolder> GetFolders();

        ClearanceInboxFolder GetFolderById(long folderId);

        List<ListItem> RejectReasons();
        List<ClearanceInboxDispatch> DispatchWorkgroups(long workGroupId);
        ClearanceInboxProjectDetail GetInboxProjectDetail(RoleGroup roleGroup, long folderId, long clearanceProjectId, long workgroupId);
        void UpdateRequestAssignedToUser(ClearanceInboxRequest clearanceInboxRequest);
        void UpdateRequestReviewComment(ClearanceInboxRequest clearanceInboxRequest);
        void UpdateRequestAssignedTo_ReviewComment(ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool isSelectedAllAcrossPages, byte gridType, long userId, string userName, bool isAssignedToEnabled, bool isCommentMultipleEnabled, string commentMultiple);
        ClearanceInboxSearchResult PerformRequestAction(IClearanceInboxModel clearanceInboxModel, ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool selectAllAcrossPages, byte gridType);
        Dictionary<string, string> UserWorkgroups();

        InboxContractSearch ClearanceContractSearch(ContractDetails searchContract, LeanUserInfo userInformation);
        List<ContractDetails> GetResourceArtistDetail(long resourceId,List<long> routedItemId, LeanUserInfo userInformation);
        ClearanceInboxResourceHistory GetResourceHistory(long ResourceId, int startIndex, int pagesize, string sortingField);
        void AddRejectReason(ClearanceRejectReason clearanceRejectData, string userLoginName);
        void DeleteRejectReason(long reasonId, string userLoginName);
        ClearanceRejectReasonList GetRejectReasons();
        List<ClearanceInboxRequest> GetInboxProjectRequests(RoleGroup roleGroup, long clearanceProjectId, long workgroupId, long folderId, int pageSize, int pageNo, String sortField, byte gridType);
        void SetNotificationStatus(RoleGroup roleGroup, long projectId, RoutingNotificationEnum notificationType);

        void SaveRCCHandler(long ProjectId, long UserId);
        ContractInfo CreateClearanceContract(ContractDetails contractDetail, LeanUserInfo userInfo);
        bool SaveContractResourceLinking(List<KeyValuePair<long,bool>> contractList, long resourceId, long routedItemList, LeanUserInfo userInfo);
        string GenerateEmail(long clrProjectId, string emailType, LeanUserInfo userInfo, List<long> resourcesId, bool selectAllAcrossPages, byte gridType, RoleGroup roleGroup, ClearanceInboxRequestAction clearanceInboxRequestaction = null);
        bool SaveContractResourceLinkingSelectedRequests(List<KeyValuePair<long, bool>> contractList, string resourceIds, LeanUserInfo userInfo);
        List<ContractDetails> GetResourceArtistDetailSelectedResources(string ResourceIds, LeanUserInfo userInformation);
    }
}
