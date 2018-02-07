/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TerritoryDataInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 15-02-2013
 * ************************************************************************ 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for Territorial Display
                  
****************************************************************************/
namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    public class TerritoryDataInfo
    {
        public long Id { get; set; }
        public byte IncludedType { get; set; }
        public bool IsTerritory { get; set; }
    }
}
