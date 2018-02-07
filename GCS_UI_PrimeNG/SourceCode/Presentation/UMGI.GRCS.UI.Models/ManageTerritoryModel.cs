using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;

namespace UMGI.GRCS.UI.Models
{

    [Serializable]
    public class ManageTerritoryModel : IManageTerritoryModel<TerritorialDisplay>
    {
        #region "Properties"

        /// <summary>
        /// Get/Set Territory details
        /// </summary>
        public List<TerritorialDisplay> Territories { get; set; }

        public List<TerritorialDisplay> UpdatedTerritories { get; set; }

        public AnaTargetApplication TargetApplication { get; set; }

        /// <summary>
        /// Indicates which Manage Territory window is coming from (1: from Project level; 2: from Workgroup ; 3 or more: from Resources)
        /// </summary>
        public int IdForTerritory { get; set; }

        /// <summary>
        /// Indicates when the call is made from Workgroup forms
        /// </summary>
        public bool IsCallFromWorkgroup { get; set; }

        #endregion

        #region "Constructor"

        /// <summary>
        /// Initializes a new inatance of the class
        /// </summary>
        public ManageTerritoryModel()
        {
            //Initializes a new instance of the TerritorialDisplay collection class.
            Territories = new List<TerritorialDisplay>();
            UpdatedTerritories = new List<TerritorialDisplay>();
        }

        #endregion

    }
}
