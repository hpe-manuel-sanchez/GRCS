/* ************************************************************************ 
 * Copyrights ? 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsType.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 12-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *
 * Description  : Contains the ENUM values for each Rights Type
 * 
*****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    /// <summary>
    /// Enum for the Rights Type
    /// </summary>
    [DataContract]
    public enum RightsType1
    {
        [EnumMember]
        Contractrights = 34,
        [EnumMember]
        Projectrights = 38,
        [EnumMember]
        Resourcerights = 36,
        [EnumMember]
        Releaserights = 35,
        [EnumMember]
        Trackrights = 37,
        [EnumMember]
        Artistrights = 107
    }
}
