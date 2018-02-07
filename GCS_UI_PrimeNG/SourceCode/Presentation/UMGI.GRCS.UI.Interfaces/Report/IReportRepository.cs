/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IReportRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 1/3/2013
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
using System.Linq;
using System.Text;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Responses;
namespace UMGI.GRCS.UI.Interfaces.Report
{
    public interface IReportRepository
    {
        /// <summary>
        /// Get the contracts for ActiveRoster
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<IActiveRosterModel> GetActiveRosterDetails(ActiveRosterReportFilter filters);
        /// <summary>
        /// Get the contracts for RightsExpiry
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<IRightsExpiryModel> GetRightsExpiryDetails(RightsExpiryReportFilters filters);
        /// <summary>
        /// Get the LostRightDate
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetLostRightDateList();
        /// <summary>
        /// Get the contracts for RightsExpiryToBeDetermined
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<IRightsExpiryToBeDeterminedModel> GetRightsExpiryToBeDeterminedDetails(RightsExpiryToBeDeterminedReportFilter filters);
        /// <summary>
        /// Get the contracts for ReleaseCommitment
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<IReleaseCommitmentModel> GetReleaseCommitmentDetails(ReleaseCommitmentReportFilter filters);
        /// <summary>
        /// Get the list of Artist strating with searchTerm
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        List<ArtistInfo> AutoSearchArtistName(string searchTerm);
        /// <summary>
        /// Get the list of WorkflowStatuses
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetWorkflowStatuses();

        /// <summary>
        /// Get list of Secondary Exploitation Sample 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<ISecondaryExploitationSampleModel> GetSecondaryExploitationSampleDetails(SecondaryExploitationSampleReportFilter filters);

        /// <summary>
        ///  Get list of Rights Aquired
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<IRightsAcquiredModel> GetRightsAcquiredDetails(RightsAcquiredReportFilter filters);

      
        /// <summary>
        ///  Get list of PreCleared Resources
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<IPreClearedResourcesModel> GetPreClearedResourcesDetails(PreClearedResourceReportFilter filters);

        /// <summary>
        /// Get Resource Type List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetResourceType();

        /// <summary>
        /// Get Resource Type List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetPreClearances();

        DigitalRestrictions getDigitalExpoliatation();
        /// <summary>
        /// Get the Content Type list
        /// </summary>
        /// <returns></returns>
       // IEnumerable<SelectListItem> ContentTypeList();

        /// <summary>
        /// Get the Commercial model list
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> CommercialModelTypeList();

        /// <summary>
        /// Get the Use Type list
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> UseTypeList();

        /// <summary>
        /// Get Resource Genre List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetResourceGenre();

        /// <summary>
        /// Get Roll Up Status Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns
        List<IRollUpStatusModel> GetRollUpStatusDetails(RollUpStatusReportFilter filters);

        /// <summary>
        /// Get Resource Rights Discrepancies Detail
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<IResourceRightsDiscrepanciesModel> GetResourceRightsDiscrepanciesDetail(ResourceRightsDiscrepanciesReportFilter filters);

        /// <summary>
        /// Get Resource Rights Discrepancies Detail
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSecondaryExploitationType();
       
        /// <summary>
        ///  Get list of Secondary Exploitation Rights
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>     
        List<ISecondaryExploitationRightsModel> GetSecondaryExploitationRightsDetails(SecondaryExploitationRightsReportFilter filters);

        /// <summary>
        /// get the company detail for user
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        List<ClearanceAdminCompany> GetClearanceAdminCompanies(GrsTasks tasks);
    }
}
