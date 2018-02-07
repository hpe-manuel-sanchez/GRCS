var rightsIds = "", paramQuery = undefined, getCustomWQCheck;
$.tab = { totalTabCounter: 2 };
var filterParams;
$(document).ready(function () {
    $('#Contract_Administration').html('Contract Administration');
    //$('.gr_breadCrumbNav').html("Rights Administration > > Rights Review Work Queue");
    var path = getUrlVars()["path"];
    if (path != null && path != '') {
        path = decodeURIComponent(path);
        var link = path.substring(path.lastIndexOf('&gt; &gt;') + 0);
        path = path.replace(link, '<a href="javascript:void(0);"  onclick="javascript:window.history.go(-1);return false;">' + link + '</a>') + " > > Review Rights";
        $('.gr_breadCrumbNav').html(path);
    }
    $('#divRightsPriorityWorkQueueTab').tabs();
    rightsIds = $('#spanListIds').html().toString();
    var searchCriteria = rightsIds.substr(1, rightsIds.length - 2);
    filterParams = { IsNavigatedWq: '1' };
    $('#spanHiddenSearchParams').html(searchCriteria);
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
});

/*Disables the given element's Editing*/
function disableEditing(element) {
    $(element).addClass('disabled');
    $(element).click(function () {
        return false;
    });
    return false;
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

function setScrollBarHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}

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