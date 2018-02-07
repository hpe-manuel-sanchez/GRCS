using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMGI.GRCS.UI.Controllers;

namespace DynamicControlsCreation.ViewModels
{
  public class DefaultControlsViewModel
  {
    public List<ControlViewModel> Controls { get; set; }
    public IEnumerable<SelectListItem> Dropdownlist { get; set; }
    public DateTime? FromDt { get; set; }
    public DateTime? ToDt { get; set; }
    public Dictionary<string, ControlKeys> VisibleControls { get; set; }
    public DefaultControlsViewModel(string screenName)
    {
      Controls = new List<ControlViewModel>();
      VisibleControls = new Dictionary<string, ControlKeys>();
      VisibleControls = GetControlObject(screenName);
    }
      
    private Dictionary<string, ControlKeys> GetControlObject(string screenName)
    {
        Dictionary<string, ControlKeys> objControls = new Dictionary<string, ControlKeys>();
        switch (screenName)
        {
            case "ResourcesBasicSearch":
                objControls = ResourcesBasicSearch();
                break;
            case "ResourcesReleaseParameters":
                objControls = ResourcesReleaseParameters();
                break;
            case "TracksBasicSearch":
                objControls = TracksBasicSearch();
                break;
            case "TracksSearchReleaseParameters":
                objControls = TracksSearchReleaseParameters();
                break;
            case "ResourcesAndTracksBasicSearch":
                objControls = ResourcesAndTracksBasicSearch();
                break;
            case "ResourcesAndTracksSearchReleaseParameters":
                objControls = ResourcesAndTracksSearchReleaseParameters();
                break;
            case "Releases":
                objControls = Releases();
                break;
            case "DigitalRestrictionsResourcesBasicSearch":
                objControls = DigitalRestrictionsResourcesBasicSearch();
                break;
            case "DigitalRestrictionsResourcesReleaseParameters":
                objControls = DigitalRestrictionsResourcesReleaseParameters();
                break;
            case "DigitalRestrictionsTracksBasicSearch":
                objControls = DigitalRestrictionsTracksBasicSearch();
                break;
            case "DigitalRestrictionsTracksSearchReleaseParameters":
                objControls = DigitalRestrictionsTracksSearchReleaseParameters();
                break;
            case "DigitalRestrictionsResourcesAndTracksBasicSearch":
                objControls = DigitalRestrictionsResourcesAndTracksBasicSearch();
                break;
            case "DigitalRestrictionsResourcesAndTracksSearchReleaseParameters":
                objControls = DigitalRestrictionsResourcesAndTracksSearchReleaseParameters();
                break;
            case "DigitalRestrictionsReleases":
                objControls = DigitalRestrictionsReleases();
                break;
            case "SecondaryExploitationBasicSearch":
                objControls = SecondaryExploitationBasicSearch();
                break;
            case "SecondaryExploitationReleaseParameters":
                objControls = SecondaryExploitationReleaseParameters();
                break;
            case "PreClearedBasicSearch":
                objControls = PreClearedBasicSearch();
                break;
            case "PreClearedReleaseParameters":
                objControls = PreClearedReleaseParameters();
                break;
        }
        return objControls;
    }

    private Dictionary<string, ControlKeys> ResourcesBasicSearch()//Ref. WF 29.1
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "pc_Left_ISRC", ChildClass1 = "cc1_Left_lblISRC", ChildClass2 = "cc2_Left_txtISRC", ChildClass3 = "cc3_Left_btnISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "pc_Right_AritstName", ChildClass1 = "cc1_Right_lblArtistName", ChildClass2 = "cc2_Right_txtArtistName", ChildClass3 = "cc3_Right_chkArtistName",ChildClass4="cc4_Right_lblExactSearch" });        
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "pc_Left_ResourceTitle",ChildClass1="cc1_Left_lblResourceTitle",ChildClass2="cc2_Left_txtResourceTitle" });        
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "pc_Right_LinkedContractName",ChildClass1="cc1_Right_lblLinkedContractName",ChildClass2="cc2_Right_txtLinkedContractName",ChildClass3="cc2_Right_btnLinkedContractName" });           
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "pc_Left_ResourceType",ChildClass1="cc1_Left_lblResourceType",ChildClass2="cc2_Left_ddlResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "pc_Right_RepertoireMultipleContracts", ChildClass1 = "cc1_Right_chkRepertoireMultipleContracts", ChildClass2 = "cc2_Right_lblRepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "pc_Left_TerritorialRights", ChildClass1 = "cc1_Left_lblTerritorialRights", ChildClass2 = "cc2_Left_txtTerritorialRights",ChildClass3="cc3_Left_lnkSelRemCountries" });        
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "pc_Right_RightsReviewStatus",ChildClass1="cc1_Right_lblRightsReviewStatus",ChildClass2="cc2_Right_ddlRightsReviewStatus" });        
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "pc_Left_RightsPeriod",ChildClass1="cc1_Left_lblRightsPeriod",ChildClass2="cc2_Left_ddlRightsPeriod" });       
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "pc_Right_LostRightsIndicator",ChildClass1="cc1_Right_lblLostRightsIndicator",ChildClass2="cc2_ddlLostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "pc_Left_PhysicialExploitationRights", ChildClass1 = "cc1_Left_lblPhysicialExploitationRights", ChildClass2 = "cc2_Left_ddlPhysicialExploitationRights" });        
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "pc_Right_LostRightsReason",ChildClass1="cc1_Right_lblLostRightsReason",ChildClass2="cc2_Right_ddlLostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "pc_Left_DigitalExploitationRights", ChildClass1 = "cc1_Left_lblDigitalExploitationRights", ChildClass2 = "cc2_Left_ddlDigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "pc_Right_DigitalUnbundlingAllowed", ChildClass1 = "cc1_Right_lblDigitalUnbundlingAllowed", ChildClass2 = "cc2_Right_ddlDigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "pc_Left_MobileExploitationRights", ChildClass1 = "cc1_Left_lblMobileExploitationRights", ChildClass2 = "cc2_Left_ddlMobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "pc_Right_PPBRevenueClaim", ChildClass1 = "cc1_Right_lblPPBRevenueClaim", ChildClass2 = "cc2_Right_ddlPPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "pc_Left_ActiveforMarketing", ChildClass1 = "cc1_Left_ActiveforMarketing", ChildClass2 = "cc2_Left_ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "pc_Right_LostRightsDate", ChildClass1 = "cc1_Right_lblLostRightsDate", ChildClass2 = "cc2_Right_lblFromLostRightsDate", ChildClass3 = "cc2_Right_fromdtLostRightsDate", ChildClass4 = "cc2_Right_lblToLostRightsDate",ChildClass5 = "cc3_Right_todtLostRightsDate" });        
        
        return objScreen1;
    }
    private Dictionary<string, ControlKeys> ResourcesReleaseParameters()//Ref. WF 29.2
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> TracksBasicSearch()//Ref. WF 29.3
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "pc_Left_ISRC", ChildClass1 = "cc1_Left_lblISRC", ChildClass2 = "cc2_Left_txtISRC", ChildClass3 = "cc3_Left_btnISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "pc_Right_AritstName", ChildClass1 = "cc1_Right_lblArtistName", ChildClass2 = "cc2_Right_txtArtistName", ChildClass3 = "cc3_Right_chkArtistName", ChildClass4 = "cc4_Right_lblExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "pc_Left_ResourceTitle", ChildClass1 = "cc1_Left_lblResourceTitle", ChildClass2 = "cc2_Left_txtResourceTitle" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "pc_Right_ResourceType", ChildClass1 = "cc1_Right_lblResourceType", ChildClass2 = "cc2_Right_ddlResourceType" });
        
        
        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "pc_Left_TerritorialRights", ChildClass1 = "cc1_Left_lblTerritorialRights", ChildClass2 = "cc2_Left_txtTerritorialRights", ChildClass3 = "cc3_Left_lnkSelRemCountries" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "pc_Right_RightsReviewStatus", ChildClass1 = "cc1_Right_lblRightsReviewStatus", ChildClass2 = "cc2_Right_ddlRightsReviewStatus" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "pc_Left_DigitalExploitationRights", ChildClass1 = "cc1_Left_lblDigitalExploitationRights", ChildClass2 = "cc2_Left_ddlDigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "pc_Right_DigitalUnbundlingAllowed", ChildClass1 = "cc1_Right_lblDigitalUnbundlingAllowed", ChildClass2 = "cc2_Right_ddlDigitalUnbundlingAllowed" });
        
        return objScreen1;
    }
    private Dictionary<string, ControlKeys> TracksSearchReleaseParameters()//Ref. WF 29.4
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> ResourcesAndTracksBasicSearch()//Ref. WF 29.5
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "pc_Left_ISRC", ChildClass1 = "cc1_Left_lblISRC", ChildClass2 = "cc2_Left_txtISRC", ChildClass3 = "cc3_Left_btnISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "pc_Right_AritstName", ChildClass1 = "cc1_Right_lblArtistName", ChildClass2 = "cc2_Right_txtArtistName", ChildClass3 = "cc3_Right_chkArtistName", ChildClass4 = "cc4_Right_lblExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "pc_Left_ResourceTitle", ChildClass1 = "cc1_Left_lblResourceTitle", ChildClass2 = "cc2_Left_txtResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "pc_Right_LinkedContractName", ChildClass1 = "cc1_Right_lblLinkedContractName", ChildClass2 = "cc2_Right_txtLinkedContractName", ChildClass3 = "cc2_Right_btnLinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "pc_Left_ResourceType", ChildClass1 = "cc1_Left_lblResourceType", ChildClass2 = "cc2_Left_ddlResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "pc_Right_RepertoireMultipleContracts", ChildClass1 = "cc1_Right_chkRepertoireMultipleContracts", ChildClass2 = "cc2_Right_lblRepertoireMultipleContracts" });
        
        
        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "pc_Left_TerritorialRights", ChildClass1 = "cc1_Left_lblTerritorialRights", ChildClass2 = "cc2_Left_txtTerritorialRights", ChildClass3 = "cc3_Left_lnkSelRemCountries" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "pc_Right_RightsReviewStatus", ChildClass1 = "cc1_Right_lblRightsReviewStatus", ChildClass2 = "cc2_Right_ddlRightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "pc_Left_RightsPeriod", ChildClass1 = "cc1_Left_lblRightsPeriod", ChildClass2 = "cc2_Left_ddlRightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "pc_Right_LostRightsIndicator", ChildClass1 = "cc1_Right_lblLostRightsIndicator", ChildClass2 = "cc2_ddlLostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "pc_Left_PhysicialExploitationRights", ChildClass1 = "cc1_Left_lblPhysicialExploitationRights", ChildClass2 = "cc2_Left_ddlPhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "pc_Right_LostRightsReason", ChildClass1 = "cc1_Right_lblLostRightsReason", ChildClass2 = "cc2_Right_ddlLostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "pc_Left_DigitalExploitationRights", ChildClass1 = "cc1_Left_lblDigitalExploitationRights", ChildClass2 = "cc2_Left_ddlDigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "pc_Right_DigitalUnbundlingAllowed", ChildClass1 = "cc1_Right_lblDigitalUnbundlingAllowed", ChildClass2 = "cc2_Right_ddlDigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "pc_Left_MobileExploitationRights", ChildClass1 = "cc1_Left_lblMobileExploitationRights", ChildClass2 = "cc2_Left_ddlMobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "pc_Right_PPBRevenueClaim", ChildClass1 = "cc1_Right_lblPPBRevenueClaim", ChildClass2 = "cc2_Right_ddlPPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "pc_Left_ActiveforMarketing", ChildClass1 = "cc1_Left_ActiveforMarketing", ChildClass2 = "cc2_Left_ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "pc_Right_LostRightsDate", ChildClass1 = "cc1_Right_lblLostRightsDate", ChildClass2 = "cc2_Right_lblFromLostRightsDate", ChildClass3 = "cc2_Right_fromdtLostRightsDate", ChildClass4 = "cc2_Right_lblToLostRightsDate", ChildClass5 = "cc3_Right_todtLostRightsDate" });        

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> ResourcesAndTracksSearchReleaseParameters()//Ref. WF 29.6
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> Releases()//Ref. WF 29.7
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("UPC", new ControlKeys() { ID = "UPC", ParentClass = "pc_Left_UPC", ChildClass1 = "cc1_Left_lblUPC", ChildClass2 = "cc2_Left_txtUPC", ChildClass3 = "cc3_Left_btnUPC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "pc_Right_AritstName", ChildClass1 = "cc1_Right_lblArtistName", ChildClass2 = "cc2_Right_txtArtistName", ChildClass3 = "cc3_Right_chkArtistName", ChildClass4 = "cc4_Right_lblExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "pc_Left_ResourceTitle", ChildClass1 = "cc1_Left_lblResourceTitle", ChildClass2 = "cc2_Left_txtResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "pc_Right_LinkedContractName", ChildClass1 = "cc1_Right_lblLinkedContractName", ChildClass2 = "cc2_Right_txtLinkedContractName", ChildClass3 = "cc2_Right_btnLinkedContractName" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "pc_Right_RepertoireMultipleContracts", ChildClass1 = "cc1_Right_chkRepertoireMultipleContracts", ChildClass2 = "cc2_Right_lblRepertoireMultipleContracts" });        

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "pc_Left_TerritorialRights", ChildClass1 = "cc1_Left_lblTerritorialRights", ChildClass2 = "cc2_Left_txtTerritorialRights", ChildClass3 = "cc3_Left_lnkSelRemCountries" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "pc_Left_RightsPeriod", ChildClass1 = "cc1_Left_lblRightsPeriod", ChildClass2 = "cc2_Left_ddlRightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "pc_Right_LostRightsIndicator", ChildClass1 = "cc1_Right_lblLostRightsIndicator", ChildClass2 = "cc2_ddlLostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "pc_Left_PhysicialExploitationRights", ChildClass1 = "cc1_Left_lblPhysicialExploitationRights", ChildClass2 = "cc2_Left_ddlPhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "pc_Right_LostRightsReason", ChildClass1 = "cc1_Right_lblLostRightsReason", ChildClass2 = "cc2_Right_ddlLostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "pc_Left_DigitalExploitationRights", ChildClass1 = "cc1_Left_lblDigitalExploitationRights", ChildClass2 = "cc2_Left_ddlDigitalExploitationRights" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "pc_Right_LostRightsDate", ChildClass1 = "cc1_Right_lblLostRightsDate", ChildClass2 = "cc2_Right_lblFromLostRightsDate", ChildClass3 = "cc2_Right_fromdtLostRightsDate", ChildClass4 = "cc2_Right_lblToLostRightsDate", ChildClass5 = "cc3_Right_todtLostRightsDate" });                

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> DigitalRestrictionsResourcesBasicSearch()//Ref. WF 29.8
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> DigitalRestrictionsResourcesReleaseParameters()//Ref. WF 29.9
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> DigitalRestrictionsTracksBasicSearch()//Ref. WF 29.10
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> DigitalRestrictionsTracksSearchReleaseParameters()//Ref. WF 29.11
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> DigitalRestrictionsResourcesAndTracksBasicSearch()//Ref. WF 29.12
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> DigitalRestrictionsResourcesAndTracksSearchReleaseParameters()//Ref. WF 29.13
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> DigitalRestrictionsReleases()//Ref. WF 29.14
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> SecondaryExploitationBasicSearch()//Ref. WF 29.15
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "pc_Left_ISRC", ChildClass1 = "cc1_Left_lblISRC", ChildClass2 = "cc2_Left_txtISRC", ChildClass3 = "cc3_Left_btnISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "pc_Right_AritstName", ChildClass1 = "cc1_Right_lblArtistName", ChildClass2 = "cc2_Right_txtArtistName", ChildClass3 = "cc3_Right_chkArtistName", ChildClass4 = "cc4_Right_lblExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "pc_Left_ResourceTitle", ChildClass1 = "cc1_Left_lblResourceTitle", ChildClass2 = "cc2_Left_txtResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "pc_Right_LinkedContractName", ChildClass1 = "cc1_Right_lblLinkedContractName", ChildClass2 = "cc2_Right_txtLinkedContractName", ChildClass3 = "cc2_Right_btnLinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "pc_Left_ResourceType", ChildClass1 = "cc1_Left_lblResourceType", ChildClass2 = "cc2_Left_ddlResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "pc_Right_RepertoireMultipleContracts", ChildClass1 = "cc1_Right_chkRepertoireMultipleContracts", ChildClass2 = "cc2_Right_lblRepertoireMultipleContracts" });


        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "pc_Left_TerritorialRights", ChildClass1 = "cc1_Left_lblTerritorialRights", ChildClass2 = "cc2_Left_txtTerritorialRights", ChildClass3 = "cc3_Left_lnkSelRemCountries" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "pc_Right_RightsReviewStatus", ChildClass1 = "cc1_Right_lblRightsReviewStatus", ChildClass2 = "cc2_Right_ddlRightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "pc_Left_RightsPeriod", ChildClass1 = "cc1_Left_lblRightsPeriod", ChildClass2 = "cc2_Left_ddlRightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "pc_Right_LostRightsIndicator", ChildClass1 = "cc1_Right_lblLostRightsIndicator", ChildClass2 = "cc2_ddlLostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "pc_Left_PhysicialExploitationRights", ChildClass1 = "cc1_Left_lblPhysicialExploitationRights", ChildClass2 = "cc2_Left_ddlPhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "pc_Right_LostRightsReason", ChildClass1 = "cc1_Right_lblLostRightsReason", ChildClass2 = "cc2_Right_ddlLostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "pc_Left_DigitalExploitationRights", ChildClass1 = "cc1_Left_lblDigitalExploitationRights", ChildClass2 = "cc2_Left_ddlDigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "pc_Right_DigitalUnbundlingAllowed", ChildClass1 = "cc1_Right_lblDigitalUnbundlingAllowed", ChildClass2 = "cc2_Right_ddlDigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "pc_Left_MobileExploitationRights", ChildClass1 = "cc1_Left_lblMobileExploitationRights", ChildClass2 = "cc2_Left_ddlMobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "pc_Right_PPBRevenueClaim", ChildClass1 = "cc1_Right_lblPPBRevenueClaim", ChildClass2 = "cc2_Right_ddlPPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "pc_Left_ActiveforMarketing", ChildClass1 = "cc1_Left_ActiveforMarketing", ChildClass2 = "cc2_Left_ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "pc_Right_LostRightsDate", ChildClass1 = "cc1_Right_lblLostRightsDate", ChildClass2 = "cc2_Right_lblFromLostRightsDate", ChildClass3 = "cc2_Right_fromdtLostRightsDate", ChildClass4 = "cc2_Right_lblToLostRightsDate", ChildClass5 = "cc3_Right_todtLostRightsDate" });        

        //Third accordion items
        objScreen1.Add("SecondaryExploitationTypes", new ControlKeys() { ID = "SecondaryExploitationTypes", ParentClass = "pc_SecondaryExploitationTypes" });


        return objScreen1;
    }
    private Dictionary<string, ControlKeys> SecondaryExploitationReleaseParameters()//Ref. WF 29.16
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "LostRightsDate" });

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> PreClearedBasicSearch()//Ref. WF 29.17
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items

        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "pc_Left_ISRC", ChildClass1 = "cc1_Left_lblISRC", ChildClass2 = "cc2_Left_txtISRC", ChildClass3 = "cc3_Left_btnISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "pc_Right_AritstName", ChildClass1 = "cc1_Right_lblArtistName", ChildClass2 = "cc2_Right_txtArtistName", ChildClass3 = "cc3_Right_chkArtistName", ChildClass4 = "cc4_Right_lblExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "pc_Left_ResourceTitle", ChildClass1 = "cc1_Left_lblResourceTitle", ChildClass2 = "cc2_Left_txtResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "pc_Right_LinkedContractName", ChildClass1 = "cc1_Right_lblLinkedContractName", ChildClass2 = "cc2_Right_txtLinkedContractName", ChildClass3 = "cc2_Right_btnLinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "pc_Left_ResourceType", ChildClass1 = "cc1_Left_lblResourceType", ChildClass2 = "cc2_Left_ddlResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "pc_Right_RepertoireMultipleContracts", ChildClass1 = "cc1_Right_chkRepertoireMultipleContracts", ChildClass2 = "cc2_Right_lblRepertoireMultipleContracts" });


        //second accordion items

        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "pc_Left_TerritorialRights", ChildClass1 = "cc1_Left_lblTerritorialRights", ChildClass2 = "cc2_Left_txtTerritorialRights", ChildClass3 = "cc3_Left_lnkSelRemCountries" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "pc_Right_RightsReviewStatus", ChildClass1 = "cc1_Right_lblRightsReviewStatus", ChildClass2 = "cc2_Right_ddlRightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "pc_Left_RightsPeriod", ChildClass1 = "cc1_Left_lblRightsPeriod", ChildClass2 = "cc2_Left_ddlRightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "pc_Right_LostRightsIndicator", ChildClass1 = "cc1_Right_lblLostRightsIndicator", ChildClass2 = "cc2_ddlLostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "pc_Left_PhysicialExploitationRights", ChildClass1 = "cc1_Left_lblPhysicialExploitationRights", ChildClass2 = "cc2_Left_ddlPhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "pc_Right_LostRightsReason", ChildClass1 = "cc1_Right_lblLostRightsReason", ChildClass2 = "cc2_Right_ddlLostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "pc_Left_DigitalExploitationRights", ChildClass1 = "cc1_Left_lblDigitalExploitationRights", ChildClass2 = "cc2_Left_ddlDigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "pc_Right_DigitalUnbundlingAllowed", ChildClass1 = "cc1_Right_lblDigitalUnbundlingAllowed", ChildClass2 = "cc2_Right_ddlDigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "pc_Left_MobileExploitationRights", ChildClass1 = "cc1_Left_lblMobileExploitationRights", ChildClass2 = "cc2_Left_ddlMobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "pc_Right_PPBRevenueClaim", ChildClass1 = "cc1_Right_lblPPBRevenueClaim", ChildClass2 = "cc2_Right_ddlPPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "pc_Left_ActiveforMarketing", ChildClass1 = "cc1_Left_ActiveforMarketing", ChildClass2 = "cc2_Left_ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "pc_Right_LostRightsDate", ChildClass1 = "cc1_Right_lblLostRightsDate", ChildClass2 = "cc2_Right_lblFromLostRightsDate", ChildClass3 = "cc2_Right_fromdtLostRightsDate", ChildClass4 = "cc2_Right_lblToLostRightsDate", ChildClass5 = "cc3_Right_todtLostRightsDate" });        

        //Third accordion items
        objScreen1.Add("PreClearanceTypes", new ControlKeys() { ID = "PreClearanceTypes", ParentClass = "pc_PreClearanceTypes"});

        return objScreen1;
    }
    private Dictionary<string, ControlKeys> PreClearedReleaseParameters()//Ref. WF 29.18
    {
        Dictionary<string, ControlKeys> objScreen1 = new Dictionary<string, ControlKeys>();
        //first Accordion items
        objScreen1.Add("ISRC", new ControlKeys() { ID = "ISRC", ParentClass = "ISRC" });
        objScreen1.Add("ArtistName", new ControlKeys() { ID = "ArtistName", ParentClass = "ArtistName" });
        objScreen1.Add("ExactSearch", new ControlKeys() { ID = "ExactSearch", ParentClass = "ExactSearch" });
        objScreen1.Add("ResourceTitle", new ControlKeys() { ID = "ResourceTitle", ParentClass = "ResourceTitle" });
        objScreen1.Add("LinkedContractName", new ControlKeys() { ID = "LinkedContractName", ParentClass = "LinkedContractName" });
        objScreen1.Add("ResourceType", new ControlKeys() { ID = "ResourceType", ParentClass = "ResourceType" });
        objScreen1.Add("RepertoireMultipleContracts", new ControlKeys() { ID = "RepertoireMultipleContracts", ParentClass = "RepertoireMultipleContracts" });

        //second accordion items
        objScreen1.Add("TerritorialRights", new ControlKeys() { ID = "TerritorialRights", ParentClass = "TerritorialRights" });
        objScreen1.Add("RightsReviewStatus", new ControlKeys() { ID = "RightsReviewStatus", ParentClass = "RightsReviewStatus" });
        objScreen1.Add("RightsPeriod", new ControlKeys() { ID = "RightsPeriod", ParentClass = "RightsPeriod" });
        objScreen1.Add("LostRightsIndicator", new ControlKeys() { ID = "LostRightsIndicator", ParentClass = "LostRightsIndicator" });
        objScreen1.Add("PhysicialExploitationRights", new ControlKeys() { ID = "PhysicialExploitationRights", ParentClass = "PhysicialExploitationRights" });
        objScreen1.Add("LostRightsReason", new ControlKeys() { ID = "LostRightsReason", ParentClass = "LostRightsReason" });
        objScreen1.Add("DigitalExploitationRights", new ControlKeys() { ID = "DigitalExploitationRights", ParentClass = "DigitalExploitationRights" });
        objScreen1.Add("DigitalUnbundlingAllowed", new ControlKeys() { ID = "DigitalUnbundlingAllowed", ParentClass = "DigitalUnbundlingAllowed" });
        objScreen1.Add("MobileExploitationRights", new ControlKeys() { ID = "MobileExploitationRights", ParentClass = "MobileExploitationRights" });
        objScreen1.Add("PPBRevenueClaim", new ControlKeys() { ID = "PPBRevenueClaim", ParentClass = "PPBRevenueClaim" });
        objScreen1.Add("ActiveforMarketing", new ControlKeys() { ID = "ActiveforMarketing", ParentClass = "ActiveforMarketing" });
        objScreen1.Add("LostRightsDate", new ControlKeys() { ID = "LostRightsDate", ParentClass = "pc_Right_LostRightsDate", ChildClass1 = "cc1_Right_lblLostRightsDate", ChildClass2 = "cc2_Right_lblFromLostRightsDate", ChildClass3 = "cc2_Right_fromdtLostRightsDate", ChildClass4 = "cc2_Right_lblToLostRightsDate", ChildClass5 = "cc3_Right_todtLostRightsDate" });        

        return objScreen1;
    }
  }
}