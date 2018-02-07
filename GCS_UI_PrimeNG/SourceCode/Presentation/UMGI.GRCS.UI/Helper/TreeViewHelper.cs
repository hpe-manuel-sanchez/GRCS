/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Ramesh Johnson
 * Created on     : 24-07-2012 
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
using System.Linq;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Helper class for Territorial Rights
    /// </summary>
    public static class TreeViewHelper
    {
        /// <summary>
        /// Gets the country parent id.
        /// </summary>
        /// <param name="parentCountryLinks">The parent country links.</param>
        /// <returns></returns>
        public static long GetCountryParentId(IEnumerable<TerritorialDisplay> parentCountryLinks)
        {
            return parentCountryLinks.OrderBy(i => i.ParentId).Select(i => i.ParentId).FirstOrDefault();
        }

        /// <summary>
        /// Method to check whether country has children
        /// </summary>
        /// <param name="countryLinks">The country links.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static bool CountryHasChildren(IEnumerable<TerritorialDisplay> countryLinks, long id)
        {
            return countryLinks != null && countryLinks.Any(i => i.ParentId == id);
        }

        /// <summary>
        /// Gets the child country info.
        /// </summary>
        /// <param name="childLinks">The child links.</param>
        /// <param name="parentIdForChildren">The parent id for children.</param>
        /// <returns></returns>
        public static IEnumerable<TerritorialDisplay> GetChildCountryInfo(IEnumerable<TerritorialDisplay> childLinks, long parentIdForChildren)
        {
            return childLinks.Where(i => i.ParentId == parentIdForChildren)
                .OrderBy(i => i.ParentId).ThenBy(i => i.Name);
        }
    }

}