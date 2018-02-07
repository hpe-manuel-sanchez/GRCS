/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

var checkstate;
$('#ReleaseSearch_IsArtistExactSearch').click(function () {
    if (this.checked == false) {
        checkstate = false;
    }
    else {
        checkstate = true;
    }
});

$('#searchrelease').click(function (e) {
    e.preventDefault();
    $('#releasehide').show();
    $('#reljqgrid').show();
    if (($('#ReleaseSearch_Upc').val() != '' || $('#ReleaseSearch_ArtistName').val() != '' || $('#ReleaseSearch_ReleaseTitle').val() != '' || $('#ReleaseSearch_ArtistId').val() != '' || $('#release_ConfigurationId').val() != '' || $('#ReleaseSearch').val() != '')) {
        $('#reljqgrid').jtable('load',
                {
                    upc: $('#ReleaseSearch_Upc').val(),
                    artistName: $('#ReleaseSearch_ArtistName').val(),
                    releaseTitle: $('#ReleaseSearch_ReleaseTitle').val(),
                    artistId: $('#ReleaseSearch_ArtistId').val(),
                    artistExactSearch: checkstate,
                    configurationGroup: $('#ReleaseSearch').val(),
                    configurationId: $('#release_ConfigurationId').val()
                });
    }
    else {
        alert("Please Enter a SearchCritearia");
    }
});

$(document).ready(function () {
    $('#reljqgrid').hide();
    var pageSize = 25;
    $('#ReleaseSearch_ArtistId').val('');
    $('#reljqgrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true,
        actions: {
            listAction: '/GCS/Release/ReleaseSearch'
        },

        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
        recordsLoaded: function () { $('.jtable .jtable-no-data-row').show(); },
        fields: {
            Upc: {
                title: 'UPC'
            },

            ReleaseTitle: {
                title: 'Release Title'
            },

            VersionTitle: {
                title: 'Version Title'
            },

            ArtistName: {
                title: 'Artist Name'
            },

            Configuration: {
                title: 'Configuration Id'
            },

            ScopeType: {
                title: 'Release Scope'
            },

            R2StatusType: {
                title: 'R2 Status'
            },

            DataAdminCompany: {
                title: 'Data Admin Company'
            },

            CatalogueNo: {
                title: 'CatalogueNo Id'
            },

            PYear: {
                title: 'P Year'
            },

            PCompanyId: {
                title: 'P Notice Company'
            },

            PLicensingExtension: {
                title: 'P Licensing Extension'
            }
        }
    });

    //Accordion style collapse/expand
    $('#releaseaccordion .head').click(function (e) {
        e.preventDefault();
        $(this).next().toggle();
        $(this).find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    var linkProjToReper = $('<div id="testpopupforrep"></div>')
                .html('<p>' + messageCommon.onLoading + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: "Please Confirm",
                    show: 'clip',
                    hide: 'clip',
                    width: "98%",
                    position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 50]
                });

    $('#unlinkRelRes').click(function (e) {
        e.preventDefault();
        //Load partial view
        linkProjToReper.load('/GCS/Project/PropagateLinkingFromProjectToRepertoire');
        linkProjToReper.dialog('option', { title: "Please Confirm" });
        //Open Dialog
        linkProjToReper.dialog('open');
        return false;
    });
});

//ResetButton
$('#resetRelease').click(function (e) {
    e.preventDefault();
    $('#ReleaseSearch_Upc').val('');
    $('#ReleaseSearch_ArtistName').val('');
    $('#ReleaseSearch_ReleaseTitle').val('');
    $('#ReleaseSearch_ArtistId').val('');
    $('#release_ConfigurationId')[0].selectedIndex = 0;
    $('#ReleaseSearch_IsArtistExactSearch').removeAttr('checked');
    $('#ReleaseSearch')[0].selectedIndex = 0;
});