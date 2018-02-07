/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var AddressBookMessages = {
    EmailValidation: "Please Enter Email ID",
    EmailCorrectValidation: "Please Enter Valid Email ID",
    CountryNameValidation: "Please Enter Country",
    GroupNameValidation: "Please Enter Role/Group Name",
    SaveAddress: "Role/Group Successfully saved",
    addressAlreadyExist: 'This Role/Group Name already exists in the system. Please provide a different name to continue',
    allFeildsValidation: " Please Enter Mandatory Fields",
    fail: "Please provide Valid Details"
};

//Create dialog
var objDialog = $('<div id="Dummy"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.infoTitle,
            show: 'clip',
            hide: 'clip',
            width: 500,
            position: [(($(document).width() - 500) / 2), 50]
        });
var validateEmail = true;

$('#EmailGroupDetails').keyup(function () {
    var emailAdd = ($('#EmailGroupDetails').val());
    emailAdd = emailAdd.replace(/\s/g, '');
    $('#EmailGroupDetails').text(emailAdd);
    var result = emailAdd.split(";");
    for (var index = 0; index < result.length; index++) {
        if (result[index] == '')
            return true;

        if (ValidateEmail(result[index])) {
            $(this).css('background-color', 'white');
            validateEmail = true;
        }
        else {
            $(this).css('background-color', 'pink');
            validateEmail = false;
        }
    }
    return false;
});

function ValidateEmail(emailAdd) {
    var filter = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,3}(;|$)/;
    if (filter.test(emailAdd)) {
        return true;
    }
    else {
        return false;
    }
}


$('#SaveEmailGroup').click(function () {
   
    var addressCountryName = $('#countryName').val();
    var addressEmailId = $('#EmailGroupDetails').val();
    var roleGroupId = $('#roleGroupId').val();
    var groupName = $('#roleGroupName').val();
    var countryId = $('#countryId').val();
    if ((groupName == "" || groupName == null) && (addressCountryName == "" || addressCountryName == null) && (addressEmailId == "" || addressEmailId == null)) {
        MandatoryValidateForName();
        MandatoryValidateForCountry();
        MandatoryValidateForEmailIds();
        objDialog.html(AddressBookMessages.allFeildsValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
        objDialog.dialog('open');
//        $('#validEmail').hide();
//        $('#valid').hide();
//        $('#validCountry').hide();
//        $('#emailValidation').empty();
//        $('#emailValidation').append(AddressBookMessages.allFeildsValidation);
//        $('#validEmail').show();
//        $('#emailValidation').show();
        return false;
    }
    
    
    if (groupName == "" || groupName == null) {
        MandatoryValidateForName();
        objDialog.html(AddressBookMessages.GroupNameValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
                objDialog.dialog('open');
//        $('#nameValidation').empty();
//        $('#nameValidation').append(AddressBookMessages.GroupNameValidation);
//        $('#valid').show();
//        $('#nameValidation').show();
        return false;
    }
    if (addressCountryName == "" || addressCountryName == null) {
        MandatoryValidateForCountry();
        objDialog.html(AddressBookMessages.CountryNameValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
                objDialog.dialog('open');
//        $('#countryValidation').empty();
//        $('#countryValidation').append(AddressBookMessages.CountryNameValidation);
//        $('#validCountry').show();
//        $('#countryValidation').show();
        return false;
    }
    if (addressEmailId == "" || addressEmailId == null) {
        MandatoryValidateForEmailIds();
        objDialog.html(AddressBookMessages.EmailValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
                objDialog.dialog('open');
//        $('#emailValidation').empty();
//        $('#emailValidation').append(AddressBookMessages.EmailValidation);
//        $('#validEmail').show();
//        $('#emailValidation').show();
        return false;
    }

    var result = validateEmail;

    if (result == false) {
        objDialog.html(AddressBookMessages.EmailCorrectValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
        objDialog.dialog('open');
//        $('#emailValidation').empty();
//        $('#emailValidation').append(AddressBookMessages.EmailCorrectValidation);
//        $('#validEmail').show();
//        $('#emailValidation').show();

        return false;
    };

    result = MandatoryValidateForName();

    if (result == false) {
        objDialog.html(AddressBookMessages.GroupNameValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
                objDialog.dialog('open');
//        $('#nameValidation').empty();
//        $('#nameValidation').append(AddressBookMessages.GroupNameValidation);
//        $('#valid').show();
//        $('#nameValidation').show();
        return false;
    };

    result = MandatoryValidateForCountry();

    if (result == false) {
        objDialog.html(AddressBookMessages.CountryNameValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
                objDialog.dialog('open');
//        $('#countryValidation').empty();
//        $('#countryValidation').append(AddressBookMessages.CountryNameValidation);
//        $('#validCountry').show();
//        $('#countryValidation').show();
        return false;
    };

    result = MandatoryValidateForEmailIds();

    if (result == false) {
        objDialog.html(AddressBookMessages.EmailValidation);
        objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
        //Open Dialog
                objDialog.dialog('open');
//        $('#emailValidation').empty();
//        $('#emailValidation').append(AddressBookMessages.EmailValidation);
//        $('#validEmail').show();
//        $('#emailValidation').show();
        return false;
    };


    $.post('/GCS/Contract/UpdateRoleGroup/', {
        countryName: addressCountryName,
        groupName: groupName,
        addressEmailIds: addressEmailId,
        roleGroupId: roleGroupId,
        countryId: countryId,
        isUpdate: 1
    }, function (data) {
        if ($(data).find("#displayMessage").val() == "success") {
 //            objDialog.html(AddressBookMessages.SaveAddress)
 //            objDialog.dialog('open');
            //            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); $('#jqgrid').jtable('load'); } } });
            $('#jqgrid').jtable('load');
            ShowSuccess(AddressBookMessages.SaveAddress);
            $('#Address').dialog('close');
        }
        else if ($(data).find("#displayMessage").val() == "Fail") {
            objDialog.html(AddressBookMessages.fail);
            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); $('#jqgrid').jtable('load'); } } });
            objDialog.dialog('open');
            $('#Address').dialog('close');
        }
        else if ($(data).find("#displayMessage").val() == "Exists") {
            objDialog.html(AddressBookMessages.addressAlreadyExist);
            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
                        objDialog.dialog('open');
//            $('#nameValidation').empty();
//            $('#nameValidation').append(AddressBookMessages.addressAlreadyExist);
//            $('#valid').show();
//            $('#nameValidation').show();
        }


    }
        );

});
var addressbookMessages = {
    cancelValid: 'Are you sure, you want to cancel?',
    confirm: "confirm"
};

$('#CancelGroup').click(function (e) {
    e.preventDefault();
    $("#Address").dialog('close');
    
});



function MandatoryValidateForEmailIds() {

    if ($('#EmailGroupDetails').val() == null || $('#EmailGroupDetails').val() == '') {
        $('#EmailGroupDetails').addClass('input-validation-error');
//        $('#emailValidation').empty();
//        $('#emailValidation').append(AddressBookMessages.EmailValidation);
//        $('#validEmail').show();
//        $('#emailValidation').show();
        return false;
    }
    else {
//        $('#validEmail').hide();
        $('#EmailGroupDetails').removeClass('input-validation-error');
    }

    return true;
}
function MandatoryValidateForCountry() {

    if ($('#countryName').val() == null || $('#countryName').val() == '') {
        $('#countryName').addClass('input-validation-error');
//        $('#countryValidation').empty();
//        $('#countryValidation').append(AddressBookMessages.CountryNameValidation);
//        $('#validCountry').show();
//        $('#countryValidation').show();
        return false;
    }
    else {
//        $('#validCountry').hide();
        $('#countryName').removeClass('input-validation-error');
    }

    return true;
}
function MandatoryValidateForName() {

    if ($('#roleGroupName').val() == null || $('#roleGroupName').val() == '') {
        $('#roleGroupName').addClass('input-validation-error');
//        $('#nameValidation').empty();
//        $('#nameValidation').append(AddressBookMessages.GroupNameValidation);
//        $('#valid').show();
//        $('#nameValidation').show();
        return false;
    }
    else {
//        $('#valid').hide();
        $('#roleGroupName').removeClass('input-validation-error');
    }

    return true;
}

$('#roleGroupName').blur(function () {
    MandatoryValidateForName();
});
$('#countryName').blur(function () {
    MandatoryValidateForCountry();
});
$('#EmailGroupDetails').blur(function () {
    MandatoryValidateForEmailIds();
});

$(document).ready(function () {
    
    //AutoComplete Country
    var target1 = $("#countryName");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#countryName").val(ui.item.value);
            $("#countryId").val(ui.item.id);
        }, change: function (event, ui) {
            if (ui.item == null) {
                $("#countryName").val("");
            }
            else if (ui.item != null && $("#countryName").val() != ui.item.value) {
                $("#countryName").val("");
            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#countryName").val(ui.item.value);
                $("#countryId").val(ui.item.id);
            }


        }
    });

    $("#addRoleGroup").scroll(function () { $(".ui-autocomplete").hide(); });

});




   


   


    

       
