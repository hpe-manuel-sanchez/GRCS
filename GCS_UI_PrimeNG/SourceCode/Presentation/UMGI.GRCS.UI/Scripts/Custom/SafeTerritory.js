//Variables
var filteredCollection;
var include = [];
var exclude = [];
var searchItems = [];
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function () {
    Array.prototype.indexOf = IndexOf;
    //    $('body').keyup(function (e) {
    //        if ($('#btnSaveTerritory').is(':visible') && e.which == 13) {
    //            Save();
    //        }
    //    });

    SetupTreeView("TreeView");
    RadioButtonClick();
    radio_onload();
    $("#btnClear").click(function () {
        Clear();
    });

    $('#autocomplete').keypress(function (event) {
        if (event.which == '13') {
            $('#btnSearchTerritory').click();
            return false;
        }
    });
    $("#btnSearchTerritory").click(function () {
        Search();
    });

    //button save
    $("#btnSaveTerritory").click(function () {
        Save();
    });

    //button cancel
    $("#btnCancelPopup").click(function () {
        cancel();
    });

    $(window).bind("resize", resizeHandler);

    resizeHandler();

    $('#Territory').hide().fadeIn('fast');
});

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

//function checkedParentTerritoris(PID, NA, INCLUDE, EXCLUDE) {
//    window.JSLINQ(window.sitecollection).All(function (parent) {
//        if (parent.Id == PID) {
//            parent.IsNotApplicable = NA;
//            parent.IsIncluded = INCLUDE;
//            parent.IsExcluded = EXCLUDE;
//        }
//        return true;
//    });
//    return true;
//}

function isTerritorialParentSelected(id, isNa, isInclude, isExclude) {
    var countryItems = window.JSLINQ(window.sitecollection).Where(function (item) { return item.Id == id; });
    var terriItems = [];
    window.JSLINQ(countryItems.items).All(function (countryItem) {
        var testere = window.JSLINQ(window.sitecollection).Where(function (item) { return item.Id == countryItem.ParentId; });
        if (testere.items[0] != null)
            terriItems.push(testere.items[0]);
        return true;
    });

    if (isNa) {
        return !(window.JSLINQ(terriItems).Any(function (item) { return item.IsIncluded || item.IsExcluded && item.IsTerritory; }) == true);
    }
    else if (isInclude)
        return !(window.JSLINQ(terriItems).Any(function (item) { return item.IsExcluded && item.IsTerritory; }) == true);
    else if (isExclude)
        return true;

    return true;
}

function isParentDuplicate(territories, id) {
    return (window.JSLINQ(territories.items).Count(function (item) { return item.Id == id; }) > 1);
}

function getDuplicateParent(id) {
    return window.JSLINQ(window.sitecollection).First(function (item) { return item.IsTerritory && item.Id == id; });
}

//set/get check box value in UI purpose
function RadioButtonClick() {
    $('input:radio').click(function (e) {
        e.stopPropagation();
        // var splitvalue = (this.value).split('|');
        //var id = splitvalue[0];

        //invalid selection part for include and exclude
        //var country = window.JSLINQ(window.sitecollection).Where(function (item) { return item.Id == id; });
        //        if (!isTerritorialParentSelected(id, document.getElementsByName(this.name)[0].checked, document.getElementsByName(this.name)[1].checked, document.getElementsByName(this.name)[2].checked) && document.getElementsByName(this.name)[0].checked)//IsNA
        //        {
        //            if (country.items[0].IsNotApplicable)
        //                document.getElementsByName(this.name)[0].checked = true;
        //            else if (country.items[0].IsIncluded)
        //                document.getElementsByName(this.name)[1].checked = true;
        //            else if (country.items[0].IsExcluded)
        //                document.getElementsByName(this.name)[2].checked = true;
        //            alert("This selection is not valid");
        //            return false;
        //        }

        //        if (!isTerritorialParentSelected(id, document.getElementsByName(this.name)[0].checked, document.getElementsByName(this.name)[1].checked, document.getElementsByName(this.name)[2].checked) && document.getElementsByName(this.name)[1].checked)//IsIncluded
        //        {
        //            if (country.items[0].IsNotApplicable)
        //                document.getElementsByName(this.name)[0].checked = true;
        //            else if (country.items[0].IsIncluded)
        //                document.getElementsByName(this.name)[1].checked = true;
        //            else if (country.items[0].IsExcluded)
        //                document.getElementsByName(this.name)[2].checked = true;
        //            alert("This selection is not valid");
        //            return false;
        //        }

        //        if (!isTerritorialParentSelected(id, document.getElementsByName(this.name)[0].checked, document.getElementsByName(this.name)[1].checked, document.getElementsByName(this.name)[2].checked) && document.getElementsByName(this.name)[2].checked)//IsExcluded
        //        {
        //            if (country.items[0].IsNotApplicable)
        //                document.getElementsByName(this.name)[0].checked = true;
        //            else if (country.items[0].IsIncluded)
        //                document.getElementsByName(this.name)[1].checked = true;
        //            else if (country.items[0].IsExcluded)
        //                document.getElementsByName(this.name)[2].checked = true;
        //            alert("This selection is not valid");
        //            return false;
        //        }

        if (this.checked == false) {
            checkChildNode(this);
            $($(this).parents('li')[0]).find('ul').find('input[type="radio"][alt=' + this.alt + ']').each(function () {
                this.checked = false;
                checkChildNode(this);
            });
        }
        else {
            checkChildNode(this);
            //            $($(this).parents('li')[0]).find('ul').find('input[type="radio"][alt=' + this.alt + ']').each(function () {
            //                //this.checked = true;
            //               // checkChildNode(this);
            //            });
        }

        $("#includeitems").html("");
        sortAppendedElements();
        territorialCount();
    });
}

function sortAppendedElements() {
    include.sort();
    jQuery.each(include, function (index, value) {
        $("#includeitems").append("<option value=\'" + value + "\'>" + value + "</option>");
    });
    $('#TerritoriesCount').text('( ' + include.length + ' )');
}

function territorialCount() {
    var objTerritorialList = [];
    for (var radioIndex = 0; radioIndex < window.sitecollection.length; radioIndex++) {
        if (window.sitecollection[radioIndex].IsTerritory == true) {
            objTerritorialList.push(window.sitecollection[radioIndex]);
        }
    }

    for (var total = 0; total < objTerritorialList.length; total++) {
        var countryCount = 0;
        var includeCount = 0;
        for (var territorialCount = 0; territorialCount < window.sitecollection.length; territorialCount++) {
            if (window.sitecollection[territorialCount].ParentId == objTerritorialList[total].Id) {
                countryCount++;
            }
            if (window.sitecollection[territorialCount].ParentId == objTerritorialList[total].Id && window.sitecollection[territorialCount].IsIncluded == true) {
                includeCount++;
            }
        }
        if (objTerritorialList[total].Id != 2 && objTerritorialList[total].Id != 0) {
            objTerritorialList[total].TerritoryCount = includeCount + " of " + countryCount;
            $('#TerritorialCount' + objTerritorialList[total].Id).text(objTerritorialList[total].TerritoryCount);
        }
        else {
            var allIncluded = 0;
            for (var parent = 0; parent < window.sitecollection.length; parent++) {
                if (window.sitecollection[parent].IsIncluded == true && window.sitecollection[parent].IsTerritory == false) {
                    allIncluded++;
                }
            }
            var totalCountryCount = window.JSLINQ(window.sitecollection).Count(function (totalCountryCounts) { return !totalCountryCounts.IsTerritory });
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
    var radioIndex;
    if (checkeditem.alt == "Radio0") {
        include = jQuery.grep(include, function (value) {
            return value != territoryName;
        });
        for (radioIndex = 0; radioIndex < window.sitecollection.length; radioIndex++) {
            radioName = window.sitecollection[radioIndex].Name + "|" + window.sitecollection[radioIndex].Id.toString() + window.sitecollection[radioIndex].ParentId.toString();
            if (radioName == checkeditem.name && window.sitecollection[radioIndex].Id == id && window.sitecollection[radioIndex].ParentId == parentid || window.sitecollection[radioIndex].Id == id) {
                window.sitecollection[radioIndex].IsNotApplicable = true;
                window.sitecollection[radioIndex].IsSafeTerritory = false;
                if (document.getElementsByName(radioName).length > 0)
                    document.getElementsByName(radioName)[0].checked = true;
            }
        }
    }
    else if (checkeditem.alt == "Radio1") {
        include = jQuery.grep(include, function (value) {
            return value != territoryName;
        });

        include.push(territoryName);
        for (radioIndex = 0; radioIndex < window.sitecollection.length; radioIndex++) {
            radioName = window.sitecollection[radioIndex].Name + "|" + window.sitecollection[radioIndex].Id.toString() + window.sitecollection[radioIndex].ParentId.toString();
            if (radioName == checkeditem.name && window.sitecollection[radioIndex].Id == id && window.sitecollection[radioIndex].ParentId == parentid || window.sitecollection[radioIndex].Id == id) {
                window.sitecollection[radioIndex].IsNotApplicable = false;
                window.sitecollection[radioIndex].IsSafeTerritory = true;
                if (document.getElementsByName(radioName).length > 0)
                    document.getElementsByName(radioName)[1].checked = true;
            }
        }
    }
    //    else if (checkeditem.alt == "Radio2") {
    //        include = jQuery.grep(include, function (value) {
    //            return value != territoryName;
    //        });
    //        exclude = jQuery.grep(exclude, function (value) {
    //            return value != territoryName;
    //        });
    //        //jQuery.inArray(checkeditem,sitecollection)
    //        for (radioIndex = 0; radioIndex < sitecollection.length; radioIndex++) {
    //            radioName = sitecollection[radioIndex].Name + "|" + sitecollection[radioIndex].Id.toString() + sitecollection[radioIndex].ParentId.toString();
    //            if (radioName == checkeditem.name && sitecollection[radioIndex].Id == id && sitecollection[radioIndex].ParentId == parentid || sitecollection[radioIndex].Id == id) {
    //                if (sitecollection[radioIndex].IsTerritory == false) {
    //                    var index = jQuery.inArray(territoryName, exclude);
    //                    if (index == -1) {
    //                        exclude.push(territoryName);
    //                    }
    //                    sitecollection[radioIndex].IsNotApplicable = false;
    //                    sitecollection[radioIndex].IsIncluded = false;
    //                    sitecollection[radioIndex].IsExcluded = true;
    //                }

    //                if (sitecollection[radioIndex].IsTerritory == true) {
    //                    sitecollection[radioIndex].IsNotApplicable = false;
    //                    sitecollection[radioIndex].IsIncluded = false;
    //                    sitecollection[radioIndex].IsExcluded = true;
    //                }
    //                if (document.getElementsByName(radioName).length > 0)
    //                    document.getElementsByName(radioName)[2].checked = true;
    //            }
    //        }
    //    }
}

//load time check the checked box checked/unchecked and assign the value for proper item
function radio_onload() {
    searchItems = new Array();
    for (var siteIndex = 0; siteIndex < window.sitecollection.length; siteIndex++) {
        var index;
        index = jQuery.inArray(window.sitecollection[siteIndex].Name, searchItems);
        if (index == -1) {
            searchItems.push(window.sitecollection[siteIndex].Name);
        }
        if (window.sitecollection[siteIndex].IsNotApplicable == true) {
            window.sitecollection[siteIndex].IsNotApplicable = true;
            window.sitecollection[siteIndex].IsSafeTerritory = false;
        }
        else if (window.sitecollection[siteIndex].IsSafeTerritory == true) {
            window.sitecollection[siteIndex].IsNotApplicable = false;
            window.sitecollection[siteIndex].IsSafeTerritory = true;
            index = jQuery.inArray(window.sitecollection[siteIndex].Name, include);
            if (index == -1) {
                include.push(window.sitecollection[siteIndex].Name);
            }
        }
        else {
            window.sitecollection[siteIndex].IsNotApplicable = true;
            window.sitecollection[siteIndex].IsSafeTerritory = false;
        }
    }
    sortAppendedElements();

    //fill the autocomplete texbox values
    $("input#autocomplete").autocomplete({
        source: searchItems, minLength: 3
    });
}

function cancel() {
    $('#Territory').dialog('close');
}

function resizeHandler() {
}

function Clear() {
    $("#autocomplete").val('');
    var request = JSON.stringify(window.sitecollection);
    $.ajax({
        url: '/GCS/Territory/SafeTerritoryAutocompleteSuggestions/' + $("#autocomplete").val(),
        type: 'POST',
        data: request,
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#mainDiv').html(data);
            SetupTreeView("TreeView");
            RadioButtonClick();
        }
    });
}
function Search() {
    var searchText = $.trim($("#autocomplete").val());
    if (searchText.length > 2) {
        var territorialParent = window.sitecollection;
        var rootTerritorial = window.JSLINQ(territorialParent).Where(function (rootParent) { return rootParent.Id == 0; });
        var territorialFilteredList = window.jLinq.from(territorialParent).contains("Name", searchText).orderBy("Name").select();
        var territorial = territorialFilteredList;
        window.JSLINQ(territorialFilteredList).All(function (item) {
            if (!window.JSLINQ(territorial).Any(function (rootParent) { return rootParent.Id == item.ParentId; })) {
                if (window.JSLINQ(territorialParent).Any(function (sibling) { return sibling.Id == item.ParentId; }))
                    territorial.push(window.JSLINQ(territorialParent).First(function (child) { return child.Id == item.ParentId; }));

                if (window.JSLINQ(territorialParent).Any(function (sibling) { return sibling.ParentId == item.Id && sibling.Id != item.ParentId; }))
                    territorial.push(window.JSLINQ(territorialParent).Where(function (child) { return child.ParentId == item.Id && child.Id != item.ParentId; }));
            }
            return true;
        });
        if (window.JSLINQ(rootTerritorial).Any() && !window.JSLINQ(territorial).Any(function (parent) { return parent.Id == rootTerritorial.First().Id; })) {
            territorial.push(rootTerritorial);
        }
        var request = JSON.stringify(territorial);
        $.ajax({
            url: '/GCS/Territory/SafeTerritoryAutocompleteSuggestions/' + $("#autocomplete").val(),
            type: 'POST',
            data: request,
            async: true,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#mainDiv').html(data);
                SetupTreeView("TreeView");
                RadioButtonClick();
            }
        });
    }
    else if (searchText.length == 0) {
        Clear();
    }
}

function Save() {
    var saveditems = [];
    for (var siteIndex = 0; siteIndex < window.sitecollection.length; siteIndex++) {
        if (window.sitecollection[siteIndex].IsTerritory) {
            if (window.sitecollection[siteIndex].IsSafeTerritory == true) {
                var index = jQuery.inArray(window.sitecollection[siteIndex], saveditems);
                if (index == -1) {
                    saveditems.push(window.sitecollection[siteIndex]);
                }
            }
        }
    }
    var territory = JSON.stringify(saveditems);
    $.ajax({
        url: '/GCS/Routing/AddSafeTerritories/',
        type: 'POST',
        dataType: 'html',
        async: false,
        data: territory,
        cache: false,
        success: function (data) {
            if (data == 'False') {
                $("#saveErrorMessage").show();
                $("#saveErrorMessage").html(window.pageErrorMessage);
            }
            else {
                $("#saveMessage").show();
                $("#saveMessage").html(window.pageSuccessMessage);
            }
        },
        contentType: 'application/json; charset=utf-8'
    });
    //$('#Territory').dialog('close');
}

function getSafeTerritoryAuditHistory(AuditTypeId) {
    var SelectedItemIds = [0];
    var displayTitle = 'Safe Territory List';
    displayAuditTrail(AuditTypeId, SelectedItemIds, displayTitle);
    return false;
}