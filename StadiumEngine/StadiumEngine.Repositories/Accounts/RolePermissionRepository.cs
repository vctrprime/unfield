using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class RolePermissionRepository : BaseRepository<RolePermission>, IRolePermissionRepository
{
    public RolePermissionRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<RolePermission?> GetAsync( int roleId, int permissionId ) =>
        await Entities.FirstOrDefaultAsync( rp => rp.RoleId == roleId && rp.PermissionId == permissionId );

    public new void Add( RolePermission rolePermission ) => base.Add( rolePermission );

    public new void Remove( RolePermission rolePermission ) => base.Remove( rolePermission );
}