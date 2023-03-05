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
        string superuserPassword = _passwordGenerator.Generate( 8 );
        superuser.Password = _hasher.Crypt( superuserPassword );

        legal.Roles = new List<Role> { await GetBaseRole( legal.Stadiums ) };

        legal.Users = new List<User> { superuser };

        return superuserPassword;
    }

    private async Task<Role> GetBaseRole( IEnumerable<Stadium> stadiums )
    {
        Role role = new()
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
        List<Permission> permissions = await _permissionRepository.GetAll();
        List<string> permissionsKeys = new()
        {
            "schedule",
            "offers"
        };

        IEnumerable<RolePermission> rolePermissions = permissions
            .Where( p => permissionsKeys.Contains( p.PermissionGroup.Key ) ).Select(
                p => new RolePermission { Permission = p } );
        return rolePermissions.ToList();
    }

    private List<RoleStadium> GetRoleStadiums( IEnumerable<Stadium> stadiums )
    {
        IEnumerable<RoleStadium> roleStadiums = stadiums.Select(
            s => new RoleStadium { Stadium = s } );

        return roleStadiums.ToList();
    }
}