/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : EmailType.cs
 * Project      : UMG GRCS
 * Author       : Arunagiri G
 * Created on   : 27-02-2013 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Declare EmailType Enum

****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Lookups
{
    [DataContract]
    public enum EmailType
    {
        R2ProjectCreated = 1,
        R2ReleasePushFailed = 2,
        R2ResoucePushFailed = 3,
        CreateCompanyByGta = 4,
        FreeHandResourceApproved = 5,
        ResourceCompletenessCheckFailed = 6,
        UPCReducedBelow300 = 7,
        RequestTypeRemoved = 8,
        ProjectCompletedAutomatically = 9,
        ProjectCompletedManually = 10,
        ProjectCancelledForPendingUsers = 11,
        RequestPendingInArtistConsent = 12,
        ResourceCancelled = 13,
        ProjectCompletedAutomaticallyAndApproved = 14,
        ProjectCancelledForApprovedUsers = 15,
        ResourceExcluded = 16,
        ResourceIncluded = 17,
        ProjectReInstatedForApprovedUsers = 18,
        ResourceAutoRejected = 19,
        ResourceAutoApproved = 20,
        ResourceReInstated = 21,
        ProjectReInstatedForPendingUsers = 22,
        RequestReApplied = 23,
        RequestRemainder = 24,
        ApprovalPendingForRequest = 25,
        UmgiApprovalPendingForRequest = 26,
        RequestReviewCompleted = 27,
        ArtistManagementFlagUpdated = 28,
        InterNationalMarketingSkipped = 29,

        General = 30,
        UPCAllocation = 31,
        Master = 32,
        TVRadioBreakICLA = 33,
        RegularNonTraditionalNonTVRadioBreak = 34,

        RoutingFailed = 35,
        AutoApprovedMAEmail = 36,

        MasterWithReviewStatus = 37,
        RegularNonTraditionalNonTVRadioBreakWithReviewStatus = 38,
        RegularNonTraditionalNewArtistManagement = 39,
        RegularNonTraditionalExistingArtistManagement = 40,
        MasterArtistManagement = 41,
        MasterArtistManagementWithSelectedResources = 42,
        RegularNonTraditionalNewArtistManagementWithSelectedResources = 43,
        ResourceAutoApprovedPreCleared = 44,
        ReminderRegularNonTraditionalNew = 45,
        ReminderRegularNonTraditionalExisting = 46,
        ReminderMaster = 47,
        ResourceCancelledAutomatic = 48,

        RegularNonTraditionalArtistManagement = 99 //Generic Id for New and Regular ReleaseType.

    }
}
