/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RegularProject.cs 
 * Project Code : UMG-GRCS 
 * Author       : sarika tyagi
 * Created on   : 10-10-2012
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
    public class RegularProject : ClearanceProject
    {

        /// <summary>
        /// EstimatedSalesUnit
        /// </summary>
        [DataMember]
        // [Display(Name = "EstimatedSalesUnit", ResourceType = typeof(Entity))]
        public int EstimatedSalesUnit { get; set; }

        /// <summary>
        /// ReleaseDate
        /// </summary>
        [DataMember]
        // [Display(Name = "ReleaseDate", ResourceType = typeof(Entity))]
        public long ReleaseDate { get; set; }

        ///// <summary>
        ///// Details
        ///// </summary>
        //[DataMember]
        //public string Details { get; set; }

        /// <summary>
        /// ThirdPartyRepertoire
        /// </summary>
        [DataMember]
        public string ThirdPartyRepertoire { get; set; }

        /// <summary>
        /// MultiArtist
        /// </summary>
        [DataMember]
        public bool MultiArtist { get; set; }

        /// <summary>
        /// Compilation
        /// </summary>
        [DataMember]
        public bool Compilation { get; set; }

        /// <summary>
        /// HighPriority
        /// </summary>
        [DataMember]
        public bool HighPriority { get; set; }

        /// <summary>
        /// ScopeAndRequestType
        /// </summary>
        [DataMember]
        public RequestTypeRegular ScopeAndRequestType { get; set; }

        [DataMember]
        public bool IsSensitiveDataChanged { get; set; }



    }
}

