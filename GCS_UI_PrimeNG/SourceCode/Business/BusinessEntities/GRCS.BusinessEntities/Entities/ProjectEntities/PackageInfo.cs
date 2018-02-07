/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Package.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Ajitha R
 * Created on   : 19-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 *                                   
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Package.                                      
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    [DataContract]
    [Serializable]
    public class PackageInfo : EntityInformation
    {
        /// <summary>
        /// Gets or sets the package id.
        /// </summary>
        /// <value>The package id.</value>
        [DataMember]
        public long PackageId { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        [DataMember]
        public long ParentId { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        [DataMember]
        public long Sequence  { get; set; }

        /// <summary>
        /// Gets or sets the release id.
        /// </summary>
        /// <value>The release id.</value>
        [DataMember]
        public long ReleaseId { get; set; }


        /// <summary>
        /// Gets or sets the upc.
        /// </summary>
        /// <value>The upc.</value>
        [DataMember]
        public string Upc { get; set; }

        /// <summary>
        /// Gets or sets the upc.
        /// </summary>
        /// <value>The upc.</value>
        [DataMember]
        public string ArchiveFlag { get; set; }

        [DataMember]
        public List<PackageInfo> packageInfo { set; get; }

        [DataMember]
        public long? R2ReleaseId { get; set; }

        [DataMember]
        public bool IsNewlyAddedAfterSubmit { get; set; }

        [DataMember]
        public long OwningcompanyID { get; set; }

        [DataMember]
        public string IsAddedBySearch { get; set; }

    }
}
