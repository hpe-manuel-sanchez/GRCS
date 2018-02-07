
$(document).ready(function () {


    $('#popupwindow').click(function (e) {
        e.preventDefault();
        var objDialog = $('<div></div>')
        .html('<p>vijay venkatesh prasad</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'hiiiiiiii',
            show: 'clip',
            hide: 'clip',
            width: 1000
        });


        //        objDialog.html('<p>Loading...</p>');

        //Load partial view
        //        objDialog.load('/GCS/Contract/SearchContract', "",
        //        function (responseText, textStatus, XMLHttpRequest) {
        //            if (textStatus == 'error') {
        //                NewDialog.html('<p>Error in loading...</p>');
        //            }
        //        }
        //        );

        //Open Dialog       
        objDialog.dialog('open', { title: "hiiiiii" });
    });

});
