/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Release.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 07-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Release Details
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release Details
    /// </summary>
    [DataContract]
    public class Release : Repertoire 
    {
        /// <summary>
        /// Gets or sets the type of the release.
        /// </summary>
        /// <value>The type of the release.</value>
        [DataMember]
        public ReleaseType ReleaseType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the UPC id.
        /// </summary>
        /// <value>The UPC id.</value>
        [DataMember]
        public string UPCId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the configration.
        /// </summary>
        /// <value>The configration.</value>
        [DataMember]
        public string Configration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights source.
        /// </summary>
        /// <value>The rights source.</value>
        [DataMember]
        public RightsSource RightsSource
        {
            get;
            set;
        }
        //UI Purpose
        [DataMember]
        public string RightsSourceInfo
        {
            get;
            set;
        }

        [DataMember]
        public int? RightsSourceId
        {
            get;
            set;
        }

        [DataMember]
        public bool IsSplitDeal
        {
            get;
            set;
        }

        [DataMember]
        public long? ContractId
        {
            get;
            set;
        }

        [DataMember]
        public string ReleaseTypeInfo
        {
            get;
            set;
        }

        [DataMember]
        public long RightSetId
        {
            get;
            set;
        }

        [DataMember]
        public string TerritorialRights
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the modified date time.
        /// </summary>
        /// <value>The modified date time.</value>
        [DataMember]
        public string ModifiedDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the LostRights
        /// </summary>
        [DataMember]
        public string LostRights
        {
            get;
            set;
        }
    }
}
