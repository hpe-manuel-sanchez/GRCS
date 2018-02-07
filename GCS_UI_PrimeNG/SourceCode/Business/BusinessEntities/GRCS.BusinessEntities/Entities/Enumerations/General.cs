using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Entities.Enumerations
{
    public class General
    {
        #region InboxFolders
        public enum InboxFolder
        {
            [Description("High Priority")]
            HighPriority = 1,
            Pending = 2,
            ArtistConsent = 3,
            Orphan = 5,
            OneStop = 6,
            Submitted = 8,
            Unsubmitted = 9,
            Reopened = 10,
            Research = 82,
            [Description("Internal Review")]
            InternalReview = 83,
            [Description("Side Artist / Sample")]
            SideArtistSample = 84,
        }
        #endregion

        #region Clearance Project

        public enum ProjectType
        {
            Master = 1,
            Regular
        }

        public enum StatusType
        {
            Unsubmitted = 1,
            Submitted = 2,
            Cancelled = 3,
            Completed = 4,
            ReSubmitted = 5,
            ReOpened = 6,
            ReInstated = 7
        }

        public enum MasterRequestType
        {
            Advertising = 1,
            Film,
            Trailer,
            Other
        }

        public enum MusicClassType
        {
            Classical = 1,
            Pop,
            Jazz,
            Other
        }

        public enum LiveStudioType
        {
            Live = 1,
            Studio
        }

        public enum ResourceType
        {
            Audio = 1,
            Image,
            Merchandise,
            Other,
            Text
        }

        public enum RegularSalesChannelId
        {
            Physical = 1,
            Digitial = 2,
            PhysicalandDigital = 3,
            Regular = 4,
            Club = 5,
            Nontraditional = 6,
            Promotional = 7,
            PriceReduction = 8,
            Master = 9,
            TVPhysical = 10,
            TVALaCarte = 11,
            TVSubscription = 12,
            TVMobileRealTone = 13,
            TVMobileRingBackTone = 14,
            TVStreaming = 15
        }

        public enum PriceLevel
        {
            Top = 1,
            Mid = 3,
            Budget = 2
        }

        public enum RequestTypeValues
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9
        };

        #endregion
    }
}
