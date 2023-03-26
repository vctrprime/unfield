using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Facades.Services.Accounts;

internal interface IRoleRepositoryFacade
{
    Task<List<Role>> GetRolesAsync( int legalId );
    Task<Role?> GetRoleAsync( int roleId );
    void AddRole( Role role );
    void UpdateRole( Role role );
    void RemoveRole( Role role );
    Task<List<Permission>> GetPermissionsAsync();
    Task<List<Permission>> GetPermissionsAsync( int roleId );
    Task<RolePermission?> GetRolePermissionAsync( int roleId, int permissionId );
    void AddRolePermission( RolePermission rolePermission );
    void RemoveRolePermission( RolePermission rolePermission );
    Task<RoleStadium?> GetRoleStadiumAsync( int roleId, int stadiumId );
    void AddRoleStadium( RoleStadium roleStadium );
    void RemoveRoleStadium( RoleStadium roleStadium );
}