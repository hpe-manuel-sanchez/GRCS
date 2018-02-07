using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Syncfusion.Mvc.Grid;
using UMGI.GRCS.BusinessEntities.Constants;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.Enumerations;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.Resx.Resource.UIResources;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.Utilities;
using Constants = UMGI.GRCS.UI.Utilities.Constants;
using BusinessConstants = UMGI.GRCS.BusinessEntities.Constants.Constants;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Controllers
{
    public class WorkGroupController : BaseController
    {
        #region Variable Declarations

        IWorkgroupRepository _workgroupRepository;
        IContractRepository _contractRepository;
        IClearanceProjectRepository _clearanceProjectRepository;
        IUserRepository _userRepository;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Intializes a new instance of the class
        /// </summary>
        public WorkGroupController(){}

        /// <summary>
        /// Intializes a new instance of the class with repositories and SessionWrappers
        /// </summary>
        /// <param name="workgroupRepository">workgroupRepository instance</param>
        /// <param name="sessionWrapper">sessionWrapper instance</param>
        /// <param name="contractRepository">contractRepositoryinstance</param>
        /// <param name="logFactory">The logFactory instance</param>
        /// <param name="globalRepository">The globalRepository instance</param>
        public WorkGroupController(IWorkgroupRepository workgroupRepository, IClearanceProjectRepository clearanceProjectRepository, ISessionWrapper sessionWrapper, IContractRepository contractRepository, IUserRepository userRepository, ILogFactory logFactory)
        {
            try
            {
                LoggerFactory = logFactory;
                _workgroupRepository = workgroupRepository;
                _clearanceProjectRepository = clearanceProjectRepository;
                SessionWrapper = sessionWrapper;
                _contractRepository = contractRepository;
                _userRepository = userRepository;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion

        #region Public Methods

        #region Clearance Home

        /// <summary>
        /// Get Clearance home View
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult Index()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return RedirectToAction("Index", "ClearanceInbox");
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get mimic user info
        /// </summary>
        /// <param name="userName">The userName</param>
        /// <param name="isMimic">Check ismimic user</param>
        /// <returns>Json result</returns>
        [HttpPost]
        public JsonResult MimicUserInfo(string userName, bool isMimic)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (!string.IsNullOrEmpty(userName) && isMimic)
                {
                    SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName = SessionWrapper.CurrentUserInfo.UserLoginName;
                    SessionWrapper.CurrentUserInfo.UserLoginName = userName;
                    SessionWrapper.GcsPermissionsBeforeMimic = SessionWrapper.GcsCurrentPermissions;
                    GcsAuthentication mimicedUserPermissions = _userRepository.GetGcsAuthorizationByLoginNameAndApplication(SessionWrapper.CurrentUserInfo);
                    if (mimicedUserPermissions.Permissions != null && mimicedUserPermissions.Permissions.Any())
                    {
                        SessionWrapper.GcsCurrentPermissions = mimicedUserPermissions;
                        SessionWrapper.CurrentUserInfo.UserId = mimicedUserPermissions.UserId;
                        SessionWrapper.GcsCurrentPermissions.MimicUserName = mimicedUserPermissions.UserName;
                        SessionWrapper.GcsCurrentPermissions.IsMimicUser = true;
                        SessionWrapper.CurrentUserInfo.IsMimicUser = true;
                    }
                    else
                    {
                        SessionWrapper.CurrentUserInfo.UserLoginName = SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                        SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName = null;
                        SessionWrapper.GcsCurrentPermissions = SessionWrapper.GcsPermissionsBeforeMimic;

                        LoggerFactory.LogWriter.MethodExit();
                        return Json(new { Result = Constants.JsonError, JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    SessionWrapper.CurrentUserInfo.UserLoginName = SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                    //Set the Rcc admin permission back to current permission in order to avoid ANA request
                    SessionWrapper.GcsCurrentPermissions = SessionWrapper.GcsPermissionsBeforeMimic;
                    SessionWrapper.GcsPermissionsBeforeMimic = null;
                    SessionWrapper.CurrentUserInfo.UserName = SessionWrapper.GcsCurrentPermissions.UserName;
                    SessionWrapper.CurrentUserInfo.UserId = SessionWrapper.GcsCurrentPermissions.UserId;
                    SessionWrapper.GcsCurrentPermissions.IsMimicUser = false;
                    SessionWrapper.CurrentUserInfo.IsMimicUser = false;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = string.Empty, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        #endregion

        #region Manage Company

        /// <summary>
        /// Search Companies with the given parameters
        /// </summary>
        /// <param name="name">string contains the company name</param>
        /// <param name="isacCode">The ISAC code</param>
        /// <param name="country">string contains country name</param>
        /// <param name="jtStartIndex">jtable startIndex from jtable querystring</param>
        /// <param name="jtPageSize">jtable pageSize from jtable querystring</param>
        /// <param name="jtSorting">Sorting details</param>
        /// <returns>JSON formatted content for display company details in Jtable</returns>
        public JsonResult SearchCompany(string name, string isacCode, string country, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("name: {0},isacCode: {1},country: {2}", name, isacCode, country));

                var userInfo = new UserInfo { UserLoginName = GetCurrentLoginName() };
                int totalRowCount = 0;
                var companyList = _workgroupRepository.CompanySearch(name, isacCode, country, jtStartIndex, jtPageSize, jtSorting, userInfo.UserLoginName);

                if (companyList.Count > 0)
                    totalRowCount = companyList[0].PageDetails.TotalRows;

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = Constants.JsonOk, Records = companyList.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        ///  Search workgroup companies with the given parameters
        /// </summary>
        /// <param name="name">string contains the company name</param>
        /// <param name="isacCode">The ISAC code</param>
        /// <param name="country">string contains country name</param>
        /// <param name="workgroupId">The workgroupId</param>
        /// <param name="jtStartIndex">jtable startIndex from jtable querystring</param>
        /// <param name="jtPageSize">jtable pageSize from jtable querystring</param>
        /// <param name="jtSorting">Sorting details</param>
        /// <returns>JSON formatted content for display company details in Jtable</returns>
        public JsonResult GetCompaniesOfWorkgroup(string name, string isacCode, string country, string workgroupId, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("name: {0}, isacCode: {1}, country: {2}, workgroupId: {3}", name, isacCode, country, workgroupId));

                var totalRowCount = 0;
                var companySearchCriteria = new CompanySearchCriteria
                {
                    Name = name,
                    IsacCode = isacCode,
                    CountryName = country,
                    WorkGroupId = Convert.ToInt64(workgroupId),
                    StartIndex = jtStartIndex,
                    PageSize = jtPageSize,
                    SortField = jtSorting,
                    UserLoginName = GetCurrentLoginName()
                };
                var companyList = _workgroupRepository.GetCompaniesOfWorkgroup(companySearchCriteria);
                if (companyList.Count > 0)
                {
                    totalRowCount = companyList[0].PageDetails.TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = companyList.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        ///  HTTP GET Method used to AutoComplete Suggestive search for the companies
        /// </summary>
        /// <param name="term">search string contains company name</param>
        /// <param name="page">The pageName</param>
        /// <param name="parentWorkgroupId">The parentWorkgroupId</param>
        /// <returns>JSON formatted content for display company details in Jtable</returns>
        [HttpGet]
        public JsonResult SuggestiveSearchCompany(string term, string page, string parentWorkgroupId, UserInfo userInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("term: {0}, page: {1}, parentWorkgroupId: {2}, userInfo: {3}", term, page, parentWorkgroupId, userInfo.UserName));

                var companyList = new List<CompanyInfo>();
                if ((string.Compare(page, Constants.CreateParentWorkgroupView, true) == 0) || (string.Compare(page, Constants.MaintainParentWorkgroupView, true) == 0))
                {
                    companyList = _workgroupRepository.CompanySearch(term, string.Empty, string.Empty, BusinessConstants.StartIndex, BusinessConstants.PageSize, string.Empty, userInfo.UserLoginName);
                }
                else
                {
                    var companySearchCriteria = new CompanySearchCriteria
                    {
                        Name = term,
                        IsacCode = string.Empty,
                        CountryName = string.Empty,
                        WorkGroupId = Convert.ToInt64(parentWorkgroupId),
                        StartIndex = BusinessConstants.StartIndex,
                        PageSize = BusinessConstants.PageSize,
                        SortField = string.Empty,
                        UserLoginName = userInfo.UserName
                    };
                    companyList = _workgroupRepository.GetCompaniesOfWorkgroup(companySearchCriteria);
                }

                var list = companyList.Select(
                                            item =>
                                            new AutoSuggestionEntity { id = Convert.ToInt32(item.Id), value = item.Name, label = item.Name })
                                            .ToList();

                LoggerFactory.LogWriter.MethodExit();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        ///  Search Company contains the given company ids
        /// </summary>
        /// <param name="companyIds">Comma seperated company Ids for search</param>
        /// <param name="deletedCompIds">Comma seperated company Ids for ignore from search</param>
        /// <param name="isSort">Find Sorting enable/not</param>
        /// <returns>JSON formatted content for display company details in Jtable</returns>
        public JsonResult AddCompany(string companyIds, string deletedCompIds, string isSort)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("CompanyIds: {0} ,DeletedCompanyIds: {1}", companyIds, deletedCompIds));

                var userInfo = GetCurrentLoginName();
                var companyList = new List<CompanyInfo>();
                var afterSortedCompanyList = new List<CompanyInfo>();
                if (!string.IsNullOrEmpty(companyIds))
                {
                    companyIds = companyIds.TrimEnd(',');
                    var arrCompanyIds = companyIds.Split(BusinessConstants.CommaSeperator);
                    long[] compIds = Array.ConvertAll(arrCompanyIds, long.Parse);
                    compIds = compIds.Distinct().ToArray();

                    if (!string.IsNullOrEmpty(deletedCompIds))
                    {
                        if (companyIds.Contains(deletedCompIds))
                        {
                            companyIds = companyIds.Replace(deletedCompIds, string.Empty).Trim();
                        }
                    }
                    companyList = _workgroupRepository.GetCompanies(companyIds, userInfo);
                    if (isSort == BusinessConstants.IsSortValue)
                    {
                        if (compIds != null && compIds.Any())
                        {
                            foreach (long compId in compIds)
                            {
                                afterSortedCompanyList.AddRange(from addedCompany in companyList where addedCompany.Id.Equals(compId) select new CompanyInfo { Name = addedCompany.Name, ISACCode = addedCompany.ISACCode, CountryName = addedCompany.CountryName, Id = addedCompany.Id, ModifiedDateTime = addedCompany.ModifiedDateTime });
                            }
                        }
                    }
                    else
                    {
                        afterSortedCompanyList = (from compList in companyList
                                                  orderby compList.CountryName, compList.Name
                                                  select compList).ToList();
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = afterSortedCompanyList.AsQueryable(), TotalRecordCount = afterSortedCompanyList.Count });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// HTTP POST method used to format the company list session to JSON formatted content.
        /// </summary>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult DataViewCompany(string companies)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                var companyList = new List<CompanyInfo>();
                if (!string.IsNullOrEmpty(companies))
                {
                    var serializer = new JavaScriptSerializer();
                    companyList = serializer.Deserialize<List<CompanyInfo>>(companies);
                    LoggerFactory.LogWriter.MethodExit();
                    return Json(new { Result = Constants.JsonOk, Records = companyList.AsQueryable() });
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = companyList.AsQueryable() });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        /// <summary>
        ///  Used to view the Manage company partial view
        /// </summary>
        /// <param name="parentPage">The parentPage</param>
        /// <param name="parentWorkgroupId">The parentWorkgroupId</param>
        /// <returns>Specified ManageCompany View and Model</returns>
        public PartialViewResult ManageCompanyPopup(string parentPage, string parentWorkgroupId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("parentPage: {0},parentWorkgroupId: {1}", parentPage, parentWorkgroupId));
                PermissionCheckNdRedirect(new[] { GcsTasks.CreateParentWorkgroup, GcsTasks.CreateChildWorkgroup, GcsTasks.MaintainParentWorkgroup, GcsTasks.MaintainChildWorkgroup });
                ViewBag.parentPage = parentPage;
                ViewBag.parentWorkgroupId = parentWorkgroupId;
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ManageCompanyView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        ///  Used to view the Manage company partial view on Routing Variations page
        /// </summary>
        /// <param name="parentPage">The parentPage</param>
        /// <param name="parentWorkgroupId">The parentWorkgroupId</param>
        /// <returns>Specified ManageCompany View and Model</returns>
        public PartialViewResult ManageCompany(string companies, string id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("parentPage: {0},parentWorkgroupId: {1}", companies, id));

                ViewBag.companyKeyId = id;
                ViewBag.savedCompanies = companies;

                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ManageCompanyView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        ///  Used to view the Single User Select partial view
        /// </summary>
        /// <returns>Specified SingleSelectUser View and Model</returns>
        public PartialViewResult SelectSingleUserPopup()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = BusinessConstants.MimicUser;
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(BusinessConstants.SelectSingleUserView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        ///  Used to view the Mimic User Select partial view
        /// </summary>
        /// <returns>Specified Mimic View and Model</returns>
        public PartialViewResult MimicUserPopup()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = BusinessConstants.MimicUser;
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.MimicUserView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion

        #region Manage Users

        /// <summary>
        /// Search user details with specified search criteria
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="loginName">The loginName</param>
        /// <param name="country">contains any country name text</param>
        /// <param name="jtStartIndex">jtable startIndex from jtable querystring</param>
        /// <param name="jtPageSize">jtable pageSize from jtable querystring</param>
        /// <param name="jtSorting">sorting detail</param>
        /// <returns>JSON formatted user details</returns>
        public JsonResult SearchUsers(string userName, string loginName, string country, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("userName: {0},loginName: {1},country: {2}", userName, loginName, country));
                string userLoginName = GetCurrentLoginName();
                var totalRecCount = 0;
                var userSearch = new WorkGroupUserSearchCriteria();
                var filter = new PagingBase { StartIndex = jtStartIndex, PageSize = jtPageSize, SortField = jtSorting };
                userSearch.UserName = userName;
                userSearch.LoginId = loginName;
                userSearch.CountryName = country;
                userSearch.GridFilterlterCriteria = filter;
                var searchUsers = _workgroupRepository.GetUsers(userSearch, AnaTargetApplication.Gcs, userLoginName);

                SessionWrapper.SearchedUsers = searchUsers;
                if (searchUsers.Count > 0)
                {
                    totalRecCount = searchUsers[0].TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = searchUsers.AsQueryable(), TotalRecordCount = totalRecCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Search user details with specified search criteria
        /// </summary>
        /// <param name="args">table paging and sorting information from table querystring</param>
        /// <param name="userName">The username</param>
        /// <param name="loginName">The loginName</param>
        /// <param name="country">contains any country name text</param>
        /// <param name="jtPageSize">table pageSize from page sizr deopdown</param>
        /// <returns>GridJSONActions user details</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MimicUserPopup(PagingParams args, string userName, string loginName, string country, string pageName)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("userName: {0},loginName: {1},country: {2}", userName, loginName, country));
                //UserLoginName fetching for special user purpose
                string userLoginName = SessionWrapper.CurrentUserInfo.UserLoginName;
                int totalRowCount = 0;

                var request = (RequestType)Convert.ToInt32(args.RequestType);

                if (args.StartIndex < 0) args.StartIndex = BusinessConstants.StartIndex;

                if (request == RequestType.Refresh) args.StartIndex = BusinessConstants.StartIndex;

                var filter = new PagingBase { StartIndex = args.StartIndex, PageSize = args.PageSize };


                if (args.SortDescriptors != null && (request == RequestType.Sorting || request == RequestType.Paging))
                {
                    SortDescriptor firstOrDefault = args.SortDescriptors.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        filter.SortField = firstOrDefault.ColumnName;
                        filter.IsAscendingOrder = firstOrDefault.SortDirection.ToString() == BusinessConstants.Ascending;
                    }
                }
                var searchUsers = new List<WorkGroupUser>();
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(loginName) || !string.IsNullOrEmpty(country))
                {
                    var userSearch = new WorkGroupUserSearchCriteria();
                    userSearch.UserName = userName;
                    userSearch.LoginId = loginName;
                    userSearch.CountryName = country;
                    userSearch.UserToExclude = pageName == Constants.MimicUserView ? userLoginName : string.Empty;
                    userSearch.GridFilterlterCriteria = filter;
                    searchUsers = _workgroupRepository.GetUsers(userSearch, AnaTargetApplication.Gcs, userLoginName).ToList();
                }
                if (searchUsers.Any())
                {
                    totalRowCount = searchUsers.ToList()[0].TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();

                return searchUsers.GridJSONActions<WorkGroupUser>(totalRowCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Search user details with specified search criteria
        /// </summary>
        /// <param name="args">table paging and sorting information from table querystring</param>
        /// <param name="userName">The username</param>
        /// <param name="loginName">The loginName</param>
        /// <param name="country">contains any country name text</param>
        /// <param name="jtPageSize">table pageSize from page sizr deopdown</param>
        /// <returns>GridJSONActions user details</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SelectSingleUserPopup(PagingParams args, string userName, string loginName, string country, string pageName)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("userName: {0},loginName: {1},country: {2}", userName, loginName, country));
                //UserLoginName fetching for special user purpose
                string userLoginName = SessionWrapper.CurrentUserInfo.UserLoginName;
                int totalRowCount = 0;

                var request = (RequestType)Convert.ToInt32(args.RequestType);

                args.PageSize = args.PageSize;

                if (args.StartIndex < 0) args.StartIndex = BusinessConstants.StartIndex;

                if (request == RequestType.Refresh) args.StartIndex = BusinessConstants.StartIndex;

                var filter = new PagingBase { StartIndex = args.StartIndex, PageSize = args.PageSize };

                if (args.SortDescriptors != null && (request == RequestType.Sorting || request == RequestType.Paging))
                {
                    SortDescriptor firstOrDefault = args.SortDescriptors.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        filter.SortField = firstOrDefault.ColumnName;
                        filter.IsAscendingOrder = firstOrDefault.SortDirection.ToString() == BusinessConstants.Ascending;
                    }
                }
                var searchSingleUsers = new List<WorkGroupUser>();
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(loginName) || !string.IsNullOrEmpty(country))
                {
                    var userSearch = new WorkGroupUserSearchCriteria();
                    userSearch.UserName = userName;
                    userSearch.LoginId = loginName;
                    userSearch.CountryName = country;
                    userSearch.UserToExclude = pageName == BusinessConstants.MimicUser ? userLoginName : string.Empty;
                    userSearch.GridFilterlterCriteria = filter;
                    searchSingleUsers = _workgroupRepository.GetUsers(userSearch, AnaTargetApplication.Gcs, userLoginName).ToList();
                }
                if (searchSingleUsers.Any())
                {
                    totalRowCount = searchSingleUsers.ToList()[0].TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();

                return searchSingleUsers.GridJSONActions<WorkGroupUser>(totalRowCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get user details for the given search criteria
        /// </summary>
        /// <param name="loginNames">The comma seperated user loginNames</param>
        /// <param name="deletedUserIds">Ignore the comma seperated UserIds for search</param>
        /// <param name="clickType">Check the clickType is Add/Save/Delete</param>
        /// <param name="defaultUserIds">The defaultUserIds</param>
        /// <param name="mngWkgUserIds">The mngWkgUserIds</param>
        /// <param name="r2AuthorisedUserIds">The r2AuthorisedUserIds</param>
        /// <param name="allocateUpcUserIds">The allocateUpcUserIds</param>
        /// <param name="isSort">Find Sorting is enable</param>
        /// <param name="inRoleUserIds">The inRoleUserIds</param>
        /// <returns>JSON formatted User Details</returns>
        public JsonResult AddUsers(string loginNames, string deletedUserIds, string clickType, string defaultUserIds, string inRoleUserIds, string mngWkgUserIds, string r2AuthorisedUserIds, string allocateUpcUserIds, string isSort)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("LoginNames:{0} ,Deleted: {1}", loginNames, deletedUserIds));
                var userName = GetCurrentLoginName();
                var selectedUsersWithAllFields = new List<WorkGroupUser>();
                var selectedUsersAfterSort = new List<WorkGroupUser>();
                if (!string.IsNullOrEmpty(loginNames))
                {
                    if (clickType == BusinessConstants.ClickTypeDelete)
                    {
                        if (!string.IsNullOrEmpty(deletedUserIds))
                        {
                            string[] arrDeletedUserIds = deletedUserIds.Split(BusinessConstants.CommaSeperator);
                            foreach (var arrDeletedUserId in arrDeletedUserIds)
                            {
                                loginNames = loginNames.Replace(arrDeletedUserId, string.Empty);
                            }
                        }
                    }

                    var listUserIds = loginNames.Split(new char[] { BusinessConstants.CommaSeperator }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                    listUserIds = listUserIds.Distinct().ToList();
                    var loginNamesWithNoDuplicates = new StringBuilder();
                    foreach (string user in listUserIds)
                    {
                        loginNamesWithNoDuplicates.AppendFormat(BusinessConstants.CommaFormat, user);
                    }
                    if (loginNamesWithNoDuplicates.Length > 0)
                    {
                        loginNames = loginNamesWithNoDuplicates.ToString(0, loginNamesWithNoDuplicates.Length - 1);
                    }
                    if (loginNames.Length >= BusinessConstants.LoginNameCount)
                    {
                        selectedUsersWithAllFields = _workgroupRepository.GetWorkgroupUserList(loginNames, userName);
                    }
                    var appUsers = SessionWrapper.SearchedUsers;
                    List<WorkGroupUser> selectedUsers = null;
                    if (SessionWrapper.SelectedUsers != null)
                    {
                        selectedUsers = SessionWrapper.SelectedUsers;
                    }

                    bool isUserAlreadyAdded = false;
                    foreach (string user in listUserIds)
                    {
                        if (!selectedUsersWithAllFields.Any(s => s.LoginName.ToLower().Equals(user.ToLower())))
                        {
                            selectedUsersWithAllFields.AddRange((from au in appUsers
                                                                 where au.LoginName.ToLower().Equals(user.ToLower())
                                                                 select new WorkGroupUser { Name = au.Name, LoginName = au.LoginName, Email = au.Email, UserWorkgroupNames = au.UserWorkgroupNames, IsInRole = au.IsInRole }));
                            if (appUsers.Any(s => s.LoginName.ToLower().Equals(user.ToLower())))
                                isUserAlreadyAdded = true;
                        }

                        if (selectedUsers != null && !isUserAlreadyAdded)
                        {
                            if (!selectedUsersWithAllFields.Any(s => s.LoginName.ToLower().Equals(user.ToLower())))
                            {
                                selectedUsersWithAllFields.AddRange((from selUserDoesNotInLocDb in selectedUsers
                                                                     where selUserDoesNotInLocDb.LoginName.ToLower().Equals(user.ToLower())
                                                                     select new WorkGroupUser { Name = selUserDoesNotInLocDb.Name, LoginName = selUserDoesNotInLocDb.LoginName, Email = selUserDoesNotInLocDb.Email, UserWorkgroupNames = selUserDoesNotInLocDb.UserWorkgroupNames, IsInRole = selUserDoesNotInLocDb.IsInRole }));
                            }
                        }
                        isUserAlreadyAdded = false;
                    }
                    if (clickType == BusinessConstants.ClickTypeSave || string.Compare(clickType, BusinessConstants.ClickTypeAdd, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        SessionWrapper.SelectedUsers = selectedUsersWithAllFields;
                    }

                    if (isSort == BusinessConstants.IsSortValue)
                    {
                        foreach (string logName in listUserIds)
                        {
                            selectedUsersAfterSort.AddRange(from addedUser in selectedUsersWithAllFields where addedUser.LoginName.ToLower().Equals(logName.ToLower()) select new WorkGroupUser { Name = addedUser.Name, LoginName = addedUser.LoginName, Email = addedUser.Email, UserWorkgroupNames = addedUser.UserWorkgroupNames, IsInRole = addedUser.IsInRole });
                        }
                    }
                    else
                    {
                        selectedUsersAfterSort = (from selectUser in selectedUsersWithAllFields
                                                  orderby selectUser.Name
                                                  select selectUser).ToList();
                    }
                    defaultUserIds = defaultUserIds.ToLower();
                    var arrDefaultUserIDs = defaultUserIds.Split(new char[] { BusinessConstants.CommaSeperator }, StringSplitOptions.RemoveEmptyEntries);
                    inRoleUserIds = inRoleUserIds.ToLower();
                    var arrInRoleUserIds = inRoleUserIds.Split(new char[] { BusinessConstants.CommaSeperator }, StringSplitOptions.RemoveEmptyEntries);

                    mngWkgUserIds = mngWkgUserIds.ToLower();
                    var arrMngWkgUserIds = mngWkgUserIds.Split(new char[] { BusinessConstants.CommaSeperator }, StringSplitOptions.RemoveEmptyEntries);

                    r2AuthorisedUserIds = r2AuthorisedUserIds.ToLower();
                    var arrR2AuthorisedUserIds = r2AuthorisedUserIds.Split(new char[] { BusinessConstants.CommaSeperator }, StringSplitOptions.RemoveEmptyEntries);

                    allocateUpcUserIds = allocateUpcUserIds.ToLower();
                    var arrAllocateUpcUserIds = allocateUpcUserIds.Split(new char[] { BusinessConstants.CommaSeperator }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var user in selectedUsersAfterSort)
                    {
                        user.IsUserDefault = arrDefaultUserIDs.Contains(user.LoginName.ToLower());
                        user.IsInRole = arrInRoleUserIds.Contains(user.LoginName.ToLower());
                        user.CanManageWorkgroup = arrMngWkgUserIds.Contains(user.LoginName.ToLower());
                        user.IsR2Authorized = arrR2AuthorisedUserIds.Contains(user.LoginName.ToLower());
                        user.CanAllocateUpc = arrAllocateUpcUserIds.Contains(user.LoginName.ToLower());
                    }
                    SessionWrapper.WorkGroupUsers = selectedUsersWithAllFields;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = selectedUsersAfterSort.AsQueryable(), TotalRecordCount = selectedUsersAfterSort.Count });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get Manage user partial view
        /// </summary>
        /// <returns>Partial view response</returns>
        public PartialViewResult OpenManageUsers()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                PermissionCheckNdRedirect(new[] { GcsTasks.CreateParentWorkgroup, GcsTasks.CreateChildWorkgroup, GcsTasks.MaintainParentWorkgroup, GcsTasks.MaintainChildWorkgroup });
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ManageUser, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the manage user partial view 
        /// </summary>
        /// <returns>Partial View</returns>
        public ActionResult GetManageUserTemplate()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                if (_workgroupRepository != null)
                {
                    LoggerFactory.LogWriter.MethodExit();
                    return PartialView(Constants.ManageUser);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return null;
        }

        #endregion

        #region Manage WorkGroup

        /// <summary>
        /// Display Search workgroup screen
        /// </summary>
        /// <returns>Specified view and model</returns>
        public ActionResult SearchWorkgroup()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                PermissionCheckNdRedirect(new[] { GcsTasks.DeactivateWorkgroup });
                LoggerFactory.LogWriter.MethodExit();
                return View(Constants.SearchWorkgroupView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get view workgroup partial view
        /// </summary>
        /// <param name="workgroupId">The workgroupId</param>
        /// <returns>Specified view and model</returns>
        public PartialViewResult ViewWorkgroup(long workgroupId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0}", workgroupId));
                string userName = GetCurrentLoginName();
                Workgroup workGroupDetails = _workgroupRepository.Workgroup(workgroupId, userName);
                ViewBag.workGroupIdTerritory = workgroupId;

                if (_workgroupRepository.WorkgroupModel.Workgroup.Companies.Count > 0)
                {
                    var serializer = new JavaScriptSerializer();
                    ViewBag.ViewCompany = serializer.Serialize(_workgroupRepository.WorkgroupModel.Workgroup.Companies);
                }
                List<TerritorialDisplay> territorialDisplay = workGroupDetails != null && workGroupDetails.Territories != null ? workGroupDetails.Territories : null;
                if (territorialDisplay != null)
                {
                    ViewBag.territoryDisplay = territorialDisplay;
                }
                var defaultUserNames = new StringBuilder();
                var usersInRole = new StringBuilder();
                var usersCanMngworkgroup = new StringBuilder();
                var usersR2Authorized = new StringBuilder();
                var usersCanAllocateUpc = new StringBuilder();
                if (workGroupDetails != null)
                {
                    foreach (var user in workGroupDetails.Users)
                    {
                        if (user.IsUserDefault)
                        {
                            defaultUserNames.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.IsInRole)
                        {
                            usersInRole.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.CanManageWorkgroup)
                        {
                            usersCanMngworkgroup.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.IsR2Authorized)
                        {
                            usersR2Authorized.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.CanAllocateUpc)
                        {
                            usersCanAllocateUpc.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                    }
                    ViewBag.defaultUserName = defaultUserNames.ToString();
                    ViewBag.UsersInRole = usersInRole.ToString();
                    ViewBag.UsersCanMngworkgroup = usersCanMngworkgroup.ToString();
                    ViewBag.UsersR2Authorized = usersR2Authorized.ToString();
                    ViewBag.UsersCanAllocateUPC = usersCanAllocateUpc.ToString();
                    ViewBag.workgroupRoleName = workGroupDetails.RoleName;
                    var serializerModifyDate = new JavaScriptSerializer();
                    ViewBag.ModifiedDateTime = serializerModifyDate.Serialize(workGroupDetails.ModifiedDateTime);
                }

                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ViewWorkgroup, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get child workgroup for the specified workgroup
        /// </summary>
        /// <param name="workgroupId">Used to get the child workgroup</param>
        /// <param name="workgroupType">The workgroupType(Child/Parent)</param>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult ViewChildWorkgroup(long workgroupId, string workgroupType)
        {
            var contractIdCollectionForResource = new StringBuilder();
            var contractIdCollectionForArtist = new StringBuilder();
            var serializer = new JavaScriptSerializer();
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0},workgroupType: {1}", workgroupId, workgroupType));
                string userName = GetCurrentLoginName();

                ChildWorkgroup workGroupChildDetails = null;
                if (string.Compare(workgroupType, BusinessConstants.WorkGroupType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    workGroupChildDetails = _workgroupRepository.GetChildWorkgroup(workgroupId, userName);
                }
                else
                {
                    _workgroupRepository.Workgroup(workgroupId, userName);
                }
                ViewBag.workGroupIdTerritory = workgroupId;
                if (_workgroupRepository.WorkgroupModel.Workgroup.Companies.Count > 0)
                {
                    ViewBag.ViewCompany = serializer.Serialize(_workgroupRepository.WorkgroupModel.Workgroup.Companies);
                }

                if (_workgroupRepository.WorkgroupModel.GetChildWorkgroup.ArtistContracts != null)
                {
                    if (_workgroupRepository.WorkgroupModel.GetChildWorkgroup.ArtistContracts.Count > 0)
                    {
                        foreach (var artistContractId in _workgroupRepository.WorkgroupModel.GetChildWorkgroup.ArtistContracts)
                        {
                            contractIdCollectionForArtist.Append(contractIdCollectionForArtist).Append(artistContractId.ContractId.ToString(CultureInfo.InvariantCulture)).Append(BusinessConstants.LineSeperator);
                        }
                        ViewBag.contractIdListForArtist = contractIdCollectionForArtist.ToString();
                    }
                }
                if (_workgroupRepository.WorkgroupModel.GetChildWorkgroup.ResourceContracts != null)
                {
                    if (_workgroupRepository.WorkgroupModel.GetChildWorkgroup.ResourceContracts.Count > 0)
                    {
                        foreach (var resourceContractId in _workgroupRepository.WorkgroupModel.GetChildWorkgroup.ResourceContracts)
                        {
                            contractIdCollectionForResource.Append(resourceContractId.ContractId + BusinessConstants.AddComma + resourceContractId.ResourceId + BusinessConstants.LineSeperator);
                        }

                        ViewBag.contractIdListForResource = contractIdCollectionForResource.ToString();
                    }
                }

                List<TerritorialDisplay> territorialDisplay = workGroupChildDetails != null && workGroupChildDetails.Territories != null ? workGroupChildDetails.Territories : null;
                if (territorialDisplay != null)
                {
                    ViewBag.territoryDisplay = territorialDisplay;
                }
                var defaultUserNames = new StringBuilder();
                var usersInRole = new StringBuilder();
                var usersCanMngworkgroup = new StringBuilder();
                var usersR2Authorized = new StringBuilder();
                var usersCanAllocateUpc = new StringBuilder();
                if (workGroupChildDetails != null)
                {
                    ViewBag.workgroupRoleName = workGroupChildDetails.RoleName;

                    foreach (var user in workGroupChildDetails.Users)
                    {
                        if (user.IsUserDefault)
                        {
                            defaultUserNames.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.IsInRole)
                        {
                            usersInRole.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.CanManageWorkgroup)
                        {
                            usersCanMngworkgroup.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.IsR2Authorized)
                        {
                            usersR2Authorized.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                        if (user.CanAllocateUpc)
                        {
                            usersCanAllocateUpc.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                        }
                    }
                }
                ViewBag.defaultUserName = defaultUserNames.ToString();
                ViewBag.UsersInRole = usersInRole.ToString();
                ViewBag.UsersCanMngworkgroup = usersCanMngworkgroup.ToString();
                ViewBag.UsersR2Authorized = usersR2Authorized.ToString();
                ViewBag.UsersCanAllocateUPC = usersCanAllocateUpc.ToString();
                var serializerModifyDate = new JavaScriptSerializer();
                ViewBag.ModifiedDateTime = serializerModifyDate.Serialize(workGroupChildDetails.ModifiedDateTime);

                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ViewChildWorkgroup, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Search workgroup with workgroup parameters
        /// </summary>
        /// <param name="workgroupName">Search this text contains in any workgroup name from the table</param>
        /// <param name="workgroupRole">The workgroupRole</param>
        /// <param name="workgroupCompany">Search this text contains in any workgroup company name from the table</param>
        /// <param name="workgroupUser">Search this text contains in any workgroup user name from the table</param>
        /// <param name="workgroupCountry">Search this text contains in any workgroup country name from the table</param>
        /// <param name="isStatus">The isStatus</param>
        /// <param name="jtStartIndex">querystring from jtable for startIndex</param>
        /// <param name="jtPageSize">querystring from jtable for pagesize</param>
        /// <param name="jtSorting">sorting detail</param>
        /// <returns>List of workGroupSearchResult formatted as JSON content</returns>
        [HttpPost]
        public JsonResult SearchWorkgroup(string workgroupName, int workgroupRole, string workgroupCompany, string workgroupUser, string workgroupCountry, bool isStatus, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupName: {0},workgroupRole: {1},workgroupCompany: {2},workgroupUser: {3},workgroupCountry: {4}", workgroupName, workgroupRole, workgroupCompany, workgroupUser, workgroupCountry));
                var workgroupSearchCriteria = new WorkgroupSearchCriteria
                {
                    Name = workgroupName,
                    Role = workgroupRole,
                    Company = workgroupCompany,
                    User = workgroupUser,
                    Country = workgroupCountry,
                    IsActiveOnly = isStatus,
                    StartIndex = jtStartIndex,
                    PageSize = jtPageSize,
                    SortField = jtSorting,
                    UserLoginName = SessionWrapper.CurrentUserInfo.UserLoginName
                };
                var rccAdmin = SessionWrapper.GcsCurrentPermissions;
                if (rccAdmin.Roles != null)
                {
                    workgroupSearchCriteria.IsRccAdmin = rccAdmin.Roles.Where(roles => roles.Value == BusinessConstants.RCCAdmin).GroupBy(role => role.Value).Count() != 0;
                }
                int totalRowCount = 0;
                List<WorkgroupSearchResult> workgroupList = _workgroupRepository.SearchWorkgroup(workgroupSearchCriteria);
                if (workgroupList.Count > 0)
                {
                    totalRowCount = workgroupList[0].TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = workgroupList.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        /// <summary>
        /// Deactivate the specified workgroup
        /// </summary>
        /// <param name="workGroupId">The workGroupId</param>
        /// <param name="modifiedDateTime">The modifiedDateTime</param>
        /// <param name="fromParent">Used to check from parent view or not</param>
        /// <param name="isParent">Used to check is parentv view or not</param>
        /// <param name="jtStartIndex">querystring from jtable for startIndex</param>
        /// <param name="jtPageSize">querystring from jtable for pagesize</param>
        /// <returns>List of Pending Request formatted as JSON content</returns>
        [HttpPost]
        public JsonResult DeactivateWorkGroup(long workGroupId, string modifiedDateTime, bool fromParent, bool isParent, int jtStartIndex, int jtPageSize)
        {
            int totalRowCount = 0;
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workGroupId: {0}", workGroupId));
                PermissionCheckNdRedirect(new[] { GcsTasks.DeactivateWorkgroup });
                modifiedDateTime = modifiedDateTime.Replace(BusinessConstants.Slash, string.Empty);
                var serializer = new JavaScriptSerializer();
                var modifiedDate = serializer.Deserialize<DateTime>(BusinessConstants.DateLiteralLeft + modifiedDateTime + BusinessConstants.DateLiteralRight).ToLocalTime();
                var requestSearch = new RequestSearch
                {
                    WorkGroupId = workGroupId,
                    ModifiedDateTime = modifiedDate,
                    FromParent = fromParent,
                    IsParent = isParent,
                    StartIndex = jtStartIndex,
                    PageSize = jtPageSize,
                    UserLoginName = GetCurrentLoginName()
                };
                var pendingRequest = _workgroupRepository.DeactivateWorkGroup(requestSearch);
                if (pendingRequest.Count > 0)
                {
                    totalRowCount = pendingRequest[0].TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = pendingRequest.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (System.ServiceModel.FaultException fx)
            {
                return Json(new { Result = fx.Message, fx.Message });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        /// <summary>
        /// Request reassign to the given workgroup
        /// </summary>
        /// <param name="expectWorkgroupId">The expectWorkgroupId</param>
        /// <param name="assignedgtWorkgroupId">The assignedgtWorkgroupId</param>
        /// <param name="requestIds">The requestIds</param>
        /// <returns>JSON Formatted content</returns>
        [HttpPost]
        public JsonResult RequestReassignToWorkgroup(long expectWorkgroupId, long assignedgtWorkgroupId, string requestIds)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("expectWorkgroupId: {0},assignedgtWorkgroupId: {1},requestIds: {2}", expectWorkgroupId, assignedgtWorkgroupId, requestIds));
                var userName = GetCurrentLoginName();
                bool result = _workgroupRepository.RequestReassignToWorkgroup(expectWorkgroupId, assignedgtWorkgroupId, requestIds, userName);
                LoggerFactory.LogWriter.MethodExit();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        /// <summary>
        /// Get workgroup details with child workgroup
        /// </summary>
        /// <param name="parentId">The parentId</param>
        /// <param name="workgroupId">The workgroupId</param>
        /// <param name="workgroupName">Search this text contains in any workgroup name from the table</param>
        /// <param name="workgroupRole">The workgroupRole</param>
        /// <param name="workgroupCompany">Search this text contains in any workgroup company name from the table</param>
        /// <param name="workgroupUser">Search this text contains in any workgroup user name from the table</param>
        /// <param name="workgroupCountry">Search this text contains in any workgroup country name from the table</param>
        /// <param name="jtStartIndex">querystring from jtable for startIndex</param>
        /// <param name="jtPageSize">querystring from jtable for pagesize</param>
        /// <param name="jtSorting">sorting detail</param>
        /// <returns>Workgroup details formatted as JSON content</returns>
        [HttpPost]
        public JsonResult GetWorkgroupByChild(long parentId, long workgroupId, string workgroupName, int workgroupRole, string workgroupCompany, string workgroupUser, string workgroupCountry, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("parentId: {0},workgroupId: {1},workgroupName: {2},workgroupRole: {3},workgroupCompany: {4},,workgroupUser: {5},workgroupCountry: {6}", parentId, workgroupId, workgroupName, workgroupRole, workgroupCompany, workgroupUser, workgroupCountry));
                var workGroupSearchCriteria = new WorkgroupSearchCriteria
                {
                    Name = workgroupName,
                    Role = workgroupRole,
                    Company = workgroupCompany,
                    User = workgroupUser,
                    Country = workgroupCountry,
                    StartIndex = jtStartIndex,
                    PageSize = jtPageSize,
                    SortField = jtSorting,
                    UserLoginName = GetCurrentLoginName()
                };
                List<WorkgroupSearchResult> workgroupList = null;
                int totalRowCount = 0;
                workgroupList = _workgroupRepository.GetWorkgroupByChild(parentId, workgroupId, workGroupSearchCriteria);
                if (workgroupList.Count > 0)
                {
                    totalRowCount = workgroupList[0].TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = workgroupList.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        /// <summary>
        /// Get search workgroup partial view
        /// </summary>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult SearchWorkgroupRedirect()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return PartialView(Constants.SearchWorkGroupView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// This method used to view the Create Parent Workgroup Screen
        /// </summary>
        /// <returns>View</returns>
        public ActionResult CreateParentWorkgroup()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                PermissionCheckNdRedirect(GcsTasks.CreateParentWorkgroup);
                ClearWorkgroupSession();
                ViewBag.PageName = BusinessConstants.CreateParentWorkgroup;
                LoggerFactory.LogWriter.MethodExit();
                return View(Constants.CreateParentWorkgroupView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// This method used to view the Maintain Parent Workgroup Screen
        /// </summary>
        /// <returns>View</returns>
        public ViewResult MaintainParentWorkGroup()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = Constants.MaintainParentWorkgroupView;
                PermissionCheckNdRedirect(GcsTasks.MaintainParentWorkgroup);
                LoggerFactory.LogWriter.MethodExit();
                return View(Constants.MaintainParentWorkgroupView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// This Method for Create Child Workgroup Cancel button click its redirect to Parent Workgroup 
        /// </summary>
        /// <param name="workgroupId">The workgroupId</param>
        /// <returns>View</returns>
        public ActionResult EditParentWorkGroup(string workgroupId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0}", workgroupId));
                string userName = GetCurrentLoginName();
                PermissionCheckNdRedirect(GcsTasks.MaintainParentWorkgroup);
                if (string.IsNullOrEmpty(workgroupId)) return null;
                ClearWorkgroupSession();
                ViewBag.WorkgroupId = workgroupId;
                ViewBag.PageName = Constants.MaintainParentWorkgroupView;
                Workgroup workGroupDetails = _workgroupRepository.Workgroup(Convert.ToInt64(workgroupId), userName);
                if (_workgroupRepository.WorkgroupModel != null && _workgroupRepository.WorkgroupModel.Workgroup != null)
                {
                    var serializer = new JavaScriptSerializer();

                    if (workGroupDetails != null)
                    {
                        ViewBag.ModifiedDateTime = serializer.Serialize(workGroupDetails.ModifiedDateTime);
                        if (workGroupDetails.ChildWorkgroups.Any())
                        {
                            ViewBag.ChildWorkGroupDetails = workGroupDetails.ChildWorkgroups;
                        }
                        List<TerritorialDisplay> territorialDisplay = workGroupDetails.Territories != null ? workGroupDetails.Territories : null;
                        ViewBag.workGroupIdTerritory = workgroupId;

                        if (territorialDisplay != null && territorialDisplay.Any())
                        {
                            ViewBag.territoryDisplay = territorialDisplay;
                        }
                    }

                    if (_workgroupRepository.WorkgroupModel.Workgroup.Companies.Any())
                    {
                        var companyIds = new StringBuilder();
                        foreach (var company in _workgroupRepository.WorkgroupModel.Workgroup.Companies)
                        {
                            companyIds.Append(company.Id).Append(BusinessConstants.AddComma);
                        }
                        ViewBag.companyIds = companyIds.ToString();
                    }

                    if (_workgroupRepository.WorkgroupModel.Workgroup.Users.Any())
                    {
                        var userNames = new StringBuilder();
                        var userLoginNames = new StringBuilder();
                        var defaultUserLogiNames = new StringBuilder();
                        var defaultUserNames = new StringBuilder();
                        var usersInRole = new StringBuilder();
                        var usersCanMngworkgroup = new StringBuilder();
                        var usersR2Authorized = new StringBuilder();
                        var usersCanAllocateUpc = new StringBuilder();
                        var usersInRoleLogiNames = new StringBuilder();
                        var usersCanMngworkgroupLogiNames = new StringBuilder();
                        var usersR2AuthorizedLogiNames = new StringBuilder();
                        var usersCanAllocateUpcLogiNames = new StringBuilder();

                        foreach (var user in _workgroupRepository.WorkgroupModel.Workgroup.Users)
                        {
                            userNames.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                            userLoginNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName);
                            if (user.IsUserDefault)
                            {
                                defaultUserLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                                defaultUserNames.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                            }
                            if (user.IsInRole)
                            {
                                usersInRole.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersInRoleLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                            if (user.CanManageWorkgroup)
                            {
                                usersCanMngworkgroup.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersCanMngworkgroupLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                            if (user.IsR2Authorized)
                            {
                                usersR2Authorized.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersR2AuthorizedLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                            if (user.CanAllocateUpc)
                            {
                                usersCanAllocateUpc.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersCanAllocateUpcLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                        }

                        ViewBag.names = userNames.ToString();
                        ViewBag.loginNames = userLoginNames.ToString();
                        ViewBag.defaultUsers = defaultUserLogiNames.ToString();
                        ViewBag.defaultUserName = defaultUserNames.ToString();
                        ViewBag.UsersInRole = usersInRole.ToString();
                        ViewBag.UsersCanMngworkgroup = usersCanMngworkgroup.ToString();
                        ViewBag.UsersR2Authorized = usersR2Authorized.ToString();
                        ViewBag.UsersCanAllocateUPC = usersCanAllocateUpc.ToString();
                        ViewBag.UsersInRoleLogiNames = usersInRoleLogiNames.ToString();
                        ViewBag.UsersCanMngworkgroupLogiNames = usersCanMngworkgroupLogiNames.ToString();
                        ViewBag.UsersR2AuthorizedLogiNames = usersR2AuthorizedLogiNames.ToString();
                        ViewBag.UsersCanAllocateUPCLogiNames = usersCanAllocateUpcLogiNames.ToString();
                        ViewBag.ManageUsersListLogiNames = serializer.Serialize(_workgroupRepository.WorkgroupModel.Workgroup.Users);
                    }

                    ViewBag.defaultUserName = _workgroupRepository.WorkgroupModel.DefaultUserName;
                    ViewBag.RoleId = _workgroupRepository.WorkgroupModel.Workgroup.RoleID;
                    ViewBag.roleNameForEdit = _workgroupRepository.WorkgroupModel.Workgroup.RoleName;
                    ViewBag.maintainWorkgroupRoleId = _workgroupRepository.WorkgroupModel.Workgroup.RoleID;
                    SessionWrapper.WorkGroupUsers = _workgroupRepository.WorkgroupModel.Workgroup.Users;
                    ViewBag.ShowWGDetails = true;

                    LoggerFactory.LogWriter.MethodExit();

                    return View(Constants.MaintainParentWorkgroupView, _workgroupRepository.WorkgroupModel);
                }
                return null;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to view the Maintain ParentWorkgroup screen on edit view
        /// </summary>
        /// <param name="workgroupId">The workgroupId</param>
        /// <returns>Partial View</returns>
        public PartialViewResult MaintainParentWorkGroupWithData(string workgroupId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0}", workgroupId));
                string userName = GetCurrentLoginName();
                PermissionCheckNdRedirect(GcsTasks.MaintainParentWorkgroup);
                if (string.IsNullOrEmpty(workgroupId)) return null;
                ViewBag.PageName = Constants.MaintainParentWorkgroupView;
                ClearWorkgroupSession();
                ViewBag.WorkgroupId = workgroupId;
                Workgroup workGroupDetails = _workgroupRepository.Workgroup(Convert.ToInt64(workgroupId), userName);
                if (_workgroupRepository.WorkgroupModel != null && _workgroupRepository.WorkgroupModel.Workgroup != null)
                {
                    var serializer = new JavaScriptSerializer();
                    if (workGroupDetails != null)
                    {
                        ViewBag.ModifiedDateTime = serializer.Serialize(workGroupDetails.ModifiedDateTime);

                        if (workGroupDetails.ChildWorkgroups.Any())
                        {
                            ViewBag.ChildWorkGroupDetails = workGroupDetails.ChildWorkgroups;
                        }
                        List<TerritorialDisplay> territorialDisplay = workGroupDetails.Territories != null ? workGroupDetails.Territories : null;
                        ViewBag.workGroupIdTerritory = workgroupId;

                        if (territorialDisplay != null && territorialDisplay.Any())
                        {
                            ViewBag.territoryDisplay = territorialDisplay;
                        }
                    }

                    if (_workgroupRepository.WorkgroupModel.Workgroup.Companies.Any())
                    {
                        var companyIds = new StringBuilder();
                        foreach (var company in _workgroupRepository.WorkgroupModel.Workgroup.Companies)
                        {
                            companyIds.Append(company.Id).Append(BusinessConstants.AddComma);
                        }
                        ViewBag.companyIds = companyIds.ToString();
                        ViewBag.CompanyList = serializer.Serialize(_workgroupRepository.WorkgroupModel.Workgroup.Companies);
                    }

                    if (_workgroupRepository.WorkgroupModel.Workgroup.Users.Any())
                    {
                        var userNames = new StringBuilder();
                        var userLoginNames = new StringBuilder();
                        var defaultUserLogiNames = new StringBuilder();
                        var defaultUserNames = new StringBuilder();
                        var usersInRole = new StringBuilder();
                        var usersCanMngworkgroup = new StringBuilder();
                        var usersR2Authorized = new StringBuilder();
                        var usersCanAllocateUpc = new StringBuilder();
                        var usersInRoleLogiNames = new StringBuilder();
                        var usersCanMngworkgroupLogiNames = new StringBuilder();
                        var usersR2AuthorizedLogiNames = new StringBuilder();
                        var usersCanAllocateUpcLogiNames = new StringBuilder();

                        foreach (var user in _workgroupRepository.WorkgroupModel.Workgroup.Users)
                        {
                            userNames.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                            userLoginNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName);
                            if (user.IsUserDefault)
                            {
                                defaultUserLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                                defaultUserNames.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                            }
                            if (user.IsInRole)
                            {
                                usersInRole.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersInRoleLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                            if (user.CanManageWorkgroup)
                            {
                                usersCanMngworkgroup.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersCanMngworkgroupLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                            if (user.IsR2Authorized)
                            {
                                usersR2Authorized.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersR2AuthorizedLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                            if (user.CanAllocateUpc)
                            {
                                usersCanAllocateUpc.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                usersCanAllocateUpcLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                            }
                        }

                        ViewBag.names = userNames.ToString();
                        ViewBag.loginNames = userLoginNames.ToString();
                        ViewBag.defaultUsers = defaultUserLogiNames.ToString();
                        ViewBag.defaultUserName = defaultUserNames.ToString();
                        ViewBag.UsersInRole = usersInRole.ToString();
                        ViewBag.UsersCanMngworkgroup = usersCanMngworkgroup.ToString();
                        ViewBag.UsersR2Authorized = usersR2Authorized.ToString();
                        ViewBag.UsersCanAllocateUPC = usersCanAllocateUpc.ToString();
                        ViewBag.UsersInRoleLogiNames = usersInRoleLogiNames.ToString();
                        ViewBag.UsersCanMngworkgroupLogiNames = usersCanMngworkgroupLogiNames.ToString();
                        ViewBag.UsersR2AuthorizedLogiNames = usersR2AuthorizedLogiNames.ToString();
                        ViewBag.UsersCanAllocateUPCLogiNames = usersCanAllocateUpcLogiNames.ToString();
                        ViewBag.ManageUsersListLogiNames = serializer.Serialize(_workgroupRepository.WorkgroupModel.Workgroup.Users);
                    }

                    ViewBag.RoleId = _workgroupRepository.WorkgroupModel.Workgroup.RoleID.ToString(CultureInfo.InvariantCulture);
                    ViewBag.roleNameForEdit = _workgroupRepository.WorkgroupModel.Workgroup.RoleName;
                    ViewBag.maintainWorkgroupRoleId = _workgroupRepository.WorkgroupModel.Workgroup.RoleID;
                    SessionWrapper.WorkGroupUsers = _workgroupRepository.WorkgroupModel.Workgroup.Users;

                    LoggerFactory.LogWriter.MethodExit();
                    return PartialView(Constants.MaintainParentWorkgroupUpdateView, _workgroupRepository.WorkgroupModel);
                }
                return null;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to view child workgroup with datas
        /// </summary>
        /// <param name="workgroupId">The workgroupId</param>
        /// <param name="userInfo">The userInfo</param>
        public void GetChildWorkgroupWithData(string workgroupId, UserInfo userInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0}, userInfo: {1}", workgroupId, userInfo.UserName));

                if (!string.IsNullOrEmpty(workgroupId))
                {
                    ClearWorkgroupSession();
                    ChildWorkgroup workGroupDetails = _workgroupRepository.GetChildWorkgroup(Convert.ToInt64(workgroupId), userInfo.UserName);
                    if (_workgroupRepository.WorkgroupModel != null && _workgroupRepository.WorkgroupModel.Workgroup != null)
                    {

                        var serializer = new JavaScriptSerializer();
                        ViewBag.ModifiedDateTime = serializer.Serialize(workGroupDetails.ModifiedDateTime);

                        if (_workgroupRepository.WorkgroupModel.Workgroup.Companies != null && _workgroupRepository.WorkgroupModel.Workgroup.Companies.Any())
                        {
                            var companyIds = new StringBuilder();
                            foreach (var company in _workgroupRepository.WorkgroupModel.Workgroup.Companies)
                            {
                                companyIds.Append(company.Id).Append(BusinessConstants.AddComma);
                            }
                            ViewBag.companyIds = companyIds.ToString();
                            ViewBag.CompanyList = serializer.Serialize(_workgroupRepository.WorkgroupModel.Workgroup.Companies);
                        }

                        if (_workgroupRepository.WorkgroupModel.Workgroup.Users != null && _workgroupRepository.WorkgroupModel.Workgroup.Users.Any())
                        {
                            var userNames = new StringBuilder();
                            var userLoginNames = new StringBuilder();
                            var defaultUserLogiNames = new StringBuilder();
                            var usersInRole = new StringBuilder();
                            var usersCanMngworkgroup = new StringBuilder();
                            var usersR2Authorized = new StringBuilder();
                            var usersCanAllocateUpc = new StringBuilder();
                            var usersInRoleLogiNames = new StringBuilder();
                            var usersCanMngworkgroupLogiNames = new StringBuilder();
                            var usersR2AuthorizedLogiNames = new StringBuilder();
                            var usersCanAllocateUpcLogiNames = new StringBuilder();
                            foreach (var user in _workgroupRepository.WorkgroupModel.Workgroup.Users)
                            {
                                userNames.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                userLoginNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName);
                                if (user.IsUserDefault)
                                {
                                    defaultUserLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName);
                                }
                                if (user.IsInRole)
                                {
                                    usersInRole.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                    usersInRoleLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                                }
                                if (user.CanManageWorkgroup)
                                {
                                    usersCanMngworkgroup.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                    usersCanMngworkgroupLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                                }
                                if (user.IsR2Authorized)
                                {
                                    usersR2Authorized.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                    usersR2AuthorizedLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                                }
                                if (user.CanAllocateUpc)
                                {
                                    usersCanAllocateUpc.AppendFormat(BusinessConstants.SemiColonFormatWithSpace, user.Name);
                                    usersCanAllocateUpcLogiNames.AppendFormat(BusinessConstants.CommaFormat, user.LoginName.Trim());
                                }
                            }
                            ViewBag.names = userNames.ToString();
                            ViewBag.loginNames = userLoginNames.ToString();
                            ViewBag.defaultUsers = defaultUserLogiNames.ToString();
                            ViewBag.UsersInRole = usersInRole.ToString();
                            ViewBag.UsersCanMngworkgroup = usersCanMngworkgroup.ToString();
                            ViewBag.UsersR2Authorized = usersR2Authorized.ToString();
                            ViewBag.UsersCanAllocateUPC = usersCanAllocateUpc.ToString();
                            ViewBag.UsersInRoleLogiNames = usersInRoleLogiNames.ToString();
                            ViewBag.UsersCanMngworkgroupLogiNames = usersCanMngworkgroupLogiNames.ToString();
                            ViewBag.UsersR2AuthorizedLogiNames = usersR2AuthorizedLogiNames.ToString();
                            ViewBag.UsersCanAllocateUPCLogiNames = usersCanAllocateUpcLogiNames.ToString();
                            ViewBag.ChildWkgMngUsersList = serializer.Serialize(_workgroupRepository.WorkgroupModel.Workgroup.Users);
                        }

                        ViewBag.defaultUserName = _workgroupRepository.WorkgroupModel.DefaultUserName;

                        if (workGroupDetails != null)
                        {
                            ViewBag.workGroupId = workGroupDetails.ParentId;
                            ViewBag.ChildWorkGroupName = workGroupDetails.Name;

                            List<TerritorialDisplay> territorialDisplay = workGroupDetails.Territories != null ? workGroupDetails.Territories : null;
                            ViewBag.workGroupIdTerritory = workgroupId;
                            if (territorialDisplay != null && territorialDisplay.Any())
                            {
                                ViewBag.territoryDisplay = territorialDisplay;
                            }

                            if (workGroupDetails.RequestTypesLookup != null && workGroupDetails.RequestTypesLookup.Any())
                            {
                                var checkArray = new List<string>();
                                foreach (var keyValues in workGroupDetails.RequestTypesLookup)
                                {
                                    checkArray.Add(keyValues.Value);
                                }
                                ViewBag.RequestTypes = checkArray;
                                ViewBag.RequestTypeList = serializer.Serialize(workGroupDetails.DeviationRequestTypes);
                            }


                            if (workGroupDetails.ArtistContracts != null && workGroupDetails.ArtistContracts.Any())
                            {
                                var artistContract = new StringBuilder();
                                foreach (var artist in workGroupDetails.ArtistContracts)
                                {
                                    artistContract.Append(artist.ContractId).Append(BusinessConstants.LineSeperator);
                                }
                                ViewBag.ArtistContractIds = artistContract.ToString();
                                ViewBag.ArtistContractList = serializer.Serialize(workGroupDetails.ArtistContracts);
                            }

                            if (workGroupDetails.ResourceContracts != null && workGroupDetails.ResourceContracts.Any())
                            {
                                var resourceContract = new StringBuilder();
                                foreach (var resource in workGroupDetails.ResourceContracts)
                                {
                                    resourceContract.Append(resource.ContractId + BusinessConstants.AddComma + resource.ResourceId + BusinessConstants.LineSeperator);
                                }
                                ViewBag.ResourceContractIds = resourceContract.ToString();
                                ViewBag.ResourceContractList = serializer.Serialize(workGroupDetails.ResourceContracts);
                            }
                        }

                        ViewBag.RoleId = _workgroupRepository.WorkgroupModel.Workgroup.RoleID;
                        ViewBag.roleNameForEdit = _workgroupRepository.WorkgroupModel.Workgroup.RoleName;
                        ViewBag.maintainWorkgroupRoleId = _workgroupRepository.WorkgroupModel.Workgroup.RoleID;
                        SessionWrapper.WorkGroupUsers = _workgroupRepository.WorkgroupModel.Workgroup.Users;

                        LoggerFactory.LogWriter.MethodExit();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get child workgroup layout and details partial view
        /// </summary>
        /// <param name="workgroupId">The workgroupId</param>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult GetChildWorkGroup(string workgroupId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0}", workgroupId));

                var userInfo = new UserInfo { UserLoginName = GetCurrentLoginName() };
                PermissionCheckNdRedirect(GcsTasks.MaintainChildWorkgroup);
                ViewBag.PageName = BusinessConstants.MaintainChildWorkgroup;
                GetChildWorkgroupWithData(workgroupId, userInfo);
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.MaintainChildWorkGroup, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get child workgroup details partial view
        /// </summary>
        /// <param name="workgroupId">The workgroupId</param>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult GetChildWorkGroupPartial(string workgroupId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0}", workgroupId));
                var userInfo = new UserInfo { UserLoginName = GetCurrentLoginName() };
                PermissionCheckNdRedirect(GcsTasks.MaintainChildWorkgroup);
                ViewBag.PageName = BusinessConstants.MaintainChildWorkgroup;
                GetChildWorkgroupWithData(workgroupId, userInfo);
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.CreateChildWorkGroupPartial, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        /// <summary>
        /// This method to Create Parent Workgroup save logics
        /// </summary>
        /// <param name="collection">FormCollection</param>
        /// <returns>string</returns>
        [HttpPost]
        public string CreateParentWorkgroupSave(FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                PermissionCheckNdRedirect(GcsTasks.CreateParentWorkgroup);
                var workgroupDetails = new Workgroup();
                string userName = GetCurrentLoginName();
                var resultInfo = string.Empty;
                ViewBag.ValidationMsg = string.Empty;
                if (Request.Form.Count > 0)
                {
                    workgroupDetails.Name = collection["txtWorkgroupName"].ToString(CultureInfo.InvariantCulture);
                    workgroupDetails.R2Contract = collection["txtR2Contact"].ToString(CultureInfo.InvariantCulture);
                    workgroupDetails.RoleID = int.Parse((collection["RolesList"] == null) ? string.Empty : collection["RolesList"].ToString(CultureInfo.InvariantCulture));

                    workgroupDetails.Territories = GetTerritoryDisplayList(collection);
                    if (collection["hdnincludeTerritoryString"] != null && collection["hdnincludeTerritoryString"] != string.Empty)
                    {
                        workgroupDetails.IncludedTerritories = collection["hdnincludeTerritoryString"].ToString(CultureInfo.InvariantCulture);
                    }
                    if (collection["hdnexcludeTerritoryString"] != null && collection["hdnexcludeTerritoryString"] != string.Empty)
                    {
                        workgroupDetails.ExcludedTerritories = collection["hdnexcludeTerritoryString"].ToString(CultureInfo.InvariantCulture);
                    }
                    if (collection["hdnCompanyIds"] != null && collection["hdnCompanyIds"] != string.Empty)
                    {
                        var companylist = collection["hdnCompanyIds"].ToString(CultureInfo.InvariantCulture);
                        if (companylist.Length > 0)
                        {
                            var companyDetails = companylist.Split(new char[] { Convert.ToChar(BusinessConstants.AddComma) }, StringSplitOptions.RemoveEmptyEntries);
                            workgroupDetails.Companies = new List<CompanyInfo>();
                            foreach (var company in companyDetails)
                            {
                                workgroupDetails.Companies.Add(new CompanyInfo { Id = long.Parse(company) });
                            }
                        }
                    }

                    string userInfoJsonObject = collection["hdnUserDetailsForSave"].ToString(CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(userInfoJsonObject))
                    {
                        var jsonSerialize = new JavaScriptSerializer();
                        var userDetails = jsonSerialize.Deserialize<List<WorkGroupUser>>(userInfoJsonObject);
                        workgroupDetails.Users = userDetails;
                    }

                    if (workgroupDetails != null && !string.IsNullOrEmpty(userName))
                    {
                        resultInfo = _workgroupRepository.AddWorkgroup(workgroupDetails, userName);
                    }
                }

                if (!string.IsNullOrEmpty(resultInfo))
                {
                    ViewBag.ValidationMsg = ClearenceResource.ParentWorkgroupDataExist;
                    LoggerFactory.LogWriter.MethodExit();
                    return ViewBag.ValidationMsg;
                }

                LoggerFactory.LogWriter.MethodExit();
                return string.Empty;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to Update the workgroup details
        /// </summary>
        /// <param name="collection">The form collection</param>
        /// <returns>string</returns>
        [HttpPost]
        public ActionResult UpdateParentWorkgroup(FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                PermissionCheckNdRedirect(GcsTasks.MaintainParentWorkgroup);
                var workgroupDetails = new Workgroup();
                string userName = GetCurrentLoginName();
                var resultInfo = false;
                ViewBag.ValidationMsg = string.Empty;
                var jsonSerialize = new JavaScriptSerializer();
                if (!string.IsNullOrEmpty(collection["hdnWorkgroupId"]))
                {
                    var workgroupId = Convert.ToInt64(collection["hdnWorkgroupId"]);
                    if (Request.Form.Count > 0)
                    {
                        workgroupDetails.ID = workgroupId;
                        workgroupDetails.Name = collection["txtWorkgroupName"].ToString(CultureInfo.InvariantCulture);
                        workgroupDetails.R2Contract = collection["txtR2Contact"].ToString(CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(collection["hdnRoleId"]))
                            workgroupDetails.RoleID = Convert.ToInt32(collection["hdnRoleId"]);

                        workgroupDetails.Territories = GetTerritoryDisplayList(collection);
                        if (collection["hdnincludeTerritoryString"] != null && collection["hdnincludeTerritoryString"] != string.Empty)
                        {
                            workgroupDetails.IncludedTerritories = collection["hdnincludeTerritoryString"].ToString(CultureInfo.InvariantCulture);
                        }
                        if (collection["hdnexcludeTerritoryString"] != null && collection["hdnexcludeTerritoryString"] != string.Empty)
                        {
                            workgroupDetails.ExcludedTerritories = collection["hdnexcludeTerritoryString"].ToString(CultureInfo.InvariantCulture);
                        }
                        var serializer = new JavaScriptSerializer();
                        workgroupDetails.ModifiedDateTime = serializer.Deserialize<DateTime>(collection["hdnModifiedTime"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();

                        if (collection["hdnCompanyIds"] != null && collection["hdnCompanyIds"] != string.Empty)
                        {
                            var companylist = collection["hdnCompanyIds"].ToString(CultureInfo.InvariantCulture);
                            if (companylist.Length > 0)
                            {
                                var companyDetails = companylist.Split(new char[] { Convert.ToChar(BusinessConstants.AddComma) }, StringSplitOptions.RemoveEmptyEntries);
                                workgroupDetails.Companies = new List<CompanyInfo>();
                                List<CompanyInfo> companyInfos = null;
                                if (collection["hdnJsonCompanyList"] != null)
                                {
                                    companyInfos = serializer.Deserialize<List<CompanyInfo>>(collection["hdnJsonCompanyList"].ToString(CultureInfo.InvariantCulture));
                                }

                                foreach (var company in companyDetails)
                                {
                                    if (companyInfos != null)
                                    {
                                        var companyInfo = (from comp in companyInfos where comp.Id == long.Parse(company) select comp).FirstOrDefault();
                                        workgroupDetails.Companies.Add(companyInfo == null
                                                                                ? new CompanyInfo { Id = long.Parse(company) }
                                                                                : new CompanyInfo
                                                                                {
                                                                                    Id = long.Parse(company),
                                                                                    ModifiedDateTime = companyInfo.ModifiedDateTime.ToLocalTime()
                                                                                });
                                    }
                                    else
                                    {
                                        workgroupDetails.Companies.Add(new CompanyInfo { Id = long.Parse(company) });
                                    }
                                }
                            }
                        }
                        workgroupDetails.Users = new List<WorkGroupUser>();
                        string userInfoJsonObject = collection["hdnUserDetailsForSave"].ToString(CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(userInfoJsonObject))
                        {
                            var userDetails = serializer.Deserialize<List<WorkGroupUser>>(userInfoJsonObject);
                            List<WorkGroupUser> userInfos = null;
                            if (collection["hdnJsonManageUsersList"] != null)
                            {
                                userInfos = serializer.Deserialize<List<WorkGroupUser>>(collection["hdnJsonManageUsersList"].ToString(CultureInfo.InvariantCulture));
                            }
                            foreach (var addedUser in userDetails)
                            {
                                if (userInfos != null)
                                {
                                    var wkgMngUserInfo = (from user in userInfos where user.LoginName == addedUser.LoginName select user).FirstOrDefault();
                                    workgroupDetails.Users.Add(wkgMngUserInfo == null ? new WorkGroupUser
                                    {
                                        Name = addedUser.Name,
                                        LoginName = addedUser.LoginName,
                                        Email = addedUser.Email,
                                        IsUserDefault = addedUser.IsUserDefault,
                                        IsInRole = addedUser.IsInRole,
                                        CanManageWorkgroup = addedUser.CanManageWorkgroup,
                                        IsR2Authorized = addedUser.IsR2Authorized,
                                        CanAllocateUpc = addedUser.CanAllocateUpc
                                    }
                                                                                    : new WorkGroupUser
                                                                                    {
                                                                                        Name = addedUser.Name,
                                                                                        LoginName = addedUser.LoginName,
                                                                                        Email = addedUser.Email,
                                                                                        IsUserDefault = addedUser.IsUserDefault,
                                                                                        IsInRole = addedUser.IsInRole,
                                                                                        CanManageWorkgroup = addedUser.CanManageWorkgroup,
                                                                                        IsR2Authorized = addedUser.IsR2Authorized,
                                                                                        CanAllocateUpc = addedUser.CanAllocateUpc,
                                                                                        ModifiedDateTime = wkgMngUserInfo.ModifiedDateTime.ToLocalTime()
                                                                                    });
                                }
                            }
                            if (userInfos == null)
                            {
                                workgroupDetails.Users = userDetails;
                            }
                        }
                        else
                        {
                            List<WorkGroupUser> userInfos = null;
                            if (collection["hdnJsonManageUsersList"] != null)
                            {
                                userInfos = serializer.Deserialize<List<WorkGroupUser>>(collection["hdnJsonManageUsersList"].ToString(CultureInfo.InvariantCulture));
                            }
                            workgroupDetails.Users = userInfos;
                        }
                        if (workgroupDetails != null && !string.IsNullOrEmpty(userName))
                        {
                            resultInfo = _workgroupRepository.UpdateWorkGroup(workgroupDetails, userName);
                        }
                    }
                }
                if (!resultInfo)
                {
                    ViewBag.ValidationMsg = ClearenceResource.ParentWorkgroupDataExist;
                    return Json(ViewBag.ValidationMsg);
                }
                Workgroup updatedWorkgroup = _workgroupRepository.Workgroup(workgroupDetails.ID, userName);
                var data = new List<string>();
                if (updatedWorkgroup != null)
                {
                    data.Add(jsonSerialize.Serialize(updatedWorkgroup.ModifiedDateTime));
                    data.Add(jsonSerialize.Serialize(updatedWorkgroup.Companies));
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(data);
            }
            catch (System.ServiceModel.FaultException fx)
            {
                if (fx.Code.Name == GrsErrorCode.ConcurrencyErrorCode)
                {
                    ViewBag.ValidationMsg = fx.Message;
                    return Json(ViewBag.ValidationMsg);
                }
                throw;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(null);
            }
        }

        /// <summary>
        /// Used to view the Delete workgroup partial view
        /// </summary>
        /// <returns>Partial view</returns>
        public ActionResult DeleteWorkgroup()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                PermissionCheckNdRedirect(new[] { GcsTasks.DeleteWorkgroup });
                LoggerFactory.LogWriter.MethodExit();
                return View(Constants.DeleteWorkgroupView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Delete the specified workgroup
        /// </summary>
        /// <param name="workgroupId">Used to delete the workgroup</param>
        /// <returns>JSON formatted content</returns>
        [HttpPost]
        public JsonResult DeleteWorkgroup(long workgroupId, string modifiedDateTime, string Id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupId: {0}", workgroupId));
                string userName = GetCurrentLoginName();
                var serializer = new JavaScriptSerializer();
                DateTime modifiedDate;

                if (Id == BusinessConstants.IdSearchPage)
                {
                    modifiedDateTime = modifiedDateTime.Replace(BusinessConstants.Slash, string.Empty);
                    modifiedDate = serializer.Deserialize<DateTime>(BusinessConstants.DateLiteralLeft + modifiedDateTime + BusinessConstants.DateLiteralRight).ToLocalTime();
                }
                else
                {
                    modifiedDate = serializer.Deserialize<DateTime>(modifiedDateTime).ToLocalTime();
                }

                PermissionCheckNdRedirect(new[] { GcsTasks.DeleteWorkgroup });
                bool result = _workgroupRepository.DeleteWorkgroup(workgroupId, userName, modifiedDate);
                LoggerFactory.LogWriter.MethodExit();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (System.ServiceModel.FaultException fx)
            {
                return Json(new { Result = fx.Message, fx.Message });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// This method to view the Create Child Workgroup Screen
        /// </summary>
        /// <returns>ViewResult</returns>
        public ViewResult CreateChildWorkgroup(string parentWorkgroupId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string userName = GetCurrentLoginName();
                var serializer = new JavaScriptSerializer();
                PermissionCheckNdRedirect(GcsTasks.CreateChildWorkgroup);
                ViewBag.workGroupId = parentWorkgroupId;
                ViewBag.PageName = BusinessConstants.CreateChildWorkgroup;
                var workGroupDetails = _workgroupRepository.Workgroup(Convert.ToInt64(parentWorkgroupId), userName);
                ViewBag.workGroupIdTerritory = Convert.ToInt64(parentWorkgroupId);
                ViewBag.parentWorkgroupId = workGroupDetails.ParentID;
                var territorialDisplay = workGroupDetails.Territories;
                if (territorialDisplay != null)
                {
                    ViewBag.territoryDisplay = territorialDisplay;
                }
                ViewBag.maintainWorkgroupRoleId = workGroupDetails.RoleID;
                ViewBag.roleNameForEdit = workGroupDetails.RoleName;
                ViewBag.RequestTypeList = serializer.Serialize(_workgroupRepository.WorkgroupModel.RequestTypeList);

                LoggerFactory.LogWriter.MethodExit();
                return View(Constants.CreateChildWorkgroupView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// This method to Create Child Workgroup save logics 
        /// </summary>
        /// <param name="collections">FormCollection</param>
        /// <returns>string</returns>
        [HttpPost]
        public ActionResult CreateChildWorkgroupSave(FormCollection collections)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                PermissionCheckNdRedirect(new[] { GcsTasks.CreateChildWorkgroup, GcsTasks.MaintainChildWorkgroup });
                var childWorkgroupDetails = new ChildWorkgroup();
                var resultInfo = string.Empty;
                ViewBag.ValidationMsg = string.Empty;
                if (Request.Form.Count > 0)
                {
                    childWorkgroupDetails.UserLoginName = GetCurrentLoginName();
                    childWorkgroupDetails.ParentId = long.Parse(collections["hdnWorkgroupId"].ToString(CultureInfo.InvariantCulture));
                    childWorkgroupDetails.Name = collections["txtWorkgroupName"].ToString(CultureInfo.InvariantCulture);
                    childWorkgroupDetails.R2Contract = collections["hdnR2Contract"].ToString(CultureInfo.InvariantCulture);
                    childWorkgroupDetails.RoleId = long.Parse(collections["hdnRoleId"].ToString(CultureInfo.InvariantCulture));

                    var serializer = new JavaScriptSerializer();

                    if (collections["hdnCompanyIds"] != null && collections["hdnCompanyIds"] != string.Empty)
                    {
                        var companylist = collections["hdnCompanyIds"].ToString(CultureInfo.InvariantCulture);
                        if (companylist.Length > 0)
                        {
                            var companyDetails = companylist.Split(new char[] { Convert.ToChar(BusinessConstants.AddComma) }, StringSplitOptions.RemoveEmptyEntries);
                            childWorkgroupDetails.Companies = new List<CompanyInfo>();
                            List<CompanyInfo> companyInfos = null;
                            if (collections["hdnJsonCompanyList"] != null)
                            {
                                companyInfos = serializer.Deserialize<List<CompanyInfo>>(collections["hdnJsonCompanyList"].ToString(CultureInfo.InvariantCulture));
                            }

                            foreach (var company in companyDetails)
                            {
                                if (companyInfos != null)
                                {
                                    var companyInfo = (from comp in companyInfos where comp.Id == long.Parse(company) select comp).FirstOrDefault();
                                    childWorkgroupDetails.Companies.Add(companyInfo == null
                                                                            ? new CompanyInfo { Id = long.Parse(company) }
                                                                            : new CompanyInfo
                                                                            {
                                                                                Id = long.Parse(company),
                                                                                ModifiedDateTime = companyInfo.ModifiedDateTime.ToLocalTime()
                                                                            });
                                }
                                else
                                {
                                    childWorkgroupDetails.Companies.Add(new CompanyInfo { Id = long.Parse(company) });
                                }
                            }
                        }
                    }
                    childWorkgroupDetails.Users = new List<WorkGroupUser>();
                    string userInfoJsonObject = collections["hdnUserDetailsForSave"].ToString(CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(userInfoJsonObject))
                    {
                        var userDetails = serializer.Deserialize<List<WorkGroupUser>>(userInfoJsonObject);
                        List<WorkGroupUser> userInfos = null;
                        if (collections["hdnJsonChildWkgMngUsersList"] != null)
                        {
                            userInfos = serializer.Deserialize<List<WorkGroupUser>>(collections["hdnJsonChildWkgMngUsersList"].ToString(CultureInfo.InvariantCulture));
                        }
                        foreach (var addedUser in userDetails)
                        {
                            if (userInfos != null)
                            {
                                var wkgMngUserInfo = (from user in userInfos where user.LoginName == addedUser.LoginName select user).FirstOrDefault();
                                childWorkgroupDetails.Users.Add(
                                                                wkgMngUserInfo == null ? 
                                                                new WorkGroupUser
                                                                {
                                                                    Name = addedUser.Name,
                                                                    LoginName = addedUser.LoginName,
                                                                    Email = addedUser.Email,
                                                                    IsUserDefault = addedUser.IsUserDefault,
                                                                    IsInRole = addedUser.IsInRole,
                                                                    CanManageWorkgroup = addedUser.CanManageWorkgroup,
                                                                    IsR2Authorized = addedUser.IsR2Authorized,
                                                                    CanAllocateUpc = addedUser.CanAllocateUpc
                                                                }
                                                                : new WorkGroupUser
                                                                {
                                                                    Name = addedUser.Name,
                                                                    LoginName = addedUser.LoginName,
                                                                    Email = addedUser.Email,
                                                                    IsUserDefault = addedUser.IsUserDefault,
                                                                    IsInRole = addedUser.IsInRole,
                                                                    CanManageWorkgroup = addedUser.CanManageWorkgroup,
                                                                    IsR2Authorized = addedUser.IsR2Authorized,
                                                                    CanAllocateUpc = addedUser.CanAllocateUpc,
                                                                    ModifiedDateTime = wkgMngUserInfo.ModifiedDateTime.ToLocalTime()
                                                                });
                            }
                        }
                        if (userInfos == null)
                        {
                            childWorkgroupDetails.Users = userDetails;
                        }
                    }
                    else
                    {
                        List<WorkGroupUser> userInfos = null;
                        if (collections["hdnJsonChildWkgMngUsersList"] != null)
                        {
                            userInfos = serializer.Deserialize<List<WorkGroupUser>>(collections["hdnJsonChildWkgMngUsersList"].ToString(CultureInfo.InvariantCulture));
                            childWorkgroupDetails.Users = userInfos;
                        }
                    }

                    GetRequestType(collections, childWorkgroupDetails);

                    var requestTypes = new List<DeviationRequestType>();
                    if (collections["hdnRequestTypeList"] != null)
                    {
                        requestTypes = serializer.Deserialize<List<DeviationRequestType>>(collections["hdnRequestTypeList"].ToString(CultureInfo.InvariantCulture));
                    }
                    if (requestTypes != null)
                    {
                        if (childWorkgroupDetails.RequestTypes != null)
                        {
                            childWorkgroupDetails.DeviationRequestTypes = new List<DeviationRequestType>();
                            foreach (var request in childWorkgroupDetails.RequestTypes)
                            {
                                var requestDetails = (from req in requestTypes where req.Id == request.Value select req).SingleOrDefault();
                                if (requestDetails != null)
                                {
                                    childWorkgroupDetails.DeviationRequestTypes.Add(new DeviationRequestType { Id = requestDetails.Id, Name = requestDetails.Name, ModifiedDateTime = requestDetails.ModifiedDateTime.ToLocalTime() });
                                }
                            }
                        }
                    }

                    if (collections["hdnManageArtistIds"] != null)
                    {
                        var devArtistContract = collections["hdnManageArtistIds"].ToString(CultureInfo.InvariantCulture);
                        if (devArtistContract.Length > 0)
                        {
                            var arrContractIDs = devArtistContract.Split(new char[] { BusinessConstants.SingleOrSeperator }, StringSplitOptions.RemoveEmptyEntries);
                            childWorkgroupDetails.ArtistContracts = new List<DeviationArtistContract>();
                            List<DeviationArtistContract> deviationArtistContract = null;
                            if (collections["hdnArtistContractList"] != null)
                            {
                                deviationArtistContract = serializer.Deserialize<List<DeviationArtistContract>>(collections["hdnArtistContractList"].ToString(CultureInfo.InvariantCulture));
                            }

                            foreach (var value in arrContractIDs)
                            {
                                if (deviationArtistContract != null)
                                {
                                    var deviationArtist = (from dev in deviationArtistContract where dev.ContractId == long.Parse(value) select dev).FirstOrDefault();
                                    childWorkgroupDetails.ArtistContracts.Add(deviationArtist == null
                                                                                  ? new DeviationArtistContract { ContractId = long.Parse(value) }
                                                                                  : new DeviationArtistContract
                                                                                  {
                                                                                      ContractId = long.Parse(value),
                                                                                      ModifiedDateTime = deviationArtist.ModifiedDateTime.ToLocalTime()
                                                                                  });
                                }
                                else
                                {
                                    childWorkgroupDetails.ArtistContracts.Add(new DeviationArtistContract { ContractId = long.Parse(value) });
                                }
                            }
                        }
                    }
                    if (collections["hdnManageResourceIds"] != null)
                    {
                        var devResourceContract = collections["hdnManageResourceIds"].ToString(CultureInfo.InvariantCulture);
                        if (devResourceContract.Length > 0)
                        {
                            var jsonSerialize = new JavaScriptSerializer();
                            var resourceContractDetails = jsonSerialize.Deserialize<List<DeviationResourceContract>>(devResourceContract);

                            childWorkgroupDetails.ResourceContracts = new List<DeviationResourceContract>();
                            List<DeviationResourceContract> deviationResourceContract = null;
                            if (collections["hdnResourceContractList"] != null)
                            {
                                deviationResourceContract = serializer.Deserialize<List<DeviationResourceContract>>(collections["hdnResourceContractList"].ToString(CultureInfo.InvariantCulture));
                            }
                            foreach (var resourceContractIds in resourceContractDetails)
                            {
                                if (deviationResourceContract != null)
                                {
                                    var deviationResource = (from dev in deviationResourceContract where dev.ContractId == resourceContractIds.ContractId && dev.ResourceId == resourceContractIds.ResourceId select dev).FirstOrDefault();
                                    childWorkgroupDetails.ResourceContracts.Add(deviationResource == null
                                                                                    ? new DeviationResourceContract { ContractId = resourceContractIds.ContractId, ResourceId = resourceContractIds.ResourceId }
                                                                                    : new DeviationResourceContract
                                                                                          {
                                                                                              ContractId = resourceContractIds.ContractId,
                                                                                              ResourceId = resourceContractIds.ResourceId,
                                                                                              ModifiedDateTime = deviationResource.ModifiedDateTime.ToLocalTime()
                                                                                          });
                                }
                                else
                                {
                                    childWorkgroupDetails.ResourceContracts.Add(new DeviationResourceContract { ContractId = resourceContractIds.ContractId, ResourceId = resourceContractIds.ResourceId });
                                }
                            }


                        }
                    }
                    string hiddenPageName = collections["hiddenPageName"].ToString(CultureInfo.InvariantCulture);
                    if (string.Compare(hiddenPageName, BusinessConstants.CreateChildWorkgroup, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        resultInfo = _workgroupRepository.AddChildWorkgroup(childWorkgroupDetails);
                    }
                    else
                    {
                        childWorkgroupDetails.ParentId = long.Parse(collections["hdnParentId"].ToString(CultureInfo.InvariantCulture));
                        childWorkgroupDetails.Id = Convert.ToInt64(collections["hiddenWorkgroupId"]);
                        childWorkgroupDetails.ModifiedDateTime = serializer.Deserialize<DateTime>(collections["hdnModifiedTime"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                        bool result = _workgroupRepository.UpdateChildWorkGroup(childWorkgroupDetails);
                        if (!result)
                        {
                            ViewBag.ValidationMsg = ClearenceResource.ParentWorkgroupDataExist;
                            return Json(ViewBag.ValidationMsg);
                        }
                        var jsonSerialize = new JavaScriptSerializer();

                        ChildWorkgroup workGroupDetails = _workgroupRepository.GetChildWorkgroup(Convert.ToInt64(childWorkgroupDetails.Id), childWorkgroupDetails.UserLoginName);
                        var data = new List<string>();
                        if (workGroupDetails != null)
                        {
                            data.Add(jsonSerialize.Serialize(workGroupDetails.ModifiedDateTime));
                            data.Add(jsonSerialize.Serialize(workGroupDetails.Companies));
                        }
                        LoggerFactory.LogWriter.Info(BusinessConstants.MsgParentWorkgroupUpdation);
                        LoggerFactory.LogWriter.Debug(BusinessConstants.MsgParentWorkgroupUpdation);
                        return Json(data);
                    }
                }
                if (!string.IsNullOrEmpty(resultInfo))
                {
                    ViewBag.ValidationMsg = ClearenceResource.ParentWorkgroupDataExist;
                    return Json(ViewBag.ValidationMsg);
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(string.Empty);
            }
            catch (System.ServiceModel.FaultException fx)
            {
                if (fx.Code.Name == GrsErrorCode.ConcurrencyErrorCode)
                {
                    ViewBag.ValidationMsg = fx.Message;
                    return Json(ViewBag.ValidationMsg);
                }
                throw;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to view the SearchWorkGroup view
        /// </summary>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult PartialSearchWorkgroup()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return PartialView(Constants.SearchWorkGroupView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Search workgroup with parameters including contract id.
        /// </summary>
        /// <param name="workgroupName">Search this text contains in any workgroup name from the table</param>
        /// <param name="workgroupRole">The workgroupRole</param>
        /// <param name="workgroupCompany">Search this text contains in any workgroup company name from the table</param>
        /// <param name="workgroupUser">Search this text contains in any workgroup user name from the table</param>
        /// <param name="workgroupCountry">Search this text contains in any workgroup country name from the table</param>
        /// <param name="jtStartIndex">querystring from jtable for startIndex</param>
        /// <param name="jtPageSize">querystring from jtable for pagesize</param>
        /// <param name="contractIds">The contractIds</param>
        /// <param name="jtSorting">sorting detail</param>
        /// <returns>Workgroup details formatted as JSON content</returns>
        [HttpPost]
        public JsonResult GetWorkgroups(string workgroupName, int workgroupRole, string workgroupCompany, string workgroupUser, string workgroupCountry, int jtStartIndex, int jtPageSize, string contractIds, string jtSorting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupName: {0},workgroupRole: {1},workgroupCompany: {2},workgroupUser: {3},workgroupCountry: {4},contractIds: {5}", workgroupName, workgroupRole, workgroupCompany, workgroupUser, workgroupCountry, contractIds));
                var workGroupSearchCriteria = new WorkgroupSearchCriteria
                {
                    Name = workgroupName,
                    Role = workgroupRole,
                    Company = workgroupCompany,
                    User = workgroupUser,
                    Country = workgroupCountry,
                    StartIndex = jtStartIndex,
                    PageSize = jtPageSize,
                    SortField = jtSorting,
                    UserLoginName = SessionWrapper.CurrentUserInfo.UserLoginName
                };
                var rccAdmin = SessionWrapper.GcsCurrentPermissions;
                if (rccAdmin.Roles != null)
                {
                    workGroupSearchCriteria.IsRccAdmin = rccAdmin.Roles.Where(roles => roles.Value == BusinessConstants.RCCAdmin).GroupBy(role => role.Value).Count() != 0;
                }
                int totalRowCount = 0;
                List<WorkgroupSearchResult> workgroupList = _workgroupRepository.GetWorkgroups(workGroupSearchCriteria, contractIds);
                if (workgroupList != null)
                {
                    if (workgroupList.Count > 0)
                    {
                        totalRowCount = workgroupList[0].TotalRows;
                    }
                }
                else
                {
                    workgroupList = new List<WorkgroupSearchResult>();
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = workgroupList.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        /// <summary>
        /// Suggestive search
        /// </summary>
        /// <param name="suggestiveInput">The suggestiveInput</param>
        /// <param name="workgroupElement">The workgroupElement</param>
        /// <param name="pageName">The pageName</param>
        /// <param name="workgroupId">The workgroupId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>JSON formatted result</returns>
        [OutputCache(Duration = 0)]
        public JsonResult SuggestiveSearchForWorkgroup(string suggestiveInput, string workgroupElement, string pageName, string workgroupId, UserInfo userInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("suggestiveInput: {0}, workgroupElement: {1}, workgroupId: {2}, userInfo: {3}", suggestiveInput, workgroupElement, workgroupId, userInfo.UserName));
                List<string> suggestiveResultList = _workgroupRepository.SuggestiveSearchForWorkgroup(suggestiveInput, workgroupElement, pageName, workgroupId, userInfo.UserLoginName);
                LoggerFactory.LogWriter.MethodExit();
                return Json(suggestiveResultList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        #endregion

        #region Manage Artist/Resource/Request and Contracts

        /// <summary>
        /// Used to view Manage ArtistContract View
        /// </summary>
        /// <param name="id">The pagename</param>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult SearchforManageArtist(string id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.pageName = id;
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ManageArtistContractView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to view Manage ResourceContract View
        /// </summary>
        /// <param name="id">The pagename</param>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult SearchforManageResourceArtist(string id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.pageName = id;
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ManageResourceContractView, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to deserialize the given JSON object.
        /// </summary>
        /// <param name="artistData">The JSON object</param>
        /// <returns>JSON formatted content</returns>
        [HttpPost]
        public JsonResult DisplayManageArtistContract(string artistData)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<ArtistSearchCriteria> getArtistContracts;
                if (!string.IsNullOrEmpty(artistData))
                {
                    artistData = artistData.Replace(BusinessConstants.SlashWithSingleQuote, BusinessConstants.SlashWithDoubleQuote);
                    var jsonSerialize = new JavaScriptSerializer();
                    getArtistContracts = jsonSerialize.Deserialize<List<ArtistSearchCriteria>>(artistData);
                }
                else
                {
                    getArtistContracts = new List<ArtistSearchCriteria>();
                }
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = getArtistContracts.AsQueryable() });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// This method to search Artist information from R2 & GCS system.
        /// </summary>
        /// <param name="artistId">The artistId</param>
        /// <param name="artistName">The artistName</param>
        /// <param name="clearanceAdminCompanyName">The clearanceAdminCompanyName</param>
        /// <param name="jtStartIndex">querystring from jtable for startIndex</param>
        /// <param name="jtPageSize">querystring from jtable for pagesize</param>
        /// <param name="rowIndex">The rowIndex</param>
        /// <returns>JSON formatted Artist Contracts</returns>
        public JsonResult ManageArtist(string artistId, string artistName, string clearanceAdminCompanyName, int jtStartIndex, int jtPageSize, long rowIndex)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("artistId: {0},artistName: {1},clearanceAdminCompanyName: {2}", artistId, artistName, clearanceAdminCompanyName));
                var userInfo = new UserInfo { UserLoginName = GetCurrentLoginName() };
                var totalRowCount = 0;
                const bool isPaging = true;
                long artistIdValue = 0;
                if (!string.IsNullOrEmpty(artistId))
                    long.TryParse(artistId, out artistIdValue);

                var artistSearchCriteria = new ArtistSearchCriteria
                {
                    ArtistId = artistIdValue,
                    ArtistName = string.IsNullOrEmpty(artistName) ? string.Empty : artistName,
                    ClearanceAdminCompanyName = string.IsNullOrEmpty(clearanceAdminCompanyName) ? string.Empty : clearanceAdminCompanyName,
                    UserName = Constants.GcsUserName,
                    Criteria = { RowIndex = rowIndex, PageSize = jtPageSize, StartIndex = jtStartIndex, UserName = Constants.GcsUserName }
                };

                var manageArtistContract = _contractRepository.SearchContractbyArtist(artistSearchCriteria, isPaging, userInfo);
                if (manageArtistContract.ArtistContracts == null) manageArtistContract.ArtistContracts = new List<ArtistContract>();
                if (manageArtistContract.ArtistContracts.Count > 0)
                {
                    totalRowCount = manageArtistContract.TotalRows;
                    rowIndex = manageArtistContract.RowIndex;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = manageArtistContract.ArtistContracts.AsQueryable(), TotalRecordCount = totalRowCount, RowIndex = rowIndex });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// This method to search Resource information from R2 & GCS system.
        /// </summary>
        /// <param name="artistName">The artistName</param>
        /// <param name="title">The title</param>
        /// <param name="isrcCode">The isrcCode</param>
        /// <param name="artistId">The artistId</param>
        /// <param name="versionTitle">The versionTitle</param>
        /// <param name="resourceTypeId">The resourceTypeId</param>
        /// <param name="resourceType">The resourceType</param>
        /// <param name="clearanceAdminCompanyName">The clearanceAdminCompanyName</param>
        /// <param name="jtStartIndex">querystring from jtable for startIndex</param>
        /// <param name="jtPageSize">querystring from jtable for pagesize</param>
        /// <param name="rowIndex">The rowIndex</param>
        /// <returns>JSON formatted Resource Contracts</returns>
        public JsonResult ManageResourceContract(string artistName, string title, string isrcCode, string artistId, string versionTitle, string resourceTypeId, string resourceType, string clearanceAdminCompanyName, int jtStartIndex, int jtPageSize, long rowIndex)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("artistName: {0},title: {1},isrcCode: {2},artistId: {3},versionTitle: {4},resourceTypeId: {5},resourceType: {6},clearanceAdminCompanyName: {7}", artistName, title, isrcCode, artistId, versionTitle, resourceTypeId, resourceType, clearanceAdminCompanyName));
                var userInfo = new UserInfo { UserLoginName = GetCurrentLoginName() };
                var totalRowCount = 0;
                const bool isPaging = true;
                long artistIdValue = 0;
                if (!string.IsNullOrEmpty(artistId))
                    long.TryParse(artistId, out artistIdValue);

                var resourceSearchCriteria = new ResourceSearchCriteria
                {
                    ArtistName = artistName,
                    ResourceTitle = title,
                    Isrc = isrcCode,
                    ArtistId = artistIdValue,
                    VersionTitle = versionTitle,
                    ResourceType = resourceType,
                    ResourceTypeId = Convert.ToInt32(resourceTypeId),
                    ClearanceAdminCompanyName = clearanceAdminCompanyName,
                    UserName = userInfo.UserLoginName,
                    Criteria = { RowIndex = rowIndex, PageSize = jtPageSize, StartIndex = jtStartIndex, UserName = userInfo.UserLoginName }
                };
                var manageResourceContract = _contractRepository.SearchContractbyResource(resourceSearchCriteria, isPaging, userInfo);
                if (manageResourceContract.ResourceContracts == null) manageResourceContract.ResourceContracts = new List<ResourceContract>();
                if (manageResourceContract.ResourceContracts.Count > 0)
                {
                    totalRowCount = manageResourceContract.TotalRows;
                    rowIndex = manageResourceContract.RowIndex;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = manageResourceContract.ResourceContracts.AsQueryable(), TotalRecordCount = totalRowCount, RowIndex = rowIndex });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get Resource Contracts contains in the given contractids 
        /// </summary>
        /// <param name="contractIds">The contractIds</param>
        ///  <param name="events">The save event</param>
        /// <returns>JSON formatted content</returns>
        public JsonResult GetResourceContractByContractIdList(string deviationResourceContract, string events)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("deviationResourceContract: {0}", deviationResourceContract));
                var jsonSerialize = new JavaScriptSerializer();
                var resourceContractDetails = jsonSerialize.Deserialize<List<DeviationResourceContract>>(deviationResourceContract);
                var userInfo = new UserInfo();
                userInfo.UserLoginName = GetCurrentLoginName();
                var totalRowCount = 0;
                var resourceContracts = new List<ResourceContract>();
                if (!string.IsNullOrEmpty(deviationResourceContract))
                {
                    resourceContracts = _contractRepository.GetResourceContractByContractIdList(resourceContractDetails, userInfo) ?? new List<ResourceContract>();
                    if (events == BusinessConstants.Save)
                    {
                        resourceContracts = resourceContracts.OrderBy(resourcelist => resourcelist.ArtistName).ThenBy(resourcelist => resourcelist.ResourceTitle).ThenBy(artistlist => artistlist.ClearanceAdminCompanyName).ToList();
                    }

                    if (resourceContracts.Count > 0)
                    {
                        totalRowCount = resourceContracts.Count;
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = resourceContracts.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        /// <summary>
        /// Get contract/Resource information based on the given pageName
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="pageName">The pageName</param>
        /// <returns>Specified view and model</returns>
        [HttpPost]
        public PartialViewResult GetContractInformation(string id, string pageName)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("ContractIds: {0}", id));
                string userLoginName = GetCurrentLoginName();
                if (!(string.IsNullOrEmpty(id)))
                {
                    if (pageName == BusinessConstants.ManageResourceContract)
                    {
                        ViewBag.notLinkedInfo = ClearenceResource.resourceNotLinkedInfo;
                        _workgroupRepository.GetContractsByResource(Convert.ToInt64(id), userLoginName);
                    }
                    else if (pageName == BusinessConstants.ManageArtistContract)
                    {
                        ViewBag.notLinkedInfo = ClearenceResource.artistNotLinkedInfo;
                        _workgroupRepository.GetContractsByArtist(Convert.ToInt64(id), userLoginName);
                    }
                    else
                    {
                        ViewBag.PageName = pageName;
                        _workgroupRepository.GetLeanContract(Convert.ToInt64(id), userLoginName);
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.ContractInformation, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to get Link workgroup to artist contract view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult LinkArtistContractsToWorkgroups()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = BusinessConstants.LinkWorkGroupToArtistContract;
                PermissionCheckNdRedirect(GcsTasks.MaintainChildWorkgroup);
                LoggerFactory.LogWriter.MethodExit();
                return View(Constants.LinkWorkgroupToArtistContract, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Used to get Link workgroup to resource contract view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult LinkResourceContractsToWorkgroups()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = BusinessConstants.LinkWorkgroupToResourceContract;
                PermissionCheckNdRedirect(GcsTasks.MaintainChildWorkgroup);
                LoggerFactory.LogWriter.MethodExit();
                return View(Constants.LinkWorkgroupToResourceContract, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// used to Link ArtistContract to Workgroup based on the given workgroup and contract ids
        /// </summary>
        /// <param name="contractIds">The contractIds</param>
        /// <param name="workgroupIds">The workgroupIds</param>
        /// <param name="userInformation">The userInformation</param>
        /// <returns>JSON formatted content</returns>
        [HttpPost]
        public JsonResult LinkArtistContractToWorkgroup(string contractIds, string workgroupIds, string userInformation)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("contractIds: {0},workgroupIds: {1}", contractIds, workgroupIds));
                var userInfo = GetCurrentLoginName();
                List<long> result = _workgroupRepository.LinkArtistContractToWorkgroup(contractIds, workgroupIds, userInfo);
                LoggerFactory.LogWriter.MethodExit();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        ///  used to Link ResourceContract to Workgroup based on the given workgroup and contract ids
        /// </summary>
        /// <param name="contractIds">The contractIds</param>
        /// <param name="workgroupIds">The workgroupIds</param>
        /// <param name="userInformation">The userInformation</param>
        /// <returns>JSON formatted content</returns>
        [HttpPost]
        public JsonResult LinkResourceContractToWorkgroup(List<DeviationResourceContract> deviationResourceContract, string workgroupIds, string userInformation)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupIds: {1}", workgroupIds));
                var userInfo = GetCurrentLoginName();
                List<long> result = _workgroupRepository.LinkResourceContractToWorkgroup(deviationResourceContract, workgroupIds, userInfo);
                LoggerFactory.LogWriter.MethodExit();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        /// <summary>
        ///  used to Link ResourceContract to Workgroup based on the given workgroup and contract ids
        /// </summary>
        /// <param name="contractIds">The contractIds</param>
        /// <param name="workgroupIds">The workgroupIds</param>
        /// <param name="userInformation">The userInformation</param>
        /// <returns>JSON formatted content</returns>
        [HttpPost]
        public JsonResult LinkResourceContractToWorkgroup1(List<DeviationResourceContract> deviationResourceContract, string workgroupIds)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                UserInfo userInfo = new UserInfo();
                userInfo.UserLoginName = GetCurrentLoginName();
                bool result = false;
                LoggerFactory.LogWriter.MethodExit();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get Artist contract details for the given contractIds
        /// </summary>
        /// <param name="contractids">The contractIds</param>
        /// <param name="events">The save event</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult GetArtistByContract(string contractids, string events)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("contractids: {0}", contractids));
                string userLoginName = GetCurrentLoginName();
                var getartistList = new List<ArtistContract>();
                if (!string.IsNullOrEmpty(contractids))
                {
                    var contractIds = new List<long>(contractids.Split(new char[] { BusinessConstants.SingleOrSeperator }, StringSplitOptions.RemoveEmptyEntries).Select((long.Parse)));
                    getartistList = _contractRepository.GetArtistByContract(contractIds, userLoginName);

                    if (events == BusinessConstants.Save)
                        getartistList = getartistList.OrderBy(artistlist => artistlist.ArtistName).ThenBy(artistlist => artistlist.ClearanceAdminCompanyName).ToList();
                }
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = getartistList.AsQueryable() });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        #endregion

        #region Add and Remove Users in Workgroup

        /// <summary>
        /// Get the specified partial view
        /// </summary>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult SearchWorkgroupToAddUsers()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = BusinessConstants.Add;
                return PartialView(BusinessConstants.SearchWorkGroupToAddRemoveUsersPartial, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the specified partial view
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult SearchWorkGroupToAddRemoveUsers()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                PermissionCheckNdRedirect(GcsTasks.ManageUserWorkgroups);
                ViewBag.PageName = BusinessConstants.SearchWorkGroupToAddRemoveUsers;
                LoggerFactory.LogWriter.MethodExit();
                return View(BusinessConstants.SearchWorkGroupToAddRemoveUsers, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }



        /// <summary>
        /// Search workgroup based on the given search criteria
        /// </summary>
        /// <param name="args">Paging parameters</param>
        /// <param name="workgroupName">The workgroupName</param>
        /// <param name="workgroupRole">The workgroupRole</param>
        /// <param name="workgroupCompany">The workgroupCompany</param>
        /// <param name="workgroupUser">The workgroupUser</param>
        /// <param name="workgroupCountry">The workgroupCountry</param>
        /// <param name="isStatus">The isStatus</param>
        /// <param name="searchLoginId">The searchLoginId</param>
        /// <returns>ActionResult</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchWorkgroupToAddUsers(PagingParams args, string workgroupName, int workgroupRole, string workgroupCompany, string workgroupUser, string workgroupCountry, bool isStatus, string searchLoginId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("workgroupName: {0},workgroupRole: {1},workgroupCompany: {2},workgroupUser: {3},workgroupCountry: {4}", workgroupName, workgroupRole, workgroupCompany, workgroupUser, workgroupCountry));
                var workgroupSearchCriteria = new WorkgroupSearchCriteria
                {
                    Name = workgroupName,
                    Role = workgroupRole,
                    Company = workgroupCompany,
                    User = workgroupUser,
                    Country = workgroupCountry,
                    IsActiveOnly = isStatus,
                    StartIndex = args.StartIndex,
                    PageSize = args.PageSize,
                    UserLoginName = GetCurrentLoginName()
                };

                string userLoginName = SessionWrapper.CurrentUserInfo.UserLoginName;

                var rccAdmin = SessionWrapper.GcsCurrentPermissions;
                if (rccAdmin.Roles != null)
                {
                    workgroupSearchCriteria.IsRccAdmin = rccAdmin.Roles.Where(roles => roles.Value == BusinessConstants.RCCAdmin).GroupBy(role => role.Value).Count() != 0;
                }
                int totalRowCount = 0;
                var workgroupList = new List<WorkgroupSearchResult>();
                if (workgroupName != string.Empty || workgroupCompany != string.Empty || workgroupUser != string.Empty || workgroupCountry != string.Empty || workgroupRole != -1)
                {
                    workgroupList = _workgroupRepository.SearchWorkgroupToAddUsers(workgroupSearchCriteria, userLoginName, searchLoginId);
                }
                if (workgroupList.Count > 0)
                {
                    totalRowCount = workgroupList[0].TotalRows;

                }

                LoggerFactory.LogWriter.MethodExit();
                return workgroupList.GridJSONActions<WorkgroupSearch>(totalRowCount);

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        /// <summary>
        /// Get the specified partial view 
        /// </summary>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult SearchWorkgroupForRemoveUsers()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = BusinessConstants.Remove;
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(BusinessConstants.SearchWorkGroupToAddRemoveUsersPartial, _workgroupRepository.WorkgroupModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Search workgroup
        /// </summary>
        /// <param name="args">The paging parameters</param>
        /// <param name="loginName">The loginName</param>
        /// <returns>ActionResult</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchWorkgroupForRemoveUsers(PagingParams args, string loginName)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                int totalRowCount = 0;

                var workgroupSearchCriteria = new WorkgroupSearchCriteria
                {
                    StartIndex = args.StartIndex,
                    PageSize = args.PageSize,
                    UserLoginName = SessionWrapper.CurrentUserInfo.UserLoginName
                };

                var rccAdmin = SessionWrapper.GcsCurrentPermissions;
                if (rccAdmin.Roles != null)
                    workgroupSearchCriteria.IsRccAdmin = rccAdmin.Roles.Where(roles => roles.Value == BusinessConstants.RCCAdmin).GroupBy(role => role.Value).Count() != 0;
                
                var workgroupListForRemove = _workgroupRepository.SearchWorkgroupForRemoveUsers(workgroupSearchCriteria, loginName);

                if (workgroupListForRemove != null && workgroupListForRemove.Count > 0)
                    totalRowCount = workgroupListForRemove[0].TotalRows;

                LoggerFactory.LogWriter.MethodExit();

                return workgroupListForRemove.GridJSONActions<WorkgroupSearch>(totalRowCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Add user in multiple workgroup
        /// </summary>
        /// <param name="workgroupDetails">The workgroupDetails</param>
        /// <param name="userData">The userData</param>
        /// <returns>True if success, otherwise false</returns>
        public bool AddUserInMultipleWorkgroup(List<Workgroup> workgroupDetails, WorkGroupUser userData)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                string userLoginName = GetCurrentLoginName();
                bool isoutput = _workgroupRepository.AddUserInMultipleWorkgroup(workgroupDetails, userData, userLoginName);
                LoggerFactory.LogWriter.MethodExit();
                return isoutput;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Remove users in multiple workgroup
        /// </summary>
        /// <param name="loginName">The loginName</param>
        /// <param name="workgroupIdList">The workgroupIdList</param>
        /// <returns>JsonResult</returns>
        public JsonResult RemoveUserFromMultipleWorkgroup(string userToRemove, string workgroupIdList)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                var workgroupNameList = new List<string>();
                string userLoginName = GetCurrentLoginName();
                workgroupNameList = _workgroupRepository.RemoveUserFromMultipleWorkgroup(userToRemove, workgroupIdList, userLoginName);
                LoggerFactory.LogWriter.MethodExit();
                return Json(workgroupNameList);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion

        #region User Preferences
        /// <summary>
        /// Get ActionMethod to load the UserPreferences Screen
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPreferences()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                UserPreferenceViewModel modelObject = new UserPreferenceViewModel();
                IClearanceProjectModel clearancePACompanyAndCurrency = new ClearanceProjectModel();
                List<UserPreference> userPreference = new List<UserPreference>();
                UserPreferenceInfo userPreferenceInfo = new UserPreferenceInfo();
                List<WorkgroupBase> workGroupList = new List<WorkgroupBase>();
                var reasons = new List<SelectListItem>();

                clearancePACompanyAndCurrency = _workgroupRepository.GetClrPACompanyAndCurrencyUserList(SessionWrapper.CurrentUserInfo.UserLoginName);//SessionWrapper.CurrentUserInfo.UserLoginName);
                LoggerFactory.LogWriter.Info("Currency and Company retrieved Successfully");
                LoggerFactory.LogWriter.Debug("Currency and Company retrieved Successfully");
                userPreferenceInfo.PreferenceList = _workgroupRepository.GetEmailPreference(SessionWrapper.CurrentUserInfo.UserLoginName);//userInfo.UserLoginName);
                LoggerFactory.LogWriter.Info("Email Preference retrieved Successfully");
                LoggerFactory.LogWriter.Debug("Email Preference retrieved Successfully");
                userPreference = _workgroupRepository.GetUserPreferences(SessionWrapper.CurrentUserInfo.UserId);
                LoggerFactory.LogWriter.Info("GetUserPreferences retrieved Successfully");
                LoggerFactory.LogWriter.Debug("GetUserPreferences retrieved Successfully");
                var requestorId = (from requestor in userPreferenceInfo.PreferenceList where requestor.Description == BusinessConstants.Requestor && requestor.PreferenceType == BusinessConstants.ValueOne select requestor.PreferenceID).SingleOrDefault();
                var reviewerId = (from reviewer in userPreferenceInfo.PreferenceList where reviewer.Description == BusinessConstants.Reviewer && reviewer.PreferenceType == BusinessConstants.ValueOne select reviewer.PreferenceID).SingleOrDefault();
                var companyId = (from Company in userPreferenceInfo.PreferenceList where Company.Description == BusinessConstants.RequestingCompany && Company.PreferenceType == BusinessConstants.ValueThree select Company.PreferenceID).SingleOrDefault();
                var currencyId = (from Currency in userPreferenceInfo.PreferenceList where Currency.Description == BusinessConstants.Currency && Currency.PreferenceType == BusinessConstants.ValueFour select Currency.PreferenceID).SingleOrDefault();

                if (!SessionWrapper.GcsCurrentPermissions.IsUserPreferenceSet)
                {
                    modelObject.initialLoad = true;
                }

                foreach (var preference in userPreference)
                {
                    if (preference.PreferenceID == requestorId)
                    {
                        modelObject.InboxRole = BusinessConstants.Requestor;
                    }
                    else if (preference.PreferenceID == reviewerId)
                    {
                        modelObject.InboxRole = BusinessConstants.Reviewer;
                    }
                    else if (preference.PreferenceID == companyId)
                    {
                        if (preference.UserPreferenceValuesList.Count > 0)
                        {
                            modelObject.RequestingCompanyId = preference.UserPreferenceValuesList[0].Value;
                        }
                    }
                    else if (preference.PreferenceID == currencyId)
                    {
                        if (preference.UserPreferenceValuesList.Count > 0)
                        {
                            modelObject.CurrencyId = preference.UserPreferenceValuesList[0].Value;
                        }
                    }

                }
                userPreferenceInfo.UserPreferenceList = userPreference;
                modelObject.userPreferenceInfo = userPreferenceInfo;
                workGroupList = _workgroupRepository.GetWorkgroups(SessionWrapper.CurrentUserInfo.UserId);
                LoggerFactory.LogWriter.Info("GetWorkgroups  retrieved Successfully");
                LoggerFactory.LogWriter.Debug("GetWorkgroups  retrieved Successfully");
                for (int i = 0; i < workGroupList.Count; i++)
                {
                    reasons.Add(new SelectListItem
                    {
                        Text = workGroupList[i].Name,
                        Value = Convert.ToString(workGroupList[i].Id)
                    });
                }
                if (modelObject.userPreferenceInfo.PreferenceList.Count > 0)
                {
                    modelObject.ConsolidatedEmail = modelObject.userPreferenceInfo.PreferenceList[0].Description;
                }
                if (workGroupList.Count > 0)
                {
                    modelObject.DisplayWorkgroupList = true;
                    ViewBag.WorkgroupCount = workGroupList.Count;
                }
                modelObject.Currency = clearancePACompanyAndCurrency.CurrencyList;
                modelObject.RqstngCmpny = clearancePACompanyAndCurrency.CompanyList;
                modelObject.listItem = reasons;

                //implement check for more than one workgroup to display the list
                LoggerFactory.LogWriter.MethodExit();
                return View("UserPreference", modelObject);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Post Action method for the user preference screen
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void UserPreferences(FormCollection formCollection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<UserPreference> UserPreferenceList = new List<UserPreference>();
                List<Preferences> preferenceList = new List<Preferences>();
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                var jsonobj = serializer.Deserialize<List<UserPreference>>(formCollection["hdnJsonUserPref"]);
                
                foreach (var item in jsonobj)
                {
                    UserPreference userPreference = new UserPreference();
                    List<UserPreferenceValues> UserPreferenceValuesList = new List<UserPreferenceValues>();
                    if (item.WorkGroupValues != null)
                    {
                        string[] workGroupArray = { };
                        workGroupArray = item.WorkGroupValues.Split(BusinessConstants.CommaSeperator);
                        for (int i = 0; i < workGroupArray.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(workGroupArray[i]))
                            {
                                UserPreferenceValues UserPreferenceValues = new UserPreferenceValues();
                                UserPreferenceValues.Value = workGroupArray[i];
                                UserPreferenceValuesList.Add(UserPreferenceValues);
                            }

                        }
                    }
                    userPreference.PreferenceID = item.PreferenceID;
                    userPreference.UserPreferenceValuesList = UserPreferenceValuesList;
                    UserPreferenceList.Add(userPreference);
                }
                string loginUserName = GetCurrentLoginName();
                long userId = SessionWrapper.GcsCurrentPermissions.UserId;

                bool result = _workgroupRepository.InsertUserPrefernces(UserPreferenceList, userId, loginUserName);
                if (result == true)
                {
                    preferenceList = _workgroupRepository.GetEmailPreference(loginUserName);

                    var requestorPreferenceId = (from requestor in preferenceList where requestor.Description == BusinessConstants.Requestor && requestor.PreferenceType == BusinessConstants.ValueOne select requestor.PreferenceID).SingleOrDefault();
                    var reviewerPreferenceId = (from reviewer in preferenceList where reviewer.Description == BusinessConstants.Reviewer && reviewer.PreferenceType == BusinessConstants.ValueOne select reviewer.PreferenceID).SingleOrDefault();
                    var companyPreferenceId = (from Company in preferenceList where Company.Description == BusinessConstants.RequestingCompany && Company.PreferenceType == BusinessConstants.ValueThree select Company.PreferenceID).SingleOrDefault();
                    var currencyPreferenceId = (from Currency in preferenceList where Currency.Description == BusinessConstants.Currency && Currency.PreferenceType == BusinessConstants.ValueFour select Currency.PreferenceID).SingleOrDefault();

                    var chkrequestorPreferenceId = (from requestor in UserPreferenceList.Where(user => user.PreferenceID == requestorPreferenceId) select requestor.PreferenceID).SingleOrDefault();
                    var chkreviewerPreferenceId = (from reviewer in UserPreferenceList.Where(user => user.PreferenceID == reviewerPreferenceId) select reviewer.PreferenceID).SingleOrDefault();
                    var chkcompanyPreferenceId = (from Company in UserPreferenceList.Where(company => company.PreferenceID == companyPreferenceId) select Company).SingleOrDefault();
                    var chkcurrencyPreferenceId = (from Currency in UserPreferenceList.Where(currency => currency.PreferenceID == currencyPreferenceId) select Currency).SingleOrDefault();

                    if (chkrequestorPreferenceId != null)
                    {
                        SessionWrapper.GcsCurrentPermissions.PreferredRoleGroup = new KeyValuePair<Byte, string>((byte)BusinessConstants.ByteThree, BusinessConstants.Requestor);

                    }
                    if (chkreviewerPreferenceId != null)
                    {
                        SessionWrapper.GcsCurrentPermissions.PreferredRoleGroup = new KeyValuePair<Byte, string>((byte)BusinessConstants.ByteOne, BusinessConstants.Reviewer);

                    }
                    if (chkcompanyPreferenceId != null)
                    {
                        foreach (var userPreferenceValuesList in chkcompanyPreferenceId.UserPreferenceValuesList)
                        {
                            SessionWrapper.GcsCurrentPermissions.DefaultRequestingCompanyId = Convert.ToInt64(userPreferenceValuesList.Value);
                        }
                    }
                    if (chkcurrencyPreferenceId != null)
                    {
                        foreach (var userPreferenceValuesList in chkcurrencyPreferenceId.UserPreferenceValuesList)
                        {
                            SessionWrapper.GcsCurrentPermissions.DefaultCurrency = userPreferenceValuesList.Value;
                        }
                    }

                    SessionWrapper.GcsCurrentPermissions.IsUserPreferenceSet = true;
                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize the given objects
        /// </summary>
        /// <param name="workGroupRepository">The repository object for workgroup</param>
        /// <param name="sessionWrapper">The sessionWrapper</param>
        /// <param name="contractRepository">The contractRepository</param>
        private void InitilizeObjects(IWorkgroupRepository workGroupRepository, ISessionWrapper sessionWrapper, IContractRepository contractRepository)
        {
            try
            {
                _workgroupRepository = workGroupRepository;
                SessionWrapper = sessionWrapper;
                _contractRepository = contractRepository;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This method to get the RequestType values
        /// </summary>
        /// <param name="collections">FormCollection</param>
        /// <param name="childWorkgroupDetails">ChildWorkgroup</param>
        private static void GetRequestType(FormCollection collections, ChildWorkgroup childWorkgroupDetails)
        {
            try
            {
                var requestType = string.Empty;
                childWorkgroupDetails.RequestTypes = new List<KeyValuePair<long, long>>();

                requestType = collections["chkRegular"].ToString(CultureInfo.InvariantCulture);
                if (requestType == BusinessConstants.RegularRequest)
                {
                    childWorkgroupDetails.RequestTypes.Add(new KeyValuePair<long, long>((long)General.RequestTypeValues.Zero, (long)General.RequestTypeValues.Three));
                }
                requestType = collections["chkTVRadio"].ToString(CultureInfo.InvariantCulture);
                if (requestType == BusinessConstants.TvRadioRequest)
                {
                    childWorkgroupDetails.RequestTypes.Add(new KeyValuePair<long, long>((long)General.RequestTypeValues.Zero, (long)General.RequestTypeValues.Four));
                }
                requestType = collections["chkPriceReduction"].ToString(CultureInfo.InvariantCulture);
                if (requestType == BusinessConstants.PriceReductionRequest)
                {
                    childWorkgroupDetails.RequestTypes.Add(new KeyValuePair<long, long>((long)General.RequestTypeValues.Zero, (long)General.RequestTypeValues.Five));
                }
                requestType = collections["chkClub"].ToString(CultureInfo.InvariantCulture);
                if (requestType == BusinessConstants.ClubRequest)
                {
                    childWorkgroupDetails.RequestTypes.Add(new KeyValuePair<long, long>((long)General.RequestTypeValues.Zero, (long)General.RequestTypeValues.Six));
                }
                requestType = collections["chkTraditional"].ToString(CultureInfo.InvariantCulture);
                if (requestType == BusinessConstants.NonTraditionalRequest)
                {
                    childWorkgroupDetails.RequestTypes.Add(new KeyValuePair<long, long>((long)General.RequestTypeValues.Zero, (long)General.RequestTypeValues.Seven));
                }
                requestType = collections["chkPromotional"].ToString(CultureInfo.InvariantCulture);
                if (requestType == BusinessConstants.PromotionalRequest)
                {
                    childWorkgroupDetails.RequestTypes.Add(new KeyValuePair<long, long>((long)General.RequestTypeValues.Zero, (long)General.RequestTypeValues.Eight));
                }
                requestType = collections["chkMaster"].ToString(CultureInfo.InvariantCulture);
                if (requestType == BusinessConstants.MasterRequest)
                {
                    childWorkgroupDetails.RequestTypes.Add(new KeyValuePair<long, long>((long)General.RequestTypeValues.Zero, (long)General.RequestTypeValues.Nine));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Clear the sessions
        /// </summary>
        private void ClearWorkgroupSession()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                Session.Remove("userDetails");
                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get territory list from the specified collection
        /// </summary>
        /// <param name="collection">The FormCollection</param>
        /// <returns>TerritorialDisplay list</returns>
        private List<TerritorialDisplay> GetTerritoryDisplayList(FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                var jsonObject = collection["hdnterritoryDetailsForSave"].ToString(CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(jsonObject))
                {
                    jsonObject = jsonObject.Replace(BusinessConstants.CountryNameWithSingleQuote, BusinessConstants.CountryNameAfterRemoveSingleQuote);
                    jsonObject = jsonObject.Replace(BusinessConstants.SlashWithSingleQuote, BusinessConstants.SlashWithDoubleQuote);

                    var jsonSerialize = new JavaScriptSerializer();
                    var territoryDetails = jsonSerialize.Deserialize<List<TerritorialDisplay>>(jsonObject);
                    LoggerFactory.LogWriter.MethodExit();
                    return territoryDetails;
                }
                LoggerFactory.LogWriter.MethodExit();
                return null;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get Current Login Name
        /// </summary>
        /// <param name="collection">The FormCollection</param>
        /// <returns>LoginName String</returns>
        private string GetCurrentLoginName()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion

    }
}

