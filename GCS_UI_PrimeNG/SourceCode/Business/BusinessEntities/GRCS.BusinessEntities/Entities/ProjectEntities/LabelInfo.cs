/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : LabelInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vijay Venkatesh Prasad . N
 * Created on   : 19-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      Defines the LabelInfo Entities

****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    /// <summary>
    /// LabelInfo
    /// </summary>
    [DataContract]
    public class LabelInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelInfo"/> class.
        /// </summary>
        public LabelInfo()
        {
            LabelList = new List<Identifier<long, string,string>>();
        }

        /// <summary>
        /// Gets or sets the label list.
        /// </summary>
        /// <value>The label list.</value>
        [DataMember]
        public List<Identifier<long,string,string>> LabelList { get; set; }
    }
}
