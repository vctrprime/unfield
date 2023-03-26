#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRolePermissionRepository
{
    Task<RolePermission?> GetAsync( int roleId, int permissionId );
    void Add( RolePermission rolePermission );
    void Remove( RolePermission rolePermission );
}