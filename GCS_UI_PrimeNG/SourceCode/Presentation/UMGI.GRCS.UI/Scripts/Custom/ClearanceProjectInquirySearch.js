

//For Inquiry Search Button click
$(function () {
    $('#btnSearchProject').click(function (e) {
        var pagingCount = 25;
        $('#ddlSearchProjectPaging').val(25);
        LoadSearchJtableForInquerySearch(pagingCount);
    });
});

//change the dropdown selection
$(function () {
    $('#ddlSearchProjectPaging').change(function () {
        var rowCount = $(this).val();
        LoadSearchJtableForInquerySearch(rowCount);
    });
});


function LoadSearchJtableForInquerySearch(pageSize) {

    $('#searchProjectListInquiry').jtable('destroy');
    var searchlist = '';

    //Project Ref Number
    var PRN =RemoveSpace($('#txtPRN').val());

    //3rd party
    var ThirdPartyCompany = RemoveSpace($('#txtThirdPartyCompany').val());

    //Project Title
    var ProjectTitle = RemoveSpace($('#txtProjectTitle').val());

    //local ref.
    var LocalReference = RemoveSpace($('#txtLocalReference').val());

    //Requestor
    var Requestor = RemoveSpace($('#txtRequestor').val());

    //Requesting Company
    var selectedCompanyListIndex = $('#CompanyList')[0].selectedIndex;
    var CompanyListtext = $('#CompanyList')[0][selectedCompanyListIndex].text;
    var CompanyListID = $('#CompanyList')[0][selectedCompanyListIndex].value;
    var CompanyListValue = -1;
    if (CompanyListID.length > 0) CompanyListValue = CompanyListID;


    //Project Type
    var selectedProjectTypeIndex = $('#ddlInquiryProjectType')[0].selectedIndex;
    var ProjectTypetext = $('#ddlInquiryProjectType')[0][selectedProjectTypeIndex].text;
    var ProjectTypeID = $('#ddlInquiryProjectType')[0][selectedProjectTypeIndex].value;
    var ProjectTypeValue = -1;
    if (ProjectTypeID.length > 0) ProjectTypeValue = ProjectTypeID;

    //Request Type
    var selectedRequestTypeIndex = $('#ddlInquiryRequestType')[0].selectedIndex;
    var RequestTypetext = $('#ddlInquiryRequestType')[0][selectedRequestTypeIndex].text;
    var RequestTypeID = $('#ddlInquiryRequestType')[0][selectedRequestTypeIndex].value;
    var RequestTypeValue = -1;
    if (RequestTypeID.length > 0) RequestTypeValue = RequestTypeID;

    //UPC
    var UPC = RemoveSpace($('#txtUPC').val());
    //ISRC
    var ISRC = RemoveSpace($('#txtISRC').val());
    //ArtistName
    var ArtistName = RemoveSpace($('#txtArtistName').val());
    //ReleaseTitle
    var ReleaseTitle = RemoveSpace($('#txtReleaseTitle').val());
    //ResourceTitle
    var ResourceTitle =RemoveSpace($('#txtResourceTitle').val());
    //Version
    var VersionTitle = RemoveSpace($('#txtVersion').val());

    //If exact match is selected or not
    
    if ($('#ArtistExactMatchCheck').is(':checked')) {
        var ArtistExactMatch = true;
    }
    else {
        var ArtistExactMatch = false;
    }

    if (PRN != '') { searchlist = searchlist + PRN + '/'; }
    if (selectedProjectTypeIndex > 0) { searchlist = searchlist + ProjectTypetext + '/'; }
    if (selectedCompanyListIndex > 0) { searchlist = searchlist + CompanyListtext + '/'; }
    if (ProjectTitle != '') { searchlist = searchlist + ProjectTitle + '/'; }
    if (selectedRequestTypeIndex > 0) { searchlist = searchlist + RequestTypetext + '/'; }
    if (Requestor != '') { searchlist = searchlist + Requestor + '/'; }
    if (LocalReference != '') { searchlist = searchlist + LocalReference + '/'; }
    if (ThirdPartyCompany != '') { searchlist = searchlist + ThirdPartyCompany + '/'; }

    if (UPC != '') { searchlist = searchlist + UPC + '/'; }
    if (ISRC != '') { searchlist = searchlist + ISRC + '/'; }
    if (ArtistName != '') { searchlist = searchlist + ArtistName + '/'; }
    if (ReleaseTitle != '') { searchlist = searchlist + ReleaseTitle + '/'; }
    if (ResourceTitle != '') { searchlist = searchlist + ResourceTitle + '/'; }
    if (VersionTitle != '') { searchlist = searchlist + VersionTitle + '/'; }

    searchlist = searchlist.substring(0, searchlist.length - 1)
    document.getElementById('spnSearchResult').innerHTML = searchlist;
    RequestTypetext = (RequestTypetext == 'Non Traditional') ? 'NonTradition' : RequestTypetext;
    RequestTypetext = (RequestTypetext == 'Others') ? 'Other' : RequestTypetext;
    if (searchlist.length > 0) {
        $("#divErrorMessage").text("");
        $("#divErrorMessage").hide();
        pagingCount = $('#ddlSearchProjectPaging').val();
        $("#trShowPaging").show();
        $('#searchProjectListInquiry').jtable({
            paging: true,
            pageSize: pagingCount,
            sorting: true,
            defaultSorting: 'ProjectReferenceId ASC',
            selecting: true,
            selectingCheckboxes: false,
            multiSelect: false,
            actions: {
                listAction: '/GCS/Search/InquirySearch'
            },
            recordsLoaded: function (event, data) {
                var heightGrid = window.innerHeight - $('#mainPanelDv11').height() - $('#mainPanelDv13').height() - $('#trShowPaging').height() - 144;
                $("#searchProjectListInquiry .jtable-main-container").css({ "padding": "5px 15px", "width": "auto", "position": "relative", "overflow-y": "auto", "overflow-x": "hidden", "height": heightGrid });
            },
            fields: {
                ProjectReferenceId: {
                    key: true,
                    create: false,
                    edit: true,
                    list: false
                },
                ClearanceProjectID: {
                    title: 'P.ID',
                    width: '0%',
                    list: false
                },
                ProjectReferenceId: {
                    title: 'Project Reference Number',
                    width: '15%',
                    sorting: true,
                    display: function (Project) { //1
                        var ProjRefNo = Project.record.ProjectReferenceId;
                        var ProjId = Project.record.ClearanceProjectID;
                        var projStatus = Project.record.StatusType;
                        var ProjType = Project.record.ProjectType;
                        var test = $('<a href="#"' + 'style="text-decoration:underline !important;">' + ProjRefNo + '</a>').click(function (e) {
                            LoadClearanceProjectInquiry(ProjId, ProjType)
                            e.preventDefault();
                        });
                        return test;
                    } //1
                },
                ProjectTitle: {
                    title: 'Project Title',
                    width: '15%',
                    sorting: true
                },
                LocalReference: {
                    title: 'Local Reference',
                    width: '12%',
                    sorting: true
                },
                ProjectTypeDesc: {
                    title: 'Project Type',
                    width: '10%',
                    sorting: true
                },
                RequestTypeDesc: {
                    title: 'Requestor Type',
                    width: '15%',
                    sorting: true
                },
                RequestCompanyName: {
                    title: 'Requesting Company',
                    width: '14%',
                    sorting: true
                },
                ThirdPartyCompanyName: {
                    title: '3rd Party Company',
                    width: '12%',
                    sorting: true
                },
                StatusTypeDesc: {
                    title: 'Status',
                    width: '15%',
                    sorting: true
                }
            },

            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#searchProjectListInquiry').jtable('selectedRows');
                $('#SelectedRowListInquiry').empty();
                if ($selectedRows.length > 0) {
                    $selectedRows.each(function () {
                        //Show selected rows
                        
                    });
                }
                else {
                    //No rows selected
                }
            }

        });

        $('#searchProjectListInquiry').jtable('load', {
            ProjectReferenceId: PRN,
            ProjectTypeId: ProjectTypeValue,
            ThirdPartyCompany: ThirdPartyCompany,
            ProjectTitle: ProjectTitle,
            RequestTypeID: RequestTypeValue,
            LocalReference: LocalReference,
            Requestor: Requestor,
            RequestingCompany: CompanyListValue, //RequestingCompany,
            UPC: UPC,
            ISRC: ISRC,
            ArtistName: ArtistName,
            ReleaseTitle: ReleaseTitle,
            ResourceTitle: ResourceTitle,
            VersionTitle: VersionTitle,
            RequestTypeDesc: RequestTypetext,
            ArtistExactMatch: ArtistExactMatch
        });
    } else {
        $("#divErrorMessage").text('Please enter atleast one search criteria');
        $('#divErrorMessage').addClass('warning');
        $("#divErrorMessage").show();
    }

}


$("#imgProjectLevel").live("click", function () {
    $("#trProjectLevel").slideToggle("fast");
    var imgSrc = $("#imgProjectLevel").attr("src");
    if (imgSrc.indexOf('left') == -1) {
        $("#imgProjectLevel").attr("src", "/Gcs/Images/left.png");
        $("#imgProjectLevel").attr("title", "Expand");
    }
    else {
        $("#imgProjectLevel").attr("src", "/Gcs/Images/Down.png");
        $("#imgProjectLevel").attr("title", "Collapse");
    }
    // return false;
});

$("#imgReleaseLevel").live("click", function () {
    $("#trReleaseLevel").slideToggle("fast");
    var imgSrc = $("#imgReleaseLevel").attr("src");
    if (imgSrc.indexOf('left') == -1) {
        $("#imgReleaseLevel").attr("src", "/Gcs/Images/left.png");
        $("#imgReleaseLevel").attr("title", "Expand");
    }
    else {
        $("#imgReleaseLevel").attr("src", "/Gcs/Images/Down.png");
        $("#imgReleaseLevel").attr("title", "Collapse");
    }
});


// for RESET click
$(function () {
    $('#btnSearchProjectReset').click(function () {
        $('#searchProjectListInquiry').jtable('destroy');
        $('#SelectedRowListInquiry').empty();
        $('#spnSearchResult').empty();
        $("#trAddProject").hide();
        $("#trShowPaging").hide();
        $("#ddlSearchProjectPaging").hide();

        document.getElementById('txtPRN').value = '';
        document.getElementById('ddlInquiryProjectType').selectedIndex = 0;
        document.getElementById('CompanyList').value = '';

        document.getElementById('txtProjectTitle').value = '';
        document.getElementById('ddlInquiryRequestType').selectedIndex = 0;
        document.getElementById('txtRequestor').value = '';
        document.getElementById('txtLocalReference').value = '';
        document.getElementById('txtThirdPartyCompany').value = '';

        document.getElementById('txtUPC').value = '';
        document.getElementById('txtISRC').value = '';
        document.getElementById('txtArtistName').value = '';
        document.getElementById('txtReleaseTitle').value = '';
        document.getElementById('txtResourceTitle').value = '';
        document.getElementById('txtVersion').value = '';
        return false;
    });
});



function LoadClearanceProjectInquiry(ProjId, ProjectTypeValue) {
  
 
    var url = '/GCS/ClearanceProject/OpenClearanceProjectInReadOnly?Projectid=' + ProjId + "&ProjectTypeId=" + ProjectTypeValue;
    window.open(url, 'ProjectDetail' + ProjId, 'resizable=yes');

}




$(document).ready(function () { //1
    $("#ProjectType").change(function () {//2      
        var id = $(this).val();
        $.getJSON("/GCS/Search/GetRequestTypeList", { id: id }, //3
    function (carData) { //4        
        var select = $("#RequestType");
        select.empty();
        select.append($('<option/>', {
            value: 0,
            text: "Select"
        })); //4        
        $.each(carData, function (index, itemData) { //5            
            select.append($('<option/>', {//6
                value: itemData.Value,
                text: itemData.Description
            })); //6
        }); //5
    }); //3
    }); //2
});  //1



function moveDialogBoxAtTop() {

    var dialogue = $('.ui-dialog')

    dialogue.animate({

        top: "40px"
    }, 0);

}



function RemoveSpace(txtval) {
    if (($.trim(txtval)) != '') {
        return $.trim(txtval);
    }
    else {
        return "";
    }
}


function ChangeInquiryProjectType(ProjectTypeId) {

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

            $('#ddlInquiryRequestType').html(items);

        }


    });
}