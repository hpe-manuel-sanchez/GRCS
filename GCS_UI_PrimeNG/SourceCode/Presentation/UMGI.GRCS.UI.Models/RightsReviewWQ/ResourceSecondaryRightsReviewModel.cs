/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ResourceSecondaryRightsReviewModel.cs
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
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.UI.Interfaces.RightsReviewWQ;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    public class ResourceSecondaryRightsReviewModel : IResourceSecondaryRightsReviewModel 
    {
        public ResourceSecondaryRightsResult RightsData { get; set; }
        public SecondaryRightsMasterData MaterData { get; set; }
        public ResourceSecondaryRightsReviewModel()
        {
            ReviewStatusDropDown = new List<SelectListItem>
                                       {
                                           new SelectListItem() { Text = ReviewStatusType.NewForReview.ToString(), Value = ReviewStatusType.NewForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.FinalForReview.ToString(), Value = ReviewStatusType.FinalForReview.ToString() },
                                           new SelectListItem() { Text = ReviewStatusType.Final.ToString(), Value = ReviewStatusType.Final.ToString() }
                                       };

        }
       
        public List<SecondaryRightsMasterData> SecondaryExploitation {get;set;}
        public ResourceSecondaryRights right { get; set; }
        public List<SelectListItem> ReviewStatusDropDown { get; set; }
       
        
        /// <summary>
        /// Gets or sets the restrictions.
        /// </summary>
        /// <value>The restrictions.</value>
        
        public List<SelectListItem> Restrictions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the exploitation types.
        /// </summary>
        /// <value>The exploitation types.</value>

        public List<SelectListItem> ExploitationTypes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the consent period.
        /// </summary>
        /// <value>The consent period.</value>

        public List<SelectListItem> ConsentPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the restricted options.
        /// </summary>
        /// <value>The restricted options.</value>

        public List<SelectListItem> RestrictedOptions
        {
            get;
            set;
        }
      
    }
}
