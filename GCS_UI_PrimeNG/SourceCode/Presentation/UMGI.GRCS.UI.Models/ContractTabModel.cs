/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractTabModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 10-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;
using UMGI.GRCS.UI.Interfaces;
using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.UI.Models
{    
    /// <summary>
    /// Model For Contract Details Page
    /// </summary>
    [Serializable]
    public class ContractTabModel : IContractTabModel,ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractTabModel"/> class.
        ///      
        /// </summary>
       
        public ContractDetails Contract { get; set; }
        public ContractInfo Contracts { get; set; }
        public NoticeCompany PcCompany { get; set; }
        public ArtistDetail Artist { get; set; }
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
        public IEnumerable<SelectListItem> PcCompanyCountryList { get; set; }
        public IEnumerable<SelectListItem> ContractDescriptionList { get; set; }
        public IEnumerable<SelectListItem> ContractStatusList { get; set; }
        public IEnumerable<SelectListItem> OnActiveRosterList { get; set; }
        public IEnumerable<SelectListItem> RightsPeriodList { get; set; }
        public IEnumerable<SelectListItem> LostRightsIndicatorList { get; set; }
        public IEnumerable<SelectListItem> LostRightsReasonList { get; set; }
        public IEnumerable<SelectListItem> LegalReviewRequiredList { get; set; }
        public IEnumerable<SelectListItem> ActiveForMarketingList { get; set; }
        public IEnumerable<SelectListItem> SensitiveArtistList { get; set; }
        public IEnumerable<SelectListItem> SelectTemplate { get; set; }
        public IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        public ContractInfo AddParentContract { get; set; }
        public List<ContractInfo> SplitContract { get; set; }
        public string UserRoleName { get; set; }
        
        public ContractTabModel()
        {
            Contract = new ContractDetails();
            Contracts = new ContractInfo();
            PcCompany = new NoticeCompany();
            Artist = new ArtistDetail();
            SearchGlobalAddressList = new GlobalAddressSearch();
            ContractsSearch = new ContractSearch();
            AddParentContract = new ContractInfo();
            SplitContract = new List<ContractInfo>();

           

            # region Search Global Address 
            var objCategory = new List<StringIdentifier>
                                  {
                                      new StringIdentifier {Id = 0, Description = Constants.NameOnly},
                                      new StringIdentifier {Id = 1, Description = Constants.Id}
                                  };
            SearchCategory = objCategory.Select(category => new SelectListItem { Text = category.Description, Value = category.Id.ToString(CultureInfo.InvariantCulture) });
            # endregion
                       
            var objYesNoList = new List<StringIdentifier>
                                   {
                                       new StringIdentifier {Id = 0, Description = Constants.YesValue},
                                       new StringIdentifier {Id = 1, Description = Constants.NoValue}
                                   };

            LostRightsIndicatorList = objYesNoList.Reverse<StringIdentifier>().Select(p => new SelectListItem { Text = p.Description, Value = p.Id.ToString(CultureInfo.InvariantCulture), Selected = true });
            LegalReviewRequiredList = objYesNoList.Reverse<StringIdentifier>().Select(p => new SelectListItem { Text = p.Description, Value = p.Id.ToString(CultureInfo.InvariantCulture), Selected = true });
            ActiveForMarketingList = objYesNoList.Reverse<StringIdentifier>().Select(p => new SelectListItem { Text = p.Description, Value = p.Id.ToString(CultureInfo.InvariantCulture), Selected = true });
            SensitiveArtistList = objYesNoList.Reverse<StringIdentifier>().Select(p => new SelectListItem { Text = p.Description, Value = p.Id.ToString(CultureInfo.InvariantCulture), Selected = true });          
        }

        protected ContractTabModel(SerializationInfo info, StreamingContext context) 
        {
            this.Contract = (ContractDetails)info.GetValue("Contract", typeof(ContractDetails));
            this.AddParentContract = (ContractInfo)info.GetValue("AddParentContract", typeof(ContractInfo));
            this.SplitContract = (List<ContractInfo>)info.GetValue("SplitContract", typeof(List<ContractInfo>)); ;            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Contract", this.Contract);
            info.AddValue("AddParentContract", this.AddParentContract);
            info.AddValue("SplitContract", this.SplitContract);
        }
    }
}
