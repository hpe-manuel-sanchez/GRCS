
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ActiveRosterModel.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Richa
 * Created on   : 22-Jan-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description : Defines the Model for Active Roster Report Search Result
                  
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.UI.Interfaces.Report;
using System.Web.Mvc;
namespace UMGI.GRCS.UI.Models.Report
{
    /// <summary>
    /// Active Roster Report Search Result
    /// </summary>
    [Serializable]
    public class ActiveRosterModel : IActiveRosterModel
    {
        /// <summary>
        /// Gets or sets the Contract Id .
        /// </summary>
        /// <value>The Contract Id.</value>
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the Contract Local Reference Number .
        /// </summary>
        /// <value>The Contract Local Reference Number.</value>
        public String LocalReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        /// <value>The Artist.</value>
        public String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Contract Contracting Parties .
        /// </summary>
        /// <value>The Contract Contracting Parties.</value>
        public String ContractingParties { get; set; }

        /// <summary>
        /// Gets or sets the Contract CLEARANCE CO .
        /// </summary>
        /// <value>The Contract CLEARANCE CO.</value>
        public String CLEARANCECO { get; set; }

        /// <summary>
        /// Gets or sets the Contract Commencement Date .
        /// </summary>
        /// <value>The Contract Commencement Date.</value>
        public DateTime? CommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the Contract Territorial Right .
        /// </summary>
        /// <value>The Contract Territorial Right.</value>
        public String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the Contract UMG Signing Company.
        /// </summary>
        /// <value>The Contract UMG Signing Company.</value>
        public String UMGSigningCompany { get; set; }

        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        /// <value>The flag for Linked Repertoire.</value>
        public string LinkedRepertoire { get; set; }

        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        /// <value>The flag for Linked Repertoire.</value>
        public List<IActiveRosterModel> contracts { get; set; }

        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        /// <value>The flag for Linked Repertoire.</value>
        public long recordCount { get; set; }

        /// <summary>
        /// Gets or sets the Clearance Company Id .
        /// </summary>
        /// <value>The flag for Linked Repertoire.</value>
        public string ClearanceCompanyId  { get; set; }

        /// <summary>
        /// Gets or sets the Clearance Company Name .
        /// </summary>
        /// <value>The Clearance Company Name.</value>
        public string ClearanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets Export Type .
        /// </summary>
        public string ExportType { get; set; }

        /// <summary>
        /// Gets or sets Sort Field .
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets Ascending/Descending .
        /// </summary>
        public string IsAscending { get; set; }

        /// <summary>
        /// Gets or sets Total ResultCount .
        /// </summary>
        public int Total { get; set; }
    }
}






