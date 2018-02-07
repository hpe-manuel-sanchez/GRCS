/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsFilterModel
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 06-03-2013 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 * Vijay.np                     06-03-2013                     Added Properties
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    /// <summary>
    /// Model for Rights Review WorkQueue Search Parameters
    /// </summary>
    public class RightsFilterModel
    {
        public bool Audio { get; set; }

        public bool Video { get; set; }

        public bool Images { get; set; }

        public bool Download { get; set; }

        public bool NewForReview { get; set; }

        public bool FinalForReview { get; set; }

        public bool Final { get; set; }

        public bool SampleExists { get; set; }

        public bool SideArtistExists { get; set; }

        public bool Resource { get; set; }

        public bool Track { get; set; }

        public bool Streaming { get; set; }

        public bool All { get; set; }

        public bool AlaCarte { get; set; }

        public bool AdFunded { get; set; }

        public bool Subscription { get; set; }

        public bool NoRights { get; set; }

        public bool NoRestriction { get; set; }

        public bool ConsentRequired { get; set; }

        public bool Consult { get; set; }

        public bool ReferToLegal { get; set; }

        public bool NoReleaseDate { get; set; }

        public bool DuringAndAfterTerm { get; set; }

        public bool DuringHoldBackPeriod { get; set; }

        public bool DuringTerm { get; set; }

        public string Title { get; set; }

        public byte? PredefinedQuereyTypeId { get; set; }

        public string FromDt { get; set; }

        public string ToDt { get; set; }

        public string ReleaseTitle { get; set; }

        public string Upc { get; set; }

        public string Isrc { get; set; }

        public string LinkedContract { get; set; }

        public int ClearanceAdminCompany { get; set; }

        public int ReasonForReview { get; set; }

        public string ArtistName { get; set; }

        public bool IsExactSearch { get; set; }

        public long[] RightSetIds { get; set; }

        public bool IsCustomWq { get; set; }

        public string IsNavigatedWq { get; set; }

        public string TabIndex { get; set; }
    }
}
