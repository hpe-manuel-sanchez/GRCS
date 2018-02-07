using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Lookups
{
    public enum RepertoireStatus
    {
        #region "Repertoire Status Enum"
        [EnumMember] UnProcessed =1,
        [EnumMember] Processed=2,
        [EnumMember] Processing=3

        #endregion
    }
}
