/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ExcelAndWordOpenXmlConstants.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Rikhu
 * Created on     : 18-09-2012 
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
using System.Web;

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// class for Constants used in Word and Excel OpenXml
    /// </summary>
    public static class ExcelAndWordOpenXmlConstants
    {
        //--WordHelperOpenXML--
        //Table Constants
        public const int TableBorderSize = 1;
        public const int TableCellLeftMarginWidth = 40;
        public const int TableCellRightMarginWidth = 40;
        public const int FormKeyFontSize = 9;
        public const int FormValueFontSize = 8;
        public const string TableWidth = "5000";
        public const string TableCellTopMarginWidth = "40";
        public const string TableCellBottomMarginWidth = "40";
        public const string TableCellColorBackgroundShadeBlack = "EEECE1";
        public const string ColorAuto = "auto";
        public const string Left = "Left";
        public const string Right = "Right";
        //First Column Id is 1, Second as 2, Third as 1 and so on,
        //for creating a Key Value paired form with alternate colours
        public const int AlternateTableColumnId = 2;
        //Paragraph Constants
        public const int FontMultiplicationForOpenXml = 2;
        public const string FontArial = "Arial";
        public const string FontSizePageHeader = "28";
        public const string FontSizeSectionHeader = "20";
        public const string FontSizeParagraph = "18";
        public const string TableCellWidth = "1000";
        public const string TableCellBackgroundShadeColor = "EEECE1";
        //ContractDetails ID & WorkFlow Status
        public const string ContractInformation = "Contract Information";
        public const string ContractId = "Contract ID";
        public const string WorkFlowStatus = "Work Flow Status";
        public const string ParentContract = "Parent Contract";
        public const int MainDetailsFontSize = 10;
        //Parent Contract
        public const string Artist = "Artist";
        public const string ContractingParty = "Contracting Party";
        public const string ContractDescription = "Contract Description";
        public const string ClearanceCompany = "Clearance Company";
        public const string LocalRefNumber = "Local Ref No";
        public const string ParentContractId = "Parent Contract ID";
        //Split Deal
        public const string SplitDeal = "Split Deal";
        //Contract Header
        public const string ContractHeader = "Contract Header";
        public const string ArtistId = "Artist ID";
        public const string ArtistInLocalCharacters = "Artist in Local Characters";
        public const string ContractStatus = "Contract Status";
        public const string LocalContractRefNo = "Local Contract Ref No";
        public const string ContractCommencement = "Contract Commencement";
        public const string EndOfTerm = "End of Term";
        public const string OnActiveRoster = "On Active Roster";
        public const string ClearanceAdminCompanyCountry = "Clearance Admin Company-Country";
        public const string UmgSigningCompany = "UMG Signing Company";
        //Territorial Rights & Rights Period
        public const string TerritorialRightsAndRightsPeriod = "Territorial Rights & Rights Period";
        public const string TerritorialRightsDefinition = "Territorial Rights Definition";
        public const string InPerpetuity = "In Perpetuity";
        public const string LifeOfCopyright = "Life of Copyright";
        public const string OtherDuration = "Other Duration";
        public const string RightsExpiry = "Rights Expiry";
        public const string RightsReversion = "Rights Reversion";
        public const string Litigation = "Litigation";
        public const string RightsPeriod = "Rights Period";
        public const string ReleaseCommitmentAndRightsReversion = "Release Commitment & Rights Reversion";
        public const string RightsExpiryRule = "Rights Expiry Rule";
        public const string LostRightsIndicator = "Lost Rights Indicator";
        public const string LostRightsDate = "Lost Rights Date";
        public const string RightsExceptions = "Rights Exceptions";
        public const string RightsExceptionApplied = "Rights Exception Applied";
        public const string LostRightsReason = "Lost Rights Reason";
        //Repertoire Defaults
        public const string RepertoireDefaults = "Repertoire Defaults";
        public const string PCNoticeCompanyCountry = "P/C Notice Company-Country";
        public const string PCNoticeCompanyExtension = "P/C Notice Company Extension";
        public const string RightsType = "Rights Type";
        public const string LegalRightsReviewRequired = "Legal Rights Review Required";
        //Clearance Controls
        public const string ClearanceControls = "Clearance Controls";
        public const string ActiveForMarketing = "Active for Marketing";
        public const string SensitiveArtist = "Sensitive Artist";
        public const string ClearanceNotes = "Clearance Notes";
        //Notes & Notifications
        public const string NotesAndNotifications = "Notes & Notifications";
        public const string EmailNotificationRecipients = "Email Notification Recipients";
        public const string GeneralNotes = "General Notes";
        //Rights And Restrictions
        public const string NoRightsAndRestrictionsAvailable = "No Rights & Restrictions Available";
        public const string RightsAndRestrictions = "Rights & Restrictions";
        public const string RightsAcquisition = "Rights Acquisition";
        public const string AvailableRightsAcquisition = "Available Rights Acquisition";
        public const string Restricted = "Restricted";
        public const string Acquired = "Acquired";
        public const string Notes = "Notes";
        //"360° Deal"
        public const string Deal360 = "360° Deal";
        public const string ActiveOrPassive = "Active/Passive";
        public const string ActiveAndPassive = "Active & Passive";
        public const string Active = "Active";
        public const string Passive = "Passive";
        public const string Undefined = "undefined ";
        //Digital Restrictions
        public const string DigitalRestrictions = "Digital Restrictions";
        public const string ContentType = "Content Type";
        public const string UseType = "Use Type";
        public const string CommercialModels = "Commercial Models";
        public const string Restrictions = "Restrictions";
        public const string ConsentPeriod = "Consent Period";
        public const string Audio = "Audio";
        public const string Video = "Video";
        public const string Image = "Image";
        public const string Download = "Download";
        public const string Streaming = "Streaming";
        public const string DigitalMerhandise = "Digital Merchandise";
        public const string AlaCarte = "A la Carte";
        public const string Subscription = "Subscription";
        public const string AdFunded = "Ad-funded";
        public const string NoRights = "No Rights";
        public const string ConsentRequired = "Consent Required";
        public const string ReferToLegal = "Refer To Legal";
        public const string NoRestriction = "No Restriction";
        public const string Consult = "Consult";
        public const string DuringHoldbackPeriod = "During Holdback period";
        public const string DuringTerm = "During Term";
        public const string DuringAndAfterTerm = "During and After Term";
        //Secondary Exploitation
        public const string SecondaryExploitation = "Secondary Exploitation";
        public const string ExploitationType = "Exploitation Type";
        public const string HoldbackPeriod = "Holdback Period";
        public const string NoSecondaryExploitationAvailable = "No Secondary Exploitation Available";
        //Others
        public const string NotAvailable = "Not Available";
        public const string Yes = "Yes";
        public const string No = "No";
        public const string None = "None";
        public const string All = "All";
    }

    /// <summary>
    /// Enum used as Option Id, mostly used in Switch cases
    /// </summary>
    public enum OptionId
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five
    }
}