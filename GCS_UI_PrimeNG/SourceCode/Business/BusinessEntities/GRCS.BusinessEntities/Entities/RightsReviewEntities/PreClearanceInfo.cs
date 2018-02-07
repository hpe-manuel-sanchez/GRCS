/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : PreClearanceInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for PreClearance Information                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for PreClearance Information
    /// </summary>
    [DataContract]
    public class PreClearanceInfo
    {

        public PreClearanceInfo()
        {
            RightInfo = new RightSetInfo();
            ReviewStatusTypes = new ResourceRightSet();
            IncludedCountries = new List<long>();
            ExcludedCountries = new List<long>();
        }
       
        [DataMember]
        public bool? TopPriceCompilation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [mid price compilation].
        /// </summary>
        /// <value><c>true</c> if [mid price compilation]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? MidPriceCompilation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [budget compilation].
        /// </summary>
        /// <value><c>true</c> if [budget compilation]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? BudgetCompilation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [direct marketing].
        /// </summary>
        /// <value><c>true</c> if [direct marketing]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? DirectMarketing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PreClearanceInfo"/> is premium.
        /// </summary>
        /// <value><c>true</c> if premium; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? Premium
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [master syncronise use].
        /// </summary>
        /// <value><c>true</c> if [master syncronise use]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? MasterSyncroniseUse
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the territory exclusions.
        /// </summary>
        /// <value>The territory exclusions.</value>
        [DataMember]
        public string TerritoryExclusions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        [DataMember]
        public ResourceRightSet ReviewStatusTypes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right set info.
        /// </summary>
        /// <value>The right set info.</value>
        [DataMember]
        public RightSetInfo RightInfo
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the Countries exclusion Ids.
        /// </summary>
        /// <value>The Countries exclusion Ids.</value>
        [DataMember]
        public List<long> ExcludedCountries
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the included countries.
        /// </summary>
        /// <value>The included countries.</value>
        [DataMember]
        public List<long> IncludedCountries
        {
            get;
            set;
        }


    }
}
