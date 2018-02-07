$(document).ready(function () {
    SearchMassReminderRequests();
});

$("#btnClose").click(function () {
    $('#massReminder').dialog('close');
    $('#massReminder').remove();
});

$("#btnSendReminder").click(function (e) {
    var activeInbox = _activeInbox;
    var clrProjectId = _clrProjectId_Requestor;

    var $selectedRows = $('#massRemindersList').jtable('selectedRows');
    if ($selectedRows.length == 0) {
        $("#divMassReminderErrorMessage").text("Please Select atleast one Request");
        $('#divMassReminderErrorMessage').addClass('warning');
        $("#divMassReminderErrorMessage").show();
        return false;
    }
    var selectedRequests = [];
    var routedItemRequest = [];
    var ModifiedDateRequest;
    var ModifiedDateRoutedItem;

    $selectedRows.each(function () {
        var record = $(this).data('record');

        var RequestId = record.RequestId;
        var RoutedItemId = record.RoutedItemId;
        routedItemRequest.push({ Key: RoutedItemId, Value: RequestId });
        ModifiedDateRequest = record.ModifiedDateRequestString;
        ModifiedDateRoutedItem = record.ModifiedDateRoutedString;

        // set ClearanceInboxRequestList
        selectedRequests.push({
            KeyRoutedItemRequest: null,
            RoutedItemId: RoutedItemId,
            RequestId: RequestId,
            ModifiedDateRoutedString: ModifiedDateRoutedItem,
            ModifiedDateRequestString: ModifiedDateRequest
        });
    });

    postData = {
        clearanceInboxModel: getFilters().clearanceInboxModel, //getFilters().clearanceInboxModel,
        clearanceInboxRequestAction: {
            WorkgroupId: $('#hdnWorkgroupId').val(),
            FolderId: $('#hdnFolderId').val(),
            ProjectId: $('#hdnProjectId').val(),
            Requests: selectedRequests,
            ActionId: 15,
            Comment: null
        },
        roleGroup: RoleGroup.Requestor,
        selectAllAcrossPages: false,
        gridType: 0
    };

    $.ajax({
        url: '/GCS/ClearanceInbox/PerformRequestAction',
        type: 'POST',
        data: JSON.stringify(postData),

        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#massReminder').dialog('close');
            $('#massReminder').remove();
            //activeInbox.find('#divLeftPanel').html(data);
            refreshLeftPanel(activeInbox, true, data);
            // have to write  code selected project on lefthand side with global variables

            var postDataR = {
                args: null,
                roleGroup: RoleGroup.Requestor,
                clearanceInboxRequestAction: {
                    WorkgroupId: _workGroupId_Requestor,
                    FolderId: _folderId_Requestor,
                    ProjectId: _clrProjectId_Requestor,
                    RoleName: _roleName
                },
                gridType: 0
            };
            displayLoadingPanel(activeInbox, false);
            RefreshRightPanel(postDataR);
        },
        error: function () {
            //  displayLoadingPanel(false);

            //displayMessage(true, MessageType.Error, msg_ErrorSavingData);
        }
    });
});

function SearchMassReminderRequests() {
    var projectId = $('#hdnProjectId').val();
    var workGroupId = $('#hdnWorkgroupId').val();
    var folderId = $('#hdnFolderId').val();

    $('#massRemindersList').jtable({
        paging: true,
        pageSize: 1000,
        sorting: false,
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/ClearanceInbox/MassRemindersWithParam'
        },
        fields: {
            ResourceTitle: {
                title: resourceName
            },
            RequestId: {
                list: false
            },
            RoutedItemId: {
                list: false
            },
            RequestType: {
                title: requestType
            },
            Isrc: {
                title: isrc
            },
            ModifiedDateRequestString: {
                title: 'Requested Date',
                width: '8%',
                list: false
            },
            ModifiedDateRoutedString: {
                title: 'Routed Item Date',
                width: '8%',
                list: false
            }
        },
        selectionChanged: function () {
            var $selectedRows = $('#massRemindersList').jtable('selectedRows');
            //$('#SelectedRowList').empty();
            if ($selectedRows.length > 0) {
                // Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');

                    $('#searchReminderList').append(record.RoutedItemId + ',' + record.RequestId + '~');
                    $('#searchReminderList').hide();
                });
            }
        }
    });

    $('#massRemindersList').jtable('load', {
        WorkgroupId: workGroupId,
        FolderId: folderId,
        ProjectId: projectId,
        Requests: null,
        ActionId: 15,
        Comment: null
    });
}