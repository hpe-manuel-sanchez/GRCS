using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.UI.Rights.Models;
using UMGI.GRCS.Entities.ArtistEntities;
using UMGI.GRCS.Entities.ContractEntities;
using UMGI.GRCS.Entities.NoticeCompanyEntities;
using UMGI.GRCS.Entities.Templates.GlobalAddressEntities;
using UMGI.GRCS.Entities.Entities.ContractEntities;

namespace UMGI.GRCS.UI.Rights.Interfaces
{
    public interface IContractRepository
    {
        IContractCreationManager ObjContractRepository { get; set; }

        List<Contract> SearchParentContract(string artistName, string localContractNumber, string contractingParty, long contractId, int startIndex = 0, int pageSize = 0, string sortField = null);

        List<ArtistSearchResult> SearchForArtist(string artistname, string artistfname, string artistlname, long artistid = 0, bool flag = false, int startIndex = 0, int pageSize = 0, string sortField = null);
        List<NoticeCompany> SearchPCNoticeCompany(string CompanyName, string Country, int startIndex = 0, int pageSize = 0, string sortField = null);
        List<GlobalAddress> GlobalAddressList(string SearchValue,long SearchCategoryId=0,int startIndex = 0, int pageSize = 0, string sortField = null);
        List<ContractSearch> SearchContract(string ArtistName, string WorkflowStatus, string ContractStatus, int startIndex = 0, int pageSize = 0, string sortField = null);
        List<ContractSearch> AdvancedSearchContract(string ContractingParty, string ClearanceAdminCompany, string ArtistNameInLocalCharacters, string UMGSigningCompanyId, string LocalContractRefNumber, DateTime ContractCommencementDate, DateTime ContractEndDate, long contractId, long RightsType, int startIndex = 0, int pageSize = 0, string sortField = null);

        List<StringListEntity> AutoSearchClearanceCompCountry(string searchTerm);
        List<StringListEntity> AutoSearchContractDesc(string searchTerm);
        List<StringListEntity> AutoSearchPCCompanyCountry(string searchTerm);
    }
}
