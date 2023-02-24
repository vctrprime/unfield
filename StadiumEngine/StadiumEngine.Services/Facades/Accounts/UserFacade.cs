using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Services.Facades.Services.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class UserFacade : IUserFacade
{
    private readonly IUserServiceFacade _userServiceFacade;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ILegalRepository _legalRepository;

    public UserFacade(
        IUserServiceFacade userServiceFacade, 
        IUserRepository userRepository, 
        IRoleRepository roleRepository,
        ILegalRepository legalRepository)
    {
        _userServiceFacade = userServiceFacade;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _legalRepository = legalRepository;
    }
    
    public async Task<string> AddUser(User user, bool isAdminUser = false)
    {
        user.PhoneNumber = _userServiceFacade.CheckPhoneNumber(user.PhoneNumber);

        var userSameNumber = await _userRepository.Get(user.PhoneNumber);
        if (userSameNumber != null) throw new DomainException(isAdminUser ? "Пользователь с таким номером телефона уже существует" : ErrorsKeys.LoginAlreadyExist);
        
        
        if (isAdminUser)
        {
            var legalId = await _legalRepository.GetByFilter(string.Empty);
            user.LegalId = legalId.First().Id;
        }
        else
        {
            var role = await _roleRepository.Get(user.RoleId ?? 0);
            CheckRoleAccess(role, user.LegalId);
            
            if (!role.RoleStadiums.Any())
                throw new DomainException(ErrorsKeys.UserRolesDoesntHaveStadiums);
        }
        
        var userPassword = _userServiceFacade.GeneratePassword(8);
        user.Password = _userServiceFacade.CryptPassword(userPassword);
        
        _userRepository.Add(user);

        return userPassword;
    }
    
    private void CheckRoleAccess(Role? role, int legalId)
    {
        if (role == null || legalId != role.LegalId) throw new DomainException(ErrorsKeys.RoleNotFound);
    }
}