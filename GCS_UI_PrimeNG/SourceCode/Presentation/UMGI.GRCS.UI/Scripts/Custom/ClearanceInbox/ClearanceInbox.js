var inboxColumnSetting = (function () {

    var init = function (settings) {

        inboxColumnSetting.settings = {
            columnSettingToSave: []
        }
        $.extend(inboxColumnSetting.settings, settings);
    }

    var functions = {
        resized: function (sender, args) {
            var _column = sender._gridHeaderTable.cells[args.cellIndex].id;
            var _width = args.width;
            var _ID = sender._ID;

            functions.getWidthColumns(_column, _width, _ID);

            resizeDropDownLists(_column, _width, _ID)
        },
        getWidthColumns: function (column, width, ID) {

            var isNew = true;

            $.each(inboxColumnSetting.settings.columnSettingToSave, function (index, obj) {
                if (obj["GridId"] === ID && obj["Column"] === column) {
                    obj["Width"] = width;
                    isNew = false;
                    return false;
                }
            });

            if (isNew) {
                _object = {
                    "GridId": ID,
                    "Column": column,
                    "Width": width
                };
                inboxColumnSetting.settings.columnSettingToSave.push(_object);
            }

        }
    }

    var data = {
        columnSettingToSave: function () {
            return settings.columnSettingToSave;
        }
    }

    return {
        _init: init,
        _resized: functions.resized
    }
})();

//#region - Global Variables

var _activeInbox;
var _activeRoleGroup;

var _clrProjectId_Requestor;
var _workGroupId_Requestor;
var _folderId_Requestor;
var _clrProjectId_Reviewer;
var _workGroupId_Reviewer;
var _folderId_Reviewer;
var _roleName;
var _clrProjectId_RCCAdmin;
var _workGroupId_RCCAdmin;
var _folderId_RCCAdmin;
var _projectType;
var _projectStatus;

var lbl_Request_RouteToRccAdmin_Across_Pages = 'Route to RCC Admin';
var lbl_Request_ConditionallyApprove = 'Conditionally Approve Request';
var lbl_Request_RejectRequest = 'Reject Request';
var lbl_Request_Dispatch = 'Dispatch';
var lbl_Request_RouteToRccAdmin = 'Route to RCC Admin';
var _lockedProjectStatus;

var MessageType = {
    Info: 'info',
    Alert: 'alert',
    Warning: 'warning',
    Success: 'success',
    Error: 'error'
};

var ContentType = {
    Text: 'Text',
    Html: 'Html'
};

var EmailTypeEnum = {
    MasterArtistManagement: 41,
    MasterArtistManagementWithSelectedResources: 42,
    RegularNonTraditionalArtistManagement: 99
};

var browserName;

var nAgt = navigator.userAgent;

if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
    browserName = "Microsoft Internet Explorer";
}

else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
    browserName = "Firefox";
}

else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
    browserName = "Chrome";
}

else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
    browserName = "Safari";
}

var SystemFolder = {
    ReviewerHighPriority: 1,
    ReviewerPending: 2,
    ReviewerArtistConsent: 3,
    RCCAdminHighPriority: 4,
    RCCAdminOrphan: 5,
    RCCAdminOneStop: 6,
    RequestorHighPriority: 7,
    RequestorSubmitted: 8,
    RequestorUnsubmitted: 9,
    RequestorReopened: 10,
    Research: ('Research').toUpperCase(),
    InternalReview: ('Internal Review').toUpperCase(),
    SideArtistSample: ('Side Artist / Sample').toUpperCase()
};

$('body').keydown(function (e) {

    if (e.keyCode == 13) { getSearchResult(); }

});

//#endregion - Global Variables

//#region - Page

function displayMessage(activeInbox, truefalse, messageType, messageText, contentType) {

    var messageElement = activeInbox.find('#divMessage');

    messageElement.text('')
                  .html('')
                  .removeClass()
                  .hide();

    contentType = contentType || ContentType.Text;

    if (truefalse) {

        if (contentType == ContentType.Text) {

            messageElement.text(messageText);

        }
        else {

            messageElement.html(messageText);

        }

        messageElement.addClass(messageType);

        messageElement.show();
    }

    applyCustomTheam(_activeRoleGroup);

}

function displayLoadingPanel(activeInbox, truefalse) {

    var loadingPanel = $($.find('#loadingDv'));

    if (truefalse) {
        displayMessage(activeInbox, false);

        loadingPanel.show();

    }
    else {

        loadingPanel.hide();

    }

}

//#endregion - Page

//#region - Header

function showFilters() {
    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;

    var popup = $($.find('#popup-filters-' + activeRoleGroup));
    var popupInitialState = $(popup.clone());
    var popupWidth = 400;
    var optionsSelectedToShow = 2;

    popupInitialState.find('#ddlRccHandler').replaceWith($(popup[0].outerHTML).find('#ddlRccHandler'));
    popupInitialState.find('#ddlRequestor').replaceWith($(popup[0].outerHTML).find('#ddlRequestor'));
    popupInitialState.find('#ddlWorkgroup').replaceWith($(popup[0].outerHTML).find('#ddlWorkgroup'));

    displayMessage(activeInbox, false);

    var ddlRccHandler = popup.find('#ddlRccHandler');
    var ddlRequestor = popup.find('#ddlRequestor');
    var ddlWorkgroup = popup.find('#ddlWorkgroup');

    var btnSave = popup.find('#btn-popup-Filters-Save');
    var btnApplyFilters = popup.find('#btn-popup-Filters-ApplyFilters');
    var btnCancel = popup.find('#btn-popup-Filters-Cancel');
    var btnReset = popup.find('#btn-popup-Filters-Clear');

    var allpopupControls = $([])
                            .add(ddlRccHandler)
                            .add(ddlRequestor)
                            .add(ddlWorkgroup)
                            .add(btnSave)
                            .add(btnApplyFilters)
                            .add(btnReset)
                            .add(btnCancel);

    if (activeRoleGroup == "Reviewer") {
        popupWidth = 550;
        optionsSelectedToShow = 1;
    }

    $([]).add(ddlRccHandler)
         .add(ddlRequestor)
         .add(ddlWorkgroup)
         .multiselect({
             selectedList: optionsSelectedToShow,
             header: false,
             height: '92',
             minWidth: '300',
             noneSelectedText: '',
             selectedText: '# selected'
         });

    //change the width of the workgroup dropdown.
    $("#ddlWorkgroup + button").addClass("reviewerFilter");

    btnSave.click(function () { saveFilters(); });
    btnApplyFilters.click(function () { applyFilters(); });
    btnReset.click(function () { resetPopup(popup); });
    btnCancel.click(function () { closePopup(); restorePopupState(); });

    popup.dialog({
        autoOpen: false,
        width: popupWidth,
        modal: true,
        resizable: false,
        title: lbl_Filters,
        close: function () { btnCancel.click(); }
    }).css('z-index', 99999);

    popup.dialog('open');

    function saveFilters() {

        var clearanceInboxModel = getFilters().clearanceInboxModel;

        var postData = {
            roleGroup: activeRoleGroup,
            clearanceInboxModel: clearanceInboxModel,
            columnSetting: inboxColumnSetting.settings.columnSettingToSave
        };

        displayLoadingPanel(activeInbox, true);

        $.ajax({
            url: url_SaveInboxFilters,
            type: 'POST',
            data: JSON.stringify(postData),
            async: 'async',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {

                if (data.Error != true) {
                    closePopup();
                    refreshLeftPanel(activeInbox, true, data);
                    displayFilterState();
                }
                else {
                    displayMessage(activeInbox, true, MessageType.Error, data.Message);
                }
            },
            error: function () {
                displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);
            },
            complete: function () {
                displayLoadingPanel(activeInbox, false);
            }
        });
    }

    function applyFilters() {
        closePopup();
        getSearchResult();
        displayFilterState();
    }

    function restorePopupState() {
        popup.remove();
        popupInitialState.appendTo($('body'));
    }

    function resetPopup(context) {
        var checkboxElementChecked = context.find("input:checked");
        if ((checkboxElementChecked != undefined || checkboxElementChecked != null) && checkboxElementChecked.length > 0)
            checkboxElementChecked.prop('checked', false);

        /* multiselect widget control */
        var selectElements = context.find("select[multiple='multiple']");
        if (selectElements != undefined || selectElements != null)
            selectElements.multiselect("uncheckAll");
    }

    function closePopup() {
        allpopupControls.unbind();

        var targetList = null;

        if (ddlRccHandler.length > 0) targetList = ddlRccHandler;
        if (ddlRequestor.length > 0) targetList = ddlRequestor;
        if (ddlWorkgroup.length > 0) targetList = ddlWorkgroup;

        if (targetList != null) {

            $(targetList.find('option')).removeAttr('selected');

            var sourceList = $('.ui-multiselect-menu.ui-widget.ui-widget-content.ui-corner-all').find(':input:checked');

            $.each(sourceList, function (index, element) {

                $(targetList.find('option[value="' + $(element).val() + '"]')).attr('selected', 'selected');

            });

        }

        $([]).add(ddlRccHandler)
             .add(ddlRequestor)
             .add(ddlWorkgroup)
             .multiselect('destroy');

        popup.dialog('destroy');

    }
}

function getSearchResult(clearanceInboxModel) {

    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;

    var postData = {
        roleGroup: activeRoleGroup,
        clearanceInboxModel: clearanceInboxModel || getFilters().clearanceInboxModel,
        columnSetting: inboxColumnSetting.settings.columnSettingToSave
    };

    displayLoadingPanel(activeInbox, true);

    $.ajax({
        url: url_GetInboxSearchResult,
        type: 'POST',
        data: JSON.stringify(postData),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.Error != true) {
                refreshLeftPanel(activeInbox, true, data);
            }
            else {
                displayMessage(activeInbox, true, MessageType.Error, data.Message);
            }
        },
        error: function () {
            displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);
        },
        complete: function () {
            displayLoadingPanel(activeInbox, false);
        }
    });
}

function getFilters() {
    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;
    var activeInboxFilters = $($.find('#popup-filters-' + activeRoleGroup));
    var requestType = [];
    var rccAdminRequestType = [];
    var rccHandler = [];
    var requestor = [];
    var scopeType = [];
    var searchType = [];
    var searchText = null;
    var workgroup = [];

    var selectedFilters = $.merge(activeInboxFilters.find(':input').serializeArray(),
                                  activeInbox.find('#inboxSearch :input').serializeArray());

    $.each(selectedFilters, function (index, element) {
        switch (element.name) {

            case 'requestType':
                requestType.push({ Value: element.value });
                break;

            case 'rccAdminRequestType':
                rccAdminRequestType.push({ Value: element.value });
                break;

            case 'ddlRccHandler':
                !element.value ? null : rccHandler.push({ Value: element.value });
                break;

            case 'ddlRequestor':
                !element.value ? null : requestor.push({ Value: element.value });
                break;

            case 'ddlWorkgroup':
                !element.value ? null : workgroup.push({ Id: element.value });
                break;

            case 'scopeType':
                scopeType.push({ Value: element.value });
                break;

            case 'ddlSearchType':
                !element.value ? null : searchType.push({ Value: element.value });
                break;

            case 'ddlAssignedToUser':
                if (activeInbox.find('#ddlAssignedToUser').hasClass('displayBlock')) {
                    searchText = !element.value ? null : element.value;
                }
                break;

            case 'txtSearchText':
                var text = $.trim(element.value);
                searchText = !text ? null : text;
                break;
        }
    });

    var inboxState = getInboxState(activeInbox);

    var clearanceInboxModel = {
        clearanceInboxModel: {
            FilterCriteria: { RequestType: requestType, RccAdminRequestType: rccAdminRequestType, RccHandler: rccHandler, Requestor: requestor, ScopeType: scopeType, Workgroup: workgroup },
            SearchCriteria: { SearchText: searchText, SearchType: searchType, InboxState: inboxState }
        }
    };

    return clearanceInboxModel;
}

function getInboxState(activeInbox) {

    var activeLeftPanel = activeInbox.find('#divLeftPanel');

    var selectedFolderId = null;
    var selectedProjectId = null;
    var showAllProjects = null;
    var sortBy = null;

    var selectedFolderId = getSelectedFolderId(activeLeftPanel);

    var selectedProject = activeLeftPanel.find('.GridContent table tbody tr .SelectionBackground').closest('tr');

    selectedProjectId = selectedProject.find('.clearanceProjectId').text() || null;

    showAllProjects = activeLeftPanel.find('.GroupCaption.Expand').find('.folder-Container').find('.showDefault.displayBlock').length != 0 ? true : false;

    sortBy = getInboxSortState(activeInbox);

    return {
        FolderSize: const_DefaultFolderSize.toString(),
        SelectedFolderId: selectedFolderId,
        SelectedProjectId: selectedProjectId,
        ShowAllProjects: showAllProjects,
        SortByDirection: sortBy ? sortBy['sortDirection'] : null,
        SortByColumnName: sortBy ? sortBy['sortColumn'] : null
    };
}

function getSelectedFolderId(activeLeftPanel) {
    var selectedFolder = activeLeftPanel.find('.GroupCaption.Expand').find('.folder-Container')[0];
    var selectedFolderName = selectedFolder == undefined ? null : selectedFolder.id;
    var selectedFolderId = selectedFolderName == null ? null : selectedFolderName.replace('folder-', '');
    return selectedFolderId;
}

function getInboxSortState(activeInbox) {

    var gridId = activeInbox.find('#divLeftPanel').find('div[id^=grdFolder-]')[0].id;
    grid = $find(gridId);
    var sortDirection = grid._sortdirection[0] || null;
    var sortColumn = grid._sortedColumns[0] || null;

    return sortColumn && sortDirection ? { sortColumn: sortColumn, sortDirection: sortDirection } : null;
}

function refreshInbox() {

    getSearchResult();

}

function clearSearchResult() {
    var activeInbox = _activeInbox;

    var txtSearchText = activeInbox.find('#txtSearchText');
    var ddlAssignedToUser = activeInbox.find('#ddlAssignedToUser');
    var ddlSearchType = activeInbox.find('#ddlSearchType');

    txtSearchText.val('');
    ddlAssignedToUser.length != 0 ? ddlAssignedToUser.get(0).selectedIndex = 0 : null;
    ddlSearchType.get(0).selectedIndex = 0;

    txtSearchText.removeClass('displayNone').addClass('displayBlock');
    ddlAssignedToUser.removeClass('displayBlock').addClass('displayNone');

    getSearchResult();
}

function displayFilterState() {
    var activeInbox = _activeInbox;
    var filterCriteria = getFilters().clearanceInboxModel.FilterCriteria;

    if (filterCriteria.RequestType.length > 0
        || filterCriteria.RccAdminRequestType.length > 0
        || filterCriteria.RccHandler.length > 0
        || filterCriteria.Requestor.length > 0
        || filterCriteria.ScopeType.length > 0
        || filterCriteria.Workgroup.length > 0)

        activeInbox.find('#inboxSearch #filters').removeClass('filters-inactive').addClass('filters-active');
    else
        activeInbox.find('#inboxSearch #filters').removeClass('filters-active').addClass('filters-inactive');

}

//#endregion - Header

//#region - Left Panel

function displayInbox(sender, roleGroup) {

    _activeInbox = $($(sender).attr('href'));
    _activeRoleGroup = roleGroup;
    RefreshEmailPanel(0, 1);

    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;

    if (activeInbox.is(':not(:empty)')) {
        return;
    };

    displayLoadingPanel(activeInbox, true);

    $.ajax({
        url: url_GetInbox,
        type: 'POST',
        data: { roleGroup: activeRoleGroup },
        async: 'async',
        success: function (data) {

            if (data.Error != true) {

                refreshLeftPanel(activeInbox, false, data);

                displayFilterState();

            }
            else {

                displayMessage(activeInbox, true, MessageType.Error, data.Message);

            }
        },
        error: function () {

            displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);

        },
        complete: function () {

            displayLoadingPanel(activeInbox, false);

        }
    });
}

function saveInboxFolder(folderId) {

    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;

    var activeLeftPanel = activeInbox.find(' #divLeftPanel');

    var changedProjects = activeLeftPanel.find('#grdFolder-' + _activeRoleGroup + '-Projects .dataChanged');
    var changedProjectRows = changedProjects.closest('tr');

    var projects = [];

    $(changedProjectRows).each(function (index, element) {

        element = $(element);

        projects.push({
            ClearanceProjectId: element.find('.clearanceProjectId').text(),
            WorkgroupId: element.find('.workgroupId').text(),
            CurrentFolderId: element.find('.currentFolderId').text(),
            AssignedToUserId: element.find('.dataChanged').val(),
            ModifiedUserAssignedTo: element.find('.modifiedUserAssignedTo').text(),
            ModifiedDateAssignedTo: element.find('.modifiedDateAssignedTo').text()
        });

    });

    if (projects.length == 0) {

        displayMessage(activeInbox, true, MessageType.Error, msg_NoChangesToSave);
        return;

    }

    var postData = {
        roleGroup: activeRoleGroup,
        clearanceInboxModel: getFilters().clearanceInboxModel,
        clearanceInboxFolder: { Projects: projects },
        columnSetting: inboxColumnSetting.settings.columnSettingToSave
    };

    displayLoadingPanel(activeInbox, true);

    $.ajax({
        url: url_SaveInboxFolder,
        type: 'POST',
        data: JSON.stringify(postData),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            if (data.Error != true) {

                refreshLeftPanel(activeInbox, true, data);

            }
            else {

                displayMessage(activeInbox, true, MessageType.Error, data.Message);

            }
        },
        error: function () {

            displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);

        },
        complete: function () {

            displayLoadingPanel(activeInbox, false);

        }
    });

}

function manageFolder(folderId, folderName, projectCount, folderAction) {

    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;
    var activeLeftPanel = activeInbox.find(' #divLeftPanel');

    var popup = activeInbox.find('.popup-inbox-manageFolder').clone(true);
    var popupTitle;

    displayMessage(activeInbox, false);

    var lblFolderMessage = popup.find('#lbl-folder-message');
    var lblFolderName = popup.find('#lbl-folder-name');
    var txtFolderName = popup.find('#txt-folder-name');

    var btnCreate = popup.find('#btn-folder-create');
    var btnSave = popup.find('#btn-folder-save');
    var btnCancel = popup.find('#btn-folder-cancel');

    var allPopupControls = $([]).add(lblFolderMessage).add(lblFolderName).add(txtFolderName)
                                .add(btnCreate).add(btnSave).add(btnCancel);
    btnCreate.click(function () { manageFolder_doRequest(); });
    btnSave.click(function () { manageFolder_doRequest(); });
    btnCancel.click(function () { closePopup(); });

    switch (folderAction) {

        case FolderAction.Create:

            popupTitle = lbl_NewFolder;
            lblFolderName.text(lbl_FolderName);
            txtFolderName.val('');
            txtFolderName.focus(function () { $(this).select(); });

            $([]).add(lblFolderName)
                 .add(txtFolderName)
                 .add(btnCreate)
                 .add(btnCancel)
                 .removeClass('displayNone')
                 .addClass('displayBlock');
            break;

        case FolderAction.Edit:

            popupTitle = lbl_EditFolder;
            lblFolderName.text(lbl_FolderName);
            txtFolderName.val(folderName);
            txtFolderName.focus(function () { $(this).select(); });

            $([]).add(lblFolderName)
                 .add(txtFolderName)
                 .add(btnSave)
                 .add(btnCancel)
                 .removeClass('displayNone')
                 .addClass('displayBlock');
            break;

        case FolderAction.Delete:

            if (projectCount > 0) {

                displayMessage(activeInbox, true, MessageType.Error, msg_FolderContainsProjects);
                return;

            }

            var msgFolderDelete = popup.find('.msg-folder-delete').clone(true);

            msgFolderDelete.find('.folderToDelete').text(folderName);

            displayMessage(activeInbox, true, MessageType.Warning, msgFolderDelete[0].innerHTML, ContentType.Html);

            var btnFolderDelete = activeInbox.find('#divMessage .btn-folder-delete');
            var btnFolderDeleteCancel = activeInbox.find('#divMessage .btn-folder-delete-cancel');

            btnFolderDelete.click(function () { manageFolder_doRequest(); });
            btnFolderDeleteCancel.click(function () { displayMessage(activeInbox, false); });

            return;
            break;
    }

    popup.dialog({
        autoOpen: false,
        height: 120,
        width: 410,
        modal: true,
        resizable: false,
        title: popupTitle,
        close: closePopup
    }).css('z-index', 99999);

    popup.dialog('open');

    function manageFolder_doRequest() {


        function showError(messageText) {

            lblFolderMessage.parent().addClass(MessageType.Error).show();
            lblFolderMessage.text(messageText).show();

        }

        function hideError() {

            lblFolderMessage.parent().removeClass(MessageType.Error).hide();
            lblFolderMessage.text('').hide();

        }

        hideError();

        switch (folderAction) {

            case FolderAction.Create:

                folderName = $.trim(txtFolderName.val());

                if (folderName == '') { showError(msg_EnterFolderName); return; }

                if (folderName.length > const_MaxLengthForFolderName) { showError(msg_FolderNameLengthExceeded); return; }

                break;

            case FolderAction.Edit:

                var newfolderName = $.trim(txtFolderName.val());

                if (newfolderName == '') { showError(msg_EnterFolderName); return; }

                if (newfolderName.length > const_MaxLengthForFolderName) { showError(msg_FolderNameLengthExceeded); return; }

                else if (newfolderName == folderName) { showError(msg_EnterDifferentFolderName); return; }

                folderName = newfolderName;

                break;

            case FolderAction.Delete:

                break;
        }

        var postData = {
            roleGroup: activeRoleGroup,
            clearanceInboxModel: getFilters().clearanceInboxModel,
            folderId: folderId,
            folderName: folderName,
            folderAction: folderAction,
            columnSetting: inboxColumnSetting.settings.columnSettingToSave
        };

        displayLoadingPanel(activeInbox, true);

        $.ajax({
            url: url_ManageInboxFolders,
            type: 'POST',
            data: JSON.stringify(postData),
            async: 'async',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {

                if (data.Error != true) {

                    closePopup();

                    refreshLeftPanel(activeInbox, true, data);

                }
                else {

                    displayMessage(activeInbox, true, MessageType.Error, data.Message);

                }
            },
            error: function () {

                displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);

            },
            complete: function () {

                displayLoadingPanel(activeInbox, false);

            }
        });
    }

    function closePopup() {

        popup.remove();

    }
}

function deleteUnsubmittedProjects() {

    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;

    var activeLeftPanel = activeInbox.find(' #divLeftPanel');

    var projects = [];

    //--
    var clearanceProjects = activeLeftPanel.find('#grdFolder-Requestor-Projects_Table');
    var selectedProjects = clearanceProjects.find('.unsubmittedRequest:input:checked');

    $(selectedProjects).each(function (index, element) {

        projects.push({ ClearanceProjectId: $(element).val() });

    });

    if (projects.length == 0) {

        return;

    }

    var postData = {
        roleGroup: activeRoleGroup,
        clearanceInboxModel: getFilters().clearanceInboxModel,
        clearanceInboxFolder: { Projects: projects },
        columnSetting: inboxColumnSetting.settings.columnSettingToSave
    };

    displayLoadingPanel(activeInbox, true);

    $.ajax({
        url: url_DeleteUnsubmittedProjects,
        type: 'POST',
        data: JSON.stringify(postData),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            if (data.Error != true) {

                refreshLeftPanel(activeInbox, true, data);

            }
            else {

                displayMessage(activeInbox, true, MessageType.Error, data.Message);

            }
        },
        error: function () {

            displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);

        },
        complete: function () {

            displayLoadingPanel(activeInbox, false);

        }
    });

}

function showAllProjects(folderId, trueFalse) {
    var activeRoleGroup = _activeRoleGroup;

    var clearanceInboxModel = getFilters().clearanceInboxModel;

    clearanceInboxModel.SearchCriteria.InboxState.SelectedFolderId = folderId.toString();
    clearanceInboxModel.SearchCriteria.InboxState.ShowAllProjects = trueFalse;

    $('#hdnFolderToExpandByDefault-' + activeRoleGroup).val(folderId.toString());

    getSearchResult(clearanceInboxModel);
}

function updateSearchCriteria(roleGroup) {

    if (roleGroup == RoleGroup.Reviewer) {

        //--
        var activeInbox = _activeInbox;

        //--
        var txtSearchText = activeInbox.find('#txtSearchText');
        var ddlAssignedToUser = activeInbox.find('#ddlAssignedToUser');
        var ddlSearchType = activeInbox.find('#ddlSearchType');

        //--
        if (ddlSearchType.val() == const_ReviewerSearchCriteriaAssignedTo) {

            txtSearchText.removeClass('displayBlock').addClass('displayNone');
            ddlAssignedToUser.removeClass('displayNone').addClass('displayBlock');

        }
        else {

            txtSearchText.removeClass('displayNone').addClass('displayBlock');
            ddlAssignedToUser.removeClass('displayBlock').addClass('displayNone');

        }

    }

}

function updateProjectReadStatus(dataRow, isUnread, clearanceProjectId, workgroupId, folderId) {

    //--
    var activeInbox = _activeInbox;

    //--
    if ((isUnread == true) && (dataRow.hasClass('displayBold') == false)) {
        return;
    }

    //--
    var markAsUnread = dataRow.find('.isUnread');

    //--
    var postData = {
        clearanceInboxProject: {
            CurrentFolderId: folderId,
            ClearanceProjectId: clearanceProjectId,
            WorkgroupId: workgroupId,
            IsUnread: isUnread
        }
    };

    //--
    $.ajax({
        url: url_UpdateProjectReadStatus,
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: 'json',
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            if (data.Error != true) {

                if (isUnread) {

                    markAsUnread.removeClass('displayNone').addClass('displayBlock');

                    dataRow.removeClass('displayBold');

                }
                else {

                    markAsUnread.removeClass('displayBlock').addClass('displayNone');

                    dataRow.addClass('displayBold');

                }
            }

            else {

                displayMessage(activeInbox, true, MessageType.Error, data.Message);
            }
        },
        error: function () {

            displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);

        },
        complete: function () {



        }
    });
}

function viewProject(clearanceProjectId, projectTypeId, projectStatusId) {

    var activeRoleGroup = _activeRoleGroup;

    //--
    var wwidth = $(window).width() - 50;
    var wheight = $(window).height() - 50;
    var url = url_OpenProjectDetail + '?Projectid=' + clearanceProjectId + '&ProjectTypeId=' + projectTypeId + '&ProjectStatus=' + projectStatusId + '&RoleGroup=' + activeRoleGroup;

    //--
    var child = window.open(url, 'ProjectDetail' + clearanceProjectId, 'width=' + wwidth + 'px,height=' + wheight + 'px,top=40px,left=10px,resizable=yes', false);
}

function markDataAsChanged(dropdown, originalValue) {

    //--
    var selectedValue = dropdown.val();

    //--
    selectedValue == originalValue ? dropdown.removeClass('dataChanged') : dropdown.addClass('dataChanged');

}

function onGridRecordSelected(sender, args) {

    //--
    preventMultipleRowSelection(sender);

    //--    
    updateProjectReadStatus($(args.row), true, args.record.ClearanceProjectId, args.record.WorkgroupId, args.record.FolderId);

    //--
    var record = args.record;

    //--
    selectedFolderId = record.CurrentFolderId || null;

    selectedProjectId = record.ClearanceProjectId || null;

    _projectStatus = record.ProjectStatusId;

    _projectType = record.ProjectTypeId;

    //--
    CallRightHandPanel(sender, args);

}

function onActionBegin(sender, args) {
    //--
    args.cancel = true;

    //--
    var header = $find(args.data.gridID);

    //--
    var sortCell = $(header.currentSortedCell);
    var sortColumn = header.currentSortColumn;
    var sortDirection = header.currentContextSortDirection;
    var sortColumnIndex = header.currentColIdx;

    //--
    sortCell.closest('tr').find('span.sortDirection').remove();
    sortCell.append('<span class="sortDirection ' + (sortDirection == 'Ascending' ? 'Ascending' : 'Descending') + '"></span>');

    //--
    $('#' + args.data.gridID).attr({ sortColumn: sortColumn, sortDirection: sortDirection, sortColumnIndex: sortColumnIndex });

    //--
    getSearchResult();

}

function preventMultipleRowSelection(sender) {

    //--
    var activeInbox = _activeInbox;
    var activeLeftPanel = activeInbox.find(' #divLeftPanel');

    //--
    activeLeftPanel.find('div[id^=grdFolder-]:not(#' + sender._ID + ') .GridContent table tbody td.SelectionBackground').removeClass('SelectionBackground');

}

function enableDragDrop(activeInbox) {

    //--
    var activeLeftPanel = activeInbox.find(' #divLeftPanel');
    var activeRoleGroup = _activeRoleGroup;

    //--
    var folders = activeLeftPanel.find("div[id^=grdFolder-] .GridContent tbody tr .GroupCaption");
    var projects = activeLeftPanel.find("div[id^=grdFolder-] .GridContent tbody tr:has(td.clearanceProjectId)");

    //--
    var dragHelper;

    //--
    var dragContainer = activeInbox.find('.inboxContent');

    //--
    projects.draggable({
        scope: 'inbox',
        addClasses: false,
        cursorAt: { top: -5, left: -5 },
        opacity: 0.95,
        zIndex: 10000,
        scroll: true,
        scrollSpeed: 20,
        scrollSensitivity: 20,
        containment: dragContainer,
        helper: getDragHelper,
        start: showDragHelper,
        drag: dragDragHelper,
        stop: disposeDragHelper
    });

    folders.droppable({
        scope: 'inbox',
        addClasses: false,
        hoverClass: 'droppable',
        tolerance: 'pointer',
        drop: moveProject
    });

    function getDragHelper(event, ui) {

        //--
        var draggedProject = $(event.target).closest('tr');

        //--
        var projectTitle = draggedProject.find('.projectTitle').text();
        var projectReferenceNumber = draggedProject.find('.projectReferenceNumber a').text();
        var projectType = draggedProject.find('.projectType').text();

        //--        
        dragHelper = activeInbox.find('#popup-dragHelper').clone();

        //--
        dragHelper.find('#projectTitle').text(projectTitle);
        dragHelper.find('#projectReferenceNumber').text(projectReferenceNumber);
        dragHelper.find('#projectType').text(projectType);

        //--
        dragContainer.append(dragHelper);

        return dragHelper;
    }

    function showDragHelper(event, ui) {
        $(event.target).closest('.folder-Container').droppable('destroy');
        dragHelper.show();
    }

    function dragDragHelper(event, ui) {
        ui.position.top = event.clientY - dragContainer.offset().top + dragContainer.scrollTop() + 10;
    }

    function disposeDragHelper() {
        if (dragHelper != null) {
            dragHelper.remove();
            dragHelper = null;
        }
    }

    function moveProject(event, ui) {

        //--
        displayMessage(activeInbox, false);

        //--
        var draggedProject = $(ui.draggable);

        var dropFolder = $(event.target).find('.folder-Container')[0].id;

        //--
        var currentFolderId = draggedProject.find('.currentFolderId').text();
        var originalSystemFolderId = draggedProject.find('.originalSystemFolderId').text();
        var toFolderId = dropFolder.substring(dropFolder.indexOf('-') + 1);
        var clearanceProjectId = draggedProject.find('.clearanceProjectId').text();

        var workgroupId = draggedProject.find('.workgroupId').text();

        //--
        var projectIsDraggedFromSystemFolder = $("#folder-" + currentFolderId).hasClass("isSystemFolder");
        var projectIsDroppedToSystemFolder = $("#folder-" + toFolderId).hasClass("isSystemFolder");

        //--
        if (toFolderId == currentFolderId) {
            return;
        }

        //--
        if (projectIsDraggedFromSystemFolder && projectIsDroppedToSystemFolder) {
            displayMessage(activeInbox, true, MessageType.Error, msg_ProjectCannotBeMovedBetweenSystemFolders);
            return;
        }

        //--
        if ((projectIsDroppedToSystemFolder) && (toFolderId != originalSystemFolderId)) {
            displayMessage(activeInbox, true, MessageType.Error, msg_ProjectCanOnlyBeMovedToItsOriginalSystemFolder);
            return;
        }

        //--
        var currentFolderName = $('#folderName_' + currentFolderId).val().toUpperCase();
        var toFolderName = $('#folderName_' + toFolderId).val().toUpperCase();

        if (projectIsDraggedFromSystemFolder || projectIsDroppedToSystemFolder) {
            if ((SystemFolder.ReviewerArtistConsent == currentFolderId) || (SystemFolder.ReviewerArtistConsent == toFolderId) ||
                (SystemFolder.Research == currentFolderName) || (SystemFolder.Research == toFolderName) ||
                (SystemFolder.InternalReview == currentFolderName) || (SystemFolder.InternalReview == toFolderName) ||
                (SystemFolder.SideArtistSample == currentFolderName) || (SystemFolder.SideArtistSample == toFolderName)) {
                displayMessage(activeInbox, true, MessageType.Error, msg_ProjectCannotBeMovedFromOrToStaticFolder);
                return;
            }
        }

        //--
        var postData = {
            roleGroup: activeRoleGroup,
            clearanceInboxModel: getFilters().clearanceInboxModel,
            clearanceInboxProject: {
                CurrentFolderId: currentFolderId,
                OriginalSystemFolderId: originalSystemFolderId,
                ToFolderId: toFolderId,
                ClearanceProjectId: clearanceProjectId,
                WorkgroupId: workgroupId
            },
            columnSetting: inboxColumnSetting.settings.columnSettingToSave
        };

        //--
        displayLoadingPanel(activeInbox, true);

        $.ajax({
            url: url_ManageInboxProjects,
            type: 'POST',
            data: JSON.stringify(postData),
            async: 'async',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {

                if (data.Error != true) {

                    refreshLeftPanel(activeInbox, true, data);

                }
                else {

                    displayMessage(activeInbox, true, MessageType.Error, data.Message);

                }
            },
            error: function () {

                displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);

            },
            complete: function () {

                displayLoadingPanel(activeInbox, false);

            }
        });
    }
}

function getSelectedFolder(activeInbox, inboxType) {
    var folderId = $('#hdnFolderToExpandByDefault-' + inboxType).val();

    if (folderId == 0) {
        var id = '#grdFolder-' + inboxType + '-Projects .GroupCaption.Expand .folder-Container';
        if (activeInbox.find(id).length > 0) {
            folderId = activeInbox.find(id)[0].id.replace('folder-', '');
        }
    }
    return folderId;
}

function refreshLeftPanel(activeInbox, searchResultsOnly, data) {
    //--
    var activeRoleGroup = _activeRoleGroup;

    //--
    var selectedFolder = getSelectedFolder(activeInbox, activeRoleGroup);

    //--
    var regionToRefresh = searchResultsOnly ? activeInbox.find('#divLeftPanel') : activeInbox;

    //--
    regionToRefresh.html(data);

    //--
    applyCustomTheam(_activeRoleGroup);

    //--
    preventRowSelection(activeInbox);

    //--
    layoutInbox(activeInbox, activeRoleGroup);

    //--
    if (activeRoleGroup != RoleGroup.Requestor) {

        enableDragDrop(activeInbox);

    }
    if (activeInbox.find("#hdnConcurrencyError").val() != null && activeInbox.find("#hdnConcurrencyError").val() != "") {

        var objWarningDialog = $("<div id='Confirm'></div>")
            .html("<p><b> " + activeInbox.find("#hdnConcurrencyError").val() + " </b></p>")
            .dialog({
                autoOpen: false,
                modal: true,
                show: 'clip',
                hide: 'clip',
                width: 500,
                height: 200
            });
        objWarningDialog.dialog('open');
        objWarningDialog.dialog({
            buttons:
                {
                    'Ok':
                     function (e) {
                         $(this).dialog('close');
                         activeInbox.find("#hdnConcurrencyError").val("");
                     }
                }
        });
    }

    eraseGridGroupColumn();

    folderClickEvent(null, selectedFolder, activeRoleGroup);
}

function preventRowSelection(activeInbox) {

    //--
    activeInbox.find(' #divLeftPanel .preventRowSelection')
        .closest('td')
        .bind('click mousedown', function (event) {
            event.cancelBubble = true;
            if (event.stopPropagation) {
                event.stopPropagation();
            }
        });
}

function eraseGridGroupColumn() {
    var myTable = '#grdFolder-' + _activeRoleGroup + '-Projects .Table';
    var length = $(myTable).find("colgroup").length;
    for (var i = 0; i < length; i++) {
        $(myTable).find("colgroup")[i].childNodes[1].style.width = "0px";
    }
}

function folderClickEvent(sender, folderId, inboxType) {

    if (folderId == undefined) {
        folderId = $('#hdnFolderToExpandByDefault-' + inboxType).val();
    }

    var gridName = 'grdFolder-' + inboxType + '-Projects';
    var id = '#' + gridName + ' #folder-' + folderId;
    var collapseClass = '.RecordPlusCollapse';
    var expandClass = '.RecordPlusExpand';

    var length = $('#' + gridName).find('.GroupCaption').length;

    if ($(id).closest('tr').find(collapseClass).length > 0) {

        var selectedClassName = $(id).closest('tr').find('.GroupCaption')[0].className;

        $find(gridName).collapseAll();

        for (var i = 0; i < length - 1; i++) {
            $('#' + gridName).find('.GroupCaption')[i].className = 'GroupCaption Collapse'
        }

        if (selectedClassName != 'GroupCaption Collapse EmptyFolder') {
            $(id).closest('tr').find(collapseClass).click();
            $(id).closest('tr').find('.GroupCaption')[0].className = 'GroupCaption Expand'

        }
    }
    else if ($(id).closest('tr').find(expandClass).length > 0) {
        $(id).closest('tr').find(expandClass).click();
        $(id).closest('tr').find('.GroupCaption')[0].className = 'GroupCaption Collapse'
    }
}

//#endregion - Left Panel

//#region - Right Panel
var flag = true;

function CallRightHandPanel(sender, args) {

    displayLoadingPanel(true);
    var clrProjectId = args.record.ClearanceProjectId;
    var workGroupId = args.record.WorkgroupId;
    var folderId = args.record.CurrentFolderId;
    var roleName = $(args.record.RoleName).attr('title');
    var projectTypeId = args.record.ProjectTypeId;
    var roleGroup;

    _roleName = roleName;
    if (_activeRoleGroup == RoleGroup.Requestor) {
        roleGroup = RoleGroup.Requestor;
        _clrProjectId_Requestor = clrProjectId;
        _workGroupId_Requestor = workGroupId;
        _folderId_Requestor = folderId;
        _projectType = projectTypeId;
    }
    if (_activeRoleGroup == RoleGroup.Reviewer) {
        roleGroup = RoleGroup.Reviewer;
        _clrProjectId_Reviewer = clrProjectId;
        _workGroupId_Reviewer = workGroupId;
        _folderId_Reviewer = folderId;
    }
    if (_activeRoleGroup == RoleGroup.RCCAdmin) {
        roleGroup = RoleGroup.RCCAdmin;
        _clrProjectId_RCCAdmin = clrProjectId;
        _workGroupId_RCCAdmin = workGroupId;
        _folderId_RCCAdmin = folderId;
        roleName = "RCC Admin";
        _roleName = roleName;
    }
    var postData = {
        args: null,
        roleGroup: roleGroup,
        clearanceInboxRequestAction: {
            WorkgroupId: workGroupId,
            FolderId: folderId,
            ProjectId: clrProjectId,
            ProjectType: projectTypeId,
            RoleName: roleName
        },
        gridType: 0
    };
    RefreshRightPanel(postData);
}

function RefreshRightpanelGrid(inputData) {

    switch (inputData.roleGroup) {

        case "Requestor": if (inputData.gridType == 0) {
            $('#TblRequestorResourceGrid .RefreshPager').click()
            return;
        }

        else if (inputData.gridType == 1) {
            $('#TblRequestorTrackGrid .RefreshPager').click()
            return;
        }
            break;
        case "Reviewer": if (inputData.gridType == 1) {
            $('#TblReviewerResourceGrid .RefreshPager').click()
            return;
        }

        else if (inputData.gridType == 2) {
            $('#TblReviewerTrackGrid .RefreshPager').click()
            return;
        }
            break;
        case "RCCAdmin": if (inputData.gridType == 1) {
            $('#TblRCCAdminResourceGrid .RefreshPager').click()
            return;
        }

        else if (inputData.gridType == 2) {
            $('#TblRCCAdminTrackGrid .RefreshPager').click()
            return;
        }
            break;
    }
}

function RefreshRightPanel(postData) {

    var activeInbox = _activeInbox;
    displayLoadingPanel(activeInbox, true);

    $.ajax({
        url: '/GCS/ClearanceInbox/GetInboxProjectDetail',
        type: 'POST',
        data: JSON.stringify(postData),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            activeInbox.find('#RightPane').html(data).promise().done(resizeRightHeader(_activeRoleGroup));
            if (_activeRoleGroup == RoleGroup.Reviewer) {
                if ($("#hdnRComments").length > 0) {
                    if ($("#hdnRComments").val() != null) {
                        $("#review_comment").text($("#hdnRComments").val());
                    }
                }
            }

            displayLoadingPanel(activeInbox, false);

            RefreshEmailPanel(1, _projectStatus, _projectType);
        },
        error: function (data) {
            displayLoadingPanel(activeInbox, false);
        }
    });

}

function ActionBeginRequestorResource(sender, args) {

    var gridType = 1;
    args.data["clearanceInboxRequestAction.WorkgroupId"] = _workGroupId_Requestor;
    args.data["clearanceInboxRequestAction.FolderId"] = _folderId_Requestor;
    args.data["clearanceInboxRequestAction.ProjectId"] = _clrProjectId_Requestor;
    args.data["clearanceInboxRequestAction.RoleName"] = _roleName;
    args.data["clearanceInboxRequestAction.ProjectType"] = _projectType;
    args.data["gridType"] = gridType;
    args.data["rolegroup"] = RoleGroup.Requestor;

}

function ActionBeginRequestorTrack(sender, args) {
    var gridType = 2;
    args.data["clearanceInboxRequestAction.WorkgroupId"] = _workGroupId_Requestor;
    args.data["clearanceInboxRequestAction.FolderId"] = _folderId_Requestor;
    args.data["clearanceInboxRequestAction.ProjectId"] = _clrProjectId_Requestor;
    args.data["clearanceInboxRequestAction.RoleName"] = _roleName;
    args.data["clearanceInboxRequestAction.ProjectType"] = _projectType;
    args.data["gridType"] = gridType;
    args.data["rolegroup"] = RoleGroup.Requestor;

}

function processProjectAction(action) {
    var activeInbox = _activeInbox;
    var clrProjectId = _clrProjectId_Requestor;
    var workGroupId = _workGroupId_Requestor;
    var folderId = _folderId_Requestor;
    document.getElementById('divProjectActions').style.display = "none";
    postData = {
        clearanceInboxModel: getFilters().clearanceInboxModel,
        clearanceInboxRequestAction: {
            WorkgroupId: _workGroupId_Requestor,
            FolderId: _folderId_Requestor,
            ProjectId: _clrProjectId_Requestor,
            Requests: null,
            ActionId: action,
            Comment: null,
            ProjectModifiedDateString: $("#hdnProjectModifiedDate").val()
        },
        roleGroup: RoleGroup.Requestor,
        selectAllAcrossPages: false,
        gridType: 0,
        columnSetting: inboxColumnSetting.settings.columnSettingToSave
    };
    if (action == "15") {
        var value =
        {
            WorkgroupId: workGroupId,
            FolderId: folderId,
            ProjectId: clrProjectId,
            Requests: null,
            ActionId: action,
            Comment: $("#projectRefNum").text()
        };
        var objDialog = $('<div id="massReminder" style="margin:0;padding:0;"></div>');
        objDialog.dialog({
            resizable: false,
            width: 600,
            title: 'Mass Reminders',
            modal: true,
            close: function () {
                $(this).remove(); // ensures any form variables are reset.  
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            },
            open: function () {

                $(this).load("/GCS/ClearanceInbox/MassReminders", value);
            }
        });
        objDialog.dialog('open');
        var dialogue = $('.ui-dialog');
        dialogue.animate({

            top: "60px"

        }, 0);

    }
    else {
        displayLoadingPanel(activeInbox, true);
        $.ajax({
            url: '/GCS/ClearanceInbox/PerformRequestAction',
            type: 'POST',
            data: JSON.stringify(postData),
            async: 'async',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
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

                var exist = TestSelectRow(_activeRoleGroup, clrProjectId, activeInbox, _folderId_Requestor);
                if (exist) {
                    RefreshRightpanelGrid(postDataR);
                }
                else {
                    activeInbox.find('#RightPane').empty();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

                displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);

            }
        });
    }

}

function ToogleDiv() {

    if (document.getElementById('divProjectActions').style.display == "none") {
        document.getElementById('divProjectActions').style.display = "block";
    }
    else {
        document.getElementById('divProjectActions').style.display = "none";
    }
}

function ShowDiv(divID) {
    document.getElementById('divProjectActions').style.display = "block";
}

function SetFlag() {
    flag = true;

}

function SetFlagFalse(divID) {
    document.getElementById('divProjectActions').style.display = "none";


}

function Toggle(divID) {
    if (flag) {
        flag = false;
        document.getElementById('divProjectActions').style.display = "block";

    }
    else {
        flag = true;
        document.getElementById('divProjectActions').style.display = "none";
    }
}

function actionOnRequest(projReqId) {
    var Beginslash = "\"\\/";
    var EndSlash = "\\/\"";
    var activeInbox = _activeInbox;
    var selectedRequests = [];
    var routedItemRequest = [];
    var substr = projReqId.split('~');
    var ClearanceProjectId = substr[0];
    var RequestId = substr[1];
    var RoutedItemId = substr[2];
    var WorkGroupId = substr[3];
    var Action = substr[4];
    var ModifiedDatetime = Beginslash + substr[5] + EndSlash;
    var ModifiedUser = substr[6];
    var ModifiedDatetimeRequest = Beginslash + substr[7] + EndSlash;
    var comments = "";
    if (substr[8] != null) {
        comments = substr[8];
    }
    routedItemRequest.push({ Key: RoutedItemId, Value: RequestId });
    selectedRequests.push({
        KeyRoutedItemRequest: routedItemRequest,
        Comment: comments,
        RoutedItemId: RoutedItemId,
        RequestId: RequestId,
        ModifiedUser: ModifiedUser, // for concurrency implementation
        ModifiedDateRoutedString: ModifiedDatetime, // for concurrency implementation
        ModifiedDateRequestString: ModifiedDatetimeRequest // for concurrency implementation
    });

    postData = {
        clearanceInboxModel: getFilters().clearanceInboxModel,
        clearanceInboxRequestAction: {
            WorkgroupId: _workGroupId_Requestor,
            ToWorkgroupId: WorkGroupId,
            FolderId: null,
            ProjectId: ClearanceProjectId,
            Requests: selectedRequests,
            ActionId: Action,
            Comment: comments || null
        },
        roleGroup: RoleGroup.Requestor,
        selectAllAcrossPages: false,
        gridType: 0
    };

    var clrProjectId = _clrProjectId_Requestor;
    var workGroupId = _workGroupId_Requestor;
    var folderId = _folderId_Requestor;
    displayLoadingPanel(activeInbox, true);
    $.ajax({
        url: '/GCS/ClearanceInbox/PerformRequestAction',
        type: 'POST',
        data: JSON.stringify(postData),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            refreshLeftPanel(activeInbox, true, data);
            // have to write  code selected project on lefthand side with global variables     

            var postDataR = {
                args: null,
                roleGroup: RoleGroup.Requestor,
                clearanceInboxRequestAction: {
                    WorkgroupId: workGroupId,
                    FolderId: folderId,
                    ProjectId: clrProjectId,
                    RoleName: _roleName
                },
                gridType: $("#tabsRequestor").tabs('option', 'selected')
            };
            displayLoadingPanel(activeInbox, false);

            var exist = TestSelectRow(_activeRoleGroup, clrProjectId, activeInbox, folderId);
            if (exist) {
                RefreshRightpanelGrid(postDataR);
            }
            else {
                activeInbox.find('#RightPane').empty();
            }
        },
        error: function () {
            displayLoadingPanel(activeInbox, false);
        }
    });

}

function TestSelectRow(inboxType, clrProjectId, activeInbox, folderId) {
    var exist = false;
    var currentRowsTable = '#grdFolder-' + inboxType + '-Projects_Table tr';

    $(currentRowsTable).each(function () {
        $this = $(this)
        if ($this.find(".clearanceProjectId").length > 0) {
            var ProjectRowId = $this.find(".clearanceProjectId")[0].innerText;
            var folderRowId = $this.find(".FolderId")[0].innerText;

            if (folderRowId == folderId && ProjectRowId == clrProjectId) {
                exist = true;
                return false;
            }
        }
    });

    return exist;
}

function openReapplyCommentDialog(projReqId) {

    var windowWidth = $(document).width;
    var objReapplyPopup = $('<div id="ReapplyComment"></div>')
                .html('<br/><input id="CommentTextArea" type="textarea" style="height:60px;width:97%;float:right"/>')
                .dialog({
                    autoOpen: false,
                    resizable: false,
                    modal: true,
                    show: 'clip',
                    hide: 'clip',
                    width: "28%",
                    position: [(window.mouseXPos - 50), window.mouseYPos + 10],
                    open: function (event) {
                        $('.ui-dialog-buttonset').find('button:contains("Cancel")').addClass('reqSumCancelButton');
                    },
                    close: function () {
                        $(this).remove(); // ensures any form variables are reset.
                    }
                });
    objReapplyPopup.dialog('option', { title: "Re-Apply" });
    objReapplyPopup.dialog({
        buttons: {
            Cancel: function () { $(this).dialog('close'); }, 'Submit': function () {

                var cmnt = $("#CommentTextArea").val();
                projReqId = projReqId + '~' + cmnt;
                if (cmnt != "") {
                    $(this).dialog('close');
                    actionOnRequest(projReqId);
                    $('#CommentTextArea').removeClass('input-validation-error');
                }
                else {
                    $('#CommentTextArea').addClass('input-validation-error');
                }
            }
        }
    });
    objReapplyPopup.dialog('open');
}

function routingInfo(proj) {
    var substr = proj.split('~');
    var ClearanceProjectId = substr[0];
    var RequestId = substr[1];
    var RoutedItemID = substr[2];
    var windowWidth = $(document).width;
    var windowHeight = $(document).height;
    var routingDetailsPopup = $('<div id="routingDetailsMainDiv"></div>')
             .html('<p>' + 'Loading...' + '</p>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        show: 'clip',
                        hide: 'clip',
                        width: "75%",
                        height: 380,
                        position: [(windowWidth / 4), windowHeight / 4],
                        open: function (event, ui) {
                            $('#routingDetailsMainDiv').css('overflow', 'auto');
                        },
                        close: function () {
                            $(this).remove();

                        }
                    });

    routingDetailsPopup.html('<p>' + 'Loading...' + '</p>');
    routingDetailsPopup.load(

                $.ajax({
                    url: '/GCS/ClearanceProject/RoutingDetailsOnRequestSummary?routedItemID=' + RoutedItemID,
                    type: 'POST',
                    dataType: 'html',
                    async: true,
                    data: [],
                    cache: false,
                    success: function (data) {
                        if (data == 'error') {
                            routingDetailsPopup.html('<p>' + 'Error' + '</p>');
                        }
                        else {
                            routingDetailsPopup.html(data);
                        }
                    },
                    contentType: 'text/html; charset=utf-8'
                }));

    routingDetailsPopup.dialog('option', { title: "RoutingDetails" });
    //Open Dialog       
    routingDetailsPopup.dialog('open');
    return false;
}

function GridOnLoadResource(events, args) {
    ShowColumnsForResourceUnsubmitted();
    ShowHideRequestsTabs();
}

function GridOnLoadTrack(events, args) {
    ShowColumnsForTrackUnsubmitted();
    ShowHideRequestsTabs();
}

function ShowColumnsForResourceUnsubmitted() {
    if (_folderId_Requestor == 9) {
        var gridobj = $find("TblRequestorResourceGrid");
        gridobj.get_VisibleColumns()[0].Visible = false;
        gridobj.get_VisibleColumns()[4].Visible = false;
        gridobj.get_VisibleColumns()[5].Visible = false;
        gridobj.get_VisibleColumns()[6].Visible = false;
        gridobj.get_VisibleColumns()[7].Visible = false;
        gridobj.get_VisibleColumns()[8].Visible = false;
        gridobj.get_VisibleColumns()[9].Visible = false;
        gridobj.get_VisibleColumns()[10].Visible = false;
        gridobj.get_VisibleColumns()[11].Visible = false;
        gridobj.get_VisibleColumns()[12].Visible = false;
    }
}

function ShowColumnsForTrackUnsubmitted() {
    if (_folderId_Requestor == 9) {
        var gridobj = $find("TblRequestorTrackGrid");
        gridobj.get_VisibleColumns()[0].Visible = false;
        gridobj.get_VisibleColumns()[1].Visible = false;
        gridobj.get_VisibleColumns()[3].Visible = true;
        gridobj.get_VisibleColumns()[4].Visible = true;
        gridobj.get_VisibleColumns()[5].Visible = true;
        gridobj.get_VisibleColumns()[6].Visible = true;
        gridobj.get_VisibleColumns()[7].Visible = true;
        gridobj.get_VisibleColumns()[8].Visible = false;
        gridobj.get_VisibleColumns()[9].Visible = false;
        gridobj.get_VisibleColumns()[10].Visible = false;
        gridobj.get_VisibleColumns()[11].Visible = false;
        gridobj.get_VisibleColumns()[12].Visible = false;
        gridobj.get_VisibleColumns()[13].Visible = false;
        gridobj.get_VisibleColumns()[14].Visible = false;
        gridobj.get_VisibleColumns()[15].Visible = false;
        gridobj.get_VisibleColumns()[16].Visible = false;
        gridobj.get_VisibleColumns()[17].Visible = false;
    }
}

function ShowHideRequestsTabs() {
    var resourcedata = 0;

    if (browserName == "Firefox") {
        if ($('#TblRequestorResourceGrid_Table')[0].rows.length >= 1 && ($('#TblRequestorResourceGrid_Table')[0].rows[0].textContent != "No records to display")) {
            $('#requestorResourceListItem').show();
            $('#tabsReq-1').show();
            resourcedata = 1;
            $(function () { $("#tabsRequestor").tabs({ selected: 0 }); });
        }


        if ($('#TblRequestorTrackGrid_Table')[0].rows.length >= 1 && ($('#TblRequestorTrackGrid_Table')[0].rows[0].textContent != "No records to display")) {
            $('#requestorExistingResourceListItem').show();
            $('#tabsReq-2').show();
            if (resourcedata == 0)
                $(function () { $("#tabsRequestor").tabs({ selected: 1 }); });
        }
    }

    else {
        if ($('#TblRequestorResourceGrid_Table')[0].rows.length >= 1 && ($('#TblRequestorResourceGrid_Table')[0].rows[0].outerText != "No records to display")) {
            $('#requestorResourceListItem').show();
            $('#tabsReq-1').show();
            resourcedata = 1;
            $(function () { $("#tabsRequestor").tabs({ selected: 0 }); });
        }

        if ($('#TblRequestorTrackGrid_Table')[0].rows.length >= 1 && ($('#TblRequestorTrackGrid_Table')[0].rows[0].outerText != "No records to display")) {
            $('#requestorExistingResourceListItem').show();
            $('#tabsReq-2').show();
            if (resourcedata == 0)
                $(function () { $("#tabsRequestor").tabs({ selected: 1 }); });
        }
    }

}
//#endregion - Right Panel

//#region - Right Panel- Reviewer
function GridResizedReviewerResource(sender, args) {

    var _column = sender._gridHeaderTable.cells[args.cellIndex].id;
    var _width = args.width;
    var _ID = sender._ID;
    resizeDropDownLists(_column, _width, _ID)
}

function ResourceHistory(ResourceId) {
    var objResourceHistoryPopup = $('<div id="ResourceHistoryPopup" style="margin:0;padding:0;height:400px !important;overflow-y:auto;"></div>');
    $(objResourceHistoryPopup).dialog({
        autoOpen: true,
        width: 900,
        height: 400,
        resizable: true,
        title: 'Review History',
        modal: true,
        open: function () {
            $('#loadingDivPA').show();
        },
        close: function () {
            $(this).remove(); // ensures any form variables are reset.  
            $(this).dialog('close');
            $('#loadingDivPA').hide();
        }
    });
    var values =
        {
            "ResourceId": ResourceId
        };

    $.ajax({
        url: '/GCS/ClearanceInbox/ResourceHistory',
        type: 'POST',
        data: JSON.stringify(values),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#loadingDivPA').hide();
            $(objResourceHistoryPopup).html(data);
        },
        error: function (data) {

        }
    });

    var dialogue = $('.ui-dialog');

    dialogue.animate({
        top: "30px"
    }, 0);
}

function TestReviewer() {
    var clrProjectId = 0;
    var pageSize = 1;
    var pageNo = 1;
    var jtSorting = "test";

    $('#loadingDivPA').show();
    $.post('/GCS/ClearanceInbox/GetRightPanelReviewerDetails', { clrProjectId: clrProjectId, pageSize: pageSize, pageNo: pageNo, jtSorting: jtSorting }, function (data) {

        $('#RightPanel').html(data);
        $('#wrapperexample2').show();
        $('#loadingDivPA').hide();
    });
}

function ManageContract(ResourceId, ContractId, RoutedItemId) {
    var objManageContractPopup = $('<div id="ManageContractPopup" style="margin:0;padding:0;"></div>');
    var values =
        {
            "ResourceId": ResourceId,
            "ContractId": ContractId,
            "RoutedItemId": RoutedItemId
        };
    $(objManageContractPopup).dialog({
        autoOpen: true,
        width: 900,
        resizable: false,
        title: 'Manage Contract',
        modal: true,
        open: function () {

            $(this).load("/GCS/ClearanceInbox/ClearanceSearchContract", values);
        },
        close: function () {
            $(this).remove(); // ensures any form variables are reset.  
            $(this).dialog('close');
            $('#loadingDivPA').hide();
        }
    });
    objManageContractPopup.dialog('open');


    var dialogue = $('.ui-dialog');

    dialogue.animate({

        top: "60px"

    }, 0);

}

function ManageContractSelectedRequests(resourceIds) {
    var objManageContractPopup = $('<div id="ManageContractPopup" style="margin:0;padding:0;"></div>');

    var resourceIdCollection = "";

    for (var i = 0; i < resourceIds.length; i++) {
        if (resourceIdCollection == "")
            resourceIdCollection = resourceIds[i].Key + '|' + resourceIds[i].Value;
        else
            resourceIdCollection = resourceIdCollection + '~' + resourceIds[i].Key + '|' + resourceIds[i].Value;
    }

    var values =
        {
            "ResourceIds": resourceIdCollection
        };

    $(objManageContractPopup).dialog({
        autoOpen: true,
        width: 900,
        resizable: false,
        title: 'Manage Contract',
        modal: true,
        open: function () {

            $(this).load("/GCS/ClearanceInbox/ClearanceSearchContractSelectedRequests", values);
        },
        close: function () {
            $(this).remove(); // ensures any form variables are reset.  
            $(this).dialog('close');
            $('#loadingDivPA').hide();
        }
    });
    objManageContractPopup.dialog('open');


    var dialogue = $('.ui-dialog');

    dialogue.animate({

        top: "60px"

    }, 0);

}

function GridBeginResource(sender, args) {

    $("#TblReviewerResourceGrid .manualPagerLabel:eq(1)").empty();
    $("#TblReviewerResourceGrid .manualPagerLabel:eq(1)").text("Show item per page");
    var gridType = 1;
    args.data["clearanceInboxRequestAction.WorkgroupId"] = _workGroupId_Reviewer;
    args.data["clearanceInboxRequestAction.FolderId"] = _folderId_Reviewer;
    args.data["clearanceInboxRequestAction.ProjectId"] = _clrProjectId_Reviewer;
    args.data["clearanceInboxRequestAction.RoleName"] = _roleName;
    args.data["gridType"] = gridType;
    args.data["rolegroup"] = RoleGroup.Reviewer;
}

function GridBeginTrack(sender, args) {

    $("#TblReviewerTrackGrid .manualPagerLabel:eq(1)").empty();
    $("#TblReviewerTrackGrid .manualPagerLabel:eq(1)").text("Show item per page");
    var gridType = 2;
    args.data["clearanceInboxRequestAction.WorkgroupId"] = _workGroupId_Reviewer;
    args.data["clearanceInboxRequestAction.FolderId"] = _folderId_Reviewer;
    args.data["clearanceInboxRequestAction.ProjectId"] = _clrProjectId_Reviewer;
    args.data["clearanceInboxRequestAction.RoleName"] = _roleName;
    args.data["gridType"] = gridType;
    args.data["rolegroup"] = RoleGroup.Reviewer;
}

function GridOnLoadReviewerTracks(events, args) {
    ShowHideReviewerTabs();
}

function GridOnLoadReviewerResource(events, args) {
    ShowHideReviewerTabs();
}

function ShowHideReviewerTabs() {
    var reviewerdata = 0;
    if (browserName == "Firefox") {
        if ($('#TblReviewerResourceGrid_Table')[0].rows.length >= 1 && ($('#TblReviewerResourceGrid_Table')[0].rows[0].textContent != "No records to display")) {
            $('#reviewerResourceListItem').show();
            $('#Reviewer_ResourceGrid').show();
            reviewerdata = 1;
            var comments = $('#TblReviewerResourceGrid_Table td')[13].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            $(function () { $("#tabsReviewer").tabs({ selected: 0 }); });
        }
        if ($('#TblReviewerTrackGrid_Table')[0].rows.length >= 1 && ($('#TblReviewerTrackGrid_Table')[0].rows[0].textContent != "No records to display")) {
            $('#reviewerExistingResourceListItem').show();
            $('#wrapperexample2').show();
            var comments = $('#TblReviewerTrackGrid_Table td')[13].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            if (reviewerdata == 0)
                $(function () { $("#tabsReviewer").tabs({ selected: 1 }); });
        }
    }

    else {
        if ($('#TblReviewerResourceGrid_Table')[0].rows.length >= 1 && ($('#TblReviewerResourceGrid_Table')[0].rows[0].outerText != "No records to display")) {
            $('#reviewerResourceListItem').show();
            $('#Reviewer_ResourceGrid').show();
            reviewerdata = 1;
            var comments = $('#TblReviewerResourceGrid_Table td')[13].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            $(function () { $("#tabsReviewer").tabs({ selected: 0 }); });
        }
        if ($('#TblReviewerTrackGrid_Table')[0].rows.length >= 1 && ($('#TblReviewerTrackGrid_Table')[0].rows[0].outerText != "No records to display")) {
            $('#reviewerExistingResourceListItem').show();
            $('#wrapperexample2').show();
            var comments = $('#TblReviewerTrackGrid_Table td')[13].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            if (reviewerdata == 0)
                $(function () { $("#tabsReviewer").tabs({ selected: 1 }); });
        }
    }
}

function ActionSuccess(sender, args) {
    setSyncGridToolTip("#TblReviewerResourceGrid_Table");
    SyncfusionGridScroll();
}

function RecordSelection(sender, args) {
    if (sender._selectionManager._event.target.count > 1) {
        if (sender._selectionManager._event.target[0].nodeName != null) {
            if (sender._selectionManager._event.target[0].nodeName == "OPTION") {
                return false;
            }
        }
    }
}

function SyncfusionGridScroll() {
    var pagerHgt = $(".GridPager").height();
    var headerHgt = $(".GridHeader").height();
    var browsHgt = 0;
    if ($.browser.msie)
        browsHgt = 16;
    else
        browsHgt = 13;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;

    var JtableTop = $("#TblReviewerResourceGrid").offset().top;
    var TopfootPos = $(".footer").offset().top;
    var TotRecHeight = $("#TblReviewerResourceGrid_Table").height() + reduceHgt;
    var TableHeight = TopfootPos - JtableTop;
    var gridObj = $find("TblReviewerResourceGrid");
    if (TotRecHeight >= TableHeight)
        setSyncfusionGridHeight(gridObj, TableHeight - reduceHgt);
    else
        setSyncfusionGridHeight(gridObj, TotRecHeight - reduceHgt + 20);
}

function setSyncfusionGridHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();

}

function oncheckboxCheck(checkbox, WorkGroupId, projectId, GridType, InboxType) {
    $("#WorkGrpId").val(WorkGroupId);
    $("#hdnProjectRefNo").val(projectId);

    if (!checkbox.checked) {
        if (InboxType == "Reviewer") {
            if (GridType == "TrackGrid") {
                $("#ChkSelectAllTrack").attr('checked', false);
            }
            else {
                $("#ChkSelectAllResource").attr('checked', false);
            }
            $(checkbox).removeClass("IsChanged");
        }
        else {
            if (GridType == "TrackGrid") {
                $("#ChkSelectAllTrack").attr('checked', false);
            }
            else {
                $("#ChkSelectAllResource").attr('checked', false);
            }
        }
    }
    else {
        var Grid;
        var GridTable;
        var GridPager;
        var GridTableChkbox;
        var GridSelectAllChkbox;
        if (InboxType == "Reviewer") {
            $(checkbox).addClass("IsChanged");
            if (GridType == "TrackGrid") {
                Grid = "#TblReviewerTrackGrid .MsgBar";
                GridPager = "#TblReviewerTrackGrid  .TblReviewerTrackGrid_pageSizeCustomizer";
                GridTable = "#TblReviewerTrackGrid_Table";
                GridTableChkbox = "#TblReviewerTrackGrid_Table  input[type=checkbox]";
                GridSelectAllChkbox = "#ChkSelectAllTrack";
            }
            else {
                Grid = "#TblReviewerResourceGrid .MsgBar";
                GridPager = "#TblReviewerResourceGrid  .TblReviewerResourceGrid_pageSizeCustomizer";
                GridTable = "#TblReviewerResourceGrid_Table";
                GridTableChkbox = "#TblReviewerResourceGrid_Table  input[type=checkbox]";
                GridSelectAllChkbox = "#ChkSelectAllResource";
            }
        }
        else {
            if (GridType == "TrackGrid") {
                Grid = "#TblRCCAdminTrackGrid .MsgBar";
                GridPager = "#TblRCCAdminTrackGrid  .TblRCCAdminTrackGrid_pageSizeCustomizer";
                GridTable = "#TblRCCAdminTrackGrid_Table";
                GridTableChkbox = "#TblRCCAdminTrackGrid_Table  input[type=checkbox]";
                GridSelectAllChkbox = "#ChkSelectAllTrack";
            }
            else {
                Grid = "#TblRCCAdminResourceGrid .MsgBar";
                GridPager = "#TblRCCAdminResourceGrid  .TblRCCAdminResourceGrid_pageSizeCustomizer";
                GridTable = "#TblRCCAdminResourceGrid_Table";
                GridTableChkbox = "#TblRCCAdminResourceGrid_Table  input[type=checkbox]";
                GridSelectAllChkbox = "#ChkSelectAllResource";
            }
        }
        if (!$(GridSelectAllChkbox)[0].checked) {

            var pageindex = $('.NumericItem.Spacing.CurrentItem').text();
            var CurrentPageSize = parseInt($(GridPager)[0].children[1].value);
            if ($(GridTable)[0].rows.length > CurrentPageSize) {
                for (var i = 0; i < CurrentPageSize; i++) {
                    if (!($(GridTableChkbox)[i].checked)) {
                        return;
                    }
                }
                $(GridSelectAllChkbox).attr('checked', true);
            }
            else {
                for (var i = 0; i < $(GridTable)[0].rows.length; i++) {
                    if (!($(GridTableChkbox)[i].checked)) {
                        return;
                    }
                }
                $(GridSelectAllChkbox).attr('checked', true);
            }
        }
    }
}

function actionOnSelectedItem(actionId, inboxType) {
    var selectAllAcrossPagesChecked = false;
    var selectedRequests = [];
    var routedItemRequest = [];
    var resourceIds = [];
    var comment;
    var toWorkgroupId;
    var WorkGroupId;
    var reviewCommentAvailableOnAllRequests = true;
    var gridRows;
    var td;
    var ModifiedDateRequest;
    var ModifiedDateRoutedItem;
    var gridIdSelected;
    var gridTR;

    if (inboxType == "Reviewer") {
        selectAllAcrossPagesChecked = $("#SelectAllAcrossPagesReviewer").is(':checked');
        clrProjectId = _clrProjectId_Reviewer;
        workGroupId = _workGroupId_Reviewer;
        folderId = _folderId_Reviewer;
        roleGroup = RoleGroup.Reviewer;
        var index = $("#tabsReviewer").tabs('option', 'selected');
        if (index == 0) {
            gridRows = $("#TblReviewerResourceGrid_Table  input[type=checkbox]");
            td = "#TblReviewerResourceGrid_Table td";
            gridIdSelected = 1;
            gridTR = '#TblReviewerResourceGrid_Table TR';
        }
        else {
            gridRows = $("#TblReviewerTrackGrid_Table  input[type=checkbox]");
            td = "#TblReviewerTrackGrid_Table td";
            gridIdSelected = 2;
            gridTR = '#TblReviewerTrackGrid_Table TR';
        }
    }
    else {
        clrProjectId = _clrProjectId_RCCAdmin;
        workGroupId = _workGroupId_RCCAdmin;
        folderId = _folderId_RCCAdmin;
        roleGroup = RoleGroup.RCCAdmin;
        var index = $("#tabsRCCAdmin").tabs('option', 'selected');
        if (index == 0) {
            gridRows = $("#TblRCCAdminResourceGrid_Table  input[type=checkbox]");
            td = "#TblRCCAdminResourceGrid_Table td";
            gridIdSelected = 1;
            gridTR = '#TblRCCAdminResourceGrid_Table TR';
        }
        else {
            gridRows = $("#TblRCCAdminTrackGrid_Table  input[type=checkbox]");
            td = "#TblRCCAdminTrackGrid_Table td";
            gridIdSelected = 2;
            gridTR = '#TblRCCAdminTrackGrid_Table TR';
        }
    }


    if (!selectAllAcrossPagesChecked) { //For normal actions

        $.each(gridRows, function (index, element) {

            var crntVisibility = gridRows[index].className;
            routedItemRequest = [];
            if (crntVisibility != "hidden") {
                if (element.checked) {
                    var reviewComment;

                    reviewComment = $(gridTR).eq(index).find('.textareaclass')[0].value;

                    if (reviewComment.toString().trim() == '') reviewCommentAvailableOnAllRequests = false;


                    var KeyRoutedKeypairManageContractModifiedDates = element.value.toString().split('^');
                    ModifiedDateRequest = KeyRoutedKeypairManageContractModifiedDates[1];
                    ModifiedDateRoutedItem = KeyRoutedKeypairManageContractModifiedDates[2];

                    var KeyRoutedKeypairManageContract = KeyRoutedKeypairManageContractModifiedDates[0].toString().split('~');
                    var KeyRoutedKeypair = KeyRoutedKeypairManageContract[0].split('|');
                    var resourceId = KeyRoutedKeypairManageContract[1];


                    if (KeyRoutedKeypair != null) {
                        for (var i = 0; i < KeyRoutedKeypair.length; i++) {

                            var KeyRoutedKey_val = KeyRoutedKeypair[i].toString().split(',');
                            var RoutedItemId = KeyRoutedKey_val[0];
                            var RequestId = KeyRoutedKey_val[1];
                            routedItemRequest.push({ Key: RoutedItemId, Value: RequestId });
                            resourceIds.push({ Key: resourceId, Value: RoutedItemId });
                        }
                    }
                    WorkGroupId = $("#WorkGrpId").val();
                    var Action = actionId;
                    selectedRequests.push({
                        KeyRoutedItemRequest: routedItemRequest,
                        Comment: reviewComment,
                        WorkgroupId: workGroupId,
                        ModifiedDateRoutedString: ModifiedDateRoutedItem,
                        ModifiedDateRequestString: ModifiedDateRequest
                    });
                }
            }
            else {
                var reviewComment = '';
                reviewComment = $("#review_comment").text();

                if (reviewComment.toString().trim() == '') reviewCommentAvailableOnAllRequests = false;

                var KeyRoutedKeypairManageContractModifiedDates = element.value.toString().split('^');
                ModifiedDateRequest = KeyRoutedKeypairManageContractModifiedDates[1];
                ModifiedDateRoutedItem = KeyRoutedKeypairManageContractModifiedDates[2];

                var KeyRoutedKeypairManageContract = KeyRoutedKeypairManageContractModifiedDates[0].toString().split('~');

                var KeyRoutedKeypair = KeyRoutedKeypairManageContract[0].split('|');
                var resourceId = KeyRoutedKeypairManageContract[1];


                if (KeyRoutedKeypair != null) {
                    for (var i = 0; i < KeyRoutedKeypair.length; i++) {
                        var KeyRoutedKey_val = KeyRoutedKeypair[i].toString().split(',');
                        var RoutedItemId = KeyRoutedKey_val[0];
                        var RequestId = KeyRoutedKey_val[1];
                        routedItemRequest.push({ Key: RoutedItemId, Value: RequestId });
                        resourceIds.push({ Key: resourceId, Value: RoutedItemId });
                    }
                }
                WorkGroupId = $("#WorkGrpId").val();
                var Action = actionId;
                selectedRequests.push({
                    KeyRoutedItemRequest: routedItemRequest,
                    Comment: reviewComment,
                    WorkgroupId: workGroupId,
                    ModifiedDateRoutedString: ModifiedDateRoutedItem,
                    ModifiedDateRequestString: ModifiedDateRequest
                });
            }
        });
        if (selectedRequests.length == 0) {
            return;
        }
    }

    else if (actionId == "17" || actionId == "18") {
        return;
    }

    switch (actionId) {

        case "1": // Approve  
        case "5": // Artist Consent
        case "17": // Route Action RCC Admin  
        case "76": // Move To Research Folder
        case "77": // Move To Internal Review Folder
        case "78": // Move To Side Artist Sample  
            break;
        case "2": // Conditionally Approve
            if (!reviewCommentAvailableOnAllRequests || selectAllAcrossPagesChecked) {
                showPopupConditionallyApprove();
                return;
            }
            break;
        case "3": // Reject
            if (!reviewCommentAvailableOnAllRequests || selectAllAcrossPagesChecked) {
                showPopupReject();
                return;
            }
            break;
        case "4": // Dispatch             
            showPopupDispatch();
            return;
            break;
        case "6": // Route To RCC Admin    
            if (!reviewCommentAvailableOnAllRequests || selectAllAcrossPagesChecked) {
                showPopupRouteToRccAdmin();
                return;
            }
            break;
        case "18": // Manage Contract
            ManageContractSelectedRequests(resourceIds);
            return;
            break;
    }


    do_RequestedAction(gridIdSelected);


    function showPopupConditionallyApprove() {

        //--
        var popUp = $('#popup-request-conditionallyapprove').clone();

        //--
        $('#RightPane').append(popUp);

        //--
        var txtComments = popUp.find('#txt-request-conditionallyapprove-comments');
        var btnConditionallyApprove = popUp.find('#btn-request-conditionallyapprove-conditionallyapprove');
        var btnCancel = popUp.find('#btn-request-conditionallyapprove-cancel');

        //--
        btnConditionallyApprove.click(function () {

            comment = $.trim(txtComments.val());

            if (comment == '') return;

            closePopUp();

            do_RequestedAction(gridIdSelected);

        });

        btnCancel.click(function () {

            closePopUp();

        });

        //--
        popUp.dialog({
            autoOpen: false,
            height: 170,
            modal: true,
            resizable: false,
            title: lbl_Request_ConditionallyApprove,
            close: closePopUp
        });

        //--
        popUp.dialog('open');

        //--
        function closePopUp() {

            popUp.dialog('destroy');

            if (popUp != null) { popUp.remove(); popUp = null; }

        }
    }

    function showPopupReject() {

        //--
        var popUp = $('#popup-request-rejectrequest').clone();

        //--
        $('#RightPane').append(popUp);

        //--
        var ddlRejectReasons = popUp.find('#ddlRejectReasons');
        var txtComments = popUp.find('#txt-request-rejectrequest-comments');
        var btnRejectRequest = popUp.find('#btn-request-rejectrequest-reject');
        var btnCancel = popUp.find('#btn-request-rejectrequest-cancel');

        btnRejectRequest.click(function () {

            var selectedRejectReason = ddlRejectReasons.find(':selected');
            if (comment == null) {
                comment = '';
            }
            if (txtComments.val() != null && txtComments.val() != '' && selectedRejectReason.val() != '') {
                comment = selectedRejectReason.text() + ' ' + txtComments.text();
            }
            else if (txtComments.val() != null && txtComments.val() != '' && selectedRejectReason.val() == '') {
                comment = txtComments.text();
            }
            else if (txtComments.val() == null && selectedRejectReason.val() != '') {
                comment = selectedRejectReason.text();
            }
            else if (txtComments.val() == '' && selectedRejectReason.val() != '') {
                comment = selectedRejectReason.text();
            }

            if (comment == '') return;

            closePopUp();

            do_RequestedAction(gridIdSelected);

        });

        btnCancel.click(function () {

            closePopUp();

        });

        //--
        popUp.dialog({
            autoOpen: false,
            height: 250,
            width: 350,
            modal: true,
            resizable: false,
            title: lbl_Request_RejectRequest,
            close: closePopUp
        });

        //--
        popUp.dialog('open');

        //--
        function closePopUp() {

            popUp.dialog('destroy');

            if (popUp != null) { popUp.remove(); popUp = null; }

        }
    }

    function showPopupDispatch() {

        //--
        var popUp = $('#popup-request-dispatch').clone();

        //--
        $('#RightPane').append(popUp);

        //--
        var divParentChildWorkgroup = popUp.find('#div-request-dispatch-workgroups');
        var btnDispatch = popUp.find('#btn-request-dispatch-dispatch');
        var btnCancel = popUp.find('#btn-request-dispatch-cancel');

        var table = popUp.find("#inboxTable");
        var radiobutton = $(table).find("input[type=radio]")
        if (radiobutton.length == 0) {
            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p><b> No Workgroups are there to dispatch. </b></p>')
            .dialog({
                autoOpen: false,
                modal: true,
                show: 'clip',
                hide: 'clip',
                width: 250,
                height: 100
            });
            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Ok':
                     function (e) {
                         $(this).dialog('close');
                     }
                }

            });
            return;
        }

        //--
        btnDispatch.click(function () {

            toWorkgroupId = divParentChildWorkgroup.find('input:radio:checked').val();

            if (toWorkgroupId == null || toWorkgroupId == '') return;

            closePopUp();

            do_RequestedAction(gridIdSelected);

        });

        btnCancel.click(function () {

            closePopUp();

        });

        //-- 
        popUp.dialog({
            autoOpen: false,
            height: 230,
            modal: true,
            resizable: false,
            title: lbl_Request_Dispatch,
            close: closePopUp
        });

        //--
        popUp.dialog('open');

        //--
        function closePopUp() {

            popUp.dialog('destroy');

            if (popUp != null) { popUp.remove(); popUp = null; }

        }
    }

    function showPopupRouteToRccAdmin() {

        //--
        var popUp = $('#popup-request-routetorccadmin').clone();

        //--
        $('#RightPane').append(popUp);

        //--
        var txtComments = popUp.find('#txt-request-routetorccadmin-comments');
        var btnRouteToRccAdmin = popUp.find('#btn-request-routetorccadmin-routetorccadmin');
        var btnCancel = popUp.find('#btn-request-routetorccadmin-cancel');

        //--
        btnRouteToRccAdmin.click(function () {

            comment = $.trim(txtComments.val());

            if (comment == '') return;

            closePopUp();

            do_RequestedAction(gridIdSelected);

        });

        btnCancel.click(function () {

            closePopUp();

        });

        //--
        popUp.dialog({
            autoOpen: false,
            height: 170,
            modal: true,
            resizable: false,
            title: lbl_Request_RouteToRccAdmin,
            close: closePopUp
        });

        //-- 
        popUp.dialog('open');

        //--
        function closePopUp() {

            popUp.dialog('destroy');

            if (popUp != null) { popUp.remove(); popUp = null; }

        }
    }

    function do_RequestedAction(gridIdSelected) {
        var clrProjectId;
        var workGroupId;
        var folderId;
        var roleGroup;
        if (inboxType == "Reviewer") {
            clrProjectId = _clrProjectId_Reviewer;
            workGroupId = _workGroupId_Reviewer;
            folderId = _folderId_Reviewer;
            roleGroup = RoleGroup.Reviewer;
        }
        else {
            clrProjectId = _clrProjectId_RCCAdmin;
            workGroupId = _workGroupId_RCCAdmin;
            folderId = _folderId_RCCAdmin;
            roleGroup = RoleGroup.RCCAdmin;
        }
        postData = {
            clearanceInboxModel: getFilters().clearanceInboxModel,
            clearanceInboxRequestAction: {
                WorkgroupId: workGroupId,
                FolderId: folderId,
                ProjectId: clrProjectId,
                Requests: selectedRequests,
                ActionId: actionId,
                Comment: comment || null,
                ToWorkgroupId: toWorkgroupId || null
            },
            roleGroup: roleGroup,
            selectAllAcrossPages: selectAllAcrossPagesChecked,
            gridType: gridIdSelected
        };

        var activeInbox = _activeInbox;
        displayLoadingPanel(activeInbox, true);

        $.ajax({
            url: '/GCS/ClearanceInbox/PerformRequestAction',
            type: 'POST',
            data: JSON.stringify(postData),
            async: 'async',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {

                refreshLeftPanel(activeInbox, true, data);

                var postDataR = {
                    args: null,
                    clearanceInboxRequestAction: {
                        WorkgroupId: workGroupId,
                        FolderId: folderId,
                        ProjectId: clrProjectId,
                        RoleName: _roleName
                    },
                    gridType: gridIdSelected
                };
                // have to write  code selected project on lefthand side with global variables  
                (inboxType == "Reviewer") ? postDataR.roleGroup = RoleGroup.Reviewer : postDataR.roleGroup = RoleGroup.RCCAdmin;

                displayLoadingPanel(activeInbox, false);

                var exist = TestSelectRow(_activeRoleGroup, clrProjectId, activeInbox, folderId);

                if (exist) {
                    RefreshRightPanel(postDataR);
                }
                else {
                    activeInbox.find('#RightPane').empty();
                }
            },
            error: function () {
                displayLoadingPanel(activeInbox, false);
            }

        });
    }
}

function SaveReviewComments_AssignedTo(InboxType) {
    
    var context = "#reviewerHeaderContent";
    var selectedRequests = [];
    var routedItemRequest = [];
    var comment;
    var workGroupId;
    var activeInbox = _activeInbox;
    displayLoadingPanel(activeInbox, true);
    var clrProjectId = $(context).find('#hdnclrProjectId').val();
    var roleName = $(context).find('#hdnRoleName').val();
    var folderId = $(context).find('#hdnFolderId').val();
    var grid_Table_Resource;
    var grid_Td_Resource;
    var grid_Checkbox_Resource;
    var grid_Table_Track;
    var grid_Td_Track;
    var grid_Checkbox_Track;

    var selectAllAcrossPagesChecked = $("#SelectAllAcrossPagesReviewer").is(':checked');

    var selectedAssigneToChecked = false;
    var selectedAssigneToId = 0;
    var selectedAssigneToName = '';    

    var selectedCommentChecked = false;
    var commentMultipleText = '';

    if (InboxType == 'Reviewer') {
        grid_Table_Track = "#TblReviewerTrackGrid_Table";
        grid_Td_Track = "#TblReviewerTrackGrid_Table  td";
        grid_Checkbox_Track = "#TblReviewerTrackGrid_Table  input[type=checkbox]";
        grid_Table_Resource = "#TblReviewerResourceGrid_Table";
        grid_Td_Resource = "#TblReviewerResourceGrid_Table  td";
        grid_Checkbox_Resource = "#TblReviewerResourceGrid_Table  input[type=checkbox]";
        selectedAssigneToChecked = $("#chkEnableAssignedTo").is(':checked');
        selectedCommentChecked = $("#chkEnableCommentMultiple").is(':checked');
        commentMultipleText = $.trim($('#txtCommentMultiple').val());
        selectedAssigneToName = $("#assignedToMultiple :selected").text();
        selectedAssigneToId = $("#assignedToMultiple").val() != "" && $("#assignedToMultiple").val() != undefined ? $("#assignedToMultiple").val() : 0;
    }
    else {
        grid_Table_Track = "#TblRCCAdminTrackGrid_Table";
        grid_Td_Track = "#TblRCCAdminTrackGrid_Table  td";
        grid_Checkbox_Track = "#TblRCCAdminTrackGrid_Table  input[type=checkbox]";
        grid_Table_Resource = "#TblRCCAdminResourceGrid_Table";
        grid_Td_Resource = "#TblRCCAdminResourceGrid_Table  td";
        grid_Checkbox_Resource = "#TblRCCAdminResourceGrid_Table  input[type=checkbox]";
    }
    
    if ($(grid_Table_Track)[0].rows.length > 0) {
        var gridRows = $(grid_Td_Track).find('.IsChanged');
        var rowIndexStart = -1;
        $.each(gridRows, function (index, element) {

            routedItemRequest = [];

            var rowIndex = $(grid_Td_Track).find('.IsChanged')[index].parentElement.parentElement.rowIndex;

            if (rowIndex !== rowIndexStart) {

                rowIndexStart = rowIndex;
                var checkboxChecked = $(grid_Checkbox_Track)[rowIndex].checked;
                var reviewComment;
                if (InboxType == 'Reviewer' && selectedCommentChecked && checkboxChecked) {
                    reviewComment = commentMultipleText;
                }
                else {
                    if ($(grid_Td_Track).find('.textareaclass').length > 0)
                        reviewComment = $(grid_Td_Track).find('.textareaclass')[rowIndex].innerText;

                    if (reviewComment != null) {
                        if (reviewComment == '') {
                            if ($("#review_comment") != null)
                                reviewComment = $("#review_comment").text();
                            else
                                reviewComment = '';
                        }
                    }
                    else {
                        if ($("#review_comment") != null)
                            reviewComment = $("#review_comment").text();
                        else
                            reviewComment = '';
                    }
                }

                var assignedToUserId;
                var assignedToChange = false;

                if (InboxType == 'Reviewer') {
                    assignedToUserId = $(grid_Td_Track).find(".assignedToUserName .assignedToUserId")[rowIndex].value;
                    assignedToUserId !== "" ? assignedToUserId : 0;
                    assignedToChange = (assignedToUserId != selectedAssigneToId && checkboxChecked && selectedAssigneToChecked);
                }

                var newKeyRoutedKeypair = $(grid_Checkbox_Track)[rowIndex].value.toString().split('^')[0];

                if (newKeyRoutedKeypair != undefined)
                    var keyRoutedKeypair = newKeyRoutedKeypair.split('|');

                if (keyRoutedKeypair != null) {
                    for (var i = 0; i < keyRoutedKeypair.length; i++) {
                        var keyRoutedKey_val = keyRoutedKeypair[i].toString().split(',');
                        var routedItemId = keyRoutedKey_val[0];
                        var requestId = keyRoutedKey_val[1];
                        routedItemRequest.push({ Key: routedItemId, Value: requestId });
                    }
                }

                workGroupId = $(grid_Td_Track).find('.WorkgroupId')[rowIndex].value;

                var modifiedDateRequest;
                var modifiedDateRoutedItem;

                if ($(grid_Td_Track).find('.ModifiedRequestDate').length > 0)
                    modifiedDateRequest = $(grid_Td_Track).find('.ModifiedRequestDate')[rowIndex].value;

                if ($(grid_Td_Track).find('.ModifiedRoutedItemDate').length > 0)
                    modifiedDateRoutedItem = $(grid_Td_Track).find('.ModifiedRoutedItemDate')[rowIndex].value;

                if (assignedToChange) {
                    selectedRequests.push({
                        KeyRoutedItemRequest: routedItemRequest,
                        Comment: reviewComment,
                        WorkgroupId: workGroupId,
                        AssignedToUserId: selectedAssigneToId,
                        AssignedToUser: selectedAssigneToName,
                        ActionId: 82, // Assigned To Action                     
                        ModifiedDateRoutedString: modifiedDateRoutedItem,
                        ModifiedDateRequestString: modifiedDateRequest
                    });
                }
                else {
                    selectedRequests.push({
                        KeyRoutedItemRequest: routedItemRequest,
                        Comment: reviewComment,
                        WorkgroupId: workGroupId,
                        AssignedToUserId: assignedToUserId,
                        ActionId: 0,
                        ModifiedDateRoutedString: modifiedDateRoutedItem,
                        ModifiedDateRequestString: modifiedDateRequest
                    });
                }
            }
        });
    }
    
    if ($(grid_Table_Resource)[0].rows.length > 0) {
        var gridRows = $(grid_Td_Resource).find('.IsChanged');
        var rowIndexStart = -1;
        $.each(gridRows, function (index, element) {

            routedItemRequest = [];

            var rowIndex = $(grid_Td_Resource).find('.IsChanged')[index].parentElement.parentElement.rowIndex;

            if (rowIndex !== rowIndexStart) {

                rowIndexStart = rowIndex;
                var checkboxChecked = $(grid_Checkbox_Resource)[rowIndex].checked;
                
                var reviewComment;
                if (InboxType == 'Reviewer' && selectedCommentChecked && checkboxChecked) {
                    reviewComment = commentMultipleText;
                }
                else {

                    if ($(grid_Td_Resource).find('.textareaclass').length > 0)
                        reviewComment = $(grid_Td_Resource).find('.textareaclass')[rowIndex].innerText;

                    if (reviewComment == '') {
                        if ($("#review_comment") != null)
                            reviewComment = $("#review_comment").text();
                    }
                }

                var assignedToUserId;
                var assignedToChange = false;

                if (InboxType == 'Reviewer') {
                    assignedToUserId = $(grid_Td_Resource).find(".assignedToUserName .assignedToUserId")[rowIndex].value;
                    assignedToUserId !== "" ? assignedToUserId : 0;
                    assignedToChange = (assignedToUserId != selectedAssigneToId && checkboxChecked && selectedAssigneToChecked);
                }

                var newKeyRoutedKeypair = $(grid_Checkbox_Resource)[rowIndex].value.toString().split('^')[0];
                if (newKeyRoutedKeypair != undefined)
                    var keyRoutedKeypair = newKeyRoutedKeypair.split('|');

                if (keyRoutedKeypair != null) {
                    for (var i = 0; i < keyRoutedKeypair.length; i++) {
                        var keyRoutedKey_val = keyRoutedKeypair[i].toString().split(',');
                        var routedItemId = keyRoutedKey_val[0];
                        var requestId = keyRoutedKey_val[1];
                        routedItemRequest.push({ Key: routedItemId, Value: requestId });
                    }
                }

                workGroupId = $(grid_Td_Resource).find('.WorkgroupId')[rowIndex].value;

                var modifiedDateRequest;

                if ($(grid_Td_Resource).find('.ModifiedRequestDate').length > 0)
                    modifiedDateRequest = $(grid_Td_Resource).find('.ModifiedRequestDate')[rowIndex].value;

                var modifiedDateRoutedItem;

                if ($(grid_Td_Resource).find('.ModifiedRoutedItemDate').length > 0)
                    modifiedDateRoutedItem = $(grid_Td_Resource).find('.ModifiedRoutedItemDate')[rowIndex].value;

                if (assignedToChange) {
                    selectedRequests.push({
                        KeyRoutedItemRequest: routedItemRequest,
                        Comment: reviewComment,
                        WorkgroupId: workGroupId,
                        AssignedToUserId: selectedAssigneToId,
                        AssignedToUser: selectedAssigneToName,
                        ActionId: 82, // Assigned To Action                     
                        ModifiedDateRoutedString: modifiedDateRoutedItem,
                        ModifiedDateRequestString: modifiedDateRequest
                    });
                }
                else {
                    selectedRequests.push({
                        KeyRoutedItemRequest: routedItemRequest,
                        Comment: reviewComment,
                        WorkgroupId: workGroupId,
                        AssignedToUserId: assignedToUserId,
                        ActionId: 0,
                        ModifiedDateRoutedString: modifiedDateRoutedItem,
                        ModifiedDateRequestString: modifiedDateRequest
                    });
                }

            }
        });
    }

    if (selectedRequests.length == 0) {
        if (InboxType == 'Reviewer') {
            var index = $("#tabsReviewer").tabs('option', 'selected');
            if (index == 0) {
                gridRows = $("#TblReviewerResourceGrid_Table  input[type=checkbox]");
                td = "#TblReviewerResourceGrid_Table td";
            }
            else {
                gridRows = $("#TblReviewerTrackGrid_Table  input[type=checkbox]");
                td = "#TblReviewerTrackGrid_Table td";
            }
        }
        else {
            var index = $("#tabsRCCAdmin").tabs('option', 'selected');
            if (index == 0) {
                gridRows = $("#TblRCCAdminResourceGrid_Table  input[type=checkbox]");
                td = "#TblRCCAdminResourceGrid_Table td";

            }
            else {
                gridRows = $("#TblRCCAdminTrackGrid_Table  input[type=checkbox]");
                td = "#TblRCCAdminTrackGrid_Table td";
            }
        }

        if ($("#review_comment").length > 0) {
            $.each(gridRows, function (index, element) {
                routedItemRequest = [];
                var reviewComment = '';
                reviewComment = $("#review_comment").text();

                if ($(td).find('.ModifiedRequestDate').length > 0)
                    modifiedDateRequest = $(td).find('.ModifiedRequestDate')[index].value;

                if ($(td).find('.ModifiedRoutedItemDate').length > 0)
                    modifiedDateRoutedItem = $(td).find('.ModifiedRoutedItemDate')[index].value;

                var newKeyRoutedKeypair = element.value.toString().split('^')[0];

                if (newKeyRoutedKeypair != undefined)
                    var keyRoutedKeypair = newKeyRoutedKeypair.split('|');

                if (keyRoutedKeypair != null) {
                    for (var i = 0; i < keyRoutedKeypair.length; i++) {
                        var keyRoutedKey_val = keyRoutedKeypair[i].toString().split(',');
                        var routedItemId = keyRoutedKey_val[0];
                        var requestId = keyRoutedKey_val[1];
                        routedItemRequest.push({ Key: routedItemId, Value: requestId });
                    }
                }
                workGroupId = $("#WorkGrpId").val();
                selectedRequests.push({
                    KeyRoutedItemRequest: routedItemRequest,
                    Comment: reviewComment,
                    WorkgroupId: workGroupId,
                    ModifiedDateRoutedString: modifiedDateRoutedItem,
                    ModifiedDateRequestString: modifiedDateRequest
                });
            });

        }
        else if (!selectAllAcrossPagesChecked) {
            displayLoadingPanel(activeInbox, false);
            return;
        }
    }

    if (InboxType == 'Reviewer')
        workGroupId = _workGroupId_Reviewer;

    var gridType = $("#tabsReviewer").tabs('option', 'selected') + 1;
    
    var postData = {
        args: null,
        roleGroup: RoleGroup.Reviewer,
        clearanceInboxRequestAction: {
            WorkgroupId: workGroupId,
            FolderId: folderId,
            ProjectId: clrProjectId,
            Requests: selectedRequests,
            RoleName: roleName
        },
        gridType: gridType,
        isSelectedAllAcrossPages: selectAllAcrossPagesChecked,
        userId: selectedAssigneToId,
        userName: selectedAssigneToName,
        isAssignedToEnabled: selectedAssigneToChecked,
        isCommentMultipleEnabled: selectedCommentChecked,
        commentMultiple: commentMultipleText
    };

    var url = '/GCS/ClearanceInbox/UpdateRequestAssignedTo_ReviewComment'

    var loadingPopUpParameter = $($.find('#loadingDv'));

    var parameters = {
        loadingPopUp: loadingPopUpParameter,
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: true
    };

    function success(data) {
        if (data.result.Result == 'OK') {
            RefreshRightPanel(postData);
        }
    };

    GCS.ajax.functions.ajaxRequest(url, parameters, success);
}

function isValueChangedCss(ctrl) {
    if (ctrl[0].className != '') {
        var className = ctrl[0].className.toString();
        ctrl[0].className = className + " IsChanged";
    }
    else {
        ctrl[0].className = "IsChanged";
    }
}

function SelectAllCheckboxes(checkbox, GridType, inboxType) {
    var Grid;
    var GridTable;
    var GridPager;
    var GridTableChkbox;

    if (inboxType == "Reviewer") {
        if (GridType == "TrackGrid") {
            Grid = "#TblReviewerTrackGrid .MsgBar";
            GridPager = "#TblReviewerTrackGrid  .TblReviewerTrackGrid_pageSizeCustomizer";
            GridTable = "#TblReviewerTrackGrid_Table";
            GridTableChkbox = "#TblReviewerTrackGrid_Table  input[type=checkbox]";
        }
        else {
            Grid = "#TblReviewerResourceGrid .MsgBar";
            GridPager = "#TblReviewerResourceGrid  .TblReviewerResourceGrid_pageSizeCustomizer";
            GridTable = "#TblReviewerResourceGrid_Table";
            GridTableChkbox = "#TblReviewerResourceGrid_Table  input[type=checkbox]";
        }

        if (checkbox[0].checked)
            $(GridTableChkbox).addClass("IsChanged");
        else
            $(GridTableChkbox).removeClass("IsChanged");

    }
    else {
        if (GridType == "TrackGrid") {
            Grid = "#TblRCCAdminTrackGrid .MsgBar";
            GridPager = "#TblRCCAdminTrackGrid  .TblRCCAdminTrackGrid_pageSizeCustomizer";
            GridTable = "#TblRCCAdminTrackGrid_Table";
            GridTableChkbox = "#TblRCCAdminTrackGrid_Table  input[type=checkbox]";
        }
        else {
            Grid = "#TblRCCAdminResourceGrid .MsgBar";
            GridPager = "#TblRCCAdminResourceGrid  .TblRCCAdminResourceGrid_pageSizeCustomizer";
            GridTable = "#TblRCCAdminResourceGrid_Table";
            GridTableChkbox = "#TblRCCAdminResourceGrid_Table  input[type=checkbox]";
        }

    }
    var pageindex = $('.NumericItem.Spacing.CurrentItem').text();

    var CurrentPageSize = parseInt($(GridPager)[0].children[1].value);
    if ($(GridTable)[0].rows.length > CurrentPageSize) {
        for (var i = 0; i < CurrentPageSize; i++) {
            if (checkbox[0].checked) {
                $(GridTableChkbox)[i].checked = true;
            }
            else {
                $(GridTableChkbox)[i].checked = false;
            }
        }
    }
    else {
        for (var i = 0; i < $(GridTable)[0].rows.length; i++) {
            if (checkbox[0].checked) {
                $(GridTableChkbox)[i].checked = true;
            }
            else {
                $(GridTableChkbox)[i].checked = false;
            }
        }
    }
}

//RCC Admin Right hand panel
function GridBeginRCCAdminResource(sender, args) {;
    $("#TblRCCAdminResourceGrid .manualPagerLabel:eq(1)").empty();
    $("#TblRCCAdminResourceGrid .manualPagerLabel:eq(1)").text("Show item per page");
    var gridType = 1;
    args.data["clearanceInboxRequestAction.WorkgroupId"] = _workGroupId_RCCAdmin;
    args.data["clearanceInboxRequestAction.FolderId"] = _folderId_RCCAdmin;
    args.data["clearanceInboxRequestAction.ProjectId"] = _clrProjectId_RCCAdmin;
    args.data["clearanceInboxRequestAction.RoleName"] = _roleName;
    args.data["gridType"] = gridType;
    args.data["rolegroup"] = RoleGroup.RCCAdmin;
}

function GridBeginRCCAdminTrack(sender, args) {
    $("#TblRCCAdminTrackGrid .manualPagerLabel:eq(1)").empty();
    $("#TblRCCAdminTrackGrid .manualPagerLabel:eq(1)").text("Show item per page");
    var gridType = 2;
    args.data["clearanceInboxRequestAction.WorkgroupId"] = _workGroupId_RCCAdmin;
    args.data["clearanceInboxRequestAction.FolderId"] = _folderId_RCCAdmin;
    args.data["clearanceInboxRequestAction.ProjectId"] = _clrProjectId_RCCAdmin;
    args.data["clearanceInboxRequestAction.RoleName"] = _roleName;
    args.data["gridType"] = gridType;
    args.data["rolegroup"] = RoleGroup.RCCAdmin;
}

function GridOnLoadRCCAdminResource(events, args) {
    ShowHideRCCAdminTabs();
}

function GridOnLoadRCCAdminTracks(events, args) {
    ShowHideRCCAdminTabs();
}

function SaveRCCHandler() {
    var activeInbox = _activeInbox;
    var postData = {
        "ProjectId": _clrProjectId_RCCAdmin,
        "User_Id": $("#rcc_handler").val()
    };
    $.ajax({
        url: '/GCS/ClearanceInbox/SaveRCCHandler',
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: 'json',
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.Result == 'OK') {

                displayLoadingPanel(activeInbox, false);
            }
            else {
                displayLoadingPanel(activeInbox, false);
            }
        },
        error: function () {
            displayLoadingPanel(activeInbox, false);
        }
    });

}

function ShowHideRCCAdminTabs() {
    var rccadmindata = 0;
    if (browserName == "Microsoft Internet Explorer") {
        if ($('#TblRCCAdminResourceGrid_Table')[0].rows.length >= 1 && ($('#TblRCCAdminResourceGrid_Table')[0].rows[0].outerText != "No records to display")) {
            $('#RCCResourceListItem').show();
            $('#RCCAdmin_ResourceGrid').show();
            rccadmindata = 1;
            var comments = $('#TblRCCAdminResourceGrid_Table td')[10].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            $(function () { $("#tabsRCCAdmin").tabs({ selected: 0 }); });
        }
        if ($('#TblRCCAdminTrackGrid_Table')[0].rows.length >= 1 && ($('#TblRCCAdminTrackGrid_Table')[0].rows[0].outerText != "No records to display")) {
            $('#RCCExistingResourceListItem').show();
            $('#RCCAdmin_ExistingResources').show();
            var comments = $('#TblRCCAdminTrackGrid_Table td')[10].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            if (rccadmindata == 0)
                $(function () { $("#tabsRCCAdmin").tabs({ selected: 1 }); });
        }
    }
    else {
        if ($('#TblRCCAdminResourceGrid_Table')[0].rows.length >= 1 && ($('#TblRCCAdminResourceGrid_Table')[0].rows[0].textContent != "No records to display")) {
            $('#RCCResourceListItem').show();
            $('#RCCAdmin_ResourceGrid').show();
            rccadmindata = 1;
            var comments = $('#TblRCCAdminResourceGrid_Table td')[10].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            $(function () { $("#tabsRCCAdmin").tabs({ selected: 0 }); });
        }
        if ($('#TblRCCAdminTrackGrid_Table')[0].rows.length >= 1 && ($('#TblRCCAdminTrackGrid_Table')[0].rows[0].textContent != "No records to display")) {
            $('#RCCExistingResourceListItem').show();
            $('#RCCAdmin_ExistingResources').show();
            var comments = $('#TblRCCAdminTrackGrid_Table td')[10].outerText;
            if (_roleName == "UMGI Marketing Reviewer") {
                $("#review_comment").text(comments);
            }
            if (rccadmindata == 0)
                $(function () { $("#tabsRCCAdmin").tabs({ selected: 1 }); });
        }
    }
}

//#endregion - Right Panel- Reviewer

function getResourcesSelected() {

    var activeRoleGroup = _activeRoleGroup;

    if (activeRoleGroup == 'Requestor')
        return null;

    var resourcesId = [];
    var grid_Table_Resource;
    var grid_Td_Resource;

    if (activeRoleGroup == 'Reviewer') {
        grid_Table_Resource = "#TblReviewerResourceGrid_Table";
        grid_Td_Resource = "#TblReviewerResourceGrid_Table  td";
    }
    else {
        grid_Table_Resource = "#TblRCCAdminResourceGrid_Table";
        grid_Td_Resource = "#TblRCCAdminResourceGrid_Table  td";
    }

    if ($(grid_Table_Resource)[0].rows.length > 0) {

        var gridRows = $(grid_Td_Resource).find(':input:checked');
        var rowIndexStart = -1;

        $.each(gridRows, function (index, element) {
            var rowIndex = $(grid_Td_Resource).find(':input:checked')[index].parentElement.parentElement.rowIndex;
            if (rowIndex !== rowIndexStart) {
                rowIndexStart = rowIndex;
                var resourceId = $(grid_Td_Resource).find('.resourceId')[rowIndex].value;
                resourcesId.push(resourceId);
            }
        });
    }

    return resourcesId;
}

function generateEmail(emailType) {

    var projectId = 0;
    var resourcesId = [];
    var activeInbox = _activeInbox;
    var activeRoleGroup = _activeRoleGroup;
    var selectAllAcrossPagesChecked = false;
    var folderId = 0;
    var gridType = 0;
    var workGroupId = 0;

    displayMessage(activeInbox, false);

    if (activeRoleGroup == RoleGroup.Requestor)
        projectId = _clrProjectId_Requestor;

    if (activeRoleGroup == RoleGroup.Reviewer) {
        projectId = _clrProjectId_Reviewer;
        selectAllAcrossPagesChecked = $("#SelectAllAcrossPagesReviewer").is(':checked');
        folderId = $('#hdnFolderId').val();
        gridType = $("#tabsReviewer").tabs('option', 'selected') + 1;
        workGroupId = _workGroupId_Reviewer;
    }

    if (activeRoleGroup == RoleGroup.RCCAdmin)
        projectId = _clrProjectId_RCCAdmin;

    if ((emailType === EmailTypeEnum.MasterArtistManagement || emailType === EmailTypeEnum.RegularNonTraditionalArtistManagement)
        && !selectAllAcrossPagesChecked)
        resourcesId = getResourcesSelected();

    if (projectId != 0) {

        var url = '/GCS/ClearanceInbox/GenerateEmail';

        var values = {
            emailType: emailType,
            clrProjectId: projectId,
            resourcesId: resourcesId,
            isSelectedAllAcrossPages: selectAllAcrossPagesChecked,
            gridType: gridType,
            roleGroup: activeRoleGroup,
            clearanceInboxRequestAction: {
                WorkgroupId: workGroupId,
                FolderId: folderId
            },
        };

        var parameters = {
            type: 'POST',
            data: JSON.stringify(values),
            contentType: 'application/json; charset=utf-8',
            async: true
        };

        function success(data) {
            if (data.result != "")
                displayMessage(activeInbox, true, MessageType.Error, data.result);
            else
                displayMessage(activeInbox, true, MessageType.success, 'Mail has been triggered.');
        };

        function error(data) {
            displayMessage(activeInbox, true, MessageType.Error, msg_ErrorFetchingDataFromServer);
        };

        GCS.ajax.functions.ajaxRequest(url, parameters, success, error);

    }

    return false;

}

function RefreshEmailPanel(isRightPanel, projectStatus, projectType) {

    //hide all the item of email's menu and button
    $("#ulMailList li").hide();
    $("#btnEmail").hide();

    if (_activeRoleGroup == RoleGroup.Requestor && isRightPanel == "1" && projectStatus != 1) {
        $("#lnkEmailGeneral").show();
        $("#btnEmail").show();
        if (projectType == 2) {
            $("#lnkUPCAllocation").show();
            $("#lnkTvRadio").show();
            $("#lnkRegularNon").show();
            $("#lnkRegularNonWithReviewStatus").show();
            $("#lnkEmailRegularArtistManagement").show();
        }
        else {
            $("#lnkEmailMaster").show();
            $("#lnkEmailMasterWithReviewStatus").show();
            $("#lnkEmailArtistManagmentMaster").show();
        }
    }
    if (_activeRoleGroup == RoleGroup.Reviewer && isRightPanel == "1" && projectStatus != 1) {
        $("#lnkEmailGeneral").show();
        $("#btnEmail").show();
        if (projectType == 2) {
            $("#lnkTvRadio").show();
            $("#lnkRegularNon").show();
            $("#lnkRegularNonWithReviewStatus").show();
            $("#lnkEmailRegularArtistManagement").show();
        }
        else {
            $("#lnkEmailMaster").show();
            $("#lnkEmailMasterWithReviewStatus").show();
            $("#lnkEmailArtistManagmentMaster").show();
        }
    }
    if (_activeRoleGroup == RoleGroup.RCCAdmin && isRightPanel == "1" && projectStatus != 1) {
        $("#lnkEmailGeneral").show();
        $("#btnEmail").show();
        if (projectType == 2) {
            $("#lnkTvRadio").show();
            $("#lnkRegularNon").show();
            $("#lnkRegularNonWithReviewStatus").show();
            $("#lnkEmailRegularArtistManagement").show();
        }
        else {
            $("#lnkEmailMaster").show();
            $("#lnkEmailMasterWithReviewStatus").show();
            $("#lnkEmailArtistManagmentMaster").show();
        }
    }
}

function UndoAction(value, inboxType, actionId) {
    var Beginslash = "\"\\/";
    var EndSlash = "\\/\"";
    var activeInbox = _activeInbox;
    var selectedRequests = [];
    var routedItemRequest = [];
    var substr = value.split('~');
    var KeyRoutedKeypair = substr[0].toString().split('|');
    var KeyRoutedKey_val = KeyRoutedKeypair.toString().split(',');
    var RoutedItemId = KeyRoutedKey_val[0];
    var RequestId = KeyRoutedKey_val[1];
    routedItemRequest.push({ Key: RoutedItemId, Value: RequestId });
    var ModifiedDatetime = Beginslash + substr[1] + EndSlash;
    var ModifiedDatetimeRequest = Beginslash + substr[2] + EndSlash;

    var clrProjectId = _clrProjectId_Reviewer;
    var workGroupId = _workGroupId_Reviewer;
    var folderId = _folderId_Reviewer;
    var roleGroup = RoleGroup.Reviewer;
    var gridIdSelected;

    if (inboxType == "Reviewer") {
        var index = $("#tabsReviewer").tabs('option', 'selected');
        if (index == 0) {
            gridIdSelected = 1;
        }
        else {
            gridIdSelected = 2;
        }
    }
    else {
        var index = $("#tabsRCCAdmin").tabs('option', 'selected');
        if (index == 0) {
            gridIdSelected = 1;
        }
        else {
            gridIdSelected = 2;
        }
    }

    selectedRequests.push({
        KeyRoutedItemRequest: routedItemRequest,
        WorkgroupId: workGroupId,
        ModifiedDateRoutedString: ModifiedDatetime,
        ModifiedDateRequestString: ModifiedDatetimeRequest
    });

    postData = {
        clearanceInboxModel: getFilters().clearanceInboxModel,
        clearanceInboxRequestAction: {
            WorkgroupId: workGroupId,
            FolderId: folderId,
            ProjectId: clrProjectId,
            Requests: selectedRequests,
            ActionId: actionId
        },
        roleGroup: roleGroup,
        selectAllAcrossPages: false,
        gridType: gridIdSelected
    };

    displayLoadingPanel(activeInbox, true);

    $.ajax({
        url: '/GCS/ClearanceInbox/PerformRequestAction',
        type: 'POST',
        data: JSON.stringify(postData),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            refreshLeftPanel(activeInbox, true, data);
            var postDataR;
            // have to write  code selected project on lefthand side with global variables  
            if (inboxType == "Reviewer") {
                postDataR = {
                    args: null,
                    roleGroup: RoleGroup.Reviewer,
                    clearanceInboxRequestAction: {
                        WorkgroupId: workGroupId,
                        FolderId: folderId,
                        ProjectId: clrProjectId,
                        RoleName: _roleName
                    },
                    gridType: gridIdSelected
                };
            }
            else {
                postDataR = {
                    args: null,
                    roleGroup: RoleGroup.RCCAdmin,
                    clearanceInboxRequestAction: {
                        WorkgroupId: workGroupId,
                        FolderId: folderId,
                        ProjectId: clrProjectId,
                        RoleName: _roleName
                    },
                    gridType: gridIdSelected
                };
            }
            displayLoadingPanel(activeInbox, false);

            var exist = TestSelectRow(_activeRoleGroup, clrProjectId, activeInbox, folderId);
            if (exist) {
                RefreshRightpanelGrid(postDataR);
            }
            else {
                activeInbox.find('#RightPane').empty();
            }
        },
        error: function (data) {
            displayLoadingPanel(activeInbox, false);
        }
    });
}

//Notification
$(document).ready(function () {
    inboxColumnSetting._init({})
});