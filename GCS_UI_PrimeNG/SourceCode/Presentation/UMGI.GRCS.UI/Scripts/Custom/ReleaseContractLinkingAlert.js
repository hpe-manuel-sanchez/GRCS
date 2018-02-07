$(document).ready(function () {
    $('#btnLinkedReleaseOk').click(function () {
        $('#relePopup').dialog('close');
        if ($('#hiddenReleaseCount').val() != $('#hiddenLinkedRepertoireCount').val() && $('#sourcePage').val() != 'Resource') {
            $.post('/GCS/Release/LinkReleasesWithResources', function (data) {
                PropagateLinking(data);
            });
        }

        if (($('#hiddenReleaseCount').val() != $('#hiddenLinkedRepertoireCount').val()) && $('#sourcePage').val() == 'Resource') {
            alert('The system will now link the repertoire to the contract in the background.Press OK to continue');
            window.location.href = '/GCS/Contract/SearchContract';
        }
    });

    $('#artistNameRelAlert').html($('#loadArtistName').val());
    $('#contractingPartyRelAlert').html($('#loadContractingParty').val());
    $('#clearanceAdminRelAlert').html($('#loadClearanceCompanyName').val());
});

function PropagateLinking(content) {
    var proplinkingRel = $('<div id="repPopupLinking"></div>')
                      .html('<p>' + messageCommon.onLoading + '</p>')
                      .dialog({
                          autoOpen: false,
                          modal: true,
                          title: messageCommon.infoTitle,
                          show: 'clip',
                          hide: 'clip',
                          width: "98%",
                          position: [(($(window).width() - ($(window).width() * 98) / 100) / 2), 25],
                          close: function () {
                              $(this).remove(); // ensures any form variables are reset.
                          }
                      });

    //  Load partial view

    proplinkingRel.html(content);
    proplinkingRel.dialog('open');
    return false;
}