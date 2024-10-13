using Microsoft.AspNetCore.Mvc;

namespace Unfield.Commands;

public class BaseCancelBookingCommand : BaseCommand
{
    [FromBody]
    public BaseCancelBookingCommandBody Data { get; set; } = null!;
}

public class BaseCancelBookingCommandBody
{
    public string BookingNumber { get; set; } = null!;
    public bool CancelOneInRow { get; set; }
    public string? Reason { get; set; }
    public DateTime Day { get; set; }
}