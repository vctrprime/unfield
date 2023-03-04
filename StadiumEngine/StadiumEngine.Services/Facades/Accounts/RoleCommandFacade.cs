using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Services.Checkers;
using StadiumEngine.Services.Facades.Services.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class RoleCommandFacade : IRoleCommandFacade
{
    private readonly IRoleRepositoryFacade _roleRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountsAccessChecker _accountsAccessChecker;

    public RoleCommandFacade(
        IRoleRepositoryFacade roleRepositoryFacade,
        IStadiumRepository stadiumRepository,
        IUserRepository userRepository,
        IAccountsAccessChecker accountsAccessChecker)
    {
        _roleRepositoryFacade = roleRepositoryFacade;
        _stadiumRepository = stadiumRepository;
        _userRepository = userRepository;
        _accountsAccessChecker = accountsAccessChecker;
    }
    
    public void AddRole(Role role)
    {
        _roleRepositoryFacade.AddRole(role);
    }

    public async Task UpdateRole(
        int roleId, 
        int legalId,
        int userId,
        string name,
        string description)
    {
        var user = await _userRepository.Get(roleId);
        
        if (user?.RoleId == roleId)
            throw new DomainException(ErrorsKeys.ModifyCurrentRole);
        
        var role = await _roleRepositoryFacade.GetRole(roleId);
        _accountsAccessChecker.CheckRoleAccess(role, legalId);

        role.Name = name;
        role.Description = description;
        role.UserModifiedId = userId;
        
        _roleRepositoryFacade.UpdateRole(role);
    }

    public async Task DeleteRole(int roleId, int legalId, int userModifiedId)
    {
        var role = await _roleRepositoryFacade.GetRole(roleId);
        _accountsAccessChecker.CheckRoleAccess(role, legalId);

        if (role.RoleStadiums.Any() || role.Users.Any())
            throw new DomainException(
                ErrorsKeys.DeleteRoleHasBindings);
        
        role.UserModifiedId = userModifiedId;
        _roleRepositoryFacade.RemoveRole(role);
    }
    
    public async Task ToggleRolePermission(int roleId, int permissionId, int legalId, int userId)
    {
        var user = await _userRepository.Get(userId);
        
        if (user?.RoleId == roleId)
            throw new DomainException(ErrorsKeys.ModifyPermissionsCurrentRole);
        
        var role = await _roleRepositoryFacade.GetRole(roleId);
        _accountsAccessChecker.CheckRoleAccess(role, legalId);
        
        var rolePermission = await _roleRepositoryFacade.GetRolePermission(roleId, permissionId);
        if (rolePermission == null)
        {
            rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                UserCreatedId = userId
            };
            _roleRepositoryFacade.AddRolePermission(rolePermission);
        }
        else
        {
            _roleRepositoryFacade.RemoveRolePermission(rolePermission);
        }
    }

    public async Task ToggleRoleStadium(int roleId, int stadiumId, int legalId, int userId)
    {
        var user = await _userRepository.Get(userId);
        
        if (user?.RoleId == roleId)
            throw new DomainException(ErrorsKeys.ModifyStadiumsCurrentRole);
        
        var role = await _roleRepositoryFacade.GetRole(roleId);
        _accountsAccessChecker.CheckRoleAccess(role, legalId);

        var stadiums = await _stadiumRepository.GetForLegal(legalId);
        var stadium = stadiums.FirstOrDefault(s => s.Id == stadiumId);
        
        if (stadium == null) throw new DomainException(ErrorsKeys.StadiumNotFound);
        
        var roleStadium = await _roleRepositoryFacade.GetRoleStadium(roleId, stadiumId);
        if (roleStadium == null)
        {
            roleStadium = new RoleStadium
            {
                RoleId = roleId,
                StadiumId = stadiumId,
                UserCreatedId = userId
            };
            _roleRepositoryFacade.AddRoleStadium(roleStadium);
        }
        else
        {
            if (role.Users.Any() 
                && !(role.RoleStadiums.Any(rs => rs.StadiumId != stadiumId)))
            {
                throw new DomainException(ErrorsKeys.LastRoleStadiumUnbind);
            }
            _roleRepositoryFacade.RemoveRoleStadium(roleStadium);
        }
    }
}