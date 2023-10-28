using AutoMapper;
using Mediator;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Schedule;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class GetBookingListHandler : BaseRequestHandler<GetBookingListQuery, List<BookingListItemDto>>
{
    private readonly IMediator _mediator;
    private readonly ISchedulerQueryService _queryService;

    public GetBookingListHandler(
        IMediator mediator,
        ISchedulerQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _mediator = mediator;
        _queryService = queryService;
    }

    public override async ValueTask<List<BookingListItemDto>> Handle(
        GetBookingListQuery request,
        CancellationToken cancellationToken )
    {
        if ( String.IsNullOrEmpty( request.BookingNumber ) )
        {
            if ( !request.Start.HasValue || !request.End.HasValue )
            {
                return new List<BookingListItemDto>();
            }

            List<SchedulerEventDto> events = await _mediator.Send(
                new GetSchedulerEventsQuery
                {
                    ClientDate = request.ClientDate,
                    Start = request.Start.Value,
                    End = request.End.Value,
                    Language = request.Language,
                    WithDisabledEvents = false
                },
                cancellationToken );

            IEnumerable<BookingListItemDto> result = Mapper.Map<IEnumerable<BookingListItemDto>>( events.Select( x => x.Data ) );
            return SortedResult( result );
        }
        else
        {
            List<BookingListItem> bookings = await _queryService.SearchAllByNumberAsync( request.BookingNumber, _currentStadiumId );
            IEnumerable<BookingListItemDto> result = Mapper.Map<IEnumerable<BookingListItemDto>>( bookings );

            return SortedResult( result );
        }
    }

    private static List<BookingListItemDto> SortedResult( IEnumerable<BookingListItemDto> result ) =>
        result
            .OrderBy( x => x.Day )
            .ThenBy( x => TimePointParser.Parse( x.Time ) )
            .ToList();
}