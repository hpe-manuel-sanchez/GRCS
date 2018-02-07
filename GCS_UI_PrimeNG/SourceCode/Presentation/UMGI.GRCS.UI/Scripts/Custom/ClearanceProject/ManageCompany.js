var manageCompany = (function () {

    var init = function (settings) {
        manageCompany.settings = {
            textBoxNameId: '#name',
            textBoxcountryId: '#country',
            popUpId: '#SearchedCompanyList',
            autocomplete:
                {
                    loadingClass: "ui-autocomplete-loading",
                    inputClass: "ui-autocomplete-input",
                    context: "#tabs-3",
                    appendTo: "#SearchedCompanyList"
                },
            buttons:
                {
                    add: $('#btnAdd'),
                    searchCompany: $('#btnSearchComp'),
                    addFreeHand: $('#btnAddFreeHand'),
                    cancel: $('#btnCancel'),
                    addFreeHandComp: $('#btnAddFreeHandComp'),
                    reset: $('#btnReset')
                }
        }
        $.extend(manageCompany.settings, settings);

        functions.loadwatermark();

        functions.autocomplete(manageCompany.settings.textBoxNameId);
        functions.autocomplete(manageCompany.settings.textBoxcountryId);

        manageCompany.settings.buttons.add.click(events.add);
        manageCompany.settings.buttons.searchCompany.click(events.searchCompany);
        manageCompany.settings.buttons.addFreeHand.click(events.addFreeHand);
        manageCompany.settings.buttons.cancel.click(events.cancel);
        manageCompany.settings.buttons.addFreeHandComp.click(events.addFreeHandComp);
        manageCompany.settings.buttons.reset.click(events.reset);
    }

    var functions = {
        autocomplete: function (id) {
            var minLength = 3;
            $(id).autocomplete({
                appendTo: manageCompany.settings.autocomplete.appendTo,
                source: function (request, response) {
                    var url = $(id).attr("data-autocomplete-source-manual");

                    $(id).addClass(manageCompany.settings.autocomplete.loadingClass);
                    $(id).removeClass(manageCompany.settings.autocomplete.inputClass);

                    var terms = {
                        term: request.term,
                        searchBy: $(id).attr("SearchFor")
                    };

                    $.getJSON(url, terms, function (data) {
                        if (GCS.utilities.functions.isTheDialogPopupOpened(manageCompany.settings.popUpId)) {
                            response(data);
                        };
                        $(id).removeClass(manageCompany.settings.autocomplete.loading);
                        $(id).addClass(manageCompany.settings.autocomplete.inputClass);
                    });
                },
                minLength: minLength,
                select: function (event, ui) {
                    $(id).val(ui.item.value);
                }
            });
        },
        addThirdPartyCompany: function () {

            $("#divErrorMessageOnThirdPartyPage").text("");
            $("#divErrorMessageOnThirdPartyPage").hide();
            $('#divErrorMessageOnThirdPartyPage').removeClass('warning');

            $("#tblThirdParty").show();
            $("#rowAddedCompanyResult").show();
            $("#hdnCompId").val(selectedCompId);

            $("#txtThirdPartyCompId").val(selectedCompId);
            if (selectedCompName.length > 0) {
                $("#tdCompName").html(selectedCompName);
                $("#hdnCompName").val(selectedCompName);
                $("#name1").val(selectedCompName);
                $("#txtThirdPartyCompName").val(selectedCompName);
            }
            else {
                $("#tdCompName").html("");
            }
            if (selectedCompIsacCode != null && selectedCompIsacCode.length > 0) {
                $("#tdIsacCode").html(selectedCompIsacCode);
                $("#txtThirdPartyCompISAC").val(selectedCompIsacCode);
                $("#hdnThirdPartyComapnyISAC").val(selectedCompIsacCode);
            }
            else {
                $("#tdIsacCode").html("");
            }
            if (selectedCountryName.length > 0) {
                $("#tdCountry").html(selectedCountryName);
                $("#country1").val(selectedCountryName);
                $("#txtThirdPartyCompCountry").val(selectedCountryName);
                $("#hdnThirdPartyComapnyCountry").val(selectedCountryName);
            }
            else {
                $("#tdCountry").html("");
            }
            $('#SearchedCompanyList').dialog('close');
            $('#SearchedCompanyList')[0].innerHTML = "";
        },
        loadwatermark: function () {
            $.watermark.options.className = 'watermark';

            $("#name").watermark(watermarkCompanyName);
            $("#isaccode").watermark(watermarkIsacCode);
            $("#country").watermark(watermarkCountry);
        },
        reset: function () {
            $('#name').val('');
            $('#isaccode').val('');
            $('#country').val('');
        }
    }

    var events = {
        add: function () {

            if (selectedCompId != null && selectedCompId != "") {
                if (parseInt($("#hdnCompId").val()) > 0) {
                    var objWarningDialog = $('<div id="Confirm"></div>')
                .html('<p>This action will remove the previously added company. Do you want to continue?</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: 'Confirm',
                    show: 'clip',
                    hide: 'clip',
                    width: 300
                });

                    objWarningDialog.dialog('open');
                    objWarningDialog.dialog({
                        buttons:
                    {
                        'Yes': function (e) {
                            $(this).dialog('close');
                            functions.addThirdPartyCompany();
                        },
                        'No': function () {
                            $(this).dialog('close');
                            $("#SearchedCompanyList").dialog("close");
                        }
                    }
                    });
                }
                else {
                    $('#SearchedCompanyList').dialog('close');
                    $('#SearchedCompanyList')[0].innerHTML = "";
                    functions.addThirdPartyCompany();
                }
            }
            else {
                $("#divErrorMessageOnThirdPartyPage").text(thirdPartyMandatorySelection);
                $('#divErrorMessageOnThirdPartyPage').addClass('warning');
                $("#divErrorMessageOnThirdPartyPage").show();
            }
        },
        searchCompany: function () {

            $("#name").autocomplete("close");
            $("#country").autocomplete("close");

            if (($('#name').val() == null || $('#name').val() == "") && ($('#isaccode').val() == null || $('#isaccode').val() == "") && ($('#country').val() == null || $('#country').val() == "")) {
                $("#divErrorMessageOnThirdPartyPage").text(mandatorySearchCriteria);
                $('#divErrorMessageOnThirdPartyPage').addClass('warning');
                $("#divErrorMessageOnThirdPartyPage").show();

                $('#name').focus();
                return false;
            }
            $("#manageCmpnyNoRecordsMsg").hide();
            $("#rowCompanyName").hide();
            $('#searchCompanyResults').show();
            $("#divErrorMessageOnThirdPartyPage").text("");
            $("#divErrorMessageOnThirdPartyPage").hide();
            $('#divErrorMessageOnThirdPartyPage').removeClass('warning');
            pagingCount = $('#ddlCompanyPaging').val();
            selectedCompId = "";
            var recordsCount = 0;
            functions.loadwatermark();
            searchlist1 = "";
            var company1 = $('#name').val();
            var isacCode1 = $('#isaccode').val();
            var country1 = $('#country').val();
            if (company1 != '') { searchlist1 = searchlist1 + company1 + " / "; }
            if (isacCode1 != '') { searchlist1 = searchlist1 + isacCode1 + " / "; }
            if (country1 != '') { searchlist1 = searchlist1 + country1; }
            if (country1 == '') {
                searchlist1 = searchlist1.substring(0, searchlist1.length - 1);
            }

            LoadSearchJtable(pagingCount);
        },
        addFreeHand: function () {

            $("#rowCompanyName").show();
            $("#btnAdd").removeAttr("disabled");
            $("#btnAddFreeHandComp").removeAttr("disabled");
            $("#btnAddFreeHand").attr("disabled", "disabled");
        },
        cancel: function () {

            $('#SearchedCompanyList').dialog('close');
            $('#SearchedCompanyList')[0].innerHTML = "";
            document.getElementById('name1').value = '';
            document.getElementById('isaccode1').value = '';
            document.getElementById('country1').value = '';
            Loadwatermark();
        },
        addFreeHandComp: function () {

            freeHandCompName = $('#freeHandCompanyName').val();

            if (freeHandCompName != null && freeHandCompName.trim() != "") {

                $("#tblThirdParty").show();
                $("#rowAddedCompanyResult").show();
                $("#divErrorMessageOnThirdPartyPage").text("");
                $("#divErrorMessageOnThirdPartyPage").hide();
                $('#divErrorMessageOnThirdPartyPage').removeClass('warning');

                var img = '<img src="/GCS/images/project.png"  style="border:0;" title="Project" />';
                $("#tdCompName").html(img + " " + freeHandCompName);
                $("#tdIsacCode").html("");
                $("#tdCountry").html(" ");
                $("#hdnCompId").val("");
                //For UC-055 Added Start here
                $("#hdnThirdPartyComapnyISAC").val("");
                $("#hdnThirdPartyComapnyCountry").val("");
                //For UC-055 Added ends here
                $("#hdnCompName").val(freeHandCompName);
                $("#txtThirdPartyCompId").val(-1);
                $("#imgFreehandCompany").show();
                $("#txtThirdPartyCompName").val(freeHandCompName);

                $('#SearchedCompanyList').dialog('close');
                $('#SearchedCompanyList')[0].innerHTML = "";
                document.getElementById('name1').value = '';
                document.getElementById('isaccode1').value = '';
                document.getElementById('country1').value = '';

            }
            else {
                $("#divErrorMessageOnThirdPartyPage").text("Enter Frehand Company Name");
                $('#divErrorMessageOnThirdPartyPage').addClass('warning');
                $("#divErrorMessageOnThirdPartyPage").show();
            }
        },
        reset: function () {
            functions.reset();
            functions.loadwatermark();
        }
    }

    return {
        _init: init
    }
})();

$(document).ready(function () {
    manageCompany._init({})
});
