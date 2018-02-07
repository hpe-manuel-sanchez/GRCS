/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IEmailManager.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Arunagiri G
 * Created on   : 26-04-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IEmailManager
    {
        bool SaveEmailData(List<ClearanceEmail> clearanceEmail);

        bool SendEmail();

        bool SendConsolidatedEmail();
    }
}
