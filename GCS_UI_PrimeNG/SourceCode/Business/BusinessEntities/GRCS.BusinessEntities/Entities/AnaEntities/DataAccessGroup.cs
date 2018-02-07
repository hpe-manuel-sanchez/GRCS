/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DataAccessGroup.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil kumar
 * Created on   : 05-10-2012
 * Modified on  : 22-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for ANA
                  
****************************************************************************/
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    [DataContract]
    [Serializable]
    public class DataAccessGroup : EntityInformation
    {
        [DataMember]
        public DagFilterType DagFilterType { get; set; }

        /// <summary>
        /// GRS DataFilter
        /// </summary>
        [DataMember]
        public ObservableCollection<DataAccessGroupData> DataAccessGroupData { get; set; }
    }
}