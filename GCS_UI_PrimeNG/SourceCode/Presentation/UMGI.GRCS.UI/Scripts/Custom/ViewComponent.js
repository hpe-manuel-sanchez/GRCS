$(document).ready(function () {
    $('#popupLeftScrollableDv').jstree();
    $('#popupLeftScrollableDv').jstree().bind('loaded.jstree', function (e, data) {
        var depth = 1;
        data.inst.get_container().find('li').each(function (i) {
            if (data.inst.get_path($(this)).length <= depth) {
                data.inst.open_node($(this));
            }
        });
    });
    $('#loadingDivPA').hide();
});

function GetDetails(ReleaseId) {
    var SplittedVal = ReleaseId.toString().split('-');
    var popup = window.opener;
    if (popup) {
        var _roleGroup = popup._activeRoleGroup
    }

    var values =
        {
            "clearanceRelease.R2ReleaseId": SplittedVal[0],
            "clearanceRelease.Upc": SplittedVal[1],
            "clearanceRelease.IsAddedBySearchPkg": SplittedVal[2],
            "clearanceRelease.IsRemoved": (_roleGroup != "Requestor" && _roleGroup != null) ? true : false
        };
    $('#loadingDivPA').show();

    $.ajax({
        type: 'POST',
        url: '/GCS/ClearanceRelease/ReleaseDetailsViewComponent',
        data: values,
        success: function (response) {
            $('#popupRightDv').html(response);
            if ($("#hdnStatusType").val() == 3 || $("#hdnStatusType").val() == 4) {
                $('#viewComponentRemoveLink').attr('disabled', 'disabled');
            }
            $('#loadingDivPA').hide();
        }
    });

    //$('#popupRightDv').load('/GCS/ClearanceRelease/ReleaseDetailsViewComponent', values);
}

function RemoveRelease(ReleaseId) {
    var formId = window.parent.document.forms(0);
    var parentHiddenField = window.parent.document.getElementById("RemovedPackageReleases" + $('#hdnrowId').text());
    if (parentHiddenField.value != null && parentHiddenField.value != 'Empty') {
        parentHiddenField.value = parentHiddenField.value + ',' + ReleaseId;
    }
    else {
        parentHiddenField.value = ReleaseId;
    }

    var parentIncludedUpcField = window.parent.document.getElementById("ExistingReleasesUPC" + $('#hdnrowId').text());
    var Includedpackagestring = window.parent.document.getElementById("PackageIncludedUPC" + $('#hdnrowId').text());

    if (parentIncludedUpcField.value != null && parentIncludedUpcField.value != 'Empty' && parentHiddenField.value != '') {
        var AddedPackagesUPCs;
        var existingReleases = parentIncludedUpcField.value.toString().split('~');
        for (var i = 0; i < existingReleases.length; i++) {
            if (existingReleases[i] != '') {
                var removedupcs = parentHiddenField.value.toString().split(',');
                if (removedupcs != null) {
                    var existingReleasesDetails = existingReleases[i].toString().split('=');
                    if (AddedPackagesUPCs == null) {
                        if (removedupcs.indexOf(existingReleasesDetails[0] + '-' + existingReleasesDetails[6]) < 0) {
                            AddedPackagesUPCs = existingReleasesDetails[6];
                        }
                    }
                    else {
                        if (removedupcs.indexOf(existingReleasesDetails[0] + '-' + existingReleasesDetails[6]) < 0) {
                            AddedPackagesUPCs = AddedPackagesUPCs + ', ' + existingReleasesDetails[6];
                        }
                    }
                }
            }
        }
        //Includedpackagestring.innerHTML = AddedPackagesUPCs;
        if (AddedPackagesUPCs != undefined)
            Includedpackagestring.innerHTML = AddedPackagesUPCs;
        else {
            Includedpackagestring.innerHTML = "";
            //window.parent.document.getElementById("ExistingReleasesUPC" + $('#hdnrowId').text()).value = "";
        }
    }
    $('#ViewComponentPopup').remove();
    $('#ViewComponentPopup').dialog('close');
    $(".ViewComponent").click();
}