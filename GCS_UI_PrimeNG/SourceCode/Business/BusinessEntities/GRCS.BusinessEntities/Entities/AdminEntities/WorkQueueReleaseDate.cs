using System;
using System.Runtime.Serialization;
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : WorkQueueReleaseDate.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vaishnavi Lakshmanan
 * Created on   : 05-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for RightsDataReviewCondition
                  
****************************************************************************/
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
   

    /// <summary>
    /// WorkQueueReleaseDate
    /// </summary>
    [DataContract]
    public class WorkQueueReleaseDate : BaseSearch 
    {
        /// <summary>
        /// Gets or sets the release date lead id.
        /// </summary>
        /// <value>The release date lead id.</value>
        [DataMember]
        public int ReleaseDateLeadId { get; set; }

        /// <summary>
        /// Gets or sets the release date lead.
        /// </summary>
        /// <value>The release date lead.</value>
        [DataMember]
        public int? ReleaseDateLead { get; set; }

        /// <summary>
        /// Gets or sets the rights countries.
        /// </summary>
        /// <value>The rights countries.</value>
        [DataMember]
        public RightsCountry RightsCountry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkQueueReleaseDate"/> class.
        /// </summary>
        public WorkQueueReleaseDate()
        {
            RightsCountry = new RightsCountry();
        }

        /// <summary>
        /// Gets or sets the Global and country and company.
        /// </summary>
        /// <value>Global and country and company</value>
        [DataMember]
        public int WorkQueueLevel { get; set; }

        /// <summary>
        /// Last Modified Time will be helpful while updating a record to check concurrency issue
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// Gets or sets the last modified date. 
        /// This property is used to hold the serialized LastModifiedTime for Update scenario. This one need not be datemember property as it's not defined to travel across WCF services.
        /// </summary>
        /// <value>The last modified date.</value>
        [DataMember]
        public string LastModifiedDate { get; set; }
    }
}
