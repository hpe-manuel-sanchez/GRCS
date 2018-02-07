$(document).ready(function (e) {
   
    LoadManageCompanywatermark();
    $("#btnAdd").hide();
    // $("#btnCancel1").hide();
    $('#ddlCompanyPaging').hide();

});
function showManageCompanyMsg(msg) {

   
    var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + msg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: "Manage Company",
                show: 'clip',
                hide: 'clip',
                width: 300
            });

    objWarningDialog.dialog('open');
    objWarningDialog.dialog({
        buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        addManageCompany(e);

                    },
                    'No': function () {
                        $(this).dialog('close');
                        $('#manageCompanyContainer').dialog('close');
                        $('#manageCompanyContainer').remove();
                    }
                }
    });
}



$('#btnAdd').click(function (e) {

    var $selectedRows = $('#searchCompanyResults').jtable('selectedRows');
    if ($selectedRows.length == 0) {
        hideshowErrorDiv("Please Select atleast one Company");
        return false;
    }    
    if ($('#divAdminCompany').text().toString().trim() != "") {

        showManageCompanyMsg("This action will remove the previously added company. Do you want to continue (Yes/No)");
        return false;

    }
    addManageCompany(e);

});

function addManageCompany(e) {
 
    var $selectedRows = $('#searchCompanyResults').jtable('selectedRows');
    $selectedRows.each(function () {

        var record = $(this).data('record');
        // $('#divAdminCompany').text('');
        $('#divAdminCompany').text(record.Name + "(" + record.ISOCode + ")");
        $('#hdnAdminCompany').val(record.Id);
        $('#divAdminCompany').show();


    });

    $('#manageCompanyContainer').dialog('close');
    $('#manageCompanyContainer').remove();
}

$('#btnCancel1').click(function () {


    $('#manageCompanyContainer').dialog('close');
    $('#manageCompanyContainer').remove();
  
});
function LoadManageCompanywatermark() {

    if (jQuery("#CompanyDetails_Name") != null) {
        jQuery("#CompanyDetails_Name").watermark(watermarkCompanyName);
    }
    if (jQuery("#isaccode") != null) {
        jQuery("#isaccode").watermark(watermarkIsacCode);
    }
    if (jQuery("#country") != null) {
        jQuery("#country").watermark(watermarkCountry);
    }

}

$('#btnOpenSearchComp').click(function () {

    if (($('#CompanyDetails_Name').val() == null || $('#CompanyDetails_Name').val() == "") && ($('#isaccode').val() == null || $('#isaccode').val() == "") && ($('#country').val() == null || $('#country').val() == "")) {
        hideshowErrorDiv(createWorkgroupMessages.searchInValid);
        $('#manageCompanyContainer').css("width", "100%");
        return false;
    }

    pagingCount = $('#ddlCompanyPaging').val();


    var searchlist = '';
    company = $('#CompanyDetails_Name').val();
    isacCode = $('#isaccode').val();
    var country = $('#country').val();

    if (company != '') { searchlist = searchlist + company + '/'; }
    if (isacCode != '') { searchlist = searchlist + isacCode + '/'; }
    if (country != '') { searchlist = searchlist + country; }
    if (country == '') {
        searchlist = searchlist.substring(0, searchlist.length - 1);
    }

    $('#spnMgCompanyAddedResultLabel').html('');
    $('#spnMgCompanyAddedResultLabel').hide();
    $("#searchResultForCompany").html('<p style=\"margin-left:13px\">' + "Search Result For <b>" + searchlist + '&nbsp;<span id="cmpSearchResultRecordcount"/></b></p>');
    $('#managecompany').hide();
    $('#searchCompanyPopup').show();
    $('#divSearchCompanyPaging').show();
    LoadManageCompanywatermark();
    $('#searchCompanyResults').jtable('destroy');
    LoadSearchJtable(pagingCount);
    $("#btnCancel1").show();
    $("#btnAdd").show();
});

$('#btnReset').click(function () {
    if ($('#btnRemove').is(':visible')) {
        if ($('#addedManageCompanyResults')[0].innerHTML != "") {
            $('#spnMgCompanyAddedResultLabel').html(listOfMgCompanies);
            $('#spnMgCompanyAddedResultLabel').show();
        }
        else {
            $('#spnMgCompanyAddedResultLabel').html('');
            $('#spnMgCompanyAddedResultLabel').hide();
        }
        Reset();
        hideErrorDiv();
        LoadManageCompanywatermark();
    }
   
});

function hideshowErrorDiv($msg) {

    if ($msg != undefined && $msg != "") {
        $("#popupMgCompanyerrorMessage").show();
        $("#popupMgCompanyerrorMessage").html($msg);
    }
    else {

        $("#popupMgCompanyerrorMessage").hide();
        $("#popupMgCompanyerrorMessage").html('');
    }
 

}

function LoadSearchJtable(pageSize) {

    hideshowErrorDiv();
    $('#searchCompanyResults').jtable({
       paging: true,
        pageSize: pageSize,
        sorting: true,       
        selecting: true,
        selectOnRowClick: true,
        multiselect: false,
        defaultSorting: 'Company ASC', 
            
        actions: {
            listAction: "/GCS/ClearanceInbox/SearchCompany"
        },
        loadingRecords: function () {
            //debugger
            $('.jtable .jtable-no-data-row').hide();
        },
        fields: {
            radiobutton: {
                title: 'Select',
                width: '1%',
                sorting: false,
                edit: false,
                create: false,
                display: function () {
                    //creating radiobutton column in list
                    var $radiobutton = $('<input type="radio" value="0" name="rbtnSelect" />');
                    return $radiobutton;
                }
            },
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false                              
            },
            Name: {
                title: mgCompJtableColNames.Company,
                width: '30%'
            },

            ISACCode: {
                title: mgCompJtableColNames.ISACCompCode,
                width: '15%'
            },

            CountryName: {
                title: mgCompJtableColNames.CountryName,
                width: '20%'
            },
            CompanyType: {
                title: 'Company Type',
                width: '20%',
                display: function (project) {

                    var displayText = project.record.CompanyType == "T" ? "Third Party" : "Universal Music";

                    return displayText;
                }
            },
            UniversalLicenseeIndicator: {
                title: 'Univeral Licensee',
                width: '10%',
                display: function (project) {

                    var displayText = project.record.UniversalLicenseeIndicator == true ? "Yes" : "No";
                    return displayText;
                }
            }

        },
        recordsLoaded: function (event, data) {
            $(".jtable-command-column-header").hide();
            $(".jtable-selecting-column").hide();

            $('#cmpSearchResultRecordcount').html('(' + data.serverResponse.TotalRecordCount + ')');
            //$('#searchCompanyResults .jtable thead tr th:first').remove();
            // $('<th class="jtable-command-column-header" style="width: 1%; ">Select</th>').prependTo('#searchCompanyResults .jtable thead tr:first');


            if (data.records.length > 0) {
                $('.jtable .jtable-no-data-row').show();
                //    $("#searchCompanyResults input").removeAttr("checked");
                //   $("#searchCompanyResults tr").removeClass("jtable-row-selected");

                if (document.getElementById('btnAdd').style.visibility == 'hidden') {
                    document.getElementById('btnAdd').style.visibility = 'visible';
                }
                if (document.getElementById('btnCancel1').style.display == 'none') {
                    $("#btnCancel1").show();
                }
                $('#ddlCompanyPaging').show();

                $("#btnAdd").removeAttr("disabled");
            }
            else {
                $("#btnAdd").attr("disabled", "disabled");

            }
        },

        //Register to selectionChanged event to hanlde events

        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('#searchCompanyResults').jtable('selectedRows');
            jsonObj = '';
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');                   
                    var chselect = $(this)[0].cells[0].children[0].checked;
                    if (chselect == false) {
                        $(this)[0].cells[0].children[0].checked = true;
                    }
                    jsonObj = jsonObj + record.Id + ",";
                });
            }
        }
    });
 
        $('#searchCompanyResults').jtable('load', {
            name: $('#CompanyDetails_Name').val(),
            isacCode: $('#isaccode').val(),
            country: $('#country').val()
        });
   
}
