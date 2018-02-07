var SelectedValues = "";
var pageCount = 0;
var rowIndex = -1;

$(function () {
    $('#btnSearchReset').click(function (e) {
        $('#txtUpcChild').val('');
        $('#txtArtistNameChild').val('');
        $('#txtReleaseTitleChild').val('');
        $('#txtArtistIDChild').val('');
        $('#txtR2ProjectIdChild').val('');

        return false;
    });
});

$(function () {
    $('#ddlSearchReleasePaging').change(function () {
        LoadSearchReleaseJtable();
    });
});

function closeChildRow(a) {
    a = this.getChildRow(a);
    a.is(":visible") && a.hide()
}

function LoadSearchReleaseJtable() {
    var searchlist = '';
    pageCount = $('#ddlSearchReleasePaging').val();

    var UpcNumber = $('#txtUpcChild').val().substring();
    var ArtistName = $('#txtArtistNameChild').val().substring();
    var ArtistID = $('#txtArtistIDChild').val().substring();
    var ReleaseTitle = $('#txtReleaseTitleChild').val().substring();
    var R2ProjectId = $('#txtR2ProjectIdChild').val().substring();

    if (UpcNumber != '') { searchlist = searchlist + UpcNumber + '/'; }
    if (ArtistName != '') { searchlist = searchlist + ArtistName + '/'; }
    if (ArtistID != 0) { searchlist = searchlist + ArtistID + '/'; }
    if (ReleaseTitle != '') { searchlist = searchlist + ReleaseTitle + '/'; }
    if (R2ProjectId != '') { searchlist = searchlist + R2ProjectId + '/'; }

    searchlist = searchlist.substring(0, searchlist.length - 1)
    document.getElementById('spnSearchResult').innerHTML = searchlist;
    document.getElementById('spnNoResults').innerHTML = searchlist;

    if (UpcNumber != '' || ArtistName != '' || ArtistID != '' || ReleaseTitle != '' || R2ProjectId != '') {
        $('#searchProjectList').jtable('destroy');
        $('#searchProjectList').jtable({
            pageSize: pageCount,
            selecting: true,
            selectOnRowClick: false,
            multiselect: true,
            selectingCheckboxes: true,
            paging: true, //Enable paging
            sorting: true, //Enable sorting
            defaultSorting: 'UpcNumber ASC',
            actions: {
                listAction: '/GCS/Search/GetReleasesSearch?UPC=' + UpcNumber + '&ArtistName=' + ArtistName + '&ArtistID=' + ArtistID + '&ReleaseTitle=' + ReleaseTitle + '&R2ProjectId=' + R2ProjectId + '&pageSize=' + pageCount
            },
            loadingRecords: function () {
                $('.jtable .jtable-no-data-row').hide();
            },
            recordsLoaded: function (event, data) {
                rowIndex = data.serverResponse.RowIndex;
                ReleaseSearch(rowIndex);
                SectionHideShow(data);
                $('.jtable .jtable-no-data-row').show();
            },
            fields: {
                UpcNumber1: {
                    title: '',
                    width: '5%',
                    sorting: false,
                    edit: false,
                    create: false,
                    listClass: 'child-opener-image-column',
                    display:
                    function (Release) {
                        var $img = $('<img class="child-opener-image" src="/GCS/Images/Acc_Arrow_RIght.png" title="Expand" />');
                        $img.click(function () {

                            id = $(this).closest("td").find("img:first").attr("id");
                            var cla = $(this).closest("td").find("img:first").attr("class");
                            if (cla == "child-opener-image Hide") {
                                $(this).closest("td").find("img:first").attr("src", '/GCS/Images/Down.png');
                                $(this).closest("td").find("img:first").removeClass("Hide");
                                $(this).closest("td").find("img:first").addClass("Show"); cla = cla.replace("Hide", "Show");
                            } else if (cla == "child-opener-image Show") {
                                $(this).closest("td").find("img:first").attr("src", '/GCS/Images/Acc_Arrow_RIght.png'); cla = cla.replace("Show", "Hide");
                                $(this).closest("td").find("img:first").removeClass("Show");
                                $(this).closest("td").find("img:first").addClass("Hide");
                                $('#searchProjectList').find('.jtable-child-row').hide();
                            }

                            if (cla == "child-opener-image Show" || cla == "child-opener-image") {
                                $(this).closest("td").find("img:first").attr("src", '/GCS/Images/Down.png');
                                $(this).closest("td").find("img:first").addClass("Show");

                                $('#searchProjectList').jtable('openChildTable',
                                    $img.closest('tr'),
                                    {
                                        title: '',
                                        actions: {
                                            listAction: '/GCS/Search/GetRegularResources?R2ReleaseId=' + Release.record.R2ReleaseId
                                        },
                                        fields:
                                        {
                                            SequenceNo:
                                                    {
                                                        key: true,
                                                        title: 'Sequence',
                                                        width: '5%'
                                                    },
                                            ResourceTitle:
                                                    {
                                                        key: true,
                                                        title: 'Resource Title',
                                                        width: '20%'
                                                    },
                                            VersionTitle:
                                                    {
                                                        key: true,
                                                        title: 'Version Title',
                                                        width: '20%'
                                                    },
                                            ArtistName:
                                                    {
                                                        key: true,
                                                        title: 'Artist Name',
                                                        width: '30%'
                                                    },
                                            Duration:
                                                    {
                                                        key: true,
                                                        title: 'Duration',
                                                        width: '10%'
                                                    },
                                            Isrc:
                                                    {
                                                        key: true,
                                                        title: 'ISRC',
                                                        width: '10%'
                                                    }
                                        }
                                    },
                                     function (data) {
                                         //opened handler
                                         data.childTable.jtable('load');
                                     });
                            }
                        });

                        return $img;
                    }
                },
                Upc: {
                    key: true,
                    title: 'UPC Number',
                    width: '20%'
                },
                ReleaseTitle: {
                    title: 'Release Title',
                    width: '15%'
                },
                VersionTitle: {
                    title: 'Version Title',
                    width: '15%'
                },
                ArtistName: {
                    title: 'Artist Name',
                    width: '20%'
                },
                ConfigurationDisplay: {
                    title: 'Configuration',
                    width: '10%'
                },
                DataAdminCompany: {
                    title: 'Data Admin By Company',
                    width: '20%'
                }
            },
            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#searchProjectList').jtable('selectedRows');
                $('#SelectedRowList').empty();
                $('#SelectedRowListReource').empty();
                SelectedValues = "";
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        if (record != undefined) {
                            $('#SelectedRowList').append(record.R2ReleaseId + "=" + record.ReleaseTitle + "=" + record.VersionTitle + "=" + record.ArtistName + "=" + record.Configuration + "=" + record.DataAdminCompany + "=" +
                            record.Upc + "=" + record.DataAdminCompanyId + "=" + record.CatalogueNo + "=" + record.ComponentCount + "=" +
                            record.ConfigurationDisplay + "=" + record.EarilerReleaseDate + "=" + record.Grid + "=" + record.LabelId + "=" + record.labelName + "=" +
                            record.MusicType_Desc + "=" + record.MusicType_Id + "=" + record.PackageIndicator + "=" + record.PackageText + "=" +
                            record.PCompanyId + "=" + record.PCompanyName + "=" + record.R2AccountId + "=" + record.ScopeType + "=" +
                            record.Sequence + "=" + record.SoundtrackIndicator + "=" + record.Is_Ost + "=" + record.TrackCount + '~');
                            var recdisplay = document.getElementById('SelectedRowList');
                            var recdisplayresource = document.getElementById('SelectedRowListResource');
                            var shortArtistName = record.ArtistName.toString().split(',');
                            if (SelectedValues == "") {
                                if (shortArtistName[0] == null) {
                                    SelectedValues = record.R2ReleaseId + "=" + $(this)[0].rowIndex + "=" + record.Upc + "/" + record.ReleaseTitle.toString().substring(0, 20) + ".. =" + record.Upc + "/" + record.ReleaseTitle;
                                }
                                else {
                                    SelectedValues = record.R2ReleaseId + "=" + $(this)[0].rowIndex + "=" + record.Upc + "/" + shortArtistName[0].toString().substring(0, 20) + ".. /" + record.ReleaseTitle.toString().substring(0, 20) + ".. =" + record.Upc + "/" + record.ArtistName + "/" + record.ReleaseTitle;
                                }
                            }
                            else {
                                if (shortArtistName[0] == null) {
                                    SelectedValues = SelectedValues + '|' + record.R2ReleaseId + "=" + $(this)[0].rowIndex + "=" + record.Upc + "/" + record.ReleaseTitle.toString().substring(0, 20) + ".. =" + record.Upc + "/" + record.ReleaseTitle;
                                }
                                else {
                                    SelectedValues = SelectedValues + '|' + record.R2ReleaseId + "=" + $(this)[0].rowIndex + "=" + record.Upc + "/" + shortArtistName[0].toString().substring(0, 20) + ".. /" + record.ReleaseTitle.toString().substring(0, 20) + ".. =" + record.Upc + "/" + record.ArtistName + "/" + record.ReleaseTitle;
                                }
                            }
                        }
                    });
                }
            }
        });
        $('#trShowPaging').show();
        $('#searchProjectList').jtable('load', {
            "rowIndex": rowIndex
        });
    } else {
        $('#warningmessage').show();
    }
}

function SectionHideShow(data) {
    if (data.serverResponse.TotalRecordCount > 0) {
        $('#trShowPaging').show();
        $("#trNoResults").hide();
        $("#trAddProject").show();

    }
    else {
        $("#trNoResults").show();
        $('#trShowPaging').hide();
        $("#trAddProject").hide();
    }
}

$('#SearchReleaseExisting').click(function () {
    rowIndex = -1;
    $("#hdnR2RegularRowIndex").val("");
    LoadSearchReleaseJtable();
});

$('#BtnAddReleaseToProject').click(function () {

    $('#loadingDivPA').show();
    var $selectedRows = $('#searchProjectList').jtable('selectedRows');
    if ($('#CallFrom').val() == null) {
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        var values = {
            "SearchSelectedValues": $('#SelectedRowList').text(),
            "SearchSelectedValuesResource": $('#SelectedRowListResource').text()
        };

        // if no record is selected then it should return with message no records is selected
        if ($('#SelectedRowList').text() == "") {
            $('#spnNotSelected').empty();
            $('#spnNotSelected').append("No Record is selected!");
            $('#trNotSelected').show();
            $('#loadingDivPA').hide();
            return;
        }
        var parentHiddenField = window.parent.document.getElementById("releaseListFromSearchPopUp");
        var parentHiddenFielddetail = window.parent.document.getElementById("resourceListFromSearchPopUpDetail");
        var parentHiddenFieldAddToProject = window.parent.document.getElementById("resourceListAddToProject");

        parentHiddenField.value = values.SearchSelectedValues;
        parentHiddenFielddetail.value = values.SearchSelectedValuesResource;
        parentHiddenFieldAddToProject.value = "Child"
        // duplicacy check

        var existingResources = $('#ExistingRelease').text().toString().split(',');
        var selectedResources = SelectedValues.toString().split('|');

        $('#spnIsrc').empty();
        var CompleteString = "";
        var IsExist = false;
        for (var i = 0; i < existingResources.length; i++) {
            for (var j = 0; j < selectedResources.length; j++) {
                var SelRes_Index = selectedResources[j].split('=');

                if (existingResources[i] == SelRes_Index[0]) {
                    if ($('#spnIsrc').text() == "") {
                        $('#spnIsrc').append(SelRes_Index[2]);
                        $('#spnIsrc')[0].title = SelRes_Index[3];
                        CompleteString = SelRes_Index[3];
                        $selectedRows.each(function () {
                            var record = $(this).data('record');
                            if ($(this)[0].rowIndex == SelRes_Index[1]) {
                                $(this).removeClass('jtable-row-selected');
                                $(this).addClass('HighlightedRow');
                            }
                        });
                    }
                    else {
                        $('#spnIsrc').append(',' + SelRes_Index[2]);
                        CompleteString = CompleteString + "||" + SelRes_Index[3];
                        $('#spnIsrc')[0].title = CompleteString;
                        $selectedRows.each(function () {
                            var record = $(this).data('record');
                            if ($(this)[0].rowIndex == SelRes_Index[1]) {
                                $(this).removeClass('jtable-row-selected');
                                $(this).addClass('HighlightedRow');
                            }
                        });
                    }
                    IsExist = true;
                }
            }
        }

        if (IsExist == false) {
            $('#SearchReleaseDialog').remove();
            $('#SearchReleaseDialog').dialog('close');

            var parentHiddenField = window.parent.document.getElementById("releaseListFromSearchPopUp");
            parentHiddenField.value = values.SearchSelectedValues;
            var parentHiddenField = window.parent.document.getElementById("resourceListFromSearchPopUpDetail");
            parentHiddenField.value = values.SearchSelectedValuesResource;

            $('#hdncommand').val("AddtoProject");
            // added by dhruv for Ajax call

            var digitalVal = $('#CreateRegularProjectForm').serialize();
            $.post('/GCS/ClearanceProject/RegularAddButtonForRelease', digitalVal, function (data) {
                $('#loadingDivPA').hide();
                $('#SearchReleasePopup').remove();
                $('#SearchReleasePopup').dialog('close');
                $('#tabs-3').html(data);

                //making new release added show in expanded mode by Raja
                for (var k = 0; k < $('#ReleaseModelCount').val() ; k++) {
                    if ($('#ReleaseId' + k).val() == 0) {
                        var id = $('#h5' + k).find(".details").attr("name");
                        $('#ClearanceRelease' + id).show();
                        $("#tr" + id + "child").show();
                        $("#h5" + k).removeClass('rightArrow');
                        $("#h5" + k).addClass('downArrow');
                    }
                }
            });
            return false;
        }
        else {
            $('#loadingDivPA').hide();
            $('#trShowMessage').show();
        }
    }
    else {
        var values =
                    {
                        "SearchSelectedValues": $('#SelectedRowList').text(),
                        "SearchSelectedValuesResource": $('#SelectedRowListResource').text()
                    };
        // if no record is selected then it should return with message no records is selected
        if ($('#SelectedRowList').text() == "") {
            $('#spnNotSelected').empty();
            $('#spnNotSelected').append("No Record is selected!");
            $('#trNotSelected').show();
            $('#loadingDivPA').hide();
            return;
        }
        if ($('#hdnrowId').text() != '') {
            var removedReleaseAddedAgain = false;
            var formId = window.parent.document.forms(0);

            var parentHiddenField = window.parent.document.getElementById("ExistingReleasesUPC" + $('#hdnrowId').text());
            var Includedpackagestring = window.parent.document.getElementById("PackageIncludedUPC" + $('#hdnrowId').text());

            var parentHiddenFieldRemovedResources = window.parent.document.getElementById("RemovedPackageReleases" + $('#hdnrowId').text());

            var removedResourcesSplit = parentHiddenFieldRemovedResources.value;

            var existingResources;
            if (parentHiddenField.value != null && parentHiddenField.value != '') {
                existingResources = parentHiddenField.value.toString().split('~');
            }
            var selectedResources = SelectedValues.toString().split('|');

            $('#spnIsrc').empty();
            var IsExist = false;
            var CompleteString = "";
            if (existingResources != null) {
                for (var i = 0; i < existingResources.length; i++) {
                    var existingreleases = existingResources[i].toString().split('=')
                    for (var j = 0; j < selectedResources.length; j++) {
                        var SelRes_Index = selectedResources[j].split('=');

                        var NewRemovedPackageReleases = "";
                        if (existingreleases[0] == SelRes_Index[0] && removedResourcesSplit.indexOf(existingreleases[0]) >= 0) {
                            removedReleaseAddedAgain = true;
                            var resourcesRemoved = new Array();
                            resourcesRemoved = removedResourcesSplit.split(',');
                            for (var i = 0; i < resourcesRemoved.length; i++) {
                                if (resourcesRemoved[i].toString().indexOf(existingreleases[0]) < 0) {

                                    if (NewRemovedPackageReleases != "") {
                                        NewRemovedPackageReleases = NewRemovedPackageReleases + ',' + resourcesRemoved[i].toString();
                                    }
                                    else {
                                        NewRemovedPackageReleases = resourcesRemoved[i].toString();
                                    }
                                }
                            }
                            $("#RemovedPackageReleases" + $('#hdnrowId').text()).val(NewRemovedPackageReleases);
                        }

                        if (existingreleases[0] == SelRes_Index[0] && removedResourcesSplit.indexOf(existingreleases[0]) < 0) {
                            if ($('#spnIsrc').text() == "") {
                                $('#spnIsrc').append(SelRes_Index[2]);
                                $('#spnIsrc')[0].title = SelRes_Index[3];
                                CompleteString = SelRes_Index[3];
                                $selectedRows.each(function () {
                                    var record = $(this).data('record');
                                    if ($(this)[0].rowIndex == SelRes_Index[1]) {
                                        $(this).removeClass('jtable-row-selected');
                                        $(this).addClass('HighlightedRow');
                                    }
                                });
                            }
                            else {
                                $('#spnIsrc').append(',' + SelRes_Index[2]);
                                CompleteString = CompleteString + "||" + SelRes_Index[3];
                                $('#spnIsrc')[0].title = CompleteString;
                                $selectedRows.each(function () {
                                    var record = $(this).data('record');
                                    if ($(this)[0].rowIndex == SelRes_Index[1]) {

                                        $(this).removeClass('jtable-row-selected');
                                        $(this).addClass('HighlightedRow');
                                    }
                                });
                            }
                            IsExist = true;
                        }
                    }
                }
            }
            if (IsExist == false) {
                if (parentHiddenField.value != null && parentHiddenField.value != '') {
                    parentHiddenField.value = parentHiddenField.value + values.SearchSelectedValues;
                    var AddedPackagesUPCs;
                    var existingReleases = parentHiddenField.value.toString().split('~');
                    for (var i = 0; i < existingReleases.length; i++) {
                        if (existingReleases[i] != '') {
                            var removedupcs = $("#RemovedPackageReleases" + $('#hdnrowId').text())[0].value.toString().split(',');
                            if (removedupcs != null) {
                                var existingReleasesDetails = existingReleases[i].toString().split('=');
                                if (AddedPackagesUPCs == null) {
                                    if (removedupcs.indexOf(existingReleasesDetails[0] + '-' + existingReleasesDetails[6]) < 0) {
                                        AddedPackagesUPCs = existingReleasesDetails[6];
                                    }
                                }
                                else {
                                    if (removedupcs.indexOf(existingReleasesDetails[0] + '-' + existingReleasesDetails[6]) < 0) {
                                        //added by Raja to check for any duplicate release
                                        if (AddedPackagesUPCs.indexOf(existingReleasesDetails[6]) < 0) {
                                            AddedPackagesUPCs = AddedPackagesUPCs + ', ' + existingReleasesDetails[6];
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Includedpackagestring.innerHTML = AddedPackagesUPCs;
                }
                else {
                    parentHiddenField.value = values.SearchSelectedValues;
                    var existingReleases = parentHiddenField.value.toString().split('~');
                    var AddedPackagesUPCs;
                    for (var i = 0; i < existingReleases.length; i++) {
                        if (existingReleases[i] != '') {
                            var existingReleasesDetails = existingReleases[i].toString().split('=');
                            if (AddedPackagesUPCs == null) {
                                AddedPackagesUPCs = existingReleasesDetails[6];
                            }
                            else {
                                //added by Raja to check for any duplicate release
                                if (AddedPackagesUPCs.indexOf(existingReleasesDetails[6]) < 0) {
                                    AddedPackagesUPCs = AddedPackagesUPCs + ', ' + existingReleasesDetails[6];
                                }
                            }
                        }
                    }
                    Includedpackagestring.innerHTML = AddedPackagesUPCs;
                }
                $('#liPackageIncludedLabel' + $('#hdnrowId').text()).show();
                $('#liPackageIncludedUpc' + $('#hdnrowId').text()).show();
                $('#hdnrowId').empty();
                $('#loadingDivPA').hide();
                $('#SearchReleasePopup').remove();
                $('#SearchReleasePopup').dialog('close');
            }
            else {
                $('#loadingDivPA').hide();
                $('#trShowMessage').show();
            }
        }
    }
});

function ReleaseSearch(rowIndex) {
    $('#searchProjectList').jtable('reset', {
        "UPC": $('#txtUpcChild').val(),
        "ArtistName": $('#txtArtistNameChild').val(),
        "ArtistID": $('#txtArtistIDChild').val(),
        "ReleaseTitle": $('#txtReleaseTitleChild').val(),
        "R2ProjectId": $('#txtR2ProjectIdChild').val(),
        "pageSize": pageCount,
        "rowIndex": rowIndex
    });
}

$(document).ready(function () {
    jQuery("#txtUpcChild").watermark("UPC");
    jQuery("#txtArtistNameChild").watermark("Artist Name");
    jQuery("#txtReleaseTitleChild").watermark("Release Title");
    jQuery("#txtArtistIDChild").watermark("Artist ID");
    jQuery("#txtR2ProjectIdChild").watermark("R2 Project Id");

    if ($('#CallFrom').val() == null) {
        var searchListParentUpc = $('#txtUPC').val();
        $('#txtUpcChild').val(searchListParentUpc);
        var searchListParenArtistNm = $('#txtArtistName').val();
        $('#txtArtistNameChild').val(searchListParenArtistNm);
        var searchListParentReleaseTitle = $('#txtReleaseTitle').val();
        $('#txtReleaseTitleChild').val(searchListParentReleaseTitle);
        var searchListParentArtistID = $('#txtArtistID').val();
        $('#txtArtistIDChild').val(searchListParentArtistID);
        var searchListParentR2ProjectId = $('#txtR2ProjectId').val();
        $('#txtR2ProjectIdChild').val(searchListParentR2ProjectId);

        if (searchListParentUpc != "" || searchListParenArtistNm != "" || searchListParentReleaseTitle != "" || searchListParentArtistID != "" || searchListParentR2ProjectId != "") {
            rowIndex = $("#hdnR2RegularRowIndex").val();
            LoadSearchReleaseJtable();
        }
    }

    $("#SearchReleaseDialog").css("min-height", "90px");
});