using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.BusinessEntities.Lookups;
using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.UI.Models
{
    [Serializable]
    public class ClearanceProjectModel : IClearanceProjectModel, ISerializable
    {
        public ClearanceProjectModel()
        {
            CompanySearch = new List<CompanyInfo>();
            CompanyDetails = new CompanyInfo();
            var _ItemsPerPage = new List<StringIdentifier>
            {
                new StringIdentifier { Id = 25, Description = "25" },
                new StringIdentifier { Id = 50, Description = "50" },
                new StringIdentifier { Id = 100, Description = "100" },
                new StringIdentifier { Id = 150, Description = "150" },
            };
            ItemsPerPage = _ItemsPerPage.Select(results => new SelectListItem { Text = results.Description, Value = results.Id.ToString(CultureInfo.InvariantCulture) });

            MasterProjectDetails = new MasterProject();
            RegularProjectDetails = new ClearanceRegularProject();

            var _YesNoDropdown = new List<StringIdentifier>
            {
            new StringIdentifier { Id = 1, Description = "No" },
            new StringIdentifier { Id = 2, Description = "Yes" },
            };
            YesNoDropdown = _YesNoDropdown.Select(results => new SelectListItem { Text = results.Description, Value = results.Id.ToString(CultureInfo.InvariantCulture) });

            var _requestTypeManufacturedBy = new List<StringIdentifier>
            {
                new StringIdentifier { Id = 1, Description = "Yes" },
                new StringIdentifier { Id = 2, Description = "No" },
            };
            RequestTypeManufacturedBy = _requestTypeManufacturedBy.Select(results => new SelectListItem { Text = results.Description, Value = results.Id.ToString(CultureInfo.InvariantCulture) });

        }

        public MasterProject MasterProjectDetails { get; set; }

        public ClearanceRegularProject RegularProjectDetails { get; set; }

        public List<RequestTypeRegular> GetCurrentPricelevel { get; set; }

        public List<RequestTypeRegular> GetRequestedPriceLevel { get; set; }

        public List<CompanyInfo> CompanySearch { get; set; }

        public CompanyInfo CompanyDetails { get; set; }

        public List<CompanyInfo> GetCompanies { get; set; }

        public int TotalRows { get; set; }

        public static int TotalRowsCount { get; set; }

        // for data already saved in the project

        public string selectedRows { get; set; }

        public List<DropDeviatedICLALevel> dropDeviatedIclaLevel { get; set; }

        public List<DropPriceLevel> dropPriceLevel { get; set; }

        public int ReadOnlyMode { get; set; }

        //-------------------Release new ------------------------------------

        public RegularProject ReleaseProjectDetails { get; set; }

        public string ConfigGroupName { get; set; }

        public int ItemsPerPageDefaultValue { get; set; }

        public List<DropClubLevel> dropClubLevel { get; set; }

        public List<DropPromotionalLevel> dropPromotionalLevel { get; set; }

        public IEnumerable<SelectListItem> ProjectType { get; set; }

        public List<MySelectListItem> ProjectTypeTemp { get; set; }

        public IEnumerable<SelectListItem> RequestType { get; set; }

        public List<MySelectListItem> RequestTypeTemp { get; set; }

        public IEnumerable<SelectListItem> ItemsPerPage { get; set; }

        public List<MySelectListItem> ItemsPerPageTemp { get; set; }

        public IEnumerable<SelectListItem> CurrPriceLevelList { get; set; }

        public List<MySelectListItem> CurrPriceLevelListTemp { get; set; }

        public IEnumerable<SelectListItem> RequestedPriceLevelList { get; set; }

        public List<MySelectListItem> RequestedPriceLevelListTemp { get; set; }

        public IEnumerable<SelectListItem> LabelList { get; set; }

        public List<MySelectListItem> LabelListTemp { get; set; }

        public IEnumerable<ListItem> ConfigList { get; set; }

        public List<MySelectListItem> ConfigListTemp { get; set; }

        public IEnumerable<SelectListItem> ConfigGroupList { get; set; }

        public List<MySelectListItem> ConfigGroupListTemp { get; set; }

        public IEnumerable<SelectListItem> MusicType { get; set; }

        public List<MySelectListItem> MusicTypeTemp { get; set; }

        public IEnumerable<SelectListItem> PriceLevelType { get; set; }

        public List<MySelectListItem> PriceLevelTypeTemp { get; set; }

        public IEnumerable<SelectListItem> ICLALevelType { get; set; }

        public List<MySelectListItem> ICLALevelTypeTemp { get; set; }

        public IEnumerable<SelectListItem> ResourceType { get; set; }

        public List<MySelectListItem> ResourceTypeTemp { get; set; }

        public IEnumerable<SelectListItem> RecordingType { get; set; }

        public List<MySelectListItem> RecordingTypeTemp { get; set; }

        public IEnumerable<SelectListItem> YesNoDropdown { get; set; }

        public List<MySelectListItem> YesNoDropdownTemp { get; set; }

        public IEnumerable<SelectListItem> MusicTypeResourceTab { get; set; }

        public List<MySelectListItem> MusicTypeResourceTabTemp { get; set; }

        public IEnumerable<SelectListItem> ResourceTypeResourceTab { get; set; }

        public List<MySelectListItem> ResourceTypeResourceTabTemp { get; set; }

        public IEnumerable<SelectListItem> RecordingTypeResourceTab { get; set; }

        public List<MySelectListItem> RecordingTypeResourceTabTemp { get; set; }

        public IEnumerable<SelectListItem> RequestTypeManufacturedBy { get; set; }

        public List<MySelectListItem> RequestTypeManufacturedByTemp { get; set; }

        public IEnumerable<SelectListItem> DivisionList { get; set; }

        public List<MySelectListItem> DivisionListTemp { get; set; }

        public IEnumerable<SelectListItem> CompanyList { get; set; }

        public List<MySelectListItem> CompanyListTemp { get; set; }

        public IEnumerable<SelectListItem> CurrencyList { get; set; }

        public List<MySelectListItem> CurrencyListTemp { get; set; }

        public List<ContractDetails> ContractRolesAndRights { get; set; }

        public string UPCAllocationRightsGroup { get; set; }

        public R2Authentication _r2Authentication { get; set; }

        public List<ListItem> RccHandler { get; set; }

        public int isInMaintainMode { get; set; }

        public bool IsProjectBlocked { get; set; }

        public RoleGroup roleGroupName { get; set; }

        protected ClearanceProjectModel(SerializationInfo info, StreamingContext context)
        {
            this.MasterProjectDetails = (MasterProject)info.GetValue("MasterProjectDetails", typeof(MasterProject));
            this.RegularProjectDetails = (ClearanceRegularProject)info.GetValue("RegularProjectDetails", typeof(ClearanceRegularProject));
            this.GetCurrentPricelevel = (List<RequestTypeRegular>)info.GetValue("GetCurrentPricelevel", typeof(List<RequestTypeRegular>));
            this.GetRequestedPriceLevel = (List<RequestTypeRegular>)info.GetValue("GetRequestedPriceLevel", typeof(List<RequestTypeRegular>));
            this.dropDeviatedIclaLevel = (List<DropDeviatedICLALevel>)info.GetValue("dropDeviatedIclaLevel", typeof(List<DropDeviatedICLALevel>));
            this.dropPriceLevel = (List<DropPriceLevel>)info.GetValue("dropPriceLevel", typeof(List<DropPriceLevel>));
            this.dropClubLevel = (List<DropClubLevel>)info.GetValue("dropClubLevel", typeof(List<DropClubLevel>));
            this.dropPromotionalLevel = (List<DropPromotionalLevel>)info.GetValue("dropPromotionalLevel", typeof(List<DropPromotionalLevel>));
            this.RccHandler = (List<ListItem>)info.GetValue("RccHandler", typeof(List<ListItem>));
            
            object temp;
            this.ProjectTypeTemp = null;
            temp = info.GetValue("ProjectType", typeof(List<MySelectListItem>));
            if (temp != null) this.ProjectType = DeSerializeSelectItemList(temp);

            this.RequestTypeTemp = null;
            temp = info.GetValue("RequestType", typeof(List<MySelectListItem>));
            if (temp != null) this.RequestType = DeSerializeSelectItemList(temp);

            this.ItemsPerPageTemp = null;
            temp = info.GetValue("ItemsPerPage", typeof(List<MySelectListItem>));
            if (temp != null) this.ItemsPerPage = DeSerializeSelectItemList(temp);

            this.CurrPriceLevelListTemp = null;
            temp = info.GetValue("CurrPriceLevelList", typeof(List<MySelectListItem>));
            if (temp != null) this.CurrPriceLevelList = DeSerializeSelectItemList(temp);

            this.RequestedPriceLevelListTemp = null;
            temp = info.GetValue("RequestedPriceLevelList", typeof(List<MySelectListItem>));
            if (temp != null) this.RequestedPriceLevelList = DeSerializeSelectItemList(temp);

            this.LabelListTemp = null;
            temp = info.GetValue("LabelList", typeof(List<MySelectListItem>));
            if (temp != null) this.LabelList = DeSerializeSelectItemList(temp);

            this.ConfigListTemp = null;
            temp = info.GetValue("ConfigList", typeof(List<ListItem>));
            if (temp != null)
            {
                List<MySelectListItem> input = (List<MySelectListItem>)temp;
                var output = from ip in input select new ListItem() { Selected = ip.Selected, Text = ip.Text, Value = ip.Value };
                this.ConfigList = output;
            }

            this.ConfigGroupListTemp = null;
            temp = info.GetValue("ConfigGroupList", typeof(List<MySelectListItem>));
            if (temp != null) this.ConfigGroupList = DeSerializeSelectItemList(temp);

            this.MusicTypeTemp = null;
            temp = info.GetValue("MusicType", typeof(List<MySelectListItem>));
            if (temp != null) this.MusicType = DeSerializeSelectItemList(temp);

            this.PriceLevelTypeTemp = null;
            temp = info.GetValue("PriceLevelType", typeof(List<MySelectListItem>));
            if (temp != null) this.PriceLevelType = DeSerializeSelectItemList(temp);

            this.ICLALevelTypeTemp = null;
            temp = info.GetValue("ICLALevelType", typeof(List<MySelectListItem>));
            if (temp != null) this.ICLALevelType = DeSerializeSelectItemList(temp);

            this.ResourceTypeTemp = null;
            temp = info.GetValue("ResourceType", typeof(List<MySelectListItem>));
            if (temp != null) this.ResourceType = DeSerializeSelectItemList(temp);

            this.RecordingTypeTemp = null;
            temp = info.GetValue("RecordingType", typeof(List<MySelectListItem>));
            if (temp != null) this.RecordingType = DeSerializeSelectItemList(temp);

            this.YesNoDropdownTemp = null;
            temp = info.GetValue("YesNoDropdown", typeof(List<MySelectListItem>));
            if (temp != null) this.YesNoDropdown = DeSerializeSelectItemList(temp);

            this.MusicTypeResourceTabTemp = null;
            temp = info.GetValue("MusicTypeResourceTab", typeof(List<MySelectListItem>));
            if (temp != null) this.MusicTypeResourceTab = DeSerializeSelectItemList(temp);

            this.ResourceTypeResourceTabTemp = null;
            temp = info.GetValue("ResourceTypeResourceTab", typeof(List<MySelectListItem>));
            if (temp != null) this.ResourceTypeResourceTab = DeSerializeSelectItemList(temp);

            this.RecordingTypeResourceTabTemp = null;
            temp = info.GetValue("RecordingTypeResourceTab", typeof(List<MySelectListItem>));
            if (temp != null) this.RecordingTypeResourceTab = DeSerializeSelectItemList(temp);

            this.RequestTypeManufacturedByTemp = null;
            temp = info.GetValue("RequestTypeManufacturedBy", typeof(List<MySelectListItem>));
            if (temp != null) this.RequestTypeManufacturedBy = DeSerializeSelectItemList(temp);

            this.DivisionListTemp = null;
            temp = info.GetValue("DivisionList", typeof(List<MySelectListItem>));
            if (temp != null) this.DivisionList = DeSerializeSelectItemList(temp);

            this.CompanyListTemp = null;
            temp = info.GetValue("CompanyList", typeof(List<MySelectListItem>));
            if (temp != null) this.CompanyList = DeSerializeSelectItemList(temp);

            this.CurrencyListTemp = null;
            temp = info.GetValue("CurrencyList", typeof(List<MySelectListItem>));
            if (temp != null) this.CurrencyList = DeSerializeSelectItemList(temp);

            this.UPCAllocationRightsGroup = (string)info.GetValue("UPCAllocationRightsGroup", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MasterProjectDetails", this.MasterProjectDetails);
            info.AddValue("RegularProjectDetails", this.RegularProjectDetails);
            info.AddValue("GetCurrentPricelevel", this.GetCurrentPricelevel);
            info.AddValue("GetRequestedPriceLevel", this.GetRequestedPriceLevel);
            info.AddValue("dropDeviatedIclaLevel", this.dropDeviatedIclaLevel);
            info.AddValue("dropPriceLevel", this.dropPriceLevel);
            info.AddValue("dropClubLevel", this.dropClubLevel);
            info.AddValue("dropPromotionalLevel", this.dropPromotionalLevel);
            info.AddValue("RccHandler", this.RccHandler);

            this.ProjectTypeTemp = this.ProjectType == null ? null : SerializeSelectItemList(this.ProjectType);
            info.AddValue("ProjectType", this.ProjectTypeTemp);

            this.RequestTypeTemp = this.RequestType == null ? null : SerializeSelectItemList(this.RequestType);
            info.AddValue("RequestType", this.RequestTypeTemp);

            this.ItemsPerPageTemp = this.ItemsPerPage == null ? null : SerializeSelectItemList(this.ItemsPerPage);
            info.AddValue("ItemsPerPage", this.ItemsPerPageTemp);

            this.CurrPriceLevelListTemp = this.CurrPriceLevelList == null ? null : SerializeSelectItemList(this.CurrPriceLevelList);
            info.AddValue("CurrPriceLevelList", this.CurrPriceLevelListTemp);

            this.RequestedPriceLevelListTemp = this.RequestedPriceLevelList == null ? null : SerializeSelectItemList(this.RequestedPriceLevelList);
            info.AddValue("RequestedPriceLevelList", this.RequestedPriceLevelListTemp);

            this.LabelListTemp = this.LabelList == null ? null : SerializeSelectItemList(this.LabelList);
            info.AddValue("LabelList", this.LabelListTemp);

            if (this.ConfigList == null)
                this.ConfigListTemp = null;
            else
            {
                this.ConfigListTemp = (from ip in this.ConfigList select new MySelectListItem() { Selected = ip.Selected, Text = ip.Text, Value = ip.Value }).ToList<MySelectListItem>();
            }
            info.AddValue("ConfigList", this.ConfigListTemp);

            this.ConfigGroupListTemp = this.ConfigGroupList == null ? null : SerializeSelectItemList(this.ConfigGroupList);
            info.AddValue("ConfigGroupList", this.ConfigGroupListTemp);

            this.MusicTypeTemp = this.MusicType == null ? null : SerializeSelectItemList(this.MusicType);
            info.AddValue("MusicType", this.MusicTypeTemp);

            this.PriceLevelTypeTemp = this.PriceLevelType == null ? null : SerializeSelectItemList(this.PriceLevelType);
            info.AddValue("PriceLevelType", this.PriceLevelTypeTemp);

            this.ICLALevelTypeTemp = this.ICLALevelType == null ? null : SerializeSelectItemList(this.ICLALevelType);
            info.AddValue("ICLALevelType", this.ICLALevelTypeTemp);

            this.ResourceTypeTemp = this.ResourceType == null ? null : SerializeSelectItemList(this.ResourceType);
            info.AddValue("ResourceType", this.ResourceTypeTemp);

            this.RecordingTypeTemp = this.RecordingType == null ? null : SerializeSelectItemList(this.RecordingType);
            info.AddValue("RecordingType", this.RecordingTypeTemp);

            this.YesNoDropdownTemp = this.YesNoDropdown == null ? null : SerializeSelectItemList(this.YesNoDropdown);
            info.AddValue("YesNoDropdown", this.YesNoDropdownTemp);

            this.MusicTypeResourceTabTemp = this.MusicTypeResourceTab == null ? null : SerializeSelectItemList(this.MusicTypeResourceTab);
            info.AddValue("MusicTypeResourceTab", this.MusicTypeResourceTabTemp);

            this.ResourceTypeResourceTabTemp = this.ResourceTypeResourceTab == null ? null : SerializeSelectItemList(this.ResourceTypeResourceTab);
            info.AddValue("ResourceTypeResourceTab", this.ResourceTypeResourceTabTemp);

            this.RecordingTypeResourceTabTemp = this.RecordingTypeResourceTab == null ? null : SerializeSelectItemList(this.RecordingTypeResourceTab);
            info.AddValue("RecordingTypeResourceTab", this.RecordingTypeResourceTabTemp);

            this.RequestTypeManufacturedByTemp = this.RequestTypeManufacturedBy == null ? null : SerializeSelectItemList(this.RequestTypeManufacturedBy);
            info.AddValue("RequestTypeManufacturedBy", this.RequestTypeManufacturedByTemp);

            this.DivisionListTemp = this.DivisionList == null ? null : SerializeSelectItemList(this.DivisionList);
            info.AddValue("DivisionList", this.DivisionListTemp);

            this.CompanyListTemp = this.CompanyList == null ? null : SerializeSelectItemList(this.CompanyList);
            info.AddValue("CompanyList", this.CompanyListTemp);

            this.CurrencyListTemp = this.CurrencyList == null ? null : SerializeSelectItemList(this.CurrencyList);
            info.AddValue("CurrencyList", this.CurrencyListTemp);

            info.AddValue("UPCAllocationRightsGroup", this.UPCAllocationRightsGroup);
        }

        List<MySelectListItem> SerializeSelectItemList(IEnumerable<SelectListItem> input)
        {
            var output = (from ip in input select new MySelectListItem(ip)).ToList<MySelectListItem>();
            return output;
        }

        IEnumerable<SelectListItem> DeSerializeSelectItemList(object sessionItem)
        {
            List<MySelectListItem> input = (List<MySelectListItem>)sessionItem;
            var output = from ip in input select ip.GetSelectListItem();
            return output;
        }

    }
    [Serializable]
    public class MySelectListItem
    {
        public MySelectListItem(SelectListItem myData)
        {
            this.Selected = myData.Selected;
            this.Text = myData.Text;
            this.Value = myData.Value;
        }
        public MySelectListItem() { }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public SelectListItem GetSelectListItem()
        {
            return new SelectListItem() { Selected = this.Selected, Text = this.Text, Value = this.Value };
        }
    }

}
