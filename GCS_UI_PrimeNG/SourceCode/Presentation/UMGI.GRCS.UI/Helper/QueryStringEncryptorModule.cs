using System;
using System.Web;


namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryStringEncryptorModule : IHttpModule
    {
        #region IHttpModule Members
        
        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose 
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        #endregion

        private const string ParameterName = "enc=";

        /// <summary>
        /// Handles the BeginRequest event of the context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void context_BeginRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;

            if (!context.Request.Url.OriginalString.Contains("?") || !context.Request.RawUrl.Contains("?")) return;
            var query = ExtractQuery(context.Request.RawUrl);
            var path = GetVirtualPath();

            if (query.StartsWith(ParameterName, StringComparison.OrdinalIgnoreCase))
            {
                // Decrypts the query string and rewrites the path. 
                var rawQuery = query.Replace(ParameterName, string.Empty);
                var decryptedQuery = EncryptionUtility.Decrypt(rawQuery);
                context.RewritePath(path, string.Empty, decryptedQuery);
            }
            else if (context.Request.HttpMethod == "GET")
            {
                // Encrypt the query string and redirects to the encrypted URL. 
                // Remove if you don't want all query strings to be encrypted automatically. 
                var encryptedQuery = "?" + ParameterName + EncryptionUtility.Encrypt(query);
                context.Response.Redirect(path + encryptedQuery, false);
            }
        }

        /// <summary> 
        /// Parses the current URL and extracts the virtual path without query string. 
        /// </summary> 
        /// <returns>The virtual path of the current URL.</returns> 
        private static string GetVirtualPath()
        {
            var path = HttpContext.Current.Request.RawUrl;
            path = path.Substring(0, path.IndexOf("?"));
            path = path.Substring(path.LastIndexOf("/") + 1);
            return path;
        }

        /// <summary> 
        /// Parses a URL and returns the query string. 
        /// </summary> 
        /// <param name="url">The URL to parse.</param> 
        /// <returns>The query string without the question mark.</returns> 
        private static string ExtractQuery(string url)
        {
            var index = url.IndexOf("?") + 1;
            return url.Substring(index);
        }
    }
}