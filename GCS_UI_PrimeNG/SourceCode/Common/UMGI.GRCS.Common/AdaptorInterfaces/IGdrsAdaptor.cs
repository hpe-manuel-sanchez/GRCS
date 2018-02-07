/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IGdrsAdaptor.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using UMGI.GRCS.BusinessEntities.Entities.GdrsEntities;

namespace UMGI.GRCS.Common.AdaptorInterfaces
{
    // Enumeration for the search option. The search option can be Release Id based or UPC based
    public enum SearchOption
    {
        ProductId,
        Upc
    }


    public interface IGdrsAdaptor
    {
      //  List<GdrsReleaseDetail> GetReleaseDateFromReleaseId(List<long> releaseIdList);
     //   List<GdrsReleaseDetail> GetReleaseDateFromUpcList(List<string> upcList);
        GdrsReleaseDetail GetReleaseDateFromUpc(string upc);

    }
}