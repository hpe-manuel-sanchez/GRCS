/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
    Rubini Raja      17/07/2012      Changed for Warning as Error: 
 *                                   'ContractStatus' hides inherited member 
 *                                   ContractInfo.ContractStatus'. 
 *                                   Use the new keyword if hiding was intended.
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Contract Search Class
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// Contract Search which is inherited from ContractDetails class
    /// </summary>
    [DataContract]
    public class ContractSearch : ContractDetails
    {
        /// <summary>
        /// AdminCompany
        /// </summary>
        [DataMember]
        public string AdminCompany { get; set; }

        /// <summary>
        /// Value of ClearanceCompany
        /// </summary>
        [DataMember]
        public string ClearanceCompanyValue { get; set; }

        /// <summary>
        /// SigningCompany
        /// </summary>
        [DataMember]
        public string SigningCompany { get; set; }

        /// <summary>
        /// ContractName
        /// </summary>
        [DataMember]
        public string ContractName { get; set; }

        /// <summary>
        /// WorkFlowStatus
        /// </summary>
        [DataMember]
        public string WorkFlowStatus { get; set; }

        /// <summary>
        /// ContractSearchStatus
        /// </summary>
        [DataMember]
        public string ContractSearchStatus { get; set; }

        /// <summary>
        /// Value of RightsType
        /// </summary>
        [DataMember]
        public string RightsTypeValue { get; set; }
    }
}