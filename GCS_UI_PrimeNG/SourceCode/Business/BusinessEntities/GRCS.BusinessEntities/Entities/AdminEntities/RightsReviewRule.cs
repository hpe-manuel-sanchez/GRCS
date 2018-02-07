using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    /// <summary>
    /// RightsReviewRule
    /// </summary>
    [DataContract]
    public class RightsReviewRule : BaseSearch
    {
        public RightsReviewRule()
        {
            RightsCountries = new RightsCountry();
            RightsDataReviewCondition = new List<RightsDataReviewCondition>();
        }

      
        /// <summary>
        /// Gets or sets the is review condition level.
        /// </summary>
        /// <value>The is review condition level.</value>
        [DataMember]
        public long? IsReviewConditionLevel { get; set; }

        /// <summary>
        /// Gets or sets the rights data review condition.
        /// </summary>
        /// <value>The rights data review condition.</value>
        [DataMember]
        public IEnumerable<RightsDataReviewCondition> RightsDataReviewCondition { get; set; }

        /// <summary> 
        /// Gets or sets the rights countries. 
        /// </summary> 
        /// <value>The rights countries.</value> 
        [DataMember]
        public RightsCountry RightsCountries { get; set; }


        /// <summary>
        /// Gets or sets the Global and country and company.
        /// </summary>
        /// <value>Global and country and company</value>
        [DataMember]
        public int DataReviewLevel { get; set; }
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
        public string LastModifiedDate { get; set; }
    }
}
