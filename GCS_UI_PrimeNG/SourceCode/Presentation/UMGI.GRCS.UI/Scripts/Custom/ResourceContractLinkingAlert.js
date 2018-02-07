$(document).ready(function () {
    $('#resourceLinkOk').click(function () {
        $('#resourcePopup').dialog('close');
        if ($('#hiddeCounValidation').val() != $('#hiddenCount').val()) {
            alert('The system will now link the repertoire to the contract in the background.Press OK to continue');
            window.location.href = '/GCS/Contract/SearchContract';
        }
    });

    $('#artistNameResAlert').html($('#loadArtistName').val());
    $('#contractingPartyResAlert').html($('#loadContractingParty').val());
    $('#clearanceAdminResAlert').html($('#loadClearanceCompanyName').val());
});