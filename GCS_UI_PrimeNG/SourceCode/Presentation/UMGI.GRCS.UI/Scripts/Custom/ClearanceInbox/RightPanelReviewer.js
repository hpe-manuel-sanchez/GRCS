var inboxRightPanelReviewer = (function () {

    var init = function (settings) {
        inboxRightPanelReviewer.settings = {
            chkEnableAssignedTo: $('#chkEnableAssignedTo'),
            ddlAssignToMultiple: $('#ddlAssignToMultiple'),
            chkEnableCommentMultiple: $('#chkEnableCommentMultiple'),
            txtCommentMultiple: $('#txtCommentMultiple'),
        }
        $.extend(inboxRightPanelReviewer.settings, settings);
        setup();
    }

    var setup = function () {
        inboxRightPanelReviewer.settings.chkEnableAssignedTo.bind("change", events.chkEnableAssignedToChange);
        inboxRightPanelReviewer.settings.chkEnableCommentMultiple.bind("change", events.chkEnableCommentMultipleChange);
    }

    events = {
        chkEnableAssignedToChange: function (event) {
            inboxRightPanelReviewer.settings.ddlAssignToMultiple.prop('disabled', !this.checked);
        },
        chkEnableCommentMultipleChange: function (event) {
            inboxRightPanelReviewer.settings.txtCommentMultiple.prop('disabled', !this.checked);
        }
    }
    return {
        _init: init
    }
})();

$(document).ready(function () {
    inboxRightPanelReviewer._init({})
});