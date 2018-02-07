/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IAutoLinkCdlContract.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 24-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 * 
 ***********************************************************************************
 * Description : This is a partial class of IContract which contains methods related
 *               to CDL-Contract
 ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    public partial interface IContract
    {
        #region AutoLink CDL-Contract-Sprint 5

        /// <summary>
        /// Save CDL-Contract mapping
        /// </summary>
        /// <param name="autoLinkCdlContract">The auto link CDL contract.</param>
        /// <returns></returns>
        [OperationContract]
        bool SaveAutoLinkCdlContract(ContractMapping autoLinkCdlContract);

        /// <summary>
        /// Deletes the CDL contracts.
        /// </summary>
        /// <param name="dictIdTime">The auto link ids & dates deletion list.</param>
        /// <param name="userName">UserName</param>
        [OperationContract]
        void DeleteCdlContracts(Dictionary<long, DateTime> dictIdTime, string userName);

        /// <summary>
        /// GetAutoMappedCompanies method will fetch the matched companies from the Cdl-Contract mapping table
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        [OperationContract]
        DataResponseInfo GetLinkedCompanies(string companyName);

        /// <summary>
        /// GetLinkedDivisions method will fetch the matched divisions from the Cdl-Contract mapping table
        /// </summary>
        /// <param name="divisionName">Name of the division.</param>
        /// <returns></returns>
        [OperationContract]
        DataResponseInfo GetLinkedDivisions(string divisionName);

        /// <summary>
        /// GetLinkedLabels method will fetch the matched labels from the Cdl-Contract mapping table
        /// </summary>
        /// <param name="labelName">Name of the division.</param>
        /// <returns></returns>
        [OperationContract]
        DataResponseInfo GetLinkedLabels(string labelName);

        /// <summary>
        /// FilerCdlMappings method will fetch the CDL-Contract mapping details
        /// </summary>
        /// <param name="mappingFilterCriteria">The mapping filter criteria.</param>
        /// <returns></returns>
        [OperationContract]
        MappingFilterResults FilterCdlMappings(MappingFilterCriteria mappingFilterCriteria);

        #endregion
    }
}
