$(document).ready(function () {
    $('#Link').click(function () {
        $('<div id="Territory">Loading ...</div>').load(this.href).dialog({
            title: titleMessage,
            modal: true,
            show: 'clip',
            hide: 'clip',
            width: "98%",
            height: 530,
            minHeight: 530,
            close: function () {
                $(this).remove();
            }
        });
        return false;
    });

    $("#myjquerymenu .ulGCSMenu").children().last().addClass("last");
    $("#myjquerymenu .ulGCSMenu").children().last().children().first().addClass("rightcurve");
    $('#email_menu').css('margin', '0');
    $('#email_menu').css('padding', '0');

    $("#btnEmail").hover(function () {
        $("#email_menu").show();
        $("#ulMailList").show();
    });
    $("#email_menu").bind('mouseleave', function () {
        $("#email_menu").hide();
    });
});

ShowHelpPopup = function () {
    window.open('/GCS/GCSHelpFiles/Clearance_System.htm#Welcome.htm', "PopupWindow", "resizable=1, scrollbars=1");
}