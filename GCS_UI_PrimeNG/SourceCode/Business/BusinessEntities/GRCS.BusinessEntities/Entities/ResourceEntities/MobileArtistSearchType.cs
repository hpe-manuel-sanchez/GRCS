/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MobileArtistSearchType.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil kumar
 * Created on   : 22-03-2013
 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

*/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    [DataContract]
    public enum MobileArtistSearchType
    {
        [EnumMember] NoFilter = 0,
        [EnumMember] IncludeMobileArtists = 1,
        [EnumMember] ExcludeMobileArtists = 2,
    }
}