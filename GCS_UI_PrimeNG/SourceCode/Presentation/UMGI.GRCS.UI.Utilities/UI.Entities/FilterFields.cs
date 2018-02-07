/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : AutoSearchEntity.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 01-08-2012 
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

namespace UMGI.GRCS.UI.Utilities.UI.Entities
{
    /// <summary>
    /// Entity for Paging and Sorting
    /// </summary>
    public class FilterFields
    {
        public int StartIndex { get; set; }

        public int PageSize { get; set; }

        public string SortField { get; set; }
    }
}
