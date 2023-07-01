using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Services.Facades.Accounts;
using StadiumEngine.Services.Checkers;

namespace StadiumEngine.Services.Core.Accounts;

internal class UserCommandService : IUserCommandService
{
    private readonly IAccountsAccessChecker _accountsAccessChecker;
    private readonly IUserRepositoryFacade _userRepositoryFacade;
    private readonly IUserServiceFacade _userServiceFacade;

    public UserCommandService(
        IUserServiceFacade userServiceFacade,
        IUserRepositoryFacade userRepositoryFacade,
        IAccountsAccessChecker accountsAccessChecker
    )
    {
        _userServiceFacade = userServiceFacade;
        _userRepositoryFacade = userRepositoryFacade;
        _accountsAccessChecker = accountsAccessChecker;
    }

    public async Task<User> AuthorizeUserAsync( string login, string password )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( login );

        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.InvalidLogin );
        }

        if ( !_userServiceFacade.CheckPassword( user.Password, password ) )
        {
            throw new DomainException( ErrorsKeys.InvalidPassword );
        }

        user.LastLoginDate = DateTime.Now.ToUniversalTime();

        _userRepositoryFacade.UpdateUser( user );

        return user;
    }

    public async Task<User> ChangeStadiumAsync( int userId, int stadiumId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        ;

        if ( ( user.Role == null
               && user.Legal.Stadiums.FirstOrDefault( s => s.Id == stadiumId ) == null )
             ||
             ( user.Role != null
               && user.Role.RoleStadiums.Select( s => s.Stadium ).FirstOrDefault( s => s.Id == stadiumId ) == null ) )
        {
            throw new DomainException( ErrorsKeys.Forbidden );
        }

        return user;
    }

    public async Task ChangeLegalAsync( int userId, int legalId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        ;

        if ( user.LegalId == legalId )
        {
            return;
        }

        user.LegalId = legalId;
        _userRepositoryFacade.UpdateUser( user );
    }

    public async Task<string> AddUserAsync( User user, bool isAdminUser = false )
    {
        user.PhoneNumber = _userServiceFacade.CheckPhoneNumber( user.PhoneNumber );

        User? userSameNumber = await _userRepositoryFacade.GetUserAsync( user.PhoneNumber );
        if ( userSameNumber != null )
        {
            throw new DomainException(
                isAdminUser ? "Пользователь с таким номером телефона уже существует" : ErrorsKeys.LoginAlreadyExist );
        }

        if ( isAdminUser )
        {
            List<Legal> legals = await _userRepositoryFacade.GetLegalsAsync( String.Empty );
            user.LegalId = legals.First().Id;
        }
        else
        {
            Role? role = await _userRepositoryFacade.GetRoleAsync( user.RoleId ?? 0 );
            _accountsAccessChecker.CheckRoleAccess( role, user.LegalId );

            if ( !role!.RoleStadiums.Any() )
            {
                throw new DomainException( ErrorsKeys.UserRolesDoesntHaveStadiums );
            }
        }

        string userPassword = _userServiceFacade.GeneratePassword( 8 );
        user.Password = _userServiceFacade.CryptPassword( userPassword );

        _userRepositoryFacade.AddUser( user );

        return userPassword;
    }

    public async Task ChangeLanguageAsync( int userId, string language )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        user.Language = language;
        user.UserModifiedId = userId;

        _userRepositoryFacade.UpdateUser( user );
    }

    public async Task ChangePasswordAsync(
        int userId,
        string newPassword,
        string oldPassword )
    {
        if ( !_userServiceFacade.ValidatePassword( newPassword ) )
        {
            throw new DomainException( ErrorsKeys.PasswordDoesntMatchConditions );
        }

        User? user = await _userRepositoryFacade.GetUserAsync( userId );
        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        if ( !_userServiceFacade.CheckPassword( user.Password, oldPassword ) )
        {
            throw new DomainException( ErrorsKeys.InvalidPassword );
        }

        user.Password = _userServiceFacade.CryptPassword( newPassword );
        _userRepositoryFacade.UpdateUser( user );
    }

    public async Task<(User, string)> ResetPasswordAsync( string phoneNumber )
    {
        phoneNumber = _userServiceFacade.CheckPhoneNumber( phoneNumber );

        User? user = await _userRepositoryFacade.GetUserAsync( phoneNumber );
        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        string userPassword = _userServiceFacade.GeneratePassword( 8 );
        user.Password = _userServiceFacade.CryptPassword( userPassword );

        _userRepositoryFacade.UpdateUser( user );

        return ( user, userPassword );
    }

    public async Task UpdateUserAsync( User user, int legalId )
    {
        Role? role = await _userRepositoryFacade.GetRoleAsync( user.RoleId ?? 0 );
        _accountsAccessChecker.CheckRoleAccess( role, legalId );

        if ( !role!.RoleStadiums.Any() )
        {
            throw new DomainException( ErrorsKeys.UserRolesDoesntHaveStadiums );
        }

        _userRepositoryFacade.UpdateUser( user );
    }

    public async Task DeleteUserAsync( int userId, int legalId, int userModifiedId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        _accountsAccessChecker.CheckUserAccess( user, legalId );

        if ( user!.IsSuperuser )
        {
            throw new DomainException( ErrorsKeys.CantDeleteSuperuser );
        }

        user.UserModifiedId = userModifiedId;
        user.PhoneNumber = $"{user.PhoneNumber}.deleted-by-{userModifiedId}.{DateTime.Now.Ticks}";

        _userRepositoryFacade.RemoveUser( user );
    }
}