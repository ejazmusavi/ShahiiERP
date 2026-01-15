using ShahiiERP.Domain.Tenants;
using System;

namespace ShahiiERP.Application.Common.Models
{
    public class TenantContextModel
    {
        public Guid TenantId { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? ConnectionString { get; set; }
        public TenantDatabaseMode DatabaseMode { get; set; }
    }
}
