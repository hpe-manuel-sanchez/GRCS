var searchContractPopupMessages = {
    searchValidation: "Please Enter atleast one Field for Search Criteria", artistName: "Artist", adminCompany: "Clearance Admin Co",
    localCntrctNo: "Local Contract Ref No:", commencDate: "Commencement Date", territorialRights: "Territorial Rights", ReleaseLinkTitle: "Link Release To Contract",
    ApprovedStatus: "Please select only Approved Contract To Link", EntryContract: "Please select Contract To Link", EntryContract_RepertoireRightsSearch: "Please select atleast one Contract",
    LinkingFailed: "Contract Linking Failed", PropagationFailed: "PropagationFailed", confirmChangeLink: "Confirm Change Link", ProjectLinkingTitle: "Link Project To Contract",
    changeLinkMsg: "Changing the contract linking for the selected release will change the rights data set against the release. Do you want to continue?"
};

//Initialized Variable for Messages from Resource file
var searchContractForReviewMessages;

$(document).ready(function () {
    //Start Check for Release page 
    setTimeout(function () { $('#Artist_Info_Name').focus() }, 500);
    var hdnReleaseRights = $('#hiddenReleaseRights').val();

    //Start Check for Resource Page
    var hdnResourceRights = $('#hiddenResourceRights').val();

    searchContractForReviewMessages = window.rightsReviewWorkQueueMessages;
    $('.warningContainers').hide();
    $('.cntrctSearchPopupButtons').hide();
    $('.splitCntrctSearchPopupButtons').hide();
    $(".ui-jtable-loading").hide();


    $('#contractSearchPopup, #RightsSearchContract').mousedown(function (e) {
        e.stopImmediatePropagation();
    });

    var pageSize = 25, rowIndex = -1, selectedReviewContracts;

    //For Change/Unlink, multiselect is false.
    var multiSelect = true;
    if (searchContractPopupForChangeOrUnlink == true)
        multiSelect = false;
    else
        multiSelect = true;

    $('#searchContractGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'ContractId ASC',
        selecting: true,
        selectingCheckboxes: true,
        selectOnRowClick: true,
        multiselect: multiSelect,
        actions: {
            listAction: '/GCS/Contract/SearchContracts'
        },
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();

        },
        selectionChanged: function () {
            //Get all selected rows
            var selectedRowsz = $('#searchContractGrid').jtable('selectedRows');
            if (selectedRowsz.length > 0) {
                selectedRowsz.each(function () {
                    var record = $(this).data('record');
                    if (record.ContractStatus == 'Clearance Routing Contract') {
                        $("#searchContractGrid input").removeAttr("checked");
                        $("#searchContractGrid tr").removeClass("jtable-row-selected");
                    }
                });
            }
        },
        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("SearchCount").innerHTML = searchContractForReviewMessages.searchResults + rowIndex + ")";
            $('#btnOk').hide();
            $('#btnCancel').hide();
            $('.jtable .jtable-no-data-row').show();
            $("#searchContractGrid input").removeAttr("checked");
            $("#searchContractGrid tr").removeClass("jtable-row-selected");
            $(".ui-jtable-loading").hide();
            setToolTip(this);
            ResizeTableSize();
            reSizeSearchPage();
            ResizeContractPopUp();
            $('.divRightsWorkQueueCntrctSearchBtns').show();
        },
        fields: {
            'Linked': {
                title: '',
                display: function (test) {
                    $(this).find('input').attr("checked", true);
                    var linkImage;
                    if (test.record.HasRepertoire)
                        linkImage = $('<img src="/GCS/Images/linked_Contract.png" title"Repertoire Linked">');
                    else
                        linkImage = "";
                    return linkImage;


                }
            },
            ArtistName: {
                title: searchContractPopupMessages.artistName
            },

            ContractingParty: {
                title: messageCommon.cntrctParty
            },
            WorkflowStatus: {
                title: messageCommon.workflowStatus
            },
            ContractStatus: {
                title: messageCommon.cntrctStatus
            },
            ContractDescription: {
                title: messageCommon.cntrctDesc
            },
            ClearanceCompanyCountry: {
                title: searchContractPopupMessages.adminCompany
            },
            TerritorialRightsDefinition: {
                title: searchContractPopupMessages.territorialRights,
                display: function (data) {
                    var territorial;
                    if (data.record.TerritorialRightsDefinition == null) {
                        territorial = '';
                        return territorial;
                    } else {
                        return decodeURIComponent(data.record.TerritorialRightsDefinition).replace(/]/g, '');
                    }
                }
            },
            ContractCommencementDate: {
                title: searchContractPopupMessages.commencDate,
                type: 'date',
                displayFormat: $.datepicker.regional[''].dateFormat
            },
            LocalContractRefNumber: {
                title: searchContractPopupMessages.localCntrctNo
            },
            ContractId: {
                title: messageCommon.cntrctId
            }
        },
        selectionChanged: function () {
            //Get all selected rows
            var selectedRows = $('#searchContractGrid').jtable('selectedRows');
            if (selectedRows.length > 0) {
                selectedRows.each(function () {
                    var record = $(this).data('record');
                    if (searchContractPopupForChangeOrUnlink == true) {
                        if (record.WorkflowStatus != "Approved") {
                            $("#searchContractGrid tr").removeClass("jtable-row-selected");
                            $("#searchContractGrid input").removeAttr("checked");
                        }
                    }
                    $('#hiddenContractId').val(record.ContractId);

                });
            }
        }
    });

    //  $('#searchContractGrid').jtable('load');

    //Ok Button Click Function 
    $('#btnOkRightsWorkQ').click(function () {
        if (searchContractPopupForChangeOrUnlink == true)
            searchContractForChangeOrUnlink();
        else
            searchContractForNormalSearch();
    });

    function searchContractForChangeOrUnlink() {
        var contract = [];
        var selectedContract = $('#searchContractGrid').jtable('selectedRows');
        var selectedContractID;
        if (selectedContract.length == 0) {
            $('#SearchMsgAlert').empty();
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            $('.warningContainers').show();

        }
        $('#searchwarning').hide();

        selectedContract.each(function () {
            //Right Set Grid id value match with Selected Contract Id - need to throw the error message to user//             
            var record = $(this).data('record');
            selectedContractID = record.ContractId;

            var workFlowCriteria = record.WorkflowStatus;
            if (workFlowCriteria == "Approved")
                contract.push(record);
        });
        if (SelGridContractID == selectedContractID) {
            $('#SearchMsgAlert').empty();
            $('#SearchMsgAlert').append('Selected Contract already been Linked to Resource / Release');
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            $('.warningContainers').show();
            return false;
        }

        if (searchContractPopupForChangeOrUnlink == true && isChangeLink == true && isUnLink == false)
            changeLink();
    }

    function changeLink() {
        var contract = [];
        var selectedContract = $('#searchContractGrid').jtable('selectedRows');
        if (selectedContract.length == 0) {
            $('#SearchMsgAlert').empty();
            $('#SearchMsgAlert').append(searchContractPopupMessages.EntryContract);
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            $('.warningContainers').show();
            return false;
        }
        $('#searchwarning').hide();
        selectedContract.each(function () {
            var record = $(this).data('record');
            var workFlowCriteria = record.WorkflowStatus;
            if (workFlowCriteria == "Approved")
                contract.push(record);
            else
                return false;
        });



        //        var confirmationMessage = 'Do you want to propagate the contract linking owned by the selected Repertoire ?';
        //        confirmLinkresDialog.html(confirmationMessage);

        //        confirmLinkresDialog.dialog({
        //            buttons: {
        //                'Yes': function (e) {
        //                    e.preventDefault();
        //                    $('#loadingDiv').show();
        //                    $(this).dialog('close');
        changeContract(selectedGridItems, contract); //Commented the confirmation popup as per QA Suggestion.
        //                },
        //                'No': function (e) {
        //                    e.preventDefault();
        //                    $('#confirmLinkresDialog').dialog('close');
        //                    $(this).dialog('close');
        //                }
        //            }
        //        });
        //        confirmLinkresDialog.dialog('open');
    }

    //Unlinks the contract from selected record and links to the new contract records
    function changeContract(selectedGridItems, contract) {
        $('#searchwarning').hide();
        $('#loadingDiv').show();
        $.post("/GCS/RightsReviewWorkQueue/ChangeContract", { changeLinkItems: JSON.stringify(selectedGridItems) })
                 .success(function (data) {
                     if (data.Record.length > 0) {
                         var concurencyDialog = $('<div id="divConcurencyDialog"></div>')
                                 .html('<p>' + messageCommon.onLoading + '</p>')
                                 .dialog({
                                     autoOpen: false,
                                     modal: true,
                                     title: "Warning",
                                     show: 'clip',
                                     hide: 'clip',
                                     width: 300
                                 });
                         selectedGridItems = JSLINQ(selectedGridItems).Where(function (item) {
                             if (JSLINQ(data.Record).Any(function (record) { return record == item.RightSetId; }) == false) {
                                 return item;
                             }
                         });
                         selectedGridItems = selectedGridItems.items;
                         var idHtml = "";
                         for (var index = 0; index < data.Record.length; index++) {
                             var indexNew = index + 1;
                             if (idHtml == "") idHtml = "<p style='line-height:1em;margin-bottom:2px;'>" + indexNew + data.Record[index] + "</p>";
                             else {
                                 idHtml = idHtml + "<p style='line-height:1em;margin-bottom:2px;'>" + indexNew + data.Record[index] + "</p>";
                             }
                         }

                         concurencyDialog.html("<p style='line-height:1em;margin-bottom:2px;'><b> Change link failed for repertoires due to concurrency.Please Refresh and continue.</b></p>");//+ idHtml);
                         concurencyDialog.dialog('open');
                         concurencyDialog.dialog({
                             buttons: {
                                 'Ok': function () {
                                     $(this).dialog('close');
                                     if(selectedGridItems.length==0) {
                                         $('#loadingDiv').hide(); $('#loaderPanel').hide();
                                         $('#RightsSearchContract').dialog('close');
                                         $find($('#hdnGridId').val()).sendRefreshRequest(); //Refresh : Repertoire Result Grid(parent page)
                                     }
                                 }
                             }
                         });
                         
                     }
                     if (selectedGridItems.length > 0) {
                         linkContract(selectedGridItems, contract);
                     }
                 })
                .error(function () {
                    $('#loadingDiv').hide(); $('#loaderPanel').hide();
                    $('#RightsSearchContract').dialog('close');
                    ShowWarning("Change link failed for the selected records.");
                    pageScrollRightsWorkQueue();
                });
    }

    //Links to the new contract records.
    function linkContract(selectedGridItems, contract) {
        $('#searchwarning').hide();
        $('#loadingDiv').show();
        //debugger;
        $.post("/GCS/RightsReviewWorkQueue/LinkContract", { changeLinkItems: JSON.stringify(selectedGridItems), contract: JSON.stringify(contract) })
            .success(function () {
                $('#RightsSearchContract').dialog('close');
                $find($('#hdnGridId').val()).sendRefreshRequest(); //Refresh : Repertoire Result Grid(parent page)
                ShowSuccess("The link for the selected items has been changed");
                customMenu();
                pageScrollRightsWorkQueue();
               // $('#loadingDiv').hide(); $('#loaderPanel').hide();
            }).error(function () {
                $('#loadingDiv').hide(); $('#loaderPanel').hide();
                $('#RightsSearchContract').dialog('close');
                ShowWarning("Change link failed for the selected records.");
                customMenu();
                pageScrollRightsWorkQueue();
            });
    }

    function searchContractForNormalSearch() {
        selectedReviewContracts = "";
        window.selectedcontractId = "";
        //To Select Multiple Records
        var selectedRowss = $('#searchContractGrid').jtable('selectedRows');
        if (selectedRowss.length > 0) {
            selectedRowss.each(function () {
                var records = $(this).data('record');
                var commencementDate;
                if (records.ContractCommencementDate == null) {
                    commencementDate = "NA";
                } else {
                    var re = /-?\d+/;
                    var m = re.exec(records.ContractCommencementDate);
                    var date = new Date(parseInt(m[0]));
                    var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                    commencementDate = date.getDate().toString() + ' ' + monthArray[date.getMonth("mmm")] + ' ' + date.getFullYear().toString();

                }
                if (selectedReviewContracts == "") {
                    if (records.ArtistName != "") {
                        selectedReviewContracts = records.ArtistName + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    } else {
                        selectedReviewContracts = records.ContractingParty + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    }
                } else {
                    if (records.ArtistName != "") {
                        selectedReviewContracts = selectedReviewContracts + "; " + records.ArtistName + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    } else {
                        selectedReviewContracts = selectedReviewContracts + "; " + records.ContractingParty + "," + commencementDate + "," + records.ClearanceCompanyCountry;
                    }
                }
                if (window.selectedcontractId == '') {
                    window.selectedcontractId += records.ContractId;
                }
                else {
                    window.selectedcontractId += '-' + records.ContractId;
                }
            });
            //$('#contractSearchPopup').dialog('close');

            //TODO Need change if ID changes
            if ((hdnReleaseRights == 'Release') || (hdnResourceRights == "Resource")) {
                $('#RepertoireFilter_LinkedContract').val(selectedReviewContracts);
                $('#hdnReleaseContractId').val(window.selectedcontractId);
                //TO hide the tab
                $('#liSearchContractTab').hide();
                $("#customSettingTabReview").tabs('select', '#divOtherParameterTab');
                $("#btnCreateRightsWorkQRelease").focus();
            }
            else {
                $('#txtAreaLinkedContract').val(selectedReviewContracts);
                $('#txtAreaLinkedContractId').val(selectedcontractId);
                $('#txtAreaLinkedContract').css('background-color', '#CCCCCC');
                //                $('.linkContractInputText').attr('title', selectedReviewContracts);
                //                $('.linkContractInputText').tooltip();
            }
            if ($("#customSettingTabReview").length == 0) {
                $('#contractSearchPopup').dialog('close');
            }
            return false;
        } else {
            $('#SearchMsgAlert').append("Please Select a Contract");
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            $('.warningContainers').show();
            return false;
        }
    }



    var artistCheckstate = false;

    //For Exact Match CheckBox 
    $('#Contract_IsExactArtistSearch').click(function (e) {
        e.stopPropagation();
        if (this.checked) {
            artistCheckstate = true;
        }
    });

    //search button click
    $('#searchcontractPopup').click(function (e) {
        e.preventDefault();
        reSizeSearchPage();

        if (checkSearchCriteria()) {
            $('#SearchMsgAlert').empty();
            $('#SearchMsgAlert').append(searchContractForReviewMessages.searchCritereaValidation);
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            $('.warningContainers').show();
            return false;
        } else {
            $('#searchwarning').hide();
        }
        $('#searchContractGrid').jtable('load', {

            artistName: $('#Artist_Info_Name').val(),
            hasSearchCriteria: true,
            contractingParty: $('#popupContractingPartyId').val(),
            clearanceAdminCompany: $('#clearanceCompSearchPopup').val(),
            localContractRefNumber: $('#searchLocalContract').val(),
            contractId: $('#contractPopupId').val(),
            isartistExactMatch: artistCheckstate

        });
        var element = document.getElementById('contractgrid');
        element.style.display = 'block';
        var showDropDown = document.getElementById('SearchContractPaging');
        showDropDown.style.display = 'block';
        $('#searchContractGrid').show();
        return false;
    });


    //Reset button click
    $('#resetContract').click(function (e) {
        e.preventDefault();
        $('#searchwarning').hide();
        $('#Artist_Info_Name').val('');
        $('#popupContractingPartyId').val('');
        $('#clearanceCompSearchPopup').val('');
        $('#searchLocalContract').val('');
        $('#contractPopupId').val('');
        $('#Contract_IsExactArtistSearch').removeAttr('checked');
    });


    $('input:text').keydown(function (e) {
        if (e.keyCode == 13) {
            $('#searchcontractPopup').click();
        }
    });

    //clearanceAdmin Company Auto search
    var target = $("#clearanceCompSearchPopup");
    target.autocomplete({
        source: target.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#clearanceCompSearchPopup").val(ui.item.value);
            $("#clearanceCompSearchId").val(ui.item.addValue);
            $("#clearanceCountrySearchId").val(ui.item.id);

        },
        response: function (event, ui) {

            var autocomplete = target.data("autocomplete");
            if (ui.content.length == 1) {
                autocomplete.selectedItem = ui.content[0];
            }
            if (autocomplete.selectedItem) {
                setTimeout(function () {
                    autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                    target.autocomplete('close');
                    $("#Artist_Info_Name").focus();
                }, 200);
            }
        }
    });



    //Cancel Button Click Function
    $('#btnCancelRightsWorkQ').click(function () {
        if (hdnReleaseRights == 'Release' || hdnReleaseRights == 'Resource') {
            //TO hide the hidden tab
            $('#liSearchContractTab').hide();
            $("#customSettingTabReview").tabs('select', '#divOtherParameterTab');
        }
        else if ($("#contractSearchPopup").is(':visible')) {
            $("#contractSearchPopup").dialog('close');
        }
        else {
            $('#RightsSearchContract').dialog('close');
        }

    });


    // DropDown select
    $('#SearchContractPaging select').change(function () {
        pageSize = $('#SearchContractPaging select').val();
        $('#searchContractGrid').jtable({ 'pageSize': pageSize });
        $('#searchContractGrid').jtable('load', {
            artistName: $('#Artist_Info_Name').val(),
            hasSearchCriteria: "true",
            contractingParty: $('#popupContractingPartyId').val(),
            clearanceAdminCompany: $('#clearanceCompSearchPopup').val(),
            localContractRefNumber: $('#searchLocalContract').val(),
            contractId: $('#contractPopupId').val(),
            isartistExactMatch: artistCheckstate
        });
    });
});

function checkSearchCriteria() {
    var artistName = $('#Artist_Info_Name').val();
    var contractingParty = $('#popupContractingPartyId').val();
    var clearanceAdminCompany = $('#clearanceCompSearchPopup').val();
    var localContractRefNumber = $('#searchLocalContract').val();
    var contractId = $('#contractPopupId').val();
    return (artistName == "" || artistName == null) && (contractingParty == "" || contractingParty == null) && (clearanceAdminCompany == "" || clearanceAdminCompany == null) && (localContractRefNumber == "" || localContractRefNumber == null) && (contractId == "" || contractId == null);
}

function reSizeSearchPage() {

    if ($('#searchwarning').css("display") == 'block') {
        $(".buttons").css("margin-right", "25px");
    }
    else {

        $(".buttons").css("margin-right", "25px");
    }


}

function ResizeTableSize() {
    if ($("#contractSearchPopup").is(':visible')) {
        if ($("#searchContractGrid .jtable-main-container").is(':visible')) {
            var popupHeight = $('#contractSearchPopup').height();
            var filterHeight = (popupHeight * 35) / 100;
            var gridheight = (popupHeight * 65) / 100;
            $('#searchPopup').css('height', filterHeight);
            $("#contractSearchPopup .jtable-main-container").css('height', gridheight - 95);
        }
    }
}

function ResizeContractPopUp() {
    if ($("#RightsSearchContract").length>0) {
        if ($("#RightsSearchContract .jtable-main-container").is(':visible')) {
            var DialogHgt = $("#RightsSearchContract").offset().top;
            var TblConHgt = $("#RightsSearchContract .jtable-main-container").offset().top;
            var TotDiaHgt = $("#RightsSearchContract").height();
            var ReduceHgt = TblConHgt - DialogHgt;
            var actualHeight = TotDiaHgt - ReduceHgt;
            if (actualHeight < 0)
                actualHeight = actualHeight * -1;
            var jtableheight = $("#RightsSearchContract .jtable-main-container").find(".jtable").height() + $("#RightsSearchContract .jtable-main-container").find(".jtable-left-area").height();
            if (jtableheight >= actualHeight)
                $("#RightsSearchContract .jtable-main-container").css('height', actualHeight - 60 + "px");
            else
                $("#RightsSearchContract .jtable-main-container").css('height', jtableheight + 50 + "px");
        }
    }
}


$("#contractPopupId").keypress(function (e) {
    if ($('#Contract_LocalContractRefNumber').val() != "") {
        e = e || event;
	    var chr = getChar(e);
	   if (!isNumeric(chr) && chr !== null) {
	    return false;
	  }
	}
});


// helper functions
function isNumeric(val) {
    return val !== "NaN" && (+val) + '' === val + ''
}

function getChar(event) {
    if (event.which == null) {
        return String.fromCharCode(event.keyCode) // IE
    } else if (event.which != 0 && event.charCode != 0) {
        return String.fromCharCode(event.which)   // the rest
    } else {
        return null // special key
    }
}
function customMenu() {

    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none') {

        if (!$.support.leadingWhitespace) {

            $('.customMenu .helpNavMenu ul#subnavlist').removeClass("customAlignWarning");
        }

        else if (($.browser.webkit)) {
            $('.customMenu .helpNavMenu ul#subnavlist').removeClass("customAlignWarningChrome");

        }
    }
    else
        if (!$.support.leadingWhitespace) {

            $('.customMenu .helpNavMenu ul#subnavlist').addClass("customAlignWarning");
        }
        else if (($.browser.webkit)) {
            $('.customMenu .helpNavMenu ul#subnavlist').addClass("customAlignWarningChrome");

        }


}