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
    var strngContractDate = $('#commencementDateRel').html();
    $('#commencementDateRel').html(formatDate(strngContractDate));
    formatDate(strngContractDate);

    reSizeContractWorkPage();
    clearanceAdminComp = $('#loadClearanceCompanyId').val();
    contractId = $('#repertoireContractId').val();

    $('.checkResources').each(function () {
        $(this).click(function () {
            if ($(this).is(':checked')) {
                resourceGridChecked($(this).parents('#accordion').find('.jtableGrid'));
            } else {
                resourceGridUnchecked($(this).parents('#accordion').find('.jtableGrid'));
            }
        });
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

    $('#accordion .head .newHead')
        .click(function (e) {
            e.preventDefault();
            $(this).parent().next().toggle();
            $(this).parent().find('a').toggleClass('iconBottom');
            return false;
        })
        .next()
        .show();

    var pageSize = 6;
    $('.jtableGrid').each(function () {
        griddetails = $(this);
        var rowCount = 0;
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
            loadingRecords: function () {
                $('.jtable .jtable-no-data-row').hide();
                $(".ui-jtable-loading").show();
            },
            recordsLoaded: function (event, data) {
                $('.jtable .jtable-no-data-row').show();
                rowIndex = data.serverResponse.RowIndex;
                rowCount = data.serverResponse.TotalRecordCount;
                grsReleases(griddetails, rowIndex);
                $(".jtableGrid input").removeAttr("checked");
                $(".jtableGrid tr").removeClass("jtable-row-selected");
                $(".ui-jtable-loading").hide();
                setTool($('.tool'));
                $(this).parents('#accordion').find('.resultcount').html('Resource (' + rowCount + ')');
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
                                    var dateString5 = '';
                                    if (ma != null) {
                                        var date = new Date(parseInt(ma[0]));
                                        var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString5 = date.getDate().toString() + ' ' + monthArray[date.getMonth("mmm")] + ' ' + date.getFullYear().toString();
                                    }
                                    if (test.record.LinkedContractDetails[a].ArtistName == '' || test.record.LinkedContractDetails[a].ArtistName == undefined) {
                                        htmlText = htmlText + 'Contracting Party:' + resource.record.LinkedContractDetails[a].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString5 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[a].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText = htmlText + 'Artist:' + resource.record.LinkedContractDetails[a].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString5 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[a].ClearanceCompanyCountry + "<br>";
                                    }

                                    $(image).prop("title", "-", htmlText);
                                    $(image).tooltip({ showURL: false, showBody: "-" });
                                }
                            } else {
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
                                    var dateString4 = '';
                                    if (mv != null) {
                                        var vdate = new Date(parseInt(mv[0]));
                                        var monthArrayv = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString4 = vdate.getDate().toString() + ' ' + monthArrayv[vdate.getMonth("mmm")] + ' ' + vdate.getFullYear().toString();
                                    }

                                    if (test.record.LinkedContractDetails[v].ArtistName == '' || test.record.LinkedContractDetails[v].ArtistName == undefined) {
                                        htmlText1 = htmlText1 + 'Contracting Party:' + resource.record.LinkedContractDetails[v].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString4 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[v].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText1 = htmlText1 + 'Artist:' + resource.record.LinkedContractDetails[v].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString4 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[v].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText1);
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
                                    var dateString3 = '';

                                    if (mi != null) {
                                        var datei = new Date(parseInt(mi[0]));
                                        var monthArray1 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString3 = datei.getDate().toString() + ' ' + monthArray1[datei.getMonth("mmm")] + ' ' + datei.getFullYear().toString();
                                    }

                                    if (test.record.LinkedContractDetails[i].ArtistName == '' || test.record.LinkedContractDetails[i].ArtistName == undefined) {
                                        htmlText2 = htmlText2 + 'Contracting Party:' + resource.record.LinkedContractDetails[i].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString3 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[i].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText2 = htmlText2 + 'Artist:' + resource.record.LinkedContractDetails[i].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString3 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[i].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText2);
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
                                    var dateString2 = '';
                                    if (mm != null) {
                                        var datem = new Date(parseInt(mm[0]));
                                        var monthArray2 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString2 = datem.getDate().toString() + ' ' + monthArray2[datem.getMonth("mmm")] + ' ' + datem.getFullYear().toString();
                                    }
                                    if (test.record.LinkedContractDetails[m].ArtistName == '' || test.record.LinkedContractDetails[m].ArtistName == undefined) {
                                        htmlText3 = htmlText3 + 'Contracting Party:' + resource.record.LinkedContractDetails[m].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString2 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[m].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText3 = htmlText3 + 'Artist:' + resource.record.LinkedContractDetails[m].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString2 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[m].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText3);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/merchandise.png" title="Merchandise">');
                            }
                        }
                        if (resource.record.ResourceType == "OTHER") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/page.gif">' + '<img  src="/Gcs/Images/linked_Contract.png" height="16" width="16">');
                                var htmlText4 = "";
                                for (var o = 0; o < resource.record.LinkedContractDetails.length; o++) {
                                    var reo = /-?\d+/;
                                    var mo = reo.exec(resource.record.LinkedContractDetails[o].CommencementDate);
                                    var dateString1 = '';
                                    if (mo != null) {
                                        var dateo = new Date(parseInt(mo[0]));
                                        var monthArray4 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString1 = dateo.getDate().toString() + ' ' + monthArray4[dateo.getMonth("mmm")] + ' ' + dateo.getFullYear().toString();
                                    }
                                    if (test.record.LinkedContractDetails[o].ArtistName == '' || test.record.LinkedContractDetails[o].ArtistName == undefined) {
                                        htmlText4 = htmlText4 + 'Contracting Party:' + resource.record.LinkedContractDetails[o].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString1 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[o].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText4 = htmlText4 + 'Artist:' + resource.record.LinkedContractDetails[o].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString1 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[o].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText4);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/page.gif" title="Others" height="16" width="16">');
                            }
                        }
                        if (resource.record.ResourceType == "TEXT") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/script_Text.png">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText5 = "";
                                for (var t = 0; t < resource.record.LinkedContractDetails.length; t++) {
                                    var ret = /-?\d+/;
                                    var mt = ret.exec(resource.record.LinkedContractDetails[t].CommencementDate);
                                    var dateString = '';
                                    if (mt != null) {
                                        var datet = new Date(parseInt(mt[0])).getDate().toString();
                                        var monthArray5 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString = datet.getDate().toString() + ' ' + monthArray5[datet.getMonth("mmm")] + ' ' + datet.getFullYear().toString();
                                    }

                                    if (test.record.LinkedContractDetails[t].ArtistName == '' || test.record.LinkedContractDetails[t].ArtistName == undefined) {
                                        htmlText5 = htmlText5 + 'Contracting Party:' + resource.record.LinkedContractDetails[t].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[t].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText5 = htmlText5 + 'Artist:' + resource.record.LinkedContractDetails[t].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[t].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText5);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/script_Text.png" title="Text">');
                            }
                        }

                        return image;
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
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#contractSelectList').append(record.ContractId);
                        //var recdisplay = document.getElementById('contractSelectList');
                        // recdisplay.style.display = 'none';
                    });
                } else {
                    $('#contractSelectList').append(messages.viewValid);
                }
            }
        });
        count++;
        $(this).jtable('load', {
            releaseId: $(this).attr('title'),
            clearanceAdminCompany: clearanceAdminComp,
            rowIndex: -1,
            contractId: contractId
        });
    });

    //BtnClick function
    $('.btnLinkResofRel').click(function () {
        $('#relePopup').dialog('close');
        $('#repPopupLinking').dialog('close');
        GetResourceForReleases();
        if (count == empty) {
            alert("Please Select a Resource");
            return false;
        }
        LinkReleasesAssociatedResources();
    });

    $('.cancelRelease').click(function () {
        $('#relePopup').dialog('close');
        $('#repPopupLinking').dialog('close');
        LinkReleasesAssociatedResources();
        window.location.href = '/GCS/Contract/SearchContract';
    });
});

function reSizeContractWorkPage() {
    var h = $(window).height();
    $(".scrollPropRelToRes").css('height', h - 120);
}

function resourceChecked() {
    $('.jtableGrid').each(function () {
        var allRows = $(this).find('tr').addClass('jtable-row-selected');
        allRows.find('td.jtable-selecting-column input').attr('checked', true);
        var header = $(this).find('th');
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
    $(item).jtable('reset', {
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
        url: "/GCS/Release/LinkAssociatedResources/" + contractId,
        type: 'POST',
        data: JSON.stringify(selectedResources),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert('The system will now link the repertoire to the contract in the background.Press OK to continue');
            window.location.href = '/GCS/Contract/SearchContract';
        },
        error: function (data) {
            //alert(data.responseText);
        }
    });
}

// Tool tip
function setTool(term) {
    var len = $(term).text().length;
    if (len > 15) { }
    var textString = $(term).text();
    if (textString.indexOf('') == 0)
        textString = textString.replace(/[;]/g, '; ');

    $(term).prop("title", textString);
    $(term).tooltip();
    $(term).text($(term).text().substr(0, 15) + '...');
}