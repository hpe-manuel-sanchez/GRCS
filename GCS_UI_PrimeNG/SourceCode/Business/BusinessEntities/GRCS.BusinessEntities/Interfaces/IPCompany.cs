/* *************************--*******************************************
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IPCompany.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : PavanKumar Kota
 * Created on   : 11-Jan-2013 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 * 
 ***********************************************************************************
 * Description : operations for PCompanies
 ***********************************************************************************/

using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IPCompany
    {
        /// <summary>
        /// Gets the pc notice companies from R2.
        /// </summary>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        [OperationContract]
        NoticeCompanySearchResult GetPcNoticeCompaniesFromR2(NoticeCompanySearch searchOption);
    }
}
