﻿/// <reference path="RepertoireRightsSearch.js" />
/// <reference path="../LayoutRoot.js" />

var objDialog = $('<div></div>')
    .html('<p>' + messageCommon.onLoading + '</p>')
    .dialog({
        autoOpen: false,
        modal: true,
        title: messageCommon.warningTitle,
        show: 'clip',
        hide: 'clip',
        width: 300
    });

$(document).ready(function ($) {

    /****************************** Color Representation for Grid Starts **************************/

    $(".StackedHeader th:lt(19)").attr('style', 'background-color: #8EB3DD !important; padding: 4px !important; border: none;');
    $(".StackedHeader th:nth-child(19)").attr('style', 'background-color: #9acbef !important; padding: 4px !important;');
    $(".StackedHeader th:nth-child(20)").attr('style', 'background-color: #ffdc25 !important; padding: 4px !important;');

    $(".HeaderBar th:lt(25)").attr('style', 'background-color: #B8D0E9 !important');
    $(".HeaderBar th:lt(26):gt(17)").attr('style', 'background-color: #c2e0f5 !important');
    $(".HeaderBar th:gt(25)").attr('style', 'background-color: #fcf190 !important');

    /****************************** Color Representation for Grid Ends **************************/

    //BreadCrumbs
//    $('#Home').html('<a>Repertoire Rights Search </a>');
//    $('#Repertoire_Rights_Search').html(
//            '<a>Rights & Digital Restrictions</a>' +
//                '<label class="breadCrumbSeparator">>> </label>' +
//                '<a>Show Releases</a>'
//        );
    $('.gr_breadCrumbNav').html("Repertoire Rights Search > > Rights & Digital Restrictions > > Show Releases");
    
    $("#jqgrid").css('width', $(window).width() - 30);
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
        var checkSearchCriteria = CheckSearchCriteria();
        var digitalRestrictionValidationSuccess = validateDigitalRestrictionCombinationforSearch();
        var validateDigitalRestrictionForRow = validateDigitalRestrictionRow();

        if (digitalRestrictionValidationSuccess == false)
            return false;
        
        if (validateDigitalRestrictionForRow == false) {
            ShowError("Please complete the Digital Restriction search criteria");
            return false;
        }
        
        if (!isValidationSuccess) {
            //Date time Validation to be done first.
            return false;
        }
        else if ((!checkSearchCriteria) && isValidationSuccess) {
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
                $find("DigitalRestrictionsReleases").sendRefreshRequest();
            });
            return true;
        }
        else {
            ShowError(RepertoireRightsSearchConstants.searchValidation);
            return false;
        }
    });

    //Export to Excel Report
    $('#ListExcelExport').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        var isValidationSuccess = DateValidationOnSearchClick();
        var checkSearchCriteria = CheckSearchCriteria();
        var digitalRestrictionValidationSuccess = validateDigitalRestrictionCombinationforSearch();
        var validateDigitalRestrictionForRow = validateDigitalRestrictionRow();

        if (digitalRestrictionValidationSuccess == false)
            return false;
        
        if (validateDigitalRestrictionForRow == false) {
            ShowError("Please complete the Digital Restriction search criteria");
            return false;
        }

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

    // Reset button click 
    $('#btnResetRepertoireRightsSearchCriteria').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();

        $('#delIcon').hide();
        $('#endDateValidation').hide();

        $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').attr('disabled', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', false);
        $('#SearchRightsContract').attr('disabled', false);

        $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('checked', false);
        $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val('');
        $('#AdminCompanyNames').val('');

        $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').removeClass('disabled');
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').removeClass('disabled');
        $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').removeClass('disabled');
        $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').removeClass('disabled');
        
        $('#SearchRightsContract').removeClass('disabled');
        $('#SearchRightsContract').removeClass('SearchRightsContractGrey');
        $('#SearchRightsContract').addClass('SearchRightsContractNormal');
        
        $('#SearchCriteria_SearchRightsCriteria_TerritorialRightsId').val('');
        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractId').val('');
        $('#hiddenInvalidUPCs').val('');
        $('#hiddenIsViewSensitiveRightsData').val('');

        $('#SearchCriteria_SearchRightsCriteria_RightsPeriodId')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_LostRightsIndicator')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsPhysicalExploitationRights')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_LostRightsReasonId')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_IsDigitalExploitationRights')[0].selectedIndex = 0;
        $('#SearchCriteria_SearchRightsCriteria_FromDt').val('From');
        $('#SearchCriteria_SearchRightsCriteria_ToDt').val('To');

        var digitalVal = $("#RightsRepertoireSearchForm").serialize();
        //$('#loadingDiv').show();
        $.post('/GCS/RepertoireRightsSearch/ResetDigitalRestrictions/', digitalVal, function (data) {
            $('.sample').html($(data).find(".sample").html());
            MethodsToBeIncludedAfterPost();
        })
                .error(function () {
                    objDialog.html('<p>' + messages.serverError + '</p>');
                    objDialog.dialog('open', { title: messageCommon.warningTitle });
                });
    });

    CheckBoxValidationForRestrictionExists();

    BtnAddDigitalRestrictions();

    BtnRemoveDigitalRestrictions();
});



//CheckBoxValidationForRestrictionExists : Checkbox functionality for a specific row in DR table
function CheckBoxValidationForRestrictionExists() {
    $(".selector").children().children("input:checkbox").click(function (e) {
        var test = $(this)[0].className;
        if ($(this).attr('checked') == 'checked') {
            $('input[class=' + test + ']').each(function () {
                $(this).attr('checked', false);
            });
            $(this).attr('checked', true);
        }
    });
}


//Remove Digital Restrictions Function
function BtnRemoveDigitalRestrictions() {
    $('.btnRemoveDigitalTemplate').on("click", function (e) {
        e.preventDefault();

        //Image respective checkbox for model linkage for digital row removal
        $('input[id=' + $(this)[0].id + ']').attr('checked', true);

        var digitalVal = $("#RightsRepertoireSearchForm").serialize();
        $('#loadingDiv').show();
        $.post('/GCS/RepertoireRightsSearch/RemoveDigitalRestrictions/', digitalVal, function (data) {
            $('.sample').html($(data).find(".sample").html());
            MethodsToBeIncludedAfterPost();
        })
            .error(function () {
                objDialog.html('<p>' + messages.serverError + '</p>');
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });
    });
}


//Add Digital Restrictions Function
function BtnAddDigitalRestrictions() {
    $('#btnAddDigitalTemplate').click(function (e) {
        e.preventDefault();
        e.stopPropagation();

        $('#loadingDiv').show();
        var digitalVal = $("#RightsRepertoireSearchForm").serialize();
        $.post('/GCS/RepertoireRightsSearch/AddDigitalRestrictions', digitalVal, function (data) {
            $('.sample').html($(data).find(".sample").html());
            $('#DigitalExploitationListValidation').hide();
            MethodsToBeIncludedAfterPost();
        })
            .error(function () {
                objDialog.html('<p>' + messageCommon.error + '</p>');
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });
    });
}


//MethodsToBeIncludedAfterPost
function MethodsToBeIncludedAfterPost() {
    CheckBoxValidationForRestrictionExists();
    BtnRemoveDigitalRestrictions();
}

function validateDigitalRestrictionCombinationforSearch() {
    var result = true;
    var digitalarray = new Array();

    $("select.UseTypeList").each(function () {
        var tempUseType = $(this).val();
        var tempCommercialModel = $($("select.CommercialModelList")[$("select.UseTypeList").index(this)]).val();
        digitalarray.push(tempUseType + tempCommercialModel);
    });

    for (var row = 0; row < digitalarray.length; row++) {
        var rowid = arrHasDupes(digitalarray, row);
        if (rowid > 0) {
            var useType = $($("select.UseTypeList")[rowid]);
            popupdialogDuplicateSearch(useType);
            return false;
        }
    }
    return result;
}

function arrHasDupes(arrayValues, id) {                  // finds any duplicate array elements using the fewest possible comparison
    var counter;
    var arrayLength;
    arrayLength = arrayValues.length;
    for (counter = 0; counter < arrayLength; counter++) { // outer loop uses each item i at 0 through n
        if (counter != id)
            if (arrayValues[id] == arrayValues[counter]) return counter;
    }
    return -1;
}

function popupdialogDuplicateSearch(tempUseType) {
    objDialog.html('<p>' + messageCommon.digitalUnique + '</p>');
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); tempUseType.focus(); } }, close: function () { tempUseType.focus(); } });
    objDialog.dialog('open');
}


//Check the Search Critaria 
//Checks Mandatory Fields : 
//Return Value : False -> Success : Grid to reload
//Return Value : True -> Fail : Alerts user error.
function CheckSearchCriteria() {
    var upc = $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val();
    var artistName = $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val();
    var releaseTitle = $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').val();
    var linkedContractName = $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val();
    var countries = $('#AdminCompanyNames').val();
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
    var fromDate = $('#SearchCriteria_SearchRightsCriteria_FromDt').val();
    var toDate = $('#SearchCriteria_SearchRightsCriteria_ToDt').val();

    if ($('#pageName').val() == 'DigitalRestrictionsReleases') {
        return ((upc == "" || upc == null)
            && (artistName == "" || artistName == null)
            && (releaseTitle == "" || releaseTitle == null)
            && (linkedContractName == "" || linkedContractName == null)
            && (countries == "" || countries == null)
            && (rightsPeriod == "" || rightsPeriod == null)
            && (lostRightsIndicator == "" || lostRightsIndicator == null)
            && (physicalExpRights == "" || physicalExpRights == null)
            && (lostRightsReason == "" || lostRightsReason == null)
            && (digitalExploitationRights == "" || digitalExploitationRights == null)
            && (fromDate == "From" || fromDate == "" || fromDate == null)
            && (toDate == "To" || toDate == "" || toDate == null));

    } else {
        return false;
    }
}


/*SyncFusion Grid Code*/

function DigRestRowDataBound(sender, args) {
    var image = '';
    var tooltipText = null; 
    switch (args.Data.ReleaseType) {
        case 'PR':
            image = '<img  src="/GCS/Images/Physical_Release.gif">';
            tooltipText = 'Physical Release';
            break;
        case 'PP':
            image = '<img  src="/GCS/Images/package_physical.gif">';
            tooltipText = 'Physical Package';
            break;
        case 'DR': image = '<img  src="/GCS/Images/Digital_Release.gif">';
            tooltipText = 'Digital Release';
            break;
        case 'DP': image = '<img  src="/GCS/Images/package_digital.gif">';
            tooltipText = 'Digital Package';
            break;
        case 'MPR':
            image = '<img  src="/GCS/Images/MAC_Img.png">';
            tooltipText = 'Multi-Artist Compilation';
            break;
        case 'MPP':
            image = '<img  src="/GCS/Images/MAC_Img.png">';
            tooltipText = 'Multi-Artist Compilation';
            break;
        case 'MDR': image = '<img  src="/GCS/Images/MAC_Img.png">';
            tooltipText = 'Multi-Artist Compilation';
            break;
        case 'MDP': image = '<img  src="/GCS/Images/MAC_Img.png">';
            tooltipText = 'Multi-Artist Compilation';
            break;

    }     
    var tdResourceType = args.Element.children[9];
    $(tdResourceType).html(image);
    if (tooltipText != null) {
        $(tdResourceType).attr("title",  tooltipText);
        $(tdResourceType).tooltip();//{ showBody: "~" });
    }

    image = '';
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
            image = '<div class=\'rightsSplit\'></div>';
        }
        else if (args.Data.IsSplitContract == "2") {//Empty
            image = '';
        }
        else {
            image = '<div class=\'rightslinkedContract\'></div>';
        }
    }
    var linkedContract = args.Element.children[15];
    $(linkedContract).html(image);
    if (htmlText != null) {
        $(linkedContract).attr("title", htmlText);
        $(linkedContract).tooltip();//{ showBody: "~" });
    }

    image = '';
    htmlText = args.Data.LookupDataSourceText;
    if (args.Data.LkupDataSourceVal == "1") {//Roll-Up-Complete : Green flag
        image = '<div class="rightsSourceIconComplete"></div>';
    }
    else if (args.Data.LkupDataSourceVal == "2") {//Roll-Up-Partial(Missing Rights) : Red flag
        image = '<div class="rightsSourceIconPartial"></div>';
    }
    else if (args.Data.LkupDataSourceVal == "3") {//Roll-Up-Partial(Complex Rights) : Red flag
        image = '<div class="rightsSourceIconPartial"></div>';
    }
    else if (args.Data.LkupDataSourceVal == "4") {//Roll-Up-Partial(Missing & Complex Rights) : Red flag
        image = '<div class="rightsSourceIconPartial"></div>';
    }
    else if (args.Data.LkupDataSourceVal == "5") {//By User : Green flag
        image = '<div class="rightsSourceIconComplete"></div>';
    }
    else if (args.Data.LkupDataSourceVal == "6") {//Contract : Yellow flag
        image = '<div class="rightsSourceIconContract"></div>';
    }
    else if (args.Data.LkupDataSourceVal == "7") {//Clearance Request : Green flag
        image = '<div class="rightsSourceIconComplete"></div>';
    }
    var dataSource = args.Element.children[25];
    $(dataSource).html(image);
    if (htmlText != null) {
        $(dataSource).attr("title", htmlText);
        $(dataSource).tooltip(); //{ showBody: "~" });
    }
}

//function GridBegin(sender, args) {
//    $("#DigitalRestrictionsReleases .MsgBar").empty();
//    $("#DigitalRestrictionsReleases .MsgBar").text("Search Results (0)");
//    $(".EmptyCell").html("Retrieving Records");
//    args.data["pageName"] = $('#pageName').val();
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
//    $("#DigitalRestrictionsReleases .MsgBar").empty();
//    $("#DigitalRestrictionsReleases .MsgBar").text("Search Results (" + totCount + ")");
//    $("#DigitalRestrictionsReleases .manualPagerLabel:eq(1)").empty();
//    $("#DigitalRestrictionsReleases .manualPagerLabel:eq(1)").text("Show item per page");
//}

//function ActionSuccess(sender, args) {
//    var totCount = sender.get_TotalRecordsCount();
//    $('#warning').hide();
//    $("#DigitalRestrictionsReleases .MsgBar").empty();
//    $("#DigitalRestrictionsReleases .MsgBar").text("Search Results (" + totCount + ")");
//    SyncfusionGridScroll();
//    setSyncGridToolTip("#DigitalRestrictionsReleases_Table");
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

//    var jtableTop = $("#DigitalRestrictionsReleases").offset().top;
//    var topfootPos = $(".footer").offset().top;
//    var totRecHeight = $("#DigitalRestrictionsReleases_Table").height() + reduceHgt;
//    var tableHeight = topfootPos - jtableTop;
//    var gridObj = $find("DigitalRestrictionsReleases");
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
//    $find("DigitalRestrictionsReleases").sendRefreshRequest();
//}

//$(function () {
//    setTimeout(function () {
//        var gridObj = $find("DigitalRestrictionsReleases");
//        gridObj.set_AllowSelection(true);
//        gridObj.set_AllowSelection(false);
//    }, 0);

//    $("#DigitalRestrictionsReleases_Table").bind("click", function (event) {
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
//    var obj = $("#DigitalRestrictionsReleases").find(".GridHeader #chkAll");
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
//    var obj = $("#DigitalRestrictionsReleases").find(".GridHeader #chkAll");
//    if (obj.attr("checked") == "checked") {
//        $(".GridContent").find('#chkChild').attr("checked", "checked");
//    }
//    else {
//        $(".GridContent").find('#chkChild').removeAttr("checked");
//    }
//    synGridCheckBoxSelection(args);
//}


//function synGridCheckBoxSelection(events) {
//    var gridObj = $find("DigitalRestrictionsReleases");
//    var checkboxes = $("#DigitalRestrictionsReleases").find(".GridContent #chkChild");
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

function MergeRows(sender, args) {
    if (args.GridCell.Column.Member == "DigitalRestriction.MergeCount") {
        if (args.GridCell.Text.toString() != "0") {
            args.SetRange(parseInt(args.GridCell.Text.toString()), 1);
        }
    }
    var all = "false";
    if(args.GridCell.Column.Member == "") {
     var allHeader = args.GridCell.Element.children[0];
        if (allHeader.tagName == "INPUT") {
            var allid = allHeader.getAttribute("id");
            if (allid == "chkChild")
                all = "true";
        }
    }
    if (all == "true" || args.GridCell.Column.Member == "ResourceType" || args.GridCell.Column.Member == "Isrc" || args.GridCell.Column.Member == "Upc" || args.GridCell.Column.Member == "Artist" || args.GridCell.Column.Member == "Title" || args.GridCell.Column.Member == "VersionTitle" || args.GridCell.Column.Member == "UPCId" || args.GridCell.Column.Member == "Artist" || args.GridCell.Column.Member == "ReleaseDate" || args.GridCell.Column.Member == "LinkedContract" || args.GridCell.Column.Member == "ClearanceDataAdminCompany" || args.GridCell.Column.Member == "IsArtist" || args.GridCell.Column.Member == "IsSample" || args.GridCell.Column.Member == "PYear" || args.GridCell.Column.Member == "ReviewStatus" || args.GridCell.Column.Member == "IsActiveForMarketing" || args.GridCell.Column.Member == "TerritorialRights" || args.GridCell.Column.Member == "RightsPeriod" || args.GridCell.Column.Member == "IsLostRightsIndicator" || args.GridCell.Column.Member == "LostRightsReason" || args.GridCell.Column.Member == "LostRightsDate" || args.GridCell.Column.Member == "IsPhysicalExploitationRights" || args.GridCell.Column.Member == "IsDigitalExploitationRights" || args.GridCell.Column.Member == "IsMobileExploitationRights" || args.GridCell.Column.Member == "IsPpbRevenueClaim" || args.GridCell.Column.Member == "IsDigitalUnbundlingAllowed" || args.GridCell.Column.Member == "LkupDataSourceVal") {
        if (args.GridCell.Data.DigitalRestriction.MergeCount != "0") {
            args.SetRange(parseInt(args.GridCell.Data.DigitalRestriction.MergeCount), 1);
        }
    }
}

