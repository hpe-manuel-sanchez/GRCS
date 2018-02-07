using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Proxies.ClearanceInboxService;
using UMGI.GRCS.UI.Proxies.ClearanceProjectService;
using UIConstant = UMGI.GRCS.UI.Utilities.Constants;
using BLConstant = UMGI.GRCS.BusinessEntities.Constants.Constants;
using RoleType = UMGI.GRCS.BusinessEntities.Lookups.RoleType;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Models
{
    public partial class ClearanceInboxRepository : IClearanceInboxRepository
    {
        private ILogFactory _logFactory;
        private ISessionWrapper _sessionWrapper;
        private LeanUserInfo _userInfo;
        private List<KeyValuePair<Byte, string>> _roleGroups;
        private KeyValuePair<Byte, string> _preferredRoleGroup;

        public ClearanceInboxRepository(ILogFactory logFactory, ISessionWrapper sessionWrapper)
        {
            try
            {
                _logFactory = logFactory;
                _sessionWrapper = sessionWrapper;
                _userInfo = GetUserInfo();
                _roleGroups = _sessionWrapper.GcsCurrentPermissions.RoleGroups;
                _preferredRoleGroup = _sessionWrapper.GcsCurrentPermissions.PreferredRoleGroup;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }

        private LeanUserInfo GetUserInfo()
        {
            var userInfo = _sessionWrapper.CurrentUserInfo;

            return new LeanUserInfo
            {
                UserId = userInfo.UserId,
                UserLoginName = userInfo.UserLoginName,
                UserName = userInfo.UserName,
                EmailId = userInfo.EmailId,
                MimicedRccAdminLoginName = userInfo.MimicedRccAdminLoginName
            };
        }

        public void AddRejectReason(ClearanceRejectReason clearanceRejectData, string userLoginName)
        {
            using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                clearanceInboxClient.AddRejectReason(clearanceRejectData, userLoginName);
        }

        public void DeleteRejectReason(long reasonId, string userLoginName)
        {
            using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                clearanceInboxClient.DeleteRejectReason(reasonId, userLoginName);
        }

        public ClearanceRejectReasonList GetRejectReasons()
        {
            try
            {
                ClearanceRejectReasonList rejectReasonLst = new ClearanceRejectReasonList();
                using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                    rejectReasonLst.clearanceRejectReasonList = clearanceInboxClient.GetRejectReasons();
                return rejectReasonLst;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }

        public List<ClearanceInboxFolder> GetFolders()
        {
            try
            {
                using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                    return clearanceInboxClient.GetFolders();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }
        
        public ClearanceInboxFolder GetFolderById(long folderId)
        {
            try
            {
                using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                    return clearanceInboxClient.GetFolderById(folderId);
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetFolderById"), ex);
                throw;
            }
        }
        
        public List<ListItem> RejectReasons()
        {
            try
            {
                using (ClearanceProjectClient clearanceProjectClient = new ClearanceProjectClient())
                    return clearanceProjectClient
                        .GetMasterData(new List<string> { UIConstant.ClearanceInboxRejectReason }, _userInfo)
                        .Where(c => c.Type == UIConstant.ClearanceInboxRejectReason)
                        .Select(r => new ListItem { Text = r.Description, Value = r.Value.ToString() })
                        .ToList();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "RejectReasons"), ex);
                throw;
            }
        }

        public List<ClearanceInboxDispatch> DispatchWorkgroups(long workGroupId)
        {
            try
            {
                using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                    return clearanceInboxClient.GetUserDispatchWorkgroups(workGroupId);
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "DispatchWorkgroups"), ex);
                throw;
            }
        }

        public ClearanceInboxProjectDetail GetInboxProjectDetail(RoleGroup roleGroup, long folderId, long clearanceProjectId, long workgroupId)
        {
            try
            {
                using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                    return clearanceInboxClient.GetInboxProjectDetail(roleGroup, _userInfo, folderId, clearanceProjectId, workgroupId);
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetInboxProjectDetail"), ex);
                throw;
            }
        }

        public List<ClearanceInboxRequest> GetInboxProjectRequests(RoleGroup roleGroup, long clearanceProjectId, long workgroupId, long folderId, int pageSize, int pageNo, String sortField, byte gridType)
        {
            try
            {
                using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                    return clearanceInboxClient.GetInboxProjectRequests(roleGroup, _userInfo, clearanceProjectId, workgroupId, folderId, pageSize, pageNo, sortField, gridType);
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetInboxProjectRequests"), ex);
                throw;
            }
        }

        public void UpdateRequestAssignedToUser(ClearanceInboxRequest clearanceInboxRequest)
        {
            try
            {
                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    string mimicUserLoginName = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    string mimicUserName = _sessionWrapper.CurrentUserInfo.UserName;

                    _userInfo.UserLoginName = _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                    _userInfo.UserName = _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;

                    using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                        clearanceInboxClient.UpdateRequestAssignedToUser(_userInfo, clearanceInboxRequest);

                    _userInfo.UserLoginName = mimicUserLoginName;
                    _userInfo.UserName = mimicUserName;
                }
                else
                {
                    _userInfo.UserLoginName = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    _userInfo.UserName = _sessionWrapper.CurrentUserInfo.UserName;

                    using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                        clearanceInboxClient.UpdateRequestAssignedToUser(_userInfo, clearanceInboxRequest);
                }
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "UpdateRequestAssignedToUser"), ex);
                throw;
            }
        }

        public void UpdateRequestReviewComment(ClearanceInboxRequest clearanceInboxRequest)
        {
            try
            {
                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    string mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    string mimicusername = _sessionWrapper.CurrentUserInfo.UserName;

                    _userInfo.UserLoginName = _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                    _userInfo.UserName = _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;

                    using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                        clearanceInboxClient.UpdateRequestReviewComment(_userInfo, clearanceInboxRequest);

                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }
                else
                {
                    _userInfo.UserLoginName = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    _userInfo.UserName = _sessionWrapper.CurrentUserInfo.UserName;
                    using (ClearanceInboxClient clearanceInboxClient = new ClearanceInboxClient())
                        clearanceInboxClient.UpdateRequestReviewComment(_userInfo, clearanceInboxRequest);
                }

            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "UpdateRequestReviewComment"), ex);
                throw;
            }
        }

        public void UpdateRequestAssignedTo_ReviewComment(ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool isSelectedAllAcrossPages, byte gridType, long userId, string userName, bool isAssignedToEnabled, bool isCommentMultipleEnabled, string commentMultiple)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                string mimicuserloginname = string.Empty;
                string mimicusername = string.Empty;
                // temporary fix for UAT Build

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    mimicusername = _sessionWrapper.CurrentUserInfo.UserName;
                }

                _userInfo.UserLoginName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserLoginName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                _userInfo.UserName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                clearanceInboxClient.UpdateRequestAssignedTo_ReviewComment(_userInfo, clearanceInboxRequestAction, roleGroup, isSelectedAllAcrossPages, gridType, userId, userName, isAssignedToEnabled, isCommentMultipleEnabled, commentMultiple);

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }
                clearanceInboxClient.Close();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public ClearanceInboxSearchResult PerformRequestAction(IClearanceInboxModel clearanceInboxModel, ClearanceInboxRequestAction clearanceInboxRequestAction, RoleGroup roleGroup, bool selectAllAcrossPages, byte gridType)
        {

            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ClearanceInboxSearchResult clearanceSearchResult = new ClearanceInboxSearchResult();

                string mimicuserloginname = string.Empty;
                string mimicusername = string.Empty;
                // temporary fix for UAT Build
                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    mimicusername = _sessionWrapper.CurrentUserInfo.UserName;
                }

                _userInfo.UserLoginName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserLoginName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                _userInfo.UserName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                clearanceSearchResult = clearanceInboxClient.PerformRequestAction(_userInfo, clearanceInboxModel.FilterCriteria, clearanceInboxModel.SearchCriteria, clearanceInboxRequestAction, roleGroup, selectAllAcrossPages, gridType);

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }

                clearanceInboxClient.Close();
                return clearanceSearchResult;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        #region Clearance Project search contract

        public InboxContractSearch ClearanceContractSearch(ContractDetails searchContract, LeanUserInfo userInformation)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                // _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "searchContract :{1},FilterFields :{2}", "SearchContract", searchContract, filterFields));

                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();

                var contractSearchResult = new InboxContractSearch
                {
                    ContractSearchInfo = clearanceInboxClient.ClearanceContractSearch(searchContract, userInformation)
                };


                if (contractSearchResult.ContractSearchInfo != null && contractSearchResult.ContractSearchInfo.Count > 0)
                {
                    contractSearchResult.ContractsCount = contractSearchResult.ContractSearchInfo.First().PageDetails.TotalRows;
                }


                _logFactory.LogWriter.Debug(string.Format(Constants.MethodEnd, "ClearanceContractSearch"));
                clearanceInboxClient.Close();
                return contractSearchResult;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public List<ContractDetails> GetResourceArtistDetail(long resourceId, List<long> routedItemId, LeanUserInfo userInformation)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                // _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "searchContract :{1},FilterFields :{2}", "SearchContract", searchContract, filterFields));
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                var contractSearchResult = new InboxContractSearch
                {
                    ContractSearchInfo = clearanceInboxClient.GetResourceArtistDetail(resourceId, routedItemId, userInformation)
                };
                clearanceInboxClient.Close();
                return contractSearchResult.ContractSearchInfo;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public List<ContractDetails> GetResourceArtistDetailSelectedResources(string ResourceIds, LeanUserInfo userInformation)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                // _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "searchContract :{1},FilterFields :{2}", "SearchContract", searchContract, filterFields));
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                var contractSearchResult = new InboxContractSearch
                {
                    ContractSearchInfo = clearanceInboxClient.GetResourceArtistDetailSelectedResources(ResourceIds, userInformation)
                };
                clearanceInboxClient.Close();

                return contractSearchResult.ContractSearchInfo;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }


        public ClearanceInboxResourceHistory GetResourceHistory(long ResourceId, int startIndex, int pagesize, string sortingField)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                var resourceHistory = new ClearanceInboxResourceHistory();

                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                resourceHistory = clearanceInboxClient.GetResourceHistory(_userInfo, ResourceId, startIndex, pagesize, sortingField);
                clearanceInboxClient.Close();
                return resourceHistory;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }

        }


        #endregion

        public Dictionary<string, string> UserWorkgroups()
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {

                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();

                var workgroups = clearanceInboxClient.GetUserWorkgroups(_userInfo);

                var userWorkgroups = new Dictionary<string, string>();
                StringBuilder workgroupMembersOptionList;

                foreach (var workgroup in workgroups)
                {
                    workgroupMembersOptionList = new StringBuilder();

                    foreach (var workgroupMember in workgroup.Value)
                    {
                        workgroupMembersOptionList.AppendFormat(UIConstant.ClearanceInboxWorkgroupUserOption, workgroupMember.Key, workgroupMember.Value);
                    }
                    userWorkgroups.Add(workgroup.Key.ToString(), workgroupMembersOptionList.ToString());
                }

                clearanceInboxClient.Close();
                return userWorkgroups;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public void SetNotificationStatus(RoleGroup roleGroup, long projectId, RoutingNotificationEnum notificationType)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                clearanceInboxClient.SetNotificationStatus(roleGroup, projectId, notificationType, _userInfo);
                clearanceInboxClient.Close();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }

        }

        public ContractInfo CreateClearanceContract(ContractDetails contractDetail, LeanUserInfo userInfo)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {

                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ContractInfo contractInfo = new ContractInfo();
                contractInfo = clearanceInboxClient.CreateClearanceContract(contractDetail, userInfo);
                clearanceInboxClient.Close();
                return contractInfo;

            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        #region SaveContractResourceLinking
        /// Save Contract Resource Linking
        /// <param name="contractList"></param>
        /// <param name="resourceId"></param>
        /// <param name="routedItemList"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public bool SaveContractResourceLinking(List<KeyValuePair<long, bool>> contractList, long resourceId, long routedItemList, LeanUserInfo userInfo)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                string mimicuserloginname = string.Empty;
                string mimicusername = string.Empty;
                // temporary fix for UAT Build

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    mimicusername = _sessionWrapper.CurrentUserInfo.UserName;
                }

                _userInfo.UserLoginName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserLoginName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                _userInfo.UserName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                bool result = clearanceInboxClient.SaveContractDetail(contractList, resourceId, routedItemList, userInfo);
                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }
                clearanceInboxClient.Close();
                return result;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }


        }

        public bool SaveContractResourceLinkingSelectedRequests(List<KeyValuePair<long, bool>> contractList, string resourceIds, LeanUserInfo userInfo)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                string mimicuserloginname = string.Empty;
                string mimicusername = string.Empty;
                // temporary fix for UAT Build

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    mimicusername = _sessionWrapper.CurrentUserInfo.UserName;
                }

                _userInfo.UserLoginName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserLoginName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                _userInfo.UserName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                bool result = clearanceInboxClient.SaveContractDetailSelectedRequests(contractList, resourceIds, userInfo);
                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }
                clearanceInboxClient.Close();
                return result;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }


        }

        #endregion

        public void SaveRCCHandler(long ProjectId, long UserId)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                clearanceInboxClient.SaveRCCHandler(ProjectId, UserId, _userInfo);
                clearanceInboxClient.Close();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }

        }

        public string GenerateEmail(long clrProjectId, string emailType, LeanUserInfo userInfo, List<long> resourcesId, bool selectAllAcrossPages, byte gridType, RoleGroup roleGroup, ClearanceInboxRequestAction clearanceInboxRequestaction)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                string resultstring = clearanceInboxClient.GenerateEmail(clrProjectId, emailType, userInfo, resourcesId, selectAllAcrossPages, gridType, roleGroup, clearanceInboxRequestaction );
                clearanceInboxClient.Close();
                return resultstring;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public List<KeyValuePair<byte, string>> RoleGroups
        {
            get
            {
                try
                {
                    var roleGroups = new List<KeyValuePair<byte, string>>
                        {
                            _roleGroups.FirstOrDefault(r => r.Key == (byte) RoleGroup.RCCAdmin),
                            _roleGroups.FirstOrDefault(r => r.Key == (byte) RoleGroup.Reviewer),
                            _roleGroups.FirstOrDefault(r => r.Key == (byte) RoleGroup.Requestor)
                        };

                    if (roleGroups.FirstOrDefault(r => r.Key == (byte)RoleGroup.Requestor).Key == 0 && _sessionWrapper.GcsCurrentPermissions.Permissions != null && _sessionWrapper.GcsCurrentPermissions.Permissions.Any(r => r.Role == UIConstant.RequestorR2Authorized || r.Role == UIConstant.RequestorUpcAllocation))
                    {
                        roleGroups.Add(new KeyValuePair<byte, string>((byte)RoleGroup.Requestor, RoleGroup.Requestor.ToString()));
                    }

                    roleGroups.RemoveAll(r => r.Key == 0);

                    return roleGroups;
                }
                catch (Exception ex)
                {
                    _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                    throw;
                }
            }
        }

        public KeyValuePair<byte, string> PreferredRoleGroup
        {
            get
            {
                try
                {
                    var preferredRoleGroup = new KeyValuePair<byte, string>();

                    if (_preferredRoleGroup.Key != 0)
                    {
                        preferredRoleGroup = _preferredRoleGroup;
                    }
                    else
                    {
                        var roleGroups = RoleGroups.Distinct().ToDictionary(k => k.Key, v => v.Value);

                        var userInReviewerRole = roleGroups.ContainsKey((byte)RoleGroup.Reviewer);
                        var userInRccAdminRole = roleGroups.ContainsKey((byte)RoleGroup.RCCAdmin);
                        var userInRequestorRole = roleGroups.ContainsKey((byte)RoleGroup.Requestor);

                        if (userInRccAdminRole)
                        {
                            preferredRoleGroup = roleGroups.FirstOrDefault(k => k.Key == (byte)RoleGroup.RCCAdmin);
                        }
                        else if (userInReviewerRole)
                        {
                            preferredRoleGroup = roleGroups.FirstOrDefault(k => k.Key == (byte)RoleGroup.Reviewer);
                        }
                        else if (userInRequestorRole)
                        {
                            preferredRoleGroup = roleGroups.FirstOrDefault(k => k.Key == (byte)RoleGroup.Requestor);
                        }
                    }

                    return preferredRoleGroup;
                }
                catch (Exception ex)
                {
                    _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                    throw;
                }
            }
        }

        public bool DisplayArtistConsentFolder
        {
            get
            {
                try
                {
                    var rolesThatCanSeeArtistConsentFolder = new List<byte>{(byte)RoleType.LocalLabelReviewer,
                                                                            (byte)RoleType.NationalMarketingReviewer,
                                                                            (byte)RoleType.InternationalMarketingReviewer,
                                                                            (byte)RoleType.NationalLegalReviewer,
                                                                            (byte)RoleType.InternationalLegalReviewer};

                    return _sessionWrapper.GcsCurrentPermissions.Roles.Select(r => r.Key).Intersect(rolesThatCanSeeArtistConsentFolder).Any();
                }
                catch (Exception ex)
                {
                    _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                    throw;
                }
            }
        }

        public long GetFolderToExpandByDefault(RoleGroup roleGroup, List<ClearanceInboxFolder> folders)
        {
            try
            {
                SystemFolder primaryDefaultFolder = 0;
                SystemFolder secondaryDefaultFolder = 0;

                switch (roleGroup)
                {
                    case RoleGroup.Reviewer:

                        primaryDefaultFolder = UIConstant.ReviewerPrimaryDefaultFolder;
                        secondaryDefaultFolder = UIConstant.ReviewerSecondaryDefaultFolder;

                        break;

                    case RoleGroup.RCCAdmin:

                        primaryDefaultFolder = UIConstant.RccAdminPrimaryDefaultFolder;
                        secondaryDefaultFolder = UIConstant.RccAdminSecondaryDefaultFolder;

                        break;

                    case RoleGroup.Requestor:

                        primaryDefaultFolder = UIConstant.RequestorPrimaryDefaultFolder;
                        secondaryDefaultFolder = UIConstant.RequestorSecondaryDefaultFolder;

                        break;
                }

                var primaryFolderProjectCount = (from f in folders
                                                 where f.FolderId == (long)primaryDefaultFolder
                                                 select f.ProjectCount).FirstOrDefault();

                return (long)(primaryFolderProjectCount > 0 ? primaryDefaultFolder : secondaryDefaultFolder);
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }

        public ClearanceInboxSearchResult SaveInboxFilters(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ClearanceInboxSearchResult clearanceInboxSearchResult = new ClearanceInboxSearchResult();
                string mimicuserloginname = string.Empty;
                string mimicusername = string.Empty;
                // temporary fix for UAT Build

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    mimicusername = _sessionWrapper.CurrentUserInfo.UserName;
                }

                _userInfo.UserLoginName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserLoginName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                _userInfo.UserName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                clearanceInboxSearchResult = clearanceInboxClient.SaveInboxFilters(_userInfo, roleGroup, clearanceInboxModel.FilterCriteria, clearanceInboxModel.SearchCriteria);
                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }
                clearanceInboxClient.Close();
                return clearanceInboxSearchResult;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }

        }

        public IClearanceInboxModel GetInboxData(RoleGroup roleGroup)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();

                clearanceInboxClient.Open();
                var clearanceInboxData = clearanceInboxClient.GetInboxData(_userInfo, roleGroup);
                clearanceInboxClient.Close();

                return new ClearanceInboxModel
                {
                    FilterCriteria = clearanceInboxData.FilterCriteria,
                    SearchCriteria = clearanceInboxData.SearchCriteria,
                    SearchResult = clearanceInboxData.SearchResult
                };
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public List<ListItem> GetRccHandlers()
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {

                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                List<ListItem> rccHandlers = new List<ListItem>();
                rccHandlers = clearanceInboxClient.GetRccHandlers(_userInfo);
                clearanceInboxClient.Close();
                return rccHandlers;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public List<ListItem> GetRequestors()
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {

                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                List<ListItem> getRequestors = new List<ListItem>();
                getRequestors = clearanceInboxClient.GetRequestors(_userInfo);
                clearanceInboxClient.Close();
                return getRequestors;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }

        }

        public Dictionary<long, Dictionary<long, string>> GetUserWorkgroups()
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                Dictionary<long, Dictionary<long, string>> userWorkgroups = new Dictionary<long, Dictionary<long, string>>();
                userWorkgroups = clearanceInboxClient.GetUserWorkgroups(_userInfo);
                clearanceInboxClient.Close();
                return userWorkgroups;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }

        }

        public ClearanceInboxSearchResult GetInboxSearchResult(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {

                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ClearanceInboxSearchResult clearanceSearchResult = new ClearanceInboxSearchResult();
                clearanceSearchResult = clearanceInboxClient.GetInboxSearchResult(_userInfo, roleGroup, clearanceInboxModel.FilterCriteria, clearanceInboxModel.SearchCriteria);
                clearanceInboxClient.Close();
                return clearanceSearchResult;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public ClearanceInboxSearchResult ManageInboxFolders(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder, FolderAction folderAction)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ClearanceInboxSearchResult clearanceSearchResult = new ClearanceInboxSearchResult();
                clearanceSearchResult = clearanceInboxClient.ManageInboxFolders(_userInfo, roleGroup, clearanceInboxModel.FilterCriteria, clearanceInboxModel.SearchCriteria, clearanceInboxFolder, folderAction);
                clearanceInboxClient.Close();
                return clearanceSearchResult;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public ClearanceInboxSearchResult ManageInboxProjects(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxProject clearanceInboxProject)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ClearanceInboxSearchResult clearanceSearchResult = new ClearanceInboxSearchResult();
                clearanceSearchResult = clearanceInboxClient.ManageInboxProjects(_userInfo, roleGroup, clearanceInboxModel.FilterCriteria, clearanceInboxModel.SearchCriteria, clearanceInboxProject);
                clearanceInboxClient.Close();
                return clearanceSearchResult;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public void UpdateProjectReadStatus(ClearanceInboxProject clearanceInboxProject)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                string mimicuserloginname = string.Empty;
                string mimicusername = string.Empty;
                // temporary fix for UAT Build

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    mimicusername = _sessionWrapper.CurrentUserInfo.UserName;
                }

                _userInfo.UserLoginName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserLoginName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                _userInfo.UserName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                clearanceInboxClient.UpdateProjectReadStatus(_userInfo, clearanceInboxProject);

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }
                clearanceInboxClient.Close();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public ClearanceInboxSearchResult SaveInboxFolder(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ClearanceInboxSearchResult clearanceInboxSearchResult = new ClearanceInboxSearchResult();

                string mimicuserloginname = string.Empty;
                string mimicusername = string.Empty;
                // temporary fix for UAT Build

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    mimicuserloginname = _sessionWrapper.CurrentUserInfo.UserLoginName;
                    mimicusername = _sessionWrapper.CurrentUserInfo.UserName;
                }
                _userInfo.UserLoginName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserLoginName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                _userInfo.UserName = _sessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? _sessionWrapper.CurrentUserInfo.UserName : _sessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;

                clearanceInboxSearchResult = clearanceInboxClient.SaveInboxFolder(_userInfo, roleGroup, clearanceInboxModel.FilterCriteria, clearanceInboxModel.SearchCriteria, clearanceInboxFolder);

                if (_sessionWrapper.GcsCurrentPermissions.IsMimicUser)
                {
                    _userInfo.UserLoginName = mimicuserloginname;
                    _userInfo.UserName = mimicusername;
                }
                clearanceInboxClient.Close();
                return clearanceInboxSearchResult;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }
        }

        public ClearanceInboxSearchResult DeleteUnsubmittedProjects(RoleGroup roleGroup, IClearanceInboxModel clearanceInboxModel, ClearanceInboxFolder clearanceInboxFolder)
        {
            ClearanceInboxClient clearanceInboxClient = null;
            try
            {
                clearanceInboxClient = new ClearanceInboxClient();
                clearanceInboxClient.Open();
                ClearanceInboxSearchResult clearanceInboxSearchResult = new ClearanceInboxSearchResult();
                clearanceInboxSearchResult = clearanceInboxClient.DeleteUnsubmittedProjects(_userInfo, roleGroup, clearanceInboxModel.FilterCriteria, clearanceInboxModel.SearchCriteria, clearanceInboxFolder);
                clearanceInboxClient.Close();
                return clearanceInboxSearchResult;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceInboxClient != null && clearanceInboxClient.State == CommunicationState.Faulted)
                {
                    clearanceInboxClient.Abort();
                    clearanceInboxClient = null;
                }
            }

        }
    }
}
