using AutoMapper;
using Mediator;
using StadiumEngine.Common.Enums.Schedule;
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
                    WithDisabledEvents = false,
                    StadiumId = request.StadiumId
                },
                cancellationToken );

            List<BookingListItemDto> result = Mapper.Map<List<BookingListItemDto>>( events );
            return SortedStatusResult( result, false, request.ClientDate );
        }
        else
        {
            List<BookingListItem> bookings =
                await _queryService.SearchAllByNumberAsync( request.BookingNumber, _currentStadiumId );
            List<BookingListItemDto> result = Mapper.Map<List<BookingListItemDto>>( bookings );

            return SortedStatusResult( result, true, request.ClientDate );
        }
    }

    private static List<BookingListItemDto> SortedStatusResult(
        List<BookingListItemDto> result,
        bool byNumberQuery,
        DateTime clientDate )
    {
        foreach ( BookingListItemDto item in result )
        {
            if ( item.IsWeekly )
            {
                if ( !byNumberQuery )
                {
                    item.Status = GetStatusByDate( item, (int) BookingStatus.WeeklyItemActive,( int )BookingStatus.WeeklyItemFinished, clientDate );
                }
                else
                {
                    item.Status = item.Day.HasValue ? BookingStatus.WeeklyActive : BookingStatus.WeeklyFinished;
                }
            }
            else
            {
                item.Status = GetStatusByDate( item, (int) BookingStatus.Active, ( int )BookingStatus.Finished, clientDate );
            }
        }

        return result
            .OrderBy( x => x.Day )
            .ThenBy( x => x.OriginalData.StartHour )
            .ThenBy( x => x.FieldName )
            .ToList();
    }

    private static BookingStatus GetStatusByDate(
        BookingListItemDto item,
        int activeStatus,
        int finishedStatus,
        DateTime clientDate ) =>
        item.Day != null &&
        item.Day.Value.AddHours( ( double )( item.OriginalData.StartHour + item.OriginalData.HoursCount ) ) > clientDate
            ? ( BookingStatus )activeStatus
            : ( BookingStatus )finishedStatus;
}