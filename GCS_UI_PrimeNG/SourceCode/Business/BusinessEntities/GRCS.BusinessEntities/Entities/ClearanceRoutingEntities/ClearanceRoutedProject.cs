/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ClearanceRoutedProject.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Vaibhav Pant
 * Created on     : 10-01-2013 
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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public class ClearanceRoutedProject : ClearanceProject
    {

        [DataMember]
        public List<RoutedResource> RoutedResources { get; set; }

        [DataMember]
        public bool IsCrossBorderSafeTerritories { get; set; }

        [DataMember]
        public bool IsCrossBorder { get; set; }

        [DataMember]
        public bool IsThirdPartyCompany { get; set; }

        [DataMember]
        public bool IsResubmitted { get; set; }

        [DataMember]
        public RoutingParams RoutedParams { get; set; }

        [DataMember]
        public List<long> ClearenceAdminCountryId { get; set; } //database

        [DataMember]
        public List<long> ClearenceAdminCompanyId { get; set; }

        [DataMember]
        public int RequestingCountryId { get; set; }

        [DataMember]
        public Boolean HasResourceWithSensitiveExploitation { get; set; }

        /// <summary>
        /// MultiArtist
        /// </summary>                
        [DataMember]
        //  [Display(Name = "MultiArtist", ResourceType = typeof(Entity))]
        public bool MultiArtist { get; set; }

        /// <summary>
        /// IsCompilation
        /// </summary>                
        [DataMember]
        //  [Display(Name = "IsCompilation", ResourceType = typeof(Entity))]
        public bool IsCompilation { get; set; }

        /// <summary>
        /// Scope and Request Type
        /// </summary>
        [DataMember]
        //  [Display(Name = "Scope and Request Type", ResourceType = typeof(Entity))]
        public RequestTypeRegular ScopeAndRequestType { get; set; }

        [DataMember]
        // <summary>
        // Routing Sales Channels identified based on project’s Scope and Request Type/Sales Channels 
        // </summary>
        public List<Int16> SalesChannels { get; set; }

        [DataMember]
        public LeanUserInfo UserInfoDetails { get; set; }

        [DataMember]
        public RoutingType RoutingType { get; set; }

        [DataMember]
        public RoutingAction RoutingAction { get; set; }

        [DataMember]
        public RoutingCaller RoutingCaller { get; set; }

        public LeanUserInfo RequesterUserInfo { get; set; }

        [DataMember]
        public List<int> WorkgroupSalesChannels { get; set; }

        [DataMember]
        [XmlIgnore]
        public List<KeyValuePair<long, bool>> CompanyGlobalFlagInfo { get; set; }

        public List<ClearanceEmail> ClearanceEmails { get; set; }

        [DataMember]
        //  [Display(Name = "MultiArtist", ResourceType = typeof(Entity))]
        public bool IsAutoCompleted { get; set; }

        [DataMember]
        public byte UMGICrossBorderRequestType { get; set; }

        [DataMember]
        public byte UMGIThirdPartyCompanyType { get; set; }

        [DataMember]
        public byte UMGIMidPriceReductionType { get; set; }

        [DataMember]
        public byte UMGIBudgetPriceReductionType { get; set; }

        [DataMember]
        public byte UMGISensitiveExploitationType { get; set; }

        [DataMember]
        public bool IsNewCountriesAdded { get; set; }

        [DataMember]
        [XmlIgnore]
        public bool IsInternationalWithPriceReduction { get; set; }

        [DataMember]
        public bool IsDeviatedICLA { get; set; }


    }
}
