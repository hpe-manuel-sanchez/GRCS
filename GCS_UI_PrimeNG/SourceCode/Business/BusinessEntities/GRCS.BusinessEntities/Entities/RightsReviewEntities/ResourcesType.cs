/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourcesType.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 11-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the enum for Resources Type  
                  
****************************************************************************/
using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// ENUM values for each Resources Type 
    /// </summary>
    [DataContract]
    [Flags]
    public enum ResourcesType
    {
        [EnumMember]
        All = 0x0,
        [EnumMember]
        Audio = 0x1,

        [EnumMember]
        Video = 0x2,

        [EnumMember]
        Image = 0x3,

        [EnumMember]
        MAC = 0x4,

        [EnumMember]
        Text = 0x5,

        [EnumMember]
        Other = 0x6,

        
    }
}
