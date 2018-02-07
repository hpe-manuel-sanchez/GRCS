/// <reference path="/GCS/Scripts/jquery-1.5.1-vsdoc.js" />
/// <reference path="../jquery-1.5.1.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />




var isChangeLink = false;
var objClearanceDialog = $('<div id="ClearanceAdminDialog"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: '',
            show: 'clip',
            hide: 'clip',
            width: '55%',
            resizable: true,
            position: [(($(window).width() - (($(window).width() * 55) / 100)) / 2), 15]
        });


$(document).ready(function () {

//    $('#DefaultCollapsed').ready(function () {
//        $('#accordion .head').next().toggle();
//        $($('#accordion').find('a')[0]).toggleClass('iconBottom');
//    });

    $('#ReviewRight').attr('disabled', 'disabled');
    $('#ReRoute').attr('disabled', 'disabled');

    reSizePriorityWorkQueuePage();
    $('#clrfilter').hide();

    //Clearance Company link
    //Load partial view
    objClearanceDialog.load('/GCS/RepertoireRightsSearch/ClearanceAdminCountry', "",
                 function (responseText, textStatus) {
                     if (textStatus == 'error') {
                         objClearanceDialog.html('<p>' + messageCommon.error + '</p>');
                     }
                 });

    $('#clearanceAdminCountry').click(function (e) {
        e.preventDefault();
        objClearanceDialog.dialog('option', { title: "ClearenceAdminCountry" });
        //Open Dialog
        objClearanceDialog.dialog('open');
    });


    //Clear Filter Click
    $('#clrfilter').click(function (e) {
        e.preventDefault(); //clrfilter
        $('#FilterApp').hide();
        $('#clrfilter').hide();
        $('#ArtistName').val('');
        $('#ContractDescription').val('');
        $('#Titles').val('');
        $('#AdminCompany').val('');
        $('#AdminCompanyNames').val('');

        $('#WorkQueues_ArtistName').val('142');
        $('#WorkQueues_Title').val('142');
        $('#WorkQueues_ContractDescription').val('142');
        $('#WorkQueues_ContractReviewReason').val('0');

        $('#accordion').next().toggle();
        $($('#accordion').find('a')[0]).toggleClass('iconBottom');
        $('#workQueueGrid').jtable('load', {
            artistName: $('#ArtistName').val(),
            contractDesc: $('#ContractDescription').val(),
            descTitle: $('#Titles').val(),
            reasonForReview: $('#WorkQueues_ContractReviewReason option:selected').text(),
            adminCompany: $('#AdminCompany').val()
        });

    });


    //Accordion style collapse/expand
    $('#accordion .head').click(function (e) {
        HideWarningSuccess();
        e.preventDefault();
        $(this).next().toggle();
        $($('#accordion').find('a')[0]).toggleClass('iconBottom');

        return false;

    }).next().show();


    reSizePriorityWorkQueuePage();
    //Resize
    $(window).resize(function () {
        reSizePriorityWorkQueuePage();
    });

});




//Resize page

function reSizePriorityWorkQueuePage() {
    var h = $(window).height();
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".scrollWorkQueue").css('height', h - 185);
    else
        $(".scrollWorkQueue").css('height', h - 225);
}
        



