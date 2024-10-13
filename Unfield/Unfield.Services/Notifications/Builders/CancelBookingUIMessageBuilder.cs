using System.Globalization;
using Unfield.Common.Enums.Notifications;
using Unfield.Common.Static;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Services.Core.Notifications;

namespace Unfield.Services.Notifications.Builders;

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