/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : HoldbackPeriod.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the Constants which are used for HoldBack period                                     
                  
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// HoldBack Period  
    /// </summary>
    public class HoldbackPeriod
    {
        // HoldBack Period Constants
        //HoldbackPeriodId
        public int Id { get; set; }

        //HoldbackPeriodName
        public string Name { get; set; }
    }
}