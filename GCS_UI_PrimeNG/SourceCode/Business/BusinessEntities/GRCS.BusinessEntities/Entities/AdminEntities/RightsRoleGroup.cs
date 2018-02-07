using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    /// <summary>
    /// RightsRoleGroup
    /// </summary>
    [DataContract]
    public class RightsRoleGroup:EntityInformation
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is added.
        /// </summary>
        /// <value><c>true</c> if this instance is added; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? IsInsertedRoleGroup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is removed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is removed; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool? IsDeletedRoleGroup { get; set; }
    }
}
