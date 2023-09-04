using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Commands.Schedule;

public sealed class CancelSchedulerBookingCommand : BaseCommand, IRequest<CancelSchedulerBookingDto>
{
    public string BookingNumber { get; set; } = null!;
    public bool CancelOneInRow { get; set; }
    public string? Reason { get; set; }
    public DateTime Day { get; set; }
}