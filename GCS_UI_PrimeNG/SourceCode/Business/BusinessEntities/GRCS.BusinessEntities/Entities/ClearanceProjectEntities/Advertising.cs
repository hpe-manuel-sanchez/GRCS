/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Advertising.cs 
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
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class Advertising : RequestTypeBaseMaster
    {
        private const string DateFormat = "0:dd/Mmm/yyyy";
        /// <summary>
        /// AdvertisedProducts
        /// </summary>
        [DataMember]
        //  [Display(Name = "AdvertisedProducts", ResourceType = typeof(Entity))]
        public string AdvertisedProducts { get; set; }

        /// <summary>
        /// NoOfSpots
        /// </summary>
        [DataMember]
        //  [Display(Name = "NoOfSpots", ResourceType = typeof(Entity))]
        public string NoOfSpots { get; set; }

        /// <summary>
        /// DurationFrom
        /// </summary>
        [DataMember]
        //  [Display(Name = "DurationFrom", ResourceType = typeof(Entity))]
        [DisplayFormat(DataFormatString = DateFormat, ApplyFormatInEditMode = true)]

        public DateTime? DurationFrom { get; set; }

        /// <summary>
        /// DurationTo
        /// </summary>
        [DataMember]
        // [Display(Name = "DurationTo", ResourceType = typeof(Entity))]
        [DisplayFormat(DataFormatString = DateFormat, ApplyFormatInEditMode = true)]
        public DateTime? DurationTo { get; set; }
    }
}
