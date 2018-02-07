/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MPReleaseResourceAssociaton.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : G Senthil Kumar
 * Created on   : 15-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                                  
*************************************************************************** 
 * Reviewed by      Modified on     Remarks 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using WSMediaPortalProxy;


namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// This is an extension class for MPReleaseRights and contains the functionality for explicit conversion
    /// </summary>
    /// <remarks>The Explicit conversion is done for the proxy object "ReleaseRights"</remarks>
    public partial class MPReleaseRights
    {
        /// <summary>
        /// Parses the resource rights expiry date.
        /// </summary>
        /// <param name="rightsExpiryDate">The rights expiry date.</param>
        /// <returns></returns>
        private static DateTime? ParseResourceRightsExpiryDate(string rightsExpiryDate)
        {
            if (!String.IsNullOrEmpty(rightsExpiryDate))
            {
                var dateTimeSplit = rightsExpiryDate.Split(Constants.Constants.WhiteSpaceChar);
                if (dateTimeSplit.Any())
                {
                    var dateSplit =
                        dateTimeSplit[0].Split(Constants.Constants.MediaPortalRightsExpiryDateSeperator);
                    if (dateSplit.Count() == 3)
                    {
                        var expiryDateOfResource = new DateTime(Convert.ToInt32(dateSplit[2]),
                                                                Convert.ToInt32(dateSplit[0]),
                                                                Convert.ToInt32(dateSplit[1]));
                        return expiryDateOfResource;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Gets the earliest expiry date.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <returns></returns>
        private static DateTime? GetEarliestExpiryDate(IEnumerable<Resource> resources)
        {
            DateTime?[] earliestExpiryDate = { DateTime.MaxValue };
            if (resources != null)
            {
                foreach (
                    var expiryDateOfResource in
                        resources.Select(resource => ParseResourceRightsExpiryDate(resource.RightsExpiryDate)).Where(
                            expiryDateOfResource => expiryDateOfResource != null).Where(
                                expiryDateOfResource => expiryDateOfResource < earliestExpiryDate[0]))
                {
                    earliestExpiryDate[0] = expiryDateOfResource;
                }
            }
            return earliestExpiryDate[0] == DateTime.MaxValue ? null : earliestExpiryDate[0];
        }


        /// <summary>
        /// Converts "ReleaseRights" to "MPReleaseRights"
        /// </summary>
        /// <param name="releaseRightsList">Object to be converted</param>
        /// <param name="upc">the UPC</param>
        /// <returns>Converted "MPReleaseRights" object</returns>
        public static MPReleaseRights GetMPReleaseRights(ReleaseRights releaseRightsList, string upc)
        {
            if (releaseRightsList == null || releaseRightsList.Releases == null)
                return null;


            return (from releaseRights in releaseRightsList.Releases
                    where releaseRights.UPC == upc
                    select new MPReleaseRights
                               {
                                   Upc = releaseRights.UPC,
                                   ExclusiveInfo = releaseRights.ExclusiveInfo,
                                   AccountId = releaseRights.AccountNumber,
                                   CompanyId = releaseRights.CompanyId,
                                   DivisionId = releaseRights.DivisionId,
                                   LabelId = releaseRights.LabelId,
                                   Grid = releaseRights.GRid,
                                   CanDeliveryToYouTube = releaseRights.DeliverToYouTubeVevo,
                                   ResourceRights =
                                       GetMPResourceRights(releaseRights.Resources, releaseRightsList.Rights),
                                   RightsExpiryDate = GetEarliestExpiryDate(releaseRights.Resources),
                                   RightsRestrictions = GetRightsRestrictionsOfResources(GetMPResourceRights(releaseRights.Resources, releaseRightsList.Rights)),
                               }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the rights restrictions of resources.
        /// </summary>
        /// <param name="resourceRights">The resource rights.</param>
        /// <returns></returns>
        public static List<MPRightsRestrictions> GetRightsRestrictionsOfResources(List<MPResourceRights> resourceRights)
        {
            var rightsRestrictions = new List<MPRightsRestrictions>();
            foreach (var mpResourceRights in resourceRights)
            {
                rightsRestrictions.AddRange(mpResourceRights.RightsRestrictions);
            }
            return MPRollUpRestrictions.GetRolledUpRestrictions(rightsRestrictions);
        }

        /// <summary>
        /// Gets associated Resource Rights collection of the Release
        /// </summary>
        /// <param name="resources">Actual Resource object array from Media Portal</param>
        /// <param name="rights">Rights set Definition</param>
        /// <returns>MPResourceRightsCollection</returns>
        private static List<MPResourceRights> GetMPResourceRights(ICollection<Resource> resources, Right[] rights)
        {
            if (resources == null || resources.Count <= 0)
                return null;

            return resources.Select(resource => new MPResourceRights
                                                    {
                                                        ExpiryDate =
                                                            ParseResourceRightsExpiryDate(resource.RightsExpiryDate),
                                                        Isrc = resource.ISRC,
                                                        ResourceType =
                                                            (MPResourceType)
                                                            Enum.Parse(typeof(MPResourceType), resource.Type.ToString(),
                                                                       true),
                                                        RightsRestrictions =
                                                            GetRightsRestrictions(resource.ReleaseTerritories, resource.ResourceTerritories, rights),
                                                        CountriesAllowedForStreaming =
                                                            GetCountriesAllowedForStreaming(resource.ResourceTerritories)
                                                    }).ToList();
        }

        /// <summary>
        /// Gets associated Track Rights from ReleaseTerritory
        /// </summary>
        /// <param name="releaseTerritories">Actual ReleaseTerritory array</param>
        /// <param name="resourceTerritories"> </param>
        /// <param name="rights">Rights set Definition</param>
        /// <returns>MPTrackRightsCollection</returns>
        private static List<MPRightsRestrictions> GetRightsRestrictions(
            ICollection<ReleaseTerritory> releaseTerritories, ICollection<ResourceTerritory> resourceTerritories,
            Right[] rights)
        {
            var rightsRestrictions = new List<MPRightsRestrictions>();
            if (releaseTerritories == null || releaseTerritories.Count == 0)
                return rightsRestrictions;

            rightsRestrictions.AddRange(releaseTerritories.Select(territory => new MPRightsRestrictions
                                                                                   {
                                                                                       CountryIsoCodes =
                                                                                           new List<string>
                                                                                               {
                                                                                                   territory.
                                                                                                       TerritoryISOCode
                                                                                               },
                                                                                       IsAlbumOnly =
                                                                                           territory.TerritoryRights != null && Convert.ToBoolean(
                                                                                               territory.TerritoryRights
                                                                                                   .Count(
                                                                                                       right =>
                                                                                                       right.RightId ==
                                                                                                       GetRightId(
                                                                                                           Constants.
                                                                                                               Constants
                                                                                                               .
                                                                                                               AlbumOnly,
                                                                                                           ref rights))),
                                                                                       IsDownload =
                                                                                           territory.TerritoryRights != null && Convert.ToBoolean(
                                                                                               territory.TerritoryRights
                                                                                                   .Count(
                                                                                                       right =>
                                                                                                       right.RightId ==
                                                                                                       GetRightId(
                                                                                                           Constants.
                                                                                                               Constants
                                                                                                               .Download,
                                                                                                           ref rights))),
                                                                                       IsStreamable =
                                                                                           territory.TerritoryRights != null && Convert.ToBoolean(
                                                                                               territory.TerritoryRights
                                                                                                   .Count(
                                                                                                       right =>
                                                                                                       right.RightId ==
                                                                                                       GetRightId(
                                                                                                           Constants.
                                                                                                               Constants
                                                                                                               .
                                                                                                               Streamable,
                                                                                                           ref rights))),
                                                                                       IsMakeOwnRingtone =
                                                                                           territory.TerritoryRights != null && Convert.ToBoolean(
                                                                                               territory.TerritoryRights
                                                                                                   .Count(
                                                                                                       right =>
                                                                                                       right.RightId ==
                                                                                                       GetRightId(
                                                                                                           Constants.
                                                                                                               Constants
                                                                                                               .
                                                                                                               MakeOwnRingtone,
                                                                                                           ref rights))),
                                                                                       IsAddSupportedStraming = SetAddSupportedStraming(resourceTerritories, territory.TerritoryISOCode)
                                                                                   }));

            return MPRollUpRestrictions.GetRolledUpRestrictions(rightsRestrictions);
        }

        private static bool SetAddSupportedStraming(ICollection<ResourceTerritory> resourceTerritories, string territoryIsoCode)
        {
            if (resourceTerritories == null)
            {
                return true;
            }

            YouTubeVevoRight youTube = (from obj in resourceTerritories
                                        where obj.YouTubeVevoRights != null &&
                                            obj.TerritoryISOCode == territoryIsoCode
                                        select obj.YouTubeVevoRights).FirstOrDefault();

            if (youTube != null)
            {
                return Convert.ToBoolean(youTube.AdSupportedStreaming);
            }
            return true;
        }

        /// <summary>
        /// Get associated RightsID from the defined rights set
        /// </summary>
        /// <param name="rightDescription">Rights Description</param>
        /// <param name="rights">Reference rights set array</param>
        /// <returns>int</returns>
        private static int GetRightId(string rightDescription, ref Right[] rights)
        {
            var rightId = 0;

            if (rights != null && rights.Length > 0)
                rightId = (from right in rights
                           where right.Name.ToUpper().Equals(rightDescription.ToUpper())
                           select right.Id).Single();

            return rightId;
        }


        /// <summary>
        /// Gets the countries allowed for streaming.
        /// </summary>
        /// <param name="resourceTerritories">The resource territories.</param>
        /// <returns></returns>
        private static List<string> GetCountriesAllowedForStreaming(
            ICollection<ResourceTerritory> resourceTerritories)
        {
            var countryList = new List<string>();

            if (resourceTerritories == null || resourceTerritories.Count == 0)
                return countryList;


            countryList.AddRange(from territory in resourceTerritories
                                 where territory.YouTubeVevoRights != null
                                 where territory.YouTubeVevoRights.AdSupportedStreaming
                                 select territory.TerritoryISOCode);


            return countryList;
        }
    }
}