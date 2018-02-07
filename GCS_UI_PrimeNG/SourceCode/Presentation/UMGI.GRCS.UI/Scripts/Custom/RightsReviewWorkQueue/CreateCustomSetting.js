/// <reference path="/GCS/Scripts/Custom/RightsReviewWorkqueue/RightsReviewWorkqueue.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/jquery-1.7.2.min.js" />
/// <reference path="/GCS/Scripts/Custom/jquery.js" />
/// <reference path="/GCS/Scripts/jquery-1.6.3.js" />
/// <reference path="/GCS/Scripts/jquery-1.8.0.js" />
/// <reference path="/GCS/Scripts/jquery-1.6.3.js" />
/// <reference path="/GCS/Scripts/jquery-1.8.2.js" />

var paramQuery = '';
var parameters = '';
var result = 'true';
var gridId = "workQueueGridCustom";
var id = null;

//Initialized Variable for Messages from Resource file
var createCustomSettingPopupMesages;

$(document).ready(function () {
    resizePopUp();
    createCustomSettingPopupMesages = window.rightsReviewWorkQueueMessages;
    $("#customSettingTabReview").tabs
    ({
        select: function (event, ui) {
            switch (ui.index) {
                case 0:
                    // first tab selected
                    resizePopUp();
                    $('#splitWarning').hide();
                    if ($('#hiddenReferrerPage').val() == createCustomSettingPopupMesages.resource) {
                        //$('#btnCreateRightsWorkQResource').show();
                        //$('#btnCancelCreateRightsWorkQResource').show();
                        $('#btnCreateRightsWorkQRelease').show();
                        $('#btnCancelCreateRightsWorkQRelease').show();
                    }
                    if ($('#hiddenReferrerPage').val() == createCustomSettingPopupMesages.release) {
                        $('#btnCreateRightsWorkQRelease').show();
                        $('#btnCancelCreateRightsWorkQRelease').show();
                    }
                    
                    $('#btnOkContractSearchTab').hide();
                    $('#btnCancelContractSearchTab').hide();
                    $(".clearBoth").show();
                    break;
                case 1:
                    // second tab selected
                    resizePopUp();
                    $('#splitWarning').hide();
                    if ($('#hiddenReferrerPage').val() == createCustomSettingPopupMesages.resource) {
                        //$('#btnCreateRightsWorkQResource').show();
                        //$('#btnCancelCreateRightsWorkQResource').show();
                        $('#btnCreateRightsWorkQRelease').show();
                        $('#btnCancelCreateRightsWorkQRelease').show();
                    }
                    if ($('#hiddenReferrerPage').val() == createCustomSettingPopupMesages.release) {
                        $('#btnCreateRightsWorkQRelease').show();
                        $('#btnCancelCreateRightsWorkQRelease').show();
                    }
                    $('#btnOkContractSearchTab').hide();
                    $('#btnCancelContractSearchTab').hide();
                    if ($('#divOtherParameterTab').html() == "") {
                        ReloadOtherParameterTab();
                    }
                    $(".clearBoth").show();
                    break;
                case 2:
                    resizePopUp();
                    //$('#btnCreateRightsWorkQResource').hide();
                    //$('#btnCancelCreateRightsWorkQResource').hide();
                    $('#btnCreateRightsWorkQRelease').hide();
                    $('#btnCancelCreateRightsWorkQRelease').hide();
                    $('#btnOkContractSearchTab').show();
                    $('#btnCancelContractSearchTab').show();
                    $(".clearBoth").hide();
                    break;
            }
        }

    });



  

    //TO Remove the Added tab
    $('#btnCancelContractSearchTab').click(function () {
        $('#liSearchContractTab').hide();
        $('#contractSearchPopup').hide();
        $("#customSettingTabReview").tabs('select', '#divOtherParameterTab');
    });


    $('#btnOkContractSearchTab').click(function () {
        var selectedReviewContracts = "";
        //To Select Multiple Records
        var selectedRowss = $('#searchContractGrid').jtable('selectedRows');
        if (selectedRowss.length > 0) {
            selectedRowss.each(function () {
                var records = $(this).data('record');
                var commencementDate;
                if (records.ContractCommencementDate == null) {
                    commencementDate = "NA";
                } else {
                    var re = /-?\d+/;
                    var m = re.exec(records.ContractCommencementDate);
                    var date = new Date(parseInt(m[0]));
                    var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                    commencementDate = date.getDate().toString() + ' ' + monthArray[date.getMonth("mmm")] + ' ' + date.getFullYear().toString();

                }
                if (selectedReviewContracts == "") {
                    if (records.ArtistName != "") {
                        selectedReviewContracts = records.ArtistName + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    } else {
                        selectedReviewContracts = records.ContractingParty + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    }
                } else {
                    if (records.ArtistName != "") {
                        selectedReviewContracts = selectedReviewContracts + ", " + records.ArtistName + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    } else {
                        selectedReviewContracts = selectedReviewContracts + ", " + records.ContractingParty + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    }
                }
            });
            $('#txtAreaLinkContract').val(selectedReviewContracts);
            $('#liSearchContractTab').hide();
            $('#contractSearchPopup').hide();
            $("#customSettingTabReview").tabs('select', '#divOtherParameterTab');
            return false;
        } else {
            $('#SearchMsgAlert').append("Please Select a Contract");
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            return false;
        }
    });

});




function ReloadOtherParameterTab() {

    $.get('/GCS/RightsReviewWorkQueue/CreateCustomSetting', function (data) {
        $('#divOtherParameterTab').html(data);
    });
}

function AddParameterQueryDiv() {
    //paramQuery = '<table class="custom"><tr><td>Parameter Query : </td><td class="bold">' + parameterPreDefined + '</td></tr></table>';
    paramQuery = '<div style="background-color:#fff;float:left;"><div><div style="padding:10px 0;font-size:1.1em;font-weight:normal;float:left;">PreDefined Parameter:</div><div style="padding:10px 0px 10px 10px;font-size:1.1em;font-weight:bold;color:#143489;float:left;">' + parameterPreDefined + '</div></div></div>';
}
var tabTitle = $("#tab_title"),
    tabContent = $("#tab_content"),
    tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close tabClose'>Remove Tab</span></li>"; //,
var tabs = $("#divRightsPriorityWorkQueueTab").tabs();

function addTab() {
    //    GetPreDefinedParameters();
    //    GetOtherParameters();
    //    CheckParameterTabs();

    //  if (ValidateSettingConfig()) {

    var id = "workQueueTabs-" + $.tab.totalTabCounter,
                label = tabTitle.val() || "Custom WQ ",

                li = $(tabTemplate.replace(/#\{href\}/g, "#" + id).replace(/#\{label\}/g, label)),
                tabContentHtml = tabContent.val() || "Custom Work Queue content.";

    tabs.find(".uitabWork").append(li);
   // gridId = id;

    if (IsPredefinedSelected) {
        AddParameterQueryDiv();
    }
    else {
        //  AddOtherParameterQueryDiv();
        AddParameterQueryDiv();
    }
    tabs.append("<div id='" + id + "'><p> hello" + $.tab.tabCounter + "<p></p></p></div>");

    $.post('/GCS/RightsReviewWorkQueue/PreClearance', function (data) {
        $('#' + id).html(data);
    });

    
    tabs.tabs("refresh");
    $.tab.tabCounter = $.tab.tabCounter + 1;
    $.tab.totalTabCounter = $.tab.totalTabCounter + 1;
    $.tab.customSettings.push({ id: $.tab.tabCounter, tab: id });
    $("#divRightsPriorityWorkQueueTab").tabs("select", id);
}
function resizePopUp() {
    var wid4 = $("#popupContractingPartyId").width();
    $("#clearanceCompSearchPopup").css("width", wid4 + 114);    
}

$(window).resize(function () {
    resizePopUp();
});

//Validating for Special Characters

//$("#RepertoireFilter_UPC, #RepertoireFilter_Artist, #RepertoireFilter_ReleaseTitle, #txtAdminCompany").keypress(function (e) {
//    ValidateSpecialCharacters(e, "<,>,&,\", ,\\t,%,;,(,),{,},!");
//});

$("#ISRC, #RepertoireFilter_UPC, #RepertoireFilter_Artist, #txtAdminCompany").keypress(function (e) {
    //ValidateSpecialCharacters(e);
});