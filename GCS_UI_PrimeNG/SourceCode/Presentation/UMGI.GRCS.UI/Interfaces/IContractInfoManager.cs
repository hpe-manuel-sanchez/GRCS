using System;
using System.Collections.Generic;
using UMGI.GRCS.Entities.ContractEntities;
using UMGI.GRCS.Entities.NoticeCompanyEntities;

namespace UMGI.GRCS.UI.Rights.Interfaces
{
    public interface IContractInfoManager
    {
        Contract NewContract { get; set; }
        //IEnumerable<NoticeCompany> SelectCountryLists { get; set; }
       

    }
}
