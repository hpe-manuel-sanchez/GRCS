var releaseNewOrExist;
var _projectStatus;
var currencyDdwnFocus = 0;
var IsResubmissionTrigerred = 0;

var StatusType = {
    Unsubmitted: { value: 1, name: "Unsubmitted" },
    Submitted: { value: 2, name: "Submitted" },
    Cancelled: { value: 3, name: "Cancelled" },
    Completed: { value: 4, name: "Completed" },
    ReSubmitted: { value: 5, name: "ReSubmitted" },
    ReOpened: { value: 6, name: "ReOpened" }
};

var globalPostBackCheck = false;

var regularProjectMessages = {
    cancelValidSave: 'No details are entered for the project. Do you want to save the blank project?',
    confim: "Confirm"
};

function setBottomsButtonVisibilty(projectStatus, btnUPCAllocate, PushToR2NewReleaseExist, RCCAdminAllocateUPC) {

    if (RCCAdminAllocateUPC != 0 && RCCAdminAllocateUPC != null) {

        var Count = $('#hdnReleaseRowsCount').val();
        var textReleaseArray = new Array("chkUpcNumber_", "txtNewReleaseUpcNum");
        var controlId = "#btnAllocateUPC";
        for (relcount = 0; relcount <= Count - 1; relcount++) {
            var upcNum = $("#hdnUpcNumber" + relcount).val()
            for (var i = 0; i < textReleaseArray.length; i++) {

                if (upcNum == '') {
                    var FieldName = textReleaseArray[i] + relcount;
                    controlId += ",#" + FieldName
                }
            }
        }

        var selectControlId = '#rcc_handler';
        $(function () { $('input:not(' + controlId + ',#btnEmail, #btnPrint,#IsReadOnlyMode), textarea, .ui-datepicker-trigger,  select:not(' + selectControlId + ')').prop('disabled', true); });
        $(function () { $('input, textarea, .ui-datepicker-trigger, select:not(' + selectControlId + ')').prop('title', ''); });

        $('#btnAllocateUPC').show();
        $('#btnAllocateUPC').addClass('primbutton');
        $('#btnReOpen').hide();
        $('#btnSubmit').hide();
        $('#btnSave').hide();
        $('#btnCancelProject').hide();
        $('#btnComplete').hide();
        $('#btnPushToR2').hide();
        $('#rcc_handler').prop('disabled', false);
    }
    else if (projectStatus == StatusType.Cancelled.name) {

        $(function () { $('input:not(#btnReinstate,#btnPrint,#btnEmail, #refreshBtn), textarea, select, .ui-datepicker-trigger, #btnAddRelease').attr('disabled', 'disabled'); });
        $('#btnReinstate').show();
        $('#btnReinstate').removeClass();
        $('#btnReinstate').addClass('primbutton');
        $('#btnReOpen').hide();
        $('#btnSubmit').hide();
        $('#btnSave').hide();
        $('#btnCancelProject').hide();
        $('#btnComplete').hide();
        $('#btnAllocateUPC').hide();
        $('#btnPushToR2').hide();
        $('#btnEmail').show();
        $('#btnPrint').show();

    }
    else if (projectStatus == StatusType.ReOpened.name) {

        $('#btnReinstate').hide();
        $('#btnReOpen').hide();
        $('#btnSubmit').hide();
        $('#btnSaveResubmit').show();
        $('#btnSave').hide();
        $('#btnSaveResubmit').removeClass();
        $('#btnSaveResubmit').addClass('primbutton');
        $('#btnCancelProject').show();
        $('#btnComplete').show();
        $('#btnPushToR2').hide();
        if (PushToR2NewReleaseExist != "" && PushToR2NewReleaseExist != null) {
            $('#btnPushToR2').show();
        }
    }
    else if (projectStatus == StatusType.Submitted.name) {

        $('#btnReinstate').hide();
        $('#btnReOpen').hide();
        $('#btnSubmit').show();
        $('#btnSubmit').attr("value", "Save");
        $('#btnSave').hide();
        $('#btnSubmit').removeClass();
        $('#btnSubmit').addClass('primbutton');
        $('#btnCancelProject').show();
        $('#btnComplete').show();
        $('#btnPushToR2').hide();
        if (PushToR2NewReleaseExist != "" && PushToR2NewReleaseExist != null) {
            $('#btnPushToR2').show();
        }
    }
    else if (projectStatus == StatusType.ReSubmitted.name) {

        $('#btnReinstate').hide();
        $('#btnReOpen').hide();
        $('#btnSubmit').show();
        $('#btnSubmit').attr("value", "Save");
        $('#btnSave').hide();
        $('#btnSubmit').removeClass();
        $('#btnSubmit').addClass('primbutton');
        $('#btnCancelProject').show();
        $('#btnComplete').show();
        $('#btnPushToR2').hide();
        if (PushToR2NewReleaseExist != "" && PushToR2NewReleaseExist != null) {
            $('#btnPushToR2').show();
        }
    }
    else if (projectStatus == StatusType.Completed.name) {

        $(function () { $('input:not(#btnReOpen,#btnPrint,#btnEmail, #refreshBtn), textarea, select, .ui-datepicker-trigger, #btnAddRelease').attr('disabled', 'disabled'); });
        $('#btnReinstate').hide();
        $('#btnSubmit').hide();
        $('#btnSave').hide();
        $('#btnCancelProject').hide();
        $('#btnComplete').hide();
        $('#btnReOpen').show();
        $('#btnReOpen').removeClass();
        $('#btnReOpen').addClass('primbutton');
        $('#btnAllocateUPC').hide();
        $('#btnPushToR2').hide();
    }
    else {

        $('#btnReinstate').hide();
        $('#btnReOpen').hide();
        $('#btnSubmit').hide();
        $('#btnSave').show();
        $('#btnSave').removeClass();
        $('#btnSave').addClass('secbutton');
        $('#btnSubmit').show();
        $('#btnComplete').hide();
        $('#btnCancelProject').hide();
        $('#btnAllocateUPC').hide();
        $('#btnPushToR2').hide();
    };

    // UC011 A changes
    if (projectStatus == StatusType.ReOpened.name || projectStatus == StatusType.Submitted.name || projectStatus == StatusType.ReSubmitted.name) {
        _projectStatus = projectStatus;

        for (var i = 0; i < RegularControlsList.length; i++) {
            if (RegularControlsList[i].indexOf("btnManageTerritories_1") != -1) {
                if (document.getElementById(RegularControlsList[i]) != null) {
                    document.getElementById(RegularControlsList[i]).title = ResubmissionTooltipMsg;
                }
            }

            if (RegularControlsList[i].indexOf("_") == -1
            && RegularControlsList[i].indexOf("-") == -1 && RegularControlsList[i].indexOf("rdoNonTrad") == -1) {
                if (document.getElementById(RegularControlsList[i]) != null) {
                    document.getElementById(RegularControlsList[i]).title = ResubmissionTooltipMsg;
                }
            }
            else {
                if ($('#cmbReleaseNewOrExisting').val() == "1") {
                    var ReleaseCount = $('#hdnReleaseRowsCount').val();
                }
                else {
                    var ReleaseCount = $('#ReleaseModelCount').val();
                }

                for (var j = 0; j < ReleaseCount; j++) {

                    if (document.getElementById(RegularControlsList[i] + j) != null) {

                        document.getElementById(RegularControlsList[i] + j).title = ResubmissionTooltipMsg;
                    }
                    //check for New Release
                    if ($('#cmbReleaseNewOrExisting').val() == "1") {
                        if (document.getElementById("LabelId_" + j) != null && $("#ddlMusicType_" + j).val() == "1") {
                            document.getElementById("labelName_" + j).title = ResubmissionTooltipMsg;
                        }

                        if (document.getElementById("txtNoOfComp_" + j) != null) {
                            document.getElementById("txtNoOfComp_" + j).title = ResubmissionTooltipMsg;
                        }
                    }
                }
            }
        }
    }
};

function HideTooltipMsg(e, contId) {
    var RlsId = $(contId).attr('id');

    if (($('#hdnProjectStatusId').val() == "2") || ($('#hdnProjectStatusId').val() == "5") || ($('#hdnProjectStatusId').val() == "6")) {
        if ($('#cmbReleaseNewOrExisting').val() == "1") {

            if ($("#" + RlsId).val() == "2") {
                var n = RlsId.lastIndexOf("_");
                var j = RlsId.substring(n + 1, RlsId.length);
                document.getElementById("labelName_" + j).title = "";
            }
            if ($("#" + RlsId).val() == "1") {
                var n = RlsId.lastIndexOf("_");
                var j = RlsId.substring(n + 1, RlsId.length);
                document.getElementById("labelName_" + j).title = ResubmissionTooltipMsg;
            }
        }
    }
}

function OnlyNumeric(event, inputType) {
    //Validate the key press event for integer/character/Aplhanuemeric
    if (window.event) { var charCode = window.event.keyCode; }
    else if (event) { var charCode = event.which; }
    else { return true; }

    if (inputType == 'i') {//integer validation
        if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 96 || charCode > 105))
        { return false; }
    }
    else if (inputType == 'c') {//character validation
        if (charCode > 31 && ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)))
        { return false; }
    }
    else if (inputType == 'a') {//alphanumeric validation
        if (charCode > 31 && ((charCode < 48 || charCode > 57) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)))
        { return false; }
    }
    else {

        return true;
    }
    return true;
}

function ParentLoadTerritories(id) {
    var tempId = id;
    var territoryDisplayCollection = "";
    if (id != 1 && id != 2) {
        territoryDisplayCollection = $('#hdnterritoryDetailsForSave_' + id).val();
        if (territoryDisplayCollection == "" || territoryDisplayCollection == "[]") {
            tempId = 1;
        }
    }

    territoryDisplayCollection = $('#hdnterritoryDetailsForSave_' + tempId).val();
    if (territoryDisplayCollection != "" && territoryDisplayCollection != undefined) {
        territoryDisplayCollection = JSON.parse(territoryDisplayCollection);
        LoadTerritories(territoryDisplayCollection, id);
    }
    else {
        LoadTerritories(territoryDisplayCollection, id);
    }
}

function LoadTerritories(territories, key) {
    var territory = JSON.stringify(territories);
    var manageTerritoryTitleOpt = manageTerritoryTitle;
    if (key == 2) {
        manageTerritoryTitleOpt = campaignManageTerritoryTitle;
    }
    var objTerritorialPopup = $('<div id="Territory"></div>')
        .html('<p>Loading</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: manageTerritoryTitleOpt,
            show: 'clip',
            hide: 'clip',
            width: "98%",
            height: 510,
            minHeight: 510,
            close: function () {
                $(this).remove(); // ensures any form variables are reset.

            }
        })
        .load($.ajax({
            url: '/GCS/Territory/GetTerritories/' + key,
            type: 'POST',
            dataType: 'html',
            async: true,
            data: territory,
            cache: false,
            success: function (data) {
                if (data == 'error') {
                    objTerritorialPopup.html('<p>Error</p>');
                }
                else {
                    if (key != "1" && key != "2") {
                        var appendstring = "$('.Expanded').find('.territoryCountry td:eq(3)').each(function (a) { $(this).find('input[type=radio]').each(function (){ $(this).attr('disabled',true);});  });$('.Expanded').find('.territoryCountry td:eq(1)').each(function (a) { $(this).find('input[type=radio]').each(function (){ $(this).attr('disabled',true);});  }); $('.Expanded').find('.territoryCountry td:eq(2)').each(function (a) { $(this).find('input[type=radio]').each(function (){ $(this).attr('disabled',true);});  });  $('.Collapsed').find('.territoryCountry td:eq(2)').each(function (a) { var radCon =$(this).find('input[type=radio]'); var Id = radCon.attr('Name'); var Name = Id.toString().split('|'); var isExcludedAtProject = false; var isIncludedAtProject = false; var isExcludedAtResource = false;  var ExcludedResources = $('#UnselectedCountries_1')[0].innerText.toString().split(';'); var IncludedResources = $('#hdnIncludedTerritoriesProject').val().toString().split(';');  var ExcludedatResourceLevel = $('#UnselectedCountries_' + " + key + ")[0].innerText.toString().split(';'); if (ExcludedResources != null) { if (ExcludedResources != '') { for (var i = 0; i < ExcludedResources.length; i++) { if (ExcludedResources[i].toString().trim() == Name[0]) { isExcludedAtProject = true; } } } } if (IncludedResources != null) { if (IncludedResources != '') { for (var i = 0; i < IncludedResources.length; i++) { if (IncludedResources[i].toString().trim() == Name[0]) { isIncludedAtProject = true; } } } } if (ExcludedatResourceLevel != null) { if (ExcludedatResourceLevel != '') { for (var i = 0; i < ExcludedatResourceLevel.length; i++) { if (ExcludedatResourceLevel[i].toString().trim() == Name[0]) { isExcludedAtResource = true; isIncludedAtProject = true; } } } }   if(!radCon.is(':checked') && (isExcludedAtProject == true || isExcludedAtResource == false || isIncludedAtProject == false)){radCon.attr('disabled',true); }  var previous = $(this).prev().find('input[type=radio]'); previous.attr('disabled',true);  if(!radCon.is(':checked') && (isExcludedAtProject == true || isExcludedAtResource == false || isIncludedAtProject == false)) { var next = $(this).next().find('input[type=radio]'); next.attr('disabled',true);   }}); var  sitecollection";
                        data = data.replace('var  sitecollection', appendstring);
                    }
                    objTerritorialPopup.html(data);
                }
            },
            contentType: 'application/json; charset=utf-8'
        }))
        .dialog('open');
}

function ParentCall() {
    $('.hdnManageTerriCls').each(function () {

        var id = $(this).attr('id').split('_');
        var territoryDisplayCollection = $('#hdnterritoryDetailsForSave_' + id[1]).val();
        if (territoryDisplayCollection != "" && territoryDisplayCollection != undefined && territoryDisplayCollection != "[]") {

            territoryDisplayCollection = JSON.parse(territoryDisplayCollection);
            callbackHandler(territoryDisplayCollection, id[1], null, null);
        }


    });
}

function CheckDuplicates(Name, List) {
    $.each(List, function (index, element) {
        if (List[index] == Name) {
            return true;
        }
    });
    return false;

}

function callbackHandlerForResource(territoryCollection, uniqueKey, includedItems, excludedItems, wasReset, closePopUpCallBack) {
    if (territoryCollection.length === 0 && wasReset) {
        var objWarningDialog = $('<div id="Confirm"></div>')
                                    .html('<p>No Territory has been selected, do you still want to continue?</p>')
                                    .dialog({
                                        autoOpen: false,
                                        modal: true,
                                        title: regularProjectMessages.confim,
                                        show: 'clip',
                                        hide: 'clip',
                                        width: 400,
                                        height: 200
                                    });
        objWarningDialog.dialog('open');
        objWarningDialog.dialog({
            buttons:
            {
                'Yes': function (e) {
                    $(this).dialog('close');
                    saveTerritoryCollection(territoryCollection, uniqueKey, includedItems, excludedItems);
                    closePopUpCallBack();
                },
                'No': function (e) {
                    $(this).dialog('close');
                }
            }
        });
    }
    else {
        var mismatchTerritories = territoryValidationAtResourceLevel(territoryCollection);

        if (mismatchTerritories.length > 0) {
            changesOnResourceValidated = false;
            var mismatchTerritoriesToShow = mismatchTerritories.join(", ");

            var objWarningDialog = $('<div id="Confirm"></div>')
                                    .html('<p>Selected countries and/or territories are not part of the Project Territory, please make a valid selection.</p><br>' + mismatchTerritoriesToShow + '</p>')
                                    .dialog({
                                        autoOpen: false,
                                        modal: true,
                                        title: regularProjectMessages.confim,
                                        show: 'clip',
                                        hide: 'clip',
                                        width: 500,
                                        height: 200
                                    });
            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Ok': function (e) {
                        $(this).dialog('close');
                    }
                }
            });
        }
        else {
            saveTerritoryCollection(territoryCollection, uniqueKey, includedItems, excludedItems);
            closePopUpCallBack();
        }
    }
}

function callbackHandler(territoryCollection, uniqueKey, includedItems, excludedItems) {
    saveTerritoryCollection(territoryCollection, uniqueKey, includedItems, excludedItems)
}

function saveTerritoryCollection(territoryCollection, uniqueKey, includedItems, excludedItems) {

    var tempVar = JSON.stringify(territoryCollection);

    if (uniqueKey == "1" && $('#hdnTempterritoryDetailsForSave').val() != "" && $('#hdnTempterritoryDetailsForSave').val() != tempVar) {
        $('#hdnTempterritoryDetailsForSave').val(tempVar);
        $('.hdnManageTerriCls').each(function () {
            var id = $(this).attr('id').split('_');
            if (id[1] != 2 && id[1] != 1) {
                $("#selectedCountries_" + id[1]).html('');
                $("#UnselectedCountries_" + id[1]).html('');
                $('#hdnterritoryDetailsForSave_' + id[1]).val('');
            }
        });
    }

    if (uniqueKey == 1) {
        $('#hdnTempterritoryDetailsForSave').val(tempVar);
    }

    var TerritoryChangedOnResource = false;
    if (uniqueKey != 1 && uniqueKey != 2 && $('#hdnterritoryDetailsForSave_' + uniqueKey).val() != tempVar) {
        TerritoryChangedOnResource = true;
    }

    if (uniqueKey == 1 || uniqueKey == 2 || $('#hdnterritoryDetailsForSave_' + 1).val() != tempVar || TerritoryChangedOnResource == true) {

        $('#hdnterritoryDetailsForSave_' + uniqueKey).val(tempVar);

        // GCS start
        sitecollection = territoryCollection;
        //Clear the Countries
        $("#selectedCountries_" + uniqueKey).html('');
        $("#UnselectedCountries_" + uniqueKey).html('');

        var stringOfcountriesterritories = "";
        var stringOfexcountriesterritories = "";
        includedTerritoriesListResult = [];
        excludedTerritoriesListResult = [];
        // GCS End

        var includeTerriString = '';
        var includeCountryString = '';
        var territoryString = '';
        var territoryExcludeString = '';
        var excludeString = '';
        var finalString = '';
        var includedTerritoriesList = [];
        var excludeCountryString = '';
        var excludedCountriesList = [];
        var excludeCountryFinalString = '';
        var CheckexcludedTerrString = '';
        var Checkexcludedsubstring = '';

        rightsIncludedCountry = [];
        rightsExcludedCountry = [];
        rightsIncludedTerritory = [];
        rightsExcludedTerritory = [];

        var includedTerritories = JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded && item.IsTerritory; });
        var excludedTerritories = JSLINQ(sitecollection).Where(function (item) { return item.IsExcluded && item.IsTerritory; });
        var excludedCountries = JSLINQ(sitecollection).Where(function (item) { return item.IsExcluded && !item.IsTerritory; });
        var includedCountries = JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded && !item.IsTerritory; });

        JSLINQ(includedTerritories.items).OrderBy(function (p) { return p.Name; }).All(function (includedTerri) {

            var parent = getParent(includedTerri.ParentId);
            if ((parent == undefined && !checkifDuplicatesParentIsSelected(includedTerri)) || (parent != undefined && parent.IsIncluded == false && !checkifDuplicateIsSelected(parent))) {

                //Eliminate duplicate - South East Asia (Universal Territories) is a duplicate
                var index = jQuery.inArray(includedTerri.Name, includedTerritoriesList);
                if (index == -1) {
                    includeTerriString = includedTerri.Name;
                    includedTerritoriesList.push(includedTerri.Name);
                    includedTerritoriesListResult.push(includedTerri);
                    //review right
                    rightsIncludedTerritory.push(includedTerri.Id);
                    var excludeTerriString = '';
                    var excludeSubTerriString = '';
                    var excludedTerritoriesList = [];
                    //Excluded territories
                    JSLINQ(excludedTerritories.items).All(function (excludedTerri) {

                        var index = jQuery.inArray(excludedTerri.Name, excludedTerritoriesList);
                        if (index == -1) {
                            //review right
                            rightsExcludedTerritory.push(excludedTerri.Id);

                            //Main Territories
                            if (excludedTerri.ParentId == includedTerri.Id) {
                                excludeTerriString += excludedTerri.Name + '; ';
                                excludedTerritoriesList.push(excludedTerri.Name);
                                excludedTerritoriesListResult.push(excludedTerri);
                                //review right
                            } //Sub Territories
                            else {
                                var rootParent = getRootParent(null, excludedTerri.ParentId, true); //Get root parent which is selected
                                if (rootParent != null && (rootParent.Id == includedTerri.Id) && rootParent.IsIncluded == true) {
                                    excludeSubTerriString += excludedTerri.Name + '; ';
                                    excludedTerritoriesList.push(excludedTerri.Name);
                                    excludedTerritoriesListResult.push(excludedTerri);
                                    //review right
                                }
                                else {
                                    if (rootParent == null && excludedTerri.ParentId == 0) {
                                        if (!CheckDuplicates(excludedTerri.Name, excludedTerritoriesList)) {
                                            var splittedTerr = CheckexcludedTerrString.toString().split(';');
                                            var isExist = false;
                                            $.each(splittedTerr, function (index, element) {
                                                if (splittedTerr[index].toString() != "" && splittedTerr[index].toString() != " ") {
                                                    if (excludedTerri.Name == splittedTerr[index].toString().trim()) {
                                                        isExist = true;
                                                    }
                                                }
                                            });
                                            if (!isExist) {
                                                excludeTerriString += excludedTerri.Name + '; ';
                                                excludedTerritoriesList.push(excludedTerri.Name);
                                                excludedTerritoriesListResult.push(excludedTerri);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        return true;
                    });
                    excludedTerritoriesListGCS = excludedTerritoriesList;
                    excludeCountryString = '';
                    excludedCountriesList = [];
                    //Excluded countries
                    JSLINQ(excludedCountries.items).All(function (excludedCountry) {

                        var index = jQuery.inArray(excludedCountry.Name, excludedCountriesList);
                        if (index == -1) {

                            rightsExcludedCountry.push(excludedCountry.Id);
                            if (!checkifDuplicatesParentIsExcluded(excludedCountry)) {
                                if (excludedCountry.ParentId == includedTerri.Id) {
                                    excludeCountryString += excludedCountry.Name + '; ';
                                    excludedCountriesList.push(excludedCountry.Name);
                                } else {
                                    var parent = getParent(excludedCountry.ParentId);
                                    var rootParent = getRootParent(null, excludedCountry.ParentId, true); //Get root parent which is selected

                                    if (rootParent != null && (rootParent.Id == includedTerri.Id) && parent.IsExcluded == false && !checkifDuplicatesParentIsExcluded(excludedCountry) && rootParent.IsIncluded == true) {
                                        excludeCountryString += excludedCountry.Name + '; ';
                                        excludedCountriesList.push(excludedCountry.Name);
                                    }
                                    else {
                                        if (rightsExcludedTerritory.length > 0) {
                                            var IsExcluded = false;

                                            $.each(rightsExcludedTerritory, function (index, element) {
                                                if (excludedCountry.ParentId == rightsExcludedTerritory[index].toString()) {
                                                    IsExcluded = true;
                                                }
                                            });
                                            if (parent == null && rootParent == null && IsExcluded == false) {
                                                if (!CheckDuplicates(excludedCountry.Name, excludedCountriesList)) {
                                                    excludeCountryString += excludedCountry.Name + '; ';
                                                    excludedCountriesList.push(excludedCountry.Name);
                                                }
                                            }

                                        }
                                        else {
                                            if (parent == null && rootParent == null) {
                                                if (!CheckDuplicates(excludedCountry.Name, excludedCountriesList)) {
                                                    excludeCountryString += excludedCountry.Name + '; ';
                                                    excludedCountriesList.push(excludedCountry.Name);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            ;
                        }
                        return true;
                    });

                    if (excludeCountryString != "") {
                        var splittedTerr = excludeCountryFinalString.toString().split(';');
                        $.each(splittedTerr, function (index, element) {
                            if (splittedTerr[index].toString() != "" && splittedTerr[index].toString() != " ") {
                                if (excludeCountryString.indexOf(splittedTerr[index].toString().trim() + ";") >= 0) {
                                    excludeCountryString = excludeCountryString.toString().replace(splittedTerr[index].toString().trim() + ";", '');
                                }
                            }
                        });
                        excludeCountryFinalString += excludeCountryString;
                    }
                    var tempStr = ', ';
                    CheckexcludedTerrString = CheckexcludedTerrString + excludeTerriString.toString();
                    Checkexcludedsubstring = Checkexcludedsubstring + excludeSubTerriString.toString();
                    excludeString = excludeTerriString.toString() + excludeSubTerriString.toString() + excludeCountryString.toString();

                    if (excludeString != '') {

                        if (endsWith(territoryExcludeString, '/') == true || territoryExcludeString == '')
                            tempStr = ' ';

                        territoryExcludeString += tempStr + includeTerriString + (" Excluding " + excludeString).replace(new RegExp(', ' + '$'), '') + " /";
                    } else {

                        if (endsWith(territoryString, '/') == true || territoryString == '')
                            tempStr = ' ';

                        territoryString += tempStr + includeTerriString;
                    }
                }
                excludedCountriesListGCS = excludedCountriesList
            }

            return true;
        });
        if (excludedTerritories.items.length > 0 || (uniqueKey != 1 && uniqueKey != 2 && excludedCountries.items.length > 0)) {
            var excludeTerriString = '';
            var excludeSubTerriString = '';
            var excludedTerritoriesList = [];
            if (excludedTerritories.items.length > 0) {
                //Excluded territories
                $.each(excludedTerritories.items, function (index1, element1) {
                    var IsExcluded = false;
                    rightsExcludedTerritory.push(excludedTerritories.items[index1].Id);
                    $.each(excludedTerritories.items, function (index, element) {
                        if (excludedTerritories.items[index].ParentId != 0 && excludedTerritories.items[index1].ParentId == excludedTerritories.items[index].ParentId.toString()) {
                            IsExcluded = true;
                        }
                    });
                    if (IsExcluded == false) {
                        if (!CheckDuplicates(excludedTerritories.items[index1].Name, excludedTerritories)) {
                            excludeSubTerriString += excludedTerritories.items[index1].Name + '; ';
                            excludedTerritoriesList.push(excludedTerritories.items[index1].Name);
                            excludedTerritoriesListResult.push(excludedTerritories.items[index1]);
                        }
                    }
                });
                excludedTerritoriesListGCS = excludedTerritoriesList;
            }
            if (excludeCountryString == "") {
                excludeCountryString = '';
                excludedCountriesList = [];
                JSLINQ(excludedCountries.items).All(function (excludedCountry) {

                    var index = jQuery.inArray(excludedCountry.Name, excludedCountriesList);
                    if (index == -1) {
                        rightsExcludedCountry.push(excludedCountry.Id);
                        var parent = getParent(excludedCountry.ParentId);
                        var rootParent = getRootParent(null, excludedCountry.ParentId, true); //Get root parent which is selected

                        if (rootParent != null && parent.IsExcluded == false && !checkifDuplicatesParentIsExcluded(excludedCountry) && rootParent.IsIncluded == true) {
                            excludeCountryString += excludedCountry.Name + '; ';
                            excludedCountriesList.push(excludedCountry.Name);
                        }
                        else {
                            if (rightsExcludedTerritory.length > 0) {
                                var IsExcluded = false;
                                $.each(rightsExcludedTerritory, function (index, element) {
                                    if (excludedCountry.ParentId == rightsExcludedTerritory[index].toString()) {
                                        IsExcluded = true;
                                    }
                                    else {
                                        var parentids = [];
                                        $.each(excludedCountries.items, function (index3, element3) {
                                            if (excludedCountry.Name == excludedCountries.items[index3].Name) {
                                                parentids.push(excludedCountries.items[index3].ParentId);
                                            }
                                        });
                                        if (parentids != null) {
                                            $.each(parentids, function (index4, element4) {
                                                if (parentids[index4] == rightsExcludedTerritory[index].toString()) {
                                                    IsExcluded = true;
                                                }
                                            });
                                        }
                                    }
                                });
                                if (parent == null && rootParent == null && IsExcluded == false) {
                                    if (!CheckDuplicates(excludedCountry.Name, excludedCountriesList)) {
                                        excludeCountryString += excludedCountry.Name + '; ';
                                        excludedCountriesList.push(excludedCountry.Name);
                                    }
                                }
                            }
                            else {
                                if (parent == null && rootParent == null) {
                                    if (!CheckDuplicates(excludedCountry.Name, excludedCountriesList)) {
                                        excludeCountryString += excludedCountry.Name + '; ';
                                        excludedCountriesList.push(excludedCountry.Name);
                                    }
                                }
                            }
                        }
                    }
                    return true;
                });

                if (excludeCountryString != "") {
                    var splittedTerr = excludeCountryFinalString.toString().split(';');
                    $.each(splittedTerr, function (index, element) {
                        if (splittedTerr[index].toString() != "" && splittedTerr[index].toString() != " ") {
                            if (excludeCountryString.indexOf(splittedTerr[index].toString().trim() + ";") >= 0) {
                                excludeCountryString = excludeCountryString.toString().replace(splittedTerr[index].toString().trim() + ";", '');
                            }
                        }
                    });
                    excludeCountryFinalString += excludeCountryString;
                }
            }
            var tempStr = ', ';

            excludeString = excludeTerriString.toString() + excludeSubTerriString.toString() + excludeCountryString.toString();

            if (excludeString != '') {

                if (endsWith(territoryExcludeString, '/') == true || territoryExcludeString == '')
                    tempStr = ' ';

                territoryExcludeString += tempStr + includeTerriString + (" Excluding " + excludeString).replace(new RegExp(', ' + '$'), '') + " /";
            } else {

                if (endsWith(territoryString, '/') == true || territoryString == '')
                    tempStr = ' ';

                territoryString += tempStr + includeTerriString;
            }
            excludedCountriesListGCS = excludedCountriesList
        }

        includedTerritoriesListGCS = includedTerritoriesList;

        if (territoryExcludeString != '') {

            if (territoryString == '') {
                territoryString = territoryExcludeString;
            } else {
                territoryString = territoryExcludeString + ' ' + territoryString;
            }
        }

        var includedCountriesList = [];
        JSLINQ(includedCountries.items).OrderBy(function (p) { return p.Name; }).All(function (includedCountry) {

            var index = jQuery.inArray(includedCountry.Name, includedCountriesList);
            if (index == -1) {
                rightsIncludedCountry.push(includedCountry.Id);
                if (!checkifDuplicatesParentIsSelected(includedCountry)) {

                    var parent = getParent(includedCountry.ParentId);
                    var rootParent = getRootParent(null, includedCountry.ParentId, true); //Get root parent which is excluded

                    if (parent == null && rootParent == null) {
                        includeCountryString += includedCountry.Name + '; ';
                        includedCountriesList.push(includedCountry.Name);
                    } else if (parent != null && parent.IsIncluded == false || (rootParent != null && rootParent.IsIncluded == false)) {
                        includeCountryString += includedCountry.Name + '; ';
                        includedCountriesList.push(includedCountry.Name);
                    }
                }
            }
            return true;
        });
        includedCountriesListGCS = includedCountriesList;
        if (includeCountryString != '') {
            var temp = ', ';
            if (territoryString == '' || endsWith(territoryString, '/') == true)
                temp = ' ';
            //Remove last , and replace /, with /
            finalString = (territoryString + temp + includeCountryString.replace(new RegExp(", " + '$'), '')).replace(/\/, /g, '/ ');
        } else {
            finalString = territoryString.replace(new RegExp("\/" + '$'), '').replace(new RegExp(", " + '$'), '');
        }

        var includedvalues;
        for (var i = 0; i < includedTerritoriesListResult.length; i++) {
            childIncludedCountries = [];
            if (includedTerritoriesListResult[i].Name != "World" && includedTerritoriesListResult[i].Name != "Universe") {
                var countriesFinal = GetTerritoryCountries(includedTerritoriesListResult[i].Id);
                var countries = "";

                var dupliacteTerritories = {};
                var distinctTerritories = [];

                $.each(countriesFinal, function (i, data) {
                    if (!dupliacteTerritories[data]) {
                        dupliacteTerritories[data] = true;
                        distinctTerritories.push(data);
                    }
                });
                if (distinctTerritories != null) {
                    distinctTerritories.sort()
                    for (var j = 0; j < distinctTerritories.length; j++) {
                        if (countries != "") {
                            countries = countries + '; ' + distinctTerritories[j];
                        }
                        else {
                            countries = distinctTerritories[j];
                        }
                    }
                }
                if (i == includedTerritoriesListResult.length - 1) {
                    var link = '<a id="' + includedTerritoriesListResult[i].Id + uniqueKey + '"style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + includedTerritoriesListResult[i].Name + '</a>; ';
                }
                else {
                    var link = '<a id="' + includedTerritoriesListResult[i].Id + uniqueKey + '"style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + includedTerritoriesListResult[i].Name + '</a>; ';
                }
            }
            else {
                var link = includedTerritoriesListResult[i].Name;

            }
            $('#selectedCountries_' + uniqueKey).append(link);
            if (uniqueKey == 1) {
                if (includedvalues != null) {
                    includedvalues = includedvalues + countries + ';' + $('#selectedCountries_' + uniqueKey)[0].innerText;
                }
                else {
                    includedvalues = countries + ';' + $('#selectedCountries_' + uniqueKey)[0].innerText;
                }
                $('#hdnIncludedTerritoriesProject').val(includedvalues);
            }
            if (includedTerritoriesListResult[i].Name != "World" && includedTerritoriesListResult[i].Name != "Universe")
                $('#' + includedTerritoriesListResult[i].Id + uniqueKey).flyout();

        }
        $('#selectedCountries_' + uniqueKey).append(includeCountryString);
        $('#hdnincludeTerritoryString_' + uniqueKey).val($('#selectedCountries_' + uniqueKey)[0].innerText);
        var excludedvalues;
        if (excludedTerritoriesListResult.length > 0) {
            var excludedTerritoriesListResultLength = excludedTerritoriesListResult.length;
            for (var i = 0; i < excludedTerritoriesListResultLength; i++) {

                childExcludedCountries = [];
                if (excludedTerritoriesListResult[i].Name != "World" || excludedTerritoriesListResult[i].Name != "Universe") {
                    var countriesFinal = GetExcludedCountries(excludedTerritoriesListResult[i].Id);
                    var countries = "";

                    var dupliacteTerritories = {};
                    var distinctTerritories = [];

                    $.each(countriesFinal, function (i, data) {
                        if (!dupliacteTerritories[data]) {
                            dupliacteTerritories[data] = true;
                            distinctTerritories.push(data);
                        }
                    });
                    if (distinctTerritories != null) {
                        distinctTerritories.sort();
                        var distinctTerritoriesLength = distinctTerritories.length;
                        for (var j = 0; j < distinctTerritoriesLength; j++) {
                            if (countries != "") {
                                countries = countries + '; ' + distinctTerritories[j];
                            }
                            else {
                                countries = distinctTerritories[j];
                            }
                        }
                    }

                    if (i == excludedTerritoriesListResult.length - 1) {
                        var link = '<a id="' + excludedTerritoriesListResult[i].Id + uniqueKey + '"style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + excludedTerritoriesListResult[i].Name + '</a>; ';

                    }
                    else {
                        var link = '<a id="' + excludedTerritoriesListResult[i].Id + uniqueKey + '"style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + excludedTerritoriesListResult[i].Name + '</a>; ';



                    }
                }
                else {
                    var link = excludedTerritoriesListResult[i].Name;
                }


                $('#UnselectedCountries_' + uniqueKey).append(link);
                if (uniqueKey == 1) {
                    if (excludedvalues != null) {
                        excludedvalues = excludedvalues + countries + ';' + $('#UnselectedCountries_' + uniqueKey)[0].innerText;
                    }
                    else {
                        excludedvalues = countries + ';' + $('#UnselectedCountries_' + uniqueKey)[0].innerText;
                    }
                    $('#hdnExcludedTerritoriesProject').val(excludedvalues);
                }
                if (excludedTerritoriesListResult[i].Name != "World" && excludedTerritoriesListResult[i].Name != "Universe")
                    $('#' + excludedTerritoriesListResult[i].Id + uniqueKey).flyout();
            }
        }
        else {
            $('#hdnExcludedTerritoriesProject').val('');
        }
        $('#UnselectedCountries_' + uniqueKey).append(excludeCountryFinalString);
        $('#hdnExcludeTerritoryString_' + uniqueKey).val($('#UnselectedCountries_' + uniqueKey)[0].innerText);
        return finalString;
    }
}

function territoryValidationAtResourceLevel(resourceTerritories) {

    var hdnterritoryDetailsForSave_1 = $('#hdnterritoryDetailsForSave_' + 1).val();
    var projectTerritories = [];

    if (hdnterritoryDetailsForSave_1 != '') {
        projectTerritories = JSON.parse(hdnterritoryDetailsForSave_1);
    }
    
    //Validate if any Resource included country is excluded in project level
    var mismatchTerritories = [];
    if (projectTerritories.length > 0) {
        var includedResourceTerritories = JSLINQ(resourceTerritories).Where(function (item) { return item.IsIncluded });        
        $.each(includedResourceTerritories.items, function (i, obj) {
            var isProjectTerritory =
                JSLINQ(projectTerritories).Where(function (item) { return item.Id == includedResourceTerritories.items[i].Id && item.ParentId == includedResourceTerritories.items[i].ParentId })
            if (isProjectTerritory.items[0] == null || isProjectTerritory.items[0].IsExcluded) {
                var exist = $.inArray(includedResourceTerritories.items[i].Name, mismatchTerritories);
                if (exist === -1)
                    mismatchTerritories.push(includedResourceTerritories.items[i].Name);
            }
        });
    }

    return mismatchTerritories;
}

function getParentById(parentId, collection)//Pass ParentId
{
    return JSLINQ(collection).First(function (item) { return item.Id == parentId });
}

function ValidationCheckOnRegularUnsubmitted() {

    var IsNotEmpty = false;
    var frm = document.forms[0]; //find all controls on page
    for (i = 0; i < frm.elements.length; i++) {

        if (frm.elements[i].type == "checkbox") { //set IsEmpty flag for checkbox
            if (frm.elements[i].checked == true) {


                return IsNotEmpty = true;

            }
        }

        if (frm.elements[i].type == "radio") { //set IsEmpty flag for radio button
            if (frm.elements[i].checked == true) {


                IsNotEmpty = true;
                return IsNotEmpty = true;

            }
        }

        if (frm.elements[i].type == "text") {//set IsEmpty flag for text field
            if (($.trim($('#' + frm.elements[i].id).val())) != "") {
                if ($('#' + frm.elements[i].id).val() != "-1") {
                    if ($('#' + frm.elements[i].id)[0].disabled != true) {


                        return IsNotEmpty = true;
                    }
                }
            }
        }

        if (frm.elements[i].type == "textarea") {//set IsEmpty flag for text area field
            if (($.trim($('#' + frm.elements[i].id).val())) != "") {

                return IsNotEmpty = true;
            }
        }

    } //end for loop

    if ($('#selectedCountries_1').text() != "") { //if any coutry/territory is included
        return IsNotEmpty = true;
    }

    if ($('#UnselectedCountries_1').text() != "") {//if any coutry/territory is excluded
        return IsNotEmpty = true;
    }

    var tblFileUpload = document.getElementById('tblFileUpload'), //if any document is uploaded
        rows = tblFileUpload.getElementsByTagName('tr');
    if (rows.length > 0) {
        return IsNotEmpty = true;
    }

    if (parseInt($('#hdnResourceListCount').val()) > 0) {//count the rows in resource
        return IsNotEmpty = true;
    }

    if (parseInt($('#cmbReleaseNewOrExisting').val()) == 2) {//for existing release

        if (parseInt($('#ReleaseModelCount').val()) > 0) {//if there is any existing release
            return IsNotEmpty = true;
        }
    }

    if (parseInt($('#cmbReleaseNewOrExisting').val()) == 1) {//for new  release

        if (parseInt($('#hdnReleaseRowsCount').val()) == 1) {//if there is any existing release
            if (($('#LabelId_0').val() > 0) && ($('#LabelId_0').val() != "")) {
                return IsNotEmpty = true;
            }

            if (($('#ddlConfigGrpList_0').val() > 0) && ($('#ddlConfigGrpList_0').val() != "")) {
                return IsNotEmpty = true;
            }

            if (($('#ddlConfigList_0').val() > 0) && ($('#ddlConfigList_0').val() != "")) {
                return IsNotEmpty = true;
            }

            if (($('#ddlMusicType_0').val() > 0) && ($('#ddlMusicType_0').val() != "")) {
                return IsNotEmpty = true;
            }

        }

        if (parseInt($('#hdnReleaseRowsCount').val()) > 1) {//if there is any existing release
            return IsNotEmpty = true;
        }
    }
    return IsNotEmpty;
}

function ValidateMoneyFormat(id) {
    var v_text = $(id).val();
    var v_regex = new RegExp("^[0-9]{1,14}.[0-9]{1,2}$");

    if ($(id).val() != '') {
        if (!v_regex.test(v_text)) {
            var replaceValue = v_text.replace(/[^0-9$.,]/g, '')
            $(id).val(replaceValue);
        }
    }
}

function ValidateIntegers(id) {

    var v_text = $(id).val();
    var v_regex = new RegExp('^\\d+$');

    if ($(id).val() != '') {
        if (!v_regex.test(v_text)) {
            var replaceValue = v_text.replace(/[^0-9\.]/g, '');
            $(id).val(replaceValue);
        }

    }
}

function formatCurrency(id) {
    //Validate the key press event for integer/character/Aplhanuemeric
    if (window.event) { var charCode = window.event.keyCode; }
    else if (event) { var charCode = event.which; }
    else { return true; }
    if (charCode >= 37 && charCode <= 40)
    { return true; }

    var num = $(id).val();
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    $(id).val((((sign) ? '' : '-') + num + '.' + cents));
}

function ValidationOnMoneyFields() {

    var textArray = new Array("txtTVBudget", "txtTVBudgetUSD", "txtTVProdCost",
                               "txtRdoBudget", "txtRdoBudgetUSD", "txtRdoProdCost",
                               "txtOthrBudget", "txtOthrBudgetUSD", "txtOthrProdCost",
                               "txtPRevDate", "txtPRevWith", "txtPRevWithout",
                               "txtDRevDate", "txtDRevWith", "txtDRevWithout");
    var returnFlag = true;
    var Count = $('#hdnReleaseRowsCount').val();
    var textReleaseArray = new Array("txtExPPD_", "txtEstRetail_", "txtResourceFee_",
                               "txtDeemedPPD_", "txtInvPrice_", "txtSellPrice_",
                               "txtICLAcc_");

    var existingReleaseCount = $("#ReleaseModelCount").val();
    var textExistingReleaseArray = new Array("txtExactPPD-TVRadio", "txtEstRetail-TVRadio", "txtResourceFee-Non",
                               "txtDeemedPPD-Non", "txtInvoicePrice-Non", "txtSellingPrice-Non", "txtAccounting-Non");


    for (var i = 0; i < textArray.length; i++) {
        var FieldName = textArray[i];
        if ($('#' + FieldName).attr("value") != null || $('#' + FieldName).attr("value") != "" && $('#' + FieldName).is(':visible')) {
            if (!ValidateMoneyFormat($('#' + FieldName))) {
                returnFlag = false;
            }
        }
    }



    for (relcount = 0; relcount <= Count - 1; relcount++) {
        for (var i = 0; i < textReleaseArray.length; i++) {
            var FieldName = textReleaseArray[i] + relcount;
            if ($('#' + FieldName).attr("value") != null || $('#' + FieldName).attr("value") != "" && $('#' + FieldName).is(':visible')) {
                if (!ValidateMoneyFormat($('#' + FieldName))) {
                    returnFlag = false;
                }
            }
        }
    }


    for (relcountTemp = 0; relcountTemp <= existingReleaseCount - 1; relcountTemp++) {
        for (var i = 0; i < textExistingReleaseArray.length; i++) {
            var FieldName = textExistingReleaseArray[i] + relcountTemp;
            if ($('#' + FieldName).attr("value") != null || $('#' + FieldName).attr("value") != "" && $('#' + FieldName).is(':visible')) {
                if (!ValidateMoneyFormat($('#' + FieldName))) {
                    returnFlag = false;
                }
            }
        }
    }

    if (returnFlag == false) {

        $('#divErrorMessage').text(moneyValidaton);
        $('#divErrorMessage').addClass('warning');
        $("#divErrorMessage").show();
    }
    else {
        $("#divErrorMessage").hide();
    }
    return returnFlag;
}

function ValidateMoneyFormat(id) {

    var v_text = $(id).val();
    if ((v_text.indexOf('.') == -1) && (v_text.length > 0)) {
        v_text = v_text + ".00";
        $(id).val(v_text);
    }
    if (v_text.indexOf(".") > 0) {
        var charAfterdot = v_text.substring(v_text.indexOf("."), v_text.length - 1);
        if (charAfterdot.length == 0) {
            v_text = v_text + "00";
            $(id).val(v_text);
        }

    }

    var v_regex = new RegExp("^[0-9]{1,14}.[0-9]{1,2}$");

    if ($(id).val() != '') {
        if (!v_regex.test(v_text)) {

            $(id).addClass('input-validation-error');
            return false;
        }
        else {

            $(id).removeClass('input-validation-error');
        }
    }
    return true;
}

function SaveRCCHandler() {
    var postData = {
        "ProjectId": $("#Clr_Project_Id").val(),
        "User_Id": $("#rcc_handler").val()
    };
    $.ajax({
        url: '/GCS/ClearanceInbox/SaveRCCHandler',
        type: 'POST',
        data: JSON.stringify(postData),
        dataType: 'json',
        async: 'async',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.Result == 'OK') {
            }
        },
        error: function () {
        }
    });

}

function RemoveClassPriceReduction() {
    if ($("#chkPriceReduction").is(':checked')) {
        $("#cboRequestPriceList").val("--Select--");
        $("#cboRequestPriceList").removeClass('input-validation-error');
    }
    else {
        $("#cboRequestPriceList").val("--Select--");
        $("#cboRequestPriceList").removeClass('input-validation-error');
    }
}

function triggerRegularProjInfoAuditHistory(AuditType) {
    var SelectedItemIds = [];
    var displayTitle;
    SelectedItemIds.push($("#Clr_Project_Id").val());

    if ($("#ProjectRefId").val().length > 0) {
        displayTitle = $("#ProjectRefId").val() + ' - ';
    }
    else {
        displayTitle = '';
    }

    if (AuditType == 'RegularNonTraditionalProjectProjectAuditHistory') {
        displayTitle = displayTitle + 'Project Information';

    }
    else { // For Regular RequestType
        displayTitle = displayTitle + 'Request Type';
    }
    displayAuditTrail(AuditType, SelectedItemIds, displayTitle);
    return false;
}

function wrapChkbox(chkboxId) {
    if (!($(chkboxId).parent().is("label"))) {
        $(chkboxId).wrap(function () {
            return ('<label class="input-validation-error"  style="width:20px; height:18px;" />')
        });
    }
}

function unwrapChkbox(chkboxId) {
    if (($(chkboxId).parent().is("label"))) {
        $(chkboxId).unwrap();
    }
}

function ValidateDecimal(event, ctlid) {

    if (event.shiftKey) {
        return false
    }

    var v_text = $(ctlid).val();
    //Validate the key press event for integer/character/Aplhanuemeric
    if (window.event) { var charCode = window.event.keyCode; }
    else if (event) { var charCode = event.which; }
    else { return true; }
    if ((charCode == 37) || (charCode == 46) || (charCode == 36) || (charCode == 35) || (charCode == 8) || (charCode == 39)) {
        return true;
    }

    if ((v_text.indexOf(".") == -1) && (charCode == 190 || charCode == 110)) {

        return true;
    }
    if ((v_text.indexOf(".") > 0) && (charCode == 190 || charCode == 110)) {
        return false;
    }
    if (v_text.length == 14 && charCode != 190 && charCode != 110) {
        if ((document.selection) && (document.selection.createRange().text.length > 0)) {
            return true;
        }
        if ((document.getSelection) && (document.getSelection.toString().length > 0)) {
            return true;
        }
        return false;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 96 || charCode > 105))
    { return false; }

    if (v_text.indexOf(".") > 0) {

        var charAfterdot = v_text.substring(v_text.indexOf("."), v_text.length - 1);
        if (charAfterdot.length >= 2) {
            if ((document.selection) && (document.selection.createRange().text.length > 0)) {
                return true;
            }
            if ((document.getSelection) && (document.getSelection.toString().length > 0)) {
                return true;
            }
            return false;
        }

    }

    if (v_text.length == 17) {
        return false;
    }
    return true;

}

function currencyFocus() {
    currencyDdwnFocus = 1;
}

function currencyBlur() {
    currencyDdwnFocus = 0;
}

function OnlyNumericReleaseNoofTracks(event, inputType) {
    if (event.shiftKey) {
        return false
    }
    //Validate the key press event for integer/character/Aplhanuemeric
    if (window.event) { var charCode = window.event.keyCode; }
    else if (event) { var charCode = event.which; }
    else { return true; }
    if (inputType == 'i') {//integer validation
        if (!((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105) || (charCode == 8) || (charCode == 46) || (charCode == 37) || (charCode == 39)))
        { return false; }
    }
    return true;
}

function showReleaseResubmitMsg() {
    if ($('#hdnProjectStatusId').val() != undefined) {
        _projectStatus = $('#hdnProjectStatusId').val();
    }
    if (_projectStatus == StatusType.ReOpened.value || _projectStatus == StatusType.Submitted.value || _projectStatus == StatusType.ReSubmitted.value) {

        if ($('#cmbReleaseNewOrExisting').val() == "1") {
            var ReleaseCount = $('#hdnReleaseRowsCount').val();
            var _releaseId = '#hdnReleaseId';
            var seperator = '_';
        }
        else {
            var ReleaseCount = $('#ReleaseModelCount').val();
            var _releaseId = '#ReleaseId';
            var seperator = '-';
        }
        for (var i = 0; i < RegularControlsList.length; i++) {

            if (RegularControlsList[i].indexOf("btnManageTerritories_1") != -1) {
                if (document.getElementById(RegularControlsList[i]) != null) {
                    document.getElementById(RegularControlsList[i]).title = ResubmissionTooltipMsg;
                }
            }

            if (RegularControlsList[i].indexOf("_") == -1
            && RegularControlsList[i].indexOf("-") == -1 && RegularControlsList[i].indexOf("rdoNonTrad") == -1) {
                if (document.getElementById(RegularControlsList[i]) != null) {
                    document.getElementById(RegularControlsList[i]).title = ResubmissionTooltipMsg;
                }
            }
            else {
                if ($('#cmbReleaseNewOrExisting').val() == "1") {
                    var ReleaseCount = $('#hdnReleaseRowsCount').val();
                }
                else {
                    var ReleaseCount = $('#ReleaseModelCount').val();
                }

                for (var j = 0; j < ReleaseCount; j++) {

                    if (document.getElementById(RegularControlsList[i] + j) != null) {

                        document.getElementById(RegularControlsList[i] + j).title = ResubmissionTooltipMsg;
                    }
                    //check for New Release
                    if ($('#cmbReleaseNewOrExisting').val() == "1") {

                        if (document.getElementById("LabelId_" + j) != null && $("#ddlMusicType_" + j).val() == "1") {

                            document.getElementById("labelName_" + j).title = ResubmissionTooltipMsg;
                        }
                        if (document.getElementById("txtNoOfComp_" + j) != null) {
                            document.getElementById("txtNoOfComp_" + j).title = ResubmissionTooltipMsg;
                        }
                    }
                }


            }
        }

    }
}

function showResourceResubmitMsg() {

    if ($('#hdnProjectStatusId').val() != undefined) {
        _projectStatus = $('#hdnProjectStatusId').val();
    }

    if (_projectStatus == StatusType.ReOpened.value || _projectStatus == StatusType.Submitted.value || _projectStatus == StatusType.ReSubmitted.value) {
        var resourceCount = $("#hdnResourceListCount").val();
        for (var j = 0; j < resourceCount; j++) {
            if ($("#hdnClearanceResourceId" + j).val() != 0) {
                document.getElementById("btnManageTerritories_" + (j + 3)).title = ResubmissionTooltipMsg;
                document.getElementById("chkGloballyCleared" + j).title = ResubmissionTooltipMsg;
                if ($("#hdnR2ResourceId" + j).val() == 0) {
                    document.getElementById("BtnManagerArtistFreeHandRegular" + j).title = ResubmissionTooltipMsg;
                    if (document.getElementById("RegularProjectDetails_ClearanceResource_" + j + "__Title") != null)
                        document.getElementById("RegularProjectDetails_ClearanceResource_" + j + "__Title").title = ResubmissionTooltipMsg;
                    document.getElementById("RegularProjectDetails_ClearanceResource_" + j + "__VersionTitle").title = ResubmissionTooltipMsg;
                    document.getElementById("ddlRecordingTypeResourceTab_" + j).title = ResubmissionTooltipMsg;
                    document.getElementById("ddlResourceTypeResourceTab_" + j).title = ResubmissionTooltipMsg;
                    document.getElementById("ddlMusicTypeResourceTab_" + j).title = ResubmissionTooltipMsg;
                    document.getElementById("RegularProjectDetails_ClearanceResource_" + j + "__MusicTime").title = ResubmissionTooltipMsg;
                }
            }
        }
    }
}

function UpdateModifiedRequestTypeRequestSummaryResource(modifiedSalesChannelIds) {
    var flag = true;
    var ResourceCount = $('#hdnRequestSummaryListCount').val();
    for (var j = 0; j < modifiedSalesChannelIds.length; j++) {
        for (var i = 0; i < ResourceCount; i++) {
            if (document.getElementById("hdnSalesChannelId" + i) != null) {
                if (document.getElementById("hdnSalesChannelId" + i).value == modifiedSalesChannelIds[j]) {
                    if (document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 13 && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 5 && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 6
            && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 7 && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 10) {//Id of Rejected, Auto Rejected, Cancelled, SYSTEM CANCEL,
                        document.getElementById("hdnRequestInfoIsRoutingTriggered" + i).value = "true";
                    }
                }
            }
        }
    }
    return flag;
}

$(document).ready(function () {
    $('#cmbReleaseNewOrExisting').live("change", function () {

        if ($(this).val() == "2") {
            $("#FlagReleaseNewOrExisting").val('Exist');
        }
        else if ($(this).val() == "1") {
            $("#FlagReleaseNewOrExisting").val('New');
        }
        else {
            $("#FlagReleaseNewOrExisting").val('');
        }

        $('#loadingDivPA').show();

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/RegularReleaseDropdownChange', digitalVal, function (data) {
            $('#loadingDivPA').hide();

            $('#divPartialUpdate').html(data);

            if ($("#FlagReleaseNewOrExisting").val() == "Exist") {
                if ($('#hdnStatusType').val() == 1) {
                }
                $('#cmbReleaseNewOrExisting').val('2');
                $(function () { $("#tabs").tabs({ selected: 1 }) });
            }
            else {
                if ($('#hdnStatusType').val() == 1) {
                }
                $('#cmbReleaseNewOrExisting').val('1');
                $(function () { $("#tabs").tabs({ selected: 1 }) });
            }

            return false;
        });

    });

    releaseNewOrExist = $('#FlagReleaseNewOrExisting').val();
    $("#rdoExistPrj").attr('checked', true);
    var projectMode = "";
    var IsCancelledRejExists = false;
    var lstCombinedDataList = null;
    var ResourceId = "";

    $('#btnReinstate').prop('disabled', true);
    $('#btnReOpen').prop('disabled', true);
    $('#btnSubmit').prop('disabled', true);
    $('#btnSave').prop('disabled', true);
    $('#btnCancelProject').prop('disabled', true);
    $('#btnComplete').prop('disabled', true);

    $(function () {
        $('#SearchR2ProjectsDialog').dialog({
            autoOpen: false,
            width: 1000,

            resizable: false,
            title: 'Search For R2 Project',
            modal: true,
            close: function () {
                $('#SearchR2ProjectsDialog')[0].innerHTML = "";

            },
            open: function (event, ui) {

                $(this).load('/GCS/Search/ClrR2ProjectsSearch');

                var dialogue = $('.ui-dialog')

                dialogue.animate({

                    top: "40px"

                }, 0);

            }
        });
    });

    $(function () {
        $('#PushToR2Dialog').dialog({
            autoOpen: false,
            width: 450,

            resizable: false,
            title: 'Push To R2',
            modal: true,
            close: function () {
                $('#PushToR2Dialog')[0].innerHTML = "";

            },
            open: function (event, ui) {

                $(this).load('/GCS/Search/ClrPushToR2');

                var dialogue = $('.ui-dialog')

                dialogue.animate({

                    top: "40px"

                }, 0);

            }
        });
    });

    //Condition to select the Cancelled Resource
    function SelectCancelResource() {
        var ckeckBoxList = $('#tblCancelResources').find('input:checkbox');
        for (i = 0; i < ckeckBoxList.length - 1; i++) {
            if (ckeckBoxList[i + 1].checked) {
                if (lstRoutedItems == "") {
                    lstRoutedItems = $('#hdnCancelClearanceProjectRoutedItemId' + i)[0].value;
                }
                else {
                    lstRoutedItems = lstRoutedItems + ',' + $('#hdnCancelClearanceProjectRoutedItemId' + i)[0].value;
                }
            }
        }
    }

    //Condition to select the Rejected Resource
    function SelectRejectResource() {
        var ckeckBoxList = $('#tblRejectResources').find('input:checkbox');
        for (i = 0; i < ckeckBoxList.length - 1; i++) {
            if (ckeckBoxList[i + 1].checked) {
                if (lstRoutedItems == "") {
                    lstRoutedItems = $('#hdnRejectedClearanceProjectRoutedItemId' + i)[0].value;
                }
                else {
                    lstRoutedItems = lstRoutedItems + ',' + $('#hdnRejectedClearanceProjectRoutedItemId' + i)[0].value;
                }
            }
        }
    }

    var lstRoutedItems = ""
    $("#btnCancelRejectSubmit").live("click", function () {
        SelectCancelResource();
        SelectRejectResource();
        $('#divCancelledRejResource').dialog('close');
        IsValidationWithCancelRejectedResourceLevel(lstRoutedItems)
    });

    function IsValidationWithCancelRejectedResourceLevel(routedItems) {
        $('#loadingDivPA').hide();
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        if (routedItems != "") {
            UpdateSelectedCancelledRejectedResource(routedItems);
        }
        // check if all validations are over
        if ($('#RegularValidationLevel').val() == "Project") {
            IsSalesChannelValidationsDone(projectMode);
        }
        else if ($('#RegularValidationLevel').val() == "Resource") {
            IsReleaseTabValidationDone(projectMode);
        }
        else if ($('#RegularValidationLevel').val() == "ReleaseTab") {
            IsReleaseConfigValidationDone(projectMode);
        }
        else if ($('#RegularValidationLevel').val() == "Release") {
            IsSalesChannelValidationsDone(projectMode);
        }
    }

    //Update status flag for Newly added Resource
    function SetFlagFalseForNewlyAddedResources() {

        if (document.getElementById('tblResource') != null) {
            var div = document.getElementById('tblResource');
            for (i = 0; i < div.children.length; i++) {
                hdnId = "hdnIsNewAdded" + i;
                $('#' + hdnId).val('false');
            }
        }

        if (document.getElementById('tblRelease') != null) {
            var div = document.getElementById('tblRelease');
            for (i = 0; i < div.children.length; i++) {
                hdnId = "hdnIsNewReleaseAdded" + i;
                $('#' + hdnId).val('false');
            }
        }
        $('#hdnAdditionalResourceCheck').val("NO");
    }

    //Update status flag for Newly added Resource
    function GetFlagForNewlyAddedResources() {

        var flagAdd = false;

        if (lstCombinedDataList[3].length > 0) {
            if (lstCombinedDataList[3][0] < 0) {
                flagAdd = true;
            }
        }

        if (document.getElementById('tblResource') != null) {
            var div = document.getElementById('tblResource');
            for (i = 0; i < div.children.length; i++) {
                if ($("#hdnIsNewAdded" + i).val() == "True" && $("#hdnArchiveFlagRegular" + i).val() != "Y")
                    flagAdd = true;
            }
        }

        if ($('#FlagReleaseNewOrExisting').val() == "New") {
            if (document.getElementById('tblRelease') != null) {
                var div = document.getElementById('tblRelease');
                for (i = 0; i < div.children.length; i++) {
                    // for new release, if ddlExistingRelease_+i is Yes then only show additional resource message, otherwise it will just save that release & resubmission will not trigger.
                    // for existing release, always show additional resource msg
                    if ($("#hdnIsNewReleaseAdded" + i).val() == "True" && $("#hdnArchiveFlag" + i).val() != "Y" && $('#ddlExistingRelease_' + i + ' :selected').text() == 'Yes')
                        flagAdd = true;
                }
            }
        }
        else {
            if (document.getElementById('tblRelease') != null) {
                var div = document.getElementById('tblRelease');
                for (i = 0; i < div.children.length; i++) {
                    // for new release, if ddlExistingRelease_+i is Yes then only show additional resource message, otherwise it will just save that release & resubmission will not trigger.
                    // for existing release, always show additional resource msg
                    if ($("#hdnIsNewReleaseAdded" + i).val() == "True" && $("#Archive" + i).val() != "Y")
                        flagAdd = true;
                }
            }
        }
        return flagAdd;
    }


    //Update only the IsRoutingTrigger flag for Cancelled and Rejected Resource
    function UpdateSelectedCancelledRejectedResource(routedItems) {
        var routedItemList = routedItems.split(',');
        for (var i = 0; i < routedItemList.length; i++) {
            if (routedItemList[i] > 0) {
                var table = document.getElementById('tblRequestInfoSummary'),
                   rows = table.getElementsByTagName('tr');
                for (var rowi = 0; rowi < rows.length; rowi++) {
                    if (document.getElementById("hdnRequestInfoRoutedItemId" + rowi) != null) {
                        if (document.getElementById("hdnRequestInfoRoutedItemId" + rowi).value == routedItemList[i]) {
                            if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 5) || (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 7)
                             || document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 10 || document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 6) {
                                document.getElementById("hdnRequestInfoIsRoutingTriggered" + rowi).value = "true";
                            }

                        }
                    }
                }
            }
        }
    }

    // document.ready ends here
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

    $("#btnPushToR2").live("click", function () {

        // if linking has not been done before
        var digitalVal = $('#CreateRegularProjectForm').serialize();
        if ($('#R2_Project_Id').val() == 0 || $('#R2_Project_Id').val() == "") {
            // Ajax call for athorization check
            $('#loadingDivPA').show();
            $.post('/GCS/ClearanceProject/PermissionPushToR2FirstPush', digitalVal, function (data) {
                $('#loadingDivPA').hide();

                if (data.indexOf("Valid") >= 0) {
                    // 2.	[UC013-0385]If the third party company is free hand
                    // a.	[UC013-0390]The clearance system does not allow the first push to R2
                    // fetch comp Id from controller

                    if (data.indexOf("freehand") >= 0) {
                        $('#divErrorMsgPushToR2').show();
                        $('#divErrorMsgPushToR2').text(freeHandCompanyMsg);
                        return;
                    }
                    $('#PushToR2Dialog').dialog('open');
                }
                else {
                    $('#divErrorMsgPushToR2').show();
                    $('#divErrorMsgPushToR2').text(authorizedR2UserMsgPush);
                    return;
                }

            });

        }
        else {

            $('#loadingDivPA').show();
            $.post('/GCS/ClearanceProject/PermissionPushToR2SubsequentPush', digitalVal, function (data) {
                $('#loadingDivPA').hide();

                if (data == true) {
                    pushNewReleasesToR2();
                }
                else {
                    $('#divErrorMsgPushToR2').show();
                    $('#divErrorMsgPushToR2').text(authorizedR2UserMsgSubsequent);
                    return;
                }

            });

        }

    });

    $("#SubmitPush").live("click", function () {
        // check which option has been selected, store this in hidden variable
        // reset collection, being used to search project
        $('#searchProjectListPush').val("");
        $('#searchR2ProjectCode').val("");

        if ($('#hdnSearchForR2Project').val() == "1" || $('#hdnSearchForR2Project').val() == "2") {
            $('#loadingDivPA').show();
            // added by dhruv for Ajax call
            if ($('#hdnSearchForR2Project').val() == "1") {
                $.post('/GCS/ClearanceProject/GetAuthorizationsForR2Search', function (data) {
                    $('#loadingDivPA').hide();
                    if (data == true) {
                        $('#PushToR2Dialog').dialog('close');
                        $('#SearchR2ProjectsDialog').dialog('open');
                    }
                    else {
                        $('#PushToR2Dialog').dialog('close');
                        $('#divErrorMsgPushToR2').show();
                        $('#divErrorMsgPushToR2').text(authorizedUserSearchMsgPush);
                        return;
                    }

                });
            }
            else {
                $.post('/GCS/ClearanceProject/GetAuthorizationsForR2Create', function (data) {
                    $('#loadingDivPA').hide();
                    if (data == true) {
                        $('#PushToR2Dialog').dialog('close');
                        $('#SearchR2ProjectsDialog').dialog('open');
                    }
                    else {
                        $('#PushToR2Dialog').dialog('close');
                        $('#divErrorMsgPushToR2').show();
                        $('#divErrorMsgPushToR2').text(authorizedUserMsgPush);
                        return;
                    }
                });
            }

        }

    });

    $("#CancelPush").live("click", function () {
        $('#PushToR2Dialog').dialog('close');

    });

    $("#rdoExistPrj").live("click", function () {

        $('#hdnSearchForR2Project').val("1");
    });

    $("#rdoNewPrj").live("click", function () {

        $('#hdnSearchForR2Project').val("2");

    });

    $("#btnSave").live("click", function () {

        if ($('#loadingDivPA').is(':visible')) {
            return;
        }

        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        $('#hdncommand').val("Save");
        if ((!$("#chkRegularRetail").is(':checked')) &&
            (!$("#chkClub").is(':checked')) &&
            (!$("#chkNonTrditional").is(':checked')) &&
            (!$("#chkPromotional").is(':checked'))) {

            $("#chkRegularRetail").attr('checked', true);

        }

        // added by dhruv for Ajax call
        if ($('#hdnRegularProjectStatusId').val() == 1) {
            PerformSavingUnsubmittedRegularProject();
            return;
        }
    });

    /////////////////Saving UnSubmitted project////////////////////////////////////////////////////////
    function PerformSavingUnsubmittedRegularProject() {
        var flag = false;
        if (ValidationCheckOnRegularUnsubmitted() == false) {

            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>No details are entered for the project. Do you want to save the blank project?</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: regularProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 400
            });

            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');

                        SaveRegularProject();

                    },
                    'No': function () {
                        $(this).dialog('close');

                    }
                }
            });
        }
        else {

            SaveRegularProject();

        }
        return flag;
    }

    //******************** Section For Refreshing Project Data- START****************


    function SaveRegularProject() {

        $('#loadingDivPA').show();
        var digitalVal = $('#CreateRegularProjectForm').serialize();

        $.post('/GCS/ClearanceProject/SaveRegularProject', digitalVal, function (data) {

            if (data.Result == "ERROR") {
                ShowErrorMessage();
                return false;
            }

            RefershProjectData(data.Records);
            ShowMessage();
        });

    }

    function RefershProjectData(model) {

        FillProjectInformationTab(model);

        if (model.RegularProjectDetails.ReleaseNewOrExisting == "New") {

            FillNewReleaseTab(model);

            FillResourceTab(model);

        }
        else {

            FillExistingReleaseTab(model);
        }

    }

    function FillProjectInformationTab(model) {

        $('#txtProjectReferenceId').val(model.RegularProjectDetails.ProjectReferenceId); // All two are same below
        $('#ProjectRefId').val(model.RegularProjectDetails.ProjectReferenceId);
        $('#lblprojectreferencenumber').text(model.RegularProjectDetails.ProjectReferenceId);
        $('#Clr_Project_Id').val(model.RegularProjectDetails.ClrProjectId);
        $('#hdnrequesterWorkgroupId').val(model.RegularProjectDetails.RequesterWorkgroupId);
        $('#hdnRegularProjectStatusId').val(model.RegularProjectDetails.StatusType); // All three are same below
        $('#hdnStatusType').val(model.RegularProjectDetails.StatusType);
        $('#hdnProjectStatusId').val(model.RegularProjectDetails.StatusType);
        $('#R2_Project_Id').val(model.RegularProjectDetails.R2_Project_Id);
        $('#hdnCompId').val(model.CompanyDetails.Id);
        $('#hdnCompName').val(model.CompanyDetails.Name);
        $('#txtThirdPartyCompISAC').val(model.RegularProjectDetails.thirdPartyCompanyDetails.ISACCode); // All two are same below
        $('#txtThirdPartyCompCountry').val(model.RegularProjectDetails.thirdPartyCompanyDetails.CountryName);
        $('#hdnThirdPartyComapnyISAC').val(model.RegularProjectDetails.thirdPartyCompanyDetails.ISACCode); // All two are same below
        $('#hdnThirdPartyComapnyCountry').val(model.RegularProjectDetails.thirdPartyCompanyDetails.CountryName);
        $('#txtThirdPartyCompName').val(model.RegularProjectDetails.ThirdPartyCompanyName);
        $('#txtThirdPartyCompId').val(model.RegularProjectDetails.ThirdPartyCompanyID);
        $('#hdnRoleGroup').val(model.roleGroupName);

        // All left variables
        //** isInMaintainMode
        //** ProjectModifiedDate
        //** ReleaseNewOrExisting
        //** IsSensitiveDataChanged
        //** ScopeAndRequestType.newlyAddedSalesChannelsAfterSubmit

    }

    function FillNewReleaseTab(model) {

        if (model.RegularProjectDetails.ObjRelease != undefined) {

            var CountOfReleaseOnClient = $("input[id^='RemovedPackageReleases']").length;
            var ReleaseIndex = 0;

            for (var i = 0; i < CountOfReleaseOnClient; i++) {

                if ($('#hdnArchiveFlag' + i).val() == 'Y') {
                    continue;
                }

                $('#hdnReleaseId' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].ReleaseId);
                $('#hdnArchiveFlag' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].Archive_Flag);
                $('#hdnClr_Project_Id' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].Clr_Project_Id);
                $('#ExistingReleasesUPC' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].ExistingReleases);
                $('#RemovedPackageReleases' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].RemovedPackageReleases);
                $('#hdnIsNewReleaseAdded' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].IsNewlyAddedAfterSubmit);
                $('#hdnIsReleaseRouted' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].IsRouted);
                $('#hdnConfigId' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].ConfigIdSelected);
                $('#hdnUpcNumber' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].Upc);

                ReleaseIndex++;
            }

            $('#hdnReleaseRowsCount').val(model.RegularProjectDetails.ObjRelease.length);


            // All left variables
            //** isPushedToR2
            //** Is_Upc_Manual
            //** ClrProjectReleaseIds--- only used in Audit history
            //** ArtistInfo
            //** SuggestedFee_Non
            //** ICLA_Non
            //** LabelId
        }

    }

    function FillResourceTab(model) {

        if (model.RegularProjectDetails.ClearanceResource != undefined) {

            var CountOfResourceOnClient = $("input[id^='hdnIsResourceRouted']").length;
            var ResourceIndex = 0;

            for (var i = 0; i < CountOfResourceOnClient; i++) {

                if ($('#hdnArchiveFlagRegular' + i).val() == 'Y') {
                    continue;
                }

                $('#hdnRegularResourceArtistName' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].ArtistName);
                $('#hdnIsNewAdded' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].IsNewlyAddedAfterSubmit);
                $('#hdnIsResourceRouted' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].IsRouted);
                $('#hdnR2ApprovedFreeHandResource' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].FreeHandResourceStatus);
                $('#hdnClearanceResourceIdToUpdate' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].ResourceIdToUpdate);
                $('#hdnreplacefreehandresc' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].ReplaceFreeHandFlag);
                $('#hdnArchiveFlagRegular' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].ArchiveFlag);
                $('#hdnClearanceResourceId' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].ClearanceResourceId);
                $('#ResourceId' + i).val(model.RegularProjectDetails.ClearanceResource[ResourceIndex].ResourceId);

                ResourceIndex++;

            }

            $('#hdnResourceListCount').val(model.RegularProjectDetails.ClearanceResource.length);
            // All left variables
            //** ArtistInfo
            //** RecordingTypeDesc
            //** ResourceTypeDesc
            //** MusicTypeDesc
            //** MusicTime
            //** Isrc
            //** VersionTitle
            //** Title
            //** ExcerptTime
            //** SuggestedFee
            //** IsGloballyCleared
            //** Comments
            //** R2_ResourceId
            //** AdminCompanyId

        }
    }

    function FillExistingReleaseTab(model) {

        if (model.RegularProjectDetails.ObjRelease != undefined) {

            var CountOfReleaseOnClient = $("input[id^='R2ReleaseId']").length;
            var ReleaseIndex = 0;

            for (var i = 0; i < CountOfReleaseOnClient; i++) {

                if ($('#Archive' + i).val() == 'Y') {
                    continue;
                }

                $('#ReleaseId' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].ReleaseId);
                $('#Archive' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].Archive_Flag);
                $('#ClrProjectId' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].Clr_Project_Id);
                $('#hdnIsNewReleaseAdded' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].IsNewlyAddedAfterSubmit);
                $('#hdnIsReleaseRouted' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].IsRouted);
                $('#hdnartistName' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].ArtistName); // Below two are same
                $('#hdnArtistName-release' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].ArtistName);
                $('#hdnPackage-release' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].PackageIndicator);
                $('#lblMusicType-release' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].MusicType_Desc);
                $('#hdnMusicType-release' + i).val(model.RegularProjectDetails.ObjRelease[ReleaseIndex].MusicType_Id);

                ReleaseIndex++;
            }

            $('#ReleaseModelCount').val(model.RegularProjectDetails.ObjRelease.length);

            // All left variables
            //** R2ReleaseId
            //** Account_Id
            //** Created_Dttm
            //** Created_User
            //** Is_Non_Traditional
            //** Is_Price_Reduction
            //** Is_Promotional
            //** Is_Regular_Retail
            //** Is_TV
            //** Is_Club
            //** ReleaseTitle-- used two times as hidden field
            //** ClrProjectReleaseIds--- only used in Audit history
            //** ArtistInfo
            //** SoundtrackIndicator
            //** Is_Ost
            //** LabelId
            //** labelName
            //** ConfigurationDisplay
            //** Configuration
            //** TrackCount
            //** ComponentCount
            //** ICLA_Non
            //** SuggestedFee_Non

        }

    }

    function ShowErrorMessage() {

        $('#divErrorMessage').html("Error in Saving Project").show();
        $('#divErrorMessage').addClass('error msg-margin');
        $('#divMessage').hide();
        $('#loadingDivPA').hide();
    }
    function ShowMessage() {

        $('#divMessage').html("Project Saved Successfully").show();
        $('#divErrorMessage').hide();
        $('#loadingDivPA').hide();
    }
    //******************** Section For Refreshing Project Data- END****************

    // button used for Submit project and  resubmit project when opened in Unsubmitted ,Submitted & Resubmitted State
    $("#btnSubmit").live("click", function () {
        if ($('#loadingDivPA').is(':visible')) {
            return;
        }

        if ($(".ui-dialog").is(":visible"))
            return;

        var IsValid = false;
        var ispriceValidation = false;
        globalPostBackCheck = true;

        // validations check
        IsValid = ValidationForRegularProject();

        if (IsValid == false) {
            return false;
        }

        ispriceValidation = ValidationOnMoneyFields();

        if (ispriceValidation == false) {

            return false;
        }
        var statusType = $('#hdnStatusType').val();
        var statusCommand;
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        // Set command based on Current status
        if (statusType == 1) {   // UnSubmitted then Submitted
            $('#hdncommand').val("Submitted"); // Set commmand in model.RegularProjectDetails.Command
            statusCommand = "Submitted";
        }
        else if (statusType == 2) {   // Submitted then ReSubmitted
            statusCommand = "ReSubmitted"; // Set commmand in model.RegularProjectDetails.Command
        }
        else if (statusType == 5) {   // Resubmitted then ReSubmitted
            $('#hdncommand').val("ReSubmitted");  // Set commmand in model.RegularProjectDetails.Command
            statusCommand = "ReSubmitted";
        }
        projectMode = "submit";

        if (statusCommand == "ReSubmitted") {
            RegularProjectSubmissionValidations("submit");
        }
        else {
            SubmitRegularProject();
        }
    });

    function SubmitReopenedRegularProject() {
        globalPostBackCheck = true;
        // Saving Data
        // added by dhruv for Ajax call
        $('#loadingDivPA').show();
        // added by dhruv for Ajax call

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/ReSubmitRegularProject', digitalVal, function (data) {
            $('#loadingDivPA').hide();
            if (data.Error) {
                $('#divErrorMessage').html(data.Message);
                $('#divErrorMessage').show();
                $('#divErrorMessage').addClass('error msg-margin');
                return false;
            }
            var projectid = $(data).find('#Clr_Project_Id').val();
            var statustype = $(data).find('#hdnStatusType').val();

            var popup = window.opener;
            if (popup) {
                var _roleGroup = popup._activeRoleGroup;
                if (_roleGroup == undefined) {
                    _roleGroup = '';
                }
                window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype + '&ActiveRoleGroup=' + _roleGroup;
            }
            else {
                window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype;
            }
        });
    }

    function SubmitRegularProject() {
        // Saving Data
        // added by dhruv for Ajax call
        globalPostBackCheck = true;
        $('#loadingDivPA').show();

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/SubmitRegularProject', digitalVal, function (data) {

            if (data.Result == "ERROR") {
                $('#divErrorMessage').html(data.Message);
                $('#divErrorMessage').show();
                $('#divErrorMessage').addClass('error msg-margin');
                $('#loadingDivPA').hide();
                return false;
            }
            var projectid = data.Records.RegularProjectDetails.ClrProjectId;
            var statustype = data.Records.RegularProjectDetails.StatusType;
            $('#loadingDivPA').hide();
            var popup = window.opener;
            if (popup) {
                var _roleGroup = popup._activeRoleGroup;
                if (_roleGroup == undefined) {
                    _roleGroup = '';
                }
                window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype + '&ActiveRoleGroup=' + _roleGroup;
            }
            else {
                window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype;
            }
        });

    }

    function IsResourceValidationsDone(input) {
        var flag = false;
        $('#RegularValidationLevel').val("Resource");
        var digitalVal = $('#CreateRegularProjectForm').serialize();
        if (lstCombinedDataList[5] != null && lstCombinedDataList[5].length > 0)
            flag = UpdateRoutingTriggerFlag(lstCombinedDataList[5]);

        if (flag) {//If there is change in Resoruce fields
            var objWarningDialog = $('<div id="Confirm"></div>')
                .html('<p>' + ResubmissionResourceEditmsg + '</p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    title: regularProjectMessages.confim,
                    show: 'clip',
                    hide: 'clip',
                    width: 300
                });
            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        IsResubmissionTrigerred = 1;
                        var value =
                                    {
                                        "Isrc": ResourceId,
                                        "Id": $('#Clr_Project_Id').val()
                                    };
                        if (IsCancelledRejExists == true) {
                            $('#RegularValidationLevel').val("Resource");
                            $('#loadingDivPA').show();
                            $.get('/GCS/ClearanceProject/GetCancelRejectedResourceRegular', value, function (data) {
                                IsValidationWithCancelRejectedResourceLevel(data.listfinal);
                            });
                        }
                        else {
                            // continue with saving the project
                            IsReleaseTabValidationDone(input);
                        }
                    },
                    'No': function () {
                        $(this).dialog('close');
                        UpdateFalseRequestSummaryResource();
                        //code for discard amendment at project level
                        $('#loadingDivPA').show();
                        $.post('/GCS/ClearanceProject/DiscardAllAmendments', digitalVal, function (data) {
                            $('#loadingDivPA').hide();
                            $('#divPartialUpdate').html(data);
                        });
                    }
                }
            });
        }
        else {//If there is change in other fields of resource
            //Update the IsRoutingTrigger flag for all the resource
            flag = UpdateRoutingTriggerFlag(lstCombinedDataList[6]);
            if (flag) {//If there si change in Resoruce fields
                var objWarningDialog = $('<div id="Confirm"></div>')
                    .html('<p>' + ResubmissionResourceOtherFieldEditmsg + '</p>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        title: regularProjectMessages.confim,
                        show: 'clip',
                        hide: 'clip',
                        width: 300
                    });
                objWarningDialog.dialog('open');
                objWarningDialog.dialog({
                    buttons:
                    {
                        'Yes': function (e) {
                            $(this).dialog('close');
                            IsResubmissionTrigerred = 1;
                            var value =
                                        {
                                            "Isrc": ResourceId,
                                            "Id": $('#Clr_Project_Id').val()
                                        };
                            if (IsCancelledRejExists == true) {
                                $('#RegularValidationLevel').val("Resource");
                                $('#loadingDivPA').show();
                                $.get('/GCS/ClearanceProject/GetCancelRejectedResourceRegular', value, function (data) {
                                    IsValidationWithCancelRejectedResourceLevel(data.listfinal);
                                });
                            }
                            else {
                                // continue with saving the data
                                IsReleaseTabValidationDone(input);
                            }
                        },
                        'No': function () {
                            $(this).dialog('close');
                            UpdateFalseRequestSummaryResource();
                            $('#hdnAdditionalResourceCheck').val("NO");
                            // continue with saving the data
                            IsReleaseTabValidationDone(input);
                        }
                    }
                });
            }
            else {
                // continue with saving the data
                IsReleaseTabValidationDone(input);
            }
        }
    }

    function ApplyResubmittionReasons(reasonProjectResubmission, reasonReleaseResubmission, reasonResourceResubmission) {

        $('#hdnResubmitReasonComments').val(reasonProjectResubmission);

        var hiddenResourceVariable = 'hdnResourceResubmitReasonComments_';
        $('[id^=' + hiddenResourceVariable + ']').val('');
        for (var i = 0; i < reasonResourceResubmission.length; i++) {
            $('#' + hiddenResourceVariable + reasonResourceResubmission[i].Key).val(reasonResourceResubmission[i].Value);
        }

        var hiddenReleaseVariable = 'hdnReleaseResubmitReasonComments_';
        $('[id^=' + hiddenReleaseVariable + ']').val('');
        for (var i = 0; i < reasonReleaseResubmission.length; i++) {
            $('#' + hiddenReleaseVariable + reasonReleaseResubmission[i].Key).val(reasonReleaseResubmission[i].Value);
        }

    }

    function IsProjectValidationsDone(input) {
        $('#hdnPackageRoutingCheck').val("YES");
        var RequestSummaryCount = $('#hdnRequestSummaryListCount').val();
        if (RequestSummaryCount <= 0 && (document.getElementById('hdnProjWithNoResources').value != "true"))
            return false;

        $('#hdnAdditionalResourceCheck').val("YES");
        var flag = false;
        $('#RegularValidationLevel').val("Project");
        UpdateFalseRequestSummaryResource();
        var digitalVal = $('#CreateRegularProjectForm').serialize();
        $.post('/GCS/ClearanceProject/GetNewRegularProjectModel', digitalVal, function (data) {
            $('#loadingDivPA').hide();
            ApplyResubmittionReasons(data.ReasonProjectResubmissionToReturn, data.ReasonReleaseResubmission, data.ReasonResourceResubmission)
            lstCombinedDataList = data.LstCombinedDataList;
            modifiedSalesChannelIds = data.ModifiedSalesChannelIds;
            if (lstCombinedDataList != null && lstCombinedDataList.length > 0 && lstCombinedDataList[4][0] == 0) {
                var objWarningDialog = $('<div id="Confirm"></div>')
                    .html('<p>' + ResubmissionProjectLevelMsg + '</p>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        title: regularProjectMessages.confim,
                        show: 'clip',
                        hide: 'clip',
                        width: 300
                    });
                objWarningDialog.dialog('open');
                objWarningDialog.dialog({
                    buttons:
                    {
                        'Yes': function (e) {
                            $(this).dialog('close');
                            IsResubmissionTrigerred = 1;
                            flag = true;
                            $('#IsSensitiveDataChanged').val("true"); //Set the Hidden field value is true if there is project level change

                            if (modifiedSalesChannelIds.length > 9 || jQuery.inArray(1, modifiedSalesChannelIds) ||
                                    jQuery.inArray(2, modifiedSalesChannelIds)) {
                                UpdateAllRequestSummaryResource(); //Set the IsRoutingTrigger flag to true for all the Resources where Request type status is Approved ,Conditonally approved and Waiting
                            }
                            else {
                                UpdateModifiedRequestTypeRequestSummaryResource(modifiedSalesChannelIds);
                            }
                            updateProjectStatus();
                            //If there exist cancelled and Rejected resoruces for the project
                            var value =
                                        {
                                            "Isrc": data
                                        };
                            UpdateRoutingTriggerFlagProjectLevel();
                            if (IsCancelledRejExists == true) {
                                var objWarningDialog = $('<div id="Confirm"></div>')
                                    .html('<p>' + ResubmissionCancelledResourceEditmsg + '</p>')
                                    .dialog({
                                        autoOpen: false,
                                        modal: true,
                                        title: regularProjectMessages.confim,
                                        show: 'clip',
                                        hide: 'clip',
                                        width: 300
                                    });
                                objWarningDialog.dialog('open');
                                objWarningDialog.dialog({
                                    buttons:
                                    {
                                        'Yes': function (e) {
                                            $(this).dialog('close');
                                            $('#RegularValidationLevel').val("Project");
                                            var value =
                                                        {
                                                            "Isrc": "",
                                                            "Id": $('#Clr_Project_Id').val()
                                                        };
                                            var objDialog = $('#divCancelledRejResource');
                                            objDialog.dialog({
                                                resizable: false,
                                                width: 900,
                                                title: "Resubmit Projects",
                                                modal: true,
                                                open: function () {
                                                    $(this).load('/GCS/ClearanceProject/GetCancelRejectedResourceRegularForSelection', value);
                                                    $(this).height($(window).height() - 150);
                                                }
                                            });
                                            objDialog.dialog('open');
                                        },
                                        'No': function () {
                                            $(this).dialog('close');
                                            IsSalesChannelValidationsDone(input);
                                        }
                                    }
                                });
                            }
                            else { //If there does not exist Cancel and Rejected Resource in project
                                // continue with saving data
                                IsSalesChannelValidationsDone(input);
                            }
                        },
                        'No': function () {
                            $(this).dialog('close');
                            $('#loadingDivPA').show();
                            $.post('/GCS/ClearanceProject/DiscardAllAmendments', digitalVal, function (data) {
                                $('#loadingDivPA').hide();
                                $('#divPartialUpdate').html(data);
                            });
                        }
                    }
                });
            }
            else {
                IsResourceValidationsDone(input);
            }
        });
    }

    function IsReleaseConfigValidationDone(input) {

        var flag = false;
        $('#RegularValidationLevel').val("Release");

        var digitalVal = $('#CreateRegularProjectForm').serialize();


        if (lstCombinedDataList[8] != null && lstCombinedDataList[8].length > 0) {
            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionConfigurationEditmsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: regularProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 300
            });

            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        IsResubmissionTrigerred = 1;
                        $('#IsSensitiveDataChanged').val("true"); //Set the Hidden field value is true if there is project level change

                        UpdateAllRequestSummaryResource(); //Set the IsRoutingTrigger flag to true for all the Resources where Request type status is Approved ,Conditonally approved and Waiting
                        updateProjectStatus();

                        UpdateRoutingTriggerFlagProjectLevel();

                        if (IsCancelledRejExists == true) {
                            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionCancelledResourceEditmsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: regularProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 300
            });

                            objWarningDialog.dialog('open');
                            objWarningDialog.dialog({
                                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');

                        var value =
                                        {
                                            "Isrc": "",
                                            "Id": $('#Clr_Project_Id').val()
                                        };

                        $('#RegularValidationLevel').val("Release");
                        var objDialog = $('#divCancelledRejResource');
                        objDialog.dialog({
                            resizable: false,
                            width: 900,
                            title: "Resubmit Projects",
                            modal: true,
                            open: function () {
                                $(this).load('/GCS/ClearanceProject/GetCancelRejectedResourceRegularForSelection', value);
                                $(this).height($(window).height() - 150);
                            }

                        });
                        objDialog.dialog('open');
                    },
                    'No': function () {
                        $(this).dialog('close');
                        //resubmit project and trigger routing
                        IsSalesChannelValidationsDone(input);
                    }
                }
                            });

                        }
                        else {
                            //resubmit project and trigger routing
                            IsSalesChannelValidationsDone(input);
                        }
                    },
                    'No': function () {
                        $(this).dialog('close');
                        // save configuration change only
                        IsSalesChannelValidationsDone(input);
                    }
                }
            });

        }
        else {
            IsSalesChannelValidationsDone(input);
        }
    }

    function IsReleaseTabValidationDone(input) {

        var flag = false;
        $('#RegularValidationLevel').val("ReleaseTab");

        var digitalVal = $('#CreateRegularProjectForm').serialize();


        flag = UpdateRequestsFlagForRelease(lstCombinedDataList[7]);
        if (flag) {
            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionFieldsEditMsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: regularProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 300
            });

            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        $('#IsSensitiveDataChanged').val("true"); //Set the Hidden field value is true if there is project level change
                        IsResubmissionTrigerred = 1;
                        var value =
                                        {
                                            "Isrc": ResourceId,
                                            "Id": $('#Clr_Project_Id').val()
                                        };

                        if (IsCancelledRejExists == true) {
                            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionCancelledResourceEditmsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: regularProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 300
            });

                            objWarningDialog.dialog('open');
                            objWarningDialog.dialog({
                                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');

                        $('#RegularValidationLevel').val("ReleaseTab");
                        var objDialog = $('#divCancelledRejResource');
                        objDialog.dialog({
                            resizable: false,
                            width: 900,
                            title: "Resubmit Projects",
                            modal: true,
                            open: function () {
                                $(this).load('/GCS/ClearanceProject/GetCancelRejectedResourceForRelease', value);
                            }

                        });
                        objDialog.dialog('open');
                    },
                    'No': function () {
                        $(this).dialog('close');
                        //resubmit project and trigger routing
                        IsReleaseConfigValidationDone(input);
                    }
                }
                            });

                        }
                        else {
                            //resubmit project and trigger routing
                            IsReleaseConfigValidationDone(input);
                        }
                    },
                    'No': function () {
                        $(this).dialog('close');
                        $('#loadingDivPA').show();
                        $.post('/GCS/ClearanceProject/DiscardAllAmendments', digitalVal, function (data) {
                            $('#loadingDivPA').hide();

                            $('#divPartialUpdate').html(data);

                        });
                    }
                }
            });


        }
        else {
            IsReleaseConfigValidationDone(input);
        }
    }

    function IsSalesChannelValidationsDone(input) {
        $('#RegularValidationLevel').val("SalesChannel");

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        // d.	[UC011A-0444.3]If the requestor has changed the sales channel or request type in a regular/non-traditional project


        flag = UpdateSalesChannelsFlag(lstCombinedDataList);
        if (flag) {
            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionSalesChannelsEditmsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: regularProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 300
            });

            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        IsResubmissionTrigerred = 1;
                        // continue with saving the project
                        IsPackageValidationsDone(input);
                    },
                    'No': function () {
                        $(this).dialog('close');
                        $('#loadingDivPA').show();
                        $.post('/GCS/ClearanceProject/DiscardAllAmendments', digitalVal, function (data) {
                            $('#loadingDivPA').hide();

                            $('#divPartialUpdate').html(data);

                        });
                    }
                }
            });

        }
        else {
            IsPackageValidationsDone(input);
        }
    }

    function IsNewResourceAddedToProject(input) {

        // i.	[UC011B-0125]The clearance system displays a message “additional resources have been saved to the project.
        // Do you wish to submit?”(Yes/No)
        var flagAdd = GetFlagForNewlyAddedResources();
        if (flagAdd) {
            var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionNewResourceReleaseAddMsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: regularProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 300
            });

            objWarningDialog.dialog('open');
            objWarningDialog.dialog({
                buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        IsResubmissionTrigerred = 1;
                        $('#hdnAdditionalResourceCheck').val("YES");


                        if (input == "submit")
                            SubmitRegularProject();
                        else
                            SubmitReopenedRegularProject();
                    },
                    'No': function () {
                        $(this).dialog('close');
                        SetFlagFalseForNewlyAddedResources();
                        $('#hdnPackageRoutingCheck').val("NO");

                        if (IsResubmissionTrigerred == 1)
                            $('#hdnAdditionalResourceCheck').val("YES");
                        else
                            $('#hdnAdditionalResourceCheck').val("NO");

                        if (input == "submit")
                            SubmitRegularProject();
                        else
                            SubmitReopenedRegularProject();
                    }
                }
            });
        }
        else {

            if (IsResubmissionTrigerred == 1)
                $('#hdnAdditionalResourceCheck').val("YES");
            else
                $('#hdnAdditionalResourceCheck').val("NO");

            if (input == "submit")
                SubmitRegularProject();
            else
                SubmitReopenedRegularProject();
        }

    }

    function IsPackageValidationsDone(input) {
        var flag = false;

        $('#RegularValidationLevel').val("Package");

        var digitalVal = $('#CreateRegularProjectForm').serialize();
        // d.	[UC011A-0444.3]If the requestor has changed the sales channel or request type in a regular/non-traditional project

        flag = UpdatePackageRemoveFlag(lstCombinedDataList, input);
    }

    //Update the IsRouting Trigger flag for All the resource where status of Request is not Cancelled and Rejected
    function RegularProjectSubmissionValidations(input) {
        // dialog will come for rejected/cancelled requests
        $('#loadingDivPA').show();
        $.ajax({
            url: '/GCS/ClearanceProject/GetRequestsummaryData/',
            type: 'POST',
            async: true,
            data: { ClrProjectId: $('#Clr_Project_Id').val().toString() },
            success: function (data) {
                if (data.Result == "ERROR") {
                    $('#loadingDivPA').hide();
                    alert("Error on Request summary tab");
                    return false;
                }
                else if (data.Records.length == 0 && (document.getElementById('hdnProjWithNoResources').value != "true")) {
                    $('#loadingDivPA').hide();
                    alert("No data on Request summary tab");
                    return false;
                }
                if (data.Records.length > 0) {
                    var stringtext = "";
                    var counter = 0;
                    for (i = 0; i < data.Records.length; i++) {
                        stringtext += "<tr><td>";
                        stringtext += "<input  id='hdnSalesChannelId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].SalesChannelId' type='hidden' value=" + data.Records[i].SalesChannelId + " />";
                        stringtext += "<input  id='hdnRSRequestlId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestId' type='hidden' value=" + data.Records[i].RequestId + " />";
                        stringtext += "<input  id='hdnIsRoutingStop" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingStop' type='hidden' value=" + data.Records[i].IsRoutingStop + " />";
                        stringtext += "<input  id='hdnRequestInfoReleaseId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ReleaseId' type='hidden' value=" + data.Records[i].ReleaseId + " />";
                        stringtext += "<input  id='hdnRequestInfoPackageId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].PackageId' type='hidden' value=" + data.Records[i].PackageId + " />";
                        stringtext += "<input  id='hdnRequestInfoClearanceResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[0].ClearanceProjectResourceId[" + i + "].ClearanceProjectResourceId' type='hidden' value=" + data.Records[i].ClearanceProjectResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceId' type='hidden' value=" + data.Records[i].ResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestTypeId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestTypeId' type='hidden' value=" + data.Records[i].RequestTypeId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestStatusId" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatusId' type='hidden' value=" + data.Records[i].ApprovalStatusId + " />";
                        stringtext += "<input  id='hdnRequestInfoIsRoutingTriggered" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingTriggered' type='hidden' value=" + data.Records[i].IsRoutingTriggered + " />  ";
                        stringtext += "<input  id='hdnRequestInfoRoutedItemId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RoutedItemId' type='hidden' value=" + data.Records[i].RoutedItemId + " /> ";
                        stringtext += "<input id='hdnRequestInfoUpc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Upc' type='hidden' value=" + $.trim(data.Records[i].Upc) + " />";
                        stringtext += "<input id='hdnRequestInfoResourceTitle" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceTitle' type='hidden' value=" + encodeURIComponent($.trim(data.Records[i].ResourceTitle)) + " />";
                        stringtext += "<input id='hdnRequestInfoIsrc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Isrc' type='hidden' value=" + data.Records[i].Isrc + " />";
                        stringtext += "<input id='hdnRequestInfoAdminCompany" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].AdminCompany' type='hidden' value=" + $.trim(data.Records[i].AdminCompany) + " />";
                        stringtext += "<input id='hdnRequestInfoRequestType" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestType' type='hidden' value=" + $.trim(data.Records[i].RequestType) + " />";
                        stringtext += "<input id='hdnRequestInfoConfiguration" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Configuration' type='hidden' value=" + $.trim(data.Records[i].Configuration) + " />";
                        stringtext += "<input id='hdnRequestInfoApprovalStatus" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatus' type='hidden' value=" + data.Records[i].ApprovalStatus + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRequest" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRequestString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRequestString) + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRouted" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRoutedString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRoutedString) + " />";
                        stringtext += "</td></tr>";
                        if (data.Records[i].ApprovalStatusId == 5 || data.Records[i].ApprovalStatusId == 7 || data.Records[i].ApprovalStatusId == 10)
                            counter++;
                    }
                    var otherhiddens = "<input id='hdnActionRequestId' name='hdnActionRequestId' type='hidden' value='' />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRequestSummaryListCount' name='RegularProjectDetails.RequestInfoList.Count' type='hidden' value=" + data.Records.length + " />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRSCancelledRejectedResourceCount' name='Count' type='hidden'value=" + counter + " />";
                    stringtext = "<table id='tblRequestInfoSummary' width='100%' >" + stringtext + otherhiddens + "<tr><td></td></tr><tr><td></td></tr></table>";
                    $('#tblRequestInfoSummary').replaceWith(stringtext);
                }
                IsProjectValidationsDone(input);
            }
        });
    }

    function UpdateRoutingTriggerFlagProjectLevel() {

        IsCancelledRejExists = false;
        var flag = false;
        var table = document.getElementById('tblRequestInfoSummary'),
                   rows = table.getElementsByTagName('tr');
        for (var rowi = 0; rowi < rows.length; rowi++) {
            if (document.getElementById("hdnRequestInfoResourceId" + rowi) != null) {
                if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 5) ||
                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 6) ||
                                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 7)
                                || document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 10) {

                    flag = true;
                    IsCancelledRejExists = true;
                }
            }
        }
        return flag;
    }

    function updateProjectStatus() {
        var statusType = $('#hdnStatusType').val();

        // Set command based on Current status
        if (statusType == 2) {   // Submitted then ReSubmitted
            $('#hdncommand').val("ReSubmitted"); // Set commmand in model.RegularProjectDetails.Command
        }
        else if (statusType == 5) {   // Resubmitted then ReSubmitted
            $('#hdncommand').val("ReSubmitted");  // Set commmand in model.RegularProjectDetails.Command
        }
        else if (statusType == 6) {
            $('#hdncommand').val("ReSubmitted");
        }

    }

    function UpdateRoutingTriggerFlag(ListResourceId) { //Update Routing Trigger flag in Request Summary tab
        IsCancelledRejExists = false;
        var flag = false;

        if (ListResourceId != null && ListResourceId.length > 0) {
            flag = true;
            for (var j = 0; j < ListResourceId.length; j++) {
                if (ListResourceId[j] > 0) {
                    if (ResourceId == "") {
                        ResourceId = ListResourceId[j].toString();
                    }
                    else {
                        ResourceId = ResourceId + ',' + ListResourceId[j].toString();
                    }
                    var table = document.getElementById('tblRequestInfoSummary'),
                   rows = table.getElementsByTagName('tr');
                    for (var rowi = 0; rowi < rows.length; rowi++) {
                        if (document.getElementById("hdnRequestInfoResourceId" + rowi) != null) {

                            if (document.getElementById("hdnRequestInfoResourceId" + rowi).value == ListResourceId[j]) {
                                if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 13) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 5) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 6)
                                && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 7) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 10)) {
                                    document.getElementById("hdnRequestInfoIsRoutingTriggered" + rowi).value = "true";
                                }
                                if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 5) ||
                                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 6) ||
                                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 7)
                                || document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 10) {
                                    IsCancelledRejExists = true;
                                }

                            }
                        }

                    }
                }
            }
        }
        return flag;
    }

    function UpdateRequestsFlagSetFalseForRelease(ListReleaseId) { //Update Routing Trigger flag in Request Summary tab

        var flag = false;
        if (ListReleaseId != null && ListReleaseId.length > 0) {
            flag = true;
            for (var j = 0; j < ListReleaseId.length; j++) {
                if (ListReleaseId[j] > 0) {

                    var table = document.getElementById('tblRequestInfoSummary'),
                   rows = table.getElementsByTagName('tr');
                    for (var rowi = 0; rowi < rows.length; rowi++) {
                        if (document.getElementById("hdnRequestInfoReleaseId" + rowi) != null) {
                            if (document.getElementById("hdnRequestInfoReleaseId" + rowi).value == ListReleaseId[j]) {
                                if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 13) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 5) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 6)
                                && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 7) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 10)) {
                                    document.getElementById("hdnRequestInfoIsRoutingTriggered" + rowi).value = "false";
                                }

                            }
                        }

                    }
                }
            }
        }
        return flag;
    }

    function UpdateRequestsFlagForRelease(ListReleaseId) { //Update Routing Trigger flag in Request Summary tab
        IsCancelledRejExists == false;
        var flag = false;
        if (ListReleaseId != null && ListReleaseId.length > 0) {
            flag = true;
            for (var j = 0; j < ListReleaseId.length; j++) {
                if (ListReleaseId[j] > 0) {
                    if (ResourceId == "") {
                        ResourceId = ListReleaseId[j].toString();
                    }
                    else {
                        ResourceId = ResourceId + ',' + ListReleaseId[j].toString();
                    }
                    var table = document.getElementById('tblRequestInfoSummary'),
                   rows = table.getElementsByTagName('tr');
                    for (var rowi = 0; rowi < rows.length; rowi++) {
                        if (document.getElementById("hdnRequestInfoReleaseId" + rowi) != null) {
                            if (document.getElementById("hdnRequestInfoReleaseId" + rowi).value == ListReleaseId[j]) {
                                if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 13) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 5) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 6)
                                && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 7) && (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value != 10)) {
                                    document.getElementById("hdnRequestInfoIsRoutingTriggered" + rowi).value = "true";
                                }
                                if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 5) ||
                                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 6) ||
                                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 7)
                                || document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 10) {
                                    IsCancelledRejExists = true;
                                }

                            }
                        }

                    }
                }
            }
        }
        return flag;
    }

    function UpdatePackageRemoveFlag(lstCombinedDataList, input) { //Update Routing Trigger flag in Request Summary tab
        var flag = false;

        if (lstCombinedDataList[3][0] > 0) {

            if (lstCombinedDataList[3][0] > 0 || (lstCombinedDataList[3][0] == -1 && lstCombinedDataList[3].length > 1)) {
                var objWarningDialog = $('<div id="Confirm"></div>')
                    .html('<p>' + ResubmissionPackageRemovalMsg + '</p>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        title: regularProjectMessages.confim,
                        show: 'clip',
                        hide: 'clip',
                        width: 300
                    });

                objWarningDialog.dialog('open');
                objWarningDialog.dialog({
                    buttons:
                    {
                        'Yes': function (e) {
                            $(this).dialog('close');

                            for (var j = 0; j < lstCombinedDataList[3].length; j++) {
                                if (lstCombinedDataList[3][j] > 0) {
                                    var table = document.getElementById('tblRequestInfoSummary'),
                                rows = table.getElementsByTagName('tr');
                                    for (var rowi = 0; rowi < rows.length; rowi++) {
                                        if (document.getElementById("hdnRequestInfoPackageId" + rowi) != null) {
                                            if (document.getElementById("hdnRequestInfoPackageId" + rowi).value == lstCombinedDataList[3][j]) {
                                                document.getElementById("hdnRequestInfoIsRoutingTriggered" + rowi).value = "false";
                                                document.getElementById("hdnIsRoutingStop" + rowi).value = "true";
                                                flag = true;
                                                IsResubmissionTrigerred = 1;
                                                $('#hdnAdditionalResourceCheck').val("YES");
                                            }
                                        }

                                    }
                                }
                            }
                            IsNewResourceAddedToProject(input);

                        },
                        'No': function () {
                            $(this).dialog('close');
                            $('#loadingDivPA').show();
                            var digitalVal = $('#CreateRegularProjectForm').serialize();
                            $.post('/GCS/ClearanceProject/DiscardAllAmendments', digitalVal, function (data) {
                                $('#loadingDivPA').hide();

                                $('#divPartialUpdate').html(data);

                            });

                        }

                    }
                });
            }
            else {
                IsNewResourceAddedToProject(input);
            }

        }
        else {
            IsNewResourceAddedToProject(input);
        }


    }

    function UpdateSalesChannelsFlag(lstCombinedDataList) { //Update Routing Trigger flag in Request Summary tab
        var flag = false;

        $('#IsUMGiMarkettingRoutingRequired').val(false);
        $('#IsNewCountriesAddedAfterSubmit').val(false);

        // special case regular
        if (lstCombinedDataList[2].length > 0) {
            $('#IsUMGiMarkettingRoutingRequired').val(true);

            if (lstCombinedDataList[2].length == 2)
                $('#IsNewCountriesAddedAfterSubmit').val(true);

        }

        // set all flags to 0 before re-assigning values
        var salesChannelCountPrev = $('#newlyAddedSalesChannelsAfterSubmitCount').val();
        for (var i = 0; i < salesChannelCountPrev; i++) {
            $('#newlyAddedSalesChannelsAfterSubmit' + i).val(0);
        }

        if (lstCombinedDataList[0].length > 0) {
            flag = true;
            var salesChannelCount = lstCombinedDataList[0].length;
            for (var i = 0; i < salesChannelCount; i++) {
                $('#newlyAddedSalesChannelsAfterSubmit' + i).val(lstCombinedDataList[0][i]);
            }

        }
        if (lstCombinedDataList[1].length > 0) {
            flag = true;
            for (var j = 0; j < lstCombinedDataList[1].length; j++) {
                if (lstCombinedDataList[1][j] > 0) {
                    var table = document.getElementById('tblRequestInfoSummary'),
                       rows = table.getElementsByTagName('tr');
                    for (var rowi = 0; rowi < rows.length; rowi++) {
                        if (document.getElementById("hdnSalesChannelId" + rowi) != null) {
                            if (document.getElementById("hdnSalesChannelId" + rowi).value == lstCombinedDataList[1][j]) {

                                document.getElementById("hdnRequestInfoIsRoutingTriggered" + rowi).value = "false";
                                document.getElementById("hdnIsRoutingStop" + rowi).value = "true";

                                if ((document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 5) ||
                                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 6) ||
                                (document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 7)
                                || document.getElementById("hdnRequestInfoRequestStatusId" + rowi).value == 10) {
                                    IsCancelledRejExists = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        return flag;
    }

    //Update the IsRouting Trigger flag for All the resource where status of Request is not Cancelled and Rejected
    function UpdateAllRequestSummaryResource() {
        var flag = true;
        var ResourceCount = $('#hdnRequestSummaryListCount').val();
        for (var i = 0; i < ResourceCount; i++) {
            if (document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 13 && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 5 && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 6
            && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 7 && document.getElementById("hdnRequestInfoRequestStatusId" + i).value != 10) {//Id of Rejected, Auto Rejected, Cancelled, SYSTEM CANCEL,
                document.getElementById("hdnRequestInfoIsRoutingTriggered" + i).value = "true";
            }
        }
        return flag;
    }

    //Update the IsRouting Trigger flag set to false
    function UpdateFalseRequestSummaryResource() {
        var flag = true;
        var ResourceCount = $('#hdnRequestSummaryListCount').val();
        for (var i = 0; i < ResourceCount; i++) {
            if (document.getElementById("hdnRequestInfoIsRoutingTriggered" + i) != null) {
                document.getElementById("hdnRequestInfoIsRoutingTriggered" + i).value = "false";
            }

        }
    }

    // button used for  resubmit project when opened in Reopened state State
    $("#btnSaveResubmit").live("click", function () {

        var IsValid = false;
        var ispriceValidation = false;
        // validations check
        IsValid = ValidationForRegularProject();

        if (IsValid == false) {
            return false;
        }

        ispriceValidation = ValidationOnMoneyFields();

        if (ispriceValidation == false) {

            return false;
        }


        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);

        //UC011 A changes
        projectMode = "reopened";

        RegularProjectSubmissionValidations("reopened");
    });

    $("#btnCancelProject").live("click", function () {
        globalPostBackCheck = true;
        $('#loadingDivPA').show();

        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);

        $('#hdncommand').val("Cancel");

        $.ajax({
            url: '/GCS/ClearanceProject/GetRequestsummaryData/',
            type: 'POST',
            async: true,
            data: { ClrProjectId: $('#Clr_Project_Id').val().toString() },
            success: function (data) {

                if (data.Result == "ERROR") {
                    $('#loadingDivPA').hide();
                    alert("Error on Request summary tab");
                    return false;
                }
                else if (data.Records.length == 0 && (document.getElementById('hdnProjWithNoResources').value != "true")) {
                    $('#loadingDivPA').hide();
                    alert("No data on Request summary tab");
                    return false;
                }

                if (data.Records.length > 0) {
                    var stringtext = "";
                    var counter = 0;
                    for (i = 0; i < data.Records.length; i++) {

                        stringtext += "<tr><td>";
                        stringtext += "<input  id='hdnSalesChannelId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].SalesChannelId' type='hidden' value=" + data.Records[i].SalesChannelId + " />";
                        stringtext += "<input  id='hdnRSRequestlId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestId' type='hidden' value=" + data.Records[i].RequestId + " />";
                        stringtext += "<input  id='hdnIsRoutingStop" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingStop' type='hidden' value=" + data.Records[i].IsRoutingStop + " />";
                        stringtext += "<input  id='hdnRequestInfoReleaseId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ReleaseId' type='hidden' value=" + data.Records[i].ReleaseId + " />";
                        stringtext += "<input  id='hdnRequestInfoPackageId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].PackageId' type='hidden' value=" + data.Records[i].PackageId + " />";
                        stringtext += "<input  id='hdnRequestInfoClearanceResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[0].ClearanceProjectResourceId[" + i + "].ClearanceProjectResourceId' type='hidden' value=" + data.Records[i].ClearanceProjectResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceId' type='hidden' value=" + data.Records[i].ResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestTypeId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestTypeId' type='hidden' value=" + data.Records[i].RequestTypeId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestStatusId" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatusId' type='hidden' value=" + data.Records[i].ApprovalStatusId + " />";
                        stringtext += "<input  id='hdnRequestInfoIsRoutingTriggered" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingTriggered' type='hidden' value=" + data.Records[i].IsRoutingTriggered + " />  ";
                        stringtext += "<input  id='hdnRequestInfoRoutedItemId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RoutedItemId' type='hidden' value=" + data.Records[i].RoutedItemId + " /> ";
                        stringtext += "<input id='hdnRequestInfoUpc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Upc' type='hidden' value=" + $.trim(data.Records[i].Upc) + " />";
                        stringtext += "<input id='hdnRequestInfoResourceTitle" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceTitle' type='hidden' value=" + $.trim(data.Records[i].ResourceTitle).replace("'", "\\'") + " />";
                        stringtext += "<input id='hdnRequestInfoIsrc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Isrc' type='hidden' value=" + data.Records[i].Isrc + " />";
                        stringtext += "<input id='hdnRequestInfoAdminCompany" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].AdminCompany' type='hidden' value=" + $.trim(data.Records[i].AdminCompany) + " />";
                        stringtext += "<input id='hdnRequestInfoRequestType" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestType' type='hidden' value=" + $.trim(data.Records[i].RequestType) + " />";
                        stringtext += "<input id='hdnRequestInfoConfiguration" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Configuration' type='hidden' value=" + $.trim(data.Records[i].Configuration) + " />";
                        stringtext += "<input id='hdnRequestInfoApprovalStatus" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatus' type='hidden' value=" + data.Records[i].ApprovalStatus + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRequest" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRequestString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRequestString) + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRouted" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRoutedString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRoutedString) + " />";

                        stringtext += "</td></tr>";

                        if (data.Records[i].ApprovalStatusId == 5 || data.Records[i].ApprovalStatusId == 7 || data.Records[i].ApprovalStatusId == 10)
                            counter++;

                    }
                    var otherhiddens = "<input id='hdnActionRequestId' name='hdnActionRequestId' type='hidden' value='' />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRequestSummaryListCount' name='RegularProjectDetails.RequestInfoList.Count' type='hidden' value=" + data.Records.length + " />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRSCancelledRejectedResourceCount' name='Count' type='hidden'value=" + counter + " />";

                    stringtext = "<table id='tblRequestInfoSummary' width='100%' >" + stringtext + otherhiddens + "<tr><td></td></tr><tr><td></td></tr></table>";

                    $('#tblRequestInfoSummary').replaceWith(stringtext);
                }

                var digitalVal = $('#CreateRegularProjectForm').serialize();
                $.post('/GCS/ClearanceProject/CancelRegularProject', digitalVal, function (data) {
                    $('#loadingDivPA').hide();
                    if (data.Error) {
                        $('#divErrorMessage').html(data.Message);
                        $('#divErrorMessage').show();
                        $('#divErrorMessage').addClass('error msg-margin');
                        return false;
                    }
                    var projectid = $(data).find('#Clr_Project_Id').val();
                    var statustype = "3"
                    var popup = window.opener;
                    if (popup) {
                        var _roleGroup = popup._activeRoleGroup;
                        if (_roleGroup == undefined) {
                            _roleGroup = '';
                        }
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype + '&ActiveRoleGroup=' + _roleGroup;
                    }
                    else {
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype;
                    }
                });
            }
        });
    });

    $("#btnComplete").live("click", function () {

        globalPostBackCheck = true;
        $('#loadingDivPA').show();

        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);

        $('#hdncommand').val("Cancel");

        $.ajax({
            url: '/GCS/ClearanceProject/GetRequestsummaryData/',
            type: 'POST',
            async: true,
            data: { ClrProjectId: $('#Clr_Project_Id').val().toString() },
            success: function (data) {

                if (data.Result == "ERROR") {
                    $('#loadingDivPA').hide();
                    alert("Error on Request summary tab");
                    return false;
                }
                else if (data.Records.length == 0 && (document.getElementById('hdnProjWithNoResources').value != "true")) {
                    $('#loadingDivPA').hide();
                    alert("No data on Request summary tab");
                    return false;
                }

                if (data.Records.length > 0) {
                    var stringtext = "";
                    var counter = 0;
                    for (i = 0; i < data.Records.length; i++) {

                        stringtext += "<tr><td>";
                        stringtext += "<input  id='hdnSalesChannelId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].SalesChannelId' type='hidden' value=" + data.Records[i].SalesChannelId + " />";
                        stringtext += "<input  id='hdnRSRequestlId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestId' type='hidden' value=" + data.Records[i].RequestId + " />";
                        stringtext += "<input  id='hdnIsRoutingStop" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingStop' type='hidden' value=" + data.Records[i].IsRoutingStop + " />";
                        stringtext += "<input  id='hdnRequestInfoReleaseId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ReleaseId' type='hidden' value=" + data.Records[i].ReleaseId + " />";
                        stringtext += "<input  id='hdnRequestInfoPackageId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].PackageId' type='hidden' value=" + data.Records[i].PackageId + " />";
                        stringtext += "<input  id='hdnRequestInfoClearanceResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[0].ClearanceProjectResourceId[" + i + "].ClearanceProjectResourceId' type='hidden' value=" + data.Records[i].ClearanceProjectResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceId' type='hidden' value=" + data.Records[i].ResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestTypeId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestTypeId' type='hidden' value=" + data.Records[i].RequestTypeId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestStatusId" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatusId' type='hidden' value=" + data.Records[i].ApprovalStatusId + " />";
                        stringtext += "<input  id='hdnRequestInfoIsRoutingTriggered" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingTriggered' type='hidden' value=" + data.Records[i].IsRoutingTriggered + " />  ";
                        stringtext += "<input  id='hdnRequestInfoRoutedItemId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RoutedItemId' type='hidden' value=" + data.Records[i].RoutedItemId + " /> ";
                        stringtext += "<input id='hdnRequestInfoUpc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Upc' type='hidden' value=" + $.trim(data.Records[i].Upc) + " />";
                        stringtext += "<input id='hdnRequestInfoResourceTitle" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceTitle' type='hidden' value=" + $.trim(data.Records[i].ResourceTitle).replace("'", "\\'") + " />";
                        stringtext += "<input id='hdnRequestInfoIsrc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Isrc' type='hidden' value=" + data.Records[i].Isrc + " />";
                        stringtext += "<input id='hdnRequestInfoAdminCompany" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].AdminCompany' type='hidden' value=" + $.trim(data.Records[i].AdminCompany) + " />";
                        stringtext += "<input id='hdnRequestInfoRequestType" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestType' type='hidden' value=" + $.trim(data.Records[i].RequestType) + " />";
                        stringtext += "<input id='hdnRequestInfoConfiguration" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Configuration' type='hidden' value=" + $.trim(data.Records[i].Configuration) + " />";
                        stringtext += "<input id='hdnRequestInfoApprovalStatus" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatus' type='hidden' value=" + data.Records[i].ApprovalStatus + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRequest" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRequestString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRequestString) + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRouted" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRoutedString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRoutedString) + " />";

                        stringtext += "</td></tr>";

                        if (data.Records[i].ApprovalStatusId == 5 || data.Records[i].ApprovalStatusId == 7 || data.Records[i].ApprovalStatusId == 10)
                            counter++;

                    }


                    var otherhiddens = "<input id='hdnActionRequestId' name='hdnActionRequestId' type='hidden' value='' />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRequestSummaryListCount' name='RegularProjectDetails.RequestInfoList.Count' type='hidden' value=" + data.Records.length + " />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRSCancelledRejectedResourceCount' name='Count' type='hidden'value=" + counter + " />";

                    stringtext = "<table id='tblRequestInfoSummary' width='100%' >" + stringtext + otherhiddens + "<tr><td></td></tr><tr><td></td></tr></table>";

                    $('#tblRequestInfoSummary').replaceWith(stringtext);

                }

                var digitalVal = $('#CreateRegularProjectForm').serialize();
                $.post('/GCS/ClearanceProject/CompleteRegularProject', digitalVal, function (data) {
                    $('#loadingDivPA').hide();
                    if (data.Error) {
                        $('#divErrorMessage').html(data.Message);
                        $('#divErrorMessage').show();
                        $('#divErrorMessage').addClass('error msg-margin');
                        return false;
                    }
                    var projectid = $(data).find('#Clr_Project_Id').val();
                    var statustype = "4";
                    var popup = window.opener;
                    if (popup) {
                        var _roleGroup = popup._activeRoleGroup;
                        if (_roleGroup == undefined) {
                            _roleGroup = '';
                        }
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype + '&ActiveRoleGroup=' + _roleGroup;
                    }
                    else {
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype;
                    }
                });
            }

        });



    });

    $("#btnReinstate").live("click", function () {

        globalPostBackCheck = true;
        $('#loadingDivPA').show();

        if ($('#cmbReleaseNewOrExisting').val() == "2") {
            $("#FlagReleaseNewOrExisting").val('Exist');
        }
        else if ($('#cmbReleaseNewOrExisting').val() == "1") {
            $("#FlagReleaseNewOrExisting").val('New');
        }

        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);

        $('#hdncommand').val("Reinstate");
        $(document).each(function () { $('input:not(#btnReinstate), textarea, select').attr('disabled', false) });


        //Added by Raja to make all the request summary requests are updated.
        $.ajax({
            url: '/GCS/ClearanceProject/GetRequestsummaryData/',
            type: 'POST',
            async: true,
            data: { ClrProjectId: $('#Clr_Project_Id').val().toString() },
            success: function (data) {

                if (data.Result == "ERROR") {
                    $('#loadingDivPA').hide();
                    alert("Error on Request summary tab");
                    return false;
                }
                else if (data.Records.length == 0 && (document.getElementById('hdnProjWithNoResources').value != "true")) {
                    $('#loadingDivPA').hide();
                    alert("No data on Request summary tab");
                    return false;
                }

                if (data.Records.length > 0) {
                    var stringtext = "";
                    var counter = 0;
                    for (i = 0; i < data.Records.length; i++) {

                        stringtext += "<tr><td>";
                        stringtext += "<input  id='hdnSalesChannelId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].SalesChannelId' type='hidden' value=" + data.Records[i].SalesChannelId + " />";
                        stringtext += "<input  id='hdnRSRequestlId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestId' type='hidden' value=" + data.Records[i].RequestId + " />";
                        stringtext += "<input  id='hdnIsRoutingStop" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingStop' type='hidden' value=" + data.Records[i].IsRoutingStop + " />";
                        stringtext += "<input  id='hdnRequestInfoReleaseId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ReleaseId' type='hidden' value=" + data.Records[i].ReleaseId + " />";
                        stringtext += "<input  id='hdnRequestInfoPackageId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].PackageId' type='hidden' value=" + data.Records[i].PackageId + " />";
                        stringtext += "<input  id='hdnRequestInfoClearanceResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[0].ClearanceProjectResourceId[" + i + "].ClearanceProjectResourceId' type='hidden' value=" + data.Records[i].ClearanceProjectResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceId' type='hidden' value=" + data.Records[i].ResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestTypeId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestTypeId' type='hidden' value=" + data.Records[i].RequestTypeId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestStatusId" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatusId' type='hidden' value=" + data.Records[i].ApprovalStatusId + " />";
                        stringtext += "<input  id='hdnRequestInfoIsRoutingTriggered" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingTriggered' type='hidden' value=" + data.Records[i].IsRoutingTriggered + " />  ";
                        stringtext += "<input  id='hdnRequestInfoRoutedItemId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RoutedItemId' type='hidden' value=" + data.Records[i].RoutedItemId + " /> ";
                        stringtext += "<input id='hdnRequestInfoUpc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Upc' type='hidden' value=" + $.trim(data.Records[i].Upc) + " />";
                        stringtext += "<input id='hdnRequestInfoResourceTitle" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceTitle' type='hidden' value=" + $.trim(data.Records[i].ResourceTitle).replace("'", "\\'") + " />";
                        stringtext += "<input id='hdnRequestInfoIsrc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Isrc' type='hidden' value=" + data.Records[i].Isrc + " />";
                        stringtext += "<input id='hdnRequestInfoAdminCompany" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].AdminCompany' type='hidden' value=" + $.trim(data.Records[i].AdminCompany) + " />";
                        stringtext += "<input id='hdnRequestInfoRequestType" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestType' type='hidden' value=" + $.trim(data.Records[i].RequestType) + " />";
                        stringtext += "<input id='hdnRequestInfoConfiguration" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Configuration' type='hidden' value=" + $.trim(data.Records[i].Configuration) + " />";
                        stringtext += "<input id='hdnRequestInfoApprovalStatus" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatus' type='hidden' value=" + data.Records[i].ApprovalStatus + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRequest" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRequestString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRequestString) + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRouted" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRoutedString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRoutedString) + " />";

                        stringtext += "</td></tr>";

                        if (data.Records[i].ApprovalStatusId == 5 || data.Records[i].ApprovalStatusId == 7 || data.Records[i].ApprovalStatusId == 10)
                            counter++;

                    }


                    var otherhiddens = "<input id='hdnActionRequestId' name='hdnActionRequestId' type='hidden' value='' />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRequestSummaryListCount' name='RegularProjectDetails.RequestInfoList.Count' type='hidden' value=" + data.Records.length + " />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRSCancelledRejectedResourceCount' name='Count' type='hidden'value=" + counter + " />";

                    stringtext = "<table id='tblRequestInfoSummary' width='100%' >" + stringtext + otherhiddens + "<tr><td></td></tr><tr><td></td></tr></table>";

                    $('#tblRequestInfoSummary').replaceWith(stringtext);

                }

                // added by dhruv for Ajax call

                var digitalVal = $('#CreateRegularProjectForm').serialize();
                $.post('/GCS/ClearanceProject/ReinstateRegularProject', digitalVal, function (data) {
                    $('#loadingDivPA').hide();
                    if (data.Error) {
                        $('#divErrorMessage').html(data.Message);
                        $('#divErrorMessage').show();
                        $('#divErrorMessage').addClass('error msg-margin');
                        return false;
                    }
                    var projectid = $(data).find('#Clr_Project_Id').val();
                    var statustype = "5";
                    var popup = window.opener;
                    if (popup) {
                        var _roleGroup = popup._activeRoleGroup;
                        if (_roleGroup == undefined) {
                            _roleGroup = '';
                        }
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype + '&ActiveRoleGroup=' + _roleGroup;
                    }
                    else {
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype;
                    }
                });
            }

        });
    });

    $("#btnReOpen").live("click", function () {

        globalPostBackCheck = true;
        $('#loadingDivPA').show();

        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);

        if ($('#cmbReleaseNewOrExisting').val() == "2") {
            $("#FlagReleaseNewOrExisting").val('Exist');
        }
        else if ($('#cmbReleaseNewOrExisting').val() == "1") {
            $("#FlagReleaseNewOrExisting").val('New');
        }

        $('#hdncommand').val("Reopen");
        $(document).each(function () { $('input:not(#btnReinstate), textarea, select').attr('disabled', false) });

        $.ajax({
            url: '/GCS/ClearanceProject/GetRequestsummaryData/',
            type: 'POST',
            async: true,
            data: { ClrProjectId: $('#Clr_Project_Id').val().toString() },
            success: function (data) {

                if (data.Result == "ERROR") {
                    $('#loadingDivPA').hide();
                    alert("Error on Request summary tab");
                    return false;
                }
                else if (data.Records.length == 0 && (document.getElementById('hdnProjWithNoResources').value != "true")) {
                    $('#loadingDivPA').hide();
                    alert("No data on Request summary tab");
                    return false;
                }

                if (data.Records.length > 0) {
                    var stringtext = "";
                    var counter = 0;
                    for (i = 0; i < data.Records.length; i++) {

                        stringtext += "<tr><td>";
                        stringtext += "<input  id='hdnSalesChannelId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].SalesChannelId' type='hidden' value=" + data.Records[i].SalesChannelId + " />";
                        stringtext += "<input  id='hdnRSRequestlId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestId' type='hidden' value=" + data.Records[i].RequestId + " />";
                        stringtext += "<input  id='hdnIsRoutingStop" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingStop' type='hidden' value=" + data.Records[i].IsRoutingStop + " />";
                        stringtext += "<input  id='hdnRequestInfoReleaseId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ReleaseId' type='hidden' value=" + data.Records[i].ReleaseId + " />";
                        stringtext += "<input  id='hdnRequestInfoPackageId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].PackageId' type='hidden' value=" + data.Records[i].PackageId + " />";
                        stringtext += "<input  id='hdnRequestInfoClearanceResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[0].ClearanceProjectResourceId[" + i + "].ClearanceProjectResourceId' type='hidden' value=" + data.Records[i].ClearanceProjectResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoResourceId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceId' type='hidden' value=" + data.Records[i].ResourceId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestTypeId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestTypeId' type='hidden' value=" + data.Records[i].RequestTypeId + " />";
                        stringtext += "<input  id='hdnRequestInfoRequestStatusId" + i + "'  name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatusId' type='hidden' value=" + data.Records[i].ApprovalStatusId + " />";
                        stringtext += "<input  id='hdnRequestInfoIsRoutingTriggered" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].IsRoutingTriggered' type='hidden' value=" + data.Records[i].IsRoutingTriggered + " />  ";
                        stringtext += "<input  id='hdnRequestInfoRoutedItemId" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RoutedItemId' type='hidden' value=" + data.Records[i].RoutedItemId + " /> ";
                        stringtext += "<input id='hdnRequestInfoUpc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Upc' type='hidden' value=" + $.trim(data.Records[i].Upc) + " />";
                        stringtext += "<input id='hdnRequestInfoResourceTitle" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ResourceTitle' type='hidden' value=" + $.trim(data.Records[i].ResourceTitle).replace("'", "\\'") + " />";
                        stringtext += "<input id='hdnRequestInfoIsrc" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Isrc' type='hidden' value=" + data.Records[i].Isrc + " />";
                        stringtext += "<input id='hdnRequestInfoAdminCompany" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].AdminCompany' type='hidden' value=" + $.trim(data.Records[i].AdminCompany) + " />";
                        stringtext += "<input id='hdnRequestInfoRequestType" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].RequestType' type='hidden' value=" + $.trim(data.Records[i].RequestType) + " />";
                        stringtext += "<input id='hdnRequestInfoConfiguration" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].Configuration' type='hidden' value=" + $.trim(data.Records[i].Configuration) + " />";
                        stringtext += "<input id='hdnRequestInfoApprovalStatus" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ApprovalStatus' type='hidden' value=" + data.Records[i].ApprovalStatus + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRequest" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRequestString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRequestString) + " />";
                        stringtext += "<input id='hdnRequestInfoModifiedDateRouted" + i + "' name='RegularProjectDetails.RequestInfoList[" + i + "].ModifiedDateRoutedString' type='hidden' value=" + JSON.parse(data.Records[i].ModifiedDateRoutedString) + " />";

                        stringtext += "</td></tr>";

                        if (data.Records[i].ApprovalStatusId == 5 || data.Records[i].ApprovalStatusId == 7 || data.Records[i].ApprovalStatusId == 10)
                            counter++;

                    }


                    var otherhiddens = "<input id='hdnActionRequestId' name='hdnActionRequestId' type='hidden' value='' />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRequestSummaryListCount' name='RegularProjectDetails.RequestInfoList.Count' type='hidden' value=" + data.Records.length + " />";
                    otherhiddens += "<input data-val='true' data-val-number='The field Count must be a number.' data-val-required='The Count field is required.' id='hdnRSCancelledRejectedResourceCount' name='Count' type='hidden'value=" + counter + " />";

                    stringtext = "<table id='tblRequestInfoSummary' width='100%' >" + stringtext + otherhiddens + "<tr><td></td></tr><tr><td></td></tr></table>";

                    $('#tblRequestInfoSummary').replaceWith(stringtext);

                }

                // added by dhruv for Ajax call

                var digitalVal = $('#CreateRegularProjectForm').serialize();
                $.post('/GCS/ClearanceProject/ReopenRegularProject', digitalVal, function (data) {
                    $('#loadingDivPA').hide();
                    if (data.Error) {
                        $('#divErrorMessage').html(data.Message);
                        $('#divErrorMessage').show();
                        $('#divErrorMessage').addClass('error msg-margin');
                        return false;
                    }
                    var projectid = $(data).find('#Clr_Project_Id').val();
                    var statustype = "6";
                    var popup = window.opener;
                    if (popup) {
                        var _roleGroup = popup._activeRoleGroup;
                        if (_roleGroup == undefined) {
                            _roleGroup = '';
                        }
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype + '&ActiveRoleGroup=' + _roleGroup;
                    }
                    else {
                        window.location.href = '/GCS/ClearanceProject/OpenRegularProjectForSubmit?projectType=Regular&projectId=' + projectid + '&Statustype=' + statustype;
                    }
                });
            }

        });
    });

    $('#cmbReleaseNewOrExisting').live("change", function () {
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
    });

    function ValidationOnProjectSave() {
        var IsValid = true;
        var focusFound = false;
        var focusField = "";
        var TabNo;
        $("#divMessage").hide();

        $("#divErrorMessage").text(""); //Set the Validation message blank


        if ($("#chkRegularRetail").is(':checked') || $("#chkClub").is(':checked') || $("#chkNonTrditional").is(':checked') || $("#chkPromotional").is(':checked')) {

            $('#tdRequestType').removeClass('input-validation-error');
            unwrapChkbox('#chkRegularRetail');
            unwrapChkbox('#chkClub');
            unwrapChkbox('#chkNonTrditional');
            unwrapChkbox('#chkPromotional');
            $("#divErrorMessage").html(mandatoryFields).hide();
        }
        else {
            if (TabNo == null) {
                TabNo = 1;
            }
            $('#chkRegularRetail').focus();
            $('#tdRequestType').addClass('input-validation-error');
            $("#divErrorMessage").html(mandatoryFields).show();
            wrapChkbox('#chkRegularRetail');
            wrapChkbox('#chkClub');
            wrapChkbox('#chkNonTrditional');
            wrapChkbox('#chkPromotional');
            IsValid = false;
            if (focusFound == false) {
                ControlToFocus = "#chkRegularRetail";
            }
            focusFound = true;
        }
        if (IsValid == false) {
            $('#' + focusField).focus();
            $('#divErrorMessage').addClass('warning');
            $("#divErrorMessage").show();
            $("#tabs").tabs({ selected: TabNo });
            return false;
        }
        else {
            $("#divErrorMessage").text("");
            $("#divErrorMessage").hide();
            $('#divErrorMessage').removeClass('warning');
            return true;
        }
    }

    function ValidationForRegularProject() {
        var IsValid = true;
        var focusFound = false;
        var focusField = "";
        var TabNo;
        $("#divMessage").hide();

        $("#divErrorMessage").text(""); //Set the Validation message blank

        if (ValidateMandatoryFieldsReleaseNew('txtEstimatedSalesUnit') == false) {
            IsValid = false;
            TabNo = 0;
            if (focusFound == false) {
                focusField = 'txtEstimatedSalesUnit';
                focusFound = true;
            }
            $("#divErrorMessage").html(mandatoryFields).show();
            $('#tdScope').addClass('input-validation-error');
        }
        else {
            $('#tdScope').removeClass('input-validation-error');
        }

        if ($("#chkOneStop").is(':checked') && ValidateMandatoryFieldsReleaseNew('txtOneStopReason') == false) {
            IsValid = false;
            TabNo = 0;
            if (focusFound == false) {
                focusField = 'txtOneStopReason';
                focusFound = true;
            }
            $("#divErrorMessage").html(mandatoryFields).show();
            $('#tdScope').addClass('input-validation-error');
        }
        else {
            $('#tdScope').removeClass('input-validation-error');
        }
        if (!ValidateDiv('selectedCountries_1', 'btnManageTerritories')) { TabNo = 0; IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'selectedCountries_1'; focusFound = true; } } //return false;}

        if ($("#chk3rdParty").is(':checked')) {

            if (ValidateMandatoryFieldsReleaseNew('txtLicenseTerm') == false) {
                IsValid = false;
                TabNo = 0;
                if (focusFound == false) {
                    focusField = 'txtLicenseTerm';
                    focusFound = true;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#tdScope').addClass('input-validation-error');
            }
            var displayThirdParty = $("#tblThirdParty").css("display");
            var table = document.getElementById('thirdPartyCompanyTable'),
            rows = table.getElementsByTagName('tr'), i, j, cells;

            if (rows.length <= 2) {
                for (i = 0, j = rows.length; i < j; ++i) {
                    cells = rows[i].getElementsByTagName('td');
                    for (k = 0; k < cells.length; k++) {
                        if ((cells[k].id == "tdCompName" && cells[k].innerText == " ") ||
                         (cells[k].id == "tdCompName" && displayThirdParty == 'none')) {
                            IsValid = false;

                            if (focusFound == false) {
                                focusField = 'name1';
                                focusFound = true;
                            }
                            if (TabNo == null) {
                                TabNo = 0;
                                $("#divErrorMessage").html(thirdPartyMandatorySelection).show();
                            }
                            $('#name1').addClass('input-validation-error');
                            $('#divErrorMessage').addClass('warning');
                            $("#divErrorMessage").show();
                            $('#tdScope').addClass('input-validation-error');
                            return false;
                        }
                    }
                }
            }
        }
        else {
            $('#tdScope').removeClass('input-validation-error');
        }

        //Request Type Validations
        if ($("#chkPhysical").is(':checked') || $("#chkDigital").is(':checked')) {
            $('#chkPhysical').removeClass('input-validation-error');
            $('#chkDigital').removeClass('input-validation-error');
            $("#divErrorMessage").html(mandatoryFields).hide();
            unwrapChkbox('#chkPhysical');
            unwrapChkbox('#chkDigital');
        }
        else {
            if (TabNo == null) {
                TabNo = 1;
            }
            $('#tdScope').addClass('input-validation-error');
            $("#divErrorMessage").html(mandatoryFields).show();
            wrapChkbox('#chkPhysical');
            wrapChkbox('#chkDigital');
            IsValid = false;
            if (focusFound == false) {
                ControlToFocus = "#chkPhysical";
            }
            focusFound = true;
        }


        if ($("#chkRegularRetail").is(':checked') || $("#chkClub").is(':checked') || $("#chkNonTrditional").is(':checked') || $("#chkPromotional").is(':checked')) {

            $('#tdRequestType').removeClass('input-validation-error');
            unwrapChkbox('#chkRegularRetail');
            unwrapChkbox('#chkClub');
            unwrapChkbox('#chkNonTrditional');
            unwrapChkbox('#chkPromotional');
            $("#divErrorMessage").html(mandatoryFields).hide();
        }
        else {
            if (TabNo == null) {
                TabNo = 1;
            }
            $('#chkRegularRetail').focus();
            $('#tdRequestType').addClass('input-validation-error');
            $("#divErrorMessage").html(mandatoryFields).show();
            wrapChkbox('#chkRegularRetail');
            wrapChkbox('#chkClub');
            wrapChkbox('#chkNonTrditional');
            wrapChkbox('#chkPromotional');
            IsValid = false;
            if (focusFound == false) {
                ControlToFocus = "#chkRegularRetail";
            }
            focusFound = true;
        }


        // TV/Radio Break ICLA
        //Alteast one should be selected : TV or Radio or Other,
        if ($("#chkTVRadioBreakICLA").is(':checked')) {
            if (ValidateMandatoryFieldsRequestType('txtDurationFrom') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                if (focusFound == false) {
                    ControlToFocus = "#txtDurationFrom";
                }
                focusFound = true;
            }
            if (ValidateMandatoryFieldsRequestType('txtDurationTo') == false) {

                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                if (focusFound == false) {
                    ControlToFocus = "#txtDurationTo";
                }
                focusFound = true;
            }
            if ($("#chkTV").is(':checked') || $("#chkRadio").is(':checked') || $("#chkOthers").is(':checked')) {
                $('#tvRadioOtherGroup').removeClass('input-validation-error');
            }
            else {
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#tvRadioOtherGroup').addClass('input-validation-error');
                IsValid = false;

            }
        }

        //if TV checkbox is selected
        if ($("#chkTV").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {
            if (ValidateMandatoryFieldsRequestType('txtTVBudget') == false) {

                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $('#txtTVBudget').addClass('input-validation-error');
                $("#divErrorMessage").html(mandatoryFields).show();
                if (focusFound == false) {
                    ControlToFocus = "#txtTVBudget";
                }
                focusFound = true;
            }
            else {
                $('#txtTVBudget').removeClass('input-validation-error');
            }
            if ($('#CurrencyList option:selected').text() != "USD") {
                if (ValidateMandatoryFieldsRequestType('txtTVBudgetUSD') == false) {
                    IsValid = false;
                    if (TabNo == null) {
                        TabNo = 1;
                    }
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtTVBudgetUSD').addClass('input-validation-error');
                    if (focusFound == false) {
                        ControlToFocus = "#txtTVBudgetUSD";
                    }
                    focusFound = true;
                }
                else {
                    $('#txtTVBudgetUSD').removeClass('input-validation-error');
                }
            }
            else {
                $('#txtTVBudgetUSD').removeClass('input-validation-error');
            }
        }

        //if Radio checkbox is selected
        if ($("#chkRadio").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

            if (ValidateMandatoryFieldsRequestType('txtRdoBudget') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtRdoBudget').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtRdoBudget";
                }
                focusFound = true;
            }
            else {
                $('#txtRdoBudget').removeClass('input-validation-error');
            }
            if ($('#CurrencyList option:selected').text() != "USD") {
                if (ValidateMandatoryFieldsRequestType('txtRdoBudgetUSD') == false) {
                    IsValid = false;
                    if (TabNo == null) {
                        TabNo = 1;
                    }
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtRdoBudgetUSD').addClass('input-validation-error');
                    if (focusFound == false) {
                        ControlToFocus = "#txtRdoBudgetUSD";
                    }
                    focusFound = true;
                }
                else {
                    $('#txtRdoBudgetUSD').removeClass('input-validation-error');
                }
            }
            else {
                $('#txtRdoBudgetUSD').removeClass('input-validation-error');
            }

        }

        //if Other checkbox is selected

        if ($("#chkOthers").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

            if (ValidateMandatoryFieldsRequestType('txtOthrBudget') == false) {

                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtOthrBudget').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtOthrBudget";
                }
                focusFound = true;
            }
            else {
                $('#txtOthrBudget').removeClass('input-validation-error');
            }

            if ($('#CurrencyList option:selected').text() != "USD") {
                if (ValidateMandatoryFieldsRequestType('txtOthrBudgetUSD') == false) {
                    IsValid = false;
                    if (TabNo == null) {
                        TabNo = 1;
                    }
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtOthrBudgetUSD').addClass('input-validation-error');
                    if (focusFound == false) {
                        ControlToFocus = "#txtOthrBudgetUSD";
                    }
                    focusFound = true;
                }
                else {
                    $('#txtOthrBudgetUSD').removeClass('input-validation-error');
                }
            }
            else {
                $('#txtOthrBudgetUSD').removeClass('input-validation-error');
            }

            if (ValidateMandatoryFieldsRequestType('txtOthrMedDetails') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtOthrMedDetails').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtOthrMedDetails";
                }
                focusFound = true;
            }
            else {
                $('#txtOthrMedDetails').removeClass('input-validation-error');
            }
        }


        //Sale Channel
        if ($("#chkTVRadioBreakICLA").is(':checked')) {
            if ($("#chkPhysicals").is(':checked') || $("#chkALaCarteDownload").is(':checked') || $("#chkSubscriptionDownload").is(':checked') || $("#chkMobileRealTonesDownload").is(':checked') || $("#chkMobileRingBackDownload").is(':checked') || $("#chkStreaming").is(':checked')) {
                $('#ulSaleChannel').removeClass('input-validation-error');
            }
            else {
                if (TabNo == null) {
                    TabNo = 1;
                }
                $('#ulSaleChannel').addClass('input-validation-error');
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#ulSaleChannel').focus();
                IsValid = false;
            }
        }

        //For campaign countries
        if ($("#chkTVRadioBreakICLA").is(':checked')) {
            if (!ValidateDiv('selectedCountries_2', 'btnManageTerritories_1')) {
                TabNo = 1; IsValid = false; NotValid = true;
                if (focusFound == false) {
                    focusField = 'selectedCountries_2';
                    focusFound = true;
                }
            }
        }


        //If chkPhysical is selected then validate Physical Sales Split & Physical Revenue Split.
        if ($("#chkPhysical").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtPSaleDate') == false) {

                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtPSaleDate').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtPSaleDate";
                }
                focusFound = true;
            }
            else {
                $('#txtPSaleDate').removeClass('input-validation-error');
            }


            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtPSaleWith') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtPSaleWith').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtPSaleWith";
                }
                focusFound = true;
            }
            else {
                $('#txtPSaleWith').removeClass('input-validation-error');
            }

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtPSaleWithout') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtPSaleWithout').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtPSaleWithout";
                }
                focusFound = true;
            }
            else {
                $('#txtPSaleWithout').removeClass('input-validation-error');
            }

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtPRevDate') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtPRevDate').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtPRevDate";
                }
                focusFound = true;
            }
            else {
                $('#txtPRevDate').removeClass('input-validation-error');
            }

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtPRevWith') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtPRevWith').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtPRevWith";
                }
                focusFound = true;
            }
            else {
                $('#txtPRevWith').removeClass('input-validation-error');
            }

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtPRevWithout') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtPRevWithout').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtPRevWithout";
                }
                focusFound = true;
            }
            else {
                $('#txtPRevWithout').removeClass('input-validation-error');
            }
        }


        //If chkDigital is selected then validate Digital Sales Split & Digital Revenue Split.
        if ($("#chkDigital").is(':checked') && $("#chkTVRadioBreakICLA").is(':checked')) {

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtDSaleDate') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtDSaleDate').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtDSaleDate";
                }
                focusFound = true;
            }
            else {
                $('#txtDSaleDate').removeClass('input-validation-error');
            }

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtDSaleWith') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtDSaleWith').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtDSaleWith";
                }
                focusFound = true;
            }
            else {
                $('#txtDSaleWith').removeClass('input-validation-error');
            }


            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtDSaleWithout') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtDSaleWithout').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtDSaleWithout";
                }
                focusFound = true;
            }
            else {
                $('#txtDSaleWithout').removeClass('input-validation-error');
            }


            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtDRevDate') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtDRevDate').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtDRevDate";
                }
                focusFound = true;
            }
            else {
                $('#txtDRevDate').removeClass('input-validation-error');
            }

            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtDRevWith') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtDRevWith').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtDRevWith";
                }
                focusFound = true;
            }
            else {
                $('#txtDRevWith').removeClass('input-validation-error');
            }


            if (ValidateMandatoryFieldsRequestTypePhySalesTodate('txtDRevWithout') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtDRevWithout').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtDRevWithout";
                }
                focusFound = true;
            }
            else {
                $('#txtDRevWithout').removeClass('input-validation-error');
            }
        }

        //Price Reduction
        if ($("#chkPriceReduction").is(':checked')) {

            selRoleTypeText = $("#cboCurrentPriceList option:selected").text();
            if (selRoleTypeText.match('--Select Role--')) {
                $('#cboCurrentPriceList').addClass('input-validation-error');
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                if (focusFound == false) {
                    ControlToFocus = "#cboCurrentPriceList";
                }
                focusFound = true;
            }
            else {
                $('#cboCurrentPriceList').removeClass('input-validation-error');
            }

            selRoleTypeText = $("#cboRequestPriceList option:selected").text();
            if (selRoleTypeText.match('--Select--')) {
                $('#cboRequestPriceList').addClass('input-validation-error');
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                if (focusFound == false) {
                    ControlToFocus = "#cboRequestPriceList";
                }
                focusFound = true;
            }
            else {
                $('#cboRequestPriceList').removeClass('input-validation-error');
            }
        }


        //If chkPromotional selected
        if ($("#chkPromotional").is(':checked')) {

            if (ValidateMandatoryFieldsReleaseNew('txtDistTo') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtDistTo').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtDistTo";
                }
                focusFound = true;
            }
            else {
                $('#txtDistTo').removeClass('input-validation-error');
            }
        }


        //If chkNonTrditional selected
        if ($("#chkNonTrditional").is(':checked')) {

            if (ValidateMandatoryFieldsReleaseNew('txtClientName') == false) {
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtClientName').addClass('input-validation-error');
                if (focusFound == false) {
                    ControlToFocus = "#txtClientName";
                }
                focusFound = true;
            }
            else {
                $('#txtClientName').removeClass('input-validation-error');
            }

            if ($("#cmbManByUMG option:selected").text() == "-Select-") {
                $('#cmbManByUMG').addClass('input-validation-error');
                IsValid = false;
                if (TabNo == null) {
                    TabNo = 1;
                }
                $("#divErrorMessage").html(mandatoryFields).show();
                if (focusFound == false) {
                    ControlToFocus = "#cmbManByUMG";
                }
                focusFound = true;
            }
            else {
                $('#ManufacturedByUMG').removeClass('input-validation-error');
            }


            //If chkNonTrditional selected with Premium checkbox
            if ($("#chkNonTrditional").is(':checked') && $("#chkPremium").is(':checked')) {

                if (ValidateMandatoryFieldsReleaseNew('txtAPremiumComments') == false) {
                    IsValid = false;
                    if (TabNo == null) {
                        TabNo = 1;
                    }
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtAPremiumComments').addClass('input-validation-error');
                    if (focusFound == false) {
                        ControlToFocus = "#txtAPremiumComments";
                    }
                    focusFound = true;
                }
                else {
                    $('#txtAPremiumComments').removeClass('input-validation-error');
                }
            }


            if ($("#chkNonTrditional").is(':checked') && $("#chkGiveAwayFreeOfCharge").is(':checked')) {

                if (ValidateMandatoryFieldsReleaseNew('txtAGiveAwayComments') == false) {
                    IsValid = false;
                    if (TabNo == null) {
                        TabNo = 1;
                    }
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtAGiveAwayComments').addClass('input-validation-error');
                    if (focusFound == false) {
                        ControlToFocus = "#txtAGiveAwayComments";
                    }
                    focusFound = true;
                }
                else {
                    $('#txtAGiveAwayComments').removeClass('input-validation-error');
                }
            }


            if ($("#chkNonTrditional").is(':checked') && $("#chkOther").is(':checked')) {
                if (ValidateMandatoryFieldsReleaseNew('txtAOtherComments') == false) {
                    IsValid = false;
                    if (TabNo == null) {
                        TabNo = 1;
                    }
                    $("#divErrorMessage").html(mandatoryFields).show();
                    $('#txtAOtherComments').addClass('input-validation-error');
                    if (focusFound == false) {
                        ControlToFocus = "#txtAOtherComments";
                    }
                    focusFound = true;
                }
                else {
                    $('#txtAOtherComments').removeClass('input-validation-error');
                }

            }
        }
        //End Request Type Validations
        if ($("#FlagReleaseNewOrExisting").val() == "New") {
            var Count = $('#hdnReleaseRowsCount').val();
            var CountResource = $('#hdnResourceListCount').val();
            var CountResourceArchived = $('#hdnResourceListCount').val();

            for (i = 0; i <= CountResourceArchived - 1; i++) {
                if ($('#hdnArchiveFlagRegular' + i).val() == "Y") {
                    CountResource = CountResource - 1;
                }
            }

            //Start For globally cleared comments in resource tab
            if (parseInt(CountResource) > 0) {
                for (i = 0; i <= CountResource - 1; i++) {
                    if ($('#chkGloballyCleared' + i).is(':checked')) {
                        if ($('#txtGloballyCleared' + i).val() == "") {
                            $("#divErrorMessage").html(mandatoryFields).show();
                            $('#txtGloballyCleared' + i).addClass('btn-validation-error');
                            $("#divMessage").hide();
                            IsValid = false;
                        }
                        else {
                            $('#txtGloballyCleared' + i).removeClass('btn-validation-error');
                        }
                    }
                }
            }
            //End For globally cleared comments in resource tab

            for (relcount = 0; relcount <= Count - 1; relcount++) {
                var archflg = "#hdnArchiveFlag" + relcount;
                if ($(archflg).val() == "N") {

                    if (!ValidateDiv('divArtistName' + relcount + '', 'btnManageArtist' + relcount + '')) { if (TabNo == null) { TabNo = 2; } IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'btnManageArtist' + relcount + ''; focusFound = true; } } //return false;}
                    if (ValidateMandatoryFieldsReleaseNew('txtTitle_' + relcount) == false) {
                        IsValid = false;
                        if (TabNo == null) {
                            TabNo = 2;
                        }
                        if (focusFound == false) {
                            focusField = 'txtTitle_' + relcount;
                            focusFound = true;
                        }
                        $("#divErrorMessage").html(mandatoryFields).show();
                        $('#tdScope').addClass('input-validation-error');
                    }
                    else {
                        $('#tdScope').removeClass('input-validation-error');
                    }

                    if (ValidateMandatoryFieldsReleaseNew('LabelId_' + relcount) == false) {
                        IsValid = false;
                        if (TabNo == null) {
                            TabNo = 2;
                        }
                        if (focusFound == false) {
                            focusField = 'labelName_' + relcount;
                            focusFound = true;
                        }
                        $("#divErrorMessage").html(mandatoryFields).show();
                        $('#labelName_' + relcount).val("");
                        $('#labelName_' + relcount).addClass('input-validation-error');
                    }
                    else {
                        $('#labelName_' + relcount).removeClass('input-validation-error');
                    }
                    if (ValidateMandatoryFieldsReleaseNew('ddlConfigGrpList_' + relcount) == false) {
                        IsValid = false;
                        if (TabNo == null) {
                            TabNo = 2;
                        }
                        if (focusFound == false) {
                            focusField = 'ddlConfigGrpList_' + relcount;
                            focusFound = true;
                        }
                        $("#divErrorMessage").html(mandatoryFields).show();
                        $('#tdScope').addClass('input-validation-error');
                    }
                    else {
                        $('#tdScope').removeClass('input-validation-error');
                    }
                    if (ValidateMandatoryFieldsReleaseNew('ddlConfigList_' + relcount) == false) {
                        IsValid = false;
                        if (TabNo == null) {
                            TabNo = 2;
                        }
                        if (focusFound == false) {
                            focusField = 'ddlConfigList_' + relcount;
                            focusFound = true;
                        }
                        $("#divErrorMessage").html(mandatoryFields).show();
                        $('#tdScope').addClass('input-validation-error');
                    }
                    else {
                        $('#tdScope').removeClass('input-validation-error');
                    }
                    if (ValidateMandatoryFieldsReleaseNew('ddlMusicType_' + relcount) == false) {
                        IsValid = false;
                        if (TabNo == null) {
                            TabNo = 2;
                        }
                        if (focusFound == false) {
                            focusField = 'ddlMusicType_' + relcount;
                            focusFound = true;
                        }
                        $("#divErrorMessage").html(mandatoryFields).show();
                        $('#tdScope').addClass('input-validation-error');
                    }
                    else {
                        $('#tdScope').removeClass('input-validation-error');
                    }
                    if (ValidateMandatoryFieldsReleaseNew('txtNoOfTrack_' + relcount) == false) {
                        IsValid = false;
                        if (TabNo == null) {
                            TabNo = 2;
                        }
                        if (focusFound == false) {
                            focusField = 'txtNoOfTrack_' + relcount;
                            focusFound = true;
                        }
                        $("#divErrorMessage").html(mandatoryFields).show();
                        $('#tdScope').addClass('input-validation-error');
                    }
                    else {
                        $('#tdScope').removeClass('input-validation-error');
                    }

                    //No of components Mandatory check.
                    if ($("#ddlPackage_" + relcount + " option:selected").text() == "Yes") {
                        if (ValidateMandatoryFieldsReleaseNew('txtNoOfComp_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            if (focusFound == false) {
                                focusField = 'txtNoOfComp_' + relcount;
                                focusFound = true;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                    }

                    //validations for Sales channel

                    if ($("#chkRegularRetail").is(':checked')) {
                        if ($("#chkRegDeviatedICLA_" + relcount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtRegAreaComments_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtRegAreaComments_' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('txtRegAreaComments_' + relcount).removeClass('input-validation-error');
                        }
                        if ($("#chkRegDeviatedICLA_" + relcount).is(':checked') && $('#ddlRegICLALevel_' + relcount + ' option:selected').text() == "--Select--") {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'ddlRegICLALevel_' + relcount;
                                focusFound = true;
                            }
                            $('ddlRegICLALevel_' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('ddlRegICLALevel_' + relcount).removeClass('input-validation-error');
                        }

                    }
                    else {
                        $('txtRegAreaComments_' + relcount).removeClass('input-validation-error');
                        $('ddlRegICLALevel_' + relcount).removeClass('input-validation-error');
                    }

                    if ($("#chkTVRadioBreakICLA").is(':checked')) {
                        if ($("#chkTVDeviatedICLA_" + relcount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtTVAreaComments_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtTVAreaComments_' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                            $('txtTVAreaComments_' + relcount).removeClass('input-validation-error');
                        }

                        if ($("#chkTVDeviatedICLA_" + relcount).is(':checked') && $('#ddlTVICLALevel_' + relcount + ' option:selected').text() == "--Select--") {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'ddlTVICLALevel_' + relcount;
                                focusFound = true;
                            }
                            $('#ddlTVICLALevel_' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('#ddlTVICLALevel_' + relcount).removeClass('input-validation-error');
                        }
                    }
                    else {
                        $('#tdScope').removeClass('input-validation-error');
                        $('txtTVAreaComments_' + relcount).removeClass('input-validation-error');
                        $('ddlTVICLALevel_' + relcount).removeClass('input-validation-error');
                    }

                    if ($("#chkClub").is(':checked')) {
                        if ($("#chkClubDeviatedICLA_" + relcount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtClubAreaComments_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtClubAreaComments_' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#txtClubAreaComments_' + relcount).removeClass('input-validation-error');
                        }

                        if ($("#chkClubDeviatedICLA_" + relcount).is(':checked') && $('#ddlClubICLALevel_' + relcount + ' option:selected').text() == "--Select--") {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'ddlClubICLALevel_' + relcount;
                                focusFound = true;
                            }
                            $('#ddlClubICLALevel_' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('#ddlClubICLALevel_' + relcount).removeClass('input-validation-error');
                        }
                    }
                    else {
                        $('txtClubAreaComments_' + relcount).removeClass('input-validation-error');
                        $('ddlClubICLALevel_' + relcount).removeClass('input-validation-error');
                    }

                    if ($("#chkPromotional").is(':checked')) {
                        if ($("#chkPromoDeviatedICLA_" + relcount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtPromoAreaComments_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtPromoAreaComments_' + relcount;
                                focusFound = true;
                            }
                            $('#txtPromoAreaComments_' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('#txtPromoAreaComments_' + relcount).removeClass('input-validation-error');
                        }

                        if ($("#chkPromoDeviatedICLA_" + relcount).is(':checked') && $('#ddlPromoICLALevel_' + relcount + ' option:selected').text() == "--Select--") {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'ddlPromoICLALevel_' + relcount;
                                focusFound = true;
                            }
                            $('#ddlPromoICLALevel_' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('#ddlPromoICLALevel_' + relcount).removeClass('input-validation-error');
                        }
                    }
                    else {
                        $('#txtPromoAreaComments_' + relcount).removeClass('input-validation-error');
                        $('ddlPromoICLALevel_' + relcount).removeClass('input-validation-error');
                    }
                    if ($("#chkNonTrditional").is(':checked')) {
                        if ((!$('#rdoNonTrad1' + relcount).is(':checked')) && (!$('#rdoNonTrad2' + relcount).is(':checked'))) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'rdoNonTrad1' + relcount;
                                focusFound = true;
                            }
                            $('#dvNonTraditionalRelease' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('#dvNonTraditionalRelease' + relcount).removeClass('input-validation-error');
                        }

                        if ($("#chkNonTradDeviatedICLA_" + relcount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtNonTradAreaComments_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtNonTradAreaComments_' + relcount;
                                focusFound = true;
                            }
                            $('#txtNonTradAreaComments_' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('#txtNonTradAreaComments_' + relcount).removeClass('input-validation-error');
                        }

                        if ($("#chkNonTradDeviatedICLA_" + relcount).is(':checked') && $('#ddlNonICLALevel_' + relcount + ' option:selected').text() == "--Select--") {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'ddlNonICLALevel_' + relcount;
                                focusFound = true;
                            }
                            $('#ddlNonICLALevel_' + relcount).addClass('input-validation-error');
                        }
                        else {
                            $('#ddlNonICLALevel_' + relcount).removeClass('input-validation-error');
                        }
                    }
                    else {
                        $('#txtNonTradAreaComments_' + relcount).removeClass('input-validation-error');
                        $('ddlNonICLALevel_' + relcount).removeClass('input-validation-error');
                    }
                    // validations for Exact PPD
                    if ($("#chkTVRadioBreakICLA").is(':checked')) {
                        if (ValidateMandatoryFieldsReleaseNew('txtExPPD_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtExPPD_' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                        // Validation for estimation detail
                        if (ValidateMandatoryFieldsReleaseNew('txtEstRetail_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtEstRetail_' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                    }
                    // Validation for estimation detail
                    if ($("#cmbManByUMG option:selected").text() == "Yes") {
                        if (ValidateMandatoryFieldsReleaseNew('txtInvPrice_' + relcount) == false &&
                            $('#rdoNonTrad1' + relcount).is(':checked')) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtInvPrice_' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                    }

                    // Validation for Accounting Base
                    if ($('#rdoNonTrad1' + relcount).is(':checked') && $("#chkNonTrditional").is(':checked')) {
                        //For ICLA Accounting Base
                        if (($("#cmbManByUMG option:selected").text() == "Yes" && $('#chkPartwork').is(':checked')) || ($("#cmbManByUMG option:selected").text() == "No" && (!$('#chkGiveAwayFreeOfCharge').is(':checked')))) {
                            if (ValidateMandatoryFieldsReleaseNew('txtICLAcc_' + relcount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtICLAcc_' + relcount;
                                    focusFound = true;
                                }
                                $('txtICLAcc_' + relcount).addClass('input-validation-error');
                            }
                            else {
                                $('txtICLAcc_' + relcount).removeClass('input-validation-error');
                            }
                        }
                        if (ValidateMandatoryFieldsReleaseNew('txtICLALevelNon' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtICLALevelNon' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                    }
                    else {
                        $('txtICLALevelNon' + relcount).removeClass('input-validation-error');
                        $('txtICLAcc_' + relcount).removeClass('input-validation-error');
                    }


                    if ($("#chkNonTrditional").is(':checked') && $("#chkGiveAwayFreeOfCharge").is(':checked') && ($("#cmbManByUMG option:selected").text() == "No")) {
                        // Validation for Accounting Base
                        if ($('#rdoNonTrad1' + relcount).is(':checked')) {
                            if (ValidateMandatoryFieldsReleaseNew('txtDeemedPPD_' + relcount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtDeemedPPD_' + relcount;
                                    focusFound = true;
                                }
                                $('#tdScope').addClass('input-validation-error');
                            }
                            else {
                                $('#tdScope').removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                    }
                    // Validation for Accounting Base
                    if ($('#rdoNonTrad2' + relcount).is(':checked') && $("#chkNonTrditional").is(':checked')) {
                        $('#dvNonTraditionalRelease' + relcount).removeClass('input-validation-error');
                        if (ValidateMandatoryFieldsReleaseNew('txtResourceFee_' + relcount) == false) {
                            IsValid = false;
                            if (TabNo == null) {
                                TabNo = 2;
                            }
                            $("#divErrorMessage").html(mandatoryFields).show();
                            if (focusFound == false) {
                                focusField = 'txtResourceFee_' + relcount;
                                focusFound = true;
                            }
                            $('#tdScope').addClass('input-validation-error');
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                    }

                }

            }
            //end validations for sales channel

            if (IsValid == true) {
                var IsResources = false;
                if (CountResource == '' || CountResource == 0) {
                    for (relcount = 0; relcount <= Count - 1; relcount++) {
                        if ($("#ddlPackage_" + relcount + " option:selected").text() == "Yes") {
                            if (ValidateMandatoryFieldsReleaseNew('PackageIncludedUPC' + relcount) == false) {
                                IsResources = false;
                            }
                            else {
                                IsResources = true;
                                break;
                            }
                        }
                    }
                    if (IsResources == false) {
                        if (TabNo == null) {
                            TabNo = 3;
                        }
                        $("#divErrorMessage").html(mandatoryResourceFields).show();
                        $("#divMessage").hide();
                        IsValid = false;
                    }
                }
                else {
                    for (i = 0; i <= CountResource - 1; i++) {
                        if ($("#hdnR2ResourceId" + i).val() == 0) {
                            if (!ValidateDiv('divArtistNameRegular' + i + '', 'BtnManagerArtistFreeHandRegular' + i + '')) { if (TabNo == null) { TabNo = 2; } IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'BtnManagerArtistFreeHandRegular' + i + ''; focusFound = true; } }
                        }
                        if ($('#chkGloballyCleared' + i).is(':checked')) {

                            if ($('#txtGloballyCleared' + i).val() == "") {
                                $("#divErrorMessage").html(mandatoryFields).show();
                                $('#txtGloballyCleared' + i).addClass('btn-validation-error');
                                $("#divMessage").hide();
                                if (TabNo == null) {
                                    TabNo = 3;
                                }
                                IsValid = false;
                            }
                            else {
                                $('#txtGloballyCleared' + i).removeClass('btn-validation-error');
                            }
                        }
                        else {
                            $('#txtGloballyCleared' + i).removeClass('btn-validation-error');
                        }
                    }
                }

            }
        }
        else {
            // validation of mandatory fields of ReleaseExisting
            if ($("#ReleaseModelCount").val() > 0) {
                for (icount = 0; icount < $("#ReleaseModelCount").val() ; icount++) { // validations for all sales channel

                    var archflg = "#Archive" + icount;
                    if ($(archflg).val() == "N") {
                        if ($("#chkRegularRetail").is(':checked')) {
                            if ($("#chkIsDeviatedICLALevel-Regular" + icount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtComments-Regular' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtComments-Regular' + icount;
                                    focusFound = true;
                                }
                                $('#tdScope').addClass('input-validation-error');
                            }
                            else {
                                $('#tdScope').removeClass('input-validation-error');
                                $('txtComments-Regular' + icount).removeClass('input-validation-error');
                            }
                            if ($("#chkIsDeviatedICLALevel-Regular" + icount).is(':checked') && $('#ddlDevICLALevel-Regular' + icount + ' option:selected').text() == "--Select--") {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'ddlDevICLALevel-Regular' + icount;
                                    focusFound = true;
                                }
                                $('#ddlDevICLALevel-Regular' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#ddlDevICLALevel-Regular' + icount).removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('txtComments-Regular' + icount).removeClass('input-validation-error');
                            $('ddlDevICLALevel-Regular' + icount).removeClass('input-validation-error');
                        }
                        if ($("#chkTVRadioBreakICLA").is(':checked')) {
                            if ($("#chkIsDeviatedICLALevel-TVRadio" + icount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtComments-TVRadio' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtComments-TVRadio' + icount;
                                    focusFound = true;
                                }
                                $('#tdScope').addClass('input-validation-error');
                            }
                            else {
                                $('#tdScope').removeClass('input-validation-error');
                                $('txtComments-TVRadio' + icount).removeClass('input-validation-error');
                            }

                            if ($("#chkIsDeviatedICLALevel-TVRadio" + icount).is(':checked') && $('#ddlDevICLALevel-TVRadio' + icount + ' option:selected').text() == "--Select--") {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'ddlDevICLALevel-TVRadio' + icount;
                                    focusFound = true;
                                }
                                $('#ddlDevICLALevel-TVRadio' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#ddlDevICLALevel-TVRadio' + icount).removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('txtComments-TVRadio' + icount).removeClass('input-validation-error');
                            $('ddlDevICLALevel-TVRadio' + icount).removeClass('input-validation-error');
                        }
                        if ($("#chkPriceReduction").is(':checked')) {
                            if ($("#chkIsDeviatedICLALevel-PriceReduction" + icount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtComments-PriceReduction' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtComments-PriceReduction' + icount;
                                    focusFound = true;
                                }
                                $('#txtComments-PriceReduction' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#txtComments-PriceReduction' + icount).removeClass('input-validation-error');
                            }
                            if ($("#chkIsDeviatedICLALevel-PriceReduction" + icount).is(':checked') && $('#ddlDevICLALevel-PriceReduction' + icount + ' option:selected').text() == "--Select--") {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'ddlDevICLALevel-PriceReduction' + icount;
                                    focusFound = true;
                                }
                                $('#ddlDevICLALevel-PriceReduction' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#ddlDevICLALevel-PriceReduction' + icount).removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('#txtComments-PriceReduction' + icount).removeClass('input-validation-error');
                            $('ddlDevICLALevel-PriceReduction' + icount).removeClass('input-validation-error');
                        }

                        if ($("#chkClub").is(':checked')) {
                            if ($("#chkIsDeviatedICLALevel-Club" + icount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtComments-Club' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtComments-Club' + icount;
                                    focusFound = true;
                                }
                                $('#txtComments-Club' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#txtComments-Club' + icount).removeClass('input-validation-error');
                            }

                            if ($("#chkIsDeviatedICLALevel-Club" + icount).is(':checked') && $('#ddlDevICLALevel-Club' + icount + ' option:selected').text() == "--Select--") {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'ddlDevICLALevel-Club' + icount;
                                    focusFound = true;
                                }
                                $('#ddlDevICLALevel-Club' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#ddlDevICLALevel-Club' + icount).removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('txtComments-Club' + icount).removeClass('input-validation-error');
                            $('ddlDevICLALevel-Club' + icount).removeClass('input-validation-error');
                        }

                        if ($("#chkPromotional").is(':checked')) {
                            if ($("#chkIsDeviatedICLALevel-Promotional" + icount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtComments-Promotional' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtICLALevel-Promotional' + icount;
                                    focusFound = true;
                                }
                                $('#txtICLALevel-Promotional' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#txtICLALevel-Promotional' + icount).removeClass('input-validation-error');
                            }

                            if ($("#chkIsDeviatedICLALevel-Promotional" + icount).is(':checked') && $('#ddlDevICLALevel-Promotional' + icount + ' option:selected').text() == "--Select--") {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'ddlDevICLALevel-Promotional' + icount;
                                    focusFound = true;
                                }
                                $('#ddlDevICLALevel-Promotional' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#ddlDevICLALevel-Promotional' + icount).removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('#txtICLALevel-Promotional' + icount).removeClass('input-validation-error');
                            $('ddlDevICLALevel-Promotional' + icount).removeClass('input-validation-error');
                        }
                        if ($("#chkNonTrditional").is(':checked')) {

                            if ((!$('#rdoNonTrad1' + icount).is(':checked')) && (!$('#rdoNonTrad2' + icount).is(':checked'))) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'rdoNonTrad1' + icount;
                                    focusFound = true;
                                }
                                $('#dvNonTraditionalRelease' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#dvNonTraditionalRelease' + icount).removeClass('input-validation-error');
                            }

                            if ($("#chkIsDeviatedICLALevel-Non" + icount).is(':checked') && ValidateMandatoryFieldsReleaseNew('txtComments-Non' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtComments-Non' + icount;
                                    focusFound = true;
                                }
                                $('#txtComments-Non' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#txtComments-Non' + icount).removeClass('input-validation-error');
                            }

                            if ($("#chkIsDeviatedICLALevel-Non" + icount).is(':checked') && $('#ddlDevICLALevel-Non' + icount + ' option:selected').text() == "--Select--") {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'ddlDevICLALevel-Non' + icount;
                                    focusFound = true;
                                }
                                $('#ddlDevICLALevel-Non' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#ddlDevICLALevel-Non' + icount).removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('#txtComments-Non' + icount).removeClass('input-validation-error');
                            $('ddlDevICLALevel-Non' + icount).removeClass('input-validation-error');
                        }
                        // validations for Exact PPD
                        if ($("#chkTVRadioBreakICLA").is(':checked')) {
                            if (ValidateMandatoryFieldsReleaseNew('txtExactPPD-TVRadio' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtExactPPD-TVRadio' + icount;
                                    focusFound = true;
                                }
                                $('#txtExactPPD-TVRadio' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#txtExactPPD-TVRadio' + icount).removeClass('input-validation-error');
                            }
                            // Validation for estimation detail
                            if (ValidateMandatoryFieldsReleaseNew('txtEstRetail-TVRadio' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtEstRetail-TVRadio' + icount;
                                    focusFound = true;
                                }
                                $('#txtEstRetail-TVRadio' + icount).addClass('input-validation-error');
                            }
                            else {
                                $('#txtEstRetail-TVRadio' + icount).removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('#txtExactPPD-TVRadio' + icount).removeClass('input-validation-error');
                            $('#txtEstRetail-TVRadio' + icount).removeClass('input-validation-error');
                        }

                        // Validation for estimation detail
                        if ($("#cmbManByUMG option:selected").text() == "Yes") {
                            if (ValidateMandatoryFieldsReleaseNew('txtInvoicePrice-Non' + icount) == false && $('#rdoNonTrad1' + icount).is(':checked')) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtInvoicePrice-Non' + icount;
                                    focusFound = true;
                                }
                                $('#tdScope').addClass('input-validation-error');
                            }
                            else {
                                $('#tdScope').removeClass('input-validation-error');
                            }
                        }

                        // Validation for Accounting Base
                        if ($('#rdoNonTrad1' + icount).is(':checked')) {
                            $('#dvNonTraditionalRelease' + icount).removeClass('input-validation-error');
                            if ($("#cmbManByUMG option:selected").text() == "Yes" && $('#chkPartwork').is(':checked') || ($("#cmbManByUMG option:selected").text() == "No" && (!$('#chkGiveAwayFreeOfCharge').is(':checked')))) {
                                if (ValidateMandatoryFieldsReleaseNew('txtAccounting-Non' + icount) == false) {
                                    IsValid = false;
                                    if (TabNo == null) {
                                        TabNo = 2;
                                    }
                                    $("#divErrorMessage").html(mandatoryFields).show();
                                    if (focusFound == false) {
                                        focusField = 'txtAccounting-Non' + icount;
                                        focusFound = true;
                                    }
                                    $('txtAccounting-Non' + icount).addClass('input-validation-error');
                                }
                                else {
                                    $('txtAccounting-Non' + icount).removeClass('input-validation-error');
                                }
                            }
                            if (ValidateMandatoryFieldsReleaseNew('txtICLALevel-Non' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtICLALevel-Non' + icount;
                                    focusFound = true;
                                }
                                $('#tdScope').addClass('input-validation-error');
                            }
                            else {
                                $('#tdScope').removeClass('input-validation-error');
                            }
                        }
                        else {
                            $('txtICLALevel-Non' + icount).removeClass('input-validation-error');
                            $('txtAccounting-No' + icount).removeClass('input-validation-error');
                        }
                        if ($("#chkNonTrditional").is(':checked') && $("#chkGiveAwayFreeOfCharge").is(':checked') && ($("#cmbManByUMG option:selected").text() == "No")) {
                            // Validation for Accounting Base
                            if ($('#rdoNonTrad1' + icount).is(':checked')) {
                                if (ValidateMandatoryFieldsReleaseNew('txtDeemedPPD-Non' + icount) == false) {
                                    IsValid = false;
                                    if (TabNo == null) {
                                        TabNo = 2;
                                    }
                                    $("#divErrorMessage").html(mandatoryFields).show();
                                    if (focusFound == false) {
                                        focusField = 'txtDeemedPPD-Non' + icount;
                                        focusFound = true;
                                    }
                                    $('#tdScope').addClass('input-validation-error');
                                }
                                else {
                                    $('#tdScope').removeClass('input-validation-error');
                                }
                            }
                        }
                        else {
                            $('#tdScope').removeClass('input-validation-error');
                        }
                        // Validation for Accounting Base
                        if ($('#rdoNonTrad2' + icount).is(':checked')) {
                            if (ValidateMandatoryFieldsReleaseNew('txtResourceFee-Non' + icount) == false) {
                                IsValid = false;
                                if (TabNo == null) {
                                    TabNo = 2;
                                }
                                $("#divErrorMessage").html(mandatoryFields).show();
                                if (focusFound == false) {
                                    focusField = 'txtResourceFee-Non' + icount;
                                    focusFound = true;
                                }
                                $('#tdScope').addClass('input-validation-error');
                            }
                            else {
                                $('#tdScope').removeClass('input-validation-error');
                            }
                        }

                    }
                }
            }
            else {

                IsValid = false;
                if (focusFound == false) {
                    focusField = 'txtUPC';
                    focusFound = true;
                }
                if (TabNo == null) {
                    TabNo = 2;
                    $("#divErrorMessage").html("Please link atleast one release").show();
                }
                $('#divErrorMessage').addClass('warning');
                $("#divErrorMessage").show();
                $('#tdScope').addClass('input-validation-error');


            }
        }

        if (IsValid == false) {
            $('#' + focusField).focus();
            $('#divErrorMessage').addClass('warning');
            $("#divErrorMessage").show();
            $("#tabs").tabs({ selected: TabNo });
            return false;
        }
        else {
            $("#divErrorMessage").text("");
            $("#divErrorMessage").hide();
            $('#divErrorMessage').removeClass('warning');
            return true;
        }


    }

    function Validate() {
        var IsValid = true;
        var focusFound = false;
        var focusField = "";
        $("#divErrorMessage").text(""); //Set the Validation message blank

        if (ValidateMandatoryFields('txtEstimatedSalesUnit') == false) {
            IsValid = false;
            if (focusFound == false) {
                focusField = 'txtEstimatedSalesUnit';
                focusFound = true;
            }
            $('#tdScope').addClass('input-validation-error');
        }
        else {
            $('#tdScope').removeClass('input-validation-error');
        }

        if ($("#chkOneStop").is(':checked') && ValidateMandatoryFields('txtOneStopReason') == false) {
            IsValid = false;
            if (focusFound == false) {
                focusField = 'txtOneStopReason';
                focusFound = true;
            }
            $('#tdScope').addClass('input-validation-error');
        }
        else {
            $('#tdScope').removeClass('input-validation-error');
        }

        if (IsValid == false) {
            $('#' + focusField).focus();
            $("#divErrorMessage").text(mandatoryFields);

            return false;
        }
        else {
            $("#divErrorMessage").text("");
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

});