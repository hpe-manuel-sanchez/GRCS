
//button submit click event
$(function () {
    $('#btnSubmit').click(function (e) {
        
        var workgroupIndex = $('#WorkgroupUser')[0].selectedIndex;
        if (workgroupIndex == 0) {
            $("#showwaringingmsg").text(mandatoryselectUser);
            $('#showwaringingmsg').addClass('warning');
            $('#WorkgroupUser').addClass('btn1-validation-error');
            $('#WorkgroupUser').show();
            $("#showwaringingmsg").show();
            // $('#WorkgroupUser').addClass('input-validation-error');
            return false;
        }
        else {
            //code to pass the value

            $('#loadingDivPA').show();
            var selectedProjectValue = '';
            var $selectedRows = $('#searchedProjectList').jtable('selectedRows');

            $('#SelectedRowList').empty();
            if ($selectedRows.length > 0) {
                // LoadAddedSearchJtable('Add');
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    selectedProjectValue = selectedProjectValue + record.ClrProjectId;
                    selectedProjectValue = selectedProjectValue + ",";

                });
            }


            $("#showwaringingmsg").text("");
            $("#showwaringingmsg").hide();
            $('#WorkgroupUser').removeClass('input-validation-error');
            var workgroupUserId = $('#WorkgroupUser')[0][workgroupIndex].value;
            var value =
                                        {
                                            "Isrc": selectedProjectValue,
                                            "ProjectId": workgroupUserId
                                        };

            $.ajax({
                url: '/GCS/ClearanceProject/TransferUser',
                type: 'POST',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(value),
                success: function (data) {
                    $('#hdnShowSuccessMessage').val("yes");
                    ShowSuccessMessageAfterTransferUser();
                    $('#divUserTransfer').dialog('close');
                    
                    $('#btnSearchProjectUserTransfer').click();
                    $('#loadingDivPA').hide();
                }
            });


           

        }



    });
});

////button cancel click event
$(function () {
    $('#btnCancel').click(function (e) {
        $("#tblProjectsToUserTransfer").hide();
        $("#tblProjectsToUserTransfer").empty();
        $('#WorkgroupUser').removeClass('input-validation-error');
        $("#showwaringingmsg").text("");
        $("#showwaringingmsg").hide();
        $("#divShowUser").hide();
        $('#WorkgroupUser').removeClass('input-validation-error');
        $('#divUserTransfer').dialog('close');

    });

});




//Set the default button User Transfer Popup
$('body').keydown(function (e) {
    if (e.keyCode == 13) {
        $("#btnSubmit").trigger('click');
    }
});



function ShowSuccessMessageAfterTransferUser() {
    
    $("#divUserTransferSuccessMessage").text("Projects transferred successfully");
    $('#divUserTransferSuccessMessage').addClass('success');
    $("#divUserTransferSuccessMessage").show();
    return true;
}

