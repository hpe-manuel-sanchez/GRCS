namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// Provides an object representation of Resource Track Right
    /// </summary>    
    public class MPTrackRights
    {
        /// <summary>
        ///  Gets or Sets the Album Right Existence
        /// </summary>        
        public bool IsAlbumOnly { get; set; }

        /// <summary>
        ///  Gets or Sets the Download Right Existence
        /// </summary>        
        public bool IsDownload { get; set; }

        /// <summary>
        ///  Gets or Sets the Streamable Right Existence
        /// </summary>        
        public bool IsStreamable { get; set; }

        /// <summary>
        ///  Gets or Sets the MakeOwnRingtone Right Existence
        /// </summary>        
        public bool IsMakeOwnRingtone { get; set; }

        /// <summary>
        /// Gets or Sets the Territory/Country Iso code
        /// </summary>        
        public string CountryISOCode { get; set; }
    }
}
