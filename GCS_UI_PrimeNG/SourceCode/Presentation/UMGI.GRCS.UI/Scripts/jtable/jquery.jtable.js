﻿/* 
jTable 1.7
http://www.jtable.org
---------------------------------------------------------------------------
Copyright (C) 2011-2012 by Halil ?brahim Kalkan (http://www.halilibrahimkalkan.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
(function(c) {
    c.widget("hik.jtable", {
        options: {
            actions: {},
            fields: {},
            animationsEnabled: !0,
            defaultDateFormat: "yy-mm-dd",
            dialogShowEffect: "fade",
            dialogHideEffect: "fade",
            showCloseButton: !1,
            closeRequested: function() {},
            formCreated: function() {},
            formSubmitting: function() {},
            formClosed: function() {},
            loadingRecords: function() {},
            recordsLoaded: function() {},
            rowInserted: function() {},
            rowsRemoved: function() {},
            messages: {
                serverCommunicationError: "An error occured while communicating to the server.",
                loadingMessage: "",
                noDataAvailable: "No data available!",
                areYouSure: "Are you sure?",
                save: "Save",
                saving: "Saving",
                cancel: "Cancel",
                error: "Error",
                close: "Close",
                cannotLoadOptionsFor: "Can not load options for field {0}"
            }
        },
        _$mainContainer: null ,
        _$table: null ,
        _$tableBody: null ,
        _$tableRows: null ,
        _$bottomPanel: null ,
        _$busyDiv: null ,
        _$busyMessageDiv: null ,
        _$errorDialogDiv: null ,
        _columnList: null ,
        _fieldList: null ,
        _keyField: null ,
        _firstDataColumnOffset: 0,
        _lastPostData: null ,
        _cache: null ,
        _create: function() {
            this._normalizeFieldsOptions();
            this._initializeFields();
            this._createFieldAndColumnList();
            this._createMainContainer();
            this._createTableTitle();
            this._createTable();
            this._createBottomPanel();
            this._createBusyPanel();
            this._createErrorDialogDiv();
            this._addNoDataRow()
        },
        _normalizeFieldsOptions: function() {
            var b = this;
            c.each(b.options.fields, function(a, c) {
                b._normalizeFieldOptions(a, c)
            })
        },
        _normalizeFieldOptions: function(b, a) {
            a.listClass = a.listClass || "";
            a.inputClass = a.inputClass || ""
        },
        _initializeFields: function() {
            this._lastPostData = {};
            this._$tableRows = [];
            this._columnList = 
            [];
            this._fieldList = [];
            this._cache = []
        },
        _createFieldAndColumnList: function() {
            var b = this;
            c.each(b.options.fields, function(a, c) {
                b._fieldList.push(a);
                !0 == c.key && (b._keyField = a);
                !1 != c.list && "hidden" != c.type && b._columnList.push(a)
            })
        },
        _createMainContainer: function() {
            this._$mainContainer = c('<div class="jtable-main-container"></div>').appendTo(this.element)
        },
        _createTableTitle: function() {
            var b = this;
            if (b.options.title) {
                var a = c('<div class="jtable-title"><div class="jtable-title-text">' + b.options.title + "</div></div>").appendTo(b._$mainContainer);
                b.options.showCloseButton && c('<button class="jtable-command-button jtable-close-button" title="' + b.options.messages.close + '"><span>' + b.options.messages.close + "</span></button>").appendTo(a).click(function(a) {
                    a.preventDefault();
                    a.stopPropagation();
                    b._onCloseRequested()
                })
            }
        },
        _createTable: function() {
            this._$table = c('<table class="jtable"></table>').appendTo(this._$mainContainer);
            this._createTableHead();
            this._createTableBody()
        },
        _createTableHead: function() {
            this._addRowToTableHead(c("<thead></thead>").appendTo(this._$table))
        },
        _addRowToTableHead: function(b) {
            this._addColumnsToHeaderRow(c("<tr></tr>").appendTo(b))
        },
        _addColumnsToHeaderRow: function(b) {
            for (var a = 0; a < this._columnList.length; a++) {
                var c = this._columnList[a];
                this._createHeaderCellForField(c, this.options.fields[c]).data("fieldName", c).appendTo(b)
            }
        },
        _createHeaderCellForField: function(b, a) {
            a.width = a.width;
            return c('<th class="jtable-column-header" style="width:' + a.width + '"><div class="jtable-column-header-container"><span class="jtable-column-header-text">' + 
            a.title + "</span></div></th>").data("fieldName", b)
        },
        _createEmptyCommandHeader: function() {
            return c('<th class="jtable-command-column-header" style="width:1%"></th>')
        },
        _createTableBody: function() {
            this._$tableBody = c("<tbody></tbody>").appendTo(this._$table)
        },
        _createBottomPanel: function() {
            this._$bottomPanel = c('<div class="jtable-bottom-panel"></div>').appendTo(this._$mainContainer);
            //this._$bottomPanel.append('<div class="jtable-left-area"></div>');this._$bottomPanel.append('<div class="jtable-right-area"></div>')
            $('<div class="jtable-left-area"></div>').appendTo(this._$mainContainer);
        },
        _createBusyPanel: function() {
            this._$busyMessageDiv = c('<div class="jtable-busy-message"></div>').prependTo(this._$mainContainer);
            this._$busyDiv = c('<div class="jtable-busy-panel-background"></div>').prependTo(this._$mainContainer);
            this._hideBusy()
        },
        _createErrorDialogDiv: function() {
            var b = this;
            b._$errorDialogDiv = c("<div></div>").appendTo(b._$mainContainer);
            b._$errorDialogDiv.dialog({
                autoOpen: !1,
                show: b.options.dialogShowEffect,
                hide: b.options.dialogHideEffect,
                modal: !0,
                title: b.options.messages.error,
                buttons: [{
                    text: b.options.messages.close,
                    click: function() {
                        b._$errorDialogDiv.dialog("close")
                    }
                }]
            })
        },
        load: function(b, a) {
            this._lastPostData = b;
            this._reloadTable(a)
        },
        reload: function(b) {
            this._reloadTable(b)
        },
        destroy: function() {
            this.element.empty();
            c.Widget.prototype.destroy.call(this)
        },
        _reloadTable: function(b) {
            var a = this;
            a._showBusy(a.options.messages.loadingMessage);
            var c = a._createRecordLoadUrl();
            a._onLoadingRecords();
            a._performAjaxCall(c, a._lastPostData, !0, function(d) {
                a._hideBusy();
                if (d.Result != "OK")
                    a._showError(d.Message);
                else {
                    a._removeAllRows("reloading");
                    a._addRecordsToTable(d.Records);
                    a._onRecordsLoaded(d);
                    b && b()
                }
            }, function() {
                a._hideBusy();
                a._showError(a.options.messages.serverCommunicationError)
            })
        },
        _createRecordLoadUrl: function() {
            return this.options.actions.listAction
        },
        _createRowFromRecord: function(b) {
            b = c("<tr></tr>").data("record", b);
            this._addCellsToRowUsingRecord(b);
            return b
        },
        _addCellsToRowUsingRecord: function(b) {
            for (var a = 0; a < this._columnList.length; a++)
                this._createCellForRecordField(b.data("record"), this._columnList[a]).appendTo(b)
        },
        _createCellForRecordField: function(b, 
        a) {
            return c('<td class="' + this.options.fields[a].listClass + '"></td>').append(this._getDisplayTextForRecordField(b, a) || "")
        },
        _addRecordsToTable: function(b) {
            var a = this;
            c.each(b, function(b, d) {
                a._addRowToTable(a._createRowFromRecord(d))
            });
            a._refreshRowStyles()
        },
        _addRowToTable: function(b, a, c, d) {
            !0 != c && (c = !1);
            !1 != d && (d = !0);
            0 >= this._$tableRows.length && this._removeNoDataRow();
            a = this._normalizeNumber(a, 0, this._$tableRows.length, this._$tableRows.length);
            a == this._$tableRows.length ? (this._$tableBody.append(b),
            this._$tableRows.push(b)) : 0 == a ? (this._$tableBody.prepend(b),
            this._$tableRows.unshift(b)) : (this._$tableRows[a - 1].after(b),
            this._$tableRows.splice(a, 0, b));
            this._onRowInserted(b, c);
            !0 == c && (this._refreshRowStyles(),
            this.options.animationsEnabled && d && this._showNewRowAnimation(b))
        },
        _showNewRowAnimation: function(b) {
            b.addClass("jtable-row-created", "slow", "", function() {
                b.removeClass("jtable-row-created", 5E3)
            })
        },
        _removeRowsFromTable: function(b, a) {
            var f = this;
            0 >= b.length || (b.remove(),
            b.each(function() {
                f._$tableRows.splice(f._findRowIndex(c(this)), 
                1)
            }),
            f._onRowsRemoved(b, a),
            0 == f._$tableRows.length && f._addNoDataRow(),
            f._refreshRowStyles())
        },
        _findRowIndex: function(b) {
            return this._findIndexInArray(b, this._$tableRows, function(a, b) {
                return a.data("record") == b.data("record")
            })
        },
        _removeAllRows: function(b) {
            if (!(0 >= this._$tableRows.length)) {
                var a = this._$tableBody.find("tr");
                this._$tableBody.empty();
                this._$tableRows = [];
                this._onRowsRemoved(a, b);
                this._addNoDataRow()
            }
        },
        _addNoDataRow: function() {
            var b = this._$table.find("thead th").length;
            c('<tr class="jtable-no-data-row"></tr>').append('<td colspan="' + 
            b + '">' + this.options.messages.noDataAvailable + "</td>").appendTo(this._$tableBody)
        },
        _removeNoDataRow: function() {
            this._$tableBody.find(".jtable-no-data-row").remove()
        },
        _refreshRowStyles: function() {
            
            for (var b = 0; b < this._$tableRows.length; b++)
                0 == b % 2 ? this._$tableRows[b].addClass("jtable-row-even") : this._$tableRows[b].addClass("jtable-row-odd")
        },
        _getDisplayTextForRecordField: function(b, a) {
            var c = this.options.fields[a]
              , d = b[a];
            return c.display ? c.display({
                record: b
            }) : "date" == c.type ? this._getDisplayTextForDateRecordField(c, 
            d) : "checkbox" == c.type ? this._getCheckBoxTextForFieldByValue(a, d) : c.options ? this._getOptionsWithCaching(a)[d] : d
        },
        _getDisplayTextForDateRecordField: function(b, a) {
            if (!a)
                return "";
            var f = b.displayFormat || this.options.defaultDateFormat
              , d = this._parseDate(a);
            return c.datepicker.formatDate(f, d)
        },
        _parseDate: function(b) {
            if (0 <= b.indexOf("Date"))
                return new Date(parseInt(b.substr(6)));
            if (10 == b.length)
                return new Date(parseInt(b.substr(0, 4)),parseInt(b.substr(5, 2)) - 1,parseInt(b.substr(8, 2)));
            if (19 == b.length)
                return new Date(parseInt(b.substr(0, 
                4)),parseInt(b.substr(5, 2)) - 1,parseInt(b.substr(8, 2)),parseInt(b.substr(11, 2)),parseInt(b.substr(14, 2)),parseInt(b.substr(17, 2)));
            this._logWarn("Given date is no properly formatted: " + b);
            return new Date
        },
        _showError: function(b) {
            this._$errorDialogDiv.html(b).dialog("open")
        },
        _showBusy: function(b) {
            this._$busyMessageDiv.is(":visible") || (this._$busyDiv.width(this._$mainContainer.width()),
            this._$busyDiv.height(this._$mainContainer.height()),
            this._$busyDiv.show(),
            this._$busyMessageDiv.show());
            this._$busyMessageDiv.html(b)
        },
        _hideBusy: function() {
            this._$busyDiv.hide();
            this._$busyMessageDiv.html("").hide()
        },
        _isBusy: function() {
            return this._$busyMessageDiv.is(":visible")
        },
        _performAjaxCall: function(b, a, f, d, i) {
            c.ajax({
                url: b,
                type: "POST",
                dataType: "json",
                data: a,
                async: f,
                success: function(a) {
                    d(a)
                },
                error: function() {
                    i()
                }
            })
        },
        getRowByKey: function(b) {
            for (var a = 0; a < this._$tableRows.length; a++)
                if (b == this._$tableRows[a].data("record")[this._keyField])
                    return this._$tableRows[a];
            return null 
        },
        _onLoadingRecords: function() {
            this._trigger("loadingRecords", 
            null , {})
        },
        _onRecordsLoaded: function(b) {
            this._trigger("recordsLoaded", null , {
                records: b.Records,
                serverResponse: b
            })
        },
        _onRowInserted: function(b, a) {
            this._trigger("rowInserted", null , {
                row: b,
                record: b.data("record"),
                isNewRow: a
            })
        },
        _onRowsRemoved: function(b, a) {
            this._trigger("rowsRemoved", null , {
                rows: b,
                reason: a
            })
        },
        _onCloseRequested: function() {
            this._trigger("closeRequested", null , {})
        }
    })
})(jQuery);
(function(c) {
    c.extend(!0, c.hik.jtable.prototype, {
        _getPropertyOfObject: function(b, a) {
            if (a.indexOf(".") < 0)
                return b[a];
            var c = a.substring(0, a.indexOf("."))
              , d = a.substring(a.indexOf(".") + 1);
            return this._getPropertyOfObject(b[c], d)
        },
        _setPropertyOfObject: function(b, a, c) {
            if (a.indexOf(".") < 0)
                b[a] = c;
            else {
                var d = a.substring(0, a.indexOf("."))
                  , a = a.substring(a.indexOf(".") + 1);
                this._setPropertyOfObject(b[d], a, c)
            }
        },
        _insertToArrayIfDoesNotExists: function(b, a) {
            c.inArray(a, b) < 0 && b.push(a)
        },
        _findIndexInArray: function(b, 
        a, c) {
            c || (c = function(a, d) {
                return a == d
            }
            );
            for (var d = 0; d < a.length; d++)
                if (c(b, a[d]))
                    return d;
            return -1
        },
        _normalizeNumber: function(b, a, c, d) {
            return b == void 0 || b == null  ? d : b < a ? a : b > c ? c : b
        },
        _formatString: function() {
            if (arguments.length == 0)
                return null ;
            for (var b = arguments[0], a = 1; a < arguments.length; a++)
                b = b.replace("{" + (a - 1) + "}", arguments[a]);
            return b
        },
        _logDebug: function(b) {
            window.console && console.log("jTable DEBUG: " + b)
        },
        _logInfo: function(b) {
            window.console && console.log("jTable INFO: " + b)
        },
        _logWarn: function(b) {
            window.console && 
            console.log("jTable WARNING: " + b)
        },
        _logError: function(b) {
            window.console && console.log("jTable ERROR: " + b)
        }
    })
})(jQuery);
(function(c) {
    c.extend(!0, c.hik.jtable.prototype, {
        _submitFormUsingAjax: function(b, a, c, d) {
            this._performAjaxCall(b, a, true, c, d)
        },
        _createInputLabelForRecordField: function(b) {
            return c('<div class="jtable-input-label">' + this.options.fields[b].title + "</div>")
        },
        _createInputForRecordField: function(b, a, f) {
            var d = this.options.fields[b];
            if (a == void 0)
                a = d.defaultValue;
            if (d.input) {
                a = c(d.input({
                    value: a,
                    record: f
                }));
                a.attr("id") || a.attr("id", "Edit-" + b);
                return a
            }
            return d.type == "date" ? this._createDateInputForField(d, b, 
            a) : d.type == "textarea" ? this._createTextAreaForField(d, b, a) : d.type == "password" ? this._createPasswordInputForField(d, b, a) : d.type == "checkbox" ? this._createCheckboxForField(d, b, a) : d.options ? d.type == "radiobutton" ? this._createRadioButtonListForField(d, b, a) : this._createDropDownListForField(d, b, a) : this._createTextInputForField(d, b, a)
        },
        _createInputForHidden: function(b, a) {
            if (a == void 0 || a == null )
                a = "";
            return c('<input type="hidden" value="' + a + '" name="' + b + '" id="Edit-' + b + '"></input>')
        },
        _createDateInputForField: function(b, 
        a, f) {
            a = c('<input class="' + b.inputClass + '" id="Edit-' + a + '" type="text"' + (f != void 0 ? 'value="' + f + '"' : "") + ' name="' + a + '"></input>');
            a.datepicker({
                dateFormat: b.displayFormat || this.options.defaultDateFormat
            });
            return c('<div class="jtable-input jtable-date-input"></div>').append(a)
        },
        _createTextAreaForField: function(b, a, f) {
            return c('<div class="jtable-input jtable-textarea-input"><textarea class="' + b.inputClass + '" id="Edit-' + a + '" name="' + a + '">' + (f || "") + "</textarea></div>")
        },
        _createTextInputForField: function(b, 
        a, f) {
            return c('<div class="jtable-input jtable-text-input"><input class="' + b.inputClass + '" id="Edit-' + a + '" type="text"' + (f != void 0 ? 'value="' + f + '"' : "") + ' name="' + a + '"></input></div>')
        },
        _createPasswordInputForField: function(b, a, f) {
            return c('<div class="jtable-input jtable-password-input"><input class="' + b.inputClass + '" id="Edit-' + a + '" type="password"' + (f != void 0 ? 'value="' + f + '"' : "") + ' name="' + a + '"></input></div>')
        },
        _createCheckboxForField: function(b, a, f) {
            var d = this;
            f == void 0 && (f = f || d._getCheckBoxPropertiesForFieldByState(a, 
            false).Value);
            var i = c('<div class="jtable-input jtable-checkbox-input"></div>')
              , e = c('<input class="' + b.inputClass + '" id="Edit-' + a + '" type="checkbox" name="' + a + '" value="' + f + '" />').appendTo(i)
              , g = c("<span>" + (b.formText || d._getCheckBoxTextForFieldByValue(a, f)) + "</span>").appendTo(i);
            d._getIsCheckBoxSelectedForFieldByValue(a, f) && e.attr("checked", "checked");
            var h = function() {
                var c = d._getCheckBoxPropertiesForFieldByState(a, e.is(":checked"));
                e.attr("value", c.Value);
                g.html(b.formText || c.DisplayText)
            }
            ;
            e.click(function() {
                h()
            });
            if (b.setOnTextClick != false) {
                g.addClass("jtable-option-text-clickable");
                g.click(function() {
                    e.is(":checked") ? e.attr("checked", false) : e.attr("checked", true);
                    h()
                })
            }
            return i
        },
        _createDropDownListForField: function(b, a, f) {
            var d = c('<div class="jtable-input jtable-dropdown-input"></div>')
              , i = c('<select class="' + b.inputClass + '" id="Edit-' + a + '" name=' + a + "></select>").appendTo(d)
              , b = this._getOptionsWithCaching(a);
            c.each(b, function(a, d) {
                i.append('<option value="' + a + '"' + (a == f ? ' selected="selected"' : "") + ">" + d + "</option>")
            });
            return d
        },
        _createRadioButtonListForField: function(b, a, f) {
            var d = c('<div class="jtable-input jtable-radiobuttonlist-input"></div>')
              , i = this._getOptionsWithCaching(a)
              , e = 0;
            c.each(i, function(g, i) {
                var j = c('<div class="jtable-radio-input"></div>').appendTo(d)
                  , k = c('<input type="radio" id="Edit-' + a + e++ + '" class="' + b.inputClass + '" name="' + a + '" value="' + g + '"' + (g == f ? ' checked="true"' : "") + " />").appendTo(j)
                  , j = c("<span>" + i + "</span>").appendTo(j);
                if (b.setOnTextClick != false) {
                    j.addClass("jtable-option-text-clickable");
                    j.click(function() {
                        k.is(":checked") || k.attr("checked", true)
                    })
                }
            });
            return d
        },
        _getCheckBoxTextForFieldByValue: function(b, a) {
            return this.options.fields[b].values[a]
        },
        _getIsCheckBoxSelectedForFieldByValue: function(b, a) {
            return this._createCheckBoxStateArrayForFieldWithCaching(b)[1].Value.toString() == a.toString()
        },
        _getCheckBoxPropertiesForFieldByState: function(b, a) {
            return this._createCheckBoxStateArrayForFieldWithCaching(b)[a ? 1 : 0]
        },
        _createCheckBoxStateArrayForFieldWithCaching: function(b) {
            var a = "checkbox_" + 
            b;
            this._cache[a] || (this._cache[a] = this._createCheckBoxStateArrayForField(b));
            return this._cache[a]
        },
        _createCheckBoxStateArrayForField: function(b) {
            var a = []
              , f = 0;
            c.each(this.options.fields[b].values, function(d, b) {
                f++ < 2 && a.push({
                    Value: d,
                    DisplayText: b
                })
            });
            return a
        },
        _getOptionsWithCaching: function(b) {
            var a = "options_" + b;
            if (!this._cache[a]) {
                var c = this.options.fields[b].options;
                this._cache[a] = typeof c == "string" ? this._downloadOptions(b, c) : jQuery.isArray(c) ? this._buildOptionsFromArray(c) : c
            }
            return this._cache[a]
        },
        _downloadOptions: function(b, a) {
            var c = this
              , d = {};
            c._performAjaxCall(a, void 0, false, function(a) {
                if (a.Result != "OK")
                    c._showError(a.Message);
                else
                    for (var b = 0; b < a.Options.length; b++)
                        d[a.Options[b].Value] = a.Options[b].DisplayText
            }, function() {
                var a = c._formatString(c.options.messages.cannotLoadOptionsFor, b);
                c._showError(a)
            });
            return d
        },
        _buildOptionsFromArray: function(b) {
            for (var a = {}, c = 0; c < b.length; c++)
                a[b[c]] = b[c];
            return a
        },
        _setEnabledOfDialogButton: function(b, a, c) {
            if (b) {
                a != false ? b.removeAttr("disabled").removeClass("ui-state-disabled") : 
                b.attr("disabled", "disabled").addClass("ui-state-disabled");
                c && b.find("span").text(c)
            }
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype._create;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            recordAdded: function() {},
            messages: {
                addNewRecord: "+ Add new record"
            }
        },
        _$addRecordDiv: null ,
        _create: function() {
            b.apply(this, arguments);
            this._createAddRecordDialogDiv()
        },
        _createAddRecordDialogDiv: function() {
            var a = this;
            if (a.options.actions.createAction) {
                a._$addRecordDiv = c("<div></div>").appendTo(a._$mainContainer);
                a._$addRecordDiv.dialog({
                    autoOpen: false,
                    show: a.options.dialogShowEffect,
                    hide: a.options.dialogHideEffect,
                    width: "auto",
                    minWidth: "300",
                    modal: true,
                    title: a.options.messages.addNewRecord,
                    buttons: [{
                        text: a.options.messages.cancel,
                        click: function() {
                            a._$addRecordDiv.dialog("close")
                        }
                    }, {
                        id: "AddRecordDialogSaveButton",
                        text: a.options.messages.save,
                        click: function() {
                            var b = c("#AddRecordDialogSaveButton")
                              , d = a._$addRecordDiv.find("form");
                            if (a._trigger("formSubmitting", null , {
                                form: d,
                                formType: "create"
                            }) != false) {
                                a._setEnabledOfDialogButton(b, false, a.options.messages.saving);
                                a._saveAddRecordForm(d, b)
                            }
                        }
                    }],
                    close: function() {
                        var b = 
                        a._$addRecordDiv.find("form").first()
                          , d = c("#AddRecordDialogSaveButton");
                        a._trigger("formClosed", null , {
                            form: b,
                            formType: "create"
                        });
                        a._setEnabledOfDialogButton(d, true, a.options.messages.save);
                        b.remove()
                    }
                });
                if (!a.options.addRecordButton)
                    a.options.addRecordButton = a._createAddRecordButton();
                a.options.addRecordButton.click(function(b) {
                    b.preventDefault();
                    a._showAddRecordForm()
                })
            }
        },
        _createAddRecordButton: function() {
            return c('<span class="jtable-add-record"><a href="#">' + this.options.messages.addNewRecord + 
            "</a></span>").appendTo(this._$bottomPanel.find(".jtable-right-area"))
        },
        showCreateForm: function() {
            this._showAddRecordForm()
        },
        addRecord: function(a) {
            var b = this
              , a = c.extend({
                clientOnly: false,
                animationsEnabled: b.options.animationsEnabled,
                url: b.options.actions.createAction,
                success: function() {},
                error: function() {}
            }, a);
            if (a.record)
                if (a.clientOnly) {
                    b._addRowToTable(b._createRowFromRecord(a.record), null , true, a.animationsEnabled);
                    a.success()
                } else
                    b._submitFormUsingAjax(a.url, c.param(a.record), function(d) {
                        if (d.Result != 
                        "OK") {
                            b._showError(d.Message);
                            a.error(d)
                        } else {
                            b._onRecordAdded(d);
                            b._addRowToTable(b._createRowFromRecord(d.Record), null , true, a.animationsEnabled);
                            a.success(d)
                        }
                    }, function() {
                        b._showError(b.options.messages.serverCommunicationError);
                        a.error()
                    });
            else
                b._logWarn("options parameter in addRecord method must contain a record property.")
        },
        _showAddRecordForm: function() {
            for (var a = c('<form id="jtable-create-form" class="jtable-dialog-form jtable-create-form" action="' + this.options.actions.createAction + '" method="POST"></form>'), 
            b = 0; b < this._fieldList.length; b++)
                if (this.options.fields[this._fieldList[b]].create != false)
                    if (this.options.fields[this._fieldList[b]].type == "hidden")
                        a.append(this._createInputForHidden(this._fieldList[b], this.options.fields[this._fieldList[b]].defaultValue));
                    else {
                        var d = c('<div class="jtable-input-field-container"></div>').appendTo(a);
                        d.append(this._createInputLabelForRecordField(this._fieldList[b]));
                        d.append(this._createInputForRecordField(this._fieldList[b]))
                    }
            this._$addRecordDiv.append(a).dialog("open");
            this._trigger("formCreated", null , {
                form: a,
                formType: "create"
            })
        },
        _saveAddRecordForm: function(a, b) {
            var d = this;
            a.data("submitting", true);
            d._submitFormUsingAjax(a.attr("action"), a.serialize(), function(a) {
                if (a.Result != "OK") {
                    d._showError(a.Message);
                    d._setEnabledOfDialogButton(b, true, d.options.messages.save)
                } else {
                    d._onRecordAdded(a);
                    d._addRowToTable(d._createRowFromRecord(a.Record), null , true);
                    d._$addRecordDiv.dialog("close")
                }
            }, function() {
                d._showError(d.options.messages.serverCommunicationError);
                d._setEnabledOfDialogButton(b, 
                true, d.options.messages.save)
            })
        },
        _onRecordAdded: function(a) {
            this._trigger("recordAdded", null , {
                record: a.Record,
                serverResponse: a
            })
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype._create
      , a = c.hik.jtable.prototype._addColumnsToHeaderRow
      , f = c.hik.jtable.prototype._addCellsToRowUsingRecord;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            recordUpdated: function() {},
            rowUpdated: function() {},
            messages: {
                editRecord: "Edit Record"
            }
        },
        _$editDiv: null ,
        _$editingRow: null ,
        _create: function() {
            b.apply(this, arguments);
            this._createEditDialogDiv()
        },
        _createEditDialogDiv: function() {
            var a = this;
            a._$editDiv = c("<div></div>").appendTo(a._$mainContainer);
            a._$editDiv.dialog({
                autoOpen: false,
                show: a.options.dialogShowEffect,
                hide: a.options.dialogHideEffect,
                width: "auto",
                minWidth: "300",
                modal: true,
                title: a.options.messages.editRecord,
                buttons: [{
                    text: a.options.messages.cancel,
                    click: function() {
                        a._$editDiv.dialog("close")
                    }
                }, {
                    id: "EditDialogSaveButton",
                    text: a.options.messages.save,
                    click: function() {
                        var b = a._$editDiv.find("#EditDialogSaveButton")
                          , c = a._$editDiv.find("form");
                        if (a._trigger("formSubmitting", null , {
                            form: c,
                            formType: "edit"
                        }) != false) {
                            a._setEnabledOfDialogButton(b, false, a.options.messages.saving);
                            a._saveEditForm(c, b)
                        }
                    }
                }],
                close: function() {
                    var b = a._$editDiv.find("form:first")
                      , e = c("#EditDialogSaveButton");
                    a._trigger("formClosed", null , {
                        form: b,
                        formType: "edit"
                    });
                    a._setEnabledOfDialogButton(e, true, a.options.messages.save);
                    b.remove()
                }
            })
        },
        updateRecord: function(a) {
            var b = this
              , a = c.extend({
                clientOnly: false,
                animationsEnabled: b.options.animationsEnabled,
                url: b.options.actions.updateAction,
                success: function() {},
                error: function() {}
            }, a)
              , e = a.record[b._keyField];
            if (!a.record || !e)
                b._logWarn("options parameter in updateRecord method must contain a record that contains the key field property.");
            else {
                var g = b.getRowByKey(e);
                if (g == null )
                    b._logWarn("Can not found any row by key: " + e);
                else if (a.clientOnly) {
                    c.extend(g.data("record"), a.record);
                    b._updateRowTexts(g);
                    b._onRecordUpdated(g, null );
                    a.animationsEnabled && b._showUpdateAnimationForRow(g);
                    a.success()
                } else
                    b._submitFormUsingAjax(a.url, c.param(a.record), function(e) {
                        if (e.Result != "OK") {
                            b._showError(e.Message);
                            a.error(e)
                        } else {
                            c.extend(g.data("record"), a.record);
                            b._updateRowTexts(g);
                            b._onRecordUpdated(g, e);
                            a.animationsEnabled && b._showUpdateAnimationForRow(g);
                            a.success(e)
                        }
                    }, function() {
                        b._showError(b.options.messages.serverCommunicationError);
                        a.error()
                    })
            }
        },
        _addColumnsToHeaderRow: function(b) {
            a.apply(this, arguments);
            this.options.actions.updateAction != void 0 && b.append(this._createEmptyCommandHeader())
        },
        _addCellsToRowUsingRecord: function(a) {
            var b = this;
            f.apply(this, arguments);
            if (b.options.actions.updateAction != void 0) {
                var e = c('<td class="jtable-command-column"></td>').appendTo(a);
                c('<button class="jtable-command-button jtable-edit-command-button" title="' + b.options.messages.editRecord + 
                '"><span>' + b.options.messages.editRecord + "</span></button>").appendTo(e).click(function(g) {
                    g.preventDefault();
                    g.stopPropagation();
                    b._showEditForm(a)
                })
            }
        },
        _showEditForm: function(a) {
            for (var b = a.data("record"), e = c('<form id="jtable-edit-form" class="jtable-dialog-form jtable-edit-form" action="' + this.options.actions.updateAction + '" method="POST"></form>'), g = 0; g < this._fieldList.length; g++)
                if (this.options.fields[this._fieldList[g]].key == true)
                    e.append(this._createInputForHidden(this._fieldList[g], b[this._fieldList[g]]));
                else if (this.options.fields[this._fieldList[g]].edit != false)
                    if (this.options.fields[this._fieldList[g]].type == "hidden")
                        e.append(this._createInputForHidden(this._fieldList[g], b[this._fieldList[g]]));
                    else {
                        var h = c('<div class="jtable-input-field-container"></div>').appendTo(e);
                        h.append(this._createInputLabelForRecordField(this._fieldList[g]));
                        var j = this._getValueForRecordField(b, this._fieldList[g]);
                        h.append(this._createInputForRecordField(this._fieldList[g], j, b))
                    }
            this._$editingRow = a;
            this._$editDiv.append(e).dialog("open");
            this._trigger("formCreated", null , {
                form: e,
                formType: "edit",
                record: b
            })
        },
        _saveEditForm: function(a, b) {
            var c = this;
            c._submitFormUsingAjax(a.attr("action"), a.serialize(), function(g) {
                if (g.Result != "OK") {
                    c._showError(g.Message);
                    c._setEnabledOfDialogButton(b, true, c.options.messages.save)
                } else {
                    var h = c._$editingRow;
                    c._updateRecordValuesFromEditForm(h.data("record"), a);
                    c._updateRowTexts(h);
                    c._onRecordUpdated(h, g);
                    c.options.animationsEnabled && c._showUpdateAnimationForRow(h);
                    c._$editDiv.dialog("close")
                }
            }, function() {
                c._showError(c.options.messages.serverCommunicationError);
                c._setEnabledOfDialogButton(b, true, c.options.messages.save)
            })
        },
        _updateRecordValuesFromEditForm: function(a, b) {
            for (var e = 0; e < this._fieldList.length; e++) {
                var g = this._fieldList[e]
                  , h = this.options.fields[g];
                if (h.edit != false) {
                    var j = b.find('[name="' + g + '"]');
                    if (h.type == "date") {
                        h = h.displayFormat || this.options.defaultDateFormat;
                        try {
                            var f = c.datepicker.parseDate(h, j.val());
                            a[g] = "/Date(" + f.getTime() + ")/"
                        } catch (m) {
                            a[g] = "/Date(" + (new Date).getTime() + ")/"
                        }
                    } else if (h.options && h.type == "radiobutton") {
                        j = j.filter('[checked="true"]');
                        a[g] = j.length ? j.val() : void 0
                    } else
                        a[g] = j.val()
                }
            }
        },
        _getValueForRecordField: function(a, b) {
            var c = this.options.fields[b]
              , g = a[b];
            return c.type == "date" ? this._getDisplayTextForDateRecordField(c, g) : g
        },
        _updateRowTexts: function(a) {
            for (var b = a.data("record"), c = a.find("td"), g = 0; g < this._columnList.length; g++) {
                var h = this._getDisplayTextForRecordField(b, this._columnList[g]);
                c.eq(this._firstDataColumnOffset + g).html(h || "")
            }
            this._onRowUpdated(a)
        },
        _showUpdateAnimationForRow: function(a) {
            a.stop(true, true).addClass("jtable-row-updated", 
            "slow", "", function() {
                a.removeClass("jtable-row-updated", 5E3)
            })
        },
        _onRowUpdated: function(a) {
            this._trigger("rowUpdated", null , {
                row: a,
                record: a.data("record")
            })
        },
        _onRecordUpdated: function(a, b) {
            this._trigger("recordUpdated", null , {
                record: a.data("record"),
                row: a,
                serverResponse: b
            })
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype._create
      , a = c.hik.jtable.prototype._addColumnsToHeaderRow
      , f = c.hik.jtable.prototype._addCellsToRowUsingRecord;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            deleteConfirmation: !0,
            recordDeleted: function() {},
            messages: {
                deleteConfirmation: "This record will be deleted. Are you sure?",
                deleteText: "Delete",
                deleting: "Deleting",
                canNotDeletedRecords: "Can not deleted {0} of {1} records!",
                deleteProggress: "Deleted {0} of {1} records, processing..."
            }
        },
        _$deleteRecordDiv: null ,
        _$deletingRow: null ,
        _create: function() {
            b.apply(this, arguments);
            this._createDeleteDialogDiv()
        },
        _createDeleteDialogDiv: function() {
            var a = this;
            a._$deleteRecordDiv = c('<div><p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span><span class="jtable-delete-confirm-message"></span></p></div>').appendTo(a._$mainContainer);
            a._$deleteRecordDiv.dialog({
                autoOpen: false,
                show: a.options.dialogShowEffect,
                hide: a.options.dialogHideEffect,
                modal: true,
                title: a.options.messages.areYouSure,
                buttons: [{
                    text: a.options.messages.cancel,
                    click: function() {
                        a._$deleteRecordDiv.dialog("close")
                    }
                }, {
                    id: "DeleteDialogButton",
                    text: a.options.messages.deleteText,
                    click: function() {
                        var b = c("#DeleteDialogButton");
                        a._setEnabledOfDialogButton(b, false, a.options.messages.deleting);
                        a._deleteRecordFromServer(a._$deletingRow, function() {
                            a._removeRowsFromTableWithAnimation(a._$deletingRow);
                            a._$deleteRecordDiv.dialog("close")
                        }, function(c) {
                            a._showError(c);
                            a._setEnabledOfDialogButton(b, true, a.options.messages.deleteText)
                        })
                    }
                }],
                close: function() {
                    var b = c("#DeleteDialogButton");
                    a._setEnabledOfDialogButton(b, true, a.options.messages.deleteText)
                }
            })
        },
        deleteRows: function(a) {
            var b = this;
            if (!(a.length <= 0 || b._isBusy()))
                if (a.length == 1)
                    b._deleteRecordFromServer(a, function() {
                        b._removeRowsFromTableWithAnimation(a)
                    }, function(a) {
                        b._showError(a)
                    });
                else {
                    b._showBusy(b._formatString(b.options.messages.deleteProggress, 0, a.length));
                    var e = 0
                      , g = function() {
                        var c = a.filter(".jtable-row-ready-to-remove");
                        c.length < a.length && b._showError(b._formatString(b.options.messages.canNotDeletedRecords, a.length - 
                        c.length, a.length));
                        c.length > 0 && b._removeRowsFromTableWithAnimation(c);
                        b._hideBusy()
                    }
                      , h = 0;
                    a.each(function() {
                        var f = c(this);
                        b._deleteRecordFromServer(f, function() {
                            ++h;
                            ++e;
                            f.addClass("jtable-row-ready-to-remove");
                            b._showBusy(b._formatString(b.options.messages.deleteProggress, h, a.length));
                            e >= a.length && g()
                        }, function() {
                            ++e;
                            e >= a.length && g()
                        })
                    })
                }
        },
        deleteRecord: function(a) {
            var b = this
              , a = c.extend({
                clientOnly: false,
                animationsEnabled: b.options.animationsEnabled,
                url: b.options.actions.deleteAction,
                success: function() {},
                error: function() {}
            }, a);
            if (a.key) {
                var e = b.getRowByKey(a.key);
                if (e == null )
                    b._logWarn("Can not found any row by key: " + a.key);
                else if (a.clientOnly) {
                    b._removeRowsFromTableWithAnimation(e, a.animationsEnabled);
                    a.success()
                } else
                    b._deleteRecordFromServer(e, function(c) {
                        b._removeRowsFromTableWithAnimation(e, a.animationsEnabled);
                        a.success(c)
                    }, function(c) {
                        b._showError(c);
                        a.error(c)
                    }, a.url)
            } else
                b._logWarn("options parameter in deleteRecord method must contain a record key.")
        },
        _addColumnsToHeaderRow: function(b) {
            a.apply(this, 
            arguments);
            this.options.actions.deleteAction != void 0 && b.append(this._createEmptyCommandHeader())
        },
        _addCellsToRowUsingRecord: function(a) {
            f.apply(this, arguments);
            var b = this;
            if (b.options.actions.deleteAction != void 0) {
                var e = c('<td class="jtable-command-column"></td>').appendTo(a);
                c('<button class="jtable-command-button jtable-delete-command-button" title="' + b.options.messages.deleteText + '"><span>' + b.options.messages.deleteText + "</span></button>").appendTo(e).click(function(c) {
                    c.preventDefault();
                    c.stopPropagation();
                    b._deleteButtonClickedForRow(a)
                })
            }
        },
        _deleteButtonClickedForRow: function(a) {
            var b = this, e, g = b.options.messages.deleteConfirmation;
            if (c.isFunction(b.options.deleteConfirmation)) {
                e = {
                    row: a,
                    record: a.data("record"),
                    deleteConfirm: true,
                    deleteConfirmMessage: g,
                    cancel: false,
                    cancelMessage: null 
                };
                b.options.deleteConfirmation(e);
                if (e.cancel) {
                    e.cancelMessage && b._showError(e.cancelMessage);
                    return
                }
                g = e.deleteConfirmMessage;
                e = e.deleteConfirm
            } else
                e = b.options.deleteConfirmation;
            if (e != false) {
                b._$deleteRecordDiv.find(".jtable-delete-confirm-message").html(g);
                b._showDeleteDialog(a)
            } else
                b._deleteRecordFromServer(a, function() {
                    b._removeRowsFromTableWithAnimation(a)
                }, function(a) {
                    b._showError(a)
                })
        },
        _showDeleteDialog: function(a) {
            this._$deletingRow = a;
            this._$deleteRecordDiv.dialog("open")
        },
        _deleteRecordFromServer: function(a, b, c, g) {
            var h = this;
            if (a.data("deleting") != true) {
                a.data("deleting", true);
                var f = {};
                f[h._keyField] = a.data("record")[h._keyField];
                h._performAjaxCall(g || h.options.actions.deleteAction, f, true, function(g) {
                    if (g.Result != "OK") {
                        a.data("deleting", false);
                        c && c(g.Message)
                    } else {
                        h._trigger("recordDeleted", null , {
                            record: a.data("record"),
                            row: a,
                            serverResponse: g
                        });
                        b && b(g)
                    }
                }, function() {
                    a.data("deleting", false);
                    c && c(h.options.messages.serverCommunicationError)
                })
            }
        },
        _removeRowsFromTableWithAnimation: function(a, b) {
            var c = this;
            if (b == void 0)
                b = c.options.animationsEnabled;
            b ? a.stop(true, true).addClass("jtable-row-deleting", "slow", "").promise().done(function() {
                c._removeRowsFromTable(a, "deleted")
            }) : c._removeRowsFromTable(a, "deleted")
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype._create
      , a = c.hik.jtable.prototype._addColumnsToHeaderRow
      , f = c.hik.jtable.prototype._addCellsToRowUsingRecord
      , d = c.hik.jtable.prototype._onLoadingRecords
      , i = c.hik.jtable.prototype._onRecordsLoaded
      , e = c.hik.jtable.prototype._onRowsRemoved;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            selecting: !1,
            multiselect: !1,
            selectingCheckboxes: !1,
            selectOnRowClick: !0,
            selectionChanged: function() {}
        },
        _selectedRecordIdsBeforeLoad: null ,
        _$selectAllCheckbox: null ,
        _shiftKeyDown: !1,
        _create: function() {
            this.options.selecting && 
            this.options.selectingCheckboxes && ++this._firstDataColumnOffset;
            this._bindKeyboardEvents();
            b.apply(this, arguments)
        },
        _bindKeyboardEvents: function() {
            var a = this;
            c(document).keydown(function(b) {
                switch (b.which) {
                case 16:
                    a._shiftKeyDown = true
                }
            }).keyup(function(b) {
                switch (b.which) {
                case 16:
                    a._shiftKeyDown = false
                }
            })
        },
        selectedRows: function() {
            return this._getSelectedRows()
        },
        _addColumnsToHeaderRow: function(b) {
            this.options.selecting && this.options.selectingCheckboxes && (this.options.multiselect ? b.append(this._createSelectAllHeader()) : 
            b.append(this._createEmptyCommandHeader()));
            a.apply(this, arguments)
        },
        _addCellsToRowUsingRecord: function(a) {
            this.options.selecting && this._makeRowSelectable(a);
            f.apply(this, arguments)
        },
        _onLoadingRecords: function() {
            this._storeSelectionList();
            d.apply(this, arguments)
        },
        _onRecordsLoaded: function() {
            this._restoreSelectionList();
            i.apply(this, arguments)
        },
        _onRowsRemoved: function(a, b) {
            b != "reloading" && this.options.selecting && a.filter(".jtable-row-selected").length > 0 && this._onSelectionChanged();
            e.apply(this, arguments)
        },
        _createSelectAllHeader: function() {
            var a = this
              , b = c('<th class="jtable-column-header jtable-column-header-selecting"></th>')
              , e = c('<div class="jtable-column-header-container"></div>').appendTo(b);
            a._$selectAllCheckbox = c('<input type="checkbox" />').appendTo(e);
            a._$selectAllCheckbox.click(function() {
                if (a._$tableRows.length <= 0)
                    a._$selectAllCheckbox.attr("checked", false);
                else {
                    var b = a._$tableBody.find("tr");
                    a._$selectAllCheckbox.is(":checked") ? a._selectRows(b) : a._deselectRows(b);
                    a._onSelectionChanged()
                }
            });
            return b
        },
        _storeSelectionList: function() {
            var a = this;
            if (a.options.selecting) {
                a._selectedRecordIdsBeforeLoad = [];
                a._getSelectedRows().each(function() {
                    a._selectedRecordIdsBeforeLoad.push(c(this).data("record")[a._keyField])
                })
            }
        },
        _restoreSelectionList: function() {
            if (this.options.selecting) {
                for (var a = 0, b = 0; b < this._$tableRows.length; ++b)
                    if (c.inArray(this._$tableRows[b].data("record")[this._keyField], this._selectedRecordIdsBeforeLoad) > -1) {
                        this._selectRows(this._$tableRows[b]);
                        ++a
                    }
                this._selectedRecordIdsBeforeLoad.length > 
                0 && this._selectedRecordIdsBeforeLoad.length != a && this._onSelectionChanged();
                this._selectedRecordIdsBeforeLoad = [];
                this._refreshSelectAllCheckboxState()
            }
        },
        _getSelectedRows: function() {
            return this._$tableBody.find(".jtable-row-selected")
        },
        _makeRowSelectable: function(a) {
            var b = this;
            b.options.selectOnRowClick && a.click(function() {
                b._invertRowSelection(a)
            });
            if (b.options.selectingCheckboxes) {
                var e = c('<td class="jtable-selecting-column"></td>')
                  , d = c('<input type="checkbox" />').appendTo(e);
                b.options.selectOnRowClick || 
                d.click(function() {
                    b._invertRowSelection(a)
                });
                a.append(e)
            }
        },
        _invertRowSelection: function(a) {
            if (a.hasClass("jtable-row-selected"))
                this._deselectRows(a);
            else if (this._shiftKeyDown) {
                var b = this._findRowIndex(a)
                  , c = this._findFirstSelectedRowIndexBeforeIndex(b) + 1;
                if (c > 0 && c < b)
                    this._selectRows(this._$tableBody.find("tr").slice(c, b + 1));
                else {
                    c = this._findFirstSelectedRowIndexAfterIndex(b) - 1;
                    c > b ? this._selectRows(this._$tableBody.find("tr").slice(b, c + 1)) : this._selectRows(a)
                }
            } else
                this._selectRows(a);
            this._onSelectionChanged()
        },
        _findFirstSelectedRowIndexBeforeIndex: function(a) {
            for (a = a - 1; a >= 0; --a)
                if (this._$tableRows[a].hasClass("jtable-row-selected"))
                    return a;
            return -1
        },
        _findFirstSelectedRowIndexAfterIndex: function(a) {
            for (a = a + 1; a < this._$tableRows.length; ++a)
                if (this._$tableRows[a].hasClass("jtable-row-selected"))
                    return a;
            return -1
        },
        _selectRows: function(a) {
            this.options.multiselect || this._deselectRows(this._getSelectedRows());
            a.addClass("jtable-row-selected");
            this.options.selectingCheckboxes && a.find("td.jtable-selecting-column input").attr("checked", 
            true);
            this._refreshSelectAllCheckboxState()
        },
        _deselectRows: function(a) {
            a.removeClass("jtable-row-selected");
            this.options.selectingCheckboxes && a.find("td.jtable-selecting-column input").removeAttr("checked");
            this._refreshSelectAllCheckboxState()
        },
        _refreshSelectAllCheckboxState: function() {
            if (this.options.selectingCheckboxes && this.options.multiselect) {
                var a = this._$tableRows.length
                  , b = this._getSelectedRows().length;
                if (b == 0) {
                    this._$selectAllCheckbox.prop("indeterminate", false);
                    this._$selectAllCheckbox.attr("checked", 
                    false)
                } else if (b == a) {
                    this._$selectAllCheckbox.prop("indeterminate", false);
                    this._$selectAllCheckbox.attr("checked", true)
                } else {
                    this._$selectAllCheckbox.attr("checked", false);
                    this._$selectAllCheckbox.prop("indeterminate", true)
                }
            }
        },
        _onSelectionChanged: function() {
            this._trigger("selectionChanged", null , {})
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype.load
      , a = c.hik.jtable.prototype._create
      , f = c.hik.jtable.prototype._createRecordLoadUrl
      , d = c.hik.jtable.prototype._addRowToTable
      , i = c.hik.jtable.prototype._removeRowsFromTable
      , e = c.hik.jtable.prototype._onRecordsLoaded;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            paging: !1,
            pageSize: 10,
            messages: {
                pagingInfo: "Showing {0} to {1} of {2} records"
            }
        },
        _$pagingListArea: null ,
        _totalRecordCount: 0,
        _currentPageNo: 1,
        _create: function() {
            a.apply(this, arguments);
            this._createPageListArea()
        },
        _createPageListArea: function() {
            if (this.options.paging)
                this._$pagingListArea = c('<span class="jtable-page-list"></span>').prependTo(this._$mainContainer.find(".jtable-left-area"))
        },
        load: function() {
            this._currentPageNo = 1;
            b.apply(this, arguments)
        },
        reset: function(postData) {
            this._lastPostData = postData;
            arguments = postData;
        },
        _createRecordLoadUrl: function() {
            var a = f.apply(this, arguments);
            return a = this._addPagingInfoToUrl(a, this._currentPageNo)
        },
        _addRowToTable: function(a, b, c) {
            c && this.options.paging ? this._reloadTable() : d.apply(this, arguments)
        },
        _removeRowsFromTable: function(a, b) {
            i.apply(this, 
            arguments);
            if (this.options.paging) {
                this._$tableRows.length <= 0 && this._currentPageNo > 1 && --this._currentPageNo;
                this._reloadTable()
            }
        },
        _onRecordsLoaded: function(a) {
            this._totalRecordCount = a.TotalRecordCount;
            this._createPagingList();
            e.apply(this, arguments)
        },
        _addPagingInfoToUrl: function(a, b) {
            if (!this.options.paging)
                return a;
            var c = (b - 1) * this.options.pageSize
              , e = this.options.pageSize;
            return a + (a.indexOf("?") < 0 ? "?" : "&") + "jtStartIndex=" + c + "&jtPageSize=" + e
        },
        _createPagingList: function() {
            if (this.options.paging && 
            !(this.options.pageSize <= 0)) {
                this._$pagingListArea.empty();
                var a = this._calculatePageCount();
                this._createFirstAndPreviousPageButtons();
                this._createPageNumberButtons(this._calculatePageNumbers(a));
                this._createLastAndNextPageButtons(a);
                this._createPagingInfo();
                this._bindClickEventsToPageNumberButtons()
            }
        },
        _createFirstAndPreviousPageButtons: function() {
            if (this._currentPageNo > 1) {
                c('<span class="jtable-page-number-first">|&lt&nbsp;<label>First</label></span>').data("pageNumber", 1).appendTo(this._$pagingListArea);
                c('<span class="jtable-page-number-previous">&lt&nbsp;<label>Previous</label></span>').data("pageNumber", 
                this._currentPageNo - 1).appendTo(this._$pagingListArea)
            }
        },
        _createLastAndNextPageButtons: function(a) {
            if (this._currentPageNo < a) {
                c('<span class="jtable-page-number-next"><label>Next</label>&nbsp;&gt;&gt;</span>').data("pageNumber", this._currentPageNo + 1).appendTo(this._$pagingListArea);
                c('<span class="jtable-page-number-last"><label>Last</label>&nbsp;&gt|</span>').data("pageNumber", a).appendTo(this._$pagingListArea)
            }
        },
        _createPageNumberButtons: function(a) {
            for (var b = 0, e = 0; e < a.length; e++) {
                a[e] - b > 1 && c('<span class="jtable-page-number-space">...</span>').appendTo(this._$pagingListArea);
                this._createPageNumberButton(a[e]);
                b = a[e]
            }
        },
        _createPageNumberButton: function(a) {
            c('<span class="' + (this._currentPageNo == a ? "jtable-page-number-active" : "jtable-page-number") + '">' + a + "</span>").data("pageNumber", a).appendTo(this._$pagingListArea)
        },
        _calculatePageCount: function() {
            var a = Math.floor(this._totalRecordCount / this.options.pageSize);
            this._totalRecordCount % this.options.pageSize != 0 && ++a;
            return a
        },
        _calculatePageNumbers: function(a) {
            if (a <= 6) {
                for (var b = [], c = 1; c <= a; ++c)
                    b.push(c);
                return b
            }
            b = [1, 2, 3, a - 2, 
            a - 1, a];
            c = this._normalizeNumber(this._currentPageNo - 1, 1, a, 1);
            a = this._normalizeNumber(this._currentPageNo + 1, 1, a, 1);
            this._insertToArrayIfDoesNotExists(b, c);
            this._insertToArrayIfDoesNotExists(b, this._currentPageNo);
            this._insertToArrayIfDoesNotExists(b, a);
            b.sort(function(a, b) {
                return a - b
            });
            return b
        },
        _createPagingInfo: function() {
            var a = (this._currentPageNo - 1) * this.options.pageSize + 1
              , b = this._currentPageNo * this.options.pageSize
              , b = this._normalizeNumber(b, a, this._totalRecordCount, 0);
            if (b >= a) {
                a = this._formatString(this.options.messages.pagingInfo, 
                a, b, this._totalRecordCount);
                c('<span class="jtable-page-info">' + a + "</span>").appendTo(this._$pagingListArea)
            }
        },
        _bindClickEventsToPageNumberButtons: function() {
            var a = this;
            a._$pagingListArea.find(".jtable-page-number,.jtable-page-number-previous,.jtable-page-number-next,.jtable-page-number-first,.jtable-page-number-last").click(function(b) {
                b.preventDefault();
                b = c(this);
                a._currentPageNo = b.data("pageNumber");
                a._reloadTable()
            })
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype._normalizeFieldOptions
      , a = c.hik.jtable.prototype._createHeaderCellForField
      , f = c.hik.jtable.prototype._createRecordLoadUrl;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            sorting: !1,
            defaultSorting: ""
        },
        _lastSorting: "",
        _normalizeFieldOptions: function(a, c) {
            b.apply(this, arguments);
            c.sorting = c.sorting != false
        },
        _createHeaderCellForField: function(b, c) {
            var e = a.apply(this, arguments);
            this.options.sorting && c.sorting && this._makeColumnSortable(e, b);
            return e
        },
        _createRecordLoadUrl: function() {
            var a = 
            f.apply(this, arguments);
            return a = this._addSortingInfoToUrl(a)
        },
        _makeColumnSortable: function(a, b) {
            var c = this;
            a.addClass("jtable-column-header-sortable").click(function(b) {
                b.preventDefault();
                c._sortTableByColumn(a)
            });
            if (c.options.defaultSorting.indexOf(b) > -1)
                if (c.options.defaultSorting.indexOf("DESC") > -1) {
                    a.addClass("jtable-column-header-sorted-desc");
                    c._lastSorting = b + " DESC"
                } else {
                    a.addClass("jtable-column-header-sorted-asc");
                    c._lastSorting = b + " ASC"
                }
        },
        _sortTableByColumn: function(a) {
            a.siblings().removeClass("jtable-column-header-sorted-asc jtable-column-header-sorted-desc");
            if (a.hasClass("jtable-column-header-sorted-asc")) {
                a.removeClass("jtable-column-header-sorted-asc").addClass("jtable-column-header-sorted-desc");
                this._lastSorting = a.data("fieldName") + " DESC"
            } else {
                a.removeClass("jtable-column-header-sorted-desc").addClass("jtable-column-header-sorted-asc");
                this._lastSorting = a.data("fieldName") + " ASC"
            }
            this._reloadTable()
        },
        _addSortingInfoToUrl: function(a) {
            return !this.options.sorting || this._lastSorting == "" ? a : a + (a.indexOf("?") < 0 ? "?" : "&") + "jtSorting=" + this._lastSorting
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype._create
      , a = c.hik.jtable.prototype._normalizeFieldOptions
      , f = c.hik.jtable.prototype._createHeaderCellForField
      , d = c.hik.jtable.prototype._createCellForRecordField;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            tableId: void 0,
            columnResizable: !0,
            columnSelectable: !1,
            saveUserPreferences: !0
        },
        _$columnSelectionDiv: null ,
        _$columnResizeBar: null ,
        _cookieKeyPrefix: null ,
        _currentResizeArgs: null ,
        _create: function() {
            b.apply(this, arguments);
            this._createColumnResizeBar();
            this._createColumnSelection();
            this._cookieKeyPrefix = this._generateCookieKeyPrefix();
            this.options.saveUserPreferences && this._loadColumnSettings();
            this._normalizeColumnWidths()
        },
        _normalizeFieldOptions: function(b, c) {
            a.apply(this, arguments);
            if (this.options.columnResizable)
                c.columnResizable = c.columnResizable != false;
            if (!c.visibility)
                c.visibility = "visible"
        },
        _createHeaderCellForField: function(a, b) {
            var c = f.apply(this, arguments);
            this.options.columnResizable && b.columnResizable && a != this._columnList[this._columnList.length - 1] && this._makeColumnResizable(c);
            b.visibility == "hidden" && c.hide();
            return c
        },
        _createCellForRecordField: function(a, b) {
            var c = d.apply(this, arguments);
            this.options.fields[b].visibility == "hidden" && c.hide();
            return c
        },
        changeColumnVisibility: function(a, b) {
            this._changeColumnVisibilityInternal(a, b);
            this._normalizeColumnWidths();
            this.options.saveUserPreferences && this._saveColumnSettings()
        },
        _changeColumnVisibilityInternal: function(a, b) {
            var c = this._columnList.indexOf(a);
            if (c < 0)
                this._logWarn('Column "' + a + '" does not exist in fields!');
            else if (["visible", 
            "hidden", "fixed"].indexOf(b) < 0)
                this._logWarn('Visibility value is not valid: "' + b + '"! Options are: visible, hidden, fixed.');
            else {
                var d = this.options.fields[a];
                if (d.visibility != b) {
                    d.visibility != "hidden" && b == "hidden" ? this._$table.find("th:nth-child(" + (c + 1) + "),td:nth-child(" + (c + 1) + ")").hide() : d.visibility == "hidden" && b != "hidden" && this._$table.find("th:nth-child(" + (c + 1) + "),td:nth-child(" + (c + 1) + ")").show().css("display", "table-cell");
                    d.visibility = b
                }
            }
        },
        _createColumnSelection: function() {
            var a = this;
            this._$columnSelectionDiv = 
            c('<div class="jtable-column-selection-container"></div>').appendTo(a._$mainContainer);
            this._$table.find("thead").bind("contextmenu", function(b) {
                if (a.options.columnSelectable) {
                    b.preventDefault();
                    c('<div class="jtable-contextmenu-overlay"></div>').click(function() {
                        c(this).remove();
                        a._$columnSelectionDiv.hide()
                    }).bind("contextmenu", function() {
                        return false
                    }).appendTo(document.body);
                    a._fillColumnSelection();
                    a._$columnSelectionDiv.css({
                        left: b.pageX,
                        top: b.pageY
                    }).show()
                }
            })
        },
        _fillColumnSelection: function() {
            for (var a = 
            this, b = c('<ul class="jtable-column-select-list"></ul>'), d = 0; d < this._columnList.length; d++) {
                var f = this._columnList[d]
                  , j = this.options.fields[f]
                  , k = c("<li></li>").appendTo(b)
                  , k = c('<label for="' + f + '"><span>' + (j.title || f) + "</span><label>").appendTo(k)
                  , f = c('<input type="checkbox" name="' + f + '">').prependTo(k).click(function() {
                    var b = c(this)
                      , e = b.attr("name");
                    a.options.fields[e].visibility != "fixed" && a.changeColumnVisibility(e, b.is(":checked") ? "visible" : "hidden")
                });
                j.visibility != "hidden" && f.attr("checked", "checked");
                j.visibility == "fixed" && f.attr("disabled", "disabled")
            }
            this._$columnSelectionDiv.html(b)
        },
        _createColumnResizeBar: function() {
            this._$columnResizeBar = c('<div class="jtable-column-resize-bar" style="position: fixed; top: -100px; left: -100px;"></div>').appendTo(this._$mainContainer).hide()
        },
        _makeColumnResizable: function(a) {
            var b = this;
            c('<div class="jtable-column-resize-handler"></div>').appendTo(a.find(".jtable-column-header-container")).mousedown(function(d) {
                d.preventDefault();
                d.stopPropagation();
                var f = 
                a.nextAll("th.jtable-column-header:visible:first");
                if (f.length) {
                    b._currentResizeArgs = {
                        currentColumnStartWidth: a.outerWidth(),
                        minWidth: 10,
                        maxWidth: a.outerWidth() + f.outerWidth() - 10,
                        mouseStartX: d.pageX,
                        minResizeX: function() {
                            return this.mouseStartX - (this.currentColumnStartWidth - this.minWidth)
                        },
                        maxResizeX: function() {
                            return this.mouseStartX + (this.maxWidth - this.currentColumnStartWidth)
                        }
                    };
                    var j = function(a) {
                        if (b._currentResizeArgs) {
                            a = b._normalizeNumber(a.pageX, b._currentResizeArgs.minResizeX(), b._currentResizeArgs.maxResizeX());
                            b._$columnResizeBar.css("left", a + "px")
                        }
                    }
                      , k = function(d) {
                        if (b._currentResizeArgs) {
                            c(document).unbind("mousemove", j);
                            c(document).unbind("mouseup", k);
                            b._$columnResizeBar.hide();
                            var d = b._normalizeNumber(b._currentResizeArgs.currentColumnStartWidth + (d.pageX - b._currentResizeArgs.mouseStartX), b._currentResizeArgs.minWidth, b._currentResizeArgs.maxWidth)
                              , g = f.outerWidth() + (b._currentResizeArgs.currentColumnStartWidth - d)
                              , l = a.data("width-in-percent") / b._currentResizeArgs.currentColumnStartWidth;
                            a.data("width-in-percent", 
                            d * l);
                            f.data("width-in-percent", g * l);
                            a.css("width", a.data("width-in-percent") + "%");
                            f.css("width", f.data("width-in-percent") + "%");
                            b._normalizeColumnWidths();
                            b._currentResizeArgs = null ;
                            b.options.saveUserPreferences && b._saveColumnSettings()
                        }
                    }
                    ;
                    b._$columnResizeBar.show().css({
                        top: a.offset().top + "px",
                        left: d.pageX + "px",
                        height: b._$table.height() + "px"
                    });
                    c(document).bind("mousemove", j);
                    c(document).bind("mouseup", k)
                }
            })
        },
        _normalizeColumnWidths: function() {
            var a = this._$table.find("thead th.jtable-command-column-header").data("width-in-percent", 
            1).css("width", "1%")
              , b = this._$table.find("thead th.jtable-column-header")
              , d = 0;
            b.each(function() {
                var a = c(this);
                a.is(":visible") && (d = d + a.outerWidth())
            });
            var f = {}
              , j = 100 - a.length;
            b.each(function() {
                var a = c(this);
                if (a.is(":visible")) {
                    var b = a.data("fieldName")
                      , a = a.outerWidth() * j / d;
                    f[b] = a
                }
            });
            b.each(function() {
                var a = c(this);
                if (a.is(":visible")) {
                    var b = a.data("fieldName");
                    a.data("width-in-percent", f[b]).css("width", f[b] + "%")
                }
            })
        },
        _saveColumnSettings: function() {
            var a = this
              , b = "";
            this._$table.find("thead th.jtable-column-header").each(function() {
                var d = 
                c(this)
                  , f = d.data("fieldName")
                  , d = d.data("width-in-percent");
                b = b + (f + "=" + a.options.fields[f].visibility + ";" + d) + "|"
            });
            this._setCookie("column-settings", b.substr(0, b.length - 1));
            this._logDebug("saved")
        },
        _loadColumnSettings: function() {
            var a = this
              , b = this._getCookie("column-settings");
            if (b) {
                var d = {};
                c.each(b.split("|"), function(a, b) {
                    var c = b.split("=")
                      , e = c[0]
                      , c = c[1].split(";");
                    d[e] = {
                        columnVisibility: c[0],
                        columnWidth: c[1]
                    }
                });
                this._$table.find("thead th.jtable-column-header").each(function() {
                    var b = c(this)
                      , e = b.data("fieldName")
                      , 
                    f = a.options.fields[e];
                    if (d[e]) {
                        f.visibility != "fixed" && a._changeColumnVisibilityInternal(e, d[e].columnVisibility);
                        b.data("width-in-percent", d[e].columnWidth).css("width", d[e].columnWidth + "%")
                    }
                });
                this._logDebug("loaded")
            }
        },
        _setCookie: function(a, b) {
            var a = this._cookieKeyPrefix + a
              , c = new Date;
            c.setDate(c.getDate() + 30);
            document.cookie = encodeURIComponent(a) + "=" + encodeURIComponent(b) + "; expires=" + c.toUTCString()
        },
        _getCookie: function(a) {
            for (var a = this._cookieKeyPrefix + a, b = document.cookie.split("; "), c = 0; c < b.length; c++)
                if (b[c]) {
                    var d = 
                    b[c].split("=");
                    if (d.length == 2 && decodeURIComponent(d[0]) === a)
                        return decodeURIComponent(d[1] || "")
                }
            return null 
        },
        _generateCookieKeyPrefix: function() {
            var a = "";
            this.options.tableId && (a = a + this.options.tableId + "#");
            a = a + this._columnList.join("$") + "#c" + this._$table.find("thead th").length;
            var b = 0;
            if (a.length != 0)
                for (var c = 0; c < a.length; c++)
                    var d = a.charCodeAt(c)
                      , b = (b << 5) - b + d
                      , b = b & b;
            return "jtable#" + b
        }
    })
})(jQuery);
(function(c) {
    var b = c.hik.jtable.prototype._removeRowsFromTable;
    c.extend(!0, c.hik.jtable.prototype, {
        options: {
            openChildAsAccordion: !1
        },
        openChildTable: function(a, b, d) {
            var i = this;
            if (b.showCloseButton == void 0)
                b.showCloseButton = true;
            if (b.showCloseButton && !b.closeRequested)
                b.closeRequested = function() {
                    i.closeChildTable(a)
                }
                ;
            i.options.openChildAsAccordion && a.siblings().each(function() {
                i.closeChildTable(c(this))
            });
            i.closeChildTable(a, function() {
                var e = i.getChildRow(a).find("td").empty()
                  , g = c('<div class="jtable-child-table-container"></div>').appendTo(e);
                e.data("childTable", g);
                g.jtable(b);
                i.openChildRow(a);
                g.hide().slideDown("fast", function() {
                    d && d({
                        childTable: g
                    })
                })
            })
        },
        closeChildTable: function(a, b) {
            var c = this
              , i = this.getChildRow(a).find("td")
              , e = i.data("childTable");
            if (e) {
                i.data("childTable", null );
                e.slideUp("fast", function() {
                    e.jtable("destroy");
                    e.remove();
                    c.closeChildRow(a);
                    b && b()
                })
            } else
                b && b()
        },
        isChildRowOpen: function(a) {
            return this.getChildRow(a).is(":visible")
        },
        getChildRow: function(a) {
            return a.data("childRow") || this._createChildRow(a)
        },
        openChildRow: function(a) {
            a = 
            this.getChildRow(a);
            a.is(":visible") || a.show();
            return a
        },
        closeChildRow: function(a) {
            a = this.getChildRow(a);
            a.is(":visible") && a.hide()
        },
        _removeRowsFromTable: function(a, f) {
            var d = this;
            f == "deleted" && a.each(function() {
                var a = c(this)
                  , b = a.data("childRow");
                if (b) {
                    d.closeChildTable(a);
                    b.remove()
                }
            });
            b.apply(this, arguments)
        },
        _createChildRow: function(a) {
            var b = this._findRowIndex(a)
              , d = this._$table.find("thead th").length
              , d = c('<tr class="jtable-child-row"></tr>').append('<td colspan="' + d + '"></td>');
            this._addRowToTable(d, 
            b + 1);
            a.data("childRow", d);
            d.hide();
            return d
        }
    })
})(jQuery);