using Unfield.Common.Enums.Notifications;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.DTO.Accounts.Users;
using Unfield.Jobs.Background.Notifications;

namespace Unfield.Handlers.Facades.Accounts.Users;

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