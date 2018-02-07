var searchParams, tabIndex, upc, artist, isExactSearch, releasetitle, adminCompany, contractIds, fromDt, toDt, noRlsDt, queryTypevar,
releaseDigitalBulkEditBtnId, parentRowDatas = new Array(), rowDatas = new Array(), rightSetId = new Array(),
mergeCount = new Array(), childs = new Array(), newArrayDigital = new Array(),
drErrorExist = false, isOtherSelected = false, rightSetArray = [];
var isMac = 0;

//var hiddenGridId;
$(document).ready(function () {
    /*To show loader on popup open*/
//    $('#loadingDiv').hide().ajaxStart(function () {//ui-autocomplete-input ui-autocomplete-loading
//        if (!$("[id*='waiting_WaitingPopup']").is(':visible') && !$(".ui-jtable-loading").is(':visible') && !$(".ui-autocomplete-loading").is(':visible'))
//            $(this).show();
//    }).ajaxStop(function () { $(this).hide(); });

    $("div.colorTabs").css("borderBottom", '3px solid Yellow');
    /****************** Colors for header **************/
    $("#divReleaseDigitalRestrictionsTab .HeaderBar th:lt(16)").attr('style', 'background-color: #B8D0E9 !important');
    $("#divReleaseDigitalRestrictionsTab .HeaderBar th:gt(16)").attr('style', 'background-color: #fcf190 !important');
    /****************** Colors for header **************/

    $('.RefreshPager').click(function () {
        $(".checkboxReleaseDigitalClass").removeAttr('checked');
    });
});




/*Digital Grid Edit*/
function releaseDigitalCellEdit(sender, args) {
    //Linking to UC019
    if (args.colObj.Name == "UPCId") {
        var upcId = args.value;
        if (upcId != "" && upcId != null) {
            window.filterCustomParams = {
                Resource: true,
                Track: true,
                Isrc: '',
                Upc: upcId,
                ArtistName: '',
                IsExactSearch: false,
                Title: '',
                ReleaseTitle: '',
                NoReleaseDate: false,
                TabIndex: 1,
                ClearanceAdminCompany: 0,
                LinkedContract: undefined,
                NewForReview: false,
                FinalForReview: false,
                Final: false,
                SampleExists: false,
                SideArtistExists: false,
                FromDt: '',
                ToDt: ''
            };
            $('#loadingDiv').show();
            setTimeout(function () {
                createCustomResourceTab(window.filterCustomParams);
            }, 10);
        }
        $('#' + $('#hdnGridId').val()).mousedown();
    }

    if (args.colObj.Name == "RestrictonReasonLnr") {
        if (sender._currentDataItem != null) {
            var restriction = sender._currentDataItem.RestrictionLnr;
            if (restriction == "") {
                restriction = validateDigitalRestrictionReason(args, $(sender._editTD).parent().index());
            }
            if (restriction != "" && restriction != null) {
                if (restriction.indexOf('Rights') != -1) {
                    $("#RestrictonReasonLnr").attr("disabled", false);
                } else {
                    $("#RestrictonReasonLnr").attr("disabled", true);
                }
            }
            isOtherSelected = false;
            $('#RestrictonReasonLnr').change(function () {
                if ($(this).find(':selected').text() == "Other") {
                    var formElement = "<form id='" + $('#hdnGridId').val() + "EditForm'><input id='RestrictonReasonLnr' type='text' value='' name='RestrictonReasonLnr'><span class='field-validation-valid' data-valmsg-replace='true' data-valmsg-or='NotesLnr'></span></form>";
                    $(sender._editTD).html(formElement);
                    $('#RestrictonReasonLnr').focus();
                    isOtherSelected = true;
                }
            });
        } else {
            var rowIndexRestrictionReason = $(sender._editTD).parent().index();
            var restrictionNew = validateDigitalRestrictionReason(args, rowIndexRestrictionReason);
            if (restrictionNew != "") {
                if (restrictionNew.indexOf('Rights') != -1) {
                    $("#RestrictonReasonLnr").attr("disabled", false);
                } else {
                    $("#RestrictonReasonLnr").attr("disabled", true);
                }
            }
            $('#RestrictonReasonLnr').change(function () {
                if ($(this).find(':selected').text() == "Other") {
                    var formElement = "<form id='" + $('#hdnGridId').val() + "EditForm'><input id='RestrictonReasonLnr' type='text' value='' name='RestrictonReasonLnr'><span class='field-validation-valid' data-valmsg-replace='true' data-valmsg-or='NotesLnr'></span></form>";
                    $(sender._editTD).html(formElement);
                    $('#RestrictonReasonLnr').focus();
                    isOtherSelected = true;
                }
            });
        }
    }

    if (args.colObj.Name == "ConsentPeriodLnr") {
        var rowIndex = $(sender._editTD).parent().index();
        var restrictionType = validateReleaseDitgitalRestrictionOnCellSave(args, rowIndex);
        if (restrictionType != undefined && restrictionType != "") {
            if (restrictionType.indexOf("No") != -1 || restrictionType.indexOf("Refer") != -1) {
                $('#ConsentPeriodLnr').attr("disabled", true);
            } else {
                $('#ConsentPeriodLnr').attr("disabled", false);
            }
        }
    }

}

/*Digital Grid Cell Save*/
function releaseDigitalCellSave(sender, args) {
    HideWarningSuccess();
//    if (args.colObj.Name == "RestrictonReason") {
//        var restrictionNew = sender._currentDataItem.Restriction;
//        if (restrictionNew != "" && restrictionNew != null) {
//            if (restrictionNew.indexOf('Rights') != -1) {
//                $(sender._editTD).attr("disabled", false);
//            } else {
//                $(sender._editTD).attr("disabled", true);
//                args.value = "";
//            }
//        }
//    }
    if (args.colObj.Name == "RestrictonReasonLnr") {
        if (sender._currentDataItem != null) {
            var restrictionNewe = sender._currentDataItem.RestrictionLnr;
            if (restrictionNewe=="") {
                 restrictionNewe = validateDigitalRestrictionReason(args, $(sender._editTD).parent().index());
            }
            if (restrictionNewe != "" && restrictionNewe != null) {
                if (restrictionNewe.indexOf('Rights') != -1) {
                    if (args.value == args.oldValue && args.oldValue == "Other") {
                        args.value = sender._currentDataItem.RestrictionReasonForOthers;
                    }
                    $(sender._editTD).attr("disabled", false);
                } else {
                    $(sender._editTD).attr("disabled", true);
                    args.value = "";
                }
            }
            if (isOtherSelected) {
                sender._currentDataItem.RestrictionReasonForOthers = args.value;
                isOtherSelected = false;
            }
        } else {
            var restrictionNew = validateDigitalRestrictionReason(args, $(sender._editTD).parent().index());
            if (restrictionNew != "" && restrictionNew != null) {
                if (restrictionNew.indexOf('Rights') != -1) {
                    if (args.value == args.oldValue && args.oldValue == "Other") {
                        args.value = restrictionNew;
                    }
                    $(sender._editTD).attr("disabled", false);
                } else {
                    $(sender._editTD).attr("disabled", true);
                    args.value = "";
                }
            }
        }

    }

    if (args.colObj.Name == "RestrictionLnr") {
        if (args.value.indexOf("No") == 0 || args.value.indexOf("Refer") == 0) {
            $('#ConsentPeriodLnr').attr("disabled", true);
            $(sender._editTD.nextSibling).next().html("");
            if (sender._currentDataItem != null) {
                sender._currentDataItem.RestrictionLnr = "";
            }
        }
        else {
            $('#ConsentPeriodLnr').attr("disabled", false);
        }
        if (args.value.indexOf("Rights") != -1) {
            $(sender._editTD.nextSibling).attr("disabled", false);
        } 
        else {
            $(sender._editTD.nextSibling).html("");
            if (sender._currentDataItem != null) {
                sender._currentDataItem.RestrictonReasonLnr = "";
            }
            $(sender._editTD.nextSibling).attr("disabled", true);
        }
    }

    if (args.colObj.Name == "ConsentPeriodLnr") {
        var rowIndex = $(sender._editTD).parent().index();
        var restriction = validateReleaseDitgitalRestrictionOnCellSave(args, rowIndex);
        if (restriction != undefined && restriction != "") {
            if (restriction.indexOf("No") != -1 || restriction.indexOf("Refer") != -1) {
                $('#ConsentPeriodLnr').attr("disabled", true);
                args.value = "";
            } else {
                $('#ConsentPeriodLnr').attr("disabled", false);
                args.value = args.value;
            }
        }
    }

    if (args.colObj.Name == "UPCId") {
        args.value = args.oldValue;
    }
    
}

function onReleaseDigitalError(sender, args) {
    var grid_Id = sender._ID;
    if (args.XMLHttpRequest.responseText != null) {
        var errorVal = args.XMLHttpRequest.responseText.split("<title>")[1].split("</title>")[0];
        //ShowWarning(errorVal);
        displayDialog("Error", "&nbsp;&nbsp;&nbsp;Error Retreiving WorkQueue Data");
    }
    SyncfusionGridScroll(grid_Id);
    $("#" + grid_Id).find(".MsgBar").html("<div class='alignLeft' style='margin: 3px 15px 0 -2px;'>Search Results (0)</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input id=" + releaseDigitalBulkEditBtnId + " type='button' style='float: left;' value='Bulk Edit' class='primbutton'><div class='primbutton_right'></div></div>");
    //rightsAcquiredGridScroll();
    callBulkEditReleaseDigitalButton();
    $('#loaderPanel').hide();
    $('#loadingDiv').hide();
}

/*returns the restriction type of the selected row*/
function validateReleaseDitgitalRestrictionOnCellSave(args, rowIndex) {
    var restriction = "";
    $($('#' + $('#hdnRelDigGridId').val() + '_Table').find('tr')[rowIndex]).find('td').each(function () {
        if ($(this).index() == 19) {
            restriction = $(this).html();
            return restriction;
        }
    });
    return restriction;
}






/*Check For Duplication of UseTypeLnr and Commercial model Combination*/
function inValidCombination(childData) {
    var inValid = false;
    for (var i = 0; i < childs.length; i++) {
        var isUniqueDownloadErrorAppended = false;
        var isUniqueStreamingErrorAppended = false;
        newArrayDigital = new Array();
        var data = childData[i];
        var childDatas = data.Value;
        var aryDigital = new Array();
        var childRows;        
        if (childData[i].isUpdated) {//If updated alone
            for (var j = 0; j < childDatas.length; j++) {
                var isErrorOccured = false;
                var newRow = childDatas[j];
                var splitRow = newRow.split('^');
                var useType = splitRow[0].trim();
                var commercialModel = splitRow[1].trim();
                var restriction = splitRow[2].trim();
                var restrictionReason = splitRow[3].trim();
                var consentPeriod = splitRow[4].trim();
                var notes = splitRow[5].trim();


                if ((useType == "" && commercialModel != "") || (useType != "" && commercialModel == "") || (useType != "" && commercialModel != "" && restriction == "") || (useType == "" && commercialModel == "" && (restriction != "" || consentPeriod != "" || restrictionReason != "" || notes != ""))) { //Validation to check is the DR is complete
                    isErrorOccured = true;
                   appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalIncompleteValidation);
                }

               if (!isErrorOccured && ((useType == "" && commercialModel == "") && childDatas.length > 1 && childDatas[j].split('^')[7].trim() == 0)) { //Validation to check is the DR is complete                
                   isErrorOccured = true;
                    appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalIncompleteValidation);
                }

                if ( !isErrorOccured && (useType == digitalMasterData.useTypeDownload && commercialModel == digitalMasterData.commercialModelAdFunded)) { //Validation to check is the DR is having invalid combination
                    isErrorOccured = true;
                    appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalValidCombinationValidation);
                }

                if (!isErrorOccured && restriction != "" && consentPeriod == "") { //Validation to check is the DR is incomplete with consentPeriod
                    if (restriction == digitalMasterData.restrictionTypeConsult || restriction.indexOf(digitalMasterData.restrictionTypeConsent) == 0) {
                        isErrorOccured = true;
                        appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalConsentPeriodValidation + (parseInt(j) + 1), childRows);
                    }
                }

                if (!isErrorOccured && useType != "" && commercialModel != "") { //Validation to check is the DR is having duplicate entries
                    aryDigital.push(useType + commercialModel);
                    var isDuplicate = checkDrAlreadyExists(aryDigital);
                    if (isDuplicate != false && isDuplicate > 0) {
                        isErrorOccured = true;
                        appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalDuplicateCombinationValidation);
                    }
                }

                if (!isErrorOccured && useType != "" && commercialModel != "") {
                    var digitalObject = new Object();
                    digitalObject.CommercialModel = new Array();

                    if (!fillUseTypeAndCommercial(useType, commercialModel, (parseInt(j)))) {
                        digitalObject.UseType = useType;
                        digitalObject.CommercialModel.push(commercialModel + "^" + (parseInt(j)));
                        newArrayDigital.push(digitalObject);
                    }

                    var isUniqueCombination = checkDigitalUniqueCombination(useType);
                    if (isUniqueCombination.toString() != "true") {//Validation for All combination
                        if (useType == digitalMasterData.useTypeDownload && !isUniqueDownloadErrorAppended) {
                            isUniqueDownloadErrorAppended = true;
                            var rowId = parseInt(splitRow[splitRow.length - 1]) - isUniqueCombination;
                            if (isUniqueCombination == "false" || isUniqueCombination < 0)
                                rowId = splitRow[splitRow.length - 1];

                            isErrorOccured = true;
                            appendErrorToGrid(rowId.toString(), digitalGridMessages.digitalUniqueCombinationValidation);
                        } if (useType == digitalMasterData.useTypeStreaming && !isUniqueStreamingErrorAppended) {
                            isUniqueStreamingErrorAppended = true;
                            var rowIdNew = parseInt(splitRow[splitRow.length - 1]) - isUniqueCombination;
                            if (isUniqueCombination == "false" || isUniqueCombination < 0)
                                rowIdNew = splitRow[splitRow.length - 1];

                            isErrorOccured = true;
                            appendErrorToGrid(rowIdNew.toString(), digitalGridMessages.digitalUniqueCombinationValidation);
                        }
                    }
                }

               

                if (!isErrorOccured && restriction.indexOf(digitalMasterData.restrictionTypeRights) != -1 && restrictionReason == "") {//Reason Mandatory for No Rights                    
                    appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalRestrictionReasonValidation);
                }
            }
        }
    }
    return inValid;
}

/*Validations For Digital RestrictionLnr Section Ends*/



/*SyncFusion Grid Code*/
function onRelDigActionBegin(sender, args) {
    HideWarningSuccess();
    $("#hdnRelDigGridId").val(sender._ID);
    $('#hdnGridId').val(sender._ID);
    $("[id*='waiting_WaitingPopup']").hide();
    //$('#loadingDiv').hide();
    var num = sender._ID.replace(/[^0-9]/g, '');
    searchParams = $("#" + "hiddenSearchParams" + num).val();
    
    if (searchParams != undefined ) {
        var splitValues = searchParams.split(',');
        var searchContractIds = '';
        if (splitValues[6] != undefined) {
            searchContractIds = splitValues[6].replace(/-/g, ',');
        }
        args.data["tabIndex"] = splitValues[0];
        args.data["upc"] = splitValues[1];
        args.data["artist"] = splitValues[2];
        args.data["isExactSearch"] = splitValues[3];
        args.data["releaseTitle"] = splitValues[4];
        args.data["adminCompany"] = splitValues[5];
        args.data["contractIds"] = searchContractIds;
        args.data["fromDt"] = splitValues[7];
        args.data["toDt"] = splitValues[8];
        args.data["noRlsDt"] = splitValues[9];
        args.data["queryType"] = splitValues[10];

        if (args.data["queryType"] == '1') {
            isMac = 1;
        }

        args.data["isDynamic"] = true;
        if (splitValues[11] != "")
            args.data["linkIsrc"] = splitValues[11];
        if (splitValues[12] != "")
            args.data["linkUpc"] = splitValues[12];
        if (splitValues[13] != "")
            args.data["isLinkNavigation"] = splitValues[13] == "true";
        if(searchParams=="")
            args.data["isDynamic"] = false;
    } else {
        args.data["tabIndex"] = null;
        args.data["upc"] = null;
        args.data["artist"] = null;
        args.data["isExactSearch"] = null;
        args.data["releaseTitle"] = null;
        args.data["adminCompany"] = null;
        args.data["contractIds"] = null;
        args.data["fromDt"] = null;
        args.data["toDt"] = null;
        args.data["noRlsDt"] = null;
        args.data["queryType"] = null;
        args.data["isDynamic"] = false;
        args.data["linkIsrc"] = null;
        args.data["linkUpc"] = null;
        args.data["isLinkNavigation"] = true;
    }
}

/*Merge Parent and Child Rows */
function RelDigMergeRow(sender, args) {
    if (args.GridCell.Column.Member == "Actions" || args.GridCell.Column.Member == "UseTypeLnr" || args.GridCell.Column.Member == "CommercialModelsLnr" || args.GridCell.Column.Member == "RestrictionLnr" || args.GridCell.Column.Member == "RestrictonReasonLnr" || args.GridCell.Column.Member == "ConsentPeriodLnr" || args.GridCell.Column.Member == "NotesLnr") {
        if (args.GridCell.Data.RightsEditPermitted == "N") {
            disableEditing(args.GridCell.Element);
        }
    }
    if (args.GridCell.Column.Member == "UPCId") {
        args.GridCell.Data.UPCId == null ? args.GridCell.Data.UPCId = "" : args.GridCell.Data.UPCId = args.GridCell.Data.UPCId;
    }
    if (args.GridCell.Column.Member == "MergeCount") {
        if (args.GridCell.Text.toString() != "0") {
            args.SetRange(parseInt(args.GridCell.Text.toString()), 1);
        }
    }
    if (args.GridCell.Column.Member == "RestrictonReasonLnr") {
        if (args.GridCell.Data.RestrictionReasonForOthers != "" && args.GridCell.Data.RestrictionReasonForOthers != null && args.GridCell.Data.RestrictionReasonForOthers != "null") {
            $(args.GridCell.Element).html(args.GridCell.Data.RestrictionReasonForOthers);
        }
    }
    var chkAllColumn = "False";
    if (args.GridCell.Column.Member == " ") {
        var chkInput = args.GridCell.Element.children;
        var checkboxid = $(chkInput)[0].getAttribute("id");
        if (checkboxid == "checkboxReleaseDigitalchild") {
            ReleasedisableEditingCheck(args.GridCell.Element);
            chkAllColumn = "True";
        }
    }
    if (chkAllColumn == "True" || args.GridCell.Column.Member == "RightSetIdLnr" || args.GridCell.Column.Member == "ReleaseType" || args.GridCell.Column.Member == "RightsSourceId" || args.GridCell.Column.Member == "UPCId" || args.GridCell.Column.Member == "Artist" || args.GridCell.Column.Member == "Title" || args.GridCell.Column.Member == "VersionTitle" || args.GridCell.Column.Member == "ReleaseDate" || args.GridCell.Column.Member == "Configration" || args.GridCell.Column.Member == "IsSplitDeal" || args.GridCell.Column.Member == "AdminCompany" || args.GridCell.Column.Member == "TerritorialRights") {
        if (args.GridCell.Data.releaseMergeCount != "0") {
            args.SetRange(parseInt(args.GridCell.Data.MergeCount), 1);
        }
    }
}

function RelDigActionSuccess(sender, args) {
    syncGridPagerSearchWQ(sender._ID);
    $('#warning').hide();
    var grid_Id = sender._ID;
    SyncfusionGridScrollRelDig(grid_Id);
    var totCount = sender.get_TotalRecordsCount();
    $("#" + grid_Id + " .MsgBar").empty();
    $("#" + grid_Id + " .MsgBar").text("Search Results (" + totCount + ")");

    if (args.RequestType == "Save") {
        ShowSuccess(window.ReleaseCustomsSearchConstants.ReleaseRightsSuccessMessages);
        //displayDialog("Success", "&nbsp;&nbsp;&nbsp;" + window.ReleaseCustomsSearchConstants.ReleaseRightsSuccessMessages);
        SyncfusionGridScrollRelDig(grid_Id);
        customMenu();
    }
    
    setSyncGridToolTip("#" + grid_Id + "_Table");
    $("#jqgrid .manualPagerLabel:eq(1)").empty();
    $("#jqgrid .manualPagerLabel:eq(1)").text("Show item per page");

    if (args.RequestType == "sorting") {
        if (sortType == "DESC") {
            if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Ascending'></span>";
            }
        }
        else if (sortType == "ASC") {
            if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Descending'></span>";
            }
        }
        else {
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a release WQ for that UPC.'></div>";
        }
    }
    else {
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[6].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a release WQ for that UPC.'></div>";
    }
    $(".resourceInfoHeader").tooltip();
    //bulk edit
    releaseDigitalBulkEditBtnId = "bulkEditReleaseDigital" + grid_Id.replace(/[^0-9]/g, '');
    $("#" + grid_Id).find(".MsgBar").html("<div class='alignLeft' style='margin: 3px 15px 0 -2px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input type='button' id=" + releaseDigitalBulkEditBtnId + " style='float: left;' value='Bulk Edit' class='primbutton'><div class='primbutton_right'></div></div>");
    ReleasedigitalRestrictionsCheckList(); 
    callBulkEditReleaseDigitalButton();
    ScrollBarMovement(sender._ID);
    AddedLastRowCell(sender, 14);
}

function SyncfusionGridScrollRelDig(grid_Id) {
    var pagerHgt = $("#" + grid_Id + " .GridPager").height();
    var headerHgt = $("#" + grid_Id + " .GridHeader").height();
    var browsHgt = 0;
    if ($.browser.msie)
        browsHgt = 16;
    else
        browsHgt = 25;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;
    var sTable = "#" + grid_Id;
    var jtableTop = $("" + sTable + "").offset().top;
    var topfootPos = $(".footer").offset().top;
    var totRecHeight = $("#" + grid_Id + "_Table").height() + reduceHgt;
    var tableHeight = topfootPos - jtableTop;
    var gridObj = $find(grid_Id);
    if (totRecHeight >= tableHeight)
        setSyncfusionGridHeightRelDig(gridObj, tableHeight - reduceHgt);
    else
        setSyncfusionGridHeightRelDig(gridObj, totRecHeight - reduceHgt + 20);
    if($.browser.mozilla) {
        $("#" + grid_Id + " .sf-sp-vscroller").css("display", "block");
    }
}

function setSyncfusionGridHeightRelDig(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}

function RelDigonLoad(sender, args) {
    var grid_Id = sender._ID;
   
    $("#" + grid_Id + "_toolbar").hide();
    var totCount = sender.get_TotalRecordsCount();
    $('#warning').hide();
    $("#" + grid_Id + " .MsgBar").empty();
    $("#" + grid_Id + " .MsgBar").text("Search Results (" + totCount + ")");
    $("#" + grid_Id + " .manualPagerLabel:eq(1)").empty();
    $("#" + grid_Id + " .manualPagerLabel:eq(1)").text("Show item per page");
    //add check box all
    var chk = $("#" + grid_Id + " .GridHeader").find("th.HeaderCell .HeaderCellDiv")[2];
    $(chk).append("<input type=\"Checkbox\" id=\"checkboxAllRelDig\" onclick=\"ReleaseDigitalCheckBoxAllClick(event)\"> All");

}

function checkImageForReleaeDigitalRights(events, args) {
    var image = '';
    var title = '';
    if (args.Data.ReleaseType == 'PR') {
        image = '<img  src="/GCS/Images/Physical_Release.gif">';
        title = "Physical Release";
    } else if (args.Data.ReleaseType == 'DR') {
        image = '<img  src="/GCS/Images/Digital_Release.gif">';
        title = "Digital Release";
    } else if (args.Data.ReleaseType == 'PL') {
        image = '<img  src="/GCS/Images/Physical_Link.png">';
        title = "Physical Link";
    } else if (args.Data.ReleaseType == 'DL') {
        image = '<img  src="/GCS/Images/digital_link_create.gif">';
        title = "Digital Link";
    } else if (args.Data.ReleaseType == 'DP') {
        image = '<img  src="/GCS/Images/package_digital.gif">';
        title = "Digital Package";
    } else if (args.Data.ReleaseType == 'PP') {
        image = '<img  src="/GCS/Images/package_physical.gif">';
        title = "Physical Package";
    } else if (args.Data.ReleaseType == 'PDL') {
        image = '<img  src="/GCS/Images/package_digital_link.gif">';
        title = "Digital Package (Linked)";
    } else if (args.Data.ReleaseType == 'PPL') {
        image = '<img  src="/GCS/Images/package_physical_link.gif">';
        title = "Physical Package (Linked)";
    }
    if (title != '') {
        $(args.Element.children[4]).attr("title", '~' + title);
        $(args.Element.children[4]).tooltip({ showBody: "~" });
    }
    var tdReleaseType = args.Element.children[4];
    $(tdReleaseType).html(image);

    title = '';
    image = '';
    if (args.Data.RightsSourceId == '0') {
        image = '';
    }
    else if (args.Data.RightsSourceId == '1') {
        image = '<div class="rightsSourceIconComplete"></div>';
        title = "Roll-Up-Complete";
    }
    else if (args.Data.RightsSourceId == '2') {
        image = '<div class="rightsSourceIconPartial"></div>';
        title = "Roll-Up-Partial(Missing Rights)";
    }
    else if (args.Data.RightsSourceId == '3') {
        image = '<div class="rightsSourceIconPartial"></div>';
        title = "Roll-Up-Partial(Complex Rights)";
    }
    else if (args.Data.RightsSourceId == '4') {
        image = '<div class="rightsSourceIconPartial"></div>';
        title = "Roll-Up-Partial(Missing & Complex Rights)";
    }
    else if (args.Data.RightsSourceId == '5') {
        image = '<div class="rightsSourceIconComplete"></div>';
        title = "By User";
    }
    else if (args.Data.RightsSourceId == '6') {
        image = '<div class="rightsSourceIconContract"></div>';
        title = "Contract";
    }
    else if (args.Data.RightsSourceId == '7') {
        image = '<div class="rightsSourceIconComplete"></div>';
        title = "Clearance";
    }
    else if (args.Data.RightsSourceId == '8') {
        image = '';
        title = "Notification";
    }
    if (title != '') {
        $(args.Element.children[5]).attr("title", '~' + title);
        $(args.Element.children[5]).tooltip({ showBody: "~" });
    }
    var tdRightsSourceId = args.Element.children[5];

    var isMediaPortalData = 0;
    if (args.Data.IsEditableRight == '0') {
        isMediaPortalData = 1;
    }
    

    $(tdRightsSourceId).html(image);

    if (args.Data.LinkedTooltip != null) {
        var htmlText = '';
        if (args.Data.ContractId != 0) {
            if (args.Data.IsSplitDeal == true) {
                image = '<img  src="/GCS/Images/split.png">';
                if (args.Data.LinkedTooltip != '') {
                    htmlText = args.Data.LinkedTooltip;
                }
            }
            else {
                image = '<img  src="/GCS/Images/linked_Contract.png">';
                if (args.Data.LinkedTooltip != '') {
                    htmlText = args.Data.LinkedTooltip;
                }
            }
        }

        else if (args.Data.ContractId == 0 || isMac == "1" && isMediaPortalData == 0) {
            image = '<img  src="/GCS/Images/MAC_Img.png">';
            htmlText = "Multi Artist Compilation";
        }   
    

        
        if (htmlText != '') {
            $(args.Element.children[12]).attr("title", '~' + htmlText);
            $(args.Element.children[12]).tooltip({ showBody: "~" });
        }
        var tdIsSplitDeal = args.Element.children[12];
        if (args.Data.ContractId != 0) {            
            $(tdIsSplitDeal).html(image);
        }
        else if (args.Data.ContractId == 0 || isMac == '1') {
            $(tdIsSplitDeal).html(image);
        }
        else {
            $(tdIsSplitDeal).html('');
        }

        image = '';
        var htmlError = null;
        if (args.Data.Error != null && args.Data.Error != '') {
            if (args.Data.Error != null) {
                image = '<div class=\'errorImageRights\'></div>';
                htmlError = args.Data.Error;
            }
        }
        var tdError = args.Element.children[16];
        $(tdError).html(image);
        if (htmlError != null && htmlError != '') {
            $(tdError).attr("title", '~' + htmlError);
            $(tdError).tooltip({ showBody: "~" });
        }   

    }

    var tdUpc = args.Element.children[6];
    if (args.Data.LostRightsLnr == "True") {
        $(tdUpc)[0].style.backgroundColor = "#FFBFBF";  // Light Color When Error Show append "FFE9E8";
        $(tdUpc)[0].style.fontWeight = "bold";
    }
}


/**********  Plus Functionality **********/
function ReleaseDigPlusClick1(element) {
    $('#loadingDiv').show();
    setTimeout(function() {

        var relDigGridId = $("#hdnRelDigGridId").val();
        var tablePositiontop = $("#" + relDigGridId + " .GridContent").children("div:first").css("top");
        var tablePositionLeft = $("#" + relDigGridId + " .GridContent").children("div:first").css("left");
        var vscrollposition = $("#" + relDigGridId + " .sf-sp-Vhandle").css("top");
        var hscrollposition = $("#" + relDigGridId + " .sf-sp-Hhandle").css("left");
        var hscrollHeader = $("#" + relDigGridId + " .GridHeader .Table").css("left");

        if ($(element.parentNode).hasClass('disabledElement')) {
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return false;
        }
        var clickrow = element.parentNode.parentNode;
        var rowindex = $(clickrow).index();
        $find(relDigGridId).jsonModeMgr._AddAction();

        var newinsertTr = $("#" + relDigGridId + "_Table").find("tr")[0];
        // if first row is disabled while adding new row , the new row also disabled so we are removed disabledElement class
        $(newinsertTr).find('.disabledElement').removeClass('disabledElement');

        var addedRecords = $find(relDigGridId)._edit._addedRecords;
        if (addedRecords.length > -1) {
            addedRecords[addedRecords.length - 1].RightsEditPermitted = 'Y';
        }
        $(newinsertTr).children("td.RightsEditPermitted").html('Y');

        var td = $(newinsertTr).children("td.RowCell")[0];
        var righSetVal = GetRightSetID(rowindex + 1, relDigGridId);
        $(td).find("#RightSetIdLnr").val(righSetVal);

        $("#" + $("#hdnRelDigGridId").val()).mousedown();
        ReleaseInsertedNewDataUpdate(clickrow, newinsertTr);
        var reltomodDate = $(newinsertTr).children("td.RowCell")[23];
        $(reltomodDate).click();
        var modifiedDate = DigReleaseGetModifiedDate(rowindex + 1, relDigGridId);
        $(reltomodDate).find("#ModifiedDateTimeLnr").val(modifiedDate);
        $("#" + $("#hdnRelDigGridId").val()).mousedown();
        /* Removed updated cell red color flag for parent row - start*/
        var delUpdatedCells = $(newinsertTr).find("td.RowCell").slice(0, 14);
        $(delUpdatedCells).removeClass("updatedCell");
        /*end*/
        var toHideMergeTd = $("#" + relDigGridId + "_Table").find('tr:eq(0)').children("td.RowCell").slice(0, 14);
        $(toHideMergeTd).addClass("Merged");
        var toRowindex = $(newinsertTr).index();
        moveRow(toRowindex, rowindex, relDigGridId);
        AddandDeleteRow(rowindex, "Add", relDigGridId);
        SyncfusionGridScrollRelDig(relDigGridId);

        var totRows = $("#" + relDigGridId + "_Table").find("tr");
        if (rowindex + 1 == totRows.length - 1) {
            var lastrowIndex = rowindex + 1;
            var lastTablerow = $("#" + relDigGridId + "_Table").find('tr')[lastrowIndex];
            $(lastTablerow).find("td").addClass("LastRowCell");
        }

        $("#" + relDigGridId + " .GridContent").children("div:first").css("top", tablePositiontop);
        $("#" + relDigGridId + " .GridContent").children("div:first").css("left", tablePositionLeft);
        $("#" + relDigGridId + " .sf-sp-Vhandle").css("top", vscrollposition);
        $("#" + relDigGridId + " .sf-sp-Hhandle").css("left", hscrollposition);
        $("#" + relDigGridId + " .GridHeader .Table").css("left", hscrollHeader);
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
    }, 50);
}
function ReleaseDigPlusClick(element) {

    if ($(element.parentNode).hasClass('disabledElement')) {
        return false;
    }
    var clickrow = element.parentNode.parentNode;
    var rowindex = $(clickrow).index();
    var row = $(element).parents('tr');
    AddNewRowRelease(rowindex, row);
    var relDigGridId = $("#hdnRelDigGridId").val();
    SyncfusionGridScrollRelDig(relDigGridId);
}
//this function return TR which is visible in the UI for the resource/track/release
function getParentRowRelease(rowindex, flag) {

    // Get Grid Id from hidden input box
    var digRestGridId = $("#hdnGridId").val();
    var tables = $('#' + digRestGridId + '_Table tr');
    // Get Grid object
    var gridObj = $find(digRestGridId);
    for (var index = rowindex - 1; index >= 0; index--) {
        var row = tables[index];
        var isChildRow = $(row).children("td").hasClass('Merged');
        //var isRowVisible $($(row)[2]).has(":visible");
        if (!isChildRow)
            return row;

    }
}

// to verify dynamic id appending
function AddNewRowRelease(rowindex, trow) {
    //set mergedcolumnindex
    var mergeColumnIndex = 14;
    // Get Grid Id from hidden input box
    var digRestGridId = $("#hdnGridId").val();
    // Get Grid object
    var gridObj = $find(digRestGridId);

    var gridDatas = gridObj._edit._data;
    // Get selected row  right set id 
    var rightSetid = $(trow).find('.RightSetId').html();
    // declare object
    var newObject = null;
    // get current row object based on right set id
    $.each(gridDatas, function (k, item) {
        if (item.RightSetIdLnr == rightSetid) {
            newObject = JSON.parse(JSON.stringify(item));
            return true;
        }
    });
    // Is Current (plus button click) row child, ture
    // else false
    var isChildRow = $(trow).children("td").hasClass('Merged');
    if (isChildRow) {
        // Get Current row index
        var index = $(trow).index();
        //Get parent row
        trow = getParentRowRelease(index, isChildRow)

    }
    // Clone current click tr
    var newinsertTr = $(trow).clone();
    ///Remove class attribute
    $(newinsertTr).removeAttr("class");
    /// Add calss attribute 
    $(newinsertTr).addClass("InsertedRow");
    ///Get old cell span 
    var oldRowSpan = $(trow).children("td.mergeCount").html();
    if (parseInt(oldRowSpan) == 0)
        oldRowSpan = "1";
    //Increment new row span
    var newRowSpan = parseInt(oldRowSpan) + 1;
    /// Increament(1)  RowSpan for parent row
    $(trow).children("td.RowCell").slice(0, mergeColumnIndex).attr("rowspan", newRowSpan);
    ///Increment merge cell count to Parent row 
    var tdcell = $(trow).children("td.RowCell")[1];
    // increment merged value to parent row element.
    $(tdcell).text(newRowSpan);
    ///Remove class attribute
    $(newinsertTr).removeAttr("class");
    /// Add calss attribute 
    $(newinsertTr).addClass("InsertedRow");
    /// set 0 for merge cell to the inserted new row
    var tdcell = $(newinsertTr).children("td.RowCell")[1];
    // set merged value as 0 for newly added child row, 
    //only parent row have merged value.    
    $(tdcell).html('0');
    ///Removed Span for newly add row
    $(newinsertTr).children("td.RowCell").slice(0, mergeColumnIndex).removeAttr("rowspan");
    /// set class merged for newly add row for hide this left table cell  
    $(newinsertTr).children("td.RowCell").slice(0, mergeColumnIndex).addClass("Merged");
    // in the cloned Tr, set null value for corresponding fields  
    $(newinsertTr).children(".UseTypes").html(' ');
    $(newinsertTr).children(".CommercialModel").html(' ');
    $(newinsertTr).children(".Restriction").html(' ');
    $(newinsertTr).children(".RestrictionReason").html(' ');
    $(newinsertTr).children(".ConsentPeriod").html(' ');
    $(newinsertTr).children(".Notes").html(' ');
    $(newinsertTr).children(".RestrictionIdLnr").html('0');

    $(newinsertTr).children(".UseTypes").addClass("updatedCell");
    $(newinsertTr).children(".CommercialModel").addClass("updatedCell");
    $(newinsertTr).children(".Restriction").addClass("updatedCell");
    $(newinsertTr).children(".RestrictionReason").addClass("updatedCell");
    $(newinsertTr).children(".ConsentPeriod").addClass("updatedCell");
    $(newinsertTr).children(".Notes").addClass("updatedCell");
    ///  add new row in the grid
    $("#" + digRestGridId + "_Table").find('tr').eq(rowindex).after(newinsertTr);
    //var enablechkSelection = $("#" + digRestGridId + "_Table").find("tr:eq(" + rowindex + 1 + ")").children()[3];
   // ReleasedisableEditingCheck(enablechkSelection);
    // if object is not null set empy value to corresponding fields
    if (newObject != null) {
        newObject.UseTypeLnr = '';
        newObject.CommercialModelsLnr = '';
        newObject.RestrictionLnr = '';
        newObject.RestrictonReasonLnr = '';
        newObject.ConsentPeriodLnr = '';
        newObject.NotesLnr = '';
        newObject.RestrictionIdLnr = 0;
        // set isEdit mode as true
        gridObj._edit._isEdit = true;
        // new object to synfucsion addedroecrds collection for edit /send to server
        gridObj._edit._addedRecords.push(newObject);
    }
    // add inserted rows collection for edit purpose.
    Array.add(gridObj._edit._insertedRowsCollection, $(newinsertTr)[0]);
    setTimeout("ReleaseenablenewRowSelection(" + rowindex + ")", 1000);
}

function ReleaseInsertedNewDataUpdate(curRowTrDel, newinsertTr) {
    var deleltedParentTd = $(curRowTrDel).children("td.RowCell").slice(3, 14);
    var newinsertedHtml = $(newinsertTr).children("td.RowCell").slice(3, 14);
    for (var i = 0; i <= deleltedParentTd.length; i++) {
        var deltdVal = deleltedParentTd[i];
        var tdhtml = $(deltdVal).html();
        var inserTd = $(newinsertedHtml)[i];
        $(inserTd).html(tdhtml);
    }
}

function AddandDeleteRow(curindex, status, relDigGridId) {
    var trs = $("#" + relDigGridId + "_Table").find('tr');
    var delFlag = "false";

    var firstrow = $("#" + relDigGridId + "_Table").find("tr")[curindex];
    var delrow = $(firstrow).children("td:eq(2)").html();

    if (parseInt(curindex) == 0 || (parseInt(delrow) != 0 && parseInt(delrow) != null))
        delFlag = "true";

    for (var i = curindex; i >= 0; i--) {
        var extTr = $("#" + relDigGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        if (parseInt(mergeVal) > 0) {
            if (status == "Add") {
                var rowspanRows = $("#" + relDigGridId + "_Table").find('tr')[i];
                $(rowspanRows).children("td.RowCell").slice(0, 14).attr("rowspan", parseInt(mergeVal) + 1);
                //$(rowspanRows).find("td[rowspan]").attr("rowspan", parseInt(mergeVal) + 1)
                $(td).html(parseInt(mergeVal) + 1);
                //$("#" + relDigGridId + "_Table").find("tr:eq(1)").children("td.RowCell:eq(0)").html();
                var originalParent = $("#" + relDigGridId + "_Table").find("tr")[i];
                var orgVal = $(originalParent).children("td.RowCell")[0];
                //var rightSetValue = $(orgVal).html();
                var originalchild = $("#" + relDigGridId + "_Table").find("tr")[parseInt(curindex)];
                var orgChildVal = $(originalchild).children("td.RowCell")[0];
                var rightSetChildValue = $(orgChildVal).html();
            }
            else {
                if (delFlag == "true") {
                    var rowspanRowsTemp = $("#" + relDigGridId + "_Table").find('tr')[i + 1];
                    var nextrowFlag = $(rowspanRowsTemp).is(":visible");
                    if (nextrowFlag == false) {
                        var nextRowIndex = i + 1;
                        nextRowDeleteReMergeRelease(nextRowIndex, parseInt(mergeVal), relDigGridId, "currentrowdelete");
                        break;
                    }
                    var firstRowTd = $(rowspanRowsTemp).find("td.RowCell").slice(0, 14);
                    $(firstRowTd).attr("rowspan", parseInt(mergeVal) - 1);
                    $(firstRowTd).removeClass("Merged");
                    var mergeTd = $(rowspanRowsTemp).children("td.RowCell")[1];
                    $(mergeTd).html(parseInt(mergeVal) - 1);
                }
                else {
                    var rowspanTr = $("#" + relDigGridId + "_Table").find('tr')[i];
                    $(rowspanTr).find("td.RowCell").slice(0, 14).attr("rowspan", parseInt(mergeVal) - 1);
                    $(td).html(parseInt(mergeVal) - 1);
                }
            }
            break;
        } else {
            if (status == "Add") {
                var curRowTr = $("#" + relDigGridId + "_Table").find('tr')[curindex];
                var isTrRowSpan = $(curRowTr).children("td.RowCell")[1];
                var currowspanVal = $(isTrRowSpan).html();
                var isMerged = $(isTrRowSpan).hasClass('Merged');
                if (parseInt(currowspanVal) == "0" && isMerged == false) {
                    var currentRowTd = $(curRowTr).find("td.RowCell").slice(0, 14);
                    $(currentRowTd).attr("rowspan", 2);
                    $(isTrRowSpan).html("2");
                    return;
                }
            }
        }
    }
}

function nextRowDeleteReMergeRelease(nextRowIndex, mergeVal, relDigGridId, status) {
    var tablelength = $("#" + relDigGridId + "_Table").find('tr').length;
    if (status != "newinsertdelete")
    nextRowIndex = parseInt(nextRowIndex) + 1;
    for (var i = nextRowIndex; i <= tablelength; i++) {
        var extTr = $("#" + relDigGridId + "_Table").find('tr')[i];
        var nextRowFlag = $(extTr).is(":visible");
        if (nextRowFlag == true) {
            var nextrowTd = $(extTr).find("td.RowCell").slice(0, 14);
            $(nextrowTd).attr("rowspan", parseInt(mergeVal) - 1);
            $(nextrowTd).removeClass("Merged");
            var mergeTd = $(extTr).children("td.RowCell")[1];
            $(mergeTd).html(parseInt(mergeVal) - 1);
            break;
        }
    }
}

function GetRightSetID(curindex, relDigGridId) {
    for (var i = curindex; i >= 0; i--) {
        var extTr = $("#" + relDigGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        var originalParent, orgVal;
        if (parseInt(mergeVal) > 0) {
            originalParent = $("#" + relDigGridId + "_Table").find("tr")[i];
            orgVal = $(originalParent).children("td.RowCell")[0];
            var rightSetVal = $(orgVal).html();
            break;
        } else {
            originalParent = $("#" + relDigGridId + "_Table").find("tr")[i];
            var rowMerge = $(extTr).children("td.RowCell")[1];
            var rowMergeVal = $(rowMerge).html();
            var isMerged = $(rowMerge).hasClass('Merged');
            if (parseInt(rowMergeVal) == 0 && isMerged == false) {
                orgVal = $(originalParent).children("td.RowCell")[0];
                rightSetVal = $(orgVal).html();
                break;
            }
        }
    }
    return rightSetVal;
}

function DigReleaseGetModifiedDate(curindex, digRestGridId) {
    var modifiedDate;
    for (var i = curindex; i >= 0; i--) {
        var extTr = $("#" + digRestGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        var originalParent, orgVal;
        if (parseInt(mergeVal) > 0) {
            originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
            orgVal = $(originalParent).children("td.RowCell")[23];
            modifiedDate = $(orgVal).html();
            break;
        } else {
            originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
            var rowMerge = $(extTr).children("td.RowCell")[1];
            var rowMergeVal = $(rowMerge).html();
            var isMerged = $(rowMerge).hasClass('Merged');
            if (parseInt(rowMergeVal) == 0 && isMerged == false) {
                orgVal = $(originalParent).children("td.RowCell")[23];
                modifiedDate = $(orgVal).html();
                break;
            }
        }
    }
    return modifiedDate;
}

function moveRow(oldPosition, newPosition, relDigGridId) {
    var row = $("#" + relDigGridId + "_Table").find('tr').eq(oldPosition).remove();
    $("#" + relDigGridId + "_Table").find('tr').eq(newPosition).after(row);
}

function ReleaseDigDeleteClick(element) {
    $('#loadingDiv').show();
    setTimeout(function () {

        if ($(element.parentNode).hasClass("disabledElement")) {
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return false;
        }

        var relDigGridId = $("#hdnRelDigGridId").val();
        var gridObj = $find(relDigGridId);

        var row = element.parentNode.parentNode;
        var rowindex = $(row).index();
        if (rowindex == -1) {
            row = $(element).parents('tr');
            rowindex = $(row).index();
        }

        $("#" + relDigGridId + "_Table > tbody > tr > td").removeClass('SelectionBackground');
        gridObj.get_SelectionManager().selectedRowsCollection = new Array();
        $(row).children().addClass("SelectionBackground");
        var insertedRowFlag = $(row).hasClass("InsertedRow");
        var insertCnt, deletedIndex;
        if (insertedRowFlag == false)
            insertCnt = $(row).prevAll(".InsertedRow").length;
        Array.add(gridObj.get_SelectionManager().selectedRowsCollection, rowindex);
        var curRowTrDel = $("#" + relDigGridId + "_Table").find('tr')[rowindex];
        var isTrRowSpanDel = $(curRowTrDel).children("td.RowCell")[1];
        var currowspanDelVal = $(isTrRowSpanDel).html();
        var isMergedDel = $(isTrRowSpanDel).hasClass('Merged');
        if (parseInt(currowspanDelVal) == "0" && isMergedDel == false) {
            if (insertedRowFlag == true) {
                $('#loadingDiv').hide(); $('#loaderPanel').hide();
                return;
            }
            var nullChildTd = $(curRowTrDel).children()[32];
            var nullChildrenVal = $(nullChildTd).html();
            if (nullChildrenVal == "-1" || nullChildrenVal == -1)
                ReleaseRemoveUpdatedSingleParentRow(row);
            else {
                ReleaseSingleChildDelete(relDigGridId, gridObj, curRowTrDel, rowindex, insertCnt);
                gridObj.get_SelectionManager().selectedRowsCollection = new Array();
                ReleaseAddedLastRowCellInDeleteClick(gridObj);
            }
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return;
        }
        else if (parseInt(currowspanDelVal) == "1" && isMergedDel == false && insertedRowFlag == true) {
            ReleaseDigtalRemoveRuntimeInsertedRowAsParentRow(row);
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return;
        }
        var rowSpanTest = $("#" + relDigGridId + "_Table").find('tr')[rowindex];
        var rowSpanTestTd = $(rowSpanTest).children("td.RowCell")[1];
        var rowSpanVal = $(rowSpanTestTd).html();
        if (parseInt(rowSpanVal) == 1) {
            ReleaseSingleChildDelete(relDigGridId, gridObj, curRowTrDel, rowindex, insertCnt);
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            gridObj.get_SelectionManager().selectedRowsCollection = new Array();
            ReleaseAddedLastRowCellInDeleteClick(gridObj);
            return;
        }
        else {
            var insertRowFlag = $(rowSpanTest).hasClass("InsertedRow");
            if (insertRowFlag == true) {
                if (parseInt(rowSpanVal) > 0) {
                    gridObj.sendDeleteRequest();
                    nextRowDeleteReMergeRelease(rowindex, parseInt(rowSpanVal), relDigGridId, "newinsertdelete");
                    var nextRow = $("#" + relDigGridId + "_Table").find('tr')[rowindex];
                    var nextrowTd = $(nextRow).find("td.RowCell").slice(0, 14);
                    $(nextrowTd).attr("rowspan", parseInt(rowSpanVal) - 1);
                    $(nextrowTd).removeClass("Merged");
                    var mergeTd = $(nextRow).children("td.RowCell")[1];
                    $(mergeTd).html(parseInt(rowSpanVal) - 1);
                    $('#loadingDiv').hide(); $('#loaderPanel').hide();
                    gridObj.get_SelectionManager().selectedRowsCollection = new Array();
                    ReleaseAddedLastRowCellInDeleteClick(gridObj);
                    return;
                }
                else if (parseInt(rowSpanVal) == 0) {
                    gridObj.sendDeleteRequest();
                    ReleaseAddedRowRemerge(relDigGridId, rowindex - 1);
                    $('#loadingDiv').hide(); $('#loaderPanel').hide();
                    gridObj.get_SelectionManager().selectedRowsCollection = new Array();
                    ReleaseAddedLastRowCellInDeleteClick(gridObj);
                    return;
                }
            }
            var addedcollection = gridObj._edit._addedRecords;
            gridObj._edit._addedRecords = new Array();
            ToAddInsertedRowsLengthRelease(insertCnt, gridObj);
            gridObj.sendDeleteRequest();
            gridObj._edit._addedRecords = new Array();
            gridObj._edit._addedRecords = addedcollection;
            AddandDeleteRow(rowindex, "Delete", relDigGridId);
            gridObj.get_SelectionManager().selectedRowsCollection = new Array();
            ReleaseAddedLastRowCellInDeleteClick(gridObj);
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
        }
    }, 50);
}
function ReleaseAddedLastRowCellInDeleteClick(gridObj) {
    var trslength = $(gridObj._gridContentTable).find("tr").length;
    trslength = trslength - 1;
    for (var i = trslength; i >= 0; i--) {
        var nextTr = $(gridObj._gridContentTable).find("tr")[i];
        if ($(nextTr).is(":visible")) {
            var hasLastRowTd = $(nextTr).children("td.RowCell")[17];
            if (!$(hasLastRowTd).hasClass("LastRowCell")) {
                var nextTds = $(nextTr).children("td");
                $(nextTds).removeClass("LastRowCell");
                $(nextTds).addClass("LastRowCell");
            }
            break;
        }
    }
}
function ToAddInsertedRowsLengthRelease(insertCnt, gridObj) {
    for (var i = 0; i < insertCnt; i++) {
        gridObj._edit._addedRecords.push(i);
    }
}
function RemoveSelectedRowCollectionRelease(gridObj, prevSelecteditems, rowindex) {
    Array.remove(gridObj.get_SelectionManager().selectedRowsCollection, rowindex);
    Array.remove(prevSelecteditems, rowindex);
    gridObj.get_SelectionManager().selectedRowsCollection = prevSelecteditems;
}

function ReleaseRemoveUpdatedSingleParentRow(row) {
    var strfilter = '';
    var usertype = $(row).find('.UseTypes').html();
    var CommercialModelTypes = $(row).find('.CommercialModel').html();
    var RestrictionTypes = $(row).find('.Restriction').html();
    var RestrictionReason = $(row).find('.RestrictionReason').html();
    var ConsentPeriodTypes = $(row).find('.ConsentPeriod').html();
    var Notes = $(row).find('.Notes').html();
    var UseTypes = $(row).find('.UseTypes').html();
    var RightSetID = $($(row).find('td.RowCell')[0]).html();
    if (usertype)
        strfilter = usertype;
    if (CommercialModelTypes)
        strfilter += CommercialModelTypes;
    if (RestrictionTypes)
        strfilter += RestrictionTypes;
    if (RestrictionReason)
        strfilter += RestrictionReason;
    if (ConsentPeriodTypes)
        strfilter += ConsentPeriodTypes;
    var digRestGridId = $("#hdnRelDigGridId").val();
    var gridObj = $find(digRestGridId);
    var updateRecords = gridObj._edit._updatedRecords;
    var values = '';
    $(row).find('.UseTypes').html('');
    $(row).find('.CommercialModel').html('');
    $(row).find('.Restriction').html('');
    $(row).find('.RestrictionReason').html('');
    $(row).find('.ConsentPeriod').html('');
    $(row).find('.Notes').html('');
    $(row).find('.UseTypes').html('');

    for (i = 0; i < updateRecords.length; i++) {
        if (updateRecords[i].RightSetIdLnr == RightSetID) {
            updateRecords.splice(i, 1);
            break;
        }
    }
}

function ReleaseAddedRowRemerge(digRestGridId, rowindex) {
    for (var i = rowindex; i >= 0; i--) {
        var extTr = $("#" + digRestGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        var originalParent;
        if (parseInt(mergeVal) > 0) {
            originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
            var nextrowTd = $(originalParent).find("td.RowCell").slice(0, 14);
            $(nextrowTd).attr("rowspan", parseInt(mergeVal) - 1);
            var mergeTd = $(extTr).children("td.RowCell")[1];
            $(mergeTd).html(parseInt(mergeVal) - 1);
            break;
        }
    }
}

function ReleaseDigtalRemoveRuntimeInsertedRowAsParentRow(row) {
    var strfilter = '';
    var usertype = $(row).find('.UseTypes').html();
    var CommercialModelTypes = $(row).find('.CommercialModel').html();
    var RestrictionTypes = $(row).find('.Restriction').html();
    var RestrictionReason = $(row).find('.RestrictionReason').html();
    var ConsentPeriodTypes = $(row).find('.ConsentPeriod').html();
    var Notes = $(row).find('.Notes').html();
    var UseTypes = $(row).find('.UseTypes').html();
    var RightSetID = $($(row).find('td.RowCell')[0]).html();
    if (usertype)
        strfilter = usertype;
    if (CommercialModelTypes)
        strfilter += CommercialModelTypes;
    if (RestrictionTypes)
        strfilter += RestrictionTypes;
    if (RestrictionReason)
        strfilter += RestrictionReason;
    if (ConsentPeriodTypes)
        strfilter += ConsentPeriodTypes;
    var digRestGridId = $("#hdnRelDigGridId").val();
    var gridObj = $find(digRestGridId);
    var addedRecords = gridObj._edit._addedRecords;
    var values = '';
    $(row).find('.UseTypes').html('');
    $(row).find('.CommercialModel').html('');
    $(row).find('.Restriction').html('');
    $(row).find('.RestrictionReason').html('');
    $(row).find('.ConsentPeriod').html('');
    $(row).find('.Notes').html('');
    $(row).find('.UseTypes').html('');

    for (i = 0; i < addedRecords.length; i++) {
        if (addedRecords[i].RestrictionIdLnr == 0 && addedRecords[i].RightSetIdLnr == RightSetID) {
            addedRecords[i].UseTypeLnr = '';
            addedRecords[i].CommercialModelsLnr = '';
            addedRecords[i].RestrictionLnr = '';
            addedRecords[i].RestrictonReasonLnr = '';
            addedRecords[i].ConsentPeriodLnr = '';
            addedRecords[i].NotesLnr = '';
        }
    }
}
 
function ReleaseSingleChildDelete(digRestGridId, gridObj, curRowTrDel, rowindex, insertCnt) {
    var tablePositiontop = $("#" + digRestGridId + " .GridContent").children("div:first").css("top");
    var tablePositionLeft = $("#" + digRestGridId + " .GridContent").children("div:first").css("left");
    var vscrollposition = $("#" + digRestGridId + " .sf-sp-Vhandle").css("top");
    var hscrollposition = $("#" + digRestGridId + " .sf-sp-Hhandle").css("left");
    var hscrollHeader = $("#" + digRestGridId + " .GridHeader .Table").css("left");

    var addedcollection = gridObj._edit._addedRecords;
    gridObj._edit._addedRecords = new Array();
    ToAddInsertedRowsLengthRelease(insertCnt, gridObj);
    gridObj.sendDeleteRequest();
    gridObj._edit._addedRecords = new Array();
    gridObj._edit._addedRecords = addedcollection;

    gridObj.jsonModeMgr._AddAction();

    var newinsertTr = $("#" + digRestGridId + "_Table").find("tr")[0];
    $(newinsertTr).children().removeClass("SelectionBackground");
    $(newinsertTr).find('.disabledElement').removeClass('disabledElement');
    var addedRecords = gridObj._edit._addedRecords;
    
    if (addedRecords.length > -1) {
        addedRecords[addedRecords.length - 1].RightsEditPermitted = 'Y';
    }
    $(newinsertTr).children("td.RightsEditPermitted").html('Y');

    var delUpdatedCells = $(newinsertTr).find("td.RowCell").slice(0, 14);
    $(delUpdatedCells).removeClass("updatedCell");
    $(delUpdatedCells).removeClass("Merged");
    var toRowindex = $(newinsertTr).index();
    moveRow(toRowindex, rowindex, digRestGridId);
    //var enablechkSelection = $("#" + digRestGridId + "_Table").find("tr:eq(" + rowindex + 1 + ")").children()[3];
    //ReleasedisableEditingCheck(enablechkSelection);
    var rightSetvaltd = $(curRowTrDel).children("td.RowCell")[0];
    var rightsetVal = $(rightSetvaltd).html();
    var td = $(newinsertTr).children("td.RowCell")[0];
    $(td).find("#RightSetIdLnr").val(rightsetVal);
    $("#" + digRestGridId).mousedown();

    //var reltomodDate = $(newinsertTr).children("td.RowCell")[23];
    var reltomodDatevaltd = $(curRowTrDel).children("td.RowCell")[23];
    var reltomodDateVal = $(reltomodDatevaltd).html();
    var reltomodDate = $(newinsertTr).children("td.RowCell")[23];
    $(reltomodDate).click();
    //var modifiedDate = DigReleaseGetModifiedDate(rowindex + 1, digRestGridId);
    if ($(reltomodDate).find("#ModifiedDateTimeLnr").length == 0) {
        //
        var dataindex = addedRecords.length - 1;
        if (dataindex > -1) {
            addedRecords[dataindex].ModifiedDateTimeLnr = reltomodDateVal;
        }
    }
    else {
        $(reltomodDate).find("#ModifiedDateTimeLnr").val(reltomodDateVal);
    }
    $("#" + digRestGridId).mousedown();

    ReleaseInsertedNewDataUpdate(curRowTrDel, newinsertTr);
    setTimeout("ReleaseenablenewRowSelection(" + rowindex + ")", 1000);
    $("#" + digRestGridId + " .GridContent").children("div:first").css("top", tablePositiontop);
    $("#" + digRestGridId + " .GridContent").children("div:first").css("left", tablePositionLeft);
    $("#" + digRestGridId + " .sf-sp-Vhandle").css("top", vscrollposition);
    $("#" + digRestGridId + " .sf-sp-Hhandle").css("left", hscrollposition);
    $("#" + digRestGridId + " .GridHeader .Table").css("left", hscrollHeader);
}

//check box check all


function ReleaseDigitalCheckBoxAllClick(args) {
    var grid_Id = $('#hdnGridId').val()
    var obj = $("#" + grid_Id).find(".GridHeader #checkboxAllRelDig");
    var gridObj = $find(grid_Id);
    var ischecked = false;
    if (obj.attr("checked") == "checked") {
        $("#" + grid_Id + " .GridContent").find('#checkboxReleaseDigitalchild').attr("checked", "checked");
        $("#" + grid_Id + "_Table > tbody > tr > td").addClass("SelectionBackground");
        ischecked = true;
    }
    else {
        $("#" + grid_Id + " .GridContent").find('#checkboxReleaseDigitalchild').removeAttr("checked");
        $("#" + grid_Id + "_Table > tbody > tr > td").removeClass("SelectionBackground");
        ischecked = false;
    }
    CheckAllClickRelease(ischecked, gridObj, grid_Id);
}


//function CheckAllClickRelease(haschecked, gridObj, digiGridName) {
//    if (haschecked) {
//        gridObj.get_SelectionManager().selectedRowsCollection = new Array();
//        var checkboxes = $("#" + digiGridName).find(".GridContent #checkboxReleaseDigitalchild");
//        $.each(checkboxes, function (index, checkbox) {
//            Array.add(gridObj.get_SelectionManager().selectedRowsCollection, index);
//        });
//    }
//    else {
//        var checkboxes = $("#" + digiGridName).find(".GridContent #checkboxReleaseDigitalchild");
//        $.each(checkboxes, function (index, checkbox) {
//            gridObj.get_SelectionManager().deselectRow(index);
//        });
//    }
//    SetSelectedRecordsCollectionRelease(gridObj);
//}

//function ReleaseRecordSelectionCheckAll(sender) {
//    var totalLength = $("#" + sender._ID + "_Table tr").length;
//    var checked = $('#' + sender._ID + ' tr td').filter(':has(:checkbox:checked)').length;
//    $("#checkboxAllRelDig").removeAttr("indeterminate");
//    if (totalLength == checked)
//        $("#checkboxAllRelDig").attr('checked', true);
//    else
//        $("#checkboxAllRelDig").prop("indeterminate", true);
//}

//function ReleaseRecordSelection(sender, args) {
//    HideWarningSuccess();
//    var tr = args.getRow();
//    $(tr).find('#checkboxReleaseDigitalchild').attr("checked", "checked");
//    BulkEditCheckBox(tr, true);
//    var gridobj = $find(sender._ID);
//    SetSelectedRecordsCollectionRelease(gridobj);
//    setTimeout(function () { ReleaseRecordSelectionCheckAll(sender), 0 });

//}
//function ReleaseUnRecordSelectionCheckAll(sender) {
//    var totalLength = $("#" + sender._ID + "_Table tr").length;
//    var checked = $('#' + sender._ID + ' tr td').filter(':has(:checkbox:checked)').length;
//    $("#checkboxAllRelDig").removeAttr("indeterminate");

//    if (checked == 0)
//        $("#checkboxAllRelDig").removeAttr('checked');
//    else
//        $("#checkboxAllRelDig").prop("indeterminate", true);
//}
//function ReleaseUnRecordSelection(sender, args) {
//    var tr = args.getRow();
//    $(tr).find('#checkboxReleaseDigitalchild').removeAttr("checked", "checked");
//    BulkEditCheckBox(tr, false);
//    var gridobj = $find(sender._ID);
//    SetSelectedRecordsCollectionRelease(gridobj);
//    setTimeout(function () { ReleaseUnRecordSelectionCheckAll(sender), 0 });
//}


//function ReleaseBulkEditCheckBox(row, hascheked) {
//    var rowmergeTd, mergeCnt;
//    var index = $(row).index();
//    if (hascheked) {
//        rowmergeTd = $(row).children()[2];
//        mergeCnt = $(rowmergeTd).html();
//        if (parseInt(mergeCnt) > 0)
//            ReleaseBulkRowSelection(index, mergeCnt, true);
//    } else {
//        rowmergeTd = $(row).children()[2];
//        mergeCnt = $(rowmergeTd).html();
//        if (parseInt(mergeCnt) > 0)
//            ReleaseBulkRowSelection(index, mergeCnt, false);
//    }
//}

//function ReleaseBulkRowSelection(index, mergeCnt, checkEnable) {
//    index = index + 1;
//    mergeCnt = mergeCnt - 1;
//    for (var i = 0; i < mergeCnt; i++) {
//        var gridid = $('#hdnGridId').val();
//        var row = $("#" + gridid + "_Table").find("tr")[index];
//        var checkbox = $(row).find("#checkboxReleaseDigitalchild")[0];
//        var gridObj = $find(gridid);
//        if (checkEnable == true) {
//            $(checkbox).attr("checked", true);
//            $(row).children().addClass("SelectionBackground");
//            Array.add(gridObj.get_SelectionManager().selectedRowsCollection, index);
//        }
//        else {
//            $(checkbox).attr("checked", false);
//            $(row).children().removeClass("SelectionBackground");
//            Array.remove(gridObj.get_SelectionManager().selectedRowsCollection, index);
//        }
//        index++;
//    }
//}
function ReleasedigitalRestrictionsCheckList() {
    var digiGridName = $('#hdnGridId').val();
    setTimeout(function () {
        var gridObj = $find(digiGridName);
        gridObj.set_AllowSelection(true);
        gridObj.set_AllowSelection(false);
    }, 0);
}

function ReleasedisableEditingCheck(element) {
    $(element).click(function (event) {
        ReleaseClickCheckbox(event);
        event.stopPropagation();
        return true;
    });
    //return false;
}

function ReleaseenablenewRowSelection(rowindex) {
    var gridid = $('#hdnGridId').val();
    var row = $("#" + gridid + "_Table").find("tr")[rowindex + 1];
    var checkbox = $(row).find("#checkboxReleaseDigitalchild");
    var element = checkbox.parent();
    $(element).click(function (event) {
        ReleaseClickCheckbox(event);
        event.stopPropagation();
        return true;
    });
}
function ReleaseClickCheckbox(event) {
    //var checkbox = $(element).find("#checkboxReleaseDigitalchild");
    if (event.target.id == "checkboxReleaseDigitalchild") {
        var checkbox = event.target;
        var row = event.target.parentNode.parentNode;
        var row_index = $(row).index();
        var gridid = $('#hdnGridId').val();
        var gridobj = $find(gridid);
        if (checkbox.checked) {
            Array.add(gridobj.get_SelectionManager().selectedRowsCollection, row_index);
            $(row).children().addClass("SelectionBackground");
            //$(checkbox).attr("checked", true);
            //RecordSelectionCheckAllRelease();
            BulkEditCheckBoxRelease(row, true);
        }
        else {
            gridobj.get_SelectionManager().deselectRow(row_index);
            //$(checkbox).attr("checked", false);
            //UnRecordSelectionCheckAllRelease();
            BulkEditCheckBoxRelease(row, false);
        }
        SetSelectedRecordsCollectionRelease(gridobj);
    }
}

function BulkEditCheckBoxRelease(row, hascheked) {

    var rowmergeTd, mergeCnt;
    var gridid = $('#hdnGridId').val();
    //var index = $(row).index();
    var index = $("#" + gridid + "_Table tr:visible").index(row);
    if (hascheked) {
        rowmergeTd = $(row).children()[2];
        mergeCnt = $(rowmergeTd).html();
        if (parseInt(mergeCnt) > 0)
            BulkRowSelectionRelease(index, mergeCnt, true);
    } else {
        rowmergeTd = $(row).children()[2];
        mergeCnt = $(rowmergeTd).html();
        if (parseInt(mergeCnt) > 0)
            BulkRowSelectionRelease(index, mergeCnt, false);
    }
}

function BulkRowSelectionRelease(index, mergeCnt, checkEnable) {
    index = index + 1;
    mergeCnt = mergeCnt - 1;
    var gridid = $('#hdnGridId').val();
    for (var i = 0; i < mergeCnt; i++) {
        var row = $("#" + gridid + "_Table tr:visible")[index];
        var checkbox = $(row).find("#checkboxReleaseDigitalchild")[0];
        var gridObj = $find(gridid);
        if (checkEnable == true) {
            $(checkbox).attr("checked", true);
            $(row).children().addClass("SelectionBackground");
            Array.add(gridObj.get_SelectionManager().selectedRowsCollection, index);
        }
        else {
            $(checkbox).attr("checked", false);
            $(row).children().removeClass("SelectionBackground");
            Array.remove(gridObj.get_SelectionManager().selectedRowsCollection, index);
        }
        index++;
    }
}

/* if Grid Checkbox unchecked based on that Over All check box will be uncheck or semi checked  */
function UnRecordSelectionCheckAllRelease() {
    var gridid = $('#hdnGridId').val();
    var totalLength = $("#" + gridid + "_Table tr").length;
    var checked = $('#' + gridid + ' tr td').filter(':has(:checkbox:checked)').length;
    $("#checkboxAllRelDig").removeAttr("indeterminate");
    if (checked == 0)
        $("#checkboxAllRelDig").removeAttr('checked');
    else
        $("#checkboxAllRelDig").prop("indeterminate", true);
}

/* if Grid Checkbox nchecked based on that Over All check box will be checked or semi checked  */
function RecordSelectionCheckAllRelease() {
    var gridid = $('#hdnGridId').val();
    var totalLength = $("#" + gridid + "_Table tr").length;
    var checked = $('#' + gridid + ' tr td').filter(':has(:checkbox:checked)').length;
    $("#checkboxAllRelDig").removeAttr("indeterminate");
    if (totalLength == checked)
        $("#checkboxAllRelDig").attr('checked', true);
    else
        $("#checkboxAllRelDig").prop("indeterminate", true);
}
function ReleaseDigRestCheckBoxAllClick(args) {
    digiGridName = $('#hdnGridId').val();
    var gridObj = $find(digiGridName);
    var obj = $("#" + digiGridName).find(".GridHeader #checkboxAllRelDig");
    var ischecked = false;
    if (obj.attr("checked") == "checked") {
        $("#" + digiGridName + " .GridContent").find('#checkboxReleaseDigitalchild').attr("checked", "checked");
        $("#" + digiGridName + "_Table > tbody > tr > td").addClass("SelectionBackground");
        ischecked = true;
    }
    else {
        $("#" + digiGridName + " .GridContent").find('#checkboxReleaseDigitalchild').removeAttr("checked");
        $("#" + digiGridName + "_Table > tbody > tr > td").removeClass("SelectionBackground");
        ischecked = false;
    }
    CheckAllClick(ischecked, gridObj, digiGridName);
}

function  CheckAllClickRelease(haschecked, gridObj, digiGridName) {
    if (haschecked) {
        gridObj.get_SelectionManager().selectedRowsCollection = new Array();
        var checkboxes = $("#" + digiGridName).find(".GridContent #checkboxReleaseDigitalchild");
        $.each(checkboxes, function (index, checkbox) {
            Array.add(gridObj.get_SelectionManager().selectedRowsCollection, index);
        });
    }
    else {
        var checkboxes = $("#" + digiGridName).find(".GridContent #checkboxReleaseDigitalchild");
        $.each(checkboxes, function (index, checkbox) {
            gridObj.get_SelectionManager().deselectRow(index);
        });
    }
    SetSelectedRecordsCollectionRelease(gridObj);
}



function SetSelectedRecordsCollectionRelease(gridObj) {
    var result = gridObj.get_SelectedRecords();
    selectedGridItems = [];
    releaseRightSetArray = [];
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
        var isUnique = true;
        for (var i = 0; i < releaseRightSetArray.length; i++) {
            if (releaseRightSetArray[i] == value.RightSetIdLnr) {
                isUnique = false;
            }
        }
        if (isUnique) {
            selectedGridItems.push(
                {
                    KeyId: getValue(getKeyValue(value.ResourceId, value.ReleaseId)),
                    R2KeyId: getR2KeyValue(value.R2ResourceId, value.R2ReleaseId),
                    ContractId: getValue(value.ContractId),
                    LinkType: getValue(linkType),
                    RightSetId: getValue(value.RightSetIdLnr)
                });
        }
    });
}


/*************************************/

if (!window.callBulkEditReleaseDigitalButton) {
    //Release Bulk Edit button click
    function callBulkEditReleaseDigitalButton() {
        var grid_Id = $('#hdnGridId').val();
        $("#" + releaseDigitalBulkEditBtnId).click(function () {
            HideWarningSuccess();
            var bulkEditRecordlength = $find(grid_Id).get_SelectedRecords().length;
            if (bulkEditRecordlength >= 1) {
                var objBulkEditReleaseDigital = $('<div class="divobjBulkEditDigital"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: 'Bulk Edit - Digital Restrictions',
                    show: 'clip',
                    hide: 'clip',
                    width: 900,
                    height: 550,
                    close: function () {
                        $(this).remove();
                    }
                });
                objBulkEditReleaseDigital.load('/GCS/RightsReviewWorkQueue/DigitalRestrictionsBulkEdit?HasDelete=0', "",
                        function (responseText, textStatus) {
                            if (textStatus == 'error') {
                                objBulkEditDigital.html('<p>' + messageCommon.error + '</p>');
                            }
                        });
                objBulkEditReleaseDigital.dialog('open');
            } else {
                ShowWarning('Please select Repertoire');
                SyncfusionGridScrollRelDig(grid_Id);
            }
        });
    } 
}
function validateDigitalRestrictionReason(args, rowIndex) {
    var restrictionReason = "";
    $($('#' + $('#hdnGridId').val() + '_Table').find('tr')[rowIndex]).find('td').each(function () {
        if ($(this).index() == 19) {
            restrictionReason = $(this).html();
            if (restrictionReason.indexOf('<') > 0)
                restrictionReason = $(this).find('#RestrictionLnr').val();
            return restrictionReason;
        }
    });
    return restrictionReason;
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
