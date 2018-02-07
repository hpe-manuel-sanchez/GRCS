/// <reference path="../Scripts/Custom/LayoutRoot.js" />
/// <reference path="../Scripts/Custom/SearchRepertoire.js" />
/// <reference path="/GCS/Scripts/Custom/SearchContractPopup.js" />

var propMessages = {
    albumTitle: "Title",
    versionTitle: "Version Title",
    artistName: "Artist",
    upc: "UPC",
    isrc: "ISRC",
    all: "All",
    Filtermessage: "The search results matching the search criteria exceed from 1000 results. Please refine your search criteria and try again"
};
var selectedReleases = [];
var selectedResources = [];
var rowIndexRelease;
var rowIndexResource;
var contractId;
var postingInfo;
var projectId;
var formatDate;
$(document).ready(function () {
    var strngContractDate = $('#commencementDateProj').html();
    $('#commencementDateProj').html(formatDate(strngContractDate));
    formatDate(strngContractDate);
    contractId = $('#associatedContractId').val();
    projectId = $('#linkAssociatedProjectId').val();
    //Accordion function
    $('#accordion .head .newHead').click(function (e) {
        e.preventDefault();
        $(this).parent().next().toggle();
        $(this).parent().find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    pageSize = 6;

    //Release Grid
    $('#releaseGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/Release/GetAssociatedRelease'
        },

        loadingRecords: function () {
            $(".ui-jtable-loading").show();
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {
            if (data.serverResponse.R2rowsRetrieved >= 1000) {
                alert(propMessages.Filtermessage);
            }
            rowIndexRelease = data.serverResponse.RowIndex;
            grsSearchRelease(rowIndexRelease);
            var totalRowsRelease = data.serverResponse.TotalRecordCount;
            $('#releaseCount').html("Release(" + totalRowsRelease + ")");
            $('.jtable .jtable-no-data-row').show();
        },
        fields: {
            'Image': {
                title: '',
                sorting: false,
                display: function (test) {
                    var image;

                    if (test.record.IsAlreadyLinked) {
                        image = $('<img  src="/Gcs/Images/linked_Contract.png">'); //
                        var htmlText = "";
                        for (var i = 0; i < test.record.LinkedContractDetails.length; i++) {
                            var re = /-?\d+/;
                            var dateString = "";
                            var m = re.exec(test.record.LinkedContractDetails[i].CommencementDate);
                            if (m != null) {
                                var date = new Date(parseInt(m[0]));
                                var monthArray5 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                dateString = date.getDate().toString() + ' ' + monthArray5[date.getMonth("mmm")] + ' ' + date.getFullYear().toString();
                            }
                            if (test.record.LinkedContractDetails[i].ArtistName == '' || test.record.LinkedContractDetails[i].ArtistName == undefined) {
                                htmlText = htmlText + 'Contracting Party:' + test.record.LinkedContractDetails[i].ContractingParty + "<br>" +
                                    'Commencement Date:' + dateString + "<br>" + 'Data Admin Company :' + test.record.LinkedContractDetails[i].ClearanceCompanyCountry + "<br>";
                            } else {
                                htmlText = htmlText + 'Artist:' + test.record.LinkedContractDetails[i].ArtistName + "<br>" +
                                    'Commencement Date:' + dateString + "<br>" + 'Data Admin Company :' + test.record.LinkedContractDetails[i].ClearanceCompanyCountry + "<br>";
                            }

                            $(image).prop("title", htmlText);
                            $(image).tooltip({ showURL: false, showBody: "-" });
                        }
                    } else {
                        image = '';
                    }
                    return image;
                }
            },
            ReleaseTitle: {
                title: propMessages.albumTitle
            },
            VersionTitle: {
                title: propMessages.versionTitle
            },
            ArtistName: {
                title: propMessages.artistName
            },
            Upc: {
                title: propMessages.upc
            }
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('#releaseGrid').jtable('selectedRows');
            $('#contractSelectList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#contractSelectList').append(record.ContractId);
                });
            }
            else {
                //No rows selected
                $('#contractSelectList').empty();
            }
        }
    });
    $('#releaseGrid').jtable('load', {
        contractId: $('#associatedContractId').val(),
        projectId: projectId,
        rowIndex: -1,
        clearanceAdminCompany: $('#associatedClearanceId').val()
    });

    //Resource Grid
    $('#resourceGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/Resource/GetAssociatedResource'
        },

        loadingRecords: function () {
            $(".ui-jtable-loading").show();
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {
            if (data.serverResponse.R2rowsRetrieved >= 1000) {
                alert(propMessages.Filtermessage);
            }
            var totalRowsResource = data.serverResponse.TotalRecordCount;
            $('#resourceCount').html('Resource(' + totalRowsResource + ')');
            rowIndexResource = data.serverResponse.RowIndex;
            grsSearchResource(rowIndexResource);
            $(".ui-jtable-loading").hide();
            $('.jtable .jtable-no-data-row').show();
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
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('#contractSelectList').append(record.ContractId);
                    //var recdisplay = document.getElementById('contractSelectList');
                    // recdisplay.style.display = 'none';
                });
            }
            else {
                //No rows selected
                // $('#contractSelectList').append(messages.viewValid);
            }
        }
    });
    $('#resourceGrid').jtable('load', {
        contractId: $('#associatedContractId').val(),
        projectId: projectId,
        rowIndex: -1,
        clearanceAdminCompany: $('#associatedClearanceId').val()
    });

    $('.confirmProjectAssociated').click(function (e) {
        e.preventDefault();
        if (!GetReleaseandResource())
            return false;
        linkRelease();
        linkResource();
        if ($('#projPropPopup').find('#ProProjectLinking').val() == 'workQueue') {
            if ($('#projPropPopup').find('#hidHasAssociatedRel').val() == 'true') {
                propagataReleaseLinking(propQueryString);
            }
            $('#projPropPopup').dialog('close');
            $('#workQueueGrid').jtable('load', {
                artistName: $('#WorkQueues_ArtistName').val(),
                contractDesc: $('#WorkQueues_ContractDescription').val(),
                descTitle: $('#WorkQueues_Title').val(),
                reasonForReview: $('#WorkQueues_ContractReviewReason').val(),
                adminCompany: $('#WorkQueues_CompanyName').val()
            });
        }

        $('#loadingDiv').show();
        $('#linkProjectToContractId1').dialog('close');
        return true;
    });

    reSizeRepertoireWorkPage();

    $(window).resize(function () {
        reSizeRepertoireWorkPage();
    });

    $('.cancelLink').click(function () {
        $('#linkProjectToContractId1').dialog('close');
        window.location.href = '/GCS/Contract/SearchContract';
    });
});

function linkRelease() {
    $.ajax({
        url: '/GCS/Project/LinkAssociatedRelease/' + contractId,
        type: 'POST',
        data: JSON.stringify(selectedReleases),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            return true;
        },
        error: function (data) {
            // alert(data.responseText);
            return false;
        }
    });
}

function linkResource() {
    $.ajax({
        url: '/GCS/Project/LinkAssociatedResource/' + contractId,
        type: 'POST',
        data: JSON.stringify(selectedResources),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert('The system will now link the repertoire to the contract in the background.Press OK to continue');
            if ($('#projPropPopup').find('#ProProjectLinking').val() == 'workQueue') {
                if ($('#projPropPopup').find('#hidHasAssociatedRel').val() == 'true') {
                    propagataReleaseLinking(propQueryString);
                }
                $('#projPropPopup').dialog('close');
                $('#workQueueGrid').jtable('load', {
                    artistName: $('#WorkQueues_ArtistName').val(),
                    contractDesc: $('#WorkQueues_ContractDescription').val(),
                    descTitle: $('#WorkQueues_Title').val(),
                    reasonForReview: $('#WorkQueues_ContractReviewReason').val(),
                    adminCompany: $('#WorkQueues_CompanyName').val()
                });
            } else {
                window.location.href = '/GCS/Contract/SearchContract';
            }
        },
        error: function (data) {
            // alert(data.responseText);
        }
    });
}

function GetReleaseandResource() {
    var $selectedReleaseRows = $('#releaseGrid').jtable('selectedRows');
    var $selectedResourceRows = $('#resourceGrid').jtable('selectedRows');
    if ($selectedReleaseRows.length == 0 && $selectedResourceRows.length == 0) {
        alert('Please select a repertoire');
        return false;
    }
    else {
        if ($selectedReleaseRows.length > 0) {
            //Show selected rows
            $selectedReleaseRows.each(function () {
                selectedReleases.push($(this).data('record'));
            });
        }
        if ($selectedResourceRows.length > 0) {
            //Show selected rows
            $selectedResourceRows.each(function () {
                selectedResources.push($(this).data('record'));
            });
        }
        return true;
    }
}

function reSizeRepertoireWorkPage() {
    var h = $(window).height();
    //$("#projectContainer").css('height', h - 275);
    $(".mainProjectContainer").css('height', h - 175);
    //$(".jtable-main-container").css('height', h - 375);
}

function releaseChecked() {
    var allRows = $('#releaseGrid').find('tr').addClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', true);
    var header = $('.jqgrid').find('th');
    header.find('td.jtable-column-header input').attr('checked', true);
}

function releaseUnchecked() {
    var allRows = $('#releaseGrid').find('tr').removeClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', false);
}

function resourceChecked() {
    var allRows = $('#resourceGrid').find('tr').addClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', true);
    var header = $('.jqgrid1').find('th');
    header.find('td.jtable-column-header input').attr('checked', true);
}

function resourceUnchecked() {
    var allRows = $('#resourceGrid').find('tr').removeClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', false);
}

function projectChecked() {
    var allRows = $('#projectGrid').find('tr').addClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', true);
    var header = $('.jqgrid1').find('th');
    header.find('td.jtable-column-header input').attr('checked', true);
}

function projectUnchecked() {
    var allRows = $('#projectGrid').find('tr').removeClass('jtable-row-selected');
    allRows.find('td.jtable-selecting-column input').attr('checked', false);
}

$('#selectAllRepertoire ').click(function () {
    if ($('#selectAllRepertoire').is(':checked')) {
        $('#releaseCheckbox').attr('checked', true);
        $('#resourceCheckbox').attr('checked', true);
        $('#selectProject').attr('checked', true);
        releaseChecked();
        resourceChecked();
    }
    else {
        $('#releaseCheckbox').attr('checked', false);
        $('#resourceCheckbox').attr('checked', false);
        $('#selectProject').attr('checked', false);
        releaseUnchecked();
        resourceUnchecked();
    }
});

$('#selectProject ').click(function () {
    if ($('#selectProject').is(':checked')) {
        $('#releaseCheckbox').attr('checked', true);
        $('#resourceCheckbox').attr('checked', true);
        releaseChecked();
        resourceChecked();
    }
    else {
        $('#releaseCheckbox').attr('checked', false);
        $('#resourceCheckbox').attr('checked', false);
        releaseUnchecked();
        resourceUnchecked();
    }
});

function grsSearchRelease(rowIndexRelease) {
    $('#releaseGrid').jtable('reset', {
        contractId: $('#associatedContractId').val(),
        projectId: projectId,
        rowIndex: rowIndexRelease,
        clearanceAdminCompany: $('#associatedClearanceId').val()
    });
}

function grsSearchResource(rowIndexResource) {
    $('#releaseGrid').jtable('reset', {
        projectId: projectId,
        rowIndex: rowIndexResource,
        contractId: $('#associatedContractId').val(),
        clearanceAdminCompany: $('#associatedClearanceId').val()
    });
}

$(document).ready(function () {
});