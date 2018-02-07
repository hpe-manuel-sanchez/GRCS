using System;
using System.Collections.Generic;
using System.Linq;
using UMGI.GRCS.Entities.ContractEntities;
using UMGI.GRCS.UI.Rights.Interfaces;
using System.Web.Mvc;


namespace UMGI.GRCS.UI.Rights.Models
{
    public class RightsRestrictionManager : IRightsRestrictionManager
    {

        public RightsRestrictionManager()
        {

            
            RightsAcquired = new List<ContractRightsAcquired>();

            # region Rights Acquired Dummy Data
            isAcquiredList = new List<bool> { true, false }.Select(a => new SelectListItem { Text = GetText(a), Value = a.ToString() });

            RightsAcquired.Add(new ContractRightsAcquired() {IsAcquired = true,Notes= "",RightAcquiredTypeId = "Physical Explotation Rights:"});
            RightsAcquired.Add(new ContractRightsAcquired() { IsAcquired = false, Notes = "test", RightAcquiredTypeId = "Digital Explotation Rights:" });
            RightsAcquired.Add(new ContractRightsAcquired() { IsAcquired = true, Notes = "", RightAcquiredTypeId = "      - claim on UGC Sites:" });
            RightsAcquired.Add(new ContractRightsAcquired() { IsAcquired = true, Notes = "test", RightAcquiredTypeId = "      - Claim on VEVO:" });
            RightsAcquired.Add(new ContractRightsAcquired() { IsAcquired = true, Notes = "", RightAcquiredTypeId = "Mobile Explotation Rights:" });
            RightsAcquired.Add(new ContractRightsAcquired() { IsAcquired = true, Notes = "test", RightAcquiredTypeId = "Claim Public Performance & Broadcasting Revenue:" });
            # endregion Acquired Dummy Data



            # region Digital Restriction Dummy data

            //if (DigitalRestriction == null)
            //{
               DigitalRestriction = new List<ContractDigitalRestrictions>();
            //}
            
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "1", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });
            DigitalRestriction.Add(new ContractDigitalRestrictions() { DigitalRestrictionId = "", ContentTypeId = "", UseTypeId = "", CommercialModelId = "", RestrictionId = "", ConsentPeriodId = "", Notes = "" });

            # endregion Digital Restriction DropDown Dummy data




            # region ContentType DropDown Dummy data

            // RestrictionList = new List<string> { "0","1", "2", "3", "4", "5" }.Select(restriction => new SelectListItem { Text = Getrestriction(restriction), Value = restriction.ToString() });
            List<ContentType> objContentType = new List<ContentType>();
            objContentType.Add(new ContentType() { ContentTypeId = "0", ContentTypeName = "Select          " });
            objContentType.Add(new ContentType() { ContentTypeId = "1", ContentTypeName = "Audio" });
            objContentType.Add(new ContentType() { ContentTypeId = "2", ContentTypeName = "Video" });
            objContentType.Add(new ContentType() { ContentTypeId = "3", ContentTypeName = "Image" });        
            ContentTypeList = objContentType.Select(ContentType => new SelectListItem { Text = ContentType.ContentTypeName, Value = ContentType.ContentTypeId.ToString() });

            # endregion Restriction DropDown Dummy data   



            # region Use Type DropDown Dummy data

            // RestrictionList = new List<string> { "0","1", "2", "3", "4", "5" }.Select(restriction => new SelectListItem { Text = Getrestriction(restriction), Value = restriction.ToString() });
            List<UseType> objUseType = new List<UseType>();
            objUseType.Add(new UseType() { UseTypeId = "0", UseTypeName = "Select          " });
            objUseType.Add(new UseType() { UseTypeId = "1", UseTypeName = "Download" });
            objUseType.Add(new UseType() { UseTypeId = "2", UseTypeName = "Streaming" });
            objUseType.Add(new UseType() { UseTypeId = "3", UseTypeName = "Digital Merchandise" });
            UseTypeList = objUseType.Select(UseType => new SelectListItem { Text = UseType.UseTypeName, Value = UseType.UseTypeId.ToString() });

            # endregion Use Type DropDown Dummy data

            # region CommercialModels DropDown Dummy data

            // RestrictionList = new List<string> { "0","1", "2", "3", "4", "5" }.Select(restriction => new SelectListItem { Text = Getrestriction(restriction), Value = restriction.ToString() });
            List<CommercialModels> objCommercialModels = new List<CommercialModels>();
            objCommercialModels.Add(new CommercialModels() { CommercialModelsId = "0", CommercialModelsName = "Select          " });
            objCommercialModels.Add(new CommercialModels() { CommercialModelsId = "1", CommercialModelsName = "A la Carte" });
            objCommercialModels.Add(new CommercialModels() { CommercialModelsId = "2", CommercialModelsName = "Subscription" });
            objCommercialModels.Add(new CommercialModels() { CommercialModelsId = "3", CommercialModelsName = "Ad-Funded" });
            objCommercialModels.Add(new CommercialModels() { CommercialModelsId = "3", CommercialModelsName = "<<All>>" });
            CommercialModelsList = objCommercialModels.Select(CommercialModelsName => new SelectListItem { Text = CommercialModelsName.CommercialModelsName, Value = CommercialModelsName.CommercialModelsId.ToString() });

            # endregion CommercialModes DropDown Dummy data


            # region Restriction DropDown Dummy data

            // RestrictionList = new List<string> { "0","1", "2", "3", "4", "5" }.Select(restriction => new SelectListItem { Text = Getrestriction(restriction), Value = restriction.ToString() });
            List<Restriction> objRestriction = new List<Restriction>();
            objRestriction.Add(new Restriction() { RestrictionTypeId = "0", RestrictionTypeName = "Select          " });
            objRestriction.Add(new Restriction() { RestrictionTypeId = "1", RestrictionTypeName = "No Rights" });
            objRestriction.Add(new Restriction() { RestrictionTypeId = "2", RestrictionTypeName = "Consent Required" });
            objRestriction.Add(new Restriction() { RestrictionTypeId = "3", RestrictionTypeName = "Refer to legal" });
            objRestriction.Add(new Restriction() { RestrictionTypeId = "4", RestrictionTypeName = "No Restrictions" });
            objRestriction.Add(new Restriction() { RestrictionTypeId = "5", RestrictionTypeName = "Consult" });
            RestrictionList = objRestriction.Select(Restriction => new SelectListItem { Text = Restriction.RestrictionTypeName, Value = Restriction.RestrictionTypeId.ToString() });

            # endregion Restriction DropDown Dummy data

            # region Consent Period DropDown Dummy data

            // RestrictionList = new List<string> { "0","1", "2", "3", "4", "5" }.Select(restriction => new SelectListItem { Text = Getrestriction(restriction), Value = restriction.ToString() });
            List<ConsentPeriod> objConsentPeriod = new List<ConsentPeriod>();
            objConsentPeriod.Add(new ConsentPeriod() { ConsentPeriodTypeId = "0", ConsentPeriodTypeName = "Select" });
            objConsentPeriod.Add(new ConsentPeriod() { ConsentPeriodTypeId = "1", ConsentPeriodTypeName = "During Hold back Period" });
            objConsentPeriod.Add(new ConsentPeriod() { ConsentPeriodTypeId = "2", ConsentPeriodTypeName = "During Term" });
            objConsentPeriod.Add(new ConsentPeriod() { ConsentPeriodTypeId = "3", ConsentPeriodTypeName = "During & After Term" });
            ConsentPeriodList = objConsentPeriod.Select(ConsentPeriod => new SelectListItem { Text = ConsentPeriod.ConsentPeriodTypeName, Value = ConsentPeriod.ConsentPeriodTypeId.ToString() });

            # endregion Consent Period DropDown Dummy data

        }
        public IEnumerable<SelectListItem> ContentTypeList;

        public IEnumerable<SelectListItem> UseTypeList;

        public IEnumerable<SelectListItem> CommercialModelsList;
        public IEnumerable<SelectListItem> RestrictionList;
        public IEnumerable<SelectListItem> ConsentPeriodList;




        public List<ContractDigitalRestrictions> addDigitalRestriction(List<ContractDigitalRestrictions> listDigitalRestriction)
        {
           ContractDigitalRestrictions objRestriction = new ContractDigitalRestrictions();

           if (listDigitalRestriction == null)
           {
               listDigitalRestriction = new List<ContractDigitalRestrictions>();
           }
           listDigitalRestriction.Add(objRestriction);
           return listDigitalRestriction;
        }





        public string GetText(Boolean a)
        {
            if (a == true)
                return "Yes";
            else
                return "No";
        }


        public List<ContractRightsAcquired> RightsAcquired { get; set; }

        public List<ContractDigitalRestrictions> DigitalRestriction { get; set; }

        public IEnumerable<SelectListItem> isAcquiredList;

    }
    public class ContentType
    {
        public string ContentTypeId { get; set; }
        public string ContentTypeName { get; set; }
    }

    public class UseType
    {
        public string UseTypeId { get; set; }
        public string UseTypeName { get; set; }
    }

    public class CommercialModels
    {
        public string CommercialModelsId { get; set; }
        public string CommercialModelsName { get; set; }
    }


    
}