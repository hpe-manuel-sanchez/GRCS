/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : Constants.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 10-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */
using UMGI.GRCS.BusinessEntities.Lookups;
namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Constants related to this project
    /// </summary>
    public class Constants
    {
        //Config Factory Constants
        public const string Binding = "binding";
        public const string IsSecured = "IsSecured";
        public const string ServiceTimeout = "ServiceTimeOut";
        public const string IsBindingInConfig = "IsBindingInConfig";
        public const string ContractService = "ContractService";
        public const string ArtistService = "ArtistService";
        public const string ProjectService = "ProjectService";
        public const string ReleaseService = "ReleaseService";
        public const string ResourceService = "ResourceService";
        public const string RightsService = "RightsService";
        public const string UserService = "UserService";
        public const string GrcsUtilityService = "GrcsUtilityService";
        public const string WorkQueueService = "WorkQueueService";
        public const string GlobalService = "GlobalService";
        public const string RepertoireService = "RepertoireService";
        public const string PCompanyService = "PCompanyService";
        public const string PageSize = "PageSize";
        public const string PageSizeValues = "PageSizeValues";
        public const string AppVersion = "AppVersion";
        public const string AppEnvironment = "AppEnvironment";
        public const string AppBuildDate = "AppBuildDate";
        public const string IsContractAdminModuleOnly = "IsContractAdminModuleOnly";
        public const string IsLocalDateTimeEnabled = "IsLocalDateTimeEnabled";
        //GCS
        public const string WorkgroupService = "WorkgroupService";
        public const string ClearanceInboxService = "ClearanceInboxService";
        public const string ClearanceProjectService = "ClearanceProjectService";
        public const string ClearanceResourceService = "ClearanceResourceService";
        public const string ClearanceReleaseService = "ClearanceReleaseService";
        public const string RoutingService = "RoutingService";
        //Service Factory Constants
        public const string BasicHttp = "BasicHttp";
        public const string NetTcp = "NetTcp";

        public const string BasicHttpBindingName = "httpBinding";
        public const string NetTcpBindingName = "tcpBinding";

        public const string MethodStart = "Method Entry: {0}";
        public const string MethodEnd = "Method Exit: {0}";
        public const string MethodDebugInfo = "Debugging Info: Method:{0}, Details:{1}"; //Method name{0} and Details{1}
        public const string MethodException = "Method:{0}";
        public const string MethodResult = "Result:{0}, Count:{1}";

        public const string UserName = "UserName";
        public const string NameSpace = "http://grcs.com";

        public const string AcceptEncoding = "Accept-Encoding";
        public const string Compression = "gzip, deflate";

        //Reports
        public const string ReportService = "ReportService";

        public const string ReportServerUrl = "ReportServerUrl";

        public const string ReportUserDomain = "ReportUserDomain";

        public const string ReportUserName = "ReportUserName";

        public const string ReportUserPassword = "ReportUserPassword";

        public const string ReportPath = "ReportPath";
        //END Reports

        public const int TakeTwoHundred = 200;
        public const int ClearanceInboxDefaultFolderSize = 3;
        public const string ExceptionWithinMethod = "Exception in Method: {0}";

        #region WorkGroupController Constant variables

        public const string ClearanceHomeView = "HomeClearence";
        public const string SearchWorkgroupView = "DeactiveWorkGroup";
        public const string CreateParentWorkgroupView = "CreateParentWorkgroup";
        public const string JsonOk = "OK";
        public const string JsonError = "ERROR";
        public const string ViewWorkgroup = "ViewWorkgroup";
        public const string ViewChildWorkgroup = "ViewChildWorkgroup";
        public const string Territory = " of ";

        public const string ManageUser = "ManageUsers";
        public const string ManageTerritory = "ManageTerritory";
        public const string NoData = "No data Available";
        public const string DeleteWorkgroupView = "DeleteWorkgroup";
        public const string MaintainParentWorkgroupView = "MaintainParentWorkgroup";
        public const string MaintainParentWorkgroupUpdateView = "MaintainParentWorkgroupEditView";
        public const string LinkWorkgroupToArtistContract = "LinkWGToArtistContract";
        public const string LinkWorkgroupToResourceContract = "LinkWGToResourceContract";
        public const string GcsUserName = "GRCS";
        public const string ContractInformation = "ContractInformation";
        public const string ManageCompanyView = "ManageCompany";
        public const string SearchWorkGroupView = "SearchWorkgroup";
        public const string MaintainChildWorkGroup = "MaintainChildWorkGroup";
        public const string CreateChildWorkGroupPartial = "CreateChildWorkGroupPartial";
        public const string CreateChildWorkgroupView = "CreateChildWorkgroup";
        public const string ManageArtistContractView = "ManageArtistContract";
        public const string ManageResourceContractView = "ManageResourceContract";
        public const string MimicUserView = "MimicUser";
        public const string SafeTerritory = "SafeTerritory";
        public const string AddRemoveUsers = "SearchWorkGroupToAddRemoveUsers";

        #endregion

        #region Clearance Inbox

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

        //Role Group
        public const string ClearanceInboxRoleGroup = "RLGRP";

        //Default number of projects that should be displayed in a folder
        public const long DefaultFolderSize = 20;

        //Default string length after which string clipping occurs
        public const int DefaultTextLength = 25;

        //Appender text to indicate that string has more part to it
        public const string TextOverflowIndicator = "...";

        public const long MaxLengthForFolderName = 255;

        //Reviewer Search Criteria - Assigned To - Id from Lookup Table
        public const long ReviewerSearchCriteriaAssignedTo = 8;

        //System folders to expand by default
        public const SystemFolder ReviewerPrimaryDefaultFolder = SystemFolder.ReviewerHighPriority;
        public const SystemFolder ReviewerSecondaryDefaultFolder = SystemFolder.ReviewerPending;
        public const SystemFolder RequestorPrimaryDefaultFolder = SystemFolder.RequestorHighPriority;
        public const SystemFolder RequestorSecondaryDefaultFolder = SystemFolder.RequestorSubmitted;
        public const SystemFolder RccAdminPrimaryDefaultFolder = SystemFolder.RCCAdminOrphan;
        public const SystemFolder RccAdminSecondaryDefaultFolder = SystemFolder.RCCAdminHighPriority;

        public const string ClearanceInboxRejectReason = "REASN";
        public const string ClearanceInboxWorkgroupUserOption = "<option value='{0}'>{1}</option>";

        //Requestor Roles
        public const string RequestorR2Authorized = "R2 Authorized Requestor";
        public const string RequestorUpcAllocation = "UPC Allocation Requestor";

        #endregion Clearance Inbox
    }
}
