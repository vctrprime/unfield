using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Services.Facades.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class RoleRepositoryFacade : IRoleRepositoryFacade
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleStadiumRepository _roleStadiumRepository;

    public RoleRepositoryFacade(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository,
        IRoleStadiumRepository roleStadiumRepository )
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _roleStadiumRepository = roleStadiumRepository;
    }

    public async Task<List<Role>> GetRolesAsync( int legalId ) => await _roleRepository.GetAllAsync( legalId );

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

    public async Task<RoleStadium?> GetRoleStadiumAsync( int roleId, int stadiumId ) =>
        await _roleStadiumRepository.GetAsync( roleId, stadiumId );

    public void AddRoleStadium( RoleStadium roleStadium ) => _roleStadiumRepository.Add( roleStadium );

    public void RemoveRoleStadium( RoleStadium roleStadium ) => _roleStadiumRepository.Remove( roleStadium );
}