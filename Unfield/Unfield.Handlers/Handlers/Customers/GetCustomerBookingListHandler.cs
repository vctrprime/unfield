using AutoMapper;
using Mediator;
using Unfield.Common;
using Unfield.Common.Enums.Customers;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Customers;
using Unfield.DTO.Schedule;
using Unfield.Queries.Customers;
using Unfield.Queries.Schedule;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class
    GetCustomerBookingListHandler : BaseCustomerRequestHandler<GetCustomerBookingListQuery,
    List<CustomerBookingListItemDto>>
{
    private readonly IMediator _mediator;
    private readonly IStadiumQueryService _stadiumQueryService;

    public GetCustomerBookingListHandler(
        IMediator mediator,
        IStadiumQueryService stadiumQueryService,
        IMapper mapper,
        ICustomerClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _mediator = mediator;
        _stadiumQueryService = stadiumQueryService;
    }

    public override async ValueTask<List<CustomerBookingListItemDto>> Handle(
        GetCustomerBookingListQuery request,
        CancellationToken cancellationToken )
    {
        Stadium? stadium = await _stadiumQueryService.GetAsync( request.StadiumToken );

        if ( stadium is null )
        {
            throw new DomainException( ErrorsKeys.StadiumNotFound );
        }

        DateTime start = request.Mode == CustomerBookingListMode.Previous
            ? request.ClientDate.AddMonths( -2 )
            : request.ClientDate;

        DateTime end = request.Mode == CustomerBookingListMode.Previous
            ? request.ClientDate.AddMinutes( 1 )
            : request.ClientDate.AddMonths( 1 );

        List<BookingListItemDto> bookings = await _mediator.Send(
            new GetBookingListQuery
            {
                ClientDate = request.ClientDate,
                Start = start,
                End = end,
                Language = request.Language,
                StadiumId = stadium.Id,
                CustomerPhoneNumber = _customerPhoneNumber,
            },
            cancellationToken );

        List<CustomerBookingListItemDto> result = Mapper.Map<List<CustomerBookingListItemDto>>( bookings );

        return result;
    }
}