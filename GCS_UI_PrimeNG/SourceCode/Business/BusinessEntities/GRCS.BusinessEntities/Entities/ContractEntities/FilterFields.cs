/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : FilterFields
 * Project Code :   
 * Author       : Siva
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Siva              03/08/2012      Initial Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for Filter Fields
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// FilterFields
    /// </summary>
    [DataContract]
    [Serializable]
    public class FilterFields : Page
    {
        /// <summary>
        /// FilterName
        /// </summary>
        [DataMember]
        public string FilterName { get; set; }

        /// <summary>
        /// Primary key of the interface log table
        /// </summary>
        [DataMember]
        public long RowIndex { get; set; }


        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        /// <value>The account id.</value>
        [DataMember]
        public long AccountId { get; set; }

        /// <summary>
        /// Gets or sets the column identifier.
        /// </summary>
        /// <value>The column identifier.</value>
        [DataMember]
        public long ColumnIdentifier { get; set; }

        ///// <summary>
        ///// Gets or sets the Qualification Criteria.
        ///// </summary>
        ///// <value>false</value>

        [DataMember]
        public bool QualificationCriteria { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FilterFields"/> class.
        /// </summary>
        public FilterFields()
        {
            IsAscendingOrder = true;
        }
    }
}