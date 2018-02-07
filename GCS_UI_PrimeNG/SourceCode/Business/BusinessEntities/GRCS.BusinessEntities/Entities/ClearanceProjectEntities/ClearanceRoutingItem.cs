using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.Core.Utilities.Helper;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceRoutingItem : IGrcsBackgroundPendingItem
    {

        /// <summary>
        /// ItemID to be routed from Routing Queue
        /// </summary>
        [DataMember]
        public long UniqueId
        {
            get
            {
                return ClrRoutingQueueId;
            }
        }

        /// <summary>
        /// ItemID to be routed from Routing Queue
        /// </summary>
        [DataMember]
        public long ClrRoutingQueueId { get; set; }

        /// <summary>
        /// Project routed ID
        /// </summary>
        [DataMember]
        public long ClrProjectId { get; set; }

        /// <summary>
        /// Status of routed item
        /// </summary>
        [DataMember]
        public RoutingQueueStatus QueueStatus { get; set; }

        /// <summary>
        /// User requestor for routing project
        /// </summary>
        [DataMember]
        public string CreatorUser { get; set; }

        /// <summary>
        /// User requestor Email
        /// </summary>
        [DataMember]
        public string RequestorEmailId { get; set; }

        /// <summary>
        /// Clearance Email data for notifications
        /// </summary>
        [DataMember]
        public List<ClearanceEmail> ClearanceEmails { get; set; }

        /// <summary>
        /// Clearance Project
        /// </summary>
        [DataMember]
        public ClearanceRoutedProject ProjectRouted { get; set; }
    }
}
