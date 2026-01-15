using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShahiiERP.Domain.Entities.Identity;

namespace ShahiiERP.Infrastructure.Persistence.Configurations.Tenant
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
        }
    }
}
