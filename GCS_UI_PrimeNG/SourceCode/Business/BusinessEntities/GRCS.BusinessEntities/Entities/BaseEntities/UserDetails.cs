/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    :   UserDetails.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       :         Pavan Kumar K
 * Created on   :    21-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the User Table entities                                    
                  
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    public class UserDetails : EntityInformation
    {
        /// <summary>
        /// Gets or sets the name of the login.
        /// </summary>
        /// <value>The name of the login.</value>
        public string LoginName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the hash key.
        /// </summary>
        /// <value>The hash key.</value>
        public string HashKey { get; set; }

        /// <summary>
        /// Gets or sets the user AnA permission XML string.
        /// </summary>
        /// <value>The user AnA permission XML string.</value>
        public string UserAnAPermissionXmlString { get; set; }
    }
}
