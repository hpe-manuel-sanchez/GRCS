/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ContractSearchCollection
 * Project Code :   
 * Author       : Siva
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Siva              03/08/2012      Inital Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
* Description :  Defines the entities for Contract Search Result Class
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// ContractSearchResult
    /// </summary>
    [DataContract]
    public class ContractSearchResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractSearchResult"/> class.
        /// </summary>
        public ContractSearchResult()
        {
            ContractSearchInfo = new List<ContractInfo>();
        }

        /// <summary>
        /// ContractSearchInfo
        /// </summary>
        [DataMember]
        public List<ContractInfo> ContractSearchInfo { get; set; }

        /// <summary>
        /// ContractsCount
        /// </summary>
        [DataMember]
        public long ContractsCount { get; set; }
    }
}