$(document).ready(function () {
    var undoText = "";
    var undoNum = "";
    var isMarktng = false;
    var isLegal = false;
    var isUMGI = false;
    var isEdit = false;
    var confirmLoss = false;
    var editCurrent = false;
    var prvsUndoObject = null;

    $('#btnAddReasn').click(function () {
        var isValid = false;

        if ($('#chkMarktng').is(':checked') == true) {
        }
        else {
            $('#chkMarktng').value = false;
        }
        if ($('#chkLegal').is(':checked') == true) {
        }
        else {
            $('#chkLegal').value = false;
        }
        if ($('#chkUMGI').is(':checked') == true) {
        }
        else {
            $('#chkUMGI').value = false;
        }
        $('#errorMessage').hide();
        isValid = validateNewReason();

        if (isValid == true) {
            $.ajax({
                url: '/GCS/ClearanceInbox/ManageRejectReason',
                type: "POST",
                dataType: "json",
                data: { sequenceNum: $('#txtReasnNum').val(), comments: $('#txtAreaReason').val(), isMarktng: $('#chkMarktng')[0].checked, isLegal: $('#chkLegal')[0].checked, isUMGI: $('#chkUMGI')[0].checked },
                success: function (data) {
                    window.location.href = "ManageRejectReason";
                }
            });
        }
    });

    $('.undobtn').click(function () {
        var $lblTxtLi = $(this.parentNode.parentNode);
        var $btnLi = $(this.parentNode.parentNode);
        var $chkBoxLi = $(this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode);
        prvsUndoObject = $lblTxtLi.find("#btnUndoReasn");
        $btnLi.find('#btnDeleteReasn').show();
        $btnLi.find('#btnEditReasn').show();
        $btnLi.find('#btnSaveReasn').hide();
        $(prvsUndoObject).hide();
        $chkBoxLi.find('#chckMarktng')[0].checked = isMarktng;
        $chkBoxLi.find('#chckLegal')[0].checked = isLegal;
        $chkBoxLi.find('#chckUMGI')[0].checked = isUMGI;
        $chkBoxLi.find('#chckMarktng').attr('disabled', true);
        $chkBoxLi.find('#chckLegal').attr('disabled', true);
        $chkBoxLi.find('#chckUMGI').attr('disabled', true);
        $lblTxtLi.find('#txtReasonNum').val(undoNum);
        $lblTxtLi.find('#txtReason').val(undoText);
        $lblTxtLi.find('#txtReasonNum').hide();
        $lblTxtLi.find('#lblReasonNum').show();
        $lblTxtLi.find('#txtReason').hide();
        $lblTxtLi.find('#lblRejectReason').show();
        $('#errorMessage').html("");
        $('#errorMessage').hide();
        $lblTxtLi.find('#txtReasonNum').removeClass('input-validation-error');
        isEdit = false;
    });

    $('.deletebtn').click(function () {
        var $chkBoxLi = $(this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode);
        var reasonId = $chkBoxLi.find('#hdnReasonId')[0].value;
        $.ajax({
            url: '/GCS/ClearanceInbox/DeleteRejectionReason',
            type: "POST",
            dataType: "json",
            data: { reasonId: reasonId },
            success: function (data) {
                window.location.href = "ManageRejectReason";
            }
        });
    });

    $('.editbtn').click(function () {
        var id = this.id;
        if (isEdit == true) {
            confirmLoss = confirm("Prior Edit details are not saved. Are you sure to loose changes?");
            if (confirmLoss == true) {
                prvsUndoObject.click();
                editCurrent = true;
            }
        }
        if (isEdit == false) {
            isEdit = true;
            var $lblTxtLi = $(this.parentNode.parentNode);
            var $btnLi = $(this.parentNode.parentNode);
            var $chkBoxLi = $(this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode);
            prvsUndoObject = $lblTxtLi.find("#btnUndoReasn");
            $btnLi.find('#btnDeleteReasn').hide();
            $btnLi.find('#btnEditReasn').hide();
            $btnLi.find('#btnSaveReasn').show();
            $(prvsUndoObject).show();
            if ($chkBoxLi.find('#chckMarktng')[0].checked == true) {
                isMarktng = true;
            }
            if ($chkBoxLi.find('#chckLegal')[0].checked == true) {
                isLegal = true;
            }
            if ($chkBoxLi.find('#chckUMGI')[0].checked == true) {
                isUMGI = true;
            }
            $chkBoxLi.find('#chckMarktng').attr('disabled', false);
            $chkBoxLi.find('#chckLegal').attr('disabled', false);
            $chkBoxLi.find('#chckUMGI').attr('disabled', false);
            $lblTxtLi.find('#txtReasonNum').show();
            undoNum = $lblTxtLi.find('#lblReasonNum')[0].innerHTML;
            undoText = $lblTxtLi.find('#lblRejectReason')[0].innerText || $lblTxtLi.find('#lblRejectReason')[0].textContent;
            $lblTxtLi.find('#lblReasonNum').hide();
            $lblTxtLi.find('#txtReason').show();
            $lblTxtLi.find('#lblRejectReason').hide();
        }
    });

    $('.savebtn').click(function () {
        id = this.id;
        var instance = this;
        var $lblTxtLi = $(this.parentNode.parentNode);
        var $btnLi = $(this.parentNode.parentNode);
        var $chkBoxLi = $(this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode);

        var sno = $lblTxtLi.find('#txtReasonNum').val();
        var reason = $lblTxtLi.find('#txtReason').val();
        var isMarktng = $chkBoxLi.find('#chckMarktng')[0].checked;
        var isLegal = $chkBoxLi.find('#chckLegal')[0].checked;
        var isUMGI = $chkBoxLi.find('#chckUMGI')[0].checked;
        var reasonId = $chkBoxLi.find('#hdnReasonId')[0].value;
        var modifiedDttm = $chkBoxLi.find('#modDateTime')[0].innerHTML;
        var valid = true;
        valid = validateExistingReason(sno, reason, isMarktng, isLegal, isUMGI);

        if (valid) {
            $.ajax({
                url: '/GCS/ClearanceInbox/UpdateManagePredefinedRejectionReason',
                type: "POST",
                dataType: "json",
                data: { reasonId: reasonId, sequenceNum: sno, comments: reason, isMarktng: isMarktng, isLegal: isLegal, isUMGI: isUMGI, modifiedDttm: modifiedDttm },
                success: function (data) {
                    window.location.href = "ManageRejectReason";
                }
            });
        }
    });

    $('#btnSaveReasn').hide();
    $('#btnUndoReasn').hide();
});

function validateExistingReason(sno, reason, isMarktng, isLegal, isUMGI) {
    var currentString = reason;
    if (reason == "" || reason == null) {
        $('#errorMessage').show();
        $('#errorMessage')[0].innerHTML = "please enter the reason before adding";
        return false;
    }
    else if (currentString.length > 255) {
        $('#errorMessage').show();
        $('#errorMessage')[0].innerHTML = "Reject Reason exceeds more than 255 characters";
        return false;
    }
    else if (sno == "" || sno == null)  // text box for sequence number
    {
        $('#errorMessage').show();
        $('#errorMessage')[0].innerHTML = "Please enter Reason Number";
        return false;
    }
    else {
        if (isMarktng == true) {
            $('#errorMessage').html("");
            $('#errorMessage').hide();
            return true;
        }
        else if (isLegal == true) {
            $('#errorMessage').html("");
            $('#errorMessage').html("");
            return true;
        }
        else if (isUMGI == true) {
            $('#errorMessage').html("");
            $('#errorMessage').html("");
            return true;
        }
        else {
            $('#errorMessage').show();
            $('#errorMessage')[0].innerHTML = "please select at least one option in applicable to column";
            return false;
        }
    }
}

function validateNewReason() {
    //$('#errorMessage')[0].outerHTML = "";
    var currentString = $('#txtAreaReason').val();
    if ($('#txtAreaReason').val() == "" || $('#txtAreaReason').val() == null) {
        $('#errorMessage').show();
        $('#errorMessage')[0].innerHTML = "please enter the reason before adding";
        return false;
    }
    else if (currentString.length > 255) {
        $('#errorMessage').show();
        $('#errorMessage')[0].innerHTML = "Reject Reason exceeds more than 255 characters";
        return false;
    }
    else if ($('#txtReasnNum').val() == "" || $('#txtReasnNum').val() == null) {
        $('#errorMessage').show();
        $('#errorMessage')[0].innerHTML = "Please enter Reason Number";
        return false;
    }
    else {
        if ($('#chkMarktng').is(':checked') == true) {
            $('#errorMessage').html("");
            $('#errorMessage').html("");
            return true;
        }
        else if ($('#chkLegal').is(':checked') == true) {
            $('#errorMessage').html("");
            $('#errorMessage').html("");
            return true;
        }
        else if ($('#chkUMGI').is(':checked') == true) {
            $('#errorMessage').html("");
            $('#errorMessage').html("");
            return true;
        }
        else {
            $('#errorMessage').show();
            $('#errorMessage')[0].innerHTML = "please select at least one option in applicable to column";
            return false;
        }
    }
}