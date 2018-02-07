/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var isExistArtist = false;
var hideArtistResult = true;
var rowIndex = -1;

var searchArtistMessages = {
    talentTitle: 'Talent Id',
    firstName: 'First Name',
    lastName: 'Last Name/Group',
    displayName: 'Display Name',
    title: 'Title',
    titlePrefix: 'Prefix',
    rolesPerformed: 'Roles Performed',
    dataAdmnCmpny: 'R2 Data Admin Company',
    aliasIndicator: 'Alias Indicator',
    addInfo: 'Additional Info'
};

var ArtistSearchMessages = {
    serverCommunicationError: 'An error occured while communicating to the server.',
    //loadingMessage: 'Loading records...',
    noDataAvailable: 'Artist cannot be Found',
    areYouSure: 'Are you sure?',
    save: 'Save',
    saving: 'Saving',
    cancel: 'Cancel',
    error: 'Error',
    close: 'Close',
    cannotLoadOptionsFor: 'Can not load options for field {0}'
};

//change the dropdown selection

$(function () {
    $('#ddlSearchProjectPagingArtist').change(function () {
        hideArtistResult = false;
        LoadSearchArtistJtable();

        $('#searchArtistResult').jtable('load', {
            "ArtistName": $('#Artist_FirstName').val(),
            "ArtistID": $('#Artist_Info_Id').val(),
            "PageSize": $('#ddlSearchProjectPagingArtist').val(),
            "UserName": 'vivek_gupta',
            "RowIndex": rowIndex
        });
    });
});

//Reset the value of rowindex
function ArtisteSearch(rowIndex) {
    $('#searchArtistResult').jtable('reset', {
        "ArtistName": $('#Artist_FirstName').val(),
        "ArtistID": $('#Artist_Info_Id').val(),
        "PageSize": $('#ddlSearchProjectPagingArtist').val(),
        "UserName": 'vivek_gupta',
        "RowIndex": rowIndex
    });
}

$(function LoadSearchArtistJtable() {
    var artistPageSize = $('#ddlSearchProjectPagingArtist').val();
    var checkstate;
    $('#searchForArtist').focus();

    $('#btnAddArtist').hide();
    $('#btnCancelArtist').hide();
    $('#searchArtistResult').jtable({
        messages: ArtistSearchMessages, //Set messages as artist messages
        paging: true,
        pageSize: artistPageSize,
        sorting: true,
        selecting: true,
        selectOnRowClick: true,
        multiselect: false,
        defaultSorting: 'FirstName ASC',
        actions: {
            listAction: '/GCS/Artist/ArtistSearch'
        },

        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
        },
        recordsLoaded: function (event, data) {
            rowIndex = data.serverResponse.RowIndex;
            ArtisteSearch(rowIndex);

            showHidePageSections(data);
            $('.jtable .jtable-no-data-row').show();
        },
        fields: {
            radiobutton: {
                title: 'Select',
                width: '1%',
                sorting: false,
                edit: false,
                create: false,
                display: function () {
                    //creating radiobutton column in list
                    var $radiobutton = $('<input type="radio" value="0" name="rbtnSelect" />');
                    return $radiobutton;
                }
            },
            ArtistName: {
                title: "Artist Name",
                width: '25%',
                display: function (name) {
                    var name = name.record.FirstName + " " + name.record.LastName;
                    return name;
                }
            },
            ArtistID: {
                title: "Artist Id",
                width: '20%',
                display: function (Id) {
                    var Id = Id.record.Info.NameId;
                    return Id;
                }
            },
            AdditonalInfo: {
                width: '30%',
                title: searchArtistMessages.addInfo
            },
            TalentId: {
                width: '30%',
                visibility: 'hidden',
                title: searchArtistMessages.talentTitle,
                display: function (Id) {
                    var Id = Id.record.Info.Id;
                    return Id;
                }
            }
        },
        selectionChanged: function () {
            var $selectedRows = $('#searchArtistResult').jtable('selectedRows');
            $('#searchArtistList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    var chselect = $(this)[0].cells[0].children[0].checked;
                    if (chselect == false) {
                        $(this)[0].cells[0].children[0].checked = true;
                    }
                    var recdisplay = document.getElementById('searchArtistList');
                    recdisplay.style.display = 'none';
                    $('#searchArtistList').append(record.FirstName + " " + record.LastName + '|' + record.Info.Id + '|' + record.AdditonalInfo + '|' + record.Info.NameId);
                    // added by dhruv
                    $("#warningmessageartist").hide();
                });
            }
        }
    });

    if (hideArtistResult) {
        $('#searchArtistResult').hide();
    }

    $('#btnRemoveArtist').click(function (event) {
        var noOfItemsSelected = 0;
        var countExisting = $('#hdnExistingArtistCount').val();
        for (l = 0; l < countExisting; l++) {
            var chk = "chkSelect " + l;
            if (document.getElementById(chk).checked == true) {
                noOfItemsSelected = noOfItemsSelected + 1;
            }
        }

        var rowNum = $('#hdnrowId').text();
        var checkIfSelected = false;
        for (k = 0; k < countExisting; k++) {
            var chk = "chkSelect " + k;
            var aName = "artistName " + k;
            var aId = "aId " + k;
            if (document.getElementById(chk).checked == true) {
                if (countExisting > noOfItemsSelected) {
                    checkIfSelected = true;
                    $("#warningmessageartist").hide();

                    var rowid = "row " + k;
                    var artistnameId = document.getElementById(aId).innerHTML;
                    var name = $('#divArtistName').text().split(',')
                    var hdnartistId = $('#hdnArtistId').text().split('=')
                    if (hdnartistId == '') {
                        if ($('#hdnRegularResource').val() == '') {
                            if (rowNum != '') {
                                hdnartistId = $('#hdnArtist' + rowNum).val().split('=')
                            }
                        }
                        else {
                            hdnartistId = $('#hdnArtistRegular' + rowNum).val().split('=')
                        }
                    }
                    for (m = 0; m < hdnartistId.length; m++) {
                        var splitartistId = hdnartistId[m].split(':')
                        if ($.trim(splitartistId[1]) == artistnameId) {
                            hdnartistId.splice(m, 1);
                            name.splice(m, 1);
                        }
                    }

                    $('#hdnArtistId').empty();
                    $('#divArtistName').empty();
                    $('#divArtistName').attr('title', $('#divArtistName').html());
                    if ($('#hdnRegularResource').val() == '') {
                        if (rowNum != '') {
                            $('#divArtistName' + rowNum).empty();
                            $('#hdnArtistId' + rowNum).empty();
                            $('#hdnArtist' + rowNum).val("");
                        }
                    }
                    else {
                        if (rowNum != '') {
                            $('#hdnRegularResourceArtistName' + rowNum).val("");
                            $('#divArtistNameRegular' + rowNum).empty();
                            $('#hdnArtistRegular' + rowNum).empty();
                            $('#hdnArtistRegular' + rowNum).val("");
                        }
                    }
                    var ArtistNames = "";
                    for (n = 0; n < hdnartistId.length; n++) {
                        var splitname = hdnartistId[n].split(':');

                        $('#hdnArtistId').append(hdnartistId[n] + '=');
                        $('#divArtistName').append(splitname[0]);
                        $('#divArtistName').attr('title', $('#divArtistName').html());
                        if (splitname[0] != '') {
                            $('#divArtistName').show();
                            if ($('#hdnRegularResource').val() == '') {
                                if (rowNum != '') {
                                    ArtistNames = (ArtistNames == "") ? splitname[0] : ArtistNames + "," + splitname[0];
                                    $('#divArtistName' + rowNum).empty();
                                    $('#hdnArtistId' + rowNum).append(hdnartistId[n] + '=');
                                }
                            }
                            else {
                                if (rowNum != '') {
                                    if ($('#hdnRegularResourceArtistName' + rowNum).val() == "") {
                                        $('#hdnRegularResourceArtistName' + rowNum).val(splitname[0]);
                                    }
                                    else {
                                        $('#hdnRegularResourceArtistName' + rowNum).val($('#hdnRegularResourceArtistName' + rowNum).val() + "," + splitname[0]);
                                    }
                                    $('#divArtistNameRegular' + rowNum).empty();
                                    var artistName = $('#hdnRegularResourceArtistName' + rowNum).val();
                                    if ($('#hdnRegularResourceArtistName' + rowNum).val().length > 25) {
                                        artistName = $('#hdnRegularResourceArtistName' + rowNum).val().substring(0, 25) + '...';
                                    }
                                    $('#divArtistNameRegular' + rowNum).append(artistName);
                                    $('#divArtistNameRegular' + rowNum).show();
                                    if ($('#hdnArtistRegular' + rowNum).val() == "") {
                                        $('#hdnArtistRegular' + rowNum).val(hdnartistId[n] + '=');
                                    }
                                    else {
                                        $('#hdnArtistRegular' + rowNum).val($('#hdnArtistRegular' + rowNum).val() + hdnartistId[n] + '=');
                                    }
                                    $('#divArtistNameRegular' + rowNum).attr('title', $('#hdnRegularResourceArtistName' + rowNum).val());
                                }
                            }
                        }
                    }
                    if ($('#hdnRegularResource').val() == '') {
                        if (rowNum != '') {
                            $('#divArtistName' + rowNum).attr('title', ArtistNames);
                            var divval = (ArtistNames.length > 25) ? ArtistNames.substring(0, 25) + '...' : ArtistNames;
                            $('#divArtistName' + rowNum).append(divval);
                            $('#divArtistName' + rowNum).show();
                            $('#hdnArtist' + rowNum).val($('#hdnArtistId' + rowNum).text());
                        }
                    }
                    else {
                        if (rowNum != '') {
                            $('#hdnArtistRegular' + rowNum).val($('#hdnArtistRegular' + rowNum).val());
                        }
                    }
                    var element = document.getElementById(rowid);
                    element.removeNode(true);
                }

                else {
                    $("#warningmessageartist").text('Linking atleast one artist is mandatory');
                    $('#warningmessageartist').addClass('warning');
                    $("#warningmessageartist").show();
                    return false;
                }
            }
        }
        if (checkIfSelected == false) {
            if (countExisting > 0) {
                $("#warningmessageartist").text(atleastremoveartist);
                $('#warningmessageartist').addClass('warning');
                $("#warningmessageartist").show();
                return false;
            }
        }
        else {
            $('#ArtistResourcePopup').dialog('close');
            $('#ArtistResourcePopup').dialog({
                autoOpen: true,
                width: 1000,
                resizable: false,
                title: 'Manage Artist',
                modal: true
            });

            var values = {
                "existingArtist": $('#hdnArtistId').text()
            }
            if (values.existingArtist == '') {
                if (rowNum != '') {
                    values = {
                        "existingArtist": $('#hdnArtistId' + rowNum).text()
                    }
                }
            }

            $('#ArtistResourcePopup').load('/GCS/Artist/SearchForArtist', values);
            //added by Dhruv
            $('#tblExistingArtist').show();
            var dialogue = $('.ui-dialog');
            dialogue.animate({ top: "30px" }, 0);
        }
    });

    $('#btnAddArtist').click(function (event) {
        //added by Dhruv

        if ($('#searchArtistList').text() != "") {
            if (parent.document.getElementById('lblArtistName') == null) {
                var artists = $('#searchArtistList')[0].innerHTML.split('|');

                var artistFirstName = artists[0];
                var artistId = artists[3];
                var artistInfo = artists[2];
                var TalentId = artists[1];
                isExistArtist = false;
                CheckExisting(artistId);
                if (!isExistArtist) {
                    //Single resource from Resource Search Popup
                    if ($('#divArtistName').html() == '') {
                        $('#divArtistName').append(artistFirstName);
                        setFreeHandToolTip('divArtistName', artistFirstName);
                    }
                    else {
                        $('#divArtistName').append(',' + artistFirstName);
                        setFreeHandToolTip('divArtistName', artistFirstName);
                        // $('#divArtistName').attr('title', artistFirstName);
                    }
                    $('#divArtistName').show();

                    $('#hdnArtistId').append(artistFirstName + ':' + artistId + ':' + TalentId + '=');
                    if ($('#hdnRegularResource').val() == '') {
                        var rowNum = $('#hdnrowId').text();
                        if (rowNum != '') {
                            //Multiple resource from Resource tab
                            if ($('#divArtistName' + rowNum).html() == '') {
                                $('#divArtistName' + rowNum).append(artistFirstName);
                                setFreeHandToolTip('divArtistName' + rowNum, artistFirstName);
                            }
                            else {
                                $('#divArtistName' + rowNum).append(',' + artistFirstName);
                                setFreeHandToolTip('divArtistName' + rowNum, artistFirstName);
                            }
                            $('#divArtistName' + rowNum).show();
                            //add by vikas, execute the function in only New Release case
                            if ($('#cmbReleaseNewOrExisting').val() == "1") {
                                EnableDuplicateButtonForNewRelease(rowNum);
                            }
                            $('#hdnArtist' + rowNum).val($('#hdnArtist' + rowNum).val() + artistFirstName + ':' + artistId + ':' + TalentId + '=');
                            $('#hdnArtistId' + rowNum).append(artistFirstName + ':' + artistId + ':' + TalentId + '=');
                        }
                    }
                    else {
                        var rowNum = $('#hdnrowId').text();
                        if (rowNum != '') {
                            //Multiple resource from Resource tab
                            if ($('#divArtistNameRegular' + rowNum).html() == '') {
                                $('#hdnRegularResourceArtistName' + rowNum).val("");
                                $('#hdnRegularResourceArtistName' + rowNum).val(artistFirstName);
                                var artistFirstNames = artistFirstName;
                                $('#divArtistNameRegular' + rowNum).attr('title', artistFirstNames);
                                if (artistFirstNames.length > 25) {
                                    var artistFirstNames = artistFirstNames.substring(0, 25) + '...';
                                }
                                $('#divArtistNameRegular' + rowNum).append(artistFirstNames);

                                //setFreeHandToolTip('divArtistNameRegular' + rowNum, artistFirstName);
                            }
                            else {
                                $('#divArtistNameRegular' + rowNum).html("");
                                var artistFirstNames = $('#hdnRegularResourceArtistName' + rowNum).val() + ',' + artistFirstName;
                                $('#hdnRegularResourceArtistName' + rowNum).val("");
                                $('#hdnRegularResourceArtistName' + rowNum).val(artistFirstNames);
                                $('#divArtistNameRegular' + rowNum).attr('title', artistFirstNames);
                                if (artistFirstNames.length > 25) {
                                    var artistFirstNames = artistFirstNames.substring(0, 25) + '...';
                                }
                                $('#divArtistNameRegular' + rowNum).append(artistFirstNames);
                            }

                            $('#divArtistNameRegular' + rowNum).show();
                            $('#hdnArtistRegular' + rowNum).val($('#hdnArtistRegular' + rowNum).val() + artistFirstName + ':' + artistId + ':' + TalentId + '=');
                            $('#hdnArtistIdRegular' + rowNum).append(artistFirstName + ':' + artistId + ':' + TalentId + '=');
                        }
                    }

                    //Added By Deepak Kaushik on 11/25/2012. Dont Delete Below line. Required to remove dynamic div.
                    $('#hdnrowId').empty();
                    $('#ArtistResourcePopup').remove();
                    $('#ArtistResourcePopup').dialog('close'); // added by dhruv
                    $('#btnManageArtist').removeClass('input-validation-error');
                }
            }
        }
        else {
            // added by dhruv
            $("#warningmessageartist").text(atleastaddartist);
            $('#warningmessageartist').addClass('warning');
            $("#warningmessageartist").show();
            return false;
        }
    });

    function CheckExisting(id) {
        var existingArtistId = $('#hdnArtistId').text().split('=');
        $("#spnIsrcArtist").empty();
        for (k = 0; k < existingArtistId.length; k++) {
            var artistId = existingArtistId[k].split(':');
            if (artistId[1] != '' && id != '') {
                if (artistId[1] == id) {
                    if ($("#spnIsrcArtist").text() == "") {
                        $("#spnIsrcArtist").append(id);
                    }
                    else {
                        $("#spnIsrcArtist").append("," + id);
                    }
                    isExistArtist = true;
                }
            }
        }
        if (isExistArtist) {
            $("#trShowMessageArtist").show();
        }
        else {
            $("#trShowMessageArtist").hide();
        }
    }

    $('#searchForArtist').click(function (e) {
        rowIndex = -1;
        pagingCount = $('#ddlSearchProjectPagingArtist').val();
        $('#tblExistingArtist').hide();
        var searchCriteria = {
            "ArtistName": $('#Artist_FirstName').val(),
            "ArtistId": $('#Artist_Info_Id').val(),
            "jtStartIndex": 0,
            "PageSize": pagingCount,
            "RowIndex": '-1',
            "UserName": 'vivek_gupta'
        };
        $('#searchForArtist').focus();
        e.preventDefault();
        var artistFirstName = $('#Artist_FirstName').val();
        var artistid = $('#Artist_Info_Id').val();
        var searchlist = '';
        if (artistFirstName != '') { searchlist = searchlist + artistFirstName + '/'; }
        if (artistid != 0) { searchlist = searchlist + artistid + '/'; }
        searchlist = searchlist.substring(0, searchlist.length - 1)
        document.getElementById('spnSearchResultArtist').innerHTML = searchlist;

        if ((artistFirstName == '') && (artistid == '')) {
            // added by dhruv
            $("#warningmessageartist").text(atleastsearchcriteria);
            $('#warningmessageartist').addClass('warning');
            $("#warningmessageartist").show();
            return false;
        }
        else {
            $('#searchArtistResult').jtable('load', {
                ArtistName: artistFirstName
                    , ArtistID: artistid
                    , PageSize: pagingCount,
                RowIndex: '-1',
                UserName: 'vivek_gupta'
            });

            $('#searchArtistResult').show();
            var grid = document.getElementById('ArtistPaging');
            grid.style.display = 'block';

            $("#warningmessageartist").hide();
        }
        return false;
    });

    function showHidePageSections(data) {
        if (data.serverResponse.TotalRecordCount > 0) {
            $('#btnAddArtist').show();
            $('#tblExistingArtist').hide();
            $("#trShowPagingArtist").show();
            $('#btnCancelArtist').show();
        }
    }

    $('#ArtistPaging select').change(function () {
        artistPageSize = $('#ArtistPaging select').val();
        $('#searchArtistResult').jtable({ 'pageSize': artistPageSize });
        $('#searchArtistResult').jtable('load', {
            artistFName: $('#Artist_FirstName').val()
        , artistId: $('#Artist_Info_Id').val()
        , flag: checkstate
        });
        $('#searchArtistResult').show();
    });

    $('input:checkbox').click(function () {
        if (this.checked == false) {
            checkstate = false;
        }
        else {
            checkstate = true;
        }
    });

    $('#btnCancelArtist').click(function (e) {
        e.preventDefault();
        $('#hdnrowId').empty();
        $('#ArtistResourcePopup').remove();
        $('#ArtistResourcePopup').dialog('close'); // added by dhruv
        $('#btnManageArtist').removeClass('input-validation-error');
    });

    //focusing on search

    if (event.keyCode == 13) { $("searchForArtist").focus(); };

    $('#resetButton').click(function (e) {
        e.preventDefault();
        $('#Artist_FirstName').val('');
        $('#Artist_Info_Id').val('');
        $("#warningmessageartist").hide();
    });
});

//Added for disable duplicate button
function EnableDuplicateButtonForNewRelease(y) {
    if ($('#hdnReleaseRowsCount').val() > 0) { //If there are more than one release
        for (y = 0; y < $('#hdnReleaseRowsCount').val() ; y++) { //loop for release
            $("#btnDuplicate_" + y).attr('disabled', 'disabled'); //set the button as disabled in starting
            $("#btnDuplicate_" + y).addClass("disableButtonColor");
            if (($('#divArtistName' + y).text() != "") && ($('#txtTitle_' + y).val() != "") && ($('#txtNoOfTrack_' + y).val() != "") && ($('#LabelId_' + y).val() != "") && ($('#LabelId_' + y).val() != "") && ($('#ddlMusicType_' + y).val() != "")) {
                $("#btnDuplicate_" + y).removeAttr('disabled'); //set the button as disabled in starting
                $("#btnDuplicate_" + y).removeClass("disableButtonColor");
                $("#btnDuplicate_" + y).removeClass("disableButtonColor");
            }
        }
    }
}

function setFreeHandToolTip(controlName, artistName) {
    var rowFreeHandNum = $('#hdnFreehandRowId').val();
    var controlId = $('#' + controlName);
    var title = $(controlId).attr('title');

    // var artistName = $(controlId).text();
    if (rowFreeHandNum == "1") {
        if (title == '') {
            if (artistName.length > 25) {
                $(controlId).text('');
                $(controlId).attr('title', artistName);
                $(controlId).attr('title', artistName.replace('&amp;', '&'));
                var newartistName = artistName.substring(0, 25) + '...';
                $(controlId).append(newartistName);
            }

            else {
                $(controlId).text('');
                $(controlId).append(artistName);
                $(controlId).attr('title', artistName.replace('&amp;', '&'));
                $(controlId).attr('title', $(controlId).html());
            }
        }

        else {
            title = title + "," + artistName;
            if (title.length > 25) {
                $(controlId).text('');
                $(controlId).attr('title', title);
                $(controlId).attr('title', title.replace('&amp;', '&'));
                var newartistName = title.substring(0, 25) + '...';
                $(controlId).append(newartistName);
            }

            else {
                $(controlId).text('');
                $(controlId).append(title);
                $(controlId).attr('title', $(controlId).html());
                $(controlId).attr('title', title.replace('&amp;', '&'));
            }
        }
    }
    // str.replace("Microsoft", "W3Schools");
}