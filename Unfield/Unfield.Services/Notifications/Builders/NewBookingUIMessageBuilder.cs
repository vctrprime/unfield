using System.Globalization;
using Unfield.Common.Enums.Notifications;
using Unfield.Common.Static;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Services.Core.Notifications;

namespace Unfield.Services.Notifications.Builders;

public class NewBookingUIMessageBuilder : IUIMessageBuilder
{
    private readonly Booking _booking;

    public NewBookingUIMessageBuilder( Booking booking )
    {
        _booking = booking;
    }

    public UIMessage Build() =>
        new UIMessage
        {
            MessageType = UIMessageType.BookingFromForm,
            StadiumId = _booking.Field.StadiumId,
            Texts = new List<UIMessageText>
            {
                new UIMessageText
                {
                    Index = 0,
                    Text = _booking.Number
                },
                new UIMessageText
                {
                    Index = 1,
                    Text = $"{_booking.Day:dd.MM.yyyy} {TimePointParser.Parse( _booking.StartHour )}"
                },
                new UIMessageText
                {
                    Index = 2,
                    Text = _booking.Customer.Name
                },
                new UIMessageText
                {
                    Index = 3,
                    Text = _booking.TotalAmountAfterDiscount.ToString( CultureInfo.InvariantCulture )
                }
            }
        };
}