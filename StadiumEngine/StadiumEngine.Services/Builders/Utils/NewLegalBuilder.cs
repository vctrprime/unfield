using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Builders.Utils;

internal class NewLegalBuilder : INewLegalBuilder
{
    private readonly IHasher _hasher;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPermissionRepository _permissionRepository;

    public NewLegalBuilder(
        IPermissionRepository permissionRepository,
        IPasswordGenerator passwordGenerator,
        IHasher hasher )
    {
        _permissionRepository = permissionRepository;
        _passwordGenerator = passwordGenerator;
        _hasher = hasher;
    }

    public async Task<string> Build( Legal legal, User superuser )
    {
        var superuserPassword = _passwordGenerator.Generate( 8 );
        superuser.Password = _hasher.Crypt( superuserPassword );

        legal.Roles = new List<Role>
        {
            await GetBaseRole( legal.Stadiums )
        };

        legal.Users = new List<User>
        {
            superuser
        };

        return superuserPassword;
    }

    private async Task<Role> GetBaseRole( IEnumerable<Stadium> stadiums )
    {
        var role = new Role
        {
            Name = "Администратор",
            Description = "Базовая роль для администратора (добавлена автоматически)",
            RolePermissions = await GetRolePermissions(),
            RoleStadiums = GetRoleStadiums( stadiums )
        };

        return role;
    }

    private async Task<List<RolePermission>> GetRolePermissions()
    {
        var permissions = await _permissionRepository.GetAll();
        var permissionsKeys = new List<string>
        {
            "schedule", "offers"
        };

        var rolePermissions = permissions.Where( p => permissionsKeys.Contains( p.PermissionGroup.Key ) ).Select(
            p => new RolePermission
            {
                Permission = p
            } );
        return rolePermissions.ToList();
    }

    private List<RoleStadium> GetRoleStadiums( IEnumerable<Stadium> stadiums )
    {
        var roleStadiums = stadiums.Select(
            s => new RoleStadium
            {
                Stadium = s
            } );

        return roleStadiums.ToList();
    }
}