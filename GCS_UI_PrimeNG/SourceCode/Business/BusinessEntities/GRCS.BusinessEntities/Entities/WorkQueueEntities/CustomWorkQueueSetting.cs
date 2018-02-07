/* ***************************************************************************  
* Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : CustomWorkQueueSetting.cs
 * Project Code :   
 * Author       : Rubini Raja
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Work queue custom setting properties for other parameters
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    /// <summary>
    /// Work queue custom setting properties
    ///         for other parameters
    /// </summary>
    [DataContract]
    public class CustomWorkQueueSetting : ResourceParameters
    {
        /// <summary>
        /// Represents the Filter Type of the Work queue
        /// </summary>
        [DataMember]
        public WorkQueueFilterType WorkQueueFilter
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the Id of the filter type
        /// 
        /// </summary>
        [DataMember]
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the description of the Work queue
        /// </summary>
        [DataMember]
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the Artist Name in the filter
        /// </summary>
        [DataMember]
        public string ArtistName
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the Data admin company in the filter
        /// </summary>
        [DataMember]
        public int DataAdminCompany
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the start week of release date filter
        /// </summary>
        [DataMember]
        public int StartWeek
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the end week of release date filter
        /// </summary>
        [DataMember]
        public int EndWeek
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the Repertoire Type - Project/Release/Resource
        /// </summary>
        [DataMember]
        public string RepertoireType
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the parameter pre defined.
        /// </summary>
        /// <value>The parameter pre defined.</value>
        [DataMember]
        public string parameterPreDefined
        {
            get;
            set;
        }        
    }
}
