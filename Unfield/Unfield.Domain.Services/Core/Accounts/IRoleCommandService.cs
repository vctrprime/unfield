using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Services.Core.Accounts;

public interface IRoleCommandService
{
    void AddRole( Role role );

    Task UpdateRoleAsync(
        int roleId,
        int stadiumGroupId,
        int userId,
        string name,
        string? description );

    Task DeleteRoleAsync( int roleId, int stadiumGroupId, int userModifiedId );

    Task ToggleRolePermissionAsync(
        int roleId,
        int permissionId,
        int stadiumGroupId,
        int userId );
}