/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractMapping.cs 
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
 * Description :  Defines the ContractMapping CDL-Contract Entities
 
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    [DataContract]
    public class ContractMapping : MappingInfo
    {
        /// <summary>
        /// A unique AutoLink Id to represent a CDL-Contract mapping
        /// </summary>
        [DataMember]
        public long AutoLinkId { get; set; }

        /// <summary>
        /// A unique ContractId
        /// </summary>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// Last Modified Time will be helpful while updating a record to check concurrency issue
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the last modified date. 
        /// This property is used to hold the serialized LastModifiedTime for Update scenario. This one need not be datemember property as it's not defined to travel across WCF services.
        /// </summary>
        /// <value>The last modified date.</value>
        public string LastModifiedDate { get; set; }
    }
}
