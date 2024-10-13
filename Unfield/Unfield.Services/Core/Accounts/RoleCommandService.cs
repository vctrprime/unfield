using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Services.Facades.Accounts;
using Unfield.Services.Checkers;

namespace Unfield.Services.Core.Accounts;

internal class RoleCommandService : IRoleCommandService
{
    private readonly IAccountsAccessChecker _accountsAccessChecker;
    private readonly IRoleRepositoryFacade _roleRepositoryFacade;
    private readonly IUserRepository _userRepository;

    public RoleCommandService(
        IRoleRepositoryFacade roleRepositoryFacade,
        IUserRepository userRepository,
        IAccountsAccessChecker accountsAccessChecker )
    {
        _roleRepositoryFacade = roleRepositoryFacade;
        _userRepository = userRepository;
        _accountsAccessChecker = accountsAccessChecker;
    }

    public void AddRole( Role role ) => _roleRepositoryFacade.AddRole( role );

    public async Task UpdateRoleAsync(
        int roleId,
        int stadiumGroupId,
        int userId,
        string name,
        string? description )
    {
        User? user = await _userRepository.GetAsync( roleId );

        if ( user?.RoleId == roleId )
        {
            throw new DomainException( ErrorsKeys.ModifyCurrentRole );
        }

        Role? role = await _roleRepositoryFacade.GetRoleAsync( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, stadiumGroupId );

        role!.Name = name;
        role.Description = description;
        role.UserModifiedId = userId;

        _roleRepositoryFacade.UpdateRole( role );
    }

    public async Task DeleteRoleAsync( int roleId, int stadiumGroupId, int userModifiedId )
    {
        Role? role = await _roleRepositoryFacade.GetRoleAsync( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, stadiumGroupId );

        if ( role != null )
        {
            if ( role.Users.Any() )
            {
                throw new DomainException(
                    ErrorsKeys.DeleteRoleHasBindings );
            }
            
            role.UserModifiedId = userModifiedId;
            _roleRepositoryFacade.RemoveRole( role );
        }
    }

    public async Task ToggleRolePermissionAsync( int roleId, int permissionId, int stadiumGroupId, int userId )
    {
        User? user = await _userRepository.GetAsync( userId );

        if ( user?.RoleId == roleId )
        {
            throw new DomainException( ErrorsKeys.ModifyPermissionsCurrentRole );
        }

        Role? role = await _roleRepositoryFacade.GetRoleAsync( roleId );
        _accountsAccessChecker.CheckRoleAccess( role, stadiumGroupId );

        RolePermission? rolePermission = await _roleRepositoryFacade.GetRolePermissionAsync( roleId, permissionId );
        if ( rolePermission == null )
        {
            rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                UserCreatedId = userId
            };
            _roleRepositoryFacade.AddRolePermission( rolePermission );
        }
        else
        {
            _roleRepositoryFacade.RemoveRolePermission( rolePermission );
        }
    }
}