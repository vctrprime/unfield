using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Schedule;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class GetSchedulerEventsHandler : BaseRequestHandler<GetSchedulerEventsQuery, List<SchedulerEventDto>>
{
    private readonly ISchedulerQueryService _queryService;

    public GetSchedulerEventsHandler(
        ISchedulerQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<SchedulerEventDto>> Handle(
        GetSchedulerEventsQuery request,
        CancellationToken cancellationToken )
    {
        List<SchedulerEvent> events = await _queryService.GetEventsAsync( request.Start, request.End, _currentStadiumId, request.Language ?? "ru" );

        List<SchedulerEventDto> eventsDto = Mapper.Map<List<SchedulerEventDto>>( events );

        return eventsDto;
    }
}