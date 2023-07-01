using AutoMapper;
using Mediator;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Core.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Offers.Fields;
using StadiumEngine.Queries.Schedule;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class GetSchedulerFieldsHandler : BaseRequestHandler<GetSchedulerFieldsQuery, SchedulerFieldsDto>
{
    private readonly IMediator _mediator;
    private readonly IMainSettingsQueryService _mainSettingsQueryService;

    public GetSchedulerFieldsHandler(
        IMediator mediator,
        IMainSettingsQueryService mainSettingsQueryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService) : base( mapper, claimsIdentityService )
    {
        _mediator = mediator;
        _mainSettingsQueryService = mainSettingsQueryService;
    }

    public override async ValueTask<SchedulerFieldsDto> Handle(
        GetSchedulerFieldsQuery request,
        CancellationToken cancellationToken )
    {
        List<FieldDto> fields = await _mediator.Send( new GetFieldsQuery(), cancellationToken );
        MainSettings settings = await _mainSettingsQueryService.GetByStadiumIdAsync( _currentStadiumId );

        return new SchedulerFieldsDto
        {
            StartHour = settings.OpenTime,
            EndHour = settings.CloseTime,
            Data = fields.Where( x => x.IsActive ).Select( x => new SchedulerFieldDto( x ) ).ToList(),
        };
    }
}