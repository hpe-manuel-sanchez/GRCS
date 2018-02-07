/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReviewRightSaveResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 02-04-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Review Right Save Result Info
                  
****************************************************************************/
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    public class ReviewRightSaveResult
    {
        public long RightSetId { get; set; }
        public long RepertoireId { get; set; }
        public long AssociatedId { get; set; }
        public RepertoireType ResourceType { get; set; }
        public bool IsUpdated { get; set; }
    }
}
