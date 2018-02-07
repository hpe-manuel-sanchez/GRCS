
using System;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Constants
{
    public partial class Constants
    {
        public const string AppsettingsSupportTeamEmail = "SUPPORT_TEAM_EMAIL";
        public const string GrcsSupportTeamName = "Support Team Personnel";
        public const string GrcsEmailsignature = "Thanks,\nGRCS";
        public const string BackGroundEmailSmtp = "BACKGROUND_EMAIL_SMTP";


        /// <summary>
        /// Gets the service access erroras email subject.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns></returns>
        public static string GetServiceAccessErrorasEmailSubject(string serviceName)
        {
            return String.Format("Error in {0} connectivity", serviceName);
        }

        /// <summary>
        /// Gets the service access erroras email body.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>string which can be sent as Email</returns>
        public static string GetServiceAccessErrorasEmailBody(string serviceName, string sourceSystem,
                                                              Exception exception)
        {
            return String.Format(
                "Dear {0}, \n\n{1} faces a connectivity issue while communicating with {2}.\nPlease resolve the issue with the technical team.\n\n{3}\n\nDetails of the error as follows\nTimeStamp:{4}\n{5}",
                GrcsSupportTeamName, sourceSystem, serviceName,
                GrcsEmailsignature, DateTime.Now, exception);
        }

        /// <summary>
        /// Gets the service access erroras email body.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>string which can be sent as Email</returns>
        public static string GetServiceAccessErrorasEmailBody(string serviceName, Exception exception)
        {
            return GetServiceAccessErrorasEmailBody(serviceName, AnaTargetApplication.Grcs.ToString(), exception);
        }

        /// <summary>
        /// Gets the service access erroras email body.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>string which can be sent as Email</returns>
        public static string GetParametersOfServiceAccessError<T>(T parameters)
        {
            return String.Format("\n\nInput parameters to the service are as follows..\n{0}", EntityInformation.ToString(parameters));
        }

        #region Interface

        public enum InterfaceLogEntryType : byte
        {
            R2Service = 1,
            UpStreamNotification = 2,
            DownStreamNotification = 3,
            Email = 4,
        }

        public const string ArtistSearchAccountScope = "OWNED";
        public const string GdrsSupportedTerritories = "North America";

        #region Connection Entities

        public const string InterfaceDataEntities = "InterfaceDataEntities";

        #endregion

        public const string AppSettingsMPSchedularFileLogPath = "MP_NOTIFICS_LOG";
        public const string AppSettingsMPSchedularFileLogPathEnable = "MP_NOTIFICS_LOG_ENABLE";

        public const int ReviewReasonTypePcCompanyMismatch = 3;
        public const int ReviewReasonTypeRightSetMismatch = 5;
        public const int ReviewReasonTypeRightsTypeMismatch = 6;

        public const string AppSettingsGrsConfigurationInAna = "ANA_APPNAME_GRS";
        public const string AppSettingsR2ConfigurationInAna = "ANA_APPNAME_R2";

        #region Upstream Notification

        public const string Starting = "Starting ";
        public const string Stopping = "Stopping ";

        public const bool EnableIgnoreCase = true;

        public const string ServiceMarketSubscriberNameSpace =
            "http://itga.umusic.com/servicemarket/subscribermanager/v1";

        public const string ServiceMarketSubscriberDescription = "Receives Notification From Service Market";

        public const string CprsSubscriberNameSpace = "http://umusic.net/webservices/itdi/vanilla";
        public const string CprsSubscriberDescription = "Receives Notification From CPRS";

        public const string CprsSubscriberMessageInput =
            "Notification Message => Product Id: {0}, Area Of Interest: {1}, Message Creation Date: {2}";

        public const string UpStreamNotificationProcessor = "UpStream Notification Processor";
        public const string UpstreamNotificationProcessorType = "Starting UpStream Notification Processor Type: {0}";

        public const string ErrorInStartingMessageProcessor =
            "Error Starting UpStream Notification Processor : Exception {0}";

        #endregion Upstream Notification

        #region Grcs subscribers

        public const string AppSettingsCprsSubscriberUrl = "GRCS_CPRS_SUBSCRIBER_URL";
        public const string AppSettingsServiceMarketSubscriberUrl = "GRCS_SM_SUBSCRIBER_URL";

        #endregion Grcs subscribers

        #region OHI

        public const string BlcIDCreatorCPRS = "UMG.CPRS";
        public const string BlcIDCreatorGrcs = "UMG.GRCS";


        public const string AppSettingsOhiUserName = "OHI_USERNAME";
        public const string AppSettingsOhiPassword = "OHI_PASSWORD";

        #endregion

        #region Cprs

        public const string AppSettingsCprsUserName = "CPRS_USERNAME";
        public const string AppSettingsCprsPassword = "CPRS_PASSWORD";
        public const string FaultExceptionNoDataFoundInCprs = "NoDataException";

        #endregion Cprs

        #region R2Service

        #region Authentication

        public const string AppSettingsR2UserName = "R2_USERNAME";
        public const string AppSettingsR2Password = "R2_PASSWORD";

        #endregion Authentication

        #region URL

        public const string AppSettingsR2UrlResourceMaintainance = "R2_URL_RESOURCE_MAINTAINANCE";
        public const string AppSettingsR2UrlProjectMaintainance = "R2_URL_PROJECT_MAINTAINANCE";
        public const string AppSettingsR2UrlResourceSearch = "R2_URL_RESOURCE";
        public const string AppSettingsR2UrlPeopleSearch = "R2_URL_ARTIST";
        public const string AppSettingsR2UrlCompanySearch = "R2_URL_COMPANY";
        public const string AppSettingsR2UrlProjectSearch = "R2_URL_PROJECT";
        public const string AppSettingsR2UrlReleaseSearch = "R2_URL_RELEASE";
        public const string AppSettingsR2UrlLoader = "R2_URL_LOADER";
        public const string AppSettingsR2SearchMaxRows = "R2_SERVICE_MAXROWS";
        public const string AppSettingsR2ServiceTimeOut = "R2_SERVICE_TIMEOUT";
        public const string R2ServiceDefaultTimeOut = "00:20:00";

        #endregion URL

        #endregion R2Service

        #region Media Portal

        #region Authentication

        public const string AppSettingsMpUserName = "MP_USERNAME";
        public const string AppSettingsMpPassword = "MP_PASSWORD";

        #endregion Authentication

        #region URL

        public const string AppSettingsMPServiceUrl = "MP_SERVICE_URL";

        #endregion URL

        #endregion Media Portal

        public const string OracleReservedCharactersExpression =
            @"\,|\&|\=|\?|\{}|\-|\;|\~|\$|\!|\>|\*|\%";

        public const string AppSettingsR2HistoricalDateCheck = "R2_HISTORICAL_REPERTOIRE_DT";

        // Interface Services
        public const string InterfaceServiceAna = "ANA Service";

        public const string InterfaceServiceAnaDags = "ANA Service Populating DAG Type : {0}";
        public const string InterfaceServiceAnaRoles = "ANA Service Populating Role : {0}";
        public const string InterfaceServiceAnaTasks = "ANA Service Populating Task(s) : {0}";
        public const string InterfaceServiceMp = "Media Portal Service";

        // Interface Logging
        public const string InterfaceConnectionInit = "Connecting With {0}";

        public const string InterfaceConnectionSuccess = "Successfully Connected With {0}";
        public const string InterfaceConnectionTimer = "Recorded TimeSpan to fetch details from {0} is {1}";
        public const string InterfaceConnectionTimestamp = "TimeSpan Recorded at {0} is {1}";

        public const string InterfaceConnectionFailure = "Connecting with {0} Failure With Exception {1}";
        public const string InterfaceReturnsNull = "{0} Returns null Value";
        public const string InterfaceReturnsUnreadableFormat = "Cannot process the message from service {0}, Value {1}";
        public const string InterfaceReturnVal = "{0} Returned {1} Value(s)";
        public const string InterfaceReturnsZero = "{0} Returns Array Length Zero";
        public const string InterfaceArgNotSupported = "Method: {0} does not support an argument of this type: {1}";
        public const int ParamTypeId = 1;
        public const int ParamTypeSearch = 2;
        public const string InterfaceServiceR2 = "R2";
        public const string InterfaceServiceCprs = "CPRS";
        public const string InterfaceServiceGdrs = "GDRS";

        public const string InterfaceServiceR2PeopleSearch = "R2 People Search Service";
        public const string InterfaceServiceR2CompanySearch = "R2 Company Search Service";
        public const string InterfaceServiceR2ResourceSearch = "R2 Resource Search Service";
        public const string InterfaceServiceR2ReleaseSearch = "R2 Release Search Service";
        public const string InterfaceServiceR2ProjectSearch = "R2 Project Search Service";
        public const string InterfaceServiceR2ProjectMaintainance = "R2 Project Maintenance Service";
        public const string InterfaceServiceR2ResourceMaintainance = "R2 Resource Maintenance Service";
        public const string InterfaceServiceR2Loader = "R2 WS Loader Service";

        public const int MaxRowsR2Search = 100;

        // During Release and resource search, to Get the Artist Id we need to call R2 again giving the artist name. To reduce the count
        // restricted the search to only one.
        public const int MaxRowsArtistSubSearch = 3;

        public const string CharY = "Y";
        public const string CharN = "N";
        public const string CharE = "E";

        public const string CharUpcAuto = "A";
        public const string CharUpcManual = "M";
        public const string IsRightsTypeNonExc = "NONEXC";


        public const string R2ReleaseTypePhysicalIdentifier = "P";
        public const string R2ReleaseTypeMediaPortalIdentifier = "Media Portal";
        public const string R2ReleaseTypeGcsIdentifier = "Global Rights and Clearances";
        public const string R2ResourceTypeOthers = "OTHERS";

        public const string R2AliasIndicator = "Y";
        public const string R2NonAliasIndicator = "N";
        public const string R2ResourceHasSampleIdentifier = "Y";
        public const string R2PrimaryIndicator = "Y";
        //it is from SP please do not modify.
        public const string NotApplicable = "NA,NA,NA,";

        public const int R2SideArtistIndicator = 210;
        public const string R2GetXmlError = "Error in Parsing GetXml from R2 Service ";

        public const string R2GetXmlErrorContribution = "Error in Parsing GetXml from R2 Service Contribution";
        public const string R2GetXmlErrorReleases = "Error in Parsing GetXml from R2 Service Releases";
        public const string R2GetXmlErrorResources = "Error in Parsing GetXml from R2 Service Resource";
        public const string R2GetXmlErrorProject = "Error in Parsing GetXml from R2 Service Project {0}";

        public const string CprsSubscriberService = "CPRS Subscriber";
        public const string ErrorInMessageQueueTransaction = "Cannot commit an Enque Operation in MSMQ";
        public const string ErrorInMessageQueueSetup = "Cannot find MSMQ for Enqueuing this Message";
        public const string ErrorInRightsExpiry = "Error : ExpiryNotification ";

        // ReSharper disable RedundantDefaultFieldInitializer
        public const int CprsProductCode = 0;

        // ReSharper restore RedundantDefaultFieldInitializer

        public const char Comma = ',';

        public const char SemiColon = ';';
        public const char WhiteSpaceChar = ' ';
        public const string WhiteSpace = " ";
        public const string WildCardCharacter = "*";
        public const string WildCardValue = " {0} ";
        public const string AscendingOrder = " asc";
        public const string DescendingOrder = " desc";

        public const string R2TalentRoleTypeComposer = "COMPOSER";
        public const string R2TalentRoleTypeArtist = "ARTIST";
        public const string R2TalentRoleTypeWriter = "WRITER";

        public const string MpTestData = "MPTESTDATA";

        public const string NotificationXmlCdataPrefix = "<![CDATA[";
        public const string NotificationXmlCdataSuffix = "]]>";

        public const string EmbedXmlInCData = "<![CDATA[{0}]]>";

        public const string NotificationXmlSmDataNode = "Data";
        public const string NotificationXmlSmHeaderNode = "Body";
        public const string NotificationXmlSmUniqueIdElement = "IdSource";

        public const string XmlNameSpaceIdentifier = "xmlns";

        public const string GtaActiveStatus = "A";
        public const int TakeTwoHundred = 200;

        public const string ResourceCompletenessResultNode = @"complete(.*?)=(.*?)(.*)";
        public const string ResourceCompletenessResultNodeValue = @"\""([^\""]*)";
        public const string RegExpressionForXmlNameSpace = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";

        public const string R2ServiceException = "com.umusic.rms.ws.common.WSRMSException";
        public const string AnaNoChangeIndicator = "NoChanges";

        public const string InvalidArtistIdErrorCode = "9001";

        public const string InvalidArtistIdErrorMessage =
            "Invalid ArtisttalentnameId, GRCS does not hold Rights for the artist";

        public const string InvalidReleaseIdErrorCode = "9021";

        public const string InvalidReleaseIdErrorMessage =
            "Invalid Release Id, GRCS does not hold Rights for the Release";

        public const string InvalidIsrcIdErrorCode = "9011";
        public const string InvalidIsrcIdErrorMessage = "Invalid ISRC, GRCS does not hold Rights for the ISRC";
        public const string InvalidUpcIdErrorCode = "9022";
        public const string InvalidUpcIdErrorMessage = "Invalid UPC, GRCS does not hold Rights for the UPC";
        public const string InvalidResourceIdErrorCode = "9010";

        public const string InvalidResourceIdErrorMessage =
            "Invalid Resource Id, GRCS does not hold Rights for the resource";

        public const string InvalidProjectIdErrorCode = "9031";

        public const string InvalidProjectIdErrorMessage =
            "Invalid Project Id, GRCS does not hold Rights for the Project";

        #region upstream Notification

        public const int UpStreamServiceAdditionalTimeForStartUp = 10000;
        // ReSharper disable RedundantDefaultFieldInitializer
        public const int ReportoireInBoundQueue = 0;

        // ReSharper restore RedundantDefaultFieldInitializer

        public const int ArtistCompanyInboundQueue = ReportoireInBoundQueue + 1;
        public const int GtaInBoundQueue = ArtistCompanyInboundQueue + 1;
        public const int MediaPortalInBoundQueue = GtaInBoundQueue + 1;
        public const int GdrsCprsInBoundQueue = MediaPortalInBoundQueue + 1;
        public const int InboundQueueCount = GdrsCprsInBoundQueue + 1;

        public static readonly string[] AppSettingsInBoundQueueNames = new[]
                                                                           {
                                                                               "REPORTOIRE_INBOUND_QUEUE",
                                                                               "ARTISTCOMPANY_INBOUND_QUEUE",
                                                                               "GTA_INBOUND_QUEUE", "MP_INBOUND_QUEUE",
                                                                               "RELEASE_DATE_INBOUND_QUEUE"
                                                                           };

        public const string DownStreamReleaseNotification = "DownStream Release Notification";


        public enum UpStreamNotificationQueueId
        {
            None = InboundQueueCount,
            R2Repertoire = ReportoireInBoundQueue,
            R2ArtistCompany = ArtistCompanyInboundQueue,
            CprsAndGdrs = GdrsCprsInBoundQueue,
            ReleaseRights = MediaPortalInBoundQueue,
            Gta = GtaInBoundQueue
        };


        public const string ArtistNotificationService = "ARTIST_SERVICE";
        public const string PcCompanyNotificationService = "PCCOMPANY_SERVICE";
        public const string ProjectNotificationService = "PROJECT_SERVICE";
        public const string ReleaseNotificationService = "RELEASE_SERVICE";
        public const string ResourceNotificationService = "RESOURCE_SERVICE";
        public const string ReleaseDateNotificationService = "RELEASE_DATE_SERVICE";
        public const string GtaNotificationService = "GTA_SERVICE";

        public const string UpSteamNotificationProcessorConfigName = "NOTIFICATION_PROCESSOR_BINDING_CONFIG";

        public const string NotificationServiceTimeOut = "NOTIFICATION_SERVICE_TIMEOUT";
        public const string NotificationServiceRetryCount = "NOTIFICATION_SERVICE_RETRY_ATTEMPTS";
        public const string IsNotificationServiceSecure = "NOTIFICATION_SERVICE_ISSECURE";
        public const string NotificationServiceBindingType = "NOTIFICATION_SERVICE_BINDING_TYPE";

        public const string MessageQueueError = "Error in MSMQ {0}, Exception {1}";
        public const string NotificationProcessingError = "Exception while processing Notification Exception {0}";

        // project Work Queue constants

        public const string ProjectTaskName = "ProjectWorkQueue";
        public const string StatusType = "Active";
        public const string TaskType = "Priority";
        public const string ReleaseTaskName = "ContractLinkingRequired";

        public const string ArtistType = "MultiArtist";
        public const string ArtistType1 = "VariousArtist";
        public const string OrginatedFromGcs = "GRCS";

        #endregion upstream Notification

        public static readonly string[] CprsPhysicalReleaseScheduleTags = new[]
                                                                              {
                                                                                  "CPRSProductID",
                                                                                  "ProductUPC",
                                                                                  "ReleaseDate"
                                                                              };

        public static readonly string[] CPRSAllowedDateTimes = new[]
                                                                   {
                                                                       "dd/MM/yyyy HH:mm:ss",
                                                                       "yyyy-MM-ddTHH:mm:ss",
                                                                       "yyyyMMdd"
                                                                   };

        public static readonly string CPRSNotificationReleaseDateFormat = "yyyyMMdd";
        public static readonly string DshedNotificationReleaseDateFormat = "yyyy-MM-dd";

        public const char MediaPortalRightsExpiryDateSeperator = '/';

        public const string AppSettingsMpr2SyncInterval = "MP_R2_SYNC_INTERVAL";

        public const string AppSettingsMPSchedularNotificationInterval =
            "MP_SCHEDULAR_NOTIFICATION_INTERVAL";

        public const string AppSettingsPush2R2Interval =
            "PUSH2R2_SCHEDULAR_INTERVAL";

        public const string DigitalExploitationRights = "Digital Exploitation";
        public const string MobileExploitationRights = "Mobile Exploitation";
        public const string Adfunded = "Ad-funded";
        public const string All = "All";
        public const string NoRight = "No Rights";
        public const string PendingQaApproval = "Q";

        # endregion Interface
    }
}