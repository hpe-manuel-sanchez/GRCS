var jsonObj = "";
var pagingCount = "";
var selectedCompIds = "";
var savedCompanyIds = "";
var companyIdsWhenSave = "";
var company = "";
var isacCode = "";
var country = "";
var isFirstTimeChildWorkgroup = "true";
jGridsList["searchCompanyResults"] = [];
$('.ui-dialog-titlebar-close').attr("title", "Close");

$(document).ready(function (e) {
    Reset();
    hideErrorDiv();
    Loadwatermark();

    $('#btnRemove').hide();
    $('#btnSave').hide();
    $('#btnCancel').hide();

    if (workGroupId.length > 0) {
        pagingCount = 10;
    }

    //Added for Routing Variation Companies
    if (fromPage == "routingVariations" && routingVariationCompanies != null && routingVariationCompanies != "") {
        $('#hdnSavedCompanyValues').val(routingVariationCompanies);
    }
    if ($('#hdnSavedCompanyValues').val() != "" || ($('#hdnMaintainWorkGroup').length != 0)) {
        LoadAddedSearchJtable('aftersave');
    }
    else {
        if (workGroupId.length > 0 && (fromPage.toLowerCase() == 'createchildworkgroup' || fromPage.toLowerCase() == 'maintainchildworkgroup')) {
            LoadSearchJtable(pagingCount, workGroupId);
        }
    }

    if (fromPage == 'maintainchildworkgroup') {
        if (maintainChildCompanyIds != "") {
            LoadAddedSearchJtable('aftersave');
            $("#searchCompanyPopup").hide();
            $("#managecompany").show();
            $("#addedManageUserResults").show();
        }
    }

    if (fromPage.toLowerCase() == 'createchildworkgroup' || fromPage.toLowerCase() == 'maintainchildworkgroup') {
        if ((fromPage.toLowerCase() == 'maintainchildworkgroup' && maintainChildCompanyIds == "") ||
            (fromPage.toLowerCase() == 'createchildworkgroup')) {
            $("#searchCompanyPopup").show();
            $("#managecompany").hide();
        }
    }
    else {
        $("#searchCompanyPopup").hide();
        $("#managecompany").show();
        $("#addedManageUserResults").show();
    }
    isManageCompanyPartialViewLoaded = "true";

    if ($('#btnRemove').is(':visible')) {
        if ($('#addedManageCompanyResults')[0].innerHTML != "") {
            $('#spnMgCompanyAddedResultLabel').css({ "display": "block" });
            $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
            $('#spnMgCompanyAddedResultLabel').show();
        }
    }
    else {
        $('#spnMgCompanyAddedResultLabel').html('');
        $('#spnMgCompanyAddedResultLabel').hide();
    }

    $('#btnOpenSearchComp').click(function () {
        if (($('#CompanyDetails_Name').val() == null || $('#CompanyDetails_Name').val() == "") && ($('#isaccode').val() == null || $('#isaccode').val() == "") && ($('#country').val() == null || $('#country').val() == "")) {
            showErrorDiv(createWorkgroupMessages.searchInValid);
            $('#manageCompanyContainer').css("width", "100%");
            return false;
        }

        pagingCount = $('#ddlCompanyPaging').val();

        var searchlist = '';
        company = $('#CompanyDetails_Name').val();
        isacCode = $('#isaccode').val();
        country = $('#country').val();

        if (company != '') { searchlist = searchlist + company + '/'; }
        if (isacCode != '') { searchlist = searchlist + isacCode + '/'; }
        if (country != '') { searchlist = searchlist + country; }
        if (country == '') {
            searchlist = searchlist.substring(0, searchlist.length - 1);
        }

        $('#spnMgCompanyAddedResultLabel').html('');
        $('#spnMgCompanyAddedResultLabel').hide();
        $("#searchResultForCompany").html('<p style=\"margin-left:13px\">' + "Search Result For <b>" + searchlist + '&nbsp;<span id="cmpSearchResultRecordcount"/></b></p>');
        $('#managecompany').hide();
        $('#searchCompanyPopup').show();
        $('#divSearchCompanyPaging').show();
        LoadwatermarkForSearchCompany();
        Loadwatermark();
        $('#searchCompanyResults').jtable('destroy'); jGridsList["searchCompanyResults"] = [];
        LoadSearchJtable(pagingCount, workGroupId);
        $("#btnCancel1").show();
        document.getElementById('btnAdd').style.visibility = 'visible';
    });

    $('#btnReset').click(function () {
        if ($('#btnRemove').is(':visible')) {
            if ($('#addedManageCompanyResults')[0].innerHTML != "") {
                $('#spnMgCompanyAddedResultLabel').css({ "display": "block" });
                $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
                $('#spnMgCompanyAddedResultLabel').show();
            }
            else {
                $('#spnMgCompanyAddedResultLabel').html('');
                $('#spnMgCompanyAddedResultLabel').hide();
            }
            Reset();
            hideErrorDiv();
            Loadwatermark();
        }
        else {
            hideErrorDiv(); ResetForSearchCompany();
            //Bug Fix for 4714
            //            $('#searchCompanyResults').jtable('destroy');
            //            document.getElementById('btnAdd').style.visibility = 'hidden';
            //            $("#divSearchCompanyPaging").hide();
            //            $("#btnCancel1").hide();
            //            $("#searchResultForCompany").html('');
            //Bug Fix for 4714
            LoadwatermarkForSearchCompany();
        }
    });

    $('#btnRemove').click(function () {
        LoadAddedSearchJtable('delete');
        selectedCompIds = "";
    });

    $('#btnSave').click(function () {
        if (companyIdsWhenSave != '') {
            LoadAddedSearchJtable('save');
        }
        else {
            LoadAddedSearchJtable('save');
            document.getElementById('hdnSavedCompanyValues').value = '';
            if (fromPage != "routingVariations") companyValues = '';
            $('#companyList').jtable('destroy');
        }
        document.getElementById('CompanyDetails_Name').value = '';
        document.getElementById('isaccode').value = '';
        document.getElementById('country').value = '';
        $('#manageCompanyContainer').dialog('close');
    });

    $('#btnAdd').click(function () {
        if (jsonObj.length > 0) {
            ShowManageCompanyPartialViewPopup();
            LoadAddedSearchJtable('Add');
            $("#managecompany").show();
            if ($('#addedManageCompanyResults')[0].innerHtml != "") {
                $('#spnMgCompanyAddedResultLabel').css({ "display": "block" });
                $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
                $('#spnMgCompanyAddedResultLabel').show();
                $('#addedManageCompanyResults').show();
            }
            $("#searchCompanyPopup").hide();
        }
        else {
            showErrorDiv(noRowsSelected);
        }
    });

    ////Closes manage company popup with save,remove and cancel button

    $('#btnCancel').click(function () {
        if ($('#hdnAddedValues').val().indexOf(jsonObj) != -1) {
            document.getElementById('hdnAddedValues').value = $('#hdnAddedValues').val().replace(jsonObj, '');
        }
        $('#manageCompanyContainer').dialog('close');
        document.getElementById('CompanyDetails_Name').value = '';
        document.getElementById('isaccode').value = '';
        document.getElementById('country').value = '';
    });

    ////Closes search Company popup with Add and Cancel buttons
    $('#btnCancel1').click(function () {
        $('#searchCompanyPopup').hide();
        $('#managecompany').show();
        if ($('#addedManageCompanyResults')[0].innerHTML != "") {
            $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
            $('#spnMgCompanyAddedResultLabel').css({ "display": "block" });
            $('#spnMgCompanyAddedResultLabel').show();
            $('#addedManageCompanyResults').show();
            $('#btnSave').show();
            $('#btnCancel').show();
        }
        else {
            $('#btnSave').hide();
            $('#btnCancel').hide();
        }
    });

    //Company paging change event
    $('#CompanyDetails_Name')
        .watermark(watermarkCompanyName)
        .keypress(function (event) {
            if (event.keyCode == '13') {
                $('#btnOpenSearchComp').click();
                return false;
            }
        });

    $('#isaccode').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnOpenSearchComp').click();
            return false;
        }
    });
    $('#country').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnOpenSearchComp').click();
            return false;
        }
    });

    $("#CompanyDetails_Name")
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB &&
                $(this).data("autocomplete").menu.active) {
                event.preventDefault();
            }
        })
        .blur(function (event) {
            if (!$('#CompanyDetails_Name').is(":focus"))
                $('#CompanyDetails_Name').removeClass("ui-autocomplete-loading");
        })
        .autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                    type: "GET",
                    dataType: "json",
                    data: { suggestiveInput: request.term, workgroupElement: "Company", pageName: pageName, workgroupId: parentWorkgroupId },
                    term: request.term,
                    success: function (data) {
                        if (data.length == 1) {
                            $('#CompanyDetails_Name').val(data[0]);
                        }
                        response(
                            $.map(data, function (records, autocomplete) {
                                if ($('#CompanyDetails_Name').is(":focus"))
                                    return records;
                                else {
                                    autocomplete.disabled = true;
                                    return;
                                }
                            }));
                    }
                });
            },
            minLength: 3,
            focus: function () {
                return false;
            },
            select: function (event, ui) {
                if (ui.item == null) {
                    return;
                }
                else {
                    $('#CompanyDetails_Name').val("");
                    $('#CompanyDetails_Name').val(ui.item.value);
                }
                return false;
            }
        });

    $('#ddlCompanyPaging').change(function () {
        var rowCount = $(this).val();
        if (($('#CompanyDetails_Name').val() == null || $('#CompanyDetails_Name').val() == "") && ($('#isaccode').val() == null || $('#isaccode').val() == "") && ($('#country').val() == null || $('#country').val() == "")) {
            showErrorDiv(createWorkgroupMessages.searchInValid);
            $('#manageCompanyContainer').css("width", "100%");
            return false;
        }
        LoadSearchJtable(rowCount, workGroupId);
    });
});

//Loads search results jtable with Add and Cancel button

function LoadSearchJtable(pageSize, workGroupId) {
    hideErrorDiv();
    $('#searchCompanyResults').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'Name ASC',
        selecting: true,
        multiselect: true,
        columnResizable: false,
        selectOnRowClick: false,
        selectingCheckboxes: true,
        actions: {
            listAction: GetCompaniesTypeForSearch(workGroupId)
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
                width: '5%'
            },
            Name: {
                title: mgCompJtableColNames.Company,
                width: '40%'
            },

            ISACCode: {
                title: mgCompJtableColNames.ISACCompCode,
                width: '20%'
            },

            CountryName: {
                title: mgCompJtableColNames.CountryName,
                width: '35%'
            }
        },
        loadingRecords: function () {
            isGridLoad = true;
        },
        recordsLoaded: function (event, data) {
            displaySelectedRecordsInGrid($("#searchCompanyResults"), jGridsList["searchCompanyResults"], "Id");
            isGridLoad = false;
            $('#cmpSearchResultRecordcount').html('(' + data.serverResponse.TotalRecordCount + ')');
            $('#searchCompanyResults .jtable thead tr th:first').remove();
            $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#searchCompanyResults .jtable thead tr:first');

            if (data.records.length > 0) {
                $('.jtable .jtable-no-data-row').show();
                //$("#searchCompanyResults input").removeAttr("checked");
                //$("#searchCompanyResults tr").removeClass("jtable-row-selected");

                if (document.getElementById('btnAdd').style.visibility == 'hidden') {
                    document.getElementById('btnAdd').style.visibility = 'visible';
                }
                if (document.getElementById('btnCancel1').style.display == 'none') {
                    $("#btnCancel1").show();
                }

                $("#btnAdd").removeAttr("disabled");
            }
            else {
                $("#btnAdd").attr("disabled", "disabled");
            }
        },

        //Register to selectionChanged event to hanlde events

        selectionChanged: function () {
            if (!isGridLoad)
                updateSelectedRecordsInGrid($("#searchCompanyResults"), jGridsList["searchCompanyResults"], "Id");
            //Get all selected rows
            var $selectedRows = $('#searchCompanyResults').jtable('selectedRows');
            jsonObj = '';
            //    		if ($selectedRows.length > 0) {
            //    			//Show selected rows
            //    			$selectedRows.each(function () {
            //    				var record = $(this).data('record');
            //    				jsonObj = jsonObj + record.Id + ",";
            //    			});
            //    		}
            if (jGridsList["searchCompanyResults"].length > 0) {
                for (var i = 0; i < jGridsList["searchCompanyResults"].length; i++) {
                    jsonObj = jsonObj + jGridsList["searchCompanyResults"][i].Id + ",";
                }
            }
        }
    });
    if (workGroupId.length == 0) {
        $('#searchCompanyResults').jtable('load', {
            name: $('#CompanyDetails_Name').val(),
            isacCode: $('#isaccode').val(),
            country: $('#country').val()
        });
    }
    else {
        $('#searchCompanyResults').jtable('load', {
            name: $('#CompanyDetails_Name').val(),
            isacCode: $('#isaccode').val(),
            country: $('#country').val(),
            workgroupId: workGroupId
        });
    }
}

function GetSeachCompanyType(workGroupId, clickevent) {
    if (workGroupId.length == 0 || clickevent == "delete" || $('#hdnSavedCompanyValues').val() != "" || clickevent == "Add" || fromPage == 'maintainchildworkgroup') {
        return '/GCS/WorkGroup/AddCompany';
    }

    else {
        return '/GCS/WorkGroup/GetCompaniesOfWorkgroup';
    }
}

function GetCompaniesTypeForSearch(workGroupId) {
    if (workGroupId.length == 0) {
        return '/GCS/WorkGroup/SearchCompany';
    }

    else {
        return '/GCS/WorkGroup/GetCompaniesOfWorkgroup';
    }
}

function ShowManageCompanyPartialViewPopup() {
    var objDialog = $('#manageCompanyContainer')

        .dialog({
            autoOpen: true,
            modal: true,
            title: 'Manage Company',
            show: 'clip',
            hide: 'clip',
            width: 1000
        });
}

//Loads Added Company jtable with Save,Remove and CancelButton

function LoadAddedSearchJtable(clickevent) {
    hideErrorDiv();
    $('#addedManageCompanyResults').jtable({
        /*title: 'List of Companies',*/
        paging: true,
        pageSize: pagingCount,
        sorting: false,
        defaultSorting: 'Company ASC',
        selecting: true,
        columnResizable: false,
        multiselect: true,
        selectOnRowClick: false,
        selectingCheckboxes: true,

        actions: {
            listAction: GetSeachCompanyType(workGroupId, clickevent)
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
                width: '5%'
            },
            Name: {
                title: mgCompJtableColNames.Company,
                width: '40%'
            },

            ISACCode: {
                title: mgCompJtableColNames.ISACCompCode,
                width: '20%'
            },

            CountryName: {
                title: mgCompJtableColNames.CountryName,
                width: '35%'
            }
        },
        recordsLoaded: function (event, data) {
            $('#addedManageCompanyResults .jtable thead tr th:first').remove();
            $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#addedManageCompanyResults .jtable thead tr:first');
            if (data.records.length > 0) {
                companyIdsWhenSave = '';
                $('#btnRemove').show();
                $('#btnSave').show();
                $('#btnCancel').show();
                $('#spnMgCompanyAddedResultLabel').css({ "display": "block" });
                $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
                $('#spnMgCompanyAddedResultLabel').show();
                for (var i = 0; i < data.records.length; i++) {
                    companyIdsWhenSave += data.records[i].Id + ',';
                }
                removedAllRows = "false";
            }
            else {
                removedAllRows = "true";
                $('#btnRemove').hide();
                $('#btnSave').show();
                $('#btnCancel').show();
                document.getElementById('hdnAddedValues').value = '';
                companyIdsWhenSave = '';
                if (clickevent == 'save') {
                    document.getElementById('hdnSavedCompanyValues').value = '';
                }
            }
        },

        //Register to selectionChanged event to hanlde events

        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('#addedManageCompanyResults').jtable('selectedRows');
            selectedCompIds = '';
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    selectedCompIds = selectedCompIds + record.Id + ",";
                });
            }
            else {
                selectedCompIds = '';
            }
        }
    });
    if (clickevent == 'Add') {
        if ($('#hdnMaintainWorkGroup').length != 0) {
            //  jsonObj = jsonObj + companyValues;
            jsonObj = jsonObj + ManageCompany
        }

        // ManageCompany = jsonObj;
        //added for routing variations
        if (fromPage.toLowerCase() == 'createchildworkgroup' || fromPage.toLowerCase() == 'maintainchildworkgroup' || fromPage.toLowerCase() == 'routingvariations') {
            document.getElementById('hdnAddedValues').value = companyIdsWhenSave;
        }

        if (jsonObj != '') {
            document.getElementById('hdnAddedValues').value = jsonObj + document.getElementById('hdnAddedValues').value;
        }

        $('#addedManageCompanyResults').jtable('load', {
            companyIds: $('#hdnAddedValues').val(),
            deletedCompIds: "",
            isSort: 'false'
        });
    }

    else if (clickevent == 'companiesforworkgroup') {
        if ($('#hdnMaintainWorkGroup').length != 0) {
            jsonObj = jsonObj + companyValues;
        }

        ManageCompany = jsonObj;

        if (jsonObj != '') {
            document.getElementById('hdnAddedValues').value = jsonObj + document.getElementById('hdnAddedValues').value;
        }

        if (workGroupId.length == 0) {
            $('#addedManageCompanyResults').jtable('load', {
                companyIds: $('#hdnAddedValues').val(),
                deletedCompIds: "",
                isSort: 'false'
            });
        }
        else {
            $('#addedManageCompanyResults').jtable('load', {
                name: $('#CompanyDetails_Name').val(),
                isacCode: $('#isaccode').val(),
                country: $('#country').val(),
                workgroupId: workGroupId
            });
        }
    }

    else if (clickevent == 'save') {
        if (fromPage == "routingVariations") {//send saved companies to routing page
            document.getElementById('hdnSavedCompanyValues').value = companyIdsWhenSave;
            mngCompanyCallbackHandler(companyIdsWhenSave, "");
        } else {
            if ($('#hdnMaintainWorkGroup').length != 0) {
                companyValues = companyIdsWhenSave;
            }
            //ManageCompany = jsonObj; //Moved to out side of else condition fo routing variations

            document.getElementById('hdnSavedCompanyValues').value = companyIdsWhenSave;
            document.getElementById('hdnCompanyIds').value = companyIdsWhenSave;
            CompanySaved(companyIdsWhenSave);
        }
        ManageCompany = jsonObj;
        selectedCompIds = '';
        jsonObj = '';
    }
        //}

    else if (clickevent == 'delete') {
        jsonObj = companyIdsWhenSave;
        if (workGroupId.length > 0 || fromPage.toLowerCase() == 'createparentworkgroup') {
            document.getElementById('hdnAddedValues').value = companyIdsWhenSave;
        }
        //Added for Routing Variations
        if (fromPage.toLowerCase() == 'routingvariations') {
            document.getElementById('hdnAddedValues').value = companyIdsWhenSave;
        }

        if (selectedCompIds.length > 0) {
            var arrCompanyIds = selectedCompIds.split(',');
            for (var row = 0; row < arrCompanyIds.length - 1; row++) {
                if (document.getElementById('hdnAddedValues').value.match(arrCompanyIds[row])) {
                    document.getElementById('hdnAddedValues').value = document.getElementById('hdnAddedValues').value.replace(arrCompanyIds[row] + ',', "");
                }
                if (jsonObj.match(arrCompanyIds[row])) {
                    jsonObj = jsonObj.replace(arrCompanyIds[row] + ',', "");
                }
            }
            ManageCompany = jsonObj;
            if ($('#hdnMaintainWorkGroup').length == 0) {
                $('#addedManageCompanyResults').jtable('load', {
                    companyIds: $('#hdnAddedValues').val(),
                    deletedCompIds: selectedCompIds
                });
            }
            else {
                $('#addedManageCompanyResults').jtable('load', {
                    companyIds: jsonObj,
                    deletedCompIds: selectedCompIds
                });
            }
        }
        else {
            showErrorDiv(noRowsSelected);
        }
    }
    else if (clickevent == 'aftersave') {
        Loadwatermark();
        if (fromPage != "") {
            if ($('#hdnSavedCompanyValues').val() != "") {
                $('#addedManageCompanyResults').jtable('load', {
                    companyIds: $('#hdnSavedCompanyValues').val(),
                    deletedCompIds: ""
                });
            }
            else {
                if (companyValues != "") {
                    $('#addedManageCompanyResults').jtable('load', {
                        companyIds: companyValues,
                        deletedCompIds: ""
                    });
                    $('#addedManageCompanyResults').show();
                }
                if (companyValues == "")
                    $('#addedManageCompanyResults').hide();
            }
        }
        else {
            if ($('#hdnMaintainWorkGroup').length == 0) {
                $('#addedManageCompanyResults').jtable('load', {
                    companyIds: $('#hdnSavedCompanyValues').val(),
                    deletedCompIds: ""
                });
            }
            else {
                if (companyValues != "") {
                    $('#addedManageCompanyResults').jtable('load', {
                        companyIds: companyValues,
                        deletedCompIds: ""
                    });
                    $('#addedManageCompanyResults').show();
                }
                if (companyValues == "")
                    $('#addedManageCompanyResults').hide();
            }
        }
    }
    else {
        $('#addedManageCompanyResults').jtable('load', {
            companyIds: $('#hdnAddedValues').val(),
            deletedCompIds: selectedCompIds
        });
    }
}

function CompanySaved(selectedCompanies) {
    LoadManageCompanytable(selectedCompanies);
    if (selectedCompanies != "") {
        $('#btnManageCompany').removeClass('input-validation-error');
        if (($("#RolesList").length > 0 && $("#RolesList option:selected").text().match('Inquiry')) || ($('#hdnMaintainWorkGroup').length != 0 && selectedWorkgroupRole.match('Inquiry'))) {
            $('#btnManageTerritories').removeClass('input-validation-error');
        }
    }
    $('#tdcompanyList').removeClass('input-validation-error');
    hideErrorDiv();
}

function Reset() {
    document.getElementById('CompanyDetails_Name').value = '';
    document.getElementById('isaccode').value = '';
    document.getElementById('country').value = '';
}

function Loadwatermark() {
    if (jQuery("#CompanyDetails_Name") != null) {
        jQuery("#CompanyDetails_Name").watermark(watermarkCompanyName);
    }
    if (jQuery("#isaccode") != null) {
        jQuery("#isaccode").watermark(watermarkIsacCode);
    }
    if (jQuery("#country") != null) {
        jQuery("#country").watermark(watermarkCountry);
    }
}

function ResetForSearchCompany() {
    document.getElementById('CompanyDetails_Name').value = '';
    document.getElementById('isaccode').value = '';
    document.getElementById('country').value = '';
}

function LoadwatermarkForSearchCompany() {
    jQuery("#CompanyDetails_Name").watermark(watermarkCompanyName);
    jQuery("#isaccode").watermark(watermarkIsacCode);
    jQuery("#country").watermark(watermarkCountry);
}

function LoadManageCompanytable(companyvalues) {
    hideErrorDiv();
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

function hideErrorDiv() {
    $("#popupMgCompanyerrorMessage").hide();
    $("#popupMgCompanyerrorMessage").html('');
}

function showErrorDiv($msg) {
    $("#popupMgCompanyerrorMessage").show();
    $("#popupMgCompanyerrorMessage").html($msg);
}