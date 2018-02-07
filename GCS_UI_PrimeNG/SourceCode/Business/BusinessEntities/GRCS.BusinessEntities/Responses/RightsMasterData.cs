using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    using System.Runtime.Serialization;

    using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

    /// <summary>
    /// MasterData
    /// </summary>
    [DataContract]
    public class RightsMasterData 
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="MasterData"/> class.
        /// </summary>
        public RightsMasterData()
        {
            CommercialModelTypes = new List<StringIdentifier>();
            ConsentPeriodTypes = new List<StringIdentifier>();
            ContentTypes = new List<StringIdentifier>();
            RestrictionTypes = new List<StringIdentifier>();
            UseTypes = new List<StringIdentifier>();
            //DigitalRestrictionTemplates = new List<TemplateDetails>();
            RightsReviewStatus = new List<StringIdentifier>();
        }

        /// <summary>
        /// Gets or sets the content types.
        /// </summary>
        /// <value>The content types.</value>
        [DataMember]
        public List<StringIdentifier> ContentTypes { get; set; }

        /// <summary>
        /// Gets or sets the use types.
        /// </summary>
        /// <value>The use types.</value>
        [DataMember]
        public List<StringIdentifier> UseTypes { get; set; }

        /// <summary>
        /// Gets or sets the restriction types.
        /// </summary>
        /// <value>The restriction types.</value>
        [DataMember]
        public List<StringIdentifier> RestrictionTypes { get; set; }

        /// <summary>
        /// Gets or sets the commercial model types.
        /// </summary>
        /// <value>The commercial model types.</value>
        [DataMember]
        public List<StringIdentifier> CommercialModelTypes { get; set; }

        /// <summary>
        /// Gets or sets the consent period types.
        /// </summary>
        /// <value>The consent period types.</value>
        [DataMember]
        public List<StringIdentifier> ConsentPeriodTypes { get; set; }

     
        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [DataMember]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the Rights Review Status
        /// </summary>
        /// <value>The Rights Review Status.</value>
        [DataMember]
        public List<StringIdentifier> RightsReviewStatus { get; set; }
    }
}
