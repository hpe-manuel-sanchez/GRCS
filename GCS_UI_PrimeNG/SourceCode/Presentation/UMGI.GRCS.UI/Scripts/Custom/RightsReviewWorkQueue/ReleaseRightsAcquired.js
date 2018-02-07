var releaseBulkEditBtnId = '';
var rightsIncludedC, rightsIncludedT, rightsExcludedC, rightsExcludedT;
var lostRightsDateErrorMessage = "";
var isMac = 0;
var isSingleTRmodified = false;
//var grid_Id = $('#hdnGridId').val();
$(document).ready(function () {
    /*To show loader on popup open*/
//    $('#loadingDiv').hide().ajaxStart(function () {//ui-autocomplete-input ui-autocomplete-loading
//        if (!$("[id*='waiting_WaitingPopup']").is(':visible') && !$(".ui-jtable-loading").is(':visible') && !$(".ui-autocomplete-loading").is(':visible'))
//            $(this).show();
//    }).ajaxStop(function () { $(this).hide(); });
});

/*SyncFusion Grid Code*/
function onReleaseRightsActionBegin(sender, args) {
    $("[id*='waiting_WaitingPopup']").hide();
    //$('#loadingDiv').hide();
    HideWarningSuccess();
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
        if(searchParams == "")
                args.data["isDynamic"] = false;
    }
    else {
       // args.data["rightsSetId"] = "test";
        args.data["isDynamic"] = false;
    }
    $('#hdnGridId').val(sender._ID);
}

function checkImageForReleaeRightsAcquired(events, args) {
    
    var image = '';
    var title = '';
    if (args.Column.Name == "UPCId") {
        args.Data.UPCId == null ? args.Data.UPCId = "" : args.Data.UPCId = args.Data.UPCId;
    }
    if (args.Column.Name == "RightsEditPermitted") {
        if (args.Data.RightsEditPermitted == "N") {
            disableEditing(args.Element);
        }
    }

    if (args.Column.Name == "TerritorialRights" || args.Column.Name == "LostRightsDate" || args.Column.Name == "RightsPeriod" || args.Column.Name == "LostRightsText" || args.Column.Name == "LostReason" || args.Column.Name == "ExceptionText" || args.Column.Name == "PhysicallyExploitedText" || args.Column.Name == "DigitallyExploitedText") {
        if (args.Data.RightsEditPermitted == "N") {
            disableEditing(args.Element);
        }
    }

    if (args.Column.Name == "LostRightsDate") {
        if (args.Data.LostRightsText != null && args.Data.SensitiveInfoPermitted == "N" && args.Data.LostRightsText == "N") {
            disableEditing(args.Element);
            $(args.Element).html("");
        }
    } 

    if (args.Column.Name == "LostReason") {
        if (args.Data.SensitiveInfoPermitted == "N" && args.Data.LostRightsText == "N") {
            $(args.Element).html("");
            disableEditing(args.Element);
        }
    }

    if (args.Column.Name == "RightsExpiryRule") {
        if (args.Data.SensitiveInfoPermitted == "N" && args.Data.LostRightsText == "N") {
            args.Data.RightsExpiryRule = "";
        }
    } 
    

    if (args.Column.Name == "ReleaseType") {
        if (args.Data.ReleaseType == 'PR') { // done
            image = '<img  src="/GCS/Images/Physical_Release.gif">';
            title = "Physical Release";
        } else if (args.Data.ReleaseType == 'DR') { // done
            image = '<img  src="/GCS/Images/Digital_Release.gif">';
            title = "Digital Release";
        } else if (args.Data.ReleaseType == 'PL') {
            image = '<img  src="/GCS/Images/Physical_Link.png">';
            title = "Physical Link";
        } else if (args.Data.ReleaseType == 'DL') {
            image = '<img  src="/GCS/Images/digital_link_create.gif">';
            title = "Digital Link";
        } else if (args.Data.ReleaseType == 'DP') { // done
            image = '<img  src="/GCS/Images/package_digital.gif">';
            title = "Digital Package";
        } else if (args.Data.ReleaseType == 'PP') { // done
            image = '<img  src="/GCS/Images/package_physical.gif">';
            title = "Physical Package";
        } else if (args.Data.ReleaseType == 'PDL') {
            image = '<img  src="/GCS/Images/package_digital_link.gif">';
            title = "Digital Package (Linked)";
        } else if (args.Data.ReleaseType == 'PPL') {
            image = '<img  src="/GCS/Images/package_physical_link.gif">';
            title = "Physical Package (Linked)";
        }
        $(args.Element)[0].innerHTML = image;
        if (title != '') {
            $(args.Element).attr("title", '~' + title);
            $(args.Element).tooltip({ showBody: "~" });
        }
        return false;
    }

    if (args.Column.Name == "RightsSourceId") {
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
        $(args.Element)[0].innerHTML = image;
        if (title != '') {
            $(args.Element).attr("title", '~' + title);
            $(args.Element).tooltip({ showBody: "~" });
        }
        return false;
    }
    
    if (args.Column.Name == "TerritorialRights") {
        if (args.Data.RightsEditPermitted == "Y") {
            if (args.Data.TerritorialRights == "null" || args.Data.TerritorialRights == "" || args.Data.TerritorialRights == " ") {
                $($(args.Element)[0]).addClass("tdglobePlus");
            }
        }
    }
    if (args.Column.Name == "IsSplitDeal") {
        var htmlText = '';
        var image = '';
        var isMediaPortalData = 0;        
        if (args.Data.IsEditableRight == '0') {
            isMediaPortalData = 1;
        }
        if (args.Data.ContractId == null || isMac == "1" && isMediaPortalData == 0) {
            image = '<div class=\'rightsMAC\'></div>';           
            htmlText = "Multi Artist Compilation";
        }
        else if (args.Data.IsSplitDeal == "0") {
            image = '<div class=\'rightslinkedContract\'></div>';
            if (args.Data.LinkedTooltip != null && args.Data.LinkedTooltip != '') {
                htmlText = args.Data.LinkedTooltip;
            }
        }
        else if (args.Data.IsSplitDeal == "1") {
            image = '<div class=\'rightsSplit\'></div>';
            if (args.Data.LinkedTooltip != null && args.Data.LinkedTooltip != '') {
                htmlText = args.Data.LinkedTooltip;
            }
        }
    
        $(args.Element)[0].innerHTML = image;
        if (htmlText != '') {
            $(args.Element).attr("title", '~' + htmlText);
            $(args.Element).tooltip({ showBody: "~" });
        }
        
    }

    if (args.Column.Name == "ExceptionText") {
        var htmlRightsExpText = null;
        if (args.Data.ExceptionText == 'Applied') {
            if (args.Data.Notes != '') {
                htmlRightsExpText = "<b>Rights Exception Applied: </b>~" + args.Data.Notes;
            }
            else {
                htmlRightsExpText = "<b>Rights Exception Applied</b>";
            }
        }
        else if (args.Data.ExceptionText == 'Not Applied') {
            if (args.Data.Notes != '') {
                htmlRightsExpText = "<b>Rights Exception Not Applied: </b>~" + args.Data.Notes;
            }
            else {
                htmlRightsExpText = "<b>Rights Exception Not Applied</b>";
            }
        }
        else {
            disableEditing(args.Element);
        }
        if (htmlRightsExpText != null) {
            $(args.Element).attr("title", '~' + htmlRightsExpText);
            $(args.Element).tooltip({ showBody: "~" });
        }

    }

    if (args.Column.Name == "Error") {
        var htmlError = null;
        if (args.Data.Error != null) {
            image = '<div class=\'errorImageRights\'></div>';
            htmlError = args.Data.Error;
        }
        $(args.Element)[0].innerHTML = image;
        if (htmlError != null) {
            $(args.Element).attr("title", '~' + htmlError);
            $(args.Element).tooltip({ showBody: "~" });
        }
    }

    if (args.Column.Name == "RightsPeriod") {
        var htmlRightsRuleText = null;
        if (args.Data.RightsExpiryRule != '') {
            image = '<span class=\'resourceInfoRights\'></span>';
            htmlRightsRuleText = "<b>Rights Expiry Rule: </b>" + args.Data.RightsExpiryRule;
        }
        $(args.Element)[0].innerHTML += image;
        if (htmlRightsRuleText != null) {
            $(".resourceInfoRights").attr("title", '~' + htmlRightsRuleText);
            $(".resourceInfoRights").tooltip({ showBody: "~" });
        }
    }

    if (args.Column.Name == "UPCId") {
        if (args.Data.LostRightsText == 'Y') {
            $(args.Element)[0].style.backgroundColor = "#FFBFBF";  // Light Color When Error Show append "FFE9E8";
            $(args.Element)[0].style.fontWeight = "bold";
        }
    }
}

function onReleaseRightsLoad(sender, args) {
    var grid_Id = sender._ID;
    //check box add
    var chk = $("#" + grid_Id + " .GridHeader").find("th.HeaderCell .HeaderCellDiv")[0];
    $(chk).append("<input type=\"Checkbox\" id=\"releaseRightschkAll\" onclick=\"ReleaseRightsCheckBoxAllClick(event)\"> All");
    //grid tool bar hide
    $("#" + grid_Id + "_toolbar").hide();
    var totCount = sender.get_TotalRecordsCount();
    $('#warning').hide();
    $("#" + grid_Id + " .MsgBar").empty();
    $("#" + grid_Id + " .MsgBar").text("Search Results (" + totCount + ")");
    $("#" + grid_Id + " .manualPagerLabel:eq(1)").empty();
    $("#" + grid_Id + " .manualPagerLabel:eq(1)").text("Show item per page");
}

function onReleaseRightsActionSuccess(sender, args) {
    syncGridPagerSearchWQ(sender._ID);
    var grid_Id = sender._ID;
    var totCount = sender.get_TotalRecordsCount();
    $("#" + grid_Id + " .MsgBar").empty();
    $("#" + grid_Id + " .MsgBar").text("Search Results (" + totCount + ")");
    
    if (args.RequestType == "Save") {
        ShowSuccess(window.ReleaseCustomsSearchConstants.ReleaseRightsSuccessMessages);
      //  displayDialog("Success", "&nbsp;&nbsp;&nbsp;" + window.ReleaseCustomsSearchConstants.ReleaseRightsSuccessMessages);
        SyncfusionGridScroll(grid_Id);
        customMenu();
    }
    if (args.RequestType == "sorting") {
        if (sortType == "DESC") {
            if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Ascending'></span>";
            }
        }
        else if (sortType == "ASC") {
            if (sortColumn == "UPCId") {
                sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a release WQ for that UPC.'></div><span class='Descending'></span>";
            }
        }
        else {
            sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a release WQ for that UPC.'></div>";
        }
    }
    else {
        sender.get_HeaderTable().childNodes[1].childNodes[0].childNodes[4].childNodes[0].innerHTML = "UPC<div class='resourceInfoHeader' title='Click on UPC to create a WQ for resources/tracks on that release.'></div>";
    }

    //var totCount = sender.get_TotalRecordsCount();
    $('#warning').hide();
    //var grid_Id = sender._ID;
    SyncfusionGridScroll(grid_Id);
    setSyncGridToolTip("#" + grid_Id + "_Table");
    $("#jqgrid .manualPagerLabel:eq(1)").empty();
    $("#jqgrid .manualPagerLabel:eq(1)").text("Show item per page");
    // add Bulk Edit button
    releaseBulkEditBtnId = "bulkEditReleaseRightsAcquired" + grid_Id.replace(/[^0-9]/g, '');
    $("#" + grid_Id).find(".MsgBar").html("<div class='alignLeft' style='margin: 3px 15px 0 -2px;'>Search Results (" + totCount + ")</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input type='button' id=" + releaseBulkEditBtnId + " style='float: left;' value='Bulk Edit' class='primbutton'><div class='primbutton_right'></div></div>");
    callBulkEditReleaseButton();
    releaseCheckList();
    $(".resourceInfoHeader").tooltip();
    ScrollBarMovement(sender._ID);
}

function SyncfusionGridScroll(grid_Id) {
    var pagerHgt = $("#" + grid_Id + " .GridPager").height();
    var headerHgt = $("#" + grid_Id + " .GridHeader").height();
    var browsHgt = 0;
    if ($.browser.msie)
        browsHgt = 50;
    else
        browsHgt = 45;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;
    var sTable = "#" + grid_Id;
    var jtableTop = $("" + sTable + "").offset().top;
    var topfootPos = $(".footer").offset().top;
    var totRecHeight = $("#" + grid_Id + "_Table").height() + reduceHgt;
    var tableHeight = topfootPos - jtableTop;
    var gridObj = $find(grid_Id);
    if (totRecHeight >= tableHeight)
        setSyncfusionGridHeight(gridObj, tableHeight - reduceHgt);
    else
        setSyncfusionGridHeight(gridObj, totRecHeight - reduceHgt + 20);
}
function setSyncfusionGridHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}

//on Save button click while occuring grid error
function onReleaseRightsGridSaveError(sender, args) {
    if (args.XMLHttpRequest.responseText != null) {
        var errorVal = args.XMLHttpRequest.responseText.split("<title>")[1].split("</title>")[0];
        //ShowWarning(errorVal);
        displayDialog("Error", "&nbsp;&nbsp;&nbsp;Error Retreiving WorkQueue Data");
    }
    SyncfusionGridScroll(sender._ID);
    $("#" + sender._ID).find(".MsgBar").html("<div class='alignLeft' style='margin: 3px 15px 0 -2px;'>Search Results (0)</div><div class='bulkEditBtnContainer'><div class='primbutton_left'></div><input id=" + releaseBulkEditBtnId + " type='button' style='float: left;' value='Bulk Edit' class='primbutton'><div class='primbutton_right'></div></div>");
    //rightsAcquiredGridScroll();
    callBulkEditReleaseButton();
    $('#loaderPanel').hide();
    $('#loadingDiv').hide();
}

function onCellEditReleaseCustom(sender, args) {
    if ($(".divobjBulkEditRightsAcquired").is(':visible')) {
        args.cancel = true;
    }
    if ($(".divobjBulkEditRightsAcquired").is(':visible')) {
        args.cancel = true;
        $('.divobjBulkEditRightsAcquired').mousedown(function (e) {
            e.stopImmediatePropagation();
        });
        $('.divobjBulkEditRightsAcquired').keypress(function (e) {
            e.stopImmediatePropagation();
        });
    }
    else if (args.colObj.Name == "TerritorialRights") {
        $("#" + sender._ID + "EditForm").find("#TerritorialRights").attr('disabled', 'disabled');
        var rightsId = sender._currentDataItem.RightSetId;
        var territoryNames = sender._currentDataItem.TerritorialRights;
        var objbulkEditReleaseRightsAcquiredTR = $('<div class="divobjBulkEditRightsAcquired"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Bulk Edit - Release Rights Acquired',
            show: 'clip',
            hide: 'clip',
            width: 900,
            height: 550,
            resizable: false,
            close: function () {
                $(this).remove();
                $("#" + $('#hdnGridId').val()).mousedown();
            }
        });
        objbulkEditReleaseRightsAcquiredTR.load('/GCS/RightsReviewWorkQueue/RightsAcquiredBulkEdit', "",
                        function (responseText, textStatus) {
                            if (textStatus == 'error') {
                                objbulkEditReleaseRightsAcquiredTR.html('<p>' + messageCommon.error + '</p>');
                            }
                        });
        objbulkEditReleaseRightsAcquiredTR.dialog('open');
        trEditType = "Single";
        trRightsId = rightsId;
        isSingleTRmodified = false;
    }
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
}

/*Validate Data On Release Rights Acquired Grid Save*/
function validateReleaseRightsAcquiredSave(grid_Id) {
    var isSaveError = false;
    
    $("#" + grid_Id + "_Table").find('tr').each(function () {
        var lostRights, lostRightsReason, lostRightsDate, isError = false;
        var tr = this;
        $(this).find('td').each(function () {
            var isSaveErrorNew = false;
            if ($(this).index() == 15) {
                lostRights = $(this).html();
            }
            if ($(this).index() == 16) {
                lostRightsDate = $(this).html();
            }
            if ($(this).index() == 17) {
                lostRightsReason = $(this).html();
            }
            if ($(this).hasClass('hasError')) {
                isSaveError = true;
                isSaveErrorNew = true;
            }
            if (!isSaveErrorNew) {
                if ($(this).index() == 13) {
                    $(this).html("");
                    $(tr)[0].style.backgroundColor = "";
                    $(tr)[0].style.border = "";
                }
            }
        });
        if (lostRightsDate != "") {
            if (lostRightsReason == "") {
                var rightsLostReasonErrorMessage = "Please select a Lost Rights Reason";
                $(this).find('td').each(function () {
                    if ($(this).index() == 13) {
                        $(this).html("<span class='alertImage " + "imgRightsErrorMessage" + $(tr).index() + "' title='" + rightsLostReasonErrorMessage + "' ></span>");
                        $('.' + "imgRightsErrorMessage" + $(tr).index()).tooltip({ showURL: false });
                        $(tr)[0].style.backgroundColor = "#FFE9E8";
                        $(tr)[0].style.border = "2px solid red";
                        isError = true;
                        return isError;
                    }
                });
            }
        }
        if (isError) {
            isSaveError = true;
            // return false;
        }
    });
        return isSaveError;
}

//Validation on save of rights acquired Grid Cell-Save.
function validateReleaseRightsOnEdit(sender, args) {
    HideWarningSuccess();
    var lostRights, lostRightsDate;
    lostRights = sender._currentDataItem.LostRightsText;
    lostRightsDate = sender._currentDataItem.LostRightsDate;
    if (lostRightsDate != null) {
        if (lostRightsDate.toString().indexOf('UTC') != -1) {
            if (lostRightsDate.getDate().length == 1)
                lostRightsDate = '0' + lostRightsDate.getDate() + ' ' + lostRightsDate.toDateString().substr(4, 3) + ' ' + lostRightsDate.getFullYear();
            else
                lostRightsDate = lostRightsDate.getDate() + ' ' + lostRightsDate.toDateString().substr(4, 3) + ' ' + lostRightsDate.getFullYear();
        }
    }
    if (args.value != "" && args.colObj.Name == "LostRightsDate") {
        if (validateLostRightsDate(args.value, lostRights, $(sender._editTD).parent())) {//args.value is Date
            $(sender._editTD).parent().find('td').each(function () {
                if ($(this).hasClass('releaseRightsTabError')) {
                    $(this).removeClass('hasError');
                    $(this).parent()[0].style.backgroundColor = "";
                    $(this).parent()[0].style.border = "";
                    $(this).html("");
                }
            });
            args.value = args.value;
            sender._currentDataItem.LostRightsDateLinear = args.value;
        } else {
            sender._currentDataItem.LostRightsDate = "";
            sender._currentDataItem.LostRightsDateLinear = "";
            $($(sender._editTD).parent().find('td')[16]).html("");
            $($(sender._editTD).parent().find('td')[16]).addClass('updatedCell');
            args.value = "";
        }

    }

    if (args.value != "" && args.colObj.Name == "LostRightsText") {
        if (!$(sender._editTD.nextSibling).hasClass('disabledElement')) {
            if (validateLostRightsDate(lostRightsDate, args.value, $(sender._editTD).parent())) {//args.value is Date
                args.value = args.value;
            }
            else {
                if (!$(sender._editTD.nextSibling).hasClass('disabledElement')) {
                    sender._currentDataItem.LostRightsDate = "";
                    sender._currentDataItem.LostRightsDateLinear = "";
                    $($(sender._editTD).parent().find('td')[16]).html("");
                    $($(sender._editTD).parent().find('td')[16]).addClass('updatedCell');
                   // args.value = args.oldValue;
                }
            } 
        }

    }
    if ($(".divobjBulkEditRightsAcquired").is(':visible')) {
        args.cancel = true;
        args.preventPropagationClick = true;
    }
    else {
        if (args.colObj.Name == "TerritorialRights") {
            if (isSingleTRmodified == true) {
                if (rightsIncludedC != null && rightsIncludedC != undefined)
                    sender._currentDataItem.IncludedCountryLnr = rightsIncludedC;
                if (rightsIncludedT != null && rightsIncludedT != undefined)
                    sender._currentDataItem.IncludedTerritoryLnr = rightsIncludedT;
                if (rightsExcludedC != null && rightsExcludedC != undefined)
                    sender._currentDataItem.ExcludedCountryLnr = rightsExcludedC;
                if (rightsExcludedT != null && rightsExcludedT != undefined)
                    sender._currentDataItem.ExcludedTerritoryLnr = rightsExcludedT;
            }
            if (args.value == undefined || args.value == "") {
                args.value = args.oldValue;
            }
            //        if (args.value == args.oldValue)
            //            args.value = args.value + "  "; 

            rightsIncludedC = "";
            rightsIncludedT = "";
            rightsExcludedC = "";
            rightsExcludedT = "";
            $("#" + sender._ID + "EditForm").find("#TerritorialRights").attr('disabled', 'disabled');
        }
    }
   
    //rightsAcquiredGridScroll();
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
            if ($(tdElements[indexPasts]).hasClass('releaseRightsTabError')) {
                $(tdElements[indexPasts]).addClass('hasError');
                $(tdElements[indexPasts]).html("<span class='alertImage imagRightsDateErrorMessage' title='" + lostRightsDateErrorMessage + "' ></span>");
                $('.' + "imagRightsDateErrorMessage").tooltip({ showURL: false });
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
            if ($(tdElements[indexFuture]).hasClass('releaseRightsTabError')) {
                $(tdElements[indexPasts]).addClass('hasError');
                $(tdElements[indexFuture]).html("<span class='alertImage imagRightsDateErrorMessage' title='" + lostRightsDateErrorMessage + "' ></span>");
                $('.' + "imagRightsDateErrorMessage").tooltip({ showURL: false });
                $(tdElements[indexFuture]).parent()[0].style.backgroundColor = "#FFE9E8";
                $(tdElements[indexFuture]).parent()[0].style.border = "2px solid red";
                return false;
            }
        }
        return false;
    }
    $(tdElements).each(function () {
        $(this).removeClass('hasError');
    });
    return true;
}

function releaseCheckList() {//releaseRightsAcquireGrid1
    var grid_Id = $('#hdnGridId').val();
    if (grid_Id == "")
        grid_Id = "releaseRightsAcquireGrid1";
    setTimeout(function () {
        var gridObjRights = $find(grid_Id);
        gridObjRights.set_AllowSelection(true);
        gridObjRights.set_AllowSelection(false);
    }, 0);
    $("#" + grid_Id + "_Table").unbind("click");
    $("#" + grid_Id + "_Table").bind("click", function (event) {
        event.stopImmediatePropagation();
        HideWarningSuccess();
        var selectedTr = event.target.parentElement;
        var className = event.target.className;
        if (className == "releaseRightschkChildClass")
            event.target = event.target.parentNode;
        if (event.target.tagName == "TD") {
            if (className != "releaseRightschkChildClass") {
                var checkbox = $(selectedTr).find("#releaseRightschkChild")[0];
                if(checkbox !='undefined')
                    $(checkbox).attr("checked", !checkbox.checked);
            }
            synGridCheckBoxSelectionReleaseRightsAcquired(event);
        }
    });
}


function ReleaseRightsCheckBoxAllClick(args) {
    var grid_Id = $('#hdnGridId').val();
    var obj = $("#" + grid_Id).find(".GridHeader #releaseRightschkAll");
    if (obj.attr("checked") == "checked") {
        $("#" + grid_Id + " .GridContent").find('#releaseRightschkChild').attr("checked", "checked");
    }
    else {
        $("#" + grid_Id + " .GridContent").find('#releaseRightschkChild').removeAttr("checked");
    }
    synGridCheckBoxSelectionReleaseRightsAcquired(args);
}

function synGridCheckBoxSelectionReleaseRightsAcquired(events) {
    var curRow, curindex, ckFlag, tagValidchk;
    var grid_Id = $('#hdnGridId').val();
    var tablePositiontop = $("#" + grid_Id + " .GridContent").children("div:first").css("top");
    var tablePositionLeft = $("#" + grid_Id + " .GridContent").children("div:first").css("left");
    var vscrollposition = $("#" + grid_Id + " .sf-sp-Vhandle").css("top");
    var hscrollposition = $("#" + grid_Id + " .sf-sp-Hhandle").css("left");
    var hscrollHeader = $("#" + grid_Id + " .GridHeader .Table").css("left");
    //events.stopImmediatePropagation();
    var gridObjRights = $find(grid_Id);
    var checkboxes = $("#" + grid_Id).find(".GridContent #releaseRightschkChild");
    if ($.browser.msie) {
        tagValidchk = events.srcElement.outerHTML;
        tagValidchk = $(tagValidchk).attr("id");
    } else
        tagValidchk = events.target.id;

    if (tagValidchk != "releaseRightschkAll") {
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

        selectedGridItems.push(
        {
            KeyId: getValue(getKeyValue(value.ResourceId, value.ReleaseId)),
            R2KeyId: getR2KeyValue(value.R2ResourceId, value.R2ReleaseId),
            ContractId: getValue(value.ContractId),
            LinkType: getValue(linkType),
            RightSetId: getValue(value.RightSetId)
        });
    });
    $("#" + grid_Id + " .GridContent").children("div:first").css("top", tablePositiontop);
    $("#" + grid_Id + " .GridContent").children("div:first").css("left", tablePositionLeft);
    $("#" + grid_Id + " .sf-sp-Vhandle").css("top", vscrollposition);
    $("#" + grid_Id + " .sf-sp-Hhandle").css("left", hscrollposition);
    $("#" + grid_Id + " .GridHeader .Table").css("left", hscrollHeader);
}



//Bulk Edit Popup open
function callBulkEditReleaseButton() {
    $("#" + releaseBulkEditBtnId).click(function () {
        var grid_Id = $('#hdnGridId').val();
        var bulkEditRecordlength = $find(grid_Id).get_SelectedRecords().length;
        if (bulkEditRecordlength >= 1) {
            var objbulkEditReleaseRightsAcquired = $('<div class="divobjBulkEditRightsAcquired"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Bulk Edit - Release Rights Acquired',
            show: 'clip',
            hide: 'clip',
            width: 900,
            height: 550,
            resizable: false,
            close: function () {
                $(this).remove();
                $("#" + grid_Id).mousedown();
            }
        });
            objbulkEditReleaseRightsAcquired.load('/GCS/RightsReviewWorkQueue/RightsAcquiredBulkEdit', "",
                        function (responseText, textStatus) {
                            if (textStatus == 'error') {
                                objbulkEditReleaseRightsAcquired.html('<p>' + messageCommon.error + '</p>');
                            }
                        });
            objbulkEditReleaseRightsAcquired.dialog('open');
            trEditType = "Bulk";
        } else {
            ShowWarning('Please select Repertoire');
            SyncfusionGridScroll(grid_Id);
        }
    });
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
