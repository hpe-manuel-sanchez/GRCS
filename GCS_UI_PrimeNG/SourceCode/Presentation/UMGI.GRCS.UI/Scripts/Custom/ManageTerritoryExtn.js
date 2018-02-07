var sitecollection = [];
var includedTerritoriesListResult = [];
var excludedTerritoriesListResult = [];
var childIncludedCountries = [];
var childExcludedCountries = [];

function getRootParent(element, parentId, IsIncluded)//Pass ParentId
{
    if (JSLINQ(sitecollection).Count(function (item) { return item.IsIncluded && item.IsTerritory && item.Id == parentId }) > 0) {
        var elem = JSLINQ(sitecollection).First(function (item) { return item.IsIncluded && item.IsTerritory && item.Id == parentId });
        return getRootParent(elem, elem.ParentId, IsIncluded);
    }
    return element;
}

function getParent(parentId)//Pass ParentId
{
    return JSLINQ(sitecollection).First(function (item) { return item.IsIncluded && item.IsTerritory && item.Id == parentId });
}

function getParentExcluded(parentId)//Pass ParentId
{
    return JSLINQ(sitecollection).First(function (item) { return item.IsExcluded && item.IsTerritory && item.Id == parentId });
}

function getParentById(parentId)//Pass ParentId
{
    return JSLINQ(sitecollection).First(function (item) { return item.Id == parentId });
}

function hasParent(parentId)//Pass ParentId
{
    return JSLINQ(sitecollection).Any(function (item) { return item.IsTerritory && item.Id == parentId });
}

function isDuplicate(territories, id) {
    return (JSLINQ(territories.items).Count(function (item) { return item.IsIncluded && item.IsTerritory && item.Id == id }) > 1);
}

function isDuplicateExcluded(territories, id) {
    return (JSLINQ(territories.items).Count(function (item) { return item.IsExcluded && item.IsTerritory && item.Id == id }) > 1);
}

function getDuplicateTerritories(territories, id) {
    return (JSLINQ(territories.items).Where(function (item) { return item.IsIncluded && item.IsTerritory && item.Id == id }));
}

function isParentSelected(territories, terri) {
    if (isDuplicate(territories, terri.Id) == true) {
        if (getParentExcluded(terri.ParentId) != null && getParent(terri.ParentId).IsIncluded)
            return true;

        var countryItems = JSLINQ(sitecollection).Where(function (item) { return item.Id == terri.Id })
        var terriItems = [];
        var countryItems123 = JSLINQ(countryItems.items).All(function (countryItem) {
            var testere = JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded && item.Id == countryItem.ParentId });
            if (testere.items[0] != null)
                terriItems.push(testere.items[0]);
            return true;
        });

        return JSLINQ(terriItems).Any(function (item) { return item.IsIncluded && item.IsTerritory });
    }
    return false;
}

function isParentExcluded(territories, terri) {
    if (getParentExcluded(terri.ParentId) != null && getParentExcluded(terri.ParentId).IsExcluded)
        return true;

    var countryItems = JSLINQ(sitecollection).Where(function (item) { return item.Id == terri.Id })
    var terriItems = [];
    var countryItems123 = JSLINQ(countryItems.items).All(function (countryItem) {
        var testere = JSLINQ(sitecollection).Where(function (item) { return item.IsExcluded && item.Id == countryItem.ParentId });
        if (testere.items[0] != null)
            terriItems.push(testere.items[0]);
        return true;
    });

    return JSLINQ(terriItems).Any(function (item) { return item.IsExcluded && item.IsTerritory });
}

// GCS start

var excludedCountriesListGCS = [];
var excludedTerritoriesListGCS = [];
var includedCountriesListGCS = [];
var includedTerritoriesListGCS = [];


// GCS End

// String formation New

function setTerritoryCountry(territoryCollection, divIncludedTerritoryName, divExcludedTerritoryName) {
    // GCS start
    sitecollection = territoryCollection[0].Value;
    //Clear the Countries 
    $('#'+divIncludedTerritoryName).html('');
    $('#'+divExcludedTerritoryName).html('');
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
                            //rightsExcludedTerritory.push(excludedTerri.Id);
                        } //Sub Territories
                        else {
                            var rootParent = getRootParent(null, excludedTerri.ParentId, true); //Get root parent which is selected
                            if (rootParent != null && (rootParent.Id == includedTerri.Id) && rootParent.IsIncluded == true) {
                                excludeSubTerriString += excludedTerri.Name + '; ';
                                excludedTerritoriesList.push(excludedTerri.Name);
                                excludedTerritoriesListResult.push(excludedTerri);
                                //review right
                                //rightsExcludedTerritory.push(excludedTerri.Id);
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
                                //rightsExcludedCountry.push(excludedCountry.Id);
                            } else {
                                var parent = getParent(excludedCountry.ParentId);
                                var rootParent = getRootParent(null, excludedCountry.ParentId, true); //Get root parent which is selected

                                if (rootParent != null && (rootParent.Id == includedTerri.Id) && parent.IsExcluded == false && !checkifDuplicatesParentIsExcluded(excludedCountry) && rootParent.IsIncluded == true) {
                                    excludeCountryString += excludedCountry.Name + '; ';
                                    excludedCountriesList.push(excludedCountry.Name);
                                    //review right
                                    //rightsExcludedCountry.push(excludedCountry.Id);
                                }
                            }
                        }
                        ;
                    }
                    return true;
                });
                if (excludeCountryString != "") {
                    excludeCountryFinalString += excludeCountryString;
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
            }
            excludedCountriesListGCS = excludedCountriesList

        }

        return true;
    });

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
                    // rightsIncludedCountry.push(includedCountry.Id);

                } else if (parent != null && parent.IsIncluded == false || (rootParent != null && rootParent.IsIncluded == false)) {
                    includeCountryString += includedCountry.Name + '; ';
                    includedCountriesList.push(includedCountry.Name);
                    //review right
                    //rightsIncludedCountry.push(includedCountry.Id);
                }
            }
        }

        return true;
    });
    includedCountriesListGCS = includedCountriesList;
    //
    if (includeCountryString != '') {
        var temp = ', ';
        if (territoryString == '' || endsWith(territoryString, '/') == true)
            temp = ' ';
        //Remove last , and replace /, with /
        finalString = (territoryString + temp + includeCountryString.replace(new RegExp(", " + '$'), '')).replace(/\/, /g, '/ ');
    } else {
        finalString = territoryString.replace(new RegExp("\/" + '$'), '').replace(new RegExp(", " + '$'), '');
    }


    // $('#selectedCountries').append(SetSelectedterritories(includedTerritoriesListResult));
    //working
    //SetSelectedterritories(includedTerritoriesListResult);
    //Included Countries 
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
                var link = '<a id="' + includedTerritoriesListResult[i].Id + '" style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + includedTerritoriesListResult[i].Name + '</a>; ';
            }
            else {
                var link = '<a id="' + includedTerritoriesListResult[i].Id + '"style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + includedTerritoriesListResult[i].Name + '</a>; ';
            }
            
        }
        else {
            var link = includedTerritoriesListResult[i].Name;
        }
        $('#' + divIncludedTerritoryName).append(link);
         if (includedTerritoriesListResult[i].Name != "World" && includedTerritoriesListResult[i].Name != "Universe") 
        $('#' + includedTerritoriesListResult[i].Id).flyout();
    }
    $('#' + divIncludedTerritoryName).append(includeCountryString);

 
   //  Excluded Countries 
    for (var i = 0; i < excludedTerritoriesListResult.length; i++) {
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

            if (i == excludedTerritoriesListResult.length - 1) {
                var link = '<a id="' + excludedTerritoriesListResult[i].Id + '"style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + excludedTerritoriesListResult[i].Name + '</a> ';
            }
            else {
                var link = '<a id="' + excludedTerritoriesListResult[i].Id + '" style="text-decoration: underline;" href="javascript:void(0);"  title="' + countries + '">' + excludedTerritoriesListResult[i].Name + '</a>; ';
            }
        }
        else {
            var link = excludedTerritoriesListResult[i].Name;
        }
        $('#' + divExcludedTerritoryName).append(link);
        if (excludedTerritoriesListResult[i].Name != "World" && excludedTerritoriesListResult[i].Name != "Universe")
            $('#' + excludedTerritoriesListResult[i].Id).flyout();
    }
    $('#' + divExcludedTerritoryName).append(excludeCountryFinalString);
    return finalString;
}


function checkifDuplicateIsSelected(element) {
    var duplicates = getDuplicates(element);
    if (duplicates.items.length == 0)
        return false;

    return JSLINQ(duplicates.items).Any(function (item) { return item.IsIncluded; });
}

function getDuplicates(element) {
    return (JSLINQ(sitecollection).Where(function (item) { return item.Id == element.Id && item.ParentId != element.ParentId; }));
}

function checkifDuplicatesParentIsSelected(element) {
    var duplicates = getDuplicates(element);
    if (duplicates.items.length == 0)
        return false;

    return JSLINQ(duplicates.items).Any(function (item) {
        return getParent(item.ParentId) != null && getParent(item.ParentId).IsIncluded;
    });
}

function checkifDuplicatesParentIsExcluded(element) {
    var duplicates = getDuplicates(element);
    if (duplicates.items.length == 0)
        return false;

    return JSLINQ(duplicates.items).Any(function (item) {
        return getParentExcluded(item.ParentId) != null && getParentExcluded(item.ParentId).IsExcluded;
    });
}

function endsWith(str, suffix) {
    return str.indexOf(suffix, str.length - suffix.length) !== -1;
}

// String formation New

function reSizeTerritorial() {

    if ($('#territorialWarning').css("display") == 'none')
        $(".trContainer").css('height', '100%');
    else
        $(".trContainer").css('height', '93%');
}


function GetTerritoryCountries(id) {
    // childIncludedCountries = [];

    var childs = JSLINQ(sitecollection).Where(function (dict) { return dict.ParentId == id }).OrderBy(function (dict) { return dict.Name; });
    for (var i = 0; i < childs.items.length; i++) {
        {
            if (childs.items[i].IsTerritory == false) {
                childIncludedCountries.push(childs.items[i].Name);
            } else {
                GetTerritoryCountries(childs.items[i].Id);
            }
        }
    }
    return childIncludedCountries;
}


//Included Territories List
//function GetIncludedCountries(id) {
//   // childIncludedCountries = [];

//    var childs = JSLINQ(sitecollection).Where(function (dict) { return dict.ParentId == id && dict.IsIncluded == true }).OrderBy(function (dict) { return dict.Name; });
//    for (var i = 0; i < childs.items.length; i++) {
//        {
//            if (childs.items[i].IsTerritory == false) {
//                childIncludedCountries.push(childs.items[i].Name);
//            } else {
//                GetIncludedCountries(childs.items[i].Id);
//            }
//        }
//    }
//    return childIncludedCountries;
//}

//Excluded Territories List
function GetExcludedCountries(id) {
 //   childExcludedCountries = [];
    var childs = JSLINQ(sitecollection).Where(function (dict) { return dict.ParentId == id && dict.IsExcluded == true }).OrderBy(function (dict) { return dict.Name; });
    for (var i = 0; i < childs.items.length; i++) {
        {
            if (childs.items[i].IsTerritory == false) {
                childExcludedCountries.push(childs.items[i].Name);
            } else {
                GetExcludedCountries(childs.items[i].Id);
            }
        }
    }
    return childExcludedCountries;
}
