/*	-----------------------------------------------------
	Utilities GRCS 

	Version: 1.0.1
	
	December 29, 2016

	Requires:  jQuery 1.2.3+
	
------------------------------------------------------*/

var GCS = {
    ajax: (function () {

        var init = function (settings) {
            ajax.settings = {
                loadingPanelPopUp: undefined,
                isError: false,
                errorURL: "/GCS/Error.htm",
                urlLogUIError: "/GCS/Error/LogError",
                errorMessages: {
                    undefinedParameters: {
                        errorThrown: 'Undefined Parameters',
                        status: '600'
                    },
                    javaScriptException: {
                        errorThrown: 'Throw JavaScript Exception: ',
                        status: '601'
                    }
                }
            }
            $.extend(ajax.settings, settings);
        }

        var functions = {
            /**
            * ajax request          : public
            *
            * @url	                {string}                containing the URL to which the request is sent. 
            * @parameters           {object}                ajax setting parameters
            * @callBackSuccess      {functionType}          callback for success
            * @callBackError        {functionType}          callback for error
            */
            ajaxRequest: function (url, parameters, callBackSuccess, callBackError) {
                if (parameters.loadingPopUp !== undefined && parameters.loadingPopUp !== '') {
                    ajax.settings.loadingPanelPopUp = parameters.loadingPopUp;
                }
                ajax.ajaxRequest(url, parameters, callBackSuccess, callBackError);
            }
        }

        var ajax = {
            /**
            * ajax request          : private
            *
            * @url	                {string}            containing the URL to which the request is sent. 
            * @parameters           {object}            ajax setting parameters
            * @callBackSuccess      {functionType}      callback for success
            * @callBackError        {functionType}      callback for error
            */
            ajaxRequest: function (url, parameters, callBackSuccess, callBackError) {

                function internalError(status, message) {
                    utilities.displayLoadingPanel(false);

                    ajax.ajaxLogError(status, message);

                    var data = utilities.buildResult(true, message, status, null);

                    if (callBackError !== undefined) {
                        callBackError(data);
                    }
                    else {
                        utilities.redirectStatus(status, message);
                    }
                }

                try {
                    utilities.displayLoadingPanel(true);

                    if (url !== undefined && url !== null && url !== '' && parameters !== undefined && parameters !== null && parameters !== '') {
                        $.ajax(url, parameters)
                            .done(onSuccess)
                            .fail(onError)
                            .complete(onComplete);

                        function onSuccess(result) {
                            if (callBackSuccess !== undefined) {
                                var name = '';
                                var isError = result.Error === true ? true : false;
                                var message = result.Message;
                                var json = result.json;

                                var data = utilities.buildResult(isError, message, name, result, json);

                                callBackSuccess(data);
                            };
                        }

                        function onError(jqXHR, textStatus, errorThrown) {
                            var postData = {
                                parameters: parameters,
                                url: url
                            }

                            var name = jqXHR.status;
                            var message = errorThrown;

                            ajax.settings.isError = true;
                            ajax.ajaxLogError(name, message, postData);

                            if (callBackError !== undefined) {
                                var data = utilities.buildResult(isError, message, name);
                                callBackError(data);
                            }
                            else {
                                utilities.redirectStatus(jqXHR.status, errorThrown);
                            }
                        }

                        function onComplete() {
                            utilities.displayLoadingPanel(false);
                        }
                    }
                    else {
                        var message = ajax.settings.errorMessages.undefinedParameters.errorThrown;
                        var status = ajax.settings.errorMessages.undefinedParameters.status;

                        internalError(status, message);
                    }
                } catch (e) {
                    var message = ajax.settings.errorMessages.javaScriptException.errorThrown + ' ' + e.message;
                    var status = ajax.settings.errorMessages.javaScriptException.status;

                    internalError(status, message);
                }
            },
            /**
            * ajax log error    :   private
            *
            * @status	        {string}                contain the status exception description number. 
            * @exception        {string}                contain the status exception description.
            * @postData         {object}                object post data sended  
            */
            ajaxLogError: function (status, exception, postData) {

                try {
                    utilities.displayLoadingPanel(true);

                    var data = {
                        status: status,
                        exception: exception,
                        postData: JSON.stringify(postData)
                    };

                    var parameters = {
                        type: 'POST',
                        data: JSON.stringify(data),
                        contentType: 'application/json; charset=utf-8'
                    };

                    $.ajax(ajax.settings.urlLogUIError, parameters)
                            .fail(onError)
                            .complete(onComplete);

                    function onError(jqXHR, textStatus, errorThrown) {
                        utilities.redirectStatus(jqXHR.status, errorThrown);
                    }

                    function onComplete() {
                        utilities.displayLoadingPanel(false);
                    }
                } catch (e) {
                    utilities.displayLoadingPanel(false);

                    var message = ajax.settings.errorMessages.javaScriptException.errorThrown + ' ' + e.message;
                    var status = ajax.settings.errorMessages.javaScriptException.status;

                    utilities.redirectStatus(status, message);
                }
            },
        }

        var utilities = {
            /**
            * redirect status   :   private
            *
            * @status	    {string}                contain the status exception description number. 
            * @message      {string}                contain the status exception description.
            */
            redirectStatus: function (status, message) {
                var parameters = '?sts=' + status + '&msg=' + message;
                window.location.href = ajax.settings.errorURL + parameters;
            },
            /**
            * display loading panel     :   private
            *
            * @truefalse    {boolean}               define if the loading modal popup will be show. 
            */
            displayLoadingPanel: function (truefalse) {
                if (ajax.settings.loadingPanelPopUp !== undefined) {
                    var loadingPanel = $($.find(ajax.settings.loadingPanelPopUp));
                    if (truefalse) {
                        loadingPanel.show();
                    }
                    else {
                        loadingPanel.hide();
                    }
                }
            },
            /**
            * ajax log error    :   private
            *
            * @isError	    {boolean}               define has been an error occurred. 
            * @message      {string}                contain the status description message.
            * @name         {string}                contain the status number message
            * @result       {string}                result returned for the request
            * @json         {object}                json object result returned for the request
            * 
            * @return       {object}				returns the object with the summary of the request
            */
            buildResult: function (isError, message, name, result, json) {
                var result = {
                    isError: isError,
                    message: message,
                    name: name,
                    result: result,
                    json: json
                }
                return result;
            }
        }

        return {
            init: init,
            functions: functions
        }
    })(),
    utilities: (function () {

        var init = function (settings) {
        }

        var functions = {
            /**
            * get the header parameter values   :   public
            *
            * @name         {string}                parameter request name. 
            * @url          {string}                url header.
            * 
            * @return       {string}				returns the value of the parameter
            */
            getParameterByName: function (name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            },
            /**
            * get file size of an input component by index  :   public
            *
            * @component    {string}                component name of the input file. 
            * @!file        {number}                the index of the file required, by default is 0
            * 
            * @return       {number}				returns the size of the file requested in bytes
            */
            getFileSizeByIndex: function (component, file) {
                var size = 0;
                if (file === undefined) {
                    size = component.files[0].size;
                }
                else {
                    size = component.files[file].size;
                }
                return size;
            },
            /**
            * get a unique guid id                  :   public
            *
            * @return       {string}                return a unique guid id 
            */
            getUniqueGuid: function () {
                return functions.getUniqueId() + functions.getUniqueId() + '-' +
                    functions.getUniqueId() + '-' +
                    functions.getUniqueId() + '-' +
                    functions.getUniqueId() + '-' +
                    functions.getUniqueId() + functions.getUniqueId() + functions.getUniqueId();
            },
            /**
            * get a unique id                       :   public
            *
            * @return       {number}                return a unique id 
            */
            getUniqueId: function () {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            },
            /**
            * get the browser properties            :   public
            *
            * @return       {object}                   return an object with the version and name of the browser
            */
            getNavigatorProperties: function () {
                var browserName;

                if ((!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0)
                    browserName = 'Opera';
                else if (typeof InstallTrigger !== 'undefined')
                    browserName = 'Firefox';
                else if (Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0 || (function (p) { return p.toString() === "[object SafariRemoteNotification]"; })(!window['safari'] || safari.pushNotification))
                    browserName = 'Safari';
                else if (/*@cc_on!@*/false || !!document.documentMode)
                    browserName = 'Internet Explorer';
                else if (!!window.StyleMedia)
                    browserName = 'Edge';
                else if (!!window.chrome && !!window.chrome.webstore)
                    browserName = 'Chrome';
                if (((!!window.chrome && !!window.chrome.webstore) || ((!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0)) && !!window.CSS)
                    browserName = 'Blink';

                var properties = {
                    version: parseInt($.browser.version, 10),
                    name: browserName
                };
                return properties;
            },
            /**
            * get totally files size of an input component   :   public
            *
            * @inputId      {string}                component id. 
            * 
            * @return       {number}				returns the size of the file requested in bytes
            */
            getFileSize: function (inputId) {
                var navigatorProperties = functions.getNavigatorProperties();
                var fileSize = 0;

                if (!(navigatorProperties.version <= 9 && navigatorProperties.name != 'Internet Explorer')) {
                    var input = $('#' + inputId);
                    if (input[0].files.length > 0) {
                        fileSize = input[0].files[0].size;
                    }
                }
                return fileSize;
            },
            /**
            * check if the dialog is currently open   :   public
            *
            * @dialogId      {string}               component id. 
            * 
            * @return        {boolean}				returns if the popup dialog is open
            */
            isTheDialogPopupOpened: function (dialogId) {
                if ($(dialogId).hasClass("ui-dialog-content") && $(dialogId).dialog("isOpen")) {
                    return true;
                }
                return false;
            },
            /**
            * check if the dialog is currently open   :   public
            *
            * @dialogId      {string}               component id. 
            * 
            * @return        {boolean}				returns if the popup dialog is open
            */
            getSelectedTab: function (screenTabId) {
                var id = screenTabId + ' li.ui-state-active';
                var selectedTabId = $(id).attr("id");
                return selectedTabId;
            }
        }
        return {
            init: init,
            functions: functions
        }
    })()
}

$(document).ready(function () {
    GCS.ajax.init({});
    GCS.utilities.init({});
});