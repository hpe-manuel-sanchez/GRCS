$(document).ready(function () {
    //Non Tradition
    $("#imgNonTrad").live("click", function () {
        $("#trNonTrad").slideToggle("fast");
        var imgSrc = $("#imgNonTrad").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgNonTrad").attr("src", "/Gcs/Images/left.png");
            $("#imgNonTrad").attr("title", "Expand");
        }
        else {
            $("#imgNonTrad").attr("src", "/Gcs/Images/Down.png");
            $("#imgNonTrad").attr("title", "Collapse");
        }
        return false;
    });

    //Promotional
    $("#imgPromo").live("click", function () {
        $("#trPromo").slideToggle("fast");
        var imgSrc = $("#imgPromo").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgPromo").attr("src", "/Gcs/Images/left.png");
            $("#imgPromo").attr("title", "Expand");
        }
        else {
            $("#imgPromo").attr("src", "/Gcs/Images/Down.png");
            $("#imgPromo").attr("title", "Collapse");
        }
        return false;
    });

    //Club
    $("#imgClub").live("click", function () {
        $("#trClub").slideToggle("fast");
        var imgSrc = $("#imgClub").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgClub").attr("src", "/Gcs/Images/left.png");
            $("#imgClub").attr("title", "Expand");
        }
        else {
            $("#imgClub").attr("src", "/Gcs/Images/Down.png");
            $("#imgClub").attr("title", "Collapse");
        }
        return false;
    });

    //Price Reduction
    $("#imgPriceRed").live("click", function () {
        $("#trPriceRed").slideToggle("fast");
        var imgSrc = $("#imgPriceRed").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgPriceRed").attr("src", "/Gcs/Images/left.png");
            $("#imgPriceRed").attr("title", "Expand");
        }
        else {
            $("#imgPriceRed").attr("src", "/Gcs/Images/Down.png");
            $("#imgPriceRed").attr("title", "Collapse");
        }
        return false;
    });

    //TV-Radio
    $("#imgTVRadio").live("click", function () {
        $("#trTVRadio").slideToggle("fast");
        var imgSrc = $("#imgTVRadio").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgTVRadio").attr("src", "/Gcs/Images/left.png");
            $("#imgTVRadio").attr("title", "Expand");
        }
        else {
            $("#imgTVRadio").attr("src", "/Gcs/Images/Down.png");
            $("#imgTVRadio").attr("title", "Collapse");
        }
        return false;
    });

    //Scope
    $("#imgScope").live("click", function () {
        $("#trScope").slideToggle("fast");
        var imgSrc = $("#imgScope").attr("src");
        if (imgSrc.indexOf('left') == -1) {
            $("#imgScope").attr("src", "/Gcs/Images/left.png");
            $("#imgScope").attr("title", "Expand");
        }
        else {
            $("#imgScope").attr("src", "/Gcs/Images/Down.png");
            $("#imgScope").attr("title", "Collapse");
        }
        return false;
    });
});


// visibilty of physical sales and revenue section when physical scope checkbox is selected
/*
$("#bodyRequest").ready(function () {

$("#tdChkTVRadio").hide();
$("#tdChkPriceREd").hide();

$("#trTVRadioHeader").hide();
$("#trTVRadio").hide();

$("#trPriceRedHeader").hide();
$("#trPriceRed").hide();

$("#trClubHeader").hide();
$("#trClub").hide();

$("#trPromoHeader").hide();
$("#trPromo").hide();

$("#trNonTradHeader").hide();
$("#trNonTrad").hide();

});

// visibilty of Tv radio price reduction checkbox when regular retail checkbox is selected
$("#tdChkReg").live("click", function () {


if ($("#chkRegularRetail").is(':checked')) {

$("#tdChkTVRadio").show();
$("#tdChkPriceREd").show();

}
else {

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


// visibilty of TV/radio section when TV/radio checkbox is selected
$("#tdChkTVRadio").live("click", function () {

if ($("#chkTVRadioBreakICLA").is(':checked')) {

$("#trTVRadioHeader").show();
$("#trTVRadio").show();

$("#tdChkTV").show();
$("#trTVBudget").hide();
$("#trTVBudgetUSD").hide();
$("#trTVProdCost").hide();

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

$("#trTVRadioHeader").hide();
$("#trTVRadio").hide();

}

});




// visibilty of physical sales and revenue section when physical scope checkbox is selected
$("#tdChkPhysical").live("click", function () {

showHidePhysicalSalesRevenue();

});

// visibilty of digital sales and revenue section when digital scope checkbox is selected
$("#tdChkDigital").live("click", function () {

showHidePhysicalSalesRevenue();

});

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
}
else {

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
}
else {
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
}
else {
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
$("#imgScopeHide").attr("src", "/Gcs/Images/Up.png");
$("#imgScopeHide").attr("title", "Expand");
}
else {

$("#imgScopeHide").attr("src", "/Gcs/Images/Down.png");
$("#imgScopeHide").attr("title", "Collapse");
}
return false;
});

// expandcollapse for TV/Radio section on request type tab

$("#imgTVHide").live("click", function () {
$("#trTVRadio").slideToggle("fast");

var imgSrc = $("#imgTVHide").attr("src");

if (imgSrc.indexOf('Up') == -1) {
$("#imgTVHide").attr("src", "/Gcs/Images/Up.png");
$("#imgTVHide").attr("title", "Expand");
}
else {

$("#imgTVHide").attr("src", "/Gcs/Images/Down.png");
$("#imgTVHide").attr("title", "Collapse");
}
return false;
});

// expandcollapse for Price reduction section on request type tab

$("#imgPriceRed").live("click", function () {
$("#trPriceRed").slideToggle("fast");

var imgSrc = $("#imgPriceRed").attr("src");

if (imgSrc.indexOf('Up') == -1) {
$("#imgPriceRed").attr("src", "/Gcs/Images/Up.png");
$("#imgPriceRed").attr("title", "Expand");
}
else {

$("#imgPriceRed").attr("src", "/Gcs/Images/Down.png");
$("#imgPriceRed").attr("title", "Collapse");
}
return false;
});
// expandcollapse for club section on request type tab

$("#imgClub").live("click", function () {
$("#trClub").slideToggle("fast");

var imgSrc = $("#imgClub").attr("src");

if (imgSrc.indexOf('Up') == -1) {
$("#imgClub").attr("src", "/Gcs/Images/Up.png");
$("#imgClub").attr("title", "Expand");
}
else {

$("#imgClub").attr("src", "/Gcs/Images/Down.png");
$("#imgClub").attr("title", "Collapse");
}
return false;
});
// expandcollapse for promotional section on request type tab

$("#imgPromo").live("click", function () {
$("#trPromo").slideToggle("fast");

var imgSrc = $("#imgPromo").attr("src");

if (imgSrc.indexOf('Up') == -1) {
$("#imgPromo").attr("src", "/Gcs/Images/Up.png");
$("#imgPromo").attr("title", "Expand");
}
else {

$("#imgPromo").attr("src", "/Gcs/Images/Down.png");
$("#imgPromo").attr("title", "Collapse");
}
return false;
});

// expandcollapse for non traditional section on request type tab

$("#imgNonTrad").live("click", function () {
$("#trNonTrad").slideToggle("fast");

var imgSrc = $("#imgNonTrad").attr("src");

if (imgSrc.indexOf('Up') == -1) {
$("#imgNonTrad").attr("src", "/Gcs/Images/Up.png");
$("#imgNonTrad").attr("title", "Expand");
}
else {

$("#imgNonTrad").attr("src", "/Gcs/Images/Down.png");
$("#imgNonTrad").attr("title", "Collapse");
}
return false;
});

//**************************************** expand collapse arrow functionality end ********************************************




// visibilty of price reduction section when price reduction checkbox is selected
$("#chkPriceReduction").live("click", function () {

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

if ($("#chkClub").is(':checked')) {

$("#trClubHeader").show();
$("#trClub").show();
}
else {

$("#trClubHeader").hide();
$("#trClub").hide();
}

});

// visibilty of promotional section when promotional checkbox is selected
$("#chkPromotional").live("click", function () {

if ($("#chkPromotional").is(':checked')) {

$("#trPromoHeader").show();
$("#trPromo").show();
}
else {

$("#trPromoHeader").hide();
$("#trPromo").hide();
}

});

// visibilty of non traditional section when non traditional checkbox is selected
$("#chkNonTrditional").live("click", function () {

if ($("#chkNonTrditional").is(':checked')) {
$("#trNonTradHeader").show();
$("#trNonTrad").show();

$("#trPremuimCommnts").hide();
$("#trGivAwayCommnts").hide();
$("#trOtherCommnts").hide();
}
else {
$("#trNonTradHeader").hide();
$("#trNonTrad").hide();
}

});


// visibilty of comments in non traditional section when premium checkbox is selected
$("#chkPremium").live("click", function () {


if ($("#chkPremium").is(':checked')) {
$("#trPremuimCommnts").show();
}
else {
$("#trPremuimCommnts").hide();
}



});

$("#chkGiveAwayFreeOfCharge").live("click", function () {

if ($("#chkGiveAwayFreeOfCharge").is(':checked')) {
$("#trGivAwayCommnts").show();
}
else {
$("#trGivAwayCommnts").hide();
}

});

$("#chkOther").live("click", function () {


if ($("#chkOther").is(':checked')) {
$("#trOtherCommnts").show();
}
else {
$("#trOtherCommnts").hide();
}

});


// Sum the Textbox value Function for Physical Sales Split & Digital Sales Split:
//Row:1
$("#txtPSaleDate").blur(function () {

var $nFirstNum = 0;
var $nSecondNum = 0;


if ($("input[name='txtPSaleDate']").val != null && $("input[name='txtPSaleDate']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPSaleDate').val())
}
if ($("input[name='txtDSaleDate']").val != null && $("input[name='txtDSaleDate']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDSaleDate').val())
}
var $nResult = $nFirstNum + $nSecondNum
alert($nResult);
if (isNaN($nResult)) {
$nResult = "0";        
}
$("#divTSaleDate").text($nResult);
//alert($("input[name='txtDSaleDate']").val().length)

});

$("#txtDSaleDate").blur(function () {
var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPSaleDate']").val != null && $("input[name='txtPSaleDate']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPSaleDate').val())
}
if ($("input[name='txtDSaleDate']").val !=null && $("input[name='txtDSaleDate']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDSaleDate').val())
}
var $nResult = $nFirstNum + $nSecondNum
alert($nResult);
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTSaleDate").text($nResult);
});
//Row:2
$("#txtPSaleWith").blur(function () {


var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPSaleWith']").val !=null && $("input[name='txtPSaleWith']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPSaleWith').val())
}
if ($("input[name='txtDSaleWith']").val !=null && $("input[name='txtDSaleWith']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDSaleWith').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTSaleWith").text($nResult);
   
});

$("#txtDSaleWith").blur(function () {
var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPSaleWith']").val !=null && $("input[name='txtPSaleWith']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPSaleWith').val())
}
if ($("input[name='txtDSaleWith']").val !=null && $("input[name='txtDSaleWith']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDSaleWith').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTSaleWith").text($nResult);
});
//Row:3
$("#txtPSaleWithout").blur(function () {

var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPSaleWithout']").val !=null && $("input[name='txtPSaleWithout']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPSaleWithout').val())
}
if ($("input[name='txtDSaleWithout']").val !=null && $("input[name='txtDSaleWithout']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDSaleWithout').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTSaleWithout").text($nResult);
});

$("#txtDSaleWithout").blur(function () {
var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPSaleWithout']").val !=null && $("input[name='txtPSaleWithout']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPSaleWithout').val())
}
if ($("input[name='txtDSaleWithout']").val !=null && $("input[name='txtDSaleWithout']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDSaleWithout').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTSaleWithout").text($nResult);
});


// Sum the Textbox value Function for Physical Revenue Split  & Digital Revenue Split
//Row:1
$("#txtPRevDate").blur(function () {

var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPRevDate']").val !=null && $("input[name='txtPRevDate']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPRevDate').val())
}
if ($("input[name='txtDRevDate']").val !=null && $("input[name='txtDRevDate']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDRevDate').val())
}
var $nResult = $nFirstNum + $nSecondNum
$("#divTRevDate").text($nResult);
    

});

$("#txtDRevDate").blur(function () {
var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPRevDate']").val !=null && $("input[name='txtPRevDate']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPRevDate').val())
}
if ($("input[name='txtDRevDate']").val !=null && $("input[name='txtDRevDate']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDRevDate').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTRevDate").text($nResult);
});
//Row:2
$("#txtPRevWith").blur(function () {

var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPRevWith']").val !=null && $("input[name='txtPRevWith']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPRevWith').val())
}
if ($("input[name='txtDRevWith']").val !=null && $("input[name='txtDRevWith']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDRevWith').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTRevWith").text($nResult);

});

$("#txtDRevWith").blur(function () {
var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPRevWith']").val !=null && $("input[name='txtPRevWith']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPRevWith').val())
}
if ($("input[name='txtDRevWith']").val !=null && $("input[name='txtDRevWith']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDRevWith').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTRevWith").text($nResult);
});
//Row:3
$("#txtPRevWithout").blur(function () {

var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPRevWithout']").val !=null && $("input[name='txtPRevWithout']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPRevWithout').val())
}
if ($("input[name='txtDRevWithout']").val !=null && $("input[name='txtDRevWithout']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDRevWithout').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTRevWithoute").text($nResult);
});

$("#txtDRevWithout").blur(function () {
var $nFirstNum = 0;
var $nSecondNum = 0;
if ($("input[name='txtPRevWithout']").val !=null && $("input[name='txtPRevWithout']").attr("value") != "") {
$nFirstNum = parseInt($('#txtPRevWithout').val())
}
if ($("input[name='txtDRevWithout']").val !=null && $("input[name='txtDRevWithout']").attr("value") != "") {
$nSecondNum = parseInt($('#txtDRevWithout').val())
}
var $nResult = $nFirstNum + $nSecondNum
if (isNaN($nResult)) {
$nResult = "0";
}
$("#divTRevWithoute").text($nResult);
});

//Calendar Script
$(function(){  
var pickerOpts = {
dateFormat: "dd/m/yy", 
showOn: "button",
buttonImage: "/Gcs/Images/Calender_Icon_img.png",         
buttonImageOnly: true
};
$("#txtDurationFrom").datepicker(pickerOpts);
$("#txtDurationTo").datepicker(pickerOpts); 
});


$("#txtDurationFrom").change(function () {
var startDt = document.getElementById("txtDurationFrom").value;
var endDt = document.getElementById("txtDurationTo").value;
if ((new Date(startDt).getTime() > new Date(endDt).getTime())) {
alert("From date cannot be greater than the To Date.");
$("#txtDurationFrom").val("");

}
});
$("#txtDurationTo").change(function () {
var startDt = document.getElementById("txtDurationFrom").value;
var endDt = document.getElementById("txtDurationTo").value;
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

var IsValid = true;
var ControlToFocus = "";
var FocusFound = false;

$("#divErrorMessage").text(""); //Set the Validation message blank

if ($("#chkPhysical").is(':checked') || $("#chkDigital").is(':checked')) {
$('#tdScope').removeClass('input-validation-error');
//$('#chkPhysical').removeClass('input-validation-error');
//$('#chkDigital').removeClass('input-validation-error');
}
else {

        
$('#tdScope').addClass('input-validation-error');
//$('#chkPhysical').addClass('input-validation-error');
//$('#chkDigital').addClass('input-validation-error');
//$("#divErrorMessage").text(mandatoryFields);

IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#chkPhysical";
}
FocusFound = true;


}


if ($("#chkRegularRetail").is(':checked') || $("#chkClub").is(':checked') || $("#chkNonTrditional").is(':checked') || $("#chkPromotional").is(':checked')) {

$('#tdRequestType').removeClass('input-validation-error');
//         $('#chkRegularRetail').removeClass('input-validation-error');
//         $('#chkClub').removeClass('input-validation-error');
//         $('#chkNonTrditional').removeClass('input-validation-error');
//         $('#chkPromotional').removeClass('input-validation-error');
}
else {
$('#chkRegularRetail').focus();
$('#tdRequestType').addClass('input-validation-error');
//         $('#chkRegularRetail').addClass('input-validation-error');
//         $('#chkClub').addClass('input-validation-error');
//         $('#chkNonTrditional').addClass('input-validation-error');
//         $('#chkPromotional').addClass('input-validation-error');

IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#chkRegularRetail";
}
FocusFound = true;
}


// TV/Radio Break ICLA 
if ($("#chkTVRadioBreakICLA").is(':checked')) {
if (ValidateMandatoryFields('txtDurationFrom') == false) {
             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDurationFrom";
}
FocusFound = true;
}
if (ValidateMandatoryFields('txtDurationTo') == false) {
             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDurationTo";
}
FocusFound = true;
}
}

//Alteast one should be selected : TV or Radio or Other, 
if ($("#chkTVRadioBreakICLA").is(':checked')) {
if ($("#chkTV").is(':checked') || $("#chkRadio").is(':checked') || $("#chkOthers").is(':checked')) {
$('#chkTV').removeClass('input-validation-error');
$('#chkRadio').removeClass('input-validation-error');
$('#chkOthers').removeClass('input-validation-error');

}
else {

$('#chkTV').addClass('input-validation-error');
$('#chkRadio').addClass('input-validation-error');
$('#chkOthers').addClass('input-validation-error');             
IsValid = false;

}
}

//if TV checkbox is selected 
if ($("#chkTV").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {
if (ValidateMandatoryFields('txtTVBudget') == false) {
             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtTVBudget";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtTVBudgetUSD') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtTVBudgetUSD";
}
FocusFound = true;
}
}

//if Radio checkbox is selected 
if ($("#chkRadio").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

if (ValidateMandatoryFields('txtRdoBudget') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtRdoBudget";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtRdoBudgetUSD') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtRdoBudgetUSD";
}
FocusFound = true;
}

}

//if Other checkbox is selected 
if ($("#chkOthers").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

if (ValidateMandatoryFields('txtOthrBudget') == false) {
             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtOthrBudget";
}
FocusFound = true;
}


if (ValidateMandatoryFields('txtOthrBudgetUSD') == false) {
$('#txtOthrBudgetUSD').focus();
IsValid = false;
}

if (ValidateMandatoryFields('txtOthrMedDetails') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtOthrMedDetails";
}
FocusFound = true;
}
}


//Sale Channel
if ($("#chkTVRadioBreakICLA").is(':checked')) {
if ($("#chkPhysicals").is(':checked') || $("#chkALaCarteDownload").is(':checked') || $("#chkSubscriptionDownload").is(':checked') || $("#chkMobileRealTonesDownload").is(':checked') || $("#chkMobileRingBackDownload").is(':checked') || $("#chkPromotional").is(':checked')) {
$('#tblSaleChannel').removeClass('input-validation-error');
}
else {
$('#tblSaleChannel').addClass('input-validation-error');
$('#tblSaleChannel').focus();
IsValid = false;
}
}


//If chkPhysical is selected then validate Physical Sales Split & Physical Revenue Split.
if ($("#chkPhysical").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

if (ValidateMandatoryFields('txtPSaleDate') == false) {
             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtPSaleDate";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtPSaleWith') == false) {
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtPSaleWith";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtPSaleWithout') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtPSaleWithout";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtPRevDate') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtPRevDate";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtPRevWith') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtPRevWith";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtPRevWithout') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtPRevWithout";
}
FocusFound = true;
}
}


//If chkDigital is selected then validate Digital Sales Split & Digital Revenue Split.
if ($("#chkDigital").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

if (ValidateMandatoryFields('txtDSaleDate') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDSaleDate";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtDSaleWith') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDSaleWith";
}
FocusFound = true;
}


if (ValidateMandatoryFields('txtDSaleWithout') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDSaleWithout";
}
FocusFound = true;
}


if (ValidateMandatoryFields('txtDRevDate') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDRevDate";
}
FocusFound = true;
}

if (ValidateMandatoryFields('txtDRevWith') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDRevWith";
}
FocusFound = true;
}


if (ValidateMandatoryFields('txtDRevWithout') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDRevWithout";
}
FocusFound = true;
}
}

//Price Reduction
if ($("#chkPriceReduction").is(':checked')) {
selRoleTypeText = $("#cboCurrentPriceList option:selected").text();
if (selRoleTypeText.match('--Select Role--')) {
$('#cboCurrentPriceList').addClass('input-validation-error');
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#cboCurrentPriceList";
}
FocusFound = true;
}
else {
$('#cboCurrentPriceList').removeClass('input-validation-error');
}

selRoleTypeText = $("#cboRequestPriceList option:selected").text();
if (selRoleTypeText.match('--Select Role--')) {
$('#cboRequestPriceList').addClass('input-validation-error');             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#cboRequestPriceList";
}
FocusFound = true;
}
else {
$('#cboRequestPriceList').removeClass('input-validation-error');
}
}


//If chkPromotional selected
if ($("#chkPromotional").is(':checked')) {

if (ValidateMandatoryFields('txtDistTo') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtDistTo";
}
FocusFound = true;
}
}


//If chkNonTrditional selected 
if ($("#chkNonTrditional").is(':checked')) {

if (ValidateMandatoryFields('txtClientName') == false) {             
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtClientName";
}
FocusFound = true;
}

//If chkNonTrditional selected with Premium checkbox
if ($("#chkNonTrditional").is(':checked') && $("#chkPremium").is(':checked')) {

if (ValidateMandatoryFields('txtAPremiumComments') == false) {                 
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtAPremiumComments";
}
FocusFound = true;
}
}


if ($("#chkNonTrditional").is(':checked') && $("#chkGiveAwayFreeOfCharge").is(':checked')) {

if (ValidateMandatoryFields('txtAGiveAwayComments') == false) {                 
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtAGiveAwayComments";
}
FocusFound = true;
}
}


if ($("#chkNonTrditional").is(':checked') && $("#chkOther").is(':checked')) {
if (ValidateMandatoryFields('txtAOtherComments') == false) {                 
IsValid = false;
if (FocusFound == false) {
ControlToFocus = "#txtAOtherComments";
}
FocusFound = true;
}

}
}

if (IsValid == false) {
$("#divErrorMessage").text(mandatoryFields);
$(ControlToFocus).focus();
return false;
}
else {
$("#divErrorMessage").text("");
return true;
}

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

if ( $('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == "" && $('#' + FieldName).is(':visible') ) {
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
*/