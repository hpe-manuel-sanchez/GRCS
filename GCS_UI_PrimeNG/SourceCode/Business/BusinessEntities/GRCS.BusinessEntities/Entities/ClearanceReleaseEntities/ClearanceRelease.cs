
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceRelease.cs 
 * Project Code : UMG-GRCS 
 * Author       : Rohit Gupta
 * Created on   : 09-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceRelease : ReleaseInfo
    {

        [DataMember]
        public List<long> ClrProjectReleaseIds { get; set; }

        [DataMember]
        public Boolean IsMultiArtist { get; set; }

        [DataMember]
        public Boolean Icla_Level_Deviated { get; set; }

        [DataMember]
        public string Configuration_Desc { get; set; }

        [DataMember]
        public string ConfigurationGroup_Id { get; set; }

        [DataMember]
        public string ConfigurationGroup_Desc { get; set; }

        [DataMember]
        public int MusicType_Id { get; set; }

        [DataMember]
        public string MusicType_Desc { get; set; }

        // public ClearanceRelease()
        //{
        //    //List<DropSampleData> _TempDropDown = new List<DropSampleData>();          
        //    //DropSampleData d = new DropSampleData();
        //    //d.Description = "test";
        //    //d.Id = 1;
        //    //_TempDropDown.Add(d);
        //    //d.Id = 2;
        //    //d.Description = "test2";
        //    //_TempDropDown.Add(d);
        //}
        [DataMember]
        public string releaseComposer { get; set; }

        [DataMember]
        public string labelName { get; set; }

        [DataMember]
        public string Project_Code { get; set; }

        [DataMember]
        public long SalesChannelId { get; set; }

        [DataMember]
        public Int64 Clr_Project_Id { get; set; }

        [DataMember]
        public Int64 Account_Id { get; set; }

        [DataMember]
        public bool SoundTrack { get; set; }

        [DataMember]
        public string SoundTrackText { get; set; }

        [DataMember]
        public bool Package { get; set; }

        [DataMember]
        public string PackageText { get; set; }

        [DataMember]
        public byte MusicType { get; set; }

        [DataMember]
        public int NumberOfTracks { get; set; }

        [DataMember]
        public int NumberOfComponents { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public byte ICLALevel_Regular { get; set; }

        [DataMember]
        public string ICLALevelText_Regular { get; set; }

        [DataMember]
        public Boolean Is_Regular_Retail { get; set; }

        [DataMember]
        public byte PriceLevel_Regular { get; set; }

        [DataMember]
        public string PriceLevel_RegularDesc { get; set; } //Dinesh

        [DataMember]
        public bool IsDeviatedICLALevel_Regular { get; set; }

        [DataMember]
        public string DeviatedICLALevel_Regular { get; set; }

        [DataMember]
        public string DeviatedICLALevel_RegularDesc { get; set; }


        [DataMember]
        public string Comments_SalesChannel_Regular { get; set; }

        [DataMember]
        public Boolean Is_TV { get; set; }

        [DataMember]
        public byte ICLALevel_TV { get; set; }

        [DataMember]
        public string ICLALevelText_TV { get; set; }

        [DataMember]
        public byte PriceLevel_TV { get; set; }

        [DataMember]
        public string PriceLevel_TVDesc { get; set; }//Dinesh

        [DataMember]
        public bool IsDeviatedICLALevel_TV { get; set; }

        [DataMember]
        public string DeviatedICLALevel_TV { get; set; }

        [DataMember]
        public string DeviatedICLALevel_TVDesc { get; set; }//Dinesh

        [DataMember]
        public string Comments_SalesChannel_TV { get; set; }

        [DataMember]
        public Boolean Is_Price_Reduction { get; set; }

        [DataMember]
        public byte ICLALevel_Price { get; set; }

        [DataMember]
        public string ICLALevelText_Price { get; set; }

        [DataMember]
        public byte PriceLevel_Price { get; set; }

        [DataMember]
        public string PriceLevel_PriceDesc { get; set; }

        [DataMember]
        public string DeviatedICLALevel_Price { get; set; }

        [DataMember]
        public string DeviatedICLALevel_PriceDesc { get; set; }

        [DataMember]
        public bool IsDeviatedICLALevel_Price { get; set; }

        [DataMember]
        public string Comments_SalesChannel_Price { get; set; }

        [DataMember]
        public Boolean Is_Club { get; set; }

        [DataMember]
        public byte ICLALevel_Club { get; set; }

        [DataMember]
        public string ICLALevelText_Club { get; set; }

        [DataMember]
        public byte PriceLevel_Club { get; set; }
        [DataMember]
        public string PriceLevel_ClubDesc { get; set; } //Dinesh



        [DataMember]
        public string DeviatedICLALevel_Club { get; set; }

        [DataMember]
        public string DeviatedICLALevel_ClubDesc { get; set; } //Dinesh

        [DataMember]
        public string Comments_SalesChannel_Club { get; set; }

        [DataMember]
        public bool IsDeviatedICLALevel_Club { get; set; }

        [DataMember]
        public Boolean Is_Promotional { get; set; }

        [DataMember]
        public byte ICLALevel_Promotional { get; set; }

        [DataMember]
        public string ICLALevelText_Promotional { get; set; }

        [DataMember]
        public byte PriceLevel_Promotional { get; set; }
        [DataMember]
        public string PriceLevel_PromotionalDesc { get; set; }//Dinesh

        [DataMember]
        public string DeviatedICLALevel_Promotional { get; set; }

        [DataMember]
        public string DeviatedICLALevel_PromotionalDesc { get; set; }


        [DataMember]
        public string Comments_SalesChannel_Promotional { get; set; }

        [DataMember]
        public bool IsDeviatedICLALevel_Promotional { get; set; }

        [DataMember]
        public Boolean Is_Non_Traditional { get; set; }

        [DataMember]
        public byte ICLALevel_Non { get; set; }

        [DataMember]
        public string ICLALevelText_Non { get; set; }

        [DataMember]
        public byte PriceLevel_Non { get; set; }

        [DataMember]
        public string PriceLevel_NonDesc { get; set; }//Dinesh

        [DataMember]
        public string DeviatedICLALevel_Non { get; set; }

        [DataMember]
        public string DeviatedICLALevel_NonDesc { get; set; }//Dinesh

        [DataMember]
        public string Comments_SalesChannel_Non { get; set; }

        [DataMember]
        public Decimal? ExactPPD { get; set; }

        [DataMember]
        public Decimal? EstimatedRetail { get; set; }

        [DataMember]
        public bool ICLA_Non { get; set; }

        [DataMember]
        public bool IsDeviatedICLALevel_Non { get; set; }

        [DataMember]
        public bool SuggestedFee_Non { get; set; }

        [DataMember]
        public Decimal? ResourceFee { get; set; }

        [DataMember]
        public Decimal? DeemedPPD { get; set; }

        [DataMember]
        public Decimal? InvoicePrice { get; set; }

        [DataMember]
        public Decimal? SellingPriceLesVAT { get; set; }

        [DataMember]
        public Decimal? ICLAAccountingBase { get; set; }

        [DataMember]
        public byte? Rights_Type { get; set; }

        [DataMember]
        public string Configuration_Id { get; set; }

        [DataMember]
        public Int64 P_Company_Id { get; set; }

        [DataMember]
        public Int32 P_Year { get; set; }

        [DataMember]
        public string P_Notice_Extension { get; set; }

        [DataMember]
        public bool Is_Mac { get; set; }

        [DataMember]
        public Boolean Is_Ost { get; set; }


        [DataMember]
        public int? No_Components { get; set; }

        [DataMember]
        public int No_Tracks { get; set; }

        [DataMember]
        public byte R2_Workflow_Status_Type { get; set; }

        [DataMember]
        public string Created_User { get; set; }

        [DataMember]
        public DateTime Created_Dttm { get; set; }

        [DataMember]
        public string Modified_User { get; set; }

        [DataMember]
        public DateTime Modified_Dttm { get; set; }

        [DataMember]
        public string Archive_Flag { get; set; }

        [DataMember]
        public string DropDownStatus { get; set; }

        [DataMember]
        public bool IsRemoved { get; set; }

        [DataMember]
        public bool IsSaved { get; set; }

        [DataMember]
        public List<ClearanceResource> resourceDetail { get; set; }


        [DataMember]
        public bool IsDeviatedICLA_Regular { get; set; }

        [DataMember]
        public bool IsDeviatedICLA_TV { get; set; }

        [DataMember]
        public bool IsDeviatedICLA_Price { get; set; }

        [DataMember]
        public bool IsDeviatedICLA_Club { get; set; }

        [DataMember]
        public bool IsDeviatedICLA_Promo { get; set; }

        [DataMember]
        public bool IsDeviatedICLA_nonTrad { get; set; }

        [DataMember]
        public int PriceLevel_Reg_Id { get; set; }

        [DataMember]
        public int PriceLevel_Promo_Id { get; set; }

        [DataMember]
        public int PriceLevel_Non_Id { get; set; }

        [DataMember]
        public int PriceLevel_Club_Id { get; set; }

        [DataMember]
        public int PriceLevel_Price_Id { get; set; }

        [DataMember]
        public int PriceLevel_TV_Id { get; set; }

        [DataMember]
        public List<string> ConfigGroup_List_Id { get; set; }

        [DataMember]
        public int ICLALevel_Reg_Id { get; set; }

        [DataMember]
        public int ICLALevel_Promo_Id { get; set; }

        [DataMember]
        public int ICLALevel_Non_Id { get; set; }

        [DataMember]
        public int ICLALevel_Club_Id { get; set; }

        [DataMember]
        public int ICLALevel_Price_Id { get; set; }

        [DataMember]
        public int ICLALevel_TV_Id { get; set; }

        [DataMember]
        public int Package_Id { get; set; }

        [DataMember]
        public int ExistingReleasePkg_Id { get; set; }

        [DataMember]
        public Int64 ConfigId { get; set; }

        [DataMember]
        public Int64 ConfigIdSelected { get; set; }

        [DataMember]
        public bool IsCompilation { get; set; }

        [DataMember]
        public bool IsExisting { get; set; }

        [DataMember]
        public DateTime ReleaseDt { get; set; }

        [DataMember]
        public string Is_Upc_Manual { get; set; }

        //[DataMember]
        //public string No_ComponentsText { get; set; } //Dinesh
      
        [DataMember]
        public string isPushedToR2 { get; set; }

        [DataMember]
        public string Status_Type { get; set; }

        [DataMember]
        public int RowId { get; set; }

        [DataMember]
        public ClearancePushR2Item ClrPushR2Item { get; set; }
        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        [DataMember]
        public long ParentId { get; set; }

        [DataMember]
        public List<long> PackageIds { get; set; }

        /// <summary>
        /// Gets or sets the CallFrom.
        /// </summary>
        /// <value>The upc.</value>
        [DataMember]
        public string CallFrom { get; set; }

        [DataMember]
        public string ExistingReleases { get; set; }

        [DataMember]
        public string RemovedPackageReleases { get; set; }

        [DataMember]
        public long processId { get; set; }

        [DataMember]
        public long? R2ProjectId { get; set; }

        [DataMember]
        public string releaseTypeDetail { get; set; }

        [DataMember]
        public bool submit { get; set; }

        [DataMember]
        public bool IsNewlyAddedAfterSubmit { get; set; }

        [DataMember]
        public string ReleaseResubmitReasonComments { get; set; }

        [DataMember]
        public bool? IsRouted { get; set; }

        [DataMember]
        public List<ListItem> ConfigListRelease { get; set; }
        //public List<SelectListItem> ConfigListRelease { get; set; }

        [DataMember]
        public string IsAddedBySearchPkg { get; set; }
       
    }

    [Serializable]
    public class DropDeviatedICLALevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class DropPriceLevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class DropClubLevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class DropPromotionalLevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }


}
