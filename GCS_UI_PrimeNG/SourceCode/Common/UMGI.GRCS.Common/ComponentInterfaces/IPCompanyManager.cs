/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IPCompanyManager.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vijay R
 * Created on   : 03-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Pavan Kumar       30.10.2012      Header Added, Code Refactored
 * KrishnaPrasad     29/11/2012      Interface method added for 
 *                                   UpdatePCompany
 * Pavan Kumar      11/01/2012    Added GetPcNoticeCompanies 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      

****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IPCompanyManager
    {
        /// <summary>
        /// Updates the P company.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="uniqueId">The unique id.</param>
        /// <returns></returns>
        long UpdatePCompany(string xmlData,long uniqueId);

        /// <summary>
        /// Gets the PC notice companies from DB
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns></returns>
        List<NoticeCompany> GetPcNoticeCompanies(string userInfo);

        /// <summary>
        /// Gets the PC notice companies from R2.
        /// </summary>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        NoticeCompanySearchResult GetPcNoticeCompaniesFromR2(NoticeCompanySearch searchOption);

        /// <summary>
        /// Saves the pc notice company to DB
        /// </summary>
        /// <param name="noticeCompany">The notice company.</param>
        void SavePcNoticeCompany(NoticeCompany noticeCompany);
    }
}