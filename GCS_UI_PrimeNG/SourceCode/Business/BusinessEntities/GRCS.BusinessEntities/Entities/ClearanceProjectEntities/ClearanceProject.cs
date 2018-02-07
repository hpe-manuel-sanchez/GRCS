/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceProject.cs 
 * Project Code : UMG-GRCS 
 * Author       : dhruv arora
 * Created on   : 09-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceProject
    {
        /// <summary>
        /// ClearanceProjectID
        /// </summary>
        [DataMember]
        // [Display(Name = "ClearanceProjectID", ResourceType = typeof(Entity))]
        public long ClearanceProjectID { get; set; }


        /// <summary>
        /// RequesterWorkgroupId
        /// </summary>
        [DataMember]
        // [Display(Name = "RequesterWorkgroupId", ResourceType = typeof(Entity))]
        public int RequesterWorkgroupId { get; set; }

        /// <summary>
        /// RequesterCompanyId
        /// </summary>
        [DataMember]
        // [Display(Name = "RequesterCompanyId", ResourceType = typeof(Entity))]
        public int RequesterCompanyId { get; set; }

        /// <summary>
        /// RequesterUserId
        /// </summary>
        [DataMember]
        // [Display(Name = "RequesterUserId", ResourceType = typeof(Entity))]
        public int RequesterUserId { get; set; }

        /// <summary>
        /// IsMaster
        /// </summary>
        [DataMember]
        //  [Display(Name = "IsMaster", ResourceType = typeof(Entity))]
        public bool IsMaster { get; set; }

        /// <summary>
        /// Details
        /// </summary>
        [DataMember]
        // [Display(Name = "Details", ResourceType = typeof(Entity))]
        public string Details { get; set; }

        /// <summary>
        /// StatusType
        /// </summary>
        [DataMember]
        // [Display(Name = "StatusType", ResourceType = typeof(Entity))]
        public int StatusType { get; set; }

        /// <summary>
        /// StatusTypeDesc
        /// </summary>
        [DataMember]
        // [Display(Name = "Status TypeDesc", ResourceType = typeof(Entity))]
        public string StatusTypeDesc { get; set; }

        /// <summary>
        /// ProjectType
        /// </summary>
        [DataMember]
        //[Display(Name = "ProjectType", ResourceType = typeof(Entity))]
        public int ProjectType { get; set; }


        /// <summary>
        /// CreatedBy
        /// </summary>
        [DataMember]
        // [Display(Name = "CreatedBy", ResourceType = typeof(Entity))]
        public string CreatedBy { get; set; }

        /// <summary>
        /// ProjectReferenceId
        /// </summary>
        [DataMember]
        // [Display(Name = "ProjectReferenceId", ResourceType = typeof(Entity))]
        public string ProjectReferenceId { get; set; }


        /// <summary>
        ///  CreatedDate
        /// </summary>
        [DataMember]
        // [Display(Name = "CreatedDate", ResourceType = typeof(Entity))]
        public DateTime OriginalReleaseDate { get; set; }

        /// <summary>
        /// RequestingCompany
        /// </summary>
        [DataMember]
        // [Display(Name = "RequestingCompany", ResourceType = typeof(Entity))]
        public int RequestingCompany { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [DataMember]
        //  [Display(Name = "Currency", ResourceType = typeof(Entity))]
        public string Currency { get; set; }




        [DataMember]
        public List<TerritorialDisplay> Territories { get; set; }

        /// <summary>
        /// ProjectTitle
        /// </summary>
        [DataMember]
        // [Display(Name = "ProjectTitle", ResourceType = typeof(Entity))]
        public string ProjectTitle { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DataMember]
        //[Display(Name = "Status", ResourceType = typeof(Entity))]
        public string Status { get; set; }

        /// <summary>
        /// LocalReference
        /// </summary>
        [DataMember]
        // [Display(Name = "LocalReference", ResourceType = typeof(Entity))]
        public string LocalReference { get; set; }


        /// <summary>
        /// UploadProjectDocuments
        /// </summary>
        [DataMember]
        //  [Display(Name = "UploadProjectDocuments", ResourceType = typeof(Entity))]
        public string UploadProjectDocuments { get; set; }


        /// <summary>
        /// SensitiveExplotation
        /// </summary>
        [DataMember]
        //  [Display(Name = "SensitiveExplotation", ResourceType = typeof(Entity))]
        public bool SensitiveExplotation { get; set; }


        /// <summary>
        /// OneStop
        /// </summary>
        [DataMember]
        // [Display(Name = "OneStop", ResourceType = typeof(Entity))]
        public bool OneStop { get; set; }

        /// <summary>
        /// OneStopReason
        /// </summary>
        [DataMember]
        // [Display(Name = "OneStopReason", ResourceType = typeof(Entity))]
        public string OneStopReason { get; set; }

        /// <summary>
        /// LicenseTerm
        /// </summary>
        [DataMember]
        // [Display(Name = "LicenseTerm", ResourceType = typeof(Entity))]
        public string LicenseTerm { get; set; }


        /// <summary>
        [DataMember]
        public int ProjectTypeId { get; set; }

        /// <summary>
        [DataMember]
        public string ProjectTypeDesc { get; set; }

        /// <summary>
        [DataMember]
        public int RequestTypeID { get; set; }

        /// <summary>
        [DataMember]
        public string RequestTypeDesc { get; set; }


        /// <summary>
        [DataMember]
        public int RequestCompanyID { get; set; }

        /// <summary>
        [DataMember]
        public string RequestCompanyName { get; set; }

        /// <summary>
        [DataMember]
        public int ThirdPartyCompanyID { get; set; }

        /// <summary>
        [DataMember]
        public string ThirdPartyCompanyName { get; set; }

        /// <summary>
        [DataMember]
        public List<ClearanceResource> ClearanceResource { get; set; }

        [DataMember]
        public long ClrProjectId { get; set; }

        [DataMember]
        public string CreatedUserName { get; set; }

        [DataMember]
        public List<UploadDocument> listUploadDocument { get; set; }

        [DataMember]
        public List<ClearanceRelease> ObjRelease { get; set; }

        [DataMember]
        public ClearanceReleaseSearch ObjReleaseSearch { get; set; }

        [DataMember]
        public ClearanceReleaseSearchResult ObjReleaseSearchResult { get; set; }

        [DataMember]
        public string Command { get; set; }

        [DataMember]
        [XmlIgnore]
        public List<ClearanceInboxRequest> RequestInfoList { get; set; }
   
        
        private bool isUMGiMarkettingRoutingRequired = true;

        [DataMember]
        public bool IsUMGiMarkettingRoutingRequired
        {
            get { return isUMGiMarkettingRoutingRequired; }
            set { isUMGiMarkettingRoutingRequired = value; }
        }

        [DataMember]
        public string LoggedInUser { get; set; }

        [DataMember]
        public string Rcc_User { get; set; }

        [DataMember]
        public string IncludedTerritories { get; set; }

        [DataMember]
        public string ExcludedTerritories { get; set; }

        [DataMember]
        [DefaultValue(false)]
        public bool IsChangedfromSave { get; set; }

        [DataMember]
        public string ResubmitReasonComments { get; set; }

        [DataMember]
        public DateTime ProjectModifiedDate { get; set; }

        [DataMember]
        public bool IsProjectResubmitted { get; set; }

        [DataMember]
        public bool IsNewCountriesAddedAfterSubmit { get; set; }
    }
}
