using AutoMapper;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

internal sealed class GetCustomerBookingHandler : BaseCustomerRequestHandler<GetCustomerBookingQuery, BookingListItemDto>
{
    private readonly ICustomerBookingQueryService _queryService;

    public GetCustomerBookingHandler( 
        IMapper mapper, 
        ICustomerClaimsIdentityService claimsIdentityService, 
        ICustomerBookingQueryService queryService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }
    
    public override async ValueTask<BookingListItemDto> Handle(
        GetCustomerBookingQuery request,
        CancellationToken cancellationToken )
    {
        BookingListItem booking = await _queryService.GetCustomerBookingAsync(
            request.Number,
            _customerPhoneNumber,
            request.StadiumToken,
            request.Day );
        
        BookingListItemDto bookingDto = Mapper.Map<BookingListItemDto>( booking );

        return bookingDto;
    }
}