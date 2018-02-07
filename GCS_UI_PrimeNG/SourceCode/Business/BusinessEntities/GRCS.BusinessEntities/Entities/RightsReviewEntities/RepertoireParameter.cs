/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseDateRange.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 11-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Release Date Range details
                  
****************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release Date Range Details
    /// </summary>
    [DataContract]
    public class RepertoireParameter : ReviewWQFilters
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [DataMember]
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [DataMember]
        public DateTime? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [no release date].
        /// </summary>
        /// <value><c>true</c>
        /// if [no release date]; otherwise,
        /// <c>false</c>.</value>
        [DataMember]
        public bool? NoReleaseDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        [DataMember]
        public string AdminCompany
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        [DataMember]
        public string UserAdminCompanies
        {
            get;
            set;
        }

        [DataMember]
        public Dictionary<GrsTasks, List<long>>
            TaskBasedCompanies
        {
            get;
            set;
        }

        public Dictionary<long, Dictionary<GrsTasks, bool>>
         TaskBasedPermissions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>        
        public List<long> UserAdminCompany
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Audio.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is Audio; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsFinal
        {
            get;
            set;
        }


        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? FromDt { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? ToDt { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? PreDefinedFromDt { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? PreDefinedToDt { get; set; }

        [DataMember]
        public byte? PreDefinedID { get; set; }

    }
}
