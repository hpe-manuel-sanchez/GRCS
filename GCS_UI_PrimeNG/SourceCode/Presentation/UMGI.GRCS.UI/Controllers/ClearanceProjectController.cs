using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.Enumerations;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.Resx.Resource.Layout;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.UI.Helper;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.Utilities;
using Constants = UMGI.GRCS.BusinessEntities.Constants.Constants;
using ProjectSearchResult = UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities.ProjectSearchResult;
using RoleType = UMGI.GRCS.BusinessEntities.Lookups.RoleType;

namespace UMGI.GRCS.UI.Controllers
{
    public partial class ClearanceProjectController : BaseController
    {

        private IClearanceProjectRepository _IClearanceProjectRepository;
        private IClearanceProjectModel _IClearanceProjectModel;
        private IClearanceReleaseRepository _IClearanceReleaseRepository;
        private IClearanceResourceRepository _IClearanceResourceRepository;
        private IClearenceUserTransferModel _IClearenceUserTransferModel;

        IClearanceResourceModel _clearanceResourceModel;
        List<string> _type = null;
        private List<ClearanceRelease> clearancePackageRelease = new List<ClearanceRelease>();
        private double MaxByteArrayLength;
        byte regularRoutingStatus;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractController"/> class.
        /// </summary>
        public ClearanceProjectController()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractController"/> class.
        /// </summary>
        /// <param name="contractRepository">The contract repository.</param>
        /// <param name="sessionWrapper"> </param>
        public ClearanceProjectController(IClearanceProjectRepository projectRepository, IClearanceReleaseRepository clearanceReleaseRepository, IClearanceResourceRepository clearanceResourceRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            try
            {
                _IClearanceProjectRepository = projectRepository;
                _IClearanceReleaseRepository = clearanceReleaseRepository;
                _IClearanceResourceRepository = clearanceResourceRepository;
                LoggerFactory = logFactory;
                SessionWrapper = sessionWrapper;
                _IClearanceProjectModel = new ClearanceProjectModel();
                MaxByteArrayLength = Convert.ToDouble(ConfigurationManager.AppSettings["MaxByteArrayLength"]);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
            }
        }

        #region "Create Regualr/Special Project"

        /// <summary>
        /// Create Regular Project, event will fire on clicking Create Regulare Project
        /// </summary>
        /// <returns></returns>
        // [OutputCache(Duration = 3600, VaryByParam = "None", VaryByCustom = "UserName")]
        public ActionResult CreateRegularProject()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                PermissionCheckNdRedirect(new[] { GcsTasks.ClrProjectCreation, GcsTasks.ClrProjectSubmission });

                _type = new List<string>
                    {
                        Constants.ClearanceCurrPriceLevelType,
                        Constants.ClearanceReqPriceLevelType
                    };
                ClearanceProjectModel clrProjectModel = new ClearanceProjectModel();

                var iClearanceProjectModel = _IClearanceProjectRepository.GetMasterData(_type, getUserInfo());
                _IClearanceProjectModel.CurrPriceLevelList = iClearanceProjectModel.CurrPriceLevelList;
                _IClearanceProjectModel.RequestedPriceLevelList = iClearanceProjectModel.RequestedPriceLevelList;
                iClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(getUserInfo().UserLoginName);
                _IClearanceProjectModel.CurrencyList = iClearanceProjectModel.CurrencyList;
                _IClearanceProjectModel.CompanyList = iClearanceProjectModel.CompanyList;
                _IClearanceProjectModel.RegularProjectDetails.StatusTypeDesc = General.StatusType.Unsubmitted.ToString();
                _IClearanceProjectModel.RegularProjectDetails.CreatedByUser = SessionWrapper.CurrentUserInfo.UserName;
                _IClearanceProjectModel.RegularProjectDetails.CreatedBy = SessionWrapper.CurrentUserInfo.UserName;
                _IClearanceProjectModel.RegularProjectDetails.CreatedDate = DateTime.Now.ToString(@ClearanceLayout.RegularProjectCreateDate);
                _IClearanceProjectModel.RegularProjectDetails.listUploadDocument = new List<UploadDocument>();
                _IClearanceProjectModel.RegularProjectDetails.StatusTypeDesc = General.StatusType.Unsubmitted.ToString();
                _IClearanceProjectModel.RegularProjectDetails.StatusType = (int)General.StatusType.Unsubmitted;
                _IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting = "New";
                _IClearanceProjectModel.RegularProjectDetails.IsExisting = false;
                _IClearanceProjectModel.RegularProjectDetails.Currency = string.IsNullOrEmpty(SessionWrapper.GcsCurrentPermissions.DefaultCurrency) ? ClearanceLayout.PreferredCurrency : SessionWrapper.GcsCurrentPermissions.DefaultCurrency;
                _IClearanceProjectModel.RegularProjectDetails.RequestCompanyID = (int)SessionWrapper.GcsCurrentPermissions.DefaultRequestingCompanyId;
                _IClearanceProjectModel.RegularProjectDetails.RequesterCompanyId = (int)SessionWrapper.GcsCurrentPermissions.DefaultRequestingCompanyId;

                ViewBag.RoleGroup = null;
                ViewBag.Title = "Create New Regular/Non Traditional Project";

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateRegularProject", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return View("CreateRegularProject", _IClearanceProjectModel);
            }
        }


        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult ReleaseRegularTracks(ClearanceReleaseModel clearanceReleaseModel)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceReleaseModel clrReleaseModel = new ClearanceReleaseModel();
                clrReleaseModel.clearanceRelease.RowId = clearanceReleaseModel.clearanceRelease.RowId;
                clrReleaseModel = GetR2ReleaseTracks(clearanceReleaseModel.clearanceRelease);

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("ReleaseRegularTracks", clrReleaseModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        #endregion
        
        #region "the tabbed layout and page navigation"

        [HttpPost]

        public ActionResult TabAjaxExample()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return View();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
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

        private List<ClearanceRelease> ParseStringRelease(string text)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                //split the selected Resource
                string[] arrReslist = text.Split('~');

                List<ClearanceRelease> listSelectedItems = new List<ClearanceRelease>();

                for (int i = 0; i < arrReslist.Length; i++)
                {

                    string[] listResources = arrReslist[i].Split('=');

                    //check the project user id is not null or empty
                    if (!string.IsNullOrEmpty(listResources[0]))
                    {
                        ClearanceRelease clrRelease = new ClearanceRelease();

                        clrRelease.Upc = listResources[6];
                        clrRelease.ReleaseTitle = listResources[1];
                        clrRelease.VersionTitle = listResources[2];
                        clrRelease.ArtistName = listResources[3];
                        clrRelease.Configuration = listResources[4];
                        clrRelease.DataAdminCompany = listResources[5];
                        clrRelease.R2ReleaseId = long.Parse(listResources[0]);
                        clrRelease.DataAdminCompanyId = Convert.ToInt64(listResources[7]);
                        clrRelease.CatalogueNo = listResources[8];
                        clrRelease.ComponentCount = Convert.ToInt64(listResources[9]);
                        clrRelease.ConfigurationDisplay = listResources[10];
                        clrRelease.Grid = listResources[12];
                        clrRelease.LabelId = Convert.ToInt64(listResources[13]);
                        clrRelease.labelName = listResources[14];
                        clrRelease.MusicType_Desc = listResources[15];
                        clrRelease.MusicType_Id = Convert.ToInt32(listResources[16]);
                        clrRelease.PackageIndicator = listResources[17];
                        clrRelease.PackageText = listResources[18] == "NO" ? "No" : listResources[18];
                        clrRelease.PCompanyId = Convert.ToInt64(listResources[19]);
                        clrRelease.PCompanyName = listResources[20];
                        clrRelease.R2AccountId = Convert.ToInt64(listResources[21]);
                        clrRelease.ScopeType = listResources[22];
                        clrRelease.Sequence = listResources[23];
                        clrRelease.SoundtrackIndicator = listResources[24];
                        clrRelease.Is_Ost = Convert.ToBoolean(listResources[25]);
                        clrRelease.TrackCount = listResources[26] == "" ? 0 : Convert.ToInt64(listResources[26]); // terrtioty operator applied by dhruv as it was giving error while trackcount is null
                        clrRelease.Archive_Flag = "N";
                        clrRelease.IsNewlyAddedAfterSubmit = true;

                        if (listResources.Count() <= Constants.ReleaseColumnCount)
                        { }
                        else
                        {
                            clrRelease.Package_Id = Convert.ToInt32(listResources[27]);
                            clrRelease.ReleaseId = long.Parse(listResources[28]);
                        }
                        listSelectedItems.Add(clrRelease);
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return listSelectedItems;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<ClearanceResource> ParseStringResource(string text)
        {
            //split the selected Resource
            string[] arrReslist = text.Split('~');
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<ClearanceResource> listSelectedItems = new List<ClearanceResource>();
                listSelectedItems.Add(new ClearanceResource()
                {
                    SourceUpc = arrReslist[0]
                });

                for (int i = 0; i < arrReslist.Length; i++)
                {

                    string[] listResources = arrReslist[i].Split(':');

                    //check the project user id is not null or empty
                    if (listResources[0] != "")
                    {
                        listSelectedItems.Add(new ClearanceResource()
                        {

                            ClearanceResourceId = Int32.Parse(listResources[0]),
                            ArchiveFlag = listResources[1],
                            Duration = listResources[2],
                            SuggestedFee = Decimal.Parse(listResources[3]),
                            ExcerptTime = listResources[4],
                            Comments = listResources[5],
                            SensitiveExplotation_ClearanceResource = Boolean.Parse(listResources[6]),
                            SourceUpc = listResources[7],
                            SequenceNo = listResources[8],
                            ResourceTitle = listResources[9],
                            VersionTitle = listResources[10],
                            ArtistName = listResources[11],
                            Isrc = listResources[12]
                        });
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return listSelectedItems;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }
        #endregion
        
        #region "Release Operations"

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetRegularReleases(ClearanceReleaseSearch searchList)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceReleaseSearchResult releaseExisting = new ClearanceReleaseSearchResult();
                string UpcNumber = searchList.UpcNumber;
                string ReleaseTitle = searchList.ReleaseTitle;
                int ArtistID = searchList.ArtistID;
                string ArtistName = searchList.ArtistName;

                releaseExisting = _IClearanceReleaseRepository.GetExistingReleases(UpcNumber, ReleaseTitle, ArtistName, ArtistID, getUserInfo());

                ClearanceRelease objreleasedetail = new ClearanceRelease();
                List<ClearanceRelease> objreleasedt = new List<ClearanceRelease>(releaseExisting.releaseDetail.Count);
                foreach (ClearanceRelease var in releaseExisting.releaseDetail)
                {
                    objreleasedetail = var;
                    objreleasedetail.resourceDetail = releaseExisting.resourceDetail;
                    objreleasedt.Add(objreleasedetail);
                }

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = Constants.JsonOk, Records = objreleasedt, TotalRecordCount = objreleasedt.Count });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult ReloadReleaseTab(List<ClearanceRelease> listSelectedItems)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return PartialView("ReleaseRegular", listSelectedItems);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion
        
        #region "Create New Project From Existing Project"
        public ActionResult CreateNewFromExisting()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return View();
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        #endregion
        
        #region "Search Project to Create New Project From Existing Project"

        public ActionResult SearchProject()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return View();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [HttpPost]
        public JsonResult SearchProject(ClearanceProjectSearchCriteria projectSearchCriteria)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                int totalRowCount = 0;
                ProjectSearchResult projectList = _IClearanceProjectRepository.SearchProject(projectSearchCriteria, getUserInfo());
                if (projectList != null)
                    totalRowCount = projectList.TotalRows;
                
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = "OK", Records = projectList.Values.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Error", ex.Message });
            }
        }

        #endregion

        #region "Re-Instate / Re-Open Project"

        public ActionResult SearchCancelledProject(string id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                _IClearanceProjectModel = _IClearanceProjectRepository.FillProjectSearchDropDown();

                if (id != null && id == "C")
                    _IClearanceProjectModel.ReadOnlyMode = 1;

                LoggerFactory.LogWriter.MethodExit();

                return View("SearchCancelledProject", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [HttpPost]
        public JsonResult SearchCancelledProject(ClearanceProjectSearchCriteria projectSearchCriteria)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                int totalRowCount = 0;
                if (projectSearchCriteria.ReadOnly == "Y")
                {
                    projectSearchCriteria.listStatus_Type = new List<byte>() { Constants.CancelledProject, Constants.CompletedProject };
                }

                ProjectSearchResult cancelledProjectList = _IClearanceProjectRepository.SearchProject(projectSearchCriteria, getUserInfo());

                LoggerFactory.LogWriter.MethodExit();

                if (cancelledProjectList != null)
                {
                    totalRowCount = cancelledProjectList.TotalRows;
                    return Json(new { Result = "OK", Records = cancelledProjectList.Values.AsQueryable(), TotalRecordCount = totalRowCount });
                }
                else
                    return Json(new { Result = "No Cancelled Projects Found", Records = "", TotalRecordCount = totalRowCount });
                
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Error", ex.Message });
            }
        }

        public JsonResult CheckUserStatusLocked(string projectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                int tempProjectId = Convert.ToInt32(projectId);
                string loggedInUser = _IClearanceProjectRepository.IsProjectUnlocked(tempProjectId, getUserInfo());

                LoggerFactory.LogWriter.MethodExit();

                if (string.IsNullOrEmpty(loggedInUser))
                    return Json(new { Result = "OK" });
                else
                    return Json(new { Result = loggedInUser });
               
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult OpenCancelledAndCompletedProjects(ClearanceProjectInquirySearchCriteria input)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (input.ProjectStatus == "null")
                {
                    ViewBag.DefaultTab = 2;
                    ViewBag.RCCAllocateUpc = true;
                }
                else
                {
                    ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), input.ProjectStatus, true).ToString();
                }
                string projectType = input.ProjectTypeId == 1 ? General.ProjectType.Master.ToString() : General.ProjectType.Regular.ToString();
                int projectId = input.ProjectId;

                LoggerFactory.LogWriter.MethodExit();

                return GetProjectDetails(projectType, projectId);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [OutputCache(Duration = 0)]
        public ActionResult OpenClearanceProject(ClearanceProjectInquirySearchCriteria input)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), input.ProjectStatus, true).ToString();

                string projectType = input.ProjectTypeId == 1 ? General.ProjectType.Master.ToString() : General.ProjectType.Regular.ToString();
                int projectId = input.ProjectId;
                //RCC Handler
                ViewBag.RoleGroup = input.RoleGroup;
                //End RCC handler

                if (input.RoleGroup == BusinessEntities.Lookups.RoleGroup.RCCAdmin)
                {
                    ViewBag.DefaultTab = 2;
                    ViewBag.RCCAllocateUpc = true;
                    ViewBag.ProjectStatus = null;
                }

                LoggerFactory.LogWriter.MethodExit();

                return GetProjectDetails(projectType, projectId);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult OpenClearanceProjectForEmail(ClearanceProjectInquirySearchCriteria inputProjRef)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectInquirySearchCriteria input = new ClearanceProjectInquirySearchCriteria();
                List<string> lstData = new List<string>();
                lstData = _IClearanceProjectRepository.SearchProjectForEmail(inputProjRef.ProjectReferenceId, getUserInfo());
                input.ProjectTypeId = Convert.ToByte(lstData[0]);
                input.ProjectStatus = lstData[1];
                input.ProjectId = Convert.ToInt32(lstData[2]);
                string hasUserPermissionToViewProject = lstData[3];

                if (SessionWrapper.GcsCurrentPermissions.RoleGroups.Count(g => g.Key == (byte)RoleGroup.RCCAdmin) > 0)
                {
                    input.RoleGroup = BusinessEntities.Lookups.RoleGroup.RCCAdmin;
                    string loggedInUser = _IClearanceProjectRepository.IsProjectUnlocked(input.ProjectId, getUserInfo());
                    if (loggedInUser == string.Empty)
                    {
                        return OpenClearanceProject(input);
                    }
                    else
                    {
                        input.Requestor = loggedInUser;
                        ClearanceProjectModel clearanceProjectModel = new ClearanceProjectModel();
                        clearanceProjectModel.MasterProjectDetails.ClrProjectId = input.ProjectId;
                        clearanceProjectModel.MasterProjectDetails.ProjectType = input.ProjectTypeId;
                        clearanceProjectModel.MasterProjectDetails.Status = input.ProjectStatus;
                        clearanceProjectModel.MasterProjectDetails.CreatedByUser = loggedInUser;
                        clearanceProjectModel.MasterProjectDetails.Rcc_User = input.RoleGroup.ToString();
                        return View("ShowClearanceLockingMessage", clearanceProjectModel);
                    }

                }
                else if (SessionWrapper.GcsCurrentPermissions.RoleGroups.Count(g => g.Key == (byte)RoleGroup.Requestor) > 0 && hasUserPermissionToViewProject != "N")
                {
                    input.RoleGroup = BusinessEntities.Lookups.RoleGroup.Requestor;

                    string loggedInUser = _IClearanceProjectRepository.IsProjectUnlocked(input.ProjectId, getUserInfo());
                    if (loggedInUser == string.Empty)
                    {
                        return OpenClearanceProject(input);
                    }
                    else
                    {
                        input.Requestor = loggedInUser;
                        ClearanceProjectModel clearanceProjectModel = new ClearanceProjectModel();
                        clearanceProjectModel.MasterProjectDetails.ClrProjectId = input.ProjectId;
                        clearanceProjectModel.MasterProjectDetails.ProjectType = input.ProjectTypeId;
                        clearanceProjectModel.MasterProjectDetails.Status = input.ProjectStatus;
                        clearanceProjectModel.MasterProjectDetails.CreatedByUser = loggedInUser;
                        clearanceProjectModel.MasterProjectDetails.Rcc_User = input.RoleGroup.ToString();
                        return View("ShowClearanceLockingMessage", clearanceProjectModel);
                    }
                }

                else if (SessionWrapper.GcsCurrentPermissions.RoleGroups.Count(g => g.Key == (byte)RoleGroup.Reviewer) > 0)
                {
                    input.RoleGroup = BusinessEntities.Lookups.RoleGroup.Reviewer;
                    return OpenClearanceProjectInReadOnly(input);

                }
                else if (SessionWrapper.GcsCurrentPermissions.Roles != null && SessionWrapper.GcsCurrentPermissions.Roles.Any(r => r.Key == (byte)RoleType.Inquiry))
                {
                    return OpenClearanceProjectInReadOnly(input);
                }
                else
                {
                    RedirectToUnAuthorizedPage();
                }

                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), input.ProjectStatus, true).ToString();

                string projectType = input.ProjectTypeId == 1 ? General.ProjectType.Master.ToString() :

                General.ProjectType.Regular.ToString();
                int projectId = input.ProjectId;
                //RCC Handler
                ViewBag.RoleGroup = input.RoleGroup;
                //End RCC handler
                LoggerFactory.LogWriter.MethodExit();

                return GetProjectDetails(projectType, projectId);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult OpenClearanceProjectFromInbox(ClearanceProjectInquirySearchCriteria inputProjRef)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (inputProjRef.RoleGroup == BusinessEntities.Lookups.RoleGroup.Requestor)
                {
                    string loggedInUser = _IClearanceProjectRepository.IsProjectUnlocked(inputProjRef.ProjectId, getUserInfo());
                    if (loggedInUser == string.Empty)
                    {
                        LoggerFactory.LogWriter.MethodExit();
                        return OpenClearanceProject(inputProjRef);
                    }
                    else
                    {
                        inputProjRef.Requestor = loggedInUser;
                        ClearanceProjectModel clearanceProjectModel = new ClearanceProjectModel();
                        clearanceProjectModel.MasterProjectDetails.ClrProjectId = inputProjRef.ProjectId;
                        clearanceProjectModel.MasterProjectDetails.ProjectType = inputProjRef.ProjectTypeId;
                        clearanceProjectModel.MasterProjectDetails.Status = inputProjRef.ProjectStatus;
                        clearanceProjectModel.MasterProjectDetails.CreatedByUser = loggedInUser;
                        clearanceProjectModel.MasterProjectDetails.Rcc_User = inputProjRef.RoleGroup.ToString();
                        LoggerFactory.LogWriter.MethodExit();
                        return View("ShowClearanceLockingMessage", clearanceProjectModel);
                    }
                }

                else if (inputProjRef.RoleGroup == BusinessEntities.Lookups.RoleGroup.RCCAdmin)
                {
                    inputProjRef.RoleGroup = BusinessEntities.Lookups.RoleGroup.RCCAdmin;
                    string loggedInUser = _IClearanceProjectRepository.IsProjectUnlocked(inputProjRef.ProjectId, getUserInfo());
                    if (loggedInUser == string.Empty)
                    {
                        LoggerFactory.LogWriter.MethodExit();
                        return OpenClearanceProject(inputProjRef);
                    }
                    else
                    {
                        inputProjRef.Requestor = loggedInUser;
                        ClearanceProjectModel clearanceProjectModel = new ClearanceProjectModel();
                        clearanceProjectModel.MasterProjectDetails.ClrProjectId = inputProjRef.ProjectId;
                        clearanceProjectModel.MasterProjectDetails.ProjectType = inputProjRef.ProjectTypeId;
                        clearanceProjectModel.MasterProjectDetails.Status = inputProjRef.ProjectStatus;
                        clearanceProjectModel.MasterProjectDetails.CreatedByUser = loggedInUser;
                        clearanceProjectModel.MasterProjectDetails.Rcc_User = inputProjRef.RoleGroup.ToString();
                        LoggerFactory.LogWriter.MethodExit();
                        return View("ShowClearanceLockingMessage", clearanceProjectModel);
                    }

                }
                else if (inputProjRef.RoleGroup == BusinessEntities.Lookups.RoleGroup.Reviewer)
                {
                    LoggerFactory.LogWriter.MethodExit();
                    inputProjRef.RoleGroup = BusinessEntities.Lookups.RoleGroup.Reviewer;
                    return OpenClearanceProjectInReadOnly(inputProjRef);
                }
                else
                {
                    LoggerFactory.LogWriter.MethodExit();
                    return Json(new { Result = "OK" });
                }
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult OpenRegularProjectForSubmit(string projectType, int projectId, string Statustype, string ActiveRoleGroup = null)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), Statustype, true).ToString();
                ViewBag.LoadTemplate = "1";
                ViewBag.RoleGroup = BusinessEntities.Lookups.RoleGroup.Requestor;
                //End RCC handler
                ClearanceProjectModel model = new ClearanceProjectModel();
                model = (ClearanceProjectModel)TempData["EntityForSubmtProject" + "_" + projectId];

                if (ActiveRoleGroup != null)
                    model.isInMaintainMode = 1;
                
                if (model.RegularProjectDetails.ReleaseNewOrExisting.Equals("New"))
                {
                    // Enable Disable Push To R2 button
                    // [UC013-0045]The clearance system validates if the UPC has been allocated to at-least one new release on the project
                    // if release is there without push, then check if upc null is there, that is allowed only if project is third party
                    ClearanceProjectModel clearancemodelPushToR2 = new ClearanceProjectModel();
                    clearancemodelPushToR2.RegularProjectDetails = model.RegularProjectDetails;
                    if (clearancemodelPushToR2.RegularProjectDetails.ObjRelease != null)
                        EnableDisblePushToR2Btn(clearancemodelPushToR2);

                    LoggerFactory.LogWriter.Debug("Successfully Enable Disable Push To R2");

                }

                if (model.RegularProjectDetails.ThirdPartyCompanyID == 0)
                    model.RegularProjectDetails.ThirdPartyCompanyID = -1;
                
                if (model.RegularProjectDetails.ScopeAndRequestType.RequestedPriceLevel_ID == 0)
                    model.RegularProjectDetails.ScopeAndRequestType.RequestedPriceLevel_ID = 1;
                
                Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = model.RegularProjectDetails.listUploadDocument;

                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    foreach (var resource in model.RegularProjectDetails.ClearanceResource)
                    {
                        // for all recording Types
                        if (resource.LiveStudioType == ((int)General.LiveStudioType.Live).ToString())
                            resource.RecordingTypeDesc = (General.LiveStudioType.Live).ToString();
                        else if (resource.LiveStudioType == ((int)General.LiveStudioType.Studio).ToString())
                            resource.RecordingTypeDesc = General.LiveStudioType.Studio.ToString();

                        // for all resource type
                        if (resource.ResourceType == ((int)General.ResourceType.Audio).ToString())
                            resource.ResourceTypeDesc = General.ResourceType.Audio.ToString();
                        else if (resource.ResourceType == ((int)General.ResourceType.Image).ToString())
                            resource.ResourceTypeDesc = (General.ResourceType.Image).ToString();
                        else if (resource.ResourceType == ((int)General.ResourceType.Merchandise).ToString())
                            resource.ResourceTypeDesc = General.ResourceType.Merchandise.ToString();
                        else if (resource.ResourceType == ((int)General.ResourceType.Other).ToString())
                            resource.ResourceTypeDesc = General.ResourceType.Other.ToString();
                        else if (resource.ResourceType == ((int)General.ResourceType.Text).ToString())
                            resource.ResourceTypeDesc = General.ResourceType.Text.ToString();

                        // for all music type
                        if (resource.MusicClassType == ((int)General.MusicClassType.Classical).ToString())
                            resource.MusicTypeDesc = General.MusicClassType.Classical.ToString();
                        else if (resource.MusicClassType == ((int)General.MusicClassType.Jazz).ToString())
                            resource.MusicTypeDesc = General.MusicClassType.Jazz.ToString();
                        else if (resource.MusicClassType == ((int)General.MusicClassType.Pop).ToString())
                            resource.MusicTypeDesc = General.MusicClassType.Pop.ToString();
                        else if (resource.MusicClassType == ((int)General.MusicClassType.Other).ToString())
                            resource.MusicTypeDesc = General.MusicClassType.Other.ToString();

                        if (resource.SuggestedFee != null)
                        {
                            decimal suggestfee;
                            suggestfee = resource.SuggestedFee.GetValueOrDefault();
                            resource.SuggestedFee = Convert.ToDecimal(suggestfee.ToString("F02", CultureInfo.InvariantCulture));
                        }
                    }
                }

                var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                Territories.Add(1, model.RegularProjectDetails.Territories);
                Territories.Add(2, model.RegularProjectDetails.ScopeAndRequestType.Territories);
                int tempI = 2;
                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    foreach (ClearanceResource cls in model.RegularProjectDetails.ClearanceResource)
                    {
                        Territories.Add(++tempI, cls.TerritorialRights);
                    }
                }

                ViewBag.ProjectTerritories = Territories;
                
                List<string> list = RegularListTooltipControl(model.RegularProjectDetails.ReleaseNewOrExisting);
                ViewBag.ControlsRegularList = list;

                Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId] = model;
                
                model.RccHandler = BindRCCHandlerDropdown(RoleType.RCCAdmin);
                if (model.RegularProjectDetails.ReleaseNewOrExisting == "Exist")
                    ViewBag.DefaultTab = 3;
                else
                    ViewBag.DefaultTab = 4;

                if (TempData["DefaultTabCheck"] != null)
                    ViewBag.DefaultTab = TempData["DefaultTabCheck"];

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateRegularProject", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult GetProjectDetails(string projectType, int projectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (projectType == "Master")
                {
                    #region "Show Master Project Details"
                    LoggerFactory.LogWriter.Info("Get Master Data Method initiated");
                    ClearanceProjectModel clearanceModel = new ClearanceProjectModel();
                    clearanceModel.isInMaintainMode = 1;
                    _IClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(SessionWrapper.CurrentUserInfo.UserLoginName);

                    //To set Dropdown values of freehand Resource
                    _type = new List<string>();
                    _type.Add(Constants.ClearanceMusicType);//pass type as constants
                    _type.Add(Constants.ClearanceResourceType);
                    _type.Add(Constants.ClearanceRecordingType);
                    _clearanceResourceModel = _IClearanceProjectRepository.GetMasterDataResource(_type, getUserInfo());
                    _type.Clear();
                    clearanceModel.RecordingTypeResourceTab = _clearanceResourceModel.RecordingType;
                    _clearanceResourceModel.ResourceType = _clearanceResourceModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
                    clearanceModel.ResourceTypeResourceTab = _clearanceResourceModel.ResourceType;
                    var MusicType = _clearanceResourceModel.MusicType.Where(x => x.Text.ToUpper() != "JAZZ").ToList();
                    _clearanceResourceModel.MusicType = MusicType;
                    clearanceModel.MusicTypeResourceTab = _clearanceResourceModel.MusicType;
                    //End-To set Dropdown values of freehand Resource
                    LoggerFactory.LogWriter.Debug("Get Master Project Detail Method Initiated " + string.Format("Project Id: {0}", projectId));

                    clearanceModel.MasterProjectDetails = _IClearanceProjectRepository.GetMasterProjectDetails(projectId, getUserInfo());

                    Session["fileList" + "_" + clearanceModel.MasterProjectDetails.ClearanceProjectID] = clearanceModel.MasterProjectDetails.listUploadDocument;
                    
                    foreach (var resource in clearanceModel.MasterProjectDetails.ClearanceResource)
                    {
                        // for all recording Types
                        if (resource.RecordingTypeDesc != null)
                        {
                            if (resource.RecordingTypeDesc.ToUpper() == General.LiveStudioType.Live.ToString().ToUpper())
                                resource.LiveStudioType = ((int)General.LiveStudioType.Live).ToString();
                            else if (resource.RecordingTypeDesc.ToUpper() == General.LiveStudioType.Studio.ToString().ToUpper())
                                resource.LiveStudioType = ((int)General.LiveStudioType.Studio).ToString();
                            else
                            {
                                resource.RecordingTypeDesc = General.LiveStudioType.Live.ToString();
                                resource.LiveStudioType = ((int)General.LiveStudioType.Live).ToString();
                            }
                        }
                        // for all resource type
                        if (resource.ResourceTypeDesc != null)
                        {
                            if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Audio.ToString().ToUpper())
                                resource.ResourceType = ((int)General.ResourceType.Audio).ToString();
                            else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Image.ToString().ToUpper())
                                resource.ResourceType = ((int)General.ResourceType.Image).ToString();
                            else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Merchandise.ToString().ToUpper())
                                resource.ResourceType = ((int)General.ResourceType.Merchandise).ToString();
                            else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Other.ToString().ToUpper())
                                resource.ResourceType = ((int)General.ResourceType.Other).ToString();
                            else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Text.ToString().ToUpper())
                                resource.ResourceType = ((int)General.ResourceType.Text).ToString();
                        }
                        // for all music type
                        if (resource.MusicTypeDesc != null)
                        {
                            if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Classical.ToString().ToUpper())
                                resource.MusicClassType = ((int)General.MusicClassType.Classical).ToString();
                            else if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Jazz.ToString().ToUpper())
                                resource.MusicClassType = ((int)General.MusicClassType.Jazz).ToString();
                            else if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Pop.ToString().ToUpper())
                                resource.MusicClassType = ((int)General.MusicClassType.Pop).ToString();
                            else if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Other.ToString().ToUpper())
                                resource.MusicClassType = ((int)General.MusicClassType.Other).ToString();
                        }
                        if (resource.SuggestedFee != null)
                        {
                            decimal suggestfee;
                            suggestfee = resource.SuggestedFee.GetValueOrDefault();
                            resource.SuggestedFee = Convert.ToDecimal(suggestfee.ToString("F02", CultureInfo.InvariantCulture));
                        }
                    }

                    clearanceModel.CurrencyList = _IClearanceProjectModel.CurrencyList;
                    clearanceModel.CompanyList = _IClearanceProjectModel.CompanyList;

                    foreach (var company in clearanceModel.CompanyList)
                    {
                        if (company.Value == clearanceModel.MasterProjectDetails.RequesterCompanyId.ToString())
                            company.Selected = true;
                    }

                    var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                    Territories.Add(1, clearanceModel.MasterProjectDetails.Territories);
                    int i = 2;

                    foreach (ClearanceResource cls in clearanceModel.MasterProjectDetails.ClearanceResource)
                    {
                        Territories.Add(++i, cls.TerritorialRights);
                    }

                    clearanceModel.RequestTypeManufacturedBy = _IClearanceProjectModel.RequestTypeManufacturedBy;

                    ViewBag.ProjectTerritories = Territories;
                    ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), clearanceModel.MasterProjectDetails.StatusType.ToString(), true).ToString();
                    LoggerFactory.LogWriter.Info("Successfully Set Territories Detail on ViewBag");
                    List<string> list = MasterListTooltipControl();
                    ViewBag.ControlsList = list;
                    Session["OldEntityMaster" + "_" + clearanceModel.MasterProjectDetails.ClearanceProjectID] = clearanceModel;
                    
                    #endregion
                    clearanceModel.RccHandler = BindRCCHandlerDropdown(RoleType.RCCAdmin);
                    
                    if (ViewBag.RoleGroup != null)
                        clearanceModel.roleGroupName = ViewBag.RoleGroup;
                    
                    if (clearanceModel.MasterProjectDetails.RequestInfoList != null)
                    {
                        var serializer = new JavaScriptSerializer();
                        clearanceModel.MasterProjectDetails.RequestInfoList.All(irow =>
                        {
                            irow.ModifiedDateRequestString = serializer.Serialize(irow.ModifiedDateRequest);
                            irow.ModifiedDateRoutedString = serializer.Serialize(irow.ModifiedDate);
                            return true;
                        });
                    }

                    LoggerFactory.LogWriter.MethodExit();

                    return View("CreateMasterProject", clearanceModel);
                }
                else if (projectType == "Regular")
                {
                    LoggerFactory.LogWriter.Info("Get Regular Data Method initiated");
                    _type = new List<string>
                    {
                        Constants.ClearanceCurrPriceLevelType,
                        Constants.ClearanceReqPriceLevelType
                    };
                    ClearanceProjectModel clrProjectModel = new ClearanceProjectModel();
                    var iClearanceProjectModel = _IClearanceProjectRepository.GetMasterData(_type, getUserInfo());
                    _IClearanceProjectModel.CurrPriceLevelList = iClearanceProjectModel.CurrPriceLevelList;
                    _IClearanceProjectModel.RequestedPriceLevelList = iClearanceProjectModel.RequestedPriceLevelList;
                    iClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(getUserInfo().UserLoginName);
                    _IClearanceProjectModel.CurrencyList = iClearanceProjectModel.CurrencyList;
                    _IClearanceProjectModel.CompanyList = iClearanceProjectModel.CompanyList;

                    _IClearanceProjectModel.RegularProjectDetails = _IClearanceProjectRepository.GetRegularProjectInfo_RequestType(projectId, getUserInfo());

                    if (_IClearanceProjectModel.RegularProjectDetails.ThirdPartyCompanyID == 0)
                        _IClearanceProjectModel.RegularProjectDetails.ThirdPartyCompanyID = -1;

                    if (_IClearanceProjectModel.RegularProjectDetails.ScopeAndRequestType.RequestedPriceLevel_ID == 0)
                        _IClearanceProjectModel.RegularProjectDetails.ScopeAndRequestType.RequestedPriceLevel_ID = 1;

                    if (string.IsNullOrEmpty(_IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting))
                        _IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting = "New";

                    Session["fileList" + "_" + _IClearanceProjectModel.RegularProjectDetails.ClrProjectId] = _IClearanceProjectModel.RegularProjectDetails.listUploadDocument;

                    _IClearanceProjectModel.isInMaintainMode = 1;

                    if (ViewBag.DefaultTab == 2)// For RCC Admin
                        _IClearanceProjectModel.ReadOnlyMode = 1;

                    var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                    Territories.Add(1, _IClearanceProjectModel.RegularProjectDetails.Territories);
                    Territories.Add(2, _IClearanceProjectModel.RegularProjectDetails.ScopeAndRequestType.Territories);
                    ViewBag.ProjectTerritories = Territories;

                    List<string> list = RegularListTooltipControl(_IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting);
                    ViewBag.ControlsRegularList = list;

                    Session["OldEntity" + "_" + _IClearanceProjectModel.RegularProjectDetails.ClrProjectId] = _IClearanceProjectModel;
                    
                    _IClearanceProjectModel.IsProjectBlocked = false;
                    _IClearanceProjectModel.RccHandler = BindRCCHandlerDropdown(RoleType.RCCAdmin);
                    ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), _IClearanceProjectModel.RegularProjectDetails.StatusType.ToString(), true).ToString();

                    if (ViewBag.RoleGroup != null)
                        _IClearanceProjectModel.roleGroupName = ViewBag.RoleGroup;

                    if (_IClearanceProjectModel.RegularProjectDetails.RequestInfoList != null)
                    {
                        var serializer = new JavaScriptSerializer();
                        _IClearanceProjectModel.RegularProjectDetails.RequestInfoList.All(i =>
                        {
                            i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                            i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                            return true;
                        });
                    }

                    LoggerFactory.LogWriter.MethodExit();

                    return View("CreateRegularProject", _IClearanceProjectModel);
                }
                else
                {
                    #region "Go Back To SearchPage"
                    try
                    {
                        _IClearanceProjectModel = _IClearanceProjectRepository.FillProjectSearchDropDown();
                        LoggerFactory.LogWriter.MethodExit();
                        return View("SearchCancelledProject", _IClearanceProjectModel);
                    }
                    catch (Exception ex)
                    {
                        LoggerFactory.LogWriter.Error(Category.UI, ex);
                        throw;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        // Remove from DB when Remove is Clicked
        [HttpPost]
        public string RegularProjectRemoveRelease(ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                bool flag;

                if (model != null)
                {
                    foreach (var release in model.RegularProjectDetails.ObjRelease)
                    {
                        if ((release.Archive_Flag == "Y" && release.ReleaseId != 0 && model.RegularProjectDetails.ClrProjectId != 0))
                        {
                            release.Clr_Project_Id = model.RegularProjectDetails.ClrProjectId;
                            flag = _IClearanceProjectRepository.RemoveReleaseProject(release, getUserInfo());
                            break;
                        }
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

            return string.Empty;
        }

        // Remove from DB when Remove is Clicked
        [HttpPost]
        public string RegularProjectRemoveNewRelease(ClearanceProjectModel model, string releaseId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                bool flag;

                if (model.RegularProjectDetails.ObjRelease != null && (!releaseId.Equals(string.Empty)))
                {
                    var release = (from rel in model.RegularProjectDetails.ObjRelease where rel.ReleaseId != 0 && rel.ReleaseId.ToString() == releaseId select rel).SingleOrDefault();
                    release.Clr_Project_Id = model.RegularProjectDetails.ClrProjectId;
                    flag = _IClearanceProjectRepository.RemoveReleaseProject(release, getUserInfo());
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

        [HttpPost]
        public JsonResult ReInstateProject(string clearanceProjectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        #region "ReleaseNew"

        /// <summary>
        /// Get list of territories and countries.
        /// </summary>
        /// <returns>Partial view</returns>
        public PartialViewResult ReleaseNew()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                string viewName = "ReleaseNewRegular";
                return PartialView(viewName);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion "ReleaseExisting"

        #region "ReleaseExisting"

        /// <summary>
        /// Get list of territories and countries.
        /// </summary>
        /// <returns>Partial view</returns>
        public PartialViewResult ReleaseExisting()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                string viewName = "ReleaseRegular";

                return PartialView(viewName);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion "ReleaseExisting"

        #region "CopyProject"

        public ActionResult CopyProject()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return View();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        #endregion "CopyProject"

        #region "CreateMasterProject"

        public ActionResult CreateMasterProject()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                PermissionCheckNdRedirect(new[] { GcsTasks.ClrProjectCreation, GcsTasks.ClrProjectSubmission });

                Advertising advertisingDetails = new Advertising();
                Film filmDetails = new Film();
                Trailer trailerDetails = new Trailer();
                Others otherDetails = new Others();
                List<ClearanceResource> clearanceResourceList = new List<ClearanceResource>();
                ClearanceResource clearanceResource = new ClearanceResource();
                ClearanceResource clearanceResource1 = new ClearanceResource();
                ArtistSearch artistSearch = new ArtistSearch();

                _IClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(SessionWrapper.CurrentUserInfo.UserLoginName);
                LoggerFactory.LogWriter.Debug("Successfully Get Master Data (Currency and Requesting Company)");
                _IClearanceProjectModel.MasterProjectDetails.ClearanceResource = clearanceResourceList;
                _IClearanceProjectModel.MasterProjectDetails.listUploadDocument = new List<UploadDocument>();

                ViewBag.List = clearanceResourceList;
                _IClearanceProjectModel.MasterProjectDetails.Advertising = advertisingDetails;
                _IClearanceProjectModel.MasterProjectDetails.Film = filmDetails;
                _IClearanceProjectModel.MasterProjectDetails.Trailer = trailerDetails;
                _IClearanceProjectModel.MasterProjectDetails.Others = otherDetails;
                _IClearanceProjectModel.MasterProjectDetails.CreatedDate = DateTime.Now.ToString(@ClearanceLayout.RegularProjectCreateDate);
                _IClearanceProjectModel.MasterProjectDetails.CreatedByUser = SessionWrapper.CurrentUserInfo.UserName;
                _IClearanceProjectModel.MasterProjectDetails.CreatedBy = SessionWrapper.CurrentUserInfo.UserName;
                _IClearanceProjectModel.MasterProjectDetails.StatusTypeDesc = General.StatusType.Unsubmitted.ToString();
                _IClearanceProjectModel.MasterProjectDetails.StatusType = (int)General.StatusType.Unsubmitted;
                _IClearanceProjectModel.MasterProjectDetails.Currency = string.IsNullOrEmpty(SessionWrapper.GcsCurrentPermissions.DefaultCurrency) ? ClearanceLayout.PreferredCurrency : SessionWrapper.GcsCurrentPermissions.DefaultCurrency;
                _IClearanceProjectModel.MasterProjectDetails.RequestCompanyID = (int)SessionWrapper.GcsCurrentPermissions.DefaultRequestingCompanyId;
                _IClearanceProjectModel.MasterProjectDetails.RequesterCompanyId = (int)SessionWrapper.GcsCurrentPermissions.DefaultRequestingCompanyId;

                ViewBag.DefaultTab = "0";
                ViewBag.RoleGroup = null;

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateMasterProject", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        #endregion "CreateMasterProject"

        private T Deserialize<T>(string json)
        {
            LoggerFactory.LogWriter.MethodStart();

            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();

            LoggerFactory.LogWriter.MethodExit();

            return obj;
        }

        private List<TerritorialDisplay> ConvertInToManageTerritoryObject(string manageTerriory)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (manageTerriory != string.Empty)
            {
                manageTerriory = manageTerriory.Replace('\'', '\"');
                JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
                List<TerritorialDisplay> territoryDetails = jsonSerialize.Deserialize<List<TerritorialDisplay>>(manageTerriory);
                LoggerFactory.LogWriter.MethodExit();
                return territoryDetails;
            }

            LoggerFactory.LogWriter.MethodExit();

            return null;
        }

        private List<ClearanceResource> ParseString(string text)
        {
            LoggerFactory.LogWriter.MethodStart();

            //split the selected Resource
            string[] strResourcelist = text.Split('~');

            List<ClearanceResource> listSelectedItems = new List<ClearanceResource>();
            ClearanceResource resourceDetail = new ClearanceResource();

            for (int i = 0; i < strResourcelist.Length; i++)
            {
                string[] listResources = strResourcelist[i].Split('|');

                //check the project user id is not null or empty
                if (listResources.Length > 1)
                {
                    ArtistSearch artistSearch = new ArtistSearch();
                    artistSearch.FirstName = listResources[3].Trim();
                    ClearanceResource objClearanceResource = new ClearanceResource();
                    // ~ & | symbols are being used in this method to split multiple resources & properties, so resource title should not contain these two symbols.
                    objClearanceResource.Title = listResources[1].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.VersionTitle = listResources[2].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.ArtistName = listResources[3].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.Duration = listResources[5].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.MusicTime = listResources[5].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.PYear = Convert.ToInt32(listResources[6].Trim());
                    objClearanceResource.RightsTypeCode = listResources[7].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.OwnedProjectId = Convert.ToInt64(listResources[8].Trim());
                    objClearanceResource.GenreId = listResources[9].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.PCompanyId = Convert.ToInt64(listResources[10].Trim());
                    objClearanceResource.PLicensingExtension = listResources[11].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.SampleCredit = listResources[12].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.R2AccountId = Convert.ToInt64(listResources[13].Trim());
                    objClearanceResource.R2_ResourceId = Convert.ToInt64(listResources[4].Trim());
                    objClearanceResource.MusicTypeDesc = listResources[15].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.RecordingTypeDesc = listResources[16].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.ResourceTypeDesc = listResources[17].Trim().Replace("(tilde)", "~").Replace("(pipe)", "|");
                    objClearanceResource.AdminCompanyId = Convert.ToInt64(listResources[18].Trim());
                    objClearanceResource.IsMobileResource = Convert.ToBoolean(listResources[19].Trim());
                    objClearanceResource.HasSample = Convert.ToBoolean(listResources[20].Trim());
                    objClearanceResource.HasSideArtist = Convert.ToBoolean(listResources[21].Trim());

                    if (listResources.Length.Equals(Constants.ResourceColumnCount)) 
                    {
                        objClearanceResource.Isrc = listResources[0].Trim();
                        objClearanceResource.ResourceId = 0;
                        objClearanceResource.ResourceIndexToUpdate = long.Parse(listResources[22].Trim());
                        objClearanceResource.ArchiveFlag = "N";
                    }
                    else
                    {
                        decimal? SuggestedFee = null;

                        if (listResources[22].Trim() != string.Empty)
                            SuggestedFee = Convert.ToDecimal(listResources[22].Trim());

                        objClearanceResource.Isrc = string.Empty;
                        objClearanceResource.ResourceId = 0;
                        objClearanceResource.SuggestedFee = SuggestedFee;
                        objClearanceResource.ExcerptTime = listResources[23].Trim();
                        objClearanceResource.Comments = listResources[24].Trim();
                        objClearanceResource.ArtistInfo = ParseStringArtist(listResources[25].Trim());
                        objClearanceResource.ResourceIndexToUpdate = long.Parse(listResources[26].Trim());
                        objClearanceResource.ArchiveFlag = "N";
                        objClearanceResource.TerritorialRights = ConvertInToManageTerritoryObject(listResources[27]);
                        objClearanceResource.IncludedTerritories = listResources[28];
                        objClearanceResource.ExcludedTerritories = listResources[29];
                    }

                    listSelectedItems.Add(objClearanceResource);
                }
            }

            LoggerFactory.LogWriter.MethodExit();

            return listSelectedItems;
        }

        #region Upload Button Event

        public ActionResult ReadFile(string id, ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                List<UploadDocument> listUploadDocument = new List<UploadDocument>();
                UploadDocument uploadDocumentOne = null;
                string clrProjectId = string.Empty;

                string[] rowData = id.Split('~');
                clrProjectId = rowData[2];
                int documentId = rowData[1].Length > 0 ? int.Parse(rowData[1]) : 0;
                if (documentId > 0)
                {
                    uploadDocumentOne = _IClearanceProjectRepository.ReadFile(documentId, getUserInfo());
                }
                else
                {
                    if (Session["fileList" + "_" + clrProjectId] != null)
                    {
                        listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + clrProjectId];
                        foreach (UploadDocument uploadDocument in listUploadDocument)
                        {
                            if (rowData[0] == uploadDocument.Name)
                            {
                                uploadDocumentOne = uploadDocument;
                                break;
                            }
                        }
                    }
                }
                if (uploadDocumentOne != null)
                {
                    WriteFile(System.Web.HttpContext.Current, uploadDocumentOne.Data, uploadDocumentOne.Name);
                }

                model.MasterProjectDetails.listUploadDocument = new List<UploadDocument>();
                model.MasterProjectDetails.listUploadDocument = PopulateUploadedDocs(listUploadDocument);
                model = SetSelectedDropDown(model);

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateMasterProject", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult ReadRegularFile(string id, ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<UploadDocument> listUploadDocument = new List<UploadDocument>();
                UploadDocument uploadDocumentOne = null;
                string clrProjectId = string.Empty;

                string[] rowData = id.Split('~');
                clrProjectId = rowData[2];
                int documentId = rowData[1].Length > 0 ? int.Parse(rowData[1]) : 0;
                if (documentId > 0)
                {
                    uploadDocumentOne = _IClearanceProjectRepository.ReadFile(documentId, getUserInfo());
                }
                else
                {
                    if (Session["fileList" + "_" + clrProjectId] != null)
                    {
                        listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + clrProjectId];
                        foreach (UploadDocument uploadDocument in listUploadDocument)
                        {
                            if (rowData[0] == uploadDocument.Name)
                            {
                                uploadDocumentOne = uploadDocument;
                            }
                        }
                    }
                }
                if (uploadDocumentOne != null)
                {
                    WriteFile(System.Web.HttpContext.Current, uploadDocumentOne.Data, uploadDocumentOne.Name);
                }

                model.RegularProjectDetails.listUploadDocument = new List<UploadDocument>();
                foreach (UploadDocument uploadDocument in listUploadDocument)
                {
                    model.RegularProjectDetails.listUploadDocument.Add(new UploadDocument
                    {
                        Id = uploadDocument.Id,
                        Name = uploadDocument.Name,
                        Type = uploadDocument.Type
                    });
                }

                CacheData(model, string.Empty);

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateRegularProject", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public string RemoveRegularFile(string fileName, string clrProjectId, IList<ClearanceResource> list)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                SetModelProperties(clrProjectId, false, fileName);
                LoggerFactory.LogWriter.MethodExit();
                return string.Empty;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public void WriteFile(HttpContext context, Byte[] content, String fileName)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                context.Response.Clear();
                context.Response.Cache.SetExpires(DateTime.MinValue);

                if (content != null && content.Length > 0)
                {
                    string contentType = "application/vnd.ms-excel";
                    string accept = string.Format("{0}{1}", "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-powerpoint, application/msword, */*, ", contentType);

                    context.Response.Buffer = true;
                    context.Response.AddHeader("accept-Encoding", "gzip, deflate");
                    context.Response.AddHeader("Content-Disposition", string.Format("{0}{1}", "attachment;filename=", fileName));
                    context.Response.AddHeader("accept-Language", "en-us");
                    context.Response.AddHeader("accept", accept);
                    context.Response.Charset = "ISO-8859-1";
                    context.Response.BinaryWrite(content);
                }

                LoggerFactory.LogWriter.MethodExit();

                context.Response.End();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public string RemoveFile(string fileName, string clrProjectId, IList<ClearanceResource> list)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                SetModelProperties(clrProjectId, true, fileName);

                LoggerFactory.LogWriter.MethodExit();

                return string.Empty;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion

        private List<ArtistInfo> ParseStringArtist(string text)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string[] arrReslist = text.Split('=');
                List<ArtistInfo> listSelectedItems = new List<ArtistInfo>();

                for (int i = 0; i < arrReslist.Length; i++)
                {

                    string[] listResources = arrReslist[i].Split(':');
                    //check the artist id is not null or empty
                    if (!string.IsNullOrEmpty(listResources[0]))
                    {
                        listSelectedItems.Add(new ArtistInfo()
                        {
                            Id = Convert.ToInt64(listResources[2]),
                            Name = listResources[0],
                            NameId = Convert.ToInt64(listResources[1])

                        });
                    }
                }
                LoggerFactory.LogWriter.MethodExit();

                return listSelectedItems;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // getting next status based on current status
        private int GetNextProjectStatus(string command, int status, string statusdesc, bool isUpdateNextstatus, out string statusDesc)
        {
            LoggerFactory.LogWriter.MethodStart();

            string statusDescpt = string.Empty;
            //Set the next status while Submit,ReOpening and Resubmit
            if ((statusdesc == General.StatusType.Submitted.ToString()) || (statusdesc == General.StatusType.ReSubmitted.ToString()) || (statusdesc == General.StatusType.ReOpened.ToString()))
            {
                if (command == null)
                    command = "Save";
            }

            if (command == ClearanceLayout.MasterProjectInfoSaveButton)
            {
                if (status == (int)(General.StatusType.Submitted))
                {
                    if (isUpdateNextstatus == true)
                    {
                        status = (int)(General.StatusType.ReSubmitted);
                        statusDescpt = General.StatusType.ReSubmitted.ToString();
                    }
                    else
                    {
                        status = (int)(General.StatusType.Submitted);
                        statusDescpt = General.StatusType.Submitted.ToString();
                    }
                    statusDesc = statusDescpt;
                    return status;
                }
                else if (status == (int)(General.StatusType.Unsubmitted)) // save & submit
                {
                    status = (int)(General.StatusType.Unsubmitted);
                    statusDescpt = General.StatusType.Unsubmitted.ToString();
                    statusDesc = statusDescpt;
                    return status;
                }
                else if (status == (int)(General.StatusType.ReOpened))
                {
                    if (isUpdateNextstatus == true)
                    {
                        status = (int)(General.StatusType.ReSubmitted);
                        statusDescpt = General.StatusType.ReSubmitted.ToString();
                    }
                    else
                    {
                        status = (int)(General.StatusType.ReOpened);
                        statusDescpt = General.StatusType.ReOpened.ToString();
                    }
                    statusDesc = statusDescpt;
                    return status;
                }
            }
            else if (command == ClearanceLayout.CancelButton)
            {
                status = (int)(General.StatusType.Cancelled);
                statusDescpt = General.StatusType.Cancelled.ToString();
                statusDesc = statusDescpt;
                return status;
            }
            else if (command == "Complete")
            {
                status = (int)(General.StatusType.Completed);
                statusDescpt = General.StatusType.Completed.ToString();
                statusDesc = statusDescpt;
                return status;
            }
            else if (command == ClearanceLayout.ReInstateProjectButton)
            {
                status = (int)(General.StatusType.ReInstated); // have to set based on previous state, for now hardcoded
                statusDescpt = General.StatusType.ReInstated.ToString();
                statusDesc = statusDescpt;
                return status;
            }
            else if (command == "ReOpen")
            {
                status = (int)(General.StatusType.ReOpened); // have to set based on previous state, for now hardcoded
                statusDescpt = General.StatusType.ReOpened.ToString();
                statusDesc = statusDescpt;
                return status;
            }
            else if (command == ClearanceLayout.MasterProjectInfoSubmitButton)
            {
                status = (int)(General.StatusType.Submitted); // have to set based on previous state, for now hardcoded
                statusDescpt = General.StatusType.Submitted.ToString();
                statusDesc = statusDescpt;
                return status;
            }
            else if (command == null)
            {
                statusDescpt = statusdesc;  // setting the same status as it was in case for postbacks and  command=null

            }
            if (string.IsNullOrEmpty(statusDescpt))
                statusDesc = statusdesc;
            else
                statusDesc = statusDescpt;

            LoggerFactory.LogWriter.MethodExit();

            return status;
        }

        #region "SaveProjectDetails"
        // Added for Saving and submitting  Master Project
        //NOte****** This function is called for Add to project button Also********
        public ActionResult SaveProjectDetails(string command, ClearanceProjectModel model, FormCollection collection, IList<ClearanceResource> list)
        {
            
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<string> ToolTiplist = MasterListTooltipControl();
                ViewBag.ControlsList = ToolTiplist;
                string statusDesc = string.Empty;
                model.MasterProjectDetails.IsResubmissionOccured = false;

                // ************START*********************Setting Next status--- Maintain MAster Project
                model.MasterProjectDetails.StatusType = GetNextProjectStatus(command, model.MasterProjectDetails.StatusType, model.MasterProjectDetails.StatusTypeDesc, model.MasterProjectDetails.IsSensitiveDataChanged, out  statusDesc);
                model.MasterProjectDetails.StatusTypeDesc = statusDesc;
                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), model.MasterProjectDetails.StatusType.ToString(), true).ToString();
                model = SetSelectedDropDown(model);

                if ((model.MasterProjectDetails.StatusType == (int)(General.StatusType.Cancelled))
                   || (model.MasterProjectDetails.StatusType == (int)(General.StatusType.Completed)))
                {
                    var userInfo = getUserInfo();
                    userInfo.UserLoginName = SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                    if (Request.Form["hdnProjectModifiedDate"] != null)
                    {
                        var serializer = new JavaScriptSerializer();
                        var modifiedDateRouted = serializer.Deserialize<DateTime>(Request.Form["hdnProjectModifiedDate"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                        model.MasterProjectDetails.ProjectModifiedDate = modifiedDateRouted;
                    }

                    //to make complete/cancel/reinstate in background
                    _IClearanceProjectRepository.StatusProjectUpdate(model.MasterProjectDetails.ClrProjectId, model.MasterProjectDetails.StatusType, null, userInfo, model.MasterProjectDetails.ProjectModifiedDate);
                    UpdateRequestWaitingToCancel(model);
                }


                // ************END*********************Setting Next status--- Maintain MAster Project
                // get master dropdowns on project Information Tab

                // START*********Territorry Funtionality for setting territorry collection
                ViewBag.ProjectTerritories = SetMasterManageTerritories(collection, model);
                // END*********Territorry Funtionality for setting territorry collection

                // **************Upload control functionality**********//


                //set artist for resource
                model = SetArtistDetailsForResource(model, collection);

                if (Request.Files.Count > 0 && Request.Files[0].FileName != string.Empty && command == null)
                {
                    return UploadMasterProjectDocument(model, collection["hdnRemoveFile"]);
                }
                else
                {
                    // set Default tab
                    ViewBag.DefaultTab = collection["hdnDefaultTab"];

                    if (collection["hdnAdditionalResourceCheck"] != null)
                    {
                        if (collection["hdnAdditionalResourceCheck"].ToString().Equals("NO"))
                        {
                            TempData["DefaultTabCheck"] = ViewBag.DefaultTab;
                        }
                        else if (collection["hdnAdditionalResourceCheck"].ToString().Equals("YES"))
                        {
                            model.MasterProjectDetails.IsResubmissionOccured = true;
                        }
                    }
                    string projectReferenceId = string.Empty;

                    LeanUserInfo userInfo = new LeanUserInfo();
                    // Add to project Function

                    if (Request.Form.Count > 0)
                    {
                        userInfo.UserLoginName = SessionWrapper.CurrentUserInfo.UserLoginName;  // to discuss

                        // Set MAster Project entities
                        model = SetMasterProjectEntities(model);
                        model.MasterProjectDetails.ClearanceResource = model.MasterProjectDetails.ClearanceResource != null ? RemoveResourceList(model.MasterProjectDetails.ClearanceResource) : new List<ClearanceResource>();

                        // START*********Insert Or Update Project if not coming from Add to project button
                        if (collection["resourceListFromSearchPopUp"].Equals(string.Empty))
                        {
                            var userInfoSave = getUserInfo();
                            userInfoSave.UserLoginName = SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                            model.MasterProjectDetails = _IClearanceProjectRepository.SaveProjectDetails(model.MasterProjectDetails, userInfoSave);
                            model.MasterProjectDetails.ResubmitReasonComments = string.Empty;
                            if (model.MasterProjectDetails.StatusType == (int)General.StatusType.Submitted)
                            {
                                model.MasterProjectDetails.StatusTypeDesc = General.StatusType.Submitted.ToString();
                            }
                            else if (model.MasterProjectDetails.StatusType == (int)General.StatusType.ReSubmitted)
                            {
                                model.MasterProjectDetails.StatusTypeDesc = General.StatusType.ReSubmitted.ToString();
                                if (model.MasterProjectDetails.IsSensitiveDataChanged)
                                    model.MasterProjectDetails.IsSensitiveDataChanged = false;
                            }

                            Session[Constants.Sessions.OldEntityMaster + model.MasterProjectDetails.ClearanceProjectID] = model;
                            if ((command == Constants.strCommand))//for Reopen Master Project
                            {
                                UpdateRequestCancelToWaiting(model);
                            }

                            Session["fileList" + "_" + model.MasterProjectDetails.ClearanceProjectID] = model.MasterProjectDetails.listUploadDocument;
                            projectReferenceId = model.MasterProjectDetails.ProjectReferenceId;
                            ModelState.Clear();
                            if (model.MasterProjectDetails.ClearanceResource == null)
                            {
                                model.MasterProjectDetails.ClearanceResource = new List<ClearanceResource>();
                            }
                            else
                            {
                                model.MasterProjectDetails.ClearanceResource.ToList().All(i =>
                                {
                                    i.ResourceResubmitReasonComments = string.Empty;
                                    return true;
                                });
                            }
                        }
                        // END*********Insert Or Update Project if not coming from Add to project button
                    }

                    ModelState.Clear();

                    //Remove all resources from listofClearanceResource which have archive flag Y

                    model.MasterProjectDetails.ClearanceResource = RemoveResourceList(model.MasterProjectDetails.ClearanceResource);

                    if (model.MasterProjectDetails.ClearanceResource != null)
                    {
                        model.MasterProjectDetails.ClearanceResource = model.MasterProjectDetails.ClearanceResource.OrderBy(Resourcelist => Resourcelist.ArtistName).ThenBy(Resourcelist => Resourcelist.Title).ThenBy(Resourcelist => Resourcelist.VersionTitle).ToList();
                        var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                        Territories.Add(1, model.MasterProjectDetails.Territories);

                        int tempI = 2;
                        if (model.MasterProjectDetails.ClearanceResource != null)
                        {
                            foreach (ClearanceResource cls in model.MasterProjectDetails.ClearanceResource)
                            {
                                Territories.Add(++tempI, cls.TerritorialRights);
                            }
                        }

                        ViewBag.ProjectTerritories = Territories;

                        if (model.MasterProjectDetails.StatusType == (int)(General.StatusType.Submitted) ||
                            model.MasterProjectDetails.StatusType == (int)(General.StatusType.ReSubmitted)
                            )
                        {
                            model.MasterProjectDetails.ClearanceResource.Where(i =>
                                i.IsNewlyAddedAfterSubmit == true).ToList().All(i =>
                                {
                                    i.IsNewlyAddedAfterSubmit = false;
                                    i.IsRouted = true;
                                    return true;
                                });

                            model.MasterProjectDetails.ClearanceResource.Where(i =>
                                i.IsRouted == false || i.IsRouted == null).ToList().All(i =>
                                {
                                    i.IsNewlyAddedAfterSubmit = true;
                                    i.IsRouted = false;
                                    return true;
                                });
                        }

                    }
                    if (model.roleGroupName == 0)
                    {
                        ViewBag.RoleGroup = BusinessEntities.Lookups.RoleGroup.Requestor;
                        model.roleGroupName = BusinessEntities.Lookups.RoleGroup.Requestor;
                    }
                    else
                    {
                        ViewBag.RoleGroup = model.roleGroupName;
                    }

                    ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), model.MasterProjectDetails.StatusType.ToString(), true).ToString();
                    if (model.MasterProjectDetails.RequestInfoList != null)
                    {
                        List<ClearanceInboxRequest> requestList;
                        byte routingStatus;
                        requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.MasterProjectDetails.ClearanceProjectID.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                        model.MasterProjectDetails.RequestInfoList = requestList;
                        var serializer = new JavaScriptSerializer();
                        model.MasterProjectDetails.RequestInfoList.All(irow =>
                        {
                            irow.ModifiedDateRequestString = serializer.Serialize(irow.ModifiedDateRequest);
                            irow.ModifiedDateRoutedString = serializer.Serialize(irow.ModifiedDate);
                            return true;
                        });
                    }

                    LoggerFactory.LogWriter.MethodExit();

                    ////Set the next status while Submit,ReOpening and Resubmit //Add by vikas UC-011A,B
                    if (model.MasterProjectDetails.Command == "save" && ((model.MasterProjectDetails.StatusType == (int)(General.StatusType.Submitted)) || (model.MasterProjectDetails.StatusType == (int)(General.StatusType.ReSubmitted)) || (model.MasterProjectDetails.StatusType == (int)(General.StatusType.ReOpened))))
                    {
                        // Call Save and return to home
                        ViewBag.DefaultTab = "2";
                        if (TempData["DefaultTabCheck"] != null)
                            ViewBag.DefaultTab = TempData["DefaultTabCheck"];
                        return View("CreateMasterProject", model);
                    }

                    if (command != null && (model.MasterProjectDetails.StatusType == (int)(General.StatusType.ReSubmitted)) ||
                        model.MasterProjectDetails.StatusType == (int)(General.StatusType.Cancelled) ||
                        model.MasterProjectDetails.StatusType == (int)(General.StatusType.Completed))
                    {
                        // Call Save and return to homeD;
                        ViewBag.DefaultTab = "2";
                        if (TempData["DefaultTabCheck"] != null)
                            ViewBag.DefaultTab = TempData["DefaultTabCheck"]; ;
                        return View("CreateMasterProject", model);
                    }

                    if ((command == ClearanceLayout.MasterProjectInfoSubmitButton) && model.MasterProjectDetails.StatusType == (int)(General.StatusType.Submitted))
                    {
                        ViewBag.DefaultTab = "2";
                        if (TempData["DefaultTabCheck"] != null)
                            ViewBag.DefaultTab = TempData["DefaultTabCheck"];
                        return View("CreateMasterProject", model);
                    }
                    if ((command == ClearanceLayout.ReInstateProjectButton) && model.MasterProjectDetails.StatusType == (int)(General.StatusType.Submitted))
                    {
                        return View("CreateMasterProject", model);
                    }

                    if (model.MasterProjectDetails.StatusType == (int)(General.StatusType.ReOpened))
                    {
                        // Call Update Method and return Edit
                        return View("CreateMasterProject", model);
                    }

                    // Exception Handling block
                    if (string.IsNullOrEmpty(projectReferenceId))
                    {
                        // If condition when coming from AddToProject button, thus donot set validation message
                        if (collection["resourceListFromSearchPopUp"].Equals(string.Empty))
                        {
                            ViewBag.ValidationWarningMsg = ClearanceLayout.UnsuccesfullMessageProjectSave;
                        }
                        return View("CreateMasterProject", model);
                    }
                    else
                    {
                        ViewBag.DefaultTab = "2";
                        if (TempData["DefaultTabCheck"] != null)
                            ViewBag.DefaultTab = TempData["DefaultTabCheck"];

                        ViewBag.ValidationMsg = ClearanceLayout.SuccesfullMessageProjectSave;
                        return View("CreateMasterProject", model);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                model.MasterProjectDetails.listUploadDocument = new List<UploadDocument>();
                if (ex.Message.Contains("Concurrency Exception :"))
                {
                    ViewBag.ConcurrencyWarningMsg = ex.Message;
                }
                else
                {
                    ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                }

                return View("CreateMasterProject", model);
            }
        }

        private Dictionary<long, List<TerritorialDisplay>> SetMasterManageTerritories(FormCollection collection, ClearanceProjectModel model)
        {
            LoggerFactory.LogWriter.MethodStart();

            var Territories = new Dictionary<long, List<TerritorialDisplay>>();
            int rowId = 0;

            var collectionList = collection.AllKeys.Where(s => s.Contains("hdnterritoryDetailsForSave")).ToList();

            foreach (string terriVal in collectionList)
            {
                string jsonObject = collection[terriVal].ToString();

                if (jsonObject != null)
                {
                    jsonObject = jsonObject.Replace("Cote d'Ivoire", "Cote dIvoire");
                    jsonObject = jsonObject.Replace('\'', '\"');
                    JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
                    List<TerritorialDisplay> territoryDetails = jsonSerialize.Deserialize<List<TerritorialDisplay>>(jsonObject);
                    if (terriVal == "hdnterritoryDetailsForSave_1")
                    {
                        model.MasterProjectDetails.Territories = territoryDetails;

                        if (collection["hdnincludeTerritoryString_1"] != null && collection["hdnincludeTerritoryString_1"] != string.Empty)
                        {
                            model.MasterProjectDetails.IncludedTerritories = collection["hdnincludeTerritoryString_1"].ToString(CultureInfo.InvariantCulture);
                        }

                        if (collection["hdnExcludeTerritoryString_1"] != null && collection["hdnExcludeTerritoryString_1"] != string.Empty)
                        {
                            model.MasterProjectDetails.ExcludedTerritories = collection["hdnExcludeTerritoryString_1"].ToString(CultureInfo.InvariantCulture);
                        }

                        Territories.Add(1, territoryDetails);
                    }
                    else
                    {
                        model.MasterProjectDetails.ClearanceResource[rowId].TerritorialRights = territoryDetails;
                        int tempResourceId = rowId + 3;

                        if (collection["hdnincludeTerritoryString_" + tempResourceId] != null && collection["hdnincludeTerritoryString_" + tempResourceId] != string.Empty)
                        {
                            model.MasterProjectDetails.ClearanceResource[rowId].IncludedTerritories = collection["hdnincludeTerritoryString_" + tempResourceId].ToString(CultureInfo.InvariantCulture);
                        }

                        if (collection["hdnExcludeTerritoryString_" + tempResourceId] != null && collection["hdnExcludeTerritoryString_" + tempResourceId] != string.Empty)
                        {
                            model.MasterProjectDetails.ClearanceResource[rowId].ExcludedTerritories = collection["hdnExcludeTerritoryString_" + tempResourceId].ToString(CultureInfo.InvariantCulture);
                        }

                        rowId++;
                        Territories.Add(2 + rowId, territoryDetails);
                    }

                    ViewBag.ProjectTerritories = Territories;
                }

            }

            LoggerFactory.LogWriter.MethodExit();

            return Territories;
        }

        private Dictionary<long, List<TerritorialDisplay>> SetManageTerritories(FormCollection collection, ClearanceProjectModel model)
        {
            LoggerFactory.LogWriter.MethodStart();

            var Territories = new Dictionary<long, List<TerritorialDisplay>>();
            int rowId = 0;

            var collectionList = collection.AllKeys.Where(s => s.Contains("hdnterritoryDetailsForSave")).ToList();

            foreach (string terriVal in collectionList)
            {
                string jsonObject = collection[terriVal].ToString();

                if (jsonObject != null)
                {
                    jsonObject = jsonObject.Replace("Cote d'Ivoire", "Cote dIvoire");
                    jsonObject = jsonObject.Replace('\'', '\"');
                    JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
                    List<TerritorialDisplay> territoryDetails = jsonSerialize.Deserialize<List<TerritorialDisplay>>(jsonObject);
                    if (terriVal == "hdnterritoryDetailsForSave_1")
                    {
                        model.RegularProjectDetails.Territories = territoryDetails;
                        if (collection["hdnincludeTerritoryString_1"] != null && collection["hdnincludeTerritoryString_1"] != string.Empty)
                        {
                            model.RegularProjectDetails.IncludedTerritories = collection["hdnincludeTerritoryString_1"].ToString(CultureInfo.InvariantCulture);
                        }
                        if (collection["hdnExcludeTerritoryString_1"] != null && collection["hdnExcludeTerritoryString_1"] != string.Empty)
                        {
                            model.RegularProjectDetails.ExcludedTerritories = collection["hdnExcludeTerritoryString_1"].ToString(CultureInfo.InvariantCulture);
                        }

                        Territories.Add(1, territoryDetails);
                    }
                    else if (terriVal == "hdnterritoryDetailsForSave_2")
                    {
                        model.RegularProjectDetails.ScopeAndRequestType.Territories = territoryDetails;
                        if (collection["hdnincludeTerritoryString_2"] != null && collection["hdnincludeTerritoryString_2"] != string.Empty)
                        {
                            model.RegularProjectDetails.ScopeAndRequestType.IncludedTerritories = collection["hdnincludeTerritoryString_2"].ToString(CultureInfo.InvariantCulture);
                        }
                        if (collection["hdnExcludeTerritoryString_2"] != null && collection["hdnExcludeTerritoryString_2"] != string.Empty)
                        {
                            model.RegularProjectDetails.ScopeAndRequestType.ExcludedTerritories = collection["hdnExcludeTerritoryString_2"].ToString(CultureInfo.InvariantCulture);
                        }

                        Territories.Add(2, territoryDetails);
                    }
                    else
                    {
                        model.RegularProjectDetails.ClearanceResource[rowId].TerritorialRights = territoryDetails;
                        int tempResourceId = rowId + 3;

                        if (collection["hdnincludeTerritoryString_" + tempResourceId] != null && collection["hdnincludeTerritoryString_" + tempResourceId] != string.Empty)
                        {
                            model.RegularProjectDetails.ClearanceResource[rowId].IncludedTerritories = collection["hdnincludeTerritoryString_" + tempResourceId].ToString(CultureInfo.InvariantCulture);
                        }

                        if (collection["hdnExcludeTerritoryString_" + tempResourceId] != null && collection["hdnExcludeTerritoryString_" + tempResourceId] != string.Empty)
                        {
                            model.RegularProjectDetails.ClearanceResource[rowId].ExcludedTerritories = collection["hdnExcludeTerritoryString_" + tempResourceId].ToString(CultureInfo.InvariantCulture);
                        }

                        rowId++;
                        Territories.Add(2 + rowId, territoryDetails);
                    }
                }

            }

            LoggerFactory.LogWriter.MethodExit();

            return Territories;
        }

        public ClearanceProjectModel SetRegularProjectEntities(string command, ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                SetClearanceProjectEntity(model.RegularProjectDetails, false);
                if (model.RegularProjectDetails.MultiArtist)
                {
                    model.RegularProjectDetails.Compilation = true;
                }

                if (Request.Form["hdnProjectModifiedDate"] != null)
                {
                    var serializer = new JavaScriptSerializer();
                    var modifiedDateRouted = serializer.Deserialize<DateTime>(Request.Form["hdnProjectModifiedDate"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    model.RegularProjectDetails.ProjectModifiedDate = modifiedDateRouted;
                }

                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    foreach (var resource in model.RegularProjectDetails.ClearanceResource)
                    {
                        // for all recording Types
                        if (resource.RecordingTypeDesc != null)
                        {
                            if (string.Compare(resource.RecordingTypeDesc, General.LiveStudioType.Live.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.LiveStudioType = ((int)General.LiveStudioType.Live).ToString();
                            else if (string.Compare(resource.RecordingTypeDesc, General.LiveStudioType.Studio.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.LiveStudioType = ((int)General.LiveStudioType.Studio).ToString();
                        }

                        // for all resource type
                        if (resource.ResourceTypeDesc != null)
                        {
                            if (string.Compare(resource.ResourceTypeDesc, General.ResourceType.Audio.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.ResourceType = ((int)General.ResourceType.Audio).ToString();
                            else if (string.Compare(resource.ResourceTypeDesc, General.ResourceType.Image.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.ResourceType = ((int)General.ResourceType.Image).ToString();
                            else if (string.Compare(resource.ResourceTypeDesc, General.ResourceType.Merchandise.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.ResourceType = ((int)General.ResourceType.Merchandise).ToString();
                            else if (string.Compare(resource.ResourceTypeDesc, General.ResourceType.Other.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.ResourceType = ((int)General.ResourceType.Other).ToString();
                            else if (string.Compare(resource.ResourceTypeDesc, General.ResourceType.Text.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.ResourceType = ((int)General.ResourceType.Text).ToString();
                        }

                        if (resource.MusicTypeDesc != null)
                        {
                            if (string.Compare(resource.MusicTypeDesc, General.MusicClassType.Classical.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.MusicClassType = ((int)General.MusicClassType.Classical).ToString();
                            else if (string.Compare(resource.MusicTypeDesc, General.MusicClassType.Jazz.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.MusicClassType = ((int)General.MusicClassType.Jazz).ToString();
                            else if (string.Compare(resource.MusicTypeDesc, General.MusicClassType.Pop.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.MusicClassType = ((int)General.MusicClassType.Pop).ToString();
                            else if (string.Compare(resource.MusicTypeDesc, General.MusicClassType.Other.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                                resource.MusicClassType = ((int)General.MusicClassType.Other).ToString();
                        }
                        if (resource.ArtistInfo != null)
                        {
                            foreach (var artist in resource.ArtistInfo)
                            {
                                artist.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                            }
                        }
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return model;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // Set MasterProjectSaveEntitites
        private ClearanceProjectModel SetMasterProjectEntities(ClearanceProjectModel model)
        {
            LoggerFactory.LogWriter.MethodStart();

            SetClearanceProjectEntity(model.MasterProjectDetails, true);
            model.MasterProjectDetails.Advertising.MasterRequestType = Convert.ToInt32(General.MasterRequestType.Advertising);
            model.MasterProjectDetails.Film.MasterRequestType = Convert.ToInt32(General.MasterRequestType.Film);
            model.MasterProjectDetails.Trailer.MasterRequestType = Convert.ToInt32(General.MasterRequestType.Trailer);
            model.MasterProjectDetails.Others.MasterRequestType = Convert.ToInt32(General.MasterRequestType.Other);

            if (Request.Form["hdnProjectModifiedDate"] != null)
            {
                var serializer = new JavaScriptSerializer();
                var modifiedDateRouted = serializer.Deserialize<DateTime>(Request.Form["hdnProjectModifiedDate"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                model.MasterProjectDetails.ProjectModifiedDate = modifiedDateRouted;
            }

            if (model.MasterProjectDetails.ClearanceResource != null)
            {
                foreach (var resource in model.MasterProjectDetails.ClearanceResource)
                {
                    // for all recording Types
                    if (resource.RecordingTypeDesc != null)
                    {
                        if (resource.RecordingTypeDesc.ToUpper() == General.LiveStudioType.Live.ToString().ToUpper())
                            resource.LiveStudioType = ((int)General.LiveStudioType.Live).ToString();
                        else if (resource.RecordingTypeDesc.ToUpper() == General.LiveStudioType.Studio.ToString().ToUpper())
                            resource.LiveStudioType = ((int)General.LiveStudioType.Studio).ToString();
                    }
                    // for all resource type
                    if (resource.ResourceTypeDesc != null)
                    {
                        if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Audio.ToString().ToUpper())
                            resource.ResourceType = ((int)General.ResourceType.Audio).ToString();
                        else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Image.ToString().ToUpper())
                            resource.ResourceType = ((int)General.ResourceType.Image).ToString();
                        else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Merchandise.ToString().ToUpper())
                            resource.ResourceType = ((int)General.ResourceType.Merchandise).ToString();
                        else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Other.ToString().ToUpper())
                            resource.ResourceType = ((int)General.ResourceType.Other).ToString();
                        else if (resource.ResourceTypeDesc.ToUpper() == General.ResourceType.Text.ToString().ToUpper())
                            resource.ResourceType = ((int)General.ResourceType.Text).ToString();
                    }

                    // for all music type
                    if (resource.MusicTypeDesc != null)
                    {
                        if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Classical.ToString().ToUpper())
                            resource.MusicClassType = ((int)General.MusicClassType.Classical).ToString();
                        else if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Jazz.ToString().ToUpper())
                            resource.MusicClassType = ((int)General.MusicClassType.Jazz).ToString();
                        else if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Pop.ToString().ToUpper())
                            resource.MusicClassType = ((int)General.MusicClassType.Pop).ToString();
                        else if (resource.MusicTypeDesc.ToUpper() == General.MusicClassType.Other.ToString().ToUpper())
                            resource.MusicClassType = ((int)General.MusicClassType.Other).ToString();
                        // For freehand set created by user
                    }

                    if (resource.ArtistInfo != null)
                    {
                        foreach (var artist in resource.ArtistInfo)
                        {
                            artist.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                        }
                    }

                    if (model.MasterProjectDetails.SensitiveExplotation)
                    {
                        resource.SensitiveExplotation_ClearanceResource = true;
                    }
                }
            }

            LoggerFactory.LogWriter.MethodExit();

            return model;
        }

        // set selected dropdowns on project Information Tab
        private ClearanceProjectModel SetSelectedDropDown(ClearanceProjectModel model)
        {
            LoggerFactory.LogWriter.MethodStart();

            _IClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(SessionWrapper.CurrentUserInfo.UserLoginName);
            model.CurrencyList = _IClearanceProjectModel.CurrencyList;
            model.CompanyList = _IClearanceProjectModel.CompanyList;

            //To set Dropdown values of freehand Resource
            _type = new List<string>();
            _type.Add(Constants.ClearanceMusicType);//pass type as constants
            _type.Add(Constants.ClearanceResourceType);
            _type.Add(Constants.ClearanceRecordingType);
            _clearanceResourceModel = _IClearanceProjectRepository.GetMasterDataResource(_type, getUserInfo());
            _type.Clear();
            model.RecordingTypeResourceTab = _clearanceResourceModel.RecordingType;
            _clearanceResourceModel.ResourceType = _clearanceResourceModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
            model.ResourceTypeResourceTab = _clearanceResourceModel.ResourceType;

            var MusicType = _clearanceResourceModel.MusicType.Where(x => x.Text.ToUpper() != "JAZZ").ToList();
            _clearanceResourceModel.MusicType = MusicType;
            model.MusicTypeResourceTab = _clearanceResourceModel.MusicType;
            //End-To set Dropdown values of freehand Resource

            LoggerFactory.LogWriter.MethodExit();

            return model;
        }

        // Add List of resources from Advance Popup
        private ClearanceProjectModel AdvancePopupResourceListAdd(ClearanceProjectModel model, string resourceListFromSearchPopup)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                // add list of resources from addtoproject
                if (!resourceListFromSearchPopup.Equals(string.Empty))
                {
                    // set Default tab
                    ViewBag.DefaultTab = 1;
                    List<ClearanceResource> listSelectedItems = new List<ClearanceResource>();
                    listSelectedItems = ParseString(resourceListFromSearchPopup);
                    for (int i = 0; i < listSelectedItems.Count; i++)
                    {
                        if (listSelectedItems[i].ExcerptTime == null)
                        {
                            listSelectedItems[i].ExcerptTime = listSelectedItems[i].Duration;
                        }

                        if (model.MasterProjectDetails.ClearanceResource != null)
                        {
                            if (listSelectedItems[i].ResourceIndexToUpdate != 0)
                            {
                                int InsertIndex = UpdateMasterProjectResource(listSelectedItems[0].ResourceIndexToUpdate, model);
                                //find index of column
                                listSelectedItems[i].IsNewlyAddedAfterSubmit = false;
                                model.MasterProjectDetails.ClearanceResource.Insert(InsertIndex, listSelectedItems[i]);
                                model.MasterProjectDetails.ClearanceResource[InsertIndex].FreeHandResourceStatus = "Y";
                                if (model.MasterProjectDetails.ClearanceResource[InsertIndex - 1].ResourceId > 0)
                                {
                                    model.MasterProjectDetails.ClearanceResource[InsertIndex].ResourceIdToUpdate = model.MasterProjectDetails.ClearanceResource[InsertIndex - 1].ResourceId;
                                }
                                else
                                {
                                    model.MasterProjectDetails.ClearanceResource[InsertIndex].ResourceIdToUpdate = model.MasterProjectDetails.ClearanceResource[InsertIndex - 1].ResourceIdToUpdate;
                                }

                                model.MasterProjectDetails.ClearanceResource[InsertIndex].SuggestedFee = model.MasterProjectDetails.ClearanceResource[InsertIndex - 1].SuggestedFee;
                                model.MasterProjectDetails.ClearanceResource[InsertIndex].SensitiveExplotation_ClearanceResource = model.MasterProjectDetails.ClearanceResource[InsertIndex - 1].SensitiveExplotation_ClearanceResource;
                                model.MasterProjectDetails.ClearanceResource[InsertIndex].Comments = model.MasterProjectDetails.ClearanceResource[InsertIndex - 1].Comments;
                            }
                            else
                            {
                                listSelectedItems[i].IsNewlyAddedAfterSubmit = true; //Added for UC-011A
                                model.MasterProjectDetails.ClearanceResource.Insert(0, listSelectedItems[i]); //Updated for UC-011B
                            }
                        }
                        else
                        {
                            List<ClearanceResource> listResource = new List<ClearanceResource>();
                            listSelectedItems[i].IsNewlyAddedAfterSubmit = true; //Added for UC-011A

                            listResource.Insert(0, listSelectedItems[i]);
                            model.MasterProjectDetails.ClearanceResource = listResource;
                        }
                    }

                }

                LoggerFactory.LogWriter.MethodExit();

                return model;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // Add List of resources from Advance Popup
        private ClearanceProjectModel AdvancePopupResourceListAddRegular(ClearanceProjectModel model, string ListIsrc, string resourceListFromSearchPopUp)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                // set archive Flag Delete when Remove clicked
                if (ListIsrc != null)
                {
                    if (!ListIsrc.Equals(string.Empty))
                    {
                        string[] listSelectedItems = ListIsrc.Split(','); ;
                        for (int i = 0; i < listSelectedItems.Length; i++)
                        {
                            model.RegularProjectDetails.ClearanceResource.Where(r => r.Isrc == listSelectedItems[i]).ToList().ForEach(rr => rr.ArchiveFlag = "Y");
                        }
                    }
                }

                // add list of resources from addtoproject
                if (resourceListFromSearchPopUp != null)
                {
                    if (!resourceListFromSearchPopUp.Equals(string.Empty))
                    {
                        // set Default tab
                        ViewBag.DefaultTab = 3;
                        List<ClearanceResource> listSelectedItems = new List<ClearanceResource>();
                        listSelectedItems = ParseString(resourceListFromSearchPopUp);
                        for (int i = 0; i < listSelectedItems.Count; i++)
                        {
                            if (model.RegularProjectDetails.ClearanceResource != null)
                            {
                                if (listSelectedItems[i].ResourceIndexToUpdate != 0)
                                {
                                    //Add by vikas to update the resource
                                    int InsertIndex = UpdateRegularProjectResource(listSelectedItems[0].ResourceIndexToUpdate, model);
                                    //find index of column
                                    listSelectedItems[i].IsNewlyAddedAfterSubmit = false; //Added for UC-011A
                                    model.RegularProjectDetails.ClearanceResource.Insert(InsertIndex, listSelectedItems[i]);
                                    model.RegularProjectDetails.ClearanceResource[InsertIndex].FreeHandResourceStatus = "Y";
                                    if (model.RegularProjectDetails.ClearanceResource[InsertIndex - 1].ResourceId > 0)
                                    {
                                        model.RegularProjectDetails.ClearanceResource[InsertIndex].ResourceIdToUpdate = model.RegularProjectDetails.ClearanceResource[InsertIndex - 1].ResourceId;
                                    }
                                    else
                                    {
                                        model.RegularProjectDetails.ClearanceResource[InsertIndex].ResourceIdToUpdate = model.RegularProjectDetails.ClearanceResource[InsertIndex - 1].ResourceIdToUpdate;
                                    }

                                    model.RegularProjectDetails.ClearanceResource[InsertIndex].SuggestedFee = model.RegularProjectDetails.ClearanceResource[InsertIndex - 1].SuggestedFee;
                                    model.RegularProjectDetails.ClearanceResource[InsertIndex].SensitiveExplotation_ClearanceResource = model.RegularProjectDetails.ClearanceResource[InsertIndex - 1].SensitiveExplotation_ClearanceResource;
                                    model.RegularProjectDetails.ClearanceResource[InsertIndex].Comments = model.RegularProjectDetails.ClearanceResource[InsertIndex - 1].Comments;
                                }
                                else
                                {
                                    listSelectedItems[i].IsNewlyAddedAfterSubmit = true; //Added for UC-011A
                                    model.RegularProjectDetails.ClearanceResource.Insert(0, listSelectedItems[i]);
                                }
                            }
                            else
                            {
                                List<ClearanceResource> listResource = new List<ClearanceResource>();
                                listSelectedItems[i].IsNewlyAddedAfterSubmit = true; //Added for UC-011A
                                listResource.Insert(0, listSelectedItems[i]);
                                model.RegularProjectDetails.ClearanceResource = listResource;
                            }
                        }
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return model;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // Removed resources from list which have "Y" flag
        private List<ClearanceResource> RemoveResourceList(List<ClearanceResource> resourceList)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                resourceList.RemoveAll(r => r.ArchiveFlag == Constants.ArchiveFlag);
                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return resourceList;
        }

        // Removed releases from list which have "Y" flag
        private List<ClearanceRelease> RemoveReleaseFromList(List<ClearanceRelease> releaseList)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                releaseList.RemoveAll(r => r.Archive_Flag == Constants.ArchiveFlag);
                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return releaseList;
        }

        // Remove from DB when Remove is Clicked
        [HttpPost]
        public string MasterProjectRemoveResource(ClearanceProjectModel model, string ClearanceResourceId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                bool flag;
                List<long> listClrearanceResourceIds = null;
                if (model.MasterProjectDetails.ClearanceResource != null && (!ClearanceResourceId.Equals(string.Empty)))
                {
                    listClrearanceResourceIds = new List<long>();
                    foreach (var resource in model.MasterProjectDetails.ClearanceResource)
                    {
                        if ((resource.ClearanceResourceId.ToString() == ClearanceResourceId) && (resource.ClearanceResourceId != 0))
                            listClrearanceResourceIds.Add(resource.ClearanceResourceId);
                    }
                    if (listClrearanceResourceIds != null)
                        flag = _IClearanceResourceRepository.RemoveResourceProject("Y", listClrearanceResourceIds, getUserInfo());
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

        [HttpPost]
        public string RegularProjectRemoveResource(ClearanceProjectModel model, string ClearanceResourceId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                bool flag;
                List<long> listClrearanceResourceIds = null;
                if (model.RegularProjectDetails.ClearanceResource != null && (!ClearanceResourceId.Equals(string.Empty)))
                {
                    listClrearanceResourceIds = new List<long>();
                    foreach (var resource in model.RegularProjectDetails.ClearanceResource)
                    {
                        if ((resource.ClearanceResourceId != 0) && (resource.ClearanceResourceId.ToString() == ClearanceResourceId))
                            listClrearanceResourceIds.Add(resource.ClearanceResourceId);
                    }
                    if (listClrearanceResourceIds != null)
                        flag = _IClearanceResourceRepository.RemoveResourceProject("Y", listClrearanceResourceIds, getUserInfo());
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

        #endregion "SaveProjectDetails"

        #region "Save Regular Project and Company Operations"

        public ActionResult RemoveThirdParty()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (Session["manageCompany"] != null)
                {
                    Session.Remove("managecompany");
                }

                var viewName = "ProjectInformationRegular";
                LoggerFactory.LogWriter.MethodExit();

                return PartialView(viewName, _IClearanceProjectModel);
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult RemoveThirdParty(string id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return Content("true");
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Content("false");
            }
        }

        public JsonResult DeleteCompany(int companyID)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<CompanyInfo> companyList = new List<CompanyInfo>();
                if (Session["ThirdParty"] != null)
                {
                    Session.Remove("ThirdParty");
                }
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = "OK", Records = companyList.AsQueryable(), TotalRecordCount = companyList.Count });
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        public JsonResult RemoveCompany(string companyIds, string deletedCompIds)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<CompanyInfo> companyList = new List<CompanyInfo>();
                if ((!string.IsNullOrEmpty(companyIds)) && (!string.IsNullOrEmpty(deletedCompIds)))
                {
                    if (companyIds.Contains(deletedCompIds))
                        companyIds = companyIds.Replace(deletedCompIds, "").Trim();
                }

                if (!string.IsNullOrEmpty(companyIds))
                    companyList = _IClearanceProjectRepository.GetCompanies(companyIds, getUserInfo());
                
                if (Session["ThirdParty"] != null)
                    Session.Remove("ThirdParty");
                
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = companyList.AsQueryable(), TotalRecordCount = companyList.Count });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public JsonResult AddCompany(string companyIds, string deletedCompIds)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<CompanyInfo> companyList = new List<CompanyInfo>();
                if ((!string.IsNullOrEmpty(companyIds)) && (!string.IsNullOrEmpty(deletedCompIds)))
                {
                    if (companyIds.Contains(deletedCompIds))
                    {
                        companyIds = companyIds.Replace(deletedCompIds, "").Trim();
                        if (Session["ThirdParty"] != null)
                        {
                            Session.Remove("ThirdParty");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(companyIds))
                {
                    companyList = _IClearanceProjectRepository.GetCompanies(companyIds, getUserInfo());
                }

                if (!string.IsNullOrEmpty(companyIds))
                {
                    Session["ThirdParty"] = companyIds;
                }

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = companyList.AsQueryable(), TotalRecordCount = companyList.Count });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public List<ClearanceRelease> SearchReleaseR2(string upcNumber, string ReleaseTitle, string ArtistName, long ArtistID, string r2ProjectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ReleaseSearchCriteria releaseSearchCriteria = new ReleaseSearchCriteria();
                releaseSearchCriteria.Upc = upcNumber;
                releaseSearchCriteria.ArtistName = ArtistName;
                releaseSearchCriteria.ArtistId = ArtistID;
                releaseSearchCriteria.ReleaseTitle = ReleaseTitle;
                releaseSearchCriteria.R2ProjectId = r2ProjectId;
                releaseSearchCriteria.Criteria.RowIndex = -1;
                releaseSearchCriteria.Criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ReleasePageSize"]);
                releaseSearchCriteria.Criteria.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                releaseSearchCriteria.Criteria.QualificationCriteria = true;

                ReleaseSearchResult releaseSearchResult = _IClearanceReleaseRepository.R2ReleaseSearch(releaseSearchCriteria, getUserInfo());

                List<ClearanceRelease> objreleaseEntity = new List<ClearanceRelease>();
                if (releaseSearchResult.Values.Count > 0)
                {
                    foreach (var releaseSearch in releaseSearchResult.Values)
                    {
                        ClearanceRelease clrReleaseSearch = new ClearanceRelease();
                        clrReleaseSearch.resourceDetail = new List<ClearanceResource>();
                        clrReleaseSearch.AdminCompanyId = releaseSearch.AdminCompanyId;
                        clrReleaseSearch.ArtistInfo = releaseSearch.ArtistInfo;
                        if (clrReleaseSearch.ArtistInfo != null && clrReleaseSearch.ArtistInfo.Count > 0)
                        {
                            foreach (ArtistInfo artistInfor in clrReleaseSearch.ArtistInfo)
                            {
                                if (string.IsNullOrEmpty(clrReleaseSearch.ArtistName))
                                    clrReleaseSearch.ArtistName = artistInfor.Name;
                                else
                                    clrReleaseSearch.ArtistName = string.Format("{0},{1}", clrReleaseSearch.ArtistName, artistInfor.Name);
                            }
                        }
                        else
                        {
                            clrReleaseSearch.ArtistName = releaseSearch.ArtistName;
                        }

                        clrReleaseSearch.AssignedType = releaseSearch.AssignedType;
                        clrReleaseSearch.CatalogueNo = releaseSearch.CatalogueNo;
                        clrReleaseSearch.ComponentCount = releaseSearch.ComponentCount;
                        clrReleaseSearch.Configuration = releaseSearch.Configuration;
                        clrReleaseSearch.ConfigurationDisplay = releaseSearch.ConfigurationDisplay;
                        clrReleaseSearch.DataAdminCompany = releaseSearch.DataAdminCompany;
                        clrReleaseSearch.DataAdminCompanyId = releaseSearch.DataAdminCompanyId;
                        clrReleaseSearch.DivisionId = releaseSearch.DivisionId;
                        clrReleaseSearch.EarilerReleaseDate = releaseSearch.EarilerReleaseDate;
                        clrReleaseSearch.Grid = releaseSearch.Grid;
                        clrReleaseSearch.IsAlreadyLinked = releaseSearch.IsAlreadyLinked;
                        clrReleaseSearch.IsMac = releaseSearch.IsMac;
                        clrReleaseSearch.IsMediaPortal = releaseSearch.IsMediaPortal;
                        clrReleaseSearch.LabelId = releaseSearch.LabelId;
                        clrReleaseSearch.labelName = _IClearanceProjectRepository.getLabelNmForExistingRelease(Convert.ToInt32(releaseSearch.LabelId), getUserInfo());
                        clrReleaseSearch.LinkedContractDetails = releaseSearch.LinkedContractDetails;
                        clrReleaseSearch.MobileArtist = releaseSearch.MobileArtist;
                        clrReleaseSearch.MusicType_Desc = releaseSearch.MusicClassType;
                        clrReleaseSearch.MusicType_Id = _IClearanceProjectRepository.getMusicClassTypeIdForExistingRelease(releaseSearch.MusicClassType, getUserInfo());
                        clrReleaseSearch.OwnedProjectId = releaseSearch.OwnedProjectId;
                        clrReleaseSearch.PackageIndicator = releaseSearch.PackageIndicator;

                        if (string.IsNullOrEmpty(clrReleaseSearch.PackageIndicator) || clrReleaseSearch.PackageIndicator.Contains('N'))
                            clrReleaseSearch.PackageText = "No";
                        else
                            clrReleaseSearch.PackageText = "Yes";

                        clrReleaseSearch.PackageInfo = releaseSearch.PackageInfo;
                        clrReleaseSearch.PCompanyId = releaseSearch.PCompanyId;
                        clrReleaseSearch.PCompanyName = releaseSearch.PCompanyName;
                        clrReleaseSearch.PLicensingExtension = releaseSearch.PLicensingExtension;
                        clrReleaseSearch.PYear = releaseSearch.PYear;
                        clrReleaseSearch.R2AccountId = releaseSearch.R2AccountId;
                        clrReleaseSearch.R2ReleaseId = releaseSearch.ReleaseId;
                        clrReleaseSearch.R2Status = releaseSearch.R2Status;
                        clrReleaseSearch.R2StatusType = releaseSearch.R2StatusType;
                        clrReleaseSearch.ReleaseTitle = releaseSearch.ReleaseTitle;
                        clrReleaseSearch.ReleaseType = releaseSearch.ReleaseType;
                        clrReleaseSearch.ScopeType = releaseSearch.ScopeType;
                        clrReleaseSearch.Sequence = releaseSearch.Sequence;
                        clrReleaseSearch.SoundtrackIndicator = releaseSearch.SoundtrackIndicator;
                        clrReleaseSearch.Is_Ost = clrReleaseSearch.SoundtrackIndicator.Equals("1") ? true : false;
                        clrReleaseSearch.TrackCount = releaseSearch.TrackCount;
                        clrReleaseSearch.TrackInfo = releaseSearch.TrackInfo;
                        clrReleaseSearch.Upc = releaseSearch.Upc;
                        clrReleaseSearch.UserName = releaseSearch.UserName;
                        clrReleaseSearch.VersionTitle = releaseSearch.VersionTitle;
                        // set checkboxfor as true by default
                        clrReleaseSearch.IsDeviatedICLALevel_Club = false;
                        clrReleaseSearch.IsDeviatedICLALevel_Non = false;
                        clrReleaseSearch.IsDeviatedICLALevel_Price = false;
                        clrReleaseSearch.IsDeviatedICLALevel_Promotional = false;
                        clrReleaseSearch.IsDeviatedICLALevel_Regular = false;
                        clrReleaseSearch.IsDeviatedICLALevel_TV = false;
                        clrReleaseSearch.IsNewlyAddedAfterSubmit = true;
                        clrReleaseSearch.Archive_Flag = "N";
                        objreleaseEntity.Insert(0, clrReleaseSearch);
                    }

                }
                LoggerFactory.LogWriter.MethodExit();

                return objreleaseEntity;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private ClearanceReleaseModel GetR2ReleaseTracks(ClearanceRelease clrRelease)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<TrackInfo> trackInfo = new List<TrackInfo>();
                clrRelease.resourceDetail = new List<ClearanceResource>();
                ClearanceReleaseModel clrReleaseModel = new ClearanceReleaseModel();
                trackInfo = _IClearanceReleaseRepository.R2GetReleaseAdditionalDetails(clrRelease.R2ReleaseId, getUserInfo());

                if (trackInfo != null)
                {
                    if (trackInfo.Count > 0)
                    {
                        trackInfo = trackInfo.OrderBy(trackinf => trackinf.SequenceNo).ToList();
                        List<ClearanceResource> clrResourceAdd = new List<ClearanceResource>();
                        foreach (TrackInfo trackAdd in trackInfo)
                        {
                            ClearanceResource clrResourceSearch = new ClearanceResource();
                            clrResourceSearch.ArtistInfo = trackAdd.ArtistInfo;

                            var artistNameList = trackAdd.ArtistInfo.Where(s => s.IsPrimary == null).Select(a => a.Name).ToList();
                            if (clrRelease.R2ReleaseId == trackAdd.ReleaseId)
                            {
                                artistNameList = trackAdd.ArtistInfo.Where(s => s.IsPrimary == "Y").Select(a => a.Name).ToList();
                            }

                            if (artistNameList != null && artistNameList.Count > 0)
                            {
                                clrResourceSearch.ArtistName = string.Join(",", artistNameList);
                            }

                            if (trackAdd.Isrc != null && trackAdd.Isrc.Count() > 0)
                            {
                                foreach (var isrcAdd in trackAdd.Isrc)
                                {
                                    if (string.IsNullOrEmpty(clrResourceSearch.Isrc))
                                    {
                                        clrResourceSearch.Isrc = isrcAdd;
                                    }
                                    else
                                    {
                                        clrResourceSearch.Isrc = string.Format("{0},{1}", clrResourceSearch.Isrc, isrcAdd);
                                    }
                                }
                            }
                            clrResourceSearch.ReleaseId = trackAdd.ReleaseId;
                            clrResourceSearch.ResourceTitle = trackAdd.ResourceTitle;
                            clrResourceSearch.VersionTitle = trackAdd.ResourceVersionTitle;
                            clrResourceSearch.SequenceNo = Convert.ToString(trackAdd.SequenceNo);
                            clrResourceSearch.Duration = trackAdd.TrackDuration;
                            clrResourceSearch.ResourceId = trackAdd.TrackId;
                            clrResourceSearch.UserName = trackAdd.UserName;
                            clrResourceAdd.Add(clrResourceSearch);
                        }
                        clrRelease.resourceDetail = clrResourceAdd;

                    }
                    clrReleaseModel.clearanceRelease = clrRelease;
                }

                LoggerFactory.LogWriter.MethodExit();

                return clrReleaseModel;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #region Upload Control

        private ActionResult UploadMasterProjectDocument(ClearanceProjectModel model, string removeFile)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                double byteArrayLength = 0;
                List<UploadDocument> listUploadDocument;

                if (Session["fileList" + "_" + model.MasterProjectDetails.ClearanceProjectID] == null)
                {
                    listUploadDocument = new List<UploadDocument>();
                }
                else
                {
                    listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + model.MasterProjectDetails.ClearanceProjectID];
                    foreach (UploadDocument uploadDocumentOne in listUploadDocument)
                    {
                        byteArrayLength += uploadDocumentOne.Data.Length;
                    }
                }

                string[] listSelectedItems = new string[10];

                if (!removeFile.Equals(string.Empty))
                {
                    listSelectedItems = removeFile.Split(',');
                }

                for (int fileCount = 0; fileCount < Request.Files.Count; fileCount++)
                {
                    if (!string.IsNullOrEmpty(Request.Files[fileCount].FileName))
                    {
                        string fileName = Request.Files[fileCount].FileName.Substring(Request.Files[fileCount].FileName.LastIndexOf("\\") + 1);
                        if (!listSelectedItems.Contains(fileName))
                        {
                            UploadDocument uploadDocument = new UploadDocument();

                            byte[] fileData = null;

                            using (var binaryReader = new BinaryReader(Request.Files[fileCount].InputStream))
                            {
                                fileData = binaryReader.ReadBytes(Request.Files[fileCount].ContentLength);
                            }
                            uploadDocument.Data = fileData;
                            uploadDocument.Name = fileName;
                            uploadDocument.Type = Request.Files[fileCount].ContentType;
                            byteArrayLength += fileData.Length;
                            if (byteArrayLength > MaxByteArrayLength)
                            {
                                ViewBag.ValidationWarningMsg = ClearanceLayout.UploadDoumentMsg;
                                model.MasterProjectDetails.listUploadDocument = new List<UploadDocument>();
                                model.MasterProjectDetails.listUploadDocument = PopulateUploadedDocs(listUploadDocument);
                                model = SetSelectedDropDown(model);
                                Session["fileList" + "_" + model.MasterProjectDetails.ClearanceProjectID] = listUploadDocument;

                                return View("CreateMasterProject", model);
                            }
                            else
                            {
                                listUploadDocument.Add(uploadDocument);
                            }
                        }
                        else
                        {
                            listSelectedItems = Array.FindAll(listSelectedItems, l => l != fileName);
                        }
                    }
                }

                Session["fileList" + "_" + model.MasterProjectDetails.ClearanceProjectID] = listUploadDocument;
                model.MasterProjectDetails.listUploadDocument = new List<UploadDocument>();
                model.MasterProjectDetails.listUploadDocument = PopulateUploadedDocs(listUploadDocument);
                model = SetSelectedDropDown(model);

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateMasterProject", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private ActionResult UploadRegualrProjectDocument(ClearanceProjectModel model, string strRemoveFile)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                double byteArrayLength = 0;

                List<UploadDocument> listUploadDocument;
                if (Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] == null)
                {
                    listUploadDocument = new List<UploadDocument>();
                }
                else
                {
                    listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId];
                    foreach (UploadDocument uploadDocumentOne in listUploadDocument)
                    {
                        byteArrayLength += uploadDocumentOne.Data.Length;
                    }
                }

                string[] listSelectedItems = new string[10];

                if (!strRemoveFile.Equals(string.Empty))
                {
                    listSelectedItems = strRemoveFile.Split(',');
                }

                for (int fileCount = 0; fileCount < Request.Files.Count; fileCount++)
                {
                    if (!string.IsNullOrEmpty(Request.Files[fileCount].FileName))
                    {
                        string fileName = Request.Files[fileCount].FileName.Substring(Request.Files[fileCount].FileName.LastIndexOf("\\") + 1);
                        if (!listSelectedItems.Contains(fileName))
                        {

                            UploadDocument uploadDocument = new UploadDocument();

                            byte[] fileData = null;

                            using (var binaryReader = new BinaryReader(Request.Files[fileCount].InputStream))
                            {
                                fileData = binaryReader.ReadBytes(Request.Files[fileCount].ContentLength);
                            }
                            uploadDocument.Data = fileData;
                            uploadDocument.Name = fileName;
                            uploadDocument.Type = Request.Files[fileCount].ContentType;
                            byteArrayLength += fileData.Length;
                            if (byteArrayLength > MaxByteArrayLength)
                            {
                                ViewBag.ValidationWarningMsg = ClearanceLayout.UploadDoumentMsg;
                                model.RegularProjectDetails.listUploadDocument = new List<UploadDocument>();
                                model.RegularProjectDetails.listUploadDocument = PopulateUploadedDocs(listUploadDocument);
                                model = SetSelectedDropDown(model);
                                Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = listUploadDocument;
                                return View("CreateRegularProject", model);
                            }
                            else
                            {
                                listUploadDocument.Add(uploadDocument);
                            }
                        }
                        else
                        {
                            listSelectedItems = Array.FindAll(listSelectedItems, l => l != fileName);
                        }
                    }
                }

                Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = listUploadDocument;
                model.RegularProjectDetails.listUploadDocument = new List<UploadDocument>();
                model.RegularProjectDetails.listUploadDocument = PopulateUploadedDocs(listUploadDocument);

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateRegularProject", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion

        private List<ArtistInfo> ParseArtistforRelease(ClearanceRelease clearanceRelease, string artistString)
        {
            LoggerFactory.LogWriter.MethodStart();
            string[] artistDetail = artistString.Split('=');
            List<ArtistInfo> listArtistInfo = new List<ArtistInfo>();
            // added for updating the display if artist name
            clearanceRelease.ArtistName = string.Empty;
            
            for (int i = 0; i < artistDetail.Length - 1; i++)
            {
                string[] strArtistDetail;
                // added for checking empty id
                if (artistDetail[i] != string.Empty)
                {
                    strArtistDetail = artistDetail[i].Split(':');
                    ArtistInfo artist = new ArtistInfo();
                    artist.Name = strArtistDetail[0];
                    artist.Id = Convert.ToInt64(strArtistDetail[2]);
                    artist.NameId = Convert.ToInt64(strArtistDetail[1]);

                    if ((clearanceRelease.ArtistName.Trim() == string.Empty) || !(string.IsNullOrEmpty(clearanceRelease.ArtistName)))
                    {
                        clearanceRelease.ArtistName = artist.Name;
                    }
                    else
                    {
                        clearanceRelease.ArtistName = string.Format("{0}, {1}", clearanceRelease.ArtistName, artist.Name);
                    }
                    artist.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                    listArtistInfo.Add(artist);
                }
            }
            LoggerFactory.LogWriter.MethodExit();
            return listArtistInfo;
        }

        private ClearanceProjectModel SetArtistDetailsForRelease(ClearanceProjectModel model, FormCollection collection)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (model.RegularProjectDetails.ObjRelease != null)
            {
                for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                {
                    if (!string.IsNullOrEmpty(collection[string.Format("{0}{1}", "hdnArtist", i.ToString())]))
                    {
                        model.RegularProjectDetails.ObjRelease[i].ArtistInfo = ParseArtistforRelease(model.RegularProjectDetails.ObjRelease[i], collection[string.Format("{0}{1}", "hdnArtist", i.ToString())]);
                    }
                }

            }

            LoggerFactory.LogWriter.MethodExit();
            return model;
        }

        // Used for Upload control functionality in Regular Project
        [HttpPost]
        public ActionResult RegularProjectPostBacks(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ViewBag.LoadTemplate = "1";

                command = model.RegularProjectDetails.Command;
                string configList = string.Empty;
                // ************START*********************Setting Next status--- Maintain MAster Project
                //To set Dropdown values of freehand Resource

                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        model.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = model.RegularProjectDetails.ObjRelease[i].ConfigId;
                    }
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);

                }
                else
                {
                    if (model.RegularProjectDetails.ReleaseNewOrExisting == "New")
                    {
                        ClearanceReleaseSearchResult releaseDetailGRSData = new ClearanceReleaseSearchResult();
                        ClearanceRelease releaseNew = new ClearanceRelease();
                        releaseNew.Archive_Flag = "N";
                        releaseNew.TrackCount = null;
                        releaseNew.No_Components = null;
                        releaseDetailGRSData.releaseDetail = new List<ClearanceRelease>();
                        releaseDetailGRSData.releaseDetail.Add(releaseNew);
                        model.RegularProjectDetails.ObjRelease = releaseDetailGRSData.releaseDetail;
                    }
                }

                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), model.RegularProjectDetails.StatusType.ToString(), true).ToString();
                // ************END*********************Setting Next status--- Maintain MAster Project

                ViewBag.DefaultTab = collection["hdnDefaultTab"];

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);
                // artist detials for NEw Release
                if (model.RegularProjectDetails.ReleaseNewOrExisting == "New")
                {
                    model = SetArtistDetailsForRelease(model, collection);
                }
                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }
                else
                {
                    configList = string.Empty;
                }

                CacheData(model, configList);

                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    model.RegularProjectDetails.ClearanceResource = RemoveResourceList(model.RegularProjectDetails.ClearanceResource);
                }
                else
                {
                    model.RegularProjectDetails.ClearanceResource = new List<ClearanceResource>();
                }
                ModelState.Clear();
                List<string> list = RegularListTooltipControl(model.RegularProjectDetails.ReleaseNewOrExisting);
                ViewBag.ControlsRegularList = list;

                if (Request.Files.Count > 0 && Request.Files[0].FileName != string.Empty)// && command == null)
                {
                    UploadRegualrProjectDocument(model, collection["hdnRemoveFile"]);
                }
                else
                {
                    if (Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] != null)
                    {
                        List<UploadDocument> listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId];
                        model.RegularProjectDetails.listUploadDocument = new List<UploadDocument>();
                        foreach (UploadDocument uploadDocument in listUploadDocument)
                        {
                            model.RegularProjectDetails.listUploadDocument.Add(new UploadDocument
                            {
                                Id = uploadDocument.Id,
                                Name = uploadDocument.Name,
                                Type = uploadDocument.Type
                            });
                        }
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateRegularProject", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                CacheData(model, "");
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                return View("CreateRegularProject", model);
            }
        }

        #endregion

        public ActionResult ManageCompany()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                string viewName = "ManageCompany";
                return PartialView(viewName, _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        public JsonResult SearchCompany(string name, string isacCode, string country, int jtStartIndex, int jtPageSize)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                bool isPaging = true;
                int totalRowCount = 0;
                var companyList = _IClearanceProjectRepository.CompanySearch(name, isacCode, country, jtStartIndex, jtPageSize, isPaging, getUserInfo());

                if (companyList.Count > 0)
                {
                    totalRowCount = companyList[0].PageDetails.TotalRows;
                    _IClearanceProjectModel.TotalRows = companyList[0].PageDetails.TotalRows;
                    ClearanceProjectModel.TotalRowsCount = companyList[0].PageDetails.TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = "OK", Records = companyList.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // for copying details for New Master Project
        //Change regular constant type
        public ActionResult CopyProjectDetails()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (Request.Form["SelectedProjectType"] == General.ProjectType.Regular.ToString())
                    return CopyRegularProject(Request.Form["SelectedProjectId"]);
                else
                    return CopyMasterProjectDetails(Request.Form["SelectedProjectId"]);
               
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // Added for copying details for New Regular Project
        //Set ID AS NULL OR 0 for resource
        public ActionResult CopyRegularProject(string clrProjectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel _IClearanceProjectModel = new ClearanceProjectModel();
                //To set Dropdown values of freehand Resource
                _type = new List<string>();
                _type.Add(Constants.ClearanceMusicType);//pass type as constants
                _type.Add(Constants.ClearanceResourceType);
                _type.Add(Constants.ClearanceRecordingType);
                _clearanceResourceModel = _IClearanceProjectRepository.GetMasterDataResource(_type, getUserInfo());
                _type.Clear();
                _IClearanceProjectModel.RecordingTypeResourceTab = _clearanceResourceModel.RecordingType;
                _clearanceResourceModel.ResourceType = _clearanceResourceModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
                _IClearanceProjectModel.ResourceTypeResourceTab = _clearanceResourceModel.ResourceType;
                var MusicType = _clearanceResourceModel.MusicType.Where(x => x.Text.ToUpper() != "JAZZ").ToList();
                _clearanceResourceModel.MusicType = MusicType;
                _IClearanceProjectModel.MusicTypeResourceTab = _clearanceResourceModel.MusicType;
                //End-To set Dropdown values of freehand Resource
                _IClearanceProjectModel.RegularProjectDetails = _IClearanceProjectRepository.GetRegularProjectDetails(Convert.ToInt32(clrProjectId), getUserInfo());
                _IClearanceProjectModel.RegularProjectDetails.ProjectReferenceId = string.Empty;
                _IClearanceProjectModel.RegularProjectDetails.ClrProjectId = 0;
                _IClearanceProjectModel.RegularProjectDetails.R2_Project_Code = string.Empty;
                _IClearanceProjectModel.RegularProjectDetails.R2_Project_Id = 0;
                _IClearanceProjectModel.RegularProjectDetails.listUploadDocument.ForEach(rr => rr.Id = 0);
                _IClearanceProjectModel.RegularProjectDetails.ClearanceResource.RemoveAll(r => r.R2_ResourceId == 0);

                CacheData(_IClearanceProjectModel, string.Empty);
                if (string.IsNullOrEmpty(_IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting))
                    _IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting = "Exist";

                _IClearanceProjectModel.RegularProjectDetails.StatusTypeDesc = General.StatusType.Unsubmitted.ToString();
                _IClearanceProjectModel.RegularProjectDetails.StatusType = (int)General.StatusType.Unsubmitted;

                var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                Territories.Add(1, _IClearanceProjectModel.RegularProjectDetails.Territories);
                Territories.Add(2, _IClearanceProjectModel.RegularProjectDetails.ScopeAndRequestType.Territories);
                int tempI = 2;
                if (_IClearanceProjectModel.RegularProjectDetails.ClearanceResource != null)
                {
                    foreach (ClearanceResource cls in _IClearanceProjectModel.RegularProjectDetails.ClearanceResource)
                    {
                        Territories.Add(++tempI, cls.TerritorialRights);
                    }
                }

                ViewBag.ProjectTerritories = Territories;
                ViewBag.LoadTemplate = "1";
                if (_IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting == null)
                    _IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting = "New";

                if (_IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting == "Exist")
                {
                    _IClearanceProjectModel.RegularProjectDetails.ClearanceResource = new List<ClearanceResource>();
                    _IClearanceProjectModel.RegularProjectDetails.ObjRelease = new List<ClearanceRelease>();
                }
                else if (_IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting == "New")
                {

                    ClearanceReleaseSearchResult releaseDetailGRSData = new ClearanceReleaseSearchResult();
                    ClearanceRelease releaseNew = new ClearanceRelease();
                    releaseNew.Archive_Flag = "N";
                    releaseNew.IsNewlyAddedAfterSubmit = true;
                    releaseDetailGRSData.releaseDetail = new List<ClearanceRelease>();
                    releaseDetailGRSData.releaseDetail.Add(releaseNew);
                    _IClearanceProjectModel.RegularProjectDetails.ObjRelease = releaseDetailGRSData.releaseDetail;
                    _IClearanceProjectModel.RegularProjectDetails.ClearanceResource = new List<ClearanceResource>();
                }
                _IClearanceProjectModel.RegularProjectDetails.CreatedBy = SessionWrapper.CurrentUserInfo.UserName;

                _IClearanceProjectModel.RegularProjectDetails.CreatedDate = DateTime.Now.ToString(@ClearanceLayout.RegularProjectCreateDate);
                _IClearanceProjectModel.RegularProjectDetails.CreatedByUser = SessionWrapper.CurrentUserInfo.UserName;

                if (_IClearanceProjectModel.RegularProjectDetails.ObjRelease != null)
                {
                    for (int i = 0; i < _IClearanceProjectModel.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        _IClearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(_IClearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();

                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateRegularProject", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        // Added for copying details for New Master Project
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CopyMasterProjectDetails(string clrProjectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                int freeHandCount = 0;
                LoggerFactory.LogWriter.Info(string.Format("SelectProjectId: {0}", clrProjectId));
                ClearanceProjectModel clearancemodel = new ClearanceProjectModel();
                _IClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(SessionWrapper.CurrentUserInfo.UserLoginName);

                //To set Dropdown values of freehand Resource
                _type = new List<string>();
                _type.Add(Constants.ClearanceMusicType);//pass type as constants
                _type.Add(Constants.ClearanceResourceType);
                _type.Add(Constants.ClearanceRecordingType);
                _clearanceResourceModel = _IClearanceProjectRepository.GetMasterDataResource(_type, getUserInfo());
                _type.Clear();
                clearancemodel.RecordingTypeResourceTab = _clearanceResourceModel.RecordingType;
                _clearanceResourceModel.ResourceType = _clearanceResourceModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
                clearancemodel.ResourceTypeResourceTab = _clearanceResourceModel.ResourceType;
                var MusicType = _clearanceResourceModel.MusicType.Where(x => x.Text.ToUpper() != "JAZZ").ToList();
                _clearanceResourceModel.MusicType = MusicType;
                clearancemodel.MusicTypeResourceTab = _clearanceResourceModel.MusicType;
                //End-To set Dropdown values of freehand Resource

                clearancemodel.MasterProjectDetails = _IClearanceProjectRepository.GetMasterProjectDetails(Convert.ToInt32(clrProjectId), getUserInfo());
                clearancemodel.MasterProjectDetails.CreatedBy = SessionWrapper.CurrentUserInfo.UserName;
                clearancemodel.MasterProjectDetails.ProjectReferenceId = string.Empty;
                clearancemodel.MasterProjectDetails.CreatedDate = DateTime.Now.ToString(@ClearanceLayout.RegularProjectCreateDate);
                clearancemodel.MasterProjectDetails.CreatedByUser = SessionWrapper.CurrentUserInfo.UserName;
                //set null id as 0 and keep the data in session
                clearancemodel.MasterProjectDetails.listUploadDocument.ForEach(rr => rr.Id = 0);

                foreach (var resource in clearancemodel.MasterProjectDetails.ClearanceResource)
                {
                    // for all recording Types
                    if (resource.LiveStudioType == ((int)General.LiveStudioType.Live).ToString())
                        resource.RecordingTypeDesc = (General.LiveStudioType.Live).ToString();
                    else if (resource.LiveStudioType == ((int)General.LiveStudioType.Studio).ToString())
                        resource.RecordingTypeDesc = General.LiveStudioType.Studio.ToString();

                    // for all resource type
                    if (resource.ResourceType == ((int)General.ResourceType.Audio).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Audio.ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Image).ToString())
                        resource.ResourceTypeDesc = (General.ResourceType.Image).ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Merchandise).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Merchandise.ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Other).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Other.ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Text).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Text.ToString();

                    // for all music type
                    if (resource.MusicClassType == ((int)General.MusicClassType.Classical).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Classical.ToString();
                    else if (resource.MusicClassType == ((int)General.MusicClassType.Jazz).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Jazz.ToString();
                    else if (resource.MusicClassType == ((int)General.MusicClassType.Pop).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Pop.ToString();
                    else if (resource.MusicClassType == ((int)General.MusicClassType.Other).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Other.ToString();
                    //for COPY functionality , clearance resource should be 0
                    resource.ClearanceResourceId = 0;

                    if (resource.SuggestedFee != null)
                    {
                        decimal suggestfee;
                        suggestfee = resource.SuggestedFee.GetValueOrDefault();
                        resource.SuggestedFee = Convert.ToDecimal(suggestfee.ToString("F02", CultureInfo.InvariantCulture));

                    }
                    resource.IsNewlyAddedAfterSubmit = true;
                }

                // For Copy Functionality, Freehand resources are not copied
                for (int i = 0; i < clearancemodel.MasterProjectDetails.ClearanceResource.Count; i++)
                {
                    //check for Cancelled resource
                    var IsCancelled = clearancemodel.MasterProjectDetails.RequestInfoList.Where(s => s.ApprovalStatus == General.StatusType.Cancelled.ToString() && s.ResourceId == clearancemodel.MasterProjectDetails.ClearanceResource[i].ResourceId).Select(s => s.ResourceId).SingleOrDefault();
                    if ((clearancemodel.MasterProjectDetails.ClearanceResource[i].R2_ResourceId == 0) || (IsCancelled > 0))
                    {
                        if (clearancemodel.MasterProjectDetails.ClearanceResource[i].R2_ResourceId == 0) //count only Freehand Resource
                            freeHandCount = freeHandCount + 1;

                        clearancemodel.MasterProjectDetails.ClearanceResource.RemoveAt(i);
                        i = i - 1;
                    }
                }

                clearancemodel.MasterProjectDetails.StatusTypeDesc = General.StatusType.Unsubmitted.ToString();
                clearancemodel.MasterProjectDetails.StatusType = (int)General.StatusType.Unsubmitted;

                clearancemodel.CurrencyList = _IClearanceProjectModel.CurrencyList;
                clearancemodel.CompanyList = _IClearanceProjectModel.CompanyList;

                var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                Territories.Add(1, clearancemodel.MasterProjectDetails.Territories);
                int rowId = 2;

                foreach (ClearanceResource cls in clearancemodel.MasterProjectDetails.ClearanceResource)
                {
                    Territories.Add(++rowId, cls.TerritorialRights);
                }

                ViewBag.ProjectTerritories = Territories;

                if (freeHandCount > 0)
                {
                    ViewBag.ValidationMsg = "" + freeHandCount + " " + ClearanceLayout.msgNotCopyFreeHand;
                }

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateMasterProject", clearancemodel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // for copying details for New Master Project
        public ActionResult GetProjectDetail(string projectReferenceId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ClearanceProjectModel clearancemodel = new ClearanceProjectModel();
                return View("CreateMasterProject", GetDetail(clearancemodel, projectReferenceId));
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private ClearanceProjectModel GetDetail(ClearanceProjectModel clearancemodel, string projectReferenceId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Info(string.Format("projectReferenceId: {0}", projectReferenceId));

                _IClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(SessionWrapper.CurrentUserInfo.UserLoginName);

                clearancemodel.MasterProjectDetails = _IClearanceProjectRepository.GetMasterProjectDetails(Convert.ToInt32(projectReferenceId), getUserInfo());
                clearancemodel.MasterProjectDetails.CreatedBy = SessionWrapper.CurrentUserInfo.UserLoginName;

                clearancemodel.MasterProjectDetails.CreatedDate = DateTime.Now.ToShortDateString();
                clearancemodel.MasterProjectDetails.CreatedByUser = SessionWrapper.CurrentUserInfo.UserLoginName;

                foreach (var resource in clearancemodel.MasterProjectDetails.ClearanceResource)
                {
                    // for all recording Types
                    if (resource.LiveStudioType == ((int)General.LiveStudioType.Live).ToString())
                        resource.RecordingTypeDesc = (General.LiveStudioType.Live).ToString();
                    else if (resource.LiveStudioType == ((int)General.LiveStudioType.Studio).ToString())
                        resource.RecordingTypeDesc = General.LiveStudioType.Studio.ToString();

                    // for all resource type
                    if (resource.ResourceType == ((int)General.ResourceType.Audio).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Audio.ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Image).ToString())
                        resource.ResourceTypeDesc = (General.ResourceType.Image).ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Merchandise).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Merchandise.ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Other).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Other.ToString();
                    else if (resource.ResourceType == ((int)General.ResourceType.Text).ToString())
                        resource.ResourceTypeDesc = General.ResourceType.Text.ToString();

                    // for all music type
                    if (resource.MusicClassType == ((int)General.MusicClassType.Classical).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Classical.ToString();
                    else if (resource.MusicClassType == ((int)General.MusicClassType.Jazz).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Jazz.ToString();
                    else if (resource.MusicClassType == ((int)General.MusicClassType.Pop).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Pop.ToString();
                    else if (resource.MusicClassType == ((int)General.MusicClassType.Other).ToString())
                        resource.MusicTypeDesc = General.MusicClassType.Other.ToString();

                }

                clearancemodel.CurrencyList = _IClearanceProjectModel.CurrencyList;
                clearancemodel.CompanyList = _IClearanceProjectModel.CompanyList;
                if (clearancemodel.MasterProjectDetails.StatusType == (int)(General.StatusType.Cancelled) || clearancemodel.MasterProjectDetails.StatusType == (int)(General.StatusType.Completed))
                {
                    clearancemodel.ReadOnlyMode = 1;
                }

                LoggerFactory.LogWriter.MethodExit();

                return clearancemodel;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public RequestTypeRegular SaveRequestType(ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProject regularProject = new MasterProject();
                RequestTypeRegular requestTypeRegular = new RequestTypeRegular();
                TVRadioBreakICLABase tvRadioBreak = new TVRadioBreakICLABase();
                TV tv = new TV();
                Radio radio = new Radio();
                OthersICLA otherICLA = new OthersICLA();

                if (Request.Form.Count > 0)
                {
                    requestTypeRegular.Territories = model.RegularProjectDetails.ScopeAndRequestType.Territories;
                    requestTypeRegular.ExcludedTerritories = model.RegularProjectDetails.ScopeAndRequestType.ExcludedTerritories;
                    requestTypeRegular.IncludedTerritories = model.RegularProjectDetails.ScopeAndRequestType.IncludedTerritories;
                    requestTypeRegular.Physical = model.RegularProjectDetails.ScopeAndRequestType.Physical;
                    requestTypeRegular.Digital = model.RegularProjectDetails.ScopeAndRequestType.Digital;

                    requestTypeRegular.RegularRetail = model.RegularProjectDetails.ScopeAndRequestType.RegularRetail;
                    requestTypeRegular.TVRadioBreakICLA = model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA;
                    requestTypeRegular.PriceReduction = model.RegularProjectDetails.ScopeAndRequestType.PriceReduction;

                    requestTypeRegular.Club = model.RegularProjectDetails.ScopeAndRequestType.Club;
                    requestTypeRegular.NonTraditional = model.RegularProjectDetails.ScopeAndRequestType.NonTraditional;
                    requestTypeRegular.Promotional = model.RegularProjectDetails.ScopeAndRequestType.Promotional;

                    if (!string.IsNullOrEmpty(model.RegularProjectDetails.ScopeAndRequestType.DurationFrom))
                        requestTypeRegular.DurationFrom = model.RegularProjectDetails.ScopeAndRequestType.DurationFrom;

                    if (!string.IsNullOrEmpty(model.RegularProjectDetails.ScopeAndRequestType.DurationTo))
                        requestTypeRegular.DurationTo = model.RegularProjectDetails.ScopeAndRequestType.DurationTo;

                    tv.IsTV = model.RegularProjectDetails.ScopeAndRequestType.TV.IsTV;


                    if (model.RegularProjectDetails.ScopeAndRequestType.TV.Budget.ToString() != string.Empty)
                        tv.Budget = model.RegularProjectDetails.ScopeAndRequestType.TV.Budget;

                    if (model.RegularProjectDetails.ScopeAndRequestType.TV.BudgetInUSD.ToString() != string.Empty)
                        tv.BudgetInUSD = model.RegularProjectDetails.ScopeAndRequestType.TV.BudgetInUSD;

                    if (model.RegularProjectDetails.ScopeAndRequestType.TV.ProductionCostOfCommercial.ToString() != string.Empty)
                        tv.ProductionCostOfCommercial = model.RegularProjectDetails.ScopeAndRequestType.TV.ProductionCostOfCommercial;

                    radio.IsRadio = model.RegularProjectDetails.ScopeAndRequestType.Radio.IsRadio;

                    if (model.RegularProjectDetails.ScopeAndRequestType.Radio.Budget.ToString() != string.Empty)
                        radio.Budget = model.RegularProjectDetails.ScopeAndRequestType.Radio.Budget;

                    if (model.RegularProjectDetails.ScopeAndRequestType.Radio.BudgetInUSD.ToString() != string.Empty)
                        radio.BudgetInUSD = model.RegularProjectDetails.ScopeAndRequestType.Radio.BudgetInUSD;

                    if (model.RegularProjectDetails.ScopeAndRequestType.Radio.ProductionCostOfCommercial.ToString() != string.Empty)
                        radio.ProductionCostOfCommercial = model.RegularProjectDetails.ScopeAndRequestType.Radio.ProductionCostOfCommercial;

                    otherICLA.IsOthers = model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.IsOthers;

                    if (model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.Budget.ToString() != string.Empty)
                        otherICLA.Budget = model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.Budget;

                    if (model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.BudgetInUSD.ToString() != string.Empty)
                        otherICLA.BudgetInUSD = model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.BudgetInUSD;

                    if (model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.ProductionCostOfCommercial.ToString() != string.Empty)
                        otherICLA.ProductionCostOfCommercial = model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.ProductionCostOfCommercial;

                    if (!string.IsNullOrEmpty(model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.OtherMediaDetails))
                        otherICLA.OtherMediaDetails = model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.OtherMediaDetails;

                    requestTypeRegular.SalesChannelPhysical = model.RegularProjectDetails.ScopeAndRequestType.SalesChannelPhysical;

                    requestTypeRegular.SalesChannelAlaCarteDownload = model.RegularProjectDetails.ScopeAndRequestType.SalesChannelAlaCarteDownload;

                    requestTypeRegular.SalesChannelSubscriptionDownload = model.RegularProjectDetails.ScopeAndRequestType.SalesChannelSubscriptionDownload;

                    requestTypeRegular.SalesChannelMobileRealTones = model.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRealTones;

                    requestTypeRegular.SalesChannelMobileRingBackTones = model.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRingBackTones;

                    requestTypeRegular.Streaming = model.RegularProjectDetails.ScopeAndRequestType.Streaming;

                    if (model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesToDate.ToString() != string.Empty)
                        requestTypeRegular.PhysicalSalesSplitSalesToDate = model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesToDate;

                    if (model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWith.ToString() != string.Empty)
                        requestTypeRegular.PhysicalSalesSplitSalesWith = model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWith;

                    if (model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWithout.ToString() != string.Empty)
                        requestTypeRegular.PhysicalSalesSplitSalesWithout = model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWithout;

                    if (model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesToDate.ToString() != string.Empty)
                        requestTypeRegular.DigitalSalesSplitSalesToDate = model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesToDate;

                    if (model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWith.ToString() != string.Empty)
                        requestTypeRegular.DigitalSalesSplitSalesWith = model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWith;

                    if (model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWithout.ToString() != string.Empty)
                        requestTypeRegular.DigitalSalesSplitSalesWithout = model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWithout;

                    if (model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueToDate.ToString() != string.Empty)
                        requestTypeRegular.PhysicalRevenueToDate = model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueToDate;

                    if (model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWith.ToString() != string.Empty)
                        requestTypeRegular.PhysicalRevenueWith = model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWith;

                    if (model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWithout.ToString() != string.Empty)
                        requestTypeRegular.PhysicalRevenueWithout = model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWithout;

                    if (model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueToDate.ToString() != string.Empty)
                        requestTypeRegular.DigitalRevenueToDate = model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueToDate;

                    if (model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWith.ToString() != string.Empty)
                        requestTypeRegular.DigitalRevenueWith = model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWith;

                    if (model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWithout.ToString() != string.Empty)
                        requestTypeRegular.DigitalRevenueWithout = model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWithout;

                    if ((model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesToDate.ToString() != string.Empty) && (model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesToDate.ToString() != string.Empty))
                        requestTypeRegular.TotalSalesSplitSalesToDate = model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesToDate + model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesToDate;

                    if ((model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWith.ToString() != string.Empty) && (model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWith.ToString() != string.Empty))
                        requestTypeRegular.TotalSalesSplitSalesWith = model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWith + model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWith;

                    if ((model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWithout.ToString() != string.Empty) && (model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWithout.ToString() != string.Empty))
                        requestTypeRegular.TotalSalesSplitSalesWithout = model.RegularProjectDetails.ScopeAndRequestType.PhysicalSalesSplitSalesWithout + model.RegularProjectDetails.ScopeAndRequestType.DigitalSalesSplitSalesWithout;

                    if ((model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueToDate.ToString() != string.Empty) && (model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueToDate.ToString() != string.Empty))
                        requestTypeRegular.TotalRevenueToDate = model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueToDate + model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueToDate;

                    if ((model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWith.ToString() != string.Empty) && (model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWith.ToString() != string.Empty))
                        requestTypeRegular.TotalRevenueWith = model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWith + model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWith;

                    if ((model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWithout.ToString() != string.Empty) && (model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWithout.ToString() != string.Empty))
                        requestTypeRegular.TotalRevenueWithout = model.RegularProjectDetails.ScopeAndRequestType.PhysicalRevenueWithout + model.RegularProjectDetails.ScopeAndRequestType.DigitalRevenueWithout;


                    if (model.CurrPriceLevelList != null)
                        if (model.RequestedPriceLevelList.ToString() != string.Empty)
                            requestTypeRegular.AdditionalMailOrder = model.RegularProjectDetails.ScopeAndRequestType.AdditionalMailOrder;

                    requestTypeRegular.IntroductoryUse = model.RegularProjectDetails.ScopeAndRequestType.IntroductoryUse;
                    requestTypeRegular.DistributionTo = model.RegularProjectDetails.ScopeAndRequestType.DistributionTo;
                    requestTypeRegular.ClientName = model.RegularProjectDetails.ScopeAndRequestType.ClientName;
                    requestTypeRegular.ClientWebsite = model.RegularProjectDetails.ScopeAndRequestType.ClientWebsite;
                    requestTypeRegular.MediaPromoSpendComment = model.RegularProjectDetails.ScopeAndRequestType.MediaPromoSpendComment;

                    if (!string.IsNullOrEmpty(model.RegularProjectDetails.ScopeAndRequestType.ManufacturedByUMG))
                        requestTypeRegular.ManufacturedByUMG = model.RegularProjectDetails.ScopeAndRequestType.ManufacturedByUMG;

                    requestTypeRegular.Partwork = model.RegularProjectDetails.ScopeAndRequestType.Partwork;
                    requestTypeRegular.Kiosk = model.RegularProjectDetails.ScopeAndRequestType.Kiosk;
                    requestTypeRegular.MailOrder = model.RegularProjectDetails.ScopeAndRequestType.MailOrder;
                    requestTypeRegular.Internet = model.RegularProjectDetails.ScopeAndRequestType.Internet;
                    requestTypeRegular.DirectResponse = model.RegularProjectDetails.ScopeAndRequestType.DirectResponse;
                    requestTypeRegular.Educational = model.RegularProjectDetails.ScopeAndRequestType.Educational;

                    requestTypeRegular.Premium = model.RegularProjectDetails.ScopeAndRequestType.Premium;
                    requestTypeRegular.PremiumComments = model.RegularProjectDetails.ScopeAndRequestType.PremiumComments;
                    requestTypeRegular.GiveAwayFreeCharge = model.RegularProjectDetails.ScopeAndRequestType.GiveAwayFreeCharge;
                    requestTypeRegular.GiveAwayComments = model.RegularProjectDetails.ScopeAndRequestType.GiveAwayComments;

                    requestTypeRegular.Other = model.RegularProjectDetails.ScopeAndRequestType.Other;
                    requestTypeRegular.OtherComments = model.RegularProjectDetails.ScopeAndRequestType.OtherComments;
                    requestTypeRegular.CurrentPriceLevel_ID = model.RegularProjectDetails.ScopeAndRequestType.CurrentPriceLevel_ID;
                    requestTypeRegular.RequestedPriceLevel_ID = model.RegularProjectDetails.ScopeAndRequestType.RequestedPriceLevel_ID;

                    requestTypeRegular.TV = tv;
                    requestTypeRegular.Radio = radio;
                    requestTypeRegular.OthersICLA = otherICLA;
                    requestTypeRegular.newlyAddedSalesChannelsAfterSubmit = model.RegularProjectDetails.ScopeAndRequestType.newlyAddedSalesChannelsAfterSubmit;

                }

                LoggerFactory.LogWriter.MethodExit();

                return requestTypeRegular;
            }
            catch (Exception ex)
            {

                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Created By : Dinesh
        /// Description : Used In Project Inquiry Page to open the POPup in read only mode.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult OpenClearanceProjectInReadOnly(ClearanceProjectInquirySearchCriteria input)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                int ProjectId = input.ProjectId;
                int ProjectTypeId = input.ProjectTypeId;
                if (ProjectTypeId == Convert.ToInt32(General.ProjectType.Master))//Master Project
                {
                    LoggerFactory.LogWriter.Info("Get Master Data Method initiated");

                    _IClearanceProjectModel.MasterProjectDetails = _IClearanceProjectRepository.GetMasterProjectDetails(ProjectId, getUserInfo());
                    _IClearanceProjectModel.ReadOnlyMode = 1;
                    _IClearanceProjectModel.isInMaintainMode = 1;
                    var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                    Territories.Add(1, _IClearanceProjectModel.MasterProjectDetails.Territories);
                    int i = 2;

                    if (_IClearanceProjectModel.MasterProjectDetails.ClearanceResource != null)
                    {
                        decimal suggestfee;

                        foreach (ClearanceResource cls in _IClearanceProjectModel.MasterProjectDetails.ClearanceResource)
                        {
                            Territories.Add(++i, cls.TerritorialRights);
                            suggestfee = cls.SuggestedFee.GetValueOrDefault();
                            cls.SuggestedFee = Convert.ToDecimal(suggestfee.ToString("F02", CultureInfo.InvariantCulture));
                        }
                    }

                    ViewBag.ProjectTerritories = Territories;
                    ViewBag.RoleGroup = input.RoleGroup;

                    List<string> list = MasterListTooltipControl();
                    ViewBag.ControlsList = list;
                    _IClearanceProjectModel.roleGroupName = input.RoleGroup;
                    _IClearanceProjectModel.RccHandler = BindRCCHandlerDropdown(RoleType.RCCAdmin);
                                        
                    LoggerFactory.LogWriter.MethodExit();

                    return View("CreateMasterProject", _IClearanceProjectModel);
                }
                else //Regular Project
                {
                    LoggerFactory.LogWriter.Info("Get Regular Data Method initiated");

                    _IClearanceProjectModel.RegularProjectDetails = _IClearanceProjectRepository.GetRegularProjectDetails(ProjectId, getUserInfo());
                    _IClearanceProjectModel.ReadOnlyMode = 1;
                    _IClearanceProjectModel.isInMaintainMode = 1;
                    
                    var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                    Territories.Add(1, _IClearanceProjectModel.RegularProjectDetails.Territories);
                    Territories.Add(2, _IClearanceProjectModel.RegularProjectDetails.ScopeAndRequestType.Territories);
                    int tempI = 2;

                    if (_IClearanceProjectModel.RegularProjectDetails.ClearanceResource != null)
                    {
                        decimal suggestfee;

                        foreach (ClearanceResource cls in _IClearanceProjectModel.RegularProjectDetails.ClearanceResource)
                        {
                            Territories.Add(++tempI, cls.TerritorialRights);
                            suggestfee = cls.SuggestedFee.GetValueOrDefault();
                            cls.SuggestedFee = Convert.ToDecimal(suggestfee.ToString("F02", CultureInfo.InvariantCulture));
                        }
                    }

                    ViewBag.LoadTemplate = "1";
                    ViewBag.DefaultTab = 0;
                    ViewBag.ProjectTerritories = Territories;
                    ViewBag.RoleGroup = input.RoleGroup;

                    _IClearanceProjectModel.IsProjectBlocked = input.IsProjectBlocked;

                    List<string> list = RegularListTooltipControl(_IClearanceProjectModel.RegularProjectDetails.ReleaseNewOrExisting);
                    ViewBag.ControlsRegularList = list;

                    _IClearanceProjectModel.roleGroupName = input.RoleGroup;
                    _IClearanceProjectModel.RccHandler = BindRCCHandlerDropdown(RoleType.RCCAdmin);

                    LoggerFactory.LogWriter.MethodExit();

                    return View("CreateRegularProject", _IClearanceProjectModel);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        public List<ListItem> BindRCCHandlerDropdown(RoleType roleType)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<ListItem> listRCCHandler = new List<ListItem>();
                listRCCHandler = _IClearanceProjectRepository.GetRCCHandlerList(roleType, getUserInfo());
                LoggerFactory.LogWriter.MethodExit();
                return listRCCHandler;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult ProjectSearchUserTransfer()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                _IClearanceProjectModel = _IClearanceProjectRepository.FillProjectSearchDropDown(getUserInfo());
                LoggerFactory.LogWriter.MethodExit();
                return View("ProjectSearchUserTransfer", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult ProjectOwnershipTransfer(ResourceSearch Input)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                _IClearenceUserTransferModel = new ClearenceUserTransferModel();
                _IClearenceUserTransferModel = _IClearanceProjectRepository.FillWorkGroupUserDropDown(getUserInfo(), Input.PageNumber);
                //split the input value
                string[] arrProjlist;
                if (!string.IsNullOrEmpty(Input.Isrc))
                {
                    arrProjlist = Input.Isrc.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


                List<ClearanceProject> listAdd = new List<ClearanceProject>();
                for (int i = 0; i < arrProjlist.Count(); i++)
                {
                    string[] listUseriD = arrProjlist[i].Split(',');
                    // check the project user id is not null or empty
                    if (!string.IsNullOrEmpty(listUseriD[0]))
                    {
                        listAdd.Add(new ClearanceProject()
                        {
                            ClrProjectId = long.Parse(listUseriD[0]),
                            ProjectTitle = listUseriD[1],
                            ProjectReferenceId = listUseriD[2],
                            CreatedUserName = listUseriD[3]
                        }
                            );
                    }

                }

                _IClearenceUserTransferModel.listClearanceProjectTransfer = listAdd;

                LoggerFactory.LogWriter.MethodExit();

                return View("ProjectOwnershipTransfer", _IClearenceUserTransferModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult TransferUser(ResourceSearch Input)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("NewOwnderId: {0}", Input.ProjectId));

                int NewOwnerId = int.Parse(Input.ProjectId.ToString());

                //split the selected projects
                string[] arrProjlist = Input.Isrc.Split(',');
                bool result = _IClearanceProjectRepository.TransferUser(arrProjlist, NewOwnerId, getUserInfo());

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Error", ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ProjectSearchForUserTransfer(ClearanceProjectSearchCriteria projectSearchCriteria)
        {

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                int totalRowCount = 0;
                int[] intarrList = new int[3];

                intarrList[0] = Convert.ToInt32(General.StatusType.Submitted);
                intarrList[1] = Convert.ToInt32(General.StatusType.ReSubmitted);
                intarrList[2] = Convert.ToInt32(General.StatusType.ReOpened);

                ProjectSearchResult projectList = _IClearanceProjectRepository.SearchProjectForUserTransfer(projectSearchCriteria, intarrList, getUserInfo());
                if (projectList != null)
                {
                    totalRowCount = projectList.TotalRows;
                }

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = projectList.Values.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Error", ex.Message });
            }
        }

        /// <summary>
        /// Fill config list on on the basis of selected configuration group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetConfigList(ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                IEnumerable<ListItem> ConfigList = _IClearanceReleaseRepository.GetReleaseConfigList(model.ConfigGroupName, getUserInfo()).ConfigList;
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { success = true, Records = ConfigList });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "cancel", ex.Message });
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for ReSubmit Regular Project By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReSubmitRegularProject(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string IsExistingOrNew = string.Empty;
                string configList = string.Empty;
                string projectReferenceId = string.Empty;

                //For updating multi artist flag
                if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                {
                    if (collection.GetValues("hdnMultiartist")[0] != string.Empty)
                    {
                        model.RegularProjectDetails.MultiArtist = Convert.ToBoolean(collection.GetValues("hdnMultiartist")[0]);
                    }
                }

                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }

                CacheData(model, configList);

                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                }

                ViewBag.DefaultTab = collection["hdnDefaultTab"];

                if (collection["hdnAdditionalResourceCheck"] != null)
                {
                    if (collection["hdnAdditionalResourceCheck"].ToString().Equals("NO"))
                        TempData["DefaultTabCheck"] = ViewBag.DefaultTab;
                }

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);

                model = SetArtistDetailsForResourceRegular(model, collection);

                if (Request.Form.Count > 0)
                {
                    if (model.RegularProjectDetails.ReleaseNewOrExisting != null)
                    {
                        IsExistingOrNew = model.RegularProjectDetails.ReleaseNewOrExisting;
                    }
                    //**** if new release is saving, get artist details  from hidden field
                    if (IsExistingOrNew.Equals(Constants.ReleaseType.New))
                    {
                        model = SetArtistDetailsForRelease(model, collection);
                        // done for mainting the configuration dropdown in case of new release
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = model.RegularProjectDetails.ObjRelease[i].ConfigId;
                        }
                        model.RegularProjectDetails.IsExisting = false;
                    }
                    else
                    {
                        model.RegularProjectDetails.IsExisting = true;
                    }


                    SetRegularProjectEntities(command, model);
                    if (IsExistingOrNew.Equals(Constants.ReleaseType.New))
                    {
                        GetPackageInfoForReleaseNew(model, collection);
                    }
                    if (collection["hdnPackageRoutingCheck"] != null)
                    {
                        if (collection["hdnPackageRoutingCheck"].ToString().Equals("NO"))
                        {
                            model.RegularProjectDetails.ObjRelease.ToList().ForEach(i => i.PackageInfo.ToList().All(j => j.IsNewlyAddedAfterSubmit = false));
                        }
                    }

                    // save request type detail
                    model.RegularProjectDetails.ScopeAndRequestType = SaveRequestType(model);
                    model.RegularProjectDetails = _IClearanceProjectRepository.SaveRegularProjectDetails(model.RegularProjectDetails, getUserInfo());

                    projectReferenceId = model.RegularProjectDetails.ProjectReferenceId;

                    if (model.RegularProjectDetails.StatusType == (int)General.StatusType.Submitted)
                    {
                        model.RegularProjectDetails.StatusTypeDesc = General.StatusType.Submitted.ToString();
                    }
                    else if (model.RegularProjectDetails.StatusType == (int)General.StatusType.ReSubmitted)
                    {
                        model.RegularProjectDetails.StatusTypeDesc = General.StatusType.ReSubmitted.ToString();

                        if (model.RegularProjectDetails.IsSensitiveDataChanged)
                            model.RegularProjectDetails.IsSensitiveDataChanged = false;
                    }

                    ModelState.Clear();
                    if (model.RegularProjectDetails.ClearanceResource != null)
                    {

                        model.RegularProjectDetails.ClearanceResource = model.RegularProjectDetails.ClearanceResource.OrderBy(Resourcelist => Resourcelist.ArtistName).ThenBy(Resourcelist => Resourcelist.Title).ThenBy(Resourcelist => Resourcelist.VersionTitle).ToList();
                        var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                        Territories.Add(1, model.RegularProjectDetails.Territories);
                        Territories.Add(2, model.RegularProjectDetails.ScopeAndRequestType.Territories);
                        int tempI = 2;
                        foreach (ClearanceResource cls in model.RegularProjectDetails.ClearanceResource)
                        {
                            Territories.Add(++tempI, cls.TerritorialRights);
                        }

                        ViewBag.ProjectTerritories = Territories;
                        model.RegularProjectDetails.ClearanceResource.Where(i =>
                            i.IsNewlyAddedAfterSubmit == true).ToList().All(i =>
                            {
                                i.IsNewlyAddedAfterSubmit = false;
                                i.IsRouted = true;
                                return true;
                            });

                        model.RegularProjectDetails.ClearanceResource.Where(i =>
                            i.IsRouted == false || i.IsRouted == null).ToList().All(i =>
                            {
                                i.IsNewlyAddedAfterSubmit = true;
                                i.IsRouted = false;
                                return true;
                            });

                    }

                    if (model.RegularProjectDetails.ObjRelease != null)
                    {
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(model.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();

                        }

                        model.RegularProjectDetails.ObjRelease.Where(i =>
                           i.IsNewlyAddedAfterSubmit == true).ToList().All(i =>
                           {
                               i.IsNewlyAddedAfterSubmit = false;
                               i.IsRouted = true;
                               return true;
                           });

                        model.RegularProjectDetails.ObjRelease.Where(i =>
                            i.IsRouted == false || i.IsRouted == null).ToList().All(i =>
                            {
                                i.IsNewlyAddedAfterSubmit = true;
                                i.IsRouted = false;
                                return true;
                            });
                    }
                }

                ViewBag.LoadTemplate = "1";
                LoggerFactory.LogWriter.Info("Successfully Called Resubmitted Regular Project");
                LoggerFactory.LogWriter.Debug("Successfully Called Resubmitted Regular Project");

                TempData["EntityForSubmtProject" + "_" + model.RegularProjectDetails.ClrProjectId] = model;

                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    List<ClearanceInboxRequest> requestList;
                    byte routingStatus;
                    var serializer = new JavaScriptSerializer();
                    requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.RegularProjectDetails.ClrProjectId.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                    requestList.All(i =>
                    {
                        i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                        i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                        return true;
                    });
                    model.RegularProjectDetails.RequestInfoList = requestList;
                }

                if (IsExistingOrNew.Equals(Constants.ReleaseType.New))
                {
                    @ViewBag.DefaultTab = "4";
                }
                else
                {
                    @ViewBag.DefaultTab = "3";
                }

                LoggerFactory.LogWriter.MethodExit();
                return PartialView("CreateRegularProjectPartialView", model);
            }
            catch (Exception ex)
            {
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Unable to Resubmit Project" });
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for Cancel Regular Project By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CancelRegularProject(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string configList = string.Empty;

                //*** start for filling dropdown****//
                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }

                CacheData(model, configList);
                //   *** End for filling dropdown****//

                model.RegularProjectDetails.StatusType = (int)General.StatusType.Cancelled;
                model.RegularProjectDetails.StatusTypeDesc = General.StatusType.Cancelled.ToString();

                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    string Beginslash = "\"\\/D";
                    string EndSlash = ")\\/\"";
                    model.RegularProjectDetails.RequestInfoList.All(i =>
                    {

                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace("/D", Beginslash);
                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace(")/", EndSlash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace("/D", Beginslash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace(")/", EndSlash);
                        return true;
                    });
                }

                if (Request.Form["hdnProjectModifiedDate"] != null)
                {
                    var serializer = new JavaScriptSerializer();
                    var modifiedDateRouted = serializer.Deserialize<DateTime>(Request.Form["hdnProjectModifiedDate"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    model.RegularProjectDetails.ProjectModifiedDate = modifiedDateRouted;
                }

                ClearanceRoutedProject clearanceRoutedProjectData = new ClearanceRoutedProject();
                clearanceRoutedProjectData = UpdateRequestWaitingToCancelForRegular(model);
                _IClearanceProjectRepository.StatusProjectUpdate(model.RegularProjectDetails.ClrProjectId, model.RegularProjectDetails.StatusType, clearanceRoutedProjectData, getUserInfo(), model.RegularProjectDetails.ProjectModifiedDate);

                ViewBag.LoadTemplate = "1";
                LoggerFactory.LogWriter.Info("Successfully Called Cancel Regular Project");
                LoggerFactory.LogWriter.Debug("Successfully Called Cancel Regular Project");

                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);

                }

                string isExistingOrNew = string.Empty;
                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);
                if (model.RegularProjectDetails.ReleaseNewOrExisting != null)
                {
                    isExistingOrNew = model.RegularProjectDetails.ReleaseNewOrExisting;
                }

                //**** if new release is saving, get artist details  from hidden field
                if (isExistingOrNew.Equals("New"))
                {
                    model = SetArtistDetailsForRelease(model, collection);
                    // done for mainting the configuration dropdown in case of new release
                    if (model.RegularProjectDetails.ObjRelease != null)
                    {
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = model.RegularProjectDetails.ObjRelease[i].ConfigId;
                        }
                    }
                    else
                    {
                        ClearanceReleaseSearchResult releaseDetailGRSData = new ClearanceReleaseSearchResult();
                        ClearanceRelease releaseNew = new ClearanceRelease();
                        releaseNew.Archive_Flag = "N";
                        releaseNew.TrackCount = null;
                        releaseNew.No_Components = null;

                        releaseDetailGRSData.releaseDetail = new List<ClearanceRelease>();
                        releaseDetailGRSData.releaseDetail.Add(releaseNew);
                        model.RegularProjectDetails.ObjRelease = releaseDetailGRSData.releaseDetail;
                    }

                    model.RegularProjectDetails.IsExisting = false;
                }
                else
                {
                    model.RegularProjectDetails.IsExisting = true;
                }
                //******End for artist details*****///
                SetRegularProjectEntities(command, model);
                if (isExistingOrNew.Equals("New"))
                {
                    GetPackageInfoForReleaseNew(model, collection);
                }
                // save request type detail
                model.RegularProjectDetails.ScopeAndRequestType = SaveRequestType(model);
                
                if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                {
                    if (collection.GetValues("hdnMultiartist")[0] != string.Empty)
                    {
                        model.RegularProjectDetails.MultiArtist = Convert.ToBoolean(collection.GetValues("hdnMultiartist")[0]);
                    }
                }
                Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = model.RegularProjectDetails.listUploadDocument;
                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    if (isExistingOrNew.Equals("New"))
                    {
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ExistingReleases = clearancePackageRelease[i].ExistingReleases;
                            model.RegularProjectDetails.ObjRelease[i].RemovedPackageReleases = clearancePackageRelease[i].RemovedPackageReleases;
                            model.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(model.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();

                        }
                    }
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                }

                //Remove resources where archive flag is set to "Y"
                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    model.RegularProjectDetails.ClearanceResource = RemoveResourceList(model.RegularProjectDetails.ClearanceResource);
                }
                else
                {
                    model.RegularProjectDetails.ClearanceResource = new List<ClearanceResource>();
                }

                TempData["EntityForSubmtProject" + "_" + model.RegularProjectDetails.ClrProjectId] = model;
                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    List<ClearanceInboxRequest> requestList;
                    byte routingStatus;
                    var serializer = new JavaScriptSerializer();
                    requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.RegularProjectDetails.ClrProjectId.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                    requestList.All(i =>
                    {
                        i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                        i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                        return true;
                    });
                    model.RegularProjectDetails.RequestInfoList = requestList;
                }

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("CreateRegularProjectPartialView", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                return Json(new { Error = true, Message = "Error in Cancelling Project" });
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for Complete Regular Project By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompleteRegularProject(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("command: {0}", command));

                string configList = string.Empty;
                //*** start for filling dropdown****//
                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }

                CacheData(model, configList);
                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    string Beginslash = "\"\\/D";
                    string EndSlash = ")\\/\"";
                    model.RegularProjectDetails.RequestInfoList.All(i =>
                    {

                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace("/D", Beginslash);
                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace(")/", EndSlash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace("/D", Beginslash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace(")/", EndSlash);
                        return true;
                    });
                }
                //   *** End for filling dropdown****//
                model.RegularProjectDetails.StatusType = (int)General.StatusType.Completed;
                model.RegularProjectDetails.StatusTypeDesc = General.StatusType.Completed.ToString();

                if (Request.Form["hdnProjectModifiedDate"] != null)
                {
                    var serializer = new JavaScriptSerializer();
                    var modifiedDateRouted = serializer.Deserialize<DateTime>(Request.Form["hdnProjectModifiedDate"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    model.RegularProjectDetails.ProjectModifiedDate = modifiedDateRouted;
                }
                ClearanceRoutedProject clearanceRoutedProjectData = new ClearanceRoutedProject();
                clearanceRoutedProjectData = UpdateRequestWaitingToCancelForRegular(model);
                _IClearanceProjectRepository.StatusProjectUpdate(model.RegularProjectDetails.ClrProjectId, model.RegularProjectDetails.StatusType, clearanceRoutedProjectData, getUserInfo(), model.RegularProjectDetails.ProjectModifiedDate);

                ViewBag.LoadTemplate = "1";
                LoggerFactory.LogWriter.Info("Successfully Called Complete Regular Project");
                LoggerFactory.LogWriter.Debug("Successfully Called Complete Regular Project");
                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);

                }

                string isExistingOrNew = string.Empty;
                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);
                if (model.RegularProjectDetails.ReleaseNewOrExisting != null)
                {
                    isExistingOrNew = model.RegularProjectDetails.ReleaseNewOrExisting;
                }

                //**** if new release is saving, get artist details  from hidden field
                if (isExistingOrNew.Equals("New"))
                {
                    model = SetArtistDetailsForRelease(model, collection);
                    // done for mainting the configuration dropdown in case of new release
                    if (model.RegularProjectDetails.ObjRelease != null)
                    {
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = model.RegularProjectDetails.ObjRelease[i].ConfigId;
                        }
                    }
                    else
                    {
                        ClearanceReleaseSearchResult releaseDetailGRSData = new ClearanceReleaseSearchResult();
                        ClearanceRelease releaseNew = new ClearanceRelease();
                        releaseNew.Archive_Flag = "N";
                        releaseNew.TrackCount = null;
                        releaseNew.No_Components = null;

                        releaseDetailGRSData.releaseDetail = new List<ClearanceRelease>();
                        releaseDetailGRSData.releaseDetail.Add(releaseNew);
                        model.RegularProjectDetails.ObjRelease = releaseDetailGRSData.releaseDetail;
                    }

                    model.RegularProjectDetails.IsExisting = false;
                }
                else
                {
                    model.RegularProjectDetails.IsExisting = true;
                }
                //******End for artist details*****///
                SetRegularProjectEntities(command, model);
                if (isExistingOrNew.Equals("New"))
                {
                    GetPackageInfoForReleaseNew(model, collection);
                }
                // save request type detail
                model.RegularProjectDetails.ScopeAndRequestType = SaveRequestType(model);

                if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                {
                    if (collection.GetValues("hdnMultiartist")[0] != string.Empty)
                    {
                        model.RegularProjectDetails.MultiArtist = Convert.ToBoolean(collection.GetValues("hdnMultiartist")[0]);
                    }
                }
                Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = model.RegularProjectDetails.listUploadDocument;
                if (model.RegularProjectDetails.ObjRelease != null)
                {

                    if (isExistingOrNew.Equals("New"))
                    {
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ExistingReleases = clearancePackageRelease[i].ExistingReleases;
                            model.RegularProjectDetails.ObjRelease[i].RemovedPackageReleases = clearancePackageRelease[i].RemovedPackageReleases;
                            model.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(model.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();
                        }
                    }
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                }

                //Remove resources where archive flag is set to "Y"
                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    model.RegularProjectDetails.ClearanceResource = RemoveResourceList(model.RegularProjectDetails.ClearanceResource);
                }
                else
                {
                    model.RegularProjectDetails.ClearanceResource = new List<ClearanceResource>();
                }

                TempData["EntityForSubmtProject" + "_" + model.RegularProjectDetails.ClrProjectId] = model;
                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    List<ClearanceInboxRequest> requestList;
                    byte routingStatus;
                    var serializer = new JavaScriptSerializer();
                    requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.RegularProjectDetails.ClrProjectId.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                    requestList.All(i =>
                    {
                        i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                        i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                        return true;
                    });
                    model.RegularProjectDetails.RequestInfoList = requestList;
                }

                LoggerFactory.LogWriter.MethodExit();
                return PartialView("CreateRegularProjectPartialView", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                return Json(new { Error = true, Message = "Error in Completing Project" });
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for Reinstate Regular Project By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReinstateRegularProject(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("command: {0}", command));

                string configList = string.Empty;

                //*** start for filling dropdown****//
                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }
                CacheData(model, configList);

                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    string Beginslash = "\"\\/D";
                    string EndSlash = ")\\/\"";
                    model.RegularProjectDetails.RequestInfoList.All(i =>
                    {

                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace("/D", Beginslash);
                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace(")/", EndSlash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace("/D", Beginslash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace(")/", EndSlash);
                        return true;
                    });
                }

                model.RegularProjectDetails.StatusType = (int)General.StatusType.ReInstated;
                model.RegularProjectDetails.StatusTypeDesc = General.StatusType.ReInstated.ToString();

                if (Request.Form["hdnProjectModifiedDate"] != null)
                {
                    var serializer = new JavaScriptSerializer();
                    var modifiedDateRouted = serializer.Deserialize<DateTime>(Request.Form["hdnProjectModifiedDate"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    model.RegularProjectDetails.ProjectModifiedDate = modifiedDateRouted;
                }

                ClearanceRoutedProject clearanceRoutedProjectData = new ClearanceRoutedProject();
                clearanceRoutedProjectData = UpdateRequestCancelToWaitingForRegular(model);
                bool isReinstated = _IClearanceProjectRepository.StatusProjectUpdate(model.RegularProjectDetails.ClrProjectId, model.RegularProjectDetails.StatusType, clearanceRoutedProjectData, getUserInfo(), model.RegularProjectDetails.ProjectModifiedDate);
                if (isReinstated)
                {
                    model.RegularProjectDetails.StatusType = (int)General.StatusType.ReSubmitted;
                    model.RegularProjectDetails.StatusTypeDesc = General.StatusType.ReSubmitted.ToString();
                }
                else
                {
                    model.RegularProjectDetails.StatusType = (int)General.StatusType.Submitted;
                    model.RegularProjectDetails.StatusTypeDesc = General.StatusType.Submitted.ToString();
                }
                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);
                //***** Territory Section End*********///


                string IsExistingOrNew = string.Empty;

                if (model.RegularProjectDetails.ReleaseNewOrExisting != null)
                {
                    IsExistingOrNew = model.RegularProjectDetails.ReleaseNewOrExisting;
                }

                //**** if new release is saving, get artist details  from hidden field
                if (IsExistingOrNew.Equals("New"))
                {
                    model = SetArtistDetailsForRelease(model, collection);
                    // done for mainting the configuration dropdown in case of new release
                    if (model.RegularProjectDetails.ObjRelease != null)
                    {
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = model.RegularProjectDetails.ObjRelease[i].ConfigId;
                        }
                    }

                }
                //******End for artist details*****///

                SetRegularProjectEntities(command, model);
                // save request type detail
                model.RegularProjectDetails.ScopeAndRequestType = SaveRequestType(model);
                Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = model.RegularProjectDetails.listUploadDocument;

                //Remove resources where archive flag is set to "Y"
                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    model.RegularProjectDetails.ClearanceResource = RemoveResourceList(model.RegularProjectDetails.ClearanceResource);
                }
                else
                {
                    model.RegularProjectDetails.ClearanceResource = new List<ClearanceResource>();
                }
                ModelState.Clear();

                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), model.RegularProjectDetails.StatusType.ToString(), true).ToString();

                // change all cancelled request status as waiting
                ViewBag.LoadTemplate = "1";
                LoggerFactory.LogWriter.Info("Successfully Called Reinstate Regular Project");
                LoggerFactory.LogWriter.Debug("Successfully Called Reinstate Regular Project");
                TempData["EntityForSubmtProject" + "_" + model.RegularProjectDetails.ClrProjectId] = model;
                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    List<ClearanceInboxRequest> requestList;
                    byte routingStatus;
                    var serializer = new JavaScriptSerializer();
                    requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.RegularProjectDetails.ClrProjectId.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                    requestList.All(i =>
                    {
                        i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                        i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                        return true;
                    });
                    model.RegularProjectDetails.RequestInfoList = requestList;
                }

                LoggerFactory.LogWriter.MethodExit();
                return PartialView("CreateRegularProjectPartialView", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                return Json(new { Error = true, Message = "Error in Reinstating Project" });
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for Reopen Regular Project By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReopenRegularProject(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("command: {0}", command));

                string configList = string.Empty;

                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }

                CacheData(model, configList);

                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    string Beginslash = "\"\\/D";
                    string EndSlash = ")\\/\"";
                    model.RegularProjectDetails.RequestInfoList.All(i =>
                    {

                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace("/D", Beginslash);
                        i.ModifiedDateRequestString = i.ModifiedDateRequestString.Replace(")/", EndSlash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace("/D", Beginslash);
                        i.ModifiedDateRoutedString = i.ModifiedDateRoutedString.Replace(")/", EndSlash);
                        return true;
                    });
                }

                model.RegularProjectDetails.StatusType = (int)General.StatusType.ReOpened;
                model.RegularProjectDetails.StatusTypeDesc = General.StatusType.ReOpened.ToString();

                if (Request.Form["hdnProjectModifiedDate"] != null)
                {
                    var serializer = new JavaScriptSerializer();
                    var modifiedDateRouted = serializer.Deserialize<DateTime>(Request.Form["hdnProjectModifiedDate"].ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                    model.RegularProjectDetails.ProjectModifiedDate = modifiedDateRouted;
                }
                _IClearanceProjectRepository.StatusProjectUpdate(model.RegularProjectDetails.ClrProjectId, model.RegularProjectDetails.StatusType, null, getUserInfo(), model.RegularProjectDetails.ProjectModifiedDate);

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);

                string IsExistingOrNew = string.Empty;

                if (model.RegularProjectDetails.ReleaseNewOrExisting != null)
                {
                    IsExistingOrNew = model.RegularProjectDetails.ReleaseNewOrExisting;
                }

                //**** if new release is saving, get artist details  from hidden field
                if (IsExistingOrNew.Equals("New"))
                {
                    model = SetArtistDetailsForRelease(model, collection);
                    // done for mainting the configuration dropdown in case of new release
                    for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        model.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = model.RegularProjectDetails.ObjRelease[i].ConfigId;
                    }

                }
                //******End for artist details*****///

                SetRegularProjectEntities(command, model);
                if (IsExistingOrNew.Equals("New"))
                {
                    GetPackageInfoForReleaseNew(model, collection);
                }
                // save request type detail
                model.RegularProjectDetails.ScopeAndRequestType = SaveRequestType(model);
                Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = model.RegularProjectDetails.listUploadDocument;

                //Remove resources where archive flag is set to "Y"
                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    model.RegularProjectDetails.ClearanceResource = RemoveResourceList(model.RegularProjectDetails.ClearanceResource);
                }
                else
                {
                    model.RegularProjectDetails.ClearanceResource = new List<ClearanceResource>();
                }
                ModelState.Clear();

                Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId] = model;


                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), model.RegularProjectDetails.StatusType.ToString(), true).ToString();
                // change all cancelled request status as waiting

                for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                {
                    model.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(model.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();

                }
                ViewBag.LoadTemplate = "1";
                LoggerFactory.LogWriter.Info("Successfully Called Reopen Regular Project");
                LoggerFactory.LogWriter.Debug("Successfully Called Reopen Regular Project");
                TempData["EntityForSubmtProject" + "_" + model.RegularProjectDetails.ClrProjectId] = model;
                ViewBag.RoleGroup = model.roleGroupName;
                if (model.RegularProjectDetails.RequestInfoList != null)
                {
                    List<ClearanceInboxRequest> requestList;
                    byte routingStatus;
                    var serializer = new JavaScriptSerializer();
                    requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.RegularProjectDetails.ClrProjectId.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                    requestList.All(i =>
                    {
                        i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                        i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                        return true;
                    });
                    model.RegularProjectDetails.RequestInfoList = requestList;
                }

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("CreateRegularProjectPartialView", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                return Json(new { Error = true, Message = "Error in Reopening Project" });
            }
        }

        /// <summary>
        /// Created By:Jyoti
        /// Description: Using for Add to project Button functionality in Master Project.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult MasterAddButtonForResource(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                // get master dropdowns on project Information Tab
                model = SetSelectedDropDown(model);
                //set artist for resource
                ViewBag.ProjectTerritories = SetMasterManageTerritories(collection, model);
                model = SetArtistDetailsForResource(model, collection);
                model = AdvancePopupResourceListAdd(model, collection["resourceListFromSearchPopUp"]);
                //Remove all resources from listofClearanceResource which have archive flag Y
                model.MasterProjectDetails.ClearanceResource = RemoveResourceList(model.MasterProjectDetails.ClearanceResource);

                var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                Territories.Add(1, model.MasterProjectDetails.Territories);
                int tempI = 2;
                foreach (ClearanceResource cls in model.MasterProjectDetails.ClearanceResource)
                {
                    Territories.Add(++tempI, cls.TerritorialRights);
                }

                ViewBag.ProjectTerritories = Territories;
                ModelState.Clear();

                LoggerFactory.LogWriter.MethodExit();
                return PartialView("Resources", model);
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for Add to PRoject Button Functionality  in  Regular Project for resource By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegularAddButtonForResource(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("command: {0}", command));

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);

                //Add Resources to Project-Regular project Resource Tab
                model = AdvancePopupResourceListAddRegular(model, collection["hdnListIsrc"], collection["resourceListFromSearchPopUp"]);
                CacheData(model, "");

                var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                Territories.Add(1, model.RegularProjectDetails.Territories);
                Territories.Add(2, model.RegularProjectDetails.ScopeAndRequestType.Territories);
                int tempI = 2;
                foreach (ClearanceResource cls in model.RegularProjectDetails.ClearanceResource)
                {
                    Territories.Add(++tempI, cls.TerritorialRights);
                }

                ViewBag.ProjectTerritories = Territories;
                //Remove resources where archive flag is set to "Y"
                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    model.RegularProjectDetails.ClearanceResource = RemoveResourceList(model.RegularProjectDetails.ClearanceResource);
                }
                ModelState.Clear();

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("ResourcesRegular", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                return PartialView("ResourcesRegular", model);
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for Duplicate Button Functionality  in  Regular Project By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult RegularAddButtonForRelease(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                command = model.RegularProjectDetails.Command;
                string configList = string.Empty;
                // ************START*********************Setting Next status--- Maintain MAster Project
                //To set Dropdown values of freehand Resource

                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        model.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = model.RegularProjectDetails.ObjRelease[i].ConfigId;
                        model.RegularProjectDetails.ObjRelease[i].PackageText = model.RegularProjectDetails.ObjRelease[i].PackageIndicator == "N" ? "No" : "Yes";
                    }
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                }
                ViewBag.DefaultTab = collection["hdnDefaultTab"];

                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }
                else
                {
                    configList = string.Empty;
                }

                CacheData(model, configList);

                // render CreateRegularProject with AddToProject entities updated
                if ((collection["resourceListAddToProject"] != null) && (collection["resourceListAddToProject"].Equals("Child")))
                {
                    List<ClearanceRelease> listSelectedItems = new List<ClearanceRelease>();
                    listSelectedItems = ParseStringRelease(collection["releaseListFromSearchPopUp"]);
                    // Get tracks from R2 & add it to ObjRelease.resourceDetail
                    if (model.RegularProjectDetails.ObjRelease == null)
                    {
                        model.RegularProjectDetails.ObjRelease = new List<ClearanceRelease>();
                    }
                    foreach (ClearanceRelease clrRelease in listSelectedItems)
                    {
                        string findDuplicate = (from findArchive in model.RegularProjectDetails.ObjRelease
                                                where findArchive.R2ReleaseId == clrRelease.R2ReleaseId
                                                select findArchive.Archive_Flag).FirstOrDefault();

                        if (string.IsNullOrEmpty(findDuplicate) || findDuplicate == "Y")
                        {
                            model.RegularProjectDetails.ObjRelease.Insert(0, clrRelease);
                        }
                    }
                    ModelState.Clear();

                    ViewBag.DefaultTab = "2";

                    LoggerFactory.LogWriter.MethodExit();

                    return PartialView("ReleaseRegular", model);
                }

                if ((collection["searchUniqueRecord"] != null) && collection["searchUniqueRecord"].Equals("Unique"))
                {
                    string UpcNumber = collection["txtUPC"].ToString().Replace(",", "");
                    string ReleaseTitle = collection["txtReleaseTitle"].ToString().Replace(",", "");
                    string ArtistName = collection["txtArtistName"].ToString().Replace(",", "");
                    long ArtistID = Int64.Parse(collection["txtArtistID"].Equals("") ? "0" : collection["txtArtistID"]);
                    string R2ProjectId = collection["txtR2ProjectId"].ToString().Replace(",", "");
                    List<ClearanceRelease> releaseExisting = new List<ClearanceRelease>();
                    ClearanceReleaseSearchResult clrReleaseSearchResult = new ClearanceReleaseSearchResult();

                    // Search from R2 for Unique Record condition.
                    releaseExisting = SearchReleaseR2(UpcNumber, ReleaseTitle, ArtistName, ArtistID, R2ProjectId);

                    if (model.RegularProjectDetails.ObjRelease != null)
                    {
                        foreach (ClearanceRelease objrelease in releaseExisting)
                        {
                            string findDuplicate = (from findArchive in model.RegularProjectDetails.ObjRelease
                                                    where findArchive.R2ReleaseId == objrelease.R2ReleaseId
                                                    select findArchive.Archive_Flag).FirstOrDefault();

                            if (string.IsNullOrEmpty(findDuplicate) || findDuplicate == "Y")
                            {
                                model.RegularProjectDetails.ObjRelease.Add(objrelease);
                            }
                        }
                    }
                    else
                    {
                        model.RegularProjectDetails.ObjRelease = releaseExisting;
                    }
                    // release existing
                    ViewBag.DefaultTab = "2";

                    LoggerFactory.LogWriter.MethodExit();

                    return PartialView("ReleaseRegular", model);
                }
                LoggerFactory.LogWriter.MethodExit();

                return PartialView("ReleaseRegular", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;

                return PartialView("ReleaseRegular", model);
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description : Using for change release dropdown By AJAX call.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegularReleaseDropdownChange(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("command: {0}", command));

                command = model.RegularProjectDetails.Command;
                string configList = string.Empty;

                //*** start for filling dropdown****//
                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }
                else
                {
                    configList = string.Empty;
                }

                var iClearanceProjectModel = _IClearanceProjectRepository.GetMasterData(_type, getUserInfo());
                model.CurrPriceLevelList = iClearanceProjectModel.CurrPriceLevelList;
                model.RequestedPriceLevelList = iClearanceProjectModel.RequestedPriceLevelList;
                iClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(getUserInfo().UserLoginName);
                model.CurrencyList = iClearanceProjectModel.CurrencyList;
                model.CompanyList = iClearanceProjectModel.CompanyList;
                //   *** End for filling dropdown****//

                // for maintaining the state of TAB selected
                ViewBag.DefaultTab = collection["hdnDefaultTab"];

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);
                //***** Territory Section End*********///

                string IsExistingOrNew = string.Empty;

                if (model.RegularProjectDetails.ReleaseNewOrExisting == "Exist")
                {
                    model.RegularProjectDetails.ObjRelease = new List<ClearanceRelease>();
                    model.RegularProjectDetails.IsExisting = true;
                }
                else if (model.RegularProjectDetails.ReleaseNewOrExisting == "New")
                {
                    model.RegularProjectDetails.IsExisting = false;
                    ClearanceReleaseSearchResult releaseDetailGRSData = new ClearanceReleaseSearchResult();
                    ClearanceRelease releaseNew = new ClearanceRelease();
                    releaseNew.Archive_Flag = "N";
                    releaseDetailGRSData.releaseDetail = new List<ClearanceRelease>();
                    releaseDetailGRSData.releaseDetail.Add(releaseNew);
                    model.RegularProjectDetails.ObjRelease = releaseDetailGRSData.releaseDetail;
                }


                if (Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] == null)
                {
                    model.RegularProjectDetails.listUploadDocument = new List<UploadDocument>();
                }
                else
                {
                    model.RegularProjectDetails.listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId];
                }

                LoggerFactory.LogWriter.MethodExit();
                
                if (model.RegularProjectDetails.ClrProjectId != 0)
                {
                    Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId] = model;
                }
                return PartialView("CreateRegularProjectPartialView", model);
            }
            catch (Exception ex)
            {
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return PartialView("CreateRegularProjectPartialView", model);
            }
        }

        private int UpdateMasterProjectResource(long ResourceIndexToUpdate, ClearanceProjectModel model)
        {
            try
            {

                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("listResource: {0}", ResourceIndexToUpdate));

                int UpdatedResourceIndex = int.Parse(ResourceIndexToUpdate.ToString());
                model.MasterProjectDetails.ClearanceResource[UpdatedResourceIndex - 1].ReplaceFreeHandFlag = "Y";
                model.MasterProjectDetails.ClearanceResource[UpdatedResourceIndex - 1].IsNewlyAddedAfterSubmit = false;//Added for UC-011A
                LoggerFactory.LogWriter.MethodExit();
                return UpdatedResourceIndex;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private int UpdateRegularProjectResource(long ResourceIndexToUpdate, ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("listResource: {0}", ResourceIndexToUpdate));
                int UpdatedResourceIndex = int.Parse(ResourceIndexToUpdate.ToString());
                model.RegularProjectDetails.ClearanceResource[UpdatedResourceIndex - 1].ReplaceFreeHandFlag = "Y";
                LoggerFactory.LogWriter.MethodExit();
                return UpdatedResourceIndex;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Projects the search.
        /// </summary>
        /// <param name="clearanceCompanyCountryId">The clearance company country id.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="projectTitle">The project title.</param>
        /// <param name="labelId">The label.</param>
        /// <param name="dataAdminCompany">The data admin company.</param>
        /// <param name="jtStartIndex">Start index of the jt.</param>
        /// <param name="jtPageSize">Size of the jt page.</param>
        /// <param name="jtSorting">The jt sorting.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ProjectSearch(string projectCode = "", string projectTitle = "", long artistId = 0, string artistName = "", long divisionId = 0, long labelId = 0, long dataAdminCompanyId = 0, int pageSize = 0, int jtStartIndex = 0, string jtSorting = null, long RowIndex = 0)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "projectId:{0},projectTitle:{1},divisionId:{2},labelId:{3},dataAdminCompanyId;{4},pageSize:{5},jtStartIndex:{6},jtSorting:{7}",
                       "ProjectSearch", projectCode, projectTitle, divisionId, labelId, dataAdminCompanyId, pageSize, jtStartIndex, jtSorting == null ? "null" : jtSorting));
                var projectcriteria = new ProjectSearchCriteria
                {
                    Criteria = { PageSize = pageSize, RowIndex = RowIndex, UserName = SessionWrapper.CurrentUserInfo.UserLoginName, StartIndex = jtStartIndex },
                    ArtistName = artistName,
                    ClearanceAdminCompanyId = dataAdminCompanyId,
                    LabelId = labelId,
                    ProjectCode = projectCode,
                    Title = projectTitle,
                    divisionId = divisionId,
                    ArtistId = artistId,
                    ApplicationType = "Gcs"
                };

                var projectresults = _IClearanceProjectRepository.ProjectSearch(projectcriteria);
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = projectresults.Values, RowIndex = projectresults.RowIndex, TotalRecordCount = projectresults.RowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// Propagates the unlinking release to resource.
        /// </summary>
        /// <returns></returns>
        public ActionResult LinkingNewReleaseToR2(ClearanceProjectModel model, FormCollection collection)
        {
            string R2ProjectId = string.Empty;
            string R2ProjectKey = string.Empty;
            
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (model != null)
                {
                    if (collection["hdnSearchForR2Project"] == "1")
                    {
                        // save R2_Project_Id in Clr_Project table & return R2_Project_Id on success, on failure return 0
                        string R2ProjectUniqueId = string.Empty;
                        R2ProjectUniqueId = collection["searchProjectListPush"].ToString();
                        R2ProjectKey = R2ProjectUniqueId; // to send project id to js for updating flag
                        R2ProjectId = collection["searchR2ProjectCode"].ToString();

                        bool bR2ProjectId = false;
                        if (!string.IsNullOrEmpty(R2ProjectUniqueId))
                        {
                            model.RegularProjectDetails.R2_Project_Id = long.Parse(R2ProjectUniqueId);
                            model.RegularProjectDetails.R2_Project_Code = R2ProjectId;
                            bR2ProjectId = _IClearanceProjectRepository.SaveR2ProjectIdLinking(model.RegularProjectDetails, getUserInfo());
                            // if result is false, make re project id as empty, so that push releases method should not be invoked
                            if (!bR2ProjectId)
                            {
                                R2ProjectId = string.Empty;
                                R2ProjectKey = string.Empty;
                            }
                        }
                        // R2_Project_Id will be updated in the model when this call returns to js file.
                    }
                    else
                    {
                        // merge here R2 adapter method for creating new project in R2 db, it would return R2_Project_Id
                        // fetch all values to add in R2 project from model
                        Dictionary<long, string> lstProjectData = new Dictionary<long, string>();
                        long projectId = 0;
                        long tryParseSearch = 0;
                        long adminCompanyId = 0;
                        long divisionId = 0;
                        long labelId = 0;
                        string projectTitle = collection["projectTitleFreeHandPush"];

                        if (long.TryParse(collection["companyIdFreeHandPush"].ToString(), out tryParseSearch))
                            adminCompanyId = tryParseSearch;
                        
                        if (long.TryParse(collection["divisionIdFreeHandPush"].ToString(), out tryParseSearch))
                            divisionId = tryParseSearch;

                        if (long.TryParse(collection["labelIdFreeHandPush"].ToString(), out tryParseSearch))
                            labelId = tryParseSearch;
                        
                        // get artist info from collection
                        List<long> artistIds = new List<long>();
                        ArtistInfo artistInfoR2 = new ArtistInfo();
                        string artistDetail = collection["hdnArtistIdPush"];
                        string[] artistInfo = artistDetail.Split('=');
                        for (int i = 0; i < artistInfo.Length; i++)
                        {
                            string[] listArtists = artistInfo[i].ToString().Split(':');

                            //check the project user id is not null or empty
                            if (!string.IsNullOrEmpty(listArtists[0].ToString()))
                            {
                                artistInfoR2.Name = listArtists[0].ToString();
                                artistInfoR2.Id = long.Parse(listArtists[1].ToString());
                                artistIds.Add(long.Parse(listArtists[1].ToString()));
                            }
                        }
                        // send all above values to R2 method here
                        // R2ProjectId = R2 call for creating new project which returns atkeast R2ProjectId
                        R2Project projectR2 = new R2Project();
                        projectR2.Description = projectTitle;
                        projectR2.CompanyId = adminCompanyId;
                        projectR2.DivisionId = divisionId;
                        projectR2.LabelId = labelId;
                        projectR2.ArtistIds = artistIds;
                        lstProjectData = _IClearanceProjectRepository.CreateProject(projectR2, model.RegularProjectDetails.ClrProjectId, getUserInfo());
                        foreach (var projectData in lstProjectData)
                        {
                            R2ProjectId = Convert.ToString(projectData.Value);
                            projectId = projectData.Key;
                            R2ProjectKey = Convert.ToString(projectData.Key);
                            break;
                        }
                        model.RegularProjectDetails.R2_Project_Id = projectId;
                        model.RegularProjectDetails.R2_Project_Code = R2ProjectId;
                        bool projectLink = false;
                        projectLink = _IClearanceProjectRepository.SaveR2ProjectIdLinking(model.RegularProjectDetails, getUserInfo());

                        if (!projectLink)
                        {
                            R2ProjectId = string.Empty;
                            R2ProjectKey = string.Empty;
                        }
                    }

                }

                LoggerFactory.LogWriter.MethodExit();

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                R2ProjectId = string.Empty;
                R2ProjectKey = string.Empty;
                return Json(R2ProjectId + '~' + R2ProjectKey, JsonRequestBehavior.AllowGet);
            }

            
            return Json(R2ProjectId + '~' + R2ProjectKey, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PushNewReleasesToR2(ClearanceProjectModel model, FormCollection collection)
        {
            
            string data = string.Empty;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                // latest data is fetched from database, for PushToR2 status. For Resource & releases already pushed to R2.
                // 1. due to release & resource PushToR2 flag not getting updated after successful push in model.
                // 2. And if request summar refresh button is not clicked then it model will no contain latest routing status
                // 3. And in case cancel/reinstate buttons did not update routing status correctly.
                // 4. for Manual Upc it will be saved in the database, only after save button is clicked.
                // Below hit to db for fetching detail is required, to avoid any failure due to above scenerios
                _IClearanceProjectModel.RegularProjectDetails = _IClearanceProjectRepository.GetRegularProjectDetails(Convert.ToInt32(model.RegularProjectDetails.ClrProjectId), getUserInfo());

                // flag if nothing can be pushed
                bool isPushValid = true;
                bool isReleaseToPush = true;
                bool isResourceToPush = true;
                List<ClearanceRelease> lstReleaseToPush = new List<ClearanceRelease>();
                List<ClearanceResource> lstResourceToPush = new List<ClearanceResource>();

                if (isPushValid)
                {
                    lstReleaseToPush = _IClearanceProjectModel.RegularProjectDetails.ObjRelease.Where(i => string.IsNullOrEmpty(i.isPushedToR2) || i.isPushedToR2 == "N").ToList();
                    // if there are releases to push, check if upc is not null
                    // if upc is null then it should third party project only
                    if (lstReleaseToPush != null && lstReleaseToPush.Count > 0)
                    {
                        bool isUpcNull = false;
                        var upcCheck = lstReleaseToPush.Where(i => string.IsNullOrEmpty(i.Upc)).ToList().Take(1);

                        if (upcCheck != null && upcCheck.Count() > 0)
                        {
                            isUpcNull = true;
                        }
                        else
                        {
                            isUpcNull = false;
                        }

                        // check if project is third party then upc null can be pushed
                        if (!_IClearanceProjectModel.RegularProjectDetails.ThirdParty && isUpcNull)
                        {
                            data = isUpcNull ? (string.IsNullOrEmpty(data) ? "UPC" : string.Format("{0}{1}", data, "~UPC")) : data;
                            // if all the releases are having upc null then set the flag that releases will not be pushed
                            int ireleaseCount = lstReleaseToPush.Count;
                            int ireleaseUpcNullCount = lstReleaseToPush.Count(i => string.IsNullOrEmpty(i.Upc));
                            if (ireleaseCount == ireleaseUpcNullCount)
                            {
                                // there are no releases that can be pushed
                                isReleaseToPush = false;
                            }
                            else
                            {
                                // get the list of releases that can be pushed
                                lstReleaseToPush = lstReleaseToPush.Where(i => (!string.IsNullOrEmpty(i.Upc))).ToList();
                            }

                        }

                    }
                    else
                    {
                        isReleaseToPush = false;
                    }

                }

                if (isPushValid)
                {
                    // if there are releases to push, call xml generating method or update GCS db for releases that push has been initiated
                    lstResourceToPush = _IClearanceProjectModel.RegularProjectDetails.ClearanceResource.Where(i => i.isPushedToR2 == null || i.isPushedToR2 == "" || i.isPushedToR2 == "N").ToList();
                    // if there is any resource having status cancelled or rejected
                    if (lstResourceToPush != null && lstResourceToPush.Count > 0)
                    {
                        bool isResourcefreehand = false;
                        foreach (ClearanceResource clrResourceCheck in lstResourceToPush)
                        {
                            isResourcefreehand = clrResourceCheck.R2_ResourceId == 0 ? true : false;
                        }

                        data = isResourcefreehand ? (string.IsNullOrEmpty(data) ? "FreehandResource" : string.Format("{0}{1}", data, "~FreehandResource")) : data;
                        int iresourceCount = lstResourceToPush.Count;
                        // cancelled, rejected, Auto Rejected
                        foreach (ClearanceResource clrResource in lstResourceToPush)
                        {
                            List<ClearanceInboxRequest> clrInboxRequest = new List<ClearanceInboxRequest>();
                            clrInboxRequest = _IClearanceProjectModel.RegularProjectDetails.RequestInfoList.Where(i =>
                                i.ResourceId == clrResource.ResourceId).ToList();

                            if (clrInboxRequest != null && clrInboxRequest.Count > 0)
                            {
                                if (clrInboxRequest[0].RoutingCalculationStatus == 3)
                                {
                                    List<ClearanceInboxRequest> clrInboxRequestCount = new List<ClearanceInboxRequest>();
                                    clrInboxRequestCount = clrInboxRequest.Where(i => i.ApprovalStatusId != 5 && i.ApprovalStatusId != 6 && i.ApprovalStatusId != 7
                                        && i.ApprovalStatusId != 10).ToList();
                                    if (clrInboxRequestCount != null && clrInboxRequestCount.Count > 0)
                                    {
                                        clrResource.Resource_Status = "approved";
                                    }
                                    else
                                    {
                                        clrResource.Resource_Status = "cancelled";
                                    }
                                }
                                else
                                {
                                    clrResource.Resource_Status = "cancelled";
                                }
                            }
                            else
                            {
                                clrResource.Resource_Status = "cancelled";
                            }
                        }

                        int iresourceNullCount = lstResourceToPush.Count(i => i.Resource_Status == "cancelled" || i.R2_ResourceId == 0);
                        if (iresourceCount == iresourceNullCount)
                        {
                            // there are no releases that can be pushed
                            isResourceToPush = false;
                        }
                        else
                        {
                            // get the list of releases that can be pushed- status & not freehand
                            lstResourceToPush = lstResourceToPush.Where(i => i.Resource_Status != "cancelled" && i.R2_ResourceId != 0).ToList();

                        }
                    }
                    else
                    {
                        isResourceToPush = false;
                    }

                }

                if (isPushValid)
                {
                    if (isReleaseToPush)
                    {
                        string releaseList = string.Empty;
                        // saving releases to intermediate table
                        // saving resources to intermediate table
                        List<ClearancePushR2Item> clrPushR2Item = new List<ClearancePushR2Item>();
                        foreach (ClearanceRelease clrRelease in lstReleaseToPush)
                        {
                            ClearancePushR2Item clrPushItem = new ClearancePushR2Item();
                            clrPushItem.ArchiveFlag = "N";
                            clrPushItem.ClrProjectId = _IClearanceProjectModel.RegularProjectDetails.ClrProjectId;
                            clrPushItem.CreatedDttm = DateTime.Now;
                            clrPushItem.CreatedUser = SessionWrapper.CurrentUserInfo.UserLoginName;
                            clrPushItem.Error_Description = null;
                            clrPushItem.ItemId = clrRelease.ReleaseId;
                            clrPushItem.ItemType = 1;
                            clrPushItem.ModifiedDttm = DateTime.Now;
                            clrPushItem.ModifiedUser = SessionWrapper.CurrentUserInfo.UserLoginName;
                            clrPushItem.Status_Type = 1;
                            clrPushR2Item.Add(clrPushItem);
                            if (string.IsNullOrEmpty(releaseList))
                            {
                                releaseList = clrRelease.ReleaseId.ToString();
                            }
                            else
                            {
                                releaseList = string.Format("{0}~{1}", releaseList, clrRelease.ReleaseId.ToString()); //releaseList + "~" + clrRelease.ReleaseId.ToString();
                            }

                        }
                        _IClearanceProjectRepository.SaveReleaseResourceToPushToR2(clrPushR2Item, getUserInfo());
                        data = string.IsNullOrEmpty(data) ? releaseList : string.Format("{0}~{1}", data, releaseList);
                    }
                    if (isResourceToPush)
                    {
                        // saving resources to intermediate table
                        string resourceList = string.Empty;
                        List<ClearancePushR2Item> clrPushR2Item = new List<ClearancePushR2Item>();
                        foreach (ClearanceResource clrResource in lstResourceToPush)
                        {
                            ClearancePushR2Item clrPushItem = new ClearancePushR2Item();
                            clrPushItem.ArchiveFlag = "N";
                            clrPushItem.ClrProjectId = _IClearanceProjectModel.RegularProjectDetails.ClrProjectId;
                            clrPushItem.CreatedDttm = DateTime.Now;
                            clrPushItem.CreatedUser = SessionWrapper.CurrentUserInfo.UserLoginName;
                            clrPushItem.Error_Description = null;
                            clrPushItem.ItemId = clrResource.ClearanceResourceId;
                            clrPushItem.ItemType = 2;
                            clrPushItem.ModifiedDttm = DateTime.Now;
                            clrPushItem.ModifiedUser = SessionWrapper.CurrentUserInfo.UserLoginName;
                            clrPushItem.Status_Type = 1;
                            clrPushR2Item.Add(clrPushItem);
                            if (string.IsNullOrEmpty(resourceList))
                            {
                                resourceList = clrResource.ResourceId.ToString();
                            }
                            else
                            {
                                resourceList = string.Format("{0}~{1}", resourceList, clrResource.ResourceId.ToString());
                            }
                        }
                        _IClearanceProjectRepository.SaveReleaseResourceToPushToR2(clrPushR2Item, getUserInfo());
                        data = string.IsNullOrEmpty(data) ? resourceList : string.Format("{0}~{1}", data, resourceList);
                    }

                    // if condition based on isReleaseToPush & isResourceToPush return
                    if (isReleaseToPush || isResourceToPush)
                    {
                        data = string.IsNullOrEmpty(data) ? "Success" : string.Format("{0}{1}", data, "~Success");
                    }
                    else
                        data = string.IsNullOrEmpty(data) ? "NoPush" : string.Format("{0}{1}", data, "~NoPush");
                }
                else
                {
                    data = string.IsNullOrEmpty(data) ? "NoPush" : string.Format("{0}{1}", data, "~NoPush");
                }

                LoggerFactory.LogWriter.MethodExit();

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // generate xml for Push New release to R2
        private void TrySerializeImages(XmlTextWriter writer, IClearanceProjectModel model)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (model.RegularProjectDetails.ObjRelease.Count > 0)
            {
                XmlSerializer ser = new XmlSerializer(model.RegularProjectDetails.ObjRelease[0].GetType());
                writer.WriteStartElement("data");
                writer.WriteStartElement("releases");

                ser.Serialize(writer, model.RegularProjectDetails.ObjRelease[0]);

                writer.WriteEndElement();
                writer.WriteEndElement();
            }

            LoggerFactory.LogWriter.MethodExit();
        }

        [OutputCache(Duration = 0)]
        public JsonResult AutoCompleteSearch(SearchCriteria autoSearch)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<string> tags = _IClearanceProjectRepository.AutoCompleteSearch(autoSearch, SessionWrapper.CurrentUserInfo.UserLoginName);
                LoggerFactory.LogWriter.MethodExit();
                return this.Json(tags, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #region request summary
        public List<ClearanceInboxRequest> GetRequestSummaryMockData()
        {
            LoggerFactory.LogWriter.MethodStart();
            return new List<ClearanceInboxRequest>();
        }


        [HttpPost]
        public ActionResult GetRequestsummary(ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ClearanceInboxRequest clearanceReq = new ClearanceInboxRequest();
                List<ClearanceInboxRequest> requestList;
                byte routingStatus;
                requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.MasterProjectDetails.ClearanceProjectID.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                model.MasterProjectDetails.RequestInfoList = requestList;
                var serializer = new JavaScriptSerializer();
                model.MasterProjectDetails.RequestInfoList.All(i =>
                {
                    i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                    i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                    return true;
                });
                ModelState.Clear();
                LoggerFactory.LogWriter.MethodExit();
                return PartialView("RequestSummary", model);
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [HttpPost]
        public ActionResult GetRegularRequestsummary(ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ClearanceInboxRequest clearanceReq = new ClearanceInboxRequest();
                List<ClearanceInboxRequest> requestList;
                byte routingStatus;
                var serializer = new JavaScriptSerializer();
                requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(model.RegularProjectDetails.ClrProjectId.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                requestList.All(i =>
                {
                    i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                    i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                    return true;
                });
                model.RegularProjectDetails.RequestInfoList = requestList;
                ModelState.Clear();
                LoggerFactory.LogWriter.MethodExit();
                return PartialView("RequestSummaryRegular", model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpPost]
        private List<ClearanceInboxRequest> SeperateRequestList(string clrProjectId, int pageSize, int pageNo, short gridType, string jtSorting = null)
        {
            LoggerFactory.LogWriter.MethodStart();
            ClearanceInboxRequest clearanceReq = new ClearanceInboxRequest();
            List<ClearanceInboxRequest> requestList;
            List<ClearanceInboxRequest> _ResourcesList = new List<ClearanceInboxRequest>();
            requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(clrProjectId, gridType, pageSize, pageNo, jtSorting, getUserInfo(), out regularRoutingStatus);

            if (gridType == 1)
            {
                foreach (ClearanceInboxRequest request in requestList)
                {
                    if (!(request.ReleaseId > 0))
                    {
                        _ResourcesList.Add(request);
                    }
                }
            }
            else
            {
                foreach (ClearanceInboxRequest request in requestList)
                {
                    if (request.ReleaseId > 0)
                    {
                        _ResourcesList.Add(request);
                    }
                }
            }

            LoggerFactory.LogWriter.MethodExit();

            return _ResourcesList;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetRequestSummaryMaster(string clrProjectId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<ClearanceInboxRequest> requestSummaryMasterList = new List<ClearanceInboxRequest>();
                byte routingStatus;
                requestSummaryMasterList = _IClearanceProjectRepository.GetRequestSummaryRequests(clrProjectId, 0, jtPageSize, jtStartIndex, jtSorting, getUserInfo(), out routingStatus);
                var serializer = new JavaScriptSerializer();
                requestSummaryMasterList.All(i =>
                {
                    i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                    i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                    return true;
                });

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = requestSummaryMasterList.AsQueryable(), TotalRecordCount = (requestSummaryMasterList.Count == 0) ? 0 : requestSummaryMasterList[0].TotalRecordCount, RoutingStatus = routingStatus });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "cancel", ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetNewResources(string clrProjectId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<ClearanceInboxRequest> _newResourcesList = SeperateRequestList(clrProjectId, jtPageSize, jtStartIndex, 1, jtSorting);
                var serializer = new JavaScriptSerializer();
                _newResourcesList.All(i =>
                {
                    i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                    i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                    return true;
                });
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = _newResourcesList.AsQueryable(), TotalRecordCount = (_newResourcesList.Count == 0) ? 0 : _newResourcesList[0].TotalRecordCount, RoutingStatus = regularRoutingStatus });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "cancel", ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetExistingTracks(string clrProjectId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<ClearanceInboxRequest> _existingTracksList = SeperateRequestList(clrProjectId, jtPageSize, jtStartIndex, 2, jtSorting);
                var serializer = new JavaScriptSerializer();
                _existingTracksList.All(i =>
                {
                    i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                    i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                    return true;
                });
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = Constants.JsonOk, Records = _existingTracksList.AsQueryable(), TotalRecordCount = (_existingTracksList.Count == 0) ? 0 : _existingTracksList[0].TotalRecordCount, RoutingStatus = regularRoutingStatus });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "cancel", ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult RoutingDetailsOnRequestSummary(long routedItemID)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                List<ClearanceRoutingDetails> routingDetails = _IClearanceProjectRepository.GetRoutingDetails(routedItemID);
                ViewBag.ResourceTitle = routingDetails[0].ResourceTitle;
                LoggerFactory.LogWriter.MethodExit();
                return PartialView("RoutingDetails", routingDetails);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }


        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ActionOnRequest(int RequesterWorkgroupId, ClearanceInboxRequest clearanceInboxRequest)
        {
            int action = Convert.ToInt32(clearanceInboxRequest.SequenceNo);
            int routedItemId = (int)clearanceInboxRequest.RoutedItemId;
            long? workGroupId = clearanceInboxRequest.WorkgroupId;
            int requestId = (int)clearanceInboxRequest.RequestId;
            var serializer = new JavaScriptSerializer();
            var modifiedDateRouted = serializer.Deserialize<DateTime>(clearanceInboxRequest.ModifiedDateRoutedString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
            var modifiedRequestDate = serializer.Deserialize<DateTime>(clearanceInboxRequest.ModifiedDateRequestString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceRoutedProject clrRoutedProject = new ClearanceRoutedProject();
                List<RoutedResource> routedResourceList = new List<RoutedResource>();
                List<RoutedRequest> routedRequestList = new List<RoutedRequest>();
                RoutedResource routedResource = new RoutedResource();
                RoutedRequest routedRequest = new RoutedRequest();
                LeanUserInfo userinfo = new LeanUserInfo();
                userinfo = getUserInfo();
                clrRoutedProject.UserInfoDetails = userinfo;
                clrRoutedProject.RequesterWorkgroupId = RequesterWorkgroupId;
                clrRoutedProject.ClrProjectId = clearanceInboxRequest.ClearanceProjectId;


                if (action == 0)
                {
                    clrRoutedProject.RoutingAction = RoutingAction.Cancel;
                    routedResource.RoutedItemId = routedItemId;
                    routedResource.LastModifiedDateTime = modifiedDateRouted;
                    routedRequest.WorkgroupId = (int)workGroupId;
                    routedRequest.RequestStatus = RequestStatus.Waiting;
                    routedRequest.LastModifiedDateTime = modifiedRequestDate;
                    routedResourceList.Add(routedResource);
                    routedRequestList.Add(routedRequest);
                    routedResource.Request = routedRequestList;
                    clrRoutedProject.RoutedResources = routedResourceList;
                    _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());
                }
                else if (action == 1)
                {
                    clrRoutedProject.RoutingAction = RoutingAction.ReApply;
                    routedResource.RoutedItemId = routedItemId;
                    routedResource.LastModifiedDateTime = modifiedDateRouted;
                    routedRequest.Comment = clearanceInboxRequest.Comment;
                    routedRequest.RequestId = requestId;
                    routedRequest.WorkgroupId = (int)workGroupId;
                    routedRequest.RequestStatus = RequestStatus.Rejected;
                    routedRequest.LastModifiedDateTime = modifiedRequestDate;
                    routedResourceList.Add(routedResource);
                    routedRequestList.Add(routedRequest);
                    routedResource.Request = routedRequestList;
                    clrRoutedProject.RoutedResources = routedResourceList;
                    _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());

                }
                else if (action == 2)
                {
                    clrRoutedProject.RoutingAction = RoutingAction.ReInstate;
                    routedResource.RoutedItemId = routedItemId;
                    routedResource.LastModifiedDateTime = modifiedDateRouted;
                    routedRequest.WorkgroupId = (int)workGroupId;
                    routedRequest.RequestStatus = RequestStatus.Rejected; // as serilizing giving error
                    routedRequest.LastModifiedDateTime = modifiedRequestDate;
                    routedResourceList.Add(routedResource);
                    routedRequestList.Add(routedRequest);
                    routedResource.Request = routedRequestList;
                    clrRoutedProject.RoutedResources = routedResourceList;
                    _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());

                }
                else if (action == 3)
                {
                    clrRoutedProject.RoutingAction = RoutingAction.Exclude;
                    routedResource.RoutedItemId = routedItemId;
                    routedResource.LastModifiedDateTime = modifiedDateRouted;
                    routedRequest.WorkgroupId = (int)workGroupId;
                    routedRequest.RequestStatus = RequestStatus.Rejected; // as serilizing giving error
                    routedRequest.LastModifiedDateTime = modifiedRequestDate;
                    routedResourceList.Add(routedResource);
                    routedRequestList.Add(routedRequest);
                    routedResource.Request = routedRequestList;
                    clrRoutedProject.RoutedResources = routedResourceList;
                    _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());

                }
                else if (action == 4)
                {
                    clrRoutedProject.RoutingAction = RoutingAction.Exclude;
                    routedResource.RoutedItemId = routedItemId;
                    routedResource.LastModifiedDateTime = modifiedDateRouted;
                    routedRequest.WorkgroupId = (int)workGroupId;
                    routedRequest.RequestStatus = RequestStatus.Rejected; // as serilizing giving error
                    routedRequest.LastModifiedDateTime = modifiedRequestDate;
                    routedResourceList.Add(routedResource);
                    routedRequestList.Add(routedRequest);
                    routedResource.Request = routedRequestList;
                    clrRoutedProject.RoutedResources = routedResourceList;
                    _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());

                }
                else if (action == 5)
                {
                    clrRoutedProject.RoutingAction = RoutingAction.Include;
                    routedResource.RoutedItemId = routedItemId;
                    routedResource.LastModifiedDateTime = modifiedDateRouted;
                    routedRequest.WorkgroupId = (int)workGroupId;
                    routedRequest.RequestStatus = RequestStatus.Rejected; // as serilizing giving error
                    routedRequest.LastModifiedDateTime = modifiedRequestDate;
                    routedResourceList.Add(routedResource);
                    routedRequestList.Add(routedRequest);
                    routedResource.Request = routedRequestList;
                    clrRoutedProject.RoutedResources = routedResourceList;
                    _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());

                }
                else if (action == 6)
                {
                    clrRoutedProject.RoutingAction = RoutingAction.Reminders;
                    routedResource.RoutedItemId = routedItemId;
                    routedResource.LastModifiedDateTime = modifiedDateRouted;
                    routedRequest.WorkgroupId = (int)workGroupId;
                    routedRequest.RequestId = requestId;
                    routedRequest.LastModifiedDateTime = modifiedRequestDate;
                    routedRequest.RequestStatus = RequestStatus.Waiting; // as serilizing giving error
                    routedResourceList.Add(routedResource);
                    routedRequestList.Add(routedRequest);
                    routedResource.Request = routedRequestList;
                    clrRoutedProject.RoutedResources = routedResourceList;
                    _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());
                }

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new { Result = "Error", ex.Message });
            }
        }


        public bool EnableDisblePushToR2Btn(ClearanceProjectModel clrProjectModel)
        {
            bool enablePushToR2 = false;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<ClearanceRelease> lstReleaseToPush = new List<ClearanceRelease>();
                if (clrProjectModel.RegularProjectDetails.ObjRelease != null)
                {
                    lstReleaseToPush = clrProjectModel.RegularProjectDetails.ObjRelease.Where(i => i.ReleaseId > 0).ToList();

                    if (lstReleaseToPush != null && lstReleaseToPush.Count > 0)
                        lstReleaseToPush = lstReleaseToPush.Where(i => i.isPushedToR2 == null || i.isPushedToR2 == "" || i.isPushedToR2 == "N").ToList();

                    if (lstReleaseToPush.Count > 0)
                    {
                        var upcExist = lstReleaseToPush.Where(i => i.Upc != null || i.Upc != "").FirstOrDefault();
                        if (upcExist != null && !string.IsNullOrEmpty(upcExist.Upc))
                        {
                            ViewBag.PushToR2NewReleaseExist = upcExist.Upc;
                            enablePushToR2 = true;
                        }
                        else if (clrProjectModel.RegularProjectDetails.ThirdParty)
                        {
                            ViewBag.PushToR2NewReleaseExist = "exist";
                            enablePushToR2 = true;
                        }

                    }
                }

                // or if user has saved only resources, then check if linking has already been done & other resource check
                // if there are releases to push, call xml generating method or update GCS db for releases that push has been initiated
                if (enablePushToR2 == false && clrProjectModel.RegularProjectDetails.R2_Project_Id != 0 && clrProjectModel.RegularProjectDetails.R2_Project_Id != null)
                {
                    List<ClearanceResource> lstResourceToPush = new List<ClearanceResource>();
                    if (clrProjectModel.RegularProjectDetails.ClearanceResource != null)
                    {
                        lstResourceToPush = clrProjectModel.RegularProjectDetails.ClearanceResource.Where(i => i.ResourceId > 0).ToList();

                        if (lstResourceToPush != null && lstResourceToPush.Count > 0)
                            lstResourceToPush = lstResourceToPush.Where(i => i.isPushedToR2 == null || i.isPushedToR2 == "" || i.isPushedToR2 == "N").ToList();

                        // if there is any resource having status cancelled or rejected
                        if (lstResourceToPush != null && lstResourceToPush.Count > 0)
                        {
                            int iresourceCount = lstResourceToPush.Count;
                            int iresourceNullCount = lstResourceToPush.Count(i => i.Resource_Status == "cancelled" || i.Resource_Status == "rejected" || i.R2_ResourceId == 0);
                            if (iresourceCount == iresourceNullCount)
                            {
                                // there are no resources that can be pushed
                                enablePushToR2 = false;
                            }
                            else
                            {
                                enablePushToR2 = true;
                                ViewBag.PushToR2NewReleaseExist = "exist";
                            }
                        }
                        else
                        {
                            enablePushToR2 = false;
                        }
                    }
                    else
                    {
                        enablePushToR2 = false;
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return enablePushToR2;
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        //List of controls to show tooltip on Regular page on Project Information tabl and Request Type tab
        private List<string> RegularListTooltipControl(string IsReleaseNewOrExisting)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<string> arrayList = null;
                arrayList = new List<string>();
                arrayList.Add("CurrencyList");
                arrayList.Add("chkSensitiveExplotation");
                arrayList.Add("chkOneStop");
                arrayList.Add("chk3rdParty");
                arrayList.Add("chkTV");
                arrayList.Add("chkRadio");
                arrayList.Add("chkOthers");
                arrayList.Add("chkAdditionalMailOrder");
                arrayList.Add("chkIntroductoryUse");
                arrayList.Add("chkPartwork");
                arrayList.Add("chkKiosk");
                arrayList.Add("chkMailOrder");
                arrayList.Add("chkInternet");
                arrayList.Add("chkDirectResponse");
                arrayList.Add("chkEducational");
                arrayList.Add("chkPremium");
                arrayList.Add("chkGiveAwayFreeOfCharge");
                arrayList.Add("chkOther");
                arrayList.Add("btnManageTerritories");
                arrayList.Add("cboRequestPriceList");
                arrayList.Add("cmbManByUMG");
                arrayList.Add("txtDurationFrom");
                arrayList.Add("txtDurationTo");
                arrayList.Add("txtTVBudget");
                arrayList.Add("txtTVBudgetUSD");
                arrayList.Add("txtTVProdCost");
                arrayList.Add("txtRdoBudgetUSD");
                arrayList.Add("txtRdoBudget");
                arrayList.Add("txtOthrBudget");
                arrayList.Add("txtOthrBudgetUSD");
                arrayList.Add("txtOthrProdCost");
                arrayList.Add("btnManageTerritories_1");
                arrayList.Add("txtPSaleDate");
                arrayList.Add("txtPRevDate");
                arrayList.Add("txtDSaleDate");
                arrayList.Add("txtDRevDate");
                arrayList.Add("txtRdoProdCost");


                //Release Regular tab
                if (IsReleaseNewOrExisting.Equals("New"))
                {
                    arrayList.Add("ddlRegPriceLevel_");
                    arrayList.Add("ddlTVPriceLevel_");
                    arrayList.Add("chkRegDeviatedICLA_");
                    arrayList.Add("chkTVDeviatedICLA_");
                    arrayList.Add("chkClubDeviatedICLA_");
                    arrayList.Add("chkPromoDeviatedICLA_");
                    arrayList.Add("chkNonTradDeviatedICLA_");
                    arrayList.Add("ddlRegICLALevel_");
                    arrayList.Add("ddlTVICLALevel_");
                    arrayList.Add("ddlClubICLALevel_");
                    arrayList.Add("ddlPromoICLALevel_");
                    arrayList.Add("ddlNonICLALevel_");
                    arrayList.Add("txtExPPD_");
                    arrayList.Add("txtEstRetail_");
                    arrayList.Add("txtInvPrice_");
                    arrayList.Add("txtSellPrice_");
                    arrayList.Add("txtICLAcc_");
                    arrayList.Add("txtDeemedPPD_");
                    arrayList.Add("txtResourceFee_");
                    arrayList.Add("rdoNonTrad1");
                    arrayList.Add("rdoNonTrad2");
                }
                else
                {
                    arrayList.Add("ddlPriceLevel-Regular");
                    arrayList.Add("ddlPriceLevel-TVRadio");
                    arrayList.Add("ddlPriceLevel-PriceReduction");
                    arrayList.Add("ddlPriceLevel-Club");
                    arrayList.Add("ddlPriceLevel-Promotional");
                    arrayList.Add("chkIsDeviatedICLALevel-Regular");
                    arrayList.Add("chkIsDeviatedICLALevel-TVRadio");
                    arrayList.Add("chkIsDeviatedICLALevel-PriceReduction");
                    arrayList.Add("chkIsDeviatedICLALevel-Club");
                    arrayList.Add("chkIsDeviatedICLALevel-Promotional");
                    arrayList.Add("chkIsDeviatedICLALevel-Non");
                    arrayList.Add("ddlDevICLALevel-Non");
                    arrayList.Add("ddlDevICLALevel-Regular");
                    arrayList.Add("ddlDevICLALevel-TVRadio");
                    arrayList.Add("ddlDevICLALevel-PriceReduction");
                    arrayList.Add("ddlDevICLALevel-Club");
                    arrayList.Add("ddlDevICLALevel-Promotional");
                    arrayList.Add("txtExactPPD-TVRadio");
                    arrayList.Add("txtEstRetail-TVRadio");
                    arrayList.Add("txtInvoicePrice-Non");
                    arrayList.Add("txtSellingPrice-Non");
                    arrayList.Add("txtAccounting-Non");
                    arrayList.Add("txtDeemedPPD-Non");
                    arrayList.Add("txtResourceFee-Non");
                    arrayList.Add("rdoNonTrad1");
                    arrayList.Add("rdoNonTrad2");
                }

                LoggerFactory.LogWriter.MethodExit();

                return arrayList;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        // this function checks for properties in checkItems and their values whether it is changed or not
        public static bool PublicInstancePropertiesEqual<T>(T self, T to, List<string> checkItems) where T : class
        {
            try
            {
                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    List<string> includeList = new List<string>();
                    includeList = checkItems;
                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {

                        string propertyCheck = type.Name + "." + pi.Name;
                        if (includeList.Contains(propertyCheck))
                        {

                            if (IsPrimitive(type.GetProperty(pi.Name)))
                            {
                                string selfValue = type.GetProperty(pi.Name).GetValue(self, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(self, null).ToString();
                                string toValue = type.GetProperty(pi.Name).GetValue(to, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(to, null).ToString();

                                if (selfValue != toValue)
                                {
                                    if (propertyCheck == "RequestTypeRegular.AdditionalMailOrder")
                                    {
                                        if (selfValue == "True")
                                        {
                                            return false;
                                        }
                                    }
                                    else if (propertyCheck == "RequestTypeRegular.IntroductoryUse")
                                    {
                                        if (selfValue == "True")
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }

                            }
                        }
                    }
                    return true;
                }

                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PublicInstancePropertiesEqualRequestType<T>(T self, T to, List<string> checkItems, ref List<int> modifiedSalesChannelIds) where T : class
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string ResubmitReason = string.Empty;

                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    List<string> includeList = new List<string>();
                    includeList = checkItems;
                    string[] TVRadioBreakFileds = new string[20];
                    string[] NonTraditionalFileds = new string[10];
                    checkItems.CopyTo(4, TVRadioBreakFileds, 0, 18);
                    if (!checkItems.Contains("RequestTypeRegular.RequestedPriceLevel_ID"))
                    {
                        checkItems.CopyTo(24, NonTraditionalFileds, 0, 10);
                    }
                    else
                    {
                        checkItems.CopyTo(25, NonTraditionalFileds, 0, 10);
                    }

                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {

                        string propertyCheck = type.Name + "." + pi.Name;
                        if (includeList.Contains(propertyCheck))
                        {

                            if (IsPrimitive(type.GetProperty(pi.Name)))
                            {

                                string selfValue = type.GetProperty(pi.Name).GetValue(self, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(self, null).ToString();
                                string toValue = type.GetProperty(pi.Name).GetValue(to, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(to, null).ToString();


                                if (selfValue != toValue)
                                {

                                    if (propertyCheck == "RequestTypeRegular.AdditionalMailOrder")
                                    {
                                        if (selfValue == "True")
                                        {
                                            if (string.IsNullOrEmpty(ResubmitReason))
                                                ResubmitReason = propertyCheck;
                                            else
                                            {
                                                ResubmitReason = string.Format("{0},{1}", ResubmitReason, propertyCheck);
                                            }
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Club));
                                        }
                                    }
                                    else if (propertyCheck == "RequestTypeRegular.IntroductoryUse")
                                    {
                                        if (selfValue == "True")
                                        {
                                            if (string.IsNullOrEmpty(ResubmitReason))
                                                ResubmitReason = propertyCheck;
                                            else
                                            {
                                                ResubmitReason = string.Format("{0},{1}", ResubmitReason, propertyCheck);
                                            }
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Club));
                                        }
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(ResubmitReason))
                                            ResubmitReason = propertyCheck;
                                        else
                                        {
                                            ResubmitReason = string.Format("{0},{1}", ResubmitReason, propertyCheck);
                                        }

                                        if (TVRadioBreakFileds.Contains(propertyCheck))
                                        {
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVPhysical));
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVALaCarte));
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVSubscription));
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRealTone));
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRingBackTone));
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVStreaming));
                                        }
                                        else if (propertyCheck.Equals("RequestTypeRegular.RequestedPriceLevel_ID"))
                                        {
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.PriceReduction));
                                        }
                                        else if (NonTraditionalFileds.Contains(propertyCheck))
                                        {
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                        else if (propertyCheck == "RequestTypeRegular.Physical")
                                        {
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Physical));
                                        }
                                        else if (propertyCheck == "RequestTypeRegular.Digital")
                                        {
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Digitial));
                                        }
                                    }

                                }

                            }

                        }
                    }
                    modifiedSalesChannelIds = modifiedSalesChannelIds.Distinct().ToList();
                    return ResubmitReason;
                }

                LoggerFactory.LogWriter.MethodExit();

                return ResubmitReason;
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public string PublicInstancePropertiesEqualScopeIsTV<T>(T self, T to, List<string> checkItems) where T : class
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string ResubmitReason = string.Empty;

                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    List<string> includeList = new List<string>();
                    includeList = checkItems;

                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {

                        string propertyCheck = type.Name + "." + pi.Name;
                        if (includeList.Contains(propertyCheck))
                        {

                            if (IsPrimitive(type.GetProperty(pi.Name)))
                            {

                                string selfValue = type.GetProperty(pi.Name).GetValue(self, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(self, null).ToString();
                                string toValue = type.GetProperty(pi.Name).GetValue(to, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(to, null).ToString();


                                if (selfValue != toValue)
                                {
                                    if (string.IsNullOrEmpty(ResubmitReason))
                                        ResubmitReason = pi.Name;
                                    else
                                        ResubmitReason = string.Format("{0},{1}", ResubmitReason, pi.Name);

                                }

                            }

                        }
                    }

                    return ResubmitReason;
                }

                LoggerFactory.LogWriter.MethodExit();

                return ResubmitReason;
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public string PublicInstancePropertiesEqualScopeIsRadio<T>(T self, T to, List<string> checkItems) where T : class
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string ResubmitReason = string.Empty;

                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    List<string> includeList = new List<string>();
                    includeList = checkItems;

                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {

                        string propertyCheck = type.Name + "." + pi.Name;
                        if (includeList.Contains(propertyCheck))
                        {

                            if (IsPrimitive(type.GetProperty(pi.Name)))
                            {

                                string selfValue = type.GetProperty(pi.Name).GetValue(self, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(self, null).ToString();
                                string toValue = type.GetProperty(pi.Name).GetValue(to, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(to, null).ToString();

                                if (selfValue != toValue)
                                {
                                    if (string.IsNullOrEmpty(ResubmitReason))
                                        ResubmitReason = pi.Name;
                                    else
                                        ResubmitReason = string.Format("{0},{1}", ResubmitReason, pi.Name);

                                }

                            }

                        }
                    }

                    return ResubmitReason;
                }

                LoggerFactory.LogWriter.MethodExit();

                return ResubmitReason;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        public string PublicInstancePropertiesEqualScopeOthersICLA<T>(T self, T to, List<string> checkItems) where T : class
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string ResubmitReason = string.Empty;

                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    List<string> includeList = new List<string>();
                    includeList = checkItems;

                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {

                        string propertyCheck = type.Name + "." + pi.Name;
                        if (includeList.Contains(propertyCheck))
                        {

                            if (IsPrimitive(type.GetProperty(pi.Name)))
                            {

                                string selfValue = type.GetProperty(pi.Name).GetValue(self, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(self, null).ToString();
                                string toValue = type.GetProperty(pi.Name).GetValue(to, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(to, null).ToString();

                                if (selfValue != toValue)
                                {
                                    if (string.IsNullOrEmpty(ResubmitReason))
                                        ResubmitReason = pi.Name;
                                    else
                                        ResubmitReason = string.Format("{0},{1}", ResubmitReason, pi.Name);

                                }

                            }

                        }
                    }

                    return ResubmitReason;
                }

                LoggerFactory.LogWriter.MethodExit();

                return ResubmitReason;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public string PublicInstancePropertiesEqualRegular<T>(T self, T to, List<string> checkItems) where T : class
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string ResubmitReason = string.Empty;

                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    List<string> includeList = new List<string>();
                    includeList = checkItems;

                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {

                        string propertyCheck = type.Name + "." + pi.Name;
                        if (includeList.Contains(propertyCheck))
                        {

                            if (IsPrimitive(type.GetProperty(pi.Name)))
                            {

                                string selfValue = type.GetProperty(pi.Name).GetValue(self, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(self, null).ToString();
                                string toValue = type.GetProperty(pi.Name).GetValue(to, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(to, null).ToString();


                                if (selfValue != toValue)
                                {
                                    if (string.IsNullOrEmpty(ResubmitReason))
                                        ResubmitReason = pi.Name;
                                    else
                                        ResubmitReason = string.Format("{0},{1}", ResubmitReason, pi.Name);

                                }

                            }

                        }
                    }

                    return ResubmitReason;
                }

                LoggerFactory.LogWriter.MethodExit();

                return ResubmitReason;
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public static bool PublicInstancePropertiesEqual1<T>(T self, T to, Dictionary<string, string> checkItems, out string ResubmitReason, string finalResubmitReasonComments) where T : class
        {
            ResubmitReason = string.Empty;
            bool flag = true;
            try
            {
                
                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    Dictionary<string, string> includeList = new Dictionary<string, string>();
                    includeList = checkItems;
                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {

                        string propertyCheck = type.Name + "." + pi.Name;
                        if (includeList.ContainsKey(propertyCheck))
                        {

                            if (IsPrimitive(type.GetProperty(pi.Name)))
                            {

                                string selfValue = type.GetProperty(pi.Name).GetValue(self, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(self, null).ToString();
                                string toValue = type.GetProperty(pi.Name).GetValue(to, null) == null ? string.Empty : type.GetProperty(pi.Name).GetValue(to, null).ToString();

                                if (selfValue != toValue)
                                {
                                    flag = false;

                                    if (includeList[propertyCheck] != "Request Type")
                                    {
                                        if (string.IsNullOrEmpty(ResubmitReason))
                                            ResubmitReason = includeList[propertyCheck];
                                        else
                                            ResubmitReason = string.Format("{0},{1}", ResubmitReason, includeList[propertyCheck]);
                                    }
                                    else
                                    {
                                        if (!finalResubmitReasonComments.Contains("Request Type") && !ResubmitReason.Contains("Request Type"))
                                        {
                                            if (string.IsNullOrEmpty(ResubmitReason))
                                                ResubmitReason = includeList[propertyCheck];
                                            else
                                                ResubmitReason = string.Format("{0},{1}", ResubmitReason, includeList[propertyCheck]);
                                        }
                                    }

                                }

                            }

                        }
                    }
                    return flag;
                }
                return self == to;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        // check whether property of class is primitive or not
        private static bool IsPrimitive(PropertyInfo type)
        {
            bool flag = false;

            if (type.PropertyType.FullName == typeof(bool).FullName ||
                type.PropertyType.FullName == typeof(Boolean).FullName ||
                type.PropertyType.FullName == typeof(string).FullName ||
                type.PropertyType.FullName == typeof(int).FullName ||
                type.PropertyType.FullName == typeof(decimal).FullName ||
                type.PropertyType.FullName == typeof(float).FullName ||
                type.PropertyType.FullName == typeof(DateTime).FullName ||
                type.PropertyType.FullName == typeof(Decimal?).FullName ||
                type.PropertyType.FullName == typeof(int?).FullName ||
                type.PropertyType.FullName == typeof(float?).FullName)
            {
                flag = true;
            }

            return flag;
        }

        private string CompareConfigurationNewReleaseSubmitReasons(ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                StringBuilder ProjectLevelChange = new StringBuilder();
                ClearanceProjectModel Oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId];

                List<ClearanceRelease> NewModelRelease = model.RegularProjectDetails.ObjRelease;
                List<ClearanceRelease> OldModelRelease = Oldmodel.RegularProjectDetails.ObjRelease;

                LoggerFactory.LogWriter.Info("Compare Configuration New Release Method Initiated");

                // List of ClearanceResource
                if (NewModelRelease != null && OldModelRelease != null)
                {
                    for (int i = 0; i < OldModelRelease.Count; i++)
                    {
                        bool flagConfigChange = false;
                        ClearanceRelease lstReleaseAdd = new ClearanceRelease();

                        var newGlobalClearanceFlag = (from newlist in NewModelRelease where newlist.ReleaseId == OldModelRelease[i].ReleaseId select newlist).ToList();
                        if (newGlobalClearanceFlag != null && newGlobalClearanceFlag.Count > 0)
                        {
                            //check for configuration change
                            if (Oldmodel.RegularProjectDetails.ReleaseNewOrExisting == Constants.ReleaseType.Exist)
                            {
                                if (OldModelRelease[i].Configuration != newGlobalClearanceFlag[0].Configuration)
                                {
                                    flagConfigChange = true;
                                }
                            }
                            else
                            {
                                if (OldModelRelease[i].ConfigId != newGlobalClearanceFlag[0].ConfigId)
                                {
                                    flagConfigChange = true;
                                }
                            }
                        }
                        if (flagConfigChange)
                        {
                            if (ProjectLevelChange.Length > 0) ProjectLevelChange.Append(",");
                            ProjectLevelChange.Append(lstReleaseAdd.ReleaseId);
                            ProjectLevelChange.Append(":");
                            ProjectLevelChange.Append("Configuration");
                        }

                    }
                }
                LoggerFactory.LogWriter.Info("Successfully Called Compare Configuration New Release");
                LoggerFactory.LogWriter.MethodExit();

                return ProjectLevelChange.ToString();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private void CompareConfigurationNewRelease(ClearanceProjectModel model, FormCollection collection, List<List<int>> lstCombinedDataList)
        {
            try
            {

                LoggerFactory.LogWriter.MethodStart();
                
                ClearanceProjectModel oldmodel = new ClearanceProjectModel();

                oldmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId];

                List<ClearanceRelease> NewModelRelease = null;
                NewModelRelease = new List<ClearanceRelease>();
                NewModelRelease = model.RegularProjectDetails.ObjRelease;

                List<ClearanceRelease> oldModelRelease = null;
                oldModelRelease = new List<ClearanceRelease>();
                oldModelRelease = oldmodel.RegularProjectDetails.ObjRelease;

                List<int> lstReleaseChanged = new List<int>();

                // List of ClearanceResource
                if (NewModelRelease != null && oldModelRelease != null)
                {
                    for (int i = 0; i < oldModelRelease.Count; i++)
                    {
                        var newGlobalClearanceFlag = (from newlist in NewModelRelease where newlist.ReleaseId == oldModelRelease[i].ReleaseId select newlist).ToList();
                        if (newGlobalClearanceFlag != null && newGlobalClearanceFlag.Count > 0)
                        {
                            //check for configuration change
                            if (oldmodel.RegularProjectDetails.ReleaseNewOrExisting == "Exist")
                            {
                                if (oldModelRelease[i].Configuration != newGlobalClearanceFlag[0].Configuration)
                                {
                                    ClearanceRelease lstReleaseAdd = new ClearanceRelease();
                                    lstReleaseAdd = (from newlist in NewModelRelease
                                                     where newlist.ReleaseId == oldModelRelease[i].ReleaseId
                                                     select newlist).FirstOrDefault();
                                    lstReleaseChanged.Add(Convert.ToInt32(lstReleaseAdd.ReleaseId));
                                    continue;
                                }
                            }
                            else
                            {
                                if (oldModelRelease[i].ConfigId != newGlobalClearanceFlag[0].ConfigId)
                                {
                                    ClearanceRelease lstReleaseAdd = new ClearanceRelease();
                                    lstReleaseAdd = (from newlist in NewModelRelease
                                                     where newlist.ReleaseId == oldModelRelease[i].ReleaseId
                                                     select newlist).FirstOrDefault();
                                    lstReleaseChanged.Add(Convert.ToInt32(lstReleaseAdd.ReleaseId));
                                    continue;
                                }
                            }
                        }
                    }
                }

                lstCombinedDataList.Add(lstReleaseChanged);

                LoggerFactory.LogWriter.MethodExit();
                LoggerFactory.LogWriter.Debug("Successfully Called Compare Configuration New Release");
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        private void CompareNewlyAddedSalesChannels(ClearanceProjectModel model, FormCollection collection, List<List<int>> lstCombinedDataList)
        {
            LoggerFactory.LogWriter.MethodStart();

            List<int> arrSalesChannelId = new List<int>();
            List<int> arrRemovedSalesChannelId = new List<int>();
            List<int> arrSpecialCaseRegular = new List<int>();

            ViewBag.Newmodelproject = model;
            ClearanceProjectModel oldmodel = new ClearanceProjectModel();
            oldmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId];

            bool checkSpecialCase = true;

            if (oldmodel != null && model != null)
            {

                checkSpecialCase = CompareSpecialCaseRegular(model, oldmodel);

                if (!checkSpecialCase)
                {
                    arrSpecialCaseRegular.Add(1);

                    bool IsResubmission = IsNewlyAddedCountryAfterSubmit(model.RegularProjectDetails.Territories, oldmodel.RegularProjectDetails.Territories);
                    if (!IsResubmission) IsResubmission = IsNotTheSameCountryForCompany(model.RegularProjectDetails.RequesterCompanyId, oldmodel.RegularProjectDetails.RequesterCompanyId);

                    if (IsResubmission)
                        arrSpecialCaseRegular.Add(1);
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.RegularRetail != model.RegularProjectDetails.ScopeAndRequestType.RegularRetail)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.RegularRetail == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Regular));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Regular));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.Club != model.RegularProjectDetails.ScopeAndRequestType.Club)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.Club == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Club));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Club));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.NonTraditional != model.RegularProjectDetails.ScopeAndRequestType.NonTraditional)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.NonTraditional == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.Promotional != model.RegularProjectDetails.ScopeAndRequestType.Promotional)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.Promotional == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Promotional));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.Promotional));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.PriceReduction != model.RegularProjectDetails.ScopeAndRequestType.PriceReduction)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.PriceReduction == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.PriceReduction));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.PriceReduction));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.SalesChannelPhysical != model.RegularProjectDetails.ScopeAndRequestType.SalesChannelPhysical)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.SalesChannelPhysical == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVPhysical));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVPhysical));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.SalesChannelAlaCarteDownload != model.RegularProjectDetails.ScopeAndRequestType.SalesChannelAlaCarteDownload)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.SalesChannelAlaCarteDownload == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVALaCarte));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVALaCarte));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.SalesChannelSubscriptionDownload != model.RegularProjectDetails.ScopeAndRequestType.SalesChannelSubscriptionDownload)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.SalesChannelSubscriptionDownload == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVSubscription));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVSubscription));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRealTones != model.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRealTones)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRealTones == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRealTone));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRealTone));
                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRingBackTones != model.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRingBackTones)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.SalesChannelMobileRingBackTones == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRingBackTone));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRingBackTone));

                }

                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.Streaming != model.RegularProjectDetails.ScopeAndRequestType.Streaming)
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.Streaming == true)
                        arrSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVStreaming));
                    else
                        arrRemovedSalesChannelId.Add(Convert.ToInt16(General.RegularSalesChannelId.TVStreaming));
                }

            }

            lstCombinedDataList.Add(arrSalesChannelId);
            lstCombinedDataList.Add(arrRemovedSalesChannelId);
            lstCombinedDataList.Add(arrSpecialCaseRegular);

            LoggerFactory.LogWriter.MethodExit();

        }

        [HttpPost]
        public JsonResult GetNewRegularProjectModel(ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                bool Flag = true;
                ModelState.Clear();
                List<List<int>> LstCombinedDataList = new List<List<int>>();
                List<int> LstProjectLevelChange = new List<int>();
                List<string> ArrayList;
                List<string> OldRequestsArrayList = new List<string>();
                List<int> ModifiedSalesChannelIds = new List<int>();

                //summary
                string ReasonProjectResubmissionToReturn = null;
                StringBuilder ReasonProjectResubmission = new StringBuilder();
                List<KeyValuePair<long, string>> ReasonReleaseResubmission = new List<KeyValuePair<long, string>>();
                List<KeyValuePair<long, string>> ReasonResourceResubmission = new List<KeyValuePair<long, string>>();


                ViewBag.Newmodelproject = model;
                ClearanceProjectModel Oldmodel = new ClearanceProjectModel();
                if (Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId] != null)
                    Oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId];
                else
                {
                    _IClearanceProjectModel.RegularProjectDetails = _IClearanceProjectRepository.GetRegularProjectDetails(Convert.ToInt32(model.RegularProjectDetails.ClrProjectId), getUserInfo());
                    Oldmodel.RegularProjectDetails = _IClearanceProjectModel.RegularProjectDetails;
                    Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId] = Oldmodel;
                }
                Session[Constants.Sessions.NewEntity + model.RegularProjectDetails.ClrProjectId] = model;

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);

                CompareNewlyAddedSalesChannels(model, collection, LstCombinedDataList);
                RemovePackageRequests(model, collection, LstCombinedDataList);

                //section for Regular project details
                ArrayList = new List<string>();
                ArrayList.Add("ClearanceRegularProject.Currency");
                ArrayList.Add("ClearanceRegularProject.SensitiveExplotation");
                ArrayList.Add("ClearanceRegularProject.OneStop");
                ArrayList.Add("ClearanceRegularProject.ThirdParty");

                if (model.RegularProjectDetails != null && Oldmodel.RegularProjectDetails != null)
                {
                    Flag = PublicInstancePropertiesEqual(model.RegularProjectDetails, Oldmodel.RegularProjectDetails, ArrayList);

                    // for collecting resubmission reasons *************************************************************************
                    ReasonProjectResubmission.Append(PublicInstancePropertiesEqualRegular(model.RegularProjectDetails, Oldmodel.RegularProjectDetails, ArrayList));
                    ReasonProjectResubmission = ReasonProjectResubmission.Replace("Explotation", "Exploitation");

                    //TV Section Of Regular Project
                    ArrayList.Add("TV.IsTV");
                    ArrayList.Add("TV.Budget");
                    ArrayList.Add("TV.BudgetInUSD");
                    ArrayList.Add("TV.ProductionCostOfCommercial");

                    //Radio Section Of Regular Project
                    ArrayList.Add("Radio.IsRadio");
                    ArrayList.Add("Radio.Budget");
                    ArrayList.Add("Radio.BudgetInUSD");
                    ArrayList.Add("Radio.ProductionCostOfCommercial");

                    //Other Section of Regular Project
                    ArrayList.Add("OthersICLA.IsOthers");
                    ArrayList.Add("OthersICLA.Budget");
                    ArrayList.Add("OthersICLA.BudgetInUSD");
                    ArrayList.Add("OthersICLA.ProductionCostOfCommercial");

                    //RequestTypeRegular Section Of Regular Project
                    ArrayList.Add("RequestTypeRegular.DurationFrom");
                    ArrayList.Add("RequestTypeRegular.DurationTo");

                    //Radio Break ICLA Section of RequestTypeRegular of Regular Project Section
                    ArrayList.Add("RequestTypeRegular.PhysicalSalesSplitSalesToDate");
                    ArrayList.Add("RequestTypeRegular.DigitalSalesSplitSalesToDate");
                    ArrayList.Add("RequestTypeRegular.PhysicalRevenueToDate");
                    ArrayList.Add("RequestTypeRegular.DigitalRevenueToDate");

                    //Price Reduction Section of RequestTypeRegular of Regular Project Section
                    if (!model.RegularProjectDetails.ReleaseNewOrExisting.Equals(Constants.ReleaseType.New) && model.RegularProjectDetails.ScopeAndRequestType.PriceReduction)
                    {
                        ArrayList.Add("RequestTypeRegular.RequestedPriceLevel_ID");  //22
                    }

                    //Club Section of RequestTypeRegular of Regular Project Section
                    ArrayList.Add("RequestTypeRegular.AdditionalMailOrder");
                    ArrayList.Add("RequestTypeRegular.IntroductoryUse");

                    //Non Traditional Section of Regular Project Section
                    ArrayList.Add("RequestTypeRegular.ManufacturedByUMG");  //25
                    ArrayList.Add("RequestTypeRegular.Partwork");
                    ArrayList.Add("RequestTypeRegular.Kiosk");
                    ArrayList.Add("RequestTypeRegular.MailOrder");
                    ArrayList.Add("RequestTypeRegular.Internet");
                    ArrayList.Add("RequestTypeRegular.DirectResponse");
                    ArrayList.Add("RequestTypeRegular.Educational");
                    ArrayList.Add("RequestTypeRegular.Premium");
                    ArrayList.Add("RequestTypeRegular.GiveAwayFreeCharge");
                    ArrayList.Add("RequestTypeRegular.Other");
                    ArrayList.Add("RequestTypeRegular.Physical");
                    ArrayList.Add("RequestTypeRegular.Digital");
                    OldRequestsArrayList.AddRange(ArrayList);
                    // for collecting resubmission reasons
                    if (model.RegularProjectDetails.ScopeAndRequestType.Club != Oldmodel.RegularProjectDetails.ScopeAndRequestType.Club)
                    {
                        OldRequestsArrayList.Remove("RequestTypeRegular.AdditionalMailOrder");
                        OldRequestsArrayList.Remove("RequestTypeRegular.IntroductoryUse");
                    }
                    if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA != Oldmodel.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                    {
                        OldRequestsArrayList.Remove("TV.IsTV");
                        OldRequestsArrayList.Remove("TV.Budget");
                        OldRequestsArrayList.Remove("TV.BudgetInUSD");
                        OldRequestsArrayList.Remove("TV.ProductionCostOfCommercial");
                        OldRequestsArrayList.Remove("Radio.IsRadio");
                        OldRequestsArrayList.Remove("Radio.Budget");
                        OldRequestsArrayList.Remove("Radio.BudgetInUSD");
                        OldRequestsArrayList.Remove("Radio.ProductionCostOfCommercial");
                        OldRequestsArrayList.Remove("OthersICLA.IsOthers");
                        OldRequestsArrayList.Remove("OthersICLA.Budget");
                        OldRequestsArrayList.Remove("OthersICLA.BudgetInUSD");
                        OldRequestsArrayList.Remove("OthersICLA.ProductionCostOfCommercial");
                        OldRequestsArrayList.Remove("RequestTypeRegular.DurationFrom");
                        OldRequestsArrayList.Remove("RequestTypeRegular.DurationTo");
                        OldRequestsArrayList.Remove("RequestTypeRegular.PhysicalSalesSplitSalesToDate");
                        OldRequestsArrayList.Remove("RequestTypeRegular.DigitalSalesSplitSalesToDate");
                        OldRequestsArrayList.Remove("RequestTypeRegular.PhysicalRevenueToDate");
                        OldRequestsArrayList.Remove("RequestTypeRegular.DigitalRevenueToDate");
                    }
                    if (model.RegularProjectDetails.ScopeAndRequestType.NonTraditional != Oldmodel.RegularProjectDetails.ScopeAndRequestType.NonTraditional)
                    {
                        OldRequestsArrayList.Remove("RequestTypeRegular.ManufacturedByUMG");  //25
                        OldRequestsArrayList.Remove("RequestTypeRegular.Partwork");
                        OldRequestsArrayList.Remove("RequestTypeRegular.Kiosk");
                        OldRequestsArrayList.Remove("RequestTypeRegular.MailOrder");
                        OldRequestsArrayList.Remove("RequestTypeRegular.Internet");
                        OldRequestsArrayList.Remove("RequestTypeRegular.DirectResponse");
                        OldRequestsArrayList.Remove("RequestTypeRegular.Educational");
                        OldRequestsArrayList.Remove("RequestTypeRegular.Premium");
                        OldRequestsArrayList.Remove("RequestTypeRegular.GiveAwayFreeCharge");
                        OldRequestsArrayList.Remove("RequestTypeRegular.Other");
                    }


                    string RequestTypeReasons = PublicInstancePropertiesEqualRequestType(model.RegularProjectDetails.ScopeAndRequestType, Oldmodel.RegularProjectDetails.ScopeAndRequestType, ArrayList, ref ModifiedSalesChannelIds);
                    if (!string.IsNullOrEmpty(RequestTypeReasons))
                    {
                        if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                        ReasonProjectResubmission.Append(RequestTypeReasons);
                    }

                    // for collecting resubmission reasons
                    if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA == Oldmodel.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                    {
                        if (model.RegularProjectDetails.ScopeAndRequestType.TV.IsTV)
                        {
                            string ScopeIsTV = PublicInstancePropertiesEqualScopeIsTV(model.RegularProjectDetails.ScopeAndRequestType.TV, Oldmodel.RegularProjectDetails.ScopeAndRequestType.TV, ArrayList);
                            if (!string.IsNullOrEmpty(ScopeIsTV))
                            {
                                if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                                ReasonProjectResubmission.Append(ScopeIsTV);
                            }
                        }

                        // for collecting resubmission reasons
                        if (model.RegularProjectDetails.ScopeAndRequestType.Radio.IsRadio)
                        {
                            string ScopeIsRadio = PublicInstancePropertiesEqualScopeIsRadio(model.RegularProjectDetails.ScopeAndRequestType.Radio, Oldmodel.RegularProjectDetails.ScopeAndRequestType.Radio, ArrayList);
                            if (!string.IsNullOrEmpty(ScopeIsRadio))
                            {
                                if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                                ReasonProjectResubmission.Append(ScopeIsRadio);
                            }
                        }

                        // for collecting resubmission reasons
                        if (model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.IsOthers)
                        {
                            string ScopeOthersICLA = PublicInstancePropertiesEqualScopeOthersICLA(model.RegularProjectDetails.ScopeAndRequestType.OthersICLA, Oldmodel.RegularProjectDetails.ScopeAndRequestType.OthersICLA, ArrayList);
                            if (!string.IsNullOrEmpty(ScopeOthersICLA))
                            {
                                if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                                ReasonProjectResubmission.Append(ScopeOthersICLA);
                            }
                        }

                        // for collecting resubmission reasons
                        if (!CompareRegularTerritory(model.RegularProjectDetails.ScopeAndRequestType.Territories, Oldmodel.RegularProjectDetails.ScopeAndRequestType.Territories))
                        {
                            if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                            ReasonProjectResubmission.Append("ScopeAndRequestType Territories");
                        }
                    }

                    // for collecting resubmission reasons territories change
                    if (!CompareRegularTerritory(model.RegularProjectDetails.Territories, Oldmodel.RegularProjectDetails.Territories))
                    {
                        if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                        ReasonProjectResubmission.Append("Project Territories");
                    }

                    // for collecting resubmission reasons company change
                    if (IsNotTheSameCountryForCompany(model.RegularProjectDetails.RequesterCompanyId, Oldmodel.RegularProjectDetails.RequesterCompanyId))
                    {
                        if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                        ReasonProjectResubmission.Append("Requesting Company");
                    }

                    // for collecting resubmission reasons release change
                    if (IsReleaseIncludedAPriceReduction(model, Oldmodel))
                    {
                        if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                        ReasonProjectResubmission.Append("Release Change");
                    }

                    // for collecting resubmission reasons
                    if (model.RegularProjectDetails.ReleaseNewOrExisting == Constants.ReleaseType.New)
                    {
                        string ProjectLevelReleaseReasons = CompareRegularReleaseSubmitReasons(model, collection, ReasonReleaseResubmission, ref ModifiedSalesChannelIds);
                        if (!string.IsNullOrEmpty(ProjectLevelReleaseReasons))
                        {
                            if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                            ReasonProjectResubmission.Append(ProjectLevelReleaseReasons);
                        }

                        string ProjectLevelReleaseConfig = CompareConfigurationNewReleaseSubmitReasons(model, collection);
                        if (!string.IsNullOrEmpty(ProjectLevelReleaseConfig))
                        {
                            if (ReasonProjectResubmission.Length > 0) ReasonProjectResubmission.Append(',');
                            ReasonProjectResubmission.Append(ProjectLevelReleaseConfig);
                        }
                    }

                    if (!Flag)
                    {
                        LstProjectLevelChange.Add(0);
                        LstCombinedDataList.Add(LstProjectLevelChange);
                        LoggerFactory.LogWriter.MethodExit();

                        ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                        return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                    }
                }


                Flag = PublicInstancePropertiesEqual(model.RegularProjectDetails.ScopeAndRequestType, Oldmodel.RegularProjectDetails.ScopeAndRequestType, OldRequestsArrayList);
                if (!Flag)
                {
                    LstProjectLevelChange.Add(0);
                    LstCombinedDataList.Add(LstProjectLevelChange);
                    LoggerFactory.LogWriter.MethodExit();

                    ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                    return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                }

                if (model.RegularProjectDetails.ScopeAndRequestType.TV.IsTV)
                {
                    Flag = PublicInstancePropertiesEqual(model.RegularProjectDetails.ScopeAndRequestType.TV, Oldmodel.RegularProjectDetails.ScopeAndRequestType.TV, OldRequestsArrayList);
                    if (!Flag)
                    {
                        LstProjectLevelChange.Add(0);
                        LstCombinedDataList.Add(LstProjectLevelChange);
                        LoggerFactory.LogWriter.MethodExit();

                        ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                        return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                    }
                }

                if (model.RegularProjectDetails.ScopeAndRequestType.Radio.IsRadio)
                {

                    Flag = PublicInstancePropertiesEqual(model.RegularProjectDetails.ScopeAndRequestType.Radio, Oldmodel.RegularProjectDetails.ScopeAndRequestType.Radio, OldRequestsArrayList);
                    if (!Flag)
                    {
                        LstProjectLevelChange.Add(0);
                        LstCombinedDataList.Add(LstProjectLevelChange);
                        LoggerFactory.LogWriter.MethodExit();

                        ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                        return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                    }
                }

                if (model.RegularProjectDetails.ScopeAndRequestType.OthersICLA.IsOthers)
                {

                    Flag = PublicInstancePropertiesEqual(model.RegularProjectDetails.ScopeAndRequestType.OthersICLA, Oldmodel.RegularProjectDetails.ScopeAndRequestType.OthersICLA, OldRequestsArrayList);
                    if (!Flag)
                    {
                        LstProjectLevelChange.Add(0);
                        LstCombinedDataList.Add(LstProjectLevelChange);
                        LoggerFactory.LogWriter.MethodExit();

                        ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                        return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                    }
                }

                Flag = !IsNecessaryResubmissionForCompanyOrTerritory(model.RegularProjectDetails, Oldmodel.RegularProjectDetails);
                if (!Flag)
                {
                    LstProjectLevelChange.Add(0);
                    LstCombinedDataList.Add(LstProjectLevelChange);
                    LoggerFactory.LogWriter.MethodExit();

                    ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                    return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                }

                Flag = !IsReleaseIncludedAPriceReduction(model, Oldmodel);
                if (!Flag)
                {
                    LstProjectLevelChange.Add(0);
                    LstCombinedDataList.Add(LstProjectLevelChange);
                    LoggerFactory.LogWriter.MethodExit();

                    ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                    return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                }

                //Compare for Territory for Request Type Regular Tab
                if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA == Oldmodel.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                {
                    Flag = CompareRegularTerritory(model.RegularProjectDetails.ScopeAndRequestType.Territories, Oldmodel.RegularProjectDetails.ScopeAndRequestType.Territories);
                }
                if (!Flag)
                {
                    LstProjectLevelChange.Add(0);
                    LstCombinedDataList.Add(LstProjectLevelChange);
                    LoggerFactory.LogWriter.MethodExit();

                    ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                    return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                }
                if (model.RegularProjectDetails.ReleaseNewOrExisting == Constants.ReleaseType.New)
                {

                    List<List<int>> LstNewReleaseData = new List<List<int>>();
                    CompareRegularRelease(model, collection, LstNewReleaseData);

                    if (LstNewReleaseData[0].Count > 0)
                        Flag = false;

                    if (!Flag)
                    {
                        LstProjectLevelChange.Add(0);
                        LstCombinedDataList.Add(LstProjectLevelChange);
                        LoggerFactory.LogWriter.MethodExit();

                        ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                        return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
                    }
                }

                LstProjectLevelChange.Add(-1);
                LstCombinedDataList.Add(LstProjectLevelChange);

                if (Flag)
                {

                    //BEGIN - RESOURCE LEVEL
                    CompareRegularResourceSubmitReasons(model, collection, ReasonResourceResubmission);
                    CompareRegularOtherResourceEntitySubmitReasons(model, ReasonResourceResubmission);

                    //BEGIN - RELEASE LEVEL
                    CompareRegularReleaseSubmitReasons(model, collection, ReasonReleaseResubmission, ref ModifiedSalesChannelIds);

                    //BEGIN - Other types of resubmittion
                    CompareRegularResource(model, collection, LstCombinedDataList);
                    CompareRegularOtherResourceEntity(model, LstCombinedDataList);
                    CompareRegularRelease(model, collection, LstCombinedDataList);
                    CompareConfigurationNewRelease(model, collection, LstCombinedDataList);
                }

                LoggerFactory.LogWriter.MethodExit();

                ReasonProjectResubmissionToReturn = ReasonProjectResubmission.ToString();
                return Json(new { LstCombinedDataList, ModifiedSalesChannelIds, ReasonProjectResubmissionToReturn, ReasonReleaseResubmission, ReasonResourceResubmission });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Is Necessary Resubmission For Change In Company Or Territory
        /// check if you have change in territory or resquest company
        /// </summary>
        /// <param name="newProject"></param>
        /// <param name="oldProject"></param>
        /// <returns>bool</returns>
        private bool IsNecessaryResubmissionForCompanyOrTerritory(ClearanceRegularProject newProject, ClearanceRegularProject oldProject)
        {
            LoggerFactory.LogWriter.MethodStart();

            bool IsNecessaryResubmission = false;

            if (IsNotTheSameCountryForCompany(newProject.RequesterCompanyId, oldProject.RequesterCompanyId))
                IsNecessaryResubmission = true;
            else if (!CompareRegularTerritory(newProject.Territories, oldProject.Territories))
                IsNecessaryResubmission = true;

            LoggerFactory.LogWriter.MethodExit();

            return IsNecessaryResubmission;
        }

        //function to check if some thing is changed in Release for Regular Project
        private string CompareRegularReleaseSubmitReasons(ClearanceProjectModel model, FormCollection collection, List<KeyValuePair<long, string>> lstReleaseReasons, ref List<int> modifiedSalesChannelIds)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                StringBuilder ProjectLevelReasonsToReturn = new StringBuilder();

                ClearanceProjectModel Oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId];

                List<ClearanceRelease> NewModelRelease = model.RegularProjectDetails.ObjRelease;
                List<ClearanceRelease> OldModelRelease = Oldmodel.RegularProjectDetails.ObjRelease;

                LoggerFactory.LogWriter.Info("Compare Regular Release Method Initiated");

                {
                    if (OldModelRelease.Count > 0 && NewModelRelease.Count > 0)
                    {
                        StringBuilder ProjectLevelReasons = null;
                        for (int i = 0; i < OldModelRelease.Count; i++)
                        {
                            // for regular section
                            ProjectLevelReasons = new StringBuilder();
                            var newModelReleaseList = (from NewList in NewModelRelease where NewList.ReleaseId == OldModelRelease[i].ReleaseId select NewList).ToList();
                            if (newModelReleaseList != null && newModelReleaseList.Count > 0)
                            {
                                //To check whether regular retail is newly added or existing- as resumbission shouldnt happen for all the request types if new request type is added
                                if (Oldmodel.RegularProjectDetails.ScopeAndRequestType.RegularRetail == model.RegularProjectDetails.ScopeAndRequestType.RegularRetail == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Regular != newModelReleaseList[0].PriceLevel_Regular
                                        && OldModelRelease[i].PriceLevel_Regular != 0)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("PriceLevel");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Regular));
                                    }
                                    if (OldModelRelease[i].IsDeviatedICLALevel_Regular != newModelReleaseList[0].IsDeviatedICLALevel_Regular)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("IsDeviatedICLALevel_Regular");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Regular));
                                    }
                                    if (newModelReleaseList[0].DeviatedICLALevel_Regular != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Regular != newModelReleaseList[0].DeviatedICLALevel_Regular && (OldModelRelease[i].DeviatedICLALevel_Regular != "0")))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("DeviatedICLALevel_Regular");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Regular));
                                        }
                                    }
                                }
                                // for TV section
                                if (Oldmodel.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA == model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_TV != newModelReleaseList[0].PriceLevel_TV
                                        && OldModelRelease[i].PriceLevel_TV != 0)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("PriceLevel_TV");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVPhysical));
                                    }
                                    if (OldModelRelease[i].IsDeviatedICLALevel_TV != newModelReleaseList[0].IsDeviatedICLALevel_TV)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("IsDeviatedICLALevel_TV");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVPhysical));
                                    }
                                    if (newModelReleaseList[0].DeviatedICLALevel_TV != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_TV != newModelReleaseList[0].DeviatedICLALevel_TV && (OldModelRelease[i].DeviatedICLALevel_TV != "0")))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("DeviatedICLALevel_TV");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVPhysical));
                                        }
                                    }
                                    if (newModelReleaseList[0].ExactPPD != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].ExactPPD) != Convert.ToDecimal(newModelReleaseList[0].ExactPPD))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("ExactPPD");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (newModelReleaseList[0].EstimatedRetail != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].EstimatedRetail) != Convert.ToDecimal(newModelReleaseList[0].EstimatedRetail))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("EstimatedRetail");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (modifiedSalesChannelIds.Contains(10))
                                    {
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVALaCarte));
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVSubscription));
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRealTone));
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVMobileRingBackTone));
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.TVStreaming));
                                    }
                                }
                                // price reduction
                                if (Oldmodel.RegularProjectDetails.ScopeAndRequestType.PriceReduction == model.RegularProjectDetails.ScopeAndRequestType.PriceReduction == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Price != newModelReleaseList[0].PriceLevel_Price
                                        && OldModelRelease[i].PriceLevel_Price != 0)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("PriceLevel_Price");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.PriceReduction));
                                    }

                                    if (OldModelRelease[i].IsDeviatedICLALevel_Price != newModelReleaseList[0].IsDeviatedICLALevel_Price)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("IsDeviatedICLALevel_Price");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.PriceReduction));
                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_Price != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Price != newModelReleaseList[0].DeviatedICLALevel_Price && (OldModelRelease[i].DeviatedICLALevel_Price != "0")))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("DeviatedICLALevel_Price");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.PriceReduction));
                                        }
                                    }
                                }
                                // Promotional
                                if (Oldmodel.RegularProjectDetails.ScopeAndRequestType.Promotional == model.RegularProjectDetails.ScopeAndRequestType.Promotional == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Promotional != newModelReleaseList[0].PriceLevel_Promotional
                                        && OldModelRelease[i].PriceLevel_Promotional != 0)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("PriceLevel_Promotional");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Promotional));
                                    }


                                    if (OldModelRelease[i].IsDeviatedICLALevel_Promotional != newModelReleaseList[0].IsDeviatedICLALevel_Promotional)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("IsDeviatedICLALevel_Promotional");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Promotional));
                                    }


                                    if (newModelReleaseList[0].DeviatedICLALevel_Promotional != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Promotional != newModelReleaseList[0].DeviatedICLALevel_Promotional && (OldModelRelease[i].DeviatedICLALevel_Promotional != "0")))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("DeviatedICLALevel_Promotional");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Promotional));
                                        }
                                    }
                                }
                                // Club
                                if (Oldmodel.RegularProjectDetails.ScopeAndRequestType.Club == model.RegularProjectDetails.ScopeAndRequestType.Club == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Club != newModelReleaseList[0].PriceLevel_Club
                                        && OldModelRelease[i].PriceLevel_Club != 0)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("PriceLevel_Club");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Club));
                                    }
                                    if (OldModelRelease[i].IsDeviatedICLALevel_Club != newModelReleaseList[0].IsDeviatedICLALevel_Club)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("IsDeviatedICLALevel_Club");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Club));
                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_Club != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Club != newModelReleaseList[0].DeviatedICLALevel_Club && (OldModelRelease[i].DeviatedICLALevel_Club != "0")))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("DeviatedICLALevel_Club");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Club));
                                        }
                                    }
                                }
                                // Non Tradition
                                if (Oldmodel.RegularProjectDetails.ScopeAndRequestType.NonTraditional == model.RegularProjectDetails.ScopeAndRequestType.NonTraditional == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Non != newModelReleaseList[0].PriceLevel_Non
                                        && OldModelRelease[i].PriceLevel_Non != 0)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("PriceLevel_Non");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                    }
                                    if (OldModelRelease[i].IsDeviatedICLALevel_Non != newModelReleaseList[0].IsDeviatedICLALevel_Non)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("IsDeviatedICLALevel_Non");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                    }
                                    if (newModelReleaseList[0].DeviatedICLALevel_Non != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Non != newModelReleaseList[0].DeviatedICLALevel_Non && (OldModelRelease[i].DeviatedICLALevel_Non != "0")))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("DeviatedICLALevel_Non");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (newModelReleaseList[0].InvoicePrice != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].InvoicePrice) != Convert.ToDecimal(newModelReleaseList[0].InvoicePrice))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("InvoicePrice");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (newModelReleaseList[0].SellingPriceLesVAT != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].SellingPriceLesVAT) != Convert.ToDecimal(newModelReleaseList[0].SellingPriceLesVAT))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("SellingPriceLesVAT");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (newModelReleaseList[0].ICLAAccountingBase != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].ICLAAccountingBase) != Convert.ToDecimal(newModelReleaseList[0].ICLAAccountingBase))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("ICLAAccountingBase");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (newModelReleaseList[0].DeemedPPD != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].DeemedPPD) != Convert.ToDecimal(newModelReleaseList[0].DeemedPPD))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("DeemedPPD");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (newModelReleaseList[0].ResourceFee != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].ResourceFee) != Convert.ToDecimal(newModelReleaseList[0].ResourceFee))
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("ResourceFee");
                                            modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                        }
                                    }
                                    if (OldModelRelease[i].ICLA_Non != newModelReleaseList[0].ICLA_Non)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("ICLA_Non");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                    }
                                    if (OldModelRelease[i].SuggestedFee_Non != newModelReleaseList[0].SuggestedFee_Non)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("SuggestedFee");
                                        modifiedSalesChannelIds.Add(Convert.ToInt16(General.RegularSalesChannelId.Nontraditional));
                                    }
                                }
                                if (model.RegularProjectDetails.ReleaseNewOrExisting.Equals(Constants.ReleaseType.New))
                                {
                                    if (OldModelRelease[i].MusicType_Id == 1)
                                    {
                                        if (OldModelRelease[i].LabelId != newModelReleaseList[0].LabelId)
                                        {
                                            if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                            ProjectLevelReasons.Append("Label");
                                        }
                                    }

                                    int? Old_No_Components = 0;
                                    int? New_No_Components = 0;

                                    if (OldModelRelease[i].No_Components == null)
                                    {
                                        Old_No_Components = 0;
                                    }
                                    else
                                    {
                                        Old_No_Components = OldModelRelease[i].No_Components;
                                    }

                                    if (newModelReleaseList[0].No_Components == null)
                                    {
                                        New_No_Components = 0;
                                    }
                                    else
                                    {
                                        New_No_Components = newModelReleaseList[0].No_Components;
                                    }

                                    if (Old_No_Components < New_No_Components)
                                    {
                                        if (ProjectLevelReasons.Length > 0) ProjectLevelReasons.Append("~");
                                        ProjectLevelReasons.Append("No_Components");
                                    }
                                }
                            }
                            if (ProjectLevelReasons.Length > 0)
                            {
                                lstReleaseReasons.Add(new KeyValuePair<long, string>(OldModelRelease[i].ReleaseId, ProjectLevelReasons.ToString()));

                                if (ProjectLevelReasonsToReturn.Length > 0) ProjectLevelReasonsToReturn.Append("~");
                                ProjectLevelReasonsToReturn.Append(OldModelRelease[i].ReleaseId.ToString());
                                ProjectLevelReasonsToReturn.Append(":");
                                ProjectLevelReasonsToReturn.Append(ProjectLevelReasons.ToString());
                            }
                        }
                    }
                }

                modifiedSalesChannelIds = modifiedSalesChannelIds.Distinct().ToList();

                LoggerFactory.LogWriter.MethodExit();

                return ProjectLevelReasonsToReturn.ToString();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private void CompareRegularRelease(ClearanceProjectModel model, FormCollection collection, List<List<int>> lstCombinedDataList)
        {
            try
            {

                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = new ClearanceProjectModel();

                oldmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId];

                List<ClearanceRelease> NewModelRelease = null;
                NewModelRelease = new List<ClearanceRelease>();
                NewModelRelease = model.RegularProjectDetails.ObjRelease;

                List<ClearanceRelease> OldModelRelease = null;
                OldModelRelease = new List<ClearanceRelease>();
                OldModelRelease = oldmodel.RegularProjectDetails.ObjRelease;

                List<int> lstReleaseChanged = new List<int>();

                
                {
                    if (OldModelRelease.Count > 0 && NewModelRelease.Count > 0)
                    {
                        for (int i = 0; i < OldModelRelease.Count; i++)
                        {
                            // for regular section
                            var newModelReleaseList = (from NewList in NewModelRelease where NewList.ReleaseId == OldModelRelease[i].ReleaseId select NewList).ToList();
                            if (newModelReleaseList != null && newModelReleaseList.Count > 0)
                            {
                                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.RegularRetail == model.RegularProjectDetails.ScopeAndRequestType.RegularRetail == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Regular != newModelReleaseList[0].PriceLevel_Regular
                                        && OldModelRelease[i].PriceLevel_Regular != 0)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                    if (OldModelRelease[i].IsDeviatedICLALevel_Regular != newModelReleaseList[0].IsDeviatedICLALevel_Regular)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_Regular != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Regular != newModelReleaseList[0].DeviatedICLALevel_Regular && (OldModelRelease[i].DeviatedICLALevel_Regular != "0")))
                                        {

                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;

                                        }
                                    }
                                }

                                // for TV section

                                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA == model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_TV != newModelReleaseList[0].PriceLevel_TV
                                        && OldModelRelease[i].PriceLevel_TV != 0)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                    if (OldModelRelease[i].IsDeviatedICLALevel_TV != newModelReleaseList[0].IsDeviatedICLALevel_TV)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_TV != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_TV != newModelReleaseList[0].DeviatedICLALevel_TV && (OldModelRelease[i].DeviatedICLALevel_TV != "0")))
                                        {

                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;

                                        }
                                    }

                                    if (newModelReleaseList[0].ExactPPD != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].ExactPPD) != Convert.ToDecimal(newModelReleaseList[0].ExactPPD))
                                        {

                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;

                                        }
                                    }

                                    if (newModelReleaseList[0].EstimatedRetail != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].EstimatedRetail) != Convert.ToDecimal(newModelReleaseList[0].EstimatedRetail))
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }
                                }

                                // price reduction

                                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.PriceReduction == model.RegularProjectDetails.ScopeAndRequestType.PriceReduction == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Price != newModelReleaseList[0].PriceLevel_Price
                                        && OldModelRelease[i].PriceLevel_Price != 0)
                                    {

                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;

                                    }

                                    if (OldModelRelease[i].IsDeviatedICLALevel_Price != newModelReleaseList[0].IsDeviatedICLALevel_Price)
                                    {

                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;

                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_Price != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Price != newModelReleaseList[0].DeviatedICLALevel_Price && (OldModelRelease[i].DeviatedICLALevel_Price != "0")))
                                        {

                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;

                                        }
                                    }
                                }

                                // Promotional

                                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.Promotional == model.RegularProjectDetails.ScopeAndRequestType.Promotional == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Promotional != newModelReleaseList[0].PriceLevel_Promotional
                                        && OldModelRelease[i].PriceLevel_Promotional != 0)
                                    {

                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;

                                    }

                                    if (OldModelRelease[i].IsDeviatedICLALevel_Promotional != newModelReleaseList[0].IsDeviatedICLALevel_Promotional)
                                    {

                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;

                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_Promotional != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Promotional != newModelReleaseList[0].DeviatedICLALevel_Promotional && (OldModelRelease[i].DeviatedICLALevel_Promotional != "0")))
                                        {

                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;

                                        }
                                    }
                                }

                                // Club
                                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.Club == model.RegularProjectDetails.ScopeAndRequestType.Club == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Club != newModelReleaseList[0].PriceLevel_Club
                                        && OldModelRelease[i].PriceLevel_Club != 0)
                                    {

                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;

                                    }

                                    if (OldModelRelease[i].IsDeviatedICLALevel_Club != newModelReleaseList[0].IsDeviatedICLALevel_Club)
                                    {

                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;

                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_Club != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Club != newModelReleaseList[0].DeviatedICLALevel_Club && (OldModelRelease[i].DeviatedICLALevel_Club != "0")))
                                        {

                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;

                                        }
                                    }
                                }

                                // Non Tradition

                                if (oldmodel.RegularProjectDetails.ScopeAndRequestType.NonTraditional == model.RegularProjectDetails.ScopeAndRequestType.NonTraditional == true)
                                {
                                    if (OldModelRelease[i].PriceLevel_Non != newModelReleaseList[0].PriceLevel_Non
                                        && OldModelRelease[i].PriceLevel_Non != 0)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                    if (OldModelRelease[i].IsDeviatedICLALevel_Non != newModelReleaseList[0].IsDeviatedICLALevel_Non)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                    if (newModelReleaseList[0].DeviatedICLALevel_Non != null)
                                    {
                                        if ((OldModelRelease[i].DeviatedICLALevel_Non != newModelReleaseList[0].DeviatedICLALevel_Non && (OldModelRelease[i].DeviatedICLALevel_Non != "0")))
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }

                                    if (newModelReleaseList[0].InvoicePrice != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].InvoicePrice) != Convert.ToDecimal(newModelReleaseList[0].InvoicePrice))
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }

                                    if (newModelReleaseList[0].SellingPriceLesVAT != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].SellingPriceLesVAT) != Convert.ToDecimal(newModelReleaseList[0].SellingPriceLesVAT))
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }

                                    if (newModelReleaseList[0].ICLAAccountingBase != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].ICLAAccountingBase) != Convert.ToDecimal(newModelReleaseList[0].ICLAAccountingBase))
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }

                                    if (newModelReleaseList[0].DeemedPPD != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].DeemedPPD) != Convert.ToDecimal(newModelReleaseList[0].DeemedPPD))
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }

                                    if (newModelReleaseList[0].ResourceFee != null)
                                    {
                                        if (Convert.ToDecimal(OldModelRelease[i].ResourceFee) != Convert.ToDecimal(newModelReleaseList[0].ResourceFee))
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }

                                    if (OldModelRelease[i].ICLA_Non != newModelReleaseList[0].ICLA_Non)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                    if (OldModelRelease[i].SuggestedFee_Non != newModelReleaseList[0].SuggestedFee_Non)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }
                                }

                                if (model.RegularProjectDetails.ReleaseNewOrExisting.Equals("New"))
                                {

                                    if (OldModelRelease[i].MusicType_Id == 1)
                                    {
                                        if (OldModelRelease[i].LabelId != newModelReleaseList[0].LabelId)
                                        {
                                            lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                            continue;
                                        }
                                    }

                                    int? Old_No_Components = 0;
                                    int? New_No_Components = 0;

                                    if (OldModelRelease[i].No_Components == null)
                                        Old_No_Components = 0;
                                    else
                                        Old_No_Components = OldModelRelease[i].No_Components;

                                    if (newModelReleaseList[0].No_Components == null)
                                        New_No_Components = 0;
                                    else
                                        New_No_Components = newModelReleaseList[0].No_Components;

                                    if (Old_No_Components < New_No_Components)
                                    {
                                        lstReleaseChanged.Add(Convert.ToInt32(OldModelRelease[i].ReleaseId));
                                        continue;
                                    }

                                }
                            }

                        }
                    }
                }

                lstCombinedDataList.Add(lstReleaseChanged);
                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        private bool CompareMasterTerritory(List<TerritorialDisplay> NewModelTerritory, List<TerritorialDisplay> oldModelTerritory)
        {
            bool flag = true;
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                // List of  territories
                if (NewModelTerritory != null && oldModelTerritory != null)
                {
                    {

                        //find the Include country list for New Model
                        var NewModelIncludeList = (from x in NewModelTerritory
                                                   where x.IsIncluded == true
                                                   select x.Id
                                ).Distinct().ToList();

                        //find the Include country list for old Model
                        var OldModelIncludeList = (from x in oldModelTerritory
                                                   where x.IsIncluded == true
                                                   select x.Id
                              ).Distinct().ToList();

                        //if the list has items
                        if (NewModelIncludeList != null && OldModelIncludeList != null)
                        {
                            //if the count of list is not same
                            if (NewModelIncludeList.Count != OldModelIncludeList.Count)
                            {
                                flag = false;
                                return flag;
                            }

                            //If the count is same but id are not same

                            for (int i = 0; i < NewModelIncludeList.Count; i++)
                            {
                                flag = OldModelIncludeList.Contains(NewModelIncludeList[i]);
                                if (flag == false)
                                {
                                    return flag;
                                }
                            }
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }

                        //find the Exclude country list for New Model
                        var NewModelExcludeList = (from x in NewModelTerritory
                                                   where x.IsExcluded == true
                                                   select x.Id
                                 ).Distinct().ToList();

                        //find the Exclude country list for old Model
                        var OldModelExcludeList = (from x in oldModelTerritory
                                                   where x.IsExcluded == true
                                                   select x.Id
                              ).Distinct().ToList();

                        //if the Inclue list has items
                        if (NewModelExcludeList != null && OldModelExcludeList != null)
                        {
                            //if the count of list is not same
                            if (NewModelExcludeList.Count != OldModelExcludeList.Count)
                            {
                                flag = false;
                                return flag;
                            }

                            //If the count is same but id are not same

                            for (int i = 0; i < NewModelExcludeList.Count; i++)
                            {
                                flag = OldModelExcludeList.Contains(NewModelExcludeList[i]);
                                if (flag == false)
                                {
                                    return flag;
                                }
                            }
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }

                }
                else
                {
                    if (NewModelTerritory != oldModelTerritory)
                    {
                        flag = false;
                        return flag;
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return flag;
        }


        //Function to compare Territerioes for Regular
        private bool CompareRegularTerritory(List<TerritorialDisplay> NewModelTerritory, List<TerritorialDisplay> oldModelTerritory)
        {
            bool flag = true;
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                // List of  territories
                if (NewModelTerritory != null && oldModelTerritory != null)
                {
                    {
                        //find the Include country list for New Model
                        var NewModelIncludeList = (from x in NewModelTerritory
                                                   where x.IsIncluded == true
                                                   select x.Id
                                ).Distinct().ToList();

                        //find the Include country list for old Model
                        var OldModelIncludeList = (from x in oldModelTerritory
                                                   where x.IsIncluded == true
                                                   select x.Id
                              ).Distinct().ToList();

                        //if the list has items
                        if (NewModelIncludeList != null && OldModelIncludeList != null)
                        {
                            //if the count of list is not same
                            if (NewModelIncludeList.Count != OldModelIncludeList.Count)
                            {
                                flag = false;
                                return flag;
                            }

                            //If the count is same but id are not same

                            for (int i = 0; i < NewModelIncludeList.Count; i++)
                            {
                                flag = OldModelIncludeList.Contains(NewModelIncludeList[i]);
                                if (flag == false)
                                {
                                    return flag;
                                }
                            }
                        }
                        else
                        {
                            flag = true;
                            return flag;

                        }

                        //find the Exclude country list for New Model
                        var NewModelExcludeList = (from x in NewModelTerritory
                                                   where x.IsExcluded == true
                                                   select x.Id
                                 ).Distinct().ToList();

                        //find the Exclude country list for old Model
                        var OldModelExcludeList = (from x in oldModelTerritory
                                                   where x.IsExcluded == true
                                                   select x.Id
                              ).Distinct().ToList();

                        //if the Inclue list has items
                        if (NewModelExcludeList != null && OldModelExcludeList != null)
                        {
                            //if the count of list is not same
                            if (NewModelExcludeList.Count != OldModelExcludeList.Count)
                            {
                                flag = false;
                                return flag;
                            }

                            //If the count is same but id are not same

                            for (int i = 0; i < NewModelExcludeList.Count; i++)
                            {
                                flag = OldModelExcludeList.Contains(NewModelExcludeList[i]);
                                if (flag == false)
                                {
                                    return flag;
                                }
                            }
                        }
                        else
                        {
                            flag = true;
                            return flag;

                        }

                    }

                }
                else
                {
                    if (NewModelTerritory != oldModelTerritory)
                    {
                        int inNewCount = 0;
                        int inOldCount = 0;
                        if (NewModelTerritory != null)
                        {
                            inNewCount = NewModelTerritory.Count;
                        }
                        if (oldModelTerritory != null)
                        {
                            inOldCount = oldModelTerritory.Count;
                        }
                        if (inNewCount != inOldCount)
                        {
                            flag = false;
                            return flag;
                        }

                    }
                }

                LoggerFactory.LogWriter.MethodExit();

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return flag;
        }

        //Function to compare of Mkt Reviewer in Regular Project

        private bool CompareSpecialCaseRegular(ClearanceProjectModel model, ClearanceProjectModel oldmodel)
        {
            bool flag = true;

            List<long> countryIdList = new List<long>();

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<string> arrayList = null;

                //****************************************************sensitive check
                //check for sensitive explotation
                if (model.RegularProjectDetails.SensitiveExplotation)
                {
                    flag = false;
                    return flag;
                }

                //****************************************************third party check

                arrayList = new List<string>();
                CompanyInfo compDetail = new CompanyInfo();
                string countryName = string.Empty;
                compDetail = _IClearanceProjectRepository.GetRequestorCompany(model.RegularProjectDetails.RequesterCompanyId, getUserInfo());

                if (compDetail != null && compDetail.CountryName != null)
                {
                    countryName = compDetail.CountryName;
                }

                // write a check if requestor's country is not US
                if (model.RegularProjectDetails.ThirdPartyCompanyName != null)
                {
                    if ((model.RegularProjectDetails.ThirdPartyCompanyName.Contains("Reader's Digest"))
                         && (!countryName.Equals("United States")))
                    {
                        flag = false;
                        return flag;
                    }
                }

                //****************************************************scope level check

                //check if the releases have a price reduction situation
                flag = !IsReleaseIncludedAPriceReduction(model, oldmodel);
                if (!flag)
                    return flag;

                //check for Regular request has been updated for new release at budget
                if (model.RegularProjectDetails.ReleaseNewOrExisting == "Exist"
                    && model.RegularProjectDetails.ScopeAndRequestType.PriceReduction)
                {
                    flag = false;
                    return flag;
                }

                flag = !IsNecessaryResubmissionForCompanyOrTerritory(model.RegularProjectDetails, oldmodel.RegularProjectDetails);

                if (!flag)
                    return flag;

                LoggerFactory.LogWriter.MethodExit();

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

            return flag;
        }

        private bool IsNewlyAddedCountryAfterSubmit(List<TerritorialDisplay> NewModelTerritory, List<TerritorialDisplay> oldModelTerritory)
        {
            bool IsAddedCountry = false;
            List<long> newRequestedCountries;
            List<long> oldRequestedCountries;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                newRequestedCountries = GetIncludedMinusExcludedCountries(NewModelTerritory);
                oldRequestedCountries = GetIncludedMinusExcludedCountries(oldModelTerritory);

                var result = (from newcountries in newRequestedCountries
                              join oldcountries in oldRequestedCountries on newcountries equals oldcountries
                              select newcountries).ToList();

                if (newRequestedCountries.Count > result.Count)
                    IsAddedCountry = true;

                LoggerFactory.LogWriter.MethodExit();

                return IsAddedCountry;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private bool IsNotTheSameCountryForCompany(int newCompanyId, int oldCompanyId)
        {
            LoggerFactory.LogWriter.MethodStart();

            CompanyInfo newCompanyInfoCountry = _IClearanceProjectRepository.GetRequestorCompany(newCompanyId, getUserInfo());
            CompanyInfo oldCompanyInfoCountry = _IClearanceProjectRepository.GetRequestorCompany(oldCompanyId, getUserInfo());

            LoggerFactory.LogWriter.MethodExit();

            if (newCompanyInfoCountry.CountryId == oldCompanyInfoCountry.CountryId)
            {
                return false;
            }
            return true;
        }

        private bool IsReleaseIncludedAPriceReduction(ClearanceProjectModel model, ClearanceProjectModel oldmodel)
        {
            LoggerFactory.LogWriter.MethodStart();

            bool flag = false;
            if (oldmodel.RegularProjectDetails.ObjRelease.Count < model.RegularProjectDetails.ObjRelease.Count)
            {
                foreach (ClearanceRelease objRelease in model.RegularProjectDetails.ObjRelease.Where(i => i.ReleaseId == 0))
                {
                    if (model.RegularProjectDetails.ScopeAndRequestType.RegularRetail &&
                       model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA == false &&
                        model.RegularProjectDetails.ScopeAndRequestType.PriceReduction == false)
                    {
                        if ((model.RegularProjectDetails.ReleaseNewOrExisting == Constants.ReleaseType.Exist) &&
                            (objRelease.PriceLevel_Regular == General.PriceLevel.Budget.GetHashCode() || objRelease.PriceLevel_Regular == General.PriceLevel.Mid.GetHashCode()) &&
                            (!oldmodel.RegularProjectDetails.ObjRelease.Where(
                                i => i.PriceLevel_Regular == General.PriceLevel.Budget.GetHashCode() || i.PriceLevel_Regular == General.PriceLevel.Mid.GetHashCode()
                                ).Any())
                            )
                        {
                            flag = true;
                            break;
                        }
                        else if ((model.RegularProjectDetails.ReleaseNewOrExisting == Constants.ReleaseType.New) &&
                            (objRelease.PriceLevel_Regular == General.PriceLevel.Budget.GetHashCode()) &&
                                (!oldmodel.RegularProjectDetails.ObjRelease.Where(i => i.PriceLevel_Regular == General.PriceLevel.Budget.GetHashCode()
                                ).Any())
                            )
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                    {
                        if ((model.RegularProjectDetails.ReleaseNewOrExisting == Constants.ReleaseType.Exist) &&
                            (objRelease.PriceLevel_TV == General.PriceLevel.Budget.GetHashCode() || objRelease.PriceLevel_TV == General.PriceLevel.Mid.GetHashCode()) &&
                            (!oldmodel.RegularProjectDetails.ObjRelease.Where(
                                i => i.PriceLevel_TV == General.PriceLevel.Budget.GetHashCode() || i.PriceLevel_TV == General.PriceLevel.Mid.GetHashCode()
                                ).Any())
                            )
                        {
                            flag = true;
                            break;
                        }
                        else if ((model.RegularProjectDetails.ReleaseNewOrExisting == Constants.ReleaseType.New) &&
                            (objRelease.PriceLevel_TV == General.PriceLevel.Budget.GetHashCode()) &&
                                (!oldmodel.RegularProjectDetails.ObjRelease.Where(i => i.PriceLevel_TV == General.PriceLevel.Budget.GetHashCode()
                                ).Any())
                            )
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }

            LoggerFactory.LogWriter.MethodExit();

            return flag;
        }

        private List<long> GetIncludedMinusExcludedCountries(List<TerritorialDisplay> countries)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<long> countryList = new List<long>();

                if (countries != null)
                {
                    var includedCountries = (from territories in countries
                                             where territories.IsTerritory == false
                                                && territories.IsIncluded == true
                                             select territories.Id).ToList();

                    var excludedCountries = (from territories in countries
                                             where territories.IsTerritory == false
                                                && territories.IsIncluded == false
                                             select territories.Id).ToList();
                    //select included minus excluded for processing
                    countryList = (from ic in includedCountries
                                   where !excludedCountries.Contains(ic)
                                   select ic).Distinct().ToList();
                }

                LoggerFactory.LogWriter.MethodExit();

                return countryList;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        //Function to comapare the Resource for Regular Project
        private void CompareRegularResourceSubmitReasons(ClearanceProjectModel model, FormCollection collection, List<KeyValuePair<long, string>> lstResourceReasons)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel Oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId];
                List<ClearanceResource> NewModelResource = model.RegularProjectDetails.ClearanceResource;
                List<ClearanceResource> OldModelResource = Oldmodel.RegularProjectDetails.ClearanceResource;

                // List of ClearanceResource
                if (NewModelResource != null && OldModelResource != null)
                {
                    StringBuilder SubmitReasons = null;
                    for (int i = 0; i < OldModelResource.Count; i++)
                    {
                        SubmitReasons = new StringBuilder();
                        var newResourceList = (from newlist in NewModelResource where newlist.ClearanceResourceId == OldModelResource[i].ClearanceResourceId select newlist).ToList();
                        if (newResourceList != null && newResourceList.Count > 0)
                        {
                            //check for sensitive explotation
                            if (OldModelResource[i].IsGloballyCleared != newResourceList[0].IsGloballyCleared)
                            {
                                if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                SubmitReasons.Append("IsGloballyCleared");
                            }
                            //Compare for Territory for Request Type Regular Tab
                            if (!CompareRegularTerritory(OldModelResource[i].TerritorialRights, newResourceList[0].TerritorialRights))
                            {
                                if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                SubmitReasons.Append("Territories");
                            }
                            //condition for only Free Hand Resource
                            if (OldModelResource[i].ClearanceResourceId > 0 && OldModelResource[i].R2_ResourceId == 0)
                            {
                                //Checking for all resoruces except the free hand replaced resoruces
                                //check for Artist
                                if (OldModelResource[i].ArtistName != newResourceList[0].ArtistName)
                                {
                                    if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                    SubmitReasons.Append("ArtistName");
                                }
                                //check for Title
                                if (OldModelResource[i].Title != newResourceList[0].Title)
                                {
                                    if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                    SubmitReasons.Append("Title");
                                }
                                //check for Version Title
                                if (OldModelResource[i].VersionTitle != newResourceList[0].VersionTitle)
                                {
                                    if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                    SubmitReasons.Append("VersionTitle");
                                }
                                //check for Recording Type
                                if (OldModelResource[i].RecordingTypeDesc.ToUpper() != newResourceList[0].RecordingTypeDesc.ToUpper())
                                {
                                    if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                    SubmitReasons.Append("RecordingTypeDesc");
                                }
                                //check for Resource Type
                                if (OldModelResource[i].ResourceTypeDesc.ToUpper() != newResourceList[0].ResourceTypeDesc.ToUpper())
                                {
                                    if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                    SubmitReasons.Append("ResourceTypeDesc");
                                }
                                //check for Music Type
                                if (OldModelResource[i].MusicTypeDesc != newResourceList[0].MusicTypeDesc)
                                {
                                    if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                    SubmitReasons.Append("MusicTypeDesc");
                                }
                                //check for Duration
                                if (OldModelResource[i].MusicTime != newResourceList[0].MusicTime)
                                {
                                    if (SubmitReasons.Length > 0) SubmitReasons.Append("~");
                                    SubmitReasons.Append("MusicTime");
                                }
                            }
                        }
                        if (SubmitReasons.Length > 0)
                        {
                            lstResourceReasons.Add(new KeyValuePair<long, string>(newResourceList[0].ResourceId, SubmitReasons.ToString()));
                        }
                    }
                }
                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        private void CompareRegularResource(ClearanceProjectModel model, FormCollection collection, List<List<int>> lstCombinedDataList)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                bool flag = true;

                ClearanceProjectModel Oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId];

                List<ClearanceResource> NewModelResource = model.RegularProjectDetails.ClearanceResource;
                List<ClearanceResource> OldModelResource = Oldmodel.RegularProjectDetails.ClearanceResource;

                List<int> lstresourceChanged = new List<int>();

                // List of ClearanceResource
                if (NewModelResource != null && OldModelResource != null)
                {
                    if (OldModelResource != null)
                    {
                        for (int i = 0; i < OldModelResource.Count; i++)
                        {
                            var newResourceList = (from newlist in NewModelResource where newlist.ClearanceResourceId == OldModelResource[i].ClearanceResourceId select newlist).ToList();
                            if (newResourceList != null && newResourceList.Count > 0)
                            {
                                //check for sensitive explotation
                                if (OldModelResource[i].IsGloballyCleared != newResourceList[0].IsGloballyCleared)
                                {
                                    flag = false;
                                    lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    continue;
                                }
                                //Compare for Territory for Request Type Regular Tab
                                flag = CompareRegularTerritory(OldModelResource[i].TerritorialRights, newResourceList[0].TerritorialRights);
                                if (!flag)
                                {
                                    lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    continue;
                                }
                                //condition for only Free Hand Resource
                                if (OldModelResource[i].ClearanceResourceId > 0 && OldModelResource[i].R2_ResourceId == 0)
                                {
                                    //Checking for all resoruces except the free hand replaced resoruces
                                    //check for Artist
                                    if (OldModelResource[i].ArtistName != newResourceList[0].ArtistName)
                                    {
                                        flag = false;
                                        lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    }
                                    //check for Title
                                    else if (OldModelResource[i].Title != newResourceList[0].Title)
                                    {
                                        flag = false;
                                        lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    }
                                    //check for Version Title
                                    else if (OldModelResource[i].VersionTitle != newResourceList[0].VersionTitle)
                                    {
                                        flag = false;
                                        lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    }
                                    //check for Recording Type
                                    else if (OldModelResource[i].RecordingTypeDesc.ToUpper() != newResourceList[0].RecordingTypeDesc.ToUpper())
                                    {
                                        flag = false;
                                        lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    }
                                    //check for Resource Type
                                    else if (OldModelResource[i].ResourceTypeDesc.ToUpper() != newResourceList[0].ResourceTypeDesc.ToUpper())
                                    {
                                        flag = false;
                                        lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    }
                                    else if (OldModelResource[i].MusicTypeDesc != newResourceList[0].MusicTypeDesc)
                                    {
                                        flag = false;
                                        lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    }
                                    else if (OldModelResource[i].MusicTime != newResourceList[0].MusicTime)
                                    {
                                        flag = false;
                                        lstresourceChanged.Add(Convert.ToInt32(newResourceList[0].ResourceId));
                                    }
                                }
                            }
                        }
                    }
                }

                lstCombinedDataList.Add(lstresourceChanged);

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        public ActionResult DiscardAllAmendments(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ModelState.Clear();

                ClearanceProjectModel oldmodel = new ClearanceProjectModel();
                oldmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId];
                Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId] = oldmodel;

                CacheData(oldmodel, "");

                // for maintaining the state of TAB selected
                ViewBag.DefaultTab = collection["hdnDefaultTab"];

                List<string> list = RegularListTooltipControl(oldmodel.RegularProjectDetails.ReleaseNewOrExisting);
                ViewBag.ControlsRegularList = list;

                var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                Territories.Add(1, oldmodel.RegularProjectDetails.Territories);
                Territories.Add(2, oldmodel.RegularProjectDetails.ScopeAndRequestType.Territories);

                int tempI = 2;
                if (oldmodel.RegularProjectDetails.ClearanceResource != null)
                {
                    foreach (ClearanceResource cls in oldmodel.RegularProjectDetails.ClearanceResource)
                    {
                        Territories.Add(++tempI, cls.TerritorialRights);
                    }
                }

                ViewBag.ProjectTerritories = Territories;
                string projectReferenceId = string.Empty;
                ViewBag.LoadTemplate = "1";

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("CreateRegularProjectPartialView", oldmodel);
            }
            catch (Exception ex)
            {
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return PartialView("CreateRegularProjectPartialView", model);
            }
        }

        #endregion

        public ActionResult GetCancelRejectedResourceForRelease(ResourceInfo releaseInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ClearanceProjectModel oldmodel = new ClearanceProjectModel();

                oldmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + releaseInfo.Id];

                if (!string.IsNullOrEmpty(releaseInfo.Isrc))
                {
                    List<long> listResource = new List<long>();
                    listResource = releaseInfo.Isrc.Split(',').Select(long.Parse).ToList();
                    List<ClearanceInboxRequest> listfinal = new List<ClearanceInboxRequest>();
                    listfinal = (from list in oldmodel.RegularProjectDetails.RequestInfoList
                                 where listResource.Contains(Convert.ToInt32(list.ReleaseId))
                                 select list).ToList();

                    oldmodel.RegularProjectDetails.RequestInfoList = listfinal;
                }

                LoggerFactory.LogWriter.MethodExit();
                return View("CancelledRejResource", oldmodel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult GetCancelRejectedResourceRegularForSelection(ResourceInfo resourceInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = (ClearanceProjectModel)Session["NewEntity" + "_" + resourceInfo.Id];
                List<ClearanceInboxRequest> listfinal = new List<ClearanceInboxRequest>();

                if (!string.IsNullOrEmpty(resourceInfo.Isrc))
                {
                    List<long>  listResource = resourceInfo.Isrc.Split(',').Select(long.Parse).ToList();
                    listfinal = (from list in oldmodel.RegularProjectDetails.RequestInfoList
                                 where listResource.Contains(Convert.ToInt32(list.ResourceId))
                                 select list).ToList();

                    oldmodel.RegularProjectDetails.RequestInfoList = listfinal;
                }
                else
                {
                    listfinal = (from list in oldmodel.RegularProjectDetails.RequestInfoList
                                 where (list.ApprovalStatusId == 5 || list.ApprovalStatusId == 7 || list.ApprovalStatusId == 10)
                                 select list).ToList();

                    oldmodel.RegularProjectDetails.RequestInfoList = listfinal;
                }

                ViewBag.ProjectType = "Regular";

                LoggerFactory.LogWriter.MethodExit();

                return View("CancelledRejResource", oldmodel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult GetCancelRejectedResourceRegular(ResourceInfo resourceInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = (ClearanceProjectModel)Session["NewEntity" + "_" + resourceInfo.Id];
                List<long> listfinal = new List<long>();

                if (!string.IsNullOrEmpty(resourceInfo.Isrc))
                {
                    List<long> listResource = new List<long>();
                    listResource = resourceInfo.Isrc.Split(',').Select(long.Parse).ToList();

                    listfinal = (from list in oldmodel.RegularProjectDetails.RequestInfoList
                                 where listResource.Contains(list.ResourceId)
                                 select list.RoutedItemId).ToList();
                }
                else
                {
                    listfinal = (from list in oldmodel.RegularProjectDetails.RequestInfoList
                                 where (list.ApprovalStatusId == 5 || list.ApprovalStatusId == 7 || list.ApprovalStatusId == 10)
                                 select list.RoutedItemId).ToList();
                }

                ViewBag.ProjectType = "Regular";

                string routedItemList = string.Empty;
                if (listfinal != null && listfinal.Count > 0)
                    routedItemList = string.Join(",", listfinal);

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { listfinal = routedItemList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private void CompareRegularOtherResourceEntitySubmitReasons(ClearanceProjectModel model, List<KeyValuePair<long, string>> lstResourceOtherReasons)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel Oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntity + model.RegularProjectDetails.ClrProjectId];

                List<ClearanceResource> NewModelResource = model.RegularProjectDetails.ClearanceResource;
                List<ClearanceResource> OldModelResource = Oldmodel.RegularProjectDetails.ClearanceResource;

                // List of ClearanceResource
                if (NewModelResource != null && OldModelResource != null)
                {
                    for (int i = 0; i < OldModelResource.Count; i++)
                    {
                        string submitResource = string.Empty;
                        var newCommentsFlag = (from newlist in NewModelResource where newlist.ClearanceResourceId == OldModelResource[i].ClearanceResourceId select newlist.Comments).FirstOrDefault();
                        //check for sensitive explotation
                        if (OldModelResource[i].Comments != Convert.ToString(newCommentsFlag))
                        {

                            ClearanceResource lstResourceAdd = lstResourceAdd = (from newlist in NewModelResource
                                                                                 where newlist.ClearanceResourceId == OldModelResource[i].ClearanceResourceId
                                                                                 select newlist).FirstOrDefault();

                            if (lstResourceOtherReasons.Exists(r => r.Key == lstResourceAdd.ResourceId))
                            {
                                string reasonDesc = lstResourceOtherReasons.Where(r => r.Key == lstResourceAdd.ResourceId).First().Value;
                                lstResourceOtherReasons.Remove(new KeyValuePair<long, string>(lstResourceAdd.ResourceId, reasonDesc));
                                reasonDesc = reasonDesc + "~Comments";
                                lstResourceOtherReasons.Add(new KeyValuePair<long, string>(lstResourceAdd.ResourceId, reasonDesc));
                            }
                            else
                            {
                                lstResourceOtherReasons.Add(new KeyValuePair<long, string>(lstResourceAdd.ResourceId, "Comments"));
                            }
                        }
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private void CompareRegularOtherResourceEntity(ClearanceProjectModel model, List<List<int>> lstCombinedDataList)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                ClearanceProjectModel oldmodel = new ClearanceProjectModel();

                oldmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId];

                List<ClearanceResource> NewModelResource = null;
                NewModelResource = new List<ClearanceResource>();
                NewModelResource = model.RegularProjectDetails.ClearanceResource;

                List<ClearanceResource> oldModelResource = null;
                oldModelResource = new List<ClearanceResource>();
                oldModelResource = oldmodel.RegularProjectDetails.ClearanceResource;

                List<int> lstresourceChanged = new List<int>();

                // List of ClearanceResource
                if (NewModelResource != null && oldModelResource != null)
                {

                    if (oldModelResource != null)
                    {
                        for (int i = 0; i < oldModelResource.Count; i++)
                        {

                            var newCommentsFlag = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.Comments).FirstOrDefault();
                            //check for sensitive explotation
                            if (oldModelResource[i].Comments != Convert.ToString(newCommentsFlag))
                            {

                                ClearanceResource lstResourceAdd = new ClearanceResource();
                                lstResourceAdd = (from newlist in NewModelResource
                                                  where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId
                                                  select newlist).FirstOrDefault();
                                lstresourceChanged.Add(Convert.ToInt32(lstResourceAdd.ResourceId));
                                continue;

                            }
                        }
                    }
                }

                lstCombinedDataList.Add(lstresourceChanged);

                LoggerFactory.LogWriter.MethodExit();

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            
        }

        private void RemovePackageRequests(ClearanceProjectModel model, FormCollection collection, List<List<int>> lstCombinedDataList)
        {

            List<int> arrPackageIdToRemove = new List<int>();
            ViewBag.Newmodelproject = model;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = new ClearanceProjectModel();
                oldmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId];

                GetPackageInfoForOldModel(oldmodel);
                GetPackageInfoForReleaseNew(model, collection);

                if (model.RegularProjectDetails.ObjRelease.Where(i => i.PackageInfo.Where(j => j.IsNewlyAddedAfterSubmit).ToList().Count > 0).ToList().Count > 0)
                {
                    arrPackageIdToRemove.Add(-1);
                }

                if (oldmodel != null && model != null)
                {
                    foreach (ClearanceRelease clrRelease in oldmodel.RegularProjectDetails.ObjRelease)
                    {
                        if (clrRelease.ReleaseId != 0) // this is release saved already in the database
                        {
                            var clrReleaseNewModel = model.RegularProjectDetails.ObjRelease.Where(i => i.ReleaseId == clrRelease.ReleaseId).FirstOrDefault();

                            if (clrReleaseNewModel != null && clrReleaseNewModel.ReleaseId > 0)
                            {
                                foreach (PackageInfo pkgInfo in clrRelease.PackageInfo)
                                {
                                    PackageInfo packageInfoOld = clrReleaseNewModel.PackageInfo.Where(i => i.PackageId == pkgInfo.PackageId && i.ArchiveFlag == "N").FirstOrDefault();
                                    if (packageInfoOld == null)
                                    {
                                        arrPackageIdToRemove.Add(Convert.ToInt32(pkgInfo.PackageId));
                                        GetChildrenForPackage(long.Parse(pkgInfo.R2ReleaseId.ToString()), arrPackageIdToRemove);
                                    }

                                }
                            }
                        }

                    }

                }

                LoggerFactory.LogWriter.MethodExit();

                lstCombinedDataList.Add(arrPackageIdToRemove);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        private void GetChildrenForPackage(long releaseId, List<int> arrPackageIdToRemove)
        {
            LoggerFactory.LogWriter.MethodStart();

            List<PackageInfo> pkgInfoChild = new List<PackageInfo>();
            pkgInfoChild = _IClearanceReleaseRepository.GetPackageDetailsforProjectRelease(releaseId, getUserInfo());

            if (pkgInfoChild != null & pkgInfoChild.Count > 0)
            {
                foreach (var child in pkgInfoChild)
                {
                    arrPackageIdToRemove.Add(Convert.ToInt32(child.PackageId));
                }
            }

            LoggerFactory.LogWriter.MethodExit();
        }

        [HttpPost]
        public JsonResult GetNewMasterProjectModel(ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                model.MasterProjectDetails.IsSensitiveDataChanged = true;
                bool flag = false;

                List<string> arrayList = null;

                ViewBag.Newmodelproject = model;
                ClearanceProjectModel oldmodel = new ClearanceProjectModel();
                oldmodel = (ClearanceProjectModel)Session["OldEntityMaster" + "_" + model.MasterProjectDetails.ClearanceProjectID];

                ViewBag.ProjectTerritories = SetMasterManageTerritories(collection, model);

                //section for master project details
                arrayList = new List<string>();
                arrayList.Add("MasterProject.Currency");
                arrayList.Add("MasterProject.SensitiveExplotation");
                arrayList.Add("MasterProject.OneStop");
                arrayList.Add("MasterProject.LicenseTerm");
                arrayList.Add("MasterProject.IsAdvertisingRequest");
                arrayList.Add("MasterProject.IsFilmRequest");
                arrayList.Add("MasterProject.IsTrailerRequest");
                arrayList.Add("MasterProject.IsOtherRequest");

                flag = PublicInstancePropertiesEqual(model.MasterProjectDetails, oldmodel.MasterProjectDetails, arrayList);

                if (!flag)
                {
                    return Json(flag);
                }

                //Advertising Section Of Master Project
                arrayList = new List<string>();
                arrayList.Add("Advertising.AdvertisedProducts");
                arrayList.Add("Advertising.TV");
                arrayList.Add("Advertising.Cinema");
                arrayList.Add("Advertising.Video");
                arrayList.Add("Advertising.Radio");
                arrayList.Add("Advertising.Internet");
                arrayList.Add("Advertising.Other");
                arrayList.Add("Advertising.OtherComments");
                arrayList.Add("Advertising.OptionalAdditionalRights");
                flag = PublicInstancePropertiesEqual(model.MasterProjectDetails.Advertising, oldmodel.MasterProjectDetails.Advertising, arrayList);

                if (!flag)
                {
                    return Json(flag);
                }

                //Film Section Of Master Project
                arrayList = new List<string>();
                arrayList.Add("Film.TV");
                arrayList.Add("Film.Cinema");
                arrayList.Add("Film.Video");
                arrayList.Add("Film.Internet");
                arrayList.Add("Film.Other");
                arrayList.Add("Film.OtherComments");
                arrayList.Add("Film.OptionalAdditionalRights");
                arrayList.Add("Film.InitialNoOfVideos");
                flag = PublicInstancePropertiesEqual(model.MasterProjectDetails.Film, oldmodel.MasterProjectDetails.Film, arrayList);

                if (!flag)
                {
                    return Json(flag);
                }

                //Trailer Section of Master Project
                arrayList = new List<string>();
                arrayList.Add("Trailer.TV");
                arrayList.Add("Trailer.Cinema");
                arrayList.Add("Trailer.Video");
                arrayList.Add("Trailer.Internet");
                arrayList.Add("Trailer.Other");
                arrayList.Add("Trailer.OtherComments");
                arrayList.Add("Trailer.OptionalAdditionalRights");
                arrayList.Add("Trailer.InitialNoOfVideos");
                flag = PublicInstancePropertiesEqual(model.MasterProjectDetails.Trailer, oldmodel.MasterProjectDetails.Trailer, arrayList);

                if (!flag)
                {
                    return Json(flag);
                }

                //Other Section of Master Project
                arrayList = new List<string>();
                arrayList.Add("Others.TV");
                arrayList.Add("Others.Cinema");
                arrayList.Add("Others.Video");
                arrayList.Add("Others.Internet");
                arrayList.Add("Others.Other");
                arrayList.Add("Others.OtherComments");
                arrayList.Add("Others.OptionalAdditionalRights");
                arrayList.Add("Others.InitialNoOfVideos");
                flag = PublicInstancePropertiesEqual(model.MasterProjectDetails.Others, oldmodel.MasterProjectDetails.Others, arrayList);

                if (!flag)
                {
                    return Json(flag);
                }

                Session["OldEntityMaster" + "_" + model.MasterProjectDetails.ClearanceProjectID] = oldmodel;
                Session["NewEntityMaster" + "_" + model.MasterProjectDetails.ClearanceProjectID] = model;

                LoggerFactory.LogWriter.MethodExit();
                return Json(flag);
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        //Function to compare Territories for Master Project
        private bool CompareMasterTerritoryAtprojectlevel(List<TerritorialDisplay> NewModelTerritory, List<TerritorialDisplay> oldModelTerritory)
        {
            bool flag = true;
            try
            {

                LoggerFactory.LogWriter.MethodStart();

                if (NewModelTerritory != null && oldModelTerritory != null)
                {
                    //find the Include country list for New Model
                    var NewModelIncludeList = (from x in NewModelTerritory
                                               where x.IsIncluded == true
                                               select x.Id
                            ).Distinct().ToList();

                    //find the Include country list for old Model
                    var OldModelIncludeList = (from x in oldModelTerritory
                                               where x.IsIncluded == true
                                               select x.Id
                          ).Distinct().ToList();

                    //if the list has items
                    if (NewModelIncludeList != null && OldModelIncludeList != null)
                    {
                        //if the count of list is not same
                        if (NewModelIncludeList.Count != OldModelIncludeList.Count)
                        {
                            flag = false;
                            return flag;
                        }

                        //If the count is same but id are not same

                        for (int i = 0; i < NewModelIncludeList.Count; i++)
                        {
                            flag = OldModelIncludeList.Contains(NewModelIncludeList[i]);
                            if (flag == false)
                            {
                                return flag;
                            }
                        }
                    }
                    else
                    {
                        flag = false;
                        return flag;
                    }

                    //find the Exclude country list for New Model
                    var NewModelExcludeList = (from x in NewModelTerritory
                                               where x.IsExcluded == true
                                               select x.Id
                             ).Distinct().ToList();

                    //find the Exclude country list for old Model
                    var OldModelExcludeList = (from x in oldModelTerritory
                                               where x.IsExcluded == true
                                               select x.Id
                          ).Distinct().ToList();

                    //if the Inclue list has items
                    if (NewModelExcludeList != null && OldModelExcludeList != null)
                    {
                        //if the count of list is not same
                        if (NewModelExcludeList.Count != OldModelExcludeList.Count)
                        {
                            flag = false;
                            return flag;
                        }

                        //If the count is same but id are not same

                        for (int i = 0; i < NewModelExcludeList.Count; i++)
                        {
                            flag = OldModelExcludeList.Contains(NewModelExcludeList[i]);
                            if (flag == false)
                            {
                                return flag;
                            }
                        }
                    }
                    else
                    {
                        flag = false;
                        return flag;

                    }

                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return flag;
        }

        private List<string> MasterListTooltipControl()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<string> arrayList = null;
                arrayList = new List<string>();
                arrayList.Add("chkSE");
                arrayList.Add("chkOS");
                arrayList.Add("chkFilm");
                arrayList.Add("chkAdvertising");
                arrayList.Add("chkTrailer");
                arrayList.Add("chkOther");
                arrayList.Add("chkAdvertisingTV");
                arrayList.Add("chkAdvertisingCinema");
                arrayList.Add("chkAdvertisingVideo");
                arrayList.Add("chkAdvertisingRadio");
                arrayList.Add("chkAdvertisingInternet");
                arrayList.Add("chkAdvertisingOther");
                arrayList.Add("chkFilmTV");
                arrayList.Add("chkFilmCinema");
                arrayList.Add("chkFilmVideo");
                arrayList.Add("chkFilmInternet");
                arrayList.Add("chkFilmTrailer");
                arrayList.Add("chkFilmOther");
                arrayList.Add("chkTrailersTV");
                arrayList.Add("chkTrailersCinema");
                arrayList.Add("chkTrailersVideo");
                arrayList.Add("chkTrailersInternet");
                arrayList.Add("chkTrailersOther");
                arrayList.Add("chkOthersTV");
                arrayList.Add("chkOthersCinema");
                arrayList.Add("chkOthersVideo");
                arrayList.Add("chkOthersRadio");
                arrayList.Add("chkOthersInternet");
                arrayList.Add("chkOthersOther");

                arrayList.Add("txtLicenseTerm");
                arrayList.Add("txtAdvertisedProducts");
                arrayList.Add("txtAdvertiseOtherComments");
                arrayList.Add("txtAdvOptAddRights");
                arrayList.Add("txtFilmOtherComments");
                arrayList.Add("txtFilmVideo");
                arrayList.Add("txtFilmAddRights");
                arrayList.Add("TrailerOtherComments");
                arrayList.Add("TrailerVideos");
                arrayList.Add("txtTrailerAddRights");
                arrayList.Add("OthersOtherComments");
                arrayList.Add("OthersOtherVideo");
                arrayList.Add("txtOthersAddRights");
                arrayList.Add("MasterProjectDetails_Currency");

                arrayList.Add("btnManageTerritories");

                arrayList.Add("_txtExcerptTime");
                arrayList.Add("_txtSuggestedFee");
                arrayList.Add("_chksensivtive");
                arrayList.Add("_btnManageTerritories_");


                LoggerFactory.LogWriter.MethodExit();
                return arrayList;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult GetCancelRejectedResourceForSelection(ResourceInfo resourceInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = (ClearanceProjectModel)Session["NewEntityMaster" + "_" + resourceInfo.Id];
                List<ClearanceInboxRequest> listfinal = new List<ClearanceInboxRequest>();

                if (!string.IsNullOrEmpty(resourceInfo.Isrc))
                {
                    List<long> listResource = resourceInfo.Isrc.Split(',').Select(long.Parse).ToList();
                    listfinal = (from list in oldmodel.MasterProjectDetails.RequestInfoList
                                 where listResource.Contains(list.ClearanceProjectResourceId)
                                 select list).ToList();

                    oldmodel.MasterProjectDetails.RequestInfoList = listfinal;
                }
                else
                {
                    listfinal = (from list in oldmodel.MasterProjectDetails.RequestInfoList
                                 where (list.ApprovalStatusId == 5 || list.ApprovalStatusId == 7 || list.ApprovalStatusId == 10)
                                 select list).ToList();

                    oldmodel.MasterProjectDetails.RequestInfoList = listfinal;
                }
                ViewBag.ProjectType = "Master";

                LoggerFactory.LogWriter.MethodExit();

                return View("CancelledRejResource", oldmodel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpGet]
        public ActionResult GetCancelRejectedResource(ResourceInfo resourceInfo)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel  oldmodel = (ClearanceProjectModel)Session["NewEntityMaster" + "_" + resourceInfo.Id];
                List<long> listfinal = new List<long>();

                if (!string.IsNullOrEmpty(resourceInfo.Isrc))
                {
                    List<long> listResource = resourceInfo.Isrc.Split(',').Select(long.Parse).ToList();
                    listfinal = (from list in oldmodel.MasterProjectDetails.RequestInfoList
                                 where listResource.Contains(list.ClearanceProjectResourceId)
                                 select list.RoutedItemId).ToList();
                }
                else
                {
                    listfinal = (from list in oldmodel.MasterProjectDetails.RequestInfoList
                                where (list.ApprovalStatusId == 5 || list.ApprovalStatusId == 7 || list.ApprovalStatusId == 10)
                                select list.RoutedItemId).ToList();
                }
                ViewBag.ProjectType = "Master";
                string routedItemList = string.Empty;
                if (listfinal != null && listfinal.Count > 0)
                    routedItemList = string.Join(",", listfinal);

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { listfinal = routedItemList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public ActionResult DiscardMasterResubmission(string selectedTab, string clrProjectId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("ClearanceProjectModel: {0}", selectedTab));

                ClearanceProjectModel clearancemodel = new ClearanceProjectModel();
                clearancemodel = (ClearanceProjectModel)Session["OldEntityMaster" + "_" + clrProjectId];

                ModelState.Clear();
                TempData["MasterResourceComments"] = null;

                LoggerFactory.LogWriter.Info("Successfully Discard Master Resubmission View Loaded");
                LoggerFactory.LogWriter.Debug("Successfully Discard Master Resubmission View Loaded");
                List<string> list = MasterListTooltipControl();
                ViewBag.ControlsList = list;

                var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                Territories.Add(1, clearancemodel.MasterProjectDetails.Territories);
                int i = 2;
                foreach (ClearanceResource cls in clearancemodel.MasterProjectDetails.ClearanceResource)
                {
                    Territories.Add(++i, cls.TerritorialRights);
                }

                clearancemodel.RequestTypeManufacturedBy = _IClearanceProjectModel.RequestTypeManufacturedBy;
                ViewBag.DefaultTab = selectedTab;
                ViewBag.ProjectTerritories = Territories;
                ViewBag.ProjectStatus = Enum.Parse(typeof(General.StatusType), clearancemodel.MasterProjectDetails.StatusType.ToString(), true).ToString();

                LoggerFactory.LogWriter.MethodExit();

                return View("CreateMasterProject", clearancemodel);

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        public List<long> ListOfResourceStatus()
        {
            List<long> ListClearanceResourceId = new List<long>();
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = new ClearanceProjectModel();
                oldmodel = (ClearanceProjectModel)TempData["OldEntity"];
                TempData["OldEntity"] = oldmodel;

                ListClearanceResourceId = (from list in oldmodel.MasterProjectDetails.RequestInfoList where list.ApprovalStatusId == 1 || list.ApprovalStatusId == 2 || (list.ApprovalStatusId == 5) select list.ClearanceProjectResourceId).ToList();

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return ListClearanceResourceId;
        }

        [HttpPost]
        public JsonResult GetRoutingTriggered(ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string ProjectReason = string.Empty;
                ClearanceProjectModel oldmodel = new ClearanceProjectModel();

                if (Session[Constants.Sessions.OldEntityMaster + model.MasterProjectDetails.ClearanceProjectID] != null)
                    oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntityMaster + model.MasterProjectDetails.ClearanceProjectID];
                else
                {
                    _IClearanceProjectModel.MasterProjectDetails = _IClearanceProjectRepository.GetMasterProjectDetails(Convert.ToInt32(model.MasterProjectDetails.ClearanceProjectID), getUserInfo());
                    oldmodel.MasterProjectDetails = _IClearanceProjectModel.MasterProjectDetails;
                    Session[Constants.Sessions.OldEntityMaster + model.MasterProjectDetails.ClearanceProjectID] = oldmodel;
                }

                List<int> lstMasterSpecialCase = new List<int>();

                List<List<int>> CombinedList = new List<List<int>>(); //Combined list

                if (model.MasterProjectDetails.SensitiveExplotation)
                {
                    lstMasterSpecialCase.Insert(0, 1);
                    CombinedList.Insert(0, lstMasterSpecialCase);
                }
                else
                {
                    lstMasterSpecialCase.Insert(0, 0);
                    CombinedList.Insert(0, lstMasterSpecialCase);
                }

                List<int> listProjectLevel = new List<int>(); //project level list


                bool isUMGISensitiveDataChanged = IsUMGISensitiveDataChanged(model.MasterProjectDetails, oldmodel.MasterProjectDetails);
                List<long> sensitiveResourceListConditionChanged = GetUMGISensitiveDataChangedOnResource(model.MasterProjectDetails.ClearanceResource, oldmodel.MasterProjectDetails.ClearanceResource);

                Dictionary<string, string> arrayList = null; //list for project level fields

                ViewBag.Newmodelproject = model;

                model = SetArtistDetailsForResource(model, collection);


                ViewBag.ProjectTerritories = SetMasterManageTerritories(collection, model);

                //section for master project details
                arrayList = new Dictionary<string, string>();
                arrayList.Add("MasterProject.Currency", "Currency");
                arrayList.Add("MasterProject.SensitiveExplotation", "Sensitive Exploitation");
                arrayList.Add("MasterProject.OneStop", "One Stop");
                arrayList.Add("MasterProject.LicenseTerm", "LicenseTerm");
                arrayList.Add("MasterProject.IsAdvertisingRequest", "Advertising Request");
                arrayList.Add("MasterProject.IsFilmRequest", "FilmRequest");
                arrayList.Add("MasterProject.IsTrailerRequest", "TrailerRequest");
                arrayList.Add("MasterProject.IsOtherRequest", "OtherRequest");

                //Advertising Section Of Master Project
                arrayList.Add("Advertising.AdvertisedProducts", "Request Type");
                arrayList.Add("Advertising.TV", "Request Type");
                arrayList.Add("Advertising.Cinema", "Request Type");
                arrayList.Add("Advertising.Video", "Request Type");
                arrayList.Add("Advertising.Radio", "Request Type");
                arrayList.Add("Advertising.Internet", "Request Type");
                arrayList.Add("Advertising.Other", "Request Type");
                arrayList.Add("Advertising.OtherComments", "Request Type");
                arrayList.Add("Advertising.OptionalAdditionalRights", "Request Type");

                //Film Section Of Master Project
                arrayList.Add("Film.TV", "Request Type");
                arrayList.Add("Film.Cinema", "Request Type");
                arrayList.Add("Film.Video", "Request Type");
                arrayList.Add("Film.Internet", "Request Type");
                arrayList.Add("Film.Other", "Request Type");
                arrayList.Add("Film.OtherComments", "Request Type");
                arrayList.Add("Film.OptionalAdditionalRights", "Request Type");
                arrayList.Add("Film.InitialNoOfVideos", "Request Type");

                //Trailer Section of Master Project
                arrayList.Add("Trailer.TV", "Request Type");
                arrayList.Add("Trailer.Cinema", "Request Type");
                arrayList.Add("Trailer.Video", "Request Type");
                arrayList.Add("Trailer.Internet", "Request Type");
                arrayList.Add("Trailer.Other", "Request Type");
                arrayList.Add("Trailer.OtherComments", "Request Type");
                arrayList.Add("Trailer.OptionalAdditionalRights", "Request Type");
                arrayList.Add("Trailer.InitialNoOfVideos", "Request Type");

                //Other Section of Master Project
                arrayList.Add("Others.TV", "Request Type");
                arrayList.Add("Others.Cinema", "Request Type");
                arrayList.Add("Others.Video", "Request Type");
                arrayList.Add("Others.Internet", "Request Type");
                arrayList.Add("Others.Other", "Request Type");
                arrayList.Add("Others.OtherComments", "Request Type");
                arrayList.Add("Others.OptionalAdditionalRights", "Request Type");
                arrayList.Add("Others.InitialNoOfVideos", "Request Type");
                string ResubmitReasonComments = string.Empty;
                string finalResubmitReasonComments = string.Empty;

                List<KeyValuePair<long, string>> ReasonResourceResubmission = new List<KeyValuePair<long, string>>();

                bool flag1 = PublicInstancePropertiesEqual1(model.MasterProjectDetails, oldmodel.MasterProjectDetails, arrayList, out ResubmitReasonComments, finalResubmitReasonComments);
                finalResubmitReasonComments = ResubmitReasonComments;
                bool flag2 = PublicInstancePropertiesEqual1(model.MasterProjectDetails.Advertising, oldmodel.MasterProjectDetails.Advertising, arrayList, out ResubmitReasonComments, finalResubmitReasonComments);
                if (!string.IsNullOrEmpty(ResubmitReasonComments)) finalResubmitReasonComments = !string.IsNullOrEmpty(finalResubmitReasonComments) ? string.Format("{0},{1}", finalResubmitReasonComments, ResubmitReasonComments) : ResubmitReasonComments;
                bool flag3 = PublicInstancePropertiesEqual1(model.MasterProjectDetails.Film, oldmodel.MasterProjectDetails.Film, arrayList, out ResubmitReasonComments, finalResubmitReasonComments);
                if (!string.IsNullOrEmpty(ResubmitReasonComments)) finalResubmitReasonComments = !string.IsNullOrEmpty(finalResubmitReasonComments) ? string.Format("{0},{1}", finalResubmitReasonComments, ResubmitReasonComments) : ResubmitReasonComments;
                bool flag4 = PublicInstancePropertiesEqual1(model.MasterProjectDetails.Trailer, oldmodel.MasterProjectDetails.Trailer, arrayList, out ResubmitReasonComments, finalResubmitReasonComments);
                if (!string.IsNullOrEmpty(ResubmitReasonComments)) finalResubmitReasonComments = !string.IsNullOrEmpty(finalResubmitReasonComments) ? string.Format("{0},{1}", finalResubmitReasonComments, ResubmitReasonComments) : ResubmitReasonComments;
                bool flag5 = PublicInstancePropertiesEqual1(model.MasterProjectDetails.Others, oldmodel.MasterProjectDetails.Others, arrayList, out ResubmitReasonComments, finalResubmitReasonComments);
                if (!string.IsNullOrEmpty(ResubmitReasonComments)) finalResubmitReasonComments = !string.IsNullOrEmpty(finalResubmitReasonComments) ? string.Format("{0},{1}", finalResubmitReasonComments, ResubmitReasonComments) : ResubmitReasonComments;
                bool flag6 = CompareMasterTerritoryAtprojectlevel(model.MasterProjectDetails.Territories, oldmodel.MasterProjectDetails.Territories);

                if (!flag6)
                {
                    finalResubmitReasonComments = (!string.IsNullOrEmpty(finalResubmitReasonComments)) ? string.Format("{0},{1}", finalResubmitReasonComments, "Change in Territories") : "Change in Territories";
                }
                // for collecting resubmission reasons

                if ((flag1 == false) || (flag2 == false) || (flag3 == false) || (flag4 == false) || (flag5 == false) || ((flag6 == false)))
                {
                    listProjectLevel.Insert(0, 1);
                    ViewBag.ResubmitReasonComments = finalResubmitReasonComments;
                    CombinedList.Insert(1, listProjectLevel);
                    ProjectReason = finalResubmitReasonComments;
                }

                Session[Constants.Sessions.NewEntityMaster + model.MasterProjectDetails.ClearanceProjectID] = model;
                //if there is no change at project level

                CombinedList.Insert(1, listProjectLevel);

                List<int> MasterResourceCompareSpecifiedFieldsList = MasterResourceCompareSpecifiedFields(ReasonResourceResubmission, model.MasterProjectDetails.ClearanceProjectID);
                CombinedList.Insert(2, MasterResourceCompareSpecifiedFieldsList);//add the specified fields


                List<int> MasterResourceCompareUnSpecifiedFeidsList = CompareMasterResourceUnspecifiedFields(ReasonResourceResubmission, model.MasterProjectDetails.ClearanceProjectID);
                CombinedList.Insert(3, MasterResourceCompareUnSpecifiedFeidsList);//add the Unspecified fields list

                List<int> lstNewAddedResource = IsAddedNewResource(model.MasterProjectDetails.ClearanceProjectID);
                CombinedList.Insert(4, lstNewAddedResource);//add the Unspecified fields list

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { CombinedList = CombinedList,
                                  isSensitiveDataChanged = isUMGISensitiveDataChanged,
                                  sensitiveResourceListConditionChanged = sensitiveResourceListConditionChanged,
                                  projectReason = ProjectReason,
                                  reasonResourceResubmission = ReasonResourceResubmission
                            });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private bool IsUMGISensitiveDataChanged(MasterProject newMasterProject, MasterProject oldMasterProject)
        {
            try
            {
                bool isUMGISensitiveDataChanged = false;

                LoggerFactory.LogWriter.MethodStart();

                if (newMasterProject.SensitiveExplotation != oldMasterProject.SensitiveExplotation)
                {
                    isUMGISensitiveDataChanged = true;
                }

                LoggerFactory.LogWriter.MethodExit();

                return isUMGISensitiveDataChanged;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<long> GetUMGISensitiveDataChangedOnResource(List<ClearanceResource> newMasterProjectResources, List<ClearanceResource> oldMasterProjectResources)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<long> sensitiveList = new List<long>();
                foreach (ClearanceResource newResource in newMasterProjectResources)
                {
                    ClearanceResource oldresource = oldMasterProjectResources.Where(i => i.R2_ResourceId == newResource.R2_ResourceId).FirstOrDefault();
                    if (oldresource != null)
                    {
                        if (oldresource.SensitiveExplotation_ClearanceResource != newResource.SensitiveExplotation_ClearanceResource)
                        {
                            sensitiveList.Add(newResource.R2_ResourceId);
                        }
                    }
                    else
                    {
                        if (newResource.SensitiveExplotation_ClearanceResource)
                            sensitiveList.Add(newResource.R2_ResourceId);
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

                return sensitiveList;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        public List<int> MasterResourceCompareSpecifiedFields(List<KeyValuePair<long, string>> lstResourceReasons, long clrProjectId)
        {

            List<int> ListClearanceResourceId = new List<int>();

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = new ClearanceProjectModel();
                oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntityMaster + clrProjectId];

                ClearanceProjectModel model = new ClearanceProjectModel();
                model = (ClearanceProjectModel)Session[Constants.Sessions.NewEntityMaster + clrProjectId];
                model.MasterProjectDetails.IsSensitiveDataChanged = false;
                Session[Constants.Sessions.NewEntityMaster + clrProjectId] = model;
                List<ClearanceResource> NewModelResource = null;
                NewModelResource = new List<ClearanceResource>();
                NewModelResource = model.MasterProjectDetails.ClearanceResource;

                List<ClearanceResource> oldModelResource = null;
                oldModelResource = new List<ClearanceResource>();
                oldModelResource = oldmodel.MasterProjectDetails.ClearanceResource;

                LoggerFactory.LogWriter.Debug(string.Format("oldmodel: {0}", oldmodel));

                if (NewModelResource != null && oldModelResource != null)
                {
                    //check for all other resorce
                    if (oldModelResource != null)
                    {
                        for (int i = 0; i < oldModelResource.Count; i++)
                        {
                            //condition for all Resource
                            if (oldModelResource[i].ClearanceResourceId > 0)
                            {
                                //compare master resource territory
                                var newResourceList = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist).ToList();
                                bool flag = true;
                                if (newResourceList != null)
                                    flag = CompareRegularTerritory(oldModelResource[i].TerritorialRights, newResourceList[0].TerritorialRights);
                                if (!flag)
                                {
                                    ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Territory");

                                }

                                var newSensitiveExplotation_ClearanceResource = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.SensitiveExplotation_ClearanceResource).FirstOrDefault();
                                if ((oldmodel.MasterProjectDetails.SensitiveExplotation == true) && ((model.MasterProjectDetails.SensitiveExplotation == true)))
                                {

                                }
                                else
                                {
                                    if (oldModelResource[i].SensitiveExplotation_ClearanceResource != newSensitiveExplotation_ClearanceResource)
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Sensitive Explotation");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));

                                    }
                                }

                                //check for Excert Time
                                var newExcertTime = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.ExcerptTime).FirstOrDefault();
                                if (newExcertTime != null)
                                {
                                    string strNewTime = newExcertTime;
                                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                                    dtfi.ShortTimePattern = "hh:mm:ss";
                                    dtfi.TimeSeparator = ":";
                                    DateTime objNewDate;
                                    DateTime objOldDate;
                                    try
                                    {
                                        objNewDate = Convert.ToDateTime(strNewTime, dtfi);
                                    }
                                    catch
                                    {
                                        objNewDate = DateTime.Now;
                                    }

                                    string strOldTime = oldModelResource[i].ExcerptTime;
                                    DateTimeFormatInfo dtfiOld = new DateTimeFormatInfo();
                                    dtfi.ShortTimePattern = "hh:mm:ss";
                                    dtfi.TimeSeparator = ":";
                                    try
                                    {
                                        objOldDate = Convert.ToDateTime(strOldTime, dtfiOld);
                                    }
                                    catch
                                    {
                                        objOldDate = DateTime.Now;
                                    }

                                    if (objOldDate < objNewDate)
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Excert Time");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }
                                }
                                //check for Suggested Fee
                                var newSuggestedFee = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.SuggestedFee).FirstOrDefault();
                                if (newSuggestedFee != null)
                                {
                                    try
                                    {
                                        newSuggestedFee = Convert.ToDecimal(newSuggestedFee);
                                    }
                                    catch
                                    {
                                        newSuggestedFee = 11;
                                    }
                                    decimal OldSuggestionFee;
                                    try
                                    {
                                        OldSuggestionFee = Convert.ToDecimal(oldModelResource[i].SuggestedFee.ToString());
                                    }
                                    catch
                                    {
                                        OldSuggestionFee = 11;
                                    }

                                    if (Convert.ToDecimal(OldSuggestionFee) > Convert.ToDecimal(newSuggestedFee))
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Suggested Fee");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }
                                }
                            }

                            //condition for only Free Hand Resource
                            if (oldModelResource[i].ClearanceResourceId > 0 && oldModelResource[i].R2_ResourceId == 0)
                            {
                                //check for Artist
                                var newArtistInfo = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.ArtistName).FirstOrDefault();
                                if (newArtistInfo != null)
                                {
                                    if (oldModelResource[i].ArtistName.ToUpper() != newArtistInfo.ToUpper())
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Artist");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }

                                }

                                //check for Title
                                var newTitle = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.Title).FirstOrDefault();
                                if (newTitle != null)
                                {
                                    if (oldModelResource[i].Title.ToLower() != newTitle.ToLower())
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Title");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }
                                }

                                //check for Version Title
                                var newVersionTitle = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.VersionTitle).FirstOrDefault();

                                newVersionTitle = string.IsNullOrEmpty(newVersionTitle) ? newVersionTitle : newVersionTitle.ToUpper();
                                string OldVersionTitle = string.IsNullOrEmpty(oldModelResource[i].VersionTitle) ? oldModelResource[i].VersionTitle : oldModelResource[i].VersionTitle.ToUpper();
                                if (OldVersionTitle != newVersionTitle)
                                {
                                    model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Version Title");
                                    ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                }

                                //check for Recording Type
                                var newRecordingType = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.RecordingTypeDesc).FirstOrDefault();
                                if (newRecordingType != null)
                                {
                                    if (oldModelResource[i].RecordingTypeDesc.ToLower() != newRecordingType.ToLower())
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Recording Type");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }
                                }

                                //check for Resource Type
                                var newResourceType = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.ResourceTypeDesc).FirstOrDefault();
                                if (newResourceType != null)
                                {
                                    if (oldModelResource[i].ResourceTypeDesc.ToLower() != newResourceType.ToLower())
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Resource Type");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }
                                }

                                //check for Music Type
                                var newMusicType = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.MusicTypeDesc).FirstOrDefault();
                                if (newMusicType != null)
                                {
                                    if (oldModelResource[i].MusicTypeDesc.ToLower() != newMusicType.ToLower())
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Music Type");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }
                                }

                                //check for Duration
                                var newResourceDuration = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.MusicTime).FirstOrDefault();
                                if (newResourceDuration != null)
                                {
                                    if (oldModelResource[i].MusicTime != newResourceDuration)
                                    {
                                        model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = FormatString(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments, "Duration");
                                        ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments))
                            {
                                lstResourceReasons.Add(new KeyValuePair<long, string>(long.Parse(oldModelResource[i].ResourceId.ToString()), model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments));
                            }

                        }
                    }

                }

                LoggerFactory.LogWriter.MethodExit();

                Session[Constants.Sessions.NewEntityMaster + clrProjectId] = model;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return ListClearanceResourceId;
        }

        public List<int> CompareMasterResourceUnspecifiedFields(List<KeyValuePair<long, string>> lstResourceReasons, long clrProjectId)
        {
            List<int> ListClearanceResourceId = new List<int>();

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = new ClearanceProjectModel();
                oldmodel = (ClearanceProjectModel)Session[Constants.Sessions.OldEntityMaster + clrProjectId];

                ClearanceProjectModel model = new ClearanceProjectModel();
                model = (ClearanceProjectModel)Session[Constants.Sessions.NewEntityMaster + clrProjectId];
                model.MasterProjectDetails.IsSensitiveDataChanged = false;
                Session[Constants.Sessions.NewEntityMaster + clrProjectId] = model;
                List<ClearanceResource> NewModelResource = null;
                NewModelResource = new List<ClearanceResource>();
                NewModelResource = model.MasterProjectDetails.ClearanceResource;

                List<ClearanceResource> oldModelResource = null;
                oldModelResource = new List<ClearanceResource>();
                oldModelResource = oldmodel.MasterProjectDetails.ClearanceResource;

                if (NewModelResource != null && oldModelResource != null)
                {
                    for (int i = 0; i < oldModelResource.Count; i++)
                    {
                        //condition for all Resource
                        if (oldModelResource[i].ClearanceResourceId > 0)
                        {
                            //check for Comments
                            var NewComments = (from newlist in NewModelResource where newlist.ClearanceResourceId == oldModelResource[i].ClearanceResourceId select newlist.Comments).FirstOrDefault();

                            if (oldModelResource[i].Comments != NewComments)
                            {
                                model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments = "Comments";
                                ListClearanceResourceId.Add(Convert.ToInt32(oldModelResource[i].ResourceId.ToString()));

                                if (lstResourceReasons.Exists(r => r.Key == oldModelResource[i].ResourceId))
                                {
                                    string reasonDesc = lstResourceReasons.Where(r => r.Key == oldModelResource[i].ResourceId).First().Value;
                                    lstResourceReasons.Remove(new KeyValuePair<long, string>(oldModelResource[i].ResourceId, reasonDesc));
                                    reasonDesc = reasonDesc + "," + "Comments";
                                    lstResourceReasons.Add(new KeyValuePair<long, string>(oldModelResource[i].ResourceId, reasonDesc));
                                }
                                else
                                {
                                    lstResourceReasons.Add(new KeyValuePair<long, string>(oldModelResource[i].ResourceId, model.MasterProjectDetails.ClearanceResource[i].ResourceResubmitReasonComments));
                                }
                            }
                        }
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return ListClearanceResourceId;
        }

        public List<int> IsAddedNewResource(long clrProjectId)
        {
            List<int> lstNewAddedResource = new List<int>();

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel oldmodel = new ClearanceProjectModel();
                oldmodel = (ClearanceProjectModel)Session["OldEntityMaster" + "_" + clrProjectId];

                ClearanceProjectModel model = new ClearanceProjectModel();
                model = (ClearanceProjectModel)Session["NewEntityMaster" + "_" + clrProjectId];
                model.MasterProjectDetails.IsSensitiveDataChanged = false;

                //filter resoruce from free hand
                List<ClearanceResource> NewModelFilterResource = null;
                NewModelFilterResource = new List<ClearanceResource>();
                NewModelFilterResource = model.MasterProjectDetails.ClearanceResource.Where(x => x.ReplaceFreeHandFlag != "Y").ToList();
                //check if No free hand resoruce is replaced
                if (NewModelFilterResource != null)
                {
                    if (NewModelFilterResource.Count != oldmodel.MasterProjectDetails.ClearanceResource.Count)
                    {
                        lstNewAddedResource.Insert(0, 1);//new resource is added
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return lstNewAddedResource;
        }

        private bool UpdateRequestWaitingToCancel(ClearanceProjectModel model)
        {
            bool flag = false;
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                if (model.MasterProjectDetails.RequestInfoList != null && model.MasterProjectDetails.RequestInfoList.Count > 0)
                {
                    ClearanceRoutedProject clrRoutedProject = new ClearanceRoutedProject();
                    List<RoutedResource> routedResourceList = new List<RoutedResource>();
                    List<RoutedRequest> routedRequestList = new List<RoutedRequest>();
                    RoutedResource routedResource;
                    RoutedRequest routedRequest;
                    LeanUserInfo userinfo = new LeanUserInfo();
                    userinfo = getUserInfo();
                    clrRoutedProject.UserInfoDetails = userinfo;
                    clrRoutedProject.RoutingAction = RoutingAction.SystemCancel;
                    clrRoutedProject.ClrProjectId = model.MasterProjectDetails.ClrProjectId;
                    clrRoutedProject.StatusType = model.MasterProjectDetails.StatusType;
                    List<ClearanceInboxRequest> listRequest = new List<ClearanceInboxRequest>();
                    listRequest = model.MasterProjectDetails.RequestInfoList.ToList();
                    var serializer = new JavaScriptSerializer();
                    if (listRequest != null && listRequest.Count > 0)
                    {
                        //added to the list
                        for (int i = 0; i < listRequest.Count; i++)
                        {
                            routedResource = new RoutedResource();
                            routedRequest = new RoutedRequest();
                            var modifiedDateRouted = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRoutedString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            var modifiedRequestDate = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRequestString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            routedResource.LastModifiedDateTime = modifiedDateRouted;
                            routedRequest.LastModifiedDateTime = modifiedRequestDate;
                            routedResource.RoutedItemId = (int)listRequest[i].RoutedItemId;
                            routedRequest.WorkgroupId = Convert.ToInt32(listRequest[i].WorkgroupId);
                            routedRequest.RequestStatus = RequestStatus.Waiting;
                            routedRequestList.Add(routedRequest);

                            routedResource.Request = routedRequestList;
                            routedResourceList.Add(routedResource);

                        }
                        clrRoutedProject.RoutedResources = routedResourceList;
                        _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());
                    }

                }

                LoggerFactory.LogWriter.MethodExit();

                flag = true;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

            return flag;
        }

        private ClearanceRoutedProject UpdateRequestWaitingToCancelForRegular(ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                if (model.RegularProjectDetails.RequestInfoList != null && model.RegularProjectDetails.RequestInfoList.Count > 0)
                {
                    ClearanceRoutedProject clrRoutedProject = new ClearanceRoutedProject();
                    List<RoutedResource> routedResourceList = new List<RoutedResource>();
                    List<RoutedRequest> routedRequestList = new List<RoutedRequest>();
                    RoutedResource routedResource;
                    RoutedRequest routedRequest;
                    LeanUserInfo userinfo = new LeanUserInfo();
                    userinfo = getUserInfo();
                    clrRoutedProject.UserInfoDetails = userinfo;
                    clrRoutedProject.RoutingAction = RoutingAction.SystemCancel;
                    clrRoutedProject.ClrProjectId = model.RegularProjectDetails.ClrProjectId;
                    clrRoutedProject.StatusType = model.RegularProjectDetails.StatusType;
                    List<ClearanceInboxRequest> listRequest = new List<ClearanceInboxRequest>();
                    listRequest = model.RegularProjectDetails.RequestInfoList.ToList();//CANCEL
                    var serializer = new JavaScriptSerializer();

                    if (listRequest != null && listRequest.Count > 0)
                    {
                        //added to the list
                        for (int i = 0; i < listRequest.Count; i++)
                        {
                            routedResource = new RoutedResource();
                            routedRequest = new RoutedRequest();
                            var modifiedDateRouted = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRoutedString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            var modifiedRequestDate = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRequestString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            routedResource.LastModifiedDateTime = modifiedDateRouted;
                            routedResource.RoutedItemId = (int)listRequest[i].RoutedItemId;
                            routedRequest.LastModifiedDateTime = modifiedRequestDate;
                            routedRequest.WorkgroupId = Convert.ToInt32(listRequest[i].WorkgroupId);
                            routedRequest.RequestStatus = RequestStatus.Waiting;
                            routedRequestList.Add(routedRequest);

                            routedResource.Request = routedRequestList;
                            routedResourceList.Add(routedResource);
                        }

                        clrRoutedProject.RoutedResources = routedResourceList;

                    }
                    return clrRoutedProject;

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

        private ClearanceRoutedProject UpdateRequestCancelToWaitingForRegular(ClearanceProjectModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                if (model.RegularProjectDetails.RequestInfoList != null && model.RegularProjectDetails.RequestInfoList.Count > 0)
                {
                    ClearanceRoutedProject clrRoutedProject = new ClearanceRoutedProject();
                    List<RoutedResource> routedResourceList = new List<RoutedResource>();
                    List<RoutedRequest> routedRequestList = new List<RoutedRequest>();
                    RoutedResource routedResource;
                    RoutedRequest routedRequest;
                    LeanUserInfo userinfo = new LeanUserInfo();
                    userinfo = getUserInfo();
                    clrRoutedProject.UserInfoDetails = userinfo;
                    clrRoutedProject.RoutingAction = RoutingAction.SystemReInstate;
                    clrRoutedProject.ClrProjectId = model.RegularProjectDetails.ClrProjectId;
                    List<ClearanceInboxRequest> listRequest = new List<ClearanceInboxRequest>();

                    listRequest = model.RegularProjectDetails.RequestInfoList.ToList();
                    var serializer = new JavaScriptSerializer();
                    if (listRequest != null && listRequest.Count > 0)
                    {
                        //added to the list
                        for (int i = 0; i < listRequest.Count; i++)
                        {
                            routedResource = new RoutedResource();
                            routedRequest = new RoutedRequest();
                            var modifiedDateRouted = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRoutedString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            var modifiedRequestDate = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRequestString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            routedResource.LastModifiedDateTime = modifiedDateRouted;
                            routedRequest.LastModifiedDateTime = modifiedRequestDate;
                            routedResource.RoutedItemId = (int)listRequest[i].RoutedItemId;
                            routedRequest.WorkgroupId = Convert.ToInt32(listRequest[i].WorkgroupId);
                            routedRequest.RequestStatus = RequestStatus.Rejected;
                            routedRequestList.Add(routedRequest);

                            routedResource.Request = routedRequestList;
                            routedResourceList.Add(routedResource);
                        }

                        clrRoutedProject.RoutedResources = routedResourceList;

                    }

                    LoggerFactory.LogWriter.MethodExit();

                    return clrRoutedProject;
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

        private bool UpdateRequestCancelToWaiting(ClearanceProjectModel model)
        {

            bool flag = false;
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                
                if (model.MasterProjectDetails.RequestInfoList != null && model.MasterProjectDetails.RequestInfoList.Count > 0)
                {
                    ClearanceRoutedProject clrRoutedProject = new ClearanceRoutedProject();
                    List<RoutedResource> routedResourceList = new List<RoutedResource>();
                    List<RoutedRequest> routedRequestList = new List<RoutedRequest>();
                    RoutedResource routedResource;
                    RoutedRequest routedRequest;
                    LeanUserInfo userinfo = new LeanUserInfo();
                    userinfo = getUserInfo();
                    clrRoutedProject.UserInfoDetails = userinfo;
                    clrRoutedProject.RoutingAction = RoutingAction.SystemReInstate;
                    clrRoutedProject.ClrProjectId = model.MasterProjectDetails.ClrProjectId;
                    List<ClearanceInboxRequest> listRequest = new List<ClearanceInboxRequest>();
                    listRequest = model.MasterProjectDetails.RequestInfoList.ToList();
                    var serializer = new JavaScriptSerializer();
                    if (listRequest != null && listRequest.Count > 0)
                    {
                        //added to the list
                        for (int i = 0; i < listRequest.Count; i++)
                        {
                            routedResource = new RoutedResource();
                            routedRequest = new RoutedRequest();
                            var modifiedDateRouted = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRoutedString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            var modifiedRequestDate = serializer.Deserialize<DateTime>(listRequest[i].ModifiedDateRequestString.ToString(CultureInfo.InvariantCulture)).ToLocalTime();
                            routedResource.LastModifiedDateTime = modifiedDateRouted;
                            routedRequest.LastModifiedDateTime = modifiedRequestDate;
                            routedResource.RoutedItemId = (int)listRequest[i].RoutedItemId;
                            routedRequest.WorkgroupId = Convert.ToInt32(listRequest[i].WorkgroupId);
                            routedRequest.RequestStatus = RequestStatus.Rejected;
                            routedRequestList.Add(routedRequest);

                            routedResource.Request = routedRequestList;
                            routedResourceList.Add(routedResource);
                        }

                        clrRoutedProject.RoutedResources = routedResourceList;
                        _IClearanceProjectRepository.ClearanceRoutingAction(clrRoutedProject, getUserInfo());
                    }
                    
                }

                LoggerFactory.LogWriter.MethodExit();

                flag = true;
                return flag;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description :
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetReleasesOfRegularProject(int clrprojectId, string NeworExisting, int projectStatus, int requestWorkGroupId, BusinessEntities.Lookups.RoleGroup roleGroup, string multiArtist)
        {
            ClearanceProjectModel model = new ClearanceProjectModel();
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceRegularProject regularproject = new ClearanceRegularProject();
                string ViewName = "ReleaseNewRegular";
                if (clrprojectId != 0)
                {
                    regularproject = _IClearanceProjectRepository.GetRegularProjectReleases(clrprojectId, getUserInfo());
                    model.RegularProjectDetails.ObjRelease = regularproject.ObjRelease;
                    model.RegularProjectDetails.ReleaseNewOrExisting = NeworExisting;
                    List<string> list = RegularListTooltipControl(model.RegularProjectDetails.ReleaseNewOrExisting);
                    ViewBag.ControlsRegularList = list;
                    ClearanceProjectModel clrprojectmodel = new ClearanceProjectModel();
                    clrprojectmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + clrprojectId];
                    if (clrprojectmodel.RegularProjectDetails.StatusType == 3 || clrprojectmodel.RegularProjectDetails.StatusType == 4)
                        ViewName = "ReleaseRegularReadOnly";

                    if (regularproject.ObjRelease.Count > 0)
                        clrprojectmodel.RegularProjectDetails.ObjRelease = regularproject.ObjRelease;
                    if (regularproject != null)
                    {
                        for (int i = 0; i < regularproject.ObjRelease.Count; i++)
                        {
                            clrprojectmodel.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(clrprojectmodel.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();
                        }
                    }
                    Session["OldEntity" + "_" + clrprojectId] = clrprojectmodel;
                }
                else
                {
                    ClearanceReleaseSearchResult releaseDetailGRSData = new ClearanceReleaseSearchResult();
                    ClearanceRelease releaseNew = new ClearanceRelease();
                    releaseNew.Archive_Flag = "N";
                    releaseNew.TrackCount = null;
                    releaseNew.No_Components = null;
                    releaseNew.IsNewlyAddedAfterSubmit = true;

                    releaseDetailGRSData.releaseDetail = new List<ClearanceRelease>();
                    releaseDetailGRSData.releaseDetail.Add(releaseNew);
                    model.RegularProjectDetails.ObjRelease = releaseDetailGRSData.releaseDetail;
                    if (model.RegularProjectDetails != null)
                    {
                        for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                        {
                            model.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(model.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();

                        }
                    }
                }

                _type = new List<string>
                    {
                        Constants.ClearanceMusicType,
                        Constants.ClearancePriceLevelType,
                        Constants.ClearanceClubPriceLevel,
                        Constants.ClearanceICLALevelType,
                        Constants.ClearanceCurrPriceLevelType,
                        Constants.ClearanceReqPriceLevelType,
                        Constants.ClearacePromotionalPriceLevel
                    };

                var iClearanceProjectModel = _IClearanceProjectRepository.GetMasterData(_type, getUserInfo());
                model.RegularProjectDetails.StatusType = projectStatus;
                model.RegularProjectDetails.MultiArtist = multiArtist == "True" || multiArtist == "true" || multiArtist == "TRUE" ? true : false;
                model.MusicType = iClearanceProjectModel.MusicType;
                model.MusicTypeResourceTab = iClearanceProjectModel.MusicType;
                model.PriceLevelType = iClearanceProjectModel.PriceLevelType;
                model.dropClubLevel = iClearanceProjectModel.dropClubLevel;
                model.ICLALevelType = iClearanceProjectModel.ICLALevelType;
                model.CurrPriceLevelList = iClearanceProjectModel.CurrPriceLevelList;
                model.RequestedPriceLevelList = iClearanceProjectModel.RequestedPriceLevelList;
                model.dropPromotionalLevel = iClearanceProjectModel.dropPromotionalLevel;
                model.dropDeviatedIclaLevel = model.ICLALevelType.Select(i => new DropDeviatedICLALevel { Id = int.Parse(i.Value), Description = i.Text }).ToList();
                model.dropPriceLevel = model.PriceLevelType.Select(p => new DropPriceLevel { Id = int.Parse(p.Value), Description = p.Text }).ToList();
                var temp = new List<ListItem>();
                model.ConfigList = from t in temp select t;
                model.ConfigGroupList = _IClearanceReleaseRepository.GetReleaseConfigGroupList(getUserInfo()).ConfigGroupList;
                model.UPCAllocationRightsGroup = checkPermissionForUPCAllocation(requestWorkGroupId, roleGroup);

                LoggerFactory.LogWriter.MethodExit();

                return PartialView(ViewName, model);
            }
            catch (Exception ex)
            {
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error in Loading Release" });
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description :
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetResourcesOfRegularProject(int clrprojectId, string NeworExisting, string ActiveRoleGroup = null)
        {
            string ViewName = string.Empty;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel model = new ClearanceProjectModel();
                ClearanceRegularProject regularproject = new ClearanceRegularProject();
                ClearanceProjectModel clrprojectmodel = new ClearanceProjectModel();

                if (clrprojectId != 0)
                {
                    if (Session["OldEntity" + "_" + clrprojectId] != null)
                        clrprojectmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + clrprojectId];

                    if (clrprojectmodel != null)
                        model = clrprojectmodel;

                    if (ActiveRoleGroup != "Requestor" && ActiveRoleGroup != null)
                        model.ReadOnlyMode = 1;
                    else
                        model.ReadOnlyMode = 0;

                    regularproject = _IClearanceProjectRepository.GetRegularProjectResources(clrprojectId, getUserInfo());
                    model.RegularProjectDetails.ClearanceResource = regularproject.ClearanceResource;
                    model.RegularProjectDetails.ReleaseNewOrExisting = NeworExisting;
                    if (model.RegularProjectDetails.ClearanceResource != null)
                    {
                        foreach (var resource in model.RegularProjectDetails.ClearanceResource)
                        {

                            if (resource.LiveStudioType == ((int)General.LiveStudioType.Live).ToString())
                                resource.RecordingTypeDesc = (General.LiveStudioType.Live).ToString();
                            else if (resource.LiveStudioType == ((int)General.LiveStudioType.Studio).ToString())
                                resource.RecordingTypeDesc = General.LiveStudioType.Studio.ToString();

                            if (resource.ResourceType == ((int)General.ResourceType.Audio).ToString())
                                resource.ResourceTypeDesc = General.ResourceType.Audio.ToString();
                            else if (resource.ResourceType == ((int)General.ResourceType.Image).ToString())
                                resource.ResourceTypeDesc = (General.ResourceType.Image).ToString();
                            else if (resource.ResourceType == ((int)General.ResourceType.Merchandise).ToString())
                                resource.ResourceTypeDesc = General.ResourceType.Merchandise.ToString();
                            else if (resource.ResourceType == ((int)General.ResourceType.Other).ToString())
                                resource.ResourceTypeDesc = General.ResourceType.Other.ToString();
                            else if (resource.ResourceType == ((int)General.ResourceType.Text).ToString())
                                resource.ResourceTypeDesc = General.ResourceType.Text.ToString();

                            if (resource.MusicClassType == ((int)General.MusicClassType.Classical).ToString())
                                resource.MusicTypeDesc = General.MusicClassType.Classical.ToString();
                            else if (resource.MusicClassType == ((int)General.MusicClassType.Jazz).ToString())
                                resource.MusicTypeDesc = General.MusicClassType.Jazz.ToString();
                            else if (resource.MusicClassType == ((int)General.MusicClassType.Pop).ToString())
                                resource.MusicTypeDesc = General.MusicClassType.Pop.ToString();
                            else if (resource.MusicClassType == ((int)General.MusicClassType.Other).ToString())
                                resource.MusicTypeDesc = General.MusicClassType.Other.ToString();

                            if (resource.SuggestedFee != null)
                            {
                                decimal suggestfee;
                                suggestfee = Convert.ToDecimal(resource.SuggestedFee);
                                resource.SuggestedFee = Convert.ToDecimal(suggestfee.ToString("F02", CultureInfo.InvariantCulture));

                            }

                        }

                    }

                }

                if (model.RegularProjectDetails.ClearanceResource != null)
                {
                    foreach (var resource in model.RegularProjectDetails.ClearanceResource.Where(i => i.R2_ResourceId == 0))
                    {
                        // Get Master Data for Resource Tab
                        _type = new List<string>
                    {
                        Constants.ClearanceMusicType,
                        Constants.ClearanceResourceType,
                        Constants.ClearanceRecordingType

                    };
                        var iClearanceProjectModel = _IClearanceProjectRepository.GetMasterData(_type, getUserInfo());
                        model.MusicTypeResourceTab = iClearanceProjectModel.MusicType.Where(m => m.Text.ToUpper() != "JAZZ").ToList();
                        iClearanceProjectModel.ResourceType = iClearanceProjectModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
                        model.ResourceTypeResourceTab = iClearanceProjectModel.ResourceType;
                        model.RecordingTypeResourceTab = iClearanceProjectModel.RecordingType;
                        break;
                    }
                    if (model.RegularProjectDetails.ClearanceResource != null)
                        model.RegularProjectDetails.ClearanceResource = model.RegularProjectDetails.ClearanceResource.OrderBy(Resourcelist => Resourcelist.ArtistName).ThenBy(Resourcelist => Resourcelist.Title).ThenBy(Resourcelist => Resourcelist.VersionTitle).ToList();

                    var Territories = new Dictionary<long, List<TerritorialDisplay>>();
                    Territories.Add(1, model.RegularProjectDetails.Territories);
                    if (model.RegularProjectDetails.ScopeAndRequestType != null)
                        Territories.Add(2, model.RegularProjectDetails.ScopeAndRequestType.Territories);
                    int tempI = 2;
                    foreach (ClearanceResource cls in model.RegularProjectDetails.ClearanceResource)
                    {
                        Territories.Add(++tempI, cls.TerritorialRights);
                    }

                    ViewBag.ProjectTerritories = Territories;

                    List<string> list = RegularListTooltipControl(model.RegularProjectDetails.ReleaseNewOrExisting);
                    ViewBag.ControlsRegularList = list;
                    if (model.RegularProjectDetails.StatusType == 3 || model.RegularProjectDetails.StatusType == 4)
                        model.ReadOnlyMode = 1;

                }

                if (clrprojectId > 0)
                    Session["OldEntity" + "_" + clrprojectId] = model;

                if (model.ReadOnlyMode == 1)
                    ViewName = "ResourceRegularReadOnly";
                else
                    ViewName = "ResourcesRegular";

                LoggerFactory.LogWriter.MethodExit();

                return PartialView(ViewName, model);
            }
            catch (Exception ex)
            {
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error in Loading Resource" });
            }
        }

        /// <summary>
        /// Created By : Dhruv
        /// Description :
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetExistingReleasesOfRegularProject(int clrprojectId, string NeworExisting, string ActiveRoleGroup = null)
        {
           
            string ViewName = string.Empty;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel model = new ClearanceProjectModel();
                ClearanceRegularProject regularproject = new ClearanceRegularProject();

                if (clrprojectId != 0)
                {
                    regularproject = _IClearanceProjectRepository.GetRegularProjectReleases(clrprojectId, getUserInfo());
                    model.RegularProjectDetails.ObjRelease = regularproject.ObjRelease;
                    model.RegularProjectDetails.ReleaseNewOrExisting = NeworExisting;
                    model.RegularProjectDetails.StatusType = regularproject.StatusType;

                    List<string> list = RegularListTooltipControl(model.RegularProjectDetails.ReleaseNewOrExisting);
                    ViewBag.ControlsRegularList = list;
                    ClearanceProjectModel clrprojectmodel = new ClearanceProjectModel();
                    clrprojectmodel = (ClearanceProjectModel)Session["OldEntity" + "_" + clrprojectId];
                    if (regularproject.ObjRelease.Count > 0)
                        clrprojectmodel.RegularProjectDetails.ObjRelease = regularproject.ObjRelease;
                    Session["OldEntity" + "_" + clrprojectId] = clrprojectmodel;
                    if (clrprojectmodel.RegularProjectDetails != null)
                    {
                        model.ReadOnlyMode = clrprojectmodel.ReadOnlyMode;
                        if (ActiveRoleGroup != "Requestor" && ActiveRoleGroup != null)
                            model.ReadOnlyMode = 1;
                        else
                            model.ReadOnlyMode = 0;
                        model.RegularProjectDetails.StatusType = clrprojectmodel.RegularProjectDetails.StatusType;
                        if (model.RegularProjectDetails.StatusType == 3 || model.RegularProjectDetails.StatusType == 4)
                            model.ReadOnlyMode = 1;
                    }
                }
                _type = new List<string>
                    {
                        Constants.ClearanceMusicType,
                        Constants.ClearancePriceLevelType,
                        Constants.ClearanceClubPriceLevel,
                        Constants.ClearanceICLALevelType,
                        Constants.ClearanceCurrPriceLevelType,
                        Constants.ClearanceReqPriceLevelType,
                        Constants.ClearacePromotionalPriceLevel
                    };

                var iClearanceProjectModel = _IClearanceProjectRepository.GetMasterData(_type, getUserInfo());
                model.CurrPriceLevelList = iClearanceProjectModel.CurrPriceLevelList;
                model.RequestedPriceLevelList = iClearanceProjectModel.RequestedPriceLevelList;
                model.MusicType = iClearanceProjectModel.MusicType;
                model.PriceLevelType = iClearanceProjectModel.PriceLevelType;
                model.dropClubLevel = iClearanceProjectModel.dropClubLevel;
                model.ICLALevelType = iClearanceProjectModel.ICLALevelType;
                model.dropPromotionalLevel = iClearanceProjectModel.dropPromotionalLevel;
                model.dropDeviatedIclaLevel = model.ICLALevelType.Select(i => new DropDeviatedICLALevel { Id = int.Parse(i.Value), Description = i.Text }).ToList();
                model.dropPriceLevel = model.PriceLevelType.Select(p => new DropPriceLevel { Id = int.Parse(p.Value), Description = p.Text }).ToList();

                if (model.ReadOnlyMode == 1)
                    ViewName = "ReleaseRegularReadOnly";
                else
                    ViewName = "ReleaseRegular";

                LoggerFactory.LogWriter.MethodExit();

                return PartialView(ViewName, model);
            }
            catch (Exception ex)
            {
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Error = true, Message = "Error in Loading Release" });
            }
        }

        public ActionResult AllocateUPC()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return View(_IClearanceProjectModel);
            }

            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }


        }

        public JsonResult SearchProjectToAllocateUPC(ClearanceProjectSearchCriteria projectSearchCriteria)
        {

            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("ProjectTitle: {0}", projectSearchCriteria.ProjectReferenceId));

                int totalRowCount = 0;
                ProjectSearchResult projectList = _IClearanceProjectRepository.SearchProjectToAllocateUPC(projectSearchCriteria, getUserInfo());

                if (projectList != null)
                    totalRowCount = projectList.TotalRows;
                
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = projectList.Values.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Error", ex.Message });
            }
        }

        public ActionResult ClearanceProjectLocking()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                return View(_IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        public JsonResult SearchClearanceUnlockedProjects(ClearanceProjectSearchCriteria projectSearchCriteria)
        {

            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("ProjectTitle: {0}", projectSearchCriteria.ProjectReferenceId));

                int totalRowCount = 0;
                ProjectSearchResult projectList = _IClearanceProjectRepository.SearchClearanceUnlockedProjects(projectSearchCriteria, getUserInfo());

                if (projectList != null)
                    totalRowCount = projectList.TotalRows;

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = projectList.Values.AsQueryable(), TotalRecordCount = totalRowCount });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Error", ex.Message });
            }
        }

        public JsonResult UnlockProject(string projectId)
        {

            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("ProjectTitle: {0}", projectId));

                long unlockProjectId = 0;
                unlockProjectId = long.Parse(projectId);
                _IClearanceProjectRepository.UnlockProject(unlockProjectId, getUserInfo());

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = "Ok" });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "Error", ex.Message });
            }
        }

        public ActionResult GetAuthorizationsForR2Search()
        {
            Boolean data = false;

            try
            {
                LoggerFactory.LogWriter.MethodStart();
                data = _IClearanceProjectRepository.GetAuthorizationsForR2(SessionWrapper.CurrentUserInfo.UserLoginName, "Search Project", AnaTargetApplication.R2, string.Empty);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            LoggerFactory.LogWriter.MethodExit();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAuthorizationsForR2Create()
        {
            Boolean data = false;

            try
            {
                LoggerFactory.LogWriter.MethodStart();
                data = _IClearanceProjectRepository.GetAuthorizationsForR2(SessionWrapper.CurrentUserInfo.UserLoginName, "Create Project", AnaTargetApplication.R2, string.Empty);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            LoggerFactory.LogWriter.MethodExit();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0)]
        public JsonResult AutoCompleteSearchForDAG(SearchCriteria autoSearch)
        {
            try
            {
                // Get Tags from database
                LoggerFactory.LogWriter.MethodStart();
                List<ClearanceAdminCompany> lstCompanyData = new List<ClearanceAdminCompany>();
                lstCompanyData = _IClearanceProjectRepository.AutoCompleteCreateSearchProject(autoSearch, SessionWrapper.CurrentUserInfo.UserId);
                LoggerFactory.LogWriter.MethodExit();
                return Json(_IClearanceProjectRepository.AutoCompleteCreateSearchProject(autoSearch, SessionWrapper.CurrentUserInfo.UserId).Select(item => new AutoSuggestionEntity { id = Convert.ToInt32(item.Id), value = item.Name, label = item.Name }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [OutputCache(Duration = 0)]
        public JsonResult AutoCompleteSearchForLabels(SearchCriteria autoSearch)
        {
            try
            {
                // Get Tags from database
                LoggerFactory.LogWriter.MethodStart();
                List<ClearanceMasterData> lstlabelData = new List<ClearanceMasterData>();
                lstlabelData = _IClearanceProjectRepository.AutoCompleteSearchLabels(autoSearch, SessionWrapper.CurrentUserInfo.UserId);
                LoggerFactory.LogWriter.MethodExit();
                return Json(_IClearanceProjectRepository.AutoCompleteSearchLabels(autoSearch, SessionWrapper.CurrentUserInfo.UserId).Select(item => new AutoSuggestionEntity { id = Convert.ToInt32(item.Value), value = item.Description, label = item.Description }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }

        public ActionResult PermissionPushToR2SubsequentPush(ClearanceProjectModel model, FormCollection collection)
        {
            
            Boolean data = false;
            // check for Push To R2 (first push) rights
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (PermissionExtension.HasAnyPermission(new[] { GcsTasks.PushToR2SubsequentPush }) || PermissionExtension.HasAnyPermission(new[] { GcsTasks.PushToR2FirstPush }))
                    data = true;

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private string checkPermissionForUPCAllocation(int requestWorkgroupId, BusinessEntities.Lookups.RoleGroup roleGroup)
        {
            string userRole = string.Empty;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (roleGroup == BusinessEntities.Lookups.RoleGroup.RCCAdmin)
                {
                    userRole = BusinessEntities.Lookups.RoleGroup.RCCAdmin.ToString();
                }
                else if (roleGroup == BusinessEntities.Lookups.RoleGroup.Requestor)
                {

                    if (_IClearanceProjectRepository.HasSpecialPermission(SessionWrapper.GcsCurrentPermissions.UserId, requestWorkgroupId, false))
                    {
                        if (PermissionExtension.HasAnyPermission(new[] { GcsTasks.UpCallocation }))
                            userRole = BusinessEntities.Lookups.RoleGroup.Requestor.ToString();
                    }
                }

                LoggerFactory.LogWriter.MethodExit();

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
            }
                      
            return userRole;
        }

        public ActionResult PermissionPushToR2FirstPush(ClearanceProjectModel model, FormCollection collection)
        {
            string data = string.Empty;
            bool IsR2Authorized = false;

            try
            {
                LoggerFactory.LogWriter.MethodStart();

                ClearanceProjectModel modelFromDb = new ClearanceProjectModel();

                if (Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId] != null)
                    modelFromDb = (ClearanceProjectModel)Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId];
                else
                {
                    _IClearanceProjectModel.RegularProjectDetails = _IClearanceProjectRepository.GetRegularProjectDetails(Convert.ToInt32(model.RegularProjectDetails.ClrProjectId), getUserInfo());
                    modelFromDb.RegularProjectDetails = _IClearanceProjectModel.RegularProjectDetails;
                    Session["OldEntity" + "_" + model.RegularProjectDetails.ClrProjectId] = modelFromDb;
                }

                IsR2Authorized = _IClearanceProjectRepository.HasSpecialPermission(SessionWrapper.GcsCurrentPermissions.UserId, modelFromDb.RegularProjectDetails.RequesterCompanyId, true);

                if (IsR2Authorized)
                    if (PermissionExtension.HasAnyPermission(new[] { GcsTasks.PushToR2FirstPush }))
                        data = "Valid";

                if (modelFromDb.RegularProjectDetails.ThirdParty && modelFromDb.RegularProjectDetails.ThirdPartyCompanyID <= 0)
                    data = data + '~' + "freehand";

                LoggerFactory.LogWriter.MethodExit();

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private void SetMasterProjectUploadDocModel(ClearanceProjectModel model, List<UploadDocument> listUploadDocument)
        {
            LoggerFactory.LogWriter.MethodStart();

            model.MasterProjectDetails.listUploadDocument = new List<UploadDocument>();
            foreach (UploadDocument uploadDocument in listUploadDocument)
            {
                model.MasterProjectDetails.listUploadDocument.Add(new UploadDocument
                {
                    Id = uploadDocument.Id,
                    Name = uploadDocument.Name,
                    Type = uploadDocument.Type
                });
            }
            model = SetSelectedDropDown(model);

            LoggerFactory.LogWriter.MethodExit();
        }

        private void SetRegularProjectUploadDocModel(ClearanceProjectModel model, List<UploadDocument> listUploadDocument)
        {
            LoggerFactory.LogWriter.MethodStart();

            model.RegularProjectDetails.listUploadDocument = new List<UploadDocument>();
            foreach (UploadDocument uploadDocument in listUploadDocument)
            {
                model.RegularProjectDetails.listUploadDocument.Add(new UploadDocument
                {
                    Id = uploadDocument.Id,
                    Name = uploadDocument.Name,
                    Type = uploadDocument.Type
                });
            }
            LoggerFactory.LogWriter.MethodExit();

        }

        private List<UploadDocument> PopulateUploadedDocs(List<UploadDocument> uploadedDocsList)
        {
            LoggerFactory.LogWriter.MethodStart();

            var listUploadDocument = new List<UploadDocument>();
            foreach (UploadDocument uploadDocument in uploadedDocsList)
            {
                listUploadDocument.Add(new UploadDocument
                {
                    Id = uploadDocument.Id,
                    Name = uploadDocument.Name,
                    Type = uploadDocument.Type
                });
            }
            LoggerFactory.LogWriter.MethodExit();

            return listUploadDocument;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetRequestType(ResourceSearch Input)
        {
            IEnumerable<SelectListItem> RequestTypeList = null;
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                RequestTypeList = _IClearanceProjectRepository.GetRequestType(Input.PageSize).Select(RequestType => new SelectListItem { Text = RequestType.Name, Value = RequestType.Id.ToString() });
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { success = true, Records = RequestTypeList });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "cancel", ex.Message });
            }
        }

        private void SetClearanceProjectEntity(ClearanceProject clrProject, bool isMaster)
        {
            LoggerFactory.LogWriter.MethodStart();

            long clrProjectSession = 0;
            if (isMaster == true)
            {
                clrProject.ProjectType = Convert.ToInt32(General.ProjectType.Master);
                clrProjectSession = clrProject.ClearanceProjectID;
            }
            else
            {
                clrProject.ProjectType = Convert.ToInt32(General.ProjectType.Regular);
                clrProjectSession = clrProject.ClrProjectId;
            }
            clrProject.IsMaster = isMaster;

            if (Session["fileList" + "_" + clrProjectSession] == null)
            {
                clrProject.listUploadDocument = new List<UploadDocument>();
            }
            else
            {
                clrProject.listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + clrProjectSession];
            }
            LoggerFactory.LogWriter.MethodExit();
        }
        
        private ClearanceProjectModel SetArtistDetailsForResource(ClearanceProjectModel model, FormCollection collection)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (model.MasterProjectDetails.ClearanceResource != null)
            {
                for (int i = 0; i < model.MasterProjectDetails.ClearanceResource.Count; i++)
                {

                    if (!string.IsNullOrEmpty(collection["hdnArtist" + i.ToString()]))
                    {
                        if (model.MasterProjectDetails.ClearanceResource[i].R2_ResourceId == 0)
                        {
                            model.MasterProjectDetails.ClearanceResource[i].ArtistInfo = ParseArtistforResource(model.MasterProjectDetails.ClearanceResource[i], collection["hdnArtist" + i.ToString()]);
                        }
                    }

                    if (model.MasterProjectDetails.ClearanceResource[i].ArtistInfo != null)
                    {
                        foreach (var artist in model.MasterProjectDetails.ClearanceResource[i].ArtistInfo)
                        {
                            artist.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                            if (model.MasterProjectDetails.ClearanceResource[i].R2_ResourceId == 0)
                            {
                                artist.IsPrimary = "Y";
                            }

                        }
                    }
                }

            }

            LoggerFactory.LogWriter.MethodExit();

            return model;
        }

        private List<ArtistInfo> ParseArtistforResource(ClearanceResource clearanceResource, string artistString)
        {
            LoggerFactory.LogWriter.MethodStart();

            string[] artistDetail = artistString.Split('=');
            List<ArtistInfo> listArtistInfo = new List<ArtistInfo>();

            clearanceResource.ArtistName = string.Empty;

            for (int i = 0; i < artistDetail.Length - 1; i++)
            {
                string[] strArtistDetail;

                if (artistDetail[i] != string.Empty)
                {
                    strArtistDetail = artistDetail[i].Split(':');
                    ArtistInfo artist = new ArtistInfo();

                    artist.Name = strArtistDetail[0];
                    artist.Id = Convert.ToInt64(strArtistDetail[2]);
                    artist.NameId = Convert.ToInt64(strArtistDetail[1]);

                    if ((clearanceResource.ArtistName.Trim() == string.Empty))
                        clearanceResource.ArtistName = artist.Name;
                    else
                        clearanceResource.ArtistName = clearanceResource.ArtistName + "," + artist.Name;

                    artist.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                    listArtistInfo.Add(artist);

                }
            }
            LoggerFactory.LogWriter.MethodExit();

            return listArtistInfo;
        }

        private void SetModelProperties(string clrProjectId, bool isMaster, string fileName)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("fileName{0}", fileName));

                List<UploadDocument> listUploadDocument = new List<UploadDocument>();
                string clrProjectIdSession = string.Empty;

                string[] rowData = fileName.Split('~');
                if (isMaster)
                    clrProjectIdSession = clrProjectId;
                else
                    clrProjectIdSession = clrProjectId;

                if (Session["fileList" + "_" + clrProjectIdSession] != null)
                {
                    listUploadDocument = (List<UploadDocument>)Session["fileList" + "_" + clrProjectIdSession];
                    listUploadDocument = listUploadDocument.Except(listUploadDocument.Where(a => a.Name == rowData[0])).ToList();
                    Session["fileList" + "_" + clrProjectIdSession] = listUploadDocument;
                }

                int documentId = rowData[1].Length > 0 ? int.Parse(rowData[1]) : 0;

                if (documentId > 0)
                    _IClearanceProjectRepository.RemoveFile(documentId, getUserInfo());

                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Add New Releases
        /// </summary>
        /// <param name="clearanceProjectModel"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult AddRelease(ClearanceProjectModel clearanceProjectModel, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                #region Fill Master Data
                CacheData(clearanceProjectModel, "");
                var MusicType = clearanceProjectModel.MusicType.Where(x => x.Text.ToUpper() != "JAZZ").ToList();
                clearanceProjectModel.MusicType = MusicType;
                #endregion
                if (clearanceProjectModel.RegularProjectDetails.ObjRelease != null && clearanceProjectModel.RegularProjectDetails.ObjRelease.Count > 0)
                {
                    for (int i = 0; i < clearanceProjectModel.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        clearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigIdSelected = clearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigId;
                        clearanceProjectModel.RegularProjectDetails.ObjRelease[i].ArtistInfo = ParseArtistforRelease(clearanceProjectModel.RegularProjectDetails.ObjRelease[i], collection["hdnArtist" + i]);
                    }

                    clearanceProjectModel.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(clearanceProjectModel.RegularProjectDetails.ObjRelease);
                    ClearanceRelease releaseNew = new ClearanceRelease();
                    releaseNew.Archive_Flag = "N";
                    releaseNew.IsNewlyAddedAfterSubmit = true;
                    releaseNew.TrackCount = null;
                    releaseNew.No_Components = null;
                    clearanceProjectModel.RegularProjectDetails.ObjRelease.Add(releaseNew);
                    for (int i = 0; i < clearanceProjectModel.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        clearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(clearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();
                    }
                }
                else
                {
                    ClearanceReleaseSearchResult releaseDetailGRSData = new ClearanceReleaseSearchResult();
                    ClearanceRelease releaseNew = new ClearanceRelease();
                    releaseNew.Archive_Flag = "N";
                    releaseNew.IsNewlyAddedAfterSubmit = true;
                    releaseNew.TrackCount = null;
                    releaseNew.No_Components = null;
                    releaseDetailGRSData.releaseDetail = new List<ClearanceRelease>();
                    releaseDetailGRSData.releaseDetail.Add(releaseNew);
                    clearanceProjectModel.RegularProjectDetails.ObjRelease = releaseDetailGRSData.releaseDetail;

                    for (int i = 0; i < clearanceProjectModel.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        clearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(clearanceProjectModel.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();
                    }

                }
                ModelState.Clear();
                ViewBag.DefaultTab = "2";
                LoggerFactory.LogWriter.MethodExit();
                return PartialView("ReleaseNewRegular", clearanceProjectModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("ReleaseNewRegular", clearanceProjectModel);
            }
        }

        private string FormatString(string prvstring, string newstring)
        {
            LoggerFactory.LogWriter.MethodStart();

            if (string.IsNullOrEmpty(prvstring))
                prvstring = newstring;
            else
                prvstring = string.Format("{0},{1}", prvstring, newstring);

            LoggerFactory.LogWriter.MethodExit();

            return prvstring;
        }

        public JsonResult UnlockProjectWhenSesionExpire()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                string encodedQueryStringData = Request.UrlReferrer.Query.ToString();

                if (!(string.IsNullOrEmpty(encodedQueryStringData)))
                {
                    encodedQueryStringData = encodedQueryStringData.Replace("?enc=", "");
                    string decodedQueryStringData = EncryptionUtility.Decrypt(encodedQueryStringData);

                    string unlockProjectId = GetQueryStringParams(decodedQueryStringData, "Projectid");
                    string RoleGroup = GetQueryStringParams(decodedQueryStringData, "RoleGroup");

                    if (RoleGroup == "Requestor")
                    {
                        _IClearanceProjectRepository.UnlockProject(Convert.ToInt64(unlockProjectId), getUserInfo());
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private string GetQueryStringParams(string querystring, string param)
        {
            LoggerFactory.LogWriter.MethodStart();

            string[] sURLVariables = querystring.Split('&');
            string value = string.Empty;

            for (var i = 0; i < sURLVariables.Length; i++)
            {
                var sParameterName = sURLVariables[i].Split('=');

                if (sParameterName[0] == param)
                {
                    value = sParameterName[1];
                    return value;
                }
            }

            LoggerFactory.LogWriter.MethodExit();

            return value;
        }

    }
}


