using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2ReleaseAdditionalInfo : R2ServiceAdditionalInfo
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        [DataMember]
        public string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the package info.
        /// </summary>
        /// <value>The package info.</value>
        [DataMember]
        public List<PackageInfo> PackageInfo { get; set; }

        /// <summary>
        /// Gets or sets the track info.
        /// </summary>
        /// <value>The track info.</value>
        [DataMember]
        public List<TrackInfo> TrackInfo { get; set; }

        [DataMember]
        public List<R2ReleaseResources> ReleaseResources { get; set; }

        [DataMember]
        public string RightsType;

    }


}
