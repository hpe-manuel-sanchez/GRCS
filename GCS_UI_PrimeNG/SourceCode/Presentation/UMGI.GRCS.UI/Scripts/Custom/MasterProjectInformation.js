var trAdvertising = false;
var trFilm = false;
var trTrailers = false;
var trOthers = false;

function removeHighlightAdvertising() {
    $('#txtAdvertisedProducts').removeClass('input-validation-error');
    $('#Advertising_Frame').removeClass('input-validation-error');
    $('#txtAdvertisingVideo').removeClass('input-validation-error');
    $('#txtAdvertiseOtherComments').removeClass('input-validation-error');
}

function removeHighlightFilm() {
    $('#Film_Frame').removeClass('input-validation-error');
    $('#txtFilmVideo').removeClass('input-validation-error');
    $('#txtFilmOtherComments').removeClass('input-validation-error');
}

function removeHighlightTrailer() {
    $('#Trailers_Frame').removeClass('input-validation-error');
    $('#TrailerVideos').removeClass('input-validation-error');
    $('#TrailerOtherComments').removeClass('input-validation-error');
}

function toggleVisibility(id) {
    if ($("#" + id).css('visibility') == 'visible' || $("#" + id).css('visibility') == 'inherit') {
        $("#" + id).css('visibility', 'hidden');
    } else {
        $("#" + id).css('visibility', '');
    }
}

function txtDurationFromToChange(id, e) {
    var dateString = $(id).val();
    if (!/^\d{2}\/\d{2}\/\d{4}$/.test(dateString)) {
        $(id).val('');
        $("#txtDurationFrom").datepicker("option", {
            minDate: null,
            maxDate: null
        });

        $("#txtDurationTo").datepicker("option", {
            minDate: null,
            maxDate: null
        });

        $("#txtDurationFrom").css('vertical-align', 'top');
        $("#txtDurationFrom").css('margin-right', '10px');
        $("#txtDurationTo").css('vertical-align', 'top');
        $("#txtDurationTo").css('margin-right', '10px');
    }
    else {
        return false;
    }
}

function ValidateMandatoryFields(FieldName) {
    if (($('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == "0" || $('#' + FieldName).val() == "" || $('#' + FieldName).attr("value") == "")) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

function ValidateCheckbox(checkBoxName, ContainerID) {

    if ($('#' + checkBoxName).is(':checked')) {
        $('#' + ContainerID).removeClass('input-validation-error');
        if ((ContainerID == 'tdRequestType') || (ContainerID == 'tdRequestType2') || (ContainerID == 'tdRequestType3')) {
            $('#tdRequestType3').removeClass('input-validation-error');
            $('#tdRequestType').removeClass('input-validation-error');
            $('#tdRequestType2').removeClass('input-validation-error');
        }
        if ((ContainerID == 'chkAdver_Video_Other') || (ContainerID == 'chkAdver_TV_Radio') || (ContainerID == 'chkAdver_Cinema_Internet')) {

            $('#Advertising_Frame').removeClass('input-validation-error noBG');
        }
        if ((ContainerID == 'ChkTrailersTV_Internet') || (ContainerID == 'ChkTrailersCinema_Video') || (ContainerID == 'ChkTrailersOther')) {

            $('#Trailers_Frame').removeClass('input-validation-error noBG');
        }
        if ((ContainerID == 'chkFilmTV_Internet') || (ContainerID == 'chkFilmCinema_Trailor') || (ContainerID == 'chkFilmVideo_Other')) {

            $('#Film_Frame').removeClass('input-validation-error noBG');
        }
        if ((ContainerID == 'chkOthersTV_Radio') || (ContainerID == 'chkOthersCinema_Internet') || (ContainerID == 'chkOthersVideo_Other')) {

            $('#Other_Frame').removeClass('input-validation-error noBG');
        }
        return true;
    }
    else {
        if ((ContainerID == 'chkAdver_Video_Other') || (ContainerID == 'chkAdver_TV_Radio') || (ContainerID == 'chkAdver_Cinema_Internet')) {
            $('#Advertising_Frame').addClass('input-validation-error noBG');
        }
        else if ((ContainerID == 'ChkTrailersTV_Internet') || (ContainerID == 'ChkTrailersCinema_Video') || (ContainerID == 'ChkTrailersOther')) {
            $('#Trailers_Frame').addClass('input-validation-error noBG');
        }
        else if ((ContainerID == 'chkFilmTV_Internet') || (ContainerID == 'chkFilmCinema_Trailor') || (ContainerID == 'chkFilmVideo_Other')) {
            $('#Film_Frame').addClass('input-validation-error noBG');
        }
        else if ((ContainerID == 'chkOthersTV_Radio') || (ContainerID == 'chkOthersCinema_Internet') || (ContainerID == 'chkOthersVideo_Other')) {
            $('#Other_Frame').addClass('input-validation-error noBG');
        }
        else {
            $('#' + ContainerID).addClass('input-validation-error');
        }
        $(".frameDv").css("background", "");
        return false;
    }
}

function ValidateDivCheck(divId, controlToNotice) {
    if (($('#' + divId).html() != null)) {
        var divtext = $('#' + divId).html();
        if (divtext == "   ") {
            $('#' + controlToNotice).addClass('btn-validation-error');
            return false;
        }

        if (($('#' + divId).html() != "")) {
            $('#' + controlToNotice).removeClass('btn-validation-error');
            return true;
        }
        else {
            $('#' + controlToNotice).addClass('btn-validation-error');
            return false;
        }
    }
    else {
        $('#' + controlToNotice).addClass('btn-validation-error');
        return false;
    }
}

function SetAdvertisingDefaultValue() {
    if (!$("#chkAdvertising").is(":checked")) {
        $("#chkAdvertisingTV").attr('checked', false);
        $("#chkAdvertisingCinema").attr('checked', false);
        $("#chkAdvertisingRadio").attr('checked', false);
        $("#chkAdvertisingInternet").attr('checked', false);
        $("#chkAdvertisingVideo").attr('checked', false);
        $("#txtAdvertisingVideo").val("");
        $("#chkAdvertisingOther").attr('checked', false);
        $("#txtAdvertiseOtherComments").val("");
        $("#txtDurationFrom").val("");
        $("#txtDurationTo").val("");
        $("#txtAdvOptAddRights").val("");
        $("#txtAdvertisedProducts").val("");
        $("#txtNoOfSpots").val("");
        $("#divAdvertisingother")[0].style.visibility = "hidden";
        $("#divAdvertisingVideo")[0].style.visibility = "hidden";
        $("#divLblAdvertisingVideo")[0].style.visibility = "hidden";
    }
}

function SetTrailerDefaultValue() {
    if (!$("#chkTrailer").is(":checked")) {
        $("#chkTrailersTV").attr('checked', false);
        $("#chkTrailersInternet").attr('checked', false);
        $("#chkTrailersCinema").attr('checked', false);
        $("#chkTrailersVideo").attr('checked', false);
        $("#chkTrailersOther").attr('checked', false);
        $("#TrailerVideos").val("");
        $("#txtTrailerAddRights").val("");
        $("#TrailerOtherComments").val("");
        $("#divLblTrailersVideo")[0].style.visibility = "hidden";
        $("#txtTrailersVideo")[0].style.visibility = "hidden";
        $("#txtTrailersOther")[0].style.visibility = "hidden";
    }
}

function SetFilmDefaultValue() {
    if (!$("#chkFilm").is(":checked")) {
        $("#chkFilmTV").attr('checked', false);
        $("#chkFilmInternet").attr('checked', false);
        $("#chkFilmCinema").attr('checked', false);
        $("#chkFilmTrailer").attr('checked', false);
        $("#chkFilmVideo").attr('checked', false);
        $("#chkFilmOther").attr('checked', false);
        $("#txtFilmVideo").val("");
        $("#txtFilmOtherComments").val("");
        $("#txtFilmAddRights").val("");
        $("#divlblFilmVideo")[0].style.visibility = "hidden";
        $("#divtxtFilmVideo")[0].style.visibility = "hidden";
        $("#divtxtFilmOther")[0].style.visibility = "hidden";
    }
}

function SetOtherDefaultValue() {
    if (!$("#chkOther").is(":checked")) {
        $("#chkOthersTV").attr('checked', false);
        $("#chkOthersRadio").attr('checked', false);
        $("#chkOthersCinema").attr('checked', false);
        $("#chkOthersInternet").attr('checked', false);
        $("#chkOthersVideo").attr('checked', false);
        $("#chkOthersOther").attr('checked', false);
        $("#OthersOtherVideo").val("");
        $("#OthersOtherComments").val("");
        $("#txtOthersAddRights").val("");
        $("#divLblOtherVideo")[0].style.visibility = "hidden";
        $("#txtOtherVideo")[0].style.visibility = "hidden";
        $("#txtOtherOther")[0].style.visibility = "hidden";
    }
}

function SetDefaultValue() {
    if (parseInt($("#hdnclrProjectId").val()) == 0) {
        SetOtherDefaultValue();
        SetAdvertisingDefaultValue();
        SetTrailerDefaultValue();
        SetFilmDefaultValue();
    }
}

function unlockProject(projectid) {
    var values =
                {
                    "projectId": projectid
                }
    $.ajax({
        url: '/GCS/ClearanceProject/UnlockProject',
        type: 'POST',
        async: false,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values)
    });
}

function BeforeUploadFileOnClient() {
    isSelect = true;

    var fileUpload = $("#tblFileUpload");
    if (fileUpload != null && fileUpload.length == 1 && fileUpload[0].rows.length >= 10) {
        return false;
    }
}

function UploadFileOnClient(obj) {
    hideHeaderMessages();

    if (isSelect == true) {

        isSelect = false;

        var filepath = obj.value;
        var fileName = filepath.substr((filepath.lastIndexOf('\\') + 1));
        var id = obj.id;

        var fileUpload = $("#tblFileUpload");
        if (fileUpload != null && fileUpload.length == 1 && fileUpload[0].rows.length >= 10) {

            $('#hdnRemoveFile').val() == "" ? $('#hdnRemoveFile').val(fileName) : $('#hdnRemoveFile').val($('#hdnRemoveFile').val() + "," + fileName);
            $("#divErrorMessage").html(uploadMessage).show();
            $('#divErrorMessage').addClass('warning');
        }
        else {
            createDynamicTable(fileUpload, fileName, id);
        }

        $("#" + id).hide();

        var fileUploadLength = $("#tblFileUpload")[0].rows.length;
        if (fileUploadLength <= 10) {

            var newId = GCS.utilities.functions.getUniqueId()
            var div = document.createElement('DIV');
            div.innerHTML = '<input id="fileUpload_' + newId + '" name = "fileUpload_11" type="file" class="width-file"  onclick="BeforeUploadFileOnClient();" onchange="UploadFileOnClient(this);" />';
            $("#fileInput11").show();
            $("#fileInput11").append(div);
            $("#btnUpload").show();
        }

        $("#tblFileUpload").show();
    }
}

function createDynamicTable(tbody, displayValue, id) {
    if (tbody != null || tbody.length > 0) {
        var trow = $("<tr>");

        $("<td>").append(displayValue).data("col", 1).appendTo(trow);
        $("<td>").append("<a href='#' class = 'RemoveFile'  onClick='RemoveFileOnLocal(\"" + displayValue + "~" + id + "\")' id='RemoveFile" + id + "'>" + "Remove</a>").data("col", 2).appendTo(trow);

        trow.appendTo(tbody);
    }
}

function RemoveFileOnLocal(rowValue) {
    hideHeaderMessages();

    var substr = rowValue.split('~');
    var id = substr[1];
    var name = substr[0];

    $('#hdnRemoveFile').val() == "" ? $('#hdnRemoveFile').val(name) : $('#hdnRemoveFile').val($('#hdnRemoveFile').val() + "," + name);

    $("#RemoveFile" + id).closest("tr").remove();
    $("#" + id).closest("div").remove();
}

function RemoveFile(rowValue) {
    hideHeaderMessages();

    var substr = rowValue.split('~');
    $("#hdnRemoveFromServer" + substr[2]).closest("tr").remove();

    var values = {
        "fileName": rowValue,
        "clrProjectId": $('#hdnclrProjectId').val()
    };

    $.ajax({
        type: 'POST',
        url: '/Gcs/ClearanceProject/RemoveFile',
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values),
    });
}

function ResetRequestTypes() {
    if ($("#chkAdvertising").is(":checked")) {
        SetTrailerDefaultValue();
        SetFilmDefaultValue();
    }

    if ($("#chkTrailer").is(":checked")) {
        SetFilmDefaultValue();
        SetAdvertisingDefaultValue();
    }

    if ($("#chkFilm").is(":checked")) {
        SetAdvertisingDefaultValue();
        SetTrailerDefaultValue();
    }
    if ($("#chkOther").is(":checked") == false) {
        SetOtherDefaultValue();
    }
}

function ResetContainerDiv() {
    $('#Advertising_Frame').removeClass('input-validation-error noBG');
}

function ResetContainerDivFilm() {
    $('#Film_Frame').removeClass('input-validation-error noBG');
}

function ResetContainerDivTrailers() {
    $('#Trailers_Frame').removeClass('input-validation-error noBG');
}

function ResetContainerDivOthers() {
    $('#Other_Frame').removeClass('input-validation-error noBG');
}

function displayClientName() {
    $('#txtClientName').attr('title', $('#txtClientName').val());
}

function displayClientWebsite() {
    document.getElementById('MasterProjectDetails_ClientWebsite').title = $('#MasterProjectDetails_ClientWebsite').val();
}

function displayLicenseTerm() {
    if (($('#hdnMasterProjectStatusId').val() != "2") && ($('#hdnMasterProjectStatusId').val() != "5") && ($('#hdnMasterProjectStatusId').val() != "6")) {
        $('#txtLicenseTerm').attr('title', $('#txtLicenseTerm').val());
    }
}

function uploadFile() {
    hideHeaderMessages();

    globalPostBackCheck = true;
    var form = window.parent.document.forms[0];

    if (checkTotalSize(form.id)) {
        form.submit();
        $('#loadingDivPA').show();
    }
    else {
        $("#divErrorMsg").html(uploadDoumentMsg).hide();
        $("#divErrorMsg").show();
    }
}

function checkTotalSize(formId, limitTotalSize) {
    if (limitTotalSize === undefined) {
        var iisLimitSize = 4000000;
        limitTotalSize = iisLimitSize;
    }

    var navigatorProperties = GCS.utilities.functions.getNavigatorProperties();

    var isPossibleSubmit = true;
    var totalSizeFiles = 0;

    $('#' + formId + ' input[type="file"]').each(function () {
        var inputId = this.id;
        totalSizeFiles = totalSizeFiles + GCS.utilities.functions.getFileSize(inputId);
    });

    if (totalSizeFiles > limitTotalSize) {
        isPossibleSubmit = false;
    }

    return isPossibleSubmit;
}

function hideHeaderMessages() {
    $("#divErrorMessage").html('').hide();
    $("#divErrorMessage1").html('').hide();
    $("#divErrorMsg").html('').hide();
}

$(document).ready(function () {

    $("#txtDurationFrom").datepicker({
        dateFormat: "dd/m/yy",
        showOn: "button",
        buttonImage: "/Gcs/Images/GCS_Calender_Icon_img.png",
        buttonImageOnly: true,
        height: '12px',

        onSelect: function (selected) {
            $("#txtDurationTo").datepicker("option", "minDate", selected);
            $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "margin-left": "10px" });
        }
    });
    $("#txtDurationTo").datepicker({
        dateFormat: "dd/m/yy",
        showOn: "button",
        buttonImage: "/Gcs/Images/GCS_Calender_Icon_img.png",
        buttonImageOnly: true,
        height: '12px',

        onSelect: function (selected) {
            $("#txtDurationFrom").datepicker("option", "maxDate", selected);
            $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "margin-left": "10px" });
        }
    });

    $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'middle', "margin-left": '10px' });

    $('#tabs').tabs({
        select: function (event, ui) {
            var selected = ui.panel.id;
            switch (selected) {
                case 'tabs-1':
                    $('#masterProjInfoHistoryLink').show();
                    break;
                case 'tabs-2':
                    $('#masterProjInfoHistoryLink').hide();
                    break;
                case 'tabs-3':
                    $('#masterProjInfoHistoryLink').hide();
                    break;
                default:
                    break;
            }
        }
    });

    $.watermark.options.className = 'watermark';
    if ($("#chkAdvertising").is(":checked")) {
        $('#divRequestTypeSection').removeClass('input-validation-error');
        $("#trAdvertising").show();
    }
    if ($("#chkFilm").is(":checked")) {
        $('#divRequestTypeSection').removeClass('input-validation-error');
        $("#trFilm").show();
    }
    if ($("#chkTrailer").is(":checked")) {
        $('#divRequestTypeSection').removeClass('input-validation-error');
        $("#trTrailers").show();
    }
    if ($("#chkOther").is(":checked")) {
        $('#divRequestTypeSection').removeClass('input-validation-error');
        $("#trOthers").show();
    }
    if ($("#chkOS").is(":checked")) {
        $("#tdTxtboxOneStop").show();
    }
    if ($("#chkAdvertisingVideo").is(":checked")) {
        $("#adv-video-audio").show();
        $("#adv-video-audio").css('visibility', '');
        $("#divAdvertisingVideo").show();
        $("#divAdvertisingVideo").css('visibility', '');
        $("#divLblAdvertisingVideo").show();
        $("#divLblAdvertisingVideo").css('visibility', '');
    }
    if ($("#chkAdvertisingOther").is(":checked")) {
        $("#adv-video-audio").show();
        $("#adv-video-audio").css('visibility', '');
        $("#divAdvertisingother").show();
        $("#divAdvertisingother").css('visibility', '');
    }
    if ($("#chkFilmVideo").is(":checked")) {
        $("#divtxtFilmVideo").show();
        $("#divtxtFilmVideo").css('visibility', '');
        $("#divlblFilmVideo").show();
        $("#divlblFilmVideo").css('visibility', '');
    }
    if ($("#chkFilmOther").is(":checked")) {
        $("#divtxtFilmOther").show();
        $("#divtxtFilmOther").css('visibility', '');
    }
    if ($("#chkTrailersVideo").is(":checked")) {
        $("#txtTrailersVideo").show();
        $("#txtTrailersVideo").css('visibility', '');
        $("#txtTrailersVideo").show();
        $("#divLblTrailersVideo").show();
        $("#divLblTrailersVideo").css('visibility', '');
    }
    if ($("#chkTrailersOther").is(":checked")) {
        $("#txtTrailersOther").show();
        $("#txtTrailersOther").css('visibility', '');
    }
    if ($("#chkOthersVideo").is(":checked")) {
        $("#txtOtherVideo").show();
        $("#divLblOtherVideo").show();
        $("#divLblOtherVideo").css('visibility', '');
        $("#txtOtherVideo").css('visibility', '');
    }
    if ($("#chkOthersOther").is(":checked")) {
        $("#txtOtherOther").show();
        $("#txtOtherOther").css('visibility', '');
    }

    jQuery("#OneStopReason").watermark("One Stop Reason");
    jQuery("#txtAdvertiseOtherComments").watermark("Other Comments");
    jQuery("#txtFilmOtherComments").watermark("Other Comments");
    jQuery("#TrailerOtherComments").watermark("Other Comments");
    jQuery("#OthersOtherComments").watermark("Other Comments");

    $("#imgHide").on("click", function () {
        $("#tblProject").slideToggle("fast");
        var imgSrc = $("#imgHide").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgHide").attr("src", "/Gcs/Images/left.png");
            $("#imgHide").attr("title", "Expand");
        }
        else {
            $("#imgHide").attr("src", "/Gcs/Images/Down.png");
            $("#imgHide").attr("title", "Collapse");
        }
        return false;
    });

    $("#imgHideRequestType").on("click", function () {
        $("#trRequestType").slideToggle("fast");
        $("#trAdvertising").slideToggle("fast");
        $("#trFilm").slideToggle("fast");
        $("#trTrailers").slideToggle("fast");
        $("#trOthers").slideToggle("fast");

        //Advertising
        if ($("#chkAdvertising").is(":checked")) {
            $("#trAdvertising").show();
            $('#divRequestTypeSection').removeClass('input-validation-error');
        }
        else {
            $("#trAdvertising").hide();
        }

        //Film
        if ($("#chkFilm").is(":checked")) {
            $("#trFilm").show();
            $('#divRequestTypeSection').removeClass('input-validation-error');
        }
        else {
            $("#trFilm").hide();
        }

        //Trailer
        if ($("#chkTrailer").is(":checked")) {
            $("#trTrailers").show();
            $('#divRequestTypeSection').removeClass('input-validation-error');
        }
        else {
            $("#trTrailers").hide();
        }

        //Other
        if ($("#chkOther").is(":checked")) {
            $("#trOthers").show();
            $('#divRequestTypeSection').removeClass('input-validation-error');
        }
        else {
            $("#trOthers").hide();
        }
    });

    $("#chkAdvertising").on("click", function () {
        $('#divRequestTypeSection').removeClass('input-validation-error');
        $("#trAdvertising").fadeToggle("fast");
        $("#chkFilm").attr('checked', false);
        removeHighlightFilm()
        $("#chkTrailer").attr('checked', false);
        removeHighlightTrailer();
        $("#trFilm").hide();
        $("#trTrailers").hide();
        if (parseInt($("#hdnclrProjectId").val()) == 0) {
            $("#adv-video-audio").hide();
        }
        trAdvertising = $("#trAdvertising").is(":visible");
        trFilm = $("#trFilm").is(":visible");
        trTrailers = $("#trTrailers").is(":visible");
        trOthers = $("#trOthers").is(":visible");
        SetDefaultValue();

        if (!$("#chkAdvertising").is(":checked")) {
            removeHighlightAdvertising();
        }
    });

    $("#chkFilm").on("click", function () {
        $('#divRequestTypeSection').removeClass('input-validation-error');

        $("#trFilm").fadeToggle("fast");

        $("#chkAdvertising").attr('checked', false);
        removeHighlightAdvertising();
        $("#chkTrailer").attr('checked', false);
        removeHighlightTrailer()
        $("#trAdvertising").hide();
        $("#trTrailers").hide();
        if (parseInt($("#hdnclrProjectId").val()) == 0) {
            $("#Film-video-Other").hide();
        }

        trAdvertising = $("#trAdvertising").is(":visible");
        trFilm = $("#trFilm").is(":visible");
        trTrailers = $("#trTrailers").is(":visible");
        trOthers = $("#trOthers").is(":visible");
        SetDefaultValue();

        if (!$("#chkFilm").is(":checked")) {
            removeHighlightFilm();
        }
    });

    $("#chkTrailer").on("click", function () {
        $('#divRequestTypeSection').removeClass('input-validation-error');
        $("#trTrailers").fadeToggle("fast");
        $("#chkFilm").attr('checked', false);
        removeHighlightFilm();
        $("#chkAdvertising").attr('checked', false);
        removeHighlightAdvertising();
        $("#trFilm").hide();
        $("#trAdvertising").hide();
        if (parseInt($("#hdnclrProjectId").val()) == 0) {
            $("#Trailer-video-audio").hide();
        }
        trAdvertising = $("#trAdvertising").is(":visible");
        trFilm = $("#trFilm").is(":visible");
        trTrailers = $("#trTrailers").is(":visible");
        trOthers = $("#trOthers").is(":visible");
        SetDefaultValue();

        //For removing data when the checkbox is unchecked.
        if (!$("#chkTrailer").is(":checked")) {
            removeHighlightTrailer();
        }
    });

    $("#chkOther").on("click", function () {
        $('#divRequestTypeSection').removeClass('input-validation-error');
        $("#trOthers").fadeToggle("fast");

        trOthers = $("#trOthers").is(":visible");
        if (parseInt($("#hdnclrProjectId").val()) == 0) {
            $("#Others-video-Other").hide();
        }
        trAdvertising = $("#trAdvertising").is(":visible");
        trFilm = $("#trFilm").is(":visible");
        trTrailers = $("#trTrailers").is(":visible");
        trOthers = $("#trOthers").is(":visible");
        SetDefaultValue();

        //For removing data when the checkbox is unchecked.
        if (!$("#chkOther").is(":checked")) {
            $('#Other_Frame').removeClass('input-validation-error');
            $('#OthersOtherVideo').removeClass('input-validation-error');
            $('#OthersOtherComments').removeClass('input-validation-error');
        }
    });

    $("#chkOS").on("click", function () {
        $("#tdTxtboxOneStop").fadeToggle("fast");
    });

    $("#chkAdvertisingVideo").on("click", function () {
        toggleVisibility('divAdvertisingVideo');
        toggleVisibility('divLblAdvertisingVideo');
        if ($("#chkAdvertisingVideo").is(":checked")) {
            $("#adv-video-audio").show();
        }
        else {
            if (($("#chkAdvertisingOther").is(":checked"))) {
                $("#adv-video-audio").show();
            }
            else {
                $("#adv-video-audio").hide();
            }
        }
    });

    $("#chkAdvertisingOther").on("click", function () {
        if ($("#chkAdvertisingOther").is(":checked")) {
            $("#adv-video-audio").show();
        }
        else {
            if (($("#chkAdvertisingVideo").is(":checked"))) {
                $("#adv-video-audio").show();
            }
            else {
                $("#adv-video-audio").hide();
            }
        }
        toggleVisibility('divAdvertisingother');
    });

    $("#chkFilmVideo").on("click", function () {
        toggleVisibility('divtxtFilmVideo');
        toggleVisibility('divlblFilmVideo');
        if ($("#chkFilmVideo").is(":checked")) {
            $("#Film-video-Other").show();
        }
        else {
            if (($("#chkFilmOther").is(":checked"))) {
                $("#Film-video-Other").show();
            }
            else {
                $("#Film-video-Other").hide();
            }
        }
    });

    $("#chkFilmOther").on("click", function () {
        toggleVisibility('divtxtFilmOther');

        if ($("#chkFilmOther").is(":checked")) {
            $("#Film-video-Other").show();
        }
        else {
            if (($("#chkFilmVideo").is(":checked"))) {
                $("#Film-video-Other").show();
            }
            else {
                $("#Film-video-Other").hide();
            }
        }
    });

    $("#chkTrailersVideo").on("click", function () {
        toggleVisibility('txtTrailersVideo');
        toggleVisibility('divLblTrailersVideo');

        if ($("#chkTrailersVideo").is(":checked")) {
            $("#Trailer-video-audio").show();
        }
        else {
            if (($("#chkTrailersOther").is(":checked"))) {
                $("#Trailer-video-audio").show();
            }
            else {
                $("#Trailer-video-audio").hide();
            }
        }
    });

    $("#chkTrailersOther").on("click", function () {
        toggleVisibility('txtTrailersOther');

        if ($("#chkTrailersOther").is(":checked")) {
            $("#Trailer-video-audio").show();
        }
        else {
            if (($("#chkTrailersVideo").is(":checked"))) {
                $("#Trailer-video-audio").show();
            }
            else {
                $("#Trailer-video-audio").hide();
            }
        }
    });

    $("#chkOthersVideo").on("click", function () {
        toggleVisibility('txtOtherVideo');
        toggleVisibility('divLblOtherVideo');
        if ($("#chkOthersVideo").is(":checked")) {
            $("#Others-video-Other").show();
        }
        else {
            if (($("#chkOthersOther").is(":checked"))) {
                $("#Others-video-Other").show();
            }
            else {
                $("#Others-video-Other").hide();
            }
        }
    });

    $("#chkOthersOther").on("click", function () {

        toggleVisibility('txtOtherOther');
        if ($("#chkOthersOther").is(":checked")) {
            $("#Others-video-Other").show();
        }
        else {
            if (($("#chkOthersVideo").is(":checked"))) {
                $("#Others-video-Other").show();
            }
            else {
                $("#Others-video-Other").hide();
            }
        }
    });

    $("#btnSubmit").on("click", function () {
        if ($('#loadingDivPA').is(':visible')) {
            return;
        }
        manageWrapper();
        if (!onSubmitResource())
            return false;
        ResetRequestTypes();
        $("#divMessage").hide();

        $("#divErrorMessage1").hide();
        var IsValid = true;
        var NotValid = false;
        var focusFound = false;
        var focusField = '';
        if (!ValidateMandatoryFields('txtProjectTitle')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtProjectTitle'; focusFound = true; } } //return false;}
        if ($('#chkOS').is(':checked')) {
            if (!ValidateMandatoryFields('OneStopReason')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'OneStopReason'; focusFound = true; } } //return false;}
        }

        if (!ValidateMandatoryFields('txtClientName')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtClientName'; focusFound = true; } } //return false;}
        if (!ValidateMandatoryFields('txtLicenseTerm')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtLicenseTerm'; focusFound = true; } } //return false;}

        if (!(ValidateCheckbox('chkAdvertising', 'divRequestTypeSection') || ValidateCheckbox('chkTrailer', 'divRequestTypeSection') || ValidateCheckbox('chkFilm', 'divRequestTypeSection') || ValidateCheckbox('chkOther', 'divRequestTypeSection'))) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'divRequestTypeSection'; focusFound = true; } } //return false;}

        if ($('#chkAdvertising').is(':checked')) {
            $('#divRequestTypeSection').removeClass('input-validation-error');
            if (!ValidateMandatoryFields('txtAdvertisedProducts')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtAdvertisedProducts'; focusFound = true; } } //return false;}
            if (!(ValidateCheckbox('chkAdvertisingVideo', 'chkAdver_Video_Other') || ValidateCheckbox('chkAdvertisingOther', 'chkAdver_Video_Other') || ValidateCheckbox('chkAdvertisingRadio', 'chkAdver_TV_Radio') || ValidateCheckbox('chkAdvertisingInternet', 'chkAdver_Cinema_Internet') || ValidateCheckbox('chkAdvertisingCinema', 'chkAdver_Cinema_Internet') || ValidateCheckbox('chkAdvertisingTV', 'chkAdver_TV_Radio'))) { IsValid = false; NotValid = true; } //return false;}
            if ($('#chkAdvertisingOther').is(':checked')) {
                if (!ValidateMandatoryFields('txtAdvertiseOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtAdvertiseOtherComments'; focusFound = true; } } //return false;}
            }
        }
        if ($('#chkFilm').is(':checked')) {
            if (!(ValidateCheckbox('chkFilmTV', 'chkFilmTV_Internet') || ValidateCheckbox('chkFilmInternet', 'chkFilmTV_Internet') || ValidateCheckbox('chkFilmCinema', 'chkFilmCinema_Trailor') || ValidateCheckbox('chkFilmTrailer', 'chkFilmCinema_Trailor') || ValidateCheckbox('chkFilmVideo', 'chkFilmVideo_Other') || ValidateCheckbox('chkFilmOther', 'chkFilmVideo_Other'))) { IsValid = false; NotValid = true; } //return false;}
            if ($('#chkFilmOther').is(':checked')) {
                if (!ValidateMandatoryFields('txtFilmOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtFilmOtherComments'; focusFound = true; } } //return false;}
            }
        }
        if ($('#chkTrailer').is(':checked')) {
            if (!(ValidateCheckbox('chkTrailersTV', 'ChkTrailersTV_Internet') || ValidateCheckbox('chkTrailersInternet', 'ChkTrailersTV_Internet') || ValidateCheckbox('chkTrailersCinema', 'ChkTrailersCinema_Video') || ValidateCheckbox('chkTrailersOther', 'ChkTrailersOther') || ValidateCheckbox('chkTrailersVideo', 'ChkTrailersCinema_Video'))) { IsValid = false; NotValid = true; } //return false;}
            if ($('#chkTrailersOther').is(':checked')) {
                if (!ValidateMandatoryFields('TrailerOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'TrailerOtherComments'; focusFound = true; } } //return false;}
            }
        }
        if ($('#chkOther').is(':checked')) {
            if (!(ValidateCheckbox('chkOthersTV', 'chkOthersTV_Radio') || ValidateCheckbox('chkOthersRadio', 'chkOthersTV_Radio') || ValidateCheckbox('chkOthersCinema', 'chkOthersCinema_Internet') || ValidateCheckbox('chkOthersInternet', 'chkOthersCinema_Internet') || ValidateCheckbox('chkOthersVideo', 'chkOthersVideo_Other') || ValidateCheckbox('chkOthersOther', 'chkOthersVideo_Other'))) { IsValid = false; NotValid = true; } //return false;}
            if ($('#chkOthersOther').is(':checked')) {
                if (!ValidateMandatoryFields('OthersOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'OthersOtherComments'; focusFound = true; } } //return false;}
            }
        }
        if (!ValidateDivCheck('selectedCountries_1', 'btnManageTerritories')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'selectedCountries_1'; focusFound = true; } } //return false;}

        if (IsValid == false && NotValid == true) {
            $("#divErrorMessage").html(mandatoryFields).show();
            $("#divErrorMessage").addClass('input-validation-error');
            $('#' + focusField).focus();
            $("#tabs").tabs({ selected: 0 });
            return false;
        }
        else if ($('#hdnArchiveFlag0').val() == null) {
            $("#tabs").tabs({ selected: 1 });
            $("#divErrorMessage").html(mandatoryResourceFields).show();
            $("#divMessage").hide();

            return false;
        }

        else if ($('#hdnArchiveFlag0').val() != null) {
            $("#tabs").tabs({ selected: 1 });
            var Count = $('#hdnResourceListCount').val();
            var CountResourcesArchived = $('#hdnResourceListCount').val();
            for (i = 0; i <= CountResourcesArchived - 1; i++) {
                if ($('#hdnArchiveFlag' + i).val() == "Y") {
                    Count = Count - 1;
                }
            }
            if (Count <= 0) {
                $("#tabs").tabs({ selected: 1 });
                $("#divErrorMessage").html(mandatoryResourceFields).show();
                $("#divMessage").hide();

                return false;
            }
            var isValidated = true;
            for (i = 0; i <= Count - 1; i++) {
                if ($('#txtSuggestedFee' + i).val() == "") {
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtSuggestedFee' + i).addClass('btn-validation-error');
                    $("#divMessage").hide();
                    isValidated = false;
                }
                else {
                    $('#txtSuggestedFee' + i).removeClass('btn-validation-error');
                }
                if ($('#txtExcerptTime' + i).val() == "") {
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtExcerptTime' + i).addClass('btn-validation-error');
                    $("#divMessage").hide();
                    isValidated = false;
                }
                else {
                    $('#txtExcerptTime' + i).removeClass('btn-validation-error');
                }

                if ($('#BtnManagerArtistFreeHand' + i) != null) {
                    if ($('#divArtistName' + i).text() == "") {
                        $('#BtnManagerArtistFreeHand' + i).addClass('btn-validation-error');
                        isValidated = false;
                    }
                }
                else {
                    $('#BtnManagerArtistFreeHand' + i).removeClass('btn-validation-error');
                }
            }
            if (isValidated) {
                $("#divErrorMessage").html(mandatoryFields).hide();
                $("#divErrorMessage").text("");
                $('#btnSubmit1').click()
            }
            return isValidated;
        }

        else {
            $("#divErrorMessage").text("");
            $('#btnSubmit1').click()
            return true;
        }
    });

    $("#btnUpload").on("click", uploadFile);
});