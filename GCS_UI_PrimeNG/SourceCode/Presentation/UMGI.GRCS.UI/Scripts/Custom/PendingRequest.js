var pendingRequestsMessages = {
    ProjectID: 'Project ID',
    ResourceTitle: 'Resource Title',
    ArtistName: 'Artist Name',
    WorkgroupName: 'Workgroup Name',
    ISRC: 'ISRC',
    ClearanceOwner: 'Clearance Owner',
    WorkgroupID: 'WorkgroupID',
    dialogboxTitle: 'Deactivate Workgroup -',
    DeactiveMessage: 'Selected workgroup is deactivated'
};
var workgroupID = 0;
var workgroupName = '';
var IsParent = false;
var parentID = 0;
var requestIDs = '';
var FromParent = false;
var pagingCount = "";
var workgroupModifiedDateTime = '';
jGridsList["ChildPendingRequest"] = [];
jGridsList["ParentPendingRequest"] = [];
$('.ui-dialog-titlebar-close').attr("title", "Close");
//Create dialog
function showPendingRequestPopup(workgroupname) {
    var objDialog = $('#DisplayPendingRequest').dialog({
        autoOpen: true,
        modal: true,
        title: pendingRequestsMessages.dialogboxTitle + workgroupname,
        show: 'clip',
        hide: 'clip',
        width: 1000
    });
    $('#pendingRequestErrorMessage').hide().html('');
    $('#deactivateWgPendingRequestInfoMessage').hide().html('');
}
var childRequestCount = 0;
var parentRequestCount = 0;

function showPendingRequest(selectedWorkgroupID, workgroupModifiedDateTime, selectedParentID, workgroupname, isParent) {
    jGridsList["ChildPendingRequest"] = [];
    jGridsList["ParentPendingRequest"] = [];
    document.getElementById('spnParentPendingRequest').innerHTML = workgroupname;
    pagingCount = $('#ddlpendingrequestparentPaging').val();
    requestIDs = '';
    workgroupID = selectedWorkgroupID;
    workgroupName = workgroupname;
    IsParent = isParent;
    parentID = selectedParentID;
    workgroupModifiedDateTime = workgroupModifiedDateTime;

    if (IsParent == true) {
        var hideParentPendingRequest = document.getElementById('ParentPendingRequest');
        hideParentPendingRequest.style.display = 'block';
        showParentPendingRequest(selectedWorkgroupID, workgroupModifiedDateTime);
    }
    else {
        //show only child pending request
        showChildPendingRequest(selectedWorkgroupID, isParent, parentRequestCount, workgroupModifiedDateTime);
        var hideParentPendingRequest = document.getElementById('ParentPendingRequest');
        hideParentPendingRequest.style.display = 'none';
        var hideParentPendingRequest = document.getElementById('ChildPendingRequest');
        hideParentPendingRequest.style.display = 'block';
    }
    //    showPendingRequestPopup(workgroupname);
}

function showChildPendingRequest(selectedWorkgroupID, fromParent, parentRequestCount, workgroupModifiedDateTime) {
    $(".buttonPanel").show();
    $('#ChildPendingRequest').jtable({
        paging: true,
        pageSize: pagingCount,
        columnResizable: false,
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/Workgroup/DeactivateWorkGroup'
        },
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () { $('.jtable .jtable-no-data-row').show(); },
        fields: {
            ID: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            ProjectReferenceNumber: {
                title: pendingRequestsMessages.ProjectID,
                width: '12%'
            },
            WorkgroupName: {
                title: pendingRequestsMessages.WorkgroupName,
                width: '24%'
            },
            ResourceTitle: {
                title: pendingRequestsMessages.ResourceTitle,
                width: '18%'
            },
            ArtistName: {
                title: pendingRequestsMessages.ArtistName,
                width: '15%'
            },
            ISRC: {
                title: pendingRequestsMessages.ISRC,
                width: '15%'
            },

            ClearanceOwner: {
                title: pendingRequestsMessages.ClearanceOwner,
                width: '20%'
            },
            WorkgroupID: {
                title: pendingRequestsMessages.WorkgroupID,
                width: '15%',
                list: false
            }
        },
        loadingRecords: function () {
            isGridLoad = true;
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            if (!isGridLoad)
                updateSelectedRecordsInGrid($("#ChildPendingRequest"), jGridsList["ChildPendingRequest"], "ID");
            //Get all selected rows
            var $selectedRows = $('#ChildPendingRequest').jtable('selectedRows');
            $('#SelectedChildPendingRequest').empty();
            //            if ($selectedRows.length > 0) {
            //                //Show selected rows
            //                var selectedRequestIDs = '';
            //                $selectedRows.each(function () {
            //                    var record = $(this).data('record');
            //                    selectedRequestIDs = selectedRequestIDs + record.ID + '|';
            //                    var recdisplay = document.getElementById('SelectedChildPendingRequest');
            //                    recdisplay.style.display = 'none';
            //                });
            //                $('#SelectedChildPendingRequest').append(selectedRequestIDs.substring(0, selectedRequestIDs.length - 1));
            //            }
            if (jGridsList["ChildPendingRequest"].length > 0) {
                var selectedRequestIDs = '';
                for (var i = 0; i < jGridsList["ChildPendingRequest"].length; i++) {
                    selectedRequestIDs = selectedRequestIDs + jGridsList["ChildPendingRequest"][i].ID + "|";
                }
                var recdisplay = document.getElementById('SelectedChildPendingRequest');
                recdisplay.style.display = 'none';
                $('#SelectedChildPendingRequest').append(selectedRequestIDs.substring(0, selectedRequestIDs.length - 1));
            }
        },
        recordsLoaded: function (event, data) {
            displaySelectedRecordsInGrid($("#ChildPendingRequest"), jGridsList["ChildPendingRequest"], "ID");
            isGridLoad = false;
            //hiding checkbox in header
            $('#ChildPendingRequest .jtable thead tr th:first').remove();
            $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#ChildPendingRequest .jtable thead tr:first');

            childRequestCount = data.records.length;
            if (IsParent == true) { hideParentChildPendingRequestTable(parentRequestCount, childRequestCount, selectedWorkgroupID); }
            else { hideChildPendingRequestTable(childRequestCount, selectedWorkgroupID); }
            FromParent = false;
        }
    });

    $('#ChildPendingRequest').jtable('load', {
        workGroupId: selectedWorkgroupID,
        modifiedDateTime: workgroupModifiedDateTime,
        fromParent: FromParent,
        isParent: IsParent
    });
}
//
function showParentPendingRequest(selectedWorkgroupID, workgroupModifiedDateTime) {
    $('#ParentPendingRequest').jtable({
        paging: true,
        pageSize: pagingCount,
        selecting: true,
        columnResizable: false,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/Workgroup/DeactivateWorkGroup'
        },
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () { $('.jtable .jtable-no-data-row').show(); },
        fields: {
            ID: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            ProjectReferenceNumber: {
                title: pendingRequestsMessages.ProjectID,
                width: '25%'
            },
            WorkgroupName: {
                title: pendingRequestsMessages.WorkgroupName,
                width: '25%',
                list: false
            },
            ResourceTitle: {
                title: pendingRequestsMessages.ResourceTitle,
                width: '25%'
            },
            ArtistName: {
                title: pendingRequestsMessages.ArtistName,
                width: '15%'
            },
            ISRC: {
                title: pendingRequestsMessages.ISRC,
                width: '15%'
            },

            ClearanceOwner: {
                title: pendingRequestsMessages.ClearanceOwner,
                width: '15%'
            },
            WorkgroupID: {
                title: pendingRequestsMessages.WorkgroupID,
                width: '15%',
                list: false
            }
        },
        loadingRecords: function () {
            isGridLoad = true;
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            if (!isGridLoad)
                updateSelectedRecordsInGrid($("#ParentPendingRequest"), jGridsList["ParentPendingRequest"], "ID");
            //Get all selected rows
            var $selectedRows = $('#ParentPendingRequest').jtable('selectedRows');
            $('#SelectedParentPendingRequest').empty();
            //            if ($selectedRows.length > 0) {
            //                //Show selected rows
            //                var selectedRequestIDs = '';
            //                $selectedRows.each(function () {
            //                    var record = $(this).data('record');
            //                    selectedRequestIDs = selectedRequestIDs + record.ID + '|';
            //                    var recdisplay = document.getElementById('SelectedParentPendingRequest');
            //                    recdisplay.style.display = 'none';
            //                });
            //                $('#SelectedParentPendingRequest').append(selectedRequestIDs.substring(0, selectedRequestIDs.length - 1));
            //            }
            if (jGridsList["ParentPendingRequest"].length > 0) {
                var selectedRequestIDs = '';
                for (var i = 0; i < jGridsList["ParentPendingRequest"].length; i++) {
                    selectedRequestIDs = selectedRequestIDs + jGridsList["ParentPendingRequest"][i].ID + "|";
                }
                var recdisplay = document.getElementById('SelectedParentPendingRequest');
                recdisplay.style.display = 'none';
                $('#SelectedParentPendingRequest').append(selectedRequestIDs.substring(0, selectedRequestIDs.length - 1));
            }
        },
        recordsLoaded: function (event, data) {
            displaySelectedRecordsInGrid($("#ParentPendingRequest"), jGridsList["ParentPendingRequest"], "ID");
            isGridLoad = false;
            //hiding checkbox in header
            $('#ParentPendingRequest .jtable thead tr th:first').remove();
            $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#ParentPendingRequest .jtable thead tr:first');
            parentRequestCount = data.records.length;
            FromParent = true;
            showChildPendingRequest(selectedWorkgroupID, FromParent, parentRequestCount, workgroupModifiedDateTime);
        }
    });
    $('#ParentPendingRequest').jtable('load', {
        workGroupId: selectedWorkgroupID,
        modifiedDateTime: workgroupModifiedDateTime,
        fromParent: FromParent,
        isParent: IsParent
    });
}

function hideChildPendingRequestTable(requestCount, selectedWorkgroupID) {
    if (requestCount == 0) {
        if (frompage == "viewworkgroup") {
            $('#ViewWorkgroup').dialog('close');
            frompage = "";
        }

        $('#searchWorkGroupList').jtable('deleteRecord', {
            key: selectedWorkgroupID,
            clientOnly: true,
            animationsEnabled: false
        });
        $('#deactivateWgSuccessMessage').show();
        $('#deactivateWgSuccessMessage').html(pendingRequestsMessages.DeactiveMessage);
        $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
    }
    else {
        showPendingRequestPopup(workgroupName);
    }
}

function hideParentChildPendingRequestTable(parentCount, childCount, selectedWorkgroupID) {
    if (parentCount == 0 && childCount == 0) {
        if (frompage == "viewworkgroup") {
            $('#ViewWorkgroup').dialog('close');
            frompage = "";
        }
        $('#searchWorkGroupList').jtable('deleteRecord', {
            key: selectedWorkgroupID,
            clientOnly: true,
            animationsEnabled: false
        });

        $('#deactivateWgSuccessMessage').show();
        $('#deactivateWgSuccessMessage').html(pendingRequestsMessages.DeactiveMessage);
        //                                                            $(this).dialog("close");
        //                                                        }
        //                                                        }
        //                                                    });
        $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
    }
    else {
        showPendingRequestPopup(workgroupName);
    }
}

function SearchWorkgroupForRedirection(workgroupID, isParent, requestIDs, parentID, roleId) {
    var formValues = '';
    $('#SearchWorkgroupRedirect')
        .dialog({
            autoOpen: true,
            modal: true,
            title: 'Search and Redirect',
            show: 'clip',
            hide: 'clip',
            width: "98%",
            position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50]
        })
        .load('/GCS/Workgroup/SearchWorkgroupRedirect');
}

$(function () {
    $('#btnSelectPendingRequest').click(function (e) {
        requestIDs = '';
        if ($.trim($('#SelectedParentPendingRequest')[0].innerHTML).length > 0) {
            requestIDs = requestIDs + $('#SelectedParentPendingRequest')[0].innerHTML;
        }
        if ($.trim($('#SelectedChildPendingRequest')[0].innerHTML).length > 0) {
            if (requestIDs.length > 0) {
                requestIDs = requestIDs + '|' + $('#SelectedChildPendingRequest')[0].innerHTML;
            }
            else {
                requestIDs = $('#SelectedChildPendingRequest')[0].innerHTML;
            }
        }
        if (requestIDs.length > 0) {
            $('#pendingRequestErrorMessage').hide();
            $('#pendingRequestErrorMessage').html('');
            SearchWorkgroupForRedirection(workgroupID, IsParent, requestIDs, parentID);
        }
        else {
            $('#pendingRequestErrorMessage').show();
            $('#pendingRequestErrorMessage').html(searchWorkgroupMessages.noRowsSelected);
        }
    });
    $('#btnCancelPendingRequest').click(function (e) {
        $('#DisplayPendingRequest').dialog('close');
    });
    $('#ddlpendingrequestparentPaging').change(function () {
        var rowCount = $(this).val();
        showPendingRequest(workgroupID, workgroupModifiedDateTime, parentID, workgroupName, IsParent);
    });
});