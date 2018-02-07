/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceProjectDetails.cs 
 * Project Code : UMG-GRCS 
 * Author       : Jelio Halleys J
 * Created on   : 18-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

*************************************************************************** 
 * Description  : Contains the Basic Clearance Project Details
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    public class ClearanceProjectDetails
    {
        /// <summary>
        /// ClearanceProjectID
        /// </summary>
        [DataMember]        
        public long ClearanceProjectID { get; set; }
        
        /// <summary>
        /// ProjectID
        /// </summary>
        [DataMember]
        public long? ProjectID { get; set; }

        /// <summary>
        /// ProjectCode
        /// </summary>
        [DataMember]
        public string ProjectCode { get; set; }

        /// <summary>
        /// ProjectType
        /// </summary>
        [DataMember]
        public byte? ProjectType { get; set; }

        /// <summary>
        /// CreatedDate
        /// </summary>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// List of ReleaseTerritory Countries
        /// </summary>
        [DataMember]
        public List<string> ReleaseTerritories { get; set; }

        /// <summary>
        /// ProjectTypeDescription
        /// </summary>
        [DataMember]
        public String ProjectTypeDescription { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// LocalReference
        /// </summary>
        [DataMember]
        public string LocalReference { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DataMember]
        public byte? Status { get; set; }

        /// <summary>
        /// StatusDescription
        /// </summary>
        [DataMember]
        public string StatusDescription { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [DataMember]
        public string Currency { get; set; }

        /// <summary>
        /// RequesterCompanyId
        /// </summary>
        [DataMember]
        public string RequesterCompanyId { get; set; }

        /// <summary>
        /// SensitiveExploitationFlag
        /// </summary>
        [DataMember]
        public bool? SensitiveExploitationFlag { get; set; }
        
        /// <summary>
        /// ReleaseType
        /// </summary>
        [DataMember]
        public string ReleaseType { get; set; }

        /// <summary>
        /// List of CampaignTerritory Countries
        /// </summary>
        [DataMember]
        public List<string> CampaignTerritories { get; set; }


    }


}
