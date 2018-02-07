/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IReleaseCommitmentModel.cs
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
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
namespace UMGI.GRCS.UI.Interfaces.Report
{
    public interface IReleaseCommitmentModel
    {
        /// <summary>
        /// Class for ReleaseCommitment search result
        /// </summary>
         long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the Artist and Concatenated ContractingParties .
        /// </summary>
         String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Contract Contracting Parties .
        /// </summary>
         String ContractingParties { get; set; }

        /// <summary>
        /// Gets or sets the Contract Clearence Admin Company
        /// </summary>
         String CLEARANCECO { get; set; }

        /// <summary>
        /// Gets or sets the Contract Commencement Date .
        /// </summary>
         DateTime? CommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the Contract Description.
        /// </summary>
         String ContractDescription { get; set; }

        /// <summary>
        /// Gets or sets the End Of Term Date .
        /// </summary>
         DateTime? EndOfTermDate { get; set; }

        /// <summary>
        ///  Gets or sets the Rights Period .
        /// </summary>
         string RightsPeriod { get; set; }

        /// <summary>
        ///  Gets or sets the RightsExpiryRule .
        /// </summary>
         string RightsExpiryRule { get; set; }

        /// <summary>
        ///  Gets or sets the LostRightsDate.
        /// </summary>
         DateTime? LostRightsDate { get; set; }

        /// <summary>
        /// Gets or sets the TerritorialRight .
        /// </summary>
         String TerritorialRight { get; set; }

        /// <summary>
        ///  Gets or sets the ReleaseCommitmentAndRightsReversion.
        /// </summary>
         string ReleaseCommitmentAndRightsReversion { get; set; }

        /// <summary>
        ///  Gets or sets the Number Of Linked Release.
        /// </summary>
         int NoOfLinkedReleases { get; set; }

        /// <summary>
        ///  Gets or sets the Number Of Linked Resource.
        /// </summary>
         int NoOfLinkedResources { get; set; }

        /// <summary>
         /// Get or set the recordCount
        /// </summary>
        long recordCount { get; set; }

        /// <summary>
        /// Get or set the ClearanceCompanyId
        /// </summary>
        String ClearanceCompanyId { get; set; }

        /// <summary>
        /// Get or set the ClearanceCompanyName
        /// </summary>
        String ClearanceCompanyName { get; set; }

        /// <summary>
        /// Get or set the ExportType
        /// </summary>
        String ExportType { get; set; }

        /// <summary>
        /// Get or set the ReleaseCommitmentModelContracts
        /// </summary>
        List<IReleaseCommitmentModel> ReleaseCommitmentModelContracts { get; set; }

        /// <summary>
        /// Get or set the WorkflowStatuses
        /// </summary>
        List<StringIdentifier> WorkflowStatuses { get; set; }

        /// <summary>
        /// Get or set the LinkedRepertoire
        /// </summary>
        bool LinkedRepertoire { get; set; }

        /// <summary>
        /// Get or set the ArtistSearch
        /// </summary>
        ArtistDetail ArtistSearch { get; set; }

        /// <summary>
        /// Gets or sets Sort Field .
        /// </summary>
        string SortField { get; set; }

        /// <summary>
        /// Gets or sets Ascending/Descending .
        /// </summary>
        string IsAscending { get; set; }

        /// <summary>
        /// Gets or sets Total ResultCount .
        /// </summary>
        int Total { get; set; }
    }
}
