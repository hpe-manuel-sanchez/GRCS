var searchlist = '';
var pagingCount = "";
var selectedRoleID = -1;
var workgroupType = "parent";
var frompage = "";
$('.ui-dialog-titlebar-close').attr("title", "Close");

function LoadSearchJtable() {
    searchlist = '';
    var name = $('#txtName').val();
    var selectedRoleIndex = $('#RolesList')[0].selectedIndex;
    var roletext = $('#RolesList')[0][selectedRoleIndex].text;
    var roleID = $('#RolesList')[0][selectedRoleIndex].value;
    var roleValue = -1;
    if (roleID.length > 0) roleValue = roleID;
    var company = $('#txtCompany').val();
    var user = $('#txtUser').val();
    var country = $('#txtCountry').val();
    if (name != '') { searchlist = searchlist + name + '/'; }
    if (selectedRoleIndex != 0) { searchlist = searchlist + roletext + '/'; }
    if (company != '') { searchlist = searchlist + company + '/'; }
    if (user != '') { searchlist = searchlist + user + '/'; }
    if (country != '') { searchlist = searchlist + country + '/'; }
    searchlist = searchlist.substring(0, searchlist.length - 1);
    document.getElementById('spnSearchResult').innerHTML = searchlist + '&nbsp;<span id="cmpSearchResultRecordcount"/>';

    if (searchlist.length > 0) {
        var showDropDown = document.getElementById('SearchWorkgroupPaging');
        showDropDown.style.display = 'block';
        pagingCount = $('#ddlSearchPaging').val();
        $("#buttonPanel").show();
        $('#searchWorkGroupList').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: true,
            defaultSorting: 'Name ASC',
            columnResizable: false,
            selecting: true,
            selectingCheckboxes: true,
            multiselect: false,
            selectOnRowClick: false,
            actions: {
                listAction: '/GCS/WorkGroup/SearchWorkgroup'
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
                    width: '15%'
                },
                StatusType: {
                    title: wgSearchJtableColNames.wgType, //'Workgroup Type',
                    width: '15%'
                },
                RoleID: {
                    list: false
                },
                RoleName: {
                    title: wgSearchJtableColNames.wgRole, //'Role',
                    width: '10%'
                },
                Company: {
                    title: wgSearchJtableColNames.company, //'Company',
                    width: '20%',
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
                },
                ModifiedDateTime: {
                    dataType: Date,
                    list: false
                }
            },
            recordsLoaded: function (event, data) {
                $('#cmpSearchResultRecordcount').html('(' + data.serverResponse.TotalRecordCount + ')');
                $('#searchWorkGroupList .jtable thead tr th:first').remove();
                $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#searchWorkGroupList .jtable thead tr:first');
            },
            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#searchWorkGroupList').jtable('selectedRows');
                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#SelectedRowList').append(record.ID + '~' + record.Name + '~' + record.StatusType + '~' + record.ParentID + '~' + record.RoleID + '~' + record.ModifiedDateTime);
                        var recdisplay = document.getElementById('SelectedRowList');
                        recdisplay.style.display = 'none';
                    });
                }
                else {
                    //No rows selected
                }
            }
        });

        hideError();
        $('#deactivateWgSuccessMessage').hide();
        $('#deactivateWgSuccessMessage').html('');
        $('#searchWorkGroupList').jtable('load', {
            workgroupName: name,
            workgroupRole: roleValue,
            workgroupCompany: company,
            workgroupUser: user,
            workgroupCountry: country,
            isStatus: 'false'
        });
    } else {
        showError(searchWorkgroupMessages.searchInValid);
    }
}

function moveDialogAtTop() {
    var dialogue = $('.ui-dialog')
    dialogue.animate({ top: "40px" }, 0);
}

function showError(message) {
    $('#mgDeactivateSelectedWorkgroupErrorMessage').show();
    $('#mgDeactivateSelectedWorkgroupErrorMessage').html(message);
}

function hideError() {
    $('#mgDeactivateSelectedWorkgroupErrorMessage').hide();
    $('#mgDeactivateSelectedWorkgroupErrorMessage').html('');
}

$(document).ready(function () {
    $('#btnGetWorkGrouplist').click(function (e) {
        LoadSearchJtable();
    });
    $('#ddlSearchPaging').change(function () {
        var rowCount = $(this).val();
        LoadSearchJtable(rowCount);
    });
    $('#btnDeactiveWorkGroup').click(function (e) {
        var workgroupSelected = $.trim($('#SelectedRowList')[0].innerHTML);
        var workgroups = workgroupSelected.split('~');
        var workgroupid = workgroups[0];
        var workgroupname = workgroups[1];
        var workgroupModifiedDateTime = workgroups[5];
        var isParent = false;
        if (workgroupSelected.length > 0) {
            if (workgroups[2] == 'Parent') { isParent = true; }
            var parentID = workgroups[3];
            selectedRoleID = workgroups[4];
            if (workgroupid != null && workgroupid != '') {
                //show parent request
                showPendingRequest(workgroupid, workgroupModifiedDateTime, parentID, workgroupname, isParent);
            }
            hideError();
            $('#deactivateWgSuccessMessage').hide();
            $('#deactivateWgSuccessMessage').html('');
        }
        else {
            $('#deactivateWgSuccessMessage').hide();
            $('#deactivateWgSuccessMessage').html('');
            showError(searchWorkgroupMessages.noRowsSelected);
        }
    });
    $('#btnCancelDeactive').click(function (e) {
        $('#SearchWorkgroupPaging').hide(); $('#searchWorkGroupList').hide();
        window.location.href = "/GCS/workgroup/";
        return false;
    });
    $('#btnCancelWorkGroup').click(function () {
        $('#DisplayPendingRequest').dialog('close');
        return false;
    });
    $('#btnSearchWorkgroupReset').click(function () {
        hideError();
        $('#deactivateWgSuccessMessage').hide();
        $('#deactivateWgSuccessMessage').html('');
        document.getElementById('txtName').value = '';
        document.getElementById('txtCompany').value = '';
        document.getElementById('txtUser').value = '';
        document.getElementById('txtCountry').value = '';
        document.getElementById('RolesList').selectedIndex = 0;

        jQuery("#txtName").watermark(watermarkname);
        jQuery("#txtCompany").watermark(watermarkcompany);
        jQuery("#txtUser").watermark(watermarkuser);
        jQuery("#txtCountry").watermark(watermarkcountry);
        return false;
    });
    //Open view Workgroup
    $('#btnViewWorkGroup').click(function (e) {
        $('#ViewWorkgroup').html('');
        workgroupSelected = $.trim($('#SelectedRowList')[0].innerHTML);
        var workgroups = workgroupSelected.split('~');
        frompage = "viewworkgroup";
        workgroupid = workgroups[0];
        selectedRoleID = workgroups[4];
        workgroupType = workgroups[2];
        if (workgroupSelected.length > 0) {
            var objDialog = $('#ViewWorkgroup').dialog({
                autoOpen: true,
                modal: true,
                title: title, /* workgroups[1] */
                show: 'clip',
                hide: 'clip',
                resizable: false,
                height: 400,
                width: "95%"
            });
            //Load partial view
            if (workgroupType.toLowerCase() == "child") {
                objDialog.load('/GCS/Workgroup/ViewChildWorkgroup?WorkgroupId=' + workgroupid + "&workgroupType=" + workgroupType);
            }
            else {
                objDialog.load('/GCS/Workgroup/ViewWorkgroup?WorkgroupId=' + workgroupid);
            }

            moveDialogAtTop();
            hideError();
            $('#deactivateWgSuccessMessage').hide();
            $('#deactivateWgSuccessMessage').html('');
        }
        else {
            showError(searchWorkgroupMessages.noRowsSelected);
        }
    });

    $("#txtName")
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
                event.preventDefault();
            }
        })
        .blur(function (event) {
            if (!$('#txtName').is(":focus"))
                $('#txtName').removeClass("ui-autocomplete-loading");
        })
        .autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                    type: "GET",
                    dataType: "json",
                    data: { suggestiveInput: request.term, workgroupElement: "WorkgroupName" },
                    term: request.term,
                    success: function (data) {
                        if (data.length == 1) {
                            $("#txtName").val(data[0]);
                        }
                        response(
                            $.map(data, function (records, autocomplete) {
                                if ($('#txtName').is(":focus"))
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
                    $('#txtName').val("");
                    $('#txtName').val(ui.item.value);
                }
                return false;
            }
        });

    //suggestivebox for Company
    $("#txtCompany")
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
                event.preventDefault();
            }
        })
        .blur(function (event) {
            if (!$('#txtCompany').is(":focus"))
                $('#txtCompany').removeClass("ui-autocomplete-loading");
        })
        .autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                    type: "GET",
                    dataType: "json",
                    data: { suggestiveInput: request.term, workgroupElement: "Company", pageName: pageName },
                    term: request.term,
                    success: function (data) {
                        if (data.length == 1) {
                            $("#txtCompany").val(data[0]);
                        }
                        response(
                            $.map(data, function (records, autocomplete) {
                                if ($('#txtCompany').is(":focus"))
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
                    $('#txtCompany').val("");
                    $('#txtCompany').val(ui.item.value);
                }
                return false;
            }
        });

    //suggestivebox for User
    $("#txtUser")
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
                event.preventDefault();
            }
        })
        .blur(function (event) {
            if (!$('#txtUser').is(":focus"))
                $('#txtUser').removeClass("ui-autocomplete-loading");
        })
        .autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                    type: "GET",
                    dataType: "json",
                    data: { suggestiveInput: request.term, workgroupElement: "User" },
                    term: request.term,
                    success: function (data) {
                        if (data.length == 1) {
                            $("#txtUser").val(data[0]);
                        }
                        response(
                            $.map(data, function (records, autocomplete) {
                                if ($('#txtUser').is(":focus"))
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
                    $('#txtUser').val("");
                    $('#txtUser').val(ui.item.value);
                }
                return false;
            }
        });

    //suggestivebox for Country
    $("#txtCountry")
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
                event.preventDefault();
            }
        })
        .blur(function (event) {
            if (!$('#txtCountry').is(":focus"))
                $('#txtCountry').removeClass("ui-autocomplete-loading");
        })
        .autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                    type: "GET",
                    dataType: "json",
                    data: { suggestiveInput: request.term, workgroupElement: "Country" },
                    term: request.term,
                    success: function (data) {
                        if (data.length == 1) {
                            $("#txtCountry").val(data[0]);
                        }
                        response(
                            $.map(data, function (records, autocomplete) {
                                if ($('#txtCountry').is(":focus"))
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
                    $('#txtCountry').val("");
                    $('#txtCountry').val(ui.item.value);
                }
                return false;
            }
        });

    jQuery("#txtName").watermark(watermarkname);
    jQuery("#txtCompany").watermark(watermarkcompany);
    jQuery("#txtUser").watermark(watermarkuser);
    jQuery("#txtCountry").watermark(watermarkcountry);

    $('#RolesList').keypress(function (event) {
        var selectedRoleIndex = $('#RolesList')[0].selectedIndex;
        if (selectedRoleIndex != 0 && selectedRoleIndex != -1) {
            if (event.keyCode == '13') {
                $('#btnGetWorkGrouplist').click();
            }
        }
    });
});