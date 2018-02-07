/// <reference path="/GCS/Scripts/Custom/PriorityWorkQueue.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/PreDefinedClearanceAdminCompany.js" />
/// <reference path="/GCS/Scripts/Custom/SearchContractPopup.js" />

var paramQuery = '';
var parameters = '';
var result = 'true';
var gridId = "workQueueGridCustom";
var preDefinedParam = '';
var preDefinedParamText = '';
var repertoireTypeval = '';
//UC12 Predefind Parameters
var id = null;
var description = null;
var clearanceAdminCompanies = null;
var otherParamArtistName = null;
var preDefinedClearAdminCompny = null;
var lostRightsDateWindowStartDate = null;
var lostRightsDateWindowEndDate = null;
var parameterPreDefined = null;
var workQueueFilters = ',';
var IsPredefinedSelected = false;
var IsOtherParamSelected = false;
var RepertoireType = '';
var parameterPreDefined = '';
var isLoad = 0;
var Parmeter = '';
var parameterPredefinedId = '';
$(document).ready(function () {
   
    // reSizeWindow();
    reSizePriorityWorkQueuePage();
    $("#dataAdminCompanyLst").hide();
    $("#bottomResetSelection").hide();
    $('#OtherParamId').attr("disabled", true);
    $("#ParameterQuery").val("");
    $("#CustomWorkQueueSetting_DataAdminCompany").val("");
    $("#preDefinedParameters").change(function () {
        $("#crtWorkQueue").focus();
    });
    
    $("#LostRightsDateWindowStartDate").change(function () {
        $("#crtWorkQueue").focus();
    });
    $("#LostRightsDateWindowEndDate").change(function () {
        $("#crtWorkQueue").focus();
    });
    //AutoComplete Country  

    var clearanceDataAdminCompany = $("#ParameterQuery");
    $("input[data-autocomplete-source]").hide();
    clearanceDataAdminCompany.autocomplete({
        source: clearanceDataAdminCompany.attr("data-autocomplete-source-manual"),
        minLength: 2,

        select: function (event, ui) {
          
            $("#ParameterQuery").val(ui.item.value);
            $("#CustomWorkQueueSetting_DataAdminCompany").val(ui.item.id);
        },
        change: function (event, ui) {

            if (ui.item == null) {
                $("#ParameterQuery").val("");
            }
            else if (ui.item != null && $("#ParameterQuery").val() != ui.item.value) {
                $("#ParameterQuery").val("");
            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#ParameterQuery").val(ui.item.value);
                $("#CustomWorkQueueSetting_DataAdminCompany").val(ui.item.id);
            }
        }

    });

    $('#resetSelection').click(function () {
        $('#txtClearanceAdminCompPre').val('');
        $('#ClearenceListPre').find('tr').remove();
        $('#SelectedListPre').find('tr').remove();
        $('#labelSelCountPre').html($('#SelectedListPre').find('input:checkbox').length);
    });

    $('#linkClearPre').click(function () {
        $('#txtClearanceAdminCompPre').val('');
        $('#ClearenceListPre').find('tr').remove();
    });


});

$(function () {
    $("#preDefinedParameters").click(function () {

        var selectedParameter = $('#preDefinedParameters option:selected').val();
        var selectListCompany = $('#SelectedList').text();
        if (selectedParameter == "3") {
            $("#dataAdminCompanyLst").show();
            $("#bottomResetSelection").show();
            ResizePredefinedClearanceCompany();
        }
        else {
            $("#dataAdminCompanyLst").hide();
            $("#bottomResetSelection").hide(); 

        }
    });
});

function ResizePredefinedClearanceCompany() {
   
    if ($("#CustomSettingPopUp").is(':visible')) {
        if ($(".cacTableContainer").is(':visible')) {
            var TotDiaHgt = $("#CustomSettingPopUp").height();
            var ReduceHgt = TotDiaHgt - 240;
            $(".cacTable").css('height', ReduceHgt + "px");
        }
    };
}

function GetPreDefinedParameters() {
 
    parameterPreDefined = GetDropDownParameterValues('preDefinedParameters');
    preDefinedClearAdminCompny = GetParameterValues('txtClearanceAdminCompPre');
    preDefinedClearAdminCompny = getClearanceValuesPre();
    if (parameterPreDefined != '-Select-') {
        if (parameterPreDefined == 'Clearance Admin Company') {
            $('#preDefinedParameters option:selected').val(3);
            if (preDefinedClearAdminCompny.length > 1)
                IsPredefinedSelected = true;
            else {
                IsPredefinedSelected = false;
            }
        } else {
            //IsPredefinedSelected = true;
            if (parameterPreDefined == "Contract Status Deal Memo/ Draft Contract") {
                $('#preDefinedParameters option:selected').val(1);
                IsPredefinedSelected = true;
                //$('#preDefinedParameters option:selected').val(parameterPreDefined);
            }
            else if (parameterPreDefined == "Contract WorkFlow Status Under amendment/ Pending Approval") {
                $('#preDefinedParameters option:selected').val(2);
               // $('#preDefinedParameters option:selected').val(parameterPreDefined);
                IsPredefinedSelected = true;
            }
        }
        parameterPredefinedId = $('#preDefinedParameters').val();
    }
    else {
//        if (parameterPreDefined == 'Contract Status Deal Demo/ Draft Contract') {
//            IsPredefinedSelected = true;
//            $('#preDefinedParameters option:selected').val(1);
//        }
//        else if (parameterPreDefined == 'Contract WorkFlow Status Under amendment/ Pending Approval') {
//            $('#preDefinedParameters option:selected').val(2);
//            IsPredefinedSelected = true;
//        }
//        else {
            parameterPreDefined = '';
            preDefinedClearAdminCompny = '';
        //}
            parameterPredefinedId='';
    }
}

function GetOtherParameters() {
   
    id = GetParameterValues('OtherParamId');
    
    description = GetParameterValues('OtherParamDescription');
//    if (description.indexOf('\'') >= 0) {
//        description = description.replace(/'/g, '\"');
//    }

    clearanceAdminCompanies = GetParameterValues('CustomWorkQueueSetting_DataAdminCompany');
    
    otherParamArtistName = GetParameterValues('OtherParamArtistName');
//    if (otherParamArtistName.indexOf('\'') >= 0) {
//        otherParamArtistName = otherParamArtistName.replace(/'/g, '\"');
//    }

    lostRightsDateWindowStartDate = GetDropDownParameterValues('LostRightsDateWindowStartDate');
    
    if (lostRightsDateWindowStartDate == 'Select "X" Weeks') {
        lostRightsDateWindowStartDate = '';
    }
    lostRightsDateWindowEndDate = GetDropDownParameterValues('LostRightsDateWindowEndDate');
    if (lostRightsDateWindowEndDate == 'Select "Y" Weeks') {
        lostRightsDateWindowEndDate = '';
    }

    
   
    var splitVal = '';
    var cbProject = $('#projects');
    if (cbProject.is(':checked') != null) {
        RepertoireType = $('#projects').val();
       
    }
       

    var cbRelease = $('#releases');
    if (cbRelease.is(':checked') != null && cbProject.is(':checked') != null) {
        splitVal = '|';
        RepertoireType = RepertoireType + splitVal + $('#releases').val();
      
    }
    else if (cbRelease.is(':checked') != null) {
        RepertoireType = $('#releases').val();
    }
       

    var cbResources = $('#resources');
    if (cbResources.is(':checked') != null && cbProject.is(':checked') != null && cbRelease.is(':checked') != null) {
        splitVal = '|';
        RepertoireType += splitVal + $('#resources').val();

    } else if (cbResources.is(':checked') != null && cbRelease.is(':checked') != null) {
        splitVal = '|';
        RepertoireType += splitVal + $('#resources').val();

    }
    else if (cbResources.is(':checked') != null) {
        RepertoireType = $('#resources').val();
    }

    if (cbResources.is(':checked') == true || cbProject.is(':checked') == true || cbRelease.is(':checked') == true) {
        IsOtherParamSelected = true;
    }
    else if (id != '' || description != '' || clearanceAdminCompanies != '' || otherParamArtistName != '' ||
        lostRightsDateWindowStartDate != '' || lostRightsDateWindowEndDate != '') {
        IsOtherParamSelected = true;
    } else {
        IsOtherParamSelected = false;
    }

    
    preDefinedParamText = GetParameterValues('preDefinedParameters');
   // $('#preDefinedParameters option:selected').val(predefinedParamSelection);
    
 
//    if (RepertoireType != '' || RepertoireType != null) {
//       
//        if (cbProject.checked != null && RepertoireType=='Project') {
//            cbProject.is(':checked');
//        }
//        var cbReleases = $('#releases');
//        if (cbReleases.checked != null && RepertoireType == 'Release') {
//            cbReleases.is(':checked');
//        }

//        var cbResources = $('#resources');
//        if (cbResources.checked != null  && RepertoireType=='Resource') {
//            cbResources.is(':checked');
//        }
//    }
  

}

function ConstructParameterQuery() {
  
    paramQuery = 'Id:' + id + ' Description:' + description + ' Artist Name:' + otherParamArtistName + ' Data Admin Company:' + clearanceAdminCompanies + 'parameterPreDefined:' + parameterPreDefined + 'ClearanceAdminCompanies:' + preDefinedClearAdminCompny
    + 'lostRightsDateWindowEndDate:' + lostRightsDateWindowEndDate + 'lostRightsDateWindowStartDate:' + lostRightsDateWindowStartDate + 'filters:' + workQueueFilters + 'RepertoireType:' + RepertoireType;

}

function GetParameterValues(parameterName) {
    return $('#' + parameterName).val();
}

function GetDropDownParameterValues(parameterName) {
    //debugger;
    return $('#' + parameterName + '  option:selected').text();
}

function ValidateSettingConfig() {
   //debugger;
//    if (workQueueFilters.length < 4) {
    if (IsOtherParamSelected) {
        if (IsPredefinedSelected) {
            $('#SplitAlert').empty();
            $('#SplitAlert').append('Please define the Custom Parameters or select a Pre-defined Parameter');
            $('#SplitAlert').show();
            $('#splitWarning').show();
            return false;
        }
        else if (!IsPredefinedSelected && parameterPreDefined == 'Clearance Admin Company') {
            $('#SplitAlert').empty();
            $('#SplitAlert').append('Please define the Custom Parameters or select a Pre-defined Parameter');
            $('#SplitAlert').show();
            $('#splitWarning').show();
            return false;
        }
        return true;
    }
    if (IsPredefinedSelected) {
        if (IsOtherParamSelected) {
            $('#SplitAlert').empty();
            $('#SplitAlert').append('Please define the Custom Parameters or select a Pre-defined Parameter');
            $('#SplitAlert').show();
            $('#splitWarning').show();
            return false;
        }
        return true;
            }
         else if (!IsPredefinedSelected && parameterPreDefined != '') {
            // alert('Select either Pre-defined or other parameters');


            if (parameterPreDefined == 'Clearance Admin Company') {
                $('#SplitAlert').empty();
                $('#SplitAlert').append('Please select at least one Clearance Admin Company.');
                $('#SplitAlert').show();
                $('#splitWarning').show();
                return false;
            } else {
                return true;
            }
        } else if (!IsPredefinedSelected && repertoireTypeval == '') {
            $('#SplitAlert').empty();
            $('#SplitAlert').append('Please define the Custom Parameters or select a Pre-defined Parameter');
                $('#SplitAlert').show();
                $('#splitWarning').show();
                return false;
            }
            else if (!IsPredefinedSelected && repertoireTypeval != '') {
            return true;
        }
//            if (parameterPreDefined == 'Clearance Admin Company') {
//             
//                $('#SplitAlert').append('Please Select any one company in Predefined Parameter');
//            }
//            else {
//                $('#SplitAlert').append('Please Select either Pre-defined or Custom parameter');

//            }
//            $('#splitWarning').show();
//            $('#SplitAlert').show();

            //  objCustomSettingDialog.dialog('open');
          
        }


$(document).ready(function () {
    //Tab Change
   
    //addTab();
    //var stringParams = JSON.stringify(paramQuery.param);
    $("#customSettingTab").tabs
    ({
        select: function (event, ui) {//debugger;
          
            switch (ui.index) {

                case 0:
                
                    // first tab selected
                    $('#splitWarning').hide();
                    $("#bottomResetSelection").hide();

                    break;
                case 1:
                    // second tab selected
                    //GetParameterValues();
                    $('#splitWarning').hide();
                    if ($('#preDefinedParameters option:selected').val() == "3")
                        $("#bottomResetSelection").show();

                    //parameterPreDefined = GetParameterValues('preDefinedParameters');


                    if ($('#divOtherParameterTab').html() == "") {
                        //debugger;
                        ReloadOtherParameterTab();
                    }
                    //
                    //                    var predefinedParamSelection = GetParameterValues('preDefinedParameters');
                    //                    $('#preDefinedParameters option:selected').val(predefinedParamSelection);
                    //debugger;
                    var Predefinedvalue = $('#hdnPredefined').val();
                    if (Predefinedvalue == 'Contract Status Deal Demo/ Draft Contract' || Predefinedvalue == '1') {
                        $('#preDefinedParameters').val(1);
                        $("#dataAdminCompanyLst").hide();
                        $("#bottomResetSelection").hide();
                    }
                    if (Predefinedvalue == 'Contract WorkFlow Status Under amendment/ Pending Approval' || Predefinedvalue=='2') {
                        $('#preDefinedParameters').val(2);
                        $("#dataAdminCompanyLst").hide();
                        $("#bottomResetSelection").hide();
                    }
                    if (Predefinedvalue == 'Clearance Admin Company' || Predefinedvalue == '3') {
                        $('#preDefinedParameters').val(3);
                        $("#dataAdminCompanyLst").show();
                        $("#bottomResetSelection").show();
                        ResizePredefinedClearanceCompany();
                        $(".cacTable").css('height','200px');
                    }
                    if (Predefinedvalue == '') {
                        $('#preDefinedParameters').val(0);
                        $("#dataAdminCompanyLst").hide();
                        $("#bottomResetSelection").hide();
                    }
                    //
                    //$('#preDefinedParameters option:selected').val(2);
                    break;
            }
            //return false;


        }


    });
    //return false;
});

function AddParameterQueryDiv() {
    
    //paramQuery = '<table class="custom"><tr><td>Parameter Query : </td><td class="bold">' + parameterPreDefined + '</td></tr></table>';
    //paramQuery = paramQuery+ '<div style="background-color:#fff;float:left;"><div><div style="padding:10px 0;font-size:1.1em;font-weight:normal;float:left;">PreDefined Parameter:</div><div style="padding:10px 0px 10px 10px;font-size:1.1em;font-weight:bold;color:#143489;float:left;">' + parameterPreDefined + '</div></div></div>';
}

function AddOtherParameterQueryDiv() {
    
   
    var clearanceCac = GetParameterValues('ParameterQuery');
    var releaseDates = '';
    if (lostRightsDateWindowStartDate.length > 0) {
        releaseDates = 'Not less than ' + lostRightsDateWindowStartDate + '';
    }
    if (lostRightsDateWindowEndDate.length > 0) {
        if (releaseDates.length > 0) {
            releaseDates = releaseDates + ' and ';
        }
        releaseDates = releaseDates + ' greater than ' + lostRightsDateWindowEndDate + '';
    }
    if (releaseDates.length > 0) {
        releaseDates = releaseDates + ' weeks';
    }

    paramQuery = paramQuery + '<div style="float:left; padding:10px 0;">';
    if (id != '') {
        paramQuery = paramQuery + '<span class="alignleft"><span class="contractInfoPipe"></span><span class="InfoName">Custom Parameter:&nbsp;</span><span class="contractInfo" id="secUpc">' + id + '</span></span>'; ; 
    }
    if (otherParamArtistName != '') {
        paramQuery = paramQuery + '<span class="alignleft"><span class="contractInfoPipe"></span><span class="InfoName">Artist Name:&nbsp;</span><span class="contractInfo" id="secUpc">' + otherParamArtistName + '</span></span>'; ;
    }

    if (description != '') {
        paramQuery = paramQuery + '<span class="alignleft"><span class="contractInfoPipe"></span><span class="InfoName">Description:&nbsp;</span><span class="contractInfo" id="secUpc">' + description + '</span></span>'; ; ;
    }

    if (clearanceAdminCompanies != '') {
        paramQuery = paramQuery + '<span class="alignleft"><span class="contractInfoPipe"></span><span class="InfoName">Data Admin Company:&nbsp;</span><span class="contractInfo" id="secUpc">' + clearanceCac + '</span></span>'; ; ;    
    }
 
    var reportorType = '';
    var split = '';
    var cbProject = $('#projects');
    if (cbProject.is(':checked') != null) {
        if (cbProject.is(':checked')) {
            //debugger;
            reportorType = reportorType + split + "Project";
            split = '/';
        }
    }


    var cbRelease = $('#releases');
    if (cbRelease.is(':checked') != null) {
        if (cbRelease.is(':checked')) {
            reportorType = reportorType + split + "Release";
            split = '/';
        }
    }


    var cbResources = $('#resources');
    if (cbResources.is(':checked') != null) {
        if (cbResources.is(':checked')) {
            reportorType = reportorType + split + "Resource";
            split = '/';
        }
    }

    if (reportorType != '') {
        paramQuery = paramQuery + '<span class="alignleft"><span class="contractInfoPipe"></span><span class="InfoName">Repertoire Type:&nbsp;</span><span class="contractInfo" id="secUpc">' + reportorType + '</span></span>'; 
    }
  

    if (releaseDates != '') {
        paramQuery = paramQuery + '<span class="alignleft"><span class="contractInfoPipe"></span><span class="InfoName">Release Date Range:&nbsp;</span><span class="contractInfo" id="secUpc">' + releaseDates + '</span></span>';
    }

    //preDefinedParamText = GetParameterValues('preDefinedParameters');
    //debugger;
    if (parameterPreDefined != '') {
        paramQuery = paramQuery + '<span class="alignleft"><span class="contractInfoPipe"></span><span class="InfoName">Predefined Parameter:&nbsp;</span><span class="contractInfo" id="secUpc">' + parameterPreDefined + '</span></span>';     
    }
   
    
    //$('#preDefinedParameters option:selected').val(parameterPreDefined);
    
    
    paramQuery = paramQuery + '</div>';   
    //paramQuery = '<table class="custom"><tr><td>Custom Parameter: </td><td class="bold">Id</td><td>' + id + '</td><td class="bold">Artist Name</td><td>' + otherParamArtistName + '</td><td class="bold">Description</td><td>' + description + '</td><td class="bold">Data Admin Company</td>   <td>' + clearanceAdminCompanies + '</td><td class="bold">RepertoireType</td><td>' + RepertoireType + '</td><td class="bold">Release Date Range</td><td>' + releaseDates + '</td></tr><table>';
}

$(function () {

    $("#dataAdminCompanyLst").hide();

    var tabTitle = $("#tab_title"),
            tabContent = $("#tab_content"),
            tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close tabClose'>Remove Tab</span></li>"; //,

    var tabs = $("#priorityWorkQueueTab").tabs();

    // actual addTab function: adds new tab using the input from the form above
    function addTab() {

        GetPreDefinedParameters();
        GetOtherParameters();

        CheckParameterTabs();

        if (ValidateSettingConfig()) {

            var id = "workQueueTabs-" + $.tab.totalTabCounter,
                label = tabTitle.val() || "Custom WQ ",

                li = $(tabTemplate.replace(/#\{href\}/g, "#" + id).replace(/#\{label\}/g, label)),
                tabContentHtml = tabContent.val() || "Custom Work Queue" + $.tab.tabCounter + " content.";

            tabs.find(".ui-tabs-nav").append(li);
            gridId = id;
            //debugger;
            AddParameterQueryDiv();
            AddOtherParameterQueryDiv();
            //debugger;
            Parmeter = parameterPreDefined;
            //            if (IsPredefinedSelected) {
            //                AddParameterQueryDiv();
            //            }
            //            else {
            //                AddOtherParameterQueryDiv();
            //            }
            tabs.append("<div id='" + id + "'><p>" + paramQuery + "<p></p></p></div>");
            // tabs.append("<div id='" + id + "'></div>");
            InitializeWorkQueueJtable(gridId);
            tabs.tabs("refresh");
            $.tab.tabCounter = $.tab.tabCounter + 1;
            $.tab.totalTabCounter = $.tab.totalTabCounter + 1;
            $.tab.customSettings.push({ id: $.tab.tabCounter, tab: id, value: parameters });
            $("#priorityWorkQueueTab").tabs("select", id);
            var stringParams = JSON.stringify(getFilterParametersCheck());
            $('#' + id).append("<div id='hidden" + id + "' style='display:none'>" + stringParams + "</div>");

            //debugger;
            //if (parameters.param.parameterPreDefined == 1) {
            //$('#preDefinedParameters option:selected').val(2);
            //}

            return false;
        }
    }

    $("#divOtherParameterTab").change(function () {
        var paramLength = $('#divOtherParameterTab input:checked').length;
        var paramCheck = $('#divOtherParameterTab input:checked').val();
        if (paramLength == 1) {
            $('#OtherParamId').attr("disabled", false);
        } else {

            $('#OtherParamId').attr("disabled", true);
            $('#OtherParamId').val('');
            $('#LostRightsDateWindowStartDate').attr('disabled', false);
            $('#LostRightsDateWindowEndDate').attr('disabled', false);

        }
        if (paramCheck == 'project') {
            $('#LostRightsDateWindowStartDate').attr('disabled', true);
            $('#LostRightsDateWindowEndDate').attr('disabled', true);
        } else {
            $('#LostRightsDateWindowStartDate').attr('disabled', false);
            $('#LostRightsDateWindowEndDate').attr('disabled', false);
        }


    });

    tabs = $("#priorityWorkQueueTab").tabs({
        //collapsible: true,

        select: function (event, ui) {
            $('#Remove').attr('disabled', true);
            $('#hidFlag').val('Y');
            window.customTab = ui.panel.id;
            $("#hiddenTabSelectId").val(window.customTab.split('-')[1]);
            setTimeout("WorkQueueGridScroll(" + $("#hiddenTabSelectId").val() + ")", 1000);
            if (window.customTab == 'workQueueTabs-1') {
                window.customTab = '';
                $('#Remove').attr('disabled', false);
                $('#hidFlag').val('N');
                $('#CntrctLinkngRefresh').hide();
            }
            else
                $('#CntrctLinkngRefresh').show(); 

        }
    });

    //RefreshWorkQueue
    $('#refreshWorkQueuePre').unbind('click');
    $('#refreshWorkQueuePre').click(function (e) {
        reSizePriorityWorkQueuePage();
        e.preventDefault();

        //ar $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        var gridId = 'workQueueGrid';
        if (window.customTab != '') {
            gridId = customTab;
        }
        //get the work queue grid id
        refreshGrid();
        $('#' + gridId).jtable('load', { param: $('#hidden' + gridId).html() });

        return false;
    });

    // $('#WorkQueueSelectPre').unbind('click');
    $('#WorkQueueSelectPre').click(function (e) {

        return false;
    });
    //DropDown select
    $('#WorkQueueSelectPre select').change(function () {
        //debugger;
        HideWarningSuccess();
        //debugger;
        pageSize = $('#WorkQueueSelectPre select').val();

        //get the work queue grid id
        var gridId = 'workQueueGrid';
        if (window.customTab != '') {
            gridId = customTab;
        }
        $('#' + gridId).jtable({ 'pageSize': pageSize });
        //refreshGrid();
        $('#' + gridId).jtable('load', { param: $('#hidden' + gridId).html() });
        refreshGrid();
        return false;
    });


    $('#cancelWorkQueue').click(function () {
        objCustomSettingDialog.dialog('close');
    });

    $("#crtWorkQueue").unbind("click").click(function () {
        $('#Remove').attr('disabled', true);
        $('#WorkQueueSelectPre select').val(25);
        $("#buttonHide").show();
        //objCustomSettingDialog.dialog('open');
        if ($.tab.tabCounter == 9) {
            //$('#CustomSettingLink').attr("disabled", true);
            ShowWarning(priorityWorkQueueMessage.Only7CustomSettingsWarning);
            objCustomSettingDialog.dialog('close');
            return false;
        }
        else {
            //paramQuery = '';
            //GetPreDefinedParameters();
            getFilterParametersCheck();
            // ConstructParameterQuery();
            if (otherMaxValuesCheck()) {
                ValidateCustomSettings(parameters);
                //  debugger;
                if (result == "true") {
                    HideWarningSuccess();
                    addTab();
                    if (ValidateSettingConfig() == true) {

                        objCustomSettingDialog.dialog('close');
                    }

                    //reSizePriorityWorkQueuePage();
                } else {
                    if (isLoad == 0) {
                        var errorMsg = priorityWorkQueueMessage.WarningCustomSetting;
                        $('#SplitAlert').empty();
                        $('#SplitAlert').append(errorMsg);
                        $('#splitWarning').show();
                        $('#SplitAlert').show();
                    }
                }
            }
        }
        isLoad = 0;
        return false;
    });

    function otherMaxValuesCheck() {
        //debugger;
        var xValues = $('#LostRightsDateWindowStartDate option:selected').val();
        var yValues = $('#LostRightsDateWindowEndDate option:selected').val();
        if (parseInt(xValues) > parseInt(yValues)) {
            $('#endDateValidation').show();
            objCustomSettingDialog.dialog('open');
            return false;

        }
        return true;
    }

    //Resize page
    function reSizePriorityWorkQueuePage() {

        var h = $(window).height();
        $("#workQueueGridCustom").css('height', h - 185);
    }

    $("#priorityWorkQueueTab").unbind('click').live('click', function (e) {
        $("#buttonHide").hide();
        var panelId = e.target.getAttribute("href");
        if (panelId == "#workQueueTabs-1") {
            $("#buttonHide").hide();
        } else {
            $("#buttonHide").show();
        }
    });



    // close icon: removing the tab on click
    $("#priorityWorkQueueTab span.ui-icon-close").die('click').live('click', function () {

        var panelId = $(this).closest("li").remove().attr("aria-controls");
        $("#" + panelId).remove();
        DeleteCustomSettingFromCache(panelId);
        $.tab.tabCounter = $.tab.tabCounter - 1;
        tabs.tabs("refresh");
        $('#CustomSettingLink').attr("disabled", false);
        HideWarningSuccess();

    });
});

//Get filter parameters
function getFilterParametersCheck() {
   
    //var artistName = GetParameterValues('ArtistName');
    var contractDesc = GetParameterValues('ContractDescription');
    //var descTitle = GetParameterValues('Titles');
    //var adminCompany = GetParameterValues('AdminCompany');
    //UC12
    var id = GetParameterValues('OtherParamId');
    var description = GetParameterValues('OtherParamDescription');
    if (description.indexOf('\'') >= 0) {
        description = description.replace(/'/g, '\"');
    }

    //var clearanceAdminCompanies = GetParameterValues('ParameterQuery');
    var otherParamArtistName = GetParameterValues('OtherParamArtistName');
    if (otherParamArtistName.indexOf('\'') >= 0) {
        otherParamArtistName = otherParamArtistName.replace(/'/g, '\"');
    }

    var preDefinedClearAdminCompny = getClearanceIdsPre();

    var reasonForReviewFilterText = GetDropDownParameterValues('WorkQueues_ContractReviewReason');
    var artistFilterText = GetDropDownParameterValues('WorkQueues_ArtistName');
    var contractDescFilterText = GetDropDownParameterValues('WorkQueues_ContractDescription');
    var titleFilterText = GetDropDownParameterValues('WorkQueues_Title');
    

  
    var split = '';
    //debugger;
    var cbProject = $('#projects');
    if (cbProject != null) {
        if (cbProject.is(':checked') == true) { /* do something */
            repertoireTypeval = $('#projects').val();
          
        }
        
    }
   
    var cbReleases = $('#releases');
    if (cbReleases != null) {
        if (cbReleases.is(':checked') == true && cbProject.is(':checked') == true) {
            split = '|';
            repertoireTypeval = repertoireTypeval + split + $('#releases').val();
        }
       else if (cbReleases.is(':checked') == true) { /* do something */
         
            repertoireTypeval = $('#releases').val();
         
        }
    }

    var cbResources = $('#resources');
    if (cbResources != null) {
        if (cbResources.is(':checked') == true && cbProject.is(':checked') == true && cbReleases.is(':checked') == true) { /* do something */
            split = '|';
            repertoireTypeval = repertoireTypeval + split +$('#resources').val();

        }
        else if(cbResources.is(':checked') == true && cbReleases.is(':checked') == true) {
            split = '|';
            repertoireTypeval = repertoireTypeval + split + $('#resources').val();
        }
        else if(cbResources.is(':checked') == true) {
            repertoireTypeval = $('#resources').val();
        }
    }

    //debugger;
    var parameterPredefined = '';
    if($('#preDefinedParameters') != null)
    {
        if ($('#preDefinedParameters option:selected')) {
            parameterPredefined = GetDropDownParameterValues('preDefinedParameters');
            //parameterPredefined = $('#preDefinedParameters option:selected').val();
            parameterPredefinedId = $('#preDefinedParameters').val();
        }
    }
    parameterPredefinedId = $('#preDefinedParameters').val();
    //var parameterPreDefined = GetParameterValues('preDefinedParameters');
//    $('#preDefinedParameters option:selected').val(predefinedParamSelection);
   
    //debugger;
    //debugger;
   // CheckParameterTabs();
    //UC12
  
    var param = { param: { artistName: otherParamArtistName, artistFilterText: artistFilterText, contractDesc: contractDesc,
        contractDescFilterText: contractDescFilterText, titleFilterText: titleFilterText, descTitle: description,
        reasonForReview: reasonForReviewFilterText, adminCompany: preDefinedClearAdminCompny,
        //UC12
        otherParameterId: id, Description: description, otherClearanceAdminCompany: GetParameterValues('CustomWorkQueueSetting_DataAdminCompany'), otherParamArtistName: otherParamArtistName,
        lostRightsDateWindowStartDate: lostRightsDateWindowStartDate, lostRightsDateWindowEndDate: lostRightsDateWindowEndDate,
        parameterPreDefined: parameterPredefinedId, preDefinedClearAdminCompny: preDefinedClearAdminCompny,
        workQueueFilters: workQueueFilters, RepertoireType: repertoireTypeval
    }
    };
    parameters = param;
    return param;
}

function CheckParameterTabs() {
    $('#divOtherParameterTab input:checked').each(function () {
        //workQueueFilters = workQueueFilters + ',';
        workQueueFilters += this.value;
    });
}

function ValidateCustomSettings(paramQuery) {
    //debugger;
    $.each($.tab.customSettings, function () {
        this.value.param.workQueueFilters = ','; // for checking existing parameters
        if (paramQuery.param.lostRightsDateWindowEndDate == null && paramQuery.param.lostRightsDateWindowStartDate == null) {
            paramQuery.param.lostRightsDateWindowEndDate = '';
            paramQuery.param.lostRightsDateWindowStartDate = '';
           
        }
      //  debugger;
        if (JSON.stringify(this.value.param).toLowerCase() == JSON.stringify(paramQuery.param).toLowerCase()) {
            //debugger;
            result = 'false';
            return false;
        } else {
            result = 'true';
        }


        //                if (this.value.param.RepertoireType.toUpperCase() == paramQuery.param.RepertoireType.toUpperCase()) {
        //                    debugger;
        //                    result = 'false';
        //                }

        //        var checkParam1 = JSON.stringify(this.value.param).toLowerCase();
        //        var checkParam2 = JSON.stringify(paramQuery.param).toLowerCase();

        //        var strparam1 = checkParam1.split(',');
        //        var strparam2 = checkParam2.split(',');

        //        for (var i = 0; i < 15; i++) {
        //            var checkParam1 = strparam1[i].split(':');
        //            var checkParam2 = strparam2[i].split(':');

        //            if (checkParam1[1].toLowerCase().trim() == checkParam2[1].toLowerCase().trim()) {
        //                result = 'false';
        //            }

        //        }

        //        //debugger;
        //        var repType1 = strparam1[17].replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, ''); 
        //        var repType2 = strparam2[18].split(':');
        //        //var test1 = repType2[1].toLowerCase().trim().html(repType2[1].toLowerCase().trim().replace(/&[^;]+;/g, ''));
        //        var stt = repType2[1].replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
        //        if (repType1.toLowerCase().trim() == stt.toLowerCase().trim()) {
        //            result = 'false';
        //        } else {
        //            result = 'true';
        //        }

        //        var selectedRepertoire = '';
        //        var cbProject = $('#projects');
        //        if (cbProject != null) {
        //            if (cbProject.is(':checked') == true) { /* do something */
        //                selectedRepertoire = $('#projects').val();
        //            }
        //        }
        //        var cbReleases = $('#releases');
        //        if (cbReleases != null) {
        //            if (cbReleases.is(':checked') == true) { /* do something */
        //                selectedRepertoire = $('#releases').val();
        //            }
        //        }

        //        var cbResources = $('#resources');
        //        if (cbResources != null) {
        //            if (cbResources.is(':checked') == true) { /* do something */
        //                selectedRepertoire = $('#resources').val();
        //            }
        //        }
        //        debugger;
        //        if (result == 'false' && selectedRepertoire.toUpperCase() == this.value.param.RepertoireType.toUpperCase()) {
        //            result = 'false';
        //        } else {
        //            result = 'true';
        //        }
        return true;

    });
}

function DeleteCustomSettingFromCache(panelId) {

    $.each($.tab.customSettings, function () {
        if (this.tab != null) {
            if (this.tab.toLowerCase() == panelId.toLowerCase()) {
                $.tab.customSettings.pop(this);
            }
        }
    });
    //    var initTabCounter = 2;
    //    var tempTabSettings = $.tab.customSettings;
    //    var tempTabSettings1 = [];
    //    var tabId = 'workQueueTabs-';
    //    $.each(tempTabSettings, function () {
    //        initTabCounter = initTabCounter + 1;
    //        tempTabSettings1.push({
    //            id: this.id,
    //            tab: this.tab,
    //            value: this.value
    //        });
    //    });
    //    $.tab.tabCounter = initTabCounter;
    //    $.tab.customSettings = tempTabSettings1;
    //    var res = $('ui-tabs-nav').find('*workQueueTabs-');

}

function ReloadOtherParameterTab() {
    //debugger;
    $.get('/GCS/WorkQueue/CustomWorkQueueSetting', function (data) {
        //debugger;
        $('#divOtherParameterTab').html(data);
    });
}

function InitializeWorkQueueJtable() {
    //Jq Grid WorkQueue
    var rowIndex = -1;
    var pageSize = 25;
    $('#' + gridId).jtable
            ({
                paging: true,
                pageSize: pageSize,
                sorting: true,
                defaultSorting: 'Type ASC',
                selecting: true,
                multiselect: true,
                selectingCheckboxes: true,
                selectOnRowClick: true,
                loadingRecords: function () {
                    $('.jtable .jtable-no-data-row').hide();
                    //$(".ui-jtable-loading").show();
                    $('#loadingDiv').show();
                    setTimeout(function () { $(".ui-jtable-loading").hide(); }, 5000);
                    $(".ui-jtable-loading").removeClass("jtable-row-selected");
                    $(".ui-jtable-loading").find('input:checkbox').removeAttr("checked");
                },
                recordsLoaded: function (event, data) {
                    rowIndex = data.serverResponse.TotalRecordCount;
                    if (data.serverResponse.TotalRecordCount >= 1000) {
                        alert("The search results matching the search criteria exceed from 1000 results. Please refine your search criteria and try again");
                    }
                    //document.getElementById("PriorWorkQueueCount").innerHTML = "WorkQueue Filter (" + rowIndex + ")";
                    $('.jtable .jtable-no-data-row').show();
                    //$(".ui-jtable-loading").hide();
                    $('#loadingDiv').hide();
                    $(this).find(" input").removeAttr("checked");
                    $(this).find(" tr").removeClass("jtable-row-selected");

                    setToolTip(this);
                    resultItem = $('#' + gridId).html();
                    var Tabid = $.tab.totalTabCounter;
                    var gridid = Tabid - 1;
                    $("#hiddenTabSelectId").val(gridid);
                    WorkQueueGridScroll(gridid);
                    
                },
                actions: {
                    listAction: '/GCS/WorkQueue/WorkQueueFilter'
                },
                fields: {
                    'ReType': {
                        title: priorityWorkQueueMessage.Type,
                        display: function (test) {
                            var image = "";
                            if (test.record.ResourceType == "Project") {
                                //image = $('<img  src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img src="/GCS/Images/Project.png" title="Project" >');
                                image = $('<span class="imgProject" title="Project"></span>' + '<span  id="alert" style="display:none;" class="imgAlert" ></span>');
                            }
                            else if (test.record.ResourceType == "Release") {
                                //image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/Release.png" title="Release">');
                                if ((test.record.ScopeType == "1") && (test.record.IsPackage == "0")) {
                                    //image = $('<img  src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/Physical_Release.gif" title="PhysicalRelease">');
                                    image = $('<span class="imgPhysicalRelease" title="PhysicalRelease"></span>' + '<span id="alert" style="display:none;"class="imgAlert" ></span>');
                                }
                                if ((test.record.ScopeType == "1") && (test.record.IsPackage == "1")) {
                                   // image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/package_physical.gif" title="PhysicalPackage">');
                                    image = $('<span class="imgPhysicalPackage" title="PhysicalPackage"></span>' + '<span id="alert" style="display:none;" class="imgAlert" ></span>');
                                }
                                if ((test.record.ScopeType == "2") && (test.record.IsPackage == "0")) {
                                    //image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/Digital_Release.gif" title="DigitalRelease">');
                                    image = $('<span class="imgDigitalRelease" title="DigitalRelease"></span>' + '<span id="alert" style="display:none;" class="imgAlert" ></span>');
                                }
                                if ((test.record.ScopeType == "2") && (test.record.IsPackage == "1")) {
                                    // image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/package_digital.gif" title="DigitalPackage">');
                                    image = $('<span class="imgDigitalPackage" title="DigitalPackage"></span>' + '<span id="alert" style="display:none;" class="imgAlert" ></span>');
                                }
                            }
                                
                            else if (test.record.ResourceType == "Resource") {

                                if (test.record.RepertoireSubType == "Audio") {
                                    //image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/ResourceAudio.png" title="Audio">');
                                    image = $('<span class="imgResourceAudio" title="Audio"></span>' + '<span id="alert" style="display:none;"class="imgAlert" ></span>');
                                }
                                else if (test.record.RepertoireSubType == "Video") {
                                    //image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/ResourceVideo.png" title="Video">');
                                    image = $('<span class="imgResourceVideo"  title="Video"></span>' + '<span id="alert" style="display:none;" class="imgAlert" ></span>');
                                }
                                else if (test.record.RepertoireSubType == "Image") {
                                    //image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/ResourceImage.png" title="Image">');
                                    image = $('<span class="imgResourceImage" title="Image"></span>' + '<span id="alert" style="display:none;"class="imgAlert" ></span>');
                                }
                            }
                            return image;
                        },
                        sorting: false,
                        listClass: 'workQAlertImages'
                    },

                    Type: {
                        title: priorityWorkQueueMessage.TaskId,
                        width: '100px'
                    },
                    Title: {
                        title: priorityWorkQueueMessage.Title

                    },
                    VersionTitle: {
                        title: priorityWorkQueueMessage.VersionTitle

                    },
                    ArtistName: {
                        title: priorityWorkQueueMessage.ArtistName
                    },
                    ReleaseDate: {
                        title: priorityWorkQueueMessage.ReleaseDate,
                        type: 'date',
                        displayFormat: $.datepicker.regional[''].dateFormat

                    },
                    CommencementDate: {
                        type: 'date',
                        displayFormat: $.datepicker.regional[''].dateFormat,
                        list: false
                    },

                    //                    ContractReviewReason: {
                    //                        title: priorityWorkQueueMessage.ContractReviewReason

                    //                    },
                    CompanyName: {
                        title: priorityWorkQueueMessage.CompanyName

                    },

                    LinkedContractName: {
                        title: priorityWorkQueueMessage.LinkedContractName,
                        sorting: false,
                        display: function (test) {
                            var image = "";

                            if (test.record.ContractId != null) {
                                image =  $('<span class="imgLinkedContract" ></span>');
                                var dateString;
                                try {
                                    var re = /-?\d+/;
                                    var m = re.exec(test.record.CommencementDate);
                                    var date = new Date(parseInt(m[0]));
                                    var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                    dateString = date.getDate().toString() + ' ' + monthArray[date.getMonth("mmm")] + ' ' + date.getFullYear().toString();
                                } catch (e) {
                                    dateString = "NA";
                                }


                                if (test.record.ContractArtistName == '' || test.record.ContractArtistName == undefined) {
                                    $(image).prop("title", "-" + "<b>" + test.record.ContractingParty + "</b><br><b>" + dateString + '</b>');
                                } else {
                                    $(image).prop("title", "-" + "<b>" + test.record.ContractArtistName + "</b><br><b>" + dateString + '</b>');
                                }
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            } else {
                                image = '';
                            }

                            $(image).click(function () {
                                window.location.href = "/GCS/Contract/EditContract/" + test.record.ContractId;
                            });

                            return image;
                        }
                    },
                    PYear: {
                        title: priorityWorkQueueMessage.PYear

                    },
                    OwnedReleases: {
                        title: priorityWorkQueueMessage.OwnedReleases

                    },
                    OwnedResources: {
                        title: priorityWorkQueueMessage.OwnedResources

                    }
                },
                //Register to selectionChanged event to hanlde events
                selectionChanged: function () {
                    HideWarningSuccess();
                    //Get all selected rows
                    var selectedRows = $('#' + gridId).jtable('selectedRows');
                    // $('#workQueueGrid').jtable('selectedRows');
                    if (selectedRows.length > 0) {
                        selectedRows.each(function () {
                            var record = $(this).data('record');
                            $('#hiddenContractId').val(record.ContractId);

                        });
                    }
                }

            });
    if (gridId != 'workQueueGridCustom') {
        $('#' + gridId).jtable('load', { param: JSON.stringify(getFilterParametersCheck()) });
    }
}
$('input:text').keydown(function (event) {
    //debugger;
    //alert("Keypress keycode:" + event.keyCode);
    //alert("lenth:" + $('#roleGroupName').val().toString().length);
    if (event.keyCode == 13) {
        $('#crtWorkQueue').click();
        isLoad = 1;
        return false;
    }
    return true;
});

//function refreshCustomGrid() {
//    $('#' + gridId).jtable({ recordsLoaded: function (event, data) {
//        rowIndex = data.serverResponse.TotalRecordCount;
//        document.getElementById("PriorWorkQueueCount").innerHTML = "Work Queue Filter (" + rowIndex + ")";
//        $('.jtable .jtable-no-data-row').show();
//        $(".ui-jtable-loading").hide();
//        $('#loadingDiv').hide();
//        $('#' + gridId+" input").removeAttr("checked");
//        $('#' + gridId+" tr").removeClass("jtable-row-selected");
//        setToolTip(this);
//        pageScroll();

//    }
//    });
//}

$("#LostRightsDateWindowEndDate, #LostRightsDateWindowStartDate, #resources, #releases, #projects, #preDefinedParameters").bind("mouseup", HideWarningSuccess);
//$("#LostRightsDateWindowEndDate").bind("mouseup", pageScroll);
$("#OtherParamDescription, #OtherParamArtistName, #ParameterQuery, #txtClearanceAdminComp ")
          .bind("keyup", HideWarningSuccess);
function saveCustomworkqueue(str) {
    //debugger;
    $("#buttonHide").show();
    if ($.tab.tabCounter == 9) {
        ShowWarning(priorityWorkQueueMessage.Only7CustomSettingsWarning);
        objCustomSettingDialog.dialog('close');
        return false;
    }
    else {
        //getFilterParametersCheck();
        // ConstructParameterQuery();
        //if (otherMaxValuesCheck()) {
        //            ValidateCustomSettings(parameters);
        //            debugger;
        //            if (result == "true") {
        //                HideWarningSuccess();
        //                //addTab();
        ////                if (ValidateSettingConfig() == true) {

        ////                    objCustomSettingDialog.dialog('close');
        ////                }
        //                //reSizePriorityWorkQueuePage();
        //            } else {
        //                if (str == 1) {
        //                var errorMsg = priorityWorkQueueMessage.WarningCustomSetting;
        //                ShowWarning(errorMsg);
        //                }
        //                
        //            }

        objCustomSettingDialog.dialog('close');
    }

    return true;
}
