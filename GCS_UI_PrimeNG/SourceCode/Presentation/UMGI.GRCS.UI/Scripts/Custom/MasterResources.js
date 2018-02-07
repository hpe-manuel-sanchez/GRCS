var existingResourceRowId = "";
var resourceRowId = "";

function OnSensitiveChecked() {

    if ($("#chkSE").is(':checked')) {
        var resourceCount = $("#hdnResourceListCount").val();
        for (var i = 0; i < resourceCount; i++) {
            if ($("chksensivtive" + i) != null) {
                document.getElementById('chksensivtive' + i).checked = true;
            }
        }
        $('.check').prop('checked', true);
        $('.check').attr('disabled', true);
    }
    else {
        var resourceCount = $("#hdnResourceListCount").val();
        for (var i = 0; i < resourceCount; i++) {
            if ($("chksensivtive" + i) != null) {
                document.getElementById('chksensivtive' + i).checked = false;
            }
        }
        $('.check').prop('checked', false);
        $('.check').attr('disabled', false);
    }
}

function setTitleInHidden(Idval) {
    $('#hdnResourceTitle' + Idval).val(document.getElementById("MasterProjectDetails_ClearanceResource_" + Idval + "__Title").value);
}

function setVersionTitleInHidden(Idval) {
    $('#hdnVersionTitle' + Idval).val(document.getElementById("MasterProjectDetails_ClearanceResource_" + Idval + "__VersionTitle").value);
}

function OpenAdvanceResourceSearchPopup() {
    $("#SearchDialog").dialog("open");
}

function SaveExistingResources() {
    var resourcesadded = "";
    $('#ExistingResources').empty();
    var table = document.getElementById('tblResource'),
            rows = table.getElementsByTagName('tr'), i, j, cells;
    var l = 0
    for (i = 0, j = rows.length; i < j; ++i) {
        cells = rows[i].getElementsByTagName('td');

        for (k = 0; k < cells.length; k++) {
            if (cells[k].id == "hdnFields " + l) {//Added for UC-055
                if ($('#hdnArchiveFlag' + l).val() == "N") {
                    if ($('#ExistingResources').text() == "") {
                        $('#ExistingResources').append($.trim($('#hdnR2ResourceId' + l).val())); //Added for UC-055
                        existingResourceRowId = l;
                        $('#' + l)[0].style.borderColor = "#222222";
                        l = l + 1;
                    }
                    else {
                        $('#ExistingResources').append(',' + $.trim($('#hdnR2ResourceId' + l).val())); //Added for UC-055
                        existingResourceRowId = existingResourceRowId + ',' + l;
                        $('#' + l)[0].style.borderColor = "#222222";
                        l = l + 1;
                    }
                }
                else {
                    l = l + 1;
                }
            }
        }
    }
}

function OnDeleteClick(rowid) {
    if (true) {

        var ClearanceResourceId = $('#hdnClearanceResourceId' + rowid).val();
        $('#' + rowid).hide();

        // added by dhruv for removing from database on Remove click
        $('#hdnClearanceResourceId').val(ClearanceResourceId);
        $('#hdnArchiveFlag' + rowid).val("Y");
        //territory remove
        var tempI = (rowid + 3);
        $("#selectedCountries_" + tempI).html('');
        $("#UnselectedCountries_" + tempI).html('');
        $('#hdnterritoryDetailsForSave_' + tempI).val('');
        var digitalVal = $('#CreateMasterProject').serialize();
        $.post('/GCS/ClearanceProject/MasterProjectRemoveResource', digitalVal + "&ClearanceResourceId=" + ClearanceResourceId, function (data) {
        });
    }
    return false;
}

function OnExpandAllClick(objType) {

    var resourceCount = $("#hdnResourceListCount").val();
    for (var i = 0; i < resourceCount; i++) {
        if (objType == 'c') {
            $("#contentDv" + i).hide();
            $('#Header' + i).removeClass('downArrow');
            $('#Header' + i).addClass('rightArrow');
        }
        else {
            $("#contentDv" + i).show();
            $('#Header' + i).removeClass('rightArrow');
            $('#Header' + i).addClass('downArrow');
        }
    }
    return false;
}

function OpenAdvanceResourceSearchandUpdatePopup(SelectResoruceID) {
    var selectedResourceId = parseInt(SelectResoruceID) + 1;

    var value =
        {
            "ArtistId": selectedResourceId

        };
    $('#SearchDialog').empty();
    var objDialog = $('#SearchDialog')
    objDialog.dialog({
        resizable: false,
        width: 1000,
        title: 'Search for Resources',

        modal: true,
        open: function () {
            SaveExistingResources()
            $(this).load("/GCS/Search/AdvanceResourceSearchUpdatePopup", value);
        }
    });
    objDialog.dialog('open');
    var dialogue = $('.ui-dialog');
    dialogue.animate({ top: "40px" }, 0);
}

function TextAreaValidationForMax(id) {

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

function OnlyNumericForSuggestedFee(event, ctlid) {

    if (event.shiftKey) {
        return false
    }

    var v_text = $(ctlid).val();
    var _index = doGetCaretPosition(ctlid);

    if (window.event) { var charCode = window.event.keyCode; }
    else if (event) { var charCode = event.which; }
    else { return true; }

    if (charCode == 8) {
        if (v_text.charAt(_index - 1) == ".") {
            if (v_text.length == 17) {
                $(ctlid).val(v_text.substring(0, 14));
                return false;
            }
            else if (v_text.length == 16) {
                $(ctlid).val(v_text.substring(0, 13));
                return false;
            }
        }
    }
    else if (charCode == 46) {
        if (v_text.charAt(_index) == ".") {
            if (v_text.length == 17) {
                $(ctlid).val(v_text.substring(0, 14));
                return false;
            }
            else if (v_text.length == 16) {
                $(ctlid).val(v_text.substring(0, 13));
                return false;
            }
        }
    }
    if ((charCode == 37) || (charCode == 46) || (charCode == 36) || (charCode == 35) || (charCode == 8) || (charCode == 39)) {
        return true;
    }

    if ((v_text.indexOf(".") == -1) && (charCode == 190 || charCode == 110) && ((_index == v_text.length - 2) || (_index == v_text.length - 1) || (_index == v_text.length))) {
        return true;
    }

    if ((v_text.indexOf(".") > 0) && (charCode == 190 || charCode == 110)) {
        return false;
    }

    if (v_text.length == 14 && charCode != 190 && charCode != 110 && v_text.indexOf(".") == -1) {
        if ((document.selection) && (document.selection.createRange().text.length > 0)) {
            return true;
        }
        if ((document.getSelection) && (document.getSelection.toString().length > 0)) {
            return true;
        }
        return false;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 96 || charCode > 105))
    { return false; }

    if (v_text.indexOf(".") > 0) {
        if (_index <= v_text.indexOf(".") && v_text.substring(0, v_text.indexOf(".")).length < 14) {
            return true;
        }
        var charAfterdot = v_text.substring(v_text.indexOf("."), v_text.length - 1);
        if (charAfterdot.length >= 2) {
            if ((document.selection) && (document.selection.createRange().text.length > 0)) {
                return true;
            }
            if ((document.getSelection) && (document.getSelection.toString().length > 0)) {
                return true;
            }
            return false;
        }

    }

    if (v_text.length == 17) {
        return false;
    }
    return true;

}

function doGetCaretPosition(oField) {

    // Initialize
    var iCaretPos = 0;

    // IE Support
    if (document.selection) {

        // Set focus on the element
        oField.focus();

        // To get cursor position, get empty selection range
        var oSel = document.selection.createRange();

        // Move selection start to 0 position
        oSel.moveStart('character', -oField.value.length);

        // The caret position is selection length
        iCaretPos = oSel.text.length;
    }

        // Firefox support
    else if (oField.selectionStart || oField.selectionStart == '0')
        iCaretPos = oField.selectionStart;

    // Return results
    return (iCaretPos);
}

function ValidateMoneyFormat(id) {

    var v_text = $(id).val();
    var v_regex1 = new RegExp("^[0-9]{1,14}.{0,1}$");
    if (v_regex1.test(v_text)) {
        if ((v_text.indexOf('.') == -1) && (v_text.length > 0)) {
            v_text = v_text + ".00";
            $(id).val(v_text);
        }
        if (v_text.indexOf(".") > 0) {
            var charAfterdot = v_text.substring(v_text.indexOf("."), v_text.length - 1);
            if (charAfterdot.length == 0) {
                v_text = v_text + "00";
                $(id).val(v_text);
            }

        }
    }

    var v_regex = new RegExp("^[0-9]{1,14}.[0-9]{1,2}$");

    if ($(id).val() != '') {
        if (!v_regex.test(v_text)) {
            $(id).addClass('input-validation-error');
            return false;
        }
        else {
            $(id).removeClass('input-validation-error');
        }
    }
    return true;
}

function OnlyNumeric(event, inputType) {

    //Validate the key press event for integer/character/Aplhanuemeric
    if (window.event) { var charCode = window.event.keyCode; }
    else if (event) { var charCode = event.which; }
    else { return true; }
    if (navigator.appName == "Microsoft Internet Explorer") {

        if (inputType == 'i') {//integer validation                
            if (window.event.shiftKey == false) {
                if ((charCode > 47 && charCode < 58) || (charCode == 8) || (charCode == 56) || (charCode == 46) || (charCode >= 96 && charCode <= 105) || (charCode == 37) || (charCode == 39)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }

        }
        else if (inputType == 'c') {//character validation
            if (charCode > 31 && ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)))
            { return false; }
        }
        else if (inputType == 'a') {//alphanumeric validation
            if (charCode > 31 && ((charCode < 48 || charCode > 57) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)))
            { return false; }
        }
        else {

            return true;
        }
        return true;
    }
    else {
        if (event.shiftKey == false) {
            if (!((event.which >= 48) && (event.which <= 57) || (event.keyCode == 8) || (event.keyCode == 46) || (event.keyCode == 9) || (event.keyCode == 37) || (event.keyCode == 39) || (event.keyCode >= 96 && event.keyCode <= 105))) {
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return false;
        }
    }
}

function ValidateDuration(id) {
    var v_text = $(id).val();
    var v_regex = new RegExp("^[0-9]{1,2}:[0-9]{2}:[0-9]{2}$");

    if ($(id)[0].type != "hidden") {
        if ($(id).val() != '') {
            if (!v_regex.test(v_text)) {

                $('#divErrorMessage')[0].innerText = durationAndExcerptTimeMsg;
                $("#divErrorMessage").addClass('input-validation-error');
                $('#divErrorMessage').slideDown(0)
                $(id).focus();
                return false;
            }
        }
    }
    return true;
}

function onSubmitResource() {
    var count = 0;
    count = $('#hdnResourceListCount')[0].value;
    for (var i = 0; i <= count - 1; i++) {
        if (!ValidateDuration('#MasterProjectDetails_ClearanceResource_' + i.toString() + '__MusicTime') || !ValidateDuration('#txtExcerptTime' + i.toString())

                || !MoneyValidatorOnSubmit('#txtSuggestedFee' + i.toString())) {
            return false;
        }
    }
    return true;
}

function MoneyValidator(id) {
    if ($(id)[0].value > 922337203685477) {

        $('#divErrorMessage')[0].innerText = moneyRange;
        $('#divErrorMessage').addClass('input-validation-error');
        $('#divErrorMessage').slideDown(0)
        $(id).focus();
        return false;
    }
    return true;
}

function MoneyValidatorOnSubmit(id) {
    if ($(id)[0].value > 922337203685477) {
        $('#divErrorMessage')[0].innerText = moneyRange;
        $('#divErrorMessage').addClass('input-validation-error');
        $('#divErrorMessage').slideDown(0)
        $(id).focus();
        return false;
    }
    return true;
}

function OpenArtistSearchPopup(cntrlId) {
    var objArtistPopup = $('<div id="ArtistResourcePopup" style="margin:0;padding:0;"></div>');
    var managebtnId = cntrlId.id;
    var rowId = managebtnId.toString().substring(24, managebtnId.length);
    if ($('#spnartistId' + rowId).text() != "") {
        $(objArtistPopup).dialog({
            autoOpen: true,
            width: 1000,

            resizable: false,
            title: 'Manage Artist',
            modal: true,
            modal: true,
            close: function () {
                $('#hdnrowId').empty();
                $(this).remove(); // ensures any form variables are reset.  
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            }
        });
        var values =
            {
                "existingArtist": $('#hdnArtist' + rowId).val()
            }

        $(objArtistPopup).load('/GCS/Artist/SearchForArtist', values);
        $('#hdnrowId').empty();
        $('#hdnrowId').append(rowId);
        //added by Dhruv
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
            width: 1000,
            title: 'Manage Artist',

            modal: true,
            close: function () {
                $('#hdnrowId').empty();
                $(this).remove(); // ensures any form variables are reset.  
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            },
            open: function () {
                $(this).load('/GCS/Artist/SearchForArtist');
                $('#hdnrowId').empty();
                $('#hdnrowId').append(rowId);
            }
        });
        var dialogue = $('.ui-dialog');
        dialogue.animate({ top: "40px" }, 0);
    }
}

function setArtistInformation() {

    if (parseInt($('#hdnResourceListCount').val()) > 0) {
        for (k = 0; k < $('#hdnResourceListCount').val() ; k++) {
            var artistName = "";
            if ($('#hdnArtistCount' + k).val() != null) {
                for (i = 0; i < $('#hdnArtistCount' + k).val() ; i++) {
                    $('#spnartistId' + k).append($('#hdnArtistName' + k + '_' + i).val() + ':' + $('#hdnArtistId' + k + '_' + i).val() + ':' + $('#hdnArtistTalentId' + k + '_' + i).val() + '=')
                    $('#hdnArtist' + k).val($('#hdnArtist' + k).val() + $('#hdnArtistName' + k + '_' + i).val() + ':' + $('#hdnArtistId' + k + '_' + i).val() + ':' + $('#hdnArtistTalentId' + k + '_' + i).val() + '=')

                    if ($('#spnartistId' + k).text() == "") {
                        artistName += $('#hdnArtistName' + k + '_' + i).val();
                    }
                    else {
                        artistName += ',' + $('#hdnArtistName' + k + '_' + i).val();
                    }
                }
            }
        }
    }
}

function triggerMasterResourceAuditHistory(resourceId) {
    var AuditTypeId;
    var SelectedItemIds = [];
    var displayTitle;

    var ClearanceResourceId = $('#hdnClearanceResourceId' + resourceId).val();
    var ResourceId = $('#ResourceId' + resourceId).val();
    var R2ResourceId = $('#hdnR2ResourceId' + resourceId).val();


    if ($("#hdnProjRefId").val().length > 0) {
        displayTitle = $("#hdnProjRefId").val();
    }

    if ($('#hdnIsrc' + resourceId).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnIsrc' + resourceId).val();
    }

    if ($('#hdnResourceTitle' + resourceId).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnResourceTitle' + resourceId).val();
    }

    if ($('#hdnVersionTitle' + resourceId).val().length > 0) {
        displayTitle = displayTitle + ' - ' + $('#hdnVersionTitle' + resourceId).val();
    }


    if (R2ResourceId == 0) {
        AuditTypeId = AuditObjectType.MasterProjectResourceFreehandAuditHistory; //Free hand resource
        SelectedItemIds = [];
        SelectedItemIds.push(ClearanceResourceId);
        SelectedItemIds.push(ResourceId);
    }
    else {
        AuditTypeId = AuditObjectType.MasterProjectResourceAuditHistory; // R2 Resource
        SelectedItemIds = [];
        SelectedItemIds.push(ClearanceResourceId);
    }
    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
    return false;
}

function ValidateDurationAndExcerpt(id) {

    var v_text = $(id).val();
    var v_regex = new RegExp("^[0-9]{1,2}:[0-9]{2}:[0-9]{2}$");

    if ($(id).val() != '') {
        if (!v_regex.test(v_text)) {

            $('#warningmessageDurationExcerpt').html(durationExcerptMessage);
            $('#warningmessageDurationExcerpt').addClass('warning');
            $('#warningmessageDurationExcerpt').slideDown(0)
            $(id).addClass('input-validation-error');
            $(id).focus();
            return false;
        }
        else {
            $('#warningmessageDurationExcerpt').hide();
            $(id).removeClass('input-validation-error');
        }
    }
    return true;
}

function createSearchDialog() {
    var objDialog = $('#SearchDialog')
    objDialog.dialog({
        resizable: false,
        height: $(window).height() - 100,
        width: 1000,
        title: 'Search for Resources',
        autoOpen: false,
        modal: true,
        open: function () {
            SaveExistingResources()
            $(this).load("/GCS/Search/AdvanceResourceSearch");
        },
        close: function () {
            $('#SearchDialog').empty();
        }
    });
    var dialogue = $('.ui-dialog');
    dialogue.animate({
        top: "40px"
    }, 0);
};

$(document).ready(function () {

    createSearchDialog();

    $('body').unbind('keydown');

    $('body').keydown(function (e) {
        if (e.keyCode == 13) {
            if ($('#loadingDivPA').is(':visible')) {
                return;
            }
            if (currencyDdwnFocus == '0') {
                var index = $("#tabs").tabs('option', 'selected');
                if (index == "1") {
                    var checkResourceSearch = 0;

                    if ($("#SearchDialog") != undefined) {

                        if ($("#SearchDialog")[0] != undefined) {
                            if ($("#SearchDialog")[0].innerHTML.toString().trim() != "") {
                                checkResourceSearch = 1;
                            }
                        }

                    }

                    if (checkResourceSearch == 1) {
                        if ($("#ArtistResourcePopup").length != 0) {
                            if ($("#ArtistResourcePopup")[0].innerHTML.toString() != "") {
                                $("#searchForArtist").trigger('click');
                            }
                            else {
                                if ($("#Territory").length != 0) {
                                    if ($('#Territory')[0].innerHTML.toString() != "") {
                                        $("#btnSearchTerritory").trigger('click');
                                    }
                                    else {
                                        $("#btnsearch").trigger('click');
                                    }
                                }
                                else {
                                    $("#btnsearch").trigger('click');
                                }
                            }
                        }
                        else if (document.getElementById("freeHand") != null) {
                            var _freehandId = document.getElementById("freeHand");
                            if (_freehandId.style.display == 'none' || _freehandId.style.display == '') {
                                $("#btnsearch").trigger('click');
                            }
                            else {
                                $("#btnaddToProject").trigger('click');
                            }
                        }

                        else {
                            $("#btnsearch").trigger('click');
                        }
                    }
                    else {
                        if ($("#ArtistResourcePopup").length != 0) {
                            if ($("#ArtistResourcePopup")[0].innerHTML.toString() != "") {
                                $("#searchForArtist").trigger('click');
                            }
                            else {
                                if ($("#Territory").length != 0) {
                                    if ($('#Territory')[0].innerHTML.toString() != "") {
                                        $("#btnSearchTerritory").trigger('click');
                                    }
                                    else {
                                        $("#btnSearch").trigger('click');
                                    }
                                }
                                else {
                                    $("#btnSearch").trigger('click');
                                }
                            }
                        }
                        else {
                            if ($("#Territory").length != 0) {
                                if ($('#Territory')[0].innerHTML.toString() != "") {
                                    $("#btnSearchTerritory").trigger('click');
                                }
                                else {
                                    $("#btnSearch").trigger('click');
                                }
                            }
                            else {
                                $("#btnSearch").trigger('click');
                            }
                        }

                    }
                    return false;
                }
                else {
                    if ($("#Territory").length != 0) {
                        if ($('#Territory')[0].innerHTML.toString() != "") {
                            $("#btnSearchTerritory").trigger('click');
                        }
                        else {
                            $("#btnSearch").trigger('click');
                        }
                    }
                    else {
                        $("#btnSave").trigger('click');
                    }
                    return false;
                }
            }
        }
    });


    $("#tblResource h5").click(function () {
        var imgId = $(this).attr("id").substring(6);
        var tblId = $("#tblChild " + imgId);
        var display = document.getElementById('tblChild ' + imgId).style.display;
        if (display == 'none') {
            $("#tblChild " + imgId).show();
            document.getElementById('tblChild ' + imgId).style.display = "";
            $("#contentDv" + imgId).show();
            $('#Header' + imgId).removeClass('rightArrow');
            $('#Header' + imgId).addClass('downArrow');
        }
        else {
            $("#tblChild " + imgId).hide();
            document.getElementById('tblChild ' + imgId).style.display = "none";
            $("#contentDv" + imgId).hide();
            $('#Header' + imgId).removeClass('downArrow');
            $('#Header' + imgId).addClass('rightArrow');
        }
        return false;
    });

    var div = document.getElementById('tblResource');
    for (i = 0; i < div.children.length; i++) {
        $("#txtMRComment" + i).watermark(watermarkComments);
    }

    setArtistInformation();

    if ($("#chkSE").is(':checked')) {
        var resourceCount = $("#hdnResourceListCount").val();
        for (var i = 0; i < resourceCount; i++) {
            if ($("chksensivtive" + i) != null) {
                document.getElementById('chksensivtive' + i).checked = true;
            }
        }
        $('.check').prop('checked', true);
        $('.check').attr('disabled', true);
    }

    var table = document.getElementById('tblResource'),
            rows = table.getElementsByTagName('tr'), i, j, cells;

    var l = 0
    for (i = 0; i < rows.length; i++) {
        var RowId = rows[i].id.toString();

        if (RowId.indexOf("Hide Y") != -1) {
            rows[i].style.display = "none";
        }
    }

    var div = document.getElementById('tblResource');
    for (i = 0; i < div.children.length; i++) {
        var divId = div.children[i].id.toString();
        if (divId.indexOf("Hide Y") != -1) {
            $('#' + i).css("display", "none");
        }
    }

    jQuery("#txtISRC").watermark("ISRC");
    jQuery("#txtResuorceTitle").watermark("Resource Title");
    jQuery("#txtArtistName").watermark("Artist Name");
    jQuery("#txtUPC").watermark("UPC");
    jQuery("#txtReleaseTitle").watermark("Release Title");
    jQuery("#txtR2ProjectID").watermark("R2 Project ID");

    $('.disabledRemovelnk').click(function (e) {
        e.preventDefault();
    });

    if (($('#hdnMasterProjectStatusId').val() == "2") || ($('#hdnMasterProjectStatusId').val() == "5") || ($('#hdnMasterProjectStatusId').val() == "6")) {
        var resourceCount = $("#hdnResourceListCount").val();
        for (var i = 0; i < MasterControlsList.length; i++) {
            if (MasterControlsList[i].indexOf("_") == 0) {
                var resId = MasterControlsList[i].substr(1);
                for (var j = 0; j < resourceCount; j++) {
                    if ($("#hdnClearanceResourceId" + j).val() != 0) {
                        $("#contentDv" + j).find($("[id^=" + resId + "]"))[0].title = ResubmissionTooltipMsg;
                        if ($("#hdnR2ResourceId" + j).val() == 0) {
                            document.getElementById("BtnManagerArtistFreeHand" + j).title = ResubmissionTooltipMsg;
                            if (document.getElementById("MasterProjectDetails_ClearanceResource_" + j + "__Title") != null)
                                document.getElementById("MasterProjectDetails_ClearanceResource_" + j + "__Title").title = ResubmissionTooltipMsg;
                            document.getElementById("MasterProjectDetails_ClearanceResource_" + j + "__VersionTitle").title = ResubmissionTooltipMsg;
                            document.getElementById("ddlRecordingTypeResourceTab_" + j).title = ResubmissionTooltipMsg;
                            document.getElementById("ddlResourceTypeResourceTab_" + j).title = ResubmissionTooltipMsg;
                            document.getElementById("ddlMusicTypeResourceTab_" + j).title = ResubmissionTooltipMsg;
                            document.getElementById("MasterProjectDetails_ClearanceResource_" + j + "__MusicTime").title = ResubmissionTooltipMsg;
                        }
                    }
                }
            }
        }
    }


    $('#btnSearch').click(function (e) {
        if ($('#loadingDivPA').is(':visible')) {
            return;
        }
        var ResourceIndexToUpdate = "0";
        $('#hdncommandMaster').val(""); //Added By vikas For UC-011A,b

        e.preventDefault();
        $("#warningmessageResource").hide();
        $("#trShowMessageResource").hide();

        var ISRC = $('#txtISRC').val().substring();
        var ResourceTitle = $('#txtResuorceTitle').val().substring();
        var ArtistName = $('#txtArtistName').val().substring();
        var UPC = $('#txtUPC').val().substring();
        var R2ProjectID = $('#txtR2ProjectID').val().substring();

        if (ISRC != '' || ResourceTitle != '' || ArtistName != '' || UPC != '' || R2ProjectID) {

            $('#loadingDivPA').show();
            $.ajax({
                url: "/GCS/Search/SearchR2Resource",
                type: "POST",
                dataType: "json",
                data: {
                    Isrc: ISRC,
                    ResourceTitle: ResourceTitle,
                    ReleaseUpc: UPC,
                    ArtistName: ArtistName,
                    R2ProjectID: R2ProjectID,
                    "Criteria.RowIndex": '-1',
                    "Criteria.UserName": 'vivek_gupta',
                    "Criteria.PageSize": '5',
                    "Criteria.StartIndex": '0',
                    "jtStartIndex": '0',
                    "Criteria.QualificationCriteria": false
                },
                success: function (result) {
                    var recCount = result.TotalRecordCount;
                    if (recCount == 1) {
                        var record = result.Records[0];
                        //Added by Jyoti -Validation duplicate check- Unique Resource Search
                        var isExist = false;
                        SaveExistingResources();
                        resourceRowId = "";
                        $("#spnIsrcResource").empty();
                        var existingResources = $('#ExistingResources').text().toString().split(',');
                        var splittedRows = existingResourceRowId.toString().split(',');
                        for (var i = 0; i < existingResources.length; i++) {
                            if ($.trim(existingResources[i]) == record.ResourceId) {
                                isExist = true;
                                var ShortArtistName = record.ArtistName.toString().split(',');
                                var completeString = "";
                                resourceRowId = resourceRowId + ',' + splittedRows[i];
                                if ($("#spnIsrcResource").text() == "") {
                                    $("#spnIsrcResource").append(record.Isrc + "/" + ShortArtistName[0].toString().substring(0, 20) + "/" + record.Title.toString().substring(0, 20));
                                    $("#spnIsrcResource")[0].title = record.Isrc + "/" + record.ArtistName + "/" + record.Title;
                                    completeString = record.Isrc + "/" + record.ArtistName + "/" + record.Title;
                                }
                                else {
                                    $("#spnIsrcResource").append("," + record.Isrc + "/" + ShortArtistName[0].toString().substring(0, 20) + "/" + record.Title.toString().substring(0, 20));
                                    CompleteString = CompleteString + "||" + record.Isrc + "/" + record.ArtistName + "/" + record.Title;
                                    $("#spnIsrcResource")[0].title = CompleteString;
                                }
                            }
                        }
                        //End---------Duplicate Resource Check
                        if (isExist == false) {
                            var Resource = record.Isrc +
                                    '|' + record.Title.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.VersionTitle.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.ArtistName.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.ResourceId +
                                    '|' + record.Duration.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.PYear +
                                    '|' + record.RightsTypeCode.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.OwnedProjectId +
                                    '|' + record.GenreId.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.PCompanyId +
                                    '|' + record.PLicensingExtension.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.SampleCredit.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.R2AccountId +
                                    '|' + record.R2_ResourceId +
                                    '|' + record.MusicTypeDesc.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.RecordingTypeDesc.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.ResourceTypeDesc.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.AdminCompanyId +
                                    '|' + record.IsMobileResource +
                                    '|' + record.HasSample +
                                    '|' + record.HasSideArtist +
                                    '|' + ResourceIndexToUpdate + '~';

                            var formId = window.parent.document.forms[0];
                            var parentHiddenField = window.parent.document.getElementById("resourceListFromSearchPopUp");
                            parentHiddenField.value = Resource;
                            var digitalVal = $('#CreateMasterProject').serialize();

                            e.preventDefault();
                            e.stopPropagation();

                            $.post('/GCS/ClearanceProject/MasterAddButtonForResource', digitalVal, function (data) {
                                $('#tabs-2').html(data);
                                $('#hdnTempterritoryDetailsForSave').val('');
                                ParentCall();
                                $('#loadingDivPA').hide();
                            });
                            return false;
                        }
                        else {
                            $("#trShowMessageResource").show();
                            var table = document.getElementById('tblResource'),
                                    rows = table.getElementsByTagName('tr'), i, j, cells;
                            var splittedRowIds = resourceRowId.split(',');
                            for (i = 0, j = rows.length; i < j; ++i) {
                                for (var k = 0; k < splittedRowIds.length; k++) {
                                    if (splittedRowIds[k] != "") {
                                        if (i == splittedRowIds[k]) {
                                            $('#' + i)[0].style.borderColor = "#ff0000";
                                        }
                                    }
                                }
                            }
                            $('#loadingDivPA').hide();
                            return false;
                        }
                    }
                    else {
                        OpenAdvanceResourceSearchPopup();
                    }
                    $('#loadingDivPA').hide();
                },
                error: function (result) {
                    $('#loadingDivPA').hide();
                }
            });
        }
        else {
            OpenAdvanceResourceSearchPopup();
        }
    });
});