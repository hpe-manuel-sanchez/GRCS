/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : CprsNotification.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 01-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Email Groups
                  
****************************************************************************/
using System;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.CprsEntities
{
    public class CprsNotification : EntityInformation
    {
        /// <summary>
        /// ProductID
        /// </summary>
        public int ProductId { get; set; }


        /// <summary>
        /// MessageCreation
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}