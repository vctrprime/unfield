using StadiumEngine.Common;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Domain.Services.Models.Notifications;
using StadiumEngine.Services.Notifications.Resolvers;

namespace StadiumEngine.Services.Infrastructure;

internal class SmsSender : ISmsSender
{
    private readonly ISmsTemplateResolver _templateResolver;

    public SmsSender( ISmsTemplateResolver templateResolver )
    {
        _templateResolver = templateResolver;
    }
    
    public async Task SendPasswordAsync( PasswordNotification passwordNotification )
    {
        string template = _templateResolver.ResolvePasswordNotificationTemplate( passwordNotification );
        await SendAsync( passwordNotification.PhoneNumber, String.Format( template, passwordNotification.Password ) );
    }

    public async Task SendBookingAccessCodeAsync( Booking booking, string language )
    {
        string template = _templateResolver.ResolveBookingAccessCodeTemplate( language );
        await SendAsync( booking.Customer.PhoneNumber, String.Format( template, booking.Number, $"{booking.Day:dd.MM.yyyy} {TimePointParser.Parse( booking.StartHour )}", booking.AccessCode ) );
    }

    public async Task SendBookingConfirmation( Booking booking, string language )
    {
        string template = _templateResolver.ResolveBookingConfirmationTemplate( booking, language );
        await SendAsync( booking.Customer.PhoneNumber, template );
    }

    private static async Task SendAsync( string phoneNumber, string message ) =>
        await Task.Run( () => Console.WriteLine( message ) );
}