/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DataAccessGroup.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil kumar
 * Created on   : 05-10-2012
 * Modified on  : 22-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for ANA
                  
****************************************************************************/
using System.Runtime.Serialization;
using System;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// Entities based on the Filtered Permisions from ANA 
    /// </summary>
    [DataContract]
    [Serializable]
    public class DataAccessGroupData : EntityInformation
    {
        //CompanyId, WorkFlow Id,Contract status Id will be accomodated here
        [DataMember]
        public string Id { get; set; }

        //WorkFlow name,Contract status name will be accomodated here
        [DataMember]
        public string Name { get; set; }

        //Country Id will be accomodated here. Only applicable for an Entity Type Country
        [DataMember]
        public string CountryId { get; set; }

        [DataMember]
        public long? R2AccountId { get; set; }
    }
}