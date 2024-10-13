using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Models.Notifications;

namespace Unfield.Services.Notifications.Resolvers;

internal interface ISmsTemplateResolver
{
    string ResolvePasswordNotificationTemplate( PasswordNotification passwordNotification );
    string ResolveBookingAccessCodeTemplate( string language );
    string ResolveBookingConfirmationTemplate( Booking booking, string language );
}