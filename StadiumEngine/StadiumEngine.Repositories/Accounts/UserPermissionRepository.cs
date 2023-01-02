using Microsoft.EntityFrameworkCore;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class UserPermissionRepository : BaseRepository<RolePermission>, IUserPermissionRepository
{
    private DbSet<Permission> _permissions;
    
    public UserPermissionRepository(MainDbContext context) : base(context)
    {
        _permissions = context.Set<Permission>();
    }

    public async Task<List<Permission>> Get(int roleId, bool isSuperuser)
    {
        if (isSuperuser)
        {
            return await _permissions.Include(p => p.PermissionGroup).ToListAsync();
        }

        return await Entities
            .Include(rp => rp.Permission)
            .ThenInclude(p => p.PermissionGroup)
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}