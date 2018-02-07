/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/AddressBook.js" />



//var deletedItemList = [];
var ManageAddressBookMessages = { fail: "Fail", confim: "Confirm", Title: "Add Role/Group", EditTitle: "Edit Role/Group", DeleteRoleGroup: "Record  Deleted Successfully", ConfirmDeletion: "Do you want to delete the selected rows from the system?", FiterFeilds: "Please Enter the Fields for Filter Criteria ", SelectRow: "Please Select Row For Edit ", Minlength: "Please Provide 3 characters For Filter Criteria", SelectRowForEdit: "Please select only one Row for Edit", DeleteRow: "Please Select a Row To Delete", NameValidation: "Please Enter Minimum 3 Characters" };

var objGlobalDialog = $('<div id="Address"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            width: "60%",
            resizable: true,
      position: [(($(window).width() - (($(window).width() * 60) / 100)) / 2), 50]
  });


  $(document).ready(function () {
      $('#CreateNewEntry').focus();
            $('#clrfilter').hide();
//            $('#main').click(function () {
//                HideWarningSuccess();
//                return false;
//            });
            $('#jqPager select').change(function () {
                HideWarningSuccess();
                pageSize = $('#jqPager select').val();
                $('#jqgrid').jtable({ 'pageSize': pageSize });
                $('#jqgrid').jtable('load',
            {
                roleGroupName: $('#EmailGroupDetails_Name').val(),
                countryName: $('#EmailGroupDetails_CountryDetails_CountryName').val()
            });

            });




            $('#CreateNewEntry').click(function (e) {
                e.preventDefault();
                HideWarningSuccess();
                objGlobalDialog.html('<p>' + messageCommon.onLoading + '</p>');
                objGlobalDialog.load('/GCS/Contract/AddRoleGroup/', "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                objGlobalDialog.html('<p>' + messageCommon.error + '</p>');
            }
        });

                objGlobalDialog.dialog('option', { title: ManageAddressBookMessages.Title });
                //Open Dialog
                objGlobalDialog.dialog('open');

            });
            reSize();


            $(".scrollAddressBook").scroll(function () { $(".ui-autocomplete").hide(); });

            $(window).resize(function () {
                reSize();
            });

            //Accordion style collapse/expand
            $('#accordion').click(function (e) {
                e.preventDefault();
                HideWarningSuccess();
                $(this).next().toggle();
                $(this).find('a').toggleClass('iconBottom');
                $('#EmailGroupDetails_Name').focus();
                return false;

            }).next().show();
        });
function reSize() {
    var h = $(window).height();

    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".scrollAddressBook").css('height', h - 160);
    else
        $(".scrollAddressBook").css('height', h - 188);
}

var addressMessages = {
    all: "All",
    roleName: "Role/ Group Name",
    country: "Country",
    emailId: "Email IDs",
    groupId: "Group Id",
    countryId: "Country Id"
};

var rowIndex = -1;
var pageSize = 25;
$(function () {
    pageSize = pageSize;
    $('#jqgrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'Name ASC',
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true,
        selectOnRowClick: true,
        loadingRecords: function () {
            $(".ui-jtable-loading").show();
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {

            rowIndex = data.serverResponse.TotalRecordCount;
            document.getElementById("roleResultCount").innerHTML = "Role/Group Name (" + rowIndex + ")";
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            $("#jqgrid input").removeAttr("checked");
            $("#jqgrid tr").removeClass("jtable-row-selected");
            setToolTip(this);
        },
        actions: {
            listAction: '/GCS/Contract/ManageAddress'
        },
        fields: {

            Id:
                {
                    title: addressMessages.groupId,
                    list: false
                },
            Name:
                {
                    title: addressMessages.roleName,
                    width: '15%'
                },
            'CountryName':
                {
                    title: addressMessages.country,
                    width: '15%',
                    display: function (country) {
                        var countryName = country.record.CountryDetails.CountryName;
                        return countryName;
                    }
                },
            'CountryId':
                {
                    title: addressMessages.country,
                    display: function (country) {
                        var countryId = country.record.CountryDetails.CountryId;
                        return countryId;
                    },
                    list: false
                },
            EmailIds:
                {
                    title: addressMessages.emailId,
                    width: '70%'
                }
        },
        selectionChanged: function () {
            //Get all selected rows
            HideWarningSuccess();
            var $selectedRows = $('#jqgrid').jtable('selectedRows');

            $('#selectAddressList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#selectAddressList').append(record.Name + ':' + record.CountryDetails.CountryName
                        + ':' + record.EmailIds + ':' + record.Id + ':' + record.CountryDetails.CountryId + ':');
                    var recdisplay = document.getElementById('selectAddressList');
                    recdisplay.style.display = 'none';
                });
            }
            else {
                //No rows selected
                $('#selectAddressList').append(messageCommon.viewValid);
            }
        }
    });


    $('#DeleteAddress').click(function () {
        var $selectedRows = $('#jqgrid').jtable('selectedRows');
        if ($selectedRows.length == 0) {
            //            objDialog.html(ManageAddressBookMessages.DeleteRow);
            //            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            //            objDialog.dialog('open');
            ShowWarning(ManageAddressBookMessages.DeleteRow);
            return false;
        } else {
            var objWarningDialog = $('<div id="delete"></div>')
        .html('<p>' + ManageAddressBookMessages.ConfirmDeletion + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: ManageAddressBookMessages.confim,
            show: 'clip',
            hide: 'clip',
            width: 300
        });

            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        e.preventDefault();
                        if ($selectedRows.length > 0) {
                            var groupId = '';
                            //Show selected rows
                            $selectedRows.each(function () {
                                var record = $(this).data('record');
                                groupId = groupId + ',' + record.Id;
                            });
                        }
                        $(this).dialog('close');
                        $.get('/GCS/Contract/DeleteEmailGroups/', { groupId: groupId },
                            function (data) {
                                if ($(data).find("#deleteDisplayMessage").val() == "success") {
                                    $('#jqgrid').jtable('load');
                                    ShowSuccess(ManageAddressBookMessages.DeleteRoleGroup);

                                    //                                    objDialog.html(ManageAddressBookMessages.DeleteRoleGroup);
                                    //                                    objDialog.dialog('open');
                                    //                                    objDialog.dialog({
                                    //                                        buttons: {
                                    //                                            'Ok': function () {
                                    //                                                $(this).dialog('close');
                                    //                                                $('#jqgrid').jtable('load');
                                    //                                            }
                                    //                                        }
                                    //                                    });
                                } else {
                                    objDialog.html(ManageAddressBookMessages.fail);
                                    objDialog.dialog('open');
                                    objDialog.dialog({
                                        buttons: {
                                            'Ok': function () {
                                                $(this).dialog('close');
                                                $('#jqgrid').jtable('load');
                                            }
                                        }
                                    });

                                }
                            });
                    }, 'Cancel': function () {
                        $(this).dialog('close');
                        $('#jqgrid').jtable('load');
                    }
                }

            });

        }
        return false;
    });

    $('#jqgrid').jtable('load',
        {
            groupId: $('#Id').val(),
            countryId: $('#CountryId').val()
        });



    //Reset button click
    $('#resetAddress').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        $('#EmailGroupDetails_Name').val(''),
        $('#EmailGroupDetails_CountryDetails_CountryName').val('');
        // window.location.href = "/GCS/Contract/ManageAddressBook/";
    });
    //Clear Filter Click
    $('#clrfilter').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        $('#jqgrid').jtable('load');
        //        $('#accordion').next().toggle();
        //        $('#accordion').find('a').toggleClass('iconBottom');

        $('#EmailGroupDetails_Name').val(''),
        $('#EmailGroupDetails_CountryDetails_CountryName').val('');

        // window.location.href = "/GCS/Contract/ManageAddressBook/";
    });

    //For Filtering
    $('#filterAddress').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();


        if (($('#EmailGroupDetails_Name').val() == "" || $('#EmailGroupDetails_Name').val() == null) && ($('#EmailGroupDetails_CountryDetails_CountryName').val() == "" || $('#EmailGroupDetails_CountryDetails_CountryName').val() == null)) {
            //            objDialog.html(ManageAddressBookMessages.FiterFeilds);
            //            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            // Open Dialog
            //            objDialog.dialog('open');

            // $('.jtable .jtable-no-data-row').show();
            //$('#jqgrid').jtable('load');

            $(".ui-jtable-loading").hide();
            ShowWarning(ManageAddressBookMessages.FiterFeilds);
            // return false;
        }
        else {
            $('#jqgrid').jtable('load',
            {
                roleGroupName: $('#EmailGroupDetails_Name').val(),
                countryName: $('#EmailGroupDetails_CountryDetails_CountryName').val()
            });
            $('#accordion').next().toggle();
            $('#accordion').find('a').toggleClass('iconBottom');
            $('#clrfilter').show();
            $('#filt').show();
        }
        return false;
    });
//        $('#EmailGroupDetails_Name').keyup(function () {
//            HideWarningSuccess();
//        });
//        $("#EmailGroupDetails_CountryDetails_CountryName").keyup(function () {
//            HideWarningSuccess();
//        });
        $("#EmailGroupDetails_CountryDetails_CountryName, #EmailGroupDetails_Name")
          .bind("keyup", HideWarningSuccess);

    //Edit button clik
    $('#EditAddress').click(function () {
        var $selectedRows = $('#jqgrid').jtable('selectedRows');
        if ($selectedRows.length == 1) {
            var addressSelected = $('#selectAddressList')[0].innerHTML;
            var address = addressSelected.split(':');
            var groupName = address[0];
            var addressCountryName = address[1];
            var addressEmailId = address[2];
            var roleGroupId = address[3];
            var addressCountryId = address[4];
            var isUpd = -1;
            if (addressSelected != null && addressSelected != '') {

                //Load partial view
                objGlobalDialog.load('/GCS/Contract/EditRoleGroup/',
                            {
                                countryName: addressCountryName,
                                groupName: groupName,
                                addressEmailIds: addressEmailId,
                                roleGroupId: roleGroupId,
                                countryId: addressCountryId,
                                isUpdate: isUpd

                            },
                            function (responseText, textStatus) {
                                if (textStatus == 'error') {
                                    objGlobalDialog.html('<p>' + messageCommon.error + '</p>');

                                }
                            });

                //Open Dialog
                objGlobalDialog.dialog('option', { title: ManageAddressBookMessages.EditTitle });
                objGlobalDialog.dialog('open');
            }
            //            else {
            //                objDialog.html(ManageAddressBookMessages.SelectRow);
            //                objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //                //Open Dialog
            //                objDialog.dialog('open');
            //                return false;
            //            }
        }
        else if ($selectedRows.length == 0) {
            //                objDialog.html(ManageAddressBookMessages.SelectRow);
            //                objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            //                objDialog.dialog('open');
            ShowWarning(ManageAddressBookMessages.SelectRow);
            return false;
        }
        if ($selectedRows.length > 1) {
            //            objDialog.html(ManageAddressBookMessages.SelectRowForEdit);
            //            objDialog.dialog({ buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            //Open Dialog
            //            objDialog.dialog('open');
            ShowWarning(ManageAddressBookMessages.SelectRowForEdit);
            return false;

        }
        return false;
    });






    var objDialog = $('<div id="ContractTab"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            width: 500
        });

});
   
    


    $(document).ready(function () {
        //AutoComplete Country
        var target1 = $("#EmailGroupDetails_CountryDetails_CountryName");
        $('#accordion').next().toggle();
        $('#accordion').find('a').toggleClass('iconBottom');
        $("input[data-autocomplete-source]").hide();
        target1.autocomplete({
            source: target1.attr("data-autocomplete-source-manual"),
            minLength: 2,

            select: function (event, ui) {

                $("#EmailGroupDetails_CountryDetails_CountryName").val(ui.item.value);
                $("#EmailGroupDetails_CountryDetails_CountryId").val(ui.item.id);

            },

            //        search: function (event,ui) {
            //                $('#loadingDiv').hide();
            //                 $('#grsLoadingSmall').show()
            //                .ajaxStart(function () { $(this).show(); })
            //                .ajaxStop(function () { $(this).hide(); });


            //           },

            change: function (event, ui) {

                if (ui.item == null) {
                    $("#EmailGroupDetails_CountryDetails_CountryName").val("");
                }
                else if (ui.item != null && $("#EmailGroupDetails_CountryDetails_CountryName").val() != ui.item.value) {
                    $("#EmailGroupDetails_CountryDetails_CountryName").val("");
                }
                else if (ui.item != null && $(ui.item.value != null)) {
                    $("#EmailGroupDetails_CountryDetails_CountryName").val(ui.item.value);
                    $("#EmailGroupDetails_CountryDetails_CountryId").val(ui.item.id);
                }
            }

        });


    });
   
    
    









