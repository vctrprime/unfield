using Unfield.Common.Enums.Bookings;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Resolvers.Customers;

internal interface IBookingAuthorizedCustomerResolver
{
    Task<AuthorizedCustomerDto?> ResolveAsync( BookingSource source, int? stadiumId );
}