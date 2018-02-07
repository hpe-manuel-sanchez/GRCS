/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RepertoireInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik P
 * Created on   : 26-12-2012
 * ************************************************************************ 
 * Modification History
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using UMGI.GRCS.BusinessEntities.Lookups;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// Class contains the entities
    /// for repertoire information
    /// </summary>
    [DataContract]
    public class RepertoireInfo
    {
        /// <summary>
        /// Repertoire Id
        /// </summary>
        [DataMember]
        public long Id;
        /// <summary>
        /// Enum which contains types 
        /// of repertoire
        /// </summary>
        [EnumMember]
        public RepertoireType Type;
       
    }
}
