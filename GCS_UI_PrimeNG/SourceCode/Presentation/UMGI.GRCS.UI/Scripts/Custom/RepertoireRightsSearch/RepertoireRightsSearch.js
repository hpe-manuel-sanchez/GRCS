/// <reference path="/GCS/Scripts/Custom/AuditTrail.js" />

var selectedRepertorieRightsItems = [];
var blRightsSensitiveData = null;

var objDialog = $('<div></div>')
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

//integrate Countries
var isChangeLink = false;
var objClearanceDialog = $('<div id="ClearanceAdminDialog"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: '',
            show: 'clip',
            hide: 'clip',
            width: getPopupWidth(55, 550),
            height: getPopupHeight(80, 300, 60),
            minHeight: 300,
            minWidth: 550,
            position: [getXPosition(55, 0), getYPosition(80, 60)],
            resizable: true,
            resize: function (event, ui) {
                if ($("#ClearanceAdminDialog").is(':visible')) {
                    if ($(".cacTableContainer").is(':visible')) {
                        var TotDiaHgt = $("#ClearanceAdminDialog").height();
                        var ReduceHgt = TotDiaHgt - 110;
                        $(".cacTable").css('height', ReduceHgt + "px");
                    }
                }
            },
            close: function () {
                $('#btnSearchRepertoireRights').focus();
            }
        });

function addCountries() {
    $('#ReviewRight').attr('disabled', 'disabled');
    $('#ReRoute').attr('disabled', 'disabled');

    reSizePriorityWorkQueuePage();
    $('#clrfilter').hide();

    //Clearance Company link Load partial view
    objClearanceDialog.load('/GCS/RepertoireRightsSearch/ClearanceAdminCountry', "",
                 function (responseText, textStatus) {
                     if (textStatus == 'error') {
                         objClearanceDialog.html('<p>' + messageCommon.error + '</p>');
                     }
                 });

    $('#clearanceAdminCountry').click(function (e) {
        e.preventDefault();
        objClearanceDialog.dialog('option', { title: RepertoireRightsSearchConstants.SelectRemoveCountries });
        objClearanceDialog.dialog('open');
        if ($(".cacTableContainer").is(':visible')) {
            var TotDiaHgt = $("#ClearanceAdminDialog").height();
            var ReduceHgt = TotDiaHgt - 110;
            $(".cacTable").css('height', ReduceHgt + "px");
        }
    });


    //Clear Filter Click
    $('#clrfilter').click(function (e) {
        e.preventDefault();
        $('#FilterApp').hide();
        $('#clrfilter').hide();
        $('#ArtistName').val('');
        $('#ContractDescription').val('');
        $('#Titles').val('');
        $('#AdminCompany').val('');
        $('#AdminCompanyNames').val('');

        $('#WorkQueues_ArtistName').val('142');
        $('#WorkQueues_Title').val('142');
        $('#WorkQueues_ContractDescription').val('142');
        $('#WorkQueues_ContractReviewReason').val('0');

        $('#accordion').next().toggle();
        $($('#accordion').find('a')[0]).toggleClass('iconBottom');
        $('#workQueueGrid').jtable('load', {
            artistName: $('#ArtistName').val(),
            contractDesc: $('#ContractDescription').val(),
            descTitle: $('#Titles').val(),
            reasonForReview: $('#WorkQueues_ContractReviewReason option:selected').text(),
            adminCompany: $('#AdminCompany').val()
        });

    });

}

//Resize PriorityWorkQueue Page
function reSizePriorityWorkQueuePage() {
    var h = $(window).height();
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".scrollWorkQueue").css('height', h - 185);
    else
        $(".scrollWorkQueue").css('height', h - 225);
}


function centerPopup() {
    $('.ClearanceAdminDialog').each(function () {
        $(this).css('left', ($(window).width() - $(this).outerWidth()) / 2 + 'px');
        $(this).css('top', ($(window).height() - $(this).outerHeight()) / 2 + 'px');
    });
};

var pageSize = 25; //jqgrid List Page Size

function resizeAccordion() {
    $('#accordion .head').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        $(this).next().toggle();
        $(this).find('a').toggleClass('iconBottom');
        $('#ddlFilterDatasetLevel').focus();
        resizeHandler();
        return false;
    }).next().show();
}

$(document).ready(function () {

    //Chrome enter click 

    $('#accordion').change(function () {
        $('#btnSearchRepertoireRights').focus();
    });

    $('#accordion').find('input:text').keyup(function (e) {
        if (e.keyCode == 13) {
            $('#btnSearchRepertoireRights').click();
        }
    });

    $('#accordion').find('textarea').keyup(function (e) {
        if (e.keyCode == 13) {
            $('#btnSearchRepertoireRights').click();
        }
    });

    $('#accordion').find('input:checkbox').change(function () {
        $('#btnSearchRepertoireRights').focus();
    });

//    var firstInput = $('RightsRepertoireSearchForm').find('input[type=text],input[type=radio],input[type=checkbox],textarea,select').filter(':visible:first');
//    if (firstInput != null) {
//        debugger;
//        $('#btnSearchRepertoireRights').focus();
//    }

    blRightsSensitiveData = $('#hiddenIsViewSensitiveRightsData').val();

    //synfusion paging validation
    syncGridPagerSearch();

    redirectToChangeLink();

    redirectToReviewRights();

    auditTrailRepertorie();

    ClearContractItems();
    DigitalRestricionParameter();
    $('#SearchRightsContract').addClass('SearchRightsContractNormal');
    $('#loadingDiv').hide()
    // hide it initially 
    .ajaxStart(function () {
        $(this).hide();
    });

    //Date Validation
    $('#SearchCriteria_SearchRightsCriteria_FromDt').change(function () {
        if (DateValidationOnSearchClick() != false) {
            //HideWarningSuccess();
            $('#endDateValidation').hide();
        }
    });
    $('#SearchCriteria_SearchRightsCriteria_ToDt').change(function () {
        if (DateValidationOnSearchClick() != false) {
            //HideWarningSuccess();
            $('#endDateValidation').hide();
        }
    });

    $("#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName").click(function () {
        $(this).blur();
    });

    $("#SearchCriteria_SearchRightsCriteria_TerritorialRightsId").click(function () {
        $(this).blur();
    });

    $('#SearchCriteria_SearchRightsCriteria_IsDigitalExploitationRights').change(function () {
        if ($('#SearchCriteria_SearchRightsCriteria_IsDigitalExploitationRights :selected').text() == "No") {
            $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed')[0].selectedIndex = 0;
            $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed').attr('disabled', true);
            $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed').addClass('disabled');
        }
        else {
            $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed').attr('disabled', false);
            $('#SearchCriteria_SearchRightsCriteria_IsDigitalUnbundlingAllowed').removeClass('disabled');
        }
    });

    $(window).bind("resize", resizePopUpHandler);
    function resizePopUpHandler(e) {
        if (e == undefined || $(e.target).hasClass('ui-dialog') == false) {

            $('#ClearanceAdminDialog').dialog('option', 'width', getPopupWidth(55, 550));
            $('#ClearanceAdminDialog').dialog('option', 'height', getPopupHeight(80, 300, 60));
            $('#ClearanceAdminDialog').dialog('option', 'position', [getXPosition(55, 0), getYPosition(80, 60)]);

            $('#RightsSearchContract').dialog('option', 'width', getPopupWidth(70, 750));
            $('#RightsSearchContract').dialog('option', 'height', getPopupHeight(80, 300, 60));
            $('#RightsSearchContract').dialog('option', 'position', [getXPosition(70, 0), getYPosition(80, 60)]);
        }
    }
    $("#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName").attr("readonly", "true");

    var watermark = 'From';
    var watermark1 = 'To';
    $('.rrs #SearchCriteria_SearchRightsCriteria_FromDt').blur(function () {
        if ($(this).val().length == 0)
            $(this).val(watermark).addClass('watermark');
    }).focus(function () {
        if ($(this).val() == watermark)
            $(this).val('').removeClass('watermark');
    }).val(watermark).addClass('watermark');
    $('.rrs #SearchCriteria_SearchRightsCriteria_ToDt').blur(function () {
        if ($(this).val().length == 0)
            $(this).val(watermark1).addClass('watermark');
    }).focus(function () {
        if ($(this).val() == watermark1)
            $(this).val('').removeClass('watermark');
    }).val(watermark1).addClass('watermark');


    resizeAccordion();

    SecondaryAccordionDefaultStatus();

    var h1 = $(window).height();

    $(".rrs").css('height', h1 - 122);

    $(".rrs").scroll(function () {

        $(".ui-datepicker").hide();
    });


    $(window).resize(function () {

        //pageScroll();
        reSizeRepertoireRightsSearch();
    });
    reSizeRepertoireRightsSearch();
    var isrcFunc = function () {
        
    };
    
   
    $('#SearchCriteria_SearchResourceReleaseCriteria_Isrc').keyup(function () {

        $('#SearchCriteria_SearchResourceReleaseCriteria_Isrc').val($('#SearchCriteria_SearchResourceReleaseCriteria_Isrc').val().replace(/\s/g, ''));
        
        if ($(this).val().length > 0) {
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').addClass('disabled');
            //SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').addClass('disabled');

            $('#SearchRightsContract').attr('disabled', true);

            $('#SearchRightsContract').removeClass('SearchRightsContractNormal'); //Bugzilla Id 4293
            $('#SearchRightsContract').addClass('SearchRightsContractGrey');
            $('#delIcon').hide(); //Bugzilla Id 5134

            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val('');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').val('');
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val('');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId')[0].selectedIndex = 0;
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').addClass('disabled');

        }
        else {
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').removeClass('disabled');

            $('#SearchRightsContract').attr('disabled', false);
            $('#SearchRightsContract').removeClass('SearchRightsContractGrey');
            $('#SearchRightsContract').addClass('SearchRightsContractNormal');

            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').removeClass('disabled');
        }

    });

    $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').keyup(function () {

        $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val($('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val().replace(/\s/g, ''));
        
        if ($(this).val().length > 0) {
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').addClass('disabled');

            $('#SearchRightsContract').attr('disabled', true);
            $('#SearchRightsContract').addClass('disabled');
            $('#SearchRightsContract').removeClass('SearchRightsContractNormal'); //Bugzilla Id 4293
            $('#SearchRightsContract').addClass('SearchRightsContractGrey');
            $('#delIcon').hide(); //Bugzilla Id 5134

            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').attr('disabled', true);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').val('');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val('');
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val('');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').addClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').addClass('disabled');

        }
        else {
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTrackTitle').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').removeClass('disabled');

            $('#SearchRightsContract').attr('disabled', false);
            $('#SearchRightsContract').removeClass('disabled');
            $('#SearchRightsContract').removeClass('SearchRightsContractGrey'); //Bugzilla Id 4293
            $('#SearchRightsContract').addClass('SearchRightsContractNormal');

            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').attr('disabled', false);
            $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').removeClass('disabled');
            $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').removeClass('disabled');
        }

    });

    //Paging--Commented due to Syncfution Upgrade
    //    $('#jqPager select').change(function () {
    //        HideWarningSuccess();
    //        pageSize = $('#jqPager select').val();
    //        var pageName = $('#pageName').val();
    //        var model = $("#RightsRepertoireSearchForm").serialize();
    //        $.post('/GCS/RepertoireRightsSearch/PopulateSearchCriteria', model, function () {
    //            $('#jqgrid').jtable({ 'pageSize': pageSize });
    //            $('#jqgrid').jtable('load', { pageName: pageName, jtPageSize: pageSize });
    //        });
    //    });

    uploadISRC();
    uploadUPC();

    //integrate countries
    addCountries();

    // Open Search Contract popup
    $('#SearchRightsContract').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        e.stopPropagation();
        $('#hiddenRepertorieRightsForLink').val('false');
        $("#RepertoireRightsSearchParameters_IsRepertoiteToMultipleContract").removeAttr("disabled");
        var objautoLinkingRepertoirePopupDialog = $('<div id="RightsSearchContract"></div>')
                              .html('<p>' + messageCommon.onLoading + '</p>')
                              .dialog({
                                  autoOpen: false,
                                  modal: true,
                                  title: RepertoireRightsSearchConstants.SearchContractTitle,
                                  show: 'clip',
                                  hide: 'clip',
                                  width: getPopupWidth(70, 750),
                                  height: getPopupHeight(80, 300, 60),
                                  minHeight: 300,
                                  minWidth: 750,
                                  position: [getXPosition(70, 0), getYPosition(80, 60)],
                                  resize: function (event, ui) {
                                      ResizeContractPopUp();
                                  },
                                  close: function () {
                                      $(this).remove(); // ensures any form variables are reset.
                                      $('#btnSearchRepertoireRights').focus();
                                      //pageScroll();
                                  }
                              });

        //  Load partial view
        objautoLinkingRepertoirePopupDialog.load('/GCS/Contract/ContractSearchPopup', "",
                        function (responseText, textStatus) {
                            $('#contractSearchPopup').find('.cntrctSearchPopupButtons').hide();
                            $('#contractSearchPopup').find('.splitCntrctSearchPopupButtons').hide();
                            $('#searchwarning').hide();
                            var Rights = $('#hiddenRepertorieRights').val();
                            if (Rights == 'RepertorieRights')
                                $('.cntrctSearchPopupButtons').hide();
                            if (textStatus == 'error') {
                                objautoLinkingRepertoirePopupDialog.html('<p>' + messageCommon.error + '</p>');
                            }
                        }
                        );
        objautoLinkingRepertoirePopupDialog.dialog('open');
        $('#loadingDiv').hide();
    });

});

//open dialog for Invalid Isrc's
function GetInvalidUploadAlert() {
    $("#dialogFileUpload").dialog({
        buttons:
        {
            'OK':
            function () {
                $(this).dialog('close');
            }
        }
    });
}


function reSizeRepertoireRightsSearch() {
    var h = $(window).height();
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none' && $('#Errorr').css("display") == 'none')
        $(".rrs").css('height', h - 122);
    else
        $(".rrs").css('height', h - 144);

    // $(".rrs").css("height", h - 122);
    resizeHandler();
}

//From Date and To Date Validation [UC0016-285,UC0016-295,UC016-305]//
function DateValidationOnSearchClick() {
    //blRightsSensitiveData = $('#hiddenIsViewSensitiveRightsData').val();
    var now = new Date();
    now = Date.parse(now);
    var fromDateValue = $('#SearchCriteria_SearchRightsCriteria_FromDt').val();
    var toDateValue = $('#SearchCriteria_SearchRightsCriteria_ToDt').val();
    if (fromDateValue != "From" && toDateValue == "To") {
        HideWarningSuccess();
        $('#endDateValidationText').html("Please select a 'To' date");
        $('#endDateValidation').show();
        return false;
    }
    if (fromDateValue == "From" && toDateValue != "To") {
        HideWarningSuccess();
        $('#endDateValidationText').html("Please select a 'From' date");
        $('#endDateValidation').show();
        return false;
    }
    var fromDt = Date.parse($('#SearchCriteria_SearchRightsCriteria_FromDt').val());
    var toDt = Date.parse($('#SearchCriteria_SearchRightsCriteria_ToDt').val());
    if (toDt == fromDt) {
        HideWarningSuccess();
        $('#endDateValidationText').html(RepertoireRightsSearchConstants.DateValidationEqual);
        $('#endDateValidation').show();
        return false;
    }
    if (toDt < fromDt) {
        HideWarningSuccess();
        $('#endDateValidationText').html(RepertoireRightsSearchConstants.DateValidation);
        $('#endDateValidation').show();
        return false;
    }
    if (now < toDt && blRightsSensitiveData == 'False') {
        //$('#endDateValidationText').html(RepertoireRightsSearchConstants.InsufficientPrivilege);
        $('#endDateValidation').hide();
        ShowError(RepertoireRightsSearchConstants.InsufficientPrivilege);
        return false;
    } else if (now < toDt && blRightsSensitiveData == 'True') {
        //$('#endDateValidationText').html(RepertoireRightsSearchConstants.SenstivePrivilege);
        $('#endDateValidation').hide();
        ShowError(RepertoireRightsSearchConstants.SenstivePrivilege);
        return true;
    }
    return true;
}


//Enable Resource Parameters
function enableResourceParameters() {
    $('#SearchCriteria_SearchResourceReleaseCriteria_Isrc').attr('disabled', false);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', false);
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', false);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').attr('disabled', false);
    $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', false);
    $('#SearchRightsContract').attr('disabled', false);
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', false);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').attr('disabled', false);
}

function ResizeContractPopUp() {
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
}

function pageScroll() {

    var JtableTop = $(".jtable-main-container").offset().top;
    var TopfootPos = $(".footer").offset().top;
    var TotRecHeight = $(".jtable").height() + 31;
    var TableHeight = TopfootPos - JtableTop;
    if (TotRecHeight >= TableHeight) {
        $(".jtable-main-container").css('height', TableHeight + "px");
    }
    else {
        $(".jtable-main-container").css('height', TotRecHeight + 30 + "px");
    }

}

function uploadISRC() {
    $('#fileISRCUpload').fileupload({
        dataType: 'text',
        url: '/GCS/RepertoireRightsSearch/GetIsrcData/',
        maxFileSize: 5000000,
        acceptFileTypes: /(\.|\/)(xls|xlsx)$/i,
        add: function (e, data) {
            $('#loadingDiv').show();
            data.submit();
        },
        success: function (data) {
            $('#loadingDiv').hide();
            $('#SearchCriteria_SearchResourceReleaseCriteria_Isrc').val('');
            if (data == ";") {
                ShowError(RepertoireRightsSearchConstants.fileEmptyMsg);
                enableResourceParameters();
                GetInvalidUploadAlert();
            }
            else if (data.substring(0, 7) == "Error: ") {
                ShowError(data.substring(7));
                GetInvalidUploadAlert();
            }
            else {
                var isrc = data.split(';');
                $('#SearchCriteria_SearchResourceReleaseCriteria_Isrc').val(isrc[0]);
                if (isrc[0].length > 0) {
                    disableForIsrc();
                }
                if (isrc[1].length > 0) {
                    HideWarningSuccess();
                    $('#hiddenInvalidIsrcs').val(isrc[1]);
                    objDialog.html('<p><div class="alertImage"/>' + RepertoireRightsSearchConstants.FileUploadMsgTitle + '<div style="overflow: auto;height: 100px;border: 1px solid black;">' + isrc[1] + '</div>' + '</p>');
                    objDialog.dialog('open', { title: messageCommon.alertTitle });
                    objDialog.dialog({
                        buttons: {
                            'OK': function () {
                                $(this).dialog('close');
                            }
                        }
                    });
                    GetInvalidUploadAlert();
                }

            }
            return false;
        },
        error: function () {
        }
    });
}

function uploadUPC() {
    $('#fileUPCUpload').fileupload({
        dataType: 'text',
        url: '/GCS/RepertoireRightsSearch/GetUpcData/',
        add: function (e, data) {
            $('#loadingDiv').show();
            data.submit();
        },
        success: function (data) {
            $('#loadingDiv').hide();
            $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val('');
            if (data == ";") {
                ShowError(RepertoireRightsSearchConstants.fileEmptyMsg);
                enableResourceParameters();
                GetInvalidUploadAlert();
            }
            else if (data.substring(0, 7) == "Error: ") {
                ShowError(data.substring(7));
                GetInvalidUploadAlert();
            }
            else {
                var upc = data.split(';');
                $('#SearchCriteria_SearchResourceReleaseCriteria_Upc').val(upc[0]);
                if (upc[0].length > 0) {
                    disableForUpc();
                }
                if (upc[1].length > 0) {
                    $('#hiddenInvalidUPCs').val(upc[1]);
                    objDialog.html('<p><div class="alertImage"/>' + RepertoireRightsSearchConstants.FileUploadMsgTitle + '<div style="overflow: auto;height: 100px;border: 1px solid black;">' + upc[1] + '</div>' + '</p>');
                    objDialog.dialog('open', { title: messageCommon.alertTitle });
                    objDialog.dialog({
                        buttons: {
                            'OK': function () {
                                $(this).dialog('close');
                            }
                        }
                    });
                    GetInvalidUploadAlert();
                }
            }
            return false;
        },
        error: function () {
        }
    });
}


function disableForIsrc() {
    $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').addClass('disabled');
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled');
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').addClass('disabled');
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').addClass('disabled');
    //$('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').addClass('disabled');
    $('#SearchRightsContract').attr('disabled', true);
    $('#SearchRightsContract').addClass('disabled');

    $('#SearchRightsContract').removeClass('SearchRightsContractNormal'); //Bugzilla Id 4293
    $('#SearchRightsContract').addClass('SearchRightsContractGrey');
    $('#delIcon').hide(); //Bugzilla Id 5134


    $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val('');
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle').val('');
    $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val('');
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId')[0].selectedIndex = 0;

    $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').addClass('disabled');
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').addClass('disabled');
}
function disableForUpc() {

    $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').addClass('disabled');
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsExactSearch').addClass('disabled');
    $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').addClass('disabled');
    //$('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').addClass('disabled');
    $('#SearchRightsContract').attr('disabled', true);
    $('#SearchRightsContract').addClass('disabled');
    
    $('#SearchRightsContract').removeClass('SearchRightsContractNormal'); //Bugzilla Id 4293
    $('#SearchRightsContract').addClass('SearchRightsContractGrey');
    $('#delIcon').hide();//Bugzilla Id 5134

    $('#SearchCriteria_SearchResourceReleaseCriteria_ReleaseTitle').val('');
    $('#SearchCriteria_SearchResourceReleaseCriteria_ArtistName').val('');
    $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val('');

    $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_IsMultipleContracts').addClass('disabled');
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').attr('disabled', true);
    $('#SearchCriteria_SearchResourceReleaseCriteria_ResourceTypeId').addClass('disabled');
}


function SecondaryAccordionDefaultStatus() {
    $('.SecondaryAccordion .head').next().hide();
    $('.SecondaryAccordion .head').find('a').toggleClass('iconBottom');
}

function resizeHandler() {
    var wid = $(".rrs #SearchCriteria_SearchResourceReleaseCriteria_ResourceTitle").width();


    if ($.browser.msie && parseInt($.browser.version, 10) === 7) {

        $(".rrs #SearchCriteria_SearchResourceReleaseCriteria_Isrc").css("width", wid - 38);
    }
    else {

        $(".rrs #SearchCriteria_SearchResourceReleaseCriteria_Isrc").css("width", wid - 32);
    }

}
//change link
function redirectToChangeLink() {
    $('#btnChangeLink').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();

        var isContractLinkingAvailable = true;
        var isRecordsSelected = true;
        if (selectedRepertorieRightsItems.length < 1) {
            isRecordsSelected = false;
            ShowWarning("Please select atleast one record.");
        }

        $.each(selectedRepertorieRightsItems, function(k, value) {
            if (value.ContractId == '' || value.ContractId == null) {
                ShowWarning("This action is not available - no Contract is linked");
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
                    resize: function(event, ui) {
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
                    close: function() {
                        $(this).remove(); // ensures any form variables are reset.
                    }
                });

            if (selectedRepertorieRightsItems.length > 0 && selectedRepertorieRightsItems != null) {
                isChangeLink = true;

                objDialogForChangeLinkConfirmation.dialog({ title: "Confirm" });
                objDialogForChangeLinkConfirmation.html("Do you want to change contract linking for the selected repertoire records?");
                objDialogForChangeLinkConfirmation.dialog('open');
                objDialogForChangeLinkConfirmation.dialog({
                    buttons: {
                        'Yes': function() {
                            $('#hiddenRepertorieRightsForLink').val('true');
                            objSearchContractDialog.html('<p>' + messageCommon.onLoading + '</p>');
                            objSearchContractDialog.load('/GCS/Contract/ContractSearchPopup/', "",
                                function(responseText, textStatus) {
                                    if (textStatus == 'error') {
                                        objSearchContractDialog.html('<p>' + messageCommon.error + '</p>');
                                    }
                                });

                            objSearchContractDialog.dialog('option', { title: "Search for Contract" });
                            objSearchContractDialog.dialog('open');
                            $(this).dialog('close');
                        },
                        'No': function() {
                            $('#hiddenRepertorieRightsForLink').val('false');
                            $(this).dialog('close');
                        }
                    }
                });
            }
        }
    });
}

//rights review condition

function redirectToReviewRights() {

    $('#btnReviewRights').click(function (e) {
        e.preventDefault();
        if ($('#hiddenIsViewRightsReviewWQ').val() == 'True') {
            var rightsetid = getRightSetIds();
            if (selectedRepertorieRightsItems.length > 0) {
                rightsetid = rightsetid.substring(0, rightsetid.length - 1);
                var breadCurmbs = '&path=' + encodeURIComponent($('.gr_breadCrumbNav').html());
                $.post('/GCS/WorkQueue/SetRightSetIdsForReviewRights', { rightSetIds: rightsetid }, function () {
                    if (selectedRepertorieRightsItems[0].LinkType == 'Resource') {
                        window.location.href = "/GCS/RightsReviewWorkQueue/CustomRightsReviewWorkQueue?customWorkQueueType=Resource" + breadCurmbs;
                    } else if (selectedRepertorieRightsItems[0].LinkType == 'Release') {
                        window.location.href = "/GCS/RightsReviewWorkQueue/CustomRightsReviewWorkQueue?customWorkQueueType=Release" + breadCurmbs;
                    }
                });
            }
            else {
                ShowError("Please select an item.");
            }
        }
        else {
            ShowError("You are not authorised to Review Rights.");
        }
    });
}

function getRightSetIds() {
    var rightsetid = "";
    for (var item = 0; item < selectedRepertorieRightsItems.length; item++) {
        rightsetid += selectedRepertorieRightsItems[item].RightSetId + ",";
    }
    return rightsetid;
}

function GridBegin(sender, args) {
    $("#" + $('#pageName').val() + " .MsgBar").empty();
    $("#" + $('#pageName').val() + " .MsgBar").text("Search Results (0)");
    //$(".EmptyCell").html("Retrieving Records");RC-1430
    args.data["pageName"] = $('#pageName').val();
}

//On Record Selection 
function OnRecordSelection(sender, args) {
    var tr = args.getRow();
    $(tr).find('input:checkbox').attr("checked", "checked");
    var result = sender.get_SelectedRecords();
    return false;
}

//On Record unselection
function OnRecordUnSelection(sender, args) {
    var tr = args.getRow();
    $(tr).find('input:checkbox').removeAttr("checked", "checked");
}

function onLoad(sender) {
    var chk = $(".GridHeader").find("th.HeaderCell .HeaderCellDiv")[0];
    $(chk).append("<input type=\"Checkbox\" id=\"chkAll\" onclick=\"CheckBoxAllClick(event)\"> All");
    var totCount = sender.get_TotalRecordsCount();
    $('#warning').hide();
    $("#" + $('#pageName').val() + " .MsgBar").empty();
    $("#" + $('#pageName').val() + " .MsgBar").text("Search Results (" + totCount + ")");
    $("#" + $('#pageName').val() + " .manualPagerLabel:eq(1)").empty();
    $("#" + $('#pageName').val() + " .manualPagerLabel:eq(1)").text("Show item per page");
}

function ActionSuccess(sender, args) {
    var totCount = sender.get_TotalRecordsCount();
//    if (totCount == -1) {
//        totCount = 0;
//        ShowError("Incomplete DR search criteria");
//    }
    $('#warning').hide();
    $("#" + $('#pageName').val() + " .MsgBar").empty();
    $("#" + $('#pageName').val() + " .MsgBar").text("Search Results (" + totCount + ")");
    SyncfusionGridScroll();
    setSyncGridToolTip("#" + $('#pageName').val() + "_Table");
    $("#jqgrid .manualPagerLabel:eq(1)").empty();
    $("#jqgrid .manualPagerLabel:eq(1)").text("Show item per page");
    AddedLastRowCell(sender);
//    var lastmergeRowindex = $(sender._gridContentTable).find('tbody > tr').children("td:[rowspan]").parent().last().index();
//    $(sender._gridContentTable).find('tbody tr:eq(' + lastmergeRowindex + ') td:[rowspan]').addClass('LastRowCell');
}

function ActionFailure(sender, args) {
    var pageName = $('#pageName').val();
    displayDialog("Error","Error in "+ pageName);
}

function AddedLastRowCell(sender) {
    var trslength = $(sender._gridContentTable).find("tr").length;
    trslength = trslength - 1;
    var lastTr = $(sender._gridContentTable).find("tr")[trslength];
    var selectTd = $(lastTr).children("td.RowCell")[0];
    var ismergeFlag = $(selectTd).hasClass('Merged');
    if (ismergeFlag == true) {
        var totMergeRow = trslength - 1;
        for (var i = totMergeRow; i >= 0; i--) {
            var nextTr = $(sender._gridContentTable).find("tr")[i];
            var nextTd = $(nextTr).children("td.RowCell")[0];
            var isNextTdmergeFlag = $(nextTd).hasClass('Merged');
            if (isNextTdmergeFlag == false) {
                var lastvisbleRowTd = $(nextTr).children("td.RowCell");
                $(lastvisbleRowTd).addClass("LastRowCell");
                break;
            }
        }
    }
}


function SyncfusionGridScroll() {
    var pagerHgt = $(".GridPager").height();
    var headerHgt = $(".GridHeader").height();
    var browsHgt = 0;
    if ($.browser.msie)
        browsHgt = 16;
    else
        browsHgt = 13;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;

    var jtableTop = $("#" + $('#pageName').val()).offset().top;
    var topfootPos = $(".footer").offset().top;
    var totRecHeight = $("#" + $('#pageName').val() + "_Table").height() + reduceHgt;
    var tableHeight = topfootPos - jtableTop;
    var gridObj = $find($('#pageName').val());
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

//To Refresh Grid
function RefreshClick() {
    $find($('#pageName').val()).sendRefreshRequest();
}

$(function () {
    setTimeout(function () {
        var gridObj = $find($('#pageName').val());
        gridObj.set_AllowSelection(true);
        gridObj.set_AllowSelection(false);
    }, 0);

    $("#" + $('#pageName').val() + "_Table").bind("click", function (event) {

        HideWarningSuccess();
        var Selected_tr = event.target.parentElement;
        var className = event.target.className;
        if (className == "chkChildClass")
            event.target = event.target.parentNode;
        if (event.target.tagName == "TD") {
            if (className != "chkChildClass") {
                var checkbox = $(Selected_tr).find("#chkChild")[0];
                $(checkbox).attr("checked", !checkbox.checked);
            }
            synGridCheckBoxSelection(event);
        }
    });
});


function CheckBoxAllClick(args) {
    var obj = $("#" + $('#pageName').val()).find(".GridHeader #chkAll");
    if (obj.attr("checked") == "checked") {
        $(".GridContent").find('#chkChild').attr("checked", "checked");
    }
    else {
        $(".GridContent").find('#chkChild').removeAttr("checked");
    }
    synGridCheckBoxSelection(args);
}


function synGridCheckBoxSelection(events) {
    var curRow, curindex, ckFlag, tagValidchk;
    var gridObj = $find($('#pageName').val());
    var gridid = $('#pageName').val();

    var tablePositiontop = $("#" + gridid + " .GridContent").children("div:first").css("top");
    var tablePositionLeft = $("#" + gridid + " .GridContent").children("div:first").css("left");
    var vscrollposition = $("#" + gridid + " .sf-sp-Vhandle").css("top");
    var hscrollposition = $("#" + gridid + " .sf-sp-Hhandle").css("left");
    var hscrollHeader = $("#" + gridid + " .GridHeader .Table").css("left");

    var checkboxes = $("#" + $('#pageName').val()).find(".GridContent #chkChild");
    if ($.browser.msie) {
        tagValidchk = events.srcElement.outerHTML;
        tagValidchk = $(tagValidchk).attr("id");
    } else
        tagValidchk = events.target.id;

    if (tagValidchk != "chkAll") {
        curRow = events.target.parentNode;
        curindex = $(curRow).index();
    }
    $.each(checkboxes, function (index, checkbox) {
        if (checkbox.checked) {

            var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];
            if (($.inArray(index, gridObj.get_SelectionManager().selectedRowsCollection)) == -1) {
                var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                gridObj.get_SelectionManager()._mDownHandler(eve);
            }

        }
        else {
            gridObj.get_SelectionManager().deselectRow(index);
        }
    });

    var result = gridObj.get_SelectedRecords();
    selectedRepertorieRightsItems = [];
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
            else if (releaseIdValue == '' && resourceIdValue == '') {
                linkType = 'Resource';
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
        selectedRepertorieRightsItems.push(
            {
                KeyId: getKeyValue(value.ResourceId, value.ReleaseId),
                R2KeyId: getR2KeyValue(value.R2ResourceId, value.R2ReleaseId),
                ContractId: getValue(value.ContractId),
                ArtistName: getValue(value.Artist),
                VersionTitle: getValue(value.VersionTitle),
                LinkType: linkType,
                RightSetId: getValue(value.RightsSetId)
            });
    });

    $("#" + gridid + " .GridContent").children("div:first").css("top", tablePositiontop);
    $("#" + gridid + " .GridContent").children("div:first").css("left", tablePositionLeft);
    $("#" + gridid + " .sf-sp-Vhandle").css("top", vscrollposition);
    $("#" + gridid + " .sf-sp-Hhandle").css("left", hscrollposition);
    $("#" + gridid + " .GridHeader .Table").css("left", hscrollHeader);
}

//Clearing Contract Items
function ClearContractItems() {
    $('textarea.deletable').wrap('<span class="deleteicon" />').after($('<span id="delIcon" style="right: 15px;top: -10px;"/>').click(function () {
        var objWarningDialogForDeleteContract = $('<div id="deleteContract Items"></div>')
            .html('<p>' + "Do you want to delete the linked contracts?" + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: "Confirm",
                show: 'clip',
                hide: 'clip',
                width: 300,
                close: function () { $(this).remove(); }
            });

        objWarningDialogForDeleteContract.dialog('open');
        objWarningDialogForDeleteContract.dialog({
            buttons:
                {
                    'Yes': function(e) {
                        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val('');
                        $('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractId').val('');
                        $('#delIcon').hide();
                        $(this).dialog('close');
                    },
                    'Cancel': function() {
                        $(this).dialog('close');
                    }
                }
        });
        //$(this).prev('input').val('').focus();
    }));


    if ($('#SearchCriteria_SearchResourceReleaseCriteria_LinkedContractName').val() == '') {
        $('#delIcon').hide();
    }
}

function DigitalRestricionParameter()  {
    AddDigitalRestricionParameter();
}
function AddDigitalRestricionParameter() {

    $('#btnAddDigitalTemplate').click(function(e) {
        e.preventDefault();
        //            e.stopPropagation();
        var length = $('#DRParameter').find('tr').length - 1;
        if (length < 0)
            length = 0;
        var digital = $('#AddDigitalRestriction').find('tr').html();
        $('#DRParameter').find('tbody').append('<tr>' + digital + '</tr>');

        $('#DRParameter').find('tr').each(function(i) {
            $(this).find('select, input').each(function() {
                if (i - 1 == length) {
                    $(this).attr('name', $(this).attr('name').replace( /\[k\]/g , "[" + length + "]"));
                    $(this).attr('id', $(this).attr('id').replace( /\_k\_/g , "_" + length + "_"));
                }

                $(this).attr('name', $(this).attr('name').replace( /\[\d\]/g , "[" + (i - 1).toString() + "]"));
                $(this).attr('id', $(this).attr('id').replace( /\_\d\_/g , "_" + (i - 1).toString() + "_"));
            });
        });
    });
}



//Audit Trail
//function getResourceAuditHistory(AuditTypeId) {
//    var SelectedItemIds = [];
//    //if ($find("searchContractSyncGrid").get_SelectedRecords().length > 0)
//        //SelectedItemIds = $find("searchContractSyncGrid").get_SelectedRecords()[0].ContractId;

//    var displayTitle;
//    //SelectedItemIds[0].ContractId;
//    //if ($("#hdnProjRefId").val().length > 0) {
//    displayTitle = "";
//    //    }
//    //    else {
//    //        displayTitle = '';
//    //    }
//    displayTitle = displayTitle + 'Contract Information';
//    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
//    return false;
//}

//Audity Trial
function auditTrailRepertorie() {
    $('#btnRepertoireAuditTrail').click(function () {
        if ($find($('#pageName').val()).get_SelectedRecords().length == 0 || $find($('#pageName').val()).get_SelectedRecords().length > 1) {
            ShowWarning("Please select any one Repertoire to view audit history");
            SyncfusionGridScroll();
            return false;
        }

        var SelectedItemIds = "";
        var elamentType = "";
        var searchResults = "";
        var displayTitle = 'Repertorie Rights Information';

        var gridid = $('#pageName').val();
        var obj = $find(gridid).get_SelectedRecords()[0];
        SelectedItemIds = obj.RightsSetId;

        if (SelectedItemIds == "") {
            ShowError("Please select any one Repertoire to view audit history");
            return false;
        }
        
        if (gridid.indexOf('Releases') >= 0) {

            elamentType = "ReleaseRightsAuditHistory";
            searchResults = obj.Artist + ',' + obj.Upc + ',' + obj.Title;
        }
        else if (gridid.indexOf('Resource') >= 0) {

            elamentType = "ResourceAndTracksRightsAuditHistory";
            searchResults = obj.Artist + ',' + obj.Isrc + ',' + obj.Title;
        }
        else if (gridid.indexOf('Track') >= 0) {
            searchResults = obj.Upc + ',' + '' + ',' + obj.Title + ',' + obj.Isrc + ',' + '' + ',' + obj.Artist;  // 2nd value ReleaseArtist , 5th value ReleaseTitle - sending comma by default
            elamentType = "TrackRepertorie";
        }
        else {
            return false;
        }
        displayAuditTrail(elamentType, SelectedItemIds, displayTitle, searchResults);
    });
}


//DR combination validation
function validateDigitalRestrictionRow() {
    var result = true;
    $("select.UseTypeList").each(function () {
        var tempUseType = $(this).val();
        var tempCommercialModel = $($("select.CommercialModelList")[$("select.UseTypeList").index(this)]).val();
        var restrictionExistOrNotValue = '';
        $(".RestrictionExistOrNot" + $("select.UseTypeList").index(this).toString()).each(function() {
            restrictionExistOrNotValue = restrictionExistOrNotValue + $(this)[0].checked;
        });
        if (tempUseType == '0' && tempCommercialModel == '0' && restrictionExistOrNotValue == 'falsefalse') {
            //do nothing 
        }
        else if (tempUseType == '0' || tempCommercialModel == '0' || restrictionExistOrNotValue == 'falsefalse') {
            result = false;
            return false;
        }
    });
    return result;
}