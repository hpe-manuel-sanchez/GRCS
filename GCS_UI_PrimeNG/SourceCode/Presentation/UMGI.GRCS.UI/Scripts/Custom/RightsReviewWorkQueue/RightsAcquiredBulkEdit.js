/// <reference path="/GCS/Scripts/Custom/RightsReviewWorkqueue/RightsReviewWorkqueue.js" />
/// <reference path="/GCS/Scripts/Custom/ManageTerritory.js" />
/// <reference path="../jquery-1.5.1-vsdoc.js" />
/// <reference path="../jquery-1.5.1.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/RightsReviewWorkqueue/RightsAcquiredTab.js" />
/// <reference path="/GCS/Scripts/Custom/RightsReviewWorkqueue/ReleaseRightsAcquired.js" />

var activeForMarketing, rightsPeriod, lostRights, lostRightsDate, rightsExpection, lostRightsReason, physicalExploitation, digitalExploitation, digitalUnbundled, mobileExploitation, ppbRevenue, tempRightsId, reviewStatusType, territoryRights;
var contractid = "0";
var isContractPropagated = "True";
var gridName = $('#hdnGridId').val();
var gridObj = $find(gridName);
var repertoireType = '';
var noChangeRequired = "No Change Required";
var valueZero = "0";
$(document).ready(function () {
    territoryRights = "";
    rightsIncludedC = "";
    rightsIncludedT = "";
    rightsExcludedC = "";
    rightsExcludedT = "";
    //To add watermark on blur function of lostRightDate
    var watermark = 'No Change Required';

    $('#lostRightsDate').blur(function () {
        if ($(this).val().length == 0)
            $(this).val(watermark).addClass('watermark');
    }).focus(function () {
        if ($(this).val() == watermark)
            $(this).val('').removeClass('watermark');
    }).val(watermark).addClass('watermark');

    $("#" + gridName + "waiting_WaitingPopup").hide();
    if (gridName.toString().indexOf('release') != -1) {
        repertoireType = 'release';
        $(".resourceCtrls").hide();
    }
    if (trEditType == "Single") {
        $("#rightsAcquiredBulkEditTab").tabs({ disabled: [0] });
        $("#rightsAcquiredBulkEditTab").tabs("select", 1);
        $('#ddlreviewStatus').attr('disabled', true);
        loadTRData("Single", trRightsId);
        $('.buttonContainer').hide();
    }
    else {
        loadTRData("Bulk", 0);
        $("#divTerritorialRightsPopupTabContent").hide();
    }

    setTimeout(function () {
        var TotDiaHgt = $("#rightsAcquiredBulkEditTab").height();
        var ReduceHgt = TotDiaHgt - 10;
        $("#rightsAcquiredBulkEditTab").css('height', ReduceHgt + "px");
    }, 300);

    bulkEditPopupMesages = window.rightsReviewWorkQueueMessages;
    $(".datefield").css("width", "100px");
    $(".datefield").datepicker({ showOn: 'both', buttonImage: "/GCS/Images/Calender_Icon_img.png", buttonImageOnly: true, changeMonth: true, changeYear: true, yearRange: '1900:2100' });
    $("#rightsAcquiredBulkEditTab").tabs({
        select: function (event, ui) {
            switch (ui.index) {
                case 0:
                    // first tab selected
                    $('#splitWarning').hide();
                    $('.commonParentContainer').hide();
                    $('#divTerritorialRightsPopupTab').hide();
                    break;
                case 1:
                    // second tab selected
                    $('.commonParentContainer').show();
                    $("#divTerritorialRightsPopupTabContent").show();
                    $('#splitWarning').hide();
                    if ($('#divTerritorialRightsPopupTabContent').html() == null || $('#divTerritorialRightsPopupTabContent').html() == "") {
                        loadTRData("Bulk", 0);
                    }
                    $('.buttonContainer').hide();
                    break;
            }
        }
    });


    /*Rights Data validations starts*/

    //Disables the digital Unbundling if digital exploitation is No
    $('#ddldigitalExploitation').change(function () {
        if ($(this).val() == "N") {
            $('#ddldigitalUnbundling').val("N");
            $('#ddldigitalUnbundling').attr('disabled', true);
        } else {
            $('#ddldigitalUnbundling').attr('disabled', false);
        }
    });



    /*Validation for lost Rights Date*/
    $('#lostRightsDate').change(function () {
        if (!validateLostRightsDate($('#lostRightsDate').val(), $('#ddllostRights').val())) {
            //if (!validateLostRightsDate($('#lostRightsDate').val(), $('#ddllostRights').val())) {
            $('#lostRightsDate').blur(function () {
                if ($(this).val().length == 0)
                    $(this).val(watermark).addClass('watermark');
            }).focus(function () {
                if ($(this).val() == watermark)
                    $(this).val('').removeClass('watermark');
            }).val(watermark).addClass('watermark');
            $('#splitWarning').show();
            $('.divWarningContainer').show();
            $('#SplitAlert').html(lostRightsDateErrorMessage);
        }
    });

    /*Validation for lost Rights Date*/
    $('#ddllostRights').change(function () {
        if (!validateLostRightsDate($('#lostRightsDate').val(), $('#ddllostRights').val())) {
            $('#lostRightsDate').blur(function () {
                if ($(this).val().length == 0)
                    $(this).val(watermark).addClass('watermark');
            }).focus(function () {
                if ($(this).val() == watermark)
                    $(this).val('').removeClass('watermark');
            }).val(watermark).addClass('watermark');
            $('#splitWarning').show();
            $('.divWarningContainer').show();
            $('#SplitAlert').html(lostRightsDateErrorMessage);
        }
    });


    /*Rights Data validations starts*/

});

function loadTRData(editType, rightsId) {
    $.get('/GCS/RightsReviewWorkQueue/TerritorialRights/', { trEditType: editType, rightSetId: rightsId }, function (data) {  // Sample RightSetId : 20002302
        $('#divTerritorialRightsPopupTabContent').html(data);
    });
}


//button Apply Changes
$("#btnApply").click(function () {
    if (trEditType != "Single") {
        currentRightsAcquiredValues();
        if (lostRights == "Y" && (lostRightsDate == "" || lostRightsDate == "No Change Required")) {
            $('#splitWarning').show();
            $('.divWarningContainer').show();
            $('#SplitAlert').html("Lost Rights Date Required");
            return false;
        }
        else {
            if (activeForMarketing != valueZero || rightsPeriod != noChangeRequired || lostRights != valueZero || lostRightsDate != noChangeRequired || rightsExpection != valueZero || lostRightsReason != noChangeRequired || physicalExploitation != valueZero || digitalExploitation != valueZero || digitalUnbundled != valueZero || mobileExploitation != valueZero || ppbRevenue != valueZero || reviewStatusType != noChangeRequired || territoryRights != "" || reviewStatusType != noChangeRequired) {
                if (checkStatusBulkConfirmation == "true") {
                    updateRightsAcquriedBulk();
                }
                else {
                    $("#divConfirmDialog").dialog('open');
                    $("#divConfirmDialog").dialog({ title: 'Bulk-Edit Confirmation', buttons: {
                        'Yes':
            function () {
                if ($("#chkFutureMsg").is(":checked")) {
                    window.checkStatusBulkConfirmation = "true";
                }
                $("#divConfirmDialog").dialog('close');
                updateRightsAcquriedBulk();
            },
                        'Cancel':
             function () {
                 $("#divConfirmDialog").dialog('close');
             }

                    }
                    });
                }

            }
            else {
                $('#splitWarning').show();
                $('.divWarningContainer').show();
                $('#SplitAlert').html("No Changes to apply");
            }
        }
    }
    else {
        updateRightsAcquriedBulk();
        $('.divobjBulkEditRightsAcquired').dialog('close');
    }
});

//button re-set territorial rights
$("#btnResetTerritorialRights").click(function () {
    ReSet();
    territoryRights = "";
});

//button re-set rights acquired
$('#btnResetRightsAcquired').click(function () {
    var watermark = 'No Change Required';
    $('#ddlactiveMarketing').val(0);
    $('#ddlrightsPeriod').val(0);
    $('#ddllostRights').val(0);
    $('#lostRightsDate').val("");
    $('#ddlrightsException').val(0);
    $('#ddllostRightsReason').val(0);
    $('#ddlphysicalExploitation').val(0);
    $('#ddldigitalExploitation').val(0);
    $('#ddldigitalUnbundling').val(0);
    $('#ddlmobileExploitation').val(0);
    $('#ddlppbClaim').val(0);
    $('#ddlreviewStatus').val(0);
    $('#lostRightsDate').blur(function () {
        if ($(this).val().length == "")
            $(this).val(watermark).addClass('watermark');
    }).focus(function () {
        if ($(this).val() == watermark)
            $(this).val('').removeClass('watermark');
    }).val(watermark).addClass('watermark');
});

//button cancel
$('#btnCancel').click(function () {
    $('.divobjBulkEditRightsAcquired').dialog('close');
});


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

// Bulk Edit data Append to table collection starts here
function updateRightsAcquriedBulk() {
    territoryRights = "";
    var gridDatas = gridObj.get_SelectedRecords();
    var territoryVals = "";
    if (trEditType == "Single") {
        isSingleTRmodified = true;
        territoryVals = setNewStringFormation();
        if (repertoireType == "release") {
            var td = $("#TerritorialRights").parent().parent();
            var oldVal = $("#" + gridName + "EditForm").find("#TerritorialRights").val();
            if (oldVal == territoryVals) {
                territoryVals = territoryVals + " ";
            }
            else if (territoryVals == "") {
                territoryVals = " ";
            }
            if (territoryVals.length > 254) {
                $("#" + gridName + "EditForm").find("#TerritorialRights").val(territoryVals.substring(0, 254));
                $(td).attr('title', territoryVals.substring(0, 254) + '...');
            }
            else {
                $("#" + gridName + "EditForm").find("#TerritorialRights").val(territoryVals);
                $(td).attr('title', territoryVals);
            }
            $("#" + gridName + "EditForm").find("#TerritorialRights").attr('disabled', 'disabled');
            $("#" + gridName + "EditForm").find("#TerritorialRights").focusout();
            $(td).addClass("updatedCell");
            $(td).removeClass("tdglobePlus");
            $(td).tooltip();
        }
        else {
            var oldVal = $("#" + gridName + "EditForm").find("#TerritoryRightsLnr").val();
            var td = $("#TerritoryRightsLnr").parent().parent();
            if (oldVal == territoryVals) {
                territoryVals = territoryVals + " ";
            }
            else if (territoryVals == "") {
                territoryVals = " ";
            }
            if (territoryVals.length > 254) {
                $("#" + gridName + "EditForm").find("#TerritoryRightsLnr").val(territoryVals.substring(0, 254));
                $(td).attr('title', territoryVals.substring(0, 254) + '...');
            }
            else {
                $("#" + gridName + "EditForm").find("#TerritoryRightsLnr").val(territoryVals);
                $(td).attr('title', territoryVals);
            }
            $("#" + gridName + "EditForm").find("#TerritoryRightsLnr").attr('disabled', 'disabled');
            $("#" + gridName + "EditForm").find("#TerritoryRightsLnr").focusout();

            $(td).addClass("updatedCell");
            $(td).removeClass("tdglobePlus");

            $(td).tooltip();
        }
        rightsIncludedT = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded && item.IsTerritory; }).Select(function (item) { return item.Id.toString(); }).items);
        rightsExcludedT = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsExcluded && item.IsTerritory; }).Select(function (item) { return item.Id; }).items);
        rightsExcludedC = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsExcluded && !item.IsTerritory; }).Select(function (item) { return item.Id; }).items);
        rightsIncludedC = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded && !item.IsTerritory; }).Select(function (item) { return item.Id; }).items);
    }
    else {
           currentRightsAcquiredValues();
            setTimeout(function () {
                appendData();
                for (var rightsAcqIndex = 0; rightsAcqIndex < gridDatas.length; rightsAcqIndex++) {
                    tempRightsId = gridDatas[rightsAcqIndex].RightSetId;
                    if (repertoireType == 'release') {
                        currentReleaseGridItems(tempRightsId);
                    }
                    else {
                        currentGridItems(tempRightsId);
                    }
                }
            }, 1000);
            gridObj._edit._isEdit = true;
            $('.divobjBulkEditRightsAcquired').dialog('close');
    }
    territoryVals = "";
}

/*Appends Data to Grid*/
function currentGridItems(tempId) {
    var rightsDatas = gridObj._edit._data;
    var replaceObj = false;
    for (var x = 0; x < rightsDatas.length; x++) {
        if (rightsDatas[x].RightsId == tempId) {
            if (rightsDatas[x].ReviewStatusPermitted != "N") {
                if (reviewStatusType != "No Change Required")
                    rightsDatas[x].ReviewStatusLnr = reviewStatusType;
            }
            if (rightsDatas[x].RightsEditPermitted != "N") {
                if (activeForMarketing != "0")
                    rightsDatas[x].ActiveForMrktLnr = activeForMarketing;
                if (rightsPeriod != "No Change Required")
                    rightsDatas[x].RightsPeriodLnr = rightsPeriod;
                if (lostRights != "0")
                    rightsDatas[x].LostRightsLnr = lostRights;
                if (!(rightsDatas[x].LostRightsLnr == "N" && rightsDatas[x].SensitiveInfoPermitted == "N")) {//|| (rightsDatas[x].LostRightsLnr == "Y" && rightsDatas[x].SensitiveInfoPermitted == "Y")) {
                    if (lostRightsDate != "" && lostRightsDate != "No Change Required") {
                        rightsDatas[x].LostRightsDateLnr = lostRightsDate;
                        rightsDatas[x].LostRightsDateLinear = lostRightsDate;
                    }
                }
                if (rightsExpection != "0")
                    rightsDatas[x].RightsExceptionLnr = rightsExpection;
                if (lostRightsReason != "No Change Required")
                    rightsDatas[x].LostRightsReasonLnr = lostRightsReason;
                if (physicalExploitation != "0")
                    rightsDatas[x].PhysicalExpLnr = physicalExploitation;
                if (digitalExploitation != "0")
                    rightsDatas[x].DigitalExpLnr = digitalExploitation;
                if (digitalUnbundled != "0")
                    rightsDatas[x].DigitalUnbundledLnr = digitalUnbundled;
                if (mobileExploitation != "0")
                    rightsDatas[x].MobileExpLnr = mobileExploitation;
                if (ppbRevenue != "0")
                    rightsDatas[x].PpbClaimLnr = ppbRevenue;
                if (territoryRights != "")
                    rightsDatas[x].TerritoryRightsLnr = territoryRights;
                if (rightsIncludedC != "")
                    rightsDatas[x].IncludedCountryLnr = rightsIncludedC;
                if (rightsExcludedC != "")
                    rightsDatas[x].ExcludedCountryLnr = rightsExcludedC;
                if (rightsIncludedT != "")
                    rightsDatas[x].IncludedTerritoryLnr = rightsIncludedT;
                if (rightsExcludedT != "")
                    rightsDatas[x].ExcludedTerritoryLnr = rightsExcludedT;
            }
            if (!(rightsDatas[x].RightsEditPermitted == "N") || !(rightsDatas[x].ReviewStatusPermitted == "N") || !(rightsDatas[x].SensitiveInfoPermitted == "N")) {
                if (gridObj._edit._updatedRecords.length > 0) {
                    for (var y = 0; y < gridObj._edit._updatedRecords.length; y++) {
                        if (gridObj._edit._updatedRecords[y].RightsId == tempId) {
                            gridObj._edit._updatedRecords[y] = rightsDatas[x];
                            replaceObj = true;
                        }
                    }
                }
                if (replaceObj == false)
                    gridObj._edit._updatedRecords.push(rightsDatas[x]);
            }
        }
    }
    gridObj._edit._data = rightsDatas;
}
// release
function currentReleaseGridItems(tempId) {
    var rightsDatas = gridObj._edit._data;
    var replaceObj = false;
    for (var x = 0; x < rightsDatas.length; x++) {
        if (rightsDatas[x].RightSetId == tempId) {
                if (rightsDatas[x].RightsEditPermitted != "N") {
                    if (rightsPeriod != "No Change Required")
                        rightsDatas[x].RightsPeriod = rightsPeriod;
                    if (lostRights != "0")
                        rightsDatas[x].LostRightsText = lostRights;
                    if ((isFutureDate(rightsDatas[x].LostRightsDate) && rightsDatas[x].SensitiveInfoPermitted != "N") || (!isFutureDate(rightsDatas[x].LostRightsDate) && rightsDatas[x].SensitiveInfoPermitted == "Y")) {
                        if (lostRightsDate != "" && lostRightsDate != "No Change Required") {
                            rightsDatas[x].LostRightsDate = lostRightsDate;
                            rightsDatas[x].LostRightsDateLinear = lostRightsDate;
                        }
                    }
                    if (rightsExpection != "0")
                        rightsDatas[x].ExceptionText = rightsExpection;
                    if (lostRightsReason != "No Change Required")
                        rightsDatas[x].LostReason = lostRightsReason;
                    if (physicalExploitation != "0")
                        rightsDatas[x].PhysicallyExploitedText = physicalExploitation;
                    if (digitalExploitation != "0")
                        rightsDatas[x].DigitallyExploitedText = digitalExploitation;
                    if (territoryRights != "")
                        rightsDatas[x].TerritorialRights = territoryRights;
                    if (rightsIncludedC != "")
                        rightsDatas[x].IncludedCountryLnr = rightsIncludedC;
                    if (rightsExcludedC != "")
                        rightsDatas[x].ExcludedCountryLnr = rightsExcludedC;
                    if (rightsIncludedT != "")
                        rightsDatas[x].IncludedTerritoryLnr = rightsIncludedT;
                    if (rightsExcludedT != "")
                        rightsDatas[x].ExcludedTerritoryLnr = rightsExcludedT;
                }
            
            if (!(rightsDatas[x].RightsEditPermitted == "N") || !(rightsDatas[x].SensitiveInfoPermitted == "N")) {
                if (gridObj._edit._updatedRecords.length > 0) {
                    for (var y = 0; y < gridObj._edit._updatedRecords.length; y++) {
                        if (gridObj._edit._updatedRecords[y].RightsId == tempId) {
                            gridObj._edit._updatedRecords[y] = rightsDatas[x];
                            replaceObj = true;
                        }
                    }
                }
                if (replaceObj == false) {
                    gridObj._edit._updatedRecords.push(rightsDatas[x]);
                }
            }
        }
    }
    gridObj._edit._data = rightsDatas;
}
/*Appends Data to Table*/
function appendData() {  //colObj commented
    var checkboxes;
    if (repertoireType == "release") {
        checkboxes = $("#" + gridName).find(".GridContent #releaseRightschkChild");
    }
    else {
        checkboxes = $("#" + gridName).find(".GridContent #chkChild");
    }

    for (var checkboxIds = 0; checkboxIds < checkboxes.length; checkboxIds++) {
        if (checkboxes[checkboxIds].checked) {
            var row = gridObj.get_ContentTable().getElementsByTagName("tr")[checkboxIds];
                if (repertoireType == "release") {
                    appendReleaseValuestoRow(row);
                }
                else {
                    appendValuestoRow(row);
                }
                appendValuestoTerritoryRow(row);
        }
    }
}

/*Appends Data to Each Cell*/

function appendValuestoTerritoryRow(tr) {
    //currentTerritoryRightsAcquiredValues();
    var appendTerritorytd = '';
    var isRightsEdit = '';
    if (repertoireType == 'release') {
        appendTerritorytd = $(tr).find('td.tdTerritoryRightsLnr');//tr.children[12];
        isRightsEdit = $(tr).find('td.tdRightsEditPermitted').text(); // tr.children[23].innerHTML;
    }
    else {
        appendTerritorytd = $(tr).find('td.tdTerritoryRightsLnr'); // tr.children[18];
        isRightsEdit = $(tr).find('td.tdRightsEditPermitted').text(); //tr.children[36].innerHTML;
        var isReviewEdit = $(tr).find('td.tdReviewStatusPermitted').text(); // tr.children[35].innerHTML;
        var appendtdReview = $(tr).find('td.tdReviewStatus'); // tr.children[4];
        if (reviewStatusType != "No Change Required") {
            if (isReviewEdit != "N") {
                $(appendtdReview).html('');
                $(appendtdReview).addClass("reviewFlagGreenUpdated");
                $(appendtdReview).attr('title', 'Final');
                $(appendtdReview).tooltip();
            }
        }
    }
    if (isRightsEdit != "N") {
        if (territoryRights != "") {
            if (territoryRights.length > 254) {
                $(appendTerritorytd).html(territoryRights.substring(0, 254) + '..');
                $(appendTerritorytd).attr('title', territoryRights.substring(0,254)+'..');
            }
            else {
                $(appendTerritorytd).html(territoryRights);
                $(appendTerritorytd).attr('title', territoryRights);
            }
            $(appendTerritorytd).removeClass("tdglobePlus");
            $(appendTerritorytd).addClass("updatedCell");
            $(appendTerritorytd).tooltip();
        }
    }
}

//
function appendValuestoRow(tr) {
   // currentRightsAcquiredValues();
    var appendtd = $(tr).find('td.tdActiveForMrktLnr');  //.children[20];
    var appendtd0 = $(tr).find('td.tdRightsPeriodLnr');  //.children[21];
    var appendtd1 = $(tr).find('td.tdLostRightsLnr');  //.children[22];
    var appendtd2 = $(tr).find('td.tdLostRightsDateLnr');  //.children[23];
    var appendtd3 = $(tr).find('td.tdLostRightsReasonLnr');  //.children[24];
    var appendtd4 = $(tr).find('td.tdRightsExceptionLnr');  //.children[25];
    var appendtd5 = $(tr).find('td.tdPhysicalExpLnr');  //.children[26];
    var appendtd6 = $(tr).find('td.tdDigitalExpLnr');  //.children[27];
    var appendtd7 = $(tr).find('td.tdDigitalUnbundledLnr');  //.children[28];
    var appendtd8 = $(tr).find('td.tdMobileExpLnr');  //.children[29];
    var appendtd9 = $(tr).find('td.tdPpbClaimLnr');  //.children[30];
    var appendtd10 = $(tr).find('td.tdReviewStatus');  //.children[4];
    var isReviewEdit = $(tr).find('td.tdReviewStatusPermitted').text();  //.children[35].innerHTML;
    var isRightsEdit = $(tr).find('td.tdRightsEditPermitted').text();  //.children[36].innerHTML;
    var isSensitiveData = $(tr).find('td.tdSensitiveInfoPermitted').text();  //.children[37].innerHTML;
    if (isRightsEdit != "N") {
        if (activeForMarketing != "0") {
            $(appendtd).html(activeForMarketing);
            $(appendtd).addClass("updatedCell");
        }
        if (rightsPeriod != "No Change Required") {
            $(appendtd0).html(rightsPeriod);
            $(appendtd0).addClass("updatedCell");
        }
        if (lostRights != "0") {
            $(appendtd1).html(lostRights);
            $(appendtd1).addClass("updatedCell");
        }
        if (!(lostRights=="N" && isSensitiveData == "N")) { // || (!isFutureDate(lostRightsDate) && isSensitiveData == "Y")) {
            if (lostRightsDate != "" && lostRightsDate != "No Change Required") {
                $(appendtd2).html(lostRightsDate);
                $(appendtd2).addClass("updatedCell");
            }
        }
        if (lostRightsReason != "No Change Required") {
            $(appendtd3).html(lostRightsReason);
            $(appendtd3).addClass("updatedCell");
        }
        if (rightsExpection != "0") {
            $(appendtd4).html(rightsExpection);
            $(appendtd4).addClass("updatedCell");
        }
        if (physicalExploitation != "0") {
            $(appendtd5).html(physicalExploitation);
            $(appendtd5).addClass("updatedCell");
        }
        if (digitalExploitation != "0") {
            $(appendtd6).html(digitalExploitation);
            $(appendtd6).addClass("updatedCell");
        }
        if (digitalUnbundled != "0") {
            $(appendtd7).html(digitalUnbundled);
            $(appendtd7).addClass("updatedCell");
        }
        if (mobileExploitation != "0") {
            $(appendtd8).html(mobileExploitation);
            $(appendtd8).addClass("updatedCell");
        }
        if (ppbRevenue != "0") {
            $(appendtd9).html(ppbRevenue);
            $(appendtd9).addClass("updatedCell");
        }
    }
    if (reviewStatusType != "No Change Required") {
        if (isReviewEdit != "N") {
            $(appendtd10).html('');
            $(appendtd10).addClass("reviewFlagGreenUpdated");
            $(appendtd10).attr('title', 'Final');
            $(appendtd10).tooltip();
        }
    }
}

// Element values appending to variables for updating in grid row
function currentRightsAcquiredValues() {
    activeForMarketing = $('#ddlactiveMarketing').val();
    rightsPeriod = $('#ddlrightsPeriod').find(":selected").text();
    lostRights = $('#ddllostRights').val();
    lostRightsDate = $('#lostRightsDate').val();
    rightsExpection = $('#ddlrightsException').val();
    lostRightsReason = $('#ddllostRightsReason').find(":selected").text();
    physicalExploitation = $('#ddlphysicalExploitation').val();
    digitalExploitation = $('#ddldigitalExploitation').val();
    digitalUnbundled = $('#ddldigitalUnbundling').val();
    mobileExploitation = $('#ddlmobileExploitation').val();
    ppbRevenue = $('#ddlppbClaim').val();
    reviewStatusType = $('#ddlreviewStatus').find(":selected").text();
    territoryRights = setNewStringFormation();
    rightsIncludedT = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded && item.IsTerritory; }).Select(function (item) { return item.Id.toString(); }).items);
    rightsExcludedT = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsExcluded && item.IsTerritory; }).Select(function (item) { return item.Id; }).items);
    rightsExcludedC = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsExcluded && !item.IsTerritory; }).Select(function (item) { return item.Id; }).items);
    rightsIncludedC = JSON.stringify(JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded && !item.IsTerritory; }).Select(function (item) { return item.Id; }).items);
}


// release
function appendReleaseValuestoRow(tr) {
    //currentRightsAcquiredValues();
    var rightsPeriodtd = $(tr).find('td.tdRightsPeriodLnr'); // tr.children[14];
    var lostRightstd = $(tr).find('td.tdLostRightsLnr'); // tr.children[15];
    var lostRightsDatetd = $(tr).find('td.tdLostRightsDateLnr'); //tr.children[16];
    var lostReasontd = $(tr).find('td.tdLostRightsReasonLnr'); //tr.children[17];
    var exceptiontd = $(tr).find('td.tdRightsExceptionLnr'); //tr.children[18];
    var physicallyExploitationtd = $(tr).find('td.tdPhysicalExpLnr'); //tr.children[19];
    var digitalExploitationtd = $(tr).find('td.tdDigitalExpLnr'); // tr.children[20];
    var isRightsEdit = $(tr).find('td.tdRightsEditPermitted').text(); // tr.children[23].innerHTML;
    var isSensitiveData = $(tr).find('td.tdSensitiveInfoPermitted').text(); // tr.children[24].innerHTML;
    if (isRightsEdit != "N") {
        if (rightsPeriod != "No Change Required") {
            $(rightsPeriodtd).html(rightsPeriod);
            $(rightsPeriodtd).addClass("updatedCell");
        }

        if (lostRights != "0") {
            $(lostRightstd).html(lostRights);
            $(lostRightstd).addClass("updatedCell");
        }
        if ((isFutureDate(lostRightsDate) && isSensitiveData != "N") || (!isFutureDate(lostRightsDate) && isSensitiveData == "Y")) {
            if (lostRightsDate != "" && lostRightsDate != "No Change Required") {
                $(lostRightsDatetd).html(lostRightsDate);
                $(lostRightsDatetd).addClass("updatedCell");
            }
        }
        if (lostRightsReason != "No Change Required") {
            $(lostReasontd).html(lostRightsReason);
            $(lostReasontd).addClass("updatedCell");
        }

        if (rightsExpection != "0") {
            $(exceptiontd).html(rightsExpection);
            $(exceptiontd).addClass("updatedCell");
        }
        if (physicalExploitation != "0") {
            $(physicallyExploitationtd).html(physicalExploitation);
            $(physicallyExploitationtd).addClass("updatedCell");
        }
        if (digitalExploitation != "0") {
            $(digitalExploitationtd).html(digitalExploitation);
            $(digitalExploitationtd).addClass("updatedCell");
        }
    }
}

