using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.Enumerations;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.ViewModels.Search;
using BusinessConstants = UMGI.GRCS.BusinessEntities.Constants.Constants;
using UMGI.GRCS.UI.ViewConstant;
using Lookups = UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Controllers
{
    [ValidateInput(false)]
    public partial class SearchController : BaseController
    {
        private IClearanceProjectRepository _IProjectRepository;
        private IClearanceReleaseRepository _IClearanceReleaseRepository;
        private IClearanceResourceRepository _IClearanceResourceRepository;
        IClearanceResourceModel _clearanceResourceModel;
        private IClearanceProjectModel _IClearanceProjectModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractController"/> class.
        /// </summary>
        public SearchController()
        {

        }

        public SearchController(IClearanceProjectRepository clearanceProjectRepository, IClearanceResourceRepository clearanceResourceRepository, IClearanceReleaseRepository clearanceReleaseRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            _IProjectRepository = clearanceProjectRepository;
            _IClearanceReleaseRepository = clearanceReleaseRepository;
            _IClearanceResourceRepository = clearanceResourceRepository;
            LoggerFactory = logFactory;
            SessionWrapper = sessionWrapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        private LeanUserInfo getUserInfo()
        {
            var userInfo = SessionWrapper.CurrentUserInfo;

            return new LeanUserInfo
            {
                UserId = userInfo.UserId,
                UserLoginName = userInfo.UserLoginName,
                UserName = userInfo.UserName,
                EmailId = userInfo.EmailId
            };
        }

        [OutputCache(Duration = 3600, VaryByParam = "None", VaryByCustom = "None")]
        public ActionResult AdvanceResourceSearch(string clrprjmodel)
        {
            LoggerFactory.LogWriter.MethodStart();

            this.ViewData.Add("id", 0);
            List<string> _type = new List<string>();

            _type.Add(Constants.ClearanceMusicType);
            _type.Add(Constants.ClearanceResourceType);
            _type.Add(Constants.ClearanceRecordingType);

            _clearanceResourceModel = _IProjectRepository.GetMasterDataResource(_type, getUserInfo());
            var MusicType = _clearanceResourceModel.MusicType.Where(x => x.Text.ToUpper() != "JAZZ").ToList();
            _clearanceResourceModel.MusicType = MusicType;
            var ResourceType = _clearanceResourceModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
            _clearanceResourceModel.ResourceType = ResourceType;

            LoggerFactory.LogWriter.MethodExit();

            return View(ViewConstant.Search.AdvanceResourceSearch, _clearanceResourceModel);
        }

        public ActionResult AdvanceResourceSearchUpdatePopup(ArtistSearchCriteria artistsearch)
        {
            LoggerFactory.LogWriter.MethodStart();

            this.ViewData.Add("id", int.Parse(artistsearch.ArtistId.ToString()));
            List<string> _type = new List<string>();
            _type.Add(Constants.ClearanceMusicType);
            _type.Add(Constants.ClearanceResourceType);
            _type.Add(Constants.ClearanceRecordingType);

            _clearanceResourceModel = _IProjectRepository.GetMasterDataResource(_type, getUserInfo());
            var MusicType = _clearanceResourceModel.MusicType.Where(x => x.Text.ToUpper() != "JAZZ").ToList();
            _clearanceResourceModel.MusicType = MusicType;
            var ResourceType = _clearanceResourceModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
            _clearanceResourceModel.ResourceType = ResourceType;

            LoggerFactory.LogWriter.MethodExit();

            return View(ViewConstant.Search.AdvanceResourceSearch, _clearanceResourceModel);
        }

        public ActionResult ClearanceResourceRights(int id)
        {
            LoggerFactory.LogWriter.MethodStart();

            id = int.Parse(Request.Params["id"].ToString());
            ViewBag.id = id;

            LoggerFactory.LogWriter.MethodExit();

            return View();
        }

        public ActionResult ReleaseRegularData(int id)
        {
            LoggerFactory.LogWriter.MethodStart();

            id = int.Parse(Request.Params["id"].ToString());
            ViewBag.id = id;

            LoggerFactory.LogWriter.MethodExit();

            return View();
        }

        public ActionResult ClearanceProjectSearch()
        {
            LoggerFactory.LogWriter.MethodStart();

            _IClearanceProjectModel = _IProjectRepository.FillProjectSearchDropDown();

            LoggerFactory.LogWriter.MethodExit();

            return View("ClearanceProjectSearch", _IClearanceProjectModel);
        }

        #region "Inquiry Search"

        /// <summary>
        /// To fill the drop down of InquirySearch Page on the page Load
        /// </summary>
        public ActionResult InquirySearch()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                _IClearanceProjectModel = _IProjectRepository.GetInquiryClearanceProjectSearchDropDown(SessionWrapper.CurrentUserInfo.UserLoginName, getUserInfo());
                _IClearanceProjectModel.ItemsPerPageDefaultValue = 25;

                LoggerFactory.LogWriter.MethodExit();

                return View("InquirySearch", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetRequestTypeList(string id)
        {
            LoggerFactory.LogWriter.MethodStart();

            int LookUpTypeId = 0;
            JsonResult result = new JsonResult();
            if (string.IsNullOrEmpty(id)) id = "0";
            if (System.Convert.ToInt32(id) == System.Convert.ToInt32(General.ProjectType.Master))
            {
                LookUpTypeId = 55; //will use Enum lateron
            }
            else if (System.Convert.ToInt32(id) == System.Convert.ToInt32(General.ProjectType.Regular))
            {
                LookUpTypeId = 56;//will use Enum lateron
            }
            var RequestTypeList = _IProjectRepository.GetClearanceProjectRequestTypeDropDownByProjectType(LookUpTypeId, getUserInfo());
            result.Data = RequestTypeList.ToList();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            LoggerFactory.LogWriter.MethodStart();

            return result;
        }

        [HttpPost]
        public JsonResult InquirySearch(ClearanceProjectInquirySearchCriteria projectSearchCriteria, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                projectSearchCriteria.RoleGroup = BusinessEntities.Lookups.RoleGroup.Reviewer;
                projectSearchCriteria.SortField = jtSorting;
                int totalRowCount = 0;
                UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities.ProjectSearchResult projectList = _IProjectRepository.InquiryClearanceProjectSearch(projectSearchCriteria, SessionWrapper.CurrentUserInfo.UserLoginName);
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

        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AddFreeHandResource(ClearanceResourceModel resourceModel)
        {
            LoggerFactory.LogWriter.MethodStart();

            return Json(new { success = true });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Duration = 0)]
        public JsonResult SearchR2Resource(ResourceSearchCriteria searchCriteria, int jtStartIndex)
        {
            LoggerFactory.LogWriter.MethodStart();

            searchCriteria.Criteria.StartIndex = jtStartIndex;
            ClearanceResourceSearchResult listResourceDetails = new ClearanceResourceSearchResult();

            if (FilterValidated(searchCriteria))
            {
                if (searchCriteria.IsIncludeMobileLock)
                    searchCriteria.MobileArtistSearchType = MobileArtistSearchType.IncludeMobileArtists;
                else
                    searchCriteria.MobileArtistSearchType = MobileArtistSearchType.ExcludeMobileArtists;

                listResourceDetails = _IClearanceResourceRepository.SearchR2Resource(searchCriteria, getUserInfo());

                if (listResourceDetails.lstClearanceResource.Count() > 0)
                {
                    listResourceDetails.lstClearanceResource = listResourceDetails.lstClearanceResource.OrderBy(x => x.ResourceTitle).OrderBy(x => x.Isrc).ToList();
                    var List = listResourceDetails.lstClearanceResource.ToList();
                    List.All(i =>
                    {
                        if (i.ResourceTypeDesc.ToUpper() != "AUDIO" && i.ResourceTypeDesc.ToUpper() != "VIDEO")
                        {
                            listResourceDetails.lstClearanceResource.Remove(i);
                            listResourceDetails.RowsRetreived = listResourceDetails.RowsRetreived - 1;
                        }
                        return true;
                    });
                }
            }

            LoggerFactory.LogWriter.MethodExit();

            return Json(new { Result = "OK", Records = listResourceDetails.lstClearanceResource.AsQueryable(), RowIndex = listResourceDetails.RowIndex, TotalRecordCount = listResourceDetails.RowsRetreived });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AddtoProject(ClearanceResourceModel resourceModel)
        {
            LoggerFactory.LogWriter.MethodStart();

            string[] _arrReslist = resourceModel.SearchSelectedValues.Split('~');

            List<ClearanceResource> listSelectedItems = new List<ClearanceResource>();
            ClearanceResource resourceDetail = new ClearanceResource();
            for (int i = 0; i < _arrReslist.Length; i++)
            {

                string[] listResources = _arrReslist[i].ToString().Split(':');

                if (listResources[0].ToString() != "")
                {
                    listSelectedItems.Add(new ClearanceResource()
                    {
                        Isrc = listResources[0].ToString(),
                        Title = listResources[1].ToString(),
                        VersionTitle = listResources[2].ToString()
                    });
                }
            }

            LoggerFactory.LogWriter.MethodExit();

            return Json(new { success = true, Records = listSelectedItems });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult ReloadResourceTab(List<ClearanceResource> listSelectedItems)
        {
            LoggerFactory.LogWriter.MethodStart();

            ClearanceProjectModel c = new ClearanceProjectModel();
            c.MasterProjectDetails.ClearanceResource = listSelectedItems;

            LoggerFactory.LogWriter.MethodExit();

            return PartialView("Resources", c);
        }

        #region "Search Release"
        /// <summary>
        /// Get list of territories and countries.
        /// </summary>
        /// <returns>Partial view</returns>
        public PartialViewResult SearchForRelease()
        {
            LoggerFactory.LogWriter.MethodStart();

            IClearanceProjectModel _IClearanceProjectModel = new ClearanceProjectModel();
            string viewName = "SearchForRelease";

            LoggerFactory.LogWriter.MethodExit();

            return PartialView(viewName, _IClearanceProjectModel);
        }

        [HttpPost]
        public JsonResult GetRegularResources(string R2ReleaseId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<ClearanceResource> clrResource = new List<ClearanceResource>();
                List<TrackInfo> trackInfo = _IClearanceReleaseRepository.R2GetReleaseAdditionalDetails(long.Parse(R2ReleaseId), getUserInfo());
                List<ClearanceResource> clrResourceAdd = new List<ClearanceResource>();
                trackInfo = trackInfo.OrderBy(trackinf => trackinf.SequenceNo).ToList();
                foreach (var track in trackInfo)
                {
                    ClearanceResource clrResourceSearch = new ClearanceResource();
                    var artistNameList = track.ArtistInfo.Where(s => s.IsPrimary == null).Select(a => a.Name).ToList();
                    if (R2ReleaseId == Convert.ToString(track.ReleaseId))
                    {
                        artistNameList = track.ArtistInfo.Where(s => s.IsPrimary == "Y").Select(a => a.Name).ToList();
                    }
                    if (artistNameList != null && artistNameList.Count > 0)
                    {
                        clrResourceSearch.ArtistName = string.Join(",", artistNameList);
                    }
                    if (track.Isrc != null && track.Isrc.Count() > 0)
                    {
                        clrResourceSearch.Isrc = string.Join(",", track.Isrc);
                    }
                    clrResourceSearch.ReleaseId = track.ReleaseId;
                    clrResourceSearch.ResourceTitle = track.ResourceTitle;
                    clrResourceSearch.VersionTitle = track.ResourceVersionTitle;
                    clrResourceSearch.SequenceNo = Convert.ToString(track.SequenceNo);
                    clrResourceSearch.Duration = track.TrackDuration;
                    clrResourceSearch.ResourceId = track.TrackId;
                    clrResourceSearch.UserName = track.UserName;
                    clrResourceAdd.Add(clrResourceSearch);
                }
                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = BusinessConstants.JsonOk, Records = clrResourceAdd, TotalRecordCount = trackInfo.Count });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult GetRelease(string UPC, string ArtistName, string ArtistID, string ReleaseTitle, string R2ProjectId)
        {
            LoggerFactory.LogWriter.MethodStart();

            ReleaseSearchCriteria releaseSearchCriteria = new ReleaseSearchCriteria();
            releaseSearchCriteria.Upc = UPC;
            releaseSearchCriteria.ArtistName = ArtistName;
            releaseSearchCriteria.ArtistId = string.IsNullOrEmpty(ArtistID.ToString()) ? 0 : Convert.ToInt64(ArtistID);
            releaseSearchCriteria.ReleaseTitle = ReleaseTitle;
            releaseSearchCriteria.R2ProjectId = R2ProjectId;
            releaseSearchCriteria.Criteria.RowIndex = -1;
            releaseSearchCriteria.Criteria.PageSize = 10;
            releaseSearchCriteria.Criteria.UserName = "vivek_gupta";
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
                                clrReleaseSearch.ArtistName = clrReleaseSearch.ArtistName + "," + artistInfor.Name;
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
                    clrReleaseSearch.labelName = _IProjectRepository.getLabelNmForExistingRelease(Convert.ToInt32(releaseSearch.LabelId), getUserInfo());
                    clrReleaseSearch.LinkedContractDetails = releaseSearch.LinkedContractDetails;
                    clrReleaseSearch.MobileArtist = releaseSearch.MobileArtist;
                    clrReleaseSearch.MusicType_Desc = releaseSearch.MusicClassType;
                    clrReleaseSearch.MusicType_Id = _IProjectRepository.getMusicClassTypeIdForExistingRelease(releaseSearch.MusicClassType, getUserInfo());
                    clrReleaseSearch.OwnedProjectId = releaseSearch.OwnedProjectId;
                    clrReleaseSearch.PackageIndicator = releaseSearch.PackageIndicator;
                    if (string.IsNullOrEmpty(clrReleaseSearch.PackageIndicator) || clrReleaseSearch.PackageIndicator.Contains('N'))
                    {
                        clrReleaseSearch.PackageText = "NO";
                    }
                    else
                    {
                        clrReleaseSearch.PackageText = "Yes";
                    }
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
                    clrReleaseSearch.IsDeviatedICLALevel_Club = true;
                    clrReleaseSearch.IsDeviatedICLALevel_Non = true;
                    clrReleaseSearch.IsDeviatedICLALevel_Price = true;
                    clrReleaseSearch.IsDeviatedICLALevel_Promotional = true;
                    clrReleaseSearch.IsDeviatedICLALevel_Regular = true;
                    clrReleaseSearch.IsDeviatedICLALevel_TV = true;
                    clrReleaseSearch.Archive_Flag = "N";
                    objreleaseEntity.Add(clrReleaseSearch);
                }
            }
            LoggerFactory.LogWriter.MethodExit();

            return Json(new { Result = "OK", Records = objreleaseEntity.AsQueryable(), RowIndex = releaseSearchResult.RowIndex, TotalRecordCount = releaseSearchResult.RowCount });
        }

        /// <summary>
        /// Get list of territories and countries.
        /// </summary>
        /// <returns>Partial view</returns>
        public PartialViewResult ClrR2ProjectsSearch(ClearanceProjectModel regularProjectModel)
        {
            LoggerFactory.LogWriter.MethodStart();

            IClearanceProjectModel _IClearanceProjectModel = new ClearanceProjectModel();
            string viewName = "ClrR2ProjectsSearch";
            List<ClearanceAdminCompany> _clearanceAdminCompany = new List<ClearanceAdminCompany>();

            List<SelectListItem> companyListdrop = new List<SelectListItem>();
            SelectListItem lstItemCompany = new SelectListItem();
            companyListdrop.Add(lstItemCompany);
            _IClearanceProjectModel.CompanyList = companyListdrop;

            // get divisions
            List<SelectListItem> divisionList = new List<SelectListItem>();
            SelectListItem lstItem = new SelectListItem();
            divisionList.Add(lstItem);
            _IClearanceProjectModel.DivisionList = divisionList;

            // get labels
            List<SelectListItem> labelList = new List<SelectListItem>();
            SelectListItem lstItemLabel = new SelectListItem();
            labelList.Add(lstItemLabel);
            _IClearanceProjectModel.LabelList = labelList;

            // initialize projectInfo class
            ProjectInfo projectInfo = new ProjectInfo();
            projectInfo.artistId = null;
            _IClearanceProjectModel.RegularProjectDetails.ProjectInfo = projectInfo;

            LoggerFactory.LogWriter.MethodExit();

            return PartialView(viewName, _IClearanceProjectModel);
        }

        public PartialViewResult ClrPushToR2()
        {
            LoggerFactory.LogWriter.MethodStart();

            string viewName = "ClrPushToR2";

            LoggerFactory.LogWriter.MethodExit();

            return PartialView(viewName);
        }

        /// <summary>
        /// Auto Search for Project
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [OutputCache(Duration = 0)]
        public JsonResult AutoCompleteSearch(SearchCriteria autoSearch)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<string> tags = _IProjectRepository.AutoCompleteSearch(autoSearch, SessionWrapper.CurrentUserInfo.UserLoginName);

                LoggerFactory.LogWriter.MethodExit();

                return this.Json(tags, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult GetResourceRights(long r2resourceId)
        {
            LoggerFactory.LogWriter.MethodStart();

            ViewResourceRolesAndRights viewResourceRolesAndRights = MapResourceRolesAndRights(_IClearanceResourceRepository.GetResourceRights(r2resourceId, getUserInfo()));

            LoggerFactory.LogWriter.MethodExit();

            return PartialView(ViewConstant.Search.ResourceRolesAndRights, viewResourceRolesAndRights);
        }

        private ViewResourceRolesAndRights MapResourceRolesAndRights(List<ContractDetails> contractRolesAndRights)
        {
            LoggerFactory.LogWriter.MethodStart();

            ViewResourceRolesAndRights viewModel = new ViewResourceRolesAndRights();

            for (int crc = 0; crc < contractRolesAndRights.Count; crc++)
            {
                ViewContractDetails contractDetails = new ViewContractDetails();
                contractDetails.PcNoticeCountryCompany = contractRolesAndRights[crc].PcNoticeCountryCompany;
                contractDetails.ClearingNotes = contractRolesAndRights[crc].ClearingNotes;
                contractDetails.IsPhysicalRights = GetBooleanToString(contractRolesAndRights[crc].IsPhysicalRights);
                contractDetails.IsLossRightsIndicator = contractRolesAndRights[crc].IsLossRightsIndicator == true ? BusinessConstants.Yes : BusinessConstants.No; ;
                contractDetails.IsDigitalRights = GetBooleanToString(contractRolesAndRights[crc].IsDigitalRights);
                contractDetails.IsActiveForMarketing = GetBooleanToString(contractRolesAndRights[crc].IsActiveForMarketing);
                contractDetails.IsDigitalUnbundle = GetBooleanToString(contractRolesAndRights[crc].IsDigitalUnbundle);
                contractDetails.IsSensitiveArtist = GetBooleanToString(contractRolesAndRights[crc].IsSensitiveArtist);
                contractDetails.TerritorialRightsDefinition = contractRolesAndRights[crc].TerritorialRightsDefinition == null ? string.Empty : contractRolesAndRights[crc].TerritorialRightsDefinition;

                for (int cpci = 0; cpci < contractRolesAndRights[crc].ContractPreClearanceInformation.Count; cpci++)
                {
                    ViewContractPreClearanceInformation contractPreClearanceInformation = new ViewContractPreClearanceInformation();
                    contractPreClearanceInformation.PreclearanceTypeDesc = contractRolesAndRights[crc].ContractPreClearanceInformation[cpci].PreclearanceTypeDesc;
                    contractPreClearanceInformation.IsPreclearance = contractRolesAndRights[crc].ContractPreClearanceInformation[cpci].IsPreclearance == true ? BusinessConstants.Yes : BusinessConstants.No;
                    contractPreClearanceInformation.PreClearedTerritoryExclusion = contractRolesAndRights[crc].ContractPreClearanceInformation[cpci].PreClearedTerritoryExclusion;

                    contractDetails.ContractPreClearanceInformation.Add(contractPreClearanceInformation);
                }

                for (int cerl = 0; cerl < contractRolesAndRights[crc].ContractExploitationRestrictionsList.Count; cerl++)
                {
                    ViewContractExploitationRestrictions ContractExploitationRestrictionsList = new ViewContractExploitationRestrictions();
                    ContractExploitationRestrictionsList.ExploitaionTypeName = contractRolesAndRights[crc].ContractExploitationRestrictionsList[cerl].ExploitaionTypeName;
                    ContractExploitationRestrictionsList.Rights = GetRights(contractRolesAndRights[crc].ContractExploitationRestrictionsList[cerl]);

                    contractDetails.ContractExploitationRestrictionsList.Add(ContractExploitationRestrictionsList);
                }

                viewModel.ContractRolesAndRights.Add(contractDetails);
            }

            LoggerFactory.LogWriter.MethodExit();

            return viewModel;
        }

        private string GetBooleanToString(bool? variable)
        {
            string description = string.Empty;
            if (variable == true) description = BusinessConstants.Yes;
            else if (variable == false) description = BusinessConstants.No;
            return description;
        }

        private string GetRights(ContractExploitationRestrictions contractExploitationRestrictions)
        {
            LoggerFactory.LogWriter.MethodStart();

            string rights = BusinessConstants.Yes;
            if (contractExploitationRestrictions.IsRestriction
                && contractExploitationRestrictions.RestrictionTypeId == (byte)Lookups.RestrictionType.NoRights)
            {
                rights = BusinessConstants.No;
            }

            LoggerFactory.LogWriter.MethodExit();

            return rights;
        }

        public ActionResult ProjectSearchLoadDivisionsLabels(ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                IClearanceProjectModel _IClearanceProjectModel = new ClearanceProjectModel();

                List<MySelectListItem> lstDivisionGet = (List<MySelectListItem>)TempData["DivisionData"];
                var outputDivisionGet = from ip in lstDivisionGet select ip.GetSelectListItem();

                _IClearanceProjectModel.DivisionList = outputDivisionGet;

                // get divisions
                string companyNameSelected = string.Empty;
                if (collection["companyIdSelectedValue"] != null)
                {
                    companyNameSelected = collection["companyNameSelectedValue"];
                }

                string companyIdSelected = string.Empty;
                if (collection["companyIdSelectedValue"] != null)
                {
                    companyIdSelected = collection["companyIdSelectedValue"];
                }

                string divisionIdSelected = string.Empty;
                if (collection["divisionIdSelectedValue"] != null)
                {
                    divisionIdSelected = collection["divisionIdSelectedValue"];
                }

                string TaskTypeSelectedValue = string.Empty;
                if (collection["TaskTypeSelectedValue"] != null)
                {
                    TaskTypeSelectedValue = collection["TaskTypeSelectedValue"];
                }


                if (!string.IsNullOrEmpty(divisionIdSelected))
                {
                    List<CompanyInfo> labelList = new List<CompanyInfo>();

                    long tryParsecompany = 0;
                    long companyId = 0;
                    if (long.TryParse(companyIdSelected.ToString(), out tryParsecompany))
                        companyId = tryParsecompany;

                    long tryParseDiv = 0;
                    long divisionId = 0;
                    if (long.TryParse(divisionIdSelected.ToString(), out tryParseDiv))
                        divisionId = tryParseDiv;

                    labelList = _IProjectRepository.GetLabels(companyId, divisionId, TaskTypeSelectedValue, getUserInfo());
                    List<SelectListItem> labelListDropDown = new List<SelectListItem>();
                    labelListDropDown = labelList.Select(item => new SelectListItem { Value = Convert.ToString(item.Id), Text = item.Name }).ToList();

                    _IClearanceProjectModel.LabelList = labelListDropDown;
                }


                if (_IClearanceProjectModel.DivisionList == null)
                {
                    List<SelectListItem> divisionList = new List<SelectListItem>();
                    SelectListItem lstItem = new SelectListItem();
                    divisionList.Add(lstItem);
                    _IClearanceProjectModel.DivisionList = divisionList;
                }

                if (_IClearanceProjectModel.LabelList == null)
                {
                    List<SelectListItem> labelList = new List<SelectListItem>();
                    SelectListItem lstItem = new SelectListItem();
                    labelList.Add(lstItem);
                    _IClearanceProjectModel.LabelList = labelList;
                }

                // initialize projectInfo class
                ProjectInfo projectInfo = new ProjectInfo();

                // assign selected companies back
                long tryParseSearch = 0;
                if (long.TryParse(companyIdSelected.ToString(), out tryParseSearch))
                    projectInfo.AdminCompanyId = tryParseSearch;
                projectInfo.DataAdminCompany = companyNameSelected;

                // assign selected divisions back
                long tryParseDivision = 0;
                if (long.TryParse(divisionIdSelected.ToString(), out tryParseDivision))
                    projectInfo.divisionId = tryParseDivision;


                // Assign project id, artist id, artist Name for existing project
                // Assign project title, artist id, artist Name for new project
                string projectIdSelected = string.Empty;
                if (!string.IsNullOrEmpty(collection["projectIdSelectedValue"]))
                {
                    projectIdSelected = collection["projectIdSelectedValue"];
                }
                string projectTitleSelected = string.Empty;
                if (!string.IsNullOrEmpty(collection["projectTitleFreeHandPush"]))
                {
                    projectTitleSelected = collection["projectTitleFreeHandPush"];
                }

                long? artistIdSelected = null;
                if (!string.IsNullOrEmpty(collection["hdnArtistIdPush"]))
                {
                    long artistTestId;
                    if (long.TryParse(collection["hdnArtistIdPush"], out artistTestId))
                    {
                        artistIdSelected = artistTestId;
                    }

                }
                string artistNameSelected = string.Empty;
                if (!string.IsNullOrEmpty(collection["hdnArtistNamePush"]))
                {
                    artistNameSelected = collection["hdnArtistNamePush"];
                }
                string artistNameInfo = string.Empty;
                if (!string.IsNullOrEmpty(collection["divArtistNameSelected"]))
                {
                    artistNameInfo = collection["divArtistNameSelected"];
                }
                string artistIdInfo = string.Empty;
                if (!string.IsNullOrEmpty(collection["hdnArtistIdSelected"]))
                {
                    artistIdInfo = collection["hdnArtistIdSelected"];
                }

                projectInfo.ProjectCode = projectIdSelected;
                projectInfo.Title = projectTitleSelected;
                projectInfo.artistId = artistIdSelected;
                projectInfo.ArtistName = artistNameSelected;
                projectInfo.ArtistNameInfo = artistNameInfo;
                projectInfo.ArtistIdInfo = artistIdInfo;

                _IClearanceProjectModel.RegularProjectDetails.ProjectInfo = projectInfo;

                var outputDivisionData = (from ip in _IClearanceProjectModel.DivisionList select new MySelectListItem(ip)).ToList<MySelectListItem>();
                TempData["DivisionData"] = outputDivisionData;

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("ClrR2ProjectsSearch", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationMsg = ex.Message;
                return PartialView("ClrR2ProjectsSearch", model);
            }
        }

        public ActionResult ProjectSearchLoadDivisionsDAG(ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                IClearanceProjectModel _IClearanceProjectModel = new ClearanceProjectModel();

                // get divisions
                string companyNameSelected = string.Empty;
                if (collection["companyIdSelectedValue"] != null)
                {
                    companyNameSelected = collection["companyNameSelectedValue"];
                }

                string TaskTypeSelectedValue = string.Empty;
                if (collection["TaskTypeSelectedValue"] != null)
                {
                    TaskTypeSelectedValue = collection["TaskTypeSelectedValue"];
                }

                string companyIdSelected = string.Empty;
                if (collection["companyIdSelectedValue"] != null)
                {
                    companyIdSelected = collection["companyIdSelectedValue"];
                    if (!string.IsNullOrEmpty(companyIdSelected))
                    {
                        List<CompanyInfo> divisionList = new List<CompanyInfo>();
                        divisionList = _IProjectRepository.GetDivisions(companyIdSelected, TaskTypeSelectedValue, getUserInfo());
                        List<SelectListItem> divisionListDropDown = new List<SelectListItem>();
                        divisionListDropDown = divisionList.Select(item => new SelectListItem { Value = Convert.ToString(item.Id), Text = item.Name }).ToList();

                        _IClearanceProjectModel.DivisionList = divisionListDropDown;
                    }
                }

                if (_IClearanceProjectModel.DivisionList == null)
                {
                    List<SelectListItem> divisionList = new List<SelectListItem>();
                    SelectListItem lstItem = new SelectListItem();
                    divisionList.Add(lstItem);
                    _IClearanceProjectModel.DivisionList = divisionList;
                }

                if (_IClearanceProjectModel.LabelList == null)
                {
                    List<SelectListItem> labelList = new List<SelectListItem>();
                    SelectListItem lstItem = new SelectListItem();
                    labelList.Add(lstItem);
                    _IClearanceProjectModel.LabelList = labelList;
                }

                // initialize projectInfo class
                ProjectInfo projectInfo = new ProjectInfo();

                long tryParseSearch = 0;
                if (long.TryParse(companyIdSelected.ToString(), out tryParseSearch))
                    projectInfo.AdminCompanyId = tryParseSearch;

                projectInfo.DataAdminCompany = companyNameSelected;


                // Assign project id, artist id, artist Name for existing project
                // Assign project title, artist id, artist Name for new project
                string projectIdSelected = string.Empty;
                if (!string.IsNullOrEmpty(collection["projectIdSelectedValue"]))
                {
                    projectIdSelected = collection["projectIdSelectedValue"];

                }
                string projectTitleSelected = string.Empty;
                if (!string.IsNullOrEmpty(collection["projectTitleFreeHandPush"]))
                {
                    projectTitleSelected = collection["projectTitleFreeHandPush"];
                }

                long? artistIdSelected = null;
                if (!string.IsNullOrEmpty(collection["hdnArtistIdPush"]))
                {
                    long artistTestId;
                    if (long.TryParse(collection["hdnArtistIdPush"], out artistTestId))
                    {
                        artistIdSelected = artistTestId;
                    }

                }
                string artistNameSelected = string.Empty;
                if (!string.IsNullOrEmpty(collection["hdnArtistNamePush"]))
                {
                    artistNameSelected = collection["hdnArtistNamePush"];
                }
                string artistNameInfo = string.Empty;
                if (!string.IsNullOrEmpty(collection["divArtistNameSelected"]))
                {
                    artistNameInfo = collection["divArtistNameSelected"];
                }
                string artistIdInfo = string.Empty;
                if (!string.IsNullOrEmpty(collection["hdnArtistIdSelected"]))
                {
                    artistIdInfo = collection["hdnArtistIdSelected"];
                }

                projectInfo.ProjectCode = projectIdSelected;
                projectInfo.Title = projectTitleSelected;
                projectInfo.artistId = artistIdSelected;
                projectInfo.ArtistName = artistNameSelected;
                projectInfo.ArtistNameInfo = artistNameInfo;
                projectInfo.ArtistIdInfo = artistIdInfo;
                _IClearanceProjectModel.RegularProjectDetails.ProjectInfo = projectInfo;

                var outputDivisionData = (from ip in _IClearanceProjectModel.DivisionList select new MySelectListItem(ip)).ToList<MySelectListItem>();

                TempData["DivisionData"] = outputDivisionData;

                LoggerFactory.LogWriter.MethodExit();

                return PartialView("ClrR2ProjectsSearch", _IClearanceProjectModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationMsg = ex.Message;
                return PartialView("ClrR2ProjectsSearch", model);
            }
        }

        public ActionResult GetAuthorizationsForR2CreatePartialUpdates()
        {
            Boolean data = false;
            IClearanceProjectModel _IClearanceProjectModel = new ClearanceProjectModel();
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                data = _IProjectRepository.GetAuthorizationsForR2(SessionWrapper.CurrentUserInfo.UserLoginName, "Create Project", AnaTargetApplication.R2, string.Empty);

                if (data)
                {
                    List<ClearanceAdminCompany> _clearanceAdminCompany = new List<ClearanceAdminCompany>();

                    List<SelectListItem> companyListdrop = new List<SelectListItem>();
                    SelectListItem lstItemCompany = new SelectListItem();
                    companyListdrop.Add(lstItemCompany);
                    _IClearanceProjectModel.CompanyList = companyListdrop;

                    // get divisions
                    List<SelectListItem> divisionList = new List<SelectListItem>();
                    SelectListItem lstItem = new SelectListItem();
                    divisionList.Add(lstItem);
                    _IClearanceProjectModel.DivisionList = divisionList;

                    // get labels
                    List<SelectListItem> labelList = new List<SelectListItem>();
                    SelectListItem lstItemLabel = new SelectListItem();
                    labelList.Add(lstItemLabel);
                    _IClearanceProjectModel.LabelList = labelList;

                    // initialize projectInfo class
                    ProjectInfo projectInfo = new ProjectInfo();
                    projectInfo.artistId = null;
                    _IClearanceProjectModel.RegularProjectDetails.ProjectInfo = projectInfo;

                    LoggerFactory.LogWriter.MethodExit();

                    return PartialView("ClrR2ProjectsSearch", _IClearanceProjectModel);
                }
                else
                {
                    LoggerFactory.LogWriter.MethodExit();

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetReleasesSearch(string UPC, string ArtistName, string ArtistID, string ReleaseTitle, string r2ProjectId, string pageSize, int jtStartIndex, long rowIndex = -1)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                var releaseSearchCriteria = new ReleaseSearchCriteria
                {
                    Upc = UPC,
                    ArtistName = ArtistName,
                    ArtistId = string.IsNullOrEmpty(ArtistID) ? 0 : Convert.ToInt64(ArtistID),
                    ReleaseTitle = ReleaseTitle,
                    R2ProjectId = r2ProjectId,
                    Criteria =
                    {
                        RowIndex = rowIndex,
                        PageSize = Convert.ToInt32(pageSize),
                        UserName = string.Empty,
                        QualificationCriteria = true,
                        StartIndex = jtStartIndex
                    }
                };

                var releaseSearchResult = _IClearanceReleaseRepository.R2ReleaseSearch(releaseSearchCriteria, getUserInfo());

                var objreleaseEntity = new List<ClearanceRelease>();

                if (releaseSearchResult.Values.Count > 0)
                {
                    objreleaseEntity.AddRange(releaseSearchResult.Values.Select(releaseSearch => new ClearanceRelease
                    {
                        resourceDetail = new List<ClearanceResource>(),
                        AdminCompanyId = releaseSearch.AdminCompanyId,
                        ArtistInfo = releaseSearch.ArtistInfo,
                        ArtistName = ((releaseSearch.ArtistInfo != null) && (releaseSearch.ArtistInfo.Count > 0)) ? string.Join(",", releaseSearch.ArtistInfo.Select(a => a.Name)) : releaseSearch.ArtistName,
                        AssignedType = releaseSearch.AssignedType,
                        CatalogueNo = releaseSearch.CatalogueNo,
                        ComponentCount = releaseSearch.ComponentCount,
                        Configuration = releaseSearch.Configuration,
                        ConfigurationDisplay = releaseSearch.ConfigurationDisplay,
                        DataAdminCompany = releaseSearch.DataAdminCompany,
                        DataAdminCompanyId = releaseSearch.DataAdminCompanyId,
                        DivisionId = releaseSearch.DivisionId,
                        EarilerReleaseDate = releaseSearch.EarilerReleaseDate,
                        Grid = releaseSearch.Grid,
                        IsAlreadyLinked = releaseSearch.IsAlreadyLinked,
                        IsMac = releaseSearch.IsMac,
                        IsMediaPortal = releaseSearch.IsMediaPortal,
                        LabelId = releaseSearch.LabelId,
                        labelName = releaseSearchResult.Labels.Where(l => l.Value == releaseSearch.LabelId).Select(l => l.Description).FirstOrDefault(),
                        LinkedContractDetails = releaseSearch.LinkedContractDetails,
                        MobileArtist = releaseSearch.MobileArtist,
                        MusicType_Desc = releaseSearch.MusicClassType,
                        MusicType_Id = Convert.ToInt32(releaseSearchResult.MusicClassTypes.Where(m => m.Description == releaseSearch.MusicClassType).Select(m => m.Value).FirstOrDefault()),
                        OwnedProjectId = releaseSearch.OwnedProjectId,
                        PackageIndicator = releaseSearch.PackageIndicator,
                        PackageInfo = releaseSearch.PackageInfo,
                        PackageText = (string.IsNullOrEmpty(releaseSearch.PackageIndicator) || releaseSearch.PackageIndicator.Contains('N')) ? "NO" : "Yes",
                        PCompanyId = releaseSearch.PCompanyId,
                        PCompanyName = releaseSearch.PCompanyName,
                        PLicensingExtension = releaseSearch.PLicensingExtension,
                        PYear = releaseSearch.PYear,
                        R2AccountId = releaseSearch.R2AccountId,
                        R2ReleaseId = releaseSearch.ReleaseId,
                        R2Status = releaseSearch.R2Status,
                        R2StatusType = releaseSearch.R2StatusType,
                        ReleaseTitle = releaseSearch.ReleaseTitle,
                        ReleaseType = releaseSearch.ReleaseType,
                        ScopeType = releaseSearch.ScopeType,
                        Sequence = releaseSearch.Sequence,
                        SoundtrackIndicator = releaseSearch.SoundtrackIndicator,
                        Is_Ost = releaseSearch.SoundtrackIndicator.Equals("1"),
                        TrackCount = releaseSearch.TrackCount,
                        TrackInfo = releaseSearch.TrackInfo,
                        Upc = releaseSearch.Upc,
                        UserName = releaseSearch.UserName,
                        VersionTitle = releaseSearch.VersionTitle,
                        IsDeviatedICLALevel_Club = true,
                        IsDeviatedICLALevel_Non = true,
                        IsDeviatedICLALevel_Price = true,
                        IsDeviatedICLALevel_Promotional = true,
                        IsDeviatedICLALevel_Regular = true,
                        IsDeviatedICLALevel_TV = true
                    }));
                }

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = BusinessConstants.JsonOk, Records = objreleaseEntity.AsQueryable(), TotalRecordCount = releaseSearchResult.RowCount, RowIndex = releaseSearchResult.RowIndex });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        private bool FilterValidated(ResourceSearchCriteria searchCriteria)
        {
            bool validFilter = false;

            if (!string.IsNullOrEmpty(searchCriteria.Isrc) || !string.IsNullOrEmpty(searchCriteria.ArtistName) ||
                !string.IsNullOrEmpty(searchCriteria.Upc) || searchCriteria.ArtistId > 0 ||
                !string.IsNullOrEmpty(searchCriteria.ResourceTitle) || !string.IsNullOrEmpty(searchCriteria.VersionTitle) ||
                !string.IsNullOrEmpty(searchCriteria.R2ProjectID))
            {
                validFilter = true;
            }

            return validFilter;
        }
    }
}
