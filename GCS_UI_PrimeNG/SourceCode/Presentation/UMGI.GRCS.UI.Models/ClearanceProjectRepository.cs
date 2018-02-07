/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ClearanceProjectRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Dhruv Arora
 * Created on     : 14-10-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Proxies.ClearanceProjectService;
using ProjectSearchResult = UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities.ProjectSearchResult;
using RoleType = UMGI.GRCS.BusinessEntities.Lookups.RoleType;

namespace UMGI.GRCS.UI.Models
{
    public class ClearanceProjectRepository : IClearanceProjectRepository
    {
        #region "Public Variables"


        private IConfigFactory ConfigurationFactory { get; set; }
        private IAddressBookModel AddressBookModel { get; set; }
        private IClearanceProjectModel IClearanceProjectModel { get; set; }
        private IClearanceResourceModel IClearanceResourceModel { get; set; }
        private IClearenceUserTransferModel IClearenceUserTransferModel { get; set; }
        readonly ILogFactory _logFactory;

        private string Isparent { get; set; }

        #endregion

        #region "Constants"
        /// <summary>
        /// Constants related to this class
        /// </summary>
        /// 
        const string _msgFillDeviatedICLALevel = "Fill Deviated ICLA Level Method Initiated";
        const string _msgFillPriceLevel = "Fill Price Level Method Initiated";
        const string _msgSearchR2MockDataResource = "Search R2 Mock Data Resource Method Initiated";
        const string _msgFillPromotionalPriceLevel = "Fill Promotional Price Level Method Initiated";
        const string _msgFillClubPriceLevel = "Fill Club Price Level Method Initiated";
        const string _msgCreateProject = "Create Project Method Initiated";
        const string _msgProjectSearch = "Project Search Method Initiated";
        const string _msggetMusicClassTypeIdForExistingRelease = "Get Music Class Type Id For Existing Release Method Initiated";
        const string _msggetLabelNmForExistingRelease = "Get label NM For Existing Release Method Initiated";
        const string _msgFillProjectSearchDropDown = "Fill Project Search DropDown Method Initiated";
        const string _msgGetClearanceProjectDropDownByUserList = "Get Clearance Project DropDown By UserList Method Initiated";
        const string _msgSearchProject = "Search Project Method Initiated";
        const string _msgSaveProjectDetails = "Search Project Detail Method Initiated";
        const string _msgGetMasterProjectDetails = "Get Master Project Details Method Initiated";
        const string _msgGetCurrentPricelevel = "Get Current Price Level Method Initiated";
        const string _msgGetRequestedPriceLevel = "Get Requested Price Level Method Initiated";
        const string _msgGetReleaseConfigList = "Get Release Config List Method Initiated";
        const string _msgSaveRequestType = "Save Request Type Method Initiated";
        const string _msgSaveRegularProjectDetails = "Save Regular Project Details Method Initiated";
        const string _msgCompanySearch = "Company Search Method Initiated";
        const string _msgGetCompanies = "Get Companies Method Initiated";
        const string _msgGetMasterData = "Get Master Data Method Initiated";
        const string _msgGetInquiryClearanceProjectSearchDropDown = "Get Inquirty Clearance Project Search Drop Down Method Initiated";
        const string _msgInquiryClearanceProjectSearch = "Inquiry Clearance Project Search Method Initiated";
        const string _msgGetClearanceProjectRequestTypeDropDownByProjectType = "Get Clearance Project Request Type DropdownBy Project Type Method Initiated";
        const string _msgGetExistingReleases = "Get Existing Releases Method Initiated";
        const string _msgGetRegularReleases = "Get Regular Releases Method Initiated";
        const string _msgGetRegularResources = "Get Regular Resources Method Initiated";
        const string _msgTransferUser = "Transfer User Method Initiated";
        const string _msgFillWorkGroupUserDropDown = "Fill Work Group User Drop Down Method Initiated";
        const string _msgSearchProjectForUserTransfer = "Search Project For User Transfer Method Initiated";
        const string _msgRemoveFile = "Remove File Method Initiated";
        const string _msgReadFile = "Read File Method Initiated";
        const string _msgCancelProject = "Cancel Project Method Initiated";
        const string _msgGetRegularProjectDetails = "Get Regular Project Details Method Initiated";
        const string _msgGetRegularProjectInformation_Request = "Get Regular Project Information and Request Type Method Initiated";
        const string _msgGetRegularProjectReleases = "Get Regular Project Releases Method Initiated";
        const string _msgGetRegularProjectResources = "Get Regular Project Resources Method Initiated";
        const string _msgCompleProject = "Complete Project Method Initiated";
        const string _msgStatusProjectUpdate = "Project Status Update Method Initiated";
        const string _msgRemoveReleaseProject = "Remove Release Project Method Initiated";
        const string _msgGetCompaniesPushToR2 = "Company Search Method Initiated";
        const string _msgGetDivisionPushToR2 = "Get Division Push To R2 Method Initiated";
        const string _msgGetLabelPushToR2 = "Get Label Push TO R2 Method Initiated";
        const string _msgSaveR2ProjectIdLinking = "Save R2 Project Id Linking Method Initiated";
        const string _msgAutoCompleteSearch = "Auto Complete Search Method Initiated";
        const string _msgSaveReleaseResourceToPushToR2 = "Save Release Resource To Push TO R2 Method Initiated";
        const string _msgGetRequestSummaryRequests = "Get Request Summary Requests Method Initiated";
        const string _msgClearanceRoutingAction = "Clearance Routing Action Method Initiated";
        const string _msgGetSafeCountries = "Get Safe Countries Method Initiated";
        const string _msgGetRequestorCompany = "Get Requestor Company Method Initiated";
        const string _msgSaveContractDetail = "Save Contract Resource Linking Method Initiated";
        const string _msgProvideSearchInput = "Provide atleast one input";
        #endregion

        #region "Public Property"


        /// <summary>
        /// Get/Set Teritory details
        /// </summary>
        private IManageTerritoryModel<TerritorialDisplay> ManageTerritoryModel { get; set; }

        #endregion

        #region "Project Repository Constructor"
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractRepository"/> class.
        /// </summary>
        public ClearanceProjectRepository(IConfigFactory configFactory, ILogFactory logFactory)
        {
            try
            {
                _logFactory = logFactory;
                ConfigurationFactory = configFactory;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Fill the ICLA level 
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        //public List<DropDeviatedICLALevel> FillDeviatedICLALevel(LeanUserInfo userInfo)
        //{
        //    ClearanceProjectClient clearanceProjectClient = null;
        //    try
        //    {

        //        _logFactory.LogWriter.Debug(_msgFillDeviatedICLALevel);
        //        List<DropDeviatedICLALevel> dropDeviatedICLALevel = new List<DropDeviatedICLALevel>();
        //        IClearanceProjectModel = new ClearanceProjectModel();
        //        clearanceProjectClient = new ClearanceProjectClient();
        //        clearanceProjectClient.Open();
        //        dropDeviatedICLALevel = clearanceProjectClient.FillDeviatedICLALevel(userInfo);
        //        IClearanceProjectModel.dropDeviatedIclaLevel = dropDeviatedICLALevel;
        //        clearanceProjectClient.Close();
        //        return dropDeviatedICLALevel;
        //    }
        //    catch (FaultException ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //    }
        //}

        /// <summary>
        /// Fill the Price Level
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        //public List<DropPriceLevel> FillPriceLevel(LeanUserInfo userInfo)
        //{
        //    ClearanceProjectClient clearanceProjectClient = null;
        //    try
        //    {
        //        _logFactory.LogWriter.Debug(_msgFillPriceLevel);
        //        List<DropPriceLevel> dropPriceLevel = new List<DropPriceLevel>();
        //        IClearanceProjectModel = new ClearanceProjectModel();
        //        clearanceProjectClient = new ClearanceProjectClient();
        //        clearanceProjectClient.Open();
        //        dropPriceLevel = clearanceProjectClient.FillPriceLevel(userInfo);               
        //        IClearanceProjectModel.dropPriceLevel = dropPriceLevel;
        //        clearanceProjectClient.Close();
        //        return dropPriceLevel;
        //    }
        //    catch (FaultException ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);                
        //        throw;
        //    }
        //    finally
        //    {
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //    }
        //}

        /// <summary>
        /// Fill the Promotional Price Level
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        //public List<DropPromotionalLevel> FillPromotionalPriceLevel(LeanUserInfo userInfo)
        //{
        //    ClearanceProjectClient clearanceProjectClient = null;
        //    try
        //    {
        //        _logFactory.LogWriter.Debug(_msgFillPromotionalPriceLevel);
        //        List<DropPromotionalLevel> dropPriceLevel = new List<DropPromotionalLevel>();
        //        IClearanceProjectModel = new ClearanceProjectModel();
        //        clearanceProjectClient = new ClearanceProjectClient();
        //        clearanceProjectClient.Open();
        //        dropPriceLevel = clearanceProjectClient.FillPromotionalPriceLevel(userInfo);
        //        IClearanceProjectModel.dropPromotionalLevel = dropPriceLevel;
        //        clearanceProjectClient.Close();
        //        return dropPriceLevel;
        //    }
        //    catch (FaultException ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //    }
        //}
        /// <summary>
        /// Fill the Club Price Level
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        //public List<DropClubLevel> FillClubPriceLevel(LeanUserInfo userInfo)
        //{
        //    ClearanceProjectClient clearanceProjectClient = null;
        //    try
        //    {
        //        _logFactory.LogWriter.Debug(_msgFillClubPriceLevel);
        //        List<DropClubLevel> dropPriceLevel = new List<DropClubLevel>();
        //        IClearanceProjectModel = new ClearanceProjectModel();
        //        clearanceProjectClient = new ClearanceProjectClient();
        //        clearanceProjectClient.Open();
        //        dropPriceLevel = clearanceProjectClient.FillClubPriceLevel(userInfo);
        //        IClearanceProjectModel.dropClubLevel = dropPriceLevel;
        //        clearanceProjectClient.Close();
        //        return dropPriceLevel;
        //    }
        //    catch (FaultException ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //    }
        //}
        /// <summary>
        /// Get MusicType ID for existing release
        /// <param name="userInfo"></param>
        /// <param name="musicType"></param>
        /// </summary>
        /// 
        public int getMusicClassTypeIdForExistingRelease(string musicType, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msggetMusicClassTypeIdForExistingRelease);
                int MusicClassTypeId = 0;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                MusicClassTypeId = clearanceProjectClient.getMusicClassTypeIdForExistingRelease(musicType, userInfo);
                clearanceProjectClient.Close();
                return MusicClassTypeId;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        /// <summary>
        /// Get Label Name for existing release
        /// <param name="userInfo"></param>
        /// <param name="LabelId"></param>
        /// </summary>
        /// 
        public string getLabelNmForExistingRelease(int LabelId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msggetLabelNmForExistingRelease);
                string labelName = string.Empty;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                labelName = clearanceProjectClient.getLabelNmForExistingRelease(LabelId, userInfo);
                clearanceProjectClient.Close();
                return labelName;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        #region "Fill project search drop down"
        /// <summary>
        /// Fill project search dropdown
        /// <param name="userInfo"></param>

        /// </summary>
        /// 
        public IClearanceProjectModel FillProjectSearchDropDown(LeanUserInfo userInfo = null)
        {

            List<ClearanceMasterData> _dropDownList = null;
            List<ClearanceMasterData> _WorkGroupDropdownList = new List<ClearanceMasterData>();
            IClearanceProjectModel = new ClearanceProjectModel();
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgFillProjectSearchDropDown);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                //if (_dropDownList == null)
                //{
                _dropDownList = clearanceProjectClient.FillProjectSearchDropDown(userInfo);
                //}

                if (userInfo != null)
                {
                    //string strLoginName = userInfo.UserLoginName.ToString();
                    _WorkGroupDropdownList = clearanceProjectClient.GetWorkGroupDropdown(userInfo.UserLoginName);
                }

                IClearanceProjectModel.ProjectType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceProjectType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });

                IClearanceProjectModel.RequestType = _dropDownList.Where(ClearanceMasterData => (ClearanceMasterData.Type == Constants.ClearanceRequestType) || (ClearanceMasterData.Type == Constants.ClearanceRegularRequestType)).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                // IClearanceProjectModel.ConfigGroupList=_WorkGroupDropdownList.Select(item => new SelectListItem { Text = item.Type, Value = item.Value.ToString() });


                //count the number of items in workgroup
                if (_WorkGroupDropdownList.Count > 1)
                {
                    _WorkGroupDropdownList.Insert(0, new ClearanceMasterData { Description = "Select", Value = 0, Type = null });
                }

                IClearanceProjectModel.ConfigGroupList = _WorkGroupDropdownList.Select(item => new SelectListItem { Text = item.Description, Value = item.Value.ToString() });
                clearanceProjectClient.Close();
                return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }
        #endregion


        #region "Fill Master Project Company/Currency dropdown"
        /// <summary>
        /// Get ClearanceProject currency and company DropDown
        /// <param name="loginName"></param>
        /// </summary>
        /// 
        public IClearanceProjectModel GetClearanceProjectDropDownByUserList(string loginName)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetClearanceProjectDropDownByUserList);
                List<ClearanceMasterData> _dropDownList = null;
                IClearanceProjectModel = new ClearanceProjectModel();
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                //if (_dropDownList == null)
                _dropDownList = clearanceProjectClient.GetClearanceProjectDropDownByUserList(loginName);
                IClearanceProjectModel.CurrencyList = _dropDownList.Where(item => item.Type == "Currency").Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) }).OrderBy(item => item.Text);
                IClearanceProjectModel.CompanyList = _dropDownList.Where(item => item.Type == "Company").Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) }).OrderBy(item => item.Text);
                clearanceProjectClient.Close();
                return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }


        }
        #endregion


        #region "Search Clearance Project"
        /// <summary>
        /// Project search and get result
        /// <param name="projectSearchCriteria"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public ProjectSearchResult SearchProject(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchProject);
                if (projectSearchCriteria.ProjectReferenceId == null && projectSearchCriteria.ProjectTypeId == -1 && projectSearchCriteria.ThirdPartyCompany == null && projectSearchCriteria.ProjectTitle == null && projectSearchCriteria.RequestTypeID == -1 && projectSearchCriteria.Requestor == null && projectSearchCriteria.LocalReference == null && projectSearchCriteria.RequestingCompany == null)
                {
                    throw new Exception(_msgProvideSearchInput);
                }

                var calcStartIndex = projectSearchCriteria.jtStartIndex;
                if (projectSearchCriteria.jtStartIndex >= ConfigurationFactory.PageSize)
                    calcStartIndex = projectSearchCriteria.jtStartIndex / ConfigurationFactory.PageSize * projectSearchCriteria.jtPageSize;
                projectSearchCriteria.jtStartIndex = calcStartIndex;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ProjectSearchResult projectSearchResult = new ProjectSearchResult();
                projectSearchResult=clearanceProjectClient.ClearanceProjectSearch(projectSearchCriteria, userInfo);
                clearanceProjectClient.Close();
                return projectSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region "Save Master Project Details"
        /// <summary>
        /// Save Master Project Details
        /// </summary>
        /// <param name="MasterProject">Master Project Details</param>
        /// <param name="userInfo">User Information</param>
        public MasterProject SaveProjectDetails(MasterProject masterProjectDetails, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {

                _logFactory.LogWriter.Debug(_msgSaveProjectDetails);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                MasterProject masterproject = new MasterProject();
                masterproject=clearanceProjectClient.SaveProjectDetails(masterProjectDetails, userInfo);
                clearanceProjectClient.Close();
                return masterproject;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        #endregion


        #region "Get Master Project Details"
        /// <summary>
        /// Save Master Project Details
        /// </summary>
        /// <param name="MasterProject">Master Project Details</param>
        /// <param name="userInfo">User Information</param>
        public MasterProject GetMasterProjectDetails(int clearanceProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetMasterProjectDetails);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                MasterProject masterproject = new MasterProject();
                masterproject=clearanceProjectClient.GetMasterProjectDetails(clearanceProjectId, userInfo);
                clearanceProjectClient.Close();
                return masterproject;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        #endregion

        #region "Get Price level"
        /// <summary>
        /// Get Current Price level
        /// </summary>
        //public List<RequestTypeRegular> GetCurrentPricelevel(LeanUserInfo userInfo)
        //{
        //    ClearanceProjectClient clearanceProjectClient = null;
        //    try
        //    {
        //        _logFactory.LogWriter.Debug(_msgGetCurrentPricelevel);
        //        clearanceProjectClient = new ClearanceProjectClient();
        //        clearanceProjectClient.Open(); 
        //        List<RequestTypeRegular> currentPriceLevel = new List<RequestTypeRegular>();
        //        currentPriceLevel = clearanceProjectClient.GetCurrentPricelevel(userInfo);
        //        clearanceProjectClient.Close();
        //        return currentPriceLevel;
        //    }
        //    catch (FaultException ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //    }

        //}

        /// <summary>
        /// Get requested Price level
        /// </summary>
        //public List<RequestTypeRegular> GetRequestedPriceLevel(LeanUserInfo userInfo)
        //{
        //    ClearanceProjectClient clearanceProjectClient = null;
        //    try
        //    {
        //        _logFactory.LogWriter.Debug(_msgGetRequestedPriceLevel);
        //        clearanceProjectClient = new ClearanceProjectClient();
        //        clearanceProjectClient.Open();
        //        List<RequestTypeRegular> requestedPriceLevel = new List<RequestTypeRegular>();
        //        requestedPriceLevel = clearanceProjectClient.GetRequestedPriceLevel(userInfo);
        //        clearanceProjectClient.Close();
        //        return requestedPriceLevel;
        //    }
        //    catch (FaultException ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
        //        {
        //            clearanceProjectClient.Abort();
        //            clearanceProjectClient = null;
        //        }
        //    }

        //}

        #endregion


        #region "Save Request Type Regular"
        /// <summary>
        /// Save Request Type Regular
        /// </summary>
        /// <param name="MasterProject">Master Project Details</param>
        /// <param name="userInfo">User Information</param>
        public bool SaveRequestType(ClearanceRegularProject requestTypeRegular, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSaveRequestType);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result=clearanceProjectClient.SaveRequestType(requestTypeRegular, userInfo);
                clearanceProjectClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        #endregion

        #region "Save Regular Project Details"
        /// <summary>
        /// Save Regular Project Details
        /// </summary>
        /// <param name="MasterProject">Regular Project Details</param>
        /// <param name="userInfo">User Information</param>
        public ClearanceRegularProject SaveRegularProjectDetails(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Info("Repository Method Entry-SaveRegularProject");
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ClearanceRegularProject clearanceRegularProject = new ClearanceRegularProject();
                clearanceRegularProject=clearanceProjectClient.SaveRegularProjectDetails(regularProjectDetails, userInfo);
                clearanceProjectClient.Close();
                _logFactory.LogWriter.Info("Repository Method Exit-SaveRegularProject");
                return clearanceRegularProject;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        #endregion

        #region Third Party Search
        /// <summary>
        /// Search Company
        /// <param name="companyName"></param>
        /// <param name="country"></param>
        /// <param name="isacCode"></param>
        /// <param name="isPaging"></param>
        /// <param name="pageSize"></param>
        /// <param name="startIndex"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, bool isPaging, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgCompanySearch);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open(); 
                List<CompanyInfo> compList = new List<CompanyInfo>();
                compList = clearanceProjectClient.SearchCompany(companyName, isacCode, country, startIndex, pageSize, isPaging, userInfo);
                clearanceProjectClient.Close();
                return compList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        /// <summary>
        /// GetCompanies
        /// <param name="companyIds"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public List<CompanyInfo> GetCompanies(string companyIds, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetCompanies);
                List<CompanyInfo> compList = new List<CompanyInfo>();
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                compList = clearanceProjectClient.GetCompanies(companyIds, userInfo);
                clearanceProjectClient.Close();
                return compList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion Third Party Search



        /// <summary>
        /// Fetch Master Data
        /// <param name="userInfo"></param>
        /// <param name="inputMasterDataType"></param>
        /// </summary>
        /// 
        public IClearanceProjectModel GetMasterData(List<string> inputMasterDataType, LeanUserInfo userInfo)
        {

            List<ClearanceMasterData> _dropDownList = null;
            ClearanceProjectClient clearanceProjectClient = null;
            IClearanceProjectModel = new ClearanceProjectModel();
            try
            {
                _logFactory.LogWriter.Debug(_msgGetMasterData);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                _dropDownList = clearanceProjectClient.GetMasterData(inputMasterDataType, userInfo);
                if (_dropDownList != null)
                {
                    IClearanceProjectModel.MusicType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceMusicType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });

                    IClearanceProjectModel.ICLALevelType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceICLALevelType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });

                    IClearanceProjectModel.CurrPriceLevelList = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearancePriceLevelType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                    IClearanceProjectModel.RequestedPriceLevelList = _dropDownList
                        .Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearancePriceLevelType)
                        .Where(ClearanceMasterData => ClearanceMasterData.Value != Constants.PriceLevel.Top.GetHashCode())
                        .Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                    IClearanceProjectModel.PriceLevelType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearancePriceLevelType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });

                    IClearanceProjectModel.dropClubLevel = (from d in _dropDownList
                                                            where d.Type == Constants.ClearanceClubPriceLevel
                                                            select new DropClubLevel()
                                                            {
                                                                Id = (int)d.Value,
                                                                Description = d.Description
                                                            }).ToList();
                    IClearanceProjectModel.dropPromotionalLevel = (from d in _dropDownList
                                                                   where d.Type == Constants.ClearacePromotionalPriceLevel
                                                                   select new DropPromotionalLevel()
                                                                   {
                                                                       Id = (int)d.Value,
                                                                       Description = d.Description
                                                                   }).ToList();

                    IClearanceProjectModel.ResourceType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceResourceType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                    IClearanceProjectModel.RecordingType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceRecordingType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                }
                clearanceProjectClient.Close();
                return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }


        #region "Inquiry Search"

        #region GetInquiryClearanceProjectSearchDropDown
        /// <summary>
        /// Get the Project Inquiry search drop down
        /// <param name="userInfo"></param>
        /// <param name="loginName"></param>
        /// </summary>
        /// 
        public IClearanceProjectModel GetInquiryClearanceProjectSearchDropDown(string loginName, LeanUserInfo userInfo)
        {

            List<ClearanceMasterData> _dropDownList = null;
            List<ClearanceMasterData> _dropDownCompanyList = null;
            ClearanceProjectClient clearanceProjectClient = null;
            IClearanceProjectModel = new ClearanceProjectModel();
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                _logFactory.LogWriter.Debug(_msgGetInquiryClearanceProjectSearchDropDown);
                //if (_dropDownList == null)
                _dropDownList = clearanceProjectClient.FillProjectSearchDropDown(userInfo);
                //if (_dropDownCompanyList == null)
                _dropDownCompanyList = clearanceProjectClient.GetClearanceProjectDropDownByUserList(loginName);

                IClearanceProjectModel.ProjectType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceProjectType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                IClearanceProjectModel.RequestType = _dropDownList.Where(ClearanceMasterData => (ClearanceMasterData.Type == Constants.ClearanceRequestType) || (ClearanceMasterData.Type == Constants.ClearanceRegularRequestType)).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                IClearanceProjectModel.CompanyList = _dropDownCompanyList.Where(ClearanceMasterData => ClearanceMasterData.Type == "Company").Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                clearanceProjectClient.Close();

                return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region InquiryClearanceProjectSearch
        /// <summary>
        /// Inquiry Clearance Project Search
        /// <param name="loginName"></param>
        /// <param name="projectSearchCriteria"></param>
        /// </summary>
        /// 
        public ProjectSearchResult InquiryClearanceProjectSearch(ClearanceProjectInquirySearchCriteria projectSearchCriteria, string loginName)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgInquiryClearanceProjectSearch);
               
                
                 clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ProjectSearchResult projectSearchResult = new ProjectSearchResult();
                projectSearchResult=clearanceProjectClient.ClearanceProjectInquirySearch(projectSearchCriteria, loginName);
                clearanceProjectClient.Close();
                return projectSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region "GetClearanceProjectRequestTypeDropDownByProjectType"
        /// <summary>
        /// Get Request type drop down
        /// <param name="RequestTypeId"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public List<ClearanceMasterData> GetClearanceProjectRequestTypeDropDownByProjectType(int RequestTypeId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetClearanceProjectRequestTypeDropDownByProjectType);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                List<ClearanceMasterData> RequestTypeList = new List<ClearanceMasterData>();
                RequestTypeList = clearanceProjectClient.GetClearanceProjectRequestTypeDropDownByProjectType(RequestTypeId, userInfo);
                clearanceProjectClient.Close();
                return RequestTypeList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion
        #endregion



        #region GetRegularReleases
        /// Get Regular Release
        /// <param name="searchcriteria"></param>
        /// 
        /// </summary>
        /// 
        public IClearanceProjectModel GetRegularReleases(ClearanceReleaseSearch searchcriteria)
        {
            try
            {
                _logFactory.LogWriter.Debug(_msgGetRegularReleases);
                IClearanceProjectModel = new ClearanceProjectModel();
                if (HttpContext.Current.Session["ReleaseExistingModel"] != null)
                    return ((IClearanceProjectModel)HttpContext.Current.Session["ReleaseExistingModel"]);
                else
                    return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }
        #endregion

        #region GetRegularResources
        /// Get Regular Resources
        /// <param name="upcNumber"></param>
        /// 
        /// </summary>
        /// 
        public List<ClearanceResource> GetRegularResources(int upcNumber)
        {
            try
            {
                _logFactory.LogWriter.Debug(_msgGetRegularResources);
                List<ClearanceResource> resources = new List<ClearanceResource>();

                resources.Add(new ClearanceResource() { Title = "Resource title1", Isrc = "1" });
                resources.Add(new ClearanceResource() { Title = "Resource title2", Isrc = "2" });

                return resources;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }
        #endregion

        #region "Transfer Ownership"
        /// TransferUser
        /// <param name="UserTransferList"></param>
        /// <param name="NewOwnerId"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public bool TransferUser(string[] UserTransferList, int NewOwnerId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgTransferUser);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                var result = clearanceProjectClient.TransferUser(UserTransferList.ToList(), NewOwnerId, userInfo);
                clearanceProjectClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region "Get workgroup user"
        /// Get WorkGroup User DropDown
        /// <param name="userInfo"></param>
        /// <param name="WorkgroupID"></param>
        /// </summary>
        /// 
        public IClearenceUserTransferModel FillWorkGroupUserDropDown(LeanUserInfo userInfo, int WorkgroupID)
        {
            List<UserTransfer> _dropDownList = new List<UserTransfer>();
            IClearenceUserTransferModel = new ClearenceUserTransferModel();
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgFillWorkGroupUserDropDown);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                _dropDownList = clearanceProjectClient.FillWorkGroupUserDropDown(userInfo, WorkgroupID);
                if (_dropDownList != null)
                {
                    IClearenceUserTransferModel.WorkgroupUser = _dropDownList.Select(item => new SelectListItem { Text = item.To, Value = item.touserId.ToString() });
                }
                clearanceProjectClient.Close();
                return IClearenceUserTransferModel;


            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }

        #endregion



        #region "Search Clearance Project for User Transfer"
        /// Search project 
        /// <param name="projectSearchCriteria"></param>
        /// <param name="_arrstatuslist"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public ProjectSearchResult SearchProjectForUserTransfer(ClearanceProjectSearchCriteria projectSearchCriteria, int[] _arrstatuslist, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchProjectForUserTransfer);
                if (projectSearchCriteria.ProjectReferenceId == null 
                    && projectSearchCriteria.ProjectTypeId == -1 
                    && projectSearchCriteria.ThirdPartyCompany == null 
                    && projectSearchCriteria.ProjectTitle == null 
                    && projectSearchCriteria.RequestTypeID == -1 
                    && projectSearchCriteria.Requestor == null 
                    && projectSearchCriteria.LocalReference == null 
                    && projectSearchCriteria.RequestingCompany == null 
                    && projectSearchCriteria.WorkgroupID <= 0)
                {
                    throw new Exception(_msgProvideSearchInput);
                }

                var calcStartIndex = projectSearchCriteria.jtStartIndex;
                if (projectSearchCriteria.jtStartIndex >= ConfigurationFactory.PageSize)
                    if (ConfigurationFactory.PageSize == 0)
                    {
                        ConfigurationFactory.PageSize = 25;
                    }
                calcStartIndex = projectSearchCriteria.jtStartIndex / ConfigurationFactory.PageSize * projectSearchCriteria.jtPageSize;
                projectSearchCriteria.jtStartIndex = calcStartIndex;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ProjectSearchResult projectSearchResult = new ProjectSearchResult();
                projectSearchResult=clearanceProjectClient.SearchProjectForUserTransfer(projectSearchCriteria, _arrstatuslist.ToList(), userInfo);
                clearanceProjectClient.Close();
                return projectSearchResult;
            }

            catch (DivideByZeroException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }

            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region Remove File
        /// RemoveFile
        /// <param name="documentId"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public bool RemoveFile(int documentId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgRemoveFile);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result=clearanceProjectClient.RemoveFile(documentId, userInfo);
                clearanceProjectClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }
        public UploadDocument ReadFile(int documentId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgReadFile);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                UploadDocument uploadDocument = new UploadDocument();
                uploadDocument=clearanceProjectClient.ReadFile(documentId, userInfo);
                clearanceProjectClient.Close();
                return uploadDocument;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }


        #endregion


        #region InquiryClearanceProjectSearch
        /// Inquiry Clearance ProjectSearch
        /// <param name="SearchCriteria"></param>

        /// </summary>
        /// 
        public ProjectSearchResult InquiryClearanceProjectSearch(ClearanceProjectSearchCriteria SearchCriteria)
        {
            throw new NotImplementedException();
        }
        #endregion





        #region GetRegularProjectDetails
        /// Get RegularProject Details
        /// <param name="userInfo"></param>
        /// <param name="clearanceProjectId"></param>
        /// </summary>
        /// 
        public ClearanceRegularProject GetRegularProjectDetails(int clearanceProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetRegularProjectDetails);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ClearanceRegularProject clearanceRegularProject = new ClearanceRegularProject();
                clearanceRegularProject=clearanceProjectClient.GetRegularProjectDetails(clearanceProjectId, userInfo);
                clearanceProjectClient.Close();
                return clearanceRegularProject;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region GetRegularProjectInfo_RequestType
        /// <summary>
        /// Get Regular project- Project Information and Request Type Tab
        /// </summary>
        /// <param name="clearanceProjectId"></param>
        /// <param name="userInfo"></param>
        /// <returns>ClearanceRegularProject</returns>

        public ClearanceRegularProject GetRegularProjectInfo_RequestType(int clearanceProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetRegularProjectInformation_Request);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ClearanceRegularProject clearanceRegularProject = new ClearanceRegularProject();
                clearanceRegularProject= clearanceProjectClient.GetRegularProjectInfo_RequestType(clearanceProjectId, userInfo);
                clearanceProjectClient.Close();
                return clearanceRegularProject;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion


        #region GetRegularProjectReleases
        /// <summary>
        /// Get Regular project Releases
        /// </summary>
        /// <param name="clearanceProjectId"></param>
        /// <param name="userInfo"></param>
        /// <returns>ClearanceRegularProject</returns>
        public ClearanceRegularProject GetRegularProjectReleases(int clearanceProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetRegularProjectReleases);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ClearanceRegularProject clearanceRegularProject = new ClearanceRegularProject();
                clearanceRegularProject = clearanceProjectClient.GetRegularProjectReleases(clearanceProjectId, userInfo);
                clearanceProjectClient.Close();
                return clearanceRegularProject;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region GetRegularProjectResources
        /// <summary>
        /// Get Regular Project Resources
        /// </summary>
        /// <param name="clearanceProjectId"></param>
        /// <param name="userInfo"></param>
        /// <returns>ClearanceRegularProject</returns>
        public ClearanceRegularProject GetRegularProjectResources(int clearanceProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetRegularProjectResources);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ClearanceRegularProject clearanceRegularProject = new ClearanceRegularProject();
                clearanceRegularProject=clearanceProjectClient.GetRegularProjectResources(clearanceProjectId, userInfo);
                clearanceProjectClient.Close();
                return clearanceRegularProject;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion


        #region StatusProjectUpdate
        // added by dhruv
        /// Project status Update
        /// <param name="userInfo"></param>
        /// <param name="projectId"></param>
        /// <param name="statustype"></param>
        /// </summary>
        /// 
        public bool StatusProjectUpdate(long projectId, int statustype, ClearanceRoutedProject clearanceRoutedProjectData, LeanUserInfo user, DateTime ModifiedDate)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgStatusProjectUpdate);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result = clearanceProjectClient.StatusProjectUpdate(projectId, statustype, clearanceRoutedProjectData, user, ModifiedDate);
                clearanceProjectClient.Close();
                return result;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region RemoveReleaseProject
        /// Remove Release Project
        /// <param name="userInfo"></param>
        /// <param name="clearanceRelease"></param>
        /// </summary>
        /// 
        public bool RemoveReleaseProject(ClearanceRelease clearanceRelease, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgRemoveReleaseProject);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result = clearanceProjectClient.RemoveReleaseProject(clearanceRelease, userInfo);
                clearanceProjectClient.Close();
                return result;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion



        #region GetCompaniesPushToR2
        /// Get Companies PushToR2
        /// <param name="userInfo"></param>
        /// <param name="Workgroups"></param>
        /// <param name="KeyValuePair"></param>
        /// </summary>
        /// 
        public List<CompanyInfo> GetCompaniesPushToR2(List<KeyValuePair<long, string>> Workgroups, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetCompaniesPushToR2);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                var projectSearchResult = clearanceProjectClient.GetCompaniesPushToR2(Workgroups, userInfo);
                clearanceProjectClient.Close();                
                return projectSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region GetDivisionPushToR2
        /// Get DivisionPush ToR2
        /// <param name="userInfo"></param>
        /// <param name="companyIds"></param>
        /// </summary>
        /// 
        public IClearanceProjectModel GetDivisionPushToR2(string companyIds, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetDivisionPushToR2);
                List<ClearanceRegularProject> _dropDownList = null;
                IClearanceProjectModel = new ClearanceProjectModel();
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                //if (_dropDownList == null)
                _dropDownList = clearanceProjectClient.GetDivisionPushToR2(companyIds, userInfo);
              
                IClearanceProjectModel.DivisionList = _dropDownList.Select(DivisionList => new SelectListItem { Text = DivisionList.divisionName.ToString(), Value = DivisionList.divisionId.ToString(CultureInfo.InvariantCulture) });
                clearanceProjectClient.Close();
                return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region GetLabelPushToR2
        /// Get LabelPush ToR2
        /// <param name="userInfo"></param>
        /// <param name="labelIds"></param>
        /// </summary>
        /// 
        public IClearanceProjectModel GetLabelPushToR2(string labelIds, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetLabelPushToR2);
                List<ClearanceRegularProject> _dropDownList = null;
                IClearanceProjectModel = new ClearanceProjectModel();
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                //if (_dropDownList == null)
                _dropDownList = clearanceProjectClient.GetLabelPushToR2(labelIds, userInfo);

                IClearanceProjectModel.LabelList = _dropDownList.Select(LabelList => new SelectListItem { Text = LabelList.divisionName.ToString(), Value = LabelList.divisionId.ToString(CultureInfo.InvariantCulture) });
                clearanceProjectClient.Close();
                return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }

            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region SaveR2ProjectIdLinking
        ///Save R2 ProjectId Linking
        /// <param name="userInfo"></param>
        /// <param name="regularProjectDetails"></param>
        /// </summary>
        /// 
        public bool SaveR2ProjectIdLinking(ClearanceRegularProject regularProjectDetails, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSaveR2ProjectIdLinking);
                bool bR2ProjectId = false;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bR2ProjectId = clearanceProjectClient.SaveR2ProjectIdLinking(regularProjectDetails, userInfo);
                clearanceProjectClient.Close();
                return bR2ProjectId;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region AutoCompleteSearch
        /// Auto complete search
        /// <param name="autoSearch,"></param>
        /// <param name="userLogin"></param>
        /// </summary>
        /// 
        public List<string> AutoCompleteSearch(SearchCriteria autoSearch, string userLogin)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgAutoCompleteSearch);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                List<string> resultList = new List<string>();
                resultList=clearanceProjectClient.AutoCompleteSearch(autoSearch, userLogin);
                clearanceProjectClient.Close();
                return resultList; 

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region SaveReleaseResourceToPushToR2
        /// Save ReleaseResourceToPush ToR2
        /// <param name="clrPushR2Item,"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public bool SaveReleaseResourceToPushToR2(List<ClearancePushR2Item> clrPushR2Item, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSaveReleaseResourceToPushToR2);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result=clearanceProjectClient.SaveReleaseResourceToPushToR2(clrPushR2Item, userInfo);
                clearanceProjectClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion



        #region GetRequestSummaryRequests
        /// <summary>
        /// Method used to get reqeust summary  data
        /// </summary>
        /// <param name="autoSearch"></param>
        /// <returns></returns>
        public List<ClearanceInboxRequest> GetRequestSummaryRequests(string clearanceProjectId, short GridType, int pageSize, int pageNo, string jtSorting, LeanUserInfo userInfo, out byte routingStatus)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetRequestSummaryRequests);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                List<ClearanceInboxRequest> clearanceInboxRequest = new List<ClearanceInboxRequest>();
                clearanceInboxRequest = clearanceProjectClient.GetRequestSummaryRequests(out routingStatus, clearanceProjectId, GridType, pageSize, pageNo, jtSorting, userInfo);
                clearanceProjectClient.Close();
                return clearanceInboxRequest;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }
        #endregion

        #region ClearanceRoutingAction
        /// Clearance Routing Action
        /// <param name="clrRoutedProject"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public bool ClearanceRoutingAction(ClearanceRoutedProject clrRoutedProject, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgClearanceRoutingAction);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result=clearanceProjectClient.ClearanceRoutingAction(clrRoutedProject, userInfo);
                clearanceProjectClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region GetRequestorCompany
        /// Fetch Requestor Company
        /// <param name="company_Id"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public CompanyInfo GetRequestorCompany(long company_Id, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug("");
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                CompanyInfo companyInfo = new CompanyInfo();
                companyInfo= clearanceProjectClient.GetRequestorCompany(company_Id, userInfo);
                clearanceProjectClient.Close();
                return companyInfo;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region GetRoutingDetails
        /// Get Routing Details
        /// <param name="routedItemID"></param>

        /// </summary>
        /// 
        public List<ClearanceRoutingDetails> GetRoutingDetails(long routedItemID)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgClearanceRoutingAction);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                List<ClearanceRoutingDetails> resultList = new List<ClearanceRoutingDetails>();
                resultList=clearanceProjectClient.GetRoutingDetails(routedItemID);
                clearanceProjectClient.Close();
                return resultList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }

            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }
        #endregion



        #region SearchProjectToAllocateUPC
        /// Search Project To AllocateUPC
        /// <param name="projectSearchCriteria"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public ProjectSearchResult SearchProjectToAllocateUPC(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchProject);
                var calcStartIndex = projectSearchCriteria.jtStartIndex;
                if (projectSearchCriteria.jtStartIndex >= ConfigurationFactory.PageSize)
                    calcStartIndex = projectSearchCriteria.jtStartIndex / ConfigurationFactory.PageSize * projectSearchCriteria.jtPageSize;
                projectSearchCriteria.jtStartIndex = calcStartIndex;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ProjectSearchResult projectSearchResult = new ProjectSearchResult();
                projectSearchResult = clearanceProjectClient.SearchProjectToAllocateUPC(projectSearchCriteria, userInfo);
                clearanceProjectClient.Close();
                return projectSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region SearchClearanceUnlockedProjects
        /// SearchClearanceUnlockedProjects
        /// <param name="projectSearchCriteria"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public ProjectSearchResult SearchClearanceUnlockedProjects(ClearanceProjectSearchCriteria projectSearchCriteria, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchProject);
                var calcStartIndex = projectSearchCriteria.jtStartIndex;
                if (projectSearchCriteria.jtStartIndex >= ConfigurationFactory.PageSize)
                    calcStartIndex = projectSearchCriteria.jtStartIndex / ConfigurationFactory.PageSize * projectSearchCriteria.jtPageSize;
                projectSearchCriteria.jtStartIndex = calcStartIndex;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                ProjectSearchResult projectSearchResult = new ProjectSearchResult();
                projectSearchResult=clearanceProjectClient.SearchClearanceUnlockedProjects(projectSearchCriteria, userInfo);
                clearanceProjectClient.Close();
                return projectSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region UnlockProject
        /// UnlockProject
        /// <param name="clrProjectId"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public bool UnlockProject(long clrProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchProject);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result=clearanceProjectClient.UpdateProjectLockingStatus(clrProjectId, userInfo);
                clearanceProjectClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region IsProjectUnlocked
        /// IsProjectUnlocked
        /// <param name="clrProjectId"></param>
        /// <param name="userInfo"></param>
        /// <param name="projectType"></param> 
        /// </summary>
        /// 
        public string IsProjectUnlocked(int clrProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchProject);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                string resultstring=clearanceProjectClient.IsProjectUnlocked(clrProjectId, userInfo);
                clearanceProjectClient.Close();
                return resultstring;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        public bool IsProjectUnlockedByAdmin(long clrProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Is Project Update by Locked User");
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                bool result=clearanceProjectClient.IsProjectUnlockedByAdmin(clrProjectId, userInfo);
                clearanceProjectClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        public bool GetAuthorizationsForR2(string windowsUserName, string taskName,
                                                     AnaTargetApplication targetApplication,
                                                     string hashCode)
        {
            bool data = false;
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                data = clearanceProjectClient.GetAuthorizationsForR2(windowsUserName, taskName, targetApplication, hashCode);
                clearanceProjectClient.Close();
                return data;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }


        public List<CompanyInfo> GetDivisions(string divisionIds, string taskType, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                List<CompanyInfo> getDivisions = new List<CompanyInfo>();
                getDivisions = clearanceProjectClient.GetDivisions(divisionIds, taskType, userInfo);
                clearanceProjectClient.Close();
                return getDivisions;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }


        public List<CompanyInfo> GetLabels(long companyId, long divisionId, string taskType, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                List<CompanyInfo> getLabels = clearanceProjectClient.GetLabels(companyId, divisionId, taskType, userInfo);
                clearanceProjectClient.Close();
                return getLabels;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }


        /// <summary>
        /// Get Release Config list
        /// <param name="userInfo"></param>
        /// <param name="ConfigGroupId"></param>
        /// </summary>
        /// 
        public List<HoldbackPeriod> GetRequestType(int ProjectTypeId)
        {

            List<HoldbackPeriod> _dropDownList = null;
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetReleaseConfigList);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                _dropDownList = clearanceProjectClient.GetRequestType(ProjectTypeId);
                clearanceProjectClient.Close();
                return _dropDownList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }


        public List<ListItem> GetRCCHandlerList(RoleType roleType, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                List<ListItem> getRCCHandlerList = new List<ListItem>();
                getRCCHandlerList=clearanceProjectClient.GetRCCHandlerList(roleType, userInfo);
                clearanceProjectClient.Close();
                return getRCCHandlerList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        #region ProjectSearch
        /// <summary>
        /// Projects the search.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public BusinessEntities.Entities.ProjectEntities.ProjectSearchResult ProjectSearch(ProjectSearchCriteria criteria)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgProjectSearch);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                var projectSearchResult = clearanceProjectClient.Search(criteria);
                clearanceProjectClient.Close();
                return projectSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion

        #region CreateProject
        /// CreateProject
        /// <param name="project"></param>

        /// </summary>
        /// 
        public Dictionary<long, string> CreateProject(R2Project project, long clrProjectId, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                var userInformation = new UserInfo
                {
                    UserId = userInfo.UserId,
                    UserLoginName = userInfo.UserLoginName,
                    UserName = userInfo.UserName,
                    EmailId = userInfo.EmailId
                };
                
                Dictionary<long, string> lstProjectData = new Dictionary<long, string>();
                _logFactory.LogWriter.Debug(_msgCreateProject);
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                lstProjectData = clearanceProjectClient.CreateProject(project, clrProjectId, userInformation);
                clearanceProjectClient.Close();
                return lstProjectData;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
        #endregion
        public IClearanceResourceModel GetMasterDataResource(List<string> inputMasterDataType, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetMasterData);
                IClearanceResourceModel = new ClearanceResourceModel();
                List<ClearanceMasterData> _dropDownList = null;
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                if (_dropDownList == null)
                    _dropDownList = clearanceProjectClient.GetMasterData(inputMasterDataType, userInfo);

                IClearanceResourceModel.ResourceType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceResourceType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                IClearanceResourceModel.RecordingType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceRecordingType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                IClearanceResourceModel.MusicType = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceMusicType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                IClearanceResourceModel.ResourceTypeFreehand = _dropDownList.Where(ClearanceMasterData => ClearanceMasterData.Type == Constants.ClearanceResourceType).Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) });
                clearanceProjectClient.Close();
                return IClearanceResourceModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }
        public List<ResourceDetail> SearchR2MockDataResource(ResourceSearch resourceSearch, LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchR2MockDataResource);
                List<ResourceDetail> clrResourceSearchResult = new List<ResourceDetail>();
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                // if (clrResourceSearchResult == null)
                clrResourceSearchResult = clearanceProjectClient.SearchR2Resource(resourceSearch, userInfo);

                clearanceProjectClient.Close();
                return clrResourceSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        public List<string> SearchProjectForEmail(string projectReference,LeanUserInfo userInfo)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            List<string> lstData = new List<string>();
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchR2MockDataResource);
                ClearanceProjectInquirySearchCriteria input = new ClearanceProjectInquirySearchCriteria();
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                lstData = clearanceProjectClient.SearchProjectForEmail(projectReference, userInfo);
                clearanceProjectClient.Close();
                return lstData;
                
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        public bool HasSpecialPermission(long userId, long workgroupId, bool isR2AuthorizedPermissionCheck)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            bool IsR2Authorized = false;
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                IsR2Authorized = clearanceProjectClient.HasSpecialPermission(userId, workgroupId, isR2AuthorizedPermissionCheck);
                clearanceProjectClient.Close();
                return IsR2Authorized;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }

        }

        public List<ClearanceAdminCompany> AutoCompleteCreateSearchProject(SearchCriteria autoSearch, long userId)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            List<ClearanceAdminCompany> lstCompanyData = new List<ClearanceAdminCompany>();
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                lstCompanyData = clearanceProjectClient.AutoCompleteCreateSearchProject(autoSearch, userId);
                clearanceProjectClient.Close();
                return lstCompanyData;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }

        public long GetTerritoryIdRequestorCompany(long RequestorCompanyId)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            long territoryId = 0;
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                territoryId = clearanceProjectClient.GetTerritoryIdRequestorCompany(RequestorCompanyId);
                clearanceProjectClient.Close();
                return territoryId;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }


        public List<ClearanceMasterData> AutoCompleteSearchLabels(SearchCriteria autoSearch, long userId)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            List<ClearanceMasterData> lstlabelData = new List<ClearanceMasterData>();
            try
            {
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                lstlabelData = clearanceProjectClient.AutoCompleteSearchLabels(autoSearch, userId);
                clearanceProjectClient.Close();
                return lstlabelData;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
        }
    }
}
