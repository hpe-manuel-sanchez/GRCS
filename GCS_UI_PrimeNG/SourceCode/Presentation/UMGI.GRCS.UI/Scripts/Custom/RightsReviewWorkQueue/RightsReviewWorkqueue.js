
var customTab = '';
var chkBoxResource = false, chkBoxTrack = false, chkBoxImage = false, chkBoxAudio = false, chkBoxVideo = false, chkBoxNewForReview = false, chkBoxFinalForReview = false,
    chkBoxSampleExists = false, chkBoxSideArtistExists = false, chkBoxDownload = false, chkBoxStreaming = false, chkBoxAll = false, chkBoxAlaCarte = false, chkBoxSubscription = false,
    chkBoxAdFunded = false, chkBoxNoRights = false, chkBoxConsentRequired = false, chkBoxReferToLegal = false, chkBoxNoRestriction = false, chkBoxConsult = false, chkBoxDuringandAfterTerm = false,
    chkBoxDuringHoldbackPeriod = false, chkBoxDuringTerm = false, chkBoxArtistExact = false, clearanceCountryId = 0, drpDwnReviewReason,
    textArtistName, clearanceAdminCompany, textTitle, textLinkedContract, textIsrc, textUpc, reasonForReview, filterParams;
var searchCritaria = '';
var tabNumber = '';
var paramQuery = "";
var getCurid = "";
var getVisDiv = "";
var getCustomWQCheck = "";
var rightsReviewWorkQueueMessagess;
/*Variable Initialization for RightsReview Pages*/


//Initialized Variable for Messages from Resource file


$(document).ready(function () {
    $('.gr_breadCrumbNav').html("Rights Administration > > Rights Review Work Queue");

    $("head").append("<link>");
    var css = $("head").children(":last");
    css.attr({
        rel: "stylesheet",
        type: "text/css",
        href: "../Content/SubPages/CustomSetting.css"
    });

//    $('#loadingDiv').ajaxStart(function () {
//        $("[id*='waiting_WaitingPopup']").hide();
//        if (!$(".ui-autocomplete-loading").is(':visible') && !$(".ui-jtable-loading").is(':visible')) {
//            $(this).show();
//        }
//        if (!$('.ui-widget-overlay').is(':visible')) {
//            $('#loaderPanel').css({ 'height': $(window).height(), 'width': $(window).width() });
//            $('#loaderPanel').show();
//        }
//    }).ajaxStop(function () {
//        $(this).hide();
//    });

    resizeTextSize();
    rightsReviewWorkQueueMessagess = window.rightsReviewWorkQueueMessages;

    //Handled the BradCrumbs Path According to this page
    $('#Home').html('<a href="/GCS/Home/Index">' + rightsReviewWorkQueueMessagess.rightsAdministration + '</a>');

    $("#divRightsPriorityWorkQueueTab").tabs();
    $("#divRightsPriorityWorkQueueTab").show();

    //Initializing the Tab 
    $("#divRightsPriorityWorkQueueTab").tabs({
        select: function (event, ui) {
            if (event.currentTarget != undefined) {
                getCustomWQCheck = event.currentTarget.getAttribute("href");
            }
            if ($.browser.msie) {
                var getCurid = '';
                if (event.currentTarget != undefined) {
                    getCurid = event.currentTarget.getAttribute("href");
                }
                if (getCurid != '') {
                    var getVisDiv = getCurid + " #screenTabs";
                    if ($(getVisDiv).children().length > 0) {
                        var selectedTabCntrl = $(getVisDiv).find('li[aria-selected*="true"]');
                        var tabcontrolid = selectedTabCntrl.attr("aria-controls");
                        var intgridid = getCurid.split('-')[1];
                        var gridName;
                        if ($(getVisDiv).children().length == 2) {
                            if (tabcontrolid == "divReleaseRightsAcquiredTab")
                                gridName = GetTabSelectionGrid(5) + intgridid;
                            else if (tabcontrolid == "divReleaseDigitalRestrictionsTab")
                                gridName = GetTabSelectionGrid(6) + intgridid;
                        }
                        if ($(getVisDiv).children().length == 4) {
                            var tabuseCase = ui.tab.innerText;
                            var customWqrTflag = false;
                            if (tabuseCase == "Custom WQ-R&T")
                                customWqrTflag = true;
                            if (tabcontrolid == "divRightsAcquiredTab" || tabcontrolid == "divResourceRightsAcquiredTab") {//Resource
                                if (customWqrTflag == true)
                                    gridName = GetTabSelectionGrid(1) + intgridid;
                                else
                                    gridName = GetTabSelectionGrid(1);
                            } else if (tabcontrolid == "divDigitalRestrictionsTab" || tabcontrolid == "divResourceDigitalRestrictionsTab") {
                                if (customWqrTflag == true)
                                    gridName = GetTabSelectionGrid(2) + intgridid;
                                else
                                    gridName = GetTabSelectionGrid(2);
                            } else if (tabcontrolid == "divSecondaryExploitationTab" || tabcontrolid == "divResourceSecondaryExploitationTab") {
                                if (customWqrTflag == true)
                                    gridName = GetTabSelectionGrid(3) + intgridid;
                                else
                                    gridName = GetTabSelectionGrid(3);
                            } else {
                                if (customWqrTflag == true)
                                    gridName = GetTabSelectionGrid(4) + intgridid;
                                else
                                    gridName = GetTabSelectionGrid(4);
                            }
                        }

                        if (gridName.indexOf('rightsAcquired') == -1) {
                            $(".blueClassAcquiredLink").removeClass("blueClassAcquiredLink"); //
                            $(".blueClassAcquired").removeClass("blueClassAcquired");
                        }
                        setReviewTabIndex(gridName);
                        $("#hiddenTabSelectionId").val(gridName);
                        $('#hdnGridId').val(gridName);
                        //EnableSyncScrollBar();
                        setTimeout('EnableSyncScrollBar()', 2000);
                    }
                }
            }
        }
        // }
    });


    /*click Functionality to open custom setting popup for resource and tracks*/
    $('#resourcesSetting').click(function () {
        if (($.tab.tabCounter <= 8)) {
            var objRightsCustomSettingDialog = $('<div id="contractSearchPopup"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: rightsReviewWorkQueueMessagess.customSetting,
                    show: 'clip',
                    hide: 'clip',
                    width: 750,
                    height: 500,
                    //  width: getPopupWidth(55, 575),
                    // height: getPopupHeight(90, 440, 40),
                    // minHeight: 440,
                    // minWidth: 750,
                    //position: [getXPosition(45, 0), getYPosition(90, 40)],
                    resizable: false,
                    close: function () {
                        $(this).remove();
                    }
                });
            $.post('/GCS/RightsReviewWorkQueue/CreateCustomSetting/?referrer=Resource', function (data) {
                objRightsCustomSettingDialog.html(data);
                objRightsCustomSettingDialog.dialog('open');
            }).error(function () {
                objRightsCustomSettingDialog.html('<p>' + messageCommon.error + '</p>');
            });
        }
        else {
            ShowWarning(rightsReviewWorkQueueMessagess.tabLimit);
        }
    });

    /*click Functionality to open custom setting popup for Release*/
    $('#releasesSetting').click(function () {
        if (($.tab.tabCounter <= 8)) {
            var objReleaseRightsCustomSettingDialog = $('<div id="contractSearchPopup"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: rightsReviewWorkQueueMessagess.customSetting,
                    show: 'clip',
                    hide: 'clip',
                    width: 750,
                    height: 500,
                    //minHeight: 440,
                    //minWidth: 750,
                    //position: [getXPosition(45, 0), getYPosition(90, 40)],
                    resizable: false,
                    close: function () {
                        $(this).remove();
                    }
                });
            $.post('/GCS/RightsReviewWorkQueue/CreateCustomSetting/?referrer=Release', function (data) {
                objReleaseRightsCustomSettingDialog.html(data);
                objReleaseRightsCustomSettingDialog.dialog('open');
            }).error(function () {
                objReleaseRightsCustomSettingDialog.html('<p>' + messageCommon.error + '</p>');
            });
        }
        else {
            ShowWarning(rightsReviewWorkQueueMessagess.tabLimit);
        }
    });




});






function EnableSyncScrollBar() {
    var gridid = $("#hiddenTabSelectionId").val();
    var sfHorizontal = "#" + gridid + " " + ".sf-sp-hscroller, #" + gridid + " " + ".sf-sp-vscroller";
    //var gg = $("#" + gridid).is(":visible");
    $(sfHorizontal).css("display", "block");
    //    $(sfHorizontal).show();
    var pagerHgt = $("#" + gridid + " .GridPager").height();
    var headerHgt = $("#" + gridid + " .GridHeader").height();
    var browsHgt = 0;
    if ($.browser.msie)
        browsHgt = 20;
    else
        browsHgt = 15;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;
    var sTable = "#" + gridid;
    var jtableTop = $("" + sTable + "").offset().top;
    var topfootPos = $(".footer").offset().top;
    var totRecHeight = $("#" + gridid + "_Table").height() + reduceHgt;
    var tableHeight = topfootPos - jtableTop;
    var gridObj = $find(gridid);
    if (totRecHeight >= tableHeight)
        setScrollBarHeight(gridObj, tableHeight - reduceHgt);
    else
        setScrollBarHeight(gridObj, totRecHeight - reduceHgt + 20);
}

function setScrollBarHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}

function GetTabSelectionGrid(id) {
    switch (id) {
        case 1:
            return 'rightsAcquired';
        case 2:
            return 'digitalGrid';
        case 3:
            return 'secondaryExp';
        case 4:
            return 'preClearanceGrid';
        case 5:
            return 'releaseRightsAcquireGrid';
        case 6:
            return 'releaseDigitalRestrictionGrid';
    }
}
function resizeTextSize() {

    $("#txtIsrc").css('width', '155px');
    $("#txtUpc").css('width', '155px');
    var wid1 = $('#txtArtistName').width();
    var wid2 = $("#txtTitles").width();
    var wid3 = $("#RightsMasterData_ReviewReason_Values").width();
    $("#txtArtistName").css('width', wid2 - 95);
    $("#txtClearanceAdminCompany").css('width', wid3);
    $(".upcContainer").css("margin-left", wid2 - 305);
    $("#txtAreaLinkedContract").css("width", wid3 - 20);
    //$("#divReviewFilterButtons").css("margin-right", wid3 - 312);
}



$(window).resize(function () {
    resizeTextSize();
});




/*Returns the contractIds to unlink*/
//function getContractIdsToUnlink(gridId) {
//    var unlinkDetails = "";
//    var rowsSelected = $('#' + gridId + '_Table').find('tr input:checked').parents('tr');
//    if ($(rowsSelected).length > 1) {
//        ShowWarning("Cannot Unlink more than one Repertoire");
//        return false;
//    }
//    $(rowsSelected).each(function () {
//        var contractId = "";
//        var resourceId = "";
//        var tdLength = $(this).find('td').length;
//        contractId = $($(this).find('td')[tdLength - 2]).html(); //ContractID position 
//        resourceId = $($(this).find('td')[tdLength - 1]).html(); //ResourceId position 
//        unlinkDetails = contractId + "," + resourceId;
//    });
//    return unlinkDetails;
//}

function validateRepertoireToUnlink(unlinkDetails) {
    var isValid = true;
    if (unlinkDetails == "" || unlinkDetails.split(',')[0] == "") {
        if (unlinkDetails.split(',')[0] == "") {
            ShowWarning(rightsReviewWorkQueueMessagess.resourceUnlink);
            isValid = false;
            return false;
        }
        else {
            ShowWarning(rightsReviewWorkQueueMessagess.resourceUnlink);
            isValid = false;
            return false;
        }
    }
    return isValid;
}


function setReviewTabIndex(gridName) {
    if (gridName.indexOf('rights') != -1) {
        reviewTabIndex = 1;
    }
    if (gridName.indexOf('digital') != -1) {
        reviewTabIndex = 2;
    }
    if (gridName.indexOf('secondary') != -1) {
        reviewTabIndex = 3;
    }
    if (gridName.indexOf('pre') != -1) {
        reviewTabIndex = 4;
    }
}