$(document).ready(function () {
    $('.ui-dialog-titlebar-close').attr("title", "Close");
    var userPreferencesData = {};
    var prevPreferenceId = null;
    var InboxRole = "";
    var RqstCmpny = null;
    var Currency = null;
    var rcvArtstMgmnt = null;
    var rcvUPCAlloc = null;
    var rcvEmlInbx = null;
    var rqstRmvdInbx = null;
    var addRsrcReq = null;
    var reqRmvdArtst = null;
    var reqMstrPrjct = null;
    var autPrjctCompl = null;

    var FnlRvwRqst = false;
    var ConsolidatedEmail = false;
    var rcvArtstMgmnt = false;
    var rcvUPCAlloc = false;
    var rcvEmlInbx = false;
    var rqstRmvdInbx = false;
    var addRsrcReq = false;
    var reqRmvdArtst = false;
    var reqMstrPrjct = false;
    var autPrjctCompl = false;
    var defaultcurrency = 'USD';
    var rstmulrcvEmlInbx = [];
    var rstmulrqstRmvdInbx = [];
    var rstaddRsrcReq = [];
    var rstreqRmvdArtst = [];
    var rstreqMstrPrjct = [];
    var rstautPrjctCompl = [];
    var hasError = false;
    var ConsolidatedEmailPref = 1;
    var initialLoad = true;

    $('#errorMessage').hide();
    $('#divMessage').hide();
    $('.requestorrdobtn').click(function () {
        $('#requestor').attr("checked", false);
        InboxRole = "Requestor";
    });
    // var isRequestor = '@ViewBag.Requstor';
    if ($('#initialLoad').val() == "False") {
        var initialLoad = false;
    }

    if (initialLoad == false) {
        $('.multiselect').multiselect("checkAll");
    }
    if ($('#companyVal').val() != null || $('#companyVal').val() != "") {
        $('#ddlRequestngCompany').val($('#companyVal').val());
    }
    if (chkRequestor == "True") {
        if ($('#CurrencyId').val() == null || $('#CurrencyId').val() == "") {
            $('#ddlCurrList').length
            var Currency = $('#ddlCurrList')[0];
            for (var i = 0; i < Currency.length; i++) {
                if (Currency[i].innerHTML == defaultcurrency) {
                    Currency[i].selected = true;
                }
            }
        }
        // if ($('#CurrencyId').val() != null || $('#CurrencyId').val() != "") {
        if ($('#CurrencyId').length > 0) {
            var Currency = $('#ddlCurrList')[0];
            for (var i = 0; i < Currency.length; i++) {
                if (Currency[i].innerHTML == $('#CurrencyId').val()) {
                    Currency[i].selected = true;
                }
            }
        }
    }

    $('.multiselect').multiselect();
    $('.multiselect').multiselect("uncheckAll");
    $('.multiselect').multiselect({
        header: false,
        selectedText: 'Select Workgroups',
        noneSelectedText: 'Select Workgroups',
        height: "100px"
    });
    if (initialLoad == true) {
        $('.multiselect').multiselect("checkAll");
    }

    $.each($('#userDDL').find(".currency").children(), function (index, savedRow) {
        var isDefaultImage = savedRow.children[0].children[0].checked;
        var id = savedRow.children[0].children[1].value;
        // var singlWgList = $('#SingleValueList').val();
        if (workGroupCount > 1) {
            if (isDefaultImage == true) {
                if (savedRow.children[0].children[3] != undefined) {
                    var list = savedRow.children[0].children[3].value;
                    // var ddl = $(savedRow.children[1].innerHTML);
                    var ddl = $(savedRow.children[1]).find('.multiselect');

                    if (list != null) {
                        var workgroupList = list.split(',');
                        var checkBoxList = $(ddl.multiselect("widget")).find(':checkbox');
                        for (var j = 0; j < checkBoxList.length; j++) {
                            var wrongCompanyIds = JSLINQ(workgroupList).Where(function (contracts) { return contracts == checkBoxList[j].value });
                            if (wrongCompanyIds.items.length != 0) {
                                checkBoxList[j].checked = true;
                            }
                        }
                    }
                }
                else {
                    $('.multiselect').multiselect("checkAll");

                    if (id == 7 && initialLoad == true) {
                        savedRow.children[0].children[0].checked = false;
                        $(savedRow.children[0].childNodes[0]).prop('disabled', true);
                        $(savedRow.children[0].childNodes[1]).prop('disabled', true);
                        $(savedRow.children[1]).hide();
                    }
                }
            }
            else {
                $(savedRow.children[1]).hide();
                if (id == 6) {
                    if (savedRow.nextSibling.nodeType == 3) {
                        if (savedRow.nextElementSibling.children[0].children[0].checked) {
                            $(savedRow.children[0].childNodes[1]).prop('disabled', true);
                            $(savedRow.children[0].childNodes[2]).prop('disabled', true);
                        }
                    } else {
                        if (savedRow.nextSibling.children[0].children[0].checked) {
                            $(savedRow.children[0]).prop('disabled', true);
                        }
                    }
                }

                if (id == 7) {
                    if (savedRow.previousSibling.nodeType == 3) {
                        if (savedRow.previousElementSibling.children[0].children[0].checked) {
                            $(savedRow.children[0].childNodes[1]).prop('disabled', true);
                            $(savedRow.children[0].childNodes[2]).prop('disabled', true);
                        }
                    } else {
                        if (savedRow.previousSibling.children[0].children[0].checked) {
                            $(savedRow.children[0]).prop('disabled', true);
                        }
                    }
                }
            }
        }
        else {
            $('.multiselect').multiselect("checkAll");
            $(savedRow.children[1]).hide();
            if (id == 7 && (initialLoad == true || isDefaultImage == false)) {
                //    savedRow.children[0].children[0].checked = false;
                //    $(savedRow.children[0].children[0]).prop('disabled', true);
                savedRow.children[0].children[0].checked = false;
                $(savedRow.children[0].childNodes[0]).prop('disabled', true);
                $(savedRow.children[0].childNodes[1]).prop('disabled', true);
            }
        }
    });
    var previousObject = null;

    $('.dynamicclass2').click(function () {
        var i = 0;
        var selectedRow = $(this.parentNode.parentNode);
        var prefId = selectedRow.find('#hdnPrefID').val();
        var prefDesc = selectedRow.find('#hdnPrefDesc').val();
        var preferenceId = selectedRow.find('#hdnPrefID').val();
        var selectedMultiSelect = selectedRow.find('.multiselect');
        if (selectedMultiSelect != null) {
            selectedMultiSelect.multiselect("checkAll");
        }
        var isDefaultImage = selectedRow[0].children[0].children[0].checked;
        var id = selectedRow[0].children[0].children[1].value;
        if (isDefaultImage == true) {
            $(selectedRow[0].children[1]).show();
            $(selectedRow[0].children[0].children[0]).attr('disabled', false);
            if (id == 6) {
                if (selectedRow[0].nextSibling.nodeType == 3) {
                    selectedRow[0].nextElementSibling.children[0].children[0].checked = false;
                    $(selectedRow[0].nextElementSibling.children[0].childNodes[1]).prop('disabled', true);
                    $(selectedRow[0].nextElementSibling.children[0].childNodes[2]).prop('disabled', true);
                    $(selectedRow[0].nextElementSibling.children[1]).hide();
                }
                else {
                    selectedRow[0].nextSibling.children[0].children[0].checked = false;
                    $(selectedRow[0].nextSibling.children[0]).attr('disabled', true);
                    $(selectedRow[0].nextSibling.children[1]).hide();
                }
            }
            if (id == 7) {
                if (selectedRow[0].previousSibling.nodeType == 3) {
                    selectedRow[0].previousElementSibling.children[0].children[0].checked = false;
                    $(selectedRow[0].previousElementSibling.children[0].childNodes[1]).prop('disabled', true);
                    $(selectedRow[0].previousElementSibling.children[0].childNodes[2]).prop('disabled', true);
                    $(selectedRow[0].previousElementSibling.children[1]).hide();
                }
                else {
                    selectedRow[0].previousSibling.children[0].children[0].checked = false;
                    $(selectedRow[0].previousSibling.children[0]).attr('disabled', true);
                    $(selectedRow[0].previousSibling.children[1]).hide();
                }
            }
        }
        else {
            if (id == 6) {
                if (selectedRow[0].nextSibling.nodeType == 3) {
                    $(selectedRow[0].nextElementSibling.children[0].childNodes[1]).prop('disabled', false);
                    $(selectedRow[0].nextElementSibling.children[0].childNodes[2]).prop('disabled', false);
                } else {
                    $(selectedRow[0].nextSibling.children[0]).prop('disabled', false);
                    $(selectedRow[0].nextSibling.childNodes[0].children[0]).prop('disabled', false);
                }
            }
            if (id == 7) {
                if (selectedRow[0].previousSibling.nodeType == 3) {
                    $(selectedRow[0].previousElementSibling.children[0].childNodes[1]).prop('disabled', false);
                    $(selectedRow[0].previousElementSibling.children[0].childNodes[2]).prop('disabled', false);
                } else {
                    $(selectedRow[0].previousSibling.childNodes[0]).attr('disabled', false);
                }
            }
            $(selectedRow[0].children[1]).hide();
        }
        if (workGroupCount <= 1) {
            $(selectedRow[0].children[1]).hide();
        }
        CheckSelectedChkbox();
    });

    $('.dynamicclass1').click(function () {
        CheckSelectedChkbox();
    });

    $('#Cancelbtn').click(function () {
        window.location.href = "/GCS/workgroup/";
    });
    $('#savebtn').click(function () {
        var savedRow = "";
        var savedUserInfo = {};
        var userPreferencesObject = [];
        if ($('#rcvFnlEmlTxt')[0].children[0].checked == true) {
            var id = "";
            id = ConsolidatedEmailPref;
            savedUserInfo = { 'PreferenceID': id };
            userPreferencesObject.push(savedUserInfo);
        }

        if ($('#reviewer').length > 0) {
            if ($('#reviewer')[0].checked == true) {
                var id = "";
                id = reviewerId;
                savedUserInfo = { 'PreferenceID': id };
                userPreferencesObject.push(savedUserInfo);
            }
        }

        if ($('#requestor').length > 0) {
            if ($('#requestor')[0].checked == true) {
                var id = "";
                id = requestorId;
                savedUserInfo = { 'PreferenceID': id };
                userPreferencesObject.push(savedUserInfo);
            }
        }
        //        else {
        //            var id = "";
        //            if (chkReviewer == "True") {
        //                id = reviewerId;
        //            } else {
        //                id = requestorId;
        //            }
        //            // id = 10;
        //            savedUserInfo = { 'PreferenceID': id };
        //            userPreferencesObject.push(savedUserInfo);
        //        }
        if (chkRequestor == "True") {
            if ($('#ddlRequestngCompany').val() != null || $('#ddlRequestngCompany').val() != "") {
                var id = "";
                id = companyId;
                if ($('#ddlRequestngCompany').val() != "") {
                    savedUserInfo = { 'PreferenceID': id, 'WorkGroupValues': $('#ddlRequestngCompany').val() };
                    userPreferencesObject.push(savedUserInfo);
                }
            }

            if ($('#ddlCurrList').length > 0) {
                var id = "";
                id = currencyId;
                var selectedText = "";
                for (var i = 0; i < $('#ddlCurrList')[0].length; i++) {
                    if ($('#ddlCurrList')[0][i].selected == true) {
                        selectedText = $('#ddlCurrList')[0][i].innerHTML;
                    }
                }

                if (selectedText != "" && selectedText != "--Select Role--") {
                    savedUserInfo = { 'PreferenceID': id, 'WorkGroupValues': selectedText };
                    userPreferencesObject.push(savedUserInfo);
                }
            }
        }

        $.each($('#userDDL1').find(".currency").children(), function (index, savedRow) {
            var isDefaultImage = savedRow.children[0].checked;
            if (isDefaultImage == true) {
                var id = "";
                id = savedRow.children[1].value;
                savedUserInfo = { 'PreferenceID': id };
                userPreferencesObject.push(savedUserInfo);
            }
        });

        $.each($('#userDDL').find(".currency").children(), function (index, savedRow) {
            var isDefaultImage = savedRow.children[0].children[0].checked;
            if (isDefaultImage == true) {
                var id = "";
                var text = "";
                var rstautPrjctCompl = [];
                var workGroupValues = "";
                id = savedRow.children[0].children[1].value;
                if ($(savedRow.children[1].innerHTML) != null) {
                    //var ddl = $(savedRow.children[1].innerHTML);
                    var ddl = $(savedRow.children[1]).find('.multiselect');
                    var result1 = ddl.find('input').filter(':checked');
                    var result = ddl.multiselect("getChecked");
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].value != "") {
                            workGroupValues += result[i].value + ',';
                        }
                    }
                    if (workGroupValues == "") {
                        $('#errorMessage').show();
                        $('#errorMessage')[0].innerHTML = userPreferenceWarningMsg;
                        hasError = true;
                        return false;
                    }
                    else {
                        $('#errorMessage').hide();
                        $('#errorMessage')[0].innerHTML = "";
                        hasError = false;
                    }
                }
                if (prevPreferenceId == 6 && id == 7) {
                    $('#errorMessage')[0].innerHTML = "";
                    savedRow.childNodes[1].childNodes[0].checked = false;
                    $(savedRow.childNodes[1].childNodes[0]).attr('disabled', 'disabled');
                    //                    $('#errorMessage').show();
                    //                    $('#errorMessage')[0].innerHTML = "Artist Consent Folder has been ignored as it already includes in Inbox cancellation";
                }
                else {
                    savedUserInfo = { 'PreferenceID': id, 'WorkGroupValues': workGroupValues };
                    userPreferencesObject.push(savedUserInfo);
                    prevPreferenceId = id;
                }
            }
        });

        $('#hdnJsonUserPref').val(JSON.stringify(userPreferencesObject));
        var loadingPanel = $($.find('#loadingDv'));
        var digitalVal = $('#UserPreferences').serialize();
        if (hasError == false) {
            loadingPanel.show();
            $.ajax({
                type: "POST",
                url: '/GCS/WorkGroup/UserPreferences',
                data: digitalVal,
                success: function (data) {
                    loadingPanel.hide();
                    $('#divMessage').show();
                    $('#divMessage')[0].innerHTML = userPreferenceSuccessMsg;
                }
            });
        }
    });
});

function CheckSelectedChkbox() {
    var isChecked = false;
    $("#userDDL1 .currency [type='checkbox']:checked").each(function () {
        isChecked = true;
    });
    $("#userDDL .currency .userPreferenceLi [type='checkbox']:checked").each(function () {
        isChecked = true;
    });
    if (!isChecked) {
        var ConsolidatedEmailCheckbox = $('#rcvFnlEmlTxt').find(':checkbox');
        ConsolidatedEmailCheckbox[0].checked = false;
        $('#rcvFnlEmlTxt').hide();
    }
    else {
        $('#rcvFnlEmlTxt').show();
    }
}