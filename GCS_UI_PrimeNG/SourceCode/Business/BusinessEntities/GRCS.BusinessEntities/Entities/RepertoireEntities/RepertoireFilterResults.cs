
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RepertoireInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Mohammad Ghouse M
 * Created on   : 22-05-2013
 * ************************************************************************ 
 * Modification History
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
      [Serializable]
     public class RepertoireFilterResults

      {

          public RepertoireFilterResults()
          {
              ProjectIds= new List<long>();
              ReleaseIds = new List<long>();
              ResourceIds = new List<long>();
          }

          /// <summary>
          /// List of Filtered ProjectIds
          /// </summary>
          [DataMember]
          public List<long> ProjectIds { get; set; }

          /// <summary>
          /// List of Filtered ReleaseIds
          /// </summary>
          [DataMember]
          public List<long> ReleaseIds { get; set; }

          /// <summary>
          /// List of Filtered ResourceIds
          /// </summary>
          [DataMember]
          public List<long> ResourceIds { get; set; }

      }
}
