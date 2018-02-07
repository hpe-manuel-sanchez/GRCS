

$(function () {
    $('#btnLockedSearch').click(function (e) {
        var pagingCount = 25;
       // $('#ddlSearchProjectPaging').val(25);
        SearchProjectLocking(pagingCount);
    });

});

//change the dropdown selection
$(function () {
    $('#ddlSearchProjectPaging').change(function () {
        var rowCount = $(this).val();
        SearchProjectLocking(rowCount);
    });
});

function SearchProjectLocking(pageSize) {
 

    var searchlist = '';
    var pagingCount = 5;
    //Project Reference Number
    var projectReferenceNum = RemoveSpace($('#txtProjectRefNumber').val());
    if ($.trim(projectReferenceNum) != '') { searchlist = searchlist + projectReferenceNum  }
    document.getElementById('spnSearchResult').innerHTML = searchlist;

    if (searchlist.length > 0) {
        hideError();
        $("#trShowPaging").show();
        pagingCount = $('#ddlSearchProjectPaging').val();
     
        $("#projectLockingPaging").show();
        $('#searcProjectLockingList').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: false,
            defaultSorting: 'Name ASC',
            selecting: false,
            selectingCheckboxes: false,
            multiSelect: false,
            actions: {
                listAction: '/GCS/ClearanceProject/SearchClearanceUnlockedProjects'
            },

            fields: {
                ProjectReferenceId: {

                    create: false,
                    edit: true,
                    list: false
                },
                ClrProjectId: {
                    list: false,
                    key: true
                },
                ProjectReferenceId: {
                    title: jHProjectReferenceNumber,
                    width: '20%'
                  

                },
                ProjectTitle: {
                    title: jHProjectTitle,
                    width: '20%'

                },
                Status_Type: {
                    title: jHLockingStatus,
                    width: '10%',
                    display:
                    function (Project) {
                        return getLoggedInUserText(Project);
                    }

                },
                LoggedInUser: {
                    title: jHLockedBy,
                    width: '10%'


                },
                Project_Type: {
                    title :'',
                    width: '10%',
                    display:
                    function (Project) {
                        return getLoggedInUserButton(Project);
                    }

                }

                   

            }



        });

        $('#searcProjectLockingList').jtable('load', {
            ProjectReferenceId: projectReferenceNum
           
        });
    } else {
        showError(mandatorySearchCriteria);

        return false;

     
    }

}


function getLoggedInUserButton(Project) {
   
        if(Project.record.LoggedInUser == undefined || Project.record.LoggedInUser == null || Project.record.LoggedInUser == '')
        {

            return "";
          }
        else
        {
            var unlockButton = '<input type="button" class="primbutton" value="Unlock" onclick=unlockProject(' + Project.record.ClrProjectId + ')>';
            return unlockButton;
        }
        
}


function unlockProject( projectid) {
    var values =
                {
                    "projectId": projectid
                }


    $.ajax({
        url: '/GCS/ClearanceProject/UnlockProject',
        type: 'POST',
        async: false,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(values),
        success: function (data) {

          
            if (data.length != 0) {
                var pagingCount = 25;
                SearchProjectLocking(pagingCount);
              
            }
        }
    });
}
function getLoggedInUserText(Project) {
  

if(Project.record.LoggedInUser == undefined || Project.record.LoggedInUser == null || Project.record.LoggedInUser == '')
        return 'Unlocked';
        else return 'Locked';
       
       
}

function getDisplayText(Project) {

   

        var ProjRefNo = Project.record.ProjectReferenceId;
        var ProjId = Project.record.ClrProjectId;
        var projStatus = Project.record.StatusTypeDesc;
        var ProjType = Project.record.ProjectTypeDesc;

        var test = $('<a href="#"' + 'style="text-decoration:underline !important;">' + ProjRefNo + '</a>').click(function (e) {
            LoadClearanceProjectInquiry(ProjId, ProjType, projStatus);
            e.preventDefault();
        });
        return test;
 
  
}

function LoadClearanceProjectInquiry(ProjId, ProjType, ProjectStatus) {
    ProjType = ProjType == 'Master' ? 0 : 1;

    var url = '/GCS/ClearanceProject/OpenCancelledAndCompletedProjects?Projectid=' + ProjId + "&ProjectTypeId=" + ProjType + "&ProjectStatus=" + ProjectStatus;
    window.open(url, "_blank");
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

$('#btnLockReset').live("click", function () {

     
        hideError();
        $("#projectLockingPaging").hide();
        document.getElementById('txtProjectRefNumber').value = '';
        jQuery("#txtProjectRefNumber").watermark("Project Ref No.");
       
        return false;
    });