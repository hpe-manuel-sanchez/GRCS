$('#btnCreate').click(function (e) {
    if ($("#hdnAdminCompany").val() == "") {
        $("#divContractErrorMessage").text("Please select Admin Company");
        $('#divContractErrorMessage').addClass('warning');
        $("#divContractErrorMessage").show();
        return false;
    }

    if ($("#hdnArtistId").text() == "") {
        $("#divContractErrorMessage").text("Please select Artist");
        $('#divContractErrorMessage').addClass('warning');
        $("#divContractErrorMessage").show();
        return false;
    }

    $("#divContractErrorMessage").text("");
    $('#divContractErrorMessage').remove('warning');
    $("#divContractErrorMessage").hide();

    var artistInfo = $("#hdnArtistId").text();

    postData = {
        artistId: artistInfo.split(':')[1].replace('=', ''),
        artistName: artistInfo.split(':')[0].replace('=', ''),
        clearanceCompanyId: $("#hdnAdminCompany").val(),
        notes: $("#txtContractNotes").val(),
        talentId: artistInfo.split(':')[2].replace('=', '')
    };

    $('#createContract').remove();
    $('#createContract').dialog('close');
    $('#createContract').empty();

    $.ajax({
        url: '/GCS/ClearanceInbox/CreateInboxClearanceContract',
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: 'json',
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.Result == 'OK') {
                if (data.Records.ContractId != "") {
                    linkContractWithExistingContract(data.Records.ContractId);
                }
            }
        }
    });
});

function linkContractWithExistingContract(contractId) {
    postData = {
        artistName: null,
        contractingParty: null,
        umgSigningCompanyId: null,
        artistNameInLocalCharacters: null,
        contractStatus: null,
        localContractRefNumber: null,
        rightsType: null,
        clearanceAdminCompany: null,
        contractId: contractId,
        artistId: null,
        jtPageSize: 1
    };

    $.ajax({
        url: '/GCS/ClearanceInbox/ClearanceSearchContractWithParam',
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: 'json',
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.Result == 'OK') {
                var contractIdArray = new Array();
                var artistIdArray = new Array();
                var i = 0;
                $('.chkClass').each(function () {
                    var id = $(this).attr('id');
                    var tempId = id.replace('chk', '');
                    contractIdArray[i] = tempId;
                    artistIdArray[i] = $(this).val();

                    i++;
                });
                //artistIdArray = artistIdArray.distinct();

                var index = artistIdArray.indexOf(data.Records[0].ArtistId);
                if (index >= 0) {
                    appendRowInTable(data.Records[0], data.Records[0].ArtistId);
                }
                else {
                    CreateMainTable(data.Records[0]);
                }
                // CreateMainTable(data.Records[0]);
            }
        }
    });
}

$("#btnSearchCompany").click(function () {
    var objDialog = $('<div id="manageCompanyContainer" style="margin:0;padding:0;"></div>');
    objDialog.dialog({
        resizable: false,
        autoOpen: true,
        width: 1000,
        title: 'Manage Company',

        modal: true,
        close: function () {
            $(this).remove(); // ensures any form variables are reset.
            $(this).dialog('close');
            $('#loadingDivPA').hide();
        },
        open: function () {
            $(this).load('/GCS/ClearanceInbox/ClearanceManageCompany');
        }
    });
    var dialogue = $('.ui-dialog');

    dialogue.animate({
        top: "40px"
    }, 0);
});

$("#btnSearchArtist").click(function () {
    var objDialog = $('<div id="ArtistResourcePopup" style="margin:0;padding:0;"></div>');
    objDialog.dialog({
        resizable: false,
        autoOpen: true,
        width: 1000,
        title: 'Manage Artist',

        modal: true,
        close: function () {
            $(this).remove(); // ensures any form variables are reset.
            $(this).dialog('close');
            $('#loadingDivPA').hide();
        },
        open: function () {
            $(this).load('/GCS/Artist/SearchForArtist');
        }
    });
    var dialogue = $('.ui-dialog');

    dialogue.animate({
        top: "40px"
    }, 0);
});

$("#btnCancelContract").click(function () {
    $('#createContract').remove();
    $('#createContract').dialog('close');
});

Array.prototype.distinct = function () {
    var derivedArray = [];
    for (var i = 0; i < this.length; i += 1) {
        if (!derivedArray.contains(this[i])) {
            derivedArray.push(this[i])
        }
    }
    return derivedArray;
};