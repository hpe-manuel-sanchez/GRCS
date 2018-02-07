/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightSetInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 07-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Right Set Information 
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Right Set Information
    /// </summary>
    [DataContract]
    public class RightSetInfo 
    {
        /// <summary>
        /// Gets or sets the right set Id.
        /// </summary>
        /// <value>The right set Id.</value>
        [DataMember]
        public long RightSetId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right set Id.
        /// </summary>
        /// <value>The right set Id.</value>
        [DataMember]
        public long? ReleaseId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Repertoire Unique Id.
        /// </summary>
        /// <value>Repertoire Unique Id.</value>
        [DataMember]
        public long RepertoireId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the territorial rights.
        /// </summary>
        /// <value>The territorial rights.</value>
        [DataMember]
        public string TerritorialRights
        {
            get;
            set;
        }       

        /// <summary>
        /// Gets or sets the modified date time.
        /// </summary>
        /// <value>The modified date time.</value>
        [DataMember]
        public string ModifiedDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [lost rights].
        /// </summary>
        /// <value><c>true</c> if [lost rights];
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? LostRights
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right set Id.
        /// </summary>
        /// <value>The right set Id.</value>
        [DataMember]
        public long? ContractId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right source type.
        /// </summary>
        /// <value>The right source type.</value>
        [DataMember]
        public byte? RightSource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right source type.
        /// </summary>
        /// <value>The right source type.</value>
        [DataMember]
        public int IsEditableRight
        {
            get;
            set;
        }
    }
}
