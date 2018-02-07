var sortType = "";
var sortColumn = "";
var rightsAcquiredterritoriesStr = "";
var trEditType = "", trRightsId = "";
var lostRightsDateErrorMessage = "";
var rightsGridID = $('#hdnGridId').val();
if (rightsGridID == undefined || rightsGridID == "") {
    rightsGridID = "rightsAcquired";
}

var rightsIncludedC, rightsIncludedT, rightsExcludedC, rightsExcludedT;
var isSingleTRmodified = false;


$(document).ready(function () {

    $('.divRightsPriorityWorkQTab .colorTabs ul li a ').each(function () {
        $(this).parent().addClass('blueClassAcquired');
        $('.divRightsPriorityWorkQTab .colorTabs ul li a').eq(0).addClass('blueClassAcquiredLink');
        //$(this).addClass('blueClassAcquiredLink');
    });
    $('.divResourceRightsPriorityWorkQTab .colorTabs ul li a ').each(function () {
        $(this).parent().addClass('blueClassAcquired');
        $('.divResourceRightsPriorityWorkQTab .colorTabs ul li a').eq(0).addClass('blueClassAcquiredLink');
        //$(this).addClass('blueClassAcquiredLink');
    });

    /*Hiding the syncfusion toolbar on page load */
    $('.sf-toolbar').hide();

    if (window.resizeAccordion) {
        resizeAccordion();
    }

    $('.RefreshPager').click(function () {
        $("#chkAll").removeAttr('checked');
        HideWarningSuccess();
    });

});



//Bulk Edit Popup open
// if (!window.callBulkEditButton) {
function callBulkEditButton() {
    var rightsBulkGridId = $('#hdnGridId').val();
    var bulkEditRecordlength = $find(rightsBulkGridId).get_SelectedRecords().length;
    if (bulkEditRecordlength >= 1) {
        $("#" + rightsBulkGridId + "waiting_WaitingPopup").css({ "display": "block", "top": ($('#' + rightsBulkGridId).height() / 2), "height": $(window).height() });
        var objBulkEditRightsAcquired = $('<div class="divobjBulkEditRightsAcquired"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: 'Bulk Edit - Rights Acquired',
                    show: 'clip',
                    hide: 'clip',
                    width: 900,
                    height: 550,
                    resizable: false,
                    close: function () {
                        $(this).remove();
                        $("#" + rightsGridID).mousedown();
                    }
                });
        objBulkEditRightsAcquired.load('/GCS/RightsReviewWorkQueue/RightsAcquiredBulkEdit', "",
                            function (responseText, textStatus) {
                                if (textStatus == 'error') {
                                    objBulkEditRightsAcquired.html('<p>' + messageCommon.error + '</p>');
                                }
                            });
        objBulkEditRightsAcquired.dialog('open');
        trEditType = "Bulk";
    }
    else {
        ShowWarning('Please select Repertoire');
        rightsAcquiredGridScroll($('#hdnGridId').val());
    }
}

// Bulk Edit Dialog 

/*Validate Data On Rights Acquired Grid Save*/
if (!window.validateRightsAcquiredSave) {
    function validateRightsAcquiredSave(newGridId) {
        var isSaveError = false;
        var rightsLostReasonErrorMessage = "Please select a Lost Rights Reason";
        var rightsGridRow=$('#' + newGridId + '_Table').find('tr');
        for (var rowIndex = 0; rowIndex < rightsGridRow.length; rowIndex++) {
            var tr = rightsGridRow[rowIndex];
            var lostRightsReason, lostRightsDate, isError = false;
            var isupdated = false;
            var isSaveErrorNew = false;
            var tdError = $(tr).find('td.tdError');
            lostRightsDate = $(tr).find('td.tdLostRightsDateLnr').text();//tdError
             
            lostRightsReason = $(tr).find('td.tdLostRightsReasonLnr').text();
               
                if ($(tr).find('td.tdError').hasClass('hasError')) {
                    isSaveError = true;
                    isSaveErrorNew = true;
                }
                if (!isSaveErrorNew) {
                    $(tdError).html("");
                        $(tr)[0].style.backgroundColor = "";
                        $(tr)[0].style.border = "";
                }
                if ($(tr).find('td.tdRightsExceptionLnr').hasClass('updatedCell') || $(tr).find('td.tdPhysicalExpLnr').hasClass('updatedCell') || $(tr).find('td.tdDigitalExpLnr').hasClass('updatedCell') || $(tr).find('td.tdDigitalUnbundledLnr').hasClass('updatedCell') ||
                    $(tr).find('td.tdRightsPeriodLnr').hasClass('updatedCell') || $(tr).find('td.tdActiveForMrktLnr').hasClass('updatedCell') || $(tr).find('td.tdPpbClaimLnr').hasClass('updatedCell') || $(tr).find('td.tdMobileExpLnr').hasClass('updatedCell') ||
                    $(tr).find('td.tdLostRightsLnr').hasClass('updatedCell') || $(tr).find('td.tdLostRightsDateLnr').hasClass('updatedCell') || $(tr).find('td.tdLostRightsReasonLnr').hasClass('updatedCell') || $(tr).find('td.tdTerritoryRightsLnr').hasClass('updatedCell')) {
                    isupdated = true;
                }


            if (lostRightsDate != "" && isupdated) {
                if (lostRightsReason == "") {
                            $(tdError).html("<span class='alertImage " + "imgRightsErrorMessage" + $(tr).index() + "' title='" + rightsLostReasonErrorMessage + "' ></span>");
                            $('.' + "imgRightsErrorMessage" + $(tr).index()).tooltip({ showURL: false });
                            $(tr)[0].style.backgroundColor = "#FFE9E8";
                            $(tr)[0].style.border = "2px solid red";
                            isError = true;
                            return isError;
                }
            }
            if (isError) {
                isSaveError = true;
            }
            
        }
        return isSaveError;
    }
}


//Validation on save of rights acquired Grid Cell-Save.
function validateValuesOnEdit(sender, args) {//$(sender._editTD)

    if (args.colObj.Name == "RightsPeriodLnr") {
        if ((args.oldValue == "Life of Copyright" && args.value.indexOf("Copyright") != -1) || (args.oldValue == "In Perpetuity" && args.value.indexOf("Perpetuity") != -1) || (args.oldValue == "Other Duration" && args.value.indexOf('Other') != -1)) {
            args.value = args.oldValue;
        }
    }

    if (args.colObj.Name == "LostRightsReasonLnr") {
        if ((args.oldValue == "Rights Reversion" && args.value.indexOf("Reversion") != -1) || (args.oldValue == "Rights Expiry" && args.value.indexOf("Expiry") != -1)) {//Compared like this since special character is added.
            args.value = args.oldValue;
        }
    }

    if (args.colObj.Name == "RightsExceptionLnr") {
        if (args.oldValue == "Not Applied" && args.value.indexOf("Not") != -1) {
            args.value = args.oldValue;
        }
    }
    if (args.colObj.Name == "ReviewStatusLnr") {
        if (args.value != args.oldValue) {
            if (args.value == "Final") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).addClass('reviewFlagGreenUpdated');
                }, 100);
            }
            else if (args.value == "NewForReview") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).addClass('reviewFlagBlueUpdated');
                }, 100);
            }
            else if (args.value == "FinalForReview") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).addClass('reviewFlagOrangeUpdated');
                }, 100);
            }
        }
        else {
            if (args.value == "NewForReview") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).addClass('reviewFlagBlue');
                }, 100);
            }
            else if (args.value == "FinalForReview") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).addClass('reviewFlagOrange');
                }, 100);
            }
            else if (args.value == "Final") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).addClass('reviewFlagGreen');
                }, 100);
            }
        }
    }
    
    if ($(".divobjBulkEditRightsAcquired").is(':visible')) {
        args.cancel = true;
        args.preventPropagationClick = true;
    }
    else {
        if (args.colObj.Name == "TerritoryRightsLnr") {
            if (isSingleTRmodified == true) {
                if (rightsIncludedC != null && rightsIncludedC != undefined)
                    sender._currentDataItem.IncludedCountryLnr = rightsIncludedC;
                if (rightsIncludedT != null && rightsIncludedT != undefined)
                    sender._currentDataItem.IncludedTerritoryLnr = rightsIncludedT;
                if (rightsExcludedC != null && rightsExcludedC != undefined)
                    sender._currentDataItem.ExcludedCountryLnr = rightsExcludedC;
                if (rightsExcludedT != null && rightsExcludedT != undefined)
                    sender._currentDataItem.ExcludedTerritoryLnr = rightsExcludedT;

                if (args.value == undefined || args.value == "") {
                    args.value = args.oldValue;
                }
                //if (args.value == args.oldValue)
                //args.value = args.value + "  ";

                rightsIncludedC = "";
                rightsIncludedT = "";
                rightsExcludedC = "";
                rightsExcludedT = "";
            }
        }
    }
    var lostRights, lostRightsDate;
    lostRights = sender._currentDataItem.LostRightsLnr;
    lostRightsDate = sender._currentDataItem.LostRightsDateLnr;
    if (lostRightsDate != null) {
        if (lostRightsDate.toString().indexOf('UTC') != -1) {
            if (lostRightsDate.getDate().length == 1)
                lostRightsDate = '0' + lostRightsDate.getDate() + ' ' + lostRightsDate.toDateString().substr(4, 3) + ' ' + lostRightsDate.getFullYear();
            else
                lostRightsDate = lostRightsDate.getDate() + ' ' + lostRightsDate.toDateString().substr(4, 3) + ' ' + lostRightsDate.getFullYear();
        }
    }
    if (args.value != "" && args.colObj.Name == "LostRightsDateLnr") {
        if (validateLostRightsDate(args.value, lostRights, $(sender._editTD).parent())) {//args.value is Date
            $(sender._editTD).parent().find('td').each(function () {
                if ($(this).hasClass('resourceRightsTabError')) {
                    $(this).removeClass('hasError');
                    $(this).parent()[0].style.backgroundColor = "";
                    $(this).parent()[0].style.border = "";
                    $(this).html("");
                }
            });
            args.value = args.value;
            sender._currentDataItem.LostRightsDateLinear = args.value;
        } else {
            sender._currentDataItem.LostRightsDateLnr = "";
            sender._currentDataItem.LostRightsDateLinear = "";
            $($(sender._editTD).parent().find('td')[23]).html("");
            $($(sender._editTD).parent().find('td')[23]).addClass('updatedCell');
            args.value = "";
        }

    }

    if (args.value != "" && args.colObj.Name == "LostRightsLnr") {
        if (!$(sender._editTD.nextSibling).hasClass('disabledElement')) {
            if (validateLostRightsDate(lostRightsDate, args.value, $(sender._editTD).parent())) { //args.value is Y or N
                args.value = args.value;
                // sender._currentDataItem.LostRightsDateLinear = args.value;
            } else {
                if (!$(sender._editTD.nextSibling).hasClass('disabledElement')) {
                    sender._currentDataItem.LostRightsDateLnr = "";
                    sender._currentDataItem.LostRightsDateLinear = "";
                    $($(sender._editTD).parent().find('td')[23]).html("");
                    $($(sender._editTD).parent().find('td')[23]).addClass('updatedCell');
                }
            }
        }
    }

    if (args.value != "" && args.colObj.Name == "DigitalExpLnr") {
        if (args.value == "N") {
            sender._currentDataItem.DigitalUnbundledLnr = "N";
            $(sender._editTD.nextSibling).html("N");
            $(sender._editTD.nextSibling).addClass("updatedCell");
        }
    }

    if (args.colObj.Name == "UPCId") {
        args.value = args.oldValue;
    }

    if (args.colObj.Name == "ISRCId") {
        args.value = args.oldValue;
    }

    $("#" + sender._ID + "EditForm").find("#TerritoryRightsLnr").attr('disabled', 'disabled');
}

/*Lost Rights Date Validation*/
function validateLostRightsDate(dateToValidate, lostRightsData, tr) {
    var tdElements = $(tr).find('td');
    var actualDate = new Date(getGenericDateString(dateToValidate)); // convert to actual date
    var newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate() + 1); // create new increased date            
    var newDateString = ('0' + newDate.getDate()).substr(-2) + ' ' + newDate.toDateString().substr(4, 3) + ' ' + newDate.getFullYear();
    if (lostRightsData == "Y" && new Date() < new Date(newDateString)) {
        lostRightsDateErrorMessage = "Please select a Lost Rights Date which is in the past";
        for (var indexPasts = 0; indexPasts < tdElements.length; indexPasts++) {
            if ($(tdElements[indexPasts]).hasClass('resourceRightsTabError')) {
                $(tdElements[indexPasts]).addClass('hasError');
                $(tdElements[indexPasts]).html("<span class='alertImage imgRightsDateErrorMessage"+$(tr).index()+"' title='" + lostRightsDateErrorMessage + "' ></span>");
                $('.' + "imgRightsDateErrorMessage" + $(tr).index()).tooltip({ showURL: false });
                $(tdElements[indexPasts]).parent()[0].style.backgroundColor = "#FFE9E8";
                $(tdElements[indexPasts]).parent()[0].style.border = "2px solid red";
                return false;
            }
        }
        return false;
    }
    if (lostRightsData == "N" && new Date() > new Date(newDateString)) {
        lostRightsDateErrorMessage = "Please select a Lost Rights Date which is in the future";
        for (var indexFuture = 0; indexFuture < tdElements.length; indexFuture++) {
            if ($(tdElements[indexFuture]).hasClass('resourceRightsTabError')) {
                $(tdElements[indexFuture]).addClass('hasError');
                $(tdElements[indexFuture]).html("<span class='alertImage imgRightsDateErrorMessage' title='" + lostRightsDateErrorMessage + "' ></span>");
                $('.' + "imgRightsDateErrorMessage").tooltip({ showURL: false });
                $(tdElements[indexFuture]).parent()[0].style.backgroundColor = "#FFE9E8";
                $(tdElements[indexFuture]).parent()[0].style.border = "2px solid red";
                return false;
            }
        }
        return false;
    }
    $(tdElements).each(function() {
        $(this).removeClass('hasError');
    });
    return true;
}



/*Right acquired grid action begin*/
function onRightsGridActionBegin(events, args) {
    //$('#loadingDiv').hide();
    var num = events._ID.replace(/[^0-9]/g, '');
    $("[id*='waiting_WaitingPopup']").hide();
    var tabIndex = $("#" + "hiddenCusomTabIndex" + num).val();
    $('#hdnGridId').val(events._ID);

    if (num == '' || num == undefined) {        
        //uc18 coding part//
        args.data["filterParams"] = JSON.stringify(filterParams);
        args.data["isResourceCustom"] = true;
        $("#" + events._ID + " .MsgBar").empty();
        $("#" + events._ID + " .MsgBar").text("Search Results ( )");
        if (args.getRequestType() == "sorting") {
            if (args.data["SortDescriptors[0].ColumnName"] == "ISRCId" || args.data["SortDescriptors[0].ColumnName"] == "UPCId") {
                sortColumn = args.data["SortDescriptors[0].ColumnName"];
                if (args.data["SortDescriptors[0].SortDirection"] == "Ascending") {
                    args.data["SortDescriptors[0].SortDirection"] = "Descending";
                    sortType = "DESC";
                } else if (args.data["SortDescriptors[0].SortDirection"] == "Descending") {
                    args.data["SortDescriptors[0].SortDirection"] = "Ascending";
                    sortType = "ASC";
                } else {
                    args.data["SortDescriptors[0].SortDirection"] = "Ascending";
                    sortType = "ASC";
                }
            } else {
                sortType = "Normal";
                sortColumn = "Normal";
            }
        }
    } else {
        //UC 19 Coding Part//
        args.data["filterParams"] = JSON.stringify(window.tabCustomParams[num - 2]);
        //var splitValues = searchParams.split(',');
        args.data["tabIndex"] = tabIndex;

    }
}

/*On Grid Load(Action Success Of syncFusion)*/
function onRightsAcquiredGridSuccess(sender, args) {
    $('[id*=chkAll]').removeAttr('checked');
    $('[id*=chkAll]').removeAttr("indeterminate");
    setImageForRightsAcquired($('#'+sender._ID+"_Table").find('tr'));
    syncGridPagerSearchWQ(sender._ID);
    if (args.RequestType == "Save") {
        ShowSuccess(window.rightsAcquiredMessages.saveSuccess);
        rightsAcquiredGridScroll(sender._ID);
        customMenu();
        //pageScrollRightsWorkQueue();
    }
    if (args.RequestType == "sorting") {
        if (sortType == "DESC") {
            if (sortColumn == "ISRCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div><span class='Ascending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            } else if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Ascending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
            }
        }
        else if (sortType == "ASC") {
            if (sortColumn == "ISRCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div><span class='Descending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            }
            else if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Descending'></span>";
            }
        }
        else {
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip'  title='Click on UPC to create a release WQ for that UPC.'></div>";
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
        }

    } else {
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip'  title='Click on UPC to create a release WQ for that UPC.'></div>";
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
    }
    var totCount = sender.get_TotalRecordsCount();
    $("#" + sender._ID).find(".MsgBar").html("<div class='alignLeft' style='margin: 5px 5px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input type='button' id='bulkEditRightsAcquired' style='float: left;' value='Bulk Edit' onClick='callBulkEditButton()' class='primbutton'><div class='primbutton_right'></div></div>");
    setSyncGridToolTip("#" + sender._ID + "_Table");
    $("#" + sender._ID + "_UpcTooltip").tooltip();
    $("#" + sender._ID + "_IsrcTooltip").tooltip();
    rightsAcquiredGridScroll(sender._ID);
    rightsAcquiredCheckList();
    ScrollBarMovement(sender._ID);
    var num = sender._ID.replace(/[^0-9]/g, '');
    if (num != '' && num != undefined) {
        $find(sender._ID)._MvcTable.get_parentTable().hideColumnByIndex(16); // UC 19 - Column hiding  
    }
    
}

/*On Grid Error(Action Error Of syncFusion)*/
function onRightsAcquiredGridError(sender, args) {
    var totCount = sender.get_TotalRecordsCount();
    if (args.XMLHttpRequest.responseText != null) {
        displayDialog("Error", "&nbsp;&nbsp;&nbsp;Error Retreiving WorkQueue Data");
        rightsAcquiredGridScroll(sender._ID);
    }
    $("#" + sender._ID).find(".MsgBar").html("<div class='alignLeft' style='margin: 5px 5px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input type='button' id='bulkEditRightsAcquired' style='float: left;' value='Bulk Edit' class='primbutton'><div class='primbutton_right'></div></div>");
    rightsAcquiredGridScroll(sender._ID);
    $('#loaderPanel').hide();
    $('#loadingDiv').hide();
}

/*Sync Fusion GRid scroll functionality*/
function rightsAcquiredGridScroll(gridid) {
    rightsGridID = gridid;
    if ($("#" + rightsGridID).length > 0) {
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
}

function setSyncfusionGridHeight(gridObjRights, height) {
    gridObjRights.set_GridHeight(height);
    gridObjRights.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObjRights.refreshScroller();
}


function onCellEditCustom(sender, args) {
    if ($(".divobjBulkEditRightsAcquired").is(':visible')) {
        args.cancel = true;
        $('.divobjBulkEditRightsAcquired').mousedown(function (e) {
            e.stopImmediatePropagation();
        });
        $('.divobjBulkEditRightsAcquired').keypress(function (e) {
            e.stopImmediatePropagation();
        });
    }
    else if (args.colObj.Name == "TerritoryRightsLnr") {
        $("#" + sender._ID + "EditForm").find("#TerritoryRightsLnr").attr('disabled', 'disabled');
        var rightsId = sender._currentDataItem.RightsId;
        var objBulkEditRightsAcquiredTR = $('<div class="divobjBulkEditRightsAcquired"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: 'Bulk Edit - Rights Acquired',
                    show: 'clip',
                    hide: 'clip',
                    width: 900,
                    height: 550,
                    resize: function () {
                        if ($(".divobjBulkEditRightsAcquired").is(':visible')) {
                            var TotDiaHgt = $(".divobjBulkEditRightsAcquired").height();
                            var ReduceHgt = TotDiaHgt - 10;
                            $("#rightsAcquiredBulkEditTab").css('height', ReduceHgt + "px");
                        }
                    },
                    resizable: false,
                    close: function () {
                        $(this).remove();
                        $("#" + rightsGridID).mousedown();
                    }
                });
        objBulkEditRightsAcquiredTR.load('/GCS/RightsReviewWorkQueue/RightsAcquiredBulkEdit', "",
                        function (responseText, textStatus) {
                            if (textStatus == 'error') {
                                objBulkEditRightsAcquiredTR.html('<p>' + messageCommon.error + '</p>');
                            }
                        });
        objBulkEditRightsAcquiredTR.dialog('open');
        trEditType = "Single";
        trRightsId = rightsId;
        isSingleTRmodified = false;
    }
    if (args.colObj.Name == "DigitalUnbundledLnr") {
        if (sender._currentDataItem.ResourceType == '0') {
            $("#DigitalUnbundledLnr").attr("disabled", "disabled");
        }
    }

    if (args.colObj.Name == "UPCId") {
        var upcId = args.value;
        if (upcId != "" && upcId != null) {
            $('#loadingDiv').show();
                setTimeout(function () {
                    createCustomReleaseTab("1,,,,,,,,,,,," + upcId + ",true,true");
                }, 10);
        }
        $('#' + $('#hdnGridId').val()).mousedown();
    }

    if (args.colObj.Name == "ISRCId") {
        var isrcId = args.value;
        if (isrcId != "" && isrcId != null) {
            $('#loadingDiv').show();
                setTimeout(function () {
                    createCustomReleaseTab("1,,,,,,,,,,," + isrcId + ",,true,true");
                }, 10);
        }
        $('#' + $('#hdnGridId').val()).mousedown();
    }

    if (args.colObj.Name == "DigitalUnbundledLnr") {
        if (sender._currentDataItem.DigitalExpLnr == "N") {
            $(sender._editTD).attr('disabled', true);
        } else {
            $(sender._editTD).attr('disabled', false);
        }
    }

    if (args.colObj.Name == "DigitalUnbundledLnr") {
        if (sender._currentDataItem.DigitalExpLnr == "N") {
            $(sender._editTD).attr('disabled', true);
        } else {
            $(sender._editTD).attr('disabled', false);
        }
    }

    if (args.colObj.Name == "ReviewStatusLnr") {
        $(sender._editTD).removeClass('reviewFlagBlue');
        $(sender._editTD).removeClass('reviewFlagOrange');
        $(sender._editTD).removeClass('reviewFlagGreen');
        $(sender._editTD).removeClass('reviewFlagBlueUpdated');
        $(sender._editTD).removeClass('reviewFlagOrangeUpdated');
        $(sender._editTD).removeClass('reviewFlagGreenUpdated');
        var num = sender._gridID.replace(/[^0-9]/g, '');
        if (window.location.search != "") {
            num = '10';
        }
        var cellValue = $(sender._editTD).parent().find('td.flagStatus').html(); //$(sender._editTD).parent().find('td')[47].innerHTML;
        if (cellValue == "1") {
            $("#ReviewStatusLnr option[value='FinalForReview']").remove();
            $("#ReviewStatusLnr option[value='NewForReview']").text("New For Review");
        }
        else if (cellValue == "2") {
            $("#ReviewStatusLnr option[value='NewForReview']").remove();
            $("#ReviewStatusLnr option[value='FinalForReview']").text("Final For Review");
        }
        else if (cellValue == "3") {
            if (num == '' || num == undefined) {
                $("#ReviewStatusLnr option[value='NewForReview']").remove();
                $("#ReviewStatusLnr option[value='FinalForReview']").remove();
            }
            else {
                $("#ReviewStatusLnr option[value='NewForReview']").remove();
                $("#ReviewStatusLnr option[value='FinalForReview']").text("Final For Review");
            }
        }
    }
}


function rightsAcquiredCheckList() {
    rightsGridID = $('#hdnGridId').val();
    setTimeout(function () {
        var gridObjRights = $find(rightsGridID);
        gridObjRights.set_AllowSelection(true);
        gridObjRights.set_AllowSelection(false);
    }, 0);
    $("#" + rightsGridID + "_Table").unbind("click");
    $("#" + rightsGridID + "_Table").bind("click", function (event) {
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
}


function CheckBoxAllClick(args) {
    rightsGridID = $('#hdnGridId').val();
    var obj = $("#" + rightsGridID).find(".GridHeader #chkAll");
    if (obj.attr("checked") == "checked") {
        $("#" + rightsGridID + " .GridContent").find('#chkChild').attr("checked", "checked");
    }
    else {
        $("#" + rightsGridID + " .GridContent").find('#chkChild').removeAttr("checked");
    }
    synGridCheckBoxSelectionRightsAcquired(args);
}

function synGridCheckBoxSelectionRightsAcquired(events) {
    var curRow, curindex, ckFlag, tagValidchk;
    rightsGridID = $('#hdnGridId').val();
    var tablePositiontop = $("#" + rightsGridID + " .GridContent").children("div:first").css("top");
    var tablePositionLeft = $("#" + rightsGridID + " .GridContent").children("div:first").css("left");
    var vscrollposition = $("#" + rightsGridID + " .sf-sp-Vhandle").css("top");
    var hscrollposition = $("#" + rightsGridID + " .sf-sp-Hhandle").css("left");
    var hscrollHeader = $("#" + rightsGridID + " .GridHeader .Table").css("left");

    var gridObjRights = $find(rightsGridID);
    var checkboxes = $("#" + rightsGridID).find(".GridContent #chkChild");
    if ($.browser.msie) {
        tagValidchk = events.srcElement.outerHTML;
        tagValidchk = $(tagValidchk).attr("id");
    } else
        tagValidchk = events.target.id;

    if (tagValidchk != "chkAll") {
        curRow = events.target.parentNode;
        curindex = $(curRow).index();
    }
    ckFlag = false;
    $.each(checkboxes, function (index, checkbox) {
        if (checkbox.checked) {
            if (curindex == index)
                ckFlag = true;
            var row = gridObjRights.get_ContentTable().getElementsByTagName("tr")[index];
             if (ckFlag == false) {
                if (($.inArray(index, gridObjRights.get_SelectionManager().selectedRowsCollection)) == -1) {
                    var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                    gridObjRights.get_SelectionManager()._mDownHandler(eve);
                } 
            }
            if (ckFlag == true) {
                if (($.inArray(index, gridObjRights.get_SelectionManager().selectedRowsCollection)) == -1) {
                    var curTarget = { target: events.target, ctrlKey: true };
                    gridObjRights.get_SelectionManager()._mDownHandler(curTarget);
                }
            }
            ckFlag = false;
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
        selectedGridItems.push(
        {
            KeyId: getValue(getKeyValue(value.RepertoireId)),
            R2KeyId: getR2KeyValue(value.R2ResourceId),
            ContractId: getValue(value.ContractIdLnr),
            LinkType: getValue(linkType),
            RightSetId: getValue(value.RightSetId)
        });
    });

    $("#" + rightsGridID + " .GridContent").children("div:first").css("top", tablePositiontop);
    $("#" + rightsGridID + " .GridContent").children("div:first").css("left", tablePositionLeft);
    $("#" + rightsGridID + " .sf-sp-Vhandle").css("top", vscrollposition);
    $("#" + rightsGridID + " .sf-sp-Hhandle").css("left", hscrollposition);
    $("#" + rightsGridID + " .GridHeader .Table").css("left", hscrollHeader);
}

function rightsStrip(gridID) {
    $("#" + gridID + " .HeaderBar th:lt(19)").attr('style', 'background-color: #B8D0E9 !important');
    $("#" + gridID + " .HeaderBar th:gt(19)").attr('style', 'background-color: #c2e0f5 !important');
}



function onRightsAcquiredLoad(sender, args) {
    $("#" + sender._ID + "_toolbar").hide();
    $("#" + sender._ID + " .manualPagerLabel:eq(1)").empty();
    $("#" + sender._ID + " .manualPagerLabel:eq(1)").text("Show item per page");
    var chk = $("#" + sender._ID + " .GridHeader").find("th.HeaderCell .HeaderCellDiv")[0];
    $(chk).append("<input type=\"Checkbox\" id=\"chkAll\" onclick=\"CheckBoxAllClick(event)\"> All");
    $find(sender._ID).get_LocalizedStrings().bulkeditalert = "You have unsaved changes pending. If you refresh the workqueue you will lose these changes. Are you sure you want to proceed?";
    rightsStrip(sender._ID);
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
function pageScrollRightsWorkQueue() {
    /*To Handle the Scroll on page load*/
    var windowHeight = $(window).height();
    var scrollHeight = windowHeight - 39 - 127;
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none' && $('#Errorr').css("display") == 'none') {
        $('#divScrollPriorityWorkQueue').css('height', scrollHeight);
    }
    else {
        $('#divScrollPriorityWorkQueue').css('height', scrollHeight - 20);
    }


    //Scroll function handling on page resize
    $(window).resize(function () {
        windowHeight = $(window).height();
        scrollHeight = windowHeight - 39 - 127;
        if (scrollHeight > 700) {
            scrollHeight = 700;
        }
        if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none' && $('#Errorr').css("display") == 'none') {
            $('#divScrollPriorityWorkQueue').css('height', scrollHeight);
        }
        else {
            $('#divScrollPriorityWorkQueue').css('height', scrollHeight - 20);
        }
    });
    setTimeout(function () { $(window).unbind('resize'); }, 1000);
}

//sets the image and validation in action succes of the grid
function setImageForRightsAcquired(gridRows) {
    if ($(gridRows[0]).find('td.EmptyCell').length == 0) {
        for (var row = 0; row < gridRows.length; row++) {
            var tdUpcId = $(gridRows[row]).find('td.tdUpcId');
            var tdIsrcId = $(gridRows[row]).find('td.tdIsrcId');
            if ($(tdUpcId).html() == null || $(tdUpcId).html() == "") {
                $(tdUpcId).html("");
                disableEditingWithOutClass(tdUpcId); 
            }
            if ($(tdIsrcId).html() == null || $(tdIsrcId).html() == "") {
                $(tdIsrcId).html("");
                disableEditingWithOutClass(tdIsrcId); 
            }

            if ($(gridRows[row]).find('td.tdReviewStatusPermitted').html() == "N") {
                disableEditing($(gridRows[row]).find('td.tdReviewStatus'));
            }
            var rightsEditPermission = $(gridRows[row]).find('td.tdRightsEditPermitted').html();
            var tdSensitivePermission = $(gridRows[row]).find('td.tdSensitiveInfoPermitted');
            var sensitivePermission = $(tdSensitivePermission).html();
            var tdLostRightsLinear = $(gridRows[row]).find('td.tdLostRightsLnr');
            var lostRightsLinear = $(tdLostRightsLinear).html();
            var tdLostRightsDate = $(gridRows[row]).find('td.tdLostRightsDateLnr');
            var tdLostRightsReason = $(gridRows[row]).find('td.tdLostRightsReasonLnr');
            var tdResourceType = $(gridRows[row]).find('td.tdResourceType');
            var tdReviewStatus = $(gridRows[row]).find('td.tdReviewStatus');
            var tdRightsException = $(gridRows[row]).find('td.tdRightsExceptionLnr');
            var tdTerritorialData = $(gridRows[row]).find('td.tdTerritoryRightsLnr');
            var tdExpiryRule = $(gridRows[row]).find('td.tdRightsExpiryRule');
            var tdRightsType = $(gridRows[row]).find('td.tdRightsTypeLnr');
            var sampleExistsLnrElmt = $(gridRows[row]).find('td.tdSampleExistLinear');
            
            if (rightsEditPermission == "N") {
                disableEditing(tdRightsException);
                disableEditing($(gridRows[row]).find('td.tdPhysicalExpLnr'));
                disableEditing($(gridRows[row]).find('td.tdDigitalExpLnr'));
                disableEditing($(gridRows[row]).find('td.tdDigitalUnbundledLnr'));
                disableEditing($(gridRows[row]).find('td.tdMobileExpLnr'));
                disableEditing($(gridRows[row]).find('td.tdPpbClaimLnr'));
                disableEditing($(gridRows[row]).find('td.tdActiveForMrktLnr'));
                disableEditing($(gridRows[row]).find('td.tdRightsPeriodLnr'));
                disableEditing(tdLostRightsLinear);
                disableEditing(tdLostRightsDate);
                disableEditing(tdLostRightsReason);
                disableEditing(tdTerritorialData);
            }
            var resourceType = $(tdResourceType).html();
            if (sensitivePermission == "N" && lostRightsLinear == "N") {
                disableEditing(tdLostRightsDate);
                disableEditing(tdLostRightsReason);
                $(tdLostRightsDate).html("");
                $(tdLostRightsReason).html("");
                $(tdExpiryRule).html("");
            }
            var image = "";
            var tooltipText = "";
            switch (resourceType.trim()) {
                case "1":
                    image = '<div class=\'resourceAudio\'></div>';
                    tooltipText = 'Audio';
                    break;
                case "2":
                    image = '<div class=\'resourceVideo\'></div>';
                    tooltipText = 'Video';
                    break;
                case "3":
                    image = '<div class=\'resourceImage\'></div>';
                    tooltipText = 'Image';
                    break;
                case "4":
                    image = '<div class=\'resourceMerchandise\'></div>';
                    tooltipText = 'Merchandise';
                    break;
                case "5":
                    image = '<div class=\'resourceText\'></div>';
                    tooltipText = 'Text';
                    break;
                case "6":
                    image = '<div class=\'resourceOthers\'></div>';
                    tooltipText = 'Other';
                    break;
            }
            $(tdResourceType).html(image);
            if (tooltipText != "") {
                $(tdResourceType).attr("title", '~' + tooltipText);
                $(tdResourceType).tooltip({ showBody: "~" });
            }
            var tdError = $(gridRows[row]).find('td.tdError');
            var error = $(tdError).html();
            if (error != "" && error != null) {
                $(tdError).html('<div class=\'errorImageRights\'></div>');
                $(tdError).attr("title", '~' + error);
                $(tdError).tooltip({ showBody: "~" });
            }
            var tdLinkedContract = $(gridRows[row]).find('td.tdLinkedContract');
            var linkedContractTooltip = $(gridRows[row]).find('td.tdLinkedTooltip').text();
            var linkedContract = $(tdLinkedContract).html();
            if (linkedContract != "" && linkedContract != null) {
                var htmlText = '';
                image = '';
                switch (linkedContract.trim()) {
                    case "1":
                        image = '<div class=\'rightslinkedContract\'></div>'; //tdLinkedTooltip
                        htmlText = linkedContractTooltip;
                        break;
                    case "2":
                        image = '<div class=\'rightsSplit\'></div>';
                        htmlText = linkedContractTooltip;
                        break;
                    case "3":
                        image = '<div class=\'rightsMAC\'></div>';
                        htmlText = "Multi Artist Compilation";
                        break;
                    case "4":
                        image = '<div class=\'rightsSAC\'></div>';
                        htmlText = "Single Artist Release";
                        break;
                }
                $(tdLinkedContract).html(image);
                if (htmlText != null && image != '') {
                    $(tdLinkedContract).attr("title", '~' + htmlText);
                    $(tdLinkedContract).tooltip({ showBody: "~" });
                }
            }


            var reviewStatus = $(tdReviewStatus).html();
            var reviewStatusPermission = $(gridRows[row]).find('td.tdReviewStatusPermitted').html();
            var htmlTextReviewStatusLnr = null;
            image = '';
            switch (reviewStatus.trim()) {
                case "NewForReview":
                    htmlTextReviewStatusLnr = "New For Review";
                    if (reviewStatusPermission == "N") {
                        image = '<div class=\'flagBlue\'></div>';
                        $(tdReviewStatus).html(image);
                    } else {
                        $(tdReviewStatus).html("");
                        $(tdReviewStatus).addClass("reviewFlagBlue");
                    }
                    break;
                case "FinalForReview":
                    htmlTextReviewStatusLnr = "Final For Review";
                    if (reviewStatusPermission == "N") {
                        image = '<div class=\'flagOrange\'></div>';
                        $(tdReviewStatus).html(image);
                    } else {
                        $(tdReviewStatus).html("");
                        $(tdReviewStatus).addClass("reviewFlagOrange");
                    }
                    break;
                case "Final":
                    htmlTextReviewStatusLnr = "Final";
                    if (reviewStatusPermission == "N") {
                        image = '<div class=\'flagGreen\'></div>';
                        $(tdReviewStatus).html(image);
                    } else {
                        $(tdReviewStatus).html("");
                        $(tdReviewStatus).addClass("reviewFlagGreen");
                    }
                    break;
            }

            if (htmlTextReviewStatusLnr != null) {
                $(tdReviewStatus).attr("title", '~' + htmlTextReviewStatusLnr);
                $(tdReviewStatus).tooltip({ showBody: "~" });
            }

            var territorialData = $(tdTerritorialData).html();
            if (territorialData.trim() == "" || territorialData == null || territorialData == "null") {
                $(tdTerritorialData).addClass("tdglobePlus");
            }
            else {
                $(tdTerritorialData).attr("title", '~' + territorialData);
                $(tdTerritorialData).tooltip({ showBody: "~" });
            }

            var htmlRightsRuleText = null;

            var expiryRule = $(tdExpiryRule).html();
            image = '';
            if (expiryRule != null && expiryRule.trim() != "") {
                image = '<span class=\'resourceInfoRights\'></span>';
                htmlRightsRuleText = "<b>Rights Expiry Rule: </b>" + expiryRule;
            }
            $(tdExpiryRule).html($(tdExpiryRule).html() + image);
            if (htmlRightsRuleText != null) {
                $(".resourceInfoRights").attr("title", '~' + htmlRightsRuleText);
                $(".resourceInfoRights").tooltip({ showBody: "~" });
            }

            var sampleExistsText = sampleExistsLnrElmt.text();
            var tdSampleDescription = $(gridRows[row]).find('td.tdSampleDescription');
            var sampleDescription = $(tdSampleDescription).text();
            if (sampleExistsText == "Y") {
                if (sampleDescription != "" && sampleDescription != null) {
                    $(sampleExistsLnrElmt).attr("title", '~' + sampleDescription);
                    $(sampleExistsLnrElmt).tooltip({ showBody: "~" });
                }
            }


            var rightsException = $(tdRightsException).html();
            var notes = $(gridRows[row]).find('td.tdNotes').html();
            var htmlRightsExpText = null;
            if (rightsException == 'Applied') {
                if (notes != null && notes != "") {
                    htmlRightsExpText = "<b>Rights Exception Applied: </b>~" + notes;
                } else {
                    htmlRightsExpText = "<b>Rights Exception Applied</b>";
                }
            } else if (rightsException == 'Not Applied') {
                if (notes != null && notes != "") {
                    htmlRightsExpText = "<b>Rights Exception Not Applied: </b>~" + notes;
                } else {
                    htmlRightsExpText = "<b>Rights Exception Not Applied</b>";
                }
            }
            else {
                disableEditing(tdRightsException);
            }
            if (htmlRightsExpText != null) {
                $(tdRightsException).attr("title", '~' + htmlRightsExpText);
                $(tdRightsException).tooltip({ showBody: "~" });
            }
            if (lostRightsLinear == "Y") {
               // $(tdRightsException)[0].style.backgroundColor = "#FFBFBF";
                $(tdRightsType)[0].style.backgroundColor = "#FFBFBF";        
            }

        }
    }
}

