/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ControlPermissions.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 25-08-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */
using System;
using System.Collections.Generic;
using UMGI.GRCS.UI.Utilities;
using System.Xml.Serialization;
using System.Xml;
using System.Web;

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Helper class for permission
    /// </summary>
    public static class ControlPermissions
    {
        /// <summary>
        /// Gets the permissions of all controls.
        /// </summary>
        /// <returns></returns>
        public static List<PermissionEntity> GetControlPermissions()
        {
            try
            {
                var serialiseXml = new XmlSerializer(typeof(List<PermissionEntity>));
                using (var readxml = XmlReader.Create(HttpContext.Current.Server.MapPath("~\\App_Data\\ControlPermissions.xml")))
                {
                    return (List<PermissionEntity>)serialiseXml.Deserialize(readxml);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}