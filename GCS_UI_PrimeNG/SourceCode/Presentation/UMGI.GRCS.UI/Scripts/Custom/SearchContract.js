var messages = {
    dateValid: 'Please enter From and To Date',
    copyValid: "Please select a contract to copy.",
    linkValid: "Please select a contract to Link",
    unlinkValid: "Please select a contract to Unlink",
    deleteValid: "Please select a contract to delete",
    deleteValidParent: "Contract cannot be Deleted since it is a Parent Contract",
    editAccess: "You are not authorised to Edit this Contract",
    copyAccess: "You are not authorised to Copy Contract",
    deleteAccess: "You are not authorised to Delete Contract",
    viewAccess: "You are not authorised to View this Contract",
    linkAccess: "You are not authorised to Link Contract",
    unlinkAccess: "You are not authorised to Unlink Contract",
    confirmDelete: "Are you sure you want to delete ?",
    confirm: 'Confirm',
    oneEditValid: "Only one row can be edited at a time.",
    oneviewValid: "Only one row can be viewed at a time.",
    ApprovedLinkValid: "Please set status to Approved in order to link repertoire",
    ApprovedUnlinkValid: "Please Select a Approved Contract to Unlink",
    LinkContractValid: "Only one Contract can be Linked at a time",
    CopyContractValid: "Only one Contract can be Copied at a time"
};
var objWarningDialog = $('<div id="Warning"></div>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            resizable: true,
            width: 250,
            height: 150
        });

$(function () {
    if ($('#Contract_ContractId').val() == 0)
        $('#Contract_ContractId').val('');

    reSizeSearchPage();

    //Create dialog
    var objSearchContractDialog = $('<div id="SearchContractArtist"></div>')
.html('<p>' + messageCommon.onLoading + '</p>')
.dialog({
    autoOpen: false,
    modal: true,
    title: messageCommon.infoTitle,
    show: 'clip',
    hide: 'clip',
    width: 1000
});

    var pageSize = 25;
    var rowIndex = -1;

    $('#searchcontractList').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'ContractId ASC',
        selecting: true,
        selectingCheckboxes: true,
        selectOnRowClick: true,
        actions: {
            listAction: '/GCS/Contract/SearchContract'
        },
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
            //$('#SearchAlert').hide();
        },
        // //For tooltip

        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("searchResultCount").innerHTML = "Search Result (" + rowIndex + ")";
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            setToolTip(this);
            $("#searchcontractList input").removeAttr("checked");
            $("#searchcontractList tr").removeClass("jtable-row-selected");
        },
        fields: {
            'Parent': {
                title: '',
                display: function (test) {
                    var image;
                    if (test.record.HasChildContract == true)
                        image = $('<img  src="/Gcs/Images/parentcontract.png" title="Parent Contract">');
                    else if (test.record.HasParentContract == true)
                        image = $('<img  src="/Gcs/Images/childcontract.png" title="Child Contract">');
                    else
                        image = "";
                    if (test.record.HasRepertoire)
                        image = $('<img src="/Gcs/Images/linked_Contract.png" title"Linked Contract">');
                    return image;
                }
            },
            'ArtistName': {
                title: messageCommon.artistName,
                display: function (data) {
                    var artistName = data.record.ArtistName;
                    var artist = artistName;
                    return artist;
                }
            },
            ContractingParty: {
                title: messageCommon.cntrctParty,
                display: function (data) {
                    if (data.record.ContractingParty != null)
                        return data.record.ContractingParty;
                    else
                        return data.record.ContractingParty;
                }
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
                title: messageCommon.adminCmpny
            },
            UmgSigningCompany: {
                title: messageCommon.umgSignCmpny
            },
            RightsTypeName: {
                title: messageCommon.rightsType
            },
            ContractCommencementDate: {
                title: messageCommon.cmncmentDate,
                type: 'date',
                displayFormat: 'dd M yy'
            },
            LocalContractRefNumber: {
                title: messageCommon.localCntrctNo
            },
            ContractId: {
                title: messageCommon.cntrctId,
                display: function (test) {
                    return test.record.ContractId;
                }
            },
            'CanEdit': {
                title: 'Hi',
                display: function (data) {
                    var canEdit = data.record.CanEdit;
                    if (canEdit)
                        return 'Yes';
                    else
                        return 'False';
                },
                visibility: 'hidden'
            },
            'CanDelete': {
                title: 'Hi',
                display: function (data) {
                    var canDelete = data.record.CanDelete;
                    if (canDelete)
                        return 'Yes';
                    else
                        return 'False';
                },
                visibility: 'hidden'
            },
            'CanCopy': {
                title: 'Hi',
                display: function (data) {
                    var canDelete = data.record.CanCopy;
                    if (canDelete)
                        return 'Yes';
                    else
                        return 'False';
                },
                visibility: 'hidden'
            },
            'CanView': {
                title: 'Hi',
                display: function (data) {
                    var canDelete = data.record.CanView;
                    if (canDelete)
                        return 'Yes';
                    else
                        return 'False';
                },
                visibility: 'hidden'
            },
            'CanLink': {
                title: 'Hi',
                display: function (data) {
                    var canLink = data.record.CanLink;
                    if (canLink)
                        return 'Yes';
                    else
                        return 'False';
                },
                visibility: 'hidden'
            },
            'CanLinkApproved': {
                title: 'Hi',
                display: function (data) {
                    var canLinkApproved = data.record.CanLinkApproved;
                    if (canLinkApproved)
                        return 'Yes';
                    else
                        return 'False';
                },
                visibility: 'hidden'
            }
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            HideWarningSuccess();
            //Get all selected rows
            var $selectedRows = $('#searchcontractList').jtable('selectedRows');
            $('#SelectedRowList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#SelectedRowList').append(record.ContractId + ':' + record.HasChildContract + ':' + record.CanEdit + ':' + record.CanDelete + ':' + record.CanCopy + ':' + record.CanView + ':' + record.CanLink + ':' + record.CanLinkApproved);
                    var recdisplay = document.getElementById('SelectedRowList');
                    recdisplay.style.display = 'none';
                });
            }
        }
    });

    $(document).ready(function () {
        if ($('#isFromSession').val() == "True") {
            $('#searchcontractList').jtable('load', {
                isDefaultLoad: true
            });
            var element = document.getElementById('contractgrid');
            element.style.display = 'block';
            var showDropDown = document.getElementById('SearchContractPaging');
            showDropDown.style.display = 'block';
            var exportToExcel = document.getElementById('ContractListExcelExport');
            exportToExcel.style.display = 'block';
            var exportToWord = document.getElementById('ExportToWord');
            exportToWord.style.display = 'block';
            $('#searchcontractList').show();
        }
        else {
            $('#SearchContractPaging').hide();
            $('#searchcontractList').hide();
        }

        $(".searchContractScroll").scroll(function () {
            $(".ui-datepicker").hide();
        });

        //                $('#main').click(function () {
        //                    HideWarningSuccess();
        //                    return false;
        //                });
        //$('#SearchAlert').hide();

        $('#SearchContractPaging select').change(function () {
            HideWarningSuccess();
            var paging = $('#SearchContractPaging select').val();
            pageSize = paging;
            var workflowindex = $('#WorkFlowStatus')[0].selectedIndex;
            var workflowtext = $('#WorkFlowStatus')[0][workflowindex].text;
            if (workflowtext == 'All')
                workflowtext = '';
            var contractindex = $('#ContractStatusList')[0].selectedIndex;
            var contracttext = $('#ContractStatusList')[0][contractindex].text;
            if (contracttext == 'All')
                contracttext = '';
            var clearanceAdminCompany = $('#ClearanceAdminCompany')[0].selectedIndex;
            var clearanceAdminCompanyText = $('#ClearanceAdminCompany')[0][clearanceAdminCompany].text;
            if (clearanceAdminCompanyText == '-- Select --')
                clearanceAdminCompanyText = '';
            var rightsType = $('#RightsType')[0].selectedIndex;
            var rightsTypeText = $('#RightsType')[0][rightsType].text;
            if (rightsTypeText == 'All')
                rightsTypeText = '';
            $('#searchcontractList').jtable({ 'pageSize': pageSize });
            $('#searchcontractList').jtable('load', {
                artistName: $('#Artist_Info_Name').val(),
                workflowStatus: workflowtext,
                contractStatus: contracttext,
                contractingParty: $('#Contract_ContractingParty').val(),
                clearanceAdminCompany: clearanceAdminCompanyText,
                artistNameInLocalCharacters: $('#Contract_ArtistNameInLocalCharacters').val(),
                umgSigningCompanyId: $('#Contract_UmgSigningCompany').val(),
                localContractRefNumber: $('#Contract_LocalContractRefNumber').val(),
                contractCommencementDate: $('#Contract_ContractCommencementDate').val(),
                contractEndDate: $('#Contract_ContractEndDate').val(),
                contractId: $('#Contract_ContractId').val(),
                rightsType: $('#Contract_RightsTypeId').val(),
                otherRights: $('#Contract_OtherRights').val(),
                jtPageSize: paging,
                isDefaultLoad: true
            });

            var searchGrid = document.getElementById('contractgrid');
            searchGrid.style.display = 'block';
            $('#searchcontractList').show();
        });
        // $('#SearchAlert').hide();
    });

    //search button click
    $('#searchcontract').click(function (e) {
        $('#warning').hide();
        e.preventDefault();
        e.preventDefault();
        searchContract();
        var element = document.getElementById('contractgrid');
        element.style.display = 'block';
        var showDropDown = document.getElementById('SearchContractPaging');
        showDropDown.style.display = 'block';
        var exportToExcel = document.getElementById('ContractListExcelExport');
        exportToExcel.style.display = 'block';
        var exportToWord = document.getElementById('ExportToWord');
        exportToWord.style.display = 'block';
        $('#searchcontractList').show();
        reSizeSearchPage();
        //$('#searchContractAccordion .head').find('a').toggleClass('iconBottom');
        //$('.artistWrkflowContractContainer').toggle();
    });

    $(window).resize(function () {
        reSizeSearchPage();
    });

    //Reset button click
    $('#resetContract').click(function (e) {
        $('#warning').hide();
        e.preventDefault();
        //                var element = document.getElementById('contractgrid');
        //                element.style.display = 'none';
        var showDropDown = document.getElementById('SearchContractPaging');
        showDropDown.style.display = 'none';
        var exportToExcel = document.getElementById('ContractListExcelExport');
        exportToExcel.style.display = 'none';
        var exportToWord = document.getElementById('ExportToWord');
        exportToWord.style.display = 'none';
        $('#Artist_Info_Name').val('');
        $('#Contract_ContractingParty').val('');
        $('#Contract_ArtistNameInLocalCharacters').val('');
        $('#Contract_UmgSigningCompany').val('');
        $('#Contract_ContractId').val('');
        $('#ContractsSearch_OtherRights').val('');
        $('#Contract_ContractEndDate').val('');
        $('#Contract_LocalContractRefNumber').val('');
        $('#Contract_ContractCommencementDate').val('');
        $('#Contract_ContractCommencementDate').val('');
        $('#Contract_UmgSigningCompany').val('');
        $('#RightsType')[0].selectedIndex = 0;
        $('#ClearanceAdminCompany')[0].selectedIndex = 0;
        $('#ContractStatusList')[0].selectedIndex = 0;
        $('#WorkFlowStatus')[0].selectedIndex = 0;
    });

    //Link to repertoire button click
    $('#LinkToRepertoire').click(function () {
        $('#warning').hide();
        var $selectedRows = $('#searchcontractList').jtable('selectedRows');
        if ($selectedRows.length > 1) {
            ShowWarning(messages.LinkContractValid);
            return false;
        }
        var contractSelected = $('#SelectedRowList')[0].innerHTML;
        var contracts = contractSelected.split(':');
        var contractid = contracts[0];
        var canLink = contracts[6];
        var canLinkApproved = contracts[7];
        if (contractid != null && contractid != '' && canLink == "true" && canLinkApproved == "true") {
            window.location.href = "/GCS/Project/SearchRepertoire/" + contractid;
        }
        else if (contractid != null && contractid != '' && canLink != "true") {
            ShowWarning(messages.linkAccess);
            return false;
        }
        else if (contractid != null && contractid != '' && canLink != "false" && canLinkApproved != "true") {
            ShowWarning(messages.ApprovedLinkValid);
            return false;
        }
        else {
            ShowWarning(messages.linkValid);
            return false;
        }
    });

    //Copy Contract button clik

    $('#CopyContract').click(function () {
        $('#warning').hide();
        var contractSelected = $('#SelectedRowList')[0].innerHTML;
        var contracts = contractSelected.split(':');
        var contractid = contracts[0];
        //var isParent = contracts[1];
        //var canEdit = contracts[2];
        //var canDelete = contracts[3];
        var canCopy = contracts[4];
        //var canView = contracts[5];
        var $selectedRows = $('#searchcontractList').jtable('selectedRows');
        if ($selectedRows.length > 1) {
            ShowWarning(messages.CopyContractValid);
            return false;
        }
        if (contractid != null && contractid != '' && canCopy == "true")
            window.location.href = "/GCS/Contract/CopyContract/" + contractid;
        else if (contractid != null && contractid != '' && canCopy != "true") {
            ShowWarning(messages.copyAccess);
            return false;
        }
        else {
            ShowWarning(messages.copyValid);
            return false;
        }
    });

    //Edit Contract button clik
    $('#EditContract').click(function () {
        var $selectedRows = $('#searchcontractList').jtable('selectedRows');
        if ($selectedRows.length > 1) {
            ShowWarning(messages.oneEditValid);
            return false;
        }
        else if ($selectedRows.length == 0) {
            ShowWarning(messageCommon.editValid);
            return false;
        }

        var contractSelected = $('#SelectedRowList')[0].innerHTML;
        var contracts = contractSelected.split(':');
        var contractid = contracts[0];
        //var isParent = contracts[1];
        var canEdit = contracts[2];
        //var canDelete = contracts[3];
        //var canCopy = contracts[4];
        //var canView = contracts[5];
        if (contractid != null && contractid != '' && canEdit == "true")
            window.location.href = "/GCS/Contract/EditContract/" + contractid;
        else if (contractid != null && contractid != '' && canEdit != "true") {
            ShowWarning(messages.editAccess);
            return false;
        }
    });

    //Delete button
    $('#DeleteContract').click(function () {
        var contractSelected = $('#SelectedRowList')[0].innerHTML;
        var contracts = contractSelected.split(':');
        var id = contracts[0];
        var isParent = contracts[1];
        //var canEdit = contracts[2];
        var canDelete = contracts[3];
        //var canCopy = contracts[4];
        //var canView = contracts[5];
        //&& CanDelete == "true"
        if (id != '' && isParent == "false" && canDelete == "true") {
            var objDeleteDialog = $('<div id="delete"></div>').html('<p>' + messages.confirmDelete + '</p>')
.dialog({ autoOpen: false, modal: true, title: messages.confirm, show: 'clip', hide: 'clip', width: 300 });

            objDeleteDialog.dialog('open');
            objDeleteDialog.dialog({
                buttons:
        {
            'Yes': function () {
                $(this).dialog('close');
                $.get('/GCS/Contract/DeleteContract?id=' + id).error(function () {
                    objSearchContractDialog.html('<p>' + messageCommon.deleteFailure + '</p>');
                    objSearchContractDialog.dialog('open', { title: messageCommon.deleteFailure });
                });
                searchContract();
            },
            'No': function () {
                $(this).dialog('close');
                return false;
            }
        }
            });
        } else if (id != '' && canDelete == "false") {
            ShowWarning(messages.deleteAccess);
            return false;
        }
        else if (id != '' && isParent == "true") {
            ShowWarning(messages.deleteValidParent);
            return false;
        } else if (id != '' && isParent == "false" && canDelete == "false") {
            ShowWarning(messages.deleteAccess);
            return false;
        }
        else {
            ShowWarning(messages.deleteValid);
            return false;
        }
        return false;
    });

    $("#Contract_ContractId").keyup(function () {
        if ($('#Contract_ContractId').val() != "") {
            var value = $('#Contract_ContractId').val().replace(/^\s\s*/, '').replace(/\s\s*$/, '');
            var intRegex = /^\d+$/;
            if (!intRegex.test(value)) {
                value = document.getElementById('Contract_ContractId').value.replace(/[^0-9\.]+/g, '');
            }
            $('#Contract_ContractId').val(value);
        }
    });

    //Edit Contract button clik
    $('#ViewContract').click(function () {
        var $selectedRows = $('#searchcontractList').jtable('selectedRows');
        if ($selectedRows.length > 1) {
            ShowWarning(messages.oneviewValid);
            return false;
        }
        var contractSelected = $('#SelectedRowList')[0].innerHTML;
        var contracts = contractSelected.split(':');
        var contractid = contracts[0];
        //var isParent = contracts[1];
        var canView = contracts[5];
        if (contractid != null && contractid != '' && canView == "true")
            window.location.href = "/GCS/Contract/ViewContract/" + contractid;
        else if (contractid != null && contractid != '' && canView != "true") {
            ShowWarning(messages.viewAccess);
            return false;
        }
        else {
            ShowWarning(messageCommon.viewValid);
            return false;
        }
    });

    //For generating Excel report for Contract Search Results
    $('#ContractListExcelExport').click(function (e) {
        e.preventDefault();
        var paging = $('#SearchContractPaging select').val();
        pageSize = paging;
        var workflowindex = $('#WorkFlowStatus')[0].selectedIndex;
        var workflowtext = $('#WorkFlowStatus')[0][workflowindex].text;
        var hasSearchCriteria = "";
        if (workflowtext == 'All')
            workflowtext = '';
        var contractindex = $('#ContractStatusList')[0].selectedIndex;
        var contracttext = $('#ContractStatusList')[0][contractindex].text;
        if (contracttext == 'All')
            contracttext = '';
        var clearanceAdminCompany = $('#ClearanceAdminCompany')[0].selectedIndex;
        var clearanceAdminCompanyText = $('#ClearanceAdminCompany')[0][clearanceAdminCompany].text;
        if (clearanceAdminCompanyText == '-- Select --')
            clearanceAdminCompanyText = '';
        var rightsType = $('#RightsType')[0].selectedIndex;
        var rightsTypeText = $('#RightsType')[0][rightsType].text;
        if (rightsTypeText == 'All')
            rightsTypeText = '';
        if (workflowtext != '' || contracttext != '' || rightsTypeText != '' || clearanceAdminCompanyText != '' || $('#Artist_Info_Name').val() != '' || $('#Contract_ContractingParty').val() != '' || $('#Contract_ArtistNameInLocalCharacters').val() != '' || $('#Contract_UmgSigningCompany').val() != '' || $('#Contract_LocalContractRefNumber').val() != '' || $('#Contract_ContractCommencementDate').val() != '' || $('#Contract_ContractEndDate').val() != '' || $('#ContractsSearch_OtherRights').val() != '' || $('#Contract_ContractId').val() != '')
            hasSearchCriteria = "true";

        var parameters = {
            artistName: $('#Artist_Info_Name').val(),
            workflowStatus: workflowtext,
            contractStatus: contracttext,
            contractingParty: $('#Contract_ContractingParty').val(),
            clearanceAdminCompany: clearanceAdminCompanyText,
            artistNameInLocalCharacters: $('#Contract_ArtistNameInLocalCharacters').val(),
            umgSigningCompanyId: $('#Contract_UmgSigningCompany').val(),
            localContractRefNumber: $('#Contract_LocalContractRefNumber').val(),
            contractCommencementDate: $('#Contract_ContractCommencementDate').val(),
            contractEndDate: $('#Contract_ContractEndDate').val(),
            rightsType: rightsTypeText,
            hasSearchCriteria: hasSearchCriteria,
            otherRights: $('#ContractsSearch_OtherRights').val(),
            contractId: $('#Contract_ContractId').val(),
            pageSize: paging
        };

        window.location = '/GCS/Contract/CreateContractSearchResultExcel?' + $.param(parameters);
    });

    //For generating Word report for Contract Search Results
    $('#ContractListWordExport').click(function (e) {
        e.preventDefault();
        var workflowindex = $('#WorkFlowStatus')[0].selectedIndex;
        var workflowtext = $('#WorkFlowStatus')[0][workflowindex].text;
        if (workflowtext == 'All')
            workflowtext = '';
        var contractindex = $('#ContractStatusList')[0].selectedIndex;
        var contracttext = $('#ContractStatusList')[0][contractindex].text;
        if (contracttext == 'All')
            contracttext = '';
        var clearanceAdminCompany = $('#ClearanceAdminCompany')[0].selectedIndex;
        var clearanceAdminCompanyText = $('#ClearanceAdminCompany')[0][clearanceAdminCompany].text;
        if (clearanceAdminCompanyText == '-- Select --')
            clearanceAdminCompanyText = '';
        var rightsType = $('#RightsType')[0].selectedIndex;
        var rightsTypeText = $('#RightsType')[0][rightsType].text;
        if (rightsTypeText == '-- Select --')
            rightsTypeText = '';

        var parameters = {
            artistName: $('#Artist_Info_Name').val(),
            workflowStatus: workflowtext,
            contractStatus: contracttext,
            contractingParty: $('#Contract_ContractingParty').val(),
            clearanceAdminCompany: clearanceAdminCompanyText,
            artistNameInLocalCharacters: $('#Contract_ArtistNameInLocalCharacters').val(),
            umgSigningCompanyId: $('#Contract_UmgSigningCompany').val(),
            localContractRefNumber: $('#Contract_LocalContractRefNumber').val(),
            contractCommencementDate: $('#Contract_ContractCommencementDate').val(),
            contractEndDate: $('#Contract_ContractEndDate').val(),
            contractId: $('#Contract_ContractId').val(),
            rightsType: rightsTypeText,
            otherRights: $('#ContractsSearch_OtherRights').val()
        };

        window.location = '/GCS/Contract/CreateContractSearchResultWord?' + $.param(parameters);
    });

    //To generate Word Document for the Selected Contract
    $('#ExportToWord').click(function () {
        var contractSelected = $('#SelectedRowList')[0].innerHTML;
        var contracts = contractSelected.split(':');
        var contractid = contracts[0];
        //var isParent = contracts[1];
        var canView = contracts[5];
        if (contractid != null && contractid != '' && canView == "true")
            window.location = '/GCS/Contract/CreateContractInformationWord?contractId=' + contractid;
        else if (contractid != null && contractid != '' && canView != "true") {
            ShowWarning(messages.viewAccess);
            return false;
        }
        else {
            ShowWarning(messageCommon.viewValid);
            return false;
        }
    });
});

$(document).ready(function () {
    //AutoComplete Artist
    var target1 = $("#Artist_Info_Name");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"), minLength: 2,
        select: function (event, ui) {
            $("#Artist_Info_Name").val(ui.item.value);
        }
    });

    $(".searchContractScroll").scroll(function () { $(".ui-autocomplete").hide(); });

    //Accordion style collapse/expand
    $('#searchContractAccordion .head').click(function (e) {
        e.preventDefault();
        $(this).find('a').toggleClass('iconBottom');
        $('.accItem').toggle();
        return false;
    }).next().show();

    //    $('#main').click(function () {
    //        $('#warning').hide();
    //        $('#success').hide();
    //        reSizeSearchPage();
    //        return false;
    //    });
});

function reSizeSearchPage() {
    var h = $(window).height();
    // $("#searchcontractList").css('height', h - 340);
    //$(".jtable-main-container").css('height', h - 340);
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".searchContractScroll").css('height', h - 180);
    else
        $(".searchContractScroll").css('height', h - 200);
    // $(".searchContractScroll").css('height', h - 185);

    $("#searchContractBtnContainer").css('left', $(window).width() - $("#searchContractBtnContainer").width() - 16);

    $("#searchContractAccordion").css('width', $(window).width() - 32);

    $("#contractgrid").css('width', $(window).width() - 30);
}

function searchContract() {
    var workflowindex = $('#WorkFlowStatus')[0].selectedIndex;
    var workflowtext = $('#WorkFlowStatus')[0][workflowindex].text;
    var hasSearchCriteria = "";
    if (workflowtext == 'All')
        workflowtext = '';
    var contractindex = $('#ContractStatusList')[0].selectedIndex;
    var contracttext = $('#ContractStatusList')[0][contractindex].text;
    if (contracttext == 'All')
        contracttext = '';
    var clearanceAdminCompany = $('#ClearanceAdminCompany')[0].selectedIndex;
    var clearanceAdminCompanyText = $('#ClearanceAdminCompany')[0][clearanceAdminCompany].text;
    if (clearanceAdminCompanyText == '-- Select --')
        clearanceAdminCompanyText = '';
    var rightsType = $('#RightsType')[0].selectedIndex;
    var rightsTypeText = $('#RightsType')[0][rightsType].text;
    if (rightsTypeText == 'All')
        rightsTypeText = '';
    if (workflowtext != '' || contracttext != '' || rightsTypeText != '' || clearanceAdminCompanyText != '' || $('#Artist_Info_Name').val() != '' || $('#Contract_ContractingParty').val() != '' || $('#Contract_ArtistNameInLocalCharacters').val() != '' || $('#Contract_UmgSigningCompany').val() != '' || $('#Contract_LocalContractRefNumber').val() != '' || $('#Contract_ContractCommencementDate').val() != '' || $('#Contract_ContractEndDate').val() != '' || $('#ContractsSearch_OtherRights').val() != '' || $('#Contract_ContractId').val() != '')
        hasSearchCriteria = "true";
    $('#searchcontractList').jtable('load', {
        artistName: $('#Artist_Info_Name').val(),
        workflowStatus: workflowtext,
        contractStatus: contracttext,
        contractingParty: $('#Contract_ContractingParty').val(),
        clearanceAdminCompany: clearanceAdminCompanyText,
        artistNameInLocalCharacters: $('#Contract_ArtistNameInLocalCharacters').val(),
        umgSigningCompanyId: $('#Contract_UmgSigningCompany').val(),
        localContractRefNumber: $('#Contract_LocalContractRefNumber').val(),
        contractCommencementDate: $('#Contract_ContractCommencementDate').val(),
        contractEndDate: $('#Contract_ContractEndDate').val(),
        rightsType: rightsTypeText,
        hasSearchCriteria: hasSearchCriteria,
        otherRights: $('#ContractsSearch_OtherRights').val(),
        contractId: $('#Contract_ContractId').val()
    });
}

$(document).ready(function () {
    $('#Artist_Info_Name').focus();
});

//Unlink Button
$(document).ready(function () {
    $('#btnUnlinkContract').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('#warning').hide();

        var $selectedRows = $('#searchcontractList').jtable('selectedRows');
        if ($selectedRows.length > 0) {
            $selectedRows.each(function () {
                var record = $(this).data('record');
                $('#Contract_ContractId').val(record.ContractId);
            });
        }
        var contractSelected = $('#SelectedRowList')[0].innerHTML;
        var contracts = contractSelected.split(':');
        var contractid = contracts[0];
        var canLink = contracts[6];
        var canLinkApproved = contracts[7];
        if (contractid != null && contractid != '' && canLink == "true" && canLinkApproved == "true") {
            var unlinkRepFromContract = $('<div id="UnlinkContractFromRep"></div>')
                              .html('<p>' + messageCommon.onLoading + '</p>')
                              .dialog({
                                  autoOpen: false,
                                  modal: true,
                                  title: 'Please Confirm',
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
        }
        else if (contractid != null && contractid != '' && canLink != "true") {
            ShowWarning(messages.unlinkAccess);
            return false;
        }
        else if (contractid != null && contractid != '' && canLink != "false" && canLinkApproved != "true") {
            ShowWarning(messages.ApprovedUnlinkValid);
            return false;
        }
        else {
            ShowWarning(messages.unlinkValid);
            return false;
        }
    });
});
$('.btncancelUnlink').click(function () {
    $('#UnlinkContractFromRep').dialog('close');
});