/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : LinkedContractDetails.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 03/10/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
            
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for LinkedContractDetails 
                  
****************************************************************************/
using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
   [DataContract]
   [Serializable]
    public class LinkedContractDetails : EntityInformation
    {
        
        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
       [DataMember]
       public long? ContractId { get; set; }


        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
       [DataMember]
       public string ArtistName { get; set; }

       /// <summary>
       /// Gets or sets the contract description.
       /// </summary>
       /// <value>The contract description.</value>
       [DataMember]
       public string ContractDescription { get; set; }

       /// <summary>
       /// Gets or sets the contracting party.
       /// </summary>
       /// <value>The contracting party.</value>
       [DataMember]
       public string ContractingParty { get; set; }

       /// <summary>
       /// Gets or sets the commencement date.
       /// </summary>
       /// <value>The commencement date.</value>
       [DataMember]
       public string CommencementDate { get; set; }

       /// <summary>
       /// Gets or sets the clearance company country id.
       /// </summary>
       /// <value>The clearance company country id.</value>
       [DataMember]
       public string ClearanceCompanyCountryId { get; set; }


       /// <summary>
       /// Gets or sets the clearance company country.
       /// </summary>
       /// <value>The clearance company country.</value>
       [DataMember]
       public string ClearanceCompanyCountry { get; set; }
    }
}
