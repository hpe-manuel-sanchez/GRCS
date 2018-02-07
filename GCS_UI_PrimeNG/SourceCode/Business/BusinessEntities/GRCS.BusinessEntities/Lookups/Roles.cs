/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : WorkFlowEnum.cs
 * Project Code :   
 * Author       : Pavan Kumar K
 * Created on   : 10 Sep 2012 
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Rengaraj         18/09/2012      Add File description
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Declare Role Names as enum

****************************************************************************/

using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Lookups
{
    /// <summary>
    /// Enumerator for Roles
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Legal Editor")] 
        LegalEditor = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Legal Reviwer")]
        LegalReviwer = 2,

         
        [Description("Read Only Basic")]
        ReadOnlyBasic = 3,

        [Description("Read Only")]
        ReadOnly = 4,


        [Description("Power User")]
        PowerUser = 5



    }
}