var selectedGridItems = []; //Used for Unlink and Change Link

//SearchContractPopup : For ChangeLink OR OrdinarySearch.
var searchContractPopupForChangeOrUnlink = false;
var isChangeLink = false;
var isUnLink = false;

var objDialogForChangeLinkConfirmation = $('<div></div>')
    .html('<p>' + messageCommon.onLoading + '</p>')
    .dialog({
        autoOpen: false,
        modal: true,
        title: messageCommon.alertTitle,
        show: 'clip',
        hide: 'clip',
        width: 300,
        close: function () { $(this).remove(); }
    });

var searchParams, prevReleasIndex, releaseTabResult, releaseMessagess, releaseRightsTabIndex, releaseindex, tabIndex, upc, artist, isExactSearch, releasetitle, adminCompany, contractIds, fromDt, toDt, noRlsDt, queryType;
$(document).ready(function () {
    var newParamQuery = paramQuery;
    if (newParamQuery != undefined)
        $('#searchParamQuery').html(newParamQuery);


    releaseMessagess = window.rightsReviewWorkQueueMessages;

    //Handled the BradCrumbs Path According to this page
    $('#Home').html('<a href="/GCS/Home/Index">' + releaseMessagess.rightsAdministration + '</a>');

    var newPageId = $.tab.totalTabCounter - 1;

    if (newParamQuery != undefined)
        $("#" + "searchParamQuery" + newPageId).html(newParamQuery);

    $(".divReleaseRightsPriorityWorkQTab").tabs
    ({
        select: function (event, ui) {
            HideWarningSuccess();            
            var pageId;
            prevReleasIndex = releaseindex;
            releaseindex = ui.index;
            var gridId = $('#hdnGridId').val();
            if (window.getCustomWQCheck) {
                pageId = getCustomWQCheck.substring(getCustomWQCheck.length - 1, getCustomWQCheck.length);
            }
            if ($find(gridId)._edit._isEdit == true) {//checks is there is any edited values -to save
                releaseTabResult = confirm("Your changes will be lost! Are you sure you want to continue?");
            } else {
                releaseTabResult = true;
            }
            if (releaseTabResult == true) {
                if (pageId == "" || pageId == undefined) {
                    pageId = gridId.replace(/[^0-9]/g, '')
                }
                if (pageId == "")
                    pageId = "1";
                var h1 = $(window).height();
                $(".divReleaseRightsWorkQueue").css('height', h1 - 150);
                switch (ui.index) {
                    case 0:
                        // first tab selected 
                        releaseRightsTabIndex = 1;
                        //if the grid has already populated then refresh else create
                        if ($('#releaseRightsAcquireGrid' + pageId).html() != null && $('#releaseRightsAcquireGrid' + pageId).html().trim() != "") {
                            getFilteredReleaseGridData(pageId);
                        } else {
                            $.get('/GCS/RightsReviewWorkQueue/ReleaseRightsAcquired?pageId=' + pageId, function (data) {
                                $('#workQueueTabs-' + pageId).find('#divReleaseRightsAcquiredTab').html(data);
                            });
                        }
                        break;
                    case 1:
                        // second tab selected 
                        releaseRightsTabIndex = 2;
                        //if the grid has already populated then refresh else create
                        if ($('#releaseDigitalRestrictionGrid' + pageId).html() != null && $('#' + 'releaseDigitalRestrictionGrid' + pageId).html().trim() != "") {
                            getFilteredReleaseGridData(pageId);
                        } else {
                            $.get('/GCS/RightsReviewWorkQueue/ReleaseDigitalRestrictions?pageId=' + pageId, function (data) {
                                if ($('#workQueueTabs-' + pageId).length > 0)
                                    $('#workQueueTabs-' + pageId).find('#divReleaseDigitalRestrictionsTab').html(data);
                                else {
                                    $('#divReleaseDigitalRestrictionsTab').html(data);
                                }

                            });
                        }
                        break;
                }
            }
            else {
                return false;
            }
        }

    });

    /*To add the blue class on page load for first tab*/
    $('.divReleaseRightsPriorityWorkQTab .colorTabs ul li a').each(function () {
        $(this).parent().addClass('blueClassAcquired');
        $('.divReleaseRightsPriorityWorkQTab .colorTabs ul li a').eq($('.divReleaseRightsPriorityWorkQTab .colorTabs ul li a').length - 2).addClass('blueClassAcquiredLink');
        //$(this).addClass('blueClassAcquiredLink');
    });


    /*To Dynamically Add color to the tab header*/
    $('.divReleaseRightsPriorityWorkQTab .colorTabs ul li a').click(function () {
        var tab = $(this).html();
        if (!releaseTabResult) {
            return false;
        }
        else {
            switch (tab.toLowerCase()) {
                case "rights acquired ":
                    $(this).parents('ul').find('li').each(function () {
                        $(this).removeClass('blueClassAcquired');
                        $(this).removeClass('yellowDigitalRestriction');
                    });
                    $(this).parent().addClass('blueClassAcquired');

                    $(this).parents('ul').find('a').each(function () {
                        $(this).removeClass('yellowDigitalRestrictionLink');
                        $(this).removeClass('greenSecondaryExploitationLink');

                    });
                    $(this).addClass('blueClassAcquiredLink');
                    $("div.colorTabs").css("borderBottom", '3px solid Blue');
                    break;
                case "digital restriction ":
                    $(this).parents('ul').find('li').each(function () {
                        $(this).removeClass('blueClassAcquired');
                        $(this).removeClass('yellowDigitalRestriction');

                    });
                    $(this).parent().addClass('yellowDigitalRestriction');
                    $(this).parents('ul').find('a').each(function () {
                        $(this).removeClass('violetPreClearanceLink');
                        $(this).removeClass('greenSecondaryExploitationLink');
                        $(this).removeClass('blueClassAcquiredLink');

                    });
                    $(this).addClass('yellowDigitalRestrictionLink');
                    break;
            }
        }
        return false;
    });
});



//clearance admin company
var target = $("#AdminCompany");
target.autocomplete({
    source: target.attr("data-autocomplete-source-manual"),
    minLength: 2,
    select: function (event, ui) {
        $("#AdminCompany").val(ui.item.value);
    },
    response: function (event, ui) {
        var autocomplete = target.data("autocomplete");
        if (ui.content.length == 1) {
            autocomplete.selectedItem = ui.content[0];
        }
        if (autocomplete.selectedItem) {
            setTimeout(function () {
                autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                target.autocomplete('close');
                $("#AdminCompany").focus();
            }, 200);
        }
    }
});


/*Get Filter Data for Release Grid*/
function getFilteredReleaseGridData(pageId) {
    switch (releaseRightsTabIndex) {
        case 1:
            $find('releaseRightsAcquireGrid' + pageId).sendRefreshRequest();
            break;
        case 2:
            $find('releaseDigitalRestrictionGrid' + pageId).sendRefreshRequest();
            break;
    }
}

function getValue(idValue) {
    if (idValue == '' || idValue == undefined) { return null; }
    return idValue;
};
var linkType = '';
function getKeyValue(resourceIdValue, releaseIdValue) {
    if (releaseIdValue != '' && releaseIdValue != undefined && releaseIdValue != null) {
        linkType = 'Release';
        return releaseIdValue;
    }
    else if (resourceIdValue != '' && resourceIdValue != undefined && resourceIdValue != null) {
        linkType = 'Resource';
        return resourceIdValue;
    }
    else {
        linkType = '';
        return '';
    }
};
function getR2KeyValue(resourceIdValue, releaseIdValue) {
    if (releaseIdValue != '' && releaseIdValue != undefined && releaseIdValue != null) {
        return releaseIdValue;
    }
    else if (resourceIdValue != '' && resourceIdValue != undefined && resourceIdValue != null) {
        return resourceIdValue;
    }
    else {
        return '';
    }
};