/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : LinkedInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 03-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for LinkedInfo Details
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
   [DataContract]
   [Serializable]
   public class LinkedInfo : EntityInformation
    {
        /// <summary>
        /// Gets or sets the Projectid.
        /// </summary>
        /// <value>The Projectid.</value>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the ArtistName.
        /// </summary>
        /// <value>The ArtistName.</value>
        [DataMember]
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the user UserMailId.
        /// </summary>
        /// <value>The user UserMailId.</value>
        [DataMember]
        public string UserMailId { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company id.
        /// </summary>
        /// <value>The clearance admin company id.</value>
        [DataMember]
        public long ClearanceAdminCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the clearnceadmin company.
        /// </summary>
        /// <value>The clearnceadmin company.</value>
        [DataMember]
        public string ClearanceAdminCompany { get; set; }


        /// <summary>
        /// Gets or sets the ISRC.
        /// </summary>
        /// <value>The ISRC.</value>
        [DataMember]
        public string ISRC { get; set; }


        /// <summary>
        /// Gets or sets the UPC.
        /// </summary>
        /// <value>The UPC.</value>
        [DataMember]
        public string UPC { get; set; }


        /// <summary>
        /// Gets or sets the type of the repertoire.
        /// </summary>
        /// <value>The type of the repertoire.</value>
        [DataMember]
        public string RepertoireType { get; set; }


        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        /// <value>The type of the item.</value>
        [DataMember]
        public long? ItemType { get; set; }

        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the contracting party.
        /// </summary>
        /// <value>The contracting party.</value>
        [DataMember]
        public string ContractingParty { get; set; }

        /// <summary>
        /// Gets or sets the filter criteria.
        /// </summary>
        /// <value>The filter criteria.</value>
        [DataMember]
        public FilterFields FilterCriteria { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LinkedInfo"/> is isrelease.
        /// </summary>
        /// <value><c>true</c> if isrelease; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Isrelease { get; set; }
    }
}
