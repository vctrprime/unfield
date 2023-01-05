using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<Permission>> GetAll()
    {
        return await Entities.Include(p => p.PermissionGroup)
            .OrderBy( p => p.PermissionGroup.Sort)
            .ThenBy( p => p.Sort)
            .ToListAsync();
    }

    public async Task<List<Permission>> GetForRole(int roleId)
    {
        return await Entities
            .Include(p => p.PermissionGroup)
            .Include(rp => rp.RolePermissions)
            .Where(p => p.RolePermissions.Select( rp => rp.RoleId).Contains(roleId))
            .OrderBy( p => p.PermissionGroup.Sort)
            .ThenBy( p => p.Sort)
            .ToListAsync();
    }

    
    public new void Add(Permission permission)
    {
        base.Add(permission);
    }

    public new void Update(Permission permission)
    {
        base.Update(permission);
    }
}