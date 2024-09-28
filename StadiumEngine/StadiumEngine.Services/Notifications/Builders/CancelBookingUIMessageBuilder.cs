using System.Globalization;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications.Builders;

public class CancelBookingUIMessageBuilder : IUIMessageBuilder
{
    private readonly Booking _booking;
    private readonly DateTime _day;
    private readonly string? _reason;

    public CancelBookingUIMessageBuilder( Booking booking, DateTime day, string? reason )
    {
        _booking = booking;
        _day = day;
        _reason = reason;
    }

    public UIMessage Build() =>
        new UIMessage
        {
            MessageType = UIMessageType.CancelBookingByCustomer,
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
                    Text = $"{_day:dd.MM.yyyy} {TimePointParser.Parse( _booking.StartHour )}"
                },
                new UIMessageText
                {
                    Index = 2,
                    Text = _reason
                },
                new UIMessageText
                {
                    Index = 3,
                    Text = _booking.IsWeekly ? _booking.IsWeeklyStoppedDate.HasValue ? 
                            "weekly_full" : "weekly" : "single"
                },
            }
        };
}