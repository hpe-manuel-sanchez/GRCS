/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var textBackup = '';
var timeOutId;

$(document).ready(function () {

    reSize();

    $("#txtClearanceAdminComp").keyup(function (e) {
        e.preventDefault();
        if (textBackup != $("#txtClearanceAdminComp").val() && $("#txtClearanceAdminComp").val() != '' && $("#txtClearanceAdminComp").val().length > 1) {
            $(this).addClass('ui-autocomplete-loading');

            if (timeOutId) {
                clearTimeout(timeOutId);
                timeOutId = setTimeout(cacAutoSearch, 1000);
            }
            else {
                timeOutId = setTimeout(cacAutoSearch, 1000);
            }
        }
    });

    $('#labelSelCount').html('0');

    $('#linkRemoveAll').click(function () {
        $('#txtClearanceAdminComp').val('');
        $('#SelectedList').find('tr').remove();
        $('#ClearenceList').find('tr').remove();
        $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
    });

    $('#subOk').click(function () {
        $('#AdminCompanyNames').val(getClearanceValues());
        $('#AdminCompany').val(getClearanceIds());
        $('#ClearanceAdminDialog').dialog('close');
    });

    $('#subCancel').click(function () {
        $('#ClearanceAdminDialog').dialog('close');
    });

    $('#linkClear').click(function () {
        $('#txtClearanceAdminComp').val('');
        $('#ClearenceList').find('tr').remove();
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
    $(z1).append('<image class="imgClose" src="/GCS/Images/Close.png"></image>');
    
    $(x).find('input:checkbox').attr('checked', true);

    $(z1).find(".imgClose").click(function () {
        if (!this.checked) {
            UnCheckAlreadyAvailable($(this).parents('tr').find('input:checkbox'));
            $(this).parents('tr').remove();
            $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
        }
    });

    $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
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

function reSize() {
    var h = $(window).height();

    $(".cacTable").css('height', h - 300);
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

    $.post("/GCS/Global/AutoSearchClearanceCompCountry?term=" + $("#txtClearanceAdminComp").val(), function (data) {
        $('#txtClearanceAdminComp').removeClass('ui-autocomplete-loading');
        $('#ClearenceList').empty();
        textBackup = $("#txtClearanceAdminComp").val();
        for (var index = 0; index < data.length; index++) {

            var check = document.createElement('input');
            check.type = "checkbox";
            check.value = data[index].value;
            check.id = data[index].id;

            if (isAlreadyAvailable(check)) {
                $(check).prop('checked', true);
            }

            var x = document.getElementById('ClearenceList').insertRow($('ClearenceList').find('tr').length);
            var y = x.insertCell(0);
            var z = x.insertCell(1);
            y.appendChild(check);
            z.innerHTML = data[index].value;

            $(x).attr("class", "sertd1");
            $(y).attr("class", "sertd2");
            $(z).attr("class", "sertd3");

            $(check).click(function () {
                if (this.checked) {
                    createCACTableRow('SelectedList', this);
                }
            });

        }
    });

}