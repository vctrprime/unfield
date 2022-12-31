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

    public async Task<List<Permission>> Get(int userId)
    {
        var user = await Users.FindAsync(userId);
        
        if (user is null) throw new DomainException("User not found!");

        if (user.IsSuperuser)
        {
            return await _permissions.Include(p => p.PermissionGroup).ToListAsync();
        }

        return await Entities
            .Include(rp => rp.Permission)
            .ThenInclude(p => p.PermissionGroup)
            .Where(rp => rp.RoleId == user.RoleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}