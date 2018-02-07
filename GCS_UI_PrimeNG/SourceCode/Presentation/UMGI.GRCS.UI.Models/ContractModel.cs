/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 10-07-2012 
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

using UMGI.GRCS.UI.Interfaces;
using System;

namespace UMGI.GRCS.UI.Models
{    
    /// <summary>
    /// Class for contract creation pages.
    /// </summary>
    [Serializable]
    public class ContractModel : IContractModel 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractModel"/> class.
        /// </summary>
        public ContractModel()
        {
            ContractTabModel = new ContractTabModel();
            RightsRestrictionTabModel = new RightsRestrictionTabModel();
            SecondaryExploitationTabModel = new SecondaryExploitationTabModel();
            TerritorialRightsPopup = new TerritorialRightsModel();
            ManageAddress = new ManageAddressBookModel();

        }
        public IContractTabModel ContractTabModel { get; set; }
        public IRightsRestrictionTabModel RightsRestrictionTabModel { get; set; }
        public ISecondaryExploitationTabModel SecondaryExploitationTabModel { get; set; }
        public IContractTerritorialRightsModel TerritorialRightsPopup { get; set; }
        public string TemplateName { get; set; }
        public long TemplateId { get; set; }
        public IManageAddressBookModel ManageAddress { get; set; }
    
        public bool IsNewTemplate { get; set; }
     
    }
}
