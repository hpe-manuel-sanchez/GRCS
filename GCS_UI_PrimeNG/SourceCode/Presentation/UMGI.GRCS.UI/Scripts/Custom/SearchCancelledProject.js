// ON PAGE LOAD
$(document).ready(function () {
    jQuery("#txtPRN").watermark("Project Ref No.");
    jQuery("#txtLocalReference").watermark("Local Ref.");
    jQuery("#txtRequestingCompany").watermark("Requesting Comp.");
    jQuery("#txtUPC").watermark("Third Party Comp.");
    jQuery("#txtRequestor").watermark("Requestor");
    jQuery("#txtProjectTitle").watermark("Project Title");
});

// for SEARCH click
$(function () {
    $('#btnSearchCancelledProject').click(function (e) {
        var pagingCount = 25;
        $('#ddlSearchProjectPaging').val(25);
        LoadSearchJtable(pagingCount);
    });
});

//change the dropdown selection
$(function () {
    $('#ddlSearchProjectPaging').change(function () {
        var rowCount = $(this).val();
        LoadSearchJtable(rowCount);
    });
});

function LoadSearchJtable(pageSize) {
    var searchlist = '';
    var pagingCount = 5;
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
    var selectedProjectTypeIndex = $('#ddlProjectTypeMaintain')[0].selectedIndex;
    var ProjectTypetext = $('#ddlProjectTypeMaintain')[0][selectedProjectTypeIndex].text;
    var ProjectTypeID = $('#ddlProjectTypeMaintain')[0][selectedProjectTypeIndex].value;
    var ProjectTypeValue = -1;
    if (ProjectTypeID.length > 0) ProjectTypeValue = ProjectTypeID;
    //Request Type
    var selectedRequestTypeIndex = $('#ddlRequestTypeMaintain')[0].selectedIndex;
    var RequestTypetext = $('#ddlRequestTypeMaintain')[0][selectedRequestTypeIndex].text;
    var RequestTypeID = $('#ddlRequestTypeMaintain')[0][selectedRequestTypeIndex].value;
    var RequestTypeValue = -1;
    if (RequestTypeID.length > 0) RequestTypeValue = RequestTypeID;

    if ($.trim(PRN) != '') { searchlist = searchlist + PRN + '/'; }
    if (selectedProjectTypeIndex != 0) { searchlist = searchlist + ProjectTypetext + '/'; }
    if ($.trim(UPC) != '') { searchlist = searchlist + UPC + '/'; }
    if ($.trim(ProjectTitle) != '') { searchlist = searchlist + ProjectTitle + '/'; }
    if ($.trim(LocalReference) != '') { searchlist = searchlist + LocalReference + '/'; }
    if ($.trim(Requestor) != '') { searchlist = searchlist + Requestor + '/'; }
    if ($.trim(RequestingCompany) != '') { searchlist = searchlist + RequestingCompany + '/'; }
    if (selectedRequestTypeIndex != 0) {
        searchlist = searchlist + RequestTypetext + '/';
    }

    searchlist = searchlist.substring(0, searchlist.length - 1)
    document.getElementById('spnSearchResult').innerHTML = searchlist;
    RequestTypetext = (RequestTypetext == 'Non Traditional') ? 'NonTradition' : RequestTypetext;
    RequestTypetext = (RequestTypetext == 'Others') ? 'Other' : RequestTypetext;
    if (searchlist.length > 0) {
        hideError();
        pagingCount = $('#ddlSearchProjectPaging').val();
        $("#trAddProject").show();
        $("#trShowPaging").show();
        $('#searchProjectList').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: false,
            defaultSorting: 'Name ASC',
            selecting: false,
            selectingCheckboxes: false,
            multiSelect: false,
            actions: {
                listAction: '/GCS/ClearanceProject/SearchCancelledProject'
            },
            recordsLoaded: function (event, data) {
                var heightGrid = window.innerHeight - $('#deleteWorkgroup').height() - $('#trShowPaging').height() - 144;
                $('#searchProjectList .jtable-main-container').css('max-height', (heightGrid));
            },
            fields: {
                ProjectReferenceId: {
                    key: true,
                    create: false,
                    edit: true,
                    list: false
                },
                ClrProjectId: {
                    list: false
                },
                ProjectReferenceId: {
                    title: jHProjectReferenceNumber,
                    width: '15%',
                    display:
                    function (Project) {
                        return getDisplayText(Project);
                    }
                },
                ProjectTitle: {
                    title: jHProjectTitle,
                    width: '20%',
                    display:
                    function (Project) {
                        return GetTitleText(Project);
                    }
                },

                LocalReference: {
                    title: jHLocalReference,
                    width: '10%'
                },
                ProjectTypeDesc: {
                    title: jHProjectType,
                    width: '10%'
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
                    width: '15%'
                },
                RequestTypeDesc: {
                    title: 'Request Type',
                    width: '5%',
                    visibility: $('#hdnReadOnly').val() == 1 ? 'visible' : 'hidden'
                },
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
                    });
                }
                else {
                    //No rows selected
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
            ReadOnly: $('#hdnReadOnly').val() == 1 ? "Y" : "N",
            RequestTypeDesc: RequestTypetext
        });
    } else {
        showError(mandatorySearchCriteria);

        return false;
    }
}

function GetTitleText(Project) {
    if (Project.record.ProjectTitle.length > 25) {
        return '<div title="' + Project.record.ProjectTitle + '">' + Project.record.ProjectTitle.substring(0, 25) + '</div>'
    }
    else {
        return Project.record.ProjectTitle;
    }
}

function getDisplayText(Project) {
    if ($('#hdnReadOnly').val() == 1) {
        var ProjRefNo = Project.record.ProjectReferenceId;
        var projectId = Project.record.ClrProjectId;
        var projectStatus = Project.record.StatusTypeDesc;
        var projectType = Project.record.ProjectTypeDesc;

        var displayText = $('<a href="#"' + 'style="text-decoration:underline !important;">' + ProjRefNo + '</a>').click(function (e) {
            OpenCancelledCompletedProject(projectId, projectType, projectStatus);
            e.preventDefault();
        });
        return displayText;
    }
    else {
        var projectReferenceNum = Project.record.ProjectReferenceId;
        var displayText = $('<a href="#"' + 'style="text-decoration:underline !important;">' + projectReferenceNum + '</a>').click(function (e) {
            CheckUserStatusLocked(Project);
            e.preventDefault();
        });

        return displayText;
    }
}

function projInfo(ProjRefTypeDesc) {
    var substr = ProjRefTypeDesc.split('~');
    var ProjId = substr[0];
    var ProjectTypeDesc = substr[1];
    var ProjStatus = substr[2];
    document.getElementById('SelectedProjectId').value = ProjId;
    document.getElementById('SelectedProjectType').value = ProjectTypeDesc;
    if (ProjStatus == GCSCancelStatus) { document.getElementById('CancelledProjectId').value = ProjId; } else { document.getElementById('CancelledProjectId').value = 0; }
    if (ProjStatus == GCSReOpenedStatus) { document.getElementById('ReOpenedProjectId').value = ProjId; } else { document.getElementById('ReOpenedProjectId').value = 0; }
    if (ProjStatus == GCSSubmittedStatus) { document.getElementById('SubmittedProjectId').value = ProjId; } else { document.getElementById('SubmittedProjectId').value = 0; }
    if (ProjStatus == GCSReSubmittedStatus) { document.getElementById('ReSubmittedProjectId').value = ProjId; } else { document.getElementById('ReSubmittedProjectId').value = 0; }
    if (ProjStatus == GCSCompletedStatus) { document.getElementById('CompleteProjectId').value = ProjId; } else { document.getElementById('CompleteProjectId').value = 0; }
    var formId = document.forms[0];
    formId.submit();
}

function CheckUserStatusLocked(Project) {
    var projectId = Project.record.ClrProjectId;
    var projectStatus = Project.record.StatusTypeDesc;
    var projectType = Project.record.ProjectTypeDesc;
    var values =
                {
                    "projectId": projectId
                }
    if ($('#hdnCurrentUserRole').val() != "Y") {
        $.ajax({
            url: '/GCS/ClearanceProject/CheckUserStatusLocked',
            type: 'POST',
            async: false,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(values),
            success: function (data) {
                if (data.length != 0 && data.Result == "OK") {
                    openClearanceProject(projectId, projectType, projectStatus);
                }
                else {
                    showPopupLockedProject(projectId, projectType, projectStatus, data.Result);
                }
            }
        });
    }
    else {
        LoadClearanceProjectInquiry(projectId, projectType, projectStatus);
    }
}

function LoadClearanceProjectInquiry(projectId, projectType) {
    projectType = projectType == 'Master' ? 1 : 2;
    var url = '/GCS/ClearanceProject/OpenClearanceProjectInReadOnly?Projectid=' + projectId + "&ProjectTypeId=" + projectType;
    window.open(url, 'ProjectDetail' + projectId, 'resizable=yes');
}

function openClearanceProject(projectId, projectType, projectStatus) {
    projectType = projectType == 'Master' ? 1 : 2;
    var url = '/GCS/ClearanceProject/OpenClearanceProject?Projectid=' + projectId + "&ProjectTypeId=" + projectType + "&ProjectStatus=" + projectStatus;
    window.open(url, 'ProjectDetail' + projectId, 'resizable=yes');
}

function OpenCancelledCompletedProject(ProjId, ProjType, ProjectStatus) {
    ProjType = ProjType == 'Master' ? 1 : 2;
    var url = '/GCS/ClearanceProject/OpenCancelledAndCompletedProjects?Projectid=' + ProjId + "&ProjectTypeId=" + ProjType + "&ProjectStatus=" + ProjectStatus;
    window.open(url, 'ProjectDetail' + ProjId, 'resizable=yes');
}

// for RESET click
$(function () {
    $('#btnSearchCancelledProjectReset').live("click", function () {
        hideError();
        $("#trAddProject").hide();

        $("#trShowPaging").hide();

        document.getElementById('txtPRN').value = '';
        document.getElementById('ddlProjectTypeMaintain').selectedIndex = 0;
        document.getElementById('txtUPC').value = '';
        document.getElementById('txtProjectTitle').value = '';
        document.getElementById('ddlRequestTypeMaintain').selectedIndex = 0;
        document.getElementById('txtRequestor').value = '';
        document.getElementById('txtLocalReference').value = '';
        document.getElementById('txtRequestingCompany').value = '';

        jQuery("#txtPRN").watermark("Project Ref No.");
        jQuery("#txtLocalReference").watermark("Local Ref.");
        jQuery("#txtRequestingCompany").watermark("Requesting Comp.");
        jQuery("#txtUPC").watermark("Third Party Comp.");
        jQuery("#txtRequestor").watermark("Requestor");
        jQuery("#txtProjectTitle").watermark("Project Title");
        return false;
    });
});

//Set the default button for project search page
$('body').keydown(function (e) {
    if (e.keyCode == 13) {
        $("#btnSearchCancelledProject").trigger('click');
    }
});

function showError(message) {
    $("#divErrorMessage").text(message);
    $('#divErrorMessage').addClass('warning');
    $("#divErrorMessage").show();
}

function hideError() {
    $("#divErrorMessage").text('');
    $('#divErrorMessage').removeClass('warning');
    $("#divErrorMessage").hide();
}

function RemoveSpace(txtval) {
    if (($.trim(txtval)) != '') {
        return $.trim(txtval);
    }
    else {
        return "";
    }
}

function showPopupLockedProject(ProjId, ProjType, ProjectStatus, userName) {
    var msg = projectLockingMsg.replace("User", userName);
    var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + msg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: "Locked Project",
                show: 'clip',
                hide: 'clip',
                width: 300
            });

    objWarningDialog.dialog('open');
    objWarningDialog.dialog({
        buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        LoadClearanceProjectInquiry(ProjId, ProjType, ProjectStatus)
                    },
                    'No': function () {
                        $(this).dialog('close');
                    }
                }
    });
}

function ChangeProjectTypeMaintain(ProjectTypeId) {
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

            $('#ddlRequestTypeMaintain').html(items);
        }
    });
}