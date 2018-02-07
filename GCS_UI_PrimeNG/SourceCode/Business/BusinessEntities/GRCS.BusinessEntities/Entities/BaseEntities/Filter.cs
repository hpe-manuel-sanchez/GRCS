/* ***************************************************************************  
* Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : Filter
 * Project Code :   
 * Author       : Siva
 * Created on   :  
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Siva              23/10/2012      Initial Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for filtering fields
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    public class Filter : EntityInformation
    {
        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        [DataMember]
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [DataMember]
        public string Value { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
