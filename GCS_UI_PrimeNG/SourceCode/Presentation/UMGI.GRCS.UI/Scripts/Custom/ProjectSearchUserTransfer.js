// for SEARCH click
$(function () {
    $('#btnSearchProjectUserTransfer').click(function (e) {
        ConditionallyHideSuccessMessageAfterTransferUser();
        var pagingCount = 25;
        $('#ddlSearchProjectPagingUserTransfer').val(25);
        $(".dataGridWrapper").show();
        LoadSearchJProjectTransfertable(pagingCount);
        $("#hdnShowSuccessMessage").val("");
    });
});

$(function () {
    $('#ddlSearchProjectPagingUserTransfer').change(function () {
        HideSuccessMessageAfterTransferUser();
        var rowCount = $(this).val();
        LoadSearchJProjectTransfertable(rowCount);
    });
});

function LoadSearchJProjectTransfertable(pageSize) {
    var searchlist = '';
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
    var selectedProjectTypeIndex = $('#ddlProjectType')[0].selectedIndex;
    var ProjectTypetext = $('#ddlProjectType')[0][selectedProjectTypeIndex].text;
    var ProjectTypeID = $('#ddlProjectType')[0][selectedProjectTypeIndex].value;
    var ProjectTypeValue = -1;
    if (ProjectTypeID.length > 0) ProjectTypeValue = ProjectTypeID;
    //Request Type
    var selectedRequestTypeIndex = $('#ddlRequestType')[0].selectedIndex;
    var RequestTypetext = $('#ddlRequestType')[0][selectedRequestTypeIndex].text;
    var RequestTypeID = $('#ddlRequestType')[0][selectedRequestTypeIndex].value;
    var RequestTypeValue = -1;
    if (RequestTypeID.length > 0) RequestTypeValue = RequestTypeID;

    //Workgroup
    var selectedWorkgroupIndex = $('#ConfigGroupList')[0].selectedIndex;
    var Workgrouptext = $('#ConfigGroupList')[0][selectedWorkgroupIndex].text;
    var WorkgroupID = $('#ConfigGroupList')[0][selectedWorkgroupIndex].value;
    var WorkgroupValue = -1;
    if (WorkgroupID.length > 0) WorkgroupValue = WorkgroupID;

    document.getElementById('hdnselectWorkgroupId').value = WorkgroupID;

    if ($.trim(PRN) != '') { searchlist = searchlist + PRN + '/'; }
    if (selectedProjectTypeIndex != 0) { searchlist = searchlist + ProjectTypetext + '/'; }
    if ($.trim(UPC) != '') { searchlist = searchlist + UPC + '/'; }
    if ($.trim(ProjectTitle) != '') { searchlist = searchlist + ProjectTitle + '/'; }
    if ($.trim(LocalReference) != '') { searchlist = searchlist + LocalReference + '/'; }
    if ($.trim(Requestor) != '') { searchlist = searchlist + Requestor + '/'; }
    if ($.trim(RequestingCompany) != '') { searchlist = searchlist + RequestingCompany + '/'; }
    if (selectedRequestTypeIndex != 0) { searchlist = searchlist + RequestTypetext + '/'; }
    if ((WorkgroupID != 0) && (WorkgroupID != -1)) { searchlist = searchlist + Workgrouptext + '/'; }
    searchlist = searchlist.substring(0, searchlist.length - 1)

    document.getElementById('spnProjectSearchResult').innerHTML = searchlist;
    RequestTypetext = (RequestTypetext == 'Non Traditional') ? 'NonTradition' : RequestTypetext;
    RequestTypetext = (RequestTypetext == 'Others') ? 'Other' : RequestTypetext;
    if (searchlist.length > 0 && ((WorkgroupID != 0) && (WorkgroupID != -1))) {
        $('#ConfigGroupList').removeClass('btn1-validation-error');
        pagingCount = $('#ddlSearchProjectPagingUserTransfer').val();
        $("#trAddProject").show();
        $("#trShowPaging").show();
        $('#searchedProjectList').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: false,
            defaultSorting: 'Name ASC',
            selecting: true,
            selectingCheckboxes: true,
            multiselect: true,
            actions: {
                listAction: '/GCS/ClearanceProject/ProjectSearchForUserTransfer'
            },
            recordsLoaded: function (event, data) {
                var heightGrid = window.innerHeight - $('#searchPanel').height() - $('#showTitle').height() - 134;
                $('#searchedProjectList .jtable-main-container').css({ "height": heightGrid, "max-height": heightGrid });
            },
            fields: {
                ProjectReferenceId: {
                    create: false,
                    edit: true,
                    list: false
                },
                ProjectReferenceId: {
                    list: false
                },
                ProjectReferenceId: {
                    title: jHProjectReferenceNumber,
                    width: '16%'
                },
                ProjectTitle: {
                    title: jHProjectTitle,
                    width: '10%'
                },
                LocalReference: {
                    title: jHLocalReference,
                    width: '10%'
                },
                ProjectTypeDesc: {
                    title: jHProjectType,
                    width: '10%'
                },
                RequestTypeDesc: {
                    title: jHRequeserType,
                    width: '15%'
                },
                StatusTypeDesc: {
                    title: jHStatus,
                    width: '10%'
                },
                RequestCompanyName: {
                    title: jHRequestingCompany,
                    width: '15%'
                },
                ThirdPartyCompanyName: {
                    title: jH3rdPartycompany,
                    width: '12%'
                },
                ClrProjectId:
                {
                    key: true,
                    list: false
                }
                ,
                CreatedUserName: {
                    title: jHRequestor,
                    width: '22%'
                },
                RequesterUserId: {
                    list: false
                },
                RequesterWorkgroupId: {
                    list: false
                }
            },

            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#searchedProjectList').jtable('selectedRows');
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        document.getElementById('SelectedProjectId').value = record.ClrProjectId;
                    });
                }
                else {
                }
            }
        });

        $('#searchedProjectList').jtable('load', {
            ProjectReferenceId: PRN,
            ProjectTypeId: ProjectTypeValue,
            ThirdPartyCompany: UPC,
            ProjectTitle: ProjectTitle,
            RequestTypeID: RequestTypeValue,
            LocalReference: LocalReference,
            Requestor: Requestor,
            RequestingCompany: RequestingCompany,
            WorkgroupID: WorkgroupValue,
            RequestTypeDesc: RequestTypetext
        });
        hidemessageError();
        $("#showTitle").show();
        $("#showPaging").show();
    } else {
        showmessageError(mandatorySearchCriteria);
        $('#ConfigGroupList').addClass('btn1-validation-error');
        $("#showTitle").hide();
        $("#showPaging").hide();
        return false;

    }
}

// for RESET click
$(function () {
    $('#btnSearchProjectReset').click(function () {
        HideSuccessMessageAfterTransferUser();
        $('#searchedProjectList').removeClass('input-validation-error');
        hidemessageError();
        $("#showTitle").hide();
        $("#showPaging").hide();
        $('#ConfigGroupList').removeClass('btn1-validation-error');
        document.getElementById('txtPRN').value = '';
        document.getElementById('ddlProjectType').selectedIndex = 0;
        document.getElementById('txtUPC').value = '';
        document.getElementById('txtProjectTitle').value = '';
        document.getElementById('ddlRequestType').selectedIndex = 0;
        document.getElementById('txtRequestor').value = '';
        document.getElementById('txtLocalReference').value = '';
        document.getElementById('txtRequestingCompany').value = '';

        $('#ConfigGroupList')[0].selectedIndex = 0;
        
        return false;
    });
});

//Set the default button for project search page
$('body').keydown(function (e) {
    if (e.keyCode == 13) {
        $("#btnSearchProjectUserTransfer").trigger('click');
    }
});
function showmessageError(message) {
    $("#divUserTransferErrorMessage").text(message);
    $('#divUserTransferErrorMessage').addClass('warning');
    $("#divUserTransferErrorMessage").show();
}

function hidemessageError() {
    $("#divUserTransferErrorMessage").text('');
    $('#divUserTransferErrorMessage').removeClass('warning');
    $("#divUserTransferErrorMessage").hide();
}

function setControlFocus() {
    //Workgroup
    var selectedWorkgroupIndex = $('#ConfigGroupList')[0].selectedIndex;
    //Requesting Company
    var RequestingCompany = $('#txtRequestingCompany').val();
    //local ref
    var LocalReference = $('#txtLocalReference').val();
    //Requestor
    var Requestor = $('#txtRequestor').val();
    //Request Type
    var selectedRequestTypeIndex = $('#RequestType')[0].selectedIndex;
    //Project Title
    var ProjectTitle = $('#txtProjectTitle').val();
    //3rd party
    var UPC = $('#txtUPC').val();
    //Project Type
    var selectedProjectTypeIndex = $('#ProjectType')[0].selectedIndex;
    //Project Reference Number
    var PRN = $('#txtPRN').val();

    if (selectedWorkgroupIndex > 0) {
        $('#ConfigGroupList')[0].focus();
    }
    if (RequestingCompany != '') {
        $('#txtRequestingCompany').focus();
    }

    if (LocalReference != '') {
        $('#txtLocalReference').focus();
    }

    if (Requestor != '') {
        $('#txtRequestor').focus();
    }

    if (selectedRequestTypeIndex != 0) {
        $('#RequestType')[0].focus();
    }

    if (ProjectTitle != '') {
        $('#txtProjectTitle').focus();
    }

    if (UPC != '') {
        $('#txtUPC').focus();
    }

    if (selectedProjectTypeIndex > 0) {
        $('#ProjectType')[0].focus();
    }

    if (PRN != '') {
        $('#txtPRN').focus();
    }
}

//for Transfer button //Add by vikas
$(function () {
    $('#btnTransferProject').click(function (e) {
        HideSuccessMessageAfterTransferUser();
        LoadAddedJtable();
    });
});

function LoadAddedJtable() {
    var pagingCount = 5;
    var selectedProjectValue = '';
    var $selectedRows = $('#searchedProjectList').jtable('selectedRows');

    $('#SelectedRowList').empty();
    if ($selectedRows.length > 0) {
        $selectedRows.each(function () {
            var record = $(this).data('record');
            selectedProjectValue = selectedProjectValue + record.ClrProjectId + "," + record.ProjectTitle + ',' + record.ProjectReferenceId + ',' + record.CreatedUserName;
            selectedProjectValue = selectedProjectValue + "\r\n";
            document.getElementById('hdnselectWorkgroupId').value = '';
            document.getElementById('hdnselectWorkgroupId').value = record.RequesterWorkgroupId;
        });
    }

    var value =
        {
            "Isrc": selectedProjectValue,
            "PageNumber": document.getElementById('hdnselectWorkgroupId').value
        };

    if (selectedProjectValue.length >= 1) {
        $("#tblProjectsToUserTransfer").empty();
        document.getElementById('hdnprojectid').value = selectedProjectValue;
        $("#divUserTransferErrorMessage").text("");
        $("#divUserTransferErrorMessage").hide();
        $('#searchedProjectList').removeClass('input-validation-error');
        var objDialog = $('#divUserTransfer');
        $('#loadingDivPA').show();
        objDialog.dialog({
            resizable: false,
            height: 330,
            width: 900,
            title: UserTransferPopupTitle,
            modal: true,
            open: function () {
                $(this).load('/GCS/ClearanceProject/ProjectOwnershipTransfer', value);
            }
        });
        objDialog.dialog('open');
        $("#tblProjectsToUserTransfer").show();
        $("#divShowUser").show();
    }

    else {
        $("#divUserTransferErrorMessage").text(mandatoryselectProject);
        $('#divUserTransferErrorMessage').addClass('warning');
        $("#divUserTransferErrorMessage").show();

        $("#divShowUser").hide();
        return false;
    }
}

function HideSuccessMessageAfterTransferUser() {
    $("#divUserTransferSuccessMessage").text("");
    $('#divUserTransferSuccessMessage').addClass('success');
    $("#divUserTransferSuccessMessage").hide();
    $("#hdnShowSuccessMessage").val("");
    return true;
}

function ConditionallyHideSuccessMessageAfterTransferUser() {
    if ($("#hdnShowSuccessMessage").val() != "yes") {
        $("#divUserTransferSuccessMessage").text("");
        $('#divUserTransferSuccessMessage').addClass('success');
        $("#divUserTransferSuccessMessage").hide();
        return
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
function ChangeProjectType(e, contId) {
    var ProjectTypeId = $(contId).attr('id');
    GetRequestType(ProjectTypeId);
    return false;
}

function GetRequestType(ProjectTypeId) {
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

            $('#ddlRequestType').html(items);
        }
    });
}