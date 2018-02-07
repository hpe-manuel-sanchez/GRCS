/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/CustomSetting.js" />

var textbackPreupPre = '';
var timeOutIdPre;
var backPre = '';
$(document).ready(function () {

    reSizePre();
    //UC12 predefined Parameter/.
    $("#txtClearanceAdminCompPre").keyup(function (e) {
        e.preventDefault();
        if (textbackPreupPre != $("#txtClearanceAdminCompPre").val() && $("#txtClearanceAdminCompPre").val() != '' && $("#txtClearanceAdminCompPre").val().length > 1) {
            $(this).addClass('ui-autocomplete-loading');
            if (timeOutIdPre) {
                clearTimeout(timeOutIdPre);
                timeOutIdPre = setTimeout(cacAutoSearchPre, 200);
            }
            else {
                timeOutIdPre = setTimeout(cacAutoSearchPre, 200);
            }
        }
    });

    $('#labelSelCountPre').html('0');

    $('#linkRemoveAllPre').click(function () {
        $('#txtClearanceAdminCompPre').val('');
        $('#SelectedListPre').find('tr').remove();
        var tr = $('#ClearenceListPre').find('tr');
        $(tr).each(function () {
            $(this).find('input').removeAttr('checked');
        });
        //  $('#ClearenceListPre').find('tr').remove();
        $('#labelSelCountPre').html($('#SelectedListPre').find('input:checkbox').length);
    });

    $('#linkClearPre').click(function () {
        $('#txtClearanceAdminCompPre').val('');
        $('#ClearenceListPre').find('input:checkbox').attr('checked', false);
        $('#txtClearanceAdminCompPre').val('');
//        $('#SelectedListPre').find('tr').remove();
        $('#ClearenceListPre').find('input:checkbox').attr('checked', false);
        $('#labelSelCountPre').html($('#SelectedListPre').find('input:checkbox').length);
        //        $('#ClearenceListPre').find('tr').remove();
    });


});

function createCACTableRowPre(tableId, check) {
    
    var isAvailable = false;
    $('#SelectedListPre').find('input:checkbox').each(function () {
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
    var x = document.getElementById(tableId).insertRow($('#SelectedListPre').find('tr').length);
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
            UnCheckAlreadyAvailablePre($(this).parents('tr').find('input:checkbox'));
            $(this).parents('tr').remove();
            $('#labelSelCountPre').html($('#SelectedListPre').find('input:checkbox').length);
        }
    });

    $('#labelSelCountPre').html($('#SelectedListPre').find('input:checkbox').length);
    return false;
}

function isAlreadyAvailablePre(check) {

    var isAvailable = false;
    
    $('#SelectedListPre').find('input:checkbox').each(function () {
        if ($(this).attr('id') == check.id) {
            isAvailable = true;
            return false;
        }
        return true;
    });

    return isAvailable;
}

function getClearanceValuesPre() {
    var clearanceString = '';
    $('#SelectedListPre').find('input:checkbox').each(function () {
        if (clearanceString == '')
            clearanceString += $(this).attr('value');
        else 
            clearanceString += ', ' + $(this).attr('value');
    });
    return clearanceString;
}

function getClearanceIdsPre() {
    var clearanceString = '';
    $('#SelectedListPre').find('input:checkbox').each(function () {
        if (clearanceString == '')
            clearanceString += $(this).attr('id');
        else
            clearanceString += ', ' + $(this).attr('id');
    });
    return clearanceString;
}

function reSizePre() {
    if ($("#CustomSettingPopUp").is(':visible')) {
                          if ($(".cacTableContainer").is(':visible')) {
                              var TotDiaHgt = $("#CustomSettingPopUp").height();
                              var ReduceHgt = TotDiaHgt - 210;
                                $(".cacTable").css('height', ReduceHgt + "px");
                            }
                        }
                  
}

function reSizePreWindowPre() {
    var h = $(window).height();
    //$(".cacTable").css('height', h - 400);
}

$(window).resize(function () {
    reSizePre();
});

function UnCheckAlreadyAvailablePre(check) {

    var isAvailable = false;
    $('#ClearenceListPre').find('input:checkbox').each(function () {
        if ($(this).attr('id') == $(check).attr('id')) {
            isAvailable = true;
            $(this).attr('checked', false);
            return false;
        }
        return true;
    });

    return isAvailable;
}

function cacAutoSearchPre() {
    var FormatStr = '';
    FormatStr = $("#txtClearanceAdminCompPre").val().replace("&", "|");

    if ($("#txtClearanceAdminCompPre").val().indexOf("&").length = -1)
        FormatStr = FormatStr.replace("&", "|");
    else {
        FormatStr = $("#txtClearanceAdminCompPre").val();
    }

    $.post("/GCS/Global/AutoSearchLinkUnlinkCompCountry?term=" + FormatStr, function (data) {
        $('#txtClearanceAdminCompPre').removeClass('ui-autocomplete-loading');
        $('#ClearenceListPre').empty();
        textbackPreupPre = $("#txtClearanceAdminCompPre").val();
        for (var index = 0; index < data.length; index++) {

            var check = document.createElement('input');
            check.type = "checkbox";
            check.value = data[index].value;
            check.id = data[index].id;

            if (isAlreadyAvailablePre(check)) {
                $(check).prop('checked', true);
            }

            var x = document.getElementById('ClearenceListPre').insertRow($('ClearenceListPre').find('tr').length);
            var y = x.insertCell(0);
            var z = x.insertCell(1);
            y.appendChild(check);
            z.innerHTML = data[index].value;

            $(x).attr("class", "sertd1");
            $(y).attr("class", "sertd2");
            $(z).attr("class", "sertd3");

            $(check).click(function () {
                if (this.checked) {
                    createCACTableRowPre('SelectedListPre', this);
                }
            });

        }
    });

}


