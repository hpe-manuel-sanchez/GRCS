var savedUsers = "";
var DefaultUsers = "";
var ManageCompany = "";
var selRoleTypeText = "";
var companyValues = "";
var workGroupUserNames = '';
var maintainWorkgroupRoleId = "";
var fromPage = "";
var removedAllRows = "";
var defaultPageSize = 10;
var isDynamicScriptLoadedForManageUser = "false";
var isDynamicScriptLoadedForManageCompany = "false";
var workGroupId = "";
var isManageUserSearchResetClick = "false";
var maintainChildCompanyIds = "";
var showWGDetails = false;
$('.ui-dialog-titlebar-close').attr("title", "Close");

function getDefaultUserColumnVisibilty() {
    if ($('#hdnMaintainWorkGroup').length == 0) {
        if ($("#RolesList > option:selected").attr("value") != "8" && $("#RolesList > option:selected").attr("value") != "9" && $("#RolesList > option:selected").attr("value") != "") {
            return 'visible';
        }
        else {
            return 'hidden';
        }
    }
    else {
        if (maintainWorkgroupRoleId != "8" && maintainWorkgroupRoleId != "9") {
            return 'visible';
        }
        else {
            return 'hidden';
        }
    }
}

/* Manage company partial view changes */
function LoadViewAfterDynamicScriptLoadedForManageCompany() {
    if (isDynamicScriptLoadedForManageCompany == "true") {
        Reset();
        ResetForSearchCompany();
        $('#addedManageCompanyResults').jtable('destroy');
        if (workGroupId.length > 0) {
            pagingCount = 10;
        }
        if ($('#hdnSavedCompanyValues').val() != "") {
            $('#spnMgCompanyAddedResultLabel').html('');
            $('#spnMgCompanyAddedResultLabel').hide();
            $("#divSearchCompanyPaging").show();
        }
        else {
            if (workGroupId.length > 0) {
                LoadAddedSearchJtable('companiesforworkgroup');
                $("#divSearchCompanyPaging").show();
            }
        }
    }
}

function ShowManageCompanyPopup() {
    //Manage Company popup Loading if already company Added
    var objDialog = $('#manageCompanyContainer')
        .dialog({
            autoOpen: true,
            modal: true,
            title: 'Manage Company',
            show: 'clip',
            hide: 'clip',
            width: 1000,
            resizable: false,
            beforeClose: function (event, ui) {
                if ($('#btnRemove').is(':visible')) {
                    ResetForSearchCompany();
                    return true;
                }
                else {
                    $("#managecompany").show();
                    $("#searchCompanyPopup").hide();
                    if ($('#addedManageCompanyResults').innerHTML != "") {
                    }
                }
            }
        });
    objDialog.load('/GCS/Workgroup/ManageCompanyPopup?parentPage=' + fromPage + "&parentWorkgroupId=" + workGroupId);
    isDynamicScriptLoadedForManageCompany = "true";
    moveDialogBoxAtTop();
}
/*End Manage company partial view changes */

/*Manage Users partial view changes */
function ShowManagerUsersPopup() {
    var objDialog = $('#manageUsers')
                 .dialog({
                     autoOpen: true,
                     modal: true,
                     title: 'Manage Users of Workgroup',
                     show: 'clip',
                     hide: 'clip',
                     resizable: false,
                     width: 1000
                 });
}

function ResetManageUsers() {
    jQuery("#username").watermark(watermarkUserName);
    jQuery("#userid").watermark(watermarkUserId);
    jQuery("#usercountry").watermark(watermarkCountry);
}
/* End Manage Users partial view changes */

function CancelClicked(buton) {
    window.location.href = "/GCS/workgroup/MaintainParentWorkgroup";
}

function ValidateEmail(emailAdd) {
    var filter = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,3}(;|$)/;
    if (filter.test(emailAdd)) {
        return false;
    }
    else {
        return true;
    }
}

function LoadwatermarkForManageUsers() {
    jQuery("#username").watermark(watermarkUserName);
    jQuery("#userid").watermark(watermarkUserId);
    jQuery("#usercountry").watermark(watermarkCountry);
    jQuery("#username1").watermark(watermarkUserName);
    jQuery("#userid1").watermark(watermarkUserId);
    jQuery("#usercountry1").watermark(watermarkCountry);
}

function LoadManageCompanytable(companyvalues) {
    $('#companyList').jtable({
        paging: false,
        sorting: false,
        defaultSorting: 'Company ASC',
        selecting: false,
        columnResizable: false,
        multiselect: true,
        selectingCheckboxes: false,
        selectOnRowClick: false,
        actions: {
            listAction: '/GCS/WorkGroup/AddCompany'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Name: {
                title: mgCompJtableColNames.CompName,
                width: '40%'
            },
            ISACCode: {
                title: mgCompJtableColNames.ISACCode,
                width: '20%'
            },

            CountryName: {
                title: mgCompJtableColNames.CountryName,
                width: '25%'
            }
        }
    });
    $('#companyList').jtable('load', {
        companyIds: companyvalues,
        deletedCompIds: '',
        isSort: 'true'
    });
}

function LoadTerritories(territories, key) {
    var objTerritorialPopup = $('<div id="Territory"></div>')
                    .html('<p>' + 'Loading' + '</p>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        title: manageTerritoryTitle,
                        show: 'clip',
                        hide: 'clip',
                        width: "98%",
                        height: 510,
                        minHeight: 510,
                        close: function () {
                            $(this).remove(); // ensures any form variables are reset.
                        }
                    });
    objTerritorialPopup.html('<p>' + 'Loading' + '</p>');
    var IsCallFromWorkgroup = true;
    var postData = {
        territory: territories,
        id: key,
        IsCallFromWorkgroup: IsCallFromWorkgroup
    };

    objTerritorialPopup.load(
        $.ajax({
            url: '/GCS/Territory/GetTerritories/',
            type: 'POST',
            dataType: 'html',
            async: true,
            data: JSON.stringify(postData),
            cache: false,
            success: function (data) {
                if (data == 'error') {
                    objTerritorialPopup.html('<p>' + 'Error' + '</p>');
                }
                else {
                    objTerritorialPopup.html(data);
                }
            },
            contentType: 'application/json; charset=utf-8'
        }));
    objTerritorialPopup.dialog('option', { title: manageTerritoryTitle });
    //Open Dialog
    objTerritorialPopup.dialog('open');
}

function moveDialogBoxAtTop() {
    var dialogue = $('.ui-dialog')

    dialogue.animate({
        top: "40px"
    }, 0);
}

function getWorkgroupAuditHistory(AuditTypeId) {
    var SelectedItemIds = [];
    var displayTitle;
    SelectedItemIds.push($('#hdnWorkgroupId').val());
    if ($('#txtWorkgroupName').attr("value").length > 0) {
        displayTitle = $('#txtWorkgroupName').attr("value");
    }
    else {
        displayTitle = '';
    }
    displayWGAuditTrail(AuditTypeId, SelectedItemIds, displayTitle, selectedWorkgroupRole, true);
    return false;
}

$(document).ready(function (e) {
    if (showWGDetails == true) {
        document.getElementById('btncreate').style.visibility = 'visible';
        document.getElementById('btncancel').style.visibility = 'visible';
    }
    $('#btnManageCompany').click(function () {
        jGridsList["searchCompanyResults"] = [];
        $('#btnRemove').hide();
        $('#btnSave').hide();
        $('#btnCancel').hide();
        $('#ddlCompanyPaging').val(defaultPageSize);
        $("#popupMgCompanyerrorMessage").html('');
        $("#popupMgCompanyerrorMessage").hide('');
        $('#addedManageCompanyResults').jtable('destroy');
        $('#searchCompanyResults').jtable('destroy');
        $('#spnMgCompanyAddedResultLabel').html('');
        $('#spnMgCompanyAddedResultLabel').hide();
        if ($('#hdnMaintainWorkGroup').length != 0) {
            if (companyValues != "") {
                ShowManageCompanyPopup();
                jsonObj = companyValues;
                if (removedAllRows == "true" && companyValues == "") {
                    companyValues = "";
                    LoadAddedSearchJtable('aftersave');
                }
                else if (removedAllRows == "true" && companyValues != "") {
                    LoadAddedSearchJtable('aftersave');
                }
            }
            else {
                ShowManageCompanyPopup();
                $('#spnMgCompanyAddedResultLabel').html('');
                $('#spnMgCompanyAddedResultLabel').hide();
                $('#addedManageCompanyResults').jtable('destroy');
                $('#btnManageSave').hide();
                $('#btnManageCancel').hide();
                $('#btnManageRemove').hide();
            }
        }
        else if ($('#hdnSavedCompanyValues').val() != "" && $('#hdnSavedCompanyValues') != null) {
            ShowManageCompanyPopup();
            $('#addedManageCompanyResults').jtable('destroy');
        }
        else {
            if ($('#hdnMaintainWorkGroup').length == 0) {
                if ($('#hdnSavedCompanyValues').val() == "") {
                    //Manage Company popup Loading for CreateParent
                    var objDialog = $('#manageCompanyContainer').dialog({
                        autoOpen: true,
                        modal: true,
                        title: 'Manage Company',
                        show: 'clip',
                        hide: 'clip',
                        width: 1000,
                        resizable: false,
                        beforeClose: function (event, ui) {
                            if ($('#btnRemove').is(':visible')) {
                                $('#spnMgCompanyAddedResultLabel').html('');
                                $('#spnMgCompanyAddedResultLabel').hide();
                                return true;
                            }
                            else {
                                $("#managecompany").show();
                                $("#searchCompanyPopup").hide();
                                $('#popupMgCompanyerrorMessage').hide();
                                if ($('#addedManageCompanyResults').data().jtable == null) {
                                    $('#spnMgCompanyAddedResultLabel').html('');
                                    $('#spnMgCompanyAddedResultLabel').hide();
                                }
                            }
                        }
                    });
                    $('#btnOpenSearchComp').focus();
                    if ($('#hdnSavedCompanyValues').val() == "" && isDynamicScriptLoadedForManageCompany == "false") {
                        $('#addedManageCompanyResults').jtable('destroy');
                        objDialog.load('/GCS/Workgroup/ManageCompanyPopup?parentPage=' + fromPage + "&parentWorkgroupId=" + workGroupId);
                        isDynamicScriptLoadedForManageCompany = "true";
                        $('#spnMgCompanyAddedResultLabel').html('');
                        $('#spnMgCompanyAddedResultLabel').hide();
                    }
                    else {
                        $('#addedManageCompanyResults').jtable('destroy');
                        if ($('#addedManageCompanyResults').data().jtable == null) {
                            $('#spnMgCompanyAddedResultLabel').html('');
                            $('#spnMgCompanyAddedResultLabel').hide();
                        }
                        LoadViewAfterDynamicScriptLoadedForManageCompany();
                        Loadwatermark();
                        $('#btnManageSave').hide();
                        $('#btnManageCancel').hide();
                        $('#btnManageRemove').hide();
                    }
                }
            }
        }
        moveDialogBoxAtTop();
    });

    /*Manage Users partial view changes */
    $('#RolesList').change(function () {
        $("#divUsers").html('');
        $('#hdnSavedUserValues').val(""); DefaultUsers = '';
        $('#hdnSavedInRoleUserValues').val("");
        $('#hdnSavedMngWkpUserValues').val("");
        $('#hdnSavedR2UserValues').val("");
        $('#hdnSavedUPCAllocUserValues').val("");
    });

    $("#divUsers").show();
    $('#btnManageUsers').click(function () {
        if ($('#hdnMaintainWorkGroup').length == 0) {
            if ($("#RolesList > option:selected").attr("value") == null ||
                $("#RolesList > option:selected").attr("value") == "" ||
                $("#RolesList > option:selected").attr("value") == "--Select Role--" ||
                $("#RolesList > option:selected").attr("value") == "-1") {
                $('#RolesList').addClass('input-validation-error');
                $("#errorMessage").show();
                $("#errorMessage").html("Please Select Workgroup Role");
                $('#RolesList').focus();
                return false;
            }
            else {
                $('#RolesList').removeClass('input-validation-error');
                $("#errorMessage").hide();
                $("#errorMessage").html('');
            }
        }
        ResetManageUsers();
        $('#username').value = "";

        LoadwatermarkForManageUsers();
        $("#popupMuCompanyerrorMessage").html('');
        $("#popupMuCompanyerrorMessage").hide();
        $('#addedManageUserResults').jtable('destroy');

        var objDialog = $('#manageUsers').dialog({
            autoOpen: true,
            modal: true,
            title: 'Manage Users of Workgroup',
            show: 'clip',
            hide: 'clip',
            width: 1000,
            resizable: false,
            beforeClose: function (event, ui) {
                if ($('#btnAddUser').is(':visible') || isManageUserSearchResetClick == "true") {
                    $("#mainsearch").show();
                    ResetManageUsers();
                    $("#addedManageUserResults").show();
                    $('#popupMuCompanyerrorMessage').hide();
                    if ($('#addedManageUserResults').data().jtable == null) {
                        $("#mainbutton").hide();
                        $('#spnMgUserAddedResultLabel').html('');
                        $('#spnMgUserAddedResultLabel').hide();
                    }
                    else {
                        if ($('#addedManageUserResults').is(':visible')) {
                            if ($('#addedManageUserResults').data().jtable._totalRecordCount != 0) {
                                $("#mainbutton").show();
                            }
                            else {
                                $("#mainbutton").hide();
                            }
                            $('#spnMgUserAddedResultLabel').html(listOfAddedMgUsers);
                            $('#spnMgUserAddedResultLabel').show();
                        }
                    }
                    $("#searchUserPopup").hide();
                    isManageUserSearchResetClick = "false";
                    return false;
                }
                else {
                    return true;
                }
            }
        });

        $('#btnOpenSearchUser').focus();

        savedUsers = $('#hdnSavedUserNameValues').val();
        if ($('#hdnSavedUserValues').val() != "") {
            $('#manageUsers').jtable('destroy');
            $("#addedManageUserResults").show();
            $("#mainbutton").show();
            LoadAddedUsersJtable('afterSave');
            $("#mainsearch").show();
            ResetManageUsers();
        }
        else if ($('#hdnSavedUserValues').val() == "" && $('#hdnMaintainWorkGroup').length == 0) {
            $('#manageUsers').jtable('destroy');
            $('#spnMgUserAddedResultLabel').html('');
            $('#spnMgUserAddedResultLabel').hide();
            $("#addedManageUserResults").hide();
            $("#mainbutton").hide();
            $("#searchUserPopup").hide();
            $("#mainsearch").show();
            ResetManageUsers();
            ShowManagerUsersPopup();
            if ($('#hdnSavedUserNameValues').val() == "" && isDynamicScriptLoadedForManageUser == "false") {
                objDialog.load('/GCS/Workgroup/OpenManageUsers');
                isDynamicScriptLoadedForManageUser = "true";
            }
        }

        if ($('#hdnMaintainWorkGroup').length != 0) {
            savedUsers = workGroupUserNames;
            if (isDynamicScriptLoadedForManageUser == "true") {
                LoadAddedUsersJtable('afterSave');
                $('#addedManageUserResults').show();
                $('#managecompany').show();
                $('#mainbutton').show();
                $('#spnMgUserAddedResultLabel').html(listOfAddedMgUsers);
                $('#spnMgUserAddedResultLabel').show();
            }
            else {
                objDialog.load('/GCS/Workgroup/OpenManageUsers');
                isDynamicScriptLoadedForManageUser = "true";
                $('#spnMgUserAddedResultLabel').html(listOfAddedMgUsers);
                $('#spnMgUserAddedResultLabel').show();
            }
        }

        if (savedUsers == "") {
            $('#spnMgUserAddedResultLabel').hide();
        }

        moveDialogBoxAtTop();
    });

    /*Manage Users partial view changes */

    $('#btnreset').click(function () {
        window.location.href = "/GCS/WorkGroup/";
    });
    $('#btnManageTerritories').addClass('btndisabled');
    $("#btnManageTerritories").attr("disabled", true);
    $('#RolesList').change(function () {
        selRoleTypeText = $("#RolesList option:selected").text();
        if ((selRoleTypeText.match('Inquiry')) || ((selRoleTypeText.match('Local Label Reviewer')))) {
            $("#btnManageTerritories").attr("disabled", false);
            $('#btnManageTerritories').removeClass('btndisabled');
        } else {
            //Clear the Territory Related objects
            window.hashTerritory = {};
            window.territoryCollection = [];
            document.getElementById('territoryDetailsForSave').value = '';
            $("#btnManageTerritories").attr("disabled", "disabled");
            $("#selectedCountries").html('');
            $('#hdnSavedCountryValues').val("");
            $('#hdnAddCountryValues').val("");
            $('#addedCountryResults').jtable('destroy');
            $('#btnManageTerritories').addClass('btndisabled');
            document.getElementById('btnSaveCountry').style.visibility = 'hidden';
            document.getElementById('btnRemoveCountry').style.visibility = 'hidden';
            document.getElementById('btnCancelCountry').style.visibility = 'hidden';
            $('#btnManageTerritories').removeClass('input-validation-error');
        }
        if ((selRoleTypeText.match('UMGI Global Clearance')) || (selRoleTypeText.match('UMGI Marketing Reviewer')) || (selRoleTypeText.match('RCC Admin'))) {
            $("#btnManageCompany").attr("disabled", "disabled");
            $('#companyList').jtable('destroy'); $('#hdnSaveCompanyList').val(""); $('#hdnSavedCompanyValues').val(""); $('#hdnAddedValues').val("");
        }
        else {
            $("#btnManageCompany").removeAttr("disabled");
            $('#btnManageCompany').removeClass('btndisabled');
        }

        if ($("#RolesList > option:selected").attr("value") == "8" || $("#RolesList > option:selected").attr("value") == "9" || $("#RolesList > option:selected").attr("value") == "") {
            DefaultUsers = "";
            DefaultUserNames = "";
            document.getElementById('divDefaultUsers').innerHTML = DefaultUserNames;
            $('#hdnDefaultsUserForSave').val('');
        }
    });
    $('#txtWorkgroupName').change(function (event) {
        if (($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "")) {
            $('#txtWorkgroupName').addClass('input-validation-error');
        } else {
            $('#txtWorkgroupName').removeClass('input-validation-error');
        }
    });

    if ($('#hdnMaintainWorkGroup').length != 0) {
        if (($('#RoleName').val() == 'Inquiry') || ($('#RoleName').val() == 'Local Label Reviewer')) {
            $("#btnManageTerritories").removeAttr("disabled");
            $('#btnManageTerritories').removeClass('btndisabled');
        }
        else {
            $("#btnManageTerritories").attr("disabled", "disabled");
        }
        //During the loading page here hide the company button
        if (($('#RoleName').val() == 'UMGI Global Clearance') || ($('#RoleName').val() == 'UMGI Marketing Reviewer') || ($('#RoleName').val() == 'RCC Admin')) {
            $("#btnManageCompany").attr("disabled", "disabled");
        }
        else {
            $("#btnManageCompany").removeAttr("disabled");
            $('#btnManageCompany').removeClass('btndisabled');
        }

        companyValues = $('#companyIdSession').val();
        if (companyValues != '')
            LoadManageCompanytable(companyValues);
    }

    $('#btncreate').click(function () {
        //
        var usersData = '';
        usersData = $.trim($('#divUsers')[0].innerHTML);
        var companyData = '';
        companyData = $.trim($('#companyList')[0].innerHTML);
        var countryData = '';
        countryData = $.trim($('#selectedCountries')[0].innerHTML);
        // RoleType
        selRoleTypeText = $("#RolesList option:selected").text();
        $("#errorMessage").hide();
        $("#errorMessage").html('');
        $('#divMessage').hide(); $("#divMessage").html('');

        if ($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "") {
            $('#txtWorkgroupName').addClass('input-validation-error');
        }

        if (($('#btnManageCompany').is(":disabled") != true) && (companyData == "" || companyData == "&nbsp;" || companyData == null)) {
            $('#btnManageCompany').addClass('input-validation-error')
        }

        if (usersData == "" || usersData == "&nbsp;" || usersData == null) {
            $('#btnManageUsers').addClass('input-validation-error');
        }

        if ($('#hdnMaintainWorkGroup').length != 0 && selectedWorkgroupRole.match('Inquiry')) {
            if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) {
                $('#btnManageCompany').addClass('input-validation-error');
                $('#btnManageTerritories').addClass('input-validation-error'); $("#errorMessage").show(); $("#errorMessage").html(mandatoryFields);
            } else {
                $('#btnManageCompany').removeClass('input-validation-error'); $('#btnManageTerritories').removeClass('input-validation-error');
            }
        }
        // All
        if (($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "") && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
            $('#RolesList').addClass('input-validation-error');
            if (selRoleTypeText.match('Inquiry')) {
                $('#RolesList').removeClass('input-validation-error');
                // Manage Company && Country && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    //displayDialog(pageTitle, mandatoryFields);
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Country
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Users
                if (((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }
                if (usersData == "" || usersData == "&nbsp;" || usersData == null) {
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    return false;
                }
            }

            if (selRoleTypeText.match('Local Label Reviewer')) {
                $('#RolesList').removeClass('input-validation-error');
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Country
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Country && Users
                if ((countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageTerritories').focus();
                    return false;
                }
                else {
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Country
                if (countryData == "" || countryData == "&nbsp;" || countryData == null) {
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageTerritories').focus();
                    return false;
                }
                else {
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }
                // manage user
                if (usersData == "" || usersData == "&nbsp;" || usersData == null) {
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageUsers').focus();
                    return false;
                }
                else {
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }
            }

            if (selRoleTypeText.match('Reviewer') || selRoleTypeText.match('Requestor') || selRoleTypeText.match('UMGI Global Clearance')) {
                $('#RolesList').removeClass('input-validation-error');
                // Manage Company && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) || (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    if (companyData == "" || companyData == "&nbsp;" || companyData == null) {
                        //CR Implentation
                        if (selRoleTypeText.match('UMGI Global Clearance') || selRoleTypeText.match('UMGI Marketing Reviewer')) {
                            $('#btnManageCompany').removeClass('input-validation-error');
                        }
                        else {
                            $('#btnManageCompany').addClass('input-validation-error');
                        }
                    }
                    if (usersData == "" || usersData == "&nbsp;" || usersData == null) {
                        $('#btnManageUsers').addClass('input-validation-error');
                    }
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company
                if (companyData == "" || companyData == "&nbsp;" || companyData == null) {
                    //CR Implentation
                    if (selRoleTypeText.match('UMGI Global Clearance') || selRoleTypeText.match('UMGI Marketing Reviewer')) {
                        $('#btnManageCompany').removeClass('input-validation-error');
                    }
                    else {
                        $('#btnManageCompany').addClass('input-validation-error');
                        $("#errorMessage").show();
                        $("#errorMessage").html(mandatoryFields);
                        $('#btnManageCompany').focus();
                        return false;
                    }
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }
            }
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#txtWorkgroupName').focus();
            return false;
        }
        else {
            $("#errorMessage").hide();
            $("#errorMessage").html('');
            if (!($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "")) {
                $('#txtWorkgroupName').removeClass('input-validation-error');
            }
            $('#RolesList').removeClass('input-validation-error');
            if (!(usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                $('#btnManageUsers').removeClass('input-validation-error');
            }
        }

        if ($('#btncreate').attr('value') != 'Save') {
            //Roles
            if ($("#RolesList > option:selected").attr("value") == null || $("#RolesList > option:selected").attr("value") == "" || $("#RolesList > option:selected").attr("value") == "--Select Role--" || $("#RolesList > option:selected").attr("value") == "-1") {
                $('#RolesList').addClass('input-validation-error');
                $("#errorMessage").show();
                $("#errorMessage").html(mandatoryFields);
                $('#RolesList').focus();
                return false;
            }
            else {
                $('#RolesList').removeClass('input-validation-error');
                $("#errorMessage").hide();
                $("#errorMessage").html('');
            }

            if (selRoleTypeText.match('Inquiry')) {
                // Manage Company && Country && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Country
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Users
                if (((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }
            }

            if (selRoleTypeText.match('Local Label Reviewer')) {
                // Manage Company && Country && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Country
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Manage Company && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Country && Users
                if ((countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageTerritories').focus();
                    return false;
                }
                else {
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }

                // Country
                if (countryData == "" || countryData == "&nbsp;" || countryData == null) {
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageTerritories').focus();
                    return false;
                }
                else {
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $("#errorMessage").hide();
                    $("#errorMessage").html('');
                }
            }

            if (selRoleTypeText.match('Reviewer') || selRoleTypeText.match('Requestor') || selRoleTypeText.match('UMGI Global Clearance')) {
                // Manage Company && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    //CR Implentation
                    if (selRoleTypeText.match('UMGI Global Clearance') || selRoleTypeText.match('UMGI Marketing Reviewer')) {
                        $('#btnManageCompany').removeClass('input-validation-error');
                    }
                    else {
                        $('#btnManageCompany').addClass('input-validation-error');
                    }
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                }

                // Manage Company
                if (companyData == "" || companyData == "&nbsp;" || companyData == null) {
                    //CR Implentation
                    if (selRoleTypeText.match('UMGI Global Clearance') || selRoleTypeText.match('UMGI Marketing Reviewer')) {
                        $('#btnManageCompany').removeClass('input-validation-error');
                    }
                    else {
                        $('#btnManageCompany').addClass('input-validation-error');
                        $("#errorMessage").show();
                        $("#errorMessage").html(mandatoryFields);
                        $('#btnManageCompany').focus();
                        return false;
                    }
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                }
            }
        }
        else {
            var roleNameEdit = $('#RoleName').val();
            if (roleNameEdit.match('Inquiry')) {
                // Manage Company && Country && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                }
                // Manage Company && Country
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                }

                // Manage Company && Users
                if (((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                }
            }

            if (roleNameEdit.match('Local Label Reviewer')) {
                // Manage Company && Country && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                }

                // Manage Company && Country
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (countryData == "" || countryData == "&nbsp;" || countryData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageTerritories').removeClass('input-validation-error');
                }

                // Manage Company && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageCompany').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                }

                // Country && Users
                if ((countryData == "" || countryData == "&nbsp;" || countryData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageTerritories').focus();
                    return false;
                }
                else {
                    $('#btnManageTerritories').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                }
                // Country
                if (countryData == "" || countryData == "&nbsp;" || countryData == null) {
                    $('#btnManageTerritories').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageTerritories').focus();
                    return false;
                }
                else {
                    $('#btnManageTerritories').removeClass('input-validation-error');
                }
            }

            if (roleNameEdit.match('Reviewer') || roleNameEdit.match('Requestor') || roleNameEdit.match('UMGI Global Clearance')) {
                // Manage Company && Users
                if ((companyData == "" || companyData == "&nbsp;" || companyData == null) && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
                    //CR Implementation
                    if (roleNameEdit.match('UMGI Global Clearance') || roleNameEdit.match('UMGI Marketing Reviewer')) {
                        $('#btnManageCompany').removeClass('input-validation-error');
                    } else {
                        $('#btnManageCompany').addClass('input-validation-error');
                    }
                    $('#btnManageUsers').addClass('input-validation-error');
                    $("#errorMessage").show();
                    $("#errorMessage").html(mandatoryFields);
                    $('#btnManageCompany').focus();
                    return false;
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                    $('#btnManageUsers').removeClass('input-validation-error');
                }

                // Manage Company
                if (companyData == "" || companyData == "&nbsp;" || companyData == null) {
                    //CR Implementation
                    if (roleNameEdit.match('UMGI Global Clearance') || roleNameEdit.match('UMGI Marketing Reviewer')) {
                        $('#btnManageCompany').removeClass('input-validation-error');
                    }
                    else {
                        $('#btnManageCompany').addClass('input-validation-error');
                        $("#errorMessage").show();
                        $("#errorMessage").html(mandatoryFields);
                        $('#btnManageCompany').focus();
                        return false;
                    }
                }
                else {
                    $('#btnManageCompany').removeClass('input-validation-error');
                }
            }
        }

        // All
        if (($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "") && ($("#RolesList > option:selected").attr("value") == null || $("#RolesList > option:selected").attr("value") == "" || $("#RolesList > option:selected").attr("value") == "--Select Role--" || $("#RolesList > option:selected").attr("value") == "-1") && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
            $('#txtWorkgroupName').addClass('input-validation-error');
            $('#RolesList').addClass('input-validation-error');
            $('#btnManageUsers').addClass('input-validation-error');
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#txtWorkgroupName').focus();
            return false;
        }
        else {
            $("#errorMessage").hide();
            $("#errorMessage").html('');
            $('#txtWorkgroupName').removeClass('input-validation-error');
            $('#RolesList').removeClass('input-validation-error');
            $('#btnManageUsers').removeClass('input-validation-error');
        }
        //WorkgroupName, Roles
        if (($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "") && ($("#RolesList > option:selected").attr("value") == null || $("#RolesList > option:selected").attr("value") == "" || $("#RolesList > option:selected").attr("value") == "--Select Role--" || $("#RolesList > option:selected").attr("value") == "-1")) {
            $('#txtWorkgroupName').addClass('input-validation-error');
            $('#RolesList').addClass('input-validation-error');
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#txtWorkgroupName').focus();
            return false;
        }
        else {
            $('#txtWorkgroupName').removeClass('input-validation-error');
            $('#RolesList').removeClass('input-validation-error');
            $("#errorMessage").hide();
            $("#errorMessage").html('');
        }
        //WorkgroupName, Users
        if (($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "") && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
            $('#txtWorkgroupName').addClass('input-validation-error');
            $('#btnManageUsers').addClass('input-validation-error');
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#txtWorkgroupName').focus();
            return false;
        }
        else {
            $('#txtWorkgroupName').removeClass('input-validation-error');
            $('#btnManageUsers').removeClass('input-validation-error');
            $("#errorMessage").hide();
            $("#errorMessage").html('');
        }

        //Roles, Users
        if (($("#RolesList > option:selected").attr("value") == null || $("#RolesList > option:selected").attr("value") == "" || $("#RolesList > option:selected").attr("value") == "--Select Role--" || $("#RolesList > option:selected").attr("value") == "-1") && (usersData == "" || usersData == "&nbsp;" || usersData == null)) {
            $('#RolesList').addClass('input-validation-error');
            $('#btnManageUsers').addClass('input-validation-error');
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#RolesList').focus();
            return false;
        }
        else {
            $('#RolesList').removeClass('input-validation-error');
            $('#btnManageUsers').removeClass('input-validation-error');
            $("#errorMessage").hide();
            $("#errorMessage").html('');
        }

        // Name
        if ($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "") {
            $('#txtWorkgroupName').addClass('input-validation-error');
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#txtWorkgroupName').focus();
            return false;
        }
        else {
            $('#txtWorkgroupName').removeClass('input-validation-error');
            $("#errorMessage").hide();
            $("#errorMessage").html('');
        }

        // Users
        if (usersData == "" || usersData == "&nbsp;" || usersData == null) {
            $('#btnManageUsers').addClass('input-validation-error');
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#btnManageUsers').focus();
            return false;
        }
        else {
            $('#btnManageUsers').removeClass('input-validation-error');
        }
        //R2Contact Email Address Valid
        if ($('#txtR2Contact').attr("value").length != 0) {
            if (ValidateEmail($('#txtR2Contact').attr("value"))) {
                $('#txtR2Contact').addClass('input-validation-error');
                $("#errorMessage").show();
                $("#errorMessage").html(validEmailId);
                $('#txtR2Contact').focus();
                return false;
            } else {
                $('#txtR2Contact').removeClass('input-validation-error');
            }
        }
        var loadingPanel = $($.find('#loadingDv'));
        //serialize method
        if ($('#btncreate').attr('value') != 'Save') {
            var digitalVal = $("#CreateParentWorkgroupForm").serialize();
            loadingPanel.show();
            $.post('/GCS/Workgroup/CreateParentWorkgroupSave', digitalVal, function (data) {
                if (data.length > 0) {
                    loadingPanel.hide();
                    $("#errorMessage").show();
                    $("#errorMessage").html(data);
                }
                else {
                    //redirect
                    loadingPanel.hide();
                    window.location.href = "/GCS/Workgroup/";
                }
            });
        } else {
            var digitalVal = $("#UpdateParentWorkgroupForm").serialize();
            loadingPanel.show();
            $.post('/GCS/Workgroup/UpdateParentWorkgroup', digitalVal, function (data) {
                if (data != null) {
                    if (data.length == 2) {
                        loadingPanel.hide();
                        document.getElementById('hdnModifiedTime').value = data[0];
                        document.getElementById('hdnJsonCompanyList').value = data[1];
                        $("#divMessage").show();
                        $("#divMessage").html(pageSuccessMessage);
                        $("#main").animate({ scrollTop: '0' }, 800);
                    }
                    else {
                        loadingPanel.hide();
                        $("#errorMessage").show();
                        $("#errorMessage").html(data);
                    }
                }
                else {
                    loadingPanel.hide();
                    window.location.href = "/GCS/Workgroup/Error.HTML";
                }
            });
        }
    });

    $('#btnManageTerritories').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        if (territoryCollection.length != 0) {
            if ($('#hdnMaintainWorkGroup').length == 0) {
                var includedCountries = JSLINQ(territoryCollection).Where(function (dict) { return dict.Key == 0 });
                if (includedCountries.items.length != 0) {
                    var territoryValues = includedCountries.items[0].Value;
                    var workKey = includedCountries.items[0].Key;
                    LoadTerritories(territoryValues, workKey);
                }
            }
            else {
                var includedCountries = JSLINQ(territoryCollection).Where(function (dict) { return dict.Key == workGroupKey });
                if (includedCountries.items.length != 0) {
                    var territoryValues = includedCountries.items[0].Value;
                    var workKey = includedCountries.items[0].Key;
                    LoadTerritories(territoryValues, workKey);
                }
            }
        }
        else {
            if (fromPage == "MaintainParentWorkgroup") {
                LoadTerritories(territoryCollection, workGroupKey);
            }
            else {
                LoadTerritories(territoryCollection, "0");
            }
        }
    });

    $('#btnCreateChildWorkgroup').click(function () {
        var parentWgpId = $('#hdnWorkgroupId').val();
        window.location.href = "/GCS/WorkGroup/CreateChildWorkgroup?parentWorkgroupId=" + parentWgpId;
    });
});