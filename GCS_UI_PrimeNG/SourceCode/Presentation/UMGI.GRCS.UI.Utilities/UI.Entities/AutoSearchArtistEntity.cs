/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : AutoSearchEntity.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 22-08-2012 
 * Reference      : UC006 – 0080
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
    public class AutoSearchArtistEntity
    {
        public string label { get; set; }

        public string value { get; set; }

        public string id { get; set; }
    }

}
