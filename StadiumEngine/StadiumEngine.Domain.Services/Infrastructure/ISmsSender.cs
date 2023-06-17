using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Infrastructure;

public interface ISmsSender
{
    Task SendPasswordAsync( string phoneNumber, string password, string language );

    Task SendBookingAccessCodeAsync( Booking booking, string language );
}