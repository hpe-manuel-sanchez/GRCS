/* *************************--*******************************************
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TemplateInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : PavanKumar Kota
 * Created on   : 15-02-2013 
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

namespace UMGI.GRCS.BusinessEntities.Entities.Templates
{
    [DataContract]
    public class TemplateInfo: EntityInformation
    {
        [DataMember]
        public long TemplateId { get; set; }
        [DataMember]
        public string TemplateName { get; set; }

        [DataMember]
        public long? ContractStatusId { get; set; }
        [DataMember]
        public string ContractStatus { get; set; }

        [DataMember]
        public string ContractDescription { get; set; }

        [DataMember]
        public long ClearanceAdminCompanyId { get; set; }
        [DataMember]
        public string ClearanceAdminCompany { get; set; }

        [DataMember]
        public long? RightsTypeId { get; set; }
        [DataMember]
        public string RightsType { get; set; }

        [DataMember]
        public long? UmgSigningCompanyId { get; set; }
        [DataMember]
        public string UmgSigningCompany { get; set; }

        [DataMember]
        public long? PcNoticeCompanyCountryId { get; set; }
        [DataMember]
        public string PcNoticeCompanyCountry { get; set; }

        [DataMember]
        public long? ArtistId { get; set; }
        [DataMember]
        public string ArtistName { get; set; }

        [DataMember]
        public string ContractingParty { get; set; }

        public DateTime LastModifiedDateTime{ get; set; }
    }
}
