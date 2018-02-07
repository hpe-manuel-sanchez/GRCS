/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RollUpStatus.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Richa
 * Created on   : 16-Apr-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description : Defines the entities for Roll-Up Status Report Search Result
                  
****************************************************************************/
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{/// <summary>
 /// Roll-Up Status Report Search Result
 /// </summary>
    [DataContract]
    public class RollUpStatus : EntityInformation
    { /// <summary>
        /// Gets or sets the UPC .
        /// </summary>
        [DataMember]
        public string UPC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the VersionTitle .
        /// </summary>
        [DataMember]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        [DataMember]
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the DataAdminCompany .
        /// </summary>
        [DataMember]
        public string DataAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Multi-Artist Compilation .
        /// </summary>
        [DataMember]
        public string MultiArtistCompilation { get; set; }

        /// <summary>
        /// Gets or sets the Release Date .
        /// </summary>
        [DataMember]
        public string ReleaseDate { get; set; }

        ///// <summary>
        ///// Gets or sets the ClearanceAdminCompany .
        /// </summary>
        [DataMember]
        public string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Roll-up Status .
        /// </summary>
        [DataMember]
        public string RollUp_Status { get; set; }

        /// <summary>
        /// Gets or sets the Last Roll-up Status .
        /// </summary>
        [DataMember]
        public string LastRollUpDate { get; set; }

        /// <summary>
        /// Gets or sets the Release issuer .
        /// </summary>
        [DataMember]
        public string Releaseissuer { get; set; }
        
        /// <summary>
        /// Gets or sets the Package .
        /// </summary>
        [DataMember]
        public string Package { get; set; }

        /// <summary>
        /// Gets or sets the Package .
        /// </summary>
        [DataMember]
        public string TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the total Resultcount .
        /// </summary>
        [DataMember]
        public int Total { get; set; }
    }
}
