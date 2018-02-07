using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2Releases
    {
        [DataMember]
        public WSReleaseSearchServiceProxy.WSRelease[] Releases { get; set; }

        public WSReleaseSearchServiceProxy.WSRelease GetAccountSpecificRelease(string accountId, int companyId,
                                                                               int divisionId, int labelId)
        {
            return Releases == null
                       ? null
                       : Releases.FirstOrDefault(
                           release =>
                           System.String.CompareOrdinal(release.accountId, accountId) == 0 &&
                           System.String.CompareOrdinal(release.companyId, companyId.ToString(CultureInfo.InvariantCulture)) == 0 &&
                           System.String.CompareOrdinal(release.divisionId, divisionId.ToString(CultureInfo.InvariantCulture)) == 0 &&
                           System.String.CompareOrdinal(release.labelId, labelId.ToString(CultureInfo.InvariantCulture)) == 0);
        }
    }
}