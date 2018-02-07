var priorityWorkQueueMessages;
/*Filter Fields Validation and functionailties*/
$(document).ready(function () {

    window.filterParams = { Resource: chkBoxResource, Track: chkBoxTrack, Images: chkBoxImage, Audio: chkBoxAudio, Video: chkBoxVideo,
        NewForReview: chkBoxNewForReview, FinalForReview: chkBoxFinalForReview, SampleExists: chkBoxSampleExists, SideArtistExists: chkBoxSideArtistExists, Download: chkBoxDownload,
        Streaming: chkBoxStreaming, All: chkBoxAll, AlaCarte: chkBoxAlaCarte, AdFunded: chkBoxAdFunded, NoRights: chkBoxNoRights,
        ConsentRequired: chkBoxConsentRequired, ReferToLegal: chkBoxReferToLegal, NoRestriction: chkBoxNoRestriction, Consult: chkBoxConsult, artistName: textArtistName,
        upc: textUpc, isrc: textIsrc, title: textTitle, linkedContract: textLinkedContract, Subscription: chkBoxSubscription, DuringAndAfterTerm: chkBoxDuringandAfterTerm, DuringHoldBackPeriod: chkBoxDuringHoldbackPeriod, DuringTerm: chkBoxDuringTerm, Title: textTitle, Upc: textUpc, Isrc: textIsrc, LinkedContract: textLinkedContract, ClearanceAdminCompany: clearanceCountryId, ReasonForReview: drpDwnReviewReason
    };

    /*DisablingTextBox onload*/
    $('#txtAreaLinkedContract').attr('readonly', 'readonly');

    $('#txtAreaLinkedContract').focusin(function () {
        $('#btnReviewFilter').focus();
    });

    /*To Handle the Scroll on page load*/
    var windowHeight = $(window).height();
    var scrollHeight = windowHeight - 39 - 127;
    //$('#divScrollPriorityWorkQueue').css('height', scrollHeight);

    //Scroll function handling on page resize
    $(window).resize(function () {
        windowHeight = $(window).height();
        scrollHeight = windowHeight - 39 - 127;
        if (scrollHeight > 700) {
            scrollHeight = 700;
        }
        // $('#divScrollPriorityWorkQueue').css('height', scrollHeight);
    });

    /*Autocomplete Functionality for clearance admin company*/
    var target = $("#txtClearanceAdminCompany");
    target.autocomplete({
        source: target.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#txtClearanceAdminCompany").val(ui.item.value);
            $("#clearanceCompSearchId").val(ui.item.addValue);
            $("#hiddenClearanceCountryId").val(ui.item.id);

        },
        response: function (event, ui) {

            var autocomplete = target.data("autocomplete");
            if (ui.content.length == 1) {
                autocomplete.selectedItem = ui.content[0];
            }
            if (autocomplete.selectedItem) {
                setTimeout(function () {
                    autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                    target.autocomplete('close');
                    $("#txtArtistName").focus();
                }, 200);
            }
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtClearanceAdminCompany").val(""); $("#hiddenClearanceCountryId").val('');

            }
            else if (ui.item != null && $("#txtClearanceAdminCompany").val() != ui.item.value) {
                $("#txtClearanceAdminCompany").val(""); $("#hiddenClearanceCountryId").val('');

            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#txtClearanceAdminCompany").val(ui.item.value);
                $("#hiddenClearanceCountryId").val(ui.item.id);
            }

        }
    });

    resizeHandler();


    /*Accordion Function*/
    $('#divRightsReviewFilterAccordion .head').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        setTimeout(function () { resizeAccordion(); }, 150); //TO Align Text Boxes on Click time out to handle asynchronous execution
        $(this).next().toggle();
        $(this).find('a').toggleClass('iconBottom');
        //  resizeGrid();
        GridScrollBar($('#hdnGridId').val());
        return false;
    }).next().hide();

    /*Search Contract Image Click Function*/
    $('#imgSearchReviewContract').click(function () {
        openSearchContractPopup();
    });

    disableCheckBox();

    $('#chkConsentRequired,#chkConsult').click(function () {
        if ($('#chkConsentRequired').is(':checked') || $('#chkConsult').is(':checked')) {
            $('#holdBackContainer').find('input:checkbox').each(function () {
                $(this).attr('disabled', false);
            });
        } else {
            disableCheckBox();
        }
    });

    /*To clear the filter Data on Reset Click function */
    $('#btnReviewFilterReset').click(function () {
        HideWarningSuccess();
        $('#divRightsReviewFilter input:checked').each(function () {
            $(this).removeAttr('Checked');
        });
        $('#divHiddenDigitalRestrictionContainer input:checked').each(function () {
            $(this).removeAttr('Checked');
        });
        $('#divDataField input:text,textarea').each(function () {
            $(this).val('');
        });
        $('#chkBoxArtistExact').removeAttr('Checked');
        $('#RightsMasterData_ReviewReason_Values').val('0');
        $('#txtAreaLinkedContractId').val("");
        $('#hiddenClearanceCountryId').val("");
        $('#txtAreaLinkedContract').css('background-color', '#FFFFFF');
        // $('#spanReviewFilter').hide();
    });


    /*Clear Filter image click function*/
    $('#clearReviewFilter').click(function (e) {
        e.stopImmediatePropagation();
        $('#btnReviewFilterReset').click();
        $('#spanReviewFilter').hide();
        getSearchParamsForResources();
        getFilteredGridData("");
    });

    /*Apply Filter on Enter Click */
    $('.filterBody').keyup(function (e) {
        if (e.keyCode == 13) {
            $('#btnReviewFilter').click();
        }
    });

    $('#divRightsReviewFilterAccordion').find('input:checkbox').change(function () {
        $('#btnReviewFilter').focus();
    });

    //    $('#divRightsReviewFilterAccordion').mouseup(function () {
    //        $('#btnReviewFilter').focus();
    //    });
    /*Filter Button click function*/
    $('#btnReviewFilter').click(function () {
        HideWarningSuccess();
        $('#divRightsReviewFilterAccordion .head').click();
        getSearchParamsForResources();
        if (reviewTabIndex != '2') {
            if (chkBoxResource == false && chkBoxTrack == false && chkBoxImage == false && chkBoxAudio == false && chkBoxVideo == false && chkBoxNewForReview == false && chkBoxFinalForReview == false && chkBoxSampleExists == false && chkBoxSideArtistExists == false && drpDwnReviewReason == "0" && textArtistName == '' && textUpc == '' && textIsrc == '' && textTitle == '' && textLinkedContract == '' && clearanceCountryId == 0) {
                ShowWarning(priorityWorkQueueMessages.filterSearch);
                return false;
            }
        }
        else {
            if (reviewTabIndex == '2') {
                if (chkBoxResource == false && chkBoxTrack == false && chkBoxImage == false && chkBoxAudio == false && chkBoxVideo == false && chkBoxNewForReview == false && chkBoxFinalForReview == false && chkBoxSampleExists == false && chkBoxSideArtistExists == false && drpDwnReviewReason == "0" && textArtistName == '' && textUpc == '' && textIsrc == '' && textTitle == '' && textLinkedContract == '' && clearanceCountryId == 0 && chkBoxDownload == false && chkBoxStreaming == false && chkBoxAll == false && chkBoxAlaCarte == false && chkBoxAdFunded == false && chkBoxNoRights == false && chkBoxConsentRequired == false && chkBoxReferToLegal == false && chkBoxNoRestriction == false && chkBoxConsult == false && chkBoxSubscription == false && chkBoxDuringandAfterTerm == false && chkBoxDuringHoldbackPeriod == false && chkBoxDuringTerm == false) {
                    ShowWarning(priorityWorkQueueMessages.filterSearch);
                    return false;
                }
            }
        }
        getFilteredGridData("");
        $('#spanReviewFilter').show();
    });


});

/*TO get Filter Parameters*/
function getSearchParamsForResources() {
    $('#divRightsReviewFilter ,#divDigitalRestrictionFilterAccordion').find('input:checkbox').each(function () {//TODO Digital CheckBoxes
   
        var id = $(this).prop('id');
        switch (id) {
            case 'chkResource':
                window.chkBoxResource = $(this).is(':checked');
                break;
            case 'chkTrack':
                window.chkBoxTrack = $(this).is(':checked');
                break;
            case 'chkImage':
                window.chkBoxImage = $(this).is(':checked');
                break;
            case 'chkAudio':
                window.chkBoxAudio = $(this).is(':checked');
                break;
            case 'chkVideo':
                window.chkBoxVideo = $(this).is(':checked');
                break;
            case 'chkNewForReview':
                window.chkBoxNewForReview = $(this).is(':checked');
                break;
            case 'chkFinalForReview':
                window.chkBoxFinalForReview = $(this).is(':checked');
                break;
            case 'chkSampleExists':
                window.chkBoxSampleExists = $(this).is(':checked');
                break;
            case 'chkSideArtistExists':
                window.chkBoxSideArtistExists = $(this).is(':checked');
                break;
            case 'chkDownload':
                if (reviewTabIndex == 2)/*Digital Restriction Tab is selected*/
                    window.chkBoxDownload = $(this).is(':checked');
                break;
            case 'chkStreaming':
                if (reviewTabIndex == 2)
                    window.chkBoxStreaming = $(this).is(':checked');
                break;
            case 'commercialModelAll':
                if (reviewTabIndex == 2)
                    window.chkBoxAll = $(this).is(':checked');
                break;
            case 'chkSubscription':
                if (reviewTabIndex == 2)
                    window.chkBoxSubscription = $(this).is(':checked');
                break;
            case 'chkAlaCarte':
                if (reviewTabIndex == 2)
                    window.chkBoxAlaCarte = $(this).is(':checked');
                break;
            case 'chkAdFunded':
                if (reviewTabIndex == 2)
                window.chkBoxAdFunded = $(this).is(':checked');
                break;
            case 'chkNoRights':
                if (reviewTabIndex == 2)
                    window.chkBoxNoRights = $(this).is(':checked');
                break;
            case 'chkConsentRequired':
                if (reviewTabIndex == 2)
                    window.chkBoxConsentRequired = $(this).is(':checked');
                break;
            case 'chkReferToLegal':
                if (reviewTabIndex == 2)
                    window.chkBoxReferToLegal = $(this).is(':checked');
                break;
            case 'chkNoRestriction':
                if (reviewTabIndex == 2)
                    window.chkBoxNoRestriction = $(this).is(':checked');
                break;
            case 'chkConsult':
                if (reviewTabIndex == 2)
                    window.chkBoxConsult = $(this).is(':checked');
                break;
            case 'chkDuringandAfterTerm':
                if (reviewTabIndex == 2)
                    window.chkBoxDuringandAfterTerm = $(this).is(':checked');
                break;
            case 'chkDuringHoldbackPeriod':
                if (reviewTabIndex == 2)
                    window.chkBoxDuringHoldbackPeriod = $(this).is(':checked');
                break;
            case 'chkDuringTerm':
                if (reviewTabIndex == 2)
                    window.chkBoxDuringTerm = $(this).is(':checked');
                break;
        }
    });
    window.drpDwnReviewReason=$('#RightsMasterData_ReviewReason_Values').val();
    window.textArtistName = $('#txtArtistName').val();
    window.textArtistName = window.textArtistName.replace(/'/g, "''"); 
    window.textUpc = $('#txtUpc').val();
    window.textIsrc = $('#txtIsrc').val();
    window.textTitle = $('#txtTitles').val().replace(/'/g, "''");
    window.textLinkedContract = $('#txtAreaLinkedContractId').val();
    window.textLinkedContract = window.textLinkedContract.replace(/-/g, ","); 
    window.clearanceCountryId = $('#hiddenClearanceCountryId').val();
    window.chkBoxArtistExact = $('#chkBoxArtistExact').is(":checked");
    if(clearanceCountryId=="") {
        window.clearanceCountryId = 0;
    }

    window.filterParams = { Resource: chkBoxResource, Track: chkBoxTrack, Images: chkBoxImage, Audio: chkBoxAudio, Video: chkBoxVideo,
        NewForReview: chkBoxNewForReview, FinalForReview: chkBoxFinalForReview, SampleExists: chkBoxSampleExists, SideArtistExists: chkBoxSideArtistExists, Download: chkBoxDownload,
        Streaming: chkBoxStreaming, All: chkBoxAll, AlaCarte: chkBoxAlaCarte, AdFunded: chkBoxAdFunded, NoRights: chkBoxNoRights,IsExactSearch: chkBoxArtistExact,
        ConsentRequired: chkBoxConsentRequired, ReferToLegal: chkBoxReferToLegal, NoRestriction: chkBoxNoRestriction, Consult: chkBoxConsult, ArtistName: textArtistName,
        upc: textUpc, isrc: textIsrc, title: textTitle, Subscription: chkBoxSubscription, DuringAndAfterTerm: chkBoxDuringandAfterTerm, DuringHoldBackPeriod: chkBoxDuringHoldbackPeriod, DuringTerm: chkBoxDuringTerm, Title: textTitle, Upc: textUpc, Isrc: textIsrc, LinkedContract: textLinkedContract, ClearanceAdminCompany: clearanceCountryId, ReasonForReview: drpDwnReviewReason
    };
}


/*To Open Search Contract Popup*/
function openSearchContractPopup() {
    var objReviewContractSearchPopupDialog = $('<div id="contractSearchPopup"> </div>')
   .html('<p>' + messageCommon.onLoading + '</p>')
    .dialog({
        autoOpen: false,
        modal: true,
        title: 'Search For Contract',
        show: 'clip',
        hide: 'clip',
        width: getPopupWidth(70, 750),
        height: getPopupHeight(80, 300, 60),
        minHeight: 300,
        minWidth: 750,
        position: [getXPosition(70, 0), getYPosition(80, 60)],
        resize: function () {
//            if ($("#contractSearchPopup").is(':visible')) {
//                if ($("#contractSearchPopup .jtable-main-container").is(':visible')) {
//                    var popupHeight = $('#contractSearchPopup').height();
//                    var filterHeight = (popupHeight * 35) / 100;
//                    var gridheight = (popupHeight * 65) / 100;
//                    $('#searchPopup').css('height', filterHeight);
//                    // var jtableheight = $(".jtable").height() + $(".jtable-left-area").height();
//                    //contractgrid
//                    // $("#contractSearchPopup .jtable-main-container").css('height', jtableheight - 800 + "px");
//                    $("#contractSearchPopup .jtable-main-container").css('height', gridheight-30);
//                }
            //            }
            if ($("#contractSearchPopup").is(':visible')) {
                if ($("#contractSearchPopup .jtable-main-container").is(':visible')) {
                    var DialogHgt = $("#contractSearchPopup").offset().top;
                    var TblConHgt = $("#contractSearchPopup .jtable-main-container").offset().top;
                    var TotDiaHgt = $("#contractSearchPopup").height();
                    var ReduceHgt = TblConHgt - DialogHgt;
                    var actualHeight = TotDiaHgt - ReduceHgt;
                    var jtableheight = $(".jtable").height() + $(".jtable-left-area").height();
                    if (jtableheight >= actualHeight)
                        $("#contractSearchPopup .jtable-main-container").css('height', actualHeight - (20 + $(".divRightsWorkQueueCntrctSearchBtns").height()) + "px");
                    else
                        $("#contractSearchPopup .jtable-main-container").css('height', jtableheight + 50 + "px");
                }
            }
        },
        close: function () {
            $(this).remove(); // ensures any form variables are reset.
        }
    });
    //  Load partial view
    objReviewContractSearchPopupDialog.load('/GCS/Contract/ContractSearchPopup', function () {

    });
    objReviewContractSearchPopupDialog.dialog('open');

}


function resizeHandler() {
    var wid = $("#ClearanceAdminCompany").width();
    $("#LinkedContract").css("width", wid - 17);
    $("#test").css("width", wid + 9);
    var w = $("#Titles").width();
    $("#Artist_FirstName").css("width", w - 90);
}

$(window).bind("resize", resizeHandler);


/*Styling for UPC and ISRC*/
function resizeAccordion() {
    $("#txtIsrc").css('width', '155px');
    $("#txtUpc").css('width', '155px');
    var wid1 = $('#txtArtistName').width();
    var wid2 = $("#txtTitles").width();
    var wid3 = $("#RightsMasterData_ReviewReason_Values").width();
    $("#txtArtistName").css('width', wid2 - 95);
    $("#txtClearanceAdminCompany").css('width', wid3);
    $(".upcContainer").css("margin-left", wid2 - 305);
    $("#txtAreaLinkedContract").css("width", wid3 - 20);
   // $("#divReviewFilterButtons").css("margin-left", wid3 - 100);
  
}

function disableCheckBox() {
    $('#holdBackContainer').find('input:checkbox').each(function () {
        $(this).attr('disabled', true);
    });
}

function resizeGrid() {
    var gridId=$('#hdnGridId').val();
    switch (gridId) {
        case "rightsAcquired":
            if ($('#' + gridId).is(':visible') && !$('#divDataField').is(':visible')) {
                rightsAcquiredGridScroll($('#hdnGridId').val());
            }
            break;
        case "digitalGrid":
            if ($('#' + gridId).is(':visible') && !$('#divDataField').is(':visible')) {
                DigRestSyncfusionGridScroll();
            }
            break;
        case "secondaryExp":
            if ($('#' + gridId).is(':visible') && !$('#divDataField').is(':visible')) {
                secondaryGridScroll();
            }
            break;
        case "preClearanceGrid":
            if ($('#' + gridId).is(':visible') && !$('#divDataField').is(':visible')) {
                preClearanceGridScroll();
            }
            break;
        default:
            if ($('#' + gridId).is(':visible') && !$('#divDataField').is(':visible')) {
                rightsAcquiredGridScroll($('#hdnGridId').val());
            }
            break;
    }
}
