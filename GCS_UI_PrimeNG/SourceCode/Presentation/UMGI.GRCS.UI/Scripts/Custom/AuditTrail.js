//#region - Global Variables

var AuditObjectType =
{
    ContractAuditHistory: 'ContractAuditHistory',
    ReleaseRightsAuditHistory: 'ReleaseRightsAuditHistory',
    ResourceAndTracksRightsAuditHistory: 'ResourceAndTracksRightsAuditHistory',
    CompanyDivisionLabelMappingWithContract: 'CompanyDivisionLabelMappingWithContract',
    WorkgroupAuditHistory: 'WorkgroupAuditHistory',
    ManageSafeTerritoriesAuditHistory: 'ManageSafeTerritoriesAuditHistory',
    RoutingVariationAuditHistory: 'RoutingVariationAuditHistory',
    MasterProjectProjectAuditHistory: 'MasterProjectProjectAuditHistory',
    MasterProjectResourceAuditHistory: 'MasterProjectResourceAuditHistory',
    MasterProjectResourceFreehandAuditHistory: 'MasterProjectResourceFreehandAuditHistory',
    RegularNonTraditionalProjectProjectAuditHistory: 'RegularNonTraditionalProjectProjectAuditHistory',
    RegularNonTraditionalProjectRequestTypeAuditHistory: 'RegularNonTraditionalProjectRequestTypeAuditHistory',
    RegularNonTraditionalProjectReleaseExistsAuditHistory: 'RegularNonTraditionalProjectReleaseExistsAuditHistory',
    RegularNonTraditionalProjectReleaseNewAuditHistory: 'RegularNonTraditionalProjectReleaseNewAuditHistory',
    RegularNonTraditionalProjectResourceAuditHistory: 'RegularNonTraditionalProjectResourceAuditHistory',
    RegularNonTraditionalProjectResourceFreehandAuditHistory: 'RegularNonTraditionalProjectResourceFreehandAuditHistory'    
};

var url_GetAuditTrailFilters = '/GCS/Global/GetAuditTrailFilters';
var url_GetAuditTrail = '/GCS/Global/GetAuditTrail';
$('.ui-dialog-titlebar-close').attr("title", "Close");
var _auditObjectType = null;

var _selectedItemId = null;

var _displayTitle = null;

//#endregion - Global Variables

//#region - Layout

function performLayout() {

    //--
    var originalWindow = $(window);

    //--
    var windowHeight = originalWindow.height();
    var windowWidth = originalWindow.width();

    //--
    var container = $('.container');
    var headerRightPanel = $('.headerRightPanel');
    var leftPanel = $('.leftPanel');
    var rightPanel = $('.rightPanel');
    var header = $('.header');
    var contentHeight = windowHeight - header.height();
    var contentWidth = windowWidth - (leftPanel.width()) - 1;

    //--
    container.css({ 'height': windowHeight, 'width': windowWidth });
    headerRightPanel.css({ 'width': contentWidth });
    leftPanel.css({ 'height': contentHeight });
    rightPanel.css({ 'height': contentHeight-15, 'width': contentWidth });

}

//#endregion - Layout

//#region - Left Panel

function displayAuditTrail(auditObjectType, selectedItemId, displayTitle) {

    //--    
    _auditObjectType = auditObjectType;

    _selectedItemId = selectedItemId;

    _displayTitle = displayTitle;

    //--
    var url = url_GetAuditTrailFilters + '?auditObjectType=' + auditObjectType;

    //--
    var originalWindow = $(window);

    var windowHeight = originalWindow.height() - 100;
    var windowWidth = originalWindow.width() - 100;
    var windowTop = (originalWindow.height() - windowHeight) / 2;
    var windowLeft = (originalWindow.width() - windowWidth) / 2;
    var windowName = 'AuditTrail_' + auditObjectType;
    var windowOptions = 'width=' + windowWidth + ',height=' + windowHeight + ',top=' + windowTop + ',left=' + windowLeft + ',resizable=no,scrollbars=no,location=no,menubar=no,toolbar=no,status=no';

    //--

    window.open(url, windowName, windowOptions, null);
}

/* THIS CODE NEEDS CLEANUP - IMPACTS ONLY THE LEFT PANEL OF AUDIT TRAIL*/

function displayWGAuditTrail(auditObjectType, selectedItemId, displayTitle, selectedWorkgroupRole, isParent) {
    //--
    _auditObjectType = auditObjectType;

    _selectedItemId = selectedItemId;

    _displayTitle = displayTitle;

    //--
    var url = '/GCS/Global/GetWGAuditTrailFilters?auditObjectType=' + auditObjectType + '&selectedWorkgroupRole=' + selectedWorkgroupRole +'&isParent=' + isParent;

    //--
    var originalWindow = $(window);

    var windowHeight = originalWindow.height() - 100;
    var windowWidth = originalWindow.width() - 100;
    var windowTop = (originalWindow.height() - windowHeight) / 2;
    var windowLeft = (originalWindow.width() - windowWidth) / 2;
    var windowName = 'AuditTrail_' + auditObjectType;
    var windowOptions = 'width=' + windowWidth + ',height=' + windowHeight + ',top=' + windowTop + ',left=' + windowLeft + ',resizable=no,scrollbars=no,location=no,menubar=no,toolbar=no,status=no';

    //--
    window.open(url, windowName, windowOptions, null);


}

window.onload = function () {    
    var popup = window.opener;
    if (popup) {                        
        $('#auditHeader').text(popup._displayTitle);
        _auditObjectType = popup._auditObjectType;
        _selectedItemId = popup._selectedItemId;
        _displayTitle = popup._displayTitle;
                
        if (popup.releaseNewOrExist == "New") {        
            $("#CheckboxItem233").parent().hide();
        }

        if ($('#auditFilterCount').val() == 2) {
        $("#accordion input[type='checkbox']").each(function () {
            $(this).prop('checked',true);
        });
            getAuditTrail();
        }

    }

  };

$(document).ready(function () {
       // $("#accordion > div").accordion({ header: "h3", collapsible: true });
       $("#accordion h5").click(function () {                   
            var obj = $(this).closest("div").parent();
            $(obj).find('.div1').toggle();
            $(this).toggleClass('rightArrow');
        });   
});

function resetSelection() {

    $("#accordion input[type='checkbox']").each(function () {
        $(this).prop('checked',false);
    });

}

function getSelectedAuditConfiguration() {
    var checkedFiltersList = [];
    $("#accordion input[type='checkbox']").each(function () {
        if ($(this).prop('checked') == true) {
            checkedFiltersList.push($(this).attr('id').substring(12, 16));
        }
    });
    return checkedFiltersList;
}

/* THIS CODE NEEDS CLEANUP - IMPACTS ONLY THE LEFT PANEL OF AUDIT TRAIL*/

//#endregion - Left Panel

//#region - Right Panel

function exportToExcel() {

    //--
    var selectedAuditConfiguration = getSelectedAuditConfiguration();

    if (selectedAuditConfiguration.length == 0) { return; }

    //--
    var headerRightPanel = $('.headerRightPanel');

    //--
    headerRightPanel.find('#auditObjectType').val(_auditObjectType);
    headerRightPanel.find('#selectedAuditConfiguration').val(selectedAuditConfiguration.join('|'));
    headerRightPanel.find('#selectedItemId').val(_selectedItemId.join('|'));
    headerRightPanel.find('#displayTitle').val(_displayTitle);

    //--
    $("#frmExportToExcel").submit();

}

function getAuditTrail() {

    //--    
    var selectedAuditConfiguration = getSelectedAuditConfiguration();

    if (selectedAuditConfiguration.length == 0) { return; }

    //--
    $('.loadingDv').show();

    //--
    var postData = { auditObjectType: _auditObjectType,
        selectedAuditConfiguration: selectedAuditConfiguration,
        selectedItemId: $.makeArray(_selectedItemId)
    };

    //--
    var rightPanel = $('.rightPanel');

    //--
    $.ajax({
        url: url_GetAuditTrail,
        type: 'POST',
        data: JSON.stringify(postData),
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {        
            if (data.Error != true) {

                rightPanel.html(data);

            }
            else {

                rightPanel.html("Error loading Audit History.");

            }
        },
        error: function () {

            rightPanel.html("Error communicating with server.");

        },
        complete: function () {

            $('.loadingDv').hide();

        }
    });
}

//#endregion - Right Panel
