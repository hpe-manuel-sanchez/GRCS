
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    [DataContract]
    [Serializable]
    public class LeanContractInfo : EntityInformation
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractDetails"/> class.
        /// </summary>
        public LeanContractInfo()
        {
            ContractTerritoryList = new List<TerritorialDisplay>();
        }

        [DataMember]
        public List<TerritorialDisplay> ContractTerritoryList { get; set; }

        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// clearance AdminCompany
        /// </summary>
        [DataMember]
        public string ClearanceAdminCompanyName { get; set; }

        [DataMember]
        public long ClearanceAdminCompanyId { get; set; }

        [DataMember]
        public string TerritoryData { get; set; }

        [DataMember]
        public string IncludedTerritoryData { get; set; }

        [DataMember]
        public string ExcludedTerritoryData { get; set; }

        [DataMember]
        public string ArtistName { get; set; }

        #region routed entiry
        /// <summary>
        ///no loss of rights
        /// </summary>

        public bool IsContractActive { get; set; }



        //public bool IsSyncRigts { get; set; }


        public bool IsMac { get; set; }


        public bool IsMasterUse { get; set; }


        public bool IsDigitalRights { get; set; }


        public bool IsPhysicalRights { get; set; }


        public bool IsPrecleared { get; set; }


        public long ClearanceAdminCountryId { get; set; }


        public long RightSetId { get; set; }


        //Setting default value as true for IsMacExist
        private bool isMacExist = true;

        public bool IsMacExist
        {
            get
            {
                return isMacExist;
            }

            set
            {
                isMacExist = value;
            }
        }

        //Setting default value as true for IsMasterUseExist
        private bool isMasterUseExist = true;

        public bool IsMasterUseExist
        {
            get
            {
                return isMasterUseExist;
            }

            set
            {
                isMasterUseExist = value;
            }
        }


        //Setting default value as true for IsDigitalRightsExist
        private bool isDigitalRightsExist = true;

        public bool IsDigitalRightsExist
        {
            get
            {
                return isDigitalRightsExist;
            }

            set
            {
                isDigitalRightsExist = value;
            }
        }

        //Setting default value as true for IsPhysicalRightsExist
        private bool isPhysicalRightsExist = true;

        public bool IsPhysicalRightsExist
        {
            get
            {
                return isPhysicalRightsExist;
            }

            set
            {
                isPhysicalRightsExist = value;
            }
        }

        //Setting default value as true for IsPreclearedExist
        private bool isPreclearedExist = true;

        public bool IsPreclearedExist
        {
            get
            {
                return isPreclearedExist;
            }

            set
            {
                isPreclearedExist = value;
            }
        }

        #endregion

        [DataMember]
        public bool IsResourceContract { get; set; }

        public bool IsTopPriceCompilation { get; set; }
        public bool IsMidPriceCompilation { get; set; }
        public bool IsBudgetPriceCompilation { get; set; }
        public bool IsDirectMarketing { get; set; }
        public bool IsPremium { get; set; }
        public bool IsMasterSynchronizationUse { get; set; }
    }

}

