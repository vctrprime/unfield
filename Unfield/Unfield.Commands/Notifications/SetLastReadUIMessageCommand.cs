using Mediator;
using Unfield.DTO.Notifications;

namespace Unfield.Commands.Notifications;

public sealed class SetLastReadUIMessageCommand  : BaseCommand, IRequest<SetLastReadUIMessageDto>
{
    public int MessageId { get; set; }
}