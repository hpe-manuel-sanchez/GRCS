/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RccClearanceContract.cs 
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
 *Description :  Defines the entities for Parent Contract Search
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// RccClearanceContract which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class RccClearanceContract : EntityInformation
    {
        /// <summary>
        /// WorkFlowStatus
        /// </summary>
        [DataMember]
        public string WorkFlowStatus { get; set; }

        /// <summary>
        /// ContractId
        /// </summary>
        [DataMember]
        public long? ContractId { get; set; }

        // Contracting Parties Related
        [DataMember]
        public string ContractingParty { get; set; }

        [DataMember]
        public string ClearanceAdminCompanyCountry { get; set; }


        // Artist Related
        [DataMember]
        public string Artist { get; set; }

        [DataMember]
        public long? ArtistId { get; set; }

        [DataMember]
        public string ArtistinLocalCharacters { get; set; }


        // Contract Definition
        [DataMember]
        public string ContractStatus { get; set; }


        //Clearance Related
        [DataMember]
        public string ClearanceNotes { get; set; }
    }
}