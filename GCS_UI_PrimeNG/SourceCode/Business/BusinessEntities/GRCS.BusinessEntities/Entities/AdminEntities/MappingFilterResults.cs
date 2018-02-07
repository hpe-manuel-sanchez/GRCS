/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MappingFilterResults.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 20-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the MappingFilterResults CDL-Contract Entities
 
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    [DataContract]
    public class MappingFilterResults
    {
        public MappingFilterResults()
        {
            //ContractMappingValues = new ContractMappingList();
            ContractMappingsList = new List<ContractMappingDetails>();
        }

        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>The row count.</value>
        [DataMember]
        public long RowCount { get; set; }

        ///// <summary>
        ///// Gets or sets the contract mapping list.
        ///// </summary>
        ///// <value>The contract mapping list.</value>
        //[DataMember]
        //public ContractMappingList ContractMappingValues { get; set; }

        /// <summary>
        /// Gets or sets the contract mappings list.
        /// </summary>
        /// <value>The contract mappings list.</value>
        [DataMember]
        public List<ContractMappingDetails> ContractMappingsList { get; set; }

    }
}
