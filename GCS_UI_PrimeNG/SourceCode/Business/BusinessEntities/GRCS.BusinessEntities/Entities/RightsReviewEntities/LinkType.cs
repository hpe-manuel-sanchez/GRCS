/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : LinkType.cs 
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
* Description :  Defines the enum for LinkType 
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// ENUM values for each Link Type
    /// </summary>
    [DataContract]
    public enum LinkType
    {
        [EnumMember] None,

        [EnumMember] Contract,

        [EnumMember] SplitDeal,

        [EnumMember] MAC,

        [EnumMember] SAC,

        [EnumMember] MediaPortal
    }
}
