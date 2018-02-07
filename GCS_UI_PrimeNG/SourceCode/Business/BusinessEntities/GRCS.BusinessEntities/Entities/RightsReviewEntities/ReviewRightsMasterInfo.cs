/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReviewRightsMasterInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Review Rights Master Information
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Review Rights
    /// Master Information
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReviewRightsMasterInfo
    {
        /// <summary>
        /// Append Review Reason
        /// </summary>
        /// <param name="ReviewReason"></param>
        /// <param name="ReviewReasonData"></param>
        /// <returns></returns>
        public string AppendReviewReason
            (List<int> ReviewReasons)
        {
            string ReviewReasonLnr = string.Empty;
            for (int reasonCount = 0; reasonCount < ReviewReasons.Count; reasonCount++)
            {
                ReviewReasonLnr = ReviewReasonLnr +
                    ReviewReason[(int)ReviewReasons[reasonCount]];
                if (reasonCount < ReviewReasons.Count - 1)
                    ReviewReasonLnr = ReviewReasonLnr + " , ";
            }
            return ReviewReasonLnr;
        }

        public ReviewRightsMasterInfo ConstructSaveReverseInfo()
        {
            RightsPeriodReverse = RightsPeriod.ToDictionary(key => key.Value.Replace(" ",""), value => value.Key);
            LostRightsReasonReverse = LostRightsReason.ToDictionary(key => key.Value.Replace(" ",""), value => value.Key);
            return this;
        }



        /// <summary>
        /// Gets or sets the review reason.
        /// </summary>
        /// <value>The review reason.</value>
        [DataMember]
        public Dictionary<int,string> ReviewReason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the resource types.
        /// </summary>
        /// <value>The resource types.</value>
        [DataMember]
        public Dictionary<int, string> ResourceTypes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        [DataMember]
        public Dictionary<int, string> ReviewStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        [DataMember]
        public Dictionary<int, string> RightsPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lost rights reason.
        /// </summary>
        /// <value>The lost rights reason.</value>
        [DataMember]
        public Dictionary<int, string> LostRightsReason
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the resource pre defined parameters.
        /// </summary>
        /// <value>The resource pre defined parameters.</value>
        [DataMember]
        public Dictionary<int, string> ResourcePreDefinedParameters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the release pre defined parameters.
        /// </summary>
        /// <value>The release pre defined parameters.</value>
        [DataMember]
        public Dictionary<int, string> ReleasePreDefinedParameters
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        public Dictionary<string, int> RightsPeriodReverse
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        public Dictionary<string, int> LostRightsReasonReverse
        {
            get;
            set;
        }
      
    }
}
