/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ResourceRightsWorkQueueModel.cs
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


using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.UI.Interfaces;
using System.Linq;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    public class ResourceRightsWorkQueueModel : IResourceRightsWorkQueueModel
    {
        /// <summary>
        /// ResourceRightsWorkQueueModel
        /// </summary>
        public ResourceRightsWorkQueueModel()
        {
            ResourceFilterParameters=new ResourceFilterParameters();
            RightsResult = new ResourceRightsResult();
            RightsMasterData = new ReviewRightsMasterInfo();
            BooleanDropDown = new List<SelectListItem>
                                  {
                                      new SelectListItem() {Text = Constants.YValue, Value = Constants.YValue},
                                      new SelectListItem() {Text = Constants.Nvalue, Value = Constants.Nvalue, Selected = true}
                                  };
            ReviewStatusDropDown = new List<SelectListItem>
                                       {
                                           new SelectListItem() { Text = ReviewStatusType.NewForReview.ToString(), Value = ReviewStatusType.NewForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.FinalForReview.ToString(), Value = ReviewStatusType.FinalForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.Final.ToString(), Value = ReviewStatusType.Final.ToString() }
                                       };
            RightsPeriodDropDown = new List<string>();
            LostRightReasonDropDown = new List<string>();
            ReviewReasonDropDown =new List<SelectListItem>();
            RightsExceptionDropdown = new List<SelectListItem>
                                  {
                                      new SelectListItem() {Text = Constants.Applied, Value = Constants.Applied},
                                      new SelectListItem() {Text = Constants.NotApplied, Value = Constants.NotApplied, Selected = true}
                                  };
        }
        /// <summary>
        /// Get Master Data Values
        /// </summary>
        /// <param name="masterData"></param>
        /// <param name="rights"></param>
        /// <returns></returns>

        public List<ResourcesRight> GetMasterDataValues
          (ReviewRightsMasterInfo masterData, List<ResourcesRight> rights)
        {
            rights.ForEach(right =>
            {
                right.LostRightsReasonLnr =
                    right.Rights.LostReason.HasValue ?
                    (right.Rights.LostReason.Value == 0 ? string.Empty :
                    masterData.LostRightsReason[right.Rights.LostReason.Value]) : string.Empty;
                right.RightsPeriodLnr =
                right.Rights.RightsPeriod != 0 ?
                masterData.RightsPeriod[right.Rights.RightsPeriod] : string.Empty;
                right.ReviewReasonLnr =
                masterData.AppendReviewReason(right.ReviewReasons);
            });

            return rights;
        }
       

        public ResourceFilterParameters ResourceFilterParameters { get; set; }
        public ResourceRightsResult RightsResult { get; set; }
        public ReviewRightsMasterInfo RightsMasterData { get; set; }
        public List<SelectListItem> BooleanDropDown { get; set; }
        public List<SelectListItem> ReviewStatusDropDown { get; set; }
        public List<SelectListItem> ReviewReasonDropDown { get; set; }
        public List<string> RightsPeriodDropDown { get; set; }
        public List<string> LostRightReasonDropDown { get; set; }
        public List<SelectListItem> RightsPeriodDropDownList { get; set; }
        public List<SelectListItem> LostRightReasonDropDownList { get; set; }
        public List<SelectListItem> RightsExceptionDropdown { get; set; }
    }
}

