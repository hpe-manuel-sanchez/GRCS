/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var searchParentMessages = {
    artistName: "Artist",
    cntrctParty: 'Contracting Party',
    workflowStatus: 'Work Flow Status',
    cntrctStatus: 'Contract Status',
    cntrctDesc: 'Contract Description',
    clearanceAdm: 'Clearance Admin Company',
    umgSigning: 'Signing Company',
    rightsType: 'Rights Type',
    cntrctCmncment: 'Commencement Date',
    localCtrctNo: 'Local Contract No',
    cntrctId: 'Contract Id',
    confirm: 'Confirm',
    rightsProp: "Do you want to propagate the rights data from parent contract to this contract?",
    validMsg: 'Please specify at least one search criteria'
};

//Create dialog
var objDialog = $('<div ></div>')
    .html('<p>' + messageCommon.onLoading + '</p>')
    .dialog({
        autoOpen: false,
        modal: true,
        title: messageCommon.warningTitle,
        show: 'clip',
        hide: 'clip',
        width: 300
    });
var parentPageSize = 25;
var rowIndex = -1;
function loadTable() {
    if ($('#searchResult').length == 0)
        return;

    $('#searchResult').jtable({
        paging: true,
        pageSize: parentPageSize,
        sorting: true,
        selecting: true,
        selectOnRowClick: true,
        selectingCheckboxes: false,
        defaultSorting: 'ContractId ASC',
        actions: {
            listAction: '/GCS/Contract/SearchParentContract'
        },
        loadingRecords: function () {
            $(".ui-jtable-loading").show();
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("totalContracts").innerHTML = "Search Result (" + rowIndex + ")";
            if (rowIndex < 10) {
                $(".jtable-main-container").css('height', rowIndex * 40 + 30);
                // $("div .jtable-main-container").attr('overflow-y', hidden);
            }
            else {
                $(".jtable-main-container").css('height', 375);
            }
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
        },
        fields: {
            ArtistName: {
                title: searchParentMessages.artistName
            },
            ContractingParty: {
                title: searchParentMessages.cntrctParty
            },
            WorkflowStatus: {
                title: searchParentMessages.workflowStatus
            },
            ContractStatus: {
                title: searchParentMessages.cntrctStatus
            },
            ContractDescription: {
                title: searchParentMessages.cntrctDesc
            },
            ClearanceCompanyCountry: {
                title: searchParentMessages.clearanceAdm
            },
            UmgSigningCompany: {
                title: searchParentMessages.umgSigning
            },
            RightsTypeName: {
                title: searchParentMessages.rightsType
            },
            ContractCommencementDate: {
                title: searchParentMessages.cntrctCmncment,
                type: 'date',
                displayFormat: 'dd M yy'
            },
            LocalContractRefNumber: {
                title: searchParentMessages.localCtrctNo
            },
            ContractId: {
                title: searchParentMessages.cntrctId
            }
        },
        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('#searchResult').jtable('selectedRows');
            $('#searchParentcontractList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#searchParentcontractList').append(record.ContractId);
                    var recdisplay = document.getElementById('searchParentcontractList');
                    recdisplay.style.display = 'none';

                    var objWarningDialog = $('<div id="delete"></div>')
                    //                    objWarningDialog.dialog({ title: searchParentMessages.confirm });
                    //                    objWarningDialog.html(searchParentMessages.rightsProp);
                    .html('<p>' + searchParentMessages.rightsProp + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: searchParentMessages.confirm,
                    show: 'clip',
                    hide: 'clip',
                    width: 300
                });
                    objWarningDialog.dialog('open');
                    objWarningDialog.dialog({
                        buttons:
                {
                    'Yes': function () {
                        $(this).dialog('close');
                        window.location.href = "/GCS/Contract/LinkParentContract?" + "contractid=" + record.ContractId + "&flag=" + "Rights";
                        $('#parentContractPopup').dialog('close');
                    }, 'No': function () {
                        $(this).dialog('close');
                        var formValues = GetFormValues();
                        formValues = formValues + '&LinkedContractId' + '=' + record.ContractId;
                        $('#parentContractPopup').dialog('close');
                        $.post('/GCS/Contract/AddParentContract', formValues, function (data) {
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
                    }, 'Cancel': function () {
                        $(this).dialog('close');
                    }
                }
                    });
                });
            }
            else {
                //No rows selected
                $('#searchParentcontractList').append(messageCommon.viewValid);
            }
        }
    });

    $('#searchResult').hide();
}

$('#ContractPaging select').change(function () {
    $('#splitWarning').hide();
    parentPageSize = $('#ContractPaging select').val();
    $('#searchResult').jtable({ 'pageSize': parentPageSize });
    $('#searchResult').jtable('load', {
        artistName: $('#inpArtistName').val()
        , localContractNumber: $('#inpLocalContractRefNumber').val()
        , contractingParty: $('#inpContractingParty').val()
        , Id: $('#inpContractId').val()
    });
    $('#searchResult').show();
});

//ResetButton

$("#inpContractId").keyup(function () {
    if ($('#inpContractId').val() != "") {
        var value = $('#inpContractId').val().replace(/^\s\s*/, '').replace(/\s\s*$/, '');
        var intRegex = /^\d+$/;
        if (!intRegex.test(value)) {
            value = document.getElementById('inpContractId').value.replace(/[^0-9\.]+/g, '');
        }
        $('#inpContractId').val(value);
    }
});

function loadSearchParent() {
    if ($('#inpContractId').val() == 0) {
        $('#inpContractId').val('');
    }

    loadTable();

    $('#searchParentContract').click(function (e) {
        e.preventDefault();
        if (($('#inpContractingParty').val() == '') && $('#inpArtistName').val() == '' && $('#inpLocalContractRefNumber').val() == '' && $('#inpContractId').val() == '') {
            // $('#hiddenValidation').show();
            $('#SplitAlert').empty();
            $('#SplitAlert').append(searchParentMessages.validMsg);
            $('#splitWarning').show();
            $('#SplitAlert').show();
        }
        else {
            // $('#hiddenValidation').hide();
            if ($('#inpContractId').val().match('^(0|[1-9][0-9]*)$'))
                $('inpContractId').val('');
            pageSize = $('#ContractPaging select').val();
            $('#searchResult').jtable({ 'pageSize': parentPageSize });
            $('#searchResult').jtable('load', {
                artistName: $('#inpArtistName').val()
            , localContractNumber: $('#inpLocalContractRefNumber').val()
            , contractingParty: $('#inpContractingParty').val()
            , Id: $('#inpContractId').val()
            , contractId: $('#Contract_ContractId').val()
            });
            //            var grid = document.getElementById('searchResult');
            //            grid.style.display = 'block';
            //            $('#searchResult').show();
            //            var element = document.getElementById('ContractPaging');
            //            element.style.display = 'block';

            // dynamic height set for height and scroll for search result
            var h = $(window).height();
            // $("#searchResult").css('height', h - 270);
            //$(".jtable-main-container").css('height', h - (rowIndex * 15));
            // $(".jtable-bottom-panel").css("width", $(document).width());
        }
        var grid = document.getElementById('searchResult');
        grid.style.display = 'block';
        $('#searchResult').show();
        var element = document.getElementById('ContractPaging');
        element.style.display = 'block';

        $(window).resize(function () {
            reSize();
        });
    });

    $('#resetButton').click(function (e) {
        e.preventDefault();
        $('#splitWarning').hide();
        if (document.getElementById('inpLocalContractRefNumber') != null)
            document.getElementById('inpLocalContractRefNumber').value = '';
        if (document.getElementById('inpArtistName') != null)
            document.getElementById('inpArtistName').value = '';
        if (document.getElementById('inpContractId') != null)
            document.getElementById('inpContractId').value = '';
        if (document.getElementById('inpContractingParty') != null)
            document.getElementById('inpContractingParty').value = '';
        //        var grd = document.getElementById('searchResult');
        //        grd.style.display = 'none';
        var element = document.getElementById('ContractPaging');
        element.style.display = 'none';
    });
}

$(document).ready(function () {
    $('#inpContractingParty').focus();
    reSize();
    $(window).resize(function () {
        reSize();
    });
    function linkParentConfirmation() {
        e.preventDefault();
        var rights = confirm(searchParentMessages.rightsProp);
        if (rights) {
            window.location.href = "/GCS/Contract/LinkParentContract?" + "contractid=" + test.record.ContractId + "&flag=Rights";
            $('#parentContractPopup').dialog('close');
        }
        else {
            //
            var formValues = GetFormValues();
            formValues = formValues + '&LinkedContractId' + '=' + test.record.ContractId;
            $('#parentContractPopup').dialog('close');
            $.post('/GCS/Contract/AddParentContract', formValues, function (data) {
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
    }
});

function reSize() {
    var h = $(window).height();

    $("#searchResult").css('height', h - 50);
    $("#searchResult").removeAttr('overflow', scroll);
    $("#parentContractPopup").css('height', h - 60);
}
//on click hiding error messages
$("#inpContractingParty, #inpArtistName, #inpLocalContractRefNumber, #inpContractId ")
          .bind("keyup", HideWarningSuccess);