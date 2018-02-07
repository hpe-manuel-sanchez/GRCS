$(document).ready(function () {
    $('#ddlRequestingComp option').each(function () {
        $(this).attr("title", $(this).text());
    });

    function handleProjectUnlock(e) {
        if (globalPostBackCheck == true) {
            globalPostBackCheck = false;
        }
        else {
            if ($("#Clr_Project_Id").val() != "" && $("#Clr_Project_Id").val() != "0") { unlockProject($("#Clr_Project_Id").val()); }
        }
    }
    window.onbeforeunload = handleProjectUnlock;

    TextAreaValidation("#txtDetails");
    $('#ddlRequestingComp').removeClass('input-validation-error');
    if ($("#tblFileUpload") != null && $("#tblFileUpload").length == 1 && $("#tblFileUpload")[0].rows.length > 0) {
        document.getElementById("tblFileUpload").style.display = 'inline';
    }
    ParentCall();

    $('#chk3rdParty').change(function () {
        // debugger

        if ($('#Div3rdPartyDetails').css("display") == "none")
            $('#Div3rdPartyDetails').slideDown(0)
        else
            $('#Div3rdPartyDetails').slideUp(0)

        $('#txtLicenseTerm').val('');
        $('#country1').val('');
        $('#name1').val('');
        $('#isaccode1').val('');
    });

    $('#name1').keydown(function (e) {
        if (e.keyCode == 13) {
            if ($('#name1').val() == "") {
                return false;
            }
            else {
                $("#btnSearchCompany").trigger('click');
                return false;
            }
        }
    });
    $('#country1').keydown(function (e) {
        if (e.keyCode == 13) {
            var index = $("#tabs").tabs('option', 'selected');
            if ($('#country1').val() == "") {
                return false;
            }
            else {
                $("#btnSearchCompany").trigger('click');
                return false;
            }
        }
    });
    $('#isaccode1').keydown(function (e) {
        if (e.keyCode == 13) {
            var index = $("#tabs").tabs('option', 'selected');
            if ($('#isaccode1').val() == "") {
                return false;
            }
            else {
                $("#btnSearchCompany").trigger('click');
                return false;
            }
        }
    });
    $('#btnSearchCompany').keydown(function (e) {
        e.preventDefault();
        if (e.keyCode == 13) {
            $("#btnSearchCompany").trigger('click');
            return false;
        }
    });
});

$(document).ready(function () {
    //$(".contentDv:first").toggle();

    $(".contentDv").show();
    $("h5").click(function () {
        //$(".contentDv").hide();
        var obj = $(this).closest("div").parent();
        $(obj).find('.contentDv').toggle();
        if ($(this).hasClass('rightArrow')) {
            $(this).removeClass('rightArrow');
            $(this).addClass('downArrow');
        } else {
            $(this).removeClass('downArrow');
            $(this).addClass('rightArrow');
        }
    });
    $("h6").click(function () {
        //$(".contentDv").hide();
        var obj = $(this).closest("div").parent();
        if ($(this).hasClass('rightArrowUPC')) {
            $(this).removeClass('rightArrowUPC');
            $(this).addClass('downArrowUPC');
        } else {
            $(this).removeClass('downArrowUPC');
            $(this).addClass('rightArrowUPC');
        }
    });
});