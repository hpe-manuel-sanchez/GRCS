/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractMappings.cs 
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
 * Description :  Defines the ContractMappings CDL-Contract Entities
 
****************************************************************************/

using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    [DataContract]
    public class ContractMappingDetails : ContractMapping
    {
        /// <summary>
        /// Company Name of CDL combination 
        /// </summary>
        [DataMember]
        public string Company { get; set; }

        /// <summary>
        /// Division Name of CDL combination 
        /// </summary>
        [DataMember]
        public string Division { get; set; }

        /// <summary>
        /// Label Name of CDL combination 
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>The artist.</value>
        [DataMember]
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the commencement date.
        /// </summary>
        /// <value>The commencement date.</value>
        [DataMember]
        public DateTime CommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the contracting party.
        /// </summary>
        /// <value>The contracting party.</value>
        [DataMember]
        public string ContractingParty { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company.
        /// </summary>
        /// <value>The clearance admin company.</value>
        [DataMember]
        public string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the territorial rights definition.
        /// </summary>
        /// <value>The territorial rights definition.</value>
        [DataMember]
        public string TerritorialRightsDefinition { get; set; }

        /// <summary>
        /// Gets or sets the contract description.
        /// </summary>
        /// <value>The contract description.</value>
        [DataMember]
        public string ContractDescription { get; set; }

        /// <summary>
        /// Gets or Sets the CountryId
        /// </summary>
      // TODO:Need to check whether we use this CountryID
        [DataMember]
        public long CountryId { get; set; }

        /// <summary>
        /// Gets or Sets the CountryName
        /// </summary>
        // TODO:Need to check whether we use this CountryName
        [DataMember]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the country iso code.
        /// </summary>
        /// <value>The country iso code.</value>
        [DataMember]
        public string CountryIsoCode { get; set; }
    }
}
