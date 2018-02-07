/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GrsPermission.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 * Rengaraj          03-Aug-2012     modified code for coding standard format                                    
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// enumaration for GRS Permission
    /// </summary>
    [DataContract]
    [Serializable]
    public class GrsPermission : EntityInformation
    {

        /// <summary>
        /// Determines whether this instance has dag.
        /// </summary>
        /// <returns>true if Dag exists</returns>
        public bool HasDag()
        {
            if(CompanyIds == null)
            {
                if(CountryIds == null)
                {
                    return false;
                }
                return CountryIds.Count > 0;
            }
            if (CountryIds == null)
            {
                return CompanyIds.Count > 0;
            }
            return CompanyIds.Count > 0 || CountryIds.Count > 0;
        }

        /// <summary>
        /// GRS Tasks Name
        /// </summary>
        [DataMember]
        public GrsTasks Tasks { get; set; }

        /// <summary>
        /// GRS Roles
        /// </summary>
        [DataMember]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the country ids.
        /// </summary>
        /// <value>The country ids.</value>
        [DataMember]
        public List<long> CountryIds { get; set; }

        /// <summary>
        /// Gets or sets the company ids.
        /// </summary>
        /// <value>The company ids.</value>
        [DataMember]
        public Dictionary<long, long> CompanyIds { get; set; }

        /// <summary>
        /// Gets or sets the work flow ids.
        /// </summary>
        /// <value>The work flow ids.</value>
        [DataMember]
        public List<long> WorkFlowIds { get; set; }

        /// <summary>
        /// Gets or sets the contract status ids.
        /// </summary>
        /// <value>The contract status ids.</value>
        [DataMember]
        public List<long> ContractStatusIds { get; set; }

        /// <summary>
        /// GRS DAG
        /// </summary>
        [Obsolete]
        [DataMember]
        public ObservableCollection<DataAccessGroup> DataAccessGroups { get; set; }

        public GrsPermission()
        {
            CountryIds = new List<long>();
            CompanyIds = new Dictionary<long, long>();
            WorkFlowIds = new List<long>();
            ContractStatusIds = new List<long>();
        }
    }
}