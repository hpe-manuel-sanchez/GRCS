/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceRegularProject.cs 
 * Project Code : UMG-GRCS 
 * Author       : Dhruv Arora
 * Created on   : 10-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceRegularProject : ClearanceProject
    {
        /// <summary>
        /// <typeparamref name="CreatedByUser"/>
        /// </summary>
        [DataMember]
        public string CreatedByUser { get; set; }

        /// <summary>
        /// <typeparamref name="CreatedDate"/>
        /// </summary>
        [DataMember]
        public string CreatedDate { get; set; }


        /// <summary>
        /// <typeparamref name="EstimatedSalesUnit"/>
        /// </summary>
        [DataMember]
        public int? EstimatedSalesUnit { get; set; }

        /// <summary>
        /// <typeparamref name="ReleaseDate"/>
        /// </summary>
        [DataMember]
        public string ReleaseDate { get; set; }

        /// <summary>
        /// <typeparamref name="ThirdPartyRepertoire"/>
        /// </summary>
        [DataMember]
        public string ThirdPartyRepertoire { get; set; }

        /// <summary>
        /// <typeparamref name="MultiArtist"/>
        /// </summary>           
        [DataMember]
        public bool MultiArtist { get; set; }

        /// <summary>
        /// <typeparamref name="Compilation"/>
        /// </summary> 
        [DataMember]
        public bool Compilation { get; set; }

        /// <summary>
        /// <typeparamref name="HighPriority"/>
        /// </summary> 
        [DataMember]
        public bool HighPriority { get; set; }

        /// <summary>
        /// <typeparamref name="ThirdParty"/>
        /// </summary> 
        [DataMember]
        public bool ThirdParty { get; set; }


        /// <summary>
        /// <typeparamref name="ScopeAndRequestType"/>
        /// </summary> 
        [DataMember]
        public RequestTypeRegular ScopeAndRequestType { get; set; }

        /// <summary>
        /// <typeparamref name="thirdPartyCompanyDetails"/>
        /// </summary> 
        [DataMember]
        public CompanyInfo thirdPartyCompanyDetails { get; set; }

        /// <summary>
        /// <typeparamref name="ReleaseNewOrExisting"/>
        /// </summary> 
        [DataMember]
        public string ReleaseNewOrExisting { get; set; }

        /// <summary>
        /// <typeparamref name="ProjectInfo"/>
        /// </summary> 
        public ProjectInfo ProjectInfo { get; set; }

        /// <summary>
        /// <typeparamref name="divisionId"/>
        /// </summary> 
        [DataMember]
        public long divisionId { get; set; }

        /// <summary>
        /// <typeparamref name="divisionName"/>
        /// </summary> 
        [DataMember]
        public string divisionName { get; set; }

        /// <summary>
        /// <typeparamref name="R2_Project_Id"/>
        /// </summary> 
        [DataMember]
        public long? R2_Project_Id { get; set; }

        /// <summary>
        /// <typeparamref name="IsExisting"/>
        /// </summary> 
        [DataMember]
        public bool IsExisting { get; set; }

        /// <summary>
        /// <typeparamref name="IsSensitiveDataChanged"/>
        /// </summary> 
        [DataMember]
        public bool IsSensitiveDataChanged { get; set; }

        [DataMember]
        public string R2_Project_Code { get; set; }

    }
}
