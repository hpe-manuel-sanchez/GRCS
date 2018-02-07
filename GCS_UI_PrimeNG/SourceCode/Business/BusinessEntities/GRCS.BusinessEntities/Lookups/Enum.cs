/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : DigitalRestrictionEnum.cs
 * Project Code :   
 * Author       : Rengaraj G
 * Created on   : 16 Aug 2012 
 * Description  : Decalare all enum used in contract  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Declare Enum for lookups

****************************************************************************/

using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Lookups
{

    #region "Digital Restriction"

    /// <summary>
    /// Declare All Enum for DigitalRestriction Enum
    /// </summary>
    public enum DigitalRestrictionEnum
    {
        #region "Content Type Enum"

        /// <summary>
        /// Declare Enum for Audio
        /// </summary>
        [Description("Audio")]
        Audio = 0,

        /// <summary>
        /// Declare Enum for Video
        /// </summary>
        [Description("Video")]
        Video = 1,

        /// <summary>
        /// Declare Enum for Image
        /// </summary>
        [Description("Image")]
        Image = 2,

        #endregion "Content Type Enum"

        #region "Use Type Enum"

        /// <summary>
        /// Declare Enum for Streaming
        /// </summary>
        [Description("Streaming")]
        StreamingType = 3,

        /// <summary>
        /// Declare Enum for Download
        /// </summary>
        [Description("Download")]
        DownloadType = 4,

        /// <summary>
        /// Declare Enum for Digital Merchandise
        /// </summary>
        [Description("Digital Merchandise")]
        DigitalMerchandiseType = 5,

        #endregion "Use Type Enum"

        #region "Commercial Model Enum"

        /// <summary>
        /// Declare Enum for Ad-funded
        /// </summary>
        [Description("Ad-funded")]
        Adfunded = 6,

        /// <summary>
        /// Declare Enum for Subscription
        /// </summary>
        [Description("Subscription")]
        Subscription = 7,

        /// <summary>
        /// Declare Enum for Ala carte
        /// </summary>
        [Description("A la carte")]
        Alacarte = 8

        #endregion "Commercial Model"
    }

    #endregion "Digital Restriction"

    #region MasterTypes
    public enum MasterTypes
    {
        Actin = 1, // Action Performed
        Asign = 2, // Assigned
        Cnsnt = 3, // Consent Period Type
        Comme = 4, // Commercial Model Type
        Contn = 5, // Content Type

        [Description("Contract Status Type")]
        Ctrct = 6,

        Entry = 7, //Entry Type
        Explt = 8, //Exploitation Type
        Incld = 9, //Included Type
        Itemt = 10, //Item Type
        Linkt = 11, //Link Type
        Logst = 12, //Log Status
        Lostr = 13, //Lost Right Reason Type
        Precr = 14, //Preclearence Type
        Reasn = 15, //Reason type
        Reviw = 16, //Review Status Type
        Rgtdf = 17, //Rights Default Type
        Right = 18, //Right Acquired Type
        Rstrn = 19, //Restriction Type
        Rtprd = 20, //Right Period Type
        Stats = 21, //Status Type
        Taskt = 22, //Task Type
        Usety = 23, //Use Type
        Wrkfw = 24, //Workflow Status Type
        Logty = 25, //Log Type
        Acpat = 26, //Active Passive Type
        Artst = 27, //Artist Type
        Cdest = 28, //Contract Description Type
        Revcn = 33 // Review_Condition
    }
    #endregion

    #region Audit Trail

    public enum AuditObjectType
    {
        ContractAuditHistory = 1,
        ReleaseRightsAuditHistory = 2,
        ResourceAndTracksRightsAuditHistory = 3,
        CompanyDivisionLabelMappingWithContract = 4,
        WorkgroupAuditHistory = 5,
        ManageSafeTerritoriesAuditHistory = 6,
        RoutingVariationAuditHistory = 7,
        MasterProjectProjectAuditHistory = 8,
        MasterProjectResourceAuditHistory = 9,
        MasterProjectResourceFreehandAuditHistory = 10,
        RegularNonTraditionalProjectProjectAuditHistory = 11,
        RegularNonTraditionalProjectRequestTypeAuditHistory = 12,
        RegularNonTraditionalProjectReleaseExistsAuditHistory = 13,
        RegularNonTraditionalProjectReleaseNewAuditHistory = 14,
        RegularNonTraditionalProjectResourceAuditHistory = 15,
        RegularNonTraditionalProjectResourceFreehandAuditHistory = 16,
        TrackRepertorie = 17,
        CompanyContractMapping = 18


    }

    #endregion

    #region Clearance Inbox

    public enum RoleGroup
    {
        [Description("None")]
        None = 0,
        [Description("Reviewer")]
        Reviewer = 1,
        [Description("RCCAdmin")]
        RCCAdmin = 2,
        [Description("Requestor")]
        Requestor = 3
    }

    public enum RoleType
    {
        LocalLabelReviewer = 1,
        NationalMarketingReviewer = 2,
        NationalLegalReviewer = 3,
        InternationalMarketingReviewer = 4,
        InternationalLegalReviewer = 5,
        UMGIMarketingReviewer = 6,
        UMGIGlobalClearance = 7,
        Requestor = 8,
        Inquiry = 9,
        RCCAdmin = 10
    }

    public enum SystemFolder
    {
        ReviewerHighPriority = 1,
        ReviewerPending = 2,
        ReviewerArtistConsent = 3,
        RCCAdminHighPriority = 4,
        RCCAdminOrphan = 5,
        RCCAdminOneStop = 6,
        RequestorHighPriority = 7,
        RequestorSubmitted = 8,
        RequestorUnsubmitted = 9,
        RequestorReopened = 10
    }

    public enum FolderAction
    {
        Create = 1,
        Edit = 2,
        Delete = 3
    }

    #endregion Clearance Inbox

    #region Routing Enums Types

    public enum SalesChannelsRoutedResoruce : int
    {
        Regular = 4,
        Club = 5
    }

    public enum RightsType : byte
    {
        OwnedByUMG = 1,
        ExclusiveLicense = 2,
        NonUMG = 3,
        JointVenture = 4,
        NonExclusiveLicense = 5,
        IntercompanyLicense = 6
    }

    public enum ContractStatusType : byte
    {
        ClearanceRoutingContract = 3
    }

    /// <summary>
    /// Lookup_Type = EXPLT
    /// Lookup_Type_ID = 8
    /// </summary>
    public enum ExploitationType : byte
    {
        MultiArtistCompilations = 1,
        Synchronisation = 2,
        MidPrice = 3,
        Budget = 4,
        Premiums = 5,
        SampleUse = 6,
        AdvertRoyaltyBreaks = 7,
        ClubMailOrder = 8,
        RemixEdit = 9,
        GreatestHits = 10
    }

    /// <summary>
    /// Lookup_Type = RSTRN
    /// Lookup_Type_ID = 19
    /// </summary>
    public enum RestrictionType : byte
    {
        NoRights = 1,
        ConsentRequired = 2,
        ReferToLegal = 3,
        NoRestriction = 4,
        Consult = 5
    }


    public enum ConsentPeriodType : byte
    {
        DuringTerm = 2
    }

    public enum QueueStatus : byte
    {
        None = 0,
        Queued = 1,
        InProgress = 2,
        Completed = 3,
        Error = 4,
    }

    #endregion Routing Enums Types

    public enum ProjectType_ForEmail : int
    {
        Master = 1,
        Regular_Existing = 2,
        Regular_New = 3,
    }

    public enum Preclearance_Territory : byte
    {
        Excluded = 2
    }

    public enum Lookup_Type : byte
    {
        ROLET = 29 // Role Type
    }

    public enum Role_Group_Type : byte
    {
        Reviewer = 1,
        Requestor = 2,
        RCCAdmin = 3
    }

}