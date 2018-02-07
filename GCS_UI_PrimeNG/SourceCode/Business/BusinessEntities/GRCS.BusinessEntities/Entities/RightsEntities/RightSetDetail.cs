/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightSetDetail.cs 
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
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsEntities
{
    /// <summary>
    /// Class containing the properties of
    ///     Right Set
    /// </summary>
    [DataContract]
    public class RightSetDetail
    {
        /// <summary>
        /// Represents Release ID(UPC) of the
        ///     release
        /// </summary>
        [DataMember]
        public string ReleaseIdUPC { get; set; }
        /// <summary>
        /// Represents the Rights period 
        /// </summary>
        [DataMember]
        public string RightsPeriod { get; set; }
        /// <summary>
        ///  Represents the Rights expiry rules 
        /// </summary>
        [DataMember]
        public string RightsExpiryRules { get; set; }
        /// <summary>
        /// Represents the rights expiry date
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public DateTime? LostRightsDate { get; set; }
        /// <summary>
        /// Represents the rights expiry indicator
        /// </summary>
        [DataMember]
        public string LostRightsIndicator { get; set; }
        /// <summary>
        /// Represents the rights expiry reason
        /// </summary>
        [DataMember]
        public string LostRightsReason { get; set; }
        /// <summary>
        /// Represents whether having 
        ///     physical exploitation rights
        /// </summary>
        [DataMember]
        public bool HasPhysicalExploitationRights { get; set; }
        /// <summary>
        /// Represents whether having 
        ///     digital exploitation rights
        /// </summary>
        [DataMember]
        public bool HasDigitalExploitationRights { get; set; }
        /// <summary>
        /// Represents territorial right details
        /// </summary>
        [DataMember]
        public string TerritorialRights { get; set; }
    }
}
