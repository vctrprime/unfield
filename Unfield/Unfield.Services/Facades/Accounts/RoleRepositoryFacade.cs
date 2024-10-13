using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Services.Facades.Accounts;

namespace Unfield.Services.Facades.Accounts;

internal class RoleRepositoryFacade : IRoleRepositoryFacade
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IRoleRepository _roleRepository;

    public RoleRepositoryFacade(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository )
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<List<Role>> GetRolesAsync( int stadiumGroupId ) => await _roleRepository.GetAllAsync( stadiumGroupId );

    public async Task<Role?> GetRoleAsync( int roleId ) => await _roleRepository.GetAsync( roleId );

    public void AddRole( Role role ) => _roleRepository.Add( role );

    public void UpdateRole( Role role ) => _roleRepository.Update( role );

    public void RemoveRole( Role role ) => _roleRepository.Remove( role );

    public async Task<List<Permission>> GetPermissionsAsync() => await _permissionRepository.GetAllAsync();

    public async Task<List<Permission>> GetPermissionsAsync( int roleId ) =>
        await _permissionRepository.GetForRoleAsync( roleId );

    public async Task<RolePermission?> GetRolePermissionAsync( int roleId, int permissionId ) =>
        await _rolePermissionRepository.GetAsync( roleId, permissionId );

    public void AddRolePermission( RolePermission rolePermission ) => _rolePermissionRepository.Add( rolePermission );

    public void RemoveRolePermission( RolePermission rolePermission ) =>
        _rolePermissionRepository.Remove( rolePermission );
}