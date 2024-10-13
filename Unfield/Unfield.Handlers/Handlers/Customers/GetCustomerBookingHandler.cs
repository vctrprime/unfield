using AutoMapper;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Identity;
using Unfield.Domain.Services.Models.Schedule;
using Unfield.DTO.Schedule;
using Unfield.Queries.Customers;

namespace Unfield.Handlers.Handlers.Customers;

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