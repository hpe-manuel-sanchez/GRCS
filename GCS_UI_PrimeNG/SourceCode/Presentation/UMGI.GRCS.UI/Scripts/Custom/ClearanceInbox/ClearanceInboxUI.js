var originalWindow = $(window);
var originalWindowHeight = originalWindow.height();
var originalWindowWidth = originalWindow.width();

$(window).resize(function () {

    var resizedWindow = $(window);
    var resizedWindowHeight = resizedWindow.height();
    var resizedWindowWidth = resizedWindow.width();

    if ((resizedWindowHeight != originalWindowHeight) || (resizedWindowWidth != originalWindowWidth)) {

        originalWindowHeight = resizedWindowHeight;
        originalWindowWidth = resizedWindowWidth;

        applyCustomTheam(_activeRoleGroup);

    }

});

$(document).ready(function () {

});

function resizeDropDownLists(columnId, widhtResized, gridId) {
    var resizedItems =
        [
            {
                gridId: "grdFolder-Reviewer-Projects",
                columnId: "AssignedToUser",
                object: ".assignedToUser",
                value: 20
            }
        ];

    for (var i = 0; i < resizedItems.length; i++) {
        if (resizedItems[i].gridId == gridId && resizedItems[i].columnId == columnId) {
            $('#' + gridId + ' ' + resizedItems[i].object).width(widhtResized - resizedItems[i].value);
        }
    }
}

function applyCustomTheam(rolGroup) {
    var windowHeight = originalWindowHeight;
    var headerHeight = 60;
    var footerHeight = 35;
    var tabstripHeight = 41;
    var messageBarHeight = 0;

    if ($('#' + rolGroup + ' #divMessage').is(":visible")) 
        var messageBarHeight = $('#' + rolGroup + ' #divMessage').height();
    
    var filterstripHeight = 27;
    var inboxHeaderHeight = 2;

    var mainContainerHeight = originalWindowHeight - headerHeight - footerHeight;

    switch (browserName) {
        case 'Microsoft Internet Explorer':
            var inboxRightWrapperPanelHeight = mainContainerHeight - messageBarHeight - tabstripHeight;
            var splitterHeight = inboxRightWrapperPanelHeight - filterstripHeight - messageBarHeight - 1;
            var inboxContentHeight = splitterHeight - inboxHeaderHeight - 23;
            break;

        case 'Firefox':
            var inboxRightWrapperPanelHeight = mainContainerHeight - tabstripHeight - messageBarHeight;
            var splitterHeight = inboxRightWrapperPanelHeight - filterstripHeight - messageBarHeight - 3;
            var inboxContentHeight = splitterHeight - inboxHeaderHeight - 23;
            break;

        case 'Safari':
            var inboxRightWrapperPanelHeight = mainContainerHeight - tabstripHeight - messageBarHeight;
            var splitterHeight = inboxRightWrapperPanelHeight - filterstripHeight - messageBarHeight - 1;
            var inboxContentHeight = splitterHeight - inboxHeaderHeight - 23;
            break;

        default:
            var inboxRightWrapperPanelHeight = mainContainerHeight - tabstripHeight - messageBarHeight + 2;
            var splitterHeight = inboxRightWrapperPanelHeight - filterstripHeight - messageBarHeight - 2;
            var inboxContentHeight = splitterHeight - inboxHeaderHeight - 23;
            break;
    }
         
    $('.mainContainer').height(mainContainerHeight);
    $('.mainContainer').css({ 'overflow': 'hidden' });
    $('#' + rolGroup + ' #inboxRightWrapperPanel').height(inboxRightWrapperPanelHeight);
    $('#' + rolGroup + ' .MySplitter').height(splitterHeight);
    $('#' + rolGroup + ' .ui-layout-resizer').height(splitterHeight);
    $('#' + rolGroup + ' .divLeftPanel.inboxHeader.inboxContent').height(inboxContentHeight);

    $("#heade1").width(originalWindowWidth - 350);
}

function resizeGridAction(sender, args) {
    var rolGroup = _activeRoleGroup;
    resizeRightHeader(rolGroup);
}

function resizeRightHeader(rolGroup) {
    switch (rolGroup) {
        case 'Reviewer':
            var width = $('#TblReviewerTrackGrid table').width() + $('#TblReviewerResourceGrid table').width();
            $('#reviewerHeaderContent').width(width + 2);
            break;
        case 'Requestor':
            var width = $('#TblRequestorTrackGrid table').width() + $('#TblRequestorResourceGrid table').width();
            $('#requestorHeaderContent').width(width + 2);
            break;
        case 'RCCAdmin':
            var width = $('#TblRCCAdminTrackGrid table').width() + $('#TblRCCAdminResourceGrid table').width();
            $('#RCCAdminRightPanelContent').width(width + 2);
            break;
    }
}

function layoutInbox(activeInbox, activeRoleGroup) {

    var offsetSplitterBar = activeInbox.find('#grdFolder-' + activeRoleGroup + '-Projects').width() - 16;

    activeInbox.find('.MySplitter').layout({
        stateManagement: { enabled: true },
        west: { size: offsetSplitterBar },
        east: { initClosed: true },
        togglerTip_open: 'Close',
        togglerTip_closed: 'Open',
        resizerTip: 'Resize',
        sliderTip: 'Slide Open'
    });

    activeInbox.find('#divLeftPanel .inboxHeader .GridHeader th:last').addClass('lastColumn');

    var inboxHeader = activeInbox.find('#divLeftPanel .inboxHeader .Syncfusion-Grid-Core .GridHeader');
    var inboxContent = activeInbox.find('.divLeftPanel');

    inboxContent.scroll(function (event) {
        var leftScrolled = this.scrollLeft;
        inboxHeader.css('margin-left', '-' + leftScrolled + 'px');
    });

    deleteEmptyRows(activeInbox);
}

function deleteEmptyRows(activeInbox) {
    var tagHtml = "#divLeftPanel .GridContent .RowCell input[name='ShowInformation']:unchecked";
    var length = activeInbox.find(tagHtml).length;
    var uniqueIDToDelete = [];
    for (var i = 0; i < length; i++) {
        uniqueID = activeInbox.find(tagHtml)[i].parentNode.parentNode.uniqueID
        activeInbox.find(tagHtml)[i].parentNode.parentNode.id = uniqueID;
        uniqueIDToDelete.push(uniqueID);
    }

    for (var i = 0; i < uniqueIDToDelete.length; i++) {
        activeInbox.find('#' + uniqueIDToDelete[i]).remove();
    }
}