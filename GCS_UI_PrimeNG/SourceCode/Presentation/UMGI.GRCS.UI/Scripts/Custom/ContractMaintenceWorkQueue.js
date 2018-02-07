/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var messages = {
    repLink: 'Repertoire Link',
    created: 'Created By',
    changedDate: 'Last Change Date',
    editCriteria:'Click on one Row to Edit Contract '
};

$(document).ready(function () {

    var rowIndex = -1;
    var pageSize = 25;

    $('#jqgrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        actions: {
            listAction: 'ContractMaintanceWork'
        },


        //  recordsLoaded: function () { $('.jtable .jtable-no-data-row').show(); },
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();

        },
        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("workQueueResultCount").innerHTML = "WorkQueue (" + rowIndex + ")";

            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            setToolTip(this);
        },
        fields: {

            WorkflowStatus: {
                title: messageCommon.workflowStatus


            },
            ContractStatus: {
                title: messageCommon.cntrctStatus

            },
            ArtistName: {
                title: messageCommon.artistName

            },
            ContractingParty: {
                title: messageCommon.cntrctParty

            },
            ContractDescription: {
                title: messageCommon.cntrctDesc

            },
            ClearanceCompanyCountry: {
                title: messageCommon.adminCmpny

            },
            RightsTypeName: {
                title: messageCommon.rightsType

            },

            ContractCommencementDate: {
                title: messageCommon.cmncmentDate,
                type: 'date',
                displayFormat: 'dd M yy'

            },
            LocalContractRefNumber: {
                title: messageCommon.localCntrctNo

            },
            RepertoireLink: {
                title: messages.repLink,
                 display: function (test) {
                 var linkedImage;
                            if (test.record.HasRepertoire)
                                linkedImage = $('<img src="/Gcs/Images/linked_Contract.png" title"Linked Contract">');
                            else
                                linkedImage = "";
                            return linkedImage;
                            }

            },
            CreatedUser: {
                title: messages.created

            },
            ContractLastChangeDate: {
                title: messages.changedDate,
                type: 'date',
                displayFormat: 'dd M yy'

            },
            ContractId: {
                title: messageCommon.cntrctId,
                display: function (test) {
                    return test.record.ContractId;
                }

            }

        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
            HideWarningSuccess();
            var $selectedRows = $('#jqgrid').jtable('selectedRows');
            $('#contractSelectList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#contractSelectList').append(record.ContractId);
                    var recdisplay = document.getElementById('contractSelectList');
                    recdisplay.style.display = 'none';

                });
            }
            else {
                //No rows selected
                // $('#contractSelectList').append(messageCommon.viewValid);
                $('#contractSelectList').empty();

            }
        }

    });
    $('#jqgrid').jtable('load');

    $('#jqTablePager select').change(function () {
        HideWarningSuccess();
        pageSize = $('#jqTablePager select').val();
        $('#jqgrid').jtable({ 'pageSize': pageSize });
        $('#jqgrid').jtable('load');
    });


    $('#EditContract').click(function (e) {

        var contractId = $('#contractSelectList')[0].innerHTML;

        if (contractId != '')
            window.location.href = "/GCS/Contract/EditContract/" + contractId;
        else {

            ShowWarning(messages.editCriteria);

        }
    });

    reSizeContractWorkPage();

    $(window).resize(function () {
        reSizeContractWorkPage();
    });

});

function reSizeContractWorkPage() {
    var h = $(window).height();

    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".contractMaintenanceWorkQueue").css('height', h - 145);
    else
        $(".contractMaintenanceWorkQueue").css('height', h - 183);

  
}
