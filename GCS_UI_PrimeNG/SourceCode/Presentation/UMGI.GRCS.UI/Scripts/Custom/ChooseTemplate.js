$(document).ready(function () {
    //Create dialog
    var objDialog = $('<div id="createContractFromTemplate"></div>')
        .html('<p>' + messageCommon.onLoading + '</p>')
        .dialog({
            autoOpen: false,
            modal: true,
            title: messageCommon.chooseTemplate,
            show: 'clip',
            hide: 'clip',
            width: 500
        });

    //Choose Template click
    $('#ChooseTemplate').one('click', function (e) {
        e.preventDefault();

        //Load partial view
        objDialog.load('/GCS/Contract/GetContractTemplates', "",
        function (responseText, textStatus) {
            if (textStatus == 'error') {
                objDialog.html('<p>' + messageCommon.error + '</p>');
            }
        });

        objDialog.dialog('option', { width: "40%" });
        objDialog.dialog('open');
        return false;
    });

    $('#ContractBtemplate').click(function (e) {
        e.preventDefault();
        var temp = $('#SelectTemplate');
        var index = temp[0].selectedIndex;
        var templateid = temp[0][index].value;
        window.location.href = "/GCS/Contract/GetContractTemplate?id=" + templateid + "&editRequestForm=ContractTab";
    });

    //Disabling button on select -select-    
    $('#ContractBtemplate').attr("disabled", true);//On Opening
    $('#SelectTemplate').change(function () {
        if ($('#SelectTemplate').val() == 0) {
            $('#ContractBtemplate').attr("disabled", true);
        }
        else
            $('#ContractBtemplate').removeAttr("disabled", true);
    });
});
