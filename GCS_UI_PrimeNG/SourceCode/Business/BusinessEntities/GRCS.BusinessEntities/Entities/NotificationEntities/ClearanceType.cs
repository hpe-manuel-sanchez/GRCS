/* ************************************************************************ 
 * Copyrights ? 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceType.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 24-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *
 * Description  : Contains the ENUM values for each Clearance Type
 * 
*****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{

    /// <summary>
    /// Enum for the Rights Type
    /// </summary>
    [DataContract]
    public enum ClearanceType
    {
        [EnumMember]
        ClearanceProject = 201,
        [EnumMember]
        ClearanceRelease = 202,
        [EnumMember]
        ClearanceResource = 203
    }

}
