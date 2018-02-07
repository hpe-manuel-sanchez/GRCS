/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var messages = {
    repLink: 'Repertoire Link',
    created: 'Created By',
    changedDate: 'Last Change Date',
    viewValid: 'No row selected! Select row to view Contract'
};
var contractIds;
var clearanceAdminComp;
var griddetails;
var selectedResources = [];
var count = 0;
var empty = 0;
var rows = [];
$(document).ready(function () {
    reSizeContractWorkPage();
    clearanceAdminComp = $('#hiddenClearanceUnlink').val();
    contractIds = $('#hiddenContractUnlink').val();
    $('#cancelRelease').click(function () {
        $('#relePopup').dialog('close');
    });
    $('#selectRelease').click(function () {
        if ($('#selectRelease').is(':checked')) {
            resourceChecked();
        } else {
            resourceUnchecked();
        }
    });

    $('#selectAllRelease').click(function () {
        if ($('#selectAllRelease').is(':checked')) {
            $('#selectRelease').attr('checked', true);
            resourceChecked();
        } else {
            $('#selectRelease').attr('checked', false);
            resourceUnchecked();
        }
    });

    $('#accordion .head .newHead').click(function (e) {
        e.preventDefault();
        $(this).parent().next().toggle();
        $(this).parent().find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    var pageSize = 6;
    $('.jtableGrid').each(function () {
        count++;
        griddetails = $(this);
        $(this).jtable({
            paging: true,
            pageSize: pageSize,
            sorting: true,
            addNewRecord: '+ Add new record',
            defaultSorting: 'Name ASC',
            selecting: true,
            selectingCheckboxes: true,
            multiselect: true,
            actions: {
                listAction: '/GCS/WorkQueue/GetAssociatedResources'
            },

            loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); $(".ui-jtable-loading").show(); },
            recordsLoaded: function (event, data) {
                $('.jtable .jtable-no-data-row').show();
                var totalRows = data.serverResponse.TotalRecordCount;
                rows.push(totalRows);

                $(".ui-jtable-loading").hide();
            },
            fields: {
                ResourceTitle: {
                    title: "Title"
                },
                VersionTitle: {
                    title: "Version Title"
                },
                ArtistName: {
                    title: "Artist"
                },
                Isrc: {
                    title: "ISRC"
                }
            }
        });

        $(this).jtable('load',
            {
                contractId: $(this).attr('name'),
                releaseId: $(this).attr('title')
            });

        $(this).find('.testing').html('Search Result (' + rows[count] + ')');
    });

    //BtnClick function
    $('.btnLinkResofRel').click(function () {
        GetResourceForReleases();
        if (count == empty) {
            alert("Please Select a Resource");
            return false;
        }
        else {
            $('#ReleaseUnlinkPro').dialog('close');
            UnLinkReleasesAssociatedResources();
        }
    });

    $('.cancelRelease').click(function () {
        $('#ReleaseUnlinkPro').dialog('close');
    });
});
function reSizeContractWorkPage() {
    var h = $(window).height();
    $(".scrollPropRelToRes").css('height', h - 120);
    // $("#resourceGrid").css('height', h - 175);
    //$(".jtable-main-container").css('height', h - 175);
}
function resourceChecked() {
    $('.jtableGrid').each(function () {
        var allRows = $(this).find('tr').addClass('jtable-row-selected');
        allRows.find('td.jtable-selecting-column input').attr('checked', true);
        var header = griddetails.find('th');
        header.find('td.jtable-column-header jtable-column-header-selecting').attr('checked', true);
    });
}

function resourceUnchecked() {
    $('.jtableGrid').each(function () {
        var allRows = $(this).find('tr').removeClass('jtable-row-selected');
        allRows.find('td.jtable-selecting-column input').attr('checked', false);
    });
}

function GetResourceForReleases() {
    $('.jtableGrid').each(function () {
        var $selectedRows = $(this).jtable('selectedRows');
        if ($selectedRows.length > 0) {
            $selectedRows.each(function () {
                selectedResources.push($(this).data('record'));
            });
        }
        else {
            empty++;
        }
    });
    return false;
}

function UnLinkReleasesAssociatedResources() {
    //alert(selectedResources.length);

    $.ajax({
        url: "/GCS/WorkQueue/UnlinkAssociatedResources/" + contractIds,
        type: 'POST',
        data: JSON.stringify(selectedResources),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert('Unlinked sucessfully');
        },
        error: function (data) {
            //alert(data.responseText);
        }
    });
}