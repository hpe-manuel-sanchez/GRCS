/// <reference path="../jquery-1.5.1-vsdoc.js" />
/// <reference path="../jquery-1.5.1.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="/GCS/Scripts/Custom/CustomSetting.js" />

var clearanceAdminCompany;
var recordCount = 0;
var WorkFlowStatus;
var linkedRepertoire = true;
var artist;
var generateReport = false;
var companyId;
var companyNames;

$(document).ready(function () {
    $('.gr_breadCrumbNav').html("Report > > Contract Report > > Release Commitment");
    resizeHandler();
    $(window).resize(function () {
        SyncfusionGridScroll();
    });


    $('#btnreset').click(function (e) {
        e.preventDefault();
        $('#AdminCompany').val('');
        $('#AdminCompanyNames').empty();
        $('#ArtistSearch_Info_Name').val('');
        $("#ArtistSearch_Info_Id").val('');
        $('#ReleaseCommitment input:checkbox').removeAttr('checked');
        $('input:radio[name=repertoirelinked][value="1"]').click();
        $('#LinkedRepertoire').val("1");
        generateReport = false;
        companyId = '';
        companyNames = '';
    });

    //AutoComplete Artist
    $('#ArtistSearch_Info_Name').focus();

    var target1 = $("#ArtistSearch_Info_Name");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            
            if (ui.item == null) {
                return;
            }
            $("#ArtistSearch_Info_Name").val(ui.item.value);
            $("#ArtistSearch_Info_Id").val(ui.item.id);
        },
        search: function (event, ui) {
           
            if (ui.item == null) {
                $("#Artist_Info_Name").val("");
                $("#ArtistSearch_Info_Id").val("0");
                return;
            }
        },
        response: function (event, ui) {
            
            var autocomplete = target1.data("autocomplete");
            if (ui.item == null) {
                return;
            }
        },
        change: function (event, ui) {
            
            if (ui.item == null) {
                $("#Artist_Info_Name").val("");
                $("#ArtistSearch_Info_Id").val("0");
                return;
            }
//            else if (ui.item != null && $("#Artist_Info_Name").val() != ui.item.value) {
//                $("#Artist_Info_Name").val("");
//                $("#ArtistSearch_Info_Id").val("0");
//            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#Artist_Info_Name").val(ui.item.value);
                $("#ArtistSearch_Info_Id").val(ui.item.id);

            }
        }
    });


    //************** Check box value*************************//
    $('#ReleaseCommitment input:checkbox').click(function (e) {
        var status = $('#WorkFlowStatus').val();
        var chkValue = this.value;

        if ($(this).is(':checked')) {
            if (status == null || status == "") { status = chkValue; }
            else { status = status + ',' + chkValue; }
        } else {
            var chkpostval = chkValue + ',';
            var chkpreval = chkValue + ',';
            status = status.replace(chkpostval, '');
            status = status.replace(chkpreval, '');
            status = status.replace(chkValue, '');
        }
        $('#WorkFlowStatus').val(status);
    });

    //************** END Check box value*************************//
    //************** Radio button value*************************//

    $("input:radio").click(function () {
        if ($(this).is(':checked')) {
            var Repertoirevalue = this.value == "1";
            $('#LinkedRepertoire').val(Repertoirevalue);
        }
    });

    //************** END Radio button value*************************//

});


$(document).ready(function () {
    $('#AdminCompanyNames').keydown(function (e) { return false; });
    $("#TblReleaseCommitmentwaiting_WaitingPopup_Image").hide();

    $('#btnGenerateReport').click(function () {
        
        var gridobj = $find("TblReleaseCommitment");
        clearanceAdminCompany = $('#AdminCompany').val();
        WorkFlowStatus = $('#WorkFlowStatus').val();
        linkedRepertoire = $('#LinkedRepertoire').val();
        artist = $('#ArtistSearch_Info_Id').val();
        generateReport = true;
        $('#generateReport').val(true);

        $find("TblReleaseCommitment").sendRefreshRequest();
        $(".ReportTemplates").show();

    });




});
/********** grid events*****************/

function GridBegin(sender, args) {
  
    $(".EmptyCell").html("Retrieving Records");
    $("#TblReleaseCommitment .manualPagerLabel:eq(1)").empty();
    $("#TblReleaseCommitment .manualPagerLabel:eq(1)").text("Show items per page");
    args.data["AdminCompany"] = clearanceAdminCompany
    args.data["WorkFlowStatus"] = WorkFlowStatus;
    args.data["LinkedRepertoire"] = linkedRepertoire;
    args.data["artist"] = artist;
    args.data["generateReport"] = generateReport;
    
}

function ActionSuccess(sender, args) {
    SyncfusionGridScroll();
    setSyncGridToolTip("#TblReleaseCommitment_Table");
    var totCount = sender.get_TotalRecordsCount();
    $("#TblReleaseCommitment .MsgBar").text("Release Commitment Result (" + totCount + ")");
    $("#TblReleaseCommitment .manualPagerLabel:eq(1)").empty();
    $("#TblReleaseCommitment .manualPagerLabel:eq(1)").text("Show items per page");
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

    var jtableTop = $("#TblReleaseCommitment").offset().top;
    var topfootPos = $(".footer").offset().top;
    var totRecHeight = $("#TblReleaseCommitment_Table").height() + reduceHgt;
    var tableHeight = topfootPos - jtableTop;
    var gridObj = $find("TblReleaseCommitment");
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

function GridLoad(sender, args) {
    $("#TblReleaseCommitment .MsgBar").empty();
    $("#TblReleaseCommitment .manualPagerLabel:eq(1)").empty();
    $("#TblReleaseCommitment .manualPagerLabel:eq(1)").text("Show items per page");
    $("#TblReleaseCommitment .gridPagerContainerRight").find(".refLink").remove();
    //$("#TblReleaseCommitment .gridPagerContainerRight").find(".RefreshPager").after("<span class=\"hyperlink\"><a class=\"refLink\" href=\"#\" onclick=\"RefreshClick()\">Refresh</a></span>");
}


function setSyncfusionGridHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}


function RefreshClick() {
    $find("TblReleaseCommitment").sendRefreshRequest();
}


function ImageForLinkedRepertoire(events, args) {
    var image;
    switch (args.Column.Name) {
        case "LinkedRepertoire":
            if (args.Data.LinkedRepertoire == "Y") {
                image = "<img src='/GCS/Images/Flag_Green.gif' />";
            }
            else {
                image = "<img src='/GCS/Images/Flag_Red.gif' />";
            }
            return $(args.Element)[0].innerHTML = image;
            break;
        case "TerritorialRight":
            if ($(args.Element)[0].innerHTML != "") {
                image = $(args.Element)[0].innerHTML;
                $(args.Element).tooltip({ bodyHandler: function () {
                    return $("<div><font color=\"red\">" + image + "</font></div>");
                }
                });
            }
            break;
        case "CommencementDate":
        case "LostRightsDate":
        case "EndOfTermDate":
            if ($(args.Element)[0].innerHTML == "01/01/0001") {
                $(args.Element)[0].innerHTML = "";
            }
            break;
        default:
            break;
    }
}


/********** grid events end*****************/


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


    //************Delete Functionality for individual CAC*************//
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
        $('#CompanyNames').val(companyNames);
    }

    /***********************Code to open Admin Company Popup***************************/

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
            // position: [getXPosition(55, 0), getYPosition(80, 60)],
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

        //Load partial view
        objGlobalDialog.load('/GCS/Report/Company', "",
                function (responseText, textStatus) {
                    if (textStatus == 'error') {
                        objGlobalDialog.html('<p>' + messageCommon.error + '</p>');
                    }
                    $('#reportType').val('ContractReport');
                });

        if ($("#ClearanceAdminDialog").is(':visible')) {
            if ($(".cacTableContainer").is(':visible')) {
                var TotDiaHgt = $("#ClearanceAdminDialog").height();
                var ReduceHgt = TotDiaHgt - 135;
                $(".cacTable").css('height', ReduceHgt + "px");
                $(".ClearanceAdminDialog").css('position', 'center');
            }
        }
    });
    /***********************End Admin Company Popup***************************/
});

/***********************Resize Popup starts ***************************/
function resizeHandler(e) {

    if (e == undefined || $(e.target).hasClass('ui-dialog') == false) {
        // For CAC 
        $('#ClearanceAdminDialog').dialog('option', 'width', getPopupWidth(70, 750));
        $('#ClearanceAdminDialog').dialog('option', 'height', getPopupHeight(80, 300, 60));
        $('#ClearanceAdminDialog').dialog('option', 'position', [getXPosition(70, 100), getYPosition(80, 30)]);


    }
}
/***********************Resize Popup Ends ***************************/


//function checkCompanyAlreadySelected(check) {
//    var checkBoxElement = check;
//    if (isAlreadySelected(checkBoxElement)) {
//        $(checkBoxElement).prop('checked', true);

//        return checkBoxElement;
//    }
//    return checkBoxElement;
//}

//function checkIsAlreadyExist(check) {
//    var checkBoxElement = check;
//    if (isAlreadySelected(checkBoxElement)) {
//        $(checkBoxElement).prop('checked', true);
//        createCACTableRow('SelectedList', checkBoxElement);
//        return checkBoxElement;
//    }
//    return checkBoxElement;
//}

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
   
    var selectedCompanies = new Array();
    var Names = companyNames.split(',');
    var companyIds = companyId.split(',');
    for (var index = 0; index < companyIds.length; index++) {
        var newObject = new Object();
        newObject.id = companyIds[index];
        newObject.value = Names[index];
        selectedCompanies.push(newObject);
    }
    return selectedCompanies;
}
/*******************Export****************************/

function ExportPDF() {
    $('#ExportType').val("PDF");
    document.forms[0].submit();
}

function ExportExcel() {
    $('#ExportType').val("Excel");
    document.forms[0].submit();
}
/***********************************************/
$(window).bind("resize", resizeHandler);

