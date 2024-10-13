using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Services.Facades.Accounts;
using Unfield.Services.Checkers;

namespace Unfield.Services.Core.Accounts;

internal class RoleQueryService : IRoleQueryService
{
    private readonly IAccountsAccessChecker _accountsAccessChecker;
    private readonly IRoleRepositoryFacade _roleRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;

    public RoleQueryService(
        IRoleRepositoryFacade roleRepositoryFacade,
        IStadiumRepository stadiumRepository,
        IAccountsAccessChecker accountsAccessChecker )
    {
        _roleRepositoryFacade = roleRepositoryFacade;
        _stadiumRepository = stadiumRepository;
        _accountsAccessChecker = accountsAccessChecker;
    }


    public async Task<List<Role>> GetRolesForStadiumGroupAsync( int stadiumGroupId ) =>
        await _roleRepositoryFacade.GetRolesAsync( stadiumGroupId );

    public async Task<Dictionary<Permission, bool>> GetPermissionsForRoleAsync( int roleId, int stadiumGroupId )
    {
        Role? role = await _roleRepositoryFacade.GetRoleAsync( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, stadiumGroupId );

        List<Permission> permissions = await _roleRepositoryFacade.GetPermissionsAsync();
        List<Permission> rolePermissions = await _roleRepositoryFacade.GetPermissionsAsync( roleId );

        return permissions.ToDictionary(
            permission => permission,
            permission => rolePermissions.FirstOrDefault( rp => rp.Id == permission.Id ) != null );
    }
}