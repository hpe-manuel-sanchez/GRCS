using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{

    /// <summary>
    /// RightsDefaultRepertoireCriteria
    /// </summary>
    [DataContract]
    public class RightsDefaultRepertoireCriteria : EntityInformation
    {
        public RightsDefaultRepertoireCriteria()
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
        /// <value>List of countryIds  from ANA - Dag Type: Country/Company </value>
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
        /// Gets or sets the type 
        /// </summary>
        /// <value>The type of the review status.</value>
        [DataMember]
        public byte? ReviewStatusType { get; set; }


        /// <summary>
        /// Gets or sets the is digital 
        /// </summary>
        /// <value>The is digital unbundle.</value>
        [DataMember]
        public bool? IsDigitalUnbundle { get; set; }


        /// <summary>
        /// Gets or sets the is physical 
        /// </summary>
        /// <value>The is physical rights.</value>
        [DataMember]
        public bool? IsPhysicalRights { get; set; }

        /// <summary>
        /// Gets or sets the is physical 
        /// </summary>
        /// <value>The is physical rights.</value>
        [DataMember]
        public bool? IsDigitalExploitation { get; set; }
    }

}
