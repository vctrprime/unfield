using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Unfield.Common;
using Unfield.Common.Enums.Notifications;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Services.Facades.Accounts;
using Unfield.Services.Checkers;

namespace Unfield.Services.Core.Accounts;

internal class UserCommandService : IUserCommandService
{
    private readonly IAccountsAccessChecker _accountsAccessChecker;
    private readonly IUserRepositoryFacade _userRepositoryFacade;
    private readonly IUserServiceFacade _userServiceFacade;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly INotificationsQueueManager _notificationsQueueManager;

    public UserCommandService(
        IAccountsAccessChecker accountsAccessChecker,
        IUserRepositoryFacade userRepositoryFacade,
        IUserServiceFacade userServiceFacade,
        IStadiumRepository stadiumRepository,
        INotificationsQueueManager notificationsQueueManager )
    {
        _accountsAccessChecker = accountsAccessChecker;
        _userRepositoryFacade = userRepositoryFacade;
        _userServiceFacade = userServiceFacade;
        _stadiumRepository = stadiumRepository;
        _notificationsQueueManager = notificationsQueueManager;
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

        if ( user.StadiumGroup.Stadiums.FirstOrDefault( s => s.Id == stadiumId ) == null
             ||
             ( !user.IsSuperuser &&
               user.UserStadiums.Select( s => s.Stadium ).FirstOrDefault( s => s.Id == stadiumId ) == null ) )
        {
            throw new DomainException( ErrorsKeys.Forbidden );
        }

        return user;
    }

    public async Task ChangeStadiumGroupAsync( int userId, int stadiumGroupId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        if ( user.StadiumGroupId == stadiumGroupId )
        {
            return;
        }

        user.StadiumGroupId = stadiumGroupId;
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
            List<StadiumGroup> stadiumGroups = await _userRepositoryFacade.GetStadiumGroupsAsync( String.Empty );
            user.StadiumGroupId = stadiumGroups.First().Id;
        }
        else
        {
            List<Stadium> stadiums = await _stadiumRepository.GetForStadiumGroupAsync( user.StadiumGroupId );
            user.UserStadiums = new List<UserStadium>
            {
                new UserStadium
                {
                    StadiumId = stadiums.First().Id
                }
            };
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

    public async Task ResetPasswordAsync( string phoneNumber )
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
        _notificationsQueueManager.EnqueuePasswordNotification(
            phoneNumber,
            userPassword,
            user.Language,
            PasswordNotificationType.Reset,
            PasswordNotificationSubject.User );
    }

    public void UpdateUser( User user ) => _userRepositoryFacade.UpdateUser( user );

    public async Task DeleteUserAsync( int userId, int stadiumGroupId, int userModifiedId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        _accountsAccessChecker.CheckUserAccess( user, stadiumGroupId );

        if ( user!.IsSuperuser )
        {
            throw new DomainException( ErrorsKeys.CantDeleteSuperuser );
        }

        user.UserModifiedId = userModifiedId;
        user.PhoneNumber = $"{user.PhoneNumber}.deleted-by-{userModifiedId}.{DateTime.Now.Ticks}";

        _userRepositoryFacade.RemoveUser( user );
    }

    public async Task ToggleUserStadiumAsync(
        int userId,
        int stadiumId,
        int stadiumGroupId,
        int modifyUserId )
    {
        if ( userId == modifyUserId )
        {
            throw new DomainException( ErrorsKeys.ModifyStadiumsCurrentUser );
        }

        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        if ( user == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        List<Stadium> stadiums = await _stadiumRepository.GetForStadiumGroupAsync( stadiumGroupId );
        Stadium? stadium = stadiums.FirstOrDefault( s => s.Id == stadiumId );

        if ( stadium == null )
        {
            throw new DomainException( ErrorsKeys.StadiumNotFound );
        }

        UserStadium? userStadium = await _userRepositoryFacade.GetUserStadiumAsync( userId, stadiumId );
        if ( userStadium == null )
        {
            userStadium = new UserStadium
            {
                UserId = userId,
                StadiumId = stadiumId,
                UserCreatedId = modifyUserId
            };
            _userRepositoryFacade.AddUserStadium( userStadium );
        }
        else
        {
            if ( user.UserStadiums.All( rs => rs.StadiumId == stadiumId ) )
            {
                throw new DomainException( ErrorsKeys.LastUserStadiumUnbind );
            }

            _userRepositoryFacade.RemoveUserStadium( userStadium );
        }
    }
}