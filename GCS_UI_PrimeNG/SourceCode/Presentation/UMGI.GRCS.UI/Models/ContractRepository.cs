using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMGI.GRCS.UI.Rights.Interfaces;
using UMGI.GRCS.Entities.ContractEntities;
using UMGI.GRCS.Entities.ArtistEntities;
using UMGI.GRCS.Entities.NoticeCompanyEntities;
using UMGI.GRCS.Entities.Templates.GlobalAddressEntities;
using UMGI.GRCS.Entities.Entities.ContractEntities;

namespace UMGI.GRCS.UI.Rights.Models
{
    public class ContractRepository : IContractRepository
    {
        public ContractRepository()
        {
            ObjContractRepository = new ContractCreationManager();
        }

        public IContractCreationManager ObjContractRepository { get; set; }

        public List<Contract> SearchParentContract(string artistName, string localContractNumber, string contractingParty, long contractId, int startIndex = 0, int pageSize = 0, string sortField = null)
        {
            try
            {
                //TODO:
                var temp = new List<Contract>();
                temp.Add(new Contract() { ContractDescription = "test desc", ContractId = 0001, ContractingParty = "test party", ClearanceCompanyCountryId = 1, LocalContractRefNumber = "fdf" });
                //return temp
                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ArtistSearchResult> SearchForArtist(string artistname, string artistfname, string artistlname, long artistid = 0, bool flag = false, int startIndex = 0, int pageSize = 0, string sortField = null)
        {
            try
            {
                var temp = new List<ArtistSearchResult>();
                temp.Add(new ArtistSearchResult() { ArtistId = 100355, ArtistFirstName = "Ajitha", ArtistLastName = "Ramakrishnan", Prefix = "test", RolesPerformed = "Admin", DataAdminCompany = "UMG", AliasIndicator = "fo", AdditonalInfo = "This is a test" });

                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NoticeCompany> SearchPCNoticeCompany(string CompanyName, string CountryName, int startIndex = 0, int pageSize = 0, string sortField = null)
        {
            try
            {
                var temp = new List<NoticeCompany>();
                temp.Add(new NoticeCompany() { CompanyName = "UMG", CountryName = "US", AdditionalNotes = "This is a test" });

                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GlobalAddress> GlobalAddressList(string SearchValue, long SearchCategoryId = 0, int startIndex = 0, int pageSize = 0, string sortField = null)
        {
            try
            {
                var temp = new List<GlobalAddress>();
                temp.Add(new GlobalAddress() { EmailAddress = "umg@gmail.com", Location = "UK", Title = "This is a test" });

                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ContractSearch> SearchContract(string ArtistName, string WorkFlowStatus, string ContractStatus, int startIndex = 0, int pageSize = 0, string sortField = null)
        {
            try
            {
                var temp = new List<ContractSearch>();
                temp.Add(new ContractSearch() { ContractId = 1, ArtistName = "Satheesh", ContractingParty = "Sony", ContractName = "Alb", ContractCommencementDate = System.DateTime.Now, AdminCompany = "Test", SigningCompany = "UMG", LocalContractRefNumber = "tes", RightsTypeId = 12, ContractStatus = "Draft", WorkFlowStatus = "Pending Approval" });
                temp.Add(new ContractSearch() { ContractId = 2, ArtistName = "Parvath", ContractingParty = "Sony", ContractName = "Alb", ContractCommencementDate = System.DateTime.Now, AdminCompany = "Test", SigningCompany = "UMG", LocalContractRefNumber = "tes", RightsTypeId = 12, ContractStatus = "Draft", WorkFlowStatus = "Pending Approval" });
                temp.Add(new ContractSearch() { ContractId = 3, ArtistName = "Ramesh", ContractingParty = "Sony", ContractName = "Alb", ContractCommencementDate = System.DateTime.Now, AdminCompany = "Test", SigningCompany = "UMG", LocalContractRefNumber = "tes", RightsTypeId = 12, ContractStatus = "Draft", WorkFlowStatus = "Pending Approval" });
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ContractSearch> AdvancedSearchContract(string ContractingParty, string ClearanceAdminCompany, string ArtistNameInLocalCharacters, string UMGSigningCompanyId, string LocalContractRefNumber, DateTime ContractCommencementDate, DateTime ContractEndDate, long contractId, long RightsType, int startIndex = 0, int pageSize = 0, string sortField = null)
        {

            try
            {
                var temp = new List<ContractSearch>();
                temp.Add(new ContractSearch() { ContractId = 1, ArtistName = "Satheesh", ContractingParty = "Sony", ContractName = "Alb", ContractCommencementDate = System.DateTime.Now, AdminCompany = "Test", SigningCompany = "UMG", LocalContractRefNumber = "tes", RightsTypeId = 12, ContractStatus = "Draft", WorkFlowStatus = "Pending Approval" });
                temp.Add(new ContractSearch() { ContractId = 2, ArtistName = "Parvath", ContractingParty = "Sony", ContractName = "Alb", ContractCommencementDate = System.DateTime.Now, AdminCompany = "Test", SigningCompany = "UMG", LocalContractRefNumber = "tes", RightsTypeId = 12, ContractStatus = "Draft", WorkFlowStatus = "Pending Approval" });
                temp.Add(new ContractSearch() { ContractId = 3, ArtistName = "Ramesh", ContractingParty = "Sony", ContractName = "Alb", ContractCommencementDate = System.DateTime.Now, AdminCompany = "Test", SigningCompany = "UMG", LocalContractRefNumber = "tes", RightsTypeId = 12, ContractStatus = "Draft", WorkFlowStatus = "Pending Approval" });
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<StringListEntity> AutoSearchClearanceCompCountry(string searchTerm)
        {
            List<StringListEntity> searchList = new List<StringListEntity>();
            searchList.Add(new StringListEntity(1, "JB Productions, Great Britain"));
            searchList.Add(new StringListEntity(2, "abb"));
            searchList.Add(new StringListEntity(3, "abc"));
            searchList.Add(new StringListEntity(4, "bbb"));
            searchList.Add(new StringListEntity(5, "ccc"));
            searchList.Add(new StringListEntity(6, "ddd"));
            searchList.Add(new StringListEntity(7, "aabb"));
            searchList.Add(new StringListEntity(8, "ee"));
            searchList.Add(new StringListEntity(9, "gggaaaaa"));
            searchList.Add(new StringListEntity(10, "hhhaaaaa"));
            searchList.Add(new StringListEntity(11, "jjaaaaa"));
            searchList.Add(new StringListEntity(12, "kkkaaaaa"));
            searchList.Add(new StringListEntity(13, "ll"));
            searchList.Add(new StringListEntity(14, "mmm"));
            searchList.Add(new StringListEntity(15, "nnn"));
            searchList.Add(new StringListEntity(16, "aaoooaaa"));

            return new List<StringListEntity>(searchList.Where(a => a.Value.Contains(searchTerm)));
        }

        public List<StringListEntity> AutoSearchContractDesc(string searchTerm)
        {
            List<StringListEntity> searchList = new List<StringListEntity>();
            searchList.Add(new StringListEntity(1, "Contract Description 1"));
            searchList.Add(new StringListEntity(2, "abb"));
            searchList.Add(new StringListEntity(3, "abc"));
            searchList.Add(new StringListEntity(4, "bbb"));
            searchList.Add(new StringListEntity(5, "ccc"));
            searchList.Add(new StringListEntity(6, "ddd"));
            searchList.Add(new StringListEntity(7, "aabb"));
            searchList.Add(new StringListEntity(8, "ee"));
            searchList.Add(new StringListEntity(9, "gggaaaaa"));
            searchList.Add(new StringListEntity(10, "hhhaaaaa"));
            searchList.Add(new StringListEntity(11, "jjaaaaa"));
            searchList.Add(new StringListEntity(12, "kkkaaaaa"));
            searchList.Add(new StringListEntity(13, "ll"));
            searchList.Add(new StringListEntity(14, "mmm"));
            searchList.Add(new StringListEntity(15, "nnn"));
            searchList.Add(new StringListEntity(16, "aaoooaaa"));

            return new List<StringListEntity>(searchList.Where(a => a.Value.Contains(searchTerm)));
        }

        public List<StringListEntity> AutoSearchPCCompanyCountry(string searchTerm)
        {
            List<StringListEntity> searchList = new List<StringListEntity>();
            searchList.Add(new StringListEntity(1, "JB Productions, Great Britain"));
            searchList.Add(new StringListEntity(2, "abb"));
            searchList.Add(new StringListEntity(3, "abc"));
            searchList.Add(new StringListEntity(4, "bbb"));
            searchList.Add(new StringListEntity(5, "ccc"));
            searchList.Add(new StringListEntity(6, "ddd"));
            searchList.Add(new StringListEntity(7, "aabb"));
            searchList.Add(new StringListEntity(8, "ee"));
            searchList.Add(new StringListEntity(9, "gggaaaaa"));
            searchList.Add(new StringListEntity(10, "hhhaaaaa"));
            searchList.Add(new StringListEntity(11, "jjaaaaa"));
            searchList.Add(new StringListEntity(12, "kkkaaaaa"));
            searchList.Add(new StringListEntity(13, "ll"));
            searchList.Add(new StringListEntity(14, "mmm"));
            searchList.Add(new StringListEntity(15, "nnn"));
            searchList.Add(new StringListEntity(16, "aaoooaaa"));

            return new List<StringListEntity>(searchList.Where(a => a.Value.Contains(searchTerm)));
        }                
    }
}
