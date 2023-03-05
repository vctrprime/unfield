using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;

namespace StadiumEngine.Services.Facades.Services.Accounts;

internal class RoleRepositoryFacade : IRoleRepositoryFacade
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
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

    public async Task<List<Role>> GetRoles( int legalId )
    {
        return await _roleRepository.GetAll( legalId );
    }

    public async Task<Role?> GetRole( int roleId )
    {
        return await _roleRepository.Get( roleId );
    }

    public void AddRole( Role role )
    {
        _roleRepository.Add( role );
    }

    public void UpdateRole( Role role )
    {
        _roleRepository.Update( role );
    }

    public void RemoveRole( Role role )
    {
        _roleRepository.Remove( role );
    }

    public async Task<List<Permission>> GetPermissions()
    {
        return await _permissionRepository.GetAll();
    }

    public async Task<List<Permission>> GetPermissions( int roleId )
    {
        return await _permissionRepository.GetForRole( roleId );
    }

    public async Task<RolePermission?> GetRolePermission( int roleId, int permissionId )
    {
        return await _rolePermissionRepository.Get( roleId, permissionId );
    }

    public void AddRolePermission( RolePermission rolePermission )
    {
        _rolePermissionRepository.Add( rolePermission );
    }

    public void RemoveRolePermission( RolePermission rolePermission )
    {
        _rolePermissionRepository.Remove( rolePermission );
    }

    public async Task<RoleStadium?> GetRoleStadium( int roleId, int stadiumId )
    {
        return await _roleStadiumRepository.Get( roleId, stadiumId );
    }

    public void AddRoleStadium( RoleStadium roleStadium )
    {
        _roleStadiumRepository.Add( roleStadium );
    }

    public void RemoveRoleStadium( RoleStadium roleStadium )
    {
        _roleStadiumRepository.Remove( roleStadium );
    }
}