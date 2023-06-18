using AutoMapper;
using Mediator;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Offers.Fields;
using StadiumEngine.Queries.Schedule;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class GetSchedulerFieldsHandler : BaseRequestHandler<GetSchedulerFieldsQuery, SchedulerFieldsDto>
{
    private readonly IMediator _mediator;
    private readonly IStadiumMainSettingsQueryFacade _settingsQueryFacade;

    public GetSchedulerFieldsHandler(
        IMediator mediator,
        IStadiumMainSettingsQueryFacade settingsQueryFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService) : base( mapper, claimsIdentityService )
    {
        _mediator = mediator;
        _settingsQueryFacade = settingsQueryFacade;
    }

    public override async ValueTask<SchedulerFieldsDto> Handle(
        GetSchedulerFieldsQuery request,
        CancellationToken cancellationToken )
    {
        List<FieldDto> fields = await _mediator.Send( new GetFieldsQuery(), cancellationToken );
        StadiumMainSettings settings = await _settingsQueryFacade.GetByStadiumIdAsync( _currentStadiumId );

        return new SchedulerFieldsDto
        {
            StartHour = settings.OpenTime,
            EndHour = settings.CloseTime,
            Data = fields.Where( x => x.IsActive ).Select( x => new SchedulerFieldDto( x ) ).ToList(),
        };
    }
}