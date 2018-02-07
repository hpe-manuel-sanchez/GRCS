
using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// ContractRightExplotaition which is inherited from ClassInfo class
    /// </summary>
    [DataContract]
    [Serializable]
    public class ContractRightExplotaition : EntityInformation
    {
        /// <summary>
        /// [Resource].Resource_Id
        /// </summary>
        [DataMember]
        public long Resource_Id { get; set; }

        /// <summary>
        /// [Resource].Rights_Type
        /// </summary>
        [DataMember]
        public byte? Rights_Type { get; set; }

        /// <summary>
        /// [Resource_Right].Resource_Right_Id
        /// </summary>
        [DataMember]
        public long Resource_Right_Id { get; set; }

        /// <summary>
        /// [Right_Set].Right_Set_Id
        /// </summary>
        [DataMember]
        public long Right_Set_Id { get; set; }

        /// <summary>
        /// [Right_Set].Review_Status_Type
        /// </summary>
        [DataMember]
        public byte? Review_Status_Type { get; set; }

        /// <summary>
        /// [Right_Set].Is_Lost_Right
        /// </summary>
        [DataMember]
        public bool? Is_Lost_Right { get; set; }

        /// <summary>
        /// [Right_Set].Is_Clearance_Marketing_Active
        /// </summary>
        [DataMember]
        public bool? Is_Clearance_Marketing_Active { get; set; }
        
        /// <summary>
        /// [Contract].Contract_Id
        /// </summary>
        [DataMember]
        public long Contract_Id { get; set; }

        /// <summary>
        /// [Contract].Is_Clearance_Sensitive_Artist
        /// </summary>
        [DataMember]
        public bool? Is_Clearance_Sensitive_Artist { get; set; }

        /// <summary>
        /// [Contract].Is_Active_Roster
        /// </summary>
        [DataMember]
        public bool? Is_Active_Roster { get; set; }

        /// <summary>
        /// [Contract].Contract_Status_Type
        /// </summary>
        [DataMember]
        public byte? Contract_Status_Type { get; set; }

        /// <summary>
        /// [Exploitation].Exploitation_Id
        /// </summary>
        [DataMember]
        public long Exploitation_Id { get; set; }

        /// <summary>
        /// [Exploitation].Exploitation_Type
        /// </summary>
        [DataMember]
        public byte Exploitation_Type { get; set; }

        /// <summary>
        /// [Exploitation].Restriction_Type
        /// </summary>
        [DataMember]
        public byte Restriction_Type { get; set; }

        /// <summary>
        /// [Exploitation].Consent_Period_Type
        /// </summary>
        [DataMember]
        public byte? Consent_Period_Type { get; set; }
        /// <summary>
        /// [Exploitation].Is_Restriction
        /// </summary>
        [DataMember]
        public bool? Is_Restriction { get; set; }

       
    }
}