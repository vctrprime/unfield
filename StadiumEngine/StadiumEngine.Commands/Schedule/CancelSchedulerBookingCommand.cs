using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Commands.Schedule;

public sealed class CancelSchedulerBookingCommand : BaseCancelBookingCommand, IRequest<CancelSchedulerBookingDto>
{
    
}