using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Lookups;


namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceProjectModel
    {

        MasterProject MasterProjectDetails { get; set; }
        ClearanceRegularProject RegularProjectDetails { get; set; }
        IEnumerable<SelectListItem> ProjectType { get; set; }
        IEnumerable<SelectListItem> RequestType { get; set; }
        IEnumerable<SelectListItem> ItemsPerPage { get; set; }

        IEnumerable<SelectListItem> CurrPriceLevelList { get; set; }
        List<RequestTypeRegular> GetCurrentPricelevel { get; set; }

        IEnumerable<SelectListItem> RequestedPriceLevelList { get; set; }
        List<RequestTypeRegular> GetRequestedPriceLevel { get; set; }

        IEnumerable<SelectListItem> LabelList { get; set; }
        IEnumerable<ListItem> ConfigList { get; set; }
        IEnumerable<SelectListItem> ConfigGroupList { get; set; }
        IEnumerable<SelectListItem> ResourceType { get; set; }
        IEnumerable<SelectListItem> RecordingType { get; set; }
        IEnumerable<SelectListItem> MusicType { get; set; }

        IEnumerable<SelectListItem> CompanyList { get; set; }
        IEnumerable<SelectListItem> CurrencyList { get; set; }
        List<CompanyInfo> CompanySearch { get; set; }
        List<CompanyInfo> GetCompanies { get; set; }
        int TotalRows { get; set; }

        string selectedRows { get; set; }
        List<DropDeviatedICLALevel> dropDeviatedIclaLevel { get; set; }

        List<DropPriceLevel> dropPriceLevel { get; set; }
        List<DropPromotionalLevel> dropPromotionalLevel { get; set; }
        List<DropClubLevel> dropClubLevel { get; set; }
        int ReadOnlyMode { get; set; }

        IEnumerable<SelectListItem> PriceLevelType { get; set; }
        IEnumerable<SelectListItem> ICLALevelType { get; set; }
        IEnumerable<SelectListItem> YesNoDropdown { get; set; }
        int ItemsPerPageDefaultValue { get; set; }

        IEnumerable<SelectListItem> MusicTypeResourceTab { get; set; }
        IEnumerable<SelectListItem> ResourceTypeResourceTab { get; set; }
        IEnumerable<SelectListItem> RecordingTypeResourceTab { get; set; }

        IEnumerable<SelectListItem> RequestTypeManufacturedBy { get; set; }
        string UPCAllocationRightsGroup { get; set; }
        IEnumerable<SelectListItem> DivisionList { get; set; }

        R2Authentication _r2Authentication { get; set; }

        List<ListItem> RccHandler { get; set; }
        int isInMaintainMode { get; set; }

        bool IsProjectBlocked { get; set; }

        RoleGroup roleGroupName { get; set; }

    }
}
