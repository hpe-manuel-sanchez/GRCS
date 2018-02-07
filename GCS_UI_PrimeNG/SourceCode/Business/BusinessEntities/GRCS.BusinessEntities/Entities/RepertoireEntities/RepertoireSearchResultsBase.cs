﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
   [DataContract]
   [Serializable]
   public class RepertoireSearchResultsBase : EntityInformation
    {

       
       public RepertoireSearchResultsBase()
        {
            ArtistInfo = new List<ArtistInfo>();
            
        }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
       [DataMember]
       public List<ArtistInfo> ArtistInfo { get; set; }


       /// <summary>
       /// Gets or sets the name of the artist.
       /// </summary>
       /// <value>The name of the artist.</value>
       [DataMember]
       public string ArtistName { get; set; }

       /// <summary>
       /// Gets or sets the admin company id.
       /// </summary>
       /// <value>The admin company id.</value>
       [DataMember]
       public long AdminCompanyId { get; set; }

       /// <summary>
       /// Gets or sets the r2 account id.
       /// </summary>
       /// <value>The r2 account id.</value>
       [DataMember]
       public long R2AccountId { get; set; }
    
    }
}
