/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ArtistModel.cs
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
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Globalization;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.UI.Models
{
    
    public class ArtistModel : IArtistModel
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="ArtistModel"/> class.
        /// </summary>
        public ArtistModel()
        {
            Artist = new ArtistDetail();
            var _ItemsPerPage = new List<StringIdentifier>
            {
            new StringIdentifier { Id = 25, Description = "25" },
            new StringIdentifier { Id = 50, Description = "50" },
            new StringIdentifier { Id = 100, Description = "100" },
            new StringIdentifier { Id = 150, Description = "150" },
            };
            ItemsPerPage = _ItemsPerPage.Select(results => new SelectListItem { Text = results.Description, Value = results.Id.ToString(CultureInfo.InvariantCulture) });
        }
        public ArtistDetail Artist { get; set; }        
        public List<ArtistDetail> GetArtistList { get; set; }
        public IEnumerable<SelectListItem> ItemsPerPage { get; set; }        
        public string existingArtist { get; set; }
    }
}
