using ShahiiERP.Domain.Common;
using System;

namespace ShahiiERP.Shared.Entities;

public class EmailVerification: TenantScopedEntity
{
    public Guid UserId { get; set; }

    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }

    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }

    public string TokenHash { get; set; } = default!;
}
