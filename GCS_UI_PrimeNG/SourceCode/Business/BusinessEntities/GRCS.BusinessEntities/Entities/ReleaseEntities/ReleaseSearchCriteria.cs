/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : ReleaseSearchCriteria.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : vijayakumar R
 * Created on   : 03/10/2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks

***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************
 *Description :  Defines the entities for ReleaseSearchCriteria

****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
    [DataContract]
    public class ReleaseSearchCriteria : RepertoireSearchCriteriaBase
    {
        /// <summary>
        /// Gets or sets the upc.
        /// </summary>
        /// <value>The upc.</value>
        [DataMember]
        public string Upc { get; set; }

        /// <summary>
        /// Gets or sets the release title.
        /// </summary>
        /// <value>The release title.</value>
        [DataMember]
        public string ReleaseTitle { get; set; }

        /// <summary>
        /// Gets or sets the configuration group code.
        /// </summary>
        /// <value>The configuration group code.</value>
        [DataMember]
        public string ConfigurationGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the configuration id.
        /// </summary>
        /// <value>The configuration id.</value>
        [DataMember]
        public long ConfigurationId { get; set; }

        [DataMember]
        public long RowIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is artist exact search.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is artist exact search; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsArtistExactSearch { get; set; }

        /// <summary>
        ///Get or set the R2 ProjectId
        /// </summary>
        /// <value>
        ///     The R2 ProjectId
        /// </value>
        [DataMember]
        public string R2ProjectId { get; set; }
    }
}
