using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using UMGI.GRCS.Entities.ContractEntities;
using UMGI.GRCS.Entities.NoticeCompanyEntities;
using UMGI.GRCS.Entities.ArtistEntities;
using UMGI.GRCS.Entities.Templates.GlobalAddressEntities;
using UMGI.GRCS.Entities.Entities.ContractEntities;
using UMGI.GRCS.UI.Rights.Interfaces;
using UMGI.GRCS.Common.Interfaces;

namespace UMGI.GRCS.UI.Rights.Models
{
    public class ContractInfoManager :ManagerBase, IContractInfoManager
    {
        //TODO:
        //IContractService ContractService;

        public ContractInfoManager()
        {
            NewContract = new Contract();
            NewPCCompany = new NoticeCompany();
            Artist = new ArtistSearchResult();
            SearchGlobalAddressList = new GlobalAddressSearch();
            ContractsSearch = new ContractSearch();
            

            /* Dummy values for Search PC Country Dropdown */
            List<NoticeCompany> objNoticeCompany = new List<NoticeCompany>();
           

            objNoticeCompany.Add(new NoticeCompany() { CountryName = "US" });
            objNoticeCompany.Add(new NoticeCompany() { CountryName = "UK" });
            objNoticeCompany.Add(new NoticeCompany() { CountryName = "Australia" });

            Country = objNoticeCompany.Select(Restriction => new SelectListItem { Text = Restriction.CountryName, Value = Restriction.CountryName.ToString() });

            /* Dummy values for Search Global List Category Dropdown */
            List<GlobalAddressSearch> objCategory = new List<GlobalAddressSearch>();
            objCategory.Add(new GlobalAddressSearch() { SearchCategoryId = 1, SearchValue = "Name Only" });
            objCategory.Add(new GlobalAddressSearch() { SearchCategoryId = 2, SearchValue = "Id" });
            SearchCategory = objCategory.Select(category => new SelectListItem { Text = category.SearchValue, Value = category.SearchValue.ToString() });


            /* Dummy values for Search Contract screen Dropdowns */
            List<ContractSearch> objWorkFlowStatus = new List<ContractSearch>();
            objWorkFlowStatus.Add(new ContractSearch() {  WorkFlowStatus = "Draft Entry" });
            objWorkFlowStatus.Add(new ContractSearch() {  WorkFlowStatus = "Pending Approval" });
            objWorkFlowStatus.Add(new ContractSearch() {  WorkFlowStatus = "Under Amendment" });
            WorkFlowStatus = objWorkFlowStatus.Select(contract => new SelectListItem { Text = contract.WorkFlowStatus, Value = contract.WorkFlowStatus });


            List<ContractSearch> objContractStatus = new List<ContractSearch>();
            objContractStatus.Add(new ContractSearch() { ContractStatus = "Draft" });
            objContractStatus.Add(new ContractSearch() { ContractStatus = "Pending" });
            objContractStatus.Add(new ContractSearch() { ContractStatus = "Amendment" });
            ContractStatus = objContractStatus.Select(contract => new SelectListItem { Text = contract.ContractStatus, Value = contract.ContractStatus });

            //List<ContractSearch> objContractParty = new List<ContractSearch>();
            //objContractParty.Add(new ContractSearch() { ClearanceCompanyValue = "Universal Music Group" });
            //objContractParty.Add(new ContractSearch() { ClearanceCompanyValue = "Sony dste"});
            //ClearanceAdminCompany = objContractParty.Select(contract => new SelectListItem { Text = contract.ClearanceCompanyValue, Value = contract.ClearanceCompanyId.ToString() });

            List<StringListEntity> objClearanceCompList = new List<StringListEntity>();
            objClearanceCompList.Add(new StringListEntity() { Id = 1, Value = "JB Productions(Code), Great Britain" });
            objClearanceCompList.Add(new StringListEntity() { Id = 1, Value = "KK Productions" });
            ClearanceAdminCompany = objClearanceCompList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<ContractSearch> objRightsType = new List<ContractSearch>();
            objRightsType.Add(new ContractSearch() { RightsTypeValue ="All" });
            objRightsType.Add(new ContractSearch() { RightsTypeValue = "None"});
            RightsType = objRightsType.Select(contract => new SelectListItem { Text = contract.RightsTypeValue, Value = contract.RightsTypeValue.ToString() });

            List<StringListEntity> objUmgSigningCompList = new List<StringListEntity>();
            objUmgSigningCompList.Add(new StringListEntity() { Id = 0, Value = "comp1" });
            objUmgSigningCompList.Add(new StringListEntity() { Id = 1, Value = "comp2" });
            UmgSigningCompList = objUmgSigningCompList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<StringListEntity> objContractStatusList = new List<StringListEntity>();
            objContractStatusList.Add(new StringListEntity() { Id = 0, Value = "Signed Contract" });
            objContractStatusList.Add(new StringListEntity() { Id = 1, Value = "Deam Memo/Draft Contract" });
            ContractStatusList = objContractStatusList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<StringListEntity> objOnActiveRoosterList = new List<StringListEntity>();
            objOnActiveRoosterList.Add(new StringListEntity() { Id = 0, Value = "Yes Active" });
            objOnActiveRoosterList.Add(new StringListEntity() { Id = 1, Value = "No - Inactive:Out of Term/Dropped" });
            OnActiveRoosterList = objOnActiveRoosterList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<StringListEntity> objRightsPeriodList = new List<StringListEntity>();
            objRightsPeriodList.Add(new StringListEntity() { Id = 0, Value = "Life of Copyright" });
            objRightsPeriodList.Add(new StringListEntity() { Id = 1, Value = "One Year" });
            RightsPeriodList = objRightsPeriodList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<StringListEntity> objLostRightsIndicatorList = new List<StringListEntity>();
            objLostRightsIndicatorList.Add(new StringListEntity() { Id = 0, Value = "Yes" });
            objLostRightsIndicatorList.Add(new StringListEntity() { Id = 1, Value = "No" });
            LostRightsIndicatorList = objLostRightsIndicatorList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<StringListEntity> objLostRightsReasonList = new List<StringListEntity>();
            objLostRightsReasonList.Add(new StringListEntity() { Id = 0, Value = "Reason 1" });
            objLostRightsReasonList.Add(new StringListEntity() { Id = 1, Value = "Reason 2" });
            LostRightsReasonList = objLostRightsReasonList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });
                       
            List<StringListEntity> objLegalReviewRequiredList = new List<StringListEntity>();
            objLegalReviewRequiredList.Add(new StringListEntity() { Id = 0, Value = "Yes" });
            objLegalReviewRequiredList.Add(new StringListEntity() { Id = 1, Value = "No" });
            LegalReviewRequiredList = objLegalReviewRequiredList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<StringListEntity> objActiveForMarketingList = new List<StringListEntity>();
            objActiveForMarketingList.Add(new StringListEntity() { Id = 0, Value = "Yes" });
            objActiveForMarketingList.Add(new StringListEntity() { Id = 1, Value = "No" });
            ActiveForMarketingList = objActiveForMarketingList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            List<StringListEntity> objSensitiveArtistList = new List<StringListEntity>();
            objSensitiveArtistList.Add(new StringListEntity() { Id = 0, Value = "Yes" });
            objSensitiveArtistList.Add(new StringListEntity() { Id = 1, Value = "No" });
            SensitiveArtistList = objSensitiveArtistList.Select(p => new SelectListItem { Text = p.Value, Value = p.Id.ToString() });

            //TODO:
            //ContractService = GetService<IContractService>();
            //var test = ContractService.GetNoticeCompany(new NoticeCompanySearch() {CountryId=1,CompanyName="test" });                        
        }
        

        public Contract NewContract { get; set; }
        public NoticeCompany NewPCCompany { get; set; }
        public ArtistSearchResult Artist { get; set; }
        public ContractSearch ContractsSearch { get; set; }
        public GlobalAddressSearch SearchGlobalAddressList { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        public IEnumerable<SelectListItem> SearchCategory { get; set; }
        public IEnumerable<SelectListItem> WorkFlowStatus { get; set; }
        public IEnumerable<SelectListItem> ContractStatus { get; set; }
        public IEnumerable<SelectListItem> ContractingParty { get; set; }
        public IEnumerable<SelectListItem> ClearanceAdminCompany { get; set; }
        public IEnumerable<SelectListItem> RightsType { get; set; }

        public IEnumerable<SelectListItem> UmgSigningCompList { get; set; }
        public IEnumerable<SelectListItem> ContractStatusList { get; set; }
        public IEnumerable<SelectListItem> OnActiveRoosterList { get; set; }
        public IEnumerable<SelectListItem> RightsPeriodList { get; set; }
        public IEnumerable<SelectListItem> LostRightsIndicatorList { get; set; }
        public IEnumerable<SelectListItem> LostRightsReasonList { get; set; }              
        public IEnumerable<SelectListItem> LegalReviewRequiredList { get; set; }
        public IEnumerable<SelectListItem> ActiveForMarketingList { get; set; }
        public IEnumerable<SelectListItem> SensitiveArtistList { get; set; }

    }
}
