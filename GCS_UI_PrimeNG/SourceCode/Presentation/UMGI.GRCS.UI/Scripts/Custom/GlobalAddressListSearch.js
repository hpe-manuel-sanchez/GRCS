var globalAddressMessages = {
    emailName: "Role/ Group Name",
    emailTitle: "Country",
    emailLocation: "Email IDs",
    searchValidation: "Please Enter the Fields for Search Criteria "
};

var rowIndex = -1;
var pageSize = 25;
$(function () {

    //************Delete Functionality for individual Mail group*************//
    $('#emailAddress span').live('click', function () {
        var emailAddressDivText = $(this).parent().text(); //gets all the text in the div(here semicolon included)

        //Removing the parent div selected from the email notifications area
        $(this).parent().remove();

        //For deselecting of row:is done using the Name(RoleGroup) in table
        var rows = $("#searchGlobalList tr");
        rows.each(function (index) {
            var isSelectedValueText = rows[index].children[2].innerText + ';';
            if (emailAddressDivText.indexOf(isSelectedValueText) != -1) {//emailAddressDivText contains isSelectedValueText
                $(this).find('input').removeAttr("checked");
                $(this).removeClass("jtable-row-selected");
            }
        });
    });

    //*************Rights Expiry************//
    var rightsExpiryEmailReceipientsId = ''; //RightsExpiry ParentPage Email Ids

    if ($('#RightsExpiry_EmailReceipients').val() != '' && $('#RightsExpiry_EmailReceipients').val() != undefined) {//If RightsExpiry ParentPage EmailRecepients exist bugfix29Nov
        if ($('#RightsExpiry_EmailReceipientsId').val() != '') {
            rightsExpiryEmailReceipientsId = $('#RightsExpiry_EmailReceipientsId').val();
        } else if ($('#RightsExpiry_EmailReceipientsId').val() == '' && $('#Temp_RightsExpiry_EmailReceipientsId').val() != '') {
            //On Edit Temp is the previous set of unchanged email recepients; Temp_RightsExpiry_EmailReceipientsId always != ''
            //Edit Operation : First load/email recepients unchanged this method is executed
            rightsExpiryEmailReceipientsId = $('#Temp_RightsExpiry_EmailReceipientsId').val();
        }
    }
    else {
        rightsExpiryEmailReceipientsId = '';
    }

    pageSize = pageSize;
    $('#searchGlobalList').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        defaultSorting: 'EmailAddress ASC',
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); $(".ui-jtable-loading").show(); },
        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("SearchCounts").innerHTML = "Search Results(" + rowIndex + ")";
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            $("#searchGlobalList input").removeAttr("checked");
            $("#searchGlobalList tr").removeClass("jtable-row-selected");
            $('#tempSelectedRecords').empty();
            setToolTip(this);
            //****On Load Selection of Rows*****//
            var rows = $("#searchGlobalList tr");
            rows.each(function (index) {
                var isSelectedValue = rows[index].children[1].innerText;
                if (isSelectedValue == 'true') {
                    $(this).find('input').attr("checked", true);
                    $(this).addClass("jtable-row-selected");
                }
            });
            var $selectedRows = $('#searchGlobalList').jtable('selectedRows');
            $("#MailReceipients").empty();
            $('#tempSelectedRecords').empty();
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    var emailAddressText = record.Name;
                    var emailId = record.Id;
                    $('#MailReceipients').append('<div class=\"emailAddress\" id=\"emailAddress\">' + emailAddressText + '<span id=\"' + emailId + '\" style=\"cursor:pointer\"><img src="/Gcs/Images/Close.png" /></span>;&nbsp;</div>');
                });
            }
        },

        actions: {
            listAction: '/GCS/Contract/GlobalAddressList?rightsExpiryEmailReceipientsId=' + rightsExpiryEmailReceipientsId
        },


        fields: {
            IsSelected:
            {
                title: 'IsSelected',
                visibility: 'hidden',
                display: function (id) {
                    var isSelected = id.record.IsSelected;
                    return isSelected.toString();
                }
            },
            Name:
            {
                title: globalAddressMessages.emailName
            },
            'CountryName':
            {
                title: globalAddressMessages.emailTitle,
                display: function (country) {
                    var artistId = country.record.CountryDetails.CountryName;
                    return artistId;
                }
            },
            EmailIds:
            {
                title: globalAddressMessages.emailLocation
            }
        }, selectionChanged: function () {
            $('#splitWarning').hide();
            //Get all selected rows
            var $selectedRows = $('#searchGlobalList').jtable('selectedRows');
            $("#MailReceipients").empty();
            $('#tempSelectedRecords').empty();
            //$('#MailReceipientsId').val('');
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#tempSelectedRecords').append(record.Name);
                    $('#model_MailReceipients').value = $('#tempSelectedRecords').val();
                    var recdisplay = document.getElementById('tempSelectedRecords');
                    recdisplay.style.display = 'none';
                    var emailAddressText = record.Name;
                    var emailId = record.Id;
                    $('#MailReceipients').append('<div class=\"emailAddress\" id=\"emailAddress\">' + emailAddressText + '<span id=\"' + emailId + '\" style=\"cursor:pointer\"><img src="/Gcs/Images/Close.png" /></span>;&nbsp;</div>');
                });
            }
            else {
                //No rows selected
                $('#SelectedRowList').append(messageCommon.viewValid);
            }
        }
    });



    $('#searchGlobalAddressList').click(function (e) {
        e.preventDefault();

        if (($('#addressName').val() == "" || $('#addressName').val() == null) && ($('#addressCountryName').val() == "" || $('#addressCountryName').val() == null)) {
            $('#SplitAlert').empty();
            $('#SplitAlert').append(globalAddressMessages.searchValidation);
            $('#splitWarning').show();
            $('#SplitAlert').show();
        }
        else {
            $('#searchGlobalList').jtable('load',
        {
            roleGroupName: $('#addressName').val(),
            countryName: $('#addressCountryName').val()
        });
        }
        resizeHandler();

    });

    $('#resetButton').click(function (e) {
        e.preventDefault();
        $("#MailReceipients").empty();
        $('#MailReceipients').val('');
        $('#addressName').val('');
        $('#addressCountryName').val('');
        //        if (document.getElementById('addressName') != null)
        //            document.getElementById('addressName').value = '';
        //        if (document.getElementById('addressCountryName') != null)
        //            document.getElementById('addressCountryName').value = '';

        //        $('#searchGlobalList').jtable('load',
        //        {
        //            roleGroupName: $('#addressName').val(),
        //            countryName: $('#addressCountryName').val()
        //        });
    });

    $('#cancelButton').click(function () {
        $('#GlobalPopup').dialog('close');
    });

    $('#okButton').click(function (e) {
        e.preventDefault();
        //$('#Contract_EmailReceipients').val($("#MailReceipients").val());
        //$('#Contract_EmailReceipientIds').val($("#MailReceipientsId").val());
        //$('#RightsExpiry_EmailReceipients').val($("#MailReceipients").val());
        //$('#RightsExpiry_EmailReceipientsId').val($("#MailReceipientsId").val());

        var emailNames = '';
        var emailIds = '';
        $('.emailAddress').each(function (index) {
            emailNames = emailNames + $(this)[0].firstChild.data + ';';
            emailIds = emailIds + $(this)[0].children[0].id + ';';

        });

        //RightsExpiry:For Delete functionality
        $('#RightsExpiry_EmailReceipients').val(emailNames);
        $('#RightsExpiry_EmailReceipientsId').val(emailIds);

        //For ContractCreationForm
        $('#Contract_EmailReceipients').val(emailNames);
        $('#Contract_EmailReceipientIds').val(emailIds);
        $('#GlobalPopup').dialog('close');
    });





});

$(document).ready(function () {
    $('#addressName').focus();
    $('#searchGlobalList').jtable('load');
    resizeHandler();
    $('#jqPager select').change(function () {
        $('#splitWarning').hide();
        pageSize = $('#jqPager select').val();
        $('#searchGlobalList').jtable({ 'pageSize': pageSize });
        $('#searchGlobalList').jtable('load',
        {
            Name: $('#addressName').val(),
            countryName: $('#addressCountryName').val(),
            Title: $('#GlobalAddress_CountryName').val(),
            Location: $('#GlobalAddress_Location').val()
        });
    });



    $("#GlobalPopup #globalAddressListSearchContainer").scroll(function () { $(".ui-autocomplete").hide(); });
});

$(document).ready(function () {
    //AutoComplete Country
    var target1 = $("#addressCountryName");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#addressCountryName").val(ui.item.value);
            $("#MailDetails_CountryDetails_CountryId").val(ui.item.id);
        }
    });

    $(window).bind("resize", resizeHandler);
});



function resizeHandler() {


    if ($('#splitWarning').css("display") == 'none') {
        $(".jtable-main-container").css('height', $(window).height() - 260);

    }
    else {
        $(".jtable-main-container").css('height', $(window).height() - 300);

    }

    //$(".jtable-main-container").css("height", $(window).height() - 260);
    $(".jtable-main-container").css("overflow", "auto");
}
//  $(window).bind("resize", resizeHandler);



$("#addressName, #addressCountryName")
      .bind("keyup", HideWarningSuccess);