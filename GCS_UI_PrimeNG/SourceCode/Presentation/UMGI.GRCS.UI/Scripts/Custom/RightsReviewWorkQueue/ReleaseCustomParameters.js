//JS For Release Custom Parameters Tab UC-20

//Initialized Variable for Messages from Resource file
var releaseCustomParameters;
var paramQuery;
var parameterPreDefined = null;
var hdnReleaseRights = $('#hiddenReleaseRights').val();
var hdnResourceRights = $('#hiddenResourceRights').val();
// Validation
function customParamValidate() {
    if ($('#divOtherParameterTab').is(':visible')) {
       
            // Custom Tab UC19
            if (hdnResourceRights == "Resource") {

                if (!customTabValidation()) {
                    return false;
                } else {
                    return true;
                }
            }
        
            // Custom Tab UC20
         else if (hdnReleaseRights == "Release") {
            var upc = $('#RepertoireFilter_UPC').val();
            var artist = $('#RepertoireFilter_Artist').val();
            var releaseTitle = $('#RepertoireFilter_ReleaseTitle').val();
            var adminCompany = $('#txtAdminCompany').val();
            var linkedContract = $('#RepertoireFilter_LinkedContract').val();
            var fromDt = $('#FromDt').val();
            var toDt = $('#ToDt').val();
            var noRlsDt = $('#chkNoRlsDt').is(':checked');
            if (!ReleaseDateValidation(fromDt, toDt)) {
                return false;
            }

            if ((upc == '' || upc == null)
                && (artist == '' || artist == null)
                && (releaseTitle == '' || releaseTitle == null)
                && (adminCompany == '' || adminCompany == null)
                && (linkedContract == '' || linkedContract == null)
                && ((fromDt == '' || fromDt == null) && (toDt == '' || toDt == null))
                && (noRlsDt == false)) {
                $('#SplitAlert').empty();
                $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseRequiredFieldValidation);
                $('#splitWarning').show();
                resizePopUp();
                $('#SplitAlert').show();
                return false;
            }
            else {
                return true;
            }
        }
    } 
    else {

        //Predefined Tab UC19
        if (hdnResourceRights == "Resource") {
            if (!dateParameterRequired()) {
                return false;
            }
            else {
                return true;
            }
        }
        //Predefined Tab UC20
        else if (hdnReleaseRights == "Release")
        {
            var preRlsDt = $('#chkPreDefNoRlsDt').is(':checked');
            var preFromDt = $('#ReleaseDateRangeParams_PreDefinedFromDt').val();
            var preToDt = $('#ReleaseDateRangeParams_PreDefinedToDt').val();
            var premultiArtist = $('#ReleasePredefinedParameterType0').is(':checked');
            var preOST = $('#ReleasePredefinedParameterType1').is(':checked');
            var preNonMac = $('#ReleasePredefinedParameterType2').is(':checked');

            if (!ReleaseDateValidation(preFromDt, preToDt)) {
                return false;
            }
            

            if ((premultiArtist == null || premultiArtist == false)
                && (preOST == null || preOST == false)
                && (preNonMac == null || preNonMac == false)
                && ((preFromDt == '' || preFromDt == null) && (preToDt == '' || preToDt == null))
                && (preRlsDt == null || preRlsDt == false)) {
                $('#SplitAlert').empty();
                $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseRequiredFieldValidation);
                $('#splitWarning').show();
                resizePopUp();
                $('#SplitAlert').show();
                return false;
            }

            if (premultiArtist == true || preOST == true || preNonMac == true) {
                if (((preFromDt == '' || preFromDt == null) && (preToDt == '' || preToDt == null))
                && (preRlsDt == null || preRlsDt == false)) {
                    $('#SplitAlert').empty();
                    $('#SplitAlert').append('Please provide a Release Date Range or select the tickbox for repertoire with no release date');
                    $('#splitWarning').show();
                    resizePopUp();
                    $('#SplitAlert').show();
                    return false;
                }
            }

            if (((preFromDt != '' || preFromDt != null) && (preToDt != '' || preToDt != null))
                && (preRlsDt != null || preRlsDt != false)) {
                if (premultiArtist == false && preOST == false && preNonMac == false) {
                    $('#SplitAlert').empty();
                    $('#SplitAlert').append('Select predefined Parameter value');
                    $('#splitWarning').show();
                    resizePopUp();
                    $('#SplitAlert').show();
                    return false;
                }
            }
            return true;
        }
    }
}

$('#ReleaseDateRangeParams_PreDefinedFromDt').change(function () {
    $('#chkPreDefNoRlsDt').attr('checked', false);
    var preFromDt = $('#ReleaseDateRangeParams_PreDefinedFromDt').val();
    var preToDt = $('#ReleaseDateRangeParams_PreDefinedToDt').val();
    if (Date.parse(preToDt) < Date.parse(preFromDt)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.DateValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if (monthDiff(Date.parse(preFromDt), Date.parse(preToDt)) > 6) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseDateRangeValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    $("#btnCreateRightsWorkQRelease").focus();
    return true;
});

$('#ReleaseDateRangeParams_PreDefinedToDt').change(function () {
    $('#chkPreDefNoRlsDt').attr('checked', false);
    var preFromDt = $('#ReleaseDateRangeParams_PreDefinedFromDt').val();
    var preToDt = $('#ReleaseDateRangeParams_PreDefinedToDt').val();
    if (Date.parse(preToDt) < Date.parse(preFromDt)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.DateValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if (monthDiff(Date.parse(preFromDt), Date.parse(preToDt)) > 6) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseDateRangeValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    $("#btnCreateRightsWorkQRelease").focus();
    return true;
});

var searchCritaria = '';
var tabNumber = '';

$(document).ready(function () {
    $('#RepertoireFilter_Artist').focus();

    $('#divOtherParameterTab,#divPredefinedParameter .predefinedParamTabContainer').unbind('keydown').bind({
        keydown: function (e) {
            if (e.keyCode == 13) {
                $("#btnCreateRightsWorkQRelease").click();
            }
        },
        change: function () {
            $('#btnCreateRightsWorkQRelease').focus();
        }
    });
   
    var resVal = $('#hiddenResourceRights').val();
    if (resVal == "Resource") {
        $('#ISRC').val('');
        $('#ISRC').attr("disabled", "disabled");

        $('#RepertoireFilter_UPC').val('');
        $('#RepertoireFilter_UPC').attr("disabled", "disabled");
    }

    releaseCustomParameters = window.rightsReviewWorkQueueMessages;
    $('#splitWarning').hide();
    $("#RepertoireFilter_LinkedContract").attr("readonly", "true");
    $('#FromDt').attr("readonly", "true");
    $('#ToDt').attr("readonly", "true");
    $('#ReleaseDateRangeParams_PreDefinedFromDt').attr("readonly", "true");
    $('#ReleaseDateRangeParams_PreDefinedToDt').attr("readonly", "true");
    //Datepicker functionality
    $(".datefield").css("width", "100px");
    $(".datefield").datepicker({ showOn: 'both', buttonImage: "/GCS/Images/Calender_Icon_img.png", buttonImageOnly: true, changeMonth: true, changeYear: true, yearRange: '1900:2100' });

    //TO Show the Button on page load
    $('#btnCreateRightsWorkQRelease').show();
    $('#btnCancelCreateRightsWorkQRelease').show();




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

        //To hide OK & Cancel buttons
        $(".clearBoth").hide();
    });


    //Click Function to create the New Tab
    $("#btnCreateRightsWorkQRelease").unbind("click").click(function () {
        $("#buttonHide").show();
        //UPC Validation
        if (customParamValidate() == true) {
            var referrer = $('#hiddenReferrerPage').val();
            $('#contractSearchPopup').dialog('close');

            if ($('#divOtherParameterTab').is(':visible')) {
                //custom params
                getCustomSearchParams();
                AddParameterQueryDiv();
            } else {
                //Predefined params

                getPredefinedSearchParams();
                AddParameterQueryDiv();
            }
            setTimeout(function () {
                if (referrer == "Resource") {
                    createCustomResourceTab(""); /* CREATES NEW TAB //Sending empty string while creating from 19-UC*/
                } else {
                    createCustomReleaseTab(""); /* CREATES NEW TAB //Sending empty string while creating from 20-UC*/
                }
            }, 500);
        }
        else {
            return false;
        }
        //End Region Adding

        return false;

    });

    //Click function to cancel.
    $("#btnCancelCreateRightsWorkQRelease").unbind("click").click(function () {
        $("#buttonHide").show();
        $('#contractSearchPopup').dialog('close');
        return false;
    });

    //Release Date Range Change Function for Custom Parameter tab UC19
    $('#chkNoRlsDt').change(function () {
        if ($('#hiddenReferrerPage').val() == "Resource") {

            var noRlsDtCustom = $('#chkNoRlsDt').is(':checked');
            var fromDt = $('#FromDt').val();
            var toDt = $('#ToDt').val();

            if (noRlsDtCustom == true) {
                if ((fromDt != '' || fromDt != null) && (toDt != '' || toDt != null)) {
                    $(".ui-datepicker-trigger").attr('disabled', 'disabled');
                }
            }
            if (noRlsDtCustom == false) {
                $(".ui-datepicker-trigger").removeAttr('disabled');
            }
        }
    });

    //Release Date Range Change Function for Predefined Parameter tab UC19
    $('#chkPreDefNoRlsDt').change(function () {
        if ($('#hiddenReferrerPage').val() == "Resource") {

            var noRlsDtPre = $('#chkPreDefNoRlsDt').is(':checked');
            var preFromDt = $('#ReleaseDateRangeParams_PreDefinedFromDt').val();
            var preToDt = $('#ReleaseDateRangeParams_PreDefinedToDt').val();

            if (noRlsDtPre == true) {
                if ((preFromDt != '' || preFromDt != null) && (preToDt != '' || preToDt != null)) {
                    $(".ui-datepicker-trigger").attr('disabled', 'disabled');
                }
            }
            if (noRlsDtPre == false) {
                $(".ui-datepicker-trigger").removeAttr('disabled');
            }
        }
    });

    if (hdnResourceRights == "Resource") {
        var targetArtist = $("#RepertoireFilter_Artist");

        var isAutoCompleteCalled = false;
        targetArtist.autocomplete({
            // source: targetArtist.attr("data-autocomplete-source-manual"),
            source: function (request, response) {
                isAutoCompleteCalled = true;
                $.getJSON(targetArtist.attr("data-autocomplete-source-manual"),
                    { term: request.term + "^" + $('#chkExactSearch').is(':checked') },
                    function (data) {
                        var autocomplete = targetArtist.data("autocomplete");

                        if (data.length == 1) {
                            autocomplete.selectedItem = data[0];
                        }
                        if (autocomplete.selectedItem) {
                            setTimeout(function () {
                                autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                                targetArtist.autocomplete('close');
                                $("#RepertoireFilter_Artist").focus();
                            }, 200);
                        }

                        response($.map(data, function (item) { return item; }));
                    });
            },
            minLength: 2,
            select: function (event, ui) {
                if (ui.item == null) return;
                $("#RepertoireFilter_Artist").val(ui.item.value);
            },
            response: function (event, ui) {
                var autocomplete = targetArtist.data("autocomplete");
                if (ui.content.length == 1) {
                    autocomplete.selectedItem = ui.content[0];
                }
                if (autocomplete.selectedItem) {
                    setTimeout(function () {
                        autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                        targetArtist.autocomplete('close');
                        $("#RepertoireFilter_Artist").focus();
                    }, 200);
                }
            }
        });
    }

    else if (hdnReleaseRights == "Release") {
        // Artist Auto search
        var targetArtist = $("#RepertoireFilter_Artist");

        var isAutoCompleteCalled = false;
        targetArtist.autocomplete({
            // source: targetArtist.attr("data-autocomplete-source-manual"),
            source: function (request, response) {
                isAutoCompleteCalled = true;
                $.getJSON(targetArtist.attr("data-autocomplete-source-manual"),
                    { term: request.term + "^" + $('#chkExactSearch').is(':checked') },
                    function (data) {
                        var autocomplete = targetArtist.data("autocomplete");

                        if (data.length == 1) {
                            autocomplete.selectedItem = data[0];
                        }
                        if (autocomplete.selectedItem) {
                            setTimeout(function () {
                                autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                                targetArtist.autocomplete('close');
                                $("#RepertoireFilter_Artist").focus();
                            }, 200);
                        }

                        response($.map(data, function (item) { return item; }));
                    });
            },
            minLength: 2,
            select: function (event, ui) {
                if (ui.item == null) return;
                $("#RepertoireFilter_Artist").val(ui.item.value);
            },
            response: function (event, ui) {
                var autocomplete = targetArtist.data("autocomplete");
                if (ui.content.length == 1) {
                    autocomplete.selectedItem = ui.content[0];
                }
                if (autocomplete.selectedItem) {
                    setTimeout(function () {
                        autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                        targetArtist.autocomplete('close');
                        $("#RepertoireFilter_Artist").focus();
                    }, 200);
                }
            }
        });
    }
    // Resource Title Auto Search
    var targetResource = $("#ResourceTitle");
    targetResource.autocomplete({
        source: targetResource.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#ResourceTitle").val(ui.item.value);
            $("#hdnResourceId").val(ui.item.id);
        },
        response: function (event, ui) {

            var autocomplete = targetResource.data("autocomplete");
            if (ui.content.length == 1) {
                autocomplete.selectedItem = ui.content[0];
            }
            if (autocomplete.selectedItem) {
                setTimeout(function () {
                    autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                    targetResource.autocomplete('close');
                    $("#ResourceTitle").focus();
                }, 200);
            }
        }
    });



    //clearanceAdmin Company Auto search
    var targetAdminCompany = $("#txtAdminCompany");
    targetAdminCompany.autocomplete({
        source: targetAdminCompany.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#txtAdminCompany").val(ui.item.value);
            $("#hdnclearanceCompanyId").val(ui.item.id);
        },
        response: function (event, ui) {

            var autocomplete = targetAdminCompany.data("autocomplete");
            if (ui.content.length == 1) {
                autocomplete.selectedItem = ui.content[0];
            }
            if (autocomplete.selectedItem) {
                setTimeout(function () {
                    autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                    targetAdminCompany.autocomplete('close');
                    $("#txtAdminCompany").focus();
                }, 200);
            }
        }
    });

    //release title Auto search
    var target1 = $("#RepertoireFilter_ReleaseTitle");
    target1.autocomplete({
        source: target1.attr("data-autocomplete-source-manual"),
        minLength: 2,
        select: function (event, ui) {
            $("#RepertoireFilter_ReleaseTitle").val(ui.item.value);
            $("#hdnReleaseId").val(ui.item.id);
        },
        response: function (event, ui) {

            var autocomplete = target1.data("autocomplete");
            if (ui.content.length == 1) {
                autocomplete.selectedItem = ui.content[0];
            }
            if (autocomplete.selectedItem) {
                setTimeout(function () {
                    autocomplete._trigger("select", event, { item: autocomplete.selectedItem });
                    target1.autocomplete('close');
                    $("#RepertoireFilter_ReleaseTitle").focus();
                }, 200);
            }
        }
    });


});




/*Fills Search Parameters*/
function getCustomSearchParams() {

    var tabIndex = '1';
    var resourceCheck = $('#chkCustomPopupResource').is(':checked');
    var trackCheck = $('#chkCustomPopupTrack').is(':checked');
    var isrc = $('#ISRC').val();
    var upc = $('#RepertoireFilter_UPC').val();
    var artist = $('#RepertoireFilter_Artist').val().replace(/'/g, "''");    
    var isExactSearch = $('#chkExactSearch').is(':checked');
    var resourcetitle;
    if ($('#ResourceTitle').val() != null) {
       resourcetitle = $('#ResourceTitle').val().replace(/'/g, "''");
    }
    var releasetitle = $('#RepertoireFilter_ReleaseTitle').val().replace(/'/g, "''");
    var adminCompany = '';
    if (hdnResourceRights == "Resource") {
        adminCompany = 0;
    }
    if ($('#hdnclearanceCompanyId').val() != '') {
        adminCompany = $('#hdnclearanceCompanyId').val();
    }
    var contractIds = $('#hdnReleaseContractId').val();
    var fromDt = $('#FromDt').val();
    var toDt = $('#ToDt').val();
    var noRlsDt = $('#chkNoRlsDt').is(':checked');
    var newForReview = $('#chkCustomPopupNewForReview').is(':checked');
    var finalForReview = $('#chkCustomPopupFinalForReview').is(':checked');
    var final = $('#chkCustomPopupFinal').is(':checked');
    var sample = $('#chkCustomPopupSampleExist').is(':checked');
    var sideArtist = $('#chkCustomPopupSideArtist').is(':checked');
    var queryType = '';

    
    tabNumber = tabIndex;
    if (hdnReleaseRights == "Release") { //UC 20
        searchCritaria = tabIndex + ',' + upc + ',' + artist + ',' + isExactSearch + ',' + releasetitle + ',' + adminCompany + ',' + contractIds + ',' + fromDt + ',' + toDt + ',' + noRlsDt + ',' + queryType;        
    }

    if (hdnResourceRights == "Resource") { //UC 19

        //searchCritaria = tabIndex + ',' + resourceCheck + ',' + trackCheck + ',' + isrc + ',' + upc + ',' + artist + ',' + isExactSearch + ',' + resourcetitle + ',' + releasetitle + ',' + adminCompany + ',' + contractIds.replace(/-/g, ",") +',' + fromDt + ',' + toDt + ',' + noRlsDt + ',' + newForReview + ',' + final + ',' + finalForReview + ',' + sample + ',' + sideArtist + ',' + queryType;
        searchCritaria = tabIndex + ',' + resourceCheck + ',' + trackCheck + ',' + isrc + ',' + upc + ',' + artist + ',' + isExactSearch + ',' + resourcetitle + ',' + releasetitle + ',' + adminCompany + ',' + contractIds.replace(/-/g, "~") + ',' + fromDt + ',' + toDt + ',' + noRlsDt + ',' + newForReview + ',' + final + ',' + finalForReview + ',' + sample + ',' + sideArtist + ',' + queryType;
        window.filterCustomParams = {
            Resource: resourceCheck,
            Track: trackCheck,
            Isrc: isrc,
            Upc: upc,
            ArtistName: artist,
            IsExactSearch: isExactSearch,
            Title: resourcetitle,
            ReleaseTitle: releasetitle,
            NoReleaseDate: noRlsDt,
            TabIndex: tabIndex,
            ClearanceAdminCompany: adminCompany,
            LinkedContract: contractIds.replace(/-/g, ","),
            NewForReview: newForReview,
            FinalForReview: finalForReview,
            Final: final,
            SampleExists: sample,
            SideArtistExists: sideArtist,
            FromDt: fromDt,
            ToDt: toDt            
        };
    }
}

/*Fills hidden div with the search Parameters*/
function AddParameterQueryDiv() {
  
    var releaseDates = '';
    var noRlsDate = '';
    var queryType = '';
    var linkedContract = '';
    var adminCompany = '';
    var resourceCheck = '';
    var trackCheck = '';
    var newForReview = '';
    var final = '';
    var finalForReview = '';
    var sample = '';
    var sideArtist = '';
    if ($('#RepertoireFilter_LinkedContract').val() != null || $('#RepertoireFilter_LinkedContract').val() != '') {
        linkedContract = $('#RepertoireFilter_LinkedContract').val();
    }    
    var splitValues = searchCritaria.split(',');
    if(hdnReleaseRights=="Release"){
    if (splitValues[9] == "true") {
        noRlsDate = 'YES';
    }
    
    if (splitValues[7] != '' && splitValues[8] != '') {
        releaseDates = splitValues[7] + ' to ' + splitValues[8];
    }    
    if (splitValues[5] != '') {
        adminCompany = $('#txtAdminCompany').val();
    }
    
        if (splitValues[0] == 1) {
            paramQuery = '<div class="informationDiv"><span class="alignLeft"><span class="InfoName">Custom Parameter: </span></span>' + tdUpc(splitValues[1]) + tdArtist(splitValues[2]) + tdReleaseTitle(splitValues[4]) + tdDataAdminCompany(adminCompany) + tdLinkedContract(linkedContract) + tdReleaseDateRange(releaseDates) + tdNoReleaseDate(noRlsDate) + '</div>';
        } else {
            if (splitValues[10] == "1") {
                queryType = "Multi-Artist Compilation Releases";
            } else if (splitValues[10] == "2") {
                queryType = "OST Releases only";
            } else if (splitValues[10] == "3") {
                queryType = "Non-Mac Releases with Non-Exclusive tracks";
            }
            paramQuery = '<div class="informationDiv"><span class="alignLeft"><span class="InfoName">Predefined Parameter: </span></span>' + tdPredefinedQuery(queryType) + tdReleaseDateRange(releaseDates) + tdNoReleaseDate(noRlsDate) + '</div>';
        }
    }

    if (hdnResourceRights == "Resource") {
        if (splitValues[1] == "true") {
            resourceCheck = 'YES';
        }
        if (splitValues[2] == "true") {
            trackCheck = 'YES';
        }
        if (splitValues[9] != '') {
            adminCompany = $('#txtAdminCompany').val();
        }        
        if (splitValues[11] != '' && splitValues[12] != '') {
            releaseDates = splitValues[11] + ' to ' + splitValues[12];
        }
        
        if (splitValues[13] == "true") {
            noRlsDate = 'YES';
        }
        if (splitValues[14] == "true") {
            newForReview = 'YES';
        }
        if (splitValues[15] == "true") {
            final = 'YES';
        }
        if (splitValues[16] == "true") {
            finalForReview = 'YES';
        }
        if (splitValues[17] == "true") {
            sample = 'YES';
        }
        if (splitValues[18] == "true") {
            sideArtist = 'YES';
        }
        if (splitValues[0] == 1) {
            resourceParamQuery = '<div class="informationDiv"><span class="alignLeft"><span class="InfoName">Custom Parameter: </span></span>' + tdresource(resourceCheck) + tdtrack(trackCheck) + tdIsrc(splitValues[3]) + tdUpc(splitValues[4]) + tdArtist(splitValues[5]) + tdResourceTitle(splitValues[7]) + tdReleaseTitle(splitValues[8]) + tdDataAdminCompany(adminCompany) + tdLinkedContract(linkedContract) + tdReleaseDateRange(releaseDates) + tdNoReleaseDate(noRlsDate) + tdNewForReview(newForReview) + tdFinal(final) + tdFinalForReview(finalForReview) + tdsampleExists(sample) + tdsideArtistExists(sideArtist) + '</div>';
        }
        else {
            if (splitValues[19] == "1") {
                queryType = "Resource Rights Data Sets where the earliest release date is less than today's date + n Weeks ";
            } else if (splitValues[19] == "2") {
                queryType = "Track Rights Data Sets where the earliest release date is less than today's + n weeks ";
            } else if (splitValues[19] == "3") {
                queryType = "Track & Resource Rights Data Sets where the earliest release date is less than today's date + n weeks ";
            } else if (splitValues[19] == "4") {
                queryType = "Track Rights Data Set for tracks on releases which are not Multi-Artist Compilations (*Date Parameter Required) ";
            } else if (splitValues[19] == "5") {
                queryType = "Rights Data Sets for Resources with Samples (*Date Parameter Required) ";
            } else if (splitValues[19] == "6") {
                queryType = "Rights Data Sets for Resources with Side artists (*Date Parameter Required) ";
            } else if (splitValues[19] == "7") {
                queryType = "Rights Data Sets for Resources with Digital Restrictions marked as Consent Required, Consult or Refer to Legal (*Date Parameter Required)  ";
            }
            resourceParamQuery = '<div class="informationDiv"><span class="alignLeft"><span class="InfoName">Predefined Parameter: </span></span>' + tdPredefinedQuery(queryType) + tdReleaseDateRange(releaseDates) + tdNoReleaseDate(noRlsDate) + '</div>';
        }
    }
}

function tdresource(resourceCheck) {
    var tag = '';
    if (resourceCheck == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Resource:&nbsp;</span><span class="contractInfo" id="secResourceCheck">' + resourceCheck + '</span></span>';
    }
    return tag;
}

function tdtrack(trackCheck) {
    var tag = '';
    if (trackCheck == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Track:&nbsp;</span><span class="contractInfo" id="secResourceCheck">' + trackCheck + '</span></span>';
    }
    return tag;
}

function tdIsrc(isrc) {
    var tag = '';
    if (isrc != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">ISRC:&nbsp;</span><span class="contractInfo" id="secIsrc">' + isrc + '</span></span>';
    }
    return tag;
}

function tdUpc(upc) {
    var tag = '';
    if (upc != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">UPC:&nbsp;</span><span class="contractInfo" id="secUpc">' + upc + '</span></span>';
    }
    return tag;
}
function tdArtist(artist) {
    var tag = '';
    if (artist != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Artist Name:&nbsp;</span><span class="contractInfo" id="secArtist">' + artist + '</span></span>';
    }
    return tag;
}

function tdResourceTitle(resource) {
    var tag = '';
    if (resource != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Resource Title:&nbsp;</span><span class="contractInfo" id="secResource">' + resource + '</span></span>';
    }
    return tag;
}

function tdReleaseTitle(release) {
    var tag = '';
    if (release != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Release Title:&nbsp;</span><span class="contractInfo" id="secRelease">' + release + '</span></span>';
    }
    return tag;
}

function tdDataAdminCompany(adminCompany) {
    var tag = '';
    if (adminCompany != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Data Admin Company:&nbsp;</span><span class="contractInfo" id="secAdmin">' + adminCompany + '</span></span>';
    }
    return tag;
}

function tdLinkedContract(linkedContract) {
    var tag = '';
    if (linkedContract != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Linked Contract:&nbsp;</span><span class="contractInfo" id="secLinkedContract">' + linkedContract + '</span></span>';
    }
    return tag;
}

function tdReleaseDateRange(releaseDates) {
    var tag = '';
    if (releaseDates != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Release Date Range:&nbsp;</span><span class="contractInfo" id="secReleaseDates">' + releaseDates + '</span></span>';
    }
    return tag;
}

function tdNoReleaseDate(noRlsDate) {
    var tag = '';
    if (noRlsDate == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">No Release Date:&nbsp;</span><span class="contractInfo" id="secNoRlsDate">' + noRlsDate + '</span></span>';
    }
    return tag;
}

function tdNewForReview(newForReview) {
    var tag = '';
    if (newForReview == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">New For Review:&nbsp;</span><span class="contractInfo" id="secNewForReview">' + newForReview + '</span></span>';
    }
    return tag;
}

function tdFinal(final) {
    var tag = '';
    if (final == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Final:&nbsp;</span><span class="contractInfo" id="secFinal">' + final + '</span></span>';
    }
    return tag;
}

function tdFinalForReview(finalForReview) {
    var tag = '';
    if (finalForReview == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Final For Review:&nbsp;</span><span class="contractInfo" id="secFianlForReview">' + finalForReview + '</span></span>';
    }
    return tag;
}
function tdsampleExists(sample) {
    var tag = '';
    if (sample == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Sample Exists:&nbsp;</span><span class="contractInfo" id="secSample">' + sample + '</span></span>';
    }
    return tag;
}

function tdsideArtistExists(sideArtist) {
    var tag = '';
    if (sideArtist == 'YES') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="InfoName">Side Artist Exists:&nbsp;</span><span class="contractInfo" id="secSideArtist">' + sideArtist + '</span></span>';
    }
    return tag;
}
function tdPredefinedQuery(queryType) {
    var tag = '';
    if (queryType != '') {
        tag = '<span class="alignleft"><span class="contractInfoPipe">|</span><span class="contractInfo" id="secQueryType">' + queryType + '</span></span>';
    }
    return tag;
}

function getPredefinedSearchParams() {

    var tabIndex = '2';
    var queryType = '';

    //Release Part// UC 20
    var releaseVal = $('#hiddenReleaseRights').val();
    if (releaseVal == "Release") {
        if ($('#ReleasePredefinedParameterType0').is(':checked')) {
            queryType = '1';
        }
        else if ($('#ReleasePredefinedParameterType1').is(':checked')) {
            queryType = '2';
        }
        else if ($('#ReleasePredefinedParameterType2').is(':checked')) {
            queryType = '3';
        }
    }
    //Resource Part// UC 19
    else {
        if ($('#ResourcePredefinedParameterType0').is(':checked')) {
            queryType = '1';
        }
        else if ($('#ResourcePredefinedParameterType1').is(':checked')) {
            queryType = '2';
        }
        else if ($('#ResourcePredefinedParameterType2').is(':checked')) {
            queryType = '3';
        }
        else if ($('#ResourcePredefinedParameterType3').is(':checked')) {
            queryType = '4';
        }
        else if ($('#ResourcePredefinedParameterType4').is(':checked')) {
            queryType = '5';
        }
        else if ($('#ResourcePredefinedParameterType5').is(':checked')) {
            queryType = '6';
        }
        else if ($('#ResourcePredefinedParameterType6').is(':checked')) {
            queryType = '7';
        } else {
            queryType = '0';
        }
    }
    tabNumber = tabIndex;

    var fromDt = $('#ReleaseDateRangeParams_PreDefinedFromDt').val();
    var toDt = $('#ReleaseDateRangeParams_PreDefinedToDt').val();
    var noRlsDt = $('#chkPreDefNoRlsDt').is(':checked');
    var resourceCheck = '';
    var trackCheck = '';
    var upc = '';
    var artist = '';
    var isExactSearch = '';
    var releasetitle = '';
    var adminCompany = '';
    var contractIds = '';
    var isrc = '';
    var resourcetitle = '';
    var newForReview = '';
    var final = '';
    var finalForReview = '';
    var sample = '';
    var sideArtist = '';

    if (hdnReleaseRights == "Release") { // UC 20
        searchCritaria = tabIndex + ',' + upc + ',' + artist + ',' + isExactSearch + ',' + releasetitle + ',' + adminCompany + ',' + contractIds + ',' + fromDt + ',' + toDt + ',' + noRlsDt + ',' + queryType;        
        window.filterCustomParams = {
            PredefinedQuereyTypeId: queryType,
            FromDt: fromDt,
            ToDt: toDt,
            NoReleaseDate: noRlsDt,
            TabIndex: tabIndex
        };
    }
    
    if(hdnResourceRights=="Resource") { // UC 19
        searchCritaria = tabIndex + ',' + resourceCheck + ',' + trackCheck + ',' + isrc + ',' + upc + ',' + artist + ',' + isExactSearch + ',' + resourcetitle + ',' + releasetitle + ',' + adminCompany + ',' + contractIds + ',' + fromDt + ',' + toDt + ',' + noRlsDt + ',' + newForReview + ',' + final + ',' + finalForReview + ',' + sample + ',' + sideArtist + ',' + queryType;

        window.filterCustomParams = {
            PredefinedQuereyTypeId: queryType,
            FromDt: fromDt,
            ToDt: toDt,
            NoReleaseDate: noRlsDt,
            TabIndex: tabIndex
        }; 
    }
}


/*Reset the Input Fields*/
function FunClear() {
    
    $('#splitWarning').hide();
    $('#RepertoireFilter_UPC').val('');
    $('#RepertoireFilter_Artist').val('');
    $('#RepertoireFilter_ReleaseTitle').val('');
    $('#chkExactSearch').attr('checked', false);
    $('#txtAdminCompany').val('');
    $('#hdnclearanceCompanyId').val('');
    $('#RepertoireFilter_LinkedContract').val('');
    $('#FromDt').val('');
    $('#ToDt').val('');
    $('#chkNoRlsDt').attr('checked', false);
    $('#chkPreDefNoRlsDt').attr('checked', false);
    $('#ReleaseDateRangeParams_PreDefinedFromDt').val('');
    $('#ReleaseDateRangeParams_PreDefinedToDt').val('');
    $('#ReleasePredefinedParameterType0').prop('checked', false);//Can replace with each-function as in priorityworkqueue.js
    $('#ReleasePredefinedParameterType1').prop('checked', false);
    $('#ReleasePredefinedParameterType2').prop('checked', false);


    $('#ResourcePredefinedParameterType0').prop('checked', false);
    $('#ResourcePredefinedParameterType1').prop('checked', false);
    $('#ResourcePredefinedParameterType2').prop('checked', false);
    $('#ResourcePredefinedParameterType3').prop('checked', false);
    $('#ResourcePredefinedParameterType4').prop('checked', false);
    $('#ResourcePredefinedParameterType5').prop('checked', false);
    $('#ResourcePredefinedParameterType6').prop('checked', false);

    $('#chkCustomPopupResource').attr('checked', false);
    $('#chkCustomPopupTrack').attr('checked', false);
    $('#chkCustomPopupNewForReview').attr('checked', false);
    $('#chkCustomPopupFinal').attr('checked', false);
    $('#chkCustomPopupFinalForReview').attr('checked', false);
    $('#chkCustomPopupSampleExist').attr('checked', false);
    $('#chkCustomPopupSideArtist').attr('checked', false);
    $('#ISRC').val('');
    $('#ResourceTitle').val('');
    return true;
}

function FnChkVal() {
    
        $('#FromDt').val('');
        $('#ToDt').val('');
        $('#ReleaseDateRangeParams_PreDefinedFromDt').val('');
        $('#ReleaseDateRangeParams_PreDefinedToDt').val('');
    
}

//Disabling Release Title On Clicking Resource
function disableReleaseTitle() {

    if ($('#chkCustomPopupResource').is(':checked')) {
        $('#RepertoireFilter_ReleaseTitle').val('');
        $('#RepertoireFilter_ReleaseTitle').attr("disabled", "disabled");
        //$('#RepertoireFilter_UPC').val('');
        //$('#RepertoireFilter_UPC').attr("disabled", "disabled");
        $('#ISRC').removeAttr("disabled");
        $('#RepertoireFilter_UPC').removeAttr("disabled");
    }

    if ($('#chkCustomPopupResource').is(':checked')) {
        if ($('#chkCustomPopupTrack').is(':checked')) {
            $('#RepertoireFilter_ReleaseTitle').removeAttr("disabled");
            //$('#RepertoireFilter_UPC').removeAttr("disabled");
        }
    }

    if ($('#chkCustomPopupResource').is(':checked') == false && $('#chkCustomPopupTrack').is(':checked') == false) {
        $('#RepertoireFilter_UPC').val('');
        $('#RepertoireFilter_UPC').attr("disabled", "disabled");

        $('#ISRC').val('');
        $('#ISRC').attr("disabled", "disabled");
    }
}

//Enabling Release Title On clicking Track
function enableReleaseTitle() {
   
    if ($('#chkCustomPopupTrack').is(':checked')) {
        $('#RepertoireFilter_ReleaseTitle').removeAttr("disabled");
        $('#ISRC').removeAttr("disabled");
        $('#RepertoireFilter_UPC').removeAttr("disabled");
    } else {//Track Unchecked//

        if ($('#chkCustomPopupResource').is(':checked')) {
           
            //$('#RepertoireFilter_UPC').val('');
            //$('#RepertoireFilter_UPC').attr("disabled", "disabled");
            $('#RepertoireFilter_ReleaseTitle').val('');
            $('#RepertoireFilter_ReleaseTitle').attr("disabled", "disabled");
            
    }

    }
    if ($('#chkCustomPopupTrack').is(':checked')) {
        if ($('#chkCustomPopupResource').is(':checked')) {
            $('#RepertoireFilter_ReleaseTitle').removeAttr("disabled");
            //$('#RepertoireFilter_UPC').removeAttr("disabled");
        }
    }

    if ($('#chkCustomPopupResource').is(':checked') == false && $('#chkCustomPopupTrack').is(':checked') == false) {
        $('#RepertoireFilter_UPC').val('');
        $('#RepertoireFilter_UPC').attr("disabled", "disabled");

        $('#ISRC').val('');
        $('#ISRC').attr("disabled", "disabled");
    }

}
// Custom Parameter Tab Validation UC19
function customTabValidation() {
   
    var resourceCheck = $('#chkCustomPopupResource').is(':checked');
    var trackCheck = $('#chkCustomPopupTrack').is(':checked');
    var isrc = $('#ISRC').val();
    var upcCustom = $('#RepertoireFilter_UPC').val();
    var artistCustom = $('#RepertoireFilter_Artist').val();
    var resourceTitleCustom = $('#ResourceTitle').val();
    var releaseTitleCustom = $('#RepertoireFilter_ReleaseTitle').val();
    var adminCompanyCustom = $('#txtAdminCompany').val();
    var linkedContractCustom = $('#RepertoireFilter_LinkedContract').val();
    var fromDt = $('#FromDt').val();
    var toDt = $('#ToDt').val();
    var noRlsDtCustom = $('#chkNoRlsDt').is(':checked');
    var newForReview = $('#chkCustomPopupNewForReview').is(':checked');
    var finalStatus = $('#chkCustomPopupFinal').is(':checked');
    var finalForReview = $('#chkCustomPopupFinalForReview').is(':checked');
    var sampleExists = $('#chkCustomPopupSampleExist').is(':checked');
    var sideArtist = $('#chkCustomPopupSideArtist').is(':checked');
    if (!ReleaseDateValidation(fromDt, toDt)) {
        return false;
    }
    
   
    if (resourceCheck == false && trackCheck == false) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append("Please select a Resource and/or Track");
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    } else if ((resourceCheck || trackCheck) && (newForReview || finalStatus || finalForReview || sampleExists || sideArtist)) {
        $('#splitWarning').hide();
        resizePopUp();
        $('#SplitAlert').hide();
        return true;
    } 
    else if (((resourceCheck == true || trackCheck == true) || (resourceCheck == true && trackCheck == true)) && ((newForReview == true)
        || (finalStatus == true)
        || (finalForReview == true)
        || (sampleExists == true)
        || (sideArtist == true))) {
        if ((isrc == '' || isrc == null)
            && (upcCustom == '' || upcCustom == null)
            && (artistCustom == '' || artistCustom == null)
            && (resourceTitleCustom == '' || resourceTitleCustom == null)
            && (releaseTitleCustom == '' || releaseTitleCustom == null)
            && (adminCompanyCustom == '' || adminCompanyCustom == null)
            && (linkedContractCustom == '' || linkedContractCustom == null)
            && ((fromDt == '' || fromDt == null) && (toDt == '' || toDt == null))
            && (noRlsDtCustom == false)) {
            $('#SplitAlert').empty();
            $('#SplitAlert').append("Please Select Any One Parameter along with this");
            $('#splitWarning').show();
            resizePopUp();
            $('#SplitAlert').show();
            return false;
        } else {
            $('#splitWarning').hide();
            resizePopUp();
            $('#SplitAlert').hide();
            return true;
        }
    }

    if ((resourceCheck == true || trackCheck == true) || (resourceCheck == true && trackCheck == true)) {
    if ((isrc == '' || isrc == null)
            && (upcCustom == '' || upcCustom == null)
            && (artistCustom == '' || artistCustom == null)
            && (resourceTitleCustom == '' || resourceTitleCustom == null)
            && (releaseTitleCustom == '' || releaseTitleCustom == null)
            && (adminCompanyCustom == '' || adminCompanyCustom == null)
            && (linkedContractCustom == '' || linkedContractCustom == null)
            && ((fromDt == '' || fromDt == null) && (toDt == '' || toDt == null))
            && (noRlsDtCustom == false)
            && (newForReview == false)
            && (finalStatus == false)
            && (finalForReview == false)
            && (sampleExists == false)
            && (sideArtist == false)) {
            $('#SplitAlert').empty();
            $('#SplitAlert').append("Please select at least one parameter");
            $('#splitWarning').show();
            resizePopUp();
            $('#SplitAlert').show();
            return false;
        } else if (((isrc != '' || isrc != null)
                || (upcCustom != '' || upcCustom != null)
                || (artistCustom != '' || artistCustom != null)
                || (resourceTitleCustom != '' || resourceTitleCustom != null)
                || (releaseTitleCustom != '' || releaseTitleCustom != null)
                || (adminCompanyCustom != '' || adminCompanyCustom != null)
                || (linkedContractCustom != '' || linkedContractCustom != null)) || (((fromDt != '' || fromDt != null) && (toDt != '' || toDt != null))
                    || (noRlsDtCustom == true))) {
            $('#splitWarning').hide();
            resizePopUp();
            $('#SplitAlert').hide();
            return true;
        }
    }

}

//Predefined Parameter Tab Validation UC19

function dateParameterRequired() {


    var preRlsDt = $('#chkPreDefNoRlsDt').is(':checked');
    var preFromDt = $('#ReleaseDateRangeParams_PreDefinedFromDt').val();
    var preToDt = $('#ReleaseDateRangeParams_PreDefinedToDt').val();
    var premultiArtist = $('#ResourcePredefinedParameterType0').is(':checked');
    var preOst = $('#ResourcePredefinedParameterType1').is(':checked');
    var preNonMac = $('#ResourcePredefinedParameterType2').is(':checked');
    var preRelease = $('#ResourcePredefinedParameterType3').is(':checked');
    var preSample = $('#ResourcePredefinedParameterType4').is(':checked');
    var preSideArtists = $('#ResourcePredefinedParameterType5').is(':checked');
    var preDr = $('#ResourcePredefinedParameterType6').is(':checked');
    if (!ReleaseDateValidation(preFromDt, preToDt)) {
        return false;
    }
   
    if ((premultiArtist == null || premultiArtist == false)
        && (preOst == null || preOst == false)
        && (preNonMac == null || preNonMac == false)
        && (preRelease == null || preRelease == false)
        && (preSample == null || preSample == false)
        && (preSideArtists == null || preSideArtists == false)
        && (preDr == null || preDr == false)
        && ((preFromDt == '' || preFromDt == null) && (preToDt == '' || preToDt == null))
        && (preRlsDt == null || preRlsDt == false)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseRequiredFieldValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if ((premultiArtist == true) || (preOst == true) || (preNonMac == true)) {
        $('#SplitAlert').hide();
        $('#splitWarning').hide();
        return true;
    } 
    if ((preRelease == true) || (preSample == true) || (preSideArtists == true) || (preDr == true))
    {
        if(((preFromDt == '' || preFromDt == null) && (preToDt == '' || preToDt == null))
            && (preRlsDt == null || preRlsDt == false))
        {
            $('#SplitAlert').empty();
            $('#SplitAlert').append("Please Select Release Date Range");
            $('#splitWarning').show();
            $('#SplitAlert').show();
            resizePopUp();
            return false;
        }
        else if(((preFromDt != '' || preFromDt != null) && (preToDt != '' || preToDt != null))
            || (preRlsDt != null || preRlsDt != false)) {
            $('#SplitAlert').hide();
            $('#splitWarning').hide();
            return true;
        }
    }

    else {
        return true;
    }

}

//Onchange validation (Custom Parameter)
$("#FromDt").change(function () {
    $('#splitWarning').hide();
    $('#chkNoRlsDt').attr('checked', false);
    var fromDate = $("#FromDt").val();
    var toDate = $("#ToDt").val();
    if (Date.parse(toDate) < Date.parse(fromDate)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.DateValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if (monthDiff(Date.parse(fromDate), Date.parse(toDate)) > 6) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseDateRangeValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    $("#btnCreateRightsWorkQRelease").focus();
    return false;
});

$("#ToDt").change(function () {
    $('#splitWarning').hide();
    $('#chkNoRlsDt').attr('checked', false);
    var fromDate = $("#FromDt").val();
    var toDate = $("#ToDt").val();
    if (Date.parse(toDate) < Date.parse(fromDate)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.DateValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if (monthDiff(Date.parse(fromDate), Date.parse(toDate)) > 6) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseDateRangeValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    $("#btnCreateRightsWorkQRelease").focus();
    return false;
});

//Onchange validation (Predefined Parameter)
$("#ReleaseDateRangeParams_PreDefinedFromDt").change(function () {
    $('#splitWarning').hide();
    var fromDate = $("#ReleaseDateRangeParams_PreDefinedFromDt").val();
    var toDate = $("#ReleaseDateRangeParams_PreDefinedToDt").val();
    if (Date.parse(toDate) < Date.parse(fromDate)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.DateValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if (monthDiff(Date.parse(fromDate), Date.parse(toDate)) > 6) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseDateRangeValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    return false;
});

$("#ReleaseDateRangeParams_PreDefinedToDt").change(function () {
    $('#splitWarning').hide();
    var fromDate = $("#ReleaseDateRangeParams_PreDefinedFromDt").val();
    var toDate = $("#ReleaseDateRangeParams_PreDefinedToDt").val();
    if (Date.parse(toDate) < Date.parse(fromDate)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.DateValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if (monthDiff(Date.parse(fromDate), Date.parse(toDate)) > 6) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseDateRangeValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    return false;
});
/*end on change validation */


//From Date and To Date Validation//
function ReleaseDateValidation(fromDt, toDt) {
    if (fromDt == '' && toDt == '') {
        return true;
    }
    else if (fromDt == '' && toDt != '') {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.SelectFromDate);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    else if (fromDt != '' && toDt == '') {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.SelectToDate);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }

    if (Date.parse(toDt) < Date.parse(fromDt)) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.DateValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
   

    if (monthDiff(Date.parse(fromDt), Date.parse(toDt)) > 6) {
        $('#SplitAlert').empty();
        $('#SplitAlert').append(ReleaseCustomsSearchConstants.ReleaseDateRangeValidation);
        $('#splitWarning').show();
        resizePopUp();
        $('#SplitAlert').show();
        return false;
    }
    return true;
}

function monthDiff(date1, date2) {
    var months = 0;
    var dt1 = new Date(date1);
    var dt2 = new Date(date2);
    var nYears = dt2.getUTCFullYear() - dt1.getUTCFullYear();
    months = dt2.getUTCMonth() - dt1.getUTCMonth() + (nYears != 0 ? nYears * 12 : 0);
    return months;
}

function GetDropDownParameterValues(parameterName) {
    return $('#' + parameterName + '  option:selected').text();
}

function resizePopUp() {

    if (hdnResourceRights == "Resource") {
       $(".releaseRangeContainer").css('top', '350px');
      }
    else if (hdnReleaseRights == "Release") {
        if ($('#splitWarning').css("display") == 'none')
            $(".releaseRangeContainer").css('top', '390px');
        else
            $(".releaseRangeContainer").css('top', '365px');
    }
}

$('#ResourcePredefinedParameterType0').change(function () {
        $('#chkPreDefNoRlsDt').prop('checked', false);
        $('#chkPreDefNoRlsDt').attr('disabled', true);
});
    $('#ResourcePredefinedParameterType1').change(function () {
        $('#chkPreDefNoRlsDt').prop('checked', false);
        $('#chkPreDefNoRlsDt').attr('disabled', true);
    });
    $('#ResourcePredefinedParameterType2').change(function () {
        $('#chkPreDefNoRlsDt').prop('checked', false);
        $('#chkPreDefNoRlsDt').attr('disabled', true);
    });
    $('#ResourcePredefinedParameterType3').change(function () {
        $('#chkPreDefNoRlsDt').attr('disabled', false);
    });
    $('#ResourcePredefinedParameterType4').change(function () {
        $('#chkPreDefNoRlsDt').attr('disabled', false);
    });
    $('#ResourcePredefinedParameterType5').change(function () {
        $('#chkPreDefNoRlsDt').attr('disabled', false);
    });
    $('#ResourcePredefinedParameterType6').change(function () {
        $('#chkPreDefNoRlsDt').attr('disabled', false);
    });
  