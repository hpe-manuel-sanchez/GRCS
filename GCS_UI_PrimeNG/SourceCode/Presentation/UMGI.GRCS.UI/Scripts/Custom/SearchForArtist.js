/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />

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
    addInfo: 'Additional Info',
    searchValid: 'Please Enter Atleast one Search Criteria',
    Filtermessage: "The search results matching the search criteria exceed from 1000 results. Please refine your search criteria and try again"
};
var accountId;
var artistPageSize = 25;
var rowIndex;
var checkstate;
$(function () {
    $('#resetButton').click(function (e) {
        HideWarningSuccess();
        e.preventDefault();
        $('#Artist_Info_Name').val('');
        $('#Artist_FirstName').val('');
        $('#Artist_LastName').val('');
        $('#Artist_Info_Id').val('');
        //        var grd = document.getElementById('searchArtistResult');
        //        grd.style.display = 'none';
        var grid = document.getElementById('ArtistPaging');
        grid.style.display = 'none';
    });

    resizeArtistPop();
    $('#searchArtistResult').hide();

    $('#Artist_Info_Name').focus();

    $('#searchArtistResult').jtable({
        paging: true,
        pageSize: artistPageSize,
        sorting: true,
        selecting: true,
        selectOnRowClick: true,
        defaultSorting: 'FirstName ASC',
        addQuery: true,
        queryString: '&artistRowIndex=' + rowIndex,
        actions: {
            listAction: '/GCS/Contract/SearchForArtist'
        },
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
        },

        recordsLoaded: function (event, data) {
            $(".ui-jtable-loading").hide();
            var rowCount = data.serverResponse.TotalRecordCount;
            $('#searchArtistResultCount').html('Search Result (' + rowCount + ')');
            rowIndex = data.serverResponse.RowIndex;
            grsArtistSearch(rowIndex, checkstate);
            if (data.serverResponse.R2RowRetrieved >= 1000) {
                alert(searchArtistMessages.Filtermessage);
            }
            $('.jtable .jtable-no-data-row').show();
        },
        fields: {
            'Info': {
                title: searchArtistMessages.talentTitle,
                display: function (name) {
                    var dispname = name.record.Info.Id;
                    return dispname;
                }
            },
            FirstName: {
                title: searchArtistMessages.firstName
            },
            LastName: {
                title: searchArtistMessages.lastName
            },
            Info_Name: {
                title: searchArtistMessages.displayName,
                display: function (name) {
                    var dispname = name.record.Info.Name;
                    return dispname;
                }
            },
            Title: {
                title: searchArtistMessages.title
            },
            Prefix: {
                title: searchArtistMessages.titlePrefix
            },

            RolesPerformed: {
                title: searchArtistMessages.rolesPerformed
            },
            DataAdminCompany: {
                title: searchArtistMessages.dataAdmnCmpny
            },
            AliasIndicator: {
                title: searchArtistMessages.aliasIndicator
            },
            AdditonalInfo: {
                title: searchArtistMessages.addInfo
            },
        },
        selectionChanged: function () {
            HideWarningSuccess();
            var $selectedRows = $('#searchArtistResult').jtable('selectedRows');
            $('#ArtistTitle').empty();
            $('#ArtistFname').empty();
            $('#ArtistPrefix').empty();
            $('#ArtistLastname').empty();
            $('#ArtistId').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    var recdisplay = document.getElementById('ArtistTitle');
                    recdisplay.style.display = 'none';
                    var recdisplay1 = document.getElementById('ArtistFname');
                    recdisplay1.style.display = 'none';
                    var recdisplay2 = document.getElementById('ArtistPrefix');
                    recdisplay2.style.display = 'none';
                    var recdisplay3 = document.getElementById('ArtistLastname');
                    recdisplay3.style.display = 'none';
                    var recdisplay4 = document.getElementById('ArtistId');
                    recdisplay4.style.display = 'none';
                    $('#ArtistTitle').append(record.Title);
                    $('#ArtistFname').append(record.FirstName);
                    $('#ArtistPrefix').append(record.Prefix);
                    $('#ArtistLastname').append(record.LastName);
                    $('#ArtistId').append(record.Info.Id);

                    if (parent.document.getElementById('Contract_ArtistName') != null) {
                        var artistTitle = $('#ArtistTitle')[0].innerHTML;
                        var artistFirstName = $('#ArtistFname')[0].innerHTML;
                        var artistPrefix = $('#ArtistPrefix')[0].innerHTML;
                        var artistLastName = $('#ArtistLastname')[0].innerHTML;
                        var artistId = $('#ArtistId')[0].innerHTML;

                        if (artistTitle == '' || artistTitle == 'null') {
                            artistTitle = '';
                        }

                        if (artistFirstName == '' || artistFirstName == 'null') {
                            artistFirstName = '';
                        }
                        else {
                            if (artistTitle == '') {
                                artistFirstName = artistFirstName;
                            }
                            else {
                                artistFirstName = ' ' + artistFirstName;
                            }
                        }

                        if (artistPrefix == '' || artistPrefix == 'null') {
                            artistPrefix = '';
                        }
                        else {
                            if (artistTitle == '' && artistFirstName == '') {
                                artistPrefix = artistPrefix;
                            }
                            else {
                                artistPrefix = ' ' + artistPrefix;
                            }
                        }

                        if (artistLastName == '' || artistLastName == 'null') {
                            artistLastName = '';
                        }
                        else {
                            if (artistTitle == '' && artistFirstName == '' && artistLastName == '') {
                                artistLastName = artistLastName;
                            }
                            else {
                                artistLastName = ' ' + artistLastName;
                            }
                        }

                        var artistname = artistTitle + artistFirstName + artistPrefix + artistLastName;
                        if (artistname.charAt(0) === '-')
                            artistname = artistname.slice(1);
                        parent.document.getElementById('Contract_ArtistName').value = artistname;
                        parent.document.getElementById('Contract_ArtistId').value = artistId;
                        $('#ArtistContractPopup').dialog('close');
                        return false;
                    }
                });
            }
        }
    });

    // $('#searchArtistResult').hide();

    $('#searchForArtist').unbind('click').click(function (e) {
        resizeArtistPop();
        $('#searchArtistResult').show();
        $('#searchForArtist').focus();
        e.preventDefault();
        var artistName = document.getElementById('Artist_Info_Name').value;
        var artistFirstName = document.getElementById('Artist_FirstName').value;
        var artistLastName = document.getElementById('Artist_LastName').value;
        var artistid = document.getElementById('Artist_Info_Id').value;
        accountId = parent.document.getElementById('Contract_ClearanceCompanyCountryId').value;

        if ((artistName == '') && (artistFirstName == '') && (artistLastName == '') && (artistid == '')) {
            // alert(messageCommon.searchValid);
            $('#SplitAlert').empty();
            $('#SplitAlert').append(searchArtistMessages.searchValid);
            $('#splitWarning').show();
            $('#SplitAlert').show();
            return false;
        }
        else {
            searchArtist(artistName, artistFirstName, artistLastName, rowIndex, accountId, artistid, checkstate);

            $('#searchArtistResult').show();
            var grid = document.getElementById('ArtistPaging');
            grid.style.display = 'block';

            // dynamic height set for height and scroll for search result
        }
        return false;
    });

    $('#ArtistPaging select').change(function () {
        HideWarningSuccess();
        artistPageSize = $('#ArtistPaging select').val();
        $('#searchArtistResult').jtable({ 'pageSize': artistPageSize });
        $('#searchArtistResult').jtable('load', {
            artistName: $('#Artist_Info_Name').val()
        , firstName: $('#Artist_FirstName').val()
        , lastName: $('#Artist_LastName').val()
        , rowIndex: rowIndex
        , accountId: parent.document.getElementById('Contract_ClearanceCompanyCountryId').value
        , artistId: $('#Artist_Info_Id').val()
        , flag: checkstate
        , jtPageSize: artistPageSize
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

    //focusing on search

    if (event.keyCode == 13) { $("searchForArtist").focus(); };
});

function grsArtistSearch(rowIndex, checkstate) {
    $('#searchArtistResult').jtable('reset', {
        artistName: $('#Artist_Info_Name').val()
         , firstName: $('#Artist_FirstName').val()
         , lastName: $('#Artist_LastName').val()
         , rowIndex: rowIndex
         , accountId: accountId
         , artistId: $('#Artist_Info_Id').val()
         , flag: checkstate
    });
}

function searchArtist(artistName, artistFirstName, artistLastName, rowIndex, accountId, artistid, checkstate) {
    $('#searchArtistResult').jtable('load', {
        artistName: artistName
       , firstName: artistFirstName
       , lastName: artistLastName
       , rowIndex: -1
       , accountId: parent.document.getElementById('Contract_ClearanceCompanyCountryId').value
       , artistId: artistid
       , flag: checkstate
    });
}

function resizeArtistPop() {
    var h = $(window).height();
    // $("#searchArtistResult").css('height', h - 90);
    $(".jtable-main-container").css('height', h - 270);
    $("#ArtistContractPopup").css('height', h - 60);
}
//on click hiding error messages
$("#Artist_Info_Name, #Artist_FirstName, #Artist_LastName, #Artist_Info_Id ")
          .bind("keyup", HideWarningSuccess);