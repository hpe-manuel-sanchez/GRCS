/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightExpiryMailContent.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 14-11-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsEntities
{
    /// <summary>
    /// Class containing the eMail notification
    ///     contents of expiring rights
    /// </summary>
    [DataContract]
    public class RightExpiryMailContent
    {
        /// <summary>
        /// Represents territorial country name
        /// </summary>
        [DataMember]
        public string RightsCountry { get; set; }
        /// <summary>
        /// Represents start date of the
        ///     expiry period of notification
        /// </summary>
        [DataMember]
        public string StartDate { get; set; }
        /// <summary>
        /// Represents end date of the
        ///     expiry period of notification
        /// </summary>
        [DataMember]
        public string EndDate { get; set; }
        /// <summary>
        /// Represents the Rights set details 
        /// </summary>
        [DataMember]
        public List<RightSetDetail> RightSetDetail { get; set; }

        /// <summary>
        /// Represents the TO address 
        /// </summary>
        [DataMember]
        public string EmailTo { get; set; }
    }
}
