/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RepertoireRights.cs 
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
* Description :  Defines the entities for Repertoire Rights Details
                  
****************************************************************************/

using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Repertoire Rights Details
    /// </summary>
    [DataContract]
    public class RepertoireRights : RightSetInfo
    {
        /// <summary>
        /// Gets or sets the rights expiry rule.
        /// </summary>
        /// <value>The rights expiry rule.</value>
        [DataMember]
        public string RightsExpiryRule
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        [DataMember]
        public int RightsPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value text indicating
        /// whether [lost rights].
        /// </summary>
        [DataMember]
        public string LostRightsText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lost reason.
        /// </summary>
        /// <value>The lost reason.</value>
        [DataMember]
        public int? LostReason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [lost rights].
        /// </summary>
        /// <value><c>true</c> if [lost rights];
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public DateTime? LostRightsDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating 
        /// whether [physically exploited].
        /// </summary>
        /// <value><c>true</c> if [physically exploited]; 
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? PhysicallyExploited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating 
        /// whether [physically exploited].
        /// </summary>
        [DataMember]
        public string PhysicallyExploitedText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether this
        /// <see cref="RepertoireRights"/> is exception.
        /// </summary>
        /// <value><c>true</c> if exception; 
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? Exception
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating
        /// whether this
        /// <see cref="RepertoireRights"/> is exception.
        /// </summary>
        [DataMember]
        public string ExceptionText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [DataMember]
        public string Notes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [digitally exploited].
        /// </summary>
        /// <value><c>true</c> if [digitally exploited];
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? DigitallyExploited
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating
        /// whether [digitally exploited].
        /// </summary>
        [DataMember]
        public string DigitallyExploitedText
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets a value indicating
        /// whether [Error].
        /// </summary>
        [DataMember]
        public string Error
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the included country.
        /// </summary>
        /// <value>The included country.</value>
        [DataMember]
        public List<long> IncludedCountry
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the included territory.
        /// </summary>
        /// <value>The included territory.</value>
        [DataMember]
        public List<long> IncludedTerritory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the excluded country.
        /// </summary>
        /// <value>The excluded country.</value>
        [DataMember]
        public List<long> ExcludedCountry
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the excluded territory.
        /// </summary>
        /// <value>The excluded territory.</value>
        [DataMember]
        public List<long> ExcludedTerritory
        {
            get;
            set;
        }

    
    }
}
