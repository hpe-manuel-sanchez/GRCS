var existingReleaseRowId = "";
var releaseRowId = "";

function OnDeleteClickRelease(rowId) {
    if (true) {
        var Clr_Project_Id = $('#ClrProjectId' + rowId).val();
        var Clr_Release_Id = $('#ReleaseId' + rowId).val();
        $('#tr' + rowId).hide();

        $("#Archive" + rowId).val('Y');

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/RegularProjectRemoveRelease', digitalVal, function (data) {
        });
    }
    return false;
}

function GetTracks(ReleaseId, rowId) {
    var values =
        {
            "clearanceRelease.R2ReleaseId": ReleaseId,
            "clearanceRelease.RowId": rowId
        };
    document.getElementById('LoaderTracks' + rowId + '').className = 'jtable-busy-message';

    if ($.trim($('#divTracks' + rowId).text()) == "") {
        $('#divTracks' + rowId).load('/GCS/ClearanceProject/ReleaseRegularTracks', values);
    }
    else {
        document.getElementById('LoaderTracks' + rowId + '').className = '';
    }
}

function isValidObject(objToTest) {
    if (null == objToTest) return false;
    if ("undefined" == typeof (objToTest)) return false;
    return true;
}

/**
* PackageRelease
* @param cntrl
* @return 
*/
function ViewComponentExistingclass(cntrl) {
    var SplittedVal = cntrl.title.toString().split('-')
    var values =
        {
            "clearanceRelease.R2ReleaseId": SplittedVal[0],
            "clearanceRelease.Upc": SplittedVal[1]
        };
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

function ChangePriceLevel(e, contId, index) {
    var RlsId = $(contId).attr('id');
    if ($("#chkMultiArtist").is(':checked') == false) {
        if ($("#ddlPriceLevel-Regular" + index).val() == 1) { // Top
            $("#txtICLALevel-Regular" + index).val('Top');
        }

        if ($("#ddlPriceLevel-Regular" + index).val() == 2) { // Budget
            $("#txtICLALevel-Regular" + index).val('Budget');
        }
        if ($("#ddlPriceLevel-Regular" + index).val() == 3) { // Mid
            $("#txtICLALevel-Regular" + index).val('Mid');
        }
    }
    else { //if multi artist flag is selected in PI
        if (($("#ddlPriceLevel-Regular" + index).val() == 1) || ($("#ddlPriceLevel-Regular" + index).val() == 3)) { // Top or //Mid
            $("#txtICLALevel-Regular" + index).val('Multi-Artist');
        }
        else if ($("#ddlPriceLevel-Regular" + index).val() == 2) { // Budget
            $("#txtICLALevel-Regular" + index).val('Budget');
        }
    }
}

function triggerRegularExistingAuditHistory(releaseIndex) {
    var AuditTypeId;
    var SelectedItemIds = [];
    var displayTitle = '';

    AuditTypeId = AuditObjectType.RegularNonTraditionalProjectReleaseExistsAuditHistory;

    if ($("#ProjectRefId").val().length > 0) {
        displayTitle = $("#ProjectRefId").val();
    }

    if ($('#hdnUPCNumber-release' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnUPCNumber-release' + releaseIndex).val();
    }

    if ($('#hdnReleaseTitle' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnReleaseTitle' + releaseIndex).val();
    }

    if ($('#hdnVersionTitle-release' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnVersionTitle-release' + releaseIndex).val();
    }
    SelectedItemIds.push($('#ReleaseId' + releaseIndex).val());
    for (i = 0; i < $('#hdnReleaseIdsCount').val() ; i++) {
        SelectedItemIds.push($('#hdnClrReleaseIds' + releaseIndex + i).val());
    }
    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
    return false;
}

/**
* For read only page View existing components functionality for new release
* @param 
* @return 
*/
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

function triggerRegularNewAuditHistory(releaseIndex) {
    var AuditTypeId;
    var SelectedItemIds = [];
    var displayTitle = '';
    AuditTypeId = AuditObjectType.RegularNonTraditionalProjectReleaseNewAuditHistory;

    if ($("#ProjectRefId").val().length > 0) {
        displayTitle = $("#ProjectRefId").val();
    }

    if ($('#hdnUPCNumber-release' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnUPCNumber-release' + releaseIndex).val();
    }

    if ($('#hdnReleaseTitle' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnReleaseTitle' + releaseIndex).val();
    }

    if ($('#hdnVersionTitle-release' + releaseIndex).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnVersionTitle-release' + releaseIndex).val();
    }
    SelectedItemIds.push($('#ReleaseId' + releaseIndex).val());
    for (i = 0; i < $('#hdnReleaseIdsCount').val() ; i++) {
        SelectedItemIds.push($('#hdnClrReleaseIds' + releaseIndex + i).val());
    }
    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
    return false;
}

$(document).ready(function () {
    //Start - For new release regular read only page.
    if ($('#FlagReleaseNewOrExisting').val() == "New") {
        if ($('#ReleaseModelCount').val() > 0) {
            for (k = 0; k < $('#ReleaseModelCount').val() ; k++) {
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
            }
        }
    }
    //End - For new release regular read only page.

    $('#btnAllocateUPC').hide();
    // set watermark
    jQuery("#txtUPC").watermark("UPC");
    jQuery("#txtArtistName").watermark("Artist Name");
    jQuery("#txtReleaseTitle").watermark("Release Title");
    jQuery("#txtArtistID").watermark("Artist ID");
    jQuery("#txtR2ProjectId").watermark("R2 Project ID");
    // in postback UI validations should be maintained as it is, based on request type tab controls
    jQuery(".textarea").watermark("Comments");
    jQuery(".tbtextARea").watermark("Comments");

    setIclaSuggestedFeeSet();
    CheckRequestTypeData();
    showReleaseResubmitMsg();

    $("#tblRelease input[type=checkbox]").each(function () {
        $(this).click(function () {
            var $this = $(this);
            var id = $(this).attr('id');
            var inu = id.split('-')[1];
            var idICLA = $('#ddlDevICLALevel-' + inu);
            var commentsSalesChnl = $('#txtComments-' + inu);
            var mandatoryId = $('#mandatory-' + inu);
            var ddlMandatoryId = $('#mandatoryDDL-' + inu);
            // $this will contain a reference to the checkbox
            if ($this.is(':checked')) {
                // the checkbox was checked
                idICLA.removeAttr('disabled');
                commentsSalesChnl.removeAttr('disabled');
                mandatoryId.show();
                ddlMandatoryId.show();
            } else {
                // the checkbox was unchecked
                idICLA.attr('disabled', 'disabled');
                commentsSalesChnl.attr('disabled', 'disabled');
                idICLA.prop('selectedIndex', 0);
                commentsSalesChnl.val('');
                commentsSalesChnl.removeClass('input-validation-error');
                mandatoryId.hide();
                ddlMandatoryId.hide();
            }
        });
    });

    $("#tblRelease input[type=checkbox]").each(function () {
        var $this = $(this);
        var id = $(this).attr('id');
        var inu = id.split('-')[1];
        var idICLA = $('#ddlDevICLALevel-' + inu);
        var commentsSalesChnl = $('#txtComments-' + inu);
        // $this will contain a reference to the checkbox
        if ($this.is(':checked')) {
            if (inu.indexOf("Non") >= 0) {
                var rowId = inu.toString().substring(3, inu.length);
                if ($("#hdnNonTrad1" + rowId).val() == "False" || $("#hdnNonTrad1" + rowId).val() == "" || $("#hdnNonTrad1" + rowId).val() == null) {
                    idICLA.attr('disabled', 'disabled');
                    commentsSalesChnl.attr('disabled', 'disabled');
                    commentsSalesChnl.removeClass('input-validation-error');
                }
            }
            else {
                // the checkbox was checked
                idICLA.removeAttr('disabled');
                commentsSalesChnl.removeAttr('disabled');
            }
        } else {
            // the checkbox was unchecked
            idICLA.attr('disabled', 'disabled');
            commentsSalesChnl.attr('disabled', 'disabled');
            commentsSalesChnl.removeClass('input-validation-error');
        }
    });

    $('#tabs').tabs({
        select: function (event, ui) {
            var selected = ui.panel.id;
            switch (selected) {
                case 'tabs-1':
                    $('#cmbReleaseNewOrExisting').attr('disabled', true);
                    //For Audit history link
                    $('#regularProjInfoAuditLink').show();
                    $('#regularReqTypeAuditLink').hide();
                    break;

                case 'tabs-2':

                    if ($('#txtProjectReferenceId').val() == "") {
                        $('#cmbReleaseNewOrExisting').attr('disabled', false);
                    }
                        // when save clicked and release added - disable permanenlty
                    else if ($('#Archive0').val() != null && $('#txtProjectReferenceId').val() != "") {
                        if ($('#Archive0').val() != 'Y') {
                            $('#cmbReleaseNewOrExisting').attr('disabled', true);
                        }
                        else {
                            $('#cmbReleaseNewOrExisting').attr('disabled', false);
                        }
                    }
                    else {
                        $('#cmbReleaseNewOrExisting').attr('disabled', false);
                    }
                    //For Audit history link
                    $('#regularReqTypeAuditLink').show();
                    $('#regularProjInfoAuditLink').hide();
                    break;

                case 'tabs-3':
                    $('#cmbReleaseNewOrExisting').attr('disabled', true);
                    //For Audit history link
                    $('#regularProjInfoAuditLink').hide();
                    $('#regularReqTypeAuditLink').hide();
                    setIclaSuggestedFeeSet();
                    CheckRequestTypeData();
                    break;

                case 'tabs-4':
                    //For Audit history link
                    $('#regularProjInfoAuditLink').hide();
                    $('#regularReqTypeAuditLink').hide();
                    break;

                default:
                    //default script on load
                    break;
            }
        }
    });

    function setIclaSuggestedFeeSet() {
        $("#tblRelease input[type=radio]").each(function () {
            var Id = $(this).attr("id");
            var rowId = Id.toString().substring(11, Id.length);

            if ($("#hdnNonTrad1" + rowId).val() == "True") {
                $("#rdoNonTrad1" + rowId).val(true)
                $("#rdoNonTrad1" + rowId).attr('checked', true);

                NonTraditionalDiv(Id);
            }
            else if ($("#hdnNonTrad2" + rowId).val() == "True") {
                $("#rdoNonTrad2" + rowId).val(true)
                $("#rdoNonTrad2" + rowId).attr('checked', true);
                NonTraditionalDivSuggested(Id);
            }
            else {
                //disable all controls
                disableAllControl(rowId);
            }
        });
    }

    function CheckRequestTypeData() {
        $("#tblRelease div").each(function () {
            var id = $(this).attr('id');

            if (id != null) {
                if (id.indexOf("trRegular") >= 0) {
                    //check if regular is selected
                    if (($("#chkRegularRetail").is(':checked')) && (($("#chkTVRadioBreakICLA").is(':checked') == false) && ($("#chkPriceReduction").is(':checked') == false))) {
                        $(this).show();
                        $("#" + id + "Is_Regular").val(true);
                    }
                    else {
                        $(this).hide();
                        $("#" + id + "Is_Regular").val(false);
                        var chkId = $(this).attr('id');
                        var chkRowId = chkId.toString().substring(9, chkId.length);

                        $('#chkIsDeviatedICLALevel-Regular' + chkRowId).attr('checked', false);
                        $('#txtComments-Regular' + chkRowId).val('');
                        $('#txtComments-Regular' + chkRowId).attr('disabled', true);
                        $('#ddlDevICLALevel-Regular' + chkRowId).val('');
                        $('#ddlDevICLALevel-Regular' + chkRowId).attr('disabled', true);
                        $('#ddlPriceLevel-Regular' + chkRowId).val('');
                        $('#mandatory-Regular' + chkRowId).hide();
                        $('#mandatoryDDL-Regular' + chkRowId).hide();
                    }

                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(9, Id.length);

                    var ctrlRegularRetail = $("#txtICLALevel-Club" + rowId);

                    if (($("#chkMultiArtist").is(':checked') == false)) {
                        if ($("#ddlPriceLevel-Regular" + rowId).val() == 1 || $("#ddlPriceLevel-Regular" + rowId).val() == 'Top') { // Top
                            if (ctrlRegularRetail[0].tagName == 'INPUT')
                                $("#txtICLALevel-Regular" + rowId).val('Top');

                            else
                                $("#txtICLALevel-Regular" + rowId).text('Top');
                        }
                        if ($("#ddlPriceLevel-Regular" + rowId).val() == 3 || $("#ddlPriceLevel-Regular" + rowId).val() == 'Mid') { // Mid
                            if (ctrlRegularRetail[0].tagName == 'INPUT')
                                $("#txtICLALevel-Regular" + rowId).val('Mid');

                            else
                                $("#txtICLALevel-Regular" + rowId).text('Mid');
                        }
                        if ($("#ddlPriceLevel-Regular" + rowId).val() == 2 || $("#ddlPriceLevel-Regular" + rowId).val() == 'Budget') { // Budget
                            if (ctrlRegularRetail[0].tagName == 'INPUT')
                                $("#txtICLALevel-Regular" + rowId).val('Budget');

                            else
                                $("#txtICLALevel-Regular" + rowId).text('Budget');
                        }
                    }
                    if (($("#chkMultiArtist").is(':checked') == true)) {
                        if ($("#ddlPriceLevel-Regular" + rowId).val() == 1 || $("#ddlPriceLevel-Regular" + rowId).val() == 3 || $("#ddlPriceLevel-Regular" + rowId).val() == 'Top' || $("#ddlPriceLevel-Regular" + rowId).val() == 'Mid') { // Top or Mid
                            if (ctrlRegularRetail[0].tagName == 'INPUT')
                                $("#txtICLALevel-Regular" + rowId).val('Multi-Artist');

                            else
                                $("#txtICLALevel-Regular" + rowId).text('Multi-Artist');
                        }

                        if ($("#ddlPriceLevel-Regular" + rowId).val() == 2 || $("#ddlPriceLevel-Regular" + rowId).val() == 'Budget') { // Budget
                            if (ctrlRegularRetail[0].tagName == 'INPUT')
                                $("#txtICLALevel-Regular" + rowId).val('Budget');

                            else
                                $("#txtICLALevel-Regular" + rowId).text('Budget');
                        }
                    }
                }

                // check if TV is selected
                if (id.indexOf("trTV") >= 0) {
                    if ($("#chkTVRadioBreakICLA").is(':checked')) {
                        $(this).show();

                        $("#" + id + "Is_TV").val(true);
                    }
                    else {
                        var chkId = $(this).attr('id');
                        var chkRowId = chkId.toString().substring(4, chkId.length);
                        $('#chkIsDeviatedICLALevel-TVRadio' + chkRowId).attr('checked', false);
                        $('#txtComments-TVRadio' + chkRowId).val('');
                        $('#ddlDevICLALevel-TVRadio' + chkRowId).val('');
                        $('#txtExPPD-TVRadio' + chkRowId).val('');
                        $('#txtEstRetail-TVRadio' + chkRowId).val('');
                        $('#mandatory-TVRadio' + chkRowId).hide();
                        $('#mandatoryDDL-TVRadio' + chkRowId).hide();

                        $(this).hide();
                        $("#" + id + "Is_TV").val(false);
                    }
                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(4, Id.length);
                    var ctrlTVRadio = $("#txtICLALevel-TVRadio" + rowId);
                    if (ctrlTVRadio[0].tagName == 'INPUT')
                        $("#txtICLALevel-TVRadio" + rowId).val('TV Break ICLA');
                    else
                        $("#txtICLALevel-TVRadio" + rowId).text('TV Break ICLA');
                }

                // check if club is selected
                if (id.indexOf("trClub") >= 0) {
                    if ($("#chkClub").is(':checked')) {
                        $(this).show();
                        $("#" + id + "Is_Club").val(true);
                    }
                    else {
                        var chkId = $(this).attr('id');
                        var chkRowId = chkId.toString().substring(6, chkId.length);
                        $('#chkIsDeviatedICLALevel-Club' + chkRowId).attr('checked', false);
                        $('#txtComments-Club' + chkRowId).val('');
                        $('#txtICLALevel-Club' + chkRowId).val('');
                        $('#txtComments-Club' + chkRowId).attr('disabled', true);
                        $('#ddlDevICLALevel-Club' + chkRowId).prop('selectedIndex', 0);
                        $('#ddlDevICLALevel-Club' + chkRowId).attr('disabled', true);
                        $('#mandatory-Club' + chkRowId).hide();
                        $('#mandatoryDDL-Club' + chkRowId).hide();
                        $(this).hide();
                        $("#" + id + "Is_Club").val(false);
                    }
                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(6, Id.length);

                    var ctrlPriceClub = $("#ddlPriceLevel-Club" + rowId);
                    if (ctrlPriceClub[0].tagName == 'SELECT') {
                        $("#ddlPriceLevel-Club" + rowId).val(1); // Club
                    }
                    else {
                        $("#ddlPriceLevel-Club" + rowId).text('Club'); // Club
                    }

                    var ctrlClub = $("#txtICLALevel-Club" + rowId);

                    if (ctrlClub[0].tagName == 'INPUT')
                        $("#txtICLALevel-Club" + rowId).val('Club'); // Club
                    else
                        $("#txtICLALevel-Club" + rowId).text('Club'); // Club
                }

                // check if club is selected
                if (id.indexOf("trPromo") >= 0) {
                    if ($("#chkPromotional").is(':checked')) {
                        $(this).show();
                        $("#" + id + "Is_Promo").val(true);
                    }
                    else {
                        var Id = $(this).attr('id');
                        var chkRowId = Id.toString().substring(7, Id.length);
                        $('#chkIsDeviatedICLALevel-Promotional' + chkRowId).attr('checked', false);
                        $('#txtComments-Promotional' + chkRowId).val('');
                        $('#ddlDevICLALevel-Promotional' + chkRowId).prop('selectedIndex', 0);
                        $('#ddlDevICLALevel-Promotional' + chkRowId).attr('disabled', true);
                        $('#txtComments-Promotional' + chkRowId).attr('disabled', true);
                        $('#mandatory-Promotional' + chkRowId).hide();
                        $('#mandatoryDDL-Promotional' + chkRowId).hide();

                        $(this).hide();
                        $("#" + id + "Is_Promo").val(false);
                    }
                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(7, Id.length);

                    var ctrlPricePromotional = $("#ddlPriceLevel-Promotional" + rowId);
                    if (ctrlPricePromotional[0].tagName == 'SELECT') {
                        $("#ddlPriceLevel-Promotional" + rowId).val(1); // NIL
                    }
                    else {
                        $("#ddlPriceLevel-Promotional" + rowId).text('NIL'); // NIL
                    }

                    var ctrlPromo = $("#txtICLALevel-Promotional" + rowId);
                    if (ctrlPromo[0].tagName == 'INPUT')
                        $("#txtICLALevel-Promotional" + rowId).val('NIL'); // Club
                    else
                        $("#txtICLALevel-Promotional" + rowId).text('NIL'); // Club
                }

                // check if non traditional is selected
                if (id.indexOf("trPrice") >= 0) {
                    if ($("#chkPriceReduction").is(':checked')) {
                        $(this).show();
                        $("#" + id + "Is_Price").val(true);
                    }
                    else {
                        var Id = $(this).attr('id');
                        var chkRowId = Id.toString().substring(7, Id.length);

                        $('#ddlDevICLALevel-PriceReduction' + chkRowId).prop('selectedIndex', 0);
                        $('#txtComments-PriceReduction' + chkRowId).attr('disabled', true);
                        $('#txtComments-PriceReduction' + chkRowId).val('');
                        $('#chkIsDeviatedICLALevel-PriceReduction' + chkRowId).attr('checked', false);
                        $('#mandatory-PriceReduction' + chkRowId).hide();
                        $('#mandatoryDDL-PriceReduction' + chkRowId).hide();

                        $(this).hide();
                        $("#" + id + "Is_Price").val(false);
                    }
                    var Id = $(this).attr('id');
                    var rowId = Id.toString().substring(7, Id.length);
                    var ctrlPriceReductionPriceLevel = $("#ddlPriceLevel-PriceReduction" + rowId);
                    if (ctrlPriceReductionPriceLevel[0].tagName == 'SELECT')
                        $("#ddlPriceLevel-PriceReduction" + rowId).val($("#cboRequestPriceList").val());
                    else {
                        $("#ddlPriceLevel-PriceReduction" + rowId).text($("#cboRequestPriceList").val());
                        $("#ddlPriceLevel-PriceReduction" + rowId).val($("#cboRequestPriceList").val());
                    }

                    var ctrlPriceReduction = $("#txtICLALevel-PriceReduction" + rowId);
                    if (($("#chkMultiArtist").is(':checked') == false)) {
                        if ($("#ddlPriceLevel-PriceReduction" + rowId).val() == '') { // Empty
                            $("#txtICLALevel-PriceReduction" + rowId).val('');
                        }
                        if ($("#ddlPriceLevel-PriceReduction" + rowId).val() == 3 || $("#ddlPriceLevel-PriceReduction" + rowId).val() == 'Mid') { // Mid
                            if (ctrlPriceReduction[0].tagName == 'INPUT')
                                $("#txtICLALevel-PriceReduction" + rowId).val('Mid');
                            else
                                $("#txtICLALevel-PriceReduction" + rowId).text('Mid');
                        }
                        if ($("#ddlPriceLevel-PriceReduction" + rowId).val() == 2 || $("#ddlPriceLevel-PriceReduction" + rowId).val() == 'Budget') { // Budget
                            if (ctrlPriceReduction[0].tagName == 'INPUT')
                                $("#txtICLALevel-PriceReduction" + rowId).val('Budget');
                            else
                                $("#txtICLALevel-PriceReduction" + rowId).text('Budget');
                        }
                    }
                    if (($("#chkMultiArtist").is(':checked') == true)) {
                        if ($("#ddlPriceLevel-PriceReduction" + rowId).val() == '') { // Empty
                            $("#txtICLALevel-PriceReduction" + rowId).val('');
                        }
                        if ($("#ddlPriceLevel-PriceReduction" + rowId).val() == 3 || $("#ddlPriceLevel-PriceReduction" + rowId).val() == 'Mid') { //  Mid
                            if (ctrlPriceReduction[0].tagName == 'INPUT')
                                $("#txtICLALevel-PriceReduction" + rowId).val('Multi-Artist');
                            else
                                $("#txtICLALevel-PriceReduction" + rowId).text('Multi-Artist');
                        }
                        if ($("#ddlPriceLevel-PriceReduction" + rowId).val() == 2 || $("#ddlPriceLevel-PriceReduction" + rowId).val() == 'Budget') { // Budget
                            if (ctrlPriceReduction[0].tagName == 'INPUT')
                                $("#txtICLALevel-PriceReduction" + rowId).val('Budget');
                            else
                                $("#txtICLALevel-PriceReduction" + rowId).text('Budget');
                        }
                    }
                }

                // check if non traditional is selected
                if (id.indexOf("trNon") >= 0) {
                    if ($("#chkNonTrditional").is(':checked')) {
                        $(this).show();
                        $("#" + id + "Is_Non").val(true);
                    }
                    else {
                        var Id = $(this).attr('id');
                        var chkRowId = Id.toString().substring(5, Id.length);
                        $('#chkIsDeviatedICLALevel-Non' + chkRowId).attr('checked', false);
                        $('#txtSellingPrice-Non' + chkRowId).val('');
                        $('#txtAccounting-Non' + chkRowId).val('');
                        $('#txtResourceFee-Non' + chkRowId).val('');
                        $('#ddlDevICLALevel-Non' + chkRowId).prop('selectedIndex', 0);
                        $('#ddlDevICLALevel-Non' + chkRowId).attr('disabled', true);
                        $('#txtComments-Non' + chkRowId).val('');
                        $('#txtComments-Non' + chkRowId).attr('disabled', true);
                        $('#mandatory-Non' + chkRowId).hide();
                        $('#mandatoryDDL-Non' + chkRowId).hide();

                        $(this).hide();
                        $("#" + id + "Is_Non").val(false);
                    }
                }
            }
        });
    };

    $(".radioICLAclass").click(function (event) {
        event.stopPropagation();
        var Id = $(this).attr("id");
        NonTraditionalDiv(Id);
    });

    function disableAllControl(rowId) {
        $("#txtResourceFee-Non" + rowId).attr('disabled', 'disabled');
        $("#txtICLALevel-Non" + rowId).attr('disabled', 'disabled');
        $("#chkIsDeviatedICLALevel-Non" + rowId).attr('disabled', 'disabled');
        $("#txtInvoicePrice-Non" + rowId).attr('disabled', 'disabled');
        $("#txtAccounting-Non" + rowId).attr('disabled', 'disabled');
        $("#txtSellingPrice-Non" + rowId).attr('disabled', 'disabled');
        $("#txtDeemedPPD-Non" + rowId).attr('disabled', 'disabled');

        // hide and show td for disabled and enabled controls
        $("#tdResFee" + rowId).hide();
        $("#tdResFeeVal" + rowId).hide();
        $("#tdICLAl" + rowId).hide();
        $("#divNonICLA" + rowId).hide();
        $("#tdDeviated" + rowId).hide();
        $("#tdNonDeactiveICLAExisting" + rowId).hide();
        $("#tdNonCommentsExisting" + rowId).hide();
        $("#tdInvoiePrice" + rowId).hide();
        $("#tdInvoicePriceVal" + rowId).hide();
        $("#tdICLAAccBase" + rowId).hide();
        $("#tdICLAAccBaseVal" + rowId).hide();
        $("#tdSelling" + rowId)[0].style.visibility = "hidden";
        $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";
        $("#testfor" + rowId).hide();
        $("#tdDeemed" + rowId).hide();
        $("#tdDeemedVal" + rowId).hide();
    }

    function NonTraditionalDiv(Id) {
        var rowId = Id.toString().substring(11, Id.length);
        $("#hdnNonTrad1" + rowId).val('True');
        $("#hdnNonTrad2" + rowId).val('False');

        //For removing highlihgt
        $("#dvNonTraditionalRelease" + rowId).removeClass('input-validation-error');

        $("#txtResourceFee-Non" + rowId).val('');
        $("#txtResourceFee-Non" + rowId).attr('disabled', 'disabled');
        $("#tdResFee" + rowId).hide();
        $("#tdResFeeVal" + rowId).hide();

        $("#tdICLAl" + rowId).show();
        $("#divNonICLA" + rowId).show();

        $("#chkIsDeviatedICLALevel-Non" + rowId).removeAttr('disabled');

        $("#tdDeviated" + rowId).show();
        $("#tdNonDeactiveICLAExisting" + rowId).show();
        $("#tdNonCommentsExisting" + rowId).show();

        var ctrlNonTraditional = $("#txtICLALevel-Non" + rowId);
        if ((!$("#chkPartwork").is(':checked')) && (!$("#chkKiosk").is(':checked')) && ($("#chkMailOrder").is(':checked')) && (!$("#chkInternet").is(':checked')) && (!$("#chkDirectResponse").is(':checked')) && (!$("#chkEducational").is(':checked')) && (!$("#chkPremium").is(':checked')) && (!$("#chkGiveAwayFreeOfCharge").is(':checked')) && (!$("#chkOther").is(':checked'))) {
            if (ctrlNonTraditional[0].tagName == 'INPUT')
                $("#txtICLALevel-Non" + rowId).val('Mail Order');

            else
                $("#txtICLALevel-Non" + rowId).text('Mail Order');
        } else {
            if (ctrlNonTraditional[0].tagName == 'INPUT')
                $("#txtICLALevel-Non" + rowId).val('Budget');

            else
                $("#txtICLALevel-Non" + rowId).text('Budget');
        }

        if ($('#cmbManByUMG :selected').val() == 'Yes') {
            $("#txtInvoicePrice-Non" + rowId).removeAttr('disabled');
            $("#tdInvoiePrice" + rowId).show();
            $("#tdInvoicePriceVal" + rowId).show();

            $("#txtSellingPrice-Non" + rowId).attr('disabled', 'disabled');
            $("#tdSelling" + rowId)[0].style.visibility = "hidden";
            $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";
            $("#testfor" + rowId).hide();

            if ($("#chkPartwork").is(':checked')) {
                $("#txtAccounting-Non" + rowId).removeAttr('disabled');
                $("#tdICLAAccBase" + rowId).show();
                $("#tdICLAAccBaseVal" + rowId).show();
            }
            else {
                $("#txtAccounting-Non" + rowId).attr('disabled', 'disabled');
                $("#tdICLAAccBase" + rowId).hide();
                $("#tdICLAAccBaseVal" + rowId).hide();
            }

            $("#tdDeemed" + rowId).attr('disabled', 'disabled');
            $("#tdDeemed" + rowId).hide();
            $("#tdDeemedVal" + rowId).hide();
        }
        else if ($('#cmbManByUMG :selected').val() == 'No') {
            if (!$("#chkGiveAwayFreeOfCharge").is(':checked')) {
                $("#txtAccounting-Non" + rowId).removeAttr('disabled');
                $("#tdICLAAccBase" + rowId).show();
                $("#tdICLAAccBaseVal" + rowId).show();

                $("#txtSellingPrice-Non" + rowId).removeAttr('disabled');
                $("#tdSelling" + rowId)[0].style.visibility = "";
                $("#tdSellingVal" + rowId)[0].style.visibility = "";
                $("#testfor" + rowId).show();

                $("#txtDeemedPPD-Non" + rowId).attr('disabled', 'disabled');
                $("#tdDeemed" + rowId).hide();
                $("#tdDeemedVal" + rowId).hide();
            }
            else {
                $("#txtDeemedPPD-Non" + rowId).removeAttr('disabled');
                $("#tdDeemed" + rowId).show();
                $("#tdDeemedVal" + rowId).show();

                $("#txtAccounting-Non" + rowId).attr('disabled', 'disabled');
                $("#tdICLAAccBase" + rowId).hide();
                $("#tdICLAAccBaseVal" + rowId).hide();

                $("#txtSellingPrice-Non" + rowId).attr('disabled', 'disabled');
                $("#tdSelling" + rowId)[0].style.visibility = "hidden";
                $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";
                $("#testfor" + rowId).hide();
            }

            $("#txtInvoicePrice-Non" + rowId).attr('disabled', 'disabled');
            $("#tdInvoiePrice" + rowId).hide();
            $("#tdInvoicePriceVal" + rowId).hide();
        }

        else {
            $("#txtDeemedPPD-Non" + rowId).attr('disabled', 'disabled');
            $("#tdDeemed" + rowId).hide();
            $("#tdDeemedVal" + rowId).hide();

            $("#txtAccounting-Non" + rowId).attr('disabled', 'disabled');
            $("#tdICLAAccBase" + rowId).hide();
            $("#tdICLAAccBaseVal" + rowId).hide();

            $("#txtSellingPrice-Non" + rowId).attr('disabled', 'disabled');
            $("#tdSelling" + rowId)[0].style.visibility = "hidden";
            $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";
            $("#testfor" + rowId).hide();
        }
    }

    $('.radioSuggestedclass').live("click", function () {
        var Id = $(this).attr("id");
        NonTraditionalDivSuggested(Id);
    });

    function NonTraditionalDivSuggested(Id) {
        var rowId = Id.toString().substring(11, Id.length);

        $("#hdnNonTrad2" + rowId).val('True');
        $("#hdnNonTrad1" + rowId).val('False');

        //For removing highlihgt
        $("#dvNonTraditionalRelease" + rowId).removeClass('input-validation-error');

        $("#chkIsDeviatedICLALevel-Non" + rowId).attr('checked', false);
        $("#mandatory-Non" + rowId).hide();
        $("#txtComments-Non" + rowId).val('');
        $("#txtComments-Non" + rowId).attr('disabled', 'disabled');
        $("#ddlDevICLALevel-Non" + rowId).attr('disabled', 'disabled');
        $("#ddlDevICLALevel-Non" + rowId).prop('selectedIndex', 0);
        $("#txtSellingPrice-Non" + rowId).val('');
        $("#txtAccounting-Non" + rowId).val('');

        $("#txtResourceFee-Non" + rowId).removeAttr('disabled');
        $("#tdResFee" + rowId).show();
        $("#tdResFeeVal" + rowId).show();

        $("#tdDeviated" + rowId).hide();
        $("#tdNonDeactiveICLAExisting" + rowId).hide();
        $("#tdNonCommentsExisting" + rowId).hide();

        $('#txtDeemedPPD-Non' + rowId).attr('disabled', 'disabled');
        $("#tdDeemed" + rowId).hide();
        $("#tdDeemedVal" + rowId).hide();

        $('#txtInvoicePrice-Non' + rowId).attr('disabled', 'disabled');
        $("#tdInvoiePrice" + rowId).hide();
        $("#tdInvoicePriceVal" + rowId).hide();

        $('#txtAccounting-Non' + rowId).attr('disabled', 'disabled');
        $("#tdICLAAccBase" + rowId).hide();
        $("#tdICLAAccBaseVal" + rowId).hide();

        $('#txtSellingPrice-Non' + rowId).attr('disabled', 'disabled');
        $("#tdSelling" + rowId)[0].style.visibility = "hidden";
        $("#tdSellingVal" + rowId)[0].style.visibility = "hidden";
        $("#testfor" + rowId).hide();

        $('#txtICLALevel-Non' + rowId).attr('disabled', 'disabled');
        $("#tdICLAl" + rowId).hide();
        $("#divNonICLA" + rowId).hide();
    }

    $('select').change(function () {
        var v = $(this).val();
        var id = $(this).attr('id');
        if (id.indexOf("ddlPriceLevel") >= 0) {
            var inu = id.split('-')[1];
            var startText = id.split('-')[0];
            if (startText == "ddlPriceLevel") {
                var idICLA = '#txtICLALevel-' + inu
                var outIcla = $(idICLA);

                var conditionMulti = $('#chkMultiArtist').val();
                if (conditionMulti == "False") {
                    if (v == 1) {
                        outIcla.val("Top");
                    }
                    else if (v == 2) {
                        outIcla.val("Mid");
                    }
                    else if (v == 3) {
                        outIcla.val("Budget");
                    }
                }
                if (conditionMulti == "True") {
                    if (v == 1 || v == 2) {
                        outIcla.val("Multi-Artist");
                    }
                    else if (v == 3) {
                        outIcla.val("Budget");
                    }
                }
            }
        }
    });

    $("#lnkExpand").click(function (event) {
        $('#tblRelease h5').each(function (index) {
            var id = $(this).find(".details").attr("name");
            if (isValidObject(id)) {
                $('#ClearanceRelease' + id).show();
                $("#tr" + id + "child").show();
            }
        });

        $("#tblRelease").find("h5").removeClass('rightArrow');
        $("#tblRelease").find("h5").addClass('downArrow');
    });

    $("#lnkCollapse").click(function (event) {
        $('#tblRelease h5').each(function (index) {
            var id = $(this).find(".details").attr("name");
            if (isValidObject(id)) {
                $('#ClearanceRelease' + id).hide();
                $("#tr" + id + "child").hide();
            }
        });
        $("#tblRelease").find("h5").removeClass('downArrow');
        $("#tblRelease").find("h5").addClass('rightArrow');
    });

    $(".details").click(function (event) {
        id = $(this).attr("name");
        $('#ClearanceRelease' + id).toggle();
    });

    $(".detailsTracks").click(function () {
        id = $(this).attr("name");
        $('#Trackstbl' + id).toggle();
        $("#ExpandTracks" + id).find("h5").removeClass('rightArrowTracks');
        $("#ExpandTracks" + id).find("h5").addClass('downArrowTracks');
    });

    function SaveExistingRelease() {
        var releaseadded = "";
        $('#ExistingRelease').empty();
        $('#ExistingReleaseFull').empty();
        if ($("#ReleaseModelCount").val() != null) {
            for (k = 0; k < $("#ReleaseModelCount").val() ; k++) {
                if ($("#hdnUPCNumber-release" + k).val() != null) {
                    if ($("#Archive" + k).val() != 'Y') {
                        var addtext = $("#hdnUPCNumber-release" + k).val();
                        var trimmed = addtext.replace(/^\s+|\s+$/g, '');
                        var addVt = $("#hdnartistName" + k).val();
                        var addTitle = $("#hdnReleaseTitle" + k).val();
                        var trimmed = addtext.replace(/^\s+|\s+$/g, '');
                        var VTtrimmed = addVt.replace(/^\s+|\s+$/g, '');
                        var Titletrimmed = addTitle.replace(/^\s+|\s+$/g, '');
                        if ($('#ExistingRelease').text() == "") {
                            $('#ExistingRelease').append(trimmed);
                            $('#ExistingReleaseFull').append(trimmed + '/' + VTtrimmed + '/' + Titletrimmed + '~');
                            existingReleaseRowId = k;
                            $('#tr' + k)[0].style.borderColor = "#222222";
                        }
                        else {
                            $('#ExistingRelease').append(',' + trimmed);
                            $('#ExistingReleaseFull').append(trimmed + '/' + VTtrimmed + '/' + Titletrimmed + '~');
                            existingReleaseRowId = existingReleaseRowId + ',' + k;
                            $('#tr' + k)[0].style.borderColor = "#222222";
                        }
                    }
                }
            }
        }
    }

    $('#SearchRelease').click(function (event) {
        if ($('#loadingDivPA').is(':visible')) {
            return;
        }
        $('#trNoRecordFoundMsg').hide();

        var objSearchPopupExisting = $('<div id="SearchReleasePopup" style="margin:0;padding:0;"></div>');
        $(objSearchPopupExisting).dialog({
            autoOpen: false,
            width: 1000,
            resizable: false,
            title: 'Search For Release',
            modal: true,
            close: function () {
                $(this).remove(); // ensures any form variables are reset.
                $(this).hide();
                $(this).parent().hide();
                $(this).parent().remove();

                $("div[id^='SearchReleasePopup']").remove();
                $("div[id^='SearchReleasePopup']").hide();
                $("div[id^='SearchReleasePopup']").parent().hide();
                $("div[id^='SearchReleasePopup']").parent().remove();
                $('#loadingDivPA').hide();
            },
            open: function (event, ui) {
                var resourcesadded = "";
                $('#ExistingRelease').empty();
                $('#ExistingReleaseFull').empty();

                var l = 0
                if ($("#ReleaseModelCount").val() != null) {
                    for (k = 0; k < $("#ReleaseModelCount").val() ; k++) {
                        if ($("#R2ReleaseId" + k).val() != null) {
                            if ($("#Archive" + k).val() != 'Y') {
                                var addtext = $("#R2ReleaseId" + k).val();
                                var addVt = $("#hdnartistName" + k).val();
                                var addTitle = $("#hdnReleaseTitle" + k).val();
                                var trimmed = addtext.replace(/^\s+|\s+$/g, '');
                                var VTtrimmed = addVt.replace(/^\s+|\s+$/g, '');
                                var Titletrimmed = addTitle.replace(/^\s+|\s+$/g, '');
                                if ($('#ExistingRelease').text() == "") {
                                    $('#ExistingRelease').append(trimmed);
                                    $('#ExistingReleaseFull').append(trimmed + '/' + VTtrimmed + '/' + Titletrimmed);
                                }
                                else {
                                    $('#ExistingRelease').append(',' + trimmed);
                                    $('#ExistingReleaseFull').append(',' + trimmed + '/' + VTtrimmed + '/' + Titletrimmed);
                                }
                            }
                        }
                    }
                }

                $(this).load('/GCS/Search/SearchForRelease');

                var dialogue = $('.ui-dialog')

                dialogue.animate({
                    top: "40px"
                }, 0);
            }
        });
        //Added by Deepak Kaushik on 11/27/2012 to main selected tab on post back.
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        //Added by Deepak Kaushik on 11/27/2012 to main selected tab on post back.

        var UpcNumber = $('#txtUPC').val().substring();
        var ArtistName = $('#txtArtistName').val().substring();
        var ArtistID = $('#txtArtistID').val().substring();
        var ReleaseTitle = $('#txtReleaseTitle').val().substring();
        var R2ProjectId = $('#txtR2ProjectId').val().substring();

        var uniqueSearchFlag = window.document.getElementById("searchUniqueRecord");
        uniqueSearchFlag.value = "";

        if (UpcNumber != '' || ArtistName != '' || ArtistID != '' || ReleaseTitle != '' || R2ProjectId != '') {
            $('#loadingDivPA').show();
            $.ajax({
                type: 'POST',
                url: "/GCS/Search/GetRelease",
                data: {
                    UPC: UpcNumber, ArtistName: ArtistName, ArtistID: ArtistID, ReleaseTitle: ReleaseTitle, R2ProjectId: R2ProjectId
                },
                success: function (result) {
                    var recCount = result.TotalRecordCount;
                    if (recCount > 1) {
                        $('#loadingDivPA').hide();
                        $(objSearchPopupExisting).dialog('open');
                    }
                    else {
                        // duplicacy check for unique record
                        var record = result.Records[0];
                        $("#divErrorMessage").text("");
                        $("#divErrorMessage").hide();
                        $('#divErrorMessage').removeClass('warning');
                        //Added by Jyoti -Validation duplicate check- Unique Resource Search
                        var isExist = false;
                        SaveExistingRelease();
                        releaseRowId = "";
                        $("#spnUpcRelease").empty();
                        var existingReleases = $('#ExistingRelease').text().toString().split(',');
                        var existingReleasesFull = $('#ExistingReleaseFull').text().toString().split('~');
                        var splittedRows = existingReleaseRowId.toString().split(',');

                        if (record != null) {
                            for (var i = 0; i < existingReleases.length; i++) {
                                if ($.trim(existingReleases[i]) == record.Upc) {
                                    isExist = true;
                                    releaseRowId = releaseRowId + ',' + splittedRows[i];
                                    if ($("#spnUpcRelease").text() == "") {
                                        $("#spnUpcRelease").append(existingReleasesFull[i]);
                                        $("#spnUpcRelease")[0].title = existingReleasesFull[i];
                                    }
                                    else {
                                        $("#spnUpcRelease").append("," + existingReleasesFull[i]);
                                        var completestring = "," + existingReleasesFull[i]
                                        $("#spnUpcRelease")[0].title = completestring;
                                    }
                                }
                            }
                            if (isExist == false) {
                                var uniqueSearchFlag = window.document.getElementById("searchUniqueRecord");
                                uniqueSearchFlag.value = "Unique";

                                $('#hdncommand').val("AddtoProject");
                                // added by dhruv for Ajax call

                                var digitalVal = $('#CreateRegularProjectForm').serialize();
                                $.post('/GCS/ClearanceProject/RegularAddButtonForRelease', digitalVal, function (data) {
                                    $('#loadingDivPA').hide();

                                    $('#tabs-3').html(data);
                                });

                                return false;
                            }
                            else {
                                $('#loadingDivPA').hide();
                                //Added by Jyoti -Validation duplicate check- Unique Resource Search
                                $("#trShowMessageResource").show();

                                var splittedRowIds = releaseRowId.split(',');
                                if ($("#ReleaseModelCount").val() != null) {
                                    for (k = 0; k < $("#ReleaseModelCount").val() ; k++) {
                                        for (var k = 0; k < splittedRowIds.length; k++) {
                                            if (splittedRowIds[k] != "") {
                                                if (i == splittedRowIds[k]) {
                                                    $('#tr' + k)[0].style.borderColor = "#ff0000";
                                                }
                                            }
                                        }
                                    }
                                }
                                return false;
                            }
                        }
                        else {
                            $('#trNoRecordFoundMsg').show();
                            $('#trNoRecordFoundMsg').text("No Record Found !");
                        }
                    }
                    $('#loadingDivPA').hide();
                }
            })
        }
        else {
            // If the user clicks the Search button without providing any search criteria
            // Open the Advance release Search popup
            $('#loadingDivPA').hide();
            $(objSearchPopupExisting).dialog('open');
        }
        var dialogue = $('.ui-dialog')

        dialogue.animate({
            top: "40px"
        }, 0);
    });

    $("#tblRelease .contentDv").hide();

    $("#tblRelease").find("h5").removeClass('downArrow');

    $("#tblRelease").find("h5").addClass('rightArrow');

    $("#tblRelease h5").click(function () {
        var id = $(this).find(".details").attr('name');
        if (id != null) {
            var display = document.getElementById('ClearanceRelease' + id).style.display;
            if (display == 'none') {
                document.getElementById('ClearanceRelease' + id).style.display = "";
                $("#tr" + id + "child").show();
                $('#h5' + id).removeClass('rightArrow');
                $('#h5' + id).addClass('downArrow');
            } else {
                document.getElementById('ClearanceRelease' + id).style.display = "none";
                $("#tr" + id + "child").hide();
                $('#h5' + id).removeClass('downArrow');
                $('#h5' + id).addClass('rightArrow');
            }
        }
    });

    $("#tblRelease h3").click(function () {
        id = $(this).find(".detailsTracks").attr('name');
        var display = document.getElementById('Trackstbl(' + id + ')').style.display;
        if (display == 'none') {
            document.getElementById('Trackstbl(' + id + ')').style.display = "";
            $('#h3' + id).removeClass('rightArrow');
            $('#h3' + id).addClass('downArrowTracks');
            var ReleaseId = $('#R2ReleaseId' + id).val();
            GetTracks(ReleaseId, id);
        } else {
            document.getElementById('Trackstbl(' + id + ')').style.display = "none";
            $('#h3' + id).removeClass('downArrowTracks');
            $('#h3' + id).addClass('rightArrow');
        }
    });
});