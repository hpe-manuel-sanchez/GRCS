var pagingCount = 5;
var SelectedValues = "";
var isSearchClicked = false;
var isDirectSearch = true;
var rowIndex = -1;
var isRegular;

var _digitalVal = $('#CreateRegularProjectForm').serialize();
if (_digitalVal != "") {
    isRegular = true;
    $('#excerptTimeMandatory').hide();
}
else {
    isRegular = false;
    $('#excerptTimeMandatory').show();
}

var isCheckboxshow = false;

function showCheckbox() {
    var checktrue = true;
    if (document.getElementById("hdnResourceId") != null) {
        if (parseInt(document.getElementById("hdnResourceId").value) > 0) {
            checktrue = false;
        }
    }
    return checktrue;
}

function searchIntoPopUp() {
    rowIndex = -1;
    $('#trShowMessage').hide();
    $('#spnSearchResult').empty();
    $("#trShowPaging").hide();
    $("#ddlSearchProjectPaging").hide();
    $("#freeHand").hide();
    isSearchClicked = true;
    loadResourceSearchJtable();
}

function loadResourceSearchJtable() {
    var isRadioButtonShow = 'hidden';
    if (isCheckboxshow != true) {
        isRadioButtonShow = 'visible';
    }

    var IncludeMobile = false;
    if ($('#chkMob')[0].checked == true) {
        IncludeMobile = true;
    }
    else {
        IncludeMobile = false;
    }

    if (isSearchClicked == false) {
        var searchListParentUpc = window.document.getElementById("txtUPCResource");
        searchListParentUpc.value = $('#txtUPC').val();

        var searchListParenArtistNm = window.document.getElementById("txtArtistNameResource");
        searchListParenArtistNm.value = $('#txtArtistName').val();

        var searchParentListResourceTitle = window.document.getElementById("txtResourceTitleResource");
        searchParentListResourceTitle.value = $('#txtResuorceTitle').val();

        var searchParentListISRC = window.document.getElementById("txtISRCResource");
        searchParentListISRC.value = $('#txtISRC').val();

        var searchParentListR2ProjectID = window.document.getElementById("txtR2ProjectIDResource");
        searchParentListR2ProjectID.value = $('#txtR2ProjectID').val()

        if (searchListParentUpc.value == "" &&
            searchListParenArtistNm.value == "" &&
            searchParentListResourceTitle.value == "" &&
            searchParentListISRC.value == "" &&
            searchParentListR2ProjectID.value == "" &&
            $('#ddlResourceType option:selected').text() == "Audio" &&
            $('#txtArtistIdResource').val() == "" &&
            IncludeMobile == false &&
            $('#txtVersionTitleResource').val() == "") {
            isDirectSearch = false;
        }
    }
    else {
        isDirectSearch = true;
    }

    if (isDirectSearch) {
        var searchlist = '';

        var ISRC = $('#txtISRCResource').val();
        var ArtistName = $('#txtArtistNameResource').val();
        var UPC = $('#txtUPCResource').val();
        var ArtistId = $('#txtArtistIdResource').val();
        var ResourceTitle = $('#txtResourceTitleResource').val();
        var versionTitle = $('#txtVersionTitleResource').val();
        var ResourceTypeID = $('#ddlResourceType').val();
        var ResourceTypeDesc = $('#ddlResourceType option:selected').text();
        var R2ProjectID = $('#txtR2ProjectIDResource').val();

        if (ArtistName != '') { searchlist = searchlist + ArtistName + '/'; }
        if (ISRC != 0) { searchlist = searchlist + ISRC + '/'; }
        if (UPC != '') { searchlist = searchlist + UPC + '/'; }
        if (ArtistId != '') { searchlist = searchlist + ArtistId + '/'; }
        if (ResourceTitle != '') { searchlist = searchlist + ResourceTitle + '/'; }
        if (versionTitle != '') { searchlist = searchlist + versionTitle + '/'; }
        if (ResourceTypeDesc != 'Select') { searchlist = searchlist + ResourceTypeDesc + '/'; }
        if (IncludeMobile != false) { searchlist = searchlist + 'Include Mobile/'; }
        if (R2ProjectID != '') { searchlist = searchlist + R2ProjectID + '/'; }

        searchlist = searchlist.substring(0, searchlist.length - 1)
        document.getElementById('spnSearchResult').innerHTML = searchlist;
        document.getElementById('spnNoResults').innerHTML = '"' + searchlist + '"';

        if (ISRC != '' || ArtistName != '' || UPC != '' || ArtistId != '' || ResourceTitle != '' || versionTitle != ''
            || ResourceTypeID != '' || ResourceTypeDesc != '' || R2ProjectID != '' || IncludeMobile != false) {

            $("#warningmessage").hide();
            pagingCount = $('#ddlSearchProjectPaging').val();

            $('#searchProjectList').jtable({
                paging: true,
                pageSize: $('#ddlSearchProjectPaging').val(),
                sorting: false,
                selecting: true,
                selectingCheckboxes: isCheckboxshow,
                multiselect: isCheckboxshow,
                defaultSorting: 'OwnedProjectId ASC',
                selectOnRowClick: false,
                actions: {
                    listAction: '/GCS/Search/SearchR2Resource'
                },
                loadingRecords: function (event, data) {
                    $('.jtable .jtable-no-data-row').hide();
                },

                recordsLoaded: function (event, data) {
                    rowIndex = data.serverResponse.RowIndex;
                    ResourceSearch(rowIndex);

                    SectionHideShow(data);
                    $('#searchProjectList').show();
                    $('.jtable .jtable-no-data-row').show();
                    document.getElementById('resultCnt').innerHTML = data.serverResponse.TotalRecordCount;
                    $('#searchProjectList .jtable-main-container').css('max-height', ($('#SearchDialog').height() - 250));
                },
                fields: {
                    radiobutton: {
                        title: 'Select',
                        width: '10%',
                        sorting: false,
                        edit: false,
                        create: false,
                        visibility: isRadioButtonShow,
                        display: function () {
                            var $radiobutton = $('<input type="radio" value="0" name="rbtnSelect" />');
                            return $radiobutton;
                        }
                    },
                    Expand: {
                        title: '',
                        width: '1%',
                        sorting: false,
                        edit: false,
                        create: false,
                        display: function (resource) {

                            var $img = $('<img src="/GCS/Images/Acc_Arrow_RIght.png" title="Expand" class="child-opener-image"  />');

                            $img.click(function (e) {

                                var row = $img.closest('tr');
                                var $childRowColumn = $('#searchProjectList').jtable('getChildRow', row).find('td');

                                id = $(this).closest("td").find("img:first").attr("id");
                                var cla = $(this).closest("td").find("img:first").attr("class");

                                if (cla == "child-opener-image Hide") {

                                    $(this).closest("td").find("img:first").attr("src", '/GCS/Images/Acc_Arrow_Down.png');
                                    $(this).closest("td").find("img:first").removeClass("Hide");
                                    $(this).closest("td").find("img:first").addClass("Show");

                                    $childRowColumn.slideDown(0);

                                    return;
                                }
                                if (cla == "child-opener-image Show") {

                                    $(this).closest("td").find("img:first").attr("src", '/GCS/Images/Acc_Arrow_RIght.png');
                                    $(this).closest("td").find("img:first").removeClass("Show");
                                    $(this).closest("td").find("img:first").addClass("Hide");
                                    $childRowColumn.slideUp(0);

                                    return;
                                }

                                if (cla == "child-opener-image") {
                                    $(this).closest("td").find("img:first").attr("src", '/GCS/Images/Acc_Arrow_Down.png');
                                    $(this).closest("td").find("img:first").addClass("Show");
                                }

                                var values =
                                {
                                    "r2resourceId": resource.record.ResourceId
                                };

                                var divid = 'RightAndRoles' + $childRowColumn[0].uniqueNumber;

                                var $childTableContainer = $("<img src='/GCS/Scripts/jtable/themes/standard/green/loading.gif' class='loadingImage' id='" + divid + "'  /><br/>").appendTo($childRowColumn)
                                $childTableContainer = $("<div class='jtable-child-table-container'></div>").appendTo($childRowColumn);

                                $childRowColumn.data('childPartialView', $childTableContainer);
                                $('#searchProjectList').jtable('openChildRow', row);

                                if ($childRowColumn[0].firstChild.innerHTML == "") {

                                    $childTableContainer.slideDown(0, function () {
                                        $.post('/GCS/Search/GetResourceRights', values, function (data, status) {

                                            data = $.trim(data)

                                            if (data.indexOf("Clearance") == -1)
                                                $childTableContainer.html(NoRightsInGRS)
                                            else
                                                $childTableContainer.html(data);
                                            $('#' + divid).hide();

                                        });
                                    });
                                }
                            })
                            return $img;
                        }
                    },
                    Isrc: {
                        title: 'ISRC',
                        width: '5%'
                    },
                    Title: {
                        title: 'Title',
                        width: '7%'
                    },
                    VersionTitle: {
                        title: 'Version Title',
                        width: '15%'
                    },
                    ArtistName: {
                        title: 'Artist Name',
                        width: '12%'
                    },
                    ResourceId: {
                        title: 'ResourceId',
                        width: '5%',
                        visibility: 'hidden'
                    },
                    Duration: {
                        title: 'Duration',
                        width: '5%'
                    },
                    PYear: {
                        title: 'P-Year',
                        width: '9%'
                    },
                    RightsType: {
                        title: 'RightsType',
                        width: '10%'
                    },
                    OwnedProjectId: {
                        title: 'ProjectId',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    GenreId: {
                        title: 'Genre',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    PCompanyId: {
                        title: 'PCompanyId',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    PLicensingExtension: {
                        title: 'PLicensingExtension',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    SampleCredit: {
                        title: 'SampleCredit',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    R2AccountId: {
                        title: 'AccountId',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    ResourceId: {
                        title: 'ResourceId',
                        width: '10%',
                        visibility: 'hidden',
                        key: true
                    },
                    MusicTypeDesc: {
                        title: 'MusicType',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    RecordingTypeDesc: {
                        title: 'Recording Type',
                        width: '10%',
                        visibility: 'hidden'
                    },
                    ResourceTypeDesc: {
                        title: 'Resource Type',
                        width: '15%'

                    },
                    DataAdminCompanyName: {
                        title: 'Data Admin by Company',
                        width: '45%'
                    }
                },

                selectionChanged: function () {
                    var $selectedRows = $('#searchProjectList').jtable('selectedRows');

                    $('#SelectedRowList').empty();
                    SelectedValues = "";
                    if ($selectedRows.length > 0) {
                        var ResourceIndexToUpdate = "0";
                        if (document.getElementById("hdnResourceId") != null) {
                            if (parseInt(document.getElementById("hdnResourceId").value) > 0) {
                                ResourceIndexToUpdate = document.getElementById("hdnResourceId").value;
                            }
                        }
                        $selectedRows.each(function () {

                            var record = $(this).data('record');
                            var chselect = $(this)[0].cells[0].children[0].checked;
                            if (chselect == true) {
                                $(this).addClass('jtable-row-selected');

                                $('#SelectedRowList').append(
                                    record.Isrc +
                                    '|' + record.Title.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.VersionTitle.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.ArtistName.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.ResourceId +
                                    '|' + record.Duration.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.PYear +
                                    '|' + record.RightsTypeCode.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.OwnedProjectId +
                                    '|' + record.GenreId.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.PCompanyId +
                                    '|' + record.PLicensingExtension.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.SampleCredit.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.R2AccountId +
                                    '|' + record.R2_ResourceId +
                                    '|' + record.MusicTypeDesc.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.RecordingTypeDesc.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.ResourceTypeDesc.replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') +
                                    '|' + record.AdminCompanyId +
                                    '|' + record.IsMobileResource +
                                    '|' + record.HasSample +
                                    '|' + record.HasSideArtist +
                                    '|' + ResourceIndexToUpdate + '~'
                                    );

                                var shortArtistName = record.ArtistName.toString().split(',');
                                if (SelectedValues == "") {
                                    if (shortArtistName[0] == null) {
                                        SelectedValues = record.ResourceId + "=" + $(this)[0].rowIndex + "=" + record.Isrc + "/" + record.Title.toString().substring(0, 20) + ".. =" + record.Isrc + "/" + record.Title;
                                    }
                                    else {
                                        SelectedValues = record.ResourceId + "=" + $(this)[0].rowIndex + "=" + record.Isrc + "/" + shortArtistName[0].toString().substring(0, 20) + ".. /" + record.Title.toString().substring(0, 20) + ".. =" + record.Isrc + "/" + record.ArtistName + "/" + record.Title;
                                    }
                                }
                                else {
                                    if (shortArtistName[0] == null) {
                                        SelectedValues = SelectedValues + '|' + record.ResourceId + "=" + $(this)[0].rowIndex + "=" + record.Isrc + "/" + record.Title.toString().substring(0, 20) + ".. =" + record.Isrc + "/" + record.Title;
                                    }
                                    else {
                                        SelectedValues = SelectedValues + '|' + record.ResourceId + "=" + $(this)[0].rowIndex + "=" + record.Isrc + "/" + shortArtistName[0].toString().substring(0, 20) + ".. /" + record.Title.toString().substring(0, 20) + ".. =" + record.Isrc + "/" + record.ArtistName + "/" + record.Title;
                                    }
                                }
                            }
                            else {
                                $(this).addClass('jtable-main-container');

                            }
                        });
                    }
                }
            });

            function SectionHideShow(data) {
                if (data.serverResponse.TotalRecordCount > 0) {

                    $("#hdnRowIndex").val(data.serverResponse.RowIndex);
                    $("#trShowPaging").show();
                    $("#trNoResults").hide();
                    $("#trAddProject").show();
                    $("#ddlSearchProjectPaging").show();
                    var tableLength = $('.jtable').length;
                    for (i = 0; i < $('.jtable')[tableLength - 1].rows.length; i++) {
                        if ($('.jtable')[tableLength - 1].rows[i] != null) {
                            if (navigator.appName == "Microsoft Internet Explorer") {
                                if ($('.jtable')[tableLength - 1].rows[i].children[10].innerText == "NONUMG" || $('.jtable')[tableLength - 1].rows[i].children[10].innerText == "NONEXC" || $('.jtable')[tableLength - 1].rows[i].children[10].innerText == "Non-Exclusive License" || $('.jtable')[tableLength - 1].rows[i].children[10].innerText == "Non-UMG") {
                                    $('.jtable')[tableLength - 1].rows[i].className = 'HighlightedRow';
                                }
                            }
                            else {
                                if ($('.jtable')[tableLength - 1].rows[i].children[10].textContent == "NONUMG" || $('.jtable')[tableLength - 1].rows[i].children[10].textContent == "NONEXC" || $('.jtable')[tableLength - 1].rows[i].children[10].textContent == "Non-Exclusive License" || $('.jtable')[tableLength - 1].rows[i].children[10].textContent == "Non-UMG") {
                                    $('.jtable')[tableLength - 1].rows[i].className = 'HighlightedRow';
                                }
                            }
                        }
                    }
                }
                else {

                    $("#addFreeHand").show();
                    var ResourceIndexToUpdate = "0";
                    if (document.getElementById("hdnResourceId") != null) {
                        if (parseInt(document.getElementById("hdnResourceId").value) > 0) {
                            ResourceIndexToUpdate = document.getElementById("hdnResourceId").value;
                        }
                    }
                    $("#trShowPaging").hide();
                    $("#trNoResults").show();
                    $("#trAddProject").hide();
                    if (parseInt(ResourceIndexToUpdate) > 0) {
                        $("#addFreeHand").hide();
                    }
                }
            }

            $('#searchProjectList').jtable('load', {
                "Isrc": ISRC,
                "ArtistName": ArtistName,
                "ReleaseUpc": UPC,
                "ArtistId": ArtistId,
                "ResourceTitle": ResourceTitle,
                "VersionTitle": versionTitle,
                "ResourceTypeId": ResourceTypeID,
                "ResourceType": ResourceTypeDesc,
                "IsIncludeMobileLock": IncludeMobile,
                "R2ProjectID": R2ProjectID,
                "Criteria.RowIndex": rowIndex,
                "Criteria.UserName": 'vivek_gupta',
                "Criteria.PageSize": pagingCount,
                "Criteria.StartIndex": '0',
                "Criteria.QualificationCriteria": true
            });
        }
        else {
                $("#warningmessage").text(atleastsearchcriteriaresource);
                $('#warningmessage').addClass('warning');
                $("#warningmessage").show();                                    
            return false;
        }
    }

    function ResourceSearch(rowIndex) {
        $('#searchProjectList').jtable('reset', {
            "Isrc": ISRC,
            "ArtistName": ArtistName,
            "ReleaseUpc": UPC,
            "ArtistId": ArtistId,
            "ResourceTitle": $('#txtResourceTitleResource').val(),
            "releaseTitle": $('#txtReleaseTitleResource').val(),
            "VersionTitle": $('#txtVersionTitleResource').val(),
            "ResourceTypeId": $('#ddlResourceType').val(),
            "ResourceType": $('#ddlResourceType option:selected').text(),
            "IsIncludeMobileLock": IncludeMobile,
            "Criteria.RowIndex": rowIndex,
            "Criteria.UserName": 'vivek_gupta',
            "Criteria.PageSize": pagingCount,
            "Criteria.StartIndex": '0',
            "Criteria.QualificationCriteria": true
        });
    }
}

function fnSelectAll1(chkBox) {
    if (chkBox.checked == true) {
        var $Rows = $("#searchProjectList").jtable("Rows");
        var count = $("#ddlSearchProjectPaging").val();
        if ($Rows.length > 0) {
            $Rows.each(function () {
                var record = $(this).data("record");
                alert(record.Title);
            });
        }
    }
}

function validateResourceMandatoryFields(FieldName) {
    if ($('#' + FieldName).is(':visible') && ($('#' + FieldName).attr("value") == null ||
        $('#' + FieldName).attr("value") == "0" || $('#' + FieldName).val() == "" || $('#' + FieldName).attr("value") == "")) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

function resourceEventResetSearchDialogClick() {
    $('#trShowMessage').hide();
    $("#warningmessage").hide();
    $('#SelectedRowList').empty();
    $('#spnSearchResult').empty();
    $("#trNoResults").hide();
    $("#trShowPaging").hide();
    $("#ddlSearchProjectPaging").hide();

    document.getElementById('txtArtistNameResource').value = '';
    document.getElementById('ddlResourceType').selectedIndex = 0;
    document.getElementById('txtISRCResource').value = '';
    document.getElementById('txtUPCResource').value = '';
    document.getElementById('txtVersionTitleResource').value = '';
    document.getElementById('txtArtistIdResource').value = '';
    document.getElementById('txtResourceTitleResource').value = '';
    document.getElementById('txtReleaseTitleResource').value = '';
    document.getElementById('txtR2ProjectIDResource').value = '';
    document.getElementById('chkMob').checked = false;

    $("#txtArtistNameResource").watermark("Artist Name");
    $("#txtISRCResource").watermark("ISRC");
    $("#txtUPCResource").watermark("UPC");
    $("#txtVersionTitleResource").watermark("Version Title");
    $("#txtArtistIdResource").watermark("Artist Id");
    $("#txtResourceTitleResource").watermark("Resource Title");
    $("#txtReleaseTitleResource").watermark("Release Title");
    $("#txtR2ProjectIDResource").watermark("R2 Project ID");
}

function resourceEventSearchResourceClick() {

    rowIndex = -1;
    $('#trShowMessage').hide();
    $('#spnSearchResult').empty();
    $("#trShowPaging").hide();
    $("#ddlSearchProjectPaging").hide();
    $("#freeHand").hide();
    isSearchClicked = true;
    loadResourceSearchJtable();
}

function resourceEventAddToProjectClick(event) {
    if (isRegular == true) {
        if (!ValidateDuration('#freeHandResource_Duration') || !MoneyValidatorOnSubmit('#freeHandResource_SuggestedFee')) {
            return false;
        }
    }
    else {
        if (!ValidateDuration('#freeHandResource_Duration') || !ValidateDuration('#freeHandResource_ExcerptTime') ||
            !MoneyValidatorOnSubmit('#freeHandResource_SuggestedFee')) {
            return false;
        }
    }

    $('#loadingDivPA').show();
    $("#warningmessage").hide();
    if ($("#freeHand").is(":visible")) {
        var IsValid = true;
        if ($('#divArtistName').text() == "") {
            $('#btnManageArtist').addClass('input-validation-error');
            IsValid = false;
        }
        if (!validateResourceMandatoryFields('txtFreeHandResourceTitle')) { IsValid = false; }

        if (isRegular == false) {
            if (!validateResourceMandatoryFields('freeHandResource_ExcerptTime')) { IsValid = false; }
        }

        if (IsValid == false) {
            if ($("#freeHand").is(":visible")) {

                if (isRegular == false) {
                    if (($('#divArtistName').text() == "") && ($('#freeHandResource_ExcerptTime').val() != "") && ($('#txtFreeHandResourceTitle').val() != "")) {
                        $("#warningmessage").text(mandatorylinkArtist);
                    }
                    else {
                        $("#warningmessage").text(mandatoryFields);
                    }
                }
                else {
                    if (($('#divArtistName').text() == "") && ($('#txtFreeHandResourceTitle').val() != "")) {
                        $("#warningmessage").text(mandatorylinkArtist);
                    }
                    else {
                        $("#warningmessage").text(mandatoryFields);
                    }
                }
            }
            else {
                $("#warningmessage").text(mandatoryFields);
            }
            $("#warningmessage").addClass('warning');
            $("#warningmessage").show();

            $('#loadingDivPA').hide();
            return false;
        }
        var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZ";
        var string_length = 6;
        var RandomIsrc = [], pos;

        while (string_length--) {
            pos = Math.floor(Math.random() * chars.length);
            RandomIsrc.push(chars.substr(pos, 1));
        }
        RandomIsrc.join('');
        var ResourceIndexToUpdate = "0";
        if (document.getElementById("hdnResourceId") != null) {
            if (parseInt(document.getElementById("hdnResourceId").value) > 0) {
                ResourceIndexToUpdate = document.getElementById("hdnResourceId").value;
            }
        }
        var values =
            {
                "SearchSelectedValues": RandomIsrc.join('') + '|' +
                    $('#txtFreeHandResourceTitle').val().replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') + '|' +
                    $('#freeHandResource_VersionTitle').val().replace(/\|/g, '(pipe)').replace(/\~/g, '(tilde)') + '|' +
                    $('#divArtistName').attr('title') + '|0|' +
                    $('#freeHandResource_Duration').val() + '|0|RightsType|0|0|0|PcLicensingExtn|SampleCredit|0|0|' +
                    $('#MusicType option:selected').text() + '|' +
                    $('#ddlRecordingTypeFreeHand option:selected').text() + '|' +
                    $('#ResourceTypeFreehand option:selected').text() + '|0|' + 'False' + '|' + 'False' + '|' + 'False' + '|' +
                    $('#freeHandResource_SuggestedFee').val() + '|' + $('#freeHandResource_ExcerptTime').val() + '|' +
                    $('#freehandComments').val() + '|' + $('#hdnArtistId').text() + '|' + ResourceIndexToUpdate + '|' +
                    $('#hdnterritoryDetailsForSave_200').val() + '|' +
                    $('#hdnincludeTerritoryString_200').val() + '|' +
                    $('#hdnExcludeTerritoryString_200').val() + '~'
            };

        $('#SearchDialog').dialog('close');
        $('#SearchDialog').remove();

        var formId = window.parent.document.forms[0];

        window.parent.document.getElementById("resourceListFromSearchPopUp").value = "";
        var parentHiddenField = window.parent.document.getElementById("resourceListFromSearchPopUp");
        parentHiddenField.value = values.SearchSelectedValues;

        var digitalVal = $('#CreateRegularProjectForm').serialize();

        if (digitalVal != "") {
            event.preventDefault();
            event.stopPropagation();

            $.post('/GCS/ClearanceProject/RegularAddButtonForResource', digitalVal, function (data) {

                $('#tabs-4').html(data);
                $('#hdnTempterritoryDetailsForSave').val('');
                ParentCall();
                $('#loadingDivPA').hide();
            });
            return false;
        }
        else {
            var digitalVal = $('#CreateMasterProject').serialize();
            event.preventDefault();
            event.stopPropagation();

            $.post('/GCS/ClearanceProject/MasterAddButtonForResource', digitalVal, function (data) {

                $('#tabs-2').html(data);
                $('#hdnTempterritoryDetailsForSave').val('');
                ParentCall();
                $('#loadingDivPA').hide();

            });
            return false;


        }
        $('#loadingDivPA').hide();
    }
    else {
        var values =
            {
                "SearchSelectedValues": $('#SelectedRowList').text()
            };

        var existingResources = $('#ExistingResources').text().toString().split(',');
        var selectedResources = SelectedValues.toString().split('|');
        $('#spnIsrc').empty();
        var $selectedRows = $('#searchProjectList').jtable('selectedRows');
        var IsExist = false;
        var CompleteString = "";
        for (var i = 0; i < existingResources.length; i++) {
            for (var j = 0; j < selectedResources.length; j++) {
                var SelRes_Index = selectedResources[j].split('=');
                if (($.trim(existingResources[i]) != '') && (SelRes_Index[0] != '')) {
                    if (($.trim(existingResources[i]) == SelRes_Index[0])) {
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
            if (values.SearchSelectedValues != '') {
                $('#SearchDialog').dialog('close');
                $('#SearchDialog').remove();

                var formId = window.parent.document.forms[0];
                var parentHiddenField = window.parent.document.getElementById("resourceListFromSearchPopUp");
                parentHiddenField.value = values.SearchSelectedValues;

                var digitalVal = $('#CreateRegularProjectForm').serialize();

                if (digitalVal != "") {
                    event.preventDefault();
                    event.stopPropagation();


                    $.post('/GCS/ClearanceProject/RegularAddButtonForResource', digitalVal, function (data) {

                        $('#tabs-4').html(data);
                        $('#hdnTempterritoryDetailsForSave').val('');
                        ParentCall();
                        $('#loadingDivPA').hide();
                    });
                    return false;
                }
                else {
                    var digitalVal = $('#CreateMasterProject').serialize();
                    event.preventDefault();
                    event.stopPropagation();

                    $.post('/GCS/ClearanceProject/MasterAddButtonForResource', digitalVal, function (data) {

                        $('#tabs-2').html(data);
                        $('#hdnTempterritoryDetailsForSave').val('');
                        ParentCall();
                        $('#loadingDivPA').hide();

                    });
                    return false;


                }
                $('#loadingDivPA').hide();
            }
            else {
                $('#loadingDivPA').hide();
                $("#warningmessage").text('Please link atleast one Resource');
                $('#warningmessage').addClass('warning');
                $("#warningmessage").show();
                return false;
            }
        }
        else {
            $('#loadingDivPA').hide();
            $('#trShowMessage').show();
        }
    }
}

function resourceEventAddFreeHandClick(event) {
    id = $("#freeHand").show();
    $('#searchProjectList').hide();
    $("#trAddProject").show();
}

function resourceManageEventArtistClick(event) {
    var objArtistPopup = $('<div id="ArtistResourcePopup" style="margin:0;padding:0;"></div>');
    if ($('#hdnArtistId').text() != "") {
        $(objArtistPopup).dialog({
            autoOpen: true,
            width: 1000,

            resizable: false,
            title: 'Manage Artist',
            modal: true,
            modal: true,
            close: function () {
                $(this).remove();
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            }
        });
        var values =
        {
            "existingArtist": $('#hdnArtistId').text()
        }
        $(objArtistPopup).load('/GCS/Artist/SearchForArtist', values);
        $('#tblExistingArtist').show();
        var dialogue = $('.ui-dialog');
        dialogue.animate({
            top: "30px"
        }, 0);

    }
    else {
        var objDialog = $(objArtistPopup);

        objDialog.dialog({
            resizable: false,
            autoOpen: true,
            width: 1000,
            title: 'Manage Artist',

            modal: true,
            close: function () {
                $(this).remove();
                $(this).dialog('close');
                $('#loadingDivPA').hide();
            },
            open: function () {
                $(this).load('/GCS/Artist/SearchForArtist');
            }
        });
        var dialogue = $('.ui-dialog');

        dialogue.animate({
            top: "40px"
        }, 0);
    }
}

$(document).ready(function () {
    $('#SearchDialog #btnSearchResource').focus();

    isCheckboxshow = showCheckbox();

    loadResourceSearchJtable();

    $("#btnManageArtist").click(function (event) {
        resourceManageEventArtistClick(event);
    });

    $("#addFreeHand").click(function (event) {
        resourceEventAddFreeHandClick(event);
    });

    $("#btnaddToProject").click(function (event) {
        resourceEventAddToProjectClick(event);
    });

    $('#btnSearchResource').click(function () {
        resourceEventSearchResourceClick();
    });

    $('#btnSearchResourceReset').click(function () {
        resourceEventResetSearchDialogClick();
    });

    $('#ddlSearchProjectPaging').change(function () {
        loadResourceSearchJtable();
    });

    $('#SearchDialog')
        .unbind('keydown')
        .keydown(function (e) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                resourceEventSearchResourceClick();
            }
        });
});
