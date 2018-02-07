/// <reference path="../Scripts/Custom/LayoutRoot.js" />

//Accordion style collapse/expand
$('#accordion .head ').click(function (e) {
    e.preventDefault();
    $(this).next().toggle();
    $(this).find('a').toggleClass('iconBottom');
    return false;
}).next().show();

var selectedUnlinkProjectRows = [];
var selectedUnlinkReleaseRows = [];
var selectedUnlinkResourceRows = [];
var strngselectedUnlinkProject;
var strngselectedUnlinkRelease;
var strngselectedUnlinkResource;
var strngSelectedRepertoires;
var unlinkContractId;
var arrayRepertoire = [];

$(document).ready(function () {//10000014
    unlinkContractId = $('#Contract_ContractId').val();
    reSizeRepertoireWorkPage();
    var rowCount = -1;
    $(window).resize(function () {
        reSizeRepertoireWorkPage();
    });
    var pageSize = 6;
    //ProjectGrid
    $('#unlinkProjectGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/contract/UnlinkProject'
        },

        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function (event, data) {
            rowCount = data.serverResponse.TotalRecordCount;
            document.getElementById("unlinkProjCount").innerHTML = "Project (" + rowCount + ")";
            $('.jtable .jtable-no-data-row').show();
        },
        fields: {
            Title: {
                title: "Description"
            },

            ArtistName: {
                title: "Artist Name"
            },

            ProjectCode: {
                title: "Project Id"
            }
        }
        //  Register to selectionChanged event to hanlde events
        //        selectionChanged: function () {
        //            //Get all selected rows
        //            var $selectedRows = $('#unlinkProjectGrid').jtable('selectedRows');
        //            $('#contractSelectList').empty();
        //            if ($selectedRows.length > 0) {
        //                //Show selected rows
        //                $selectedRows.each(function () {
        //                    var record = $(this).data('record');
        //                    $('#contractSelectList').append(record.ProjectId);
        //                    projectIds.push(record.ProjectId);
        //                    //var recdisplay = document.getElementById('contractSelectList');
        //                    // recdisplay.style.display = 'none';
        //                });
        //            }
        //            else {
        //                //No rows selected
        //                //$('#contractSelectList').append(messages.viewValid);
        //            }

        //        }
    });
    $('#unlinkProjectGrid').jtable('load',
    {
        contractId: unlinkContractId
    });

    //Resource Grid
    $('#unlinkResourceGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/contract/UnlinkResource'
        },

        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function (event, data) {
            rowCount = data.serverResponse.TotalRecordCount;
            document.getElementById("unlinkResCount").innerHTML = "Resource (" + rowCount + ")";
            $('.jtable .jtable-no-data-row').show();
        },
        fields: {
            'Images': {
                title: '',
                sorting: false,
                display: function (resource) {
                    var image;

                    if (resource.record.ResourceType == "AUDIO") {
                        image = $('<img  src="/Gcs/Images/ResourceAudio.gif" title="Audio">');
                    }

                    if (resource.record.ResourceType == "VIDEO") {
                        image = $('<img  src="/Gcs/Images/ResourceVideo.gif" title="Video">');
                    }

                    if (resource.record.ResourceType == "IMAGE") {
                        image = $('<img  src="/Gcs/Images/ResourceImage.gif" title="Image">');
                    }
                    if (resource.record.ResourceType == "MERCHANDISE") {
                        image = $('<img  src="/Gcs/Images/merchandise.png" title="Merchandise">');
                    }
                    if (resource.record.ResourceType == "OTHER") {
                        image = $('<img  src="/Gcs/Images/page.gif" title="Others">');
                    }
                    if (resource.record.ResourceType == "TEXT") {
                        image = $('<img  src="/Gcs/Images/script_Text.png" title="Text">');
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
                title: "Artist Name"
            },
            Isrc: {
                title: "ISRC"
            }
        }
        //Register to selectionChanged event to hanlde events
        //        selectionChanged: function () {
        //            //Get all selected rows
        //            var $selectedRows = $('#unlinkResourceGrid').jtable('selectedRows');
        //            $('#contractSelectList').empty();
        //            if ($selectedRows.length > 0) {
        //                //Show selected rows
        //                $selectedRows.each(function () {
        //                    var record = $(this).data('record');
        //                    $('#contractSelectList').append(record.ContractId);
        //                    //var recdisplay = document.getElementById('contractSelectList');
        //                    // recdisplay.style.display = 'none';

        //                });
        //            }
        //            else {
        //                //No rows selected
        //                // $('#contractSelectList').append(messages.viewValid);
        //            }
        //        }
    });
    $('#unlinkResourceGrid').jtable('load', {
        contractId: unlinkContractId
    });

    //Release Grid
    $('#unlinkReleaseGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        actions: {
            listAction: '/GCS/contract/UnlinkRelease'
        },

        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function (event, data) {
            rowCount = data.serverResponse.TotalRecordCount;
            document.getElementById("unlinkRelCount").innerHTML = "Release (" + rowCount + ")";
            $('.jtable .jtable-no-data-row').show();
        },
        fields: {
            ReleaseTitle: {
                title: "Title"
            },
            VersionTitle: {
                title: "Version Title"
            },
            ArtistName: {
                title: "Artist Name"
            },
            Upc: {
                title: "UPC"
            }
        }
        //Register to selectionChanged event to hanlde events
        //        selectionChanged: function () {
        //            //Get all selected rows
        //            var $selectedRows = $('#unlinkReleaseGrid').jtable('selectedRows');
        //            $('#contractSelectList').empty();
        //            if ($selectedRows.length > 0) {
        //                //Show selected rows
        //                $selectedRows.each(function () {
        //                    var record = $(this).data('record');
        //                    $('#contractSelectList').append(record.ContractId);
        //                    //var recdisplay = document.getElementById('contractSelectList');
        //                    // recdisplay.style.display = 'none';

        //                });
        //            }
        //            else {
        //                //No rows selected
        //                //$('#contractSelectList').append(messages.viewValid);
        //            }
        //        }
    });
    $('#unlinkReleaseGrid').jtable('load',
        {
            contractId: unlinkContractId
        });

    $('#selectAllRepertoire').click(function () {
        if ($('#selectAllRepertoire').is(':checked')) {
            var allRows = $('#unlinkReleaseGrid').find('tr').addClass('jtable-row-selected');
            allRows.find('td.jtable-selecting-column input').attr('checked', true);
            var allRows2 = $('#unlinkResourceGrid').find('tr').addClass('jtable-row-selected');
            allRows2.find('td.jtable-selecting-column input').attr('checked', true);
            var allRows4 = $('#unlinkProjectGrid').find('tr').addClass('jtable-row-selected');
            allRows4.find('td.jtable-selecting-column input').attr('checked', true);
        } else {
            var allRows1 = $('#unlinkReleaseGrid').find('tr').removeClass('jtable-row-selected');
            allRows1.find('td.jtable-selecting-column input').attr('checked', false);
            var allRows3 = $('#unlinkResourceGrid').find('tr').removeClass('jtable-row-selected');
            allRows3.find('td.jtable-selecting-column input').attr('checked', false);
            var allRows5 = $('#unlinkProjectGrid').find('tr').removeClass('jtable-row-selected');
            allRows5.find('td.jtable-selecting-column input').attr('checked', false);
        }
    });

    $('.btncancelUnlink').click(function () {
        $('#UnlinkContractFromRep').dialog('close');
    });

    // Unlink Button
    $('.btnUnlinkContract').click(function () {
        //Emptying on Click
        strngselectedUnlinkProject = '';
        strngselectedUnlinkRelease = '';
        strngselectedUnlinkResource = '';
        strngSelectedRepertoires = '';
        //Getting Selected Rows
        var $selectedUnlinkProjects = $('#unlinkProjectGrid').jtable('selectedRows');
        var $selectedUnlinkReleases = $('#unlinkReleaseGrid').jtable('selectedRows');
        var $selectedUnlinkResources = $('#unlinkResourceGrid').jtable('selectedRows');
        if ($selectedUnlinkProjects.length > 0 || $selectedUnlinkReleases.length > 0 || $selectedUnlinkResources.length > 0) {
            //Getting Values From a Record
            $selectedUnlinkProjects.each(function () {
                var record = $(this).data('record');
                if (strngselectedUnlinkProject != '') {
                    strngselectedUnlinkProject = strngselectedUnlinkProject + ',' + record.ProjectId; //TODO replace with ProjectId
                }
                else {
                    strngselectedUnlinkProject = record.ProjectId;
                }
            });
            $selectedUnlinkReleases.each(function () {
                var record = $(this).data('record');
                if (strngselectedUnlinkRelease != '') {
                    strngselectedUnlinkRelease = strngselectedUnlinkRelease + ',' + record.ReleaseId; //TODO replace with ProjectId
                }
                else {
                    strngselectedUnlinkRelease = record.ReleaseId;
                }
            });
            $selectedUnlinkResources.each(function () {
                var record = $(this).data('record');
                if (strngselectedUnlinkResource != '') {
                    strngselectedUnlinkResource = strngselectedUnlinkResource + ',' + record.ResourceId; //TODO replace with ProjectId
                }
                else {
                    strngselectedUnlinkResource = record.ResourceId;
                }
            });
            // alert(strngSelectedRepertoires);
            // strngSelectedRepertoires = strngselectedUnlinkProject + ':' + strngselectedUnlinkRelease + ':' + strngselectedUnlinkResource;
            arrayRepertoire.push(strngselectedUnlinkProject);
            arrayRepertoire.push(strngselectedUnlinkRelease);
            arrayRepertoire.push(strngselectedUnlinkResource);
            //alert(strngSelectedRepertoires);
            unlinkContractFromRepertoire(strngSelectedRepertoires);
            $('#UnlinkContractFromRep').dialog('close');
        }
        else {
            //            alert('Please select a repertoire to unlink');
            $('#UnlinkAlert').empty();
            $('#UnlinkAlert').append('Please select a repertoire to unlink');
            $('#unlinkWarning').show();
            $('#UnlinkAlert').show();
        }
    });
});

//Document ReadyEnds
var forms;
function unlinkContractFromRepertoire(strngSelectedRepertoires) {
    $.ajax({
        url: "/GCS/Contract/UnlinkContract/" + unlinkContractId,
        type: 'POST',
        data: JSON.stringify(arrayRepertoire),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            ShowSuccess('Repertoire has been successfully unlinked');
            return false;
        },
        error: function (data) {
            // alert("error"+data.responseText);
        }
    });
}

function reSizeRepertoireWorkPage() {
    var h = $(window).height();

    if ($('#unlinkWarning').css("display") == 'none')
        $(".scrollUnlinkRep").css('height', h - 120);
    else
        $(".scrollUnlinkRep").css('height', h - 195);
    // $(".scrollUnlinkRep").css('height', h - 100);
    //$("#testpopup").css('height', 350);
    //$(".jtable-main-container").css('height', 200);
}

//function releaseChecked() {
//    var allRows = $('#releaseGrid').find('tr').addClass('jtable-row-selected');
//    allRows.find('td.jtable-selecting-column input').attr('checked', true);
//}

//function releaseUnchecked() {
//    var allRows1 = $('#releaseGrid').find('tr').removeClass('jtable-row-selected');
//    allRows1.find('td.jtable-selecting-column input').attr('checked', false);
//}

//function resourceChecked() {
//    var allRows2 = $('#resourceGrid').find('tr').addClass('jtable-row-selected');
//    allRows2.find('td.jtable-selecting-column input').attr('checked', true);
//}

//function resourceUnchecked() {
//    var allRows3 = $('#resourceGrid').find('tr').removeClass('jtable-row-selected');
//    allRows3.find('td.jtable-selecting-column input').attr('checked', false);
//}

//function projectChecked() {
//    var allRows4 = $('#projectGrid').find('tr').addClass('jtable-row-selected');
//    allRows4.find('td.jtable-selecting-column input').attr('checked', true);
//}

//function projectUnchecked() {
//    var allRows5 = $('#projectGrid').find('tr').removeClass('jtable-row-selected');
//    allRows5.find('td.jtable-selecting-column input').attr('checked', false);
//}