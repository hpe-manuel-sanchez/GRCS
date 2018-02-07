/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var projecttabmessages = {
    ProjectCode: "Project ID", ArtistName: "Artist Name", Title: "Title", DataAdminCompany: "Data Admin Company", Label: "Label",
    Dataadminmessage: "Please enter Data Admin Company", Searchcriteriamessage: "Please specify atleast one search criteria", Filtermessage: "The search results matching the search criteria exceed from 1000 results. Please refine your search criteria and try again"
};
var rowIndex;

var selectedProjectRecord;
$(document).ready(function () {
    // $('#ProjectInfo_ArtistName').focus();
    HideWarningSuccess();
    var contractArtistId = $('#loadArtistId').val();
    $('#ProjectInfo_ArtistName').val($('#loadArtistName').val());
    var pageSize = 25;
    var rowCount = -1;
    reSizeSearchPage();
    pageSize = pageSize;
    $('#ProjectInfo_ProjectCode').val('');
    $('#projectgrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'Name ASC',
        selecting: true,
        selectOnRowClick: true,
        selectingCheckboxes: true,
        loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); $(".ui-jtable-loading").show(); },
        recordsLoaded: function (event, data) {
            if (data.serverResponse.R2rowsRetrieved >= 1000) {
                alert(projecttabmessages.Filtermessage);
            }
            $(".ui-jtable-loading").hide();
            var rowIndex = data.serverResponse.RowIndex;
            grsprojectSearch(rowIndex);
            rowCount = data.serverResponse.TotalRecordCount;
            document.getElementById("projectResultCount").innerHTML = "Search Results(" + rowCount + ")";
            $('.jtable .jtable-no-data-row').show();
            setToolTip(this);
        },
        error: function () {
            alert('err');
        },

        actions:
        {
            listAction: '/GCS/Project/RepertoireSearch'
        },
        fields:
        {
            ProjectCode: {
                title: projecttabmessages.ProjectCode
            },

            ArtistName: {
                title: projecttabmessages.ArtistName
            },

            Title: {
                title: projecttabmessages.Title
            },

            DataAdminCompany: {
                title: projecttabmessages.DataAdminCompany
            },

            Label: {
                title: projecttabmessages.Label
            }
        },
        selectionChanged: function () {
            //Get all selected rows
            HideWarningSuccess();
            $('#hiddenProjectInfo').empty();
            var $selectedRows = $('#projectgrid').jtable('selectedRows');
            if ($selectedRows.length > 0) {
                //                $selectedRows.each(function () {
                //                    selectedProjectRecord.push($(this).data('record'));
                //                });
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    selectedProjectRecord = record;
                    $('#hiddenProjectInfo').append(record.ProjectId);
                });
            }
            else {
                //No rows selected
                $('#hiddenProjectInfo').empty();
            }
        }
    });
    //Show items per page

    $('#projectPager select').change(function () {
        pageSize = $('#projectPager select').val();
        HideWarningSuccess();
        $('#projectgrid').jtable({ 'pageSize': pageSize });
        $('#projectgrid').jtable('load',
            {
                clearanceCompanyCountryId: $('#loadClearanceCompanyId').val(),
                projectCode: $('#ProjectInfo_ProjectCode').val(),
                artistName: $('#ProjectInfo_ArtistName').val(),
                projectTitle: $('#ProjectInfo_Title').val(),
                labelId: $('#ProjectInfo_LabelId').val(),
                dataAdmincompany: $('#ProjectInfo_AdminCompanyId').val(),
                rowIndex: rowIndex
            });
    });

    //Accordion style collapse/expand
    $('#projectaccordion .head').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        $(this).next().toggle();
        // $(this)._toggleClass('iconBottom');
        $(this).find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    if (contractArtistId != "") {
        $('#projectgrid').jtable('load',
                {
                    clearanceCompanyCountryId: $('#loadClearanceCompanyId').val(),
                    artistId: contractArtistId,
                    artistName: $('#loadArtistName').val(),
                    rowIndex: -1
                });
    }

    $('#searchProject').click(function () {
        HideWarningSuccess();
        if ($('#ProjectInfo_ArtistName').val() != "" || $('#ProjectInfo_ProjectCode').val() != "" || $('#ProjectInfo_Title').val() != "" || $('#ProjectInfo_Label').val() != "" || $('#ProjectInfo_DataAdminCompany').val() != "") {
            if (($('#ProjectInfo_Label').val() != "" && $('#ProjectInfo_DataAdminCompany').val() == "")) {
                ShowWarning(projecttabmessages.Dataadminmessage);
                return false;
            }
            ProjectSearch();
            reSizeSearchPage();
        }
        else {
            //  alert(projecttabmessages.Searchcriteriamessage);
            ShowWarning(projecttabmessages.Searchcriteriamessage);
            return false;
        }
    });
    $("#ProjectInfo_ProjectCode, #ProjectInfo_ArtistName, #ProjectInfo_Title, #ProjectInfo_Label, #ProjectInfo_DataAdminCompany, #ProjectInfo_AdminCompanyId, #ProjectInfo_LabelId  ")
          .bind("keyup", HideWarningSuccess);
    $(window).resize(function () {
        reSizeSearchPage();
    });

    //AutoComplete Data Admin Company
    var target1 = $("#ProjectInfo_DataAdminCompany");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#ProjectInfo_DataAdminCompany").val(ui.item.value);
            $("#ProjectInfo_AdminCompanyId").val(ui.item.id);
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#ProjectInfo_DataAdminCompany").val(""); $("#ProjectInfo_AdminCompanyId").val('');
            }
            else if (ui.item != null && $("#ProjectInfo_DataAdminCompany").val() != ui.item.value) {
                $("#ProjectInfo_DataAdminCompany").val(""); $("#ProjectInfo_AdminCompanyId").val('');
            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#ProjectInfo_DataAdminCompany").val(ui.item.value);
                $("#ProjectInfo_AdminCompanyId").val(ui.item.id);
            }
        }
    });

    //ResetButton
    $('#resetProject').click(function (e) {
        HideWarningSuccess();
        e.preventDefault();
        $('#ProjectInfo_ProjectCode').val('');
        $('#ProjectInfo_ArtistName').val('');
        $('#ProjectInfo_Title').val('');
        $('#ProjectInfo_Label').val('');
        $('#ProjectInfo_DataAdminCompany').val('');
        $("#ProjectInfo_AdminCompanyId").val('');
        $("#ProjectInfo_LabelId").val('');
    });

    //AutoComplete Label
    var target2 = $("#ProjectInfo_Label");
    target2.autocomplete({
        source: target2.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#ProjectInfo_Label").val(ui.item.value);
            $("#ProjectInfo_LabelId").val(ui.item.id);
            //  $("#ProjectInfo_Label").val('tes');
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#ProjectInfo_Label").val(""); $("#ProjectInfo_LabelId").val('');
            }
            else if (ui.item != null && $("#ProjectInfo_Label").val() != ui.item.value) {
                $("#ProjectInfo_Label").val(""); $("#ProjectInfo_LabelId").val('');
            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#ProjectInfo_Label").val(ui.item.value);
                $("#ProjectInfo_LabelId").val(ui.item.id);
            }
        }
    });
});

function ProjectSearch() {
    HideWarningSuccess();
    $('#projectgrid').jtable('load',
                    {
                        clearanceCompanyCountryId: $('#loadClearanceCompanyId').val(),
                        projectCode: $('#ProjectInfo_ProjectCode').val(),
                        artistName: $('#ProjectInfo_ArtistName').val(),
                        projectTitle: $('#ProjectInfo_Title').val(),
                        labelId: $('#ProjectInfo_LabelId').val(),
                        dataAdmincompany: $('#ProjectInfo_AdminCompanyId').val(),
                        rowIndex: -1
                    });
}

function grsprojectSearch(rowIndex) {
    $('#projectgrid').jtable('reset',
                    {
                        clearanceCompanyCountryId: $('#loadClearanceCompanyId').val(),
                        projectCode: $('#ProjectInfo_ProjectCode').val(),
                        artistName: $('#ProjectInfo_ArtistName').val(),
                        projectTitle: $('#ProjectInfo_Title').val(),
                        labelId: $('#ProjectInfo_LabelId').val(),
                        dataAdmincompany: $('#ProjectInfo_AdminCompanyId').val(),
                        rowIndex: rowIndex
                    });
}

function reSizeSearchPage() {
    var h = $(window).height();

    $(".searchRepertoireContainer").css('height', h - 150);
}