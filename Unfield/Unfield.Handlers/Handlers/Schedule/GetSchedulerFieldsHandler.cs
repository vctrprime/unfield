using AutoMapper;
using Mediator;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Services.Core.Settings;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Fields;
using Unfield.DTO.Schedule;
using Unfield.Queries.Offers.Fields;
using Unfield.Queries.Schedule;

namespace Unfield.Handlers.Handlers.Schedule;

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