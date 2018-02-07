
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RollUpStatusModel.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Richa
 * Created on   : 16-Apr-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description : Defines the Model for Roll-Up Status Report Search Result
                  
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
    ///Roll-Up Status Report Search Result
    /// </summary>
    [Serializable]
  public class RollUpStatusModel:IRollUpStatusModel
    {
      /// <summary>
        /// 
        /// </summary>
        public string UPC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>

        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the VersionTitle .
        /// </summary>

        public string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>

        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the DataAdminCompany .
        /// </summary>

        public string DataAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Multi-Artist Compilation .
        /// </summary>

        public string MultiArtistCompilation { get; set; }

        /// <summary>
        /// Gets or sets the Release Date .
        /// </summary>

        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceAdminCompany .
        /// </summary>

        public string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Roll-up Status .
        /// </summary>

        public string RollUp_Status { get; set; }

        /// <summary>
        /// Gets or sets the Last Roll-up Status .
        /// </summary>

        public string LastRollUpDate { get; set; }

        /// <summary>
        /// Gets or sets the Release issuer .
        /// </summary>
        public string Releaseissuer { get; set; }

        /// <summary>
        /// Gets or sets the Package .
        /// </summary>

        public string Package { get; set; }

        /// <summary>
        /// Gets or sets the Package .
        /// </summary>

        public string TerritorialRight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IRollUpStatusModel> releaseList { get; set; }

        /// <summary>
        /// Gets or sets the StartDate
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the EndDate
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the recordCount
        /// </summary>
        public long recordCount { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceCompanyId
        /// </summary>
        public string ClearanceCompanyId { get; set; }
        /// <summary>
        /// Gets or sets the ExportType
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
       public  int Total { get; set; }
    }
}
