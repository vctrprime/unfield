using StadiumEngine.Common;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class SmsSender : ISmsSender
{
    public async Task SendPasswordAsync( string phoneNumber, string password, string language )
    {
        switch ( language )
        {
            case "en":
                await SendAsync( phoneNumber, String.Format( SmsTemplates.SendPasswordEn, password ) );
                break;
            default:
                await SendAsync( phoneNumber, String.Format( SmsTemplates.SendPasswordRu, password ) );
                break;
        }
    }

    public Task SendBookingNotificationAsync( Booking booking ) => SendAsync( booking.Customer.PhoneNumber, "test" );

    private static async Task SendAsync( string phoneNumber, string message ) =>
        await Task.Run( () => Console.WriteLine( message ) );
}