using Mediator;
using Unfield.Common.Enums.Bookings;
using Unfield.DTO.Customers;
using Unfield.Queries.Customers;

namespace Unfield.Handlers.Resolvers.Customers;

internal class BookingAuthorizedCustomerResolver : IBookingAuthorizedCustomerResolver
{
    private readonly IMediator _mediator;

    public BookingAuthorizedCustomerResolver( IMediator mediator )
    {
        _mediator = mediator;
    }
    
    public async Task<AuthorizedCustomerDto?> ResolveAsync( BookingSource source, int? stadiumId )
    {
        if ( source == BookingSource.Form && stadiumId.HasValue )
        {
            AuthorizedCustomerDto? authorizedCustomer = await _mediator.Send( new GetAuthorizedCustomerQuery() );
            
            if ( authorizedCustomer != null && authorizedCustomer.Stadiums.Select( x => x.Id ).Contains( stadiumId.Value ) )
            {
                return authorizedCustomer;
            }
        }

        return null;
    }
}