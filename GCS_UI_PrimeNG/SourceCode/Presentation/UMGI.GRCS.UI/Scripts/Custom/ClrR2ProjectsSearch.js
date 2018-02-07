var rowIndex = -1;
var pagingCount = 5;

$(document).ready(function () {

    autocomplete("#AdminCompanyName");
    autocomplete("#companyNameFreeHand");

    function autocomplete(id) {

        $(id).autocomplete({
            appendTo: "#projectTab",

            source: function (request, response) {

                var url = $(id).attr("data-autocomplete-source-manual");

                $(id).addClass("ui-autocomplete-loading")

                $(id).removeClass("ui-autocomplete-input")


                $.getJSON(url, { term: request.term, searchBy: $(id).attr("SearchFor") }, function (data) {

                    response(data);

                    // empty company id
                    $('#AdminCompanyId').val("");
                    $('#companyIdFreeHand').val("");

                    // empty division and label  
                    $('#DivisionId').empty();
                    var mySelect = $('#DivisionId');
                    mySelect.append(
                    $('<option></option>').val(0).html("--Select--")
                    );

                    $('#LabelId').empty();
                    var mySelectLabelId = $('#LabelId');
                    mySelectLabelId.append(
                    $('<option></option>').val(0).html("--Select--")
                    );

                    $('#divisionIdFreeHand').empty();
                    var mySelectdivisionIdFreeHand = $('#divisionIdFreeHand');
                    mySelectdivisionIdFreeHand.append(
                    $('<option></option>').val(0).html("--Select--")
                    );

                    $('#labelIdFreeHand').empty();
                    var mySelectlabelIdFreeHand = $('#labelIdFreeHand');
                    mySelectlabelIdFreeHand.append(
                    $('<option></option>').val(0).html("--Select--")
                    );


                    $(id).removeClass("ui-autocomplete-loading")

                    $(id).addClass("ui-autocomplete-input")

                });

            },

            minLength: 3,

            select: function (event, ui) {

                // fetch divisions based on company id
                $("#AdminCompanyId").val(ui.item.id);
                $("#companyIdFreeHand").val(ui.item.id);
                $(id).val(ui.item.value);
                if (id.indexOf('companyNameFreeHand') == -1)
                    $('#TaskTypeSelectedValue').val("Search Project");
                else
                    $('#TaskTypeSelectedValue').val("Create Project");
                loadDivisionsDAG();

            }

        });
    }

    $('#DivisionId').change(function () {
        loadDivisionsLabels();

    });

    $('#divisionIdFreeHand').change(function () {
        loadDivisionsLabels();

    });

    // show existing div if user has selected existing option else create new project option only
    function loadDivisionsLabels() {

        if ($('#hdnSearchForR2Project').val() == "1") {
            var dataAdmincompanyId = $('#AdminCompanyId').val();
            $('#companyIdSelectedValue').val(dataAdmincompanyId);
            var companyNameSelectedValue = $('#AdminCompanyName').val();
            $('#companyNameSelectedValue').val(companyNameSelectedValue);
            var divisionIdSelected = $('#DivisionId').val();
            $('#divisionIdSelectedValue').val(divisionIdSelected);
            var projectIdSelected = $('#ProjectId').val();
            $('#projectIdSelectedValue').val(projectIdSelected);
            var projectTitlePush = $('#Title').val();
            $('#projectTitleFreeHandPush').val(projectTitlePush);
            var artistfreeHandPush = $('#ArtistID').val();
            $('#hdnArtistIdPush').val(artistfreeHandPush);
            var artistNamefreeHandPush = $('#ArtistName').val();
            $('#hdnArtistNamePush').val(artistNamefreeHandPush);
            $('#TaskTypeSelectedValue').val("Search Project");
        }
        else {
            var dataAdmincompanyId = $('#companyIdFreeHand').val();
            $('#companyIdSelectedValue').val(dataAdmincompanyId);
            var companyNameSelectedValue = $('#companyNameFreeHand').val();
            $('#companyNameSelectedValue').val(companyNameSelectedValue);
            var divisionIdSelected = $('#divisionIdFreeHand').val();
            $('#divisionIdSelectedValue').val(divisionIdSelected);
            var projectTitlefreeHandPush = $('#projectTitleFreeHand').val();
            $('#projectTitleFreeHandPush').val(projectTitlefreeHandPush);
            $('#TaskTypeSelectedValue').val("Create Project");
            var divArtistText = $('#divArtistName').text();
            $('#divArtistNameSelected').val(divArtistText);
            var divArtistId = $('#hdnArtistId').text();
            $('#hdnArtistIdSelected').val(divArtistId);
        }
        $('#loadingDivPA').show();

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/Search/ProjectSearchLoadDivisionsLabels', digitalVal, function (data) {

            $('#loadingDivPA').hide();

            $('#SearchR2ProjectsDialog').html(data);
            $('#divArtistName').text($('#ArtistNameInfo').val());
            $('#hdnArtistId').text($('#ArtistIdInfo').val());

        });

    }

    function loadDivisionsDAG() {

        if ($('#hdnSearchForR2Project').val() == "1") {
            var dataAdmincompanyId = $('#AdminCompanyId').val();
            $('#companyIdSelectedValue').val(dataAdmincompanyId);
            var companyNameSelectedValue = $('#AdminCompanyName').val();
            $('#companyNameSelectedValue').val(companyNameSelectedValue);
            var divisionIdSelected = $('#DivisionId').val();
            $('#divisionIdSelectedValue').val(divisionIdSelected);
            var projectIdSelected = $('#ProjectId').val();
            $('#projectIdSelectedValue').val(projectIdSelected);
            var projectTitlePush = $('#Title').val();
            $('#projectTitleFreeHandPush').val(projectTitlePush);
            var artistfreeHandPush = $('#ArtistID').val();
            $('#hdnArtistIdPush').val(artistfreeHandPush);
            var artistNamefreeHandPush = $('#ArtistName').val();
            $('#hdnArtistNamePush').val(artistNamefreeHandPush);
        }
        else {
            var dataAdmincompanyId = $('#companyIdFreeHand').val();
            $('#companyIdSelectedValue').val(dataAdmincompanyId);
            var companyNameSelectedValue = $('#companyNameFreeHand').val();
            $('#companyNameSelectedValue').val(companyNameSelectedValue);
            var divisionIdSelected = $('#divisionIdFreeHand').val();
            $('#divisionIdSelectedValue').val(divisionIdSelected);
            var projectTitlefreeHandPush = $('#projectTitleFreeHand').val();
            $('#projectTitleFreeHandPush').val(projectTitlefreeHandPush);

            var divArtistText = $('#divArtistName').text();
            $('#divArtistNameSelected').val(divArtistText);
            var divArtistId = $('#hdnArtistId').text();
            $('#hdnArtistIdSelected').val(divArtistId);
        }
        $('#loadingDivPA').show();

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/Search/ProjectSearchLoadDivisionsDAG', digitalVal, function (data) {
            $('#loadingDivPA').hide();

            $('#SearchR2ProjectsDialog').html(data);

            $('#divArtistName').text($('#ArtistNameInfo').val());
            $('#hdnArtistId').text($('#ArtistIdInfo').val());

        });

    }

    if ($('#hdnSearchForR2Project').val() == 2) {
        $('#projectaccordion').hide();
        $('#trNoResults').hide();
        $('#trShowPaging').hide();
        $('#freeHand').show();
    }

    // Manage artist call starts
    $("#btnManageArtist").click(function (event) {


        var objArtistPopup = $('<div id="ArtistResourcePopup"></div>');
        if ($('#hdnArtistId').text() != "") {
            $(objArtistPopup).dialog({
                autoOpen: true,
                width: 1000,
                close: function () {
                    $(this).remove(); // ensures any form variables are reset.  
                    $(this).dialog('close');
                    $('#loadingDivPA').hide();
                },
                resizable: false,
                title: 'Manage Artist',
                close: function () {
                    $(this).remove(); // ensures any form variables are reset.  
                    $(this).dialog('close');
                    $('#loadingDivPA').hide();
                },
                modal: true
            });
            var values =
        {
            "existingArtist": $('#hdnArtistId').text()
        }

            $(objArtistPopup).load('/GCS/Artist/SearchForArtist', values);
            //added by Dhruv
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
                close: function () {
                    $(this).remove(); // ensures any form variables are reset.  
                    $(this).dialog('close');
                    $('#loadingDivPA').hide();
                },
                modal: true,
                open: function () {
                    $(this).load('/GCS/Artist/SearchForArtist');
                }
            });
            var dialogue = $('.ui-dialog');

            dialogue.animate({
                top: "40px"
            }, 0);


        }


    });

    $(function () {
        $('#itemPerPage').change(function () {
            LoadSearchJtable();
        });
    });

    // Manage artist call end here
    function LoadSearchJtable() {

        var pageSize = 25;
        var searchlist = '';
        var projectID = $('#ProjectId').val();
        var artistName = $('#ArtistName').val();
        var projectTitle = $('#Title').val();
        var label = $('#LabelId option:selected').text();
        var labelId = $('#LabelId').val();
        var dataAdmincompany = $("#AdminCompanyId option:selected").text();
        var dataAdmincompanyId = $('#AdminCompanyId').val();
        var artistID = $('#ArtistID').val();
        var division = $('#DivisionId option:selected').text();
        var divisionId = $('#DivisionId').val();
        if (projectID != '') { searchlist = searchlist + projectID + '/'; }
        if (artistName != '') { searchlist = searchlist + artistName + '/'; }
        if (projectTitle != '') { searchlist = searchlist + projectTitle + '/'; }
        if (labelId != 0) { searchlist = searchlist + label + '/'; }
        if (dataAdmincompanyId != 0) { searchlist = searchlist + dataAdmincompany + '/'; }
        if (artistID != 0) { searchlist = searchlist + artistID + '/'; }
        if (divisionId != 0) { searchlist = searchlist + division + '/'; }

        searchlist = searchlist.substring(0, searchlist.length - 1)
        document.getElementById('spnSearchResult').innerHTML = searchlist;
        document.getElementById('spnNoResults').innerHTML = searchlist;

        // show loader
        $('#loadingDivPA').show();

        pagingCount = $('#itemPerPage').val();


        $('#projectgrid').jtable({
            paging: true,
            pageSize: $('#itemPerPage').val() == 0 ? 25 : $('#itemPerPage').val(),
            sorting: true,
            defaultSorting: 'Name ASC',
            selecting: true,
            multiselect: false,
            selectOnRowClick: true,
            actions:
        {
            listAction: '/GCS/ClearanceProject/ProjectSearch?projectCode=' + projectID + '&projectTitle=' + projectTitle + '&artistId=' + artistID + '&artistName=' + artistName + '&divisionId=' + divisionId + '&labelId=' + labelId + '&dataAdminCompanyId=' + dataAdmincompanyId + '&pageSize=' + $('#itemPerPage').val() + '&RowIndex=' + rowIndex
        },

            loadingRecords: function () { $('.jtable .jtable-no-data-row').hide(); },
            recordsLoaded: function (event, data) {

                rowIndex = data.serverResponse.RowIndex;
                ProjectSearchReset(rowIndex);
                $('#loadingDivPA').hide();
                SectionHideShow(data);
                $('.jtable .jtable-no-data-row').show();
            },
            fields:
        {
            radiobutton: {
                title: 'Select',
                width: '10%',
                sorting: false,
                edit: false,
                create: false,
                display: function () {
                    //creating radiobutton column in list
                    var $radiobutton = $('<input type="radio" value="0" name="rbtnSelect" />');
                    return $radiobutton;
                }
            },

            ProjectCode: {
                title: 'Project ID'
            },

            Title: {
                title: 'Project Description'

            },

            DataAdminCompany: {
                title: 'Company'

            },

            divisionDisplay: {
                title: 'Division'

            },

            Label: {
                title: 'Label'

            },

            ArtistName: {
                title: 'Artist Name'
            },

            artistId: {
                title: 'Artist ID'
            }

        },
            selectionChanged: function () {

                var $selectedRows = $('#projectgrid').jtable('selectedRows');
                $('#searchProjectListPush').val("");
                $('#searchR2ProjectCode').val("");
                if ($selectedRows.length > 0) {
                    //Show selected rows               
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        debugger;
                        $('#searchR2ProjectCode').val(record.ProjectCode);
                        $('#searchProjectListPush').val(record.ProjectId);
                        //                    $("#warningmessageartist").hide();
                    });
                }
            }

        });
        function SectionHideShow(data) {

            if (data.serverResponse.TotalRecordCount > 0) {
                $("#trShowPaging").show();
                $("#projectgrid").show();
                $("#trNoResults").hide();

            }
            else {
                $("#trShowPaging").hide();
                $("#projectgrid").hide();
                $("#trNoResults").show();
            }

        }

        $('#projectgrid').jtable('load',
                    {
                        ProjectCode: $('#ProjectInfo_ProjectId').val(),
                        Title: $('#ProjectInfo_Title').val(),
                        ArtistName: $('#ProjectInfo_ArtistName').val(),
                        projectTitle: $('#ProjectInfo_Title').val(),
                        Label: $('#ProjectInfo_Label').val(),
                        DataAdminCompany: $('#ProjectInfo_DataAdminCompany').val(),
                        divisionDisplay: $('#ProjectInfo_divisionDisplay').val(),
                        artistId: $('#ProjectInfo_artistId').val(),
                        "Criteria.RowIndex": rowIndex,
                        "Criteria.UserName": 'vivek_gupta',
                        "Criteria.PageSize": pagingCount,
                        "Criteria.StartIndex": '0',
                        "Criteria.QualificationCriteria": true

                    });

    }

    function ProjectSearchReset(rowIndex) {
        $('#projectgrid').jtable('reset',
                    {
                        ProjectCode: $('#ProjectInfo_ProjectId').val(),
                        Title: $('#ProjectInfo_Title').val(),
                        ArtistName: $('#ProjectInfo_ArtistName').val(),
                        projectTitle: $('#ProjectInfo_Title').val(),
                        Label: $('#ProjectInfo_Label').val(),
                        DataAdminCompany: $('#ProjectInfo_DataAdminCompany').val(),
                        divisionDisplay: $('#ProjectInfo_divisionDisplay').val(),
                        artistId: $('#ProjectInfo_artistId').val(),
                        "Criteria.RowIndex": rowIndex,
                        "Criteria.UserName": 'vivek_gupta',
                        "Criteria.PageSize": pagingCount,
                        "Criteria.StartIndex": '0',
                        "Criteria.QualificationCriteria": true

                    });
    }

    //Accordion style collapse/expand
    $('#projectaccordion .head').click(function (e) {
        e.preventDefault();
        $(this).next().toggle();
        $(this).find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    function ValidationForRegularProject() {
        var IsValid = true;
        var focusFound = false;
        var focusField = "";
        var TabNo;
        $("#divErrorMsgLinkToR2").text(""); //Set the Validation message blank     

        if (ValidateMandatoryFields('projectTitleFreeHand') == false) {

            IsValid = false;

            $("#divErrorMsgLinkToR2").html(mandatoryFields).show();
            if (focusFound == false) {
                ControlToFocus = "#projectTitleFreeHand";
            }
            focusFound = true;
        }

        if (ValidateMandatoryFields('companyIdFreeHand') == false) {

            IsValid = false;

            $("#divErrorMsgLinkToR2").html(mandatoryFields).show();
            if (focusFound == false) {
                ControlToFocus = "#companyNameFreeHand";
            }
            focusFound = true;
        }
        if (ValidateMandatoryFields('companyNameFreeHand') == false) {

            IsValid = false;

            $("#divErrorMsgLinkToR2").html(mandatoryFields).show();
            if (focusFound == false) {
                ControlToFocus = "#companyNameFreeHand";
            }
            focusFound = true;
        }

        if (ValidateMandatoryFields('divisionIdFreeHand') == false) {

            IsValid = false;

            $("#divErrorMsgLinkToR2").html(mandatoryFields).show();
            if (focusFound == false) {
                ControlToFocus = "#divisionIdFreeHand";
            }
            focusFound = true;
        }

        if (!ValidateDiv('divArtistName', 'btnManageArtist')) {
            IsValid = false;
            NotValid = true;
            if (focusFound == false) {
                focusField = 'btnManageArtist'; focusFound = true;
            }
        }

        if (ValidateMandatoryFields('labelIdFreeHand') == false) {

            IsValid = false;

            $("#divErrorMsgLinkToR2").html(mandatoryFields).show();
            if (focusFound == false) {
                ControlToFocus = "#labelIdFreeHand";
            }
            focusFound = true;
        }

        if (IsValid == false) {
            $('#' + focusField).focus();
            $("#divErrorMsgLinkToR2").text(mandatoryFields);

            return false;
        }
        else {
            $("#divErrorMsgLinkToR2").text("");
            return true;
        }

    }

    function ValidateMandatoryFields(FieldName) {

        if ($('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == 0 || $('#' + FieldName).attr("value") == "" && $('#' + FieldName).is(':visible')) {
            $('#' + FieldName).addClass('input-validation-error');
            return false;
        }
        else {
            $('#' + FieldName).removeClass('input-validation-error');
            return true;
        }
    }

    function ValidateCompanyForProjectSearch() {
        var IsValid = true;
        var focusFound = false;
        var focusField = "";
        var TabNo;
        $("#divErrorMsgLinkToR2").text(""); //Set the Validation message blank     

        if (ValidateMandatoryFields('AdminCompanyId') == false) {

            IsValid = false;

            $("#divErrorMsgLinkToR2").html(mandatoryFields).show();
            if (focusFound == false) {
                ControlToFocus = "#AdminCompanyName";
            }
            focusFound = true;
        }
        if (ValidateMandatoryFields('AdminCompanyName') == false) {

            IsValid = false;

            $("#divErrorMsgLinkToR2").html(mandatoryFields).show();
            if (focusFound == false) {
                ControlToFocus = "#AdminCompanyName";
            }
            focusFound = true;
        }


        if (IsValid == false) {
            $('#' + focusField).focus();
            $("#divErrorMsgLinkToR2").text(mandatoryFields);

            return false;
        }
        else {
            $("#divErrorMsgLinkToR2").text("");
            return true;
        }

    }

    function ValidateDiv(divId, controlToNotice) {

        if ($('#' + divId).html() != "") {
            $('#' + controlToNotice).removeClass('btn-validation-error');
            return true;
        }
        else {
            $('#' + controlToNotice).addClass('btn-validation-error');
            return false;
        }
    }

    function pushNewReleasesToR2() {

        // validations for push to R2
        $('#divErrorMsgPushToR2').hide();
        $('#divErrorMsgPushToR2').empty();
        $('#divErrorMsgUpcNull').hide();
        $('#divErrorMsgUpcNull').empty();
        $('#divMessagePushToR2').hide();
        $('#divMessagePushToR2').empty();

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $('#loadingDivPA').show();
        $.post('/GCS/ClearanceProject/PushNewReleasesToR2', digitalVal, function (data) {

            var dataParse = data;
            if (dataParse.indexOf("UPC") >= 0) {
                $('#divErrorMsgUpcNull').show();
                $('#divErrorMsgUpcNull').text(upcNullMsgPush);
            }
            if (dataParse.indexOf("FreehandResource") >= 0) {
                $('#divErrorMsgPushToR2').show();
                $('#divErrorMsgPushToR2').text(freeHandResourceMsgPush);
            }

            if (dataParse.indexOf("NoPush") >= 0 && dataParse.indexOf("UPC") < 0 && dataParse.indexOf("FreehandResource") < 0) {
                $('#divErrorMsgPushToR2').show();
                $('#divErrorMsgPushToR2').text(noReleaseResourceFoundPush);
            }
            if (dataParse.indexOf("Success") >= 0) {
                $('#divMessagePushToR2').show();
                $('#divMessagePushToR2').text(R2InProgressPush);
                // disable Remove button against pushed release

                $('.hdnUPCNumberCls').each(function () {

                    var id = $(this).attr('id');
                    var ides = id.replace('hdnUpcNumber', '');
                    var releaseId = $('#hdnReleaseId' + ides).val();

                    if (dataParse.indexOf(releaseId) >= 0)
                        $('#hdnisPushedToR2' + ides).val("Y");

                    var hasValue
                    if ($('#' + id).val() != "" && $('#' + id).val() != 0)
                        hasValue = $('#' + id).val();
                    else
                        hasValue = 0;

                    if ((hasValue != 0 && $('#hdnisPushedToR2' + ides).val() == "N") || ($('#hdnisPushedToR2' + ides).val() == "Y" && $('#hdnManualUpcNumber').val() == "Y")) {

                        $("#lnkRemoveUPCNumber" + ides).removeAttr("disabled");
                        $("#lnkRemoveUPCNumber" + ides).addClass('lnkUPCNumberCss');
                    }
                    else {

                        $("#lnkRemoveUPCNumber" + ides).attr("disabled", true);
                        $("#lnkRemoveUPCNumber" + ides).addClass('lnkUPCNumberCssdisabled');
                    }

                });

            }
            $('#loadingDivPA').hide();

        });
    }

    //  Load partial view
    $('#projToRep').click(function (e) {

        if ($('#hdnSearchForR2Project').val() == "2") {
            // validations for Create New Project
            var IsValid = false;
            // validations check
            IsValid = ValidationForRegularProject();

            if (IsValid == false) {
                return false;
            }
        }

        // suppose user clicks on link button without selecting existing project
        $('#divMessageLinkToR2').hide();
        $('#divMessageLinkToR2').empty();
        $('#divErrorMsgLinkToR2').hide();
        $('#divErrorMsgLinkToR2').empty();
        $('#divMessageR2IdLinked').hide();
        $('#divMessageR2IdLinked').empty();
        if ($('#hdnSearchForR2Project').val() == 1 && $('#searchProjectListPush').val() == "") {
            $('#divErrorMsgLinkToR2').show();
            $('#divErrorMsgLinkToR2').text(R2ProjectNotSelectedPush);
            return;
        }
        // [UC013-0565] Only R2 user has rights to create a link between clearance project and R2 project    


        $('#loadingDivPA').show();
        var artistfreeHandPush = $('#hdnArtistId').text();
        var projectTitlefreeHandPush = $('#projectTitleFreeHand').val();
        var labelIdfreeHandPush = $('#labelIdFreeHand').val();
        var dataAdmincompanyIdfreeHandPush = $('#companyIdFreeHand').val();
        var divisionIdfreeHandPush = $('#divisionIdFreeHand').val();
        $('#projectTitleFreeHandPush').val(projectTitlefreeHandPush);
        $('#companyIdFreeHandPush').val(dataAdmincompanyIdfreeHandPush);
        $('#divisionIdFreeHandPush').val(divisionIdfreeHandPush);
        $('#labelIdFreeHandPush').val(labelIdfreeHandPush);
        $('#hdnArtistIdPush').val(artistfreeHandPush);

        var digitalFormValue = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/LinkingNewReleaseToR2', digitalFormValue, function (data) {
            // set R2_Project_Id to update the model
            var R2ReleaseId = "";
            var R2ReleaseKey = "";

            var R2ProjectMapping = data.split('~');
            if (R2ProjectMapping.length == 2) {
                R2ReleaseId = R2ProjectMapping[0];
                R2ReleaseKey = R2ProjectMapping[1];
            }

            if (R2ReleaseId != null && R2ReleaseId.length != 0 && R2ReleaseId != 0 && R2ReleaseId != "") {

                $('#R2_Project_Code').val(R2ReleaseId);
                $('#R2_Project_Id').val(R2ReleaseKey);
                $('#divMessageR2IdLinked').show();
                $('#divMessageR2IdLinked').text(R2ProjectIdIs + ": " + R2ReleaseId);
                // now close this dialog and start push to R2 functionality
                $('#SearchR2ProjectsDialog').dialog('close');
                pushNewReleasesToR2();
            }
            else {
                // in case project is not linked - some error
                $('#loadingDivPA').hide();
                $('#divErrorMsgLinkToR2').show();
                $('#divErrorMsgLinkToR2').text(R2LinkFailed);
            }

        });

        return false;
    });

    var proplinkingRel = $('<div id="relePopup"></div>')
                      .html('<p>' + messageCommon.onLoading + '</p>')
                      .dialog({
                          autoOpen: false,
                          modal: true,
                          title: messageCommon.infoTitle,
                          show: 'clip',
                          hide: 'clip',
                          width: "98%",
                          position: [(($(window).width() - ($(window).width() * 98) / 100) / 2), 25]
                      });


    $('#searchProject').click(function () {

        $('#spnSearchResult').empty();
        $('#spnNoResults').empty();
        $("#trShowPaging").hide();
        $("#projectgrid").hide();
        $("#freeHand").hide();
        $('#divErrorMsgLinkToR2').hide();
        isSearchClicked = true;
        rowIndex = -1;

        // validations for Create New Project
        var IsValid = false;
        // validations check
        IsValid = ValidateCompanyForProjectSearch();

        if (IsValid == false) {
            return false;
        }

        if ($('#ProjectInfo_ProjectId').val() != "" ||  $('#ProjectInfo_Title').val() != "" || $('#ProjectInfo_Label').val() != "" || $('#ProjectInfo_DataAdminCompany').val() != "") {
            LoadSearchJtable();
        }
        else {
            alert("Please Enter a SearchCritearia");
        }
    });

    $("#createR2Project").click(function (event) {

        // reset collection, being used to search project
        $('#searchProjectListPush').val("");
        $('#searchR2ProjectCode').val("");

        $('#loadingDivPA').show();
        $.post('/GCS/Search/GetAuthorizationsForR2CreatePartialUpdates', function (data) {
            $('#loadingDivPA').hide();

            if (data == false) {
                $('#divErrorMsgLinkToR2').show();
                $('#divErrorMsgLinkToR2').text(authorizedUserMsgPush);
                return;
            }
            else {
                id = $("#freeHand").show();
                $("#companyIdFreeHand").val("");
                $("#divisionIdFreeHand").val("");
                $("#labelIdFreeHand").val("");
                $('#projectgrid').hide();
                $('#hdnSearchForR2Project').val("2");

                $('#SearchR2ProjectsDialog').html(data);
            }
        });

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
                $("#ProjectInfo_DataAdminCompany").val("");
            }
            else if (ui.item != null && $("#ProjectInfo_DataAdminCompany").val() != ui.item.value) {
                $("#ProjectInfo_DataAdminCompany").val("");
            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#ProjectInfo_DataAdminCompany").val(ui.item.value);
                $("#ProjectInfo_AdminCompanyId").val(ui.item.id);
            }

        }
    });

    //ResetButton
    $('#resetProject').click(function (e) {

        e.preventDefault();
        $('#ProjectId').val('');
        $('#Title').val('');
        $('#AdminCompanyName').val('');
        $('#AdminCompanyId').val('');

        $('#DivisionId').empty();
        var mySelect = $('#DivisionId');
        mySelect.append(
                    $('<option></option>').val(0).html("--Select--")
                    );

        $('#LabelId').empty();
        var mySelectLabelId = $('#LabelId');
        mySelectLabelId.append(
                    $('<option></option>').val(0).html("--Select--")
                    );

        $('#ArtistName').val('');
        $('#ArtistID').val('');
    });

    //AutoComplete Label
    var target2 = $("#ProjectInfo_Label");
    target2.autocomplete({
        source: target2.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#ProjectInfo_Label").val(ui.item.value);
            $("#ProjectInfo_LabelId").val(ui.item.id);

        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#ProjectInfo_Label").val("");
            }
            else if (ui.item != null && $("#ProjectInfo_Label").val() != ui.item.value) {
                $("#ProjectInfo_Label").val("");
            }
            else if (ui.item != null && $(ui.item.value != null)) {
                $("#ProjectInfo_Label").val(ui.item.value);
                $("#ProjectInfo_LabelId").val(ui.item.id);
            }

        }
    });
});
