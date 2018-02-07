using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using UMGI.GRCS.BusinessEntities.Constants;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.Core.Utilities.Helper;

namespace UMGI.GRCS.Data.Entities.MasterDataEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class MasterDataEntity
    {
        private const string NoArchiveFlag = "N";

        /// <summary> 
        /// Gets the master data. 
        /// </summary> 
        /// <returns></returns> 
        public static List<Lookup> GetMasterData()
        {
            using (var masterData = new MasterDataEntities(ConfigUtil.GetConnectionString("MasterDataEntities")))
            {
                List<Lookup> lstLookUp =
                    (from lookup in masterData.Lookups.Include("Lookup_Type")
                     where lookup.Archive_Flag ==
                         NoArchiveFlag
                     select lookup).ToList();

                return lstLookUp;
            }
        }



        /// <summary>
        /// Gets the territory data.
        /// </summary>
        /// <returns></returns>
        public static List<TerritorialDisplay> GetTerritories()
        {
            using (var masterData = new MasterDataEntities(ConfigUtil.GetConnectionString("MasterDataEntities")))
            {
                //TODO:Hard coded value

                List<TerritorialDisplay> lstTerritoryQuery = (from territory in masterData.Territories
                                                              join territoryMap in masterData.Territory_Area
                                                              on territory.Territory_Id equals territoryMap.Territory_Member
                                                              where territory.Gta_Status == "A" && territoryMap.Gta_Status == "A" && territory.Archive_Flag == NoArchiveFlag
                                                              select new TerritorialDisplay
                                                              {
                                                                  Id = territory.Territory_Id,
                                                                  Name = territory.Territory_Name,
                                                                  ParentId = territoryMap.Territory_Area_Id,
                                                                  IsTerritory = true
                                                              }).OrderBy(ordCountry => ordCountry.Name).ToList();

                lstTerritoryQuery.Add(new TerritorialDisplay() { Id = 2, Name = "Universe", ParentId = -1, IsTerritory = true });

                List<TerritorialDisplay> temp = (from territory in lstTerritoryQuery
                                                 join territoryCountry in masterData.Territory_Country on
                                                     territory.Id equals territoryCountry.Territory_Id
                                                 join country in masterData.Countries on territoryCountry.Country_Id
                                                     equals country.Country_Id
                                                 where country.Gta_Status == "A" && territoryCountry.Gta_Status == "A" && country.Archive_Flag == NoArchiveFlag
                                                 select new TerritorialDisplay
                                                 {
                                                     Id = territoryCountry.Country_Id,
                                                     ParentId = territoryCountry.Territory_Id,
                                                     Name = country.Country_Name
                                                 }).OrderBy(ordCountry => ordCountry.Name).ToList();




                var duptemp = temp.GroupBy(i => new { i.Id, i.ParentId }).Select(j => j.FirstOrDefault()).ToList();
                lstTerritoryQuery.AddRange(duptemp);

                return lstTerritoryQuery.OrderBy(p => p.Id).ToList();

            }
        }
        /// <summary>
        /// Gets the name of the lookup type idby.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static long GetLookupTypeIdbyName(string description, string type)
        {
            using (var masterData = new MasterDataEntities(ConfigUtil.GetConnectionString(Constants.MasterDataEntities)))
            {

                masterData.Lookup_Type.MergeOption = MergeOption.NoTracking;
                masterData.Lookups.MergeOption = MergeOption.NoTracking;


                var lookupTypeId = (masterData.Lookup_Type.Where(lookUpType => lookUpType.Type == type).Select(
                    lookUpType => lookUpType.Lookup_Type_Id)).FirstOrDefault();
                var result = (masterData.Lookups.Where(
                    lookup => lookup.Description == description && lookup.Lookup_Type_Id == lookupTypeId).Select(
                        lookup => lookup.Value)).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// Method to get the companies associated with
        ///     a territorial country
        /// </summary>
        /// <param name="territorialCountryId">instance of
        /// territorial country to get <see cref="GetAdminCompaniesOfTerritory"/></param>
        /// <returns></returns>
        public static List<RightsCompany>
            GetAdminCompaniesOfTerritory(long territorialCountryId)
        {
            using (var masterData = new MasterDataEntities
                (ConfigUtil.GetConnectionString(Constants.MasterDataEntities)))
            {
                masterData.Companies.MergeOption=MergeOption.NoTracking;
               
                    
                List<RightsCompany> companiesList = (from companies in masterData.Companies
                                                     where companies.Country_Id == territorialCountryId
                                                     select new RightsCompany
                                                     {
                                                         Id = companies.Company_Id,
                                                         Name = companies.Name
                                                     }).ToList();
                return companiesList;
            }
        }

        /// <summary>
        /// Gets the name of the lookup type idby.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static long GetLookupTypeIdbyShortName(string description, string type)
        {
            using (var masterData = new MasterDataEntities(ConfigUtil.GetConnectionString(Constants.MasterDataEntities)))
            {
                masterData.Lookup_Type.MergeOption = MergeOption.NoTracking;
                masterData.Lookups.MergeOption = MergeOption.NoTracking;


                var lookUpTypeId = (masterData.Lookup_Type.Where(lookUpType => lookUpType.Type == type).Select(
                    lookUpType => lookUpType.Lookup_Type_Id)).FirstOrDefault();
                var result = (masterData.Lookups.Where(
                    lookup => lookup.Short_Description == description && lookup.Lookup_Type_Id == lookUpTypeId).Select(
                        lookup => lookup.Value)).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// Gets the name of the lookup.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetLookupName(byte? value, string type)
        {
            using (var masterData = new MasterDataEntities(ConfigUtil.GetConnectionString(Constants.MasterDataEntities)))
            {
                masterData.Lookup_Type.MergeOption=MergeOption.NoTracking;
                masterData.Lookups.MergeOption=MergeOption.NoTracking;

                var lookUpTypeId = (masterData.Lookup_Type.Where(lookUpType => lookUpType.Type == type).Select(
                    lookUpType => lookUpType.Lookup_Type_Id)).FirstOrDefault();
                var result = (masterData.Lookups.Where(
                    lookup => lookup.Value == value && lookup.Lookup_Type_Id == lookUpTypeId).Select(
                        lookup => lookup.Description)).FirstOrDefault();
                return result;
            }
        }
    }
}
