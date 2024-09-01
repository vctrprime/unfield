using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Core.Accounts;

public interface IRoleQueryService
{
    Task<List<Role>> GetRolesForStadiumGroupAsync( int stadiumGroupId );
    Task<Dictionary<Permission, bool>> GetPermissionsForRoleAsync( int roleId, int stadiumGroupId );
}