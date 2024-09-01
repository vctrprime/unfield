using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Services.Core.Dashboard;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Jobs.Background.Dashboard;
using StadiumEngine.Jobs.Background.Notifications;

namespace StadiumEngine.Handlers.Handlers.Utils;

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