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

    public async Task SendBookingConfirmation( Booking booking, string language )
    {
        string template = language switch
        {
            "en" => SmsTemplates.SendConfirmationEn,
            _ => SmsTemplates.SendConfirmationRu
        };

        if ( booking.BookingLockerRoom != null )
        {
            string lockerRoomTemplate = language switch
            {
                "en" => SmsTemplates.SendConfirmationLockerRoomEn,
                _ => SmsTemplates.SendConfirmationLockerRoomRu
            };
            lockerRoomTemplate = String.Format( lockerRoomTemplate, booking.BookingLockerRoom.LockerRoom.Name );
            template = String.Format( template, booking.Number, lockerRoomTemplate );
        }
        else
        {
            template = String.Format( template, booking.Number, String.Empty );
        }

        await SendAsync( booking.Customer.PhoneNumber, template );
    }

    private static async Task SendAsync( string phoneNumber, string message ) =>
        await Task.Run( () => Console.WriteLine( message ) );
}