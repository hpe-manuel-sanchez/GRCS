/// <reference path="../Scripts/Custom/LayoutRoot.js" />
/// <reference path="../Scripts/Custom/ContractTab.js" />
var splitDealMessages = {
    select: "Select",
    artist: "Artist",
    cntrctParty: "Contracting Party",
    workflowStatus: "Workflow Status",
    contractStatus: "Contract Status",
    cntrctDesc: "Contract Description",
    clearanceAdminComp: "Clearance Admin Co",
    cmncmntDate: "Commencement Date",
    territorialRights: "Territorial Rights",
    rightsPeriod: "Rights Period",
    cntrctId: "Contract ID",
    splitDealValid: "Please select a contract to link as a split deal"
};

$(document).ready(function () {
    //Popup for SpltDeal check}

    var pageSize = 25;

    $('#splitGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true,

        actions: {
            listAction: '/GCS/Contract/SplitDeal'
        },
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () {
            $('.jtable .jtable-no-data-row').show();

            //  setToolTip(this);
        },
        fields: {
            ArtistName: {
                title: splitDealMessages.artist
            },
            ContractingParty: {
                title: splitDealMessages.cntrctParty
            },
            WorkflowStatus: {
                title: splitDealMessages.workflowStatus
            },
            ContractStatus: {
                title: splitDealMessages.contractStatus
            },
            ContractDescription: {
                title: splitDealMessages.cntrctDesc
            },
            ClearanceCompanyCountry: {
                title: splitDealMessages.clearanceAdminComp
            },
            ContractCommencementDate: {
                title: splitDealMessages.cmncmntDate,
                type: 'date',
                displayFormat: 'dd M yy'
            },
            TerritorialRights: {
                title: splitDealMessages.territorialRights
            },
            RightsPeriod: {
                title: splitDealMessages.rightsPeriod
            },
            ContractId: {
                title: splitDealMessages.cntrctId
            }
        },

        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
            $('#splitWarning').hide();
            var $selectedRows = $('#splitGrid').jtable('selectedRows');
            $('#splitList').empty();
            $('#splitActualParent').empty();
            $('#splitDummyParent').empty();

            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    if ($('#splitList')[0].innerHTML == "") {
                        $('#splitList').append(record.ContractId);
                    }
                    else {
                        $('#splitList').append(",");
                        $('#splitList').append(record.ContractId);
                    }

                    if (record.ParentContractId != 0) {
                        if ($('#splitActualParent')[0].innerHTML == "") {
                            $('#splitActualParent').append(record.ParentContractId);
                        }
                        else {
                            $('#splitActualParent').append(",");
                            $('#splitActualParent').append(record.ParentContractId);
                        }
                    }

                    if (record.DummyParentId != null) {
                        if ($('#splitDummyParent')[0].innerHTML == "") {
                            $('#splitDummyParent').append(record.DummyParentId);
                        }
                        else {
                            $('#splitDummyParent').append(",");
                            $('#splitDummyParent').append(record.DummyParentId);
                        }
                    }

                    var recdisplay = document.getElementById('splitList');
                    var recParentdisplay = document.getElementById('splitActualParent');
                    var recDummydisplay = document.getElementById('splitDummyParent');

                    recdisplay.style.display = 'none';
                    recParentdisplay.style.display = 'none';
                    recDummydisplay.style.display = 'none';
                });
            }
            else {
                //No rows selected
                $('#splitList').empty();
            }
        }
    });

    $('#splitGrid').jtable('load');

    $('#jqTablePager select').change(function () {
        $('#splitWarning').hide();
        pageSize = $('#jqTablePager select').val();
        $('#splitGrid').jtable({ 'pageSize': pageSize });
        $('#splitGrid').jtable('load');
    });

    reSizeContractWorkPage();

    $(window).resize(function () {
        reSizeContractWorkPage();
    });

    $('#splitCancel').click(function (e) {
        //        $('#splitWarning').hide();
        e.preventDefault();
        var formValues = GetFormValues();
        formValues = formValues + '&SplitContractId' + '=' + "Cancel";
        $('#splitDealPopup').dialog('close');
        $.post('/GCS/Contract/SaveContract', formValues, function (data) {
            $('#divContractTab').html(data);
            reSize();
            DecodeTerritorial();
        })
                                .error(function () {
                                    DecodeTerritorial();
                                    objDialog.html('<p>' + messageCommon.error + '</p>');
                                    //Open Dialog
                                    objDialog.dialog('open', { title: messageCommon.warningTitle });
                                });
        return false;
    });
});

function reSizeContractWorkPage() {
    var h = $(window).height();
    $("#splitGrid").css('height', h - 300);
    $(".jtable-main-container").css('height', h - 300);
}

$('.duplicateButton').click(function (e) {
    //    $('#splitWarning').hide();
    e.preventDefault();
    var contractId = $('#splitList')[0].innerHTML;
    if (contractId == "" || contractId == splitDealMessages.splitDealValid) {
        window.location.href = "/GCS/Home/Index";
    }
    return false;
});

$('#splitDealBtn').click(function () {
    var formValues = GetFormValues();
    var contractId = $('#splitList')[0].innerHTML;

    var parentContractId = $('#splitActualParent')[0].innerHTML;
    var dummyContractId = $('#splitDummyParent')[0].innerHTML;

    var parentcontractArray = parentContractId.split(',');
    var count;
    var result = 0;
    for (count = 0; count < parentcontractArray.length; count++) {
        if (parentcontractArray[0] != parentcontractArray[count]) {
            result = 1;
        }
    }

    if (result == 1) {
        alert("You can't join the new contract with the selected contracts under a Split Deal. Please contact the System Administrator for help");
        return false;
    }

    var dummycontractArray = dummyContractId.split(',');
    result = 0;
    for (count = 0; count < dummycontractArray.length; count++) {
        if (dummycontractArray[0] != dummycontractArray[count]) {
            result = 1;
        }
    }

    if (result == 1) {
        alert("You can't join the new contract with the selected contracts under a Split Deal. Please contact the System Administrator for help");
        return false;
    }

    formValues = formValues + '&SplitContractId' + '=' + contractId;

    if (contractId != null && contractId != '') {
        $('#hiddenSplitValidation').hide();
        $('#splitDealPopup').dialog('close');
        $.post('/GCS/Contract/SaveContract', formValues, function (data) {
            $('#divContractTab').html(data);
            reSize();
            DecodeTerritorial();
        })
                                .error(function () {
                                    DecodeTerritorial();
                                    objDialog.html('<p>' + messageCommon.error + '</p>');
                                    //Open Dialog
                                    objDialog.dialog('open', { title: messageCommon.warningTitle });
                                });
    }
    else {
        // $('#hiddenSplitValidation').show();
        $('#SplitAlert').empty();
        $('#SplitAlert').append(splitDealMessages.splitDealValid);
        $('#splitWarning').show();
        $('#SplitAlert').show();
    }

    return false;
});