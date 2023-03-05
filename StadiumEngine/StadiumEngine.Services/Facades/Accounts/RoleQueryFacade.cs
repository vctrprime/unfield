using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Services.Checkers;
using StadiumEngine.Services.Facades.Services.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class RoleQueryFacade : IRoleQueryFacade
{
    private readonly IAccountsAccessChecker _accountsAccessChecker;
    private readonly IRoleRepositoryFacade _roleRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;

    public RoleQueryFacade(
        IRoleRepositoryFacade roleRepositoryFacade,
        IStadiumRepository stadiumRepository,
        IAccountsAccessChecker accountsAccessChecker )
    {
        _roleRepositoryFacade = roleRepositoryFacade;
        _stadiumRepository = stadiumRepository;
        _accountsAccessChecker = accountsAccessChecker;
    }


    public async Task<List<Role>> GetRolesForLegal( int legalId ) => await _roleRepositoryFacade.GetRoles( legalId );

    public async Task<Dictionary<Permission, bool>> GetPermissionsForRole( int roleId, int legalId )
    {
        Role? role = await _roleRepositoryFacade.GetRole( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, legalId );

        List<Permission> permissions = await _roleRepositoryFacade.GetPermissions();
        List<Permission> rolePermissions = await _roleRepositoryFacade.GetPermissions( roleId );

        return permissions.ToDictionary(
            permission => permission,
            permission => rolePermissions.FirstOrDefault( rp => rp.Id == permission.Id ) != null );
    }

    public async Task<Dictionary<Stadium, bool>> GetStadiumsForRole( int roleId, int legalId )
    {
        Role? role = await _roleRepositoryFacade.GetRole( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, legalId );

        List<Stadium> stadiums = await _stadiumRepository.GetForLegal( legalId );

        return stadiums.ToDictionary(
            stadium => stadium,
            stadium => role!.RoleStadiums.FirstOrDefault( rs => rs.RoleId == roleId && stadium.Id == rs.StadiumId ) !=
                       null );
    }
}