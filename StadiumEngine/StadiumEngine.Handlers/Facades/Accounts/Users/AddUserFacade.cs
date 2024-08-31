using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Jobs.Background.Notifications;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal class AddUserFacade : IAddUserFacade
{
    private readonly IUserCommandService _commandService;
    private readonly INotificationsQueueManager _notificationsQueueManager;

    public AddUserFacade(
        IUserCommandService commandService,
        INotificationsQueueManager notificationsQueueManager )
    {
        _commandService = commandService;
        _notificationsQueueManager = notificationsQueueManager;
    }

    public async Task<AddUserDto> AddAsync( User user )
    {
        string password = await _commandService.AddUserAsync( user );
        _notificationsQueueManager.EnqueuePasswordNotification(
            user.PhoneNumber,
            password,
            user.Language,
            PasswordNotificationType.Created,
            PasswordNotificationSubject.User );
        
        return new AddUserDto();
    }
}