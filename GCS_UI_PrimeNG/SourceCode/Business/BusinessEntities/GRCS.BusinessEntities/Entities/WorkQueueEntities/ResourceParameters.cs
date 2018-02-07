/* ***************************************************************************  
* Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ResourceParameters.cs
 * Project Code :   
 * Author       : Rubini Raja
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Work queue custom setting properties for 
 *                pre-defined parameters
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    /// <summary>
    /// Work queue custom setting properties for 
    ///            pre-defined parameters
    /// </summary>
    [DataContract]
    public class ResourceParameters : EntityInformation
    {
        /// <summary>
        /// public constructor
        /// </summary>
        public ResourceParameters()
        {
            //create the instance for the parameter
                //dictionary object
            WorkQueueParameter = new Dictionary<int, string>();
        }

        /// <summary>
        /// Represents the pre-defined parameters
        /// </summary>
        [DataMember]
        public Dictionary<int, string>
            WorkQueueParameter
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the clearance admin companies 
        ///     for the user
        /// </summary>
        [DataMember]
        public List<ClearanceAdminCompany> 
            ClearanceAdminCompanies
        {
            get;
            set;
        }
    }
}
