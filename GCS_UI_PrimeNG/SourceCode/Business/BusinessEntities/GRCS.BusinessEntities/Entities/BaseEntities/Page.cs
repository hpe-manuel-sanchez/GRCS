/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : Page
 * Project Code :   
 * Author       : Siva
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Siva              23/10/2012      Initial Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for pagination fields
                  
****************************************************************************/
using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    [Serializable]
    public class Page : EntityInformation
    {
        /// <summary>
        /// StartIndex
        /// </summary>
        [DataMember]
        public int StartIndex { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// SortField
        /// </summary>
        [DataMember]
        public string SortField { get; set; }

        /// <summary>
        /// CheckAscendingOrder
        /// </summary>
        [DataMember]
        public bool IsAscendingOrder { get; set; }

        /// <summary>
        /// TotalRows
        /// </summary>
        [DataMember]
        public int TotalRows { get; set; }

        /// <summary>
        /// Base Search Result - HasPageDetails
        /// </summary>
        [DataMember]
        public bool HasPageDetails { get; set; }
    }
}
