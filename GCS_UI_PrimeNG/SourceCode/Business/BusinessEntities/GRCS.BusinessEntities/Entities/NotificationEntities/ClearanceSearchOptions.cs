/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceSearchOptions.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 22-02-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                         
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *                
*************************************************************************** 
 * Description  : Enum for Clearance Release and Resource Search Option
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{

    /// <summary>
    /// Enum for Clearance Release Search Options
    /// </summary>
    [DataContract]
    public enum ReleaseSearchOption
    {
        [EnumMember]
        R2ReleaseId,

        [EnumMember]
        UPC,

        [EnumMember]
        GCSReleaseId
    };

    /// <summary>
    /// Enum for Clearance Resource Search Options
    /// </summary>
    [DataContract]
    public enum ResourceSearchOption
    {
        [EnumMember]
        R2ResourceId,

        [EnumMember]
        ISRC
    };

}
