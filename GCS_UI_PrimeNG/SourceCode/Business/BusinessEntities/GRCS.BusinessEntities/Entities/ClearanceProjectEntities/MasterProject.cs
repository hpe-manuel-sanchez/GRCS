/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MasterProject.cs 
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
using System.Runtime.Serialization;
namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class MasterProject : ClearanceProject
    {
        /// <summary>
        /// CreatedBy
        /// </summary>
        [DataMember]
        // [Display(Name = "CreatedBy", ResourceType = typeof(ClearanceLayout))]
        public string CreatedByUser { get; set; }

        /// <summary>
        /// CreatedDate
        /// </summary>
        [DataMember]
        // [Display(Name = "CreatedDate", ResourceType = typeof(ClearanceLayout))]
        public string CreatedDate { get; set; }

        /// <summary>
        /// ClientName
        /// </summary>
        [DataMember]
        // [Display(Name = "MasterProjectClientNameLabel", ResourceType = typeof(ClearanceLayout))]
        public string ClientName { get; set; }

        /// <summary>
        /// ClientWebsite
        /// </summary>
        [DataMember]
        //  [Display(Name = "ClientWebsite", ResourceType = typeof(Entity))]
        public string ClientWebsite { get; set; }

        /// <summary>
        /// IsAdvertisingRequest
        /// </summary>
        [DataMember]
        // [Display(Name = "IsAdvertisingRequest", ResourceType = typeof(Entity))]
        public bool IsAdvertisingRequest { get; set; }

        /// <summary>
        /// IsFilmRequest
        /// </summary>
        [DataMember]
        // [Display(Name = "IsFilmRequest", ResourceType = typeof(Entity))]
        public bool IsFilmRequest { get; set; }

        /// <summary>
        /// IsTrailerRequest
        /// </summary>
        [DataMember]
        // [Display(Name = "IsTrailerRequest", ResourceType = typeof(Entity))]
        public bool IsTrailerRequest { get; set; }

        /// <summary>
        /// IsOtherRequest
        /// </summary>
        [DataMember]
        // [Display(Name = "IsOtherRequest", ResourceType = typeof(Entity))]
        public bool IsOtherRequest { get; set; }

        [DataMember]
        public Advertising Advertising { get; set; }

        [DataMember]
        public Film Film { get; set; }

        [DataMember]
        public Trailer Trailer { get; set; }

        [DataMember]
        public Others Others { get; set; }

        [DataMember]
        public bool IsSensitiveDataChanged { get; set; }

        [DataMember]
        public bool IsResubmissionOccured { get; set; }


    }
}
