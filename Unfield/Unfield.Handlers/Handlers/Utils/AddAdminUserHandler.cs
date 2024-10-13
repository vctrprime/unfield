using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Infrastructure;
using Unfield.DTO.Utils;
using Unfield.Commands.Utils;
using Unfield.Common.Enums.Notifications;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Jobs.Background.Notifications;

namespace Unfield.Handlers.Handlers.Utils;

internal sealed class AddAdminUserHandler : BaseCommandHandler<AddAdminUserCommand, AddAdminUserDto>
{
    private readonly INotificationsQueueManager _notificationsQueueManager;
    private readonly IUserCommandService _commandService;

    public AddAdminUserHandler(
        IUserCommandService commandService,
        INotificationsQueueManager notificationsQueueManager,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        null,
        unitOfWork )
    {
        _commandService = commandService;
        _notificationsQueueManager = notificationsQueueManager;
    }

    protected override async ValueTask<AddAdminUserDto> HandleCommandAsync( AddAdminUserCommand request,
        CancellationToken cancellationToken )
    {
        User? user = Mapper.Map<User>( request );

        string password = await _commandService.AddUserAsync( user, true );

        _notificationsQueueManager.EnqueuePasswordNotification(
            user.PhoneNumber,
            password,
            user.Language,
            PasswordNotificationType.Created,
            PasswordNotificationSubject.User );

        return new AddAdminUserDto();
    }
}