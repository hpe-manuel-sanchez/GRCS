/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var pcNoticeMessages = {
    countryName: 'Country Name',
    companyName: 'Company Name',
    addInfo: 'Additional Information',
    companyValid: " Please enter more than 3 characters for Company Name",
    searchValid: "Please Enter Atleast one Search Criteria",
    Filtermessage: "The search results matching the search criteria exceed from 1000 results. Please refine your search criteria and try again"
};
var rowIndex;
var pcNoticePageSize = 25;
var rowcount = -1;
var rowIndex;
$(function () {
    $('#PcCompany_CountryName').focus();
    $('#searchPCNoticeList').jtable({
        paging: true,
        pageSize: pcNoticePageSize,
        sorting: true,
        selecting: true,
        selectOnRowClick: true,
        defaultSorting: 'CompanyName ASC',
        actions: {
            listAction: '/GCS/Contract/SearchPcNoticeCompany'
        },
        loadingRecords: function () {
            $(".ui-jtable-loading").show();
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {
            rowcount = data.serverResponse.TotalRecordCount;
            document.getElementById("searchPcResultCount").innerHTML = "Search Result(" + rowcount + ")";
            rowIndex = data.serverResponse.RowIndex;
            grsPcSearch(rowIndex);
            if (data.serverResponse.R2RowRetrieved >= 1000) {
                alert(pcNoticeMessages.Filtermessage);
            }
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
        },
        fields: {
            CompanyName: {
                title: pcNoticeMessages.companyName,
                display: function (pcnoticecompany) {
                    var pcnotice = pcnoticecompany.record.CompanyName;
                    return pcnotice;
                }
            },
            CountryName: {
                title: pcNoticeMessages.countryName
            },
            AdditionalNotes: {
                title: pcNoticeMessages.addInfo
            }
        },
        selectionChanged: function () {
            HideWarningSuccess();
            //Get all selected rows
            var $selectedRows = $('#searchPCNoticeList').jtable('selectedRows');
            $('#selectedPcCompany').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#selectedPcCompany').append(record.CompanyName);
                    // var recdisplay = document.getElementById('selectedPcCompany');
                    // recdisplay.style.display = 'none';
                    $(parent.document.getElementById('hiddenPCNoticeCountryCompanyId')).val(record.CompanyId);
                    var e1 = parent.document.getElementById('Contract_PcNoticeCountryCompanyId');
                    var index = parent.document.getElementById('Contract_PcNoticeCountryCompanyId').options.length;
                    var flag = 'False';
                    var companyCountry = record.CompanyName + '-' + record.CountryName;
                    var pcValue;
                    for (pcValue = 0; pcValue < index; pcValue++) {
                        if (e1.options[pcValue].text == companyCountry) {
                            e1.options[pcValue].selected = true;
                            flag = 'True';
                        }
                    }
                    if (flag == 'False') {
                        var newOption = document.createElement('option');
                        newOption.appendChild(document.createTextNode(companyCountry));
                        newOption.setAttribute('value', record.CompanyId);
                        newOption.setAttribute('text', companyCountry);
                        e1.appendChild(newOption);
                        var indexcount = e1.options.length;
                        for (pcValue = 0; pcValue < indexcount; pcValue++) {
                            if (e1.options[pcValue].text == companyCountry) {
                                e1.options[pcValue].selected = true;
                            }
                        }
                    }
                    $($(e1).parent()[0]).find('.ui-combobox input').val(companyCountry);
                    $('#PcPopup').dialog('close');
                });
            }
            else {
                //No rows selected
                $('#searchParentcontractList').append(messageCommon.viewValid);
            }
        }
    });

    $('#searchPCNoticeList').hide();

    //AutoComplete Data Admin Company
    var target1 = $("#PcCompany_CountryName");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#PcCompany_CountryName").val(ui.item.value);
            $("#hiddenPcNoticeCountryId").val(ui.item.isoCode);
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#PcCompany_CountryName").val("");
            }
            else if (ui.item != null && $("#PcCompany_CountryName").val() != ui.item.value) {
                $("#PcCompany_CountryName").val("");
            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#PcCompany_CountryName").val(ui.item.value);
                $("#hiddenPcNoticeCountryId").val(ui.item.isoCode);
            }
        }
    });

    $('#searchPCNoticeResult').click(function (e) {
        e.preventDefault();
        var length = document.getElementById('PcCompany_CompanyName').value.length;
        if (($('#PcCompany_CountryName').val() == "" || $('#PcCompany_CountryName').val() == null) && ($('#PcCompany_CompanyName').val() == "" || $('#PcCompany_CompanyName').val() == null)) {
            $('#SplitAlert').empty();
            $('#SplitAlert').append(pcNoticeMessages.searchValid);
            $('#splitWarning').show();
            $('#SplitAlert').show();
        }

            //        if ((document.getElementById('PcCompany_CountryName').selectedIndex == 0) && $('#PcCompany_CompanyName').val() == '') {
            //            // alert(messageCommon.searchValid);
            //            $('#SplitAlert').empty();
            //            $('#SplitAlert').append(pcNoticeMessages.searchValid);
            //            $('#splitWarning').show();
            //            $('#SplitAlert').show();
            //        }
        else if (document.getElementById('PcCompany_CountryName').selectedIndex == 0 && length < 3 && $('#PcCompany_CompanyName').val() != '') {
            alert(pcNoticeMessages.companyValid);
            return false;
        }
        else {
            $('#searchPCNoticeList').show();
            var pcNoticeDiv = document.getElementById('PCNoticeCompanyPaging');
            pcNoticeDiv.style.display = 'block';

            $('#searchPCNoticeList').jtable('load', {
                CompanyName: $('#PcCompany_CompanyName').val()
        , countryCode: $('#hiddenPcNoticeCountryId').val()
        , rowIndex: -1
        , jtPageSize: pcNoticePageSize
            });
        }
        var h = $(window).height();
        $("#searchPCNoticeList").css('height', h - 70);
        $(".jtable-main-container").css('height', h - 230);
        return false;
    });

    $('#PCNoticeCompanyPaging select').change(function () {
        HideWarningSuccess();
        pcNoticePageSize = $('#PCNoticeCompanyPaging select').val();
        $('#searchPCNoticeList').jtable({ 'pageSize': pcNoticePageSize });
        $('#searchPCNoticeList').jtable('load', {
            CompanyName: $('#PcCompany_CompanyName').val()
        , countryCode: $('#hiddenPcNoticeCountryId').val()
        , rowIndex: rowIndex
        , jtPageSize: pcNoticePageSize
        });
        $('#searchPCNoticeList').show();
    });

    //ResetButton
    $('#resetButton').click(function (e) {
        HideWarningSuccess();
        e.preventDefault();
        $('#PcCompany_CompanyName').val('');
        $('#PcCompany_CountryName').val('');
        //        var grd = document.getElementById('searchPCNoticeList');
        //        grd.style.display = 'none';
        var pcNoticeDiv = document.getElementById('PCNoticeCompanyPaging');
        pcNoticeDiv.style.display = 'none';
    });
});

function grsPcSearch(rowIndex) {
    $('#searchPCNoticeList').jtable('reset', {
        CompanyName: $('#PcCompany_CompanyName').val()
        , countryCode: $('#hiddenPcNoticeCountryId').val()
        , rowIndex: rowIndex
        , jtPageSize: pcNoticePageSize
    });
}
//on click hiding error messages
$("#PcCompany_CompanyName, #PcCompany_CountryName")
          .bind("keyup", HideWarningSuccess);