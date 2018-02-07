/* ************************************************************************ 
* Copyrights ® 2012 UMGI 
* ************************************************************************ 
* File Name    : RightsExpiryReportParameter.cs 
* Project Code : UMG-GRCS(C/115921) 
* Author       : Rengaraj G
* Created on   : 29-10-2012
* ************************************************************************ 
* Modification History 
* ************************************************************************ 
* Modified by       Modified on     Remarks 

*************************************************************************** 
* Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Rights Data Review criteria
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{

    /// <summary>
    /// Rights DataReview Search Criteria
    /// </summary>
    [DataContract]
    public class RightsDataReviewCriteria : EntityInformation
    {
       public RightsDataReviewCriteria()
       {
           Criteria = new FilterFields();
           rightsCountry = new RightsCountry();
       }

        /// <summary>
        /// Gets or sets the release date Country id.
        /// </summary>
        /// <value>CountryId</value>
        [DataMember]
        public long? CountryId { get; set; }

         /// <summary>
        /// Gets or sets the release date RoleGroup Id
        /// </summary>
        /// <value>CompanyId </value>
        [DataMember]
        public long? CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the countryIds
        /// </summary>
        /// <value>List of countryIds  from ANA - Dag Type: Country </value>
        [DataMember]
        public List<long> Ids { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        [DataMember]
        public FilterFields Criteria { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>The rights country.</value>
        [DataMember]
        public RightsCountry rightsCountry { get; set; }

       /// <summary>
       /// Type
       /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// ResourceId
        /// </summary>
        [DataMember]
        public long ResourceId { get; set; }


        /// <summary>
        /// Contractid
        /// </summary>
        [DataMember]
        public long Contractid { get; set; }


    }
}
