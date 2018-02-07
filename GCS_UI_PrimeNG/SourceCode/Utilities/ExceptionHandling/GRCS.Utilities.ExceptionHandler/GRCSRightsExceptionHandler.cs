using System;
using System.ComponentModel.Composition;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Constants;
using UMGI.GRCS.Common.ComponentInterfaces;
using UMGI.GRCS.Core.Utilities.Exceptions;
using UMGI.GRCS.Resx.Resource.EntityResource;

namespace GRCS.Utilities.ExceptionHandler
{
    [Export(typeof(IGrscExceptionHandler))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GrcsRightsExceptionHandler : IGrscExceptionHandler
    {
        [Import(typeof(IUserManager))]
        private Lazy<IUserManager> _usrerManagerAdapter { get; set; }

        /// <summary>
        /// GRCSs the fault exception.
        /// </summary>
        /// <param name="faultCode">The fault code.</param>
        /// <param name="message">The message.</param>
        /// <param name="rightsException">The rights exception.</param>
        /// <returns></returns>
        public FaultException<GrcsException> GrcsFaultException(string faultCode, string message, GrcsException rightsException)
        {
            //  Avoid using this
            if (string.IsNullOrEmpty(faultCode))
                faultCode = "0000";
            return new FaultException<GrcsException>(rightsException, new FaultReason(message), new FaultCode(faultCode));
        }

        /// <summary>
        /// GRCSs the fault exception.
        /// </summary>
        /// <param name="faultCode">The fault code.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public FaultException GrcsFaultException(string faultCode, string message)
        {
            if (string.IsNullOrEmpty(faultCode)) faultCode = "0000";
            return new FaultException(new FaultReason(message), new FaultCode(faultCode));
        }

        public RightsException GrcsConcurrencyException(string userName, string faultCode)
        {
            //try
            //{
            string userDisplayName = _usrerManagerAdapter.Value.GetDisplayName(userName);
            switch (faultCode)
            {
                case GrsErrorCode.SaveConcurrency:
                    string saveConcMsg = string.Format(Entity.ConcurrencyHandling, userDisplayName);
                    return new RightsException(GrsErrorCode.SaveConcurrency, saveConcMsg);

                case GrsErrorCode.LinkingConcurrency:
                    string linkConcMsg = string.Format(Entity.ConcurrencyHandling, userDisplayName);
                    return new RightsException(GrsErrorCode.LinkingConcurrency, linkConcMsg);

                case GrsErrorCode.UnlinkingConcurrency:
                    string unLinkConcMsg = string.Format(Entity.UnlinkingConcurrency, userDisplayName);
                    return new RightsException(GrsErrorCode.UnlinkingConcurrency, unLinkConcMsg);

                case GrsErrorCode.SplitDeleteConc:
                    string splitConcMsg = string.Format(Entity.SplitDeleteConc, userDisplayName);
                    return new RightsException(GrsErrorCode.SplitDeleteConc, splitConcMsg);

                case GrsErrorCode.DeleteConcurrency:
                    string deleteConcMsg = string.Format(Entity.DeleteConcurrency, userDisplayName);
                    return new RightsException(GrsErrorCode.DeleteConcurrency, deleteConcMsg);

                case GrsErrorCode.WorkFlowStatusConcError:
                    string workflowStatusErrorMsg = string.Format(Entity.WorkflowStatusConcError, userDisplayName);
                    return new RightsException(GrsErrorCode.WorkFlowStatusConcError, workflowStatusErrorMsg);

                case GrsErrorCode.RemoveWorkQueueConc:
                    string removeWorkQueueMsg = string.Format(Entity.RemoveWorkQueueError, userDisplayName);
                    return new RightsException(GrsErrorCode.RemoveWorkQueueConc, removeWorkQueueMsg);

                case GrsErrorCode.ParentConcError:
                    string parentContrctMsg = string.Format(Entity.ParentContrctConcMsg, userDisplayName);
                    return new RightsException(GrsErrorCode.ParentConcError, parentContrctMsg);

                case GrsErrorCode.DeleteContractConc:
                    string deleteContrctMsg = string.Format(Entity.DeleteContractConcMsg, userDisplayName);
                    return new RightsException(GrsErrorCode.DeleteContractConc, deleteContrctMsg);
            }

            return null;
        }
    }
}