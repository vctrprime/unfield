using StadiumEngine.Common;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class SmsSender : ISmsSender
{
    public async Task SendPasswordAsync( string phoneNumber, string password, string language )
    {
        string template = language switch
        {
            "en" => SmsTemplates.SendPasswordEn,
            _ => SmsTemplates.SendPasswordRu
        };
        
        await SendAsync( phoneNumber, String.Format( template, password ) );
    }

    public async Task SendBookingAccessCodeAsync( Booking booking, string language )
    {
        string template = language switch
        {
            "en" => SmsTemplates.SendAccessCodeEn,
            _ => SmsTemplates.SendAccessCodeRu
        };

        await SendAsync( booking.Customer.PhoneNumber, String.Format( template, booking.Number, $"{booking.Day:dd.MM.yyyy} {TimePointParser.Parse( booking.StartHour )}", booking.AccessCode ) );
    }

    private static async Task SendAsync( string phoneNumber, string message ) =>
        await Task.Run( () => Console.WriteLine( message ) );
}