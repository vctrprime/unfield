using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Jobs.Background.Notifications;

namespace StadiumEngine.Handlers.Handlers.Utils;

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