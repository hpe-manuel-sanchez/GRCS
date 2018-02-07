/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseSearchResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by         Modified on     Remarks  
 * Sudarsan Nagarajan  27-08-2012      Add the Entities  
 * Vijayakumar R       28-08-2012      Add Some missed Entities 
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceReleaseSearchResult
    {
        /// <summary>
        /// Release Search Result
        /// </summary>
        /// 

        public ClearanceReleaseSearchResult() { }

        /// <summary>
        /// Release Details
        /// </summary>
        //[DataMember]
        //public ObservableCollection<ReleaseDetail> Details { get; set; }


        [DataMember]
        public List<ClearanceRelease> releaseDetail { get; set; }

        [DataMember]
        public List<ClearanceResource> resourceDetail { get; set; }


        [DataMember]
        public ClearanceReleaseSearch releaseSearch { get; set; }

        public void Method()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gridview Display - Current Page Number
        /// </summary>
        [DataMember]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gridview Display - Current Page Size 
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// Page count 
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}