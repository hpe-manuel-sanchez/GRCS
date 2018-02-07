/* ************************************************************************ 
 * Copyrights ? 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsDownstream.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 12-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *
 * Description   : Entities used for creating Rights Data for Contract,
 *                 Project, Artist, Resource, Release and Tracks
 * 
*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsEntities
{
    class RightsDownstream
    {
    }

    #region DATASET

    /// <summary>
    /// Holds the Exploitation Data
    /// </summary>
    /// <returns></returns>
    public class ExploitationDataSet
    {
        [DataMember]
        public long? RightSetId { get; set; }
        [DataMember]
        public byte? ExploitationType { get; set; }
        [DataMember]
        public string ExploitationTypeDescription { get; set; }
        [DataMember]
        public bool? IsRestriction { get; set; }
    }

    /// <summary>
    /// Holds the Restriction Data
    /// </summary>
    /// <returns></returns>
    public class RestrictionDataSet
    {
        [DataMember]
        public long? RightSetId { get; set; }
        [DataMember]
        public byte? ContentType { get; set; }
        [DataMember]
        public byte? UseType { get; set; }
        [DataMember]
        public byte? CommercialModelType { get; set; }
        [DataMember]
        public string ContentTypeDescription { get; set; }
        [DataMember]
        public string UseTypeDescription { get; set; }
        [DataMember]
        public string CommercialModelTypeDescription { get; set; }
    }

    /// <summary>
    /// Holds the RightsAcquired Data
    /// </summary>
    /// <returns></returns>
    public class RightAcquiredDataSet
    {
        [DataMember]
        public long RightSetId { get; set; }
        [DataMember]
        public byte RightAcquiredType { get; set; }
        [DataMember]
        public string RightAcquiredTypeDescription { get; set; }
        [DataMember]
        public bool? IsAcquired { get; set; }
        [DataMember]
        public byte? ActivePassiveType { get; set; }
        [DataMember]
        public string ActivePassiveTypeDescription { get; set; }

    }

    /// <summary>
    /// Holds the RightSet Data
    /// </summary>
    /// <returns></returns>
    public class RightDataSet
    {
        [DataMember]
        public long RightSetId { get; set; }
        [DataMember]
        public DateTime? LostRightDate { get; set; }
        [DataMember]
        public bool? IsLostRight { get; set; }
        [DataMember]
        public byte? ReviewStatus { get; set; }
        [DataMember]
        public string ReviewStatusDescription { get; set; }
        [DataMember]
        public bool? IsDigitalUnbundle { get; set; }
        [DataMember]
        public List<RightAcquiredDataSet> AvailableRightAcquired { get; set; }
        [DataMember]
        public List<RightAcquiredDataSet> Deal360RightAcquired { get; set; }
        [DataMember]
        public List<ExploitationDataSet> SecExploitation { get; set; }
        [DataMember]
        public List<RestrictionDataSet> DigRestriction { get; set; }
        [DataMember]
        public List<string> CountryCodeGroup { get; set; }

    }

    /// <summary>
    /// Holds the Country IsoCode2 Data
    /// </summary>
    /// <returns></returns>
    public class CountryDataSet
    {
        [DataMember]
        public string IsoCode2 { get; set; }
    }

    /// <summary>
    /// Holds the Contract Data
    /// </summary>
    /// <returns></returns>
    public class ContractDataSet : EntityInformation
    {
        [DataMember]
        public long ContractId { get; set; }
        [DataMember]
        public long RightSetId { get; set; }
        [DataMember]
        public long? ArtistId { get; set; }
        [DataMember]
        public string ContractingParties { get; set; }
        [DataMember]
        public long AdminCompanyId { get; set; }
        [DataMember]
        public long? SigningCompanyId { get; set; }
        [DataMember]
        public string LocalReferenceNumber { get; set; }
        [DataMember]
        public byte? ContractStatusType { get; set; }
        [DataMember]
        public byte? WorkflowStatusType { get; set; }
        [DataMember]
        public long? PCompanyDefaultId { get; set; }
        [DataMember]
        public string PLineExtensionDefault { get; set; }
        [DataMember]
        public byte? RightsDefaultsType { get; set; }
        [DataMember]
        public DateTime? CommencementDt { get; set; }
        [DataMember]
        public RightDataSet ContractRightDataSet { get; set; }
        [DataMember]
        public string ContractStatusTypeDescription { get; set; }
        [DataMember]
        public string WorkflowStatusTypeDescription { get; set; }
        [DataMember]
        public string RightsDefaultTypeDescription { get; set; }
        [DataMember]
        public bool IsArchived { get; set; }
    }

    /// <summary>
    /// Holds the Project Data
    /// </summary>
    /// <returns></returns>
    public class ProjectDataSet
    {
        [DataMember]
        public long ProjectId { get; set; }
        [DataMember]
        public long RightSetId { get; set; }
        [DataMember]
        public List<ContractDataSet> ContractDataSets { get; set; }
    }

    /// <summary>
    /// Holds the Resource Data
    /// </summary>
    /// <returns></returns>
    public class ResourceDataSet
    {
        [DataMember]
        public long ResourceId { get; set; }
        [DataMember]
        public long R2ResourceId { get; set; }
        [DataMember]
        public string Isrc { get; set; }
        [DataMember]
        public List<ContractDataSet> ContractDataSets { get; set; }
        [DataMember]
        public List<RightDataSet> RightDataSets { get; set; }

    }

    /// <summary>
    /// Holds the Release Data
    /// </summary>
    /// <returns></returns>
    public class ReleaseDataSet
    {
        [DataMember]
        public long ReleaseId { get; set; }
        [DataMember]
        public long R2ReleaseId { get; set; }
        [DataMember]
        public string Upc { get; set; }
        [DataMember]
        public List<ContractDataSet> ContractDataSets { get; set; }
        [DataMember]
        public List<RightDataSet> RightDataSets { get; set; }
    }

    /// <summary>
    /// Holds the Track Data
    /// </summary>
    /// <returns></returns>
    public class TrackDataSet
    {
        [DataMember]
        public long TrackId { get; set; }
        [DataMember]
        public string Upc { get; set; }
        [DataMember]
        public string Isrc { get; set; }
        [DataMember]
        public long ResourceId { get; set; }
        [DataMember]
        public long ReleaseId { get; set; }
        [DataMember]
        public List<RightDataSet> RightDataSets { get; set; }
        [DataMember]
        public bool IsArchived { get; set; }
    }

    /// <summary>
    /// Holds the Artist Data
    /// </summary>
    /// <returns></returns>
    public class ArtistDataSet
    {
        [DataMember]
        public long ArtistId { get; set; }
        [DataMember]
        public List<ContractDataSet> ContractDataSets { get; set; }
    }
    

    #endregion



}
