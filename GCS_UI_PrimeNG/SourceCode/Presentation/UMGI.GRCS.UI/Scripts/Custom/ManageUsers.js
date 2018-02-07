var jsonObj = "";
var pagingCount = "";
var selectedUsers = "";
var selectedUsersToDelete = '';
var selRoleTypeText = "";
var imgDefault = "";
var savedUserNames = "";
var DefaultUserNames = "";
var savedUserNamesWithOutComma = "";
var rowCountIsZero = "";
var noRowsSelectedForDelete = "";
var isOnlyOneUserAdd = "";
var oneDefaultUser = "";
var oneDefaultUserName = "";
//jGridsList["searchUserResults"] = [];
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function () {
    if ($('#hdnMaintainWorkGroup').length != 0 || fromPage == 'maintainchildworkgroup') {
        LoadAddedUsersJtable('afterSave');

        $('#addedManageUserResults').show();
    }
    LoadwatermarkForManageUsers();
});

function getDefaultUserColumnVisibilty() {
    if ($('#hdnMaintainWorkGroup').length == 0 && workGroupId.length == 0) {
        if ($("#RolesList > option:selected").attr("value") != "8" && $("#RolesList > option:selected").attr("value") != "9" && $("#RolesList > option:selected").attr("value") != "") {
            return 'visible';
        }
        else {
            return 'hidden';
        }
    }

    else {
        if (maintainWorkgroupRoleId != "8" && maintainWorkgroupRoleId != "9" && maintainWorkgroupRoleId != "10" && maintainWorkgroupRoleId != "") {
            return 'visible';
        }
        else {
            return 'hidden';
        }
    }
}

$(document).ready(function () {
    $('#btnUserSave').click(function () {
        //LoadAddedUsersJtable('save');
        //Manage User CR
        var selectedWorkgroupRole = "";
        var mngUserSavedUserCollection = [];
        var savedUserInfo = {};

        if ($('#hdnMaintainWorkGroup').length != 0 || fromPage.toLowerCase().indexOf("child") != -1) {
            selectedWorkgroupRole = window.parent.$("#RoleName").val();
        }
        else {
            selectedWorkgroupRole = $("#RolesList option:selected").text();
        }
        var savedUsersLoginNames = ""; var savedDefaultUsersLoginNames = ""; var savedInRoleUsersLoginNames = ""; var savedR2UsersLoginNames = ""; var savedUPCAllocUsersLoginNames = ""; var savedMngWkpUsersLoginNames = "";
        var mngUsrDefaultUsers = ""; var mngUsrRequestUsers = ""; var mngUsrReviewerUsers = ""; var mngUsrR2Users = ""; var mngUsrUPCUsers = ""; var mngUsrMngWkgpUsers = "";
        var SelectedManageUsers = "";
        var $selectedUserRows = $('#addedManageUserResults').find(".jtable tbody").html();
        if (($($selectedUserRows)[0].innerText || $($selectedUserRows)[0].textContent) != "No data available!") {
            $.each($('#addedManageUserResults').find(".jtable tbody").children(), function (index, savedRow) {
                //Get Saved UserName
                //for Cross browser- innerText property will not support in FF, so testContent property is used
                //for rest of the browser innerText property will be supported
                var UserLoginName = savedRow.children[1].innerText || savedRow.children[1].textContent;
                var UserName = savedRow.children[3].innerText || savedRow.children[3].textContent;
                var UserEmail = savedRow.children[4].innerText || savedRow.children[4].textContent;

                var isDefaultUser = false;
                var isUserInRole = false;
                var isUserR2Authorized = false;
                var isUserCanAllocateUpc = false;
                var isUserCanMngWorkgroup = false;

                savedUsersLoginNames = savedUsersLoginNames + UserLoginName + ",";
                //check Default User
                var savedUserCount = $('#addedManageUserResults').find(".jtable tbody").children().length;
                //Default user should not set for inquiry role and requestor role
                if (!selectedWorkgroupRole.match('Inquiry') && !selectedWorkgroupRole.match('Requestor')) {
                    //Set Default User when singleuserselection
                    if (savedUserCount != 0 && savedUserCount == 1) {
                        isDefaultUser = "true";
                        savedDefaultUsersLoginNames = savedDefaultUsersLoginNames + UserLoginName + ",";
                        mngUsrDefaultUsers = mngUsrDefaultUsers + UserName + ";&nbsp";
                    }
                    else {
                        var isDefaultImage = $(savedRow.children[2].innerHTML).attr("src");
                        if (isDefaultImage.indexOf("green") != -1) {
                            isDefaultUser = "true";
                            savedDefaultUsersLoginNames = savedDefaultUsersLoginNames + UserLoginName + ",";
                            mngUsrDefaultUsers = mngUsrDefaultUsers + UserName + ";&nbsp";
                        } else {
                            isDefaultUser = "false";
                        }
                    }
                }
                //Check Saved User Rights
                var UserRights = savedRow.children[5];
                $.each(UserRights.children, function (rightsIndex, chkSelectedUserRigts) {
                    if (chkSelectedUserRigts.checked) {
                        if (chkSelectedUserRigts.value == mgUserRequestorRights.Requestor) {
                            isUserInRole = "true";
                            savedInRoleUsersLoginNames = savedInRoleUsersLoginNames + UserLoginName + ",";
                            mngUsrRequestUsers = mngUsrRequestUsers + UserName + ";&nbsp";
                        }
                        else if (chkSelectedUserRigts.value == mgUserRequestorRights.Reviewer) {
                            isUserInRole = "true";
                            savedInRoleUsersLoginNames = savedInRoleUsersLoginNames + UserLoginName + ",";
                            mngUsrReviewerUsers = mngUsrReviewerUsers + UserName + ";&nbsp";
                        }
                        else if (chkSelectedUserRigts.value == mgUserRequestorRights.Inquiry) {
                            isUserInRole = "true";
                            savedInRoleUsersLoginNames = savedInRoleUsersLoginNames + UserLoginName + ",";
                            mngUsrReviewerUsers = mngUsrReviewerUsers + UserName + ";&nbsp";
                        }
                        else if (chkSelectedUserRigts.value == mgUserRequestorRights.Admin) {
                            isUserInRole = "true";
                            savedInRoleUsersLoginNames = savedInRoleUsersLoginNames + UserLoginName + ",";
                            mngUsrReviewerUsers = mngUsrReviewerUsers + UserName + ";&nbsp";
                        }
                        else if (chkSelectedUserRigts.value == mgUserRequestorRights.R2Authorized) {
                            isUserR2Authorized = "true";
                            savedR2UsersLoginNames = savedR2UsersLoginNames + UserLoginName + ",";
                            mngUsrR2Users = mngUsrR2Users + UserName + ";&nbsp";
                        }
                        else if (chkSelectedUserRigts.value == mgUserRequestorRights.UPCAllocation) {
                            isUserCanAllocateUpc = "true";
                            savedUPCAllocUsersLoginNames = savedUPCAllocUsersLoginNames + UserLoginName + ",";
                            mngUsrUPCUsers = mngUsrUPCUsers + UserName + ";&nbsp";
                        }
                        else if (chkSelectedUserRigts.value == mgUserRequestorRights.ManageWorkgroup) {
                            isUserCanMngWorkgroup = "true";
                            savedMngWkpUsersLoginNames = savedMngWkpUsersLoginNames + UserLoginName + ",";
                            mngUsrMngWkgpUsers = mngUsrMngWkgpUsers + UserName + ";&nbsp";
                        }
                    }
                });

                savedUserInfo = { 'Name': UserName, 'LoginName': UserLoginName, 'Email': UserEmail, 'IsUserDefault': isDefaultUser, 'IsInRole': isUserInRole, 'IsR2Authorized': isUserR2Authorized, 'CanAllocateUpc': isUserCanAllocateUpc, 'CanManageWorkgroup': isUserCanMngWorkgroup };
                mngUserSavedUserCollection.push(savedUserInfo);
            });
        }
        window.parent.$('#hdnSavedUserValues').val(savedUsersLoginNames);
        window.parent.$('#hdnDefaultsUserForSave').val(savedDefaultUsersLoginNames);

        window.parent.$('#hdnSavedInRoleUserValues').val(savedInRoleUsersLoginNames);
        window.parent.$('#hdnSavedR2UserValues').val(savedR2UsersLoginNames);
        window.parent.$('#hdnSavedUPCAllocUserValues').val(savedUPCAllocUsersLoginNames);
        window.parent.$('#hdnSavedMngWkpUserValues').val(savedMngWkpUsersLoginNames);
        if (mngUsrDefaultUsers != "" || mngUsrRequestUsers != "" || mngUsrReviewerUsers != "" || mngUsrR2Users != "" || mngUsrUPCUsers != "" || mngUsrMngWkgpUsers != "") {
            //var SelectedManageUsers = "";
            var userRequestorOrInquieryLabel = (selectedWorkgroupRole.match('Reviewer') || selectedWorkgroupRole.match('UMGI Global Clearance')) ? mgUserRequestorRights.Reviewer : mgUserRequestorRights.Inquiry;
            mngUsrDefaultUsers = mngUsrDefaultUsers != "" ? getSortedList(mngUsrDefaultUsers) : "&nbsp;";
            mngUsrRequestUsers = mngUsrRequestUsers != "" ? getSortedList(mngUsrRequestUsers) : "&nbsp;";
            mngUsrReviewerUsers = mngUsrReviewerUsers != "" ? getSortedList(mngUsrReviewerUsers) : "&nbsp;";
            mngUsrR2Users = mngUsrR2Users != "" ? getSortedList(mngUsrR2Users) : "&nbsp;";
            mngUsrUPCUsers = mngUsrUPCUsers != "" ? getSortedList(mngUsrUPCUsers) : "&nbsp;";
            mngUsrMngWkgpUsers = mngUsrMngWkgpUsers != "" ? getSortedList(mngUsrMngWkgpUsers) : "&nbsp;";
            if (selectedWorkgroupRole.match('Requestor')) {
                SelectedManageUsers = $('<ul style=""><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.Requestor + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrRequestUsers + '</div></li><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.R2Authorized + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrR2Users + '</div></li><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.UPCAllocation + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrUPCUsers + '</div></li><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.ManageWorkgroup + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrMngWkgpUsers + '</div></li></ul>');
                //$('<table id="tblUserRightsInfo"><tr><td>' + mgUserRequestorRights.DefaultUsers + '</td><td>' + getSortedList(mngUsrDefaultUsers) + '</td></tr><tr><td>' + mgUserRequestorRights.Requestor + '</td><td>' + getSortedList(mngUsrRequestUsers) + '</td></tr><tr><td>' + mgUserRequestorRights.R2Authorized + '</td><td>' + getSortedList(mngUsrR2Users) + '</td></tr><tr><td>' + mgUserRequestorRights.UPCAllocation + '</td><td>' + getSortedList(mngUsrUPCUsers) + '</td></tr><tr><td>' + mgUserRequestorRights.ManageWorkgroup + '</td><td>' + getSortedList(mngUsrMngWkgpUsers) + '</td></tr></table>');
                $("#btnManageUsers").css("margin-top", "5px");
            }
            if ((selectedWorkgroupRole.match('Reviewer') || selectedWorkgroupRole.match('UMGI Global Clearance')) && fromPage.toLowerCase().indexOf("child") == -1) {
                SelectedManageUsers = $('<ul style=""><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.DefaultUsers + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrDefaultUsers + '</div></li><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + userRequestorOrInquieryLabel + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrReviewerUsers + '</div></li><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.ManageWorkgroup + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrMngWkgpUsers + '</div></li></ul>');
                //$('<table id="tblUserRightsInfo"><tr><td>' + mgUserRequestorRights.DefaultUsers + '</td><td>' + getSortedList(mngUsrDefaultUsers) + '</td></tr><tr><td>' + userRequestorOrInquieryLabel + '</td><td>' + getSortedList(mngUsrReviewerUsers) + '</td></tr><tr><td>' + mgUserRequestorRights.ManageWorkgroup + '</td><td>' + getSortedList(mngUsrMngWkgpUsers) + '</td></tr></table>');
                $("#btnManageUsers").css("margin-top", "5px");
            }
            if ((selectedWorkgroupRole.match('Reviewer') || selectedWorkgroupRole.match('UMGI Global Clearance')) && fromPage.toLowerCase().indexOf("child") != -1) {
                SelectedManageUsers = $('<ul style=""><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.DefaultUsers + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrDefaultUsers + '</div></li><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + userRequestorOrInquieryLabel + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrReviewerUsers + '</div></li></ul>');
                $("#btnManageUsers").css("margin-top", "5px");
                //$('<table id="tblUserRightsInfo"><tr><td>' + mgUserRequestorRights.DefaultUsers + '</td><td>' + getSortedList(mngUsrDefaultUsers) + '</td></tr><tr><td>' + userRequestorOrInquieryLabel + '</td><td>' + getSortedList(mngUsrReviewerUsers) + '</td></tr></table>');
            }
            if (selectedWorkgroupRole.match('Admin')) {
                SelectedManageUsers = $('<ul style=""><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.Admin + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrReviewerUsers + '</div></li></ul>');
                //$('<table id="tblUserRightsInfo"><tr><td>' + mgUserRequestorRights.Admin + '</td><td>' + getSortedList(mngUsrReviewerUsers) + '</td></tr></table>');
                $("#btnManageUsers").css("margin-top", "5px");
            }
            if (selectedWorkgroupRole.match('Inquiry') && fromPage.toLowerCase().indexOf("child") == -1) {
                SelectedManageUsers = $('<ul style=""><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + userRequestorOrInquieryLabel + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrReviewerUsers + '</div></li><li class="mngUserSplRightsLi"><div class="mngUserSplRightsLabelDiv" >' + mgUserRequestorRights.ManageWorkgroup + '</div><div class="mngUserSplRightsNamesDataDiv">' + mngUsrMngWkgpUsers + '</div></li></ul>');
                $("#btnManageUsers").css("margin-top", "5px");
            }
        }
        if (SelectedManageUsers != "") {
            window.parent.$("#divUsers").html(SelectedManageUsers.html());
            workGroupUserNames = savedUsersLoginNames;
        }
        else {
            window.parent.$("#divUsers").html(""); workGroupUserNames = savedUsersLoginNames;
            $('#spnMgUserAddedResultLabel').html('');
            $('#spnMgUserAddedResultLabel').hide();
        }

        //Synchronize the users info
        window.parent.$('#userDetailsForSave').val(JSON.stringify(mngUserSavedUserCollection));
        //Manage User CR End
        ResetManageUsers();
        $('#manageUsers').dialog('close');
        if (savedUsersLoginNames != "") {
            $('#btnManageUsers').removeClass('input-validation-error');
        }
    });
});

function LoadSearchUsersJtable(pageSize) {
    selectedUsers = '';
    isOnlyOneUserAdd = '';
    $('#searchUserResults').jtable({
        paging: true,
        pageSize: pageSize,
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true,
        columnResizable: false,
        selectOnRowClick: false,
        sorting: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: '/GCS/WorkGroup/SearchUsers'
        },
        fields: {
            Name: {
                title: mgUserJtableColNames.UserName,
                width: '35%'
            },
            LoginName: {
                key: true,
                list: true,
                title: mgUserJtableColNames.LoginName,
                width: '35%'
            },

            CountryName: {
                title: mgUserJtableColNames.Country,
                width: '30%'
            }
        },
        //        loadingRecords: function () {
        //            isGridLoad = true;
        //        },
        recordsLoaded: function (event, data) {
            //    displaySelectedRecordsInGrid($("#searchUserResults"), jGridsList["searchUserResults"], "LoginName");
            //     isGridLoad = false;
            $('#mgnUsrSearchResultRecordcount').html('(' + data.serverResponse.TotalRecordCount + ')');
            $('#searchUserResults .jtable thead tr th:first').remove();
            $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#searchUserResults .jtable thead tr:first');
            if (data.records.length > 0) {
                document.getElementById('btnUserSave').style.visibility = 'visible';
                document.getElementById('btnUserRemove').style.visibility = 'visible';
                document.getElementById('btnUserCancel').style.visibility = 'visible';
                $("#btnAddUser").removeAttr("disabled");
            }
            else {
                document.getElementById('btnUserSave').style.visibility = 'visible';
                document.getElementById('btnUserRemove').style.visibility = 'visible';
                document.getElementById('btnUserCancel').style.visibility = 'visible';
                $("#btnAddUser").attr("disabled", "disabled");
            }
        },

        //Register to selectionChanged event to hanlde events

        selectionChanged: function () {
            // if (!isGridLoad)
            //     updateSelectedRecordsInGrid($("#searchUserResults"), jGridsList["searchUserResults"], "LoginName");
            //Get all selected rows
            var $selectedRows = $('#searchUserResults').jtable('selectedRows');
            selectedUsers = '';
            //Working previous logic
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    selectedUsers = selectedUsers + record.LoginName + ","
                });
            }
            //            if (jGridsList["searchUserResults"].length > 0) {
            //                for (var i = 0; i < jGridsList["searchUserResults"].length; i++) {
            //                    selectedUsers = selectedUsers + jGridsList["searchUserResults"][i].LoginName + ",";
            //                }
            //            }

            parent.document.getElementById('hdnAddedUsers').value = selectedUsers;
        }
    });
    $('#searchUserResults').jtable('load', {
        userName: $('#username').val(),
        loginName: $('#userid').val(),
        country: $('#usercountry').val()
    });
}

$(document).ready(function (e) {
    if ($('#addedManageUserResults')[0].innerHTML != "") {
        if ($('#addedManageUserResults').find(".jtable tbody").length != 1) {
            $('#spnMgUserAddedResultLabel').html(""); $('#spnMgUserAddedResultLabel').hide();
        } else {
            $('#spnMgUserAddedResultLabel').html(listOfAddedMgUsers);
            // $('#spnMgUserAddedResultLabel').css({ "display": "block" });
            $('#spnMgUserAddedResultLabel').show();
        }
    }
    else {
        $('#spnMgUserAddedResultLabel').html('');
        $('#spnMgUserAddedResultLabel').hide();
    }
    $('#btnOpenSearchUser').click(function () {
        muHideError();
        $('#searchUserResults').jtable('destroy');

        if (($('#username').val() == null || $('#username').val() == "") && ($('#userid').val() == null || $('#userid').val() == "") && ($('#usercountry').val() == null || $('#usercountry').val() == "")) {
            muShowError(createWorkgroupMessages.searchInValid);
            return false;
        }

        pagingCount = $('#ddlUserPaging').val();

        var searchlist = '';
        var userName = $('#username').val();
        var userId = $('#userid').val();
        var country = $('#usercountry').val();

        if (userName != '') { searchlist = searchlist + userName + '/'; }
        if (userId != '') { searchlist = searchlist + userId + '/'; }
        if (country != '') { searchlist = searchlist + country; }
        if (country == '') {
            searchlist = searchlist.substring(0, searchlist.length - 1);
        }
        $("#searchResultForUsers").html('<p style=\"margin-left:15px;\">' + searchresultForLabel + " " + '<b>' + searchlist + '&nbsp;<span id="mgnUsrSearchResultRecordcount"/></b>' + '</p>');
        $("#divSearchUserPaging").show();
        document.getElementById('btnAddUser').style.visibility = 'visible';

        $("#mainsearch").show();

        $("#addedManageUserResults").hide();
        $('#spnMgUserAddedResultLabel').html('');
        $('#spnMgUserAddedResultLabel').hide();
        $("#searchUserPopup").show();
        $("#mainbutton").hide();
        document.getElementById('username').value = $('#username').val();
        document.getElementById('userid').value = $('#userid').val();
        document.getElementById('usercountry').value = $('#usercountry').val();
        LoadSearchUsersJtable(pagingCount);
        LoadwatermarkForManageUsers();
    });
});

function LoadAddedUsersJtable(clickevent) {
    //	if (clickevent == "afterSave" && parent.document.getElementById('divDefaultUsers').innerHTML == "") {
    //		DefaultUsers = '';
    //		DefaultUserNames = '';
    //  }
    var selectedRoleTypeText = ""; var selectedRoleTypeId = "";
    if ($('#hdnMaintainWorkGroup').length != 0 || fromPage.toLowerCase().indexOf("child") != -1) {
        selectedRoleTypeText = window.parent.$("#RoleName").val();
        selectedRoleTypeId = selectedRoleTypeText;
    }
    else {
        selectedRoleTypeText = $("#RolesList option:selected").text();
        selectedRoleTypeId = $("#RolesList > option:selected").attr("value");
    }
    var userRequestorOrInquieryLabel = (selectedRoleTypeText.match('Reviewer') || selectedRoleTypeText.match('UMGI Global Clearance')) ? mgUserRequestorRights.Reviewer : mgUserRequestorRights.Inquiry; //"Inquiry";
    muHideError();
    $('#addedManageUserResults').jtable({
        /*title: 'List of Users',*/
        paging: false,
        pageSize: pagingCount,
        sorting: false,
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true,
        columnResizable: false,
        selectOnRowClick: false,

        actions: {
            listAction: '/GCS/WorkGroup/AddUsers'
        },
        fields: {
            LoginName: {
                key: true,
                visibility: 'hidden', //list: false,
                title: mgUserJtableColNames.LoginName
            },
            IsUserDefault: {
                title: mgUserJtableColNames.Default,
                width: '5%',
                edit: false,
                listClass: "txtAlign",
                visibility: getDefaultUserColumnVisibilty(),
                display: function (data) {
                    var $img;
                    var savedManageUsers = parent.document.getElementById('hdnDefaultsUserForSave').value;
                    if (data.record.IsUserDefault == true && savedManageUsers == '') {
                        $img = $('<img src="/Gcs/Images/i_flag_green.png" title="Select Default Users" />');
                        if (DefaultUserNames.length == 0) {
                            DefaultUserNames = data.record.Name + '|';
                        }
                        if ($('#hdnMaintainWorkGroup').length != 0 && DefaultUserNames.length > 0) {
                            if ($.trim(DefaultUserNames.toLowerCase()).indexOf($.trim(data.record.Name.toLowerCase())) == -1) {
                                DefaultUserNames = DefaultUserNames + data.record.Name + '|';
                            }
                        }
                        if ($('#hiddenWorkgroupId').length != 0 && DefaultUserNames.length > 0) {
                            if ($.trim(DefaultUserNames.toLowerCase()).indexOf($.trim(data.record.Name.toLowerCase())) == -1) {
                                DefaultUserNames = DefaultUserNames + data.record.Name + '|';
                            }
                        }
                    }
                    else if (savedManageUsers != '' && savedManageUsers.toLowerCase().indexOf(data.record.LoginName.toLowerCase()) != -1 && data.record.IsUserDefault == true) {
                        $img = $('<img src="/Gcs/Images/i_flag_green.png" title="Select Default Users" />');
                        if (DefaultUserNames.length == 0) {
                            DefaultUserNames = data.record.Name + '|';
                        }
                        if ($('#hdnMaintainWorkGroup').length != 0 && DefaultUserNames.length > 0) {
                            if ($.trim(DefaultUserNames.toLowerCase()).indexOf($.trim(data.record.Name.toLowerCase())) == -1) {
                                DefaultUserNames = DefaultUserNames + data.record.Name + '|';
                            }
                        }
                        if ($('#hiddenWorkgroupId').length != -1 && DefaultUserNames.length > 0) {
                            if ($.trim(DefaultUserNames.toLowerCase()).indexOf($.trim(data.record.Name.toLowerCase())) == -1) {
                                DefaultUserNames = DefaultUserNames + data.record.Name + '|';
                            }
                        }
                    } else if (savedManageUsers != '' && savedManageUsers.toLowerCase().indexOf(data.record.LoginName.toLowerCase()) != -1) {
                        $img = $('<img src="/Gcs/Images/i_flag_green.png" title="Select Default Users" />');
                    }
                    else
                        $img = $('<img src="/Gcs/Images/i_flag_grey.png" title="Select Default Users" />');
                    return $img.click(function (e) {
                        if ($("#RolesList > option:selected").attr("value") != "8" && $("#RolesList > option:selected").attr("value") != "9" && $("#RolesList > option:selected").attr("value") != "") {
                            var imgurl = $(this).attr('src');
                            //imgurl = imgurl.substr(imgurl.length - 8);
                            if (imgurl.indexOf("grey") != -1) {
                                if ($.trim(DefaultUserNames.toLowerCase()).indexOf('|') == -1) {
                                    DefaultUserNames = DefaultUserNames + '|';
                                }
                                if ($.trim(DefaultUsers.toLowerCase()).indexOf(data.record.LoginName.toLowerCase()) == -1) {
                                    DefaultUsers = DefaultUsers + data.record.LoginName + ',';
                                    DefaultUserNames = DefaultUserNames + data.record.Name + '|';
                                }
                                var imgsrc = $(this).attr('src').replace("i_flag_grey.png", "i_flag_green.png");
                                $(this).attr('src', imgsrc);
                            }
                            else {
                                var imgsrc = $(this).attr('src').replace("i_flag_green.png", "i_flag_grey.png"); $(this).attr('src', imgsrc);
                                var removeDefault = data.record.LoginName + ",";
                                var removeDefaultName = data.record.Name + "|";
                                DefaultUsers = DefaultUsers.replace(removeDefault, "");
                                DefaultUserNames = DefaultUserNames.replace(removeDefaultName, "");
                            }
                            return $img;
                        }
                    });
                }
            },

            Name: {
                title: mgUserJtableColNames.Name,
                width: '20%'
            },
            Email: {
                title: mgUserJtableColNames.Email,
                width: '20%'
            },
            //Manage User CR Startss

            //            UserWorkgroupNames: {
            //                title: mgUserJtableColNames.ActiveWgNames,
            //                width: '30%'
            //            },
            //            UserInactiveWorkgroupNames: {
            //                title: mgUserJtableColNames.InactiveWgNames,
            //                width: '25%'
            //            },
            Rights: {
                title: mgUserJtableColNames.Rights,
                width: '45%',
                visibility: (selectedRoleTypeText.toLowerCase().indexOf('admin') == -1 && selectedRoleTypeId != "") ? 'visible' : 'hidden',
                display: function (data) {
                    var $userRightsChkBox = "";

                    var AddedMngUsers = window.parent.$('#hdnSavedUserValues').val();
                    var InRoleUsers = window.parent.$('#hdnSavedInRoleUserValues').val();
                    var R2Users = window.parent.$('#hdnSavedR2UserValues').val();
                    var uPCAllocationUsers = window.parent.$('#hdnSavedUPCAllocUserValues').val();
                    var manageWorkgroupUsers = window.parent.$('#hdnSavedMngWkpUserValues').val();
                    var UserRightsHTML = "";
                    if (AddedMngUsers != '' && AddedMngUsers.toLowerCase().indexOf(data.record.LoginName.toLowerCase()) != -1) {
                        if (selectedRoleTypeText.match('Requestor')) {
                            UserRightsHTML += (data.record.IsInRole == true) ? '<input id="chkIsUserRole" class="mgnUserRights" value="' + mgUserRequestorRights.Requestor + '" type="checkbox" checked="true" /><label for="chkIsUserRole">' + mgUserRequestorRights.Requestor + '</label>' : '<input id="chkIsUserRole" class="mgnUserRights" value="' + mgUserRequestorRights.Requestor + '" type="checkbox" /><label for="chkIsUserRole">' + mgUserRequestorRights.Requestor + '</label>';
                            UserRightsHTML += (data.record.CanAllocateUpc == true) ? '<input id="chkIsUPCAllocation" type="checkbox" checked="true" class="mgnUserRights" value="' + mgUserRequestorRights.UPCAllocation + '" /><label for="chkIsUPCAllocation">' + mgUserRequestorRights.UPCAllocation + '</label> <br>' : '<input id="chkIsUPCAllocation" type="checkbox" class="mgnUserRights" value="' + mgUserRequestorRights.UPCAllocation + '" /><label for="chkIsUPCAllocation">' + mgUserRequestorRights.UPCAllocation + '</label> <br>';
                            UserRightsHTML += (data.record.IsR2Authorized == true) ? '<input id="chkIsR2Authorized" class="mgnUserRights" checked="true" type="checkbox" value="' + mgUserRequestorRights.R2Authorized + '" /><label for="chkIsR2Authorized">' + mgUserRequestorRights.R2Authorized + '</label>' : '<input id="chkIsR2Authorized" class="mgnUserRights" type="checkbox" value="' + mgUserRequestorRights.R2Authorized + '" /><label for="chkIsR2Authorized">' + mgUserRequestorRights.R2Authorized + '</label>';
                        }
                        if ((selectedRoleTypeText.match('Reviewer') || selectedRoleTypeText.match('UMGI Global Clearance') || selectedRoleTypeText.match('Inquiry')) && fromPage.toLowerCase().indexOf("child") == -1) {
                            UserRightsHTML += (data.record.IsInRole == true) ? '<input id="chkIsUserRole" class="mgnUserRights" value="' + userRequestorOrInquieryLabel + '" type="checkbox" checked="true" /><label for="chkIsUserRole">' + userRequestorOrInquieryLabel + '</label>' : '<input id="chkIsUserRole" class="mgnUserRights" value="' + userRequestorOrInquieryLabel + '" type="checkbox" /><label for="chkIsUserRole">' + userRequestorOrInquieryLabel + '</label>';
                        }
                        if ((selectedRoleTypeText.match('Reviewer') || selectedRoleTypeText.match('UMGI Global Clearance')) && fromPage.toLowerCase().indexOf("child") != -1) {
                            // Child workgroup with Reviewer Role
                            UserRightsHTML += '<input id="chkIsUserRole" class="mgnUserRights" value="' + mgUserRequestorRights.Reviewer + '" type="checkbox" checked="true" disabled="true" /><label for="chkIsUserRole">' + mgUserRequestorRights.Reviewer + '</label>';
                        }
                        if ((selectedRoleTypeText.match('Requestor') || selectedRoleTypeText.match('Reviewer') || selectedRoleTypeText.match('UMGI Global Clearance') || selectedRoleTypeText.match('Inquiry')) && fromPage.toLowerCase().indexOf("child") == -1) {
                            UserRightsHTML += (data.record.CanManageWorkgroup == true) ? '<input class="mgnUserRights" id="chkIsMngWorkgroup" value="' + mgUserRequestorRights.ManageWorkgroup + '" type="checkbox" checked="true" /><label for="chkIsMngWorkgroup">' + mgUserRequestorRights.ManageWorkgroup + '</label>' : '<input class="mgnUserRights" id="chkIsMngWorkgroup" value="' + mgUserRequestorRights.ManageWorkgroup + '" type="checkbox" /><label for="chkIsMngWorkgroup">' + mgUserRequestorRights.ManageWorkgroup + '</label>';
                        }
                        if (selectedRoleTypeText.match('Admin')) {
                            // Child workgroup with RCC Admin Role
                            UserRightsHTML += '<input id="chkIsUserRole" class="mgnUserRights" value="' + mgUserRequestorRights.Admin + '" type="checkbox" checked="true" disabled="true"/><label for="chkIsUserRole">' + mgUserRequestorRights.Admin + '</label>';
                        }
                        $userRightsChkBox = $(UserRightsHTML);
                    }
                    else {
                        if (selectedRoleTypeText.match('Requestor')) {
                            $userRightsChkBox = $('<input id="chkIsUserRole" class="mgnUserRights" value="' + mgUserRequestorRights.Requestor + '" type="checkbox" checked="true" /><label for="chkIsUserRole">' + mgUserRequestorRights.Requestor + '</label><input id="chkIsUPCAllocation" type="checkbox" class="mgnUserRights" value="' + mgUserRequestorRights.UPCAllocation + '" /><label for="chkIsUPCAllocation">' + mgUserRequestorRights.UPCAllocation + '</label> <br><input id="chkIsR2Authorized" class="mgnUserRights" type="checkbox" value="' + mgUserRequestorRights.R2Authorized + '" /><label for="chkIsR2Authorized">' + mgUserRequestorRights.R2Authorized + '</label><input id="chkIsMngWorkgroup" class="mgnUserRights" value="' + mgUserRequestorRights.ManageWorkgroup + '" type="checkbox" /><label for="chkIsMngWorkgroup">' + mgUserRequestorRights.ManageWorkgroup + '</label>');
                        }
                        if ((selectedRoleTypeText.match('Reviewer') || selectedRoleTypeText.match('UMGI Global Clearance') || selectedRoleTypeText.match('Inquiry')) && fromPage.toLowerCase().indexOf("child") == -1) {
                            $userRightsChkBox = $('<input id="chkIsUserRole" class="mgnUserRights" value="' + userRequestorOrInquieryLabel + '"  checked="true" type="checkbox" /><label for="chkIsUserRole">' + userRequestorOrInquieryLabel + '</label><input class="mgnUserRights" id="chkIsMngWorkgroup" value="' + mgUserRequestorRights.ManageWorkgroup + '" type="checkbox" /><label for="chkIsMngWorkgroup">' + mgUserRequestorRights.ManageWorkgroup + '</label>');
                        }
                        if ((selectedRoleTypeText.match('Reviewer') || selectedRoleTypeText.match('UMGI Global Clearance')) && fromPage.toLowerCase().indexOf("child") != -1) {
                            // Child workgroup with Reviewer Role
                            $userRightsChkBox = $('<input id="chkIsUserRole" class="mgnUserRights" value="' + mgUserRequestorRights.Reviewer + '" type="checkbox" checked="true" disabled="true"/><label for="chkIsUserRole" >' + mgUserRequestorRights.Reviewer + '</label>');
                        }
                        if (selectedRoleTypeText.match('Admin')) {
                            // Child workgroup with RCC Admin Role
                            $userRightsChkBox = $('<input id="chkIsUserRole" class="mgnUserRights" value="' + mgUserRequestorRights.Admin + '" type="checkbox" checked="true" disabled="true"/><label for="chkIsUserRole" >' + mgUserRequestorRights.Admin + '</label>');
                        }
                        if (selectedRoleTypeText.match('--Select Role--') || selectedRoleTypeId == "") {
                            $userRightsChkBox = $(UserRightsHTML);
                        }
                    }
                    return $userRightsChkBox.click(function (e) {
                        if ($(this).is(":checked")) {
                            $(this)[0].checked = true;
                        }
                        else {
                            $(this)[0].checked = false;
                        }
                    });
                }
            }
            //Manage User CRR Ends
        },
        recordsLoaded: function (event, data) {
            $('#addedManageUserResults .jtable thead tr th:first').remove();
            $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#addedManageUserResults .jtable thead tr:first');
            if (data.records.length > 0) {
                //Setting automatic default user when user count is one
                if (data.records.length == 1) {
                    if ($("#RolesList > option:selected").attr("value") != "8" && $("#RolesList > option:selected").attr("value") != "9" && $("#RolesList > option:selected").attr("value") != "") {
                        if (clickevent != 'Add' && clickevent != 'delete') {
                            DefaultUsers = data.records[0].LoginName + ',';
                            DefaultUserNames = data.records[0].Name + '|';
                        }
                        else {
                            isOnlyOneUserAdd = "true";
                            oneDefaultUser = data.records[0].LoginName + ',';
                            oneDefaultUserName = data.records[0].Name + '|';
                        }
                    }
                    document.getElementById('addedManageUserResults').style.visibility = 'visible';
                    document.getElementById('btnUserRemove').style.visibility = 'visible';
                    document.getElementById('btnUserSave').style.visibility = 'visible';
                    document.getElementById('btnUserCancel').style.visibility = 'visible';
                }
                else {
                    document.getElementById('addedManageUserResults').style.visibility = 'visible';
                    document.getElementById('btnUserSave').style.visibility = 'visible';
                    document.getElementById('btnUserRemove').style.visibility = 'visible';
                    document.getElementById('btnUserCancel').style.visibility = 'visible';
                }
            } else {
                if (clickevent != 'delete') {
                    document.getElementById('addedManageUserResults').style.visibility = 'hidden';
                    document.getElementById('btnUserRemove').style.visibility = 'hidden';
                    document.getElementById('btnUserSave').style.visibility = 'hidden';
                    document.getElementById('btnUserCancel').style.visibility = 'hidden';
                    rowCountIsZero = "true";
                    $('#spnMgUserAddedResultLabel').hide();
                }
                if (clickevent == 'delete') {
                    document.getElementById('btnUserSave').style.visibility = 'visible';
                    document.getElementById('btnUserRemove').style.visibility = 'hidden';
                    document.getElementById('btnUserCancel').style.visibility = 'visible';
                }
            }
            savedUsers = '';
            savedUserNames = '';
            savedUserNamesWithOutComma = '';

            if (data.records.length > 0) {
                //Show selected rows
                for (i = 0; i < data.records.length; i++) {
                    savedUsers = savedUsers + data.records[i].LoginName + ",";
                    savedUserNames = savedUserNames + data.records[i].Name + ",";
                    savedUserNamesWithOutComma = savedUserNamesWithOutComma + data.records[i].Name + "|";
                }
            }
            else {
                if (clickevent == 'save') {
                    if (rowCountIsZero == "true") {
                        DefaultUserNames = "";
                        DefaultUsers = "";
                        parent.document.getElementById('divUsers').innerHTML = "";
                        parent.document.getElementById('divDefaultUsers').innerHTML = "";
                    }
                }
            }

            if (parent.document.getElementById('hdnSavedUserNameValues') != null)
                parent.document.getElementById('hdnSavedUserNameValues').value = savedUserNames;

            if (parent.document.getElementById('hdnSavedUserNameValuesWithOutComma') != null)
                parent.document.getElementById('hdnSavedUserNameValuesWithOutComma').value = savedUserNamesWithOutComma;
        },

        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('#addedManageUserResults').jtable('selectedRows');
            selectedUsersToDelete = '';
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    selectedUsersToDelete = selectedUsersToDelete + record.LoginName + ",";
                });
            }
        }
    });
    if (clickevent == 'Add') {
        $('#addedManageUserResults').jtable('load', {
            loginNames: selectedUsers + savedUsers,
            deletedUserIds: "",
            clickType: 'Add',
            defaultUserIDs: DefaultUsers,
            inRoleUserIds: $('#hdnSavedInRoleUserValues').val(),
            mngWkgUserIds: $('#hdnSavedMngWkpUserValues').val(),
            r2AuthorisedUserIds: $('#hdnSavedR2UserValues').val(),
            allocateUpcUserIds: $('#hdnSavedUPCAllocUserValues').val(),
            isSort: 'false'
        });
        $("#addedManageUserResults").show();
        $("#searchUserPopup").hide();
        $("#mainbutton").show();
    }
    else if (clickevent == 'save') {
        parent.document.getElementById('divDefaultUsers').innerHTML = '';
        parent.document.getElementById('hdnSavedUserValues').value = savedUsers;
        if (isOnlyOneUserAdd == 'true') {
            parent.document.getElementById('hdnDefaultsUserForSave').value = oneDefaultUser;
            DefaultUsers = oneDefaultUser;
            DefaultUserNames = oneDefaultUserName;
            isOnlyOneUserAdd = "";
        }
        else {
            parent.document.getElementById('hdnDefaultsUserForSave').value = DefaultUsers;
        }

        workGroupUserNames = savedUsers;
        if (DefaultUsers.length > 0) {
            var arrDefaultUsers = DefaultUsers.split(',');
            var arrDefaultUserNames = DefaultUserNames.split('|');

            for (var i = 0; i < arrDefaultUsers.length; i++) {
                if ($.trim(savedUsers.toLowerCase()).indexOf($.trim(arrDefaultUsers[i].toLowerCase() + ',')) == -1) {
                    DefaultUsers = DefaultUsers.toLowerCase().replace(arrDefaultUsers[i].toLowerCase() + ",", '');
                }
            }

            for (var i = 0; i < arrDefaultUserNames.length; i++) {
                if ($.trim(savedUserNamesWithOutComma.toLowerCase()).indexOf($.trim(arrDefaultUserNames[i].toLowerCase() + '|')) == -1) {
                    DefaultUserNames = DefaultUserNames.toLowerCase().replace(arrDefaultUserNames[i].toLowerCase() + "|", '');
                }
            }
            DefaultUsers = $.trim(DefaultUsers);

            $('#addedManageUserResults').jtable('load', {
                loginNames: savedUsers,
                deletedUserIds: "",
                clickType: 'saved',
                defaultUserIDs: DefaultUsers,
                inRoleUserIds: $('#hdnSavedInRoleUserValues').val(),
                mngWkgUserIds: $('#hdnSavedMngWkpUserValues').val(),
                r2AuthorisedUserIds: $('#hdnSavedR2UserValues').val(),
                allocateUpcUserIds: $('#hdnSavedUPCAllocUserValues').val(),
                isSort: 'true'
            });

            //parent.document.getElementById('divUsers').innerHTML = getSortedList(parent.document.getElementById('hdnSavedUserNameValuesWithOutComma').value);
            // parent.document.getElementById('divDefaultUsers').innerHTML = getSortedList(DefaultUserNames);
            $('#tduserList').removeClass('input-validation-error');
            $('#divDefaultUsers').removeClass('input-validation-error');
        }
            //Bug# 365
        else {
            $('#addedManageUserResults').jtable('load', {
                loginNames: savedUsers,
                deletedUserIds: "",
                clickType: 'saved',
                defaultUserIDs: DefaultUsers,
                inRoleUserIds: $('#hdnSavedInRoleUserValues').val(),
                mngWkgUserIds: $('#hdnSavedMngWkpUserValues').val(),
                r2AuthorisedUserIds: $('#hdnSavedR2UserValues').val(),
                allocateUpcUserIds: $('#hdnSavedUPCAllocUserValues').val()
            });

            // parent.document.getElementById('divUsers').innerHTML = getSortedList(parent.document.getElementById('hdnSavedUserNameValuesWithOutComma').value);
            //            if (parent.document.getElementById('divDefaultUsers') != null)
            //                parent.document.getElementById('divDefaultUsers').innerHTML =  getSortedList(DefaultUserNames);

            $('#tduserList').removeClass('input-validation-error');
            $('#divDefaultUsers').removeClass('input-validation-error');
        }
    }
    else if (clickevent == 'delete') {
        if (selectedUsersToDelete.length > 0) {
            selectedUsersToDelete = selectedUsersToDelete.substring(0, selectedUsersToDelete.length - 1);
            if (DefaultUsers.length > 0) {
                var uniqueDefaultUsers = DefaultUsers.split(',');
                $.each(selectedUsersToDelete.split(","), function (index, deletedUser) {
                    if (-1 != $.inArray(deletedUser, uniqueDefaultUsers)) {
                        DefaultUsers = DefaultUsers.replace(deletedUser + ',', "");
                    }
                });
            }
            DefaultUserNames = "";
            $('#addedManageUserResults').jtable('load', {
                loginNames: savedUsers,
                deletedUserIds: selectedUsersToDelete,
                clickType: 'delete',
                defaultUserIDs: DefaultUsers,
                inRoleUserIds: $('#hdnSavedInRoleUserValues').val(),
                mngWkgUserIds: $('#hdnSavedMngWkpUserValues').val(),
                r2AuthorisedUserIds: $('#hdnSavedR2UserValues').val(),
                allocateUpcUserIds: $('#hdnSavedUPCAllocUserValues').val()
            });
        }
        else {
            noRowsSelectedForDelete = "true";
            muShowError(noRowsSelected);
        }
    }
    else if (clickevent == "afterSave") {
        if ($('#hdnMaintainWorkGroup').length != 0 || fromPage == 'maintainchildworkgroup') {
            if (window.parent.$('#hdnSavedUserValues').val().length <= workGroupUserNames.length) {
                window.parent.$('#hdnSavedUserValues').val(workGroupUserNames);
            }
            $('#addedManageUserResults').jtable('load', {
                loginNames: $('#hdnSavedUserValues').val(),
                deletedUserIds: "",
                clickType: 'Add',
                defaultUserIDs: DefaultUsers,
                inRoleUserIds: $('#hdnSavedInRoleUserValues').val(),
                mngWkgUserIds: $('#hdnSavedMngWkpUserValues').val(),
                r2AuthorisedUserIds: $('#hdnSavedR2UserValues').val(),
                allocateUpcUserIds: $('#hdnSavedUPCAllocUserValues').val(),
                isSort: 'true'
            });
        }
        else {
            $('#addedManageUserResults').jtable('load', {
                loginNames: $('#hdnSavedUserValues').val(),
                deletedUserIds: "",
                clickType: '',
                defaultUserIDs: $('#hdnDefaultsUserForSave').val(),
                inRoleUserIds: $('#hdnSavedInRoleUserValues').val(),
                mngWkgUserIds: $('#hdnSavedMngWkpUserValues').val(),
                r2AuthorisedUserIds: $('#hdnSavedR2UserValues').val(),
                allocateUpcUserIds: $('#hdnSavedUPCAllocUserValues').val(),
                isSort: 'true'
            });
        }
    }

    else {
        $('#addedManageUserResults').jtable('load', {
            userIds: $('#hdnUserAddedValues').val(),
            deletedUserIds: selectedUsers,
            clickType: '',
            defaultUserIDs: ''
        });
    }
}

$(document).ready(function () {
    $('#btnAddUser').click(function () {
        if (selectedUsers.length > 0) {
            $("#mainsearch").show();
            $("#searchUserPopup").hide();
            document.getElementById('addedManageUserResults').style.display = 'block';
            $("#addedManageUserResults").show();
            LoadAddedUsersJtable('Add');
            if ($('#addedManageUserResults')[0].innerHTML != "") {
                $('#spnMgUserAddedResultLabel').html(listOfAddedMgUsers);
                $('#spnMgUserAddedResultLabel').css({ "display": "block" });
                $('#spnMgUserAddedResultLabel').show();
            }
            jsonObj = "";
        }
        else {
            muShowError(noRowsSelected);
        }
    });
});

$(document).ready(function () {
    $('#btnUserCancel1').click(function () {
        $('#manageUsers').dialog('close');
    });
});

$(document).ready(function () {
    $('#btnUserCancel').click(function () {
        $('#manageUsers').dialog('close');
        $("#addedManageUserResults").hide();
        $('#spnMgUserAddedResultLabel').html('');
        $('#spnMgUserAddedResultLabel').hide();
        $("#mainbutton").hide();
    });
});

$(document).ready(function () {
    $('#btnUserRemove').click(function () {
        LoadAddedUsersJtable('delete');
        if (noRowsSelectedForDelete != "true") {
            savedUsers = "";
            savedUserNames = "";
            savedUserNamesWithOutComma = "";
        }
        noRowsSelectedForDelete = "false";
    });
});

$(document).ready(function () {
    $('#btnSearchUserReset').click(function () {
        ResetManageUsers();
        $('#searchUserResults').jtable('destroy');
        document.getElementById('btnAddUser').style.visibility = 'hidden';
        $("#divSearchUserPaging").hide();
        $("#searchResultForUsers").html('');
    });
});

$(document).ready(function () {
    $('#btnManageUserReset').click(function () {
        muHideError();
        if ($('#btnUserRemove').is(':visible')) {
            if ($('#addedManageUserResults')[0].innerHTML != "") {
                $('#spnMgUserAddedResultLabel').html(listOfAddedMgUsers);
                $('#spnMgUserAddedResultLabel').css({ "display": "block" });
                $('#spnMgUserAddedResultLabel').show();
            }
            else {
                $('#spnMgUserAddedResultLabel').html('');
                $('#spnMgUserAddedResultLabel').hide();
            }
            if ($('#addedManageUserResults')[0].style.visibility == 'hidden') {
                $('#spnMgUserAddedResultLabel').html('');
                $('#spnMgUserAddedResultLabel').hide();
            }
            ResetManageUsers();
        }
        else {
            ResetManageUsers();
            //Bug Fix for 4714
            //			$('#searchUserResults').jtable('destroy');
            //			$("#searchUserPopup").hide();
            //			$("#mainsearch").show();
            //			$("#searchResultForUsers").html('');
            //Bug Fix for 4714
            isManageUserSearchResetClick = "true";
        }
    });
});

$(function () {
    $('#ddlUserPaging').change(function () {
        if (($('#username').val() == null || $('#username').val() == "") && ($('#userid').val() == null || $('#userid').val() == "") && ($('#usercountry').val() == null || $('#usercountry').val() == "")) {
            muShowError(createWorkgroupMessages.searchInValid);
            return false;
        }
        var rowCount = $(this).val();
        LoadSearchUsersJtable(rowCount);
    });
});
$(document).ready(function () {
    $('#btnSearchUser').click(function () {
        LoadwatermarkForManageUsers();
        if (($('#username').val() == null || $('#username').val() == "") && ($('#userid').val() == null || $('#userid').val() == "") && ($('#usercountry').val() == null || $('#usercountry').val() == "")) {
            muShowError(createWorkgroupMessages.searchInValid);
            return false;
        }
        var searchlist1 = '';

        var userName1 = $('#username').val();
        var userId1 = $('#userid').val();
        var userCountry1 = $('#usercountry').val();

        if (userName1 != '') { searchlist1 = searchlist1 + userName1 + '/'; }
        if (userId1 != '') { searchlist1 = searchlist1 + userId1 + '/'; }
        if (userCountry1 != '') { searchlist1 = searchlist1 + userCountry1; }
        if (userCountry1 == '') {
            searchlist1 = searchlist1.substring(0, searchlist1.length - 1)
        }

        $("#searchResultForUsers").html('<p>' + searchresultForLabel + " " + '<b>' + searchlist1 + '</b>' + '</p>');
        $("#divSearchUserPaging").show();
        document.getElementById('btnAddUser').style.visibility = 'visible';
        LoadSearchUsersJtable(pagingCount);
    });
});

function LoadwatermarkForManageUsers() {
    jQuery("#username").watermark(watermarkUserName);
    jQuery("#userid").watermark(watermarkUserId);
    jQuery("#usercountry").watermark(watermarkCountry);
}

function ResetManageUsers() {
    if (document.getElementById('username') != null) {
        document.getElementById('username').value = '';
    }
    if (document.getElementById('userid') != null) {
        document.getElementById('userid').value = '';
    }
    if (document.getElementById('usercountry') != null) {
        document.getElementById('usercountry').value = '';
    }
    LoadwatermarkForManageUsers();
}

function getSortedList(input) {
    if (input.lastIndexOf('|') != -1) {
        input = input.substring(0, input.length - 1);
    }
    if (input.indexOf('|') == 0) {
        input = input.substring(1, input.length);
    }
    var sorted = [];
    var output = "";
    if (input.length > 0) {
        sorted = input.split("|");
        sorted.sort();
        for (var i = 0; i < sorted.length; i++) {
            output += sorted[i] + ";&nbsp;";
        }
        output = output.substring(0, output.length - 1);
    }
    return output;
}

$(document).ready(function () {
    //User popup keypress event when search
    $('#username').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnOpenSearchUser').click();
            return false;
        }
    });
    $('#userid').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnOpenSearchUser').click();
            return false;
        }
    });
    $('#usercountry').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnOpenSearchUser').click();
            return false;
        }
    });
});

function muShowError(message) {
    $("#popupMuCompanyerrorMessage").html(message);
    $("#popupMuCompanyerrorMessage").show();
}

function muHideError() {
    $("#popupMuCompanyerrorMessage").html('');
    $("#popupMuCompanyerrorMessage").hide();
}