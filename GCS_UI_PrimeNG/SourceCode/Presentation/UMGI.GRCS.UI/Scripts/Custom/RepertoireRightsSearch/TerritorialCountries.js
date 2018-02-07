/// <reference path="../jquery-1.5.1-vsdoc.js" />
/// <reference path="../jquery-1.5.1.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var textBackup = '';
var timeOutId;
$(document).ready(function () {
    reSize();
    timeOutId = setTimeout(cacAutoSearch, 1000);

    /*To Show the buttons for preclearance Exclusion Popup and hiding Repertoire Search*/
    if ($('#divRightsPriorityWorkQueueTab').length > 0) {
        $('#divBtnsForRepertoireSearch').hide();
        $('#divBtnsForPreClearanceTab').show();
        countryClickFunctions();
    }
    if ($(".divobjBulkEditPreClearancePopUp").is(':visible')) {
        $("#divBtnsForPreClearanceTab").hide();
    }
    $("#txtClearanceAdminComp").keyup(function (e) {
        setTimeout(cacAutoSearch, 1000);
        e.preventDefault();
        if (textBackup != $("#txtClearanceAdminComp").val() && $("#txtClearanceAdminComp").val() != '' && $("#txtClearanceAdminComp").val().length > 1) {
            $(this).addClass('ui-autocomplete-loading');
            $("#imgClearIcon").hide();
            if (timeOutId) {
                clearTimeout(timeOutId);
                timeOutId = setTimeout(cacAutoSearch, 1000);
            }
            else {
                timeOutId = setTimeout(cacAutoSearch, 1000);
            }

        }
        else {
            $('#txtClearanceAdminComp').addClass('ui-autocomplete-input');
            $("#imgClearIcon").hide();
        }
    });

    $('#labelSelCount').html('0');

    $('#linkRemoveAll').click(function () {
        //$('#txtClearanceAdminComp').val('');
        $('#SelectedList').find('tr').remove();
        $('#ClearenceList').find('input:checkbox').attr('checked', false);
        $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
        if ($(".divobjBulkEditPreClearancePopUp").is(':visible')) {
            window.parent.checkSelectedEmpty();
        }
        $('#txtClearanceAdminComp').addClass('ui-autocomplete-input');
        $("#imgClearIcon").hide();
    });

    $('#subOk').click(function () {
        $('#AdminCompanyNames').val(getClearanceValues());
        $('#SearchCriteria_SearchRightsCriteria_TerritorialRightsId').val(getClearanceIds());
        $('#ClearanceAdminDialog').dialog('close');
    });

    $('#subCancel').click(function () {
        $('#SelectedList').find('tr').remove();
        $('#txtClearanceAdminComp').val('');
        $('#ClearenceList').find('input:checkbox').attr('checked', false);
        $('#txtClearanceAdminComp').val('');
        // $('#SelectedList').find('tr').remove();
        $('#ClearenceList').find('input:checkbox').attr('checked', false);
        $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
        //        $('#ClearenceList').find('tr').remove();
        if (textBackup.length > 0) {
            $('#txtClearanceAdminComp').val('');
            clearTimeout(timeOutId);
            timeOutId = setTimeout(cacAutoSearch, 1000);
        }
        $('#ClearanceAdminDialog').dialog('close');
    });

    $('#linkClear').click(function () {
        $('#SelectedList').find('tr').remove();
        $('#txtClearanceAdminComp').val('');
        $('#ClearenceList').find('input:checkbox').attr('checked', false);
        $('#txtClearanceAdminComp').val('');
        // $('#SelectedList').find('tr').remove();
        $('#ClearenceList').find('input:checkbox').attr('checked', false);
        $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
        //        $('#ClearenceList').find('tr').remove();
        if (textBackup.length > 0) {
            $('#txtClearanceAdminComp').val('');
            clearTimeout(timeOutId);
            timeOutId = setTimeout(cacAutoSearch, 1000);
        }
        if ($(".divobjBulkEditPreClearancePopUp").is(':visible')) {
            window.parent.checkSelectedEmpty();
        }
        $('#txtClearanceAdminComp').addClass('ui-autocomplete-input');
        $("#imgClearIcon").hide();
    });

});

function createCACTableRow(tableId, check) {
    var isAvailable = false;
    $('#SelectedList').find('input:checkbox').each(function () {
        if ($(this).attr('id') == check.id) {
            isAvailable = true;
            return false;
        }
        return true;
    });

    if (isAvailable)
        return false;

    var checkNew = document.createElement('input');
    checkNew.type = "checkbox";
    checkNew.value = check.value;
    checkNew.id = check.id;
    $(checkNew).css('display', 'none');
    var x = document.getElementById(tableId).insertRow($('#SelectedList').find('tr').length);
    var y = x.insertCell(0);
    var z = x.insertCell(1);
    var z1 = x.insertCell(2);
    $(x).attr("class", "seltd1");
    $(y).attr("class", "seltd2");
    $(z).attr("class", "seltd3");
    $(z1).attr("class", "seltd4");
    y.appendChild(checkNew);
    z.innerHTML = checkNew.value;
    $(z1).append('<div class="imgClose"></div>');

    $(x).find('input:checkbox').attr('checked', true);

    $(z1).find(".imgClose").click(function () {
        if (!this.checked) {
            UnCheckAlreadyAvailable($(this).parents('tr').find('input:checkbox'));
            $(this).parents('tr').remove();
            $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
        }
    });
    if ($(".divobjBulkEditPreClearancePopUp").is(':visible')) {
        if ($('#SelectedList').children().children().length >= 1) {
            window.parent.checkSelectedNotEmpty();
        } else {
            window.parent.checkSelectedEmpty();
        }
    }
    $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
    GetSorting();
    return true;
}
function sortByKey(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    });
}


function GetSorting() {
    var table = document.getElementById("SelectedList");

    // var allRow = table.getElementsByTagName('td[class=seltd3]')[0];
    var allRow = $(table).find('tr');
    
    var ar = new Array();
    for (var i = 0; i < allRow.length; i++) {
        var elem = new Object();
        elem.tr = allRow[i];
        elem.value = $($(allRow[i]).find('.seltd3')).text();
        ar[i] = elem;
            
    }

    var result = sortByKey(ar, 'value');
    $(table).find('tr').remove();
    $(result).each(function () {
        $(table).find('tbody').append("<tr>" + $($(this)[0].tr).html() + "</tr>");
    });

    $(table).find(".imgClose").each(function () {
        $(this).click(function () {
            if (!this.checked) {
                UnCheckAlreadyAvailable($(this).parents('tr').find('input:checkbox'));
                $(this).parents('tr').remove();
                $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
                if ($(".divobjBulkEditPreClearancePopUp").is(':visible')) {
                    if ($('#SelectedList').children().children().length == 0) {
                        window.parent.checkSelectedEmpty();
                    }
                }
            }
        });
    });
}


function isAlreadyAvailable(check) {

    var isAvailable = false;

    $('#SelectedList').find('input:checkbox').each(function () {
        if ($(this).attr('id') == check.id) {
            isAvailable = true;
            return false;
        }
        return true;
    });

    return isAvailable;
}

function getClearanceValues() {
    var clearanceString = '';
    $('#SelectedList').find('input:checkbox').each(function () {
        if (clearanceString == '')
            clearanceString += $(this).attr('value');
        else
            clearanceString += ', ' + $(this).attr('value');
    });
    return clearanceString;
}

function getClearanceIds() {
    var clearanceString = '';
    $('#SelectedList').find('input:checkbox').each(function () {
        if (clearanceString == '')
            clearanceString += $(this).attr('id');
        else
            clearanceString += ', ' + $(this).attr('id');
    });
    return clearanceString;
}

function getClearanceIdsWithName() {
    var clearanceNameIdString = '';
    $('#SelectedList').find('input:checkbox').each(function () {
        if (clearanceNameIdString == '') {
            clearanceNameIdString += $(this).attr('id') + ":" + $(this).attr('value');
        } else {
            clearanceNameIdString += ', ' + $(this).attr('id') + ":" + $(this).attr('value');
        }
    });
    return clearanceNameIdString;
}

function reSize() {
if ($(".cacTableContainer").is(':visible')) {
    var TotDiaHgt = $("#ClearanceAdminDialog").height();
        var ReduceHgt = TotDiaHgt - 110;
        $(".cacTable").css('height', ReduceHgt + "px");
    }
    if ($("#divTerritoryExclusionPopup").is(':visible')) {
        if ($(".cacTableContainer").is(':visible')) {
            var TotDiaHgt = $("#divTerritoryExclusionPopup").height();
            var ReduceHgt = TotDiaHgt - 135;
            $(".cacTable").css('height', ReduceHgt + "px");
        }
    }
}

$(window).resize(function () {
    reSize();
});

function UnCheckAlreadyAvailable(check) {

    var isAvailable = false;
    $('#ClearenceList').find('input:checkbox').each(function () {
        if ($(this).attr('id') == $(check).attr('id')) {
            isAvailable = true;
            $(this).attr('checked', false);
            return false;
        }
        return true;
    });

    return isAvailable;
}

function cacAutoSearch() {

    $.post("/GCS/RepertoireRightsSearch/SelectTerritorialCountries?term=" + $("#txtClearanceAdminComp").val(), function (data) {
        $('#txtClearanceAdminComp').focus();
        $('#txtClearanceAdminComp').removeClass('ui-autocomplete-loading');

        $('#ClearenceList').empty();
        textBackup = $("#txtClearanceAdminComp").val();
        if (window.parent.checkIsCountryAlreadyExcluded && window.includedCountryArray != undefined && $('.divobjBulkEditPreClearancePopUp').length == 0) {
            data = JSLINQ(data).Where(function (item) {
                if (JSLINQ(window.includedCountryArray).Any(function (country) { return parseInt(country) == item.Id; }) == true) {
                    return item;
                }
            });
            data = data.items;
        }

        for (var index = 0; index <= data.length - 1; index++) {
            if ($('#ClearenceList').length > 0) {
                var check = document.createElement('input');
                check.type = "checkbox";
                check.value = data[index].Name;
                check.id = data[index].Id;

                if (isAlreadyAvailable(check)) {
                    $(check).prop('checked', true);
                }


                /*To select checkbox if its from Priority WorkQueue Tabs*/
                if (window.parent.checkIsCountryAlreadyExcluded) {
                    check = window.parent.checkIsCountryAlreadyExcluded(check);
                }

                var x = document.getElementById('ClearenceList').insertRow($('ClearenceList').find('tr').length);
                var y = x.insertCell(0);
                var z = x.insertCell(1);
                y.appendChild(check);
                z.innerHTML = data[index].Name;

                $(x).attr("class", "sertd1");
                $(y).attr("class", "sertd2");
                $(z).attr("class", "sertd3");
                /// <reference path="TerritorialCountries.js" />

                $(check).click(function () {
                    if (this.checked) {
                        createCACTableRow('SelectedList', this);
                    }
                });
            }
        }
        if ($("#txtClearanceAdminComp") != null && $("#txtClearanceAdminComp").length > 0) {
            if ($("#txtClearanceAdminComp").val() != '' && $("#txtClearanceAdminComp").val().length > 1) {
                $('#txtClearanceAdminComp').removeClass('ui-autocomplete-input');
                $("#imgClearIcon").show();
            }
        }

    });
   
}


// Clearance Country text box 

function clearCountry() {
    $("#txtClearanceAdminComp").val("");
    $("#imgClearIcon").hide();
    
$('#txtClearanceAdminComp').addClass('ui-autocomplete-input');
    $('#txtClearanceAdminComp').keyup();
}
