/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var messages = {
    repLink: 'Repertoire Link',
    created: 'Created By',
    changedDate: 'Last Change Date',
    viewValid: 'No row selected! Select row to view Contract'
};
var contractId;
var clearanceAdminComp;
var griddetails;
var selectedResources = [];
var count = 0;
var empty = 0;

$(document).ready(function () {
    reSizeContractWorkPage();
//    var strngContractDate = $('#commencementDateProj').html();
//    $('#commencementDateProj').html(formatDate(strngContractDate));
//    formatDate(strngContractDate);
    clearanceAdminComp = $('#hiddenClearance').val();
    contractId = $('#hiddenContract').val();
    $('#cancelRelease').click(function () {
        $('#relePopup').dialog('close');
    });

    $('.checkResources').each(function () {
        $(this).click(function () {
            if ($(this).is(':checked')) {
                resourceGridChecked($(this).parents('#accordion').find('.jtableGrid'));

            } else {
                resourceGridUnchecked($(this).parents('#accordion').find('.jtableGrid'));
            }
        });

    });
    $('#selectRelease').click(function () {
        if ($('#selectRelease').is(':checked')) {
            resourceChecked();

        } else {
            resourceUnchecked();
        }
    });


    $('#selectAllRelease').click(function () {
        if ($('#selectAllRelease').is(':checked')) {
            $('#selectRelease').attr('checked', true);
            resourceChecked();
        } else {
            $('#selectRelease').attr('checked', false);
            resourceUnchecked();
        }
    });

    $('#accordion .head .newHead').click(function (e) {
        e.preventDefault();
        $(this).parent().next().toggle();
        $(this).parent().find('a').toggleClass('iconBottom');
        return false;
    }).next().show();




  
    var pageSize = 6;
    $('.jtableGrid').each(function () {
        var rowCount = 0;
        griddetails = $(this);
        $(this).jtable({
            paging: true,
            pageSize: pageSize,
            sorting: true,
            addNewRecord: '+ Add new record',
            defaultSorting: 'Name ASC',
            selecting: true,
            selectingCheckboxes: true,
            multiselect: true,
            actions: {
                listAction: '/GCS/Release/GetAssociatedResources'
            },

            loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); $(".ui-jtable-loading").show(); },
            recordsLoaded: function (event, data) {
                $('.jtable .jtable-no-data-row').show();
                rowIndex = data.serverResponse.RowIndex;
                rowCount = data.serverResponse.TotalRecordCount;
                grsReleases(griddetails, rowIndex);
                $(this).parents('#accordion').find('.resultcount').html('Resource (' + rowCount + ')');
                $(".ui-jtable-loading").hide();
                setTool($('.tool'));
            },
            fields: {
                'Images': {
                    title: '',
                    sorting: false,
                    display: function (resource) {
                        var image;

                        if (resource.record.ResourceType == "AUDIO") {

                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/ResourceAudio.gif" >' + '<img  src="/Gcs/Images/linked_Contract.png" >');
                                var htmlText = "";
                                for (var a = 0; a < resource.record.LinkedContractDetails.length; a++) {
                                    var rea = /-?\d+/;
                                    var ma = rea.exec(resource.record.LinkedContractDetails[a].CommencementDate);
                                    var date = new Date(parseInt(ma[0]));
                                    var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                    htmlText = htmlText + 'Contracting Party:' + resource.record.LinkedContractDetails[a].ContractingParty + "<br>" +
                                        'Commencement Date:' + date.getDate().toString() + ' ' + monthArray[date.getMonth("mmm")] + ' ' + date.getFullYear().toString() + "<br>" +
                                            'Clearance Admin Company :' + resource.record.LinkedContractDetails[a].ClearanceCompanyCountry + "<br>";
                                }

                                $(image).prop("title", htmlText);
                                $(image).tooltip({ showURL: false, showBody: "-" });

                            }
                            else {
                                image = $('<img  src="/Gcs/Images/ResourceAudio.gif" title="Audio">');
                            }
                        }
                        if (resource.record.ResourceType == "VIDEO") {

                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/ResourceVideo.gif">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText1 = "";
                                for (var v = 0; v < resource.record.LinkedContractDetails.length; v++) {
                                    var rev = /-?\d+/;
                                    var mv = rev.exec(resource.record.LinkedContractDetails[v].CommencementDate);
                                    var vdate = new Date(parseInt(mv[0]));
                                    var monthArrayv = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                    htmlText1 = htmlText1 + 'Contracting Party:' + resource.record.LinkedContractDetails[v].ContractingParty + "<br>" +
                                       'Commencement Date:' + vdate.getDate().toString() + ' ' + monthArrayv[vdate.getMonth("mmm")] + ' ' + vdate.getFullYear().toString() + "<br>" +
                                            'Clearance Admin Company :' + resource.record.LinkedContractDetails[v].ClearanceCompanyCountry + "<br>";
                                }

                                $(image).prop("title", htmlText1);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/ResourceVideo.gif" title="Video">');
                            }
                        }
                        if (resource.record.ResourceType == "IMAGE") {

                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/ResourceImage.gif">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText2 = "";
                                for (var i = 0; i < resource.record.LinkedContractDetails.length; i++) {
                                    var rei = /-?\d+/;
                                    var mi = rei.exec(resource.record.LinkedContractDetails[i].CommencementDate);
                                    var datei = new Date(parseInt(mi[0]));
                                    var monthArray1 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                    htmlText2 = htmlText2 + 'Contracting Party:' + resource.record.LinkedContractDetails[i].ContractingParty + "<br>" +
                                        'Commencement Date:' + datei.getDate().toString() + ' ' + monthArray1[datei.getMonth("mmm")] + ' ' + datei.getFullYear().toString() + "<br>" +
                                            'Clearance Admin Company :' + resource.record.LinkedContractDetails[i].ClearanceCompanyCountry + "<br>";
                                }

                                $(image).prop("title", htmlText2);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/ResourceImage.gif" title="Image">');
                            }
                        }
                        if (resource.record.ResourceType == "MERCH") {

                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/merchandise.png">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText3 = "";
                                for (var m = 0; m < resource.record.LinkedContractDetails.length; m++) {
                                    var rem = /-?\d+/;
                                    var mm = rem.exec(resource.record.LinkedContractDetails[m].CommencementDate);
                                    var datem = new Date(parseInt(mm[0]));
                                    var monthArray2 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                    htmlText3 = htmlText3 + 'Contracting Party:' + resource.record.LinkedContractDetails[m].ContractingParty + "<br>" +
                                        'Commencement Date:' + datem.getDate().toString() + ' ' + monthArray2[datem.getMonth("mmm")] + ' ' + datem.getFullYear().toString() + "<br>" +
                                            'Clearance Admin Company :' + resource.record.LinkedContractDetails[m].ClearanceCompanyCountry + "<br>";
                                }

                                $(image).prop("title", htmlText3);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/merchandise.png" title="Merchandise">');
                            }
                        }
                        if (resource.record.ResourceType == "OTHER") {

                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/page.gif">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText4 = "";
                                for (var o = 0; o < resource.record.LinkedContractDetails.length; o++) {
                                    var reo = /-?\d+/;
                                    var mo = reo.exec(resource.record.LinkedContractDetails[o].CommencementDate);
                                    var dateo = new Date(parseInt(mo[0]));
                                    var monthArray4 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                    htmlText4 = htmlText4 + 'Contracting Party:' + resource.record.LinkedContractDetails[o].ContractingParty + "<br>" +
                                        'Commencement Date:' + dateo.getDate().toString() + ' ' + monthArray4[dateo.getMonth("mmm")] + ' ' + dateo.getFullYear().toString() + "<br>" +
                                            'Clearance Admin Company :' + resource.record.LinkedContractDetails[o].ClearanceCompanyCountry + "<br>";
                                }

                                $(image).prop("title", htmlText4);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/page.gif" title="Others">');
                            }
                        }
                        if (resource.record.ResourceType == "TEXT") {

                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/script_Text.png">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText5 = "";
                                for (var t = 0; t < resource.record.LinkedContractDetails.length; t++) {
                                    var ret = /-?\d+/;
                                    var mt = ret.exec(resource.record.LinkedContractDetails[t].CommencementDate);
                                    var datet = new Date(parseInt(mt[0]));
                                    var monthArray5 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                    htmlText5 = htmlText5 + 'Contracting Party:' + resource.record.LinkedContractDetails[t].ContractingParty + "<br>" +
                                        'Commencement Date:' + datet.getDate().toString() + ' ' + monthArray5[datet.getMonth("mmm")] + ' ' + datet.getFullYear().toString() + "<br>" +
                                            'Clearance Admin Company :' + resource.record.LinkedContractDetails[t].ClearanceCompanyCountry + "<br>";
                                }

                                $(image).prop("title", htmlText5);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/script_Text.png" title="Text">');
                            }
                        }

                        // ReSharper disable UsageOfPossiblyUnassignedValue
                        return image;
                        // ReSharper restore UsageOfPossiblyUnassignedValue
                    }
                },
                ResourceTitle: {
                    title: "Title"
                },
                VersionTitle: {
                    title: "Version Title"
                },
                ArtistName: {
                    title: "Artist"
                },
                Isrc: {
                    title: "ISRC"
                }
            },
            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#resourceGrid').jtable('selectedRows');
                $('#contractSelectList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#contractSelectList').append(record.ContractId);
                        //var recdisplay = document.getElementById('contractSelectList');
                        // recdisplay.style.display = 'none';

                    });
                } else {
                    //No rows selected
                    $('#contractSelectList').append(messages.viewValid);
                }
            }
        });
        count++;
        $(this).jtable('load',
            {
                releaseId: $(this).attr('title'),
                clearanceAdminCompany: clearanceAdminComp,
                rowIndex: -1,
                contractId: contractId
            });

        //        $(this).find ('#jqTablePager select').change(function () {
        //            pageSize = $('#jqTablePager select').val();
        //            $('#resourceGrid').jtable({ 'pageSize': pageSize });
        //            $('#resourceGrid').jtable('load');
        //        });
    });



    //BtnClick function
    $('.btnLinkResofRel').click(function () {

        GetResourceForReleases();
        if (count == empty) {
            alert("Please Select a Resource");
            return false;
        }
        LinkReleasesAssociatedResources();
       // $('#repPopupLinking').dialog('close');
         $('#projPropPopup').dialog('close');
    });

    $('.cancelRelease').click(function () {
       // $('#repPopupLinking').dialog('close');
        LinkReleasesAssociatedResources();
        
         $('#projPropPopup').dialog('close');
    });


});
function reSizeContractWorkPage() {
    var h = $(window).height();
    $(".scrollPropRelToRes").css('height', h - 120);
    // $("#resourceGrid").css('height', h - 175);
    //$(".jtable-main-container").css('height', h - 175);

}
function resourceChecked() {
    $('.jtableGrid').each(function () {
        var allRows = $(this).find('tr').addClass('jtable-row-selected');
        allRows.find('td.jtable-selecting-column input').attr('checked', true);
        var header = griddetails.find('th');
        header.find('td.jtable-column-header jtable-column-header-selecting').attr('checked', true);
    });
}

function resourceUnchecked() {
    $('.jtableGrid').each(function () {
        var allRows = $(this).find('tr').removeClass('jtable-row-selected');
        allRows.find('td.jtable-selecting-column input').attr('checked', false);
    });
}
function resourceGridChecked(grid) {
    var allRows = $(grid).find('tr').addClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', true);
    var header = $(grid).find('th');
    header.find('td.jtable-column-header jtable-column-header-selecting').attr('checked', true);
}

function resourceGridUnchecked(grid) {
    var allRows = $(grid).find('tr').removeClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', false);
    var header = $(grid).find('th');
    header.find('input').attr('checked', false);
}

function grsReleases(item, rowIndex) {

    $(item).jtable('reset',
            {
                releaseId: $(this).attr('title'),
                clearanceAdminCompany: clearanceAdminComp,
                rowIndex: rowIndex,
                contractId: contractId
            });
}

function GetResourceForReleases() {

    $('.jtableGrid').each(function () {
        var $selectedRows = $(this).jtable('selectedRows');
        if ($selectedRows.length > 0) {
            $selectedRows.each(function () {
                selectedResources.push($(this).data('record'));
            });

        }
        else {
            empty++;
        }

    });
    return false;
}

function LinkReleasesAssociatedResources() {
    //alert(selectedResources.length);
    
    $.ajax({
        url: "/GCS/WorkQueue/LinkAssociatedResources/" +contractId,
        type: 'POST',
        data: JSON.stringify(selectedResources),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert('Linking Process initiated sucessfully');
        },
        error: function (data) {
            //alert(data.responseText);
        }
    });
}
// Tool tip 
function setTool(term) {
    var len = $(term).text().length;
    if (len > 15) {
        var textString = $(term).text();
        if (textString.indexOf('') == 0)
            textString = textString.replace(/[;]/g, '; ');

        $(term).prop("title", textString);
        $(term).tooltip();
        $(term).text($(term).text().substr(0, 15) + '...');
    }
}


        