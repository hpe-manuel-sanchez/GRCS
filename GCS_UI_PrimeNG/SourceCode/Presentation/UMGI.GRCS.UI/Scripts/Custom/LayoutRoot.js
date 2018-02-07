var messageCommon = {
    onLoading: "Loading....",
    warningTitle: "Warning",
    infoTitle: "Info",
    error: "Error in loading...",
    searchValid: "Please specify at least one search criteria",
    searchArtistTitle: "Search for Artist",
    cntrctDesc: 'Contract Description',
    artistName: 'Artist Name',
    cntrctParty: 'Contracting Party',
    cmncmentDate: 'Contract Commencement Date',
    adminCmpny: 'Admin Company',
    umgSignCmpny: 'Signing Company',
    localCntrctNo: 'Local ContractRef Number',
    rightsType: 'Rights Type',
    cntrctStatus: 'Contract Status',
    workflowStatus: 'WorkFlow Status',
    cntrctId: 'Contract Id',
    viewValid: 'No row selected! Select row to view Contract',
    editValid: "Please select a contract to edit.",
    editAuthorization: "You are not authorized to edit this Contract",
    digitalConsentPrd: "Please select Digital Restriction Consent period",
    digitalIncomplete: "Digital Restriction grid is incomplete",
    digitalUnique: "Digital Restriction is not unique",
    removeDigital: 'No row selected! Select row to remove Digital Restriction',
    digitalCombination: "Please select valid Digital Restriction combination",
    chooseTemplate: 'Create New Contract From Template',
    deleteFailure: 'Failed to delete Contract',
    deleteSuccess: 'Contract Deleted successfully'
};

//Autocomplete functionality
$(document).ready(function () {
    //Autocomplete
    $("input[data-autocomplete-source]").each(function () {
        var target = $(this);
        target.autocomplete({ source: target.attr("data-autocomplete-source"), minLength: 2 });
    });

    $.ajaxSetup({ cache: false });

    reSize();

    $(window).resize(function () {
        reSize();
    });

    Array.prototype.indexOf = IndexOf;

    //Set textbox max length
    setTextBoxMaxLength();
    $("textarea").each(limitTextArea);

    //Hide loading div
    $(".ui-jtable-loading").hide();

    $('#loadingDiv').hide()
    // hide it initially
    .ajaxStart(function () {
        if ($(".ui-autocomplete-loading").length == 0 && $(".ui-jtable-loading").length == 0)
            $(this).show();
    })
    .ajaxStop(function () {
        $(this).hide();
        setTextBoxMaxLength();
        $("textarea").each(limitTextArea);
    });
});

//
function reSize() {
    var h = $(window).height();
    $(".scroll").css('height', h - 145);
    $(".scrollContract").css('height', h - 205);
}

$(function () {//Similar to document ready
    //Datepicker functionality
    $(".datefield")
        .css("width", "100px")
        .datepicker({
            showOn: 'both',
            buttonImage: "/GCS/Images/Calender_Icon_img.png",
            buttonImageOnly: true,
            dateFormat: "dd M yy",
            changeMonth: true,
            changeYear: true,
            yearRange: '1900:2100'
        });

    //Roles dropdown changes
    $("#selectRoles").change(function () {
        window.location.href = "/GCS/Home/ChangeRole/" + $("#selectRoles").val();
    });

    // setup the dialog
    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        height: 200,
        closeOnEscape: false,
        draggable: false,
        resizable: false,
        buttons: {
            'Yes, Keep Working': function () {
                $(this).dialog('close');
            },
            'No, Logoff': function () {
                // fire whatever the configured onTimeout callback is.
                // using .call(this) keeps the default behavior of "this" being the warning
                // element (the dialog in this case) inside the callback.
                $.idleTimeout.options.onTimeout.call(this); window.location = "/GCS/SessionTimeout.html";
            }
        }
    });

    // cache a reference to the countdown element so we don't have to query the DOM for it on each ping.
    var $countdown = $("#dialog-countdown");

    // start the idle timer plugin
    $.idleTimeout('#dialog', 'div.ui-dialog-buttonpane button:first', {
        idleAfter: 1150, //43150 - For 12 hrs
        pollingInterval: 1190, //43190 - For 12 hrs
        keepAliveURL: '/GCS/Home/RefreshSession',
        serverResponseEquals: 'OK',
        onTimeout: function () {
            window.location = "/GCS/SessionTimeout.html";
        },
        onIdle: function () {
            $(this).dialog("open");
        },
        onCountdown: function (counter) {
            $countdown.html(counter); // update the counter
        }
    });
});

function displayDialog(title, value) {
    if (value == null || value == '')
        return;

    $('<div id="MenuDialog">\<p>' + value + '</p>\</div>').dialog({
        modal: true,
        title: title,
        show: 'clip',
        hide: 'clip',
        width: 300,
        buttons: {
            'Ok': function () {
                $(this).dialog('close');
            }
        }
    });
}

//Finds the index of an item in an arrays
function IndexOf(item) {
    for (var index = 0; index < this.length; index++) {
        if (this[index] == item) {
            return index;
        }
    }
    return -1;
}

function setToolTip(item) {
    $(item).find("td").each(function () {
        var len = $(this).text().length;
        if (len > 35) {
            var textString = $(this).text();
            if (textString.indexOf('') == 0)
                textString = textString.replace(/[;]/g, '; ');

            $(this).prop("title", textString);
            $(this).tooltip();
            $(this).text($(this).text().substr(0, 35) + '...');
        }
    });
}

function limitTextArea() {
    if ($(this).prop("id") == "EmailGroupDetails")
        return;
    $(this).keydown(function () {
        if ($(this).val().length > 255) {
            $(this).val($(this).val().substring(0, 255));
            return false;
        }
    });
}

function setTextBoxMaxLength() {
    $("input:text").each(function () {
        if ($(this).prop("maxlength") == 2147483647)
            $(this).prop("maxlength", 255);
    });
}

function ShowWarning(msg) {
    $('#success').hide();
    $('#Errorr').hide();
    $('#AlertMsg').empty();
    $('#AlertMsg').append(msg);
    $('#warning').show();
}

function ShowSuccess(msg) {
    $('#warning').hide();
    $('#Errorr').hide();
    $('#SuccessMsg').empty();
    $('#SuccessMsg').append(msg);
    $('#success').show();
}
function ShowError(msg) {
    $('#warning').hide();
    $('#success').hide();
    $('#ErrorMsg').empty();
    $('#ErrorMsg').append(msg);
    $('#Errorr').show();
}
function HideWarningSuccess() {
    $('#warning').hide();
    $('#success').hide();
    $('#Errorr').hide();
    $('#splitWarning').hide()
}

function formatDate(strngDate) {
    try {
        var arraydate = strngDate.split('/');
        var temp = arraydate[1];
        var month = temp.substring(0, 2);
        var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        var newDate = arraydate[0] + " " + months[month - 1] + " " + arraydate[2];
        return newDate;
    }
    catch (e) {
        return '';
    }
}