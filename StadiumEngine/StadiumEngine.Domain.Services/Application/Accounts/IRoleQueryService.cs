using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Application.Accounts;

public interface IRoleQueryService
{
    Task<List<Role>> GetRolesForLegalAsync( int legalId );
    Task<Dictionary<Permission, bool>> GetPermissionsForRoleAsync( int roleId, int legalId );
    Task<Dictionary<Stadium, bool>> GetStadiumsForRoleAsync( int roleId, int legalId );
}