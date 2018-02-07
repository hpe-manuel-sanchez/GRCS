/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DataResponseInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 13-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the DataResponseInfo properties such as List<ResponseInfo> 
 *                Used primarily to populate Companies, 
 *                Divisions, Labels
 
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    [DataContract]
    public class DataResponseInfo : EntityInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataResponseInfo"/> class.
        /// </summary>
        public DataResponseInfo()
        {
            ResponseInfos = new List<ResponseInfo>();
        }

        /// <summary>
        /// List of Ids and Names can be hold in this property
        /// </summary>
        /// <value>The response infos.</value>
        [DataMember]
        public List<ResponseInfo> ResponseInfos { get; set; }
    }
}
