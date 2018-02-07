
namespace UMGI.GRCS.BusinessEntities.Constants
{
    public static class GrsErrorCode
    {
        #region General
        public const string ConfigValueNotFound = "30001";
        public const string ComponentPathNullOrEmpty = "30002";
        public const string ConcurrencyErrorCode = "C0001";

        #endregion

        # region UC001

        public const string UnauthorizedUser = "30101";
        public const string PowerUserRccUserError = "30102";
        # endregion

        # region  UC002

        public const string SaveContract = "30205";
        public const string GetContract = "30206";
        public const string EditContract = "30207";
        public const string DeleteContract = "30208";
        public const string ContractRetrieveFail = "30209";
        public const string SearchContract = "30210";
        public const string SaveTerritory = "30211";
        public const string SecExploitations = "30212";
        public const string GetTerritory = "30213";
        public const string GetMasterData = "30214";
        public const string PcNoticeCompany = "30215";
        public const string RightsRestriction = "30214";
        public const string DigitalRestriction = "30215";
        public const string SaveDigitalRestriction = "30216";
        public const string SplitDealContract = "30217";
        public const string AcquiredRights = "30218";
        public const string Deal360Rights = "30219";
        public const string DuplicateContract = "30220";
        public const string GetRestrictionTemplate = "30221";
        public const string RightsTable = "30222";
        public const string FilteredContracts = "30223";
        public const string GetOtherRights = "30224";
        public const string InvalidContractId = "30225";
        public const string GetChildContracts = "30226";
        public const string SaveRightSetError = "30227";
        public const string InvalidWorkflowStatusId = "30228";
        public const string GetArtists = "30229";
        public const string LoadTerritoryData = "30230";
        public const string GetContractBatchProcess = "30231";
        public const string SaveRightSetEditedError = "30232";
        public const string EditCopyAuthozError = "151188";


        # endregion

        # region  UC006

        public const string ViewContract = "30602";
        public const string CopyContract = "30103";
        public const string ErrorinGetContractEntities = "30604";
        # endregion

        # region UC009

        public const string ContractTemplates = "30903";
        public const string DigitalTemplates = "30904";
        public const string Deletetemplates = "30905";
        public const string SaveContractTemplates = "30906";
        #endregion

        # region Concurrency

        public const string DeleteContractConc = "78945";
        public const string SaveConcurrency = "12345";
        public const string DeleteConcurrency = "67890";
        public const string RemoveWorkQueueConc = "11245";
        public const string ParentConcError = "45698";
        public const string UnlinkingConcurrency = "00012";
        public const string LinkingConcurrency = "00013";
        public const string WorkFlowStatusConcError = "00123";
        public const string IsExistingContract = "00124";
        public const string SplitDeleteConc = "00125";
        # endregion
        # region UC034

        public const string GetEmailGroups = "33405";
        public const string SaveEmailGroup = "33406";
        public const string DeleteEmailGroup = "33406";
        public const string GetCountries = "33407";
        public const string EmailGroupRetrieveFail = "33408";

        #endregion

        #region UC008

        public const string NotApplicabletoLinkContract = "30801";
        public const string NotApplicabletoChangeLink = "30802";
        public const string NotApplicabletoReroute = "30803";
        public const string NotApplicabletoUnlinkContract = "30804";
        public const string NotApplicabletoReviewRights = "30805";

        //  Failed to retrieve the master data
        public const string GetWorkQueueMasterData = "30806";
        public const string RerouteResource = "30807";
        public const string RerouteResourceInvalidRoute = "30808";
        public const string RemoveWorkQueueItems = "30809";
        public const string GetWorkQueueItems = "30810";
        public const string ReviewRights = "30811";
        public const string DeleteNotificationPriorityWorkQueue = "30812";
        public const string LoadMatch = "30813";
        public const string RepertoireReviewRights = "30814";

        #endregion


        #region UC007


        public const string ErrorGetConfigurationGroup = "30701";
        public const string ErrorGetResourceType = "30702";
        public const string ErrorGetLabelInfo = "30703";
        public const string GetAssociatedRelease = "30704";
        public const string ProjectSearch = "30705";
        public const string GetAssociatedResource = "30706";
        public const string GetAssociatedReleaseResource = "30707";
        public const string ReleaseSearch = "30708";
        public const string ResourceSearch = "30709";
        public const string LinkProjectToContract = "30710";
        public const string GetCountriesForUser = "30711";
        public const string IsContractLinked = "30712";
        public const string GetReleases = "30713";
        public const string GetResources = "30714";
        public const string ErrorGetConfiguration = "30715";
        public const string LinkContractinBackGround = "30716";
        public const string ErrorInProjectManager = "30717";
        public const string ErrorInRepertoireProcessor = "30718";
        public const string GetLinkedProjectDetails = "30719";
        public const string GetLinkedReleaseDetails = "30720";
        public const string GetLinkedResourceDetails = "30721";
        public const string UnlinkRepertoire = "30722";
        public const string UnlinkRepertoireFromWorkQueue = "30723";
        public const string GetCompaniesForUser = "30724";
        public const string UnlinkRepertoireFromReleaseResource = "30725";
        public const string GetUnProcessedReleaseResources = "30726";
        public const string IsAlreadyUnlink = "38965";
        public const string GetLinkedRightSets = "38966";
        #endregion

        #region Common

        public const string ErrorAdapterNull = "30C01";
        public const string UserInfoOrPermissionNull = "30C02";
        public const string GetTaskAndRoles = "00001";
        public const string GetCountriesCommon = "00002";
        public const string GetCompanyCountries = "00006";

        #endregion

        #region UC014

        public const string GetRightCountries = "31401";
        public const string GetRightsRoleGroups = "31402";
        public const string GetRightsExpiryResults = "31403";
        public const string SaveRightsExpiryItem = "31404";
        public const string DeleteRightsExpiryItems = "31405";
        public const string GetRightsDataReviewRules = "31406";
        public const string SaveRightsDataReview = "31407";
        public const string DeleteRightsDataReview = "31408";
        public const string SearchWorkQueueComparisionParameterData = "31409";
        public const string SaveWorkQueueComparisionParameterData = "31410";
        public const string DeleteWorkQueueComparisionParameterData = "31411";
        public const string DeleteRightsDefaultRepertoireData = "31412";
        public const string SaveRightsDefaultRepertoireData = "31413";
        public const string GetRightsDefaultForRepertorieResults = "31414";
        public const string LoadDefaultRightsMasterData = "31415";
        public const string FilterValueIsNull = "31416";
        public const string SaveRightsDataReviewInputValidation = "31417";
        public const string RightsDataReviewRecordExists = "31418";
        public const string CountryAlreadyExists = "31419";
        public const string SearchRightsDataReviewResult = "31420";
        public const string PoPulateCache = "31421";
        
        #endregion

        #region UC016

        public const string GetResourcesBasicSearch = "301601";
        public const string GetResourcesReleaseParameters = "301602";
        public const string GetTracksBasicSearch = "301603";
        public const string GetTracksSearchReleaseParameters = "301604";
        public const string GetResourcesAndTracksBasicSearch = "301605";
        public const string GetResourcesAndTracksSearchReleaseParameters = "301606";
        public const string GetReleasesSearch = "301607";
        public const string GetDigitalRestrictionsResourcesBasicSearch = "301608";
        public const string GetDigitalRestrictionsResourcesReleaseParameters = "301609";
        public const string GetDigitalRestrictionsTracksBasicSearch = "301610";
        public const string GetDigitalRestrictionsTracksSearchReleaseParameters = "301611";
        public const string GetDigitalRestrictionsResourcesAndTracksBasicSearch = "301612";
        public const string GetDigitalRestrictionsResourcesAndTracksSearchReleaseParameters = "301613";
        public const string GetDigitalRestrictionsReleases = "301614";
        public const string GetSecondaryExploitationBasicSearch = "301615";
        public const string GetSecondaryExploitationReleaseParameters = "301616";
        public const string GetPreClearedBasicSearch = "301617";
        public const string GetPreClearedReleaseParameters = "301618";


        public const string GetReasonsForReview = "301619";
        public const string GetPreClearances = "301620";
        public const string GetLostRightsReasons = "301621";
        public const string GetRightsReviewStatuses = "301622";
        public const string GetRightPeriods = "301623";
        public const string ValidateIsrcFromFile = "301624";
        public const string ValidateUpc = "301625";
        #endregion

        #region UC-013

        public const string GetCompanies = "31301";
        public const string GetDivisionList = "31302";
        public const string GetLabelList = "31303";
        public const string SaveCdlContract = "31304";
        public const string GetCdlContracts = "31305";
        public const string DeleteCdlContracts = "31306";
        public const string GetAutoMappedCompanies = "31307";
        public const string GetAutoMappedDivisionList = "31308";
        public const string GetAutoMappedLabelList = "31309";

        #endregion
        public const string FailedToCreateLogInstance = "00003";
        public const string FailedToLoadRequestedAssembly = "00004";
        public const string FailedToConnectService = "00005";
        #region UC-012

        public const string LoadCustomWorkQueueSettings = "31201";
        public const string SaveUserCustomWqConfig = "31202";
        public const string GetDefaultWorkQueueParameters = "31203";


        #endregion

        #region "UC - 017"
       
        public const string CheckReviewCondition = "31701";
        public const string SaveRightsCountry = "31702";
        public const string SaveRightsTerritory = "31703";
        public const string SavePreClearance = "31704";
        public const string SavePreClearanceTerritory = "31705";
        public const string SavePreClearanceCountry = "31706";
        public const string GetDefaultRightDataRepertoireForCompany = "31707";
        public const string GetRightsDataReviewConditions  = "31708";
        public const string SaveReleaseRightsDataSet = "31709";
        public const string SaveReleaseTerritorialRights = "31711";
        public const string GetRightsReviewConditionForCompany = "31712";
        public const string SaveDefaultTrackRights = "31713";
        public const string ProcessDefaultRights = "31714";
        public const string InsertMacReleaseRights = "31715";
        public const string InsertDefaultTrackRights = "31716";
        public const string SaveNonMacReleaseRights = "31717";
        public const string SaveDefaultMacReleaseRights = "31718";
        public const string isGcsRelease = "31719";
        public const string TrackIsExist = "31720";

        #endregion "UC - 017"

        #region Interface Notification
        public const string FailedToProcessProjectNotification = "30I01";
        public const string FailedToProcessReleaseNotification = "30I02";
        public const string FailedToProcessResourceNotification = "30I03";
        
        
        #endregion

        #region UC15
        public const string RollUpRelease = "31501";
        public const string RollUpResource = "31502";
        public const string RepertoireIdNull = "31503";
        public const string ReleaseLevelHierarchyForResource = "31504";
        public const string ReleaseLevelHierarchyForRelease = "31505";
        public const string RollUpRepertoire = "31506";
        public const string GetRepertoire = "31507";
        public const string UpdateRollUpStatus = "31508";
        public const string LoadReleaseLevelHierarchy = "31509";
        public const string RollUpReleaseRight = "31510";

        #endregion

        #region Caching
        public const string GetContractStatus = "00000";
        public const string GetWorkFlowStatus = "00001";
        public const string GetContractDescription = "00002";
        public const string GetRightsPeriods = "00003";
        public const string GetLostRightsReason = "00004";
        public const string GetRightsDefaultTypes = "00005";
        public const string GetUseType = "00006";
        public const string GetContentType = "00007";
        public const string GetCommercialType = "00008";
        public const string GetConsentType = "00009";
        public const string GetRestrictions = "00010";
        public const string GetRightsAndRestrictions = "00011";
        public const string GetSecondaryExploitations = "00012";
        public const string GetDigitalRestrictions = "00013";
        public const string GetDefaultReviewRightsMasterInfo = "00014";
        public const string GetResourceTypes = "00015";
        public const string GetReviewReason = "00016";
        public const string GetReviewStatus = "00017";
        public const string GetReleasePreDefinedParameters = "00018";
        public const string GetResourcePreDefinedParameters = "00019";
        public const string GetClearanceMasterData = "00020";
        public const string GetMatch = "00021";

        #endregion

        #region UC18,20
        public const string GetReleaseRightsWQ = "31801";
        public const string GetResourceRightsWQ = "31802";
        public const string SaveReviewedReleaseRights = "31803";
        public const string SaveReviewedResourceRights = "31804";
        public const string NoRightsDataToUpdate = "31805";
        public const string GetRightsAcquiredMasterData = "31806";
        public const string LoadResourceSecondaryRights = "31807";
        public const string LoadResourcePreClearanceInfo = "31808";
        public const string LoadResourceDigitalRights = "31809";
        public const string LoadSecondaryRightsMasterData = "31810";
        public const string LoadPreClearanceMasterData = "31811";
        public const string LoadDigitalRightsMasterData = "31812";
        public const string GetResourcesPreclearancePredefined = "31813";
        public const string GetResourceSecondaryRightsPredefined = "31814";
        public const string SaveResourceSecondaryRights = "31815";
        public const string SaveResourcePreClearanceRights = "31816";
        public const string LoadReleaseDigitalRights = "31817";
        public const string LoadReleaseDigitalRightsPredefined = "31818";
        public const string SaveReleaseDigitalRights = "31819";
        public const string GetResourceDigitalRightsPredefined = "31820";
        public const string SaveResourceDigitalRights = "31821";
        public const string LoadReviewCondition = "31822";
        public const string AutoSearchReleaseTitle = "31823";
        public const string AutoSearchResourceTitle = "31824";
        public const string AutoSearchReleaseArtist = "31825";
        public const string AutoSearchResourceArtist = "31826";


        
        #endregion


        public const string ErrMsgRetrieveArtist = "Failed to retrieve Artists in GetArtist Method";
        public const string ErrMsgSaveArtist = "Failed to SaveArtist Method";
        public const string ErrMsgUpdateArtist = "Failed to UpdateArtist Method";
        public const string ErrMsgArtistIdNull = " - ArtistId is not available for UpdateArtist Method";
        public const string ErrCodeForArtistData = "001";
        public const string ErrCodeForSaveArtistData = "002";
        public const string ErrCodeForUpdateArtistData = "003";
        public const string ErrCodeNotification = "Failed to Update Notification in NotificationUpdate Method";
        public const string ErrMsgInvokeSheduleActivity = "Failed to Invoke Shedule Activity in InvokeSheduleActivity Method";
        public const string ErrMsgValidateScheduleHour = "Failed to Validate Schedule Hour in ValidateScheduleHour Method";
        public const string ErrMsgValidateScheduleDay = "Failed to Validate Schedule Day in ValidateScheduleDay Method";
        public const string ErrMsgValidateScheduleDate = "Failed to Validate Schedule Date in ValidateScheduleDate Method";
        public const string ErrMsgValidateScheduledYear = "Failed to Validate Scheduled Year in ValidateScheduledYear Method";
        public const string ErrMsgRemoveSchedule = "Failed to Remove All Schedules in RemoveAllSchedules  Method ";
        public const string ErrMsgScheduleTask = "Failed to Schedule Task in ScheduleTask Method ";
        public const string ErrMsgLoggerObjectNull = "Logger object is null!";
        public const string ErrMsgLoadingUserManagerComponents = "Unable to Load user manager or its dependecies";

        public const string ErrorInSaveGCSResourceLinkInfo = "31721";

        #region Reports
        public const string ErrorInReportManager = "32101";
        public const string ErrorInReportConstructor = "32202";
        public const string GetResourceGenre = "32203";
        public const string ErrorInReporDetail = "32204";
        #endregion
    }
}
