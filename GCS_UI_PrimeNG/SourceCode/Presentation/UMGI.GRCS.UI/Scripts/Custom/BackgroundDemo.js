/// <reference path="/GCS/Scripts/Custom/LayoutRoot.js" />
/// <reference path="Admin/MaintainRightsExpiry.js" />

$(document).ready(function () {

    $('.gr_breadCrumbNav').html("Report > > Linking Progress Report");
    //$('#Home').html('<a href="/GCS/Home/Index">System Admin</a>');
    var pagesize = 25;

    //resizeBackground();
    $("window").resize(function () {
        //  resizeBackground();
    });

    $('#jqgrid').jtable({
        paging: true,
        pageSize: pagesize,
        sorting: false,
        defaultSorting: 'LastModifiedTime DESC',
        selecting: false,
        multiselect: false,
        selectingCheckboxes: true,
        selectOnRowClick: true,
        loadingRecords: function () {
            $('.jtable .jtable-no-data-row').hide();
            $(".ui-jtable-loading").show();
        },
        recordsLoaded: function (event, data) {
            $('.jtable .jtable-no-data-row').show();
            $(".ui-jtable-loading").hide();
            setToolTip(this);
            $("#jqgrid input").removeAttr("checked");
            $("#jqgrid tr").removeClass("jtable-row-selected");
            pageScroll();
        },
        actions: {
            listAction: '/GCS/Home/BackgroundGrid/'
        },
        fields: {
           
            ItemType:
            {
                title: 'ItemType',
                display: function (data) {
                    if (data.record.ItemType == null) {
                        return '';
                    }
                    return data.record.ItemType.toString();
                }
            },
             trait:
            {
                title: 'Trait',
                display: function (data) {
                    if (data.record.Trait == null) {
                        return '';
                    }
                    return data.record.Trait;
                }
            },
             ProjectIdUPCISRC:
            {
                title: 'Project ID/UPC/ISRC',
                display: function (data) {
                    if (data.record.ProjectIdUPCISRC == null) {
                        return '';
                    }
                    return data.record.ProjectIdUPCISRC;
                }
            },
            Repertoire:
                {
                    title: 'Total No of Repertoire',
                    display: function (data) {
                        if (data.record.Repertoire == null) {
                            return '';
                        }
                        return data.record.Repertoire.toString();
                    }
                },
              Artist:
            {
                title: 'Artist',
                display: function (data) {
                    if (data.record.ARTIST == null) {
                        return '';
                    }
                    return data.record.ARTIST;
                }
            },
             description:
            {
                title: 'Title/Description',
                display: function (data) {
                    if (data.record.description == null) {
                        return '';
                    }
                    return data.record.description;
                }
            },
             VersionTitle:
            {
                title: 'Version Title',
                display: function (data) {
                    if (data.record.trait == null) {
                        return '';
                    }
                    return data.record.VersionTitle;
                }
            },
            'ContractId':
                {
                    title: 'Contract ID',
                    display: function (data) {
                        var id = data.record.ContractId;
                        return id;
                    }
                },
             NoOfRetry:
            {
                title: 'No.of Retry',
                display: function (data) {
                    if (data.record.NoOfRetry == null) {
                        return '';
                    }
                    return data.record.NoOfRetry;
                }
            },
            Processed:
            {
                title: 'Processed',
                display: function (data) {
                    if (data.record.Processed == null) {
                        return '';
                    }
                    if(data.record.Processed=='1')
                        return 'yes';
                    else {
                        return 'No';
                    }
                    return data.record.Processed;
                }
            },
            Processing:
            {
                title: 'Processing',
                display: function (data) {
                    if (data.record.Processing == null) {
                        return '';
                    }
                    if(data.record.Processing=='1')
                        return 'yes';
                     else {
                        return 'No';
                    }
                    return data.record.Processing;
                }
            },
            UnProcessed:
            {
               
                title: 'UnProcessed',
                display: function (data) {
                    
                    if (data.record.UnProcessed == null) {
                        return '';
                    }
                    if(data.record.UnProcessed=='1')
                        return 'yes';
                     else {
                        return 'No';
                    }
                    return data.record.UnProcessed;
                }
            },
             modifieddatetime:
                        {
                            title: 'Date',
                            display: function (data) {
                                return data.record.modifieddatetime;
                            },
                            type: 'date',
                            displayFormat: $.datepicker.regional[''].dateFormat
                        },
            username:
            {
                title: 'User Name',
                display: function (data) {
                    if (data.record.username == null) {
                        return '';
                    }
                    return data.record.username;
                }
            },
            Repertoire:
                {
                    title: 'Repertoire ID',
                    display: function (data) {
                        var id = data.record.Repertoire;
                        return id;
                    }
                },
            
        },
        selectionChanged: function () {
            pageScroll();
        }
    });

    //Datepicker functionality
    $(".datefield").css("width", "100px");
    $(".datefield").datepicker({ showOn: 'both', buttonImage: "/GCS/Images/Calender_Icon_img.png", buttonImageOnly: true, changeMonth: true, changeYear: true, yearRange: '1900:2100' });
    $('#backgroundReset').click(function() {
        $('#contractId').val('') ;
        $('#from').val('');
        $('#to').val('');
    });

    $('#backgroundSubmit').click(function () {
        $('#jqgrid').jtable('load',
            {
                contractId: $('#contractId').val(),
                from: $('#from').val(),
                to: $('#to').val(),
                name: $('#userName').val()
            });
    });

    $('#jqPager select').change(function () {
       pagesize = $('#jqPager select').val();
        $('#jqgrid').jtable({ 'pageSize': pagesize });
        $('#jqgrid').jtable('load',
        {
            contractId: $('#contractId').val(),
            from: $('#from').val(),
            to: $('#to').val(),
            name: $('#userName').val(),
            jtPageSize: pagesize
        });
    });


});

    function resizeBackground() {
        var w1 = $("#contractId").width();
        $("#from").css("width", w1 - 200);
        $("#to").css("width", w1 - 200);
        $(".thirdBox").css("margin-left", w1-250);
    }