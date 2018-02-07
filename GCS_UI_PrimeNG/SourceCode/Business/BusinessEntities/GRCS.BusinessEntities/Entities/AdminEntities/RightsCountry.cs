using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    /// <summary>
    /// RightsCountry
    /// </summary>
    [DataContract]
    public class RightsCountry : EntityInformation
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<long> Ids { get; set; }

        [DataMember]
        public long? ComapnyId { get; set; }

        [DataMember]
        public List<long> CompanyIds { get; set; }
    }
}
