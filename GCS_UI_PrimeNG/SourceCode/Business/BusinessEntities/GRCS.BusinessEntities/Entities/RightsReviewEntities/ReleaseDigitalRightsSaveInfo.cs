/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseDigitalSaveInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 11-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Release Digital Save Information
                                   
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release 
    /// Digital Save Information
    /// </summary>
    [DataContract]
    public class ReleaseDigitalSaveInfo
    {

        public ReleaseDigitalSaveInfo()
        {
            Rights = new List<ReleaseDigitalRights>();
            ModifierInfo = new ModifierInfo();
        }
        /// <summary>
        /// Gets or sets the resource rights.
        /// </summary>
        /// <value>The resource rights.</value>
        [DataMember]
        public List<ReleaseDigitalRights> Rights
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the modifier info.
        /// </summary>
        /// <value>The modifier info.</value>
        [DataMember]
        public ModifierInfo ModifierInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        [DataMember]
        public string AdminCompany
        {
            get;
            set;
        }
    }
}
