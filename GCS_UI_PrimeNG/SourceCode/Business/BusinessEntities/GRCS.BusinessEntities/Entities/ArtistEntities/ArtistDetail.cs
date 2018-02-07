/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ArtistDetail.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 * Rengaraj          03-Aug-2012     Artist Entities - modified code for coding standard format                                    
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for Artist Details.                                      
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    [DataContract]
    public class ArtistDetail : ArtistSearch
    {
        /// <summary>
        /// Artists Prefix Entities
        /// </summary>
        [DataMember]
        public string Prefix { get; set; }

        /// <summary>
        /// Artists Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Artists Roles Performed
        /// </summary>
        [DataMember]
        public string RolesPerformed { get; set; }

        /// <summary>
        /// Artists Data Admin Company
        /// </summary>
        [DataMember]
        public string DataAdminCompany { get; set; }

        /// <summary>
        /// Artists Alias Indicator
        /// </summary>
        [DataMember]
        public string AliasIndicator { get; set; }

        /// <summary>
        /// Artists Additional Information
        /// </summary>
        [DataMember]
        public string AdditonalInfo { get; set; }
    }
}