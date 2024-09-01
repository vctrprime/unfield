using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Resolvers.Customers;

internal interface IBookingAuthorizedCustomerResolver
{
    Task<AuthorizedCustomerDto?> ResolveAsync( BookingSource source, int? stadiumId );
}