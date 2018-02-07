/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ResourceFileHelper.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : GRCS-Team
 * Created on     : 12-01-2013 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                   Modified on                       Remarks 
 *
*************************************************************************** */

using System;
using System.Resources;
using System.Collections;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace UMGI.GRCS.UI.Helper
{
    public static class ResourceFileHelper
    {
        public static string GetJson(Type resource, CultureInfo culture)
        {
            var resourceManager = new ResourceManager(resource);

            var resourceDictionary = (from resourceItem in resource.GetProperties(BindingFlags.Public | BindingFlags.Static)
                                      where resourceItem.PropertyType == typeof(string)
                                      select new KeyValuePair<string, string>(resourceItem.Name, resourceManager.GetString(resourceItem.Name, culture))
                                      ).ToDictionary(k => k.Key, v => v.Value);

            return new JavaScriptSerializer().Serialize(resourceDictionary);
        }
    }
}