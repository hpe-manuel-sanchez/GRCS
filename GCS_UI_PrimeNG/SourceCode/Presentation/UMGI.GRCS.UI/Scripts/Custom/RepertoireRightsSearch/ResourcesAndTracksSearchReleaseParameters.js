﻿/// <reference path="RepertoireRightsSearch.js" />
/// <reference path="../LayoutRoot.js" />

var objWarningDialog = $('<div id="Warning"></div>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            resizable: true,
            width: 250,
            height: 150
        });

var pageSize = 25; //jqgrid List Page Size

$(document).ready(function ($) {

    /****************************** Color Representation for Grid Starts **************************/

    $(".StackedHeader th:lt(22)").attr('style', 'background-color: #8EB3DD !important; padding: 4px !important; border: none;');
    $(".StackedHeader th:nth-child(22)").attr('style', 'background-color: #9acbef !important; padding: 4px !important;');

    $(".HeaderBar th:lt(31)").attr('style', 'background-color: #B8D0E9 !important');
    $(".HeaderBar th:lt(32):gt(20)").attr('style', 'background-color: #c2e0f5 !important');

    /****************************** Color Representation for Grid Ends **************************/

//    $('#loadingDiv').hide()
//        .ajaxStart(function () {
//            HideWarningSuccess();
//            $('#loadingDiv').show();
//            if ($("#ResourcesAndTracksSearchReleaseParameterswaiting_WaitingPopup_Image").is(":visible") == true)
//                $('#loadingDiv').hide();
//        });

    //BreadCrumbs
//    $('#Home').html('<a>Repertoire Rights Search </a>');
//    $('#Repertoire_Rights_Search').html(
//            '<a>Rights </a>' +
//            '<label class="breadCrumbSeparator">>> </label>' +
//            '<a>Show Resources & Tracks </a>' +
//            '<label class="breadCrumbSeparator">>> </label>' +
//            '<b>Search Using Release Parameters</b>'
//    );

    $('.gr_breadCrumbNav').html("Repertoire Rights Search > > Rights > > Show Resources & Tracks > > Search Using Release Parameters");
    
    resizeHandler();

    $(window).resize(function () {
        resizeHandler();
    });

    function resizeHandler() {
        var wid = $(".rrs #SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle").width();


        if ($.browser.msie && parseInt($.browser.version, 10) === 7) {

            $(".rrs #SearchCriteria_SearchResourceReleaseCriteria_Upc").css("width", wid - 38);
        }
        else {

            $(".rrs #SearchCriteria_SearchResourceReleaseCriteria_Upc").css("width", wid - 32);
        }

    }

	//Datepicker functionality
    $(".datefield").css("width", "100px");
    $(".datefield").datepicker({ showOn: 'both', buttonImage: "/GCS/Images/Calender_Icon_img.png", buttonImageOnly: true, changeMonth: true, changeYear: true, yearRange: '1900:2100' });
    $(".datefield").each(function () {
        $(this).focusout(function () {

            if ($(this).val() == '')
                return true;

            if (validateDate($(this).val(), $.datepicker.regional[''].dateFormat) == false)
                $(this).val('');
        });
    });

    //Validating for Special Characters

//    $("#SearchCriteria_SearchResourceReleaseCriteria_Upc, #SearchCriteria_SearchResourceReleaseCriteria_ArtistName, #SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle").keypress(function (e) {
//        ValidateSpecialCharacters(e, "<,>,&,\", ,\\t,%,;,(,),{,},!");
//    });

    $("#SearchCriteria_SearchResourceReleaseCriteria_Upc, #SearchCriteria_SearchResourceReleaseCriteria_ArtistName, #SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle").keypress(function (e) {
        ValidateSpecialCharacters(e);
    });
	
    //Search Function
    $('#btnSearchRepertoireRights').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        var isValidationSuccess = DateValidationOnSearchClick();
        if (!isValidationSuccess) {
            //Date time Validation to be done first.
            return false;
        }
        else if ((!CheckSearchCriteria()) && isValidationSuccess) {
            //hide all accordion 
            $('#accordion .head').each(function () {
                if ($(this).next().is('.accbg:visible')) {
                    $(this).click();
                }
            });

            var model = $("#RightsRepertoireSearchForm").serialize();
            //pageSize = $('#jqPager select').val();
            var pageName = $('#pageName').val();
            $.post('/GCS/RepertoireRightsSearch/PopulateSearchCriteria', model, function () {
                var gridObj = $find("ResourcesAndTracksSearchReleaseParameters");
                gridObj.sendRefreshRequest();
            });
            return true;
        }
        else {
            ShowError(RepertoireRightsSearchConstants.searchValidation);
            return false;
        }
    });


    //Reset Button
    $('#btnResetRepertoireRightsSearchCriteria').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();

        $('#delIcon').hide();
        $('#endDateValidation').hide();

        $('#txtClearanceAdminComp').val('');
        $('#SelectedList').find('tr').remove();
        $('#ClearenceList').find('input:checkbox').attr('checked', false);
        $('#labelSelCount').html($('#SelectedList').find('input:checkbox').length);


        $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('checked', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('checked', false);
        $('#AdminCompanyNames').val('');

        $('#SearchCriteria_SearchRightsCriteria_TerritorialRightsId').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractId').val('');
        $('#hiddenInvalidUPCs').val('');
        $('#hiddenIsViewSensitiveRightsData').val('');

        $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').removeClass('disabled');
        $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').removeClass('disabled');
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').removeClass('disabled');
        
        $('#SearchRightsContract').removeClass('disabled');
        $('#SearchRightsContract').removeClass('SearchRightsContractGrey');
        $('#SearchRightsContract').addClass('SearchRightsContractNormal');
        
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').removeClass('disabled');
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').removeClass('disabled');

        $('#SearchCriteria_SearchRightsCriteria_RightsReviewStatusId')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_RightsPeriodId')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_LostRightsIndicator')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsPhysicalExploitationRights')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_LostRightsReasonId')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsDigitalExploitationRights')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsMobileExploitationRights')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsPpbRevenueChain')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsActiveForMarketing')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_FromDt').val('From');
        $('#SearchCriteria_SearchRightsCriteria_ToDt').val('To');

        $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed').attr('disabled', false);
        $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed').removeClass('disabled');



        $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', false);
        $('#SearchRightsContract').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', false);
    });

    //Export to Excel Report
    $('#ListExcelExport').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        var isValidationSuccess = DateValidationOnSearchClick();
        var checkSearchCriteria = CheckSearchCriteria();
        if (!isValidationSuccess) {
            //Date time Validation to be done first.
            return false;
        }
        else if ((!checkSearchCriteria) && isValidationSuccess) {
            var model = $("#RightsRepertoireSearchForm").serialize();
            var pageName = $('#pageName').val();
            $.post('/GCS/RepertoireRightsSearch/PopulateSearchCriteria', model, function () {
                window.location.href = '/GCS/RepertoireRightsSearch/CreaterepertoireRightsSearchExcel?pageName=' + pageName;
            });
            return true;
        }
        else {
            if (isValidationSuccess && checkSearchCriteria) {
                ShowError(RepertoireRightsSearchConstants.searchValidation);
                return false;
            }
        }
        return false;
    });
});

//Check the Search Critaria
//Checks Mandatory Fields : 
//Return Value : False -> Success : Grid to reload
//Return Value : True -> Fail : Alerts user error.
function CheckSearchCriteria() {
    var upc = $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val();
    var artistName = $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val();
    var resourceTitle = $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').val();
    var linkedContractName = $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val();
    var repLinkedToMultipleContract = $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').is(':checked');
    var countries = $('#AdminCompanyNames').val();
    var rightsReviewsStatusIndex = $('#SearchCriteria_SearchRightsCriteria_RightsReviewStatusId')[0].selectedIndex;
    var rightsReviewsStatus = $('#SearchCriteria_SearchRightsCriteria_RightsReviewStatusId')[0][rightsReviewsStatusIndex].text;
    var rightsPeriodIndex = $('#SearchCriteria_SearchRightsCriteria_RightsPeriodId')[0].selectedIndex;
    var rightsPeriod = $('#SearchCriteria_SearchRightsCriteria_RightsPeriodId')[0][rightsPeriodIndex].text;
    var lostRightsIndIndex = $('#SearchCriteria_SearchRightsCriteria_LostRightsIndicator')[0].selectedIndex;
    var lostRightsIndicator = $('#SearchCriteria_SearchRightsCriteria_LostRightsIndicator')[0][lostRightsIndIndex].text;
    var physicalExpRightsIndex = $('#SearchCriteria_SearchRightsCriteria_IsPhysicalExploitationRights')[0].selectedIndex;
    var physicalExpRights = $('#SearchCriteria_SearchRightsCriteria_IsPhysicalExploitationRights')[0][physicalExpRightsIndex].text;
    var lostRightsReasonIndex = $('#SearchCriteria_SearchRightsCriteria_LostRightsReasonId')[0].selectedIndex;
    var lostRightsReason = $('#SearchCriteria_SearchRightsCriteria_LostRightsReasonId')[0][lostRightsReasonIndex].text;
    var digitalExploitationRightsIndex = $('#SearchCriteria_SearchRightsCriteria_IsDigitalExploitationRights')[0].selectedIndex;
    var digitalExploitationRights = $('#SearchCriteria_SearchRightsCriteria_IsDigitalExploitationRights')[0][digitalExploitationRightsIndex].text;
    var digitalUnbundlingAllowedIndex = $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed')[0].selectedIndex;
    var digitalUnbundlingAllowed = $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed')[0][digitalUnbundlingAllowedIndex].text;
    var mobileExploitationRightsIndex = $('#SearchCriteria_SearchRightsCriteria_IsMobileExploitationRights')[0].selectedIndex;
    var mobileExploitationRights = $('#SearchCriteria_SearchRightsCriteria_IsMobileExploitationRights')[0][mobileExploitationRightsIndex].text;
    var ppbRevenueClaimIndex = $('#SearchCriteria_SearchRightsCriteria_IsPpbRevenueChain')[0].selectedIndex;
    var ppbRevenueClaim = $('#SearchCriteria_SearchRightsCriteria_IsPpbRevenueChain')[0][ppbRevenueClaimIndex].text;
    var isActiveForMarketingIndex = $('#SearchCriteria_SearchRightsCriteria_IsActiveForMarketing')[0].selectedIndex;
    var isActiveForMarketing = $('#SearchCriteria_SearchRightsCriteria_IsActiveForMarketing')[0][isActiveForMarketingIndex].text;
    var fromDate = $('#SearchCriteria_SearchRightsCriteria_FromDt').val();
    var toDate = $('#SearchCriteria_SearchRightsCriteria_ToDt').val();

    if ((upc == "" || upc == null)
                && (artistName == "" || artistName == null)
                && (resourceTitle == "" || resourceTitle == null)
                && (linkedContractName == "" || linkedContractName == null)
                && (repLinkedToMultipleContract == false)
                && (countries == "" || countries == null)
                && (rightsReviewsStatus == "" || rightsReviewsStatus == null)
                && (rightsPeriod == "" || rightsPeriod == null)
                && (lostRightsIndicator == "" || lostRightsIndicator == null)
                && (physicalExpRights == "" || physicalExpRights == null)
                && (lostRightsReason == "" || lostRightsReason == null)
                && (digitalExploitationRights == "" || digitalExploitationRights == null)
                && (digitalUnbundlingAllowed == "" || digitalUnbundlingAllowed == null)
                && (mobileExploitationRights == "" || mobileExploitationRights == null)
                && (ppbRevenueClaim == "" || ppbRevenueClaim == null)
                && (isActiveForMarketing == "" || isActiveForMarketing == null)
                && (fromDate == "" || fromDate == null || fromDate == "From")
                && (toDate == "" || toDate == null || toDate == "To")
) {
        return true;
    }
    else {
        return false;
    }
}



/*SyncFusion Grid Code*/

function checkImageForRepertoireRightsSearch(events, args) {
    var image = '';
    if (args.Column.Name == "ResourceType") {
        var tooltipText = null;
        if (args.Data.ResourceType == '1') {
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
        $(args.Element)[0].innerHTML = image;
        if (tooltipText != null) {
            $(args.Element).attr("title", tooltipText);
            $(args.Element).tooltip(); //{ showBody: "~" });
        }
    }
    if (args.Column.Name == "LinkedContract") {
        if (args.Data.LinkedContractInfo != null && args.Data.LinkedContractInfo != '') {
            var htmlText = '';
            var linkedContractInfo = args.Data.LinkedContractInfo.split('@');
            $.each(linkedContractInfo, function (index, value) {
                if (value != '') {
                    var nameDate = value.split(':');
                    htmlText = htmlText + nameDate[0]
                                            + "<br/>" + nameDate[1] + "<br/>";
                }
            });
            if (args.Data.IsSplitContract == "1") {
                image = '<img  src="/GCS/Images/split.png">';
            }
            else {
                image = '<img  src="/GCS/Images/linked_Contract.png">';
            }
        }
        $(args.Element)[0].innerHTML = image;
        if (htmlText != null) {
            $(args.Element).attr("title", htmlText);
            $(args.Element).tooltip(); //{ showBody: "~" });
        }
    }
    if (args.Column.Name == "IsSample") {
        if (args.Data.SampleExistValue != null) {
            $(args.Element).attr("title", args.Data.SampleExistValue);
            $(args.Element).tooltip(); //{ showBody: "~" });
        }
    }
    if (args.Column.Name == "ReviewStatus") {
        var htmlTextReviewStatus = null;
        if (args.Data.ReviewStatus == "1") {//New For Review:Blue
            htmlTextReviewStatus = "New For Review";
            image = '<img  src="/GCS/Images/Flag_Blue.png">';
        }
        else if (args.Data.ReviewStatus == "2") {//Final For Review:Orange
            htmlTextReviewStatus = "Final For Review";
            image = '<img  src="/GCS/Images/flag_orange.png">';
        }
        else if (args.Data.ReviewStatus == "3") {//Final:Green
            htmlTextReviewStatus = "Final";
            image = '<img  src="/GCS/Images/Flag_Green.png">';
        }
        $(args.Element)[0].innerHTML = image;
        if (htmlTextReviewStatus != null) {
            $(args.Element).attr("title", htmlTextReviewStatus);
            $(args.Element).tooltip(); //{ showBody: "~" });
        }
    }
}

function ResouceSearchBeforeDrop(sender, args) {
    if ((args.From <= 20 && args.To >= 21) || (args.From >= 21 && args.To <= 20)) {
        sender._columnDragDropMgr._isDroppable = false;
        alert("Column Reordering Not Allowed");
    }
}

//function GridBegin(sender, args) {
//    $("#ResourcesAndTracksSearchReleaseParameters .MsgBar").empty();
//    $("#ResourcesAndTracksSearchReleaseParameters .MsgBar").text("Search Results (0)");
//    $(".EmptyCell").html("Retrieving Records");
//    args.data["pageName"] = $('#pageName').val();
//    args.data["jtStartIndex"] = 0;
//    args.data["jtPageSize"] = 25;
//    args.data["jtSorting"] = null;
//}

////On Record Selection 
//function OnRecordSelection(sender, args) {
//    var tr = args.getRow();
//    $(tr).find('input:checkbox').attr("checked", "checked");
//    var result = sender.get_SelectedRecords();
//    return false;
//}

////On Record unselection
//function OnRecordUnSelection(sender, args) {
//    var tr = args.getRow();
//    $(tr).find('input:checkbox').removeAttr("checked", "checked");
//}

//function onLoad(sender) {
//    var chk = $(".GridHeader").find("th.HeaderCell .HeaderCellDiv")[0];
//    $(chk).append("<input type=\"Checkbox\" id=\"chkAll\" onclick=\"CheckBoxAllClick(event)\"> All");
//    var totCount = sender.get_TotalRecordsCount();
//    $('#warning').hide();
//    $("#ResourcesAndTracksSearchReleaseParameters .MsgBar").empty();
//    $("#ResourcesAndTracksSearchReleaseParameters .MsgBar").text("Search Results (" + totCount + ")");
//    $("#ResourcesAndTracksSearchReleaseParameters .manualPagerLabel:eq(1)").empty();
//    $("#ResourcesAndTracksSearchReleaseParameters .manualPagerLabel:eq(1)").text("Show item per page");
//}

//function ActionSuccess(sender, args) {
//    var totCount = sender.get_TotalRecordsCount();
//    $('#warning').hide();
//    $("#ResourcesAndTracksSearchReleaseParameters .MsgBar").empty();
//    $("#ResourcesAndTracksSearchReleaseParameters .MsgBar").text("Search Results (" + totCount + ")");
//    SyncfusionGridScroll();
//    setSyncGridToolTip("#ResourcesAndTracksSearchReleaseParameters_Table");
//    $("#jqgrid .manualPagerLabel:eq(1)").empty();
//    $("#jqgrid .manualPagerLabel:eq(1)").text("Show item per page");
//}


//function SyncfusionGridScroll() {
//    var pagerHgt = $(".GridPager").height();
//    var headerHgt = $(".GridHeader").height();
//    var browsHgt = 0;
//    if ($.browser.msie)
//        browsHgt = 16;
//    else
//        browsHgt = 13;
//    var reduceHgt = pagerHgt + headerHgt + browsHgt;

//    var jtableTop = $("#ResourcesAndTracksSearchReleaseParameters").offset().top;
//    var topfootPos = $(".footer").offset().top;
//    var totRecHeight = $("#ResourcesAndTracksSearchReleaseParameters_Table").height() + reduceHgt;
//    var tableHeight = topfootPos - jtableTop;
//    var gridObj = $find("ResourcesAndTracksSearchReleaseParameters");
//    if (totRecHeight >= tableHeight)
//        setSyncfusionGridHeight(gridObj, tableHeight - reduceHgt);
//    else
//        setSyncfusionGridHeight(gridObj, totRecHeight - reduceHgt + 20);
//}
//function setSyncfusionGridHeight(gridObj, height) {
//    gridObj.set_GridHeight(height);
//    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
//    gridObj.refreshScroller();
//}


////To Refresh Grid
//function RefreshClick() {
//    $find("ResourcesAndTracksSearchReleaseParameters").sendRefreshRequest();
//}

//$(function () {
//    setTimeout(function () {
//        var gridObj = $find("ResourcesAndTracksSearchReleaseParameters");
//        gridObj.set_AllowSelection(true);
//        gridObj.set_AllowSelection(false);
//    }, 0);

//    $("#ResourcesAndTracksSearchReleaseParameters_Table").bind("click", function (event) {
//        HideWarningSuccess();
//        var Selected_tr = event.target.parentElement;
//        var className = event.target.className;
//        if (className == "chkChildClass")
//            event.target = event.target.parentNode;
//        if (event.target.tagName == "TD") {
//            if (className != "chkChildClass") {
//                var checkbox = $(Selected_tr).find("#chkChild")[0];
//                $(checkbox).attr("checked", !checkbox.checked);
//            }
//            synGridCheckBoxSelection(event);
//        }
//    });
//});

//function CheckBoxAllClick(args) {
//    var obj = $("#ResourcesAndTracksSearchReleaseParameters").find(".GridHeader #chkAll");
//    if (obj.attr("checked") == "checked") {
//        $(".GridContent").find('#chkChild').attr("checked", "checked");
//        $(".GridContent").find("tr").children().not(".RowHeader").addClass("SelectionBackground");
//    }
//    else {
//        $(".GridContent").find('#chkChild').removeAttr("checked");
//        $(".GridContent").find("tr").children().not(".RowHeader").removeClass("SelectionBackground");
//    }
//}

//function CheckBoxAllClick(args) {
//    var obj = $("#ResourcesAndTracksSearchReleaseParameters").find(".GridHeader #chkAll");
//    if (obj.attr("checked") == "checked") {
//        $(".GridContent").find('#chkChild').attr("checked", "checked");
//    }
//    else {
//        $(".GridContent").find('#chkChild').removeAttr("checked");
//    }
//    synGridCheckBoxSelection(args);
//}


//function synGridCheckBoxSelection(events) {
//    var gridObj = $find("ResourcesAndTracksSearchReleaseParameters");
//    var checkboxes = $("#ResourcesAndTracksSearchReleaseParameters").find(".GridContent #chkChild");
//    $.each(checkboxes, function (index, checkbox) {
//        if (checkbox.checked) {
//            var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];
//            if (($.inArray(index, gridObj.get_SelectionManager().selectedRowsCollection)) == -1) {
//                var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
//                gridObj.get_SelectionManager()._mDownHandler(eve);
//            }
//        }
//        else {
//            gridObj.get_SelectionManager().deselectRow(index);
//        }
//    });
//    var result = gridObj.get_SelectedRecords();
//}