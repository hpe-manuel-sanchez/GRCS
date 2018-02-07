var headerText = "<th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'> <input type='checkbox' /></span></div></th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'> Contract Id</span></div></th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'>   Artist Id</span></div> </th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'>    Artist Name</span></div></th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'>   Clearance Admin Company</span></div></th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'>     Clearance Admin Id</span></div></th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'>    Resource Linked</span></div> </th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'>    Contract Status</span></div></th><th class='jtable-column-header'><div class='jtable-column-header-container'><span class='jtable-column-header-text'>   Loss of Rights</span></div> </th>";
$(document).ready(function () {
    $("#searchResultCount").hide();
    $("#SearchContractPaging").hide();
    $("#btnLinkedToResource").hide();
    $("#btnClose").hide();

    if (_activeRoleGroup == "RCCAdmin") {
        $("#btnCreateContract").show();
    }
    else {
        $("#btnCreateContract").hide();
    }

    if ($("#tblArtistContractResult")[0] != undefined && $("#tblArtistContractResult")[0].rows.length > 0) {
        $("#divSaveCancel").show();
    }
});

function cancel() {
    // $('#Territory').dialog('close');
    $("#searchResultCount").hide();
    $("#SearchContractPaging").hide();
    $("#btnLinkedToResource").hide();
    $("#btnClose").hide();
    $("#contractgrid").hide();
    $('#tblArtistContractResult').show();
    $('#btnContractLinkingSave').show();
    $('#btnCancel').show();
}

$("#btnClose").click(function () {
    hideError();
    cancel();
});

$("#btnCreateContract").click(function (e) {
    e.preventDefault();
    e.stopPropagation();

    var objManageContract = $('<div id="createContract" style="margin:0;padding:0;"></div>');

    objManageContract.dialog({
        autoOpen: true,
        width: 800,
        title: 'Create Contract',
        resizable: false,
        modal: true,

        open: function () {
            $(this).load("/GCS/ClearanceInbox/CreateClearanceContract");
        },
        close: function () {
            $(this).remove(); // ensures any form variables are reset.
            $(this).dialog('close');
            $('#loadingDivPA').hide();
        }
    });
    objManageContract.dialog('open');
    var dialogue = $('.ui-dialog');
    dialogue.animate({
        top: "40px"
    }, 0);
});

$("#btnContractLinkingSave").click("click", function (e) {
    e.preventDefault();
    e.stopPropagation();
    var inputParam = "";
    var existingContractId = $('#hdnContractId').val()
    var contractKeyList = existingContractId.split(',');
    var newlySelectedContractsList = new Array();
    var i = 0;
    $('.chkClass').each(function () {
        if ($(this).is(":checked")) {
            var id = $(this).attr('id');
            var tempId = id.replace('chk', '');
            newlySelectedContractsList[i] = tempId;
            inputParam += tempId + "~false" + ",";
        }
        i++;
    });

    for (var i = 0; i < contractKeyList.length; i++) {
        if (contractKeyList[i] != "") {
            var index = newlySelectedContractsList.indexOf(contractKeyList[i]);
            if (index == -1) {
                inputParam += contractKeyList[i] + "~true" + ",";
            }
        }
    }

    if (inputParam == "") {
        showError("Please Select atleast one Contract");
    }
    else {
        if ($('#hdnResourceIdSelectedRequests').val() == null || $('#hdnResourceIdSelectedRequests').val() == undefined || $('#hdnResourceIdSelectedRequests').val() == '') {
            var values =
            {
                "contractIdList": inputParam,
                "resourceId": $('#hdnResourceId').val(),
                "routedItemId": $('#hdnRoutedItemId').val()
            };

            $.ajax({
                type: 'POST',
                url: '/GCS/ClearanceInbox/SaveContractResourceLinking',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(values),
                success: function (response) {
                    $('#ManageContractPopup').dialog('close');
                    $('#ManageContractPopup').remove();

                    var clrProjectId = _clrProjectId_RCCAdmin;
                    var workGroupId = _workGroupId_RCCAdmin;
                    var folderId = _folderId_RCCAdmin;
                    var postDataR = {
                        args: null,
                        roleGroup: RoleGroup.RCCAdmin,
                        clearanceInboxRequestAction: {
                            WorkgroupId: workGroupId,
                            FolderId: folderId,
                            ProjectId: clrProjectId,
                            RoleName: _roleName
                        },
                        //gridType: 0
                        gridType: $("#tabsRCCAdmin").tabs('option', 'selected') + 1
                    };
                    //RefreshRightPanel(postDataR);
                    RefreshRightpanelGrid(postDataR);
                }
            });
        }
        else {
            var values =
            {
                "contractIdList": inputParam,
                "resourceIds": $('#hdnResourceIdSelectedRequests').val()
            };

            $.ajax({
                type: 'POST',
                url: '/GCS/ClearanceInbox/SaveContractResourceLinkingSelectedRequests',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(values),
                success: function (response) {
                    $('#ManageContractPopup').dialog('close');
                    $('#ManageContractPopup').remove();

                    var clrProjectId = _clrProjectId_RCCAdmin;
                    var workGroupId = _workGroupId_RCCAdmin;
                    var folderId = _folderId_RCCAdmin;
                    var postDataR = {
                        args: null,
                        roleGroup: RoleGroup.RCCAdmin,
                        clearanceInboxRequestAction: {
                            WorkgroupId: workGroupId,
                            FolderId: folderId,
                            ProjectId: clrProjectId,
                            RoleName: _roleName
                        },
                        //gridType: 0
                        gridType: $("#tabsRCCAdmin").tabs('option', 'selected') + 1
                    };
                    //RefreshRightPanel(postDataR);
                    RefreshRightpanelGrid(postDataR);
                }
            });
        }
    }
});

function cancelResoucePopup() {
    $('#ManageContractPopup').dialog('close');
    $('#ManageContractPopup').remove();
}

$("#btnCancel").click(function () {
    hideError();
    cancelResoucePopup();
});

$('#searchBtn').click(function (e) {
    $('#tblArtistContractResult').hide();
    $('#btnContractLinkingSave').hide();
    $('#btnCancel').hide();
    $('#warning').hide();
    $("#SearchContractPaging").hide();
    e.preventDefault();
    e.preventDefault();
    clearanceSearchContract();
    $('#searchcontractList').show();

    //reSizeSearchPage();
});

function clearanceSearchContract() {
    hideError();
    var element = document.getElementById('contractgrid');
    element.style.display = 'block';
    var showDropDown = document.getElementById('SearchContractPaging');
    showDropDown.style.display = 'block';
    $('#searchcontractList').show();
    var contractID = $.trim($('#txtContractID').val());
    var clearanceAdminCompanyCountry = $.trim($('#txtClearanceAdminCompanyCountry').val());
    var contractStatus = $.trim($('#txtContractStatus').val());
    var contractingParty = $.trim($('#txtContractingParty').val());
    var artist = $.trim($('#txtArtist').val());
    var artistId = $.trim($('#txtArtistId').val());
    var artistInLocalChar = $.trim($('#txtArtistInLocalChar').val());
    var rightType = $.trim($('#txtRightType').val());
    var localContractRef = $.trim($('#txtLocalContractRef').val());
    var uMGISingComp = $.trim($('#txtUMGISingComp').val());
    var SelectedResourcesCol = "";
    if ($('#hdnResourceIdSelectedRequests').val() == null || $('#hdnResourceIdSelectedRequests').val() == undefined || $('#hdnResourceIdSelectedRequests').val() == '') {
        SelectedResourcesCol = $('#hdnResourceId').val();
    }
    else {
        SelectedResourcesCol = $('#hdnResourceIdSelectedRequests').val();
    };

    if (contractID == '' && clearanceAdminCompanyCountry == '' && contractStatus == '' && contractingParty == '' && artist == '' && artistId == '' && artistInLocalChar == '' && rightType == '' && localContractRef == '' && uMGISingComp == '') {
        showError(mandatorySearchCriteria);
        $("#btnLinkedToResource").hide();
        showDropDown.style.display = 'none';
        $("#btnClose").show();
        return false;
    }
    else {
        hideError();
    }

    $('#searchcontractList').jtable({
        paging: true,
        pageSize: $('#SearchContractPaging select').val(),
        sorting: true,
        defaultSorting: 'ContractId ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/ClearanceInbox/ClearanceSearchContractWithParam'
        },
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
            //$('#SearchAlert').hide();
        },
        // //For tooltip

        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            if (rowIndex != 0) {
                $("#searchResultCount").show();
                $("#SearchContractPaging").show();
                $("#btnLinkedToResource").show();
                $("#btnClose").show();
                $("#contractgrid").show();
            }
            else {
                $("#SearchContractPaging").hide();
            }

            document.getElementById("searchResultCount").innerHTML = "Search Result (" + rowIndex + ")";
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            $("#searchcontractList input").removeAttr("checked");
            $("#searchcontractList tr").removeClass("jtable-row-selected");
            $("#contractgrid").show();
            $("#btnClose").show();
        },

        fields: {
            ContractId: {
                title: headerContractId,
                key: true,
                list: true,
                display:
                    function (Project) {
                        return RemoveNullValue(Project.record.ContractId);
                    }
            },
            ArtistId: {
                title: headerArtistId,
                list: true,
                display:
                    function (Project) {
                        return RemoveNullValue(Project.record.ArtistId);
                    }
            },
            ArtistName: {
                title: headerArtistName,
                width: '16%',
                display:
                    function (Project) {
                        return RemoveNullValue(Project.record.ArtistName);
                    }
            },
            ClearanceCompanyCountry: {
                title: headerClearanceAdminCompany,
                width: '15%',
                display:
                    function (Project) {
                        return RemoveNullValue(Project.record.ClearanceCompanyCountry);
                    }
            },
            ClearanceCompanyCountryId: {
                title: headerClearanceAdminId,
                width: '10%',
                display:
                    function (Project) {
                        return RemoveNullValue(Project.record.ClearanceCompanyCountryId);
                    }
            },
            HasRepertoire: {
                title: headerResourceLinked,
                width: '10%',
                display:
                    function (Project) {
                        return ResetBoolVal(Project.record.HasRepertoire);
                    }
            },

            ContractStatus: {
                title: headerContractStatus,
                width: '15%',
                display:
                    function (Project) {
                        return RemoveNullValue(Project.record.ContractStatus);
                    }
            },
            IsLossRightsIndicator: {
                title: headerLossOfRights,
                width: '10%',
                display:
                    function (Project) {
                        return ResetBoolVal(Project.record.IsLossRightsIndicator);
                    }
            }
        },
        selectionChanged: function () {
            var $selectedRows = $('#searchcontractList').jtable('selectedRows');
            $('#SelectedRowList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    // $('#selectedContractId').append(record.ContractId + ',');
                    $('#SelectedRowList').append(record.ContractId + ',');
                    //   var recdisplay = document.getElementById('SelectedRowList');
                    $('#SelectedRowList').hide();
                });
            }
        }
    });

    //  $('#searchcontractList').jtable('load', { });
    $('#searchcontractList').jtable('load', {
        artistName: artist,
        contractingParty: contractingParty,
        umgSigningCompanyId: uMGISingComp,
        artistNameInLocalCharacters: artistInLocalChar,
        contractStatus: contractStatus,
        localContractRefNumber: artistInLocalChar,
        rightsType: rightType,
        clearanceAdminCompany: clearanceAdminCompanyCountry,
        contractId: contractID,
        artistId: artistId,
        ResourceId: SelectedResourcesCol
    });
}

function showError(message) {
    $("#divErrorMessage").text(message);
    $('#divErrorMessage').addClass('warning');
    $("#divErrorMessage").show();
}

function hideError() {
    $("#divErrorMessage").text('');
    $('#divErrorMessage').removeClass('warning');
    $("#divErrorMessage").hide();
}

function showHide(obj, id) {
    id = $(obj).closest("td").find("img:first").attr("id");
    var rowId = id.toString().substring(4, id.length);
    var cla = $(obj).closest("td").find("img:first").attr("class");
    if (cla == "imgHide1") {
        $(obj).closest("td").find("img:first").attr("src", '/GCS/Images/Arrow-right-new_trn.png');

        $(obj).closest("td").find("img:first").removeClass("imgHide1");
        $(obj).closest("td").find("img:first").addClass("imgShow");
        $('#contractSearch' + rowId).hide();
    } else {
        $(obj).closest("td").find("img:first").attr("src", '/GCS/Images/arrow-down.png');
        $(obj).closest("td").find("img:first").removeClass("imgShow");
        $(obj).closest("td").find("img:first").addClass("imgHide1");
        $('#contractSearch' + rowId).show();
    }
}

$('#btnLinkedToResource').click(function (e) {
    var $selectedRows = $('#searchcontractList').jtable('selectedRows');
    if ($selectedRows.length == 0) {
        showError("Please Select atleast one project");
        return false;
    }

    $("#searchResultCount").hide();
    $("#SearchContractPaging").hide();
    $("#btnLinkedToResource").hide();
    $("#contractgrid").hide();
    $("#btnClose").hide();
    $('#tblArtistContractResult').show();
    $('#btnContractLinkingSave').show();
    $('#btnCancel').show();

    var contractIdArray = new Array();
    var artistIdArray = new Array();
    var i = 0;
    $('.chkClass').each(function () {
        var id = $(this).attr('id');
        var tempId = id.replace('chk', '');
        contractIdArray[i] = tempId;
        artistIdArray[$(this).attr('name')] = $(this).val();

        i++;
    });

    $selectedRows.each(function () {
        var record = $(this).data('record');
        var index = contractIdArray.indexOf(record.ContractId);
        if (index >= 0) {
            $("#chk" + record.ContractId).attr('checked', "checked");
        }
        else {
            var record = $(this).data('record');
            // artistIdArray = artistIdArray.distinct();
            var index = artistIdArray.indexOf(record.ArtistId);
            if (index >= 0) {
                appendRowInTable(record, record.ArtistId);
            }
            else {
                CreateMainTable(record);
            }
        }
    });
});

function appendRowInTable(record, index) {
    $("#divSaveCancel").show();
    var dataRow = $("<tr>");
    var tbody = $("#contractSearch" + index);
    if (tbody[0].rows.length == 1) {
        // $('table' + tbody + ' tr#1').remove();
        $("#contractSearch" + index + "> tbody").empty();
        //  $("#contractSearch"+ index +" tr:eq(2)").remove();
        $("<tr>" + headerText + "</tr>").appendTo(tbody);
    }
    $("<td>").append("<input type='checkbox' class='chkClass' checked= true id='chk" + record.ContractId + "' value=" + record.ArtistId + " name=" + index + " />").data("col", 1).appendTo(dataRow);
    $("<td>").append(RemoveNullValue(record.ContractId)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ArtistId)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ArtistName)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ClearanceCompanyCountry)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ClearanceCompanyCountryId)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + ResetBoolVal(record.HasRepertoire)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ContractStatus)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + ResetBoolVal(record.IsLossRightsIndicator)).data("col", 1).appendTo(dataRow);

    dataRow.appendTo(tbody);
}

function CreateMainTable(record) {
    $("#divSaveCancel").show();
    var rowLength = $("#tblArtistContractResult")[0] == undefined ? 0 : $("#tblArtistContractResult")[0].rows.length;
    var tbody = $("#tblArtistContractResult");
    var tableBody = $("<table width='890px;'>");
    var trow = $("<tr class='grey-background'>");
    $("<td  colspan ='9' align='left'>")
        .append("<a class='details' href='#' onclick='showHide(this, " + rowLength + ")'><img id='img_" + rowLength + "' class='imgHide1' src='/GCS/Images/arrow-down.png' alt='Expand'/></a>&nbsp;<b>" + record.ArtistName + "</b>")
        .data("col", 0)
        .appendTo(trow);
    trow.appendTo(tableBody);

    //Main table data
    var mainTableDataBody = $("<table  class='jtable' id ='contractSearch" + record.ArtistId + "'><tr>" + headerText + "</tr>");

    var dataRow = $("<tr>");

    $("<td>").append("<input type='checkbox' class='chkClass' checked= true id='chk" + record.ContractId + "' value=" + record.ArtistId + "  name=" + rowLength + " />").data("col", 1).appendTo(dataRow);
    $("<td>").append(RemoveNullValue(record.ContractId)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ArtistId)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ArtistName)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ClearanceCompanyCountry)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ClearanceCompanyCountryId)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + ResetBoolVal(record.HasRepertoire)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + RemoveNullValue(record.ContractStatus)).data("col", 1).appendTo(dataRow);
    $("<td>").append("" + ResetBoolVal(record.IsLossRightsIndicator)).data("col", 1).appendTo(dataRow);

    dataRow.appendTo(mainTableDataBody);

    var mainTableDataRow = $("<tr>");
    var mainTableDataCol = $("<td colspan='9'>");
    mainTableDataBody.appendTo(mainTableDataCol);
    mainTableDataCol.appendTo(mainTableDataRow);
    mainTableDataRow.appendTo(tableBody);

    var divid = $("<div class='Clear' style='margin-left: 15px; position: relative;height:auto;'>");
    tableBody.appendTo(divid);
    var mainTableRow = $("<tr>");
    var mainTableCol = $("<td>");
    divid.appendTo(mainTableCol);
    mainTableCol.appendTo(mainTableRow);
    mainTableRow.appendTo(tbody);
}

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (needle) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] === needle) {
                return i;
            }
        }
        return -1;
    };
}

$('#resetBtn').click(function (e) {
    $('#txtContractID').val('');
    $('#txtClearanceAdminCompanyCountry').val('');
    $('#txtContractStatus').val('');
    $('#txtContractingParty').val('');
    $('#txtArtist').val('');
    $('#txtArtistId').val('');
    $('#txtArtistInLocalChar').val('');
    $('#txtRightType').val('');
    $('#txtLocalContractRef').val('');
    $('#txtUMGISingComp').val('');
});

function RemoveNullValue(objectValue) {
    if (objectValue == undefined || objectValue == null)

        return "";
    else
        return objectValue;
}

function ResetBoolVal(objectValue) {
    if (objectValue == undefined || objectValue == null || objectValue == "False" || objectValue == "false" || objectValue == false)

        return "No";
    else
        return "Yes";
}

Array.prototype.distinct = function () {
    var derivedArray = [];
    for (var i = 0; i < this.length; i += 1) {
        if (!derivedArray.contains(this[i])) {
            derivedArray.push(this[i])
        }
    }
    return derivedArray;
};