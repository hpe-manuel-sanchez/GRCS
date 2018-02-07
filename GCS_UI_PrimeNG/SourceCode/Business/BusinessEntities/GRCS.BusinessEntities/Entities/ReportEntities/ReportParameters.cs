using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{
    [Serializable]
    public class ReportParameters
    {
        /// <summary>
        /// Name of scheduled Report
        /// </summary>
        public String ReportName { get; set; }

        /// <summary>
        /// Access credencials
        /// </summary>
        public String ReportUserName { get; set; }

        /// <summary>
        /// Access credencials
        /// </summary>
        public String ReportPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ReportDomain { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public String Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ServerPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String FTPPath { get; set; }

        /// <summary>
        /// Access credencials
        /// </summary>
        public String FTPUserName { get; set; }

        /// <summary>
        /// Access credencials
        /// </summary>
        public String FTPPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String FTPDomain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EmailReceipients { get; set; }

    }
}
