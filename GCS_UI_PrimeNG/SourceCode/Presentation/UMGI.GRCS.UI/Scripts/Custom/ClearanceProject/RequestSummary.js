var _routingStatus;

function getRequestSummaryView() {
    $('#errorMessageDiv').hide();
    var digitalVal = $('#CreateMasterProject').serialize();
    $.post('/GCS/ClearanceProject/GetRequestsummary', digitalVal, function (data) {
        $('#tabs-3').html(data);
    });
}

function newResources() {
    var projectId = $('#hdnclrProjectId').val();
    $('#newResourcesList').jtable({
        selecting: false,
        columnSelectable: false,
        resize: false,
        sorting: true,
        paging: true,
        pageSize: RequestSummaryGridPageSize,
        defaultSorting: 'ResourceTitle ASC',
        actions: {
            listAction: '/GCS/ClearanceProject/GetRequestSummaryMaster?clrProjectId=' + projectId
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
                    case 1:
                    case 2:
                        $('#newResourcesList').hide();
                        $('#divSearchRequestSummaryPaging').hide();
                        $('#errorMessageDiv').show();
                        $('#refreshBtnDiv').css('float', 'left');
                        $('#errorMessageDiv').text(RoutingMessageWhenReachedRouting);
                        break;

                    case 3:
                        $('#errorMessageDiv').hide();
                        if (data.records.length > 0) {
                            $('#refreshBtnDiv').css('float', 'right');
                            if (($('#hdnMasterProjectStatusId').val() == "3") || ($('#hdnMasterProjectStatusId').val() == "4")) {
                                $('#newResourcesList input[type=button]').attr('disabled', 'disabled');
                            }

                            var tableLength = $('.jtable').length;
                            for (i = 0; i < $('.jtable')[0].rows.length; i++) {
                                if ($('.jtable')[0].rows[i] != null) {
                                    if (navigator.appName == "Microsoft Internet Explorer") {
                                        if ($('.jtable')[0].rows[i].children[11].innerText == "NONUMG") {
                                            $('.jtable')[0].rows[i].className = 'HighlightedRow';
                                        }
                                    }
                                    else {
                                        if ($('.jtable')[0].rows[i].children[11].textContent == "NONUMG") {
                                            $('.jtable')[0].rows[i].className = 'HighlightedRow';
                                        }
                                    }
                                }
                            }

                            $('#newResourcesList').show();
                            $('#divSearchRequestSummaryPaging').show();
                            _routingStatus = 3;
                            var RequestListCount = $('#hdnRSResourceCount').val();
                            for (i = 0; i < data.records.length; i++) {
                                for (k = 0; k < RequestListCount; k++) {
                                    if ($('#hdnRSRequestlId' + k).val() == data.records[i].RequestId) {
                                        $('#hdnRSApprovalStatusId' + k).val(data.records[i].ApprovalStatusId);
                                    }
                                }
                            }
                        }
                        break;

                    case 4:
                        $('#newResourcesList').hide();
                        $('#divSearchRequestSummaryPaging').hide();
                        $('#errorMessageDiv').text(RoutingMessageWhenErrorIsInRouting);
                        $('#errorMessageDiv').show();
                        break;

                    default:
                        break;
                }
            }
            else {
                $('#divSearchRequestSummaryPaging').hide();
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
                list: false
            },
            ClearanceProjectId: {
                title: '   ',
                width: '1%',
                sorting: false,
                listClass: 'imgBorder',
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
                        else if (project.record.ApprovalStatus == 'Cancelled') {
                            var disptext = null;
                        }
                        else if (project.record.ApprovalStatus == 'Excluded') {
                            var disptext = null;
                        }

                        return disptext;
                    }
            },
            ResourceTitle: {
                title: 'Resource Title',
                width: '8%',
                sorting: true,
                display:
                      function (project) {
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
                sorting: true,
                width: '8%'
            },
            AdminCompany: {
                title: 'Clearance Owner',
                sorting: true,
                width: '7%'
            },
            RequestType: {
                title: 'Request Type',
                width: '7%'
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
                width: '8%',
                sorting: true,
                display:
                function (Project) {
                    var ProjId = Project.record.ClearanceProjectId;
                    var ReqstId = Project.record.RequestId;
                    var RoutedItemId = Project.record.RoutedItemId;
                    var projReqId = ProjId + '~' + ReqstId;
                    var projReqId = projReqId + '~' + RoutedItemId;
                    var comments = Project.record.Comment;
                    var noOfComments = Project.record.CommentCount;
                    var tempDispText = comments;

                    if (comments.length > 25) {
                        var actualValue = comments;
                        var substringDisplayText = comments.substring(0, 25) + '...';
                        tempDispText = '<span title ="' + actualValue + '" >' + substringDisplayText + '</span>';
                    }
                    var displayText = tempDispText + '<br/><label style="cursor:pointer; color:#143489" title="' + noOfComments + ' comment(s) available" OnClick="return routingInfo(' + "'" + projReqId + "'" + ')">RoutingDetails</label>'
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
                display:
                function (Project) {
                    if (isDisable != "Y") {
                        var ProjId = Project.record.ClearanceProjectId;
                        var ReqstId = Project.record.RequestId;
                        var RoutedItemId = Project.record.RoutedItemId;
                        var WorkgroupId = Project.record.WorkgroupId;
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
                        var projReqId = ProjId + '~' + ReqstId + '~' + RoutedItemId + '~' + WorkgroupId + '~' + RoutedItemDate + '~' + requestDate;
                        if (Project.record.ApprovalStatus == 'Waiting') {
                            var act = 0;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button"  class="plbutton" style="vertical-align:middle;" value="Cancel"/>').click(function (e) {
                                actionOnRequest(projReqId);
                            });
                            var displayText1 = '<br/><br/>';
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
                        else if (Project.record.ApprovalStatus == 'Approved') {
                            var act = 3;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton"  value="Exclude"/>').click(function (e) {
                                actionOnRequest(projReqId);
                            });
                            return displayText;
                        }
                        else if (Project.record.ApprovalStatus == 'Conditionally Approved') {
                            var act = 4;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton"  value="Exclude"/>').click(function (e) {
                                actionOnRequest(projReqId);
                            });
                            return displayText;
                        }
                        else if (Project.record.ApprovalStatus == 'Excluded') {
                            var act = 5;
                            projReqId = projReqId + '~' + act;
                            var displayText = $('<input type="button" class="plbutton"  value="Include"/>').click(function (e) {
                                actionOnRequest(projReqId);
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
    var objReapplyPopup = $('<div id="ReapplyComment"></div>')
            .html('<br/><input id="CommentTextArea" type="textarea" style="height:60px;width:97%;float:right"/>')
            .dialog({
                autoOpen: false,
                resizable: false,
                modal: true,
                show: 'clip',
                hide: 'clip',
                width: "28%",
                position: [(window.mouseXPos - 50), window.mouseYPos + 10],
                open: function (event) {
                    $('.ui-dialog-buttonset').find('button:contains("Cancel")').addClass('reqSumCancelButton');
                },
                close: function () {
                    $(this).remove();
                }
            });
    objReapplyPopup.dialog('option', { title: "Re-Apply" });
    objReapplyPopup.dialog({
        buttons: {
            Cancel: function () {
                $(this).dialog('close');
            },
            'Submit': function () {
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
    });
    objReapplyPopup.dialog('open');
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
                    position: [(windowWidth / 4), windowHeight / 4],
                    open: function (event, ui) {
                        $('#routingDetailsMainDiv').css('overflow', 'auto');
                    },
                    close: function () {
                        $(this).remove();
                    }
                });

    routingDetailsPopup.html('<p>Loading...</p>');
    routingDetailsPopup.load(

            $.ajax({
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
                contentType: 'text/html; charset=utf-8'
            }));

    routingDetailsPopup
        .dialog('option', { title: "RoutingDetails" })
        .dialog('open');
    return false;
}

function actionOnRequest(projReqId) {
    $('#loadingDivPA').show();
    var Beginslash = "\"\\/";
    var EndSlash = "\\/\"";
    var substr = projReqId.split('~');
    var ClearanceProjectId = substr[0];
    var RequestId = substr[1];
    var RoutedItemId = substr[2];
    var WorkGroupId = substr[3];
    var Action = substr[6];
    var ModifiedDatetime = Beginslash + substr[4] + EndSlash;
    var ModifiedDatetimeRequest = Beginslash + substr[5] + EndSlash;
    var comments = "";
    if (substr[7] != null) {
        comments = substr[7];
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
            $('#refreshBtn').click();
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
                                'Ok':
                                    function (e) {
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
    var ClearanceProjectId = substr[0];
    var RequestId = substr[1];
    var RoutedItemId = substr[2];
    var WorkGroupId = substr[3];
    var Action = 6;
    var Beginslash = "\"\\/";
    var EndSlash = "\\/\"";
    var ModifiedDatetime = Beginslash + substr[4] + EndSlash;
    var ModifiedDatetimeRequest = Beginslash + substr[5] + EndSlash;
    var comments = "";
    if (substr[7] != null) {
        comments = substr[7];
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

function SearchRequestSummaryPagingChange() {
    RequestSummaryGridPageSize = $('#ddlSearchRequestSummaryPaging').val();
    newResources();
}

$(document).ready(function () {
    $('#ddlSearchRequestSummaryPaging').val(RequestSummaryGridPageSize);

    $(document).mousemove(function (e) {
        window.mouseXPos = e.pageX;
        window.mouseYPos = e.pageY;
    });

    $('#refreshBtn').click(getRequestSummaryView);

    $('#ddlSearchRequestSummaryPaging').change(SearchRequestSummaryPagingChange);
});