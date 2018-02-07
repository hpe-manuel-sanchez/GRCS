
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceRightsDiscrepanciesModel.cs 
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
 * Description : Defines the Model for Resource Rights Discrepancies Report Search Result
                  
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.UI.Interfaces.Report;
using System.Web.Mvc;
namespace UMGI.GRCS.UI.Models.Report
{
    public class ResourceRightsDiscrepanciesModel : IResourceRightsDiscrepanciesModel
    {
        /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
       
        public string ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
       
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the VersionTitle .
        /// </summary>
       
        public string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the ResourceType .
        /// </summary>
       
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
       
        public string ResourceArtist { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePcCompany .
        /// </summary>
       
        public string ResourcePcCompany { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePExtension .
        /// </summary>
       
        public string ResourcePExtension { get; set; }

        /// <summary>
        /// Gets or sets the ResourceRightsType .
        /// </summary>
       
        public string ResourceRightsType { get; set; }

        /// <summary>
        /// Gets or sets the DataAdminCompany .
        /// </summary>
       
        public string DataAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractPcCompany .
        /// </summary>
       
        public string ContractPcCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractPExtension .
        /// </summary>
       
        public string ContractPExtension { get; set; }

        /// <summary>
        /// Gets or sets the ContractRightsType .
        /// </summary>
       
        public string ContractRightsType { get; set; }

        /// <summary>
        /// Gets or sets the ContractArtist .
        /// </summary>
       
        public string ContractArtist { get; set; }

        /// <summary>
        /// Gets or sets the ContractingParty .
        /// </summary>
       
        public string ContractingParty { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceAdminCompany .
        /// </summary>
       
        public string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractId .
        /// </summary>
       
        public string ContractId { get; set; }

        /// <summary>
        /// .
        /// </summary>
        /// <value></value>
        public List<IResourceRightsDiscrepanciesModel> Discrepancies { get; set; }

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
        public int Total { get; set; }
    }
}
