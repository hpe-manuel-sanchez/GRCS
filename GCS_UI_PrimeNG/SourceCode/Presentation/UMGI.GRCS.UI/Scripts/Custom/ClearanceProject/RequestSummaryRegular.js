var _routingStatus;
var newResourcesCount = 0;
var existingTracksCount = 0;
var isResourceGridLoaded = false;
var isTracksGridLoaded = false;
var _RequestCount = $('#hdnRequestSummaryListCount').val();
var _projectStatus = "";
var _PushToR2NewReleaseExist = "";
var _RCCAdminAllocateUPC = "";
var _btnUPCAllocate = "";

function getRequestSummaryView() {
    var digitalVal = $('#CreateRegularProjectForm').serialize();
    $.post('/GCS/ClearanceProject/GetRegularRequestsummary', digitalVal, function (data) {
        $('#tabs-5').html(data);
    });
}

function newResources() {
    var projectId = $('#Clr_Project_Id').val();
    $('#newResourcesList').jtable({
        selecting: false,
        columnSelectable: false,
        sorting: true,
        paging: true,
        pageSize: RequestSummaryNewGridPageSize,
        defaultSorting: 'RequestId ASC',
        actions: {
            listAction: '/GCS/ClearanceProject/GetNewResources?clrProjectId=' + projectId
        },
        loadingRecords: function () {
            if (_routingStatus != '3') {
                $('#loaderMsg').show();
            }
            $('#errorMessageDiv').hide();
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {
            $('#loaderMsg').hide();
            if (data.serverResponse.TotalRecordCount > 0) {
                switch (data.serverResponse.RoutingStatus) {
                    case 0:
                    case 1:
                    case 2:
                        $('#newResourcesList').hide();
                        $('#divSearchRequestSummaryNewPaging').hide();
                        $('#errorMessageDiv').show();
                        $('#refreshBtnDiv').css('float', 'left');
                        $('#errorMessageDiv').text(RoutingMessageWhenReachedRouting);
                        break;

                    case 3:
                        $('#errorMessageDiv').hide();
                        isResourceGridLoaded = true;
                        if (data.records.length > 0) {
                            newResourcesCount = data.records.length;
                            $('#refreshBtnDiv').css('float', 'right');
                            if (($('#hdnRegularProjectStatusId').val() == "3") || ($('#hdnRegularProjectStatusId').val() == "4")) {
                                $('#newResourcesList input[type=button]').attr('disabled', 'disabled');
                            }

                            var tableLength = $('.jtable').length;
                            for (i = 0; i < $('.jtable')[1].rows.length; i++) {
                                if ($('.jtable')[1].rows[i] != null) {
                                    if (navigator.appName == "Microsoft Internet Explorer") {
                                        if ($('.jtable')[1].rows[i].children[11].innerText == "NONUMG") {
                                            $('.jtable')[1].rows[i].className = 'HighlightedRow';
                                        }
                                    }
                                    else {
                                        if ($('.jtable')[1].rows[i].children[11].textContent == "NONUMG") {
                                            $('.jtable')[1].rows[i].className = 'HighlightedRow';
                                        }
                                    }
                                }
                            }
                            _routingStatus = 3;
                            $('#newResourcesList').show();
                            $('#divSearchRequestSummaryNewPaging').show();
                            var RequestListCount = $('#hdnRSResourceCount').val();
                            for (i = 0; i < data.records.length; i++) {
                                for (k = 0; k < RequestListCount; k++) {
                                    if ($('#hdnRSRequestlId' + k).val() == data.records[i].RequestId) {
                                        $('#hdnRequestInfoRequestStatusId' + k).val(data.records[i].ApprovalStatusId);
                                    }
                                }
                            }
                        }
                        else if (isTracksGridLoaded) {
                            if (parseInt(existingTracksCount) == 0) {
                                $('#hdnProjWithNoResources').val("true");
                                $('#errorMessageDiv').text(RoutingMessageWhenReleaseWithoutTracksSubmitted);
                                $('#errorMessageDiv').show();
                                $('#refreshBtnDiv').hide();
                            }
                        }
                        break;

                    case 4:
                        $('#newResourcesList').hide();
                        $('#divSearchRequestSummaryNewPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenErrorIsInRouting);
                        $('#errorMessageDiv').show();
                        break;

                    case 5:
                        $('#newResourcesList').hide();
                        $('#divSearchRequestSummaryNewPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenCancellationIsInProgress);
                        $('#errorMessageDiv').show();
                        break;

                    case 6:
                        $('#newResourcesList').hide();
                        $('#divSearchRequestSummaryNewPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenCompletionIsInProgress);
                        $('#errorMessageDiv').show();
                        break;

                    case 7:
                        $('#newResourcesList').hide();
                        $('#divSearchRequestSummaryNewPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenReinstateIsInProgress);
                        $('#errorMessageDiv').show();
                        break;

                    default:
                        break;
                }
            }
            else {
                $('#newResourcesList').hide();
                $('#divSearchRequestSummaryNewPaging').hide();
                $('#errorMessageDiv').show();
                $('#refreshBtnDiv').css('float', 'left');
                $('#errorMessageDiv').text(RoutingMessageWhenReleaseNotReturnResources);
            }
        },
        fields: {
            ClearanceProjectId: {
                title: 'Project Id',
                list: false
            },
            RequestId: {
                title: 'Resource Title',
                sorting: true,
                list: false
            },
            ClearanceProjectId: {
                title: '   ',
                width: '2%',
                listClass: 'imgBorder',
                sorting: false,
                display:
                    function (project) {
                        if (project.record.ApprovalStatus == 'Approved') {
                            var disptext = $('<img src="/GCS/Images/ClearanceInbox/approved.png" title="Approved"/>');
                        }
                        else if (project.record.ApprovalStatus == 'Conditionally Approved') {
                            var disptext = $('<img src="/GCS/Images/ClearanceInbox/conditionallyApproved.png" title="Conditionally Approved"/>');
                        }
                        else if (project.record.ApprovalStatus == 'Rejected') {
                            var disptext = $('<img src="/GCS/Images/ClearanceInbox/rejected.png" title="Rejected"/>');
                        }
                        else if (project.record.ApprovalStatus == 'Routing Stopped') {
                            var disptext = $('<img src="/GCS/Images/ClearanceInbox/routingStopped.png" title="Routing Stopped"/>');
                        }
                        else {
                            var disptext = null;
                        }
                        return disptext;
                    }
            },
            ResourceTitle: {
                title: 'Resource Title',
                width: '9%',
                sorting: true,
                display: function (project) {
                    var disptext = project.record.PrimaryArtistName + ': ' + project.record.ResourceTitle + ', ' + project.record.VersionTitle
                    if (disptext.length > 25) {
                        var actualValue = disptext;
                        actualValue = actualValue.toString().replace(/"/g, "'");
                        var substringDisplayText = disptext.substring(0, 25) + '...';
                        disptext = $('<span title ="' + actualValue + '" >' + substringDisplayText + '</span>');
                    }
                    return disptext;
                }
            },
            Isrc: {
                title: 'ISRC',
                width: '6%',
                sorting: true,
                key: true
            },
            ResourceType: {
                title: 'Resource Type',
                width: '5%',
                sorting: true
            },
            AdminCompany: {
                title: 'Clearance Owner',
                sorting: true,
                width: '7%'
            },
            RequestType: {
                title: 'Request Type',
                sorting: true,
                width: '8%'
            },
            CreatedDate: {
                title: 'Submission Date',
                width: '7%',
                sorting: true,
                type: 'date',
                displayFormat: 'dd-M-yy'
            },
            RoleName: {
                title: 'Reviewer Role',
                sorting: true,
                width: '8%'
            },
            WorkgroupName: {
                title: 'Reviewer Workgroup',
                sorting: true,
                width: '8%'
            },
            ApprovalStatus: {
                title: 'Review Status',
                sorting: true,
                width: '6%'
            },
            ReviewDate: {
                title: 'Review Date',
                width: '8%',
                sorting: true,
                type: 'date',
                displayFormat: 'dd-M-yy'
            },
            RightsType_Desc: {
                title: 'RightsType_Desc',
                visibility: 'hidden'
            },
            Comment: {
                title: 'Comment',
                sorting: true,
                width: '8%',
                display: function (Project) {
                    var ProjId = Project.record.ClearanceProjectId;
                    var ReqstId = Project.record.RequestId;
                    var RoutedItemId = Project.record.RoutedItemId;
                    var projReqId = ProjId + '~' + ReqstId + '~' + RoutedItemId;
                    var comments = Project.record.Comment;
                    var noOfComments = Project.record.CommentCount;

                    var tempDispText = comments;
                    if (comments.length > 25) {
                        var actualValue = comments;
                        var substringDisplayText = comments.substring(0, 25) + '...';
                        tempDispText = '<span title ="' + actualValue + '" >' + substringDisplayText + '</span>';
                    }
                    var displayText = tempDispText + '<br/><label style="cursor:pointer; color:#143489" title="' + noOfComments + ' comment(s) available" OnClick="return routingInfo(' + "'" + projReqId + "'" + ')">RoutingDetails</label>';
                    return displayText;
                }
            },
            ModifiedDateRequestString: {
                title: 'Requested Date',
                width: '8%',
                list: false
            },
            ModifiedDateRoutedString: {
                title: 'Routed Item Date',
                width: '8%',
                list: false
            },
            ActionsRequired: {
                title: '  ',
                width: '3%',
                sorting: false,
                listClass: 'btnVertAlign',
                display: function (Project) {
                    if (isDisable != "Y") {
                        var ProjId = Project.record.ClearanceProjectId;
                        var ReqstId = Project.record.RequestId;
                        var RoutedItemId = Project.record.RoutedItemId;
                        var WorkgroupId = Project.record.WorkgroupId;
                        var TableId = 0;
                        $('#hdnActionRequestId').val(Project.record.RequestId);
                        var Beginslash = "\"\\/";
                        var EndSlash = "\\/\"";
                        var RoutedItemDate;
                        var requestDate;
                        if (Project.record.ModifiedDateRoutedString != null) {
                            RoutedItemDate = Project.record.ModifiedDateRoutedString.toString().replace(Beginslash, "");
                            RoutedItemDate = RoutedItemDate.toString().replace(EndSlash, "");
                        }
                        if (Project.record.ModifiedDateRequestString != null) {
                            requestDate = Project.record.ModifiedDateRequestString.toString().replace(Beginslash, "");
                            requestDate = requestDate.toString().replace(EndSlash, "");
                        }
                        var projReqId = TableId + '~' + ProjId + '~' + ReqstId + '~' + RoutedItemId + '~' + WorkgroupId + '~' + RoutedItemDate + '~' + requestDate;
                        if (Project.record.ApprovalStatus == 'Waiting') {
                            var act = 0;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton" style="vertical-align:middle;" value="Cancel"/>').click(function (e) {
                                actionOnRequest(projReqId);
                            });
                            var displayText1 = '<br/>';
                            var displayText2 = $('<input type="button" class="plbutton" style="vertical-align:middle;" value="Remind"/>').click(function (e) {
                                remindRequest(projReqId);
                            });
                            return displayText.add(displayText1).add(displayText2);
                        }
                        else if (Project.record.ApprovalStatus == 'Cancelled') {
                            var act = 2;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton"  value="Re-Instate"/>').click(function (e) {
                                actionOnRequest(projReqId);
                            });
                            return displayText;
                        }
                        else if (Project.record.ApprovalStatus == 'Rejected') {
                            var act = 1;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton"  value="Re-Apply"/>').click(function (e) {
                                openReapplyCommentDialog(projReqId);
                            });
                            return displayText;
                        }
                    }
                    else {
                        return '';
                    }
                }
            }
        }
    });
    $('#newResourcesList').jtable('load');
}

function openReapplyCommentDialog(projReqId) {
    $('<div id="ReapplyComment"></div>')
       .html('<br/><input id="CommentTextArea" type="textarea" style="height:50px;width:97%;"/>')
       .dialog({
           autoOpen: false,
           resizable: false,
           modal: true,
           show: 'clip',
           hide: 'clip',
           width: "28%",
           position: [(window.mouseXPos - 50), window.mouseYPos + 10],
           option: {
               title: "Re-Apply"
           },
           open: function (event) {
               $('.ui-dialog-buttonset').find('button:contains("Cancel")').addClass('reqSumCancelButton');
           },
           close: function () {
               $(this).remove();
           },
           buttons: {
               Cancel: function () { $(this).dialog('close'); }, 'Submit': function () {
                   var cmnt = $("#CommentTextArea").val(); projReqId = projReqId + '~' + cmnt;
                   if (cmnt != "") {
                       $(this).dialog('close');
                       actionOnRequest(projReqId);
                       $('#CommentTextArea').removeClass('input-validation-error');
                   }
                   else {
                       $('#CommentTextArea').addClass('input-validation-error');
                   }
               }
           }
       })
       .dialog('open');
}

function loadExisting() {
    var projectId = $('#Clr_Project_Id').val();
    $('#existingResourcesList').jtable({
        selecting: true,
        columnSelectable: false,
        sorting: true,
        paging: true,
        pageSize: RequestSummaryExistingGridPageSize,
        defaultSorting: 'RequestId ASC',
        actions: {
            listAction: '/GCS/ClearanceProject/GetExistingTracks?clrProjectId=' + projectId
        },
        loadingRecords: function () {
            $('#errorMessageDiv').hide();
            if (_routingStatus != '3') {
                $('#loaderMsg').show();
            }
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {
            $('#loaderMsg').hide();
            if (data.serverResponse.TotalRecordCount > 0) {
                switch (data.serverResponse.RoutingStatus) {
                    case 0:
                        $('#existingResourcesList').hide();
                        $('#divSearchRequestSummaryExistingPaging').hide();
                        $('#errorMessageDiv').show();
                        $('#refreshBtnDiv').css('float', 'left');
                        $('#errorMessageDiv').text(RoutingMessageWhenReachedRouting);
                        break;

                    case 1:
                        $('#existingResourcesList').hide();
                        $('#divSearchRequestSummaryExistingPaging').hide();
                        $('#errorMessageDiv').show();
                        $('#refreshBtnDiv').css('float', 'left');
                        $('#errorMessageDiv').text(RoutingMessageWhenReachedRouting);
                        break;

                    case 2:
                        $('#existingResourcesList').hide();
                        $('#divSearchRequestSummaryExistingPaging').hide();
                        $('#errorMessageDiv').show();
                        $('#refreshBtnDiv').css('float', 'left');
                        $('#errorMessageDiv').text(RoutingMessageWhenReachedRouting);
                        break;

                    case 3:
                        $('#errorMessageDiv').hide();
                        isTracksGridLoaded = true;
                        if (data.records.length > 0) {
                            existingTracksCount = data.records.length;
                            $('#refreshBtnDiv').css('float', 'right');
                            if (($('#hdnRegularProjectStatusId').val() == "3") || ($('#hdnRegularProjectStatusId').val() == "4")) {
                                $('#existingResourcesList input[type=button]').attr('disabled', 'disabled');
                            }
                            _routingStatus = 3;
                            $('#existingResourcesList').show();
                            $('#divSearchRequestSummaryExistingPaging').show();
                            var RequestListCount = $('#hdnRSResourceCount').val();
                            for (i = 0; i < data.records.length; i++) {
                                for (k = 0; k < RequestListCount; k++) {
                                    if ($('#hdnRSRequestlId' + k).val() == data.records[i].RequestId) {
                                        $('#hdnRequestInfoRequestStatusId' + k).val(data.records[i].ApprovalStatusId);
                                    }
                                }
                            }
                        }
                        else if ($("#FlagReleaseNewOrExisting").val() != 'New') {
                            $('#hdnProjWithNoResources').val("true");
                            $('#errorMessageDiv').text(RoutingMessageWhenReleaseWithoutTracksSubmitted);
                            $('#errorMessageDiv').show();
                            $('#refreshBtnDiv').hide();
                        }
                        else if (isResourceGridLoaded) {
                            if (parseInt(newResourcesCount) == 0) {
                                $('#hdnProjWithNoResources').val("true");
                                $('#errorMessageDiv').text(RoutingMessageWhenReleaseWithoutTracksSubmitted);
                                $('#errorMessageDiv').show();
                                $('#refreshBtnDiv').hide();
                            }
                        }
                        break;

                    case 4:
                        $('#existingResourcesList').hide();
                        $('#divSearchRequestSummaryExistingPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenErrorIsInRouting);
                        $('#errorMessageDiv').show();
                        break;

                    case 5:
                        $('#existingResourcesList').hide();
                        $('#divSearchRequestSummaryExistingPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenCancellationIsInProgress);
                        $('#errorMessageDiv').show();
                        break;

                    case 6:
                        $('#existingResourcesList').hide();
                        $('#divSearchRequestSummaryExistingPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenCompletionIsInProgress);
                        $('#errorMessageDiv').show();
                        break;

                    case 7:
                        $('#existingResourcesList').hide();
                        $('#divSearchRequestSummaryExistingPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenReinstateIsInProgress);
                        $('#errorMessageDiv').show();
                        break;

                    default:
                        break;
                }
            }
            else {
                $('#existingResourcesList').hide();
                $('#divSearchRequestSummaryExistingPaging').hide();
                $('#errorMessageDiv').show();
                $('#refreshBtnDiv').css('float', 'left');
                $('#errorMessageDiv').text(RoutingMessageWhenReleaseNotReturnResources);
            }
        },
        fields: {
            ClearanceProjectId: {
                title: 'Project Id',
                width: '5%',
                list: false
            },
            RequestId: {
                title: 'Resource Title',
                width: '5%',
                sorting: true,
                list: false
            },

            ClearanceProjectId: {
                title: '   ',
                width: '2%',
                sorting: false,
                listClass: 'imgBorder',
                display: function (project) {
                    if (project.record.ApprovalStatus == 'Approved') {
                        var disptext = $('<img src="/GCS/Images/ClearanceInbox/approved.png" title="Approved"/>');
                    }
                    else if (project.record.ApprovalStatus == 'Conditionally Approved') {
                        var disptext = $('<img src="/GCS/Images/ClearanceInbox/conditionallyApproved.png" title="Conditionally Approved"/>');
                    }
                    else if (project.record.ApprovalStatus == 'Rejected') {
                        var disptext = $('<img src="/GCS/Images/ClearanceInbox/rejected.png" title="Rejected"/>');
                    }
                    else if (project.record.ApprovalStatus == 'Routing Stopped') {
                        var disptext = $('<img src="/GCS/Images/ClearanceInbox/routingStopped.png" title="Routing Stopped"/>');
                    }
                    else {
                        var disptext = null;
                    }
                    return disptext;
                }
            },
            Upc: {
                title: 'UPC',
                width: '4%',
                sorting: true,
                key: true
            },
            ResourceTitle: {
                title: 'Resource Title',
                sorting: true,
                width: '9%',
                display: function (project) {
                    var disptext = project.record.PrimaryArtistName + ': ' + project.record.ResourceTitle + ', ' + project.record.VersionTitle
                    if (disptext.length > 25) {
                        var actualValue = disptext;
                        actualValue = actualValue.toString().replace(/"/g, "'");
                        var substringDisplayText = disptext.substring(0, 25) + '...';
                        disptext = $('<span title ="' + actualValue + '" >' + substringDisplayText + '</span>');
                    }
                    return disptext;
                }
            },
            Isrc: {
                title: 'ISRC',
                sorting: true,
                width: '6%',
                key: true
            },
            ResourceType: {
                title: 'Resource Type',
                sorting: true,
                width: '5%'
            },
            AdminCompany: {
                title: 'Clearance Owner',
                sorting: true,
                width: '7%'
            },
            RequestType: {
                title: 'Request Type',
                sorting: true,
                width: '8%'
            },

            Configuration: {
                title: 'Configuration',
                sorting: true,
                width: '4%'
            },

            CreatedDate: {
                title: 'Submission Date',
                width: '7%',
                sorting: true,
                type: 'date',
                displayFormat: 'dd-M-yy'
            },
            RoleName: {
                title: 'Reviewer Role',
                sorting: true,
                width: '8%'
            },
            WorkgroupName: {
                title: 'Reviewer Workgroup',
                sorting: true,
                width: '8%'
            },
            ApprovalStatus: {
                title: 'Review Status',
                sorting: true,
                width: '6%'
            },
            ReviewDate: {
                title: 'Review Date',
                width: '8%',
                sorting: true,
                type: 'date',
                displayFormat: 'dd-M-yy'
            },
            Comment: {
                title: 'Comment',
                sorting: true,
                width: '8%',
                display: function (Project) {
                    var ProjId = Project.record.ClearanceProjectId;
                    var ReqstId = Project.record.RequestId;
                    var RoutedItemId = Project.record.RoutedItemId;
                    var projReqId = ProjId + '~' + ReqstId + '~' + RoutedItemId + '~'; // +ResourceTit;
                    var comments = Project.record.Comment;
                    var noOfComments = Project.record.CommentCount;

                    var tempDispText = comments;
                    if (comments.length > 25) {
                        var actualValue = comments;
                        var substringDisplayText = comments.substring(0, 25) + '...';
                        tempDispText = '<span title ="' + actualValue + '" >' + substringDisplayText + '</span>';
                    }
                    var displayText = tempDispText + '<br/><label style="cursor:pointer; color:#143489" title="' + noOfComments + ' comment(s) available" OnClick="return routingInfo(' + "'" + projReqId + "'" + ')">RoutingDetails</label>';
                    return displayText;
                }
            },

            ModifiedDateRequestString: {
                title: 'Requested Date',
                width: '8%',
                list: false
            },
            ModifiedDateRoutedString: {
                title: 'Routed Item Date',
                width: '8%',
                list: false
            },
            ActionsRequired: {
                title: '  ',
                width: '5%',
                listClass: 'btnVertAlign',
                sorting: false,
                display: function (Project) {
                    if (isDisable != "Y") {
                        var ProjId = Project.record.ClearanceProjectId;
                        var ReqstId = Project.record.RequestId;
                        var RoutedItemId = Project.record.RoutedItemId;
                        var WorkgroupId = Project.record.WorkgroupId;
                        var TableId = 1;

                        var Beginslash = "\"\\/";
                        var EndSlash = "\\/\"";
                        var RoutedItemDate = Project.record.ModifiedDateRoutedString.toString().replace(Beginslash, "");
                        RoutedItemDate = RoutedItemDate.toString().replace(EndSlash, "");
                        var requestDate = Project.record.ModifiedDateRequestString.toString().replace(Beginslash, "");
                        requestDate = requestDate.toString().replace(EndSlash, "");

                        $('#hdnActionRequestId').val(Project.record.RequestId);
                        var projReqId = TableId + '~' + ProjId + '~' + ReqstId + '~' + RoutedItemId + '~' + WorkgroupId + '~' + RoutedItemDate + '~' + requestDate;
                        if (Project.record.ApprovalStatus == 'Waiting') {
                            var act = 6;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton" value="Remind"/>').click(function (e) {
                                remindRequest(projReqId);
                            });
                            return displayText;
                        }
                        else if (Project.record.ApprovalStatus == 'Rejected') {
                            var act = 1;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton"  value="Re-Apply"/>').click(function (e) {
                                openReapplyCommentDialog(projReqId);
                            });
                            return displayText;
                        }
                    }
                    else {
                        return '';
                    }
                }
            }
        }
    });
    $('#existingResourcesList').jtable('load');
}

function routingInfo(proj) {
    var substr = proj.split('~');
    var ClearanceProjectId = substr[0];
    var RequestId = substr[1];
    var RoutedItemID = substr[2];
    var windowWidth = $(document).width;
    var windowHeight = $(document).height;
    var routingDetailsPopup = $('<div id="routingDetailsMainDiv"></div>')
        .html('<p>Loading...</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            show: 'clip',
            hide: 'clip',
            width: "75%",
            height: 380,
            position: [windowWidth / 4, windowHeight / 4],
            option: {
                title: 'RoutingDetails'
            },
            close: function () {
                $(this).remove();
            }
        });
    routingDetailsPopup
        .load($.ajax({
            url: '/GCS/ClearanceProject/RoutingDetailsOnRequestSummary?routedItemID=' + RoutedItemID,
            type: 'POST',
            dataType: 'html',
            async: true,
            data: [],
            cache: false,
            success: function (data) {
                if (data == 'error') {
                    routingDetailsPopup.html('<p>Error</p>');
                }
                else {
                    routingDetailsPopup.html(data);
                }
            },
            contentType: 'application/json; charset=utf-8'
        }));
    routingDetailsPopup.dialog('open');
    return false;
}

function actionOnRequest(projReqId) {
    $('#loadingDivPA').show();
    var Beginslash = "\"\\/";
    var EndSlash = "\\/\"";
    var substr = projReqId.split('~');
    var TableId = substr[0]
    var ClearanceProjectId = substr[1];
    var RequestId = substr[2];
    var RoutedItemId = substr[3];
    var WorkGroupId = substr[4];
    var Action = substr[7];
    var ModifiedDatetime = Beginslash + substr[5] + EndSlash;
    var ModifiedDatetimeRequest = Beginslash + substr[6] + EndSlash;
    var comments = "";
    if (substr[8] != null) {
        comments = substr[8];
    }

    var requesterWorkgroupId = $('#hdnrequesterWorkgroupId').val()

    $.ajax({
        url: '/GCS/ClearanceProject/ActionOnRequest',
        type: 'POST',
        data: {
            RequesterWorkgroupId: requesterWorkgroupId,
            ClearanceProjectId: ClearanceProjectId,
            RequestId: RequestId,
            RoutedItemId: RoutedItemId,
            WorkgroupId: WorkGroupId,
            SequenceNo: Action,
            Comment: comments,
            ModifiedDateRequestString: ModifiedDatetimeRequest,
            ModifiedDateRoutedString: ModifiedDatetime
        },
        dataType: 'json',
        async: 'async',
        success: function (data) {
            $('#loadingDivPA').hide();

            if (TableId == 0) {
                newResources();
            }
            else if (TableId == 1) {
                loadExisting();
            }
        },
        error: function (data) {
            $('#loadingDivPA').hide();

            if (data.Message != null && data.Message != 'undefined') {
                if (data.Message.toString().contains("Concurrency Exception :")) {
                    var objWarningDialog = $('<div id="Confirm"></div>')
                        .html('<p><b> ' + data.Message + ' </b></p>')
                        .dialog({
                            autoOpen: false,
                            modal: true,
                            show: 'clip',
                            hide: 'clip',
                            width: 500,
                            height: 200,
                            buttons: {
                                'Ok': function (e) {
                                    $(this).dialog('close');
                                }
                            }
                        })
                        .dialog('open');
                }
            }

            if (data.responseText != null && data.responseText != 'undefined') {
                var responseTextCheck = data.responseText;
                if (responseTextCheck.indexOf("Concurrency Exception :") >= 0) {
                    var objWarningDialog = $('<div id="Confirm"></div>')
                        .html('<p><b> ' + data.responseText + ' </b></p>')
                        .dialog({
                            autoOpen: false,
                            modal: true,
                            show: 'clip',
                            hide: 'clip',
                            width: 500,
                            height: 200,
                            buttons: {
                                'Ok': function (e) {
                                    $(this).dialog('close');
                                }
                            }
                        })
                        .dialog('open');
                }
            }
            return false;
        }
    });
}

function remindRequest(projReqId) {
    $('#loadingDivPA').show();
    var substr = projReqId.split('~');
    var TableId = substr[0]
    var ClearanceProjectId = substr[1];
    var RequestId = substr[2];
    var RoutedItemId = substr[3];
    var WorkGroupId = substr[4];
    var Action = 6;
    var Beginslash = "\"\\/";
    var EndSlash = "\\/\"";
    var ModifiedDatetime = Beginslash + substr[5] + EndSlash;
    var ModifiedDatetimeRequest = Beginslash + substr[6] + EndSlash;
    var comments = "";
    if (substr[8] != null) {
        comments = substr[8];
    }

    var requesterWorkgroupId = $('#hdnrequesterWorkgroupId').val()

    $.ajax({
        url: '/GCS/ClearanceProject/ActionOnRequest',
        type: 'POST',
        data: {
            RequesterWorkgroupId: requesterWorkgroupId,
            ClearanceProjectId: ClearanceProjectId,
            RequestId: RequestId,
            RoutedItemId: RoutedItemId,
            WorkgroupId: WorkGroupId,
            SequenceNo: Action,
            Comment: comments,
            ModifiedDateRequestString: ModifiedDatetimeRequest,
            ModifiedDateRoutedString: ModifiedDatetime
        },
        dataType: 'json',
        async: 'async',
        success: function (data) {
            $('#loadingDivPA').hide();
        },
        error: function (data) {
            $('#loadingDivPA').hide();

            if (data.Message != null && data.Message != 'undefined') {
                if (data.Message.toString().contains("Concurrency Exception :")) {
                    var objWarningDialog = $('<div id="Confirm"></div>')
                        .html('<p><b> ' + data.Message + ' </b></p>')
                        .dialog({
                            autoOpen: false,
                            modal: true,
                            show: 'clip',
                            hide: 'clip',
                            width: 500,
                            height: 200,
                            buttons: {
                                'Ok': function (e) {
                                    $(this).dialog('close');
                                }
                            }
                        })
                        .dialog('open');
                }
            }

            if (data.responseText != null && data.responseText != 'undefined') {
                var responseTextCheck = data.responseText;
                if (responseTextCheck.indexOf("Concurrency Exception :") >= 0) {
                    var objWarningDialog = $('<div id="Confirm"></div>')
                        .html('<p><b> ' + data.responseText + ' </b></p>')
                        .dialog({
                            autoOpen: false,
                            modal: true,
                            show: 'clip',
                            hide: 'clip',
                            width: 500,
                            height: 200,
                            buttons: {
                                'Ok': function (e) {
                                    $(this).dialog('close');
                                }
                            }
                        })
                        .dialog('open');
                }
            }
            return false;
        }
    });
}

function LoadExistingReleaseTabSynchronously() {
    var popup = window.opener;
    if (popup) {
        var _roleGroup = popup._activeRoleGroup
    }

    $('<div class="ajaxLoader"/>').show().appendTo('#tabs-3');
    $.ajax({
        async: true,
        url: '/GCS/ClearanceProject/GetExistingReleasesOfRegularProject',
        type: 'POST',
        data: {
            clrprojectId: $('#Clr_Project_Id').val(),
            NeworExisting: $('#FlagReleaseNewOrExisting').val(),
            ActiveRoleGroup: _roleGroup
        },
        success: function (data) {
            if (data.Error) {
                $('#divErrorMessage').html(data.Message);
                $('#divErrorMessage').show();
                $('#divErrorMessage').addClass('error msg-margin');
                $('#tabs-3').empty();
                return false;
            }
            $('#btnReinstate').prop('disabled', false);
            $('#btnReOpen').prop('disabled', false);
            $('#btnSubmit').prop('disabled', false);
            $('#btnSave').prop('disabled', false);
            $('#btnCancelProject').prop('disabled', false);
            $('#btnComplete').prop('disabled', false);
            $('#tabs-3').html(data);
        },
        error: function () {
            $('#tabs-3').empty();
        }
    });
}

function LoadReleaseNewTabSynchronously() {
    var roleGroup = 'Reviewer';
    if ($('#hdnRoleGroup').val() != undefined) {
        roleGroup = $('#hdnRoleGroup').val();
    }

    $('<div class="ajaxLoader"/>').show().appendTo('#tabs-3');

    $.ajax({
        async: true,
        url: '/GCS/ClearanceProject/GetReleasesOfRegularProject',
        type: 'POST',
        data: {
            clrprojectId: $('#Clr_Project_Id').val(),
            NeworExisting: $('#FlagReleaseNewOrExisting').val(),
            projectStatus: $('#hdnStatusType').val(),
            requestWorkgroupId: $('#ddlRequestingComp').val(),
            roleGroup: roleGroup,
            multiArtist: $("#chkMultiArtist").is(':checked')
        },
        success: function (data) {
            if (data.Error) {
                $('#divErrorMessage').html(data.Message);
                $('#divErrorMessage').show();
                $('#divErrorMessage').addClass('error msg-margin');
                $('#tabs-3').empty();
                return false;
            }
            $('#tabs-3').html(data);
            $('#IsReleaseTabLoaded').val('True');

            if ($('#IsResourceTabLoaded').val() == 'True') {

                $('#btnReinstate').prop('disabled', false);
                $('#btnReOpen').prop('disabled', false);
                $('#btnSubmit').prop('disabled', false);
                $('#btnSave').prop('disabled', false);
                $('#btnCancelProject').prop('disabled', false);
                $('#btnComplete').prop('disabled', false);

                var digitalVal = $('#CreateRegularProjectForm').serialize();
                $.post('/GCS/ClearanceProject/EnableDisblePushToR2Btn', digitalVal, function (data) {
                    var enableBtn = data;
                    if (enableBtn == "True" && $("#hdnStatusType").val() != 1) {
                        $('#btnPushToR2').show();
                    }
                    else {
                        $('#btnPushToR2').hide();
                    }
                });
            }
        },
        error: function () {
            $('#tabs-3').empty();
        }
    });
}

function LoadResourceRegularTabSynchronously() {

    var popup = window.opener;
    if (popup) {
        var _roleGroup = popup._activeRoleGroup
    }
    $('<div class="ajaxLoader"/>').show().appendTo('#tabs-4');

    $.ajax({
        async: true,
        url: '/GCS/ClearanceProject/GetResourcesOfRegularProject',
        type: 'POST',
        data: {
            clrprojectId: $('#Clr_Project_Id').val(),
            NeworExisting: $('#FlagReleaseNewOrExisting').val(),
            ActiveRoleGroup: _roleGroup
        },
        success: function (data) {
            if (data.Error) {
                $('#divErrorMessage').html(data.Message);
                $('#divErrorMessage').show();
                $('#divErrorMessage').addClass('error msg-margin');
                $('#tabs-4').empty();
                return false;
            }
            $('#tabs-4').html(data);
            ParentCall();
            $('#IsResourceTabLoaded').val('True');

            if ($('#IsReleaseTabLoaded').val() == 'True') {

                $('#btnReinstate').prop('disabled', false);
                $('#btnReOpen').prop('disabled', false);
                $('#btnSubmit').prop('disabled', false);
                $('#btnSave').prop('disabled', false);
                $('#btnCancelProject').prop('disabled', false);
                $('#btnComplete').prop('disabled', false);

                var digitalVal = $('#CreateRegularProjectForm').serialize();
                $.post('/GCS/ClearanceProject/EnableDisblePushToR2Btn', digitalVal, function (data) {
                    var enableBtn = data;
                    if (enableBtn == "True" && $("#hdnStatusType").val() != 1) {
                        $('#btnPushToR2').show();
                    }
                    else {
                        $('#btnPushToR2').hide();
                    }
                });
            }
        },
        error: function () {
            $('#tabs-4').empty();
        }
    });
}

function refreshButtonClick() {
    newResourcesCount = 0;
    existingTracksCount = 0;
    isResourceGridLoaded = false;
    isTracksGridLoaded = false;

    $('#errorMessageDiv').text("");

    if ($("#FlagReleaseNewOrExisting").val() == 'New' && $('#hdnRegularProjectStatusId').val() != 1) {
        newResources();
    }
    else if ($("#FlagReleaseNewOrExisting").val() == 'Exist' && $('#hdnRegularProjectStatusId').val() != 1) {
        loadExisting();
    }
}

$(document).ready(function () {    
    if (LoadTemplate != "1") {
        if ($('#FlagReleaseNewOrExisting').val() == "New") {
            LoadReleaseNewTabSynchronously();
            LoadResourceRegularTabSynchronously();
        } else if ($('#FlagReleaseNewOrExisting').val() == "Exist") {
            LoadExistingReleaseTabSynchronously();
        }
    }
    if ($("#FlagReleaseNewOrExisting").val() == 'New' && $('#hdnRegularProjectStatusId').val() != 1) {
        newResources();
    }
    else if ($("#FlagReleaseNewOrExisting").val() == 'Exist' && $('#hdnRegularProjectStatusId').val() != 1) {
        loadExisting();
    }

    $('#ddlSearchRequestSummaryNewPaging').val(RequestSummaryNewGridPageSize);

    $('#ddlSearchRequestSummaryExistingPaging').val(RequestSummaryExistingGridPageSize);

    $(document).mousemove(function (e) {
        window.mouseXPos = e.pageX;
        window.mouseYPos = e.pageY;
    });

    $('#refreshBtn').click(refreshButtonClick);

    $('#ddlSearchRequestSummaryNewPaging').change(function () {
        RequestSummaryNewGridPageSize = $('#ddlSearchRequestSummaryNewPaging').val();
        newResources();
    });

    $('#ddlSearchRequestSummaryExistingPaging').change(function () {
        RequestSummaryExistingGridPageSize = $('#ddlSearchRequestSummaryExistingPaging').val();
        loadExisting();
    });
});