var searchlist = '';
var hashWorkgroup = {};
var workgroupCollection = [];
var hashUser = {};
var userCollection = [];
var selectedright = "";
var isDefault = "";
var ID = "";
var workgroupIds = "";
var isClickSearch = false;
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function () {
    $('.SearchWgForRemoveUser_pageSizeCustomizer input:text').bind('keyup', function (e) {
        var txtVal = $('.SearchWgForRemoveUser_pageSizeCustomizer input:text').val();
        if (txtVal > 100) {
            $("#msgUserWgErrorMessage").show();
            $("#msgUserWgErrorMessage").html("Items per page cannot exceed 100 records");
            if (e.keyCode == 13) {
                e.stopImmediatePropagation();
            }
        }
        else {
            $("#msgUserWgErrorMessage").hide();
        }
    });

    $('.SearchWgForUser_pageSizeCustomizer input:text').bind('keyup', function (e) {
        var txtVal = $('.SearchWgForUser_pageSizeCustomizer input:text').val();
        if (txtVal > 100) {
            $("#msgUserWgErrorMessage").show();
            $("#msgUserWgErrorMessage").html("Items per page cannot exceed 100 records");
            if (e.keyCode == 13) {
                e.stopImmediatePropagation();
            }
        }
        else {
            $("#msgUserWgErrorMessage").hide();
        }
    });

    $("#UserSuccessMessage").hide();
    $("#UserSuccessMessage").html("");
    //Suggestive box Functionality Start//
    var pageName = "SearchWorkGroupToAddRemoveUsers";
    //Suggestive box for WorkGroupName
    $("#txtUserWgName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "WorkgroupName" },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtUserWgName").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtUserWgName').is(":focus"))
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
                $('#txtUserWgName').val("");
                $('#txtUserWgName').val(ui.item.value);
            }
            return false;
        }
    });

    //suggestivebox for Company
    $("#txtUserWgCompany").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    });
    $("#txtUserWgCompany").blur(function (event) {
        if (!$('#txtUserWgCompany').is(":focus"))
            $('#txtUserWgCompany').removeClass("ui-autocomplete-loading");
    });

    $("#txtUserWgCompany").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "Company", pageName: pageName },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtUserWgCompany").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtUserWgCompany').is(":focus"))
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
                $('#txtUserWgCompany').val("");
                $('#txtUserWgCompany').val(ui.item.value);
            }
            return false;
        }
    });
    $("#txtSearchUser").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    });
    $("#txtWgUser").blur(function (event) {
        if (!$('#txtWgUser').is(":focus"))
            $('#txtWgUser').removeClass("ui-autocomplete-loading");
    });
    //suggestivebox for User
    $("#txtWgUser").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "User" },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtWgUser").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtWgUser').is(":focus"))
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
                $('#txtWgUser').val("");
                $('#txtWgUser').val(ui.item.value);
            }
            return false;
        }
    });

    $("#txtUserWgCountry").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    });
    $("#txtUserWgCountry").blur(function (event) {
        if (!$('#txtUserWgCountry').is(":focus"))
            $('#txtUserWgCountry').removeClass("ui-autocomplete-loading");
    });
    //suggestivebox for Country
    $("#txtUserWgCountry").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GCS/Workgroup/SuggestiveSearchForWorkgroup',
                type: "GET",
                dataType: "json",
                data: { suggestiveInput: request.term, workgroupElement: "Country" },
                term: request.term,
                success: function (data) {
                    if (data.length == 1) {
                        $("#txtUserWgCountry").val(data[0]);
                    }
                    response(
                        $.map(data, function (records, autocomplete) {
                            if ($('#txtUserWgCountry').is(":focus"))
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
                $('#txtUserWgCountry').val("");
                $('#txtUserWgCountry').val(ui.item.value);
            }
            return false;
        }
    });
});
//Suggestive box Functionality End
$(document).ready(function () {
    //watermark setting
    jQuery("#txtUserWgName").watermark(watermarkname);
    jQuery("#txtUserWgCompany").watermark(watermarkcompany);
    jQuery("#txtWgUser").watermark(watermarkuser);
    jQuery("#txtUserWgCountry").watermark(watermarkcountry);

    //Search Button Click Event
    $('#btnUserGetWorkGrouplist').click(function (e) {
        var selectedRoleIndex = $('#RolesList')[0].selectedIndex;
        if (($('#txtUserWgName').val() == null || $('#txtUserWgName').val() == "") && ($('#txtUserWgCompany').val() == null || $('#txtUserWgCompany').val() == "") && ($('#txtWgUser').val() == null || $('#txtWgUser').val() == "") && ($('#txtUserWgCountry').val() == null || $('#txtUserWgCountry').val() == "") && ((selectedRoleIndex == 0 || selectedRoleIndex == -1))) {
            //  if (searchlist=="")
            // {
            $("#msgUserWgErrorMessage").show();
            $("#msgUserWgErrorMessage").html(singleSelectUserMessages.searchInValid);
            return false;
        }
        else {
            isClickSearch = true;
            $find("SearchWgForUser").sendRefreshRequest();
            $('#btnAddUserForWg').show();
            $('#btnRemoveUserForWg').hide();
            $('#btnUserCancelForWg').show();
            $("#divAddWGUser").show();
            $("#SearchWgForUser").show();
            $('#SearchUserWorkgroupPaging').show();
            $("#msgUserWgErrorMessage").val('');
        }
    });
    //Enter key press event for rolelist
    $('#RolesList').keypress(function (event) {
        var selectedRoleIndex = $('#RolesList')[0].selectedIndex;
        if (selectedRoleIndex != 0 && selectedRoleIndex != -1) {
            if (event.keyCode == '13') {
                $('#btnUserGetWorkGrouplist').click();
            }
        }
    });

    //Add Button Click Event
    $('#btnAddUserForWg').click(function (e) {
        contractCollection = [];
        hashContract = {};
        var gridObj = $find("SearchWgForUser");
        var checkboxes = $("#SearchWgForUser").find(".GridContent #chkUserWgId");
        $.each(checkboxes, function (index, checkbox) {
            selectedright = "";
            isDefault = "";
            workgroupId = "";
            hashUser = {};
            userCollection = [];
            if (checkbox.checked) {
                var isInRole = "false";
                var canManageWorkgroup = "false";
                var isR2Authorized = "false";
                var canAllocateUpc = "false";
                // var row = gridObj._contentTable.rows[index];
                var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];

                var workgroup = $(row).find("#chkUserWgId");
                ID = workgroup[0].value;
                var rightChks = $(row).find(".custom");
                $.each(rightChks, function (index1, checkedbox) {
                    if (checkedbox.checked) {
                        selectedright += checkedbox.value == "on" ? "" : checkedbox.value + ",";
                        if (checkedbox.value == rights_Requestor || checkedbox.value == rights_Reviewer || checkedbox.value == rights_Inquiry) {
                            isInRole = "true";
                        } else if (checkedbox.value == rights_ManageWorkgroup) {
                            canManageWorkgroup = "true";
                        } else if (checkedbox.value == rights_R2Authorized) {
                            isR2Authorized = "true";
                        } else if (checkedbox.value == rights_UPCAllocation) {
                            canAllocateUpc = "true";
                        }
                    }
                });

                var defaultImg = $(row).find("#imgDefault");
                var imgurl = defaultImg[0].src;
                //imgurl = imgurl.substr(imgurl.length - 8);
                if (imgurl.indexOf("grey") != -1) {//if (imgurl == 'grey.png') {
                    isDefault = "false";
                } else {
                    isDefault = "true";
                }
                hashUser = { 'IsUserDefault': isDefault, 'IsInRole': isInRole, 'CanManageWorkgroup': canManageWorkgroup, 'IsR2Authorized': isR2Authorized, 'CanAllocateUpc': canAllocateUpc };
                userCollection.push(hashUser);
                hashWorkgroup = { 'ID': ID, 'Users': userCollection };
                workgroupCollection.push(hashWorkgroup);
                //var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];
                if (($.inArray(index, gridObj.get_SelectionManager().selectedRowsCollection)) == -1) {
                    var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                    gridObj.get_SelectionManager()._mDownHandler(eve);
                }
            } else {
                gridObj.get_SelectionManager().deselectRow(index);
            }
        });
        if (ID != "") {
            var loginUserDetails = $('#hdnSelectedUserDetails').attr("value").split('|');
            var userDetails = { 'LoginName': loginUserDetails[0], 'Name': loginUserDetails[1], 'Email': loginUserDetails[2] };
            var request = workgroupCollection;
            $.ajax({
                url: '/GCS/Workgroup/AddUserInMultipleWorkgroup/',
                type: 'POST',
                data: JSON.stringify({ workgroupDetails: request, userData: userDetails }),
                async: true,
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.toLowerCase() == "true") {
                        $("#AddUserSearchDiv").dialog("close");
                        $("#UserSuccessMessage").show();
                        $("#UserSuccessMessage").html(addUsersuccessfulMsg);
                        return false;
                    } else {
                        $("#AddUserSearchDiv").dialog("close");
                        //$("#UserSuccessMessage").show();
                        //                        $("#UserSuccessMessage").html("Successfully Added the User To Selected WorkGroup");
                    }
                }
            });
        } else {
            $("#msgUserWgErrorMessage").show();
            $("#msgUserWgErrorMessage").html(addremoveUserWarninglMsg);
            return false;
        }
    });

    //Paging DropDown change event
    $('#ddlSearchUserWgPaging').change(function () {
        var selectedRoleIndex = $('#RolesList')[0].selectedIndex;
        if (($('#txtUserWgName').val() == null || $('#txtUserWgName').val() == "") && ($('#txtUserWgCompany').val() == null || $('#txtUserWgCompany').val() == "") && ($('#txtWgUser').val() == null || $('#txtWgUser').val() == "") && ($('#txtUserWgCountry').val() == null || $('#txtUserWgCountry').val() == "") && ((selectedRoleIndex == 0 || selectedRoleIndex == -1))) {
            $("#msgUserWgErrorMessage").show();
            $("#msgUserWgErrorMessage").html(singleSelectUserMessages.searchInValid);
            return false;
        }
        var gridObj = "";
        if (pageName == "Add") {
            gridObj = $find("SearchWgForUser");
            gridObj.sendRefreshRequest();
            $('#btnAddUserForWg').show();
            $('#btnRemoveUserForWg').hide();
            $('#btnUserCancelForWg').show();
            $("#divAddWGUser").show();
            $("#SearchWgForUser").show();
            $('#SearchUserWorkgroupPaging').show();
        } else {
            gridObj = $find("SearchWgForRemoveUser");
            gridObj.sendRefreshRequest();
            $("#SearchWgForRemoveUser").show();
        }
    });

    //Remove User buttong Click Event
    $('#btnRemoveUserForWg').click(function (e) {
        workgroupIds = "";
        var gridObj = $find("SearchWgForRemoveUser");
        var checkboxes = $("#SearchWgForRemoveUser").find(".GridContent #chkRemoveUserWgId");

        $.each(checkboxes, function (index, checkbox) {
            if (checkbox.checked) {
                // var row = gridObj._contentTable.rows[index];
                var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];
                var workgroup = $(row).find("#chkRemoveUserWgId");
                workgroupIds += workgroup[0].value + "|";
                if (($.inArray(index, gridObj.get_SelectionManager().selectedRowsCollection)) == -1) {
                    var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                    gridObj.get_SelectionManager()._mDownHandler(eve);
                }
            } else {
                gridObj.get_SelectionManager().deselectRow(index);
            }
        });
        if (workgroupIds != "") {
            var formValues = "userToRemove=" + $('#hdnSelectedMimicUserName').attr("value") + "&workgroupIdList=" + workgroupIds;
            var url = '/GCS/Workgroup/RemoveUserFromMultipleWorkgroup/';
            $.post(url, formValues, function (workgroupNameList) {
                if (workgroupNameList == "") {
                    $("#RemoveUserSearchDiv").dialog("close");
                    $("#UserSuccessMessage").show();
                    $("#UserSuccessMessage").html(removeUsersuccessfulMsg);
                } else {
                    $("#RemoveUserSearchDiv").dialog("close");
                    $("#selectSingleUserErrorMessage").show();
                    var loginUserDetails = $('#hdnSelectedUserDetails').attr("value").split('|');
                    var displayWorkgroupName = "";
                    if (workgroupNameList.length > 0) {
                        for (var i = 0; i < workgroupNameList.length; i++) {
                            var splitName = workgroupNameList[i].split('|');
                            displayWorkgroupName += displayWorkgroupName + "," + "&lt;&lt;" + splitName[0] + "&gt;&gt; &lt;&lt;" + splitName[1] + "&gt;&gt;";
                        }
                        displayWorkgroupName = displayWorkgroupName.substring(1, displayWorkgroupName.length);
                    }
                    $("#selectSingleUserErrorMessage").html("&lt;&lt;" + loginUserDetails[1] + "&gt;&gt; " + removeUserWarninglMsgFirst + " " + displayWorkgroupName + ".<br> " + removeUserWarninglMsgSecond);
                }
            });
        } else {
            $("#msgUserWgErrorMessage").show();
            $("#msgUserWgErrorMessage").html(addremoveUserWarninglMsg);
            return false;
        }
    });

    //Cancel Button Click Event
    $('#btnUserCancelForWg').click(function () {
        $("#RemoveUserSearchDiv").dialog("close");
        $("#AddUserSearchDiv").dialog("close");
        $("#AddUserSearchDiv").remove();
        $("#SearchWgForUser").hide();
        $("#divAddWGUser").remove();
        $("#SearchWgForUser").remove();
        $("#divRemoveWGUser").remove();
        $("#SearchWgForRemoveUser").remove();
        $("#RemoveUserSearchDiv").remove();
    });
    //Resett Button Click Event
    $("#btnUserGetWorkGroupReset").click(function () {
        if ($('#txtUserWgName') != null) {
            $('#txtUserWgName').val('');
        }
        if ($('#txtUserWgCompany') != null) {
            $('#txtUserWgCompany').val('');
        }
        if ($('#txtWgUser') != null) {
            $('#txtWgUser').val('');
        }
        if ($('#txtUserWgCountry') != null) {
            $('#txtUserWgCountry').val('');
        }
        if (jQuery("#txtUserWgName") != null) {
            jQuery("#txtUserWgName").watermark(watermarkname);
        }
        if (jQuery("#txtUserWgCompany") != null) {
            jQuery("#txtUserWgCompany").watermark(watermarkcompany);
        }
        if (jQuery("#txtWgUser") != null) {
            jQuery("#txtWgUser").watermark(watermarkuser);
        }
        if (jQuery("#txtUserWgCountry") != null) {
            jQuery("#txtUserWgCountry").watermark(watermarkcountry);
        }
        document.getElementById('RolesList').selectedIndex = 0;
        // $("#divAddWGUser").hide();
        // $("#SearchWgForUser").hide();
        //$('#btnAddUserForWg').hide();
        // $('#btnUserCancelForWg').hide();
        //$('#SearchUserWorkgroupPaging').hide();
        $("#msgUserWgErrorMessage").val('');
        $("#msgUserWgErrorMessage").hide();
        $("#UserSuccessMessage").val('');
        $("#UserSuccessMessage").hide();
    });
});

//ActionBegin function for AddUser Popup

function ActionBegin(sender, args) {
    searchlist = "";
    document.getElementById('spnUserSearchWgResult').innerHTML = "";
    var name = $('#txtUserWgName').val();
    var company = $('#txtUserWgCompany').val();
    var user = $('#txtWgUser').val();
    var country = $('#txtUserWgCountry').val();
    var roleValue = -1;
    var selectedRoleIndex = "";
    selectedRoleIndex = $('#RolesList')[0].selectedIndex;
    var roletext = $('#RolesList')[0][selectedRoleIndex].text;
    var roleId = $('#RolesList')[0][selectedRoleIndex].value;
    if (roleId.length > 0) roleValue = roleId;
    if (name != '') {
        searchlist = searchlist + name + '/';
    }
    if (selectedRoleIndex != 0) {
        searchlist = searchlist + roletext + '/';
    }
    if (company != '') {
        searchlist = searchlist + company + '/';
    }
    if (user != '') {
        searchlist = searchlist + user + '/';
    }
    if (country != '') {
        searchlist = searchlist + country + '/';
    }
    searchlist = searchlist.substring(0, searchlist.length - 1);
    document.getElementById('spnUserSearchWgResult').innerHTML = searchlist;
    args.data["workgroupName"] = name;
    args.data["workgroupRole"] = roleValue;
    args.data["workgroupCompany"] = company;
    args.data["workgroupUser"] = user;
    args.data["workgroupCountry"] = country;
    args.data["isStatus"] = false;
    // args.data["PageSize"] = $('#ddlSearchUserWgPaging') != null ? $('#ddlSearchUserWgPaging').val() : 10;
    args.data["searchLoginId"] = $('#hdnSelectedMimicUserName').attr("value");
    //        $('#btnAddUserForWg').show();
    //        $('#btnRemoveUserForWg').hide();
    //        $('#btnUserCancelForWg').show();
    //        $("#divAddWGUser").show();
    //        $("#SearchWgForUser").show();
    //        $('#SearchUserWorkgroupPaging').show();

    $('#btnAddUserForWg').hide();
    $('#btnRemoveUserForWg').hide();
    $('#btnUserCancelForWg').hide();
    if (isClickSearch) {
        $("#divAddWGUser").show();
        $("#SearchWgForUser").show();
        $('#SearchUserWorkgroupPaging').show();
    }
    else {
        $("#divAddWGUser").hide();
        $("#SearchWgForUser").hide();
        $('#SearchUserWorkgroupPaging').hide();
    }

    $("#SearchWgForUserwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
    $('.EmptyCell').html("Retrieving data....");
}

//Showing the Grid Count

function ShowResultCountForUser(sender, args) {
    $("#spnUserSearchWgResult").html(searchlist + '&nbsp;(' + sender._totalRecordsCount + ')');
    if (sender._totalRecordsCount > 0) { $('#btnAddUserForWg').show(); $('#btnUserCancelForWg').show(); }
}

//Default user flag setting

function Imageclick(sender) {
    var imgurl = sender.src;
    //imgurl = imgurl.substr(imgurl.length - 8);
    if (imgurl.indexOf("grey") != -1) {//if (imgurl == 'grey.png') {
        sender.src = sender.src.replace('i_flag_grey', 'i_flag_green'); //event.src = "/Gcs/Images/i_flag_green.png";
    } else {
        sender.src = sender.src.replace('i_flag_green', 'i_flag_grey'); // event.src = "/Gcs/Images/i_flag_grey.png";
    }
    // return event;
}

//During the Grid Loading setting the checkbox for select and Rights Flag

function SetRightCheckBox(sender, args) {
    //Working
    //        if (args.Column.Name == "Rights") {
    //            var chkBox = '';
    //            if ($.trim(args.Data.RoleName).toLowerCase().indexOf('requestor') != -1) {
    //                chkBox = '<input id="chkIsRole" class="custom" value="Requestor" type="checkbox" checked="true" /><label for="chkIsRole">Requestor</label><input id="hkUPCAllocation" type="checkbox" class="custom" value="UPC Allocation" /><label for="hkUPCAllocation">UPC Allocation</label> <br><input id="chkR2Authorized" class="custom" type="checkbox" value="R2 Authorized" /><label for="chkR2Authorized">R2 Authorized</label><input id="chkManageWorkgroup" class="custom" value="Manage Workgroup" type="checkbox" /><label for="chkManageWorkgroup">Manage Workgroup</label>';
    //            } else if ($.trim(args.Data.RoleName).toLowerCase().indexOf('inquiry') != -1) {
    //                chkBox = '<input id="chkIsRole" class="custom" value="Inquiry" type="checkbox" checked="true" /><label for="chkIsRole">Inquiry</label><input id="chkManageWorkgroup" class="custom" value="Manage Workgroup" type="checkbox" /><label for="chkManageWorkgroup">Manage Workgroup</label>';
    //            } else if ((($.trim(args.Data.RoleName).toLowerCase().indexOf('reviewer') != -1) || ($.trim(args.Data.RoleName).toLowerCase().indexOf('umgi global clearance') != -1)) && args.Data.StatusType == "Parent") {
    //                chkBox = '<input id="chkIsRole" class="custom" value="Reviewer"  checked="true" type="checkbox" /><label for="chkIsRole">Reviewer</label><input class="custom" id="chkManageWorkgroup" value="Manage Workgroup" type="checkbox" /><label for="chkManageWorkgroup">Manage Workgroup</label';
    //            } else if ((($.trim(args.Data.RoleName).toLowerCase().indexOf('reviewer') != -1) || ($.trim(args.Data.RoleName).toLowerCase().indexOf('umgi global clearance') != -1)) && args.Data.StatusType == "Child") {
    //                chkBox = '<input id="chkIsRole" class="custom" disabled="true" value="Reviewer" checked="true" type="checkbox">Reviewer</input>';
    //            }
    //            $(args.Element)[0].innerHTML = chkBox;
    //        }

    if (args.Column.Name == "Rights") {
        var chkBox = '';
        if ($.trim(args.Data.RoleName).toLowerCase().indexOf('requestor') != -1) {
            chkBox = '<input id="chkIsRole" class="custom" value="' + mgUserRequestorRights.Requestor + '" type="checkbox" checked="true" /><label for="chkIsRole">' + mgUserRequestorRights.Requestor + '</label><input id="hkUPCAllocation" type="checkbox" class="custom" value="' + mgUserRequestorRights.UPCAllocation + '" /><label for="hkUPCAllocation">' + mgUserRequestorRights.UPCAllocation + '</label> <br><input id="chkR2Authorized" class="custom" type="checkbox" value="' + mgUserRequestorRights.R2Authorized + '" /><label for="chkR2Authorized">' + mgUserRequestorRights.R2Authorized + '</label><input id="chkManageWorkgroup" class="custom" value="' + mgUserRequestorRights.ManageWorkgroup + '" type="checkbox" /><label for="chkManageWorkgroup">' + mgUserRequestorRights.ManageWorkgroup + '</label>';
        } else if ($.trim(args.Data.RoleName).toLowerCase().indexOf('inquiry') != -1) {
            chkBox = '<input id="chkIsRole" class="custom" value="Inquiry" type="checkbox" checked="true" /><label for="chkIsRole">Inquiry</label><input id="chkManageWorkgroup" class="custom" value="' + mgUserRequestorRights.ManageWorkgroup + '" type="checkbox" /><label for="chkManageWorkgroup">' + mgUserRequestorRights.ManageWorkgroup + '</label>';
        } else if ((($.trim(args.Data.RoleName).toLowerCase().indexOf('reviewer') != -1) || ($.trim(args.Data.RoleName).toLowerCase().indexOf('umgi global clearance') != -1)) && args.Data.StatusType == "Parent") {
            chkBox = '<input id="chkIsRole" class="custom" value="' + mgUserRequestorRights.Reviewer + '"  checked="true" type="checkbox" /><label for="chkIsRole">' + mgUserRequestorRights.Reviewer + '</label><input class="custom" id="chkManageWorkgroup" value="' + mgUserRequestorRights.ManageWorkgroup + '" type="checkbox" /><label for="chkManageWorkgroup">' + mgUserRequestorRights.ManageWorkgroup + '</label';
        } else if ((($.trim(args.Data.RoleName).toLowerCase().indexOf('reviewer') != -1) || ($.trim(args.Data.RoleName).toLowerCase().indexOf('umgi global clearance') != -1)) && args.Data.StatusType == "Child") {
            chkBox = '<input id="chkIsRole" class="custom" disabled="true" value="' + mgUserRequestorRights.Reviewer + '" checked="true" type="checkbox">' + mgUserRequestorRights.Reviewer + '</input>';
        }
        $(args.Element)[0].innerHTML = chkBox;
    }
    if (args.Column.Name == "ParentId") {
        $(args.Element)[0].innerHTML = args.Data.ParentID;
    }
    if (args.Column.Name == "Id") {
        $(args.Element)[0].innerHTML = args.Data.ID;
    }
    if (args.Column.Name == "Select") {
        var chkSelectBox = '';
        chkSelectBox = '<input id="chkUserWgId" value=' + args.Data.ID + ' type="checkbox"/>';
        $(args.Element)[0].innerHTML = chkSelectBox;
    }
    if (args.Column.Name == "Default") {
        if (($.trim(args.Data.RoleName).toLowerCase().indexOf('requestor') != -1) || ($.trim(args.Data.RoleName).toLowerCase().indexOf('inquiry') != -1) || ($.trim(args.Data.RoleName).indexOf('RCC Admin') != -1)) {
            $(args).find("#imgDefault").hide();
            var imgSelect = '<IMG style="display: none;" id=imgDefault title="Select Default Users" onClick="Imageclick(this)" src="/Gcs/Images/i_flag_grey.png">';
            $(args.Element)[0].innerHTML = imgSelect;
        }
        else {
            var imgSelect = '<img id="imgDefault" src="/Gcs/Images/i_flag_grey.png" title="Select Default Users" onClick="Imageclick(this)" />';
            $(args.Element)[0].innerHTML = imgSelect;
        }
    }
}

function RecordBind(sender, args) {
    if (args.Column.Name == "ParentId") {
        $(args.Element)[0].innerHTML = args.Data.ParentID;
    }
    if (args.Column.Name == "Id") {
        $(args.Element)[0].innerHTML = args.Data.ID;
    }
    if (args.Column.Name == "Select") {
        var chkBox = '';
        chkBox = '<input id="chkRemoveUserWgId" value=' + args.Data.ID + ' type="checkbox"/>';
        $(args.Element)[0].innerHTML = chkBox;
    }
}

//ActionBegin function for RemoveUser Popup

function RemoveUserFromWG(sender, args) {
    args.data["loginName"] = $('#hdnSelectedMimicUserName').attr("value");
    // args.data["PageSize"] = $('#ddlSearchUserWgPaging') != null ? $('#ddlSearchUserWgPaging').val() : 10;
    $('#btnAddUserForWg').hide();
    $('#btnRemoveUserForWg').show();
    $('#btnUserCancelForWg').show();
    document.getElementById('searchLableDiv').innerHTML = ExistingUsersMsg;
    $("#searchContainerForUser").hide();
    $("#SearchWgForRemoveUserwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
    $('.EmptyCell').html("Retrieving data....");
}

function RemoveUserGridLoad(sender, args) {
    var chk = $("#RemoveUserSearchDiv .GridHeader").find("th.HeaderCell .HeaderCellDiv")[0];
    $(chk).append("<input type=\"Checkbox\" id=\"chkAllForRemoveUsers\" onclick=\"CheckBoxAllClickForRemoveUser(event)\">");
}

function CheckBoxAllClickForRemoveUser(args) {
    var obj = $("#SearchWgForRemoveUser").find(".GridHeader #chkAllForRemoveUsers");
    if (obj.attr("checked") == "checked") {
        $(".GridContent").find('#chkRemoveUserWgId').attr("checked", "checked");
    }
    else {
        $(".GridContent").find('#chkRemoveUserWgId').removeAttr("checked");
    }
    synGridCheckBoxSelectionForRemoveUsers(args);
}

function synGridCheckBoxSelectionForRemoveUsers(events) {
    var gridObj = $find("SearchWgForRemoveUser");
    var checkboxes = $("#SearchWgForRemoveUser").find(".GridContent #chkRemoveUserWgId");
    $.each(checkboxes, function (index, checkbox) {
        if (checkbox.checked) {
            var row = gridObj._contentTable.rows[index];
            var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];
            // var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];
            if (($.inArray(index, gridObj.get_SelectionManager().selectedRowsCollection)) == -1) {
                var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                gridObj.get_SelectionManager()._mDownHandler(eve);
            }
        }
        else {
            gridObj.get_SelectionManager().deselectRow(index);
        }
    });
}

//Add User CheckBox Selection All

function AddUserGridLoad(sender, args) {
    var chk = $("#AddUserSearchDiv .GridHeader").find("th.HeaderCell .HeaderCellDiv")[0];
    $(chk).append("<input type=\"Checkbox\" id=\"chkAllForAddUsers\" onclick=\"CheckBoxAllClickForAddUser(event)\">");
}

function CheckBoxAllClickForAddUser(args) {
    var obj = $("#SearchWgForUser").find(".GridHeader #chkAllForAddUsers");
    if (obj.attr("checked") == "checked") {
        $(".GridContent").find('#chkUserWgId').attr("checked", "checked");
    }
    else {
        $(".GridContent").find('#chkUserWgId').removeAttr("checked");
    }
    synGridCheckBoxSelectionForAddUsers(args);
}

function synGridCheckBoxSelectionForAddUsers(events) {
    var gridObj = $find("SearchWgForUser");
    var checkboxes = $("#SearchWgForUser").find(".GridContent #chkUserWgId");
    $.each(checkboxes, function (index, checkbox) {
        if (checkbox.checked) {
            // var row = gridObj._contentTable.rows[index];
            var row = gridObj.get_ContentTable().getElementsByTagName("tr")[index];
            if (($.inArray(index, gridObj.get_SelectionManager().selectedRowsCollection)) == -1) {
                var eve = { target: $(row).children('td.RowCell')[0], ctrlKey: true };
                gridObj.get_SelectionManager()._mDownHandler(eve);
            }
        }
        else {
            gridObj.get_SelectionManager().deselectRow(index);
        }
    });
}