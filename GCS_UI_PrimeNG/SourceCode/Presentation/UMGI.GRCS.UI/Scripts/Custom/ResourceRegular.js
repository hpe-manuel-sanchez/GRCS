
var existingResourceRowId = "";
var resourceRowId = "";
var resourceCount = $("#hdnResourceListCount").val();

function setTitleInHidden(Idval) {
    $('#hdnResourceTitle' + Idval).val(document.getElementById("RegularProjectDetails_ClearanceResource_" + Idval + "__Title").value);
}

function setVersionTitleInHidden(Idval) {
    $('#hdnVersionTitle' + Idval).val(document.getElementById("RegularProjectDetails_ClearanceResource_" + Idval + "__VersionTitle").value);
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
            if (cells[k].id == "hdnFields " + l) { //Added for UC-055  
                if ($('#hdnArchiveFlagRegular' + l).val() == "N") {
                    if ($('#ExistingResources').text() == "") {
                        $('#ExistingResources').append($.trim($('#hdnR2ResourceId' + l).val())); //Added for UC-055
                        existingResourceRowId = l;
                        $('#ResourceRegular' + l)[0].style.borderColor = "#222222";
                        l = l + 1;
                    } else {
                        $('#ExistingResources').append(',' + $.trim($('#hdnR2ResourceId' + l).val())); //Added for UC-055
                        existingResourceRowId = existingResourceRowId + ',' + l;
                        $('#ResourceRegular' + l)[0].style.borderColor = "#222222";
                        l = l + 1;
                    }
                } else {
                    l = l + 1;
                }
            }
        }
    }
}

function OnDeleteClick(rowid) {
    if (true) {
        var ClearanceResourceId = $('#hdnClearanceResourceId' + rowid).val();
        $('#ResourceRegular' + rowid).hide();
        $('#hdnClearanceResourceId').val(ClearanceResourceId);
        $('#hdnArchiveFlagRegular' + rowid).val("Y");
        //territory remove
        var tempI = (rowid + 3);
        $("#selectedCountries_" + tempI).html('');
        $("#UnselectedCountries_" + tempI).html('');
        $('#hdnterritoryDetailsForSave_' + tempI).val('');
        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/RegularProjectRemoveResource', digitalVal + "&ClearanceResourceId=" + ClearanceResourceId, function (data) {
        });

    }
    return false;
}

function OnExpandAllClick(objType) {
    var resourceCount = $("#hdnResourceListCount").val();
    for (var i = 0; i < resourceCount; i++) {
        if (objType == 'c') {
            $("#contentDv" + i).hide();
            $("#tblChild " + i).hide();
            $('#Header' + i).removeClass('downArrow');
            $('#Header' + i).addClass('rightArrow');
            document.getElementById('tblChild ' + i).style.display = "none";
        } else {
            $("#tblChild " + i).show();
            document.getElementById('tblChild ' + i).style.display = "block";
            $("#contentDv" + i).show();
            $('#Header' + i).removeClass('rightArrow');
            $('#Header' + i).addClass('downArrow');
        }
    }
    return false;

}

function OnLoadGlobalCheck() {
    var count = $('#hdnResourceListCount').val();

    for (i = 0; i < count; i++) {
        if ($('#chkGloballyCleared' + i).is(':checked')) {
            $('#tdTxtboxGlobalClearedComments' + i).show();
        } else {
            $('#tdTxtboxGlobalClearedComments' + i).hide();
        }
    }
}

function OnGlobalClick(rowid) {

    var flag = $('#chkGloballyCleared' + rowid).is(':checked');
    if (flag) {
        $('#tdTxtboxGlobalClearedComments' + rowid).show();
    } else {

        $('#tdTxtboxGlobalClearedComments' + rowid).hide();
    }
}

function OpenAdvanceResourceSearchandUpdatePopup(SelectResoruceID) {
    var selectedResourceId = parseInt(SelectResoruceID) + 1;

    var value =
        {
            "ArtistId": selectedResourceId
        };
    $('#SearchDialog').empty()
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

    dialogue.animate({
        top: "40px"
    }, 0);
}

function triggerRegularResourceAuditHistory(resourceId) {
    var AuditTypeId;
    var SelectedItemIds = [];
    var displayTitle = '';

    var ClearanceResourceId = $('#hdnClearanceResourceId' + resourceId).val();
    var ResourceId = $('#ResourceId' + resourceId).val();
    var R2ResourceId = $('#hdnR2ResourceId' + resourceId).val();

    if ($("#ProjectRefId").val().length > 0) {
        displayTitle = $("#ProjectRefId").val();
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
        AuditTypeId = AuditObjectType.RegularNonTraditionalProjectResourceFreehandAuditHistory; //Free hand resource
        SelectedItemIds = [];
        SelectedItemIds.push(ClearanceResourceId);
        SelectedItemIds.push(ResourceId);
    } else {
        AuditTypeId = AuditObjectType.RegularNonTraditionalProjectResourceAuditHistory; // R2 Resource
        SelectedItemIds = [];
        SelectedItemIds.push(ClearanceResourceId);
    }
    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
    return false;
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

    } else {
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

        dialogue.animate({
            top: "40px"
        }, 0);
    }
}

function setArtistInformationRegular() {

    if (parseInt($('#hdnResourceListCount').val()) > 0) {
        for (k = 0; k < $('#hdnResourceListCount').val() ; k++) {
            var artistName = "";
            if ($('#hdnArtistCountRegular' + k).val() != null) {
                for (i = 0; i < $('#hdnArtistCountRegular' + k).val() ; i++) {
                    $('#spnartistIdRegular' + k).append($('#hdnArtistNameRegular' + k + '_' + i).val() + ':' + $('#hdnArtistIdRegular' + k + '_' + i).val() + ':' + $('#hdnArtistRegularTalentId' + k + '_' + i).val() + '=')
                    $('#hdnArtistRegular' + k).val($('#hdnArtistRegular' + k).val() + $('#hdnArtistNameRegular' + k + '_' + i).val() + ':' + $('#hdnArtistIdRegular' + k + '_' + i).val() + ':' + $('#hdnArtistRegularTalentId' + k + '_' + i).val() + '=')
                    if ($('#spnartistIdRegular' + k).text() == "") {
                        artistName += $('#hdnArtistNameRegular' + k + '_' + i).val();
                    } else {
                        artistName += ',' + $('#hdnArtistNameRegular' + k + '_' + i).val();
                    }
                }
            }
        }
    }

}

function TextAreaValidationForMax(id) {

    $('textarea').keydown(function (event) {
        if (event.keyCode == 13) {
            event.stopPropagation();
        } else {
            if (this.value.length >= 1073741824) {
                return false;
            }
        }
    });
}

function OpenArtistSearchPopupRegular(cntrlId) {

    var objArtistPopup = $('<div id="ArtistResourcePopup" style="margin:0;padding:0;"></div>');
    var managebtnId = cntrlId.id;
    var rowId = managebtnId.toString().substring(31, managebtnId.length);
    if ($('#hdnRegularResource') != null) {
        $('#hdnRegularResource').val("RegularResource");
    }
    if ($('#spnartistIdRegular' + rowId).text() != "") {
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
                "existingArtist": $('#hdnArtistRegular' + rowId).val()
            }
        $(objArtistPopup).load('/GCS/Artist/SearchArtistForRegularResource', values);
        $('#hdnrowId').empty();
        $('#hdnrowId').append(rowId);
        //added by Dhruv
        $('#tblExistingArtist').show();
        var dialogue = $('.ui-dialog');
        dialogue.animate({
            top: "30px"
        }, 0);

    } else {
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

        dialogue.animate({
            top: "40px"
        }, 0);
    }
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

function resourceRegularEventResourceClick() {
    var imgId = $(this).attr("id").substring(6);
    var tblId = $("#tblChild " + imgId);
    var display = document.getElementById('tblChild ' + imgId).style.display;
    if (display == 'none') {
        $("#tblChild " + imgId).show();
        document.getElementById('tblChild ' + imgId).style.display = "block";
        $("#contentDv" + imgId).show();
        $('#Header' + imgId).removeClass('rightArrow');
        $('#Header' + imgId).addClass('downArrow');
    } else {
        $("#tblChild " + imgId).hide();
        document.getElementById('tblChild ' + imgId).style.display = "none";
        $("#contentDv" + imgId).hide();
        $('#Header' + imgId).removeClass('downArrow');
        $('#Header' + imgId).addClass('rightArrow');
    }
    return false;
}

function resourceRegularEventImageClick() {
    var imgSrc = $(this).attr("src");
    var imgId = $(this).attr("id").substring(8);
    var tblId = $("#tblChild " + imgId);


    if (imgSrc.indexOf('Left') == -1) {
        $(this).attr("src", "/GCS/Images/Left.png");
        $(this).attr("title", "Expand");
        $(this).closest('tr').next().hide();
        $(this).closest('tr').next().next().hide();

    } else {

        $(this).attr("src", "/GCS/Images/Down.png");
        $(this).attr("title", "Collapse");

        document.getElementById("tblChild " + imgId).style.display = 'inline';
        $(this).closest('table').next().show();
        $(this).closest('tr').next().show();
        $(this).closest('tr').next().next().show();
    }
    return false;
}

function resourceRegularEventSearchClick(e) {

    if ($('#loadingDivPA').is(':visible')) {
        return;
    }

    var ResourceIndexToUpdate = "0";
    e.preventDefault();

    var index = $("#tabs").tabs('option', 'selected');
    $('#hdnDefaultTab').val(index);

    $("#warningmessageResource").hide();
    $("#trShowMessageResource").hide();

    var ISRC = $('#txtISRC').val().substring();
    var ResourceTitle = $('#txtResuorceTitle').val().substring();
    var ArtistName = $('#txtArtistName').val().substring();
    var UPC = $('#txtUPC').val().substring();
    var R2ProjectID = $('#txtR2ProjectID').val().substring();

    if (ISRC != '' || ResourceTitle != '' || ArtistName != '' || UPC != '' || R2ProjectID != '') {

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
                            } else {
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

                        var digitalVal = $('#CreateRegularProjectForm').serialize();

                        e.preventDefault();
                        e.stopPropagation();

                        $.post('/GCS/ClearanceProject/RegularAddButtonForResource', digitalVal, function (data) {

                            $('#tabs-4').html(data);
                            $('#hdnTempterritoryDetailsForSave').val('');
                            ParentCall();
                            $('#loadingDivPA').hide();
                        });
                        return false;

                    } else {

                        $('#loadingDivPA').hide();

                        $("#trShowMessageResource").show();
                        var table = document.getElementById('tblResource'), rows = table.getElementsByTagName('tr'), i, j, cells;
                        var splittedRowIds = resourceRowId.split(',');
                        for (i = 0, j = rows.length; i < j; ++i) {
                            for (var k = 0; k < splittedRowIds.length; k++) {
                                if (splittedRowIds[k] != "") {
                                    if (i == splittedRowIds[k]) {
                                        $('#ResourceRegular' + i)[0].style.borderColor = "#ff0000";
                                    }
                                }
                            }
                        }
                        return false;
                    }
                }
                else {
                    $('#loadingDivPA').hide();
                    OpenAdvanceResourceSearchPopup();
                }
                $('#loadingDivPA').hide();
            },
            error: function (result) { $('#loadingDivPA').hide(); }
        });

    }
    else {
        OpenAdvanceResourceSearchPopup();
    }
}

$(document).ready(function () {

    createSearchDialog();

    $("#tblResource h5").click(function () {
        resourceRegularEventResourceClick();
    });

    $(".imgHide").live("click", function () {
        resourceRegularEventImageClick();
    });

    jQuery("#txtISRC").watermark("ISRC");
    jQuery("#txtResuorceTitle").watermark("Resource Title");
    jQuery("#txtArtistName").watermark("Artist Name");
    jQuery("#txtUPC").watermark("UPC");
    jQuery("#txtR2ProjectID").watermark("R2 Project ID");

    $('.disabledRemovelnk').click(function (e) {
        e.preventDefault();
    });
    ParentCall();

    OnLoadGlobalCheck();

    showResourceResubmitMsg();

    $('#btnSearch').click(function (e) {
        resourceRegularEventSearchClick(e);
    });

    var div = document.getElementById('tblResource');
    for (i = 0; i < div.children.length; i++) {
        var divId = div.children[i].id.toString();
        if (divId.indexOf("Hide Y") != -1) {
            $('#' + i).css("display", "none");
        }
    }

    var div = document.getElementById('tblResource');
    for (var i = 0; i < div.children.length; i++) {
        $("#txtRegRComment" + i).watermark(watermarkComments);
        $("#txtGloballyCleared" + i).watermark(watermarkgloballyCleared);
    }

    setArtistInformationRegular();
});