/* Mimic User - Starts */
var isClickSearch = false;
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function (e) {
    $('div[class*=pageSizeCustomizer] input:text').bind('keyup', function (e) {
        var txtVal = $('div[class*=pageSizeCustomizer] input:text').val();
        if (txtVal > 100) {
            $("#selectSingleUserErrorMessage").show();
            $("#selectSingleUserErrorMessage").html("Items per page cannot exceed 100 records");
            if (e.keyCode == 13) {
                e.stopImmediatePropagation();
            }
        }
        else {
            $("#selectSingleUserErrorMessage").hide();
        }
    });

    $("#selectSingleUserErrorMessage").hide();
    $("#selectSingleUserErrorMessage").html("");
    $("#UserSuccessMessage").hide();
    $("#UserSuccessMessage").html("");

    if (jQuery("#txtselectSingleUserName") != null) {
        jQuery("#txtselectSingleUserName").watermark(watermarkUserName);
    }
    if (jQuery("#txtselectSingleUserId") != null) {
        jQuery("#txtselectSingleUserId").watermark(watermarkUserId);
    }
    if (jQuery("#txtselectSingleUserCountry") != null) {
        jQuery("#txtselectSingleUserCountry").watermark(watermarkCountry);
    }
    //Bug Id 4579 has been fixed.
    //MimicUser popup keypress event when search
    $('#txtselectSingleUserName').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnselectSingleSearchUser').click();
            return false;
        }
    });
    $('#txtselectSingleUserId').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnselectSingleSearchUser').click();
            return false;
        }
    });
    $('#txtselectSingleUserCountry').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnselectSingleSearchUser').click();
            return false;
        }
    });
    //Bug Id 4579 has been fixed.

    $("#btnselectSingleSearchUser").click(function () {
        isClickSearch = true;
        $("#selectSingleUserErrorMessage").hide();
        $("#selectSingleUserErrorMessage").html("");
        $("#UserSuccessMessage").html("");
        $("#UserSuccessMessage").hide();
        $("#hdnSelectedMimicUserName").val("");
        if (($('#txtselectSingleUserName').val() == null || $('#txtselectSingleUserName').val() == "") && ($('#txtselectSingleUserId').val() == null || $('#txtselectSingleUserId').val() == "") && ($('#txtselectSingleUserCountry').val() == null || $('#txtselectSingleUserCountry').val() == "")) {
            $("#selectSingleUserErrorMessage").show();
            $("#selectSingleUserErrorMessage").html(singleSelectUserMessages.searchInValid);
            return false;
        }
        $("#selectSingleUserSearchGrid").show();
        $("#selectSingleUserSearchGrid").css({ display: "block" });
        $("#btnDivSearchWorkgroupForUser").show();
        $("#selectSingleUserSearchResults").css({ display: "block" });
        $("#btnDivSearchWorkgroupForUser").css({ display: "block" });
        //            $("#selectSingleUserSearchResults").css({ display: "block" });
        //            $("#selectSingleUserSearchGrid").css({ display: "block" });

        //        //Pagename Checking for Add and Remove User in Workgroup
        //        if (pageName == "SearchWorkGroupToAddRemoveUsers") {
        //            $("#btnDivSearchWorkgroupForUser").show();
        //            $("#mimicUserBottomBarButton").hide();
        //            $("#btnDivSearchWorkgroupForUser").css({ display: "block" });
        //            $("#selectSingleUserSearchResults").css({ display: "block" });
        //            $("#selectSingleUserSearchGrid").css({ display: "block" });
        //        } else if (pageName == "MimicUser") {
        //            $("#mimicUserBottomBarButton").css({ display: "block" });
        //            $("#mimicUserBottomBarButton").show();
        //            $("#btnDivSearchWorkgroupForUser").hide();
        //            $("#selectSingleUserSearchResults").css({ display: "block" });
        //            $("#selectSingleUserSearchGrid").css({ display: "block" });
        ////            $("#mimicUserBottomBarButton").css({ display: "block" });
        //        }

        var gridObj = $find("SelectSingleUserGrid");
        gridObj.sendRefreshRequest();
        //hide syncfusion grid defalut loading image
        $("#SelectSingleUserGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
    });

    $("#btnselectSingleUserReset").click(function () {
        $("#selectSingleUserErrorMessage").hide();
        $("#selectSingleUserErrorMessage").html("");
        $("#UserSuccessMessage").val('');
        $("#UserSuccessMessage").hide();

        if ($('#txtselectSingleUserName') != null) {
            $('#txtselectSingleUserName').val('');
        }
        if ($('#txtselectSingleUserId') != null) {
            $('#txtselectSingleUserId').val('');
        }
        if ($('#txtselectSingleUserCountry') != null) {
            $('#txtselectSingleUserCountry').val('');
        }
        if (jQuery("#txtselectSingleUserName") != null) {
            jQuery("#txtselectSingleUserName").watermark(watermarkUserName);
        }
        if (jQuery("#txtselectSingleUserId") != null) {
            jQuery("#txtselectSingleUserId").watermark(watermarkUserId);
        }
        if (jQuery("#txtselectSingleUserCountry") != null) {
            jQuery("#txtselectSingleUserCountry").watermark(watermarkCountry);
        }
    });

    $("#btnMimicCancel").click(function () {
        $("#selectSingleUserDialog").dialog('close');
        $("#selectSingleUserErrorMessage").hide();
        $("#selectSingleUserErrorMessage").html("");
    });

    $('#ddlselectSingleUserPaging').change(function () {
        isClickSearch = true;
        var gridObj = $find("SelectSingleUserGrid");
        gridObj.sendRefreshRequest();
    });

    //Add user Cancel button click

    $("#btnCancelUsersInWorkgroup").click(function () {
        $("#selectSingleUserErrorMessage").hide();
        $("#selectSingleUserErrorMessage").html("");
        window.location.href = "/GCS/workgroup/";
    });
    //AddUser Dialog Open
    $('#btnAddUsersInWorkgroup').click(function () {
        if ($("#hdnSelectedMimicUserName").attr("value") != "") {
            var workgroupDialog = $('<div id="AddUserSearchDiv"></div>')
                .dialog({
                    autoOpen: true,
                    modal: true,
                    title: addUserToWorkgroupTitle,
                    show: 'clip',
                    hide: 'clip',
                    width: "95%", //1000,
                    resizable: false,
                    position: [(($(window).width() - (($(window).width() * 95) / 100)) / 2), 50],
                    beforeClose: function (event, ui) {
                        $("#AddUserSearchDiv").remove();
                        $("#divAddWGUser").remove();
                        $("#SearchWgForUser").remove();
                    }
                });
            workgroupDialog.load('/GCS/Workgroup/SearchWorkgroupToAddUsers');
            $("#selectSingleUserErrorMessage").hide();
            $("#selectSingleUserErrorMessage").html("");
            $("#UserSuccessMessage").hide();
            $("#UserSuccessMessage").html("");
            //            $('#AddUserSearchDiv').css("overflowY", "auto");
            $('#AddUserSearchDiv').css("height", "auto");
        } else {
            $("#selectSingleUserErrorMessage").show();
            $("#selectSingleUserErrorMessage").html(singleSelectUserMessages.selectRow);
        }
    });

    //Remove User Dialog Open
    $('#btnRemoveUsersFromWorkgroup').click(function () {
        if ($("#hdnSelectedMimicUserName").attr("value") != "") {
            var workgroupDialog = $('<div id="RemoveUserSearchDiv"></div>')
                .dialog({
                    autoOpen: true,
                    modal: true,
                    title: removeUserFromWorkgroupTitle,
                    show: 'clip',
                    hide: 'clip',
                    width: "95%", //1000,
                    resizable: false,
                    position: [(($(window).width() - (($(window).width() * 95) / 100)) / 2), 50],
                    beforeClose: function (event, ui) {
                        // $("#RemoveUserSearchDiv").dialog("close");
                        $("#divRemoveWGUser").remove();
                        $("#SearchWgForRemoveUser").remove();
                        $("#RemoveUserSearchDiv").remove();
                    }
                });
            workgroupDialog.load('/GCS/Workgroup/SearchWorkgroupForRemoveUsers');
            $('#RemoveUserSearchDiv').css("overflowY", "auto");
            $('#RemoveUserSearchDiv').css("height", "auto");
            $("#selectSingleUserErrorMessage").hide();
            $("#selectSingleUserErrorMessage").html("");
            $("#UserSuccessMessage").hide();
            $("#UserSuccessMessage").html("");
        } else {
            $("#selectSingleUserErrorMessage").show();
            $("#selectSingleUserErrorMessage").html(singleSelectUserMessages.selectRow);
        }
    });
});

function ActionStart(sender, args) {
    if (!isClickSearch) {
        // $("#MimicUserGridwaiting_WaitingPopup_Image").attr('src', '');
        $("#SelectSingleUserGridwaiting_WaitingPopup_Image").hide();
    }
    else {
        $("#SelectSingleUserGridwaiting_WaitingPopup_Image").show();
    }

    // var mimicUserLoadingPanel = $($.find('#loadingDv'));
    // mimicUserLoadingPanel.show();
    var name = $('#txtselectSingleUserName').val();
    var userId = $('#txtselectSingleUserId').val();
    var country = $('#txtselectSingleUserCountry').val();
    args.data["userName"] = name;
    args.data["loginName"] = userId;
    args.data["country"] = country;
    args.data["pageName"] = pageName;
    //args.data["jtPageSize"] = $('#ddlselectSingleUserPaging') != null ? $('#ddlselectSingleUserPaging').val() : 10;
    $('.EmptyCell').html("Retrieving data....");
}

function ActionComplete(sender, args) {
    var mimicUserLoadingPanel = $($.find('#loadingDv'));
    //   mimicUserLoadingPanel.hide();
}
function ShowResultCount(sender, args) {
    var searchlist = '';
    var userName = $('#txtselectSingleUserName').val();
    var userId = $('#txtselectSingleUserId').val();
    var country = $('#txtselectSingleUserCountry').val();

    if (userName != '') { searchlist = searchlist + userName + '/'; }
    if (userId != '') { searchlist = searchlist + userId + '/'; }
    if (country != '') { searchlist = searchlist + country; }
    if (country == '') {
        searchlist = searchlist.substring(0, searchlist.length - 1);
    }
    $("#selectSingleUserSearchResultCount").html('"' + searchlist + '"' + '&nbsp;(' + sender._totalRecordsCount + ')');
}
function OnUserSelection(sender) {
    var selectedUser = sender.parentNode.parentNode.childNodes[3].textContent || sender.parentElement.parentElement.childNodes[3].innerText;
    $("#hdnSelectedMimicUserName").val(selectedUser);
    var childNode2 = sender.parentNode.parentNode.childNodes[2].textContent || sender.parentElement.parentElement.childNodes[2].innerText;
    var childNode3 = sender.parentNode.parentNode.childNodes[3].textContent || sender.parentElement.parentElement.childNodes[3].innerText;
    var childNode5 = sender.parentNode.parentNode.childNodes[5].textContent || sender.parentElement.parentElement.childNodes[5].innerText;
    $("#hdnSelectedUserDetails").val(childNode3 + "|" + childNode2 + "|" + childNode5);
    //$("#hdnSelectedUserDetails").val(sender.parentElement.parentElement.childNodes[3].innerText || sender.parentElement.parentElement.childNodes[3].textContent + "|" + sender.parentElement.parentElement.childNodes[2].innerText || sender.parentElement.parentElement.childNodes[2].textContent + "|" + sender.parentElement.parentElement.childNodes[5].innerText || sender.parentElement.parentElement.childNodes[5].textContent);
}
function AddRecordCheckBox(sender, args) {
    if (args.Column.Name == selectColumnName) { //"Select") {
        var btnRadio = '';
        btnRadio = "<input type=\"radio\" name=\"mimicUser\" onClick=\"OnUserSelection(this)\" id=\"rdUser\">"
        $(args.Element)[0].innerHTML = btnRadio;
    }
}
/* Mimic User - Ends */