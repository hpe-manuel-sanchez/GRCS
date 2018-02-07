/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ParentContractSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Parent Contract Search
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// ParentContractSearch which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class ParentContractSearch : EntityInformation
    {
        /// <summary>
        /// ArtistName
        /// </summary>
        [DataMember]
        public string ArtistName { get; set; }

        /// <summary>
        /// ContractLocalReferenceNo
        /// </summary>
        [DataMember]
        public string ContractLocalReferenceNo { get; set; }

        /// <summary>
        /// ContractingParty
        /// </summary>
        [DataMember]
        public string ContractingParty { get; set; }

        /// <summary>
        /// ContractId
        /// </summary>
        [DataMember]
        public long ContractId { get; set; }
    }
}