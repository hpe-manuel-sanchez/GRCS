using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for PreDefined Parameters 
    /// WQ Details
    /// </summary>
    [DataContract]
    public class PreClearanceSaveInfo
    {
        /// <summary>
        /// Constructor to initialize ModifierInfo
        /// </summary>
        public PreClearanceSaveInfo()
        {
            ModifierInfo = new ModifierInfo();
        }
        /// <summary>
        /// Gets or sets the resource rights.
        /// </summary>
        /// <value>The resource rights.</value>
        [DataMember]
        public List<PreClearanceInfo> Rights
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
    }
}
