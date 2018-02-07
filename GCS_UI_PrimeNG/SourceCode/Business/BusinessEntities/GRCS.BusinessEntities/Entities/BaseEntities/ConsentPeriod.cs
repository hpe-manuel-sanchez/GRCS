/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ConsentPeriod.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rengaraj          03-Aug-2012     modified code for coding standard format      
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the Constants which are used for Consent Period class.                                      
                  
****************************************************************************/


namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Consent Period
    /// </summary>
    public class ConsentPeriod
    {
        // Consent Period Constants
        public int TypeId { get; set; }

        public string TypeName { get; set; }
    }
}