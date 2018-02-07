using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Proxies.ClearanceReleaseService;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Models
{
    public class ClearanceReleaseRepository : IClearanceReleaseRepository
    {
        #region "Private Variables"

        readonly ILogFactory _logFactory;
        
        #endregion


        #region "Public Variables"

        private IAddressBookModel AddressBookModel { get; set; }
        private IClearanceReleaseModel IClearanceReleaseModel { get; set; }
        private IClearanceProjectModel IClearanceProjectModel { get; set; }
        private string Isparent { get; set; }

        #endregion



        #region Private constant

        const string _msgR2ReleaseSearch = "R2 Release Search Method Initiated";
        const string _msgR2GetReleaseAdditionalDetails = "R2 Get Release Additional Detail Method Initiated";
        const string _msgGetReleaseDetails = "Get Release Details Method Initiated";
        const string _msgGetPackageDetails = "Get Package Details Method Initiated";
        const string _msgGetExistingReleases = "Get Existing Releases Method Initiated";
        const string _msgSaveExistingReleases = "Save Existing Releases Method Initiated";
        const string _msgUpdatePackage = "Update Package Method Initiated";
        const string _msgGetUPCNumber = "Get UPC Number Method Initiated";
        const string _msgRemoveUPCNumber = "Remove UPC Number Method Initiated";
        const string _msgSaveReleaseNew = "Save New Release Method Initiated";
        const string _msgGetUpc_Check_Digit = "Get UPC Check Digit Method Initiated";
        const string _msgGetPackageDetailsforProjectRelease = "Get Package Details For Project Release Method Initiated";
        const string _msgGetLabelName = "Get Label Name for Release";
        const string _msgReleaseIdNull = "Release Id is null";
        const string _msgGetReleaseLabelList = "Get ReleaseLabel List Method Initiated";
        const string _msgGetReleaseConfigList = "Get Release Config List Method Initiated";
        const string _msgGetReleaseConfigGroupList = "Get Release Config List Method Initiated";
        const string _msgGetExistingReleasesSaveMode = "Get Existing Releases Save Mode Method Initiated";
        const string _msgGetReleasesOnLoad = "Get Relases On Load Method Initiated";
        #endregion


        public ClearanceReleaseRepository(ILogFactory logFactory)
        {
            try
            {
                _logFactory = logFactory;
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
        //
        public ReleaseSearchResult R2ReleaseSearch(ReleaseSearchCriteria releaseSearchCriteria, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgR2ReleaseSearch);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                ReleaseSearchResult releaseSearchResult = new ReleaseSearchResult();
                releaseSearchResult=clearanceReleaseClient.GCSReleaseSearch(releaseSearchCriteria, userInfo);
                clearanceReleaseClient.Close();
                return releaseSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }

        }

        public ReleaseSearchResult GetReleaseDetailsBasedonR2ReleaseId(List<long> R2ReleaseId, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgR2ReleaseSearch);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                ReleaseSearchResult releaseSearchResult = new ReleaseSearchResult();
                releaseSearchResult = clearanceReleaseClient.GetReleaseDetailsBasedonR2ReleaseId(R2ReleaseId, userInfo);
                clearanceReleaseClient.Close();
                return releaseSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }

        }

        public List<TrackInfo> R2GetReleaseAdditionalDetails(long releaseId, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgR2GetReleaseAdditionalDetails);

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                List<TrackInfo> trackInfo = new List<TrackInfo>();
                trackInfo=clearanceReleaseClient.R2GetReleaseAdditionalDetails(releaseId, userInfo);
                clearanceReleaseClient.Close();
                return trackInfo;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        /// <summary>
        /// Get Release Details Based on Release Id
        /// </summary>
        /// <param name="releaseId"></param>
        /// <returns></returns>
        public IClearanceReleaseModel GetReleaseDetails(long releaseId, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                if (releaseId == 0)
                {
                    throw new Exception(_msgReleaseIdNull);
                }
                _logFactory.LogWriter.Debug(_msgGetReleaseDetails);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                IClearanceReleaseModel = new ClearanceReleaseModel();
                IClearanceReleaseModel.clearanceRelease = clearanceReleaseClient.GetReleaseDetailsGCS(releaseId, userInfo);
                clearanceReleaseClient.Close();
                return IClearanceReleaseModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        /// <summary>
        /// Get Package Details Based on Release Id
        /// </summary>
        /// <param name="releaseId"></param>
        /// <returns></returns>
        public IClearanceReleaseModel GetPackageDetails(long releaseId, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetPackageDetails);
                IClearanceReleaseModel = new ClearanceReleaseModel();
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                IClearanceReleaseModel.clearanceRelease = clearanceReleaseClient.GetReleaseAdditionalDetails(releaseId, userInfo);
                clearanceReleaseClient.Close();
                return IClearanceReleaseModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        /// <summary>
        /// Get Package Details Based on Release Id
        /// </summary>
        /// <param name="releaseId"></param>
        /// <returns></returns>
        public IClearanceReleaseModel UpdatePackage(List<PackageInfo> packageInfo, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgUpdatePackage);
                IClearanceReleaseModel = new ClearanceReleaseModel();
                IClearanceReleaseModel.clearanceRelease = new ClearanceRelease();
                IClearanceReleaseModel.clearanceRelease.PackageIds = new List<long>();
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                IClearanceReleaseModel.clearanceRelease.PackageIds = clearanceReleaseClient.UpdatePackage(packageInfo, userInfo);
                clearanceReleaseClient.Close();
                return IClearanceReleaseModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        public string GetUPCNumber(List<long> classiceReleaseId, List<long> nonClassiceReleaseId, string userId)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetUPCNumber);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                string result = clearanceReleaseClient.GetUPCNumber(classiceReleaseId, nonClassiceReleaseId, userId);
                clearanceReleaseClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        public bool RemoveUPCNumber(long projectReleaseId, string userId)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgRemoveUPCNumber);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                bool result=clearanceReleaseClient.RemoveUPCNumber(projectReleaseId, userId);
                clearanceReleaseClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        public string GetUpc_Check_Digit(string upcNumber, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetUpc_Check_Digit);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                string result=clearanceReleaseClient.GetUpc_Check_Digit(upcNumber, userInfo);
                clearanceReleaseClient.Close();
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        /// <summary>
        /// Checks if the R2 Release Id exists in GCS Database
        /// </summary>
        /// <param name="r2ReleaseId"></param>
        /// <returns>List of existing R2 Release Id</returns>
        public List<long?> GetExistingR2ReleaseIdFromGcs(List<long?> r2ReleaseId)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                r2ReleaseId = clearanceReleaseClient.GetExistingR2ReleaseIdFromGcs(r2ReleaseId);
                clearanceReleaseClient.Close();
                return r2ReleaseId;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        public List<long> GetExistingR2ReleaseIdForPackage(List<long> r2ReleaseId, long? ParentReleaseId)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                r2ReleaseId = clearanceReleaseClient.GetExistingR2ReleaseIdForPackage(r2ReleaseId, ParentReleaseId);
                clearanceReleaseClient.Close();
                return r2ReleaseId;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        public List<PackageInfo> GetPackageDetailsforProjectRelease(long releaseId, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetPackageDetailsforProjectRelease);

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                List<PackageInfo> packageInfo = new List<PackageInfo>();
                packageInfo=clearanceReleaseClient.GetPackageDetailsforProjectRelease(releaseId, userInfo);
                clearanceReleaseClient.Close();
                return packageInfo;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        public string getLabelNmForExistingRelease(int LabelId, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetLabelName);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                string result = clearanceReleaseClient.getLabelNmForExistingRelease(LabelId, userInfo);
                clearanceReleaseClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }

        public string UpdateManualUpc(long releaseId, string upcNumber, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetUpc_Check_Digit);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                string result = clearanceReleaseClient.UpdateManualUpc(releaseId, upcNumber, userInfo.UserLoginName);
                clearanceReleaseClient.Close();
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
                throw;
            }
            finally
            {
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }



        #region "Fill release new tab dropdown list"

        /// <summary>
        /// Get ReleaseLabel List
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public List<ClearanceRelease> GetReleaseLabelList(LeanUserInfo userInfo)
        {

            List<ClearanceRelease> _dropDownList = null;
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetReleaseLabelList);
                //if (_dropDownList == null)

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                _dropDownList = clearanceReleaseClient.GetReleaseLabelList(userInfo);
                clearanceReleaseClient.Close();
                return _dropDownList;
                //IClearanceProjectModel.LabelList = _dropDownList.Select(labelList => new SelectListItem { Text = labelList.labelName.ToString(), Value = labelList.LabelId.ToString(CultureInfo.InvariantCulture) });

                //return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }


        }

        /// <summary>
        /// Get Release Config list
        /// <param name="userInfo"></param>
        /// <param name="ConfigGroupId"></param>
        /// </summary>
        /// 
        public IClearanceProjectModel GetReleaseConfigList(string ConfigGroupId, LeanUserInfo userInfo)
        {

            List<ClearanceRelease> _dropDownList = new List<ClearanceRelease>();
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetReleaseConfigList);

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                if (!string.IsNullOrEmpty(ConfigGroupId))
                {
                    _dropDownList = clearanceReleaseClient.GetReleaseConfigList(ConfigGroupId, userInfo);
                }
                IClearanceProjectModel = new ClearanceProjectModel();
                IClearanceProjectModel.ConfigList = _dropDownList.Select(ConfigList => new ListItem { Text = ConfigList.Configuration_Desc, Value = ConfigList.ConfigId.ToString(CultureInfo.InvariantCulture) });
                clearanceReleaseClient.Close();
                return IClearanceProjectModel;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }

        }

        /// <summary>
        /// Get Release Config Group List
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public IClearanceProjectModel GetReleaseConfigGroupList(LeanUserInfo userInfo)
        {

            List<ClearanceRelease> _dropDownList = null;
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetReleaseConfigGroupList);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                //if (_dropDownList == null)
                _dropDownList = clearanceReleaseClient.GetReleaseConfigGroupList(userInfo);
                IClearanceProjectModel = new ClearanceProjectModel();
                IClearanceProjectModel.ConfigGroupList = _dropDownList.Select(ConfigGrpList => new SelectListItem { Text = ConfigGrpList.ConfigurationGroup_Desc, Value = ConfigGrpList.ConfigurationGroup_Id.ToString(CultureInfo.InvariantCulture) });
                clearanceReleaseClient.Close();
                return IClearanceProjectModel;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }

        }
        #endregion
        #region "GetExistingReleases"

        /// <summary>
        /// // It will call R2 for Release search
        /// <param name="UPC"></param>
        /// <param name="ReleaseTitle"></param>
        /// <param name="ArtistID"></param>
        /// <param name="userInfo"></param>
        /// <param name="ArtistName"></param>
        /// </summary>
        /// 
        public ClearanceReleaseSearchResult GetExistingReleases(string UPC, string ReleaseTitle, string ArtistName, int ArtistID, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetExistingReleases);
                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                ClearanceReleaseSearchResult clearanceSearchResult = new ClearanceReleaseSearchResult();
                clearanceSearchResult = clearanceReleaseClient.GetExistingReleases(UPC, ReleaseTitle, ArtistName, ArtistID, userInfo);
                clearanceReleaseClient.Close();
                return clearanceSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }
        #endregion

        #region "GetExistingReleasesSaveMode"
        /// <summary>
        /// Get Existing Release save mode
        /// <param name="projectCode"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public ClearanceReleaseSearchResult GetExistingReleasesSaveMode(string projectCode, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetExistingReleasesSaveMode);

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                ClearanceReleaseSearchResult clearanceReleaseSearchResult = new ClearanceReleaseSearchResult();
                clearanceReleaseSearchResult = clearanceReleaseClient.GetExistingReleasesSaveMode(projectCode, userInfo);
                clearanceReleaseClient.Close();
                return clearanceReleaseSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }
        #endregion

        #region GetReleasesOnLoad
        /// Get the existing release
        /// <param name="UPC"></param>
        /// <param name="ReleaseTitle"></param>
        /// <param name="ArtistName"></param>
        /// <param name="ArtistID"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        // It will fetch release from Rights db + other information from clearance db tables
        public ClearanceReleaseSearchResult GetReleasesOnLoad(string UPC, string ReleaseTitle, string ArtistName, int ArtistID, int startIndex, int pageSize, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetReleasesOnLoad);

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                ClearanceReleaseSearchResult clearanceReleaseSearchResult = new ClearanceReleaseSearchResult();
                clearanceReleaseSearchResult = clearanceReleaseClient.GetExistingReleases(UPC, ReleaseTitle, ArtistName, ArtistID, userInfo);
                clearanceReleaseClient.Close();
                return clearanceReleaseSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }
        #endregion

        #region SaveExistingReleases
        /// Save Existing Releases
        /// <param name="releaseSearchResult"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public string SaveExistingReleases(ClearanceReleaseSearchResult releaseSearchResult, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSaveExistingReleases);

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                string result = clearanceReleaseClient.SaveExistingReleases(releaseSearchResult, userInfo);
                clearanceReleaseClient.Close();
                return result;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }
        #endregion
        #region SaveReleaseNew
        /// <summary>
        /// Save new release tab data
        /// </summary>
        /// <param name="clearanceRelease"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string SaveReleaseNew(List<ClearanceRelease> clearanceRelease, LeanUserInfo userInfo)
        {
            ClearanceReleaseClient clearanceReleaseClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSaveReleaseNew);

                clearanceReleaseClient = new ClearanceReleaseClient();
                clearanceReleaseClient.Open();
                string result = clearanceReleaseClient.SaveReleaseNew(clearanceRelease, userInfo);
                clearanceReleaseClient.Close();
                return result;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
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
                if (clearanceReleaseClient != null && clearanceReleaseClient.State == CommunicationState.Faulted)
                {
                    clearanceReleaseClient.Abort();
                    clearanceReleaseClient = null;
                }
            }
        }
        #endregion

    }
}
