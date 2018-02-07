/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var contractMessages = {
    cancelValid: 'Are you sure, you want to exit the Contract without saving?'
};

$(document).ready(function () {
    resizeContract();

    $(window).resize(function () {
        resizeContract();
    });

    //Tab
    $("#tabs").tabs();

    //Tab Change
    $("#tabs").tabs({

        select: function (event, ui) {
            resizeContract();
            switch (ui.index) {
                case 0:
                    // first tab selected
                    $('.backButton').hide();
                    $('.nextButton').show();
                    break;
                case 1:
                    // second tab selected
                    if ($('#divRightsTab').html() == "") {
                        ReloadRightsTabs();
                    }

                    $('.backButton').show();
                    $('.nextButton').show();
                    break;
                case 2:

                    if ($('#divSecTab').html() == "") {
                        ReloadSecondaryTabs();
                    }

                    // Third tab selected
                    $('.backButton').show();
                    $('.nextButton').hide();
                    break;
            }
        }

    });

    $('.backButton').hide();

    $('.backButton').click(function () {
        var selTab = $("#tabs").tabs('option', 'selected');
        $("#tabs").tabs('select', selTab - 1);
    });

    $('.nextButton').click(function () {
        var selTab = $("#tabs").tabs('option', 'selected');
        $("#tabs").tabs('select', selTab + 1);
    });

    $('#btnExit').click(function (e) {
        e.preventDefault();

        var cancel = confirm(contractMessages.cancelValid);
        if (cancel) {
            history.go(-1);
        } else
            return false;
        return false;
    });

});

function ReloadTabs() { 
   
    //Create dialog
    var objDialog = $('<div></div>')
        .html('<p>'+messageCommon.onLoading+'</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.infoTitle,
            show: 'clip',
            hide: 'clip',
            width: 1000
        });
    
    //Load Async tab2 
    objDialog.html('<p>'+messageCommon.onLoading+'</p>');

     $.get('/GCS/Contract/RightsAndRestrictions', function (data) {
        $('#divRightsTab').html(data);
    })
    .error(function () {
        objDialog.html('<p>'+messageCommon.error+'</p>');
        objDialog.dialog('option', { title:messageCommon.warningTitle});
        //Show Dialog
        objDialog.dialog('open');
    });

    //Load Async tab3
    $.get('/GCS/Contract/SecondaryExploitation', function (data) {
        $('#divSecTab').html(data);
    })
    .error(function () {
        objDialog.html('<p>'+messageCommon.error+'</p>');
        objDialog.dialog('option', { title: messageCommon.warningTitle});
        //Show Dialog
        objDialog.dialog('open');
    });
       
}

function ReloadRightsTabs() {
 
    //Create dialog
    var objDialog = $('<div></div>')
        .html('<p>'+messageCommon.onLoading+'</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.infoTitle,
            show: 'clip',
            hide: 'clip',
            width: 1000
        });

    //Load Async tab2 
    objDialog.html('<p>'+messageCommon.onLoading+'</p>');

    $.get('/GCS/Contract/RightsAndRestrictions', function (data) {
        $('#divRightsTab').html(data);
    })
    .error(function () {
        objDialog.html('<p>'+messageCommon.error+'</p>');
        objDialog.dialog('option', { title:messageCommon.warningTitle });
        //Show Dialog
        objDialog.dialog('open');
    });
}


function ReloadSecondaryTabs() {

    //Create dialog
    var objDialog = $('<div></div>')
        .html('<p>'+messageCommon.onLoading+'</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.infoTitle,
            show: 'clip',
            hide: 'clip',
            width: 1000
        });


    //Load Async tab3
    $.get('/GCS/Contract/SecondaryExploitation', function(data) {
        $('#divSecTab').html(data);
    })
        .error(function() {
            objDialog.html('<p>'+messageCommon.error+'</p>');
            objDialog.dialog('option', { title: messageCommon.warningTitle });
            //Show Dialog
            objDialog.dialog('open');
        });
    }

function resizeContract ()
{
    var h1 = $(window).height();
    $(".scrollContract").css('height', h1 - 205);
}