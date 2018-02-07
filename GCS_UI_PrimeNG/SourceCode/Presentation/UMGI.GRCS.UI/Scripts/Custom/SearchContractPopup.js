/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/Contract.js" />
/// <reference path="/GCS/Scripts/Custom/PriorityWorkQueue.js" />
var searchContractPopupMessages = {
    searchValidation: "Please Enter atleast one Feild for Search Criteria", artistName: "Artist", adminCompany: "Clearance Admin Co",
    localCntrctNo: "Local Contract Ref No:", commencDate: "Commencement Date", territorialRights: "Territorial Rights", ReleaseLinkTitle: "Link Release To Contract",
    ApprovedStatus: "Please select only Approved Contract To Link", EntryContract: "Please select Contract To Link",
    LinkingFailed: "Contract Linking Failed", PropagationFailed: "PropagationFailed", confirmChangeLink: "Confirm Change Link", ProjectLinkingTitle: "Link Project To Contract",
    changeLinkMsg: "Changing the contract linking for the selected release will change the rights data set against the release. Do you want to continue?"
};

function reSizeSearchPage() {
    var h = $(window).height();
    if ($('#searchwarning').css("display") == 'block') {
        $("#searchPopup").css('height', h - 100);
        $(".jtable-main-container").css('height', h - 260);
        $(".buttons").css("margin-right", "25px");
    }
    else {
        $("#searchPopup").css('height', h - 130);
        $(".jtable-main-container").css('height', h - 260);
        $(".buttons").css("margin-right", "25px");
    }
}

var propQueryString = '';

var objDialog = $('<div id="contractPopup"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: '',
            show: 'clip',
            hide: 'clip',
            width: 700,
            position: [(($(document).width() - 700) / 2), 50]
        });

var objProjectPro = $('<div id="projPropPopup"></div>')
.html('<p>' + messageCommon.onLoading + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: "",
                show: 'clip',
                hide: 'clip',
                width: "98%",
                position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50],
                close: function () { $(this).remove(); }
            });
var multiSelect = false;
$(function () {
    $("#btnOk").prop("disabled", "disabled");
    $("#btnCancel").prop("disabled", "disabled");
    $('#warningContainers').hide();

    if ($('#Contract_ContractId').val() > 0)
        multiSelect = true;

    var pageSize = 25;
    var rowIndex = -1;
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
            listAction: '/GCS/Contract/SearchContract'
        },
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); $(".ui-jtable-loading").show(); },
        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;

            document.getElementById("SearchCount").innerHTML = "Search Results (" + rowIndex + ")";
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            $("#searchContractGrid input").removeAttr("checked");
            $("#searchContractGrid tr").removeClass("jtable-row-selected");
            $("#btnOk").prop("disabled", "");
            $("#btnCancel").prop("disabled", "");
        },
        fields: {
            'Linked': {
                title: '',
                display: function (test) {
                    var linkImage;
                    if (test.record.HasRepertoire)
                        linkImage = $('<img src="/Gcs/Images/linked_Contract.png" title"Linked Contract">');
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
                    return decodeURIComponent(data.record.TerritorialRightsDefinition).replace(/]/g, '');
                }
            },
            ContractCommencementDate: {
                title: searchContractPopupMessages.commencDate,
                type: 'date',
                displayFormat: 'dd M yy'
            },
            LocalContractRefNumber: {
                title: searchContractPopupMessages.localCntrctNo
            },
            ContractId: {
                title: messageCommon.cntrctId
            }
        },

        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
            var selectedRows = $('#searchContractGrid').jtable('selectedRows');
            if (selectedRows.length > 0) {
                selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#hiddenContractId').val(record.ContractId);
                });
            }
        }
    });
    var accountId = '';
    var selectedRowss = $('#workQueueGrid').jtable('selectedRows');
    selectedRowss.each(function () {
        var records = $(this).data('record');
        accountId = records.AccountId;
    });
    $(document).ready(function () {
        reSizeSearchPage();
        $("#btnOk").hide();
        $("#btnCancel").hide();
        //    $('#searchwarning').hide();

        if (selectedRowss.length == 1) {
            $("#btnOk").show();
            $('#searchContractGrid').show();
            $(".ui-jtable-loading").show();
            $("#btnCancel").show();
            $('#searchwarning').hide();
            $('#SearchCount').show();
            hasSearchCriteria = "true";

            $('#searchContractGrid').jtable('load', {
                artistName: $('#Artist_Info_Name').val(),
                workflowStatus: workflowtext,
                contractStatus: contracttext,
                rightsType: rightsTypeText,
                hasSearchCriteria: hasSearchCriteria,
                contractingParty: $('#popupContractingPartyId').val(),
                clearanceAdminCompany: $('#clearanceCompSearchPopup').val(),
                localContractRefNumber: $('#searchLocalContract').val(),
                contractId: $('#contractPopupId').val(),
                accountId: accountId
            });

            $('#searchContractGrid').show();
        } else {
            $('#searchContractGrid').hide();
            $(".ui-jtable-loading").hide();
            $('#SearchCount').hide();
            $('#SearchContractPaging').hide();
        }

        //     $('#searchContractGrid').hide();
    });
    //search button click
    $('#searchcontractPopup').click(function (e) {
        e.preventDefault();
        reSizeSearchPage();
        if (checkSearchCriteria()) {
            $('#SearchMsgAlert').empty();
            $('#SearchMsgAlert').append(searchContractPopupMessages.searchValidation);
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();

            return false;
        }
        else {
            $("#btnOk").show();
            $("#btnCancel").show();
            $('#searchwarning').hide();
            $('#SearchCount').show();
            hasSearchCriteria = "true";
            $('#btnOkSplit').show();
            $('#btnCancelSplit').show();

            $('#searchContractGrid').jtable('load', {
                artistName: $('#Artist_Info_Name').val(),
                workflowStatus: workflowtext,
                contractStatus: contracttext,
                rightsType: rightsTypeText,
                hasSearchCriteria: hasSearchCriteria,
                contractingParty: $('#popupContractingPartyId').val(),
                clearanceAdminCompany: $('#clearanceCompSearchPopup').val(),
                localContractRefNumber: $('#searchLocalContract').val(),
                contractId: $('#contractPopupId').val()
            });
            var element = document.getElementById('contractgrid');
            element.style.display = 'block';
            var showDropDown = document.getElementById('SearchContractPaging');
            showDropDown.style.display = 'block';
            $('#searchContractGrid').show();
        }
        return false;
    });

    //Reset button click
    $('#resetContract').click(function (e) {
        e.preventDefault();
        $('#searchwarning').hide();
        $('#Artist_Info_Name').val('');
        $('#popupContractingPartyId').val('');
        $('#clearanceCompSearchPopup').val();
        $('#searchLocalContract').val('');
        $('#contractPopupId').val('');
    });
});

reSizeSearchPage();
var workflowtext = "";
var contracttext = "";
var rightsTypeText = "";
hasSearchCriteria = "true";
$(document).ready(function () {
    $('input:text').keydown(function (e) {
        if (e.keyCode == 13) {
            $('#searchcontractPopup').click();
            return false;;
        }
    });

    $("#contractPopupId").keyup(function () {
        if ($('#contractPopupId').val() != "") {
            var value = $('#contractPopupId').val().replace(/^\s\s*/, '').replace(/\s\s*$/, '');
            var intRegex = /^\d+$/;
            if (!intRegex.test(value)) {
                value = document.getElementById('contractPopupId').value.replace(/[^0-9\.]+/g, '');
            }
            $('#contractPopupId').val(value);
        }
    });

    //DropDown select
    $('#SearchContractPaging select').change(function () {
        pageSize = $('#SearchContractPaging select').val();
        $('#searchContractGrid').jtable({ 'pageSize': pageSize });

        $('#searchContractGrid').jtable('load', {
            artistName: $('#Artist_Info_Name').val(),
            workflowStatus: workflowtext,
            contractStatus: contracttext,
            rightsType: rightsTypeText,
            hasSearchCriteria: hasSearchCriteria,
            contractingParty: $('#popupContractingPartyId').val(),
            clearanceAdminCompany: $('#clearanceCompSearchPopup').val(),
            localContractRefNumber: $('#searchLocalContract').val(),
            contractId: $('#contractPopupId').val()
        });
    });

    reSizeSearchPage();

    // reSizeSearchPage();
    //                //Water Mark for feilds
    //            $('#Artist_Info_Name').watermark('45 Characters');
    //            $('#popupContractingPartyId').watermark('45 Characters');
    //            $('#clearanceCompSearchPopup').watermark('45 Characters');

    //AutoComplete Artist
    var target1 = $("#Artist_Info_Name");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#Artist_Info_Name").val(ui.item.value);
        }
    });

    //clearanceAdmin Company Auto search
    var target = $("#clearanceCompSearchPopup");
    target.autocomplete({
        source: target.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#clearanceCompSearchPopup").val(ui.item.id);
            $("#clearanceCompSearchId").val(ui.item.addValue);
            $("#clearanceCountrySearchId").val(ui.item.id);
            //var indexNo = $("#clearanceCompSearchPopup").val().indexOf('-');
            //$("#Contract_UmgSigningCompany").val($("#Contract_ClearanceCompanyCountry").val().substring(0, indexNo));
        }
    });

    //            //AutoComplete Local Contract Ref
    //            var target2 = $("#Contract_LocalContractRefNumber");
    //            target2.autocomplete({
    //                source: target2.attr("data-autocomplete-source-manual"),
    //                minLength: 2,
    //                select: function (event, ui) {
    //                    $("#Contract_LocalContractRefNumber").val(ui.item.value);
    //                }
    //            });

    //AutoComplete Contracting Party
    //            var target3 = $("#popupContractingPartyId");
    //            target3.autocomplete({
    //                source: target3.attr("data-autocomplete-source-manual"),
    //                minLength: 2,
    //                select: function (event, ui) {
    //                    $("#popupContractingPartyId").val(ui.item.value);
    //                }
    //            });

    reSizeSearchPage();
    $(window).resize(function () {
        reSizeSearchPage();
    });

    $('#cancelButton').click(function () {
        $('#contractSearchPopup').dialog('close');
    });

    $('#btnOk').click(function (e) {
        var selectedWorkQueueItems = [];
        var contract = [];
        var isApprovedStatus = '';

        var selectedContract = $('#searchContractGrid').jtable('selectedRows');
        //var selectedRowss = $('#searchContractGrid').jtable('selectedRows');
        if (selectedContract.length == 0) {
            $('#SearchMsgAlert').empty();
            $('#SearchMsgAlert').append(searchContractPopupMessages.EntryContract);
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            return false;
        }
        $('#searchwarning').hide();
        selectedContract.each(function () {
            var record = $(this).data('record');
            var workFlowCriteria = record.WorkflowStatus;
            if (workFlowCriteria == "Approved") {
                isApprovedStatus = true;
                contract.push(record);
            } else {
                isApprovedStatus = false;
            }
        });

        if (isApprovedStatus == true) {
            var selectedRows = $('#workQueueGrid').jtable('selectedRows');
            selectedRows.each(function () {
                var record = $(this).data('record');
                selectedWorkQueueItems.push(record);
            });
            //                    if (isChangeLink == false) {
            //                        linkContract(selectedWorkQueueItems, contract);

            //                    } else {
            //
            //                        ChangeContract(selectedWorkQueueItems);
            //
            //                    }
            //Check already linked
            ConfirmLinkResource(selectedWorkQueueItems, contract);
        } else {
            $('#SearchMsgAlert').empty();
            $('#SearchMsgAlert').append(searchContractPopupMessages.ApprovedStatus);
            $('#SearchMsgAlert').show();
            $('#searchwarning').show();
            return false;
        }
    });

    $('#btnCancel').click(function () {
        $('#WorkQueuee').dialog('close');
    });
});
function ConfirmLinkResource(selectedWorkQueueItems, contract) {
    var confirmLinkresDialog = $('<div id="confirmLinkresDialog"></div>')
    .html('<p>' + messageCommon.onLoading + '</p>')
    .dialog({
        autoOpen: false,
        modal: true,
        title: "Confirm",
        show: 'clip',
        hide: 'clip',
        width: 300
    });

    var concurrencyErrorDialog = $('<div id="concurrencyErrorDia"></div>')
    .html('<p>' + messageCommon.onLoading + '</p>')
    .dialog({
        autoOpen: false,
        modal: true,
        title: "Already Linked",
        show: 'clip',
        hide: 'clip',
        width: 700
    });

    confirmLinkresDialog.html('Do you want to propagate the contract linking owned by the selected Resource?');
    confirmLinkresDialog.dialog({
        buttons: {
            'Yes': function (e) {
                e.preventDefault();
                $('#loadingDiv').show();
                $(this).dialog('close');
                //var qString = '?artist= ' + encodeURI(contract[0].ArtistName) + '&commencementDate=' + encodeURI(getDateString(parseDate(contract[0].ContractCommencementDate))) + '&clearanceAdminCompany=' + encodeURI(contract[0].ClearanceCompanyCountry) + '&clearanceAdminCompanyId=' + encodeURI((contract[0].ClearanceCompanyCountryId));
                $.post("/GCS/WorkQueue/IsRepertoireAlreadyLinked", { workQueueItem: JSON.stringify(selectedWorkQueueItems), id: $('#hiddenContractId').val(), contractString: JSON.stringify(contract) })
                    .success(function (data) {
                        if ($(data).find('#hiddenCount').val() == "1") {
                            //Show popup
                            concurrencyErrorDialog.html(data);
                            concurrencyErrorDialog.dialog({
                                close: function () {
                                    if (isChangeLink == false) {
                                        linkContract(selectedWorkQueueItems, contract);
                                    } else {
                                        ChangeContract(selectedWorkQueueItems, contract);
                                    }
                                }
                            });
                            concurrencyErrorDialog.dialog('open');
                        } else {
                            if (isChangeLink == false) {
                                linkContract(selectedWorkQueueItems, contract);
                            } else {
                                ChangeContract(selectedWorkQueueItems, contract);
                            }
                        }
                    })
                    .error(function (request, data) {
                        alert(request.responseText);
                        $('#loadingDiv').hide();
                        if (isChangeLink == false) {
                            linkContract(selectedWorkQueueItems, contract);
                        } else {
                            ChangeContract(selectedWorkQueueItems, contract);
                        }
                    });
            }, 'No': function (e) {
                e.preventDefault();
                $('#confirmLinkresDialog').dialog('close');
                $(this).dialog('close');
            }
        }
    });
    confirmLinkresDialog.dialog('open');
}

function ChangeContract(selectedWorkQueueItems, contract) {
    $('#searchwarning').hide();
    $('#loadingDiv').show();
    $.post("/GCS/WorkQueue/ChangeContract", { workQueueItem: JSON.stringify(selectedWorkQueueItems), id: $('#hiddenContractId').val() })
         .success(function (data) {
             if ($(data).find("#hiddenCount").val() == 'Resource') {
                 objReleaseUnlinkPro.html(data);
                 objReleaseUnlinkPro.dialog({ title: priorityWorkQueueMessage.ReleaseUnLinkTitle });
                 objReleaseUnlinkPro.dialog({
                     close: function () {
                         linkContract(selectedWorkQueueItems, contract);
                     }
                 });
                 objReleaseUnlinkPro.dialog('open');
             } else {
                 linkContract(selectedWorkQueueItems, contract);
             }
         })
        .error(function () {
            $('#loadingDiv').hide();
        });
}

function linkContract(selectedWorkQueueItems, contract) {
    $('#searchwarning').hide();
    $('#loadingDiv').show();

    $.post("/GCS/WorkQueue/LinkContract", { workQueueItem: JSON.stringify(selectedWorkQueueItems), id: $('#hiddenContractId').val() })
    .success(function (data) {
        var qString = '?artist= ' + encodeURI(contract[0].ArtistName) + '&commencementDate=' + encodeURI(getDateString(parseDate(contract[0].ContractCommencementDate))) + '&clearanceAdminCompany=' + encodeURI(contract[0].ClearanceCompanyCountry) + '&clearanceAdminCompanyId=' + encodeURI((contract[0].ClearanceCompanyCountryId));

        if ($(data).find('#hidAlreadyLinked').length != 0 && $(data).find('#hidAlreadyLinked').val() == 'AlreadyLinked') {
            displayConcurrentyMessage(checkPropagation(data, qString));
        }
        else {
            checkPropagation(data, qString);
        }
        $('#loadingDiv').hide();
        $('#WorkQueuee').dialog('close');
        $('#workQueueGrid').jtable('load', {
            artistName: $('#WorkQueues_ArtistName').val(),
            contractDesc: $('#WorkQueues_ContractDescription').val(),
            descTitle: $('#WorkQueues_Title').val(),
            reasonForReview: $('#WorkQueues_ContractReviewReason').val(),
            adminCompany: $('#WorkQueues_CompanyName').val()
        });
    })
.error(function (request, data) {
    alert(data.responseText);
    // ShowWarning(searchContractPopupMessages.LinkingFailed);
    $('#loadingDiv').hide();
});
}

function checkSearchCriteria() {
    var artistName = $('#Artist_Info_Name').val();

    var contractingParty = $('#popupContractingPartyId').val();
    var clearanceAdminCompany = $('#clearanceCompSearchPopup').val();
    var localContractRefNumber = $('#searchLocalContract').val();
    var contractId = $('#contractPopupId').val();
    return (artistName == "" || artistName == null) && (contractingParty == "" || contractingParty == null) && (clearanceAdminCompany == "" || clearanceAdminCompany == null) && (localContractRefNumber == "" || localContractRefNumber == null) && (contractId == "" || contractId == null);
}

function propagataProjectLinking(queryString, status) {
    var project = [];

    var selectedRows = $('#workQueueGrid').jtable('selectedRows');
    selectedRows.each(function () {
        var record = $(this).data('record');
        var workQueueType = record.ResourceType;
        if (workQueueType == "Project") {
            {
                project[0] = record;
                return false;
            }
        }
        return true;
    });

    $.ajax({
        url: "/GCS/workQueue/PropagateProjectLinking/" + $('#hiddenContractId').val() + queryString,
        type: 'POST',
        data: JSON.stringify(project),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            objProjectPro.dialog({ title: searchContractPopupMessages.ProjectLinkingTitle });
            objProjectPro.html(data);
            objProjectPro.find('#ProProjectLinking').val('workQueue');
            if (status == 'ProjectAndRelease') {
                objProjectPro.find('#hidHasAssociatedRel').val('true');
            }
            objProjectPro.dialog('open');
        },
        error: function (request, data) {
            alert(data.responseText);

            // ShowWarning(searchContractPopupMessages.PropagationFailed);
        }
    });
}

function propagataReleaseLinking(queryString) {
    var releases = [];
    var selectedRows = $('#workQueueGrid').jtable('selectedRows');
    selectedRows.each(function () {
        var record = $(this).data('record');
        var workQueueType = record.ResourceType;
        if (workQueueType == "Release") {
            {
                releases.push(record);
                return false;
            }
        }
        return true;
    });

    $.ajax({
        url: "/GCS/workQueue/PropagateReleaseLinking/" + $('#hiddenContractId').val() + queryString,
        type: 'POST',
        data: JSON.stringify(releases),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            objProjectPro.dialog({ title: searchContractPopupMessages.ReleaseLinkTitle });
            objProjectPro.html(data);
            objProjectPro.find('#ProProjectLinking').val('workQueue');
            objProjectPro.dialog('open');
        },
        error: function (request, data) {
            alert(data.responseText);
            //ShowWarning(searchContractPopupMessages.LinkingFailed);
        }
    });
}
var status = null;
function checkPropagation(data, queryString) {
    if ($(data).find('#displayMessage').length != 0) {
        if ($(data).find('#displayMessage').val() == 'ProjectAndRelease') {
            status = $(data).find('#displayMessage').val();

            propagataProjectLinking(queryString, status);
            propQueryString = queryString;
        }
        else if ($(data).find('#displayMessage').val() == 'Project') {
            propagataProjectLinking(queryString, status);
        } else if ($(data).find('#displayMessage').val() == 'Release') {
            propagataReleaseLinking(queryString);
        }
    }
}

function displayConcurrentyMessage(callback) {
    alert('Concurrency error');
    callback();
}

function parseDate(dateString) {
    if (dateString.indexOf('Date') >= 0) { //Format: /Date(1320259705710)/
        return new Date(parseInt(dateString.substr(6)));
    } else if (dateString.length == 10) { //Format: 2011-01-01
        return new Date(parseInt(dateString.substr(0, 4)), parseInt(dateString.substr(5, 2)) - 1, parseInt(dateString.substr(8, 2)));
    } else if (dateString.length == 19) { //Format: 2011-01-01 20:32:42
        return new Date(parseInt(dateString.substr(0, 4)), parseInt(dateString.substr(5, 2)) - 1, parseInt(dateString.substr(8, 2)), parseInt(dateString.substr(11, 2)), parseInt(dateString.substr(14, 2)), parseInt(dateString.substr(17, 2)));
    } else {
        this._logWarn('Given date is no properly formatted: ' + dateString);
        return new Date(); //Default value!
    }
}

function getDateString(date) {
    var dd = date.getDay();//yeilds day
    var MM = date.getMonth();//yeilds month
    var yyyy = date.getYear(); //yeilds year
    var HH = date.getHours();//yeilds hours
    var mm = date.getMinutes();//yields minutes
    var ss = date.getSeconds();//yields seconds
    var value = dd + "/" + MM + "/" + yyyy + " " + HH + ':' + mm + ':' + ss;
    return value;
}

//SplitDeal Popup Click function-----------starts----------------

$(document).ready(function () {
    $('#btnOkSplit').hide();
    $('#btnCancelSplit').hide();
    $('#btnOkSplit').click(function (e) {
        var selectedContracts = [];
        var selectedContract = $('#searchContractGrid').jtable('selectedRows');
        if (selectedContract.length > 0) {
            $('#contractSearchPopup').dialog('close');
            selectedContract.each(function () {
                var record = $(this).data('record');
                selectedContracts.push(record);
            });
            $.post("/GCS/Contract/AddSplitDealContract", { contracInfos: JSON.stringify(selectedContracts) },
                    function (data) {
                        $('#divContractTab').html(data);
                    });
            return true;
        }
        else {
            alert("please Select Contract");
            return false;
        }
    });

    $('#btnCancelSplit').click(function (e) {
        $('#contractSearchPopup').dialog('close');
    });
});

//SplitDeal Popup---------------------------Ends--------------------

//AutoLinkRepertoire Popup
$(document).ready(function () {
    $('#btnOkAutoLink').click(function () {
        $('#contractSearchPopup').dialog('close');
        var $selectedRows = $('#searchContractGrid').jtable('selectedRows');

        if ($selectedRows.length > 0) {
            //Show selected rows
            $selectedRows.each(function () {
                var record = $(this).data('record');

                $('#Contract').val(record.ArtistName + '/' + record.ContractingParty + '-' + record.ContractId + '-' + record.ClearanceCompanyCountry);
                $('#contractCdlId').val(record.ContractId);
            });
        }
    }

    );
    // Cancel button
    $('#btnCancelAutoLink').click(function () {
        $('#contractSearchPopup').dialog('close');
    });
});
//AutoLinkRepertoire Popup---------------------------Ends--------------------