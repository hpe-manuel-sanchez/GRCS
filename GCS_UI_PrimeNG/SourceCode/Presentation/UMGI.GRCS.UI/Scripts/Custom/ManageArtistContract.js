var vis = 'hidden';
var manageids = '';
var selectedValues = "";
var selectedAdmincompany = "";
var addData = "";
var selectedAdmincompanyId = "";
var artistparentAdmincompanyIds = "";
var returnflag = "";
var hashContract = {};
var contractCollection = [];
var nonuniqueAdminCompanyIds = [];
var selectedContractIds = "";
var artistNotLinkedData = "";
var pageSize = "";
var rowIndex = -1;
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function () {
    LoadwatermarkForManageArtist();
});

function LoadwatermarkForManageArtist() {
    jQuery("#txtMngArtist").watermark("Artist");
    jQuery("#txtMngArtistId").watermark("Artist Id");
    jQuery("#txtMngClearanceCompany").watermark("Clearance Admin Company");
}

$(document).ready(function () {
    pageSize = $('#ddlPartialArtistSearchPaging').val();
});

$(document).ready(function () {
    $('#btnLinkArtistContractToWorkgroup').click(function () {
        selectedValues = "";
        selectedAdmincompany = "";
        getChkboxSelection();
        var uniqueNames = [];
        $.each(selectedAdmincompany.split("|").slice(0, -1), function (i, el) {
            if ($.inArray(el, uniqueNames) === -1) {
                uniqueNames.push(el);
            }
        });
        if (uniqueNames.length == 0) {
            $("#mgArtisterrorMessage").show();
            $("#mgArtisterrorMessage").html(linkWgToContract_Atleast);
            $("#main").animate({ scrollTop: '0' }, 800);
            return;
        }
        if (uniqueNames.length == 1) {
            $("#mgArtisterrorMessage").hide();
            $("#mgArtisterrorMessage").html("");
            var workgroupDialog = $('#divLinkToWorkgroup')
                .dialog({
                    autoOpen: true,
                    modal: true,
                    title: linkWorkgroupToContract,
                    show: 'clip',
                    hide: 'clip',
                    width: "85%", //1000,
                    resizable: false,
                    position: [(($(window).width() - (($(window).width() * 85) / 100)) / 2), 50]
                });
            workgroupDialog.load('/GCS/Workgroup/PartialSearchWorkgroup');

        } else {
            $("#mgArtisterrorMessage").show();
            $("#mgArtisterrorMessage").html(linkWgToContract_SameCompanyMsg);
            $("#main").animate({ scrollTop: 0 }, 800);
            return;
        }
    });
});

$(document).ready(function () {
    if (pageName == 'CreateChildWorkgroup' || pageName == 'MaintainChildWorkgroup') {
        $('#headerArtistScreen').hide();
    }
    else {
        $('#headerArtistScreen').show();
    }
});


function CheckChildWGAdminCompamyDuplicate(companyValuesids) {
    //New logic
    returnflag = "false";
    nonuniqueAdminCompanyIds = [];
    var mathcFlag = "";
    if (companyValuesids != "") {
        $.each(selectedAdmincompanyId.split("|").slice(0, -1), function (i, el) {
            var contractadminids = el;
            mathcFlag = "false";
            $.each(companyValuesids.split(",").slice(0, -1), function (i, elementCompanyId) {
                if (elementCompanyId == contractadminids) {
                    mathcFlag = "true";
                }
            });
            if (mathcFlag == "false") {
                nonuniqueAdminCompanyIds.push(contractadminids);
            }
        });
        if (nonuniqueAdminCompanyIds.length > 0) {
            returnflag = "true";
        }
        else {
            returnflag = "false";
        }
    }
    return returnflag;
}

$(document).ready(function () {
    $('#btnSaveArtistContract').click(function () {
        $('#divSave').hide();
        $('#divSearchResults').hide();
        $('#divAdd').hide();
        $('#divAddedArtistContract').hide();
        $('#divManageArtist').dialog('close');
        saveArtistContract();
    });
});

$(document).ready(function () {
    $('#btnCancelArtistContractLinkToWG').click(function () {
        $('#divAdd').hide();
        $('#divSearchResults').hide();
        $('#divSave').hide();
        $('#divSearchArtistContract').jtable('destroy');
        $('#divSearchArtistContract').show();
        ResetArtistContract();
        document.getElementById('btnLinkArtistContractToWorkgroup').style.visibility = 'hidden';
        document.getElementById('btnCancelArtistContractLinkToWG').style.visibility = 'hidden';
    });
});

$(document).ready(function () {
    $('#btnSearchArtistContract').click(function () {
        rowIndex = -1;
        var searchlist = '';
        manageids = '';
        var artist = $('#txtMngArtist').val();
        var artistId = $('#txtMngArtistId').val();
        var clearanceCompany = $('#txtMngClearanceCompany').val();
        if (artist == '' && artistId == '' && clearanceCompany == '') {
            $("#mgArtisterrorMessage").show();
            $("#mgArtisterrorMessage").html(searchInValid);
            return false;
        } else {
            $("#mgArtisterrorMessage").hide();
            $("#mgArtisterrorMessage").html();
        }
        if (artist != '') { searchlist = searchlist + artist + '/'; }
        if (artistId != '') { searchlist = searchlist + artistId + '/'; }
        if (clearanceCompany != '') { searchlist = searchlist + clearanceCompany; }
        $('#spnSearchAtistPartialResult').empty();
        $('#searchArtistTotalRecordCount').empty();
        $("#spnSearchAtistPartialResult").html('"'+searchlist+'" ');
        $('&nbsp<b><span id="searchArtistTotalRecordCount"/></b>').insertAfter($("#spnSearchAtistPartialResult"));
        $('#divArtist').jtable('destroy');
        addArtistContract(pageSize);
        $('.jtable').css("margin-top", "0");
        $('#divArtist').show();
        $('#divSearchResults').show();
        $('#divAdd').show();
        $('#divArtist').show();
        $('#divSave').hide();
        $('#divAddedArtistContract').hide();
        $('#spnArtistAddedResultLabel').html('');
        $('#spnArtistAddedResultLabel').hide();
        if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            $('#divAdd').hide();
            $('#btnAddResourceContract').hide();
            var docHeight = $(window).height();
            var headerHeight = $('#deleteWorkgroup').height() + $('#divSearchResults').height();
            var footerHeight = 30;

            var mainContainerHeight = docHeight - headerHeight - footerHeight;
            $("#main").css({
                "max-height": mainContainerHeight + "px",
                "height": mainContainerHeight + "px",
                "overflow-y": "auto",
                "overflow-x": "hidden",
                "position": "relative"
            });

            $(".jtable-main-container").css({
                "max-height": "none"
            });
            $(".jtableDvwithDrop").css({

                "overflow-y": "hidden",
                "overflow-x": "hidden",
                "max-height": "none"

            });
        }
        else {
            if ($.browser.msie) {
                if ($.browser.msie && parseInt($.browser.version, 10) > 7) {

                    $(".jtableDvwithDrop").css({

                        "margin-left": "0",
                        "margin-right": "0"


                    });
                }
                else {
                    $(".jtableDvwithDrop").css({

                        "margin-left": "0",
                        "margin-right": "0"


                    });
                }
            }
        }
    });
});

$(document).ready(function () {
    $('#btnAddArtistContract').click(function () {
        getChkboxSelection();
        if (selectedValues.length > 0) {
            var SavedCompanyValue = parent.document.getElementById('hdnSavedCompanyValues').value;
            var resultCheck = "";
            if (SavedCompanyValue != "") {
                resultCheck = CheckChildWGAdminCompamyDuplicate(SavedCompanyValue);
            }
            else {
                artistparentAdmincompanyIds = "";
                var formValues = "name=" + "" + "&isacCode=" + "" + "&country=" + "" + "&workgroupId=" + workGroupId + "&jtStartIndex=" + "0" + "&jtPageSize=" + "5000";
                $.ajax({
                    url: '/GCS/Workgroup/GetCompaniesOfWorkgroup?' + formValues,
                    type: 'POST',
                    async: false,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        if (data.TotalRecordCount > 0) {
                            $.each(data.Records, function (i, el) {
                                artistparentAdmincompanyIds += el.Id + ",";
                            });
                        }
                    }
                });
                resultCheck = CheckChildWGAdminCompamyDuplicate(artistparentAdmincompanyIds);
            }
            if (resultCheck == "false") {
                $('#divAdd').hide();
                $('#divSearchResults').hide();
                $('#divArtist').hide();
                $('#divSave').show();
                $('#divAddedArtistContract').show();
                $('#spnArtistAddedResultLabel').html(listOfArtistContract);
                $('#spnArtistAddedResultLabel').show();
                LoadAddedArtistContract('Add');
            } else {
                if (nonuniqueAdminCompanyIds.length > 0);
                {
                    var wrongCompanyInfo = "";
                    var uniqueNames = [];
                    $.each(nonuniqueAdminCompanyIds, function (i, el) {
                        if ($.inArray(el, uniqueNames) === -1) {
                            uniqueNames.push(el);
                        }
                    });
                    $.each(uniqueNames, function (i, el) {

                        var wrongCompanyIds = JSLINQ(contractCollection).Where(function (contracts) { return contracts.Key == el });
                        if (wrongCompanyIds.items.length != 0) {
                            for (var i = 0; i < wrongCompanyIds.items.length; i++) {
                                wrongCompanyInfo = wrongCompanyInfo + "</br> " + wrongCompanyIds.items[i].Value;
                            }
                        }
                    });
                }
                $('#divManageArtist').css("overflowY", "auto");
                $('#divManageArtist').css('max-height',500) 
                $("#mgArtisterrorMessage").show();
                $("#mgArtisterrorMessage").html(contractcannotLinkMsg + wrongCompanyInfo);
                
            }
        }
        else {
            $('#divAdd').show();
            $('#divSearchResults').show();
            $('#divArtist').show();
            $('#divSave').hide();
            $('#divAddedArtistContract').hide();
            noRowsSelectedForDelete = "true";
            $("#mgArtisterrorMessage").show();
            $("#mgArtisterrorMessage").html(noRowsSelected);
        }
    });
});

$(document).ready(function () {
    $('#btnSearchArtistReset').click(function () {
         $("#mgArtisterrorMessage").html('');
         $("#mgArtisterrorMessage").hide();
         if (document.getElementById('txtMngArtist') != null) {
            document.getElementById('txtMngArtist').value = '';
         }
         if (document.getElementById('txtMngArtistId') != null) {
            document.getElementById('txtMngArtistId').value = '';
         }
         if (document.getElementById('txtMngClearanceCompany') != null) {
            document.getElementById('txtMngClearanceCompany').value = '';
         }
         LoadwatermarkForManageArtist();
    });
});

$(document).ready(function () {
    $('#btnCancelArtistContract').click(function () {
        $('#divAdd').hide();
        $('#divSearchResults').hide();
        $('#divArtist').hide();
          $('#divAddedArtistContract').show();
        if ($('#divAddedArtistContract')[0].innerHTML == "") {
            $('#divSave').hide();
                }
                else {
                   
                    $('#divSave').show();
                }
    });
});

$(document).ready(function () {
    $('#btnACsaveCancel').click(function () {
        $('#divArtist').jtable('destroy');
        $('#divSave').hide();
        $('#divSearchResults').hide();
        $('#divAdd').hide();
        $('#divArtist').show();
        $('#divAddedArtistContract').hide();
        $('#divManageArtist').dialog('close');
    });
});

//Reset the value of rowindex
function ArtistSearch(rowIndex) {
   $('#divArtist').jtable('reset', {
          artistId: $('#txtMngArtistId').val(),
        artistName: $('#txtMngArtist').val(),
        clearanceAdminCompanyName: $('#txtMngClearanceCompany').val(),
        rowIndex: rowIndex
    });
}



function addArtistContract(pageSize) {
    var msg = "";
    if ($("#txtMngArtist").val() != "" || $("#txtMngArtistId").val() != "") {
        msg = 'Artist is not found in R2';
    } else {
        msg = 'No data available!';
    }
    var artistAlertMessages = {
        noDataAvailable: msg
    };
    if (rowIndex != 0 || rowIndex != -1) {
        manageids = "";
    }

    $('#divArtist').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: false,
        defaultSorting: 'Name ASC',
        selecting: false,
        columnResizable: false,
        multiselect: false,
        selectingCheckboxes: false,
        selectOnRowClick: false,
        loadingRecords: function (event, data) {
            manageids = "";
        },
        recordsLoaded: function (event, data) {
            $('.jtable .jtable-no-data-row').show();
            $('#searchArtistTotalRecordCount').html('(' + data.serverResponse.TotalRecordCount + ')');
            rowIndex = data.serverResponse.RowIndex;
            ArtistSearch(rowIndex);
            if (partialViewName == "CreateChildWorkgroup") {
                document.getElementById('btnAddArtistContract').style.visibility = 'visible';
            }
            if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
                document.getElementById('btnLinkArtistContractToWorkgroup').style.visibility = 'visible';
                document.getElementById('btnCancelArtistContractLinkToWG').style.visibility = 'visible';

            }
            artistNotLinkedData = '<div class="subContWrapperDiv"  id="divContract" ><div  class="contentSubDv" style="text-align:center;"><b> ' + artistNotLinkedInfo + '</b></div></div>';
            manageids = manageids.substring(1, manageids.length);
        },
        actions: {
            listAction: '/GCS/workgroup/ManageArtist'
        },
        fields: {
            Image:
            {
                title: "",
                listClass: "txtAlign",
                width: '5%',
                display: function (managedata) {
                    var $image = $('<img id=' + managedata.record.ArtistId + ' src="/Gcs/Images/Acc_Arrow_RIght.png" />');
                    imgDefault = $image;
                    manageids = manageids + "|" + managedata.record.ArtistId;
                    $image.click(function (e) {
                        var imgurl = $(this).attr('src'); // e.srcElement.src;
                        
                        if (imgurl.indexOf("Acc_Arrow_RIght") != -1) {
                            var imgsrc = $(this).attr('src').replace("Acc_Arrow_RIght.png", "Acc_Arrow_Down.png"); 
                            $(this).attr('src', imgsrc); 

                            imgDefault = this; // e;
                            
                            var imgDefSrc = imgDefault.src.replace("Acc_Arrow_RIght.png", "Acc_Arrow_Down.png"); 
                            imgDefault.src = imgDefSrc; 
                            
                            var $childRow = $('#divArtist').jtable('getChildRow', $image.closest('tr'));
                            $childRow.css('background-color', '#f0f0f0');
                            if (!$childRow.is(':visible')) {
                                var formValues = "id=" + managedata.record.ArtistId + "&pageName=ManageArtistContract";
                                if ($childRow.find('td').text().length == 0) {
                                    if (managedata.record.ContractId != 0) {
                                        $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                                            if (data.length != 0) {
                                                $childRow.find('td').empty();
                                                $childRow.find('td').append(data);
                                            }
                                        });
                                    }
                                    else {
                                        $childRow.find('td').empty();
                                        $childRow.find('td').append(artistNotLinkedData);
                                    }
                                }
                                $childRow.show();
                            }
                        }
                        else if (imgurl.indexOf("Acc_Arrow_Down") != -1) {
                            var imgsrc = $(this).attr('src').replace("Acc_Arrow_Down.png", "Acc_Arrow_RIght.png"); 
                            $(this).attr('src', imgsrc);
                            imgDefault = this; // e;
                            var imgDefSrc = imgDefault.src.replace("Acc_Arrow_Down.png", "Acc_Arrow_RIght.png"); 
                            imgDefault.src = imgDefSrc;
                            $('#divArtist').jtable('getChildRow', $image.closest('tr')).hide();
                        }
                    });
                    return $image;
                }
            },
            ArtistId:
                {
                    title: mgArtistContractJtableColNames.ArtistId//"Artist ID"
                },
            ArtistName:
                {
                    title: mgArtistContractJtableColNames.Artist//"Artist"
                },
            ContractId:
                {
                    title: mgArtistContractJtableColNames.Contract, //"Contract",
                    listClass: "txtAlign",
                    width: '6%',
                    display: function (managedata) {
                        var $contractimage;
                        if (managedata.record.ContractId == 0) {
                            $contractimage = $('<img id=ArtistContract_' + managedata.record.ArtistId + ' name="contract_red" title="' + contractTitleRed + '" src="/Gcs/Images/contract_red.png"   />');
                        }
                        else {
                            $contractimage = $('<img id=ArtistContract_' + managedata.record.ArtistId + ' name="contract_green" title="' + contractTitleGreen + '" src="/Gcs/Images/contract_green.png"  />');
                        }
                        return $contractimage;
                    }
                },
            AdditionalInformation:
                {
                    title: mgArtistContractJtableColNames.AddtionalInfo//"Additional Information"
                }
        }
    });
    var txtartistId = $('#txtMngArtistId').val();
    var txtartistName = $('#txtMngArtist').val();
    var txtclearanceAdminCompanyName = $('#txtMngClearanceCompany').val();

    $('#divArtist').jtable('load', {
        artistId: txtartistId,
        artistName: txtartistName,
        clearanceAdminCompanyName: txtclearanceAdminCompanyName,
        rowIndex: rowIndex
    });
   
};

$(function () {
    $('#ddlPartialArtistSearchPaging').change(function () {
      var rowCount = $(this).val();
        manageids = '';
        addArtistContract(rowCount);
    });
});


function ResetArtistContract() {
    if (document.getElementById('txtMngArtist') != null) {
        document.getElementById('txtMngArtist').value = '';
    }
    if (document.getElementById('txtMngArtistId') != null) {
        document.getElementById('txtMngArtistId').value = '';
    }
    if (document.getElementById('txtMngClearanceCompany') != null) {
        document.getElementById('txtMngClearanceCompany').value = '';
    }
    if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
        document.getElementById('btnLinkArtistContractToWorkgroup').style.visibility = 'hidden';
        document.getElementById('btnCancelArtistContractLinkToWG').style.visibility = 'hidden';
    }
    $('#divArtist').jtable('destroy');
    $('#divAdd').hide();
    $('#divSearchResults').hide();
    $('#divArtist').hide();
    if ($('#divAddedArtistContract')[0].innerHTML == "") {
        $('#spnArtistAddedResultLabel').html('');
        $('#spnArtistAddedResultLabel').hide();
        $('#divAddedArtistContract').jtable('destroy');
        $('#divSave').hide();
    }
    else {
        $('#spnArtistAddedResultLabel').html(listOfArtistContract);
        $('#spnArtistAddedResultLabel').show();
        $('#divAddedArtistContract').show();
        $('#divSave').show();
    }
    LoadwatermarkForManageArtist();
}

$(document).ready(function () {
    $('#ancArtistContractExpandall').click(function (e) {
        /*********** CODE ADDED BY MOHIT FOR POPUP ***************/
        if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            $("#divArtist .jtable-main-container").css({ "max-height": "none" });


        }
        else {
            $("#divArtist .jtable-main-container").css({ "width": "auto", "max-height": "300px" });
        }
        $("#divArtist").css("position", "relative");
        $("#divAddedArtistContract").css("position", "relative");
        /***************************************************************************************/
        if (manageids.length != 0) {
            var arrmanageIds = manageids.split('|');
           
            $.each(arrmanageIds, function (i, sId) {
                var $imageExpand = $('#divArtist').find("#" + sId);
                $imageExpand[0].src = '/Gcs/images/Acc_Arrow_Down.png';
                var $childRow = $('#divArtist').jtable('getChildRow', $("#" + sId).closest('tr'));
                var $imgContract = $('#divArtist').find('#ArtistContract_' + sId);
                var formValues = "id=" + sId + "&pageName=ManageArtistContract";
                if (!$childRow.is(':visible')) {
                    if ($childRow.find('td').text().length == 0) {
                        if ($imgContract[0].name == 'contract_green') {
                            $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                                if (data.length != 0) {
                                    $childRow.find('td').empty();
                                    $childRow.find('td').append(data);
                                }
                            });
                        }
                        else {
                            $childRow.find('td').empty();
                            $childRow.find('td').append(artistNotLinkedData);
                        }
                    }
                }

            });
        }
        $('#divArtist').find('.jtable-child-row').show();
    });
});

$(document).ready(function () {
    $('#ancArtistContractSelectall').click(function (e) {
        var arrmanageIds = manageids.split('|');
        $.each(arrmanageIds, function (i, sId) {
            var $imageSelect = $('#divArtist').find("#" + sId);
            $imageSelect[0].src = '/Gcs/images/Acc_Arrow_Down.png';
            var $childRow = $('#divArtist').jtable('getChildRow', $("#" + sId).closest('tr'));
            var $imgContract = $('#divArtist').find('#ArtistContract_' + sId);
            var formValues = "id=" + sId + "&pageName=ManageArtistContract";
            if (!$childRow.is(':visible')) {
                if ($childRow.find('td').text().length == 0) {
                    if ($imgContract[0].name == 'contract_green') {
                                        $.ajax({
                                            url: '/GCS/Workgroup/GetContractInformation/?' + formValues,
                                            type: 'POST',
                                            async: false,
                                            contentType: 'application/json; charset=utf-8',
                                            success: function (data) {
                                                if (data.length != 0) {
                                                    $childRow.find('td').empty();
                                                    $childRow.find('td').append(data);
                                                    $("#divArtist .jtable-child-row input[type='checkbox']").attr('checked', true);
                                                }
                                            }
                                        });
                                    }
                                    else {
                                        $childRow.find('td').empty();
                                        $childRow.find('td').append(artistNotLinkedData);
                                    }
                }
            }
            else {

                $("#divArtist .jtable-child-row input[type='checkbox']").attr('checked', true);
            }
        });
        $('#divArtist').find('.jtable-child-row').show();
    });
});

$(document).ready(function () {
    $('#ancArtistContractDeselectall').click(function (e) {
        var arrmanageIds = manageids.split('|');
        $.each(arrmanageIds, function (i, sId) {
            var $childRow = $('#divArtist').jtable('getChildRow', $("#" + sId).closest('tr'));
            if ($childRow.is(':visible')) {
                if ($childRow.find('td').text().length != 0) {
                    $("#divArtist .jtable-child-row input[type='checkbox']").attr('checked', false);
                }
            }
            else {

                $("#divArtist .jtable-child-row input[type='checkbox']").attr('checked', false);
            }
        });
        $('#divArtist').find('.jtable-child-row').show();
    });
});



function getChkboxSelection() {
    selectedAdmincompanyId = "";
    selectedAdmincompany = "";
    selectedValues = "";
    contractCollection = [];
    hashContract = {};
    $("#divArtist .jtable-child-row [type='checkbox']:checked").each(function () {
        if (selectedValues != '') {
            if (selectedValues.indexOf($(this)[0].id + "|") == -1) {
                selectedValues += $(this)[0].id + "|";
                selectedAdmincompany += $(this)[0].name + "|";
                selectedAdmincompanyId += $(this)[0].value + "|";
                hashContract = { 'Key': $(this)[0].value, 'Value': $(this)[0].dataFld +", "+ $(this)[0].id + ", " + $(this)[0].name };
                contractCollection.push(hashContract);
            }
        }
        else {
            selectedValues += $(this)[0].id + "|";
            selectedAdmincompany += $(this)[0].name + "|";
            selectedAdmincompanyId += $(this)[0].value + "|";
            hashContract = { 'Key': $(this)[0].value, 'Value': $(this)[0].dataFld + ", " + $(this)[0].id + ", " + $(this)[0].name };
            contractCollection.push(hashContract);
        }
    });
    return selectedValues;
}
//Click Event Method
function LoadAddedArtistContract(clickevent) {
    
    if ($('#divAddedArtistContract')[0].innerHTML == "") {
        $('#spnArtistAddedResultLabel').html(listOfArtistContract);
        $('#spnArtistAddedResultLabel').show();
    }
    if (clickevent == "afterSave") {
        $('#divAdd').hide();
        $('#divSearchResults').hide();
        $('#divArtist').hide();
        $('#divSave').show();
        $('#divAddedArtistContract').show();
    }
    $('#divAddedArtistContract').jtable({
        paging: false,
        pageSize: 10,
        sorting: false,
        columnResizable: false,
        defaultSorting: 'ContractId ASC',
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true, selectOnRowClick: false,
        actions: {
            listAction: '/GCS/workgroup/GetArtistbyContract'
        },
        fields: {
            ArtistId: {
                key: true,
                create: false,
                edit: false,
                list: true,
                width: '5%'
            },
            ArtistId: {
                title: mgArtistContractJtableColNames.ArtistId,//'Artist Id',
                width: '15%'
            },
            ArtistName: {
                title: mgArtistContractJtableColNames.Artist,//'Artist',
                width: '10%'
            },
            ContractId: {
                title: mgArtistContractJtableColNames.ContractId, //'Contract Id',
                width: '15%'
            },
            ClearanceAdminCompanyName: {
                title: mgArtistContractJtableColNames.ClrAdminCompName,//'Clearance AdminCompany Name',
                width: '25%'
            },
            ISAC: {
                title: mgArtistContractJtableColNames.ClrAdminCompId,//'Clearance AdminCompany ID ',
                width: '25%'
            }
        },
        recordsLoaded: function (event, data) {
            selectedContractIds = "";
            if (data.records.length > 0) {
                $("#divAddedArtistContract input").removeAttr("checked");
                $("#divAddedArtistContract tr").removeClass("jtable-row-selected");
                for (var i = 0; i < data.records.length; i++) {
                    selectedContractIds += data.records[i].ContractId + '|';
                }
            }
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
        }
    });
    if (clickevent == 'Add') {
        var arrArtSelectedContractIds = selectedValues.split('|');
        var selectedArtUniqueContractIds = '';
        var uniqueArtistContractIds = selectedContractIds.split('|');
        $.each(arrArtSelectedContractIds, function(index, artContractId){
            if ($.inArray(artContractId, uniqueArtistContractIds) === -1) 
                selectedArtUniqueContractIds = selectedArtUniqueContractIds + artContractId + '|';
        });
        selectedValues =selectedArtUniqueContractIds+ selectedContractIds ;
        
        $('#divAddedArtistContract').jtable('load', {
            contractids: selectedValues,
            events: "add"
        });
    }
    else if (clickevent == 'delete') {
        var $selectedRows = $('#divAddedArtistContract').jtable('selectedRows');
        if (selectedContractIds != '') {
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    selectedContractIds = selectedContractIds.replace(record.ContractId + '|', '');
                });
            }
            $('#divAddedArtistContract').jtable('load', {
                contractids: selectedContractIds,
                events: "delete"
            });
        }
        else if (selectedContractIds != "") {
            var selectedArtist = selectedContractIds;
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    selectedArtist = selectedArtist.replace(record.ContractId + '|', '');
                });
            }
            $('#divAddedArtistContract').jtable('load', {
                contractids: selectedArtist,
                events: "delete"
            });
        }
    }
    else if (clickevent == 'afterSave') {
        $('#divAddedArtistContract').jtable('load', {
            contractids: parent.document.getElementById('hdnManageArtistIds').value,
            events: "save"
        });
    }
};

//Remove manage Artist
$(document).ready(function () {
    $('#btnRemoveArtistContract').click(function () {
        if ($('#divAddedArtistContract').jtable('selectedRows').length > 0) {
            LoadAddedArtistContract('delete');
        }
        else {
            $('#divAdd').hide();
            $('#divSearchResults').hide();
            $('#divArtist').hide();
            $('#divSave').show();
            $('#divAddedArtistContract').show();
            noRowsSelectedForDelete = "true";
            //displayDialog('Search For Manage Artist', noRowsSelected);
            $("#mgArtisterrorMessage").show();
            $("#mgArtisterrorMessage").html(noRowsSelected);

        }
    });
});

//Save Manage Artist
function saveArtistContract() {
    $('#divManageArtistContract').jtable({
        paging: false,
        pageSize: 10,
        sorting: false,
        columnResizable: false,
        defaultSorting: 'ContractId ASC',
        selecting: true,
        multiselect: true,
        selectingCheckboxes: false,
        actions: {
           listAction: '/GCS/workgroup/GetArtistbyContract'
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
                width: '5%'
            },
            ArtistId: {
                title: mgArtistContractJtableColNames.ArtistId,//'Artist Id',
                width: '15%'
            },
            ArtistName: {
                title: mgArtistContractJtableColNames.Artist,//'Artist',
                width: '10%'
            },
            ContractId: {
                title: mgArtistContractJtableColNames.ContractId,//'Contract Id',
                width: '10%',
                display: function (data) {
                    var ContractID = data.record.ContractId;
                    return $('<a href="#" style="text-decoration:underline;">' + ContractID + '</a>').click(function () {
                        var formValues = "id=" + ContractID + "&pageName=Childworkgroup";
                        $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                            if (data != null) {
                                var objDialog = $('#divContract')
                                                    .dialog({
                                                        autoOpen: true,
                                                        modal: true,
                                                        title: 'Contract Information',
                                                        show: 'clip',
                                                        hide: 'clip',
                                                        width: 500,
                                                        resizable: false,
                                                        height: 'auto'
                                                    });
                                objDialog.html(data);
                            }
                        });
                    });
                }
            },
            ClearanceAdminCompanyName: {
                title: mgArtistContractJtableColNames.ClrAdminCompName,//'Clearance AdminCompany Name',
                width: '25%'
            },
            ISAC: {
                title: mgArtistContractJtableColNames.ClrAdminCompId,//'Clearance AdminCompany ID ',
                width: '25%'
            }
        },
        recordsLoaded: function (event, data) {
            selectedValues = '';
        }
    });
    parent.document.getElementById('hdnManageArtistIds').value = selectedContractIds;
    $('#divManageArtistContract').jtable('load', {
        contractids: parent.document.getElementById('hdnManageArtistIds').value,
        events: "save"
    });
};
