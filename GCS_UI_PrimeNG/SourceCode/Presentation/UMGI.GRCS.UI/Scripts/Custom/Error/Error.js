var error = (function () {

    var init = function (settings) {
        error.settings = {
            statusNumber: GCS.utilities.functions.getParameterByName('sts'),
            statusMessage: GCS.utilities.functions.getParameterByName('msg'),
            errorMessageLabel: $('#errorMessage'),
            errorContainer: $('#errorContainer')
        }
        $.extend(error.settings, settings);

        setup();
    }

    var setup = function () {
        if (error.settings.statusNumber != null && error.settings.statusNumber != undefined &&
            error.settings.statusMessage != null && error.settings.statusMessage != undefined) {
            message = 'Sorry, an error ' + error.settings.statusNumber + ' ' + error.settings.statusMessage + ' has occured';
        }
        else {
            message = 'Sorry, an error has occured.';
        }

        error.settings.errorMessageLabel.text(message);

        $(window).bind("resize", functions.resetErrorPage);
    }

    var functions = {
        resetErrorPage: function () {
            var errorContainer = error.settings.errorContainer;
            var marginLeft = ($(window).width() - errorContainer.width()) / 2;
            errorContainer.css("marginLeft", marginLeft);
        }
    }

    return {
        init: init,
    }
})();

$(document).ready(function ($) {
    error.init({});
});