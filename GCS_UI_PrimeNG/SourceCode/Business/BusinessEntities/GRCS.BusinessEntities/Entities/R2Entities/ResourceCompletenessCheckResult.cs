/* ************************************************************************
 * Copyrights � 2012 UMGI
 * ************************************************************************
 * File Name    : ResourceCompletenessCheckResult.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : gsenthilkumar
 * Created on   : 22-01-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks

***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class ResourceCompletenessCheckResult
    {
        [DataMember]
        public bool? Status { get; set; }

        [DataMember]
        public string Reason { get; set; }
    }
}