$(document).ready(function () {

    //alert("test");      
    showHideControlsOnLoad();
    SumSalesToDate();
    SumSalesWith();
    SumSalesWithout();
    SumRevToDate();
    SumRevWith();
    SumRevWithout();

    $.watermark.options.className = 'watermark';
    jQuery("#txtAPremiumComments").watermark("Premium Comments");
    jQuery("#txtAGiveAwayComments").watermark("Give away Free of Charge Comments");
    jQuery("#txtAOtherComments").watermark("Others Comments");

    //Start Asterisk display for BudgetInUSD fields
    if ($('#CurrencyList option:selected').text() != "USD") {
        $('#MandatoryTvBugetInUSD').show();
        $('#MandatoryRadioBugetInUSD').show();
        $('#MandatoryOtherBugetInUSD').show();
    }
    $('#CurrencyList').change(function () {    
        if ($('#CurrencyList option:selected').text() != "USD") {
            $('#MandatoryTvBugetInUSD').show();
            $('#MandatoryRadioBugetInUSD').show();
            $('#MandatoryOtherBugetInUSD').show();
        }
        else {
            $('#MandatoryTvBugetInUSD').hide();
            $('#MandatoryRadioBugetInUSD').hide();
            $('#MandatoryOtherBugetInUSD').hide();
        }
    });
    //End Asterisk display for BudgetInUSD fields
});

function moveCall() {
    if ($.browser.msie) {

        if ($.browser.msie && parseInt($.browser.version, 10) > 7) {
            $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "marginLeft": "2px", "marginTop": "-1px" });
        } else {
            $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "marginLeft": "2px", "marginTop": "-6px" });
        }

    } else {
        $(".ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "middle", "marginLeft": "2px", "marginTop": "0" });
    }
}
// visibilty of physical sales and revenue section when physical scope checkbox is selected
//$("#bodyRequest").ready(function () {    
//    $("#tdChkTVRadio").hide();
//    $("#tdChkPriceREd").hide();

//    $("#trTVRadioHeader").hide();
//    $("#trTVRadio").hide();

//    $("#trPriceRedHeader").hide();
//    $("#trPriceRed").hide();

//    $("#trClubHeader").hide();
//    $("#trClub").hide();

//    $("#trPromoHeader").hide();
//    $("#trPromo").hide();

//    $("#trNonTradHeader").hide();
//    $("#trNonTrad").hide();

//});

// visibilty of Tv radio price reduction checkbox when regular retail checkbox is selected
$("#tdChkReg").live("click", function () {   

    $("#tblRelease div").each(function () {
        var id = $(this).attr('id');
        if (id != null) {
            if (id.indexOf("trRegular") >= 0) {
                if (($("#chkRegularRetail").is(':checked')) && (($("#chkTVRadioBreakICLA").is(':checked') == false) && ($("#chkPriceReduction").is(':checked') == false))) {

                    $("#" + id + "Is_Regular").val(true);
                }
                else {

                    $("#" + id + "Is_Regular").val(false);
                }
            }
        }
    });

    if ($("#chkRegularRetail").is(':checked')) {

        //start - For removing highlight
        unwrapChkbox('#chkRegularRetail');
        unwrapChkbox('#chkClub');
        unwrapChkbox('#chkNonTrditional');
        unwrapChkbox('#chkPromotional');
        //end - For removing highlight

        $("#tdChkTVRadio").show();
        $("#tdChkPriceREd").show();

    }
    else {

        SetTVRadioDefaultVal();
        if ($('#hdnReleaseRowsCount').val() > 0) {
            for (k = 0; k < $('#hdnReleaseRowsCount').val(); k++) {
                $('#chkRegDeviatedICLA_' + k).attr('checked', false);
                $('#ddlRegPriceLevel_' + k).prop('selectedIndex', 0);
            }
        }

        $("#tdChkTVRadio").hide();
        $("#tdChkPriceREd").hide();


        $("#trTVRadioHeader").hide();
        $("#trTVRadio").hide();

        $("#trPriceRedHeader").hide();
        $("#trPriceRed").hide();

        $("#chkTVRadioBreakICLA").attr('checked', false);
        $("#chkPriceReduction").attr('checked', false);

        $("#chkTV").attr('checked', false);
        $("#chkRadio").attr('checked', false);
        $("#chkOthers").attr('checked', false);

    }

});

function SetTVRadioDefaultVal() {
    $("#txtDurationTo").val('');
    $("#txtDurationFrom").val('');
    $("#txtTVBudget").val('');
    $("#txtTVBudgetUSD").val('');
    $("#txtTVProdCost").val('');
    $("#txtRdoBudget").val('');
    $("#txtRdoBudgetUSD").val('');
    $("#txtRdoProdCost").val('');
    $("#txtOthrBudget").val('');
    $("#txtOthrBudgetUSD").val('');
    $("#txtOthrProdCost").val('');
    $("#txtOthrMedDetails").val('');
    $('#txtPSaleDate').val('');
    $('#txtPSaleWith').val('');
    $('#txtPSaleWithout').val('');
    $('#txtPRevDate').val('');
    $('#txtPRevWith').val('');
    $('#txtPRevWithout').val('');
    $('#txtDSaleDate').val('');
    $('#txtDSaleWith').val('');
    $('#txtDSaleWithout').val('');
    $('#txtDRevDate').val('');
    $('#txtDRevWith').val('');
    $('#txtDRevWithout').val('');
    $('#chkPhysicals').attr('checked', false);
    $('#chkALaCarteDownload').attr('checked', false);
    $('#chkSubscriptionDownload').attr('checked', false);
    $('#chkMobileRealTonesDownload').attr('checked', false);
    $('#chkMobileRingBackDownload').attr('checked', false);
    $('#chkStreaming').attr('checked', false);
    SetTotalSalesRevenueDefaultVal();
    $("#chkTV").attr('checked', false);
    $("#chkRadio").attr('checked', false);
    $("#chkOthers").attr('checked', false);
    if ($('#hdnReleaseRowsCount').val() > 0) {
        for (k = 0; k < $('#hdnReleaseRowsCount').val(); k++) {
            $('#txtExPPD_' + k).val('');
            $('#txtEstRetail_' + k).val('');
            $('#chkTVDeviatedICLA_' + k).attr('checked', false);          
            $('#ddlTVPriceLevel_'+k).prop('selectedIndex', 0);
        }
    }

}

// visibilty of TV/radio section when TV/radio checkbox is selected
$("#tdChkTVRadio").live("click", function () {


    $("#tblRelease div").each(function () {
        var id = $(this).attr('id');
        if (id != null) {

            if (id.indexOf("trRegular") >= 0) {
                if (($("#chkRegularRetail").is(':checked')) && (($("#chkTVRadioBreakICLA").is(':checked') == false) && ($("#chkPriceReduction").is(':checked') == false))) {

                    $("#" + id + "Is_Regular").val(true);
                }
                else {

                    $("#" + id + "Is_Regular").val(false);
                }
            }

            if (id.indexOf("trTV") >= 0) {
                if ($("#chkTVRadioBreakICLA").is(':checked')) {
                   

                    $("#" + id + "Is_TV").val(true);
                }
                else {
                    $("#" + id + "Is_TV").val(false);
                }
            }
        }
    });

    if ($("#chkTVRadioBreakICLA").is(':checked')) {

        $("#trTVRadioHeader").show();
        $("#trTVRadio").show();

        $("#tdChkTV").show();
        $("#trTVBudget").hide();
        $("#trTVBudgetUSD").hide();
        $("#trTVProdCost").hide();
        $("#txtTVProdCost").removeClass('input-validation-error');
        $("#txtTVBudgetUSD").removeClass('input-validation-error');
        $("#txtRdoProdCost").removeClass('input-validation-error');
        $("#txtRdoBudgetUSD").removeClass('input-validation-error');
        $("#txtOthrProdCost").removeClass('input-validation-error');
        $("#txtOthrBudgetUSD").removeClass('input-validation-error');


        $("#tdChkRadio").show();
        $("#trRdBudget").hide();
        $("#trRdBudgetUSD").hide();
        $("#trRdProdCost").hide();

        $("#tdChkOther").show();
        $("#trOthBudget").hide();
        $("#trOthBudgetUSD").hide();
        $("#trOthProdCost").hide();
        $("#trOthMedDetail").hide();

        showHidePhysicalSalesRevenue();
    }
    else {
        SetTVRadioDefaultVal();
        SetTotalSalesRevenueDefaultVal()
        $("#trTVRadioHeader").hide();
        $("#trTVRadio").hide();

    }

});




// visibilty of physical sales and revenue section when physical scope checkbox is selected
$("#tdChkPhysical").live("click", function () {
    if (($("#chkPhysical").is(':checked'))) {
        showHidePhysicalSalesRevenue();
        unwrapChkbox('#chkPhysical');
        unwrapChkbox('#chkDigital');
    }
    else {
        $('#txtPSaleDate').val('');
        $('#txtPSaleWith').val('');
        $('#txtPSaleWithout').val('');
        $('#txtPRevDate').val('');
        $('#txtPRevWith').val('');
        $('#txtPRevWithout').val('');
        SetTotalSalesRevenueDefaultVal()
        showHidePhysicalSalesRevenue();
     }


});

// visibilty of digital sales and revenue section when digital scope checkbox is selected
$("#tdChkDigital").live("click", function () {
    if (($("#chkDigital").is(':checked'))) {
        showHidePhysicalSalesRevenue();
        unwrapChkbox('#chkPhysical');
        unwrapChkbox('#chkDigital');
    }
    else {
        $('#txtDSaleDate').val('');
        $('#txtDSaleWith').val('');
        $('#txtDSaleWithout').val('');
        $('#txtDRevDate').val('');
        $('#txtDRevWith').val('');
        $('#txtDRevWithout').val('');
        SetTotalSalesRevenueDefaultVal()
        showHidePhysicalSalesRevenue();
    }

});

function SetTotalSalesRevenueDefaultVal() {
    $("#divTRevDate").text("");
    $("#divTRevWith").text("");
    $("#divTRevWithoute").text("");
    $('#divTSaleDate').text("");
    $('#divTSaleWith').text("");
    $('#divTSaleWithout').text("");
}

function showHideControlsOnLoad() {

    if (($("#chkPhysical").is(':checked')) && ($("#chkDigital").is(':checked'))) {
       
        $("#tdTVPhysicalSales").show();
        $("#tdTVPhysicalRev").show();

        $("#tdPSAdd").show();
        $("#tdPRAdd").show();

        $("#tdTVDigitalSales").show();
        $("#tdTVDigitalRev").show();

        $("#tdPSEqual").show();
        $("#tdPREqual").show();


        $("#tdTVTotalSales").show();
        $("#tdTVTotalRev").show();

    }
    else if ($("#chkPhysical").is(':checked')) {

        //col1
        $("#tdTVPhysicalSales").show();
        $("#tdTVPhysicalRev").show();
        //col2
        $("#tdPSAdd").hide();
        $("#tdPRAdd").hide();
        //col3
        $("#tdTVDigitalSales").hide();
        $("#tdTVDigitalRev").hide();
        //col4
        $("#tdPSEqual").hide();
        $("#tdPREqual").hide();
        //col5
        $("#tdTVTotalSales").hide();
        $("#tdTVTotalRev").hide();

    }
    else if ($("#chkDigital").is(':checked')) {

        
        $("#tdTVPhysicalSales").hide();
        $("#tdTVPhysicalRev").hide();

        $("#tdPSAdd").hide();
        $("#tdPRAdd").hide();

        $("#tdTVDigitalSales").show();
        $("#tdTVDigitalRev").show();

        $("#tdPSEqual").hide();
        $("#tdPREqual").hide();

        $("#tdTVTotalSales").hide();
        $("#tdTVTotalRev").hide();
    }
    else {
        

        $("#tdTVPhysicalSales").hide();
        $("#tdTVPhysicalRev").hide();

        $("#tdPSAdd").hide();
        $("#tdPRAdd").hide();

        $("#tdTVDigitalSales").hide();
        $("#tdTVDigitalRev").hide();

        $("#tdPSEqual").hide();
        $("#tdPREqual").hide();

        $("#tdTVTotalSales").hide();
        $("#tdTVTotalRev").hide();

        //        $("#tdTVPhysicalSales").hide();
        //        $("#tdTVPhysicalRev").hide();

        //        $("#tdPSAdd").hide();
        //        $("#tdPRAdd").hide();

        //        $("#tdTVDigitalSales").hide();
        //        $("#tdTVDigitalRev").hide();

        //        $("#tdPSEqual").hide();
        //        $("#tdPREqual").hide();

        //        $("#tdTVTotalSales").hide();
        //        $("#tdTVTotalRev").hide();

        //        $("#tdChkTVRadio").hide();
        //        $("#tdChkPriceREd").hide();

        //        $("#trTVRadioHeader").hide();
        //        $("#trTVRadio").hide();

        //        $("#trPriceRedHeader").hide();
        //        $("#trPriceRed").hide();

        //        $("#trClubHeader").hide();
        //        $("#trClub").hide();

        //        $("#trPromoHeader").hide();
        //        $("#trPromo").hide();

        //        $("#trNonTradHeader").hide();
        //        $("#trNonTrad").hide();
    }

    //If regular retail is selected
    if ($("#chkRegularRetail").is(':checked')) {

        $("#tdChkTVRadio").show();
        $("#tdChkPriceREd").show();
    }
    else {
        $("#tdChkTVRadio").hide();
        $("#tdChkPriceREd").hide();
    }

    //if club is selected
    if ($("#chkClub").is(':checked')) {

        $("#trClubHeader").show();
        $("#trClub").show();

    }
    else {
        $("#trClubHeader").hide();
        $("#trClub").hide();

    }

    //if non tradictional is selected
    if ($("#chkNonTrditional").is(':checked')) {

        $("#trNonTradHeader").show();
        $("#trNonTrad").show();

        if ($('#ManufacturedByUMG').val().toUpperCase() == 'YES') {
            $('#cmbManByUMG').val('Yes');
        }
        else if ($('#ManufacturedByUMG').val().toUpperCase() == 'NO') {
            $('#cmbManByUMG').val('No');
        }

        if ($("#chkPremium").is(':checked')) {

            $("#trPremuimCommnts")[0].style.visibility = "";
        }
        else {
            $("#trPremuimCommnts")[0].style.visibility = "hidden";
        }


        if ($("#chkGiveAwayFreeOfCharge").is(':checked')) {
            $("#trGivAwayCommnts")[0].style.visibility = "";
        }
        else {
            $("#trGivAwayCommnts")[0].style.visibility = "hidden";
        }

        if ($("#chkOther").is(':checked')) {
            $("#trOtherCommnts")[0].style.visibility = "";
        }
        else {
            $("#trOtherCommnts")[0].style.visibility = "hidden";
        }



    }
    else {
        $("#trNonTradHeader").hide();
        $("#trNonTrad").hide();

    }

    //if Promotinal is selected
    if ($("#chkPromotional").is(':checked')) {

        $("#trPromoHeader").show();
        $("#trPromo").show();

    }
    else {
        $("#trPromoHeader").hide();
        $("#trPromo").hide();

    }

    //If TV/ICLA is selected
    if ($("#chkTVRadioBreakICLA").is(':checked')) {
        DisablechkMultiArtist();
        $("#trTVRadioHeader").show();
        $("#trTVRadio").show();


        // if TV is checked in ICLABREAK
        if ($("#chkTV").is(':checked')) {
            $("#trTVBudget").show();
            $("#trTVBudgetUSD").show();
            $("#trTVProdCost").show();
            $('#tvRadioOtherGroup').removeClass('input-validation-error');
        }
        else {

            $("#trTVBudget").hide();
            $("#trTVBudgetUSD").hide();
            $("#trTVProdCost").hide();

        }


        // if Radio is checked in ICLABREAK
        if ($("#chkRadio").is(':checked')) {
            $("#trRdBudget").show();
            $("#trRdBudgetUSD").show();
            $("#trRdProdCost").show();
            $("#txtRdoBudget").removeClass('input-validation-error');
            $("#txtRdoProdCost").removeClass('input-validation-error');
            $("#txtRdoBudgetUSD").removeClass('input-validation-error');
            $('#tvRadioOtherGroup').removeClass('input-validation-error');
        }
        else {
            $("#trRdBudget").hide();
            $("#trRdBudgetUSD").hide();
            $("#trRdProdCost").hide();
        }

        // if Other is checked in ICLABREAK
        if ($("#chkOthers").is(':checked')) {
            $("#trOthBudget").show();
            $("#trOthBudgetUSD").show();
            $("#trOthProdCost").show();
            $("#trOthMedDetail").show();
            $("#txtOthrBudget").removeClass('input-validation-error');
            $("#txtOthrProdCost").removeClass('input-validation-error');
            $("#txtOthrBudgetUSD").removeClass('input-validation-error');
            $('#tvRadioOtherGroup').removeClass('input-validation-error');
        }
        else {
            $("#trOthBudget").hide();
            $("#trOthBudgetUSD").hide();
            $("#trOthProdCost").hide();
            $("#trOthMedDetail").hide();
        }


    }
    else {
        $("#trTVRadioHeader").hide();
        $("#trTVRadio").hide();

    }

    //If price reduction is selected
    if ($("#chkPriceReduction").is(':checked')) {

        $("#trPriceRedHeader").show();
        $("#trPriceRed").show();

    }
    else {
        $("#trPriceRedHeader").hide();
        $("#trPriceRed").hide();

    }

}

function showHidePhysicalSalesRevenue() {
    if (($("#chkPhysical").is(':checked')) && ($("#chkDigital").is(':checked'))) {
        $("#tdTVPhysicalSales").show();
        $("#tdTVPhysicalRev").show();

        $("#tdPSAdd").show();
        $("#tdPRAdd").show();

        $("#tdTVDigitalSales").show();
        $("#tdTVDigitalRev").show();

        $("#tdPSEqual").show();
        $("#tdPREqual").show();


        $("#tdTVTotalSales").show();
        $("#tdTVTotalRev").show();

    }
    else if ($("#chkPhysical").is(':checked')) {

        //col1
        $("#tdTVPhysicalSales").show();
        $("#tdTVPhysicalRev").show();
        //col2
        $("#tdPSAdd").hide();
        $("#tdPRAdd").hide();
        //col3
        $("#tdTVDigitalSales").hide();
        $("#tdTVDigitalRev").hide();
        //col4
        $("#tdPSEqual").hide();
        $("#tdPREqual").hide();
        //col5
        $("#tdTVTotalSales").hide();
        $("#tdTVTotalRev").hide();


    }
    else if ($("#chkDigital").is(':checked')) {

        $("#tdTVPhysicalSales").hide();
        $("#tdTVPhysicalRev").hide();

        $("#tdPSAdd").hide();
        $("#tdPRAdd").hide();

        $("#tdTVDigitalSales").show();
        $("#tdTVDigitalRev").show();

        $("#tdPSEqual").hide();
        $("#tdPREqual").hide();

        $("#tdTVTotalSales").hide();
        $("#tdTVTotalRev").hide();
    }
    else {

        $("#tdTVPhysicalSales").hide();
        $("#tdTVPhysicalRev").hide();

        $("#tdPSAdd").hide();
        $("#tdPRAdd").hide();

        $("#tdTVDigitalSales").hide();
        $("#tdTVDigitalRev").hide();

        $("#tdPSEqual").hide();
        $("#tdPREqual").hide();

        $("#tdTVTotalSales").hide();
        $("#tdTVTotalRev").hide();
    }
}

//********************************** TV/Radio Other checkbox click **********************************

// visibilty of TV section textbox when TV checkbox is selected
$("#chkTV").live("click", function () {

    if ($("#chkTV").is(':checked')) {
        $("#trTVBudget").show();
        $("#trTVBudgetUSD").show();
        $("#trTVProdCost").show();
        $('#tvRadioOtherGroup').removeClass('input-validation-error');
    }
    else {
        $("#txtTVBudget").val('');
        $("#txtTVBudgetUSD").val('');
        $("#txtTVProdCost").val('');
        $("#trTVBudget").hide();
        $("#trTVBudgetUSD").hide();
        $("#trTVProdCost").hide();

    }
});

// visibilty of Radio section textbox when Radio checkbox is selected
$("#chkRadio").live("click", function () {

    if ($("#chkRadio").is(':checked')) {
        $("#trRdBudget").show();
        $("#trRdBudgetUSD").show();
        $("#trRdProdCost").show();
        $('#tvRadioOtherGroup').removeClass('input-validation-error');
    }
    else {
        $("#txtRdoBudget").val('');
        $("#txtRdoBudgetUSD").val('');
        $("#txtRdoProdCost").val('');
        $("#trRdBudget").hide();
        $("#trRdBudgetUSD").hide();
        $("#trRdProdCost").hide();
    }
});

// visibilty of Other section textbox when Other checkbox is selected
$("#chkOthers").live("click", function () {
    if ($("#chkOthers").is(':checked')) {          
        $("#trOthBudget").show();
        $("#trOthBudgetUSD").show();
        $("#trOthProdCost").show();
        $("#trOthMedDetail").show();
        $('#tvRadioOtherGroup').removeClass('input-validation-error');
    }
    else {
        $("#txtOthrBudget").val('');
        $("#txtOthrBudgetUSD").val('');
        $("#txtOthrProdCost").val('');
        $("#txtOthrMedDetails").val('');
        
        $("#trOthBudget").hide();
        $("#trOthBudgetUSD").hide();
        $("#trOthProdCost").hide();
        $("#trOthMedDetail").hide();
    }
});
//********************************** TV/Radio Other checkbox click end**********************************


//**************************************** expand collapse arrow functionality ********************************************

// expandcollapse for scope and request type section on request type tab
$("#imgScopeHide").live("click", function () {
    $("#trScope").slideToggle("fast");

    var imgSrc = $("#imgScopeHide").attr("src");

    if (imgSrc.indexOf('Up') == -1) {
        $("#imgScopeHide").attr("src", "/GCS/Images/Up.png");
        $("#imgScopeHide").attr("title", "Expand");
    }
    else {

        $("#imgScopeHide").attr("src", "/GCS/Images/Down.png");
        $("#imgScopeHide").attr("title", "Collapse");
    }
    return false;
});

// expandcollapse for TV/Radio section on request type tab

$("#imgTVHide").live("click", function () {
    $("#trTVRadio").slideToggle("fast");

    var imgSrc = $("#imgTVHide").attr("src");

    if (imgSrc.indexOf('Up') == -1) {
        $("#imgTVHide").attr("src", "/GCS/Images/Up.png");
        $("#imgTVHide").attr("title", "Expand");
    }
    else {

        $("#imgTVHide").attr("src", "/GCS/Images/Down.png");
        $("#imgTVHide").attr("title", "Collapse");
    }
    return false;
});

// expandcollapse for Price reduction section on request type tab

$("#imgPriceRed").live("click", function () {
    $("#trPriceRed").slideToggle("fast");

    var imgSrc = $("#imgPriceRed").attr("src");

    if (imgSrc.indexOf('Up') == -1) {
        $("#imgPriceRed").attr("src", "/GCS/Images/Up.png");
        $("#imgPriceRed").attr("title", "Expand");
    }
    else {

        $("#imgPriceRed").attr("src", "/GCS/Images/Down.png");
        $("#imgPriceRed").attr("title", "Collapse");
    }
    return false;
});
// expandcollapse for club section on request type tab

$("#imgClub").live("click", function () {
    $("#trClub").slideToggle("fast");

    var imgSrc = $("#imgClub").attr("src");

    if (imgSrc.indexOf('Up') == -1) {
        $("#imgClub").attr("src", "/GCS/Images/Up.png");
        $("#imgClub").attr("title", "Expand");
    }
    else {

        $("#imgClub").attr("src", "/GCS/Images/Down.png");
        $("#imgClub").attr("title", "Collapse");
    }
    return false;
});
// expandcollapse for promotional section on request type tab

$("#imgPromo").live("click", function () {
    $("#trPromo").slideToggle("fast");

    var imgSrc = $("#imgPromo").attr("src");

    if (imgSrc.indexOf('Up') == -1) {
        $("#imgPromo").attr("src", "/GCS/Images/Up.png");
        $("#imgPromo").attr("title", "Expand");
    }
    else {

        $("#imgPromo").attr("src", "/GCS/Images/Down.png");
        $("#imgPromo").attr("title", "Collapse");
    }
    return false;
});

// expandcollapse for non traditional section on request type tab

$("#imgNonTrad").live("click", function () {
    $("#trNonTrad").slideToggle("fast");

    var imgSrc = $("#imgNonTrad").attr("src");

    if (imgSrc.indexOf('Up') == -1) {
        $("#imgNonTrad").attr("src", "/GCS/Images/Up.png");
        $("#imgNonTrad").attr("title", "Expand");
    }
    else {

        $("#imgNonTrad").attr("src", "/GCS/Images/Down.png");
        $("#imgNonTrad").attr("title", "Collapse");
    }
    return false;
});

//**************************************** expand collapse arrow functionality end ********************************************




// visibilty of price reduction section when price reduction checkbox is selected
$("#chkPriceReduction").live("click", function () {


    $("#tblRelease div").each(function () {
        var id = $(this).attr('id');
        if (id != null) {

            if (id.indexOf("trRegular") >= 0) {
                if (($("#chkRegularRetail").is(':checked')) && (($("#chkTVRadioBreakICLA").is(':checked') == false) && ($("#chkPriceReduction").is(':checked') == false))) {

                    $("#" + id + "Is_Regular").val(true);
                }
                else {

                    $("#" + id + "Is_Regular").val(false);
                }
            }

            if (id.indexOf("trPrice") >= 0) {
                if ($("#chkPriceReduction").is(':checked')) {
                    
                    $("#" + id + "Is_Price").val(true);
                }
                else {
                    $("#" + id + "Is_Price").val(false);
                }
            }
        }
    });

    if ($("#chkPriceReduction").is(':checked')) {

        $("#trPriceRedHeader").show();
        $("#trPriceRed").show();
    }
    else {

        $("#trPriceRedHeader").hide();
        $("#trPriceRed").hide();
    }

});



// visibilty of club section when club checkbox is selected
$("#chkClub").live("click", function () { 

    $("#tblRelease div").each(function () {
        var id = $(this).attr('id');
        if (id != null) {
            if (id.indexOf("trClub") >= 0) {
                if ($("#chkClub").is(':checked')) {
                   
                    $("#" + id + "Is_Club").val(true);
                }
                else {
                    $("#" + id + "Is_Club").val(false);
                }
            }
        }
    });

    if ($("#chkClub").is(':checked')) {

        //start - For removing highlight
        unwrapChkbox('#chkRegularRetail');
        unwrapChkbox('#chkClub');
        unwrapChkbox('#chkNonTrditional');
        unwrapChkbox('#chkPromotional');
        //End - For removing highlight

        $("#trClubHeader").show();
        $("#trClub").show();
    }
    else {
        if ($('#hdnReleaseRowsCount').val() > 0) {
            for (k = 0; k < $('#hdnReleaseRowsCount').val(); k++) {
                $('#chkClubDeviatedICLA_' + k).attr('checked', false);
            }
        }
        $("#trClubHeader").hide();
        $("#trClub").hide();
        //Unchecking the respective section checkboxes
        $("#chkAdditionalMailOrder").attr('checked', false);
        $("#chkIntroductoryUse").attr('checked', false);        
    }
});

// visibilty of promotional section when promotional checkbox is selected
$("#chkPromotional").live("click", function () {


    $("#tblRelease div").each(function () {
        var id = $(this).attr('id');
        if (id != null) {
            if (id.indexOf("trPromo") >= 0) {
                if ($("#chkPromotional").is(':checked')) {

                    $("#" + id + "Is_Promo").val(true);
                }
                else {
                    $("#" + id + "Is_Promo").val(false);
                }
            }
        }
    });

    if ($("#chkPromotional").is(':checked')) {
        //start - For removing highlight
        unwrapChkbox('#chkRegularRetail');
        unwrapChkbox('#chkClub');
        unwrapChkbox('#chkNonTrditional');
        unwrapChkbox('#chkPromotional');
        //End - For removing highlight

        $("#trPromoHeader").show();
        $("#trPromo").show();
    }
    else {
        if ($('#hdnReleaseRowsCount').val() > 0) {
            for (k = 0; k < $('#hdnReleaseRowsCount').val(); k++) {
                $('#chkPromoDeviatedICLA_' + k).attr('checked', false);
            }
        }
        $("#trPromoHeader").hide();
        $("#trPromo").hide();
        $("#txtDistTo").val('');        
    }

});

// visibilty of non traditional section when non traditional checkbox is selected
$("#chkNonTrditional").live("click", function () {

    $("#tblRelease div").each(function () {
        var id = $(this).attr('id');
        if (id != null) {
            if (id.indexOf("trNon") >= 0) {
                if ($("#chkNonTrditional").is(':checked')) {

                    $("#" + id + "Is_Non").val(true);
                }
                else {
                    $("#" + id + "Is_Non").val(false);
                }
            }
        }
    });

    if ($("#chkNonTrditional").is(':checked')) {

        //start - For removing highlight
        unwrapChkbox('#chkRegularRetail');
        unwrapChkbox('#chkClub');
        unwrapChkbox('#chkNonTrditional');
        unwrapChkbox('#chkPromotional');
        //End - For removing highlight

        $("#trNonTradHeader").show();
        $("#trNonTrad").show();

        $("#trPremuimCommnts")[0].style.visibility = "hidden";
        $("#trGivAwayCommnts")[0].style.visibility = "hidden";
        $("#trOtherCommnts")[0].style.visibility = "hidden";
    }
    else {
        $("#trNonTradHeader").hide();
        $("#trNonTrad").hide();
        $("#chkPremium").attr('checked', false);
        $("#chkGiveAwayFreeOfCharge").attr('checked', false);
        $("#chkOther").attr('checked', false);
        $("#txtAPremiumComments").val('');
        $("#txtAGiveAwayComments").val('');
        $("#txtAOtherComments").val('');

        //        if ($('#hdnReleaseRowsCount').val() > 0) {
        //            for (k = 0; k < $('#hdnReleaseRowsCount').val(); k++) {
        //                $('.radioICLAclass' + k).attr('checked', false);
        //                $('#rdoNonTrad2' + k).attr('checked', false);                                
        //            }
        //        }
        $('.radioICLAclass').attr('checked', false);
        $('.radioSuggestedclass').attr('checked', false);
        if ($('#hdnReleaseRowsCount').val() > 0) {
            for (k = 0; k < $('#hdnReleaseRowsCount').val(); k++) {
                $("#txtSellPrice_" + k).val('');
                $("#chkNonTradDeviatedICLA_" + k).attr('checked', false);
                $("#txtNonTradAreaComments_" + k).val('');
                $('#ddlNonICLALevel_' + k).prop('selectedIndex', 0);
                $("#txtICLAcc_" + k).val('');
                $("#txtResourceFee_" + k).val('');
            }
        }


        //Removing all the data when parent is unchecked
        
        $("#txtClientName").val('');
        $("#txtAMedPromoComment").val('');
        $("#cmbManByUMG").prop('selectedIndex', 0);
        $("#txtClientWebsite").val('');        

        $("#tblNonTrad input[type=checkbox]").each(function () {        
            var id = $(this).attr('id');
            if (id != null) {
                if (id.indexOf("chk") >= 0) {
                    $("#" + id).attr('checked', false);
                }               
            }
        });
        //Removing all the data ends here
    }

});


// visibilty of comments in non traditional section when premium checkbox is selected
$("#chkPremium").live("click", function () {


    if ($("#chkPremium").is(':checked')) {       
        //  $("#trPremuimCommnts").show();
        $("#trPremuimCommnts")[0].style.visibility = "";
        
    }
    else {
        $("#txtAPremiumComments").val('');
        $("#trPremuimCommnts")[0].style.visibility = "hidden";
    }

});

$("#chkGiveAwayFreeOfCharge").live("click", function () {

    if ($("#chkGiveAwayFreeOfCharge").is(':checked')) {
        // $("#trGivAwayCommnts").show();
        $("#trGivAwayCommnts")[0].style.visibility = "";
    }
    else {
        $("#txtAGiveAwayComments").val('');
        $("#trGivAwayCommnts")[0].style.visibility = "hidden";
    }

});

$("#chkOther").live("click", function () {


    if ($("#chkOther").is(':checked')) {
        $("#trOtherCommnts")[0].style.visibility = "";
    }
    else {
        $("#txtAOtherComments").val('');      
        $("#trOtherCommnts")[0].style.visibility = "hidden";
    }

});

$('#cmbManByUMG').live("change", function () {

    if ($(this).val() == "Yes") {
        $("#ManufacturedByUMG").val('Yes');
    }
    else if ($(this).val() == "No") {
        $("#ManufacturedByUMG").val('No');
    }
});


// Sum the Textbox value Function for Physical Sales Split & Digital Sales Split:
//Row:1
$("#txtPSaleDate").live("blur", function () {

    SumSalesToDate();


});

function SumSalesToDate() {
    var $nFirstNum = 0;
    var $nSecondNum = 0;
    $("#divTSaleDate").text("");
    if (($('#txtPSaleDate').val() != null) && ($('#txtPSaleDate').val() != "")) {

        $nFirstNum = parseInt($('#txtPSaleDate').val(), 10);
    }
    if (($('#txtDSaleDate').val() != null) && ($('#txtDSaleDate').val() != "")) {

        $nSecondNum = parseInt($('#txtDSaleDate').val(), 10);

    }

    var $nResult = $nFirstNum + $nSecondNum
    $("#divTSaleDate").text($nResult);
    if ($nFirstNum != null || $nSecondNum != null) {
        if (($nSecondNum != 0 || $nFirstNum != 0)) {
            //            var $nResult = parseFloat($nFirstNum + $nSecondNum).toFixed(2);
            var $nResult = parseFloat($nFirstNum + $nSecondNum);
            $("#divTSaleDate").text($nResult);
        }
    }
}
function SumSalesWith() {
    var $nFirstNum = 0;
    var $nSecondNum = 0;
    $("#divTSaleWith").text("");
    if (($('#txtPSaleWith').val() != null) && ($('#txtPSaleWith').val() != "")) {
        $nFirstNum = parseInt($('#txtPSaleWith').val(), 10);
    }
    if (($('#txtDSaleWith').val() != null) && ($('#txtDSaleWith').val() != "")) {
        $nSecondNum = parseInt($('#txtDSaleWith').val(), 10);
    }
    var $nResult = $nFirstNum + $nSecondNum
//    if (isNaN($nResult)) {
//        $nResult = "";
//    }
    if ($nFirstNum != null || $nSecondNum != null) {
        if (($nSecondNum != 0 || $nFirstNum != 0)) {
            //            var $nResult = parseFloat($nFirstNum + $nSecondNum).toFixed(2);
            var $nResult = parseFloat($nFirstNum + $nSecondNum);
            $("#divTSaleWith").text($nResult);
        }
    }
    //$("#divTSaleWith").text($nResult);
}

$("#txtDSaleDate").live("blur", function () {
    SumSalesToDate();
});
//Row:2
$("#txtPSaleWith").live("blur", function () {
    SumSalesWith();
});

$("#txtDSaleWith").live("blur", function () {
    SumSalesWith();
});
//Row:3
function SumSalesWithout() {
    var $nFirstNum = 0;
    var $nSecondNum = 0;
    $("#divTSaleWithout").text("");
    if (($('#txtPSaleWithout').val() != null) && ($('#txtPSaleWithout').val() != "")) {
        $nFirstNum = parseInt($('#txtPSaleWithout').val(), 10);
    }
    if (($('#txtDSaleWithout').val() != null) && ($('#txtDSaleWithout').val() != "")) {
        $nSecondNum = parseInt($('#txtDSaleWithout').val(), 10);
    }
    var $nResult = $nFirstNum + $nSecondNum

    if ($nFirstNum != null || $nSecondNum != null) {
        if (($nSecondNum != 0 || $nFirstNum != 0)) {
            //            var $nResult = parseFloat($nFirstNum + $nSecondNum).toFixed(2);
            var $nResult = parseFloat($nFirstNum + $nSecondNum);
            $("#divTSaleWithout").text($nResult);
        }
    }
    //$("#divTSaleWithout").text($nResult);
}

$("#txtPSaleWithout").live("blur", function () {
    SumSalesWithout();

});

$("#txtDSaleWithout").live("blur", function () {
    SumSalesWithout();
    //    var $nFirstNum = 0;
    //    var $nSecondNum = 0;

    //    if (($('#txtPSaleWithout').val() != null) && ($('#txtPSaleWithout').val() != "")) {
    //        $nFirstNum = parseInt($('#txtPSaleWithout').val())
    //    }
    //    if (($('#txtDSaleWithout').val() != null) && ($('#txtDSaleWithout').val() != "")) {
    //        $nSecondNum = parseInt($('#txtDSaleWithout').val())
    //    }
    //    var $nResult = $nFirstNum + $nSecondNum
    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }
    //    $("#divTSaleWithout").text($nResult);
});


// Sum the Textbox value Function for Physical Revenue Split  & Digital Revenue Split
//Row:1
function SumRevToDate() {
    var $nFirstNum = 0;
    var $nSecondNum = 0;
    $("#divTRevDate").text("");

    if (($('#txtPRevDate').val() != null) && ($('#txtPRevDate').val() != "")) {
        $nFirstNum = parseFloat($('#txtPRevDate').val());
    }
    if (($('#txtDRevDate').val() != null) && ($('#txtDRevDate').val() != "")) {
        $nSecondNum = parseFloat($('#txtDRevDate').val());
    }

    if ($nFirstNum != null || $nSecondNum != null) {
        if (($nSecondNum != 0 || $nFirstNum != 0)) {
            var $nResult = parseFloat($nFirstNum + $nSecondNum).toFixed(2);
            $("#divTRevDate").text($nResult);
        }
    }



}
function SumRevWith() {
    var $nFirstNum = 0;
    var $nSecondNum = 0;
    $("#divTRevWith").text("");
    if (($('#txtPRevWith').val() != null) && ($('#txtPRevWith').val() != "")) {
        $nFirstNum = parseFloat($('#txtPRevWith').val(), 10);
    }
    if (($('#txtDRevWith').val() != null) && ($('#txtDRevWith').val() != "")) {
        $nSecondNum = parseFloat($('#txtDRevWith').val(), 10);
    }
    if ($nFirstNum != null || $nSecondNum != null) {
        if (($nSecondNum != 0 || $nFirstNum != 0)) {
            var $nResult = parseFloat($nFirstNum + $nSecondNum).toFixed(2);
            $("#divTRevWith").text($nResult);
        }
    }
    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }

}
function SumRevWithout() {
    var $nFirstNum = 0;
    var $nSecondNum = 0;
    $("#divTRevWithoute").text("");
    if (($('#txtPRevWithout').val() != null) && ($('#txtPRevWithout').val() != "")) {
        $nFirstNum = parseInt($('#txtPRevWithout').val(),10)
    }
    if (($('#txtDRevWithout').val() != null) && ($('#txtDRevWithout').val() != "")) {
        $nSecondNum = parseInt($('#txtDRevWithout').val(),10)
    }
    if ($nFirstNum != null || $nSecondNum != null) {
        if (($nSecondNum != 0 || $nFirstNum != 0)) {
            var $nResult = parseFloat($nFirstNum + $nSecondNum).toFixed(2);
            $("#divTRevWithoute").text($nResult);
        }
    }



    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }

}

$("#txtPRevDate").live("blur", function () {
    SumRevToDate();
});

$("#txtDRevDate").live("blur", function () {

    //    var $nFirstNum = 0;
    //    var $nSecondNum = 0;

    //    if (($('#txtPRevDate').val() != null) && ($('#txtPRevDate').val() != "")) {
    //        $nFirstNum = parseInt($('#txtPRevDate').val())
    //    }
    //    if (($('#txtDRevDate').val() != null) && ($('#txtDRevDate').val() != "")) {
    //        $nSecondNum = parseInt($('#txtDRevDate').val())
    //    }
    //    var $nResult = $nFirstNum + $nSecondNum
    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }
    //    $("#divTRevDate").text($nResult);
    SumRevToDate();
});
//Row:2
$("#txtPRevWith").live("blur", function () {

    //    var $nFirstNum = 0;
    //    var $nSecondNum = 0;

    //    if (($('#txtPRevWith').val() != null) && ($('#txtPRevWith').val() != "")) {
    //        $nFirstNum = parseInt($('#txtPRevWith').val())
    //    }
    //    if (($('#txtDRevWith').val() != null) && ($('#txtDRevWith').val() != "")) {
    //        $nSecondNum = parseInt($('#txtDRevWith').val())
    //    }
    //    var $nResult = $nFirstNum + $nSecondNum
    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }
    //    $("#divTRevWith").text($nResult);
    SumRevWith();

});

$("#txtDRevWith").live("blur", function () {
    SumRevWith();
    //    var $nFirstNum = 0;
    //    var $nSecondNum = 0;

    //    if (($('#txtPRevWith').val() != null) && ($('#txtPRevWith').val() != "")) {
    //        $nFirstNum = parseInt($('#txtPRevWith').val())
    //    }
    //    if (($('#txtDRevWith').val() != null) && ($('#txtDRevWith').val() != "")) {
    //        $nSecondNum = parseInt($('#txtDRevWith').val())
    //    }
    //    var $nResult = $nFirstNum + $nSecondNum
    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }
    //    $("#divTRevWith").text($nResult);
});
//Row:3
$("#txtPRevWithout").live("blur", function () {

    //    var $nFirstNum = 0;
    //    var $nSecondNum = 0;

    //    if (($('#txtPRevWithout').val() != null) && ($('#txtPRevWithout').val() != "")) {
    //        $nFirstNum = parseInt($('#txtPRevWithout').val())
    //    }
    //    if (($('#txtDRevWithout').val() != null) && ($('#txtDRevWithout').val() != "")) {
    //        $nSecondNum = parseInt($('#txtDRevWithout').val())
    //    }
    //    var $nResult = $nFirstNum + $nSecondNum
    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }
    //    $("#divTRevWithoute").text($nResult);
    SumRevWithout();
});

$("#txtDRevWithout").live("blur", function () {
    //    var $nFirstNum = 0;
    //    var $nSecondNum = 0;

    //    if (($('#txtPRevWithout').val() != null) && ($('#txtPRevWithout').val() != "")) {
    //        $nFirstNum = parseInt($('#txtPRevWithout').val())
    //    }
    //    if (($('#txtDRevWithout').val() != null) && ($('#txtDRevWithout').val() != "")) {
    //        $nSecondNum = parseInt($('#txtDRevWithout').val())
    //    }
    //    var $nResult = $nFirstNum + $nSecondNum
    //    if (isNaN($nResult)) {
    //        $nResult = "0";
    //    }
    //    $("#divTRevWithoute").text($nResult);
    SumRevWithout();
});

//Calendar Script
$(function () {

    //    var pickerOpts = {
    //        dateFormat: "dd/m/yy", 
    //        showOn: "button",
    //        buttonImage: "/GCS/images/Calender_Icon_img.png",         
    //        buttonImageOnly: true
    //    };
    //    $("#txtDurationFrom").datepicker(pickerOpts);
    //    $("#txtDurationTo").datepicker(pickerOpts);

    $("#txtDurationFrom").datepicker({
        dateFormat: "dd/mm/yy",
        showOn: "button",
 
        buttonImage: "/GCS/images/GCS_Calender_Icon_img.png",
        buttonImageOnly: true,
        height: '12px',
 

        onSelect: function (selected) {
            selected = selected.split('/');
            //selected[0] = parseInt(selected[0]) + 1;
            temp = selected[0];
            selected[0] = selected[1];
            selected[1] = temp;
            selected = selected.join('/');
            var actualDate = new Date(selected);
             
            var newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate());

            $("#txtDurationTo").datepicker("option", "minDate", newDate);
            moveCall();
            $("#txtDurationFrom").removeClass('input-validation-error');
        }
    });
    $("#txtDurationTo").datepicker({
        dateFormat: "dd/mm/yy",
        showOn: "button",
        buttonImage: "/GCS/images/GCS_Calender_Icon_img.png",
        buttonImageOnly: true,
        height: '12px',

        onSelect: function (selected) {
            selected = selected.split('/');
            //            selected[0] = parseInt(selected[0]) - 1;
            temp = selected[0];
            selected[0] = selected[1];
            selected[1] = temp;
            selected = selected.join('/');
            var actualDate = new Date(selected);
            var newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate());
            //$("#txtDurationFrom").datepicker("option", "maxDate", newDate);
            moveCall();
            $("#txtDurationTo").removeClass('input-validation-error');
        }
    });


    moveCall();
});

// Resetting the datepicker textboxes on deleting date in textbox
function txtDurationFromToChange(id, e) {
    //if ($(id).val() == "") {    
    var dateString = $(id).val();
    if (!/^\d{2}\/\d{2}\/\d{4}$/.test(dateString)) {
        $(id).val('');
        $("#txtDurationFrom").datepicker("option", {
            minDate: null,
            maxDate: null
        });

        $("#txtDurationTo").datepicker("option", {
            minDate: null,
            maxDate: null
        });

        $("#txtDurationFrom").css('vertical-align', 'top');
        $("#txtDurationFrom").css('margin-right', '2px');
        $("#txtDurationTo").css('vertical-align', 'top');
        $("#txtDurationTo").css('margin-right', '2px');
    }
    else {
        return false;        
    }
}

$("#txtDurationFrom").change(function () {    
    var startDt = document.getElementById("txtDurationFrom").value;
    var endDt = document.getElementById("txtDurationTo").value;
    startDt = startDt.split('/');
    temp = startDt[0];
    startDt[0] = startDt[1];
    startDt[1] = temp;
    startDt = startDt.join('/');

    endDt = endDt.split('/');
    temp = endDt[0];
    endDt[0] = endDt[1];
    endDt[1] = temp;
    endDt = endDt.join('/');
    if ((new Date(startDt).getTime() > new Date(endDt).getTime())) {
        alert("From date cannot be greater than the To Date.");
        $("#txtDurationFrom").val("");

    }
});
$("#txtDurationTo").change(function () {
    var startDt = document.getElementById("txtDurationFrom").value;
    var endDt = document.getElementById("txtDurationTo").value;
    startDt = startDt.split('/');
    temp = startDt[0];
    startDt[0] = startDt[1];
    startDt[1] = temp;
    startDt = startDt.join('/');

    endDt = endDt.split('/');
    temp = endDt[0];
    endDt[0] = endDt[1];
    endDt[1] = temp;
    endDt = endDt.join('/');

    if ((new Date(startDt).getTime() > new Date(endDt).getTime())) {
        alert("From date cannot be greater than the To Date.");
        $("#txtDurationTo").val("");
    }
});

//To enable/disabled the chkPriceReduction checkbox on cboReleaseType change event.
$('#cboReleaseType').change(function (e) {
    selReleaseType = $("#cboReleaseType option:selected").text();
    if (selReleaseType.match('Existing')) {
        $('#chkPriceReduction').removeAttr('disabled');
    }
    else {
        $('#chkPriceReduction').attr('disabled', 'disabled');
    }
});

//If chkTVRadioBreakICLA is checked then disabled the Multi Artist Checkbox from the ProjectInformation Tab.
$('#chkTVRadioBreakICLA').click(function (e) {
    
    if ($("#chkTVRadioBreakICLA").is(':checked')) {
        $('#chkMultiArtist').attr('disabled', 'disabled');

    }
    else {
        $('#chkMultiArtist').removeAttr('disabled');
    }
});



//Required Field Validation on click os Save Button
$('#btnSave').click(function () {

    //    var IsValid = true;
    //    var ControlToFocus = "";
    //    var FocusFound = false;

    //    $("#divErrorMessage").text(""); //Set the Validation message blank

    //    if ($("#chkPhysical").is(':checked') || $("#chkDigital").is(':checked')) {
    //        $('#tdScope').removeClass('input-validation-error');
    //        //$('#chkPhysical').removeClass('input-validation-error');
    //        //$('#chkDigital').removeClass('input-validation-error');
    //    }
    //    else {


    //        $('#tdScope').addClass('input-validation-error');
    //        //$('#chkPhysical').addClass('input-validation-error');
    //        //$('#chkDigital').addClass('input-validation-error');
    //        //$("#divErrorMessage").text(mandatoryFields);

    //        IsValid = false;
    //        if (FocusFound == false) {
    //            ControlToFocus = "#chkPhysical";
    //        }
    //        FocusFound = true;


    //    }


    //    if ($("#chkRegularRetail").is(':checked') || $("#chkClub").is(':checked') || $("#chkNonTrditional").is(':checked') || $("#chkPromotional").is(':checked')) {

    //        $('#tdRequestType').removeClass('input-validation-error');
    //        //         $('#chkRegularRetail').removeClass('input-validation-error');
    //        //         $('#chkClub').removeClass('input-validation-error');
    //        //         $('#chkNonTrditional').removeClass('input-validation-error');
    //        //         $('#chkPromotional').removeClass('input-validation-error');
    //    }
    //    else {
    //        $('#chkRegularRetail').focus();
    //        $('#tdRequestType').addClass('input-validation-error');
    //        //         $('#chkRegularRetail').addClass('input-validation-error');
    //        //         $('#chkClub').addClass('input-validation-error');
    //        //         $('#chkNonTrditional').addClass('input-validation-error');
    //        //         $('#chkPromotional').addClass('input-validation-error');

    //        IsValid = false;
    //        if (FocusFound == false) {
    //            ControlToFocus = "#chkRegularRetail";
    //        }
    //        FocusFound = true;
    //    }


    //    // TV/Radio Break ICLA 
    //    if ($("#chkTVRadioBreakICLA").is(':checked')) {
    //        if (ValidateMandatoryFields('txtDurationFrom') == false) {

    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDurationFrom";
    //            }
    //            FocusFound = true;
    //        }
    //        if (ValidateMandatoryFields('txtDurationTo') == false) {

    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDurationTo";
    //            }
    //            FocusFound = true;
    //        }
    //    }

    //    //Alteast one should be selected : TV or Radio or Other, 
    //    if ($("#chkTVRadioBreakICLA").is(':checked')) {
    //        if ($("#chkTV").is(':checked') || $("#chkRadio").is(':checked') || $("#chkOthers").is(':checked')) {
    //            $('#chkTV').removeClass('input-validation-error');
    //            $('#chkRadio').removeClass('input-validation-error');
    //            $('#chkOthers').removeClass('input-validation-error');

    //        }
    //        else {

    //            $('#chkTV').addClass('input-validation-error');
    //            $('#chkRadio').addClass('input-validation-error');
    //            $('#chkOthers').addClass('input-validation-error');
    //            IsValid = false;

    //        }
    //    }

    //    //if TV checkbox is selected 
    //    if ($("#chkTV").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {
    //        if (ValidateMandatoryFields('txtTVBudget') == false) {

    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtTVBudget";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtTVBudgetUSD') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtTVBudgetUSD";
    //            }
    //            FocusFound = true;
    //        }
    //    }

    //    //if Radio checkbox is selected 
    //    if ($("#chkRadio").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

    //        if (ValidateMandatoryFields('txtRdoBudget') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtRdoBudget";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtRdoBudgetUSD') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtRdoBudgetUSD";
    //            }
    //            FocusFound = true;
    //        }

    //    }

    //    //if Other checkbox is selected 
    //    if ($("#chkOthers").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

    //        if (ValidateMandatoryFields('txtOthrBudget') == false) {

    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtOthrBudget";
    //            }
    //            FocusFound = true;
    //        }


    //        if (ValidateMandatoryFields('txtOthrBudgetUSD') == false) {
    //            $('#txtOthrBudgetUSD').focus();
    //            IsValid = false;
    //        }

    //        if (ValidateMandatoryFields('txtOthrMedDetails') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtOthrMedDetails";
    //            }
    //            FocusFound = true;
    //        }
    //    }


    //    //Sale Channel
    //    if ($("#chkTVRadioBreakICLA").is(':checked')) {
    //        if ($("#chkPhysicals").is(':checked') || $("#chkALaCarteDownload").is(':checked') || $("#chkSubscriptionDownload").is(':checked') || $("#chkMobileRealTonesDownload").is(':checked') || $("#chkMobileRingBackDownload").is(':checked') || $("#chkPromotional").is(':checked')) {
    //            $('#tblSaleChannel').removeClass('input-validation-error');
    //        }
    //        else {
    //            $('#tblSaleChannel').addClass('input-validation-error');
    //            $('#tblSaleChannel').focus();
    //            IsValid = false;
    //        }
    //    }


    //    //If chkPhysical is selected then validate Physical Sales Split & Physical Revenue Split.
    //    if ($("#chkPhysical").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

    //        if (ValidateMandatoryFields('txtPSaleDate') == false) {

    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtPSaleDate";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtPSaleWith') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtPSaleWith";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtPSaleWithout') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtPSaleWithout";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtPRevDate') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtPRevDate";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtPRevWith') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtPRevWith";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtPRevWithout') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtPRevWithout";
    //            }
    //            FocusFound = true;
    //        }
    //    }


    //    //If chkDigital is selected then validate Digital Sales Split & Digital Revenue Split.
    //    if ($("#chkDigital").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

    //        if (ValidateMandatoryFields('txtDSaleDate') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDSaleDate";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtDSaleWith') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDSaleWith";
    //            }
    //            FocusFound = true;
    //        }


    //        if (ValidateMandatoryFields('txtDSaleWithout') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDSaleWithout";
    //            }
    //            FocusFound = true;
    //        }


    //        if (ValidateMandatoryFields('txtDRevDate') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDRevDate";
    //            }
    //            FocusFound = true;
    //        }

    //        if (ValidateMandatoryFields('txtDRevWith') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDRevWith";
    //            }
    //            FocusFound = true;
    //        }


    //        if (ValidateMandatoryFields('txtDRevWithout') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDRevWithout";
    //            }
    //            FocusFound = true;
    //        }
    //    }

    //    //Price Reduction
    //    if ($("#chkPriceReduction").is(':checked')) {
    //        selRoleTypeText = $("#cboCurrentPriceList option:selected").text();
    //        if (selRoleTypeText.match('--Select Role--')) {
    //            $('#cboCurrentPriceList').addClass('input-validation-error');
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#cboCurrentPriceList";
    //            }
    //            FocusFound = true;
    //        }
    //        else {
    //            $('#cboCurrentPriceList').removeClass('input-validation-error');
    //        }

    //        selRoleTypeText = $("#cboRequestPriceList option:selected").text();
    //        if (selRoleTypeText.match('--Select Role--')) {
    //            $('#cboRequestPriceList').addClass('input-validation-error');
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#cboRequestPriceList";
    //            }
    //            FocusFound = true;
    //        }
    //        else {
    //            $('#cboRequestPriceList').removeClass('input-validation-error');
    //        }
    //    }


    //    //If chkPromotional selected
    //    if ($("#chkPromotional").is(':checked')) {

    //        if (ValidateMandatoryFields('txtDistTo') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtDistTo";
    //            }
    //            FocusFound = true;
    //        }
    //    }


    //    //If chkNonTrditional selected 
    //    if ($("#chkNonTrditional").is(':checked')) {

    //        if (ValidateMandatoryFields('txtClientName') == false) {
    //            IsValid = false;
    //            if (FocusFound == false) {
    //                ControlToFocus = "#txtClientName";
    //            }
    //            FocusFound = true;
    //        }

    //        //If chkNonTrditional selected with Premium checkbox
    //        if ($("#chkNonTrditional").is(':checked') && $("#chkPremium").is(':checked')) {

    //            if (ValidateMandatoryFields('txtAPremiumComments') == false) {
    //                IsValid = false;
    //                if (FocusFound == false) {
    //                    ControlToFocus = "#txtAPremiumComments";
    //                }
    //                FocusFound = true;
    //            }
    //        }


    //        if ($("#chkNonTrditional").is(':checked') && $("#chkGiveAwayFreeOfCharge").is(':checked')) {

    //            if (ValidateMandatoryFields('txtAGiveAwayComments') == false) {
    //                IsValid = false;
    //                if (FocusFound == false) {
    //                    ControlToFocus = "#txtAGiveAwayComments";
    //                }
    //                FocusFound = true;
    //            }
    //        }


    //        if ($("#chkNonTrditional").is(':checked') && $("#chkOther").is(':checked')) {
    //            if (ValidateMandatoryFields('txtAOtherComments') == false) {
    //                IsValid = false;
    //                if (FocusFound == false) {
    //                    ControlToFocus = "#txtAOtherComments";
    //                }
    //                FocusFound = true;
    //            }

    //        }
    //    }

    //    if (IsValid == false) {
    //        $("#divErrorMessage").text(mandatoryFields);
    //        $(ControlToFocus).focus();
    //        return false;
    //    }
    //    else {
    //        $("#divErrorMessage").text("");
    //        return true;
    //    }

});




function ValidateEmail(emailAdd) {
    var filter = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,3}(;|$)/;
    if (filter.test(emailAdd)) {
        return false;
    }
    else {
        return true;
    }
}

function ValidateMandatoryFields(FieldName) {

    if ($('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == "" && $('#' + FieldName).is(':visible')) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

function ValidateMandatoryFieldsRequestType(FieldName) {

    if ($('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == "" ) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

// function ValidateMandatoryCheckBoxes(CheckBoxName) {
//     if ($('#' + CheckBoxName).is(':checked') && $('#' + CheckBoxName).is(':visible')) {
//         //$('#' + CheckBoxName).removeClass('input-validation-error');
//         return true;
//     }
//     else {         
//         //$('#' + CheckBoxName).addClass('input-validation-error');
//         return false;
//     }
// }


//If chkTVRadioBreakICLA is checked then disabled the Multi Artist Checkbox from the ProjectInformation Tab.
function DisablechkMultiArtist() {
  if ($("#chkTVRadioBreakICLA").is(':checked')) {
      $('#chkMultiArtist').attr('disabled', 'disabled');
      if ($("#chkMultiArtist").is(':checked')) {
          $('#hdnMultiartist').val("true");
      }

      else {
          $('#hdnMultiartist').val("false");
      }
        }
        else {
            $('#chkMultiArtist').removeAttr('disabled');
            if ($("#chkMultiArtist").is(':checked')) {
                $('#hdnMultiartist').val("true");
            }
            else {
                $('#hdnMultiartist').val("false");
            }
        }


    }


    $('#ulSaleChannel input[type=checkbox]').live('click', function () {    
        if ($(this).is(':checked')) {
            $('#ulSaleChannel').removeClass('input-validation-error');
        }
    });
    