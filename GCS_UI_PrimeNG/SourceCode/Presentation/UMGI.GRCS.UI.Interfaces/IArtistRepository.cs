/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IArtistRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Jyoti Tyagi
 * Created on     : 09-10-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */


using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IArtistRepository
    {

        IArtistModel ObjArtistModel { get; set; }
        ISessionWrapper SessionWrapper { get; set; }
        ArtistSearchResult SearchArtist(ArtistSearchCriteria searchOption, bool artistQualifyingCriteria);
    }
}
