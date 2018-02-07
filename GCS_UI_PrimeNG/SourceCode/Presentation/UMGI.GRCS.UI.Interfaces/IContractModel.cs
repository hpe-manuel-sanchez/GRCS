/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractTabModel.cs
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

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IContractModel
    {
        IContractTabModel ContractTabModel { get; set; }
        IRightsRestrictionTabModel RightsRestrictionTabModel { get; set; }
        ISecondaryExploitationTabModel SecondaryExploitationTabModel { get; set; }
        IContractTerritorialRightsModel TerritorialRightsPopup { get; set; }
        IManageAddressBookModel ManageAddress { get; set; }
        string TemplateName { get; set; }
        long TemplateId { get; set; }
        bool IsNewTemplate { get; set; }
       
    

    }
}
