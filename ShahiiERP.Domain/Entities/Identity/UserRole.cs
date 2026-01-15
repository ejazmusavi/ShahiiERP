using ShahiiERP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShahiiERP.Domain.Entities.Identity
{
    public class UserRole:TenantScopedEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
