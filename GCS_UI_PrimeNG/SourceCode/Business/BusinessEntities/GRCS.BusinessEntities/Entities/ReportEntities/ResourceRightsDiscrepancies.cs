/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceRightsDiscrepancies.cs 
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
 * Description : Defines the entities for Resource Rights Discrepancies Report Search Result
                  
****************************************************************************/
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{/// <summary>
 /// Resource Rights Discrepancies Report Search Result
 /// </summary>
    [DataContract]
    public class ResourceRightsDiscrepancies : EntityInformation
    {
        /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
        [DataMember]
        public string ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the VersionTitle .
        /// </summary>
        [DataMember]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the ResourceType .
        /// </summary>
        [DataMember]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        [DataMember]
        public string ResourceArtist { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePcCompany .
        /// </summary>
        [DataMember]
        public string ResourcePcCompany { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePExtension .
        /// </summary>
        [DataMember]
        public string ResourcePExtension { get; set; }

        /// <summary>
        /// Gets or sets the ResourceRightsType .
        /// </summary>
        [DataMember]
        public string ResourceRightsType { get; set; }

        /// <summary>
        /// Gets or sets the DataAdminCompany .
        /// </summary>
        [DataMember]
        public string DataAdminCompany { get; set; }        

        /// <summary>
        /// Gets or sets the ContractPcCompany .
        /// </summary>
        [DataMember]
        public string ContractPcCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractPExtension .
        /// </summary>
        [DataMember]
        public string ContractPExtension { get; set; }

        /// <summary>
        /// Gets or sets the ContractRightsType .
        /// </summary>
        [DataMember]
        public string ContractRightsType { get; set; }

        /// <summary>
        /// Gets or sets the ContractArtist .
        /// </summary>
        [DataMember]
        public string ContractArtist { get; set; }

        /// <summary>
        /// Gets or sets the ContractingParty .
        /// </summary>
        [DataMember]
        public string ContractingParty { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceAdminCompany .
        /// </summary>
        [DataMember]
        public string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the ContractId .
        /// </summary>
        [DataMember]
        public string ContractId { get; set; }

        /// <summary>
        /// Gets or sets the total Resultcount .
        /// </summary>
        [DataMember]
        public int Total { get; set; }
    }
}
