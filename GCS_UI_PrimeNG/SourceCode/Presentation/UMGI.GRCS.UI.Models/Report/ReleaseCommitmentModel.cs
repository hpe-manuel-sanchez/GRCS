/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseCommitmentModel.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vinod Chaudhary
 * Created on   : 16-Feb-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description : Defines the Model for Release Commitment Report Search Result
                  
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.UI.Interfaces.Report;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
namespace UMGI.GRCS.UI.Models.Report
{
    public class ReleaseCommitmentModel :IReleaseCommitmentModel 
    {
        /// <summary>
        /// Class for ReleaseCommitment search result
        /// </summary>
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the Artist and Concatenated ContractingParties .
        /// </summary>
        public String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Contract Contracting Parties .
        /// </summary>
        public String ContractingParties { get; set; }

        /// <summary>
        /// Gets or sets the Contract Clearence Admin Company
        /// </summary>
        public String CLEARANCECO { get; set; }

        /// <summary>
        /// Gets or sets the Contract Commencement Date .
        /// </summary>
        public DateTime? CommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the Contract Description.
        /// </summary>
        public String ContractDescription { get; set; }

        /// <summary>
        /// Gets or sets the End Of Term Date .
        /// </summary>
        public DateTime? EndOfTermDate { get; set; }

        /// <summary>
        ///  Gets or sets the Rights Period .
        /// </summary>
        public string RightsPeriod { get; set; }

        /// <summary>
        ///  Gets or sets the RightsExpiryRule .
        /// </summary>
        public string RightsExpiryRule { get; set; }

        /// <summary>
        ///  Gets or sets the LostRightsDate.
        /// </summary>
        public DateTime? LostRightsDate { get; set; }

        /// <summary>
        /// Gets or sets the TerritorialRight .
        /// </summary>
        public String TerritorialRight { get; set; }

        /// <summary>
        ///  Gets or sets the ReleaseCommitmentAndRightsReversion.
        /// </summary>
        public String ReleaseCommitmentAndRightsReversion { get; set; }

        /// <summary>
        ///  Gets or sets the Number Of Linked Release.
        /// </summary>
        public int NoOfLinkedReleases { get; set; }

        /// <summary>
        ///  Gets or sets the Number Of Linked Resource.
        /// </summary>
        public int NoOfLinkedResources { get; set; }

        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        /// <value>The flag for Linked Repertoire.</value>

        /// <summary>
        /// Get or set the recordCount
        /// </summary>
        public long recordCount { get; set; }

        /// <summary>
        /// Get or set the ClearanceCompanyId
        /// </summary>
        public String ClearanceCompanyId { get; set; }

        /// <summary>
        /// Get or set the ClearanceCompanyName
        /// </summary>
        public String ClearanceCompanyName { get; set; }

        /// <summary>
        /// Get or set the ExportType
        /// </summary>
        public String ExportType { get; set; }

        /// <summary>
        /// Get or set the ReleaseCommitmentModelContracts
        /// </summary>
        public List<IReleaseCommitmentModel> ReleaseCommitmentModelContracts { get; set; }

        /// <summary>
        /// Get list of the Artists
        /// </summary>
        public ArtistDetail ArtistSearch { get; set; }

        /// <summary>
        /// Get or set the WorkFlowStatus
        /// </summary>
        public string WorkFlowStatus { get; set; }

        /// <summary>
        /// Get the list of WorkflowStatuses
        /// </summary>
        public List<StringIdentifier> WorkflowStatuses { get; set; }

        /// <summary>
        /// Get or set LinkedRepertoire 
        /// </summary>
        public bool LinkedRepertoire { get; set; }

        /// <summary>
        /// Gets or sets Sort Field .
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets Ascending/Descending .
        /// </summary>
        public string IsAscending { get; set; }
        /// <summary>
        /// Gets or sets Total
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// ReleaseCommitmentModel constructor
        /// </summary>
        public ReleaseCommitmentModel()
        {
            ArtistSearch = new ArtistDetail();
        }
    }
}
