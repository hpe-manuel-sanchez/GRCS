/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ProjectSearchCriteria.cs
 * Project Code :   
 * Author       : Ajitha R
 * Created on   : 05/10/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for Contract Linking.
                  
******************************************************************************/
using System.ComponentModel;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    [DataContract]
  public  class LinkContractInfo: EntityInformation
    {
        /// <summary>
        /// Artist Related
        /// </summary>
        /// <value>The artist id.</value>
        [DataMember]
        [ReadOnly(true)]
        public long? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
        [DataMember]
        public string ArtistName { get; set; }



        /// <summary>
        /// Gets or sets the contracting party id.
        /// </summary>
        /// <value>The contracting party id.</value>
        [DataMember]
        public int ContractingPartyId { get; set; }

        /// <summary>
        /// Contracting Parties Related
        /// </summary>
        /// <value>The contracting party.</value>
        [DataMember]
        public string ContractingParty { get; set; }


        [DataMember]
        public byte? WorkflowStatusId { get; set; }

        /// <summary>
        /// Workflow status of Contract, From Business
        /// </summary>
        /// <value>The workflow status.</value>
        [DataMember]
        public string WorkflowStatus { get; set; }

        /// <summary>
        /// Default value of Contract Status id is null
        /// </summary>
        /// <value>The contract status id.</value>
        [DataMember]
        public byte? ContractStatusId { get; set; }

        /// <summary>
        /// Gets or sets the contract status.
        /// </summary>
        /// <value>The contract status.</value>
        [DataMember]
        public string ContractStatus { get; set; }


        /// <summary>
        /// Gets or sets the umg signing company.
        /// </summary>
        /// <value>The umg signing company.</value>
        [DataMember]
        public string UmgSigningCompany { get; set; }



        /// <summary>
        /// Gets or sets the rights type id.
        /// </summary>
        /// <value>The rights type id.</value>
        [DataMember]
        public byte? RightsTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the rights type.
        /// </summary>
        /// <value>The name of the rights type.</value>
        [DataMember]
        public string RightsTypeName { get; set; }
    }
}
