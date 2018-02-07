/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MasterRightsDefaultData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 19-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entites for Master Rights Default Data Details
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class DigitalRightsMasterData
    {

        /// <summary>
        /// Gets or sets the content types.
        /// </summary>
        /// <value>The content types.</value>
        [DataMember]
        public Dictionary<int, string> ContentTypes { get; set; }

        [DataMember]
        public Dictionary<int, string> RestrictionReason { get; set; }

        [DataMember]
        public Dictionary<string, int> RestrictionReasonReverse { get; set; }

        /// <summary>
        /// Gets or sets the use types.
        /// </summary>
        /// <value>The use types.</value>
        [DataMember]
        public Dictionary<string, int> UseTypesReverse { get; set; }

        /// <summary>
        /// Gets or sets the restriction types.
        /// </summary>
        /// <value>The restriction types.</value>
        [DataMember]
        public Dictionary<string, int> RestrictionTypesReverse { get; set; }

        /// <summary>
        /// Gets or sets the commercial model types.
        /// </summary>
        /// <value>The commercial model types.</value>
        [DataMember]
        public Dictionary<string, int> CommercialModelTypesReverse { get; set; }

        /// <summary>
        /// Gets or sets the consent period types.
        /// </summary>
        /// <value>The consent period types.</value>
        [DataMember]
        public Dictionary<string, int> ConsentPeriodTypesReverse { get; set; }

        /// <summary>
        /// Gets or sets the use types.
        /// </summary>
        /// <value>The use types.</value>
        [DataMember]
        public Dictionary<int, string> UseTypes { get; set; }

        /// <summary>
        /// Gets or sets the restriction types.
        /// </summary>
        /// <value>The restriction types.</value>
        [DataMember]
        public Dictionary<int, string> RestrictionTypes { get; set; }

        /// <summary>
        /// Gets or sets the commercial model types.
        /// </summary>
        /// <value>The commercial model types.</value>
        [DataMember]
        public Dictionary<int, string> CommercialModelTypes { get; set; }

        /// <summary>
        /// Gets or sets the consent period types.
        /// </summary>
        /// <value>The consent period types.</value>
        [DataMember]
        public Dictionary<int, string> ConsentPeriodTypes { get; set; }

        /// <summary>
        /// Constructs the save reverse info.
        /// </summary>
        /// <returns></returns>
        public DigitalRightsMasterData ConstructSaveReverseInfo()
        {
            UseTypesReverse = 
                UseTypes.ToDictionary(key => key.Value.Replace(Constants.Constants.StringEmpty, Constants.Constants.StringNullEmpty), value => value.Key);
            CommercialModelTypesReverse =
                CommercialModelTypes.ToDictionary(key => key.Value.Replace(Constants.Constants.StringEmpty, Constants.Constants.StringNullEmpty), value => value.Key);
            RestrictionTypesReverse =
                RestrictionTypes.ToDictionary(key => key.Value.Replace(Constants.Constants.StringEmpty, Constants.Constants.StringNullEmpty), value => value.Key);
            ConsentPeriodTypesReverse =
                ConsentPeriodTypes.ToDictionary(key => key.Value.Replace(Constants.Constants.StringEmpty, Constants.Constants.StringNullEmpty), value => value.Key);
            RestrictionReasonReverse =
            RestrictionReason.ToDictionary(key => key.Value.Replace(Constants.Constants.StringEmpty, Constants.Constants.StringNullEmpty), value => value.Key);            
            return this;
        }
     
    }
}
