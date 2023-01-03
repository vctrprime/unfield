using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class RolePermissionRepository : BaseRepository<RolePermission>, IRolePermissionRepository
{
    public RolePermissionRepository(MainDbContext context) : base(context)
    {
    }

    public new void Add(RolePermission rolePermission)
    {
        base.Add(rolePermission);
    }

    public new void Add(IEnumerable<RolePermission> rolePermissions)
    {
        base.Add(rolePermissions);
    }
}