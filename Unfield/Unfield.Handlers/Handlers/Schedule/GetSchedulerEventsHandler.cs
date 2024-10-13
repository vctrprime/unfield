using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Core.Schedule;
using Unfield.Domain.Services.Identity;
using Unfield.Domain.Services.Models.Schedule;
using Unfield.DTO.Schedule;
using Unfield.Queries.Schedule;

namespace Unfield.Handlers.Handlers.Schedule;

internal sealed class GetSchedulerEventsHandler : BaseRequestHandler<GetSchedulerEventsQuery, List<SchedulerEventDto>>
{
    private readonly ISchedulerQueryService _queryService;

    public GetSchedulerEventsHandler(
        ISchedulerQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<SchedulerEventDto>> Handle(
        GetSchedulerEventsQuery request,
        CancellationToken cancellationToken )
    {
        List<SchedulerEvent> events = await _queryService.GetEventsAsync(
            request.Start,
            request.End,
             _currentStadiumId == 0 && request.StadiumId.HasValue ? request.StadiumId.Value : _currentStadiumId,
            request.Language ?? "ru",
            request.WithDisabledEvents ?? true,
            request.CustomerPhoneNumber );

        List<SchedulerEventDto> eventsDto = Mapper.Map<List<SchedulerEventDto>>( events );

        int i = 1;
        foreach ( SchedulerEventDto eventDto in eventsDto )
        {
            eventDto.EventId = i;
            i++;
        }

        return eventsDto;
    }
}