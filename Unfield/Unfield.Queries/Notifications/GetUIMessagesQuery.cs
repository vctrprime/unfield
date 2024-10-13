using Mediator;
using Unfield.DTO.Notifications;

namespace Unfield.Queries.Notifications;

public sealed class GetUIMessagesQuery : BaseQuery, IRequest<List<UIMessageDto>>
{
    
}