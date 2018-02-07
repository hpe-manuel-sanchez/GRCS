var company = {
    Name: "Company Name",
    ISACCode: "ISAC Code",
    CountryName: "Country"
};

var workgroupSelected = '';
var workgroupid = '';
var workgroupname = '';
var parentID = '';
var parentName = '';
var isParent = false;
var workgroupModifiedDateTime = '';
$('.ui-dialog-titlebar-close').attr("title", "Close");
var pageSize = 25;

$(document).ready(function () {
    if (pageName == 'Deactivate') {
        document.getElementById('btnDeactiveViewWorkGroup').style.display = 'block';
        document.getElementById('btnDeleteViewWorkGroup').style.display = 'none';
    }
    else if (pageName == 'Delete') {
        document.getElementById('btnDeactiveViewWorkGroup').style.display = 'none';
        document.getElementById('btnDeleteViewWorkGroup').style.display = 'block';
    } else {
        document.getElementById('btnDeactiveViewWorkGroup').style.display = 'none';
        document.getElementById('btnDeleteViewWorkGroup').style.display = 'none';
    }
});

function LoadCompanies() {
    $('#jqgridcompany').jtable({
        paging: false,
        pageSize: pageSize,
        sorting: false,
        defaultSorting: 'Name ASC',
        selecting: false,
        columnResizable: false,
        multiselect: false,
        selectingCheckboxes: false,
        selectOnRowClick: false,
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () {
            $('.jtable .jtable-no-data-row').show();
        },
        actions: {
            listAction: '/GCS/workgroup/DataViewCompany'
        },
        fields: {
            Name:
                {
                    title: company.Name
                },
            ISACCode:
                {
                    title: company.ISACCode
                },
            CountryName:
                {
                    title: company.CountryName
                }
        }
    });

    $('#jqgridcompany').jtable('load', {
        companies: document.getElementById('hdnViewCompany').value
    });
};

function LoadArtistContract() {
    $('#jqgridArtistContract').jtable({
        paging: false,
        pageSize: pageSize,
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
            ContractId:
                {
                    title: artistContract.ContractId
                },
            ClearanceAdminCompanyName: {
                title: artistContract.ClrAdminCompany,
                width: '25%'
            },
            ClearanceAdminCompanyId: {
                title: artistContract.ClrAdminCompanyID,
                width: '25%'
            }
        }
    });

    $('#jqgridArtistContract').jtable('load', {
        contractids: contractIdListForArtist
    });
};

function LoadResourceContract() {
    $('#jqgridResourceContract').jtable({
        paging: false,
        pageSize: pageSize,
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
            listAction: '/GCS/workgroup/GetResourceContractByContractIdList'
        },
        fields: {
            ArtistId: {
                title: artistContract.ArtistID,
                width: '13%'
            },
            ArtistName: {
                title: artistContract.ArtistName,
                width: '15%'
            },
            Isrc:
                {
                    title: artistContract.Isrc
                },
            ResourceTitle:
                {
                    title: artistContract.ResourceTitle,
                    width: '14%'
                },
            ContractId:
                 {
                     title: artistContract.ContractId,
                     width: '10%'
                 },

            ClearanceAdminCompanyName: {
                title: artistContract.ClrAdminCompany,
                width: '20%'
            },
            ClearanceAdminCompanyId: {
                title: artistContract.ClrAdminCompanyID,
                width: '23%'
            }
        }
    });
    $('#jqgridResourceContract').jtable('load', {
        //        contractids: contractIdListForResource
        deviationResourceContract: JSON.stringify(maintainCRCollection)
    });
};

$(function () {
    $('#btnDeactiveViewWorkGroup').click(function () {
        workgroupSelected = $('#SelectedRowList')[0].innerHTML;
        var workgroups = workgroupSelected.split('~');
        workgroupid = workgroups[0];
        workgroupname = workgroups[1];
        parentName = workgroups[2];
        parentID = workgroups[3];
        workgroupModifiedDateTime = workgroups[5];
        if (workgroupSelected.length > 0) {
            if (parentName == 'Parent') { isParent = true; }
            if (workgroupid != null && workgroupid != '') {
                showPendingRequest(workgroupid, workgroupModifiedDateTime, parentID, workgroupname, isParent);
            }
        }
        else {
            displayDialog(searchWorkgroupMessages.viewworkgrouppopuptitle, viewWorkgroupMessages.noRowsSelected);
        }
    });
});

$(function () {
    $('#btnCancelView').click(function () {
        $('#SearchViewWorkgroup').dialog("close");
        $('#ViewWorkgroup').dialog('close');
        return false;
    });
});

$(function () {
    $('#btnDeleteViewWorkGroup').click(function () {
        var $selectedRows = $('#searchList').jtable('selectedRows');
        $selectedRows.each(function () {
            var record = $(this).data('record');
            workgroupId = record.ID;
        });
        var selectedmodifiedDateTime = document.getElementById('hdnModifiedTime').value;
        var formValues = "WorkgroupID=" + workgroupId + "&modifiedDateTime=" + selectedmodifiedDateTime + "&Id=View";
        $.post('/GCS/Workgroup/DeleteWorkgroup', formValues, function (data) {
            if (data == false) {
                if (frompage == "viewworkgroup") {
                    $('#ViewWorkgroup').dialog('close');
                    frompage = "";
                }
                $('#searchList').jtable('deleteRecord', {
                    key: workgroupId,
                    clientOnly: true,
                    animationsEnabled: false
                });

                $('#deleteWgSuccessMessage').show();
                $('#deleteWgSuccessMessage').html(msgsuccess);
                $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
            }
            else {
                $('#viewSelectedWorkgroupErrorMessage').show();
                $('#viewSelectedWorkgroupErrorMessage').html(msgpendingrequest);
            }
        });
    });
});

function setTerritoryCountry(territoryDisplayCollection) {
    //Clear the Countries
    $("#selectedCountries").html('');
    $("#excludedCountries").html('');

    var InludedTerritories = [];
    var IncludedALLCountries = [];
    var stringOfcountriesterritories = "";
    var stringOfexcountriesterritories = "";

    InludedTerritories = JSLINQ(territoryDisplayCollection[0].Value).Where(function (dict) { return dict.IsTerritory == true && dict.IsIncluded == true; }).OrderBy(function (dict) { return dict.Name; });
    IncludedALLCountries = JSLINQ(territoryDisplayCollection[0].Value).Where(function (dict) { return dict.IsTerritory == false && dict.IsIncluded == true; }).OrderBy(function (dict) { return dict.Name; });

    var dupliacteTerritories = {};
    var distinctTerritories = [];

    $.each(InludedTerritories.items, function (i, data) {
        if (!dupliacteTerritories[data.Name]) {
            dupliacteTerritories[data.Name] = true;
            distinctTerritories.push(data);
        }
    });

    // Display Logic for LINK Included countries
    for (var i = 0; i < distinctTerritories.length; i++) {
        var countries = "";
        var id = distinctTerritories[i].Id;
        var IncludedCountriesForThis = JSLINQ(territoryDisplayCollection[0].Value).Where(function (dict) { return dict.ParentId == id && dict.IsIncluded == true; });

        // get all countries under territories
        for (var j = 0; j < IncludedCountriesForThis.items.length; j++) {
            if (countries != "") {
                countries = countries + '; ' + IncludedCountriesForThis.items[j].Name;
            }
            else {
                countries = IncludedCountriesForThis.items[j].Name;
            }
            stringOfcountriesterritories = stringOfcountriesterritories + countries;
        }

        if (i == distinctTerritories.length - 1) {
            var link = '<a href="javascript:void(0);" style = "color:#000;" title="' + countries + '">' + distinctTerritories[i].Name + '</a> ';
        }
        else {
            var link = '<a href="javascript:void(0);" style = "color:#000;" title="' + countries + '">' + distinctTerritories[i].Name + '</a>; ';
        }
        $('#selectedCountries').append(link);
        countries = "";
    }

    // *******START for all included countries label which donot have territory selected*******//
    var countrylabel = "";

    var dupliacteCountries = {};
    var distinctCountries = [];

    $.each(IncludedALLCountries.items, function (i, data) {
        if (!dupliacteCountries[data.Name]) {
            dupliacteCountries[data.Name] = true;
            distinctCountries.push(data);
        }
    });

    for (var i = 0; i < distinctCountries.length; i++) {
        var count = 0;

        for (var j = 0; j < distinctTerritories.length; j++) {
            if (distinctTerritories[j].Id == distinctCountries[i].ParentId) {
                count = count + 1;
            }
        }
        if (count == 0) {
            if ($('#selectedCountries').html() != "") {
                countrylabel = countrylabel + '; ' + distinctCountries[i].Name;
            }
            else {
                if (countrylabel == "")
                    countrylabel = distinctCountries[i].Name;
                else
                    countrylabel = countrylabel + '; ' + distinctCountries[i].Name;
            }
        }
    }

    var includeString = ""; // for displaying unique country
    var countryArray = countrylabel.split(';'); // convert country label into array as country can be in multiple territories thus remove duplicities'
    if (stringOfcountriesterritories != null)
        var arrayOfcountriesterritories = stringOfcountriesterritories.split(';');
    // remove all countries from which territories selected
    for (var i = 0; i < arrayOfcountriesterritories.length; i++) {
        countryArray = jQuery.grep(countryArray, function (value) {
            return value != arrayOfcountriesterritories[i];
        });
    }

    //
    var uniquecountryNames = [];
    $.each(countryArray, function (i, el) {
        if ($.inArray(el, uniquecountryNames) === -1)
            uniquecountryNames.push(el);
    });

    // covert unique country names into string and display
    jQuery.each(uniquecountryNames, function (index, value) {
        includeString += value + ";";
    });
    if (includeString != "") {
        includeString = includeString.substring(0, includeString.length - 1);
    }

    $('#selectedCountries').append(includeString);
    $("#btnManageTerritories").css("margin-top", "5px");
    // *******END for all included countries label which donot have territory selected*******//
}