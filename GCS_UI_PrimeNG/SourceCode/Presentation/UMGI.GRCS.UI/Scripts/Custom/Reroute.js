/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
$(document).ready(function () {
    //clearanceAdmin Company Auto search
    var target = $("#txtRerouteClearanceComp");
    target.autocomplete({
        source: target.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#btnRerouteClearanceComp").val(ui.item.id);
            $("#hdnRerouteClearanceCompId").val(ui.item.addValue);
        }
    });

    //Re-Route Resource click
    $('#btnRerouteOk').click(function (e) {
        e.preventDefault();
        radioButtonValue = getRadioValue();
        var $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        var isRccInbox = 1;
        var companyId = $('#hdnRerouteClearanceCompId').val();
        var workQueueIds = '';

        $selectedRows.each(function () {
            var record = $(this).data('record');
            workQueueIds = workQueueIds + ',' + record.TaskId;
        });

        if (radioButtonValue == "Rcc User") {
            $.get('/GCS/WorkQueue/RerouteResource/', { workQueueIds: workQueueIds, clearanceAdminComp: companyId, isRccInbox: isRccInbox });
            //TODO
            alert("Rcc");
        } else {
            $.get('/GCS/WorkQueue/RerouteResource/', { workQueueIds: workQueueIds, clearanceAdminComp: companyId });
        }
        $('#ReRouteResources').dialog('close');
        $('#workQueueGrid').jtable('load', {
            artistName: $('#WorkQueues_ArtistName').val(),
            contractDesc: $('#WorkQueues_ContractDescription').val(),
            descTitle: $('#WorkQueues_Title').val(),
            reasonForReview: $('#WorkQueues_ContractReviewReason').val(),
            adminCompany: $('#WorkQueues_CompanyName').val()
        });
    });
});
var radioButtonValue = '';
$(document).ready(function () {
    $('input[name=ReRouteTo]:radio').click(function () {
        radioButtonValue = getRadioValue();
        if (radioButtonValue == "Rcc User") {
            $("#txtRerouteClearanceComp").attr('disabled', true);
            $("#txtRerouteClearanceComp").addClass('greyColor'); //greyColor
        } else {
            $("#txtRerouteClearanceComp").attr('disabled', false);
            $("#txtRerouteClearanceComp").removeClass('greyColor');
        }
    });
});
function getRadioValue() {
    if ($('input[name=ReRouteTo]:radio:checked').length > 0) {
        return $('input[name=ReRouteTo]:radio:checked').val();
    }
    else {
        return 0;
    }
}