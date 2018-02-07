/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IResourceRightsDiscrepanciesModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 16/4/2013
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
   public interface IResourceRightsDiscrepanciesModel
    {
       /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>

         string ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>

         string Title { get; set; }

        /// <summary>
        /// Gets or sets the VersionTitle .
        /// </summary>

         string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the ResourceType .
        /// </summary>

         string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>

         string ResourceArtist { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePcCompany .
        /// </summary>

         string ResourcePcCompany { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePExtension .
        /// </summary>

         string ResourcePExtension { get; set; }

        /// <summary>
        /// Gets or sets the ResourceRightsType .
        /// </summary>

         string ResourceRightsType { get; set; }

        /// <summary>
        /// Gets or sets the DataAdminCompany .
        /// </summary>

         string DataAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractPcCompany .
        /// </summary>

         string ContractPcCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractPExtension .
        /// </summary>

         string ContractPExtension { get; set; }

        /// <summary>
        /// Gets or sets the ContractRightsType .
        /// </summary>

         string ContractRightsType { get; set; }

        /// <summary>
        /// Gets or sets the ContractArtist .
        /// </summary>

         string ContractArtist { get; set; }

        /// <summary>
        /// Gets or sets the ContractingParty .
        /// </summary>

         string ContractingParty { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceAdminCompany .
        /// </summary>

         string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractId .
        /// </summary>

         string ContractId { get; set; }

        /// <summary>
        /// Gets or sets the  .
        /// </summary>
        /// <value>The  for.</value>
         List<IResourceRightsDiscrepanciesModel> Discrepancies { get; set; }

         /// <summary>
         /// Gets or sets Total ResultCount .
         /// </summary>
         int Total { get; set; }

    }
}
