using System.Globalization;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications.Builders;

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