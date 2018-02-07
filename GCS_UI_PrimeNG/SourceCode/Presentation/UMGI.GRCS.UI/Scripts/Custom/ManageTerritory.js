/// <reference path="../jquery-1.5.1-vsdoc.js" />
/// <reference path="../jquery-1.5.1.js" />
/// <reference path="../jquery-ui-1.8.11.js" />

//Variables
var include = [];
var exclude = [];
var searchItems = [];

$('.ui-dialog-titlebar-close').attr("title", "Close");
var rightsIncludedCountry = [];
var rightsExcludedCountry = [];
var rightsIncludedTerritory = [];
var rightsExcludedTerritory = [];
var isFilterApplied = false;

//Toggles between two classes for an element
function ToggleClass(element, firstClass, secondClass, event) {
    event.cancelBubble = true;
    var classes = element.className.split(" ");
    var firstClassIndex = classes.indexOf(firstClass);
    var secondClassIndex = classes.indexOf(secondClass);
    if (firstClassIndex == -1 && secondClassIndex == -1) {
        classes[classes.length] = firstClass;
    }
    else if (firstClassIndex != -1) {
        classes[firstClassIndex] = secondClass;
    }
    else {
        classes[secondClassIndex] = firstClass;
    }
    element.className = classes.join(" ");
}

//Finds the index of an item in an array
function IndexOf(item) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == item) {
            return i;
        }
    }
    return -1;
}

//The toggle event handler for each expandable/collapsable node
function ToggleNodeStateHandler(event) {
    ToggleClass($(this).parent()[0], "Collapsed", "Expanded", (event == null) ? window.event : event);
    ToggleClass($(this).find(".divTree")[0], "Collapsed", "Expanded", (event == null) ? window.event : event);
}

//Prevents the onclick event from bubbling up to parent elements
function PreventBubbleHandler(event) {
    if (!event) {
        event = window.event;
        event.cancelBubble = true;
    }
}

//Adds the relevant onclick handlers for the nodes in the tree view
function SetupTreeView(elementId) {
    var tree = document.getElementById(elementId);
    if (tree != null) {
        var treeElements = tree.getElementsByTagName("li");

        for (var i = 0; i < treeElements.length; i++) {
            if (treeElements[i].getElementsByTagName("ul").length > 0) {
                $(treeElements[i]).find('div')[0].onclick = ToggleNodeStateHandler;
            }
            else {
                $(treeElements[i]).find('div')[0].onclick = PreventBubbleHandler;
            }
        }
    }
};

function checkedParentTerritoris(PID, NA, INCLUDE, EXCLUDE) {
    var parentChecked = JSLINQ(sitecollection).All(function (parent) {
        if (parent.Id == PID) {
            parent.IsNotApplicable = NA;
            parent.IsIncluded = INCLUDE;
            parent.IsExcluded = EXCLUDE;
        }
        return true;
    });
    return true;
}

function isTerritorialParentSelected(ID, isNA, isInclude, isExclude) {
    var countryItems = JSLINQ(sitecollection).Where(function (item) { return item.Id == ID })
    var terriItems = [];
    var countryItems123 = JSLINQ(countryItems.items).All(function (countryItem) {
        var testere = JSLINQ(sitecollection).Where(function (item) { return item.Id == countryItem.ParentId });
        if (testere.items[0] != null)
            terriItems.push(testere.items[0]);
        return true;
    });

    if (isNA) {
        return !(JSLINQ(terriItems).Any(function (item) { return item.IsIncluded || item.IsExcluded && item.IsTerritory }) == true);
    }
    else if (isInclude)
        return !(JSLINQ(terriItems).Any(function (item) { return item.IsExcluded && item.IsTerritory }) == true);
    else if (isExclude)
        return true;

    return true;
}

function isParentDuplicate(territories, id) {
    return (JSLINQ(territories.items).Count(function (item) { return item.Id == id }) > 1);
}

function getDuplicateParent(id) {
    return JSLINQ(sitecollection).First(function (item) { return item.IsTerritory && item.Id == id });
}

//set/get check box value in UI purpose
function RadioButtonClick() {
    $('#TreeView input:radio').click(function (e) {

        var identifyPath = $(location).attr('href');
        var isWGPage = false;
        if (identifyPath.toLowerCase().indexOf("workgroup") != -1) {
            isWGPage = true;
        }
        $('#territorialWarning').hide();
        e.stopPropagation();
        var splitvalue = (this.value).split('|');
        var id = splitvalue[0];
        var parentid = splitvalue[1];
        //invalid selection part for include and exclude
        var country = JSLINQ(sitecollection).Where(function (item) { return item.Id == id });
        if (!isTerritorialParentSelected(id, document.getElementsByName(this.name)[0].checked, document.getElementsByName(this.name)[1].checked, document.getElementsByName(this.name)[2].checked) && document.getElementsByName(this.name)[0].checked)//IsNA
        {
            if (country.items[0].IsNotApplicable)
                document.getElementsByName(this.name)[0].checked = true;
            else if (country.items[0].IsIncluded)
                document.getElementsByName(this.name)[1].checked = true;
            else if (country.items[0].IsExcluded)
                document.getElementsByName(this.name)[2].checked = true;
            $('#territorialAlert').empty();
            $('#territorialAlert').append("This selection is not valid");
            $('#territorialWarning').show();
            $('#territorialAlert').show();
            reSizeTerritorial();
            return false;
        }

        if (!isTerritorialParentSelected(id, document.getElementsByName(this.name)[0].checked, document.getElementsByName(this.name)[1].checked, document.getElementsByName(this.name)[2].checked) && document.getElementsByName(this.name)[1].checked)//IsIncluded
        {
            if (country.items[0].IsNotApplicable)
                document.getElementsByName(this.name)[0].checked = true;
            else if (country.items[0].IsIncluded)
                document.getElementsByName(this.name)[1].checked = true;
            else if (country.items[0].IsExcluded)
                document.getElementsByName(this.name)[2].checked = true;
            $('#territorialAlert').empty();
            $('#territorialAlert').append("This selection is not valid");
            $('#territorialWarning').show();
            $('#territorialAlert').show();
            reSizeTerritorial();
            return false;
        }

        if (!isTerritorialParentSelected(id, document.getElementsByName(this.name)[0].checked, document.getElementsByName(this.name)[1].checked, document.getElementsByName(this.name)[2].checked) && document.getElementsByName(this.name)[2].checked)//IsExcluded
        {
            if (country.items[0].IsNotApplicable)
                document.getElementsByName(this.name)[0].checked = true;
            else if (country.items[0].IsIncluded)
                document.getElementsByName(this.name)[1].checked = true;
            else if (country.items[0].IsExcluded)
                document.getElementsByName(this.name)[2].checked = true;
            $('#territorialAlert').empty();
            $('#territorialAlert').append("This selection is not valid");
            $('#territorialWarning').show();
            $('#territorialAlert').show();
            reSizeTerritorial();
            return false;
        }

        //Do not allow exclude if any of its parent is not included
        if (parentid != -2 && getRootParent(null, parentid, true) == null) {
            if (this.alt == "Radio2") {
                if (country.items[0].IsNotApplicable)
                    document.getElementsByName(this.name)[0].checked = true;
                else if (country.items[0].IsIncluded)
                    document.getElementsByName(this.name)[1].checked = true;
                else if (country.items[0].IsExcluded)
                    document.getElementsByName(this.name)[2].checked = true;

                $('#territorialAlert').empty();
                $('#territorialAlert').append("This selection is not valid");
                $('#territorialWarning').show();
                $('#territorialAlert').show();
                reSizeTerritorial();
                return false;
            }
        }

        if (this.checked == false) {
            checkChildNode(this);
            $($(this).parents('li')[0]).find('ul').find('input[type="radio"][alt=' + this.alt + ']').each(function () {
                this.checked = false;
                checkChildNode(this);
            });
        }
        else {
            checkChildNode(this);
            $($(this).parents('li')[0]).find('ul').find('input[type="radio"][alt=' + this.alt + ']').each(function () {
                if (IdForTerritory >= 3 && isWGPage == false && !hasBeenReset()) {
                    var Name = (this.name).toString().split('|');
                    var isExcludedAtProject = false;
                    var isExcludedCountryAtProject = false;

                    var ExcludedResources = $('#UnselectedCountries_1')[0].innerText.toString().split(';');
                    var ExcludedCountryProject = $('#hdnExcludedTerritoriesProject').val().toString().split(';');

                    if (ExcludedCountryProject != null) {
                        if (ExcludedCountryProject != "") {
                            for (var i = 0; i < ExcludedCountryProject.length; i++) {
                                if (ExcludedCountryProject[i].toString().trim() == Name[0]) {
                                    isExcludedCountryAtProject = true;
                                }
                            }
                        }
                    }
                    if (ExcludedResources != null) {
                        if (ExcludedResources != "") {
                            for (var i = 0; i < ExcludedResources.length; i++) {
                                if (ExcludedResources[i].toString().trim() == Name[0]) {
                                    isExcludedAtProject = true;
                                }
                            }
                        }
                    }
                    if (isExcludedAtProject == false && isExcludedCountryAtProject == false) {
                        if (this.alt == "Radio1") {
                            this.checked = true;
                            this.disabled = false;
                            if (this.alt == "Radio1") {
                                $($(this).parents('li')[0]).find('input[type="radio"][alt=Radio2]').each(function () {
                                    this.disabled = false;
                                });
                            }
                            checkChildNode(this);
                        }
                        else if (this.alt == "Radio2") {
                            this.checked = true;
                            this.disabled = false;
                            $($(this).parents('li')[0]).find('input[type="radio"][alt=Radio1]').each(function () {
                                this.disabled = true;
                            });
                            checkChildNode(this);
                        }
                    }
                }
                else {
                    this.checked = true;
                    checkChildNode(this);
                }
            });
        }

        if ($('#autocomplete').val() != '' && isFilterApplied) {
            var splitvalue = (this.value).split('|');
            var id = splitvalue[0];
            checkChildElements(id, this.alt);
        }

        $("#includeitems").html("");
        $("#excludeitems").html("");

        sortAppendedElements();
        territorialCount();
    });
}

function checkChildElements(id, radioId) {
    JSLINQ(sitecollection).Where(function (p) { return p.ParentId == id; }).All(function (item) {
        if (radioId == "Radio0") {
            item.IsNotApplicable = true;
            item.IsIncluded = false;
            item.IsExcluded = false;
            if (item.IsTerritory == false) {
                var index = jQuery.inArray(item.Name, include);
                if (index != -1) {
                    include.splice(index, 1);
                    JSLINQ(sitecollection).Where(function (p) { return p.Name == item.Name; }).All(function (itemSub) {
                        itemSub.IsNotApplicable = true;
                        itemSub.IsIncluded = false;
                        return true;
                    });
                }
                index = jQuery.inArray(item.Name, exclude);
                if (index != -1) {
                    exclude.splice(index, 1);
                    JSLINQ(sitecollection).Where(function (p) { return p.Name == item.Name; }).All(function (itemSub) {
                        itemSub.IsNotApplicable = true;
                        itemSub.IsExcluded = false;
                        return true;
                    });
                }
            }
            JSLINQ(sitecollection).Where(function (p) { return p.Name == item.Name; }).All(function (dupItem) {
                dupItem.IsNotApplicable = true;
                dupItem.IsIncluded = false;
                dupItem.IsExcluded = false;
                return true;
            });
        } else if (radioId == "Radio1") {
            item.IsNotApplicable = false;
            item.IsIncluded = true;
            item.IsExcluded = false;

            if (item.IsTerritory == false) {
                var index = jQuery.inArray(item.Name, include);
                if (index == -1) {
                    include.push(item.Name);
                    var exIndex = exclude.indexOf(item.Name);
                    if (exIndex != -1) {
                        exclude.splice(exIndex, 1);
                    }
                }
            }
            JSLINQ(sitecollection).Where(function (p) { return p.Name == item.Name; }).All(function (dupItem) {
                dupItem.IsNotApplicable = false;
                dupItem.IsIncluded = true;
                dupItem.IsExcluded = false;
                return true;
            });
        }
        else if (radioId == "Radio2") {
            item.IsNotApplicable = false;
            item.IsIncluded = false;
            item.IsExcluded = true;

            if (item.IsTerritory == false) {
                var index = jQuery.inArray(item.Name, exclude);
                if (index == -1) {
                    exclude.push(item.Name);
                    var inIndex = include.indexOf(item.Name);
                    if (inIndex != -1) {
                        include.splice(inIndex, 1);
                    }
                }
            }
            JSLINQ(sitecollection).Where(function (p) { return p.Name == item.Name; }).All(function (dupItem) {
                dupItem.IsNotApplicable = false;
                dupItem.IsIncluded = false;
                dupItem.IsExcluded = true;
                return true;
            });
        }

        checkChildElements(item.Id, radioId);
        return true;
    });
}

function sortAppendedElements() {
    include.sort();
    exclude.sort();
    jQuery.each(include, function (index, value) {
        $("#includeitems").append("<option value=\'" + value + "\'>" + value + "</option>");
    });

    jQuery.each(exclude, function (index, value) {
        $("#excludeitems").append("<option value=\'" + value + "\'>" + value + "</option>");
    });
}

function territorialCount() {
    var objTerritorialList = [];
    for (radioIndex = 0; radioIndex < sitecollection.length; radioIndex++) {
        if (sitecollection[radioIndex].IsTerritory == true) {
            objTerritorialList.push(sitecollection[radioIndex]);
        }
    }

    for (var total = 0; total < objTerritorialList.length; total++) {
        var countryCount = 0;
        var includeCount = 0;
        for (var territorialCount = 0; territorialCount < sitecollection.length; territorialCount++) {
            if (sitecollection[territorialCount].ParentId == objTerritorialList[total].Id) {
                countryCount++;
            }
            if (sitecollection[territorialCount].ParentId == objTerritorialList[total].Id && sitecollection[territorialCount].IsIncluded == true) {
                includeCount++;
            }
        }
        if (objTerritorialList[total].Id != 2 && objTerritorialList[total].Id != 0) {
            objTerritorialList[total].TerritoryCount = includeCount + " of " + countryCount;
            $('#TerritorialCount' + objTerritorialList[total].Id).text(objTerritorialList[total].TerritoryCount);
        }
        else {
            var allIncluded = 0;
            for (var parent = 0; parent < sitecollection.length; parent++) {
                if (sitecollection[parent].IsIncluded == true && sitecollection[parent].IsTerritory == false) {
                    allIncluded++;
                }
            }
            var totalCountryCount = JSLINQ(sitecollection).Count(function (totalCountryCounts) { return !totalCountryCounts.IsTerritory });
            objTerritorialList[total].TerritoryCount = allIncluded + " of " + totalCountryCount;
            $('#TerritorialCount' + objTerritorialList[total].Id).text(objTerritorialList[total].TerritoryCount);
        }
    }
}

function checkChildNode(checkeditem) {
    var splitvalue = (checkeditem.value).split('|');
    var id = splitvalue[0];
    var parentid = splitvalue[1];
    var radioName;
    var splitName = (checkeditem.name).split('|');
    var territoryName = splitName[0];
    if (checkeditem.alt == "Radio0") {
        include = jQuery.grep(include, function (value) {
            return value != territoryName;
        });
        exclude = jQuery.grep(exclude, function (value) {
            return value != territoryName;
        });
        for (radioIndex = 0; radioIndex < sitecollection.length; radioIndex++) {
            radioName = sitecollection[radioIndex].Name + "|" + sitecollection[radioIndex].Id.toString() + sitecollection[radioIndex].ParentId.toString();
            if (radioName == checkeditem.name && sitecollection[radioIndex].Id == id && sitecollection[radioIndex].ParentId == parentid || sitecollection[radioIndex].Id == id) {
                sitecollection[radioIndex].IsNotApplicable = true;
                sitecollection[radioIndex].IsIncluded = false;
                sitecollection[radioIndex].IsExcluded = false;
                if (document.getElementsByName(radioName).length > 0)
                    document.getElementsByName(radioName)[0].checked = true;
            }
        }
    }

    else if (checkeditem.alt == "Radio1") {
        include = jQuery.grep(include, function (value) {
            return value != territoryName;
        });
        exclude = jQuery.grep(exclude, function (value) {
            return value != territoryName;
        });
        for (radioIndex = 0; radioIndex < sitecollection.length; radioIndex++) {
            radioName = sitecollection[radioIndex].Name + "|" + sitecollection[radioIndex].Id.toString() + sitecollection[radioIndex].ParentId.toString();
            if (radioName == checkeditem.name && sitecollection[radioIndex].Id == id && sitecollection[radioIndex].ParentId == parentid || sitecollection[radioIndex].Id == id) {
                if (sitecollection[radioIndex].IsTerritory == false) {
                    var index = jQuery.inArray(territoryName, include);
                    if (index == -1) {
                        include.push(territoryName);
                    }
                    sitecollection[radioIndex].IsNotApplicable = false;
                    sitecollection[radioIndex].IsIncluded = true;
                    sitecollection[radioIndex].IsExcluded = false;
                }
                if (sitecollection[radioIndex].IsTerritory == true) {
                    sitecollection[radioIndex].IsNotApplicable = false;
                    sitecollection[radioIndex].IsIncluded = true;
                    sitecollection[radioIndex].IsExcluded = false;
                }
                if (document.getElementsByName(radioName).length > 0)
                    document.getElementsByName(radioName)[1].checked = true;
            }
        }
    }
    else if (checkeditem.alt == "Radio2") {
        include = jQuery.grep(include, function (value) {
            return value != territoryName;
        });
        exclude = jQuery.grep(exclude, function (value) {
            return value != territoryName;
        });
        for (radioIndex = 0; radioIndex < sitecollection.length; radioIndex++) {
            radioName = sitecollection[radioIndex].Name + "|" + sitecollection[radioIndex].Id.toString() + sitecollection[radioIndex].ParentId.toString();
            if (radioName == checkeditem.name && sitecollection[radioIndex].Id == id && sitecollection[radioIndex].ParentId == parentid || sitecollection[radioIndex].Id == id) {
                if (sitecollection[radioIndex].IsTerritory == false) {
                    var index = jQuery.inArray(territoryName, exclude);
                    if (index == -1) {
                        exclude.push(territoryName);
                    }
                    sitecollection[radioIndex].IsNotApplicable = false;
                    sitecollection[radioIndex].IsIncluded = false;
                    sitecollection[radioIndex].IsExcluded = true;
                }

                if (sitecollection[radioIndex].IsTerritory == true) {
                    sitecollection[radioIndex].IsNotApplicable = false;
                    sitecollection[radioIndex].IsIncluded = false;
                    sitecollection[radioIndex].IsExcluded = true;
                }
                if (document.getElementsByName(radioName).length > 0)
                    document.getElementsByName(radioName)[2].checked = true;
            }
        }
    }
}

//load time check the checked box checked/unchecked and assign the value for proper item
function radio_onload() {
    searchItems = new Array();

    for (var siteIndex = 0; siteIndex < sitecollection.length; siteIndex++) {
        var index = jQuery.inArray(sitecollection[siteIndex].Name, searchItems);
        if (index == -1) {
            searchItems.push(sitecollection[siteIndex].Name);
        }
        if (sitecollection[siteIndex].IsNotApplicable == true) {
            sitecollection[siteIndex].IsNotApplicable = true;
            sitecollection[siteIndex].IsIncluded = false;
            sitecollection[siteIndex].IsExcluded = false;
        }
        else if (sitecollection[siteIndex].IsIncluded == true) {
            sitecollection[siteIndex].IsNotApplicable = false;
            sitecollection[siteIndex].IsIncluded = true;
            sitecollection[siteIndex].IsExcluded = false;
            if (sitecollection[siteIndex].IsTerritory == false) {
                var index = jQuery.inArray(sitecollection[siteIndex].Name, include);
                if (index == -1) {
                    include.push(sitecollection[siteIndex].Name);
                }
            }
        }
        else if (sitecollection[siteIndex].IsExcluded == true) {
            sitecollection[siteIndex].IsNotApplicable = false;
            sitecollection[siteIndex].IsIncluded = false;
            sitecollection[siteIndex].IsExcluded = true;
            if (sitecollection[siteIndex].IsTerritory == false) {
                var index = jQuery.inArray(sitecollection[siteIndex].Name, exclude);
                if (index == -1) {
                    exclude.push(sitecollection[siteIndex].Name);
                }
            }
        }
        else {
            sitecollection[siteIndex].IsNotApplicable = true;
            sitecollection[siteIndex].IsIncluded = false;
            sitecollection[siteIndex].IsExcluded = false;
        }
    }
    sortAppendedElements();

    //fill the autocomplete texbox values
    var target = $("input#autocomplete");
    target.autocomplete({
        source: searchItems,
        open: function (event, ui) {
            if ($(".ui-menu-item").is(":visible")) {
                if ($(".ui-menu-item").length == 1) {
                    setTimeout(function () {
                        target.val($(".ui-menu-item a").html());
                        target.autocomplete('close');
                        target.focus();
                    }, 200);
                }
            }
        },
        minLength: 3
    });
}

function cancel() {
    $('#Territory').dialog('close');
}

function Clear() {
    $("#autocomplete").val('');
    var request = JSON.stringify(sitecollection);
    $.post('/GCS/Territory/AutocompleteSuggestions/' + $("#autocomplete").val(), { territorialItemString: request }, function (data) {
        isFilterApplied = false;
        $('#mainDiv').html(data);
        SetupTreeView("TreeView");
        RadioButtonClick();
        if (IdForTerritory >= 3) {
            DisableResourcesonClearFilter();
        }
    });
}

function disableTerritoryRadioButtons() {
    if (!hasBeenReset()) {
        $('.Expanded').find('.territoryCountry td:eq(1)').each(function (a) {
            $(this).find('input[type=radio]').each(function () {
                $(this).attr('disabled', true);
            });
        });

        $('.Expanded').find('.territoryCountry td:eq(3)').each(function (a) {
            $(this).find('input[type=radio]').each(function () {
                $(this).attr('disabled', true);
            });
        });

        $('.Expanded').find('.territoryCountry td:eq(2)').each(function (a) {
            $(this).find('input[type=radio]').each(function () {
                $(this).attr('disabled', true);
            });
        });
    }
}

function DisableResourcesonClearFilter() {
    disableTerritoryRadioButtons();
    $('.Collapsed').find('.territoryCountry td:eq(2)').each(function (a) {
        var radCon = $(this).find('input[type=radio]');
        var Id = radCon.attr('Name');
        var Name = Id.toString().split('|');
        var isExcludedAtProject = false;
        var isIncludedAtProject = false;
        var isExcludedAtResource = false;
        var ExcludedResources = $('#UnselectedCountries_1')[0].innerText.toString().split(';');
        var IncludedResources = $('#hdnIncludedTerritoriesProject').val().toString().split(';');
        var ExcludedatResourceLevel = $('#UnselectedCountries_' + IdForTerritory)[0].innerText.toString().split(';');
        if (ExcludedResources != null) {
            if (ExcludedResources != "") {
                for (var i = 0; i < ExcludedResources.length; i++) {
                    if (ExcludedResources[i].toString().trim() == Name[0]) {
                        isExcludedAtProject = true;
                    }
                }
            }
        }
        if (IncludedResources != null) {
            if (IncludedResources != "") {
                for (var i = 0; i < IncludedResources.length; i++) {
                    if (IncludedResources[i].toString().trim() == Name[0]) {
                        isIncludedAtProject = true;
                    }
                }
            }
        }
        if (ExcludedatResourceLevel != null) {
            if (ExcludedatResourceLevel != "") {
                for (var i = 0; i < ExcludedatResourceLevel.length; i++) {
                    if (ExcludedatResourceLevel[i].toString().trim() == Name[0]) {
                        isExcludedAtResource = true;
                        isIncludedAtProject = true;
                    }
                }
            }
        }

        if (!hasBeenReset()) {
            if (!radCon.is(':checked') && (isExcludedAtProject == true || isExcludedAtResource == false || isIncludedAtProject == false)) {
                radCon.attr('disabled', true);
            }
            var previous = $(this).prev().find('input[type=radio]');

            previous.attr('disabled', true); if (!radCon.is(':checked') && (isExcludedAtProject == true || isExcludedAtResource == false || isIncludedAtProject == false)) {
                var next = $(this).next().find('input[type=radio]');
                next.attr('disabled', true);
            }
        }
    });
}

function Search() {
    var searchText = $("#autocomplete").val();
    if (searchText.length > 2) {
        var territorialParent = sitecollection;
        var rootTerritorial = JSLINQ(territorialParent).Where(function (rootParent) { return rootParent.Id == 0 });
        var territorialFilteredList = jLinq.from(territorialParent).contains("Name", searchText).orderBy("Name").select()
        var territorial = territorialFilteredList;
        JSLINQ(territorialFilteredList).All(function (item) {
            if (!JSLINQ(territorial).Any(function (rootParent) { return rootParent.Id == item.ParentId })) {
                if (JSLINQ(territorialParent).Any(function (sibling) { return sibling.Id == item.ParentId }))
                    territorial.push(JSLINQ(territorialParent).First(function (child) { return child.Id == item.ParentId }));

                if (JSLINQ(territorialParent).Any(function (sibling) { return sibling.ParentId == item.Id && sibling.Id != item.ParentId }))
                    territorial.push(JSLINQ(territorialParent).Where(function (child) { return child.ParentId == item.Id && child.Id != item.ParentId }));
            }
            return true;
        });
        if (JSLINQ(rootTerritorial).Any() && !JSLINQ(territorial).Any(function (parent) { return parent.Id == rootTerritorial.First().Id })) {
            territorial.push(rootTerritorial);
        }
        var request = JSON.stringify(territorial);
        var loadingPanel = $($.find('#loadingDv'));
        loadingPanel.show();
        $.post('/GCS/Territory/AutocompleteSuggestions/' + $("#autocomplete").val(), { territorialItemString: request }, function (data) {
            isFilterApplied = true;
            $('#mainDiv').html(data);
            SetupTreeView("TreeView");
            loadingPanel.hide();
            RadioButtonClick();
            if (IdForTerritory >= 3) {
                ClearanceResourceTerritorySearch();
            }
        });
    }
    else if (searchText.length == 0) {
        Clear();
    }
}

function ClearanceResourceTerritorySearch() {
    disableTerritoryRadioButtons();
    $('.Expanded').find('.territoryCountry td:eq(2)').each(function (a) {
        var radCon = $(this).find('input[type=radio]');
        var Id = radCon.attr('Name');
        var Name = Id.toString().split('|');
        var isExcludedAtProject = false;
        var isIncludedAtProject = false;
        var isExcludedAtResource = false;
        var ExcludedResources = $('#UnselectedCountries_1')[0].innerText.toString().split(';');
        var IncludedResources = $('#hdnIncludedTerritoriesProject').val().toString().split(';');
        var ExcludedatResourceLevel = $('#UnselectedCountries_' + IdForTerritory)[0].innerText.toString().split(';');
        if (ExcludedResources != null) {
            if (ExcludedResources != "") {
                for (var i = 0; i < ExcludedResources.length; i++) {
                    if (ExcludedResources[i].toString().trim() == Name[0]) {
                        isExcludedAtProject = true;
                    }
                }
            }
        }
        if (IncludedResources != null) {
            if (IncludedResources != "") {
                for (var i = 0; i < IncludedResources.length; i++) {
                    if (IncludedResources[i].toString().trim() == Name[0]) {
                        isIncludedAtProject = true;
                    }
                }
            }
        }

        if (ExcludedatResourceLevel != null) {
            if (ExcludedatResourceLevel != "") {
                for (var i = 0; i < ExcludedatResourceLevel.length; i++) {
                    if (ExcludedatResourceLevel[i].toString().trim() == Name[0]) {
                        isExcludedAtResource = true;
                        isIncludedAtProject = true;
                    }
                }
            }
        }

        if (!hasBeenReset()) {
            if (!radCon.is(':checked') && (isExcludedAtProject == true || isExcludedAtResource == false || isIncludedAtProject == false)) {
                radCon.attr('disabled', true);
            }
            else {
                radCon.attr('disabled', false);
            }

            var previous = $(this).prev().find('input[type=radio]');
            previous.attr('disabled', true);

            if (!radCon.is(':checked') && (isExcludedAtProject == true || isExcludedAtResource == false || isIncludedAtProject == false)) {
                var next = $(this).next().find('input[type=radio]');
                next.attr('disabled', true);
            }
            else {
                var next = $(this).next().find('input[type=radio]');
                next.attr('disabled', false);
            }
        }
    });
}

function closeManageTerritoryPopUp() {
    $('#Territory').dialog('close');
}

function Save() {
    var selectedCountries = loadSiteCollectionItems();
    if (window.parent.callbackHandler) {
        window.parent.callbackHandler(selectedCountries, IdForTerritory, include, exclude);
    }
    closeManageTerritoryPopUp();
}

function SaveResources() {
    var selectedCountries = loadSiteCollectionItems();
    if (window.parent.callbackHandlerForResource) {
        var wasReset = hasBeenReset();
        window.parent.callbackHandlerForResource(selectedCountries, IdForTerritory, include, exclude, wasReset, closeManageTerritoryPopUp);
    }
}

function loadSiteCollectionItems() {
    var savedItems = [];
    for (var siteIndex = 0; siteIndex < sitecollection.length; siteIndex++) {
        if (sitecollection[siteIndex].IsIncluded == true || sitecollection[siteIndex].IsExcluded == true) {
            savedItems.push(sitecollection[siteIndex]);
        }
    }
    return savedItems;
}

function ReSet() {

    for (var siteIndex = 0; siteIndex < sitecollection.length; siteIndex++) {
        var radioName = sitecollection[siteIndex].Name + "|" + sitecollection[siteIndex].Id.toString() + sitecollection[siteIndex].ParentId.toString();
        sitecollection[siteIndex].IsNotApplicable = true;
        sitecollection[siteIndex].IsIncluded = false;
        sitecollection[siteIndex].IsExcluded = false;

        var radioElement = $('[name="' + radioName + '"]');
        var radioElementLength = radioElement.length;
        if (radioElement !== null && radioElement !== undefined && radioElementLength > 0) {
            radioElement[0].checked = true;
            radioElement[1].checked = false;
            radioElement[2].checked = false;

            if (IdForTerritory >= 3) {
                for (var i = 0; i < radioElementLength; i++) {
                    if (radioElement[i].disabled)
                        radioElement[i].removeAttribute('disabled');
                }
            }
        }
    }
    territorialCount();
    $("#includeitems").html("");
    $("#excludeitems").html("");
    include = new Array();
    exclude = new Array();
}

function hasBeenReset() {
    var reseted = $("#btnReSet").attr('clickStatus') === "true";
    return reseted;
}

function IsAnyIncludeExcludeTerritory() {
    var result = false;
    var totalItems = JSLINQ(sitecollection).Where(function (item) { return item.IsIncluded || item.IsExcluded; }).items.length;
    if (totalItems > 0)
        result = true;

    return result;
}

$(document).ready(function () {

    Array.prototype.indexOf = IndexOf;

    $('#mainDiv').keyup(function (e) {
        e.preventDefault();

        if ($('#btnSaveTerritory').is(':visible') && e.which == 13) {
            Save();
            return false;
        }
    });

    $("#autocomplete").keypress(function (e) {
        $('#territorialWarning').hide();
        if (e.which == 13) {
            Search();
            return false;
        }
    });

    SetupTreeView("TreeView");

    RadioButtonClick();

    radio_onload();

    $("#btnClear").click(function () {
        $('#territorialWarning').hide();
        Clear();
    });

    $("#btnSearchTerritory").click(function () {
        $('#territorialWarning').hide();
        Search();
    });

    $("#btnSaveTerritory").click(function () {
        $('#territorialWarning').hide();
        Save();
    });

    $("#btnSaveResourceTerritory").click(function () {
        $('#territorialWarning').hide();
        SaveResources();
    });

    $("#btnReSet").click(function (event) {
        $(this).attr('clickStatus', true);
        $('#territorialWarning').hide();
        ReSet();
    });

    $("#btnCancelPopup").click(function () {
        $('#territorialWarning').hide();
        cancel();
    });

    reSizeTerritorial();

    $('#Territory').hide().fadeIn('fast');

    if (parent.document.URL.indexOf('ViewContract') != -1) {
        var userRole = parent.document.getElementById('UserRoleName').value;
        if (userRole == "ReadOnlyUser" || userRole == "ReadOnlyBasicUser" || userRole == "PowerUser") {
            $('#btnSaveTerritory').attr('disabled', 'disabled');
            $('#btnReSet').attr('disabled', 'disabled');
        }
    }

    var hasTerritories = IsAnyIncludeExcludeTerritory();
    if (IdForTerritory >= 3 && !hasTerritories) {
        $('#btnReSet').attr('disabled', 'disabled');
    }
});