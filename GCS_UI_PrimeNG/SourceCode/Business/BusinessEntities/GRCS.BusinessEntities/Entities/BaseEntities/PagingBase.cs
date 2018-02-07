/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : PagingBase
 * Project Code :   
 * Author       : karthikG
 * Created on   : 28/sep/2012
 * Description  :  
 * *************************************************************************** 
 * Modification History 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    [Serializable]
    public class PagingBase : ClassInfo
    {
        [DataMember]
        public int StartIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalRows { get; set; }

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
    }
}
