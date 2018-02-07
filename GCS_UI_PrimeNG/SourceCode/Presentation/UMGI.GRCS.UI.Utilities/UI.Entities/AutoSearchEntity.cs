/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : AutoSearchEntity.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 26-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Class for AutoSearch from javascript
    /// Label, Value and Id fields should be in small characters for support in javascript
    /// </summary>
    public class AutoSuggestionEntity
    {
        public string label { get; set; }

        public string value { get; set; }

        public int id { get; set; }

        public long countryId { get; set; }

        public int addValue { get; set; }

        public int hasApproval { get; set; }

        public int isPowerUser { get; set; }

        public int isRccUser { get; set; }

        public string isoCode { get; set; }
    }
}
