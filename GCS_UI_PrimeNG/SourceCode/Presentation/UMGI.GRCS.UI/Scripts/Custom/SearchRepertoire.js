var tabIndex = 1;
var contractId;
var selectedReleaseRecords = [];
var contract;
var artistName;
var commencementData;
var clearance;
var selectedResourceRecords = [];
var clearanceId;
var contractingParty;

$(document).ready(function () {
    $('#Search_Repertoire').html('Search for Projects');
    artistName = $('#loadArtistName').val();
    commencementData = $('#hiddenConCommencement').val();
    clearance = $('#loadClearanceCompanyName').val();
    contractId = $('#repertoireContractId').val();

    clearanceId = $('#loadClearanceCompanyId').val();
    contractingParty = $('#loadContractingParty').val();

    //Order Very Important!!!!
    contract = contractId + ',' + artistName + ',' + commencementData + ',' + clearance + ',' + clearanceId;

    $("table.sample tr:even").css("background-color", "#F6F7F9");
    $("table.sample tr:odd").css("background-color", "#EAF0F6");

    $('#searchContractNav').css('cursor', 'pointer');
    $('#searchContractNav').click(function (e) {
        e.preventDefault();
        window.location.href = "/GCS/Contract/SearchContract";
    });

    //Tab Change
    $("#repertoireTabs").tabs
    ({
        select: function (event, ui) {
            HideWarningSuccess();
            var h1 = $(window).height();
            $(".searchRepertoireContainer").css('height', h1 - 150);
            switch (ui.index) {
                case 0:
                    $('#Search_Repertoire').html('Search for Projects');
                    tabIndex = 1;
                    break;
                case 1:
                    $('#Search_Repertoire').html('Search for Releases');
                    tabIndex = 2;
                    if ($('#divReleaseTab').html() == "") {
                        ReloadReleaseTabs();
                    }
                    break;
                case 2:
                    tabIndex = 3;
                    $('#Search_Repertoire').html('Search for Resources');
                    if ($('#divResourceTab').html() == "") {
                        ReloadResourceTabs();
                    }
                    break;
            }
        }
    });

    //Accordion style collapse/expand

    $('#repertoireaccordion .head').click(function (e) {
        e.preventDefault();
        $(this).next().toggle();
        $(this).find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    $('#contractLink').click(function () {
        switch (tabIndex) {
            case 1:
                confirmLinkProject();
                break;
            case 2:
                if (GetRelease() == true)
                    confirmLinkRelease();
                break;
            case 3:
                if (GetResource() == true)
                    ConfirmLinkResource();
                break;
            default:
                break;
        }
    });
});

function ReloadTabs() {
    $.get('/GCS/Project/ReleaseTab', function (data) {
        $('#divReleaseTab').html(data);
    });
    $.get('/GCS/Project/ResourceTab', function (data) {
        $('#divResourceTab').html(data);
    });
}
function ReloadReleaseTabs() {
    $.get('/GCS/Project/ReleaseTab', function (data) {
        $('#divReleaseTab').html(data);
    });
}

function ReloadResourceTabs() {
    $.get('/GCS/Project/ResourceTab', function (data) {
        $('#divResourceTab').html(data);
    });
}

//Project Starts//
function confirmLinkProject() {
    if ($('#hiddenProjectInfo').html() != '') {
        var confirmLinkDialog = $('<div id="ConfirmLinkDialogId"></div>')
            .html('<p>' + messageCommon.onLoading + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: "Confirm",
                show: 'clip',
                hide: 'clip',
                width: 300
            });
        confirmLinkDialog.html("Do you also want to select any of this project's repertoire to link to the contract?");
        confirmLinkDialog.dialog({
            buttons: {
                'Select Repertoire': function (e) {
                    e.preventDefault();
                    $('#loadingDiv').show();
                    $.ajax({
                        url: "/GCS/Project/LinkContract?item=" + contract,
                        type: 'POST',
                        data: JSON.stringify(selectedProjectRecord),
                        async: true,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            LinkProjectToContract(data);
                            $('#loadingDiv').hide();
                        },
                        error: function (data) {
                            alert('Error in loading');
                            $('#loadingDiv').hide();
                        }
                    });
                    $(this).dialog('close');
                },
                'Link Project Only': function (e) {
                    e.preventDefault();
                    $.ajax({
                        url: "/GCS/Project/LinkContract?item=" + contract,
                        type: 'POST',
                        data: JSON.stringify(selectedProjectRecord),
                        async: true,
                        contentType: 'application/json; charset=utf-8',
                        success: function () {
                            alert('The system will now link the repertoire to the contract in the background.Press OK to continue');
                        },
                        error: function () {
                            alert('Error in loading');
                        }
                    });
                    $('#ConfirmLinkDialogId').dialog('close');
                }
            }
        });
        confirmLinkDialog.dialog('open');
    } else {
        ShowWarning('Please select a Project to Link');
    }
}

function LinkProjectToContract(content) {
    var linkProjDialog = $('<div id="linkProjectToContractId1"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: "Please Confirm",
            show: 'clip',
            hide: 'clip',
            width: "98%",
            position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50],
            close: function () {
                $(this).remove();
            }
        });
    //Load partial view
    linkProjDialog.html(content);
    linkProjDialog.dialog('open');
}
//Project Ends//
//Resource Starts//

function GetResource() {
    selectedResourceRecords = [];
    var $selectedRows = $('#resouceGrid').jtable('selectedRows');
    if ($selectedRows.length > 0) {
        $selectedRows.each(function () {
            HideWarningSuccess();
            selectedResourceRecords.push($(this).data('record'));
        });
        return true;
    } else {
        ShowWarning('Please select a resource to link');
        return false;
    }
}

function ConfirmLinkResource() {
    $.ajax({
        url: "/GCS/Project/IsContractLinked/" + contractId,
        type: 'POST',
        data: JSON.stringify(selectedResourceRecords),
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if ($(data).find('#hiddenCount').val() > 0) {
                LinkResource(data);
            }
            else {
                alert('The system will now link the repertoire to the contract in the background.Press OK to continue');
                window.location.href = '/GCS/Contract/SearchContract';
            }
        },
        error: function (data) {
            alert('Error in loading');
        }
    });
}

function LinkResource(content) {
    var propResource = $('<div id="resourcePopup"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Please Confirm',
            show: 'clip',
            hide: 'clip',
            width: 500
        });
    //  Load partial view
    propResource.html(content);
    propResource.dialog('open');
    return false;
}

//Resource Ends//

//Release Starts//

function GetRelease() {
    selectedReleaseRecords = [];
    var $selectedRows = $('#reljqgrid').jtable('selectedRows');
    if ($selectedRows.length > 0) {
        $selectedRows.each(function () {
            HideWarningSuccess();
            selectedReleaseRecords.push($(this).data('record'));
        });
        return true;
    } else {
        ShowWarning('Please select a release to link');
        return false;
    }
}

function confirmLinkRelease() {
    var confirmLinkrelDialog = $('<div id="confirmLinkrelDialog"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: "Confirm",
            show: 'clip',
            hide: 'clip',
            width: 300
        });
    confirmLinkrelDialog.html("Do you also want to select any associated resources to link to the contract?");
    confirmLinkrelDialog.dialog({
        buttons: {
            'Select Resources': function (e) {
                $(this).dialog('close');
                e.preventDefault();
                $('#loadingDiv').show();
                $.ajax({
                    url: "/GCS/Release/IsContractLinked?term=" + contract,
                    type: 'POST',
                    data: JSON.stringify(selectedReleaseRecords),
                    async: true,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        LinkReleaseToResource(data);
                    },
                    error: function () {
                        alert('Error in loading');
                    }
                });
            },
            'Link Release(s) Only': function (e) {
                $(this).dialog('close');
                e.preventDefault();
                $.ajax({
                    url: "/GCS/Release/LinkReleases?term=" + contract,
                    type: 'POST',
                    data: JSON.stringify(selectedReleaseRecords),
                    async: true,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        data = data.replace('<!DOCTYPE html>', '');
                        var item = '<div>' + data + '</div>';
                        var count = $(item).find('#hiddenLinkedRepertoireCount').val();
                        if (count > 0 && count != undefined) {
                            LinkReleaseToResource(data);
                        }
                        else {
                            alert('The system will now link the repertoire to the contract in the background.Press OK to continue');
                            window.location.href = '/GCS/Contract/SearchContract';
                        }
                    },
                    error: function () {
                        alert('Error in loading');
                    }
                });
            }
        }
    });
    confirmLinkrelDialog.dialog('open');
}

function LinkReleaseToResource(content) {
    var proplinkingRel = $('<div id="relePopup"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Please Confirm',
            show: 'clip',
            hide: 'clip',
            width: "98%",
            position: [(($(window).width() - ($(window).width() * 98) / 100) / 2), 25],
            close: function () {
                $(this).remove(); // ensures any form variables are reset.
            }
        });
    //  Load partial view
    proplinkingRel.html(content);
    proplinkingRel.dialog('open');
}

function PropagateUnlinking() {
    var propUnlinkingRel = $('<div id="repPopup"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Please Confirm',
            show: 'clip',
            hide: 'clip',
            width: "98%",
            position: [(($(window).width() - ($(window).width() * 98) / 100) / 2), 25]
        });
    //  Load partial view

    propUnlinkingRel.load('/GCS/Project/PropagateLinkingReleaseToResource', "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                propUnlinkingRel.html('<p>' + messageCommon.error + '</p>');
            }
        }
    );
    propUnlinkingRel.dialog('open');
    return false;
}