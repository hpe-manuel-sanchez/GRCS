/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : CompanySearchCriteria.cs
 * Project Code :   
 * Author       : R.MuthuKumar
 * Created on   : 27 March 2013
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************/
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class CompanySearchCriteria : PagingBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string IsacCode { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public long WorkGroupId { get; set; }

        [DataMember]
        public string UserLoginName { get; set; }
    }
}
