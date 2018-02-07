namespace UMGI.GRCS.BusinessEntities.Constants
{
    public partial class Constants
    {
        # region App Settings

        /// <summary>
        /// Key to Identify the Log path.
        /// </summary>
        public const string AppsettingsLog4NetConfig = "LOGGING_CONFIG";
        public const string AppSettingsArtistLogConfig = "ARTIST_LOG_PATH";
        public const string AppSettingsContractLogConfig = "CONTRACT_LOG_PATH";
        public const string AppSettingsGlobalLogConfig = "GLOBAL_LOG_PATH";
        public const string AppSettingsPCompanyLogConfig = "PCOMPANY_LOG_PATH";
        public const string AppSettingsProjectLogConfig = "PROJECT_LOG_PATH";
        public const string AppSettingsReleaseLogConfig = "RELEASE_LOG_PATH";
        public const string AppSettingsRepertoireLogConfig = "REPERTOIRE_LOG_PATH";
        public const string AppSettingsResourceLogConfig = "RESOURCE_LOG_PATH";
        public const string AppSettingsRightsLogConfig = "RIGHTS_LOG_PATH";
        public const string AppSettingsUserLogConfig = "USER_LOG_PATH";
        public const string AppSettingsUtilityLogConfig = "UTILITY_LOG_PATH";
        public const string AppSettingsWorkqueueLogConfig = "WORKQUEUE_LOG_PATH";
        public const string AppSettingsWorkflowLogConfig = "WORKFLOW_LOG_PATH";
        public const string AppSettingsSchedulerLogConfig = "SCHEDULER_CONFIG";
        public const string AppSettingsSchedulerInstanceName = "SCHEDULER_INSTANCE";
        public const string AppSettingsGtaLogConfig = "GTA_LOG_PATH";
        public const string AppenderName = "PerfRollingFileAppender";
        public const string AppSettingsWorkgroupLogConfig = "WORKGROUP_LOG_PATH";
        public const string AppSettingsClearanceProjectLogConfig = "CLEARANCEPROJECT_LOG_PATH";
        public const string TvBreakServiceLogConfig = "TV_BREAK_SERVICE_LOG_PATH";
        public const string PushResourceLogConfig = "PUSH_RESOURCE_LOG_PATH";
        public const string ClearanceInboxLogConfig = "CLEARANCE_INBOX_LOG_PATH";
        public const string ClearanceReleaseLogConfig = "CLEARANCE_RELEASE_LOG_PATH";
        public const string ClearanceResourceLogConfig = "CLEARANCERESOURCE_LOG_PATH";
        public const string RoutingLogConfig = "ROUTING_LOG_PATH";

        // Reports
        public const string AppSettingsReportLogConfig = "REPORT_LOG_PATH";
        public const string AppSettingsReportServerUrl = "ReportServerUrl";
        public const string AppSettingsReportUserDomain = "ReportUserDomain";
        public const string AppSettingsReportUserName = "ReportUserName";
        public const string AppSettingsReportUserPassword = "ReportUserPassword";
        public const string AppSettingsReportPath = "ReportPath";
        public const string AppSettingsReportFTPPath = "ReportFTPPath";
        public const string AppSettingsReportFTPDomain = "ReportFTPDomain";
        public const string AppSettingsReportFTPUserName = "ReportFTPUserName";
        public const string AppSettingsReportFTPPassword = "ReportFTPPassword";

        public const string AppSettingsComponentsPath = "COMPONENTS_PATH";
        public const string EmailTemplatePath = "NOTIFICATION_MAIL_TEMPLATE";
        public const string MsmqPath = "MSMQPath";
        public const string MailPathSource = "MailPathSource";
        public const string UPCAllocationEmail = "upcAllocationEmailId";
        public const string AppsettingsError = AppsettingsLog4NetConfigError;

        // Error Messages if App settings not set  MsmqPath
        public const string AppsettingsLog4NetConfigError = "Configuration KEY:{0} not found in {1}. Please Update!";

        public static string AppsettingsUpStreamNotificationQueueReaderTimeDelay = "UPSTREAM_QUEUE_DELAY";

        public static string WorkQueueEntity = "WorkQueueDataEntity";

        public const string AppSettingsNotificationEventQueueConfig = "NotificationEventQueue";

        //  Timeouts
        public const string LongTrasanctionTimeOut = "LongTrasanctionTimeOut";
        public const string MediumTrasactionTimeOut = "MediumTrasactionTimeOut";
        public const string LowTrasanctionTimeOutType = "LowTrasanctionTimeOut";

        public const string AppSettingsGcsSendEmailEnabledConfig = "GCS_SendEmail_Enabled";
        public const string AppSettingsGcsTakeEmailToAddressFromConfigFlagConfig = "GCS_Take_Email_ToAddress_From_Config";
        public const string AppSettingsGcsSendEmailToAddressConfig = "GCS_SendEmail_ToAddress";
        public const string AppSettingsGcsSupportTeamEmail = "GcsSupportTeamEmailId";
        public const string AppSettingsRccAdminTeamEmailId = "RccAdminTeamEmailId";
        # endregion

        # region Method

        // Method Entry/Exit
        public const string MethodStart = "Method Entry: {0}";
        public const string MethodEnd = "Method Exit: {0}";
        public const string MethodDebugInfo = "Debugging Info: Method:{0}, Details:{1}";
        public const string MethodException = "Method:{0}";
        public const string MethodResult = "Result:{0}, Count:{1}";
        public const string MethodParameters = "Parameters {0} ";
        public const string MethodStartWithParameters = "Reached {0} with Parameters {1}";
        public const string MethodEndWithDefaultValue = "Exit {0} With Default value (mostly null)";
        public const string ExceptionWithinMethod = "Exception in Method: {0}";
        public const string ExceptionWithMethod = "Exception in Method: {0} Exception: {1}";
        public const string MethodEndWithResponse = "Exit {0} with Response {1} ";
        public const string MethodParametersMismatch = "\"Method Parameters Mismatch.\"";
        
        # endregion

        # region enum

        public const string EnumError = "Enum Mismatch Occured for enum: {0}, Value: {1}";

        # endregion

        #region WorkflowStatus

        public const string UnderAmendment = "Under Amendment";
        public const string PendingApproval = "Pending Approval";
        public const string Approved = "Approved";

        #endregion WorkflowStatus

        #region Symbols

        public const string OpenSquareBraceValue = " [ ";
        public const string CloseSquareBraceValue = " ] ";
        public const string SemiColonValue = ";";
        public const string CommaValue = ",";
        public const string HyphenValue = "-";
        public const string DoubleBackSlash = "\\";

        #endregion Symbols

        #region Attributes/ Sort fields

        public const string LinkRepertoire = "Link";
        public const string SplitDealCont = "Split";
        public const string ParentContrct = "Parent";
        public const string OnActiveRoster = "OnActiveRoster";
        public const string TerritorialDefinitions = "TerritorialDefinitions";
        public const string RightsPeriod = "RightsPeriod";
        public const string LostRightsDate = "LostRightsDate";
        public const string LostRightsIndicator = "LostRightsIndicator";
        public const string LostRightsReason = "LostRightsReason";
        public const string RightsExpiryRule = "RightsExpiryRule";
        public const string RightsExceptions = "RightsExceptions";
        public const string RightsExceptionApplied = "RightsExceptionApplied";
        public const string RlsCommitmentRightsReversion = "RlsCommitmentRightsReversion";
        public const string GeneralNotes = "GeneralNotes";
        public const string PcNoticeCompanyCountry = "PCNoticeCompanyCountry";
        public const string PcNoticeCompanyExtension = "PCNoticeCompanyExtension";
        public const string LegalRightsReviewRequired = "LegalRightsReviewRequired";
        public const string ActiveforMarketing = "ActiveforMarketing";
        public const string SensitiveArtist = "SensitiveArtist";
        public const string ExactMatch = "Exact Match";
        public const string ClearanceNotes = "ClearanceNotes";
        public const string EmailNotificationRecipients = "EmailNotificationRecipients";
        public const string ContractId = "ContractId";
        public const string ArtistId = "ArtistId";
        public const string ArtistName = "ArtistName";
        public const string ContractingParty = "ContractingParty";
        public const string ClearanceAdminCompanyCountry = "ClearanceAdminCompanyCountry";
        public const string ContractStatus = "ContractStatus";
        public const string WorkflowStatus = "WorkflowStatus";
        public const string ContractDescription = "ContractDescription";
        public const string ContractCommencement = "ContractCommencement";
        public const string EndofTerm = "EndofTerm";
        public const string ContractLastChangeDate = "ContractLastChangeDate";
        public const string ContractCreatedDate = "ContractCreatedDate";
        public const string LocalContractRefNo = "LocalContractRefNo";
        public const string UmgSigningCompany = "UMGSigningCompany";
        public const string RightsType = "RightsType";
        public const string ArtistLocalCharacters = "ArtistLocalCharacters";
        public const string CreatedUser = "CreatedUser";
        public const string ModifiedUser = "ModifiedUser";
        public const string OtherRights = "OtherRights";
        public const string CountryDetails = "CountryDetails";
        public const string PriorityWorkQueueContractStatus = "Clearance Routing Contract";
        public const string PowerUserCheck = "Power User";
        public const string RccUserCheck = "RCC User";
        public const string UnProcessed = "UnProcessed";
        public const string Processed = "Processed";
        public const string Processing = "Processing";
        public const string PcCompanyMismatch = "Contract/Repertoire P/C company mismatch";
        public const string RightsTypeMismatch = "Contract/Repertoire ‘Rights Type’ mismatch";
        public const string ReviewReasonPowerUser = "Linked by Power-user";
        public const string ReviewReasonRccUser = "Linked by RCC User";
        public const string Project = "Project";
        public const string Contract = "Contract";
        public const string Release = "Release";
        public const string Track = "Track";
        public const string Resource = "Resource";
        public const string NoArchiveFlag = "N";
        public const string YesArchiveFlag = "Y";
        public const string Yes = "Yes";
        public const string No = "No";
        public const int ValMinusOne = -1;
        public const int ValZero = 0;
        public const int ValueOne = 1;
        public const int ValueTwo = 2;
        public const int ValueThree = 3;
        public const int ValueFour = 4;
        public const int ValueFive = 5;
        public const int ValueSix = 6;
        public const int ValueSeven = 7;
        public const int ValueEight = 8;
        public const int ValueNine = 9;
        public const int ValueTen = 10;
        public const int ValueEleven = 11;
        public const int NinetyNine = 99;
        public const int ValueHundred = 100;
        public const byte ValueNoChange = 111;
        public const int IncludeType = 1;
        public const int Thousand = 1000;
        public const string PowerUserFromAna = "PowerUser";
        public const string RccUserFromAna = "RCCUSER";
        public const string LegalEditor = "LegalEditor";
        public const string LegalReviewer = "LegalReviewer";
        public const string LegalEditorName = "Legal Editor";
        public const string LegalReviewerName = "Legal Reviewer";
        public const string LocalContractRefNumber = "LocalContractRefNumber";
        public const string ContractCommencementDate = "ContractCommencementDate";
        public const string RightsTypeName = "RightsTypeName";
        public const string TerritorialRightsDefinition = "TerritorialRightsDefinition";
        public const string ClearanceCompanyCountry = "ClearanceCompanyCountry";
        public const string CustomWqSetting = "CustomWorkQueueSetting";
        public const string IsOriginalIndicator = "Y";
        public const string IsRightsType = "NONEXC";
        public const string HasSample = "Has Sample";
        public const string HasSideArtist = "Has Side Artist";
        public const string ContractRightsExceptions = "contract Rights Exceptions not applied";
        public const string AutoLinkDuringClearanceProcess = "Auto Linked During the Clearance Process";
        public const string SplitdealAutolinked = "Split deal - Auto linked";
        public const string DigitalRestrictionAgainstanyexploitationTypes = "Digital restriction against any exploitation types";
        public const string Userinfo = "userInfo";
        public const string AdminCompanyId = "AdminCompanyId";
        public const string ProjectCode = "ProjectCode";
        public const string Title = "Title";
        public const string ProjectId = "ProjectId";
        public const string ReleaseTitle = "ReleaseTitle";
        public const string BlankValue = "[blank]";
        public const string Upc = "Upc";
        public const string VersionTitle = "VersionTitle";
        public const string ResourceTitle = "ResourceTitle";
        public const string Isrc = "Isrc";
        public const string NoRights = "Contract is not having Rights";
        public const string MediaPortal = "MP";
        public const string ContractDeleted = "Contract doesn't exist/ deleted";
        public const string Assign = "RDSRC";
        public const string RightSourType = "RTSRC";
        public const string ResourceSample = "Resource has a Sample";
        public const string ResourceSideArtist = "Resource has a Side-Artist";
        public const string SearchArtist = "SearchArtist:{0}";
        public const string ArtistInfo = "ArtistInfo:{0}";
        public const string TalentId = "R2TalentNameId:{0}";
        public const string XmlData = "xmlData:{0}";
        public const string TalenIdNull = "TalentName Id is null";
        public const string StringValueTen = "10";
        public const string StringValueEleven = "11";
        public const string ActiveStatus = "A";
        public const string Club = "Club";
        public const string Nil = "Nil";

        #endregion Attributes/ Sort fields

        #region Music Type
        public const string MusicTypeOthers = "OTHERS";
        public const string MusicTypePop = "POP";
        #endregion

        #region Caching Constants

        //constants for naming the cache object
        public const string IsCacheEnabled = "IsCacheEnabled";

        public const string CacheHostAddress = "CacheHostAddress";
        public const string CacheHostPort = "CacheHostPort";

        public const string DefaultRightsAndRestrictions = "DEFAULT_RIGHTS_AND_RESTRICTIONS";
        public const string DefaultSecondaryExploitations = "DEFAULT_SECONDARY_EXPLOITATIONS";
        public const string DefaultDigitalRestrictions = "DEFAULT_DIGITAL_RESTRICTIONS";
        public const string ContractFormMasterData = "CONTRACT_FORM_MASTERDATA";
        public const string TerritoryInfo = "TERRITORY_INFO";
        public const string LookUpMasterData = "LOOKUP_MASTERDATA";
        public const string GetCompanies = "GET_COMPANIES";
        public const string GetContractStatus = "CONTRACT_STATUS";
        public const string GetCountries = "GET_COUNTRIES";
        public const string GetDivisions = "GET_DIVISIONS";
        public const string GetLabels = "GET_LABELS";
        public const string GetWorkFlowStatus = "WORKFLOW_STATUS";
        public const string GetContractDescription = "CONTRACT_DESCRIPTION";
        public const string GetRightsPeriods = "RIGHTS_PERIODS";
        public const string GetLostRightsReasons = "RIGHTS_REASONS";
        public const string GetRightsDefaultTypes = "RIGHTS_DEFAULT_TYPES";
        public const string GetContentTypeModel = "CONTENT_TYPE";
        public const string GetUseTypeModel = "USE_TYPE";
        public const string GetCommercialTypeModel = "COMMERCIAL_TYPE";
        public const string GetConsentTypeModel = "CONSENT_TYPE";
        public const string GetRestrictions = "GET_RESTRICTIONS";
        public const string Template1 = "template_1";
        public const string Template2 = "template_2";
        public const string DefaultRightsReviewStatus = "DEFAULT_RIGHTS_MASTERDATA";

        public const string DefaultRightsMasterData = "DEFAULT_RIGHTS_MASTERDATA";
        public const string GetPreClearance = "DEFAULT_PRECLEARANCE";
        public const string HashCodeConstant = "GTA_NOTIFICATION";

        public const string WorkQueueMasterData = "DEFAULT_WQ_MASTERDATA";
        public const string WorkGroupRolesMasterData = "DEFAULT_MASTERDATA_WGROLES";
        public const string WorkGroupRequestTypeMasterData = "DEFAULT_MASTERDATA_WGREQUEST_TYPE";
        public const string EmailPreferenceMasterData = "DEFAULT_MASTERDATA_EMAILPREFERENCE";

        public const string ReleaseConfigGroupMasterData = "DEFAULT_RELEASE_CONFIGURATIONGROUP";
        public const string ReleaseLabelMasterData = "DEFAULT_RELEASE_LABLE";
        public const string DeviatedICLALevelMasterData = "DEFAULT_MASTERDATA_DEVIATED_ICLALEVEL";
        public const string PriceLevellMasterData = "DEFAULT_MASTERDATA_PRICELEVEL";
        public const string RequestPriceLevelMasterData = "DEFAULT_MASTERDATA_REQPRICELEVEL";
        public const string CurrentPriceLevelMasterData = "DEFAULT_MASTERDATA_CURRENTPRICELEVEL";
        public const string CurrentRequestPriceLevelMasterData = "DEFAULT_MASTERDATA_REQPRICELEVEL";
        public const string CurrencyMasterData = "DEFAULT_MASTERDATA_CURRENCY";
        public const string ClearanceProjectAdministration = "DEFAULT_CLEARANCE_PROJECT_ADMINISTRATION";

        #endregion Caching Constants

        public const string FileNotFoundMsg = "File Not Found";
        public const string FileNotFoundCode = "404";
        public const string RightsReview = "RightsReview";
        public const string CommonWebConfigLocation = "Common web.config file at \\Configuration virtual directory";
        public const string MsgEnableUserSecurity = "EnableUserSecurityProfileForAddressBook";
        public const string EnumMemberAttribute = "[EnumMemberAttribute]";
        public const string ReturnVal = "[Flags]\nenum {0}\n{{\n";
        public const string ComponentsPath = "Components path cannot be null/ empty";
        public const string LoggerObject = "Logger object is null!";

        public const string StringEmpty = " ";
        public const string StringNullEmpty = "";
        public const string UnderScore = "_";
        public const string SlashnValue = ",\n";
        public const string SlashnValueEnd = "\n}}";
        public const int Two = 2;
        public const string Compare = "\t{0} = {1}L";
        public const string ContractIncluded = "{0}\n{1}";
        public const string DateFormat = "yyyy-MM-dd HH:mm:ss fffffff";
        public const string DeleteConcurrencyHandled = "Concurrency handled while Deleting. ";
        public const string SaveConcurrencyHandled = "Concurrency handled while Saving.";
        public const string Link = "Link";
        public const string Split = "Split";

        public static string StringLessThan = "< {0} {1}";
        public static string StringGreaterThan = ">";
        public static string StringTilde = "#~";
        public static string ClosedTag = "</ {0} {1}";

        #region User Roles
        public const string LocalLabelReviewer = "Local Label Reviewer";
        public const string UMGIMarketingReviewer = "UMGI Marketing Reviewer";
        public const string NationalMarketingReviewer = "National Marketing Reviewer";
        public const string NationalLegalReviewer = "National Legal Reviewer";
        public const string InternationalMarketingReviewer = "International Marketing Reviewer";
        public const string InternationalLegalReviewer = "International Legal Reviewer";
        public const string UMGIGlobalClearance = "UMGI Global Clearance";
        public const string RCCADMIN = "RCC ADMIN";
        #endregion

        #region Service

        public const string TimeSpan = "TimeSpan";
        public const string IsSecurityEnabled = "IsSecurityEnabled";
        public const string BindingType = "BindingType";
        public const string DefaultBindingType = "net.tcp";
        public const string UserName = "UserName";
        public const string NameSpace = "http://grcs.com";
        public const string BasicUserCheck = "read";

        #endregion Service

        #region Connection Entities

        public const string ArtistDataEntities = "ArtistDataEntity";
        public const string ContractTemplateDataEntities = "ContractTemplateEntities";
        public const string ContractDataEntities = "ContractDataEntities";
        public const string GlobalDataEntities = "GlobalDataEntities";
        public const string MasterDataEntities = "MasterDataEntities";
        public const string GtaDataEntity = "GtaDataEntity";
        public const string RightsDataEntities = "RightsDataEntities";
        public const string AdminDataEntities = "AdminSettingsEntities";
        public const string RepertoireDataEntities = "RepertoireDataEntities";
        public const string WorkQueueDataEntities = "WorkQueueDataEntity";
        public const string NotificationDataEntities = "GtaDataEntity";
        public const string PCompanyDataEntities = "PCompanyDataEntity";
        public const string UserDataEntities = "UserDataEntities";
        public const string ClearanceProjectDataEntities = "ClearanceProjectDataEntities";
        public const string ClearanceDevEntities = "ClearanceDevEntities";
        public const string ContractDataForDownStreamEntities = "ContractDataForDownStreamEntities";
        public const string RoutingDataEntities = "RoutingDataEntities";
        public const string EmailDataEntities = "EmailDataEntities";

        //Reports
        public const string ReportDataEntity = "ReportDataEntity";
        public const string RepertoireRightsSearchEntities = "RepertoireRightsSearchEntities";
        public const string RightsReviewWorkQueueEntities = "RightsReviewWorkQueueEntities";

        #endregion Connection Entities

        public const string Company = "Company";
        public const string Division = "Division";
        public const string Label = "Label";
        public const string Artist = "ARTST";

        // Source of CDL combination: GRCS, MP
        public enum RightSourceType
        {
            Grcs = 1, // GRCS application
            Mp = 2 // Media Portal
        }

        public static byte NewForReview = 1;
        public static byte FinalForReview = 2;
        public static byte Final = 3;
        public static string StrFinal = "Final";

        public const long ResourceHasSample = 1;
        public const long ResourceHasSideArtist = 2;
        public const long ContractLinkWithRccUser = 3;
        public const long RightSetHasRestriction = 4;
        public const long RightSetCreateForSplitDealAutoLink = 5;
        public const long LinkcontractHasRightsExceptionNotApplied = 6;

        public const byte GrcsResource = 1;
        public const byte MpResource = 2;

        public const int ResourceType = 3;
        public const int TrackType = 4;

        public const string Audio = "AUDIO";
        public const string Video = "VIDEO";
        public const string Image = "IMAGE";

        public static byte PhysicalExploitation = 1;
        public static byte DigitalExploitation = 2;
        public static byte ContractLinkRequired = 1;
        
        public const string RepertoireMailSubject = "Repertoire mail alert";
        public const string EmailRecep = "ar_umg_gcs@hpe.com";
        
        public const string ErrorNotValidUserSetting = "Not a valid user setting";
        
        public const int Week = 7;
        public const int NumberOne = 1;
        public const int NumberofWorkingDays = 5;
        public const int NumberofMonth = 12;
        public const int NumberofDaysinMonth = 31;
        public const bool TrueValue = true;
        public const bool FalseValue = false;
        public const int DefaultStartIndex = 0;

        public const string RestrictionType = "Restriction Type";
        public const string ConsentPeriodType = "Consent Period Type";
        public const string CommercialType = "Commercial Model Type";
        public const string UseType = "Use Type";
        public const string ContentType = "Content Type";
        public const string RightsReviewStatus = "Review Status Type";
        public const string SplitDealAutoLinked = "Split deal - Auto linked";
        public const string ReleaseLinkToContract = "ReleaseLinkToContract";
        public const string ResourceLinkToContract = "ResourceLinkToContract";
        public const string TypeofStatus = "Active";
        public const string TypeofTask = "Priority";

        #region Look ups

        public const string LookupType = "Lookup_Type";
        public const string RightsReviewStatusTypeCode = "REVIW";
        public const string PreClearanceTypeCode = "PRECR";
        public const string ReviewReasonTypeCode = "REVRS";
        public const string ResourceTypeCode = "RESCE";
        public const string ScopeType = "SCOTY";
        public const string LiveStudioType = "LISTY";
        public const string MusicClassType = "MUSTY";
        public const string R2WorkflowStatusType = "R2WFS";
        public const string AudioVideoType = "AUVIT";
        public const string AscendingCode = "ASC";
        public const string DescendingCode = "DESC";
        public const string SecondaryExploitation = "EXPLT";
        public const string ContractStatusTypeCode = "CTRCT";
        public const string WorkflowStatusTypeCode = "WRKFW";
        public const string RightsTypeCode = "RGTDF";
        public const string RightsPeriodTypeCode = "RTPRD";
        public const string LostRightsReasonTypeCode = "LOSTR";
        public const string RestrictionReasonTypeCode = "RRETY";
        public const string ConsentPeriodTypeCode = "CNSNT";
        public const string CommercialModelCode = "COMME";
        public const string UseTypeCode = "USETY";
        public const string ContentTypeCode = "CONTN";
        public const string RestrictionTypeCode = "RSTRN";
        public const string MatchTypeCode = "MATCH";
        public const string RightsAndRestriction = "RIGHT";
        public const string AcquiredStatus = "ACPAT";
        public const string ArtistTypeName = "ARTST";
        public const string ResourceTypes = "RESCE";
        public const string ReleasePreDefinedParameters = "RELPR";
        public const string ResourcePreDefinedParameters = "RESPR";
        public const string PreClearanceMasterData = "YESNO";
        public const string ReviewCondition = "REVCN";
        public const string DeliveryType = "DEVTY";
        public const string SourceSystemName = "SST";
        public const string Success = "Success";
        public const string Failure = "Failure";
        public const string MediaPortalName = "MediaPortal";
        public const string IncludedTypeCode = "INCLD";
        public const string IncludedType = "Included";

        #endregion Look ups
   
        public const string ItemType = "ITEMT";
        public const string LogStatusType = "Lgstp";
        public const string RollUpUser = "RollUp";

        public const string AlbumOnly = "AlbumOnly";
        public const string Download = "Download";
        public const string Streamable = "Streamable";
        public const string Streaming = "Streaming";
        public const string MakeOwnRingtone = "MakeOwnRingtone";
        public const string Country = "Country";
        public const string ReleaseDateLeadTime = "ReleaseDateLeadTime";

        public const string RightExpiryEmailSubject = "Rights Expiry information";
        public const string EndOfTermEmailSubject = "End Of Term information";
        public const string WorkFlowEmailSubject = "WorkFlow information";
        public const string ErrorInNotification = "Error in sending email Notification";
        
        public const int PhysicalExpRightsAcquiredType = 1;
        public const int DigitalExpRightsAcquiredType = 2;
        public const byte DigRestrictionType = 2;
        public const string YValue = "Y";
        public const string NValue = "N";
        public const char YCharValue = 'Y';
        public const char NCharValue = 'N';
        public const string StrReleaseDigitalRestrictionSpName = "USP_GetCustomWQReleaseDigitalRestrictionGrid";
        public const string StrResourceDigitalRestrictionSpName = "USP_ResourceDigitalRightsReviewWQ";
        public const string StrSqlConnString = "DigitalRestriction";
        public const string StrRightSetConnString = "RightSetEdited";
        public const string StrRightSetEditedSpName = "USP_RightSetEdited";

        public const string ParamISRC = "@PARAM_ISRC";
        public const string ParamArtistName = "@PARAMARTISTNAME";
        public const string ParamIsExactSearch = "@PARAMISEXACTSEARCH";
        public const string ParamResourceTitle = "@PARAM_RESTITLE";
        public const string ParamLinkedContractID = "@PARAM_LINKEDCONTRACTID";
        public const string ParamMultiLinkedContract = "@PARAMMULREPLINKEDCONTRACT";
        public const string ParamResourceTypeID = "@PARAM_RESTYPEID";
        public const string ParamUPC = "@PARAM_UPC";
        public const string ParamReleaseTitle = "@PARAM_RELTITLE";
        public const string ParamStartValue = "@StartValue";
        public const string ParamEndValue = "@EndValue";
        public const string ParamSortField = "@SortField";
        public const string ParamSortType = "@SortType";
        public const string ParamRightReviewStatus = "@PARAM_RIGREVSTATUS";
        public const string ParamTerritorialCountryID = "@PARAM_TERRCOUNTRYID";
        public const string ParamRightPeriodID = "@PARAM_RIGPER";
        public const string ParamIsLostRight = "@PARAM_ISLostRight";
        public const string ParamLostRightReason = "@PARAM_LostRightReason";
        public const string ParamPhysicalExploitationRights = "@PARAM_PHYEXP";
        public const string ParamDigitalExploitationRights = "@PARAM_DIGEXP";
        public const string ParamDigitalUnbundlingAllowed = "@PARAM_DIGUNBUNDLING";
        public const string ParamMobileExploitationRights = "@PARAM_MOBEXP";
        public const string ParamPPBRevenueChain = "@PARAM_PPB";
        public const string ParamActiveForMarketing = "@PARAM_ActMar";
        public const string ParamLostRightFromDate = "@PARAM_Lost_Right_From_Date";
        public const string ParamLostRightToDate = "@PARAM_Lost_Right_To_Date";
        public const string ParamAdmincompanyList = "@PARMA_AdminComp_List";
        public const string ParamDigitalRestriction = "@PARAM_DigitalRestriction";
        public const string ParamArtist_Name = "@PARAM_ARTISTNAME";
        public const string ParamIsExact_Search = "@PARAM_ISEXACTSEARCH";
        public const string ParamTopPriceCompilation = "@PARAM_PRECLEATPCOM";
        public const string ParamMidPriceCompilation = "@PARAM_PRECLEAMPCOM";
        public const string ParamBudgetCompilation = "@PARAM_PRECLEABUDCOM";
        public const string ParamDirectMarketing = "@PARAM_PRECLEADIRMAR";
        public const string ParamPremium = "@PARAM_PRECLEAPREMIUM";
        public const string ParamMasterUse = "@PARAM_PRECLEAMASTERUSE";
        public const string ParamMultiArtistCompilation = "@PARAM_MUL_ARTIST_COMP";
        public const string ParamSynchronization = "@PARAM_SYNC";
        public const string ParamMidPrice = "@PARAM_MIDPRICE";
        public const string ParamBudget = "@PARAM_BUDGET";
        public const string ParamExploitationPremium = "@PARAM_PREMIUM";
        public const string ParamClubMail = "@PARAM_CLUB";
        public const string ParamRemixEdit = "@PARAM_REMIX";
        public const string ParamSampleUse = "@PARAM_SAMPLEUSE";
        public const string ParamRoyalityBreak = "@PARAM_ADV_ROYA";
        public const string ParamGreatestHits = "@PARAM_GREATEST";
        public const string ParamKiosk = "@PARAM_KIOSK";

        ///SP
        public const string USPGetDigitalRestrictionReleasesSearch = "USP_GetDigitalRestrictionReleasesSearch";
        public const string USPGetDigitalRestrictionResourcesAndTrackReleaseParameterSearch = "USP_GetDigitalRestrictionResourcesAndTrackReleaseParameterSearch";
        public const string USPGetDigitalRestrictionResourcesAndTrackBasicSearch = "USP_GetDigitalRestrictionResourcesAndTrackBasicSearch";
        public const string USPGetDigitalRestrictionTrackReleaseParameterSearch = "USP_GetDigitalRestrictionTrackReleaseParameterSearch";
        public const string USPGetDigitalRestrictionTrackBasicSearch = "USP_GetDigitalRestrictionTrackBasicSearch";
        public const string USPGetDigitalRestrictionResourceReleaseParameterSearch = "USP_GetDigitalRestrictionResourceReleaseParameterSearch";
        public const string USPGetDigitalRestrictionResourceBasicSearch = "USP_GetDigitalRestrictionResourceBasicSearch";
        public const string USPGetResourcesReleaseParameterSearch = "USP_GetResourcesReleaseParameterSearch";
        public const string USPGetPreClearanceReleaseParameterSearch = "USP_GetPreClearanceReleaseParameterSearch";
        public const string USPGetSecondaryExploitationReleaseParameterSearch = "USP_GetSecondaryExploitationReleaseParameterSearch";
        public const string USPGetTracksReleaseParameterSearch = "USP_GetTracksReleaseParameterSearch";
        public const string USPGetResourcesAndTracksReleaseParameterSearch = "USP_GetResourcesAndTracksReleaseParameterSearch";

        //For DataReader

        public const string RightSetID = "right_set_id";
        public const string DataReaderArtist = "Artist";
        public const string ReleaseDate = "ReleaseDate";
        public const string LinkedContract = "LINKED_CONTRACT";
        public const string LinkedContractInfo = "LinkedContractInfo";
        public const string ClearanceAdminCompany = "ClearanceAdminCompany";
        public const string PYear = "PYEAR";
        public const string TerritorialRights = "TerritorialRights";
        public const string LostRights = "LostRights";
        public const string PhysicalExploitationRights = "PhysicalExploitationRights";
        public const string DataReaderPhysicalExploitationRights = "DigitalExploitationRights";
        public const string DataReaderMobileExploitationRights = "MobileExploitationRights";
        public const string DataReaderPPBRevenueClaim = "PPBRevenueClaim";
        public const string DigitalUnbundlingAllowed = "DigitalUnbundlingAllowed";
        public const string TotalRows = "TotalRows";
        public const string StrResourceType = "ResourceType";
        public const string ArtistExist = "ArtistExist";
        public const string SampleExist = "SampleExist";
        public const string ReviewStatus = "ReviewStatus";
        public const string ActiveForMarketing = "ACTIVEFORMARKETING";
        public const string DataReaderUseType = "UseType";
        public const string CommercialModels = "CommercialModels";
        public const string Restrictions = "Restrictions";
        public const string ConsentPeriod = "ConsentPeriod";
        public const string Notes = "Notes";
        public const string SearchFilterlog = "Is Sensitive: {0},Search filter:{1}";
        public const string TopPriceCompilation = "TopPriceCompilation";
        public const string MidPriceCompilation = "MidPriceCompilation";
        public const string BudgetCompilation = "BudgetCompilation";
        public const string DirectMarketing = "DirectMarketing";
        public const string Premimun = "Premimun";
        public const string MasterUse = "MasterUse";
        public const string SampleCredit = "SampleCredit";
        public const string IsSplitDeal = "IsSplitDeal";
        public const string MultiartistCompilation = "MultiartistCompilation";
        public const string Synchronization = "SYNC";
        public const string MidPrice = "MIDPR";
        public const string Budget = "BUDGET";
        public const string Premium = "PREMIUM";
        public const string ClubMail = "CLUBMAIL";
        public const string RemixEdit = "REMIXEDIT";
        public const string Sample = "SAMPLE";
        public const string RoyalityBreaks = "Royaltybreaks";
        public const string Hits = "Hits";
        public const string kishok = "kishok";
        public const string TerritoryExclusion = "TerritoryExclusion";
        public const string ResourceId = "ResourceId";
        public const string ReleaseId = "ReleaseId";
        public const string Contract_Id = "Contract_Id";
        public const string Resource_Id = "RESOURCE_ID";
        public const string Release_Id = "RELEASE_ID";
        public const string ReleaseTypes = "ReleaseType";
        public const string IsMAC = "Is_Mac";
        public const string R2_Resource_Id = "R2_RESOURCE_ID";
        public const string R2_Release_Id = "R2_RELEASE_ID";
        public const string R2ResourceId = "ResourceId";
        public const string R2ReleaseId = "ReleaseId";
        public const string LookupDataSourceText = "LookupDataSourceText";
        public const string LkupDataSourceVal = "LkupDataSourceVal";

        //For Audit Trial

        public const string CdlAuditMapping = "CDLAuditMapping";
        public const string ContractAuditMapping = "ContractAuditMapping";

        //Audit Trial -End

        //Code Refactor
        public const string StringValOne = "1";
        public const string StringValTwo = "2";
        public const int SplitRightsAndRestrictions = 99;
        public const string ContractDescType = "CDEST";
        public const string SplitDealFlag = "S";
        public const string ContractTemplateType = "C";
        public const string DigitalTemplateType = "D";
        public const string TemplateDeletionSuccessMsg = "Templates Deleted Successfully";
        public const string SelectTemplateToDeleteMsg = "Please select template to delete!";
        public const string TemplateDeletionFailedMsg = "Templates deletion failed !";
        public const string ErrorMessage = "No records exist!";
        public const string UmgSigningCompanyValue = "UmgSigningCompany";
        public const string TemplateName = "TemplateName";

        public const int RightsReason_PendingApproval = 3;
        public const int RightsReason_Approved = 4;
        public const int RightsReason_UnderAmendment = 5;
        public const int RightsReason_DataEntry = 6;
        public const int ReasonType = 15;
        public const string System = "System";

        public const string SNo = "#~SNo#~";
        public const string htmaltag_div = "<div>";
        
        #region reports

        public const string ReportRightsDataDescripency = "RightsDataDescripencyReport";
        public const string ReportResourceRightsDataDescripency = "ResourceRightsDataDescripencyReport";
        public const string ReportRollUpStatus = "RollUpStatusReport";
        public const string UrlResourceRightsDataDescripency = "rptResourceRightsDataDescripency";
        public const string UrlrptReleaseRollUp = "rptReleaseRollUp";
        public const string UrlrptRightsDataDescripency = "rptRightsDataDescripency";
        public const string ReportNameSuffix = "ddMMyyyy_hhmmss";
        public const string ReportExt = ".xls";
        public const string ReportFormat = "Excel";
        public const int ReportStatusSuccess = 1;
        public const int ReportStatusFailure = 2;
        public const int ReportStatusProcessing = 3;

        #endregion

        #region upstream Notification

        public enum ProjectType
        {
            Master = 1,
            Regular
        };

        public enum StatusTypePA
        {
            Unsubmitted = 1,
            Submitted = 2,
            Cancelled = 3,
            Completed = 4,
            ReSubmitted = 5,
            ReOpened = 6
        };

        public enum LookupConstant
        {
            MUSTY,
            RESCE,
            LISTY,
            PRLTY,
            CLBPR,
            ICLTY,
            PLCTY,
            PLRTY,
            PRMPR,
            REASN,
            INXRQ,
            INSTY,
            ISREV,
            ISREQ,
            ISRCC,
            IRRCC
        };

        public enum MasterRequestType
        {
            Advertising = 1,
            Film,
            Trailer,
            Other
        };

        public enum RegularRequestType
        {
            RegularRetail = 1,
            Club,
            NonTradition,
            Promotional,
            TVRadioBreakICLA,
            PriceReduction
        }
        #endregion

        public const string GrsLogo = "grslogo";
        public const string GcsLogo = "gcslogo";

        //Excel helper strings
        public const string StringText = "String";
        public const string RowStart = "<Row>";
        public const string RowEnd = "</Row>";
        public const string ColumnWithWidth = "<Column ss:AutoFitWidth=\"0\" ss:Width=\"{0}\"/>";
        public const string DateOnlyFormat = "dd MMM yyyy";

        public const string ContractID = "ContractID";
        public const string ContractingParties = "Contracting Party";
        public const string CAdminCompanyId = "AdminCompanyId";
        public const string CAdminCompany = "AdminCompany";
        public const string CArtistName = "Artist Name";
        public const string ISRC = "ISRC";
        public const string UPC = "UPC";
        public const string UserId = "UserName";
        public const string UserMailId = "Mail";
        public const string Id = "RepertoireID";
        public const string RepertoireType = "RepertoireType";

        //Search Contract
        public const string ExportToExcelRepertoires = "Repertoires";
        public const string ResponseContentType = "application/vnd.ms-excel";
        public const string ResponseContentDisposition = "Content-Disposition";
        public const string AttachmentExcel = "attachment; filename=Report.xls";
        public const string ResponseContentWord = "attachment;filename=Contract_Information_ID_";
        public const string DocExtension = ".doc";
        public const string ResponseContentTypeWord = "application/vnd.ms-word.document";
        public const string ExcelAttachmentFolderPath = "ExcelAttachmentFolderPath";
        public const string ExcelExtension = ".Xls";
        public const string ExcelDateformat = "yyyyMMdd_hhmmss";
        public const string UnprocessedRepertoireList = "\\UnprocessedRepertoireList_";

        public const byte ByteValueZero = 0;
        public const string ReleaseTypeLicenseOut = "LICOUT";

        public const string AppSettingsUPC_THRESHOLD = "UPC_THRESHOLD";
        public const string AppSettingsIT_ADMIN_MAIL_ID = "IT_ADMIN_MAIL_ID";
        public const string AppSettingsMC_SEQ_NAME = "MC_SEQ_NAME";
        public const string AppSettingsNC_SEQ_NAME = "NC_SEQ_NAME";

        #region Routing

        public const string RoutingUrl = "RoutingUrl";
        public const string ServiceTimeOut = "ServiceTimeOut";
        public const string IsSecured = "IsSecured";
        public const string AssemblyName = "AssemblyName";
        public const short RequestPriceLevelMid = 1;
        public const short RequestPriceLevelBudget = 2;
        public const short ClassicalMusicType = 1;
        public const string GtaAdminEmailId = "GtaAdminEmailId";
        public const string R2AdminEmailId = "R2AdminEmailId";
        public const string RoutingServiceTimeInterval = "RoutingServiceTimeInterval";
        public const string EmailServiceTimeInterval = "EmailServiceTimeInterval";

        #endregion

        #region Clearance Inbox

        //String Delimiter
        public const string StringDelimiter = "|";

        //Request Type
        public const string ClearanceInboxRequestType = "INXRQ";
        public const string ClearanceInboxRccAdminRequestType = "IRRCC";

        //Scope Type
        public const string ClearanceInboxScopeType = "INSTY";

        //Search Type
        public const string ClearanceInboxReviewerSearchType = "ISREV";
        public const string ClearanceInboxRequestorSearchType = "ISREQ";
        public const string ClearanceInboxRccAdminSearchType = "ISRCC";

        //Search Type Default
        public const long ClearanceInboxReviewerDefaultSearchType = 1;
        public const long ClearanceInboxRequestorDefaultSearchType = 1;
        public const long ClearanceInboxRccAdminDefaultSearchType = 1;

        //Default number of projects that should be displayed in a folder
        public const long DefaultFolderSize = 20;

        //Clearance Inbox Filter Criteria XML
        public const string FilterCriteriaRootNode = "FilterCriteria";
        public const string FilterCriteriaRoleGroupNode = "RoleGroup";
        public const string FilterCriteriaRequestTypeNode = "RequestType";
        public const string FilterCriteriaScopeTypeNode = "ScopeType";
        public const string FilterCriteriaRccAdminRequestTypeNode = "RccAdminRequestType";
        public const string FilterCriteriaRccHandlerNode = "RccHandler";
        public const string FilterCriteriaRequestorNode = "Requestor";
        public const string FilterCriteriaWorkgroupNode = "Workgroup";

        //Clearance Inbox Filter Criteria Attribute
        public const string FilterCriteriaIdAttribute = "Id";

        //Clearance Inbox Filter Criteria XPath
        public const string FilterCriteriaRequestTypeXPath = "/RoleGroup/RequestType";
        public const string FilterCriteriaRccAdminRequestTypeXPath = "/RoleGroup/RccAdminRequestType";
        public const string FilterCriteriaRccHandlerXPath = "/RoleGroup/RccHandler";
        public const string FilterCriteriaRequestorXPath = "/RoleGroup/Requestor";
        public const string FilterCriteriaScopeTypeXPath = "/RoleGroup/ScopeType";
        public const string FilterCriteriaWorkgroupXPath = "/RoleGroup/Workgroup";

        //Work Queue Parameter's Queue Name
        public const string WQClearanceInboxFilterCriteria = "ClearanceInboxFilterCriteria";
        public const string ResourceResubmissionComments = "Resubmission Occured due to following fields :-";

        //Error Codes - These need to be moved to some suitable constant lookup file
        public const string UnhandledException = "UnhandledException";
        public const string ErrorCode_FolderProjectsAreLocked = "Few projects are in locked state. To delete a project, it must be in unlocked state.";
        public const string ErrorCode_FolderProjectsAlreadyUpdated = "Few projects have already been updated. Please refresh your inbox.";
        public const string ErrorCode_FolderAlreadyExists = "Folder already exists.";
        public const string ErrorCode_FolderDoesNotExists = "Folder does not exists.";
        public const string ErrorCode_FolderCannotModify = "Cannot modify system folder.";
        public const string ErrorCode_FolderNoDelete = "Cannot delete selected folder. To delete this folder please move its projects to some other folder.";
        public const string ErrorCode_ProjectMoveRestricted = "Project move restricted.";
        public const string ErrorCode_ProjectNotFound = "Project not found.";

        public const string InboxRequestAction_MassReminder = "Mass Reminder";
        public const string InboxRequestAction_Cancelled = "Mark as Cancelled";
        public const string InboxRequestAction_Completed = "Mark as Completed";

        // Clearance Inbox Views
        public const string RightPanelRequestorPartialView = "RightPanel-Requestor";
        public const string RightPanelReviewerPartialView = "RightPanel-Reviewer";
        public const string RightPanelRccAdminPartialView = "RightPanel-RccAdmin";

        #endregion Clearance Inbox

        #region User Special Rols
        public const string Manage_Requestor_Workgroup = "Manage Requestor Workgroup";
        public const string Manage_Inquiry_Workgroup = "Manage Inquiry Workgroup";
        public const string Manage_Reviewer_Workgroup = "Manage Reviewer Workgroup";
        public const string UPC_Allocation_Requestor = "UPC Allocation Requestor";
        public const string R2_Authorized_Requestor = "R2 Authorized Requestor";
        public const byte User_Special_RoleId = 0;
        #endregion

        public const char EmailAddressDelimiter = ',';
        public const string GetRccAdminWorkGroupId = "DEFAULT_RCC_ADMIN_WORK_GROUP_ID";
        public const string GetSalesChannelLookup = "DEFAULT_SALES_CHANNEL_LOOK_UP";
        public const string GetGlobalCompany = "DEFAULT_GLOBAL_COMPANY";

        public const int Physical = 1;
        public const int Digitial = 2;
        public const int PhysicalAndDigitalKey = 3;

        public const string RoutingParameters = "ROUTING_PARAMETERS";

        public const string ParallelRoutingCountConfig = "ParallelRoutingCount";

        public const string SendMailWithAutoCancel = "SendMailWithAutoCancel";

        #region WorkgroupDataManager Constant Variable Declaration

        public const string _msgNewWorkGroupCreation = "New Work Groups Creation";
        public const string _msgNewWorkGroupCompanyCreation = "New Workgroup Company Creation";
        public const string _msgNewUserCreation = "New User Creation";
        public const string _msgNewWorkgroupUserCreation = "New Workgroup User Creation";
        public const string _msgWorkGroupUpdation = "Work Groups Updation";
        public const string _msgWorkGroupCompanyUpdation = "Workgroup Company Updation";
        public const string _msgUserUpdation = "User Updation";
        public const string _msgChildworkgroupDeviationRequestTypeUpdation = "Child Workgroup Deviation RequestType Updation";
        public const string _msgChildworkgroupDeviationArtistContractUpdation = "Child Workgroup Deviation ArtistContract Updation";
        public const string _msgChildworkgroupDeviationResourceContractUpdation = "Child WorkGroup Deviation ResourceContract Updation";
        public const string _msgWorkgroupUpdationCompleted = "Workgroup updation successfully completed";
        public const string _msgWorkgroupUserUpdation = "Workgroup User Updation";
        public const string _msgParentWorkgroupPending = "Check the parent workgroup pending request";
        public const string _msgChildWorkgroupPending = "Check the child workgroup pending request";
        public const string _msgWorkgroupDeletion = "Workgroup Successfully Deleted";
        public const string _msgWorkgroupDeactive = "Workgroup Successfully Deactivated";
        public const string _msgWorkgroupPendingrequest = "Workgroup has pending request";
        public const string _deActivate = "Deactivated";
        public const string _uMGIMarketingReviewerRole = "UMGI Marketing Reviewer";
        public const string _uMGIGlobalClearanceRole = "UMGI Global Clearance";
        public const string _alreadyDataExist = "Already Data Exist in Database";
        public const string _msgNewChildWorkGroupCreation = "New Child Work Groups Creation";
        public const string _msgNewChildWorkGroupCompanyCreation = "New Child Workgroup Company Creation";
        public const string _msgNewChildUserCreation = "New Child User Creation";
        public const string _msgNewChildWorkGroupCountryCreation = "New Child Workgroup Company Creation";
        public const string _msgNewChildWorkgroupUserCreation = "New Child Workgroup User Creation";
        public const string _msgNewChildworkgroupDeviationRequestTypeCreation = "New Child Workgroup Deviation RequestType Creation";
        public const string _msgNewChildworkgroupDeviationArtistContractCreation = "New Child Workgroup Deviation ArtistContract Creation";
        public const string _msgNewChildworkgroupDeviationResourceContractCreation = "New Child WorkGroup Deviation ResourceContract Creation";
        public const string _workgroupDeviation = "WGDEV";
        public const string _workgroupRole = "ROLET";
        public const string _workgroupRoleAdmin = "RCC Admin";
        public const string _preferanceTypeRequestingCompany = "RC";
        public const string _preferanceTypeCurrency = "CURCY";
        public const string _userPreferenceType = "USRPR";
        public const string _userPreferenceRoleType = "INBOX";
        public const int _workgroupDeviationforRequestType = 1;
        public const int _workgroupDeviationforArtistContract = 2;
        public const int _workgroupDeviationforResourceContract = 3;
        public const int _workgroupDeviationfRequestTypeLookup = 53;
        public const int _workgroupStatusType = 1;
        public const string _workgroupRequestType = "WGRTY";
        public const int _workgroupDeactivate = 3;
        public const int _parentIdDbValue = 0;
        public const string _workGroupTypeParent = "Parent";
        public const string _workGroupTypeChild = "Child";
        public const string _lookupStatusType = "WGSTU";
        public const string _lookupTypeResource = "RESCE";
        public const string _lookupTypeRoleGroup = "RLGRP";
        public const string _parentWorkgroupUniqueness = "Business Rule Parent workgroup uniqueness";
        public const string _childWorkgroupUniqueness = "Business Rule Child workgroup uniqueness";
        public const char _commaSymbol = ',';
        public const int _workgroupMaxLength = 254;
        public const int _workgroupMinLength = 0;
        public const string _dotSymbol = "...";
        public const string _arrayCommasymbol = "{0}, ";
        public const char _barsymbol = '|';
        public const string _arraynewLinesymbol = "{0},<br />";
        public const int _suggestiveGetRecordCount = 200;
        public const string _projectRegular = "Regular";
        public const string _projectPriceReduction = "Price Reduction";
        public const string _concurrencyException = "Concurrency Exception:This record has been modified by another user.Changes you have made have not been saved, please resubmit";
        public const string concurrencyImplementation = "Concurrency Implementation completed";
        public const string _leftBrace = "(";
        public const string _rightBrace = ")";
        public const string _formatWithSemiColon = "{0}; ";

        #endregion WorkgroupDataManager Constant Variable Declaration

        #region GCS AuditTrail Key

        public const string IsAuditEnabled = "IsAuditEnabled";
        public const string StatusTypeProject = "Status_Type";
        public const string StringOne = "1";
        public const string StringTwo = "2";
        public const byte NinetyNineValue = 99;
        public const byte ByteOne = 1;
        public const byte ByteTwo = 2;

        #endregion GCS AuditTrail Key

        public const string GcsRolesAndTasksData = "DEFAULT_GCS_ROLESANDTASKS";

        #region "GCS Email Key"
        public const string ClrProjectUrl = "ClrProjectUrl";
        public const string SearchPattern = @"#~\w*#~";
        public const string EmailSignature = "GCS System";
        public const string EmailSignatureKey = "EmailSignature";
        public const string startTableArtistManagement = @"<div>  <table id='tblEmail' class='artistManagement' border='1'>";
        public const string startBold = "<b>";
        public const string endBold = "</b>";
        public const string startDivHyperLink = "<div class='lnkProjectCode'>";
        public const string startHyperLink = "<a href='";
        public const string endHyperLink = "'>";
        public const string endDivHyperLink ="</a></div>";
        public const string dot = ".";

        // Columns Of Email
        public const string ColumnProjectTitleTentative = "Project_Title_Tentative";
        public const string ColumnUMGProjectReferenceNo = "UMG_Project_Reference_No";
        public const string ColumnProjectCode = "ProjectCode";
        public const string ColumnTentative = "Tentative";

        //Emails Constants
        public const string emailDisableMessage = "Send email is disabled in configuration.";
        public const string staticDataName = "StaticData";
        public const string resourceTableName = "ResourceTable";
        public const string releaseTableName = "ReleaseTable";
        public const string resourceSelectedTable = "ResourceSelectedTable";
        public const string projectType = "projectType";

        #endregion

        #region Clearance Project Notification Data Constants

        public const int InterfaceType = 3;
        public const string CreatedUserClrMessageBuilderSvc = "ClrMessageBuilderSvc";
        public const string ModifiedUserMessageSenderService = "MessageSenderService";
        public const int NoOfRetry = 0;

        #endregion

        #region Constansts added for GCSInfo for linkikng release
        public const string GRS = "GRS";
        public const string GCS = "GCS";
        public const string LinkType = "LINTY";
        public const string RCCAdmin = "RCC Admin";
        public const string AutoLinking = "Auto Linking";
        #endregion

        public class Territories
        {
            public const string World = "World";
            public const string Universe = "Universe";
        }

        public class ReleaseType
        {
            public const string New = "New";
            public const string Exist = "Exist";
        }

        public const string JsonOk = "OK";
        public const string JsonError = "ERROR";
        public const string ClearanceMusicType = "MUSTY";
        public const string ClearanceClubPriceLevel = "CLBPR";
        public const string ClearacePromotionalPriceLevel = "PRMPR";
        public const string ClearancePriceLevelType = "PRLTY";
        public const string ClearanceICLALevelType = "ICLTY";

        public const string ClearanceCurrPriceLevelType = "PLCTY";
        public const string ClearanceReqPriceLevelType = "PLRTY";

        public const string ClearanceResourceType = "RESCE";
        public const string ClearanceRecordingType = "LISTY";

        public const string RegularProjectType = "Regular/Non Traditional Project";
        public const string MasterProjectType = "Master Project";
        public const string ArchiveFlag = "Y";

        public const byte CancelledProject = 3;
        public const byte CompletedProject = 4;
        public const int ResourceColumnCount = 23;
        public const int DummyPageSize = 10;

        public const int HardCode_requestor_workgroupID = 11;
        
        public const string strCommand = "Re-Instate";
        public const int ReleaseColumnCount = 27;

        public const int DefaultParentId = 1;

        public class Sessions
        {
            public const string OldEntity = "OldEntity_";
            public const string NewEntity = "NewEntity_";
            public const string OldEntityMaster = "OldEntityMaster_";
            public const string NewEntityMaster = "NewEntityMaster_";
        }

        public class TerritoryConstants
        {
            public const string Territory = " of ";
            public const string ManageTerritory = "ManageTerritory";
        }

        #region Workgroup Controller Constants

        public const string IsSortValue = "false";
        public const string ClickTypeDelete = "delete";
        public const string ClickTypeSave = "saved";
        public const string ClickTypeAdd = "add";
        public const string WorkGroupType = "child";
        public const string MaintainChildWorkgroup = "maintainchildworkgroup";
        public const string CreateParentWorkgroup = "createparentworkgroup";
        public const string CreateChildWorkgroup = "CreateChildWorkgroup";
        public const string UserLoginName = "GRCS";
        public const string Save = "save";
        public const string FailMessage = "Fail";
        public const string MimicUser = "MimicUser";
        public const string PhysicalRequest = "Physical,false";
        public const string DigitalRequest = "Digital,false";
        public const string RegularRequest = "Regular,false";
        public const string TvRadioRequest = "TV/Radio Break ICLA,false";
        public const string PriceReductionRequest = "Price Reduction,false";
        public const string ClubRequest = "Club,false";
        public const string NonTraditionalRequest = "Non-Traditional,false";
        public const string PromotionalRequest = "Promotional,false";
        public const string MasterRequest = "Master,false";
        public const int LoginNameCount = 3;
        public const char CommaSeperator = ',';
        public const string AudioResourceType = "Audio";
        public const string AddComma = ",";
        public const string ManageResourceContract = "ManageResourceContract";
        public const string ManageArtistContract = "ManageArtistContract";
        public const string LinkWorkGroupToArtistContract = "LinkWGToArtistContract";
        public const string LinkWorkgroupToResourceContract = "LinkWGToResourceContract";
        public const string SemiColonFormatWithSpace = "{0}; ";
        public const string CommaFormat = "{0},";
        public const string LineSeperator = "|";
        public const char SingleOrSeperator = '|';
        public const int TotalNoTwo = 2;
        public const int TotalZero = 0;
        public const string Add = "Add";
        public const string Remove = "Remove";
        public const int PageSize = 10;
        public const int StartIndex = 0;
        public const string Ascending = "Ascending";
        public const string Slash = "/";
        public const string DateLiteralLeft = @"""\/";
        public const string DateLiteralRight = @"\/""";
        public const string CountryNameWithSingleQuote = "Cote d'Ivoire";
        public const string CountryNameAfterRemoveSingleQuote = "Cote dIvoire";
        public const char SlashWithSingleQuote = '\'';
        public const char SlashWithDoubleQuote = '\"';
        public const string Requestor = "Requestor";
        public const string Reviewer = "Reviewer";
        public const string RequestingCompany = "Requesting Company";
        public const string Currency = "Currency";
        public const byte ByteThree = 3;
        public const string MsgParentWorkgroupCreation = "Parent workgroup created sucessfully";
        public const string MsgChildWorkgroupCreation = "Child workgroup created sucessfully";
        public const string MsgParentWorkgroupUpdation = "Parent workgroup updation sucessfully completed";
        public const string MsgChildWorkgroupUpdation = "Child workgroup updation sucessfully completed";
        public const string SearchUserToWorkGroup = "SearchUserToWorkGroup";
        public const string SearchWorkGroupToAddRemoveUsers = "SearchWorkGroupToAddRemoveUsers";
        public const string SearchWorkGroupToAddRemoveUsersPartial = "SearchWorkGroupToAddRemoveUsersPartial";
        public const string SelectSingleUserView = "SelectSingleUser";
        public const string IdSearchPage = "Search";

        #endregion Workgroup Controller Constants

        #region Routing Logs Comments

        public const string CommentCancelledProjectCancelled = "Cancelled: Project Cancellation";
        public const string CommentCancelledProjectCompletion = "Cancelled: Project Completion";
        public const string CommentProjectAutomaticCancel = "Cancelled: No action for 12 months";

        #endregion Workgroup Controller Constants

        public enum Workgroup
        {
            GCSSupport = 1
        }
    }
}
