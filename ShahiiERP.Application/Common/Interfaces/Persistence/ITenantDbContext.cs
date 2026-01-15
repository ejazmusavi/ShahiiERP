using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShahiiERP.Domain.Entities.Identity;

namespace ShahiiERP.Application.Common.Interfaces.Persistence
{
    public interface ITenantDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DatabaseFacade Database { get; }
    }
}
