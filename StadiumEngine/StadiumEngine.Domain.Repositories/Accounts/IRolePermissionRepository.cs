#nullable enable
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRolePermissionRepository
{
    Task<RolePermission?> Get( int roleId, int permissionId );
    void Add( RolePermission rolePermission );
    void Remove( RolePermission rolePermission );
}