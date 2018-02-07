/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : RepertoireRightsSearchResult.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : Ramesh J
 * Created on   : 18/02/2013 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for RepertoireRightsSearchResult
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;


namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// Repertoire Rights Search Result
    /// </summary>
    [DataContract]
    public class DigitalRestrictionRights : EntityInformation
    {

        public DigitalRestrictionRights()
        {
            RepertoireRightsSearchResult = new List<RepertoireRightsSearchResult>();
            DigitalRestriction = new List<ReportoireDigitalRestriction>();
        }

        [DataMember]
        public List<RepertoireRightsSearchResult> RepertoireRightsSearchResult { get; set; }
    
        [DataMember]
        public List<ReportoireDigitalRestriction> DigitalRestriction { get; set; }
    }
}
