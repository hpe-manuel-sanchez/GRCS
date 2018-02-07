/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : Routing Variation.cs
 * Project      : UMG GRCS
 * Author       : Arunagiri G
 * Created on   : 11-03-2013 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Routing variation properties

****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    public class RoutedVariation
    {

        public int RoutingRuleID { get; set; }

        public string RuleName { get; set; }

        public int RoutingVariationID { get; set; }

        public bool Is_Skip_National { get; set; }

        public bool Is_Skip_International { get; set; }

        public bool Is_Skip_Locallabel { get; set; }

        public bool Is_Parent { get; set; }

    }

}
