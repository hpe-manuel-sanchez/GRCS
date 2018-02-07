var selectedGridItems = []; //Used for Unlink and Change Link
var resourceParamQuery = '';
var rightsWorkQueueMessages;
//SearchContractPopup : For ChangeLink OR OrdinarySearch.
var searchContractPopupForChangeOrUnlink = false;
var SelGridContractID = '';
var isChangeLink = false;
var isUnLink = false; var tabCustomParams = [];
$.tab = {
    tabCounter: 2,
    totalTabCounter: 2,
    customSettings: []
};
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

//Variable Initialization
var reviewTabIndex = 1, rightsAcquiredGridId, digitalGridId, secondaryExploitationGridId, PreClearanceGridId;
var result, index = 0, prevIndex;
var rightsWorkQueueValues = {
    rightsAcquired: "rights acquired",
    digitalRestriction: "digital restriction",
    secondaryExploitation: "secondary exploitation",
    preClearance: "pre-clearance"
};
$(document).ready(function () {

    /*divRightsAcquiredTab*/
    $(".divRightsPriorityWorkQTab").tabs();

    $('#loadingDiv').ajaxStart(function () {
        if (!$(".ui-jtable-loading").is(':visible') && !$(".ui-autocomplete-loading").is(':visible')) {
            $("[id*='waiting_WaitingPopup']").hide();
            if (!$('.ui-widget-overlay').is(':visible')) {
                $('#loaderPanel').css({ 'height': $(window).height(), 'width': $(window).width() });
                $('#loaderPanel').show();
            }
        }
        else {
            //$(this).hide();
            $('#loaderPanel').hide();
        }
    }).ajaxStop(function () { $(this).hide(); });
    //pageScrollRightsWorkQueue();divScrollPriorityWorkQueue
    customMenu();
    if ($('#divCustomRightsReviewWorkQueueContainer').length > 0) {
        var windowHeight = $(window).height();
        var scrollHeight = windowHeight - 39 - 127;
        // $('#divCustomRightsReviewWorkQueueContainer').css('height', scrollHeight);

        //Scroll function handling on page resize
        $(window).resize(function () {
            windowHeight = $(window).height();
            scrollHeight = windowHeight - 39 - 127;
            if (scrollHeight > 700) {
                scrollHeight = 700;
            }
            // $('#divCustomRightsReviewWorkQueueContainer').css('height', scrollHeight);
        });
    } else {
        //        var windowHeighte = $(window).height();
        //        var scrollHeighte = windowHeighte - 39 - 127;
        //        $('#divScrollPriorityWorkQueue').css('height', scrollHeighte);

        //        //Scroll function handling on page resize
        //        $(window).resize(function () {
        //            windowHeighte = $(window).height();
        //            scrollHeighte = windowHeight - 39 - 127;
        //            if (scrollHeighte > 700) {
        //                scrollHeighte = 700;
        //            }
        //            $('#divScrollPriorityWorkQueue').css('height', scrollHeighte);
        //        });
    }

    $("#divRightsPriorityWorkQueueTab span.ui-icon-close").die('click').live('click', function () {
        var tabs = $("#divRightsPriorityWorkQueueTab").tabs();
        var panelId = $(this).closest("li").remove().attr("aria-controls");
        $("#" + panelId).remove();
        DeleteCustomSettingFromCache(panelId);
        $.tab.tabCounter = $.tab.tabCounter - 1;
        tabs.tabs("refresh");
        $('#CustomNewSettingLink').attr("disabled", false);
        HideWarningSuccess();
    });
    if ($('#divCustomRightsReviewWorkQueueContainer').length > 0) {
        /*To show loader on popup open*/
        $('#loadingDiv').ajaxStart(function () {
            if (!$(".ui-autocomplete-loading").is(':visible') && !$(".ui-jtable-loading").is(':visible')) {
                $(this).show();
            }
            $("[id*='waiting_WaitingPopup']").hide();
            if (!$('.ui-widget-overlay').is(':visible')) {
                $('#loaderPanel').css({ 'height': $(window).height(), 'width': $(window).width() });
                $('#loaderPanel').show();
            }
        }).ajaxStop(function () {
            $(this).hide();
        });
    }

    redirectToChangeLink();
    resizeAccordion();

    /* Save All Changes Action */
    $('#btnSaveAllReviewDataChanges').unbind().click(function () {
        var gridId = $('#hdnGridId').val();
        var grid = $find(gridId);
        if (checkEditPermission(gridId) == "True") {
            var updatedRecordsCount = grid._edit._updatedRecords.length;
            var deletedRecordsCount = [];
            if (grid._edit._deletedRecords != undefined && grid._edit._deletedRecords.length > 0) {
                var deletedRecords = JSLINQ(grid._edit._deletedRecords).Where(function (item) { return item.RestrictionIdLnr != 0; });
                deletedRecordsCount = deletedRecords.items.length;
            }

            var addedRecordsCount = grid._edit._addedRecords.length;

            if (grid._edit._isEdit == true && (updatedRecordsCount > 0 || deletedRecordsCount > 0 || addedRecordsCount > 0)) {
                $('#loadingDiv').show();
                setTimeout(function () {
                    if (validateSave(gridId)) {
                        grid.sendSaveRequest();
                    }
                }, 50);

            } else {
                ShowWarning(rightsWorkQueueMessages.recordSave);
                customMenu();
                //pageScrollRightsWorkQueue();
                rightsWorkQueueGridScroll(gridId);
            }
        }
        else {
            ShowWarning(rightsWorkQueueMessages.editPermission);
            customMenu();
            //pageScrollRightsWorkQueue();
            rightsWorkQueueGridScroll(gridId);
        }
    });

    /* Cancel All Changes Action */
    $('#btnCancelReviewDataChanges').click(function () {
        $('#loadingDiv').show();
        setTimeout(function () {
            var gridId = $('#hdnGridId').val();
            var grid = $find(gridId);
            grid.sendCancelRequest();
            $("#" + gridId + " .GridHeader .HeaderCell").find('input[type="checkbox"]').prop('checked', false); // added for checkbox unselection on cancel click
            HideWarningSuccess();
            customMenu();
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
        }, 50);
    });

    /*divRightsAcquiredTab*/
    $(".divRightsPriorityWorkQTab").tabs();
    $(".divRightsPriorityWorkQTab").show();


    /*Unlink Contract Button click function*///Dont Move From Here
    $('#btnReviewDataUnlinkContract').click(function () {
        var gridId = $('#hdnGridId').val();
        var repertoireType = "Resource";
        var unlinkDetails = getContractIdsToUnlink(gridId);
        if (unlinkDetails != false && validateRepertoireToUnlink(unlinkDetails)) {
            if (gridId.toString().indexOf('release') != -1)
                repertoireType = "Release";
            $.post('/GCS/RightsReviewWorkQueue/UnlinkContract/', { unLinkItems: unlinkDetails + '^' + repertoireType, isChangeLink: false },
                function (data) {
                  if (data == "") {
                        $find(gridId).sendRefreshRequest();
                        ShowSuccess(rightsWorkQueueMessages.unlinkSuccess);
                        //pageScrollRightsWorkQueue();
                        rightsWorkQueueGridScroll(gridId);
                        customMenu();
                    } else {
                        ShowWarning("Unlink failed due to concurrency");
                        $find(gridId).sendRefreshRequest();
                        rightsWorkQueueGridScroll(gridId);
                        customMenu();
                    }
                }).error(function () {
                    ShowWarning("Error in Unlink");
                    $find(gridId).sendRefreshRequest();
                    rightsWorkQueueGridScroll(gridId);
                    customMenu();
                });
        }
    });


    /*Assigning GridId*/
    rightsAcquiredGridId = $('#hiddenRightsAcquiredGridId').val();
    digitalGridId = $('#hiddenDigitalGridId').val();
    secondaryExploitationGridId = $('#hiddenSecondaryExpGridId').val();
    PreClearanceGridId = $('#hiddenPreClearanceGridId').val();

    /*Tab Change Events*/
    $(".divRightsPriorityWorkQTab").tabs
    ({
        select: function (event, ui) {
            HideWarningSuccess();
            customMenu();
            prevIndex = index;
            index = ui.index;
            resizeAccordion();
            var gridId = $('#hdnGridId').val();


            if ($find(gridId)._edit._isEdit == true) {
                result = confirm("You have unsaved changes pending. If you refresh the workqueue you will lose these changes. Are you sure you want to proceed?");
            } else {
                result = true;
            }
            return result;
        }
    });


    /*To add the blue class on page load for first tab*/
    $('.divRightsPriorityWorkQTab .colorTabs ul li a').each(function () {
        $(this).parent().addClass('blueClassAcquired');
        $('.divRightsPriorityWorkQTab .colorTabs ul li a').eq(0).addClass('blueClassAcquiredLink');
    });

    /*To Dynamically Add color to the tab header*/
    //$('.divRightsPriorityWorkQTab').on('click', 'a li ul .colorTabs', function () {a li ul .colorTabs .divRightsPriorityWorkQTab
    $('.divRightsPriorityWorkQTab .colorTabs ul li a,.divResourceRightsPriorityWorkQTab .colorTabs ul li a').live('click', function () {
        if (result) { tabLoad(index); }
        var tab = $(this).html();
        if (!result) {
            return false;
        }
        else {
            switch (tab.trim().toLowerCase()) {
                case rightsWorkQueueValues.rightsAcquired:

                    $(this).parents('ul').find('li').each(function () {
                        $(this).removeClass('blueClassAcquired');
                        $(this).removeClass('yellowDigitalRestriction');
                        $(this).removeClass('greenSecondaryExploitation');
                        $(this).removeClass('violetPreClearance');
                    });
                    $(this).parent().addClass('blueClassAcquired');

                    $(this).parents('ul').find('a').each(function () {
                        $(this).removeClass('yellowDigitalRestrictionLink');
                        $(this).removeClass('greenSecondaryExploitationLink');
                        $(this).removeClass('violetPreClearanceLink');
                        $(this).removeClass('blueClassAcquiredLink');
                    });

                    $(this).addClass('blueClassAcquiredLink');

                    $("div.colorTabs").css("borderBottom", '3px solid Blue');
                    break;
                case rightsWorkQueueValues.digitalRestriction:
                    $(this).parents('ul').find('li').each(function () {
                        $(this).removeClass('blueClassAcquired');
                        $(this).removeClass('yellowDigitalRestriction');
                        $(this).removeClass('greenSecondaryExploitation');
                        $(this).removeClass('violetPreClearance');
                    });
                    $(this).parent().addClass('yellowDigitalRestriction');

                    $(this).parents('ul').find('a').each(function () {
                        $(this).removeClass('violetPreClearanceLink');
                        $(this).removeClass('greenSecondaryExploitationLink');
                        $(this).removeClass('blueClassAcquiredLink');
                        $(this).removeClass('yellowDigitalRestrictionLink');
                    });

                    $(this).addClass('yellowDigitalRestrictionLink');

                    $("div.colorTabs").css("borderBottom", '3px solid Yellow');
                    break;
                case rightsWorkQueueValues.secondaryExploitation:
                    $(this).parents('ul').find('li').each(function () {
                        $(this).removeClass('blueClassAcquired');
                        $(this).removeClass('yellowDigitalRestriction');
                        $(this).removeClass('greenSecondaryExploitation');
                        $(this).removeClass('violetPreClearance');
                    });
                    $(this).parent().addClass('greenSecondaryExploitation');

                    $(this).parents('ul').find('a').each(function () {
                        $(this).removeClass('yellowDigitalRestrictionLink');
                        $(this).removeClass('blueClassAcquiredLink');
                        $(this).removeClass('violetPreClearanceLink');
                        $(this).removeClass('greenSecondaryExploitationLink');
                    });
                    $(this).addClass('greenSecondaryExploitationLink');
                    $("div.colorTabs").css("borderBottom", '3px solid Green');
                    break;
                case rightsWorkQueueValues.preClearance:
                    $(this).parents('ul').find('li').each(function () {
                        $(this).removeClass('blueClassAcquired');
                        $(this).removeClass('yellowDigitalRestriction');
                        $(this).removeClass('greenSecondaryExploitation');
                        $(this).removeClass('violetPreClearance');
                    });
                    $(this).parent().addClass('violetPreClearance');
                    $(this).parents('ul').find('a').each(function () {
                        $(this).removeClass('yellowDigitalRestrictionLink');
                        $(this).removeClass('greenSecondaryExploitationLink');
                        $(this).removeClass('blueClassAcquiredLink');
                        $(this).removeClass('violetPreClearanceLink');
                    });

                    $(this).addClass('violetPreClearanceLink');

                    $("div.colorTabs").css("borderBottom", '3px solid Violet');
                    break;
            }
        }

    });
});



/*Get Filter Data for Grid*/
function getFilteredGridData(pageId) {
    switch (reviewTabIndex) {
        case 1:
            //$("#rightsAcquired" + pageId + "waiting_WaitingPopup").css({ "display": "block", "height": $("#rightsAcquired" + pageId).height() });
            $find('rightsAcquired' + pageId).sendRefreshRequest();
            break;
        case 2:
            // $("#digitalGrid" + pageId + "waiting_WaitingPopup").css({ "display": "block", "height": $("#digitalGrid" + pageId).height() });
            $find('digitalGrid' + pageId).sendRefreshRequest();
            break;
        case 3:
            //  $("#secondaryExp" + pageId + "waiting_WaitingPopup").css({ "display": "block", "height": $("#secondaryExp" + pageId).height() });
            $find('secondaryExp' + pageId).sendRefreshRequest();
            break;
        case 4:
            // $("#preClearanceGrid" + pageId + "waiting_WaitingPopup").css({ "display": "block", "height": $("#preClearanceGrid" + pageId).height() });
            $find('preClearanceGrid' + pageId).sendRefreshRequest();
            break;
    }
}


/*To Find Grid Id*/
function getGridId() {
    switch (reviewTabIndex) {
        case 1:
            return 'rightsAcquired';
        case 2:
            return 'digitalGrid';
        case 3:
            return 'secondaryExp';
        case 4:
            return 'preClearanceGrid';
    }
}


function onLoad() {

    //var args1 = args;
}

/*Confirmation popup on page navigation*/
function openConfimationPopup() {
    var returnInfo = false;
    var objConfirmationPopupReview = $('<div id="ReviewConfirmationPopup"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: "Confirm",
            show: 'clip',
            hide: 'clip',
            width: 750,
            resizable: false,
            close: function () {
                $(this).remove();
            }
        });

    objConfirmationPopupReview.dialog({ buttons: [{
        text: 'Ok',
        "class": 'repertoirepopupprimary',
        click: function (e) {
            e.preventDefault();
            $(this).dialog('close');
            returnInfo = true;
        }
    }, {
        text: 'Cancel',
        "class": 'repertoirepopupsecondary',
        click: function (e) {
            $(this).dialog('close');
            e.preventDefault();

        }
    }]
    });
    objConfirmationPopupReview.dialog('open');
    return returnInfo;
}

/*Tab load events handling*/
function tabLoad(index) {
    HideWarningSuccess();
    customMenu();
    var resourceWqController = false; //For UC-19
    if (window.getCustomWQCheck) {
        //replace(/[^0-9]/g, '');
        var pageId = getCustomWQCheck.substring(getCustomWQCheck.length - 1, getCustomWQCheck.length);
        if (pageId > 1) {
            resourceWqController = true;
        }
    }
    if (tabNumber == undefined || tabNumber == null) {
        var tabNumber = '1';
    }
    var h1 = $(window).height();
    $(".divRightsWorkQueue").css('height', h1 - 150);
    switch (index) {
        case 0:
            reviewTabIndex = 1;
            // first tab selected 
            if (resourceWqController == false) {
                $(".divRightsWorkQueue").tabs('option', 'selected');
                $(".divRightsWorkQueue").tabs('select', '#divRightsAcquiredTab');
                $('#divHiddenDigitalRestrictionContainer').hide();
                if ($('#divRightsAcquiredTab').html().trim() != "") {
                    setTimeout(function () { getFilteredGridData(""); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/RightsAcquired/?gridId=' + rightsAcquiredGridId, function (data) {
                        $('#divRightsAcquiredTab').html(data);
                    });
                }
            } else {
                //UC 19 Controller//
                if ($('#divResourceRightsAcquiredTab').html().trim() != "" && $('#divResourceRightsAcquiredTab').is(':visible')) {
                    getFilteredGridData(pageId);
                    setTimeout(function () { getFilteredGridData(pageId); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/ResourceCustomWQRightsAcquired?pageId=' + pageId + '&tabNumber=' + tabNumber, function (data) {
                        $('#workQueueTabs-' + pageId).find('#divResourceRightsAcquiredTab').html(data);
                    });
                }
            }
            break;
        case 1:
            // second tab selected
            reviewTabIndex = 2;
            if (resourceWqController == false) {
                $(".divRightsWorkQueue").tabs('option', 'selected');
                $(".divRightsWorkQueue").tabs('select', '#divDigitalRestrictionsTab');
                $('#divHiddenDigitalRestrictionContainer').show();
                if ($('#divDigitalRestrictionsTab').html().trim() != "") {
                    setTimeout(function () { getFilteredGridData(""); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/ResourceDigitalRestrictions/?gridId=' + digitalGridId, function (data) {
                        $('#divDigitalRestrictionsTab').html(data);
                    });
                }
            } else {
                //UC 19 Controller//
                if ($('#divResourceDigitalRestrictionsTab').html().trim() != "" && $('#divResourceDigitalRestrictionsTab').is(':visible')) {
                    setTimeout(function () { getFilteredGridData(pageId); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/ResourceCustomWQDigitalRestrictions?pageId=' + pageId + '&tabNumber=' + tabNumber, function (data) {
                        $('#workQueueTabs-' + pageId).find('#divResourceDigitalRestrictionsTab').html(data);
                    });
                }
            }
            break;
        case 2:
            // Third tab selected
            reviewTabIndex = 3;
            if (resourceWqController == false) {
                $(".divRightsWorkQueue").tabs('option', 'selected');
                $(".divRightsWorkQueue").tabs('select', '#divSecondaryExploitationTab');
                $('#divHiddenDigitalRestrictionContainer').hide();
                if ($('#divSecondaryExploitationTab').html().trim() != "") {
                    setTimeout(function () { getFilteredGridData(""); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/SecondaryExploitation/?gridId=' + secondaryExploitationGridId, function (data) {
                        $('#divSecondaryExploitationTab').html(data);
                    });
                }
            } else {
                //UC 19 Controller//
                if ($('#divResourceSecondaryExploitationTab').html().trim() != "" && $('#divResourceSecondaryExploitationTab').is(':visible')) {
                    setTimeout(function () { getFilteredGridData(pageId); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/ResourceCustomWQSecExploitation?pageId=' + pageId + '&tabNumber=' + tabNumber, function (data) {
                        $('#workQueueTabs-' + pageId).find('#divResourceSecondaryExploitationTab').html(data);
                    });
                }

            }
            break;
        case 3:
            // Fourth tab selected
            reviewTabIndex = 4;
            if (resourceWqController == false) {
                $(".divRightsWorkQueue").tabs('option', 'selected');
                $(".divRightsWorkQueue").tabs('select', '#divPreClearanceTab');
                $('#divHiddenDigitalRestrictionContainer').hide();
                if ($('#divPreClearanceTab').html().trim() != "") {
                    setTimeout(function () { getFilteredGridData(""); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/PreClearance/?gridId=' + PreClearanceGridId, function (data) {
                        $('#divPreClearanceTab').html(data);
                    });
                }
            } else {
                //call UC 19 controller
                if ($('#divResourcePreclearanceTab').html().trim() != "" && $('#divResourcePreclearanceTab').is(':visible')) {
                    setTimeout(function () { getFilteredGridData(pageId); }, 350);
                } else {
                    $.get('/GCS/RightsReviewWorkQueue/ResourceCustomWQPreClearance?pageId=' + pageId + '&tabNumber=' + tabNumber, function (data) {
                        $('#workQueueTabs-' + pageId).find('#divResourcePreclearanceTab').html(data);
                    });
                }
            }
            break;
    }
}

function checkEditPermission(gridId) {
    if (gridId.toLowerCase().indexOf('rights') != -1) {
        return $('#editRightsDataHeader').val();
    }
    if (gridId.toLowerCase().indexOf('digital') != -1) {
        return $('#editRightsDataDigital').val();
    }
    if (gridId.toLowerCase().indexOf('pre') != -1) {
        return $('#editRightsDataPrecleared').val();
    }
    if (gridId.toLowerCase().indexOf('secondary') != -1) {
        return $('#editRightsDataSecondary').val();
    }
    return false;
}


function validateSave(gridId) {

    if (gridId.toLowerCase().indexOf('rights') != -1) {
        if (gridId.toLowerCase().indexOf('release') != -1) {
            if (validateReleaseRightsAcquiredSave(gridId)) {
                $('#loadingDiv').hide(); $('#loaderPanel').hide();
                return false;
            }
        }
        else {
            if (validateRightsAcquiredSave(gridId)) {
                $('#loadingDiv').hide(); $('#loaderPanel').hide();
                return false;
            }
        }
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        return true;
    }

    if (gridId.toLowerCase().indexOf('digital') != -1) {
        if (!validateDigitalRestrictionSave()) {
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return false;
        }
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        return true;
    }

    if (gridId.toLowerCase().indexOf('pre') != -1) {
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        return true;
    }

    if (gridId.toLowerCase().indexOf('secondary') != -1) {
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        return true;
    }
    $('#loadingDiv').hide(); $('#loaderPanel').hide();
    return false;
}


/*Disables the given element's Editing*/
function disableEditing(element) {
    $(element).addClass('disabledElement');
    $(element).click(function () {
        return false;
    });
    return false;
}

function disableEditingWithOutClass(element) {
    $(element).click(function () {
        return false;
    });
    return false;
}

/* on Row selection hide warning */
function onRowSelectionEvent(sender, args) {
    HideWarningSuccess();
    //pageScrollRightsWorkQueue();
    customMenu();
}

//Validating for Special Characters

$("#txtArtistName, #txtClearanceAdminCompany, #txtTitles, #txtIsrc, #txtUpc").keypress(function (e) {
    // ValidateSpecialCharacters(e);
});


//******Change link*******//
//Grid_Id : $('#hdnGridId').val()
$(function () {
    setTimeout(function () {
        var gridObjRights = $find($('#hdnGridId').val());
        gridObjRights.set_AllowSelection(true);
        gridObjRights.set_AllowSelection(false);
    }, 0);

    $("#rightsAcquired_Table").bind("click", function (event) {
        event.stopImmediatePropagation();
        HideWarningSuccess();
        var selectedTr = event.target.parentElement;
        var className = event.target.className;
        if (className == "chkChildClass")
            event.target = event.target.parentNode;
        if (event.target.tagName == "TD") {
            if (className != "chkChildClass") {
                var checkbox = $(selectedTr).find("#chkChild")[0];
                $(checkbox).attr("checked", !checkbox.checked);
            }
            synGridCheckBoxSelectionRightsAcquired(event);
        }
    });
});


function CheckBoxAllClick(args) {
    var obj = $("#" + $('#hdnGridId').val()).find(".GridHeader #chkAll");
    if (obj.attr("checked") == "checked") {
        $("#rightsAcquired .GridContent").find('#chkChild').attr("checked", "checked");
    }
    else {
        $("#rightsAcquired .GridContent").find('#chkChild').removeAttr("checked");
    }
    synGridCheckBoxSelectionRightsAcquired(args);
}

function synGridCheckBoxSelectionRightsAcquired(events) {
    events.stopImmediatePropagation();
    var gridObjRights = $find($('#hdnGridId').val());
    var checkboxes = $("#" + $('#hdnGridId').val()).find(".GridContent #chkChild");
    $.each(checkboxes, function (index, checkbox) {
        if (checkbox.checked) {
            var row = gridObjRights.get_ContentTable().getElementsByTagName("tr")[index];
            if (($.inArray(index, gridObjRights.get_SelectionManager().selectedRowsCollection)) == -1) {
                var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                gridObjRights.get_SelectionManager()._mDownHandler(eve);
            }
        }
        else
            gridObjRights.get_SelectionManager().deselectRow(index);

    });
    var result = gridObjRights.get_SelectedRecords();

    selectedGridItems = [];

    $.each(result, function (k, value) {
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
        selectedGridItems.push(
        {
            KeyId: getValue(getKeyValue(value.ResourceId, value.ReleaseId)),
            R2KeyId: getR2KeyValue(value.R2ResourceId, value.R2ReleaseId),
            ContractId: getValue(value.ContractId),
            LinkType: getValue(linkType),
            RightSetId: getValue(value.RightSetId)
        });
    });
}

//change link
function redirectToChangeLink() {
    //Change Link
    $('#btnReviewDataChangeLink').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        var isContractLinkingAvailable = true;
        var isRecordsSelected = true;
        if (selectedGridItems.length < 1) {
            isRecordsSelected = false;
            ShowWarning(rightsWorkQueueMessages.selectRecord);
            customMenu();
            //pageScrollRightsWorkQueue();
            var gridId = $('#hdnGridId').val();
            rightsWorkQueueGridScroll(gridId);
            return false;
        }

        $.each(selectedGridItems, function (k, value) {
            SelGridContractID = value.ContractId;
            if (value.ContractId == '' || value.ContractId == null || value.ContractId == '0') {
                ShowWarning(rightsWorkQueueMessages.linkingContract);
                customMenu();
                // pageScrollRightsWorkQueue();
                var gridId = $('#hdnGridId').val();
                rightsWorkQueueGridScroll(gridId);
                isContractLinkingAvailable = false;
                return false;
            }
        });
        if (isRecordsSelected && isContractLinkingAvailable) {
            var objSearchContractDialog = $('<div id="RightsSearchContract"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: '',
                    show: 'clip',
                    hide: 'clip',
                    width: getPopupWidth(70, 750),
                    height: getPopupHeight(80, 300, 30),
                    minHeight: 300,
                    minWidth: 750,
                    position: [getXPosition(70, 0), getYPosition(80, 60)],
                    resizable: true,
                    resize: function (event, ui) {
                        if ($("#RightsSearchContract").is(':visible')) {
                            if ($("#RightsSearchContract .jtable-main-container").is(':visible')) {
                                var DialogHgt = $("#RightsSearchContract").offset().top;
                                var TblConHgt = $("#RightsSearchContract .jtable-main-container").offset().top;
                                var TotDiaHgt = $("#RightsSearchContract").height();
                                var ReduceHgt = TblConHgt - DialogHgt;
                                var actualHeight = TotDiaHgt - ReduceHgt;
                                var jtableheight = $("#RightsSearchContract .jtable-main-container").find(".jtable").height() + $("#RightsSearchContract .jtable-main-container").find(".jtable-left-area").height();
                                if (jtableheight >= actualHeight)
                                    $("#RightsSearchContract .jtable-main-container").css('height', actualHeight - 40 + "px");
                                else
                                    $("#RightsSearchContract .jtable-main-container").css('height', jtableheight + 50 + "px");
                            }
                        }
                    },
                    close: function () {
                        searchContractPopupForChangeOrUnlink = false;
                        SelGridContractID = '';
                        isChangeLink = false;
                        isUnLink = false;
                        $(this).remove(); // ensures any form variables are reset.
                    }
                });

            if (selectedGridItems.length > 0 && selectedGridItems != null) {
                //                objDialogForChangeLinkConfirmation.dialog({ title: "Confirm" });
                //                objDialogForChangeLinkConfirmation.html("Do you want to change contract linking for the selected records?");
                //                objDialogForChangeLinkConfirmation.dialog('open');
                //                objDialogForChangeLinkConfirmation.dialog({
                //                    buttons: {
                //                        'Yes': function () {
                searchContractPopupForChangeOrUnlink = true;

                isChangeLink = true;
                objSearchContractDialog.html('<p>' + messageCommon.onLoading + '</p>');
                objSearchContractDialog.load('/GCS/Contract/ContractSearchPopup/', "",
                                function (responseText, textStatus) {
                                    if (textStatus == 'error') {
                                        objSearchContractDialog.html('<p>' + messageCommon.error + '</p>');
                                    }
                                });

                objSearchContractDialog.dialog('option', { title: "Search for Contract" });
                objSearchContractDialog.dialog('open');
                //                            $(this).dialog('close');
                //                        },
                //                        'No': function () {
                //                            searchContractPopupForChangeOrUnlink = false;
                //                            isChangeLink = false;
                //                            isUnLink = false;
                //                            $(this).dialog('close');
                //                        }
                //                    }
                //                });
            }
        }
    });
}

if (!window.resizeAccordion) {

    function resizeAccordion() {

        $("#txtIsrc").css('width', '155px');
        $("#txtUpc").css('width', '155px');
        var wid2 = $("#txtTitles").width();
        var wid3 = $("#RightsMasterData_ReviewReason_Values").width();
        $("#txtArtistName").css('width', wid2 - 95);
        $("#txtClearanceAdminCompany").css('width', wid3);
        $(".upcContainer").css("margin-left", wid2 - 305);
        $("#txtAreaLinkedContract").css("width", wid3 - 18);
        setTimeout(function () { $("#divReviewFilterButtons").css("margin-right", 10); }, 20);

    }
}


/*Functions for Change Link Functionality Starts*/
function getValue(idValue) {
    if (idValue == '' || idValue == undefined) { return null; }
    return idValue;
};

var linkType = '';
function getKeyValue(resourceIdValue) {
    if (resourceIdValue != '' && resourceIdValue != undefined && resourceIdValue != null) {
        linkType = 'Resource';
        return resourceIdValue;
    }
    else {
        linkType = '';
        return '';
    }
};

function getR2KeyValue(resourceIdValue) {
    if (resourceIdValue != '' && resourceIdValue != undefined && resourceIdValue != null) {
        return resourceIdValue;
    }
    else {
        return '';
    }
};


/*Functions for Change Link Functionality Ends*/

/*Returns True if it is Future Date*/
function isFutureDate(date) {
    if (new Date() < new Date(date)) {
        return true;
    }
    return false;
}

if (!window.getContractIdsToUnlink) {

    /*Returns the contractIds to unlink*/

    function getContractIdsToUnlink(gridId) {
        var unlinkDetails = "";
        var rowsSelected = $('#' + gridId + '_Table').find('tr input:checked').parents('tr');
        if (gridId.indexOf('digital') != -1 || gridId.indexOf('Digital') != -1) {
            var selectedDigitalRows = [];
            for (var i = 0; i < rowsSelected.length; i++) {
                if (!$($(rowsSelected[i]).find('td')[4]).hasClass('Merged')) {
                    selectedDigitalRows.push(rowsSelected[i]);
                }
            }
            rowsSelected = selectedDigitalRows;
        }


        if (rowsSelected.length == 1) {

            $(rowsSelected).each(function () {
                var rightSetId = "";
                resourceId = $(this).find('.repertoireId').text(); //ResourceId position                     
            });
        }
        else if (rowsSelected.length > 1) {
            ShowWarning(rightsWorkQueueMessages.unlinkRepertoire);
            customMenu();
            //pageScrollRightsWorkQueue(); 
            rightsWorkQueueGridScroll(gridId);
            return false;
        }
        else {
            ShowWarning(rightsWorkQueueMessages.selectRepertoire);
            customMenu();
            //pageScrollRightsWorkQueue(); 
            rightsWorkQueueGridScroll(gridId);
            return false;
        }


        $(rowsSelected).each(function () {
            var contractId = "";
            var resourceId = "";
            
            contractId = $(this).find('.contractId').text(); //ContractID 
            resourceId = $(this).find('.repertoireId').text(); //ResourceId  
            rightSetId = $(this).find('.tdRightSetId').text(); //RightSetId 
            unlinkDetails = contractId + "," + resourceId+','+rightSetId;
        });
        return unlinkDetails;
    }

    function validateRepertoireToUnlink(unlinkDetails) {
        var isValid = true;
        if (unlinkDetails == "" || unlinkDetails.split(',')[0] == "") {
            if (unlinkDetails.split(',')[0] == "") {
                ShowWarning(rightsWorkQueueMessages.resourceUnlink);
                customMenu();
                //pageScrollRightsWorkQueue();
                var gridId = $('#hdnGridId').val();
                rightsWorkQueueGridScroll(gridId);
                isValid = false;
                return false;
            } else {
                ShowWarning(rightsWorkQueueMessages.resourceUnlink);
                customMenu();
                //pageScrollRightsWorkQueue();
                var gridId = $('#hdnGridId').val();
                rightsWorkQueueGridScroll(gridId);
                isValid = false;
                return false;
            }
        }
        return isValid;
    }
}


/*Sets tooltip on reviewstatus change function*/
function setTooltipWithImage(senderElement) {
    //$(senderElement)[0].innerHTML = "<div class=\'flagGreen\'></div>";
    $(senderElement).attr('title', 'Final');
    $(senderElement).tooltip();
    return senderElement;
}


function customMenu() {

    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none') {

        if (!$.support.leadingWhitespace) {

            $('.customMenu .helpNavMenu ul#subnavlist').removeClass("customAlignWarning");
        }

        else if (($.browser.webkit)) {
            $('.customMenu .helpNavMenu ul#subnavlist').removeClass("customAlignWarningChrome");

        }
    }
    else
        if (!$.support.leadingWhitespace) {

            $('.customMenu .helpNavMenu ul#subnavlist').addClass("customAlignWarning");
        }
        else if (($.browser.webkit)) {
            $('.customMenu .helpNavMenu ul#subnavlist').addClass("customAlignWarningChrome");

        }


}


function rightsWorkQueueGridScroll(gridid) {
    rightsGridID = gridid;
    var pagerHgt = $("#" + rightsGridID + " .GridPager").height();
    var headerHgt = $("#" + rightsGridID + " .GridHeader").height();
    var browsHgt;
    if ($.browser.msie)
        browsHgt = 16;
    else
        browsHgt = 15;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;
    var jtableTop = $("#" + rightsGridID).offset().top;
    var topfootPos = $(".footer").offset().top;
    var totRecHeight = $("#" + rightsGridID + "_Table").height() + reduceHgt;
    var tableHeight = topfootPos - jtableTop;
    var gridObjRights = $find(rightsGridID);
    if (totRecHeight >= tableHeight)
        setSyncfusionGridHeight(gridObjRights, tableHeight - reduceHgt);
    else
        setSyncfusionGridHeight(gridObjRights, totRecHeight - reduceHgt + 20);
}


//Redirect to UC20//
function createCustomReleaseTab(searchParameters) {
    var appliedFilterParams;
    var workQueueFiltersDynamic;
    var tabLimitReached = false;
    if (searchParameters != "") {
        window.searchCritaria = searchParameters;
        paramQuery = undefined;


        if (searchParameters.split(',')[11] != "") {
            workQueueFiltersDynamic = searchParameters.split(',')[11];
            appliedFilterParams = "<div class='informationDiv'><span class='alignLeft'><span class='InfoName'> Custom Parameter: | ISRC : <span class='contractInfo'>" + workQueueFiltersDynamic + "</span></span></span></div>";
        }

        if (searchParameters.split(',')[12] != "") {
            workQueueFiltersDynamic = searchParameters.split(',')[12];
            appliedFilterParams = "<div class='informationDiv'><span class='alignLeft'><span class='InfoName'> Custom Parameter: | UPC : <span class='contractInfo'>" + workQueueFiltersDynamic + "</span></span></span></div>";
        }

        if ($.tab.tabCounter >= 8)
            tabLimitReached = true;
    }
    if (!tabLimitReached) {
        var tabName = "Custom WQ-Rel";

        //Region Adding
        var tabTitle = $("#tab_title");
        var tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close tabClose'>Remove Tab</span></li>"; //,
        var tabs = $("#divRightsPriorityWorkQueueTab").tabs();

        var id = "workQueueTabs-" + $.tab.totalTabCounter,
            label = tabTitle.val() || tabName,
            li = $(tabTemplate.replace(/#\{href\}/g, "#" + id).replace(/#\{label\}/g, label));
        tabs.find(".uitabWork").append(li);


        // ReSharper disable UsageOfPossiblyUnassignedValue
        tabs.append("<div id='" + id + "'><p>" + " " + "<p></p></p></div>");
        // ReSharper restore UsageOfPossiblyUnassignedValue

        var pageId = $.tab.totalTabCounter;
        tabCustomParams.push(null);
        var filterValue = window.searchCritaria.replace('&', 'amb;');

        $.get('/GCS/RightsReviewWorkQueue/ReleaseRightsWorkQueue/?pageId=' + pageId + '&searchCriteria=' + filterValue, function (data) {
            $('#' + id).html(data); //appliedFilterParams
            $('#' + id).find('#searchParamQuery' + pageId).append(appliedFilterParams);
        });

        tabs.tabs("refresh");
        $.tab.tabCounter = $.tab.tabCounter + 1;
        $.tab.totalTabCounter = $.tab.totalTabCounter + 1;
        $.tab.customSettings.push({ id: $.tab.tabCounter, tab: id });
        $("#divRightsPriorityWorkQueueTab").tabs("select", id);
    } else {
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        ShowWarning(rightsWorkQueueMessages.tabLimit);
    }
}
//Redirect to UC19//
function createCustomResourceTab(searchParameters) {
    //Set Custom WQ Value to 1 (Redirect to UC19)

    if (tabNumber == undefined || tabNumber == null) {
        var tabNumber = '1';
    }
    //window.tabNumber = tabNumber;

    var tabLimitReached = false;
    var appliedFilterParams;
    var workQueueFiltersDynamic;
    if (searchParameters != "") {
        window.searchCritaria = searchParameters;

        if (searchParameters.Isrc != "") { //Isrc 
            workQueueFiltersDynamic = searchParameters.Isrc;
            appliedFilterParams = "<div class='informationDiv'><span class='alignLeft'><span class='InfoName'> Custom Parameter: | ISRC : <span class='contractInfo'>" + workQueueFiltersDynamic + "</span></span></span></div>";
        }

        if (searchParameters.Upc != "") { //Upc
            workQueueFiltersDynamic = searchParameters.Upc;
            appliedFilterParams = "<div class='informationDiv'><span class='alignLeft'><span class='InfoName'> Custom Parameter: | UPC : <span class='contractInfo'>" + workQueueFiltersDynamic + "</span></span></span></div>";
        }

        // ReSharper disable UsageOfPossiblyUnassignedValue
        resourceParamQuery = appliedFilterParams;
        // ReSharper restore UsageOfPossiblyUnassignedValue
        if ($.tab.tabCounter >= 8)
            tabLimitReached = true;
    }

    if (!tabLimitReached) {
        var tabName = "Custom WQ-R&T";

        //Region Adding
        var tabTitle = $("#tab_title");
        var tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close tabClose'>Remove Tab</span></li>"; //,
        var tabs = $("#divRightsPriorityWorkQueueTab").tabs();

        var id = "workQueueTabs-" + $.tab.totalTabCounter,
            label = tabTitle.val() || tabName,
            li = $(tabTemplate.replace(/#\{href\}/g, "#" + id).replace(/#\{label\}/g, label));
        tabs.find(".uitabWork").append(li);

        // ReSharper disable UsageOfPossiblyUnassignedValue
        tabs.append("<div id='" + id + "'><p>" + "Retrieving Resource Custom Parameters" + "<p></p></p></div>");
        // ReSharper restore UsageOfPossiblyUnassignedValue
        var pageId = $.tab.totalTabCounter;
        tabCustomParams.push(window.filterCustomParams);
        $.get('/GCS/RightsReviewWorkQueue/ResourceRightsWorkQueue/?pageId=' + pageId + '&tabNumber=' + tabNumber, function (data) {
            $('#' + id).html(data);
            $('#' + id).find('.divSearchParamsToShow').append(resourceParamQuery);
        });

        tabs.tabs("refresh");
        $.tab.tabCounter = $.tab.tabCounter + 1;
        $.tab.totalTabCounter = $.tab.totalTabCounter + 1;
        $.tab.customSettings.push({ id: $.tab.tabCounter, tab: id });
        $("#divRightsPriorityWorkQueueTab").tabs("select", id);
    } else {
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        ShowWarning(rightsWorkQueueMessages.tabLimit);
    }
}

function DeleteCustomSettingFromCache(panelId) {
    $.each($.tab.customSettings, function () {
        if (this.tab != null) {
            if (this.tab.toLowerCase() == panelId.toLowerCase()) {
                $.tab.customSettings.pop(this);
            }
        }
    });
    var id = panelId;
    var newId; 
    while ($('#' + id).length == 0) {
        newId = parseInt(id.split('-')[1]) - 1;
        id = id.split('-')[0] + '-' + newId;
    }
    $("#divRightsPriorityWorkQueueTab").tabs("select", id);
}



function pageScrollRightsWorkQueue() {
    /*To Handle the \ on page load*/
    var windowHeight = $(window).height();
    var scrollHeight = windowHeight - 39 - 127;
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none' && $('#Errorr').css("display") == 'none') {
        $('#divScrollPriorityWorkQueue').css('height', scrollHeight); //divCustomRightsReviewWorkQueueContainer
        if ($('#divCustomRightsReviewWorkQueueContainer').length > 0) {
           // $('#divCustomRightsReviewWorkQueueContainer').css('height', scrollHeight);
        }
    }
    else {
        $('#divScrollPriorityWorkQueue').css('height', scrollHeight - 20);
        if ($('#divCustomRightsReviewWorkQueueContainer').length > 0) {
           // $('#divCustomRightsReviewWorkQueueContainer').css('height', scrollHeight - 20);
        }
    }
    //Scroll function handling on page resize
    $(window).resize(function () {
       
        windowHeight = $(window).height();
        scrollHeight = windowHeight - 39 - 127;
        if (scrollHeight > 700) {
            scrollHeight = 700;
        }
        if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none' && $('#Errorr').css("display") == 'none') {
            //$('#divScrollPriorityWorkQueue').css('height', scrollHeight);
            if ($('#divCustomRightsReviewWorkQueueContainer').length > 0) {
                //$('#divCustomRightsReviewWorkQueueContainer').css('height', scrollHeight);
            }
        } else {
            // $('#divScrollPriorityWorkQueue').css('height', scrollHeight - 20);
            if ($('#divCustomRightsReviewWorkQueueContainer').length > 0) {
                //$('#divCustomRightsReviewWorkQueueContainer').css('height', scrollHeight - 20);
            }
        }

        return false;
    });
    setTimeout(function () { $(window).unbind('resize'); }, 1000);
}



function ScrollBarMovement(gridID) {
    $("#" + gridID + " .GridContent").children("div:first").css("top", 0);
    $("#" + gridID + " .GridContent").children("div:first").css("left", 0);
    $("#" + gridID + " .sf-sp-Vhandle").css("top", 0);
    $("#" + gridID + " .sf-sp-Hhandle").css("left", 0);
}

function AddedLastRowCell(sender, sliceTd) {
    var trslength = $(sender._gridContentTable).find("tr").length;
    trslength = trslength - 1;
    var lastTr = $(sender._gridContentTable).find("tr")[trslength]
    var selectTd = $(lastTr).children("td.RowCell")[0];
    var ismergeFlag = $(selectTd).hasClass('Merged');
    if (ismergeFlag == true) {
        var totMergeRow = trslength - 1;
        for (var i = totMergeRow; i >= 0; i--) {
            var nextTr = $(sender._gridContentTable).find("tr")[i]
            var nextTd = $(nextTr).children("td.RowCell")[0];
            var isNextTdmergeFlag = $(nextTd).hasClass('Merged');
            if (isNextTdmergeFlag == false) {
                var lastvisbleRowTd = $(nextTr).children("td.RowCell").slice(0, sliceTd);
                $(lastvisbleRowTd).addClass("LastRowCell");
                break;
            }
        }
    }
}



/*Digital Restriction Common validation functions*/
/*MasterData Values used for validtions*/
var digitalMasterData = {
    restrictionReasonOther: "Other",
    restrictionTypeRights: "Rights",
    no: "No",
    restrictionTypeRefer: "Refer",
    reviewStatusFinal: "Final",
    useTypeStreaming: "Streaming",
    useTypeDownload: "Download",
    restrictionTypeConsult: "Consult",
    restrictionTypeConsent: "Consent",
    commercialModelAll: "All",
    commercialModelAdFunded: "Ad-funded"
};

/*Error Messages in tooltip*/
var digitalGridMessages = {
    digitalIncompleteValidation: "Please complete this Digital Restriction ",
    digitalValidCombinationValidation: "Please select a valid Digital Restriction combination ",
    digitalConsentPeriodValidation: "Please select a Consent/Consult Period ",
    digitalDuplicateCombinationValidation: "This Digital Restriction is a duplicate of another. Please remove or correct it ",
    digitalUniqueCombinationValidation: "This Digital Restriction is a duplicate of another. Please remove or correct it ",
    digitalRestrictionReasonValidation: "Please provide Restriction Reason at restriction row "
};

/*returns the restriction type of the selected row*/
function getRestrictionText(args, rowIndex) {
    var restriction;
    restriction = $($($('#' + $('#hdnGridId').val() + '_Table').find('tr')[rowIndex]).children('td.Restriction')).html();
    if (restriction.indexOf('<') != -1)
        restriction = $(this).find('#RestrictonLnr').val();
    return restriction;
}


/*Returns the parent row data*/
function getParentRowData() {
    var trs = $('#' + $('#hdnGridId').val() + '_Table').find('tr');
    for (var tr = 0; tr < trs.length; tr++) {
        if (checkIsParent(trs[tr])) {
            rightSetId.push($(trs[tr]).children('td.RightSetId').html());
            mergeCount.push($(trs[tr]).children('td.mergeCount').html());
        }
    }
    return;
}


/*Gets Child Datas in array*/
function getChildData() {
    var trs = $('#' + $('#hdnGridId').val() + '_Table').find('tr');
    var length = trs.length;
    for (var trIndex = 0; trIndex < length; trIndex++) {
        var tr = trs[trIndex], temp = "", tdValue = "", rightsId, isUpdated = false;
        $(tr)[0].style.backgroundColor = "";
        $(tr)[0].style.border = "";
        if ($(trs[trIndex])[0].style.display != "none") {//To handle delete scenario where row will be hidden(tr is table's row and trindex is row index)
            var tdUseType = $(trs[trIndex]).find('td.UseTypes');
            var useType = tdUseType.text(); 
            var tdCommercialModel = $(trs[trIndex]).find('td.CommercialModel');
            var commercialModel = tdCommercialModel.text(); 
            var tdRestriction = $(trs[trIndex]).find('td.Restriction');
            var restriction = tdRestriction.text(); 
            var tdRestrictionReason = $(trs[trIndex]).find('td.RestrictionReason');
            var restrictionReason = tdRestrictionReason.text(); 
            var tdConsentPeriod = $(trs[trIndex]).find('td.ConsentPeriod');
            var consentPeriod = tdConsentPeriod.text(); 
            var tdNotes = $(trs[trIndex]).find('td.Notes');
            var notes = tdNotes.text(); 
            var tdResourceType = $(trs[trIndex]).find('td.resourceType');
            var resourceType = tdResourceType.text(); 
            var tdMergeCount = $(trs[trIndex]).find('td.mergeCount');
            var mergeCount = $(tdMergeCount).text();
            var tdReviewStatus = $(trs[trIndex]).find('td.ReviewStatus');
           

            if (!isUpdated && ($(tdUseType).hasClass('updatedCell') || $(tdCommercialModel).hasClass('updatedCell') || $(tdRestriction).hasClass('updatedCell') || $(tdRestrictionReason).hasClass('updatedCell')
                || $(tdConsentPeriod).hasClass('updatedCell') || $(tdNotes).hasClass('updatedCell') || $(tdReviewStatus).hasClass('updatedCell'))) {
                isUpdated = true;
            }
            rightsId = $(trs[trIndex]).find('td.RightSetId').text();
                    tdValue = rightsId;
                    temp = useType + "^" + commercialModel + "^" + restriction + "^" + restrictionReason + "^" + consentPeriod + "^" + notes + "^" + resourceType + "^" + mergeCount +"^";
                    setData(tdValue, temp + (trIndex + 1), isUpdated);
             
            
            $(trs[trIndex]).find('td.rightsError').html("");
        }
    }
    return childs;
}



/*Push data to array after checking is rights set-id is already available*/
function setData(id, rowData, isUpdated) {
    var relationObject = new Object();
    relationObject.Id = ""; relationObject.isUpdated = ""; relationObject.Value = new Array();
    for (var i = 0; i < childs.length; i++) {
        if (childs[i].Id == id) {
            childs[i].Value.push(rowData);
            if (isUpdated) {
                childs[i].isUpdated = isUpdated;
            }
            return true;
        }
    }
    relationObject.Id = id;
    relationObject.Value.push(rowData);
    if (isUpdated)
        relationObject.isUpdated = true;
    childs.push(relationObject);
    return false;
}



/*To Check Is it Parent with class Merged*/
function checkIsParent(row) {
    var isNotMerged = false;
    $(row).find('td').each(function () {
        if (!$(this).hasClass('Merged')) {
            isNotMerged = true;
            return isNotMerged;
        }
        if (isNotMerged)
            return isNotMerged;
    });
    return isNotMerged;
}




/*Returns the Parent Row index if it is parent row else returns the empty string*/
function isParentOrChild(newChildData, childRowsRequired) {
    if (!childRowsRequired) {
        var parentRowIndex = "";
        for (var index = 0; index < newChildData.length; index++) {
            if (newChildData[index].split('^')[newChildData[index].split('^').length - 2].trim() > 0) { //7th(l-2) element is the data for mergecount unique for parents alone
                parentRowIndex = newChildData[index].split('^')[newChildData[index].split('^').length - 1];
                return parentRowIndex;
            }
        }
        return parentRowIndex;
    }
    if (childRowsRequired) {
        var childRowIndex = "";
        for (var childIndex = 0; childIndex < newChildData.length; childIndex++) {
            if (newChildData[childIndex].split('^')[newChildData[childIndex].split('^').length - 2].trim() == 0) { //7th(l-2) element is the data for mergecount unique for parents alone
                if (childRowIndex != "")
                    childRowIndex = childRowIndex + ',' + newChildData[childIndex].split('^')[newChildData[childIndex].split('^').length - 1];
                else {
                    childRowIndex = newChildData[childIndex].split('^')[newChildData[childIndex].split('^').length - 1];
                }
            }
        }
        return childRowIndex;
    }
}


/*To fill Commercial Model If  Usetype Already Exists*/
function fillUseTypeAndCommercial(useType, commercialModel, rowId) {
    for (var index = 0; index < newArrayDigital.length; index++) {
        if (newArrayDigital[index].UseType == useType) {
            newArrayDigital[index].CommercialModel.push(commercialModel + "^" + rowId);
            return true;
        }
    }
    return false;
}


/*Function to check Unique Combination (ALL)*/
function checkDigitalUniqueCombination(useType) {
    for (var index = 0; index < newArrayDigital.length; index++) {
        if (useType ==digitalMasterData.useTypeDownload && newArrayDigital[index].UseType == digitalMasterData.useTypeDownload && newArrayDigital[index].CommercialModel.length > 1) {
            for (var innerDownloadIndex = 0; innerDownloadIndex < newArrayDigital[index].CommercialModel.length; innerDownloadIndex++) {
                if (newArrayDigital[index].CommercialModel[newArrayDigital[index].CommercialModel.length - 1].split('^')[0] == digitalMasterData.commercialModelAll) {
                    return false;
                }
                if (newArrayDigital[index].CommercialModel[innerDownloadIndex].split('^')[0] == digitalMasterData.commercialModelAll) {
                    return parseInt(newArrayDigital[index].CommercialModel[innerDownloadIndex+1].split('^')[1]) - parseInt(newArrayDigital[index].CommercialModel[innerDownloadIndex].split('^')[1]);
                }
            }
        }
        if (useType == digitalMasterData.useTypeStreaming && newArrayDigital[index].UseType == digitalMasterData.useTypeStreaming && newArrayDigital[index].CommercialModel.length > 1) {
            for (var innerIndex = 0; innerIndex < newArrayDigital[index].CommercialModel.length; innerIndex++) {
                if (newArrayDigital[index].CommercialModel[newArrayDigital[index].CommercialModel.length - 1].split('^')[0] == digitalMasterData.commercialModelAll) {
                    return false;
                }
                if (newArrayDigital[index].CommercialModel[innerIndex].split('^')[0] == digitalMasterData.commercialModelAll) {
                 return parseInt(newArrayDigital[index].CommercialModel[innerIndex + 1].split('^')[1]) - parseInt(newArrayDigital[index].CommercialModel[innerIndex].split('^')[1]);
                }
            }
        }
    }
    return true;
}



/*Returns true if the combination exists already*/
function checkDrAlreadyExists(aryDigital) {
    for (var index = 0; index < aryDigital.length; index++) {
        for (var innerIndex = 0; innerIndex < aryDigital.length; innerIndex++) {
            if (index != innerIndex) {
                if (index !=aryDigital.length-1 && aryDigital[index].toString().replace("&nbsp;", " ").replace("&nbsp;", " ") == aryDigital[aryDigital.length-1].toString().replace("&nbsp;", " ").replace("&nbsp;", " ")) {
                    return innerIndex + 1;
                }
            }
        }
    }
}


function validateDigitalRestrictionSave() {
    window.childs = new Array();
    window.drErrorExist = false;
    window.newArrayDigital = new Array();
    //getParentRowData();
    var childData = getChildData(); //Array of objects with parent child relation using rights-set-id    
    if (inValidCombination(childData) || window.drErrorExist)//true Means Error
        return false;
    else {
        return true;
    }
}


/*To Append Error Icon On Error*/
function appendErrorToGrid(rowId, errorMessage) {
    window.drErrorExist = true;
    var tr = $('#' + $('#hdnGridId').val() + '_Table').find('tr')[parseInt(rowId.trim()) - 1];
    $(tr).find('td.rightsError').html("<span class='alertImage " + "imgErrorMessage" + $(tr).index() + "' title='" + errorMessage + "' ></span>");
    $(".imgErrorMessage" + $(tr).index()).tooltip({ showURL: false });
    $(tr)[0].style.backgroundColor = "#FFE9E8";
    $(tr)[0].style.border = "2px solid red";
    return;
}


function GridScrollBar(gridId) {
        var pagerHgt = $("#" + gridId + " .GridPager").height();
        var headerHgt = $("#" + gridId + " .GridHeader").height();
        var browsHgt = 0;
        if ($.browser.msie)
            browsHgt = 16;
        else
            browsHgt = 20;
        var reduceHgt = pagerHgt + headerHgt + browsHgt;

        var jtableTop = $("#" + gridId).offset().top;
        var topfootPos = $(".footer").offset().top;
        var totRecHeight = $("#" + gridId + "_Table").height() + reduceHgt;
        var tableHeight = topfootPos - jtableTop;
        var gridObj = $find(gridId);
        if (totRecHeight >= tableHeight)
            setGridScrollBarHeight(gridObj, tableHeight - reduceHgt);
        else
            setGridScrollBarHeight(gridObj, totRecHeight - reduceHgt + 20);
}

function setGridScrollBarHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}
