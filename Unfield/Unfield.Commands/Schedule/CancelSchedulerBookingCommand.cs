using Mediator;
using Unfield.DTO.Schedule;

namespace Unfield.Commands.Schedule;

public sealed class CancelSchedulerBookingCommand : BaseCancelBookingCommand, IRequest<CancelSchedulerBookingDto>
{
    
}