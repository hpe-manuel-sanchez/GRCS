/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ArtistSearchResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for Base Search.                                      
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Base Search Result
    /// </summary>
    [DataContract]
    [Serializable]
    public class BaseSearch : EntityInformation
    {
        /// <summary>
        /// Base Search Result - HasSearchCriteria 
        /// </summary>
        [DataMember]
        public bool HasSearchCriteria { get; set; }


        /// <summary>
        /// Base Search Result - HasPageDetails
        /// </summary>
        [DataMember]
        public bool HasPageDetails { get; set; }


        /// <summary>
        ///Base Search Result - PageDetails
        /// </summary>
        [DataMember]
        public FilterFields PageDetails { get; set; }


        /// <summary>
        /// Base Search
        /// </summary>
        public BaseSearch()
        {
            PageDetails = new FilterFields();
        }
    }
}