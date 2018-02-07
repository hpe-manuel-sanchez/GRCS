using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    public class ArtistSearchCriteria : RepertoireSearchCriteriaBase
    {
        /// <summary>
        /// Artists First Name
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Artists Last Name / Group Name
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// check search name used in all name fields
        /// </summary>
        [DataMember]
        public bool IsSearchNameInAllNameFields { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company name.
        /// </summary>
        /// <value>The clearance admin company name.</value>
        [DataMember]
        public string ClearanceAdminCompanyName { get; set; }
    }
}
