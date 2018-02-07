/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/CustomSetting.js" />

var clearanceAdminCompany, StartYear, EndYear, ResourceType, artist, genre, title, country, preClearanceType,
    recordCount = 0, generateReport = false, companyId = '', companyNames = '', countryId = '', countryNames = '';

$(document).ready(function () {
    $('.gr_breadCrumbNav').html("Report > > Repertoire Report > > Pre Cleared Resources");
    resizeHandler();

    $(window).resize(function () {
        SyncfusionGridScroll();
    });

    $('#yearFrom option:eq(0)').attr('disabled', 'disabled');
    $('#yearTo option:eq(0)').attr('disabled', 'disabled');
    $("#grdPreClearedResourcewaiting_WaitingPopup_Image").hide();


    //***************Year Picker*******************//
    $('#yearFrom').change(function (e) {
        $("#yearTo").children("option[value]").removeAttr('disabled');
        $('#yearTo option:eq(0)').attr('disabled', 'disabled');
        var value = $(this).val();
        for (i = 1900; i < value; i++) {
            $("#yearTo").children("option[value^=" + i + "]").attr('disabled', 'disabled');
        }
    });

    $('#yearTo').change(function (e) {
        $("#yearFrom").children("option[value]").removeAttr('disabled');
        $('#yearFrom option:eq(0)').attr('disabled', 'disabled');
        var value = $(this).val();
        for (i = 2100; i > value; i--) {
            $("#yearFrom").children("option[value^=" + i + "]").attr('disabled', 'disabled');
        }
    });
    //********************END Year Picker***************************//


    //reset
    $('#btnreset').click(function (e) {
        e.preventDefault();

        $('#Title').val('');
        $('#ResourceType').val('0');
        $('#ResourceGenre').val('0');
        $('#PreClearanceType').val('0');

        $('#AdminCompany').val('');
        $('#AdminCompanyNames').empty();

        $('#country').val('');
        $('#countryNames').empty();
                
        $("#yearTo").children("option[value]").removeAttr('disabled');
        $('#yearTo option:eq(0)').attr('selected', 'selected');
        $('#yearTo option:eq(0)').attr('disabled', 'disabled');

        $("#yearFrom").children("option[value]").removeAttr('disabled');
        $('#yearFrom option:eq(0)').attr('selected', 'selected');
        $('#yearFrom option:eq(0)').attr('disabled', 'disabled');
        
        $('#ArtistSearch_Info_Name').val('');
        $('#ArtistSearch_Info_Id').val('');
        
        generateReport = false;
        HideWarningPanel();
        
        companyId = '';
        companyNames = '';
        
        countryId = '';
        countryNames = '';
    });

    //AutoComplete Artist
    $('#ArtistSearch_Info_Name').focus();

    var target1 = $("#ArtistSearch_Info_Name");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            if (ui.item == null) return;
            $("#ArtistSearch_Info_Name").val(ui.item.value);
            $("#ArtistSearch_Info_Id").val(ui.item.id);
        },
        response: function (event, ui) {
            var autocomplete = target1.data("autocomplete");
            if (ui.content.length == 1) {
                autocomplete.selectedItem = ui.content[0];
            }
            if (autocomplete.selectedItem) {
                setTimeout(function () {
                    autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                    target1.autocomplete('close');
                    $("#ArtistSearch_Info_Name").focus();
                }, 200);
            }
        }
    });

    //generate report
    $('#btnGenerateReport').click(function () {
       
        var gridobj = $find("grdPreClearedResource");

        clearanceAdminCompany = $('#AdminCompany').val();
        StartYear = $('#yearFrom').val();
        EndYear = $('#yearTo').val();
        preClearanceType = $('#PreClearanceType').val();
        //ClearanceCompanyId = $('#AdminCompany').val();
        country = $('#country').val();
        ResourceType = $('#ResourceType').val();
        artist = $('#ArtistSearch_Info_Id').val();
        genre = $('#ResourceGenre').val();
        title = $('#Title').val();

        if (clearanceAdminCompany == "" && StartYear == "" && EndYear == "" && country == "" && ResourceType == '0' && artist == "" && genre == '0' && title == "") {
            $('#AdminCompanyNames').addClass("input-validation-error");
            $('#yearFrom').addClass("input-validation-error");
            $('#yearTo').addClass("input-validation-error");
            $('#countryNames').addClass("input-validation-error");
            $('#ResourceType').addClass("input-validation-error");
            $('#ArtistSearch_Info_Name').addClass("input-validation-error");
            $('#ResourceGenre').addClass("input-validation-error");
            $('#Title').addClass("input-validation-error");
            ShowError(PreClearedMessage.MandatoryFieldvalidation);
            resizeNew();
            return false;
        } else {
            HideWarningPanel();
        }

        generateReport = true;
        $('#generateReport').val(true);

        $find("grdPreClearedResource").sendRefreshRequest();
        $(".ReportTemplates").show();

    });

    function HideWarningPanel() {
        HideWarningSuccess();
        $('#AdminCompanyNames').removeClass("input-validation-error");
        $('#yearFrom').removeClass("input-validation-error");
        $('#yearTo').removeClass("input-validation-error");
        $('#countryNames').removeClass("input-validation-error");
        $('#ResourceType').removeClass("input-validation-error");
        $('#ArtistSearch_Info_Name').removeClass("input-validation-error");
        $('#ResourceGenre').removeClass("input-validation-error");
        $('#Title').removeClass("input-validation-error");

    }

});



/***********************Resize Popup starts ***************************/
function resizeHandler(e) {

   if (e == undefined || $(e.target).hasClass('ui-dialog') == false) {
   // For Country Dialog Box 
       $('#CountryDialog').dialog('option', 'width', getPopupWidth(70, 750));
       $('#CountryDialog').dialog('option', 'height', getPopupHeight(80, 300, 60));
       $('#CountryDialog').dialog('option', 'position', [getXPosition(70, 100), getYPosition(80, 30)]);
    // For CAC 
       $('#ClearanceAdminDialog').dialog('option', 'width', getPopupWidth(70, 750));
       $('#ClearanceAdminDialog').dialog('option', 'height', getPopupHeight(80, 300, 60));
       $('#ClearanceAdminDialog').dialog('option', 'position', [getXPosition(70, 100), getYPosition(80, 30)]);
                

    }
}
/***********************Resize Popup Ends ***************************/

/********** grid events*****************/

function GridBegin(sender, args) {
    //debugger;
    $(".EmptyCell").html("Retrieving Records");
    // args.data["AdminCompany"] = clearanceAdminCompany;
    args.data["YearFrom"] = StartYear;
    args.data["YearTo"] = EndYear;
    args.data["ClearanceCompanyId"] = clearanceAdminCompany;
    args.data["PreClearanceType"] = preClearanceType;
    args.data["ExploitationCountryId"] = country;
    args.data["ResourceType"] = ResourceType;
    args.data["artist"] = artist;
    args.data["ResourceGenre"] = genre;
    args.data["Title"] = title;

    args.data["generateReport"] = generateReport;

    $("#grdPreClearedResource .manualPagerLabel:eq(1)").empty();
    $("#grdPreClearedResource .manualPagerLabel:eq(1)").text("Show items per page");
}

function ActionSuccess(sender, args) {
    SyncfusionGridScroll();
    setSyncGridToolTip("#grdPreClearedResource_Table");
    var totCount = sender.get_TotalRecordsCount();

    $("#grdPreClearedResource .MsgBar").text("Pre-Cleared Resource Result (" + totCount + ")");
    $("#grdPreClearedResource .manualPagerLabel:eq(1)").empty();
    $("#grdPreClearedResource .manualPagerLabel:eq(1)").text("Show items per page");

    $('#SortField').val(sender.currentSortColumn);
    $('#IsAscending').val(sender.currentSortDirection);
}



function SyncfusionGridScroll() {
    var pagerHgt = $(".GridPager").height();
    var headerHgt = $(".GridHeader").height();
    var browsHgt = 0;
    if ($.browser.msie)
        browsHgt = 23;
    else
        browsHgt = 20;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;

    var jtableTop = $("#grdPreClearedResource").offset().top;
    var topfootPos = $(".footer").offset().top;
    var totRecHeight = $("#grdPreClearedResource_Table").height() + reduceHgt;
    var tableHeight = topfootPos - jtableTop;
    var gridObj = $find("grdPreClearedResource");
    if (totRecHeight >= tableHeight)
        setSyncfusionGridHeight(gridObj, tableHeight - reduceHgt);
    else
        setSyncfusionGridHeight(gridObj, totRecHeight - reduceHgt + 20);
}

function setSyncfusionGridHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}

function setToolTip() {
    $("#grdPreClearedResource_Table").find("td").each(function () {
        var len = $(this).text().length;
        if (len > 35) {
            var textString = $(this).text();
            if (textString.indexOf('') == 0)
                textString = textString.replace(/[;]/g, '; ');

            $(this).prop("title", textString);
            $(this).tooltip();
            $(this).text($(this).text().substr(0, 35) + '...');
        }
    });
}


function reSizeReportPage() {
    var h = $(window).height();
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".ReportTable").css('height', h - 160);
    else
        $(".ReportTable").css('height', h - 200);
}


function GridLoad(sender, args) {
    $("#grdPreClearedResource .MsgBar").empty();
    $("#grdPreClearedResource .manualPagerLabel:eq(1)").empty();
    $("#grdPreClearedResource .manualPagerLabel:eq(1)").text("Show items per page");
    $("#grdPreClearedResource .gridPagerContainerRight").find(".refLink").remove();
    $("#grdPreClearedResource .gridPagerContainerRight").find(".RefreshPager").after("<span class=\"hyperlink\"><a class=\"refLink\" href=\"#\" onclick=\"RefreshClick()\"></a></span>");
}


function RefreshClick() {
    $find("grdPreClearedResource").sendRefreshRequest();
}

function ImageForResourceType(events, args) {
    var image;
    if (args.Column.Name == "ResourceType") {
        switch (args.Data.ResourceType) {
            case "Audio":
                image = "<img src='/GCS/Images/ResourceAudio.gif' title=\"Audio\" />";
                break;
            case "Video":
                image = "<img src='/GCS/Images/ResourceVideo.gif' title=\"Video\"/>";
                break;
            case "Image":
                image = "<img src='/GCS/Images/ResourceImage.gif' title=\"Image\"/>";
                break;
            default:
                image = "<img src='/GCS/Images/resource_group.gif' title=\"Others\"/>";
                break;
        }
        return $(args.Element)[0].innerHTML = image;
    }

}


/********** grid events end*****************/

//************Delete Functionality for individual CAC*************//

//collaplsable panel
$(document).ready(function () {


    $('#search .report_parameter_header').click(function (e) {
        e.preventDefault();
        if ($('a').hasClass("down")) {
            $('a').removeClass("down").addClass("up");
        } else {
            $('a').removeClass("up").addClass("down");
        }

        //  $(this).find('a').toggleClass('iconBottom');
        $('.report_parameter_panel_Without_b_padding').toggle();
        SyncfusionGridScroll();
        return false;
    }).next().show();

    /***********************Code to open Admin Company Popup***************************/


    $('#clearenceCompany span').live('click', function () {
        $(this).parent().remove();
        getCompanies();
    });

    //get company Id's 
    function getCompanies() {

        var companyCount = $('.clearenceCompany').length;
        companyId = '';
        companyNames = '';
        if (companyCount != 0) {
            for (var temp = 0; temp < companyCount; temp++) {
                if (companyId == '') {
                    companyId += $('.clearenceCompany').children()[temp].id;
                    companyNames += $('.clearenceCompany').children()[temp].previousSibling.toString();
                }
                else {
                    companyId += ', ' + $('.clearenceCompany').children()[temp].id;
                    companyNames += ', ' + $('.clearenceCompany').children()[temp].previousSibling.toString();
                }
            }
        }
        $('#AdminCompany').val(companyId);
        //  $('#CompanyNames').val(companyNames);
    }


    var PopupMessages = { fail: "Fail", confim: "Confirm", Title: "Select/Remove Companies ", EditTitle: "Edit Role/Group", DeleteRoleGroup: "Record  Deleted Successfully", ConfirmDeletion: "Do you want to delete the selected rows from the system?", FiterFeilds: "Please Enter the Fields for Filter Criteria ", SelectRow: "Please Select Row For Edit ", Minlength: "Please Provide 3 characters For Filter Criteria", SelectRowForEdit: "Please select only one Row for Edit", DeleteRow: "Please Select a Row To Delete", NameValidation: "Please Enter Minimum 3 Characters" };

    var objGlobalDialog = $('<div id="ClearanceAdminDialog"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: '',
            show: 'clip',
            hide: 'clip',
            width: function () {
                var w = (($(window).width() * 55) / 100);
                if (parseInt(w) < 550)
                    return minWidth;
                else
                    return parseInt(w);
            },
            height: getPopupHeight(80, 300, 60),
            minHeight: 300,
            minWidth: 550,
            //position: [getXPosition(55, 0), getYPosition(80, 60)],
            resizable: true,
            close: function () {
                $('#ClearenceList').find('tr').remove(); //To Clear the data on close function
                $('#SelectedList').find('tr').remove(); //To Clear the data on close function
                $('#txtClearanceAdminComp').val(''); //To Clear the data on close function  
                $("#imgClearIcon").hide();
                $('#txtClearanceAdminComp').addClass('ui-autocomplete-input');
                textBackup = '';
                $('#labelSelCount').html($('#SelectedList').find('tr').length);
                getCompanies();
            }
        });


    $('#SelectRemoveCompany').click(function (e) {
        e.preventDefault();
        objGlobalDialog.dialog('option', { title: PopupMessages.Title, position: 'center' });
        //Open Dialog
        objGlobalDialog.dialog('open');
        //partial load
        objGlobalDialog.load('/GCS/Report/Company', "",
                function (responseText, textStatus) {
                    if (textStatus == 'error') {
                        objGlobalDialog.html('<p>' + messageCommon.error + '</p>');
                    }
                    $('#reportType').val('RepertoirReport');
                });

        if ($("#ClearanceAdminDialog").is(':visible')) {
            if ($(".cacTableContainer").is(':visible')) {
                var TotDiaHgt = $("#ClearanceAdminDialog").height();
                var ReduceHgt = TotDiaHgt - 110;
                $(".cacTable").css('height', ReduceHgt + "px");
                $(".ClearanceAdminDialog").css('position', 'center');
            }
        }
    });
    /***********************End Admin Company Popup***************************/

    /***********************Code to open Country Popup***************************/
    //************Delete Functionality for individual country*************//
    $('#expCountry span').live('click', function () {
        $(this).parent().remove();
        getCountries();
    });

    //get country Id's 
    function getCountries() {

        var countryCount = $('.expCountry').length;
        countryId = '';
        countryNames = '';
        if (countryCount != 0) {
            for (var temp = 0; temp < countryCount; temp++) {
                if (countryId == '') {
                    countryId += $('.expCountry').children()[temp].id;
                    countryNames += $('.expCountry').children()[temp].previousSibling.toString();
                }
                else {
                    countryId += ', ' + $('.expCountry').children()[temp].id;
                    countryNames +=  ', ' + $('.expCountry').children()[temp].previousSibling.toString();                    
                }
            }
        }
        $('#country').val(countryId);
    }

    var CountryPopupMessages = { fail: "Fail", confim: "Confirm", Title: "Select/Remove Countries ", Minlength: "Please Provide 3 characters For Filter Criteria", NameValidation: "Please Enter Minimum 3 Characters" };

    var objCountryDialog = $('<div id="CountryDialog"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: '',
            show: 'clip',
            hide: 'clip',
            width: function () {
                var w = (($(window).width() * 55) / 100);
                if (parseInt(w) < 550)
                    return minWidth;
                else
                    return parseInt(w);
            },
            height: getPopupHeight(80, 300, 60),
            minHeight: 300,
            minWidth: 550,
            // position: [getXPosition(55, 0), getYPosition(80, 60)],
            resizable: true,
            close: function () {
                $('#countryList').find('tr').remove(); //To Clear the data on close function
                $('#SelectedcountryList').find('tr').remove(); //To Clear the data on close function
                $('#txtCountry').val(''); //To Clear the data on close function
                $("#imageClearIcon").hide();
                $('#txtCountry').addClass('ui-autocomplete-input');
                $('#labelCount').html("0");
                textBkp = '';
                getCountries();
            }
        });



    $('#SelectRemovecountry').click(function (e) {
        e.preventDefault();
        //HideWarningSuccess();
        objCountryDialog.dialog('option', { title: CountryPopupMessages.Title, position: 'center' });
        //Open Dialog
        objCountryDialog.dialog('open');

        //load partial
        objCountryDialog.load('/GCS/Report/Country', "",
                function (responseText, textStatus) {
                    if (textStatus == 'error') {
                        objCountryDialog.html('<p>' + messageCommon.error + '</p>');
                    }
                });

        if ($("#CountryDialog").is(':visible')) {
            if ($(".cacTableContainer").is(':visible')) {
                var TotDiaHgt = $("#CountryDialog").height();
                var ReduceHgt = TotDiaHgt - 110;
                $(".cacTable").css('height', ReduceHgt + "px");
                $(".CountryDialog").css('position', 'center');
            }
        }
    });
    /***********************End Country Popup***************************/
});

function isCountryAlreadySelected(check) {
    var arrayIds = countryId.split(',');
    for (var i = 0; i < arrayIds.length; i++) {
        if (arrayIds[i].trim() == check.id.trim()) {
            return true;
        }
    }
    return false;
}

function setCountryDetails() {    
    var selectedCountries = new Array();
    var Names = countryNames.split(',');
    var countryIds = countryId.split(',');
    for (var index = 0; index < countryIds.length; index++) {
        var newObject = new Object();
        newObject.Id = countryIds[index];
        newObject.Name = Names[index];
        selectedCountries.push(newObject);
    }
    return selectedCountries;
}



function isAlreadySelected(check) {
    var arrayIds = companyId.split(',');
    for (var i = 0; i < arrayIds.length; i++) {
        if (arrayIds[i].trim() == check.id.trim()) {
            return true;
        }
    }
    return false;
}

function setCompanyDetails() {
    // debugger;
    var selectedCompanies = new Array();
    var Names = companyNames.split(',');
    var companyIds = companyId.split(',');
    for (var index = 0; index < companyIds.length; index++) {
        var newObject = new Object();
        newObject.Id = companyIds[index];
        newObject.Name = Names[index];
        selectedCompanies.push(newObject);
    }
    return selectedCompanies;
}

/*******************Export****************************/

function ExportExcel() {
    $('#ExportType').val("Excel");
    //$('#LostRightDateMonth').val($('#LostRightDate').val());
    document.forms[0].submit();
}

/***********************************************/
$(window).bind("resize", resizeHandler);
