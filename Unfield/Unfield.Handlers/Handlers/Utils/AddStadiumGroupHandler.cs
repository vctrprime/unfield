using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Infrastructure;
using Unfield.DTO.Utils;
using Unfield.Commands.Utils;
using Unfield.Common.Enums.Notifications;
using Unfield.Domain.Services.Core.Dashboard;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Jobs.Background.Dashboard;
using Unfield.Jobs.Background.Notifications;

namespace Unfield.Handlers.Handlers.Utils;

internal sealed class AddStadiumGroupHandler : BaseCommandHandler<AddStadiumGroupCommand, AddStadiumGroupDto>
{
    private readonly IStadiumGroupCommandService _commandService;
    private readonly INotificationsQueueManager _notificationsQueueManager;
    private readonly IDashboardQueueManager _dashboardQueueManager;

    public AddStadiumGroupHandler(
        IStadiumGroupCommandService commandService,
        INotificationsQueueManager notificationsQueueManager,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IDashboardQueueManager dashboardQueueManager ) : base(
        mapper,
        null,
        unitOfWork,
        false )
    {
        _commandService = commandService;
        _notificationsQueueManager = notificationsQueueManager;
        _dashboardQueueManager = dashboardQueueManager;
    }

    protected override async ValueTask<AddStadiumGroupDto> HandleCommandAsync(
        AddStadiumGroupCommand request,
        CancellationToken cancellationToken )
    {
        StadiumGroup? stadiumGroup = Mapper.Map<StadiumGroup>( request );
        User? superuser = Mapper.Map<User>( request.Superuser );

        superuser.Language = request.Language;

        string password = await _commandService.AddStadiumGroupAsync( stadiumGroup, superuser );

        _notificationsQueueManager.EnqueuePasswordNotification(
            superuser.PhoneNumber,
            password,
            superuser.Language,
            PasswordNotificationType.Created,
            PasswordNotificationSubject.User );

        await UnitOfWork.SaveChangesAsync();

        AddStadiumGroupDto? addStadiumGroupDto = Mapper.Map<AddStadiumGroupDto>( stadiumGroup );

        foreach ( Stadium stadium in stadiumGroup.Stadiums )
        {
            _dashboardQueueManager.EnqueueCalculateStadiumDashboard( stadium.Id );
        }

        return addStadiumGroupDto;
    }
}