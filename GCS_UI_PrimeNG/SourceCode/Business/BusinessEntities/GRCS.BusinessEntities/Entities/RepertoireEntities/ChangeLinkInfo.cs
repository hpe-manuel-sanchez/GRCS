/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : RepertoireRightsSearchResult.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : Rikhu Prasad
 * Created on   : 04/02/2013 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for ChangeLink
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{

    /// <summary>
    /// Change link info for contract
    /// </summary>
    [DataContract]
    [Serializable]
    public class ChangeLinkInfo 
    {
        /// <summary>
        /// Gets or sets the Release/Resource id.
        /// </summary>
        /// <value>The Key id.</value>
        [DataMember]
        public long? KeyId { get; set; }

        /// <summary>
        /// Gets or sets the R2 Release/Resource id.
        /// </summary>
        /// <value>The R2 Key id.</value>
        [DataMember]
        public long? R2KeyId { get; set; }

        /// <summary>
        /// Gets or sets the Contract id.
        /// </summary>
        /// <value>The Contract id.</value>
        [DataMember]
        public long? ContractId { get; set; }

        /// <summary>
        /// ArtistName
        /// </summary>
        [DataMember]
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the version title.
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the link.
        /// </summary>
        /// <value>The type of the link.</value>
        [DataMember]
        public string LinkType { get; set; }

        [DataMember]
        public long RightSetId { get; set; }
        
    }
}
