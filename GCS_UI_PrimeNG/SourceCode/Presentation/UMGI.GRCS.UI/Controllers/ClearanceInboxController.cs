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

using Syncfusion.Mvc.Grid;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.Enumerations;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.ViewModels.ClearanceInbox;
using WorkgroupEntities = UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using Constant = UMGI.GRCS.UI.Utilities.Constants;
using Constants = UMGI.GRCS.BusinessEntities.Constants.Constants;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.Core.Utilities.Helper;

namespace UMGI.GRCS.UI.Controllers
{
    public partial class ClearanceInboxController : BaseController
    {
        private IClearanceInboxModel _clearanceInboxModel;
        ClearanceInboxModel clearanceInboxModel = new ClearanceInboxModel();
        private IClearanceInboxRepository _clearanceInboxRepository;
        readonly IGlobalRepository _globalRepository;
        private byte _GridType;
        private byte _ProjectType;

        public ClearanceInboxController(IClearanceInboxRepository clearanceInboxRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory, IGlobalRepository globalRepository)
        {
            try
            {
                _clearanceInboxRepository = clearanceInboxRepository;
                SessionWrapper = sessionWrapper;
                LoggerFactory = logFactory;
                _globalRepository = globalRepository;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get ActionMethod to load the UserPreferences Screen
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPreferences()
        {
            LoggerFactory.LogWriter.MethodStart();
            return View("UserPreferences");
        }

        /// <summary>
        /// Get ActionMethod to load the Manage Reject Reason Page
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageRejectReason()
        {
            LoggerFactory.LogWriter.MethodStart();
            PermissionCheckNdRedirect(GcsTasks.CreateParentWorkgroup);
            ClearanceRejectReasonList clearanceRejectReason = new ClearanceRejectReasonList();
            ClearanceRejectModel modelObject = new ClearanceRejectModel();

            clearanceRejectReason = _clearanceInboxRepository.GetRejectReasons();
            if (clearanceRejectReason != null)
            {
                modelObject.RejectReasonList = clearanceRejectReason.clearanceRejectReasonList;
            }
            LoggerFactory.LogWriter.MethodExit();
            return View("ManageRejectReason", modelObject);
        }

        /// <summary>
        /// Post ActionMethod for the Manage Reject Reason Page
        /// </summary>
        /// <param name="sequenceNum"></param>
        /// <param name="comments"></param>
        /// <param name="isMarktng"></param>
        /// <param name="isLegal"></param>
        /// <param name="isUMGI"></param>
        [HttpPost]
        public void ManageRejectReason(int sequenceNum, string comments, bool isMarktng, bool isLegal, bool isUMGI)
        {
            LoggerFactory.LogWriter.MethodStart();
            ClearanceRejectReason clearanceRejectObject = new ClearanceRejectReason();
            clearanceRejectObject.IsLegal = isLegal;
            clearanceRejectObject.IsMarktng = isMarktng;
            clearanceRejectObject.IsUMGI = isUMGI;
            clearanceRejectObject.Sequence_No = sequenceNum;
            clearanceRejectObject.RejectReason = comments;
            string userLoginName = SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
            _clearanceInboxRepository.AddRejectReason(clearanceRejectObject, userLoginName);
            LoggerFactory.LogWriter.MethodExit();
        }

        /// <summary>
        /// Method to update the Reject Reason through Edit mode
        /// </summary>
        /// <param name="reasonId"></param>
        /// <param name="sequenceNum"></param>
        /// <param name="comments"></param>
        /// <param name="isMarktng"></param>
        /// <param name="isLegal"></param>
        /// <param name="isUMGI"></param>
        /// <param name="modifiedDttm"></param>
        public void UpdateManagePredefinedRejectionReason(long reasonId, int sequenceNum, string comments, bool isMarktng, bool isLegal, bool isUMGI, DateTime modifiedDttm)
        {
            LoggerFactory.LogWriter.MethodStart();
            ClearanceRejectReason userUpdatedReason = new ClearanceRejectReason();
            userUpdatedReason.ReasonId = reasonId;
            userUpdatedReason.IsLegal = isLegal;
            userUpdatedReason.IsMarktng = isMarktng;
            userUpdatedReason.IsUMGI = isUMGI;
            userUpdatedReason.Sequence_No = sequenceNum;
            userUpdatedReason.RejectReason = comments;
            userUpdatedReason.Modified_Dttm = modifiedDttm;
            string userLoginName = SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
            _clearanceInboxRepository.AddRejectReason(userUpdatedReason, userLoginName);
            LoggerFactory.LogWriter.MethodExit();
        }

        /// <summary>
        /// Method to Delete Reject Reason
        /// </summary>
        /// <param name="reasonId"></param>
        public void DeleteRejectionReason(long reasonId)
        {
            LoggerFactory.LogWriter.MethodStart();
            string userLoginName = SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
            _clearanceInboxRepository.DeleteRejectReason(reasonId, userLoginName);
            LoggerFactory.LogWriter.MethodExit();
        }

        [HttpPost]
        public ActionResult PerformRequestAction(ClearanceInboxModel clearanceInboxModel, ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool selectAllAcrossPages, byte gridType, InboxViewModel.ColumnSetting[] columnSetting)
        {
            ClearanceInboxSearchResult SearchResult = new ClearanceInboxSearchResult();
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (selectAllAcrossPages)
                {
                    SearchResult = _clearanceInboxRepository.PerformRequestAction(clearanceInboxModel, clearanceInboxRequestAction, roleGroup, selectAllAcrossPages, gridType);
                    if (SearchResult.ErrorMsg != null)
                    {
                        if (SearchResult.ErrorMsg.Contains("Concurrency Exception :") || SearchResult.ErrorMsg.Contains("Project is locked by"))
                        {
                            ViewBag.ConcurrencyError = SearchResult.ErrorMsg;
                        }
                    }
                }
                else
                {
                    clearanceInboxRequestAction = DeserializeRequestActions(clearanceInboxRequestAction);
                    SearchResult = _clearanceInboxRepository.PerformRequestAction(clearanceInboxModel, clearanceInboxRequestAction, roleGroup, selectAllAcrossPages, gridType);
                    if (SearchResult.ErrorMsg != null)
                    {
                        if (SearchResult.ErrorMsg.Contains("Concurrency Exception :") || SearchResult.ErrorMsg.Contains("Project is locked by"))
                        {
                            ViewBag.ConcurrencyError = SearchResult.ErrorMsg;
                        }
                    }
                }
                LoggerFactory.LogWriter.MethodExit();

                return RefreshLeftPanel(roleGroup, clearanceInboxModel, SearchResult, columnSetting);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);

                if (ex.Message.Contains("Concurrency Exception :") || ex.Message.Contains("Project is locked by"))
                {
                    ViewBag.ConcurrencyError = ex.Message;
                    return RefreshLeftPanel(roleGroup, clearanceInboxModel, SearchResult, columnSetting);
                }
                else
                {
                    ViewBag.Error = ex.Message;
                    return RefreshLeftPanel(roleGroup, clearanceInboxModel, SearchResult, columnSetting);
                }
            }
        }

        /// <summary>
        /// Method to Get RIght Hand Panel oF Requestor Grid
        /// </summary>
        /// <param name="clrProjectId"></param>
        /// /// <param name="workGroupId"></param>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetInboxProjectDetail(PagingParams args, RoleGroup roleGroup, ClearanceInboxRequestAction clearanceInboxRequestAction, byte gridType)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (args == null)
            {
                string viewName = string.Empty;
                ClearanceInboxModel clrInboxModel = new ClearanceInboxModel();
                ClearanceInboxProjectDetail inboxProjectDetails = new ClearanceInboxProjectDetail();
                ClearanceInboxFolder inboxFolder = new ClearanceInboxFolder();
                try
                {

                    if (roleGroup == RoleGroup.Requestor)
                    {
                        inboxProjectDetails = _clearanceInboxRepository.GetInboxProjectDetail(roleGroup, clearanceInboxRequestAction.FolderId, clearanceInboxRequestAction.ProjectId, clearanceInboxRequestAction.WorkgroupId);
                        ViewData["TblRequestorResourceGridData"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == false).ToList();
                        ViewData["TotalRecCountResourceGrid"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == false).Select(r => r.TotalRecordCount).FirstOrDefault();
                        ViewData["TblRequestorTrackGridData"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == true).ToList();
                        ViewData["TotalRecCountTrackGrid"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == true).Select(r => r.TotalRecordCount).FirstOrDefault();

                        viewName = Constants.RightPanelRequestorPartialView;
                    }
                    else if (roleGroup == RoleGroup.Reviewer)
                    {
                        var userWorkgroups = new Dictionary<string, string>();

                        inboxFolder = _clearanceInboxRepository.GetFolderById(clearanceInboxRequestAction.FolderId);

                        var RejectReasons = _clearanceInboxRepository.GetRejectReasons();
                        if (clearanceInboxRequestAction.RoleName.Contains("UMGI"))
                        {
                            var rReasons = RejectReasons.clearanceRejectReasonList.Where(x => x.IsUMGI == true);
                            ViewBag.RejectReasons = rReasons.Select(i => new SelectListItem { Value = Convert.ToString(i.ReasonId), Text = i.RejectReason, Selected = false }).ToList();
                        }
                        else if (clearanceInboxRequestAction.RoleName.Contains("Marketing") || clearanceInboxRequestAction.RoleName.Contains("Local"))
                        {
                            var rReasons = RejectReasons.clearanceRejectReasonList.Where(x => x.IsMarktng == true);
                            ViewBag.RejectReasons = rReasons.Select(i => new SelectListItem { Value = Convert.ToString(i.ReasonId), Text = i.RejectReason, Selected = false }).ToList();
                        }
                        else if (clearanceInboxRequestAction.RoleName.Contains("Legal"))
                        {
                            var rReasons = RejectReasons.clearanceRejectReasonList.Where(x => x.IsLegal == true);
                            ViewBag.RejectReasons = rReasons.Select(i => new SelectListItem { Value = Convert.ToString(i.ReasonId), Text = i.RejectReason, Selected = false }).ToList();
                        }

                        ViewBag.DispatchWorkgroups = _clearanceInboxRepository.DispatchWorkgroups(clearanceInboxRequestAction.WorkgroupId);

                        inboxProjectDetails = _clearanceInboxRepository.GetInboxProjectDetail(roleGroup, clearanceInboxRequestAction.FolderId, clearanceInboxRequestAction.ProjectId, clearanceInboxRequestAction.WorkgroupId);
                        clrInboxModel.TasksList = GetSpecifiedRoles(clearanceInboxRequestAction.RoleName, roleGroup, clrInboxModel, inboxFolder);
                        inboxProjectDetails.AvailableActions = GetActions(clrInboxModel);
                        userWorkgroups = _clearanceInboxRepository.UserWorkgroups();
                        
                        if (userWorkgroups != null)
                        {
                            var AssignedTo = userWorkgroups.Where(x => x.Key == Convert.ToString(clearanceInboxRequestAction.WorkgroupId)).ToList();
                            ViewBag.AssignedTo = AssignedTo[0].Value;
                        }

                        if (clearanceInboxRequestAction.RoleName == Constants.UMGIMarketingReviewer)
                        {
                            var details = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == false).ToList();
                            if (details.Count > 0)
                            {
                                ViewBag.ReviewComments = details.Select(i => i.Comment).FirstOrDefault();
                            }
                            else
                            {
                                details = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == true).ToList();
                                if (details.Count > 0)
                                {
                                    ViewBag.ReviewComments = details.Select(i => i.Comment).FirstOrDefault();
                                }
                            }
                        }

                        ViewData["TblReviewerResourceGridData"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == false).ToList();
                        ViewData["TotalRecCountResourceGrid"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == false).Select(r => r.TotalRecordCount).FirstOrDefault();
                        ViewData["TblReviewerTrackGridData"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == true).ToList();
                        ViewData["TotalRecCountTrackGrid"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == true).Select(r => r.TotalRecordCount).FirstOrDefault();

                        clrInboxModel.projectDetails = inboxProjectDetails;
                        viewName = Constants.RightPanelReviewerPartialView;
                        inboxProjectDetails.RoutingStatus = 3;
                    }
                    else if (roleGroup == RoleGroup.RCCAdmin)
                    {
                        inboxFolder = _clearanceInboxRepository.GetFolderById(clearanceInboxRequestAction.FolderId);

                        ViewBag.UserId = SessionWrapper.CurrentUserInfo.UserId;

                        inboxProjectDetails = _clearanceInboxRepository.GetInboxProjectDetail(roleGroup, clearanceInboxRequestAction.FolderId, clearanceInboxRequestAction.ProjectId, clearanceInboxRequestAction.WorkgroupId);
                        clrInboxModel.TasksList = GetSpecifiedRoles(clearanceInboxRequestAction.RoleName, roleGroup, clrInboxModel, inboxFolder);
                        inboxProjectDetails.AvailableActions = GetActions(clrInboxModel);
                        ViewData["TblRCCAdminResourceGridData"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == false).ToList();
                        ViewData["TotalRecCountRCCAdminResourceGrid"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == false).Select(r => r.TotalRecordCount).FirstOrDefault();
                        ViewData["TblRCCAdminTrackGridData"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == true).ToList();
                        ViewData["TotalRecCountRCCAdminTrackGrid"] = inboxProjectDetails.Requests.Where(r => r.IsExistingReleaseRequest == true).Select(r => r.TotalRecordCount).FirstOrDefault();

                        clrInboxModel.projectDetails = inboxProjectDetails;

                        viewName = Constants.RightPanelRccAdminPartialView;
                        inboxProjectDetails.RoutingStatus = 3;
                    }

                    if (clearanceInboxRequestAction.FolderId == 9)
                    {
                        inboxProjectDetails.RoutingStatus = 3;
                    }

                    inboxProjectDetails.RoleName = clearanceInboxRequestAction.RoleName;
                    inboxProjectDetails.FolderId = clearanceInboxRequestAction.FolderId;

                    clrInboxModel.projectDetails = inboxProjectDetails;
                    LoggerFactory.LogWriter.MethodExit();

                    return PartialView(viewName, clrInboxModel);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    LoggerFactory.LogWriter.Error(Category.UI, ex);
                    return PartialView(viewName, clrInboxModel);
                }
            }
            else
            {
                var engine = new GridHtmlActionResult<ClearanceInboxRequest>();
                engine = MaintainPagingGrid(args, Convert.ToInt32(clearanceInboxRequestAction.ProjectId), Convert.ToInt32(clearanceInboxRequestAction.WorkgroupId), Convert.ToInt32(clearanceInboxRequestAction.FolderId), gridType, roleGroup, clearanceInboxRequestAction.RoleName, clearanceInboxRequestAction.ProjectType);
                LoggerFactory.LogWriter.MethodExit();
                return engine;
            }
        }

        private Dictionary<GcsTasks, bool> GetSpecifiedRoles(string roleName, RoleGroup roleGroup, ClearanceInboxModel clrInboxModel, ClearanceInboxFolder inboxFolder)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                clrInboxModel.TasksList = new Dictionary<GcsTasks, bool>();
                if (roleName == Constants.LocalLabelReviewer)
                {
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToRequestLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToProjectLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewHistory, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewReInstateRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevConditionallyApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReject, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevDispatch, true);
                    if (inboxFolder.FolderId != (long)General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevArtistConsent, true);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevUndoArtistConsent, true);
                    }
                    clrInboxModel.TasksList.Add(GcsTasks.RevRouteToRCCAdmin, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, false);
                }
                else if (roleName == Constants.UMGIMarketingReviewer)
                {
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToRequestLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToProjectLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewHistory, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewReInstateRequest, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevConditionallyApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReject, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevDispatch, true);
                    if (inboxFolder.FolderId != (long)General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevArtistConsent, false);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevUndoArtistConsent, true);
                    }
                    clrInboxModel.TasksList.Add(GcsTasks.RevRouteToRCCAdmin, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, true);
                }
                else if (roleName == Constants.NationalMarketingReviewer)
                {
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToRequestLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToProjectLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewHistory, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewReInstateRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevConditionallyApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReject, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevDispatch, true);
                    if (inboxFolder.FolderId != (long)General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevArtistConsent, true);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevUndoArtistConsent, true);
                    }
                    clrInboxModel.TasksList.Add(GcsTasks.RevRouteToRCCAdmin, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, false);
                }
                else if (roleName == Constants.NationalLegalReviewer)
                {
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToRequestLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToProjectLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewHistory, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewReInstateRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevConditionallyApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReject, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevDispatch, true);
                    if (inboxFolder.FolderId != (long)General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevArtistConsent, true);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevUndoArtistConsent, true);
                    }
                    clrInboxModel.TasksList.Add(GcsTasks.RevRouteToRCCAdmin, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, false);
                }
                else if (roleName == Constants.InternationalMarketingReviewer)
                {
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToRequestLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToProjectLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewHistory, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewReInstateRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevConditionallyApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReject, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevDispatch, true);
                    if (inboxFolder.FolderId != (long)General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevArtistConsent, true);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevUndoArtistConsent, true);
                    }
                    clrInboxModel.TasksList.Add(GcsTasks.RevRouteToRCCAdmin, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, false);
                }
                else if (roleName == Constants.InternationalLegalReviewer)
                {
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToRequestLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToProjectLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewHistory, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewReInstateRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevConditionallyApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReject, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevDispatch, true);
                    if (inboxFolder.FolderId != (long)General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevArtistConsent, true);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevUndoArtistConsent, true);
                    }
                    clrInboxModel.TasksList.Add(GcsTasks.RevRouteToRCCAdmin, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, false);
                }
                else if (roleName == Constants.UMGIGlobalClearance)
                {
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToRequestLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevAssignedToProjectLevel, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewHistory, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevViewReInstateRequest, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevConditionallyApprove, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevReject, true);
                    clrInboxModel.TasksList.Add(GcsTasks.RevDispatch, true);
                    if (inboxFolder.FolderId != (long)General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevArtistConsent, false);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevUndoArtistConsent, true);
                    }
                    clrInboxModel.TasksList.Add(GcsTasks.RevRouteToRCCAdmin, false);
                    clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, false);
                }
                else if (roleName.ToUpper() == Constants.RCCADMIN)
                {
                    if (inboxFolder.FolderId == (long) General.InboxFolder.OneStop)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, true);
                        clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, false);
                        clrInboxModel.TasksList.Add(GcsTasks.RouteOneStopRequest, true);
                        clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, true);
                        clrInboxModel.TasksList.Add(GcsTasks.RevApprove, true);
                        clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, false);
                        clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, false);
                    }
                    else
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsProjectLevel, false);
                        clrInboxModel.TasksList.Add(GcsTasks.RevReviewCommentsRequest, true);
                        clrInboxModel.TasksList.Add(GcsTasks.RouteOneStopRequest, false);
                        clrInboxModel.TasksList.Add(GcsTasks.RevProjectLevelAction, false);
                        clrInboxModel.TasksList.Add(GcsTasks.RevApprove, false);
                        clrInboxModel.TasksList.Add(GcsTasks.RevViewContracts, true);
                        clrInboxModel.TasksList.Add(GcsTasks.RevManageContract, true);
                    }
                    if ((inboxFolder.FolderId == (long)General.InboxFolder.Orphan))
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.RouteOrphanRequest, true);
                    }
                    else
                    {
                        if (inboxFolder.FolderId == (long) General.InboxFolder.OneStop)
                        {
                            clrInboxModel.TasksList.Add(GcsTasks.RouteOrphanRequest, false);
                        }
                        else
                        {
                            clrInboxModel.TasksList.Add(GcsTasks.RouteOrphanRequest, true);
                        }
                    }
                }

                if (roleGroup == RoleGroup.Reviewer)
                {

                    if (inboxFolder.FolderName.ToUpper() != Enum.GetName(typeof(General.InboxFolder),General.InboxFolder.Research).ToUpper()  &&
                        inboxFolder.FolderName.ToUpper() != EnumExtensions.GetDescription(General.InboxFolder.InternalReview).ToUpper() &&
                        inboxFolder.FolderName.ToUpper() != EnumExtensions.GetDescription(General.InboxFolder.SideArtistSample).ToUpper() &&
                        inboxFolder.FolderId != (long) General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToResearchFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToInternalReviewFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToSideArtistSample, true);
                    }
                    else if (inboxFolder.FolderName.ToUpper() == Enum.GetName(typeof(General.InboxFolder), General.InboxFolder.Research).ToUpper() && inboxFolder.IsSystemFolder == true)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.UndoMoveToResearchFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToInternalReviewFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToSideArtistSample, true);
                    }
                    else if (inboxFolder.FolderName.ToUpper() == EnumExtensions.GetDescription(General.InboxFolder.InternalReview).ToUpper() && inboxFolder.IsSystemFolder == true)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.UndoMoveToInternalReviewFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToResearchFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToSideArtistSample, true);
                    }
                    else if (inboxFolder.FolderName.ToUpper() == EnumExtensions.GetDescription(General.InboxFolder.SideArtistSample).ToUpper() && inboxFolder.IsSystemFolder == true)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.UndoMoveToSideArtistSample, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToResearchFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToInternalReviewFolder, true);
                    }
                    else if (inboxFolder.FolderId == (long) General.InboxFolder.ArtistConsent)
                    {
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToResearchFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToInternalReviewFolder, true);
                        clrInboxModel.TasksList.Add(GcsTasks.MoveToSideArtistSample, true);
                        clrInboxModel.TasksList.Remove(GcsTasks.RevArtistConsent);
                    }
                }
                LoggerFactory.LogWriter.MethodExit();
                return clrInboxModel.TasksList;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Paramterized Paging Method to Get grid events of Requestor grids
        /// </summary>
        /// /// <param name="PagingParams"></param>
        /// /// <param name="clrProjectId"></param>
        /// /// <param name="workGroupId"></param>
        /// <param name="folderId"></param>
        /// /// <param name="gridType"></param>      
        public GridHtmlActionResult<ClearanceInboxRequest> MaintainPagingGrid(PagingParams args, int clrProjectId, int workGroupId, int folderId, byte gridType, RoleGroup rolegroup, string roleName, byte projectType)
        {
            LoggerFactory.LogWriter.MethodStart();

            ClearanceInboxProjectDetail inboxProjectDetails = new ClearanceInboxProjectDetail();
            var engine = new GridHtmlActionResult<ClearanceInboxRequest>();
            _ProjectType = projectType;
            long totalRowCount = 0;
            //For paging, refreshing of the Resource, Tracks grids
            var userInfo = new LeanUserInfo();
            userInfo = getUserInfo();
            try
            {
                if (rolegroup == RoleGroup.Requestor)
                {
                    var request = (RequestType)Convert.ToInt32(args.RequestType);

                    if (args.StartIndex < 0) args.StartIndex = 0;

                    var filter = new PagingBase { StartIndex = args.StartIndex, PageSize = args.PageSize };

                    if (args.SortDescriptors != null && (request == RequestType.Sorting || request == RequestType.Paging || request == RequestType.Refresh))
                    {
                        SortDescriptor firstOrDefault = args.SortDescriptors.FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            filter.SortField = firstOrDefault.ColumnName;
                            filter.IsAscendingOrder = firstOrDefault.SortDirection.ToString() == "Ascending";
                            if (filter.IsAscendingOrder == true)
                                filter.SortField = filter.SortField + " " + "DESC";
                            else
                                filter.SortField = filter.SortField + " " + "ASC";
                        }
                    }
                    int pageNo = (args.StartIndex / args.PageSize) + 1;

                    //End of paging

                    List<ClearanceInboxRequest> inboxRequests = new List<ClearanceInboxRequest>();
                    inboxRequests = _clearanceInboxRepository.GetInboxProjectRequests(rolegroup, clrProjectId, workGroupId, folderId, args.PageSize, pageNo, filter.SortField, gridType);

                    if (inboxRequests.Count == 0 && args.StartIndex > 0)
                    {
                        if (args.StartIndex - args.PageSize >= 0)
                        {
                            args.StartIndex = args.StartIndex - args.PageSize;
                        }
                        else
                        {
                            args.StartIndex = 0;
                        }

                        pageNo = (args.StartIndex / args.PageSize) + 1;

                        inboxRequests = _clearanceInboxRepository.GetInboxProjectRequests(rolegroup, clrProjectId, workGroupId, folderId, args.PageSize, pageNo, filter.SortField, gridType);
                    }

                    if (inboxRequests.Count > 0)
                    {
                        totalRowCount = inboxRequests[0].TotalRecordCount;
                    }

                    _GridType = gridType;
                    engine = inboxRequests.GridActions<ClearanceInboxRequest>(totalRowCount) as GridHtmlActionResult<ClearanceInboxRequest>;
                    engine.GridModel.QueryCellInfo = onQueryCellActionForRequestorGrid;
                }
                else if (rolegroup == RoleGroup.Reviewer || rolegroup == RoleGroup.RCCAdmin)
                {
                    var userWorkgroups = new Dictionary<string, string>();

                    if (rolegroup == RoleGroup.Reviewer)
                    {
                        var RejectReasons = _clearanceInboxRepository.GetRejectReasons();
                        if (roleName.Contains("UMGI"))
                        {
                            var rReasons = RejectReasons.clearanceRejectReasonList.Where(x => x.IsUMGI == true);
                            ViewBag.RejectReasons = rReasons.Select(i => new SelectListItem { Value = Convert.ToString(i.ReasonId), Text = i.RejectReason, Selected = false }).ToList();
                        }
                        else if (roleName.Contains("Marketing"))
                        {
                            var rReasons = RejectReasons.clearanceRejectReasonList.Where(x => x.IsMarktng == true);
                            ViewBag.RejectReasons = rReasons.Select(i => new SelectListItem { Value = Convert.ToString(i.ReasonId), Text = i.RejectReason, Selected = false }).ToList();
                        }
                        else if (roleName.Contains("Legal"))
                        {
                            var rReasons = RejectReasons.clearanceRejectReasonList.Where(x => x.IsLegal == true);
                            ViewBag.RejectReasons = rReasons.Select(i => new SelectListItem { Value = Convert.ToString(i.ReasonId), Text = i.RejectReason, Selected = false }).ToList();
                        }
                        ViewBag.DispatchWorkgroups = _clearanceInboxRepository.DispatchWorkgroups(workGroupId);
                    }

                    ClearanceInboxFolder inboxFolder = new ClearanceInboxFolder();
                    inboxFolder = _clearanceInboxRepository.GetFolders().Where(i => i.FolderId == folderId).First();

                    clearanceInboxModel.TasksList = GetSpecifiedRoles(roleName, rolegroup, clearanceInboxModel, inboxFolder);
                    inboxProjectDetails.AvailableActions = GetActions(clearanceInboxModel);
                    if (rolegroup == RoleGroup.Reviewer)
                    {
                        userWorkgroups = _clearanceInboxRepository.UserWorkgroups();
                        if (userWorkgroups != null)
                        {
                            var AssignedTo = userWorkgroups.Where(x => x.Key == Convert.ToString(workGroupId)).ToList();
                            ViewBag.AssignedTo = AssignedTo[0].Value;
                        }
                    }
                    var request = (RequestType)Convert.ToInt32(args.RequestType);

                    if (args.StartIndex < 0) args.StartIndex = 0;

                    var filter = new PagingBase { StartIndex = args.StartIndex, PageSize = args.PageSize };

                    if (args.SortDescriptors != null && (request == RequestType.Sorting || request == RequestType.Paging || request == RequestType.Refresh))
                    {
                        SortDescriptor firstOrDefault = args.SortDescriptors.FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            filter.SortField = firstOrDefault.ColumnName;
                            filter.IsAscendingOrder = firstOrDefault.SortDirection.ToString() == "Ascending";
                            if (filter.IsAscendingOrder == true)
                                filter.SortField = filter.SortField + " " + "DESC";
                            else
                                filter.SortField = filter.SortField + " " + "ASC";
                        }
                    }
                    int pageNo = (args.StartIndex / args.PageSize) + 1;

                    //End of paging

                    List<ClearanceInboxRequest> inboxRequests = new List<ClearanceInboxRequest>();
                    inboxRequests = _clearanceInboxRepository.GetInboxProjectRequests(rolegroup, clrProjectId, workGroupId, folderId, args.PageSize, pageNo, filter.SortField, gridType);

                    if (inboxRequests.Count == 0 && args.StartIndex > 0)
                    {
                        if (args.StartIndex - args.PageSize >= 0)
                        {
                            args.StartIndex = args.StartIndex - args.PageSize;
                        }
                        else
                        {
                            args.StartIndex = 0;
                        }

                        pageNo = (args.StartIndex / args.PageSize) + 1;

                        inboxRequests = _clearanceInboxRepository.GetInboxProjectRequests(rolegroup, clrProjectId, workGroupId, folderId, args.PageSize, pageNo, filter.SortField, gridType);
                    }

                    if (inboxRequests.Count > 0)
                    {
                        totalRowCount = inboxRequests[0].TotalRecordCount;
                    }

                    _GridType = gridType;
                    engine = inboxRequests.GridActions<ClearanceInboxRequest>(totalRowCount) as GridHtmlActionResult<ClearanceInboxRequest>;
                    if (rolegroup == RoleGroup.Reviewer)
                    {
                        engine.GridModel.QueryCellInfo = onQueryCellActionForReviewerGrid;
                    }
                    else
                    {
                        engine.GridModel.QueryCellInfo = onQueryCellActionForRCCAdminGrid;
                    }
                }
                LoggerFactory.LogWriter.MethodExit();

                return engine;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.Error = ex.Message;
                ViewData["TotalRecCountResourceGrid"] = totalRowCount;
                return engine;
            }
        }

        /// <summary>
        /// Method for desinging the custom columns of  Requestor grids
        /// </summary>    
        public void onQueryCellActionForRequestorGrid(GridTableCell<ClearanceInboxRequest> cell)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (cell.TableCellType == GridTableCellType.RecordFieldCell || cell.TableCellType == GridTableCellType.AlternateRecordFieldCell)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string RoutedItemDate = serializer.Serialize(cell.Data.ModifiedDate);
                string requestDate = serializer.Serialize(cell.Data.ModifiedDateRequest);
                string Beginslash = "\"\\/";
                string EndSlash = "\\/\"";
                RoutedItemDate = RoutedItemDate.ToString().Replace(Beginslash, "");
                RoutedItemDate = RoutedItemDate.ToString().Replace(EndSlash, "");
                requestDate = requestDate.ToString().Replace(Beginslash, "");
                requestDate = requestDate.ToString().Replace(EndSlash, "");

                string Value = "";

                switch (cell.Column.MappingName)
                {
                    case "Action":
                        if (cell.Data.ApprovalStatus == "Waiting")
                        {
                            string value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId + "~" + cell.Data.WorkgroupId + "~" + 7 + "~" + RoutedItemDate + "~" + cell.Data.ModifiedUser + "~" + requestDate; // 8 for remind
                            string valueForRemind = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId + "~" + cell.Data.WorkgroupId + "~" + 8 + "~" + RoutedItemDate + "~" + cell.Data.ModifiedUser + "~" + requestDate;
                            string cancelbutton = string.Empty;
                            if (_GridType == 1)
                                cancelbutton = "<input type=\"button\" onclick=\"actionOnRequest('" + value + "');\"  class=\"plbutton\" style=\"vertical-align:middle;width:95px !important; margin:2px !important;\" value=\"Cancel\"/>";
                            cell.Text = cancelbutton + "<br/><input type=\"button\" id=\"Remind\" onclick=\"actionOnRequest('" + valueForRemind + "');\"  class=\"plbutton\" style=\"vertical-align:middle;width:95px !important; margin:2px !important;\" value=\"Remind\"/>";
                            cell.HtmlAttributes["style"] = "vertical-align:middle !important";
                        }
                        else if (cell.Data.ApprovalStatus == "Cancelled" && _GridType == 1)
                        {
                            string value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId + "~" + cell.Data.WorkgroupId + "~" + 12 + "~" + RoutedItemDate + "~" + cell.Data.ModifiedUser + "~" + requestDate;
                            cell.Text = "<input type=\"button\" onclick=\"actionOnRequest('" + value + "');\"  class=\"plbutton\" style=\"vertical-align:middle;width:95px !important; margin:2px !important;\" value=\"Re-Instate\"/>";
                            cell.HtmlAttributes["style"] = "vertical-align:middle !important";
                        }
                        else if (cell.Data.ApprovalStatus == "Rejected")
                        {
                            string value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId + "~" + cell.Data.WorkgroupId + "~" + 9 + "~" + RoutedItemDate + "~" + cell.Data.ModifiedUser + "~" + requestDate;
                            cell.Text = "<input type=\"button\" id=\"Reapply\" onclick=\"openReapplyCommentDialog('" + value + "');\"  class=\"plbutton\" style=\"vertical-align:middle;width:95px !important; margin:2px !important;\" value=\"Re-Apply\"/>";
                            cell.HtmlAttributes["style"] = "vertical-align:middle !important";
                        }
                        else if (cell.Data.ApprovalStatus == "Approved" || cell.Data.ApprovalStatus == "Conditionally Approved" && _GridType == 1)// to get the project type as this is for master only
                        {
                            if (_ProjectType == 1)
                            {
                                string value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId + "~" + cell.Data.WorkgroupId + "~" + 10 + "~" + RoutedItemDate + "~" + cell.Data.ModifiedUser + "~" + requestDate;
                                cell.Text = "<input type=\"button\" onclick=\"actionOnRequest('" + value + "');\"  class=\"plbutton\" style=\"vertical-align:middle;width:95px !important; margin:2px !important;\" value=\"Exclude\"/>";
                                cell.HtmlAttributes["style"] = "vertical-align:middle !important";
                            }
                        }
                        else if (cell.Data.ApprovalStatus == "Excluded" && _GridType == 1)// to get the project type as this is for master only
                        {
                            string value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId + "~" + cell.Data.WorkgroupId + "~" + 11 + "~" + RoutedItemDate + "~" + cell.Data.ModifiedUser + "~" + requestDate;
                            cell.Text = "<input type=\"button\" onclick=\"actionOnRequest('" + value + "');\"  class=\"plbutton\" style=\"vertical-align:middle;width:95px !important; margin:2px !important;\" value=\"Include\"/>";
                            cell.HtmlAttributes["style"] = "vertical-align:middle !important";
                        }
                        break;
                    case "Image":// for showing the image when request is fully approved
                        switch (cell.Data.ApprovalStatusId)
                        {
                            case 2: cell.Text = "<img src=\"/GCS/Images/ClearanceInbox/approved.png\" alt=\"Approved\" title=\"Approved\"/>";
                                break;
                            case 4: cell.Text = "<img src=\"/GCS/Images/ClearanceInbox/conditionallyApproved.png\" alt=\"Conditionally Approved\" title=\"Conditionally Approved\"/>";
                                break;
                            case 5: cell.Text = "<img src=\"/GCS/Images/ClearanceInbox/rejected.png\" alt=\"Rejected\" title=\"Rejected\"/>";
                                break;
                            case 7: cell.Text = "";
                                break;
                            case 8: cell.Text = "";
                                break;
                            case 10: cell.Text = "";
                                break;
                            case 12: cell.Text = "<img src=\"/GCS/Images/ClearanceInbox/routingStopped.png\" alt=\"Routing Stopped\" title=\"Routing Stopped\"/>";
                                break;
                        }

                        if (!(cell.Text.Equals(string.Empty)))
                        {
                            cell.HtmlAttributes["style"] = "vertical-align:middle !important;";
                            cell.Column.Visible = true;
                        }
                        break;
                    case "LastRoutingComment":// for showing the actions based on Approval Status
                        Value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId;
                        long comment = cell.Data.CommentCount;
                        string commentText = cell.Data.LastRoutingComment;
                        cell.HtmlAttributes["Title"] = cell.Data.LastRoutingComment;
                        string link = "<a href=\"#\" title=" + comment + "comment(s) available style=\"font-weight:bold;color:black;\" OnClick=\"return routingInfo(" + "'" + Value + "'" + ")\">Routing Details</a>";
                        cell.Text = commentText + "<div></div>" + link;
                        break;
                    case "PrimaryArtistName":
                        cell.HtmlAttributes["Title"] = cell.Data.PrimaryArtistName ?? string.Empty;
                        break;
                    case "ResourceTitle":
                        string newFieldValue = cell.Data.ResourceTitle;

                        if (cell.Data != null && !string.IsNullOrEmpty(cell.Data.VersionTitle))
                            newFieldValue = cell.Data.ResourceTitle + " [" + cell.Data.VersionTitle + "]";

                        cell.Text = newFieldValue;
                        cell.HtmlAttributes["Title"] = newFieldValue;
                        break;
                }
            }
            LoggerFactory.LogWriter.MethodExit();
        }

        /// <summary>
        /// Method for desinging the custom columns of  Requestor grids
        /// </summary>    
        public void onQueryCellActionForReviewerGrid(GridTableCell<ClearanceInboxRequest> cell)
        {
            LoggerFactory.LogWriter.MethodStart();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (cell.TableCellType == GridTableCellType.RecordFieldCell || cell.TableCellType == GridTableCellType.AlternateRecordFieldCell)
            {
                if (cell.Data.IsDisabled)
                {
                    cell.HtmlAttributes.Add("disabled", true);
                }

                string textArea = "";
                string value = "";
                string link = "";
                KeyValuePair<GcsTasks, bool> taskVal;

                switch (cell.Column.MappingName)
                {
                    case "KeyRoutedItemRequest":// for showing the actions based on Approval Status
                        string KeyRoutedItemRequest = "";
                        foreach (KeyValuePair<string, string> kv in cell.Data.KeyRoutedItemRequest)
                        {
                            if (KeyRoutedItemRequest == "")
                            {
                                KeyRoutedItemRequest = kv.Key + "," + kv.Value;
                            }
                            else
                            {
                                KeyRoutedItemRequest = KeyRoutedItemRequest + "|" + kv.Key + "," + kv.Value;
                            }
                        }
                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevProjectLevelAction).SingleOrDefault();
                        var workgroupid = cell.Data.WorkgroupId;
                        var projectId = cell.Data.ClearanceProjectId;
                        var GridType = "";
                        if (_GridType == 1)
                        {
                            GridType = "ResourceGrid";
                        }
                        else
                        {
                            GridType = "TrackGrid";
                        }
                        if (taskVal.Value)
                        {
                            string Chkbox = "<input type=\"checkbox\" value=" + KeyRoutedItemRequest + "^" + serializer.Serialize(cell.Data.ModifiedDateRequest) + "^" + serializer.Serialize(cell.Data.ModifiedDate) + " onclick=\"oncheckboxCheck(this, " + "'" + workgroupid + "','" + projectId + "','" + GridType + "','Reviewer');\" class=\"hidden\"/>";
                            cell.Text = Chkbox;
                            cell.Column.Visible = false;
                        }
                        else
                        {
                            string Chkbox = "<input type=\"checkbox\" value=" + KeyRoutedItemRequest + "^" + serializer.Serialize(cell.Data.ModifiedDateRequest) + "^" + serializer.Serialize(cell.Data.ModifiedDate) + " onclick=\"oncheckboxCheck(this, " + "'" + workgroupid + "','" + projectId + "','" + GridType + "','Reviewer');\" class=\"visible\" />";
                            cell.Text = Chkbox;
                            cell.Column.Visible = true;
                        }

                        cell.HtmlAttributes["style"] = "vertical-align:middle !important";

                        break;


                    case "LastRoutingComment":// for showing the actions based on Approval Status
                        value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId;
                        long comment = cell.Data.CommentCount;
                        string commentText = cell.Data.LastRoutingComment;
                        link = "<div></div><a href=\"#\" title=" + comment + "comment(s) available style=\"font-weight:bold;color:black;\" OnClick=\"return routingInfo(" + "'" + value + "'" + ")\">RoutingDetails</a>";
                        cell.Text = commentText + link;
                        cell.HtmlAttributes["Title"] = cell.Data.LastRoutingComment;
                        break;
                    case "ContractSummary":// for Binding the Contracts
                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevViewContracts).SingleOrDefault();
                        if (taskVal.Value)
                        {
                            string summaryText = string.Join(",", cell.Data.ContractSummary.ToArray());
                            summaryText = summaryText.ToString().Replace('~', ' ');
                            summaryText = summaryText.ToString().Replace('=', ' ');
                            cell.Text = summaryText;
                            cell.HtmlAttributes["Title"] = summaryText;
                        }
                        else
                        {
                            cell.Column.Visible = false;
                        }
                        break;
                    case "AssignedToUserId":
                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevAssignedToRequestLevel).SingleOrDefault();
                        if (taskVal.Value)
                        {
                            value = "<div class='assignedToUserName' >" + cell.Data.AssignedToUserName + "<div>"
                                        + "<input type=\"hidden\" class=\"assignedToUserId\" value='" + cell.Data.AssignedToUserId + "'></input>";
                            cell.Text = value;
                        }
                        else
                        {
                            cell.Column.Visible = false;
                        }
                        break;
                    case "Resource History":// for showing Resource History Popup
                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevViewHistory).SingleOrDefault();
                        if (taskVal.Value)
                        {
                            string ResourceHistory = "<img src=\"/GCS/Images/ClearanceInbox/revResourceHistory.png\" onclick=\"ResourceHistory(" + cell.Data.ResourceId + ");\" title=\"Review History\"/>";
                            cell.Text = ResourceHistory;
                        }
                        else
                        {
                            cell.Column.Visible = false;
                        }
                        break;
                    case "ReinstatedRequests":// for showing Resource History Popup
                        if (cell.Data.IsReinstated)
                        {
                            taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevViewReInstateRequest).SingleOrDefault();
                            if (taskVal.Value)
                            {
                                string ReinstatedRequest = "<img src=\"/GCS/Images/ClearanceInbox/reinstated.png\" title=\"Reinstated\"/>";
                                cell.Text = ReinstatedRequest;
                                cell.HtmlAttributes["style"] = "vertical-align:middle !important";
                            }
                            else
                            {
                                cell.Column.Visible = false;
                            }
                        }
                        else
                        {
                            cell.Column.Visible = false;
                        }
                        break;
                    case "Comment":// for showing Resource History Popup
                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevReviewCommentsRequest).SingleOrDefault();
                        if (taskVal.Value)
                        {
                            string TextArea = "<TEXTAREA ROWS=2 style=\"width:125px !important;\" class=\"textareaclass\" onblur=\"return isValueChangedCss($(this));\">" + cell.Data.Comment + "</TEXTAREA>";
                            cell.Text = TextArea;
                        }
                        else
                        {
                            cell.Column.Visible = false;
                        }
                        break;
                    case "PrimaryArtistName":
                        cell.HtmlAttributes["Title"] = cell.Data.PrimaryArtistName ?? string.Empty;
                        break;
                    case "ResourceTitle":
                        string newFieldValue = cell.Data.ResourceTitle;

                        if (cell.Data != null && !string.IsNullOrEmpty(cell.Data.VersionTitle))
                            newFieldValue = cell.Data.ResourceTitle + " [" + cell.Data.VersionTitle + "]";

                        cell.Text = newFieldValue;
                        cell.HtmlAttributes["Title"] = newFieldValue;
                        break;
                    case "ModifiedDateRequest":// for showing/hiding configuratio column
                        textArea = "<input type=\"textbox\" class=\"ModifiedRequestDate\" value='" + serializer.Serialize(cell.Data.ModifiedDateRequest) + "'></input>";
                        cell.Text = textArea;
                        break;
                    case "ModifiedDate":// for showing/hiding configuratio column
                        textArea = "<input type=\"textbox\" class=\"ModifiedRoutedItemDate\" value='" + serializer.Serialize(cell.Data.ModifiedDate) + "'></input>";
                        cell.Text = textArea;
                        break;
                    case "WorkgroupId":// for showing/hiding configuratio column
                        string workgroup = "<input type=\"hidden\" class=\"WorkgroupId\" value='" + cell.Data.WorkgroupId + "'></input>";
                        cell.Text = workgroup;
                        break;
                    case "ResourceId":
                        string resourceId = "<input type=\"hidden\" class=\"resourceId\" value='" + cell.Data.ResourceId + "'></input>";
                        cell.Text = resourceId;
                        break;
                }
            }
            else if (cell.TableCellType == GridTableCellType.ColumnHeaderCell)
            {
                if (cell.Column.MappingName == "KeyRoutedItemRequest")
                {
                    cell.HtmlAttributes["style"] = "border-right:0px !important";
                }
            }

            LoggerFactory.LogWriter.MethodExit();
        }

        public void onQueryCellActionForRCCAdminGrid(GridTableCell<ClearanceInboxRequest> cell)
        {
            LoggerFactory.LogWriter.MethodStart();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (cell.TableCellType == GridTableCellType.RecordFieldCell || cell.TableCellType == GridTableCellType.AlternateRecordFieldCell)
            {
                if (cell.Data.IsDisabled)
                {
                    cell.HtmlAttributes.Add("disabled", true);
                }

                string contractIds = "";
                string summaryText = "";
                string TextArea = "";
                KeyValuePair<GcsTasks, bool> taskVal;

                switch (cell.Column.MappingName)
                {
                    case "KeyRoutedItemRequest": // for showing the actions based on Approval Status

                        string KeyRoutedItemRequest = "";
                        foreach (KeyValuePair<string, string> kv in cell.Data.KeyRoutedItemRequest)
                        {
                            if (KeyRoutedItemRequest == "")
                            {
                                KeyRoutedItemRequest = kv.Key + "," + kv.Value;
                            }
                            else
                            {
                                KeyRoutedItemRequest = KeyRoutedItemRequest + "|" + kv.Key + "," + kv.Value;
                            }
                        }
                        KeyRoutedItemRequest = KeyRoutedItemRequest + '~' + cell.Data.ResourceId;
                        var workgroupid = cell.Data.WorkgroupId;
                        var projectId = cell.Data.ClearanceProjectId;
                        var GridType = "";
                        if (_GridType == 1)
                        {
                            GridType = "ResourceGrid";
                        }
                        else
                        {
                            GridType = "TrackGrid";
                        }
                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevProjectLevelAction).SingleOrDefault();
                        var taskval1 = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevManageContract).SingleOrDefault();
                        summaryText = string.Join(",", cell.Data.ContractSummary.ToArray());
                        summaryText = summaryText.ToString().Replace('~', ' ');
                        summaryText = summaryText.ToString().Replace('=', ' ');
                        if (taskval1.Value)
                        {
                            foreach (var row in cell.Data.ContractSummary.ToArray())
                            {
                                var contracts = row.ToString().Split('~');
                                foreach (var contract in contracts)
                                {
                                    if (contract.Contains('='))
                                    {
                                        var contracts1 = contract.ToString().Split('=');
                                        if (contracts1 != null)
                                        {
                                            if (contractIds == "")
                                            {
                                                contractIds = contracts1[0];
                                            }
                                            else
                                            {
                                                contractIds = contractIds + ',' + contracts1[0];
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        KeyRoutedItemRequest = KeyRoutedItemRequest + '-' + contractIds;

                        if (taskVal.Value)
                        {
                            string Chkbox = "<input type=\"checkbox\" value=" + KeyRoutedItemRequest + "^" + serializer.Serialize(cell.Data.ModifiedDateRequest) + "^" + serializer.Serialize(cell.Data.ModifiedDate) + " onclick=\"oncheckboxCheck(this, " + "'" + workgroupid + "','" + projectId + "','" + GridType + "','RCCAdmin');\" class=\"hidden\"/>";
                            cell.Text = Chkbox;
                            cell.Column.Visible = false;
                        }
                        else
                        {
                            string Chkbox = "<input type=\"checkbox\" value=" + KeyRoutedItemRequest + "^" + serializer.Serialize(cell.Data.ModifiedDateRequest) + "^" + serializer.Serialize(cell.Data.ModifiedDate) + " onclick=\"oncheckboxCheck(this, " + "'" + workgroupid + "','" + projectId + "','" + GridType + "','RCCAdmin');\" class=\"visible\" />";
                            cell.Text = Chkbox;
                            cell.Column.Visible = true;
                        }
                        cell.HtmlAttributes["style"] = "vertical-align:middle !important";

                        break;
                    case "LastRoutingComment":// for showing the actions based on Approval Status

                        string value = cell.Data.ClearanceProjectId + "~" + cell.Data.RequestId + "~" + cell.Data.RoutedItemId;
                        long comment = cell.Data.CommentCount;
                        string link = "<a href=\"#\" title=" + comment + "comment(s) available style=\"font-weight:bold;color:black;\" OnClick=\"return routingInfo(" + "'" + value + "'" + ")\">Routing Details</a>";
                        cell.Text = cell.Data.LastRoutingComment + "<div></div>" + link;
                        cell.HtmlAttributes["Title"] = cell.Data.LastRoutingComment;

                        break;
                    case "ContractSummary":// for Binding the Contracts

                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevViewContracts).SingleOrDefault();
                        if (taskVal.Value)
                        {
                            var taskVal1 = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevManageContract).SingleOrDefault();
                            summaryText = string.Join(",", cell.Data.ContractSummary.ToArray());
                            summaryText = summaryText.ToString().Replace('~', ' ');
                            summaryText = summaryText.ToString().Replace('=', ' ');

                            if (taskVal1.Value)
                            {
                                foreach (var row in cell.Data.ContractSummary.ToArray())
                                {
                                    var contracts = row.ToString().Split('~');
                                    foreach (var contract in contracts)
                                    {
                                        if (contract.Contains('='))
                                        {
                                            var contracts1 = contract.ToString().Split('=');
                                            if (contracts1 != null)
                                            {
                                                if (contractIds == "")
                                                {
                                                    contractIds = contracts1[0];
                                                }
                                                else
                                                {
                                                    contractIds = contractIds + ',' + contracts1[0];
                                                }
                                            }
                                        }
                                    }
                                }

                                string btnManage = "<a href=\"#\" style=\"font-weight:bold;color:black;\" onclick=\"return ManageContract('" + cell.Data.ResourceId + "','" + contractIds + "','" + cell.Data.RoutedItemId + "');\">Manage</a>";
                                if (contractIds != "")
                                {
                                    cell.Text = summaryText + "<div></div>" + btnManage;
                                }
                                else
                                {
                                    cell.Text = btnManage;
                                }
                            }
                            else
                            {
                                if (cell.Data.ContractId != null)
                                {
                                    cell.Text = summaryText;
                                }
                            }
                            cell.HtmlAttributes["Title"] = summaryText;
                        }
                        else
                        {
                            cell.Column.Visible = false;
                        }

                        break;
                    case "Comment":// for showing Resource History Popup

                        taskVal = clearanceInboxModel.TasksList.Where(x => x.Key == GcsTasks.RevReviewCommentsRequest).SingleOrDefault();
                        if (taskVal.Value)
                        {
                            TextArea = "<TEXTAREA ROWS=2 style=\"width:115px !important;\" class=\"textareaclass\" onblur=\"return isValueChangedCss($(this));\">" + cell.Data.Comment + "</TEXTAREA>";
                            cell.Text = TextArea;
                        }
                        else
                        {
                            cell.Column.Visible = false;
                        }

                        break;
                    case "PrimaryArtistName":
                        cell.HtmlAttributes["Title"] = cell.Data.PrimaryArtistName ?? string.Empty;

                        break;
                    case "ResourceTitle":
                        string newFieldValue = cell.Data.ResourceTitle;

                        if (cell.Data != null && !string.IsNullOrEmpty(cell.Data.VersionTitle))
                            newFieldValue = cell.Data.ResourceTitle + " [" + cell.Data.VersionTitle + "]";

                        cell.Text = newFieldValue;
                        cell.HtmlAttributes["Title"] = newFieldValue;

                        break;
                    case "ModifiedDateRequest":

                        TextArea = "<input type=\"textbox\" class=\"ModifiedRequestDate\" value='" + serializer.Serialize(cell.Data.ModifiedDateRequest) + "'></input>";
                        cell.Text = TextArea;

                        break;
                    case "ModifiedDate":// for showing/hiding configuratio column
                        TextArea = "<input type=\"textbox\" class=\"ModifiedRoutedItemDate\" value='" + serializer.Serialize(cell.Data.ModifiedDate) + "'></input>";
                        cell.Text = TextArea;
                        break;
                    case "ResourceId":
                        string resourceId = "<input type=\"hidden\" class=\"resourceId\" value='" + cell.Data.ResourceId + "'></input>";
                        cell.Text = resourceId;
                        break;
                }
            }
            else if (cell.TableCellType == GridTableCellType.ColumnHeaderCell)
            {
                if (cell.Column.MappingName == "KeyRoutedItemRequest")
                {
                    cell.HtmlAttributes["style"] = "border-right:0px !important";
                }
            }

            LoggerFactory.LogWriter.MethodExit();
        }

        private LeanUserInfo getUserInfo()
        {
            LoggerFactory.LogWriter.MethodStart();
            var userInfo = SessionWrapper.CurrentUserInfo;
            LoggerFactory.LogWriter.MethodExit();

            return new LeanUserInfo
            {
                UserId = userInfo.UserId,
                UserLoginName = userInfo.UserLoginName,
                UserName = userInfo.UserName,
                EmailId = userInfo.EmailId,
                MimicedRccAdminLoginName = userInfo.MimicedRccAdminLoginName
            };
        }

        [HttpPost]
        public JsonResult SetNotificationStatus(RoleGroup roleGroup, byte notificationType, long projectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                RoutingNotificationEnum inboxNotificationType = (RoutingNotificationEnum)Enum.Parse(typeof(RoutingNotificationEnum), notificationType.ToString());
                _clearanceInboxRepository.SetNotificationStatus(roleGroup, projectId, inboxNotificationType);

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResourceHistory(PagingParams args, long ResourceId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (args.GridID == null)
                {
                    ViewBag.ResourceId = ResourceId;
                    ClearanceInboxModel clrInboxModel = new ClearanceInboxModel();
                    clrInboxModel.resourceHistory = new ClearanceInboxResourceHistory();
                    clrInboxModel.resourceHistory.Records = new List<ClearanceInboxResourceHistoryItem>();
                    var clearanceResourceHistoryList = _clearanceInboxRepository.GetResourceHistory(Convert.ToInt64(ResourceId), args.StartIndex, 20, "ProjectTitle ASC");
                    ViewData["TotalRecCount"] = clearanceResourceHistoryList.TotalRecordCount;
                    ViewData["data"] = clearanceResourceHistoryList.Records;

                    LoggerFactory.LogWriter.MethodExit();

                    return PartialView("ResourceHistory", clrInboxModel);
                }
                else
                {
                    var engine = new GridHtmlActionResult<ClearanceInboxResourceHistoryItem>();
                    engine = MaintainResourcHistoryPagingGrid(args, Convert.ToInt64(ResourceId));
                    engine.GridModel.QueryCellInfo = onQueryCellActionForRCCAdminGrid;

                    LoggerFactory.LogWriter.MethodExit();

                    return engine;
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public GridHtmlActionResult<ClearanceInboxResourceHistoryItem> MaintainResourcHistoryPagingGrid(PagingParams args, long ResourceId)
        {
            var engine = new GridHtmlActionResult<ClearanceInboxResourceHistoryItem>();

            try
            {
                LoggerFactory.LogWriter.MethodStart();
                             
                ClearanceInboxResourceHistory clrInboxHistory = new ClearanceInboxResourceHistory();
                List<ClearanceInboxResourceHistoryItem> clrInboxHistoryItems = new List<ClearanceInboxResourceHistoryItem>();

                long totalRowCount = 0;
                //For paging, refreshing of the Resource, Tracks grids
                var userInfo = new LeanUserInfo();
                userInfo = getUserInfo();

                var request = (RequestType)Convert.ToInt32(args.RequestType);

                if (args.StartIndex < 0) args.StartIndex = 0;

                if (request == RequestType.Refresh) args.StartIndex = 0;

                var filter = new PagingBase { StartIndex = args.StartIndex, PageSize = args.PageSize };

                if (args.SortDescriptors != null && (request == RequestType.Sorting || request == RequestType.Paging))
                {
                    SortDescriptor firstOrDefault = args.SortDescriptors.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        filter.SortField = firstOrDefault.ColumnName;
                        filter.IsAscendingOrder = firstOrDefault.SortDirection.ToString() == "Ascending";
                        if (filter.IsAscendingOrder == true)
                            filter.SortField = filter.SortField + " " + "ASC";
                        else
                            filter.SortField = filter.SortField + " " + "DESC";
                    }
                }

                //End of paging

                clrInboxHistory = _clearanceInboxRepository.GetResourceHistory(Convert.ToInt64(ResourceId), args.StartIndex, args.PageSize, filter.SortField);
                totalRowCount = clrInboxHistory.Records.Count;
                engine = clrInboxHistory.Records.GridActions<ClearanceInboxResourceHistoryItem>(totalRowCount) as GridHtmlActionResult<ClearanceInboxResourceHistoryItem>;

                LoggerFactory.LogWriter.MethodExit();

                return engine;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.Error = ex.Message;
                return engine;
            }

        }

        public void onQueryCellActionForRCCAdminGrid(GridTableCell<ClearanceInboxResourceHistoryItem> cell)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (cell.TableCellType == GridTableCellType.RecordFieldCell || cell.TableCellType == GridTableCellType.AlternateRecordFieldCell)
            {
                if (cell.Column.MappingName == "ProjectTitle")
                {
                    cell.HtmlAttributes["Title"] = cell.Data.ProjectTitle;
                }
            }

            LoggerFactory.LogWriter.MethodExit();
        }

        [HttpPost]
        public ActionResult ExportToExcel(string GridData)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (GridData != null)
            {
                TempData["GridData"] = GridData;
            }
            else
            {
                GridData = TempData["GridData"].ToString();
            }
            GridData = GridData.Replace('\'', '\"');
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            List<ClearanceInboxResourceHistoryItem> gridDetails = jsonSerialize.Deserialize<List<ClearanceInboxResourceHistoryItem>>(GridData);
            var data = gridDetails;
            LoggerFactory.LogWriter.MethodExit();

            return data.GridExportToExcel<ClearanceInboxResourceHistoryItem>("GridExcel.xlsx", ExcelVersion.Excel2010);
        }

        /// <summary>
        /// Get available actions based on role 
        /// </summary>
        /// <param name="clrInboxModel"></param>
        /// <returns></returns>
        private List<ListItem> GetActions(ClearanceInboxModel clrInboxModel)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceInboxProjectDetail inboxProjectDetails = new ClearanceInboxProjectDetail();
                inboxProjectDetails.AvailableActions = new List<ListItem>();

                foreach (var task in clrInboxModel.TasksList.Where(i => i.Value == true))
                {
                    if (task.Key == GcsTasks.RevApprove)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "Approve",
                            Value = "1",
                            Order = 2
                        });
                    }
                    else if (task.Key == GcsTasks.RevArtistConsent)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "ArtistConsent",
                            Value = "5",
                            Order = 6
                        });
                    }
                    else if (task.Key == GcsTasks.RevConditionallyApprove)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "ConditionallyApprove",
                            Value = "2",
                            Order = 3
                        });
                    }
                    else if (task.Key == GcsTasks.RevDispatch)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "Dispatch",
                            Value = "4",
                            Order = 5
                        });
                    }
                    else if (task.Key == GcsTasks.RevReject)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "Reject",
                            Value = "3",
                            Order = 4
                        });
                    }
                    else if (task.Key == GcsTasks.RevRouteToRCCAdmin)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "RouteToRCCAdmin",
                            Value = "6",
                            Order = 10
                        });
                    }
                    else if (task.Key == GcsTasks.RouteOneStopRequest)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "Route",
                            Value = "17",
                            Order = 1
                        });
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "ManageContract",
                            Value = "18",
                            Order = 2
                        });
                    }
                    else if (task.Key == GcsTasks.RouteOrphanRequest)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "Route",
                            Value = "17",
                            Order = 1
                        });
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "ManageContract",
                            Value = "18",
                            Order = 2
                        });
                    }
                    else if (task.Key == GcsTasks.MoveToResearchFolder)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "Research",
                            Value = "76",
                            Order = 7
                        });
                    }
                    else if (task.Key == GcsTasks.MoveToInternalReviewFolder)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "InternalReview",
                            Value = "77",
                            Order = 8
                        });
                    }
                    else if (task.Key == GcsTasks.MoveToSideArtistSample)
                    {
                        inboxProjectDetails.AvailableActions.Add(new ListItem
                        {
                            Text = "SideArtist/Sample",
                            Value = "78",
                            Order = 9
                        });
                    }
                }
                LoggerFactory.LogWriter.MethodExit();

                return inboxProjectDetails.AvailableActions.OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Update Request assigned to and Review comments
        /// </summary>
        /// <param name="clearanceInboxRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateRequestAssignedTo_ReviewComment(ClearanceInboxRequestAction clearanceInboxRequestaction, RoleGroup roleGroup, bool isSelectedAllAcrossPages, byte gridType, long userId, string userName, bool isAssignedToEnabled, bool isCommentMultipleEnabled, string commentMultiple)

        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (!isSelectedAllAcrossPages && clearanceInboxRequestaction.Requests.Count() > 0)
                    clearanceInboxRequestaction = DeserializeRequestActions(clearanceInboxRequestaction);

                _clearanceInboxRepository.UpdateRequestAssignedTo_ReviewComment(clearanceInboxRequestaction, roleGroup, isSelectedAllAcrossPages, gridType, userId, userName, isAssignedToEnabled, isCommentMultipleEnabled, commentMultiple);

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = "" });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private ClearanceInboxRequestAction DeserializeRequestActions(ClearanceInboxRequestAction clearanceInboxRequestAction)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var serializer = new JavaScriptSerializer();

                clearanceInboxRequestAction.Requests.All(irows =>
                {
                    var modifiedDateRouted = serializer.Deserialize<DateTime>(irows.ModifiedDateRoutedString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    var modifiedRequestDate = serializer.Deserialize<DateTime>(irows.ModifiedDateRequestString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    irows.ModifiedDate = modifiedDateRouted;
                    irows.ModifiedDateRequest = modifiedRequestDate;
                    return true;
                });

                if (clearanceInboxRequestAction.ProjectModifiedDateString != null)
                {
                    var modifiedDateProject = serializer.Deserialize<DateTime>(clearanceInboxRequestAction.ProjectModifiedDateString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    clearanceInboxRequestAction.ProjectModifiedDate = modifiedDateProject;
                }
                LoggerFactory.LogWriter.MethodExit();
                return clearanceInboxRequestAction;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpPost]
        public JsonResult SaveRCCHandler(long ProjectId, long User_Id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                _clearanceInboxRepository.SaveRCCHandler(ProjectId, User_Id);
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = "" });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public Dictionary<string, string> UserWorkgroups()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return _clearanceInboxRepository.UserWorkgroups();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult MassReminders(ClearanceInboxModel clearanceInboxModel, ClearanceInboxRequestAction clearanceInboxRequestAction)
        {
            LoggerFactory.LogWriter.MethodStart();

            clearanceInboxModel = new ClearanceInboxModel();
            ClearanceInboxFilterCriteria FilterCriteria = new ClearanceInboxFilterCriteria();
            ClearanceInboxSearchCriteria SearchCriteria = new ClearanceInboxSearchCriteria();
            clearanceInboxModel.FilterCriteria = FilterCriteria;
            clearanceInboxModel.SearchCriteria = SearchCriteria;
            clearanceInboxModel.InboxState = new ClearanceInboxState();
            clearanceInboxModel.InboxState.SelectedFolderId = clearanceInboxRequestAction.FolderId;
            clearanceInboxModel.InboxState.SelectedProjectId = clearanceInboxRequestAction.ProjectId;
            clearanceInboxModel.InboxState.FolderSize = clearanceInboxRequestAction.WorkgroupId;
            clearanceInboxModel.projectDetails = new ClearanceInboxProjectDetail();
            clearanceInboxModel.projectDetails.ProjectReferenceNumber = clearanceInboxRequestAction.Comment;

            LoggerFactory.LogWriter.MethodExit();

            return PartialView("MassReminders", clearanceInboxModel);
        }

        public JsonResult MassRemindersWithParam(ClearanceInboxRequestAction clearanceInboxRequestAction)
        {
            LoggerFactory.LogWriter.MethodStart();

            var clearanceInboxModel = new ClearanceInboxModel();
            ClearanceInboxProjectDetail inboxProjectDetails = new ClearanceInboxProjectDetail();
            clearanceInboxModel.InboxState = new ClearanceInboxState();
            clearanceInboxModel.InboxState.SelectedFolderId = clearanceInboxRequestAction.FolderId;
            clearanceInboxModel.InboxState.SelectedProjectId = clearanceInboxRequestAction.ProjectId;
            clearanceInboxModel.InboxState.FolderSize = clearanceInboxRequestAction.WorkgroupId;
            inboxProjectDetails.Requests = _clearanceInboxRepository.GetInboxProjectRequests(RoleGroup.Requestor, clearanceInboxRequestAction.ProjectId, clearanceInboxRequestAction.WorkgroupId, clearanceInboxRequestAction.FolderId, 1000, 1, string.Empty, 0);

            inboxProjectDetails.Requests = inboxProjectDetails.Requests.Where(r => r.ApprovalStatusId == 1 || r.ApprovalStatusId == 11 || r.ApprovalStatusId == 14).ToList();
            clearanceInboxModel.projectDetails = inboxProjectDetails;
            var serializer = new JavaScriptSerializer();
            inboxProjectDetails.Requests.All(i =>
            {
                i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                return true;
            });

            LoggerFactory.LogWriter.MethodExit();

            return Json(new { Result = "OK", Records = inboxProjectDetails.Requests, TotalRecordCount = inboxProjectDetails.Requests.Count });
        }

        [HttpGet]
        public ActionResult CreateClearanceContract()
        {
            LoggerFactory.LogWriter.MethodStart();
            IClearanceInboxModel _iClearanceInboxModel = new ClearanceInboxModel();
            LoggerFactory.LogWriter.MethodExit();

            return View("CreateClearanceContract", _iClearanceInboxModel);
        }

        [HttpPost]
        public ActionResult CreateInboxClearanceContract(long artistId, string artistName, long clearanceCompanyId, string notes, long talentId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                clearanceInboxModel = new ClearanceInboxModel();
                ContractDetails contractDetail = new ContractDetails();
                contractDetail.ArtistId = artistId;
                contractDetail.TalentId = talentId;
                contractDetail.ArtistName = artistName;
                contractDetail.ClearanceCompanyCountryId = contractDetail.UmgSigningCompanyId = clearanceCompanyId;
                contractDetail.RightsTypeId = null; // to be passed as null as per mail from GRS
                contractDetail.ClearingNotes = notes;
                contractDetail.WorkflowStatusId = (int)WorkflowStatus.DataEntry;
                contractDetail.WorkflowStatus = "Data Entry";
                contractDetail.RightsAndRestrictions.AcquisitableRights = new List<ContractRightsAcquired>()
                {
                    new ContractRightsAcquired() {RightAcquiredTypeId=1},
                    new ContractRightsAcquired() {RightAcquiredTypeId=2},
                    new ContractRightsAcquired() {RightAcquiredTypeId=3},
                    new ContractRightsAcquired() {RightAcquiredTypeId=4},
                    new ContractRightsAcquired() {RightAcquiredTypeId=5}
                };

                contractDetail.ContractExploitationRestrictionsList = new List<ContractExploitationRestrictions>()
                {
                    new ContractExploitationRestrictions() {ExploitationTypeId =1,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =2,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =3,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =4,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =5,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =6,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =7,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =8,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =9,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"},
                    new ContractExploitationRestrictions() {ExploitationTypeId =10,ConsentPeriodTypeId=0,RestrictionTypeId=0,RestrictionOptionId = "0"}
                };

                contractDetail.ContractStatus = Constants.PriorityWorkQueueContractStatus;
                contractDetail.ContractStatusId = 3;

                var contractInfo = _clearanceInboxRepository.CreateClearanceContract(contractDetail, getUserInfo());

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = contractInfo });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }

        public ActionResult ClearanceManageCompany()
        {
            LoggerFactory.LogWriter.MethodStart();
            return View(new ClearanceInboxModel());
        }

        public JsonResult SearchCompany(string name, string isacCode, string country, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("name: {0},isacCode: {1},country: {2}", name, isacCode, country));
                jtSorting = "Name ASC";
                int totalRowCount = 0;
                var companyList = _globalRepository.CompanySearch(name, isacCode, country, jtStartIndex, jtPageSize, jtSorting, SessionWrapper.CurrentUserInfo.UserLoginName);
                if (companyList.Count > 0)
                {
                    totalRowCount = companyList[0].PageDetails.TotalRows;
                }
                LoggerFactory.LogWriter.Info("Successfully Retrieved The Company Data");
                LoggerFactory.LogWriter.Debug("Successfully Retrieved The Company Data");
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = companyList.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult ClearanceSearchContract(InboxContractSearch inputContractSearch)
        {
            IClearanceInboxModel _iClearanceInboxModel = new ClearanceInboxModel();
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                _iClearanceInboxModel.ResourceId = inputContractSearch.ResourceId;
                _iClearanceInboxModel.ContractId = !string.IsNullOrEmpty(inputContractSearch.contractId) ? inputContractSearch.contractId.Split(',').Select(long.Parse).ToList() : new List<long>();
                _iClearanceInboxModel.RoutedItemId = inputContractSearch.RoutedItemId.Split(',').Select(long.Parse).ToList();
                _iClearanceInboxModel.ContractsSearch = _clearanceInboxRepository.GetResourceArtistDetail(inputContractSearch.ResourceId, _iClearanceInboxModel.RoutedItemId, getUserInfo());
                
                LoggerFactory.LogWriter.MethodExit();

                return View("ClearanceSearchContract", _iClearanceInboxModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                _iClearanceInboxModel.ErrorMessage = ex.Message;
                return View("ClearanceSearchContract", _iClearanceInboxModel);
            }
        }

        public ActionResult ClearanceSearchContractSelectedRequests(InboxContractSearch inputContractSearch)
        {
            IClearanceInboxModel _iClearanceInboxModel = new ClearanceInboxModel();
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string[] lstRecordsCollection = inputContractSearch.ResourceIds.Split('~');
                List<long> lstContractIds = new List<long>();
                // send only those contracts to the cshtml which are common to all selected resources, 
                // so that contract should be checked only if it is saved in manage contract table, for all selected requests. Otherwise
                // user should get option to check contract and link to all resources.               
                Boolean checkCommoncontracts = true;

                int iCounter = 0;

                foreach (var element in lstRecordsCollection)
                {
                    iCounter++;
                    string[] lstResourceData = element.Split('|');
                    string[] lstContractData = lstResourceData[0].ToString().Split('-');
                    _iClearanceInboxModel.ResourceId = Convert.ToInt64(lstContractData[0].ToString());
                    inputContractSearch.contractId = lstContractData[1].ToString();
                    _iClearanceInboxModel.RoutedItemId = lstResourceData[1].ToString().Split(',').Select(long.Parse).ToList();
                    _iClearanceInboxModel.ContractId = !string.IsNullOrEmpty(inputContractSearch.contractId) ? inputContractSearch.contractId.Split(',').Select(long.Parse).ToList() : new List<long>();

                    if (!string.IsNullOrEmpty(inputContractSearch.contractId))
                    {
                        List<long> lstContractIdsSubsequent = new List<long>();
                        foreach (var objContracts in _iClearanceInboxModel.ContractId)
                        {
                            if (iCounter == 1)
                            {
                                lstContractIds.Add(objContracts);
                            }
                            else
                            {
                                lstContractIdsSubsequent.Add(objContracts);
                            }
                        }
                        if (iCounter > 1)
                            lstContractIds.RemoveAll(i => !lstContractIdsSubsequent.Contains(i));
                    }
                    else
                    {
                        checkCommoncontracts = false;
                    }
                }

                _iClearanceInboxModel.ContractsSearch = _clearanceInboxRepository.GetResourceArtistDetailSelectedResources(inputContractSearch.ResourceIds, getUserInfo());
                _iClearanceInboxModel.ResourceIdSelectedRequests = inputContractSearch.ResourceIds;

                // Checkbox for a contract should be checked only if it is linked to all selected resources, plus saved in clr_manage_contract table for all selected resources.
                if (checkCommoncontracts)
                {
                    _iClearanceInboxModel.ContractId = lstContractIds;
                }
                else
                {
                    _iClearanceInboxModel.ContractId = new List<long>();
                }
                LoggerFactory.LogWriter.MethodExit();

                return View("ClearanceSearchContract", _iClearanceInboxModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);

                _iClearanceInboxModel.ErrorMessage = ex.Message;
                return View("ClearanceSearchContract", _iClearanceInboxModel);
            }
        }

        // This function is being used for contract search, and also for adding newly created contract along with search result. 
        // But in case of create contract, resource id will not be passed as parameter, because this contract is not yet linked to any resource.
        public JsonResult ClearanceSearchContractWithParam(string artistName, string contractingParty, string umgSigningCompanyId, string artistNameInLocalCharacters, string contractStatus, string localContractRefNumber, string rightsType, string clearanceAdminCompany, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null, long contractId = 0, long artistId = 0, string resourceId = null)
        {
            try
            {
                FilterFields filter = new FilterFields { StartIndex = jtStartIndex, PageSize = jtPageSize };

                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("artistName: {0}, contractingParty: {1}, umgSigningCompanyId: {2}, artistNameInLocalCharacters: {3}, contractStatus: {4}, localContractRefNumber: {5}, rightsType: {6}, clearanceAdminCompany: {7}, jtStartIndex: {8}, jtPageSize: {9}, jtSorting: {10}, contractId: {11}, artistId: {12}, resourceId: {13}", artistName, contractingParty, umgSigningCompanyId, artistNameInLocalCharacters, contractStatus, localContractRefNumber, rightsType, clearanceAdminCompany, jtStartIndex, jtPageSize, jtSorting, contractId, artistId, resourceId));

                if (jtSorting != null)
                {
                    var list = jtSorting.Split(' ');
                    if (list.Count() > 1)
                    {
                        filter.SortField = list[0].Trim();
                        filter.IsAscendingOrder = list[1].Trim() == "ASC";
                    }
                }

                var count = !string.IsNullOrEmpty(resourceId) ? resourceId.Count(x => x == '-') : 0;

                if (count == 1)
                {
                    resourceId = resourceId.Replace('-', '~');
                }

                var searchContract = new ContractDetails
                {
                    ArtistName = artistName,
                    ArtistId = artistId,
                    ContractStatus = contractStatus,
                    ContractingParty = contractingParty,
                    ClearanceCompanyCountry = clearanceAdminCompany,
                    ArtistNameInLocalCharacters = artistNameInLocalCharacters,
                    UmgSigningCompany = umgSigningCompanyId,
                    LocalContractRefNumber = localContractRefNumber,
                    ContractId = contractId,
                    RightsTypeName = rightsType,
                    HasSearchCriteria = true,
                    PageDetails = filter,
                    HasPageDetails = true,
                    ResourceId = !string.IsNullOrEmpty(resourceId) ?
                    resourceId.Contains('~') ? 0 : Convert.ToInt64(resourceId) : 0,
                    ResourceIdsArray = !string.IsNullOrEmpty(resourceId) ?
                    resourceId.Contains('~') ? resourceId : string.Empty : string.Empty
                };
                IClearanceInboxModel _iClearanceInboxModel = new ClearanceInboxModel();

                var contractInfo = _clearanceInboxRepository.ClearanceContractSearch(searchContract, getUserInfo());
                _iClearanceInboxModel.ContractsSearch = contractInfo.ContractSearchInfo != null ? contractInfo.ContractSearchInfo : new List<ContractDetails>();
                int totalRows = 0;
                if (contractInfo.ContractSearchInfo != null && contractInfo.ContractSearchInfo.Count > 0)
                {
                    totalRows = contractInfo.ContractSearchInfo.First().PageDetails.TotalRows;
                }

                LoggerFactory.LogWriter.Info("Successfully Retrieved The Contract Data");
                LoggerFactory.LogWriter.Debug("Successfully Retrieved The Contract Data");
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = _iClearanceInboxModel.ContractsSearch.AsQueryable(), TotalRecordCount = totalRows });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult SaveContractResourceLinking(string contractIdList, string resourceId, string routedItemId)
        {
            IClearanceInboxModel _iClearanceInboxModel = new ClearanceInboxModel();
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                long linkedResourceId = resourceId == null || resourceId == "0" ? 0 : Convert.ToInt64(resourceId);

                List<KeyValuePair<long, bool>> contractList = (from keyValuePair in contractIdList.Split(',')
                                                               where keyValuePair.Trim() != string.Empty
                                                               let splittedKeyValuePair = keyValuePair.Split('~')
                                                               select new KeyValuePair<long, bool>(Convert.ToInt64(splittedKeyValuePair[0].Trim()),
                                                                   Convert.ToBoolean(splittedKeyValuePair[1]))).ToList();
                
                List<long> routedItemList = routedItemId.Split(',').Where(s => s != string.Empty).Select(s => Convert.ToInt64(s)).ToList();
                try
                {
                    _clearanceInboxRepository.SaveContractResourceLinking(contractList, linkedResourceId, routedItemList[0], getUserInfo());
                }
                catch (Exception ex)
                {
                    LoggerFactory.LogWriter.Error(Category.UI, ex);
                    _iClearanceInboxModel.ErrorMessage = ex.Message;
                    return View("ClearanceSearchContract", _iClearanceInboxModel);
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = "OK", Records = _iClearanceInboxModel });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Fail", Records = _iClearanceInboxModel });
            }

        }

        public ActionResult SaveContractResourceLinkingSelectedRequests(string contractIdList, string resourceIds)
        {
            IClearanceInboxModel _iClearanceInboxModel = new ClearanceInboxModel();
            string[] lstRecordsCollection = resourceIds.Split('~');

            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<KeyValuePair<long, bool>> contractList = (from keyValuePair in contractIdList.Split(',')
                                                               where keyValuePair.Trim() != string.Empty
                                                               let splittedKeyValuePair = keyValuePair.Split('~')
                                                               select new KeyValuePair<long, bool>(Convert.ToInt64(splittedKeyValuePair[0].Trim()),
                                                                   Convert.ToBoolean(splittedKeyValuePair[1]))).ToList();

                _clearanceInboxRepository.SaveContractResourceLinkingSelectedRequests(contractList, resourceIds, getUserInfo());

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = _iClearanceInboxModel });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Fail", Records = _iClearanceInboxModel });
            }
        }

        [HttpPost]
        public string GenerateEmail(long clrProjectId, string emailType, List<long> resourcesId = null, bool isSelectedAllAcrossPages = false, byte gridType = 0, RoleGroup roleGroup = 0, ClearanceInboxRequestAction clearanceInboxRequestaction = null)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return _clearanceInboxRepository.GenerateEmail(clrProjectId, emailType, getUserInfo(), resourcesId, isSelectedAllAcrossPages, gridType, roleGroup, clearanceInboxRequestaction);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.Error = ex.Message;
                throw;
            }
        }
    }

    public partial class ClearanceInboxController
    {
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ViewBag.RoleGroups = _clearanceInboxRepository.RoleGroups;
                ViewBag.PreferredRoleGroup = _clearanceInboxRepository.PreferredRoleGroup;
                ViewBag.MaxLengthForFolderName = Constant.MaxLengthForFolderName;
                ViewBag.ReviewerSearchCriteriaAssignedTo = Constant.ReviewerSearchCriteriaAssignedTo;
                ViewBag.DefaultFolderSize = Constant.DefaultFolderSize;

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("Index");
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error loading Clearance Inbox." });
            }
        }

        [HttpPost]
        public ActionResult GetInbox(RoleGroup roleGroup)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                _clearanceInboxModel = _clearanceInboxRepository.GetInboxData(roleGroup);

                InboxViewModel clearanceInboxViewModel = new InboxViewModel();
                clearanceInboxViewModel.FolderToExpandByDefault = _clearanceInboxRepository.GetFolderToExpandByDefault(roleGroup, _clearanceInboxModel.SearchResult.Folders);
                clearanceInboxViewModel.Projects = GetProjects(_clearanceInboxModel.SearchResult.Folders);
                clearanceInboxViewModel.SearchCriteria = GetSearchCriteria(_clearanceInboxModel.SearchCriteria);
                clearanceInboxViewModel.FilterCriteria = GetFilterCriteria(_clearanceInboxModel.FilterCriteria, roleGroup);
                clearanceInboxViewModel.State = GetState(_clearanceInboxModel.SearchCriteria.InboxState);

                SetViewBagData(roleGroup);

                return PartialView("Inbox", clearanceInboxViewModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error fetching user Inbox." });
            }
            finally
            {
                LoggerFactory.LogWriter.MethodExit();
            }
        }

        private List<InboxViewModel.InboxProject> GetProjects(List<ClearanceInboxFolder> ClearanceInboxFolders)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                InboxViewModel inbox = new InboxViewModel();
                for (int i = 0; i < ClearanceInboxFolders.Count(); i++)
                {
                    InboxViewModel.InboxProject vmProject;

                    if (ClearanceInboxFolders[i].Projects.Count > 0)
                    {
                        for (int j = 0; j < ClearanceInboxFolders[i].Projects.Count(); j++)
                        {
                            vmProject = new InboxViewModel.InboxProject();
                            vmProject.FolderViewId = i + 1;
                            vmProject.IsSystemFolder = ClearanceInboxFolders[i].IsSystemFolder;
                            vmProject.FolderId = ClearanceInboxFolders[i].FolderId;
                            vmProject.FolderName = ClearanceInboxFolders[i].FolderName;
                            vmProject.CurrentFolderId = ClearanceInboxFolders[i].Projects[j].CurrentFolderId;
                            vmProject.OriginalSystemFolderId = ClearanceInboxFolders[i].Projects[j].OriginalSystemFolderId;
                            vmProject.ClearanceProjectId = ClearanceInboxFolders[i].Projects[j].ClearanceProjectId;
                            vmProject.ProjectTitle = ClearanceInboxFolders[i].Projects[j].ProjectTitle;
                            vmProject.ProjectType = ClearanceInboxFolders[i].Projects[j].ProjectType;
                            vmProject.ProjectTypeId = ClearanceInboxFolders[i].Projects[j].ProjectTypeId;
                            vmProject.ProjectDetail = ClearanceInboxFolders[i].Projects[j].ProjectDetail;
                            vmProject.ProjectReferenceNumber = ClearanceInboxFolders[i].Projects[j].ProjectReferenceNumber;
                            vmProject.ProjectStatusId = ClearanceInboxFolders[i].Projects[j].ProjectStatusId;
                            vmProject.EstimatedSalesUnit = ClearanceInboxFolders[i].Projects[j].EstimatedSalesUnit;
                            vmProject.ReleaseDate = ClearanceInboxFolders[i].Projects[j].ReleaseDate;
                            vmProject.ProjectSubmissionDate = ClearanceInboxFolders[i].Projects[j].ProjectSubmissionDate;
                            vmProject.NotificationRecieved = ClearanceInboxFolders[i].Projects[j].NotificationRecieved;
                            vmProject.RccHandlerId = ClearanceInboxFolders[i].Projects[j].RccHandlerId;
                            vmProject.RccHandlerName = ClearanceInboxFolders[i].Projects[j].RccHandlerName;
                            vmProject.RequestorName = ClearanceInboxFolders[i].Projects[j].RequestorName;
                            vmProject.RequestingCompanyName = ClearanceInboxFolders[i].Projects[j].RequestingCompanyName;
                            vmProject.RequestingCompanyIsoName = ClearanceInboxFolders[i].Projects[j].RequestingCompanyIsoName;
                            vmProject.ThirdPartyCompanyName = ClearanceInboxFolders[i].Projects[j].ThirdPartyCompanyName;
                            vmProject.ThirdPartyCompanyIsoName = ClearanceInboxFolders[i].Projects[j].ThirdPartyCompanyIsoName;
                            vmProject.IsUnread = ClearanceInboxFolders[i].Projects[j].IsUnread;
                            vmProject.IsThirdParty = ClearanceInboxFolders[i].Projects[j].IsThirdParty;
                            vmProject.IsAllRequestReviewed = ClearanceInboxFolders[i].Projects[j].IsAllRequestReviewed;
                            vmProject.AssignedToUserId = ClearanceInboxFolders[i].Projects[j].AssignedToUserId;
                            vmProject.AssignedToUser = ClearanceInboxFolders[i].Projects[j].AssignedToUser;
                            vmProject.RoleId = ClearanceInboxFolders[i].Projects[j].RoleId;
                            vmProject.RoleName = ClearanceInboxFolders[i].Projects[j].RoleName;
                            vmProject.WorkgroupId = ClearanceInboxFolders[i].Projects[j].WorkgroupId;
                            vmProject.WorkGroupName = ClearanceInboxFolders[i].Projects[j].WorkGroupName;
                            vmProject.TotalRecordCount = ClearanceInboxFolders[i].Projects[j].TotalRecordCount;
                            vmProject.CreatedByUser = ClearanceInboxFolders[i].Projects[j].CreatedByUser;
                            vmProject.CreatedDate = ClearanceInboxFolders[i].Projects[j].CreatedDate;
                            vmProject.ModifiedUser = ClearanceInboxFolders[i].Projects[j].ModifiedUser;
                            vmProject.ModifiedDate = ClearanceInboxFolders[i].Projects[j].ModifiedDate;
                            vmProject.ModifiedUserAssignedTo = ClearanceInboxFolders[i].Projects[j].ModifiedUserAssignedTo;
                            vmProject.ModifiedDateAssignedTo = ClearanceInboxFolders[i].Projects[j].ModifiedDateAssignedTo;
                            vmProject.ShowInformation = true;

                            inbox.Projects.Add(vmProject);
                        }
                    }
                    else
                    {
                        vmProject = new InboxViewModel.InboxProject();
                        vmProject.FolderViewId = i + 1;
                        vmProject.IsSystemFolder = ClearanceInboxFolders[i].IsSystemFolder;
                        vmProject.FolderId = ClearanceInboxFolders[i].FolderId;
                        vmProject.FolderName = ClearanceInboxFolders[i].FolderName;
                        vmProject.TotalRecordCount = 0;
                        vmProject.ShowInformation = false;
                        inbox.Projects.Add(vmProject);
                    }
                }
                LoggerFactory.LogWriter.MethodExit();

                return inbox.Projects;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private InboxViewModel.InboxSearchCriteria GetSearchCriteria(ClearanceInboxSearchCriteria ClearanceInboxSearchCriteria)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                InboxViewModel.InboxSearchCriteria searchCriteria = new InboxViewModel.InboxSearchCriteria();
                foreach (ListItem item in ClearanceInboxSearchCriteria.SearchType)
                {
                    searchCriteria.SearchType.Add(new System.Web.UI.WebControls.ListItem(item.Text, item.Value));
                }
                LoggerFactory.LogWriter.MethodExit();

                return searchCriteria;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.Service, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private InboxViewModel.InboxFilterCriteria GetFilterCriteria(ClearanceInboxFilterCriteria ClearanceInboxFilterCriteria, RoleGroup roleGroup)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                InboxViewModel.InboxFilterCriteria filterCriteria = new InboxViewModel.InboxFilterCriteria();

                foreach (ListItem item in ClearanceInboxFilterCriteria.RequestType)
                {
                    var itemDB = new System.Web.UI.WebControls.ListItem() { Text = item.Text, Value = item.Value, Selected = item.Selected };
                    filterCriteria.RequestType.Add(itemDB);
                }

                if (roleGroup == RoleGroup.RCCAdmin)
                {
                    foreach (ListItem item in ClearanceInboxFilterCriteria.RccAdminRequestType)
                    {
                        var itemDB = new System.Web.UI.WebControls.ListItem() { Text = item.Text, Value = item.Value, Selected = item.Selected };
                        filterCriteria.RccAdminRequestType.Add(itemDB);
                    }

                    foreach (ListItem item in ClearanceInboxFilterCriteria.RccHandler)
                    {
                        var itemDB = new System.Web.UI.WebControls.ListItem() { Text = item.Text, Value = item.Value, Selected = item.Selected };
                        filterCriteria.RccHandler.Add(itemDB);
                    }
                }
                else if (roleGroup == RoleGroup.Requestor)
                {
                    foreach (ListItem item in ClearanceInboxFilterCriteria.Requestor)
                    {
                        var itemDB = new System.Web.UI.WebControls.ListItem() { Text = item.Text, Value = item.Value, Selected = item.Selected };
                        filterCriteria.Requestor.Add(itemDB);
                    }
                }
                else if (roleGroup == RoleGroup.Reviewer)
                {
                    foreach (WorkgroupEntities.WorkgroupBase item in ClearanceInboxFilterCriteria.Workgroup)
                    {
                        var itemDB = new WorkgroupEntities.WorkgroupBase() { Name = item.Name, Id = item.Id, Selected = item.Selected, RoleName = item.RoleName };
                        filterCriteria.Workgroup.Add(itemDB);
                    }
                }

                foreach (ListItem item in ClearanceInboxFilterCriteria.ScopeType)
                {
                    var itemDB = new System.Web.UI.WebControls.ListItem() { Text = item.Text, Value = item.Value, Selected = item.Selected };
                    filterCriteria.ScopeType.Add(itemDB);
                }

                LoggerFactory.LogWriter.MethodExit();

                return filterCriteria;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        private InboxViewModel.InboxState GetState(ClearanceInboxState ClearanceInboxState)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                InboxViewModel.InboxState state = new InboxViewModel.InboxState();
                if (ClearanceInboxState != null)
                {
                    state.FolderSize = ClearanceInboxState.FolderSize;
                    state.SelectedFolderId = ClearanceInboxState.SelectedFolderId;
                    state.ShowAllProjects = (ClearanceInboxState.ShowAllProjects == null ? false : (bool)ClearanceInboxState.ShowAllProjects);
                    state.SelectedProjectId = ClearanceInboxState.SelectedProjectId;
                    state.ProjectReadStatus = ClearanceInboxState.ProjectReadStatus;

                    if (!String.IsNullOrEmpty(ClearanceInboxState.SortByColumnName) && !String.IsNullOrEmpty(ClearanceInboxState.SortByDirection))
                    {
                        state.SortDescriptor.ColumnName = ClearanceInboxState.SortByColumnName;
                        state.SortDescriptor.SortDirection = (ListSortDirection)Enum.Parse(typeof(ListSortDirection), (ClearanceInboxState.SortByDirection), true);
                    }
                }
                LoggerFactory.LogWriter.MethodExit();

                return state;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<InboxViewModel.ColumnSetting> GetColumnSetting(InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<InboxViewModel.ColumnSetting> ColumnSettings = new List<InboxViewModel.ColumnSetting>();
                if (columnSetting != null)
                {
                    InboxViewModel.ColumnSetting ColumnSetting = null;
                    foreach (InboxViewModel.ColumnSetting setting in columnSetting)
                    {
                        ColumnSetting = new InboxViewModel.ColumnSetting();
                        ColumnSetting.Column = setting.Column;
                        ColumnSetting.GridId = setting.GridId;
                        ColumnSetting.Width = setting.Width;
                        ColumnSettings.Add(ColumnSetting);
                    }
                }
                LoggerFactory.LogWriter.MethodExit();

                return ColumnSettings;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult SaveInboxFilters(RoleGroup roleGroup, ClearanceInboxModel clearanceInboxModel, InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var clearanceInboxSearchResult = _clearanceInboxRepository.SaveInboxFilters(roleGroup, clearanceInboxModel);
                LoggerFactory.LogWriter.MethodExit();

                return RefreshLeftPanel(roleGroup, clearanceInboxModel, clearanceInboxSearchResult, columnSetting);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error saving filters." });
            }
        }

        [HttpPost]
        public ActionResult GetInboxSearchResult(RoleGroup roleGroup, ClearanceInboxModel clearanceInboxModel, InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var clearanceInboxSearchResult = _clearanceInboxRepository.GetInboxSearchResult(roleGroup, clearanceInboxModel);
                LoggerFactory.LogWriter.MethodExit();

                return RefreshLeftPanel(roleGroup, clearanceInboxModel, clearanceInboxSearchResult, columnSetting);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error fetching search result." });
            }
        }

        [HttpPost]
        public ActionResult ManageInboxFolders(RoleGroup roleGroup, ClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder, FolderAction folderAction, InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var clearanceInboxSearchResult = _clearanceInboxRepository.ManageInboxFolders(roleGroup, clearanceInboxModel, clearanceInboxFolder, folderAction);
                LoggerFactory.LogWriter.MethodExit();

                return RefreshLeftPanel(roleGroup, clearanceInboxModel, clearanceInboxSearchResult, columnSetting);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error updating folders." });
            }
        }

        [HttpPost]
        public ActionResult ManageInboxProjects(RoleGroup roleGroup, ClearanceInboxModel clearanceInboxModel, ClearanceInboxProject clearanceInboxProject, InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var clearanceInboxSearchResult = _clearanceInboxRepository.ManageInboxProjects(roleGroup, clearanceInboxModel, clearanceInboxProject);
                LoggerFactory.LogWriter.MethodExit();

                return RefreshLeftPanel(roleGroup, clearanceInboxModel, clearanceInboxSearchResult, columnSetting);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error updating folders." });
            }
        }

        [HttpPost]
        public JsonResult UpdateProjectReadStatus(ClearanceInboxProject clearanceInboxProject)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                _clearanceInboxRepository.UpdateProjectReadStatus(clearanceInboxProject);
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Error = false, Message = "Project read status updated successfully." });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Unable to update project read status." });
            }
        }

        [HttpPost]
        public ActionResult SaveInboxFolder(RoleGroup roleGroup, ClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder, InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var clearanceInboxSearchResult = _clearanceInboxRepository.SaveInboxFolder(roleGroup, clearanceInboxModel, clearanceInboxFolder);
                LoggerFactory.LogWriter.MethodExit();

                return RefreshLeftPanel(roleGroup, clearanceInboxModel, clearanceInboxSearchResult, columnSetting);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Unable to save changes." });
            }
        }

        [HttpPost]
        public ActionResult DeleteUnsubmittedProjects(RoleGroup roleGroup, ClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder, InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var clearanceInboxSearchResult = _clearanceInboxRepository.DeleteUnsubmittedProjects(roleGroup, clearanceInboxModel, clearanceInboxFolder);
                LoggerFactory.LogWriter.MethodExit();

                return RefreshLeftPanel(roleGroup, clearanceInboxModel, clearanceInboxSearchResult, columnSetting);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                var message = (ex.GetType() == typeof(FaultException)) && (((FaultException)ex).Code.Name != Constants.UnhandledException) ? ex.Message : "Unable to delete selected projects.";
                return Json(
                    new
                    {
                        Error = true,
                        Message = message
                    });
            }
        }

        private ActionResult RefreshLeftPanel(
            RoleGroup roleGroup, ClearanceInboxModel clearanceInboxModel,
            ClearanceInboxSearchResult clearanceInboxSearchResult, InboxViewModel.ColumnSetting[] columnSetting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                InboxViewModel clearanceInboxViewModel = new InboxViewModel();
                clearanceInboxViewModel.Projects = GetProjects(clearanceInboxSearchResult.Folders);
                clearanceInboxViewModel.State = GetState(clearanceInboxModel.SearchCriteria.InboxState);
                clearanceInboxViewModel.ColumnSettings = GetColumnSetting(columnSetting);

                SetViewBagData(roleGroup);

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("FolderProjects", clearanceInboxViewModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private void SetViewBagData(RoleGroup roleGroup)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ViewBag.RoleGroup = roleGroup;

                switch (roleGroup)
                {
                    case RoleGroup.Reviewer:

                        ViewBag.UserWorkgroups = _clearanceInboxRepository.GetUserWorkgroups();
                        ViewBag.DisplayArtistConsentFolder = _clearanceInboxRepository.DisplayArtistConsentFolder;
                        break;
                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
    }
}
