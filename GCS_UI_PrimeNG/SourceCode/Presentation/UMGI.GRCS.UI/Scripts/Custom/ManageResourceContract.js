var vis = 'hidden';
var manageids = '';
var selectedValues = "";
var selectedAdmincompany = "";
var selectedAdmincompanyId = "";
var parentAdmincompanyIds = "";
var addData = "";
var returnflag = "";
var hashContract = {};
var contractCollection = [];
var hashContractAndResource = {};
var contractAndResourceCollection = [];
var nonuniqueAdminCompanyIds = [];
var selectedresourceContractIds = "";
var selectedContractIdValues = "";
var resourceNotLinkedData = "";
var pageSize = "";
var resourcepagerowIndex = -1;
var resourceContractForDivation = {};
var resourceContractCollectionForDivation = [];
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function () {
    pageSize = $('#ddlPartialResourceSearchPaging').val();
});

$(document).ready(function () {
    $('#btnLinkResourceContractToWorkgroup').click(function () {
        selectedValues = "";
        selectedAdmincompany = "";
        selectedAdmincompanyId = "";
        selectedContractIdValues = "";
        getChkboxSelection();
        var uniqueNames = [];
        $.each(selectedAdmincompany.split("|").slice(0, -1), function (i, el) {
            if ($.inArray(el, uniqueNames) === -1) {
                uniqueNames.push(el);
            }
        });
        if (uniqueNames.length == 0) {
            $("#mgResourcErrorMessage").show();
            $("#mgResourcErrorMessage").html(linkWgToContract_Atleast);
            return;
        }

        if (uniqueNames.length == 1) {
            $("#mgResourcErrorMessage").hide();
            $("#mgResourcErrorMessage").html("");
            var workgroupDialog = $('#divLinkToWorkgroup')
                .dialog({
                    autoOpen: true,
                    modal: true,
                    title: linkWorkgroupToContract,
                    show: 'clip',
                    hide: 'clip',
                    width: "98%",
                    resizable: false,
                    position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50]
                });
            workgroupDialog.load('/GCS/Workgroup/PartialSearchWorkgroup');
            //$('#divLinkToWorkgroup').css("overflowY", "auto");
           // $('#divLinkToWorkgroup').css("height", "300px");
        } else {
            $("#mgResourcErrorMessage").show();
            $("#mgResourcErrorMessage").html(linkWgToContract_SameCompanyMsg);
            return;
        }

    });
});

//$(document).ready(function () {
//    LoadwatermarkForResourceContract();
//});

$(document).ready(function () {
    if (pageName == 'CreateChildWorkgroup' || pageName == 'MaintainChildWorkgroup') {
        $('#headerResourceScreen').hide();
    }
    else {
        $('#headerResourceScreen').show();
    }
});

$(document).ready(function () {
    $('#btnSearchResourceContract').click(function () {
        manageids = '';
        resourcepagerowIndex = -1;
        addResourceContract(pageSize);
        $('#divSearchResourceContract').show();
        $('#divAddedResourceContract').hide();
        $('#spnResourceAddedResultLabel').html('');
        $('#spnResourceAddedResultLabel').hide();
        $('#divManageResource').show();
        $('#divSaveResourceContractButtons').hide();
        if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            $('#divAddResourceContractButtons').hide();
            $('#btnAddResourceContract').hide();
            $('#btnCancelResourceContract').hide();
        }

        $('.jtable').css("margin-top", "0");
        $('#btnCancelResourceContract').show();
    });
});

$(document).ready(function () {
    $('#btnAddResourceContract').click(function () {
           getChkboxSelection();

        if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') { }
        else {
            $(".jtable-main-container").css({ "position": "static" });
            $(".jtable-main-container").addClass("posFix");
        }
        if (selectedValues.length > 0) {
            var SavedCompanyValue = parent.document.getElementById('hdnSavedCompanyValues').value;
            var resultCheck = "";
            if (SavedCompanyValue != "") {
                resultCheck = CheckChildWGAdminCompamyDuplicate(SavedCompanyValue);
            }
            else {
                parentAdmincompanyIds = "";
                var formValues = "name=" + "" + "&isacCode=" + "" + "&country=" + "" + "&workgroupId=" + workGroupId + "&jtStartIndex=" + "0" + "&jtPageSize=" + "5000";
                $.ajax({
                    url: '/GCS/Workgroup/GetCompaniesOfWorkgroup?' + formValues,
                    type: 'POST',
                    async: false,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        if (data.TotalRecordCount > 0) {
                            $.each(data.Records, function (i, el) {
                                parentAdmincompanyIds += el.Id + ",";
                            });
                        }
                    }
                });
                resultCheck = CheckChildWGAdminCompamyDuplicate(parentAdmincompanyIds);
            }
            if (resultCheck == "false") {
                $("#mgResourcErrorMessage").hide();
                $("#mgResourcErrorMessage").html('');
                $('#divAddResourceContractButtons').hide();
                $('#divResourceSearchResults').hide();
                $('#divSearchResourceContract').hide();
                $('#divSaveResourceContractButtons').show();
                $('#spnResourceAddedResultLabel').html(listOfResourceContract);
                $('#spnResourceAddedResultLabel').show();
                $('#divAddedResourceContract').show();
                if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
                    $("#btnLinkToWorkgroup").show();
                    $("#btnCancelResourceContractLinkToWG").show();
                } else {
                }
                LoadAddedResourceContract('Add');
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
                //displayDialog(manageResourceTitle, contractcannotLinkMsg + wrongCompanyInfo);
                $("#mgResourcErrorMessage").show();
                $("#mgResourcErrorMessage").html(contractcannotLinkMsg + wrongCompanyInfo);
            }
        }
        else {
            $('#divAddResourceContractButtons').show();
            $('#divResourceSearchResults').show();
            $('#divSearchResourceContract').show();
            $('#divSaveResourceContractButtons').hide();
            $('#divAddedResourceContract').hide();
            noRowsSelectedForDelete = "true";
            $("#mgResourcErrorMessage").show();
            $("#mgResourcErrorMessage").html(noRowsSelected);
        }
    });
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
    $('#btnSaveResourceContract').click(function () {
        $('#divSaveResourceContractButtons').hide();
        $('#divResourceSearchResults').hide();
        $('#divAddResourceContractButtons').hide();
        $('#divAddedResourceContract').hide();
        $('#divManageResource').dialog('close');
     saveResourceContract();
     });
});

$(document).ready(function () {
    $('#btnSearchResourceReset').click(function () {
        $("#mgResourcErrorMessage").html('');
        $("#mgResourcErrorMessage").hide();
        //Bug Fix for 4714
        //        ResetResourceContract();
        if (document.getElementById('txtArtist') != null) {
        	document.getElementById('txtArtist').value = '';
        }
        if (document.getElementById('txtArtistId') != null) {
        	document.getElementById('txtArtistId').value = '';
        }
        if (document.getElementById('txtClearanceCompany') != null) {
        	document.getElementById('txtClearanceCompany').value = '';
        }
        if (document.getElementById('txtISRC') != null) {
        	document.getElementById('txtISRC').value = '';
        }
        if (document.getElementById('txtTitle') != null) {
        	document.getElementById('txtTitle').value = '';
        }
        if (document.getElementById('txtVersionTitle') != null) {
        	document.getElementById('txtVersionTitle').value = '';
        }
        document.getElementById('ddlResourceType').value = "1";

       // LoadwatermarkForResourceContract();
        //Bug Fix for 4714
    });
});

$(document).ready(function () {
    $('#btnCancelResourceContract').click(function () {
        $('#divAddResourceContractButtons').hide();
        $('#divResourceSearchResults').hide();
        $('#divSaveResourceContractButtons').hide();
        $('#divSearchResourceContract').jtable('destroy');
        $('#divSearchResourceContract').show();
        ResetResourceContract();
    });
});

$(document).ready(function () {
    $('#btnCancelResourceContractLinkToWG').click(function () {
        $('#divAddResourceContractButtons').hide();
        $('#divResourceSearchResults').hide();
        $('#divSaveResourceContractButtons').hide();
        $('#divSearchResourceContract').jtable('destroy');
        $('#divSearchResourceContract').show();
        ResetResourceContract();
        document.getElementById('btnLinkResourceContractToWorkgroup').style.visibility = 'hidden';
        document.getElementById('btnCancelResourceContractLinkToWG').style.visibility = 'hidden';
        });
});

$(document).ready(function () {
    $('#btnRCsaveCancel').click(function () {
        $('#divSaveResourceContractButtons').hide();
        $('#divResourceSearchResults').hide();
        $('#divAddResourceContractButtons').show();
        $('#divSearchResourceContract').hide();
        $('#divAddedResourceContract').hide();
        $('#divSearchResourceContract').jtable('destroy');
        $('#divManageResource').dialog('close');
    });
});

//Reset the value of rowindex
function ResourceSearch(resourcepagerowIndex) {
        var txtresourceType = '';
    var txtresourceTypetext = ''; 
    var selectedRoleIndex = $('#ddlResourceType')[0].selectedIndex;
    if (selectedRoleIndex != -1) {
        txtresourceType = $('#ddlResourceType')[0][selectedRoleIndex].value;
        txtresourceTypetext = $('#ddlResourceType')[0][selectedRoleIndex].text;
    }

    $('#divSearchResourceContract').jtable('reset', {
        artistName: $('#txtArtist').val(),
        title: $('#txtTitle').val(),
        iSrcCode: $('#txtISRC').val(),
        artistId: $('#txtArtistId').val(),
        versionTitle: $('#txtVersionTitle').val(),
        resourceTypeId: txtresourceType,
        resourceType: txtresourceTypetext,
        clearanceAdminCompanyName: $('#txtClearanceCompany').val(),
        rowIndex: resourcepagerowIndex
    });
}


function addResourceContract(pageSize) {
    var searchlist = '';
    var txtresourceType = '';
    var txtresourceTypetext = ''; 
    var artist = $('#txtArtist').val();
    var artistId = $('#txtArtistId').val();
    var isrcCode = $('#txtISRC').val();
    var title = $('#txtTitle').val();
    var versionTitle = $('#txtVersionTitle').val();
    var clearanceCompany = $('#txtClearanceCompany').val();
    var selectedRoleIndex = $('#ddlResourceType')[0].selectedIndex;
    if (artist == '' && artistId == '' && clearanceCompany == '' && title == '' && versionTitle == '' && isrcCode == '' && selectedRoleIndex == -1) {
    $("#mgResourcErrorMessage").show();
        $("#mgResourcErrorMessage").html(searchInValid);
        return false;
    }
    else {
        $('#divResourceSearchResults').show();
        $('#divAddResourceContractButtons').show();

        $("#mgResourcErrorMessage").hide();
        $("#mgResourcErrorMessage").html('');
    }
    if (artist != '') {
        searchlist = searchlist + artist + '/';
    }
    if (artistId != '') {
        searchlist = searchlist + artistId + '/';
    }
    if (clearanceCompany != '') {
        searchlist = searchlist + clearanceCompany + '/';
    }
    if (title != '') {
        searchlist = searchlist + title + '/';
    }
    if (versionTitle != '') {
        searchlist = searchlist + versionTitle + '/';
    }
    if (isrcCode != '') {
        searchlist = searchlist + isrcCode + '/';
    }
    
    if (selectedRoleIndex != -1) {
        txtresourceType = $('#ddlResourceType')[0][selectedRoleIndex].value;
        txtresourceTypetext = $('#ddlResourceType')[0][selectedRoleIndex].text;
        searchlist = searchlist + txtresourceTypetext ;
    }
    $('#spnSearchResourcePartialResult').empty();
    $('#searchResourceTotalRecordCount').empty();
    $("#spnSearchResourcePartialResult").html('"'+searchlist+'" ');
    $('&nbsp<b><span id="searchResourceTotalRecordCount"/></b>').insertAfter($("#spnSearchResourcePartialResult"));
    var msg = "";
    if ($("#txtArtist").val() != "" || $("#txtArtistId").val() != "") {
        msg = 'Artist is not found in R2';
    } else {
        msg = 'No data available!';
    }
    var resourceAlertMessages = {
        noDataAvailable: msg
    };
    $('#divSearchResourceContract').jtable({
        //   messages: resourceAlertMessages,
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
            $('#searchResourceTotalRecordCount').html("");
            $('#searchResourceTotalRecordCount').html('(' + data.serverResponse.TotalRecordCount + ')');
            resourcepagerowIndex = data.serverResponse.RowIndex;
            ResourceSearch(resourcepagerowIndex);
            if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
                document.getElementById('btnLinkResourceContractToWorkgroup').style.visibility = 'visible';
                document.getElementById('btnCancelResourceContractLinkToWG').style.visibility = 'visible';
                $('#btnCancelResourceContract').hide();
            }
            manageids = manageids.substring(1, manageids.length);
            resourceNotLinkedData = '<div class="subContWrapperDiv"  id="divContract" ><div  class="contentSubDv" style="text-align:center;"><b> ' + resourceNotLinkedInfo + '</b></div></div>';
        },
        actions: {
            listAction: '/GCS/workgroup/ManageResourceContract'
        },
        fields: {
            Image:
                {
                    title: "",
                    listClass: "txtAlign",
                    width: '5%',
                    display: function (managedata) {
                        var $image = $('<img id=' + managedata.record.R2ResourceId + ' src="/Gcs/Images/Acc_Arrow_RIght.png" />');
                        imgDefault = $image;
                        manageids = manageids + "|" + managedata.record.R2ResourceId;
                        $image.click(function (e) {
                            var imgurl = $(this).attr('src'); //e.srcElement.src;
                            // imgurl = imgurl.substr(imgurl.length - 19);
                            // if (imgurl == 'Acc_Arrow_RIght.png') {
                            if (imgurl.indexOf("Acc_Arrow_RIght") != -1) {
                                var imgsrc = $(this).attr('src').replace("Acc_Arrow_RIght.png", "Acc_Arrow_Down.png"); $(this).attr('src', imgsrc); 
                                imgDefault = this; // e;
                                var imgDefSrc = imgDefault.src.replace("Acc_Arrow_RIght.png", "Acc_Arrow_Down.png"); imgDefault.src = imgDefSrc; 
                                var $childRow = $('#divSearchResourceContract').jtable('getChildRow', $image.closest('tr'));
                                $childRow.css('background-color', '#f0f0f0');
                                if (!$childRow.is(':visible')) {
                                    var formValues = '';
                                    var formValues = "id=" + managedata.record.ResourceId + "&pageName=ManageResourceContract";
                                    if ($childRow.find('td').text().length == 0) {
                                        if (managedata.record.ContractId != 0) {
                                            $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                                                if (data.length != 0) {
                                                    $childRow.find('td').empty();
                                                    $childRow.find('td').append(data);
                                                    // var resourceId = $childRow.find(".checkBoxDv [type = 'checkbox']");
                                                    $childRow.find(".checkBoxDv [type = 'checkbox']").attr("resourceId", managedata.record.ResourceId);
                                                    //$($childRow.find(".checkBoxDv [type = 'checkbox']")).html(managedata.record.ResourceId);
                                                    // resourceId.innerHTML = managedata.record.ResourceId;
                                                }
                                            });
                                        }
                                        else {
                                            $childRow.find('td').empty();
                                            $childRow.find('td').append(resourceNotLinkedData);
                                        }
                                    }
                                    $childRow.show();
                                }
                            } else if (imgurl.indexOf("Acc_Arrow_Down") != -1) {
                                var imgsrc = $(this).attr('src').replace("Acc_Arrow_Down.png", "Acc_Arrow_RIght.png"); $(this).attr('src', imgsrc); 
                                imgDefault = this; // e;
                                var imgDefSrc = imgDefault.src.replace("Acc_Arrow_Down.png", "Acc_Arrow_RIght.png"); imgDefault.src = imgDefSrc; 
                                $('#divSearchResourceContract').jtable('getChildRow', $image.closest('tr')).hide();
                            }
                        });
                        return $image;
                    }
                },
            ResourceType:
                {
                    title: mgResourceContractJtableColNames.Type
                },
            Isrc:
                {
                    title: mgResourceContractJtableColNames.ISRC//"ISRC"
                },
            ResourceTitle:
                {
                    title: mgResourceContractJtableColNames.Title//"Title"
                },
            VersionTitle:
                {
                    title: mgResourceContractJtableColNames.VersionTitle//"Version Title"
                },
            ArtistName:
                {
                    title: mgResourceContractJtableColNames.Artist//"Artist"
                },
            ContractId:
                {
                    title: mgResourceContractJtableColNames.Contract, //"Contract",
                    listClass: "txtAlign",
                    width: '6%',
                    display: function (managedata) {
                        var $contractimage;
                        if (managedata.record.ContractId == 0) {
                            $contractimage = $('<img id=Contract_' + managedata.record.R2ResourceId + ' name="resource_contract_red" title="' + contractTitleRed + '" data-ResourceID="' + managedata.record.ResourceId + '" src="/Gcs/Images/contract_red.png" />');
                        } else {
                            $contractimage = $('<img id=Contract_' + managedata.record.R2ResourceId + ' name="resource_contract_green" title="' + contractTitleGreen + '" data-ResourceID="' + managedata.record.ResourceId + '" src="/Gcs/Images/contract_green.png" />');
                        }
                        return $contractimage;
                    }
                },
            RightsTypeName:
                {
                    title: mgResourceContractJtableColNames.AddtionalInfo//"Additional Information"
                },
            ClearanceAdminCompanyName:
                {
                    title: mgResourceContractJtableColNames.DataAdmbyComp//"Data Admin by Company"
                }
        }
    });
        $('#divSearchResourceContract').jtable('load', {
        artistName: artist,
        title: title,
        isrcCode: isrcCode,
        artistId: artistId,
        versionTitle: versionTitle,
        resourceTypeId: txtresourceType,
        resourceType: txtresourceTypetext,
        clearanceAdminCompanyName: clearanceCompany,
        rowIndex: resourcepagerowIndex
    });
  }

function LoadwatermarkForResourceContract() {
    jQuery("#txtArtist").watermark("Artist");
    jQuery("#txtArtistId").watermark("Artist Id");
    jQuery("#txtClearanceCompany").watermark("Clearance Admin Company");
    jQuery("#txtTitle").watermark("Title");
    jQuery("#txtISRC").watermark("ISRC");
    jQuery("#txtVersionTitle").watermark("VersionTitle");
}

$(function () {
    $('#ddlPartialResourceSearchPaging').change(function () {
        var rowCount = $(this).val();
        manageids = '';
        addResourceContract(rowCount);
    });
});


function ResetResourceContract() {
    if (document.getElementById('txtArtist') != null) {
        document.getElementById('txtArtist').value = '';
    }
    if (document.getElementById('txtArtistId') != null) {
        document.getElementById('txtArtistId').value = '';
    }
    if (document.getElementById('txtClearanceCompany') != null) {
        document.getElementById('txtClearanceCompany').value = '';
    }
    if (document.getElementById('txtISRC') != null) {
        document.getElementById('txtISRC').value = '';
    }
    if (document.getElementById('txtTitle') != null) {
        document.getElementById('txtTitle').value = '';
    }
    if (document.getElementById('txtVersionTitle') != null) {
        document.getElementById('txtVersionTitle').value = '';
    }
    if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
        document.getElementById('btnLinkResourceContractToWorkgroup').style.visibility = 'hidden';
        document.getElementById('btnCancelResourceContractLinkToWG').style.visibility = 'hidden';
    }
    $('#divSearchResourceContract').jtable('destroy');
    $('#divSearchResourceContract').hide();
    $('#divAddResourceContractButtons').hide();
    $('#divResourceSearchResults').hide();
    if ($('#divAddedResourceContract')[0].innerHTML == "") {
        $('#spnResourceAddedResultLabel').html('');
        $('#spnResourceAddedResultLabel').hide();
        $('#divAddedResourceContract').jtable('destroy');
        $('#divSaveResourceContractButtons').hide();
    }
    else {
        $('#spnResourceAddedResultLabel').html(listOfResourceContract);
        $('#spnResourceAddedResultLabel').show();
        $('#divAddedResourceContract').show();
        $('#divSaveResourceContractButtons').show();
    }
   // LoadwatermarkForResourceContract();
}

$(document).ready(function () {
    $('#ancResourceContractExpandAll').click(function (e) {
        e.preventDefault();
        if (pageName == 'LinkWGToArtistContract' || pageName == 'LinkWGToResourceContract') {
            $(".jtable-main-container").css({ "max-height": "none" });
        }
        if (manageids.length != 0) {
            var arrmanageIds = manageids.split('|');
            $.each(arrmanageIds, function (i, sId) {
                var $imageResourceExpand = $('#divSearchResourceContract').find("#" + sId);
                $imageResourceExpand[0].src = '/Gcs/images/Acc_Arrow_Down.png';
                var $childRow = $('#divSearchResourceContract').jtable('getChildRow', $("#" + sId).closest('tr'));
                var $imgContract = $('#divSearchResourceContract').find('#Contract_' + sId);
                var resourceID = $imgContract.attr("data-ResourceID");
                var formValues = "id=" + resourceID + "&pageName=ManageResourceContract";
                if (!$childRow.is(':visible')) {
                    if ($childRow.find('td').text().length == 0) {
                        if ($imgContract[0].name == 'resource_contract_green') {//if ($imgContract[0].nameProp == 'contract_green.png') {
                            $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                                if (data.length != 0) {
                                    $childRow.find('td').empty();
                                    $childRow.find('td').append(data);
                                    // var resourceId = $childRow.find(".checkBoxDv [type = 'checkbox']");
                                    $childRow.find(".checkBoxDv [type = 'checkbox']").attr("resourceId", resourceID);
                                    // resourceId.innerHTML = sId;
                                }
                            });
                        }
                        else {
                            $childRow.find('td').empty();
                            $childRow.find('td').append(resourceNotLinkedData);
                        }
                    }
                }

            });
        }
        $('#divSearchResourceContract').find('.jtable-child-row').show();
        return false;
    });
});

$(document).ready(function () {
    $('#ancResourceContractSelectAll').click(function (e) {
        var arrmanageIds = manageids.split('|');
        $.each(arrmanageIds, function (i, sId) {
            var $imageResourceSelect = $('#divSearchResourceContract').find("#" + sId);
            $imageResourceSelect[0].src = '/Gcs/images/Acc_Arrow_Down.png';
            var $childRow = $('#divSearchResourceContract').jtable('getChildRow', $("#" + sId).closest('tr'));
            var $imgContract = $('#divSearchResourceContract').find('#Contract_' + sId);
            var resourceID = $imgContract.attr("data-ResourceID");
            var formValues = "id=" + resourceID + "&pageName=ManageResourceContract";
            if (!$childRow.is(':visible')) {
                if ($childRow.find('td').text().length == 0) {
                    if ($imgContract[0].name == 'resource_contract_green') {//if ($imgContract[0].nameProp == 'contract_green.png') {
                        $.post('/GCS/workgroup/GetContractInformation/', formValues, function (data) {
                            if (data.length != 0) {
                                $childRow.find('td').empty();
                                $childRow.find('td').append(data);
                                //var resourceId = $childRow.find(".checkBoxDv [type = 'checkbox']");
                                //resourceId.innerHTML = sId;
                                $childRow.find(".checkBoxDv [type = 'checkbox']").attr("resourceId", resourceID);
                                $("#divSearchResourceContract .jtable-child-row input[type='checkbox']").attr('checked', true);
                            }
                        });
                    }
                    else {
                        $childRow.find('td').empty();
                        $childRow.find('td').append(resourceNotLinkedData);
                    }
                }

            }
            else {
                 $("#divSearchResourceContract .jtable-child-row input[type='checkbox']").attr('checked', true);
            }
        });
        $('#divSearchResourceContract').find('.jtable-child-row').show();
    });
});

$(document).ready(function () {
    $('#ancResourceContractDeselectall').click(function (e) {
        var arrmanageIds = manageids.split('|');
        $.each(arrmanageIds, function (i, sId) {
            var $childRow = $('#divSearchResourceContract').jtable('getChildRow', $("#" + sId).closest('tr'));
            if ($childRow.is(':visible')) {
                if ($childRow.find('td').text().length != 0) {
                                $("#divSearchResourceContract .jtable-child-row input[type='checkbox']").attr('checked', false);
                    }
                    }
                    else {
                        $("#divSearchResourceContract .jtable-child-row input[type='checkbox']").attr('checked', false);
                    }
            });
        $('#divSearchResourceContract').find('.jtable-child-row').show();
    });
});

function getChkboxSelection() {
    selectedAdmincompanyId = "";
    selectedAdmincompany = "";
    selectedValues = "";
    selectedContractIdValues = "";
    var newArray = [];
    contractCollection = [];
    hashContract = {};
    ContractAndResourceForDivation = {};
    contractAndResourceCollectionForDivation = [];
    resourceContractForDivation = {};
    resourceContractCollectionForDivation = [];
    $("#divSearchResourceContract .jtable-child-row [type='checkbox']:checked").each(function () {
        selectedValues += $(this).attr("id") + ',' + $(this).attr("resourceId") + "|";
        selectedAdmincompany += $(this)[0].name + "|";
        selectedAdmincompanyId += $(this)[0].value + "|";
        hashContract = { 'Key': $(this)[0].value, 'Value': $(this).attr("dataFld") + ", " + $(this).attr("id") + ", " + $(this)[0].name };
        contractCollection.push(hashContract);
        if (selectedContractIdValues.indexOf($(this).attr("id") + "|") == -1) {
            selectedContractIdValues += $(this).attr("id") + "|";
        }
        resourceContractForDivation = { 'ContractId': $(this).attr("id"), 'ResourceId': $(this).attr("resourceId") };
        resourceContractCollectionForDivation.push(resourceContractForDivation);
        //            if (selectedValues.indexOf($(this)[0].id + "|") == -1)   {

        //                selectedValues += $(this)[0].id + "|";
        //                selectedAdmincompany += $(this)[0].name + "|";
        //                selectedAdmincompanyId += $(this)[0].value + "|";
        //                hashContract = { 'Key': $(this)[0].value, 'Value': $(this)[0].dataFld + ", " + $(this)[0].id + ", " + $(this)[0].name + ", " + $(this)[0].innerHTML };
        //                contractCollection.push(hashContract);
        //                //  hashContractAndResource = { 'ContractId': $(this)[0].id, 'ResourceId': $(this)[0].innerHTML };
        //                // contractAndResourceCollection.push(hashContractAndResource);
        //                selectedRemoveitems = $(this)[0].id +','+$(this)[0].innerHTML;
        //               // GetResourceAndContractIds(selectedRemoveitems, "Add");
        //            }
        //            else {
        //             //   hashContractAndResource = { 'ContractId': $(this)[0].id, 'ResourceId': $(this)[0].innerHTML };
        //             //   contractAndResourceCollection.push(hashContractAndResource);
        //                selectedRemoveitems = $(this)[0].id + ',' + $(this)[0].innerHTML;
        //               // GetResourceAndContractIds(selectedRemoveitems, "Add");
        //            }
        //        }
        //        else {
        //            selectedValues += $(this)[0].id + "|";
        //            selectedAdmincompany += $(this)[0].name + "|";
        //            selectedAdmincompanyId += $(this)[0].value + "|";
        //            hashContract = { 'Key': $(this)[0].value, 'Value': $(this)[0].dataFld + ", " + $(this)[0].id + ", " + $(this)[0].name + ", " + $(this)[0].innerHTML };
        //            contractCollection.push(hashContract);
        //           // hashContractAndResource = { 'ContractId': $(this)[0].id, 'ResourceId': $(this)[0].innerHTML };
        //           // contractAndResourceCollection.push(hashContractAndResource);
        //            selectedRemoveitems = $(this)[0].id + ',' + $(this)[0].innerHTML;
        //          //  GetResourceAndContractIds(selectedRemoveitems, "Add");
        //        }
    });

 //   var tt = GetResourceAndContractIds(selectedValues);
    return selectedValues;
}
//Click Event Method
function LoadAddedResourceContract(clickevent) {

    if ($('#divAddedResourceContract')[0].innerHTML == "") {
        $('#spnResourceAddedResultLabel').html(listOfResourceContract);
        $('#spnResourceAddedResultLabel').show();
    }
    
    if (clickevent == "afterSave") {
        $('#divAddResourceContractButtons').hide();
        $('#divResourceSearchResults').hide();
        $('#divSearchResourceContract').hide();
        $('#divSaveResourceContractButtons').show();
        $('#divAddedResourceContract').show();
    }

    $('#divAddedResourceContract').jtable({
        paging: false,
        pageSize: 10,
        columnResizable: false,
        sorting: false,
        defaultSorting: 'ContractId ASC',
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true, selectOnRowClick: false,
        actions: {
            listAction: '/GCS/workgroup/GetResourceContractByContractIdList'
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
                title: mgResourceContractJtableColNames.ArtistId, //'Artist Id',
                width: '15%'
            },
            ResourceId: {
                title: mgResourceContractJtableColNames.ResourceId, //'Artist Id',
                width: '15%',
                list: false
            },
            Isrc:
                {
                    title: mgResourceContractJtableColNames.ISRC//"ISRC"
                },
                ResourceTitle:
                {
                    title: mgResourceContractJtableColNames.Title//"Title"
                },
                VersionTitle:
                {
                    title: mgResourceContractJtableColNames.VersionTitle//"Version Title"
                },
            ArtistName: {
                title: mgResourceContractJtableColNames.Artist, //'Artist',
                width: '10%'
            },
            ContractId: {
                title: mgResourceContractJtableColNames.ContractId, //'Contract Id',
                width: '15%'
            },
            ClearanceAdminCompanyName: {
                title: mgResourceContractJtableColNames.ClrAdminCompName, //'Clearance AdminCompany Name',
                width: '25%'
            },
            ISAC: {
                title: mgResourceContractJtableColNames.ClrAdminCompId, //'Clearance AdminCompany ID ',
                width: '25%'
            }
        },
        recordsLoaded: function (event, data) {
            selectedresourceContractIds = "";
            if (data.records.length > 0) {
                $("#divAddedResourceContract input").removeAttr("checked");
                $("#divAddedResourceContract tr").removeClass("jtable-row-selected");
                for (var i = 0; i < data.records.length; i++) {
                    selectedresourceContractIds += data.records[i].ContractId + ',' + data.records[i].ResourceId + '|';
                    //  selectedRemoveitems = selectedRemoveitems + record.ContractId + ',' + record.ResourceId + '|';
                }
            }
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
        }
    });
    if (clickevent == 'Add') {
 //        var arrSelectedContractIds = selectedValues.split('|');
//        var uniqueResourceContractIds = selectedresourceContractIds.split('|');
//        var selectedUniqueContractIds = '';
//        $.each(arrSelectedContractIds, function(index, selectedContractId){
//            if ($.inArray(selectedContractId, uniqueResourceContractIds) === -1) 
//            selectedUniqueContractIds = selectedUniqueContractIds + selectedContractId + '|';
//        });
        //         selectedValues = selectedUniqueContractIds + selectedresourceContractIds;
        if (selectedresourceContractIds != '') {
            selectedresourceContractIds = selectedresourceContractIds + selectedValues;
            GetResourceAndContractIds(selectedresourceContractIds, "Add");
        } else {
        GetResourceAndContractIds(selectedValues, "Add");
        }

         $('#divAddedResourceContract').jtable('load', {
             deviationResourceContract: JSON.stringify(contractAndResourceCollection),
// contractids: selectedValues,
 events: "Add"
        });
    }
else if (clickevent == 'delete') {
    var selectedRemoveitems = '';
        var $selectedRows = $('#divAddedResourceContract').jtable('selectedRows');
        if (selectedresourceContractIds != '') {
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    // selectedresourceContractIds = selectedresourceContractIds.replace(record.ContractId + '|', '');
                    selectedRemoveitems = selectedRemoveitems + record.ContractId + ',' + record.ResourceId + '|';
                });
                GetResourceAndContractIds(selectedRemoveitems,"Delete");
            }
            $('#divAddedResourceContract').jtable('load', {
                //contractids: selectedresourceContractIds,
                deviationResourceContract: JSON.stringify(contractAndResourceCollection),
                events: "delete"
            });
        }
//        else if (selectedresourceContractIds != "") {
//            var selectedArtist = selectedresourceContractIds;
//            if ($selectedRows.length > 0) {
//                $selectedRows.each(function () {
//                    var record = $(this).data('record');
//                    selectedArtist = selectedArtist.replace(record.ContractId + '|', '');
//                });
//            }
//            $('#divAddedResourceContract').jtable('load', {
//                contractids: selectedArtist,
//                events: "delete"
//            });
//        }
    }
    else if (clickevent == 'afterSave') {
        GetResourceAndContractIds(parent.document.getElementById('hdnManageResourceIds').value, "Insert");
       // contractAndResourceCollection.push(jQuery.parseJSON(parent.document.getElementById('hdnManageResourceIds').value));
        $('#divAddedResourceContract').jtable('load', {
           // contractids: parent.document.getElementById('hdnManageResourceIds').value,
            deviationResourceContract: parent.document.getElementById('hdnManageResourceIds').value,
            events: "save"
        });
    }
};

//Remove manage Resource
$(document).ready(function () {
    $('#btnRemoveResourceContract').click(function () {
        if ($('#divAddedResourceContract').jtable('selectedRows').length > 0) {
            LoadAddedResourceContract('delete');
        }
        else {
            $('#divAddResourceContractButtons').hide();
            $('#divResourceSearchResults').hide();
            $('#divSearchResourceContract').hide();
            $('#divSaveResourceContractButtons').show();
            $('#divAddedResourceContract').show();
            noRowsSelectedForDelete = "true";
            $("#mgResourcErrorMessage").show();
            $("#mgResourcErrorMessage").html(noRowsSelected);
            }
    });
});

//Save Manage Resource
function saveResourceContract() {
    $('#divManageResourceContract').jtable({
        paging: false,
        pageSize: 10,
        sorting: false,
        defaultSorting: 'ContractId ASC',
        selecting: true,
        columnResizable: false,
        multiselect: true,
        selectingCheckboxes: false,
        actions: {
            listAction: '/GCS/workgroup/GetResourceContractByContractIdList'
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
                title: mgResourceContractJtableColNames.ArtistId,//'Artist Id',
                width: '15%'
            },
            ArtistName: {
                title: mgResourceContractJtableColNames.Artist,//'Artist',
                width: '15%'
            },
            Isrc:
                {
                    title: mgResourceContractJtableColNames.ISRC//"ISRC"
                },
            ResourceTitle:
                {
                    title: mgResourceContractJtableColNames.Title//"Title"
                },
                VersionTitle:
                {
                    title: mgResourceContractJtableColNames.VersionTitle//"Version Title"
                },
            ContractId: {
                title: mgResourceContractJtableColNames.ContractId,//'Contract Id',
                width: '13%',
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
                                                        resizable:false//,
                                                        //height: 200
                                                    });
                                objDialog.html(data);
                            }
                        });
                    });
                }
            },
            ClearanceAdminCompanyName: {
                title: mgResourceContractJtableColNames.ClrAdminCompName,//'Clearance AdminCompany Name',
                width: '30%'
            },
            ISAC: {
                title: mgResourceContractJtableColNames.ClrAdminCompId,//'Clearance AdminCompany ID ',
                width: '40%'
            }
        },
        recordsLoaded: function (event, data) {
            selectedValues = '';
        }
    });

    // parent.document.getElementById('hdnManageResourceIds').value = selectedresourceContractIds;
    parent.document.getElementById('hdnManageResourceIds').value = JSON.stringify(contractAndResourceCollection);
    $('#divManageResourceContract').jtable('load', {
        deviationResourceContract: JSON.stringify(contractAndResourceCollection),
        events: "save"
    });
};

function GetResourceAndContractIds(contractAndResourceIds, event) {
    var hashContractAddAndRemove = {};
    if (event == 'Delete') {
        if (contractAndResourceIds != "") {
            $.each(contractAndResourceIds.split("|").slice(0, -1), function (i, contractAndResourceId) {
                // var contract = contractId;
                //$.each(contractAndResourceId.split(",").slice(0, -1), function (i, contractIdAndResourceID) {
                var objContractAndResourceId = contractAndResourceId.split(",");
                var resourceandcontractIds = JSLINQ(contractAndResourceCollection).Where(function (contracts) { return contracts.ContractId == objContractAndResourceId[0], contracts.ResourceId == objContractAndResourceId[1] });
                if (resourceandcontractIds.items.length != 0) {
                    //for (var i = 0; i < resourceandcontractIds.items.length; i++) {
                    //   hashContractAddAndRemove = { 'ContractId': objContractAndResourceId[0], 'ResourceId': objContractAndResourceId[1] };
                    // contractAndResourceCollection.pop(resourceandcontractIds.items[0]);
                    contractAndResourceCollection.splice($.inArray(resourceandcontractIds.items[0], contractAndResourceCollection), 1);
       
                    //resourceAndContractIds = resourceAndContractIds + "," + resourceandcontractIds.items[i].ContractIds + "|" + resourceandcontractIds.items[i].ResourceIds;
                    //  }
                }
                // });
            });
        }
    }
    else if (event == 'Add') {
    $.each(contractAndResourceIds.split("|").slice(0, -1), function (i, contractAndResourceId) {
        var objContractAndResourceId = contractAndResourceId.split(",");
        var resourceandcontractIds = JSLINQ(contractAndResourceCollection).Where(function (contracts) { return contracts.ContractId == objContractAndResourceId[0], contracts.ResourceId == objContractAndResourceId[1] });
        if (resourceandcontractIds.items.length == 0) {
            hashContractAddAndRemove = { 'ContractId': objContractAndResourceId[0], 'ResourceId': objContractAndResourceId[1] };
            // contractAndResourceCollection.push(hashContractAddAndRemove);
            contractAndResourceCollection.unshift(hashContractAddAndRemove);

        }
    });
    }
else {
    if (contractAndResourceIds != "")
    {
        var objContractAndResourceId = jQuery.parseJSON(contractAndResourceIds);
        $.each(objContractAndResourceId, function (id, obj) {
            hashRemoveContract = { 'ContractId': obj.ContractId, 'ResourceId': obj.ResourceId };
            contractAndResourceCollection.unshift(hashRemoveContract);
        });
     }
    }
}
