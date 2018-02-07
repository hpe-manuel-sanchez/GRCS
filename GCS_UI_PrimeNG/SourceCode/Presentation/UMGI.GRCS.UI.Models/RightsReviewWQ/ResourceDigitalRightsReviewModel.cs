/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ResourceDigitalRightsReviewModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Karthik P
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
using UMGI.GRCS.UI.Interfaces.RightsReviewWQ;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using System.Collections.Generic;
using System.Linq;
using System;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    public class ResourceDigitalRightsReviewModel : IResourceDigitalRightsReviewModel
    {
        public ResourceDigitalRightsReviewModel()
        {
            BindedDigitalRights = new List<ResourceDigitalRights>();
            ReviewStatusDropDown = new List<SelectListItem>
                                       {
                                           new SelectListItem() { Text = ReviewStatusType.NewForReview.ToString(), Value = ReviewStatusType.NewForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.FinalForReview.ToString(), Value = ReviewStatusType.FinalForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.Final.ToString(), Value = ReviewStatusType.Final.ToString() }
                                       };
            DigitalRestriction = new List<DigitalRightsMasterData>();
        }

        public ResourceDigitalRightsResult DigitalRightsResult { get; set; }
        public DigitalRightsMasterData DigitalRightsMasterData { get; set; }
        public List<DigitalRightsMasterData> DigitalRestriction { get; set; }
        public List<ResourceDigitalRights> BindedDigitalRights { get; set; }
        public string[] UseTypeDropDown { get; set; }
        public string[] CommercialModelReasonDropDown { get; set; }
        public string[] ConsentPeriodDropDown { get; set; }
        public string[] RestrictionDropDown { get; set; }
        public string[] RestrictionReasonDropDown { get; set; }
        public List<SelectListItem> ReviewStatusDropDown { get; set; }

        //public List<SelectListItem> UseTypes { get; set; }
        //public List<SelectListItem> CommercialModelTypes { get; set; }
        //public List<SelectListItem> ConsentPeriodTypes { get; set; }
        //public List<SelectListItem> RestrictionTypes { get; set; }
        //public List<SelectListItem> RestrictionReason { get; set; }
        //public List<ResourceDigitalRights> GetMasterDataValues
        //(MasterRightsDefaultData masterData, List<ResourceDigitalRights> rights)
        //{
        //    rights.Select(right => right.LostRightsReasonLnr =
        //         right.Rights.LostReason.HasValue ? (right.Rights.LostReason.Value == 0 ? "" : masterData.LostRightsReason[right.Rights.LostReason.Value]) : "").ToList();
        //    rights.Select(right => right.RightsPeriodLnr =
        //        right.Rights.RightsPeriod != 0 ? masterData.RightsPeriod[right.Rights.RightsPeriod] : "").ToList();
        //    rights.Select(right => right.ReviewReasonLnr = (right.ReviewReason != 0 ? masterData.ReviewReason[right.ReviewReason] : "")).ToList();
        //    return rights;
        //}


       

       public void ConstructViewInfo()
        {
            var _dict = new Dictionary<string, string>
                            {
                                {"True", "Y"},
                                {"False", "N"}
                           };
            List<ResourceDigitalRights> digitalRights = DigitalRightsResult.ResourceRights;
           
            digitalRights.Select(resource =>resource.ResourceTypeLnr =Enum.Parse(typeof (ResourcesType), resource.ResourceType.ToString()).ToString()).ToList();

            foreach (ResourceDigitalRights right in digitalRights)
            {               
                right.ContractIdLnr = right.DigitalRestriction.DigitalRestrictions.ContractId.HasValue ? 
                    right.DigitalRestriction.DigitalRestrictions.ContractId.Value.ToString() : string.Empty;
                var digitalRight = right;
                var digitalRestrictions = digitalRight.DigitalRestriction.DigitalRestrictions;
                var rightSetId = digitalRestrictions.RightSetId;
                var repertoireId = digitalRight.RepertoireId;
                var modifiedDate = digitalRestrictions.ModifiedDateTime;
                var reviewStatusLnr = digitalRight.DigitalRestriction.ReviewStatus.Status.ToString();

                var RightsTypeLnr = digitalRight.RightsType.ToString();
                var individualDigitalCollection = digitalRestrictions.DigitalRestrictions;
                digitalRestrictions.DigitalRestrictions= new List<ContractDigitalRestrictions>();
         

                if (individualDigitalCollection.Any())
                {
                    for (var restrictionCount = 0;restrictionCount < individualDigitalCollection.Count;restrictionCount++)
                    {
                        var mergeCount = individualDigitalCollection.Count;
                        var resourceRestriction = new ResourceDigitalRights();
                        if (individualDigitalCollection.Count() > 1)
                        {
                            if (restrictionCount == 0)
                            {
                                resourceRestriction = digitalRight;
                            }
                            else
                            {
                                mergeCount = 0;
                                resourceRestriction = AssignHeaderValuesForMergingRows(resourceRestriction, digitalRight);
                            }
                        }
                        else
                        {
                            mergeCount = 0;
                            resourceRestriction = digitalRight;
                            resourceRestriction.RightSetIdLnr = rightSetId;
                        }
                        resourceRestriction =AssignDigitalRightHeaderInfo(rightSetId, RightsTypeLnr,resourceRestriction, modifiedDate, reviewStatusLnr);
                        resourceRestriction.MergeCountLnr = mergeCount;
                        resourceRestriction.RepertoireId = repertoireId;
                        resourceRestriction.ReviewReasons = digitalRight.ReviewReasons;
                        resourceRestriction = FillDigitalRestrictionValues(resourceRestriction, individualDigitalCollection[restrictionCount]);
                        resourceRestriction.RightsEditPermitted = digitalRight.RightsEditPermitted;
                        resourceRestriction.SensitiveInfoPermitted = digitalRight.SensitiveInfoPermitted;
                        resourceRestriction.ReviewStatusPermitted = digitalRight.ReviewStatusPermitted;
                        if (digitalRight.DigitalRestriction.IsAcquired == false)
                        {
                            resourceRestriction.RightsEditPermitted = Constants.CharN;
                            //resourceRestriction.SensitiveInfoPermitted = Constants.CharN;
                            //resourceRestriction.ReviewStatusPermitted = Constants.CharN;
                        }
                        resourceRestriction.RestrictionReasonForOthers =
                            individualDigitalCollection[restrictionCount].RestrictionOtherReasonInfo ?? string.Empty;
                        BindedDigitalRights.Add(resourceRestriction);
                    }
                }
                else
                {
                    digitalRight = AssignDigitalRightHeaderInfo(rightSetId, RightsTypeLnr, digitalRight, modifiedDate, reviewStatusLnr);
                    digitalRight.MergeCountLnr = 0;
                    digitalRight.RestrictionIdLnr = -1;
                    BindedDigitalRights.Add(digitalRight);
                }
            }

        }

       private ResourceDigitalRights AssignHeaderValuesForMergingRows(ResourceDigitalRights resourceRestriction, ResourceDigitalRights digitalRight)
       {
           resourceRestriction.ResourceType = digitalRight.ResourceType;
           resourceRestriction.ModifiedDate = digitalRight.ModifiedDate;
           resourceRestriction.ISRCId = digitalRight.ISRCId;
           resourceRestriction.UPCId = digitalRight.UPCId;
           resourceRestriction.Artist = digitalRight.Artist;
           resourceRestriction.Title = digitalRight.Title;
           resourceRestriction.VersionTitle = digitalRight.VersionTitle;
           resourceRestriction.ReleaseDate = digitalRight.ReleaseDate;
           resourceRestriction.LinkedContract = digitalRight.LinkedContract;
           resourceRestriction.AdminCompany = digitalRight.AdminCompany;
           resourceRestriction.SideArtist = digitalRight.SideArtist;
           resourceRestriction.SampleExists = digitalRight.SampleExists;
           resourceRestriction.SampleDescription = digitalRight.SampleDescription;
           resourceRestriction.PYear = digitalRight.PYear;
           resourceRestriction.DigitalRestriction.DigitalRestrictions.TerritorialRights =
               digitalRight.DigitalRestriction.DigitalRestrictions.TerritorialRights;
           resourceRestriction.DigitalRestriction.DigitalRestrictions.LostRights =
               digitalRight.DigitalRestriction.DigitalRestrictions.LostRights;
           resourceRestriction.LinkedTooltip = digitalRight.LinkedTooltip;
           resourceRestriction.R2ResourceId = digitalRight.R2ResourceId;
           resourceRestriction.ContractIdLnr = digitalRight.ContractIdLnr;
           resourceRestriction.RightsType = digitalRight.RightsType;
           return resourceRestriction;
       }

        private ResourceDigitalRights AssignDigitalRightHeaderInfo(long rightSetId, string RightsTypeLnr,
           ResourceDigitalRights digitalRight, string modifiedDate, string reviewStatusLnr)
       {
           digitalRight.RightSetIdLnr = rightSetId;
           digitalRight.RightsTypeLnr = RightsTypeLnr;
           digitalRight.SampleExistsLnr = digitalRight.SampleExists ? "Y" : "";
           digitalRight.SideArtistLnr = digitalRight.SideArtist ? "Y" : "";
           digitalRight.TerritorialRightsLnr =
               digitalRight.DigitalRestriction.DigitalRestrictions.TerritorialRights;
           digitalRight.ModifiedDate = modifiedDate;           
           digitalRight.ReviewStatusLnr = reviewStatusLnr;
           // Added new linear for Lost Rights
           var _dict = new Dictionary<bool,string>
                            {
                                {false, "N" },
                                {true, "Y"}
                              
                            };
            digitalRight.LostRightsLnr = digitalRight.DigitalRestriction.DigitalRestrictions.LostRights.HasValue ? _dict[digitalRight.DigitalRestriction.DigitalRestrictions.LostRights.Value] : "";
          // digitalRight.LostRightsLnr != null ? _dict[digitalRight.DigitalRestriction.DigitalRestrictions.LostRights] : (bool?)null;

           digitalRight.ResourceType =digitalRight.ResourceType;
           digitalRight.ResourceTypeLnr =Enum.Parse(typeof(ResourcesType), digitalRight.ResourceType.ToString()).ToString();          
           return digitalRight;
       }

        /// <summary>
        /// Method to fill digital restriction values 
        /// </summary>
        /// <param name="resourceRestriction"></param>
        /// <param name="restriction"></param>
        /// <returns></returns>
        private ResourceDigitalRights FillDigitalRestrictionValues
            (ResourceDigitalRights resourceRestriction,
            ContractDigitalRestrictions restriction)
        {
            resourceRestriction.UseTypeLnr =
                restriction.UseTypeId != 0 ? DigitalRightsMasterData.UseTypes[restriction.UseTypeId] : "";
            resourceRestriction.CommercialModelsLnr =
                restriction.CommercialModelId != 0 ? DigitalRightsMasterData.CommercialModelTypes[restriction.CommercialModelId] : "";
            resourceRestriction.RestrictionLnr =
                restriction.RestrictionId != 0 ? DigitalRightsMasterData.RestrictionTypes[restriction.RestrictionId] : "";
            resourceRestriction.RestrictonReasonLnr =
                restriction.RestrictionReasonId != 0 ? DigitalRightsMasterData.RestrictionReason[restriction.RestrictionReasonId.Value] : "";
            if (restriction.ConsentPeriodId != null)
                resourceRestriction.ConsentPeriodLnr =
                    restriction.ConsentPeriodId != 0 ? DigitalRightsMasterData.ConsentPeriodTypes[restriction.ConsentPeriodId.Value] : "";
            resourceRestriction.NotesLnr = restriction.Notes;
            resourceRestriction.RestrictionIdLnr = restriction.DigitalRestrictionId;
           // resourceRestriction.ReviewReasonLnr =
                   // AppendReviewReason(resourceRestriction.ReviewReasons, ReviewReason);
            return resourceRestriction;
        }
    }
}
