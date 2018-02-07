/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RequestTypeRegular.cs 
 * Project Code : UMG-GRCS 
 * Author       : sarika tyagi
 * Created on   : 10-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class OthersICLA : TVRadioBreakICLABase
    {

        /// <summary>
        /// IsOthers
        /// </summary>
        [DataMember]
        public bool IsOthers { get; set; }

        /// <summary>
        /// OtherMediaDetails
        /// </summary>
        [DataMember]
        public string OtherMediaDetails { get; set; }
    }
}
