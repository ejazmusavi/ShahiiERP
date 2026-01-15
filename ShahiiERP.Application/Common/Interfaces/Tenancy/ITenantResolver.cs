using System.Threading.Tasks;
using ShahiiERP.Application.Common.Models;

namespace ShahiiERP.Application.Common.Interfaces.Tenancy
{
    public interface ITenantResolver
    {
        Task<TenantContextModel?> ResolveAsync();
    }
}
