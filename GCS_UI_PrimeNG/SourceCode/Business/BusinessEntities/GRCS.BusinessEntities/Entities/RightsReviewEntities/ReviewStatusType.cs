/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReviewStatusType.cs 
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
* Description :  Defines the enum for Review Status Type
                  
****************************************************************************/
using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// ENUM values for Review Status Type
    /// </summary>
    [DataContract]
    [Flags]
    public enum ReviewStatusType
    {
        [EnumMember]
        All = 0x0,
        [EnumMember]
        NewForReview = 0x1,

        [EnumMember]
        FinalForReview = 0x2,

        [EnumMember]
        Final = 0x3

    }
}
