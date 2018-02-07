/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : UploadDocument.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Mukta Aggarwal
 * Created on   : 09-09-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Uplodad Document
 
****************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// UploadDocument
    /// </summary>
    [DataContract]
    [Serializable]
    public class UploadDocument
    {

        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [DataMember]
        public byte[] Data { get; set; }


        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        

    }
}
