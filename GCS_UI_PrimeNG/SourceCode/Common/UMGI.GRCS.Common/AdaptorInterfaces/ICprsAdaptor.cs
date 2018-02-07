/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ICprsAdaptor.cs 
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

using UMGI.GRCS.BusinessEntities.Entities.CprsEntities;

namespace UMGI.GRCS.Common.AdaptorInterfaces
{
    public interface ICprsAdaptor
    {
        CprsReleaseDetail GetReleaseDateFromProductId(int productId);
        CprsReleaseDetail GetReleaseDateFromUpc(string upc);
    }
}