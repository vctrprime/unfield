using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.Domain.Services.Infrastructure;

namespace Unfield.Services.Builders.Utils;

internal class NewStadiumGroupBuilder : INewStadiumGroupBuilder
{
    private readonly IHasher _hasher;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPermissionRepository _permissionRepository;

    public NewStadiumGroupBuilder(
        IPermissionRepository permissionRepository,
        IPasswordGenerator passwordGenerator,
        IHasher hasher )
    {
        _permissionRepository = permissionRepository;
        _passwordGenerator = passwordGenerator;
        _hasher = hasher;
    }

    public async Task<string> BuildAsync(StadiumGroup stadiumGroup, User superuser )
    {
        string superuserPassword = _passwordGenerator.Generate( 8 );
        superuser.Password = _hasher.Crypt( superuserPassword );

        stadiumGroup.Roles = new List<Role> { await GetBaseRole( stadiumGroup.Stadiums ) };

        stadiumGroup.Users = new List<User> { superuser };

        return superuserPassword;
    }

    private async Task<Role> GetBaseRole( IEnumerable<Stadium> stadiums )
    {
        Role role = new()
        {
            Name = "Администратор",
            Description = "Базовая роль для администратора (добавлена автоматически)",
            RolePermissions = await GetRolePermissions()
        };

        return role;
    }

    private async Task<List<RolePermission>> GetRolePermissions()
    {
        List<Permission> permissions = await _permissionRepository.GetAllAsync();
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
}