/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MPRollUpRestrictions.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : G Senthil Kumar
 * Created on   : 04-03-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                                  
*************************************************************************** 
 * Reviewed by      Modified on     Remarks 
 */


using System.Collections.Generic;
using System.Linq;

namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class MPRollUpRestrictions
    {
        public static List<MPRightsRestrictions> GetRolledUpRestrictions(List<MPRightsRestrictions> _sourceRestrictions)
        {
            var targetRollUpRestrictions = new List<MPRightsRestrictions>();

            MPRightsRestrictions[] sourceRestrictions = _sourceRestrictions.ToArray();

            for (int i = 0; i < sourceRestrictions.Count(); i++)
            {
                MPRightsRestrictions sourceRollupRestrictions = sourceRestrictions[i];
                if (sourceRollupRestrictions == null) continue;
                if (targetRollUpRestrictions.Count == 0)
                {
                    targetRollUpRestrictions.Add(sourceRollupRestrictions);
                }
                else
                {
                    List<MPRightsRestrictions> existingRestrictions = targetRollUpRestrictions.ToList();

                    bool isEqual = false;
                    foreach (MPRightsRestrictions targetRestrictions in existingRestrictions)
                    {
                        if (sourceRollupRestrictions == null)
                            continue;
                        if (MPRightsRestrictionsComparer.Equals(sourceRollupRestrictions, targetRestrictions))
                        {
                            isEqual = true;
                            foreach (
                                string countryCode in
                                    sourceRollupRestrictions.CountryIsoCodes.Where(
                                        countryCode => !targetRestrictions.CountryIsoCodes.Contains(countryCode)))
                            {
                                targetRestrictions.CountryIsoCodes.Add(countryCode);
                            }
                            _sourceRestrictions[i] = null;
                            sourceRollupRestrictions = null;
                        }
                    }

                    if (isEqual == false)
                    {
                        targetRollUpRestrictions.Add(sourceRollupRestrictions);
                        _sourceRestrictions[i] = null;
                    }
                }
            }


            return targetRollUpRestrictions;
        }
    }
}