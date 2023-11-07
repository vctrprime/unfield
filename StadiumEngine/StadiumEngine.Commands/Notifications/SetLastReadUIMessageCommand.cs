using Mediator;
using StadiumEngine.DTO.Notifications;

namespace StadiumEngine.Commands.Notifications;

public sealed class SetLastReadUIMessageCommand  : BaseCommand, IRequest<SetLastReadUIMessageDto>
{
    public int MessageId { get; set; }
}