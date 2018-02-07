using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    /// <summary>
    /// ReviewStatusType class
    /// </summary>
    [DataContract]
    public class ReviewStatusType
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
    }
}
