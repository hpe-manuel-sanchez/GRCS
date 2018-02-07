/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Package.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Ajitha R
 * Created on   : 19-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 *                                   
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Package.                                      
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    [DataContract]
    [Serializable]
    public class TrackInfo : EntityInformation
    {
        [DataMember]
        public List<ArtistInfo> ArtistInfo { get; set; }

        [DataMember]
        public string TrackDuration { get; set; }

        [DataMember]
        public string[] Isrc { get; set; }

        [DataMember]
        public string ResourceTitle { get; set; }

        [DataMember]
        public string ResourceVersionTitle { get; set; }


        [DataMember]
        public long TrackId { get; set; }

        [DataMember]
        public long SequenceNo { get; set; }

        [DataMember]
        public long ReleaseId { get; set; }

        [DataMember]
        public long ResourceId { get; set; }

        [DataMember]
        public int Side { get; set; }


        public TrackInfo DeepClone()
        {
            var cloneTrackInfo = (TrackInfo)this.MemberwiseClone();
            return cloneTrackInfo;
        }
    }
}
