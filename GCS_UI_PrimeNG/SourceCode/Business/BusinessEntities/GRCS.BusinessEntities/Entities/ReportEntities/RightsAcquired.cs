using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{
    [DataContract]
    public class RightsAcquired : EntityInformation
    {
        /// <summary>
        /// Gets or sets the UPC .
        /// </summary>
        [DataMember]
        public string UPC { get; set; }

        /// <summary>
        /// Gets or sets the Configuration .
        /// </summary>
        [DataMember]
        public string Configuration { get; set; }

        /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
        [DataMember]
        public string ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the VersionTitle .
        /// </summary>
        [DataMember]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the ResourceType .
        /// </summary>
        [DataMember]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the RightsType .
        /// </summary>
        [DataMember]
        public string RightsType { get; set; }

        /// <summary>
        /// Gets or sets the RepertoireType .
        /// </summary>
        [DataMember]
        public string RepertoireType { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        [DataMember]
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceAdminCompany .
        /// </summary>
        [DataMember]
        public string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Territorial Right .
        /// </summary>
        [DataMember]
        public string TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the Territorial_Right_ToolTip .
        /// </summary>
        [DataMember]
        public string TerritorialRightToolTip { get; set; }

        /// <summary>
        /// Gets or sets the PhysicalExploitationrights .
        /// </summary>
        [DataMember]
        public string PhysicalExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the DigitalExploitationrights .
        /// </summary>
        [DataMember]
        public string DigitalExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the MobileExploitationrights .
        /// </summary>
        [DataMember]
        public string MobileExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the DigitalExploitationRestrictions .
        /// </summary>
        [DataMember]
        public string DigitalExploitationRestrictions { get; set; }

        /// <summary>
        /// Gets or sets the ContentType .
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the USEType .
        /// </summary>
        [DataMember]
        public string USEType { get; set; }

        /// <summary>
        /// Gets or sets the ComercialModel .
        /// </summary>
        [DataMember]
        public string ComercialModel { get; set; }

        /// <summary>
        /// Gets or sets the Notes .
        /// </summary>
        [DataMember]
        public string Notes { get; set; }
        /// <summary>
        /// Gets or sets the flag for  Total Result Count .
        /// </summary>
        /// <value>The flag for Total Result Count.</value>
        [DataMember]
        public int Total { get; set; }
    }
}
