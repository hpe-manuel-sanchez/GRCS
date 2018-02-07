/* ************************************************************************
 * Copyrights � 2012 UMGI
 * ************************************************************************
 * File Name    : ResourceLinkingResult.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : gsenthilkumar
 * Created on   : 06-03-2013
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
    public class ResourceLinkingResult
    {
        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string ErrorReason { get; set; }
    }
}