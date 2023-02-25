using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Services.Checkers;
using StadiumEngine.Services.Facades.Services.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class UserFacade : IUserFacade
{
    private readonly IUserServiceFacade _userServiceFacade;
    private readonly IUserRepositoryFacade _userRepositoryFacade;
    private readonly IAccountsAccessChecker _accountsAccessChecker;
    
    public UserFacade(
        IUserServiceFacade userServiceFacade, 
        IUserRepositoryFacade userRepositoryFacade,
        IAccountsAccessChecker accountsAccessChecker
        )
    {
        _userServiceFacade = userServiceFacade;
        _userRepositoryFacade = userRepositoryFacade;
        _accountsAccessChecker = accountsAccessChecker;
    }

    public async Task<User> AuthorizeUser(string login, string password)
    {
        var user = await _userRepositoryFacade.GetUser(login);

        if (user == null) throw new DomainException(ErrorsKeys.InvalidLogin);

        if (!_userServiceFacade.CheckPassword(user.Password, password)) throw new DomainException(ErrorsKeys.InvalidPassword);
        
        user.LastLoginDate = DateTime.Now.ToUniversalTime();
        
        _userRepositoryFacade.UpdateUser(user);

        return user;
    }

    public async Task<User> ChangeStadium(int userId, int stadiumId)
    {
        var user = await _userRepositoryFacade.GetUser(userId);

        if (user == null) throw new DomainException(ErrorsKeys.UserNotFound);;
        
        if ((user.Role == null 
             && user.Legal.Stadiums.FirstOrDefault(s => s.Id == stadiumId) == null)
            ||
            (user.Role != null 
             && user.Role.RoleStadiums.Select(s => s.Stadium).FirstOrDefault(s => s.Id == stadiumId) == null)) throw new DomainException(ErrorsKeys.Forbidden);

        return user;
    }

    public async Task ChangeLegal(int userId, int legalId)
    {
        var user = await _userRepositoryFacade.GetUser(userId);
        
        if (user == null) throw new DomainException(ErrorsKeys.UserNotFound);;

        if (user.LegalId == legalId) return;
        
        user.LegalId = legalId;
        _userRepositoryFacade.UpdateUser(user);
    }

    public async Task<string> AddUser(User user, bool isAdminUser = false)
    {
        user.PhoneNumber = _userServiceFacade.CheckPhoneNumber(user.PhoneNumber);

        var userSameNumber = await _userRepositoryFacade.GetUser(user.PhoneNumber);
        if (userSameNumber != null) throw new DomainException(isAdminUser ? "Пользователь с таким номером телефона уже существует" : ErrorsKeys.LoginAlreadyExist);
        
        if (isAdminUser)
        {
            var legals = await _userRepositoryFacade.GetLegals(string.Empty);
            user.LegalId = legals.First().Id;
        }
        else
        {
            var role = await _userRepositoryFacade.GetRole(user.RoleId ?? 0);
            _accountsAccessChecker.CheckRoleAccess(role, user.LegalId);
            
            if (!role.RoleStadiums.Any())
                throw new DomainException(ErrorsKeys.UserRolesDoesntHaveStadiums);
        }
        
        var userPassword = _userServiceFacade.GeneratePassword(8);
        user.Password = _userServiceFacade.CryptPassword(userPassword);
        
        _userRepositoryFacade.AddUser(user);

        return userPassword;
    }

    public async Task<User?> GetUser(int userId)
    {
        return await _userRepositoryFacade.GetUser(userId);
    }

    public async Task<List<User>> GetUsersByLegalId(int legalId)
    {
        return await _userRepositoryFacade.GetUsers(legalId);
    }

    public async Task ChangeLanguage(int userId, string language)
    {
        var user = await _userRepositoryFacade.GetUser(userId);

        if (user == null) throw new DomainException(ErrorsKeys.UserNotFound);
        
        user.Language = language;
        user.UserModifiedId = userId;
        
        _userRepositoryFacade.UpdateUser(user);
    }

    public async Task ChangePassword(
        int userId, 
        string newPassword, 
        string oldPassword)
    {
        if (!_userServiceFacade.ValidatePassword(newPassword)) throw new DomainException(ErrorsKeys.PasswordDoesntMatchConditions);
        
        var user = await _userRepositoryFacade.GetUser(userId);
        if (user == null) throw new DomainException(ErrorsKeys.UserNotFound);
        
        if (!_userServiceFacade.CheckPassword(user.Password, oldPassword)) throw new DomainException(ErrorsKeys.InvalidPassword);
        
        user.Password = _userServiceFacade.CryptPassword(newPassword);
        _userRepositoryFacade.UpdateUser(user);
    }

    public async Task<(User, string)> ResetPassword(string phoneNumber)
    {
        phoneNumber = _userServiceFacade.CheckPhoneNumber(phoneNumber);
        
        var user = await _userRepositoryFacade.GetUser(phoneNumber);
        if (user == null) throw new DomainException(ErrorsKeys.UserNotFound);
        
        var userPassword = _userServiceFacade.GeneratePassword(8);
        user.Password = _userServiceFacade.CryptPassword(userPassword);
        
        _userRepositoryFacade.UpdateUser(user);

        return (user, userPassword);
    }

    public async Task UpdateUser(User user, int legalId)
    {
        var role = await _userRepositoryFacade.GetRole(user.RoleId ?? 0);
        _accountsAccessChecker.CheckRoleAccess(role, legalId);
        
        if (!role.RoleStadiums.Any())
            throw new DomainException(ErrorsKeys.UserRolesDoesntHaveStadiums);
        
        _userRepositoryFacade.UpdateUser(user);
    }

    public async Task DeleteUser(int userId, int legalId, int userModifiedId)
    {
        var user = await _userRepositoryFacade.GetUser(userId);
        
        _accountsAccessChecker.CheckUserAccess(user, legalId);
        
        if (user.IsSuperuser) throw new DomainException(ErrorsKeys.CantDeleteSuperuser);

        user.UserModifiedId = userModifiedId;
        user.PhoneNumber = $"{user.PhoneNumber}.deleted-by-{userModifiedId}.{DateTime.Now.Ticks}";
        
        _userRepositoryFacade.RemoveUser(user);
    }

    public async Task<List<Permission>> GetUserPermissions(int userId)
    {
        var user = await _userRepositoryFacade.GetUser(userId);

        return user switch
        {
            { Role: { } } => await _userRepositoryFacade.GetPermissions(user.Role.Id),
            _ => await _userRepositoryFacade.GetPermissions()
        };
    }
    
    public async Task<List<Stadium>> GetUserStadiums(int userId, int legalId)
    {
        var user = await _userRepositoryFacade.GetUser(userId);

        return user switch
        {
            { Role: { } } => await _userRepositoryFacade.GetStadiumsForRole(user.Role.Id),
            _ => await _userRepositoryFacade.GetStadiumsForLegal(legalId)
        };
    }

    
}