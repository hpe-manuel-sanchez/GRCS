/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : Constants.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 15-09-2012 
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

namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Constants used in this project
    /// </summary>
    public class Constants
    {
        //Report
        public const string ReportService = "ReportService";

        public const string ContractService = "ContractService";
        public const string ArtistService = "ArtistService";
        public const string ReleaseService = "ReleaseService";
        public const string ProjectService = "ProjectService";
        public const string ResourceService = "ResourceService";
        public const string UserService = "UserService";
        public const string RightService = "RightsService";
        public const string GrcsUtilityService = "GrcsUtilityService";
        public const string WorkQueueService = "WorkQueueService";
        public const string GlobalService = "GlobalService";
        public const string RepertoireService = "RepertoireService";
        public const string PCompanyService = "PCompanyService";
        public const string SelectReason = "Select Reason";
        public const string EmptyString = " ";
        public const string Select = " ";
        public const string SelectForDigitalRestrictions = "-- Select --";
        public const string Unlink = "Unlink";
        public const string ChildContract = "ChildContract";
        public const string LegalEditor = "Legal Editor";
        public const string LegalReviewer = "Legal Reviewer";
        public const string PowerUserForClearance = "Power User";

        public const string DataEntry = "Data Entry";
        public const string Pending = "Pending Approval";
        public const string Approved = "Approved";
        public const string UnderAmend = "Under Amendment";
        public const string RightsContract = "Rights";

        public const string ProjectCode = "P";
        public const string ReleaseCode = "U";
        public const string ResourceCode = "I";

        public const string DataEntryEdit = "DataEntry";
        public const string PendingEdit = "PendingApproval";
        public const string ApprovedEdit = "Approved";
        public const string UnderAmendEdit = "UnderAmendment";
        public const string NewTemplate = "New";
        public const string UpdateTemplate = "Update";
        public const string UserServiceFail = "User service is not available";
        public const string SearchContract = "SearchContract";
        public const string MaintenanceWorkQueue = "ContractMaintenanceWorkQueue";
        public const string SearchContractLower = "searchcontract";
        public const string MaintenanceWorkQueueLower = "workqueue";

        public const string SignedContract = "Signed Contract";
        public const string DealDraftContract = "Deal Memo/Draft Contract";
        public const string ClearanceRoutingContract = "Clearance Routing Contract";

        public const string Canada = "Canada";
        public const string France = "France";
        public const string Germany = "Germany";
        public const string Japan = "Japan";
        public const string Netherlands = "Netherlands";
        public const string UnitedKingdom = "United Kingdom";
        public const string UnitedStates = "United States";

        public const string YesValue = "Yes";
        public const string NoValue = "No";
        public const int ValueOne = 1;
        public const int ValueTwo = 2;
        public const int ValueTwoHundred = 200;
        public const int ValueZero = 0;
        public const int ValueThree = 3;




        public const string NoInActive = "No-Inactive";
        public const string YesInActive = "Yes-Active";

        public const string NameOnly = "Name Only";
        public const string Id = "Id";

        public const string ExceptionWithinMethod = "Exception in Method: {0}";
        public const string MethodStart = "Execution started";
        public const string MethodEnd = "Execution completed";
        public const string MethodDebugInfo = "Debugging Info: Method:{0}, Details:{1}"; //Method name{0} and Details{1}
        public const string MethodException = "Method:{0}";
        public const string MethodResult = "Result:{0}, Count:{1}";

        public const string Project = "Project";
        public const string Resource = "Resource";
        public const string Release = "Release";
        public const string ProjectAndRelease = "ProjectAndRelease";
        public const string AlreadyLinked = "AlreadyLinked";
        public const string PowerUser = "PowerUser";
        public const string PowerUserCheck = "Power User";
        public const string RccUser = "RCCUser";

        public const string UmgSigningCompanystr = "UMGSigningCompany";
        public const string UmgSigningCompany = "UmgSigningCompany";

        public const string Custom = "Custom";
        public const string PreDefined = "PreDefined";

        //Custom WorkQueue
        public const string EndDate = "Select \"Y\" Weeks";
        public const string StartDate = "Select \"X\" Weeks";
        public const string DateFormat = "yyyy-MM-dd HH:mm:ss fffffff";
        public const string IsDefaultLoad = "IsDefaultLoaded";
        public const string True = "True";
        public const string SearchRepertoire = "SearchRepertoire";
        public const string CopyContract = "CopyContract";
        public const string Null = "null";
        #region UC16

        public const string ExportExcelPath = "ExportExcelPath";
        public const string Digital = "Digital";
        public const string PreCleared = "PreCleared";
        public const string Secondary = "Secondary";
        public const string Tracks = "Tracks";
        public const string Releases = "Releases";
        public const string Merge = "merge";
        //public const string Extension = ".xls";
        //public const string ExcelExtension = ".xlsx";
        public const string Comma = ",";
        public const char TrimComma = ',';
        public const char CharSpace = ' ';
        public const char CharCap = '^';

        public const string DigitalRestrictionSearch = "DigitalRestrictionSearch";
        public const string StringMed = "med";
        public const string StringAsc = "ASC";

        public const int ValueTwelve = 12;
        public const int ValueFourteen = 14;

        public const string XlsConnection = ".xls";
        public const string XlsxConnection = ".xlsx";

        #endregion
        #region UC20-18-19
        public const string RightsAcquireGrid = "rightsAcquired";
        public const string DigitalRestrictionGrid = "digitalGrid";
        public const string SecondaryExploitationGrid = "secondaryExp";
        public const string PreClearanceGrid = "preClearanceGrid";

        public const string ReleaseRightsAcquireGrid = "releaseRightsAcquireGrid";
        public const string ReleaseDigitalRestrictionGrid = "releaseDigitalRestrictionGrid";
        public const string ReleaseParamsMultiArtistValue = "1";
        public const string ReleaseParamsOstValue = "2";
        public const string ReleaseParamsNonMacValue = "3";

        public const string PredefinedRightDataSetValue = "1";
        public const string PredefinedTrackRightDataSetValue = "2";
        public const string PredefinedTrackResourceRightDataSetValue = "3";
        public const string PredefinedTrackRightDataSetForTracksReleaseValue = "4";
        public const string PredefinedRightDataSetForResourceWithSamples = "5";
        public const string PredefinedRightDataSetForResourceWithSideArtistValue = "6";
        public const string PredefinedRightDataSetForResourcesWithDigitalRestrictionsConReq = "7";


        public const string ReleaseCustomTab = "1";
        public const string ReleasePredefinedTab = "2";

        public const string Added = "Added";
        public const string Updated = "Updated";
        public const string Deleted = "Deleted";

        public const string SideArtistIssues = "SideArtistIssues";
        public const string SampleIssues = "SampleIssues";
        public const string NoRights = "NoRights";
        public const string ArtistConsentRequired = "ArtistConsentRequired";
        public const string Other = "Other";

        public const string ExceptionApplied = "Applied";
        public const string ExceptionNotApplied = "Not Applied";
        public const string ExceptionNotAppliedTrim = "NotApplied";
        public const string YValue = "Y";
        public const string Nvalue = "N";

        public const string Applied = "Applied";
        public const string NotApplied = "Not&nbsp;Applied";

        public const string ListOneValue = "1";
        public const string ListTwoValue = "2";
        public const string ListThreeValue = "3";
        public const string ListFourValue = "4";
        public const string ListFiveValue = "5";
        public const string ListZeroValue = "0";
        public const string ValueNoChange = "111";
        public const string AscendingValue = "Ascending";
        public const string HtmlSpace = "&nbsp;";
        public const string strAmb = "amb;";
        public const string strAmbSymbol = "&";
        public const string NullEmptyString = "";
        public const string RightsAcquired = "RightsAcquired";
        public const string PreClearance = "PreClearance";
        public const string SecondaryExploitationPopup = "SecondaryExploitationPopup";
        public const string RightsWorkQueue = "RightsWorkQueue";
        public const string CreateCustomSetting = "CreateCustomSetting";
        public const string lstDropDown = "lstDropDown";
        public const string SecondaryExploitation = "SecondaryExploitation";
        public const string ResourceDigitalRestrictions = "ResourceDigitalRestrictions";
        public const string ReleaseRightsWorkQueue = "ReleaseRightsWorkQueue";
        public const string ResourceRightsWorkQueue = "ResourceRightsWorkQueue";
        public const string RightsAcquiredBulkEdit = "RightsAcquiredBulkEdit";
        public const string DigitalRestrictionsBulkEdit = "DigitalRestrictionsBulkEdit";
        public const string PreClearanceBulkEdit = "PreClearanceBulkEdit";
        public const string ClearanceAdminCountryPopup = "ClearanceAdminCountryPopup";
        public const string SecondaryExploitationBulkEdit = "SecondaryExploitationBulkEdit";
        public const string ReleaseRightsAcquired = "ReleaseRightsAcquired";
        public const string ReleaseDigitalRestrictions = "ReleaseDigitalRestrictions";
        public const string CustomRightsReviewWorkQueue = "CustomRightsReviewWorkQueue";
        public const string Bulk = "Bulk";
        public const string Single = "Single";
        public const char CharY = 'Y';
        public const char CharN = 'N';
        public const string UseType = "UseType";
        public const string RestrictionType = "RestrictionType";
        public const string ConsentPeriod = "ConsentPeriod";
        public const string CommercialModel = "CommercialModel";
        public const string updatedRecords = "updatedRecords";
        public const string addedRecords = "addedRecords";
        public const string deletedRecords = "deletedRecords";
        public const string Concurrencyproblems = "Last update failed on Concurrency problems";
        public const string totalrightsAcquiredData = "totalrightsAcquiredData";
        public const string totalSecondaryData = "totalSecondaryData";
        public const string totalPreclearanceData = "totalPreclearanceData";
        public const string totalreleaseRights = "totalreleaseRights";
        public const string NewForReview = "NewForReview";
        public const string FinalForReview = "FinalForReview";
        #endregion  UC20-18-19

        #region Reports
        public enum workflowStatus
        {
            DataEntry = 1,
            PendingApproval = 2,
            Approved = 3,
            UnderAmendment = 4
        };

        public const string DefaultSortColumn = "Artist";
        public const string Pagesize = "25";
        public const int PagesizeIntegerValue25 = 25;            
        public const string NoChangeRequired = "No Change Required";
        public const string FlagTrue = "true";
        public const string FlagFalse = "false";
        public const string TrueFlag = "True";
        //public const string FlagFalse = "false";
        public const string ALL = "All";

        public const string ActiveRoster = "ActiveRoster";
        public const string RightsExpiry = "RightsExpiry";
        public const string RightsExpiryToBeDetermined = "RightsExpiryToBeDetermined";
        public const string ReleaseCommitment = "ReleaseCommitment";
        public const string RightsAquired = "RightsAquired";
        public const string SecondaryExploitationRights = "SecondaryExploitationRights";
        public const string SecondaryExploitationSample = "SecondaryExploitationSample";
        public const string PreClearedResources = "PreClearedResources";
        public const string RollupStatus = "RollUpStatus";
        public const string ResourceRightDescripency = "ResourceRightsDiscrepancies";

        public const string UrlActiveRoster = "rptActiveRoster";
        public const string UrlRightsExpiry = "rptRightsExpiry";
        public const string UrlRightsExpiryToBeDetermined = "rptRightsExpiryToBeDetemine";
        public const string UrlReleaseCommitment = "rptReleaseCommitment";
        public const string UrlRightsAquired = "rptRightsAcquired";
        public const string UrlSecondaryExploitationRights = "rptSecondaryExploitationRights";
        public const string UrlSecondaryExploitationSample = "rptSecondaryExploitationSample";
        public const string UrlPreClearedResources = "rptPreClearedResources";
        public const string UrlResourceRightsDataDescripency = "rptResourceRightsDataDescripency";
        public const string UrlActiveRosterExcel = "rptActiveRosterExcel";
        public const string UrlRightsExpiryExcel = "rptRightsExpiryExcel";
        public const string UrlRightsExpiryToBeDetemineExcel = "rptRightsExpiryToBeDetemineExcel";
        public const string UrlrptReleaseRollUp = "rptReleaseRollUp";

        public const string ReportFormatExcel = "Excel";
        public const string ReportFormatPDF = "PDF";

        public const string SelectString = "--Select--";
        public const string workflow = "1,2,3,4";
        public const string RepertoireRelease = "Release";
        public const string RepertoireResource = "Resource";
        public const string ReportdateFormat = "MM/dd/yyyy";
        public const string ReportDBdateFormat = "yyyy-MM-dd";
        public const string ContractReportType = "ContractReport";

        public const string deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>";
        public const string ReportHeaderName = "Content-disposition";

        public const string ReportHeaderValuePDF = "attachment;filename=output.pdf";
        public const string ReportHeaderValueExcel = "filename=output.xls";

        public const string ContentTypePDF = "application/pdf";
        public const string ContentTypeExcel = "application/excel";

        public const string CountryPopUpView = "~/Views/Report/Country.cshtml";
        public const string CompanyPopUpView = "~/Views/Report/Company.cshtml";

        #endregion Reports


        #region UC26

        public const string DropDownSelect = "-- Select --";


        #endregion

       
    }
}
