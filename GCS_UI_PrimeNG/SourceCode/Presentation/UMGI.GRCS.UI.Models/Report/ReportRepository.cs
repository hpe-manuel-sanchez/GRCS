/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ReportRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Richa Saini
 * Created on     : 10-1-2013 
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
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Interfaces;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Interfaces.Report;
using UMGI.GRCS.Utilities.logger;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.UI.Models.Report
{
    public class ReportRepository : IReportRepository
    {
        #region "Private Variables"

        private readonly IServiceFactory _serviceFactory;
        private readonly IReport _reportService;
        private readonly IGlobal _globalService;
        private ISessionWrapper _sessionWrapper { get; set; }
        private IConfigFactory _configurationFactory { get; set; }
        private ILogFactory _logFactory { get; set; }
        private UserInfo _userDetails { get; set; }
        private readonly IArtist _artistService;
        public readonly DateTime? defaultDateTime = null;
        #endregion

        #region "Report Repository Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractRepository"/> class.
        /// </summary>
        public ReportRepository(IServiceFactory serviceFactory, IConfigFactory configFactory, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            try
            {
                _sessionWrapper = sessionWrapper;
                _configurationFactory = configFactory;
                _serviceFactory = serviceFactory;
                _logFactory = logFactory;
                _userDetails = _sessionWrapper.CurrentUserInfo;
                _reportService = _serviceFactory.GetService<IReport>(Constants.ReportService);
                _globalService = _serviceFactory.GetService<IGlobal>(Constants.GlobalService);
                _artistService = _serviceFactory.GetService<IArtist>(Constants.ArtistService);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Get the contracts for ActiveRoster
        /// </summary>
        public List<IActiveRosterModel> GetActiveRosterDetails(ActiveRosterReportFilter filters)
        {
            try
            {
                var result = _reportService.GetActiveRosterDetails(filters);
                var contracts = (from m in result
                                 select new ActiveRosterModel
                                 {
                                     ContractId = Convert.ToInt64(m.ContractId),
                                     LocalReferenceNumber = m.LocalReferenceNumber,
                                     Artist = m.Artist,
                                     ContractingParties = m.ContractingParties,
                                     CLEARANCECO = m.CLEARANCECO,
                                     CommencementDate = m.CommencementDate != null ? Convert.ToDateTime(m.CommencementDate) : defaultDateTime,
                                     TerritorialRight = m.TerritorialRight,
                                     UMGSigningCompany = m.UMGSigningCompany,
                                     LinkedRepertoire = m.LinkedRepertoire,
                                     Total = m.Total
                                 }).ToList<IActiveRosterModel>();

                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the contracts for RightsExpiry
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<IRightsExpiryModel> GetRightsExpiryDetails(RightsExpiryReportFilters filters)
        {
            try
            {
                var result = _reportService.GetRightsExpiryDetails(filters);
                var contracts = (from m in result
                                 select new RightsExpiryModel
                                 {
                                     Artist = m.Artist,
                                     ContractingParties = m.ContractingParties,
                                     CLEARANCECO = m.CLEARANCECO,
                                     CommencementDate = m.CommencementDate != null ? Convert.ToDateTime(m.CommencementDate, CultureInfo.InvariantCulture) : defaultDateTime,
                                     LocalReferenceNumber = m.LocalReferenceNumber,
                                     RightsPeriod = m.RightsPeriod,
                                     RightsExpiryRule = m.RightsExpiryRule,
                                     LostRightsDate = m.LostRightsDate != null ? Convert.ToDateTime(m.LostRightsDate,CultureInfo.InvariantCulture) : defaultDateTime,
                                     TerritorialRight = m.TerritorialRight,
                                     WorkFlowStatus = m.WorkFlowStatus,
                                     LinkedRepertoire = m.LinkedRepertoire,
                                     IsLostRight = m.IsLostRight,
                                     Total = m.Total
                                 }).ToList<IRightsExpiryModel>();


                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the list of LostRightDate as Next n months
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetLostRightDateList()
        {
            try
            {
                var lostRight = new Dictionary<int, string>();
                lostRight.Add(0, "Select");
                for (int monthCount = 1; monthCount <= 18; monthCount++)
                {
                    lostRight.Add(monthCount, "Next " + monthCount.ToString() + " Month");
                }
                return lostRight.Select(
                       item =>
                       new SelectListItem { Text = item.Value, Value = item.Key.ToString(CultureInfo.InvariantCulture) });
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the contracts for RightsExpiryToBeDetermined
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<IRightsExpiryToBeDeterminedModel> GetRightsExpiryToBeDeterminedDetails(RightsExpiryToBeDeterminedReportFilter filters)
        {
            try
            {
                var result = _reportService.GetRightsExpiryToBeDeterminedDetails(filters);
                var contracts = (from m in result
                                 select new RightsExpiryToBeDeterminedModel
                                 {
                                     ContractId = m.ContractId,
                                     Artist = m.Artist,
                                     ContractingParties = m.ContractingParties,
                                     CLEARANCECO = m.CLEARANCECO,
                                     CommencementDate = m.CommencementDate != null ? Convert.ToDateTime(m.CommencementDate, CultureInfo.InvariantCulture) : defaultDateTime,
                                     LocalReferenceNumber = m.LocalReferenceNumber,
                                     RightsExpiryRule = m.RightsExpiryRule,
                                     TerritorialRight = m.TerritorialRight,
                                     WorkFlowStatus = m.WorkFlowStatus,
                                     LinkedRepertoire = m.LinkedRepertoire,
                                     Total = m.Total
                                 }).ToList<IRightsExpiryToBeDeterminedModel>();


                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the contracts for ReleaseCommitment
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<IReleaseCommitmentModel> GetReleaseCommitmentDetails(ReleaseCommitmentReportFilter filters)
        {
            try
            {
                var result = _reportService.GetReleaseCommitmentDetails(filters);
                var contracts = (from m in result
                                 select new ReleaseCommitmentModel
                                 {
                                     ContractId = m.ContractId,
                                     Artist = m.Artist ?? m.ContractingParties,
                                     CLEARANCECO = m.CLEARANCECO,
                                     CommencementDate = m.CommencementDate != null ? Convert.ToDateTime(m.CommencementDate, CultureInfo.InvariantCulture) : defaultDateTime,
                                     ContractDescription = m.ContractDescription,
                                     EndOfTermDate = m.EndOfTermDate != null ? Convert.ToDateTime(m.EndOfTermDate, CultureInfo.InvariantCulture) : defaultDateTime,
                                     RightsPeriod = m.RightsPeriod,
                                     RightsExpiryRule = m.RightsExpiryRule,
                                     LostRightsDate = m.LostRightsDate != null ? Convert.ToDateTime(m.LostRightsDate, CultureInfo.InvariantCulture) : defaultDateTime,
                                     TerritorialRight = m.TerritorialRight,
                                     ReleaseCommitmentAndRightsReversion = m.ReleaseCommitmentAndRightsReversion,
                                     NoOfLinkedReleases = Convert.ToInt32(m.NoOfLinkedReleases),
                                     NoOfLinkedResources = Convert.ToInt32(m.NoOfLinkedResources),
                                     Total = m.Total
                                 }).ToList<IReleaseCommitmentModel>();
                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the repertoire for Rights Aquired
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<IRightsAcquiredModel> GetRightsAcquiredDetails(RightsAcquiredReportFilter filters)
        {
            try
            {
                var result = _reportService.GetRightsAcquiredDetails(filters);
                var Repertoire = (from m in result
                                  select new RightsAcquiredModel
                                  {
                                      UPC = m.UPC,
                                      Configuration = m.Configuration,
                                      ISRC = m.ISRC,
                                      Title = m.Title,
                                      VersionTitle = m.VersionTitle,
                                      ResourceType = m.ResourceType,
                                      RightsType = m.RightsType,
                                      RepertoireType = m.RepertoireType,
                                      Artist = m.Artist,
                                      ClearanceAdminCompany = m.ClearanceAdminCompany,
                                      TerritorialRight = m.TerritorialRight,
                                      TerritorialRightToolTip = m.TerritorialRightToolTip,
                                      PhysicalExploitationrights = m.PhysicalExploitationrights,
                                      DigitalExploitationrights = m.DigitalExploitationrights,
                                      MobileExploitationrights = m.MobileExploitationrights,
                                      DigitalExploitationRestrictions = m.DigitalExploitationRestrictions,
                                      ContentType = m.ContentType,
                                      USEType = m.USEType,
                                      ComercialModel = m.ComercialModel,
                                      Notes = m.Notes,
                                      Total = m.Total
                                  }).ToList<IRightsAcquiredModel>();
                return Repertoire;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the repertoire for Secondary Exploitation Rights
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<ISecondaryExploitationRightsModel> GetSecondaryExploitationRightsDetails(SecondaryExploitationRightsReportFilter filters)
        {
            try
            {
                var result = _reportService.GetSecondaryExploitationRightsDetails(filters);
                var contracts = (from m in result
                                 select new SecondaryExploitationRightsModel
                                 {

                                     ResourceType = m.ResourceType,
                                     Title = m.Title,
                                     VersionTitle = m.VersionTitle,
                                     ISRC = m.ISRC,
                                     Artist = m.Artist,
                                     RightsType = m.RightsType,
                                     TerritorialRight = m.TerritorialRight,
                                     Total = m.Total,
                                     ExploitationType = m.ExploitationType,
                                     CAC = m.CAC,
                                     Restricted = m.Restricted.ToString(),
                                 }).ToList<ISecondaryExploitationRightsModel>();
                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the repertoire for Secondary Exploitation Sample
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<ISecondaryExploitationSampleModel> GetSecondaryExploitationSampleDetails(SecondaryExploitationSampleReportFilter filters)
        {
            try
            {
                var result = _reportService.GetSecondaryExploitationSampleDetails(filters);
                var contracts = (from m in result
                                 select new SecondaryExploitationSampleModel
                                 {
                                     ResourceType = m.ResourceType,
                                     Title = m.Title,
                                     Version_Title = m.Version_Title,
                                     ISRC = m.ISRC,
                                     Artist = m.Artist,
                                     ClearanceAdminCompany = m.ClearanceAdminCompany,
                                     ExploitationType = m.ExploitationType,
                                     Restricted = m.Restricted,
                                     IsSampleExists = m.IsSampleExists,
                                     IsSideArtistExists = m.IsSideArtistExists,
                                     RightsType = m.RightsType,
                                     Total = m.Total

                                 }).ToList<ISecondaryExploitationSampleModel>();
                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the repertoire for Pre-Cleared Resources
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<IPreClearedResourcesModel> GetPreClearedResourcesDetails(PreClearedResourceReportFilter filters)
        {
            try
            {
                var result = _reportService.GetPreClearedResourcesDetails(filters);
                var contracts = (from m in result
                                 select new PreClearedResourcesModel
                                 {
                                     ResourceType = m.ResourceType,
                                     Title = m.Title,
                                     Version_Title = m.Version_Title,
                                     ISRC = m.ISRC,
                                     Artist = m.Artist,
                                     ClearanceAdminCompany = m.ClearanceAdminCompany,
                                     PYear = m.PYear,
                                     ResourceGenre = m.ResourceGenre,
                                     PreClearanceType = m.PreClearanceType,
                                     TerritorialRight = m.TerritorialRight,
                                     RightsType = m.RightsType,
                                     Total = m.Total

                                 }).ToList<IPreClearedResourcesModel>();
                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the list of Artist strating with searchTerm
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public List<ArtistInfo> AutoSearchArtistName(string searchTerm)
        {
            try
            {
                _logFactory.LogWriter.Debug(searchTerm);

                return new List<ArtistInfo>(_reportService.GetArtists(new ArtistInfo { Name = searchTerm }).Where(a => a.Name.ToLower().Contains(searchTerm.ToLower())));
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get the list of WorkflowStatuses
        /// </summary>
        /// <returns></returns>
        public List<StringIdentifier> GetWorkflowStatuses()
        {
            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                return _reportService.GetWorkflowStatuses();
            }

            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetResourceType()
        {
            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                var resourceTabData = _reportService.GetResourceType();

                resourceTabData.Insert(0, new StringIdentifier { Description = Constants.SelectString, Id = 0 });
                var ResourceType = resourceTabData.Select(resource =>
                                                new SelectListItem
                                                {
                                                    Text = resource.Description,
                                                    Value = resource.Value.ToString()
                                                });
                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return ResourceType;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPreClearances()
        {
            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                var PreClearanceTabData = _reportService.GetPreClearances();

                PreClearanceTabData.Insert(0, new StringIdentifier { Description = Constants.ALL, Id = 0 });
                var PreClearanceList = PreClearanceTabData.Select(PreClearance =>
                                                new SelectListItem
                                                {
                                                    Text = PreClearance.Description,
                                                    Value = PreClearance.Value.ToString()
                                                });
                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return PreClearanceList;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Gets the PreClearancesTypes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetResourceGenre()
        {
            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                var resourcegenreData = _reportService.GetResourceGenre();

                resourcegenreData.Insert(0, new StringIdentifier { Description = Constants.SelectString, DescriptionTrim = "0" });
                var genreType = resourcegenreData.Select(resource =>
                                                new SelectListItem
                                                {
                                                    Text = resource.Description,
                                                    Value = resource.DescriptionTrim
                                                });
                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return genreType;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DigitalRestrictions getDigitalExpoliatation()
        {
            try
            {
                return _reportService.GetDefaultDataDigitalRestriction();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> CommercialModelTypeList()
        {
            try
            {
                var result = _reportService.GetDefaultDataDigitalRestriction();
                var CommercialModelTypeList = result.CommercialModelTypes.Select(CommercialModelType => new SelectListItem { Text = CommercialModelType.Description, Value = CommercialModelType.Id.ToString() });

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return CommercialModelTypeList;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> UseTypeList()
        {
            try
            {
                var result = _reportService.GetDefaultDataDigitalRestriction();
                result.UseTypes.Insert(0, new StringIdentifier { Id = 0, Description = Constants.ALL });
                var UseTypeList = result.UseTypes.Select(UseType => new SelectListItem { Text = UseType.Description, Value = UseType.Id.ToString() });

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return UseTypeList;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get Roll Up Status Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<IRollUpStatusModel> GetRollUpStatusDetails(RollUpStatusReportFilter filters)
        {
            try
            {
                var result = _reportService.GetRollUpStatusDetails(filters);
                var contracts = (from m in result
                                 select new RollUpStatusModel
                                 {
                                     Artist = m.Artist,
                                     ClearanceAdminCompany = m.ClearanceAdminCompany,
                                     UPC = m.UPC,
                                     DataAdminCompany = m.DataAdminCompany,
                                     LastRollUpDate = m.LastRollUpDate,
                                     MultiArtistCompilation = m.MultiArtistCompilation,
                                     ReleaseDate = m.ReleaseDate,
                                     RollUp_Status = m.RollUp_Status,
                                     Releaseissuer = m.Releaseissuer,
                                     Title = m.Title,
                                     VersionTitle = m.VersionTitle,
                                     Package = m.Package,
                                     TerritorialRight = m.TerritorialRight,
                                     Total = m.Total
                                 }).ToList<IRollUpStatusModel>();
                return contracts;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// Get Resource Rights Discrepancies Detail
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<IResourceRightsDiscrepanciesModel> GetResourceRightsDiscrepanciesDetail(ResourceRightsDiscrepanciesReportFilter filters)
        {
            var result = _reportService.GetResourceRightsDiscrepanciesDetail(filters);
            var contracts = (from m in result
                             select new ResourceRightsDiscrepanciesModel
                             {
                                 ClearanceAdminCompany = m.ClearanceAdminCompany,
                                 ContractPcCompany = m.ContractPcCompany,
                                 ContractArtist = m.ContractArtist,
                                 ContractId = m.ContractId,
                                 ContractingParty = m.ContractingParty,
                                 ContractPExtension = m.ContractPExtension,
                                 ContractRightsType = m.ContractRightsType,
                                 ResourceArtist = m.ResourceArtist,
                                 DataAdminCompany = m.DataAdminCompany,
                                 ResourcePcCompany = m.ResourcePcCompany,
                                 ResourcePExtension = m.ResourcePExtension,
                                 ResourceRightsType = m.ResourceRightsType,
                                 ResourceType = m.ResourceType,
                                 ISRC = m.ISRC,
                                 Title = m.Title,
                                 VersionTitle = m.VersionTitle,
                                 Total = m.Total
                             }).ToList<IResourceRightsDiscrepanciesModel>();
            return contracts;
        }

        /// <summary>
        /// Get Secondary Exploation Type
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSecondaryExploitationType()
        {
            try
            {
                var result = _reportService.GetSecondaryExploitationType();
                result.Insert(0, new StringIdentifier { Id = 0, Description = Constants.ALL });
                var contracts = result.Select(res => new SelectListItem { Text = res.Description, Value = res.Value.ToString() });//.OrderBy(x => x.Text);

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return contracts;


            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get compny details for reports
        /// </summary>
        /// <returns></returns>
        public List<ClearanceAdminCompany> GetClearanceAdminCompanies(GrsTasks tasks)
        {
            try
            {
                return _reportService.GetClearanceAdminCompanies(
                    new CompanySearch
                    {
                        UserName = _sessionWrapper.CurrentUserInfo.UserLoginName,
                        Tasks = tasks
                    });

            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

    }
}

