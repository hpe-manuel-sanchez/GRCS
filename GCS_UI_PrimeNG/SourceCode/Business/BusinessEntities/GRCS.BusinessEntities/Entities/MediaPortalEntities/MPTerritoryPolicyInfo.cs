namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// Provides objet representation of Resource Territory Rights.
    /// </summary>
    public class MPTerritoryPolicyInfo
    {

        /// <summary>
        /// Gets or Sets the Territory/Country Iso code
        /// </summary>       
        public string CountryISOCode { get; set; }

        /// <summary>
        /// Gets or Sets the AdSupportedStreaming Existence
        /// </summary>       
        public bool IsAdSupportedStreaming { get; set; }
    }
}
