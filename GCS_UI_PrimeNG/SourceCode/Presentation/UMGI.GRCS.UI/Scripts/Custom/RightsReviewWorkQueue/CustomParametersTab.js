//Initialized Variable for Messages from Resource file
var customParametersTabMessages;

//--------------------Js Page for the UC-19 (Resource/Tracks)------------------

$(document).ready(function () {
    customParametersTabMessages = window.rightsReviewWorkQueueMessages;
    $('#ISRC').css('width', "74%");
    $('#RepertoireFilter_UPC').css('width', "74%");
    //TO show the Button Onload of ResoursePopup
    $('#btnCreateRightsWorkQResource').show();
    $('#btnCancelCreateRightsWorkQResource').show();

    //To Create Search Contract Tab On Click of the image
    $('#SearchReviewRightsContract').unbind('click');
    $('#SearchReviewRightsContract').click(function () {
        //TO show the hidden tab
        $('#liSearchContractTab').show();
        $('#contractSearchPopup').show();
        //To Select The tab on click
        $("#customSettingTabReview").tabs('option', 'selected');
        $("#customSettingTabReview").tabs('select', '#contractSearchPopup');
        //To Refresh 
        $("#customSettingTabReview").tabs().tabs("refresh");

    });

    //Click Function to create the New Tab
    $("#btnCreateRightsWorkQResource").unbind("click").click(function () {
        $("#buttonHide").show();
        $('#contractSearchPopup').dialog('close');
        var tabName = "Custom WorkQueue";

        //Region Adding Tab Dynamically

        var tabTitle = $("#tab_title");
        var tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close tabClose'>Remove Tab</span></li>"; //,
        var tabs = $("#divRightsPriorityWorkQueueTab").tabs();
        var id = "workQueueTabs-" + $.tab.totalTabCounter,
            label = tabTitle.val() || tabName,
            li = $(tabTemplate.replace(/#\{href\}/g, "#" + id).replace(/#\{label\}/g, label));

        tabs.find(".uitabWork").append(li);


        // ReSharper disable UsageOfPossiblyUnassignedValue
        tabs.append("<div id='" + id + "'><p>" + customParametersTabMessages.retrieving + "<p></p></p></div>");
        // ReSharper restore UsageOfPossiblyUnassignedValue
        var pageId = $.tab.totalTabCounter;
        $.get('/GCS/RightsReviewWorkQueue/ResourceRightsWorkQueue/?pageId=' + pageId, function (data) {
            $('#' + id).html(data);
        });

        tabs.tabs("refresh");
        $.tab.tabCounter = $.tab.tabCounter + 1;
        $.tab.totalTabCounter = $.tab.totalTabCounter + 1;
        $.tab.customSettings.push({ id: $.tab.tabCounter, tab: id });
        $("#divRightsPriorityWorkQueueTab").tabs("select", id);

        //End Region Adding

        
        return false;
    });

    //Click function to cancel.
    $("#btnCancelCreateRightsWorkQResource").unbind("click").click(function () {
        $("#buttonHide").show();
        $('#divRightsCustomSettingPopUp').dialog('close');
        return false;
    });

});
