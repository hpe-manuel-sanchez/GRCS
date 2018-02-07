/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IPCompanyData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vijay R
 * Created on   : 03-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Pavan Kumar       30.10.2012      Header Added, Code Refactored
 * KrishnaPrasad     29/11/2012      UpdatePCompany added
 * Pavan Kumar      11/01/2012    Added LoadPcNoticeCompanies 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      

****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IPCompanyData
    {
        /// <summary>
        /// Updates the P company.
        /// </summary>
        /// <param name="noticeCompany">The notice company.</param>
        /// <param name="notificationType">Type of the notification.</param>
        /// <returns></returns>
        long UpdatePCompany(NoticeCompany noticeCompany, string notificationType);

        /// <summary>
        /// Loads the PC notice companies from DB
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns></returns>
        List<NoticeCompany> LoadPcNoticeCompanies(string userInfo);

        /// <summary>
        /// Saves the pc notice company to DB.
        /// </summary>
        /// <param name="noticeCompany">The notice company.</param>
        void SavePcNoticeCompany(NoticeCompany noticeCompany);
    }
}
