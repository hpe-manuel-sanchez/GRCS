/// <reference path="/GCS/Scripts/Custom/RightsReviewWorkqueue/RightsReviewWorkqueue.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/RightsReviewWorkqueue/PreClearanceTab.js" />
///<reference path="/GCS/Scripts/Custom/RepertoireRightsSearch/TerritorialCountries.js" />

var topPriceCompilation, midPriceCompilation, budgetCompilation, directMarketing, premium, masterSynchronization, territoryExclusion, excludedCountries, preClearanceRightsId, reviewStatusType, territoryExclusionIds ,territoryExclusionNameWithIds = "";;
var preClearancegridName = $('#hdnGridId').val();
var gridObjPreClearance = $find(preClearancegridName);
var noChangeRequired = "No Change Required";
$(document).ready(function () {
    excludedCountryIds = '';
    $("#" + preClearancegridName + "waiting_WaitingPopup").hide();
    setTimeout(function () {
        var TotDiaHgt = $(".divobjBulkEditPreClearancePopUp").height();
        var ReduceHgt = TotDiaHgt - 200;
        $(".cacTable").css('height', ReduceHgt + "px");
    }, 300);


    var waterMark = "No Change Required";
    $('#txtClearanceAdminComp').blur(function () {
        if ($(this).val().length == 0)
            $(this).val(waterMark).addClass('watermark');
    }).focus(function () {
        if ($(this).val() == waterMark)
            $(this).val('').removeClass('watermark');
    }).val(waterMark).addClass('watermark');

    $('.selectedListTable').append("<div id='hiddenWaterMark' class='watermark'>No Change Required</div>");

});
//button Apply Changes
$("#btnPreClearanceApply").click(function () {
    if (checkStatusBulkConfirmation == "true") {
        updatePreclearanceBulk();
    }
    else {
        $("#divConfirmDialog").dialog('open');
        $("#divConfirmDialog").dialog({ title: 'Bulk-Edit Confirmation', buttons: {
            'Yes':
        function () {
            if ($("#chkFutureMsg").is(":checked")) {
                checkStatusBulkConfirmation = "true";
            }
            $("#divConfirmDialog").dialog('close');
            updatePreclearanceBulk();
        },
            'Cancel':
         function () {
             $("#divConfirmDialog").dialog('close');
         }
        }
        });
    }
});

//button re-set rights acquired
$('#btnResetPreClearance').click(function () {
    $('#ddltopPriceCompilation').val(0);
    $('#ddlmidPriceCompilation').val(0);
    $('#ddlbudgetCompilation').val(0);
    $('#ddldirectMarketing').val(0);
    $('#ddlPremium').val(0);
    $('#ddlMasterOrSynchronizationUse').val(0);
    $('#ddlreviewStatus').val(0);
    territoryExclusion = "";
    territoryExclusionIds = "";
    $('#txtClearanceAdminComp').val('');
    $('#SelectedList').find('tr').remove();
    $('#txtClearanceAdminComp').addClass('ui-autocomplete-input');
    $("#imgClearIcon").hide();
    $('#ClearenceList').find('input:checkbox').attr('checked', false);
    $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);
    if ($(".divobjBulkEditPreClearancePopUp").is(':visible')) {
        window.parent.checkSelectedEmpty();
    }
    $('#txtClearanceAdminComp').mousedown();
    cacAutoSearch();
});
//button cancel
$('#btnPreClearanceCancel').click(function () {
    $('.divobjBulkEditPreClearancePopUp').dialog('close');
});

function updatePreclearanceBulk() {
    var preClearancegridDatas = gridObjPreClearance.get_SelectedRecords();
     appendPreClearanceData();
     for(var preclearanceIds=0 ; preclearanceIds < preClearancegridDatas.length ; preclearanceIds++) {
        preClearanceRightsId = preClearancegridDatas[preclearanceIds].RightsIdLnr;
        preClearanceGridItems(preClearanceRightsId);
       }
    if (topPriceCompilation != noChangeRequired || midPriceCompilation != noChangeRequired || budgetCompilation != noChangeRequired || directMarketing != noChangeRequired || premium != noChangeRequired || masterSynchronization != noChangeRequired ) {
        gridObjPreClearance._edit._isEdit = true;
        $('.divobjBulkEditPreClearancePopUp').dialog('close');
    }
    else {
        $('.divobjBulkEditPreClearancePopUp').dialog('close');
    }
}

/*Appends Data to Table*/
function appendPreClearanceData() {
    var checkboxes = $("#"+preClearancegridName).find(".GridContent #prechkChild");
    for (var checkboxIds = 0; checkboxIds < checkboxes.length; checkboxIds++) {
        if (checkboxes[checkboxIds].checked) {
            var row = gridObjPreClearance.get_ContentTable().getElementsByTagName("tr")[checkboxIds];
            appendpreClearanceValuestoRow(row);
        }
     }
}


/*Appends Data to Each Cell*/
function appendpreClearanceValuestoRow(tr) {

    currentPreClearanceValues();
    var appendtd = $(tr).find('td.tdReviewStatusLnr'); //.children[3];
    var appendtd0 = $(tr).find('td.tdTopPriceCompilationLnr'); //.children[19];
    var appendtd1 = $(tr).find('td.tdMidPriceCompilationLnr');//.children[20];
    var appendtd2 = $(tr).find('td.tdBudgetCompilationLnr'); //.children[21];
    var appendtd3 = $(tr).find('td.tdDirectMarketingLnr');  //.children[22];
    var appendtd4 = $(tr).find('td.tdPremiumLnr'); //.children[23];
    var appendtd5 = $(tr).find('td.tdMasterSyncroniseUseLnr');  //.children[24];
    var appendtd6 = $(tr).find('td.tdTerritoryExclusionsLnr');  //.children[25];
    var appendtd7 = $(tr).find('td.tdExcludedCountries'); //.children[26];
    var isReviewEdit = $(tr).find('td.tdReviewStatusPermitted').text();   //.children[30].innerHTML;
    var isRightsEdit = $(tr).find('td.tdRightsEditPermitted').text();  //.children[31].innerHTML;
    var includedArray = "";
    var excludedString = $(tr).find('td.tdTerritoryExclusionsLnr').text(); 
    includedArray = $(tr).find('td.tdIncludedCountries').text();  //.children[27]).html();
    var territoryExclusionIdNew = getExcludedCountries(territoryExclusionIds.split(','), includedArray.split(','));
    var excludedStringNew = getExcludedCountryString(territoryExclusionIdNew);
    if (excludedStringNew != "") {
        excludedString = "";
        if (excludedString == "") {
            excludedString = excludedStringNew;
        }
        else {
            excludedString = excludedString + "," + excludedStringNew;
        }
    }
    if (isReviewEdit != "N") {
        if (reviewStatusType != "No Change Required") {
            $(appendtd).html('');
            $(appendtd).addClass("reviewFlagGreenUpdated");
            $(appendtd).attr('title', 'Final');
            $(appendtd).tooltip();
            gridObjPreClearance._edit._isEdit = true;
        }
    }
    if (isRightsEdit != "N") {
        if (topPriceCompilation != "No Change Required") {
            $(appendtd0).html(topPriceCompilation);
            $(appendtd0).addClass("updatedCell");
        }

        if (midPriceCompilation != "No Change Required") {
            $(appendtd1).html(midPriceCompilation);
            $(appendtd1).addClass("updatedCell");
        }
        if (budgetCompilation != "No Change Required") {
            $(appendtd2).html(budgetCompilation);
            $(appendtd2).addClass("updatedCell");
        }
        if (directMarketing != "No Change Required") {
            $(appendtd3).html(directMarketing);
            $(appendtd3).addClass("updatedCell");
        }
        if (premium != "No Change Required") {
            $(appendtd4).html(premium);
            $(appendtd4).addClass("updatedCell");
        }
        if (masterSynchronization != "No Change Required") {
            $(appendtd5).html(masterSynchronization);
            $(appendtd5).addClass("updatedCell");
        }
        if (excludedString != "" && excludedString != undefined) {
            $(appendtd6).html(excludedString);
            $(appendtd6).addClass("updatedCell");
            $(appendtd6).attr('title', excludedString);
            $(appendtd6).tooltip();
        }
        if (territoryExclusionIdNew != "") {
            gridObjPreClearance._edit._isEdit = true;
            $(appendtd7).html(territoryExclusionIdNew);
            $(appendtd7).addClass("updatedCell");
        }
    }
}



// Element values appending to variables for updating in grid row
function currentPreClearanceValues() {
    topPriceCompilation = $('#ddltopPriceCompilation').find(":selected").text();
    midPriceCompilation = $('#ddlmidPriceCompilation').find(":selected").text();
    budgetCompilation = $('#ddlbudgetCompilation').find(":selected").text();
    directMarketing = $('#ddldirectMarketing').find(":selected").text();
    premium = $('#ddlPremium').find(":selected").text();
    masterSynchronization = $('#ddlMasterOrSynchronizationUse').find(":selected").text();
    reviewStatusType = $('#ddlreviewStatus').find(":selected").text();
    territoryExclusion = getClearanceValues();
    territoryExclusionIds = getClearanceIds();
    territoryExclusionNameWithIds = getClearanceIdsWithName();
}

/*Appends Data to Grid*/
function preClearanceGridItems(tempId) {
    var preClearanceDatas = gridObjPreClearance._edit._data;
    var replaceObj = false;
    for (var x = 0; x < preClearanceDatas.length; x++) {
        if (preClearanceDatas[x].RightsIdLnr == tempId) {
            if (preClearanceDatas[x].ReviewStatusPermitted != "N") {
                if (reviewStatusType != "No Change Required")
                    preClearanceDatas[x].ReviewStatusLnr = reviewStatusType;
            }
            if (preClearanceDatas[x].RightsEditPermitted != "N") {
                if (topPriceCompilation != "No Change Required")
                    preClearanceDatas[x].TopPriceCompilationLnr = topPriceCompilation;
                if (midPriceCompilation != "No Change Required")
                    preClearanceDatas[x].MidPriceCompilationLnr = midPriceCompilation;
                if (budgetCompilation != "No Change Required")
                    preClearanceDatas[x].BudgetCompilationLnr = budgetCompilation;
                if (directMarketing != "No Change Required")
                    preClearanceDatas[x].DirectMarketingLnr = directMarketing;
                if (premium != "No Change Required")
                    preClearanceDatas[x].PremiumLnr = premium;
                if (masterSynchronization != "No Change Required")
                    preClearanceDatas[x].MasterSyncroniseUseLnr = masterSynchronization;
                if (territoryExclusion != "")
                    preClearanceDatas[x].TerritoryExclusionsLnr = territoryExclusion;
                if (territoryExclusionIds != "") {
                    var exclusionArrays = territoryExclusionIds.split(',');
                    var includedArrays = preClearanceDatas[x].IncludedCountries != null ? preClearanceDatas[x].IncludedCountries.split(',') : "";
                    var excludedIds = preClearanceDatas[x].ExcludedCountries != null ? preClearanceDatas[x].ExcludedCountries : "";
                    var territoryExclusionIdNew = getExcludedCountries(exclusionArrays, includedArrays);
                    var appendedExcludedIds = getUniqueExcludedIds(territoryExclusionIdNew,excludedIds);
                    if (territoryExclusionIdNew != "")
                    {
                        preClearanceDatas[x].ExcludedCountries = territoryExclusionIdNew;
                        gridObjPreClearance._edit._isEdit = true;
                    }
                    
                }
            }
            if (!(preClearanceDatas[x].RightsEditPermitted == "N") || !(preClearanceDatas[x].ReviewStatusPermitted == "N")) {
                if (gridObjPreClearance._edit._updatedRecords.length > 0) {
                    for (var y = 0; y < gridObjPreClearance._edit._updatedRecords.length; y++) {
                        if (gridObjPreClearance._edit._updatedRecords[y].RightsIdLnr == tempId) {
                            gridObjPreClearance._edit._updatedRecords[y] = preClearanceDatas[x];
                            replaceObj = true;
                        }
                    }
                }
                if (replaceObj == false)
                    gridObjPreClearance._edit._updatedRecords.push(preClearanceDatas[x]);
            }
        }
    }
    gridObjPreClearance._edit._data = preClearanceDatas;
}

function checkSelectedNotEmpty() {
   $('#hiddenWaterMark').hide();
//    var waterMark = "No Change Required";
//    $('#txtClearanceAdminComp').focus(function () {
//        if ($(this).val() == waterMark && $(this).val().length==0)
//            $(this).val('').removeClass('watermark');
//    }).val(waterMark).addClass('watermark');

}
function checkSelectedEmpty() {
    $('#hiddenWaterMark').show();
    var waterMark = "No Change Required";
    $('#txtClearanceAdminComp').blur(function () {
        if ($(this).val().length == 0)
            $(this).val(waterMark).addClass('watermark');
    }).focus(function () {
        if ($(this).val() == waterMark)
            $(this).val('').removeClass('watermark');
    }).val(waterMark).addClass('watermark');
}


function getExcludedCountries(exclusionArrays, includedArrays) {
    var territoryExclusionIdNew = "";
    for (var ids = 0; ids < exclusionArrays.length; ids++) {
        for (var includedIds = 0; includedIds < includedArrays.length; includedIds++) {
            if (parseInt(includedArrays[includedIds]) == parseInt(exclusionArrays[ids])) {
                if (territoryExclusionIdNew == "") {
                    territoryExclusionIdNew = parseInt(exclusionArrays[ids]);
                } else {
                    territoryExclusionIdNew = territoryExclusionIdNew + "," + parseInt(exclusionArrays[ids]);
                }
            }
        }
    }
    return territoryExclusionIdNew;
}

function getExcludedCountryString(ids) {
     var excludedArray = ids.split(',');
     var nameIds = territoryExclusionNameWithIds.split(',');
     var excludedString = "";
     for (var index = 0; index < nameIds.length; index++) {
         for (var innerIndex = 0; innerIndex < excludedArray.length; innerIndex++) {
             if (parseInt(nameIds[index].split(':')[0]) == parseInt(excludedArray[innerIndex])) {
                 if(excludedString == "") {
                     excludedString = nameIds[index].split(':')[1];
                 } else {
                     excludedString = excludedString + ',' + nameIds[index].split(':')[1];
                 }
             }
         }
     }
     return excludedString;
 }

 function getUniqueExcludedIds(excludedIdsNew, excludedIdsOld) {
     if (excludedIdsOld == "")
         return excludedIdsNew;
     else {
         var excludedIdsOldArray = excludedIdsOld.split(',');
         var idArray = excludedIdsNew.split(',');
         for (var index = 0; index < excludedIdsOldArray.length; index++) {
             var isUnique = true, uniqueValue = "";
             for (var innerIndex = 0; innerIndex < idArray.length; innerIndex++) {
                 uniqueValue = idArray[innerIndex];
                 if (parseInt(idArray[innerIndex]) == parseInt(excludedIdsOldArray[index])) {
                     isUnique = false;
                 }

                 if (isUnique && uniqueValue != "")
                     if (excludedIdsOld) {
                         excludedIdsOld = uniqueValue;
                     }
                     else {
                         excludedIdsOld = excludedIdsOld + "," + uniqueValue;
                     }
             }
         }
         return excludedIdsOld;
     }
}