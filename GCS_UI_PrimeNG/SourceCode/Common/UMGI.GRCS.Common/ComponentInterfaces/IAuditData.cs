using System.Data.Objects;
using UMGI.GRCS.BusinessEntities;
using System;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IAuditData
    {
        void Process(ObjectContext objectContext, AuditingTask taskName, DateTime createdDtm);
    }
}
