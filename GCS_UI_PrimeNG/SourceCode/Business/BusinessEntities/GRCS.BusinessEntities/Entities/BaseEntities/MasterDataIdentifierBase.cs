/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MasterDataBase.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       :Mohammad
 * Created on   : 10/3/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for MasterDataBase class
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// MasterDataBase 
    /// </summary>
    [DataContract]
   public  class MasterDataIdentifierBase
    {
        public MasterDataIdentifierBase()
        {
        }

        public MasterDataIdentifierBase(long id, string name)
        {
            Id = id;
            Name = name;
        }
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public long Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}
