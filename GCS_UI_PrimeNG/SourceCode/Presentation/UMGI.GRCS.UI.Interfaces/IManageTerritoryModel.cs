using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IManageTerritoryModel<T> where T : class
    {
        List<T> Territories { get; set; }
        List<T> UpdatedTerritories { get; set; }
        AnaTargetApplication TargetApplication { get; set; }
    }
}
