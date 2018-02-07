/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsDefaultForRepertoire.cs 
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
* Description :  Defines the entities for RightsDefaultRepertoire
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    

    /// <summary>
    /// RightsDefaultForRepertoire
    /// </summary>
   [DataContract]
    public class RightsDefaultForRepertoire : BaseSearch 
    {
       public RightsDefaultForRepertoire()
       {
           RightsCountries = new RightsCountry();
           DigitalRestrictionList = new List<ContractDigitalRestrictions>();
           TerritorialList = new List<TerritorialDisplay>();
           //Criteria = new FilterFields();
       }
        /// <summary>
        /// Gets or sets the right set template id.
        /// </summary>
        /// <value>The right set template id.</value>
       [DataMember]
       public int RightSetTemplateId { get; set; }


       [DataMember]
       public long RightAcquiredTemplateId { get; set; }

       /// <summary>
       /// Description for the respective Id:IsReviewConditionLevel DELETE
       /// UI purpose
       /// </summary>
       public string IsReviewConditionLevelDesc { get; set; }

       /// <summary>
       /// Should Delete IsReviewConditionLevel and be replaced by DataSetLevelId
       /// </summary>
       public byte? DataSetLevelId { get; set; }

       /// <summary> 
       /// Gets or sets the rights countries. 
       /// </summary> 
       /// <value>The rights countries.</value> 
       [DataMember]
       public RightsCountry RightsCountries { get; set; }

       /// <summary>
       /// Gets or sets the type 
       /// </summary>
       /// <value>The type of the review status.</value>
       [DataMember]
       public byte? ReviewStatusType { get; set; }

  
       /// <summary>
       /// Should be replaced for ReviewStatusType
       /// </summary>
       public byte? RightsReviewStatusId { get; set; }

       /// <summary>
       /// Gets or sets the is digital 
       /// </summary>
       /// <value>The is digital unbundle.</value>
       [DataMember]
       public bool? IsDigitalUnbundle { get; set; }

       /// <summary>
       /// Gets or sets the is digital 
       /// </summary>
       /// <value>The is Digital Exploitation.</value>
       [DataMember]
       public bool? IsDigitalExploitation { get; set; }

       /// <summary>
       /// Gets or sets the is default 
       /// </summary>
       /// <value>The is default right.</value>
       [DataMember]
       public bool? IsDefaultRight { get; set; }
       
       /// <summary>
       /// Gets or sets the is physical 
       /// </summary>
       /// <value>The is physical rights.</value>
       [DataMember]
       public bool? IsPhysicalRights { get; set; }

       /// <summary>
       /// Gets or sets the digital restriction.
       /// </summary>
       /// <value>The digital restriction.</value>
       [DataMember]
       public IEnumerable<ContractDigitalRestrictions> DigitalRestriction { get; set; }
       
       /// <summary>
       /// Gets or sets the territorial rights.
       /// </summary>
       /// <value>The territorial rights.</value>
       [DataMember]
       public string TerritorialRights { get; set; }


       [DataMember]
       public List<TerritorialDisplay> TerritorialList { get; set; }

       #region "UI PURPOSE"

       public byte? DigitalUnbundlingAllowed { get; set; }
       public byte? DigitalExploitationRights { get; set; }
       public byte? PhysicalExploitationRights { get; set; }
       public string CompanyName { get; set; }
       public List<ContractDigitalRestrictions> DigitalRestrictionList { get; set; }

       #endregion

       /// <summary>
       /// Gets or sets the criteria.
       /// </summary>
       /// <value>The criteria.</value>
       [DataMember]
       public FilterFields Criteria { get; set; }
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
