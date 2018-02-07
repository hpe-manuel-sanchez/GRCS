/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsType.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 07-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the enum for Rights Type 
                  
****************************************************************************/

using System.Runtime.Serialization;
using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// ENUM values for each Rights Type 
    /// </summary>
    [DataContract]
    public enum RightTypes
    {
        [EnumMember]
        [Description(" ")]
        Unknown = 0,

        [EnumMember]
        [Description("Owned by UMG")]
        UMGOwned = 1,

        [EnumMember]
        [Description("Exclusive License")]
        ExclusiveLicense = 2,

        [EnumMember]
        [Description("Non-UMG")]
        Non_UMG = 3,

        [EnumMember]
        [Description("Joint Venture")]
        JointVenture = 4,

        [EnumMember]
        [Description("Non Exclusive License")]
        NonExclusiveLicense = 5,

        [EnumMember]
        [Description("ITC LIC")]
        ITCLIC = 6

    }
}
