/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IRightsReviewWorkQueueRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Rubini
 * Created on     : 12-02-2013 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 * 
 * ***************************************************************************/
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.UI.Interfaces.RightsReviewWQ
{
    public interface IRightsReviewWorkQueueRepository
    {

        /// <summary>
        /// Loads the release rights WQ.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ReleaseRightsResult LoadReleaseRightsWQ(ReleaseFilterParameters filter);

        /// <summary>
        /// Loads the resource rights WQ.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourceRightsResult LoadResourceRightsWQ(ResourceFilterParameters filter);

        /// <summary>
        /// Saves the reviewed release rights.
        /// </summary>
        /// <param name="releaseRights">The release rights.</param>
        /// <returns></returns>
        List<long> SaveReviewedReleaseRights(ReleaseRightsSaveInfo releaseRights);

        /// <summary>
        /// Saves the reviewed resource rights.
        /// </summary>
        /// <param name="resourceRights">The resource rights.</param>
        /// <returns></returns>
        List<long> SaveReviewedResourceRights(ResourceRightsSaveInfo resourceRights);

        /// <summary>
        /// Saves the release digital rights.
        /// </summary>
        /// <param name="rights">The rights.</param>
        /// <param name="modifierInfo">The modifier info.</param>
        /// <returns></returns>
        List<long> SaveReleaseDigitalRights(List<ReleaseDigitalRights> rights, ModifierInfo modifierInfo);

        /// <summary>
        /// Saves the resource digital rights.
        /// </summary>
        /// <param name="rights">The rights.</param>
        /// <param name="modifierInfo">The modifier info.</param>
        /// <returns></returns>
        List<long> SaveResourceDigitalRights(List<ResourceDigitalRestrictions> rights, ModifierInfo modifierInfo);

        /// <summary>
        /// Saves the resource secondary rights.
        /// </summary>
        /// <param name="secondaryRightsSaveInfo">The secondary rights save info.</param>
        /// <returns></returns>
        List<long> SaveResourceSecondaryRights(SecondaryRightsSaveInfo secondaryRightsSaveInfo);

        /// <summary>
        /// Saves the resource pre clearance rights.
        /// </summary>
        /// <param name="rights">The rights.</param>
        /// <returns></returns>
        List<long> SaveResourcePreClearanceRights(PreClearanceSaveInfo rights);

        /// <summary>
        /// Loads the resource secondary rights.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourceSecondaryRightsResult LoadResourceSecondaryRights(ResourceFilterParameters filter);

        /// <summary>
        /// Loads the resource pre clearance info.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourcePreClearanceResult LoadResourcePreClearanceInfo(ResourceFilterParameters filter);

        /// <summary>
        /// Loads the release digital rights.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ReleaseDigitalRightsResult LoadReleaseDigitalRights(ReleaseFilterParameters filter);

        /// <summary>
        /// Loads the resource digital rights.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourceDigitalRightsResult LoadResourceDigitalRights(ResourceFilterParameters filter);

        /// <summary>
        /// Gets the acquired rights master data.
        /// </summary>
        /// <returns></returns>
        ReviewRightsMasterInfo GetAcquiredRightsMasterData();

        /// <summary>
        /// Loads the resource rights predefined WQ.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourceRightsResult LoadResourceRightsPredefinedWQ(PreDefinedParametersWQ filter);

        /// <summary>
        /// Loads the pre clearance master data.
        /// </summary>
        /// <returns></returns>
        PreClearanceMasterData LoadPreClearanceMasterData();

        /// <summary>
        /// Loads the secondary master data.
        /// </summary>
        /// <returns></returns>
        SecondaryRightsMasterData LoadSecondaryMasterData();

        /// <summary>
        /// Loads the digital master data.
        /// </summary>
        /// <returns></returns>
        DigitalRightsMasterData LoadDigitalMasterData();

        /// <summary>
        /// Loads the resource digital rights predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourceDigitalRightsResult LoadResourceDigitalRightsPredefined(PreDefinedParametersWQ filter);

        /// <summary>
        /// Loads the resource secondary rights predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourceSecondaryRightsResult LoadResourceSecondaryRightsPredefined(PreDefinedParametersWQ filter);

        /// <summary>
        /// Loads the resource pre clearance predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ResourcePreClearanceResult LoadResourcePreClearancePredefined(PreDefinedParametersWQ filter);

        /// <summary>
        /// Loads the release digital rights predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        ReleaseDigitalRightsResult LoadReleaseDigitalRightsPredefined(PreDefinedParametersWQ filter);

        /// <summary>
        /// Loads the right set territory.
        /// </summary>
        /// <param name="rightSetId">The right set id.</param>
        /// <returns></returns>
        List<TerritorialDisplay> LoadRightSetTerritory(long rightSetId);

        /// <summary>
        /// Unlinks the contract.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="userName">Name of the user.</param>
        void UnlinkContract(string term, string userName);

        /// <summary>
        /// Gets the linked right sets.
        /// </summary>
        /// <param name="changeLinkItems">The change link items.</param>
        /// <returns></returns>
        List<long> GetLinkedRightSets(List<ChangeLinkInfo> changeLinkItems);

    }
}
