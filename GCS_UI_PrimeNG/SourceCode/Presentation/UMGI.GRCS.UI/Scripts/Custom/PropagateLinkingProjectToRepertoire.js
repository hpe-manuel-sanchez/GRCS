/// <reference path="../jquery-1.5.1-vsdoc.js" />
/// <reference path="../jquery-1.6.3.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var messages = {
    repLink: 'Repertoire Link',
    created: 'Created By',
    changedDate: 'Last Change Date',
    viewValid: 'No row selected! Select row to view Contract'
};
$(document).ready(function () {
    $('#releaseCheckbox').click(function () {
        if ($('#releaseCheckbox').is(':checked')) {
            releaseChecked();
        }
        else {
            releaseUnchecked();
        }
    });
    function releaseChecked() {
        var allRows = $('.jqgrid').find('tr').addClass('jtable-row-selected');
        allRows.find('td.jtable-selecting-column input').attr('checked', true);
        var header = $('.jqgrid').find('th');
        header.find('td.jtable-column-header jtable-column-header-selecting').attr('checked', true);
    }

    function releaseUnchecked() {
        var allRows = $('.jqgrid').find('tr').removeClass('jtable-row-selected');
        allRows.find('td.jtable-selecting-column input').attr('checked', false);
    }

    $('#selectAll').click(function () {
        if ($('#selectAll').is(':checked')) {
            $('#releaseCheckbox').attr('checked', true);
            releaseChecked();
        }
        else {
            releaseUnchecked();
        }
    });

    $('#accordion .head').click(function (e) {
        e.preventDefault();
        $(this).next().toggle();        
        $(this).find('a').toggleClass('iconBottom');
        return false;
    }).next().show();
    var pageSize = 6;

    $('.jqgrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/Contract/SearchContract'
        },

        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () { $('.jtable .jtable-no-data-row').show(); },
        fields: {

            WorkflowStatus: {
                title: "Description"


            },
            ContractStatus: {
                title: "Version Title"

            },
            ArtistName: {
                title: "Artist"

            },
            ContractingParty: {
                title: "ID"

            }
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('.jqgrid').jtable('selectedRows');
            $('#contractSelectList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#contractSelectList').append(record.ContractId);
                    //var recdisplay = document.getElementById('contractSelectList');
                    // recdisplay.style.display = 'none';

                });
            }
            else {
                //No rows selected
                $('#contractSelectList').append(messages.viewValid);
            }
        }

    });
    $('.jqgrid').jtable('load');

    $('#jqTablePager select').change(function () {
        pageSize = $('#jqTablePager select').val();
        $('.jqgrid').jtable({ 'pageSize': pageSize });
        $('.jqgrid').jtable('load');
    });


    reSizeContractWorkPage();

    $(window).resize(function () {
        reSizeContractWorkPage();
    });

});

function reSizeContractWorkPage() {
    var h = $(window).height();
    $(".jqgrid").css('height', h - 175);
    $(".jtable-main-container").css('height', h - 175);

}
