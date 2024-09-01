using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal interface IRoleRepositoryFacade
{
    Task<List<Role>> GetRolesAsync( int stadiumGroupId );
    Task<Role?> GetRoleAsync( int roleId );
    void AddRole( Role role );
    void UpdateRole( Role role );
    void RemoveRole( Role role );
    Task<List<Permission>> GetPermissionsAsync();
    Task<List<Permission>> GetPermissionsAsync( int roleId );
    Task<RolePermission?> GetRolePermissionAsync( int roleId, int permissionId );
    void AddRolePermission( RolePermission rolePermission );
    void RemoveRolePermission( RolePermission rolePermission );
}