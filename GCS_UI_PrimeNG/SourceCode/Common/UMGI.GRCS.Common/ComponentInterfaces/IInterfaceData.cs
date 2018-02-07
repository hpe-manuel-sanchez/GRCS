/* ************************************************************************
 * Copyrights � 2012 UMGI
 * ************************************************************************
 * File Name    : IInterfaceData.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : Senthil Kumar G
 * Created on   : 28-11-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks
***************************************************************************
 * Reviewed by       Modified on     Remarks
****************************************************************************/

using System;
using UMGI.GRCS.BusinessEntities.Constants;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IInterfaceData
    {
        /// <summary>
        /// Updates the notification item id.
        /// </summary>
        /// <param name="interfaceLogId">The interface log id.</param>
        /// <param name="itemId">The item id.</param>
        /// <param name="userName">Name of the user.</param>
        void UpdateNotificationItemId(long interfaceLogId, long itemId, string userName);

        /// <summary>
        /// Gets the releases.
        /// </summary>
        /// <param name="searchCriteria">The search criteria. Defaulted for GRS</param>
        /// <returns></returns>
        ReleaseSearchResult GetReleases(FilterFields searchCriteria);

        /// <summary>
        /// Gets the releases.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="applyQualifyingCriteria">if set to <c>true</c> [apply qualifying criteria]. Defaulted to GRS</param>
        /// <returns></returns>
        ReleaseSearchResult GetReleases(FilterFields searchCriteria, bool applyQualifyingCriteria);

        /// <summary>
        /// Gets the releases.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="applyQualifyingCriteria">if set to <c>true</c> [apply qualifying criteria].</param>
        /// <param name="applicationType">Type of the application.</param>
        /// <returns></returns>
        ReleaseSearchResult GetReleases(FilterFields searchCriteria, bool applyQualifyingCriteria, AnaTargetApplication applicationType);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="searchCriteria">The search criteria. Defaulted for GRS</param>
        /// <returns></returns>
        ProjectSearchResult GetProjects(FilterFields searchCriteria);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="searchCriteria">The search criteria. Defaulted for GRS</param>
        /// <param name="applyQualifyingCriteria">if set to <c>true</c> [apply qualifying criteria].</param>
        /// <returns></returns>
        ProjectSearchResult GetProjects(FilterFields searchCriteria, bool applyQualifyingCriteria);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="applyQualifyingCriteria">if set to <c>true</c> [apply qualifying criteria].</param>
        /// <param name="applicationType">Type of the application.</param>
        /// <returns></returns>
        ProjectSearchResult GetProjects(FilterFields searchCriteria, bool applyQualifyingCriteria, AnaTargetApplication applicationType);

        /// <summary>
        /// Gets the artists.
        /// </summary>
        /// <param name="searchCriteria">The search criteria. Defaulted to GRS specific Qualifying criteria</param>
        /// <returns></returns>
        ArtistSearchResult GetArtists(FilterFields searchCriteria);

        /// <summary>
        /// Gets the artists.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="applicationType">Type of the application.</param>
        /// <returns></returns>
        ArtistSearchResult GetArtists(FilterFields searchCriteria, AnaTargetApplication applicationType);

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        ResourceSearchResult GetResources(FilterFields searchCriteria);

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="applyQualifyingCriteria">if set to <c>true</c> [apply qualifying criteria] for GRS</param>
        /// <returns></returns>
        ResourceSearchResult GetResources(FilterFields searchCriteria, bool applyQualifyingCriteria);

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="applyQualifyingCriteria">if set to <c>true</c> [apply qualifying criteria].</param>
        /// <param name="applicationType">Type of the application.</param>
        /// <returns></returns>
        ResourceSearchResult GetResources(FilterFields searchCriteria, bool applyQualifyingCriteria, AnaTargetApplication applicationType);

        /// <summary>
        /// Gets the notice companies.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="applicationType"> </param>
        /// <returns></returns>
        NoticeCompanySearchResult GetNoticeCompanies(FilterFields searchCriteria, AnaTargetApplication applicationType);

        /// <summary>
        /// Creates the new interface log.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="interFaceType">Type of the inter face.</param>
        /// <param name="itemId"> </param>
        /// <returns>the primary key of the interface log</returns>
        long CreateNewInterfaceLog(string xml, string sourceSystem, string userName, DateTime dateTime, Constants.InterfaceLogEntryType interFaceType, long itemId);

        /// <summary>
        /// Creates the new interface log.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        long CreateNewInterfaceLog(string xml, string sourceSystem, string userName);
    }
}