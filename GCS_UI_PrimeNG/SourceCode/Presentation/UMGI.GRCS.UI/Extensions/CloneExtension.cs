/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : CloneExtension.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 28-08-2012 
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
using System.IO;
using System.Runtime.Serialization;

namespace UMGI.GRCS.UI.Extensions
{
    /// <summary>
    /// Extension Class with method to clone data cotract
    /// </summary>
    public static class CloneExtension
    {
        /// <summary>
        /// Clones the data contract.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static T CloneDataContract<T>(this T source)
        {
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var dataContractSerializer = new DataContractSerializer(typeof(T));
            var stream = new MemoryStream();
            using (stream)
            {
                dataContractSerializer.WriteObject(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)dataContractSerializer.ReadObject(stream);
            }
        }
    }
}