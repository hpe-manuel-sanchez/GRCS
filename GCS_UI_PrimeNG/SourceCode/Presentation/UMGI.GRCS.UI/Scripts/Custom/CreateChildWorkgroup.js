
var isDynamicScriptLoadedForManageUser = "false";
var isDynamicScriptLoadedForManageCompany = "false";
var isDynamicScriptLoaded = "false";
var workGroupId = "";
var isManageUserSearchResetClick = "false";
var maintainWorkgroupRoleId = "";
var DefaultUsers = "";
var ManageCompany = "";
var fromPage = "";
var workGroupUserNames = '';
var savedUsers = '';

var companyValues = "";
var removedAllRows = "";
var maintainChildCompanyIds = "";
var isManageCompanyPartialViewLoaded = "";

$('.ui-dialog-titlebar-close').attr("title", "Close");
function LoadViewAfterDynamicScriptLoadedForManageCompany() {
    if (isDynamicScriptLoadedForManageCompany == "true") {
        Reset();
        ResetForSearchCompany();
        $('#addedManageCompanyResults').jtable('destroy');
        if (workGroupId.length > 0) {
            pagingCount = 10;
        }
        if ($('#hdnSavedCompanyValues').val() != "") {
            LoadAddedSearchJtable('aftersave');
        }
        else {
            if (workGroupId.length > 0) {
            if (fromPage.toLowerCase() == 'createchildworkgroup') {
                    LoadSearchJtable(pagingCount, workGroupId);
                    $("#searchCompanyPopup").show();
                    $("#managecompany").hide();
                }
                else {
                    $("#searchCompanyPopup").hide();
                }
            }
        }
    }

}
$(document).ready(function () {


    var companyIds = $('#companyIdSession').val();
    maintainChildCompanyIds = $('#companyIdSession').val();
    if (companyIds != '') {

        LoadManageCompanytable(companyIds);
    }
});

$(document).ready(function () {
    $('#btnManageArtist').click(function () {
        $("#mgArtisterrorMessage").html('');
        $("#mgArtisterrorMessage").hide();
        $('#btnManageArtist').jtable('destroy');
        var objDialog = $('#divManageArtist')
                 .dialog({

                     autoOpen: true,
                     modal: true,
                     title: searchforArtistContract,
                     resizable: false,
                     width: "95%"

                 });

        /*********** CODE ADDED BY MOHIT FOR POPUP ***************/
        var dialogue = $('.ui-dialog');

        dialogue.animate({
            top: "40px"
        }, 0);


        $("#divArtist .jtable-main-container").css({ "width": "auto", "padding": "0 15px", "max-height": "356px" });
        $("#divArtist").css("position", "relative");
        $("#divAddedArtistContract").css("position", "relative");
        /***************************************************************************************/

        objDialog.load('/GCS/Workgroup/SearchforManageArtist/', { id: pageName },
         function (responseText, textStatus) {
             if (document.getElementById('hdnManageArtistIds').value != '') {
                 LoadAddedArtistContract('afterSave');
             }
         }
        );

        //
    });
});

$(document).ready(function () {
    $('#btnManageResource').click(function () {
        $("#mgResourcErrorMessage").html('');
        $("#mgResourcErrorMessage").hide();
        var objDialog = $('#divManageResource')
        .dialog({
            autoOpen: true,
            modal: true,
            title: searchforResourceContract,
            show: 'clip',
            hide: 'clip',
            resizable: false,
            width: "95%"
        });
        objDialog.load('/GCS/Workgroup/SearchforManageResourceArtist/', { id: pageName },
        function (responseText, textStatus) {
            if (document.getElementById('hdnManageResourceIds').value != '') {
                LoadAddedResourceContract('afterSave');
            }
        });
        /*********** CODE ADDED BY MOHIT FOR POPUP ***************/
        var dialogue = $('.ui-dialog');

        dialogue.animate({
            top: "30px"
        }, 0);
    });
});

$(document).ready(function (e) {

    $('#btnManageCompany').click(function () {
        jGridsList["searchCompanyResults"] = [];
        $('#addedManageCompanyResults').jtable('destroy');
        $('#searchCompanyResults').jtable('destroy');
        $('#spnMgCompanyAddedResultLabel').html('');
        $('#spnMgCompanyAddedResultLabel').hide();
        if ($('#hdnMaintainWorkGroup').length != 0 || fromPage.toLowerCase() == 'maintainchildworkgroup') {
            if (companyValues != "" || $('#companyIdSession').val() != "") {
                $('#addedManageCompanyResults').jtable('destroy');
                ShowManageCompanyPopup();
                jsonObj = companyValues;
                if ($('#hdnSavedCompanyValues').val() != "") {
                    companyValues = $('#hdnSavedCompanyValues').val();
                    LoadAddedSearchJtable('aftersave');
                }
                else if (fromPage == 'maintainchildworkgroup') {
                    if ($('#companyIdSession').val() == "") {
                    }
                    else {

                        companyValues = $('#companyIdSession').val();
                        document.getElementById('companyIdSession').value = "";
                    }
                }
                if (removedAllRows == "true" && companyValues == "") {
                    companyValues = "";
                    LoadAddedSearchJtable('aftersave');
                }
                else if (removedAllRows == "true" && companyValues != "") {
                    LoadAddedSearchJtable('aftersave');
                }
                else if (removedAllRows == "false") {
                    LoadAddedSearchJtable('aftersave');
                }
                $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
                $('#spnMgCompanyAddedResultLabel').show();
            }
            else {
                ShowManageCompanyPopup();
                $('#addedManageCompanyResults').jtable('destroy');
                if (isManageCompanyPartialViewLoaded == "true") {
                    if (workGroupId.length > 0 && fromPage.toLowerCase() == 'maintainchildworkgroup') {
                        if ($('#hdnSavedCompanyValues').val() != "") {
                            LoadAddedSearchJtable('aftersave');
                        }
                        else {
                            if (maintainChildCompanyIds == "") {
                                LoadSearchJtable(pagingCount, workGroupId);
                                $("#searchCompanyPopup").show();
                                $("#managecompany").hide();
                            }
                            else {
                                $("#searchCompanyPopup").hide();
                                $("#managecompany").show();
                                $("#addedManageUserResults").show();
                            }
                        }
                    }
                }
            }
        }
        else if ($('#hdnSavedCompanyValues').val() != "" && $('#hdnSavedCompanyValues') != null) {
            ShowManageCompanyPopup();
            $('#addedManageCompanyResults').jtable('destroy');
            LoadAddedSearchJtable('aftersave');
            $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
            $('#spnMgCompanyAddedResultLabel').show();
        }
        else {
            if ($('#hdnMaintainWorkGroup').length == 0 && fromPage.toLowerCase() == "createchildworkgroup") {
                if ($('#hdnSavedCompanyValues').val() == "") {
                    //Manage Company PopLoading for CreateChild
                    var objDialog = $('#manageCompanyContainer')
        .dialog({
            autoOpen: true,
            modal: true,
            title: 'Manage Company',
            show: 'clip',
            hide: 'clip',
            width: 1000,
            resizable: false,
            close: function (ev, ui) { $(this).hide(); },
            beforeClose: function (event, ui) {
                if ($('#btnRemove').is(':visible')) {
                    $('#spnMgCompanyAddedResultLabel').html('');
                    $('#spnMgCompanyAddedResultLabel').hide();
                    return true;
                }
                else {
                    $("#managecompany").show();
                    $("#searchCompanyPopup").hide();
                    if ($('#addedManageCompanyResults').data().jtable == null) {

                        $('#spnMgCompanyAddedResultLabel').html('');
                        $('#spnMgCompanyAddedResultLabel').hide();
                    } else {
                        if ($('#addedManageCompanyResults').is(':visible')) {

                            $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
                            $('#spnMgCompanyAddedResultLabel').show();
                        }
                    }
                }
                Reset();
                hideErrorDiv();
                Loadwatermark();
                $("#searchResultForCompany").html('')
                $("#searchCompanyResults").html('')

            }
        });
                    /*********** CODE ADDED BY MOHIT FOR POPUP ***************/
                    var dialogue = $('.ui-dialog');

                    dialogue.animate({
                        top: "30px"
                    }, 0);

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
        $('#btnOpenSearchComp').focus();
    });
});

function ShowManageCompanyPopup() {
    //Manage Comapny PopLoading for MaintainChild
    var objDialog = $('#manageCompanyContainer')

        .dialog({
            autoOpen: true,
            modal: true,
            title: 'Manage Company',
            show: 'clip',
            hide: 'clip',
            width: 1000,
            resizable: false,
            close: function (ev, ui) { $(this).hide(); },
            beforeClose: function (event, ui) {
                if ($('#btnRemove').is(':visible')) {
                    return true;
                }
                else {
                    $("#managecompany").show();
                    $("#searchCompanyPopup").hide();

                    if ($('#addedManageCompanyResults').innerHTML != "") {
                        $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
                        $('#spnMgCompanyAddedResultLabel').show();
                    }
                }
                Reset();
                hideErrorDiv();
                Loadwatermark();
                $("#searchResultForCompany").html('')
                $("#searchCompanyResults").html('')
            }

        });
    if (isDynamicScriptLoadedForManageCompany == "false") {
        objDialog.load('/GCS/Workgroup/ManageCompanyPopup?parentPage=' + fromPage + "&parentWorkgroupId=" + workGroupId);
        isDynamicScriptLoadedForManageCompany = "true";
    }
}


$(document).ready(function () {
    $("#divUsers").show();
    $('#btnManageUsers').click(function () {
     //   jGridsList["searchUserResults"] = [];
        $('#addedManageUserResults').jtable('destroy');
        var objDialog = $('#manageUsers')
                 .dialog({
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
                             if ($('#addedManageUserResults').data().jtable == null) {
                                 $("#mainbutton").hide();
                                 $("#divLineSeparator").hide();
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
                                         $("#divLineSeparator").hide();
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
        /*********** CODE ADDED BY MOHIT FOR POPUP ***************/
        var dialogue = $('.ui-dialog');

        dialogue.animate({
            top: "30px"
        }, 0);

        if ($('#hdnMaintainWorkGroup').length != 0 || fromPage == 'maintainchildworkgroup') {
            savedUsers = workGroupUserNames;
            if (isDynamicScriptLoadedForManageUser == "true") {
                $('#addedManageUserResults').jtable('destroy');
                LoadAddedUsersJtable('afterSave');
                $('#addedManageUserResults').show();
                $('#managecompany').show();
                $("#searchUserPopup").hide();
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
        //Load partial view 
        if ($('#hdnSavedUserNameValues').val() != "") {
            $('#manageUsers').jtable('destroy');
            $("#addedManageUserResults").show();
            $("#mainbutton").show();
            LoadAddedUsersJtable('afterSave');
            ResetManageUsers();
        }
        else if ($('#hdnSavedUserNameValues').val() == "") {
            $('#manageUsers').jtable('destroy');

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

    });
});

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

$(document).ready(function () {
    if (($('#RoleName').val() == 'UMGI Global Clearance') || ($('#RoleName').val() == 'UMGI Marketing Reviewer')) {
        $("#btnManageCompany").attr("disabled", "disabled");
    }
    else {
        $("#btnManageCompany").removeAttr("disabled");
        $('#btnManageCompany').removeClass('btndisabled');
    }
    $('#btnManageTerritories').addClass('btndisabled');
    $('#btnManageTerritories').attr("disabled", true);
    $('#btnCreateChildWorkgroup').click(function () {
        window.location.href = "/GCS/WorkGroup/CreateChildWorkgroup";
    });
    $('#txtWorkgroupName').change(function (event) {
        if (($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "")) {
            $('#txtWorkgroupName').addClass('input-validation-error');
        } else {

            $('#txtWorkgroupName').removeClass('input-validation-error');
        }
    });
});

$(document).ready(function () {
    $('#btncreate').click(function () {
        var usersData = '';
        usersData = $.trim($('#divUsers')[0].innerHTML); $("#errorMessage").hide(); $("#errorMessage").html(''); $("#divMessage").hide(); $("#divMessage").html('');
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
        }
        if ($('#txtWorkgroupName').attr("value") == null || $('#txtWorkgroupName').attr("value") == "") {
            $('#txtWorkgroupName').addClass('input-validation-error');
            $("#errorMessage").show();
            $("#errorMessage").html(mandatoryFields);
            $('#txtWorkgroupName').focus();
            return false;
        }
        else {
            $('#txtWorkgroupName').removeClass('input-validation-error');
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
        var digitalVal = $("#CreateChildWorkgroupForm").serialize();
        var loadingPanel = $($.find('#loadingDv'));
        loadingPanel.show();
        $.post('/GCS/Workgroup/CreateChildWorkgroupSave', digitalVal, function (data) {
            if (data.length > 0) {
                if (data.length == 2) {
                    document.getElementById('hdnModifiedTime').value = data[0];
                    document.getElementById('hdnJsonCompanyList').value = data[1];
                    loadingPanel.hide();
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
                if (document.getElementById('hiddenPageName').value == "CreateChildWorkgroup") {
                    //redirect
                    loadingPanel.hide();
                    window.location.href = "/GCS/Workgroup/";
                } else {
                    loadingPanel.hide();
                    $("#divMessage").show();
                    $("#divMessage").html(pageSuccessMessage);
                    $("#main").animate({ scrollTop: '0' }, 800);
                }
            }

        });
    });
});

//    ******************** Manage Resource*************************


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

function LoadArtistContract() {
    $('#divManageArtistContract').jtable({
        paging: false,
        sorting: false,
        selecting: false,
        multiselect: false,
        columnResizable: false,
        selectingCheckboxes: false,
        selectOnRowClick: false,
        width: 1000,
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () {
            $('.jtable .jtable-no-data-row').show();
        },
        actions: {
            listAction: '/GCS/workgroup/GetArtistbyContract'
        },
        fields: {

            ArtistId: {
                title: artistContract.ArtistID,
                width: '15%'
            },
            ArtistName: {
                title: artistContract.ArtistName,
                width: '20%'
            },
            ContractId: {
                title: artistContract.ContractId,
                width: '10%',
                display: function (data) {
                    var ContractID = data.record.ContractId;
                    return $('<a href="#" style="text-decoration:underline;">' + ContractID + '</a>').click(function () {
                        var formValues = "id=" + ContractID + "&pageName=Childworkgroup";
                        $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                            if (data != null) {
                                var objDialog = $('#divContract')
                                                    .dialog({
                                                        autoOpen: true,
                                                        modal: true,
                                                        title: 'Contract Information',
                                                        show: 'clip',
                                                        hide: 'clip',
                                                        width: 500,
                                                        resizable: false//,
                                                        //height: 200
                                                    });
                                objDialog.html(data);
                            }
                        });
                    });
                }
            },
            ClearanceAdminCompanyName: {
                title: artistContract.ClrAdminCompany,
                width: '25%'
            },
            ISAC: {
                title: artistContract.ClrAdminCompanyID,
                width: '25%'
            }
        }
    });

    $('#divManageArtistContract').jtable('load', {
        contractids: contractIdListForArtist,
        events: "save"
    });
};

function LoadResourceContract() {
    $('#divManageResourceContract').jtable({
        paging: false,
        sorting: false,
        selecting: false,
        multiselect: false,
        columnResizable: false,
        selectingCheckboxes: false,
        selectOnRowClick: false,
        width: 1200,
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () {
            $('.jtable .jtable-no-data-row').show();
        },
        actions: {
            listAction: '/GCS/workgroup/GetResourceContractByContractIdList'
        },
        fields: {

            ArtistId: {
                title: artistContract.ArtistID,
                width: '10%'
            },
            ArtistName: {
                title: artistContract.ArtistName,
                width: '13%'
            },
            Isrc:
                {
                    title: artistContract.Isrc,
                    width: '8%'
                },
            ResourceTitle:
                {
                    title: artistContract.ResourceTitle,
                    width: '18%'
                },
                VersionTitle:
            {
                title: mgResourceContractJtableColNames.VersionTitle,//"Version Title"
                width: '12%'
            },
            ContractId: {
                title: artistContract.ContractId,
                width: '10%',
                display: function (data) {
                    var ContractID = data.record.ContractId;
                    return $('<a href="#" style="text-decoration:underline;">' + ContractID + '</a>').click(function () {
                        var formValues = "id=" + ContractID + "&pageName=Childworkgroup";
                        $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                            if (data != null) {
                                var objDialog = $('#divContract')
                                                    .dialog({
                                                        autoOpen: true,
                                                        modal: true,
                                                        title: 'Contract Information',
                                                        show: 'clip',
                                                        hide: 'clip',
                                                        width: 500,
                                                        resizable: false//,
                                                        //height: 200
                                                    });
                                objDialog.html(data);
                            }
                        });
                    });
                }
            },

            ClearanceAdminCompanyName: {
                title: artistContract.ClrAdminCompany,
                width: '18%'
            },
            ISAC: {
                title: artistContract.ClrAdminCompanyID,
                width: '20%'
            }
        }
    });
    $('#divManageResourceContract').jtable('load', {
        //contractids: contractIdListForResource,
        deviationResourceContract: JSON.stringify(maintainCRCollection),
        events: "save"
    });

};



function getChildWorkgroupAuditHistory(AuditTypeId) {
    var SelectedItemIds = [];
    var displayTitle;
    SelectedItemIds.push($('#hdnWorkgroupId').val());
    if ($('#txtWorkgroupName').attr("value").length > 0) {
        displayTitle = $('#txtWorkgroupName').attr("value");
    }
    else {
        displayTitle = '';
    }
    displayWGAuditTrail(AuditTypeId, SelectedItemIds, displayTitle, selectedWorkgroupRole, false);
    return false;
}
