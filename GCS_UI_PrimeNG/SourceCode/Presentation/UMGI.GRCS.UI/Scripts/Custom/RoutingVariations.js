//Variables to be removed
var workGroupId = "";
var fromPage = "routingVariations";
var listOfMgCompanies = "";
var companyKeyVal = "";
//added for company
var companies = "";
var routingVariationCompanies = "";
var variationTalent = "";
//
var variationRequestTypeOptionsHtml = "";
var routingRuleId = "";
var routingRuleName = "";
var selectedTalentJsonObj = "";
var newRuleName = "";
var ruleVariationGridMode = false;
var isSaveAsClicked = false;
var isCreateNewRule = false; isAddVariationClicked = false;
var ruleId = 0;
var selectedVariationId = 0;
var variationTerritoryType = "";
var variationCompanyType = "";
var variationlinkclass = ".menucolor";
$('.ui-dialog-titlebar-close').attr("title", "Close");
$(document).ready(function (e) {
    $("#routingRulesAndVariationsDv").removeAttr("style"); //TO DO: remove after applying proper scroll style
    //Create New Rule PopUp - Start
    $("#btnCreateRule").click(function () {
        isSaveAsClicked = false; fnHideSuccessAndWarningMsgs();
        fnCreateRulePopUp();
    });

    $("#btnCreateRuleCancel").click(function () {
        $("#createRulePopUp").dialog('close');
    });

    $("#nav_up").click(function () {
        $("#routingRulesAndVariationsDv").animate({ scrollTop: '0' }, 800);;
    });

    $("#nav_down").click(function () {
        $("#routingRulesAndVariationsDv").animate({ scrollTop: $('#routingRulesAndVariationsDv')[0].scrollHeight }, 1500);
    });

    $("#btnCreateNewRule").click(function () {
        if ($.trim($("#txtNewRuleName").val()) != "") {
            $("#createRulePopUp").dialog('close');
            routingRuleName = $("#txtNewRuleName").val();
            $("#ruleNoAndNameDv").html("<h5 class='downArrow'>" + routingRuleName + "</h5>");

            if (isSaveAsClicked) {
                fnRouitngVariationsSave(isSaveAsClicked);
            }
            else {
                //Load Variation Grid with Parent Variation Record
                ruleVariationGridMode = true;
                isCreateNewRule = true; isAddVariationClicked = false;
                //Load Request Type Dropdown values
                getRequestTypeOptions();
                //Load the Variation Grid
                var RuleVariationsGridObj = $find("RoutingRuleVariationsGrid");
                RuleVariationsGridObj.sendRefreshRequest(); $("#RoutingRuleVariationsGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');

                $("#btnAddVariation").attr("disabled", "disabled"); if (RuleVariationsGridObj.get_ContentTable().getElementsByTagName("tr")[0].cells.length == 1) $("#ExportRuleNumber").val(0);
                $("#RoutingRuleVariationsDv").show();
                var scrollheight = $(window).height() - ($(".headerLayout").height() + $(".footer").height() + 20);
                $("#routingRulesAndVariationsDv").height(scrollheight);
                document.getElementById("routingRulesAndVariationsDv").style.overflowY = "scroll";
                document.getElementById("routingRulesAndVariationsDv").style.overflowX = "hidden";
            }
        }
        else {
            $('#newRoutingRuleErrorMsg').show(); $("#newRoutingRuleErrorMsg").html(mngRoutingMessages.JsMsg_EnterRuleName); //'Please Enter the Rule Name.'); //TO DO: move string to resource file
            $('#txtNewRuleName').addClass('input-validation-error');
        }
    });
    //Create New Rule PopUp - End
});

//Create New Rule Variations Grid- Start
$(document).ready(function (e) {
    //Cancel Variations
    $("#btnCancelVariations").click(function () {
        $("#RoutingRuleVariationsDv").hide(); document.getElementById("routingRulesAndVariationsDv").style.overflowY = "hidden";
        fnHideSuccessAndWarningMsgs();
    });
    //Save As Variations
    $("#btnSaveAsVariations").click(function () {
        fnHideSuccessAndWarningMsgs();
        isSaveAsClicked = true; isAddVariationClicked = false;
        fnCreateRulePopUp();
    });
    //Save Variations
    $("#btnSaveVariations").click(function () {
        fnHideSuccessAndWarningMsgs();
        isSaveAsClicked = false; isAddVariationClicked = false;
        fnRouitngVariationsSave(isSaveAsClicked);
    });
});

function fnRouitngVariationsSave(isSaveAsClick) {
    var variationsGridObj = $find("RoutingRuleVariationsGrid");
    var ruleVariationsHTML = $("#RoutingRuleVariationsGrid").find(".GridContent");

    var saveVariationsJsonObj = fnSaveVariationsJson(variationsGridObj.get_ContentTable().getElementsByTagName("tr"), isSaveAsClick);
    $.ajax({
        url: '/GCS/Routing/SaveRuleVariations/',
        type: 'POST',
        data: saveVariationsJsonObj,
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data == 0 || data > 0) {
                $("#RoutingRuleVariationsDv").hide(); document.getElementById("routingRulesAndVariationsDv").style.overflowY = "hidden";
                if (isCreateNewRule || isSaveAsClicked) {
                    $find("RoutingVariationRuleGrid").sendRefreshRequest();
                    $("#RoutingVariationRuleGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
                    fnShowSuccessMessage(mngRoutingMessages.JsMsg_ParentRuleSaved);
                } else {
                    fnShowSuccessMessage(mngRoutingMessages.JsMsg_VariationSaved);
                }
            }
            else {
                fnShowErrorMessage(mngRoutingMessages.JsMsg_VariationsAdded);
            }
        }
    });
}
//Create New Rule Variations Grid - End

//Add New Variation - Start
$(document).ready(function (e) {
    //Export Variations Data to MS-Excel
    //	$("#btnExportVariations").click(function () {
    //		$.post('/GCS/Routing/ExportRoutingVariations', { ruleNumber: routingRuleId }, function (data) {
    //			debugger;
    //		});

    //	});
    //variationIndexId =
    //Add New Variations row
    $("#btnAddVariation").click(function () {
        isAddVariationClicked = true;
        fnHideSuccessAndWarningMsgs();
        var newinsertTr = $("#RoutingRuleVariationsGrid_Table tr:first").clone();

        var variationIndexId = newinsertTr[0].cells[19].childNodes[0].nodeValue.toString() + "." + $find("RoutingRuleVariationsGrid").get_ContentTable().getElementsByTagName("tr").length.toString();
        var fieldId = "";
        var methValue = "";
        var replaceVarId = '(' + newinsertTr[0].cells[19].childNodes[0].nodeValue + ')';

        newinsertTr[0].cells[1].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[1].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[1].childNodes[1].htmlFor = newinsertTr[0].cells[1].childNodes[0].id;
        newinsertTr[0].cells[1].childNodes[3].id = fnAssignNewFieldId(newinsertTr[0].cells[1].childNodes[3].id, variationIndexId);
        newinsertTr[0].cells[1].childNodes[4].htmlFor = newinsertTr[0].cells[1].childNodes[3].id;

        newinsertTr[0].cells[2].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[2].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[2].childNodes[0].className = newinsertTr[0].cells[2].childNodes[0].className.replace(newinsertTr[0].cells[2].childNodes[0].className.split("_").pop(), variationIndexId);
        newinsertTr[0].cells[2].childNodes[0].value = $("#RoutingRuleVariationsGrid_Table tr:first")[0].cells[2].childNodes[0].value;

        newinsertTr[0].cells[7].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[7].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[7].childNodes[0].name = newinsertTr[0].cells[7].childNodes[0].id;
        newinsertTr[0].cells[7].childNodes[1].id = fnAssignNewFieldId(newinsertTr[0].cells[7].childNodes[1].id, variationIndexId);
        newinsertTr[0].cells[7].childNodes[1].name = newinsertTr[0].cells[7].childNodes[1].id;
        newinsertTr[0].cells[7].childNodes[2].id = fnAssignNewFieldId(newinsertTr[0].cells[7].childNodes[2].id, variationIndexId);
        newinsertTr[0].cells[7].childNodes[2].name = newinsertTr[0].cells[7].childNodes[2].id;
        newinsertTr[0].cells[7].childNodes[3].id = fnAssignNewFieldId(newinsertTr[0].cells[7].childNodes[3].id, variationIndexId);
        methValue = newinsertTr[0].cells[7].innerHTML.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[7].innerHTML = methValue.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[7].childNodes[4].id = fnAssignNewFieldId(newinsertTr[0].cells[7].childNodes[4].id, variationIndexId);

        newinsertTr[0].cells[8].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[8].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[8].childNodes[0].name = newinsertTr[0].cells[8].childNodes[0].id;
        newinsertTr[0].cells[8].childNodes[1].id = fnAssignNewFieldId(newinsertTr[0].cells[8].childNodes[1].id, variationIndexId);
        newinsertTr[0].cells[8].childNodes[1].name = newinsertTr[0].cells[8].childNodes[1].id;
        newinsertTr[0].cells[8].childNodes[2].id = fnAssignNewFieldId(newinsertTr[0].cells[8].childNodes[2].id, variationIndexId);
        newinsertTr[0].cells[8].childNodes[2].name = newinsertTr[0].cells[8].childNodes[2].id;
        newinsertTr[0].cells[8].childNodes[3].id = fnAssignNewFieldId(newinsertTr[0].cells[8].childNodes[3].id, variationIndexId);
        methValue = newinsertTr[0].cells[8].innerHTML.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[8].innerHTML = methValue.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[8].childNodes[4].id = fnAssignNewFieldId(newinsertTr[0].cells[8].childNodes[4].id, variationIndexId);

        newinsertTr[0].cells[9].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[9].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[9].childNodes[0].name = newinsertTr[0].cells[9].childNodes[0].id;
        newinsertTr[0].cells[9].childNodes[1].id = fnAssignNewFieldId(newinsertTr[0].cells[9].childNodes[1].id, variationIndexId);
        methValue = newinsertTr[0].cells[9].innerHTML.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[9].innerHTML = methValue.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[9].childNodes[2].className = fnAssignNewFieldId(newinsertTr[0].cells[9].childNodes[2].className, variationIndexId);
        newinsertTr[0].cells[9].childNodes[2].id = fnAssignNewFieldId(newinsertTr[0].cells[9].childNodes[2].id, variationIndexId);

        newinsertTr[0].cells[10].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[10].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[10].childNodes[0].name = newinsertTr[0].cells[10].childNodes[0].id;
        newinsertTr[0].cells[10].childNodes[1].id = fnAssignNewFieldId(newinsertTr[0].cells[10].childNodes[1].id, variationIndexId);
        newinsertTr[0].cells[10].childNodes[1].name = newinsertTr[0].cells[10].childNodes[1].id;
        newinsertTr[0].cells[10].childNodes[2].id = fnAssignNewFieldId(newinsertTr[0].cells[10].childNodes[2].id, variationIndexId);
        newinsertTr[0].cells[10].childNodes[2].name = newinsertTr[0].cells[10].childNodes[2].id;
        newinsertTr[0].cells[10].childNodes[3].id = fnAssignNewFieldId(newinsertTr[0].cells[10].childNodes[3].id, variationIndexId);
        methValue = newinsertTr[0].cells[10].innerHTML.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[10].innerHTML = methValue.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[10].childNodes[4].id = fnAssignNewFieldId(newinsertTr[0].cells[10].childNodes[4].id, variationIndexId);

        newinsertTr[0].cells[11].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[11].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[11].childNodes[0].name = newinsertTr[0].cells[11].childNodes[0].id;
        newinsertTr[0].cells[11].childNodes[1].id = fnAssignNewFieldId(newinsertTr[0].cells[11].childNodes[1].id, variationIndexId);
        methValue = newinsertTr[0].cells[11].innerHTML.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[11].innerHTML = methValue.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[11].childNodes[2].className = fnAssignNewFieldId(newinsertTr[0].cells[11].childNodes[2].className, variationIndexId);
        newinsertTr[0].cells[11].childNodes[2].id = fnAssignNewFieldId(newinsertTr[0].cells[11].childNodes[2].id, variationIndexId);

        newinsertTr[0].cells[12].childNodes[0].id = fnAssignNewFieldId(newinsertTr[0].cells[12].childNodes[0].id, variationIndexId);
        newinsertTr[0].cells[12].childNodes[0].name = newinsertTr[0].cells[12].childNodes[0].id;
        newinsertTr[0].cells[12].childNodes[1].id = fnAssignNewFieldId(newinsertTr[0].cells[12].childNodes[1].id, variationIndexId);
        methValue = newinsertTr[0].cells[12].innerHTML.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[12].innerHTML = methValue.replace(replaceVarId, "(" + variationIndexId + ")");
        newinsertTr[0].cells[12].childNodes[2].className = fnAssignNewFieldId(newinsertTr[0].cells[12].childNodes[2].className, variationIndexId);
        newinsertTr[0].cells[12].childNodes[2].id = fnAssignNewFieldId(newinsertTr[0].cells[12].childNodes[2].id, variationIndexId);

        newinsertTr[0].cells[13].innerHTML = "";
        newinsertTr[0].cells[13].innerHTML = '<input id="chkNoInterMark" value="' + mngRoutingCntrlLabels.JsCntrlName_No + '" type="checkbox" /><label for="chkNoInterMark">' + mngRoutingCntrlLabels.JsCntrlName_No + '</label>';
        newinsertTr[0].cells[14].innerHTML = "";
        newinsertTr[0].cells[14].innerHTML = '<input id="chkNoNatMark" value="' + mngRoutingCntrlLabels.JsCntrlName_No + '" type="checkbox" /><label for="chkNoNatMark">' + mngRoutingCntrlLabels.JsCntrlName_No + '</label>';
        newinsertTr[0].cells[15].innerHTML = "";
        newinsertTr[0].cells[15].innerHTML = '<input id="chkNoLocalLabelMark" value="' + mngRoutingCntrlLabels.JsCntrlName_No + '" type="checkbox" /><label for="chkNoLocalLabelMark">' + mngRoutingCntrlLabels.JsCntrlName_No + '</label>';
        newinsertTr[0].cells[16].childNodes[0].value = "";
        newinsertTr[0].cells[17].innerHTML = "";
        //newinsertTr[0].cells[17].innerHTML = '&nbsp;&nbsp;' + '<span id=\"lnkRemoveRuleVariation_' + variationIndexId + '\" class=\"menucolor\" onClick=\"RemoveRuleVariation(' + variationIndexId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Remove</span><br /><br /><br />';
        newinsertTr[0].cells[18].childNodes[0].nodeValue = false;
        newinsertTr[0].cells[19].childNodes[0].nodeValue = "";

        newinsertTr.appendTo("#RoutingRuleVariationsGrid_Table");
    });
});
function fnAssignNewFieldId(fieldId, val) {
    return fieldId.replace(fieldId.split("_").pop(), val);
}
//Add New Variation - End

//Load Rule GRid - Start
function RoutingRulesActionBegin(sender, args) {
    args.data["PageSize"] = 10;
    args.data["pageName"] = "Rules";
    $('.EmptyCell').html("Retrieving data...");
}

function RoutingRuleRecordBind(sender, args) {
    if (args.Column.Name == "RuleNumber") {
        $(args.Element)[0].innerHTML = "Rule" + "&nbsp;" + args.Data.RuleNumber;
    }
    if (args.Column.Name == "Update") {
        var ruleUpdateColValue = '<span id=\"lnkUpdateRule\" class=\"updateRule\" onClick=\"UpdateRoutingRule(this)\" style=\"text-decoration: underline; cursor: pointer;\">View/Manage</span><label class=\"menucolor\">' + "&nbsp;|&nbsp;" + '</label>';
        if (args.Data.IsActive == false) {
            ruleUpdateColValue += '<span id=\"lnkActivateRule\" class=\"activateRule\" onClick=\"ActivateRoutingRule(' + args.Data.RuleNumber + ')\" style=\"text-decoration: underline; cursor: pointer;\">Activate</span>';
        } else {
            ruleUpdateColValue += '<span id=\"lnkDeactivateRule\" class=\"deactivateRule\" onClick=\"DeactivateRoutingRule(' + args.Data.RuleNumber + ')\" style=\"text-decoration: underline; cursor: pointer;\">Deactivate</span>';
        }
        $(args.Element)[0].innerHTML = ruleUpdateColValue;
    }
}
//Load Rule GRid - End

//Load Rule Variations GRid - Start
function RuleVariationsActionBegin(sender, args) {
    args.data["PageSize"] = 5;
    args.data["pageName"] = "MangeRuleVariations";
    args.data["ruleNumber"] = (isCreateNewRule == true) ? 0 : ruleId;
    args.data["ruleVariationMode"] = ruleVariationGridMode;
    $('.EmptyCell').html("Retrieving data....");
    //workaround for key press "F, X, P, W" in text area
    //sender.keyConfigMgr.preventKeysFu = sender.keyConfigMgr.preventforParticularKey; //store preventforParticularKey to use later
    //sender.keyConfigMgr.preventforParticularKey = Function.createDelegate(sender.keyConfigMgr, preventKey); // create and assign custom function to preventforParticularKey
}
//workaround for key press "F, X, P, W" in text area
//function preventKey(e) {
//            var code;
//            if (e.keyCode) code = e.keyCode; // ie and mozilla/gecko
//            else if (e.which) code = e.which; // ns4 and opera
//            else code = e.charCode;
//            if ((e.target.tagName == 'TEXTAREA') && (code != 13 || code != 27 || code != 9)) // this code will allow f,x,p,w keys
//                return;
//            this.preventKeysFu(e); //call default function preventforParticularKey
//        }

function RuleVariationRecordBind(sender, args) {
    //if (!ruleVariationGridMode) {
    var chkProjectType = '';
    var ddlRequestType = '';
    var chkSensitiveArtist = '';
    var chkActiveArtist = '';
    var chkMultiArtist = '';
    var chkCompilation = '';
    var releaseTerritoryHtml = '';
    var owningTerritoryHtml = '';
    var owningCompanyHtml = '';
    var requestingTerritoryHtml = '';
    var requestingCompanyHtml = '';
    var talentHtml = '';
    var chkSkipInterMark = '';
    var chkSkipNationalMark = '';
    var chkSkipLocalLabMark = '';
    var listBoxComments = '';
    var updateColHtml = '';
    var hdnFieldId = "";
    var chkId = "";
    var territoryString = "";
    var IncTerrString = "";
    var ExlTerrString = "";
    //set the parent variation row color
    //	if (args.Data.IsParent) { args.Element.bgColor = "#F2F5A9"; }
    var isParentVariation = (isAddVariationClicked) ? false : args.Data.IsParent;

    //debugger;
    if (args.Column.Name == "ProjectType") {
        var projectTypeId = "chkIsMaster_" + args.Data.RoutingVariationId;
        chkProjectType = (args.Data.ProjectType == 1) ? '<input id="' + projectTypeId + '" class="customProjectType" value="' + mngRoutingCntrlLabels.JsCntrlName_Master + '" type="checkbox" checked="true" onClick=\"fnRequestTypeStatus(this)\"/>' : '<input id="' + projectTypeId + '" class="customProjectType" value="' + mngRoutingCntrlLabels.JsCntrlName_Master + '" type="checkbox" onClick=\"fnRequestTypeStatus(this)\"/>';
        chkProjectType += '<label for="' + projectTypeId + '">' + mngRoutingCntrlLabels.JsCntrlName_Master + '</label><br />';
        projectTypeId = projectTypeId.replace("Maste", "Regular");
        chkProjectType += (args.Data.ProjectType == 2) ? '<input id="' + projectTypeId + '" class="customProjectType" value="' + mngRoutingCntrlLabels.JsCntrlName_Regular + '" checked="true" type="checkbox" onClick=\"fnRequestTypeStatus(this)\"/>' : '<input id="' + projectTypeId + '" class="customProjectType" value="' + mngRoutingCntrlLabels.JsCntrlName_Regular + '" type="checkbox" onClick=\"fnRequestTypeStatus(this)\"/>';
        chkProjectType += '<label for="' + projectTypeId + '">' + mngRoutingCntrlLabels.JsCntrlName_Regular + '</label>'; //Regular / Non<br/><br/>&nbsp;&nbsp; Traditional
        $(args.Element)[0].innerHTML = chkProjectType;
    }
    if (args.Column.Name == "RvRequestType") {
        var isMasterProject = false;
        var ddlRequestTypeFieldId = "ddlRequestType_" + args.Data.RoutingVariationId; var requestTypeClass = ddlRequestTypeFieldId.replace("ddl", "");
        ddlRequestType = (args.Data.ProjectType == 1) ? '<select id="' + ddlRequestTypeFieldId + '" class="' + requestTypeClass + '" disabled="disabled">"' + variationRequestTypeOptionsHtml + '"</select>' : '<select id="' + ddlRequestTypeFieldId + '" class="' + requestTypeClass + '">"' + variationRequestTypeOptionsHtml + '"</select>';
        $(args.Element)[0].innerHTML = ddlRequestType; (args.Data.RvRequestType != null || args.Data.RvRequestType != "") ? $(args.Element)[0].childNodes[0].value = args.Data.RvRequestType : $(args.Element)[0].childNodes[0].value = 0;
        //$($(args.Element)[0].innerHTML).value(4);
        //
        //$(args.Element)[0].innerHTML = (args.Data.RvRequestType != null || args.Data.RvRequestType != "") ? ($(ddlRequestType).val(args.Data.RvRequestType)[0].outerHTML || $(ddlRequestType).val(args.Data.RvRequestType).clone()) : ddlRequestType;
    }
    if (args.Column.Name == "IsSensitiveArtist") {
        chkId = "chkYesSensitiveArt_" + args.Data.RoutingVariationId;
        chkSensitiveArtist = (args.Data.IsSensitiveArtist == true) ? '<input id="chkYesSensitiveArt" checked="true" value="' + mngRoutingCntrlLabels.JsCntrlName_Yes + '" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\"/>' : '<input id="chkYesSensitiveArt" value="' + mngRoutingCntrlLabels.JsCntrlName_Yes + '" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\"/>';
        chkSensitiveArtist += '<label for="chkYesSensitiveArt">' + mngRoutingCntrlLabels.JsCntrlName_Yes + '</label><br />';
        chkSensitiveArtist += (args.Data.IsSensitiveArtist == false) ? '<input id="chkNoSensitiveArt" checked="true" value="' + mngRoutingCntrlLabels.JsCntrlName_No + '" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\"/>' : '<input id="chkNoSensitiveArt" value="' + mngRoutingCntrlLabels.JsCntrlName_No + '" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\"/>';
        chkSensitiveArtist += '<label for="chkNoSensitiveArt">' + mngRoutingCntrlLabels.JsCntrlName_No + '</label>';
        $(args.Element)[0].innerHTML = chkSensitiveArtist;
    }
    if (args.Column.Name == "IsActiveArtist") {
        chkActiveArtist = (args.Data.IsActiveArtist == true) ? '<input id="chkYesActiveArt" checked="true" value="Yes" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\" />' : '<input id="chkYesActiveArt" value="Yes" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\" />';
        chkActiveArtist += '<label for="chkYesActiveArt">Yes</label><br />';
        chkActiveArtist += (args.Data.IsActiveArtist == false) ? '<input id="chkNoActiveArt" checked="true" value="No" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\" />' : '<input id="chkNoActiveArt" value="No" type="checkbox" onClick=\"fnChkArtistCompileStatus(this)\" />';
        chkActiveArtist += '<label for="chkNoActiveArt">No</label>';
        $(args.Element)[0].innerHTML = chkActiveArtist;
    }
    if (args.Column.Name == "IsMultiArtist") {
        chkMultiArtist = (args.Data.IsMultiArtist == true) ? '<input id="chkYesMultiArt" onClick=\"fnChkArtistCompileStatus(this)\" checked="true" value="Yes" type="checkbox" />' : '<input id="chkYesMultiArt" onClick=\"fnChkArtistCompileStatus(this)\" value="Yes" type="checkbox" />';
        chkMultiArtist += '<label for="chkYesMultiArt">Yes</label><br />';
        chkMultiArtist += (args.Data.IsMultiArtist == false) ? '<input id="chkNoMultiArt" onClick=\"fnChkArtistCompileStatus(this)\" checked="true" value="No" type="checkbox" />' : '<input id="chkNoMultiArt" onClick=\"fnChkArtistCompileStatus(this)\" value="No" type="checkbox" />';
        chkMultiArtist += '<label for="chkNoMultiArt">No</label>';
        $(args.Element)[0].innerHTML = chkMultiArtist;
    }
    if (args.Column.Name == "IsCompilation") {
        chkCompilation = (args.Data.IsCompilation == true) ? '<input id="chkYesCompile" onClick=\"fnChkArtistCompileStatus(this)\" checked="true" value="Yes" type="checkbox" />' : '<input id="chkYesCompile" onClick=\"fnChkArtistCompileStatus(this)\" value="Yes" type="checkbox" />';
        chkCompilation += '<label for="chkYesCompile">Yes</label><br />';
        chkCompilation += (args.Data.IsCompilation == false) ? '<input id="chkNoCompile" onClick=\"fnChkArtistCompileStatus(this)\" checked="true" value="No" type="checkbox" />' : '<input id="chkNoCompile" onClick=\"fnChkArtistCompileStatus(this)\" value="No" type="checkbox" />';
        chkCompilation += '<label for="chkNoCompile">No</label>';
        $(args.Element)[0].innerHTML = chkCompilation;
    }
    if (args.Column.Name == "ReleaseTerritory") {
        hdnFieldId = createHiddenFieldIds("ReleaseTerritory", args.Data.RoutingVariationId);
        releaseTerritoryHtml = '<input id="' + hdnFieldId[0] + '" name="' + hdnFieldId[0] + '" value="" type="hidden" />';
        territoryString = "hdnReleaseTerritoryInc_" + args.Data.RoutingVariationId; if (args.Data.IncludedReleaseTerritories != null) IncTerrString = args.Data.IncludedReleaseTerritories;
        releaseTerritoryHtml += '<input id="' + territoryString + '" name="' + territoryString + '" value="' + IncTerrString + '" type="hidden" />';
        territoryString = "hdnReleaseTerritoryExc_" + args.Data.RoutingVariationId; if (args.Data.ExcludedReleaseTerritories != null) ExlTerrString = args.Data.ExcludedReleaseTerritories;
        releaseTerritoryHtml += '<input id="' + territoryString + '" name="' + territoryString + '" value="' + ExlTerrString + '" type="hidden" />';
        releaseTerritoryHtml += (args.Data.IncludedReleaseTerritories == null && args.Data.ExcludedReleaseTerritories == null)
        ? '<input id="' + hdnFieldId[1] + '" onClick=\"fnReleaseTerritory(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" /><span id="' + hdnFieldId[2] + '" class=\"menucolor\" onClick=\"fnReleaseTerritory(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">View/Manage</span>'
        : '<input id="' + hdnFieldId[1] + '" onClick=\"fnReleaseTerritory(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" style=\"display:none;\"/><span id="' + hdnFieldId[2] + '" class=\"menucolor\" onClick=\"fnReleaseTerritory(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">View/Manage</span>';
        $(args.Element)[0].innerHTML = releaseTerritoryHtml;
    }
    if (args.Column.Name == "OwningTerritory") {
        hdnFieldId = createHiddenFieldIds("OwningTerritory", args.Data.RoutingVariationId);
        owningTerritoryHtml = '<input id="' + hdnFieldId[0] + '" name="' + hdnFieldId[0] + '" value="" type="hidden" />';
        territoryString = "hdnOwningTerritoryInc_" + args.Data.RoutingVariationId; if (args.Data.IncludedOwningTerritories != null) IncTerrString = args.Data.IncludedOwningTerritories;
        owningTerritoryHtml += '<input id="' + territoryString + '" name="' + territoryString + '" value="' + IncTerrString + '" type="hidden" />';
        territoryString = "hdnOwningTerritoryExc_" + args.Data.RoutingVariationId; if (args.Data.ExcludedOwningTerritories != null) ExlTerrString = args.Data.ExcludedOwningTerritories;
        owningTerritoryHtml += '<input id="' + territoryString + '" name="' + territoryString + '" value="' + ExlTerrString + '" type="hidden" />';
        owningTerritoryHtml += (args.Data.IncludedOwningTerritories == null && args.Data.ExcludedOwningTerritories == null)
        ? '<input id="' + hdnFieldId[1] + '" onClick=\"fnOwningTerritory(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" /><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnOwningTerritory(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">View/Manage</span>'
        : '<input id="' + hdnFieldId[1] + '" onClick=\"fnOwningTerritory(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" style=\"display:none;\"/><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnOwningTerritory(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">View/Manage</span>';
        $(args.Element)[0].innerHTML = owningTerritoryHtml;
    }
    if (args.Column.Name == "OwningCompany") {
        hdnFieldId = createHiddenFieldIds("OwningCompany", args.Data.RoutingVariationId);
        var compVal = (args.Data.OwningCompany != null && args.Data.OwningCompany != "") ? args.Data.OwningCompany : "";
        owningCompanyHtml = '<input id="' + hdnFieldId[0] + '" name="' + hdnFieldId[0] + '" value="' + compVal + '" type="hidden" />';
        owningCompanyHtml += (compVal == "")
        ? '<input id="' + hdnFieldId[1] + '" onClick=\"fnOwningCompany(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" /><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnOwningCompany(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">View/Manage</span>'
        : '<input id="' + hdnFieldId[1] + '" onClick=\"fnOwningCompany(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" style=\"display:none;\"/><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnOwningCompany(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">View/Manage</span>';
        $(args.Element)[0].innerHTML = owningCompanyHtml;
    }
    if (args.Column.Name == "RequestingTerritory") {
        hdnFieldId = createHiddenFieldIds("RequestingTerritory", args.Data.RoutingVariationId);
        requestingTerritoryHtml = '<input id="' + hdnFieldId[0] + '" name="' + hdnFieldId[0] + '" value="" type="hidden" />';
        territoryString = "hdnRequestingTerritoryInc_" + args.Data.RoutingVariationId; if (args.Data.IncludedRequestingTerritories != null) IncTerrString = args.Data.IncludedRequestingTerritories;
        requestingTerritoryHtml += '<input id="' + territoryString + '" name="' + territoryString + '" value="' + IncTerrString + '" type="hidden" />';
        territoryString = "hdnRequestingTerritoryExc_" + args.Data.RoutingVariationId; if (args.Data.ExcludedRequestingTerritories != null) ExlTerrString = args.Data.ExcludedRequestingTerritories;
        requestingTerritoryHtml += '<input id="' + territoryString + '" name="' + territoryString + '" value="' + ExlTerrString + '" type="hidden" />';
        requestingTerritoryHtml += (args.Data.IncludedRequestingTerritories == null && args.Data.ExcludedRequestingTerritories == null)
        ? '<input id="' + hdnFieldId[1] + '" onClick=\"fnRequestingTerritory(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" /><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnRequestingTerritory(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">View/Manage</span>'
        : '<input id="' + hdnFieldId[1] + '" onClick=\"fnRequestingTerritory(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" style=\"display:none;\"/><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnRequestingTerritory(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">View/Manage</span>';
        $(args.Element)[0].innerHTML = requestingTerritoryHtml;
    }
    if (args.Column.Name == "RequestingCompany") {
        hdnFieldId = createHiddenFieldIds("RequestingCompany", args.Data.RoutingVariationId); var compVal = (args.Data.RequestingCompany != null && args.Data.RequestingCompany != "") ? args.Data.RequestingCompany : "";
        requestingCompanyHtml = '<input id="' + hdnFieldId[0] + '" name="' + hdnFieldId[0] + '" value="' + compVal + '" type="hidden" />';
        requestingCompanyHtml += (compVal == "")
        ? '<input id="' + hdnFieldId[1] + '" onClick=\"fnRequestingCompany(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" /><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnRequestingCompany(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">View/Manage</span>'
        : '<input id="' + hdnFieldId[1] + '" onClick=\"fnRequestingCompany(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" style=\"display:none;\"/><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnRequestingCompany(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">View/Manage</span>';
        $(args.Element)[0].innerHTML = requestingCompanyHtml;
    }
    if (args.Column.Name == "ArtistList") {
        hdnFieldId = createHiddenFieldIds("Talent", args.Data.RoutingVariationId);
        var talentVal = (args.Data.variationTalent != null && args.Data.variationTalent != "") ? args.Data.variationTalent : "";
        talentHtml = '<input id="' + hdnFieldId[0] + '" name="' + hdnFieldId[0] + '" value="' + talentVal + '" type="hidden" />';
        talentHtml += (talentVal == "")
        ? '<input id="' + hdnFieldId[1] + '" onClick=\"fnTalent(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" /><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnTalent(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">View/Manage</span>'
        : '<input id="' + hdnFieldId[1] + '" onClick=\"fnTalent(' + args.Data.RoutingVariationId + ')\" value="Add" type="button" style=\"display:none;\"/><span id="' + hdnFieldId[2] + '" class=\"menucolor ' + hdnFieldId[2] + '\" onClick=\"fnTalent(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">View/Manage</span>';
        $(args.Element)[0].innerHTML = talentHtml;
    }
    if (args.Column.Name == "IsSkipIntlMarketing") {
        if (isParentVariation) {
            chkSkipInterMark = (args.Data.IsSkipIntlMarketing == true) ? '<input id="chkYesInterMark" class="customInterMark" checked="true" value="Yes" type="checkbox" />' : '<input id="chkYesInterMark" class="customInterMark" value="Yes" type="checkbox" />';
            chkSkipInterMark += '<label for="chkYesInterMark">Yes</label><br />';
        }
        else {
            chkSkipInterMark = (args.Data.IsSkipIntlMarketing == false) ? '<input id="chkNoInterMark" class="customInterMark" checked="true" value="No" type="checkbox" />' : '<input id="chkNoInterMark" class="customInterMark" value="No" type="checkbox" />';
            chkSkipInterMark += '<label for="chkNoInterMark">No</label>';
        }
        $(args.Element)[0].innerHTML = chkSkipInterMark;
    }
    if (args.Column.Name == "IsSkipNtnlMarketing") {
        if (isParentVariation) {
            chkSkipNationalMark = (args.Data.IsSkipNtnlMarketing == true) ? '<input id="chkYesNatMark" class="customNatMark" checked="true" value="Yes" type="checkbox" />' : '<input id="chkYesNatMark" class="customNatMark" value="Yes" type="checkbox" />';
            chkSkipNationalMark += '<label for="chkYesNatMark">Yes</label><br />';
        }
        else {
            chkSkipNationalMark = (args.Data.IsSkipNtnlMarketing == false) ? '<input id="chkNoNatMark" class="customNatMark" checked="true" value="No" type="checkbox" />' : '<input id="chkNoNatMark" class="customNatMark" value="No" type="checkbox" />';
            chkSkipNationalMark += '<label for="chkNoNatMark">No</label>';
        }
        $(args.Element)[0].innerHTML = chkSkipNationalMark;
    }
    if (args.Column.Name == "IsSkipLocalLabel") {
        if (isParentVariation) {
            chkSkipLocalLabMark = (args.Data.IsSkipLocalLabel == true) ? '<input id="chkYesLocalLabelMark" class="customLocalLabelMark" checked="true" value="Yes" type="checkbox" />' : '<input id="chkYesLocalLabelMark" class="customLocalLabelMark" value="Yes" type="checkbox" />';
            chkSkipLocalLabMark += '<label for="chkYesLocalLabelMark">Yes</label><br />';
        }
        else {
            chkSkipLocalLabMark += (args.Data.IsSkipLocalLabel == false) ? '<input id="chkNoLocalLabelMark" class="customLocalLabelMark" checked="true" value="No" type="checkbox" />' : '<input id="chkNoLocalLabelMark" class="customLocalLabelMark" value="No" type="checkbox" />';
            chkSkipLocalLabMark += '<label for="chkNoLocalLabelMark">No</label>';
        }
        $(args.Element)[0].innerHTML = chkSkipLocalLabMark;
    }

    if (args.Column.Name == "Comments") {
        listBoxComments = (args.Data.Comments != null && args.Data.Comments != "") ? '<textarea id="txtComments" maxlength=\"250\" style=\"width:100px !important;\">' + args.Data.Comments + '</textarea>' : '<textarea  id="txtComments" maxlength=\"250\" style=\"width:100px !important;\"></textarea>';
        $(args.Element)[0].innerHTML = listBoxComments;
    }
    if (args.Column.Name == "IsActive") {
        if (args.Data.IsActive == true) {
            var lnkDeactiveId = "lnkDeactivateRuleVariation_" + args.Data.RoutingVariationId;
            updateColHtml = '&nbsp;&nbsp;' + '<span id=\"' + lnkDeactiveId + '\" class=\"menucolor\" onClick=\"DeactivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Deactivate</span>';
            var lnkActiveId = "lnkActivateRuleVariation_" + args.Data.RoutingVariationId;
            updateColHtml += '<span id=\"' + lnkActiveId + '\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">Activate</span>';
            updateColHtml += '&nbsp;|&nbsp;<a href=\"#\" onClick=\"getRoutingVariationsAuditHistory(this)\" style=\"text-decoration: underline;\">History</a>';
        }
        else if (args.Data.IsActive == false) {
            var lnkActiveId = "lnkActivateRuleVariation_" + args.Data.RoutingVariationId;
            if (isParentVariation) { updateColHtml = (args.Data.IsSkipIntlMarketing == true || args.Data.IsSkipNtnlMarketing == true || args.Data.IsSkipLocalLabel == true) ? '&nbsp;&nbsp;' + '<span id=\"' + lnkActiveId + '\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Activate</span>' : '&nbsp;&nbsp;' + '<span id=\"lnkActivateRuleVariation\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;text-color: gray;\" disabled="disabled">Activate</span>'; }
            else { updateColHtml = (args.Data.IsSkipIntlMarketing == false || args.Data.IsSkipNtnlMarketing == false || args.Data.IsSkipLocalLabel == false) ? '&nbsp;&nbsp;' + '<span id=\"' + lnkActiveId + '\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Activate</span>' : '&nbsp;&nbsp;' + '<span id=\"lnkActivateRuleVariation\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;text-color: gray;\" disabled="disabled">Activate</span>'; }
            var lnkDeactiveId = "lnkDeactivateRuleVariation_" + args.Data.RoutingVariationId;
            updateColHtml += '<span id=\"' + lnkDeactiveId + '\" class=\"menucolor\" onClick=\"DeactivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;display:none;\">Deactivate</span>';
            updateColHtml += '&nbsp;|&nbsp;<a href=\"#\" onclick=\"getRoutingVariationsAuditHistory(this)\" style=\"text-decoration: underline;\">History</a>';
        }
        else {
            if (args.Data.RoutingVariationId > 0) {
                var lnkRemoveId = "lnkRemoveRuleVariation_" + args.Data.RoutingVariationId;
                updateColHtml = (args.Data.IsActive != true && args.Data.IsActive != false) ? '&nbsp;&nbsp;' + '<span id=\"' + lnkRemoveId + '\" class=\"menucolor\" onClick=\"RemoveRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Remove</span><br /><br /><br />' : "";
                var lnkActiveId = "lnkActivateRuleVariation_" + args.Data.RoutingVariationId;
                if (isParentVariation) { updateColHtml += (args.Data.IsSkipIntlMarketing == true || args.Data.IsSkipNtnlMarketing == true || args.Data.IsSkipLocalLabel == true) ? '&nbsp;&nbsp;' + '<span id=\"' + lnkActiveId + '\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Activate</span>' : '&nbsp;&nbsp;' + '<span id=\"lnkActivateRuleVariation\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;text-color: gray;\" disabled="disabled">Activate</span>'; }
                else { updateColHtml += (args.Data.IsSkipIntlMarketing == false || args.Data.IsSkipNtnlMarketing == false || args.Data.IsSkipLocalLabel == false) ? '&nbsp;&nbsp;' + '<span id=\"' + lnkActiveId + '\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Activate</span>' : '&nbsp;&nbsp;' + '<span id=\"lnkActivateRuleVariation\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;text-color: gray;\" disabled="disabled">Activate</span>'; }
            }
            //				else {
            //					var lnkRemoveId = "lnkRemoveRuleVariation_" + args.Data.RoutingVariationId;
            //					updateColHtml = (args.Data.IsActive != true && args.Data.IsActive != false) ? '&nbsp;&nbsp;' + '<span id=\"' + lnkRemoveId + '\" class=\"menucolor\" onClick=\"RemoveRuleVariation(' + args.Data.RoutingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Remove</span><br /><br /><br />' : "";
            //				}
        }
        $(args.Element)[0].innerHTML = updateColHtml;
    }
    if (args.Column.Name == "IsParent") {
        $(args.Element)[0].innerHTML = args.Data.IsParent;
        if (isParentVariation) {
            if (args.Data.IsActive == null || $.trim(args.Data.IsActive) == "") {
                if ((args.Data.IsSkipIntlMarketing == true || args.Data.IsSkipNtnlMarketing == true || args.Data.IsSkipLocalLabel == true)) {
                    $("#btnAddVariation").removeAttr("disabled");
                } else {
                    $("#btnAddVariation").attr("disabled", "disabled");
                }
            }
            else
                $("#btnAddVariation").removeAttr("disabled");
        }
    }
    //}
    //	else {
    //		//Add code for Existing variations
    //		debugger;
    //		alert('hai');
    //	}
}

function RoutingRuleGridLoad(sender, args) {
    $("#RoutingVariationRuleGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
}
//function ActionSuccess(sender, args) {
//	if (ruleVariationGridMode && sender._totalRecordsCount == 0) {
//		//		debugger;
//	//	$find("RoutingRuleVariationsGrid").jsonModeMgr._AddAction();
//		//$find("RoutingRuleVariationsGrid")._Edit._editMode = true;
////		sender._totalRecordsCount = 1;
////		RuleVariationRecordBind(sender, args)
//		//$("#RoutingRuleVariationsGrid_Table tr-first")
//	}
//}
//Load Rule Variations GRid - End

//Load Selected Rule Variations Grid in Edit mode - Start
function UpdateRoutingRule(sender) {//debugger;
    fnHideSuccessAndWarningMsgs();
    var ruleNo = sender.parentNode.parentNode.childNodes[1].textContent || sender.parentElement.parentElement.childNodes[1].innerText;
    var ruleName = sender.parentNode.parentNode.childNodes[2].textContent || sender.parentElement.parentElement.childNodes[2].innerText;

    ruleId = ruleNo.substring(5);//Get the RuleId by spliting the RuleNumber with space.
    ruleVariationGridMode = false;
    isCreateNewRule = false; isAddVariationClicked = false;
    //set selected rule Id and Name
    routingRuleId = ruleId;
    routingRuleName = ruleName;
    $("#ExportRuleNumber").val(routingRuleId);
    //Load Request Type Dropdown values
    getRequestTypeOptions();
    //Load the Variation Grid
    $("#divRoutingRuleVariations").innerHTML = "";
    var RuleVariationsGridObj = $find("RoutingRuleVariationsGrid");
    RuleVariationsGridObj.sendRefreshRequest();
    $("#RoutingRuleVariationsGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
    var ruleVariationHeader = '<h5 class="downArrow">' + ruleNo + '&nbsp;:&nbsp;' + ruleName + '</h5>';
    $("#ruleNoAndNameDv").html(ruleVariationHeader);
    $("#RoutingRuleVariationsDv").show();

    //debugger;
    var scrollheight = $(window).height() - ($(".headerLayout").height() + $(".footer").height() + 20);
    $("#routingRulesAndVariationsDv").height(scrollheight);
    document.getElementById("routingRulesAndVariationsDv").style.overflowY = "scroll";
    document.getElementById("routingRulesAndVariationsDv").style.overflowX = "hidden";
} //Load Selected Rule Variations Grid in Edit mode - End
//Activate / Deactivate Rule - starts
function ActivateRoutingRule(ruleNumber) {
    //var activateRule = 1; //Activate
    fnHideSuccessAndWarningMsgs();
    fnChangeRuleVariationStatus(ruleNumber, "Active", "Rule"); $("#mngRoutingRuleMainPanelDv").focus();
}

function DeactivateRoutingRule(ruleNumber) {
    //var activateRule = 2; //Deactivate
    fnHideSuccessAndWarningMsgs();
    fnChangeRuleVariationStatus(ruleNumber, "Deactive", "Rule"); $("#mngRoutingRuleMainPanelDv").focus();
} //Activate / Deactivate Rule - Ends

//Added for Rule Variations UI
//Set Saved Territory Collection
function setVariationTerritoryCollection(selectedVariationTerritoryList, TrritoryString) {
    if (selectedVariationTerritoryList != "") {
        var hdnFieldIds = createHiddenFieldIds(variationTerritoryType, selectedVariationId);
        var terrId = "";
        terrId = hdnFieldIds[0].replace('_', 'Inc_');
        document.getElementById(terrId).value = TrritoryString.IncludedCountries;
        terrId = hdnFieldIds[0].replace('_', 'Exc_');
        document.getElementById(terrId).value = TrritoryString.ExcludedCountries;
        if (selectedVariationTerritoryList != "[]") {
            document.getElementById(hdnFieldIds[0]).value = selectedVariationTerritoryList;
            //

            if (document.getElementById(hdnFieldIds[2]).style.display == "" || document.getElementById(hdnFieldIds[2]).style.display == "none" || document.getElementById(hdnFieldIds[2]).style.display == "block") {
                document.getElementById(hdnFieldIds[2]).style.display = "block";
                document.getElementById(hdnFieldIds[1]).style.display = "none";
            }
        }
        else {
            if (document.getElementById(hdnFieldIds[2]).style.display == "" || document.getElementById(hdnFieldIds[2]).style.display == "none" || document.getElementById(hdnFieldIds[2]).style.display == "block") {
                document.getElementById(hdnFieldIds[2]).style.display = "none";
                document.getElementById(hdnFieldIds[1]).style.display = "block";
            }
        }
    }
}
//Set Saved Company Collection
function setVariationCompanyCollection(selectedVariationCompanyList) {
    var hdnFieldIds = createHiddenFieldIds(variationCompanyType, selectedVariationId);
    var lnkCompanyId = variationlinkclass + "." + hdnFieldIds[2];
    document.getElementById(hdnFieldIds[0]).value = selectedVariationCompanyList;

    if (selectedVariationCompanyList != "") {
        if (document.getElementById(hdnFieldIds[2]).style.display == "" || document.getElementById(hdnFieldIds[2]).style.display == "none" || document.getElementById(hdnFieldIds[2]).style.display == "block") {
            document.getElementById(hdnFieldIds[2]).style.display = "block";
            document.getElementById(hdnFieldIds[1]).style.display = "none";
        }
        //if ($(lnkCompanyId).css('display') == 'none' || $(lnkCompanyId).css('display') == 'block') {
        //	$(lnkCompanyId).css({ "display": "block" });
        //	document.getElementById(hdnFieldIds[1]).style.display = "none";
        //}
    }
    else {
        if (document.getElementById(hdnFieldIds[2]).style.display == "" || document.getElementById(hdnFieldIds[2]).style.display == "none" || document.getElementById(hdnFieldIds[2]).style.display == "block") {
            document.getElementById(hdnFieldIds[2]).style.display = "none";
            document.getElementById(hdnFieldIds[1]).style.display = "block";
        }
        //if ($(lnkCompanyId).css('display') == 'none' || $(lnkCompanyId).css('display') == 'block') {
        //	$(lnkCompanyId).css({ "display": "none" });
        //	document.getElementById(hdnFieldIds[1]).style.display = "block";
        //}
    }
}
//Set Saved Talent Collection
function setVariationTalentCollection(variationTalentList) {
    var hdnFieldIds = createHiddenFieldIds("Talent", selectedVariationId);
    var lnkCompanyId = variationlinkclass + "." + hdnFieldIds[2];
    document.getElementById(hdnFieldIds[0]).value = variationTalentList;

    if (variationTalentList != "") {
        if (document.getElementById(hdnFieldIds[2]).style.display == "" || document.getElementById(hdnFieldIds[2]).style.display == "none" || document.getElementById(hdnFieldIds[2]).style.display == "block") {
            document.getElementById(hdnFieldIds[2]).style.display = "block";
            document.getElementById(hdnFieldIds[1]).style.display = "none";
        }
        //if ($(lnkCompanyId).css('display') == 'none' || $(lnkCompanyId).css('display') == 'block') {
        //	$(lnkCompanyId).css({ "display": "block" });
        //	document.getElementById(hdnFieldIds[1]).style.display = "none";
        //}
    }
    else {
        if (document.getElementById(hdnFieldIds[2]).style.display == "" || document.getElementById(hdnFieldIds[2]).style.display == "none" || document.getElementById(hdnFieldIds[2]).style.display == "block") {
            document.getElementById(hdnFieldIds[2]).style.display = "none";
            document.getElementById(hdnFieldIds[1]).style.display = "block";
        }
        //if ($(lnkCompanyId).css('display') == 'none' || $(lnkCompanyId).css('display') == 'block') {
        //	$(lnkCompanyId).css({ "display": "none" });
        //	document.getElementById(hdnFieldIds[1]).style.display = "block";
        //}
    }
}
function fnRequestTypeStatus(sender) {
    var variationId = sender.id.split("_"); var requestTypeClass = ".RequestType_" + variationId[1];
    var checkedState = sender.checked;
    if (sender.checked) {
        $.each(sender.parentNode.childNodes, function (i, child) { if (child.type == "checkbox") child.checked = false; });
        sender.checked = checkedState;
        if (sender.value == "Master") {
            document.getElementById(sender.parentNode.parentNode.cells[2].childNodes[0].id).disabled = 'disabled'; document.getElementById(sender.parentNode.parentNode.cells[2].childNodes[0].id).value = 0;
            //$(requestTypeClass).attr('disabled', 'disabled'); $(requestTypeClass).val("0");
        }
        else {
            document.getElementById(sender.parentNode.parentNode.cells[2].childNodes[0].id).removeAttribute("disabled", 0); document.getElementById(sender.parentNode.parentNode.cells[2].childNodes[0].id).value = 0;
            //$(requestTypeClass).removeAttr('disabled'); $(requestTypeClass).val("0");
        }
    }
    else {
        if (sender.value == "Master") {
            //	$(requestTypeClass).removeAttr('disabled'); $(requestTypeClass).val("0");
            document.getElementById(sender.parentNode.parentNode.cells[2].childNodes[0].id).removeAttribute("disabled", 0); document.getElementById(sender.parentNode.parentNode.cells[2].childNodes[0].id).value = 0;
        }
    }
}
function fnChkArtistCompileStatus(sender) {
    var checkedState = sender.checked;
    if (sender.checked) {
        $.each(sender.parentNode.childNodes, function (i, child) { if (child.type == "checkbox") child.checked = false; });
        sender.checked = checkedState;
    }
}

function fnReleaseTerritory(routingVariationId) {
    variationTerritoryType = "ReleaseTerritory";
    selectedVariationId = routingVariationId;
    var territories = document.getElementById(createVariationHiddenFieldId(variationTerritoryType, selectedVariationId)).value;

    fnOpenVariationTerritoryPopUp(routingVariationId, territories, mngRoutingMessages.JsMsg_Title_MngRelTerritory, "1");
}
function fnOwningTerritory(routingVariationId) {
    variationTerritoryType = "OwningTerritory";
    selectedVariationId = routingVariationId;
    var territories = document.getElementById(createVariationHiddenFieldId(variationTerritoryType, selectedVariationId)).value;

    fnOpenVariationTerritoryPopUp(routingVariationId, territories, mngRoutingMessages.JsMsg_Title_MngOwnTerritory, "2");
}
function fnRequestingTerritory(routingVariationId) {
    variationTerritoryType = "RequestingTerritory";
    selectedVariationId = routingVariationId;
    var territories = document.getElementById(createVariationHiddenFieldId(variationTerritoryType, selectedVariationId)).value;

    fnOpenVariationTerritoryPopUp(routingVariationId, territories, mngRoutingMessages.JsMsg_Title_MngReqTerritory, "3");
}
function fnOwningCompany(routingVariationId) {
    variationCompanyType = "OwningCompany";
    selectedVariationId = routingVariationId;
    var companies = document.getElementById(createVariationHiddenFieldId(variationCompanyType, selectedVariationId)).value;
    fnOpenRuleVariationCompanyPopUp(routingVariationId, companies, mngRoutingMessages.JsMsg_Title_MngOwnCompany);
}
function fnRequestingCompany(routingVariationId) {
    variationCompanyType = "RequestingCompany";
    selectedVariationId = routingVariationId;
    var companies = document.getElementById(createVariationHiddenFieldId(variationCompanyType, selectedVariationId)).value;
    fnOpenRuleVariationCompanyPopUp(routingVariationId, companies, mngRoutingMessages.JsMsg_Title_MngReqCompany);
}
function fnTalent(routingVariationId) {
    selectedVariationId = routingVariationId; variationTalent = "";
    variationTalent = document.getElementById(createVariationHiddenFieldId("Talent", selectedVariationId)).value;

    $('#artistFirstName').val("");
    $('#artistId').val(""); if (document.getElementById('spnSearchResultArtist') != null) { document.getElementById('spnSearchResultArtist').innerHTML = ''; }
    $("#warningmessageartist").hide();
    $('#searchArtistResult').jtable('destroy');
    $("#searchResultForArtist").hide();
    $("#SearchArtistPaging").hide();
    $("#trAddArtist").hide(); $("#trRemoveArtist").hide();
    if (variationTalent != "") {
        variationTalent = variationTalent.replace(/\,$/, "");

        $('#searchArtistResult').jtable({
            messages: ArtistSearchMessages,
            paging: true,
            pageSize: $('#ddlPagingArtist').val(),
            sorting: true,
            selecting: true,
            selectOnRowClick: false,
            multiselect: true,
            selectingCheckboxes: true,
            defaultSorting: 'FirstName ASC',
            actions: {
                listAction: '/GCS/Routing/SearchForArtist'
            },
            loadingRecords: function () {
                $('.jtable .jtable-no-data-row').hide();
            },
            recordsLoaded: function (event, data) {
                $('#searchArtistResult .jtable thead tr th:first').remove();
                $('<th class="jtable-command-column-header" style="width: 1%; ">' + mngRoutingTableColNames.JsJtabl_Select + '</th>').prependTo('#searchArtistResult .jtable thead tr:first');

                rowIndex = data.serverResponse.RowIndex;
                ArtisteSearch(rowIndex);

                showHidePageSections(data);
                $('.jtable .jtable-no-data-row').show();
                if (data.serverResponse.TotalRecordCount > 0) {
                    $("#trAddArtist").show(); $("#trRemoveArtist").show();
                    $("#searchResultForArtist").show();
                    $("#SearchArtistPaging").show();
                }
                else { $("#trAddArtist").hide(); $("#trRemoveArtist").hide(); }
            },
            fields: {
                ArtistName: {
                    title: mngRoutingTableColNames.ArtistName,//"Artist Name",
                    width: '25%',
                    display: function (name) {
                        return name.record.Name;
                    }
                },
                ArtistID: {
                    title: mngRoutingTableColNames.ArtistId,//"Artist Id",
                    width: '20%',
                    display: function (Id) {
                        return Id.record.Id;
                    }
                },
                AdditonalInfo: {
                    width: '30%',
                    title: mngRoutingTableColNames.JsJtabl_AdditionalInfo
                }
            },
            selectionChanged: function () {
                var $selectedRows = $('#searchArtistResult').jtable('selectedRows');
                selectedTalentJsonObj = '';
                if ($selectedRows.length > 0) {
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        selectedTalentJsonObj = selectedTalentJsonObj + record.Id + ",";
                        $("#warningmessageartist").hide();
                    });
                }
            }
        });

        $('#searchArtistResult').jtable('load', {
            existingArtist: variationTalent
        });
    }
    $("#manageArtistPopUp")
                .dialog({
                    autoOpen: true,
                    modal: true,
                    title: mngRoutingMessages.JsMsg_Title_MngArtist,//"Manage Artist",
                    show: 'clip',
                    hide: 'clip',
                    width: "65%",
                    resizable: false,
                    position: [(($(window).width() - (($(window).width() * 65) / 100)) / 2), 65]
                });
}
function DeactivateRuleVariation(routingVariationId) {
    fnHideSuccessAndWarningMsgs();
    fnChangeRuleVariationStatus(routingVariationId, "Deactive", "Variation"); $("#mngRoutingRuleMainPanelDv").focus();
}

function RemoveRuleVariation(routingVariationId) {
    fnHideSuccessAndWarningMsgs();
    var lnkId = 'lnkRemoveRuleVariation_' + routingVariationId;
    if (document.getElementById(lnkId).parentNode.parentNode.cells[18].innerText == "true") {//TO DO: Move the below string to resource file
        $("<div><br />" + mngRoutingMessages.JsMsg_ParentRuleDeleteConfirmation + " </div>").dialog({
            autoOpen: true,
            modal: true,
            resizable: false,
            buttons: {
                "Yes": function () {
                    fnChangeRuleVariationStatus(routingVariationId, "Remove", "Variation");
                    $(this).dialog("close");
                },
                "No": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
    else {
        fnChangeRuleVariationStatus(routingVariationId, "Remove", "Variation");
    } $("#mngRoutingRuleMainPanelDv").focus();
}

function ActivateRuleVariation(routingVariationId) {
    fnHideSuccessAndWarningMsgs();
    fnChangeRuleVariationStatus(routingVariationId, "Active", "Variation"); $("#mngRoutingRuleMainPanelDv").focus();
}

function fnChangeRuleVariationStatus(routingVariationId, statusType, ruleType) {
    $.post('/GCS/Routing/ChangeRuleVariationStatus', { Id: routingVariationId, statusType: statusType, objectType: ruleType }, function (data) {
        if (data == "") {
            //			$('#searchList').jtable('deleteRecord', {
            //				key: workgroupId,
            //				clientOnly: true,
            //				animationsEnabled: false
            //			});
            if (ruleType == "Variation") {
                if (statusType != "Remove") {
                    var lnkActiveId = 'lnkActivateRuleVariation_' + routingVariationId; var lnkDeactiveId = 'lnkDeactivateRuleVariation_' + routingVariationId; var lnkRemoveId = 'lnkRemoveRuleVariation_' + routingVariationId;
                    if ($('#' + lnkActiveId + '')[0].parentNode.nextSibling.innerHTML == "true") {
                        if (statusType.toLowerCase() == "active") {
                            fnShowSuccessMessage(mngRoutingMessages.JsMsg_RuleActivatedSuccess);
                        } else {
                            fnShowSuccessMessage(mngRoutingMessages.JsMsg_RuleDeactivatedSuccess);
                        }
                        $find("RoutingVariationRuleGrid").sendRefreshRequest();
                        $("#RoutingVariationRuleGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
                        $find("RoutingRuleVariationsGrid").sendRefreshRequest();
                        $("#RoutingRuleVariationsGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
                    }
                    else {
                        if (statusType.toLowerCase() == "active") {
                            if ($('#' + lnkRemoveId).length == 1) {
                                var newVrHtml = '&nbsp;&nbsp;' + '<span id=\"' + lnkDeactiveId + '\" class=\"menucolor\" onClick=\"DeactivateRuleVariation(' + routingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer;\">Deactivate</span>';
                                newVrHtml += '<span id=\"' + lnkActiveId + '\" class=\"menucolor\" onClick=\"ActivateRuleVariation(' + routingVariationId + ')\" style=\"text-decoration: underline; cursor: pointer; display:none;\">Activate</span>';
                                newVrHtml += '&nbsp;|&nbsp;<a href=\"#\" onClick=\"getRoutingVariationsAuditHistory(this)\" style=\"text-decoration: underline;\">History</a>';
                                $('#' + lnkActiveId + '')[0].parentNode.innerHTML = newVrHtml;
                            } else {
                                document.getElementById(lnkActiveId).style.display = "none";
                                document.getElementById(lnkDeactiveId).style.display = "";
                            } fnShowSuccessMessage(mngRoutingMessages.JsMsg_VariationActivatedSuccess);
                        }
                        if (statusType.toLowerCase() == "deactive") {
                            document.getElementById(lnkDeactiveId).style.display = "none";
                            document.getElementById(lnkActiveId).style.display = ""; fnShowSuccessMessage(mngRoutingMessages.JsMsg_VariationDeactivatedSuccess);
                        }
                    }
                }
                else {
                    var lnkRemoveId = 'lnkRemoveRuleVariation_' + routingVariationId;
                    if ($('#' + lnkRemoveId + '')[0].parentNode.nextSibling.innerHTML == "true") {
                        $("#RoutingRuleVariationsDv").hide(); document.getElementById("routingRulesAndVariationsDv").style.overflowY = "hidden";
                        fnShowSuccessMessage(mngRoutingMessages.JsMsg_RuleRemovedSuccess);
                        $find("RoutingVariationRuleGrid").sendRefreshRequest();
                        $("#RoutingVariationRuleGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
                    }
                    else {
                        fnShowSuccessMessage(mngRoutingMessages.JsMsg_VariationRemovedSuccess);
                        var ruleVariationsGridObj = $find("RoutingRuleVariationsGrid");
                        ruleVariationsGridObj.sendRefreshRequest();
                        $("#RoutingRuleVariationsGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
                    }
                }
            }
            if (ruleType == "Rule") {
                if (statusType == "Active") {
                    fnShowSuccessMessage(mngRoutingMessages.JsMsg_RuleActivatedSuccess);
                } else {
                    fnShowSuccessMessage(mngRoutingMessages.JsMsg_RuleDeactivatedSuccess);
                }
                $find("RoutingVariationRuleGrid").sendRefreshRequest();
                $("#RoutingVariationRuleGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
                $find("RoutingRuleVariationsGrid").sendRefreshRequest();
                $("#RoutingRuleVariationsGridwaiting_WaitingPopup_Image").attr('src', '/Gcs/Scripts/jtable/themes/standard/green/loading.gif');
            }
            //			fnShowSuccessMessage("Rule Has been Successfully&nbsp " + statusType + "");
        }
        else {
            fnShowErrorMessage(data);
            return false;
        }
    });
}

function fnOpenVariationTerritoryPopUp(routingVariationId, selectedTerritoryCollection, tittle, keyVal) {
    var territorykeyVal = routingVariationId + "," + keyVal;
    territoryList = selectedTerritoryCollection;
    territoryCollection.length = 0;
    var varId = (routingVariationId.toString().indexOf(".") == -1) ? routingVariationId : routingVariationId.toString().substring(0, routingVariationId.toString().length - 2);
    if (selectedTerritoryCollection.length == 0) {
        $.ajax({
            url: '/GCS/Routing/GetRoutingVariationTerritories',
            type: "POST",
            dataType: 'json',
            data: { 'variationID': varId, 'territoryType': variationTerritoryType.replace("Territory", "") },
            success: function (data) {
                if (data.length > 0) {
                    LoadTerritories(data, territorykeyVal, tittle);
                }
                else {
                    LoadTerritories(selectedTerritoryCollection, territorykeyVal, tittle);
                }
            }
        });
    }
    else {
        if (territoryCollection.length == 0) {
            territoryList = JSON.parse(selectedTerritoryCollection);
            hashTerritory = { 'Key': territorykeyVal, 'Value': territoryList };
            territoryCollection.push(hashTerritory);
            var includedCountries = JSLINQ(territoryCollection).Where(function (dict) { return dict.Key == territorykeyVal });
            if (includedCountries.items.length != 0) {
                var territoryValues = includedCountries.items[0].Value;
                var terrKey = includedCountries.items[0].Key;
                LoadTerritories(territoryValues, terrKey, tittle);
            }
        }
        else {
            var includedCountries = JSLINQ(territoryCollection).Where(function (dict) { return dict.Key == territorykeyVal });
            if (includedCountries.items.length != 0) {
                var territoryValues = includedCountries.items[0].Value;
                var terrKey = includedCountries.items[0].Key;
                LoadTerritories(territoryValues, terrKey, tittle);
            }
        }
    }
}

function fnOpenRuleVariationCompanyPopUp(routingVariationId, selectedCompanies, tittle) {
    var key = "";
    routingVariationCompanies = selectedCompanies;
    $('#btnRemove').hide();
    $('#btnSave').hide();
    $('#btnCancel').hide();

    $('#ddlCompanyPaging').val(10);

    $("#popupMgCompanyerrorMessage").html('');
    $("#popupMgCompanyerrorMessage").hide();
    var objDialog = $('#manageCompanyContainer')
    .dialog({
        autoOpen: true,
        modal: true,
        title: tittle,//'Manage Company',//TO DO: Move tittle to Resource file
        show: 'clip',
        hide: 'clip',
        width: 1000,
        resizable: false,
        beforeClose: function (event, ui) {
            if ($('#btnRemove').is(':visible')) {
                ResetForSearchCompany();
                return true;
            }
            else {
                $("#managecompany").show();
                $("#searchCompanyPopup").hide();
            }
        }
    });
    objDialog.load('/GCS/Workgroup/ManageCompany?companies=' + selectedCompanies + "&fromPage=" + key);
}
//Variation Territory PopUp
function LoadTerritories(territories, key, tittle) {
    manageTerritoryTitle = tittle;
    var objTerritorialPopup = $('<div id="Territory"></div>')
                    .html('<p>' + 'Loading' + '</p>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        title: manageTerritoryTitle, //TO DO: Move tittle to Resource file
                        show: 'clip',
                        hide: 'clip',
                        width: "98%",
                        height: 510,
                        minHeight: 510,
                        close: function () {
                            $(this).remove(); // ensures any form variables are reset.
                        }
                    });

    objTerritorialPopup.html('<p>' + 'Loading' + '</p>');
    var territory = JSON.stringify(territories);
    //Load partial view
    objTerritorialPopup.load(
        $.ajax({
            url: '/GCS/Territory/GetTerritories/' + key,
            type: 'POST',
            dataType: 'html',
            async: true,
            data: territory,
            cache: false,
            success: function (data) {
                if (data == 'error') {
                    objTerritorialPopup.html('<p>' + 'Error' + '</p>');
                }
                else {
                    objTerritorialPopup.html(data);
                }
            },
            contentType: 'application/json; charset=utf-8'
        }));
    objTerritorialPopup.dialog('option', { title: manageTerritoryTitle });
    //Open Dialog
    objTerritorialPopup.dialog('open');
}

//function fnOpenRuleVariationArtistPopUp(routingVariationId, selectedTalent, tittle) {
//	$('#ArtistPopup').dialog('close');
//	$('#ArtistPopup').dialog({
//		autoOpen: true,
//		width: 1000,
//		resizable: false,
//		title: 'Manage Artist',
//		modal: true//,
////		beforeClose: function (event, ui) {
////			$('#hdnrowId').empty();
////			$('#ArtistResourcePopup').remove();
////			$('#ArtistResourcePopup').dialog('close');
////			$('#btnManageArtist').removeClass('input-validation-error');
////			return true;
////		}
//	});
//	var values = { "existingArtist": (selectedTalent != null && selectedTalent != "") ? selectedTalent : " " }

//	$('#ArtistPopup').load('/GCS/Artist/SearchForArtist', values);
//	$('#tblExistingArtist').show();
//	var dialogue = $('.ui-dialog');
//	dialogue.animate({ top: "30px" }, 0);
//}
//

function fnCreateRulePopUp() {
    $("#txtNewRuleName").val(""); $('#txtNewRuleName').removeClass('input-validation-error'); $('#newRoutingRuleErrorMsg').hide(); $("#newRoutingRuleErrorMsg").html("");
    $("#createRulePopUp")
        .dialog({
            autoOpen: true,
            modal: true,
            title: mngRoutingMessages.JsMsg_Title_CreateRule,//"Create Rule",
            show: 'clip',
            hide: 'clip',
            width: "40%",
            resizable: false,
            position: [(($(window).width() - (($(window).width() * 50) / 100)) / 2), 50]
        });
}
//Added for Common Functions
function createVariationHiddenFieldId(fieldNameText, id) {
    var territoryFieldId = "hdn" + fieldNameText + "_" + id;
    return territoryFieldId;
}
function createHiddenFieldIds(fieldNameText, id) {
    var hiddenFieldIds = "hdn" + fieldNameText + "_" + id + "," + "btn" + fieldNameText + "_" + id + "," + "lnk" + fieldNameText + "_" + id;
    return hiddenFieldIds.split(",");
}
//Get RequestType Dropdown List Options
function getRequestTypeOptions() {
    $.ajax({
        url: '/GCS/Routing/GetRequestTypeLookUp',
        type: "GET",
        dataType: 'json',
        data: {},
        success: function (data) {
            if (data != "") {
                var requestTypeOption = "<option value='0'>Select</option>";
                $.each(data, function () {
                    requestTypeOption += '<option value="' + this.Key + '">' + this.Value + '</option>';
                });
                if (requestTypeOption != "")
                    variationRequestTypeOptionsHtml = requestTypeOption;
            }
        }
    });
}

//Create Variations Json Obj
function fnSaveVariationsJson(variationsContantTable, isSaveAsClick) {
    var ruleDetails = {};//Rule Details
    var ruleVariationDetails = {};//Variation Details
    var ruleVariationsList = [];//List of Variations
    var routingRuleVariationDetails = {};//Rule and List of Variations Details
    //
    var projectTypes = { None: "NA", Master: "Master", RegularProj: "Regular" };
    var companyTypes = { Requesting: "Requesting", Owning: "Owning" };
    var territoryTypes = { Release: "Release", Owning: "Owning", Requesting: "Requesting" };
    //
    ruleDetails = (isSaveAsClick || isCreateNewRule) ? { 'RuleName': routingRuleName } : { 'RuleNumber': routingRuleId, 'RuleName': routingRuleName };

    $.each(variationsContantTable, function (index, ruleVariationRow) {
        var variationDetails = {}; //variation Details
        var territoryDetails = {}; //Rule Details
        var territoryList = []; //List of Variations
        var companyDetails = {}; var companyInfo = {}; //Rule Details
        var companyList = []; //List of Variations
        var artistDetails = {}; //Rule Details
        var artistList = []; //List of Variations
        //Variation Variables
        var projType = null;
        var reqType = null;
        var isSensitiveArt = "";
        var isActiveArtist = "";
        var isMultiArt = "";
        var isCompile = "";
        var isSkipIntlMarket = "";
        var isSkipNtnlMarket = "";
        var isSkipLocLabel = "";
        var comments = "";
        var isActive = "";
        var isParent = "";
        var routingVariationId = "";

        var RelIncludTerr = "";
        var RelExcludTerr = "";
        var OwnIncludTerr = "";
        var OwnExcludTerr = "";
        var ReqIncludTerr = "";
        var ReqExcludTerr = "";

        projType = (ruleVariationRow.cells[1].childNodes[0].checked) ? projectTypes.Master : (ruleVariationRow.cells[1].childNodes[3].checked) ? projectTypes.RegularProj : projectTypes.None;
        if (ruleVariationRow.cells[2].childNodes[0].value != 0)
            reqType = ruleVariationRow.cells[2].childNodes[0].value;

        isSensitiveArt = (ruleVariationRow.cells[3].childNodes[0].checked) ? true : (ruleVariationRow.cells[3].childNodes[3].checked) ? false : "";
        isActiveArtist = (ruleVariationRow.cells[4].childNodes[0].checked) ? true : (ruleVariationRow.cells[4].childNodes[3].checked) ? false : "";
        isMultiArt = (ruleVariationRow.cells[5].childNodes[0].checked) ? true : (ruleVariationRow.cells[5].childNodes[3].checked) ? false : "";
        isCompile = (ruleVariationRow.cells[6].childNodes[0].checked) ? true : (ruleVariationRow.cells[6].childNodes[3].checked) ? false : "";

        if (ruleVariationRow.cells[7].childNodes[0].value.length > 0) {
            $.each($.parseJSON(ruleVariationRow.cells[7].childNodes[0].value), function (i, territoryDisplayObj) {
                territoryDetails = { 'TerritoryType': territoryTypes.Release, 'TerritorialDisplay': territoryDisplayObj, 'IsModified': true };
                territoryList.push(territoryDetails);
            });
        }
        if (ruleVariationRow.cells[7].childNodes[1].value != "") RelIncludTerr = ruleVariationRow.cells[7].childNodes[1].value;
        if (ruleVariationRow.cells[7].childNodes[2].value != "") RelExcludTerr = ruleVariationRow.cells[7].childNodes[2].value;

        if (ruleVariationRow.cells[8].childNodes[0].value.length > 0) {
            $.each($.parseJSON(ruleVariationRow.cells[8].childNodes[0].value), function (i, territoryDisplayObj) {
                territoryDetails = { 'TerritoryType': territoryTypes.Owning, 'TerritorialDisplay': territoryDisplayObj, 'IsModified': true };
                territoryList.push(territoryDetails);
            });
        }
        if (ruleVariationRow.cells[8].childNodes[1].value != "") OwnIncludTerr = ruleVariationRow.cells[8].childNodes[1].value;
        if (ruleVariationRow.cells[8].childNodes[2].value != "") OwnExcludTerr = ruleVariationRow.cells[8].childNodes[2].value;
        //Owning Companies
        if (ruleVariationRow.cells[9].childNodes[0].value.length > 0) {
            var companyIds = ruleVariationRow.cells[9].childNodes[0].value.split(',');
            $.each(companyIds, function (i, id) {
                if (id != "") {
                    companyInfo = { 'Id': id };
                    companyDetails = { 'CompanyType': companyTypes.Owning, 'CompanyInfo': companyInfo };
                    companyList.push(companyDetails);
                }
            });
        }
        if (ruleVariationRow.cells[10].childNodes[0].value.length > 0) {
            $.each($.parseJSON(ruleVariationRow.cells[10].childNodes[0].value), function (i, territoryDisplayObj) {
                territoryDetails = { 'TerritoryType': territoryTypes.Requesting, 'TerritorialDisplay': territoryDisplayObj, 'IsModified': true };
                territoryList.push(territoryDetails);
            });
        }
        if (ruleVariationRow.cells[10].childNodes[1].value != "") ReqIncludTerr = ruleVariationRow.cells[10].childNodes[1].value;
        if (ruleVariationRow.cells[10].childNodes[2].value != "") ReqExcludTerr = ruleVariationRow.cells[10].childNodes[2].value;
        //Requesting Companies
        if (ruleVariationRow.cells[11].childNodes[0].value.length > 0) {
            var companyIds = ruleVariationRow.cells[11].childNodes[0].value.split(',');
            $.each(companyIds, function (i, id) {
                if (id != "") {
                    companyInfo = { 'Id': id };
                    companyDetails = { 'CompanyType': companyTypes.Requesting, 'CompanyInfo': companyInfo };
                    companyList.push(companyDetails);
                }
            });
        }
        //Talent
        if (ruleVariationRow.cells[12].childNodes[0].value.length > 0) {
            var talentIds = ruleVariationRow.cells[12].childNodes[0].value.split(',');
            $.each(talentIds, function (i, id) {
                if (id != "") {
                    artistDetails = { 'Id': id };
                    artistList.push(artistDetails);
                }
            });
        }

        isParent = ruleVariationRow.cells[18].childNodes[0].data.indexOf("true") != -1 ? true : false;
        isSkipIntlMarket = (isParent) ? ((ruleVariationRow.cells[13].childNodes[0].checked) ? true : "") : ((ruleVariationRow.cells[13].childNodes[0].checked) ? false : "");
        isSkipNtnlMarket = (isParent) ? ((ruleVariationRow.cells[14].childNodes[0].checked) ? true : "") : ((ruleVariationRow.cells[14].childNodes[0].checked) ? false : "");
        isSkipLocLabel = (isParent) ? ((ruleVariationRow.cells[15].childNodes[0].checked) ? true : "") : ((ruleVariationRow.cells[15].childNodes[0].checked) ? false : "");
        if (ruleVariationRow.cells[16].childNodes[0].value != "")
            comments = ruleVariationRow.cells[16].childNodes[0].value;
        //isActive = (ruleVariationRow.cells[17].textContent || ruleVariationRow.cells[17].innerText); //What value to send

        if (isSaveAsClick || isCreateNewRule) {
            variationDetails = {
                'ProjectType': projType, 'RvRequestType': reqType, 'IsSensitiveArtist': isSensitiveArt, 'IsMultiArtist': isMultiArt,
                'IsCompilation': isCompile, 'IsSkipIntlMarketing': isSkipIntlMarket, 'IsSkipNtnlMarketing': isSkipNtnlMarket, 'IsSkipLocalLabel': isSkipLocLabel,
                'Comments': comments, 'IsActiveArtist': isActiveArtist, 'IsParent': isParent, 'IncludedReleaseTerritories': RelIncludTerr, 'ExcludedReleaseTerritories': RelExcludTerr,
                'IncludedOwningTerritories': OwnIncludTerr, 'ExcludedOwningTerritories': OwnExcludTerr, 'IncludedRequestingTerritories': ReqIncludTerr, 'ExcludedRequestingTerritories': ReqExcludTerr
            };
        }
        else {
            routingVariationId = (ruleVariationRow.cells[19].textContent || ruleVariationRow.cells[19].innerText);

            variationDetails = {
                'ProjectType': projType, 'RvRequestType': reqType, 'IsSensitiveArtist': isSensitiveArt, 'IsMultiArtist': isMultiArt,
                'IsCompilation': isCompile, 'IsSkipIntlMarketing': isSkipIntlMarket, 'IsSkipNtnlMarketing': isSkipNtnlMarket, 'IsSkipLocalLabel': isSkipLocLabel,
                'Comments': comments, 'IsActiveArtist': isActiveArtist, 'IsParent': isParent, 'IncludedReleaseTerritories': RelIncludTerr, 'ExcludedReleaseTerritories': RelExcludTerr,
                'IncludedOwningTerritories': OwnIncludTerr, 'ExcludedOwningTerritories': OwnExcludTerr, 'IncludedRequestingTerritories': ReqIncludTerr, 'ExcludedRequestingTerritories': ReqExcludTerr, 'RoutingVariationId': routingVariationId
            };
        }
        //Variation Details
        ruleVariationDetails = { 'Territories': territoryList, 'Companies': companyList, 'ArtistList': artistList, 'RoutingVariation': variationDetails };
        ruleVariationsList.push(ruleVariationDetails);
    });
    //Serialize the Data
    if (ruleDetails.length != 0 && ruleVariationsList.length != 0) {
        routingRuleVariationDetails = { 'RoutingRule': ruleDetails, 'RoutingVariationSaveInfo': ruleVariationsList };
        return JSON.stringify(routingRuleVariationDetails);
    }
}

$(document).ready(function (e) {
    //$('input:checkbox').click(function () {
    //	checkstate = (this.checked == false) ? false : true;
    //});
    //
    $('#btnCancelArtist').click(function (e) {
        //	e.preventDefault();
        //$('#manageArtistPopUp').remove();
        $('#manageArtistPopUp').dialog('close');
        //$('#btnManageArtist').removeClass('input-validation-error');
    });

    //if (event.keyCode == 13) { $("searchForArtist").focus(); };
    //
    $('#resetArtistButton').click(function (e) {
        //e.preventDefault();
        $('#artistFirstName').val('');
        $('#artistId').val('');
        $("#warningmessageartist").hide();
    });
    //
    $('#ddlPagingArtist').change(function () {
        hideArtistResult = false;
        //LoadSearchJtable();
        $('#searchArtistResult').jtable('load', {
            "ArtistName": $('#artistFirstName').val(),
            "ArtistID": $('#artistId').val(),
            "PageSize": $('#ddlPagingArtist').val(), //TO DO: Change to dropdown value
            "UserName": 'vivek_gupta',
            "RowIndex": rowIndex
        });
    });
    //search
    $('#searchForArtist').click(function (e) {
        //		debugger;
        rowIndex = -1;
        pagingCount = $('#ddlPagingArtist').val();
        //		$('#tblExistingArtist').hide();
        //		var searchCriteria = {
        //			"ArtistName": $('#artistFirstName').val(),
        //			"ArtistId": $('#artistId').val(),
        //			"jtStartIndex": 0,
        //			"PageSize": pagingCount,
        //			"RowIndex": '-1',
        //			"UserName": 'vivek_gupta'
        //		};
        $('#searchForArtist').focus();
        e.preventDefault();
        var artistFirstName = $('#artistFirstName').val();
        var artistid = $('#artistId').val();
        var searchlist = '';
        if (artistFirstName != '') { searchlist = searchlist + artistFirstName + '/'; }
        if (artistid != 0) { searchlist = searchlist + artistid + '/'; }
        searchlist = searchlist.substring(0, searchlist.length - 1)
        document.getElementById('spnSearchResultArtist').innerHTML = searchlist;

        if ((artistFirstName == '') && (artistid == '')) {
            $("#warningmessageartist").text(atleastsearchcriteria);
            $('#warningmessageartist').addClass('warning');
            $("#warningmessageartist").show();
            return false;
        }
        else {
            $('#searchArtistResult').jtable({
                messages: ArtistSearchMessages,
                paging: true,
                pageSize: pagingCount,
                sorting: true,
                selecting: true,
                selectOnRowClick: false,
                multiselect: true,
                selectingCheckboxes: true,
                defaultSorting: 'FirstName ASC',
                actions: {
                    listAction: '/GCS/Routing/SelectMultiArtist'
                },
                loadingRecords: function () {
                    $('.jtable .jtable-no-data-row').hide();
                },
                recordsLoaded: function (event, data) {
                    $('#searchArtistResult .jtable thead tr th:first').remove();
                    $('<th class="jtable-command-column-header" style="width: 1%; ">' + mngRoutingTableColNames.JsJtabl_Select + '</th>').prependTo('#searchArtistResult .jtable thead tr:first');
                    //					debugger;
                    rowIndex = data.serverResponse.RowIndex;
                    ArtisteSearch(rowIndex);
                    showHidePageSections(data);
                    $('.jtable .jtable-no-data-row').show();

                    if (data.serverResponse.TotalRecordCount > 0) {
                        $("#trAddArtist").show();
                        $("#searchResultForArtist").show();
                        $("#SearchArtistPaging").show();
                    }
                    else { $("#trAddArtist").hide(); }
                },
                fields: {
                    ArtistName: {
                        title: mngRoutingTableColNames.ArtistName,//"Artist Name",
                        width: '25%',
                        display: function (name) {
                            var name = name.record.FirstName + " " + name.record.LastName;
                            return name;
                        }
                    },
                    ArtistID: {
                        title: mngRoutingTableColNames.ArtistId,//"Artist Id",
                        key: true,
                        width: '20%',
                        display: function (Id) {
                            var Id = Id.record.Info.Id;
                            return Id;
                        }
                    },
                    AdditonalInfo: {
                        width: '30%',
                        title: mngRoutingTableColNames.JsJtabl_AdditionalInfo,//searchArtistMessages.addInfo
                    }
                },
                selectionChanged: function () {
                    var $selectedRows = $('#searchArtistResult').jtable('selectedRows');
                    selectedTalentJsonObj = '';
                    if ($selectedRows.length > 0) {
                        $selectedRows.each(function () {
                            var record = $(this).data('record');
                            selectedTalentJsonObj = selectedTalentJsonObj + record.Info.Id + ",";
                            $("#warningmessageartist").hide();
                        });
                    }
                }
            });

            $('#searchArtistResult').jtable('load', {
                ArtistName: artistFirstName,
                ArtistID: artistid,
                PageSize: pagingCount,
                RowIndex: '-1',
                UserName: 'vivek_gupta'
            });
            $('#searchArtistResult').show();
            //			var grid = document.getElementById('ArtistPaging');
            //			grid.style.display = 'block';
            $("#warningmessageartist").hide();
        }
        return false;
    });
    //Save
    $('#btnAddArtist').click(function (event) {
        if (selectedTalentJsonObj != "") {
            setVariationTalentCollection(selectedTalentJsonObj);
            $('#manageArtistPopUp').dialog('close');
            $('#btnManageArtist').removeClass('input-validation-error');
        }
        else {
            if ($('#searchArtistResult').find(".jtable tbody .jtable-no-data-row").length != 1) {
                $("#warningmessageartist").text(mngRoutingMessages.JsMsg_SelectOneArtist);
                $('#warningmessageartist').addClass('warning');
                $("#warningmessageartist").show();
                return false;
            } else { $('#manageArtistPopUp').dialog('close'); setVariationTalentCollection(selectedTalentJsonObj); }
        }
    });
    $('#btnRemoveArtist').click(function (e) {
        var savedTalents = selectedTalentJsonObj;
        $.each($('#searchArtistResult').find(".jtable tbody").children(), function (index, deletedRow) {
            var talentId = deletedRow.children[2].innerText || deletedRow.children[2].textContent;
            if (savedTalents.indexOf(talentId) != -1) {
                deletedRow.removeNode(true);
                savedTalents = savedTalents.replace(talentId + ',', '');
            }
        });
        selectedTalentJsonObj = savedTalents;
        document.getElementById(createVariationHiddenFieldId("Talent", selectedVariationId)).value.replace(selectedTalentJsonObj, "");
        if (selectedTalentJsonObj.length <= 0 && $('#searchArtistResult').find(".jtable tbody").children().length == 0) { $('<tr class="jtable-no-data-row"><td colSpan="4">No data available!</td></tr>').appendTo($('#searchArtistResult').find(".jtable tbody")); }
    });
});

//Reset the value of rowindex
function ArtisteSearch(rowIndex) {
    $('#searchArtistResult').jtable('reset', {
        "ArtistName": $('#artistFirstName').val(),
        "ArtistID": $('#artistId').val(),
        "PageSize": $('#ddlPagingArtist').val(),
        "UserName": 'vivek_gupta',
        "RowIndex": rowIndex
    });
}

function showHidePageSections(data) {
    if (data.serverResponse.TotalRecordCount > 0) {
        $('#btnAddArtist').show();
        //$('#tblExistingArtist').hide();
        $("#trShowPagingArtist").show();
    }
}
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
//var searchArtistMessages = {
//	talentTitle: 'Talent Id',
//	firstName: 'First Name',
//	lastName: 'Last Name/Group',
//	displayName: 'Display Name',
//	title: 'Title',
//	titlePrefix: 'Prefix',
//	rolesPerformed: 'Roles Performed',
//	dataAdmnCmpny: 'R2 Data Admin Company',
//	aliasIndicator: 'Alias Indicator',
//	addInfo: 'Additional Info'
//};

function fnHideSuccessAndWarningMsgs() {
    $("#routingVariationSuccessMessage").hide();
    $("#routingVariationSuccessMessage").html('');
    $("#routingVariationErrorMessage").hide();
    $("#routingVariationErrorMessage").html('');
}
function fnShowSuccessMessage(message) {
    $("#routingVariationSuccessMessage").show();
    $("#routingVariationSuccessMessage").html(message);
}
function fnShowErrorMessage(message) {
    $("#routingVariationErrorMessage").show();
    $("#routingVariationErrorMessage").html(message);
}

function getRoutingVariationsAuditHistory(sender) {
    var SelectedItemIds = []; var AuditTypeId = sender.parentNode.parentNode.cells[19].childNodes[0].nodeValue; var isParentrule = sender.parentNode.parentNode.cells[18].childNodes[0].nodeValue;
    var displayTitle = (isParentrule == "false") ? routingRuleId + ': ' + routingRuleName + " - Variation" : routingRuleId + ': ' + routingRuleName;
    SelectedItemIds.push(AuditTypeId);
    displayWGAuditTrail(AuditObjectType.RoutingVariationAuditHistory, SelectedItemIds, displayTitle, 0, true);
    return false;
}