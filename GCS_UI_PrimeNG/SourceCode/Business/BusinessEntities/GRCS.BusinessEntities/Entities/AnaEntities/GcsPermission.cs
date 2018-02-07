/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GcsPermission.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Sudharani Dasari
 * Created on   : 02-11-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
                               
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// enumaration for GCS Permission
    /// </summary>
    [DataContract]
    [Serializable]
    public class GcsPermission
    {
        /// <summary>
        /// GCS Tasks Name
        /// </summary>
        [DataMember]
        public GcsTasks[] Tasks { get; set; }

        /// <summary>
        /// GCS Roles
        /// </summary>
        [DataMember]
        public string Role { get; set; }

        /// <summary>
        /// GCS DAG
        /// </summary>
		//[DataMember]
		//public ObservableCollection<DataAccessGroup> DataAccessGroups { get; set; }
    }
}