/* *************************--*******************************************
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractTemplatesResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Mohammad Ghouse
 * Created on   : 19-02-2013 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
      
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    [DataContract]
    public class DigitalRestrictionTemplateResults : EntityInformation
    {
          [DataMember]
          public long Id { get; set; }

          [DataMember]
          public DateTime LastModifiedTime { get; set; }
    }
}
