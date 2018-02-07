/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : GlobalRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 27-10-2012 
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
using System.Data;
using System.Reflection;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Proxies.GlobalService;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Models
{
    public partial class GlobalRepository : IGlobalRepository
    {
         #region "Private Variables"
        
        private readonly ISessionWrapper _sessionWrapper;
        private readonly ILogFactory _logFactory;
        //private readonly IGlobal _globalService;
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalRepository"/> class.
        /// </summary>
        /// <param name="sessionWrapper">The session wrapper.</param>
        /// <param name="logFactory">The log factory.</param>
        public GlobalRepository(ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            try
            {
                _sessionWrapper = sessionWrapper;
                _logFactory = logFactory;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Clearance admin company autosearch
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public List<ClearanceAdminCompany> AutoSearchClearanceCompCountry(string searchTerm)
        {
            GlobalClient globalService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("SearchTerm:{0}",searchTerm));
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.GetCompaniesForUser(new CompanySearch
                                                              {
                                                                  IsCountryRequired = true,
                                                                  IsFilterRequired = true,
                                                                  SearchTerm = searchTerm,
                                                                  UserName = _sessionWrapper.CurrentUserInfo.UserLoginName,
                                                                  HasPageDetails = false
                                                              });
                globalService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI,ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }
        /// <summary>
        /// Clearance admin company autosearch according to user task
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public List<ClearanceAdminCompany> AutoSearchClearanceCompCountry(string searchTerm, GrsTasks tasks)
        {
            GlobalClient globalService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("SearchTerm:{0}", searchTerm));
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.GetCompaniesForUser(new CompanySearch
                {
                    IsCountryRequired = true,
                    IsFilterRequired = true,
                    SearchTerm = searchTerm,
                    UserName = _sessionWrapper.CurrentUserInfo.UserLoginName,
                    Tasks = tasks,
                    HasPageDetails = false
                });
                globalService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        /// <summary>
        /// Get the collection of territories/countries
        /// </summary>
        public List<TerritorialDisplay> GetTerritories()
        {
            GlobalClient globalService = null;

            try
            {
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.GetTerritories();
                globalService.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }
    }

    /// <summary>
    /// Audit Trail
    /// </summary>
    public partial class GlobalRepository
    {
        /// <summary>
        /// GetAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="type"> </param>
        /// <returns></returns>
        public List<AuditTrailFilter> GetAuditTrailFilters(AuditObjectType auditObjectType, int type)
        {
            try
            {
                GlobalClient globalService = null;
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.GetAuditTrailFilters(auditObjectType, type);
                globalService.Close();
                return returnValue;
              //  return _globalService.GetAuditTrailFilters(auditObjectType);

            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }

        /// <summary>
        /// GetAuditTrail
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="selectedAuditConfiguration"></param>
        /// <param name="selectedItemId"></param>
        /// <param name="cdlType"> </param>
        /// <returns></returns>
        public DataSet GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId, int cdlType)
        {
            try
            {
                GlobalClient globalService = null;
                globalService = new GlobalClient();
                globalService.Open();
                var auditTrailFilters = globalService.GetAuditTrail(auditObjectType, selectedAuditConfiguration, selectedItemId, cdlType);
                globalService.Close();
                return auditTrailFilters;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }

        /// <summary>
        /// Clearance admin company autosearch for Reportier report Permission, companies will be searched in GRS database
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public List<ClearanceAdminCompany> AutoSearchClearanceAdminCompany(string searchTerm)
        {
            GlobalClient globalService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("SearchTerm:{0}", searchTerm));
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.GetCompaniesForUser(new CompanySearch
                {
                    IsCountryRequired = false,
                    IsFilterRequired = false,
                    SearchTerm = searchTerm,
                    UserName = _sessionWrapper.CurrentUserInfo.UserLoginName,
                    HasPageDetails = false
                });
                globalService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }
    }

    /// <summary>
    /// Audit Trail and company search for GCS
    /// </summary>
    public partial class GlobalRepository
    {
        /// <summary>
        /// Search Companies with the given parameters
        /// </summary>
        /// <returns>Collection of company info</returns>
        public List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string jtSorting, string userLoginName)
        {
            GlobalClient globalClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Company Search Method Initiated");
                globalClient = new GlobalClient();
                globalClient.Open();
                var returnValue = globalClient.CompanySearch(companyName, isacCode, country, startIndex, pageSize, jtSorting, userLoginName);
                globalClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "CompanySearch"), ex);
                globalClient.Abort();
                globalClient = null;
                throw;
            }
            finally
            {
                if (globalClient != null && globalClient.State == CommunicationState.Opened)
                {
                    globalClient.Close();
                    globalClient = null;
                }
            }
        }

        /// <summary>
        /// GetAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <returns></returns>
        public List<AuditTrailFilter> GetAuditTrailFilters(AuditObjectType auditObjectType)
        {
            GlobalClient globalService = null;
            try
            {
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.GetGCSAuditTrailFilters(auditObjectType);
                globalService.Close();
                return returnValue;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }

        /// <summary>
        /// GetWGAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <returns></returns>
        public List<AuditTrailFilter> GetWGAuditTrailFilters(AuditObjectType auditObjectType, string selectedWorkgroupRole, bool isParent)
        {
            GlobalClient globalService = null;
            try
            {
                globalService = new GlobalClient();
                globalService.Open();
                var auditTrailFilters = globalService.GetGCSAuditTrailFilters(auditObjectType);
                globalService.Close();
                var deviationSection = auditTrailFilters.Find(a => a.DisplayName.ToUpper() == Constants.WGDeviations);
                if (isParent && deviationSection != null)
                {
                    auditTrailFilters.RemoveAll(a => a.AuditConfigId == deviationSection.AuditConfigId || a.ParentAuditConfigId == deviationSection.AuditConfigId);
                }
                else
                {
                    auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayChildWG || a.DisplayName.ToUpper() == Constants.DisplayTerritoryCountry);
                }
                if ((selectedWorkgroupRole.ToUpper() == Constants.Requestor_Role) || (selectedWorkgroupRole.ToUpper() == Constants.InquiryRole) || (selectedWorkgroupRole.ToUpper() == Constants.RCCAdmin_Role))
                {
                    auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayChildWG || a.DisplayName.ToUpper() == Constants.DisplayDefaultUsers || a.DisplayName.ToUpper() == Constants.DisplayReviewerRights);
                    if (selectedWorkgroupRole.ToUpper() == Constants.RCCAdmin_Role)
                    {
                        auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayManageWG || a.DisplayName.ToUpper() == Constants.DisplayInquiryRights || a.DisplayName.ToUpper() == Constants.DisplayRequestorRights || a.DisplayName.ToUpper() == Constants.DisplayR2Authorized || a.DisplayName.ToUpper() == Constants.DisplayUPCAllocation || a.DisplayName.ToUpper() == Constants.DisplayTerritoryCountry || a.DisplayName.ToUpper() == Constants.DisplayCompany);
                    }
                    if (selectedWorkgroupRole.ToUpper() == Constants.Requestor_Role)
                    {
                        auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayInquiryRights || a.DisplayName.ToUpper() == Constants.DisplayTerritoryCountry || a.DisplayName.ToUpper() == Constants.DisplayRCCRights);
                    }
                    if (selectedWorkgroupRole.ToUpper() == Constants.InquiryRole)
                    {
                        auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayRequestorRights || a.DisplayName.ToUpper() == Constants.DisplayR2Authorized || a.DisplayName.ToUpper() == Constants.DisplayUPCAllocation || a.DisplayName.ToUpper() == Constants.DisplayRCCRights);
                    }
                }
                else
                {
                    auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayRequestorRights || a.DisplayName.ToUpper() == Constants.DisplayInquiryRights || a.DisplayName.ToUpper() == Constants.DisplayR2Authorized || a.DisplayName.ToUpper() == Constants.DisplayUPCAllocation || a.DisplayName.ToUpper() == Constants.DisplayRCCRights);
                    if ((selectedWorkgroupRole.ToUpper() == Constants.GlobalClearanceRole) || (selectedWorkgroupRole.ToUpper() == Constants.MarketingReviewerRole))
                    {
                        auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayCompany);
                    }
                    if (selectedWorkgroupRole.ToUpper() != Constants.LocalReviewerRole)
                    {
                        auditTrailFilters.RemoveAll(a => a.DisplayName.ToUpper() == Constants.DisplayTerritoryCountry);
                    }
                }

                return auditTrailFilters;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }
        /// <summary>
        /// GetAuditTrail
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="selectedAuditConfiguration"></param>
        /// <param name="selectedItemId"></param>
        /// <returns></returns>
        public DataSet GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId)
        {
            GlobalClient globalService = null;
            try
            {
                globalService = new GlobalClient();
                globalService.Open();
                var auditTrailFilters = globalService.GetGCSAuditTrail(auditObjectType, selectedAuditConfiguration, selectedItemId);
                globalService.Close();
                return auditTrailFilters;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }
    }
}
