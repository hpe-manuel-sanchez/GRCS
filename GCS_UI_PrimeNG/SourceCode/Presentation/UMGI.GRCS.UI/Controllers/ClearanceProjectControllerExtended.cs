using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.Enumerations;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.Resx.Resource.Layout;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.UI.Models;
using Constants = UMGI.GRCS.BusinessEntities.Constants.Constants;

namespace UMGI.GRCS.UI.Controllers
{
    [ValidateInput(false)]
    public partial class ClearanceProjectController
    {
        private ClearanceProjectModel GetPackageInfoForReleaseNew(ClearanceProjectModel model, FormCollection collection)
        {
            if (model.RegularProjectDetails.ObjRelease != null)
            {
                int i = 0;
                foreach (var release in model.RegularProjectDetails.ObjRelease)
                {
                    if (release.Package_Id == 1)
                    {
                        release.PackageIndicator = "N";
                    }
                    else if (release.Package_Id == 2)
                    {
                        release.PackageIndicator = "Y";
                    }
                    if (!string.IsNullOrEmpty(release.ExistingReleases))
                    {
                        var listSelectedItems = ParseStringRelease(release.ExistingReleases);

                        List<long> arrRemoveRelList = new List<long>();

                        if (!string.IsNullOrEmpty(release.RemovedPackageReleases) || !string.IsNullOrEmpty(collection["RegularProjectDetails.ObjRelease[" + i + "].RemovedPackageReleases"]))
                        {
                            if (!string.IsNullOrEmpty(release.RemovedPackageReleases))
                            {
                                if (release.RemovedPackageReleases != collection["RegularProjectDetails.ObjRelease[" + i + "].RemovedPackageReleases"].ToString())
                                {
                                    release.RemovedPackageReleases = collection["RegularProjectDetails.ObjRelease[" + i + "].RemovedPackageReleases"].ToString();
                                }
                            }
                            else
                            {
                                release.RemovedPackageReleases = collection["RegularProjectDetails.ObjRelease[" + i + "].RemovedPackageReleases"];
                            }
                            clearancePackageRelease.Add(new ClearanceRelease { ExistingReleases = release.ExistingReleases, RemovedPackageReleases = release.RemovedPackageReleases });

                            foreach (var remList in release.RemovedPackageReleases.Split(',').Select(r => r.Split('-')).Where(remList => remList[0] != ""))
                            {
                                long tryParseDiv = 0;
                                if (long.TryParse(remList[0], out tryParseDiv))
                                    arrRemoveRelList.Add(tryParseDiv);
                            }
                        }
                        else
                        {
                            clearancePackageRelease.Add(new ClearanceRelease { ExistingReleases = release.ExistingReleases });
                        }

                        listSelectedItems.RemoveAll(r => arrRemoveRelList.Contains(r.R2ReleaseId));

                        var existingR2ReleaseIdFromGcs = _IClearanceReleaseRepository.GetExistingR2ReleaseIdForPackage(listSelectedItems.Select(s => s.ReleaseId).ToList(), release.ReleaseId);

                        for (var j = 0; j < listSelectedItems.Count; j++)
                        {
                            if (release.PackageInfo != null)
                                release.PackageInfo.Add(new PackageInfo
                                                            {
                                                                ReleaseId = listSelectedItems[j].R2ReleaseId,
                                                                Upc = listSelectedItems[j].Upc,
                                                                Sequence = j + 1,
                                                                ParentId = 1,
                                                                IsNewlyAddedAfterSubmit = !existingR2ReleaseIdFromGcs.Contains(listSelectedItems[j].ReleaseId) ? true : false,
                                                                PackageId = listSelectedItems[j].Package_Id,
                                                                R2ReleaseId = listSelectedItems[j].ReleaseId,
                                                                ArchiveFlag = release.ExistingReleasePkg_Id == 1 && listSelectedItems[j].Package_Id != 0 ? "Y" : listSelectedItems[j].Archive_Flag
                                                            });
                        }

                        if (release.ExistingReleasePkg_Id == 1 && release.PackageInfo != null && release.PackageInfo.Count > 0)
                        {
                            _IClearanceReleaseRepository.UpdatePackage(release.PackageInfo, getUserInfo());
                        }
                    }
                    else
                    {
                        clearancePackageRelease.Add(new ClearanceRelease { ExistingReleases = release.ExistingReleases });
                    }
                    i = i + 1;
                }
            }

            return model;
        }

        private ClearanceProjectModel GetPackageInfoForOldModel(ClearanceProjectModel model)
        {
            if (model.RegularProjectDetails.ObjRelease != null)
            {
                int i = 0;
                foreach (var release in model.RegularProjectDetails.ObjRelease)
                {
                    if (release.Package_Id == 1)
                    {
                        release.PackageIndicator = "N";
                    }
                    else if (release.Package_Id == 2)
                    {
                        release.PackageIndicator = "Y";
                    }
                    if (!string.IsNullOrEmpty(release.ExistingReleases))
                    {
                        var listSelectedItems = ParseStringRelease(release.ExistingReleases);

                        clearancePackageRelease.Add(new ClearanceRelease { ExistingReleases = release.ExistingReleases });

                        var existingR2ReleaseIdFromGcs = _IClearanceReleaseRepository.GetExistingR2ReleaseIdForPackage(listSelectedItems.Select(s => s.ReleaseId).ToList(), release.ReleaseId);

                        for (var j = 0; j < listSelectedItems.Count; j++)
                        {
                            if (release.PackageInfo != null)
                                release.PackageInfo.Add(new PackageInfo
                                {
                                    ReleaseId = listSelectedItems[j].R2ReleaseId,
                                    Upc = listSelectedItems[j].Upc,
                                    Sequence = j + 1,
                                    ParentId = 1,
                                    IsNewlyAddedAfterSubmit = !existingR2ReleaseIdFromGcs.Contains(listSelectedItems[j].ReleaseId) ? true : false,
                                    PackageId = listSelectedItems[j].Package_Id,
                                    R2ReleaseId = listSelectedItems[j].ReleaseId,
                                    ArchiveFlag = release.ExistingReleasePkg_Id == 1 && listSelectedItems[j].Package_Id != 0 ? "Y" : listSelectedItems[j].Archive_Flag
                                });
                        }

                    }
                    else
                    {
                        clearancePackageRelease.Add(new ClearanceRelease { ExistingReleases = release.ExistingReleases });
                    }
                    i = i + 1;
                }
            }

            return model;
        }

        private void CacheData(ClearanceProjectModel model, string configList)
        {
            try
            {
                _type = new List<string>
                    {
                        Constants.ClearanceMusicType,
                        Constants.ClearancePriceLevelType,
                        Constants.ClearanceClubPriceLevel,
                        Constants.ClearanceICLALevelType,
                        Constants.ClearanceCurrPriceLevelType,
                        Constants.ClearanceReqPriceLevelType,
                        Constants.ClearanceResourceType,
                        Constants.ClearanceRecordingType,
                        Constants.ClearacePromotionalPriceLevel
                    };

                var iClearanceProjectModel = _IClearanceProjectRepository.GetMasterData(_type, getUserInfo());


                model.MusicType = iClearanceProjectModel.MusicType;
                model.MusicTypeResourceTab = iClearanceProjectModel.MusicType;
                model.PriceLevelType = iClearanceProjectModel.PriceLevelType;
                model.dropClubLevel = iClearanceProjectModel.dropClubLevel;
                model.ICLALevelType = iClearanceProjectModel.ICLALevelType;
                model.CurrPriceLevelList = iClearanceProjectModel.CurrPriceLevelList;
                model.RequestedPriceLevelList = iClearanceProjectModel.RequestedPriceLevelList;
                iClearanceProjectModel.ResourceType = iClearanceProjectModel.ResourceType.Where(x => (x.Text.ToUpper() == "AUDIO") || (x.Text.ToUpper() == "VIDEO")).ToList();
                model.ResourceTypeResourceTab = iClearanceProjectModel.ResourceType;
                model.RecordingTypeResourceTab = iClearanceProjectModel.RecordingType;
                model.dropPromotionalLevel = iClearanceProjectModel.dropPromotionalLevel;
                model.dropDeviatedIclaLevel = model.ICLALevelType.Select(i => new DropDeviatedICLALevel { Id = int.Parse(i.Value), Description = i.Text }).ToList();
                model.dropPriceLevel = model.PriceLevelType.Select(p => new DropPriceLevel { Id = int.Parse(p.Value), Description = p.Text }).ToList();

                if (model.RegularProjectDetails != null && model.RegularProjectDetails.ObjRelease != null)
                {
                    for (int i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                    {
                        model.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(model.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();

                    }
                }
                model.ConfigGroupList = _IClearanceReleaseRepository.GetReleaseConfigGroupList(getUserInfo()).ConfigGroupList;
                model.MusicTypeResourceTab = model.MusicTypeResourceTab.Where(m => m.Text.ToUpper() != "JAZZ").ToList();

                iClearanceProjectModel = _IClearanceProjectRepository.GetClearanceProjectDropDownByUserList(getUserInfo().UserLoginName);

                model.CurrencyList = iClearanceProjectModel.CurrencyList;
                model.CompanyList = iClearanceProjectModel.CompanyList;

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }

        [HttpPost]
        public JsonResult SaveRegularProject(string command, ClearanceProjectModel model, FormCollection collection)
        {
            LoggerFactory.LogWriter.Info("Controller Method Entry-SaveRegularProject");

            try
            {
                var isExistingOrNew = string.Empty;

                command = model.RegularProjectDetails.Command;

                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                }

                ViewBag.DefaultTab = collection["hdnDefaultTab"];

                var projectReferenceId = string.Empty;

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);

                //set artist for resource
                model = SetArtistDetailsForResourceRegular(model, collection);

                if (Request.Form.Count > 0)
                {
                    if (model.RegularProjectDetails.ReleaseNewOrExisting != null)
                    {
                        isExistingOrNew = model.RegularProjectDetails.ReleaseNewOrExisting;
                    }

                    if (isExistingOrNew.Equals("New"))
                    {
                        model = SetArtistDetailsForRelease(model, collection);

                        if (model.RegularProjectDetails.ObjRelease != null)
                        {
                            foreach (var clearanceRelease in model.RegularProjectDetails.ObjRelease)
                            {
                                clearanceRelease.ConfigIdSelected = clearanceRelease.ConfigId;
                            }
                        }
                        else
                        {
                            var releaseDetailGrsData = new ClearanceReleaseSearchResult { releaseDetail = new List<ClearanceRelease> { new ClearanceRelease { Archive_Flag = "N", TrackCount = null, No_Components = null } } };

                            model.RegularProjectDetails.ObjRelease = releaseDetailGrsData.releaseDetail;
                        }

                        model.RegularProjectDetails.IsExisting = false;
                    }
                    else
                    {
                        model.RegularProjectDetails.IsExisting = true;
                    }

                    SetRegularProjectEntities(command, model);

                    if (isExistingOrNew.Equals("New"))
                    {
                        GetPackageInfoForReleaseNew(model, collection);
                    }

                    model.RegularProjectDetails.ScopeAndRequestType = SaveRequestType(model);

                    if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                    {
                        if (collection.GetValues("hdnMultiartist")[0] != string.Empty)
                        {
                            model.RegularProjectDetails.MultiArtist = Convert.ToBoolean(collection.GetValues("hdnMultiartist")[0]);
                        }
                    }

                    var userInfo = getUserInfo();

                    if (model.RegularProjectDetails.ClearanceResource != null)
                    {
                        for (var i = 0; i < model.RegularProjectDetails.ClearanceResource.Count; i++)
                        {
                            if (string.IsNullOrEmpty(collection["hdnArtistRegular" + i])) continue;

                            if (model.RegularProjectDetails.ClearanceResource[i].R2_ResourceId == 0)
                            {
                                model.RegularProjectDetails.ClearanceResource[i].ArtistInfo = ParseArtistforResource(model.RegularProjectDetails.ClearanceResource[i], collection["hdnArtistRegular" + i]);
                            }
                        }
                    }

                    userInfo.UserLoginName = SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;

                    model.RegularProjectDetails = _IClearanceProjectRepository.SaveRegularProjectDetails(model.RegularProjectDetails, userInfo);

                    if (model.RegularProjectDetails.StatusType == (int)General.StatusType.ReSubmitted && model.RegularProjectDetails.IsSensitiveDataChanged)
                    {
                        model.RegularProjectDetails.IsSensitiveDataChanged = false;
                    }

                    Session["fileList" + "_" + model.RegularProjectDetails.ClrProjectId] = model.RegularProjectDetails.listUploadDocument;

                    projectReferenceId = model.RegularProjectDetails.ProjectReferenceId;

                    if (model.RegularProjectDetails.ObjRelease != null)
                    {
                        if (isExistingOrNew.Equals("New"))
                        {
                            for (var i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
                            {
                                model.RegularProjectDetails.ObjRelease[i].RemovedPackageReleases = clearancePackageRelease[i].RemovedPackageReleases;
                            }
                        }

                        model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                    }

                    model.RegularProjectDetails.ClearanceResource = model.RegularProjectDetails.ClearanceResource != null ? RemoveResourceList(model.RegularProjectDetails.ClearanceResource) : new List<ClearanceResource>();
                    model.UPCAllocationRightsGroup = checkPermissionForUPCAllocation(model.RegularProjectDetails.RequesterCompanyId, model.roleGroupName);
                    ModelState.Clear();
                }

                ViewBag.RoleGroup = model.roleGroupName;
                ViewBag.LoadTemplate = "1";
                LoggerFactory.LogWriter.Info("Controller Method Exit-SaveRegularProject");
                return Json(new { Result = "OK", Records = model });

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private ClearanceProjectModel SetArtistDetailsForResourceRegular(ClearanceProjectModel model, FormCollection collection)
        {
            if (model.RegularProjectDetails.ClearanceResource != null)
            {
                for (int i = 0; i < model.RegularProjectDetails.ClearanceResource.Count; i++)
                {
                    if (!string.IsNullOrEmpty(collection["hdnArtistRegular" + i.ToString()]))
                    {
                        if (model.RegularProjectDetails.ClearanceResource[i].R2_ResourceId == 0)
                        {
                            model.RegularProjectDetails.ClearanceResource[i].ArtistInfo = ParseArtistforResource(model.RegularProjectDetails.ClearanceResource[i], collection["hdnArtistRegular" + i.ToString()]);
                        }
                    }

                    if (model.RegularProjectDetails.ClearanceResource[i].ArtistInfo != null)
                    {
                        foreach (var artist in model.RegularProjectDetails.ClearanceResource[i].ArtistInfo)
                        {
                            artist.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                            if (model.RegularProjectDetails.ClearanceResource[i].R2_ResourceId == 0)
                            {
                                artist.IsPrimary = "Y";
                            }
                        }
                    }
                }

            }

            return model;
        }

        [Authorize]
        [HttpPost]
        public JsonResult SubmitRegularProject(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                var IsExistingOrNew = string.Empty;
                var ConfigList = string.Empty;
                var ProjectReferenceId = string.Empty;

                if (model.RegularProjectDetails.ScopeAndRequestType.TVRadioBreakICLA)
                {
                    if (collection.GetValues("hdnMultiartist")[0] != string.Empty)
                    {
                        model.RegularProjectDetails.MultiArtist = Convert.ToBoolean(collection.GetValues("hdnMultiartist")[0]);
                    }
                }
                
                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                }

                ViewBag.ProjectTerritories = SetManageTerritories(collection, model);
                model.RegularProjectDetails.ClearanceResource = 
                    model.RegularProjectDetails.ClearanceResource != null ? RemoveResourceList(model.RegularProjectDetails.ClearanceResource) : new List<ClearanceResource>();
                
                //*** start for filling dropdown****//
                if (collection.GetValues("hdnConfigList") != null)
                {
                    ConfigList = collection.GetValues("hdnConfigList")[0];
                }

                if (model.roleGroupName == BusinessEntities.Lookups.RoleGroup.None)
                {
                    model.roleGroupName = BusinessEntities.Lookups.RoleGroup.Requestor;
                }

                CacheData(model, ConfigList);

                //*****Start*****// change the status based on current status which is being settted in CreateRegularProject.js
                if (model.RegularProjectDetails.Command == General.StatusType.Submitted.ToString())
                {
                    model.RegularProjectDetails.StatusType = (int)General.StatusType.Submitted;
                    model.RegularProjectDetails.StatusTypeDesc = General.StatusType.Submitted.ToString();
                }
                else if (model.RegularProjectDetails.Command == General.StatusType.ReSubmitted.ToString())
                {
                    model.RegularProjectDetails.StatusType = (int)General.StatusType.ReSubmitted;
                    model.RegularProjectDetails.StatusTypeDesc = General.StatusType.ReSubmitted.ToString();
                }
                //*****END*******//

                // for maintaining the state of TAB selected
                ViewBag.DefaultTab = collection["hdnDefaultTab"].ToString();

                if (collection["hdnAdditionalResourceCheck"] != null)
                {
                    if (collection["hdnAdditionalResourceCheck"].ToString().Equals("NO"))
                        TempData["DefaultTabCheck"] = ViewBag.DefaultTab;
                }
              
                //set artist for resource
                model = SetArtistDetailsForResourceRegular(model, collection);

                //***** Territory Section End*********///                            

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
                        for (var i = 0; i < model.RegularProjectDetails.ObjRelease.Count; i++)
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
                            model.RegularProjectDetails.ObjRelease.ForEach(i => i.PackageInfo.ForEach(j => j.IsNewlyAddedAfterSubmit = false));
                        }
                    }

                    // save request type detail            
                    model.RegularProjectDetails.ScopeAndRequestType = SaveRequestType(model);

                    var userInfo = getUserInfo();
                    userInfo.UserLoginName = SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
                    model.RegularProjectDetails = _IClearanceProjectRepository.SaveRegularProjectDetails(model.RegularProjectDetails, userInfo);
                    model.UPCAllocationRightsGroup = checkPermissionForUPCAllocation(model.RegularProjectDetails.RequesterCompanyId, model.roleGroupName);

                    ProjectReferenceId = model.RegularProjectDetails.ProjectReferenceId;

                    if (model.RegularProjectDetails.StatusType == (int)General.StatusType.ReSubmitted && model.RegularProjectDetails.IsSensitiveDataChanged)
                    {
                        model.RegularProjectDetails.IsSensitiveDataChanged = false;
                    }

                    ModelState.Clear();
                    if (model.RegularProjectDetails.ClearanceResource != null)
                    {
                        model.RegularProjectDetails.ClearanceResource = model.RegularProjectDetails.ClearanceResource
                            .OrderBy(Resourcelist => Resourcelist.ArtistName).ThenBy(Resourcelist => Resourcelist.Title)
                            .ThenBy(Resourcelist => Resourcelist.VersionTitle)
                            .ToList();

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
                            string ConfigurationGroup_Id = model.RegularProjectDetails.ObjRelease[i].ConfigurationGroup_Id;
                            model.RegularProjectDetails.ObjRelease[i].ConfigListRelease = _IClearanceReleaseRepository.GetReleaseConfigList(ConfigurationGroup_Id, getUserInfo()).ConfigList.ToList();
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

                TempData["EntityForSubmtProject" + "_" + model.RegularProjectDetails.ClrProjectId] = model;

                LoggerFactory.LogWriter.MethodExit();

                return Json(new { Result = "OK", Records = model });

            }
            catch (Exception ex)
            {
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                LoggerFactory.LogWriter.Error(Category.Business, ex);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult ReleaseNewDuplicateButton(string command, ClearanceProjectModel model, FormCollection collection)
        {
            try
            {
                var configList = string.Empty;

                if (model.RegularProjectDetails.ObjRelease != null)
                {
                    foreach (var clearanceRealease in model.RegularProjectDetails.ObjRelease)
                    {
                        clearanceRealease.ConfigIdSelected = clearanceRealease.ConfigId;
                    }

                    model.RegularProjectDetails.ObjRelease = RemoveReleaseFromList(model.RegularProjectDetails.ObjRelease);
                }

                ViewBag.DefaultTab = collection["hdnDefaultTab"];

                if (collection.GetValues("hdnConfigList") != null)
                {
                    configList = collection.GetValues("hdnConfigList")[0];
                }



                model = SetArtistDetailsForRelease(model, collection);

                if (collection.GetValues("hdnDuplicate") == null)
                {
                    var releaseDetailGrsData = new ClearanceReleaseSearchResult
                    {
                        releaseDetail = new List<ClearanceRelease> { new ClearanceRelease { Archive_Flag = "N" } }
                    };

                    model.RegularProjectDetails.ObjRelease = releaseDetailGrsData.releaseDetail;
                }

                var releaseList = model.RegularProjectDetails.ObjRelease.CloneDataContract();

                if (releaseList != null)
                {
                    ViewData["rowCount"] = releaseList.Count;
                }

                var count = 0;

                var controlName = string.Empty;

                if (collection.GetValues("hdnDuplicate") != null)
                {
                    controlName = collection.GetValues("hdnDuplicate")[0];
                }

                if (!string.IsNullOrEmpty(controlName))
                {
                    var rowNum = controlName.Substring(13, controlName.Length - 13);

                    model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].ConfigGroup_List_Id = new List<string>();

                    var collectionList = collection.AllKeys.Where(s => s.Contains("chkConfig_" + rowNum)).ToList();

                    foreach (var configListItem in collectionList)
                    {
                        model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].ConfigGroup_List_Id.AddRange(collection.GetValues(configListItem));
                    }

                    if (model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].ConfigGroup_List_Id.Count > 0)
                    {
                        count = model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].ConfigGroup_List_Id.Count;
                    }
                    else if (!string.IsNullOrEmpty(model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].ConfigurationGroup_Id))
                    {
                        count = 1;
                    }

                    for (var i = 0; i < count; i++)
                    {
                        var release = model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].CloneDataContract();

                        release.ReleaseId = 0;
                        release.Upc = string.Empty;
                        release.Is_Upc_Manual = "N";
                        release.IsNewlyAddedAfterSubmit = true;

                        if (model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].ConfigGroup_List_Id.Count > 0)
                        {
                            release.ConfigurationGroup_Id = model.RegularProjectDetails.ObjRelease[Convert.ToInt32(rowNum)].ConfigGroup_List_Id[i];
                            release.ConfigId = 0;
                        }

                        releaseList.Add(release);
                    }

                    model.RegularProjectDetails.ObjRelease = releaseList;
                    ViewBag.DefaultTab = "2";
                }

                if (model.RegularProjectDetails.ClearanceResource != null)  //Remove resources where archive flag is set to "Y"
                {
                    model.RegularProjectDetails.ClearanceResource = RemoveResourceList(model.RegularProjectDetails.ClearanceResource);
                }
                CacheData(model, configList);
                ModelState.Clear();

                ViewData["btnId"] = controlName;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
            }

            return PartialView("ReleaseNewRegular", model);
        }

        [HttpPost]
        public ActionResult GetRequestsummaryData(string ClrProjectId)
        {
            try
            {
                ClearanceInboxRequest clearanceReq = new ClearanceInboxRequest();
                List<ClearanceInboxRequest> requestList;
                byte routingStatus;
                var serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = 2147483647;
                requestList = _IClearanceProjectRepository.GetRequestSummaryRequests(ClrProjectId.ToString(), 0, 10000, 0, null, getUserInfo(), out routingStatus);
                requestList.All(i =>
                {
                    i.ModifiedDateRequestString = serializer.Serialize(i.ModifiedDateRequest);
                    i.ModifiedDateRoutedString = serializer.Serialize(i.ModifiedDate);
                    return true;
                });


                var data = new { Result = Constants.JsonOk, Records = requestList.AsQueryable() };
                return new ContentResult { Content = serializer.Serialize(data), ContentType = "application/json" };
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                ViewBag.ValidationWarningMsg = ClearanceLayout.ErrorMessage;
                ContentResult re = new ContentResult();
                var serializer = new JavaScriptSerializer();
                var data = new { Result = Constants.JsonError, Message = ex.Message };
                return new ContentResult { Content = serializer.Serialize(data), ContentType = "application/json" };
            }
        }
    }
}
