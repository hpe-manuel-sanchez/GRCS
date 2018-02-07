using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// enumeration for GCSTasks
    /// These tasks are configured in ANA for GCS application
    /// </summary>
    [DataContract(Name = "CSPermissionType")]
	//[Flags]
	[Serializable]
    public enum GcsTasks : long
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        CreateParentWorkgroup = 1,

        [EnumMember]
        MaintainParentWorkgroup = 2,

        [EnumMember]
        CreateChildWorkgroup = 3,

        [EnumMember]
        MaintainChildWorkgroup = 4,

        [EnumMember]
        DeactivateParentWorkgroup = 5,

        [EnumMember]
        DeactivateChildWorkgroup = 6,

        [EnumMember]
        DeleteParentWorkgroup = 7,

        [EnumMember]
        DeleteChildWorkgroup = 8,

        [EnumMember]
        WorkgroupDeviation = 9,

        [EnumMember]
        ClrProjectCreation = 10,

        [EnumMember]
        ClrProjectSubmission = 11,

        [EnumMember]
        DeleteClrProject = 12,

        [EnumMember]
        MapR2ProjectAndClrProject = 13,

        [EnumMember]
        UserTransfer = 14,

        [EnumMember]
        PushtoR2ForProductcreation = 15,

        [EnumMember]
        ModifyAndResubmitClrProject = 16,

        [EnumMember]
        CancelClrProject = 17,

        [EnumMember]
        CancelResourceInClrProject = 18,

        [EnumMember]
        ReinstateCancelledResources = 19,

        [EnumMember]
        ReinstateCancelledproject = 20,

        [EnumMember]
        ReapplyRejectedClearanceRequest = 21,

        [EnumMember]
        ProjectCompletion = 22,

        [EnumMember]
        ReopenCompletedProject = 23,

        [EnumMember]
        LinkingFreehandCompanyAndResource = 24,

        [EnumMember]
        UpCallocation = 25,

        [EnumMember]
        ClearanceProjectInquiry = 26,

        [EnumMember]
        RoutingVariationsMatrix = 27,

        [EnumMember]
        CreateTemplateForEmailsandClrProject = 28,

        [EnumMember]
        RemainderMails = 29,

        [EnumMember]
        ApproveClearanceRequest = 30,

        [EnumMember]
        ConditionallyApproveClearanceRequest = 31,

        [EnumMember]
        RejectClearanceRequest = 32,

        [EnumMember]
        DispatchClearanceRequest = 33,

        [EnumMember]
        LinkingUnlinkingRepertoireInGcSbyLegalReview = 34,

        [EnumMember]
        UnlinkingRepertoireInGcSbylegalReviewer = 35,

        [EnumMember]
        ArtistConsent = 36,

        [EnumMember]
        AddtopRecleared = 37,

        [EnumMember]
        ViewpriorApprovalofRequest = 38,

        [EnumMember]
        RouteOrphanRequest = 39,

        [EnumMember]
        RouteOneStopRequest = 40,

        [EnumMember]
		CreateAndMaintainSafeTerritories = 41,

        [EnumMember]
        UnlockProjects = 42,

        [EnumMember]
        MimicRoleofRequestorAndReviewer = 43,

        [EnumMember]
        CreationofclrRoutingcontract = 44,

        [EnumMember]
        SearchAndPrint = 45,

        [EnumMember]
        UserPreference = 46,

        [EnumMember]
        RevViewContracts = 47,

        [EnumMember]
        RevAssignedToRequestLevel = 48,

        [EnumMember]
        RevManageContract = 49,

        [EnumMember]
        RevReviewCommentsRequest = 50,

        [EnumMember]
        RevReviewCommentsProjectLevel = 51,

        [EnumMember]
        RevViewHistory = 52,

        [EnumMember]
        RevViewReInstateRequest = 53,

        [EnumMember]
        RevArtistConsent = 54,

        [EnumMember]
        RevUndoArtistConsent = 55,

        [EnumMember]
        RevRouteToRCCAdmin = 56,

        [EnumMember]
        RevAssignedToProjectLevel = 57,

        [EnumMember]
        RevApprove = 58,

        [EnumMember]
        RevConditionallyApprove = 59,

        [EnumMember]
        RevReject = 60,

        [EnumMember]
        RevDispatch = 61,

        // Task Count -61
        [EnumMember]
        RevProjectLevelAction = 62,

		[EnumMember]
        ManageRejectReason = 63,

		[EnumMember]
		AutomaticUPCAllocation = 64,

		[EnumMember]
		ManualUPCAllocation = 65,

		[EnumMember]
		RemoveUPC = 66,

		[EnumMember]
		ManageSafeTerritories = 67,

		[EnumMember]
		DeactivateWorkgroup = 68,

		[EnumMember]
		DeleteWorkgroup = 69,

		[EnumMember]
		ManageUserWorkgroups = 70,

		[EnumMember]
		MimicUser = 71,

		[EnumMember]
		LinkFreeHandResource = 72,

        [EnumMember]
        clrCreateNewFromExisting = 73,

        [EnumMember]
        PushToR2FirstPush = 74,

        [EnumMember]
        PushToR2SubsequentPush = 75,

        [EnumMember]
        MoveToResearchFolder = 76,
        
        [EnumMember]
        MoveToInternalReviewFolder = 77,
        
        [EnumMember]
        MoveToSideArtistSample = 78,

        [EnumMember]
        UndoMoveToResearchFolder = 79,
        
        [EnumMember]
        UndoMoveToInternalReviewFolder = 80,
        
        [EnumMember]
        UndoMoveToSideArtistSample = 81,

        [EnumMember]
        AssignedTo = 82
        
    }
}