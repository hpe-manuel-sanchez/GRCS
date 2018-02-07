/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractMappingList.cs 
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
 * Description :  Defines the ContractMappingList CDL-Contract Entities
 
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    [DataContract]
    public class ContractMappingList : EntityInformation
    {
        /// <summary>
        /// Contracts the mappings list.
        /// </summary>
        public ContractMappingList()
        {
            ContractMappingsList = new List<ContractMappingDetails>();
        }

        /// <summary>
        /// Gets or sets the contract mappings list.
        /// </summary>
        /// <value>The contract mappings list.</value>
        [DataMember]
        public List<ContractMappingDetails> ContractMappingsList { get; set; }
    }
}
