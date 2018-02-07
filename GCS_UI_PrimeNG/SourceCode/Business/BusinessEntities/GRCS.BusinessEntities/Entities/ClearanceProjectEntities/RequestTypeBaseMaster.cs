/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RequestTypeBaseMaster.cs 
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
    public class RequestTypeBaseMaster
    {

        /// <summary>
        /// MasterRequestType
        /// </summary>
        [DataMember]
        //[Display(Name = "MasterRequestType", ResourceType = typeof(Entity))]
        public int MasterRequestType { get; set; }

        /// <summary>
        /// TV
        /// </summary>
        [DataMember]
        //  [Display(Name = "TV", ResourceType = typeof(Entity))]
        public bool TV { get; set; }

        /// <summary>
        /// Radio
        /// </summary>
        [DataMember]
        //[Display(Name = "Radio", ResourceType = typeof(Entity))]
        public bool Radio { get; set; }

        /// <summary>
        /// Cinema
        /// </summary>
        [DataMember]
        // [Display(Name = "Cinema", ResourceType = typeof(Entity))]
        public bool Cinema { get; set; }

        /// <summary>
        /// Internet
        /// </summary>
        [DataMember]
        // [Display(Name = "Internet", ResourceType = typeof(Entity))]
        public bool Internet { get; set; }

        /// <summary>
        /// Video
        /// </summary>
        [DataMember]
        // [Display(Name = "Video", ResourceType = typeof(Entity))]
        public bool Video { get; set; }

        /// <summary>
        /// Other
        /// </summary>
        [DataMember]
        // [Display(Name = "Other", ResourceType = typeof(Entity))]
        public bool Other { get; set; }

        /// <summary>
        /// InitialNoOfVideos
        /// </summary>
        [DataMember]
        // [Display(Name = "InitialNoOfVideos", ResourceType = typeof(Entity))]
        public string InitialNoOfVideos { get; set; }

        /// <summary>
        /// OtherComments
        /// </summary>
        [DataMember]
        // [Display(Name = "OtherComments", ResourceType = typeof(Entity))]
        public string OtherComments { get; set; }

        /// <summary>
        /// OptionalAdditionalRights
        /// </summary>
        [DataMember]
        // [Display(Name = "OptionalAdditionalRights", ResourceType = typeof(Entity))]
        public string OptionalAdditionalRights { get; set; }


    }
}
