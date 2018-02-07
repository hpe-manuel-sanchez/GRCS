/* *************************--*******************************************
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractTemplatesResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : PavanKumar Kota
 * Created on   : 19-02-2013 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
      
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    [DataContract]
    public class ContractTemplatesResult : EntityInformation
    {
        [DataMember]
        public List<TemplateInfo> ContractTemplates { get; set; }

        [DataMember]
        public int TotalRows { get; set; }

        public ContractTemplatesResult()
        {
            ContractTemplates = new List<TemplateInfo>();
        }
    }
}
