using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Models.Notifications;

namespace StadiumEngine.Services.Notifications.Resolvers;

internal interface ISmsTemplateResolver
{
    string ResolvePasswordNotificationTemplate( PasswordNotification passwordNotification );
    string ResolveBookingAccessCodeTemplate( string language );
    string ResolveBookingConfirmationTemplate( Booking booking, string language );
}