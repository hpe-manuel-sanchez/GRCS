﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.umusic.com")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.umusic.com", IsNullable = false)]
    public partial class ReleaseRights
    {

        private bool rightsRemovedField;

        private bool rightsRemovedFieldSpecified;

        private long releaseIdField;

        private System.DateTime createdTimeField;

        private string upcField;

        private CountryGroupType[] countryCodeGroupsField;

        private ReleaseRightsContract[] contractsField;

        private LostRightsType[] rightsExpirationField;

        private AvailableRightsAcquisitionsType[] rightsAcquisitionsField;

        private DigRestrictionType[] digRestrictionsField;

        /// <remarks/>
        public bool RightsRemoved
        {
            get { return this.rightsRemovedField; }
            set { this.rightsRemovedField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RightsRemovedSpecified
        {
            get { return this.rightsRemovedFieldSpecified; }
            set { this.rightsRemovedFieldSpecified = value; }
        }

        /// <remarks/>
        public long ReleaseId
        {
            get { return this.releaseIdField; }
            set { this.releaseIdField = value; }
        }

        /// <remarks/>
        public System.DateTime CreatedTime
        {
            get { return this.createdTimeField; }
            set { this.createdTimeField = value; }
        }

        /// <remarks/>
        public string Upc
        {
            get { return this.upcField; }
            set { this.upcField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CountryCodeGroup", IsNullable = false)]
        public CountryGroupType[] CountryCodeGroups
        {
            get { return this.countryCodeGroupsField; }
            set { this.countryCodeGroupsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Contract", IsNullable = false)]
        public ReleaseRightsContract[] Contracts
        {
            get { return this.contractsField; }
            set { this.contractsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("LostRights", IsNullable = false)]
        public LostRightsType[] RightsExpiration
        {
            get { return this.rightsExpirationField; }
            set { this.rightsExpirationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AvailableRightsAcquisitions", IsNullable = false)]
        public AvailableRightsAcquisitionsType[] RightsAcquisitions
        {
            get { return this.rightsAcquisitionsField; }
            set { this.rightsAcquisitionsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DigRestriction", IsNullable = false)]
        public DigRestrictionType[] DigRestrictions
        {
            get { return this.digRestrictionsField; }
            set { this.digRestrictionsField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.umusic.com")]
    public partial class ReleaseRightsContract
    {

        private string[] territorialRightsField;

        private ContractTermType contractTermField;

        private ContractingPartiesType contractingPartiesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CountryGroupId", DataType = "positiveInteger",
            IsNullable = false)]
        public string[] TerritorialRights
        {
            get { return this.territorialRightsField; }
            set { this.territorialRightsField = value; }
        }

        /// <remarks/>
        public ContractTermType ContractTerm
        {
            get { return this.contractTermField; }
            set { this.contractTermField = value; }
        }

        /// <remarks/>
        public ContractingPartiesType ContractingParties
        {
            get { return this.contractingPartiesField; }
            set { this.contractingPartiesField = value; }
        }
    }
}