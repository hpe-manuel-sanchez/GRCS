/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseType.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 07-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the enum for Release Type 
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// ENUM values for each Release Type 
    /// </summary>
    [DataContract]
    public enum ReleaseType
    {
        [EnumMember]
        None,

        [EnumMember]
        PhysicalRelease,

        [EnumMember]
        DigitalRelease,

        [EnumMember]
        PhysicallyLinked,

        [EnumMember]
        DigitalLink,

        [EnumMember]
        DigitalPackage,

        [EnumMember]
        PhysicalPackage,

        [EnumMember]
        DigitalLinkedPackage,

        [EnumMember]
        PhysicallyLinkedPackage

    }
}
