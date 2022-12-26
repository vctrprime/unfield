using Microsoft.EntityFrameworkCore;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Data.Infrastructure.Contexts;
using StadiumEngine.Entities.Domain.Accounts;
using StadiumEngine.Repositories.Abstract.Accounts;

namespace StadiumEngine.Repositories.Concrete.Accounts;

internal class UserPermissionRepository : BaseRepository, IUserPermissionRepository
{
    public UserPermissionRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<Permission>> Get(int userId)
    {
        var user = await Context.Users.FindAsync(userId);
        
        if (user is null) throw new DomainException("User not found!");

        if (user.IsSuperuser)
        {
            return await Context.Permissions.Include(p => p.PermissionGroup).ToListAsync();
        }

        return await Context.RolePermissions
            .Include(rp => rp.Permission)
            .ThenInclude(p => p.PermissionGroup)
            .Where(rp => rp.RoleId == user.RoleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}