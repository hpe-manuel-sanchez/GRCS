/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : AutoLinkCDLContract.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 04-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the AutoLinking CDL-Contract Entities
 
****************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    [DataContract]
    [Serializable]
    public class AutoLinkCdlContract
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

        /// <summary>
        /// Contract Id to which a CDL is getting mapped
        /// </summary>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// A unique AutoLink Id to represent a CDL-Contract mapping
        /// </summary>
        [DataMember]
        public long? AutoLinkId { get; set; }

        /// <summary>
        /// Last Modified Time will be helpful while updating a record to check concurrency issue
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTime { get; set; }
    }
}
