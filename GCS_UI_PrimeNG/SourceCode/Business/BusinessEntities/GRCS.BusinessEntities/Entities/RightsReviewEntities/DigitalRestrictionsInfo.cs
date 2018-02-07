/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractDigitalRestrictions.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini
 * Created on   : 02-18-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Contract Digital Restrictions
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    [DataContract]
    public class DigitalRestrictionsInfo : RightSetInfo
    {
        public  DigitalRestrictionsInfo()
        {
            DigitalRestrictions=new List<ContractDigitalRestrictions>();
        }

        /// <summary>
        /// Property holding the NotificationRecipient "DigitalRestrictionId"
        /// </summary>
        [DataMember]
        public List<ContractDigitalRestrictions> DigitalRestrictions { get; set; }
    }
}
