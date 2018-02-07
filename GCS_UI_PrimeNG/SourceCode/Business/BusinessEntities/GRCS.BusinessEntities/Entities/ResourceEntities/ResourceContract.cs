using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using UMGI.GRCS.Resx.Resource.EntityResource;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    [DataContract]
    [Serializable]
    public class ResourceContract :ArtistContract
    {
        /// <summary>
        /// Gets or sets the isrc.
        /// </summary>
        /// <value>The isrc.</value>
        [DataMember]
        [Display(Name = "IsrcId", ResourceType = typeof(Entity))]
        public string Isrc { get; set; }

        /// <summary>
        /// Gets or sets the Resource Id.
        /// </summary>
        /// <value>Resource Id</value>
        [DataMember]
        [Display(Name = "ResourceId", ResourceType = typeof(Entity))]
        public long ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the R2Resource Id.
        /// </summary>
        /// <value>R2Resource Id</value>
        [DataMember]
        [Display(Name = "R2ResourceId", ResourceType = typeof(Entity))]
        public long? R2ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the resource title.
        /// </summary>
        /// <value>The resource title.</value>
        [DataMember]
        [Display(Name = "ResourceTitle", ResourceType = typeof(Entity))]
        public string ResourceTitle { get; set; }

        /// <summary>
        /// Gets or sets the version title.
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        [Display(Name = "ResourceVersionTitle", ResourceType = typeof(Entity))]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the Rights Type.
        /// </summary>
        /// <value>The Rights Type.</value>
        [DataMember]
        [Display(Name = "RightsType", ResourceType = typeof(Entity))]
        public int RightsType { get; set; }

        /// <summary>
        /// Gets or sets the ResourceTypeId.
        /// </summary>
        /// <value>The ResourceTypeId.</value>
        [DataMember]
        [Display(Name = "ResourceTypeId", ResourceType = typeof(Entity))]
        public int ResourceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        [Display(Name = "ResourceType", ResourceType = typeof(Entity))]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the Rights Type.
        /// </summary>
        /// <value>The Rights Type.</value>
        [DataMember]
        [Display(Name = "RightsTypeName", ResourceType = typeof(Entity))]
        public string RightsTypeName { get; set; }
    }
}
