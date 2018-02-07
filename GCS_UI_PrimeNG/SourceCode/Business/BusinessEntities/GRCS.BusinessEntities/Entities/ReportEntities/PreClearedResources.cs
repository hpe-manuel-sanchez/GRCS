using System;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{
    [DataContract]
    public class PreClearedResources : EntityInformation
    {  /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
        /// <value>The ISRC.</value>
        [DataMember]
        public String ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        /// <value>The Title.</value>
        [DataMember]
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the Version_Title .
        /// </summary>
        /// <value>The Version_Title.</value>
        [DataMember]
        public String Version_Title { get; set; }

        /// <summary>
        /// Gets or sets the Resource Type .
        /// </summary>
        /// <value>The Resource Type.</value>
        [DataMember]
        public String ResourceType { get; set; }

        ///// <summary>
        ///// Gets or sets the Resource Type .
        ///// </summary>
        ///// <value>The Resource Type.</value>
        //public IEnumerable<SelectListItem> ResourceTypeList { get; set; }

        /// <summary>
        /// Gets or sets Rights Type .
        /// </summary>
        /// <value>The Rights Type</value>
        [DataMember]
        public String RightsType { get; set; }

        ///// <summary>
        ///// Gets or sets output Rights Type .
        ///// </summary>
        ///// <value>The Rights Type</value>
        //public String Type { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        /// <value>The Artist.</value>
        [DataMember]
        public String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Clearance Admin Company .
        /// </summary>
        /// <value>The Clearance Admin Company.</value>
        [DataMember]
        public String ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the PYear .
        /// </summary>
        /// <value>The PYear.</value>
        [DataMember]
        public String PYear { get; set; }

        /// <summary>
        /// Gets or sets the Resource Genre .
        /// </summary>
        /// <value>The Resource Genre.</value>
        [DataMember]
        public String ResourceGenre { get; set; }

        ///// <summary>
        ///// Gets or sets the Resource Genre .
        ///// </summary>
        ///// <value>The Resource Genre.</value>
        //public IEnumerable<SelectListItem> ResourceGenreList { get; set; }

        /// <summary>
        /// Gets or sets the Territorial Right .
        /// </summary>
        /// <value>The Territorial Right.</value>

        [DataMember]
        public String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the PreClearance Type .
        /// </summary>
        /// <value>The PreClearance Type.</value>
        [DataMember]
        public String PreClearanceType { get; set; }

        /// <summary>
        /// Gets or sets the flag for  Total Result Count .
        /// </summary>
        /// <value>The flag for Total Result Count.</value>
        [DataMember]
        public int Total { get; set; }

        ///// <summary>
        ///// Gets or sets the PreClearance Type .
        ///// </summary>
        ///// <value>The PreClearance Type.</value>
        //public IEnumerable<SelectListItem> PreClearanceTypeList { get; set; }

        ///// <summary>
        ///// Gets or sets the StartDate
        ///// </summary>
        //public DateTime StartDate { get; set; }

        ///// <summary>
        ///// Gets or sets the EndDate
        ///// </summary>
        //public DateTime EndDate { get; set; }       

    }
}
