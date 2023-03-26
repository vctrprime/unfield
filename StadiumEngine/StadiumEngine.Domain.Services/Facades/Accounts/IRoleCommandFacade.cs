using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IRoleCommandFacade
{
    void AddRole( Role role );

    Task UpdateRoleAsync(
        int roleId,
        int legalId,
        int userId,
        string name,
        string? description );

    Task DeleteRoleAsync( int roleId, int legalId, int userModifiedId );

    Task ToggleRolePermissionAsync(
        int roleId,
        int permissionId,
        int legalId,
        int userId );

    Task ToggleRoleStadiumAsync(
        int roleId,
        int stadiumId,
        int legalId,
        int userId );
}