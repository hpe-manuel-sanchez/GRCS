var requestIds = '';
var workgroupId = '';
var parentId = '';
var pendingworkgroupName = '';
var searchlist = '';
var jtableActionList = '';
var pagingCount = "";
var selectedContractIds = "";
var workgroupType = "parent";
var frompage = "";
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function () {
    document.getElementById('divSearchHeader').innerHTML = divSearchHeader;
    if (pageName == 'Deactivate') {
        $("#searchList").addClass('jtableDvwithDrop');
        requestIds = '';
        requestIds = requestIDs;
        workgroupId = workgroupID;
        parentId = parentID;
        isParent = IsParent;
        pendingworkgroupName = workgroupName;
        jtableActionList = '/GCS/Workgroup/GetWorkgroupByChild';
        btndeactivateDisaible();
        btnsearchpartialviewDisiable();
        $("#ddlPartialRolesList").hide();
        if (parentId != 0) {
            searchlist = '  ';
            loadWorkgroupresult();
        }
    }
    else if (pageName == 'Delete') {
        jtableActionList = '/GCS/Workgroup/SearchWorkgroup';
        btndeactivateDisaible();
        btnsearchpartialviewDisiable();
        $("#ddlPartialRolesList").show();
    }
    else if (pageName == 'ManageWorkgroup') {
        jtableActionList = '/GCS/Workgroup/SearchWorkgroup';
        btndeactivateDisaible();
        btnsearchpartialviewDisiable();
        $("#ddlPartialRolesList").show();
    }
    else if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
        $("#searchList").addClass('jtableDvwithDrop');
        jtableActionList = '/GCS/Workgroup/GetWorkgroups';
        searchlist = '  ';
        if (pageName == 'LinkWGToArtistContract') {
            selectedContractIds = selectedValues;
        } else {
            selectedContractIds = selectedContractIdValues;
        }
        btndeactivateDisaible();
        btnsearchpartialviewDisiable();
        $("#ddlPartialRolesList").show();
        loadWorkgroupresult();
    }

    if (pageName == 'Deactivate' || pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
        $('#headerWGScreen').hide();
    }
    else {
        $('#headerWGScreen').show();
    }

    $('#ddlPartialRolesList').keypress(function (event) {
        var selectedRoleIndex = $('#ddlPartialRolesList')[0].selectedIndex;
        if (selectedRoleIndex != 0 && selectedRoleIndex != -1) {
            if (event.keyCode == '13') {
                $('#btnSearchWorkGroup').click();
            }
        }
    });

    $("#txtSearchName").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    });
    $("#txtSearchName").blur(function (event) {
        if (!$('#txtSearchName').is(":focus"))
            $('#txtSearchName').removeClass("ui-autocomplete-loading");
    });

    //Suggestive box for WorkGroupName
    $("#txtSearchName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "WorkgroupName" },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtSearchName").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtSearchName').is(":focus"))
                                return records;
                            else {
                                autocomplete.disabled = true;
                                return;
                            }
                        }));
                }
            });
        },
        minLength: 3,
        focus: function () {
            return false;
        },
        select: function (event, ui) {
            if (ui.item == null) {
                return;
            }
            else {
                $('#txtSearchName').val("");
                $('#txtSearchName').val(ui.item.value);
            }
            return false;
        }
    });

    //suggestivebox for Company
    $("#txtSearchCompany").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    });
    $("#txtSearchCompany").blur(function (event) {
        if (!$('#txtSearchCompany').is(":focus"))
            $('#txtSearchCompany').removeClass("ui-autocomplete-loading");
    });

    $("#txtSearchCompany").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "Company", pageName: pageName },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtSearchCompany").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtSearchCompany').is(":focus"))
                                return records;
                            else {
                                autocomplete.disabled = true;
                                return;
                            }
                        }));
                }
            });
        },
        minLength: 3,
        focus: function () {
            return false;
        },
        select: function (event, ui) {
            if (ui.item == null) {
                return;
            }
            else {
                $('#txtSearchCompany').val("");
                $('#txtSearchCompany').val(ui.item.value);
            }
            return false;
        }
    });
    $("#txtSearchUser").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    });
    $("#txtSearchUser").blur(function (event) {
        if (!$('#txtSearchUser').is(":focus"))
            $('#txtSearchUser').removeClass("ui-autocomplete-loading");
    });
    //suggestivebox for User
    $("#txtSearchUser").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "User" },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtSearchUser").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtSearchUser').is(":focus"))
                                return records;
                            else {
                                autocomplete.disabled = true;
                                return;
                            }
                        }));
                }
            });
        },
        minLength: 3,
        focus: function () {
            return false;
        },
        select: function (event, ui) {
            if (ui.item == null) {
                return;
            }
            else {
                $('#txtSearchUser').val("");
                $('#txtSearchUser').val(ui.item.value);
            }
            return false;
        }
    });

    $("#txtSearchCountry").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    });
    $("#txtSearchCountry").blur(function (event) {
        if (!$('#txtSearchCountry').is(":focus"))
            $('#txtSearchCountry').removeClass("ui-autocomplete-loading");
    });
    //suggestivebox for Country
    $("#txtSearchCountry").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "Country" },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtSearchCountry").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtSearchCountry').is(":focus"))
                                return records;
                            else {
                                autocomplete.disabled = true;
                                return;
                            }
                        }));
                }
            });
        },
        minLength: 3,
        focus: function () {
            return false;
        },
        select: function (event, ui) {
            if (ui.item == null) {
                return;
            }
            else {
                $('#txtSearchCountry').val("");
                $('#txtSearchCountry').val(ui.item.value);
            }
            return false;
        }
    });
});

$(function () {
    jQuery("#txtSearchName").watermark(watermarkname);
    jQuery("#txtSearchCompany").watermark(watermarkcompany);
    jQuery("#txtSearchUser").watermark(watermarkuser);
    jQuery("#txtSearchCountry").watermark(watermarkcountry);
});

function multibleSelection() {
    if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
        return true;
    } else {
        return false;
    }
}

$(function () {
    $('#btnSearchWorkGroup').click(function (e) {
        if (pageName == 'ManageWorkgroup') {
            loadManageWorkgroupresult();
        }
        else {
            if (pageName == 'Delete') {
                $('#deleteWgSuccessMessage').hide();
                $('#deleteWgSuccessMessage').html('');
                $('#deleteWgInfoMessage').hide();
                $('#deleteWgInfoMessage').html('');
            }
            loadWorkgroupresult();
        }
        applyCustomTheam();
    });
});

$(function () {
    $('#btnRedirectWorkGroup').click(function (e) {
        var ExpectWorkgroupID = '';
        var $selectedRows = $('#searchList').jtable('selectedRows');
        $selectedRows.each(function () {
            var record = $(this).data('record');
            ExpectWorkgroupID = record.ID;
            workgroupModifiedDateTime = record.ModifiedDateTime;
        });
        if (ExpectWorkgroupID != "") {
            var formValues = "ExpectWorkgroupID=" + ExpectWorkgroupID + "&AssignedgtWorkgroupID=" + workgroupId + "&requestIds=" + requestIds;
            $.post('/GCS/Workgroup/RequestReassignToWorkgroup', formValues, function (data) {
                if (data == true) {
                    if (parentId != 0) {
                        isParent = false;
                    }
                    $('#SearchWorkgroupRedirect').dialog('close');
                    showPendingRequest(workgroupId, workgroupModifiedDateTime, parentId, pendingworkgroupName, isParent);
                    $('#deactivateWgPendingRequestInfoMessage').show();
                    $('#deactivateWgPendingRequestInfoMessage').html(searchWorkgroupMessages.pendingworkgroupRequestMessage);
                    $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
                }
                else {
                    $('#SearchWorkgroupRedirect').dialog('close');
                    $('#DisplayPendingRequest').dialog('close');
                    $('#searchWorkGroupList').jtable('deleteRecord', {
                        key: workgroupId,
                        clientOnly: true,
                        animationsEnabled: false
                    });
                    $('#deactivateWgSuccessMessage').show();
                    $('#deactivateWgSuccessMessage').html(searchWorkgroupMessages.deactivateworkgroupSuccessfulMessage);
                    $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
                }
            });
            hideSearchError();
        }
        else {
            showSearchError(searchWorkgroupMessages.noRowsSelected);
        }
    });
});

$(function () {
    $('#btnCancelDeleteWorkGroup').click(function () {
        $('#SearchWorkgroupPartialPaging').hide(); $('#searchList').hide();
        window.location.href = "/GCS/workgroup/";
    });
});

$(function () {
    $('#btnCancelSearchWorkGroup').click(function () {
        if (pageName == 'Deactivate') {
            $('#SearchWorkgroupRedirect').dialog('close');
            return false;
        }
        else if (pageName == 'Delete') {
            window.location.href = "/GCS/workgroup/";
        }
        else if (pageName == 'ManageWorkgroup') {
            window.location.href = "/GCS/workgroup/SearchWorkgroup";
        }
        else if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            $('#divLinkToWorkgroup').dialog('close');
            return false;
        }
    });
});

function loadWorkgroupresult() {
    var selectedRoleIndex = "";
    var name = $('#txtSearchName').val();
    var roleValue = -1;
    if (pageName == 'Deactivate' && parentId == 0) {
        roleValue = selectedRoleID;
    } else {
        selectedRoleIndex = $('#ddlPartialRolesList')[0].selectedIndex;
        var roletext = $('#ddlPartialRolesList')[0][selectedRoleIndex].text;
        var roleID = $('#ddlPartialRolesList')[0][selectedRoleIndex].value;
        if (roleID.length > 0) roleValue = roleID;
    }
    var company = $('#txtSearchCompany').val();
    var user = $('#txtSearchUser').val();
    var country = $('#txtSearchCountry').val();
    if (name != '') { searchlist = searchlist + name + '/'; }
    if (selectedRoleIndex != 0) { searchlist = searchlist + roletext + '/'; }
    if (company != '') { searchlist = searchlist + company + '/'; }
    if (user != '') { searchlist = searchlist + user + '/'; }
    if (country != '') { searchlist = searchlist + country + '/'; }
    searchlist = searchlist.substring(0, searchlist.length - 1);
    document.getElementById('spnSearchPartialResult').innerHTML = searchlist + '&nbsp;<span id="cmpSearchResultRecordcount"/>';
    if (searchlist.length > 0) {
        if (pageName == 'Deactivate' || pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            btnsearchpartialviewVisible();
            $("#searchReassignPanel").show();
        } else if (pageName == 'Delete') {
            btndeactivateVisible();
        }
        var showDropDown = document.getElementById('SearchWorkgroupPartialPaging');
        showDropDown.style.display = 'block';
        $("#searchButtonPanel").show();
        pagingCount = $('#ddlPartialSearchPaging').val();
        $('#searchList').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: true,
            defaultSorting: 'Name ASC',
            selecting: true,
            columnResizable: false,
            selectingCheckboxes: true,
            selectOnRowClick: false,
            multiselect: multibleSelection(),
            actions: {
                listAction: jtableActionList
            },
            fields: {
                ID: {
                    key: true,
                    create: false,
                    edit: false,
                    list: false
                },
                ParentID: {
                    list: false
                },
                Name: {
                    title: wgSearchJtableColNames.wgName, //'Workgroup Name',
                    width: '18%'
                },
                StatusType: {
                    title: wgSearchJtableColNames.wgType, //'Workgroup Type',
                    width: '12%'
                },
                RoleName: {
                    title: wgSearchJtableColNames.wgRole, //'Role',
                    width: '15%'
                },
                Company: {
                    title: wgSearchJtableColNames.company, //'Company',
                    width: '20%',
                    sorting: false
                },
                User: {
                    title: wgSearchJtableColNames.user, //'User',
                    width: '20%',
                    sorting: false
                },
                Country: {
                    title: wgSearchJtableColNames.territoryCountry, //'Territory/Country',
                    width: '15%',
                    sorting: false
                },
                ModifiedDateTime: {
                    dataType: Date,
                    list: false
                }
            },
            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#searchList').jtable('selectedRows');
                $('#SelectedWorkgroupRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#SelectedWorkgroupRowList').append(record.ID + ':' + record.Name + ':' + record.StatusType + ':' + record.ParentID);
                        var recdisplay = document.getElementById('SelectedWorkgroupRowList');
                        recdisplay.style.display = 'none';
                    });
                }
                else {
                    //No rows selected
                }
            },
            recordsLoaded: function (event, data) {
                $('#cmpSearchResultRecordcount').html('(' + data.serverResponse.TotalRecordCount + ')');
                $('#searchList .jtable thead tr th:first').remove();
                $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#searchList .jtable thead tr:first');
                searchlist = '';
            }
        });
        if (pageName == 'Deactivate') {
            $('#searchList').jtable('load', {
                parentId: parentId,
                workgroupId: workgroupId,
                workgroupName: name,
                workgroupRole: roleValue,
                workgroupCompany: company,
                workgroupUser: user,
                workgroupCountry: country
            });
        }
        else if (pageName == 'Delete') {
            $('#searchList').jtable('load', {
                workgroupName: name,
                workgroupRole: roleValue,
                workgroupCompany: company,
                workgroupUser: user,
                workgroupCountry: country,
                isStatus: 'false'
            });
        }
        else if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            $('#searchList').jtable('load', {
                workgroupName: name,
                workgroupRole: roleValue,
                workgroupCompany: company,
                workgroupUser: user,
                workgroupCountry: country,
                contractIds: selectedContractIds
            });
        }
        hideSearchError();
    }
    else {
        showSearchError(searchWorkgroupMessages.searchInValid);
    }
}

$(function () {
    $('#ddlPartialSearchPaging').change(function () {
        var rowCount = $(this).val();
        if (pageName == 'Deactivate' && parentId != 0) {
            searchlist = '  ';
            loadWorkgroupresult(rowCount);
        }
        else if (pageName == 'Deactivate' && parentId == 0) {
            loadWorkgroupresult(rowCount);
        }
        else if (pageName == 'Delete') {
            loadWorkgroupresult(rowCount);
        }
        else if (pageName == 'ManageWorkgroup') {
            loadManageWorkgroupresult(rowCount);
        }
        else if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            searchlist = '  ';
            loadWorkgroupresult(rowCount);
        }
    });
});

function loadManageWorkgroupresult() {
    searchlist = '';
    var name = $('#txtSearchName').val();
    var selectedRoleIndex = $('#ddlPartialRolesList')[0].selectedIndex;
    var roletext = $('#ddlPartialRolesList')[0][selectedRoleIndex].text;
    var roleID = $('#ddlPartialRolesList')[0][selectedRoleIndex].value;
    var roleValue = -1;
    if (roleID.length > 0) roleValue = roleID;
    var company = $('#txtSearchCompany').val();
    var user = $('#txtSearchUser').val();
    var country = $('#txtSearchCountry').val();
    if (name != '') { searchlist = searchlist + name + '/'; }
    if (selectedRoleIndex != 0) { searchlist = searchlist + roletext + '/'; }
    if (company != '') { searchlist = searchlist + company + '/'; }
    if (user != '') { searchlist = searchlist + user + '/'; }
    if (country != '') { searchlist = searchlist + country + '/'; }
    searchlist = searchlist.substring(0, searchlist.length - 1);
    document.getElementById('spnSearchPartialResult').innerHTML = searchlist + '&nbsp;<span id="cmpSearchResultRecordcount"/>';
    if (searchlist.length > 0) {
        var showDropDown = document.getElementById('SearchWorkgroupPartialPaging');
        showDropDown.style.display = 'block';
        pagingCount = $('#ddlPartialSearchPaging').val();
        $('#searchList').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: true,
            columnResizable: false,
            defaultSorting: 'Name ASC',
            actions: {
                listAction: jtableActionList
            },
            fields: {
                ID: {
                    key: true,
                    create: false,
                    edit: false,
                    list: false
                },
                ParentID: {
                    list: false
                },
                Name: {
                    title: wgSearchJtableColNames.wgName, //'Workgroup Name',
                    width: '10%',
                    display: function (data) {
                        var WorkgroupName = data.record.Name;
                        var WorkgroupID = data.record.ID;
                        var WorkgroupType = data.record.StatusType;
                        var workgroupStatus = data.record.Status;
                        return $('<a href="#" style="text-decoration:underline;">' + WorkgroupName + '</a>').click(function () {
                            if (workgroupStatus == 1) {
                                $('#divWorkgroupSearch').empty();
                                var loadingPanel = $($.find('#loadingDv'));
                                if (WorkgroupType.toLowerCase() == "child") {
                                    loadingPanel.show();
                                    window.location.href = "GetChildWorkGroup?workgroupId=" + WorkgroupID;
                                    //loadingPanel.hide();
                                }
                                else {
                                    loadingPanel.show();
                                    // $('#divMaintainWorkgroup').load('/GCS/Workgroup/MaintainParentWorkgroupWithData?workgroupId=' + WorkgroupID);
                                    //  $('#divMaintainWorkgroup').load('/GCS/Workgroup/MaintainParentWorkgroupWithData?workgroupId=' + WorkgroupID);
                                    $('#divMaintainWorkgroup').load('/GCS/Workgroup/MaintainParentWorkgroupWithData?workgroupId=' + WorkgroupID, function () {
                                        loadingPanel.hide();
                                    });

                                    //  loadingPanel.hide();
                                }
                                // Displaying Create, Cancel button
                                document.getElementById('btncreate').style.visibility = 'visible';
                                document.getElementById('btncancel').style.visibility = 'visible';
                            } else {
                                ViewWorkgroup(WorkgroupID, WorkgroupType);
                                return false;
                            }
                        });
                    }
                },
                StatusType: {
                    title: wgSearchJtableColNames.wgType, //'Workgroup Type',
                    width: '7%'
                },
                Status: {
                    title: wgSearchJtableColNames.wgStatus, //'Workgroup Status',
                    width: '7%',
                    display: function (data) {
                        var workgroupStatus = data.record.Status;
                        if (workgroupStatus == 1) {
                            return "Active";
                        } else {
                            return "Deactive";
                        }
                    }
                },
                RoleName: {
                    title: wgSearchJtableColNames.wgRole, //'Role',
                    width: '10%'
                },
                Company: {
                    title: wgSearchJtableColNames.company, //'Company',
                    width: '15%',
                    sorting: false
                },
                User: {
                    title: wgSearchJtableColNames.user, //'User',
                    width: '10%',
                    sorting: false
                },
                Country: {
                    title: wgSearchJtableColNames.territoryCountry, //'Territory/Country',
                    width: '10%',
                    sorting: false
                }
            },
            recordsLoaded: function (event, data) {
                $('#cmpSearchResultRecordcount').html('(' + data.serverResponse.TotalRecordCount + ')');
            },
            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#searchList').jtable('selectedRows');
                $('#SelectedWorkgroupRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#SelectedWorkgroupRowList').append(record.ID + ':' + record.Name + ':' + record.StatusType + ':' + record.ParentID);
                        var recdisplay = document.getElementById('SelectedWorkgroupRowList');
                        recdisplay.style.display = 'none';
                    });
                }
                else {
                    //No rows selected
                }
            }
        });
        hideSearchError();
    }
    else {
        showSearchError(searchWorkgroupMessages.searchInValid);
    }
    $('#searchList').jtable('load', {
        workgroupName: name,
        workgroupRole: roleValue,
        workgroupCompany: company,
        workgroupUser: user,
        workgroupCountry: country,
        isStatus: 'true'
    });
}

function ViewWorkgroup(WorkgroupID, WorkgroupType) {
    $('#searchViewWorkgroup').empty();

    var viewDialog = $('<div id="SearchViewWorkgroup"></div>')
                    .html('<p>' + 'Loading' + '</p>')
                    .dialog({
                        autoOpen: true,
                        modal: true,
                        title: 'View Workgroup',
                        show: 'clip',
                        hide: 'clip',
                        width: "98%",
                        resizable: false,
                        height: 400,
                        position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50],
                        close: function () {
                            $(this).remove(); // ensures any form variables are reset.
                        }
                    });
    $('#divWorkgroupSearch').show();

    //Load partial view
    if (WorkgroupType.toLowerCase() == "child") {
        viewDialog.load('/GCS/Workgroup/ViewChildWorkgroup?WorkgroupId=' + WorkgroupID + "&workgroupType=" + WorkgroupType);
    }
    else {
        viewDialog.load('/GCS/Workgroup/ViewWorkgroup?WorkgroupId=' + WorkgroupID);
    }
}

$(function () {
    $('#btnDeleteWorkGroup').click(function (e) {
        var selectedmodifiedDateTime = "";
        var $selectedRows = $('#searchList').jtable('selectedRows');
        $selectedRows.each(function () {
            var record = $(this).data('record');
            workgroupId = record.ID;
            selectedmodifiedDateTime = record.ModifiedDateTime;
        });
        if ($selectedRows.length > 0) {
            var formValues = "WorkgroupID=" + workgroupId + "&modifiedDateTime=" + selectedmodifiedDateTime + "&Id=Search";
            $.post('/GCS/Workgroup/DeleteWorkgroup', formValues, function (data) {
                if (data == false) {
                    $('#searchList').jtable('deleteRecord', {
                        key: workgroupId,
                        clientOnly: true,
                        animationsEnabled: false
                    });
                    $('#deleteWgSuccessMessage').show();
                    $('#deleteWgSuccessMessage').html(searchWorkgroupMessages.deleteworkgroupSuccessfulMessage);
                    $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
                }
                else if (data == true) {
                    $('#deleteWgSuccessMessage').hide();
                    $('#deleteWgSuccessMessage').html('');
                    $('#deleteWgInfoMessage').show();
                    $('#deleteWgInfoMessage').html(searchWorkgroupMessages.deleteworkgroupPendingrequestMessage);
                }
                else {
                    $('#mgSearchSelectedWorkgroupErrorMessage').show();
                    $('#mgSearchSelectedWorkgroupErrorMessage').html(data.Message);
                }
            });
            hideSearchError();
            $('#deleteWgSuccessMessage').hide();
            $('#deleteWgSuccessMessage').html('');
            $('#deleteWgInfoMessage').hide();
            $('#deleteWgInfoMessage').html('');
        } else {
            showSearchError(searchWorkgroupMessages.noRowsSelected);
            $('#deleteWgSuccessMessage').hide();
            $('#deleteWgSuccessMessage').html('');
            $('#deleteWgInfoMessage').hide();
            $('#deleteWgInfoMessage').html('');
        }
    });
});

$(function () {
    $('#btnViewSearchWorkGroup').click(function (e) {
        var $selectedRows = $('#searchList').jtable('selectedRows');
        $selectedRows.each(function () {
            var record = $(this).data('record');
            workgroupId = record.ID;
            workgroupType = record.StatusType;
        });
        frompage = "viewworkgroup";
        if ($selectedRows.length > 0) {
            var objDialog = $('#ViewWorkgroup')
                 .dialog({
                     autoOpen: true,
                     modal: true,
                     autoOpen: true,
                     modal: true,
                     title: searchWorkgroupMessages.viewworkgrouppopuptitle,
                     show: 'clip',
                     hide: 'clip',
                     resizable: false,
                     height: 400,
                     width: "95%"
                 });
            if (workgroupType.toLowerCase() == "child") {
                objDialog.load('/GCS/Workgroup/ViewChildWorkgroup?WorkgroupID=' + workgroupId + "&workgroupType=" + workgroupType);
            }
            else {
                objDialog.load('/GCS/Workgroup/ViewWorkgroup?WorkgroupID=' + workgroupId);
            }
            hideSearchError();
            $('#deleteWgSuccessMessage').hide();
            $('#deleteWgSuccessMessage').html('');
            $('#deleteWgInfoMessage').hide();
            $('#deleteWgInfoMessage').html('');
            moveDialogBoxAtTop();
        }
        else {
            showSearchError(searchWorkgroupMessages.noRowsSelected);
            $('#deleteWgSuccessMessage').hide();
            $('#deleteWgSuccessMessage').html('');
            $('#deleteWgInfoMessage').hide();
            $('#deleteWgInfoMessage').html('');
        }
    });
});

$(function () {
    $('#btnSearchReset').click(function (e) {
        hideSearchError();
        if (pageName == 'Deactivate' || pageName == 'ManageWorkgroup' || pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            //Bug Fix for 4714
            //			$('#searchList').jtable('destroy');
            //			$("#searchReassignPanel").hide();
            //			var showDropDown = document.getElementById('SearchWorkgroupPartialPaging');
            //			showDropDown.style.display = 'none';
            //Bug Fix for 4714
        }
        //		else
        if (pageName == 'Delete') {
            //Bug Fix for 4714
            //			$('#searchList').jtable('destroy');
            //			$("#buttonPanel").hide();
            //			var showDropDown = document.getElementById('SearchWorkgroupPartialPaging');
            //			showDropDown.style.display = 'none';
            //			btndeactivateDisaible();
            //Bug Fix for 4714
            $('#deleteWgSuccessMessage').hide();
            $('#deleteWgSuccessMessage').html('');
            $('#deleteWgInfoMessage').hide();
            $('#deleteWgInfoMessage').html('');
        }
        document.getElementById('txtSearchName').value = '';
        document.getElementById('txtSearchCompany').value = '';
        document.getElementById('txtSearchUser').value = '';
        document.getElementById('txtSearchCountry').value = '';
        document.getElementById('ddlPartialRolesList').value = '';

        jQuery("#txtSearchName").watermark(watermarkname);
        jQuery("#txtSearchCompany").watermark(watermarkcompany);
        jQuery("#txtSearchUser").watermark(watermarkuser);
        jQuery("#txtSearchCountry").watermark(watermarkcountry);
        return false;
    });
});

function btndeactivateVisible() {
    $("#btnDeleteWorkGroup").show();
    $("#btnViewSearchWorkGroup").show();
    $("#btnCancelDeleteWorkGroup").show();
}
function btndeactivateDisaible() {
    $("#btnDeleteWorkGroup").hide();
    $("#btnViewSearchWorkGroup").hide();
    $("#btnCancelDeleteWorkGroup").hide();
}

function btnsearchpartialviewVisible() {
    if (pageName == 'Deactivate') {
        $("#btnLinkToContract").hide();
        $("#btnRedirectWorkGroup").show();
    } else {
        $("#btnLinkToContract").show();
        $("#btnRedirectWorkGroup").hide();
    }
    $("#btnCancelSearchWorkGroup").show();
}

function btnsearchpartialviewContractVisible() {
    if (pageName == 'Deactivate') {
        $("#btnLinkToContract").hide();
        $("#btnRedirectWorkGroup").show();
    } else {
        $("#btnRedirectWorkGroup").hide();
        $("#btnLinkToContract").show();
    }
    $("#btnCancelSearchWorkGroup").show();
}

function btnsearchpartialviewDisiable() {
    $("#btnCancelSearchWorkGroup").hide();
    $("#btnRedirectWorkGroup").hide();
    $("#btnLinkToContract").hide();
}

$(function () {
    $('#btnLinkToContract').click(function () {
        var workgroupIds = '';
        var $selectedRows = $('#searchList').jtable('selectedRows');
        $selectedRows.each(function () {
            var record = $(this).data('record');
            workgroupIds = workgroupIds + "|" + record.ID;
        });
        workgroupIds = workgroupIds.substring(1);
        if (workgroupIds.length > 0) {
            var url = '';
            if (pageName == 'LinkWGToArtistContract') {
                url = '/GCS/Workgroup/LinkArtistContractToWorkgroup';
            } else {
                url = '/GCS/Workgroup/LinkResourceContractToWorkgroup';
            }
            $.ajax({
                url: url,
                type: 'POST',
                data: pageName == 'LinkWGToArtistContract' ? JSON.stringify({ "contractIds": selectedContractIds, "workgroupIds": workgroupIds }) : JSON.stringify({ deviationResourceContract: resourceContractCollectionForDivation, workgroupIds: workgroupIds }),
                async: true,
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.length == 0) {
                        var objDialog = $('<div class="success"></div>')
                        .html('<center><p><b><br/>' + searchWorkgroupMessages.contractlinktoWGsucessfulMsg + '</b></p></center>')
                        .dialog({
                            autoOpen: true,
                            dialogClass: 'my-extra-class',
                            modal: true,
                            title: searchWorkgroupMessages.linkWorkgroupToContractpopuptitle,
                            show: 'clip',
                            hide: 'clip',
                            width: 300,
                            resizable: false,
                            buttons: {
                                "Ok": function () {
                                    $(this).dialog("close");
                                    $('#divLinkToWorkgroup').dialog("close");
                                }
                            }
                        });
                        $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
                    }
                    else {
                        var uniqunessContractIds = '';
                        $.each(data, function (i, contractId) {
                            uniqunessContractIds += contractId + ",";
                        });
                        var objDialog = $('<div class="warning"></div>')
                        .html('<center><p><b><br/> ' + searchWorkgroupMessages.uniquenessContractIdsMsg + ' ' + uniqunessContractIds.slice(0, -1) + ' ' + searchWorkgroupMessages.uniquenessMsg + '</b></p></center>')
                        .dialog({
                            autoOpen: true,
                            dialogClass: 'my-extra-class',
                            modal: true,
                            title: searchWorkgroupMessages.linkWorkgroupToContractpopuptitle,
                            show: 'clip',
                            hide: 'clip',
                            width: 300,
                            resizable: false,
                            buttons: {
                                "Ok": function () {
                                    $(this).dialog("close");
                                    $('#divLinkToWorkgroup').dialog("close");
                                }
                            }
                        });
                        $('.my-extra-class').find('.ui-dialog-titlebar-close').css('display', 'none');
                    }
                }
            });
            hideSearchError();
        } else {
            showSearchError(searchWorkgroupMessages.noRowsSelected);
        }
    });
});

function hideSearchError() {
    $("#mgSearchSelectedWorkgroupErrorMessage").hide();
    $("#mgSearchSelectedWorkgroupErrorMessage").html('');
}

function showSearchError(message) {
    $("#mgSearchSelectedWorkgroupErrorMessage").show();
    $("#mgSearchSelectedWorkgroupErrorMessage").html(message);
}

function moveDialogBoxAtTop() {
    var dialogue = $('.ui-dialog');
    dialogue.animate({
        top: "40px"
    }, 0);
}