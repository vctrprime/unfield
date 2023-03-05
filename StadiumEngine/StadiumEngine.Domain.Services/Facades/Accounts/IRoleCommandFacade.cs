using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IRoleCommandFacade
{
    void AddRole( Role role );

    Task UpdateRole(
        int roleId,
        int legalId,
        int userId,
        string name,
        string description );

    Task DeleteRole( int roleId, int legalId, int userModifiedId );

    Task ToggleRolePermission(
        int roleId,
        int permissionId,
        int legalId,
        int userId );

    Task ToggleRoleStadium(
        int roleId,
        int stadiumId,
        int legalId,
        int userId );
}