$(document).ready(function () {
    // for requesting company hover
    $('#ddlRequestingComp option').each(function () {
        $(this).attr("title", $(this).text());
    });

    function handleProjectUnlock(e) {
        // debugger;

        if (globalPostBackCheck == true) {
            globalPostBackCheck = false;
        }
        else {
            if ($("#hdnclrProjectId").val() != "" && $("#hdnclrProjectId").val() != "0") { unlockProject($("#hdnclrProjectId").val()); }
        }
    }
    window.onbeforeunload = handleProjectUnlock;

    //bug id 718
    $('#ddlRequestingComp').removeClass('input-validation-error');

    if ($("#tblFileUpload") != null && $("#tblFileUpload").length == 1 && $("#tblFileUpload")[0].rows.length > 0) {
        document.getElementById("tblFileUpload").style.display = 'inline';
    }

    ParentCall();

    Onchange("#chkOS", "#OneStopReason");

    //Advertisement
    Onchange('#chkAdvertisingVideo', '#txtAdvertisingVideo');
    Onchange('#chkAdvertisingOther', '#txtAdvertiseOtherComments');

    //other
    Onchange('#chkOthersOther', '#OthersOtherComments');
    Onchange('#chkOthersVideo', '#OthersOtherVideo');

    //Trailer
    Onchange('#chkTrailersVideo', '#TrailerVideos');
    Onchange('#chkTrailersOther', '#TrailerOtherComments');

    // Film

    Onchange('#chkFilmVideo', '#txtFilmVideo');
    Onchange('#chkFilmOther', '#txtFilmOtherComments');

    TextAreaValidation();
});

$(document).ready(function () {
    //$(".contentDv:first").toggle();
    $(".contentDv").show();
    $("h5").click(function () {
        //$(".contentDv").hide();
        var obj = $(this).closest("div").parent();
        $(obj).find('.contentDv').toggle();
        if ($(this)[0].className.indexOf('rightArrow') != -1) {
            $(this).removeClass('rightArrow');
            $(this).addClass('downArrow');
        }
        else {
            $(this).removeClass('downArrow');
            $(this).addClass('rightArrow');
        }
    });

    $('#txtClientName, #MasterProjectDetails_ClientWebsite, #txtLicenseTerm').click(function (event) {
        event.stopPropagation();
        return false;
    });
    $('#chkFilm').click(function () {
        if ($('#chkFilm').is(':checked')) {
            $(".vbar").css("display", "none");
        }
        else {
            $(".vbar").css("display", "bloack");
        }
    });
    $('#chkTrailer').click(function () {
        if ($('#chkTrailer').is(':checked')) {
            $(".vbar").css("display", "block");
        }
    });
});

function Onchange(checkBoxid, textAreaId) {
    // register onchange()
    $(checkBoxid).change(function () {
        // debugger
        $(textAreaId).val('');
    });
}

function TextAreaValidation(id) {
    $('textarea').keydown(function (event) {
        if (event.keyCode == 13) {
            event.stopPropagation();
        }
        else {
            if (this.value.length >= 1073741824) {
                return false;
            }
        }
    });
}

function applyCustomTheam() {
    var docHeight = $(window).height();
    var headerHeight = 60;
    var footerHeight = 45;
    var errMessageHeight = $("#divMessage").height() + 20;

    var mainContainerHeight = docHeight - headerHeight - footerHeight;
    $(".mainContainer").css("height", mainContainerHeight + "px");
    $(".mainContainer").css("overflow", "visible");

    $("#wrapper").css("height", mainContainerHeight - errMessageHeight - 95 + "px");
}