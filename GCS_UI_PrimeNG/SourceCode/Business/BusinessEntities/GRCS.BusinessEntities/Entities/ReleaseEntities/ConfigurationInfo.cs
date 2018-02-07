/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ConfigurationInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vijay Venkatesh Prasad .N
 * Created on   : 19-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      Defines the ConfigurationInfo Entities

****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{

    /// <summary>
    /// ConfigurationInfo
    /// </summary>
    [DataContract]
    public class ConfigurationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationInfo"/> class.
        /// </summary>
        public ConfigurationInfo()
        {
            ConfigurationGroup = new List<BaseConfigurationItem<string>>();
        }
      

        /// <summary>
        /// Gets or sets the configuration group.
        /// </summary>
        /// <value>The configuration group.</value>
        [DataMember]
        public List<BaseConfigurationItem<string>> ConfigurationGroup { get; set; }


        
    }
}
