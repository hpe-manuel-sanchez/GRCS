var sortType = "",sortColumn = "",parentRowDatas = new Array(),rowDatas = new Array(),rightSetId = new Array(),mergeCount = new Array(),childs = new Array(),newArrayDigital = new Array(),digiGridName = $('#hdnGridId').val(),drErrorExist = false,isOtherSelected = false,rightSetArray = [];
var invalidCombinations = new Array();


$(document).ready(function () {
    invalidCombinations.push('DownloadAd-funded');
    invalidCombinations.push('AudioDownloadAd-funded');
    invalidCombinations.push('VideoDownloadAd-funded');
    invalidCombinations.push('ImageDownloadAd-funded');
    invalidCombinations.push('ImageStreamingAll');
    invalidCombinations.push('ImageStreamingSubscription');
    invalidCombinations.push('ImageStreamingA&nbsp;la&nbsp;Carte'); //If added through dropdown
    invalidCombinations.push('ImageStreamingA la Carte'); //If loaded From Service
    invalidCombinations.push('ImageStreamingAd-funded');
    
    $('.sf-toolbar').hide(); /*TO hide Toolbar on load*/
    resizeAccordion();
    $('.RefreshPager').click(function () {
        $("#chkAllDigRest").removeAttr('checked');
        HideWarningSuccess();
    });
});

/*Validations For Digital Restriction Section Starts*/

/*On Resource Digital Cell Edit*/
function resourceDigitalCellEdit(sender, args) {

    if (args.colObj.Name == "RestrictonReasonLnr") {
        if (sender._currentDataItem != null) {
            var rowIndexRestrictionReasone = $(sender._editTD).parent().index();
            var restriction = getRestrictionText(args, rowIndexRestrictionReasone);
            if (restriction != "" && restriction != null) {
                if (restriction.indexOf(digitalMasterData.restrictionTypeRights) != -1) {
                    $("#RestrictonReasonLnr").attr("disabled", false);
                } else {
                    $("#RestrictonReasonLnr").attr("disabled", true);
                }
            }
            isOtherSelected = false;
            $('#RestrictonReasonLnr').change(function () {
                if ($(this).find(':selected').text() == digitalMasterData.restrictionReasonOther) {
                    var formElement = "<form id='digitalGridEditForm'><input id='RestrictonReasonLnr' type='text' value='' name='RestrictonReasonLnr'><span class='field-validation-valid' data-valmsg-replace='true' data-valmsg-or='NotesLnr'></span></form>";
                    $(sender._editTD).html(formElement);
                    $('#RestrictonReasonLnr').focus(); 
                    isOtherSelected = true;
                }
            });
        } else {//function to support if it is new row
            var rowIndexRestrictionReason = $(sender._editTD).parent().index();
            var restrictionNew = getRestrictionText(args, rowIndexRestrictionReason);
            if (restrictionNew != "") {
                if (restrictionNew.indexOf(digitalMasterData.restrictionTypeRights) != -1) {
                    $("#RestrictonReasonLnr").attr("disabled", false);
                } else {
                    $("#RestrictonReasonLnr").attr("disabled", true);
                }
            } 
            $('#RestrictonReasonLnr').change(function () {
                if ($(this).find(':selected').text() == digitalMasterData.restrictionReasonOther) {
                    var formElement = "<form id='digitalGridEditForm'><input id='RestrictonReasonLnr' type='text' value='' name='RestrictonReasonLnr'><span class='field-validation-valid' data-valmsg-replace='true' data-valmsg-or='NotesLnr'></span></form>";
                    $(sender._editTD).html(formElement);
                    $('#RestrictonReasonLnr').focus();
                    isOtherSelected = true;
                }
            });
        }
    }


    if (args.colObj.Name == "ConsentPeriodLnr") {
        var rowIndex = $(sender._editTD).parent().index();
        var restrictionType = getRestrictionText(args, rowIndex);
        if (restrictionType != undefined && restrictionType != "") {
            if (restrictionType.indexOf(digitalMasterData.no) != -1 || restrictionType.indexOf(digitalMasterData.restrictionTypeRefer) != -1) {
                $('#ConsentPeriodLnr').attr("disabled", true);
            } else {
                $('#ConsentPeriodLnr').attr("disabled", false);
            }
        }
    }

    if (args.colObj.Name == "UPCId") {
        var upcId = args.value;
        if (upcId != "" && upcId != null) {
            args.cancel = true;
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
            args.cancel = true;
            $('#loadingDiv').show();
                setTimeout(function () {
                    createCustomReleaseTab("1,,,,,,,,,,," + isrcId + ",,true,true");
                }, 10);
        }
        $('#' + $('#hdnGridId').val()).mousedown();
    }
    if (args.colObj.Name == "ReviewStatusLnr") {
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


/*On Resource Digital Cell Save*/
function resourceDigitalCellSave(sender, args) {
    if (args.colObj.Name == "RestrictonReasonLnr") {
        if (sender._currentDataItem != null) {
            var rowIndexRestrictionReasone = $(sender._editTD).parent().index();
            var restrictionNewe = getRestrictionText(args, rowIndexRestrictionReasone);
            if (restrictionNewe != "" && restrictionNewe != null) {
                if (restrictionNewe.indexOf(digitalMasterData.restrictionTypeRights) != -1) {
                    if (args.value == args.oldValue && args.oldValue == digitalMasterData.restrictionReasonOther) {
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
            var rowIndexRestrictionReason = $(sender._editTD).parent().index();
            var restrictionNew = getRestrictionText(args, rowIndexRestrictionReason);
            if (restrictionNew != "" && restrictionNew != null) {
                if (restrictionNew.indexOf(digitalMasterData.restrictionTypeRights) != -1) {
                    if (args.value == args.oldValue && args.oldValue == digitalMasterData.restrictionReasonOther) {
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

    if (args.colObj.Name == "ReviewStatusLnr") {
        //if (args.value == digitalMasterData.reviewStatusFinal) {
            if (args.value == "Final") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).html('<div class=\'flagGreen\'></div>');
                }, 100);
            }
            else if (args.value == "NewForReview") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).html('<div class=\'flagBlue\'></div>');
                }, 100);
            }
            else if (args.value == "FinalForReview") {
                sender._editTD = setTooltipWithImage(sender._editTD);
                $(sender._editTD).html("");
                var index = $(sender._editTD).index();
                var parentTr = $(sender._editTD).parent();
                setTimeout(function () {
                    $($(parentTr).find('td')[index]).html('');
                    $($(parentTr).find('td')[index]).html('<div class=\'flagOrange\'></div>');
                }, 100);
            }

          //  $($($(sender._editTD).parent()).find('td')[$(sender._editTD).index()]).html()
       // }
    }

    if (args.colObj.Name == "RestrictionLnr") {
        if (args.value.indexOf(digitalMasterData.no) == 0 || args.value.indexOf(digitalMasterData.restrictionTypeRefer) == 0) {
            $('#ConsentPeriodLnr').attr("disabled", true);
            $(sender._editTD.nextSibling).next().html("");
            if (sender._currentDataItem != null && sender._currentDataItem != null && sender._currentDataItem.RestrictionLnr != null && sender._currentDataItem.ConsentPeriodLnr != null ) {
                sender._currentDataItem.ConsentPeriodLnr = "";
            }
        }
        else {
            $('#ConsentPeriodLnr').attr("disabled", false);
        }
        if (args.value.indexOf(digitalMasterData.restrictionTypeRights) != -1) {
            $(sender._editTD.nextSibling).attr("disabled", false);
        } else {
            $(sender._editTD.nextSibling).html("");
            if (sender._currentDataItem != null && sender._currentDataItem.RestrictonReasonLnr != null) {
                sender._currentDataItem.RestrictonReasonLnr = "";
                $(sender._editTD.nextSibling).attr("disabled", true);
            }
        }
    }
    if (args.colObj.Name == "ConsentPeriodLnr") {
        var rowIndex = $(sender._editTD).parent().index();
        var restriction = getRestrictionText(args, rowIndex);
        if (restriction != undefined && restriction != "") {
            if (restriction.indexOf(digitalMasterData.no) != -1 || restriction.indexOf(digitalMasterData.restrictionTypeRefer) != -1) {
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

    if (args.colObj.Name == "ISRCId") {
        args.value = args.oldValue;
    }
}


/*Check For invalid Combination of UseType and Commercial model */
function inValidCombination(childData) {
    var inValid = false;
    
    for (var i = 0; i < childs.length; i++) {
        var isUniqueDownloadErrorAppended = false;
        var isUniqueStreamingErrorAppended = false;
        newArrayDigital = new Array();
        var childDatas = childData[i].Value;
        var aryDigital = new Array();
        if (childData[i].isUpdated){//If updated alone do validation
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
                var resourceType = splitRow[6].trim();
                
               
                if(resourceType=="") {
                    resourceType = childDatas[0].split('^')[6].trim();
                }
                var useTypeAndCtypes = resourceType + useType + commercialModel;
                if ((useType == "" && commercialModel != "") || (useType != "" && commercialModel == "") || (useType != "" && commercialModel != "" && restriction == "") || (useType == "" && commercialModel == "" && (restriction != "" || consentPeriod != "" || restrictionReason != "" || notes != ""))) { //Validation to check is the DR is complete
                    isErrorOccured = true;
                    appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalIncompleteValidation); 
                }
                
                if (!isErrorOccured && ((useType == "" && commercialModel == "") && childDatas.length > 1  && childDatas[j].split('^')[7].trim() == 0)) { //Validation to check is the DR is complete                
                    isErrorOccured = true;
                    appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalIncompleteValidation); 
                }

                if (!isErrorOccured && checkIsNotValidDigitalCombination(useTypeAndCtypes)) { //Validation to check is the DR is having invalid combination
                    isErrorOccured = true;
                    appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalValidCombinationValidation);
                }

                if (!isErrorOccured && (restriction != "" && consentPeriod == "")){ //Validation to check is the DR is incomplete with consentPeriod
                        if (restriction == digitalMasterData.restrictionTypeConsult || restriction.indexOf(digitalMasterData.restrictionTypeConsent) == 0) {
                            isErrorOccured = true;
                            appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalConsentPeriodValidation); 
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

                if ( useType != "" && commercialModel != "") {
                    var digitalObject = new Object();
                    digitalObject.CommercialModel = new Array();

                    if (!fillUseTypeAndCommercial(useType, commercialModel, (parseInt(j)))) {
                        digitalObject.UseType = useType;
                        digitalObject.CommercialModel.push(commercialModel + "^" + (parseInt(j)));
                        newArrayDigital.push(digitalObject);
                    }

                    var isUniqueCombination = checkDigitalUniqueCombination(useType);
                    if ( isUniqueCombination.toString() != "true") {//Validation for All combination
                        if (useType == digitalMasterData.useTypeDownload && !isUniqueDownloadErrorAppended) {
                            isUniqueDownloadErrorAppended = true;
                            var rowId = parseInt(splitRow[splitRow.length - 1]) - isUniqueCombination;
                            if (isUniqueCombination == "false" || isUniqueCombination < 0)
                                rowId = splitRow[splitRow.length - 1];

                            isErrorOccured = true;
                            appendErrorToGrid(rowId.toString(), digitalGridMessages.digitalUniqueCombinationValidation); 
                        } if (useType == digitalMasterData.useTypeStreaming &&  !isUniqueStreamingErrorAppended ) {
                            isUniqueStreamingErrorAppended = true;
                            var rowIdNew = parseInt(splitRow[splitRow.length - 1]) - isUniqueCombination;
                            if (isUniqueCombination == "false" || isUniqueCombination < 0)
                                rowIdNew = splitRow[splitRow.length - 1];

                            isErrorOccured = true;
                            appendErrorToGrid(rowIdNew.toString(), digitalGridMessages.digitalUniqueCombinationValidation); 
                        }
                    }
                }


                
                
                if (!isErrorOccured && (restriction.indexOf(digitalMasterData.restrictionTypeRights) != -1 && restrictionReason == "")) {//Reason Mandatory for No Rights
                    appendErrorToGrid(splitRow[splitRow.length - 1], digitalGridMessages.digitalRestrictionReasonValidation); 
                }
            }
        }
    }
    return inValid;
}


/*Invalid Combination check for UseType,ContentType and CommercialModel*/
function checkIsNotValidDigitalCombination(drData) {
    for (var index = 0; index < invalidCombinations.length; index++) {
        if (drData == invalidCombinations[index])
            return true;
    }
    return false;
}





/*Validations For Digital Restriction Section Ends*/
function DigRestRowDataBound(sender, args) {

    var image = '';
    var tooltipText = null;
    if (args.Data.ResourceType == 1) {
        image = '<div class=\'resourceAudio\'></div>';
        tooltipText = 'Audio';
    } else if (args.Data.ResourceType == 2) {
        image = '<div class=\'resourceVideo\'></div>';
        tooltipText = 'Video';
    } else if (args.Data.ResourceType == 3) {
        image = '<div class=\'resourceImage\'></div>';
        tooltipText = 'Image';
    } else if (args.Data.ResourceType == 4) {
        image = '<div class=\'resourceMerchandise\'></div>';
        tooltipText = 'Merchandise';
    } else if (args.Data.ResourceType == 5) {
        image = '<div class=\'resourceText\'></div>';
        tooltipText = 'Text';
    } else if (args.Data.ResourceType == 6) {
        image = '<div class=\'resourceOthers\'></div>';
        tooltipText = 'Other';
    }
    var tdResourceType = args.Element.children[4];
    $(tdResourceType).html(image);
    if (tooltipText != null) {
        $(tdResourceType).attr("title", '~' + tooltipText);
        $(tdResourceType).tooltip({ showBody: "~" });
    }

    image = '';
    var htmlTextReviewStatusLnr = null;
    if (args.Data.ReviewStatusLnr == "NewForReview") {//New For Review:Blue
        htmlTextReviewStatusLnr = "New For Review";
        image = '<div class=\'flagBlue\'></div>';
    }
    else if (args.Data.ReviewStatusLnr == "FinalForReview") {//Final For Review:Orange
        htmlTextReviewStatusLnr = "Final For Review";
        image = '<div class=\'flagOrange\'></div>';
    }
    else if (args.Data.ReviewStatusLnr == "Final") {//Final:Green
        htmlTextReviewStatusLnr = "Final";
        image = '<div class=\'flagGreen\'></div>';
    }
    var tdReviewStatus = args.Element.children[5];
    $(tdReviewStatus).html(image);
    if (htmlTextReviewStatusLnr != null) {
        $(tdReviewStatus).attr("title", '~' + htmlTextReviewStatusLnr);
        $(tdReviewStatus).tooltip({ showBody: "~" });
    }

    image = '';
    var htmlText = '';
    if (args.Data.LinkedContract != null && args.Data.LinkedContract != '') {
        if (args.Data.LinkedContract == "1") {
            image = '<div class=\'rightslinkedContract\'></div>';
            htmlText = args.Data.LinkedTooltip;
        }
        else if (args.Data.LinkedContract == "2") {
            image = '<div class=\'rightsSplit\'></div>';
            htmlText = args.Data.LinkedTooltip;
        }
        else if (args.Data.LinkedContract == "3") {
            image = '<div class=\'rightsMAC\'></div>';
            htmlText = "Multi Artist Compilation";
        }
        else if (args.Data.LinkedContract == "4") {
            image = '<div class=\'rightsSAC\'></div>';
            htmlText = "Single Artist Release";
        }
    }

    var tdLinkedContract = args.Element.children[13];
    $(tdLinkedContract).html(image);
    if (htmlText != null && htmlText != '') {
        $(tdLinkedContract).attr("title", '~' + htmlText);
        $(tdLinkedContract).tooltip({ showBody: "~" });
    }

    image = '';
    var htmlError = null;
    if (args.Data.Error != null && args.Data.Error != '') {
        if (args.Data.Error != null) {
            image = '<div class=\'errorImageRights\'></div>';
            htmlError = args.Data.Error;
        }
    }
    var tdError = args.Element.children[21];
    $(tdError).html(image);
    if (htmlError != null && htmlError != '') {
        $(tdError).attr("title", '~' + htmlError);
        $(tdError).tooltip({ showBody: "~" });
    }

    var tdRightsType = args.Element.children[18];
    if (args.Data.LostRightsLnr == "Y") {
        $(tdRightsType)[0].style.backgroundColor = "#FFBFBF";  // Light Color When Error Show append "FFE9E8";
       // $(tdRightsType)[0].style.fontWeight = "bold";
    }

    var tdSampleExists = args.Element.children[16];
    if (args.Data.SampleDescription != null && args.Data.SampleDescription != "") {
        $(tdSampleExists).attr("title", '~' + args.Data.SampleDescription);
        $(tdSampleExists).tooltip({ showBody: "~" });
    }

    var tdTerritorialRights = args.Element.children[20];
    if (args.Data.TerritorialRightsLnr == "" || args.Data.TerritorialRightsLnr == null || args.Data.TerritorialRightsLnr == "null") {
        $(tdTerritorialRights).html("");
    }
}


function callBulkEditDigitalButton() {
        HideWarningSuccess();
        digiGridName = $('#hdnGridId').val();
        var bulkEditRecordlength = $find(digiGridName).get_SelectedRecords().length;
        if (bulkEditRecordlength >= 1) {
            var objBulkEditDigital = $('<div class="divobjBulkEditDigital"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Bulk Edit - Digital Restrictions',
            show: 'clip',
            hide: 'clip',
            width: 900,
            height: 550,
            // width: getPopupWidth(75, 575),
            // height: getPopupHeight(75, 440, 40),
            // minHeight: 440,
            // minWidth: 575,
            // position: [getXPosition(65, 78), getYPosition(90, 40)],
            //            resize: function (event, ui) {
            //                if ($(".divobjBulkEditDigital").is(':visible')) {
            //                    var TotDiaHgt = $(".divobjBulkEditDigital").height();
            //                    var ReduceHgt = TotDiaHgt - 10;
            //                    //$("#rightsAcquiredBulkEditTab").css('height', ReduceHgt + "px");
            //                }
            //            },
            resizable: false,
            close: function () {
                var currentContext = this;
                setTimeout(function () { $(currentContext).remove(); }, 10);
            }
        });
//            objBulkEditDigital.load('/GCS/RightsReviewWorkQueue/DigitalRestrictionsBulkEdit?HasDelete=1', "",
//                        function (responseText, textStatus) {
//                            if (textStatus == 'error') {
//                                objBulkEditDigital.html('<p>' + messageCommon.error + '</p>');
//                            }
        //                        });
        $.get('/GCS/RightsReviewWorkQueue/DigitalRestrictionsBulkEdit?HasDelete=1', function (data) {
            objBulkEditDigital.html(data);
            objBulkEditDigital.dialog('open');
        }).error(function () {
            objBulkEditDigital.html('<p>' + messageCommon.error + '</p>');
            objBulkEditDigital.dialog('open');
        });
            
        } else {
        ShowWarning('Please select Repertoire');
            DigRestSyncfusionGridScroll();
        }
}

function DigRestDigitalLoad(sender, args) {

    var totCount = sender.get_TotalRecordsCount();
    $("#" + sender._ID).find(".MsgBar").html("<div class='alignLeft' style='margin: 3px 15px 0 -2px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input type='button' id='bulkEditDigitalRestrictions' onClick='callBulkEditDigitalButton()' style='float: left;' value='Bulk Edit' class='primbutton'><div class='primbutton_right'></div></div>");
   // callBulkEditDigitalButton();
    //$("#digitalGrid .GridHeader thead").find("tr > th:nth-child(2)").css("display", "none");
    var chk = $("#" + sender._ID + " .GridHeader").find("th.HeaderCell .HeaderCellDiv")[2];
    $(chk).append("<input type=\"Checkbox\" id=\"chkAllDigRest\" onclick=\"DigRestCheckBoxAllClick(event)\"> All");

    $('#' + sender._ID + '_toolbar').hide();
    $("#" + sender._ID + " .manualPagerLabel:eq(1)").empty();
    $("#" + sender._ID + " .manualPagerLabel:eq(1)").text("Show item per page");
    $find(sender._ID).get_LocalizedStrings().bulkeditalert = "You have unsaved changes pending. If you refresh the workqueue you will lose these changes. Are you sure you want to proceed?";
    digitalStrip(sender._ID);
}

function DigRestMergeRow(sender, args) {
    if (args.GridCell.Column.Member == "ReviewStatusLnr") {
        if (args.GridCell.Data.ReviewStatusPermitted == "N") {
            disableEditing(args.GridCell.Element);
        }
    }
    if (args.GridCell.Column.Member == "Action" || args.GridCell.Column.Member == "UseTypeLnr" || args.GridCell.Column.Member == "CommercialModelsLnr" || args.GridCell.Column.Member == "RestrictionLnr" || args.GridCell.Column.Member == "RestrictonReasonLnr" || args.GridCell.Column.Member == "ConsentPeriodLnr" || args.GridCell.Column.Member == "NotesLnr") {
        if (args.GridCell.Data.RightsEditPermitted == "N") {
            disableEditing(args.GridCell.Element);
        }
    } 
    
    if (args.GridCell.Column.Member == "RestrictonReasonLnr") {
        if (args.GridCell.Data.RestrictionReasonForOthers != "") {
            $(args.GridCell.Element).html(args.GridCell.Data.RestrictionReasonForOthers); 
        }
    }


    if (args.GridCell.Column.Member == "UPCId") {
        args.GridCell.Data.UPCId == null ? args.GridCell.Data.UPCId = "" : args.GridCell.Data.UPCId = args.GridCell.Data.UPCId;
    }

    if (args.GridCell.Column.Member == "ISRCId") {
        args.GridCell.Data.ISRCId == null ? args.GridCell.Data.ISRCId = "" : args.GridCell.Data.ISRCId = args.GridCell.Data.ISRCId;
    }

    if (args.GridCell.Column.Member == "MergeCountLnr") {
        if (args.GridCell.Text.toString() != "0") {
            args.SetRange(parseInt(args.GridCell.Text.toString()), 1);
        }
    }
    var chkAllColumn = "False";
    if (args.GridCell.Column.Member == " ") {
        var chkInput = args.GridCell.Element.children;
        var checkboxid = $(chkInput)[0].getAttribute("id");
        if (checkboxid == "checkboxDigitalchild") {
            chkAllColumn = "True";
            disableEditingCheck(args.GridCell.Element);
        }
        
    }
    if (chkAllColumn == "True" || args.GridCell.Column.Member == "ResourceType" || args.GridCell.Column.Member == "RightsTypeLnr"  || args.GridCell.Column.Member == "SideArtistLnr" || args.GridCell.Column.Member == "SampleExistsLnr" || args.GridCell.Column.Member == "All" || args.GridCell.Column.Member == "ModifiedDate" || args.GridCell.Column.Member == "AdminCompany" || args.GridCell.Column.Member == "RightSetIdLnr" || args.GridCell.Column.Member == "MergeCountLnr" || args.GridCell.Column.Member == "ReviewStatusLnr" || args.GridCell.Column.Member == "ISRCId" || args.GridCell.Column.Member == "UPCId" || args.GridCell.Column.Member == "Artist" || args.GridCell.Column.Member == "Title" || args.GridCell.Column.Member == "VersionTitle" || args.GridCell.Column.Member == "ReleaseDate" || args.GridCell.Column.Member == "PYear" || args.GridCell.Column.Member == "LinkedContract" || args.GridCell.Column.Member == "ReviewReasonLnr" || args.GridCell.Column.Member == "ReviewReasonLnr" || args.GridCell.Column.Member == "TerritorialRightsLnr") {
        if (args.GridCell.Data.MergeCountLnr != "0") {
            args.SetRange(parseInt(args.GridCell.Data.MergeCountLnr), 1);
        }
    }
}

function DigRestActionSuccess(sender, args) {
    $('[id*=chkAll]').removeAttr("indeterminate");
    $('[id*=chkAll]').removeAttr('checked');
    syncGridPagerSearchWQ(sender._ID);
    if (args.RequestType == "Save") {
       ShowSuccess(window.digitalMessages.saveSuccess);
        //displayDialog("Success", "&nbsp;&nbsp;&nbsp;" + window.digitalMessages.saveSuccess);
       DigRestSyncfusionGridScroll();
       customMenu();
      //pageScrollRightsWorkQueue();
    }
    if (args.RequestType == "sorting") {
        if (sortType == "DESC") {
            if (sortColumn == "ISRCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[7].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div><span class='Ascending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[8].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            } else if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[8].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Ascending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[7].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
            }
        }
        else if (sortType == "ASC") {
            if (sortColumn == "ISRCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[7].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div><span class='Descending'></span>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[8].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            }
            else if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[7].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[8].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Descending'></span>";
            }
        }
        else {
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[8].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[7].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
        }
    }
    else {
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[8].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' id='" + sender._ID + "_UpcTooltip' title='Click on UPC to create a release WQ for that UPC.'></div>";
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[7].childNodes[0].innerHTML = "ISRC<div class='resourceInfoHeader' id='" + sender._ID + "_IsrcTooltip' title='Click on ISRC to create a release WQ for all UPCs containing that ISRC'></div>";
    }
    DigRestSyncfusionGridScroll();
    var totCount = sender.get_TotalRecordsCount();
    setSyncGridToolTip("#" + sender._ID + "_Table");
    $(".resourceInfoHeader").tooltip();
    digitalRestrictionsCheckList();
    $("#" + sender._ID).find(".MsgBar").html("<div class='alignLeft' style='margin: 3px 15px 0 -2px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input type='button' id='bulkEditDigitalRestrictions' onClick='callBulkEditDigitalButton()' style='float: left;' value='Bulk Edit' class='primbutton'><div class='primbutton_right'></div></div>");
    digitalRestrictionsCheckList(); 
    // callBulkEditDigitalButton();
    ScrollBarMovement(sender._ID);
    AddedLastRowCell(sender, 20);
    var num = sender._ID.replace(/[^0-9]/g, '');
    if (num != '' && num != undefined) {
        $find(sender._ID)._MvcTable.get_parentTable().hideColumnByIndex(18); // UC 19 - Column hiding  
    }
}


function DigRestBegin(sender, args) {
    $('#hdnGridId').val(sender._ID);
    digiGridName = sender._ID;
   // $('#loadingDiv').hide();
    $("[id*='waiting_WaitingPopup']").hide();
    //var num = sender._ID.slice(-1);
    var num = sender._ID.replace(/[^0-9]/g, '');
    
    var tabIndex = $("#" + "hiddenCusomTabIndex" + num).val();


    $("#hdnDigRestGridId").val(sender._ID);

    $("#" + sender._ID + " .MsgBar").empty();
    // $("#" + sender._ID + " .MsgBar").text("Search Results ( )");
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
        args.data["filterParams"] = JSON.stringify(window.tabCustomParams[num - 2]);
        args.data["tabIndex"] = tabIndex;
    }
}

/*On Grid Error(Action Error Of syncFusion)*/
function resourceDigitalError(sender, args) {

    if (args.XMLHttpRequest.responseText != null) {
        //var errorVal = args.XMLHttpRequest.responseText.split("<title>")[1].split("</title>")[0];
        //ShowWarning("Error Retreiving WorkQueue Data");
        displayDialog("Error", "&nbsp;&nbsp;&nbsp;Error Retreiving WorkQueue Data");
        DigRestSyncfusionGridScroll();
        $('#loaderPanel').hide();
        $('#loadingDiv').hide();
    }
}

function DigRestSyncfusionGridScroll() {
    digiGridName = $('#hdnGridId').val();
    var num = digiGridName.replace(/[^0-9]/g, '');
    var ieWidth = 0;
    if ($.isNumeric(num))
        ieWidth = 15;

    if ($('#' + digiGridName).length > 0) {
        var pagerHgt = $("#" + digiGridName + " .GridPager").height();
        var headerHgt = $("#" + digiGridName + " .GridHeader").height();
        var browsHgt = 0;
        if ($.browser.msie)
            browsHgt = 16 + ieWidth;
        else
            browsHgt = 20;
        var reduceHgt = pagerHgt + headerHgt + browsHgt;

        var jtableTop = $("#" + digiGridName).offset().top;
        var topfootPos = $(".footer").offset().top;
        var totRecHeight = $("#" + digiGridName + "_Table").height() + reduceHgt;
        var tableHeight = topfootPos - jtableTop;
        var gridObj = $find(digiGridName);
        if (totRecHeight >= tableHeight)
            DigRestsetSyncfusionGridHeight(gridObj, tableHeight - reduceHgt);
        else
            DigRestsetSyncfusionGridHeight(gridObj, totRecHeight - reduceHgt + 20);
    }
}
function DigRestsetSyncfusionGridHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}




/**********  Plus Functionality **********/
// to verify dynamic id appending
function DigRestPlusClick1(element) {
    $('#loadingDiv').show();
    setTimeout(function () {
        var digRestGridId = $("#hdnDigRestGridId").val();
        var tablePositiontop = $("#" + digRestGridId + " .GridContent").children("div:first").css("top");
        var tablePositionLeft = $("#" + digRestGridId + " .GridContent").children("div:first").css("left");
        var vscrollposition = $("#" + digRestGridId + " .sf-sp-Vhandle").css("top");
        var hscrollposition = $("#" + digRestGridId + " .sf-sp-Hhandle").css("left");
        var hscrollHeader = $("#" + digRestGridId + " .GridHeader .Table").css("left");

        if ($(element.parentNode).hasClass('disabledElement')) {
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return false;
        }

        var clickrow = element.parentNode.parentNode;

        var rowindex = $(clickrow).index();
        $find(digRestGridId).jsonModeMgr._AddAction();

        var newinsertTr = $("#" + digRestGridId + "_Table").find("tr")[0];
        $(newinsertTr).find('.disabledElement').removeClass('disabledElement');

        var addedRecords = $find(digRestGridId)._edit._addedRecords;
        if (addedRecords.length > -1) {
            addedRecords[addedRecords.length - 1].RightsEditPermitted = 'Y';
        }
        $(newinsertTr).children("td.RightsEditPermitted").html('Y');

        var td = $(newinsertTr).children("td.RowCell")[0];
        var columnValue = DigRestGetRightSetID(rowindex + 1, digRestGridId);
        $(td).find("#RightSetIdLnr").val(columnValue[0]);
        $(newinsertTr).find("td.resourceType").html(columnValue[1]);
        $("#" + $("#hdnDigRestGridId").val()).mousedown();

        var tomodDate = $(newinsertTr).children("td.RowCell")[5];
        $(tomodDate).click();
        var modifiedDate = DigRestGetModifiedDate(rowindex + 1, digRestGridId);
        $(tomodDate).find("#ModifiedDate").val(modifiedDate);
        $("#" + $("#hdnDigRestGridId").val()).mousedown();
        /* Removed updated cell red color flag for parent row - start*/
        var delUpdatedCells = $(newinsertTr).find("td.RowCell").slice(0, 20);
        $(delUpdatedCells).removeClass("updatedCell");
        /*end*/

        InsertedNewDataUpdate(clickrow, newinsertTr);
        var toHideMergeTd = $("#" + digRestGridId + "_Table").find('tr:eq(0)').children("td.RowCell").slice(0, 20);
        $(toHideMergeTd).addClass("Merged");
        var toRowindex = $(newinsertTr).index();
        DigRestmoveRow(toRowindex, rowindex, digRestGridId);
        DigRestAddandDeleteRow(rowindex, "Add", digRestGridId);
        DigRestSyncfusionGridScroll(digRestGridId);

        var totRows = $("#" + digRestGridId + "_Table").find("tr");
        if (rowindex + 1 == totRows.length - 1) {
            var lastrowIndex = rowindex + 1;
            var lastTablerow = $("#" + digRestGridId + "_Table").find('tr')[lastrowIndex];
            $(lastTablerow).find("td").addClass("LastRowCell");
        }

        $("#" + digRestGridId + " .GridContent").children("div:first").css("top", tablePositiontop);
        $("#" + digRestGridId + " .GridContent").children("div:first").css("left", tablePositionLeft);
        $("#" + digRestGridId + " .sf-sp-Vhandle").css("top", vscrollposition);
        $("#" + digRestGridId + " .sf-sp-Hhandle").css("left", hscrollposition);
        $("#" + digRestGridId + " .GridHeader .Table").css("left", hscrollHeader); $('#loadingDiv').hide(); $('#loaderPanel').hide();
    }, 50);
    
}
function DigRestPlusClick(element) {
    
    if ($(element.parentNode).hasClass('disabledElement')) {
        return false;
    }
    var clickrow = element.parentNode.parentNode;
    var rowindex = $(clickrow).index();
    var row = $(element).parents('tr');
    ResouceAddNewRow(rowindex, row);
    var digRestGridId = $("#hdnDigRestGridId").val();
    DigRestSyncfusionGridScroll(digRestGridId);
}
//this function return TR which is visible in the UI for the resource/track/release
function getParentRow(rowindex, flag) {
     
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
function ResouceAddNewRow(rowindex, trow) {
    //set mergedcolumnindex
    var mergeColumnIndex = 20;     
    // Get Grid Id from hidden input box
    var digRestGridId = $("#hdnGridId").val();
    // Get Grid object
    var gridObj = $find(digRestGridId);

    // get selected rows objects from synfusion   
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
        trow = getParentRow(index, isChildRow)
         
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
    //var enablechkSelection = $("#" + digRestGridId + "_Table").find("tr:eq(" + rowindex + 1 + ")").children()[3];
    setTimeout("enablenewRowSelection(" + rowindex + ")", 1000);
}

function InsertedNewDataUpdate(curRowTrDel, newinsertTr) {
    var deleltedParentTd = $(curRowTrDel).children("td.RowCell").slice(3, 20);
    var newinsertedHtml = $(newinsertTr).children("td.RowCell").slice(3, 20);
    for (var i = 0; i <= deleltedParentTd.length; i++) {
        var deltdVal = deleltedParentTd[i];
        var tdhtml = $(deltdVal).html();
        var inserTd = $(newinsertedHtml)[i];
        $(inserTd).html(tdhtml);
    }
}
function DigRestAddandDeleteRow(curindex, status, digRestGridId) {
    var trs = $("#" + digRestGridId + "_Table").find('tr');
    var delFlag = "false";

    var firstrow = $("#" + digRestGridId + "_Table").find("tr")[curindex];
    var delrow = $(firstrow).children("td:eq(2)").html();

    if (parseInt(curindex) == 0 || (parseInt(delrow) != 0 && parseInt(delrow) != null))
        delFlag = "true";

    for (var i = curindex; i >= 0; i--) {
        var extTr = $("#" + digRestGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        if (parseInt(mergeVal) > 0) {
            var mergeIndex = i;

            if (status == "Add") {
                var rowspanRows = $("#" + digRestGridId + "_Table").find('tr')[i];
                $(rowspanRows).children("td.RowCell").slice(0, 20).attr("rowspan", parseInt(mergeVal) + 1);
                //$(rowspanRows).find("td[rowspan]").attr("rowspan", parseInt(mergeVal) + 1)
                $(td).html(parseInt(mergeVal) + 1);
                //$("#" + digRestGridId + "_Table").find("tr:eq(1)").children("td.RowCell:eq(0)").html();
                var originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
                var orgVal = $(originalParent).children("td.RowCell")[0];
                var rightSetValue = $(orgVal).html();
                var originalchild = $("#" + digRestGridId + "_Table").find("tr")[parseInt(curindex)];
                var orgChildVal = $(originalchild).children("td.RowCell")[0];
                var rightSetChildValue = $(orgChildVal).html();
            }
            else {
                if (delFlag == "true") {
                    var rowspanRowsTemp = $("#" + digRestGridId + "_Table").find('tr')[i + 1];
                    var nextrowFlag = $(rowspanRowsTemp).is(":visible");
                    if (nextrowFlag == false) {
                        var nextRowIndex = i + 1;
                        nextRowDeleteReMerge(nextRowIndex, parseInt(mergeVal), digRestGridId, "currentrowdelete");
                        break;
                    }
                    var firstRowTd = $(rowspanRowsTemp).find("td.RowCell").slice(0, 20);
                    $(firstRowTd).find('td.rightsError').html($($("#" + digRestGridId + "_Table").find('tr')[i]).find('td.rightsError').html());
                    $(firstRowTd).attr("rowspan", parseInt(mergeVal) - 1);
                    $(firstRowTd).removeClass("Merged");
                    var mergeTd = $(rowspanRowsTemp).children("td.RowCell")[1];
                    $(mergeTd).html(parseInt(mergeVal) - 1);
                }
                else {
                    var rowspanTr = $("#" + digRestGridId + "_Table").find('tr')[i];
                    $(rowspanTr).find("td.RowCell").slice(0, 20).attr("rowspan", parseInt(mergeVal) - 1);
                    $(td).html(parseInt(mergeVal) - 1);
                }
            }
            break;
        } else {
            if (status == "Add") {
                var curRowTr = $("#" + digRestGridId + "_Table").find('tr')[curindex];
                var isTrRowSpan = $(curRowTr).children("td.RowCell")[1];
                var currowspanVal = $(isTrRowSpan).html();
                var isMerged = $(isTrRowSpan).hasClass('Merged');
                if (parseInt(currowspanVal) == "0" && isMerged == false) {
                    var currentRowTd = $(curRowTr).find("td.RowCell").slice(0, 20);
                    $(currentRowTd).attr("rowspan", 2);
                    $(isTrRowSpan).html("2");
                    break;
                }
            }
        }
    }
}

function nextRowDeleteReMerge(nextRowIndex, mergeVal, digRestGridId, status) {
    var tablelength = $("#" + digRestGridId + "_Table").find('tr').length;
    if (status != "newinsertdelete")
        nextRowIndex = parseInt(nextRowIndex) + 1;
    for (var i = nextRowIndex; i <= tablelength; i++) {
        var extTr = $("#" + digRestGridId + "_Table").find('tr')[i];
        var nextRowFlag = $(extTr).is(":visible");
        if (nextRowFlag == true) {
            var nextrowTd = $(extTr).find("td.RowCell").slice(0, 20);
            $(nextrowTd).attr("rowspan", parseInt(mergeVal) - 1);
            $(nextrowTd).removeClass("Merged");
            $(nextrowTd).find('td.rightsError').html($($("#" + digRestGridId + "_Table").find('tr')[nextRowIndex - 2]).find('td.rightsError').html());
            var mergeTd = $(extTr).children("td.RowCell")[1];
            $(mergeTd).html(parseInt(mergeVal) - 1);
            break;
        }
    }
}
function DigRestGetRightSetID(curindex, digRestGridId) {
    var columnData=[];
    for (var i = curindex; i >= 0; i--) {
        var extTr = $("#" + digRestGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        var originalParent, orgVal;
        if (parseInt(mergeVal) > 0) {
            originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
            orgVal = $(originalParent).children("td.RowCell")[0];
            columnData.push($(orgVal).html());
            columnData.push($($(originalParent).children("td.RowCell")[28]).html());
            break;
        } else {
            originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
            var rowMerge = $(extTr).children("td.RowCell")[1];
            var rowMergeVal = $(rowMerge).html();
            var isMerged = $(rowMerge).hasClass('Merged');
            if (parseInt(rowMergeVal) == 0 && isMerged == false) {
                orgVal = $(originalParent).children("td.RowCell")[0];
                columnData.push($(orgVal).html());
                columnData.push($($(originalParent).children("td.RowCell")[28]).html());
                break;
            }
        }
    }
    return columnData;
}

function DigRestGetModifiedDate(curindex, digRestGridId) {
    var modifiedDate;
    for (var i = curindex; i >= 0; i--) {
        var extTr = $("#" + digRestGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        var originalParent, orgVal;
        if (parseInt(mergeVal) > 0) {
            originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
            orgVal = $(originalParent).children("td.RowCell")[5];
            modifiedDate = $(orgVal).html();
            break;
        } else {
            originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
            var rowMerge = $(extTr).children("td.RowCell")[1];
            var rowMergeVal = $(rowMerge).html();
            var isMerged = $(rowMerge).hasClass('Merged');
            if (parseInt(rowMergeVal) == 0 && isMerged == false) {
                orgVal = $(originalParent).children("td.RowCell")[5];
                modifiedDate = $(orgVal).html();
                break;
            }
        }
    }
    return modifiedDate;
}

function DigRestmoveRow(oldPosition, newPosition, digRestGridId) {
    var row = $("#" + digRestGridId + "_Table").find('tr').eq(oldPosition).remove();
    $("#" + digRestGridId + "_Table").find('tr').eq(newPosition).after(row);
}

function DigRestDeleteClick(element) {
        $('#loadingDiv').show();
        if ($(element.parentNode).hasClass("disabledElement")) {
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return false;
        }
        var digRestGridId = $("#hdnDigRestGridId").val();
        var gridObj = $find(digRestGridId);
        
        var row = element.parentNode.parentNode;
        var rowindex = $(row).index();

        $("#" + digRestGridId + "_Table > tbody > tr > td").removeClass('SelectionBackground');
        gridObj.get_SelectionManager().selectedRowsCollection = new Array();
        var insertedRowFlag = $(row).hasClass("InsertedRow");
        var insertCnt, deletedIndex;
        if (insertedRowFlag == false)
            insertCnt = $(row).prevAll(".InsertedRow").length;
        Array.add(gridObj.get_SelectionManager().selectedRowsCollection, rowindex);
        $(row).children().addClass("SelectionBackground");
        var curRowTrDel = $("#" + digRestGridId + "_Table").find('tr')[rowindex];
        var isTrRowSpanDel = $(curRowTrDel).children("td.RowCell")[1];
        var currowspanDelVal = $(isTrRowSpanDel).html();
        var isMergedDel = $(isTrRowSpanDel).hasClass('Merged');
        if (parseInt(currowspanDelVal) == "0" && isMergedDel == false) {
            if (insertedRowFlag == true) {
                $('#loadingDiv').hide(); $('#loaderPanel').hide();
                return;
            }
            var nullChildTd = $(curRowTrDel).children()[30];
            var nullChildrenVal = $(nullChildTd).html();
            if (nullChildrenVal == "-1" || nullChildrenVal == -1)
                RemoveUpdatedSingleParentRow(row);
            else {
                SingleChildDelete(digRestGridId, gridObj, curRowTrDel, rowindex, insertCnt);
                gridObj.get_SelectionManager().selectedRowsCollection = new Array();
                AddedLastRowCellInDeleteClick(gridObj);
            }
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return;
        }
        else if (parseInt(currowspanDelVal) == "1" && isMergedDel == false && insertedRowFlag == true) {
            RemoveRuntimeInsertedRowAsParentRow(row);
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            return;
        }
        var rowSpanTest = $("#" + digRestGridId + "_Table").find('tr')[rowindex];
        var rowSpanTestTd = $(rowSpanTest).children("td.RowCell")[1];
        var rowSpanVal = $(rowSpanTestTd).html();
        if (parseInt(rowSpanVal) == 1) {
            SingleChildDelete(digRestGridId, gridObj, curRowTrDel, rowindex, insertCnt);
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            gridObj.get_SelectionManager().selectedRowsCollection = new Array();
            AddedLastRowCellInDeleteClick(gridObj);
            return;
        }
        else {
            var insertRowFlag = $(rowSpanTest).hasClass("InsertedRow");
            if (insertRowFlag == true) {
                if (parseInt(rowSpanVal) > 0) {
                    gridObj.sendDeleteRequest();
                    nextRowDeleteReMerge(rowindex, rowSpanVal, digRestGridId, "newinsertdelete");
                    $('#loadingDiv').hide(); $('#loaderPanel').hide();
                    gridObj.get_SelectionManager().selectedRowsCollection = new Array();
                    AddedLastRowCellInDeleteClick(gridObj);
                    return;
                }
                else if (parseInt(rowSpanVal) == 0) {
                    gridObj.sendDeleteRequest();
                    addedRowRemerge(digRestGridId, rowindex - 1);
                    $('#loadingDiv').hide(); $('#loaderPanel').hide();
                    gridObj.get_SelectionManager().selectedRowsCollection = new Array();
                    AddedLastRowCellInDeleteClick(gridObj);
                    return;
                }
            }
            var addedcollection = gridObj._edit._addedRecords;
            gridObj._edit._addedRecords = new Array();
            ToAddInsertedRowsLength(insertCnt, gridObj);
            gridObj.sendDeleteRequest();
            gridObj._edit._addedRecords = new Array();
            gridObj._edit._addedRecords = addedcollection;
            DigRestAddandDeleteRow(rowindex, "Delete", digRestGridId);
            gridObj.get_SelectionManager().selectedRowsCollection = new Array();
            AddedLastRowCellInDeleteClick(gridObj);
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
        }
    }

    function AddedLastRowCellInDeleteClick(gridObj) {
        var trslength = $(gridObj._gridContentTable).find("tr").length;
        trslength = trslength - 1;
        for (var i = trslength; i >= 0; i--) {
            var nextTr = $(gridObj._gridContentTable).find("tr")[i];
            if ($(nextTr).is(":visible")) {
                var hasLastRowTd = $(nextTr).children("td.RowCell")[23];
                if (!$(hasLastRowTd).hasClass("LastRowCell")) {
                    var nextTds = $(nextTr).children("td");
                    $(nextTds).removeClass("LastRowCell");
                    $(nextTds).addClass("LastRowCell");
                }
                else
                    break;
                break;
            }
        }
    }
function ToAddInsertedRowsLength(insertCnt, gridObj) {
 for (var i=0; i < insertCnt; i++) {
     gridObj._edit._addedRecords.push(i);
 }
}
function RemoveUpdatedSingleParentRow(row) {
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
    var digRestGridId = $("#hdnDigRestGridId").val();
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

function addedRowRemerge(digRestGridId, rowindex) {
   for (var i = rowindex; i >= 0; i--) {
        var extTr = $("#" + digRestGridId + "_Table").find('tr')[i];
        var td = $(extTr).children("td.RowCell")[1];
        var mergeVal = $(td).html();
        var originalParent;
        if (parseInt(mergeVal) > 0) {
             originalParent = $("#" + digRestGridId + "_Table").find("tr")[i];
             var nextrowTd = $(originalParent).find("td.RowCell").slice(0, 20);
             $(nextrowTd).attr("rowspan", parseInt(mergeVal) - 1);
             var mergeTd = $(extTr).children("td.RowCell")[1];
             $(mergeTd).html(parseInt(mergeVal) - 1);
             break;
         }
    }
 }

 function RemoveRuntimeInsertedRowAsParentRow(row) {
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
     var digRestGridId = $("#hdnDigRestGridId").val();
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
 
function SingleChildDelete(digRestGridId, gridObj, curRowTrDel, rowindex, insertCnt) {
    var tablePositiontop = $("#" + digRestGridId + " .GridContent").children("div:first").css("top");
    var tablePositionLeft = $("#" + digRestGridId + " .GridContent").children("div:first").css("left");
    var vscrollposition = $("#" + digRestGridId + " .sf-sp-Vhandle").css("top");
    var hscrollposition = $("#" + digRestGridId + " .sf-sp-Hhandle").css("left");
    var hscrollHeader = $("#" + digRestGridId + " .GridHeader .Table").css("left");

    var addedcollection = gridObj._edit._addedRecords;
    gridObj._edit._addedRecords = new Array();
    ToAddInsertedRowsLength(insertCnt, gridObj);
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

    var delUpdatedCells = $(newinsertTr).find("td.RowCell").slice(0, 20);
    $(delUpdatedCells).removeClass("updatedCell");
    var toRowindex = $(newinsertTr).index();
    DigRestmoveRow(toRowindex, rowindex, digRestGridId);
    //var enablechkSelection = $("#" + digRestGridId + "_Table").find("tr:eq(" + rowindex + 1 + ")").children()[3];
    //disableEditingCheck(enablechkSelection);

    var rightSetvaltd = $(curRowTrDel).children("td.RowCell")[0];
    var rightsetVal = $(rightSetvaltd).html();
    var td = $(newinsertTr).children("td.RowCell")[0];
    $(td).find("#RightSetIdLnr").val(rightsetVal);
    $("#" + $("#hdnDigRestGridId").val()).mousedown();

    var tomodDatevaltd = $(curRowTrDel).children("td.RowCell")[5];
    var tomodDateVal = $(tomodDatevaltd).html();
    var tomodDate = $(newinsertTr).children("td.RowCell")[5];
    $(tomodDate).click();

    if ($(tomodDate).find("#ModifiedDate").length == 0) {
        var dataindex = addedRecords.length - 1;
        if (dataindex > -1) {
            addedRecords[dataindex].ModifiedDate = tomodDateVal;
        }
    }
    else {
        $(tomodDate).find("#ModifiedDate").val(tomodDateVal);
    }
    $("#" + $("#hdnDigRestGridId").val()).mousedown();

    InsertedNewDataUpdate(curRowTrDel, newinsertTr);
    setTimeout("enablenewRowSelection(" + rowindex + ")", 1000);
    $("#" + digRestGridId + " .GridContent").children("div:first").css("top", tablePositiontop);
    $("#" + digRestGridId + " .GridContent").children("div:first").css("left", tablePositionLeft);
    $("#" + digRestGridId + " .sf-sp-Vhandle").css("top", vscrollposition);
    $("#" + digRestGridId + " .sf-sp-Hhandle").css("left", hscrollposition);
    $("#" + digRestGridId + " .GridHeader .Table").css("left", hscrollHeader);
}


//Check Box all click event
function DigRestCheckBoxAllClick(args) {
    digiGridName = $('#hdnGridId').val();
    var gridObj = $find(digiGridName);
    var obj = $("#" + digiGridName).find(".GridHeader #chkAllDigRest");
    var ischecked = false;
    if (obj.attr("checked") == "checked") {
        $("#" + digiGridName + " .GridContent").find('#checkboxDigitalchild').attr("checked", "checked");
        $("#" + digiGridName + "_Table > tbody > tr > td").addClass("SelectionBackground");
        ischecked = true;
    }
    else {
        $("#" + digiGridName + " .GridContent").find('#checkboxDigitalchild').removeAttr("checked");
        $("#" + digiGridName + "_Table > tbody > tr > td").removeClass("SelectionBackground");
        ischecked = false;
    }
    CheckAllClick(ischecked, gridObj, digiGridName);
}

// add /removed selected index in the Synfusion object 
function CheckAllClick(haschecked, gridObj, digiGridName) {
    if (haschecked) {
        gridObj.get_SelectionManager().selectedRowsCollection = new Array();
        var checkboxes = $("#" + digiGridName).find(".GridContent #checkboxDigitalchild");
        $.each(checkboxes, function (index, checkbox) {
            Array.add(gridObj.get_SelectionManager().selectedRowsCollection, index);
        });
    }
    else {
        var checkboxes = $("#" + digiGridName).find(".GridContent #checkboxDigitalchild");
        $.each(checkboxes, function (index, checkbox) {
            gridObj.get_SelectionManager().deselectRow(index);
        });
    }
    SetSelectedRecordsCollection(gridObj);
}



function digitalRestrictionsCheckList() {
    digiGridName = $('#hdnGridId').val();
    setTimeout(function () {
        var gridObj = $find(digiGridName);
        gridObj.set_AllowSelection(true);
        gridObj.set_AllowSelection(false);
    }, 0);
}

/* Create click event for Datagrid checkbox  */
/* and avoid other event. */
function disableEditingCheck(element) {
    $(element).click(function (event) {
        ClickCheckbox(event);
        event.stopPropagation();
        return true;
    });
}

function enablenewRowSelection(rowindex) {
    var gridid = $('#hdnGridId').val();
    var row = $("#" + gridid + "_Table").find("tr")[rowindex + 1];
    var checkbox = $(row).find("#checkboxDigitalchild");
    var element = checkbox.parent();
    $(element).click(function (event) {
        ClickCheckbox(event);
        event.stopPropagation();
        return true;
    });
}
/*if gird checkbox checked add/remvoe index from Synfucsion Grid object*/
function ClickCheckbox(event) {
    //var checkbox = $(event.target).find("#checkboxDigitalchild");
    if (event.target.id == "checkboxDigitalchild") {
        var checkbox = event.target;
        var row = event.target.parentNode.parentNode;
        var row_index = $(row).index();
        var gridid = $('#hdnGridId').val();
        var gridobj = $find(gridid);
        if (checkbox.checked) {
            Array.add(gridobj.get_SelectionManager().selectedRowsCollection, row_index);
            $(row).children().addClass("SelectionBackground");
            //$(checkbox).attr("checked", true);
             //RecordSelectionCheckAll();
            BulkEditCheckBox(row, true);
        }
        else {
            gridobj.get_SelectionManager().deselectRow(row_index);
            //$(checkbox).attr("checked", false);
            //UnRecordSelectionCheckAll();
            BulkEditCheckBox(row, false);
        }
        SetSelectedRecordsCollection(gridobj);
    }
}

function BulkEditCheckBox(row, hascheked) {
    var rowmergeTd, mergeCnt;
    //var index = $(row).index();
    var gridid = $('#hdnGridId').val();
    var index = $("#" + gridid + "_Table tr:visible").index(row);
    if (hascheked) {
        rowmergeTd = $(row).children()[2];
        mergeCnt = $(rowmergeTd).html();
        if (parseInt(mergeCnt) > 0)           
            BulkRowSelection(index, mergeCnt, true);
    } else {
        rowmergeTd = $(row).children()[2];
        mergeCnt = $(rowmergeTd).html();
        if (parseInt(mergeCnt) > 0)
            BulkRowSelection(index, mergeCnt, false);
    }
}

function BulkRowSelection(index, mergeCnt, checkEnable) {
    var gridid = $('#hdnGridId').val();
    index = index + 1;
    mergeCnt = mergeCnt - 1;
    for (var i = 0; i < mergeCnt; i++) {
        var row = $("#" + gridid + "_Table tr:visible")[index];
        var checkbox = $(row).find("#checkboxDigitalchild")[0];
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
function UnRecordSelectionCheckAll() {
    var gridid = $('#hdnGridId').val();
    var totalLength = $("#" + gridid + "_Table tr").length;
    var checked = $('#' + gridid + ' tr td').filter(':has(:checkbox:checked)').length;
    $("#chkAllDigRest").removeAttr("indeterminate");
    if (checked == 0)
        $("#chkAllDigRest").removeAttr('checked');
    else
        $("#chkAllDigRest").prop("indeterminate", true);
}

/* if Grid Checkbox nchecked based on that Over All check box will be checked or semi checked  */
function RecordSelectionCheckAll() {
    var gridid = $('#hdnGridId').val();
    var totalLength = $("#" + gridid + "_Table tr").length;
    var checked = $('#' + gridid + ' tr td').filter(':has(:checkbox:checked)').length;
    $("#chkAllDigRest").removeAttr("indeterminate");
    if (totalLength == checked)
        $("#chkAllDigRest").attr('checked', true);
    else
        $("#chkAllDigRest").prop("indeterminate", true);
}
 


function SetSelectedRecordsCollection(gridObj) {
    var result = gridObj.get_SelectedRecords();
    selectedGridItems = [];
    rightSetArray = [];
    $.each(result, function (k, value) {
        var isUnique = true;
        for (var i = 0; i < rightSetArray.length; i++) {
            if (rightSetArray[i] == value.RightSetIdLnr) {
                isUnique = false;
            }
        }
        if (isUnique) {
            rightSetArray.push(value.RightSetIdLnr);
            selectedGridItems.push(
                {
                    KeyId: getValue(getKeyValue(value.RepertoireId)),
                    R2KeyId: getR2KeyValue(value.R2ResourceId),
                    ContractId: getValue(value.ContractIdLnr),
                    LinkType: getValue(linkType),
                    RightSetId: getValue(value.RightSetIdLnr)
                });
        }
    });
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


function digitalStrip(gridID) {
    $("#" + gridID + " .HeaderBar th:lt(21)").attr('style', 'background-color: #B8D0E9 !important');
    $("#" + gridID + " .HeaderBar th:gt(21)").attr('style', 'background-color: #fcf190 !important');
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

