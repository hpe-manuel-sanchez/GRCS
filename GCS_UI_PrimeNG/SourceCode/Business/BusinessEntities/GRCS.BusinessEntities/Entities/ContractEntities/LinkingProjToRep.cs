/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : CountryInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Linking Project to Repertoire
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// LinkingProjToRep which is inherited from ContractInfo class
    /// </summary>
    public class LinkingProjToRep : ContractInfo
    {
        /// <summary>
        /// SelectedContract
        /// </summary>
        [DataMember]
        public new string ContractId { get; set; }

        /// <summary>
        /// ProjectId
        /// </summary>
        [DataMember]
        public string ProjectId { get; set; }

        /// <summary>
        /// SelectDeselectAllRepertoire
        /// </summary>
        [DataMember]
        public bool IsSelectAllRep { get; set; }

        /// <summary>
        /// SelectAll
        /// </summary>
        [DataMember]
        public bool IsSelectAllRelease { get; set; }

        /// <summary>
        /// ProjectDescription
        /// </summary>
        [DataMember]
        public string ProjectDesc { get; set; }
    }
}