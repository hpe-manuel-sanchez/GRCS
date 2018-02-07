var isClickSearch = false;
$('.ui-dialog-titlebar-close').attr("title", "Close");
/* Mimic User - Starts */
$(document).ready(function (e) {
    $('div[class*=pageSizeCustomizer] input:text').bind('keyup', function (e) {
        var txtVal = $('div[class*=pageSizeCustomizer] input:text').val();
        if (txtVal > 100) {
            $("#mimicUserErrorMessage").show();
            $("#mimicUserErrorMessage").html("Items per page cannot exceed 100 records");
            if (e.keyCode == 13) {
                e.stopImmediatePropagation();
            }
        }
        else {
            $("#mimicUserErrorMessage").hide();
        }
    });

    $("#mimicUserErrorMessage").hide();
    $("#mimicUserErrorMessage").html("");

    if (jQuery("#txtMimicUserName") != null) {
        jQuery("#txtMimicUserName").watermark(watermarkUserName);
    }
    if (jQuery("#txtMimicUserId") != null) {
        jQuery("#txtMimicUserId").watermark(watermarkUserId);
    }
    if (jQuery("#txtMimicUserCountry") != null) {
        jQuery("#txtMimicUserCountry").watermark(watermarkCountry);
    }
    //Bug Id 4579 has been fixed.
    //MimicUser popup keypress event when search
    $('#txtMimicUserName').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnMimicSearchUser').click();
            return false;
        }
    });
    $('#txtMimicUserId').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnMimicSearchUser').click();
            return false;
        }
    });
    $('#txtMimicUserCountry').keypress(function (event) {
        if (event.keyCode == '13') {
            $('#btnMimicSearchUser').click();
            return false;
        }
    });
    //Bug Id 4579 has been fixed.

    $("#btnMimicSearchUser").click(function () {
        isClickSearch = true;
        $("#mimicUserErrorMessage").hide();
        $("#mimicUserErrorMessage").html("");
        $("#hdnSelectedMimicUserName").val("");
        if (($('#txtMimicUserName').val() == null || $('#txtMimicUserName').val() == "") && ($('#txtMimicUserId').val() == null || $('#txtMimicUserId').val() == "") && ($('#txtMimicUserCountry').val() == null || $('#txtMimicUserCountry').val() == "")) {
            $("#mimicUserErrorMessage").show();
            $("#mimicUserErrorMessage").html(singleSelectUserMessages.searchInValid);
            return false;
        }
        $("#mimicUserBottomBarButton").css({ display: "block" });
        $("#mimicUserBottomBarButton").show();
        $("#mimicUserSearchResults").css({ display: "block" });
        $("#mimicUserSearchGrid").css({ display: "block" });

        var gridObj = $find("MimicUserGrid");
        gridObj.sendRefreshRequest();
        //hide syncfusion grid defalut loading image
        $("#MimicUserGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
    });

    $("#btnMimicUserReset").click(function () {
        $("#mimicUserErrorMessage").hide();
        $("#mimicUserErrorMessage").html("");
        $("#UserSuccessMessage").val('');
        $("#UserSuccessMessage").hide();

        if ($('#txtMimicUserName') != null) {
            $('#txtMimicUserName').val('');
        }
        if ($('#txtMimicUserId') != null) {
            $('#txtMimicUserId').val('');
        }
        if ($('#txtMimicUserCountry') != null) {
            $('#txtMimicUserCountry').val('');
        }
        if (jQuery("#txtMimicUserName") != null) {
            jQuery("#txtMimicUserName").watermark(watermarkUserName);
        }
        if (jQuery("#txtMimicUserId") != null) {
            jQuery("#txtMimicUserId").watermark(watermarkUserId);
        }
        if (jQuery("#txtMimicUserCountry") != null) {
            jQuery("#txtMimicUserCountry").watermark(watermarkCountry);
        }
    });

    $("#btnMimicUser").click(function () {
        $("#mimicUserErrorMessage").hide();
        $("#mimicUserErrorMessage").html("");
        var userName = "";
        var isMimicedUser = "";
        userName = $('#hdnSelectedMimicUserName').attr("value");
        //  var mimicUserLoadingPanel = $($.find('#loadingDv'));
        if (userName != "") {
            isMimicedUser = true;
            $.ajax({
                url: '/GCS/Workgroup/MimicUserInfo/',
                type: 'POST',
                dataType: 'json',
                async: true,
                data: { userName: userName, isMimic: isMimicedUser },
                beforeSend: function () {
                    //    	mimicUserLoadingPanel.show();
                },
                success: function (data) {
                    if (data.Result != '') {
                        $("#mimicUserErrorMessage").show();
                        $("#mimicUserErrorMessage").html(singleSelectUserMessages.userNotMappedToWorkgroup); //"Selected User does'nt belongs to any Workgroup");
                        //         mimicUserLoadingPanel.hide();
                        return false;
                    }
                    else {
                        window.location.href = "/GCS/workgroup/";
                    }
                    //   mimicUserLoadingPanel.hide();
                }
            });
        }
        else {
            $("#mimicUserErrorMessage").show();
            $("#mimicUserErrorMessage").html(singleSelectUserMessages.selectRow); //"Please Select any One User"
            return false;
        }
    });

    $("#btnMimicCancel").click(function () {
        $("#MimicUserGridwaiting_WaitingPopup_Image").hide(); $("#MimicUserGridwaiting_WaitingPopup_Panel").hide(); $("#mimicUserDialog").dialog('close');
        $("#mimicUserErrorMessage").hide();
        $("#mimicUserErrorMessage").html("");
    });

    $('#ddlMimicUserPaging').change(function () {
        isClickSearch = true;
        var gridObj = $find("MimicUserGrid");
        gridObj.sendRefreshRequest();
    });

    //Add user Cancel button click

    $("#btnCancelUsersInWorkgroup").click(function () {
        $("#mimicUserErrorMessage").hide();
        $("#mimicUserErrorMessage").html("");
        window.location.href = "/GCS/workgroup/";
    });
});

function ActionBegin(sender, args) {
    // var mimicUserLoadingPanel = $($.find('#loadingDv'));
    // mimicUserLoadingPanel.show();
    if (!isClickSearch) {
        // $("#MimicUserGridwaiting_WaitingPopup_Image").attr('src', '');
        $("#MimicUserGridwaiting_WaitingPopup_Image").hide();
    }
    else {
        $("#MimicUserGridwaiting_WaitingPopup_Image").show();
    }
    var name = $('#txtMimicUserName').val();
    var userId = $('#txtMimicUserId').val();
    var country = $('#txtMimicUserCountry').val();
    args.data["userName"] = name;
    args.data["loginName"] = userId;
    args.data["country"] = country;
    args.data["pageName"] = pageName;
    // args.data["jtPageSize"] = $('#ddlMimicUserPaging') != null ? $('#ddlMimicUserPaging').val() : 10;
    $('.EmptyCell').html("Retrieving data....");
}

function ActionComplete(sender, args) {
    var mimicUserLoadingPanel = $($.find('#loadingDv'));
    //   mimicUserLoadingPanel.hide();
}
function ShowResultCount(sender, args) {
    var searchlist = '';
    var userName = $('#txtMimicUserName').val();
    var userId = $('#txtMimicUserId').val();
    var country = $('#txtMimicUserCountry').val();

    if (userName != '') { searchlist = searchlist + userName + '/'; }
    if (userId != '') { searchlist = searchlist + userId + '/'; }
    if (country != '') { searchlist = searchlist + country; }
    if (country == '') {
        searchlist = searchlist.substring(0, searchlist.length - 1);
    }
    $("#mimicUserSearchResultCount").html('"' + searchlist + '"' + '&nbsp;(' + sender._totalRecordsCount + ')');
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