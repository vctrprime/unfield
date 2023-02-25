using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Facades.Services.Accounts;

internal interface IRoleRepositoryFacade
{
    Task<List<Role>> GetRoles(int legalId);
    Task<Role?> GetRole(int roleId);
    void AddRole(Role role);
    void UpdateRole(Role role);
    void RemoveRole(Role role);
    Task<List<Permission>> GetPermissions();
    Task<List<Permission>> GetPermissions(int roleId);
    Task<RolePermission?> GetRolePermission(int roleId, int permissionId);
    void AddRolePermission(RolePermission rolePermission);
    void RemoveRolePermission(RolePermission rolePermission);
    Task<RoleStadium?> GetRoleStadium(int roleId, int stadiumId);
    void AddRoleStadium(RoleStadium roleStadium);
    void RemoveRoleStadium(RoleStadium roleStadium);
}