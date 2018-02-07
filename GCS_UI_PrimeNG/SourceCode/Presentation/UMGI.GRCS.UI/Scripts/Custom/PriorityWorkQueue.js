/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var priorityWorkQueueMessage = {
    TaskId: "ID/UPC/ISRC", Title: "Description/Title", VersionTitle: "Version Title", Confirm: "Confirm", Clearance: 'Clearance/Admin Company',
    ArtistName: "Artist", ReleaseDate: "Release Date", ContractReviewReason: "Reason for Review", LinkCriteria: 'Please select anyone record',
    CompanyName: "Clearance/Data Admin Co", LinkedContractName: "Linked Contract", PYear: "P-Year", ReleaseUnLinkTitle: 'Release Unlink',
    OwnedReleases: "Owned Releases", OwnedResources: "Owned Resources", RerouteTitle: "Re-route", SearchContractTitle: "Search for Contract",
    LinkEntryCriteria: "Please select only one Record to Link ", Type: "Type",
    RerouteEntryCriteria: "Please select one Resource to Re-Route", errorMessage: "Not applicable to Re-route link",
    ReviewEntry: "Please select anyone record ", FilterEntryCriteria: "Please specify atleast one filter criteria", confirmChange: 'Are you sure you want to change Link',
    RemoveCriteria: "Please Select atleast one Row to Remove ", ConfirmDeletion: "Do you want to delete the selected rows from the system?",
    RightsReviewSuccess: "Selected WorkQueue Items are moved to Rights Review WorkQueue Successfully.", ReviewFail: "Selected WorkQueue Item is not moved to Rights Review WorkQueue",
    RemoveSuccess: "Record is Removed Succesfully", RemoveFail: "Record is not Removed", RightsReviewEntry: "Please select anyone record",
    UnlinkTitle: "Confirm Unlink", UnlinkCriteria: "Please select anyone record ", UnlinkFailed: "Unlinking Failed",
    confirmUnlink: "Are you sure you want to unlink?",
    UnlinkNotApplicable: "Not applicable to Unlink", ChangeLinkNotApplicable: "Not applicable to Change link"
};

var isChangeLink = false;

var objReRouteDialog = $('<div id="ReRouteResources"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: '',
            show: 'clip',
            hide: 'clip',
            width: 710,
            resizable: true,
            height: 200,
            position: [(($(document).width() - 700) / 2), 50]
        });

var objClearanceDialog = $('<div id="ClearanceAdminDialog"></div>')
.html('<p>' + messageCommon.onLoading + '</p>')
.dialog({
    autoOpen: false,
    modal: true,
    title: '',
    show: 'clip',
    hide: 'clip',
    width: '55%',
    resizable: true,
    position: [(($(window).width() - (($(window).width() * 55) / 100)) / 2), 15]
});
var objWorkQueueDialog = $('<div id="WorkQueuee"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: '',
            show: 'clip',
            hide: 'clip',
            width: "98%",
            resizable: true,
            position: [(($(window).width() - (($(window).width() * 98) / 100)) / 2), 15]
        });
var objDialog = $('<div id="WorkQueueMessg"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.warningTitle,
            show: 'clip',
            hide: 'clip',
            width: 500
        });

var objReleaseUnlinkPro = $('<div id="ReleaseUnlinkPro"></div>')
.html('<p>' + messageCommon.onLoading + '</p>')
.dialog({
    autoOpen: false,
    modal: true,
    title: '',
    show: 'clip',
    hide: 'clip',
    width: 710,
    resizable: true,
    position: [(($(document).width() - 700) / 2), 50]
});

$(document).ready(function () {
    $('#DefaultCollapsed').ready(function () {
        $('#accordion .head').next().toggle();
        $($('#accordion').find('a')[0]).toggleClass('iconBottom');
    });
    // $('#ChangeLink').attr('disabled', 'disabled');
    // $('#UnLinkContract').attr('disabled', 'disabled');
    // $('#LinkContract').attr('disabled', 'disabled');
    $('#ReviewRight').attr('disabled', 'disabled');
    $('#ReRoute').attr('disabled', 'disabled');

    reSizePriorityWorkQueuePage();
    $('#clrfilter').hide();

    //Clearance Company link
    //Load partial view
    objClearanceDialog.load('/GCS/Global/ClearanceAdminCompany', "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                objClearanceDialog.html('<p>' + messageCommon.error + '</p>');
            }
        });

    $('#ClearanceAdminPopUp').click(function (e) {
        e.preventDefault();
        objClearanceDialog.dialog('option', { title: priorityWorkQueueMessage.Clearance });
        //Open Dialog
        objClearanceDialog.dialog('open');
    });

    //Clear Filter Click
    $('#clrfilter').click(function (e) {
        e.preventDefault(); //clrfilter
        $('#FilterApp').hide();
        $('#clrfilter').hide();
        $('#ArtistName').val('');
        $('#ContractDescription').val('');
        $('#Titles').val('');
        $('#AdminCompany').val('');
        $('#AdminCompanyNames').val('');

        $('#WorkQueues_ArtistName').val('142');
        $('#WorkQueues_Title').val('142');
        $('#WorkQueues_ContractDescription').val('142');
        $('#WorkQueues_ContractReviewReason').val('0');

        $('#accordion').next().toggle();
        $($('#accordion').find('a')[0]).toggleClass('iconBottom');
        $('#workQueueGrid').jtable('load', {
            artistName: $('#ArtistName').val(),
            contractDesc: $('#ContractDescription').val(),
            descTitle: $('#Titles').val(),
            reasonForReview: $('#WorkQueues_ContractReviewReason option:selected').text(),
            adminCompany: $('#AdminCompany').val()
        });
    });

    //Filter Button Click
    $('#filterWorkQueue').click(function (e) {
        reSizePriorityWorkQueuePage();
        e.preventDefault();
        if (checkFilterEntry()) {
            ShowWarning(priorityWorkQueueMessage.FilterEntryCriteria);

            return false;
        }
        else {
            $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
            $('#accordion .head').next().toggle();
            $($('#accordion').find('a')[0]).toggleClass('iconBottom');
            $('#clrfilter').show();
            $('#FilterApp').show();
            HideWarningSuccess();
        }
        return false;
    });

    //Reset button click
    $('#resetWorkQueue').click(function (e) {
        e.preventDefault();
        HideWarningSuccess();
        $('#ArtistName').val(''),
       $('#ContractDescription').val(''),
        $('#Titles').val('');
        $('#AdminCompany').val('');
        $('#AdminCompanyNames').val('');
        $('#WorkQueues_ArtistName').val('142');
        $('#WorkQueues_Title').val('142');
        $('#WorkQueues_ContractDescription').val('142');
        $('#WorkQueues_ContractReviewReason').val('0'); //WorkQueues_ArtistName
    });

    //Accordion style collapse/expand
    $('#accordion .head').click(function (e) {
        HideWarningSuccess();
        e.preventDefault();
        $(this).next().toggle();
        $($('#accordion').find('a')[0]).toggleClass('iconBottom');

        return false;
    }).next().show();

    //RefreshWorkQueue
    $('#refreshWorkQueue').click(function (e) {
        e.preventDefault();
        window.location.href = "/GCS/WorkQueue/ContractLinkingWorkQueue/";
    });

    //DropDown select
    $('#WorkQueueSelect select').change(function () {
        HideWarningSuccess();
        pageSize = $('#WorkQueueSelect select').val();
        $('#workQueueGrid').jtable({ 'pageSize': pageSize });
        $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
    });

    //Review Rights click
    $('#ReviewRight').click(function (e) {
        e.preventDefault();
        var $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        if ($selectedRows.length == 0) {
            ShowWarning(priorityWorkQueueMessage.RightsReviewEntry);

            return false;
        }
        else if ($selectedRows.length > 0) {
            var workQueueIds = '';
            $selectedRows.each(function () {
                var record = $(this).data('record');
                workQueueIds = workQueueIds + ',' + record.GrcsRepertoireId;
            });

            $.post('/GCS/WorkQueue/RightsReview/', { workQueueIds: workQueueIds },
                    function (data) {
                        if ($(data).find(".removeDisplayMessage").val() == "success") {
                            ShowSuccess(priorityWorkQueueMessage.RightsReviewSuccess);

                            $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
                        }
                        else {
                            ShowWarning(priorityWorkQueueMessage.ReviewFail);

                            $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
                        }
                    });
        }
        return false;
    });

    //Re-Route Resource click
    $('#ReRoute').click(function (e) {
        e.preventDefault();

        var $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        var resourceType = "";
        var reason = "";
        var workQueueIds = '';

        $selectedRows.each(function () {
            var record = $(this).data('record');
            resourceType = record.ResourceType;
            reason = record.ContractReviewReasonId;
            workQueueIds = workQueueIds + ',' + record.GrcsRepertoireId;
        });

        if ($selectedRows.length == 0 || $selectedRows.length > 1) {
            ShowWarning(priorityWorkQueueMessage.RerouteEntryCriteria);

            return false;
        }
        else if (resourceType == "Resource")              // Linked to Clearence Routing Contract - 8 (resourceType == "Resource" && reason == 1)
        {
            HideWarningSuccess();
            //                    $('#warning').hide();
            objReRouteDialog.html('<p>' + messageCommon.onLoading + '</p>');
            objReRouteDialog.load('/GCS/WorkQueue/LoadRerouteResource/',
                    function (responseText, textStatus) {
                        if (textStatus == 'error') {
                            objReRouteDialog.html('<p>' + messageCommon.error + '</p>');
                        }
                    });

            objReRouteDialog.dialog('option', { title: priorityWorkQueueMessage.RerouteTitle });
            //Open Dialog
            objReRouteDialog.dialog('open');
        }
            //                else if (resourceType == "Resource" && (reason == 150 || reason == 152)) //Auto linked during Clearence Request or Manually linked errorMessage
            //                {
            //                    Todo
            //                }
        else {
            //ShowWarning(priorityWorkQueueMessage.errorMessage);
            $(this).find('#alert').attr('title', priorityWorkQueueMessage.errorMessage);
            $(this).find('#alert').show();
            $(this).removeClass("jtable-row-selected");
            $(this).find('input:checkbox').removeAttr("checked");
        }
        return false;
    });

    //Remove click
    $('#Remove').click(function (e) {
        e.preventDefault();
        var $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        if ($selectedRows.length == 0) {
            ShowWarning(priorityWorkQueueMessage.RemoveCriteria);

            return false;
        }
        else {
            objDialog.dialog({ title: priorityWorkQueueMessage.Confirm });
            objDialog.html(priorityWorkQueueMessage.ConfirmDeletion);
            objDialog.dialog('open');
            objDialog.dialog({
                buttons:
        {
            'Yes': function () {
                e.preventDefault();
                if ($selectedRows.length > 0) {
                    var workQueueItems = [];
                    //  Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        workQueueItems.push(record);
                    });
                }
                $(this).dialog('close');
                $.get('/GCS/WorkQueue/RemoveWorkQueue/', { workQueueItems: JSON.stringify(workQueueItems) },
                    function (data) {
                        if ($(data).find(".removeDisplayMessage").val() == "success") {
                            ShowSuccess(priorityWorkQueueMessage.RemoveSuccess);

                            $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
                        } else {
                            ShowWarning(priorityWorkQueueMessage.RemoveFail);
                            //
                            $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
                        }
                    });
            }, 'Cancel': function () {
                $(this).dialog('close');
                $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
            }
        }
            });
        }
        return false;
    });

    reSizePriorityWorkQueuePage();
    //Resize
    $(window).resize(function () {
        reSizePriorityWorkQueuePage();
    });

    // Link Contract click
    $('#LinkContract').click(function (e) {
        e.preventDefault();
        isChangeLink = false;

        var $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        if ($selectedRows.length == 0) {
            ShowWarning(priorityWorkQueueMessage.LinkCriteria);

            return false;
        } else {
            var projCount = 0;
            var workQueueType;
            //  Show selected rows
            $selectedRows.each(function () {
                var record = $(this).data('record');
                workQueueType = record.ResourceType;
                if (workQueueType == "Project") {
                    projCount++;
                    if (projCount > 1)
                        return false;
                }
            });
            if (projCount > 1) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    workQueueType = record.ResourceType;
                    if (workQueueType == "Project") {
                        $(this).find('#alert').show();
                        $(this).removeClass("jtable-row-selected");
                        $(this).find('input:checkbox').removeAttr("checked");
                        $('#success').hide();
                        ShowWarning(priorityWorkQueueMessage.LinkEntryCriteria);
                    }
                });
            }
            else {
                $("#workQueueGrid tr image#alert").hide();
                objWorkQueueDialog.html('<p>' + messageCommon.onLoading + '</p>');
                objWorkQueueDialog.load('/GCS/Contract/ContractSearchPopup/', "",
           function (responseText, textStatus) {
               if (textStatus == 'error') {
                   objWorkQueueDialog.html('<p>' + messageCommon.error + '</p>');
               }
           });

                objWorkQueueDialog.dialog('option', { title: priorityWorkQueueMessage.SearchContractTitle });
                //Open Dialog
                objWorkQueueDialog.dialog('open');
            }
        }
        return false;
    });

    var rowIndex = -1;
    var pageSize = 25;
    $('#workQueueGrid').jtable
    ({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        defaultSorting: 'Type ASC',
        selecting: true,
        multiselect: true,
        selectingCheckboxes: true,
        selectOnRowClick: true,
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
        },
        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.TotalRecordCount;

            document.getElementById("PriorWorkQueueCount").innerHTML = "WorkQueue Filter (" + rowIndex + ")";
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            $("#workQueueGrid input").removeAttr("checked");
            $("#workQueueGrid tr").removeClass("jtable-row-selected");

            setToolTip(this);
        },
        actions: {
            listAction: '/GCS/WorkQueue/WorkQueueFilter'
        },
        fields: {
            'ReType': {
                title: priorityWorkQueueMessage.Type,
                display: function (test) {
                    var image = "";
                    if (test.record.ResourceType == "Project")
                        image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img src="/GCS/Images/project.-2png.png" title="Project" >');
                    else if (test.record.ResourceType == "Release")
                        image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/Release.gif" title="Release">');
                    else if (test.record.ResourceType == "Resource") {
                        if (test.record.RepertoireSubType == "Audio") {
                            image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/resource_group.gif" title="Resource">' + '<img  src="/GCS/Images/ResourceAudio.gif" title="Audio">');
                        }
                        else if (test.record.RepertoireSubType == "Video") {
                            image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/resource_group.gif" title="Resource">' + '<img  src="/GCS/Images/ResourceVideo.gif" title="Video">');
                        }
                        else if (test.record.RepertoireSubType == "Image") {
                            image = $('<img id="alert" style="display:none;" src="/GCS/Images/Error_16x16.png" title=" Alert">' + '<img  src="/GCS/Images/resource_group.gif" title="Resource">' + '<img  src="/GCS/Images/ResourceImage.gif" title="Image">');
                        }
                    }
                    return image;
                },
                sorting: false,
                listClass: 'workQAlertImages'
            },

            Type: {
                title: priorityWorkQueueMessage.TaskId,
                width: '100px'
            },
            Title: {
                title: priorityWorkQueueMessage.Title
            },
            VersionTitle: {
                title: priorityWorkQueueMessage.VersionTitle
            },
            ArtistName: {
                title: priorityWorkQueueMessage.ArtistName
            },
            ReleaseDate: {
                title: priorityWorkQueueMessage.ReleaseDate,
                type: 'date',
                displayFormat: 'dd M yy'
            },
            CommencementDate: {
                type: 'date',
                displayFormat: 'dd M yy',
                list: false
            },

            ContractReviewReason: {
                title: priorityWorkQueueMessage.ContractReviewReason
            },
            CompanyName: {
                title: priorityWorkQueueMessage.CompanyName
            },

            LinkedContractName: {
                title: priorityWorkQueueMessage.LinkedContractName,
                display: function (test) {
                    var image = "";

                    if (test.record.ContractId != null) {
                        image = $('<img  src="/Gcs/Images/linked_Contract.png" >');
                        var dateString;
                        try {
                            var re = /-?\d+/;
                            var m = re.exec(test.record.CommencementDate);
                            var date = new Date(parseInt(m[0]));
                            var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                            dateString = date.getDate().toString() + ' ' + monthArray[date.getMonth("mmm")] + ' ' + date.getFullYear().toString();
                        } catch (e) {
                            dateString = "NA";
                        }

                        if (test.record.ContractArtistName == '' || test.record.ContractArtistName == undefined) {
                            $(image).prop("title", "-" + "Artist/Contracting Party:<b>" + test.record.ContractingParty + "</b><br>CommencementDate:<b>" + dateString + '</b>');
                        } else {
                            $(image).prop("title", "-" + "Artist/Contracting Party:<b>" + test.record.ContractArtistName + "</b><br>CommencementDate:<b>" + dateString + '</b>');
                        }
                        $(image).tooltip({ showURL: false, showBody: "-" });
                    } else {
                        image = '';
                    }

                    $(image).click(function () {
                        window.location.href = "/GCS/Contract/EditContract/" + test.record.ContractId;
                    });

                    return image;
                }
            },
            PYear: {
                title: priorityWorkQueueMessage.PYear
            },
            OwnedReleases: {
                title: priorityWorkQueueMessage.OwnedReleases
            },
            OwnedResources: {
                title: priorityWorkQueueMessage.OwnedResources
            }
        },
        //Register to selectionChanged event to hanlde events
        selectionChanged: function () {
            HideWarningSuccess();
            //Get all selected rows
            var selectedRows = $('#workQueueGrid').jtable('selectedRows'); // $('#workQueueGrid').jtable('selectedRows');
            if (selectedRows.length > 0) {
                selectedRows.each(function () {
                    var record = $(this).data('record');

                    $('#hiddenContractId').val(record.ContractId);
                });
            }
        }
    });
    $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
    // Unlink Contract click
    $('#UnLinkContract').click(function (e) {
        e.preventDefault();
        var contrctId = '';
        var $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        if ($selectedRows.length == 0) {
            ShowWarning(priorityWorkQueueMessage.UnlinkCriteria);

            return false;
        }
        else {
            var selItems = [];
            $selectedRows.each(function () {
                var record = $(this).data('record');
                var workQueueType = record.ResourceType;
                contrctId = record.ContractId;

                if (workQueueType == "Project") {
                    $(this).find('#alert').attr('title', priorityWorkQueueMessage.UnlinkNotApplicable);
                    $(this).find('#alert').show();
                    $(this).removeClass("jtable-row-selected");
                    $(this).find('input:checkbox').removeAttr("checked");
                }
                if (contrctId == null) {
                    $(this).find('#alert').attr('title', priorityWorkQueueMessage.UnlinkNotApplicable);
                    $(this).find('#alert').show();
                    $(this).removeClass("jtable-row-selected");
                    $(this).find('input:checkbox').removeAttr("checked");
                }
                //                        if (record.ContractReviewReason == 'Contract linking required') {
                //                            $(this).find('#alert').attr('title', priorityWorkQueueMessage.UnlinkNotApplicable);
                //                            $(this).find('#alert').show();
                //                            $(this).removeClass("jtable-row-selected");
                //                            $(this).find('input:checkbox').removeAttr("checked");
                //                        }

                if (workQueueType != "Project" && record.ContractReviewReason != 'Contract linking required') {
                    selItems.push(record);
                }
            });

            if (selItems.length > 0 && contrctId != null) {
                objDialog.dialog({ title: priorityWorkQueueMessage.Confirm });
                objDialog.html(priorityWorkQueueMessage.confirmUnlink);
                objDialog.dialog('open');
                objDialog.dialog({
                    buttons: {
                        'Yes': function () {
                            $('#loadingDiv').show();

                            $.post("/GCS/WorkQueue/UnlinkContract", { workQueueItem: JSON.stringify(selItems), id: $('#hiddenContractId').val() })
                                .success(function (data) {
                                    if ($(data).find("#hiddenCount").val() == 'Resource') {
                                        objReleaseUnlinkPro.html(data);
                                        objReleaseUnlinkPro.dialog({ title: priorityWorkQueueMessage.ReleaseUnLinkTitle });
                                        objReleaseUnlinkPro.dialog('open');
                                    }
                                    $('#WorkQueuee').dialog('close');
                                    $('#loadingDiv').hide();
                                    $('#workQueueGrid').jtable('load', { param: JSON.stringify(getFilterParameters()) });
                                })
                                .error(function (data) {
                                    $('#loadingDiv').hide();
                                });

                            $(this).dialog('close');
                        },
                        'No': function () {
                            $(this).dialog('close');
                        }
                    }
                });
            }

            //$("#workQueueGrid tr image#alert").hide();
        }
    });

    // Change Contract click
    $('#ChangeLink').click(function (e) {
        e.preventDefault();

        isChangeLink = true;

        var $selectedRows = $('#workQueueGrid').jtable('selectedRows');
        if ($selectedRows.length == 0) {
            ShowWarning(priorityWorkQueueMessage.LinkCriteria);

            return false;
        }
        else {
            var selItems = [];
            var contrctIds = '';

            $selectedRows.each(function () {
                var record = $(this).data('record');
                var workQueueType = record.ResourceType;
                contrctIds = record.ContractId;
                if (contrctIds == null) {
                    $(this).find('#alert').attr('title', priorityWorkQueueMessage.ChangeLinkNotApplicable);
                    $(this).find('#alert').show();
                    $(this).removeClass("jtable-row-selected");
                    $(this).find('input:checkbox').removeAttr("checked");
                }

                if (workQueueType == "Project") {
                    $(this).find('#alert').attr('title', priorityWorkQueueMessage.ChangeLinkNotApplicable);
                    $(this).find('#alert').show();
                    $(this).removeClass("jtable-row-selected");
                    $(this).find('input:checkbox').removeAttr("checked");
                }
                if (record.ContractReviewReason == 'Contract linking required') {
                    $(this).find('#alert').attr('title', priorityWorkQueueMessage.ChangeLinkNotApplicable);
                    $(this).find('#alert').show();
                    $(this).removeClass("jtable-row-selected");
                    $(this).find('input:checkbox').removeAttr("checked");
                }

                if (workQueueType != "Project" && record.ContractReviewReason != 'Contract linking required') {
                    selItems.push(record);
                }
            });

            if (selItems.length > 0 && contrctIds != null) {
                isChangeLink = true;

                objDialog.dialog({ title: priorityWorkQueueMessage.Confirm });
                objDialog.html(priorityWorkQueueMessage.confirmChange);
                objDialog.dialog('open');
                objDialog.dialog({
                    buttons: {
                        'Yes': function () {
                            objWorkQueueDialog.html('<p>' + messageCommon.onLoading + '</p>');
                            objWorkQueueDialog.load('/GCS/Contract/ContractSearchPopup/', "",
                           function (responseText, textStatus) {
                               if (textStatus == 'error') {
                                   objWorkQueueDialog.html('<p>' + messageCommon.error + '</p>');
                               }
                           });

                            objWorkQueueDialog.dialog('option', { title: priorityWorkQueueMessage.SearchContractTitle });
                            //Open Dialog
                            objWorkQueueDialog.dialog('open');
                            $(this).dialog('close');
                        },
                        'No': function () {
                            $(this).dialog('close');
                        }
                    }
                });
            }
        }
    });
});
// reSizePriorityWorkQueuePage();
//Resize page
function reSizePriorityWorkQueuePage() {
    var h = $(window).height();
    if ($('#success').css("display") == 'none' && $('#warning').css("display") == 'none')
        $(".scrollWorkQueue").css('height', h - 185);
    else
        $(".scrollWorkQueue").css('height', h - 225);
}

//Get filter parameters
function getFilterParameters() {
    var artistName = $('#ArtistName').val();
    var contractDesc = $('#ContractDescription').val();
    var descTitle = $('#Titles').val();
    var reasonForReviewFilterText = $('#WorkQueues_ContractReviewReason option:selected').text();
    var artistFilterText = $('#WorkQueues_ArtistName option:selected').text();
    var contractDescFilterText = $('#WorkQueues_ContractDescription option:selected').text();
    var titleFilterText = $('#WorkQueues_Title option:selected').text();
    var adminCompany = $('#AdminCompany').val();

    var param = {
        param: {
            artistName: artistName, artistFilterText: artistFilterText, contractDesc: contractDesc,
            contractDescFilterText: contractDescFilterText, descTitle: descTitle, titleFilterText: titleFilterText,
            reasonForReview: reasonForReviewFilterText, adminCompany: adminCompany
        }
    };

    return param;
}

//Check if any filter criteria is entered
function checkFilterEntry() {
    var artistName = $('#ArtistName').val();
    var contractDesc = $('#ContractDescription').val();
    var descTitle = $('#Titles').val();
    var adminCompany = $('#AdminCompany').val();
    var reasonForReviewFilterText = $('#WorkQueues_ContractReviewReason option:selected').text();
    return (artistName == "" || artistName == null) && (contractDesc == "" || contractDesc == null) && (descTitle == "" || descTitle == null) && (reasonForReviewFilterText == "" || reasonForReviewFilterText == "-- Select --" || reasonForReviewFilterText == null) && (adminCompany == "" || adminCompany == null);
}