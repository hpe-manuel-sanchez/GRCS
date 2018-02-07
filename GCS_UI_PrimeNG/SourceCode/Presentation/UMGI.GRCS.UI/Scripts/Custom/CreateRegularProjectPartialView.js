var msg_ErrorFetchingDataFromServer = "Error fetching data from server.";
$(document).ready(function () {

    $('#lblprojectreferencenumber').text($('#txtProjectReferenceId').val());

    // Start**** Set release dropdown value set

    if ($("#FlagReleaseNewOrExisting").val() == "Exist") {
        if ($('#hdnStatusType').val() == 1) {
            $(function () { $("#tabs").tabs({ disabled: [3] }) });
        }
    }
    else {
        if ($('#hdnStatusType').val() == 1) {
            $(function () { $("#tabs").tabs({ disabled: [4] }) });
        }
    }

    if ($("#FlagReleaseNewOrExisting").val() == "Exist") {
        $('#cmbReleaseNewOrExisting option[value=2]').prop("selected", true);
    }
    else {
        $('#cmbReleaseNewOrExisting option[value=1]').prop("selected", true);
        $("#FlagReleaseNewOrExisting").val('New');
    }

    // End**** Set release dropdown value set

    //Start**** diasble release dropdown when not on request type tab
    var index = $("#tabs").tabs('option', 'selected');
   
    if (index != 1) {
        $('#cmbReleaseNewOrExisting').attr('disabled', true);
    }
    else {
        if ($("#FlagReleaseNewOrExisting").val() == "Exist") {
            if ($('#txtProjectReferenceId').val() != "") {
                if ($('#Archive0').val() != null) {
                    if ($('#Archive0').val() != 'Y') {
                        $('#cmbReleaseNewOrExisting').attr('disabled', true);
                    }
                    else {
                        $('#cmbReleaseNewOrExisting').attr('disabled', false);
                    }
                }
            }
        }
        else {
            if ($('#txtProjectReferenceId').val() != "") {
                if ($('#hdnReleaseId0').val() != 0) {
                    $('#cmbReleaseNewOrExisting').attr('disabled', true);
                }
            }

        }

    }
    //End***** diasble release dropdown when not on request type tab

});

function generateEmail(emailType) {
    var projectId = $("#Clr_Project_Id").val();
    if (projectId != 0) {
        var values =
                {
                    //"roleGroup": _activeRoleGroup,
                    "emailType": emailType,
                    "clrProjectId": projectId
                }
                $.ajax({
                    url: '/GCS/ClearanceInbox/GenerateEmail',
                    type: 'POST',
                    async: false,
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(values),
                    success: function (data) {
                        
                        if (data != "") {
                            $("#divErrorMessage").html(data).show();
                            $("#divMessage").hide();
                            $('#divErrorMessage').addClass('warning');
                        }
                        else {
                            $("#divErrorMessage").html('Mail has been triggered.').show();
                            $('#divErrorMessage').addClass('success');
                            $("#divErrorMessage").show();
                        }
                    }
                   
                });
    }
}

function displayMessage(truefalse, messageType, messageText, contentType) {
    var messageElement = $('#divMessage');

    messageElement.text('')
                  .html('')
                  .removeClass()
                  .hide();

    if (truefalse) {

        messageElement.text(messageText);

        messageElement.addClass(messageType)

        messageElement.show();
    }

    applyCustomTheam();

}

function displayLoadingPanel(truefalse) {

    var loadingPanel = $($.find('#loadingDivPA'));

    if (truefalse) {

        displayMessage(false);

        loadingPanel.show();

    }
    else {

        loadingPanel.hide();

    }

}

function applyCustomTheam() {

    var docHeight = $(window).height();
    var headerHeight = 60;
    var footerHeight = 45;
    var errMessageHeight = $("#divMessage").height() + 20;

    var mainContainerHeight = docHeight - headerHeight - footerHeight;
    $(".mainContainer").css("height", mainContainerHeight + "px");
    $("#wrapper").css("height", mainContainerHeight - errMessageHeight - 89 + "px");
    $("#wrapper").css("position", "relative");

}