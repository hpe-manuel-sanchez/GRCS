
var gridName = $('#hdnGridId').val(); var gridObj = $find(gridName); var chkboxid = $('#hdnResourceDRView').val();
var deletebtn; var reviewstatus; var tabIndex = 0;
var deletecollection = new Array();
//Create dialog
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

$(document).ready(function () {
    //resetAllForm();
    $('#warningRestriction').hide();
    $('#loadingDiv').hide(); $('#loaderPanel').hide();
    setTimeout(function () {
        var TotDiaHgt = $(".divobjBulkEditPreClearancePopUp").height();
        var ReduceHgt = TotDiaHgt - 10;
        $("#digitalRestrictionsBulkEditTab").css('height', ReduceHgt + "px");
    }, 300);




    $("#digitalRestrictionBulkEditTab").tabs({
        select: function (event, ui) {
            switch (ui.index) {
                case 0:
                    // first tab selected
                    $('#warningRestriction').hide();
                    $('#divAddDRTab').hide();
                    $('#divRemoveDRTab').hide();
                    tabIndex = 0;
                    break;
                case 1:
                    // second tab selected
                    $('#warningRestriction').hide();
                    $('#divChangeDRTab').hide();
                    $('#divRemoveDRTab').hide();
                    tabIndex = 1;
                    break;
                case 2:
                    // third tab selected
                    $('#warningRestriction').hide();
                    $('#divAddDRTab').hide();
                    $('#divChangeDRTab').hide();
                    tabIndex = 2;
                    break;

            }
            RestrictionInlineValidation();

        }
    });



});


/*DR Add*/
$('#btnAddDigitalTemplateMtn').click(function (e) {
    e.preventDefault();
    var length = $('#divAddDRTab .sampleaddDR').find('tr').length - 1;
    if (length < 0)
        length = 0;
    var digital = $('#AddDigitalRestriction').find('tr').html();
    $('#divAddDRTab .sampleaddDR').find('tbody').append('<tr>' + digital + '</tr>');

    $('#divAddDRTab .sampleaddDR').find('tr').each(function (i) {
        $(this).find('select, input').each(function () {
            if (i - 1 == length) {
                $(this).attr('name', $(this).attr('name').replace(/\[k\]/g, "[" + length + "]"));
                $(this).attr('id', $(this).attr('id').replace(/\_k\_/g, "_" + length + "_"));
            }

            $(this).attr('name', $(this).attr('name').replace(/\[\d\]/g, "[" + (i - 1).toString() + "]"));
            $(this).attr('id', $(this).attr('id').replace(/\_\d\_/g, "_" + (i - 1).toString() + "_"));
        });
        addDeleteDigitalRestrictionEventHandler();

    });

    RestrictionInlineValidation();

    var height = $('#formAddDR #divmiddeladdDR').height();
    if (height >= 360) {
        $('#formAddDR #divmiddeladdDR').height(360);
        $('#formAddDR #divmiddeladdDR').css('overflow-y', 'scroll')
        $('#formAddDR #divmiddeladdDR').animate({ scrollTop: $('#formAddDR #divmiddeladdDR')[0].scrollHeight }, 1000);
    }
});


function addDeleteDigitalRestrictionEventHandler() {
    $('.deleteImage').unbind('click');
    //To Delete the row
    $('.deleteImage').click(function (e) {

        e.preventDefault();
        HideWarningSuccess();
        resizeNew();
        $(this).parents('tr').remove();

        $('#divAddDRTab .sampleaddDR').find('tr').each(function (i) {
            $(this).find('select, input').each(function () {
                $(this).attr('name', $(this).attr('name').replace(/\[\d\]/g, "[" + (i - 1).toString() + "]"));
                $(this).attr('id', $(this).attr('id').replace(/\_\d\_/g, "_" + (i - 1).toString() + "_"));
            });
        });

        //$('#loadingDiv').hide();
        addDeleteDigitalRestrictionEventHandler();

    });

}
/*DR Add END*/
/* Save data*/
var rowindex = -1;
function index(object, context) {
    rowindex = -1;
    $.each(object, function (index, value) {
        if (value.RightSetIdLnr == context.RightSetIdLnr && value.RestrictionIdLnr == context.RestrictionIdLnr) {
            rowindex = index;
            return (false);
        }
    });
    return rowindex;
}


/*DR Add End*/

function AppendData(rowelement, columnname, value, disableflag) {
    if (value && disableflag == false) {
        var cell = $(rowelement).children(columnname);
        cell.text(value);
        cell.addClass("updatedCell");
    } else if (disableflag == true) {
        var cell = $(rowelement).children(columnname);
        cell.text("");
        cell.addClass("updatedCell");
    }
}

function ReplaceDefaultValue(String, Value, OldValue) {
    if (String == Value)
        String = OldValue;
    return String;
}
$("#btnCancel").click(function () {
    $(".ui-jtable-loading").show();
    $('.divobjBulkEditDigital').dialog('close');
});

function resetAllForm() {
    resetForm('formChangeDR');
    resetForm('formAddDR');
    resetForm('fromRemoveDR');
}
function resetForm(id) {
    $('#warningRestriction').hide();

    $('#' + id).each(function () {
        this.reset();
        $('#warningRestriction').hide();

        $('#' + id + ' :disabled').each(function () {
            $(this).attr("disabled", false);
            $(this).val(0);
        });

        $('#' + id + ' .input-validation-error').each(function () {
            $(this).removeClass('input-validation-error')
        });

    });
}

function DigRestSyncfusionGridScroll() {
    digiGridName = $('#hdnGridId').val();
    if ($('#' + digiGridName).length > 0) {
        var pagerHgt = $("#" + digiGridName + " .GridPager").height();
        var headerHgt = $("#" + digiGridName + " .GridHeader").height();
        var browsHgt = 0;
        if ($.browser.msie)
            browsHgt = 16;
        else
            browsHgt = 20;
        var reduceHgt = pagerHgt + headerHgt + browsHgt;

        var jtableTop = $("#" + digiGridName).offset().top;
        var topfootPos = $(".footer").offset().top;
        var totRecHeight = $("#" + digiGridName + "_Table").height() + reduceHgt;
        var tableHeight = topfootPos - jtableTop;
        var gridObj = $find(digiGridName);
        if (totRecHeight >= tableHeight)
            DigRestsetSyncfusionGridHeight(gridObj, tableHeight - reduceHgt);
        else
            DigRestsetSyncfusionGridHeight(gridObj, totRecHeight - reduceHgt + 20);
    }
}
function DigRestsetSyncfusionGridHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}


/*Apply Changes*/
//button Apply Changes
$("#btnApply").click(function () {
    $('#warningRestriction').hide();

    reviewstatus = ReplaceDefaultValue($('.reviewStatus option:selected').val(), RestrictionConstants.NoChangeRequired, "");

    if ($("#divChangeDRTab").is(':visible')) {
        $('#loadingDiv').show();
        setTimeout(function () { saveChanges(); }, 0);


    }
    else if ($("#divRemoveDRTab").is(':visible')) {

        deleteRestriction();
    }
    else if ($("#divAddDRTab").is(':visible')) {

        // $('.divobjBulkEditDigital').dialog('close');
        $('#formAddDR .input-validation-error').each(function () {
            $(this).removeClass('input-validation-error');
        });
        $('#loadingDiv').show();
        setTimeout(function () { AddRestriction(); }, 0);

    }

});

function IsReleaseRestriction() {
    var digRestGridId = $("#hdnGridId").val();
    var indexrel = digRestGridId.indexOf('release');
    return indexrel != -1 ? true : false;
}

var _greltomodDate, IsReleaseGrid;
var d_usertype, d_CommercialModelTypes, d_RestrictionTypes, d_RestrictionReason, d_ConsentPeriodTypes, d_Notes;
var addFlag = false;

// if no child for the parent row return true;
// other wise return false;
function HasChildForParentNode(row) {
    var flag = false;
    var inlineValue = '';
    var UseType = $(row).children(".UseTypes").html();
    var CommercialModel = $(row).children(".CommercialModel").html();
    var Restriction = $(row).children(".Restriction").html();
    var RestrictionReason = $(row).children(".RestrictionReason").html();
    var ConsentPeriod = $(row).children(".ConsentPeriod").html();
    var Notes = $(row).children(".Notes").html();
    var RightSetId = $(row).children(".RightSetId").html();

    inlineValue = UseType;
    inlineValue += CommercialModel;
    inlineValue += Restriction;
    inlineValue += RestrictionReason;
    inlineValue += ConsentPeriod;
    inlineValue += Notes;
    if (inlineValue.length == 0)
        flag = true;

    return flag;

}
/* Below is sample code for cloning the data */
// Shallow copy
// var newObject = jQuery.extend({}, oldObject);
// Deep copy
//var newObject = jQuery.extend(true, {}, oldObject);

function AddRestriction() {
    gridObj = $find(gridName);
    var RightSetcollection = new Array();
    //  var gridDatas = gridObj.get_SelectedRecords();
    var syndata = gridObj._edit._addedRecords;
    var bulkrows = $("#divDigitalSection tr:gt(0)"); // skip the header row
    var haschildParentNode = false;
    if (validateDigitalRestriction('#divDigitalSection') == false) {
        $('#loadingDiv').hide();
        $('#loaderPanel').hide();
        return false;
    }
    if (validateDigitalRestrictionCombination('#divDigitalSection') == false) {
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        return false;
    }
    if (validateDigitalRestrictonReason('#divDigitalSection') == false) {
        $('#loadingDiv').hide(); $('#loaderPanel').hide();
        return false;
    }
    IsReleaseGrid = IsReleaseRestriction();

    var selectedChkBoxes = $("#" + gridName + " input[id$=" + chkboxid + "]:checked");
    // $("#" + gridName + " input[id$=" + chkboxid + "]:checked").each(function () {
    for (var chkBoxIndex = 0; chkBoxIndex < selectedChkBoxes.length; chkBoxIndex++) {


        var rowelement = $(selectedChkBoxes[chkBoxIndex]).closest('tr');
        var isTrowVisible = $(rowelement)[0].style.display != "none";
        if (isTrowVisible) {

            var MergeFlag = true;
            var tdHasmerge = $(rowelement).children("td.RowCell")[1];
            MergeFlag = $(tdHasmerge).hasClass("Merged");

            if (MergeFlag == false) {

                //  var strGridvalue;
                var RightsEditPermitted = $(rowelement).find('.RightsEditPermitted').text();
                var RightSetId = $(rowelement).find('.RightSetId').text();
                var HasIndex = RightSetcollection.indexOf(RightSetId);
                // var newObject = JSLINQ(gridDatas).Where(function (item) { return item.RightSetIdLnr == RightSetId });



                if (HasIndex == -1) {
                    rightsetid = RightSetcollection.push(RightSetId);
                    if (RightsEditPermitted == 'Y') {

                        var newObject = null;
                        //                        for (var selectedIndex = 0; selectedIndex < gridDatas.length; selectedIndex++) {
                        //                            var item = gridDatas[selectedIndex];
                        //                            if (item.RightSetIdLnr == RightSetId) {
                        //                                newObject = item;
                        //                                break;
                        //                            }
                        //                        }

                        if (newObject == null) {
                            var gridBaseData = gridObj._edit._data;
                            for (var index = 0; index < gridObj._edit._data.length; index++) {
                                if (gridBaseData[index].RightSetIdLnr == RightSetId) {
                                    newObject = gridBaseData[index];
                                    break;
                                }
                            }

                        }

                        addFlag = true;

                        haschildParentNode = HasChildForParentNode(rowelement);
                        var addbtn = $(rowelement).find('#imgAddNew')[0];
                        var clickrow = addbtn.parentNode.parentNode;
                        var rowindex = $(clickrow).index();
                        var span = $(rowelement).find('.RightSetId').attr('rowSpan');
                        var index = rowindex;
                        if (span != 1)
                            index = index + parseInt(span) - 1;

                        //                        $.each(bulkrows, function (k, addrows) {

                        for (var rowIndex = 0; rowIndex < bulkrows.length; rowIndex++) {
                            var addrows = bulkrows[rowIndex];
                            var ObjNew = JSON.parse(JSON.stringify(newObject));

                            d_usertype = '';
                            d_CommercialModelTypes = '';
                            d_RestrictionTypes = '';
                            d_RestrictionReason = '';
                            d_ConsentPeriodTypes = '';
                            d_Notes = '';

                            d_usertype = $(addrows).find('.UseTypes option:selected').text();
                            d_CommercialModelTypes = $(addrows).find('.CommercialModelTypes option:selected').text();
                            d_RestrictionTypes = $(addrows).find('.RestrictionTypes option:selected').text();
                            RestrictionTypesid = $(addrows).find('.RestrictionTypes option:selected').val;
                            d_RestrictionReason = $(addrows).find('.RestrictionReason option:selected').text();
                            if ($(addrows).find('.txtRestrictionReason')[0].style.display != "none")
                                d_RestrictionReason = $(addrows).find('.txtRestrictionReason').val();
                            d_ConsentPeriodTypes = $(addrows).find('.ConsentPeriodTypes option:selected').text();
                            if (RestrictionTypesid == "4" || RestrictionTypesid == "1" || RestrictionTypesid == "3")
                                d_ConsentPeriodTypes = ' ';
                            d_Notes = $(addrows)[0].children[5].children[0].value;
                            var newTr = AddNewRow(index, rowelement, haschildParentNode);
                            if (haschildParentNode) {
                                haschildParentNode = false;
                                rowelement = newTr;
                            } else {
                                index = parseInt(index) + 1;
                            }

                            Array.add(gridObj._edit._insertedRowsCollection, $(newTr)[0]);
                            //  var dataindex = syndata.length;
                            ObjNew.RestrictionIdLnr = 0;

                            updateValue(ObjNew, d_usertype, d_CommercialModelTypes, d_RestrictionTypes, d_RestrictionReason, d_ConsentPeriodTypes, d_Notes);
                            gridObj._edit._isEdit = true;

                            if (RestrictionTypesid == "4" || RestrictionTypesid == "1" || RestrictionTypesid == "3")
                                ObjNew.ConsentPeriodLnr = '';

                            syndata.push(ObjNew);

                        }
                    }
                }
            }
        }
    }

    resetAllForm();
    $('#loadingDiv').hide(); $('#loaderPanel').hide();
    setTimeout("RightsWorkQueueScrollBar(" + gridName + ")", 2000);
    $('.divobjBulkEditDigital').dialog('close');
}

var addflag = false;
var newRow;
// to verify dynamic id appending
function AddNewRow(rowindex, trow, haschildParentNode) {

    var digRestGridId = $("#hdnGridId").val();
    var mergeColumnIndex = 0;
    var IsRelease = digRestGridId.indexOf('release');
    if (IsRelease != -1)
        mergeColumnIndex = 14;
    else
        mergeColumnIndex = 20;
    var newinsertTr = $(trow).clone();
    ///Remove class attribute
    $(newinsertTr).removeAttr("class");
    /// Add calss attribute 
    $(newinsertTr).addClass("InsertedRow");

    if (!haschildParentNode) {
        ///Get old cell span 
        var oldRowSpan = $(trow).children("td.mergeCount").html();
        if (parseInt(oldRowSpan) == 0)
            oldRowSpan = "1";
        //Increment new row span
        var newRowSpan = parseInt(oldRowSpan) + 1;
        /// Increament(1)  RowSpan for parent row
        $(trow).children("td.RowCell").slice(0, mergeColumnIndex).attr("rowspan", newRowSpan);


        //    ///Increment merge cell count to Parent row 
        var tdcell = $(trow).children("td.RowCell")[1];
        //    var OldMergeValue = $(tdcell).html();
        //    var newMergeValue = parseInt(OldMergeValue) + 1;
        $(tdcell).text(newRowSpan);

        ////    ///parent row check box always checked
        ////    ///While insert new row uncheck the check box
        ////    var checkbox = $(newinsertTr).find(".checkboxDigitalClass");
        ////    $(checkbox).attr("checked", false);
        ///Remove class attribute
        $(newinsertTr).removeAttr("class");
        /// Add calss attribute 
        $(newinsertTr).addClass("InsertedRow");
        /// set 0 for merge cell to the inserted new row
        var tdcell = $(newinsertTr).children("td.RowCell")[1];
        $(tdcell).html('0');

        ///Removed Span for newly add row
        $(newinsertTr).children("td.RowCell").slice(0, mergeColumnIndex).removeAttr("rowspan");
        /// set class merged for newly add row for hide this left table cell  
        $(newinsertTr).children("td.RowCell").slice(0, mergeColumnIndex).addClass("Merged");
        /// append bulk edit  value to new row
    }
    $(newinsertTr).children(".UseTypes").html('');
    $(newinsertTr).children(".CommercialModel").html('');
    $(newinsertTr).children(".Restriction").html('');
    $(newinsertTr).children(".RestrictionReason").html('');
    $(newinsertTr).children(".ConsentPeriod").html('');
    $(newinsertTr).children(".Notes").html('');
    $(newinsertTr).children(".RestrictionIdLnr").html('');
 


    AppendData(newinsertTr, '.UseTypes', d_usertype, false)
    AppendData(newinsertTr, '.CommercialModel', d_CommercialModelTypes, false)
    AppendData(newinsertTr, '.Restriction', d_RestrictionTypes, false)
    AppendData(newinsertTr, '.RestrictionReason', d_RestrictionReason, false)
    AppendData(newinsertTr, '.ConsentPeriod', d_ConsentPeriodTypes, false)
    AppendData(newinsertTr, '.Notes', d_Notes, false)


    ///  add new row in the grid
    $("#" + digRestGridId + "_Table").find('tr').eq(rowindex).after(newinsertTr);

    if (haschildParentNode) {
        var row = $("#" + digRestGridId + "_Table").find('tr').eq(rowindex).remove();
    }
    return newinsertTr;
}



function popupdialogdelete(tempContentType) {

    objDialog.html('<div style=\'width: 100%; float: left;margin-top:10px;\' > <div class=\'resourceInfoHeader\'  style=\'float: left;vertical-align:middle\' /><div style=\'float: left;vertical-align:middle\' >' + RestrictionConstants.RestrictionRemoveMessage + '</div></div>');
    //Open Dialog
    objDialog.dialog('option', { title: RestrictionConstants.RestrictionRemoveTitle });
    objDialog.dialog('option', { width: '500px',
        buttons: { 'Yes': function () {
            $(this).dialog('close');
            var digRestGridId = $("#hdnGridId").val();
            var indexrel = digRestGridId.indexOf('release');
            IsReleaseRestriction = indexrel != -1 ? true : false;
            $.each(deletecollection, function (index, value) {
                if (IsReleaseRestriction) {
                    setTimeout(function () {
                        ReleaseDigDeleteClick(value);
                    }, 0);
                }
                else {
                    setTimeout(function () {
                        DigRestDeleteClick(value);
                    }, 0);
                }

            });
            resetAllForm();
            $('.divobjBulkEditDigital').dialog('close');
        },
            'Cancel': function () {
                $(this).dialog('close');
            }
        },

        close: function () { $(this).dialog('close'); }
    });
    objDialog.dialog('open');
}
/// for delete Restriction
function deleteRestriction() {

    var strfilter = "";
    var usertype = ReplaceDefaultValue($('#fromRemoveDR .UseTypes option:selected').text(), RestrictionConstants.NA, "");
    var CommercialModelTypes = ReplaceDefaultValue($('#fromRemoveDR .CommercialModelTypes option:selected').text(), RestrictionConstants.NA, "");
    var RestrictionTypes = ReplaceDefaultValue($('#fromRemoveDR .RestrictionTypes option:selected').text(), RestrictionConstants.NA, "");
    var ConsentPeriodTypes = ReplaceDefaultValue($('#fromRemoveDR .ConsentPeriodTypes option:selected').text(), RestrictionConstants.NA, "");

    if (usertype)
        strfilter = usertype;
    if (CommercialModelTypes)
        strfilter += CommercialModelTypes;
    if (RestrictionTypes)
        strfilter += RestrictionTypes;
    if (ConsentPeriodTypes)
        strfilter += ConsentPeriodTypes;

    //var flag = false;  
    var tempContentType;
    //var uType, cModel, restriction, content;
    var selectedChkBoxes = $("#" + gridName + " input[id$=" + chkboxid + "]:checked");
    // $("#" + gridName + " input[id$=" + chkboxid + "]:checked").each(function () {
    for (var chkBoxIndexe = 0; chkBoxIndexe < selectedChkBoxes.length; chkBoxIndexe++) {
        var rowelement = $(selectedChkBoxes[chkBoxIndexe]).closest('tr');
        var strGridvalue = "";
        var RightsEditPermitted = $(rowelement).find('.RightsEditPermitted').text();
        if (RightsEditPermitted == 'Y') {
            if (usertype)
                strGridvalue = $(rowelement).find('.UseTypes').html();
            if (CommercialModelTypes)
                strGridvalue += $(rowelement).find('.CommercialModel').html();
            if (RestrictionTypes)
                strGridvalue += $(rowelement).find('.Restriction').html();
            if (ConsentPeriodTypes)
                strGridvalue += $(rowelement).find('.ConsentPeriod').html();
            if (strfilter == strGridvalue) {

                deletebtn = $(rowelement).find('#imgDelete')[0];
                deletecollection.push(deletebtn);
                tempContentType = $(selectedChkBoxes[chkBoxIndexe]).val();

            }

        }

    }

    // if (deletecollection.length > 0)
    popupdialogdelete(tempContentType);

}

function saveChangesForAddRecords(rightsetID, RestrictionTypesid) {
    var syndata = gridObj._edit._addedRecords;
    $.each(syndata, function (index, colObj) {

        if (colObj.RightSetIdLnr == rightsetID) {
            updateaddRecords(gridObj._edit._addedRecords[index], u_usertype, u_CommercialModelTypes, u_RestrictionTypes, u_RestrictionReason, u_ConsentPeriodTypes, u_Notes);

            if (RestrictionTypesid == "4" || RestrictionTypesid == "1" || RestrictionTypesid == "3")
                gridObj._edit._addedRecords[index].ConsentPeriodLnr = ' ';
        }
    });

}
var u_usertype, u_CommercialModelTypes, u_RestrictionTypes, u_RestrictionReason, u_ConsentPeriodTypes, u_Notes;
function setReviewStatus(row, reviewstatus) {
    if (reviewstatus) {
        var cell = $(row).children('.ReviewStatus');
        if (cell.length > 0) {
            $(cell).html("<div class=\'flagGreen\'></div>");
            $(cell).addClass("updatedCell");
        }
    }
}
/// for Save Restriction
function saveChanges() {
    var gridDatas = gridObj.get_SelectedRecords();
    var syndata = gridObj._edit._data;
    var Restrictionid = $('#formChangeDR .RestrictionTypes option:selected').val();
    u_usertype = ReplaceDefaultValue($('#formChangeDR .UseTypes option:selected').text(), RestrictionConstants.NoChangeRequired, "");
    u_CommercialModelTypes = ReplaceDefaultValue($('#formChangeDR .CommercialModelTypes option:selected').text(), RestrictionConstants.NoChangeRequired, "");
    u_RestrictionTypes = ReplaceDefaultValue($('#formChangeDR .RestrictionTypes option:selected').text(), RestrictionConstants.NoChangeRequired, "");
    RestrictionTypesid = $('#formChangeDR .RestrictionTypes option:selected').val();
    u_RestrictionReason = ReplaceDefaultValue($('#formChangeDR .RestrictionReason option:selected').text(), RestrictionConstants.NoChangeRequired, "");
    var resReasonTextValue = $('#formChangeDR .txtRestrictionReason').val();
    //if ($('#formChangeDR .txtRestrictionReason').is(":visible") && resReasonTextValue)
    if ($('#formChangeDR .txtRestrictionReason').is(":visible")) {
        if (!resReasonTextValue) {
            $('#formChangeDR .txtRestrictionReason').addClass('input-validation-error');
            $('#loadingDiv').hide(); $('#loaderPanel').hide();
            ShowRestrictionMsg(messageCommon.digitalIncomplete);
            return false;
        }
        $('#formChangeDR .txtRestrictionReason').removeClass('input-validation-error');
        u_RestrictionReason = $('#formChangeDR .txtRestrictionReason').val();
    }



    u_ConsentPeriodTypes = ReplaceDefaultValue($('#formChangeDR .ConsentPeriodTypes option:selected').text(), RestrictionConstants.NoChangeRequired, "");
    u_Notes = $('#formChangeDR .Notes').val();

    if (RestrictionTypesid == "4" || RestrictionTypesid == "1" || RestrictionTypesid == "3") {
        u_ConsentPeriodTypes = ' ';
        IsdisabledConsentPeriod = false;
    }
    var IsdisabledReason = true;
    var IsdisabledConsentPeriod = true;

    if (Restrictionid == 1 || !Restrictionid)
        IsdisabledReason = false;
    if (Restrictionid == 5 || Restrictionid == 2 || !Restrictionid)
        IsdisabledConsentPeriod = false;
    if (validateDigitalRestrictonReason('#divDigitaladd') == false)
        return false;
    if (validateDigitalRestriction('#divDigitaladd') == false)
        return false;


    $.each(gridDatas, function (k, colObj) {
        if (colObj.RightsEditPermitted == 'Y' &&  colObj.RestrictionIdLnr > 0) {

            updateValue(colObj, u_usertype, u_CommercialModelTypes, u_RestrictionTypes, u_RestrictionReason, u_ConsentPeriodTypes, u_Notes);
            var recordindex = index(gridObj._edit._updatedRecords, colObj);

            if (recordindex == -1) {
                var dataIndex = index(gridObj._edit._data, colObj);
                if (dataIndex != -1) {
                    colObj = gridObj._edit._data[dataIndex];
                    if (colObj.RestrictionIdLnr != -1) {  // Change Restriction disable for no child rows
                        updateValue(colObj, u_usertype, u_CommercialModelTypes, u_RestrictionTypes, u_RestrictionReason, u_ConsentPeriodTypes, u_Notes);
                        if (IsdisabledReason)
                            colObj.RestrictonReasonLnr = "";
                        if (IsdisabledConsentPeriod)
                            colObj.ConsentPeriodLnr = "";
                        gridObj._edit._updatedRecords.push(colObj);
                    } 
                }
            }
            else {
                updateValue(gridObj._edit._updatedRecords[recordindex], u_usertype, u_CommercialModelTypes, u_RestrictionTypes, u_RestrictionReason, u_ConsentPeriodTypes, u_Notes);
                if (RestrictionTypesid == "4" || RestrictionTypesid == "1" || RestrictionTypesid == "3")
                    gridObj._edit._updatedRecords[recordindex].ConsentPeriodLnr = ' ';

                if (IsdisabledReason)
                    colObj.RestrictonReasonLnr = "";
                if (IsdisabledConsentPeriod)
                    colObj.ConsentPeriodLnr = "";
            }
            recordindex = -1;
            recordindex = index(gridObj._edit._data, colObj);
            if (recordindex != -1) {
                if (gridObj._edit._data[recordindex].RestrictionIdLnr != -1)  { // Change Restriction disable for no child rows
                    updateValue(gridObj._edit._data[recordindex], u_usertype, u_CommercialModelTypes, u_RestrictionTypes, u_RestrictionReason, u_ConsentPeriodTypes, u_Notes);
                    if (RestrictionTypesid == "4" || RestrictionTypesid == "1" || RestrictionTypesid == "3")
                        gridObj._edit._data[recordindex].ConsentPeriodLnr = ' ';

                    if (IsdisabledReason)
                        gridObj._edit._data[recordindex].RestrictonReasonLnr = "";
                    if (IsdisabledConsentPeriod)
                        gridObj._edit._data[recordindex].ConsentPeriodLnr = "";
                }
            }
            saveChangesForAddRecords(colObj.RightSetIdLnr, RestrictionTypesid);
            gridObj._edit._isEdit = true;
        }
    });


    $("#" + gridName + " input[id$=" + chkboxid + "]:checked").each(function () {
        //$(this).closest('tr').children('.UserType').length

        var row = $(this).closest('tr');
        var haschild = HasChildForParentNode(row);
        var defaultflag = false;
        var RightsEditPermitted = $(row).children('.RightsEditPermitted').text();
        if (RightsEditPermitted == 'Y' && !haschild) {
            AppendData(row, '.UseTypes', u_usertype, defaultflag)
            AppendData(row, '.CommercialModel', u_CommercialModelTypes, defaultflag)
            AppendData(row, '.Restriction', u_RestrictionTypes, defaultflag)
            AppendData(row, '.RestrictionReason', u_RestrictionReason, IsdisabledReason)
            AppendData(row, '.ConsentPeriod', u_ConsentPeriodTypes, IsdisabledConsentPeriod)
            AppendData(row, '.Notes', u_Notes, defaultflag)
            setReviewStatus(row, reviewstatus);

        }

    });




    $('#warningRestriction').hide();
    resetAllForm();
    $('#loadingDiv').hide(); $('#loaderPanel').hide();
    $('.divobjBulkEditDigital').dialog('close');
}

function updateaddRecords(obj, usertype, CommercialModelTypes, RestrictionTypes, RestrictionReason, ConsentPeriodTypes, Notes) {
    if (usertype)
        obj.UseTypeLnr = usertype;
    if (CommercialModelTypes)
        obj.CommercialModelsLnr = CommercialModelTypes;
    if (RestrictionTypes)
        obj.RestrictionLnr = RestrictionTypes;
    if (RestrictionReason)
        obj.RestrictonReasonLnr = RestrictionReason;
    if (ConsentPeriodTypes)
        obj.ConsentPeriodLnr = ConsentPeriodTypes;
    if (Notes)
        obj.NotesLnr = Notes;

}
function updateValue(obj, usertype, CommercialModelTypes, RestrictionTypes, RestrictionReason, ConsentPeriodTypes, Notes) {
    if (usertype)
        obj.UseTypeLnr = usertype;
    if (CommercialModelTypes)
        obj.CommercialModelsLnr = CommercialModelTypes;
    if (RestrictionTypes)
        obj.RestrictionLnr = RestrictionTypes;
    if (RestrictionReason)
        obj.RestrictonReasonLnr = RestrictionReason;
    if (ConsentPeriodTypes)
        obj.ConsentPeriodLnr = ConsentPeriodTypes;
    if (Notes)
        obj.NotesLnr = Notes;
    if (reviewstatus) {
        // obj.RightsEditPermitted = 'N';
        obj.ReviewStatusLnr = reviewstatus;
    }
}
/* Save data END*/

function ShowRestrictionMsg(msg) {
    $('#warningRestriction').show();
    $('#warningRestriction #AlertMsg').html(msg);
}
//The below logic enables DigitalConsentList only when DigitalRestrictionList is 'Consult' or 'Consent Required' as part of UAT Bug RC:22
$(function () {

    RestrictionInlineValidation();
});

function RestrictionInlineValidation() {
    //
    if (tabIndex == 0)
        DigitalRestrictionChangeEvent('#divDigitaladd ');
    else if (tabIndex == 1)
        DigitalRestrictionChangeEvent('#divDigitalSection ');
    else
        DigitalRestrictionChangeEvent('#divDigitalRemove ');


}
// if Restriction Reason selected items others
// Textbox should display in the GUI
function RestrictionReasonOthers(tableclass) {

    $(tableclass + 'select.RestrictionReason').unbind('change');
    $(tableclass + ' select.RestrictionReason').change(function () {
        var temp = $(this).val();
        var txtbox = $($(tableclass + " input.txtRestrictionReason")[$(tableclass + " select.RestrictionReason").index(this)]);
        var strRestrictionTypes = $($(tableclass + " select.RestrictionTypes")[$(tableclass + " select.RestrictionTypes").index(this)]).val();
        var width = $(this).width() - 5;
        if (temp == 5) {
            $(txtbox).show();
            $(txtbox).focus();
            $(txtbox).val('');
            $(txtbox).width(width);
            //$(this)[0].selectedIndex = 0
            $(this).hide();
        }
        else if (temp != 5 && strRestrictionTypes != 1 && strRestrictionTypes) {

            $(txtbox).hide();
            $(this).show();

        }
    });

    $(tableclass + ' select.RestrictionReason').each(function () {
        var temp = $(this).val();
        var txtbox = $($(tableclass + " input.txtRestrictionReason")[$(tableclass + " select.RestrictionReason").index(this)]);
        var strRestrictionTypes = $($(tableclass + " select.RestrictionTypes")[$(tableclass + " select.RestrictionReason").index(this)]).val();

        var width = $(this).width();
        if (temp == 5) {
            $(txtbox).show();
            $(this).hide();
        }
        else if (temp != 5 && strRestrictionTypes != 1 && strRestrictionTypes) {
            $(txtbox).hide();
            $(txtbox).val('');
            $(this).show();

        }
    });

}
// if Restriction Reason dropdown set visibility and
// Restriction Reason text box visiblity set as hide
function ResetRestrictionTypes(tableclass, currentThis) {

    var ddRestrictionReason = $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(currentThis)]);
    $(ddRestrictionReason).attr("disabled", false);
    $(ddRestrictionReason).val('0');
    $(ddRestrictionReason).show();
    var txtbox = $($(tableclass + " input.txtRestrictionReason")[$(tableclass + " select.RestrictionTypes").index(currentThis)]).hide();

}
function ShowRestrionReason(tableclass) {

    $($(tableclass + " input.txtRestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).hide();
    $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).show();
    $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).val('');
    $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", false);
}


function DigitalRestrictionChangeEvent(tableclass) {

    $(tableclass + 'select.RestrictionTypes').unbind('change');
    $(tableclass + ' select.RestrictionTypes').change(function () {
        //ResetRestrictionTypes(tableclass, this);
        var temp = $(this).val();
        //        if (temp == "") {
        //            ShowRestrionReason(tableclass);
        //        }
        //        else {
        if (temp == 1 || temp == 3 || temp == 4) {
            $($(tableclass + " select.ConsentPeriodTypes")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", true);
            $($(tableclass + " select.ConsentPeriodTypes")[$(tableclass + " select.RestrictionTypes").index(this)]).prop('selectedIndex', 0);

        }
        else {
            $($(tableclass + " select.ConsentPeriodTypes")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", false);

        }
        // UC018-1179 
        if (temp == 1 && temp) {
            var flag = $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).val();
            if (flag != 5) {
                $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", false);
                $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).val('');
            }

        }
        else {

            $($(tableclass + " input.txtRestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).hide();
            $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).show();
            $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).prop('selectedIndex', 0);
            $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", true);

        }
        //}

    });

    $(tableclass + ' select.RestrictionTypes').each(function () {
        var temp = $(this).val();
        //ResetRestrictionTypes(tableclass, this);

        //        if (temp == "") {
        //            ShowRestrionReason(tableclass);
        //        }
        //        else {
        if (temp == 1 || temp == 3 || temp == 4) {
            $($(tableclass + " select.ConsentPeriodTypes")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", true);
            $($(tableclass + " select.ConsentPeriodTypes")[$(tableclass + " select.RestrictionTypes").index(this)]).prop('selectedIndex', 0);
        }
        else {
            $($(tableclass + " select.ConsentPeriodTypes")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", false);
        }
        // UC018-1179 
        if (temp == 1 && temp) {
            var flag = $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).val();
            if (flag != 5) {
                $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", false);
            }

        }
        else {

            $($(tableclass + " input.txtRestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).hide();
            $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).show();
            $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).prop('selectedIndex', 0);
            $($(tableclass + " select.RestrictionReason")[$(tableclass + " select.RestrictionTypes").index(this)]).attr("disabled", true);

        }
        //}


    });
    RestrictionReasonOthers(tableclass);
}
///UC018-1179 
function validateDigitalRestrictonReason(tableid) {
    var result = true;
    $(tableid + " select.RestrictionTypes").each(function () {
        var tempContentType = $(this).val();
        var RestrictionReason = $($(tableid + " select.RestrictionReason")[$(tableid + " select.RestrictionTypes").index(this)]).val();


        if (tempContentType == 1 && (RestrictionReason == 0 || !RestrictionReason)) {
            popupdialogInComplete($($(tableid + " select.RestrictionReason")[$(tableid + " select.RestrictionTypes").index(this)]));
            //ShowRestrictionMsg(messageCommon.digitalIncomplete.toString());
            result = false;
            return false;
        }

    });
    return result;
}


function validateDigitalRestriction(tableid) {
    var result = true;
    $(tableid + " select.RestrictionTypes").each(function () {
        var temp = $(this).val();
        if (temp == 2 || temp == 5) {
            var item = $($(tableid + " select.ConsentPeriodTypes")[$(tableid + " select.RestrictionTypes").index(this)]);
            if (!item.val()) {
                ShowRestrictionMsg(messageCommon.digitalConsentPrd.toString());
                result = false;
                return false;
            }
        }
        else if (temp == 1) {
            var item = $($(tableid + " select.RestrictionReason")[$(tableid + " select.RestrictionTypes").index(this)]);
            var txtitem = $($(tableid + " .txtRestrictionReason")[$(tableid + " select.RestrictionTypes").index(this)]);
            var hasTxtother = $(txtitem).is(":visible");
            if (hasTxtother) {
                var txtOtherValue = $(txtitem).val();
                if (!txtOtherValue) {
                    $(txtitem).addClass('input-validation-error');
                    ShowRestrictionMsg(messageCommon.digitalIncomplete.toString());
                    result = false;
                    return false;
                }
            }
            else {
                if (!item.val()) {
                    ShowRestrictionMsg(messageCommon.digitalIncomplete.toString());
                    result = false;
                    return false;
                }
            }
        }

        //return false;
    });

    return result;
}


function validateDigitalRestrictionCombination(tableid) {
    var result = true;
    var digitalUnique = new Array();
    var digitalcollection = new Array();
    $(tableid + " select.ConsentPeriodTypes").each(function () {
        var tmpContentdisabled = $(this).prop('disabled');
        var tempContentType = $(this).val();
        var tempUseType = $($(tableid + " select.UseTypes")[$(tableid + " select.ConsentPeriodTypes").index(this)]).val();
        var tempCommercialModel = $($(tableid + " select.CommercialModelTypes")[$(tableid + " select.ConsentPeriodTypes").index(this)]);
        var tempRestriction = $($(tableid + " select.RestrictionTypes")[$(tableid + " select.ConsentPeriodTypes").index(this)]).val();


        if (tmpContentdisabled == false) {
            if (tempContentType == 0 || tempUseType == 0 || tempCommercialModel.val() == 0 || tempRestriction == 0) {
                ShowRestrictionMsg(messageCommon.digitalIncomplete);
                result = false;
                return false;
            }
        }
        else {

            if (tempUseType == 0 || tempCommercialModel.val() == 0 || tempRestriction == 0) {
                ShowRestrictionMsg(messageCommon.digitalIncomplete);
                result = false;
                return false;
            }
        }

        //digitalcollection.push(tempContentType + tempUseType + tempCommercialModel.val());
        digitalcollection.push(tempUseType + tempCommercialModel.val());

        return true;

    });
    for (var row = 0; row < digitalcollection.length; row++) {

        var rowid = arrHasDuplicate(digitalcollection, row);
        if (rowid > 0) {
            var content = $($("select.UseTypes")[rowid]);
            popupdialogRightsDuplicatesave(content);
            result = false;
            return false;
        }
    }

    if (result != false) {
        $(tableid + " select.CommercialModelTypes").each(function () {
            var tempCommercialModel = $(this).val();
            if (tempCommercialModel == 4) {
                var tempCommercialAll = $(this);
                var tempUseType = $($(tableid + " select.UseTypes")[$(tableid + " select.CommercialModelTypes").index(this)]).val();
                var tempContentType = $($(tableid + " select.ConsentPeriodTypes")[$(tableid + " select.CommercialModelTypes").index(this)]).val();


                $(tableid + " select.ConsentPeriodTypes").each(function () {
                    var contentType = $(this).val();
                    var useType = $($(tableid + " select.UseTypes")[$(tableid + " select.ConsentPeriodTypes").index(this)]).val();
                    // if (contentType == tempContentType && useType == tempUseType)
                    //      digitalUnique.push(contentType + useType);
                    if (useType == tempUseType)
                        digitalUnique.push(useType);
                });

                if (digitalUnique.length > 1) {
                    for (var row = 0; row < digitalUnique.length; row++) {

                        var rowid = arrHasDuplicate(digitalUnique, row);
                        if (rowid > 0) {
                            popupdialogRightsDuplicatesave(tempCommercialAll);
                            result = false;
                            return false;
                        }
                    }

                }

            }
        });
    }

    return result;
}

function popupdialogRightsDuplicatesave(tempContentType) {

    objDialog.html('<div style=\'width: 100%; float: left;margin-top:10px;margin-left:12px;\' > <p>' + messageCommon.digitalUnique + '</p></div>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () {
        $(this).dialog('close');
        //  $('.sample').addClass('input-validation-error'); 
        tempContentType.addClass('input-validation-error'); tempContentType.focus();
    }
    }, close: function () { tempContentType.focus(); }
    });
    objDialog.dialog('open');
}
function arrHasDuplicate(arrayValues, id) {                          // finds any duplicate array elements using the fewest possible comparison
    var counter;
    var arrayLength;
    arrayLength = arrayValues.length;
    // to ensure the fewest possible comparisons
    for (counter = 0; counter < arrayLength; counter++) {                        // outer loop uses each item i at 0 through n
        if (counter != id && arrayValues)
            if (arrayValues[id] == arrayValues[counter]) return counter;
    }
    return -1;
}
function popupdialog(tempCommercialModel) {

    objDialog.html('<div style=\'width: 100%; float: left;margin-top:10px;margin-left:12px;\' >  <p>' + messageCommon.digitalCombination + '</p></div>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () {
        $(this).dialog('close');
        //  $('.sample').addClass('input-validation-error');
        tempCommercialModel.addClass('input-validation-error');
        tempCommercialModel.focus();
    }
    }, close: function () { tempCommercialModel.focus(); }
    });
    objDialog.dialog('open');
}
function popupdialogInComplete(tempCommercialModel) {

    objDialog.html('<div style=\'width: 100%; float: left;margin-top:10px;margin-left:12px;\' >  <p>' + messageCommon.digitalIncomplete + '</p></div>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () {
        $(this).dialog('close');
        //  $('.sample').addClass('input-validation-error');
        tempCommercialModel.addClass('input-validation-error');
        tempCommercialModel.focus();
    }
    }, close: function () { tempCommercialModel.focus(); }
    });
    objDialog.dialog('open');
}


function RightsWorkQueueScrollBar(digiGridName) {
    digiGridName = digiGridName.id;
    if ($('#' + digiGridName).length > 0) {
        var pagerHgt = $("#" + digiGridName + " .GridPager").height();
        var headerHgt = $("#" + digiGridName + " .GridHeader").height();
        var browsHgt = 0;
        if ($.browser.msie)
            browsHgt = 16;
        else
            browsHgt = 20;
        var reduceHgt = pagerHgt + headerHgt + browsHgt;

        var jtableTop = $("#" + digiGridName).offset().top;
        var topfootPos = $(".footer").offset().top;
        var totRecHeight = $("#" + digiGridName + "_Table").height() + reduceHgt;
        var tableHeight = topfootPos - jtableTop;
        var gridObj = $find(digiGridName);
        if (totRecHeight >= tableHeight)
            RightsWorkQueueScrollBarHeight(gridObj, tableHeight - reduceHgt);
        else
            RightsWorkQueueScrollBarHeight(gridObj, totRecHeight - reduceHgt + 20);
    }
}

function RightsWorkQueueScrollBarHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}
 
