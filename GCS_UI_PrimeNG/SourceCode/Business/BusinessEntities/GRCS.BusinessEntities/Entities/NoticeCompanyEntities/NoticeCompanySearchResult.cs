using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : NoticeCompanySearchResult.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Notice Company Search Results
                  
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities
{
    public class NoticeCompanySearchResult : EntityInformation
    {
        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>The details.</value>
        [DataMember]
        public ObservableCollection<NoticeCompany> Details
        {
            get
            {
                var collection = new ObservableCollection<NoticeCompany>();
                foreach (var companyDetail in CompanyDetails)
                {
                    collection.Add(companyDetail);
                }
                return collection;
            }
           
        }

        /// <summary>
        /// Artist Details
        /// </summary>
        [DataMember]
        public List<NoticeCompany> CompanyDetails { get; set; }

        /// <summary>
        /// Gets or sets the index of the row.
        /// </summary>
        /// <value>The index of the row.</value>
        [DataMember]
        public long RowIndex { get; set; }

        /// <summary>
        /// Gets or sets the rows retreived.
        /// </summary>
        /// <value>The rows retreived.</value>
        [DataMember]
        public long RowsRetreived { get; set; }

        /// <summary>
        /// Gets or sets the r2 rows retrieved.
        /// </summary>
        /// <value>The r2 rows retrieved.</value>
        [DataMember]
        public int R2RowsRetrieved { get; set; }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}