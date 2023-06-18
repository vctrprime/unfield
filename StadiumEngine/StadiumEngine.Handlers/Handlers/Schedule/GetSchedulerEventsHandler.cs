using AutoMapper;
using StadiumEngine.Domain.Services.Facades.Schedule;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Schedule;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class GetSchedulerEventsHandler : BaseRequestHandler<GetSchedulerEventsQuery, List<SchedulerEventDto>>
{
    private readonly ISchedulerQueryFacade _schedulerQueryFacade;

    public GetSchedulerEventsHandler(
        ISchedulerQueryFacade schedulerQueryFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService) : base( mapper, claimsIdentityService )
    {
        _schedulerQueryFacade = schedulerQueryFacade;
    }

    public override async ValueTask<List<SchedulerEventDto>> Handle(
        GetSchedulerEventsQuery request,
        CancellationToken cancellationToken )
    {
        List<SchedulerEvent> events = await _schedulerQueryFacade.GetEventsAsync( request.Start, request.End, _currentStadiumId );

        List<SchedulerEventDto> eventsDto = Mapper.Map<List<SchedulerEventDto>>( events );

        return eventsDto;
    }
}