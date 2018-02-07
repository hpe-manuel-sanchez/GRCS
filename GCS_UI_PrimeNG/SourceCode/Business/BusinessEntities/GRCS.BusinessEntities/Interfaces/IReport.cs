/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : IReport.cs
 * Project Code : UMG-GRCS(C/115921)  
 * Author       : Richa
 * Created on   : 23/jan/20123
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Interface methods for Report Service
                  
****************************************************************************/

using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IReport
    { /// <summary>
        /// To fetch the active roster result.
        /// </summary>
        /// <param name="clearanceCompany">The clearance admin Company.</param>
        [OperationContract]
        List<ActiveRoster> GetActiveRosterDetails(ActiveRosterReportFilter filters);
        /// <summary>
        /// To fetch Rights Expiry Report Result
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [OperationContract]
        List<RightsExpiry> GetRightsExpiryDetails(RightsExpiryReportFilters filters);
        /// <summary>
        /// To fetch Rights Expiry To Be Determined Report Result
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [OperationContract]
        List<RightsExpiryToBeDetermined> GetRightsExpiryToBeDeterminedDetails(RightsExpiryToBeDeterminedReportFilter filters);
        /// <summary>
        /// To fetch Release Commitmen Reportt Result
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [OperationContract]
        List<ReleaseCommitment> GetReleaseCommitmentDetails(ReleaseCommitmentReportFilter filters);

        /// <summary>
        /// To fetch work flow status
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
         [OperationContract]
        List<StringIdentifier> GetWorkflowStatuses();
        /// <summary>
        /// Get the list of artist in GRS system with matching starting name
        /// </summary>
        /// <param name="searchArtist"></param>
        /// <returns></returns>
        [OperationContract]
        List<ArtistInfo> GetArtists(ArtistInfo searchArtist);

        /// <summary>
        /// Method to Get RightsAcquired Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [OperationContract]
        List<RightsAcquired> GetRightsAcquiredDetails(RightsAcquiredReportFilter filters);

        /// <summary>
        /// Method to Get SecondaryExploitationRights Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [OperationContract]
        List<SecondaryExploitationRights> GetSecondaryExploitationRightsDetails(SecondaryExploitationRightsReportFilter filters);

        /// <summary>
        /// Method to Get SecondaryExploitationSample Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [OperationContract]
        List<SecondaryExploitationSample> GetSecondaryExploitationSampleDetails(SecondaryExploitationSampleReportFilter filters);

        /// <summary>
        /// Method to Get ClearedResources Details
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [OperationContract]
        List<PreClearedResources> GetPreClearedResourcesDetails(PreClearedResourceReportFilter filters);

        /// <summary>
        /// Gets the default data digital restriction.
        /// </summary>
        /// <returns>DigitalRestrictions</returns>
        [OperationContract]
        DigitalRestrictions GetDefaultDataDigitalRestriction();

        /// <summary>
        /// Method to get all work flow status types
        /// </summary>
        /// <returns></returns>
         [OperationContract]
        List<StringIdentifier> GetResourceType();

        /// <summary>
        /// Method to get all PreClearances types
        /// </summary>
        /// <returns></returns>
         [OperationContract]
        List<StringIdentifier> GetPreClearances();

        /// <summary>
        /// Get Genre type
        /// </summary>
        /// <returns></returns>
         [OperationContract]
        List<StringIdentifier> GetResourceGenre();
         /// <summary>
         /// Get Secondary Exploation type
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         List<StringIdentifier> GetSecondaryExploitationType();

         /// <summary>
         /// Get Roll Up Status Details
         /// </summary>
         /// <param name="filters"></param>
         /// <returns></returns>
         [OperationContract]
         List<RollUpStatus> GetRollUpStatusDetails(RollUpStatusReportFilter filters);

         /// <summary>
         /// Get Resource Rights Discrepancies Detail
         /// </summary>
         /// <param name="filters"></param>
         /// <returns></returns>
         [OperationContract]
         List<ResourceRightsDiscrepancies> GetResourceRightsDiscrepanciesDetail(ResourceRightsDiscrepanciesReportFilter filters);
         
        /// <summary>
         /// Get compny details for reports
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         List<ClearanceAdminCompany> GetClearanceAdminCompanies(CompanySearch searchCriteria);
    }
}
