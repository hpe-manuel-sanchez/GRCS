var msg_ErrorFetchingDataFromServer = "Error fetching data from server.";
var GlobalClickSave;
var globalPostBackCheck = false;
var currencyDdwnFocus = 0;
var lstCombinedDataList = null;
var lstRoutedItems = "";
var ResourceId = "";
var IsCancelRejectRescExist = "";

var masterProjectMessages = {
    cancelValidSave: 'No details are entered for the project. Do you want to save the blank project?',
    confim: "Confirm"
};

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
    var objTerritorialPopup = $('<div id="Territory"></div>')
        .html('<p>Loading</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: manageTerritoryTitle,
            show: 'clip',
            hide: 'clip',
            width: "98%",
            height: 510,
            minHeight: 510,
            close: function () {
                $(this).remove(); // ensures any form variables are reset.
            }
        })
        .load(    //Load partial view
            $.ajax({
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
                                        title: masterProjectMessages.confim,
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
                                        title: masterProjectMessages.confim,
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

    if (uniqueKey == "1" && $('#hdnTempterritoryDetailsForSave').val() != "" && $('#hdnTempterritoryDetailsForSave').val() != undefined && $('#hdnTempterritoryDetailsForSave').val() != tempVar) {
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
    }
    // GCS start
    sitecollection = territoryCollection;
    //Clear the Countries
    $("#selectedCountries_" + uniqueKey).html('');
    $("#UnselectedCountries_" + uniqueKey).html('');
    var stringOfcountriesterritories = "";
    var stringOfexcountriesterritories = "";
    includedTerritoriesListResult = [];
    excludedTerritoriesListResult = [];
    var excludeCountryFinalString = '';
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
                                //review right
                            } else {
                                var parent = getParent(excludedCountry.ParentId);
                                var rootParent = getRootParent(null, excludedCountry.ParentId, true); //Get root parent which is selected
                                if (rootParent != null && (rootParent.Id == includedTerri.Id) && parent.IsExcluded == false && !checkifDuplicatesParentIsExcluded(excludedCountry) && rootParent.IsIncluded == true) {
                                    excludeCountryString += excludedCountry.Name + '; ';
                                    excludedCountriesList.push(excludedCountry.Name);
                                    //review right
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

    //=====================================
    if (CheckexcludedTerrString == "" && Checkexcludedsubstring == "") {
        if (excludedTerritories.items.length > 0 || (uniqueKey != 1 && uniqueKey != 2 && excludedCountries.items.length > 0)) {
            var excludeTerriString = '';
            var excludeSubTerriString = '';
            var excludedTerritoriesList = [];
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
    }

    //=======================================
    includedTerritoriesListGCS = includedTerritoriesList;
    if (territoryExcludeString != '') {
        if (territoryString == '') {
            territoryString = territoryExcludeString;
        } else {
            territoryString = territoryExcludeString + ' ' + territoryString;
        }
    }

    //Included Countries
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
                    //review right
                } else if (parent != null && parent.IsIncluded == false || (rootParent != null && rootParent.IsIncluded == false)) {
                    includeCountryString += includedCountry.Name + '; ';
                    includedCountriesList.push(includedCountry.Name);
                    //review right
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
    //working
    //Included Countries
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

function generateEmail(emailType) {
    var projectId = $('#hdnclrProjectId').val();
    if (projectId != 0) {
        displayLoadingPanel(true);
        var values =
                {
                    "emailType": emailType,
                    "clrProjectId": projectId
                }
        $.ajax({
            url: '/GCS/ClearanceInbox/GenerateEmail',
            type: 'POST',
            async: false,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(values),
            success: function (data) {
                displayLoadingPanel(false);
                if (data != "") {
                    $("#divErrorMessage").html(data).show();
                    $("#divMessage").hide();
                    $('#divErrorMessage').addClass('warning');
                }
                else {
                    $("#divErrorMessage").html('Mail has been triggered.').show();
                    $('#divErrorMessage').addClass('success');
                    $("#divErrorMessage").show();
                }
            }
        });
    }
}

function displayMessage(truefalse, messageType, messageText, contentType) {
    var messageElement = $('#divMessage');
    messageElement.text('')
                  .html('')
                  .removeClass()
                  .hide();

    if (truefalse) {
        messageElement.text(messageText);
        messageElement.addClass(messageType)
        messageElement.show();
    }
    applyCustomTheam();
}

function displayLoadingPanel(truefalse) {
    var loadingPanel = $($.find('#loadingDivPA'));
    if (truefalse) {
        displayMessage(false);
        loadingPanel.show();
    }
    else {
        loadingPanel.hide();
    }
}

function RefreshEmailPanel(projectStatus, RoleName) {
    //hide all the item of email's menu and button
    $("#ulMailList li").hide();
    $("#btnEmail").hide();

    if (projectStatus != 1) {
        $("#btnEmail").show();
        $("#lnkEmailGeneral").show();
        $("#lnkEmailMaster").show();
        $("#lnkEmailMasterWithReviewStatus").show();
        $("#lnkEmailArtistManagmentMaster").show();
    }
}

function UpdateRoutingTriggerFlagProjectLevel() {
    var flag = false;
    var ResourceCount = $('#hdnRSResourceCount').val();
    for (var rowi = 0; rowi < ResourceCount; rowi++) {
        if (document.getElementById("hdnRSClearanceResoruceId" + rowi) != null) {
            if ((document.getElementById("hdnRSApprovalStatusId" + rowi).value == 5) || (document.getElementById("hdnRSApprovalStatusId" + rowi).value == 7)
                                || document.getElementById("hdnRSApprovalStatusId" + rowi).value == 10) {
                flag = true;
                IsCancelRejectRescExist = true;
            }
        }
    }
    return flag;
}

function UpdateRoutingTriggerFlag(ListResourceId) { //Update Routing Trigger flag in Request Summary tab
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
                var ResourceCount = $('#hdnRSResourceCount').val();
                for (var rowi = 0; rowi < ResourceCount; rowi++) {
                    if (document.getElementById("hdnRSClearanceResoruceId" + rowi) != null) {
                        if (document.getElementById("hdnRSClearanceResoruceId" + rowi).value == ListResourceId[j]) {
                            if ((document.getElementById("hdnRSApprovalStatusId" + rowi).value != 13)
                                && (document.getElementById("hdnRSApprovalStatusId" + rowi).value != 5)
                                && (document.getElementById("hdnRSApprovalStatusId" + rowi).value != 6)
                                && (document.getElementById("hdnRSApprovalStatusId" + rowi).value != 7)
                                && (document.getElementById("hdnRSApprovalStatusId" + rowi).value != 10)) {//Id of Approved, waiting and Conditionaly approved
                                if (document.getElementById("hdnRSIsRoutingTriggered" + rowi) !== null)
                                    document.getElementById("hdnRSIsRoutingTriggered" + rowi).value = "true";
                            }
                            if ((document.getElementById("hdnRSApprovalStatusId" + rowi).value == 5) || (document.getElementById("hdnRSApprovalStatusId" + rowi).value == 7) || (document.getElementById("hdnRSApprovalStatusId" + rowi).value == 10)) {
                                IsCancelRejectRescExist = true;
                            }
                        }
                    }

                }
            }
        }
    }
    return flag;
}

function UpdateAllRequestSummaryResource() {
    var flag = true;
    var ResourceCount = $('#hdnRSResourceCount').val();
    for (var i = 0; i < ResourceCount; i++) {
        if ((document.getElementById("hdnRSApprovalStatusId" + i).value != 13) && (document.getElementById("hdnRSApprovalStatusId" + i).value != 5) && (document.getElementById("hdnRSApprovalStatusId" + i).value != 6)
        && (document.getElementById("hdnRSApprovalStatusId" + i).value != 7) && (document.getElementById("hdnRSApprovalStatusId" + i).value != 10)) {//Id of Approved, waiting and Conditionaly approved
            if (document.getElementById("hdnRSIsRoutingTriggered" + i) !== null)
                document.getElementById("hdnRSIsRoutingTriggered" + i).value = "true";
        }
    }
    return flag;
}

function UpdateSensitiveListFlagsForResource(sensitiveResourceListConditionChanged) {
    for (var i = 0; i < sensitiveResourceListConditionChanged.length; i++) {
        document.getElementById('HasUMGIConditionChanged' + sensitiveResourceListConditionChanged[i]).value = "true";
    }
}

function UpdateFalseRequestSummaryResource() {
    var flag = true;
    var ResourceCount = $('#hdnRSResourceCount').val();
    for (var i = 0; i < ResourceCount; i++) {
        if (document.getElementById("hdnRSIsRoutingTriggered" + i) !== null)
            document.getElementById("hdnRSIsRoutingTriggered" + i).value = "false";
    }
}

function DiscardAmendment() {
    var digitalVal = $('#CreateMasterProject').serialize();
    var selectedTab = $("#tabs").tabs('option', 'selected');
    $('#hdnAdditionalResourceCheck').val("NO");
    window.location.href = '/GCS/ClearanceProject/DiscardMasterResubmission?selectedTab=' + selectedTab + '&clrProjectId=' + $('#hdnclrProjectId').val();
}

function IsValidationWithCancelRejectedResourceLevel(routedItems) {
    $('#loadingDivPA').hide();
    if (routedItems != "") {
        UpdateSelectedCancelledRejectedResource(routedItems);
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        $('#hdncommandMaster').val("save");
        IsAdditionalResourcesAdded();
    }
    else {
        $('#hdncommandMaster').val("save");
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        IsAdditionalResourcesAdded();
    }
}

function UpdateSelectedCancelledRejectedResource(routedItems) {
    var routedItemList = routedItems.split(',');
    for (var i = 0; i < routedItemList.length; i++) {
        if (routedItemList[i] > 0) {
            var ResourceCount = $('#hdnRSResourceCount').val();
            for (var rowi = 0; rowi < ResourceCount; rowi++) {
                if (document.getElementById("hdnRequestInfoRoutedItemId" + rowi) != null) {
                    if (document.getElementById("hdnRequestInfoRoutedItemId" + rowi).value == routedItemList[i]) {
                        if ((document.getElementById("hdnRSApprovalStatusId" + rowi).value == 5)
                            || (document.getElementById("hdnRSApprovalStatusId" + rowi).value == 7)
                            || (document.getElementById("hdnRSApprovalStatusId" + rowi).value == 10)) {//Id of Cancelled and Rejected Resource
                            if (document.getElementById("hdnRSIsRoutingTriggered" + rowi) !== null)
                                document.getElementById("hdnRSIsRoutingTriggered" + rowi).value = "true";
                        }
                    }
                }
            }
        }
    }
}

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

function ValidateMandatoryFields(FieldName) {
    if (($('#' + FieldName).attr("value") == null || $('#' + FieldName).attr("value") == "0" || $('#' + FieldName).val() == "" || $('#' + FieldName).attr("value") == "")) {
        $('#' + FieldName).addClass('input-validation-error');
        return false;
    }
    else {
        $('#' + FieldName).removeClass('input-validation-error');
        return true;
    }
}

function ValidateOnResubmit() {
    $("#divMessage").hide();
    //upload Message
    $("#divErrorMessage1").hide();
    var IsValid = true;
    var NotValid = false;
    var focusFound = false;
    var focusField = '';
    if (!ValidateMandatoryFields('txtProjectTitle')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtProjectTitle'; focusFound = true; } }
    if ($('#chkOS').is(':checked')) {
        if (!ValidateMandatoryFields('OneStopReason')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'OneStopReason'; focusFound = true; } }
    }

    if (!ValidateMandatoryFields('txtClientName')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtClientName'; focusFound = true; } }
    if (!ValidateMandatoryFields('txtLicenseTerm')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtLicenseTerm'; focusFound = true; } }

    if (!(ValidateCheckbox('chkAdvertising', 'tdRequestType') || ValidateCheckbox('chkTrailer', 'tdRequestType2') || ValidateCheckbox('chkFilm', 'tdRequestType2') || ValidateCheckbox('chkOther', 'tdRequestType3'))) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'tdRequestType'; focusFound = true; } }
    //validating Advertising
    if ($('#chkAdvertising').is(':checked')) {
        if (!ValidateMandatoryFields('txtAdvertisedProducts')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtAdvertisedProducts'; focusFound = true; } }
        if (!(ValidateCheckbox('chkAdvertisingVideo', 'chkAdver_Video_Other') || ValidateCheckbox('chkAdvertisingOther', 'chkAdver_Video_Other') || ValidateCheckbox('chkAdvertisingRadio', 'chkAdver_TV_Radio') || ValidateCheckbox('chkAdvertisingInternet', 'chkAdver_Cinema_Internet') || ValidateCheckbox('chkAdvertisingCinema', 'chkAdver_Cinema_Internet') || ValidateCheckbox('chkAdvertisingTV', 'chkAdver_TV_Radio'))) { IsValid = false; NotValid = true; }
        if ($('#chkAdvertisingOther').is(':checked')) {
            if (!ValidateMandatoryFields('txtAdvertiseOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtAdvertiseOtherComments'; focusFound = true; } }
        }
    }
    if ($('#chkFilm').is(':checked')) {
        if (!(ValidateCheckbox('chkFilmTV', 'chkFilmTV_Internet') || ValidateCheckbox('chkFilmInternet', 'chkFilmTV_Internet') || ValidateCheckbox('chkFilmCinema', 'chkFilmCinema_Trailor') || ValidateCheckbox('chkFilmTrailer', 'chkFilmCinema_Trailor') || ValidateCheckbox('chkFilmVideo', 'chkFilmVideo_Other') || ValidateCheckbox('chkFilmOther', 'chkFilmVideo_Other'))) { IsValid = false; NotValid = true; }

        if ($('#chkFilmOther').is(':checked')) {
            if (!ValidateMandatoryFields('txtFilmOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'txtFilmOtherComments'; focusFound = true; } }
        }
    }
    if ($('#chkTrailer').is(':checked')) {
        if (!(ValidateCheckbox('chkTrailersTV', 'ChkTrailersTV_Internet') || ValidateCheckbox('chkTrailersInternet', 'ChkTrailersTV_Internet') || ValidateCheckbox('chkTrailersCinema', 'ChkTrailersCinema_Video') || ValidateCheckbox('chkTrailersOther', 'ChkTrailersOther') || ValidateCheckbox('chkTrailersVideo', 'ChkTrailersCinema_Video'))) { IsValid = false; NotValid = true; }

        if ($('#chkTrailersOther').is(':checked')) {
            if (!ValidateMandatoryFields('TrailerOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'TrailerOtherComments'; focusFound = true; } }
        }
    }
    if ($('#chkOther').is(':checked')) {
        if (!(ValidateCheckbox('chkOthersTV', 'chkOthersTV_Radio') || ValidateCheckbox('chkOthersRadio', 'chkOthersTV_Radio') || ValidateCheckbox('chkOthersCinema', 'chkOthersCinema_Internet') || ValidateCheckbox('chkOthersInternet', 'chkOthersCinema_Internet') || ValidateCheckbox('chkOthersVideo', 'chkOthersVideo_Other') || ValidateCheckbox('chkOthersOther', 'chkOthersVideo_Other'))) { IsValid = false; NotValid = true; }
        if ($('#chkOthersOther').is(':checked')) {
            if (!ValidateMandatoryFields('OthersOtherComments')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'OthersOtherComments'; focusFound = true; } }
        }
    }
    if (!ValidateDiv('selectedCountries_1', 'btnManageTerritories')) { IsValid = false; NotValid = true; if (focusFound == false) { focusField = 'selectedCountries_1'; focusFound = true; } }

    if (IsValid == false && NotValid == true) {
        $("#divErrorMessage").html(mandatoryFields).show();
        $("#divErrorMessage").addClass('input-validation-error');
        $('#' + focusField).focus();
        $("#tabs").tabs({ selected: 0 });
        return false;
    }

    else if ($('#hdnArchiveFlag0').val() == null) {
        $("#tabs").tabs({ selected: 1 });
        $("#divErrorMessage").html(mandatoryResourceFields).show();
        $("#divMessage").hide();
        return false;
    }

    else if ($('#hdnArchiveFlag0').val() != null) {
        var index = $("#tabs").tabs('option', 'selected');
        $("#tabs").tabs({ selected: index });
        var Count = $('#hdnResourceListCount').val();
        var CountResourcesArchived = $('#hdnResourceListCount').val();
        for (i = 0; i <= CountResourcesArchived - 1; i++) {
            if ($('#hdnArchiveFlag' + i).val() == "Y") {
                Count = Count - 1;
            }
        }
        if (Count <= 0) {
            $("#tabs").tabs({ selected: 1 });
            $("#divErrorMessage").html(mandatoryResourceFields).show();
            $("#divMessage").hide();
            return false;
        }
        var isValidated = true;
        for (i = 0; i <= Count - 1; i++) {
            if ($('#txtSuggestedFee' + i).val() == "") {
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtSuggestedFee' + i).addClass('btn-validation-error');
                $("#divMessage").hide();
                isValidated = false;
            }
            else {
                $('#txtSuggestedFee' + i).removeClass('btn-validation-error');
            }
            if ($('#txtExcerptTime' + i).val() == "") {
                $("#divErrorMessage").html(mandatoryFields).show();
                $('#txtExcerptTime' + i).addClass('btn-validation-error');
                $("#divMessage").hide();
                isValidated = false;
            }
            else {
                $('#txtExcerptTime' + i).removeClass('btn-validation-error');
            }
            if ($('#BtnManagerArtistFreeHand' + i) != null) {
                if ($('#divArtistName' + i).text() == "") {
                    $('#BtnManagerArtistFreeHand' + i).addClass('btn-validation-error');
                    isValidated = false;
                }
            }
            else {
                $('#BtnManagerArtistFreeHand' + i).removeClass('btn-validation-error');
            }
        }
        if (isValidated) {
            $("#divErrorMessage").html(mandatoryFields).hide();
            $("#divErrorMessage").text("");
            return true;
        }
        else {
            $("#divErrorMessage").html(mandatoryFields).show();
            $("#tabs").tabs({ selected: 1 });
            return false;
        }
        return isValidated;
    }
    else {
        $("#divErrorMessage").text("");
        return true;
    }
}

function SetFlagFalseForNewlyAddedResources() {
    var div = document.getElementById('tblResource');
    for (i = 0; i < div.children.length; i++) {
        hdnId = i + "hdnIsNewAdded" + $('#hdnClearanceResourceId' + i).val();
        $('#' + hdnId).val('false');
    }
}

function ApplyResubmittionReasons(reasonProjectResubmission, reasonResourceResubmission) {
    $('#hdnResubmitReasonComments').val(reasonProjectResubmission);
    var hiddenResourceVariable = 'hdnResourceResubmitReasonComments_';
    $('[id^=' + hiddenResourceVariable + ']').val('');
    for (var i = 0; i < reasonResourceResubmission.length; i++) {
        $('#' + hiddenResourceVariable + reasonResourceResubmission[i].Key).val(reasonResourceResubmission[i].Value);
    }
}

function ChkChangelevelInMasterProject(ListofLists) {
    var Intflag = 0;
    //if list is not null
    if (ListofLists != null) {
        //If there is project level change
        if (lstCombinedDataList[1] != null && lstCombinedDataList[1].length > 0) { //there is only project level change and change in special case
            var list = ListofLists[0];
            if (list[0] == 1) { //there is chnage in special case
                $("#hdnIsUMGiMarkettingRoutingRequired").val("true");
                Intflag = 1;
            }
            if (list[0] == 0) { //means no chnage in special case
                $("#hdnIsUMGiMarkettingRoutingRequired").val("false");
                Intflag = 1;
            }
        }

    }
    return Intflag;
}

function IsAdditionalResourcesAdded() {
    var Intflag = 0;
    if (lstCombinedDataList[4] != null && lstCombinedDataList[4].length > 0) {//If new resource is added
        //if new resource has been added
        Intflag = 5;
    }
    else {
        var val = SetRoutingFagForMaster();
        if (parseInt(val) != 0) {
            Intflag = 7;
        }
        else {
            Intflag = 6;
        }
    }
    if (Intflag == 5) { //There added new resource
        NewResourceAdded();
    }
    if (Intflag == 6) { //No changed for routing
        SetFlagFalseForNewlyAddedResources();
        $('#hdncommandMaster').val("save");
        var formId = window.parent.document.forms[0];
        globalPostBackCheck = true;
        formId.submit();
    }
    if (Intflag == 7) { //No changed for routing
        ReRouteResourcepreviouslyNotRouted();
    }
}

function PerformProjectLevelRouting(isSensitiveDataChanged) {
    var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionFieldsEditMsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: masterProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 420
            });
    objWarningDialog.dialog('open');
    objWarningDialog.dialog({
        buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        $('#hdnAdditionalResourceCheck').val("YES");
                        //if confirm yes
                        $('#hdnIsUpdateNextStatus').val(isSensitiveDataChanged); //Set the Hidden field value is true if there is project level change
                        UpdateFalseRequestSummaryResource(); //Initially update the IsRoutingTrigger flag set to false
                        UpdateAllRequestSummaryResource(); //Set the IsRoutingTrigger flag to true for all the Resources where Request type status is Approved ,Conditonally approved and Waiting
                        //If there exist cancelled and Rejected resoruces for the project
                        UpdateRoutingTriggerFlagProjectLevel();
                        if (IsCancelRejectRescExist == true) {
                            $('#loadingDivPA').show();
                            ResubmitCancelRejResourceatProejctlvl();
                        }
                        else {//If there does not exist Cancel and Rejected Request for all resource
                            UpdateFalseRequestSummaryResource();
                            UpdateAllRequestSummaryResource();
                            $('#hdncommandMaster').val("save");
                            IsAdditionalResourcesAdded();
                        }
                    },
                    'No': function () {
                        $(this).dialog('close');
                        //code for discard amendment at project level
                        $('#hdncommandMaster').val("");
                        DiscardAmendment();
                    }
                }
    });
    return true;
}

function PerformResourceSpecifiedFieldsRouting(ResourceSpecifiedFieldsList, sensitiveResourceListConditionChanged) {
    UpdateFalseRequestSummaryResource(); //Initially update the IsRoutingTrigger flag set to false
    UpdateSensitiveListFlagsForResource(sensitiveResourceListConditionChanged);
    var flag = UpdateRoutingTriggerFlag(ResourceSpecifiedFieldsList);
    if (flag) {//If there si change in Resoruce fields
        $('#hdnIsUpdateNextStatus').val("false");
        var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionResourceEditmsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: masterProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 465
            });
        objWarningDialog.dialog('open');
        objWarningDialog.dialog({
            buttons:
                {
                    'Yes': function (e) {
                        $(this).dialog('close');
                        $('#hdnAdditionalResourceCheck').val("YES");
                        ResetFlagForResourceMasterProject();
                        if (IsCancelRejectRescExist == true) {
                            $('#loadingDivPA').show();
                            var value =
                                        {
                                            "Isrc": ResourceId,
                                            "Id": $('#hdnclrProjectId').val()
                                        };
                            $.get('/GCS/ClearanceProject/GetCancelRejectedResource', value, function (data) {
                                IsValidationWithCancelRejectedResourceLevel(data.listfinal);
                            });
                        }
                        else {
                            $('#hdncommandMaster').val("save");
                            IsAdditionalResourcesAdded();
                        }
                    },
                    'No': function () {
                        $(this).dialog('close');
                        //code for discard amendment at project level
                        $('#hdncommandMaster').val("");
                        DiscardAmendment();
                    }
                }
        });
    }
    else {
        IsAdditionalResourcesAdded();
    }
}

function PerformResourceOtherFieldsRouting(ResourceOtherFieldsList, sensitiveResourceListConditionChanged) {
    UpdateSensitiveListFlagsForResource(sensitiveResourceListConditionChanged);
    //Update the IsRoutingTrigger flag for all the resource
    UpdateFalseRequestSummaryResource(); //Initially update the IsRoutingTrigger flag set to false
    var flag = UpdateRoutingTriggerFlag(ResourceOtherFieldsList);
    if (flag) {//If there si change in Resoruce fields
        $('#hdnIsUpdateNextStatus').val("false");
        var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionResourceOtherFieldEditmsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: masterProjectMessages.confim,
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
                        $('#hdnAdditionalResourceCheck').val("YES");
                        ResetFlagForResourceMasterProject();
                        if (IsCancelRejectRescExist == true) {
                            $('#loadingDivPA').show();
                            var value =
                                        {
                                            "Isrc": ResourceId,
                                            "Id": $('#hdnclrProjectId').val()
                                        };
                            $.get('/GCS/ClearanceProject/GetCancelRejectedResource', value, function (data) {
                                IsValidationWithCancelRejectedResourceLevel(data.listfinal);
                            });
                        }
                        else {
                            $('#hdncommandMaster').val("save");
                            IsAdditionalResourcesAdded();
                        }
                    },
                    'No': function () {
                        $(this).dialog('close');
                        //Simply update the changes
                        UpdateFalseRequestSummaryResource();

                        $('#hdncommandMaster').val("save");
                        IsAdditionalResourcesAdded();
                    }
                }
        });
    }
    else {
        IsAdditionalResourcesAdded();
    }
}

function NewResourceAdded() {
    //confirm message
    var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + msgConfirmNewResourceAdd + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: masterProjectMessages.confim,
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
                        $('#hdnAdditionalResourceCheck').val("YES");
                        $('#hdncommandMaster').val("save");
                        ResetFlagForResourceMasterProject();
                        var formId = window.parent.document.forms[0];
                        globalPostBackCheck = true;
                        formId.submit();
                    },
                    'No': function () {
                        $(this).dialog('close');
                        $('#hdnIsUpdateNextStatus').val("false"); //Simply update the changes
                        $('#hdncommandMaster').val("save");
                        UpdateFalseRequestSummaryResource();
                        SetFlagFalseForNewlyAddedResources();
                        var formId = window.parent.document.forms[0];
                        globalPostBackCheck = true;
                        formId.submit();
                    }
                }
    });
}

function PerformSavingUnsubmittedMasterProject() {
    if (ValidationCheckOnMasterUnsubmitted() == false) {
        GlobalClickSave = "save1Click";
        showConfirmMsgWarning(masterProjectMessages.cancelValidSave, GlobalClickSave
            );
    }
    else {
        $('#loadingDivPA').show();
        $('#btnSave1').click();
    }
}

function ValidationCheckOnMasterUnsubmitted() {
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
                return IsNotEmpty = true;
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
    if ((parseInt($("#ddlRequestingComp").val()) != parseInt($("#hdnfirstRequesterCompanyId").val())) && parseInt($("#ddlRequestingComp").val()) != 0) {//if new company is selected
        return IsNotEmpty = true;
    }
    return IsNotEmpty;
}

function showConfirmMsgWarning(str, YesCallback) {
    var flag = false;
    var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + str + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: masterProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 400
            });
    objWarningDialog.dialog('open');
    objWarningDialog.dialog({
        buttons:
                {
                    'Yes': function (e) {
                        flag = true;
                        $(this).dialog('close');
                        if (GlobalClickSave == "save1Click") {
                            globalPostBackCheck = true;
                            $("#btnSave1").click();
                        }
                        return flag;
                    },
                    'No': function () {
                        $(this).dialog('close');
                        flag = false;
                        return flag;
                    }
                }
    });
    return flag;
}

function ValidateDiv(divId, controlToNotice) {
    if (($('#' + divId).html() != null)) {
        var divtext = $('#' + divId).html();
        if (divtext == "   ") {
            $('#' + controlToNotice).addClass('btn-validation-error');
            return false;
        }

        if (($('#' + divId).html() != "")) {

            $('#' + controlToNotice).removeClass('btn-validation-error');
            return true;
        }
        else {
            $('#' + controlToNotice).addClass('btn-validation-error');
            return false;
        }
    }
    else {
        $('#' + controlToNotice).addClass('btn-validation-error');
        return false;
    }
}

function ValidateCheckbox(checkBoxName, ContainerID) {
    $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());
    if ($('#' + checkBoxName).is(':checked')) {
        $('#' + ContainerID).removeClass('input-validation-error');
        if ((ContainerID == 'tdRequestType') || (ContainerID == 'tdRequestType2') || (ContainerID == 'tdRequestType3')) {
            $('#tdRequestType3').removeClass('input-validation-error');
            $('#tdRequestType').removeClass('input-validation-error');
            $('#tdRequestType2').removeClass('input-validation-error');
        }
        //check for Advertising field
        if ($('#chkAdvertising').is(':checked')) {
            $("#Advertising_Frame").css("z-index", "-999");
            //if any one of the media details checkbox are not checked
            if (
                         (!$('#chkAdvertisingTV').is(':checked')) &&
                         (!$('#chkAdvertisingRadio').is(':checked')) &&
                         (!$('#chkAdvertisingCinema').is(':checked')) &&
                         (!$('#chkAdvertisingInternet').is(':checked')) &&
                         (!$('#chkAdvertisingVideo').is(':checked')) &&
                         (!$('#chkAdvertisingOther').is(':checked'))) { //add the * mark
                $('#Advertising_Frame').addClass('input-validation-error  noBG');
                $("#Advertising_Frame").css("z-index", "0");
                $("#Advertising_Frame").css("width", "30%");
                $("#Advertising_Frame").css("height", "75px");
            }
            else {
                $('#Advertising_Frame').removeClass('input-validation-error noBG');
            }
        }
        else {
            $('#Advertising_Frame').removeClass('input-validation-error noBG');
        }
        //check for Film field
        if ($('#chkFilm').is(':checked')) {
            $("#Film_Frame").css("z-index", "-999");
            //if any one of the media details checkbox are not checked
            if (
                         (!$('#chkFilmTV').is(':checked')) &&
                         (!$('#chkFilmInternet').is(':checked')) &&
                         (!$('#chkFilmCinema').is(':checked')) &&
                         (!$('#chkFilmTrailer').is(':checked')) &&
                         (!$('#chkFilmVideo').is(':checked')) &&
                         (!$('#chkFilmOther').is(':checked'))) { //add the * mark

                $('#Film_Frame').addClass('input-validation-error noBG');
                $("#Film_Frame").css("z-index", "-1");
                if ($.browser.chrome) {
                    $("#Film_Frame").css("height", "75px");
                }
                else {
                    $("#Film_Frame").css("height", "70px");
                }
            }
            else {
                $('#Film_Frame').removeClass('input-validation-error noBG');
            }
        }
        else {
            $('#Film_Frame').removeClass('input-validation-error noBG');
        }
        //for trailor chekbox
        if ($('#chkTrailer').is(':checked')) {
            $("#Trailers_Frame").css("z-index", "-999");
            //if any one of the media details checkbox are not checked
            if (
                         (!$('#chkTrailersTV').is(':checked')) &&
                         (!$('#chkTrailersInternet').is(':checked')) &&
                         (!$('#chkTrailersCinema').is(':checked')) &&
                         (!$('#chkTrailersOther').is(':checked')) &&
                         (!$('#chkTrailersVideo').is(':checked'))) { //add the * mark

                $('#Trailers_Frame').addClass('input-validation-error noBG');
                $("#Trailers_Frame").css("z-index", "-1");
                if ($.browser.chrome) {
                    $("#Trailers_Frame").css("height", "75px");
                }
                else {
                    $("#Trailers_Frame").css("height", "70px");
                }
            }
            else {
                $('#Trailers_Frame').removeClass('input-validation-error noBG');
            }
        }
        else {
            $('#Trailers_Frame').removeClass('input-validation-error noBG');
        }
        //if other is selected
        if ($('#chkOther').is(':checked')) {
            //if any one of the media details checkbox are not checked
            if (
                         (!$('#chkOthersTV').is(':checked')) &&
                         (!$('#chkOthersRadio').is(':checked')) &&
                         (!$('#chkOthersCinema').is(':checked')) &&
                         (!$('#chkOthersInternet').is(':checked')) &&
                         (!$('#chkOthersVideo').is(':checked')) &&
                         (!$('#chkOthersOther').is(':checked'))) { //add the * mark
                $('#Other_Frame').addClass('input-validation-error noBG');
                $("#Other_Frame").css("z-index", "0");
                $("#Other_Frame").css("top", "10px");
                if ($.browser.chrome) {
                    $("#Other_Frame").css("height", "75px");
                }
                else {
                    $("#Other_Frame").css("height", "65px");
                }
                $("#Other_Frame").css("width", "30%");
            }
            else {
                $('#Other_Frame').removeClass('input-validation-error noBG');
            }
        }
        else {
            $('#Other_Frame').removeClass('input-validation-error noBG');
        }
        if ((ContainerID == 'chkAdver_Video_Other') || (ContainerID == 'chkAdver_TV_Radio') || (ContainerID == 'chkAdver_Cinema_Internet')) {
            $('#Advertising_Frame').removeClass('input-validation-error noBG');
        }
        if ((ContainerID == 'ChkTrailersTV_Internet') || (ContainerID == 'ChkTrailersCinema_Video') || (ContainerID == 'ChkTrailersOther')) {
            $('#Trailers_Frame').removeClass('input-validation-error noBG');
        }
        if ((ContainerID == 'chkFilmTV_Internet') || (ContainerID == 'chkFilmCinema_Trailor') || (ContainerID == 'chkFilmVideo_Other')) {
            $('#Film_Frame').removeClass('input-validation-error noBG');
        }
        if ((ContainerID == 'chkOthersTV_Radio') || (ContainerID == 'chkOthersCinema_Internet') || (ContainerID == 'chkOthersVideo_Other')) {
            $('#Other_Frame').removeClass('input-validation-error noBG');
        }
        return true;
    }
    else {
        if ((ContainerID == 'chkAdver_Video_Other') || (ContainerID == 'chkAdver_TV_Radio') || (ContainerID == 'chkAdver_Cinema_Internet')) {
            $('#Advertising_Frame').addClass('input-validation-error noBG');
        }
        else if ((ContainerID == 'ChkTrailersTV_Internet') || (ContainerID == 'ChkTrailersCinema_Video') || (ContainerID == 'ChkTrailersOther')) {
            $('#Trailers_Frame').addClass('input-validation-error noBG');
        }
        else if ((ContainerID == 'chkFilmTV_Internet') || (ContainerID == 'chkFilmCinema_Trailor') || (ContainerID == 'chkFilmVideo_Other')) {
            $('#Film_Frame').addClass('input-validation-error noBG');
        }
        else if ((ContainerID == 'chkOthersTV_Radio') || (ContainerID == 'chkOthersCinema_Internet') || (ContainerID == 'chkOthersVideo_Other')) {
            $('#Other_Frame').addClass('input-validation-error noBG');
        }
        else {
            $('#' + ContainerID).addClass('input-validation-error');
            if (ContainerID == 'divRequestTypeSection') {
                $('#divRequestTypeSection').css('width', '32%');
            }
        }
        $(".frameDv").css("background", "");
        return false;
    }
}

function ResubmitCancelRejResourceatProejctlvl() {
    var flag = false;
    $('#loadingDivPA').hide();
    var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ResubmissionCancelledResourceEditmsg + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: masterProjectMessages.confim,
                show: 'clip',
                hide: 'clip',
                width: 350
            });
    objWarningDialog.dialog('open');
    objWarningDialog.dialog({
        buttons:
                {
                    'Yes': function (e) {
                        flag = true;
                        $(this).dialog('close');
                        $('#loadingDivPA').show();
                        var value =
                                        {
                                            "Isrc": "",
                                            "Id": $('#hdnclrProjectId').val()
                                        };
                        var objDialog = $('#divCancelledRejResource');
                        objDialog.dialog({
                            resizable: false,
                            width: 900,
                            title: "Resubmit Projects",
                            modal: true,
                            open: function () {
                                $(this).load('/GCS/ClearanceProject/GetCancelRejectedResourceForSelection', value);
                                $(this).height($(window).height() - 150);
                            }
                        });
                        objDialog.dialog('open');
                        return flag;
                    },
                    'No': function () {
                        $(this).dialog('close');
                        flag = false;
                        UpdateFalseRequestSummaryResource();
                        UpdateAllRequestSummaryResource();
                        $('#hdncommandMaster').val("save");
                        IsAdditionalResourcesAdded();
                        return flag;
                    }
                }
    });
    return flag;
}

function SaveRCCHandler() {
    var postData = {
        "ProjectId": $("#hdnclrProjectId").val(),
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

function getMasterProjInfoAuditHistory(AuditTypeId) {
    var SelectedItemIds = [];
    var displayTitle;
    SelectedItemIds.push($("#hdnclrProjectId").val());
    if ($("#hdnProjRefId").val().length > 0) {
        displayTitle = $("#hdnProjRefId").val() + ' - ';
    }
    else {
        displayTitle = '';
    }
    displayTitle = displayTitle + 'Project Information';
    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
    return false;
}

function manageWrapper() {
    var docHeight = $(window).height();
    var headerHeight = 60;
    var footerHeight = 32;
    var errorHeight = 34;
    var mainContainerHeight = docHeight - headerHeight - footerHeight;
    $("#wrapper").css("height", mainContainerHeight - 100 - 34 + "px");
}

function currencyFocus() {
    currencyDdwnFocus = 1;
}

function currencyBlur() {
    currencyDdwnFocus = 0;
}

function SetRoutingFagForMaster() {
    var val = 0;
    var div = document.getElementById('tblResource');
    for (i = 0; i < div.children.length; i++) {
        hdnId = i + "hdnIsNewAdded" + $('#hdnClearanceResourceId' + i).val();
        if ($('#' + hdnId).val().toLowerCase() == "true" && $("#hdnArchiveFlag" + i).val() != "Y") {
            val = 1;
        }
    }
    return val;
}

function ResetFlagForResourceMasterProject() {
    var val = 0;
    var div = document.getElementById('tblResource');
    for (i = 0; i < div.children.length; i++) {
        hdnId = i + "hdnIsRouted" + $('#hdnClearanceResourceId' + i).val();
        hdnId1 = i + "hdnIsNewAdded" + $('#hdnClearanceResourceId' + i).val();
        if ($('#' + hdnId).val() == "false") {
            $('#' + hdnId1).val("true");
        }
    }
    return val;
}

function ReRouteResourcepreviouslyNotRouted() {
    //confirm message
    var objWarningDialog = $('<div id="Confirm"></div>')
            .html('<p>' + ConfirmRoutingForNotRoutedPResource + '</p>')
            .dialog({
                autoOpen: false,
                modal: true,
                title: masterProjectMessages.confim,
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
                        $('#hdncommandMaster').val("save");
                        $('#hdnAdditionalResourceCheck').val("YES");
                        ResetFlagForResourceMasterProject();
                        var formId = window.parent.document.forms[0];
                        globalPostBackCheck = true;
                        formId.submit();
                    },
                    'No': function () {
                        $(this).dialog('close');
                        $('#hdnIsUpdateNextStatus').val("false"); //Simply update the changes
                        $('#hdncommandMaster').val("save");
                        UpdateFalseRequestSummaryResource();
                        SetFlagFalseForNewlyAddedResources();
                        var formId = window.parent.document.forms[0];
                        globalPostBackCheck = true;
                        formId.submit();
                    }
                }
    });
}

$(document).ready(function () {
    $("#btnReInstate1").live("click", function () {

        $('#loadingDivPA').show();
    });

    $("#btnReOpen1").live("click", function () {

        $('#loadingDivPA').show();
    });

    $("#btnCancelProject1").live("click", function () {

        $('#loadingDivPA').show();
    });

    $("#btnComplete1").live("click", function () {

        $('#loadingDivPA').show();
    });

    $("#btnCancelProject").live("click", function () {
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        globalPostBackCheck = true;
        $('#btnCancelProject1').click();
    });

    $("#btnComplete").live("click", function () {
        if (!onSubmitResource())
            return false;
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        globalPostBackCheck = true;
        $('#btnComplete1').click();
    });

    $("#btnReinstate").live("click", function () {
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        $(document).each(function () { $('input:not(#btnReinstate, #btnReInstate1), textarea, select').attr('disabled', false) });
        globalPostBackCheck = true;
        $('#btnReInstate1').click();
    });

    $("#btnReOpen").live("click", function () {
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        $(document).each(function () { $('input:not(#btnReinstate, #btnReInstate1), textarea, select').attr('disabled', false) });
        globalPostBackCheck = true;
        $('#btnReOpen1').click();
    });

    $("#btnCancelRejectSubmit").live("click", function () {
        SelectCancelResource();
        SelectRejectResource();
        $('#divCancelledRejResource').dialog('close');
        IsValidationWithCancelRejectedResourceLevel(lstRoutedItems)
    });

    $("#btnSave1").live("click", function () {

        $('#loadingDivPA').show();
    });

    $("#btnSubmit1").live("click", function () {
        $('#loadingDivPA').show();
    });

    $("#btnSave").live("click", function () {
        if ($('#loadingDivPA').is(':visible')) {
            return;
        }
        if ($(".ui-dialog").is(":visible"))
            return;
        ResetRequestTypes();
        globalPostBackCheck = true;
        var index = $("#tabs").tabs('option', 'selected');
        $('#hdnDefaultTab').val(index);
        if ($("#hdnMasterProjectStatusId").val() == 1) {
            PerformSavingUnsubmittedMasterProject();
            return;
        }
        if (($("#hdnMasterProjectStatusId").val() == 2) || ($("#hdnMasterProjectStatusId").val() == 5)) {
            if (ValidateOnResubmit() == false) {
                return;
            }
        }
        var RequestSummaryMasterCount = $('#hdnRSResourceCount').val();
        if (RequestSummaryMasterCount <= 0)
            return false;
        $('#loadingDivPA').show();
        $('#hdnAdditionalResourceCheck').val("NO");
        var digitalVal = $('#CreateMasterProject').serialize(); //serialize the code
        //Call to check the level of changes in project
        $.post('/GCS/ClearanceProject/GetRoutingTriggered', digitalVal, function (data) {
            $('#loadingDivPA').hide();
            ApplyResubmittionReasons(data.projectReason, data.reasonResourceResubmission);
            lstCombinedDataList = data.CombinedList;
            Intchangelevel = ChkChangelevelInMasterProject(data.CombinedList);
            var sensitiveResourceListConditionChanged = data.sensitiveResourceListConditionChanged;
            if (lstCombinedDataList[1] != null && lstCombinedDataList[1].length > 0) { //Project level change
                PerformProjectLevelRouting(data.isSensitiveDataChanged);
            }
            else if (lstCombinedDataList[2] != null && lstCombinedDataList[2].length > 0) { //Resource level change
                var ResourceSpecifiedFieldsList = data.CombinedList[2];
                PerformResourceSpecifiedFieldsRouting(ResourceSpecifiedFieldsList, sensitiveResourceListConditionChanged)
            }
            else if (lstCombinedDataList[3] != null && lstCombinedDataList[3].length > 0) { //Other fields change at Resource level
                var ResourceOtherFieldsList = data.CombinedList[3];
                PerformResourceOtherFieldsRouting(ResourceOtherFieldsList, sensitiveResourceListConditionChanged)
            }
            else {
                IsAdditionalResourcesAdded();
                UpdateSensitiveListFlagsForResource(sensitiveResourceListConditionChanged);
            }
        });
    });

    if (($('#hdnMasterProjectStatusId').val() == "2") || ($('#hdnMasterProjectStatusId').val() == "5") || ($('#hdnMasterProjectStatusId').val() == "6")) {

        for (var i = 0; i < MasterControlsList.length; i++) {
            if (MasterControlsList[i].indexOf("_") == -1) {
                document.getElementById(MasterControlsList[i]).title = ResubmissionTooltipMsg;
            }
        }
        document.getElementById("MasterProjectDetails_Currency").title = ResubmissionTooltipMsg;
    }
    if ($('#hdnRoleGroup').val() != null) {
        RefreshEmailPanel($('#hdnstatusType').val(), $('#hdnRoleGroup').val());
    }
    if ($('#divConcurrency').val() != null && $('#divConcurrency').val() != "") {
        var objWarningDialog = $('<div id="Confirm"></div>')
                .html('<p><b> ' + $('#divConcurrency').val() + ' </b></p>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    show: 'clip',
                    hide: 'clip',
                    width: 500,
                    height: 200
                });
        objWarningDialog.dialog('open');
        objWarningDialog.dialog({
            buttons:
                    {
                        'Ok':
                            function (e) {
                                $(this).dialog('close');
                                $('#divConcurrency').val("");
                            }
                    }
        });
    }
    if ($("#hdnfirstRequesterCompanyId").val() == "") {
        $("#hdnfirstRequesterCompanyId").val($("#ddlRequestingComp").val());
    }
});