/* ***************************************************************************  
* Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : WorkQueueFilters
 * Project Code :   
 * Author       : Siva
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Siva              23/10/2012      Initial Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for workqueue specific filtering fields
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    [DataContract]
    public class WorkQueueFilters : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkQueueFilters"/> class.
        /// </summary>
        public WorkQueueFilters()
        {
            Filters = new List<Filter>();
            FilterType = WorkQueueFilterType.None;
        }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>The filters.</value>
        [DataMember]
        public List<Filter> Filters { get; set; }

        public override string ToString()
        {
            var build = new StringBuilder("WorkQueueFilters: ");
            foreach (var filter in Filters)
            {
                 build.AppendLine(filter.ToString());
            }
            return build.ToString();
        }

        /// <summary>
        /// Filter type whether it is Release/Resource/Project/All
        /// </summary>
        [DataMember]
        public WorkQueueFilterType FilterType { get; set; }
    }
}
