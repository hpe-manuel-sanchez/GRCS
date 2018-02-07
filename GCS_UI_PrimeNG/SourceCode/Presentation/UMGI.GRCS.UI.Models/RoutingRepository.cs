/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RoutingRepository.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * R.MuthuKumar      02/27/2013      Initial Creation
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Interfaces;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.Core.Utilities.logger;
namespace UMGI.GRCS.UI.Models
{
    public class RoutingRepository : IRoutingRepository
    {
        #region Variable Declarations

        #region Private Variables

        /// <summary>
        /// Declaration of readonly IServiceFactory used to intialize the service
        /// </summary>
        readonly IServiceFactory _serviceFactory;

        /// <summary>
        /// Declaration of readonly IRouting used to Get all workgroup service contracts
        /// </summary>
        readonly IRouting _routingService;

        /// <summary>
        /// Used to log the given details
        /// </summary>
        readonly ILogFactory _logFactory;

		private readonly IArtist _artistService;

        #endregion

        #region Properties

        public ISessionWrapper SessionWrapper { get; set; }
        public IConfigFactory ConfigurationFactory { get; set; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RoutingRepository class.
        /// </summary>
        public RoutingRepository(IServiceFactory serviceFactory, IConfigFactory configFactory, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            try
            {
                _logFactory = logFactory;
                _serviceFactory = serviceFactory;
                //Intialize the routing service
                _routingService = _serviceFactory.GetService<IRouting>(Constants.RoutingService);
				_artistService = _serviceFactory.GetService<IArtist>(Constants.ArtistService);
                SessionWrapper = sessionWrapper;
                ConfigurationFactory = configFactory;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "RoutingRepository"), ex);
                throw;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get safe territory list
        /// </summary>
        /// <returns>Collection of territories</returns>
        public List<TerritorialDisplay> GetSafeAreaTerritory()
        {
            try
            {
                return _routingService.GetSafeAreaTerritory();
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetSafeAreaTerritory"), ex);
                throw;
            }
        }

        /// <summary>
        /// Add/Update safe territories
        /// </summary>
        /// <param name="territories">The territories</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>True if successfully add/update, otherwise false</returns>
        public  bool AddSafeTerritories(List<TerritorialDisplay> territories, UserInfo userInfo)
        {
            try
            {
                return _routingService.AddSafeTerritories(territories, userInfo);
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "AddSafeTerritories"), ex);
                throw;
            }
        }

		/// <summary>
		/// Get safe territory list
		/// </summary>
		/// <returns>Collection of territories</returns>
		public List<RoutingRule> GetRoutingRules()
		{
			try
			{
				return _routingService.GetRoutingRules();
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetRoutingRules"), ex);
				throw;
			}
		}
		/// <summary>
		/// Get safe territory list
		/// </summary>
		/// <param name="ruleId">Routing Rule id</param>
		/// <returns>Result as RoutingRuleVariation</returns>
		public RoutingRuleVariation LoadRuleVariations(long ruleId)
		{
			try
			{
				return _routingService.LoadRuleVariations(ruleId);
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "LoadRuleVariations"), ex);
				throw;
			}
		}

		/// <summary>
		/// Change the Rulr or Variation Status
		/// </summary>
		/// <param name="ruleId">Routing Rule id</param>
		/// <param name="statusType">Rule or Variation Status Type</param>
		/// <param name="objectType">Rule or Variation</param>
		/// <returns>Result as True or False</returns>
		public string ChangeRuleStatus(long Id, StatusType statusType, ObjectType objectType, string userLoginName)
		{
			try
			{
				return _routingService.ChangeRuleStatus(Id, statusType, objectType, userLoginName);
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "ChangeRuleStatus"), ex);
				throw;
			}
		}

		/// <summary>
		/// Change the Rulr or Variation Status
		/// </summary>
		/// <param name="ruleVariationInfo">New Rule and Variation Information</param>
		/// <returns>Result as Rule Id</returns>
		public long SaveRuleAndVariation(RoutingRuleSaveInfo ruleVariationInfo)
		{
			try
			{
				return _routingService.SaveRuleAndVariation(ruleVariationInfo);
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "SaveRuleAndVariation"), ex);
				throw;
			}
		}

		/// <summary>
		/// Load Variation Territories
		/// </summary>
		/// <param name="variationID">RuleVariation Id</param>
		/// <param name="territoryType">territory Type</param>
		/// <returns>Result as List of Territory Display</returns>
		public List<TerritorialDisplay> LoadTerritories(long variationID, TerritoryType territoryType)
		{
			try
			{
				return _routingService.LoadTerritories(variationID, territoryType);
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "LoadTerritories"), ex);
				throw;
			}
		}

		/// <summary>
		/// Load Variation Territories
		/// </summary>
		/// <param name="variationID">RuleVariation Id</param>
		/// <param name="territoryType">territory Type</param>
		/// <returns>Result as List of Territory Display</returns>
		public List<KeyValuePair<byte, string>> GetRoutingVariationRequestTypes()
		{
			try
			{
				return _routingService.GetRoutingVariationRequestTypes();
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetRoutingVariationRequestTypes"), ex);
				throw;
			}
		}

		/// <summary>
		/// Search for Artist
		/// </summary>
		/// <param name="searchOption">Search Parameters</param>
		/// <returns></returns>
		public ArtistSearchResult SelectMultiArtist(ArtistSearchCriteria searchOption, bool artistQualifyingCriteria)
		{
			try
			{
				ArtistSearchResult artists = _artistService.SearchArtist(searchOption, artistQualifyingCriteria);
				return artists;
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "SelectMultiArtist"), ex);
				throw;
			}
		}
		/// <summary>
		/// Search for Artist
		/// </summary>
		/// <param name="searchOption">Search Parameters</param>
		/// <returns></returns>
		public List<ArtistInfo> GetArtists(string artistIds)
		{
			try
			{
				return _routingService.GetArtists(artistIds);
			}
			catch (Exception ex)
			{
				_logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetArtists"), ex);
				throw;
			}
		}    
        #endregion
    }
}
