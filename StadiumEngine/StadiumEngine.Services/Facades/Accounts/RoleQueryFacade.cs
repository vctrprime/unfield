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


    public async Task<List<Role>> GetRolesForLegalAsync( int legalId ) => await _roleRepositoryFacade.GetRolesAsync( legalId );

    public async Task<Dictionary<Permission, bool>> GetPermissionsForRoleAsync( int roleId, int legalId )
    {
        Role? role = await _roleRepositoryFacade.GetRoleAsync( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, legalId );

        List<Permission> permissions = await _roleRepositoryFacade.GetPermissionsAsync();
        List<Permission> rolePermissions = await _roleRepositoryFacade.GetPermissionsAsync( roleId );

        return permissions.ToDictionary(
            permission => permission,
            permission => rolePermissions.FirstOrDefault( rp => rp.Id == permission.Id ) != null );
    }

    public async Task<Dictionary<Stadium, bool>> GetStadiumsForRoleAsync( int roleId, int legalId )
    {
        Role? role = await _roleRepositoryFacade.GetRoleAsync( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, legalId );

        List<Stadium> stadiums = await _stadiumRepository.GetForLegalAsync( legalId );

        return stadiums.ToDictionary(
            stadium => stadium,
            stadium => role!.RoleStadiums.FirstOrDefault( rs => rs.RoleId == roleId && stadium.Id == rs.StadiumId ) !=
                       null );
    }
}