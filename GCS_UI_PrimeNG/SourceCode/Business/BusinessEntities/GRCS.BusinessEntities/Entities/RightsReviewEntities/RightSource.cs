/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsSource.cs 
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
* Description :  Defines the enum for Rights Source
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// ENUM values for each Rights Source
    /// </summary>
    [DataContract]
    public enum RightsSource
    {
        [EnumMember]
        None,

        [EnumMember]
        RollUpComplete,

        [EnumMember]
        RollUpPartial,

        [EnumMember]
        Contract,

        [EnumMember]
        Default,       
       
        [EnumMember]
        ClearanceRequest,
       
        [EnumMember]
        User,
       
        [EnumMember]
        RollUpMissing,
       
        [EnumMember]
        RollUpComplex,
      
        [EnumMember]
        RollUpMissingOrComplex,     
      
        [EnumMember]
        MAC
    }
}
