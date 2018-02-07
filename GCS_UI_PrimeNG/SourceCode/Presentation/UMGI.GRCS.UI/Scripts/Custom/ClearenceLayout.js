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
    chooseTemplate: 'Choose Template',
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

    $('#loadingDivPA').hide();

    $('#loadingDiv').hide()
    // hide it initially
    .ajaxStart(function () { $(this).show(); })
    .ajaxStop(function () { $(this).hide(); });
});

//
function reSize() {
    var h = $(window).height();
    $(".scroll").css('height', h - 145);
    $(".scrollContract").css('height', h - 245);
}

function UnlockProject() {
    $.ajax({
        url: '/GCS/ClearanceProject/UnlockProjectWhenSesionExpire/',
        type: 'POST',
        async: true,
        success: function (data) {
            return true;
        }
    });
};

$(function () {//Similar to document ready
    //Datepicker functionality
    $(".datefield").css("width", "100px");
    $(".datefield").datepicker({
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
    $("#gcsSessionTimeoutDialog").dialog({
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
                UnlockProject();
                $.idleTimeout.options.onTimeout.call(this); window.location = "/GCS/SessionTimeout.html";
            }
        }
    });

    // cache a reference to the countdown element so we don't have to query the DOM for it on each ping.
    var $countdown = $("#dialog-countdown");

    // start the idle timer plugin
    $.idleTimeout('#gcsSessionTimeoutDialog', 'div.ui-dialog-buttonpane button:first', {
        idleAfter: 7150,
        pollingInterval: 7190,
        keepAliveURL: '/GCS/Home/RefreshSession',
        serverResponseEquals: 'OK',
        onTimeout: function () {
            UnlockProject();
            window.location = "/GCS/SessionTimeout.html";
        },
        onIdle: function () {
            $(this).dialog("open");
        },
        onCountdown: function (counter) {
            $countdown.html(counter); // update the counter
        }
    });

    /* Mimic User - Starts */
    $("#lnkMimicUser").click(function () {
        // setup the dialog
        var userName = "";
        var isMimicedUser = "";
        var objMimicUserDialog = $("#mimicUserDialog").dialog({
            autoOpen: true,
            modal: true,
            width: "85%",
            position: [(($(window).width() - (($(window).width() * 85) / 100)) / 2), 50],
            height: "auto",
            title: "Mimic User",
            closeOnEscape: false,
            show: 'clip',
            hide: 'clip',
            resizable: false,
            beforeClose: function (event, ui) {
                $("#mimicUserErrorMessage").hide();
                $("#mimicUserErrorMessage").html("");
                jQuery("#txtMimicUserName").watermark(watermarkUserName);
                jQuery("#txtMimicUserId").watermark(watermarkUserId);
                jQuery("#txtMimicUserCountry").watermark(watermarkCountry);
            }
        });
        objMimicUserDialog.load("/GCS/Workgroup/MimicUserPopup");
    });
    $("#lnkMimicUserExit").click(function () {
        var userName = "";
        var isMimicedUser = "";
        isMimicedUser = false;
        var exitMimicUserLoadingPanel = $($.find('#loadingDv'));
        $.ajax({
            url: '/GCS/Workgroup/MimicUserInfo/',
            type: 'POST',
            dataType: 'json',
            async: true,
            data: { userName: userName, isMimic: isMimicedUser },
            beforeSend: function () {
                exitMimicUserLoadingPanel.show();
            },
            success: function (data) {
                if (data.Result == '') {
                    window.location.href = "/GCS/Workgroup/";
                } else {
                    window.location.href = "/GCS/SessionTimeout.html";
                }
                exitMimicUserLoadingPanel.hide();
            }
        });
    });
    /* Mimic User - Ends */
});

function displayDialog(title, value) {
    if (value == null || value == '')
        return;

    var newDialog = $('<div class="warning1">\<p>' + value + '</p>\</div>');
    newDialog.dialog({
        modal: true,
        title: title,
        show: 'clip',
        hide: 'clip',
        width: 300,
        buttons: { 'Ok': function () { $(this).dialog('close'); } }
    });
}

//Finds the index of an item in an arrays
function IndexOf(item) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == item) {
            return i;
        }
    }
    return -1;
}

/* Code By Mohit */
$(document).ready(function () {
    $("#nav_up").click(function () {
        $("#main").animate({ scrollTop: '0' }, 800);;
    });

    $("#nav_down").click(function () {
        $("#main").animate({ scrollTop: $('#main')[0].scrollHeight }, 1500);
    });

    $('#jsddm > li').mouseover(function () {
        $(this).addClass('active');
    }).mouseout(function () {
        $('#jsddm > li').removeClass('active');
    });
});

$.fn.extend({
    flyout: function () {
        var elementId = $(this)[0].id; //Elements Id/class
        var selector = $(this).selector; //gets selector
        var titleText;
        if (selector == "") {
            titleText = $(this)[0].tooltipText;
            $(this)[0].tooltipText = "";
        } else {
            titleText = $(selector).attr('tooltipText'); //Gets tooltiptext if there
            $(selector).attr('tooltipText', ''); //Removing tooltip text;
            if (titleText == "" || titleText == undefined) {
                titleText = $(selector).attr('title');
            }
            $(selector).attr('title', ''); //Removes the title text
        }
        var height = (titleText.length / 38) * 10;
        var width = 300; // 150;
        if (elementId == "") {
            elementId = "empty" + Math.floor(Math.random() * 999) + 1;
        }
        while ($('#' + elementId + "_flyOut").length > 0) {
            elementId = elementId + Math.floor(Math.random() * 999) + 1;
        }
        var flyOutDiv = "<div id='" + elementId + "_flyOut' style='z-index: 1102; padding:10px;display:none;background-color:#ff00ff; border: 1px solid #fccf82;background-color: #ffffc3; opacity:1;top:" + $(this).position().top + "px;left:" + $(this).position().left + "px;position:absolute;height:auto;width:" + width + "px;'><span  style='float:right;text-decoration:underline;margin: 0px 5px 0px 0px;position:relative;font-weight:bold;height:auto;width:100%;'><span class='flyoutContent' style='clear:both;padding:3px;text-align: start;word-wrap:break-word;float:left;width:100%;cursor: text;'>" + titleText + "</span><span id='" + elementId + "_close' class='closeFlyOut' style='float:right;width:3em;height:1em;top:-20px;position:absolute;padding:2px;cursor:pointer;'></span></span></div>";

        $(this).parents('body').append(flyOutDiv);

        $(this).mouseover(function () {//TO Show the div when mouse enters
            var top = $(this).offset().top;
            $('#' + elementId + '_flyOut').attr('inFocus', false); //in flyout focus attr will be true
            $('#' + elementId + '_flyOut').attr('inParentFocus', true); //in parent element focus attr will be true
            var flyoutHeight = $('#' + elementId + '_flyOut').height();
            if ($(window).height() - top < flyoutHeight) {//TO handle if the position of the parent is near end of page
                top = top - (flyoutHeight - ($(window).height() - top));
            }
            if (top < flyoutHeight) {
                top = flyoutHeight + 100;
            }
            $('#' + elementId + '_flyOut').css('top', top);
            $('#' + elementId + '_flyOut').css('left', $(this).offset().left + $(this).outerWidth());

            if (($(window).width() - ($(this).offset().left + $(this).width())) < width) {//TO handle if the position of the parent is near end of page
                $('#' + elementId + '_flyOut').css('left', $(this).offset().left - width - 15);
            }
            if (!$('#' + elementId + '_flyOut').is(':visible') && !$('[id*=_flyOut]').is(':visible')) {
                $('#' + elementId + '_flyOut').show();
            }
            mouseHandlers(elementId + '_flyOut');
        });

        $('#' + elementId + '_flyOut').click(function () {//To over ride Close of the flyout
            return false;
        });

        $('#' + elementId + '_flyOut').mouseover(function () {//To over ride Close of the flyout
            $('#' + elementId + '_flyOut').attr('inFocus', true);
        });

        $('#' + elementId + '_close').click(function () {//To handle Close of flyout
            $('#' + elementId + '_flyOut').hide();
        });

        $(document).keypress(function (e) {//To close the flyout on esc click function
            if ($('#' + elementId + '_flyOut').is(':visible') && e.keyCode == 27) {
                $('#' + elementId + '_flyOut').hide();
            }
        });

        $(document).scroll(function (e) {//To close the flyout on esc click function
            if ($('#' + elementId + '_flyOut').is(':visible') && !$('#' + elementId + '_flyOut').is(':focus')) {
                $('#' + elementId + '_flyOut').hide();
            }
        });

        $('#' + elementId + '_flyOut').mouseleave(function () {//To handle the close event in mouse leave to close the flyout
            setTimeout(function () {
                if ($('#' + elementId + '_flyOut').is(':visible') && $('#' + elementId + '_flyOut').attr('inParentFocus') == "false") {
                    $('#' + elementId + '_flyOut').hide();
                    $('#' + elementId + '_flyOut').attr('inParentFocus', false);
                    $('#' + elementId + '_flyOut').attr('infocus', false);
                }
            }, 500);
        });

        $(document).ready(function () {
            $(this).mouseout(function () {
                $('#' + elementId + '_flyOut').hide();
            });
        });
    }
});

function mouseHandlers(flyoutId) {//To close the flyout on document click function
    $(document).click(function () {
        if ($('#' + flyoutId).attr('infocus') == "false") {
            $('#' + flyoutId).hide();
        }
    });
}

//Jtable cross page selection
var jGridsList = [];
var isGridLoad = false;

//Update Selected Records in grid
function updateSelectedRecordsInGrid(grid, selectedRecords, idColumnName) {
    var allRows = grid.find("tr.jtable-row-odd, tr.jtable-row-even");
    if (allRows.length > 0) { //Event : Records unselection
        $(allRows).each(function () {
            var record = $(this).data('record');
            var listContainerForComparision = JSLINQ(selectedRecords).
                Where(function (item) {
                    return item[idColumnName] == record[idColumnName];
                });
            if (listContainerForComparision.items.length > 0)//The record is unselected
            {
                for (var i = 0; i < selectedRecords.length; i++) {
                    if (selectedRecords[i][idColumnName] == record[idColumnName])
                        selectedRecords.splice(i, 1);
                }
            }
        });

        var selectedRows = grid.jtable('selectedRows'); //Event : Records selection
        if (selectedRows.length > 0) {
            selectedRows.each(function () {
                var record = $(this).data('record');
                var listContainerForComparision = JSLINQ(selectedRecords).
                    Where(function (item) { return item[idColumnName] == record[idColumnName]; });
                if (listContainerForComparision.items.length == 0) { //The new record selected is pushed
                    selectedRecords.push(record);
                }
            });
        }
    }
}

//Display previously selected records in page
function displaySelectedRecordsInGrid(grid, selectedRecords, idColumnName) {
    if (selectedRecords.length > 0) {
        var allRows = grid.find("tr.jtable-row-odd, tr.jtable-row-even");
        for (var i = 0; i < selectedRecords.length; i++) {
            var tdid = selectedRecords[i][idColumnName];
            $(allRows).each(function () {
                if ($(this).data('record')[idColumnName] == tdid) {
                    $(this).find('input').attr("checked", "checked");
                    $(this).addClass("jtable-row-selected");
                }
            });
        }
    }
}

function getJtableRecords(selectedRecords) {
    var returnValue = [];
    selectedRecords.each(function () {
        var record = $(this).data('record');
        returnValue.push(record);
    });
    return returnValue;
}

//Method to convert string date to "dd M yy" format
function getGenericDateString(value) {
    try {
        var result = $.datepicker.parseDate($.datepicker.regional[''].dateFormat, value);
        return getDateString(result);
    } catch (e) {
        return '';
    }
}
//Method to convert datetime to "dd M yy" format string
function getDateString(item) {
    var dateString;
    try {
        var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
        dateString = item.getDate().toString() + ' ' + monthArray[item.getMonth("mmm")] + ' ' + item.getFullYear().toString();
    } catch (e) {
        dateString = "NA";
    }
    return dateString;
}