/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : ClearanceProjectData.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : GCS Team
 * Created on   : 12-08-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks
 *
 *
***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/


using System;
using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IClearanceInbox
    {
        [OperationContract(Name = "SaveInboxFilters")]
        ClearanceInboxSearchResult SaveInboxFilters(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria);

        [OperationContract(Name = "GetInboxFilters")]
        string GetInboxFilters(LeanUserInfo userInfo, RoleGroup roleGroup);

        [OperationContract(Name = "GetRccHandlers")]
        List<ListItem> GetRccHandlers(LeanUserInfo userInfo);

        [OperationContract(Name = "GetRequestors")]
        List<ListItem> GetRequestors(LeanUserInfo userInfo);

        [OperationContract(Name = "GetUserWorkgroups")]
        Dictionary<long, Dictionary<long, string>> GetUserWorkgroups(LeanUserInfo userInfo);

        [OperationContract(Name = "GetInboxData")]
        ClearanceInboxData GetInboxData(LeanUserInfo userInfo, RoleGroup roleGroup);

        [OperationContract(Name = "GetInboxSearchResult")]
        ClearanceInboxSearchResult GetInboxSearchResult(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria);

        [OperationContract(Name = "ManageInboxFolders")]
        ClearanceInboxSearchResult ManageInboxFolders(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxFolder clearanceInboxFolder, FolderAction folderAction);

        [OperationContract(Name = "ManageInboxProjects")]
        ClearanceInboxSearchResult ManageInboxProjects(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria,  ClearanceInboxProject clearanceInboxProject);

        [OperationContract(Name = "UpdateProjectReadStatus")]
        void UpdateProjectReadStatus(LeanUserInfo userInfo, ClearanceInboxProject clearanceInboxProject);

        [OperationContract(Name = "SaveInboxFolder")]
        ClearanceInboxSearchResult SaveInboxFolder(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxFolder clearanceInboxFolder);

        [OperationContract(Name = "DeleteUnsubmittedProjects")]
        ClearanceInboxSearchResult DeleteUnsubmittedProjects(LeanUserInfo userInfo, RoleGroup roleGroup, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxFolder clearanceInboxFolder);

        [OperationContract(Name = "GetUserIdFromUserName")]
        long GetUserIdFromUserName(string userName);

        [OperationContract(Name = "GetFolders")]
        List<ClearanceInboxFolder> GetFolders();

        [OperationContract(Name = "GetFolderById")]
        ClearanceInboxFolder GetFolderById(long folderId);

        [OperationContract(Name = "GetUserDispatchWorkgroups")]
        List<ClearanceInboxDispatch> GetDispatchWorkgroups(long workGroupId);

        [OperationContract(Name = "GetInboxProjectDetail")]
        ClearanceInboxProjectDetail GetInboxProjectDetail(RoleGroup roleGroup,LeanUserInfo userInfo, long folderId, long clearanceProjectId, long workgroupId);

        [OperationContract(Name = "GetInboxProjectRequests")]
        List<ClearanceInboxRequest> GetInboxProjectRequests(RoleGroup roleGroup, LeanUserInfo userInfo, long clearanceProjectId, long workgroupId, long folderId, int pageSize, int pageNo, String sortField, byte gridType);

        [OperationContract(Name = "UpdateRequestAssignedToUser")]
        void UpdateRequestAssignedToUser(LeanUserInfo userInfo, ClearanceInboxRequest clearanceInboxRequest);

        [OperationContract(Name = "UpdateRequestReviewComment")]
        void UpdateRequestReviewComment(LeanUserInfo userInfo, ClearanceInboxRequest clearanceInboxRequest);

        [OperationContract(Name = "UpdateRequestAssignedTo_ReviewComment")]
        void UpdateRequestAssignedTo_ReviewComment(LeanUserInfo userInfo, ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool selectAllAcrossPages, byte gridType, long userId, string userName, bool isAssignedToEnabled, bool isCommentMultipleEnabled, string commentMultiple);

        [OperationContract(Name = "PerformRequestAction")]
        ClearanceInboxSearchResult PerformRequestAction(LeanUserInfo userInfo, ClearanceInboxFilterCriteria clearanceInboxFilterCriteria, ClearanceInboxSearchCriteria clearanceInboxSearchCriteria, ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool selectAllAcrossPages, byte gridType);

        [OperationContract(Name = "ClearanceContractSearch")]
        List<ContractDetails> ClearanceContractSearch(ContractDetails contractSearch, LeanUserInfo userInformation);

        [OperationContract(Name = "GetResourceArtistDetail")]
        List<ContractDetails> GetResourceArtistDetail(long resourceId,List<long> routedItemId, LeanUserInfo userInformation);

        [OperationContract(Name = "GetResourceArtistDetailSelectedResources")]
        List<ContractDetails> GetResourceArtistDetailSelectedResources(string ResourceIds, LeanUserInfo userInformation);

        [OperationContract(Name = "GetResourceHistory")]
        ClearanceInboxResourceHistory GetResourceHistory(LeanUserInfo userInfo, long ResourceId, int startIndex, int pagesize, string sortingField);

        [OperationContract(Name = "GetRejectReasons")]
        List<ClearanceRejectReason> GetRejectReasons();

        [OperationContract(Name = "AddRejectReason")]
        void AddRejectReason(ClearanceRejectReason clearanceRejectData, string userLoginName);

        [OperationContract(Name = "DeleteRejectReason")]
        void DeleteRejectReason(long reasonId, string userLoginName);

        [OperationContract(Name = "SetNotificationStatus")]
        void SetNotificationStatus(RoleGroup roleGroupId, long projectId, RoutingNotificationEnum notificatonType, LeanUserInfo userInfo);

        [OperationContract(Name ="CreateClearanceContract")]
        ContractInfo CreateClearanceContract(ContractDetails contractDetails, LeanUserInfo userInfor);

        [OperationContract(Name  ="SaveContractDetail")]
        bool SaveManageContract(List<KeyValuePair<long, bool>> contractIdList, long resourceId, long routedItemId, LeanUserInfo userInfo);

        [OperationContract(Name = "SaveContractDetailSelectedRequests")]
        bool SaveContractDetailSelectedRequests(List<KeyValuePair<long, bool>> contractIdList, string resourceIds, LeanUserInfo userInfo);

        [OperationContract(Name = "SaveRCCHandler")]
        void SaveRCCHandler(long ProjectId, long UserId, LeanUserInfo userInfo);

        [OperationContract(Name = "GenerateEmail")]
        string GenerateEmail(long clrProjectId, string emailType, LeanUserInfo userInfo, List<long> resourcesId, bool selectAllAcrossPages, byte gridType, RoleGroup roleGroup, ClearanceInboxRequestAction clearanceInboxRequestaction);

    }
}



