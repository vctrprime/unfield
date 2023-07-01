using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Services.Facades.Accounts;
using StadiumEngine.Services.Checkers;

namespace StadiumEngine.Services.Core.Accounts;

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