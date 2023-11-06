using Mediator;
using StadiumEngine.DTO.Notifications;

namespace StadiumEngine.Queries.Notifications;

public sealed class GetUIMessagesQuery : BaseQuery, IRequest<List<UIMessageDto>>
{
    
}