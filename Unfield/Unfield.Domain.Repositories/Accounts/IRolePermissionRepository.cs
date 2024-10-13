#nullable enable
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Repositories.Accounts;

public interface IRolePermissionRepository
{
    Task<RolePermission?> GetAsync( int roleId, int permissionId );
    void Add( RolePermission rolePermission );
    void Remove( RolePermission rolePermission );
}