var searchlist1 = '';
var jsonObj = "";
var pagingCount = 5;
var startIndex = "0";
var selectedCompId = "";
var selectedCompName = "";
var selectedCompIsacCode = "";
var selectedCountryName = "";
var selectedCompIds = "";
var savedCompanyIds = "";
var selectedUsers = "";
var savedUsers = "";
var DefaultUsers = "";
var selectedUsersToDelete = '';
var ManageCompany = "";
var selRoleTypeText = "";
var companyValues = "";
var imgDefault = "";
var savedUserNames = "";
var DefaultUserNames = "";
var recordsCount = 0;
var companyName = '';
var freeHandCompName = '';
var thirdPartyCheckFlag = false;
var stopReleaseProjectTitleSync = false;
var isSelect = false;

function RemoveFreeHandCompany() {
    $("#hdnCompName").val('');
    $("#tblThirdParty").hide();
    $("#rowAddedCompanyResult").hide();
}

function RemoveThirdPartyCompany() {
    //To remove the confirm dialog when we remove company
    $("#hdnCompId").val("");

    $("#hdnCompName").val('');
    $("#txtThirdPartyCompId").val(-1);
    $("#txtThirdPartyCompName").val("");
    //Added for UC-055 start here
    $("#txtThirdPartyCompCountry").val("");
    $("#txtThirdPartyCompISAC").val("");
    $("#hdnThirdPartyComapnyISAC").val("");
    $("#hdnThirdPartyComapnyCountry").val("");
    //Added for UC-055 ends here

    $("#tblThirdParty").hide();
    $("#rowAddedCompanyResult").hide();
    Loadwatermark();
}

function LoadSearchJtable(pageSize) {
    $('#searchCompanyResults').jtable({
        paging: true,
        pageSize: pageSize,
        defaultSorting: 'Company ASC',
        selecting: true,
        multiselect: false,
        selectOnRowClick: true,
        actions: {
            listAction: '/GCS/ClearanceProject/SearchCompany'
        },
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
                width: '5%'
            },
            radiobutton: {
                title: 'Select',
                width: '10%',
                sorting: false,
                edit: false,
                create: false,
                display: function () {
                    //creating radiobutton column in list
                    var $radiobutton = $('<input type="radio" value="0" name="rbtnSelect" />');
                    return $radiobutton;
                }
            },
            Name: {
                title: 'Company',
                width: '40%'
            },
            ISACCode: {
                title: 'ISAC Company Code',
                width: '20%'
            },

            CountryName: {
                title: 'Country',
                width: '35%'
            }
        },
        recordsLoaded: function (event, data) {

            if (data.records.length > 0) {
                recordsCount = data.records.length;
                $('#searchCompanyResults').show();
                $("#SearchCompanyPaging").show();
                $("#searchResultForCompany").html('<p style=\"margin:0px !important\">' + "Search Results For &nbsp;<b>\"" + searchlist1 + '\"</b>&nbsp;<span id="cmpSearchResultRecordcount"/></p>');
                $('#cmpSearchResultRecordcount').html('(' + recordsCount + ')');

                $("#searchCompanyResults input").removeAttr("checked");
                $("#searchCompanyResults tr").removeClass("jtable-row-selected");
                $("#btnAdd").removeAttr("disabled");
                $("#btnAddFreeHandComp").hide();
                $("#btnAdd").show();
                $("#freeHandCompany").hide();

            }
            else {
                $("#SearchCompanyPaging").hide();
                $("#manageCmpnyNoRecordsMsg").show();
                $("#btnAddFreeHandComp").show();
                $("#btnAdd").attr("disabled", "disabled");
                $("#freeHandCompany").show();
                $("#rowCompanyName").hide();
                $('#searchCompanyResults').hide();
                $("#btnAddFreeHand").removeAttr("disabled");
                $("#btnAddFreeHandComp").show();
                $("#btnAddFreeHandComp").attr("disabled", "disabled");
                $("#btnAdd").hide();

            }
        },

        //Register to selectionChanged event to hanlde events

        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('#searchCompanyResults').jtable('selectedRows');
            jsonObj = '';
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    jsonObj = jsonObj + record.Id + ",";
                    selectedCompId = record.Id;
                    selectedCompName = record.Name;
                    selectedCompIsacCode = record.ISACCode;
                    selectedCountryName = record.CountryName;
                });
                $selectedRows.first(function () {
                    var record = $(this).data('record');
                    selectedCompId = record.Id;
                });
            }

        }
    });

    $('#searchCompanyResults').jtable('load', {
        name: $('#name').val(),
        isacCode: $('#isaccode').val(),
        country: $('#country').val()
    });
}

function Loadwatermark() {
    $.watermark.options.className = 'watermark';
    jQuery("#txtThirdPartyRepertoire").watermark(watermarkThirdPartyReptoire);
    jQuery("#txtOneStopReason").watermark(watermarkOneStopReason);

    jQuery("#name1").watermark(watermarkCompanyName);
    jQuery("#isaccode1").watermark(watermarkIsacCode);
    jQuery("#country1").watermark(watermarkCountry);
}

function LoadParameter() {
    jQuery("#name").val() = jQuery("#name1").val();
}

function Reset() {
    $('#name').val('');
    $('#isaccode').val('');
    $('#country').val('');
}

function ThirdPartyCheckEnable(flag) {
    if (flag) {
        $("#chk3rdParty").attr('checked', true);
        $("#name1").removeAttr("disabled");
        $("#isaccode1").removeAttr("disabled");
        $("#country1").removeAttr("disabled");
        $("#btnSearchCompany").removeAttr("disabled");
        $("#txtLicenseTerm").removeAttr("disabled");
    }
    else {
        $("#chk3rdParty").attr('checked', false);
        $("#name1").attr("disabled", "disabled");
        $("#isaccode1").attr("disabled", "disabled");
        $("#country1").attr("disabled", "disabled");
        $("#btnSearchCompany").attr("disabled", "disabled");
        $("#txtLicenseTerm").attr("disabled", "disabled");
    }
}

function CompanySaved(selectedCompanies) {
    LoadManageCompanytable(selectedCompanies);

    $('#SearchedCompanyList').removeClass('input-validation-error');
    if (selRoleTypeText.match('Inquiry')) {
        $('#selectedCountries').removeClass('input-validation-error');
    }

}

function ValidateMandatoryFields(FieldName) {
    if ($('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == "" && $('#' + FieldName).is(':visible')) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

function ValidateMandatoryFieldsReleaseNew(FieldName) {
    if (FieldName.indexOf('PackageIncludedUPC') >= 0) {
        if ($('#' + FieldName).text().length > 0) {
            return true;
        }
        else {
            return false
        }
    }
    if (($('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == "0" || $('#' + FieldName).val() == "" || $('#' + FieldName).val() == "--Select--")) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

function ValidateMandatoryFieldsRequestTypePhySalesTodate(FieldName) {
    if (($('#' + FieldName).attr("value") == null || $('#' + FieldName).val() == "" || $('#' + FieldName).attr("value") == "" || $('#' + FieldName).val() == "Comments")) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

function TextAreaValidation() {
    $('textarea').keydown(function (event) {
        if (event.keyCode == 13) {
            event.stopPropagation();
        }
        else {
            if (this.value.length >= 1073741824) {
                return false;
            }
        }
    });
}

function unlockProject(projectid) {
    var values =
                {
                    "projectId": projectid
                }
    $.ajax({
        url: '/GCS/ClearanceProject/UnlockProject',
        type: 'POST',
        async: false,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values)
    });
}

function BeforeUploadFileOnClient() {
    isSelect = true;

    var fileUpload = $("#tblFileUpload");
    if (fileUpload != null && fileUpload.length == 1 && fileUpload[0].rows.length >= 10) {
        return false;
    }
}

function UploadFileOnClient(obj) {
    hideHeaderMessages();

    if (isSelect == true) {

        isSelect = false;

        var filepath = obj.value;
        var fileName = filepath.substr((filepath.lastIndexOf('\\') + 1));
        var id = obj.id;

        var fileUpload = $("#tblFileUpload");
        if (fileUpload != null && fileUpload.length == 1 && fileUpload[0].rows.length >= 10) {

            $('#hdnRemoveFile').val() == "" ? $('#hdnRemoveFile').val(fileName) : $('#hdnRemoveFile').val($('#hdnRemoveFile').val() + "," + fileName);
            $("#divErrorMessage").html(uploadMessage).show();
            $('#divErrorMessage').addClass('warning');
        }
        else {
            createDynamicTable(fileUpload, fileName, id);
        }

        $("#" + id).hide();

        var fileUploadLength = $("#tblFileUpload")[0].rows.length;
        if (fileUploadLength <= 10) {

            var newId = GCS.utilities.functions.getUniqueId()
            var div = document.createElement('DIV');
            div.innerHTML = '<input id="fileUpload_' + newId + '" name = "fileUpload_11" type="file" class="width-file"  onclick="BeforeUploadFileOnClient();" onchange="UploadFileOnClient(this);" />';
            $("#fileInput11").show();
            $("#fileInput11").append(div);
            $("#btnUpload").show();
        }

        $("#tblFileUpload").show();
    }
}

function createDynamicTable(tbody, displayValue, id) {
    if (tbody != null || tbody.length > 0) {
        var trow = $("<tr>");

        $("<td>").append(displayValue).data("col", 1).appendTo(trow);
        $("<td>").append("<a href='#' class = 'RemoveFile'  onClick='RemoveFileOnLocal(\"" + displayValue + "~" + id + "\")' id='RemoveFile" + id + "'>" + "Remove</a>").data("col", 2).appendTo(trow);

        trow.appendTo(tbody);
    }
}

function RemoveFileOnLocal(rowValue) {
    hideHeaderMessages();

    $("#divErrorMessage").html('').hide();

    var substr = rowValue.split('~');
    var id = substr[1];
    var name = substr[0];

    $('#hdnRemoveFile').val() == "" ? $('#hdnRemoveFile').val(name) : $('#hdnRemoveFile').val($('#hdnRemoveFile').val() + "," + name);

    $("#RemoveFile" + id).closest("tr").remove();
    $("#" + id).closest("div").remove();
}

function UpdatecreateDynamicTable(fullPath) {
    for (var fileCount = 0; fileCount < 10; fileCount++) {
        var filePath = document.getElementById("fileUpload_" + fileCount).value.substr((document.getElementById("fileUpload_" + fileCount).value.lastIndexOf('\\') + 1));

        if (filePath == fullPath) {
            $("fileUpload_" + fileCount).replaceWith($("fileUpload_" + fileCount).clone(true));
        }
        if (document.getElementById("fileUpload_" + fileCount).value == "") {
            document.getElementById("fileUpload_" + fileCount).style.display = 'inline';
            document.getElementById("fileInput" + fileCount).style.display = 'inline';
            if ($("#tblFileUpload")[0] != null && $("#tblFileUpload")[0].rows.length == 0) {
                document.getElementById("tblFileUpload").style.display = 'none';
                document.getElementById("btnUpload").style.display = 'none';
            }
            return false;
        }
    }
}

function RemoveFile(rowValue) {
    hideHeaderMessages();

    var substr = rowValue.split('~');
    $("#hdnRemoveFromServer" + substr[2]).closest("tr").remove();


    var values = {
        "fileName": rowValue,
        "clrProjectId": $('#Clr_Project_Id').val()
    };

    $.ajax({
        type: 'POST',
        url: '/Gcs/ClearanceProject/RemoveRegularFile',
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values),
    });
}

function updateReleaseNewTitle(id) {
    if (($(id).val() != '') && ($('#FlagReleaseNewOrExisting').val() == "New") && ($('#lblprojectreferencenumber').text() == '') && (stopReleaseProjectTitleSync == false)) {
        $('#txtTitle_0').val($(id).val());
    }
}

function stopProjectTitleSync(id) {
    if (($('#lblprojectreferencenumber').val() == '') && (id == 'txtTitle_0')) {
        stopReleaseProjectTitleSync = true;
    }
}

function uploadFile() {
    hideHeaderMessages();

    globalPostBackCheck = true;
    var form = window.parent.document.forms[0];

    if (checkTotalSize(form.id)) {
        form.submit();
        $('#loadingDivPA').show();
    }
    else {
        $("#divErrorMsg").html(uploadDoumentMsg).hide();
        $("#divErrorMsg").show();
    }
}

function checkTotalSize(formId, limitTotalSize) {
    if (limitTotalSize === undefined) {
        var iisLimitSize = 4000000;
        limitTotalSize = iisLimitSize;
    }

    var navigatorProperties = GCS.utilities.functions.getNavigatorProperties();

    var isPossibleSubmit = true;
    var totalSizeFiles = 0;

    $('#' + formId + ' input[type="file"]').each(function () {
        var inputId = this.id;
        totalSizeFiles = totalSizeFiles + GCS.utilities.functions.getFileSize(inputId);
    });

    if (totalSizeFiles > limitTotalSize) {
        isPossibleSubmit = false;
    }

    return isPossibleSubmit;
}

function hideHeaderMessages() {
    $("#divErrorMessage").html('').hide();
    $("#divErrorMessage1").html('').hide();
    $("#divErrorMsg").html('').hide();
}

function closeAllTheAutocompleteProjectInformationTab() {
    $("#name1").autocomplete("close");
    $("#country1").autocomplete("close");
}

$(document).ready(function (e) {

    $('body').unbind('keydown');

    $('body').keydown(function (e) {
        if (e.keyCode == 13) {
            if ($('#loadingDivPA').is(':visible')) {
                return;
            }
            if (currencyDdwnFocus == '0') {
                var index = $("#tabs").tabs('option', 'selected');
                var tabid = index + 1;
                var Id = "tabs-" + tabid;
                if ($("#SearchR2ProjectsDialog")[0].innerHTML.toString().trim() != "") {
                    if ($("#ArtistResourcePopup").length != 0) {
                        if ($("#ArtistResourcePopup")[0].innerHTML.toString() != "") {
                            $("#searchForArtist").trigger('click');
                        }
                        else {
                            $("#projToRep").trigger('click');
                        }
                    }
                    else {
                        $("#projToRep").trigger('click');
                    }
                }
                else if ((index == "3")) {
                    if (Id == "tabs-4") {
                        var checkResourceSearch = 0;

                        if ($("#SearchDialog") != undefined) {
                            if ($("#SearchDialog")[0] != undefined) {
                                if ($("#SearchDialog")[0].innerHTML.toString().trim() != "") {
                                    checkResourceSearch = 1;
                                }
                            }
                        }

                        if (checkResourceSearch == 1) {
                            if ($("#ArtistResourcePopup").length != 0) {
                                if ($("#ArtistResourcePopup")[0].innerHTML.toString() != "") {
                                    $("#searchForArtist").trigger('click');
                                }
                                else {
                                    $("#btnsearch").trigger('click');
                                }
                            }
                            else if (document.getElementById("freeHand") != null) {
                                var _freehandId = document.getElementById("freeHand");
                                if (_freehandId.style.display == 'none' || _freehandId.style.display == '') {
                                    $("#btnsearch").trigger('click');
                                }
                                else {
                                    $("#btnaddToProject").trigger('click');
                                }
                            }
                            else {
                                if ($("#Territory").length != 0) {
                                    if ($('#Territory')[0].innerHTML.toString() != "") {
                                        $("#btnSearchTerritory").trigger('click');
                                    }
                                    else {
                                        $("#btnsearch").trigger('click');
                                    }
                                }
                                else {
                                    $("#btnsearch").trigger('click');
                                }
                            }
                        }
                        else {
                            if ($("#ArtistResourcePopup").length != 0) {
                                if ($("#ArtistResourcePopup")[0].innerHTML.toString() != "") {
                                    $("#searchForArtist").trigger('click');
                                }
                                else {
                                    if ($("#Territory").length != 0) {
                                        if ($('#Territory')[0].innerHTML.toString() != "") {
                                            $("#btnSearchTerritory").trigger('click');
                                        }
                                        else {
                                            $("#btnSearch").trigger('click');
                                        }
                                    }
                                    else {
                                        $("#btnSearch").trigger('click');
                                    }
                                }
                            }

                            else {
                                if ($("#Territory").length != 0) {
                                    if ($('#Territory')[0].innerHTML.toString() != "") {
                                        $("#btnSearchTerritory").trigger('click');
                                    }
                                    else {
                                        $("#btnSearch").trigger('click');
                                    }
                                }
                                else {
                                    $("#btnSearch").trigger('click');
                                }
                            }
                        }
                        return false;
                    }
                    else {
                        $("#btnSave").trigger('click');
                    }
                    return false;
                }
                else {
                    if (Id == "tabs-3") {
                        if ($("#FlagReleaseNewOrExisting").val() == "Exist") {
                            if ($("#SearchReleasePopup").length != 0) {
                                if ($('#SearchReleasePopup')[0].innerHTML.toString() != "") {
                                    $("#SearchReleaseExisting").trigger('click');
                                }
                                else {
                                    $("#SearchRelease").trigger('click');
                                }
                            }
                            else {
                                $("#SearchRelease").trigger('click');
                            }
                        }
                        else {
                            if ($("#ArtistResourcePopup").length != 0) {
                                if ($("#ArtistResourcePopup")[0].innerHTML.toString() != "") {
                                    $("#searchForArtist").trigger('click');
                                }
                                else {
                                    $("#btnSave").trigger('click');
                                }
                            }
                            else {
                                if ($("#SearchReleasePopup").length != 0) {
                                    if ($('#SearchReleasePopup')[0].innerHTML.toString() != "") {
                                        $("#SearchReleaseExisting").trigger('click');
                                    }
                                    else {
                                        $("#btnSave").trigger('click');
                                    }
                                }
                                else {
                                    $("#btnSave").trigger('click');
                                }
                            }
                        }
                    }
                    else {
                        if (Id == "tabs-1") {
                            if ($('#SearchedCompanyList')[0].innerHTML.toString() != "") {
                                var _activeElement = document.activeElement;
                                if (_activeElement.id == 'btnAdd') {
                                    $("#btnAdd").trigger('click');
                                }
                                else if (_activeElement.id == 'btnSearchComp') {
                                    $("#btnSearchComp").trigger('click');
                                }
                            }
                            else {
                                if ($("#Territory").length != 0) {
                                    if ($('#Territory')[0].innerHTML.toString() != "") {
                                        $("#btnSearchTerritory").trigger('click');
                                    }
                                    else {
                                        $("#btnSave").trigger('click');
                                    }
                                }
                                else {
                                    $("#btnSave").trigger('click');
                                }
                            }
                        }
                        else {
                            if (Id == "tabs-2") {
                                if ($("#Territory").length != 0) {
                                    if ($('#Territory')[0].innerHTML.toString() != "") {
                                        $("#btnSearchTerritory").trigger('click');
                                    }
                                    else {
                                        $("#btnSave").trigger('click');
                                    }
                                }
                                else {
                                    $("#btnSave").trigger('click');
                                }
                            }
                            else {
                                $("#btnSave").trigger('click');
                            }
                        }
                    }
                    return false;
                }
            }
        }
    });

    $("#chkMultiArtist").on("click", function () {
        if ($("#chkMultiArtist").is(':checked')) {
            $(".chkCompilation").prop('checked', true);
            $(".chkCompilation").attr("disabled", "disabled");
        }
        else {
            if ($(".chkCompilation").is(':disabled')) {
                $(".chkCompilation").prop('checked', false);
                $(".chkCompilation").removeAttr("disabled");
            }
        }
    });

    $("#chkOneStop").on("click", function () {
        if ($("#chkOneStop").is(':checked')) {
            $("#txtOneStopReason").show();
            $("#lblChkStopMandatory").show();
        }
        else {
            $("#txtOneStopReason").val('');
            $("#txtOneStopReason").hide();
            $("#lblChkStopMandatory").hide();
        }
    });

    $("#chk3rdParty").on("click", function () {
        if ($("#chk3rdParty").is(':checked')) {
            $("#name1").removeAttr("disabled");
            $("#isaccode1").removeAttr("disabled");
            $("#country1").removeAttr("disabled");
            $("#btnSearchCompany").removeAttr("disabled");
            $("#txtLicenseTerm").removeAttr("disabled");

            $("#3rdPartyCompanyMandatory").show();
            $("#licenseTermMandatory").show();
            Loadwatermark();
        }
        else {
            closeAllTheAutocompleteProjectInformationTab();
            $("#name1").attr("disabled", "disabled");
            $("#isaccode1").attr("disabled", "disabled");
            $("#country1").attr("disabled", "disabled");
            $("#btnSearchCompany").attr("disabled", "disabled");
            $("#txtLicenseTerm").attr("disabled", "disabled");
            $("#3rdPartyCompanyMandatory").hide();
            $("#licenseTermMandatory").hide();
            RemoveThirdPartyCompany();
        }
    });

    $("#imgHideProjectInfo").on("click", function () {
        $("#tblProjectInfo").slideToggle("fast");
        var imgSrc = $("#imgHideProjectInfo").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgHideProjectInfo").attr("src", "/Gcs/Images/left.png");
            $("#imgHideProjectInfo").attr("title", "Expand");
        }
        else {
            $("#imgHideProjectInfo").attr("src", "/Gcs/Images/Down.png");
            $("#imgHideProjectInfo").attr("title", "Collapse");
        }
        return false;
    });

    $("#imgHideThirdParty").on("click", function () {
        $("#tblThirdParty").slideToggle("fast");
        var imgSrc = $("#imgHideThirdParty").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgHideThirdParty").attr("src", "/Gcs/Images/left.png");
            $("#imgHideThirdParty").attr("title", "Expand");
        }
        else {
            $("#imgHideThirdParty").attr("src", "/Gcs/Images/Down.png");
            $("#imgHideThirdParty").attr("title", "Collapse");
        }
        return false;
    });

    $("#btnUpload").on("click", uploadFile);

    function isTheProjectInformationTabContext() {
        var tabId = "projectInformationTab";
        var tabIdCurrent = GCS.utilities.functions.getSelectedTab("#screenTabs");
        var isTheCurrentTab = (tabId === tabIdCurrent);
        return isTheCurrentTab;
    }

    $.watermark.options.className = 'watermark';

    autocomplete("#name1");

    autocomplete("#country1");

    function autocomplete(id) {

        $(id).autocomplete({
            appendTo: "#tabs-1",

            source: function (request, response) {
                var url = $(id).attr("data-autocomplete-source-manual");

                $(id).addClass("ui-autocomplete-loading")

                $(id).removeClass("ui-autocomplete-input")

                var terms = {
                    term: request.term,
                    searchBy: $(id).attr("SearchFor")
                };

                $.getJSON(url, terms, function (data) {

                    var popupId = "#SearchedCompanyList";

                    var isCheckedchk3rdParty = $('#chk3rdParty').prop('checked');

                    if (!(GCS.utilities.functions.isTheDialogPopupOpened(popupId)) && (isTheProjectInformationTabContext()) && isCheckedchk3rdParty) {
                        response(data);
                    };

                    $(id).removeClass("ui-autocomplete-loading");

                    $(id).addClass("ui-autocomplete-input");

                });
            },

            minLength: 3,

            select: function (event, ui) {
                $(id).val(ui.item.value);
            }
        });

        if ($('#hdnRoleGroup').val() != null) {
            RefreshEmailPanel($('#hdnProjectStatusId').val(), $('#hdnRoleGroup').val());
        }
    }

    function RefreshEmailPanel(projectStatus, RoleName) {
        //hide all the item of email's menu and button
        $("#ulMailList li").hide();
        $("#btnEmail").hide();

        if (RoleName == "Requestor" && projectStatus != "1") {
            $("#btnEmail").show();
            $("#lnkEmailGeneral").show();
            $("#lnkTvRadio").show();
            $("#lnkRegularNon").show();
            $("#lnkUPCAllocation").show();
            $("#lnkRegularNonWithReviewStatus").show();
            $("#lnkEmailRegularArtistManagement").show();
        }
        else if (projectStatus != "1") {
            $("#btnEmail").show();
            $("#lnkEmailGeneral").show();
            $("#lnkTvRadio").show();
            $("#lnkRegularNon").show();
            $("#lnkRegularNonWithReviewStatus").show();
            $("#lnkEmailRegularArtistManagement").show();
        }
    }

    $('#btnSearchCompany').click(function () {
        
        $("#name1").autocomplete("close");
        $("#country1").autocomplete("close");

        if (($('#name1').val() == null || $('#name1').val() == "") && ($('#isaccode1').val() == null || $('#isaccode1').val() == "") && ($('#country1').val() == null || $('#country1').val() == "")) {
            $("#divErrorMessage").text(mandatorySearchCriteria);
            $('#divErrorMessage').addClass('warning');
            $("#divErrorMessage").show();

            $('#name1').focus();
            return false;
        }
        searchlist1 = "";
        var company1 = $('#name1').val();
        var isacCode1 = $('#isaccode1').val();
        var country1 = $('#country1').val();
        if (company1 != '') { searchlist1 = searchlist1 + company1 + " / "; }
        if (isacCode1 != '') { searchlist1 = searchlist1 + isacCode1 + " / "; }
        if (country1 != '') { searchlist1 = searchlist1 + country1; }
        if (country1 == '') {
            searchlist1 = searchlist1.substring(0, searchlist1.length - 1);
        }
        $("#divErrorMessage").text("");
        $("#divErrorMessage").hide();
        $('#divErrorMessage').removeClass('warning');
        selectedCompId = "";

        var vdata = new Object();
        vdata.name = $('#name1').val();
        vdata.isacCode = $('#isaccode1').val();
        vdata.country = $('#country1').val();
        vdata.jtStartIndex = startIndex;
        vdata.jtPageSize = pagingCount;
        vdata = JSON.stringify(vdata);
        $.ajax({
            type: "POST",
            url: '/GCS/ClearanceProject/SearchCompany',
            async: false,
            data: vdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                
                if (result.TotalRecordCount == 1) {
                    
                    $("#tblThirdParty").show();
                    $("#rowAddedCompanyResult").show();
                    $("#hdnCompId").val(result.Records[0].Id);
                    $("#txtThirdPartyCompId").val(result.Records[0].Id);
                    if (result.Records[0].Name == null || result.Records[0].Name == "") {
                        $("#tdCompName").html("");
                    }
                    else {
                        $("#tdCompName").html(result.Records[0].Name);
                        $("#txtThirdPartyCompName").val(result.Records[0].Name);
                    }
                    if (result.Records[0].ISACCode == null || result.Records[0].ISACCode == "") {
                        tdIsacCode
                        $("#tdIsacCode").html("");
                    }
                    else {
                        $("#tdIsacCode").html(result.Records[0].ISACCode);
                        $("#txtThirdPartyCompISAC").val(result.Records[0].ISACCode); //Added for UC-055
                    }
                    if (result.Records[0].CountryName == null || result.Records[0].CountryName == "") {
                        $("#tdCountry").html("");
                        $("#tdCountry1").html("");
                    }
                    else {
                        $("#tdCountry").html(result.Records[0].CountryName);
                        $("#tdCountry1").html(result.Records[0].CountryName);
                        $("#country1").val(result.Records[0].CountryName);
                        $("#txtThirdPartyCompCountry").val(result.Records[0].CountryName); //Added for UC-055
                        $("#hdnThirdPartyComapnyCountry").val(result.Records[0].CountryName);
                    }
                }
                else {
                    
                    $("#addedManageCompanyResults").show();
                    var objDialog = $('#SearchedCompanyList')
                                        .dialog({
                                            autoOpen: true,
                                            modal: true,
                                            title: '3rd Party Company Search',
                                            show: 'clip',
                                            hide: 'clip',
                                            height: 'auto',
                                            resizable: false,
                                            close: function () {
                                                $('#SearchedCompanyList')[0].innerHTML = "";
                                            },
                                            width: 1000
                                        });
                    objDialog.load('/GCS/ClearanceProject/ManageCompany', function () {
                        $('#name').val($('#name1').val());
                        $('#isaccode').val($('#isaccode1').val());
                        $('#country').val($('#country1').val());
                        Loadwatermark();
                        pagingCount = $('#ddlCompanyPaging').val();
                        LoadSearchJtable(pagingCount);
                    }
                            );

                    var dialogue = $('.ui-dialog')

                    dialogue.animate({
                        top: "40px"
                    }, 0);

                }
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });

    if ($("#chk3rdParty").is(':checked')) {
        $("#name1").removeAttr("disabled");
        $("#isaccode1").removeAttr("disabled");
        $("#country1").removeAttr("disabled");
        $("#btnSearchCompany").removeAttr("disabled");
        $("#txtLicenseTerm").removeAttr("disabled");
        $("#Div3rdPartyDetails").show();
        if (($('#txtThirdPartyCompId').val() == null || $('#txtThirdPartyCompId').val() == "" || $('#txtThirdPartyCompId').val() == -1) && ($('#txtThirdPartyCompName').val() == null || $('#txtThirdPartyCompName').val() == "")) {
            RemoveThirdPartyCompany();
        }
        else {
            $("#tblThirdParty").show();
            $("#rowAddedCompanyResult").show();
        }
        Loadwatermark();
    }
    else {        
        $("#name1").attr("disabled", "disabled");
        $("#isaccode1").attr("disabled", "disabled");
        $("#country1").attr("disabled", "disabled");
        $("#btnSearchCompany").attr("disabled", "disabled");
        $("#txtLicenseTerm").attr("disabled", "disabled");
        RemoveThirdPartyCompany();
        $("#Div3rdPartyDetails").hide();
        $("#3rdPartyCompanyMandatory").hide();
        $("#licenseTermMandatory").hide();
        $("#tblThirdParty").hide();
        $("#rowAddedCompanyResult").hide();
    }

    if ($("#chkOneStop").is(':checked')) {
        $("#txtOneStopReason").show();
        $("#lblChkStopMandatory").show();
    }
    else {
        $("#txtOneStopReason").hide();
        $("#lblChkStopMandatory").hide();
    }

    if ($("#chkMultiArtist").is(':checked')) {
        $(".chkCompilation").prop('checked', true);
        $(".chkCompilation").attr("disabled", "disabled");
    }

    $('#btnRemove').click(function () {
        LoadAddedSearchJtable('delete');
        selectedCompIds = "";
    });

    var pickerOpts = {
        dateFormat: "dd/mm/yy",
        showOn: "button",
        buttonImage: "/Gcs/Images/GCS_Calender_Icon_img.png",
        buttonImageOnly: true
    };

    $("#txtReleaseDate").datepicker(pickerOpts);

    if ($.browser.msie) {
        if ($.browser.msie && parseInt($.browser.version, 10) > 7) {
            $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "marginLeft": "2px", "marginTop": "-1px" });
        } else {
            $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "marginLeft": "2px", "marginTop": "-6px" });
        }
    } else {
        $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "marginLeft": "2px", "marginTop": "0" });
    }

    $('#ddlCompanyPaging').change(function () {
        var rowCount = $(this).val();
        LoadSearchJtable(rowCount);
    });

    $('#screenTabs li a').click(function (event) {
        if (isTheProjectInformationTabContext()) {
            closeAllTheAutocompleteProjectInformationTab();
        }
    });
});

