/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
var resourcetabmessage = {
    Isrc: "ISRC", ResourceTitle: "Resource Title", VersionTitle: "Resource Version Title", ArtistName: "Artist Name", RightsType: "Rights Type",
    PYear: "P Year", PCompanyId: "P Company", PLicensingExtension: "P Local Licensing Extension", Filtermessage: "The search results matching the search criteria exceed from 1000 results. Please refine your search criteria and try again", Searchmessage: "Please specify atleast one search criteria", Resourcetypemessage: "Please Provide Any Search Parameter along with resource type"
};

var resourceCheckstate;
var datet;
var rowIndex;
var $selectedResources;
$(document).ready(function () {
    // $('#ResourceSearch_Isrc').focus();
    $('#ResourceSearch_IsArtistExactSearch').click(function (e) {}
        e.stopPropagation();
        if (this.checked) {
            resourceCheckstate = true;
        }
        else {
            resourceCheckstate = false;
        }
    });
    HideWarningSuccess();
    var pageSize = 25;
    var rowCount = -1;
    pageSize = pageSize;
    $('#ResourceSearch_ArtistId').val('');
    $('#resouceGrid').jtable({
        paging: true,
        pageSize: pageSize,
        sorting: true,
        addNewRecord: '+ Add new record',
        defaultSorting: 'Name ASC',
        selecting: true,
        selectingCheckboxes: true,
        multiselect: true,
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
        },
        recordsLoaded: function (event, data) {
            if (data.serverResponse.R2rowsRetrieved >= 1000) {
                alert(resourcetabmessage.Filtermessage);
            }
            $(".ui-jtable-loading").hide();
            setToolTip(this);
            var rowIndex = data.serverResponse.RowIndex;
            grsResourceSearch(rowIndex);
            rowCount = data.serverResponse.TotalRecordCount;
            document.getElementById("resourceResultCount").innerHTML = "Search Results(" + rowCount + ")";
            $('.jtable .jtable-no-data-row').show();
        },
        actions: {
            listAction: '/GCS/Resource/RepertoireSearchForResource'
        },
        fields:
            {
                'Images': {
                    title: '',
                    sorting: false,
                    display: function (resource) {
                        var image;
                        if (resource.record.ResourceType == "AUDIO") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/ResourceAudio.gif" >' + '<img  src="/Gcs/Images/linked_Contract.png" >');
                                var htmlText = "";
                                for (var a = 0; a < resource.record.LinkedContractDetails.length; a++) {
                                    var rea = /-?\d+/;
                                    var ma = rea.exec(resource.record.LinkedContractDetails[a].CommencementDate);
                                    var dateString5 = '';
                                    if (ma != null) {
                                        var date = new Date(parseInt(ma[0]));
                                        var monthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString5 = date.getDate().toString() + ' ' + monthArray[date.getMonth("mmm")] + ' ' + date.getFullYear().toString();
                                    }
                                    if (test.record.LinkedContractDetails[a].ArtistName == '' || test.record.LinkedContractDetails[a].ArtistName == undefined) {
                                        htmlText = htmlText + 'Contracting Party:' + resource.record.LinkedContractDetails[a].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString5 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[a].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText = htmlText + 'Artist:' + resource.record.LinkedContractDetails[a].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString5 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[a].ClearanceCompanyCountry + "<br>";
                                    }

                                    $(image).prop("title", "-", htmlText);
                                    $(image).tooltip({ showURL: false, showBody: "-" });
                                }
                            } else {
                                image = $('<img  src="/Gcs/Images/ResourceAudio.gif" title="Audio">');
                            }
                        }
                        if (resource.record.ResourceType == "VIDEO") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/ResourceVideo.gif">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText1 = "";
                                for (var v = 0; v < resource.record.LinkedContractDetails.length; v++) {
                                    var rev = /-?\d+/;
                                    var mv = rev.exec(resource.record.LinkedContractDetails[v].CommencementDate);
                                    var dateString4 = '';
                                    if (mv != null) {
                                        var vdate = new Date(parseInt(mv[0]));
                                        var monthArrayv = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString4 = vdate.getDate().toString() + ' ' + monthArrayv[vdate.getMonth("mmm")] + ' ' + vdate.getFullYear().toString();
                                    }

                                    if (test.record.LinkedContractDetails[v].ArtistName == '' || test.record.LinkedContractDetails[v].ArtistName == undefined) {
                                        htmlText1 = htmlText1 + 'Contracting Party:' + resource.record.LinkedContractDetails[v].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString4 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[v].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText1 = htmlText1 + 'Artist:' + resource.record.LinkedContractDetails[v].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString4 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[v].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText1);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/ResourceVideo.gif" title="Video">');
                            }
                        }
                        if (resource.record.ResourceType == "IMAGE") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/ResourceImage.gif">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText2 = "";
                                for (var i = 0; i < resource.record.LinkedContractDetails.length; i++) {
                                    var rei = /-?\d+/;
                                    var mi = rei.exec(resource.record.LinkedContractDetails[i].CommencementDate);
                                    var dateString3 = '';

                                    if (mi != null) {
                                        var datei = new Date(parseInt(mi[0]));
                                        var monthArray1 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString3 = datei.getDate().toString() + ' ' + monthArray1[datei.getMonth("mmm")] + ' ' + datei.getFullYear().toString();
                                    }

                                    if (test.record.LinkedContractDetails[i].ArtistName == '' || test.record.LinkedContractDetails[i].ArtistName == undefined) {
                                        htmlText2 = htmlText2 + 'Contracting Party:' + resource.record.LinkedContractDetails[i].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString3 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[i].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText2 = htmlText2 + 'Artist:' + resource.record.LinkedContractDetails[i].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString3 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[i].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText2);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/ResourceImage.gif" title="Image">');
                            }
                        }
                        if (resource.record.ResourceType == "MERCH") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/merchandise.png">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText3 = "";
                                for (var m = 0; m < resource.record.LinkedContractDetails.length; m++) {
                                    var rem = /-?\d+/;
                                    var mm = rem.exec(resource.record.LinkedContractDetails[m].CommencementDate);
                                    var dateString2 = '';
                                    if (mm != null) {
                                        var datem = new Date(parseInt(mm[0]));
                                        var monthArray2 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString2 = datem.getDate().toString() + ' ' + monthArray2[datem.getMonth("mmm")] + ' ' + datem.getFullYear().toString();
                                    }
                                    if (test.record.LinkedContractDetails[m].ArtistName == '' || test.record.LinkedContractDetails[m].ArtistName == undefined) {
                                        htmlText3 = htmlText3 + 'Contracting Party:' + resource.record.LinkedContractDetails[m].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString2 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[m].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText3 = htmlText3 + 'Artist:' + resource.record.LinkedContractDetails[m].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString2 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[m].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText3);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/merchandise.png" title="Merchandise">');
                            }
                        }
                        if (resource.record.ResourceType == "OTHER") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/page.gif">' + '<img  src="/Gcs/Images/linked_Contract.png" height="16" width="16">');
                                var htmlText4 = "";
                                for (var o = 0; o < resource.record.LinkedContractDetails.length; o++) {
                                    var reo = /-?\d+/;
                                    var mo = reo.exec(resource.record.LinkedContractDetails[o].CommencementDate);
                                    var dateString1 = '';
                                    if (mo != null) {
                                        var dateo = new Date(parseInt(mo[0]));
                                        var monthArray4 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString1 = dateo.getDate().toString() + ' ' + monthArray4[dateo.getMonth("mmm")] + ' ' + dateo.getFullYear().toString();
                                    }
                                    if (test.record.LinkedContractDetails[o].ArtistName == '' || test.record.LinkedContractDetails[o].ArtistName == undefined) {
                                        htmlText4 = htmlText4 + 'Contracting Party:' + resource.record.LinkedContractDetails[o].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString1 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[o].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText4 = htmlText4 + 'Artist:' + resource.record.LinkedContractDetails[o].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString1 + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[o].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText4);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/page.gif" title="Others" height="16" width="16">');
                            }
                        }
                        if (resource.record.ResourceType == "TEXT") {
                            if (resource.record.IsAlreadyLinked) {
                                image = $('<img  src="/Gcs/Images/script_Text.png">' + '<img  src="/Gcs/Images/linked_Contract.png">');
                                var htmlText5 = "";
                                for (var t = 0; t < resource.record.LinkedContractDetails.length; t++) {
                                    var ret = /-?\d+/;
                                    var mt = ret.exec(resource.record.LinkedContractDetails[t].CommencementDate);
                                    var dateString = '';
                                    if (mt != null) {
                                        datet = new Date(parseInt(mt[0])).getDate().toString();
                                        var monthArray5 = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                                        dateString = datet.getDate().toString() + ' ' + monthArray5[datet.getMonth("mmm")] + ' ' + datet.getFullYear().toString();
                                    }

                                    if (test.record.LinkedContractDetails[t].ArtistName == '' || test.record.LinkedContractDetails[t].ArtistName == undefined) {
                                        htmlText5 = htmlText5 + 'Contracting Party:' + resource.record.LinkedContractDetails[t].ContractingParty + "<br>" +
                                            'Commencement Date:' + dateString + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[t].ClearanceCompanyCountry + "<br>";
                                    } else {
                                        htmlText5 = htmlText5 + 'Artist:' + resource.record.LinkedContractDetails[t].ArtistName + "<br>" +
                                            'Commencement Date:' + dateString + "<br>" + 'Clearance Admin Company :' + resource.record.LinkedContractDetails[t].ClearanceCompanyCountry + "<br>";
                                    }
                                }

                                $(image).prop("title", "-", htmlText5);
                                $(image).tooltip({ showURL: false, showBody: "-" });
                            }
                            else {
                                image = $('<img  src="/Gcs/Images/script_Text.png" title="Text">');
                            }
                        }

                        return image;
                    }
                },
                Isrc: {
                    title: resourcetabmessage.Isrc
                },

                ResourceTitle: {
                    title: resourcetabmessage.ResourceTitle
                },

                VersionTitle: {
                    title: resourcetabmessage.VersionTitle
                },

                ArtistName: {
                    title: resourcetabmessage.ArtistName
                },

                RightsType: {
                    title: resourcetabmessage.RightsType
                },

                PYear: {
                    title: resourcetabmessage.PYear
                },

                PCompanyName: {
                    title: resourcetabmessage.PCompanyId
                },

                PLicensingExtension:
            {
                title: resourcetabmessage.PLicensingExtension
            }
            },
        selectionChanged: function () {
            HideWarningSuccess();
            //Get all selected rows
            var $selectedRows = $('#projectgrid').jtable('selectedRows');

            if ($selectedRows.length > 0) {
                //Show selected rows

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $selectedResources = record;
                });
            }
            else {
                //No rows selected
                $('#hiddenSelectedResource').empty();
            }
        }
    });

    //Accordion style collapse/expand
    $('#resourceaccordion .head').click(function (e) {
        HideWarningSuccess();
        e.preventDefault();
        $(this).next().toggle();
        $(this).find('a').toggleClass('iconBottom');
        return false;
    }).next().show();

    //Show items per page

    $('#resourcePager select').change(function () {
        HideWarningSuccess();
        var ddl = document.getElementById("ResourceSearch");
        var resourceType = ddl.options[ddl.selectedIndex].text;
        pageSize = $('#resourcePager select').val();
        $('#resouceGrid').jtable({ 'pageSize': pageSize });
        $('#resouceGrid').jtable('load',
            {
                clearanceCompanyCountryId: $('#loadClearanceCompanyId').val(),
                isrc: $('#ResourceSearch_Isrc').val(),
                artistID: $('#ResourceSearch_ArtistId').val(),
                artistName: $('#ResourceSearch_ArtistName').val(),
                resourceType: resourceType,
                resourceTitle: $('#ResourceSearch_ResourceTitle').val(),
                resourceVersionTitle: $('#ResourceSearch_VersionTitle').val(),
                isArtistExactSearch: window.resourceCheckstate,
                rowIndex: rowIndex
            });
    });

    //SearchButton
    $('#searchresource').click(function () {
        HideWarningSuccess();

        if ($('#ResourceSearch_Isrc').val() != '' || $('#ResourceSearch_ArtistId').val() != '' || $('#ResourceSearch_ArtistName').val() != '' || $('#ResourceSearch_ResourceTitle').val() != '' || $('#ResourceSearch_VersionTitle').val() != '') {
            $('#resourcehide').show();
            ResourceSearch();
        }
        else if ($('#ResourceSearch').val() != 0) {
            if ($('#ResourceSearch_Isrc').val() == '' || $('#ResourceSearch_ArtistId').val() == '' || $('#ResourceSearch_ArtistName').val() == '' || $('#ResourceSearch_ResourceTitle').val() == '' || $('#ResourceSearch_VersionTitle').val() == '') {
                alert(resourcetabmessage.Resourcetypemessage);
            }
        }
        else {
            // alert(resourcetabmessage.Searchmessage);
            ShowWarning(resourcetabmessage.Searchmessage);
            return false;
        }
    });
    $("#ResourceSearch_Isrc, #ResourceSearch_ArtistId, #ResourceSearch_ArtistName, #ResourceSearch_ResourceTitle, #ResourceSearch_VersionTitle")
          .bind("keyup ", HideWarningSuccess);
});
$("#ResourceSearch").bind("mouseup", HideWarningSuccess);

//ResetButton
$('#resetResource').click(function (e) {
    HideWarningSuccess();
    e.preventDefault();
    $('#ResourceSearch_Isrc').val('');
    $('#ResourceSearch_ArtistId').val('');
    $('#ResourceSearch_ArtistName').val('');
    $('#ResourceSearch')[0].selectedIndex = 0;
    $('#ResourceSearch_ResourceTitle').val('');
    $('#ResourceSearch_VersionTitle').val('');
    $('#ResourceSearch_IsArtistExactSearch').removeAttr('checked');
});

function ResourceSearch() {
    HideWarningSuccess();
    var ddl = document.getElementById("ResourceSearch");
    var resourceType = ddl.options[ddl.selectedIndex].text;

    $('#resouceGrid').jtable('load',
        {
            clearanceCompanyCountryId: $('#loadClearanceCompanyId').val(),
            isrc: $('#ResourceSearch_Isrc').val(),
            artistID: $('#ResourceSearch_ArtistId').val(),
            artistName: $('#ResourceSearch_ArtistName').val(),
            resourceType: resourceType,
            resourceTitle: $('#ResourceSearch_ResourceTitle').val(),
            resourceVersionTitle: $('#ResourceSearch_VersionTitle').val(),
            isArtistExactSearch: window.resourceCheckstate,
            rowIndex: -1
        });
}

function grsResourceSearch(rowIndex) {
    var ddl = document.getElementById("ResourceSearch");
    var resourceType = ddl.options[ddl.selectedIndex].text;
    $('#resouceGrid').jtable('reset',
        {
            clearanceCompanyCountryId: $('#loadClearanceCompanyId').val(),
            isrc: $('#ResourceSearch_Isrc').val(),
            artistID: $('#ResourceSearch_ArtistId').val(),
            artistName: $('#ResourceSearch_ArtistName').val(),
            resourceType: resourceType,
            resourceTitle: $('#ResourceSearch_ResourceTitle').val(),
            resourceVersionTitle: $('#ResourceSearch_VersionTitle').val(),
            isArtistExactSearch: window.resourceCheckstate,
            rowIndex: rowIndex
        });
}