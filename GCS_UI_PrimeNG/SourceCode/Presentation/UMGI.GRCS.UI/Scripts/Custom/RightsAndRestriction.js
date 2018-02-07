/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var rightsMessages = {
    severError: "Error updating server...",
    templateValidation: "Please provide Clearance Admin Company to save the Digital Restriction template",
    templateName: "Please enter Digital Restriction Template Name",
    templateTitle: "Enter Digital Restriction Template Name",
    selectDigital: "Please add  atleast one Digital Restriction",
    saveTemplateTitle: "Save Template",
    TemplateTitle: "Template Name"
};
var saveTemplateDialog = $('<div id="TemplateSave"></div>')
       .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            width: 500
        });
var isTemplateMaintenance = false;
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

$(function () {
    var h1 = $(window).height();
    $(".scrollContract").css('height', h1 - 205);

    $('#btnAddDigitalTemplate').click(function (e) {
        e.preventDefault();

        var digitalVal = $("#RightsForm").serialize();

        $('#RightsForm input[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $('#RightsForm select[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $.post('/GCS/Contract/AddDigitalRestrictions', digitalVal, function (data) {
            $('#divRightsTab').html(data);
            $(".scrollContract").animate({ scrollTop: $(document).height() + 1000 }, 1);
        })

            .error(function () {
                objDialog.html('<p>' + messageCommon.error + '</p>');
                //Open Dialog
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });

        //scrollTo(1000, 1000);
    });

    $('#btnAddDigitalTemplateMtn').click(function (e) {
        e.preventDefault();
        var digitalVal = $("#DigitalRightsForm").serialize();
        $.post('/GCS/Contract/AddDigitalRestrictions', digitalVal, function (data) {
            $('#divDigitalRestriction').html(data);
        })
            .error(function () {
                objDialog.html('<p>' + messageCommon.error + '</p>');
                //Open Dialog
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });
    });
});

$(function () {
    $('#btnRemoveDigitalTemplate').click(function (e) {
        e.preventDefault();
        var count = 0;
        $("#divDigitalRestriction tr").each(function () {
            if ($(this).find('td input:checked').length > 0) {
                $(this).remove();
                count++;
                return;
            }
        });

        if (count == 0) {
            var newDialog = $('<div id="MenuDialog">\             <p>' + messageCommon.removeDigital + '</p>\         </div>');
            //Show Dialog
            newDialog.dialog({
                modal: true,
                title: messageCommon.warningTitle,
                show: 'clip',
                hide: 'clip',
                width: 300,
                buttons: { 'Ok': function () { $(this).dialog('close'); } },
                close: function () {
                }
            });
        }

        var digitalVal = $("#RightsForm").serialize();

        $('#RightsForm input[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $('#RightsForm select[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $.post('/GCS/Contract/RemoveDigitalRestrictions/', digitalVal, function () {
            //Do nothing
        })
            .error(function () {
                objDialog.html('<p>' + rightsMessages.severError + '</p>');
                //Open Dialog
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });
    });

    $('#btnRemoveDigitalTemplateMtn').click(function (e) {
        e.preventDefault();
        var count = 0;
        $("#divDigitalRestriction tr").each(function () {
            if ($(this).find('td input:checked').length > 0) {
                $(this).remove();
                count++;
                return;
            }
        });

        if (count == 0) {
            var newDialog = $('<div id="MenuDialog">\             <p>'
                + messageCommon.removeDigital + '</p>\         </div>');
            //Show Dialog
            newDialog.dialog({
                modal: true,
                title: messageCommon.warningTitle,
                show: 'clip',
                hide: 'clip',
                width: 300,
                buttons: { 'Ok': function () { $(this).dialog('close'); } },
                close: function () {
                }
            });
            return false;
        }

        var digitalVal = $("#DigitalRightsForm").serialize();

        $('#DigitalRightsForm input[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $('#DigitalRightsForm select[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $.post('/GCS/Contract/RemoveDigitalRestrictions/', digitalVal, function () {
            //Do nothing
        })
            .error(function () {
                objDialog.html('<p>' + rightsMessages.severError + '</p>');
                //Open Dialog
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });
        return false;
    });
});

$(function () {
    $('#btnCancel').unbind("click").click(function (e) {
        e.preventDefault();
        var cancel;
        if (document.URL.indexOf('GetDigitalTemplate') >= 0) {
            cancel = confirm('Cancel Editing Template?');
            if (cancel)
                window.location.href = "/GCS/Contract/DigitalRestrictionTemplates";
            else
                return false;
            return false;
        }
        else {
            cancel = confirm(contractMessages.cancelValid);
            if (cancel)
                window.location.href = "/GCS/Home/Index";
            else
                return false;
            return false;
        }
    });

    $('#btnSaveExistingDigitalTemplate').click(function (e) {
        e.preventDefault();
        var isMaintenance = $('#IsMaintenance').val();
        var isNew = false;
        if (isMaintenance == 'True') {
            isTemplateMaintenance = true;
        } else {
            if (validateClearanceCompany() == false)
                return false;
        }

        if (validateDigitalRestriction() == false)
            return false;

        if (validateDigitalRestrictionCombination() == false)
            return false;

        if ($("#divDigitalRestriction td").length > 0) {
            $("#tabs").tabs('option', 'selected');
        } else {
            var newDialog = $('<div id="MenuDialog">\             <p>'
                + rightsMessages.selectDigital + '</p>\         </div>');
            //Show Dialog
            newDialog.dialog({
                modal: true,
                title: messageCommon.warningTitle,
                show: 'clip',
                hide: 'clip',
                width: 300,
                buttons: { 'Ok': function () { $(this).dialog('close'); } }
            });
            return false;
        }

        saveDigitalRestriction(isNew);
        return false;
    });

    $('#btnSaveDigitalTemplate').click(function (e) {
        e.preventDefault();

        var isMaintenance = $('#IsMaintenance').val();
        var isNew = true;
        if (isMaintenance == 'True') {
            isTemplateMaintenance = true;
        } else {
            if (validateClearanceCompany() == false)
                return false;
        }

        if (validateDigitalRestriction() == false)
            return false;

        if (validateDigitalRestrictionCombination() == false)
            return false;

        if ($("#divDigitalRestriction td").length > 0) {
            $("#tabs").tabs('option', 'selected');
        } else {
            var newDialog = $('<div id="MenuDialog">\             <p>'
                + rightsMessages.selectDigital + '</p>\         </div>');
            //Show Dialog
            newDialog.dialog({
                modal: true,
                title: messageCommon.warningTitle,
                show: 'clip',
                hide: 'clip',
                width: 300,
                buttons: { 'Ok': function () { $(this).dialog('close'); } }
            });
            return false;
        }

        saveDigitalRestriction(isNew);
        return false;
    });
});

function validateClearanceCompany() {
    if ($('#Contract_ClearanceCompanyCountry').val() == null || $('#Contract_ClearanceCompanyCountry').val() == '') {
        $('#Contract_ClearanceCompanyCountry').addClass('input-validation-error');
        objDialog.html(rightsMessages.templateValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        objDialog.dialog('open');
        return false;
    }
    else {
        $('#Contract_ClearanceCompanyCountry').removeClass('input-validation-error');
        return true;
    }
}

function validateDigitalRestriction() {
    var result = true;
    $("select.DigitalrestrictionList").each(function () {
        var temp = $(this).val();
        if (temp == 2 || temp == 5) {
            var item = $($("select.DigitalconsentList")[$("select.DigitalrestrictionList").index(this)]);
            if (item.val() == 0) {
                objDialog.html('<p>' + messageCommon.digitalConsentPrd + '</p>');
                objDialog.dialog('option', { title: messageCommon.warningTitle });
                objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); item.focus(); } }, close: function () { item.focus(); } });
                //Show Dialog
                objDialog.dialog('open');
                result = false;
                return false;
            }
        }
        //return false;
    });

    return result;
}

var saveDigitalTemplateDialog = $('<div id="TemplateSave"></div>')
       .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            width: 500,
            close: function () {
                $(this).remove();
            }
        });

function saveDigitalRestriction(isNew) {
    var digitalVal = $("#RightsForm").serialize();
    var templateName = $('#TemplateName').val();
    var templateId = $('#TemplateId').val();
    var ClearanceAdminId = $('#ClearanceAdmin').val();
    var isNewFlag = isNew;
    var hiddenTemplateNameClear = templateName;
    if (isTemplateMaintenance == true) {
        digitalVal = $("#DigitalRightsForm").serialize();

        saveDigitalTemplateDialog.html(
                            '<p/>  <input type=\'radio\' id=\'save\' class="target" name="saveGroup" checked=\'checked\'/> ' + ' Save Existing Template' +
                                '<p>' +
                                    '<input type=\'radio\' id=\'saveNew\' name="saveGroup" />' + ' Save as New Template' +
                                        '<p>' +
                                            rightsMessages.TemplateTitle +
                                                '</p> <input type=\'text\' name="txtBoxTemplateName" maxlength=100 id="txtBoxTemplate_Name" width=\'100px\' value='
                                                    + templateName + ' disabled=\'disabled\'></input>');
        saveDigitalTemplateDialog.dialog('option',
                            {
                                title: rightsMessages.saveTemplateTitle,
                                width: '500px',
                                close: function () {
                                    //$(this).remove(); // ensures any form variables are reset.
                                },
                                buttons:
                                    {
                                        'Save': function () {
                                            var optionCheckAttr = $("input[@id=saveGroup]:checked").attr('id');
                                            templateName = $('input:text[name=txtBoxTemplateName]').val();
                                            if (optionCheckAttr == 'saveNew') {
                                                digitalVal = digitalVal + '&Right.DigitalTemplateId=' + -1;
                                                digitalVal = digitalVal + '&isNewTemplate=' + true;
                                            } else {
                                                digitalVal = digitalVal + '&Right.DigitalTemplateId=' + templateId;
                                                digitalVal = digitalVal + '&isNewTemplate=' + false;
                                            }

                                            if (templateName == '') {
                                                $(this).find('input.txtBoxTemplateName').addClass('input-validation-error');
                                                displayDialog("Warning!", 'Please enter Template Name');
                                                objDialog.dialog('open');
                                                return false;
                                            }
                                            $(this).find('input.txtBoxTemplateName').removeClass('input-validation-error');
                                            var DigitalPopup = $(this);

                                            //                                            digitalVal = digitalVal.replace("Right.DigitalTemplateId", "Right.DigitalTemplateId123");
                                            //                                            digitalVal = digitalVal.replace("Right.DigitalTemplateName", "Right.DigitalTemplateName123");
                                            digitalVal = digitalVal + '&Right.DigitalTemplateName=' + templateName;
                                            digitalVal = digitalVal + '&ClearanceCompanyCountryId=' + ClearanceAdminId;
                                            //--
                                            $.post('/GCS/Contract/SaveDigitalRestrictionsTemplate', digitalVal, function (data) {
                                                var status = $(data).find('#digitalTemplateStatus').val();
                                                var temp = status.substring(status.length - 1, status.length);
                                                if (temp.length > 0 && temp == "!") {
                                                    $('input:text[name=txtBoxTemplateName]').val(templateName);
                                                }
                                                else {
                                                    DigitalPopup.dialog('close');
                                                    DigitalPopup.remove();
                                                }

                                                $('#DigitalRightsForm').html(data);
                                            });

                                            return false;
                                        }
                                    }
                            });

        saveDigitalTemplateDialog.dialog('option', {
            open: function () {
                $(this).find('input').focus();
                saveDigitalTemplateDialog.click(function () {
                    var optionCheckAttr = $("input[@id=saveGroup]:checked").attr('id');
                    hiddenTemplateNameClear = $('input:text[name=txtBoxTemplateName]').val();
                    if (optionCheckAttr == 'saveNew') {
                        $('input:text[name=txtBoxTemplateName]').prop("disabled", false);
                        $('input:text[name=txtBoxTemplateName]').val('');
                        $('#txtBoxTemplate_Name').focus();
                    } else {
                        $('input:text[name=txtBoxTemplateName]').prop("disabled", true);
                        templateName = $('#TemplateName').val();;
                        $('input:text[name=txtBoxTemplateName]').val(templateName);
                    }
                });
            }
        });
        saveDigitalTemplateDialog.dialog('open');
        saveDigitalTemplateDialog.click();
    }
    else {
        $('#RightsForm input[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $('#RightsForm select[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        var ClearanceAdmin = $('#ClearanceCompanyId').val();
        if ($('#ClearanceCompanyId').val().length == 0) {
            ClearanceAdmin = encodeURIComponent($('#Contract_ClearanceCompanyCountryId').val());
        }
        digitalVal = digitalVal + '&ClearanceCompanyName=' + encodeURIComponent($('#Contract_ClearanceCompanyCountry').val());
        digitalVal = digitalVal + '&ClearanceCompanyCountryId=' + ClearanceAdmin;

        saveDigitalTemplateDialog.html(
                            '<p></p> ' + rightsMessages.templateTitle +
                                                '</p> <input type=\'text\' name="txtBoxTemplateName" maxlength=100 id="txtBoxTemplate_Name" width=\'100px\' ></input>');
        saveDigitalTemplateDialog.dialog('option',
                            {
                                title: rightsMessages.templateName,
                                width: '500px',
                                close: function () {
                                    //$(this).remove(); // ensures any form variables are reset.
                                },
                                buttons:
                                    {
                                        'Save': function () {
                                            templateName = $('input:text[name=txtBoxTemplateName]').val();

                                            //                                            digitalVal = digitalVal.replace("Right.DigitalTemplateId", "Right.DigitalTemplateId123");
                                            //                                            digitalVal = digitalVal.replace("Right.DigitalTemplateName", "Right.DigitalTemplateName123");

                                            digitalVal = digitalVal + '&Right.DigitalTemplateId=' + -1;

                                            digitalVal = digitalVal + '&isNewTemplate=' + true;

                                            if (templateName == '') {
                                                $(this).find('input.txtBoxTemplateName').addClass('input-validation-error');
                                                displayDialog("Warning!", 'Please enter Template Name');
                                                objDialog.dialog('open');
                                                return false;
                                            }
                                            $(this).find('input.txtBoxTemplateName').removeClass('input-validation-error');
                                            var popup = $(this);
                                            digitalVal = digitalVal + '&Right.DigitalTemplateName=' + templateName;

                                            //--
                                            $.post('/GCS/Contract/SaveDigitalRestrictionsTemplate', digitalVal, function (data) {
                                                var status = $(data).find('#templateSaveStatus').val();
                                                var temp = status.substring(status.length - 1, status.length);
                                                if (temp.length > 0 && temp == "!") {
                                                    $('input:text[name=txtBoxTemplateName]').val(templateName);
                                                }
                                                else {
                                                    popup.dialog('close');
                                                    popup.remove();
                                                }
                                                $('#divRightsTab').html(data);
                                            });

                                            return false;
                                        }
                                    }
                            });

        saveDigitalTemplateDialog.dialog('option', { open: function () { $(this).find('input').focus(); } });
        saveDigitalTemplateDialog.dialog('open');
    };

    return false;
}

function validateDigitalRestrictionCombination() {
    var result = true;
    var digitalcollection = new Array();

    $("select.ContentTypeList").each(function () {
        var tempContentType = $(this).val();
        var tempUseType = $($("select.UseTypeList")[$("select.ContentTypeList").index(this)]).val();
        var tempCommercialModel = $($("select.CommercialModelList")[$("select.ContentTypeList").index(this)]);

        if (tempContentType == 1 || tempContentType == 2) {
            if (tempUseType == 1 && tempCommercialModel.val() == 3) {
                popupdialog($(this));
                result = false;
                return false;
            }
            else if (tempUseType == 3) {
                popupdialog($(this));
                result = false;
                return false;
            }
            else if (tempCommercialModel.val() == 4 && tempUseType != 2) {
                popupdialog($(this));
                result = false;
                return false;
            }
        }
        else if (tempContentType == 3) {
            if (tempUseType == 1 && tempCommercialModel.val() == 1) {
                result = true;
                return true;
            }
            else if (tempUseType == 3 && tempCommercialModel.val() == 1) {
                result = true;
                return true;
            }
            else {
                popupdialog($(this));
                result = false;
                return false;
            }
        }

        if (tempContentType == 0 || tempUseType == 0 || tempCommercialModel.val() == 0) {
            popupRigthsselectvalue($(this));
            result = false;
            return false;
        }

        digitalcollection.push(tempContentType + tempUseType + tempCommercialModel.val());
        // return false;
    });

    for (var row = 0; row < digitalcollection.length; row++) {
        var rowid = arrHasDuplicate(digitalcollection, row);
        if (rowid > 0) {
            var content = $($("select.ContentTypeList")[rowid]);
            popupdialogRightsDuplicatesave(content);
            return false;
        }
    }

    return result;
}

function popupRigthsselectvalue(tempContentType) {
    objDialog.html('<p>' + messageCommon.digitalIncomplete + '</p>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); tempContentType.focus(); } }, close: function () { tempContentType.focus(); } });
    objDialog.dialog('open');
}

function popupdialogRightsDuplicatesave(tempContentType) {
    objDialog.html('<p>' + messageCommon.digitalUnique + '</p>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); tempContentType.focus(); } }, close: function () { tempContentType.focus(); } });
    objDialog.dialog('open');
}

function popupdialog(tempCommercialModel) {
    objDialog.html(' <p>' + messageCommon.digitalCombination + '</p>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); tempCommercialModel.focus(); } }, close: function () { tempCommercialModel.focus(); } });
    objDialog.dialog('open');
}

$(function () {
    $('select.RightsDealrestrictedList').change(function () {
        var temp = $(this).val();

        if (temp == 2) {
            $($("select.DealrestrictionList")[$("select.RightsDealrestrictedList").index(this)]).val(0);
            $($("select.DealrestrictionList")[$("select.RightsDealrestrictedList").index(this)]).attr("disabled", true);
        }
        else {
            $($("select.DealrestrictionList")[$("select.RightsDealrestrictedList").index(this)]).removeAttr("disabled");
        }
    });
});

$(function () {
    $($("select.RightsAcquiredrestrictedList")[1]).change(function () {
        var temp = $(this).val();
        if (temp == 2) {
            $('#divDigitalRestrictionContainer').css('display', 'none');
            reSize();
        }
        else {
            $('#divDigitalRestrictionContainer').css('display', 'block');
        }
    });
});

// The below logic enables DigitalConsentList only when DigitalRestrictionList is 'Consult' or 'Consent Required' as part of UAT Bug RC:22
$(function () {
    $('select.DigitalrestrictionList').change(function () {
        var temp = $(this).val();

        if (temp == 1 || temp == 3 || temp == 4) {
            $($("select.DigitalconsentList")[$("select.DigitalrestrictionList").index(this)]).attr("disabled", true);
            $($("select.DigitalconsentList")[$("select.DigitalrestrictionList").index(this)]).val('0');
        }
        else {
            $($("select.DigitalconsentList")[$("select.DigitalrestrictionList").index(this)]).attr("disabled", false);
        }
    });
});

function arrHasDuplicate(arrayValues, id) {                          // finds any duplicate array elements using the fewest possible comparison
    var counter;
    var arrayLength;
    arrayLength = arrayValues.length;
    // to ensure the fewest possible comparisons
    for (counter = 0; counter < arrayLength; counter++) {                        // outer loop uses each item i at 0 through n
        if (counter != id)
            if (arrayValues[id] == arrayValues[counter]) return counter;
    }
    return -1;
}

$(document).ready(function () {
    $($("select.RightsAcquiredrestrictedList")[1]).each(function () {
        var temp = $(this).val();
        if (temp == 2) {
            $('#divDigitalRestrictionContainer').css('display', 'none');
        }
        else {
            $('#divDigitalRestrictionContainer').css('display', 'block');
        }
    });

    $('select.RightsDealrestrictedList').each(function () {
        var temp = $(this).val();

        if (temp == 2) {
            $($("select.DealrestrictionList")[$("select.RightsDealrestrictedList").index(this)]).val(0);
            $($("select.DealrestrictionList")[$("select.RightsDealrestrictedList").index(this)]).attr("disabled", true);
        }
        else {
            $($("select.DealrestrictionList")[$("select.RightsDealrestrictedList").index(this)]).removeAttr("disabled");
        }
    });

    $('select.DigitalrestrictionList').each(function () {
        var temp = $(this).val();

        if (temp == 1 || temp == 3 || temp == 4) {
            $($("select.DigitalconsentList")[$("select.DigitalrestrictionList").index(this)]).attr("disabled", true);
        }
        else {
            $($("select.DigitalconsentList")[$("select.DigitalrestrictionList").index(this)]).attr("disabled", false);
        }
    });
});

$(function () {
    $('select#DigitalTemplate').change(function () {
        var templateId = $(this).val();          //

        var digitalVal = $("#RightsForm").serialize();

        digitalVal = digitalVal + '&TemplateId' + '=' + templateId;
        $('#RightsForm input[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $('#RightsForm select[disabled]').each(function () {
            digitalVal = digitalVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $.post('/GCS/Contract/LoadDigitalRestrictionFromTemplate', digitalVal, function (data) {
            $('#divDigitalRestriction table.sample').html($(data).find('#divDigitalRestriction table.sample').html());
            $(".scrollContract").animate({ scrollTop: $(document).height() + 1000 }, 1);
        });
    });
});

$('#accordion .accRightsHead').click(function (e) {
    e.preventDefault();
    $(this).next().toggle();
    // $(this)._toggleClass('iconBottom');
    $(this).find('a').toggleClass('iconBottom');
    return false;
}).next().show();