/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RccClearanceContract
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Vijay Venkatesh Prasad.N
 * Created on     : 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System.Collections.Generic;
using System.Globalization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System.Web.Mvc;
using System.Linq;
using UMGI.GRCS.UI.Interfaces;


namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Model for RccClearanceContract Creation Page
    /// </summary>
    public class RccClearanceContractModel : IRccClearanceContractModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RccClearanceContractModel"/> class.
        /// </summary>
        public RccClearanceContractModel()
        {

            NewRccContract = new RccClearanceContract();

            #region
            var objContractStatusList = new List<StringIdentifier>
                                            {
                                                new StringIdentifier {Id = 0, Description = Constants.SignedContract},
                                                new StringIdentifier {Id = 1, Description = Constants.DealDraftContract}
                                            };
            ContractStatusList = objContractStatusList.Select(p => new SelectListItem { Text = p.Description, Value = p.Id.ToString(CultureInfo.InvariantCulture) });
            #endregion

            #region
            var objWorkFlowStatus = new List<ContractSearch> {new ContractSearch {WorkFlowStatus = Constants.DataEntry}};
            WorkFlowStatusList = objWorkFlowStatus.Select(contract => new SelectListItem { Text = contract.WorkFlowStatus, Value = contract.WorkFlowStatus });
            #endregion
        }
        public IEnumerable<SelectListItem> WorkFlowStatusList { get; set; }
        public RccClearanceContract NewRccContract { get; set; }
        public IEnumerable<SelectListItem> ContractStatusList { get; set; }

    }




}
