/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IActiveRosterModel.cs
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

namespace UMGI.GRCS.UI.Interfaces.Report
{
    public interface IActiveRosterModel
    {
        /// <summary>
        /// Gets or sets the Contract Id .
        /// </summary>
        /// <value>The Contract Id.</value>

        long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the Contract Local Reference Number .
        /// </summary>
        /// <value>The Contract Local Reference Number.</value>

        String LocalReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        /// <value>The Artist.</value>

        String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Contract Contracting Parties .
        /// </summary>
        /// <value>The Contract Contracting Parties.</value>

        String ContractingParties { get; set; }

        /// <summary>
        /// Gets or sets the Contract CLEARANCE CO .
        /// </summary>
        /// <value>The Contract CLEARANCE CO.</value>

        String CLEARANCECO { get; set; }

        /// <summary>
        /// Gets or sets the Contract Commencement Date .
        /// </summary>
        /// <value>The Contract Commencement Date.</value>

        DateTime? CommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the Contract Territorial Right .
        /// </summary>
        /// <value>The Contract Territorial Right.</value>

        String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the Contract UMG Signing Company.
        /// </summary>
        /// <value>The Contract UMG Signing Company.</value>

        String UMGSigningCompany { get; set; }

        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        /// <value>The flag for Linked Repertoire.</value>

        string LinkedRepertoire { get; set; }

        /// <summary>
        /// Gets or sets the contracts
        /// </summary>
        List<IActiveRosterModel> contracts { get; set; }

        /// <summary>
        /// Gets or sets the recordCount
        /// </summary>
         long recordCount { get; set; }

        /// <summary>
         /// Gets or sets the ClearanceCompanyId
        /// </summary>
          string ClearanceCompanyId { get; set; }

        /// <summary>
          /// Gets or sets the ClearanceCompanyName
        /// </summary>
          string ClearanceCompanyName { get; set; }

        /// <summary>
          /// Gets or sets the ExportType
        /// </summary>
          string ExportType { get; set; }

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
