using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using Constants = UMGI.GRCS.BusinessEntities.Constants.Constants;

namespace UMGI.GRCS.UI.Controllers
{
    [ValidateInput(false)]
    public class ClearanceReleaseController : BaseController
    {

        private IClearanceReleaseRepository _iClearanceReleaseRepository;

        static string _callFrom = "";
        
        public ClearanceReleaseController(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractController"/> class.
        /// </summary>
        /// <param>The contract repository. <name>contractRepository</name> </param>
        /// <param name="clearanceReleaseRepository"> </param>
        /// <param name="sessionWrapper"> </param>
        public ClearanceReleaseController(IClearanceReleaseRepository clearanceReleaseRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            _iClearanceReleaseRepository = clearanceReleaseRepository;
            SessionWrapper = sessionWrapper;
            LoggerFactory = logFactory;
        }

        #region UPC Number

        [HttpPost]
        public JsonResult GetUPCNumber(string classicCount)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                string[] musicType = classicCount.Split(',');
                var classiceReleaseId = new List<long>();
                var nonClassiceReleaseId = new List<long>();
                if (!string.IsNullOrEmpty(musicType[0]))
                    classiceReleaseId = musicType[0].Split('~').Where(s => !string.IsNullOrEmpty(s)).Select(s => Convert.ToInt64(s)).ToList();

                if (!string.IsNullOrEmpty(musicType[1])) 
                    nonClassiceReleaseId = musicType[1].Split('~').Where(s => !string.IsNullOrEmpty(s)).Select(s => Convert.ToInt64(s)).ToList();

                string upcNumer = _iClearanceReleaseRepository.GetUPCNumber(classiceReleaseId, nonClassiceReleaseId, SessionWrapper.CurrentUserInfo.UserLoginName);

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, upcResult = upcNumer });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", ex.Message });
            }
        }

        [HttpPost]
        public JsonResult RemoveUPCNumber(string projectReleseId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                long projReleseId = 0;
                projReleseId = long.Parse(projectReleseId);

                _iClearanceReleaseRepository.RemoveUPCNumber(projReleseId, SessionWrapper.CurrentUserInfo.UserLoginName);

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, upcResult = true });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
        
        public JsonResult GetManualUPCNumber(string upcNumber, string projectReleseId)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                long projReleseId = 0;
                projReleseId = long.Parse(projectReleseId);
                string recordResult = _iClearanceReleaseRepository.UpdateManualUpc(projReleseId, upcNumber, GetUserInfo());

                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, upcResult = recordResult });
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = "ERROR", ex.Message });
            }
        }


        private LeanUserInfo GetUserInfo()
        {
            LoggerFactory.LogWriter.MethodStart();

            var userInfo = SessionWrapper.CurrentUserInfo;

            LoggerFactory.LogWriter.MethodExit();

            return new LeanUserInfo
            {
                UserId = userInfo.UserId,
                UserLoginName = userInfo.UserLoginName,
                UserName = userInfo.UserName,
                EmailId = userInfo.EmailId
            };
        }

        #endregion

        #region Package-View Component
        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult ViewPackageComponents(ClearanceReleaseModel clearanceReleaseModel)
        {
            LoggerFactory.LogWriter.MethodStart();

            IClearanceReleaseModel _IClearanceReleaseModel = new ClearanceReleaseModel();
            if (clearanceReleaseModel.clearanceRelease.PackageInfo == null || clearanceReleaseModel.clearanceRelease.PackageInfo.Count == 0)
            {
                _callFrom = clearanceReleaseModel.clearanceRelease.CallFrom;
                _IClearanceReleaseModel = _iClearanceReleaseRepository.GetPackageDetails(clearanceReleaseModel.clearanceRelease.R2ReleaseId, GetUserInfo());
                _IClearanceReleaseModel.clearanceRelease.CallFrom = _callFrom;
            }
            else
            {
                _callFrom = "ReleaseNew";
                bool flag = false;
                var consolidatedPackageInfo = new List<PackageInfo>();
                for (int i = 0; i < clearanceReleaseModel.clearanceRelease.PackageInfo.Count; i++)
                {
                    flag = false;
                    if (clearanceReleaseModel.clearanceRelease.PackageInfo[i].ParentId == 0)
                    {
                        clearanceReleaseModel.clearanceRelease.PackageInfo[i].IsAddedBySearch = "Added";
                    }
                    if (i == 0)
                    {
                        _IClearanceReleaseModel = _iClearanceReleaseRepository.GetPackageDetails(clearanceReleaseModel.clearanceRelease.PackageInfo[i].ReleaseId, GetUserInfo());
                        if (_IClearanceReleaseModel.clearanceRelease.PackageInfo != null && _IClearanceReleaseModel.clearanceRelease.PackageInfo.Count > 0)
                        {
                            foreach (PackageInfo t in _IClearanceReleaseModel.clearanceRelease.PackageInfo)
                            {
                                if (t.ReleaseId == clearanceReleaseModel.clearanceRelease.PackageInfo[i].ReleaseId)
                                {
                                    flag = true;
                                }
                                if (t.ArchiveFlag == null)
                                {
                                    t.ArchiveFlag = "N";
                                }                             

                                else if (clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag == "Y")
                                {
                                    t.ArchiveFlag = "Y";
                                }
                                else if (clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag == "N")
                                {
                                    t.ArchiveFlag = "N";
                                }
                                consolidatedPackageInfo.Add(t);
                            }
                            if (flag == false)
                            {
                                if (clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag == null)
                                {
                                    clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag = "N";
                                }
                                
                                consolidatedPackageInfo.Add(clearanceReleaseModel.clearanceRelease.PackageInfo[i]);
                            }
                        }
                        else
                        {
                            consolidatedPackageInfo.Add(clearanceReleaseModel.clearanceRelease.PackageInfo[i]);
                        }

                    }
                    else
                    {
                        _IClearanceReleaseModel = _iClearanceReleaseRepository.GetPackageDetails(clearanceReleaseModel.clearanceRelease.PackageInfo[i].ReleaseId, GetUserInfo());
                        if (_IClearanceReleaseModel.clearanceRelease.PackageInfo != null && _IClearanceReleaseModel.clearanceRelease.PackageInfo.Count > 0)
                        {
                            foreach (PackageInfo t in _IClearanceReleaseModel.clearanceRelease.PackageInfo)
                            {
                                if (t.ReleaseId == clearanceReleaseModel.clearanceRelease.PackageInfo[i].ReleaseId)
                                {
                                    flag = true;
                                }
                                if (t.ArchiveFlag == null)
                                {
                                    t.ArchiveFlag = "N";
                                }                                
                                else if (clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag == "Y")
                                {
                                    t.ArchiveFlag = "Y";
                                }
                                else if (clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag == "N")
                                {
                                    t.ArchiveFlag = "N";
                                }
                                consolidatedPackageInfo.Add(t);
                            }
                            if (flag == false)
                            {
                                consolidatedPackageInfo.Add(clearanceReleaseModel.clearanceRelease.PackageInfo[i]);
                            }
                        }
                        else
                        {
                            if (clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag == null)
                            {
                                clearanceReleaseModel.clearanceRelease.PackageInfo[i].ArchiveFlag = "N";
                            }
                            consolidatedPackageInfo.Add(clearanceReleaseModel.clearanceRelease.PackageInfo[i]);
                        }

                    }
                }
                if (consolidatedPackageInfo.Count > 0)
                {
                    _IClearanceReleaseModel.clearanceRelease.PackageInfo = new List<PackageInfo>();
                    foreach (PackageInfo t in consolidatedPackageInfo)
                    {
                        if (t.ArchiveFlag == null)
                        {
                            t.ArchiveFlag = "N";
                        }
                        _IClearanceReleaseModel.clearanceRelease.PackageInfo.Add(t);
                    }
                }
                else
                {
                    _IClearanceReleaseModel.clearanceRelease.PackageInfo = new List<PackageInfo>();
                    for (int countpack = 0; countpack < clearanceReleaseModel.clearanceRelease.PackageInfo.Count; countpack++)
                    {
                        if (clearanceReleaseModel.clearanceRelease.PackageInfo[countpack].ArchiveFlag == null)
                        {
                            clearanceReleaseModel.clearanceRelease.PackageInfo[countpack].ArchiveFlag = "N";
                        }
                        _IClearanceReleaseModel.clearanceRelease.PackageInfo.Add(clearanceReleaseModel.clearanceRelease.PackageInfo[countpack]);
                    }

                }
                clearanceReleaseModel.clearanceRelease.R2ReleaseId = 0;

            }
            _IClearanceReleaseModel.clearanceRelease.Upc = clearanceReleaseModel.clearanceRelease.Upc;
            _IClearanceReleaseModel.clearanceRelease.CallFrom = _callFrom;

            _IClearanceReleaseModel.clearanceRelease.ParentId = Constants.DefaultParentId;
            var listPackageInfo = new List<PackageInfo>
                                      {
                                          new PackageInfo
                                              {
                                                  Upc = clearanceReleaseModel.clearanceRelease.Upc,
                                                  ParentId = Constants.DefaultParentId,
                                                  ReleaseId = clearanceReleaseModel.clearanceRelease.R2ReleaseId,
                                                  ArchiveFlag = "N",
                                                  Sequence = 0
                                              }
                                      };
            foreach (PackageInfo t in listPackageInfo)
            {
                if (_IClearanceReleaseModel.clearanceRelease.PackageInfo != null)
                    _IClearanceReleaseModel.clearanceRelease.PackageInfo.Add(t);
                else
                {
                    var listPackage = new List<PackageInfo> { t };
                    _IClearanceReleaseModel.clearanceRelease.PackageInfo = listPackage;
                }
            }

            var rootNode = _IClearanceReleaseModel.clearanceRelease.PackageInfo.FirstOrDefault(x => x.ParentId == 1);


            SetChildren(rootNode, _IClearanceReleaseModel.clearanceRelease.PackageInfo);
            if (_callFrom == "ReleaseNew")
            {
                _iClearanceReleaseRepository.UpdatePackage(clearanceReleaseModel.clearanceRelease.PackageInfo, GetUserInfo());
            }


            LoggerFactory.LogWriter.MethodExit();
            return PartialView("ViewPackageComponents", rootNode);
        }

        private void SetChildren(PackageInfo model, IEnumerable<PackageInfo> packageList)
        {
            LoggerFactory.LogWriter.MethodStart();

            var childs = packageList.
                            Where(x => x.ParentId == model.ReleaseId).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildren(child, packageList);
                    if (model.packageInfo == null)
                    {
                        model.packageInfo = new List<PackageInfo>();
                        if (child.ArchiveFlag == null)
                        {
                            child.ArchiveFlag = "N";
                        }
                        model.packageInfo.Add(child);
                    }
                    else
                    {
                        if (child.ArchiveFlag == null)
                        {
                            child.ArchiveFlag = "N";
                        }
                        model.packageInfo.Add(child);
                    }

                }
            }
            LoggerFactory.LogWriter.MethodExit();
        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult ReleaseDetailsViewComponent(ClearanceReleaseModel clearanceReleaseModel)
        {
            LoggerFactory.LogWriter.MethodStart();

            IClearanceReleaseModel _IClearanceReleaseModel = new ClearanceReleaseModel();
            try
            {
                var releaseSearchResult = new ReleaseSearchResult();
                if (clearanceReleaseModel.clearanceRelease.R2ReleaseId != 0)
                {
                    List<long> ReleaseIds=new List<long>();
                    ReleaseIds.Add(clearanceReleaseModel.clearanceRelease.R2ReleaseId);
                    releaseSearchResult = _iClearanceReleaseRepository.GetReleaseDetailsBasedonR2ReleaseId(ReleaseIds, GetUserInfo());

                    _IClearanceReleaseModel.clearanceRelease.CallFrom = _callFrom;
                    _IClearanceReleaseModel.clearanceRelease.IsAddedBySearchPkg = clearanceReleaseModel.clearanceRelease.IsAddedBySearchPkg;
                    _IClearanceReleaseModel.clearanceRelease.IsRemoved = clearanceReleaseModel.clearanceRelease.IsRemoved;
                    if (releaseSearchResult != null)
                    {
                        if (releaseSearchResult.Values != null)
                        {
                            foreach (ReleaseInfo t in releaseSearchResult.Values)
                            {
                                if (t.ReleaseId == clearanceReleaseModel.clearanceRelease.R2ReleaseId)
                                {

                                    _IClearanceReleaseModel.clearanceRelease.AdminCompanyId = t.AdminCompanyId;
                                    _IClearanceReleaseModel.clearanceRelease.ArtistInfo = t.ArtistInfo;
                                    _IClearanceReleaseModel.clearanceRelease.ArtistName = t.ArtistName;
                                    _IClearanceReleaseModel.clearanceRelease.AssignedType = t.AssignedType;
                                    _IClearanceReleaseModel.clearanceRelease.CatalogueNo = t.CatalogueNo;
                                    _IClearanceReleaseModel.clearanceRelease.ComponentCount = t.ComponentCount;
                                    _IClearanceReleaseModel.clearanceRelease.Configuration = t.Configuration;
                                    _IClearanceReleaseModel.clearanceRelease.ConfigurationDisplay = t.ConfigurationDisplay;
                                    _IClearanceReleaseModel.clearanceRelease.DataAdminCompany = t.DataAdminCompany;
                                    _IClearanceReleaseModel.clearanceRelease.DataAdminCompanyId = t.DataAdminCompanyId;
                                    _IClearanceReleaseModel.clearanceRelease.DivisionId = t.DivisionId;
                                    _IClearanceReleaseModel.clearanceRelease.EarilerReleaseDate = t.EarilerReleaseDate;
                                    _IClearanceReleaseModel.clearanceRelease.Grid = t.Grid;
                                    _IClearanceReleaseModel.clearanceRelease.Id = t.Id;
                                    _IClearanceReleaseModel.clearanceRelease.IsAlreadyLinked = t.IsAlreadyLinked;
                                    _IClearanceReleaseModel.clearanceRelease.IsMac = t.IsMac;
                                    _IClearanceReleaseModel.clearanceRelease.IsMediaPortal = t.IsMediaPortal;
                                    _IClearanceReleaseModel.clearanceRelease.LabelId = t.LabelId;
                                    _IClearanceReleaseModel.clearanceRelease.labelName = _iClearanceReleaseRepository.getLabelNmForExistingRelease(Convert.ToInt32(t.LabelId), GetUserInfo());
                                    _IClearanceReleaseModel.clearanceRelease.LinkedContractDetails = t.LinkedContractDetails;
                                    _IClearanceReleaseModel.clearanceRelease.MobileArtist = t.MobileArtist;
                                    _IClearanceReleaseModel.clearanceRelease.MusicClassType = t.MusicClassType;
                                    _IClearanceReleaseModel.clearanceRelease.OwnedProjectId = t.OwnedProjectId;
                                    _IClearanceReleaseModel.clearanceRelease.PackageIndicator = t.PackageIndicator;
                                    _IClearanceReleaseModel.clearanceRelease.PackageInfo = t.PackageInfo;
                                    _IClearanceReleaseModel.clearanceRelease.PCompanyId = t.PCompanyId;
                                    _IClearanceReleaseModel.clearanceRelease.PCompanyName = t.PCompanyName;
                                    _IClearanceReleaseModel.clearanceRelease.PLicensingExtension = t.PLicensingExtension;
                                    _IClearanceReleaseModel.clearanceRelease.PYear = t.PYear;
                                    _IClearanceReleaseModel.clearanceRelease.R2AccountId = t.R2AccountId;
                                    _IClearanceReleaseModel.clearanceRelease.R2ReleaseId = t.ReleaseId;
                                    _IClearanceReleaseModel.clearanceRelease.R2Status = t.R2Status;
                                    _IClearanceReleaseModel.clearanceRelease.R2StatusType = t.R2StatusType;
                                    _IClearanceReleaseModel.clearanceRelease.ReleaseId = t.ReleaseId;
                                    _IClearanceReleaseModel.clearanceRelease.ReleaseTitle = t.ReleaseTitle;
                                    _IClearanceReleaseModel.clearanceRelease.ReleaseType = t.ReleaseType;
                                    _IClearanceReleaseModel.clearanceRelease.ScopeType = t.ScopeType;
                                    _IClearanceReleaseModel.clearanceRelease.Sequence = t.Sequence;
                                    _IClearanceReleaseModel.clearanceRelease.SoundtrackIndicator = t.SoundtrackIndicator;
                                    _IClearanceReleaseModel.clearanceRelease.TrackCount = t.TrackCount;
                                    _IClearanceReleaseModel.clearanceRelease.TrackInfo = t.TrackInfo;
                                    _IClearanceReleaseModel.clearanceRelease.Upc = t.Upc;
                                    _IClearanceReleaseModel.clearanceRelease.UserName = t.UserName;
                                    _IClearanceReleaseModel.clearanceRelease.VersionTitle = t.VersionTitle;

                                    List<TrackInfo> trackInfo = _iClearanceReleaseRepository.R2GetReleaseAdditionalDetails(clearanceReleaseModel.clearanceRelease.R2ReleaseId, GetUserInfo());
                                    if (trackInfo != null)
                                    {
                                        trackInfo = trackInfo.OrderBy(trackinf => trackinf.SequenceNo).ToList();
                                        _IClearanceReleaseModel.clearanceRelease.TrackInfo = trackInfo;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (_IClearanceReleaseModel.clearanceRelease.TrackInfo == null || _IClearanceReleaseModel.clearanceRelease.TrackInfo.Count == 0)
                            {
                                List<TrackInfo> trackInfo = _iClearanceReleaseRepository.R2GetReleaseAdditionalDetails(clearanceReleaseModel.clearanceRelease.R2ReleaseId, GetUserInfo());
                                if (trackInfo != null)
                                {
                                    trackInfo = trackInfo.OrderBy(trackinf => trackinf.SequenceNo).ToList();
                                    _IClearanceReleaseModel.clearanceRelease.TrackInfo = trackInfo;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_IClearanceReleaseModel.clearanceRelease.TrackInfo == null || _IClearanceReleaseModel.clearanceRelease.TrackInfo.Count == 0)
                        {
                            List<TrackInfo> trackInfo = _iClearanceReleaseRepository.R2GetReleaseAdditionalDetails(clearanceReleaseModel.clearanceRelease.R2ReleaseId, GetUserInfo());
                            if (trackInfo != null)
                            {
                                trackInfo = trackInfo.OrderBy(trackinf => trackinf.SequenceNo).ToList();
                                _IClearanceReleaseModel.clearanceRelease.TrackInfo = trackInfo;
                            }
                        }
                    }                    
                }

                LoggerFactory.LogWriter.MethodExit();
                return PartialView("ReleaseDetailsViewComponent", _IClearanceReleaseModel);
                
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                ViewBag.ValidationMsgPackage = "Error while fetching Release Details";
                return PartialView("ReleaseDetailsViewComponent", _IClearanceReleaseModel); 
            }
        }

        #endregion

    }
}
