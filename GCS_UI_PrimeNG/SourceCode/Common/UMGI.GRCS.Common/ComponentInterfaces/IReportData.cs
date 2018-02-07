/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IReporData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Richa
 * Created on   : 22-Jan-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IReportData
    {
        /// <summary>
        /// get data for active roster report
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<ActiveRoster> GetActiveRosterDetails(ActiveRosterReportFilter filters);

        /// <summary>
        ///  get data for Rights Expiry
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<RightsExpiry> GetRightsExpiryDetails(RightsExpiryReportFilters    filters);

        /// <summary>
        ///  get data for Rights Expiry To Be Determined
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<RightsExpiryToBeDetermined> GetRightsExpiryToBeDeterminedDetails(RightsExpiryToBeDeterminedReportFilter filters);

        /// <summary>
        ///  get data for Release Commitment
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<ReleaseCommitment> GetReleaseCommitmentDetails(ReleaseCommitmentReportFilter filters);

        /// <summary>
        ///  get data for Get Artists
        /// </summary>
        /// <param name="searchArtist"></param>
        /// <returns></returns>
        List<ArtistInfo> GetArtists(ArtistInfo searchArtist);

        /// <summary>
        /// Method to Get RightsAcquired Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<RightsAcquired> GetRightsAcquiredDetails(RightsAcquiredReportFilter filters);

        /// <summary>
        /// Method to Get SecondaryExploitationRights Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<SecondaryExploitationRights> GetSecondaryExploitationRightsDetails(SecondaryExploitationRightsReportFilter filters);

        /// <summary>
        /// Method to Get SecondaryExploitationSample Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<SecondaryExploitationSample> GetSecondaryExploitationSampleDetails(SecondaryExploitationSampleReportFilter filters);

        /// <summary>
        /// Method to Get ClearedResources Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<PreClearedResources> GetPreClearedResourcesDetails(PreClearedResourceReportFilter filters);

        /// <summary>
        /// Method to get Lookup data by lookup type code
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        List<StringIdentifier> GetLookupData(string typeCode);

        /// <summary>
        /// Get Genre type
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetResourceGenre();

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
        /// 
        /// </summary>
        /// <param name="emailGroupDetails"></param>
        /// <returns></returns>
        string GetEmailGroupInfo(string emailGroupDetails);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateReportConfig(string reportName, int status);
    }
}
