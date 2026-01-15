using ShahiiERP.Domain.Entities;
using System;

namespace ShahiiERP.Domain.Common
{
    public abstract class TenantScopedEntity : BaseEntity
    {
        public Guid TenantId { get; set; }
    }
}
