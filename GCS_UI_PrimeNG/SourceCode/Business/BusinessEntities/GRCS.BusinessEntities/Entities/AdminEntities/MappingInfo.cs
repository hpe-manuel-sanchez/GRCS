/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MappingInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 18-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the MappingInfo CDL-Contract Entities
 
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    [DataContract]
    public class MappingInfo : EntityInformation
    {
        /// <summary>
        /// Company Id of CDL combination 
        /// </summary>
        [DataMember]
        public long CompanyId { get; set; }

        /// <summary>
        /// Division Id of CDL combination 
        /// </summary>
        [DataMember]
        public long DivisionId { get; set; }

        /// <summary>
        /// Label Id of CDL combination 
        /// </summary>
        [DataMember]
        public long LabelId { get; set; }
    }
}
