using Unfield.Common;
using Unfield.Common.Enums.Notifications;
using Unfield.Common.Static;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Infrastructure;
using Unfield.Domain.Services.Models.Notifications;
using Unfield.Services.Notifications.Resolvers;

namespace Unfield.Services.Infrastructure;

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

        template = passwordNotification.Subject == PasswordNotificationSubject.Customer && !String.IsNullOrEmpty( passwordNotification.StadiumGroupName )
            ? String.Format( template, passwordNotification.Password, passwordNotification.StadiumGroupName )
            : String.Format( template, passwordNotification.Password );
        
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