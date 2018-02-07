/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : CompanyInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rengaraj          03-Aug-2012     modified code for coding standard format   
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the CompanyInfo entities                                    
                  
****************************************************************************/

using System.Runtime.Serialization;
using System;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// CompanyInfo which is inherited from BaseIdentifier Class
    /// </summary>
    [DataContract]
    [Serializable]
    public class CompanyInfo : EntityInformation
    {
        /// <summary>
        /// Gets or Sets the CompanyId
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Gets or Sets the Company Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the CountryId
        /// </summary>
        [DataMember]
        public long CountryId { get; set; }

        /// <summary>
        /// Gets or Sets the CountryName
        /// </summary>
        [DataMember]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the r2 account id.
        /// </summary>
        /// <value>The r2 account id.</value>
        [DataMember]
        public long? R2AccountId { get; set; }

        /// <summary>
        /// Gets or sets the country iso code.
        /// </summary>
        /// <value>The country iso code.</value>
        [DataMember]
        public string CountryIsoCode { get; set; }

        [DataMember]
        public string GtaStatus { get; set; }

        [DataMember]
        public string ISACCode { get; set; }

        [DataMember]
        public DateTime ModifiedDateTime { get; set; }

        /// <summary>
        ///Base Search Result - PageDetails
        /// </summary>
        [DataMember]
        public FilterFields PageDetails { get; set; }

        [DataMember]
        public string CompanyType { get; set; }

        [DataMember]
        public bool UniversalLicenseeIndicator { get; set; }

    }
}