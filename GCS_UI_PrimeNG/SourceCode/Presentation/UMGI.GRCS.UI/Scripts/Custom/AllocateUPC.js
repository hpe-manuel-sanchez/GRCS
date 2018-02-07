

$(function () {
    $('#btnSearchUPC').click(function (e) {
        var pagingCount = 25;
       // $('#ddlSearchProjectPaging').val(25);
        SearchAllocateUPCProject(pagingCount);
    });

});

//change the dropdown selection
$(function () {
    $('#ddlSearchProjectPaging').change(function () {
        var rowCount = $(this).val();
        SearchAllocateUPCProject(rowCount);
    });
});

function SearchAllocateUPCProject(pageSize) {

    var searchlist = '';
    var pagingCount = 5;
    //Project Reference Number
    var projectReferenceNum = RemoveSpace($('#txtProjectRefNumber').val());
    if ($.trim(projectReferenceNum) != '') { searchlist = searchlist + projectReferenceNum  }
   

    if (searchlist.length > 0) {
        hideError();
        $("#trShowPaging").show();
        pagingCount = $('#ddlSearchProjectPaging').val();
     
        $("#allocateUpcPaging").show();
        $('#searcAllocateUPCList').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: false,
            defaultSorting: 'Name ASC',
            selecting: false,
            selectingCheckboxes: false,
            multiSelect: false,
            actions: {
                listAction: '/GCS/ClearanceProject/SearchProjectToAllocateUPC'
            },
            recordsLoaded: function (event, data) {

                var rowIndex = data.serverResponse.TotalRecordCount;
                document.getElementById('spnSearchResult').innerHTML = "\"" + searchlist + "\"" + " (" + rowIndex + ")";




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
                    width: '20%'

                }



            }



        });

        $('#searcAllocateUPCList').jtable('load', {
            ProjectReferenceId: projectReferenceNum
           
        });
    } else {
        showError(mandatorySearchCriteria);

        return false;

        //alert('searchWorkgroupMessages.searchInValid'); 
    }

}


function getDisplayText(Project) {


   
        var ProjRefNo = Project.record.ProjectReferenceId;
        var ProjId = Project.record.ClrProjectId;
        var projStatus = Project.record.ProjectTypeId;
        var ProjType = Project.record.ProjectTypeDesc;

        var test = $('<a href="#"' + 'style="text-decoration:underline !important;">' + ProjRefNo + '</a>').click(function (e) {

            viewProject(ProjId, ProjType, projStatus);
            e.preventDefault();
        });
        return test;
 
  
}

function LoadClearanceProjectUPC(ProjId, ProjType, ProjectStatus) {

    ProjType = ProjType == 'Master' ? 1 : 2;
    var url = '/GCS/ClearanceProject/OpenCancelledAndCompletedProjects?Projectid=' + ProjId + "&ProjectTypeId=" + ProjType + "&ProjectStatus=" + ProjectStatus;
    window.open(url, 'ProjectDetail' + ProjId, 'resizable=yes');
}





function viewProject(clearanceProjectId, projectTypeId, projectStatusId) {
 
    var activeRoleGroup = 'RCCAdmin';
    LoadProject(clearanceProjectId, 2, projectStatusId, activeRoleGroup);

}

function LoadProject(clearanceProjectId, projectTypeId, projectStatusId, activeRoleGroup) {

    var wwidth = $(window).width() - 50;
    var wheight = $(window).height() - 50;
    var url = url_OpenProjectDetail + '?Projectid=' + clearanceProjectId + '&ProjectTypeId=' + projectTypeId + '&ProjectStatus=' + projectStatusId + '&RoleGroup=' + activeRoleGroup;
    var w = window.open(url, 'ProjectDetail' + clearanceProjectId, 'width=' + wwidth + 'px,height=' + wheight + 'px,top=40px,left=10px,resizable=yes', false);


    //w.addEventListener('onload', alert("Hi"), false);
}

function doSomething() {
    var loadingPanel = $($.find('#loadingDv'));



    loadingPanel.show();


}



function LoadClearanceProjectInquiry(projectId, projectType) {

    projectType = projectType == 'Master' ? 1 : 2;
    var url = '/GCS/ClearanceProject/OpenClearanceProjectInReadOnly?Projectid=' + projectId + "&ProjectTypeId=" + projectType;
    window.open(url, 'ProjectDetail' + projectId, 'resizable=yes');
}

function RemoveSpace(txtval) {
    if (($.trim(txtval)) != '') {
        return $.trim(txtval);
    }
    else {
        return "";
    }
}

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

$('#btnResetUPC').live("click", function () {

     
        hideError();
      

        $("#allocateUpcPaging").hide();
        document.getElementById('txtProjectRefNumber').value = '';
        jQuery("#txtProjectRefNumber").watermark("Project Ref No.");
       
        return false;
    });

//Set the default button for project search page
    $('body').keydown(function (e) {

        if (e.keyCode == 13) {
            $("#btnSearchUPC").trigger('click');
        }

        if (e.charCode == 13) {
            $("#btnSearchUPC").trigger('click');
        }
    });