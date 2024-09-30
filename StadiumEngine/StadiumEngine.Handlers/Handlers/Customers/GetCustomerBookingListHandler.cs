using AutoMapper;
using Mediator;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Customers;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Customers;
using StadiumEngine.Queries.Schedule;

namespace StadiumEngine.Handlers.Handlers.Customers;

internal sealed class GetCustomerBookingListHandler : BaseCustomerRequestHandler<GetCustomerBookingListQuery, List<CustomerBookingListItemDto>>
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
        
        List<BookingListItemDto> bookings = await _mediator.Send(
            new GetBookingListQuery
            {
                ClientDate = request.ClientDate,
                Start = request.Start,
                End = request.End,
                Language = request.Language,
                StadiumId = stadium.Id,
                CustomerPhoneNumber = _customerPhoneNumber,
            },
            cancellationToken );
        
        List<CustomerBookingListItemDto> result = Mapper.Map<List<CustomerBookingListItemDto>>( bookings );

        return result;
    }
}