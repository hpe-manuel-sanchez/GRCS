/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

//en-us(English (United States))
var contractTabMessage = {
    contractId: "On Save Contract Info will be Generated",
    lostRightsOnYes: "Please provide past date for",
    lostRightsDateBold: " Lost Rights Date",
    lostRightsOnNo: "Please provide future date for",
    okButton: "Ok",
    searchParentTitle: "Search Parent Contract",
    clearanceCompArtistVal: "Please select Clearance Admin Company to add Artist",
    searchPCNotice: "Search P/C Notice Company",
    searchGlobalAddr: "Global Address List",
    territorialRights: "Territorial Rights",
    childCtrctValid: "Child Contract cannot be created",
    templateValidation: "Please provide values in the mandatory fields to save the Contract template",
    templateName: "Please Enter Contract Template Name",
    templateTitle: "Enter Contract Template Name",
    saveTemplateTitle: "Save Template",
    saveTemplateName: "Template Name",
    lostRightsValid: "Please select Lost Rights Reason",
    saveContractValid: "Please provide values in mandatory fields to save the contract",
    saveContractCnfrm: "Please check whether all contract information is filled",
    exploitPeriod: "Please select Consent period in Secondary Exploitation tab",
    exploitHoldPeriod: "Please enter HoldBack period in Secondary Exploitation tab",
    rightsProp: "Do you want to propagate the rights data from parent contract to this contract?",
    endDateGreater: "Please Provide Commencement Date earlier than End Date",
    narrowSearch: "Auto Search returned more than 200 records. Please narrow your search.",
    splitDealTitle: "Please Confirm"}
};

var hiddenTemplateName = '';
var hiddenTemplateNameClear = hiddenTemplateName;
//Create dialog
var objDialog = $('<div id="ContractTab"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            width: 500
        });

var saveContractTemplateDialog = $('<div id="ContractTemplateSave"></div>')
.html('<p>' + messageCommon.onLoading + '</p>')
.dialog({
    autoOpen: false,
    modal: true,
    title: messageCommon.warningTitle,
    show: 'clip',
    hide: 'clip',
    width: 500,
    buttons: {
        'Ok': function () {
            $(this).dialog('close');
            $(this).remove();
        },
        close: function () {
            $(this).remove(); // ensures any form variables are reset.
        }
    }
});

//Create dialog
var objDialog1 = $('<div id="Dummy"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.infoTitle,
            show: 'clip',
            hide: 'clip',
            width: "36.6%",
            position: [(($(window).width() - (($(window).width() * 36.6) / 100)) / 2), (($(window).height() - 161) / 2)]
        });

function contractvalidate() {
    var testcontract = $('#Contract_ContractId').val();
    var rowCount = $('#AddParent').length;
    if (testcontract == 0 && rowCount <= 1) {
        testcontract = 1;
        $.post('/GCS/Contract/AddParentContract', { contractId: testcontract }, function (data) {
            $('#divContractTab').html(data);
            reSize();
        })
                    .error(function () {
                        objDialog.html('<p>' + messageCommon.error + '</p>');
                        //Open Dialog
                        objDialog.dialog('open', { title: messageCommon.warningTitle });
                    });
        return false;
    }

    return false;
}
var height;
$(document).ready(function () {
    //To Make the Textbox readonly
    var pcCompany = $("#Contract_PcNoticeCountryCompanyId");
    //    .nextAll().filter(function () {
    //        return $(this).find('input').length > 0;
    //    }).find('input');
    $(pcCompany).attr('readOnly', 'readonly');
    $("#Contract_PcNoticeCountryCompanyId option").each(function () {
        $(this).attr('title', $(this).text());
    });

    //Navigation Inconsistency Handled for edit contract
    if ($('#Contract_ContractId').val() > 0) {
        var sourcePage = document.referrer;
        var page = sourcePage.substring(sourcePage.length - 3, sourcePage.length);
        if (page == "eue") {
            if ($('#Search_For_Contract').length > 0) {
                $('#Search_For_Contract').html('<a href="/GCS/Contract/ContractMaintenanceWorkQueue">Contract Maintenance WorkQueue</a>');
            }
            if ($('#Contract_Maintenance_WorkQueue').length > 0) {
                $('#Contract_Maintenance_WorkQueue').html('<a href="/GCS/Contract/ContractMaintenanceWorkQueue">Contract Maintenance WorkQueue</a>');
            }
        }
        if (page == "act") {
            if ($('#Search_For_Contract').length > 0) {
                $('#Search_For_Contract').html('<a href="/GCS/Contract/SearchContract/">Search For Contract</a>');
            }
            if ($('#Contract_Maintenance_WorkQueue').length > 0) {
                $('#Contract_Maintenance_WorkQueue').html('<a href="/GCS/Contract/SearchContract/">Search For Contract</a>');
            }
        }
    }

    //Focusing an element at a particular position(commented to focus on CAC)
    $('.scrollContract').scroll(function () {
        var height = $('.scrollContract').scrollTop();

        if (height == 0)
            $("#Contract_ClearanceCompanyCountry").focus();
        //$('#linkParent').focus();
        //        if (height > 50 && height < 100)
        //            $('#openArtistPopup').focus();
        //        if (height > 100 && height < 350)
        //            $('#linkTerritorialRights').focus();
        //        if (height > 350 && height < 450)
        //            $('#linkPCNoticeCompany').focus();
        //        if (height > 450 && height < 500)
        //           $('#addressBook').focus();
    });

    /***************************** Text Area Resize - By UID Team *************************/

    /*  $("textarea").css("overflow", "hidden");
    $("textarea").val('');

    $("textarea").keypress(function () {
    var textLength = $(this).val().length;

    if (textLength > 59) {
    var height = $(this).height();
    $(this).css('height', 6 + (height));
    }
    });*/

    /***************************** End of Text Area Resize - By UID Team *************************/

    resizeContract();

    //3 - Other Duration
    if ($("#Contract_RightsPeriodId").val() == "3") {
        $("#Contract_RightsPeriodId").val(3);
        $("#Contract_RightsExpiryRule").removeAttr("disabled");
        $("#Contract_RightsExpiryRule").removeClass("disabled");
    }

    if ($("#Contract_ContractId").val() != 0) {
        if ($("#HasChild").val() == 'true') {
            $('#linkParent').attr('disabled', 'disabled');
        }
    }

    //Disabling the lost rights reason by default

    if ($('#Contract_IsLossRightsIndicator').val() != 0 && $('#Contract_RightsExpiryDate').val().length == 0) {
        $('#Contract_LostRightsReasonId').attr('disabled', true);
        $('#Contract_LostRightsReasonId').val('0');
    }
    else {
        $('#Contract_LostRightsReasonId').attr('disabled', false);
    }

    //Enabling on change of the date or lost rights indicator
    if ($('#Contract_IsLossRightsIndicator').val() == 0 || $('#Contract_RightsExpiryDate').val().length > 0) {
        $('#Contract_LostRightsReasonId').attr('disabled', false);
    }

    //Handling onchange
    $('#Contract_IsLossRightsIndicator').change(function () {
        LostRightsIndicator();
    });

    $('#Contract_RightsExpiryDate').change(function () {
        RightsExpiry();
    });

    var user;
    if (document.URL.indexOf('ViewContract') >= 0) {
        user = $("#UserRoleName").val();
        if (user == "ReadOnlyUser") {
            ReadOnlyUser();
        }
        else if (user == "ReadOnlyBasicUser") {
            ReadOnlyBasicUser();
        }
        else if (user == "PowerUser") {
            PowerUser();
        }
        else {
            EditorReviewerUser();
        }
        // make read only artist name and id field
        $("#Contract_ArtistName").attr("readOnly", "readOnly");
        $("#Contract_ArtistId").attr("readOnly", "readOnly");
        $("#Contract_ArtistName").attr('disabled', 'disabled');
        $("#Contract_ArtistId").attr("disabled", "disabled");
    }

    if (document.URL.indexOf('editRequestForm=Maintenance') >= 0) {
        $("#btnContractSave").hide();
        $("#btnContTemplate").val('Save Template');
    }

    $('.linkToRepertoire').hide();
    $('#btnUnlinkContract').hide();
    if (document.URL.indexOf('EditContract') >= 0) {
        user = $("#UserRoleName").val();
        if (user == "PowerUser") {
            PowerUser();
        }
        else {
            EditorReviewerUser();
        }
        var workflow = $('#ddlWorkflowStatus').find('option:first').text();
        if (workflow == "Approved")
            $('.linkToRepertoire').show();
        $('#btnUnlinkContract').show();
        // make read only artist name and id field
        $("#Contract_ArtistName").attr("readOnly", "readOnly");
        $("#Contract_ArtistId").attr("readOnly", "readOnly");
        $("#Contract_ArtistName").attr('disabled', 'disabled');
        $("#Contract_ArtistId").attr("disabled", "disabled");
    }

    if ($('#unlink').length != 0) {
        $('#unlink').click(function () {
            var formValues = GetFormValues();
            formValues = formValues + '&flag=Unlink';

            $.post('/GCS/Contract/AddParentContract', formValues, function (data) {
                $('#divContractTab').html(data);
                reSize();
                if ($('.childContract').length > 0)
                    $('.childContract').hide();
                DecodeTerritorial();
            })
            .error(function () {
                objDialog.html('<p>' + messageCommon.error + '</p>');
                //Open Dialog
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });
        });
    }

    if ($('.UnlinkSplit').length != 0) {
        $('.UnlinkSplit').each(function () {
            $(this).click(function () {
                var unlink = $(this).attr("id");
                var formValues = GetFormValues();
                formValues = formValues + '&flag=' + unlink;

                $.post('/GCS/Contract/UnlinkSplitDeal', formValues, function (data) {
                    $('#divContractTab').html(data);
                    reSize();
                    DecodeTerritorial();
                })
            .error(function () {
                objDialog.html('<p>' + messageCommon.error + '</p>');
                //Open Dialog
                objDialog.dialog('open', { title: messageCommon.warningTitle });
            });
            });
        });
    }

    //Accordion style collapse/expand
    $('#accordion .head').click(function (e) {
        e.preventDefault();
        $(this).next().toggle();
        // $(this)._toggleClass('iconBottom');
        $(this).find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    if ($('#ContractTabModel_Contract_ContractId').html() != "0" && $('#ContractTabModel_Contract_ContractId').html().substring(9, 11) != "-1") {
        $('#ContractTabModel_Contract_ContractCommencementDate').html($('#Contract_ContractCommencementDate').val());
        $('#ContractTabModel_Contract_ArtistName').html($('#Contract_ArtistName').val());
        $('#ContractTabModel_Contract_ContractingParty').html($('#Contract_ContractingParty').val());
    }

    if ($('#ContractTabModel_Contract_ContractId').html() == "0" || $('#ContractTabModel_Contract_ContractId').html() == "-1" || $('#ContractTabModel_Contract_ContractId').html() == contractTabMessage.contractId) {
        $('#hiddenDescription').hide();
        $('#hiddenContractId').show();
    } else {
        $('#hiddenDescription').show();
        $('#hiddenContractId').hide();
    }

    //Datepicker functionality
    $(".datefield").css("width", "100px");
    $(".datefield").datepicker({ showOn: 'both', buttonImage: "/GCS/Images/Calender_Icon_img.png", buttonImageOnly: true, dateFormat: "dd M yy", changeMonth: true, changeYear: true, yearRange: '1900:2100' });

    if ($('#ContractTabModel_Contract_ContractId').html() == "0" || $('#ContractTabModel_Contract_ContractId').html() == "-1") {
        $('#ContractTabModel_Contract_ContractId').html(contractTabMessage.contractId);
    }

    if ($('#Contract_ContractId').length > 0) {
        if ($('#Contract_ContractId').val() != 0) {
            $('#ContractTabModel_Contract_ContractId').html(pad($('#Contract_ContractId').val(), 11));
            if ($('#ContractTabModel_Contract_ContractId').html().substring(9, 11) == "-1")
                ($('#ContractTabModel_Contract_ContractId').html(contractTabMessage.contractId));
            if ($('#ParentContract').val() == -1 || $('#ParentContract').val() == 0) {
                if ($('.childContract').length > 0) {
                    $('.childContract').show();
                }
            } else {
                if ($('.childContract').length > 0) {
                    $('.childContract').hide();
                }
            }
        }
        else {
            ($('#ContractTabModel_Contract_ContractId').html(contractTabMessage.contractId));
            if ($('.childContract').length > 0) {
                $('.childContract').hide();
            }
        }
    }

    //    if ($('#ContractTabModel_Contract_ContractId').html() == "On Save Contract Info will be Generated" )
    //    {
    //        $('#hiddenDescription').html('On Save Contract Info will be Generated');
    //    }
    //&& (childButton.ContractTabModel.Contract.ParentContractId == -1 || childButton.ContractTabModel.Contract.ParentContractId == 0) )

    //Autocomplete Clearance Admin Company - Country
    //Validation - Set UMG signing company based on the Clearance Admin Company
    var target = $("#Contract_ClearanceCompanyCountry");

    target.autocomplete({
        source: function (request, response) {
            $.getJSON(target.attr("data-autocomplete-source-manual"),
                 { term: request.term },
                          function (data) {
                              if (data.length >= 200) {
                                  objDialog.html(contractTabMessage.narrowSearch);
                                  objDialog.dialog({
                                      buttons: {
                                          'Ok': function () { $(this).dialog('close'); },
                                          close: function () {
                                              $(this).remove(); // ensures any form variables are reset.
                                          }
                                      }
                                  });
                                  //Open Dialog
                                  objDialog.dialog('open');
                              }
                              response($.map(data, function (item) { return item; }));
                          });
        }
    , minLength: 2,
        select: function (event, ui) {
            $("#Contract_ClearanceCompanyCountryId").val(ui.item.id);
            $("#Contract_UmgSigningCompanyId").val(ui.item.id);
            $("#ClearanceCountryId").val(ui.item.addValue);
            $("#ClearanceCompanyId").val(ui.item.id);
            var indexNo = $("#Contract_ClearanceCompanyCountry").val().indexOf('-');
            $("#Contract_UmgSigningCompany").val($("#Contract_ClearanceCompanyCountry").val());

            if (ui.item.hasApproval == '0') {
                $("#ContractTabModel_Contract_WorkflowStatusId option:[text='Approved']").remove();
            }
            else {
                if ($("#ContractTabModel_Contract_WorkflowStatusId option:[text='Approved']").length == 0) {
                    $('#ContractTabModel_Contract_WorkflowStatusId').append('<option value="3">Approved</option>');
                }
            }

            if (ui.item.isPowerUser == '1') {
                PowerUser();
            }
            else {
                enableAllFields();
            }
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#Contract_ClearanceCompanyCountry").val("");
            }
            else if (ui.item != null && $("#Contract_ClearanceCompanyCountry").val() != ui.item.value) {
                $("#Contract_ClearanceCompanyCountry").val("");
            }
            else if (ui.item != null && $(ui.item.addValue != null)) {
                $("#ClearanceCountryId").val(ui.item.addValue);
                $("#ClearanceCompanyId").val(ui.item.id);
            }

            $("#Contract_UmgSigningCompany").val($("#Contract_ClearanceCompanyCountry").val());
        }
    });

    var target1 = $("#Contract_UmgSigningCompany");

    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"), minLength: 2,
        select: function (event, ui) {
            $("#Contract_UmgSigningCompanyId").val(ui.item.id);
        },
        change: function (event, ui) {
            var indexNo = $("#Contract_ClearanceCompanyCountry").val().indexOf('-');

            if (ui.item == null && $("#Contract_UmgSigningCompany").val() != $("#Contract_ClearanceCompanyCountry").val().substring(0, indexNo)) {
                $("#Contract_UmgSigningCompany").val("");
            }
            else if (ui.item != null && $("#Contract_UmgSigningCompany").val() != ui.item.value && $("#Contract_UmgSigningCompany").val() != $("#Contract_ClearanceCompanyCountry").val().substring(0, indexNo)) {
                $("#Contract_UmgSigningCompany").val("");
            }
        }
    });

    //Autocomplete Combobox
    //    if ($("#Contract_PcNoticeCountryCompanyId").length > 0) {
    //        $("#Contract_PcNoticeCountryCompanyId").combobox();
    //    }

    //Autocomplete Combobox
    if ($("#Contract_ContractDescription").length > 0)
        $("#Contract_ContractDescription").combobox();

    //OnActive Roster Validation
    if ($("#Contract_ContractEndDate").length > 0) {
        $("#Contract_ContractEndDate").change(function () {
            var dateString = $("#Contract_ContractEndDate").val(); // date string
            var actualDate = new Date(dateString); // convert to actual date
            var newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate() + 1); // create new increased date
            var newDateString = ('0' + newDate.getDate()).substr(-2) + ' ' + newDate.toDateString().substr(4, 3) + ' ' + newDate.getFullYear();
            if (new Date(newDateString) > new Date()) {
                $("#Contract_IsContractInActiveRoster").val(2);
                $("#Contract_IsContractInActiveRoster").removeAttr("disabled");
            }
            else {
                $("#Contract_IsContractInActiveRoster").val(1);
                $("#Contract_IsContractInActiveRoster").attr("disabled", true);
            }
        });
    }

    if ($('#ContractTabModel_Contract_ContractId').html() != contractTabMessage.contractId) {
        $('#hiddenDescription').show();
        $('#hiddenContractId').hide();
    }

    //Validation for end date greater than start date
    $("#Contract_ContractEndDate").change(function () {
        if (new Date($("#Contract_ContractEndDate").val()) < new Date($("#Contract_ContractCommencementDate").val())) {
            objDialog.html(contractTabMessage.endDateGreater);
            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            objDialog.dialog('open');
            $("#Contract_ContractEndDate").val('');
        }
    });

    $("#Contract_ContractCommencementDate").change(function () {
        if (new Date($("#Contract_ContractEndDate").val()) < new Date($("#Contract_ContractCommencementDate").val())) {
            objDialog.html(contractTabMessage.endDateGreater);
            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            objDialog.dialog('open');
            $("#Contract_ContractEndDate").val('');
        }
    });

    //ActiveForMarketing validation
    if ($("#Contract_IsActiveForMarketing").length > 0) {
        $("#Contract_ContractEndDate").change(function () {
            if ($("#Contract_IsActiveForMarketingChanged").val() != "1") {
                if ($("#Contract_IsContractInActiveRoster").val() == "1") {
                    $("#Contract_IsActiveForMarketing").val(1);
                }
                else
                    $("#Contract_IsActiveForMarketing").val(0);
            }
        });
    }

    //ActiveForMarketing validation on manual change
    if ($("#Contract_IsActiveForMarketing").length > 0) {
        $("#Contract_IsContractInActiveRoster").change(function () {
            if ($("#Contract_IsActiveForMarketingChanged").val() != "1") {
                if ($("#Contract_IsContractInActiveRoster").val() == "1") {
                    $("#Contract_IsActiveForMarketing").val(1);
                }
                else
                    $("#Contract_IsActiveForMarketing").val(0);
            }
        });
    }

    //For manual change
    $("#Contract_IsActiveForMarketing").change(function () {
        if (($("#Contract_IsContractInActiveRoster").val() == "1" && $("#Contract_IsActiveForMarketing").val() == "0") || ($("#Contract_IsContractInActiveRoster").val() == "2" && $("#Contract_IsActiveForMarketing").val() == "1")) {
            $("#Contract_IsActiveForMarketingChanged").val(1);
        }
        else {
            $("#Contract_IsActiveForMarketingChanged").val(0);
        }
    });

    //Rights Exception Applied Disable
    if ($("#Contract_ContractId").val() == 0 || $("#Contract_ContractId").val() == '') {
        $("#Contract_IsRightsExceptionApplied").attr("disabled", true);
    }
    else
        $("#Contract_IsRightsExceptionApplied").removeAttr("disabled");

    //Rights Expiry rule validation
    $("#Contract_RightsPeriodId").change(function () {
        if ($("#Contract_RightsPeriodId").val() == "3") {
            $("#Contract_RightsExpiryRule").removeAttr("disabled");
            $("#Contract_RightsExpiryRule").removeClass("disabled");
            $("#Contract_RightsExpiryRule").val("");
        }
        else {
            $("#Contract_RightsExpiryRule").attr("disabled", true);
            $("#Contract_RightsExpiryRule").addClass("disabled");
            $("#Contract_RightsExpiryRule").val("");
        }
    });

    //Lost rights indicator
    $("#Contract_RightsExpiryDate").change(function () {
        //Enable Lost rights reason dropdown 0- Yes
        var newDate;
        var dateString;
        var actualDate;
        var newDateString;
        if ($("#Contract_IsLossRightsIndicator").val() == 0) {
            dateString = $("#Contract_RightsExpiryDate").val(); // date string
            actualDate = new Date(dateString); // convert to actual date
            newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate() + 1); // create new increased date
            newDateString = ('0' + newDate.getDate()).substr(-2) + ' ' + newDate.toDateString().substr(4, 3) + ' ' + newDate.getFullYear();
            if (new Date() < new Date(newDateString)) {
                $("#Contract_RightsExpiryDate").val('');
                if ($("#Contract_IsLossRightsIndicator").val() == 1) {
                    $('#Contract_LostRightsReasonId').attr('disabled', true);
                    $('#Contract_LostRightsReasonId').val('0');
                }
                objDialog.html('<p>' + contractTabMessage.lostRightsOnYes + '<b>' + contractTabMessage.lostRightsDateBold + '</b></p>');
                objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
                //Open Dialog
                objDialog.dialog('open');
            }
        }
        else {
            dateString = $("#Contract_RightsExpiryDate").val(); // date string
            actualDate = new Date(dateString); // convert to actual date
            newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate() + 1); // create new increased date
            newDateString = ('0' + newDate.getDate()).substr(-2) + ' ' + newDate.toDateString().substr(4, 3) + ' ' + newDate.getFullYear();
            if (new Date() > new Date(newDateString)) {
                $("#Contract_RightsExpiryDate").val('');
                if ($("#Contract_IsLossRightsIndicator").val() == 1) {
                    $('#Contract_LostRightsReasonId').attr('disabled', true);
                    $('#Contract_LostRightsReasonId').val('0');
                }
                objDialog.html('<p>' + contractTabMessage.lostRightsOnNo + '<b>' + contractTabMessage.lostRightsDateBold + '</b></p>');
                objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
                //Open Dialog
                objDialog.dialog('open');
            }
        }
    });

    //Lost Right Date Clear on Change
    $("#Contract_IsLossRightsIndicator").change(function () {
        if ($("#Contract_RightsExpiryDate").val() != '' || $("#Contract_RightsExpiryDate").val() != null) {
            $("#Contract_RightsExpiryDate").val('');
        }
    });

    //Legal Rights Required validation with Rights type
    $("#Contract_RightsTypeId").change(function () {
        if ($("#Contract_IsRightsTypeChanged").val() == '0') {//Do only if it is not  changed manually
            if ($("#Contract_RightsTypeId").val() == '3') {//Non-UMG
                $("#Contract_IsLegalRightsReviewRequired").val('1');
            }
            else {
                $("#Contract_IsLegalRightsReviewRequired").val('0');
            }
        }
    });

    $("#Contract_IsLegalRightsReviewRequired").change(function () {
        $("#Contract_IsRightsTypeChanged").val('1');
    });

    //link click
    $('#linkParent').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var objParentDialog = $('<div id="parentContractPopup"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: messageCommon.infoTitle,

                    width: "98%",
                    position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 15],
                    close: function () {
                        $(this).remove(); // ensures any form variables are reset.
                    }
                });

        loadTable();
        objParentDialog.html('<p>' + messageCommon.onLoading + '</p>');

        //Load partial view
        objParentDialog.load('/GCS/Contract/SearchParentContract', "",
        function (responseText, textStatus) {
            loadSearchParent();
            if (textStatus == 'error') {
                objParentDialog.html('<p>' + messageCommon.error + '</p>');
            }
        });

        objParentDialog.dialog('option', { title: contractTabMessage.searchParentTitle });
        //Open Dialog
        objParentDialog.dialog('open');

        //return false;
    });

    //link click
    $('#linkSplitDeal').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        SearchContractPopup();
    });

    //link for Artist Click

    $('#openArtistPopup').click(function (e) {
        e.preventDefault();

        var art = $('#Contract_ClearanceCompanyCountry').val();
        if (art == '') {
            objDialog.html('<p>' + contractTabMessage.clearanceCompArtistVal + '</p>');
            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });

            //open dialog
            objDialog.dialog('open');
            return false;
        }
        else {
            var objArtistPopup = $('<div id="ArtistContractPopup"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: messageCommon.infoTitle,
                    show: 'clip',
                    hide: 'clip',
                    width: "98%",
                    position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50],
                    close: function () { $(this).remove(); }
                });
            objArtistPopup.html('<p>' + messageCommon.onLoading + '</p>');

            //Load partial view
            objArtistPopup.load('/GCS/Contract/SearchForArtist', "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                objArtistPopup.html('<p>' + messageCommon.error + '</p>');
            }
        }
        );

            objArtistPopup.dialog('option', { title: messageCommon.searchArtistTitle });
            //Open Dialog
            objArtistPopup.dialog('open');
        }
        return false;
    });

    //link for PCNoticeCompany Click
    $('#linkPCNoticeCompany').click(function (e) {
        e.preventDefault();

        var objPcPopup = $('<div id="PcPopup"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: messageCommon.infoTitle,
                    show: 'clip',
                    hide: 'clip',
                    width: "98%",
                    position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50],
                    close: function () {
                        $(this).remove(); // ensures any form variables are reset.
                    }
                });

        objPcPopup.html('<p>' + messageCommon.onLoading + '</p>');

        //Load partial view
        objPcPopup.load('/GCS/Contract/SearchPcNoticeCompany', "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                objPcPopup.html('<p>' + messageCommon.error + '</p>');
            }
        }
        );

        objPcPopup.dialog('option', { title: contractTabMessage.searchPCNotice });
        //Open Dialog
        objPcPopup.dialog('open');
    });

    //link for GlobalAddressList Click

    $('#addressBook').click(function (e) {
        e.preventDefault();

        var objGlobalPopup = $('<div id="GlobalPopup"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: messageCommon.infoTitle,
                    show: 'clip',
                    hide: 'clip',
                    width: "98%",
                    position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 15],
                    close: function () {
                        $(this).remove(); // ensures any form variables are reset.
                    }
                });

        objGlobalPopup.html('<p>' + messageCommon.onLoading + '</p>');

        //Load partial view
        objGlobalPopup.load('/GCS/Contract/GlobalAddressList', "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                objGlobalPopup.html('<p>' + messageCommon.error + '</p>');
            }
        }
        );
        objGlobalPopup.dialog('option', { title: contractTabMessage.searchGlobalAddr });
        //Open Dialog
        objGlobalPopup.dialog('open');
    });

    //Link Territorial Rights Click
    $('#linkTerritorialRights').click(function (e) {
        e.preventDefault();
        e.stopPropagation();

        var objTerritorialPopup = $('<div id="Terrirory"></div>')
                    .html('<p>' + messageCommon.onLoading + '</p>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        title: messageCommon.infoTitle,
                        show: 'clip',
                        hide: 'clip',
                        width: "98%",
                        position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 15],
                        close: function () {
                            $(this).remove(); // ensures any form variables are reset.
                        }
                    });

        objTerritorialPopup.html('<p>' + messageCommon.onLoading + '</p>');

        var contractid = $('#Contract_ContractId').val();

        //Load partial view
        objTerritorialPopup.load('/GCS/Contract/TerritorialRights/' + contractid, "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                objTerritorialPopup.html('<p>' + messageCommon.error + '</p>');
            }
        }
        );

        objTerritorialPopup.dialog('option', { title: contractTabMessage.territorialRights });
        //Open Dialog
        objTerritorialPopup.dialog('open');
    });

    //decode
    DecodeTerritorial();

    $('.childContract').unbind();
    $('.childContract').click(function () {
        var testcontract = $('#Contract_ContractId').val();
        if (testcontract != 0 || $('#AddParent').length <= 1) {
            var objCreateChildContractDialog = $('<div id="CreateChildContract"></div>')
             .html('<p>' + contractTabMessage.rightsProp + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: contractTabMessage.splitDealTitle,
                    show: 'clip',
                    hide: 'clip',
                    width: 300
                });
            objCreateChildContractDialog.dialog('open');
            objCreateChildContractDialog.dialog({
                buttons:
                {
                    'Yes': function () {
                        window.location.href = "/GCS/Contract/LinkParentContract?" + "contractid=" + testcontract + "&flag=Rights";
                        $(this).dialog('close');
                    },
                    'No': function () {
                        $.post('/GCS/Contract/AddParentContract', { flag: "ChildContract", LinkedContractId: testcontract }, function (data) {
                            $('#divContractTab').html(data);
                            reSize();
                            $('#ContractTabModel_Contract_WorkflowStatus').val($('#ddlWorkflowStatus').find('option:first').text());
                            ReloadRightsTabs();
                            ReloadSecondaryTabs();
                        }).error(function () {
                            objDialog.html('<p>' + messageCommon.error + '</p>');
                            //Open Dialog
                            objDialog.dialog('open', { title: messageCommon.warningTitle });
                        });
                        $(this).dialog('close');
                        //return false;
                    },
                    'Cancel': function () {
                        $(this).dialog('close');
                    }
                }
            });
            //            $.post('/GCS/Contract/AddParentContract', { flag: "ChildContract", LinkedContractId: testcontract }, function (data) {
            //                $('#divContractTab').html(data);
            //                reSize();
            //                $('#ContractTabModel_Contract_WorkflowStatus').val($('#ddlWorkflowStatus').find('option:first').text());
            //                ReloadRightsTabs();
            //                ReloadSecondaryTabs();
            //                var rights = setTimeout(test, 1);
            //                if (rights) {
            //                    window.location.href = "/GCS/Contract/LinkParentContract?" + "contractid=" + testcontract + "&flag=Rights";
            //                }
            //                else {
            //                    return true;
            //                }

            //            })
            //                .error(function () {
            //                    objDialog.html('<p>' + messageCommon.error + '</p>');
            //                    //Open Dialog
            //                    objDialog.dialog('open', { title: messageCommon.warningTitle });
            //                });

            $('#ContractTabModel_Contract_ContractId').html(contractTabMessage.contractId);
            $('.editor-field input:text').val('');
            $('.editor-field select').val(0);
            $('.editor-field textarea').val('');
            $("input[type=date][name$=Contract.ContractCommencementDate]").val('');
            $("input[type=date][name$=Contract.ContractEndDate]").val('');
            $("input[type=date][name$=Contract.RightsExpiryDate]").val('');

            $('#ContractTabModel_Contract_WorkflowStatusId').val(1);
            $('#ContractTabModel_Contract_WorkflowStatusId').text(1);
        }
        else {
            // alert('Child Contract cannot be created');
            objDialog.html(contractTabMessage.childCtrctValid);
            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            objDialog.dialog('open');
        }
    });

    var contractDescption = $("#Contract_ContractDescription").nextAll().filter(function () {
        return $(this).find('input').length > 0;
    }).find('input');

    contractDescption.val($("#ddlContractDescription").val());

    var pcNoticeCompany = $("#Contract_PcNoticeCountryCompanyId");
    //    .nextAll().filter(function () {
    //        return $(this).find('input').length > 0;
    //    }).find('input');

    pcNoticeCompany.val($("#ddlPCNoticeCountryCompany").val());

    $(pcNoticeCompany).attr('readOnly', 'readonly');

    //Save Contract Template
    $('.saveContractTemplate').click(function () {
        $("#tabs").tabs('option', 'selected');
        if ($('#Contract_ClearanceCompanyCountry').val() == null || $('#Contract_ClearanceCompanyCountry').val() == '') {
            $('#Contract_ClearanceCompanyCountry').addClass('input-validation-error');
            objDialog.html(contractTabMessage.templateValidation);
            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            objDialog.dialog('open');
            return false;
        }
        else {
            $('#Contract_ClearanceCompanyCountry').removeClass('input-validation-error');
        }

        var formVal = $("#ContractTabForm").serialize();

        formVal = SetTerritorial(formVal);

        $('#ContractTabForm input[disabled]').each(function () {
            formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        if ($('#Contract_LocalContractRefNumber').val() != null)
            formVal = formVal + '&Contract.LocalContractRefNumber=' + encodeURIComponent($('#Contract_LocalContractRefNumber').val());

        var parentcontract = $('#AddParent').length;
        if (parentcontract == 1) {
            var t = $('#ParentContract').val();
            formVal = formVal + '&Contract.ParentContractId=' + encodeURIComponent(t);
        }
        formVal = formVal + '&Contract.WorkflowStatusId=' + encodeURIComponent($("#ContractTabModel_Contract_WorkflowStatusId").val());
        formVal = formVal + '&Contract.WorkflowStatus=' + encodeURIComponent($("#ContractTabModel_Contract_WorkflowStatus").val());
        formVal = formVal + '&Contract.WorkFlowIdentifier=' + encodeURIComponent($("#ContractTabModel_Contract_WorkFlowIdentifier").val());

        var contractDesc = $("#Contract_ContractDescription").nextAll().filter(function () {
            return $(this).find('input').length > 0;
        }).find('input');

        formVal = formVal.replace("Contract.ContractDescription", "Contract.ContractDescription123");

        formVal = formVal + '&Contract.ContractDescription=' + encodeURIComponent(contractDesc.val());

        var pcItem = $("#Contract_PcNoticeCountryCompanyId");
        //        .nextAll().filter(function () {
        //            return $(this).find('input').length > 0;
        //        }).find('input');

        if (pcItem.val() != 0)
            formVal = formVal.replace("Contract.PcNoticeCountryCompanyId", "Contract.PcNoticeCompanyId");

        var pcText = $("#Contract_PcNoticeCountryCompanyId option:selected").text();
        formVal = formVal + '&Contract.PcNoticeCountryCompany=' + encodeURIComponent(pcText);

        formVal = formVal + '&' + $("#RightsForm").serialize();

        $('#RightsForm select[disabled]').each(function () {
            formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $('#RightsForm input[disabled]').each(function () {
            formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        formVal = formVal + '&' + $("#SecExpForm").serialize();
        $('#SecExpForm select[disabled]').each(function () {
            formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        $('#SecExpForm input[disabled]').each(function () {
            formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
        });

        // gets the template id and name from the contract form

        var hiddenTemplateId = $('#templateId').val();
        var templateId = $('#templateId').val();
        var requestForm = $('#EditRequestForm').val();
        hiddenTemplateName = $('#templateName').val();
        hiddenTemplateNameClear = hiddenTemplateName;
        var company = $('#ClearanceCompanyId').val();
        var country = $('#ClearanceCountryId').val();

        // if request form is maintenance, open dialog box with save and save as template options
        if (requestForm == 'Maintenance') {
            formVal = formVal + '&Contract.ClearanceCompanyId=' + company;
            formVal = formVal + '&Contract.ClearanceCountryId=' + country;
            saveContractTemplateDialog.dialog();
            saveContractTemplateDialog.html(
                            '<p/>  <input type=\'radio\' id=\'save\' name="saveGroup" checked=\'checked\'/> ' + ' Save Existing Template' +
                                '<p>' +
                                    '<input type=\'radio\' id=\'saveNew\' name="saveGroup" />' + ' Save as New Template' +
                                        '<p>' +
                                            contractTabMessage.saveTemplateName +
                                                '</p> <input type=\'text\' name="txtBoxTemplateName" maxlength=100 style="font-weight:bold;" size="6" id="txtBoxTemplate_Name" width=\'100px\' value='
                                                    + hiddenTemplateName + ' disabled=\'disabled\'></input>');
            saveContractTemplateDialog.dialog('option',
                            {
                                title: contractTabMessage.saveTemplateTitle,
                                width: '500px',
                                close: function () {
                                    // $(this).remove(); // ensures any form variables are reset.
                                },
                                buttons:
                                    {
                                        'Save': function () {
                                            var optionCheckAttr = $("input[@id=saveGroup]:checked").attr('id');
                                            hiddenTemplateName = $('input:text[name=txtBoxTemplateName]').val();
                                            if (optionCheckAttr == 'saveNew') {
                                                formVal = formVal.replace("&Contract.TemplateId", "&Contract.TemplateId123");
                                                formVal = formVal.replace("&isNewTemplate", "&isNewTemplate123");
                                                formVal = formVal + '&Contract.TemplateId=' + -1;
                                                formVal = formVal + '&isNewTemplate=' + true;
                                            }
                                            else {
                                                formVal = formVal + '&Contract.TemplateId=' + hiddenTemplateId;
                                                formVal = formVal + '&isNewTemplate=' + false;
                                            }

                                            if (hiddenTemplateName == '') {
                                                $(this).find('input.txtBoxTemplateName').addClass('input-validation-error');
                                                displayDialog("Warning", 'Please enter Template Name');
                                                return false;
                                            }
                                            $(this).find('input.txtBoxTemplateName').removeClass('input-validation-error');
                                            var contractPopup = $(this);
                                            formVal = formVal.replace("&Contract.TemplateName", "&Contract.TemplateName123");

                                            formVal = formVal + '&Contract.TemplateName=' + hiddenTemplateName;
                                            //--
                                            $.post('/GCS/Contract/SaveContractTemplate', formVal, function (data) {
                                                var status = $(data).find('#contractTemplateStatus').val();
                                                var temp = status.substring(status.length - 1, status.length);
                                                if (temp.length > 0 && temp == "!") {
                                                    $('input:text[name=txtBoxTemplateName]').val(hiddenTemplateName);
                                                } else {
                                                    contractPopup.dialog('close');
                                                    contractPopup.remove();
                                                }
                                                $('#divContractTab').html(data);
                                                reSize();
                                                DecodeTerritorial();
                                            });

                                            return false;
                                        }
                                    }
                            });

            saveContractTemplateDialog.dialog('option', {
                open: function () {
                    $(this).find('input').focus();

                    saveContractTemplateDialog.unbind("click").click(function () {
                        var optionCheckAttr = $("input[@id=saveGroup]:checked").attr('id');
                        hiddenTemplateNameClear = $('input:text[name=txtBoxTemplateName]').val();
                        if (optionCheckAttr == 'saveNew') {
                            $('input:text[name=txtBoxTemplateName]').prop("disabled", false);
                            $('input:text[name=txtBoxTemplateName]').val('');
                            $('#txtBoxTemplate_Name').focus();
                        } else {
                            $('input:text[name=txtBoxTemplateName]').prop("disabled", true);
                            hiddenTemplateName = $('#templateName').val();;
                            $('input:text[name=txtBoxTemplateName]').val(hiddenTemplateName);
                            hiddenTemplateId = templateId;
                        }
                    });
                }
            });
            saveContractTemplateDialog.dialog('open');
        }
            // else if request form is contract tab, open dialog box with save as new template option
        else {
            formVal = formVal.replace("&Contract.ClearanceCompanyId", "&Contract.ClearanceCompanyId123");
            formVal = formVal.replace("&Contract.ClearanceCountryId", "&Contract.ClearanceCountryId123");
            formVal = formVal + '&Contract.ClearanceCompanyId=' + company;
            formVal = formVal + '&Contract.ClearanceCountryId=' + country;
            objDialog1.html(
                            '<p>' + contractTabMessage.templateName + '</p><input type=\'text\' maxlength=100 width=\'100px\'></input>');
            objDialog1.dialog('option', {
                title: contractTabMessage.templateTitle, width: '500px', buttons: {
                    'Save': function () {
                        var value = $(this).find('input').val();
                        if (value == '') {
                            $(this).find('input').addClass('input-validation-error');
                            return false;
                        }
                        $(this).find('input').removeClass('input-validation-error');
                        var ContractPopup = $(this);
                        formVal = formVal.replace("&Contract.TemplateName", "&Contract.TemplateName123");
                        formVal = formVal.replace("&Contract.TemplateId", "&Contract.TemplateId123");
                        formVal = formVal.replace("&isNewTemplate", "&isNewTemplate123");
                        formVal = formVal + '&Contract.TemplateName=' + value;
                        formVal = formVal + '&Contract.TemplateId=' + -1;
                        formVal = formVal + '&isNewTemplate=' + true;
                        //--
                        $.post('/GCS/Contract/SaveContractTemplate', formVal, function (data) {
                            var status = $(data).find('#contractTemplateStatus').val();
                            var temp = status.substring(status.length - 1, status.length);
                            if (temp.length > 0 && temp == "!") {
                                $('input:text[name=txtBoxTemplateName]').val(hiddenTemplateName);
                            } else {
                                ContractPopup.dialog('close');
                                ContractPopup.remove();
                            }
                            $('#divContractTab').html(data);
                            reSize();
                            DecodeTerritorial();
                        });

                        return false;
                    }
                }
            });

            objDialog1.dialog('option', {
                open: function () {
                    $(this).find('input').focus();
                }
            });
            objDialog1.dialog('open');
        }
        return false;
    });

    $('.linkToRepertoire').click(function () {
        var contractid = $('#Contract_ContractId').val();
        window.location.href = "/GCS/Project/SearchRepertoire/" + contractid;
    });

    $("#ContractTabModel_Contract_WorkflowStatus").val($("#ContractTabModel_Contract_WorkflowStatusId option:selected").html());

    $("#ContractTabModel_Contract_WorkflowStatusId").change(function () {
        $("#ContractTabModel_Contract_WorkflowStatus").val($("#ContractTabModel_Contract_WorkflowStatusId option:selected").html());
    });

    var selectedvalue = $('#ContractTabModel_Contract_WorkflowStatusId').val();
    $('#ContractTabModel_Contract_WorkflowStatusId').html($('#ddlWorkflowStatus').html());
    $('#ContractTabModel_Contract_WorkflowStatusId').val(selectedvalue);

    $('#saveNew').unbind("click").click(function () {
        var rte = $('#txtBoxTemplate_Name').val();
        alert(rte);
    });
});

function validate() {
    //Mandatory Lost Rights reason   - Should be called in save
    if ($("#Contract_RightsExpiryDate").val() != '' && $("#Contract_LostRightsReasonId").val() == 0) {
        objDialog.html('<p>' + contractTabMessage.lostRightsValid + '</p>');
        //Open Dialog
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        objDialog.dialog('open');
        return false;
    }

    if (!specialCharCheckForParty()) {
        return false;
    }
    if (!specialCharCheckForRefNo()) {
        return false;
    }
    //Mandatory Fields - Workflow status, Contract Status, Artist or Contracting party, Clearance Admin Company
    //Workflow status, Contract Status will have default values so no check needed

    if ($('#ContractTabModel_Contract_WorkflowStatusId').val() == 1) {//Validation on Data Entry status
        if (MandatoryValidateForClearanceComp() == false || MandatoryValidateForArtistorParty() == false) {
            if (MandatoryValidateForArtistorParty() == false)
                objDialog.html('<p>' + contractTabMessage.saveContractValid + '</p>');
            else
                objDialog.html('<p>' + contractTabMessage.saveContractValid + '</p>');

            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            objDialog.dialog('open');
            return false;
        }
    }
    else {//Edit Contract validation/Work flow status change validation
        if (editValidate() == false || MandatoryValidateForClearanceComp() == false || MandatoryValidateForArtistorParty() == false) {
            objDialog.html('<p>' + contractTabMessage.saveContractValid + '</p>');

            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            objDialog.dialog('open');
            return false;
        }
    }

    $("#Contract_LocalContractRefNumber").val($("#Contract_LocalContractRefNumber").val().toString().substring(0, 20));

    //Secondary Exploitation validation
    if (validateSecondaryExploitation() == false)
        return false;

    // Rights and Restriction

    if (validateRightsandRestriction() == false)
        return false;

    if (validateDigitalRestrictionCombinationforsave() == false)
        return false;

    //    //Ask confirmation if not in data entry------Commented as UAT Bug raised
    //    if ($('#ContractTabModel_Contract_WorkflowStatusId').val() != 1) {
    //        objDialog.html('<p>' + contractTabMessage.saveContractCnfrm + '</p>');
    //        objDialog.dialog({ buttons: { 'Ok': function () { saveContract(); $(this).dialog('close'); }, 'Cancel': function () { $(this).dialog('close'); } } });
    //        //Open Dialog
    //        objDialog.dialog('open');
    //        return false;
    //    }

    saveContract();
    return false;
}

function specialCharCheckForParty() {
    var strngContractRefNo = $('#Contract_ContractingParty').val();
    if (VerifyHasSpecialCharacters(strngContractRefNo)) {
        $('#Contract_ContractingParty').addClass('input-validation-error');
        return false;
    }
    else {
        $('#Contract_ContractingParty').removeClass('input-validation-error');
        return true;
    }
}

function specialCharCheckForRefNo() {
    var strngContractRefNo = $('#Contract_LocalContractRefNumber').val();

    if (VerifyHasSpecialCharacters(strngContractRefNo)) {
        $('#Contract_LocalContractRefNumber').addClass('input-validation-error');
        return false;
    }
    else {
        $('#Contract_LocalContractRefNumber').removeClass('input-validation-error');
        return true;
    }
}

function VerifyHasSpecialCharacters(strngRef) {
    var count = 0;
    var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
    for (var i = 0; i < strngRef.length; i++) {
        if (iChars.indexOf(strngRef.charAt(i)) != -1) {
            count++;
            return true;
        }
    }
    return false;
}
function saveContract() {
    //Compose form values
    var formVal = $("#ContractTabForm").serialize();

    $('#ContractTabForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    var parentcontract = $('#AddParent').length;
    if (parentcontract == 1) {
        var t = $('#ParentContract').val();
        formVal = formVal + '&Contract.ParentContractId=' + encodeURIComponent(t);
    }
    formVal = formVal + '&Contract.WorkflowStatusId=' + encodeURIComponent($("#ContractTabModel_Contract_WorkflowStatusId").val());
    formVal = formVal + '&Contract.WorkflowStatus=' + encodeURIComponent($("#ContractTabModel_Contract_WorkflowStatus").val());
    formVal = formVal + '&Contract.WorkFlowIdentifier=' + encodeURIComponent($("#ContractTabModel_Contract_WorkFlowIdentifier").val());

    var contractDesc = $("#Contract_ContractDescription").nextAll().filter(function () {
        return $(this).find('input').length > 0;
    }).find('input');

    formVal = formVal.replace("Contract.ContractDescription", "Contract.ContractDescription123");
    // added email recipients in form collection to retain in page after contract save
    formVal = formVal + '&Contract.EmailReceipients=' + encodeURIComponent($("#Contract_EmailReceipients").val());
    formVal = formVal + '&Contract.ContractDescription=' + encodeURIComponent(contractDesc.val());

    var pcItem = $("#Contract_PcNoticeCountryCompanyId");
    //    .nextAll().filter(function () {
    //        return $(this).find('input').length > 0;
    //    }).find('input');

    if (pcItem.val() != 0)
        formVal = formVal.replace("Contract.PcNoticeCountryCompanyId", "Contract.PcNoticeCompanyId");

    var pcText = $("#Contract_PcNoticeCountryCompanyId option:selected").text();
    formVal = formVal + '&Contract.PcNoticeCountryCompany=' + encodeURIComponent(pcText);

    formVal = formVal + '&' + $("#RightsForm").serialize();

    $('#RightsForm select[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    $('#RightsForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    formVal = formVal + '&' + $("#SecExpForm").serialize();

    $('#SecExpForm select[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    $('#SecExpForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    $.post('/GCS/Contract/SaveContract', formVal, function (data) {
        $('#divContractTab').html(data);
        reSize();
        DecodeTerritorial();

        if ($('#Contract_ContractId').val() != 0) {
            $('#ContractTabModel_Contract_ContractId').html(pad($('#Contract_ContractId').val(), 11));
        } else {
            ($('#ContractTabModel_Contract_ContractId').html(contractTabMessage.contractId));
        }
    })
        .error(function () {
            objDialog.html('<p>' + messageCommon.error + '</p>');
            objDialog.dialog('option', { title: messageCommon.warningTitle });
            //Show Dialog
            objDialog.dialog('open');
        });
}

function MandatoryValidateForClearanceComp() {
    if ($('#Contract_ClearanceCompanyCountry').val() == null || $('#Contract_ClearanceCompanyCountry').val() == '') {
        $('#Contract_ClearanceCompanyCountry').addClass('input-validation-error');
        return false;
    }
    else {
        $('#Contract_ClearanceCompanyCountry').removeClass('input-validation-error');
    }

    return true;
}

function MandatoryValidateForArtistorParty() {
    if (($('#Contract_ArtistName').val() == null || $('#Contract_ArtistName').val() == '') && ($('#Contract_ContractingParty').val() == null || $('#Contract_ContractingParty').val() == '')) {
        $('#Contract_ArtistName').addClass('input-validation-error');
        $('#Contract_ContractingParty').addClass('input-validation-error');
        return false;
    }
    else {
        $('#Contract_ArtistName').removeClass('input-validation-error');
        $('#Contract_ContractingParty').removeClass('input-validation-error');
    }

    return true;
}

function validateSecondaryExploitation() {
    var result = true;
    $("select.restrictionList").each(function () {
        var temp = $(this).val();
        if (temp == 2 || temp == 5) {
            var item = $($("select.consentList")[$("select.restrictionList").index(this)]);
            if (item.val() == 0) {
                objDialog.html('<p>' + contractTabMessage.exploitPeriod + '</p>');
                objDialog.dialog('option', { title: messageCommon.warningTitle });
                objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); item.focus(); } }, close: function () { item.focus(); } });
                //Show Dialog
                objDialog.dialog('open');
                result = false;
                return false;
            }
        }
        return true;
    });

    //
    if (result == true) {
        $("select.consentList").each(function () {
            var temp = $(this).val();
            if (temp == 1) {
                var item = $($("input.HoldBack")[$("select.consentList").index(this)]);
                if (item.val() == "") {
                    objDialog.html('<p>' + contractTabMessage.exploitHoldPeriod + '</p>');
                    objDialog.dialog('option', { title: messageCommon.warningTitle });
                    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); item.focus(); } }, close: function () { item.focus(); } });
                    //Show Dialog
                    objDialog.dialog('open');
                    result = false;
                    return false;
                }
            }
            return true;
        });
    }

    return result;
}

function validateRightsandRestriction() {
    var result = true;
    $("select.DigitalrestrictionList").each(function () {
        var temp = $(this).val();
        if (temp == 2 || temp == 5) {
            var item = $($("select.DigitalconsentList")[$("select.DigitalrestrictionList").index(this)]);
            if (item.val() == 0) {
                objDialog.html('<p>' + messageCommon.digitalConsentPrd + '</p>');
                objDialog.dialog('option', { title: messageCommon.warningTitle });
                objDialog.dialog('option', {
                    width: '300px',
                    s: { 'Ok': function () { $(this).dialog('close'); item.focus(); } }, close: function () { item.focus(); }
                });
                //Show Dialog
                objDialog.dialog('open');
                result = false;
                return false;
            }
        }
        return true;
    });
    //
    return result;
}

function validateDigitalRestrictionCombinationforsave() {
    var result = true;
    var digitalarray = new Array();
    $("select.ContentTypeList").each(function () {
        var tempContentType = $(this).val();
        var tempUseType = $($("select.UseTypeList")[$("select.ContentTypeList").index(this)]).val();
        var tempCommercialModel = $($("select.CommercialModelList")[$("select.ContentTypeList").index(this)]);

        if (tempContentType == 1 || tempContentType == 2) {
            if (tempUseType == 1 && tempCommercialModel.val() == 3) {
                popupdialogsave($(this));
                result = false;
                return false;
            }
            else if (tempUseType == 3) {
                popupdialogsave($(this));
                result = false;
                return false;
            }
            else if (tempCommercialModel.val() == 4 && tempUseType != 2) {
                popupdialogsave($(this));
                result = false;
                return false;
            }
        }
        else if (tempContentType == 3) {
            if (tempUseType == 1 && tempCommercialModel.val() == 1) {
                result = true;
                return true;
            }
            else if (tempUseType == 3 && tempCommercialModel.val() == 1) {
                result = true;
                return true;
            }
            else {
                popupdialogsave($(this));
                result = false;
                return false;
            }
        }
        if (tempContentType == 0 || tempUseType == 0 || tempCommercialModel.val() == 0) {
            popupdialogselectvalue($(this));
            result = false;
            return false;
        }

        digitalarray.push(tempContentType + tempUseType + tempCommercialModel.val());
        return false;
    });
    for (var row = 0; row < digitalarray.length; row++) {
        var rowid = arrHasDupes(digitalarray, row);
        if (rowid > 0) {
            var content = $($("select.ContentTypeList")[rowid]);
            popupdialogDuplicatesave(content);
            return false;
        }
    }
    return result;
}

function popupdialogselectvalue(tempContentType) {
    objDialog.html('<p>' + messageCommon.digitalIncomplete + '</p>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); tempContentType.focus(); } }, close: function () { tempContentType.focus(); } });
    objDialog.dialog('open');
}

function popupdialogDuplicatesave(tempContentType) {
    objDialog.html('<p>' + messageCommon.digitalUnique + '</p>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); tempContentType.focus(); } }, close: function () { tempContentType.focus(); } });
    objDialog.dialog('open');
}

function enableTemplateName() {
    //var optionCheckAttr = $("input[@id=saveGroup]:checked").attr('id');
    //if (optionCheckAttr == 'saveNew')
    $('input:text[name=txtBoxTemplateName]').removeAttr("disabled");
    $('input:text[name=txtBoxTemplateName]').attr("disabled", true);
}

function popupdialogsave(tempContentType) {
    objDialog.html(' <p>' + messageCommon.digitalCombination + '</p>');
    //Open Dialog
    objDialog.dialog('option', { title: messageCommon.warningTitle });
    objDialog.dialog('option', { width: '300px', buttons: { 'Ok': function () { $(this).dialog('close'); tempContentType.focus(); } }, close: function () { tempContentType.focus(); } });
    objDialog.dialog('open');
}

function arrHasDupes(arrayValues, id) {                          // finds any duplicate array elements using the fewest possible comparison
    var counter;
    var arrayLength;
    arrayLength = arrayValues.length;
    // to ensure the fewest possible comparisons
    for (counter = 0; counter < arrayLength; counter++) {                        // outer loop uses each item i at 0 through n
        if (counter != id)
            if (arrayValues[id] == arrayValues[counter]) return counter;
    }
    return -1;
}

//Pads leading zeros to ContractId
function pad(number, length) {
    var str = '' + number;
    while (str.length < length) {
        str = '0' + str;
    }

    return str;
}

function GetFormValues() {
    var formVal = $("#ContractTabForm").serialize();

    formVal = SetTerritorial(formVal);

    $('#ContractTabForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });
    var parentcontract = $('#AddParent').length;
    if (parentcontract == 1) {
        var parentContractValue = $('#ParentContract').val();
        formVal = formVal + '&Contract.ParentContractId=' + parentContractValue;
    }

    formVal = formVal + '&Contract.WorkflowStatusId=' + encodeURIComponent($("#ContractTabModel_Contract_WorkflowStatusId").val());
    formVal = formVal + '&Contract.WorkflowStatus=' + encodeURIComponent($("#ContractTabModel_Contract_WorkflowStatus").val());
    formVal = formVal + '&Contract.WorkFlowIdentifier=' + encodeURIComponent($("#ContractTabModel_Contract_WorkFlowIdentifier").val());

    var contractDesc = $("#Contract_ContractDescription").nextAll().filter(function () {
        return $(this).find('input').length > 0;
    }).find('input');

    formVal = formVal.replace("Contract.ContractDescription", "Contract.ContractDescription123");

    formVal = formVal + '&Contract.ContractDescription=' + encodeURIComponent(contractDesc.val());

    var pcItem = $("#Contract_PcNoticeCountryCompanyId");
    //    .nextAll().filter(function () {
    //        return $(this).find('input').length > 0;
    //    }).find('input');

    if (pcItem.val() != 0)
        formVal = formVal.replace("Contract.PcNoticeCountryCompanyId", "Contract.PcNoticeCompanyId");

    var pcText = $("#Contract_PcNoticeCountryCompanyId option:selected").text();
    formVal = formVal + '&Contract.PcNoticeCountryCompany=' + encodeURIComponent(pcText);

    formVal = formVal + '&' + $("#RightsForm").serialize();

    $('#RightsForm select[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    $('#RightsForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    formVal = formVal + '&' + $("#SecExpForm").serialize();
    $('#SecExpForm select[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    $('#SecExpForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' + encodeURIComponent($(this).val());
    });

    return formVal;
}

function editValidate() {
    $('#Contract_ArtistId').attr('disabled', true);
    $('#Contract_ArtistName').attr('disabled', true);

    if ($('#Contract_ContractStatusId').val() == '1')//Signed Contract
    {
        var returnVal = true;
        if (($('#Contract_ContractDescription').parent().find('span input').val() == null || $('#Contract_ContractDescription').parent().find('span input').val() == '')) {
            $('#Contract_ContractDescription').parent().find('span input').attr('style', 'background:#ffeeee;border:1px solid red');
            returnVal = false;
        }
        else {
            $('#Contract_ContractDescription').parent().find('span input').removeAttr('style', 'background:#ffeeee;border:1px solid red');
        }
        if (($("#Contract_ContractCommencementDate").val() == null || $("#Contract_ContractCommencementDate").val() == '')) {
            $('#Contract_ContractCommencementDate').addClass('input-validation-error');
            returnVal = false;
        }
        else {
            $('#Contract_ContractCommencementDate').removeClass('input-validation-error');
        }
    }
    else {
        $('#Contract_ContractDescription').parent().find('span input').removeAttr('style', 'background:#ffeeee;border:1px solid red');
        $('#Contract_ContractCommencementDate').removeClass('input-validation-error');
    }

    if ($("#Contract_RightsTypeId").val() == '1' || $("#Contract_RightsTypeId").val() == '2')//Owned By , Exclusive
    {
        if ($('#Contract_PcNoticeCountryCompanyId').val() == 0) {
            $('#Contract_PcNoticeCountryCompany').attr('style', 'background:#ffeeee;border:1px solid red');
            returnVal = false;
        }
        else {
            $('#Contract_PcNoticeCountryCompany').removeAttr('style', 'background:#ffeeee;border:1px solid red');
        }
    }
    else {
        $('#Contract_PcNoticeCountryCompanyId').removeAttr('style', 'background:#ffeeee;border:1px solid red');
    }

    //Territorial
    if ($('#Contract_TerritorialRightsDefinition').val() == '') {
        $('#Contract_TerritorialRightsDefinition').attr('style', 'background:#ffeeee;border:1px solid red');
        return false;
    } else {
        $('#Contract_TerritorialRightsDefinition').removeAttr('style');
    }

    return returnVal;
}

function DecodeTerritorial() {
    if ($('#Contract_TerritorialRightsDefinition').val() != null && $('#Contract_TerritorialRightsDefinition').val() != '')
        $('#Contract_TerritorialRightsDefinition').val(decodeURIComponent($('#Contract_TerritorialRightsDefinition').val()));
}

function SetTerritorial(formVal) {
    if ($('#Contract_TerritorialRightsDefinition').val() != null) {
        formVal = formVal.replace("Contract.TerritorialRightsDefinition", "Contract.TerritorialRightsDefinition123");
        formVal = formVal + '&' + "Contract.TerritorialRightsDefinition" + '=' + encodeURIComponent($('#Contract_TerritorialRightsDefinition').val());
        return formVal;
    }
}

$(document).ready(function () {
    $("#Contract_ClearanceCompanyCountry").focus();
    //Collapsed Accordion For Split Deal and Parent Contract
    $('#collapsed').ready(function () {
        if ($('#parentCount').val() == undefined) {
            $('#accordion .headParent').next().toggle();
            $('#accordion .headParent').find('a').toggleClass('iconBottom')
        }
        if ($('#splitCount').val() == undefined) {
            $('#accordion .splitCount').next().toggle();
            $('#accordion .headSplit').find('a').toggleClass('iconBottom')
        }
    });

    if ($('#splitCount').val() == 0 || $('#splitCount').html() == "null" || $('#splitCount').html() == null) {
        $('#splitFlag').hide();
    }
    else {
        $('#splitFlag').show();
    }

    $('#splitDealCheck').click(function () {
        SplitDealPopup();
    });
    $('#SearchContractPopup').click(function () {
        SearchContractPopup();
    });
    //To clear the contractId field in the contract searchPopup
    if ($("#contractPopupId").val() == 0)
        $("#contractPopupId").val('');

    //LostRights Message on Yes
    $('#lostRightsValidation').hide();
    if ($('#ContractTabModel_Contract_ContractId').text() != "On Save Contract Info will be Generated")
        if ($('#Contract_IsLossRightsIndicator').val() == 0) {
            $('#lostRightsValidation').show();
        }
    $('#Contract_IsLossRightsIndicator').change(function () {
        if ($('#ContractTabModel_Contract_ContractId').text() != "On Save Contract Info will be Generated") {
            if ($('#Contract_IsLossRightsIndicator').val() == 0) {
                $('#lostRightsValidation').show();
            }
            else {
                $('#lostRightsValidation').hide();
            }
        }
    });

    $(".scrollContract").scroll(function () {
        $(".ui-autocomplete").hide();
        $(".ui-datepicker").hide();
    });

    $(window).bind("resize", resizeHandler);

    function resizeHandler() {
        $("#Contract_ArtistName").css("width", $("#Contract_ContractingParty").width() - 49);

        //Google Fixes

        $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());

        if ($.browser.chrome) {
            $("#Contract_ArtistName").css("width", $("#Contract_ArtistName").width() + 3);
        }
    }

    resizeHandler();

    $('#accordion .headParent').click(function (e) {
        if ($('#parentCount').val() != 0 && $('#parentCount').html() != null) {
            e.preventDefault();
            $(this).next().toggle();
            // $(this)._toggleClass('iconBottom');
            $(this).find('a').toggleClass('iconBottom');
            return false;
        }
    }).next().show();

    $('#accordion .headSplit').click(function (e) {
        if ($('#splitCount').val() != 0 && $('#splitCount').html() != null) {
            e.preventDefault();
            $(this).next().toggle();
            // $(this)._toggleClass('iconBottom');
            $(this).find('a').toggleClass('iconBottom');
            return false;
        }
    }).next().show();
});
function SplitDealPopup() {
    //Split Deal Check

    var objSplitDialog = $('<div id="splitDealPopup"></div>')
   .html('<p>' + messageCommon.onLoading + '</p>')
    .dialog({
        autoOpen: false,
        modal: true,
        title: contractTabMessage.splitDealTitle,
        show: 'clip',
        hide: 'clip',
        width: "98%",
        position: [(($(window).width() - ($(window).width() * 98) / 100) / 2), 25],
        close: function () {
            $(this).remove(); // ensures any form variables are reset.
        }
    });
    //  Load partial view
    objSplitDialog.load('/GCS/Contract/SplitDeal', "",
                        function (responseText, textStatus) {
                            if (textStatus == 'error') {
                                objSplitDialog.html('<p>' + messageCommon.error + '</p>');
                            }
                        }
                        );
    objSplitDialog.dialog('open');
}

function SearchContractPopup() {
    //SearchContractPopup

    var objSearchContractPopupDialog = $('<div id="contractSearchPopup"> </div>')
.html('<p>' + messageCommon.onLoading + '</p>')
.dialog({
    autoOpen: false,
    modal: true,
    title: messageCommon.infoTitle,
    show: 'clip',
    hide: 'clip',
    width: "98%",
    position: [(($(window).width() - ($(window).width() * 98) / 100) / 2), 25],
    close: function () {
        $(this).remove(); // ensures any form variables are reset.
    }
});
    //  Load partial view
    objSearchContractPopupDialog.load('/GCS/Contract/ContractSearchPopup', "",
                    function (responseText, textStatus) {
                        $('#contractSearchPopup').find('.cntrctSearchPopupButtons').hide();
                        $('#contractSearchPopup').find('.splitCntrctSearchPopupButtons').show();
                        $('#contractSearchPopup').find('#sourcePage').val('ContractTab');

                        if (textStatus == 'error') {
                            objSearchContractPopupDialog.html('<p>' + messageCommon.error + '</p>');
                        }
                    }
                    );
    objSearchContractPopupDialog.dialog('open');
}

function enableAllFields() {
    $("input").removeAttr('disabled');
    $("textarea").removeAttr('disabled');
    $("select").removeAttr('disabled');
    //  $("img").attr('disabled', 'disabled');
    $(".ui-combobox-toggle").removeAttr('disabled');
    $("#linkTerritorialRights").removeAttr('disabled');
    $("#linkParent").removeAttr('disabled');
    $("#addressBook").removeAttr('disabled');
    $("#btnAddDigitalTemplate").removeAttr('disabled');
    $("#btnRemoveDigitalTemplate").removeAttr('disabled');
    $("#btnSaveDigitalTemplate").removeAttr('disabled');
    $(".ui-datepicker-trigger").removeAttr('disabled');
    $(".ui-datepicker-trigger").show();
    $('#Contract_ClearanceCompanyCountry').removeAttr('disabled');
    $('#btnContractSave').removeAttr('disabled');
    $('#btnBack').removeAttr('disabled');
    $("#btnNext").removeAttr('disabled');
    $("#btnCancel").removeAttr("disabled");

    $("#Contract_ArtistName").attr('disabled', 'disabled');

    $("#Contract_ArtistId").attr('disabled', 'disabled');
    $("#Contract_RightsExpiryRule").attr('disabled', 'disabled');

    //Rights Exception Applied Disable
    if ($("#Contract_ContractId").val() == 0 || $("#Contract_ContractId").val() == '') {
        $("#Contract_IsRightsExceptionApplied").attr("disabled", true);
    }
    else
        $("#Contract_IsRightsExceptionApplied").removeAttr("disabled");

    //OnActive Roster Validation
    if ($("#Contract_ContractEndDate").length > 0) {
        $("#Contract_ContractEndDate").change(function () {
            var dateString = $("#Contract_ContractEndDate").val(); // date string
            var actualDate = new Date(dateString); // convert to actual date
            var newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate() + 1); // create new increased date
            var newDateString = ('0' + newDate.getDate()).substr(-2) + ' ' + newDate.toDateString().substr(4, 3) + ' ' + newDate.getFullYear();
            if (new Date(newDateString) > new Date()) {
                $("#Contract_IsContractInActiveRoster").val(2);
                $("#Contract_IsContractInActiveRoster").removeAttr("disabled");
            }
            else {
                $("#Contract_IsContractInActiveRoster").val(1);
                $("#Contract_IsContractInActiveRoster").attr("disabled", true);
            }
        });
    }
    LostRightsIndicator();
    RightsExpiry();
}

function GetTemplateFormValues() {
    var formVal = $("#ContractTabForm").serialize();

    $('#ContractTabForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' +

encodeURIComponent($(this).val());
    });

    if ($('#Contract_LocalContractRefNumber').val() != null)
        formVal = formVal + '&Contract.LocalContractRefNumber=' +

encodeURIComponent($('#Contract_LocalContractRefNumber').val());

    var parentcontract = $('#AddParent').length;
    if (parentcontract == 1) {
        var t = $('#ParentContract').val();
        formVal = formVal + '&Contract.ParentContractId=' +

encodeURIComponent(t);
    }
    formVal = formVal + '&Contract.WorkflowStatusId=' +

encodeURIComponent

($("#ContractTabModel_Contract_WorkflowStatusId").val());
    formVal = formVal + '&Contract.WorkflowStatus=' +

encodeURIComponent

($("#ContractTabModel_Contract_WorkflowStatus").val());
    formVal = formVal + '&Contract.WorkFlowIdentifier=' +

encodeURIComponent

($("#ContractTabModel_Contract_WorkFlowIdentifier").val());

    var contractDesc = $("#Contract_ContractDescription").nextAll

().filter(function () {
    return $(this).find('input').length > 0;
}).find('input');

    formVal = formVal.replace("Contract.ContractDescription",

"Contract.ContractDescription123");

    formVal = formVal + '&Contract.ContractDescription=' +

encodeURIComponent(contractDesc.val());

    var pcItem = $("#Contract_PcNoticeCountryCompanyId");
    //    .nextAll

    //().filter(function () {
    //        return $(this).find('input').length > 0;
    //    }).find('input');

    if (pcItem.val() != 0)
        formVal = formVal.replace("Contract.PcNoticeCountryCompanyId", "Contract.PcNoticeCompanyId");

    var pcText = $("#Contract_PcNoticeCountryCompanyId option:selected").text();
    formVal = formVal + '&Contract.PcNoticeCountryCompany=' + encodeURIComponent(pcText);

    formVal = formVal + '&' + $("#RightsForm").serialize();

    $('#RightsForm select[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' +

encodeURIComponent($(this).val());
    });

    $('#RightsForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' +

encodeURIComponent($(this).val());
    });

    formVal = formVal + '&' + $("#SecExpForm").serialize();
    $('#SecExpForm select[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' +

encodeURIComponent($(this).val());
    });

    $('#SecExpForm input[disabled]').each(function () {
        formVal = formVal + '&' + $(this).attr('name') + '=' +

encodeURIComponent($(this).val());
    });

    return formVal;
}

function ReadOnlyUser() {
    $("input").attr('disabled', 'disabled');
    $("textarea").attr('disabled', 'disabled');
    $("select").attr('disabled', 'disabled');
    $("img").attr('disabled', 'disabled');
    $("#ddlContractDescription").attr('disabled', 'disabled');
    $("#ddlPCNoticeCountryCompany").attr('disabled', 'disabled');
    $("#linkTerritorialRights").attr('disabled', 'disabled');
    $("#linkParent").attr('disabled', 'disabled');
    $("#addressBook").attr('disabled', 'disabled');
    $("#btnAddDigitalTemplate").attr('disabled', 'disabled');
    $("#btnRemoveDigitalTemplate").attr('disabled', 'disabled');
    $("#btnSaveDigitalTemplate").attr('disabled', 'disabled');
    $(".ui-datepicker-trigger").attr('disabled', 'disabled');
    $(".ui-datepicker-trigger").hide();
    $("#unlink").attr('disabled', 'disabled');
    $(".ui-combobox-toggle").attr('disabled', 'disabled');

    $('#btnBack').removeAttr('disabled');
    $("#btnNext").removeAttr('disabled');
    $("#btnCancel").removeAttr("disabled");
    $("#Contract_ArtistName").attr('disabled', 'disabled');
    $("#Contract_ArtistId").attr('disabled', 'disabled');
    RightsExpiry();
    LostRightsIndicator();
}

function ReadOnlyBasicUser() {
    $("input").attr('disabled', 'disabled');
    $("textarea").attr('disabled', 'disabled');
    $("select").attr('disabled', 'disabled');
    $("img").attr('disabled', 'disabled');
    $("#ddlPCNoticeCountryCompany").attr('disabled', 'disabled');
    $("#linkTerritorialRights").attr('disabled', 'disabled');
    $("#linkParent").attr('disabled', 'disabled');
    $("#addressBook").attr('disabled', 'disabled');
    $("#btnAddDigitalTemplate").attr('disabled', 'disabled');
    $("#btnRemoveDigitalTemplate").attr('disabled', 'disabled');
    $("#btnSaveDigitalTemplate").attr('disabled', 'disabled');
    $(".ui-datepicker-trigger").attr('disabled', 'disabled');
    $(".ui-datepicker-trigger").hide();
    $("#unlink").attr('disabled', 'disabled');
    $('#Contract_ContractDescription').parents(".leftMainDiv").hide();
    $(".ui-combobox-toggle").attr('disabled', 'disabled');
    $("#Contract_ArtistName").attr('disabled', 'disabled');
    $("#Contract_ArtistId").attr('disabled', 'disabled');
    RightsExpiry();
    LostRightsIndicator();

    if ($("#Contract_IsContractInActiveRoster").val() == 2) {// yes
        $('#Contract_ContractEndDate').parents("#divEndDate").hide();
    }
    if ($("#Contract_IsLossRightsIndicator").val() == 1) { // no 1
        $('#Contract_RightsExpiryDate').parents("#divRightsExpiryDate").hide();
        $('#Contract_RightsExpiryRule').parents(".leftMainDiv").hide();
        $('#Contract_LostRightsReasonId').parents(".leftMainDiv").eq(0).hide();
    }
}

function PowerUser() {
    //Enable disable fields for Power User
    $("input").attr('disabled', 'disabled');
    $("textarea").attr('disabled', 'disabled');
    $("select").attr('disabled', 'disabled');
    $("img").attr('disabled', 'disabled');
    $(".ui-combobox-toggle").attr('disabled', 'disabled');

    $("#ddlContractDescription").attr('disabled', 'disabled');
    $("#ddlPCNoticeCountryCompany").attr('disabled', 'disabled');
    $('#Contract_ArtistName').attr('disabled', 'disabled');
    $("#linkTerritorialRights").attr('disabled', 'disabled');
    $("#linkParent").attr('disabled', 'disabled');
    $("#addressBook").attr('disabled', 'disabled');
    $("#btnAddDigitalTemplate").attr('disabled', 'disabled');
    $("#btnRemoveDigitalTemplate").attr('disabled', 'disabled');
    $("#btnSaveDigitalTemplate").attr('disabled', 'disabled');
    $(".ui-datepicker-trigger").attr('disabled', 'disabled');
    $(".ui-datepicker-trigger").hide();
    $("#unlink").attr('disabled', 'disabled');
    $('#Contract_ClearanceCompanyCountry').removeAttr('disabled');
    $('#btnContractSave').removeAttr('disabled');
    $('#btnBack').removeAttr('disabled');
    $("#btnNext").removeAttr('disabled');
    $("#btnCancel").removeAttr("disabled");
    $("#Contract_ArtistName").attr('disabled', 'disabled');
    $("#Contract_ArtistId").attr('disabled', 'disabled');
    RightsExpiry();
    LostRightsIndicator();
}

function EditorReviewerUser() {
    $("input").removeAttr('disabled');

    var pcNoticeCompany = $("#Contract_PcNoticeCountryCompanyId");
    //    .nextAll().filter(function () {
    //        return $(this).find('input').length > 0;
    //    }).find('input');
    $(pcNoticeCompany).attr('readOnly', 'readonly');
    $('Contract_ArtistName').attr('disabled', 'disabled');

    $("textarea").removeAttr('disabled');
    $("select").removeAttr('disabled');
    $("img").removeAttr('disabled');
    $("#ddlContractDescription").removeAttr('disabled');
    $(".ui-combobox-toggle").removeAttr('disabled');

    $("#ddlPCNoticeCountryCompany").removeAttr('disabled');
    $("#linkTerritorialRights").removeAttr('disabled');
    $("#linkParent").removeAttr('disabled');
    $("#addressBook").removeAttr('disabled');
    $("#btnAddDigitalTemplate").removeAttr('disabled');
    $("#btnRemoveDigitalTemplate").removeAttr('disabled');
    $("#btnSaveDigitalTemplate").removeAttr('disabled');
    $(".ui-datepicker-trigger").removeAttr('disabled');
    $(".ui-datepicker-trigger").show();
    $("#unlink").removeAttr('disabled');
    $('#Contract_ClearanceCompanyCountry').removeAttr('disabled');
    $('#btnContractSave').removeAttr('disabled');
    $('#btnBack').removeAttr('disabled');
    $("#btnNext").removeAttr('disabled');
    $("#btnCancel").removeAttr("disabled");
    $("#Contract_ArtistName").attr('disabled', 'disabled');
    $("#Contract_ArtistId").attr('disabled', 'disabled');
    RightsExpiry();
    LostRightsIndicator();
}

function RightsExpiry() {
    if ($('#Contract_RightsExpiryDate').val().length > 0) {
        $('#Contract_LostRightsReasonId').attr('disabled', false);
    }
    else if ($('#Contract_IsLossRightsIndicator').val() == 1) {
        $('#Contract_LostRightsReasonId').attr('disabled', true);
        $('#Contract_LostRightsReasonId').val('0');
    }
}

function LostRightsIndicator() {
    if ($('#Contract_IsLossRightsIndicator').val() == 0) {
        $('#Contract_LostRightsReasonId').attr('disabled', false);
    }
    else if ($('#Contract_RightsExpiryDate').val().length == 0) {
        $('#Contract_LostRightsReasonId').attr('disabled', true);
        $('#Contract_LostRightsReasonId').val('0');
    }
}

function test() {
    var rights = confirm(contractTabMessage.rightsProp);
    return rights;
}

//Unlink Popup
$(document).ready(function () {
    $('#btnUnlinkContract').click(function (e) {
        e.preventDefault();
        e.stopPropagation();

        var unlinkRepFromContract = $('<div id="UnlinkContractFromRep"></div>')
                              .html('<p>' + messageCommon.onLoading + '</p>')
                              .dialog({
                                  autoOpen: false,
                                  modal: true,
                                  title: messageCommon.infoTitle,
                                  show: 'clip',
                                  hide: 'clip',
                                  width: "98%",
                                  position: [(($(window).width() - ($(window).width() * 98) / 100) / 2), 25],
                                  close: function () {
                                      $(this).remove(); // ensures any form variables are reset.
                                  }
                              });

        //  Load partial view
        unlinkRepFromContract.load('/GCS/Contract/UnlinkRepertoireFromContract');
        // unlinkRepFromContract.html(content);
        unlinkRepFromContract.dialog('open');
        return false;
    });
});