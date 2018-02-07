/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : GlobalConstants.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 02-08-2012 
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

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Constants used in this project
    /// </summary>
    public class GlobalConstants
    {        
        public const string ControlVisible = "V";

        //UnAuthorized
        public const string UnAuthorized = "UnAuthorized";
        public const string RedirectUnAuthorized = "~/UnAuthorized.htm";

        //Create Contract
        public const string ContractView = "Contract";
        public const string SearchContractView = "SearchContract";
        public const string ContractTabView = "ContractTab";
        public const string RightsAndRestrictionsTabView = "RightsAndRestrictionsTab";
        public const string SecondaryExploitationTabView = "SecondaryExploitationTab";
        public const string ContractTemplateView = "CreateContract";
        public const string TerritorialRightsView = "TerritorialRights";
        public const string ContractArtistId = "Contract.ArtistId";
        public const string IsInActiveRoster = "Contract_IsContractInActiveRoster";
        public const string IsActiveForMarketing = "Contract_IsActiveForMarketing";
        public const string IsClearance = "Contract_IsClearanceRoutingContract";
        public const string IsLegalRightsReview = "Contract_IsLegalRightsReviewRequired";
        public const string IsLossRights = "Contract_IsLossRightsIndicator";
        public const string IsSensitive = "Contract_IsSensitiveArtist";
        public const string PcNoticeCountryCompanyId = "hiddenPCNoticeCountryCompanyId";
        public const string PcNoticeSelectedCountryCompanyId = "Contract.PcNoticeCompanyId";
        public const string ClearanceCompanyId = "ClearanceCompanyCountryId";
        public const string YesValue = "0";
        public const string IsActiveRosterValue = "2";
        public const string DigitalRestrictionTemplateId = "TemplateId";

        // view names for template maintenance
        public const string ContractTemplatesView = "ContractTemplates";
        public const string DigitalRestrictionTemplatesView = "DigitalRestrictionTemplates";
        public const string DigitalRestrictionView = "DigitalRestriction";
        public const string ContractTemplateName = "Contract.TemplateName";
        public const string DigitalTemplateName = "Right.DigitalTemplateName";
        public const string DigitalTemplateId = "Right.DigitalTemplateId";

        public const string JsonOk = "OK";
        public const string JsonError = "ERROR";
        public const string Territory = " of ";
        public const string NameExists = "exists";
        public const string LoadTemplate = "1";
        
        //WorkFlow Status
        public const string DataEntry = "Data Entry";
        public const string Pending = "Pending Approval";
        public const string Approved = "Approved";
        public const string UnderAmend = "Under Amendment";
        public const string DataEntryEdit = "DataEntry";
        public const string PendingEdit = "PendingApproval";
        public const string ApprovedEdit = "Approved";
        public const string UnderAmendEdit = "UnderAmendment";

        //UserRole
        public const string PowerUser = "PowerUser";
        public const string Reviewer = "Reviewer";
        public const string Editor = "Editor";
        public const string ReadOnlyUser = "ReadOnlyUser";
        public const string ReadOnlyBasicUser = "ReadOnlyBasicUser";

        //Add or Remove Parent Contract\Child Contract
        public const string AddParentFlag = "flag";
        public const string AddParentContractId = "LinkedContractId";
        public const string Unlink = "Unlink";
        public const string ChildContract = "ChildContract";
        public const string ParentContract = "ParentContract";

        //Split Deal Contract
        public const string SplitContract = "Split";
        public const string SplitContractId = "SplitContractId";
        public const string SplitCancel = "Cancel";

        //Email\Address Book
        public const string WorkflowStatusCheckForTasks = "Approved";
        public const string EmailExists = "Exists";
        public const string EmailSuccess = "success";
        public const string EmailFail = "Fail";
        public const string EmailError = "Error in SaveEmailGroup";
        public const string AddRoleGroupView = "AddRoleGroup";
        public const string ManageAddressBookView = "ManageAddressBook";
        public const string DeleteGroup = "groupId";
        public const string EmailReceipients = "Contract.EmailReceipients";
        public const string EmailReceipientIds = "Contract.EmailReceipientIds";

        //Manage Template
        public const string ContractTemplateId = "Contract.TemplateId";
        public const string IsNewTemplate = "isNewTemplate";
        public const string IsNewTemplateTrue = "true";
        public const string DeleteTemplates = "D";

        //SplitDeal
        public const string LinkSplitContractId = "SplitContractId";
        public const string UnLinkSplitContract = "flag";

        //Tasks
        public const string Tasks = "tasks";

        //Search Contract
        public const string ExportToWordContracts = "Contracts";
        public const string ResponseContentType = "application/vnd.ms-excel";
        public const string ResponseContentDisposition = "Content-Disposition";
        public const string AttachmentExcel = "attachment; filename=Report.xls";
        public const string ResponseContentWord = "attachment;filename=Contract_Information_ID_";
        public const string DocExtension = ".doc";
        public const string ResponseContentTypeWord = "application/vnd.ms-word.document";

        //Priority WorkQueue
        public const string ContractLinkingWorkView = "ContractLinkingWorkQueue";
        public const string RerouteResource = "RerouteResource";
        public const string Success = "success";
        public const string Fail = "Fail";

        public const string UnlinkAssociatedResources = "UnlinkAssociatedResources";
        public const string PropagateLinking = "PropagateLinking";
        public const string PropagateLinkingFromProjectToRepertoire = "PropagateLinkingFromProjectToRepertoire";
        public const string PropagateLinkingReleaseToResource = "PropagateLinkingReleaseToResource";

        //Search Repertoire
        public const string ArtistName = "ArtistName";
        public const string Artist = "Artist";
        public const string PCompanyName = "PCompanyName";
        public const string PNoticeCompany = "PNoticeCompany";
        public const string ProjectTab = "ProjectTab";
        public const string ReleaseTab = "ReleaseTab";
        public const string ResourceTab = "ResourceTab";
        public const string SearchRepertoire = "SearchRepertoire";
        public const string ResourceContractLinkingAlert = "ResourceContractLinkingAlert";
        public const string ReleaseContractLinkingAlert = "ReleaseContractLinkingAlert";

        //Menu
        public const string ClearanceMenu = "ClearanceMenu";
        public const string Home = "Home";
        public const string Index = "Index";
		public const string UserName = "UserName";

        //DateFormat
        public const string DateOnlyFormat = "dd MMM yyyy";
        //DatePickerHelper
        public const string Date4S = "dddd";
        public const string Date3S = "ddd";
        public const string Date2C = "DD";
        public const string Date1C = "D";
        public const string Month4C = "MMMM";
        public const string Month3C = "MMM";
        public const string Month2C = "MM";
        public const string Month1C = "M";
        public const string Month2S = "mm";
        public const string Month1S = "m";
        public const string Year4S = "yyyy";
        public const string Year2S = "yy";
        public const string Year1S = "y";
    }
}
