function GridBegin(sender, args) {
    args.data["ResourceId"] = resourceId;
    $("#TblResourceHistory .MsgBar").empty();
    $("#TblResourceHistory .manualPagerLabel:eq(1)").empty();
    $("#TblResourceHistory .manualPagerLabel:eq(1)").text("Show item per page");
}

function ExportExcelClick(sender, args) {
    var values =
                {
                    "GridData": $("#GrdDetails").val()
                };
    $.post('/GCS/ClearanceInbox/ExportToExcel', values);
}

function ActionSuccess(sender, args) {
    var totCount1 = $("#ResourceHistoryCount").val();
    $("#TblResourceHistory .MsgBar").empty();
    $("#TblResourceHistory .MsgBar").text("ResourceHistory(" + totCount1 + ")");
    setSyncGridToolTip("#TblResourceHistory_Table");
    SyncfusionGridScroll();
}

function SyncfusionGridScroll() {
    var pagerHgt = $(".GridPager").height();
    var headerHgt = $(".GridHeader").height();
    var browsHgt = 0;
    if ($.browser.msie)
        browsHgt = 16;
    else
        browsHgt = 13;
    var reduceHgt = pagerHgt + headerHgt + browsHgt;

    var JtableTop = $("#TblResourceHistory").offset().top;
    var TopfootPos = $(".footer").offset().top;
    var TotRecHeight = $("#TblResourceHistory_Table").height() + reduceHgt;
    var TableHeight = TopfootPos - JtableTop;
    var gridObj = $find("TblResourceHistory");
    if (TotRecHeight >= TableHeight)
        setSyncfusionGridHeight(gridObj, TableHeight - reduceHgt);
    else
        setSyncfusionGridHeight(gridObj, TotRecHeight - reduceHgt + 20);
}

function setSyncfusionGridHeight(gridObj, height) {
    gridObj.set_GridHeight(height);
    gridObj.scroller.sfScrollBar('GetObject').Model.TargetHeight = height;
    gridObj.refreshScroller();
}

Sys.Application.add_load(function () {
    $("#ExportOption").change(function () {
        $find("OrdersGrid").set_exportOption(this.value);
    });
});