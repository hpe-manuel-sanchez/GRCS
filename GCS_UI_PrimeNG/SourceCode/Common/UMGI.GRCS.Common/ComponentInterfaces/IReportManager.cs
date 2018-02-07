using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Responses;


namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IReportManager
    {
        /// <summary>
        /// Get Active Roster contract Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<ActiveRoster> GetActiveRosterDetails(ActiveRosterReportFilter filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<RightsExpiry> GetRightsExpiryDetails(RightsExpiryReportFilters filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<RightsExpiryToBeDetermined> GetRightsExpiryToBeDeterminedDetails(RightsExpiryToBeDeterminedReportFilter filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<ReleaseCommitment> GetReleaseCommitmentDetails(ReleaseCommitmentReportFilter filters);

        /// <summary>
        /// Get list Work flow Statuses
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetWorkflowStatuses();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchArtist"></param>
        /// <returns></returns>
        List<ArtistInfo> GetArtists(ArtistInfo searchArtist);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<RightsAcquired> GetRightsAcquiredDetails(RightsAcquiredReportFilter filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<SecondaryExploitationRights> GetSecondaryExploitationRightsDetails(SecondaryExploitationRightsReportFilter filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<SecondaryExploitationSample> GetSecondaryExploitationSampleDetails(SecondaryExploitationSampleReportFilter filters);

        /// <summary>
        /// Get Pre Cleared Resources Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<PreClearedResources> GetPreClearedResourcesDetails(PreClearedResourceReportFilter filters);

        ///// <summary>
        ///// Gets the user role based company ids.
        ///// </summary>
        ///// <param name="userName">Name of the user.</param>
        ///// <returns></returns>
        //List<long> GetUserCompanyIds(string userName);

        /// <summary>
        /// Gets the default data digital restriction.
        /// </summary>
        /// <returns></returns>
        DigitalRestrictions GetDefaultDataDigitalRestriction();

        /// <summary>
        /// Method to get all work flow status types
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetResourceType();

        /// <summary>
        /// Method to get all PreClearances types
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetPreClearances();

        /// <summary>
        /// Get Genre type
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetResourceGenre();

         /// <summary>
        /// Get Secondary Exploation Type
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetSecondaryExploitationType();

        /// <summary>
        /// Get Roll Up Status Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<RollUpStatus> GetRollUpStatusDetails(RollUpStatusReportFilter filters);

        /// <summary>
        /// Get Resource Rights Discrepancies Detail
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<ResourceRightsDiscrepancies> GetResourceRightsDiscrepanciesDetail(ResourceRightsDiscrepanciesReportFilter filters);
        
        /// <summary>
        /// method for scheduled report generation
        /// </summary>
        /// <param name="Type"></param>
        void GenerateReport(ReportParameters parameter);

        /// <summary>
        /// Get compny details for reports
        /// </summary>
        /// <returns></returns>
        List<ClearanceAdminCompany> GetClearanceAdminCompanies(CompanySearch searchCriteria);
    }
}
