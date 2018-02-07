/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ResourceRightsWorkQueueModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Srinivas
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

using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.Resx.Resource.UIResources;
using UMGI.GRCS.UI.Interfaces;
using System.Linq;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    public class ReleaseRightsWorkQueueModel : IReleaseRightsWorkQueueModel
    {
        public ReleaseRightsWorkQueueModel()
        {
            ReleaseRightsInfo = new ReleaseRightsAcquired();
            ReleaseDigitalRightsInfo = new ReleaseDigitalLnr();
            RightsMasterData = new ReviewRightsMasterInfo();
            BooleanDropDown = new List<SelectListItem>
                                  {
                                      new SelectListItem() {Text = Constants.YValue, Value = Constants.YValue},
                                      new SelectListItem() {Text = Constants.Nvalue, Value = Constants.Nvalue, Selected = true}
                                  };

            RightsExceptionDropdown = new List<SelectListItem>
                                  {
                                      new SelectListItem() {Text = Constants.Applied, Value = Constants.Applied},
                                      new SelectListItem() {Text = Constants.NotApplied, Value = Constants.NotApplied, Selected = true}
                                  };

            ReleasePredefinedParameterType = new[]
                        {
                            new SelectListItem
                                {
                                    Value = Constants.ReleaseParamsMultiArtistValue,
                                    Text = WorkQueueResource.MultiArtistCompilationReleases
                                },
                            new SelectListItem
                                {Value = Constants.ReleaseParamsOstValue, Text = WorkQueueResource.OstReleaseOnly},
                            new SelectListItem
                                {Value = Constants.ReleaseParamsNonMacValue, Text = WorkQueueResource.NonMACReleases}
                        };
            ResourcePredefinedParameterType = new[]
                        {
                            new SelectListItem
                                {
                                    Value = Constants.PredefinedRightDataSetValue,
                                    Text = WorkQueueResource.ResourceRightDataSetValue
                                },
                            new SelectListItem
                                {Value = Constants.PredefinedTrackRightDataSetValue, Text = WorkQueueResource.TrackRightDataSetValue},
                            new SelectListItem
                                {Value = Constants.PredefinedTrackResourceRightDataSetValue, Text = WorkQueueResource.TrackResourceRightDataSetValue},
                            new SelectListItem
                                {Value = Constants.PredefinedTrackRightDataSetForTracksReleaseValue, Text = WorkQueueResource.TrackRightDataSetForTracksReleaseValue},
                            new SelectListItem
                                {Value = Constants.PredefinedRightDataSetForResourceWithSamples, Text = WorkQueueResource.RightDataSetForResourceWithSamples},
                            new SelectListItem
                                {Value = Constants.PredefinedRightDataSetForResourceWithSideArtistValue, Text = WorkQueueResource.RightDataSetForResourceWithSideArtistValue},
                            new SelectListItem
                                {Value = Constants.PredefinedRightDataSetForResourcesWithDigitalRestrictionsConReq, Text = WorkQueueResource.RightDataSetForResourcesWithDigitalRestrictionsConReq}

                        };
                          
            ReleaseDateRangeParams = new RepertoireParameter();
            RightsPeriodDropDown = new List<string>();
            LostRightReasonDropDown = new List<string>();

            //Digital
            ReleaseDigitalRightsInfo = new ReleaseDigitalLnr();
            digitalRightsMasterData = new DigitalRightsMasterData();
            UserTypeDropDown = new List<string>();
            CommercialDropDown = new List<string>();
            RestrictionsDropDown = new List<string>();
            RestrictionsReasonDropDown = new List<string>();
            ConsentPeriodDropDown = new List<string>();

        }


        //public List<string> GetTrimmedValues(ReviewRightsMasterInfo masterData, List<ReleaseRight> rights)
        //{
        //   TrimmedLostRightsReason = new List<string>();
        //    foreach (var releaseRight in masterData.LostRightsReason)
        //    {
        //       var trimmedValues = releaseRight.Value.Replace(" ", "&nbsp;");
        //        TrimmedLostRightsReason.Add(trimmedValues);
        //    }

        //    return TrimmedLostRightsReason;

        //    //  rights.Select(right => right.Rights.LostReason =right.Rights.LostReason.HasValue ? masterData.LostRightsReason[right.Rights.LostReason.Value] : "").ToList();

        //    //   TrimmedLostRightsReason=rights.First().Rights.LostReason()
        //}

        //public List<string> GetTrimmedRightsPeriodValues(ReviewRightsMasterInfo masterData, List<ReleaseRight> rights)
        //{
        //    TrimmedRightsPeriodDropDown = new List<string>();
        //    foreach (var releaseRight in masterData.RightsPeriod)
        //    {
        //        var trimmedValues = releaseRight.Value.Replace(" ", "&nbsp;");
        //        TrimmedRightsPeriodDropDown.Add(trimmedValues);
        //    }

        //    return TrimmedRightsPeriodDropDown;

        //    //  rights.Select(right => right.Rights.LostReason =right.Rights.LostReason.HasValue ? masterData.LostRightsReason[right.Rights.LostReason.Value] : "").ToList();

        //    //   TrimmedLostRightsReason=rights.First().Rights.LostReason()
        //}

        //public List<string> GetTrimmedUserTypeDropDown()
        //{
            
        //    TrimmedUserTypeDropDown = new List<string>();
        //    foreach (var releaseRight in digitalRightsMasterData.UseTypes)
        //    {
        //        var trimmedValues = releaseRight.Value.Replace(" ", "&nbsp;");
        //        TrimmedUserTypeDropDown.Add(trimmedValues);
        //    }
        //    return TrimmedUserTypeDropDown;            
        //}

        //public List<string> GetTrimmedCommercialDropDown()
        //{

        //    TrimmedCommercialDropDown = new List<string>();
        //    foreach (var releaseRight in digitalRightsMasterData.CommercialModelTypes)
        //    {
        //        var trimmedValues = releaseRight.Value.Replace(" ", "&nbsp;");
        //        TrimmedCommercialDropDown.Add(trimmedValues);
        //    }
        //    return TrimmedCommercialDropDown;
        //}

        //public List<string> GetTrimmedRestrictionDropDown()
        //{

        //    TrimmedRestrictionsDropDown = new List<string>();
        //    foreach (var releaseRight in digitalRightsMasterData.RestrictionTypes)
        //    {
        //        var trimmedValues = releaseRight.Value.Replace(" ", "&nbsp;");
        //        TrimmedRestrictionsDropDown.Add(trimmedValues);
        //    }
        //    return TrimmedRestrictionsDropDown;
        //}

        //public List<string> GetTrimmedRestrictionReasonDropDown()
        //{

        //    TrimmedRestrictionReasonDropDown = new List<string>();
        //    foreach (var releaseRight in digitalRightsMasterData.RestrictionReason)
        //    {
        //        var trimmedValues = releaseRight.Value.Replace(" ", "&nbsp;");
        //        TrimmedRestrictionReasonDropDown.Add(trimmedValues);
        //    }
        //    return TrimmedRestrictionReasonDropDown;
        //}

        public List<string> GetTrimmedValueforSyncfusionDropDown(Dictionary<int, string> values)
        {
            List<string> result = new List<string>();
            foreach (var rightsValue  in values)
            {
                var trimmedValues = rightsValue.Value.Replace(" ", Constants.HtmlSpace);
                result.Add(trimmedValues);
            }
            return result;
        }

        

        //public List<string> GetTrimmedConsentPeriodDropDown()
        //{

        //    TrimmedConsentPeriodDropDown = new List<string>();
        //    foreach (var releaseRight in digitalRightsMasterData.ConsentPeriodTypes)
        //    {
        //        var trimmedValues = releaseRight.Value.Replace(" ", "&nbsp;");
        //        TrimmedConsentPeriodDropDown.Add(trimmedValues);
        //    }
        //    return TrimmedConsentPeriodDropDown;
        //}

        public ReleaseRightsAcquired ReleaseRightsInfo { get; set; }
        //public ReleaseDigitalLnr ReleaseDigitalRightsInfo { get; set; }
        public ReviewRightsMasterInfo RightsMasterData { get; set; }
        public List<SelectListItem> BooleanDropDown { get; set; }
        public List<SelectListItem> RightsExceptionDropdown { get; set; }
        public SelectListItem[] ReleasePredefinedParameterType { get; set; }
        public SelectListItem[] ResourcePredefinedParameterType { get; set; }

        public IEnumerable<string> LostRightReasonDropDown { get; set; }
        public IEnumerable<string> RightsPeriodDropDown { get; set; }
        public List<string> TrimmedLostRightsReason { get; set; }
        public List<string> TrimmedRightsPeriodDropDown { get; set; }
        public RepertoireParameter ReleaseDateRangeParams
        {
            get; set;
        }

        //Digital
        public ReleaseDigitalLnr ReleaseDigitalRightsInfo { get; set; }
        public DigitalRightsMasterData digitalRightsMasterData { get; set; }
        public IEnumerable<string> UserTypeDropDown { get; set; }
        public IEnumerable<string> CommercialDropDown { get; set; }
        public IEnumerable<string> RestrictionsDropDown { get; set; }
        public IEnumerable<string> RestrictionsReasonDropDown { get; set; }
        public IEnumerable<string> ConsentPeriodDropDown { get; set; }

        public List<string> TrimmedUserTypeDropDown { get; set; }
        public List<string> TrimmedCommercialDropDown { get; set; }
        public List<string> TrimmedRestrictionsDropDown { get; set; }
        public List<string> TrimmedRestrictionReasonDropDown { get; set; }
        public List<string> TrimmedConsentPeriodDropDown { get; set; }
        

    }

}