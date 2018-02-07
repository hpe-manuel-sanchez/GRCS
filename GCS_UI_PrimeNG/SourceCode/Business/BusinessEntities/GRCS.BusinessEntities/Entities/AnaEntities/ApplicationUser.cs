/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ApplicationUser.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description     Declare ANA Entities
****************************************************************************/


using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// This class is the pace holder for searching ANA the list of users and the search
    /// </summary>
    [DataContract]
    [Serializable]
    public class ApplicationUser : EntityInformation
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string CountryName { get; set; }

    }
}