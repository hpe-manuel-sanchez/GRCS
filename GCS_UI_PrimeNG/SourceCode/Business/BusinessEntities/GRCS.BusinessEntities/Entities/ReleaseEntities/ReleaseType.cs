/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ReleaseType.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : Vijayakumar.R
 * Created on   : 03/10/2012
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Declare ReleaseType Enum

****************************************************************************/

using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
   [DataContract]
   [Serializable]
   public enum ReleaseType
    {
       [EnumMember] Digital,

       [EnumMember] Physical
    }
}
