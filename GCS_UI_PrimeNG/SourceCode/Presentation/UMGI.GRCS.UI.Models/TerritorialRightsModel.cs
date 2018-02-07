/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : TerritorialRightsModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Ramesh Johnson
 * Created on     : 20-07-2012 
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
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.UI.Interfaces;
using System;
using System.Runtime.Serialization;


namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Model for Territorial Rights Page
    /// </summary>
    [Serializable]
    public class TerritorialRightsModel : IContractTerritorialRightsModel
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TerritorialRightsModel"/> class.
        /// </summary>
        public TerritorialRightsModel()
        {            
            SaveTerritorialRights = new List<TerritorialDisplay>();
            GetTerritorialRights = new List<TerritorialDisplay>();
        }
       
        public List<TerritorialDisplay> SaveTerritorialRights
        {
            get;
            set;
        }
        public List<TerritorialDisplay> GetTerritorialRights
        {
            get;
            set;
        }
    }
}
