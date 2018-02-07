/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceAcquiredRights.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 07-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Resource Acquired Rights Details
                  
****************************************************************************/
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource Acquired Rights Details
    /// </summary>
    [DataContract]
    public class ResourceAcquiredRights : RepertoireRights
    {
        public ResourceAcquiredRights()
        {
            Status = new ReviewStatus();
            IncludedCountry = new List<long>();
            IncludedTerritory = new List<long>();
            ExcludedCountry = new List<long>();
            ExcludedTerritory = new List<long>();
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [active for MRKT].
        /// </summary>
        /// <value><c>true</c> if [active for MRKT];
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? ActiveForMrkt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating 
        /// whether [digitally unbundled].
        /// </summary>
        /// <value><c>true</c> if [digitally unbundled];
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? DigitallyUnbundled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating 
        /// whether [mobile exploited].
        /// </summary>
        /// <value><c>true</c> if [mobile exploited]; 
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? MobileExploited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating 
        /// whether [PPB claim].
        /// </summary>
        /// <value><c>true</c> if [PPB claim];
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? PPBClaim
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [DataMember]
        public ReviewStatus Status
        {
            get;
            set;
        }

        ///// <summary>
        ///// Gets or sets the status.
        ///// </summary>
        ///// <value>The status.</value>
        //[DataMember]
        //public List<long> IncludedCountry
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Gets or sets the status.
        ///// </summary>
        ///// <value>The status.</value>
        //[DataMember]
        //public List<long> IncludedTerritory
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Gets or sets the status.
        ///// </summary>
        ///// <value>The status.</value>
        //[DataMember]
        //public List<long> ExcludedCountry
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Gets or sets the status.
        ///// </summary>
        ///// <value>The status.</value>
        //[DataMember]
        //public List<long> ExcludedTerritory
        //{
        //    get;
        //    set;
        //}
    }
}
