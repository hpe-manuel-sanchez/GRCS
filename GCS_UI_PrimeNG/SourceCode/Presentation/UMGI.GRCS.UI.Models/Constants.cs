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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Constants used in this project
    /// </summary>
    public class Constants
    {
        public const string ContractService = "ContractService";
        public const string ArtistService = "ArtistService";
        public const string ReleaseService = "ReleaseService";
        public const string ProjectService = "ProjectService";
        public const string ResourceService = "ResourceService";
        public const string UserService = "UserService";
        public const string GrcsUtilityService = "GrcsUtilityService";
        public const string WorkQueueService = "WorkQueueService";
        public const string GlobalService = "GlobalService";
        public const string RepertoireService = "RepertoireService";
        public const string SelectReason = "Select Reason";
        public const string EmptyString = " ";
        public const string Select = " ";
        public const string SelectForDigitalRestrictions = "-- Select --";
        public const string Unlink = "Unlink";
        public const string ChildContract = "ChildContract";
        public const string LegalEditor = "Legal Editor";

        public const string DataEntry = "Data Entry";
        public const string Pending = "Pending Approval";
        public const string Approved = "Approved";
        public const string UnderAmend = "Under Amendment";
        public const string RightsContract = "Rights";


        public const string DataEntryEdit = "DataEntry";
        public const string PendingEdit = "PendingApproval";
        public const string ApprovedEdit = "Approved";
        public const string UnderAmendEdit = "UnderAmendment";
        public const string NewTemplate = "New";
        public const string UpdateTemplate = "Update";
        public const string UserServiceFail = "User service is not available";

        public const string SignedContract = "Signed Contract";
        public const string DealDraftContract = "Deal Memo/Draft Contract";

        public const string Canada = "Canada";
        public const string France = "France";
        public const string Germany = "Germany";
        public const string Japan = "Japan";
        public const string Netherlands = "Netherlands";
        public const string UnitedKingdom = "United Kingdom";
        public const string UnitedStates = "United States";

        public const string YesValue = "Yes";
        public const string NoValue = "No";
        public const string ProjectCode = "P";
        public const string ReleaseCode = "U";
        public const string ResourceCode = "I";
        public const string NoInActive = "No-Inactive";
        public const string YesInActive = "Yes-Active";
        public const int ValueOne = 1;
        public const int ValueTwo = 2; 

        public const string NameOnly = "Name Only";
        public const string Id = "Id";

        public const string MethodStart = "Method Entry: {0}";
        public const string MethodEnd = "Method Exit: {0}";
        public const string MethodDebugInfo = "Debugging Info: Method:{0}, Details:{1}"; //Method name{0} and Details{1}
        public const string MethodException = "Method:{0}";
        public const string MethodResult = "Result:{0}, Count:{1}";

        public const string Project = "Project";
        public const string Resource = "Resource";
        public const string Release = "Release";
        public const string ProjectAndRelease = "ProjectAndRelease";
        public const string AlreadyLinked = "AlreadyLinked";

        public const string WorkgroupService = "WorkgroupService";
        public const string ExceptionWithinMethod = "Exception in Method: {0}";
        public const string RCCAdmin = "RCC Admin";

        public static string RCCAdmin_Role = "RCC ADMIN";
        public static string Requestor_Role = "REQUESTOR";

        //Custom WorkQueue
        public const string DateFormat = "yyyy-MM-dd HH:mm:ss fffffff";

        public enum StatusType
        {

            Unsubmitted = 1,
            Submitted = 2,
            Cancelled = 3,
            Completed = 4,
            ReSubmitted = 5,
            ReOpened = 6

        };

        public enum ResourceType
        {

            Audio = 1,
            Image,
            Merchandise,
            Other,
            Text


        };

        public enum LiveStudioType
        {

            Live = 1,
            Studio

        };

        public enum PriceLevel
        {
            Top = 1,
            Mid = 3,
            Budget = 2
        }
      
        public const string ClearanceProjectService = "ClearanceProjectService";
        public const string ClearanceProjectType = "PROTY";
        public const string ClearanceRequestType = "MASTY";
        public const string ClearanceMusicType = "MUSTY";
        public const string ClearanceResourceType = "RESCE";      
        public const string ClearanceRecordingType = "LISTY";   
        public const string ClearancePriceLevelType = "PRLTY";
        public const string ClearanceICLALevelType = "ICLTY";

        public const string ClearanceClubPriceLevel = "CLBPR";
        public const string ClearacePromotionalPriceLevel = "PRMPR";

        public const string ClearanceCurrPriveLevelType = "PLCTY";
        public const string ClearanceReqPriceLevelLevelType = "PLRTY";

        public const string ClearanceReleaseService = "ClearanceReleaseService";
        public const string ClearanceResourceService = "ClearanceResourceService";   
        public const string ClearanceReleaseType = "ClearanceReleaseType";
        public const string RoutingService = "RoutingService";
        public const string ClearanceRegularRequestType = "REQTY";
        public const string ClearanceRoutingContract = "Clearance Routing Contract";


        # region "audit trial for Workgroup"
        public const string WGDeviations ="DEVIATIONS";
        public const string DisplayChildWG = "CHILD WORKGROUPS";
        public const string InquiryRole="INQUIRY";
        public const string LocalReviewerRole="LOCAL LABEL REVIEWER";
        public const string GlobalClearanceRole = "UMGI GLOBAL CLEARANCE";
        public const string MarketingReviewerRole = "UMGI MARKETING REVIEWER";
        public const string DisplayDefaultUsers = "DEFAULT USER(S)";
        public const string DisplayReviewerRights = "REVIEWER RIGHTS";
        public const string DisplayRequestorRights = "REQUESTOR RIGHTS";
        public const string DisplayTerritoryCountry = "TERRITORY/COUNTRY";
        public const string DisplayRCCRights = "RCC RIGHTS";
        public const string DisplayCompany = "COMPANY";
        public const string DisplayInquiryRights = "INQUIRY RIGHTS";
        public const string DisplayR2Authorized = "R2 AUTHORIZED";
        public const string DisplayManageWG = "MANAGE WORKGROUP RIGHTS";
        public const string DisplayUPCAllocation = "UPC ALLOCATION RIGHTS";

#endregion
    }
}
