/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Restrictions.cs 
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
 * Description :  Defines the constants for Restrictions and RestrictionOptions 
 
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Restrictions
    /// </summary>
    public class Restrictions
    {
        //Restrictions Constants

        //RestrictionTypeId
        public int TypeId { get; set; }

        //RestrictionTypeName
        public string TypeName { get; set; }
    }

    /// <summary>
    /// RestrictionOptions
    /// </summary>
    public class RestrictionOptions
    {
        // RestrictionOptions Constants

        // RestrictionOptionId
        public int Id { get; set; }

        // RestrictionOptionName
        public string Name { get; set; }
    }
}