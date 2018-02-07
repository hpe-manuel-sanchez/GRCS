/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Film.cs 
 * Project Code : UMG-GRCS 
 * Author       : dhruv arora
 * Created on   : 09-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class Film : RequestTypeBaseMaster
    {

        /// <summary>
        /// TrailerInContext
        /// </summary>
        [DataMember]
        //  [Display(Name = "TrailerInContext", ResourceType = typeof(Entity))]
        public bool TrailerInContext { get; set; }


    }
}
