var inboxRightPanelRequestor = (function () {

    var init = function (settings) {
        inboxRightPanelRequestor.settings = {
            routingStatus: parseInt($('#requestorRoutingStatus').val()),
            routingStatusMsg: $('#routingStatusMsg'),
        }
        $.extend(inboxRightPanelRequestor.settings, settings);
    }

    var functions = {        
        showMessages: function () {            
            var routingStatusMessage = '';
            switch (inboxRightPanelRequestor.settings.routingStatus) {
                case 0:
                    if (_routingMessageWhenNotReachedRouting != undefined) {
                        routingStatusMessage = _routingMessageWhenNotReachedRouting;
                    }
                    break;
                case 1:
                    if (_routingMessageWhenReachedRouting != undefined) {
                        routingStatusMessage = _routingMessageWhenReachedRouting;
                    }
                    break;
                case 2:
                    if (_routingMessageWhenRoutingIsInProgress != undefined) {
                        routingStatusMessage = _routingMessageWhenRoutingIsInProgress;
                    }
                    break;
                case 4:
                    if (_routingMessageWhenErrorIsInRouting != undefined) {
                        routingStatusMessage = _routingMessageWhenErrorIsInRouting;
                    }
                    break;
                default:
                    break;
            }
            inboxRightPanelRequestor.settings.routingStatusMsg.text(routingStatusMessage);
        }
    }

    return {
        _init: init,
        _showMessages: functions.showMessages
    }
})();

$(document).ready(function () {
    inboxRightPanelRequestor._init({})
    inboxRightPanelRequestor._showMessages();
});