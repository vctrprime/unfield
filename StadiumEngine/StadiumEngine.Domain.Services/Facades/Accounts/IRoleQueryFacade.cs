using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IRoleQueryFacade
{
    Task<List<Role>> GetRolesForLegal(int legalId);
    Task<Dictionary<Permission, bool>> GetPermissionsForRole(int roleId, int legalId);
    Task<Dictionary<Stadium, bool>> GetStadiumsForRole(int roleId, int legalId);
}