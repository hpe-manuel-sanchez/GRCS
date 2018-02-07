/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var templateMaintenanceMessages = {
    editDigitalTemplate: "Edit Digital Restriction Template",
    templateName: "Template Name",
    viewValid: 'Please Select Row For Edit Template',
    editValid: 'Please Select one Row to edit Template',
    deleteValid: 'Please Select Row For Delete Template',
    ConfirmDeletion: "Do you want to delete the selected rows from the system?",
    confirm: "Confirm"
};

//Prepare jtable plugin
$(document).ready(function () {
    $('#jqTableTemplatePager select').change(function () {
        // alert("hi");
        HideWarningSuccess();
        pageSize = $('#jqTableTemplatePager select').val();
        $('#ContractTemplatesTable').jtable({ 'pageSize': pageSize });
        $('#ContractTemplatesTable').jtable('load');
    });

    reSizeContractTemplatePage();

    $(window).resize(function () {
        reSizeContractTemplatePage();
    });
    var rowIndex = -1;
    var pageSize = 25;
    $('#ContractTemplatesTable').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'TemplateName ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        selectOnRowClick: true,
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
            //            $('#TemplateAlerts').hide();
        },

        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("TemplateCount").innerHTML = "Contract Templates (" + rowIndex + ")";

            $('.jtable .jtable-no-data-row').show();
            $("#ContractTemplatesTable input").removeAttr("checked");
            $("#ContractTemplatesTable tr").removeClass("jtable-row-selected");
            $(".ui-jtable-loading").hide();
            setToolTip(this);
        },
        actions: {
            listAction: '/GCS/Contract/ContractTemplates'
        },

        fields: {
            TemplateName: {
                title: 'Template Name',
                columnResizable: true
            },
            TemplateId: {
                title: 'Template Id',
                list: false
            },
            ContractInfo_ContractStatus: {
                title: 'Contract Status',
                display: function (templates) {
                    var templateDescription = templates.record.ContractInfo.ContractStatus;
                    return templateDescription;
                },
                columnResizable: true
            },
            ContractInfo_ContractDescription: {
                title: 'Contract Description',
                display: function (templates) {
                    var templateDescription = templates.record.ContractInfo.ContractDescription;
                    return templateDescription;
                },
                columnResizable: true
            },
            ContractInfo_ClearanceCompanyCountry: {
                title: 'Clearance Admin Co.',
                display: function (templates) {
                    var clearanceCompany = templates.record.ContractInfo.ClearanceCompanyCountry;
                    return clearanceCompany;
                },
                columnResizable: true
            },
            ContractInfo_RightsTypeName: {
                title: 'Rights Type',
                display: function (templates) {
                    var templateDescription = templates.record.ContractInfo.RightsTypeName;
                    return templateDescription;
                },
                columnResizable: true
            },
            ContractInfo_UMGSigningCompany: {
                title: 'Signing Company',
                display: function (templates) {
                    var templateDescription = templates.record.ContractInfo.UmgSigningCompany;
                    return templateDescription;
                },
                columnResizable: true
            },
            ContractInfo_PCNoticeCountryCompany: {
                title: 'P/C Notice Company',
                display: function (templates) {
                    var templateDescription = templates.record.ContractInfo.PcNoticeCountryCompany;
                    return templateDescription;
                },
                columnResizable: true
            },
            ContractInfo_ArtistName: {
                title: 'Artist',
                display: function (templates) {
                    var templateDescription = templates.record.ContractInfo.ArtistName;
                    return templateDescription;
                },
                columnResizable: true
            },
            ContractInfo_ContractingParty: {
                title: 'Contracting Party',
                display: function (templates) {
                    var templateDescription = templates.record.ContractInfo.ContractingParty;
                    return templateDescription;
                },
                columnResizable: true
            }
        },
        selectionChanged: function () {
            //Get all selected rows
            HideWarningSuccess();
            var $selectedRows = $('#ContractTemplatesTable').jtable('selectedRows');

            $('#templateSelectList').empty();
            if ($selectedRows.length == 0) {
                //No rows selected
                $('#templateSelectList').append(messageCommon.viewValid);
            }
        }
    });

    $('#DeleteTemplate').unbind("click").click(function () {
        var $selectedRows = $('#ContractTemplatesTable').jtable('selectedRows');
        var templateType = "C";
        if ($selectedRows.length > 0) {
            var templateIds = '';
            //Show selected rows
            $selectedRows.each(function () {
                var record = $(this).data('record');
                templateIds = templateIds + ',' + record.TemplateId;
            });
        }
        $.get('/GCS/Contract/DeleteTemplates', { templateIds: templateIds, templateType: templateType })
                    .error(function () {
                        objDialog.html('<p>' + messageCommon.error + '</p>');
                        //Open Dialog
                        objDialog.dialog('open', { title: messageCommon.warningTitle });
                    });
    });

    $('#DeleteTemplate').unbind("click").click(function () {
        var $selectedRows = $('#ContractTemplatesTable').jtable('selectedRows');
        var templateType = "C";
        if ($selectedRows.length > 0) {
            var templateIds = '';
            //Show selected rows
            $selectedRows.each(function () {
                var record = $(this).data('record');
                templateIds = templateIds + ',' + record.TemplateId;
            });
            objDialog.html(templateMaintenanceMessages.ConfirmDeletion);
            objDialog.dialog('open');
            objDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        e.preventDefault();
                        window.location.href = "/GCS/Contract/DeleteTemplates?templateIds=" + templateIds.toLocaleString() + "&templateType=" + templateType;
                    }, 'Cancel': function () {
                        $(this).dialog('close');
                    }
                }
            });
        }
        else {
            //  displayDialog("Warning", templateMaintenanceMessages.deleteValid);
            ShowWarning(templateMaintenanceMessages.deleteValid);
            return false;
        }

        return false;
    });

    var objDialog = $('<div id="DeleteConfirm"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: templateMaintenanceMessages.confirm,
            show: 'clip',
            hide: 'clip',
            width: 500
        });

    $('#EditTemplate').unbind("click").click(function () {
        var $selectedRows = $('#ContractTemplatesTable').jtable('selectedRows');
        var templateId = $('#TemplateSelectList')[0].innerHTML;
        if ($selectedRows.length > 0) {
            //Show selected rows
            if ($selectedRows.length > 1) {
                // displayDialog("Warning", templateMaintenanceMessages.editValid);
                ShowWarning(templateMaintenanceMessages.editValid);
                return false;
            }
            else {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    templateId = record.TemplateId;
                });
            }
        }
        else {
            //displayDialog("Warning", templateMaintenanceMessages.viewValid);
            ShowWarning(templateMaintenanceMessages.viewValid);
            return false;
        }

        if (templateId != null && templateId != '')
            window.location.href = "/GCS/Contract/GetContractTemplate?id=" + templateId + "&editRequestForm=Maintenance";
        else
            //  displayDialog("Warning", templateMaintenanceMessages.editValid);
            ShowWarning(templateMaintenanceMessages.editValid);
        return false;
    });

    $('#ContractTemplatesTable').jtable('load');
    reSizeContractWorkPage();
});

//Get the specific member variable value from the template list JSON object as it is nested
function GetValuesFromJSON(memberName, templateListObject) {
    var result = templateListObject.record.ContractInfo.memberName;
    alert(result);
    return result;
}

$(document).ready(function () {
    var pageSize = 25;
    var rowIndex = -1;
    $('#DigitalRestrictionsTable').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'TemplateName ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        selectOnRowClick: true,
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
        },

        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("DigitalCount").innerHTML = "Digital Restriction Templates (" + rowIndex + ")";
            $('.jtable .jtable-no-data-row').show();
            $("#DigitalRestrictionsTable input").removeAttr("checked");
            $("#DigitalRestrictionsTable tr").removeClass("jtable-row-selected");
            $(".ui-jtable-loading").hide();
            setToolTip(this);
        },
        actions: {
            listAction: '/GCS/Contract/DigitalRestrictionTemplates'
        },

        fields: {
            TemplateId: {
                title: 'Template Id',
                list: false
            },
            TemplateName: {
                title: 'Template Name',
                width: '98%'
            }
        },

        selectionChanged: function () {
            //Get all selected rows
            HideWarningSuccess();
            var $selectedRows = $('#DigitalRestrictionsTable').jtable('selectedRows');
            $('#contractSelectList').empty();
            return false;
        }
    });

    $('#DigitalRestrictionsTable').jtable('load');

    //link click
    $('#editDigitalTemplate').unbind("click").click(function () {
        var templateId = -1;
        var $selectedRows = $('#DigitalRestrictionsTable').jtable('selectedRows');
        if ($selectedRows.length > 0) {
            if ($selectedRows.length > 1) {
                // displayDialog("Warning", templateMaintenanceMessages.editValid);
                ShowWarning(templateMaintenanceMessages.editValid);
            } else {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    templateId = record.TemplateId;
                    window.location = "/GCS/Contract/GetDigitalTemplate/" + record.TemplateId;
                });
            }
        }
        else {
            //No rows selected
            //  displayDialog("Warning", templateMaintenanceMessages.viewValid);
            ShowWarning(templateMaintenanceMessages.editValid);
            return false;
        }
        return false;
    });

    $('.cancelButton').unbind("click").click(function (e) {
        e.preventDefault();
        objDialog.html(contractMessages.cancelValid);
        objDialog.dialog('open');
        objDialog.dialog({
            buttons:
                {
                    'Yes': function (e) {
                        e.preventDefault();
                        window.location.href = "/GCS/Contract/DigitalRestrictionTemplates";
                    }, 'Cancel': function () {
                        $(this).dialog('close');
                    }
                }
        });
        return false;
    });

    $('#deleteDigitalTemplate').unbind("click").click(function (e) {
        e.preventDefault();
        var $selectedRows = $('#DigitalRestrictionsTable').jtable('selectedRows');
        var templateType = "D";
        if ($selectedRows.length > 0) {
            var templateIds = '';
            //Show selected rows
            $selectedRows.each(function () {
                var record = $(this).data('record');
                templateIds = templateIds + ',' + record.TemplateId;
            });
            objDialog.html(templateMaintenanceMessages.ConfirmDeletion);
            objDialog.dialog('open');
            objDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        e.preventDefault();
                        window.location.href = "/GCS/Contract/DeleteTemplates?templateIds=" + templateIds + "&templateType=" + templateType;
                    }, 'Cancel': function () {
                        $(this).dialog('close');
                    }
                }
            });
        } else {
            // displayDialog("Warning", templateMaintenanceMessages.deleteValid);
            ShowWarning(templateMaintenanceMessages.deleteValid);
            return false;
        }

        return false;
    });
});

$(document).ready(function () {
    $('#jqTableDigiPager select').change(function () {
        HideWarningSuccess();
        pageSize = $('#jqTableDigiPager select').val();
        $('#DigitalRestrictionsTable').jtable({ 'pageSize': pageSize });
        $('#DigitalRestrictionsTable').jtable('load');
    });

    $(window).resize(function () {
        reSizeContractWorkPage();
    });
});
function reSizeContractWorkPage() {
    var h = $(window).height();
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".digitalRestriction").css('height', h - 160);
    else
        $(".digitalRestriction").css('height', h - 200);
    // $("#ContractTemplatesTable").css('height', h - 100);
    //  $(".jtable-main-container").css('height', h - 200);
    // $(".digitalRestriction").css('height', h - 148);
}
function reSizeContractTemplatePage() {
    var h = $(window).height();
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".contractTemplates").css('height', h - 160);
    else
        $(".contractTemplates").css('height', h - 200);
    //$("#jqgrid").css('height', h - 175);
    // $(".jtable-main-container").css('height', h - 175);

    // $(".contractTemplates").css('height', h - 145);
}
var objDialog = $('<div id="DeleteConfirm"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: templateMaintenanceMessages.confirm,
            show: 'clip',
            hide: 'clip',
            width: 500
        });