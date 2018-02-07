/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GdrsReleaseDetail.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 01-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Email Groups
                  
****************************************************************************/
using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.GdrsEntities
{
    public class GdrsReleaseDetail : EntityInformation
    {
        /// <summary>
        ///  Release UPC
        /// </summary>
        [DataMember]
        public string Upc { get; set; }


        /// <summary>
        ///  Release ID
        /// </summary>
        [DataMember]
        public long? ReleaseId { get; set; }
       
        /// <summary>
        ///  CoreRowFlag
        /// </summary>
        [DataMember]
        public DateTime? ReleaseDate { get; set; }
    }
}
