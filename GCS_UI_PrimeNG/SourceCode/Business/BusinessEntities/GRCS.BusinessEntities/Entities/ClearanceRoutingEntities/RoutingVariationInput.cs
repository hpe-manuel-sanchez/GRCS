using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    public class RoutingVariationInput
    {
        
        #region "Routing variation inputs"
        public Int64 ProjectID { get; set; }

        public Int64 ResourceID { get; set; }

        public short RequestTypeID { get; set; }

        public Int64 ContractID { get; set; }

        public Int64 RightSetID { get; set; }

        public bool IsNationalRouting { get; set; }

        #endregion
              
    }

}
