///<reference path="/GCS/Scripts/Custom/RepertoireRightsSearch/TerritorialCountries.js" />
///<reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var sortColumn = "";
var sortType = "";
var excludedCountryIds = "";
var includedCountryIds = "";
var includedCountryArray;
var existingIds;
var preClearancegridName = $('#hdnGridId').val();
$(document).ready(function () {

    /*Hiding the syncfusion toolbar on page load */
    $('.sf-toolbar').hide();
    /*Resize functionality to handle the syncfusion scroll*/
    $(window).resize(function () {
        preClearanceGridScroll();
    });
    resizeAccordion();

    $('.RefreshPager').click(function () {
        $("#prechkAll").removeAttr('checked');
        HideWarningSuccess();
    });
});



function callPreClearanceBulkEditButton() {
    preClearancegridName = $('#hdnGridId').val();
    $('[id*=chkAll]').removeAttr('checked');
    $('[id*=chkAll]').removeAttr("indeterminate");
    includedCountryIds = "";
    includedCountryArray = "";
    var bulkEditRecordlength = $find(preClearancegridName).get_SelectedRecords().length;
    if (bulkEditRecordlength >= 1) {
        $("#" + preClearancegridName + "waiting_WaitingPopup").css({ "display": "block", "top": ($('#' + preClearancegridName).height() / 2), "height": $(window).height() });
        var objBulkEditPreClearanceDialog = $('<div class="divobjBulkEditPreClearancePopUp"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Bulk Edit- Pre-Clearance',
            show: 'clip',
            hide: 'clip',
            width: 850,
            height: 550,
            resizable: false,
            close: function () {
                $(".divobjBulkEditPreClearancePopUp").remove();
                $("#" + preClearancegridName).mousedown();
            }
        });
        objBulkEditPreClearanceDialog.load('/GCS/RightsReviewWorkQueue/PreClearanceBulkEdit', "",
                        function (responseText, textStatus) {
                            if (textStatus == 'error') {
                                objBulkEditPreClearanceDialog.html('<p>' + messageCommon.error + '</p>');
                            }
                        });
        objBulkEditPreClearanceDialog.dialog('open');
    } else {
        ShowWarning('Please select Repertoire');
        preClearanceGridScroll();
    }
}



$(window).bind("resize", resizePopUpHandler);
function resizePopUpHandler(e) {
    if (e == undefined || $(e.target).hasClass('ui-dialog') == false) {

        $('#divTerritoryExclusionPopup').dialog('option', 'width', getPopupWidth(55, 550));
        $('#divTerritoryExclusionPopup').dialog('option', 'height', getPopupHeight(80, 300, 60));
        $('#divTerritoryExclusionPopup').dialog('option', 'position', [getXPosition(55, 0), getYPosition(80, 60)]);
    }
}


/*Preclearance grid action begin*/
function onPreClearanceActionBegin(events, args) {
    //$('#loadingDiv').hide();
    $("[id*='waiting_WaitingPopup']").hide();
    //var num = events._ID.slice(-1);
    var num = events._ID.replace(/[^0-9]/g, '');
    var tabIndex = $("#" + "hiddenCusomTabIndex" + num).val();

    $("#" + events._ID + " .MsgBar").empty();
    $("#" + events._ID + " .MsgBar").text("Search Results ( )");
    $('#hdnGridId').val(events._ID);

    if (num == '' || num == undefined) {
        //UC 18 Part//
        args.data["filterParams"] = JSON.stringify(filterParams);
        if (args.getRequestType() == "sorting") {
            if (args.data["SortDescriptors[0].ColumnName"] == "ISRCId" || args.data["SortDescriptors[0].ColumnName"] == "UPCId") {
                sortColumn = args.data["SortDescriptors[0].ColumnName"];
                if (args.data["SortDescriptors[0].SortDirection"] == "Ascending") {
                    args.data["SortDescriptors[0].SortDirection"] = "Descending";
                    sortType = "DESC";
                } else if (args.data["SortDescriptors[0].SortDirection"] == "Descending") {
                    args.data["SortDescriptors[0].SortDirection"] = "Ascending";
                    sortType = "ASC";
                }
            } else {
                sortType = "Normal";
                sortColumn = "Normal";
            }
        }
    }
    else {
        //UC 19 Part//
        args.data["filterParams"] = JSON.stringify(window.tabCustomParams[num - 2]);
        args.data["tabIndex"] = tabIndex;
    }
}

function onPreClearanceLoad(sender, args) {
    $("#" + sender._ID + "_toolbar").hide();
    $("#" + sender._ID + " .manualPagerLabel:eq(1)").empty();
    $("#" + sender._ID + " .manualPagerLabel:eq(1)").text("Show item per page");
    var totCount = sender.get_TotalRecordsCount();
    $("#" + sender._ID + " .MsgBar").text("Search Results (" + totCount + ")");
    var chk = $("#" + sender._ID + " .GridHeader").find("th.HeaderCell .HeaderCellDiv")[0];
    $(chk).append("<input type=\"Checkbox\" id=\"prechkAll\" onclick=\"preCheckBoxAllClick(event)\"> All");
    $find(sender._ID).get_LocalizedStrings().bulkeditalert = "You have unsaved changes pending. If you refresh the workqueue you will lose these changes. Are you sure you want to proceed?";
    preClearanceStrip(sender._ID);
}


function onPreClearanceCellSave(sender, args) {
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
    
    if (args.colObj.Name == "TerritoryExclusionsLnr") {
        if ($("#divTerritoryExclusionPopup").is(':visible')) {
            args.cancel = true;
            args.preventPropagationClick = true;

        } else {
            sender._currentDataItem.ExcludedCountries = countrysId;
         }
        $("#" + preClearancegridName + "EditForm").find("#TerritoryExclusionsLnr").attr('disabled', 'disabled');
    }

    if (args.colObj.Name == "UPCId") {
        args.value = args.oldValue;
    }

    if (args.colObj.Name == "ISRCId") {
        args.value = args.oldValue;
    }
}

/*On Grid Load(Action Success Of syncFusion)*/
function onPreClearanceGridSuccess(sender, args) {
    syncGridPagerSearchWQ(sender._ID);
    if (args.RequestType == "Save") {
       ShowSuccess(window.preClearanceMessages.saveSuccess);
      //  displayDialog("Success", "&nbsp;&nbsp;&nbsp;" + window.preClearanceMessages.saveSuccess);
       preClearanceGridScroll();
       customMenu();
    }
    var totCount = sender.get_TotalRecordsCount();
    //$("#preClearanceGrid .MsgBar").text("Search Results (" + totCount + ")");
    $("#" + sender._ID).find(".MsgBar").html("<div class='alignLeft' style='margin: 5px 5px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input id='btnPreclearanceBulkEdit' type='button' style='float: left;' value='Bulk Edit' onClick = 'callPreClearanceBulkEditButton()' class='primbutton'><div class='primbutton_right'></div></div>");
    preClearanceGridScroll();
    if (args.RequestType == "sorting") {
        if (sortType == "DESC") {
            if (sortColumn == "ISRCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div><span class='Ascending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            } else if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader'  id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Ascending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
            }
        }
        else if (sortType == "ASC") {
            if (sortColumn == "ISRCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div><span class='Descending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader'  id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            }
            else if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader'  id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Descending'></span>";
            }
        }
        else {
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader'  id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
        }
    }
    else {
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[5].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader'  id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
    }
    setSyncGridToolTip("#" + sender._ID + "_Table");
    preClearanceCheckList();
    $("#" + sender._ID + "_UpcTooltip").tooltip();
    $("#" + sender._ID + "_IsrcTooltip").tooltip();
    ScrollBarMovement(sender._ID);
    var num = sender._ID.replace(/[^0-9]/g, '');
    if (num != '' && num != undefined) {
        $find(sender._ID)._MvcTable.get_parentTable().hideColumnByIndex(15); // UC 19 - Column hiding  
    }

    setImageForPreclearance($('#' + sender._ID + "_Table").find('tr')); 
}


function onPreClearanceCellEdit(sender, args) {
    if (!$("#divTerritoryExclusionPopup").is(':visible')) {
        if (args.colObj.Name == "TerritoryExclusionsLnr") {
            openTerritoryExclusionsPopup();
            var selectedGridRow = $(sender._editTD).parent();
            if ($(selectedGridRow).length == 1) {
                excludedCountryIds = $(selectedGridRow).find('.hiddenExcludedCountrires').html();
                includedCountryIds = $(selectedGridRow).find('.hiddenIncludedCountrires').html();
                includedCountryArray = includedCountryIds.split(',');
            }
            $('#divTerritoryExclusionPopup').mousedown(function (e) {
                e.stopImmediatePropagation();
            });
            $('#divTerritoryExclusionPopup').keypress(function (e) {
                e.stopImmediatePropagation();
            });
            setTimeout(function () {
                var isGreater = false;
                $("#" + sender._gridID + "_Table").find('input:checked').parents('tr').each(function (data) {
                    if ($("#" + sender._gridID + "_Table").find('input:checked').parents('tr').length > 1) {
                        isGreater = true;
                        return false;
                    }
                    if (isGreater)
                        return false;
                });
            }, 1000);
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
            var cellValue = $(sender._editTD).parent().find('td.tdStatus').html(); //$(sender._editTD).parent().find('td')[47].innerHTML;
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
    $("#" + sender._ID + "EditForm").find("#TerritoryExclusionsLnr").attr('disabled', 'disabled');
}


/*Sync Fusion GRid scroll functionality*/
function preClearanceGridScroll() {
    preClearancegridName = $('#hdnGridId').val();
    if ($('#' + preClearancegridName).length > 0) {
        var pagerHgt = $("#" + preClearancegridName + " .GridPager").height();
        var headerHgt = $("#" + preClearancegridName + " .GridHeader").height();

        var browsHgt;
        if ($.browser.msie)
            browsHgt = 16;
        else
            browsHgt = 25;
        var reduceHgt = pagerHgt + headerHgt + browsHgt;

        var jtableTop = $("#" + preClearancegridName).offset().top;
        var topfootPos = $(".footer").offset().top;
        var totRecHeight = $("#" + preClearancegridName + "_Table").height() + reduceHgt;
        var tableHeight = topfootPos - jtableTop;
        var gridObjPre = $find(preClearancegridName);
        if (gridObjPre != null) {
            if (totRecHeight >= tableHeight)
                setPreClearanceGridScrollSyncfusionGridHeight(gridObjPre, tableHeight - reduceHgt);
            else
                setPreClearanceGridScrollSyncfusionGridHeight(gridObjPre, totRecHeight - reduceHgt + 20);
        }
    }
}

function setPreClearanceGridScrollSyncfusionGridHeight(gridObjPre, height) {
    gridObjPre.set_GridHeight(height);
    if (gridObjPre.scroller !== null) {
        gridObjPre.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    }
    gridObjPre.refreshScroller();
}
//sets the image and validation in action succes of the grid
function setImageForPreclearance(gridRows) {
    if ($(gridRows[0]).find('td.EmptyCell').length == 0) {
        for (var row = 0; row < gridRows.length; row++) {

            var htmlTextReviewStatusLnr = null;

            var tdUpcId = $(gridRows[row]).find('td.tdUPCId');
            var tdIsrcId = $(gridRows[row]).find('td.tdISRCId');
            if ($(tdUpcId).html() == null || $(tdUpcId).html() == "") {
                $(tdUpcId).html("");
                disableEditingWithOutClass(tdUpcId);
            }
            if ($(tdIsrcId).html() == null || $(tdIsrcId).html() == "") {
                $(tdIsrcId).html("");
                disableEditingWithOutClass(tdIsrcId);
            }

            var resourceTypeEmnt = $(gridRows[row]).find('td.tdResourceType');
            var errorElmt = $(gridRows[row]).find('td.tdError');
            var reviewStatusElmt = $(gridRows[row]).find('td.tdReviewStatusLnr');
            var linkedContractElmt = $(gridRows[row]).find('td.tdLinkedContract');
            var territorialDataElmt = $(gridRows[row]).find('td.tdTerritorialRights');
            var sampleDescriptionElmt = $(gridRows[row]).find('td.tdSampleDescription');
            var territorialExclusionsElmt = $(gridRows[row]).find('td.tdTerritoryExclusionsLnr');
            var rightsTypeLnrElmt = $(gridRows[row]).find('td.tdRightsTypeLnr');
            var sampleExistsLnrElmt = $(gridRows[row]).find('td.tdSampleExistsLnr');
            var lostRightsLinear = $(gridRows[row]).find('td.tdLostRightsLnr').text();
            var reviewStatusPermission = $(gridRows[row]).find('td.tdReviewStatusPermitted').text();
            var tdIsLostRightsLnr = $(gridRows[row]).find('td.tdIsLostRights').text();
            var rightsEditPermission = $(gridRows[row]).find('td.tdRightsEditPermitted').text();
            var linkedTooltip = $(gridRows[row]).find('td.tdLinkedTooltip').text();
            var rightsTypeLnr = rightsTypeLnrElmt.text();
            var resourceType = resourceTypeEmnt.text();
            var error = errorElmt.text();
            var reviewStatus = reviewStatusElmt.text();
            var linkedContract = linkedContractElmt.text();
            var territorialData = territorialDataElmt.text();
            var sampleDescription = sampleDescriptionElmt.text();
            var territorialExclusions = territorialExclusionsElmt.text();
            var sampleExistsLnrText = sampleExistsLnrElmt.text();


            if (reviewStatusPermission == "N") {
                disableEditing(reviewStatusElmt);
            }

            if (rightsEditPermission == "N") {
                disableEditing($(gridRows[row]).find('td.tdTopPriceCompilationLnr'));
                disableEditing($(gridRows[row]).find('td.tdMidPriceCompilationLnr'));
                disableEditing($(gridRows[row]).find('td.tdBudgetCompilationLnr'));
                disableEditing($(gridRows[row]).find('td.tdDirectMarketingLnr'));
                disableEditing($(gridRows[row]).find('td.tdPremiumLnr'));
                disableEditing($(gridRows[row]).find('td.tdMasterSyncroniseUseLnr'));
                disableEditing($(gridRows[row]).find('td.tdTerritoryExclusionsLnr'));
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
            resourceTypeEmnt.html(image);
            if (tooltipText != "") {
                resourceTypeEmnt.attr("title", '~' + tooltipText);
                resourceTypeEmnt.tooltip({ showBody: "~" });
            }

            if (error != "" && error != null) {
                errorElmt.html('<div class=\'errorImageRights\'></div>');
                errorElmt.attr("title", '~' + error);
                errorElmt.tooltip({ showBody: "~" });
            }

            if (linkedContract != "" && linkedContract != null) {
                var htmlText = '';
                image = '';
                switch (linkedContract.trim()) {
                    case "1":
                        image = '<div class=\'rightslinkedContract\'></div>'; //tdLinkedTooltip
                        htmlText = linkedTooltip;
                        break;
                    case "2":
                        image = '<div class=\'rightsSplit\'></div>';
                        htmlText = linkedTooltip;
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
                linkedContractElmt.html(image);
                if (htmlText != null && image!='') {
                    linkedContractElmt.attr("title", '~' + htmlText);
                    linkedContractElmt.tooltip({ showBody: "~" });
                }
            }
            image = '';
            switch (reviewStatus.trim()) {
                case "NewForReview":
                    htmlTextReviewStatusLnr = "New For Review";
                    if (reviewStatusPermission == "N") {
                        image = '<div class=\'flagBlue\'></div>';
                        reviewStatusElmt.html(image);
                    } else {
                        reviewStatusElmt.html("");
                        reviewStatusElmt.addClass("reviewFlagBlue");
                    }
                    break;
                case "FinalForReview":
                    htmlTextReviewStatusLnr = "Final For Review";
                    if (reviewStatusPermission == "N") {
                        image = '<div class=\'flagOrange\'></div>';
                        reviewStatusElmt.html(image);
                    } else {
                        reviewStatusElmt.html("");
                        reviewStatusElmt.addClass("reviewFlagOrange");
                    }
                    break;
                case "Final":
                    htmlTextReviewStatusLnr = "Final";
                    if (reviewStatusPermission == "N") {
                        image = '<div class=\'flagGreen\'></div>';
                        reviewStatusElmt.html(image);
                    } else {
                        reviewStatusElmt.html("");
                        reviewStatusElmt.addClass("reviewFlagGreen");
                    }
                    break;
            }

            if (htmlTextReviewStatusLnr != null) {
                reviewStatusElmt.attr("title", '~' + htmlTextReviewStatusLnr);
                reviewStatusElmt.tooltip({ showBody: "~" });
            }
            if (territorialData == null || territorialData.trim() == "" || territorialData == "null") {
                territorialDataElmt.html("");
                // territorialDataElmt.addClass("tdglobePlus");
            }

            if (sampleExistsLnrText == "Y") {
                if (sampleDescription != "" && sampleDescription != null) {
                    sampleExistsLnrElmt.attr("title", '~' + sampleDescription);
                    sampleExistsLnrElmt.tooltip({ showBody: "~" });
                }
            }
            if (territorialExclusions == "" || territorialExclusions == null) {
                territorialExclusionsElmt.addClass("exclusionPlus");
            }
            else {
                territorialExclusionsElmt.attr("title", '~' + territorialExclusions);
                territorialExclusionsElmt.tooltip({ showBody: "~" });
            }

            if (tdIsLostRightsLnr == 'Y') {
                rightsTypeLnrElmt[0].style.backgroundColor = "#FFBFBF";  // Light Color When Error Show append "FFE9E8";
                // $(args.Element)[0].style.fontWeight = "bold";
            }
        }
    }
}


function checkIsCountryAlreadyExcluded(check) {
    var checkBoxElement = check;
    if (isCountryAlreadyExcluded(checkBoxElement)) {
        $(checkBoxElement).prop('checked', true);
        createCACTableRow('SelectedList', checkBoxElement);
        return checkBoxElement;
    }
    return checkBoxElement;
}


function isCountryAlreadyExcluded(check) {
    var arrayIds = excludedCountryIds.split(',');
    for (var i = 0; i < arrayIds.length; i++) {
        if (arrayIds[i].trim() == check.id) {
            return true;
        }
    }
    return false;
}

function openTerritoryExclusionsPopup() {
    var objTerritoryExclusionsPopupDialog = $('<div id="divTerritoryExclusionPopup"> </div>')
         .html('<p>Loading...</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Edit Pre-Clearance Exclusions',
            show: 'clip',
            hide: 'clip',
            width: getPopupWidth(55, 550),
            height: getPopupHeight(80, 300, 60),
            minHeight: 300,
            minWidth: 550,
            position: [getXPosition(55, 0), getYPosition(80, 60)],
            resize: function (event, ui) {
                if ($("#divTerritoryExclusionPopup").is(':visible')) {
                    if ($(".cacTableContainer").is(':visible')) {
                        var TotDiaHgt = $("#divTerritoryExclusionPopup").height();
                        var ReduceHgt = TotDiaHgt - 135;
                        $(".cacTable").css('height', ReduceHgt + "px");
                    }
                }
            },
            close: function () {
                $("#divTerritoryExclusionPopup").remove();
                $("#" + preClearancegridName).mousedown();
                $('#loadingDiv').hide(); $('#loaderPanel').hide();
            }
        });

        objTerritoryExclusionsPopupDialog.dialog('open');
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
    if ($("#divTerritoryExclusionPopup").is(':visible')) {
        if ($(".cacTableContainer").is(':visible')) {
            var TotDiaHgt = $("#divTerritoryExclusionPopup").height();
            var ReduceHgt = TotDiaHgt - 135;
            $(".cacTable").css('height', ReduceHgt + "px");
        }
    }
    objTerritoryExclusionsPopupDialog.load('/GCS/RightsReviewWorkQueue/ClearanceAdminCountryPopup');
}

var countrysId;
/* Territory exclusion popup click functionality */
function countryClickFunctions() {
    $('#btnSaveExcludedCountries').click(function () {
        var territoryValues = getClearanceValues();
        countrysId = getClearanceIds();
        if (territoryValues.length > 0) {
            if (territoryValues.length > 255) {
                $("#" + preClearancegridName + "EditForm").find("#TerritoryExclusionsLnr").val(territoryValues.substring(0, 255));
            }
            else {
                $("#" + preClearancegridName + "EditForm").find("#TerritoryExclusionsLnr").val(territoryValues);
            }
            $("#" + preClearancegridName + "EditForm").find("#TerritoryExclusionsLnr").attr('disabled', 'disabled');
            $("#" + preClearancegridName + "EditForm").find("#TerritoryExclusionsLnr").focusout();
            var td = $("#TerritoryExclusionsLnr").parent().parent();
            $(td).parent().find('.hiddenExcludedCountrires').html(getClearanceIds());
            $(td).parent().find('.hiddenExcludedCountrires').addClass('updatedCell');
            $(td).addClass("updatedCell");
            $(td).removeClass("exclusionPlus");
            if (territoryValues.length > 255) {
                $(td).attr('title', territoryValues.substring(0, 255));
            }
            else {
                $(td).attr('title', territoryValues);
            }
            $(td).tooltip();
        }
        $('#divTerritoryExclusionPopup').dialog('close');
    });

    $('#btnCancelExclCountriesSelection').click(function () {
        $('#divTerritoryExclusionPopup').dialog('close');
    });
}


function onGridSaveError(sender, args) {
    var totCount = sender.get_TotalRecordsCount();
    if (args.XMLHttpRequest.responseText != null) {
        var errorVal = args.XMLHttpRequest.responseText.split("<title>")[1].split("</title>")[0];
        if (errorVal.indexOf("No") != -1) {
            //ShowWarning(errorVal);
            displayDialog("Error", "&nbsp;&nbsp;&nbsp;" + errorVal);
            preClearanceGridScroll();
        } else {
           // ShowWarning("Error Retreiving WorkQueue Data");
            displayDialog("Error", "&nbsp;&nbsp;&nbsp;Error Retreiving WorkQueue Data");
            preClearanceGridScroll();
        }

    }
    preClearanceGridScroll();
    $("#" + sender._ID).find(".MsgBar").html("<div class='alignLeft' style='margin: 5px 5px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input id='btnPreclearanceBulkEdit' type='button' style='float: left;' value='Bulk Edit' onClick = 'callPreClearanceBulkEditButton()' class='primbutton'><div class='primbutton_right'></div></div>");
    $('#loaderPanel').hide();
    $('#loadingDiv').hide();
}

function preClearanceCheckList() {
    preClearancegridName = $('#hdnGridId').val();
    setTimeout(function () {
        var gridObjPre = $find(preClearancegridName);
        gridObjPre.set_AllowSelection(true);
        gridObjPre.set_AllowSelection(false);
    }, 0);
    $("#" + preClearancegridName + "_Table").unbind("click");
    $("#" + preClearancegridName + "_Table").bind("click", function (event) {
        event.stopImmediatePropagation();
        HideWarningSuccess();
        var selectedTr = event.target.parentElement;
        var className = event.target.className;
        if (className == "prechkChildClass")
            event.target = event.target.parentNode;
        if (event.target.tagName == "TD") {
            if (className != "prechkChildClass") {
                var checkbox = $(selectedTr).find("#prechkChild")[0];
                $(checkbox).attr("checked", !checkbox.checked);
            }
            synGridCheckBoxSelectionPreClearance(event);
        }
    });
}


function preCheckBoxAllClick(args) {
    preClearancegridName = $('#hdnGridId').val();
    var obj = $("#" + preClearancegridName).find(".GridHeader #prechkAll");
    if (obj.attr("checked") == "checked") {
        $("#" + preClearancegridName + " .GridContent").find('#prechkChild').attr("checked", "checked");
    }
    else {
        $("#" + preClearancegridName + " .GridContent").find('#prechkChild').removeAttr("checked");
    }
    synGridCheckBoxSelectionPreClearance(args);
}

function synGridCheckBoxSelectionPreClearance(events) {
    var curRow, curindex, ckFlag, tagValidchk;
    preClearancegridName = $('#hdnGridId').val();
    var tablePositiontop = $("#" + preClearancegridName + " .GridContent").children("div:first").css("top");
    var tablePositionLeft = $("#" + preClearancegridName + " .GridContent").children("div:first").css("left");
    var vscrollposition = $("#" + preClearancegridName + " .sf-sp-Vhandle").css("top");
    var hscrollposition = $("#" + preClearancegridName + " .sf-sp-Hhandle").css("left");
    var hscrollHeader = $("#" + preClearancegridName + " .GridHeader .Table").css("left");
    var gridObjPre = $find(preClearancegridName);
    var checkboxes = $("#" + preClearancegridName).find(".GridContent #prechkChild");
    if ($.browser.msie) {
        tagValidchk = events.srcElement.outerHTML;
        tagValidchk = $(tagValidchk).attr("id");
    } else
        tagValidchk = events.target.id;

    if (tagValidchk != "prechkAll") {
        curRow = events.target.parentNode;
        curindex = $(curRow).index();
    }
    ckFlag = false;
    $.each(checkboxes, function (index, checkbox) {
        if (checkbox.checked) {
            if (curindex == index)
                ckFlag = true;
            var row = gridObjPre.get_ContentTable().getElementsByTagName("tr")[index];
            if (ckFlag == false) {
                if (($.inArray(index, gridObjPre.get_SelectionManager().selectedRowsCollection)) == -1) {
                    var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                    gridObjPre.get_SelectionManager()._mDownHandler(eve);
                }
            }
            if (ckFlag == true) {
                if (($.inArray(index, gridObjPre.get_SelectionManager().selectedRowsCollection)) == -1) {
                    var curTarget = { target: events.target, ctrlKey: true };
                    gridObjPre.get_SelectionManager()._mDownHandler(curTarget);
                }
            }
            ckFlag = false;
        }
        else
            gridObjPre.get_SelectionManager().deselectRow(index);
    });
    var result = gridObjPre.get_SelectedRecords();
    selectedGridItems = [];

    $.each(result, function (k, value) {

        selectedGridItems.push(
        {
            KeyId: getValue(getKeyValue(value.RepertoireIdLnr)),
            R2KeyId: getR2KeyValue(value.R2ResourceId),
            ContractId: getValue(value.ContractIdLnr),
            LinkType: getValue(linkType),
            RightSetId: getValue(value.RightsIdLnr)
        });
    });

    $("#" + preClearancegridName + " .GridContent").children("div:first").css("top", tablePositiontop);
    $("#" + preClearancegridName + " .GridContent").children("div:first").css("left", tablePositionLeft);
    $("#" + preClearancegridName + " .sf-sp-Vhandle").css("top", vscrollposition);
    $("#" + preClearancegridName + " .sf-sp-Hhandle").css("left", hscrollposition);
    $("#" + preClearancegridName + " .GridHeader .Table").css("left", hscrollHeader);
}


//Apply all click confirmation dialog for Bulk Edit
var pcDialog = $('<div id="divConfirmDialog"></div>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: '',
                    show: 'clip',
                    hide: 'clip',
                    width: 500,
                    resize: false
                });
pcDialog.html("<div style='padding: 10px;'><div class='resourceInfoHeader' style='float:left;margin-top:13px;height:20px;'></div><p>Please confirm that you want to apply this changes to all selected rows visible on the current page</p></div><div style='padding:0px 5px 0px 10px;float:left'><input type='checkbox' id='chkFutureMsg'></div><div style='float:left;margin-top:3px'>Don't show this message in future</div>");

/*To color the column header*/
function preClearanceStrip(gridID) {
    $("#" + gridID + " .HeaderBar th:lt(18)").attr('style', 'background-color: #B8D0E9 !important');
    $("#" + gridID + " .HeaderBar th:gt(18)").attr('style', 'background-color: #ecd9ff !important');
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
