/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsRestrictionModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Parvatharaj C
 * Created on     : 10-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.UI.Interfaces;
using System.ComponentModel;
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.UI.Models
{    
    /// <summary>
    /// Model for Rights and RestrictionTab
    /// </summary>
    [Serializable]
    public class RightsRestrictionTabModel : IRightsRestrictionTabModel, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RightsRestrictionTabModel"/> class.
        /// </summary>
        public RightsRestrictionTabModel()
        {

            RightsAcquired = new List<ContractRightsAcquired>();
            DigitalRestriction = new List<ContractDigitalRestrictions>();
            RightsDeal = new List<ContractRightsAcquired>();            
        }

        public IEnumerable<SelectListItem> ContentTypeList { get; set; }
        public IEnumerable<SelectListItem> UseTypeList { get; set; }
        public IEnumerable<SelectListItem> CommercialModelsList { get; set; }
        public IEnumerable<SelectListItem> RestrictionList { get; set; }
        public IEnumerable<SelectListItem> ConsentPeriodList { get; set; }
        public List<ContractRightsAcquired> RightsAcquired { get; set; }
        public List<ContractRightsAcquired> RightsDeal { get; set; }
        public IEnumerable<SelectListItem> DealRestrictOption { get; set; }
        public List<ContractDigitalRestrictions> DigitalRestriction { get; set; }
        public IEnumerable<SelectListItem> IsAcquiredList { get; set; }
        public IEnumerable<SelectListItem> DigitalTemplate { get; set; }

        public long TemplateId { get; set; }
        public string TemplateName { get; set; }
        public bool Result { get; set; }
        public string ClearanceAdmin { get; set; }
        public bool IsNewTemplate { get; set; }
        //Concurrency
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// Gets or sets the last modified date. 
        /// This property is used to hold the serialized LastModifiedTime for Update scenario. This one need not be datemember property as it's not defined to travel across WCF services.
        /// </summary>
        /// <value>The last modified date.</value>
        
        public string LastModifiedDate { get; set; }
        protected RightsRestrictionTabModel(SerializationInfo info, StreamingContext context) 
        {
            this.RightsAcquired = (List<ContractRightsAcquired>)info.GetValue("RightsAcquired", typeof(List<ContractRightsAcquired>));
            this.DigitalRestriction = (List<ContractDigitalRestrictions>)info.GetValue("DigitalRestriction", typeof(List<ContractDigitalRestrictions>));
            this.RightsDeal = (List<ContractRightsAcquired>)info.GetValue("RightsDeal", typeof(List<ContractRightsAcquired>));                               
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RightsAcquired", this.RightsAcquired);
            info.AddValue("DigitalRestriction", this.DigitalRestriction);
            info.AddValue("RightsDeal", this.RightsDeal);
        }
    }
}
   


