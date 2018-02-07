/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClassInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Pavan Kumar      06/08/2012       Log formatted to write per line. Code Review updates
 * Senthil Kumar    29/08/2012       Removed Junk Characters that comes along with ToString()
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Base class - Overrides the ToString implementation, to return the properties and respective values.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ClassInfo
    {
        // List of Unnecessary strings which comes along with the parameter name
        // If Enabled, the class name will also be printed along with the parameters
        // List of BindingFlags used to filter from the Class <T>
        private const BindingFlags BindingFlag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        // To Get the parameters of the base class, recursion is used. Following variables control
        // the recursion, min depth is 1 and max depth is 2;
        private const int CurrentClassDepth = 1;
        private const int MaxParentClassDepth = CurrentClassDepth + 1;
        private static readonly string[] InvalidTokens = new[] {"k__BackingField", "<", ">"};

        /// <summary>
        /// Replaces the unneccesary tokens comes along with the paramter Fields
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <returns></returns>
        private string GetParameterName(string paramName)
        {
            // Replace all tokens with ""
            return InvalidTokens.Aggregate(paramName, (current, toReplace) => current.Replace(toReplace, String.Empty));
        }

        /// <summary>
        /// This method will be called recursively(CONTROLLED) to fetch the paramters of the class
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="baseClassDepth">The base class depth.</param>
        /// <returns></returns>
        private static IEnumerable<FieldInfo> GetAllFields(System.Type t, int baseClassDepth)
        {
            // Not allowing to go beyond the configured Depth
            // If the Type is ClassInfo(this class), not allowing to go beyond
            if (t == null || t == typeof (ClassInfo) || baseClassDepth > MaxParentClassDepth)
                return Enumerable.Empty<FieldInfo>();
            return GetAllFields(t.BaseType, baseClassDepth + 1).Union(t.GetFields(BindingFlag));
        }


        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var data = new StringBuilder();

            data.AppendFormat("Object: {0}, Parameters: \n", GetType().Name);
            IEnumerable<FieldInfo> fields = GetAllFields(GetType(), CurrentClassDepth);
            foreach (FieldInfo field in fields)
            {
                data.AppendLine(String.Format("{0} := {1}", GetParameterName(field.Name), field.GetValue(this)));
            }
            //if (fields.Count() > 0) // to format as {xx,xx,xx,xx}
            //    data = data.Substring(0, data.LastIndexOf(','));
            //return data + "}\n";
            return data.ToString();
        }
    }
}