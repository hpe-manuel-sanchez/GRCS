/* ***************************************************************************  
* Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : WorkQueueFilterType.cs
 * Project Code :   
 * Author       : Rubini Raja
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Rubini            06/12/2012      Flags attribute added
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Work queue filter types
                  
****************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    /// <summary>
    /// Work queue filter types
    /// </summary>
    [DataContract]
    [Flags]
    public enum WorkQueueFilterType
    {
        [EnumMember]
        All = 0x0,
        [EnumMember]
        Release = 0x1,
        [EnumMember]
        Resource = 0x2,
        [EnumMember]
        Project =0x4,
        [EnumMember]
        None = 0x8,
    }
}
