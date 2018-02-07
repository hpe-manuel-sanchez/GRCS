var flag = true;

function CheckDuplicateControls(viewData, ReleaseCount, ReleaseCountExact) {
    for (var iCountRel = 0; iCountRel < ReleaseCountExact; iCountRel++) {
        $("#tblReleaseNew" + iCountRel + " div").each(function () {

            var btnId;
            var rowId

            var id = $(this).attr('id');

            if (id != null) {
                if (id.indexOf("trPackageRow") >= 0) {
                    if (viewData != "") {
                        btnId = viewData.toString().substring(13, viewData.length);
                    }
                    else {
                        btnId = 0;
                    }

                    rowId = id.toString().substring(12, id.length);

                    if ($("#ddlPackage_" + rowId).val() == 2) {
                        $('#tdNoOfComp' + rowId).show();
                        $('#tdddlComp' + rowId).show();
                        $('#tdExistRel' + rowId).show();
                        $('#tdddlExistRel' + rowId).show();

                        if ($("#ddlExistingRelease_" + rowId).val() == 1) {
                            $('#tdViewExisting' + rowId).hide();
                        }
                        else {
                            $('#txtAreaComments_' + rowId).watermark(watermarkComments);
                            $('#tdViewExisting' + rowId).show();
                        }
                    }
                    else {
                        $('#tdNoOfComp' + rowId).hide();
                        $('#tdddlComp' + rowId).hide();
                        $('#tdExistRel' + rowId).hide();
                        $('#tdddlExistRel' + rowId).hide();
                        $('#tdViewExisting' + rowId).hide();
                    }
                }
                else if (id.indexOf("trRegNew") >= 0) {

                    if (viewData != "") {
                        btnId = viewData.toString().substring(13, viewData.length);
                    }
                    else {
                        btnId = 0;
                    }

                    rowId = id.toString().substring(8, id.length);

                    if ((ReleaseCount != null) && (ReleaseCount != "")) {

                        if (rowId >= ReleaseCount) {

                            var realvalues = [];
                            var textvalues = [];

                            $('#chkConfig_' + btnId + ' :selected').each(function (i, selected) {

                                realvalues[i] = $(selected).val();
                                textvalues[i] = $(selected).text();

                            });

                            for (var i = 0; i < realvalues.length; i++) {
                                $('#ddlConfigGrpList_' + rowId + ' option[value= "' + realvalues[i] + '"]').prop("selected", true);
                                var configId = $('#ddlConfigGrpList_' + rowId).attr("id");
                                GetConfigurationList(configId);
                                rowId++;
                            }
                            return false;
                        }
                    }
                }
                else {
                    var j = 1;
                }
            }
        });
    }
    chkDuplicateAndHide(ReleaseCountExact);
}

function CheckRequestTypeData(ReleaseCountExact) {
    for (var iCountRel = 0; iCountRel < ReleaseCountExact; iCountRel++) {
        $("#tblReleaseNew" + iCountRel + " div").each(function () {

            var id = $(this).attr('id');

            if (id != null) {

                if (id.indexOf("trRegular") >= 0) {

                    if (($("#chkRegularRetail").is(':checked')) && (($("#chkTVRadioBreakICLA").is(':checked') == false) && ($("#chkPriceReduction").is(':checked') == false))) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }

                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(9, Id.length);

                    if (($("#chkMultiArtist").is(':checked') == false)) {

                        if ($("#ddlRegPriceLevel_" + rowId).val() == 2) { // Budget 
                            $("#txtICLALevelRegular" + rowId).val('Budget');
                        }
                        if ($("#ddlRegPriceLevel_" + rowId).val() == 3) { // Mid
                            $("#txtICLALevelRegular" + rowId).val('Mid');
                        }
                        if ($("#ddlRegPriceLevel_" + rowId).val() == 1) { // Top
                            $("#txtICLALevelRegular" + rowId).val('Top');
                        }
                    }
                    if (($("#chkMultiArtist").is(':checked') == true)) {
                        if ($("#ddlRegPriceLevel_" + rowId).val() == 1 || $("#ddlRegPriceLevel_" + rowId).val() == 3) { // Top or Mid
                            $("#txtICLALevelRegular" + rowId).val('Multi-Artist');
                        }
                        if ($("#ddlRegPriceLevel_" + rowId).val() == 2) { // Budget
                            $("#txtICLALevelRegular" + rowId).val('Budget');
                        }
                    }


                    if ($('#chkRegDeviatedICLA_' + rowId) != null) {
                        var control = $('#chkRegDeviatedICLA_' + rowId).attr("id");
                        RegularDeviated(control);
                    }
                }

                // check if TV is selected
                if (id.indexOf("trTV") >= 0) {

                    if ($("#chkTVRadioBreakICLA").is(':checked')) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }

                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(4, Id.length);
                    if ($('#chkTVDeviatedICLA_' + rowId) != null) {
                        var control = $('#chkTVDeviatedICLA_' + rowId).attr("id");
                        TVDeviated(control);
                    }
                }

                // check if club is selected
                if (id.indexOf("trClubNew") >= 0) {
                    if ($("#chkClub").is(':checked')) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }

                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(9, Id.length);

                    if ($('#chkClubDeviatedICLA_' + rowId) != null) {

                        var control = $('#chkClubDeviatedICLA_' + rowId).attr("id");
                        ClubDeviated(control);
                    }
                }

                // check if club is selected
                if (id.indexOf("trPromoNew") >= 0) {
                    if ($("#chkPromotional").is(':checked')) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }

                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(10, Id.length);

                    if ($('#chkPromoDeviatedICLA_' + rowId) != null) {
                        var control = $('#chkPromoDeviatedICLA_' + rowId).attr("id");
                        PromoDeviated(control);
                    }
                }

                // check if non traditional is selected
                if (id.indexOf("trNon") >= 0) {

                    if ($("#chkNonTrditional").is(':checked')) {
                        $(this).show();
                    }
                    else {
                        $(this).hide();
                    }

                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(5, Id.length);

                    var controlId1 = $('#rdoNonTrad1' + rowId).attr("id");
                    var controlId2 = $('#rdoNonTrad2' + rowId).attr("id");

                    if ($('#' + controlId1).is(' :checked')) {

                        NonTraditionalDiv(controlId1);
                    }
                    else {
                        $('#tdNonDeactiveICLA' + rowId).hide();
                        $('#tdResFee' + rowId).hide();
                        $('#tdResFeeVal' + rowId).hide();
                        $('#tdNonComments' + rowId).hide();
                        $('#tdDeemed' + rowId).hide();
                        $('#tdDeemedVal' + rowId).hide();
                        $('#tdInvoiePrice' + rowId).hide();
                        $('#tdInvoicePriceVal' + rowId).hide();
                        $('#tdICLAAccBase' + rowId).hide();
                        $('#tdICLAAccBaseVal' + rowId).hide();
                        $('#tdSelling' + rowId)[0].style.visibility = "hidden";
                        $('#tdSellingVal' + rowId)[0].style.visibility = "hidden";
                        $('#tdICLAl' + rowId).hide();
                        $('#tdICLAVal' + rowId).hide();
                    }

                    if ($('#' + controlId2).is(' :checked')) {

                        SuggestedFeeData(controlId2);
                    }

                }

            }
        });
    }
};

function ClearIncludedList(rowid) {
    if ($('#ddlPackage_' + rowid).val() == 1) {
        $('#liPackageIncludedLabel' + rowid).hide();
        $('#liPackageIncludedUpc' + rowid).hide();
    }
    else {
        $('#liPackageIncludedLabel' + rowid).show();
        $('#liPackageIncludedUpc' + rowid).show();
    }
}

function duplicateClick(cntrl) {
    closeAllTheAutocompleteReleaseTab();

    $('#loadingDivPA').show();
    var btnId = $(cntrl).attr('id');
    $('#hdnDuplicate').attr('value', btnId);

    $('#hdncommand').val("Duplicate");

    var digitalVal = $('#CreateRegularProjectForm').serialize();

    $.post('/GCS/ClearanceProject/ReleaseNewDuplicateButton', digitalVal, function (data) {
        $('#loadingDivPA').hide();
        $('#tabs-3').html(data);
    });
}

function RegularDeviated(chkId) {

    var rowId = chkId.toString().substring(19, chkId.length);

    if ($("#" + chkId).is(':checked')) {
        $("#tdRegDeactiveICLA" + rowId).show();
        $("#tdRegComments" + rowId).show();
        $("#ddlRegICLALevel_" + rowId).removeAttr('disabled');
        $("#txtRegAreaComments_" + rowId).removeAttr('disabled');
        $("#txtRegAreaComments_" + rowId).watermark(watermarkComments);
        $("#txtRegAreaCommentsMandatory_" + rowId).show();
        $("#tdRegularDeactiveICLAMandatory" + rowId).show();
    }
    else {
        $("#txtRegAreaComments_" + rowId).val('');
        $("#ddlRegICLALevel_" + rowId).prop('selectedIndex', 0);

        $("#ddlRegICLALevel_" + rowId).attr('disabled', 'disabled');
        $("#txtRegAreaComments_" + rowId).attr('disabled', 'disabled');
        $("#txtRegAreaComments_" + rowId).removeClass('input-validation-error');
        $("#txtRegAreaCommentsMandatory_" + rowId).hide();
        $("#tdRegularDeactiveICLAMandatory" + rowId).hide();
    }
}

function TVDeviated(chkId) {


    var rowId = chkId.toString().substring(18, chkId.length);

    if ($("#" + chkId).is(':checked')) {
        $("#tdTVDeactiveICLA" + rowId).show();
        $("#tdTVComments" + rowId).show();
        $("#ddlTVICLALevel_" + rowId).removeAttr('disabled');
        $("#txtTVAreaComments_" + rowId).removeAttr('disabled');
        $("#txtTVAreaComments_" + rowId).watermark(watermarkComments);
        $("#txtTVAreaCommentsMandatory_" + rowId).show();
        $("#tdTVDeactiveICLAMandatory" + rowId).show();
    }
    else {
        $("#txtTVAreaComments_" + rowId).val('');
        $("#ddlTVICLALevel_" + rowId).prop('selectedIndex', 0);
        $("#ddlTVICLALevel_" + rowId).attr('disabled', 'disabled');
        $("#txtTVAreaComments_" + rowId).attr('disabled', 'disabled');
        $("#txtTVAreaComments_" + rowId).removeClass('input-validation-error');
        $("#txtTVAreaCommentsMandatory_" + rowId).hide();
        $("#tdTVDeactiveICLAMandatory" + rowId).hide();
    }
}

function NonTraditionalDeviated(chkId) {

    var rowId = chkId.toString().substring(23, chkId.length);

    if ($("#" + chkId).is(':checked')) {
        $("#tdNonDeactiveICLA" + rowId).show();
        $("#tdNonComments" + rowId).show();
        $("#ddlNonICLALevel_" + rowId).removeAttr('disabled');
        $("#txtNonTradAreaComments_" + rowId).removeAttr('disabled');
        $("#txtNonTradAreaCommentsMandatory_" + rowId).show();
        $("#ddlNonICLALevelMandatory" + rowId).show();
    }
    else {
        $("#tdNonDeactiveICLA" + rowId).show();
        $("#tdNonComments" + rowId).show();
        $("#ddlNonICLALevel_" + rowId).attr('disabled', 'disabled');
        $("#txtNonTradAreaComments_" + rowId).attr('disabled', 'disabled');
        $("#txtNonTradAreaCommentsMandatory_" + rowId).hide();
        $("#ddlNonICLALevelMandatory" + rowId).hide();
    }
}

function PriceDeviated(chkId) {

    var rowId = chkId.toString().substring(21, chkId.length);

    if ($("#" + chkId).is(':checked')) {
        $("#tdPriceDeactiveICLA" + rowId).show();
        $("#tdPriceComments" + rowId).show();
    }
    else {
        $("#tdPriceDeactiveICLA" + rowId).hide();
        $("#tdPriceComments" + rowId).hide();
    }
}

function ClubDeviated(chkId) {

    var rowId = chkId.toString().substring(20, chkId.length);
    if ($("#" + chkId).is(':checked')) {
        $("#tdClubDeactiveICLA" + rowId).show();
        $("#tdClubComments" + rowId).show();
        $("#ddlClubICLALevel_" + rowId).removeAttr('disabled');
        $("#txtClubAreaComments_" + rowId).removeAttr('disabled');
        $("#txtClubAreaComments_" + rowId).watermark(watermarkComments);
        $("#txtClubAreaCommentsMandatory_" + rowId).show();
        $("#tdClubDeactiveICLAMandatory" + rowId).show();
    }
    else {
        $("#txtClubAreaComments_" + rowId).val('');
        $("#ddlClubICLALevel_" + rowId).prop('selectedIndex', 0);
        $("#ddlClubICLALevel_" + rowId).attr('disabled', 'disabled');
        $("#txtClubAreaComments_" + rowId).attr('disabled', 'disabled');
        $("#txtClubAreaCommentsMandatory_" + rowId).hide();
        $("#tdClubDeactiveICLAMandatory" + rowId).hide();
    }
}

function PromoDeviated(chkId) {

    var rowId = chkId.toString().substring(21, chkId.length);
    if ($("#" + chkId).is(':checked')) {
        $("#tdPromoDeactiveICLA" + rowId).show();
        $("#tdPromoComments" + rowId).show();
        $("#ddlPromoICLALevel_" + rowId).removeAttr('disabled');
        $("#txtPromoAreaComments_" + rowId).removeAttr('disabled');
        $("#txtPromoAreaComments_" + rowId).watermark(watermarkComments);
        $("#txtPromoAreaCommentsMandatory_" + rowId).show();
        $("#tdPromoDeactiveICLAMandatory" + rowId).show();
    }
    else {
        $("#ddlPromoICLALevel_" + rowId).prop('selectedIndex', 0);
        $("#txtPromoAreaComments_" + rowId).val('');
        $("#ddlPromoICLALevel_" + rowId).attr('disabled', 'disabled');
        $("#txtPromoAreaComments_" + rowId).attr('disabled', 'disabled');
        $("#txtPromoAreaCommentsMandatory_" + rowId).hide();
        $("#tdPromoDeactiveICLAMandatory" + rowId).hide();
    }
}

function PackageClick(cntrl) {
    var rowId = cntrl.toString().substring(11, cntrl.length);
    if ($('#' + cntrl + ' :selected').text() == 'Yes') {
        $("#tdNoOfComp" + rowId).show();
        $("#tdddlComp" + rowId).show();
        $("#tdddlExistRel" + rowId).show();
        $("#tdExistRel" + rowId).show();
    }
    else {
        $("#txtNoOfComp_" + rowId).val('');
        $("#tdNoOfComp" + rowId).hide();
        $("#tdddlComp" + rowId).hide();
        $("#tdViewExisting" + rowId).hide();
        $("#tdddlExistRel" + rowId).hide();
        $("#tdExistRel" + rowId).hide();
        $("#btnSearchRelease" + rowId).hide();
        $('#ddlExistingRelease_' + rowId + ' option[value=1]').prop("selected", true);
    }
}

function GetConfigurationListParent(e, contId) {

    var configId = $(contId).attr('id');
    GetConfigurationList(configId);
    return false;
}

function GetConfigurationList(configId) {
    var rowId = configId.toString().substring(17, configId.length);

    $('#hdnConfigList').attr('value', $('#' + configId + ' :selected').val());

    var values = {
        "ConfigGroupName": $('#' + configId + ' :selected').val()
    };

    $.ajax({
        url: '/GCS/ClearanceProject/GetConfigList',
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values),
        success: function (data) {

            var items = "<option selected>--Select--</option>";
            if (data.Records != null) {
                for (var i = 0; i < data.Records.length; i++) {
                    items += "<option value='" + data.Records[i].Value + "'>" + data.Records[i].Text + "</option>";
                }
            }
            $('#ddlConfigList_' + rowId).html(items);

            if ($('#hdnConfigId' + rowId).val() != 0)
                $('#ddlConfigList_' + rowId).val($('#hdnConfigId' + rowId).val());

            if ($("#hdnStatusType").val() != 3 && $("#hdnStatusType").val() != 4 && ($('#IsReadOnlyMode').val() != 1)) {
                $('#btnAddRelease').attr('disabled', false);
            }
        }
    });
}

function ExistingRelease(cntrl) {

    var existRelId = $(cntrl).attr('id');
    var rowId = existRelId.toString().substring(19, existRelId.length);

    if ($('#' + existRelId + ' :selected').text() == 'Yes') {
        $("#tdViewExisting" + rowId).show();
        $("#btnSearchRelease" + rowId).show();
    }
    else {
        $("#tdViewExisting" + rowId).hide();
        $("#btnSearchRelease" + rowId).hide();
    }
}

function NonTraditionalDiv(Id) {

    var rowId = Id.toString().substring(11, Id.length);

    $("#tdResFee" + rowId).hide();
    $("#tdResFeeVal" + rowId).hide();

    $("#tdICLAl" + rowId).show();
    $("#tdICLAVal" + rowId).show();
    $("#tdNonDeactiveICLA" + rowId).show();
    $("#tdNonComments" + rowId).show();

    $('#chkNonTradDeviatedICLA_' + rowId).on("click", function () {
        if ($("#chkNonTradDeviatedICLA_" + rowId).is(':checked')) {
            $("#ddlNonICLALevel_" + rowId).removeAttr('disabled');
            $("#txtNonTradAreaComments_" + rowId).removeAttr('disabled');
            $("#txtNonTradAreaComments_" + rowId).watermark(watermarkComments);
            $("#txtNonTradAreaCommentsMandatory_" + rowId).show();

        }
        else {
            $("#txtNonTradAreaComments_" + rowId).val('');
            $('#ddlNonICLALevel_' + rowId).prop('selectedIndex', 0);
            $("#ddlNonICLALevel_" + rowId).attr('disabled', 'disabled');
            $("#txtNonTradAreaComments_" + rowId).attr('disabled', 'disabled');
            $("#txtNonTradAreaCommentsMandatory_" + rowId).hide();
        }
    });
    if ((!$("#chkPartwork").is(':checked')) && (!$("#chkKiosk").is(':checked')) && ($("#chkMailOrder").is(':checked')) && (!$("#chkInternet").is(':checked')) && (!$("#chkDirectResponse").is(':checked')) && (!$("#chkEducational").is(':checked')) && (!$("#chkPremium").is(':checked')) && (!$("#chkGiveAwayFreeOfCharge").is(':checked')) && (!$("#chkOther").is(':checked'))) {
        $("#txtICLALevelNon" + rowId).val('Mail Order');

    } else {
        $("#txtICLALevelNon" + rowId).val('Budget');
    }

    if ($('#cmbManByUMG :selected').val() == 'Yes') {

        $("#tdInvoiePrice" + rowId).show();
        $("#tdInvoicePriceVal" + rowId).show();

        $("#tdSelling" + rowId)[0].style.visibility = "hidden";
        $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";

        if ($("#chkPartwork").is(':checked')) {

            $("#tdICLAAccBase" + rowId).show();
            $("#tdICLAAccBaseVal" + rowId).show();
        }
        else {
            $("#tdICLAAccBase" + rowId).hide();
            $("#tdICLAAccBaseVal" + rowId).hide();

        }

        $("#tdDeemed" + rowId).hide();
        $("#tdDeemedVal" + rowId).hide();
    }
    else if ($('#cmbManByUMG :selected').val() == 'No') {

        if (!$("#chkGiveAwayFreeOfCharge").is(':checked')) {
            $("#tdICLAAccBase" + rowId).show();
            $("#tdICLAAccBaseVal" + rowId).show();

            $("#tdSelling" + rowId)[0].style.visibility = "";
            $("#tdSellingVal" + rowId)[0].style.visibility = "";

            $("#tdDeemed" + rowId).hide();
            $("#tdDeemedVal" + rowId).hide();
        }
        else {
            $("#tdDeemed" + rowId).show();
            $("#tdDeemedVal" + rowId).show();

            $("#tdICLAAccBase" + rowId).hide();
            $("#tdICLAAccBaseVal" + rowId).hide();

            $("#tdSelling" + rowId)[0].style.visibility = "hidden";
            $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";
        }

        $("#tdInvoiePrice" + rowId).hide();
        $("#tdInvoicePriceVal" + rowId).hide();
    }

    else {
        $("#tdDeemed" + rowId).hide();
        $("#tdDeemedVal" + rowId).hide();

        $("#tdICLAAccBase" + rowId).hide();
        $("#tdICLAAccBaseVal" + rowId).hide();

        $("#tdSelling" + rowId)[0].style.visibility = "hidden";
        $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";
    }
}

function SuggestedFeeData(Id) {

    var rowId = Id.toString().substring(11, Id.length);

    $("#tdResFee" + rowId).show();
    $("#tdResFeeVal" + rowId).show();
    $('#tdNonDeactiveICLA' + rowId).hide();
    $('#tdNonComments' + rowId).hide();
    $('#tdDeemed' + rowId).hide();
    $('#tdDeemedVal' + rowId).hide();
    $('#tdInvoiePrice' + rowId).hide();
    $('#tdInvoicePriceVal' + rowId).hide();
    $('#tdICLAAccBase' + rowId).hide();
    $('#tdICLAAccBaseVal' + rowId).hide();
    $('#tdSelling' + rowId)[0].style.visibility = "hidden";
    $('#tdSellingVal' + rowId)[0].style.visibility = "hidden";
    $('#tdICLAl' + rowId).hide();
    $('#tdICLAVal' + rowId).hide();

}

function OnDeleteClck(rowid) {

    var flag = confirm('Are you sure you want to delete this record?');
    if (flag) {
        $('#trRegNew' + rowid).each(function (index) {
            $("#Archive" + rowid).val('Y');
            $(this).closest('tr').next().hide();
            $(this).closest('tr').hide();
        });
        $('#trRegNew' + rowid + 'child').each(function (index) {
            $(this).closest('tr').hide();
        });
    }
    return false;
}

function setIclaSuggestedFeeSet(ReleaseCountExact) {
    for (var iCountRel = 0; iCountRel < ReleaseCountExact; iCountRel++) {
        $("#cmdmainDiv" + iCountRel + " input[type=radio]").each(function () {

            var Id = $(this).attr('id');
            var rowId = Id.toString().substring(11, Id.length);

            if ($("#hdnNonTrad1" + rowId).val() == "True") {
                $("#rdoNonTrad1" + rowId).val(true)
                $("#rdoNonTrad1" + rowId).attr('checked', true);

                NonTraditionalDiv(Id);
            }
            else if ($("#hdnNonTrad2" + rowId).val() == "True") {
                $("#rdoNonTrad2" + rowId).val(true)
                $("#rdoNonTrad2" + rowId).attr('checked', true);
                $('#tdNonDeactiveICLA' + rowId).hide();
                $('#tdResFee' + rowId).show();
                $('#tdResFeeVal' + rowId).show();
                $('#tdNonComments' + rowId).hide();
                $('#tdDeemed' + rowId).hide();
                $('#tdDeemedVal' + rowId).hide();
                $('#tdInvoiePrice' + rowId).hide();
                $('#tdInvoicePriceVal' + rowId).hide();
                $('#tdICLAAccBase' + rowId).hide();
                $('#tdICLAAccBaseVal' + rowId).hide();
                $('#tdSelling' + rowId)[0].style.visibility = "hidden";
                $('#tdSellingVal' + rowId)[0].style.visibility = "hidden";
                $('#tdICLAl' + rowId).hide();
                $('#tdICLAVal' + rowId).hide();
            }
        });
    }
}

function setConfigurationbasedonCG(ReleaseCountExact) {
    for (var iCountRel = 0; iCountRel < ReleaseCountExact; iCountRel++) {
        $("#tblReleaseNew" + iCountRel + " div").each(function () {
            var id = $(this).attr('id');

            if (id != null) {
                if (id.indexOf("trConfig") >= 0) {
                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(8, Id.length);

                    var configId = "ddlConfigGrpList_" + rowId;
                    GetConfigurationList(configId);
                }
            }
        });
    }
}

function OnDeleteClickNewRelease(rowid) {
    closeAllTheAutocompleteReleaseTab();

    var visiblerow = 0;
    var rowcount = $('#hdnReleaseRowsCount').val();

    for (i = 0; i < rowcount; i++) {
        if ($('#trRegNew' + i).is(":visible")) {
            visiblerow = visiblerow + 1;
        }
    }

    var ReleaseId = $('#hdnReleaseId' + rowid).val();

    $('#tr' + rowid).hide();

    $('#hdnReleaseId').val(ReleaseId);
    $('#hdnArchiveFlag' + rowid).val("Y");
    ShowUPCButton();

    if (ReleaseId > 0) {
        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/RegularProjectRemoveNewRelease', digitalVal + "&releaseId=" + ReleaseId, function (data) {
        });
    }

    return false;
}

function onkeyDownEventUPC(e, id) {

    if (e.keyCode == 13) {
        SetUPCNumberlink(id);
    }
}

function SetUPCNumberlink(id) {

    $("#divErrorMessage").text("");
    $("#divErrorMessage").hide();
    $('#divErrorMessage').removeClass('success');

    if ($('#txtNewReleaseUpcNum' + id).val() == "") {
        $("#lnkRemoveUPCNumber" + id).attr("disabled", true);
        $("#lnkRemoveUPCNumber" + id).addClass('lnkUPCNumberCssdisabled');
        $("#lnkRemoveUPCNumber" + id).hide();
        $('#txtAdditionalNewReleaseUpcNum' + id).val("");
        $('#loadingDivPA').hide();
    }
    else {
        if ($('#txtNewReleaseUpcNum' + id).val().length == 13) {

            $('#loadingDivPA').show();

            var values =
                {
                    "upcNumber": $('#txtNewReleaseUpcNum' + id).val(),
                    "projectReleseId": $('#hdnReleaseId' + id).val()
                };
            $.ajax({
                type: 'POST',
                url: '/GCS/ClearanceRelease/GetManualUPCNumber',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(values),
                success: function (response) {

                    if (response.Result == "OK" && response.upcResult != "") {

                        $('#txtNewReleaseUpcNum' + id).val($('#txtNewReleaseUpcNum' + id).val())
                        $('#txtAdditionalNewReleaseUpcNum' + id).val(response.upcResult)
                        $("#hdnUpcNumber" + id).val($('#txtNewReleaseUpcNum' + id).val() + response.upcResult);
                        $("#lnkRemoveUPCNumber" + id).removeAttr("disabled");
                        $("#lnkRemoveUPCNumber" + id).addClass('lnkUPCNumberCss');
                        $("#lnkRemoveUPCNumber" + id).show();
                        $("#chkUpcNumber_" + id).attr("disabled", true);
                        $("#chkUpcNumber_" + id).addClass('chkUpcNumberdisabled');
                        $("#hdnManualUpcNumber" + id).val("Y");
                        var isValueExist = true;
                        var statusType = $("#hdnStatusType").val();
                        $('.hdnUPCNumberCls').each(function () {
                            if ($(this).val() == "") {
                                isValueExist = false;
                            }
                        });
                        if ((statusType == "5" || statusType == "2" || statusType == "6") && isValueExist == false) {
                            $("#btnAllocateUPC").show();
                        }
                        else {
                            $("#btnAllocateUPC").hide();
                        }
                        $('#loadingDivPA').hide();
                    }
                    else {

                        displayDialog('Regular Project', 'Please enter 13 digit valid UPC number');
                        $('#loadingDivPA').hide();
                    }
                }
            });
        }
    }
}

function RemoveUPCNumber(controlId) {

    var upcNumer = "";
    $("#divErrorMessage").text("");
    $("#divErrorMessage").hide();
    $('#divErrorMessage').removeClass('success');

    $('#loadingDivPA').show();
    var values =
            {
                "projectReleseId": $('#hdnReleaseId' + controlId).val()
            };

    $.ajax({
        type: 'POST',
        url: '/GCS/ClearanceRelease/RemoveUPCNumber',
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values),
        success: function (response) {
            var errorMsg = "";

            if (response.Result == "OK") {
                $('#txtNewReleaseUpcNum' + controlId).val("");
                $('#txtAdditionalNewReleaseUpcNum' + controlId).val("");

                $("#lnkRemoveUPCNumber" + controlId).attr("disabled", true);
                $("#lnkRemoveUPCNumber" + controlId).hide();
                $("#lnkRemoveUPCNumber" + controlId).addClass('lnkUPCNumberCssdisabled');
                upcNumer = $("#hdnUpcNumber" + controlId).val();
                $("#hdnUpcNumber" + controlId).val("");
                $("#chkUpcNumber_" + controlId).removeAttr("disabled");
                $("#chkUpcNumber_" + controlId).attr("checked", false);
                $("#chkUpcNumber_" + controlId).removeClass('chkUpcNumberdisabled');
                $("#chkUpcNumber_" + controlId).addClass('chkUpcNumber');
                document.getElementById("tdReleaseNewUPC " + controlId).innerHTML = "";
                $("#txtNewReleaseUpcNum" + controlId).removeAttr("disabled");
                $("#txtNewReleaseUpcNum" + controlId).show();
                $("#txtNewReleaseUpcNum" + controlId).addClass('WidthUpc');
                $("#txtAdditionalNewReleaseUpcNum" + controlId).show();
                $("#hdnManualUpcNumber" + controlId).val("");
                ShowUPCButton();

                var digitalVal = $('#CreateRegularProjectForm').serialize();

                $.post('/GCS/ClearanceProject/EnableDisblePushToR2Btn', digitalVal, function (data) {
                    var enableBtn = data;

                    if (enableBtn == "True") {
                        $('#btnPushToR2').show();
                    }
                    else {
                        $('#btnPushToR2').hide();
                    }
                });
                $('#loadingDivPA').hide();
            }
            if (upcNumer != "") {
                $('#divErrorMessage').addClass('success');
                $("#divErrorMessage").show();
                $('#tdScope').addClass('input-validation-error');
                $("#divErrorMessage").html(upcNumer + " has been removed.").show();
            }
            $('#loadingDivPA').hide();
        }
    });
}

function ShowUPCButton() {
    var isValueExist = false;
    var statusType = $("#hdnStatusType").val();
    var userRole = $("#hdnCurrentUserRole").val();
    $('.hdnUPCNumberCls').each(function () {

        var id = $(this).attr('id');
        var ides = id.replace('hdnUpcNumber', '');
        var releaseId = $('#hdnReleaseId' + ides).val();
        var archiveFlag = $('#hdnArchiveFlag' + ides).val();

        var musicType = $("#ddlMusicType_" + ides).val()
        if ($(this).val() == "" && (userRole == "RCCAdmin" || (userRole == "Requestor" && musicType != "1")) && releaseId != "" && releaseId != "0" && archiveFlag != "Y") {
            isValueExist = true;
            return;
        }
    });

    if ((statusType == "5" || statusType == "2" || statusType == "6") && isValueExist == true) {
        $("#btnAllocateUPC").show();
    }
    else {
        $("#btnAllocateUPC").hide();
    }
}

function searchReleaseclass(cntrl) {
    var objSearchPopup = $('<div id="SearchReleasePopup" style="margin:0;padding:0;"></div>');
    $(objSearchPopup).dialog({

        title: 'Search For Release',
        autoOpen: true,
        modal: true,
        close: function () {
            $(this).remove(); // ensures any form variables are reset.  
            $(this).dialog('close');
            $('#loadingDivPA').hide();
        },
        show: 'clip',
        hide: 'clip',
        width: 1000,
        resizable: false,
        open: function () {
            $(this).load('/GCS/Search/SearchForRelease');
            var dialogue = $('.ui-dialog')

            dialogue.animate({
                top: "40px"
            }, 0);

        }
    });

    var searchbtnId = $(this)[0].event.srcElement.id;
    var rowId = searchbtnId.toString().substring(16, searchbtnId.length);

    $('#CallFrom').val("PackageReleaseNew");
    $('#hdnrowId').empty();
    $('#hdnrowId').append(rowId);
}

function ViewComponentNewclass(cntrl) {

    var rowId = cntrl.title.toString().split('-');
    $('#hdnrowId').empty();
    $('#hdnrowId').append(rowId[0]);
    if ($('#ExistingReleasesUPC' + rowId[0]).val() != "") {
        var selectedRows = $('#ExistingReleasesUPC' + rowId[0]).val().toString().split('~');

        var UPCNO;
        if ($('#hdnManualUpcNumber' + rowId[0]).val() != null && $('#hdnManualUpcNumber' + rowId[0]).val() != "" && $('#hdnManualUpcNumber' + rowId[0]).val() != "N") {
            UPCNO = $('#hdnManualUpcNumber' + rowId[0]).val().toString();
        }

        if (UPCNO == "" || UPCNO == null) {

            UPCNO = "New Release";
        }
        var releaseids = new Array();
        var Upcs = new Array();
        var PackageIds = new Array();
        var count = 0;
        var selectedIds;

        for (i = 0; i < selectedRows.length; i++) {
            if (selectedRows[i] != "") {

                var selectedValues = selectedRows[i].toString().split('=');
                releaseids[count] = selectedValues[0];
                Upcs[count] = selectedValues[6];
                if (selectedValues.length > 27) {
                    if ($("#ProjectRefId").val().length > 0) {
                        PackageIds[count] = selectedValues[27];
                    }
                    else {
                        PackageIds[count] = 0;
                    }
                }
                else {
                    PackageIds[count] = 0;
                }
                count = count + 1;
            }
        }

        var packageInfo = new Array();
        var RemovedReleases = new Array();
        var count1 = 0;
        var RemoveReleaseRow = $('#RemovedPackageReleases' + rowId[0]).val().toString().split(',');
        for (m = 0; m < RemoveReleaseRow.length; m++) {
            if (RemoveReleaseRow[m] != "") {

                var removedValues = RemoveReleaseRow[m].toString().split('-');
                if (removedValues != null) {
                    RemovedReleases[count1] = removedValues[0];
                    count1 = count1 + 1;
                }
            }
        }
        var flag = false;
        for (countrel = 0; countrel < releaseids.length; countrel++) {
            flag = false;
            if (selectedIds == null) {
                if (RemovedReleases != null) {
                    if (RemovedReleases.length > 0) {
                        for (countremove = 0; countremove < RemovedReleases.length; countremove++) {
                            if (flag == false) {
                                if (releaseids[countrel] == RemovedReleases[countremove]) {
                                    flag = true;
                                    selectedIds = '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"Y","clearanceRelease.Upc":"' + UPCNO + '"';
                                }
                                else {
                                    selectedIds = '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"N","clearanceRelease.Upc":"' + UPCNO + '"';
                                }
                            }
                        }
                    }
                    else {
                        selectedIds = '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"N","clearanceRelease.Upc":"' + UPCNO + '"';
                    }
                }
                else {
                    selectedIds = '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"N","clearanceRelease.Upc":"' + UPCNO + '"';
                }
            }
            else {
                if (RemovedReleases != null) {
                    if (RemovedReleases.length > 0) {
                        for (countremove = 0; countremove < RemovedReleases.length; countremove++) {
                            if (flag == false) {
                                if (releaseids[countrel] == RemovedReleases[countremove]) {
                                    flag = true;
                                    selectedIds = selectedIds + ',' + '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"Y","clearanceRelease.Upc":"' + UPCNO + '"';
                                }
                                else {
                                    selectedIds = selectedIds + ',' + '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"N","clearanceRelease.Upc":"' + UPCNO + '"';
                                }
                            }
                        }
                    }
                    else {
                        selectedIds = selectedIds + ',' + '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"N","clearanceRelease.Upc":"' + UPCNO + '"';
                    }
                }
                else {
                    selectedIds = selectedIds + ',' + '"clearanceRelease.PackageInfo[' + countrel + '].ReleaseId":"' + releaseids[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].Upc":"' + Upcs[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].PackageId":"' + PackageIds[countrel] + '","clearanceRelease.PackageInfo[' + countrel + '].ArchiveFlag":"N","clearanceRelease.Upc":"' + UPCNO + '"';
                }
            }
        }

        var values = jQuery.parseJSON('{' + selectedIds + '}');

        var objViewComponent = $('<div id="ViewComponentPopup"></div>');
        $(objViewComponent).dialog({
            autoOpen: true,
            width: 1000,
            resizable: false,
            title: 'View Component',
            modal: true,
            close: function () {
                $(this).remove(); // ensures any form variables are reset.  
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            }
        });
        $('#loadingDivPA').show();
        $(objViewComponent).load('/GCS/ClearanceRelease/ViewPackageComponents', values);

        var dialogue = $('.ui-dialog');

        dialogue.animate({
            top: "30px"
        }, 0);
    }
}

function SaveRCCHandlerAfterUPCAllocation() {
    var postData = {
        "ProjectId": $("#Clr_Project_Id").val(),
        "User_Id": 0
    };
    $.ajax({
        url: '/GCS/ClearanceInbox/SaveRCCHandler',
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: 'json',
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
        },
        error: function () {
        }
    });

}

function ManageArtistclass(cntrl) {
    closeAllTheAutocompleteReleaseTab();

    var objArtistPopup = $('<div id="ArtistResourcePopup"></div>');

    var managebtnId = cntrl.id;
    var rowId = managebtnId.toString().substring(15, managebtnId.length);
    if ($('#hdnArtistId' + rowId).text() != "") {
        $(objArtistPopup).dialog({
            autoOpen: true,
            width: 900,
            close: function () {
                $('#hdnrowId').empty();
                $(this).remove(); // ensures any form variables are reset.  
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            },
            resizable: false,
            title: 'Manage Artist',
            modal: true
        });
        var values =
        {
            "existingArtist": $('#hdnArtistId' + rowId).text()
        }

        $(objArtistPopup).load('/GCS/Artist/SearchForArtist', values);
        $('#hdnrowId').empty();
        $('#hdnrowId').append(rowId);
        $('#tblExistingArtist').show();

        var dialogue = $('.ui-dialog');

        dialogue.animate({
            top: "30px"
        }, 0);

    }
    else {
        var objDialog = $(objArtistPopup);

        objDialog.dialog({
            resizable: false,
            autoOpen: true,
            width: 900,
            title: 'Manage Artist',
            close: function () {
                $('#hdnrowId').empty();
                $(this).remove(); // ensures any form variables are reset.  
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            },
            modal: true,
            open: function () {
                $(this).load('/GCS/Artist/SearchForArtist');
                $('#hdnrowId').empty();
                $('#hdnrowId').append(rowId);
            }
        });
        var dialogue = $('.ui-dialog');

        dialogue.animate({
            top: "40px"
        }, 0);

    }
    moveDialog();
};

function ToogleDiv(divID) {

    if (document.getElementById('divConfig_' + divID).style.display == "none") {
        document.getElementById('divConfig_' + divID).style.display = "block";
    }
    else {
        document.getElementById('divConfig_' + divID).style.display = "none";
    }
}

function ShowDiv(divID) {
    document.getElementById('divConfig_' + divID).style.display = "block";
}

function SetFlag() {
    flag = true;
}

function SetFlagFalse(divID) {
    flag = false;
}

function Toggle(divID) {
    if (flag) {
        flag = false;
        document.getElementById('divConfig_' + divID).style.display = "block";
    }
    else {
        flag = true;
        document.getElementById('divConfig_' + divID).style.display = "none";
    }
}

function moveDialog() {

    var dialogue = $('.ui-dialog')

    dialogue.animate({

        top: "40px"
    }, 1000);

}

function ChnageICLAdeviation(rowId) {

    if ($("#chkNonTradDeviatedICLA_" + rowId).is(':checked')) {
        $("#ddlNonICLALevel_" + rowId).removeAttr('disabled');
        $("#txtNonTradAreaComments_" + rowId).removeAttr('disabled');
        $("#txtNonTradAreaComments_" + rowId).watermark(watermarkComments);
        $("#txtNonTradAreaCommentsMandatory_" + rowId).show();

    }
    else {
        $("#txtNonTradAreaComments_" + rowId).val('');
        $('#ddlNonICLALevel_' + rowId).prop('selectedIndex', 0);
        $("#ddlNonICLALevel_" + rowId).attr('disabled', 'disabled');
        $("#txtNonTradAreaComments_" + rowId).attr('disabled', 'disabled');
        $("#txtNonTradAreaCommentsMandatory_" + rowId).hide();
    }
}

function EnableDuplicateButton() {
    if ($('#hdnReleaseRowsCount').val() > 0) { //If there are more than one release
        for (t = 0; t < $('#hdnReleaseRowsCount').val() ; t++) { //loop for release
            $("#btnDuplicate_" + t).attr('disabled', 'disabled'); //set the button as disabled in starting
            $("#btnDuplicate_" + t).addClass("disableButtonColor");
            if (($('#divArtistName' + t).text() != "") && ($('#txtTitle_' + t).val() != "") && ($('#txtNoOfTrack_' + t).val() != "") && ($('#labelName_' + t).val() != "") && ($('#labelName_' + t).val() != "") && ($('#ddlMusicType_' + t).val())) {
                $("#btnDuplicate_" + t).removeAttr('disabled'); //set the button as disabled in starting
                $("#btnDuplicate_" + t).removeClass("disableButtonColor");
            }
        }
    }
}

function chkDuplicateAndHide(ReleaseCountExact) {

    for (var iCountRel = 0; iCountRel < ReleaseCountExact; iCountRel++) {
        if ($("#ddlPackage_" + iCountRel).val() == 1) {
            $('#tdNoOfComp' + iCountRel).hide();
            $('#tdddlComp' + iCountRel).hide();
            $('#tdExistRel' + iCountRel).hide();
            $('#tdddlExistRel' + iCountRel).hide();
            $('#tdViewExisting' + iCountRel).hide();
        }
    }
}

function triggerRegularNewAuditHistory(releaseIndex) {
    var AuditTypeId;
    var SelectedItemIds = [];
    var displayTitle = '';
    AuditTypeId = AuditObjectType.RegularNonTraditionalProjectReleaseNewAuditHistory;

    if ($("#ProjectRefId").val().length > 0) {
        displayTitle = $("#ProjectRefId").val();
    }

    if ($('#hdnUpcNumber' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnUpcNumber' + releaseIndex).val();
    }

    if ($('#txtTitle_' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#txtTitle_' + releaseIndex).val();
    }

    if ($('#txtVersionTitle_' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#txtVersionTitle_' + releaseIndex).val();
    }
    SelectedItemIds.push($('#hdnReleaseId' + releaseIndex).val());
    for (i = 0; i < $('#hdnReleaseIdsCount').val() ; i++) {
        SelectedItemIds.push($('#hdnClrReleaseIds' + releaseIndex + i).val());
    }
    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
    return false;
}

function AddRelease() {
    closeAllTheAutocompleteReleaseTab();

    $('#loadingDivPA').show();
    var digitalVal = $('#CreateRegularProjectForm').serialize();
    $.post('/GCS/ClearanceProject/AddRelease', digitalVal, function (data) {
        $('#tabs-3').html(data);
        $('#btnAddRelease').attr('disabled', true);
        $('#loadingDivPA').hide();
    });
}

function closeAllTheAutocompleteReleaseTab() {
    var releaseRowsCount = $('#hdnReleaseRowsCount').val();
    var autocompleteId = "#labelName_";
    for (k = 0; k < releaseRowsCount ; k++) {
        $(autocompleteId + k).autocomplete("close");
    }
}

function isTheReleaseTabContext() {
    var tabId = "releaseTab";
    var tabIdCurrent = GCS.utilities.functions.getSelectedTab("#screenTabs");
    var isTheCurrentTab = (tabId === tabIdCurrent);
    return isTheCurrentTab;
}

$(document).ready(function () {

    if (($('#txtProjectTitle').val() != '') && ($('#ProjectRefId').val() == '')) {
        $('#txtTitle_0').val($('#txtProjectTitle').val());
    }

    if (typeof btnUPCAllocate != "undefined") {

        if (btnUPCAllocate == true) {
            $('#btnAllocateUPC').show();
        }
    }
    $.watermark.options.className = 'watermark';

    function autocompleteLabels(labelName, LabelId) {

        function isTheReleaseTabContext() {
            var tabId = "releaseTab";
            var tabIdCurrent = GCS.utilities.functions.getSelectedTab("#screenTabs");
            var isTheCurrentTab = (tabId === tabIdCurrent);
            return isTheCurrentTab;
        }

        $(labelName).autocomplete({
            appendTo: "#tabs-3",
            source: function (request, response) {

                var url = $(labelName).attr("data-autocomplete-source-manual");

                $(labelName).addClass("ui-autocomplete-loading");

                $(labelName).removeClass("ui-autocomplete-input");

                $.getJSON(url, { term: request.term, searchBy: $(labelName).attr("SearchFor") }, function (data) {
                    closeAllTheAutocompleteReleaseTab();

                    var popupId = "#ArtistResourcePopup";

                    if (!(GCS.utilities.functions.isTheDialogPopupOpened(popupId)) && (isTheReleaseTabContext())) {
                        response(data);
                    };

                    $(LabelId).val("");

                    $(labelName).removeClass("ui-autocomplete-loading");

                    $(labelName).addClass("ui-autocomplete-input");

                });

            },
            minLength: 3,
            select: function (event, ui) {

                $(LabelId).val(ui.item.id);
                $(labelName).val(ui.item.value);
            }
        });
    }

    if ($('#hdnReleaseRowsCount').val() > 0) {
        for (k = 0; k < $('#hdnReleaseRowsCount').val() ; k++) {
            autocompleteLabels("#labelName_" + k, "#LabelId_" + k);
            if ($('#ExistingReleasesUPC' + k).val() != null && $('#ExistingReleasesUPC' + k).val() != "") {
                $('#liPackageIncludedLabel' + k).show();
                $('#liPackageIncludedUpc' + k).show();
                var AddedPackagesUPCs = null;
                var existingReleases = $('#ExistingReleasesUPC' + k).val().toString().split('~');
                for (var i = 0; i < existingReleases.length; i++) {
                    if (existingReleases[i] != '') {
                        var existingReleasesDetails = existingReleases[i].toString().split('=');
                        if (AddedPackagesUPCs == null) {
                            AddedPackagesUPCs = existingReleasesDetails[6];
                        }
                        else {
                            AddedPackagesUPCs = AddedPackagesUPCs + ', ' + existingReleasesDetails[6];
                        }
                    }
                }
                $('#PackageIncludedUPC' + k)[0].innerHTML = AddedPackagesUPCs;
            }
            else {
                $('#ddlExistingRelease_' + k + ' option[value=1]').prop("selected", true);
            }

            var artistName = "";
            if ($('#hdnArtistCount' + k).val() != null) {
                for (i = 0; i < $('#hdnArtistCount' + k).val() ; i++) {

                    $('#hdnArtistId' + k).append($('#hdnArtistName' + k + '_' + i).val() + ':' + $('#hdnArtistId' + k + '_' + i).val() + ':' + $('#hdnArtistTalentId' + k + '_' + i).val() + '=')
                    $('#hdnArtist' + k).val($('#hdnArtist' + k).val() + $('#hdnArtistName' + k + '_' + i).val() + ':' + $('#hdnArtistId' + k + '_' + i).val() + ':' + $('#hdnArtistTalentId' + k + '_' + i).val() + '=')

                    if (artistName == "") {
                        artistName += $('#hdnArtistName' + k + '_' + i).val();
                    }
                    else {
                        artistName += ',' + $('#hdnArtistName' + k + '_' + i).val();
                    }
                }

                if (artistName.length > 25) {
                    $('#divArtistName' + k).attr('title', artistName);
                    artistName = artistName.substring(0, 25) + '...';
                    $('#divArtistName' + k).append(artistName);
                }
                else {
                    $('#divArtistName' + k).attr('title', artistName);
                    $('#divArtistName' + k).append(artistName);
                }
            }
        }
    }

    var viewData = $('#hdnbuttonId').val();
    var ReleaseCount = $('#hdnReleaseCount').val();
    var ReleaseCountExact = $('#hdnReleaseRowsCount').val();

    for (var i = 0; i < ReleaseCountExact; i++) {
        $("#txtAreaComments_" + i).watermark(watermarkComments);
        if ($('#ddlExistingRelease_' + i + ' :selected').text() == 'Yes') {
            $("#tdViewExisting" + i).show();
            $("#btnSearchRelease" + i).show();
        }
        else {
            $("#tdViewExisting" + i).hide();
            $("#btnSearchRelease" + i).hide();
        }

    }
    //Show hide sales channel section on the basis of request type tab
    CheckRequestTypeData(ReleaseCountExact);

    CheckDuplicateControls(viewData, ReleaseCount, ReleaseCountExact);
    // added by dhruv
    setIclaSuggestedFeeSet(ReleaseCountExact);

    if ($('#Clr_Project_Id').val() != 0) {
        setConfigurationbasedonCG(ReleaseCountExact);
    }

    $('#tabs').tabs({
        select: function (event, ui) {
            var selected = ui.panel.id;
            switch (selected) {
                case 'tabs-1':
                    $('#cmbReleaseNewOrExisting').attr('disabled', true);
                    $('#regularProjInfoAuditLink').show();
                    $('#regularReqTypeAuditLink').hide();
                    break;
                case 'tabs-2':
                    // when first save is not clicked
                    if ($('#txtProjectReferenceId').val() == "") {
                        $('#cmbReleaseNewOrExisting').attr('disabled', false);
                    }
                        // when save clicked and release added - disable permanenlty
                    else if ($('#hdnArchiveFlag0').val() != null && $('#txtProjectReferenceId').val() != "") {
                        if ($('#hdnArchiveFlag0').val() != 'Y') {
                            $('#cmbReleaseNewOrExisting').attr('disabled', true);
                        }
                        else {
                            $('#cmbReleaseNewOrExisting').attr('disabled', false);
                        }
                    }

                    else {
                        $('#cmbReleaseNewOrExisting').attr('disabled', false);
                    }
                    $('#regularReqTypeAuditLink').show();
                    $('#regularProjInfoAuditLink').hide();
                    break;
                case 'tabs-3':
                    $('#cmbReleaseNewOrExisting').attr('disabled', true);
                    CheckRequestTypeData($('#hdnReleaseRowsCount').val());

                    if ($('#IsReadOnlyMode').val() == 1) {
                        $('#btnAddRelease').attr('disabled', 'disabled');
                        $('#tblRelease').find('input, textarea, button, select, .ui-datepicker-trigger').each(function () {
                            var id = $(this).attr('id');

                            if (id != undefined) {

                                if (id.indexOf("lnkViewExtRelease_") >= 0) {

                                    $(this)[0].innerHTML = "View Existing Release";
                                }
                                else if (id.indexOf("chkUpcNumber") < 0 && id.indexOf("lnkRemoveUPCNumber") < 0 && id.indexOf("txtNewReleaseUpcNum") < 0
                                && id.indexOf("txtAdditionalNewReleaseUpcNum") < 0) {
                                    $(this).attr('disabled', 'disabled');
                                }
                            }


                        });
                    }
                    //For Audit history link
                    $('#regularProjInfoAuditLink').hide();
                    $('#regularReqTypeAuditLink').hide();
                    break;
                case 'tabs-4':
                    $('#cmbReleaseNewOrExisting').attr('disabled', true);
                    //For Audit history link
                    $('#regularProjInfoAuditLink').hide();
                    $('#regularReqTypeAuditLink').hide();
                    break;
                case 'tabs-5':
                    //For Audit history link
                    $('#regularProjInfoAuditLink').hide();
                    $('#regularReqTypeAuditLink').hide();
                    break;
                default:
                    break;
            }
        }
    });

    $("#lnkExpand").click(function (event) {
        $('.details').each(function (index) {
            id = $(this).attr('id');
            var rowId = id.toString().substring(4, id.length);
            if (rowId != null) {
                document.getElementById('ClearanceRelease_' + rowId).style.display = "block";
                $('#img_' + rowId).removeClass('rightArrow');
                $('#img_' + rowId).addClass('downArrowUPC');
                $("#cmdmainDiv" + rowId).show();
            }
        });
    });

    $("#lnkCollapse").click(function (event) {
        $('.details').each(function (index) {

            id = $(this).attr('id');
            var rowId = id.toString().substring(4, id.length);

            if (rowId != null) {
                document.getElementById('ClearanceRelease_' + rowId).style.display = "none";
                $('#img_' + rowId).removeClass('downArrowUPC');
                $('#img_' + rowId).addClass('rightArrow');
                $("#cmdmainDiv" + rowId).hide();
            }
        });
    });

    var ReleaseCountExact = $('#hdnReleaseRowsCount').val();

    for (var i = 0; i < ReleaseCountExact; i++) {

        $('#img_' + i).click(function (event) {
            id = $(this).attr('id');
            var rowId = id.toString().substring(4, id.length);
            var display = document.getElementById('ClearanceRelease_' + rowId).style.display;

            if (rowId != null) {

                if (display == 'none') {
                    document.getElementById('ClearanceRelease_' + rowId).style.display = "block";
                    $('#img_' + rowId).removeClass('rightArrow');
                    $('#img_' + rowId).addClass('downArrowUPC');
                    $("#cmdmainDiv" + rowId).show();

                } else {
                    document.getElementById('ClearanceRelease_' + rowId).style.display = "none";
                    $('#img_' + rowId).removeClass('downArrowUPC');
                    $('#img_' + rowId).addClass('rightArrow');
                    $("#cmdmainDiv" + rowId).hide();
                }
            }
        });
    }

    EnableDuplicateButton();

    $(".regularICLAclass").on("click", function () {

        var chkId = $(this).attr('id');
        RegularDeviated(chkId);

    });

    $(".tvICLAclass").on("click", function () {

        var chkId = $(this).attr('id');
        TVDeviated(chkId);

    });

    $(".nonICLAclass").on("click", function () {

        var chkId = $(this).attr('id');
        NonTraditionalDeviated(chkId);

    });

    $(".priceICLAclass").on("click", function () {

        var chkId = $(this).attr('id');
        PriceDeviated(chkId);

    });

    $(".clubICLAclass").on("click", function () {

        var chkId = $(this).attr('id');
        ClubDeviated(chkId);

    });

    $(".promoICLAclass").on("click", function () {

        var chkId = $(this).attr('id');
        PromoDeviated(chkId);

    });

    $(".packageClass").on("change", function () {

        var pkgId = $(this).attr('id');
        PackageClick(pkgId);

    });

    $('.existingRelPkgclass').on("change", function () {

        ExistingRelease(this);

    });

    $('.regPriceLevelclass').on("change", function () {

        var Id = $(this).attr('id');
        var rowId = Id.toString().substring(17, Id.length);
        $("#txtICLALevelRegular" + rowId).val($('#' + Id + ' :selected').text());

        if (($("#chkMultiArtist").is(':checked') == false)) {

            if ($("#ddlRegPriceLevel_" + rowId).val() == 2) { // Budget 
                $("#txtICLALevelRegular" + rowId).val('Budget');
            }
            if ($("#ddlRegPriceLevel_" + rowId).val() == 3) { // Mid
                $("#txtICLALevelRegular" + rowId).val('Mid');
            }
            if ($("#ddlRegPriceLevel_" + rowId).val() == 1) { // Top
                $("#txtICLALevelRegular" + rowId).val('Top');
            }
        }
        if (($("#chkMultiArtist").is(':checked') == true)) {
            if ($("#ddlRegPriceLevel_" + rowId).val() == 1 || $("#ddlRegPriceLevel_" + rowId).val() == 3) { // Top or Mid
                $("#txtICLALevelRegular" + rowId).val('Multi-Artist');
            }
            if ($("#ddlRegPriceLevel_" + rowId).val() == 2) { // Budget
                $("#txtICLALevelRegular" + rowId).val('Budget');
            }
        }

    });

    $('.tvPriceLevelclass').on("change", function () {

        var Id = $(this).attr('id');
        var rowId = Id.toString().substring(16, Id.length);
        $("#txtICLALevelTVRadio" + rowId).val('TV Break ICLA');

    });

    $('.radioICLAclass').on("click", function (event) {

        event.stopPropagation();
        var Id = $(this).attr('id');
        var rowId = Id.toString().substring(11, Id.length);

        $("#hdnNonTrad1" + rowId).val('True');
        $("#hdnNonTrad2" + rowId).val('False');

        $("#dvNonTraditionalRelease" + rowId).removeClass('input-validation-error');

        if ($('#' + Id).is(':checked')) {
            $('#txtResourceFee_' + rowId).val('');
            $('#ICLA_Non' + rowId).val('true');
            $('#SuggestedFee_Non' + rowId).val('false');
            $("#txtNonTradAreaComments_" + rowId).attr('disabled', 'disabled');
        }
        else {
            $('#ICLA_Non' + rowId).val('false');
        }
        NonTraditionalDiv(Id);

    });

    $('.radioSuggestedclass').on("click", function () {

        var Id = $(this).attr('id');
        var rowId = Id.toString().substring(11, Id.length);

        $("#hdnNonTrad2" + rowId).val('True');
        $("#hdnNonTrad1" + rowId).val('False');

        $("#dvNonTraditionalRelease" + rowId).removeClass('input-validation-error');

        if ($('#' + Id).is(':checked')) {
            $("#txtSellPrice_" + rowId).val('');
            $("#chkNonTradDeviatedICLA_" + rowId).attr('checked', false);
            $('#ddlNonICLALevel_' + rowId).prop('selectedIndex', 0);
            $("#txtNonTradAreaComments_" + rowId).val('');
            $("#txtICLAcc_" + rowId).val('');

            $('#SuggestedFee_Non' + rowId).val('true');
            $('#ICLA_Non' + rowId).val('false');

        }
        else {
            $('#txtResourceFee_' + rowId).val('');
            $('#SuggestedFee_Non' + rowId).val('false');
        }

        SuggestedFeeData(Id);

    });

    $('.DeletelinkClass').on("click", function () {

        var linkId = $(this).attr('id');
        var rowId = linkId.toString().substring(9, linkId.length);

        OnDeleteClck(rowId);

    });

    $("#lnkReleaseNewSelectAll").on("click", function () {

        $('.chkUpcNumber').attr('checked', "checked");
    });

    $('#btnAllocateUPC').on("click", function () {

        $('#loadingDivPA').show();
        $("#divErrorMessage").text("");
        $("#divErrorMessage").hide();
        $('#divErrorMessage').removeClass('success');

        var allCheckBoxValue = [];
        var classicReleaseiD = "";
        var NonClassicReleaseiD = "";


        $('.chkUpcNumber:checked').each(function () {
            var id = $(this).attr('id');
            var ides = id.split('_');
            var musicType = $("#ddlMusicType_" + ides[1]).val();
            var ReleaseId = $('#hdnReleaseId' + ides[1]).val();

            if (musicType == 1) {

                classicReleaseiD += ReleaseId + "~";
            }
            else {

                NonClassicReleaseiD += ReleaseId + "~";
            }

        });

        if (classicReleaseiD == "" && NonClassicReleaseiD == "") {
            $('#divErrorMessage').addClass('warning');
            $("#divErrorMessage").show();
            $('#tdScope').addClass('input-validation-error');
            $("#divErrorMessage").html("Please select atleast one checkbox").show();
        }

        var values =
                {
                    "classicCount": classicReleaseiD + "," + NonClassicReleaseiD
                };

        $.ajax({
            type: 'POST',
            url: '/GCS/ClearanceRelease/GetUPCNumber',
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(values),
            success: function (response) {
                var errorMsg = "";
                var userRole = $("#hdnCurrentUserRole").val();

                if (response.Result == "OK") {
                    var nonClassyArray = new Array;
                    var classyArray = new Array;
                    var upcResultRecord = response.upcResult.split("~");
                    for (var upcCount = 0; upcCount < upcResultRecord.length; upcCount++) {
                        var upcRecord = upcResultRecord[upcCount].split(":");
                        var musicType = upcRecord[0].split("_");
                        if (musicType[0] == "NC") {
                            if (upcRecord[1] != "NOCOUNT") {
                                nonClassyArray.push(musicType[1] + "_" + upcRecord[1]);
                            }
                            else {
                                errorMsg = "No More Non-Classic UPC number exist in the system.";
                            }
                        }
                        else if (musicType[0] == "MC") {

                            if (upcRecord[1] != "NOCOUNT") {
                                classyArray.push(musicType[1] + "_" + upcRecord[1]);
                            }
                            else {
                                errorMsg += "No More Classic UPC number exist in the system.";
                            }
                        }
                    }
                    $('.chkUpcNumber:checked').each(function () {


                        var id = $(this).attr('id');
                        var ides = id.split('_');
                        var musicType = $("#ddlMusicType_" + ides[1]).val();
                        var ReleaseId = $('#hdnReleaseId' + ides[1]).val();
                        if (musicType == 1) {
                            for (var n = 0; n < classyArray.length; n++) {
                                var record = classyArray[n].split("_");
                                if (record[0] == ReleaseId) {
                                    document.getElementById("tdReleaseNewUPC " + ides[1]).innerHTML = record[1];
                                    $("#hdnUpcNumber" + ides[1]).val(record[1]);
                                }
                            }
                        }
                        else {
                            for (var n = 0; n < nonClassyArray.length; n++) {

                                var record = nonClassyArray[n].split("_");
                                if (record[0] == ReleaseId) {
                                    document.getElementById("tdReleaseNewUPC " + ides[1]).innerHTML = record[1];
                                    $("#hdnUpcNumber" + ides[1]).val(record[1]);
                                }
                            }
                        }

                        $("#lnkRemoveUPCNumber" + ides[1]).removeAttr("disabled");
                        $("#lnkRemoveUPCNumber" + ides[1]).addClass('lnkUPCNumberCss');
                        $("#lnkRemoveUPCNumber" + ides[1]).show();
                        $("#" + id).attr("disabled", true);
                        $("#txtNewReleaseUpcNum" + ides[1]).val("");
                        $("#txtAdditionalNewReleaseUpcNum" + ides[1]).val("");
                        $("#txtNewReleaseUpcNum" + ides[1]).attr("disabled", true);
                        $("#txtNewReleaseUpcNum" + ides[1]).hide();
                        $("#txtAdditionalNewReleaseUpcNum" + ides[1]).hide();
                        $("#" + id).addClass('chkUpcNumberdisabled');
                        $("#" + id).removeClass('chkUpcNumber');

                    });

                    var isValueExist = true;

                    var statusType = $("#hdnStatusType").val();
                    $('.hdnUPCNumberCls').each(function () {

                        var id = $(this).attr('id');
                        var ides = id.replace('hdnUpcNumber', '');
                        var releaseId = $('#hdnReleaseId' + ides).val();
                        var archiveFlag = $('#hdnArchiveFlag' + ides).val();
                        var musicType = $("#ddlMusicType_" + ides).val()
                        if ($(this).val() == "" && (userRole == "RCCAdmin" || (userRole == "Requestor" && musicType != "1")) && releaseId != "" && releaseId != "0" && archiveFlag != "Y") {

                            isValueExist = false;
                            return;
                        }
                    });
                    if ((statusType == "5" || statusType == "2" || statusType == "6") && isValueExist == false) {
                        $("#btnAllocateUPC").show();
                    }
                    else {
                        $("#btnAllocateUPC").hide();
                    }
                }
                $('#loadingDivPA').hide();
                if (errorMsg != "") {
                    displayDialog('Regular Project', errorMsg);
                }

                var digitalVal = $('#CreateRegularProjectForm').serialize();

                $.post('/GCS/ClearanceProject/EnableDisblePushToR2Btn', digitalVal, function (data) {

                    var enableBtn = data;

                    if (enableBtn == "True") {
                        $('#btnPushToR2').show();
                    }
                    else {
                        $('#btnPushToR2').hide();
                    }
                });

                if (userRole == "RCCAdmin") {
                    SaveRCCHandlerAfterUPCAllocation();
                }
            }
        });
    });

    $('#screenTabs li a').click(function (event) {        
        if (isTheReleaseTabContext()) {
            closeAllTheAutocompleteReleaseTab();
        }
    });
});