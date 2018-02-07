/* ************************************************************************ 
* Copyrights ® 2012 UMGI 
* ************************************************************************ 
* File Name    : AddressBook.cs 
* Project Code : UMG-GRCS(C/115921) 
* Author       : Rengaraj
* Created on   : 26-09-2012
* ************************************************************************ 
* Modification History 
* ************************************************************************ 
* Modified by       Modified on     Remarks 
* 
*                                  
*************************************************************************** 
* Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for local and system admin rights                                      
                  
****************************************************************************/


using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{

    [DataContract]
    public class AdminEntities : BaseIdentifier
    {

    }
    /// <summary>
    ///  EmailGroupDetails  which is inherited from EmailGroup
    /// </summary>
    [DataContract]
    public class MaintainRightsDataReview : AdminEntities
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaintainRightsDataReview"/> class.
        /// </summary>
        public MaintainRightsDataReview()
        {
            CountryDetails = new CountryInfo();
            PageDetails = new FilterFields();
        }

        /// <summary>
        /// CountryDetails
        /// </summary>
        [DataMember]
        public CountryInfo CountryDetails { get; set; }

        /// <summary>
        /// Maintain Rights DataReview Details
        /// </summary>
        [DataMember]
        public MaintainRightsDataReview MaintainRightsDataReviewDetails { get; set; }
    }

}
