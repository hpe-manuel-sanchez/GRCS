/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ResourcePreClearanceReviewModel.cs
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

using System.Web.Mvc;
using UMGI.GRCS.UI.Interfaces.RightsReviewWQ;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using System.Collections.Generic;
using System.Linq;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    public class ResourcePreClearanceReviewModel : IResourcePreClearanceReviewModel
    {
        /// <summary>
        /// ResourcePreclearanceReviewModel
        /// </summary>
        public ResourcePreClearanceReviewModel()
        {
            PreClearanceResult = new ResourcePreClearanceResult();
            PreClearanceMasterData = new PreClearanceMasterData();
            BooleanDropDown = new List<SelectListItem>();
            BooleanDropDown.Add(new SelectListItem() {Text = Constants.YValue, Value = Constants.YValue});
            BooleanDropDown.Add(new SelectListItem() {Text = Constants.Nvalue, Value = Constants.Nvalue});
            ReviewStatusDropDown = new List<SelectListItem>
                                       {
                                           new SelectListItem() { Text = ReviewStatusType.NewForReview.ToString(), Value = ReviewStatusType.NewForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.FinalForReview.ToString(), Value = ReviewStatusType.FinalForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.Final.ToString(), Value = ReviewStatusType.Final.ToString() }
                                       };
        }

        public ResourcePreClearanceResult PreClearanceResult { get; set; }
        public PreClearanceMasterData PreClearanceMasterData { get; set; }
        public List<SelectListItem> BooleanDropDown { get; set; }
        public List<SelectListItem> ReviewStatusDropDown { get; set; }

        /// <summary>
        /// Fill Dropdown Values
        /// </summary>
        /// <param name="result"></param>
        /// <param name="masterData"></param>
        /// <returns></returns>

        public List<PreClearanceResult> FillDropDownValues
            (List<PreClearanceResult> result, ReviewRightsMasterInfo masterData)
        {
            result.Select(info => info.ReviewReasonLnr = info.ReviewReasons.Any() ? (info.ReviewReasons[0] != Constants.ValueZero ? (masterData.ReviewReason.ContainsKey(info.ReviewReasons[0]) ? masterData.ReviewReason[info.ReviewReasons[0]] : string.Empty) : string.Empty) : string.Empty).ToList();
            return result;
        }
    }
}
