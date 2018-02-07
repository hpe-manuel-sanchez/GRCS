//Function to Select Header Checkbox while selecting child checkbox for Cancelled Resource
function SelectHeaderCancelResource() {
    var count = $('#tblCancelResources').find('input:checked').length;
    if ($('#chkCancelResource')[0].checked == true) count = count - 1;
    var tableRows = $('#tblCancelResources').find('input:checkbox').length - 1;

    if (count == tableRows) {
        $('#chkCancelResource')[0].checked = true;
    }
    else {
        $('#chkCancelResource')[0].checked = false;
    }
}

//Function to Select Header Checkbox while selecting child checkbox for Reject Resource
function SelectHeaderRejectResource() {
    var count = $('#tblRejectResources').find('input:checked').length;
    if ($('#chkRejectResource')[0].checked == true) count = count - 1;
    var tableRows = $('#tblRejectResources').find('input:checkbox').length - 1;

    if (count == tableRows) {
        $('#chkRejectResource')[0].checked = true;
    }
    else {
        $('#chkRejectResource')[0].checked = false;
    }
}

//Function to Select childs Checkboxes while selecting header checkbox for Cancel Resource
function SelectChildCancelResource() {
    var table = $('#tblCancelResources');
    if ($('#chkCancelResource')[0].checked == true) {
        $('input:checkbox', table).prop('checked', true);
    }
    else {
        $('input:checkbox', table).prop('checked', false);
    }
}

//Function to Select childs Checkboxes while selecting header checkbox for Rejected Resource
function SelectChildRejectResource() {
    var table = $('#tblRejectResources');
    if ($('#chkRejectResource')[0].checked == true) {
        $('input:checkbox', table).prop('checked', true);
    }
    else {
        $('input:checkbox', table).prop('checked', false);
    }
}