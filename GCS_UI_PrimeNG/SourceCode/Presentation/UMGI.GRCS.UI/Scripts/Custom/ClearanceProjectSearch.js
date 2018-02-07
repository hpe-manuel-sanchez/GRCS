var UpdatedMessages = {
    serverCommunicationError: 'An error occured while communicating to the server.',
    noDataAvailable: 'No project matching the search criteria',
    areYouSure: 'Are you sure?',
    save: 'Save',
    saving: 'Saving',
    cancel: 'Cancel',
    error: 'Error',
    close: 'Close',
    cannotLoadOptionsFor: 'Can not load options for field {0}'
};

function autocomplete(id) {
    $(id)[0].title = "Zoom";

    $(id).autocomplete({        
        source: function (request, response) {
            var url = $(id).attr("data-autocomplete-source-manual");
            $(id).addClass("ui-autocomplete-loading");
            $(id).removeClass("ui-autocomplete-input");

            terms = {
                term: request.term,
                searchBy: $(id).attr("SearchFor")
            };

            $.getJSON(url, terms, function (data) {
                if (!tableIsLoading()) {
                    response(data);
                }
                $(id).removeClass("ui-autocomplete-loading");
                $(id).addClass("ui-autocomplete-input");
            });
        },
        minLength: 3,
        select: function (event, ui) {
            $(id).val(ui.item.value);
        }
    });
}

function LoadSearchJtable(pageSize) {
    var searchlist = '';

    //Project Reference Number
    var PRN = RemoveSpace($('#txtPRN').val());
    //3rd party
    var UPC = RemoveSpace($('#txtUPC').val());
    //Project Title
    var ProjectTitle = RemoveSpace($('#txtProjectTitle').val());
    //local ref
    var LocalReference = RemoveSpace($('#txtLocalReference').val());
    //Requestor
    var Requestor = RemoveSpace($('#txtRequestor').val());
    //Requesting Company
    var RequestingCompany = RemoveSpace($('#txtRequestingCompany').val());
    //Project Type
    var selectedProjectTypeIndex = $('#ddlProjectTypeCopy')[0].selectedIndex;
    var ProjectTypetext = $('#ddlProjectTypeCopy')[0][selectedProjectTypeIndex].text;
    var ProjectTypeID = $('#ddlProjectTypeCopy')[0][selectedProjectTypeIndex].value;
    var ProjectTypeValue = -1;
    if (ProjectTypeID.length > 0) ProjectTypeValue = ProjectTypeID;
    //Request Type
    var selectedRequestTypeIndex = $('#ddlRequestTypeCopy')[0].selectedIndex;
    var RequestTypetext = $('#ddlRequestTypeCopy')[0][selectedRequestTypeIndex].text;
    var RequestTypeID = $('#ddlRequestTypeCopy')[0][selectedRequestTypeIndex].value;
    var RequestTypeValue = -1;
    if (RequestTypeID.length > 0) RequestTypeValue = RequestTypeID;

    if ($.trim(PRN) != '') { searchlist = searchlist + PRN + '/'; }
    if (selectedProjectTypeIndex != 0) { searchlist = searchlist + ProjectTypetext + '/'; }
    if ($.trim(UPC) != '') { searchlist = searchlist + UPC + '/'; }
    if ($.trim(ProjectTitle) != '') { searchlist = searchlist + ProjectTitle + '/'; }
    if ($.trim(LocalReference) != '') { searchlist = searchlist + LocalReference + '/'; }
    if ($.trim(Requestor) != '') { searchlist = searchlist + Requestor + '/'; }
    if ($.trim(RequestingCompany) != '') { searchlist = searchlist + RequestingCompany + '/'; }
    if (selectedRequestTypeIndex != 0) { searchlist = searchlist + RequestTypetext + '/'; }


    searchlist = searchlist.substring(0, searchlist.length - 1)
    document.getElementById('spnSearchResult').innerHTML = searchlist;
    RequestTypetext = (RequestTypetext == 'Non Traditional') ? 'NonTradition' : RequestTypetext;
    RequestTypetext = (RequestTypetext == 'Others') ? 'Other' : RequestTypetext;
    if (searchlist.length > 0) {
        $("#divErrorMessage").text("");
        $("#divErrorMessage").hide();

        pagingCount = $('#ddlSearchProjectPaging').val();
        $("#trAddProject").show();
        $("#trShowPaging").show();
        $("#trPagingRow").show();
        $('#searchProjectList').jtable({
            messages: UpdatedMessages, //Set messages as Turkish
            paging: true,
            pageSize: pagingCount,
            sorting: false,
            defaultSorting: 'Name ASC',
            selecting: true,
            selectingCheckboxes: true,
            multiSelect: true,
            actions: {
                listAction: '/GCS/ClearanceProject/SearchProject'
            },
            recordsLoaded: function (event, data) {
                var heightGrid = window.innerHeight - $('#searchPanel').height() - $('#trPagingRow').height() - 200;
                $('#searchProjectList .jtable-main-container').css('max-height', (heightGrid));
            },
            loadingRecords: function () {
                $('.jtable .jtable-no-data-row').hide();
                $(".ui-jtable-loading").show();
            },
            fields: {
                ProjectReferenceId: {
                    key: true,
                    create: false,
                    edit: true,
                    list: false
                },
                ProjectReferenceId: {
                    list: false
                },
                ProjectReferenceId: {
                    title: ClrProjSearchProjectReferenceNumberLabel,
                    width: '10%'
                },
                ProjectTitle: {
                    title: ClrProjSearchProjectTitleLabel,
                    width: '12%'
                },
                LocalReference: {
                    title: ClrProjSearchLocalReferenceLabel,
                    width: '13%'
                },
                ProjectTypeDesc: {
                    title: ClrProjSearchProjectTypeLabel,
                    width: '10%'
                },
                RequestTypeDesc: {
                    title: ClrProjSearchRequestTypeLabel,
                    width: '12%'
                }
                        ,
                RequestCompanyName: {
                    title: ClrProjSearchRequestingCompanyLabel,
                    width: '15%'
                },
                ThirdPartyCompanyName: {
                    title: ClrProjSearchThirdPartyCompanyLabel,
                    width: '17%'
                },
                ClrProjectId:
                {
                    list: false
                }
                ,
                CreatedUserName: {
                    list: false
                },
                RequesterUserId: {
                    list: false
                }

            },

            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#searchProjectList').jtable('selectedRows');
                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#SelectedRowList').append(record.ClrProjectId + ':' + record.ProjectReferenceId + ':' + record.ProjectTitle + ':' + record.LocalReference);
                        var recdisplay = document.getElementById('SelectedRowList');
                        document.getElementById('SelectedProjectId').value = record.ClrProjectId;
                        if (record.ProjectTypeDesc == 'Regular/Non-Traditional') {
                            document.getElementById('SelectedProjectType').value = 'Regular';
                        }
                        else {
                            document.getElementById('SelectedProjectType').value = record.ProjectTypeDesc;
                        }
                    });
                }
            }
        });

        $('#searchProjectList').jtable('load', {
            ProjectReferenceId: PRN,
            ProjectTypeId: ProjectTypeValue,
            ThirdPartyCompany: UPC,
            ProjectTitle: ProjectTitle,
            RequestTypeID: RequestTypeValue,
            LocalReference: LocalReference,
            Requestor: Requestor,
            RequestingCompany: RequestingCompany,
            RequestTypeDesc: RequestTypetext

        });
    } else {
        $("#divErrorMessage").text(mandatorySearchCriteria);
        $('#divErrorMessage').addClass('warning');
        $("#divErrorMessage").show();
        return false;
    }
}

function RemoveSpace(txtval) {
    if (($.trim(txtval)) != '') {
        return $.trim(txtval);
    }
    else {
        return "";
    }
}

function ChangeProjectTypeCopy(ProjectTypeId) {
    var values =
                {

                    "PageSize": $('#' + ProjectTypeId + ' :selected').val()
                };

    $.ajax({
        url: '/GCS/ClearanceProject/GetRequestType',
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values),
        success: function (data) {

            var items = "<option selected>Select</option>";

            for (var i = 0; i < data.Records.length; i++) {
                items += "<option value='" + data.Records[i].Value + "'>" + data.Records[i].Text + "</option>";
            }

            $('#ddlRequestTypeCopy').html(items);

        }


    });
}

function tableIsLoading() {
    var isLoading = $('#searchProjectList .jtable-busy-message').is(':visible');
    return isLoading;
}

function closeAllTheAutocomplete() {
    $("#txtProjectTitle").autocomplete("close");
    $("#txtUPC").autocomplete("close");
    $("#txtRequestingCompany").autocomplete("close");
}

$(document).ready(function () {
    $("#SearchDialog").css("min-height", "100px");

    autocomplete("#txtProjectTitle");
    autocomplete("#txtUPC");
    autocomplete("#txtRequestingCompany");

    $('body').keydown(function (e) {
        if (e.keyCode == 13) {
            $("#btnSearchProject").trigger('click');
        }
    });

    $('#btnSearchProject').click(function (e) {
        closeAllTheAutocomplete();

        var pagingCount = 25;
        $('#ddlSearchProjectPaging').val(25);
        LoadSearchJtable(pagingCount);
    });

    $('#ddlSearchProjectPaging').change(function () {
        closeAllTheAutocomplete();

        var rowCount = $(this).val();
        LoadSearchJtable(rowCount);
    });

    $('#btnSearchProjectReset').click(function () {
        closeAllTheAutocomplete();

        $("#diverrormessage").text("");
        $("#diverrormessage").hide();
        $("#trPagingRow").hide();

        document.getElementById('txtPRN').value = '';
        document.getElementById('ddlRequestTypeCopy').selectedIndex = 0;
        document.getElementById('txtUPC').value = '';
        document.getElementById('txtProjectTitle').value = '';
        document.getElementById('ddlProjectTypeCopy').selectedIndex = 0;
        document.getElementById('txtRequestor').value = '';
        document.getElementById('txtLocalReference').value = '';
        document.getElementById('txtRequestingCompany').value = '';

        document.getElementById('txtRequestingCompany').value = '';
        document.getElementById('txtRequestingCompany').value = '';
        document.getElementById('txtRequestingCompany').value = '';
        document.getElementById('txtRequestingCompany').value = '';
        document.getElementById('txtRequestingCompany').value = '';
        document.getElementById('txtRequestingCompany').value = '';

        return false;
    });

    $('#btnAddProject').click(function (e) {
        $("#divErrorMessage").text("");
        $("#divErrorMessage").hide();
        if (document.getElementById('SelectedProjectId').value != "") {
            $(this).closest('form')[0].submit();
        }
        else {
            $("#divErrorMessage").text(mandatoryselectProject);
            $('#divErrorMessage').addClass('warning');
            $("#divErrorMessage").show();
            return false;
        }
    });
});